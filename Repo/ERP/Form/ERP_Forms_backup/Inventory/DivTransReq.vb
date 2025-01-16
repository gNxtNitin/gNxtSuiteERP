Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class FrmDivTransReq
    Inherits System.Windows.Forms.Form
    Dim RsReqMain As ADODB.Recordset ''Recordset
    Dim RsReqDetail As ADODB.Recordset ''Recordset
    'Private PvtDBCn As ADODB.Connection

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Dim FormActive As Boolean

    Private Const ConRowHeight As Short = 12
    Dim xMyMenu As String
    Dim mcntRow As Integer

    Private Const ColItemCode As Short = 1
    Private Const ColItemDesc As Short = 2
    Private Const ColUom As Short = 3
    Private Const ColStockType As Short = 4
    Private Const ColLotNo As Short = 5
    Private Const ColStockQty As Short = 6
    Private Const ColDemandQty As Short = 7
    Private Const ColIssueQty As Short = 8
    Private Const ColIssuedQty As Short = 9
    Private Const ColBalQty As Short = 10
    Private Const ColRate As Short = 11
    Private Const ColRemarks As Short = 12

    Dim pDataShow As Boolean

    Private Sub cboDivisionFrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivisionFrom.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboDivisionFrom_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivisionFrom.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboDivisionTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivisionTo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboDivisionTo_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivisionTo.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub cboShiftcd_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShiftcd.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboShiftcd_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShiftcd.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkissue_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkIssue.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click

        On Error GoTo AddErr
        If cmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            SprdMain.Enabled = True
            txtReqNo.Enabled = False
            cmdSearch.Enabled = False

        Else
            cmdAdd.Text = ConCmdAddCaption
            ADDMode = False
            MODIFYMode = False
            MainClass.ClearGrid(SprdMain)
            Call FormatSprdMain(-1)
            Clear1()
            Show1()
        End If
        Exit Sub
AddErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub FillCboFormType()

        On Error GoTo FillERR
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing

        cboDivisionFrom.Items.Clear()

        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DIV_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDivisionFrom.Items.Add(RS.Fields("DIV_DESC").Value)
                RS.MoveNext()
            Loop
        End If

        cboDivisionFrom.SelectedIndex = -1

        cboDivisionTo.Items.Clear()

        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DIV_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDivisionTo.Items.Add(RS.Fields("DIV_DESC").Value)
                RS.MoveNext()
            Loop
        End If

        cboDivisionTo.SelectedIndex = -1


        cboShiftcd.Items.Clear()
        cboShiftcd.Items.Add(("A"))
        cboShiftcd.Items.Add(("B"))
        cboShiftcd.Items.Add(("C"))


        cboShiftcd.SelectedIndex = -1

        Exit Sub
FillERR:
        MsgBox(Err.Description)
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDelete.Click

        On Error GoTo DelErrPart

        Dim mItemCode As String
        Dim mLockBookCode As Integer

        If ValidateBranchLocking((txtReqDate.Text)) = True Then
            Exit Sub
        End If

        If lblBookType.Text = "R" Then
            mLockBookCode = CInt(ConLockStoreReq)
        Else
            mLockBookCode = CInt(ConLockIssueNote)
        End If

        If ValidateBookLocking(PubDBCn, mLockBookCode, txtReqDate.Text) = True Then
            Exit Sub
        End If

        If Trim(txtReqNo.Text) = "" Then MsgInformation("Nothing to Delete") : Exit Sub

        If PubUserID <> "G0416" Then
            If chkIssue.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgInformation("Issue Completed, Cann't be Deleted")
                Exit Sub
            End If
        End If

        If Not RsReqMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "INV_DIVTRANS_HDR", (txtReqNo.Text), RsReqMain, "AUTO_KEY_TRANS") = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "INV_DIVTRANS_HDR", "AUTO_KEY_TRANS", (txtReqNo.Text)) = False Then GoTo DelErrPart

                If DeleteStockTRN(PubDBCn, ConStockRefType_DTN, (txtReqNo.Text)) = False Then GoTo DelErrPart

                If lblBookType.Text = "I" Then
                    PubDBCn.Execute("UPDATE INV_DIVTRANS_DET SET ISSUE_QTY=0 Where AUTO_KEY_TRANS=" & Val(txtReqNo.Text) & "")
                    PubDBCn.Execute("UPDATE INV_DIVTRANS_HDR SET ISSUE_STATUS='N',ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "', Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') Where AUTO_KEY_TRANS=" & Val(txtReqNo.Text) & "")
                Else
                    PubDBCn.Execute("Delete from INV_DIVTRANS_DET Where AUTO_KEY_TRANS=" & Val(txtReqNo.Text) & "")
                    PubDBCn.Execute("Delete from INV_DIVTRANS_HDR Where AUTO_KEY_TRANS=" & Val(txtReqNo.Text) & "")
                End If

                PubDBCn.CommitTrans()
                RsReqMain.Requery() ''.Refresh
                RsReqDetail.Requery() ''.Refresh
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans() ''
        RsReqMain.Requery() ''.Refresh
        RsReqDetail.Requery() ''.Refresh
        If Err.Description <> "" Then
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
    End Sub
    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdModify.Click

        On Error GoTo ModifyErr

        If PubSuperUser = "U" Then
            If chkIssue.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgInformation("Issue Completed, Cann't be Modified")
                Exit Sub
            End If
        End If

        If cmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsReqMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True
            txtReqNo.Enabled = False
            cmdSearch.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        End If
        Exit Sub
ModifyErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnMatReq(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnMatReq(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportOnMatReq(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mTitle As String = ""

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        If lblBookType.Text = "I" Then
            mTitle = "Store Division Transfer Issue Note"
        Else
            mTitle = "Store Division Transfer Requisition Note"
        End If

        SqlStr = " SELECT " & vbCrLf & " IH.AUTO_KEY_TRANS, IH.TRANS_DATE, " & vbCrLf & " ID.SERIAL_NO, ID.ITEM_CODE, ID.ITEM_UOM, ID.ISSUE_QTY, ID.STOCK_TYPE, ID.DEMAND_QTY, " & vbCrLf & " EMP.EMP_NAME,IDA.DIV_DESC,IDB.DIV_DESC, IMST.ITEM_SHORT_DESC " & vbCrLf & " FROM " & vbCrLf & " INV_DIVTRANS_HDR IH, " & vbCrLf & " INV_DIVTRANS_DET ID, " & vbCrLf & " PAY_EMPLOYEE_MST EMP, " & vbCrLf & " INV_DIVISION_MST IDA, " & vbCrLf & " INV_DIVISION_MST IDB, " & vbCrLf & " INV_ITEM_MST IMST " & vbCrLf & " WHERE " & vbCrLf & " IH.AUTO_KEY_TRANS = ID.AUTO_KEY_TRANS AND " & vbCrLf & " IH.COMPANY_CODE = EMP.COMPANY_CODE AND " & vbCrLf & " IH.EMP_CODE = EMP.EMP_CODE AND " & vbCrLf & " IH.COMPANY_CODE = IDA.COMPANY_CODE AND " & vbCrLf & " IH.FROM_DIV_CODE = IDA.DIV_CODE AND " & vbCrLf & " IH.COMPANY_CODE = IDB.COMPANY_CODE AND " & vbCrLf & " IH.TO_DIV_CODE = IDB.DIV_CODE AND " & vbCrLf & " ID.COMPANY_CODE = IMST.COMPANY_CODE AND " & vbCrLf & " ID.ITEM_CODE = IMST.ITEM_CODE " & vbCrLf & " AND IH.AUTO_KEY_TRANS=" & Val(txtReqNo.Text) & "" & vbCrLf & " ORDER BY IH.AUTO_KEY_TRANS ASC "


        If lblBookType.Text = "I" Then
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\DivisionIssue.rpt"
        Else
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\DivisionReq.rpt"
        End If
        SetCrpt(Report1, Mode, 1, mTitle, , True, xMyMenu)
        Report1.SQLQuery = SqlStr
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If UpdateMain1 = True Then
            ADDMode = False
            MODIFYMode = False
            txtReqNo_Validating(txtReqNo, New System.ComponentModel.CancelEventArgs(False))
            If cmdAdd.Enabled = True Then cmdAdd.Focus()
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

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ISSUE_STATUS='N' AND SUBSTR(AUTO_KEY_TRANS,LENGTH(AUTO_KEY_TRANS)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        If MainClass.SearchGridMaster((txtReqNo.Text), "INV_DIVTRANS_HDR", "AUTO_KEY_TRANS", "TRANS_DATE", "ISSUE_FOR", , SqlStr) = True Then
            txtReqNo.Text = AcName
            txtReqNo_Validating(txtReqNo, New System.ComponentModel.CancelEventArgs(False))
            If txtReqNo.Enabled = True Then txtReqNo.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdSearchCC_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchCC.Click
        Call txtCost_DoubleClick(txtCost, New System.EventArgs())
    End Sub

    Private Sub cmdSearchEmp_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchEmp.Click
        Call txtEmp_DoubleClick(txtEmp, New System.EventArgs())
    End Sub

    Private Sub FrmDivTransReq_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim xIName As String
        Dim SqlStr As String = ""
        Dim mItemCode As String
        Dim mLotNo As String
        Dim mUOM As String = ""
        Dim mStockType As String = "" '

        If cboDivisionFrom.Text = "" Then
            If cboDivisionFrom.Enabled = True Then cboDivisionFrom.Focus()
            MsgInformation("Please Select From Division.")
            Exit Sub
        End If

        If cboDivisionTo.Text = "" Then
            If cboDivisionTo.Enabled = True Then cboDivisionTo.Focus()
            MsgInformation("Please Select To Division.")
            Exit Sub
        End If

        If lblBookType.Text = "I" Then Exit Sub

        If eventArgs.row = 0 And eventArgs.col = ColItemCode Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemCode


                'SqlStr = GetStockItemQry(.Text, "Y", VB6.Format(txtReqDate.Text, "DD/MM/YYYY"), ConWH)
                '    If MainClass.SearchGridMasterBySQL2(.Text, SqlStr, "Y", "1") = True Then
                '        .Row = .ActiveRow
                '        .Col = ColItemCode
                '        .Text = Trim(AcName)
                '    End If

                If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS='A' AND IS_CHILD='N'") = True Then
                        .Row = .ActiveRow
                        .Col = ColItemCode
                        .Text = Trim(AcName)
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


                '    SqlStr = GetStockItemQry(xIName, "N", VB6.Format(txtReqDate.Text, "DD/MM/YYYY"), ConWH)
                '    If MainClass.SearchGridMasterBySQL2(.Text, SqlStr, "Y", "2") = True Then
                '        .Row = .ActiveRow
                '        .Col = ColItemDesc
                '        .Text = Trim(AcName)
                '    End If

                If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        .Row = .ActiveRow
                        .Col = ColItemDesc
                        .Text = Trim(AcName)
                    Else
                        .Row = .ActiveRow
                        .Col = ColItemDesc
                        .Text = xIName
                    End If

                If MainClass.ValidateWithMasterTable(.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow
                    .Col = ColItemCode
                    .Text = Trim(MasterNo)
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColLotNo Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColLotNo
                mLotNo = Trim(.Text)


                .Col = ColUom
                mUOM = Trim(.Text)

                .Col = ColStockType
                mStockType = Trim(.Text)

                SqlStr = GetItemLotWiseQry(mItemCode, (txtReqDate.Text), mUOM, "STR", mStockType, mLotNo, ConWH, ConStockRefType_DTN, Val(txtReqNo.Text))
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColLotNo
                    .Text = Trim(AcName1)
                End If

                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColLotNo)
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColStockType Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColStockType
                If MainClass.SearchGridMaster(.Text, "INV_TYPE_MST", "STOCK_TYPE_CODE", "STOCK_TYPE_DESC", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow
                    .Col = ColStockType
                    .Text = AcName
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColStockType)
            End With
        End If

        If eventArgs.col = 0 And eventArgs.row > 0 Then '***ROW DEL. OPTION NOT REQ IN INVOICE
            SprdMain.Row = eventArgs.row
            SprdMain.Col = ColItemCode
            If eventArgs.row < SprdMain.MaxRows And (ADDMode = True Or MODIFYMode = True) Then
                MainClass.DeleteSprdRow(SprdMain, eventArgs.row, ColItemCode)
                MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
                FormatSprdMain(eventArgs.row)
            End If
        End If

    End Sub



    'Private Sub SprdMain_KeyDown(KeyCode As Integer, Shift As Integer)
    ''Dim mActiveCol As Long
    ''
    ''    mActiveCol = SprdMain.ActiveCol
    ''
    ''    If KeyCode = vbKeyReturn Or KeyCode = vbKeyTab Then
    ''        If mActiveCol = ColDemandQty Then
    ''            SprdMain.Row = SprdMain.ActiveRow
    ''            SprdMain.Col = ColDemandQty
    ''            If Val(SprdMain.Text) <> 0 Then
    ''                If SprdMain.MaxRows = SprdMain.ActiveRow Then
    ''                    MainClass.AddBlankSprdRow SprdMain, ColItemCode, ConRowHeight
    '                    FormatSprdMain SprdMain.MaxRows
    ''                End If
    ''            End If
    '            SprdMain.Row = SprdMain.MaxRows
    ''        End If
    ''    ElseIf KeyCode = vbKeyF1 Then
    ''        If mActiveCol = ColItemCode Then SprdMain_Click ColItemCode, 0
    ''        If mActiveCol = ColItemDesc Then SprdMain_Click ColItemDesc, 0
    ''
    ''    End If
    ''    KeyCode = 9999
    'End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim mBalQty As Double
        Dim mIssueQty As Double
        Dim xItemCode As String = ""
        Dim xItemDesc As String
        Dim xItemUOM As String
        Dim xStockType As String
        Dim xStockQty As Double
        Dim mIssuedQty As Double
        Dim mStdQty As Double
        Dim mDemandedQty As Double
        Dim xLotNo As String
        Dim mProdType As String
        Dim mCheckProdType As String
        Dim mDivisionCodeFrom As Double
        Dim mDivisionCodeTo As Double
        Dim mPurchaseRate As Double
        Dim mRate As Double
        Dim mFactor As Double


        Dim mLandedCost As Double
        Dim mIssueUOM As String = ""
        Dim mPurchaseUOM As String = ""
        Dim mItemCost As Double
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing


        If eventArgs.NewRow = -1 Then Exit Sub

        If cboDivisionFrom.Text = "" Then
            If cboDivisionFrom.Enabled = True Then cboDivisionFrom.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivisionFrom.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCodeFrom = CDbl(Trim(MasterNo))
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivisionTo.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCodeTo = CDbl(Trim(MasterNo))
        End If

        SprdMain.Row = SprdMain.ActiveRow

        Select Case eventArgs.col
            Case ColItemCode
                SprdMain.Col = ColItemCode
                xItemCode = Trim(SprdMain.Text)
                If xItemCode = "" Then Exit Sub
                If FillItemDescPart(xItemCode, True) = True Then
                    If DuplicateItem(ColItemCode) = False Then
                        FormatSprdMain(-1)
                        If lblBookType.Text = "I" Then
                            MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColIssueQty)
                        Else
                            MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColStockType)
                        End If
                    Else
                        eventArgs.cancel = True
                        Exit Sub
                    End If
                Else
                    eventArgs.cancel = True
                    Exit Sub
                End If
            Case ColItemDesc
                SprdMain.Col = ColItemCode
                xItemDesc = Trim(SprdMain.Text)
                If xItemDesc = "" Then Exit Sub
                If FillItemDescPart(xItemDesc, False) = True Then
                    If DuplicateItem(ColItemCode) = True Then
                        eventArgs.cancel = True
                        Exit Sub
                    End If
                Else
                    eventArgs.cancel = True
                    Exit Sub
                End If
            Case ColDemandQty
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColDemandQty
                If Val(SprdMain.Text) = 0 Then Exit Sub

                If CheckQty() = True Then
                    SprdMain.Row = SprdMain.ActiveRow
                    SprdMain.Col = ColStockQty
                    xStockQty = Val(SprdMain.Text)


                    SprdMain.Col = ColDemandQty
                    mDemandedQty = Val(SprdMain.Text)
                    If Val(SprdMain.Text) <> 0 Then
                        '                    If xStockQty < Val(SprdMain.Text) Then
                        '                        MsgInformation "You have not enough Stock."
                        '                        MainClass.SetFocusToCell SprdMain, SprdMain.ActiveRow, ColDemandQty
                        '                        Cancel = True
                        '                        Exit Sub
                        '                    Else
                        If SprdMain.MaxRows = SprdMain.ActiveRow Then
                            MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                            '                        FormatSprdMain SprdMain.MaxRows
                            FormatSprdMain(-1)
                        End If
                        '                    End If
                    End If

                    SprdMain.Col = ColIssueQty
                    If mDemandedQty < Val(SprdMain.Text) Then
                        MsgInformation("Demanded Qty Cann't be Less Than Issued Qty.")
                        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColDemandQty)
                        eventArgs.cancel = True
                        Exit Sub
                    End If
                End If
            Case ColIssueQty

                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColBalQty
                mBalQty = Val(SprdMain.Text)

                SprdMain.Col = ColIssuedQty
                mBalQty = mBalQty + Val(SprdMain.Text)


                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColIssueQty
                mIssueQty = Val(SprdMain.Text)

                If mIssueQty > mBalQty Then
                    MsgInformation("Issued Qty Cann't Be Greater Than Bal Qty.")
                    MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColIssueQty)
                    eventArgs.cancel = True
                    Exit Sub
                End If

                '            If xStockQty < Val(SprdMain.Text) Then
                '                MsgInformation "You have not enough Stock."
                '                MainClass.SetFocusToCell SprdMain, SprdMain.ActiveRow, ColDemandQty
                '                Cancel = True
                '                Exit Sub
                '            Else
                '                If SprdMain.MaxRows = SprdMain.ActiveRow Then
                '                    MainClass.AddBlankSprdRow SprdMain, ColItemCode, ConRowHeight
                ''                        FormatSprdMain SprdMain.MaxRows
                '                    FormatSprdMain -1
                '                End If
                '            End If
            Case ColLotNo
                If DuplicateItem(ColLotNo) = False Then
                    SprdMain.Row = SprdMain.ActiveRow

                    SprdMain.Col = ColItemCode
                    xItemCode = Trim(SprdMain.Text)

                    SprdMain.Col = ColUom
                    xItemUOM = Trim(SprdMain.Text)

                    SprdMain.Col = ColLotNo
                    xLotNo = Trim(SprdMain.Text)

                    SprdMain.Col = ColStockType
                    xStockType = Trim(SprdMain.Text)
                    If xStockType = "" Then Exit Sub


                    SprdMain.Col = ColIssuedQty
                    mIssuedQty = Val(SprdMain.Text)

                    SprdMain.Col = ColStockQty ''mIssuedQty +
                    SprdMain.Text = CStr(GetBalanceStockQty(xItemCode, (txtReqDate.Text), xItemUOM, "STR", xStockType, xLotNo, ConWH, mDivisionCodeFrom, ConStockRefType_DTN, Val(txtReqNo.Text)))

                    If lblBookType.Text = "R" And ADDMode = True Then
                        SqlStr = " SELECT ISSUE_UOM, PURCHASE_UOM, UOM_FACTOR,PURCHASE_COST FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(xItemCode) & "'"
                        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                        If RsTemp.EOF = False Then
                            mIssueUOM = IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)
                            mPurchaseUOM = IIf(IsDBNull(RsTemp.Fields("PURCHASE_UOM").Value), "", RsTemp.Fields("PURCHASE_UOM").Value)
                            mFactor = IIf(IsDBNull(RsTemp.Fields("UOM_FACTOR").Value) Or RsTemp.Fields("UOM_FACTOR").Value = 0, 1, RsTemp.Fields("UOM_FACTOR").Value)
                            mItemCost = IIf(IsDBNull(RsTemp.Fields("PURCHASE_COST").Value), 0, RsTemp.Fields("PURCHASE_COST").Value)
                        End If

                        If GetLatestItemCostFromPO(xItemCode, mPurchaseRate, mLandedCost, (txtReqDate.Text), "ST", "", xItemUOM, mFactor) = False Then GoTo ErrPart
                        mRate = IIf(mPurchaseRate = 0, mItemCost, mPurchaseRate)

                        SprdMain.Col = ColRate
                        SprdMain.Text = VB6.Format(mRate, "0.0000")
                    End If
                End If

            Case ColStockType
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColItemCode
                xItemCode = Trim(SprdMain.Text)

                SprdMain.Col = ColUom
                xItemUOM = Trim(SprdMain.Text)

                SprdMain.Col = ColStockType
                xStockType = Trim(SprdMain.Text)
                If xStockType = "" Then Exit Sub

                If MainClass.ValidateWithMasterTable(xStockType, "STOCK_TYPE_CODE", "STOCK_TYPE_DESC", "INV_TYPE_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                    '                MsgInformation "InValid Stock Type"
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColStockType)
                    eventArgs.cancel = True
                    Exit Sub
                Else
                    SprdMain.Col = ColIssuedQty
                    mIssuedQty = Val(SprdMain.Text)

                    SprdMain.Col = ColStockQty
                    SprdMain.Text = CStr(GetBalanceStockQty(xItemCode, (txtReqDate.Text), xItemUOM, "STR", xStockType, "", ConWH, mDivisionCodeFrom, ConStockRefType_DTN, Val(txtReqNo.Text)))

                    If lblBookType.Text = "R" And ADDMode = True Then
                        SqlStr = " SELECT ISSUE_UOM, PURCHASE_UOM, UOM_FACTOR,PURCHASE_COST FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(xItemCode) & "'"
                        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                        If RsTemp.EOF = False Then
                            mIssueUOM = IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)
                            mPurchaseUOM = IIf(IsDBNull(RsTemp.Fields("PURCHASE_UOM").Value), "", RsTemp.Fields("PURCHASE_UOM").Value)
                            mFactor = IIf(IsDBNull(RsTemp.Fields("UOM_FACTOR").Value) Or RsTemp.Fields("UOM_FACTOR").Value = 0, 1, RsTemp.Fields("UOM_FACTOR").Value)
                            mItemCost = IIf(IsDBNull(RsTemp.Fields("PURCHASE_COST").Value), 0, RsTemp.Fields("PURCHASE_COST").Value)
                        End If


                        If GetLatestItemCostFromPO(xItemCode, mPurchaseRate, mLandedCost, (txtReqDate.Text), "ST", "", xItemUOM, mFactor) = False Then GoTo ErrPart
                        mRate = IIf(mPurchaseRate = 0, mItemCost, mPurchaseRate)

                        SprdMain.Col = ColRate
                        SprdMain.Text = VB6.Format(mRate, "0.0000")
                    End If
                End If

        End Select
        '    FormatSprdMain -1
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Function DuplicateItem(ByRef pCol As Integer) As Boolean

        Dim cntRow As Integer
        Dim mCount As Byte
        Dim mCheckItemCode As String
        Dim mItemCode As String
        Dim mLotNo As String
        Dim mCheckLotNo As String
        Dim mRow As Integer

        With SprdMain
            .Row = .ActiveRow
            mRow = .ActiveRow
            .Col = ColItemCode
            mCheckItemCode = Trim(UCase(.Text))

            .Col = ColLotNo
            mCheckLotNo = Trim(UCase(.Text))

            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColItemCode
                mItemCode = Trim(UCase(.Text))

                .Col = ColLotNo
                mLotNo = Trim(UCase(.Text))

                If (mItemCode & ":" & mLotNo = mCheckItemCode & ":" & mCheckLotNo And mCheckItemCode <> "") Then
                    mCount = mCount + 1
                End If

                If mCount > 1 Then
                    DuplicateItem = True
                    MsgInformation("Duplicate Item : " & mCheckItemCode)
                    MainClass.SetFocusToCell(SprdMain, .ActiveRow, pCol)
                    Exit Function
                End If
            Next
        End With
    End Function

    Private Function CheckQty() As Boolean

        On Error GoTo ERR1
        CheckQty = True
        Exit Function
        With SprdMain
            .Row = .ActiveRow
            .Col = ColItemCode
            If Trim(.Text) = "" Then Exit Function

            .Col = ColDemandQty
            If Val(.Text) > 0 Then
                CheckQty = True
            Else
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColDemandQty)
            End If
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function FillItemDescPart(ByRef pItemCode As String, ByRef pIsItemCode As Boolean) As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        'Dim mProd_Type As Boolean
        'Dim xAutoIssue As Boolean
        '
        '    xAutoIssue = CheckAutoIssue(txtReqDate.Text)

        If Trim(pItemCode) = "" Then Exit Function
        With SprdMain
            SqlStr = "SELECT ITEM_CODE, ITEM_SHORT_DESC,ISSUE_UOM " & vbCrLf & " FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

            If pIsItemCode = True Then
                SqlStr = SqlStr & vbCrLf & " AND LTRIM(RTRIM(ITEM_CODE))='" & MainClass.AllowSingleQuote(pItemCode) & "'"
            Else
                SqlStr = SqlStr & vbCrLf & " AND LTRIM(RTRIM(ITEM_SHORT_DESC))='" & MainClass.AllowSingleQuote(pItemCode) & "'"
            End If

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then

                '            mProd_Type = IsProductionItem(pItemCode)
                '
                '            If xAutoIssue = True Then
                '                If mProd_Type = True Then
                '                    FillItemDescPart = False
                '                    MsgInformation "Auto Issue defined, so Cann't be Issue BOP & Jobwork Items"
                '                    MainClass.SetFocusToCell SprdMain, .ActiveRow, ColItemCode
                '                    Exit Function
                '                End If
                '            End If

                .Row = .ActiveRow
                .Col = ColItemCode
                .Text = Trim(IIf(IsDbNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))

                .Col = ColItemDesc
                .Text = IIf(IsDbNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value)

                .Col = ColUom
                .Text = IIf(IsDbNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)

                .Col = ColStockType
                .Text = IIf(Trim(.Text) = "", "ST", Trim(.Text))

                FillItemDescPart = True
            Else
                FillItemDescPart = False
                '            If pIsItemCode = True Then
                '                MsgInformation "Invalid Item Code"
                '            Else
                '                MsgInformation "Invalid Item Description"
                '            End If
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColItemCode)
            End If
        End With
        Exit Function
