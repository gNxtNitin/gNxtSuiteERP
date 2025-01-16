Option Strict Off
Option Explicit On
Imports System.ComponentModel
Imports Microsoft.VisualBasic.Compatibility
'Imports Microsoft.VisualBasic.PowerPacks.Printing.Compatibility.VB6
Friend Class frmStoreRetNote
    Inherits System.Windows.Forms.Form
    Dim RsReqMain As ADODB.Recordset ''Recordset
    Dim RsReqDetail As ADODB.Recordset ''Recordset
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
    Private Const ColFrmStockType As Short = 4
    Private Const ColToStockType As Short = 5
    Private Const ColHeatNo As Short = 6
    Private Const ColLotNo As Short = 7
    Private Const ColStockQty As Short = 8
    Private Const ColReturnQty As Short = 9
    Private Const ColReturnedQty As Short = 10
    Private Const ColSuppCode As Short = 11
    Private Const ColSuppName As Short = 12
    Private Const ColRemarks As Short = 13

    Dim FileDBCn As ADODB.Connection
    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub chkClosed_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkClosed.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkProductionFloor_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkProductionFloor.CheckStateChanged


        Dim xItemCode As String = ""
        Dim xItemUOM As String
        Dim xStockType As String
        Dim mReturnedQty As Double
        Dim CntRow As Integer
        Dim xAutoIssue As Boolean
        Dim xLotNo As String
        Dim mDivisionCode As Double
        Dim mInHouse As Boolean
        Dim mHeatNo As String

        xAutoIssue = CheckAutoIssue((txtSTNDate.Text), "")

        If xAutoIssue = True Then
            Exit Sub
        End If
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)

        If cboDivision.Text = "" And FormActive = True Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        With SprdMain
            For CntRow = 1 To .MaxRows
                .Row = CntRow

                .Col = ColItemCode
                xItemCode = Trim(.Text)
                If xItemCode = "" Then GoTo NextRow

                .Col = ColUom
                xItemUOM = Trim(.Text)

                .Col = ColFrmStockType
                xStockType = Trim(.Text)
                If xStockType = "" Then GoTo NextRow

                SprdMain.Col = ColLotNo
                xLotNo = Trim(SprdMain.Text)

                SprdMain.Col = ColHeatNo
                mHeatNo = Trim(SprdMain.Text)

                If MainClass.ValidateWithMasterTable(xStockType, "STOCK_TYPE_CODE", "STOCK_TYPE_DESC", "INV_TYPE_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                    MsgInformation("InValid Stock Type")
                    MainClass.SetFocusToCell(SprdMain, CntRow, ColFrmStockType)
                    Exit Sub
                Else
                    .Col = ColReturnedQty
                    mReturnedQty = Val(.Text)

                    xAutoIssue = CheckAutoIssue((txtSTNDate.Text), xItemCode)
                    If IsProductionItem(xItemCode) = False Then
                        xAutoIssue = False
                    Else
                        If IsInHouseItem(xItemCode) = True Then
                            mInHouse = True
                        End If
                    End If

                    .Col = ColStockQty
                    '                .Text = mReturnedQty + GetBalanceStockQty(xItemCode, txtSTNDate.Text, xItemUOM, Trim(txtDept.Text), xStockType, "", IIf(lblBookType.text = "P", IIf(xAutoIssue = False, ConPH, ConWH), ConWH))
                    .Text = CStr(GetBalanceStockQty(xItemCode, (txtSTNDate.Text), xItemUOM, Trim(txtDept.Text), xStockType, xLotNo, IIf(lblBookType.Text = "P", IIf(xAutoIssue = False, ConPH, ConWH), ConWH), mDivisionCode, ConStockRefType_SRN, Val(txtSTNNo.Text), , IIf(chkClosed.CheckState = System.Windows.Forms.CheckState.Checked, "C", "O"), mHeatNo))

                    If mInHouse = True And xAutoIssue = True Then
                        SprdMain.Text = CStr(CDbl(SprdMain.Text) + GetBalanceStockQty(xItemCode, (txtSTNDate.Text), xItemUOM, Trim(txtDept.Text), xStockType, xLotNo, ConPH, mDivisionCode, ConStockRefType_SRN, Val(txtSTNNo.Text), , IIf(chkClosed.CheckState = System.Windows.Forms.CheckState.Checked, "C", "O"), mHeatNo))
                    End If

                End If
NextRow:
            Next
        End With

    End Sub

    Private Sub chkStatus_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkStatus.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click

        On Error GoTo AddErr
        If cmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            SprdMain.Enabled = True
            txtSTNNo.Enabled = False
        Else
            cmdAdd.Text = ConCmdAddCaption
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
        Me.Dispose()
        Me.Close()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDelete.Click
        On Error GoTo DelErrPart

        Dim mItemCode As String



        If ValidateBranchLocking((txtSTNDate.Text)) = True Then
            Exit Sub
        End If

        If ValidateBookLocking(PubDBCn, CInt(ConLockSTN), txtSTNDate.Text) = True Then
            Exit Sub
        End If

        If PubUserID <> "G0416" Then
            If chkClosed.CheckState = System.Windows.Forms.CheckState.Checked Then
                Exit Sub
            End If
        End If


        If PubSuperUser = "U" Then
            If chkStatus.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgInformation("Cann't be Deleted.")
                Exit Sub
            End If
        End If

        If Trim(txtSTNNo.Text) = "" Then MsgInformation("Nothing to Delete") : Exit Sub


        If Not RsReqMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "INV_SRN_HDR", (txtSTNNo.Text), RsReqMain, "AUTO_KEY_ISS") = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "INV_SRN_HDR", "AUTO_KEY_SRN", (txtSTNNo.Text)) = False Then GoTo DelErrPart

                If DeleteStockTRN(PubDBCn, ConStockRefType_SRN, (txtSTNNo.Text)) = False Then GoTo DelErrPart

                PubDBCn.Execute("Delete from INV_SRN_DET Where AUTO_KEY_SRN=" & Val(txtSTNNo.Text) & "")
                PubDBCn.Execute("Delete from INV_SRN_HDR Where AUTO_KEY_SRN=" & Val(txtSTNNo.Text) & "")

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
            If chkStatus.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgInformation("Cann't be Modified")
                Exit Sub
            End If
        End If

        If PubUserID <> "G0416" Then
            If chkClosed.CheckState = System.Windows.Forms.CheckState.Checked Then
                Exit Sub
            End If
        End If

        If cmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsReqMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True
            txtSTNNo.Enabled = False
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
        Dim pDeptCode As String
        Dim mTableName As String
        Dim xAutoIssue As Boolean
        Dim mItemCode As String
        Dim mDivisionCode As Double

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If


        MainClass.ClearGrid(SprdMain)

        If Trim(txtDept.Text) = "" Then
            MsgInformation("Please Enter Dept Code.")
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

        pDeptCode = Trim(txtDept.Text)

        If Trim(txtSTNDate.Text) = "" Then
            MsgInformation("Please Enter Date.")
            Exit Sub
        End If

        SqlStr = " SELECT STOCK.ITEM_CODE, ITEM.ITEM_SHORT_DESC, STOCK.ITEM_UOM, " & vbCrLf & " TO_CHAR(SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))) AS STOCKQTY, " & vbCrLf & " ITEM_WEIGHT "

        SqlStr = SqlStr & vbCrLf & " FROM " & mTableName & " STOCK, " & vbCrLf & " INV_ITEM_MST ITEM "

        ''**********WHERE CLAUSE .......*************

        SqlStr = SqlStr & vbCrLf & " Where " & vbCrLf & " STOCK.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND STOCK.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND STOCK.COMPANY_CODE=ITEM.COMPANY_CODE " & vbCrLf & " AND STOCK.ITEM_CODE=ITEM.ITEM_CODE " & vbCrLf & " AND STOCK.STOCK_ID='" & ConPH & "'" & vbCrLf & " AND DEPT_CODE_FROM='" & pDeptCode & "' AND STOCK.DIV_CODE=" & mDivisionCode & "" & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtSTNDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

        If lblBookSubType.Text = "W" Then
            SqlStr = SqlStr & vbCrLf & " AND STOCK.STOCK_TYPE='SC'"
        ElseIf lblBookSubType.Text = "R" Then
            SqlStr = SqlStr & vbCrLf & " AND STOCK.STOCK_TYPE='RS'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND STOCK.STATUS='O'"

        SqlStr = SqlStr & vbCrLf & "GROUP BY STOCK.ITEM_CODE, ITEM.ITEM_SHORT_DESC, STOCK.ITEM_UOM, ITEM_WEIGHT"

        SqlStr = SqlStr & vbCrLf & "HAVING SUM(ITEM_QTY * DECODE(ITEM_IO,'I',1,-1))>0.001"

        SqlStr = SqlStr & vbCrLf & "ORDER BY STOCK.ITEM_CODE, ITEM.ITEM_SHORT_DESC"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        CntRow = 1
        With SprdMain
            If RsTemp.EOF = False Then
                Do While Not RsTemp.EOF



                    mItemCode = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))
                    xAutoIssue = CheckAutoIssue((txtSTNDate.Text), mItemCode)

                    If xAutoIssue = True Then
                        If IsProductionItem(mItemCode) = False Then
                            xAutoIssue = False
                        Else
                            If IsInHouseItem(mItemCode) = True Then
                                xAutoIssue = False
                            End If
                        End If
                    End If

                    If xAutoIssue = False Then
                        .Row = CntRow
                        .Col = ColItemCode
                        .Text = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))

                        .Col = ColItemDesc
                        .Text = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value))

                        .Col = ColUom
                        .Text = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_UOM").Value), "", RsTemp.Fields("ITEM_UOM").Value))

                        .Col = ColFrmStockType
                        .Text = IIf(lblBookSubType.Text = "W", "SC", "RS")

                        .Col = ColToStockType
                        .Text = "SC"

                        .Col = ColStockQty
                        .Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("STOCKQTY").Value), 0, RsTemp.Fields("STOCKQTY").Value), "0.000")

                        .Col = ColReturnQty
                        .Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("STOCKQTY").Value), 0, RsTemp.Fields("STOCKQTY").Value), "0.000")

                        .Col = ColRemarks
                        .Text = ""

                        CntRow = CntRow + 1
                        .MaxRows = CntRow
                    End If


                    RsTemp.MoveNext()
                Loop
            End If
        End With

        FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdPopulatefromExcel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPopulatefromExcel.Click
        On Error GoTo ErrPart
        Dim strFilePath As String

        If Trim(txtDept.Text) = "" Then
            MsgBox("Please select Dept First.")
            Exit Sub
        End If

        If Trim(cboDivision.Text) = "" Then
            MsgBox("Please select Division First.")
            Exit Sub
        End If

        strFilePath = My.Application.Info.DirectoryPath
        If Not fOpenFile(strFilePath, "*.xls", "Excel Data", CommonDialogOpen) Then
            GoTo NormalExit
        End If

        Call PopulateFromXLSFile(strFilePath)

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
NormalExit:
    End Sub
    Private Sub PopulateFromXLSFile(ByVal strXLSFile As String)

        On Error GoTo ErrPart
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mUOM As String = ""
        Dim mStockType As String = ""
        Dim mStockQty As Double
        Dim mReturnQty As Double
        Dim xSqlStr As String
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsFile As ADODB.Recordset
        Dim FileConnStr As String
        Dim mSuppCustCode As String
        Dim strTemp As String
        Dim strWkShName As String
        Dim strError As String = ""
        Dim mDivisionCode As Double
        Dim mHeatNo As String

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        MainClass.ClearGrid(SprdMain)
        FormatSprdMain(-1)


        FileConnStr = "Provider=MSDASQL.1;Connect Timeout=15;Extended Properties='DSN=Excel Files;DBQ=XXLSFILEX;DefaultDir=XXLSDIRX;DriverId=790;FIL=excel 8.0;MaxBufferSize=2048;PageTimeout=5;UID=admin;';Locale Identifier=1033"
        FileConnStr = Replace(FileConnStr, "XXLSFILEX", strXLSFile)
        strTemp = Mid(strXLSFile, 1, InStrRev(strXLSFile, "\") - 1)
        FileConnStr = Replace(FileConnStr, "XXLSDIRX", strTemp)

        If Not XLSConnect(Trim(FileConnStr), FileDBCn) Then
            GoTo ErrPart
        End If

        RsFile = FileDBCn.OpenSchema(ADODB.SchemaEnum.adSchemaTables)
        strWkShName = RsFile.Fields("Table_Name").Value

        mSqlStr = "SELECT * FROM ""XWKSHTX"" " ''WHERE F1 <> NULL"
        mSqlStr = Replace(mSqlStr, "XWKSHTX", strWkShName)

        If OpenExcelRecordSet(mSqlStr, RsFile, strError, FileDBCn, False) = 0 Then

            If RsFile.EOF = False Then
                Do While Not RsFile.EOF
                    mItemCode = Trim(IIf(IsDBNull(RsFile.Fields(0).Value), "", RsFile.Fields(0).Value))

                    xSqlStr = " SELECT ITEM_SHORT_DESC,ISSUE_UOM " & vbCrLf & " FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND LTRIM(RTRIM(ITEM_CODE))='" & MainClass.AllowSingleQuote(mItemCode) & "'"
                    MainClass.UOpenRecordSet(xSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                    If RsTemp.EOF = False Then
                        mItemDesc = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value))
                        mUOM = Trim(IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value))
                    Else
                        GoTo NextRecord
                    End If
                    '                If DuplicateItem = True Then GoTo NextRecord

                    mStockType = Trim(IIf(IsDBNull(RsFile.Fields(3).Value), "", RsFile.Fields(3).Value))

                    mReturnQty = Val(IIf(IsDBNull(RsFile.Fields(4).Value), 0, RsFile.Fields(4).Value))
                    mSuppCustCode = Trim(IIf(IsDBNull(RsFile.Fields(5).Value), "", RsFile.Fields(5).Value))
                    mHeatNo = Trim(IIf(IsDBNull(RsFile.Fields(6).Value), "", RsFile.Fields(6).Value))

                    If mReturnQty = 0 Then GoTo NextRecord
                    mStockQty = GetBalanceStockQty(mItemCode, (txtSTNDate.Text), mUOM, Trim(txtDept.Text), mStockType, "", ConPH, mDivisionCode, , , , IIf(chkClosed.CheckState = System.Windows.Forms.CheckState.Checked, "C", "O"), mHeatNo)

                    SprdMain.Row = SprdMain.MaxRows

                    SprdMain.Col = ColItemCode
                    SprdMain.Text = mItemCode

                    SprdMain.Col = ColItemDesc
                    SprdMain.Text = mItemDesc

                    SprdMain.Col = ColUom
                    SprdMain.Text = mUOM

                    SprdMain.Col = ColFrmStockType
                    SprdMain.Text = mStockType

                    SprdMain.Col = ColToStockType
                    SprdMain.Text = mStockType

                    SprdMain.Col = ColStockQty
                    SprdMain.Text = CStr(mStockQty)

                    SprdMain.Col = ColReturnQty
                    SprdMain.Text = CStr(mReturnQty)

                    SprdMain.Col = ColSuppCode
                    SprdMain.Text = mSuppCustCode

                    SprdMain.MaxRows = SprdMain.MaxRows + 1
                    '               FormatSprdMain -1, False

