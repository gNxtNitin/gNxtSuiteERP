Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmScrapConv
    Inherits System.Windows.Forms.Form
    Dim RsScrapHdr As ADODB.Recordset ''Recordset
    Dim RsScrapDet As ADODB.Recordset ''Recordset
    'Private PvtDBCn As ADODB.Connection

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim xMyMenu As String

    Dim FormActive As Boolean

    Private Const ConRowHeight As Short = 12

    Private Const ColItemCode As Short = 1
    Private Const ColItemDesc As Short = 2
    Private Const ColUom As Short = 3
    Private Const ColItemWt As Short = 4
    Private Const ColStockQty As Short = 5
    Private Const ColScrapQty As Short = 6
    Private Const ColScrapWt As Short = 7
    Private Const ColRemarks As Short = 8

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click

        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtDate.Enabled = True
            txtItem.Enabled = True
            cmdItemSearch.Enabled = True
            SprdMain.Enabled = True
            txtNumber.Enabled = False
        Else
            CmdAdd.Text = ConCmdAddCaption
            ADDMode = False
            MODIFYMode = False
            MainClass.ClearGrid(SprdMain)
            Call FormatSprdMain(-1)
            Show1()
        End If
        Exit Sub
AddErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDelete.Click
        On Error GoTo DelErrPart

        Dim mItemCode As String

        If ValidateBranchLocking((txtDate.Text)) = True Then
            Exit Sub
        End If

        If Trim(txtNumber.Text) = "" Then MsgInformation("Nothing to Delete") : Exit Sub

        If Not RsScrapHdr.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "INV_SCRAP_CONV_HDR", (txtNumber.Text), RsScrapHdr, "AUTO_KEY_SCRAP") = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "INV_SCRAP_CONV_HDR", "AUTO_KEY_SCRAP", (txtNumber.Text)) = False Then GoTo DelErrPart

                If DeleteStockTRN(PubDBCn, ConStockRefType_SCP, (txtNumber.Text)) = False Then GoTo DelErrPart

                PubDBCn.Execute("Delete From FIN_RGDAILYMANU_HDR Where Mkey='" & Val(txtNumber.Text) & "' AND BOOKTYPE='C'")
                PubDBCn.Execute("Delete from INV_SCRAP_CONV_DET Where AUTO_KEY_SCRAP=" & Val(txtNumber.Text) & "")
                PubDBCn.Execute("Delete from INV_SCRAP_CONV_HDR Where AUTO_KEY_SCRAP=" & Val(txtNumber.Text) & "")

                PubDBCn.CommitTrans()
                RsScrapHdr.Requery() ''.Refresh
                RsScrapDet.Requery() ''.Refresh
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans() ''
        RsScrapHdr.Requery() ''.Refresh
        RsScrapDet.Requery() ''.Refresh
        If Err.Description <> "" Then
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdModify.Click

        On Error GoTo ModifyErr

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsScrapHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True
            txtNumber.Enabled = False
            txtDate.Enabled = False
            txtItem.Enabled = False
            cmdItemSearch.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ModifyErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdPopulate_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPopulate.Click

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim CntRow As Integer
        Dim mUOM As String = ""
        Dim mScrapWt As Double
        Dim mTableName As String

        Dim mDivisionCode As Double

        MainClass.ClearGrid(SprdMain)

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If


        If Trim(txtItem.Text) = "" Then
            MsgInformation("Please Enter Item Code.")
            Exit Sub
        End If

        If Trim(txtDate.Text) = "" Then
            MsgInformation("Please Enter Date.")
            Exit Sub
        End If

        mTableName = ConInventoryTable

        '    If RsCompany.fields("COMPANY_CODE").value = 1 Then
        '        mTableName = "INV_STOCK_REC_TRN" & RsCompany.fields("FYEAR").value
        '    ElseIf RsCompany.fields("COMPANY_CODE").value = 3 Or RsCompany.fields("COMPANY_CODE").value = 10 Or RsCompany.fields("COMPANY_CODE").value = 12 Then
        '        mTableName = "INV_STOCK_REC_TRN" & VB6.Format(RsCompany.fields("COMPANY_CODE").value, "00") & RsCompany.fields("FYEAR").value
        '    Else
        '        mTableName = "INV_STOCK_REC_TRN"
        '    End If

        SqlStr = " SELECT STOCK.ITEM_CODE, ITEM.ITEM_SHORT_DESC, STOCK.ITEM_UOM, " & vbCrLf & " TO_CHAR(SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))) AS STOCKQTY, " & vbCrLf & " ITEM_WEIGHT "

        SqlStr = SqlStr & vbCrLf & " FROM " & mTableName & " STOCK, " & vbCrLf & " INV_ITEM_MST ITEM "

        ''**********WHERE CLAUSE .......*************

        SqlStr = SqlStr & vbCrLf & " Where " & vbCrLf & " STOCK.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND STOCK.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND STOCK.COMPANY_CODE=ITEM.COMPANY_CODE " & vbCrLf & " AND STOCK.ITEM_CODE=ITEM.ITEM_CODE " & vbCrLf & " AND STOCK.STOCK_ID='" & ConWH & "'" & vbCrLf & " AND STOCK.STOCK_TYPE='SC'" & vbCrLf & " AND ITEM.SCRAP_ITEM_CODE='" & MainClass.AllowSingleQuote(txtItem.Text) & "'" & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

        SqlStr = SqlStr & vbCrLf & " AND STOCK.STATUS='O'"

        SqlStr = SqlStr & vbCrLf & " AND STOCK.DIV_CODE='" & mDivisionCode & "'"

        SqlStr = SqlStr & vbCrLf & " HAVING SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))>0"
        SqlStr = SqlStr & vbCrLf & "GROUP BY STOCK.ITEM_CODE, ITEM.ITEM_SHORT_DESC, STOCK.ITEM_UOM, ITEM_WEIGHT"
        SqlStr = SqlStr & vbCrLf & "ORDER BY STOCK.ITEM_CODE, ITEM.ITEM_SHORT_DESC"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        CntRow = 1
        With SprdMain
            If RsTemp.EOF = False Then
                Do While Not RsTemp.EOF
                    .Row = CntRow
                    .Col = ColItemCode
                    .Text = Trim(IIf(IsDbNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))

                    .Col = ColItemDesc
                    .Text = Trim(IIf(IsDbNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value))

                    .Col = ColUom
                    .Text = Trim(IIf(IsDbNull(RsTemp.Fields("ITEM_UOM").Value), "", RsTemp.Fields("ITEM_UOM").Value))
                    mUOM = Trim(IIf(IsDbNull(RsTemp.Fields("ITEM_UOM").Value), "", RsTemp.Fields("ITEM_UOM").Value))

                    .Col = ColItemWt
                    .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("ITEM_WEIGHT").Value), 0, RsTemp.Fields("ITEM_WEIGHT").Value) / 1000, "0.000")

                    .Col = ColStockQty
                    .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("STOCKQTY").Value), 0, RsTemp.Fields("STOCKQTY").Value), "0.000")

                    .Col = ColScrapQty
                    .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("STOCKQTY").Value), 0, RsTemp.Fields("STOCKQTY").Value), "0.000") ''"0.00"

                    .Col = ColScrapWt
                    mScrapWt = IIf(IsDbNull(RsTemp.Fields("STOCKQTY").Value), 0, RsTemp.Fields("STOCKQTY").Value) * IIf(IsDbNull(RsTemp.Fields("ITEM_WEIGHT").Value), 0, RsTemp.Fields("ITEM_WEIGHT").Value)
                    mScrapWt = mScrapWt / 1000

                    .Text = VB6.Format(mScrapWt, "0.000")

                    .Col = ColRemarks
                    .Text = ""

                    CntRow = CntRow + 1
                    .MaxRows = CntRow
                    RsTemp.MoveNext()
                Loop
            End If
        End With

        Call CalcTots()
        FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportONScrap(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportONScrap(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportONScrap(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""
        Dim SqlStr As String = ""
        Dim mRptFileName As String
        Dim mVNo As String

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        SqlStr = ""

        Call MainClass.ClearCRptFormulas(Report1)

        Call SelectQryForScrap(SqlStr)


        mTitle = "SCRAP CONVERTION"
        mSubTitle = ""
        mRptFileName = "SCRAP.rpt"

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String)
        On Error GoTo ErrPart

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle, True) '', xMyMenu
        Report1.ReportFileName = PubReportFolderPath & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False

        Report1.Action = 1
        Report1.Reset()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Function SelectQryForScrap(ByRef mSqlStr As String) As String

        ''SELECT CLAUSE...

        mSqlStr = " SELECT " & vbCrLf & " IH.*, ID.*, INVMST_H.ITEM_SHORT_DESC "


        ''FROM CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " FROM INV_SCRAP_CONV_HDR IH, INV_SCRAP_CONV_DET ID, " & vbCrLf & " INV_ITEM_MST INVMST_H, INV_ITEM_MST INVMST_D"

        ''WHERE CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.AUTO_KEY_SCRAP=ID.AUTO_KEY_SCRAP" & vbCrLf & " AND IH.COMPANY_CODE=INVMST_H.COMPANY_CODE" & vbCrLf & " AND IH.ITEM_CODE=INVMST_H.ITEM_CODE" & vbCrLf & " AND ID.COMPANY_CODE=INVMST_D.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=INVMST_D.ITEM_CODE" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_SCRAP=" & Val(txtNumber.Text) & ""

        ''ORDER CLAUSE...

        mSqlStr = mSqlStr & vbCrLf & "ORDER BY ID.SERIAL_NO"

        SelectQryForScrap = mSqlStr
    End Function

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        Call CalcTots()

        If UpdateMain1 = True Then
            ADDMode = False
            MODIFYMode = False
            txtNumber_Validating(txtNumber, New System.ComponentModel.CancelEventArgs(False))
            If CmdAdd.Enabled = True Then CmdAdd.Focus()
        Else
            MsgInformation("Record not saved")
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        If Err.Description = "" Then Exit Sub
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub

    Private Sub cmdItemSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdItemSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster("", "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", "ISSUE_UOM", , SqlStr) = True Then
            txtItem.Text = AcName1
            lblItemDesc.Text = AcName
            TxtItem_Validating(TxtItem, New System.ComponentModel.CancelEventArgs(False))
            If txtItem.Enabled = True Then txtItem.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdEmpSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdEmpSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""


        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            txtEmp.Text = AcName1
            lblEmpName.Text = AcName
            txtEmp_Validating(txtEmp, New System.ComponentModel.CancelEventArgs(False))
            If txtEmp.Enabled = True Then txtEmp.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmScrapConv_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        On Error GoTo ErrPart
        Dim xIName As String
        Dim xSupp As String
        Dim SqlStr As String = ""

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If

        If eventArgs.row = 0 And eventArgs.col = ColItemCode Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemCode
                If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SCRAP_ITEM_CODE='" & Trim(txtItem.Text) & "' ") = True Then
                    .Row = .ActiveRow
                    .Col = ColItemCode
                    .Text = AcName
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColItemDesc Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemDesc
                xIName = .Text
                .Text = ""
                If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SCRAP_ITEM_CODE='" & Trim(txtItem.Text) & "' ") = True Then
                    .Row = .ActiveRow
                    .Col = ColItemDesc
                    .Text = AcName
                Else
                    .Row = .ActiveRow
                    .Col = ColItemDesc
                    .Text = xIName
                End If
                MainClass.ValidateWithMasterTable(.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                .Row = .ActiveRow
                .Col = ColItemCode
                .Text = MasterNo
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            End With
        End If

        If eventArgs.col = 0 And eventArgs.row > 0 Then
            SprdMain.Row = eventArgs.row
            SprdMain.Col = ColItemCode
            If eventArgs.row < SprdMain.MaxRows And (ADDMode = True Or MODIFYMode = True) Then
                MainClass.DeleteSprdRow(SprdMain, eventArgs.row, ColItemCode)
                MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
                FormatSprdMain(eventArgs.row)
            End If
        End If
        Call CalcTots()
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdMain_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdMain.KeyDownEvent

        Dim mActiveCol As Integer

        mActiveCol = SprdMain.ActiveCol

        If eventArgs.KeyCode = System.Windows.Forms.Keys.Return Or eventArgs.KeyCode = System.Windows.Forms.Keys.Tab Then
            If mActiveCol = ColScrapQty Then
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColScrapQty
                If Val(SprdMain.Text) <> 0 Then
                    If SprdMain.MaxRows = SprdMain.ActiveRow Then
                        MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                        '                    FormatSprdMain SprdMain.MaxRows
                    End If
                End If
                '            SprdMain.Row = SprdMain.MaxRows
            End If
        ElseIf eventArgs.KeyCode = System.Windows.Forms.Keys.F1 Then
            If mActiveCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
            If mActiveCol = ColItemDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemDesc, 0))
        End If
        eventArgs.KeyCode = 9999
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim mItemCode As String
        Dim mUOM As String = ""
        Dim mStockQty As Double
        Dim mScrapQty As Double
        Dim mDivisionCode As Double

        If eventArgs.NewRow = -1 Then Exit Sub

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        SprdMain.Row = SprdMain.ActiveRow

        Select Case eventArgs.Col
            Case ColItemCode
                SprdMain.Col = ColItemCode
                Call FillItemDescFromItemCode((SprdMain.Text))
                If DuplicateItem = False Then
                    FormatSprdMain(-1)
                End If
                SprdMain.Row = SprdMain.ActiveRow
                mItemCode = Trim(SprdMain.Text)

                SprdMain.Col = ColUom
                mUOM = Trim(SprdMain.Text)
                '
                '            SprdMain.Col = ColScrapQty
                '            mScrapQty = Val(SprdMain.Text)
                '
                SprdMain.Col = ColStockQty
                SprdMain.Text = CStr(GetBalanceStockQty(mItemCode, (txtDate.Text), mUOM, "STR", "SC", "", ConWH, mDivisionCode, ConStockRefType_SCP, Val(txtNumber.Text)))

            Case ColItemDesc
                SprdMain.Col = ColItemCode
                Call FillItemDescFromItemDesc((SprdMain.Text))
                If DuplicateItem = False Then
                End If

            Case ColScrapQty
                If CheckQty() = True Then
                    SprdMain.Row = SprdMain.ActiveRow
                    SprdMain.Col = ColStockQty
                    mStockQty = Val(SprdMain.Text)



                    SprdMain.Col = ColScrapQty
                    If Val(SprdMain.Text) <> 0 Then
                        '                    If RsCompany!StockBalCheck = "Y" Then
                        If mStockQty < Val(SprdMain.Text) Then
                            MsgInformation("You have not enough Stock.")
                            MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColScrapQty)
                        End If
                        '                    End If
                    End If
                End If
        End Select

        Call CalcTots()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub CalcTots()
        On Error GoTo ERR1

        Dim CntRow As Integer

        Dim mQty As Double
        Dim mWt As Double

        Dim mTotWt As Double
        Dim mTotQty As Double

        Dim mItemWt As Double
        Dim mScrapWt As Double

        With SprdMain
            For CntRow = 1 To .MaxRows
                .Row = CntRow

                .Col = ColItemCode
                If .Text = "" Then GoTo DontCalc

                .Col = ColScrapQty
                mQty = Val(.Text)
                mTotQty = mTotQty + mQty

                .Col = ColItemWt
                mItemWt = Val(.Text)

                .Col = ColScrapWt
                mWt = CDbl(VB6.Format(Val(CStr(mQty * mItemWt)), "0.000")) ''Val(.Text)
                mTotWt = mTotWt + mWt