ERR1:
        FillItemDescPart = False
        MsgInformation(Err.Description)
    End Function

    Private Sub SprdMain_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles SprdMain.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim mCancel As Boolean
        mCancel = False
        With SprdMain
            SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, mCancel))
            Cancel = mCancel
        End With
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub SprdView_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdView.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        With SprdView
            .Row = eventArgs.row
            .Col = 1
            txtReqNo.Text = .Text
            txtReqNo_Validating(txtReqNo, New System.ComponentModel.CancelEventArgs(False))
            If txtReqNo.Enabled = True Then txtReqNo.Focus()
            CmdView_Click(CmdView, New System.EventArgs())
        End With
    End Sub
    Private Function AutoGenSeqNo() As Double

        On Error GoTo AutoGenSeqNoErr
        Dim RsMRRMainGen As ADODB.Recordset = Nothing
        Dim mNewSeqNo As Integer
        Dim SqlStr As String = ""
        Dim mMaxValue As String

        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_TRANS)  " & vbCrLf & " FROM INV_DIVTRANS_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_TRANS,LENGTH(AUTO_KEY_TRANS)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMRRMainGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsMRRMainGen
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
        AutoGenSeqNo = CDbl(mNewSeqNo & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"))
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
        Dim mEntryDate As String
        Dim mDivisionCodeFrom As Double
        Dim mDivisionDescFrom As String

        Dim mDivisionCodeTo As Double
        Dim mDivisionDescTo As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mStatus = IIf(chkIssue.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        If Val(txtReqNo.Text) = 0 Then
            mVNoSeq = AutoGenSeqNo()
        Else
            mVNoSeq = Val(txtReqNo.Text)
        End If

        txtReqNo.Text = CStr(Val(CStr(mVNoSeq)))

        mEntryDate = VB6.Format(PubCurrDate, "DD-MMM-YYYY") & " " & GetServerTime

        mDivisionDescFrom = cboDivisionFrom.Text
        If MainClass.ValidateWithMasterTable(mDivisionDescFrom, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCodeFrom = Val(MasterNo)
        End If

        mDivisionDescTo = cboDivisionTo.Text
        If MainClass.ValidateWithMasterTable(mDivisionDescTo, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCodeTo = Val(MasterNo)
        End If

        SqlStr = ""
        If ADDMode = True Then
            lblMKey.Text = CStr(mVNoSeq)
            SqlStr = "INSERT INTO INV_DIVTRANS_HDR (" & vbCrLf & " AUTO_KEY_TRANS, " & vbCrLf & " COMPANY_CODE, " & vbCrLf & " TRANS_DATE, FROM_DIV_CODE, TO_DIV_CODE, " & vbCrLf & " EMP_CODE, REMARKS, COST_CENTER_CODE, " & vbCrLf & " SHIFT_CODE, ISSUE_STATUS, " & vbCrLf & " ADDUSER,ADDDATE,MODUSER,MODDATE)" & vbCrLf & " VALUES( " & vbCrLf & " " & Val(CStr(mVNoSeq)) & "," & vbCrLf & " " & RsCompany.Fields("Company_Code").Value & "," & vbCrLf & " TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & mDivisionCodeFrom & "," & mDivisionCodeTo & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtEmp.Text)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtsubdept.Text)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtCost.Text)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote((cboShiftcd.Text)) & "', " & vbCrLf & " '" & mStatus & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & mEntryDate & "','DD-MON-YYYY HH24:MI'),'','')"

            ''VB6.Format(PubCurrDate, "DD-MMM-YYYY")
        ElseIf MODIFYMode = True Then
            SqlStr = ""
            SqlStr = "UPDATE INV_DIVTRANS_HDR SET TRANS_DATE=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " FROM_DIV_CODE=" & mDivisionCodeFrom & ", TO_DIV_CODE=" & mDivisionCodeTo & ", " & vbCrLf & " EMP_CODE ='" & txtEmp.Text & "', " & vbCrLf & " REMARKS ='" & MainClass.AllowSingleQuote((txtsubdept.Text)) & "'," & vbCrLf & " COST_CENTER_CODE ='" & txtCost.Text & "'," & vbCrLf & " SHIFT_CODE ='" & MainClass.AllowSingleQuote((cboShiftcd.Text)) & "', " & vbCrLf & " ISSUE_STATUS ='" & mStatus & "'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND AUTO_KEY_TRANS =" & Val(lblMKey.Text) & ""
        End If
        PubDBCn.Execute(SqlStr)
        If UpdateDetail1(CStr(Val(CStr(mVNoSeq))), mDivisionCodeFrom, mDivisionCodeTo) = False Then GoTo ErrPart

        UpdateMain1 = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        UpdateMain1 = False
        PubDBCn.RollbackTrans() ''

        If ADDMode = True Then
            txtReqNo.Text = ""
        End If

        RsReqMain.Requery() ''.Refresh
        RsReqDetail.Requery() ''.Refresh
        If Err.Description = "" Then Exit Function
        '    If err.Number = -2147217900 Then
        '        ErrorMsg "Duplicate Item Consumption Generated, Save Again", "Duplicate", vbCritical
        '    Else
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    End If
        ''Resume
    End Function
    Private Function UpdateDetail1(ByRef pVnoseq As String, ByRef mDivisionCodeFrom As Double, ByRef mDivisionCodeTo As Double) As Boolean

        On Error GoTo UpdateDetail1Err
        Dim RsBOM As ADODB.Recordset = Nothing
        Dim mSqlStr As String
        Dim SqlStr As String = ""
        Dim i As Integer
        Dim j As Integer
        Dim mItemCode As String
        Dim mUOM As String = ""
        Dim mStockType As String = ""
        Dim mQty As Double
        Dim mRemarks As String
        Dim mIssueQty As Double
        Dim mLotNoRequied As String
        Dim mProd_Type As String
        Dim mIsConsumable As String
        Dim mLotNo As String

        Dim cntRow As Integer
        Dim xSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing


        Dim xItemCode As String = ""
        Dim xChildStock As Double
        Dim mBalIssueQty As Double
        Dim xChildIssue As Double
        Dim cntStockSno As Integer
        Dim mRate As Double

        SqlStr = " Delete From INV_DIVTRANS_DET " & vbCrLf & " WHERE AUTO_KEY_TRANS=" & Val(lblMKey.Text) & ""
        PubDBCn.Execute(SqlStr)

        If lblBookType.Text = "I" Then
            If DeleteStockTRN(PubDBCn, ConStockRefType_DTN, (txtReqNo.Text)) = False Then GoTo UpdateDetail1Err
            If DeletePaintStockTRN(PubDBCn, ConStockRefType_DTN, (txtReqNo.Text)) = False Then GoTo UpdateDetail1Err
        End If


        With SprdMain
            For i = 1 To .MaxRows - 1
                .Row = i

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColUom
                mUOM = MainClass.AllowSingleQuote(.Text)

                .Col = ColStockType
                mStockType = MainClass.AllowSingleQuote(.Text)

                .Col = ColDemandQty
                mQty = Val(.Text)

                .Col = ColIssueQty
                mIssueQty = Val(.Text)
                mBalIssueQty = Val(.Text)
                .Col = ColLotNo
                mLotNo = Trim(.Text)

                mProd_Type = GetProductionType(mItemCode)

                .Col = ColRemarks
                mRemarks = MainClass.AllowSingleQuote(.Text)

                .Col = ColRate
                mRate = Val(.Text)

                SqlStr = ""

                If mItemCode <> "" And mQty > 0 Then
                    SqlStr = " INSERT INTO INV_DIVTRANS_DET ( " & vbCrLf & " AUTO_KEY_TRANS,SERIAL_NO,ITEM_CODE,ITEM_UOM,REMARKS," & vbCrLf & " STOCK_TYPE,DEMAND_QTY,ISSUE_QTY, COMPANY_CODE,LOT_NO,ITEM_RATE) "
                    SqlStr = SqlStr & vbCrLf & " VALUES (" & Val(lblMKey.Text) & ", " & i & "," & vbCrLf & " '" & MainClass.AllowSingleQuote(mItemCode) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mUOM) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mRemarks) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mStockType) & "', " & vbCrLf & " " & mQty & "," & mIssueQty & ", " & RsCompany.Fields("COMPANY_CODE").Value & ",'" & mLotNo & "'," & mRate & ") "
                    PubDBCn.Execute(SqlStr)

                    '                mLotNo = IIf(mLotNo = 0, -1, mLotNo)

                    If lblBookType.Text = "I" Then

                        If mBalIssueQty > 0 Then
                            If UpdateStockTRN(PubDBCn, ConStockRefType_DTN, (txtReqNo.Text), i, (txtReqDate.Text), (txtReqDate.Text), mStockType, mItemCode, mUOM, mLotNo, mBalIssueQty, 0, "O", 0, 0, "", "", "STR", "STR", "", "N", "To Division Transfer Note : " & cboDivisionTo.Text, "-1", ConWH, mDivisionCodeFrom, "", "") = False Then GoTo UpdateDetail1Err

                            j = i + 10000
                            If UpdateStockTRN(PubDBCn, ConStockRefType_DTN, (txtReqNo.Text), j, (txtReqDate.Text), (txtReqDate.Text), mStockType, mItemCode, mUOM, mLotNo, mBalIssueQty, 0, "I", 0, 0, "", "", "STR", "STR", "", "N", "From Division Transfer Note : " & cboDivisionFrom.Text, "-1", ConWH, mDivisionCodeTo, "", "") = False Then GoTo UpdateDetail1Err
                        End If

                        mLotNoRequied = "N"
                        If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mLotNoRequied = MasterNo
                        End If

                        If mLotNoRequied = "Y" Then
                            If UpdateLotInPaintStock(i, (txtReqNo.Text), (txtReqDate.Text), mItemCode, mUOM, mIssueQty, "STORE") = False Then GoTo UpdateDetail1Err
                        End If
                    End If
                End If
            Next
        End With
        UpdateDetail1 = True
        Exit Function
UpdateDetail1Err:
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Function

    Private Function FieldsVarification() As Boolean

        On Error GoTo err_Renamed
        Dim mRow As Integer
        Dim mSTTaxcount As Integer
        Dim mLockBookCode As Integer
        Dim mCheckLastEntryDate As String
        Dim mDeptCode As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim xAutoIssue As Boolean
        Dim cntRow As Integer
        Dim mProd_Type As Boolean
        Dim mItemCode As String
        Dim mCheckProdType As String
        Dim mDemandedQty As Double
        Dim mDeptQty As Double
        Dim mStdQty As Double
        Dim mTodayReq As Double
        Dim mMinReq As Double
        Dim mTodayDemanded As Double
        Dim mTotTodayDemanded As Double
        Dim mDataTrue As Boolean
        Dim mString As String = ""
        Dim mTodayIssue As Double
        Dim mLotNoRequied As String
        Dim mDivisionCode As Double

        xAutoIssue = CheckAutoIssue((txtReqDate.Text), "")

        FieldsVarification = True
        If ValidateBranchLocking((txtReqDate.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If

        If lblBookType.Text = "R" Then
            mLockBookCode = CInt(ConLockStoreReq)
        Else
            mLockBookCode = CInt(ConLockIssueNote)
        End If

        If ValidateBookLocking(PubDBCn, mLockBookCode, txtReqDate.Text) = True Then
            FieldsVarification = False
            Exit Function
        End If

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsReqMain.EOF = True Then Exit Function

        If lblBookType.Text = "I" Then
            If txtReqNo.Text = "" Then
                MsgInformation("Requisition No. Cann't Blank")
                FieldsVarification = False
                Exit Function
            End If
        Else
            If MODIFYMode = True And txtReqNo.Text = "" Then
                MsgInformation("Requisition No. Cann't Blank")
                FieldsVarification = False
                Exit Function
            End If
        End If
        If txtReqDate.Text = "" Then
            MsgBox("Date is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtReqDate.Focus()
            Exit Function
        ElseIf FYChk((txtReqDate.Text)) = False Then
            FieldsVarification = False
            If txtReqDate.Enabled = True Then txtReqDate.Focus()
            Exit Function
        End If

        If lblBookType.Text = "R" Then
            '        If CheckStockQty(SprdMain, ColStockQty, ColDemandQty, ColItemCode, ColStockType, True) = False Then
            '            FieldsVarification = False
            '            Exit Function
            '        End If
        Else
            If CheckStockQty(SprdMain, ColStockQty, ColIssueQty, ColItemCode, ColStockType, True) = False Then
                FieldsVarification = False
                Exit Function
            End If

            If CheckBalDemandedQty(SprdMain, ColDemandQty, ColIssueQty) = True Then
                chkIssue.CheckState = System.Windows.Forms.CheckState.Checked
            End If
        End If


        If Trim(cboDivisionFrom.Text) = "" Then
            MsgBox("Division From Name is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            cboDivisionFrom.Focus()
            Exit Function
        End If

        If Trim(cboDivisionTo.Text) = "" Then
            MsgBox("Division To Name is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            cboDivisionTo.Focus()
            Exit Function
        End If

        If Trim(cboDivisionFrom.Text) = Trim(cboDivisionTo.Text) Then
            MsgBox("Division From & To Cann't be Same.", MsgBoxStyle.Information)
            FieldsVarification = False
            cboDivisionTo.Focus()
            Exit Function
        End If

        If lblBookType.Text = "R" Then
            If ValidateDeptRight(PubUserID, "STR", "STORE") = False Then
                FieldsVarification = False
                Exit Function
            End If
            '        If PubSuperUser = "U" Then
            '            If MainClass.ValidateWithMasterTable(PubUserEMPCode, "EMP_CODE", "EMP_DEPT_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
            '                mDeptCode = MasterNo
            '                If UCase(Trim(txtDept.Text)) <> UCase(Trim(mDeptCode)) Then
            '                    MsgBox "You Are Not in Req. Dept.", vbInformation
            '                    FieldsVarification = False
            '                End If
            '            Else
            '                MsgBox "Invalid Emp Code.", vbInformation
            '                FieldsVarification = False
            '            End If
            '        End If

            If cboDivisionFrom.Text = "" Then
                If cboDivisionFrom.Enabled = True Then cboDivisionFrom.Focus()
                MsgInformation("Please Select From Division.")
                FieldsVarification = False
                Exit Function
            End If

            If MainClass.ValidateWithMasterTable((cboDivisionTo.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = Val(MasterNo)
            Else
                MsgInformation("Invalid From Division Name.")
                FieldsVarification = False
                Exit Function
            End If

            If ValidateDivisionRight(PubUserID, mDivisionCode, UCase(Trim(cboDivisionTo.Text))) = False Then
                FieldsVarification = False
                Exit Function
            End If

        End If


        If lblBookType.Text = "I" Then
            If PubSuperUser = "U" Then
                '            If MainClass.ValidateWithMasterTable(PubUserEMPCode, "EMP_CODE", "EMP_DEPT_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
                '                mDeptCode = MasterNo
                '                If UCase(Trim(mDeptCode)) <> "STR" Then
                '                    MsgBox "You Are Not in Store Dept.", vbInformation
                '                    FieldsVarification = False
                '                    Exit Function
                '                End If
                If ValidateDeptRight(PubUserID, "STR", "STORE") = False Then
                    MsgBox("Invalid Emp Code.", MsgBoxStyle.Information)
                    FieldsVarification = False
                    Exit Function
                    '            Else
                    '                MsgBox "Invalid Emp Code.", vbInformation
                    '                FieldsVarification = False
                    '                Exit Function
                End If

                If cboDivisionTo.Text = "" Then
                    If cboDivisionTo.Enabled = True Then cboDivisionTo.Focus()
                    MsgInformation("Please Select To Division.")
                    FieldsVarification = False
                    Exit Function
                End If

                If MainClass.ValidateWithMasterTable((cboDivisionFrom.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivisionCode = Val(MasterNo)
                Else
                    MsgInformation("Invalid To Division Name.")
                    FieldsVarification = False
                    Exit Function
                End If

                If ValidateDivisionRight(PubUserID, mDivisionCode, UCase(Trim(cboDivisionFrom.Text))) = False Then
                    FieldsVarification = False
                    Exit Function
                End If

            End If
        End If


        If Trim(txtCost.Text) = "" Then
            MsgBox("Cost Center Code is Blank. Cann't Save", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtCost.Enabled Then txtCost.Focus()
            Exit Function
        Else
            SqlStr = " SELECT IH.CC_CODE, IH.CC_DESC, ID.DEPT_CODE " & vbCrLf & " FROM FIN_CCENTER_HDR IH, FIN_CCENTER_DET ID " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=ID.COMPANY_CODE AND IH.CC_CODE=ID.CC_CODE" & vbCrLf & " AND IH.CC_CODE='" & MainClass.AllowSingleQuote((txtCost.Text)) & "'" & vbCrLf & " AND ID.DEPT_CODE='STR'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = True Then
                MsgInformation("Invalid Cost Center Code for Department : STORE")
                FieldsVarification = False
                If txtCost.Enabled Then txtCost.Focus()
                Exit Function
            End If
        End If

        If Trim(txtEmp.Text) = "" Then
            MsgBox("Employee Code is Blank. Cann't Save", MsgBoxStyle.Information)
            FieldsVarification = False
            txtEmp.Focus()
            Exit Function
        Else

            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
            SqlStr = SqlStr & vbCrLf & " AND (EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE='' OR EMP_LEAVE_DATE>=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"


            If MainClass.ValidateWithMasterTable(txtEmp.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
                MsgBox("Invalid Employee Code. Cann't Save", MsgBoxStyle.Information)
                FieldsVarification = False
                txtEmp.Focus()
                Exit Function
            End If
        End If

        If PubSuperUser <> "S" Then
            mCheckLastEntryDate = GetLastEntryDate
            If mCheckLastEntryDate <> "" Then
                If CDate(txtReqDate.Text) < CDate(mCheckLastEntryDate) Then
                    MsgBox("Cann't be Add or Modify Back Entry", MsgBoxStyle.Information)
                    FieldsVarification = False
                    Exit Function
                End If
            End If
        End If

        '    If xAutoIssue = True Then
        '        With SprdMain
        '            For cntRow = 1 To .MaxRows
        '                .Row = cntRow
        '                .Col = ColItemCode
        '                mItemCode = Trim(.Text)
        '                If mItemCode <> "" Then
        '                    mProd_Type = IsProductionItem(mItemCode)
        '
        '                    If mProd_Type = True Then
        '                        FieldsVarification = False
        '                        MsgInformation "Auto Issue defined, so Cann't be Issue BOP & Jobwork Items"
        '                        MainClass.SetFocusToCell SprdMain, cntRow, ColItemCode
        '                        Exit Function
        '                    End If
        '                End If
        '            Next
        '        End With
        '    End If
        mDataTrue = False

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColStockType
                If Trim(.Text) = "QC" Then
                    FieldsVarification = False
                    MsgInformation("QC Stock Type Cann't be Issue. Please Change Stock Type.")
                    MainClass.SetFocusToCell(SprdMain, cntRow, ColStockType)
                    Exit Function
                End If

                .Row = cntRow
                .Col = ColDemandQty
                If Val(.Text) > 0 Then
                    mDataTrue = True
                End If
            Next
        End With

        If mDataTrue = False Then
            FieldsVarification = False
            MsgInformation("Nothing to Save.")
            MainClass.SetFocusToCell(SprdMain, cntRow, ColItemCode)
            Exit Function
        End If

        FieldsVarification = True
        If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "ItemCode Is Blank.") = False Then FieldsVarification = False : Exit Function
        '    If MainClass.ValidDataInGrid(SprdMain, ColDemandQty, "N", "Please Check Quantity.") = False Then FieldsVarification = False: Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColStockType, "S", "Please Check Stock Type.") = False Then FieldsVarification = False : Exit Function

        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function
    Private Function GetLastEntryDate() As String

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        GetLastEntryDate = ""
        SqlStr = ""
        SqlStr = "SELECT Max(TRANS_DATE) AS  TRANS_DATE " & vbCrLf & " FROM INV_DIVTRANS_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_TRANS,LENGTH(AUTO_KEY_TRANS)-5,4)=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND ISSUE_STATUS='Y'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            GetLastEntryDate = IIf(IsDbNull(RsTemp.Fields("TRANS_DATE").Value), "", RsTemp.Fields("TRANS_DATE").Value)
        End If

        Exit Function
ErrPart:

    End Function
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Public Sub FrmDivTransReq_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If lblBookType.Text = "I" Then
            Me.Text = "Division Transfer Note - Issue"
        Else
            Me.Text = "Division Transfer Note - Requisition"
        End If

        SqlStr = ""
        SqlStr = "Select * from INV_DIVTRANS_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsReqMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from INV_DIVTRANS_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsReqDetail, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)
        '    Call SetTextLengths

        If lblBookType.Text = "R" Then
            If cmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        Else
            Clear1()
        End If

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

        SqlStr = " SELECT  AUTO_KEY_TRANS ISSUE_NO, TRANS_DATE,FROM_DIV_CODE,TO_DIV_CODE, " & vbCrLf & " EMP_CODE EMP,DECODE(ISSUE_STATUS,'Y','COMPLETE','PENDING') AS STATUS, " & vbCrLf & " REMARKS "

        ''FROM CLAUSE...

        SqlStr = SqlStr & vbCrLf & " FROM INV_DIVTRANS_HDR "

        ''WHERE CLAUSE...

        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & vbCrLf & " AND SUBSTR(AUTO_KEY_TRANS,LENGTH(AUTO_KEY_TRANS)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        ''ORDER BY CLAUSE...

        SqlStr = SqlStr & vbCrLf & " Order by AUTO_KEY_TRANS"

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
            .set_ColWidth(1, 1200)
            .Col = 1
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight

            .set_ColWidth(2, 1200)
            .Col = 2
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter

            .set_ColWidth(3, 1000)
            .set_ColWidth(4, 1000)
            .set_ColWidth(5, 1000)
            .set_ColWidth(6, 3000)

            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            SprdView.set_RowHeight(-1, 300)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim i As Integer

        With SprdMain
            .set_RowHeight(0, ConRowHeight * 2)
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight * 1.25)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsReqDetail.Fields("ITEM_CODE").DefinedSize ''
            .set_ColWidth(ColItemCode, 8)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn)
            If lblBookType.Text = "R" Then
                .set_ColWidth(ColItemDesc, 30)
            Else
                .set_ColWidth(ColItemDesc, 19)
            End If

            .Col = ColUom
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ITEM_UOM", "INV_DIVTRANS_DET", PubDBCn)
            .set_ColWidth(ColUom, 4)

            .Col = ColStockType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = MainClass.SetMaxLength("STOCK_TYPE", "INV_DIVTRANS_DET", PubDBCn)
            .set_ColWidth(ColStockType, 4.5)

            .Col = ColLotNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = RsReqDetail.Fields("LOT_NO").DefinedSize '' MainClass.SetMaxLength("LOT_NO", "INV_DIVTRANS_DET", PubDBCn)
            .set_ColWidth(ColLotNo, 6)

            .Col = ColStockQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColStockQty, 8)

            .Col = ColRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColRate, 7)
            '        .ColHidden = True

            .Col = ColDemandQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColDemandQty, 9)

            .Col = ColIssueQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColIssueQty, 9)
            If lblBookType.Text = "R" Then
                .ColHidden = True
            Else
                .ColHidden = False
            End If

            .Col = ColIssuedQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColIssuedQty, 8)
            .ColHidden = True

            .Col = ColBalQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColBalQty, 8)
            If lblBookType.Text = "R" Then
                .ColHidden = True
            Else
                .ColHidden = False
            End If

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = MainClass.SetMaxLength("REMARKS", "INV_DIVTRANS_DET", PubDBCn)
            If lblBookType.Text = "R" Then
                .set_ColWidth(ColRemarks, 11)
            Else
                .set_ColWidth(ColRemarks, 6)
            End If

        End With
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemDesc, ColUom)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColStockQty, ColStockQty)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColIssuedQty, ColBalQty)
        If lblBookType.Text = "I" Then
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemCode, ColItemCode)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColStockType, ColLotNo)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColDemandQty, ColDemandQty)
        End If

        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then RsReqDetail.Requery() : Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        With RsReqMain
            txtReqDate.Maxlength = 10
            txtReqNo.Maxlength = .Fields("AUTO_KEY_TRANS").Precision
            txtEmp.Maxlength = .Fields("EMP_CODE").DefinedSize
            txtCost.Maxlength = .Fields("COST_CENTER_CODE").DefinedSize
            txtsubdept.Maxlength = .Fields("REMARKS").DefinedSize
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Show1()

        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim mDivisionCodeFrom As Double
        Dim mDivisionDescFrom As String

        Dim mDivisionCodeTo As Double
        Dim mDivisionDescTo As String

        With RsReqMain
            If Not .EOF Then
                txtReqNo.Enabled = False
                lblMKey.Text = .Fields("AUTO_KEY_TRANS").Value


                txtReqNo.Text = IIf(IsDbNull(.Fields("AUTO_KEY_TRANS").Value), 0, .Fields("AUTO_KEY_TRANS").Value)
                txtReqDate.Text = VB6.Format(IIf(IsDbNull(.Fields("TRANS_DATE").Value), "", .Fields("TRANS_DATE").Value), "DD/MM/YYYY")
                txtEntryDate.Text = VB6.Format(IIf(IsDbNull(.Fields("ADDDATE").Value), "", .Fields("ADDDATE").Value), "DD/MM/YYYY HH:MM")

                txtEmp.Text = IIf(IsDbNull(.Fields("EMP_CODE").Value), "", .Fields("EMP_CODE").Value)
                txtCost.Text = IIf(IsDbNull(.Fields("COST_CENTER_CODE").Value), "", .Fields("COST_CENTER_CODE").Value)
                txtsubdept.Text = IIf(IsDbNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)
                chkIssue.CheckState = IIf(.Fields("ISSUE_STATUS").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                chkIssue.Enabled = IIf(.Fields("ISSUE_STATUS").Value = "Y", False, True)

                cboShiftcd.Text = IIf(IsDbNull(.Fields("SHIFT_CODE").Value), "", .Fields("SHIFT_CODE").Value)

                If MainClass.ValidateWithMasterTable((txtEmp.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    lblEmpname.Text = MasterNo
                Else
                    lblEmpname.Text = ""
                End If

                If MainClass.ValidateWithMasterTable((txtCost.Text), "CC_CODE", "CC_DESC", "FIN_CCENTER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    lblCostctr.Text = MasterNo
                Else
                    lblCostctr.Text = ""
                End If

                mDivisionCodeFrom = IIf(IsDbNull(.Fields("FROM_DIV_CODE").Value), "", .Fields("FROM_DIV_CODE").Value)
                If MainClass.ValidateWithMasterTable(mDivisionCodeFrom, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivisionDescFrom = Trim(MasterNo)
                    cboDivisionFrom.Text = mDivisionDescFrom
                End If

                mDivisionCodeTo = IIf(IsDbNull(.Fields("TO_DIV_CODE").Value), "", .Fields("TO_DIV_CODE").Value)
                If MainClass.ValidateWithMasterTable(mDivisionCodeTo, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivisionDescTo = Trim(MasterNo)
                    cboDivisionTo.Text = mDivisionDescTo
                End If


                If lblBookType.Text = "I" Then
                    txtEmp.Enabled = False
                    txtCost.Enabled = False
                    cmdSearchEmp.Enabled = False
                    cmdSearchCC.Enabled = False

                End If
                cboDivisionFrom.Enabled = False
                cboDivisionTo.Enabled = False

                Call ShowDetail1(.Fields("AUTO_KEY_TRANS").Value, mDivisionCodeFrom, mDivisionCodeTo)
            End If
        End With
        ADDMode = False
        MODIFYMode = False

        MainClass.ButtonStatus(Me, XRIGHT, RsReqMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)

        '    FormatSprdMain -1

        SprdMain.Enabled = True
        txtReqNo.Enabled = True
        cmdSearch.Enabled = True
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub
    Private Sub ShowDetail1(ByVal pReqNum As Double, ByRef mDivisionCodeFrom As Double, ByRef mDivisionCodeTo As Double)

        On Error GoTo ERR1
        Dim i As Integer
        Dim SqlStr As String = ""
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mItemUOM As String = ""
        Dim mDemandQty As String
        Dim mIssueQty As String
        Dim mStkType As String
        Dim mRemarks As String
        Dim mDate As String
        Dim mWIPStock As String = ""
        Dim mStdQty As String
        Dim mLotNo As String
        Dim mRate As Double

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM INV_DIVTRANS_DET  " & vbCrLf & " Where AUTO_KEY_TRANS = " & Val(CStr(pReqNum)) & "" & vbCrLf & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsReqDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsReqDetail
            If .EOF = True Then Exit Sub
            '        FormatSprdMain -1
            i = 1
            .MoveFirst()

            Do While Not .EOF

                SprdMain.Row = i

                SprdMain.Col = ColItemCode
                mItemCode = IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)
                SprdMain.Text = Trim(mItemCode)

                SprdMain.Col = ColItemDesc
                MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mItemDesc = MasterNo
                SprdMain.Text = mItemDesc

                SprdMain.Col = ColUom
                mItemUOM = IIf(IsDBNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)
                SprdMain.Text = mItemUOM

                SprdMain.Col = ColStockType
                mStkType = IIf(IsDBNull(.Fields("STOCK_TYPE").Value), "", .Fields("STOCK_TYPE").Value)
                SprdMain.Text = mStkType

                mIssueQty = IIf(IsDBNull(.Fields("ISSUE_QTY").Value), 0, .Fields("ISSUE_QTY").Value)

                '            If Left(cboShiftcd.Text, 1) = "C" Then
                '                mDate = DateAdd("d", 1, txtReqDate.Text)
                '            Else
                mDate = txtReqDate.Text
                '            End If

                SprdMain.Col = ColLotNo
                mLotNo = IIf(IsDBNull(.Fields("LOT_NO").Value), "", .Fields("LOT_NO").Value)
                mLotNo = IIf(Val(mLotNo) <= 0, "", mLotNo)
                SprdMain.Text = mLotNo       '' VB6.Format(IIf(IsDBNull(.Fields("LOT_NO").Value), "", .Fields("LOT_NO").Value))


                SprdMain.Col = ColStockQty
                SprdMain.Text = CStr(GetBalanceStockQty(mItemCode, mDate, mItemUOM, "STR", mStkType, mLotNo, ConWH, mDivisionCodeFrom, ConStockRefType_DTN, Val(txtReqNo.Text)))

                mRate = IIf(IsDBNull(.Fields("ITEM_RATE").Value), "0", .Fields("ITEM_RATE").Value)

                SprdMain.Col = ColRate
                SprdMain.Text = VB6.Format(mRate, "0.0000")

                SprdMain.Col = ColDemandQty
                mDemandQty = IIf(IsDBNull(.Fields("DEMAND_QTY").Value), 0, .Fields("DEMAND_QTY").Value)
                SprdMain.Text = mDemandQty

                SprdMain.Col = ColIssueQty
                SprdMain.Text = mIssueQty

                SprdMain.Col = ColIssuedQty
                SprdMain.Text = mIssueQty

                SprdMain.Col = ColBalQty
                SprdMain.Text = CStr(Val(CStr(CDbl(mDemandQty) - CDbl(mIssueQty))))

                SprdMain.Col = ColRemarks
                mRemarks = IIf(IsDBNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)
                SprdMain.Text = mRemarks

                .MoveNext()

                i = i + 1
                SprdMain.MaxRows = i
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
        MainClass.ButtonStatus(Me, XRIGHT, RsReqMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub Clear1()


        lblMKey.Text = ""

        txtReqDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtReqNo.Text = ""

        If Trim(PubUserEMPCode) = "" Then
            txtEmp.Text = ""
            txtEmp.Enabled = True
            lblEmpname.Text = ""
        Else
            txtEmp.Text = PubUserEMPCode
            txtEmp.Enabled = False
            If MainClass.ValidateWithMasterTable((txtEmp.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblEmpname.Text = MasterNo
            Else
                lblEmpname.Text = ""
            End If
        End If

        txtCost.Text = ""
        txtsubdept.Text = ""
        cboShiftcd.SelectedIndex = 0

        cboDivisionFrom.SelectedIndex = -1
        cboDivisionFrom.Enabled = True

        cboDivisionTo.SelectedIndex = -1
        cboDivisionTo.Enabled = True

        lblCostctr.Text = ""

        txtEntryDate.Text = VB6.Format(PubCurrDate, "DD/MM/YYYY") & " " & GetServerTime
        chkIssue.CheckState = System.Windows.Forms.CheckState.Unchecked

        txtReqDate.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False)


        txtCost.Enabled = True
        cmdSearchEmp.Enabled = True

        cmdSearchCC.Enabled = True

        chkIssue.Enabled = IIf(lblBookType.Text = "I", True, False)
        cboShiftcd.Enabled = IIf(lblBookType.Text = "R", True, False)

        pDataShow = False
        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsReqMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub FrmDivTransReq_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    'Private Sub Form_KeyUp(KeyCode As Integer, Shift As Integer)
    ''    MainClass.DoFunctionKey Me, KeyCode
    'End Sub
    Public Sub FrmDivTransReq_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        xMyMenu = myMenu

        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Call SetMainFormCordinate(Me)
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        ''Me.Width = VB6.TwipsToPixelsX(11355)

        'AdoDCMain.Visible = False
        FillCboFormType()
        txtReqNo.Enabled = True
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

        If eventArgs.KeyCode = System.Windows.Forms.Keys.Return Or eventArgs.KeyCode = System.Windows.Forms.Keys.Tab Then
            If mCol = ColDemandQty Then
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColDemandQty
                If Val(SprdMain.Text) <> 0 Then
                    If SprdMain.MaxRows = SprdMain.ActiveRow Then
                        MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                        '                    FormatSprdMain SprdMain.MaxRows
                    End If
                End If
                '            SprdMain.Row = SprdMain.MaxRows
            End If
        End If
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemDesc, 0))
        SprdMain.Refresh()
    End Sub
    Private Sub txtCost_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCost.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCost_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCost.DoubleClick
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = " SELECT IH.CC_CODE, IH.CC_DESC, ID.DEPT_CODE " & vbCrLf & " FROM FIN_CCENTER_HDR IH, FIN_CCENTER_DET ID " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=ID.COMPANY_CODE AND IH.CC_CODE=ID.CC_CODE" & vbCrLf & " AND ID.DEPT_CODE='STR'"

        '    If MainClass.SearchGridMaster(txtCost.Text, "FIN_CCENTER_HDR", "CC_CODE", "CC_DESC", , , SqlStr) = True Then
        If MainClass.SearchGridMasterBySQL2((txtCost.Text), SqlStr) = True Then
            txtCost.Text = AcName
            lblCostctr.Text = AcName1
            txtCost_Validating(txtCost, New System.ComponentModel.CancelEventArgs(False))
            If txtCost.Enabled = True Then txtCost.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtCost_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCost.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCost.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCost_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCost.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtCost_DoubleClick(txtCost, New System.EventArgs())
    End Sub

    Private Sub txtCost_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCost.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        If Trim(txtCost.Text) = "" Then GoTo EventExitSub
        txtCost.Text = VB6.Format(txtCost.Text, "000")

        SqlStr = " SELECT IH.CC_CODE, IH.CC_DESC, ID.DEPT_CODE " & vbCrLf & " FROM FIN_CCENTER_HDR IH, FIN_CCENTER_DET ID " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=ID.COMPANY_CODE AND IH.CC_CODE=ID.CC_CODE" & vbCrLf & " AND IH.CC_CODE='" & MainClass.AllowSingleQuote((txtCost.Text)) & "'" & vbCrLf & " AND ID.DEPT_CODE='STR'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            lblCostctr.Text = IIf(IsDbNull(RsTemp.Fields("CC_DESC").Value), "", RsTemp.Fields("CC_DESC").Value)
        Else
            MsgInformation("Invalid Cost Center Code for Department : Store")
            Cancel = True
        End If

        '    If MainClass.ValidateWithMasterTable(txtCost.Text, "CC_CODE", "CC_DESC", "FIN_CCENTER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
        '        lblCostctr.text = MasterNo
        '    Else
        '        MsgInformation "Invalid CostC Code"
        '        Cancel = True
        '    End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtReqDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtReqDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtReqDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtReqDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtReqDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtReqDate.Text) Then
            MsgInformation("Invalid Date")
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
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        SqlStr = SqlStr & vbCrLf & " AND (EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE='' OR EMP_LEAVE_DATE>=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            txtEmp.Text = AcName1
            lblEmpname.Text = AcName
            txtEmp_Validating(txtEmp, New System.ComponentModel.CancelEventArgs(False))
            If txtEmp.Enabled = True Then txtEmp.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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
        Dim SqlStr As String = ""

        If Trim(txtEmp.Text) = "" Then GoTo EventExitSub
        txtEmp.Text = VB6.Format(txtEmp.Text, "000000")

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        SqlStr = SqlStr & vbCrLf & " AND (EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE='' OR EMP_LEAVE_DATE>=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"


        If MainClass.ValidateWithMasterTable((txtEmp.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            lblEmpname.Text = MasterNo
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

    Private Sub txtReqNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtReqNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtReqNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtReqNo.DoubleClick
        Call cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Private Sub txtReqNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtReqNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtReqNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtReqNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtReqNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Public Sub txtReqNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtReqNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mReqnum As String = ""

        If Trim(txtReqNo.Text) = "" Then GoTo EventExitSub

        If Len(txtReqNo.Text) < 6 Then
            txtReqNo.Text = Val(txtReqNo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        If MODIFYMode = True And RsReqMain.EOF = False Then mReqnum = RsReqMain.Fields("AUTO_KEY_TRANS").Value

        SqlStr = "Select * From INV_DIVTRANS_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND LTRIM(RTRIM(AUTO_KEY_TRANS))=" & Val(txtReqNo.Text) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsReqMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsReqMain.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Issue Note, Use Generate Issue Note Option To add", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "Select * From INV_DIVTRANS_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " LTRIM(RTRIM(AUTO_KEY_TRANS))=" & Val(mReqnum) & ""

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsReqMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtsubdept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtsubdept.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Function ValidLotNo(ByRef pLotNo As String, ByRef pItemCode As String) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        ValidLotNo = False
        SqlStr = " SELECT ITEM_QTY,LOT_NO " & vbCrLf & " FROM INV_PAINT_STOCK_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ITEM_CODE='" & pItemCode & "'" & vbCrLf & " AND LOT_NO='" & pLotNo & "' AND ITEM_IO='I'" & vbCrLf & " ORDER BY LOT_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            ValidLotNo = True
        End If

        Exit Function
ErrPart:
        ValidLotNo = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
End Class