NextRecord:
                    RsFile.MoveNext()
                Loop
            End If
        End If

        If RsFile.State = ADODB.ObjectStateEnum.adStateOpen Then RsFile.Close()
        RsFile = Nothing

        If FileDBCn.State = ADODB.ObjectStateEnum.adStateOpen Then
            FileDBCn.Close()
            FileDBCn = Nothing
        End If

        '    CmdPopFromFile.Enabled = False
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '        Resume
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportONSRN(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportONSRN(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportONSRN(ByRef Mode As Crystal.DestinationConstants)

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




        mTitle = "STORE RETURN NOTE"
        If lblBookType.Text = "P" Then
            If lblBookSubType.Text = "L" Then
                mTitle = mTitle & "(Line Rejection)"
            ElseIf lblBookSubType.Text = "S" Then
                mTitle = mTitle & "(General Scrap)"
            ElseIf lblBookSubType.Text = "W" Then
                mTitle = mTitle & "(W.I.P. Scrap)"
            ElseIf lblBookSubType.Text = "R" Then
                mTitle = mTitle & "(Rework Scrap)"
            ElseIf lblBookSubType.Text = "O" Then

            End If
        Else
            If lblBookSubType.Text = "F" Then
                mTitle = mTitle & "(FG Scrap - Excisable)"
            End If
        End If
        mSubTitle = ""

        If (lblBookType.Text = "P" And lblBookSubType.Text = "S") Or (lblBookType.Text = "S" And lblBookSubType.Text = "O") Then
            mRptFileName = "SRNScrap.rpt"
            Call SelectQryForSRNScrap(SqlStr)
        Else
            mRptFileName = "SRN.rpt"
            Call SelectQryForSRN(SqlStr)
        End If

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String)
        'Dim Printer As New Printer
        On Error GoTo ErrPart

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle, True, xMyMenu)
        Report1.ReportFileName = PubReportFolderPath & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False

        'Dim prt As Printer
        'If PubUniversalPrinter = "Y" And mMode = Crystal.DestinationConstants.crptToPrinter Then
        '    For Each prt In Printers
        '        If UCase(prt.DeviceName) = UCase("Universal Printer") Then
        '            Printer = prt

        '            Report1.PrinterName = prt.DeviceName
        '            Report1.PrinterDriver = prt.DriverName
        '            Report1.PrinterPort = prt.Port
        '            Report1.PrinterSelect()
        '            Exit For
        '        End If
        '    Next prt
        'End If

        Report1.Action = 1
        Report1.Reset()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Function SelectQryForSRN(ByRef mSqlStr As String) As String

        ''SELECT CLAUSE...

        mSqlStr = " SELECT " & vbCrLf & " IH.*, ID.*, INVMST.ITEM_SHORT_DESC, DEPTMST.DEPT_DESC "


        ''FROM CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " FROM INV_SRN_HDR IH, INV_SRN_DET ID, " & vbCrLf & " INV_ITEM_MST INVMST, PAY_DEPT_MST DEPTMST "

        ''WHERE CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.AUTO_KEY_SRN=ID.AUTO_KEY_SRN" & vbCrLf & " AND IH.COMPANY_CODE=DEPTMST.COMPANY_CODE" & vbCrLf & " AND IH.DEPT_CODE=DEPTMST.DEPT_CODE" & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND INVMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND DEPTMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_SRN=" & Val(txtSTNNo.Text) & ""

        If lblAction.Text = "E" Then
            mSqlStr = mSqlStr & vbCrLf & " AND BOOKTYPE='" & lblBookType.Text & "' AND BOOKSUBTYPE='" & lblBookSubType.Text & "'"
        End If

        ''ORDER CLAUSE...

        mSqlStr = mSqlStr & vbCrLf & "ORDER BY ID.SERIAL_NO"

        SelectQryForSRN = mSqlStr
    End Function

    Private Function SelectQryForSRNScrap(ByRef mSqlStr As String) As String

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mItemCode As String
        Dim mUOM As String = ""
        Dim mItemRate As Double

        Dim mScrapItemCode As String
        Dim mScrapRate As Double

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = " DELETE FROM TEMP_INV_SRN_PRN WHERE USER_ID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = " INSERT INTO TEMP_INV_SRN_PRN (" & vbCrLf & " USER_ID, AUTO_KEY_SRN, COMPANY_CODE," & vbCrLf & " SRN_DATE, DEPT_DESC, EMP_CODE," & vbCrLf & " REMARKS, SERIAL_NO, ITEM_CODE," & vbCrLf & " ITEM_UOM, RTN_QTY, SUPP_CUST_CODE," & vbCrLf & " FROM_STOCK_TYPE, TO_STOCK_TYPE, REASON_DESC," & vbCrLf & " REMARKS_DET, ITEM_WT, ITEM_RATE," & vbCrLf & " SCRAP_ITEM_CODE, SCRAP_RATE,SCRAP_WEIGHT,ITEM_DESC ) "

        SqlStr = SqlStr & vbCrLf & " SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "', IH.AUTO_KEY_SRN, IH.COMPANY_CODE," & vbCrLf & " IH.SRN_DATE, DEPTMST.DEPT_DESC, IH.EMP_CODE, " & vbCrLf & " IH.REMARKS, ID.SERIAL_NO, ID.ITEM_CODE," & vbCrLf & " ID.ITEM_UOM, ID.RTN_QTY, ID.SUPP_CUST_CODE," & vbCrLf & " ID.FROM_STOCK_TYPE, ID.TO_STOCK_TYPE, TRIM(ID.REASON_DESC)," & vbCrLf & " TRIM(ID.REMARKS), INVMST.ITEM_WEIGHT * .001, INVMST.ITEM_STD_COST," & vbCrLf & " DECODE(INVMST1.ITEM_CODE,NULL,'-1',INVMST1.ITEM_CODE), INVMST1.ITEM_STD_COST,INVMST1.ITEM_WEIGHT * .001,INVMST.ITEM_SHORT_DESC "

        ''FROM CLAUSE...
        SqlStr = SqlStr & vbCrLf & " FROM INV_SRN_HDR IH, INV_SRN_DET ID, " & vbCrLf & " INV_ITEM_MST INVMST, PAY_DEPT_MST DEPTMST, INV_ITEM_MST INVMST1 "

        ''WHERE CLAUSE...
        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.AUTO_KEY_SRN=ID.AUTO_KEY_SRN" & vbCrLf & " AND IH.COMPANY_CODE=DEPTMST.COMPANY_CODE" & vbCrLf & " AND IH.DEPT_CODE=DEPTMST.DEPT_CODE" & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND INVMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND DEPTMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_SRN=" & Val(txtSTNNo.Text) & ""

        SqlStr = SqlStr & vbCrLf & " AND INVMST.COMPANY_CODE=INVMST1.COMPANY_CODE(+)" & vbCrLf & " AND INVMST.SCRAP_ITEM_CODE=INVMST1.ITEM_CODE(+)"

        If lblAction.Text = "E" Then
            SqlStr = SqlStr & vbCrLf & " AND BOOKTYPE='" & lblBookType.Text & "' AND BOOKSUBTYPE='" & lblBookSubType.Text & "'"
        End If

        ''ORDER CLAUSE...

        SqlStr = SqlStr & vbCrLf & "ORDER BY ID.SERIAL_NO"
        PubDBCn.Execute(SqlStr)


        SqlStr = " SELECT * FROM TEMP_INV_SRN_PRN WHERE USER_ID='" & MainClass.AllowSingleQuote(PubUserID) & "' ORDER BY SERIAL_NO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False

                mItemCode = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))
                mUOM = IIf(IsDBNull(RsTemp.Fields("ITEM_UOM").Value), "", RsTemp.Fields("ITEM_UOM").Value)

                mScrapItemCode = Trim(IIf(IsDBNull(RsTemp.Fields("SCRAP_ITEM_CODE").Value), "", RsTemp.Fields("SCRAP_ITEM_CODE").Value))
                mScrapRate = 0

                '            mItemRate = GetLatestItemCostFromMRR(mItemCode, mUOM, 1, VB6.Format(txtSTNDate.Text, "DD/MM/YYYY"), "C", "SC", "STR")
                If CheckItemBom(mItemCode) = True Then
                    mItemRate = GetLatestWIPCost(mItemCode, mUOM, 1, VB6.Format(txtSTNDate.Text, "DD/MM/YYYY"), "C", "SC", "STR")
                Else
                    mItemRate = GetLatestItemCostFromMRR(mItemCode, mUOM, 1, VB6.Format(txtSTNDate.Text, "DD/MM/YYYY"), "C", "SC", "STR")
                End If

                If mScrapItemCode <> "" Then
                    mScrapRate = GetLastestScrapRate(mScrapItemCode)
                End If

                If mItemRate <> 0 Then
                    SqlStr = "UPDATE TEMP_INV_SRN_PRN SET ITEM_RATE=" & mItemRate & ", SCRAP_RATE=" & mScrapRate & "" & vbCrLf & " WHERE USER_ID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
                    PubDBCn.Execute(SqlStr)
                End If
                RsTemp.MoveNext()
            Loop
        End If

        PubDBCn.CommitTrans()

        mSqlStr = " SELECT * FROM TEMP_INV_SRN_PRN WHERE USER_ID='" & MainClass.AllowSingleQuote(PubUserID) & "' ORDER BY SERIAL_NO"
        SelectQryForSRNScrap = mSqlStr
        Exit Function
ErrPart:
        '    Resume
        mSqlStr = ""
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans()
    End Function
    Private Function GetLastestScrapRate(ByRef mScrapItemCode As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        GetLastestScrapRate = 0
        SqlStr = " SELECT ITEM_RATE " & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID " & vbCrLf & " WHERE " & vbCrLf & " IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND IH.REF_DESP_TYPE='G'" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mScrapItemCode) & "'" & vbCrLf & " AND IH.INVOICE_DATE = ( " & vbCrLf & " SELECT MAX(IH.INVOICE_DATE)" & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID " & vbCrLf & " WHERE " & vbCrLf & " IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND IH.REF_DESP_TYPE='G'" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mScrapItemCode) & "'" & vbCrLf & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(txtSTNDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetLastestScrapRate = CDbl(Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_RATE").Value), 0, RsTemp.Fields("ITEM_RATE").Value)))
        End If

        Exit Function