DontCalc:
            Next CntRow
        End With

        lblScrapQty.Text = VB6.Format(mTotQty, "0.000")
        lblScrapWt.Text = VB6.Format(mTotWt, "0.000")

        Exit Sub
ERR1:
        'Resume
        If Err.Number = 6 Then Resume Next 'OverFlow
        MsgInformation(Err.Description)
    End Sub
    Private Function DuplicateItem() As Boolean

        Dim CntRow As Integer
        Dim mCount As Byte
        Dim mCheckItemCode As String
        Dim mItemCode As String

        With SprdMain
            .Row = .ActiveRow
            .Col = ColItemCode
            mCheckItemCode = Trim(UCase(.Text))

            For CntRow = 1 To .MaxRows
                .Row = CntRow
                .Col = ColItemCode
                mItemCode = Trim(UCase(.Text))

                If (mItemCode = mCheckItemCode And mCheckItemCode <> "") Then
                    mCount = mCount + 1
                End If

                If mCount > 1 Then
                    DuplicateItem = True
                    MsgInformation("Duplicate Item : " & mCheckItemCode)
                    MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColItemCode)
                    Exit Function
                End If
            Next
        End With
    End Function

    Private Function CheckQty() As Boolean
        On Error GoTo ERR1
        Dim mItemCode As String
        Dim mScrapQty As Double
        Dim mScrapWt As Double
        Dim mUOM As String = ""



        With SprdMain
            .Row = .ActiveRow
            .Col = ColItemCode
            If Trim(.Text) = "" Then Exit Function
            mItemCode = Trim(.Text)

            If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mUOM = MasterNo
            End If

            .Row = .ActiveRow
            .Col = ColScrapQty
            mScrapQty = Val(.Text)

            .Col = ColScrapWt
            .Text = CStr(0)

            If mScrapQty > 0 Then
                If Trim(UCase(mUOM)) = Trim(lblItemUom.Text) Then
                    .Col = ColScrapWt
                    .Text = VB6.Format(Val(CStr(mScrapQty)), "0.000")
                ElseIf Trim(UCase(mUOM)) = "KGS" Then
                    .Col = ColScrapWt
                    .Text = VB6.Format(Val(CStr(mScrapQty)), "0.000")
                ElseIf Trim(UCase(mUOM)) = "TON" Or Trim(UCase(mUOM)) = "MT" Then
                    .Col = ColScrapWt
                    .Text = VB6.Format(Val(CStr(mScrapQty)) * 1000, "0.000")
                Else
                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_WEIGHT", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mScrapWt = MasterNo

                        mScrapWt = CDbl(VB6.Format(Val(CStr(mScrapWt)) / 1000, "0.000"))
                        .Col = ColItemWt
                        .Text = VB6.Format(mScrapWt, "0.000")

                        .Col = ColScrapWt
                        .Text = VB6.Format(Val(CStr(mScrapQty * mScrapWt)), "0.000")
                    End If
                End If
            Else
                '            MainClass.SetFocusToCell SprdMain, .ActiveRow, ColScrapQty
            End If
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Sub FillItemDescFromItemCode(ByRef pItemCode As String)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        If Trim(pItemCode) = "" Then Exit Sub
        With SprdMain
            SqlStr = "SELECT ITEM_SHORT_DESC,ISSUE_UOM,ITEM_WEIGHT " & vbCrLf & " FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND LTRIM(RTRIM(ITEM_CODE))='" & MainClass.AllowSingleQuote(pItemCode) & "'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                .Row = .ActiveRow
                .Col = ColItemDesc
                .Text = IIf(IsDbNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value)

                .Col = ColItemWt
                .Text = VB6.Format(Val(IIf(IsDbNull(RsTemp.Fields("ITEM_WEIGHT").Value), 0, RsTemp.Fields("ITEM_WEIGHT").Value)) / 1000, "0.000")

                .Col = ColUom
                .Text = IIf(IsDbNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)

            Else
                MsgInformation("Invaild Item Code")
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColItemCode)
            End If
        End With

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub FillItemDescFromItemDesc(ByRef pItemDesc As String)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        If Trim(pItemDesc) = "" Then Exit Sub
        With SprdMain
            SqlStr = "SELECT ITEM_CODE,CUSTOMER_PART_NO,ISSUE_UOM,ITEM_WEIGHT " & vbCrLf & " FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND LTRIM(RTRIM(ITEM_SHORT_DESC))='" & MainClass.AllowSingleQuote(pItemDesc) & "'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                .Row = .ActiveRow
                .Col = ColItemCode
                .Text = IIf(IsDbNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)

                .Col = ColItemWt
                .Text = VB6.Format(Val(IIf(IsDbNull(RsTemp.Fields("ITEM_WEIGHT").Value), 0, RsTemp.Fields("ITEM_WEIGHT").Value)) / 1000, "0.000")

                .Col = ColUom
                .Text = IIf(IsDbNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)
            Else
                MsgInformation("Invaild Item Description")
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColItemCode)
            End If
        End With
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub SprdView_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdView.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        With SprdView
            .Row = eventArgs.row
            .Col = 1
            txtNumber.Text = .Text
            txtNumber_Validating(txtNumber, New System.ComponentModel.CancelEventArgs(False))
            If txtNumber.Enabled = True Then txtNumber.Focus()
            CmdView_Click(CmdView, New System.EventArgs())
        End With
    End Sub

    Private Function AutoGenSeqNo() As String

        On Error GoTo AutoGenSeqNoErr
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mNewSeqNo As Integer
        Dim SqlStr As String = ""
        Dim mMaxValue As String

        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_SCRAP)  " & vbCrLf & " FROM INV_SCRAP_CONV_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_SCRAP,LENGTH(AUTO_KEY_SCRAP)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With RsTemp
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mMaxValue = .Fields(0).Value
                    mNewSeqNo = CInt(Mid(mMaxValue, 1, Len(mMaxValue) - 6))
                    mNewSeqNo = mNewSeqNo + 1
                Else
                    mNewSeqNo = 1
                End If
            End If
        End With
        AutoGenSeqNo = mNewSeqNo & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        Exit Function
AutoGenSeqNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function UpdateMain1() As Boolean
        On Error GoTo ErrPart
        Dim mReqnum As String = ""
        Dim SqlStr As String = ""
        Dim mVNoSeq As Double
        Dim mStatus As String
        Dim mDivisionCode As Double



        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If Val(txtNumber.Text) = 0 Then
            mVNoSeq = CDbl(AutoGenSeqNo())
        Else
            mVNoSeq = Val(txtNumber.Text)
        End If

        txtNumber.Text = CStr(Val(CStr(mVNoSeq)))

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        SqlStr = ""
        If ADDMode = True Then
            lblMKey.Text = CStr(mVNoSeq)
            SqlStr = "INSERT INTO INV_SCRAP_CONV_HDR (" & vbCrLf & " AUTO_KEY_SCRAP, " & vbCrLf & " COMPANY_CODE, " & vbCrLf & " SCRAP_DATE, " & vbCrLf & " ITEM_CODE, " & vbCrLf & " EMP_CODE, " & vbCrLf & " ADDUSER,ADDDATE,MODUSER,MODDATE, DIV_CODE)" & vbCrLf & " VALUES( " & vbCrLf & " " & Val(CStr(mVNoSeq)) & "," & vbCrLf & " " & RsCompany.Fields("Company_Code").Value & "," & vbCrLf & " TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & MainClass.AllowSingleQuote((txtItem.Text)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtEmp.Text)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'',''," & mDivisionCode & ")"
        ElseIf MODIFYMode = True Then
            SqlStr = ""
            SqlStr = "UPDATE INV_SCRAP_CONV_HDR SET " & vbCrLf & " EMP_CODE ='" & Trim(txtEmp.Text) & "', DIV_CODE=" & mDivisionCode & ", " & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND AUTO_KEY_SCRAP =" & Val(lblMKey.Text) & ""
        End If

        PubDBCn.Execute(SqlStr)

        If UpdateDetail1(CStr(Val(CStr(mVNoSeq))), mDivisionCode) = False Then GoTo ErrPart

        UpdateMain1 = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        UpdateMain1 = False
        PubDBCn.RollbackTrans() ''
        RsScrapHdr.Requery() ''.Refresh
        RsScrapDet.Requery() ''.Refresh
        If Err.Description = "" Then Exit Function
        If Err.Number = -2147217900 Then
            ErrorMsg("Duplicate Scrap Convertion Generated, Save Again", "Duplicate", MsgBoxStyle.Critical)
        Else
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        ''Resume
    End Function

    Private Function UpdateDetail1(ByRef pVnoseq As String, ByRef mDivisionCode As Double) As Boolean

        On Error GoTo UpdateDetail1
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim mItemCode As String
        Dim mUOM As String = ""
        Dim mScrapQty As Double
        Dim mScrapWt As Double
        Dim mRemarks As String


        SqlStr = " Delete From INV_SCRAP_CONV_DET " & vbCrLf & " WHERE AUTO_KEY_SCRAP=" & Val(lblMKey.Text) & ""
        PubDBCn.Execute(SqlStr)

        PubDBCn.Execute("Delete From FIN_RGDAILYMANU_HDR Where Mkey='" & Val(lblMKey.Text) & "' AND BOOKTYPE='C'")

        If DeleteStockTRN(PubDBCn, ConStockRefType_SCP, (txtNumber.Text)) = False Then GoTo UpdateDetail1

        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = ColItemCode
                mItemCode = Trim(MainClass.AllowSingleQuote(.Text))

                .Col = ColUom
                mUOM = MainClass.AllowSingleQuote(.Text)

                .Col = ColScrapQty
                mScrapQty = Val(.Text)

                .Col = ColScrapWt
                mScrapWt = Val(.Text)

                .Col = ColRemarks
                mRemarks = MainClass.AllowSingleQuote(.Text)

                SqlStr = ""

                If mItemCode <> "" And mScrapQty > 0 And mScrapWt > 0 Then
                    SqlStr = " INSERT INTO INV_SCRAP_CONV_DET ( COMPANY_CODE, " & vbCrLf & " AUTO_KEY_SCRAP,SERIAL_NO,ITEM_CODE,ITEM_UOM," & vbCrLf & " SCRAP_QTY, SCRAP_WT, REMARKS) "
                    SqlStr = SqlStr & vbCrLf & " VALUES (" & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " " & Val(lblMKey.Text) & ", " & I & "," & vbCrLf & " '" & MainClass.AllowSingleQuote(mItemCode) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mUOM) & "', " & vbCrLf & " " & mScrapQty & ", " & mScrapWt & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(mRemarks) & "') "

                    PubDBCn.Execute(SqlStr)



                    If UpdateStockTRN(PubDBCn, ConStockRefType_SCP, (txtNumber.Text), I, (txtDate.Text), (txtDate.Text), "SC", mItemCode, mUOM, CStr(-1), mScrapQty, 0, "O", 0, 0, "", "", "STR", "STR", "", "N", "SCRAP CONVERTED TO ITEM CODE : " & txtItem.Text, "", ConWH, mDivisionCode, "", Trim(txtItem.Text)) = False Then GoTo UpdateDetail1

                    If UpdateStockTRN(PubDBCn, ConStockRefType_SCP, (txtNumber.Text), I, (txtDate.Text), (txtDate.Text), "SC", (txtItem.Text), (lblItemUom.Text), CStr(-1), mScrapWt, 0, "I", 0, 0, "", "", "STR", "STR", "", "N", "SCRAP CONVERTED FROM ITEM CODE : " & mItemCode, "", ConWH, mDivisionCode, "", "") = False Then GoTo UpdateDetail1

                End If
            Next

            ''Temp Mark.....
            If UpdateProductionData(Trim(txtItem.Text), Val(lblScrapWt.Text)) = False Then GoTo UpdateDetail1

        End With
        UpdateDetail1 = True
        Exit Function
UpdateDetail1:
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function

    Private Function UpdateProductionData(ByRef mItemCode As String, ByRef mItemQty As Double) As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mTariff As String
        Dim mEntryTime As String

        If mItemQty <= 0 Then UpdateProductionData = True : Exit Function

        If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "TARIFF_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mTariff = Trim(MasterNo)
        Else
            mTariff = "-1"
        End If

        mEntryTime = GetServerTime

        SqlStr = " INSERT INTO FIN_RGDAILYMANU_HDR ( " & vbCrLf & " MKEY , COMPANY_CODE, FYEAR, BOOKTYPE, " & vbCrLf & " BILLNO , INV_PREP_TM, MDATE, " & vbCrLf & " ITEM_CODE, ITEM_QTY, " & vbCrLf & " TARIFF_CODE, UPDATEFLAG) "

        SqlStr = SqlStr & vbCrLf & " VALUES ( " & vbCrLf & " '" & lblMKey.Text & "'," & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & ", 'C', " & vbCrLf & " '" & lblMKey.Text & "', TO_DATE('" & mEntryTime & "','HH24:MI'), TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & mItemCode & "'," & mItemQty & ",'" & mTariff & "','Y' ) "
        PubDBCn.Execute(SqlStr)
        ''TO_DATE('" & txtPMemoDate.Text & "','HH24:MI')
        UpdateProductionData = True
        Exit Function