ErrPart:
        '    Resume
        GetLastestScrapRate = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)

    End Function
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked And lblBookType.Text = "P" And lblAction.Text = "E" And lblBookSubType.Text = "O" And lblUpdate.Text = "N" Then
            If MsgQuestion("Are you want To approved this Store Return Note also ? ") = CStr(MsgBoxResult.Yes) Then
                chkStatus.CheckState = System.Windows.Forms.CheckState.Checked
            End If
        End If

        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If UpdateMain1() = True Then
            ADDMode = False
            MODIFYMode = False
            txtSTNNo_Validating(txtSTNNo, New System.ComponentModel.CancelEventArgs(False))
            If cmdAdd.Enabled = True Then cmdAdd.Focus()
        Else
            MsgInformation("Record Not saved")
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

    Private Sub cmdDeptSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDeptSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""


        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , SqlStr) = True Then
            txtDept.Text = AcName1
            lblDeptname.Text = AcName
            txtDept_Validating(txtDept, New System.ComponentModel.CancelEventArgs(False))
            If txtDept.Enabled = True Then txtDept.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdCCSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCCSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        If Trim(txtDept.Text) = "" Then
            MsgInformation("Please Select Dept. First.")
            txtDept.Focus()
            Exit Sub
        End If

        SqlStr = " Select IH.CC_CODE, IH.CC_DESC, ID.DEPT_CODE " & vbCrLf & " FROM FIN_CCENTER_HDR IH, FIN_CCENTER_DET ID " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " And IH.COMPANY_CODE=ID.COMPANY_CODE And IH.CC_CODE=ID.CC_CODE" & vbCrLf & " And ID.DEPT_CODE='" & MainClass.AllowSingleQuote((txtDept.Text)) & "'"


        '        If MainClass.SearchGridMaster("", "FIN_CCENTER_HDR", "CC_DESC", "CC_CODE", , , SqlStr) = True Then
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

    Private Sub cmdEmpSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdEmpSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""


        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

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
    Private Sub frmStoreRetNote_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        On Error GoTo ErrPart
        Dim xIName As String
        Dim xSupp As String
        Dim SqlStr As String = ""
        Dim xStockType As String
        Dim xToStockType As String
        Dim mStockTypeIn As String
        Dim mLotNo As String
        Dim mUOM As String = ""
        Dim mItemCode As String
        Dim xAutoIssue As Boolean
        Dim mDept As String
        Dim mHeatNo As String

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If

        If eventArgs.row = 0 And eventArgs.col = ColItemCode Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemCode
                If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
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
                If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
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

        If eventArgs.row = 0 And eventArgs.col = ColFrmStockType Then
            With SprdMain
                .Row = .ActiveRow

                SprdMain.Col = ColToStockType
                xToStockType = Trim(SprdMain.Text)

                mStockTypeIn = "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & ""

                If lblBookType.Text = "S" Then
                    '                If lblBookSubType.text = "F" Then
                    '                    mStockTypeIn = mStockTypeIn & " AND STOCK_TYPE_CODE IN ('QC','SC')" ''sandeep
                    '                Else
                    mStockTypeIn = mStockTypeIn & " AND STOCK_TYPE_CODE NOT IN ('QC','SC')"
                    '                End If
                ElseIf lblBookType.Text = "P" And lblBookSubType.Text = "O" Then
                    mStockTypeIn = mStockTypeIn & " AND STOCK_TYPE_CODE IN ('ST','CS')" ''04-09-2014
                    '                If xToStockType = "RJ" Or xToStockType = "SC" Then
                    '                    mStockTypeIn = mStockTypeIn & " AND STOCK_TYPE_CODE NOT IN ('ST')"
                    '                Else
                    '                    mStockTypeIn = mStockTypeIn & " AND STOCK_TYPE_CODE NOT IN ('QC','SC')"
                    '                End If
                End If

                .Col = ColFrmStockType
                If MainClass.SearchGridMaster("", "INV_TYPE_MST", "STOCK_TYPE_CODE", "STOCK_TYPE_DESC", , , mStockTypeIn) = True Then
                    .Row = .ActiveRow
                    .Col = ColFrmStockType
                    .Text = AcName
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColFrmStockType)
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColToStockType Then
            With SprdMain
                .Row = .ActiveRow
                SprdMain.Col = ColFrmStockType
                xStockType = Trim(SprdMain.Text)

                mStockTypeIn = "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & ""

                If lblBookType.Text = "S" Then
                    mStockTypeIn = mStockTypeIn & " AND STOCK_TYPE_CODE IN ('SC','RJ')"
                ElseIf lblBookType.Text = "P" And lblBookSubType.Text = "O" Then
                    mStockTypeIn = mStockTypeIn & " AND STOCK_TYPE_CODE IN ('ST','CS')" ''04-09-2014
                    '                If xStockType = "ST" Then
                    '                    mStockTypeIn = mStockTypeIn & " AND STOCK_TYPE_CODE NOT IN ('RJ','SC','QC','WR','WP','FG','FC')"
                    '                Else
                    '                    mStockTypeIn = mStockTypeIn & " AND STOCK_TYPE_CODE NOT IN ('ST','QC','WR','WP','FG','FC')"
                    '                End If
                End If

                .Col = ColToStockType
                If MainClass.SearchGridMaster(.Text, "INV_TYPE_MST", "STOCK_TYPE_CODE", "STOCK_TYPE_DESC", , , mStockTypeIn) = True Then
                    .Row = .ActiveRow
                    .Col = ColToStockType
                    .Text = AcName
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColToStockType)
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColHeatNo Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColHeatNo
                mHeatNo = Trim(.Text)

                .Col = ColUom
                mUOM = Trim(.Text)

                .Col = ColFrmStockType
                xStockType = Trim(.Text)

                .Col = ColLotNo
                SqlStr = GetItemHeatWiseQry(mItemCode, (txtSTNDate.Text), mUOM, txtDept.Text, xStockType, mHeatNo, ConPH, ConStockRefType_ISS, Val(txtSTNNo.Text))
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColHeatNo
                    .Text = Trim(AcName1)
                End If

                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColHeatNo)
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

                .Col = ColFrmStockType
                xStockType = Trim(.Text)

                If IsProductionItem(mItemCode) = False Then
                    xAutoIssue = False
                Else
                    If IsInHouseItem(mItemCode) = True Then
                        xAutoIssue = False
                    End If
                End If
                If GetProductionType(mItemCode) = "J" Then
                    '                SprdMain.Text = GetBalanceStockQty(xItemCode, txtSTNDate.Text, xItemUOM, Trim(txtDept.Text), xStockType, xLotNo, ConPH, ConStockRefType_SRN, Val(txtSTNNo.Text))
                    SqlStr = GetItemLotWiseQry(mItemCode, (txtSTNDate.Text), mUOM, Trim(txtDept.Text), xStockType, mLotNo, ConPH, ConStockRefType_SRN, Val(txtSTNNo.Text))
                Else
                    mDept = IIf(lblBookType.Text = "P" And xAutoIssue = False, Trim(txtDept.Text), "STR")
                    '                SprdMain.Text = GetBalanceStockQty(xItemCode, txtSTNDate.Text, xItemUOM, Trim(txtDept.Text), xStockType, xLotNo, IIf(lblBookType.text = "P" And xAutoIssue = False, ConPH, ConWH), ConStockRefType_SRN, Val(txtSTNNo.Text))
                    SqlStr = GetItemLotWiseQry(mItemCode, (txtSTNDate.Text), mUOM, mDept, xStockType, mLotNo, IIf(lblBookType.Text = "P" And xAutoIssue = False, ConPH, ConWH), ConStockRefType_SRN, Val(txtSTNNo.Text))
                End If

                '            SqlStr = GetItemLotWiseQry(mItemCode, txtSTNDate.Text, mUOM, Trim(txtDept.Text), xStockType, mLotNo, ConPH)
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColLotNo
                    .Text = Trim(AcName1)
                End If

                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColLotNo)
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColSuppCode Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemCode
                xIName = Trim(.Text)

                If xIName = "" Then Exit Sub

                SqlStr = " SELECT A.SUPP_CUST_CODE,A.SUPP_CUST_NAME FROM " & vbCrLf & " FIN_SUPP_CUST_MST A, FIN_SUPP_CUST_DET B" & vbCrLf & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf & " AND A.SUPP_CUST_CODE=B.SUPP_CUST_CODE" & vbCrLf & " AND B.ITEM_CODE='" & MainClass.AllowSingleQuote(xIName) & "'"


                If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColSuppCode
                    .Text = AcName
                    .Col = ColSuppName
                    .Text = AcName1

                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColSuppCode)
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColSuppName Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemCode
                xIName = .Text

                If xIName = "" Then Exit Sub


                SqlStr = " SELECT A.SUPP_CUST_NAME,A.SUPP_CUST_CODE FROM " & vbCrLf & " FIN_SUPP_CUST_MST A, FIN_SUPP_CUST_DET B" & vbCrLf & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf & " AND A.SUPP_CUST_CODE=B.SUPP_CUST_CODE" & vbCrLf & " AND B.ITEM_CODE='" & MainClass.AllowSingleQuote(xIName) & "'"


                If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColSuppName
                    .Text = AcName
                    .Col = ColSuppCode
                    .Text = AcName1

                End If

                '            MainClass.ValidateWithMasterTable .Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & ""
                '            .Row = .ActiveRow
                '            .Col = ColSuppCode
                '            .Text = MasterNo
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColSuppCode)
            End With
        End If
        If lblBookType.Text = "P" And (lblBookSubType.Text = "S" Or lblBookSubType.Text = "R") Then
            If eventArgs.row = 0 And eventArgs.col = ColRemarks Then
                With SprdMain
                    .Row = .ActiveRow

                    SprdMain.Col = ColRemarks

                    If MainClass.SearchGridMaster(.Text, "INV_SRNREASON_MST", "SRN_REASON_DESC", , , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        .Row = .ActiveRow
                        .Col = ColRemarks
                        .Text = AcName
                    End If
                    MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColRemarks)
                End With
            End If
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
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SprdMain_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdMain.KeyDownEvent

        Dim mActiveCol As Integer

        mActiveCol = SprdMain.ActiveCol

        If eventArgs.keyCode = System.Windows.Forms.Keys.Return Or eventArgs.keyCode = System.Windows.Forms.Keys.Tab Then
            If mActiveCol = ColReturnQty Then
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColReturnQty
                If Val(SprdMain.Text) <> 0 Then
                    If SprdMain.MaxRows = SprdMain.ActiveRow Then
                        MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight * 1.5)
                        '                    FormatSprdMain SprdMain.MaxRows
                    End If
                End If
                '            SprdMain.Row = SprdMain.MaxRows
            End If
        ElseIf eventArgs.keyCode = System.Windows.Forms.Keys.F1 Then
            If mActiveCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
            If mActiveCol = ColItemDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemDesc, 0))

        End If
        eventArgs.keyCode = 9999
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim xItemCode As String = ""
        Dim xItemUOM As String
        Dim xStockType As String
        Dim xToStockType As String
        Dim mReturnedQty As Double
        Dim mStockTypeIn As String
        Dim xAutoIssue As Boolean
        Dim xLotNo As String
        Dim mDivisionCode As Double
        Dim mInHouse As Boolean
        Dim mProdType As String
        Dim mAutoQCIssue As String
        Dim mHeatNo As String

        xAutoIssue = CheckAutoIssue((txtSTNDate.Text), "")

        If eventArgs.newRow = -1 Then Exit Sub

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        SprdMain.Row = SprdMain.ActiveRow

        Select Case eventArgs.col
            Case ColItemCode
                SprdMain.Col = ColItemCode
                If Trim(SprdMain.Text) = "" Then Exit Sub
                Call FillItemDescFromItemCode((SprdMain.Text), mDivisionCode)
                If DuplicateItem(ColItemCode) = False Then
                    MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                    FormatSprdMain((SprdMain.MaxRows))
                    '                FormatSprdMain -1
                    '                MainClass.SetFocusToCell SprdMain, Row, ColFrmStockType
                End If

            Case ColItemDesc
                SprdMain.Col = ColItemDesc
                Call FillItemDescFromItemDesc((SprdMain.Text), mDivisionCode)
                If DuplicateItem(ColItemCode) = False Then
                End If
            Case ColReturnQty
                If CheckQty() = True Then
                    SprdMain.Row = SprdMain.ActiveRow
                    SprdMain.Col = ColReturnQty
                    '                If Val(SprdMain.Text) <> 0 Then
                    '                    If SprdMain.MaxRows = SprdMain.ActiveRow Then
                    '                        MainClass.AddBlankSprdRow SprdMain, ColItemCode, ConRowHeight
                    ''                        FormatSprdMain SprdMain.MaxRows
                    '                        FormatSprdMain -1
                    '                    End If
                    '                End If
                End If
            Case ColFrmStockType
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColItemCode
                xItemCode = Trim(SprdMain.Text)

                SprdMain.Col = ColUom
                xItemUOM = Trim(SprdMain.Text)

                SprdMain.Col = ColToStockType
                xToStockType = Trim(SprdMain.Text)

                SprdMain.Col = ColFrmStockType
                xStockType = Trim(SprdMain.Text)
                If xStockType = "" Then Exit Sub

                If xStockType = "CS" Then
                    If Trim(xToStockType) <> "" Then
                        If Trim(xToStockType) <> "CS" Then
                            MsgInformation("Invalid Stock Type. To Stock Type should be Same as From Stock Type.")
                            MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColFrmStockType)
                            Exit Sub
                        End If
                    End If
                End If

                mStockTypeIn = "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & ""

                If lblBookType.Text = "S" Then
                    mStockTypeIn = mStockTypeIn & " AND STOCK_TYPE_CODE NOT IN ('QC','SC')"
                ElseIf lblBookType.Text = "P" And lblBookSubType.Text = "O" Then
                    mStockTypeIn = mStockTypeIn & " AND STOCK_TYPE_CODE IN ('ST','CS')" ''04-09-2014
                    '                If xToStockType = "RJ" Or xToStockType = "SC" Then
                    '                    mStockTypeIn = mStockTypeIn & " AND STOCK_TYPE_CODE NOT IN ('ST','CR','FG')"
                    '                Else
                    '                    mStockTypeIn = mStockTypeIn & " AND STOCK_TYPE_CODE NOT IN ('QC','SC')"
                    '                End If
                End If

                mInHouse = False
                If MainClass.ValidateWithMasterTable(xStockType, "STOCK_TYPE_CODE", "STOCK_TYPE_DESC", "INV_TYPE_MST", PubDBCn, MasterNo, , mStockTypeIn) = False Then
                    MsgInformation("InValid Stock Type")
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColFrmStockType)
                    eventArgs.cancel = True
                    Exit Sub
                Else
                    SprdMain.Col = ColLotNo
                    xLotNo = Trim(SprdMain.Text)

                    SprdMain.Col = ColLotNo
                    mHeatNo = Trim(SprdMain.Text)


                    SprdMain.Col = ColReturnedQty
                    mReturnedQty = Val(SprdMain.Text)

                    SprdMain.Col = ColStockQty ''mReturnedQty +
                    If IsProductionItem(xItemCode) = False Then
                        xAutoIssue = False
                    Else
                        If IsInHouseItem(xItemCode) = True Then
                            mInHouse = True
                        End If
                    End If
                    If GetProductionType(xItemCode) = "J" Then
                        SprdMain.Text = CStr(GetBalanceStockQty(xItemCode, (txtSTNDate.Text), xItemUOM, Trim(txtDept.Text), xStockType, xLotNo, ConPH, mDivisionCode, ConStockRefType_SRN, Val(txtSTNNo.Text), , IIf(chkClosed.CheckState = System.Windows.Forms.CheckState.Checked, "C", "O"), mHeatNo))
                    Else
                        SprdMain.Text = CStr(GetBalanceStockQty(xItemCode, (txtSTNDate.Text), xItemUOM, Trim(txtDept.Text), xStockType, xLotNo, IIf(lblBookType.Text = "P" And xAutoIssue = False, ConPH, ConWH), mDivisionCode, ConStockRefType_SRN, Val(txtSTNNo.Text), , IIf(chkClosed.CheckState = System.Windows.Forms.CheckState.Checked, "C", "O"), mHeatNo))
                        If mInHouse = True And xAutoIssue = True Then
                            SprdMain.Text = CStr(CDbl(SprdMain.Text) + GetBalanceStockQty(xItemCode, (txtSTNDate.Text), xItemUOM, Trim(txtDept.Text), xStockType, xLotNo, ConPH, mDivisionCode, ConStockRefType_SRN, Val(txtSTNNo.Text), mHeatNo))
                        End If
                    End If
                End If

            Case ColToStockType
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColFrmStockType
                xStockType = Trim(SprdMain.Text)

                SprdMain.Col = ColToStockType
                If Trim(SprdMain.Text) = "" Then Exit Sub

                If xStockType = "CS" Then
                    If Trim(SprdMain.Text) <> "CS" Then
                        MsgInformation("Invalid Stock Type. To Stock Type should be Same as From Stock Type.")
                        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColToStockType)
                        Exit Sub
                    End If
                End If
                mStockTypeIn = "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & ""

                If lblBookType.Text = "S" Then
                    mStockTypeIn = mStockTypeIn & " AND STOCK_TYPE_CODE IN ('SC','RJ')"
                ElseIf lblBookType.Text = "P" And lblBookSubType.Text = "O" Then
                    mStockTypeIn = mStockTypeIn & " AND STOCK_TYPE_CODE IN ('ST','CS')" ''04-09-2014
                    '                If xStockType = "ST" Then
                    '                    mStockTypeIn = mStockTypeIn & " AND STOCK_TYPE_CODE NOT IN ('RJ','SC','QC','WR','WP','FG','FC','CR')"
                    '                Else
                    '                    mStockTypeIn = mStockTypeIn & " AND STOCK_TYPE_CODE NOT IN ('ST','QC','WR','WP','FG','FC','CR')"
                    '                End If
                End If

                If MainClass.ValidateWithMasterTable(SprdMain.Text, "STOCK_TYPE_CODE", "STOCK_TYPE_DESC", "INV_TYPE_MST", PubDBCn, MasterNo, , mStockTypeIn) = False Then
                    MsgInformation("InValid Stock Type")
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColToStockType)
                    Exit Sub
                End If
            Case ColSuppCode
                SprdMain.Col = ColSuppCode
                If Trim(SprdMain.Text) = "" Then Exit Sub
                If MainClass.ValidateWithMasterTable(SprdMain.Text, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    SprdMain.Col = ColSuppName
                    SprdMain.Text = MasterNo
                    '                Call FillSuppname(SprdMain.Text)
                Else
                    FormatSprdMain(-1)
                    MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColSuppCode)
                End If
            Case ColRemarks
                If lblBookType.Text = "P" And (lblBookSubType.Text = "S" Or lblBookSubType.Text = "R") Then
                    SprdMain.Row = SprdMain.ActiveRow
                    SprdMain.Col = ColItemCode
                    xItemCode = Trim(SprdMain.Text)

                    mProdType = GetProductionType(xItemCode)
                    If mProdType = "P" Or mProdType = "T" Or mProdType = "D" Or mProdType = "R" Or mProdType = "B" Or mProdType = "I" Or mProdType = "3" Then
                        SprdMain.Col = ColRemarks
                        If Trim(SprdMain.Text) = "" Then Exit Sub

                        If MainClass.ValidateWithMasterTable(SprdMain.Text, "SRN_REASON_DESC", "SRN_REASON_DESC", "INV_SRNREASON_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                            MsgInformation("Please select the valid reason.")
                            MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColRemarks)
                            Exit Sub
                        End If
                    End If
                End If

            Case ColHeatNo
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColItemCode
                xItemCode = Trim(SprdMain.Text)

                If MainClass.ValidateWithMasterTable(xItemCode, "ITEM_CODE", "HEAT_NO_REQ", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND HEAT_NO_REQ='Y'") = True Then
                    mAutoQCIssue = "Y"
                Else
                    mAutoQCIssue = "N"
                End If

                SprdMain.Col = ColHeatNo
                If Trim(SprdMain.Text) = "" And mAutoQCIssue = "Y" Then
                    MsgInformation("Heat No is Must for this Item.")
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColHeatNo)
                    eventArgs.cancel = True
                    Exit Sub
                ElseIf Trim(SprdMain.Text) <> "" And mAutoQCIssue = "N" Then
                    MsgInformation("Heat No is not required for this Item.")
                    SprdMain.Text = ""
                    'MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColStockType)
                    eventArgs.cancel = True
                    Exit Sub
                End If

            Case ColLotNo
                If DuplicateItem(ColLotNo) = False Then
                    SprdMain.Row = SprdMain.ActiveRow

                    SprdMain.Col = ColItemCode
                    xItemCode = Trim(SprdMain.Text)

                    SprdMain.Col = ColUom
                    xItemUOM = Trim(SprdMain.Text)

                    SprdMain.Col = ColHeatNo
                    mHeatNo = Trim(SprdMain.Text)

                    SprdMain.Col = ColLotNo
                    xLotNo = Trim(SprdMain.Text)
                    xLotNo = IIf(xLotNo = "0", "", xLotNo)

                    '                If xLotNo <> "" Then
                    '                    If MainClass.ValidateWithMasterTable(xLotNo, "LOT_NO", "LOT_NO", "INV_GATE_DET", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND ITEM_CODE='" & xItemCode & "'") = False Then
                    '                        MsgInformation "InValid Lot No"
                    '                        MainClass.SetFocusToCell SprdMain, Row, ColLotNo
                    '                        Exit Sub
                    '                    End If
                    '                End If

                    SprdMain.Col = ColFrmStockType
                    xStockType = Trim(SprdMain.Text)
                    If xStockType = "" Then Exit Sub
                    mInHouse = False
                    SprdMain.Col = ColStockQty ''mReturnedQty +
                    If IsProductionItem(xItemCode) = False Then
                        xAutoIssue = False
                    Else
                        If IsInHouseItem(xItemCode) = True Then
                            mInHouse = True
                        End If
                    End If
                    If GetProductionType(xItemCode) = "J" Then
                        SprdMain.Text = CStr(GetBalanceStockQty(xItemCode, (txtSTNDate.Text), xItemUOM, Trim(txtDept.Text), xStockType, xLotNo, ConPH, mDivisionCode, ConStockRefType_SRN, Val(txtSTNNo.Text), , IIf(chkClosed.CheckState = System.Windows.Forms.CheckState.Checked, "C", "O"), mHeatNo))
                    Else
                        SprdMain.Text = CStr(GetBalanceStockQty(xItemCode, (txtSTNDate.Text), xItemUOM, Trim(txtDept.Text), xStockType, xLotNo, IIf(lblBookType.Text = "P" And xAutoIssue = False, ConPH, ConWH), mDivisionCode, ConStockRefType_SRN, Val(txtSTNNo.Text), , IIf(chkClosed.CheckState = System.Windows.Forms.CheckState.Checked, "C", "O"), mHeatNo))
                        If mInHouse = True And xAutoIssue = True Then
                            SprdMain.Text = CStr(CDbl(SprdMain.Text) + GetBalanceStockQty(xItemCode, (txtSTNDate.Text), xItemUOM, Trim(txtDept.Text), xStockType, xLotNo, ConPH, mDivisionCode, ConStockRefType_SRN, Val(txtSTNNo.Text), , IIf(chkClosed.CheckState = System.Windows.Forms.CheckState.Checked, "C", "O"), mHeatNo))
                        End If
                    End If


                End If
        End Select
        '    FormatSprdMain -1
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Function DuplicateItem(ByRef pCol As Integer) As Boolean

        Dim CntRow As Integer
        Dim mCount As Byte
        Dim mCheckItemCode As String
        Dim mItemCode As String
        Dim mLotNo As String
        Dim mCheckLotNo As String

        With SprdMain
            .Row = .ActiveRow
            .Col = ColItemCode
            mCheckItemCode = Trim(UCase(.Text))

            .Col = ColLotNo
            mCheckLotNo = Trim(UCase(.Text))

            For CntRow = 1 To .MaxRows
                .Row = CntRow
                .Col = ColItemCode
                mItemCode = Trim(UCase(.Text))

                .Col = ColLotNo
                mLotNo = Trim(UCase(.Text))

                If (mItemCode & ":" & mLotNo = mCheckItemCode & ":" & mCheckLotNo And mCheckItemCode <> "") Then
                    mCount = mCount + 1
                End If

                '            If (mItemCode = mCheckItemCode And mCheckItemCode <> "") Then
                '                mCount = mCount + 1
                '            End If

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
        With SprdMain
            .Row = .ActiveRow
            .Col = ColItemCode
            If Trim(.Text) = "" Then Exit Function

            .Col = ColReturnQty
            If Val(.Text) > 0 Then
                CheckQty = True
            Else
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColReturnQty)
            End If
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Sub FillItemDescFromItemCode(ByRef pItemCode As String, ByRef mDivisionCode As Double)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xStockType As String
        Dim xItemUOM As String
        Dim xAutoIssue As Boolean
        Dim xLotNo As String
        Dim mInHouse As Boolean
        Dim mHeatNo As String

        If Trim(pItemCode) = "" Then Exit Sub
        With SprdMain
            SqlStr = "SELECT ITEM_SHORT_DESC,ISSUE_UOM " & vbCrLf & " FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND LTRIM(RTRIM(ITEM_CODE))='" & MainClass.AllowSingleQuote(pItemCode) & "'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                .Row = .ActiveRow
                .Col = ColItemDesc
                .Text = IIf(IsDBNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value)

                .Col = ColUom
                .Text = IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)
                xItemUOM = IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)

                If lblBookType.Text = "P" And lblBookSubType.Text = "L" Then
                    .Col = ColFrmStockType
                    .Text = "ST"
                    xStockType = "ST"

                    .Col = ColToStockType
                    .Text = "RJ"

                ElseIf lblBookType.Text = "P" And lblBookSubType.Text = "S" Then
                    .Col = ColFrmStockType
                    .Text = "ST"
                    xStockType = "ST"

                    .Col = ColToStockType
                    .Text = "SC"
                ElseIf lblBookType.Text = "P" And lblBookSubType.Text = "W" Then
                    .Col = ColFrmStockType
                    .Text = "SC"
                    xStockType = "SC"

                    .Col = ColToStockType
                    .Text = "SC"
                ElseIf lblBookType.Text = "P" And lblBookSubType.Text = "R" Then
                    .Col = ColFrmStockType
                    .Text = "RS"
                    xStockType = "RS"

                    .Col = ColToStockType
                    .Text = "SC"
                ElseIf lblBookType.Text = "S" And lblBookSubType.Text = "F" Then
                    .Col = ColFrmStockType
                    .Text = "FG"
                    xStockType = "FG"

                    .Col = ColToStockType
                    .Text = "SC"
                ElseIf lblBookType.Text = "S" And lblBookSubType.Text = "C" Then
                    .Col = ColFrmStockType
                    .Text = "CR"
                    xStockType = "CR"

                    .Col = ColToStockType
                    .Text = "SC"
                    '            ElseIf lblBookType.text = "S" And lblBookType.text = "S" Then
                    '                .Col = ColFrmStockType
                    '                .Text = "RJ"
                    '                xStockType = "RJ"
                    '
                    '                .Col = ColToStockType
                    '                .Text = "SC"
                Else
                    .Col = ColFrmStockType
                    xStockType = Trim(.Text)
                End If

                SprdMain.Col = ColLotNo
                xLotNo = Trim(SprdMain.Text)

                SprdMain.Col = ColHeatNo
                mHeatNo = Trim(SprdMain.Text)

                mInHouse = False

                If xStockType <> "" Then
                    If GetProductionType(pItemCode) = "J" Then
                        xAutoIssue = False
                    Else
                        xAutoIssue = CheckAutoIssue((txtSTNDate.Text), pItemCode)
                    End If
                    If IsProductionItem(pItemCode) = False Then
                        xAutoIssue = False
                    Else
                        If IsInHouseItem(pItemCode) = True Then
                            mInHouse = True
                        End If
                    End If

                    SprdMain.Col = ColStockQty ''mReturnedQty +
                    SprdMain.Text = CStr(GetBalanceStockQty(pItemCode, (txtSTNDate.Text), xItemUOM, Trim(txtDept.Text), xStockType, xLotNo, IIf(lblBookType.Text = "P", IIf(xAutoIssue = False, ConPH, ConWH), ConWH), mDivisionCode, ConStockRefType_SRN, Val(txtSTNNo.Text), , IIf(chkClosed.CheckState = System.Windows.Forms.CheckState.Checked, "C", "O"), mHeatNo))
                    If mInHouse = True Then
                        SprdMain.Text = CStr(Val(SprdMain.Text) + GetBalanceStockQty(pItemCode, (txtSTNDate.Text), xItemUOM, Trim(txtDept.Text), xStockType, xLotNo, ConPH, mDivisionCode, ConStockRefType_SRN, Val(txtSTNNo.Text), , IIf(chkClosed.CheckState = System.Windows.Forms.CheckState.Checked, "C", "O"), mHeatNo))
                    End If
                End If

            Else
                '            MsgInformation "Invaild Item Code"
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColItemCode)
            End If
        End With
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub FillItemDescFromItemDesc(ByRef pItemDesc As String, ByRef mDivisionCode As Double)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xStockType As String
        Dim xItemUOM As String
        Dim xItemCode As String = ""
        Dim xAutoIssue As Boolean
        Dim xLotNo As String
        Dim mInHouse As Boolean
        Dim mHeatNo As String

        '    xAutoIssue = CheckAutoIssue(txtSTNDate.Text)

        If Trim(pItemDesc) = "" Then Exit Sub
        With SprdMain
            SqlStr = "SELECT ITEM_CODE,CUSTOMER_PART_NO,ISSUE_UOM " & vbCrLf & " FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND LTRIM(RTRIM(ITEM_SHORT_DESC))='" & MainClass.AllowSingleQuote(pItemDesc) & "'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                .Row = .ActiveRow
                .Col = ColItemCode
                .Text = IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)
                xItemCode = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))

                '            .Col = ColPartNo
                '            .Text = IIf(IsNull(RsTemp!CUSTOMER_PART_NO), "", RsTemp!CUSTOMER_PART_NO)

                .Col = ColUom
                .Text = IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)
                xItemUOM = Trim(IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value))

                If lblBookType.Text = "P" And lblBookSubType.Text = "L" Then
                    .Col = ColFrmStockType
                    .Text = "ST"
                    xStockType = "ST"

                    .Col = ColToStockType
                    .Text = "RJ"

                ElseIf lblBookType.Text = "P" And lblBookSubType.Text = "S" Then
                    .Col = ColFrmStockType
                    .Text = "ST"
                    xStockType = "ST"

                    .Col = ColToStockType
                    .Text = "SC"
                ElseIf lblBookType.Text = "P" And lblBookSubType.Text = "W" Then
                    .Col = ColFrmStockType
                    .Text = "SC"
                    xStockType = "SC"

                    .Col = ColToStockType
                    .Text = "SC"
                ElseIf lblBookType.Text = "P" And lblBookSubType.Text = "R" Then
                    .Col = ColFrmStockType
                    .Text = "RS"
                    xStockType = "RS"

                    .Col = ColToStockType
                    .Text = "SC"
                    '            ElseIf lblBookType.text = "S" And lblBookType.text = "S" Then
                    '                .Col = ColFrmStockType
                    '                .Text = "RJ"
                    '                xStockType = "RJ"
                    '
                    '                .Col = ColToStockType
                    '                .Text = "SC"
                ElseIf lblBookType.Text = "S" And lblBookSubType.Text = "F" Then
                    .Col = ColFrmStockType
                    .Text = "FG"
                    xStockType = "FG"

                    .Col = ColToStockType
                    .Text = "SC"
                ElseIf lblBookType.Text = "S" And lblBookSubType.Text = "C" Then
                    .Col = ColFrmStockType
                    .Text = "CR"
                    xStockType = "CR"

                    .Col = ColToStockType
                    .Text = "SC"
                Else
                    .Col = ColFrmStockType
                    xStockType = Trim(.Text)
                End If

                SprdMain.Col = ColLotNo
                xLotNo = Trim(SprdMain.Text)

                SprdMain.Col = ColHeatNo
                mHeatNo = Trim(SprdMain.Text)



                mInHouse = False

                If xStockType <> "" Then
                    If GetProductionType(xItemCode) = "J" Then
                        xAutoIssue = False
                    Else
                        xAutoIssue = CheckAutoIssue((txtSTNDate.Text), xItemCode)
                    End If
                    If IsProductionItem(xItemCode) = False Then
                        xAutoIssue = False
                    Else
                        If IsInHouseItem(xItemCode) = True Then
                            mInHouse = True
                        End If
                    End If
                    SprdMain.Col = ColStockQty ''mReturnedQty +
                    SprdMain.Text = CStr(GetBalanceStockQty(xItemCode, (txtSTNDate.Text), xItemUOM, Trim(txtDept.Text), xStockType, xLotNo, IIf(lblBookType.Text = "P", IIf(xAutoIssue = False, ConPH, ConWH), ConWH), mDivisionCode, ConStockRefType_SRN, Val(txtSTNNo.Text), , IIf(chkClosed.CheckState = System.Windows.Forms.CheckState.Checked, "C", "O"), mHeatNo))

                    If mInHouse = True And xAutoIssue = True Then
                        SprdMain.Text = CStr(CDbl(SprdMain.Text) + GetBalanceStockQty(xItemCode, (txtSTNDate.Text), xItemUOM, Trim(txtDept.Text), xStockType, xLotNo, ConPH, mDivisionCode, ConStockRefType_SRN, Val(txtSTNNo.Text), , IIf(chkClosed.CheckState = System.Windows.Forms.CheckState.Checked, "C", "O"), mHeatNo))
                    End If
                End If

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
            txtSTNNo.Text = .Text
            txtSTNNo_Validating(txtSTNNo, New System.ComponentModel.CancelEventArgs(False))
            If txtSTNNo.Enabled = True Then txtSTNNo.Focus()
            CmdView_Click(CmdView, New System.EventArgs())
        End With
    End Sub
    Private Function AutoGenSeqNo() As String

        On Error GoTo AutoGenSeqNoErr
        Dim RsMRRMainGen As ADODB.Recordset = Nothing
        Dim mNewSeqNo As Integer
        Dim SqlStr As String = ""
        Dim mMaxValue As String

        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_SRN)  " & vbCrLf & " FROM INV_SRN_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_SRN,LENGTH(AUTO_KEY_SRN)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

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
        Dim mProdFloor As String
        'Dim mStatus As String
        Dim mDivisionCode As Double
        Dim mClosed As String
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()


        If MainClass.ValidateWithMasterTable((cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        mProdFloor = IIf(chkProductionFloor.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        ''RsCompany.fields("COMPANY_CODE").value = 3 And
        '    If lblBookType.text = "P" And lblBookSubType.text = "S" Then
        '        chkStatus.Value = vbChecked
        '    End If

        mClosed = IIf(chkClosed.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        mStatus = IIf(chkStatus.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")


        If Val(txtSTNNo.Text) = 0 Then
            mVNoSeq = CDbl(AutoGenSeqNo())
        Else
            mVNoSeq = Val(txtSTNNo.Text)
        End If

        txtSTNNo.Text = CStr(Val(CStr(mVNoSeq)))

        SqlStr = ""
        If ADDMode = True Then
            lblMKey.Text = CStr(mVNoSeq)
            SqlStr = "INSERT INTO INV_SRN_HDR (" & vbCrLf & " AUTO_KEY_SRN, " & vbCrLf & " COMPANY_CODE, " & vbCrLf & " SRN_DATE, " & vbCrLf & " DEPT_CODE, " & vbCrLf & " EMP_CODE, COST_CENTER_CODE,OPR_CODE,REMARKS, PRD_FLOOR,  " & vbCrLf & " ACTIONTAKEN, STATUS," & vbCrLf & " BOOKTYPE, BOOKSUBTYPE," & vbCrLf & " ADDUSER,ADDDATE,MODUSER,MODDATE,DIV_CODE,CLOSED_SRN)" & vbCrLf & " VALUES( " & vbCrLf & " " & Val(CStr(mVNoSeq)) & "," & vbCrLf & " " & RsCompany.Fields("Company_Code").Value & "," & vbCrLf & " TO_DATE('" & VB6.Format(txtSTNDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & MainClass.AllowSingleQuote((txtDept.Text)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtEmp.Text)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtCost.Text)) & "', " & vbCrLf _
                & " '', " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtRemarks.Text)) & "', '" & mProdFloor & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtAction.Text)) & "', '" & mStatus & "', " & vbCrLf & " '" & lblBookType.Text & "', '" & lblBookSubType.Text & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'',''," & mDivisionCode & ",'" & mClosed & "')"

        ElseIf MODIFYMode = True Then
            SqlStr = ""
            SqlStr = "UPDATE INV_SRN_HDR SET " & vbCrLf & " DEPT_CODE='" & txtDept.Text & "',  SRN_DATE=TO_DATE('" & VB6.Format(txtSTNDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " EMP_CODE ='" & txtEmp.Text & "', " & vbCrLf & " REMARKS ='" & MainClass.AllowSingleQuote((txtRemarks.Text)) & "'," & vbCrLf & " COST_CENTER_CODE ='" & txtCost.Text & "'," & vbCrLf & " ACTIONTAKEN ='" & MainClass.AllowSingleQuote((txtAction.Text)) & "'," & vbCrLf & " STATUS ='" & mStatus & "'," & vbCrLf _
                & " OPR_CODE =''," & vbCrLf & " PRD_FLOOR='" & mProdFloor & "', DIV_CODE=" & mDivisionCode & ", CLOSED_SRN='" & mClosed & "',"

            If lblAction.Text = "E" Then
                SqlStr = SqlStr & vbCrLf & " BOOKTYPE='" & lblBookType.Text & "', " & vbCrLf & " BOOKSUBTYPE='" & lblBookSubType.Text & "', "
            End If

            SqlStr = SqlStr & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND AUTO_KEY_SRN =" & Val(lblMKey.Text) & ""
        End If

        PubDBCn.Execute(SqlStr)

        If UpdateDetail1(CStr(Val(CStr(mVNoSeq))), mDivisionCode) = False Then GoTo ErrPart

        UpdateMain1 = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        UpdateMain1 = False
        PubDBCn.RollbackTrans() ''
        RsReqMain.Requery() ''.Refresh
        RsReqDetail.Requery() ''.Refresh
        If Err.Description = "" Then Exit Function
        If Err.Number = -2147217900 Then
            ErrorMsg("Duplicate Item Consumption Generated, Save Again", "Duplicate", MsgBoxStyle.Critical)
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
        Dim mFrmStktype As String
        Dim mToStktype As String
        Dim mReturnQty As Double
        Dim mSuppCode As String
        Dim mRemarks As String
        Dim pScrapItemCode As String
        Dim pScrapUOM As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mProdType As String
        Dim mNarration As String
        Dim xAutoIssue As Boolean
        Dim mLotNo As String
        Dim mTariffCode As String
        Dim mHeatNo As String

        xAutoIssue = CheckAutoIssue((txtSTNDate.Text), "")

        SqlStr = " Delete From INV_SRN_DET " & vbCrLf & " WHERE AUTO_KEY_SRN=" & Val(lblMKey.Text) & ""
        PubDBCn.Execute(SqlStr)

        '    PubDBCn.Execute "Delete From FIN_RGDAILYMANU_HDR Where Mkey='" & Val(lblMKey.text) & "' AND BOOKTYPE='R'"

        If DeleteStockTRN(PubDBCn, ConStockRefType_SRN, (txtSTNNo.Text)) = False Then GoTo UpdateDetail1

        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = ColItemCode
                mItemCode = Trim(MainClass.AllowSingleQuote(.Text))

                MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "TARIFF_CODE", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mTariffCode = MasterNo

                .Col = ColUom
                mUOM = MainClass.AllowSingleQuote(.Text)

                .Col = ColFrmStockType
                mFrmStktype = MainClass.AllowSingleQuote(.Text)

                .Col = ColToStockType
                mToStktype = MainClass.AllowSingleQuote(.Text)

                .Col = ColHeatNo
                mHeatNo = Trim(.Text)

                .Col = ColLotNo
                mLotNo = Trim(.Text)

                .Col = ColReturnQty
                mReturnQty = Val(.Text)

                .Col = ColSuppCode
                mSuppCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColRemarks
                mRemarks = MainClass.AllowSingleQuote(.Text)

                mProdType = GetProductionType(mItemCode)

                SqlStr = ""

                If mItemCode <> "" And mReturnQty > 0 Then
                    SqlStr = " INSERT INTO INV_SRN_DET ( " & vbCrLf _
                            & " AUTO_KEY_SRN,SERIAL_NO,ITEM_CODE,ITEM_UOM,RTN_QTY," & vbCrLf _
                            & " SUPP_CUST_CODE,FROM_STOCK_TYPE,TO_STOCK_TYPE,REMARKS,COMPANY_CODE, LOT_NO,HEAT_NO) "

                    SqlStr = SqlStr & vbCrLf _
                        & " VALUES (" & Val(lblMKey.Text) & ", " & I & "," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mItemCode) & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mUOM) & "', " & vbCrLf _
                        & " " & mReturnQty & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(mSuppCode) & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mFrmStktype) & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mToStktype) & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mRemarks) & "'," & RsCompany.Fields("COMPANY_CODE").Value & ",'" & mLotNo & "','" & MainClass.AllowSingleQuote(mHeatNo) & "') "

                    PubDBCn.Execute(SqlStr)
                End If

                If chkStatus.CheckState = System.Windows.Forms.CheckState.Checked Then
                    If lblBookType.Text = "P" And lblBookSubType.Text = "L" Then
                        mNarration = "Line Rejection (To Store)"
                    ElseIf lblBookType.Text = "S" And lblBookSubType.Text = "F" Then
                        mNarration = "FG Scrap (To Store)"
                    ElseIf lblBookType.Text = "S" And lblBookSubType.Text = "C" Then
                        mNarration = "CR Scrap (To Store)"
                    ElseIf lblBookType.Text = "P" And lblBookSubType.Text = "S" Then
                        mNarration = "Scrap Return (To Store)"
                    ElseIf lblBookType.Text = "P" And lblBookSubType.Text = "W" Then
                        mNarration = "WIP Scrap Return (To Store)"
                    ElseIf lblBookType.Text = "P" And lblBookSubType.Text = "R" Then
                        mNarration = "Rework Scrap Return (To Store)"
                    ElseIf lblBookType.Text = "S" And lblBookSubType.Text = "O" Then
                        mNarration = "Store Stock Convertion (" & mToStktype & " STOCK)"
                    Else
                        mNarration = "To : STORE (" & mToStktype & " STOCK)"
                    End If

                    If chkProductionFloor.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                        If UpdateStockTRN(PubDBCn, ConStockRefType_SRN, (txtSTNNo.Text), I, (txtSTNDate.Text), (txtSTNDate.Text), mFrmStktype, mItemCode, mUOM, mLotNo, mReturnQty, 0, "O", 0, 0, "", "", (txtDept.Text), "STR", "", "N", UCase(mNarration), mSuppCode, ConWH, mDivisionCode, Trim(lblBookSubType.Text), "", IIf(chkClosed.CheckState = System.Windows.Forms.CheckState.Checked, "C", "O"), mHeatNo) = False Then GoTo UpdateDetail1
                    Else
                        If mProdType = "P" Or mProdType = "T" Or mProdType = "J" Or mProdType = "C" Or mProdType = "R" Or mProdType = "B" Or mProdType = "I" Or mProdType = "3" Or mProdType = "4" Or (mProdType = "G" And mFrmStktype = "SC") Then
                            If xAutoIssue = True And IsProductionItem(mItemCode) = True Then
                                If mProdType = "J" Then
                                    GoTo NextLine
                                Else
                                    If IsInHouseItem(mItemCode) = True Then
                                        If xAutoIssue = True Then
                                            If GetProductFinalDept(mItemCode, (txtSTNDate.Text)) = "STR" Or GetProductFinalDept(mItemCode, (txtSTNDate.Text)) = "" Then
                                                GoTo UpdateWH
                                            Else
                                                GoTo NextLine
                                            End If
                                        Else
                                            GoTo NextLine
                                        End If
                                    Else