ErrPart:
        UpdateProductionData = False
        If Err.Description <> "" Then MsgBox(Err.Description)
        'Resume
    End Function
    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim mRow As Integer
        Dim mSTTaxcount As Integer
        Dim mCheckLastEntryDate As String

        FieldsVarification = True
        If ValidateBranchLocking((txtDate.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsScrapHdr.EOF = True Then Exit Function

        If MODIFYMode = True And txtNumber.Text = "" Then
            MsgInformation("No. can not be Blank")
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtDate.Text) = "" Then
            MsgBox("Date is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtDate.Focus()
            Exit Function
        ElseIf FYChk((txtDate.Text)) = False Then
            FieldsVarification = False
            If txtDate.Enabled = True Then txtDate.Focus()
            Exit Function
        End If

        If Trim(txtEmp.Text) = "" Then
            MsgBox("Emp Code is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtEmp.Focus()
            Exit Function
        End If

        If Trim(cboDivision.Text) = "" Then
            MsgBox("Division Name is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If cboDivision.Enabled = True Then cboDivision.Focus()
            Exit Function
        End If

        If CheckStockQty(SprdMain, ColStockQty, ColScrapQty, ColItemCode, -1, True) = False Then
            FieldsVarification = False
            Exit Function
        End If

        If PubSuperUser <> "S" Then
            mCheckLastEntryDate = GetLastEntryDate()
            mCheckLastEntryDate = DateAdd("d", -1, mCheckLastEntryDate)
            If mCheckLastEntryDate <> "" Then
                If CDate(txtDate.Text) < CDate(mCheckLastEntryDate) Then
                    MsgBox("Cann't be Add or Modify Back Entry", MsgBoxStyle.Information)
                    FieldsVarification = False
                    Exit Function
                End If
            End If
        End If

        If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "ItemCode Is Blank.") = False Then FieldsVarification = False : Exit Function
        '    If MainClass.ValidDataInGrid(SprdMain, ColScrapQty, "N", "Please Check Quantity.") = False Then FieldsVarification = False: Exit Function
        '    If MainClass.ValidDataInGrid(SprdMain, ColScrapWt, "N", "Please Check Weight.") = False Then FieldsVarification = False: Exit Function

        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Function

    Private Function GetLastEntryDate() As String

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        GetLastEntryDate = ""
        SqlStr = ""
        SqlStr = "SELECT Max(SCRAP_DATE) AS  SCRAP_DATE " & vbCrLf & " FROM INV_SCRAP_CONV_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_SCRAP,LENGTH(AUTO_KEY_SCRAP)-5,4)=" & RsCompany.Fields("FYEAR").Value & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            GetLastEntryDate = IIf(IsDbNull(RsTemp.Fields("SCRAP_DATE").Value), "", RsTemp.Fields("SCRAP_DATE").Value)
        End If

        Exit Function
ErrPart:

    End Function
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub

    Public Sub frmScrapConv_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = ""
        SqlStr = "Select * from INV_SCRAP_CONV_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsScrapHdr, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from INV_SCRAP_CONV_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsScrapDet, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)
        '    Call SetTextLengths

        If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ''Resume
    End Sub

    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        On Error GoTo AssignGridErr
        Dim SqlStr As String = ""

        SqlStr = ""

        ''SELECT CLAUSE...

        SqlStr = "SELECT  AUTO_KEY_SCRAP AS SCRAP_NO, SCRAP_DATE, ITEM_CODE, EMP_CODE "

        ''FROM CLAUSE...

        SqlStr = SqlStr & vbCrLf & " FROM INV_SCRAP_CONV_HDR "

        ''WHERE CLAUSE...

        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & vbCrLf & " and SUBSTR(AUTO_KEY_SCRAP,LENGTH(AUTO_KEY_SCRAP)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        ''ORDER BY CLAUSE...

        SqlStr = SqlStr & vbCrLf & " Order by AUTO_KEY_SCRAP"

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
AssignGridErr:
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub

    Private Sub FormatSprdView()

        With SprdView
            .Row = -1

            .set_RowHeight(0, 400)

            .set_ColWidth(0, 600)
            .set_ColWidth(1, 1500)
            .set_ColWidth(2, 1500)

            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            SprdView.set_RowHeight(-1, 300)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub

    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        With SprdMain
            .set_RowHeight(0, ConRowHeight * 2.5)
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight * 1.5)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsScrapDet.Fields("ITEM_CODE").DefinedSize ''
            .set_ColWidth(ColItemCode, 8)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn)
            .set_ColWidth(ColItemDesc, 25)

            .Col = ColUom
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsScrapDet.Fields("ITEM_UOM").DefinedSize ''
            .set_ColWidth(ColUom, 4)

            .Col = ColStockQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColStockQty, 10)

            .Col = ColItemWt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColItemWt, 8)

            .Col = ColScrapQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColScrapQty, 8)

            .Col = ColScrapWt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColScrapWt, 10)

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = MainClass.SetMaxLength("REMARKS", "INV_SCRAP_CONV_DET", PubDBCn)
            .set_ColWidth(ColRemarks, 15)

        End With
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemDesc, ColStockQty)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColScrapWt, ColScrapWt)
        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then RsScrapDet.Requery() : Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SetTextLengths()
        On Error GoTo ERR1
        With RsScrapHdr
            txtDate.Maxlength = 10
            txtNumber.Maxlength = .Fields("AUTO_KEY_SCRAP").Precision
            txtItem.Maxlength = .Fields("ITEM_CODE").DefinedSize
            txtEmp.Maxlength = .Fields("EMP_CODE").DefinedSize
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub Show1()

        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim mDivisionCode As Double
        Dim mDivisionDesc As String

        With RsScrapHdr
            If Not .EOF Then
                txtNumber.Enabled = False
                lblMKey.Text = .Fields("AUTO_KEY_SCRAP").Value

                txtNumber.Text = IIf(IsDbNull(.Fields("AUTO_KEY_SCRAP").Value), 0, .Fields("AUTO_KEY_SCRAP").Value)
                txtDate.Text = VB6.Format(IIf(IsDbNull(.Fields("SCRAP_DATE").Value), "", .Fields("SCRAP_DATE").Value), "DD/MM/YYYY")
                txtItem.Text = IIf(IsDbNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)
                txtEmp.Text = IIf(IsDbNull(.Fields("EMP_CODE").Value), "", .Fields("EMP_CODE").Value)

                If MainClass.ValidateWithMasterTable((txtItem.Text), "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    lblItemDesc.Text = MasterNo
                    If MainClass.ValidateWithMasterTable((txtItem.Text), "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        lblItemUom.Text = MasterNo
                    End If
                End If

                If MainClass.ValidateWithMasterTable((txtEmp.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    lblEmpName.Text = MasterNo
                End If

                mDivisionCode = IIf(IsDbNull(.Fields("DIV_CODE").Value), -1, .Fields("DIV_CODE").Value)

                If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivisionDesc = Trim(MasterNo)
                    cboDivision.Text = mDivisionDesc
                End If


                Call ShowDetail1(.Fields("AUTO_KEY_SCRAP").Value, mDivisionCode)
                Call CalcTots()
            End If
        End With
        ADDMode = False
        MODIFYMode = False
        cmdPopulate.Enabled = False
        MainClass.ButtonStatus(Me, XRIGHT, RsScrapHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)

        '    FormatSprdMain -1

        SprdMain.Enabled = True    '' False Sandeep 15/05/2022
        txtNumber.Enabled = True
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub

    Private Sub ShowDetail1(ByVal pReqNum As Double, ByRef mDivisionCode As Double)

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mUOM As String = ""
        Dim mScrapQty As Double
        Dim mItemWt As Double

        MainClass.ClearGrid(SprdMain)

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM INV_SCRAP_CONV_DET  " & vbCrLf & " Where AUTO_KEY_SCRAP = " & Val(CStr(pReqNum)) & "" & vbCrLf & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsScrapDet, ADODB.LockTypeEnum.adLockReadOnly)
        With RsScrapDet
            If .EOF = True Then Exit Sub
            '        FormatSprdMain -1
            I = 1
            .MoveFirst()

            Do While Not .EOF

                SprdMain.Row = I

                SprdMain.Col = ColItemCode
                mItemCode = IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)
                SprdMain.Text = mItemCode

                SprdMain.Col = ColItemDesc
                MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mItemDesc = MasterNo
                SprdMain.Text = mItemDesc

                SprdMain.Col = ColUom
                SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)
                mUOM = Trim(SprdMain.Text)
                mItemWt = 0
                If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_WEIGHT", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mItemWt = MasterNo
                End If

                SprdMain.Col = ColItemWt
                SprdMain.Text = VB6.Format(Val(CStr(mItemWt)) / 1000, "0.000")



                SprdMain.Col = ColScrapQty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("SCRAP_QTY").Value), "", .Fields("SCRAP_QTY").Value)))
                mScrapQty = Val(SprdMain.Text)

                SprdMain.Col = ColStockQty
                SprdMain.Text = CStr(mScrapQty + GetBalanceStockQty(mItemCode, (txtDate.Text), mUOM, "STR", "SC", "", ConWH, mDivisionCode))

                SprdMain.Col = ColScrapWt
                SprdMain.Text = VB6.Format(Val(CStr(mItemWt)) * mScrapQty * 0.001, "0.000") '' Val(IIf(IsNull(.Fields("SCRAP_WT").Value), "", .Fields("SCRAP_WT").Value))

                SprdMain.Col = ColRemarks
                SprdMain.Text = IIf(IsDBNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)

                .MoveNext()

                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With
        FormatSprdMain(-1)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume
    End Sub

    Private Sub ViewGrid()

        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        AdoDCMain.Refresh
            FormatSprdView()
            SprdView.Focus()
            FraFront.SendToBack()
        Else
            CmdView.Text = ConCmdGridViewCaption
            FraFront.BringToFront()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsScrapHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub Clear1()


        lblMKey.Text = ""

        txtDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtNumber.Text = ""
        txtItem.Text = ""
        txtEmp.Text = ""

        lblItemDesc.Text = ""
        lblItemUom.Text = ""
        lblEmpName.Text = ""

        cboDivision.Text = GetDefaultDivision()         ''cboDivision.SelectedIndex = -1
        cboDivision.Enabled = True

        lblScrapQty.Text = "0.000"
        lblScrapWt.Text = "0.000"
        cmdPopulate.Enabled = True
        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsScrapHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub frmScrapConv_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmScrapConv_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        '    MainClass.DoFunctionKey Me, KeyCode
    End Sub

    Public Sub frmScrapConv_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        xMyMenu = myMenu
        MainClass.RightsToButton(Me, XRIGHT)

        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        ''Me.Width = VB6.TwipsToPixelsX(11355)



        cboDivision.Items.Clear()

        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DIV_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDivision.Items.Add(RS.Fields("DIV_DESC").Value)
                RS.MoveNext()
            Loop
        End If

        cboDivision.SelectedIndex = -1

        'AdoDCMain.Visible = False
        txtNumber.Enabled = True
        FormActive = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMain.ActiveCol

        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemDesc, 0))
        SprdMain.Refresh()
    End Sub

    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        With SprdMain
            'SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub

    Private Sub txtDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtDate.Text) Then
            MsgInformation("Invalid Date")
            Cancel = True
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtItem_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItem.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtItem_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItem.DoubleClick
        Call cmdItemSearch_Click(cmdItemSearch, New System.EventArgs())
    End Sub

    Private Sub txtItem_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItem.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtItem.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub TxtItem_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtItem.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then TxtItem_DoubleClick(TxtItem, New System.EventArgs())
    End Sub

    Private Sub TxtItem_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItem.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If txtItem.Text = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable((txtItem.Text), "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            lblItemDesc.Text = MasterNo
            If MainClass.ValidateWithMasterTable((txtItem.Text), "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblItemUom.Text = MasterNo
            End If
        Else
            MsgInformation("Invalid Item Code")
            Cancel = True
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtEmp_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmp.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtEmp_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmp.DoubleClick
        Call cmdEmpSearch_Click(cmdEmpSearch, New System.EventArgs())
    End Sub

    Private Sub txtEmp_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEmp.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtEmp.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEmp_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtEmp.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtEmp_DoubleClick(txtEmp, New System.EventArgs())
    End Sub

    Private Sub txtEmp_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmp.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If txtEmp.Text = "" Then GoTo EventExitSub
        txtEmp.Text = VB6.Format(txtEmp.Text, "000000")
        If MainClass.ValidateWithMasterTable((txtEmp.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            lblEmpName.Text = MasterNo
        Else
            MsgInformation("Invalid Employee Code")
            Cancel = True
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtNumber_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNumber.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtNumber_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNumber.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtNumber.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtNumber_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtNumber.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mReqnum As String = ""

        If Trim(txtNumber.Text) = "" Then GoTo EventExitSub

        If Len(txtNumber.Text) < 6 Then
            txtNumber.Text = Trim(txtNumber.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        If MODIFYMode = True And RsScrapHdr.EOF = False Then mReqnum = RsScrapHdr.Fields("AUTO_KEY_SCRAP").Value

        SqlStr = "Select * From INV_SCRAP_CONV_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUBSTR(AUTO_KEY_SCRAP,LENGTH(AUTO_KEY_SCRAP)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_SCRAP=" & Val(txtNumber.Text) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsScrapHdr, ADODB.LockTypeEnum.adLockReadOnly)

        If RsScrapHdr.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Number, Click add to Generate Such Number", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "Select * From INV_SCRAP_CONV_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUBSTR(AUTO_KEY_SCRAP,LENGTH(AUTO_KEY_SCRAP)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AUTO_KEY_SCRAP=" & Val(mReqnum) & ""

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsScrapHdr, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