UpdateWH:
                                        If UpdateStockTRN(PubDBCn, ConStockRefType_SRN, (txtSTNNo.Text), I, (txtSTNDate.Text), (txtSTNDate.Text), mFrmStktype, mItemCode, mUOM, mLotNo, mReturnQty, 0, "O", 0, 0, "", "", (txtDept.Text), "STR", "", "N", UCase(mNarration), mSuppCode, ConWH, mDivisionCode, Trim(lblBookSubType.Text), "", IIf(chkClosed.CheckState = System.Windows.Forms.CheckState.Checked, "C", "O"), mHeatNo) = False Then GoTo UpdateDetail1
                                    End If
                                End If
                            Else
NextLine:
                                If UpdateStockTRN(PubDBCn, ConStockRefType_SRN, (txtSTNNo.Text), I, (txtSTNDate.Text), (txtSTNDate.Text), mFrmStktype, mItemCode, mUOM, mLotNo, mReturnQty, 0, "O", 0, 0, "", "", (txtDept.Text), (txtDept.Text), "", "N", UCase(mNarration), mSuppCode, ConPH, mDivisionCode, Trim(lblBookSubType.Text), "", IIf(chkClosed.CheckState = System.Windows.Forms.CheckState.Checked, "C", "O"), mHeatNo) = False Then GoTo UpdateDetail1
                            End If
                        End If
                    End If

                    If lblBookType.Text = "P" And lblBookSubType.Text = "L" Then
                        mNarration = "Line Rejection (From " & lblDeptname.Text & ")"
                    ElseIf lblBookType.Text = "P" And lblBookSubType.Text = "S" Then
                        mNarration = "Scrap Return (From " & lblDeptname.Text & ")"
                    ElseIf lblBookType.Text = "S" And lblBookSubType.Text = "F" Then
                        mNarration = "Finished Scrap Return (From " & lblDeptname.Text & ")"
                    ElseIf lblBookType.Text = "S" And lblBookSubType.Text = "C" Then
                        mNarration = "CR Scrap Return (From " & lblDeptname.Text & ")"
                    ElseIf lblBookType.Text = "P" And lblBookSubType.Text = "W" Then
                        mNarration = "WIP Scrap Return (From " & lblDeptname.Text & ")"
                    ElseIf lblBookType.Text = "P" And lblBookSubType.Text = "R" Then
                        mNarration = "Rework Scrap Return (From " & lblDeptname.Text & ")"
                    ElseIf lblBookType.Text = "S" And lblBookSubType.Text = "O" Then
                        mNarration = "Store Stock Convertion " & " (" & mFrmStktype & " STOCK )"
                    Else
                        mNarration = "From : " & lblDeptname.Text & " (" & mFrmStktype & " STOCK )"
                    End If

                    '                If lblBookType.text = "S" And lblBookSubType.text = "F" Then
                    '                        SqlStr = " INSERT INTO FIN_RGDAILYMANU_HDR ( " & vbCrLf _
                    ''                                & " MKEY , COMPANY_CODE, FYEAR, BOOKTYPE, " & vbCrLf _
                    ''                                & " BILLNO , INV_PREP_TM, MDATE, " & vbCrLf _
                    ''                                & " ITEM_CODE,ITEM_QTY, TARIFF_CODE, UPDATEFLAG) "
                    '                        SqlStr = SqlStr & vbCrLf _
                    ''                                & " VALUES ('" & lblMKey.text & "'," & RsCompany.fields("COMPANY_CODE").value & "," & vbCrLf _
                    ''                                & " " & RsCompany.fields("FYEAR").value & ", 'R'," & vbCrLf _
                    ''                                & " '" & lblMKey.text & "', TO_DATE('" & VB6.Format(txtSTNDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    ''                                & " TO_DATE('" & VB6.Format(txtSTNDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                    ''                                & " '" & mItemCode & "'," & mReturnQty & ",'" & mTariffCode & "','Y' ) "
                    '                        PubDBCn.Execute SqlStr
                    '                End If
                    If UpdateStockTRN(PubDBCn, ConStockRefType_SRN, (txtSTNNo.Text), I, (txtSTNDate.Text), (txtSTNDate.Text), mToStktype, mItemCode, mUOM, mLotNo, mReturnQty, 0, "I", 0, 0, "", "", "STR", (txtDept.Text), "", "N", UCase(mNarration), mSuppCode, ConWH, mDivisionCode, Trim(lblBookSubType.Text), "", IIf(chkClosed.CheckState = System.Windows.Forms.CheckState.Checked, "C", "O"), mHeatNo) = False Then GoTo UpdateDetail1
                End If
            Next
        End With
        UpdateDetail1 = True
        Exit Function
UpdateDetail1:
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Function
    Private Function FieldsVarification() As Boolean

        On Error GoTo err_Renamed
        Dim mRow As Integer
        Dim mSTTaxcount As Integer
        Dim mItemCode As String
        Dim mStockQty As Double
        Dim mReturnQty As Double
        Dim mProdType As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim xAutoIssue As Boolean
        Dim mLotNoRequied As String
        Dim mDivisionCode As Double
        Dim mCheckLastEntryDate As String
        Dim mInHouse As Boolean
        Dim mItemUOM As String = ""
        Dim mStockType As String = ""
        Dim mLotNo As String
        Dim mRemarks As String
        Dim mHeatNo As String

        FieldsVarification = True
        If ValidateBranchLocking((txtSTNDate.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If
        If ValidateBookLocking(PubDBCn, CInt(ConLockSTN), txtSTNDate.Text) = True Then
            FieldsVarification = False
            Exit Function
        End If


        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsReqMain.EOF = True Then Exit Function

        If MODIFYMode = True And txtSTNNo.Text = "" Then
            MsgInformation("REQUISITION No. CAN NOT Blank")
            FieldsVarification = False
            Exit Function
        End If

        If txtSTNDate.Text = "" Then
            MsgBox("Date is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtSTNDate.Focus()
            Exit Function
        ElseIf FYChk((txtSTNDate.Text)) = False Then
            FieldsVarification = False
            If txtSTNDate.Enabled = True Then txtSTNDate.Focus()
            Exit Function
        End If


        If Trim(txtDept.Text) = "" Then
            MsgBox("Department Name is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtDept.Focus()
            Exit Function
        Else
            If lblUpdate.Text = "N" Then
                If MainClass.ValidateWithMasterTable((txtDept.Text), "DEPT_CODE", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                    MsgBox("Invalid Dept Code. Cann't Save", MsgBoxStyle.Information)
                    FieldsVarification = False
                    txtDept.Focus()
                    Exit Function
                End If
            End If
        End If

        If Trim(cboDivision.Text) = "" Then
            MsgBox("Division Name is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            cboDivision.Focus()
            Exit Function
        End If

        If Trim(txtCost.Text) = "" Then
            MsgBox("Cost Center Code is Blank. Cann't Save", MsgBoxStyle.Information)
            FieldsVarification = False
            txtCost.Focus()
            Exit Function
        Else
            SqlStr = " SELECT IH.CC_CODE, IH.CC_DESC, ID.DEPT_CODE " & vbCrLf & " FROM FIN_CCENTER_HDR IH, FIN_CCENTER_DET ID " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=ID.COMPANY_CODE AND IH.CC_CODE=ID.CC_CODE" & vbCrLf & " AND IH.CC_CODE='" & MainClass.AllowSingleQuote((txtCost.Text)) & "'" & vbCrLf & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote((txtDept.Text)) & "'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = True Then
                MsgInformation("Invalid Cost Center Code for Department : " & Trim(txtDept.Text))
                FieldsVarification = False
                txtCost.Focus()
                Exit Function
            End If
        End If

        If Trim(txtEmp.Text) = "" Then
            MsgBox("Employee Name is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtEmp.Focus()
            Exit Function
        End If

        '    If Trim(txtCost.Text) = "" Then
        '        MsgBox "Cost Center Name is Blank", vbInformation
        '        FieldsVarification = False
        '        txtDept.SetFocus
        '        Exit Function
        '    End If

        If Trim(txtDept.Text) = "STR" And lblBookType.Text = "P" Then
            MsgBox("Please Check Dept.Store Dept is not a Production Floor.", MsgBoxStyle.Information)
            FieldsVarification = False
            txtDept.Focus()
            Exit Function
        End If

        If Trim(txtDept.Text) <> "STR" And lblBookType.Text = "S" Then
            MsgBox("Please Select Store Dept.", MsgBoxStyle.Information)
            FieldsVarification = False
            txtDept.Focus()
            Exit Function
        End If

        If lblUpdate.Text = "N" Then
            If ValidateDeptRight(PubUserID, Trim(txtDept.Text), UCase(Trim(lblDeptname.Text))) = False Then
                FieldsVarification = False
                Exit Function
            End If
        End If

        If MainClass.ValidateWithMasterTable(cboDivision.Text, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Val(MasterNo)
        Else
            MsgInformation("Invalid Division Name.")
            FieldsVarification = False
            Exit Function
        End If

        If ValidateDivisionRight(PubUserID, mDivisionCode, UCase(Trim(cboDivision.Text))) = False Then
            FieldsVarification = False
            Exit Function
        End If

        If PubSuperUser <> "S" Then
            mCheckLastEntryDate = GetLastEntryDate()

            If mCheckLastEntryDate <> "" Then
                If CDate(txtSTNDate.Text) < CDate(mCheckLastEntryDate) Then
                    MsgBox("Cann't be Add or Modify Back Entry", MsgBoxStyle.Information)
                    FieldsVarification = False
                    Exit Function
                End If
            End If
        End If

        ''temp mark
        '    If RsCompany.fields("COMPANY_CODE").value = 1 Then
        '        If CheckStockQty(SprdMain, ColStockQty, ColReturnQty, ColItemCode, ColFrmStockType, True) = False Then
        '            FieldsVarification = False
        '            Exit Function
        '        End If

        If Trim(txtReason.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtReason.Text, "SRN_REASON_DESC", "SRN_REASON_DESC", "INV_SRNREASON_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Please select the valid reason for the Item : ")
                txtReason.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        Dim pRtnNo As String
        With SprdMain
            For mRow = 1 To .MaxRows - 1
                .Row = mRow
                .Col = ColItemCode
                mItemCode = Trim(.Text)
                pRtnNo = ""

                If GetPendingStoreReturn(mItemCode, mDivisionCode, pRtnNo) = True Then
                    MsgInformation("Such Item Already Pending for Approval : " & pRtnNo)
                    txtReason.Focus()
                    FieldsVarification = False
                    Exit Function
                End If
                .Col = ColUom
                mItemUOM = Trim(.Text)

                .Col = ColFrmStockType
                mStockType = Trim(.Text)

                .Col = ColLotNo
                mLotNo = Trim(.Text)

                .Col = ColHeatNo
                mHeatNo = Trim(.Text)

                .Col = ColReturnQty
                mReturnQty = CDbl(Trim(.Text))

                .Col = ColRemarks
                If Trim(.Text) = "" Then
                    .Text = txtReason.Text
                End If

                mRemarks = Trim(.Text)

                mProdType = GetProductionType(mItemCode)

                SprdMain.Col = ColStockQty ''mReturnedQty +
                If IsProductionItem(mItemCode) = False Then
                    xAutoIssue = False
                Else
                    If IsInHouseItem(mItemCode) = True Then
                        mInHouse = True
                    End If
                End If
                If mProdType = "J" Then
                    .Text = CStr(GetBalanceStockQty(mItemCode, (txtSTNDate.Text), mItemUOM, Trim(txtDept.Text), mStockType, mLotNo, ConPH, mDivisionCode, ConStockRefType_SRN, Val(txtSTNNo.Text), , IIf(chkClosed.CheckState = System.Windows.Forms.CheckState.Checked, "C", "O"), mHeatNo))
                Else
                    .Text = CStr(GetBalanceStockQty(mItemCode, (txtSTNDate.Text), mItemUOM, Trim(txtDept.Text), mStockType, mLotNo, IIf(lblBookType.Text = "P" And xAutoIssue = False, ConPH, ConWH), mDivisionCode, ConStockRefType_SRN, Val(txtSTNNo.Text), , IIf(chkClosed.CheckState = System.Windows.Forms.CheckState.Checked, "C", "O"), mHeatNo))
                    If mInHouse = True And xAutoIssue = True Then
                        .Text = CStr(CDbl(.Text) + GetBalanceStockQty(mItemCode, (txtSTNDate.Text), mItemUOM, Trim(txtDept.Text), mStockType, mLotNo, ConPH, mDivisionCode, ConStockRefType_SRN, Val(txtSTNNo.Text), , IIf(chkClosed.CheckState = System.Windows.Forms.CheckState.Checked, "C", "O"), mHeatNo))
                    End If
                End If
                mStockQty = CDbl(Trim(.Text))

                mLotNoRequied = "N"
                If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mLotNoRequied = MasterNo
                End If
                If mLotNoRequied = "Y" Then
                    .Col = ColLotNo
                    If Trim(.Text) = "" Or Val(.Text) = 0 Then
                        MsgInformation("Lot No. Must For Such Item.")
                        FieldsVarification = False
                        MainClass.SetFocusToCell(SprdMain, mRow, ColLotNo)
                        Exit Function
                    End If
                End If

                '                If PubUserID = "G0416" Then GoTo NextRow

                If mProdType = "P" Or mProdType = "T" Or mProdType = "C" Or mProdType = "R" Or mProdType = "B" Or mProdType = "I" Or mProdType = "3" Or mProdType = "T" Then ''Or
                    xAutoIssue = CheckAutoIssue((txtSTNDate.Text), mItemCode)
                    If mStockQty < mReturnQty Then
                        MsgInformation("You Have Not Enough Stock For Item Code : " & mItemCode)
                        FieldsVarification = False
                        Exit Function
                    End If

                    ''Direct Scrap for Plating Items..
                    'If lblBookType.Text = "P" And lblBookSubType.Text = "S" Then
                    '    If mProdType = "P" Or mProdType = "I" Then
                    '        MsgInformation("Please make the Rework Scrap Note for the Item : " & mItemCode)
                    '        FieldsVarification = False
                    '        Exit Function
                    '    End If
                    'End If

                    '                    If IsProductionItem(mItemCode) = True And xAutoIssue = True And lblBookType.text = "P" And (lblBookSubType.text = "O" Or lblBookSubType.text = "W") Then
                    '                        MsgInformation "You Cann't be make Store Return (General / Scrap) for the Item : " & mItemCode
                    '                        FieldsVarification = False
                    '                        Exit Function
                    '                    End If
                ElseIf mProdType = "J" Then
                    If mStockQty < mReturnQty Then
                        MsgInformation("You Have Not Enough Stock For Item Code : " & mItemCode)
                        FieldsVarification = False
                        Exit Function
                    End If
                ElseIf mProdType = "G" Or mProdType = "E" Then
                    If CheckIsER1CategoryCode(mItemCode) = True Then
                        MsgInformation("You Cann't be make Store Return (General / Scrap) for the Item : " & mItemCode)
                        FieldsVarification = False
                        Exit Function
                    Else
                        If lblBookType.Text = "P" And lblBookSubType.Text = "W" Then
                            MsgInformation("You Cann't be make Store Return (WIP Scrap Return) for the Item : " & mItemCode)
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If
                End If
                If mProdType = "P" Or mProdType = "D" Or mProdType = "R" Or mProdType = "B" Or mProdType = "I" Or mProdType = "3" Or mProdType = "T" Then ''Or
                    If lblBookType.Text = "P" And (lblBookSubType.Text = "S" Or lblBookSubType.Text = "R") Then
                        If Trim(mRemarks) = "" Then
                            MsgInformation("Please select the valid reason for the Item : " & mItemCode)
                            FieldsVarification = False
                            Exit Function
                        End If
                        If MainClass.ValidateWithMasterTable(mRemarks, "SRN_REASON_DESC", "SRN_REASON_DESC", "INV_SRNREASON_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                            MsgInformation("Please select the valid reason for the Item : " & mItemCode)
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If
                End If
            Next
        End With
        '    End If
        '

        'NextRow:

        If PubSuperUser <> "S" Then
            '        If mCheckLastEntryDate <> "" Then
            If CDate(txtSTNDate.Text) < CDate(PubCurrDate) Then
                MsgBox("Cann't be Add or Modify Back Entry", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
            '        End If
        End If

        '    For mRow = 1 To SprdMain.MaxRows
        '        SprdMain.Row = mRow
        '        SprdMain.Col = ColFrmStockType
        '        If Trim(SprdMain.Text) = "FG" Then
        '            If CheckStockQty(SprdMain, ColStockQty, ColReturnQty, ColItemCode, ColFrmStockType, True) = False Then
        '                FieldsVarification = False
        '                Exit Function
        '            End If
        '        End If
        '        If CheckValidStockType(mRow, ColFrmStockType, ColItemCode, SprdMain) = False Then
        '            MsgInformation "InValid Stock Type. Please Check"
        '            MainClass.SetFocusToCell SprdMain, mRow, ColFrmStockType
        '            FieldsVarification = False
        '            Exit Function
        '        End If
        '        If CheckValidStockType(mRow, ColToStockType, ColItemCode, SprdMain) = False Then
        '            MsgInformation "InValid Stock Type. Please Check"
        '            MainClass.SetFocusToCell SprdMain, mRow, ColToStockType
        '            FieldsVarification = False
        '            Exit Function
        '        End If
        '    Next

        If SprdMain.MaxRows = 1 Then
            MsgInformation("Nothing to Save.")
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "ItemCode Is Blank.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColReturnQty, "N", "Please Check Quantity.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColFrmStockType, "S", "Please Check Stock Type.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColToStockType, "S", "Please Check Stock Type.") = False Then FieldsVarification = False : Exit Function
        'If MainClass.ValidDataInGrid(SprdMain, ColSuppCode, "S", "Please Check Supplier Name.") = False Then FieldsVarification = False : Exit Function

        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Function
    Public Function GetPendingStoreReturn(ByRef pItemCode As String, ByRef pDivision As Double, ByRef pRefNo As String) As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mValue As String

        pRefNo = ""
        SqlStr = ""
        SqlStr = " SELECT DISTINCT IH.AUTO_KEY_SRN " & vbCrLf _
            & " FROM INV_SRN_HDR IH, INV_SRN_DET ID" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " And SUBSTR(IH.AUTO_KEY_SRN,LENGTH(IH.AUTO_KEY_SRN)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " And IH.AUTO_KEY_SRN=ID.AUTO_KEY_SRN" & vbCrLf _
            & " And IH.DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "' AND IH.DIV_CODE=" & pDivision & "" & vbCrLf _
            & " And ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
            & " AND IH.STATUS='N'"

        If Val(txtSTNNo.Text) <> 0 Then
            SqlStr = SqlStr & vbCrLf & " AND IH.AUTO_KEY_SRN<> " & Val(txtSTNNo.Text) & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND IH.SRN_DATE<=TO_DATE('" & VB6.Format(txtSTNDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mValue = IIf(IsDBNull(RsTemp.Fields(0).Value), -1, RsTemp.Fields(0).Value)
                pRefNo = pRefNo & mValue
                RsTemp.MoveNext()
                If RsTemp.EOF = False Then
                    pRefNo = pRefNo & ", "
                End If
            Loop
            GetPendingStoreReturn = True
        Else
            GetPendingStoreReturn = False
        End If


        Exit Function
ErrPart:
        GetPendingStoreReturn = False
    End Function
    Private Function GetLastEntryDate() As String
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        GetLastEntryDate = IIf(PubCurrDate > RsCompany.Fields("END_DATE").Value, RsCompany.Fields("END_DATE").Value, PubCurrDate)

        '    SqlStr = ""
        '    SqlStr = "SELECT Max(SRN_DATE) AS  ISSUE_DATE " & vbCrLf _
        ''            & " FROM INV_SRN_HDR " & vbCrLf _
        ''            & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
        ''            & " AND SUBSTR(AUTO_KEY_SRN,LENGTH(AUTO_KEY_SRN)-5,4)=" & RsCompany.fields("FYEAR").value & " " & vbCrLf _
        ''            & " AND STATUS='Y'"
        '
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly
        '    If RsTemp.EOF = False Then
        '        GetLastEntryDate = IIf(IsNull(RsTemp!ISSUE_DATE), "", RsTemp!ISSUE_DATE)
        '    End If

        Exit Function
ErrPart:

    End Function
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Public Sub frmStoreRetNote_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If lblUpdate.Text = "Y" Then
            Me.Text = " Store Return Note - Approval"
        Else
            If lblBookType.Text = "P" And lblBookSubType.Text = "L" Then
                Me.Text = " Store Return Note" & " (Line Rejection)"
            ElseIf lblBookType.Text = "P" And lblBookSubType.Text = "S" Then
                Me.Text = " Store Return Note" & " (General Scrap Return)"
            ElseIf lblBookType.Text = "P" And lblBookSubType.Text = "W" Then
                Me.Text = " Store Return Note" & " (WIP Scrap Return)"
            ElseIf lblBookType.Text = "P" And lblBookSubType.Text = "R" Then
                Me.Text = " Store Return Note" & " (Rework Scrap Return)"
            ElseIf lblBookType.Text = "S" And lblBookSubType.Text = "O" Then
                Me.Text = " Store Stock Convertion Note "
            ElseIf lblBookType.Text = "S" And lblBookSubType.Text = "F" Then
                Me.Text = " Store Return Note" & " (FG Scrap - Excisable)"
            ElseIf lblBookType.Text = "S" And lblBookSubType.Text = "C" Then
                Me.Text = " Store Return Note" & " (CR Scrap - Excisable)"
            Else
                Me.Text = " Store Return Note" & " (General)"
            End If
        End If

        SqlStr = ""
        SqlStr = "Select * from INV_SRN_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsReqMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from INV_SRN_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsReqDetail, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)
        '    Call SetTextLengths

        '    FraAction.Enabled = IIf(lblAction.text = "F", True, False)
        '    If ValidateDeptRight(PubUserID, "STR", "STORE", "N") = True Then
        '        FraAction.Enabled = True
        '        chkStatus.Enabled = True
        '    Else
        '        FraAction.Enabled = False
        '        chkStatus.Enabled = False
        '    End If

        FraAction.Enabled = IIf(lblAction.Text = "F", True, IIf(PubUserID = "G0416", True, False))
        chkStatus.Enabled = IIf(lblUpdate.Text = "Y", True, IIf(PubUserID = "G0416", True, False))

        chkClosed.Enabled = False        '' IIf(PubUserID = "G0416", True, False)
        chkClosed.Visible = False    '' IIf(PubUserID = "G0416", True, False)


        If cmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
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

        SqlStr = "SELECT  AUTO_KEY_SRN AS SRN_NO, SRN_DATE, DEPT_CODE, EMP_CODE, OPR_CODE, REMARKS "

        ''FROM CLAUSE...

        SqlStr = SqlStr & vbCrLf & " FROM INV_SRN_HDR "

        ''WHERE CLAUSE...

        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & vbCrLf & " and SUBSTR(AUTO_KEY_SRN,LENGTH(AUTO_KEY_SRN)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        If lblAction.Text = "E" Then
            SqlStr = SqlStr & vbCrLf & " AND BOOKTYPE='" & lblBookType.Text & "' AND BOOKSUBTYPE='" & lblBookSubType.Text & "'"
        End If

        ''ORDER BY CLAUSE...

        SqlStr = SqlStr & vbCrLf & " Order by AUTO_KEY_SRN"

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
            .TypeEditLen = RsReqDetail.Fields("ITEM_CODE").DefinedSize ''
            .set_ColWidth(ColItemCode, 8)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn)
            .set_ColWidth(ColItemDesc, 32)

            .Col = ColUom
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsReqDetail.Fields("ITEM_UOM").DefinedSize ''
            .set_ColWidth(ColUom, 4)

            .Col = ColFrmStockType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = MainClass.SetMaxLength("FROM_STOCK_TYPE", "INV_SRN_DET", PubDBCn)
            .set_ColWidth(ColFrmStockType, 4.3)

            .Col = ColToStockType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = MainClass.SetMaxLength("FROM_STOCK_TYPE", "INV_SRN_DET", PubDBCn)
            .set_ColWidth(ColToStockType, 4.3)

            .Col = ColLotNo
            '        .CellType = SS_CELL_TYPE_INTEGER
            '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = RsReqDetail.Fields("LOT_NO").DefinedSize '' MainClass.SetMaxLength("LOT_NO", "INV_SRN_DET", PubDBCn)  ''
            .set_ColWidth(ColLotNo, 5)

            .Col = ColHeatNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsReqDetail.Fields("HEAT_NO").DefinedSize
            .set_ColWidth(ColHeatNo, 6)
            .ColHidden = IIf(RsCompany.Fields("HEATNO_HIDE").Value = "N", False, True)

            .Col = ColStockQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColStockQty, 7.5)

            .Col = ColReturnQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColReturnQty, 8)

            .Col = ColReturnedQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColReturnedQty, 9)
            .ColHidden = True

            .Col = ColSuppCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = MainClass.SetMaxLength("SUPP_CUST_CODE", "INV_SRN_DET", PubDBCn)
            .set_ColWidth(ColSuppCode, 6)

            .Col = ColSuppName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .TypeEditLen = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
            .set_ColWidth(ColSuppName, 18)

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditMultiLine = False
            .TypeEditLen = MainClass.SetMaxLength("REMARKS", "INV_SRN_DET", PubDBCn)
            .set_ColWidth(ColRemarks, 12)

        End With
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemDesc, ColUom)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColSuppName, ColSuppName)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColStockQty, ColStockQty)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColReturnedQty, ColReturnedQty)

        If lblAction.Text = "F" Then
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColFrmStockType, ColReturnedQty)
        Else
            If lblBookType.Text = "P" And lblBookSubType.Text = "L" Then
                MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColFrmStockType, ColToStockType)
            ElseIf lblBookType.Text = "P" And lblBookSubType.Text = "S" Then
                MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColFrmStockType, ColToStockType)
            ElseIf lblBookType.Text = "P" And (lblBookSubType.Text = "W" Or lblBookSubType.Text = "R") Then
                MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColFrmStockType, ColToStockType)
                '        ElseIf lblBookType.text = "S" And lblBookSubType.text = "S" Then
                '            MainClass.ProtectCell SprdMain, 1, SprdMain.MaxRows, ColFrmStockType, ColToStockType
            End If
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
            txtSTNDate.MaxLength = 10
            txtSTNNo.MaxLength = .Fields("AUTO_KEY_ISS").Precision
            txtDept.MaxLength = .Fields("DEPT_CODE").DefinedSize
            txtEmp.MaxLength = .Fields("EMP_CODE").DefinedSize
            txtCost.MaxLength = .Fields("COST_CENTER_CODE").DefinedSize
            txtRemarks.MaxLength = .Fields("REMARKS").DefinedSize
            txtAction.MaxLength = .Fields("ACTIONTAKEN").DefinedSize
            'txtProcesscd.Maxlength = .Fields("OPR_CODE").DefinedSize

        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Show1()

        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim mEntryDate As String
        Dim mDivisionCode As Double
        Dim mDivisionDesc As String = ""

        Clear1()

        With RsReqMain
            If Not .EOF Then
                txtSTNNo.Enabled = False
                lblMKey.Text = .Fields("AUTO_KEY_SRN").Value

                txtSTNNo.Text = IIf(IsDBNull(.Fields("AUTO_KEY_SRN").Value), 0, .Fields("AUTO_KEY_SRN").Value)
                txtSTNDate.Text = VB6.Format(IIf(IsDBNull(.Fields("SRN_DATE").Value), "", .Fields("SRN_DATE").Value), "DD/MM/YYYY")
                txtDept.Text = IIf(IsDBNull(.Fields("DEPT_CODE").Value), "", .Fields("DEPT_CODE").Value)
                txtEmp.Text = IIf(IsDBNull(.Fields("EMP_CODE").Value), "", .Fields("EMP_CODE").Value)
                txtCost.Text = IIf(IsDBNull(.Fields("COST_CENTER_CODE").Value), "", .Fields("COST_CENTER_CODE").Value)
                '            txtsubdept.Text = IIf(IsNull(!REMARKS), "", !REMARKS)
                'txtProcesscd.Text = IIf(IsDbNull(.Fields("OPR_CODE").Value), "", .Fields("OPR_CODE").Value)

                If MainClass.ValidateWithMasterTable((txtDept.Text), "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    lblDeptname.Text = MasterNo
                End If


                If MainClass.ValidateWithMasterTable((txtEmp.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    lblEmpname.Text = MasterNo
                End If

                If MainClass.ValidateWithMasterTable((txtCost.Text), "CC_CODE", "CC_DESC", "FIN_CCENTER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    lblCostctr.Text = MasterNo
                End If

                'If MainClass.ValidateWithMasterTable((txtProcesscd.Text), "OPR_CODE", "OPR_DESC", "PRD_OPR_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '    lblProcessCd.Text = MasterNo
                'End If


                txtRemarks.Text = IIf(IsDBNull(.Fields("Remarks").Value), "", .Fields("Remarks").Value)
                txtAction.Text = IIf(IsDBNull(.Fields("ACTIONTAKEN").Value), "", .Fields("ACTIONTAKEN").Value)
                chkStatus.CheckState = IIf(.Fields("Status").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                chkClosed.CheckState = IIf(.Fields("CLOSED_SRN").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                chkProductionFloor.CheckState = IIf(.Fields("PRD_FLOOR").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                mEntryDate = IIf(IsDBNull(.Fields("ADDUSER").Value), "", .Fields("ADDUSER").Value)
                mEntryDate = mEntryDate & " - " & VB6.Format(IIf(IsDBNull(.Fields("ADDDATE").Value), "", .Fields("ADDDATE").Value), "DD/MM/YYYY")
                mEntryDate = mEntryDate & vbCrLf & IIf(IsDBNull(.Fields("MODUSER").Value), "", .Fields("MODUSER").Value) & " - " & VB6.Format(IIf(IsDBNull(.Fields("MODDATE").Value), "", .Fields("MODDATE").Value), "DD/MM/YYYY")
                txtEntryDate.Text = mEntryDate

                mDivisionCode = IIf(IsDBNull(.Fields("DIV_CODE").Value), -1, .Fields("DIV_CODE").Value)

                If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivisionDesc = Trim(MasterNo)
                End If
                cboDivision.Text = mDivisionDesc
                cboDivision.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, IIf(.Fields("Status").Value = "Y", False, True))

                If lblUpdate.Text = "Y" Then
                    chkStatus.Enabled = IIf(.Fields("Status").Value = "Y", False, True)
                    lblBookType.Text = IIf(IsDBNull(.Fields("BookType").Value), "", .Fields("BookType").Value)
                    lblBookSubType.Text = IIf(IsDBNull(.Fields("BOOKSUBTYPE").Value), "", .Fields("BOOKSUBTYPE").Value)
                    txtSTNDate.Enabled = IIf(.Fields("Status").Value = "Y", False, True)
                Else
                    chkStatus.Enabled = False '' IIf(!Status = "Y", False, True)
                    txtSTNDate.Enabled = False
                End If

                Call ShowDetail1(lblMKey.Text, mDivisionCode)

            End If
        End With
        ADDMode = False
        MODIFYMode = False
        cmdPopulate.Enabled = False
        MainClass.ButtonStatus(Me, XRIGHT, RsReqMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)

        '    FormatSprdMain -1

        SprdMain.Enabled = True
        txtSTNNo.Enabled = True
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
    Private Sub ShowDetail1(ByRef pReqNum As Double, ByRef mDivisionCode As Double)

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mSuppCode As String
        Dim mSuppName As String = ""
        Dim mReturnedQty As Double
        Dim mItemUOM As String = ""
        Dim mStkType As String
        Dim xAutoIssue As Boolean
        Dim xLotNo As String
        Dim mInHouse As Boolean
        Dim mHeatNo As String

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM INV_SRN_DET  " & vbCrLf & " Where AUTO_KEY_SRN = " & Val(CStr(pReqNum)) & "" & vbCrLf & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsReqDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsReqDetail
            If .EOF = True Then Exit Sub
            '        FormatSprdMain -1
            I = 1
            .MoveFirst()

            Do While Not .EOF

                SprdMain.Row = I

                SprdMain.Col = ColItemCode
                mItemCode = IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)
                SprdMain.Text = mItemCode

                SprdMain.Col = ColUom
                SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)
                mItemUOM = IIf(IsDBNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)

                SprdMain.Col = ColFrmStockType
                SprdMain.Text = IIf(IsDBNull(.Fields("FROM_STOCK_TYPE").Value), "", .Fields("FROM_STOCK_TYPE").Value)
                mStkType = IIf(IsDBNull(.Fields("FROM_STOCK_TYPE").Value), "", .Fields("FROM_STOCK_TYPE").Value)

                SprdMain.Col = ColToStockType
                SprdMain.Text = IIf(IsDBNull(.Fields("TO_STOCK_TYPE").Value), "", .Fields("TO_STOCK_TYPE").Value)

                SprdMain.Col = ColHeatNo
                mHeatNo = IIf(IsDBNull(.Fields("HEAT_NO").Value), "", .Fields("HEAT_NO").Value)
                'mHeatNo = IIf(Val(mHeatNo) < 0, "", mHeatNo)
                SprdMain.Text = mHeatNo

                SprdMain.Col = ColLotNo
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("LOT_NO").Value), "", .Fields("LOT_NO").Value))
                xLotNo = IIf(IsDBNull(.Fields("LOT_NO").Value), "", .Fields("LOT_NO").Value)

                SprdMain.Col = ColReturnQty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("RTN_QTY").Value), "", .Fields("RTN_QTY").Value)))

                SprdMain.Col = ColReturnedQty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("RTN_QTY").Value), "", .Fields("RTN_QTY").Value)))
                mReturnedQty = Val(IIf(IsDBNull(.Fields("RTN_QTY").Value), "", .Fields("RTN_QTY").Value))

                SprdMain.Col = ColStockQty
                '            SprdMain.Text = mReturnedQty + GetBalanceStockQty(mItemCode, txtSTNDate.Text, mItemUOM, Trim(txtDept.Text), mStkType, "", IIf(lblbooktype.text="P", ConPH, ConWH))

                mInHouse = False
                If GetProductionType(Trim(mItemCode)) = "J" Then
                    xAutoIssue = False
                Else
                    xAutoIssue = CheckAutoIssue((txtSTNDate.Text), mItemCode)
                End If

                If IsProductionItem(mItemCode) = False Then
                    xAutoIssue = False
                Else
                    If IsInHouseItem(mItemCode) = True Then
                        mInHouse = True
                    End If
                End If

                SprdMain.Text = CStr(GetBalanceStockQty(mItemCode, (txtSTNDate.Text), mItemUOM, Trim(txtDept.Text), mStkType, xLotNo, IIf(lblBookType.Text = "P", IIf(xAutoIssue = False, ConPH, ConWH), ConWH), mDivisionCode, ConStockRefType_SRN, Val(txtSTNNo.Text), , IIf(chkClosed.CheckState = System.Windows.Forms.CheckState.Checked, "C", "O"), mHeatNo))

                If mInHouse = True And xAutoIssue = True And GetProductFinalDept(mItemCode, (txtSTNDate.Text)) <> "STR" Then
                    SprdMain.Text = CStr(Val(SprdMain.Text) + GetBalanceStockQty(mItemCode, (txtSTNDate.Text), mItemUOM, Trim(txtDept.Text), mStkType, xLotNo, ConPH, mDivisionCode, ConStockRefType_SRN, Val(txtSTNNo.Text), , IIf(chkClosed.CheckState = System.Windows.Forms.CheckState.Checked, "C", "O"), mHeatNo))
                End If

                SprdMain.Col = ColSuppCode
                mSuppCode = IIf(IsDBNull(.Fields("SUPP_CUST_CODE").Value), "", .Fields("SUPP_CUST_CODE").Value)
                SprdMain.Text = mSuppCode

                SprdMain.Col = ColRemarks
                SprdMain.Text = IIf(IsDBNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)

                SprdMain.Col = ColItemDesc
                MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mItemDesc = MasterNo
                SprdMain.Text = mItemDesc

                SprdMain.Col = ColSuppName
                MainClass.ValidateWithMasterTable(mSuppCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mSuppName = MasterNo
                SprdMain.Text = mSuppName

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
        MainClass.ButtonStatus(Me, XRIGHT, RsReqMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub Clear1()


        lblMKey.Text = ""


        If RsCompany.Fields("FYEAR").Value = GetCurrentFYNo(PubDBCn, VB6.Format(PubCurrDate, "DD/MM/YYYY")) Then
            txtSTNDate.Text = VB6.Format(PubCurrDate, "DD/MM/YYYY")
        Else
            txtSTNDate.Text = VB6.Format(RsCompany.Fields("END_DATE").Value, "DD/MM/YYYY")
        End If
        txtSTNNo.Text = ""
        txtDept.Text = ""
        txtEmp.Text = ""
        txtCost.Text = ""
        'txtProcesscd.Text = ""
        txtRemarks.Text = ""
        txtAction.Text = ""

        cboDivision.Text = GetDefaultDivision()        'cboDivision.SelectedIndex = -1
        cboDivision.Enabled = True

        lblCostctr.Text = ""
        lblDeptname.Text = ""
        lblEmpname.Text = ""
        'lblProcessCd.Text = ""
        txtEntryDate.Text = ""

        chkProductionFloor.CheckState = IIf(lblBookType.Text = "P", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

        chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked

        chkClosed.CheckState = System.Windows.Forms.CheckState.Unchecked

        chkStatus.Enabled = IIf(lblUpdate.Text = "Y", True, False)

        If lblUpdate.Text = "Y" Then
            lblBookType.Text = ""
            lblBookSubType.Text = ""
        End If

        '    If ValidateDeptRight(PubUserID, "STR", "STORE", "N") = True Then
        '        FraAction.Enabled = True
        '        chkStatus.Enabled = True
        '    Else
        '        FraAction.Enabled = False
        '        chkStatus.Enabled = False
        '    End If


        FraAction.Enabled = IIf(lblAction.Text = "F", True, False)


        txtSTNDate.Enabled = IIf(lblUpdate.Text = "Y", True, False) '' IIf(PubATHUSER = True, True, False)
        cmdPopulate.Enabled = IIf(lblBookType.Text = "P" And (lblBookSubType.Text = "W" Or lblBookSubType.Text = "R"), True, False)

        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsReqMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub frmStoreRetNote_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub frmStoreRetNote_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        '    MainClass.DoFunctionKey Me, KeyCode
    End Sub
    Public Sub frmStoreRetNote_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        cboDivision.Text = GetDefaultDivision()        'cboDivision.SelectedIndex = -1

        'AdoDCMain.Visible = False
        txtSTNNo.Enabled = True
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

        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemDesc, 0))
        SprdMain.Refresh()
    End Sub
    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        With SprdMain
            SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With

    End Sub


    Private Sub txtAction_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAction.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAction_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAction.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtAction.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCost_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCost.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCost_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCost.DoubleClick
        Call cmdCCSearch_Click(cmdCCSearch, New System.EventArgs())
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

        If Trim(txtDept.Text) = "" Then
            MsgInformation("Please Select Dept. First.")
            txtDept.Focus()
            GoTo EventExitSub
        End If

        SqlStr = " SELECT IH.CC_CODE, IH.CC_DESC, ID.DEPT_CODE " & vbCrLf & " FROM FIN_CCENTER_HDR IH, FIN_CCENTER_DET ID " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=ID.COMPANY_CODE AND IH.CC_CODE=ID.CC_CODE" & vbCrLf & " AND IH.CC_CODE='" & MainClass.AllowSingleQuote((txtCost.Text)) & "'" & vbCrLf & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote((txtDept.Text)) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            lblCostctr.Text = IIf(IsDBNull(RsTemp.Fields("CC_DESC").Value), "", RsTemp.Fields("CC_DESC").Value)
        Else
            MsgInformation("Invalid Cost Center Code for Department : " & Trim(txtDept.Text))
            Cancel = True
        End If
        '
        '    If txtCost.Text = "" Then Exit Sub
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

    Private Sub txtSTNDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSTNDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSTNDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSTNDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        Dim CntRow As Integer
        Dim mItemCode As String
        Dim mItemUOM As String = ""
        Dim mStkType As String
        Dim xAutoIssue As Boolean
        Dim xLotNo As String
        Dim mDivisionCode As Double
        Dim mInHouse As Boolean
        Dim mHeatNo As String

        If Trim(txtSTNDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtSTNDate.Text) Then
            MsgInformation("Invalid Date")
            Cancel = True
            GoTo EventExitSub
        End If

        If FYChk((txtSTNDate.Text)) = False Then
            If txtSTNDate.Enabled = True Then txtSTNDate.Focus()
            Cancel = True
            GoTo EventExitSub
        End If

        If Trim(txtDept.Text) = "" Then GoTo EventExitSub

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            GoTo EventExitSub
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        xAutoIssue = CheckAutoIssue((txtSTNDate.Text), "")
        mInHouse = False
        With SprdMain
            For CntRow = 1 To .MaxRows
                .Row = CntRow
                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColUom
                mItemUOM = Trim(.Text)

                .Col = ColFrmStockType
                mStkType = Trim(.Text)

                SprdMain.Col = ColLotNo
                xLotNo = Trim(SprdMain.Text)

                SprdMain.Col = ColHeatNo
                mHeatNo = Trim(SprdMain.Text)


                If mItemCode <> "" Then
                    xAutoIssue = CheckAutoIssue((txtSTNDate.Text), mItemCode)
                    If IsProductionItem(mItemCode) = False Then
                        xAutoIssue = False
                    Else
                        If IsInHouseItem(mItemCode) = True Then
                            mInHouse = True
                        End If
                    End If
                    .Col = ColStockQty
                    .Text = CStr(GetBalanceStockQty(mItemCode, (txtSTNDate.Text), mItemUOM, Trim(txtDept.Text), mStkType, xLotNo, IIf(lblBookType.Text = "P", IIf(xAutoIssue = False, ConPH, ConWH), ConWH), mDivisionCode, ConStockRefType_SRN, Val(txtSTNNo.Text), , IIf(chkClosed.CheckState = System.Windows.Forms.CheckState.Checked, "C", "O"), mHeatNo))

                    If mInHouse = True And xAutoIssue = True And GetProductFinalDept(mItemCode, (txtSTNDate.Text)) <> "STR" Then
                        SprdMain.Text = CStr(GetBalanceStockQty(mItemCode, (txtSTNDate.Text), mItemUOM, Trim(txtDept.Text), mStkType, xLotNo, ConPH, mDivisionCode, ConStockRefType_SRN, Val(txtSTNNo.Text), , IIf(chkClosed.CheckState = System.Windows.Forms.CheckState.Checked, "C", "O"), mHeatNo))
                    End If
                End If
            Next
        End With

        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtDept_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.DoubleClick
        Call cmdDeptSearch_Click(cmdDeptSearch, New System.EventArgs())
    End Sub

    Private Sub txtDept_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDept.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDept.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDept_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDept.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then TxtDept_DoubleClick(txtDept, New System.EventArgs())
    End Sub

    Private Sub txtDept_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDept.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        Dim CntRow As Integer
        Dim mItemCode As String
        Dim mItemUOM As String = ""
        Dim mStkType As String
        Dim xAutoIssue As Boolean
        Dim xLotNo As String
        Dim mDivisionCode As Double
        Dim mInHouse As Boolean
        Dim mHeatNo As String

        If txtDept.Text = "" Then GoTo EventExitSub

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            GoTo EventExitSub
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        If MainClass.ValidateWithMasterTable((txtDept.Text), "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            lblDeptname.Text = MasterNo
        Else
            MsgInformation("Invalid Depatment Code")
            Cancel = True
            GoTo EventExitSub
        End If

        If Trim(txtDept.Text) = "STR" And lblBookType.Text = "P" Then
            MsgBox("Please Check Dept.Store Dept is not a Production Floor.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If

        If Trim(txtDept.Text) <> "STR" And lblBookType.Text = "S" Then
            MsgBox("Please Select Store Dept.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If

        With SprdMain
            For CntRow = 1 To .MaxRows
                .Row = CntRow
                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColUom
                mItemUOM = Trim(.Text)

                .Col = ColFrmStockType
                mStkType = Trim(.Text)

                SprdMain.Col = ColLotNo
                xLotNo = Trim(SprdMain.Text)

                SprdMain.Col = ColHeatNo
                mHeatNo = Trim(SprdMain.Text)


                mInHouse = False
                If mItemCode <> "" Then
                    xAutoIssue = CheckAutoIssue((txtSTNDate.Text), mItemCode)
                    If IsProductionItem(mItemCode) = False Then
                        xAutoIssue = False
                    Else
                        If IsInHouseItem(mItemCode) = True Then
                            mInHouse = True
                        End If
                    End If
                    .Col = ColStockQty
                    .Text = CStr(GetBalanceStockQty(mItemCode, (txtSTNDate.Text), mItemUOM, Trim(txtDept.Text), mStkType, xLotNo, IIf(lblBookType.Text = "P", IIf(xAutoIssue = False, ConPH, ConWH), ConWH), mDivisionCode, ConStockRefType_SRN, Val(txtSTNNo.Text), , IIf(chkClosed.CheckState = System.Windows.Forms.CheckState.Checked, "C", "O"), mHeatNo))
                    If mInHouse = True And xAutoIssue = True And GetProductFinalDept(mItemCode, (txtSTNDate.Text)) <> "STR" Then
                        SprdMain.Text = CStr(GetBalanceStockQty(mItemCode, (txtSTNDate.Text), mItemUOM, Trim(txtDept.Text), mStkType, xLotNo, ConPH, mDivisionCode, ConStockRefType_SRN, Val(txtSTNNo.Text), , IIf(chkClosed.CheckState = System.Windows.Forms.CheckState.Checked, "C", "O"), mHeatNo))
                    End If
                End If
            Next
        End With
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


    Private Sub txtSTNNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSTNNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtSTNNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSTNNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSTNNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Public Sub txtSTNNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSTNNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mReqnum As String = ""

        If Trim(txtSTNNo.Text) = "" Then GoTo EventExitSub

        If Len(txtSTNNo.Text) < 6 Then
            txtSTNNo.Text = Trim(txtSTNNo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        If MODIFYMode = True And RsReqMain.EOF = False Then mReqnum = RsReqMain.Fields("AUTO_KEY_SRN").Value

        SqlStr = "Select * From INV_SRN_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUBSTR(AUTO_KEY_SRN,LENGTH(AUTO_KEY_SRN)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_SRN=" & Val(txtSTNNo.Text) & ""

        If lblAction.Text = "E" Then
            SqlStr = SqlStr & vbCrLf & " AND BOOKTYPE='" & lblBookType.Text & "' AND BOOKSUBTYPE='" & lblBookSubType.Text & "'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsReqMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsReqMain.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such SRN, Use Generate SRN Option To add", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "Select * From INV_SRN_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUBSTR(AUTO_KEY_SRN,LENGTH(AUTO_KEY_SRN)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AUTO_KEY_SRN=" & Val(mReqnum) & ""

                If lblAction.Text = "E" Then
                    SqlStr = SqlStr & vbCrLf & " AND BOOKTYPE='" & lblBookType.Text & "' AND BOOKSUBTYPE='" & lblBookSubType.Text & "'"
                End If

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsReqMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Public Function FillSuppname(ByRef pSuppCode As String) As Object
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        FillSuppname = ""
        '    If Trim(pSuppCode) = "" Then Exit Function
        '    With SprdMain
        '        SqlStr = "SELECT SUPP_CUST_CODE,SUPP_CUST_NAME " & vbCrLf _
        ''            & " FROM FIN_SUPP_CUST_MST " & vbCrLf _
        ''            & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
        ''            & " AND LTRIM(RTRIM(SUPP_CUST_CODE))='" & MainClass.AllowSingleQuote(pSuppCode) & "'"
        '        MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly
        '        If RsTemp.EOF = False Then
        '            .Row = .ActiveRow
        '            .Col = ColSuppName
        '            .Text = IIf(IsNull(RsTemp!SUPP_CUST_NAME), "", RsTemp!SUPP_CUST_NAME)
        '        Else
        '            MainClass.SetFocusToCell SprdMain, .ActiveRow, ColSuppCode
        '        End If
        '    End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Sub SearchReason()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster(txtReason.Text, "INV_SRNREASON_MST", "SRN_REASON_DESC", , , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtReason.Text = AcName
        End If
        txtReason.Focus()
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtReason_DoubleClick(sender As Object, e As EventArgs) Handles txtReason.DoubleClick
        Call SearchReason()
    End Sub

    Private Sub txtReason_KeyUp(sender As Object, e As KeyEventArgs) Handles txtReason.KeyUp
        Dim KeyCode As Short = e.KeyCode
        Dim Shift As Short = e.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchReason()
    End Sub

    Private Sub txtReason_Validating(sender As Object, EventArgs As CancelEventArgs) Handles txtReason.Validating
        Dim Cancel As Boolean = EventArgs.Cancel

        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        If Trim(txtReason.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtReason.Text, "SRN_REASON_DESC", "SRN_REASON_DESC", "INV_SRNREASON_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invalid Reason")
            Cancel = True
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        EventArgs.Cancel = Cancel
    End Sub
End Class
