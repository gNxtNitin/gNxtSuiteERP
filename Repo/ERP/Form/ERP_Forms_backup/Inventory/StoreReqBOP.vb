Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports System.ComponentModel

Friend Class FrmStoreReqBOP
    Inherits System.Windows.Forms.Form
    Dim RsReqMain As ADODB.Recordset ''Recordset
    Dim RsReqDetail As ADODB.Recordset ''Recordset
    'Private PvtDBCn As ADODB.Connection

    Dim mSearchStartRow As Integer

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
    Private Const ColHeatNo As Short = 5
    Private Const ColBatchNo As Short = 6
    Private Const ColStockQty As Short = 7
    Private Const ColDeptQty As Short = 8
    Private Const colStdQty As Short = 9
    Private Const ColDemandQty As Short = 10
    Private Const ColIssueQty As Short = 11
    Private Const ColIssuedQty As Short = 12
    Private Const ColBalQty As Short = 13
    Private Const ColRemarks As Short = 14
    Private Const ColTodayPlanQty As Short = 15
    Private Const ColItemCapacity As Short = 16
    Private Const ColTodayIssued As Short = 17

    Dim pDataShow As Boolean
    Dim FileDBCn As ADODB.Connection

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer

    Dim mUserMKey As Double
    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboShiftcd_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShiftcd.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboShiftcd_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShiftcd.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboStockFor_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboStockFor.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboSuppReason_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboSuppReason.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub cboSuppReason_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboSuppReason.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboStockFor_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboStockFor.SelectedIndexChanged

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

        cboDivision.Items.Clear()

        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " ORDER BY DIV_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDivision.Items.Add(RS.Fields("DIV_DESC").Value)
                RS.MoveNext()
            Loop
        End If

        cboDivision.Text = GetDefaultDivision()        'cboDivision.SelectedIndex = -1


        cboShiftcd.Items.Clear()
        cboShiftcd.Items.Add(("A"))
        cboShiftcd.Items.Add(("B"))
        cboShiftcd.Items.Add(("C"))

        cboStockFor.Items.Clear()
        cboStockFor.Items.Add("General")
        cboStockFor.Items.Add("Production")
        cboStockFor.Items.Add("Sub Store")
        cboStockFor.Items.Add("New Development")
        cboStockFor.Items.Add("Capital")

        cboShiftcd.SelectedIndex = -1
        cboStockFor.SelectedIndex = -1

        cboSuppReason.Items.Clear()
        cboSuppReason.Items.Add("1. Despatch Increase")
        cboSuppReason.Items.Add("2. Rejection")
        cboSuppReason.Items.Add("3. Short Received")
        cboSuppReason.Items.Add("4. Others")
        cboSuppReason.SelectedIndex = -1

        Exit Sub
FillERR:
        MsgBox(Err.Description)
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
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


        If chkIssue.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Issue Completed, Cann't be Deleted")
            Exit Sub
        End If

        If Not RsReqMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "INV_ISSUE_HDR", (txtReqNo.Text), RsReqMain, "AUTO_KEY_ISS") = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "INV_ISSUE_HDR", "AUTO_KEY_ISS", (txtReqNo.Text)) = False Then GoTo DelErrPart

                If DeleteStockTRN(PubDBCn, ConStockRefType_ISS, (txtReqNo.Text)) = False Then GoTo DelErrPart

                If lblBookType.Text = "I" Then
                    PubDBCn.Execute("UPDATE INV_ISSUE_DET SET ISSUE_QTY=0 Where AUTO_KEY_ISS=" & Val(txtReqNo.Text) & "")
                    PubDBCn.Execute("UPDATE INV_ISSUE_HDR SET ISSUE_STATUS='N',ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "', Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') Where AUTO_KEY_ISS=" & Val(txtReqNo.Text) & "")
                Else
                    PubDBCn.Execute("Delete from INV_ISSUE_DET Where AUTO_KEY_ISS=" & Val(txtReqNo.Text) & "")
                    PubDBCn.Execute("Delete from INV_ISSUE_HDR Where AUTO_KEY_ISS=" & Val(txtReqNo.Text) & "")
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

        If PubUserID <> "G0416" Then
            If chkIssue.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgInformation("Issue Completed, Cann't be Modified")
                Exit Sub
            End If
        End If

        If cmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsReqMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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

    Private Sub cmdPopulate_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPopulate.Click

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsTempBOM As ADODB.Recordset
        Dim mProductCode As String = ""
        Dim mProductPlanQty As Double
        Dim cntRow As Integer
        Dim mItemCode As String
        Dim mItemUOM As String = ""
        Dim mIsChild As Boolean
        Dim xAutoIssue As Boolean
        Dim mProd_Type As Boolean
        Dim mDivisionCode As Double
        Dim mStockQty As Double
        Dim xItemUOM As String
        Dim mDemandQty As Double
        Dim mInvoiceDate As String
        Dim mMainItemCode As String
        Dim mIssueNo As Double

        If Trim(txtDept.Text) = "" Then Exit Sub
        If Trim(txtReqDate.Text) = "" Then Exit Sub
        If Not IsDate(txtReqDate.Text) Then Exit Sub
        If VB.Left(cboStockFor.Text, 1) <> "P" Then MsgInformation("Please select the stock for") : Exit Sub
        If Val(txtRequestQty.Text) < 0 Then MsgInformation("Request Qty SHould be Greater Than Zero.") : Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        MainClass.ClearGrid(SprdMain)

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        xAutoIssue = CheckAutoIssue((txtReqDate.Text), "")

        If xAutoIssue = True Then
            Exit Sub
        End If

        If lblIsSuppIssue.Text = "N" Then
            txtPlanQty.Text = GetProductPlannedQty(txtProductCode.Text)

            txtIssuedQty.Text = CStr(GetDemandedQty())

            mProductCode = Trim(txtProductCode.Text)

            If MainClass.ValidateWithMasterTable(Trim(mProductCode), "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblProductDesc.Text = Trim(MasterNo)
            End If

            If MainClass.ValidateWithMasterTable(Trim(mProductCode), "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                xItemUOM = Trim(MasterNo)
            End If

            mMainItemCode = GetMainItemCode(mProductCode)

            mProductPlanQty = CDbl(VB6.Format(Val(txtRequestQty.Text), "0.00"))

        Else
            If MainClass.ValidateWithMasterTable(Trim(txtProductCode.Text), "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                xItemUOM = Trim(MasterNo)
            End If

            mMainItemCode = GetMainItemCode(txtProductCode.Text)

            mProductPlanQty = CDbl(VB6.Format(Val(txtRequestQty.Text), "0.000"))
        End If

        txtLineCapacity.Text = CStr(GetLineCapacityQty(txtProductCode.Text, txtDept.Text, txtReqDate.Text))

        If mProductPlanQty > 0 Then
            Call ShowNewBOM(mMainItemCode, mProductPlanQty, mDivisionCode)
        End If


        txtProductCode.Enabled = False
        cmdSearchProduct.Enabled = False
        txtDept.Enabled = False
        cboStockFor.Enabled = False
        txtPlanQty.Enabled = False
        txtLineCapacity.Enabled = False
        FormatSprdMain(-1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        pDataShow = True
        Exit Sub
ErrPart:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Public Function CheckHolidays(ByRef pDate As String) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        CheckHolidays = False

        SqlStr = " SELECT LEAVE_TYPE  FROM PAY_HOLIDAY_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND HOLIDAY_DATE=TO_DATE('" & UCase(VB6.Format(pDate, "DD-MMM-YYYY")) & "','DD-MON-YYYY') AND APP_STAFF='Y'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            CheckHolidays = True
        End If
        Exit Function
ErrPart:
        CheckHolidays = False
    End Function

    Private Sub cmdPopulateExcel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPopulateExcel.Click
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
        Dim mDemandQty As Double
        Dim xSqlStr As String
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsFile As ADODB.Recordset
        Dim FileConnStr As String

        Dim strTemp As String
        Dim strWkShName As String
        Dim strError As String = ""
        Dim mDivisionCode As Double
        Dim mTagNo As Double
        Dim mRow As Integer
        'Dim mStockType As String = ""

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
            mRow = 1
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


                    mStockType = Trim(IIf(IsDBNull(RsFile.Fields(3).Value), "", RsFile.Fields(3).Value))
                    mDemandQty = Val(IIf(IsDBNull(RsFile.Fields(4).Value), 0, RsFile.Fields(4).Value))

                    If mDemandQty = 0 Then GoTo NextRecord


                    SprdMain.Row = mRow '' SprdMain.MaxRows



                    SprdMain.Col = ColItemCode
                    SprdMain.Text = mItemCode
                    If CheckDuplicateItem(mItemCode) = True Then
                        SprdMain.Col = ColItemCode
                        SprdMain.Text = ""
                        GoTo NextRecord
                    End If
                    MainClass.SetFocusToCell(SprdMain, mRow, ColItemCode)
                    SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColItemCode, mRow, ColItemCode, mRow, False))

                    SprdMain.Col = ColItemDesc
                    SprdMain.Text = mItemDesc


                    SprdMain.Col = ColUom
                    SprdMain.Text = mUOM

                    SprdMain.Col = ColStockType
                    SprdMain.Text = mStockType

                    SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColStockType, mRow, ColStockType, mRow, False))

                    SprdMain.Col = ColDemandQty
                    SprdMain.Text = VB6.Format(mDemandQty, "0.000")

                    SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColDemandQty, mRow, ColDemandQty, mRow, False))

                    mRow = mRow + 1
                    '                SprdMain.MaxRows = SprdMain.MaxRows + 1
                    '               FormatSprdMain -1, False

NextRecord:
                    RsFile.MoveNext()
                Loop
            End If
        End If

        FormatSprdMain(-1)

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
        Call ReportOnMatReq(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnMatReq(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportOnMatReq(ByRef Mode As Crystal.DestinationConstants)
        'Dim Printer As New Printer

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mTitle As String = ""
        Dim cntRow As Integer
        Dim mItemCode As String
        Dim mStrStock As Double
        Dim mWIPStock As Double
        Dim mTodayIss As Double
        Dim mTodayPlan As Double

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        If lblBookType.Text = "I" Then
            mTitle = "Material Issue Note"
        Else
            mTitle = "Store Requisition Note"
        End If

        If lblIsSuppIssue.Text = "Y" Then
            mTitle = mTitle & " (Supplementary) - " & Trim(cboSuppReason.Text)
        End If

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mUserMKey = MainClass.AutoGenRowNo("PUR_PURCHASE_HDR", "PRN", PubDBCn)

        SqlStr = " DELETE FROM TEMP_STOCK_TRN WHERE USERID='" & mUserMKey & "'"
        PubDBCn.Execute(SqlStr)

        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow
                .Col = 1

                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColStockQty
                mStrStock = CDbl(VB6.Format(.Text, "0.00"))

                .Col = ColDeptQty
                mWIPStock = CDbl(VB6.Format(.Text, "0.00"))

                .Col = ColTodayPlanQty
                mTodayPlan = CDbl(VB6.Format(.Text, "0.00"))

                .Col = ColTodayIssued
                mTodayIss = CDbl(VB6.Format(.Text, "0.00"))

                SqlStr = " INSERT INTO TEMP_STOCK_TRN ( " & vbCrLf _
                    & " USERID, AUTO_KEY_ISS, SERIAL_NO, " & vbCrLf _
                    & " ITEM_CODE, STR_STOCK, WIP_STOCK, " & vbCrLf _
                    & " WEEKLY_ISS, WEEKLY_DSP ) VALUES (" & vbCrLf _
                    & " '" & mUserMKey & "', " & Val(txtReqNo.Text) & ", " & vbCrLf _
                    & " " & cntRow & ", '" & MainClass.AllowSingleQuote(mItemCode) & "'," & mStrStock & "," & vbCrLf _
                    & " " & mWIPStock & ", " & mTodayIss & ", " & mTodayPlan & ")"

                PubDBCn.Execute(SqlStr)

            Next
        End With

        PubDBCn.CommitTrans()

        SqlStr = " SELECT INV_ISSUE_HDR.*,INV_ISSUE_DET.*,INV_ITEM_MST.ITEM_SHORT_DESC, " & vbCrLf _
            & " PAY_EMPLOYEE_MST.EMP_NAME,PAY_DEPT_MST.DEPT_DESC " & vbCrLf _
            & " FROM INV_ISSUE_HDR,INV_ISSUE_DET, TEMP_STOCK_TRN,INV_ITEM_MST, " & vbCrLf _
            & " PAY_EMPLOYEE_MST,PAY_DEPT_MST " & vbCrLf _
            & " WHERE INV_ISSUE_HDR.AUTO_KEY_ISS=INV_ISSUE_DET.AUTO_KEY_ISS(+) " & vbCrLf _
            & " AND INV_ISSUE_DET.AUTO_KEY_ISS=TEMP_STOCK_TRN.AUTO_KEY_ISS(+) " & vbCrLf _
            & " AND INV_ISSUE_DET.ITEM_CODE=TEMP_STOCK_TRN.ITEM_CODE(+) " & vbCrLf _
            & " AND INV_ISSUE_DET.COMPANY_CODE=INV_ITEM_MST.COMPANY_CODE(+) " & vbCrLf _
            & " AND INV_ISSUE_DET.ITEM_CODE=INV_ITEM_MST.ITEM_CODE(+) " & vbCrLf _
            & " AND INV_ISSUE_HDR.COMPANY_CODE=PAY_EMPLOYEE_MST.COMPANY_CODE(+) " & vbCrLf _
            & " AND INV_ISSUE_HDR.EMP_CODE=PAY_EMPLOYEE_MST.EMP_CODE(+) " & vbCrLf _
            & " AND INV_ISSUE_HDR.COMPANY_CODE=PAY_DEPT_MST.COMPANY_CODE(+) " & vbCrLf _
            & " AND INV_ISSUE_HDR.DEPT_CODE=PAY_DEPT_MST.DEPT_CODE(+) " & vbCrLf _
            & " AND INV_ISSUE_HDR.AUTO_KEY_ISS=" & Val(txtReqNo.Text) & ""

        SqlStr = SqlStr & vbCrLf & " AND TEMP_STOCK_TRN.USERID='" & mUserMKey & "'"

        If lblBookType.Text = "I" Then
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\StoreIssueBOP.rpt"
        Else
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\StoreReqBOP.rpt"
        End If
        SetCrpt(Report1, Mode, 1, mTitle, , True, xMyMenu)
        Report1.SQLQuery = SqlStr
        Report1.WindowShowGroupTree = False

        'Dim prt As Printer
        'If PubUniversalPrinter = "Y" And Mode = Crystal.DestinationConstants.crptToPrinter Then
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

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = " DELETE FROM TEMP_STOCK_TRN WHERE USERID='" & mUserMKey & "'"
        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If UpdateMain1() = True Then
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
        Dim mDivisionCode As Double

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ISSUE_TYPE='N' AND IS_SUPP_ISSUE='" & lblIsSuppIssue.Text & "' AND ISSUE_STATUS='N' AND SUBSTR(AUTO_KEY_ISS,LENGTH(AUTO_KEY_ISS)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        If cboDivision.Text <> "" Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
            SqlStr = SqlStr & " AND DIV_CODE=" & mDivisionCode & ""
        End If

        If MainClass.SearchGridMaster((txtReqNo.Text), "INV_ISSUE_HDR", "AUTO_KEY_ISS", "ISSUE_DATE", "ISSUE_FOR", , SqlStr) = True Then
            txtReqNo.Text = AcName
            txtReqNo_Validating(txtReqNo, New System.ComponentModel.CancelEventArgs(False))
            If txtReqNo.Enabled = True Then txtReqNo.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdSearchDept_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchDept.Click
        Call TxtDept_DoubleClick(txtDept, New System.EventArgs())
    End Sub

    Private Sub cmdSearchCC_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchCC.Click
        Call txtCost_DoubleClick(txtCost, New System.EventArgs())
    End Sub

    Private Sub cmdSearchEmp_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchEmp.Click
        Call txtEmp_DoubleClick(txtEmp, New System.EventArgs())
    End Sub

    Private Sub cmdSearchProduct_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchProduct.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mDivisionCode As Double

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((txtProductCode.Text), "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", "", , SqlStr) = True Then
            txtProductCode.Text = AcName
            lblProductDesc.Text = AcName1
            txtProductCode_Validating(txtProductCode, New System.ComponentModel.CancelEventArgs(False))
            If txtProductCode.Enabled = True Then txtProductCode.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdUpdateIssue_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdUpdateIssue.Click
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim mDemandQty As Double
        Dim mStockQty As Double
        Dim mIssueQty As Double

        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow
                .Col = ColDemandQty
                mDemandQty = Val(.Text)

                .Col = ColStockQty
                mStockQty = Val(.Text)

                mIssueQty = IIf(mDemandQty > mStockQty, mStockQty, mDemandQty)

                mIssueQty = IIf(mIssueQty < 0, 0, mIssueQty)

                .Col = ColIssueQty
                If Val(.Text) = 0 Then
                    .Text = VB6.Format(mIssueQty, "0.0000")
                End If
            Next
        End With

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
NormalExit:
    End Sub

    Private Sub FrmStoreReqBOP_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim xIName As String
        Dim SqlStr As String = ""
        Dim mItemCode As String
        Dim mHeatNo As String
        Dim mBatchNo As String
        Dim mUOM As String = ""
        Dim mStockType As String = "" '

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If

        If lblBookType.Text = "I" Then Exit Sub

        If eventArgs.row = 0 And eventArgs.col = ColItemCode Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemCode

                If RsCompany.Fields("COMPANY_CODE").Value = 12 Then
                    SqlStr = GetStockItemQry(.Text, "Y", VB6.Format(txtReqDate.Text, "DD/MM/YYYY"), ConWH)
                    If MainClass.SearchGridMasterBySQL2(.Text, SqlStr, "Y", "1") = True Then
                        .Row = .ActiveRow
                        .Col = ColItemCode
                        .Text = Trim(AcName)
                    End If
                Else
                    If Trim(txtDept.Text) <> "" And Trim(txtProductCode.Text) <> "" Then
                        SqlStr = " SELECT ID.RM_CODE, ID.STD_QTY, INVMST.ITEM_SHORt_DESC" & vbCrLf _
                            & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf _
                            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IH.MKEY=ID.MKEY " & vbCrLf _
                            & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
                            & " AND ID.RM_CODE=INVMST.ITEM_CODE " & vbCrLf _
                            & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote((txtProductCode.Text)) & "' " & vbCrLf _
                            & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote((txtDept.Text)) & "' " & vbCrLf _
                            & " AND IH.STATUS='O'"

                        SqlStr = SqlStr & vbCrLf & " UNION " & vbCrLf _
                            & " SELECT ID.ALTER_RM_CODE AS RM_CODE, ID.ALTER_STD_QTY AS STD_QTY, INVMST.ITEM_SHORt_DESC" & vbCrLf _
                            & " FROM PRD_NEWBOM_HDR IH, PRD_BOM_ALTER_DET ID, INV_ITEM_MST INVMST" & vbCrLf _
                            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IH.MKEY=ID.MKEY " & vbCrLf _
                            & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
                            & " AND ID.MAINITEM_CODE=INVMST.ITEM_CODE " & vbCrLf _
                            & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote((txtProductCode.Text)) & "' " & vbCrLf _
                            & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote((txtDept.Text)) & "' " & vbCrLf _
                            & " AND IH.STATUS='O'"

                        '                    If Trim(.Text) <> "" Then
                        '                        SqlStr = SqlStr & " AND MAINITEM_CODE='" & MainClass.AllowSingleQuote(.Text) & "'"
                        '                    End If

                        If MainClass.SearchGridMasterBySQL2(.Text, SqlStr, "Y", "1") = True Then
                            .Row = .ActiveRow
                            .Col = ColItemCode
                            .Text = Trim(AcName)

                            .Col = colStdQty
                            .Text = Trim(AcName1)

                        End If
                    Else
                        If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS='A' AND IS_CHILD='N'") = True Then
                            .Row = .ActiveRow
                            .Col = ColItemCode
                            .Text = Trim(AcName)
                        End If
                    End If
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

                If RsCompany.Fields("COMPANY_CODE").Value = 12 Then
                    SqlStr = GetStockItemQry(xIName, "N", VB6.Format(txtReqDate.Text, "DD/MM/YYYY"), ConWH)
                    If MainClass.SearchGridMasterBySQL2(.Text, SqlStr, "Y", "2") = True Then
                        .Row = .ActiveRow
                        .Col = ColItemDesc
                        .Text = Trim(AcName)
                    End If
                Else
                    If Trim(txtDept.Text) <> "" And Trim(txtProductCode.Text) <> "" Then
                        SqlStr = " SELECT INVMST.ITEM_SHORT_DESC, ID.ALTER_STD_QTY, ID.ALTER_RM_CODE" & vbCrLf _
                            & " FROM PRD_NEWBOM_HDR IH, PRD_BOM_ALTER_DET ID, INV_ITEM_MST INVMST" & vbCrLf _
                            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IH.MKEY=ID.MKEY " & vbCrLf _
                            & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
                            & " AND ID.MAINITEM_CODE=INVMST.ITEM_CODE " & vbCrLf _
                            & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "' " & vbCrLf _
                            & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "' " & vbCrLf _
                            & " AND IH.STATUS='O'"

                        If Trim(.Text) <> "" Then
                            SqlStr = SqlStr & " AND INVMST.ITEM_SHORT_DESC='" & MainClass.AllowSingleQuote(.Text) & "'"
                        End If

                        If MainClass.SearchGridMasterBySQL2(.Text, SqlStr, "N") = True Then
                            .Row = .ActiveRow
                            .Col = ColItemDesc
                            .Text = Trim(AcName)

                            .Col = colStdQty
                            .Text = Trim(AcName1)

                        End If
                    Else
                        If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            .Row = .ActiveRow
                            .Col = ColItemDesc
                            .Text = Trim(AcName)
                        Else
                            .Row = .ActiveRow
                            .Col = ColItemDesc
                            .Text = xIName
                        End If
                    End If
                End If
                If MainClass.ValidateWithMasterTable(.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow
                    .Col = ColItemCode
                    .Text = Trim(MasterNo)
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            End With
        End If

        'CODED AS ON [01-02-2022] FOR HEATNO WISE SEARCH BY RSS
        If eventArgs.row = 0 And eventArgs.col = ColHeatNo Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColHeatNo
                mHeatNo = Trim(.Text)

                .Col = ColUom
                mUOM = Trim(.Text)

                .Col = ColStockType
                mStockType = Trim(.Text)

                .Col = ColHeatNo
                SqlStr = GetItemHeatWiseQry(mItemCode, (txtReqDate.Text), mUOM, "STR", mStockType, mHeatNo, ConWH, ConStockRefType_ISS, Val(txtReqNo.Text))
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColHeatNo
                    .Text = Trim(AcName1)
                End If

                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColHeatNo)
            End With
        End If
        'END OF CODED AS ON [01-02-2022] FOR HEATNO WISE SEARCH BY RSS

        If eventArgs.row = 0 And eventArgs.col = ColBatchNo Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColBatchNo
                mBatchNo = Trim(.Text)

                .Col = ColUom
                mUOM = Trim(.Text)

                .Col = ColStockType
                mStockType = Trim(.Text)

                .Col = ColBatchNo
                SqlStr = GetItemLotWiseQry(mItemCode, (txtReqDate.Text), mUOM, "STR", mStockType, mBatchNo, ConWH, ConStockRefType_ISS, Val(txtReqNo.Text))
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColBatchNo
                    .Text = Trim(AcName1)
                End If

                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColBatchNo)
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
        Dim mWIPStock As Double
        Dim mStdQty As Double
        Dim mDemandedQty As Double
        Dim xLotNo As String
        Dim mProdType As String
        Dim mCheckProdType As String
        Dim mDivisionCode As Double
        Dim mCommonDivision As Double
        Dim mStockQty As Double
        Dim mAutoQCIssue As String

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

                SprdMain.Col = ColItemCode
                xItemCode = Trim(SprdMain.Text)
                If xItemCode = "" Then Exit Sub

                SprdMain.Col = ColUom
                xItemUOM = Trim(SprdMain.Text)

                SprdMain.Col = ColDemandQty
                If Val(SprdMain.Text) = 0 Then Exit Sub

                If CheckQty() = True Then
                    SprdMain.Row = SprdMain.ActiveRow
                    SprdMain.Col = ColStockQty
                    xStockQty = Val(SprdMain.Text)



                    SprdMain.Col = ColDemandQty
                    mDemandedQty = Val(SprdMain.Text)
                    If Val(CStr(mDemandedQty)) <> 0 Then

                        mProdType = GetProductionType(xItemCode)
                        If mProdType = "P" Or mProdType = "B" Or mProdType = "I" Or mProdType = "R" Or mProdType = "3" Then

                        Else
                            If xStockQty < Val(CStr(mDemandedQty)) Then
                                MsgInformation("You have not enough Stock. Demanded Qty : " & mDemandedQty & " " & xItemUOM & " and you have Stock : " & xStockQty & " " & xItemUOM & ".")
                                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColDemandQty)
                                eventArgs.cancel = True
                                Exit Sub
                            End If
                        End If

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
            Case ColBatchNo
                If DuplicateItem(ColBatchNo) = False Then
                    SprdMain.Row = SprdMain.ActiveRow

                    SprdMain.Col = ColItemCode
                    xItemCode = Trim(SprdMain.Text)

                    If MainClass.ValidateWithMasterTable(xItemCode, "ITEM_CODE", "AUTO_INDENT", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AUTO_INDENT='Y'") = True Then
                        mAutoQCIssue = "Y"
                    Else
                        mAutoQCIssue = "N"
                    End If

                    SprdMain.Col = ColUom
                    xItemUOM = Trim(SprdMain.Text)

                    SprdMain.Col = ColBatchNo
                    xLotNo = Trim(SprdMain.Text)

                    SprdMain.Col = ColStockType
                    xStockType = Trim(SprdMain.Text)
                    If xStockType = "" Then Exit Sub


                    SprdMain.Col = ColIssuedQty
                    mIssuedQty = Val(SprdMain.Text)

                    SprdMain.Col = ColStockQty ''mIssuedQty +
                    mCommonDivision = GetCommonDivCode()
                    mStockQty = GetBalanceStockQty(xItemCode, (txtReqDate.Text), xItemUOM, "STR", xStockType, xLotNo, ConWH, mDivisionCode, ConStockRefType_ISS, Val(txtReqNo.Text))
                    If RsCompany.Fields("COMPANY_CODE").Value = 1 And mAutoQCIssue = "N" Then
                        mStockQty = mStockQty + GetBalanceStockQty(xItemCode, (txtReqDate.Text), xItemUOM, "STR", "QC", xLotNo, ConWH, mDivisionCode, ConStockRefType_ISS, Val(txtReqNo.Text))
                    End If
                    If mDivisionCode <> mCommonDivision Then
                        If mCommonDivision > 0 Then
                            mStockQty = mStockQty + GetBalanceStockQty(xItemCode, (txtReqDate.Text), xItemUOM, "STR", xStockType, xLotNo, ConWH, mCommonDivision, ConStockRefType_ISS, Val(txtReqNo.Text))
                        End If
                    End If
                    mStockQty = mStockQty - GetUnApprovedQty(xItemCode, mDivisionCode)
                    SprdMain.Text = CStr(mStockQty)


                    SprdMain.Col = ColDeptQty
                    mWIPStock = GetBalanceStockQty(xItemCode, (txtReqDate.Text), xItemUOM, (txtDept.Text), "ST", "", ConPH, mDivisionCode, ConStockRefType_ISS, Val(txtReqNo.Text))
                    '                mWIPStock = mWIPStock + GetProductionStock(xItemCode, txtDept.Text, mDivisionCode, txtReqDate.Text, xItemUOM)   ''GetDeptStock(xItemCode, mDivisionCode)
                    '                mWIPStock = mWIPStock - GetDeptStock(xItemCode)
                    SprdMain.Text = VB6.Format(mWIPStock, "0.0000")

                    SprdMain.Col = colStdQty
                    mProdType = GetProductionType(xItemCode)
                    If mProdType = "P" Or mProdType = "B" Or mProdType = "I" Or mProdType = "D" Or mProdType = "3" Then
                        mStdQty = GetSTDQty(xItemCode)
                    Else
                        mStdQty = 0
                    End If
                    SprdMain.Text = VB6.Format(mStdQty, "0.0000")
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
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColStockType)
                    eventArgs.cancel = True
                    Exit Sub
                End If


            Case ColStockType
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColItemCode
                xItemCode = Trim(SprdMain.Text)

                If MainClass.ValidateWithMasterTable(xItemCode, "ITEM_CODE", "AUTO_INDENT", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AUTO_INDENT='Y'") = True Then
                    mAutoQCIssue = "Y"
                Else
                    mAutoQCIssue = "N"
                End If

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
                    If xStockType = "FG" Then
                        MsgInformation("Can't be Selected FG Stock Type")
                        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColStockType)
                        eventArgs.cancel = True
                        Exit Sub
                    End If

                    If xStockType = "CR" Then
                        MsgInformation("Can't be Selected CR Stock Type")
                        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColStockType)
                        eventArgs.cancel = True
                        Exit Sub
                    End If

                    If xStockType = "RJ" Then
                        MsgInformation("Can't be Selected RJ Stock Type")
                        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColStockType)
                        eventArgs.cancel = True
                        Exit Sub
                    End If

                    If xStockType = "QC" Then
                        MsgInformation("Can't be Selected QC Stock Type")
                        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColStockType)
                        eventArgs.cancel = True
                        Exit Sub
                    End If

                    SprdMain.Col = ColIssuedQty
                    mIssuedQty = Val(SprdMain.Text)

                    SprdMain.Col = ColStockQty
                    mCommonDivision = GetCommonDivCode()
                    mStockQty = GetBalanceStockQty(xItemCode, (txtReqDate.Text), xItemUOM, "STR", xStockType, "", ConWH, mDivisionCode, ConStockRefType_ISS, Val(txtReqNo.Text))

                    If RsCompany.Fields("COMPANY_CODE").Value = 1 And mAutoQCIssue = "N" Then
                        mStockQty = mStockQty + GetBalanceStockQty(xItemCode, (txtReqDate.Text), xItemUOM, "STR", "QC", "", ConWH, mDivisionCode, ConStockRefType_ISS, Val(txtReqNo.Text))
                    End If

                    If mDivisionCode <> mCommonDivision Then
                        If mCommonDivision > 0 Then
                            mStockQty = mStockQty + GetBalanceStockQty(xItemCode, (txtReqDate.Text), xItemUOM, "STR", xStockType, "", ConWH, mCommonDivision, ConStockRefType_ISS, Val(txtReqNo.Text))
                        End If
                    End If
                    mStockQty = mStockQty - GetUnApprovedQty(xItemCode, mDivisionCode)
                    SprdMain.Text = CStr(mStockQty)

                    SprdMain.Col = ColDeptQty
                    mWIPStock = GetBalanceStockQty(xItemCode, (txtReqDate.Text), xItemUOM, (txtDept.Text), "ST", "", ConPH, mDivisionCode, ConStockRefType_ISS, Val(txtReqNo.Text))
                    '                mWIPStock = mWIPStock + GetProductionStock(xItemCode, txtDept.Text, mDivisionCode, txtReqDate.Text, xItemUOM)   ''GetDeptStock(xItemCode, mDivisionCode)
                    '                mWIPStock = mWIPStock - GetDeptStock(xItemCode)
                    SprdMain.Text = VB6.Format(mWIPStock, "0.0000")

                    SprdMain.Col = colStdQty
                    mCheckProdType = GetProductionType(xItemCode)
                    '                If GetProductionType(xItemCode) = "P" Then
                    If (mCheckProdType = "P" Or mCheckProdType = "B" Or mCheckProdType = "I" Or mCheckProdType = "D" Or mCheckProdType = "3") Then
                        mStdQty = GetSTDQty(xItemCode)
                    Else
                        mStdQty = 0
                    End If
                    SprdMain.Text = VB6.Format(mStdQty, "0.0000")

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

            .Col = ColBatchNo
            mCheckLotNo = Trim(UCase(.Text))

            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColItemCode
                mItemCode = Trim(UCase(.Text))

                .Col = ColBatchNo
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
            '        .Row = mRow
            '        .Col = ColItemCode
            '        mCheckItemCode = Trim(UCase(.Text))
            '        If IsChildItemExists(mCheckItemCode) = True Then
            '            DuplicateItem = True
            '            MsgInformation mCheckItemCode & " : Item Code is parent and Child Code is Exists."
            '            MainClass.SetFocusToCell SprdMain, mRow, pCol
            '            Exit Function
            '        End If
            '        If IsParentItemExists(mCheckItemCode) = True Then
            '            DuplicateItem = True
            '            MsgInformation mCheckItemCode & " : Item Code is Child and Parent Code is Exists."
            '            MainClass.SetFocusToCell SprdMain, mRow, pCol
            '            Exit Function
            '        End If
        End With
    End Function

    Private Function CheckDuplicateItem(ByRef mCheckItemCode As String) As Boolean
        Dim cntRow As Integer
        Dim mCount As Byte
        Dim mItemCode As String
        'Dim mLotNo As String
        'Dim mCheckLotNo As String
        'Dim mRow As Integer

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColItemCode
                mItemCode = Trim(UCase(.Text))

                If (mItemCode = mCheckItemCode And mCheckItemCode <> "") Then
                    mCount = mCount + 1
                End If

                If mCount > 1 Then
                    CheckDuplicateItem = True
                    Exit Function
                End If
            Next
        End With
    End Function
    Private Function IsChildItemExists(ByRef pItemCode As String) As Boolean

        Dim cntRow As Integer
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mChildCode As String

        IsChildItemExists = False
        SqlStr = "SELECT ITEM_CODE FROM INV_ITEM_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IS_CHILD='Y' AND PARENT_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mChildCode = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))
                With SprdMain
                    For cntRow = 1 To .MaxRows - 1
                        .Row = cntRow
                        .Col = ColItemCode
                        If mChildCode = Trim(.Text) Then
                            IsChildItemExists = True
                            Exit Function
                        End If
                    Next
                End With
                RsTemp.MoveNext()
            Loop
        End If
    End Function
    Private Function IsParentItemExists(ByRef pItemCode As String) As Boolean

        Dim cntRow As Integer
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mParentcode As String

        IsParentItemExists = False

        SqlStr = "SELECT PARENT_CODE FROM INV_ITEM_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IS_CHILD='Y' AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mParentcode = Trim(IIf(IsDBNull(RsTemp.Fields("PARENT_CODE").Value), "", RsTemp.Fields("PARENT_CODE").Value))
                With SprdMain
                    For cntRow = 1 To .MaxRows - 1
                        .Row = cntRow
                        .Col = ColItemCode
                        If mParentcode = Trim(.Text) Then
                            IsParentItemExists = True
                            Exit Function
                        End If
                    Next
                End With
                RsTemp.MoveNext()
            Loop
        End If

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
        Dim mProd_Type As Boolean
        Dim xAutoIssue As Boolean
        Dim RsTempBOM As ADODB.Recordset
        Dim xItemCode As String = ""
        Dim mItemClassification As String
        Dim mTodayPlanQty As Double
        Dim mTodayIssuedQty As Double
        Dim mItemCapacity As Double

        xAutoIssue = CheckAutoIssue((txtReqDate.Text), pItemCode)

        If Trim(pItemCode) = "" Then Exit Function
        With SprdMain
            SqlStr = "SELECT ITEM_CODE, ITEM_SHORT_DESC,ISSUE_UOM,ITEM_CLASSIFICATION  " & vbCrLf & " FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

            If pIsItemCode = True Then
                SqlStr = SqlStr & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"
            Else
                SqlStr = SqlStr & vbCrLf & " AND ITEM_SHORT_DESC='" & MainClass.AllowSingleQuote(pItemCode) & "'"
            End If

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                mItemClassification = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_CLASSIFICATION").Value), "", RsTemp.Fields("ITEM_CLASSIFICATION").Value))

                If RsCompany.Fields("COMPANY_CODE").Value <> 16 Then
                    If mItemClassification = "3" Then
                        FillItemDescPart = False
                        MsgInformation("You Cann't be Issue Diesel from Issue Note.")
                        MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColItemCode)
                        Exit Function
                    End If

                    If mItemClassification = "2" Then
                        If VB.Left(cboStockFor.Text, 1) <> "S" Then
                            FillItemDescPart = False
                            MsgInformation("Please select Sub Store for CO2.")
                            MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColItemCode)
                            Exit Function
                        End If
                    End If
                End If

                If VB.Left(cboStockFor.Text, 1) <> "N" Then
                    mProd_Type = IsProductionItem(pItemCode)

                    If PubSuperUser = "S" Or PubSuperUser = "A" Then
                        If xAutoIssue = True Then
                            If mProd_Type = True Then
                                FillItemDescPart = False
                                'MsgQuestion("Want to Delete ? ") = vbYes Then
                                If MsgQuestion("Auto Issue defined, Want to Issue BOP & Jobwork Items ?") = CStr(MsgBoxResult.No) Then
                                    MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColItemCode)
                                    Exit Function
                                End If
                            End If
                        End If
                    Else
                        If xAutoIssue = True Then
                            If mProd_Type = True Then
                                FillItemDescPart = False
                                MsgInformation("Auto Issue defined, so Cann't be Issue BOP & Jobwork Items")
                                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColItemCode)
                                Exit Function
                            End If
                        End If
                    End If
                End If

                .Row = .ActiveRow
                .Col = ColItemCode
                .Text = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))
                xItemCode = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))

                .Col = ColItemDesc
                .Text = IIf(IsDBNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value)

                .Col = ColUom
                .Text = IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)

                .Col = ColStockType
                .Text = IIf(Trim(.Text) = "", "ST", Trim(.Text))

                .Col = ColTodayPlanQty
                mTodayPlanQty = GetPlanedQty(xItemCode)

                .Text = VB6.Format(mTodayPlanQty, "0.00")

                .Col = ColItemCapacity
                mItemCapacity = GetItemCapacity(xItemCode)

                .Text = VB6.Format(mItemCapacity, "0.00")

                .Col = ColTodayIssued
                mTodayIssuedQty = GetTodayIssueQty(xItemCode)

                .Text = VB6.Format(mTodayIssuedQty, "0.00")

                '            If Trim(txtDept.Text) <> "" And Trim(txtProductCode.Text) <> "" Then
                '                SqlStr = " SELECT ID.RM_CODE, ID.STD_QTY" & vbCrLf _
                ''                        & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID" & vbCrLf _
                ''                        & " WHERE IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND IH.MKEY=ID.MKEY " & vbCrLf _
                ''                        & " AND " & vbCrLf _
                ''                        & " IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "' " & vbCrLf _
                ''                        & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "' " & vbCrLf _
                ''                        & " AND ID.RM_CODE='" & MainClass.AllowSingleQuote(xItemCode) & "'" & vbCrLf _
                ''                        & " AND IH.STATUS='O'"
                '
                '                MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTempBOM, adLockReadOnly
                '
                '                If RsTempBOM.EOF = False Then
                '                    .Col = ColStdQty
                '                    .Text = IIf(IsNull(RsTempBOM!STD_QTY), 0, RsTempBOM!STD_QTY)
                '                Else
                '
                '                    SqlStr = " SELECT ID.ALTER_STD_QTY" & vbCrLf _
                ''                        & " FROM PRD_NEWBOM_HDR IH, PRD_BOM_ALTER_DET ID" & vbCrLf _
                ''                        & " WHERE IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND IH.MKEY=ID.MKEY " & vbCrLf _
                ''                        & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "' " & vbCrLf _
                ''                        & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "' " & vbCrLf _
                ''                        & " AND ID.ALTER_RM_CODE='" & MainClass.AllowSingleQuote(xItemCode) & "'" & vbCrLf _
                ''                        & " AND IH.STATUS='O'"
                '
                '                    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTempBOM, adLockReadOnly
                '                    If RsTempBOM.EOF = False Then
                '                        .Col = ColStdQty
                '                        .Text = IIf(IsNull(RsTempBOM!ALTER_STD_QTY), 0, RsTempBOM!ALTER_STD_QTY)
                '                    Else
                '                        If CDate(txtReqDate.Text) >= CDate("08/12/2014") Then
                '                            MsgInformation "Invalid Item Code for Product Code : " & txtProductCode.Text
                '                             FillItemDescPart = False
                '                             MainClass.SetFocusToCell SprdMain, .ActiveRow, ColItemCode
                '                             Exit Function
                '                        End If
                '                    End If
                '                End If
                '            End If
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
        ''Resume
        FillItemDescPart = False
        MsgInformation(Err.Description)
    End Function

    Private Sub SprdMain_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles SprdMain.Validating
        'Dim Cancel As Boolean = eventArgs.Cancel
        'Dim mCancel As Boolean
        '    mCancel = False
        '    With SprdMain
        '        SprdMain_LeaveCell .ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, mCancel
        '        Cancel = mCancel
        '    End With
        'eventArgs.Cancel = Cancel
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
        SqlStr = "SELECT Max(AUTO_KEY_ISS)  " & vbCrLf & " FROM INV_ISSUE_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_ISS,LENGTH(AUTO_KEY_ISS)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

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
        Dim mDivisionCode As Double
        Dim mDivisionDesc As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mStatus = IIf(chkIssue.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        If Val(txtReqNo.Text) = 0 Then
            mVNoSeq = AutoGenSeqNo()
        Else
            mVNoSeq = Val(txtReqNo.Text)
        End If

        txtReqNo.Text = CStr(Val(CStr(mVNoSeq)))

        mEntryDate = VB6.Format(PubCurrDate, "DD-MMM-YYYY") & " " & GetServerTime()

        mDivisionDesc = cboDivision.Text
        If MainClass.ValidateWithMasterTable(mDivisionDesc, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Val(MasterNo)
        End If


        SqlStr = ""
        If ADDMode = True Then
            lblMKey.Text = CStr(mVNoSeq)
            SqlStr = "INSERT INTO INV_ISSUE_HDR (" & vbCrLf & " AUTO_KEY_ISS, " & vbCrLf & " COMPANY_CODE, " & vbCrLf & " ISSUE_DATE, " & vbCrLf & " DEPT_CODE, " & vbCrLf & " EMP_CODE, REMARKS, COST_CENTER_CODE, DAILY_PLAN_NO, " & vbCrLf & " SHIFT_CODE,ISSUE_STATUS, ISSUE_FOR, " & vbCrLf & " ADDUSER,ADDDATE,MODUSER,MODDATE,DIV_CODE,INHOUSE_CODE,IS_SUPP_ISSUE," & vbCrLf & " PROD_PLAN_QTY,ISSUE_TYPE,SUPP_REASON, WEEKLY_DESP_QTY)" & vbCrLf & " VALUES( " & vbCrLf & " " & Val(CStr(mVNoSeq)) & "," & vbCrLf & " " & RsCompany.Fields("Company_Code").Value & "," & vbCrLf & " TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & MainClass.AllowSingleQuote((txtDept.Text)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtEmp.Text)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtsubdept.Text)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtCost.Text)) & "', " & vbCrLf & " NULL, " & vbCrLf & " '" & MainClass.AllowSingleQuote((cboShiftcd.Text)) & "', " & vbCrLf & " '" & mStatus & "', '" & VB.Left(cboStockFor.Text, 1) & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & mEntryDate & "','DD-MON-YYYY HH24:MI'),''," & vbCrLf & " ''," & mDivisionCode & ",'" & MainClass.AllowSingleQuote((txtProductCode.Text)) & "','" & lblIsSuppIssue.Text & "'," & Val(txtRequestQty.Text) & ",'N'," & vbCrLf & " '" & IIf(VB.Left(cboSuppReason.Text, 1) = "", "0", VB.Left(cboSuppReason.Text, 1)) & "', " & Val(txtPlanQty.Text) & ")"

            ''VB6.Format(PubCurrDate, "DD-MMM-YYYY")
        ElseIf MODIFYMode = True Then
            SqlStr = ""
            SqlStr = "UPDATE INV_ISSUE_HDR SET ISSUE_DATE=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " DEPT_CODE='" & txtDept.Text & "', INHOUSE_CODE='" & MainClass.AllowSingleQuote((txtProductCode.Text)) & "'," & vbCrLf & " EMP_CODE ='" & txtEmp.Text & "', PROD_PLAN_QTY=" & Val(txtRequestQty.Text) & "," & vbCrLf & " REMARKS ='" & MainClass.AllowSingleQuote((txtsubdept.Text)) & "'," & vbCrLf & " COST_CENTER_CODE ='" & txtCost.Text & "'," & vbCrLf & " SHIFT_CODE ='" & MainClass.AllowSingleQuote((cboShiftcd.Text)) & "', " & vbCrLf & " ISSUE_FOR ='" & VB.Left(cboStockFor.Text, 1) & "', " & vbCrLf & " ISSUE_STATUS ='" & mStatus & "', IS_SUPP_ISSUE='" & lblIsSuppIssue.Text & "'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',SUPP_REASON='" & VB.Left(cboSuppReason.Text, 1) & "'," & vbCrLf & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),DIV_CODE=" & mDivisionCode & ",ISSUE_TYPE='N', " & vbCrLf & " WEEKLY_DESP_QTY=" & Val(txtPlanQty.Text) & "" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND AUTO_KEY_ISS =" & Val(lblMKey.Text) & ""
        End If
        PubDBCn.Execute(SqlStr)
        If UpdateDetail1(CStr(Val(CStr(mVNoSeq))), mDivisionCode) = False Then GoTo ErrPart


        ''    If lblBookType.text = "I" And Left(cboStockFor, 1) = "P" Then
        ''          If UpdateMtrlIssProd = False Then GoTo ErrPart
        ''    End If


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
        '    Resume
    End Function
    Private Function UpdateMtrlIssProd() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mIssueNo As Double
        Dim mStatus As String


        If MainClass.ValidateWithMasterTable(lblMKey.Text, "AUTO_KEY_ISS", "AUTO_KEY_ISS", "PRD_ISSREC_HDR", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & " ") = False Then
            mStatus = "N"
            mIssueNo = AutoGenKeyIssRec()
            SqlStr = ""
            SqlStr = "INSERT INTO PRD_ISSREC_HDR (" & vbCrLf & " COMPANY_CODE,FYEAR,AUTO_KEY_ISSREC, " & vbCrLf & " ISSREC_DATE,FROM_DEPT,TO_DEPT, " & vbCrLf & " ISS_EMP_CODE,RECV_EMP_CODE, " & vbCrLf & " COST_CENTER_CODE,SHIFT_CODE,RECV_STATUS, " & vbCrLf & " REMARKS,AUTO_KEY_ISS, " & vbCrLf & " ADDUSER,ADDDATE,MODUSER,MODDATE)" & vbCrLf & " VALUES( " & vbCrLf & " " & RsCompany.Fields("Company_Code").Value & "," & vbCrLf & " " & RsCompany.Fields("FYEAR").Value & "," & vbCrLf & " " & Val(CStr(mIssueNo)) & ",TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " 'STR', " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtDept.Text)) & "', " & vbCrLf & " '','" & MainClass.AllowSingleQuote((txtEmp.Text)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtCost.Text)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote((cboShiftcd.Text)) & "', " & vbCrLf & " '" & mStatus & "','" & MainClass.AllowSingleQuote((txtsubdept.Text)) & "'," & Val(lblMKey.Text) & ",  " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'','')"
        Else
            SqlStr = "UPDATE PRD_ISSREC_HDR SET " & vbCrLf & " TO_DEPT='" & MainClass.AllowSingleQuote(txtDept.Text) & "', " & vbCrLf & " RECV_EMP_CODE='" & MainClass.AllowSingleQuote(txtEmp.Text) & "', " & vbCrLf & " COST_CENTER_CODE='" & MainClass.AllowSingleQuote(txtCost.Text) & "', " & vbCrLf & " SHIFT_CODE='" & MainClass.AllowSingleQuote(cboShiftcd.Text) & "', " & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND AUTO_KEY_ISS =" & Val(lblMKey.Text) & ""
        End If
        PubDBCn.Execute(SqlStr)
        If UpdateMtrlIssProdDetail1(mIssueNo) = False Then GoTo ErrPart
        UpdateMtrlIssProd = True
        Exit Function
ErrPart:
        UpdateMtrlIssProd = False
        If Err.Description = "" Then Exit Function
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function


    Private Function UpdateMtrlIssProdDetail1(ByRef pIssueNo As Double) As Boolean

        On Error GoTo UpdateDetail1Err
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim mItemCode As String
        Dim mUOM As String = ""
        Dim mStockType As String = ""
        Dim mIssueQty As Double
        Dim mRemarks As String
        Dim mFirstTime As Boolean

        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColUom
                mUOM = MainClass.AllowSingleQuote(.Text)

                .Col = ColStockType
                mStockType = MainClass.AllowSingleQuote(.Text)

                .Col = ColIssueQty
                mIssueQty = Val(.Text)

                .Col = ColRemarks
                mRemarks = MainClass.AllowSingleQuote(.Text)
                SqlStr = ""

                If mItemCode <> "" Then
                    If MainClass.ValidateWithMasterTable(lblMKey.Text, "AUTO_KEY_ISS", "AUTO_KEY_ISS", "PRD_ISSREC_DET", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & " ") = False Or mFirstTime = True Then
                        mFirstTime = True
                        SqlStr = " INSERT INTO PRD_ISSREC_DET ( " & vbCrLf & " COMPANY_CODE,AUTO_KEY_ISSREC,SERIAL_NO,ITEM_CODE,ITEM_UOM,FROM_STOCK_TYPE, " & vbCrLf & " ISSUE_QTY,RECV_QTY,OPR_CODE,NEXTOPR_CODE,REMARKS,AUTO_KEY_ISS) " & vbCrLf & " VALUES ( " & RsCompany.Fields("COMPANY_CODE").Value & "," & pIssueNo & ", " & I & "," & vbCrLf & " '" & mItemCode & "','" & mUOM & "','" & mStockType & "', " & vbCrLf & " " & mIssueQty & ",0,'','', " & vbCrLf & " '" & mRemarks & "'," & Val(lblMKey.Text) & ") "
                    Else
                        SqlStr = " UPDATE PRD_ISSREC_DET SET " & vbCrLf & " ITEM_UOM='" & mUOM & "',FROM_STOCK_TYPE='" & mStockType & "', " & vbCrLf & " ISSUE_QTY=" & mIssueQty & " " & vbCrLf & " WHERE AUTO_KEY_ISS=" & Val(lblMKey.Text) & " " & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND ITEM_CODE='" & mItemCode & "' "
                    End If
                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        UpdateMtrlIssProdDetail1 = True
        Exit Function
UpdateDetail1Err:
        UpdateMtrlIssProdDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function AutoGenKeyIssRec() As Double

        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset = Nothing
        Dim mAutoGen As Double
        Dim SqlStr As String = ""

        mAutoGen = 1
        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_ISSREC)  " & vbCrLf & " FROM PRD_ISSREC_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAutoGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsAutoGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mAutoGen = CDbl(Mid(.Fields(0).Value, 1, Len(.Fields(0).Value) - 6))
                    mAutoGen = mAutoGen + 1
                Else
                    mAutoGen = 1
                End If
            End If
        End With
        AutoGenKeyIssRec = CDbl(mAutoGen & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"))
        RsAutoGen.Close()
        RsAutoGen = Nothing
        Exit Function
AutogenErr:
        MsgBox(Err.Description)
    End Function

    Private Function UpdateDetail1(ByRef pVnoseq As String, ByRef mDivisionCode As Double) As Boolean

        On Error GoTo UpdateDetail1Err
        Dim RsBOM As ADODB.Recordset = Nothing
        Dim mSqlStr As String
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim mItemCode As String
        Dim mUOM As String = ""
        Dim mStockType As String = ""
        Dim mQty As Double
        Dim mRemarks As String
        Dim mIssueQty As Double
        Dim MBATCHNORequied As String
        Dim mProd_Type As String
        Dim mIsConsumable As String = "N"
        Dim mHeatNo As String
        Dim mBatchNo As String

        Dim cntRow As Integer
        Dim pRow As Integer
        Dim xSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing

        Dim xItemCode As String = ""
        Dim xChildStock As Double
        Dim mBalIssueQty As Double
        Dim xChildIssue As Double
        Dim cntStockSno As Integer
        Dim mCommonDivision As Double
        Dim mCommonDivisionStock As Double
        Dim mIssueDivisionStock As Double
        Dim mSno As Double
        Dim mBalQty As Double
        Dim mTodayPlaned As Double
        Dim mItemCapacity As Double

        SqlStr = " Delete From INV_ISSUE_DET WHERE AUTO_KEY_ISS=" & Val(lblMKey.Text) & ""
        PubDBCn.Execute(SqlStr)

        If lblBookType.Text = "I" Then
            If DeleteStockTRN(PubDBCn, ConStockRefType_ISS, (txtReqNo.Text)) = False Then GoTo UpdateDetail1Err

            If DeletePaintStockTRN(PubDBCn, ConStockRefType_ISS, (txtReqNo.Text)) = False Then GoTo UpdateDetail1Err

            PubDBCn.Execute("DELETE FROM DSP_PAINT57F4_TRN WHERE MKey='" & lblMKey.Text & "' AND BookType='D' AND BookSubType='O' AND TRNTYPE='I'")
        End If

        mSno = 5000
        pRow = 1
        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I

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

                .Col = ColHeatNo
                mHeatNo = MainClass.AllowSingleQuote(Trim(.Text))

                .Col = ColBatchNo
                mBatchNo = MainClass.AllowSingleQuote(Trim(.Text))

                mProd_Type = GetProductionType(mItemCode)

                .Col = ColRemarks
                mRemarks = MainClass.AllowSingleQuote(.Text)

                .Col = ColTodayPlanQty
                mTodayPlaned = Val(.Text)

                .Col = ColItemCapacity
                mItemCapacity = Val(.Text)

                SqlStr = ""

                If mItemCode <> "" And mQty > 0 Then
                    SqlStr = " INSERT INTO INV_ISSUE_DET (AUTO_KEY_ISS,SERIAL_NO,ITEM_CODE,ITEM_UOM,REMARKS,FROM_STOCK_TYPE," & vbCrLf _
                            & " DEMAND_QTY,ISSUE_QTY, COMPANY_CODE, HEAT_NO, BATCH_NO, WEEK_BOP_DESP_QTY, ITEM_CAPACITY) " & vbCrLf

                    SqlStr = SqlStr & " VALUES (" & Val(lblMKey.Text) & ", " & pRow & "," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mItemCode) & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mUOM) & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mRemarks) & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mStockType) & "', " & vbCrLf _
                        & " " & mQty & "," & mIssueQty & ", " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf _
                        & " '" & mHeatNo & "','" & mBatchNo & "'," & mTodayPlaned & "," & mItemCapacity & ") "
                    PubDBCn.Execute(SqlStr)

                    pRow = pRow + 1

                    mBatchNo = mBatchNo

                    If lblBookType.Text = "I" Then

                        xSqlStr = "SELECT ITEM_CODE FROM INV_ITEM_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IS_CHILD='Y' AND PARENT_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
                        MainClass.UOpenRecordSet(xSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                        If RsTemp.EOF = False Then
                            cntStockSno = 500
                            Do While RsTemp.EOF = False
                                cntStockSno = cntStockSno + 1
                                If mBalIssueQty > 0 Then
                                    xItemCode = IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)
                                    xChildStock = GetBalanceStockQty(xItemCode, (txtReqDate.Text), mUOM, "STR", mStockType, mBatchNo, ConWH, mDivisionCode, ConStockRefType_ISS, Val(txtReqNo.Text))
                                    If mBalIssueQty >= xChildStock Then
                                        xChildIssue = xChildStock
                                        mBalIssueQty = mBalIssueQty - xChildStock
                                    Else
                                        xChildIssue = mBalIssueQty
                                        mBalIssueQty = 0
                                    End If
                                    If UpdateStockTRN(PubDBCn, ConStockRefType_ISS, (txtReqNo.Text), I + cntStockSno, (txtReqDate.Text), (txtReqDate.Text), mStockType, xItemCode, mUOM, mBatchNo, xChildIssue, 0, "O", 0, 0, "", "", "STR", (txtDept.Text), "", "N", "To : " & lblDeptname.Text, "-1", ConWH, mDivisionCode, VB.Left(cboStockFor.Text, 1), "", "", mHeatNo) = False Then GoTo UpdateDetail1Err

                                    If mProd_Type = "P" Or mProd_Type = "J" Or mProd_Type = "C" Or mProd_Type = "R" Or mProd_Type = "B" Or mProd_Type = "I" Or mProd_Type = "3" Then

                                        If RsCompany.Fields("COMPANY_CODE").Value = 1 And CDate(txtReqDate.Text) < CDate("01-DEC-2005") Then GoTo NextRec
                                        cntStockSno = cntStockSno + 1
                                        If UpdateStockTRN(PubDBCn, ConStockRefType_ISS, (txtReqNo.Text), I + cntStockSno, (txtReqDate.Text), (txtReqDate.Text), mStockType, xItemCode, mUOM, mBatchNo, xChildIssue, 0, "I", 0, 0, "", "", (txtDept.Text), (txtDept.Text), "", "N", "From : STORE TO :" & lblDeptname.Text, "-1", ConPH, mDivisionCode, VB.Left(cboStockFor.Text, 1), "", "", mHeatNo) = False Then GoTo UpdateDetail1Err
                                    End If
                                End If
                                RsTemp.MoveNext()
                            Loop
                            If mBalIssueQty > 0 Then
                                If UpdateStockTRN(PubDBCn, ConStockRefType_ISS, (txtReqNo.Text), I, (txtReqDate.Text), (txtReqDate.Text), mStockType, mItemCode, mUOM, mBatchNo, mBalIssueQty, 0, "O", 0, 0, "", "", "STR", (txtDept.Text), "", "N", "To : " & lblDeptname.Text, "-1", ConWH, mDivisionCode, VB.Left(cboStockFor.Text, 1), "", "", mHeatNo) = False Then GoTo UpdateDetail1Err

                                If mProd_Type = "P" Or mProd_Type = "J" Or mProd_Type = "C" Or mProd_Type = "R" Or mProd_Type = "B" Or mProd_Type = "I" Or mProd_Type = "3" Then

                                    If RsCompany.Fields("COMPANY_CODE").Value = 1 And CDate(txtReqDate.Text) < CDate("01-DEC-2005") Then GoTo NextRec
                                    If UpdateStockTRN(PubDBCn, ConStockRefType_ISS, (txtReqNo.Text), I, (txtReqDate.Text), (txtReqDate.Text), mStockType, mItemCode, mUOM, mBatchNo, mBalIssueQty, 0, "I", 0, 0, "", "", (txtDept.Text), (txtDept.Text), "", "N", "From : STORE TO :" & lblDeptname.Text, "-1", ConPH, mDivisionCode, VB.Left(cboStockFor.Text, 1), "", "", mHeatNo) = False Then GoTo UpdateDetail1Err
                                End If
                            End If
                        Else
                            mCommonDivisionStock = 0
                            mBalQty = 0
                            mCommonDivision = GetCommonDivCode()
                            mIssueDivisionStock = GetBalanceStockQty(mItemCode, (txtReqDate.Text), mUOM, "STR", mStockType, mBatchNo, ConWH, mDivisionCode, ConStockRefType_ISS, Val(txtReqNo.Text))
                            If mDivisionCode <> mCommonDivision Then
                                If mCommonDivision > 0 Then
                                    mCommonDivisionStock = GetBalanceStockQty(mItemCode, (txtReqDate.Text), mUOM, "STR", mStockType, mBatchNo, ConWH, mCommonDivision, ConStockRefType_ISS, Val(txtReqNo.Text))
                                End If
                            End If

                            If mIssueQty <= mIssueDivisionStock Then
                                If UpdateStockTRN(PubDBCn, ConStockRefType_ISS, (txtReqNo.Text), I, (txtReqDate.Text), (txtReqDate.Text), mStockType, mItemCode, mUOM, mBatchNo, mIssueQty, 0, "O", 0, 0, "", "", "STR", (txtDept.Text), "", "N", "To : " & lblDeptname.Text & IIf(VB.Left(cboStockFor.Text, 1) = "N", " - For Delevopment", IIf(VB.Left(cboStockFor.Text, 1) = "C", " - Capitalized", "")), "-1", ConWH, mDivisionCode, VB.Left(cboStockFor.Text, 1), "", "", mHeatNo) = False Then GoTo UpdateDetail1Err
                            Else
                                If mCommonDivision > 0 And mDivisionCode <> mCommonDivision Then
                                    If mIssueDivisionStock > 0 Then
                                        If UpdateStockTRN(PubDBCn, ConStockRefType_ISS, (txtReqNo.Text), I, (txtReqDate.Text), (txtReqDate.Text), mStockType, mItemCode, mUOM, mBatchNo, mIssueDivisionStock, 0, "O", 0, 0, "", "", "STR", (txtDept.Text), "", "N", "To : " & lblDeptname.Text & IIf(VB.Left(cboStockFor.Text, 1) = "N", " - For Delevopment", IIf(VB.Left(cboStockFor.Text, 1) = "C", " - Capitalized", "")), "-1", ConWH, mDivisionCode, VB.Left(cboStockFor.Text, 1), "", "", mHeatNo) = False Then GoTo UpdateDetail1Err

                                        mBalQty = mIssueQty - mIssueDivisionStock
                                    Else
                                        mBalQty = mIssueQty
                                    End If

                                    If mBalQty = 0 Then
                                    Else
                                        If mBalQty <= mCommonDivisionStock Then
                                            If UpdateStockTRN(PubDBCn, ConStockRefType_ISS, (txtReqNo.Text), I + mSno, (txtReqDate.Text), (txtReqDate.Text), mStockType, mItemCode, mUOM, mBatchNo, mBalQty, 0, "O", 0, 0, "", "", "STR", (txtDept.Text), "", "N", "To : " & lblDeptname.Text & IIf(VB.Left(cboStockFor.Text, 1) = "N", " - For Delevopment", IIf(VB.Left(cboStockFor.Text, 1) = "C", " - Capitalized", "")), "-1", ConWH, mCommonDivision, VB.Left(cboStockFor.Text, 1), "", "", mHeatNo) = False Then GoTo UpdateDetail1Err
                                        Else
                                            MsgInformation("Please check the stock of Item Code : " & mItemCode)
                                            UpdateDetail1 = False
                                            Exit Function
                                        End If
                                    End If
                                    mSno = mSno + 1
                                Else
                                    If UpdateStockTRN(PubDBCn, ConStockRefType_ISS, (txtReqNo.Text), I, (txtReqDate.Text), (txtReqDate.Text), mStockType, mItemCode, mUOM, mBatchNo, mIssueQty, 0, "O", 0, 0, "", "", "STR", (txtDept.Text), "", "N", "To : " & lblDeptname.Text & IIf(VB.Left(cboStockFor.Text, 1) = "N", " - For Delevopment", IIf(VB.Left(cboStockFor.Text, 1) = "C", " - Capitalized", "")), "-1", ConWH, mDivisionCode, VB.Left(cboStockFor.Text, 1), "", "", mHeatNo) = False Then GoTo UpdateDetail1Err
                                End If
                            End If
                            If mProd_Type = "P" Or mProd_Type = "J" Or mProd_Type = "C" Or mProd_Type = "R" Or mProd_Type = "B" Or mProd_Type = "I" Or mProd_Type = "3" Then

                                If RsCompany.Fields("COMPANY_CODE").Value = 1 And CDate(txtReqDate.Text) < CDate("01-DEC-2005") Then GoTo NextRec
                                If UpdateStockTRN(PubDBCn, ConStockRefType_ISS, (txtReqNo.Text), I, (txtReqDate.Text), (txtReqDate.Text), mStockType, mItemCode, mUOM, mBatchNo, mIssueQty, 0, "I", 0, 0, "", "", (txtDept.Text), (txtDept.Text), "", "N", "From : STORE TO :" & lblDeptname.Text, "-1", ConPH, mDivisionCode, VB.Left(cboStockFor.Text, 1), "", "", mHeatNo) = False Then GoTo UpdateDetail1Err
                            End If
                        End If
NextRec:
                        If VB.Left(cboStockFor.Text, 1) = "S" Then
                            If UpdateStockTRN(PubDBCn, ConStockRefType_ISS, (txtReqNo.Text), I, (txtReqDate.Text), (txtReqDate.Text), mStockType, mItemCode, mUOM, mBatchNo, mIssueQty, 0, "I", 0, 0, "", "", (txtDept.Text), "STR", "", "N", "From : STORE TO : " & lblDeptname.Text, "-1", ConSH, mDivisionCode, VB.Left(cboStockFor.Text, 1), "", "", mHeatNo) = False Then GoTo UpdateDetail1Err
                        End If

                        MBATCHNORequied = "N"
                        If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            MBATCHNORequied = MasterNo
                        End If

                        If MBATCHNORequied = "Y" Then
                            If UpdateLotInPaintStock(I, (txtReqNo.Text), (txtReqDate.Text), mItemCode, mUOM, mIssueQty, (lblDeptname.Text)) = False Then GoTo UpdateDetail1Err
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

    Private Function UpdateF4Stock(ByRef pItemCode As String, ByRef pUOM As String, ByRef pItemQty As Double, ByRef cntRow As Integer) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim pSupplierCode As String = ""
        Dim pF4No As String
        Dim pF4Date As String
        Dim mBalQty As Double
        Dim ReqdQty As Double
        Dim mF4Qty As Double
        Dim pVDate As String = ""

        SqlStr = " SELECT SUM(DECODE(ITEM_IO,'I',1,-1)*ITEM_QTY) AS ITEM_QTY, " & vbCrLf & " ITEM_CODE,PARTY_F4NO,PARTY_F4DATE " & vbCrLf & " FROM DSP_PAINT57F4_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND VDATE<=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"


        '     SqlStr = SqlStr & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pSupplierCode) & "'"

        '    If Val(txtDNNo.Text) <> 0 Then
        '        SqlStr = SqlStr & vbCrLf & " AND BILL_NO<>'" & txtDNNo.Text & "'"
        '    End If

        SqlStr = SqlStr & vbCrLf & "AND (PARTY_F4NO IS NOT NULL OR PARTY_F4NO<>0)"

        SqlStr = SqlStr & vbCrLf & "HAVING SUM(DECODE(ITEM_IO,'I',1,-1)*ITEM_QTY)>0"

        SqlStr = SqlStr & vbCrLf & " GROUP BY " & vbCrLf & " PARTY_F4NO,PARTY_F4DATE,ITEM_CODE "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            ReqdQty = pItemQty
            Do While RsTemp.EOF = False
                pF4No = IIf(IsDBNull(RsTemp.Fields("PARTY_F4NO").Value), "0", RsTemp.Fields("PARTY_F4NO").Value)
                pF4Date = VB6.Format(IIf(IsDBNull(RsTemp.Fields("PARTY_F4DATE").Value), "", RsTemp.Fields("PARTY_F4DATE").Value), "DD/MM/YYYY")
                mF4Qty = IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)

                If ReqdQty < mF4Qty Then
                    mBalQty = ReqdQty
                    ReqdQty = 0
                Else
                    mBalQty = mF4Qty
                    ReqdQty = ReqdQty - mF4Qty
                End If

                Call GetF4detailFromRGP(pF4No, pSupplierCode, pF4Date, pVDate)

                If mBalQty > 0 Then
                    If pF4No <> "" Then
                        SqlStr = "INSERT INTO DSP_PAINT57F4_TRN ( " & vbCrLf & " MKEY, COMPANY_CODE, FYEAR, " & vbCrLf & " BOOKTYPE, BOOKSUBTYPE, PARTY_F4NO, " & vbCrLf & " PARTY_F4DATE, SUPP_CUST_CODE, BILL_NO, " & vbCrLf & " BILL_DATE, ITEM_CODE,  " & vbCrLf & " ITEM_QTY, ITEM_IO, SUB_ITEM_CODE, " & vbCrLf & " SUBROWNO,BILL_QTY,TRNTYPE, VDATE,ISSCRAP) VALUES ( " & vbCrLf & " '" & lblMKey.Text & "'," & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & "," & vbCrLf & " 'D', 'O', '" & pF4No & "', " & vbCrLf & " TO_DATE('" & VB6.Format(pF4Date, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & pSupplierCode & "', '" & MainClass.AllowSingleQuote(txtReqNo.Text) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(pItemCode) & "', " & vbCrLf & " " & mBalQty & ", 'O', '" & MainClass.AllowSingleQuote(pItemCode) & "'," & vbCrLf & " " & cntRow & "," & mBalQty & ",'I',TO_DATE('" & VB6.Format(pVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'N')" & vbCrLf
                        PubDBCn.Execute(SqlStr)
                    End If

                    cntRow = cntRow + 1
                End If

                If ReqdQty = 0 Then Exit Do
                RsTemp.MoveNext()
            Loop
        End If
        UpdateF4Stock = True
        Exit Function
ErrPart:
        UpdateF4Stock = False
        If Err.Description <> "" Then MsgBox(Err.Description)
        'Resume
    End Function
    Private Sub GetF4detailFromRGP(ByRef pPartyF4No As String, ByRef pPartyCode As String, ByRef pPartyF4Date As String, ByRef pOurVDate As String)

        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing

        pPartyF4Date = ""
        pOurVDate = ""
        pPartyCode = ""

        mSqlStr = " SELECT PARTY_F4NO,PARTY_F4DATE, VDATE,SUPP_CUST_CODE " & vbCrLf & " FROM DSP_PAINT57F4_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND PARTY_F4NO='" & Trim(pPartyF4No) & "'"

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            pPartyCode = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value))
            pPartyF4Date = VB6.Format(IIf(IsDBNull(RsTemp.Fields("PARTY_F4DATE").Value), "", RsTemp.Fields("PARTY_F4DATE").Value), "DD/MM/YYYY")
            pOurVDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("VDate").Value), "", RsTemp.Fields("VDate").Value), "DD/MM/YYYY")
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


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
        Dim mQty As Double
        Dim mBalQty As Double
        Dim mUOM As String = ""
        Dim mCheckProdType As String
        Dim mDemandedQty As Double
        Dim mDeptQty As Double
        Dim mStdQty As Double
        Dim mTodayReq As Double
        Dim mPlanning As Double
        Dim mWIPStock As String = ""
        Dim mMinReq As Double
        Dim mTodayDemanded As Double
        Dim mTotTodayDemanded As Double
        Dim mDataTrue As Boolean
        Dim mString As String = ""
        Dim mTodayIssue As Double
        Dim mIssueQty As Double
        Dim mLotNoRequied As String
        Dim mDivisionCode As Double
        Dim mCommonDivision As Double
        Dim mIsDevelopmentDept As String
        Dim mProdType As String
        Dim mXCheck As Double
        Dim mItemClass As String = ""
        Dim mTodayMaterialReq As Double
        Dim mDemandQty As Double
        Dim mBalIssueQty As Double
        Dim mTodayBalanceDemand As Double
        Dim mTodayPlanQty As Double
        Dim mLineCapacity As Double
        Dim mIssuedQty As Double
        Dim mMinQty As Double
        Dim mPackingStd As Double
        Dim mWIPLockQty As Double
        Dim mNetWIPQty As Double
        'Dim mTodayIssue As Double
        Dim mFGQty As Double
        Dim mWIPCheck As Boolean
        Dim mMainItemCode As String

        xAutoIssue = CheckAutoIssue((txtReqDate.Text), "")

        FieldsVarification = False


        'If RsCompany.Fields("ISSUE_TYPE").Value = "P" Then
        '    MsgInformation("You Have No Premission to Update This Requisition Slip")
        '    FieldsVarification = False
        '    Exit Function
        'End If

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

        'If lblBookType.Text = "R" And RsCompany.Fields("COMPANY_CODE").Value = 16 Then
        '    If CheckPendingReqSlip() >= 3 Then
        '        MsgBox("There are 3 Store Requisition Slips are pending, So that you cann't be made new Store Requisition.", MsgBoxStyle.Information)
        '        FieldsVarification = False
        '        Exit Function
        '    End If
        'End If


        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        If lblBookType.Text = "R" Then
            If ValidateDeptRight(PubUserID, Trim(txtDept.Text), UCase(Trim(lblDeptname.Text))) = False Then
                FieldsVarification = False
                Exit Function
            End If

            If ValidateDivisionRight(PubUserID, mDivisionCode, UCase(Trim(cboDivision.Text))) = False Then
                FieldsVarification = False
                Exit Function
            End If

            mCommonDivision = GetCommonDivCode()

            If mCommonDivision = mDivisionCode Then
                MsgBox("Cann't be make Requisition in Common Division. Please Select the Proper Division.", MsgBoxStyle.Information)
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
        End If

        '    If PubSuperUser <> "S" Then
        If lblBookType.Text = "R" And MODIFYMode = True Then
            If CheckMaterialIssue() = True Then
                MsgBox("Material Issue Against this Store Requisition, So cann't be Save.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
        End If
        '    End If

        If lblBookType.Text = "R" Then
            If CheckStockQty(SprdMain, ColStockQty, ColDemandQty, ColItemCode, ColStockType, True, , "Y") = False Then
                FieldsVarification = False
                Exit Function
            End If
        Else
            If CheckStockQty(SprdMain, ColStockQty, ColIssueQty, ColItemCode, ColStockType, True) = False Then
                FieldsVarification = False
                Exit Function
            End If
            If CheckLotStockQty() = False Then
                FieldsVarification = False
                Exit Function
            End If
            If CheckBalDemandedQty(SprdMain, ColDemandQty, ColIssueQty) = True Then
                chkIssue.CheckState = System.Windows.Forms.CheckState.Checked
            End If
        End If

        If MODIFYMode = True Then
            If CheckDieselConsumptionEntry() = True Then
                MsgBox("You Cann't be Change This Entry, Data is Entered by Diesel Consumption.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
        End If
        ''RsCompany.fields("COMPANY_CODE").value = 1 Or         ''24-04-2011
        If MainClass.ValidateWithMasterTable(txtDept.Text, "DEPT_CODE", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DEPT_TYPE='D'") = True Then
            mIsDevelopmentDept = "Y"
        Else
            mIsDevelopmentDept = "N"
        End If

        If VB.Left(cboStockFor.Text, 1) = "N" And mIsDevelopmentDept = "N" Then
            MsgBox("Not a Development Department. Please select a Development dept.", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtDept.Enabled = True Then txtDept.Focus()
            Exit Function
        End If

        If VB.Left(cboStockFor.Text, 1) = "C" And PubSuperUser = "U" Then
            MsgBox("You have no Rights to Select such Stock Type.", MsgBoxStyle.Information)
            FieldsVarification = False
            If cboStockFor.Enabled = True Then cboStockFor.Focus()
            Exit Function
        End If



        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColItemCode
                mItemCode = Trim(.Text)

                If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_CLASSIFICATION", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mItemClass = MasterNo
                End If

                If mItemClass = "3" Then
                    MsgInformation("You Cann't be Issue Diesel from Issue Note.")
                    FieldsVarification = False
                End If

                If mItemClass = "2" Then
                    If VB.Left(cboStockFor.Text, 1) <> "S" Then
                        MsgInformation("Please select Sub Store for CO2.")
                        FieldsVarification = False
                        Exit Function
                    End If
                End If

                If mItemClass = "1" Then
                    If VB.Left(cboStockFor.Text, 1) <> "G" Then
                        MsgInformation("Please select General From MIG Wire.")
                        FieldsVarification = False
                        Exit Function
                    End If
                End If
            Next
        End With



        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColDemandQty
                mQty = Val(.Text)

                If mItemCode <> "" Then
                    mCheckProdType = GetProductionType(mItemCode)

                    If Trim(txtDept.Text) = "STR" Then
                        If mCheckProdType = "G" Or mCheckProdType = "T" Or mCheckProdType = "A" Then

                        Else
                            MsgInformation("Only Consumable Item Issue to Store Dept.")
                            FieldsVarification = False
                            MainClass.SetFocusToCell(SprdMain, cntRow, ColItemCode)
                            Exit Function
                        End If
                    End If


                    If mCheckProdType = "P" Or mCheckProdType = "B" Or mCheckProdType = "I" Or mCheckProdType = "3" Then ''mCheckProdType = "R" Or mCheckProdType = "D" Or

                    Else
                        If (mCheckProdType = "R" Or mCheckProdType = "D") Then
                            If mQty > 0 Then
                                MsgInformation("Raw Material Cann't be Issue. Item Code : " & mItemCode)
                                FieldsVarification = False
                                Exit Function
                            End If
                        Else
                            MsgInformation("Only Tube/BOP/Inhouse Item Issue. Item Code :" & mItemCode)
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If

                    mLotNoRequied = "N"
                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mLotNoRequied = MasterNo
                    End If
                    If mLotNoRequied = "Y" Then
                        .Col = ColBatchNo
                        If Trim(.Text) = "" Or Trim(.Text) <= "0" Then
                            MsgInformation("Lot No. Must For Such Item.")
                            FieldsVarification = False
                            MainClass.SetFocusToCell(SprdMain, cntRow, ColBatchNo)
                            Exit Function
                        End If
                    End If
                End If
            Next
        End With


NextLine1:


        If lblBookType.Text = "I" Then
            If PubSuperUser = "U" Then
                'If ValidateDeptRight(PubUserID, "STR", "STORE") = False Then
                '    MsgBox("Invalid Emp Code.", MsgBoxStyle.Information)
                '    FieldsVarification = False
                '    Exit Function
                'End If

                If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivisionCode = CDbl(Trim(MasterNo))
                End If
                If ValidateDivisionRight(PubUserID, mDivisionCode, UCase(Trim(cboDivision.Text))) = False Then
                    FieldsVarification = False
                    Exit Function
                End If
            End If
        End If

        If Trim(cboStockFor.Text) = "" Then
            MsgBox("Stock For is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            cboStockFor.Focus()
            Exit Function
        End If

        If Trim(cboDivision.Text) = "" Then
            MsgBox("Division Name is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            cboDivision.Focus()
            Exit Function
        End If

        If Trim(txtDept.Text) = "" Then
            MsgBox("Dept Code is Blank. Cann't Save", MsgBoxStyle.Information)
            FieldsVarification = False
            txtDept.Focus()
            Exit Function
        Else
            If MainClass.ValidateWithMasterTable(txtDept.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgBox("Invalid Dept Code. Cann't Save", MsgBoxStyle.Information)
                FieldsVarification = False
                txtDept.Focus()
                Exit Function
            End If
        End If

        If VB.Left(Trim(cboStockFor.Text), 1) = "S" Then
            If MainClass.ValidateWithMasterTable(txtDept.Text, "DEPT_CODE", "ISSUBSTORE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ISSUBSTORE='Y'") = False Then
                MsgBox("Sub Store not Defined for such Dept. Cann't Save", MsgBoxStyle.Information)
                FieldsVarification = False
                If cboStockFor.Enabled = True Then cboStockFor.Focus()
                Exit Function
            End If
        End If

        If Trim(txtCost.Text) = "" Then
            MsgBox("Cost Center Code is Blank. Cann't Save", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtCost.Enabled Then txtCost.Focus()
            Exit Function
        Else
            SqlStr = " SELECT IH.CC_CODE, IH.CC_DESC, ID.DEPT_CODE " & vbCrLf & " FROM FIN_CCENTER_HDR IH, FIN_CCENTER_DET ID " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=ID.COMPANY_CODE AND IH.CC_CODE=ID.CC_CODE" & vbCrLf & " AND IH.CC_CODE='" & MainClass.AllowSingleQuote(txtCost.Text) & "'" & vbCrLf & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = True Then
                MsgInformation("Invalid Cost Center Code for Department : " & Trim(txtDept.Text))
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

            'SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
            'If ADDMode = True Then
            '    SqlStr = SqlStr & vbCrLf & " AND (EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE='' OR EMP_LEAVE_DATE>=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
            'End If

            'If MainClass.ValidateWithMasterTable(txtEmp.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            '    MsgBox("Invalid Employee Code. Cann't Save", MsgBoxStyle.Information)
            '    FieldsVarification = False
            '    txtEmp.Focus()
            '    Exit Function
            'End If
        End If

        'If RsCompany.Fields("StockBalCheck").Value = "Y" Then
        mCheckLastEntryDate = GetLastEntryDate()


        If mCheckLastEntryDate <> "" Then
            mCheckLastEntryDate = DateAdd("d", -1, mCheckLastEntryDate)
            'If cboShiftcd.Text = "C" Then
            '    If CDate(DateAdd("d", 1, txtReqDate.Text)) < CDate(mCheckLastEntryDate) Then
            '        MsgBox("Cann't be Add or Modify Back Entry", MsgBoxStyle.Information)
            '        FieldsVarification = False
            '        Exit Function
            '    End If
            'Else
            If CDate(txtReqDate.Text) < CDate(mCheckLastEntryDate) Then
                MsgBox("Cann't be Add or Modify Back Entry", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
            'End If
        End If

        'If Trim(cboShiftcd.Text) = "C" Then
        '    If Trim(txtPMemoDate.Text) <> "" Then
        '        txtProdDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(VB6.Format(txtPMemoDate.Text, "DD/MM/YYYY"))))
        '        txtProdDate.Text = VB6.Format(txtProdDate.Text, "DD/MM/YYYY")
        '    End If
        'Else
        '    txtProdDate.Text = VB6.Format(txtPMemoDate.Text, "DD/MM/YYYY")
        'End If

        'End If

        If xAutoIssue = True Then
            With SprdMain
                For cntRow = 1 To .MaxRows
                    .Row = cntRow
                    .Col = ColItemCode
                    mItemCode = Trim(.Text)
                    If mItemCode <> "" Then
                        If VB.Left(cboStockFor.Text, 1) <> "N" Then
                            mProd_Type = IsProductionItem(mItemCode)
                            If mProd_Type = True Then
                                If PubSuperUser = "S" Or PubSuperUser = "A" Then
                                    If MsgQuestion("Auto Issue defined, Want to Issue BOP & Jobwork Items ?") = CStr(MsgBoxResult.No) Then
                                        FieldsVarification = False
                                        MsgInformation("Auto Issue defined, so Cann't be Issue BOP & Jobwork Items")
                                        MainClass.SetFocusToCell(SprdMain, cntRow, ColItemCode)
                                        Exit Function
                                    End If
                                Else
                                    FieldsVarification = False
                                    MsgInformation("Auto Issue defined, so Cann't be Issue BOP & Jobwork Items")
                                    MainClass.SetFocusToCell(SprdMain, cntRow, ColItemCode)
                                    Exit Function
                                End If
                            End If
                        End If
                    End If
                Next
            End With
        End If
        mDataTrue = False



        'If lblBookType.Text = "R" Then
        '    If Val(txtWIPQty.Text) + Val(txtFGQty.Text) > 0 Then
        '        mTodayPlanQty = Val(txtPlanQty.Text)

        '        mLineCapacity = Val(txtLineCapacity.Text)
        '        'If RsCompany.Fields("ISSUE_TYPE").Value = "C" Then
        '        '    mMinQty = (mLineCapacity * 2) 'Two Day Inventory
        '        'Else
        '        mMinQty = (mTodayPlanQty * 2) 'Two Day Inventory
        '        'End If

        '        mMinQty = System.Math.Round(mMinQty, 0)
        '        mWIPLockQty = Val(txtWIPLockQty.Text)
        '        mNetWIPQty = Val(txtWIPQty.Text) - mWIPLockQty

        '        If Val(CStr(mNetWIPQty)) > mMinQty Then
        '            MsgInformation("You have more than Minimum Qty WIP, Please clear it first.")
        '            FieldsVarification = False
        '            Exit Function
        '        End If

        '        mFGQty = Val(txtFGQty.Text)

        '        If Val(CStr(mFGQty)) > mMinQty Then
        '            MsgInformation("You have more than Minimum Qty FG, Please clear it first.")
        '            FieldsVarification = False
        '            Exit Function
        '        End If

        '    End If
        'End If

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColItemCode
                mItemCode = Trim(.Text)
                If Trim(.Text) <> "" Then
                    mMainItemCode = GetMainItemCode(mItemCode)

                    mProdType = GetProductionType(Trim(.Text))
                    If mProdType = "D" And mIsDevelopmentDept = "N" Then
                        FieldsVarification = False
                        MsgInformation("Please Select Development Dept for Development Item - " & Trim(.Text) & ".")
                        '                    MainClass.SetFocusToCell SprdMain, cntRow, ColStockType
                        Exit Function
                    End If
                    .Col = ColStockType
                    If Trim(.Text) = "QC" Then
                        FieldsVarification = False
                        MsgInformation("QC Stock Type Cann't be Issue. Please Change Stock Type.")
                        MainClass.SetFocusToCell(SprdMain, cntRow, ColStockType)
                        Exit Function
                    End If

                    .Row = cntRow
                    .Col = ColDemandQty
                    mDemandQty = Val(.Text)

                    .Col = ColIssueQty
                    mBalIssueQty = mDemandQty '- Val(.Text)

                    'temp lock
                    'mTodayBalanceDemand = GetTodayBalanceDemandQty(Trim(mItemCode))
                    If mDemandQty > 0 Then
                        .Col = ColDemandQty
                        If Val(.Text) > 0 Then
                            mDataTrue = True
                        End If

                        'If CheckItemBom(mMainItemCode) = False Then
                        '    'If mWIPCheck = True Then
                        '    .Col = ColDeptQty
                        '    mDeptQty = Val(.Text)

                        '    mTodayIssue = GetTodayIssueQty(Trim(mItemCode))

                        '    .Col = ColTodayPlanQty
                        '    mTodayPlanQty = Val(.Text)

                        '    .Col = ColItemCapacity
                        '    mLineCapacity = Val(.Text)

                        '    'If RsCompany.Fields("ISSUE_TYPE").Value = "C" Then
                        '    '    mMinQty = (mLineCapacity)
                        '    'Else
                        '    mMinQty = mTodayPlanQty '' (mTodayPlanQty) ''One Day Inventory
                        '    'End If

                        '    '                    mMinQty = Round(mMinQty * 0.5, 0) ''Half Day Inventory
                        '    mPackingStd = 1
                        '    If MainClass.ValidateWithMasterTable(Trim(mItemCode), "ITEM_CODE", "PACK_STD", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        '        mPackingStd = Val(Trim(MasterNo))
                        '    End If

                        '    mPackingStd = IIf(mPackingStd <= 0, 1, mPackingStd)

                        '    mMinQty = mMinQty / mPackingStd
                        '    mMinQty = IIf(Int(mMinQty) = mMinQty, mMinQty, Int(mMinQty) + 1) * mPackingStd
                        '    ''26/03/2019
                        '    '                        If RsCompany.fields("FYEAR").value >= 2019 Then
                        '    If lblBookType.Text = "R" And RsCompany.Fields("StockBalCheck").Value = "Y" Then
                        '        If mDeptQty + mBalIssueQty + mTodayBalanceDemand > mMinQty Then
                        '            FieldsVarification = False
                        '            MsgInformation("You have already " & mDeptQty & " stock in Dept of Item Code :  " & mItemCode & " and Total Balance Demanded Qty: " & mBalIssueQty + mTodayBalanceDemand & " Agt Planning Qty : " & mMinQty)
                        '            MainClass.SetFocusToCell(SprdMain, cntRow, ColDemandQty)
                        '            Exit Function
                        '        End If
                        '    End If
                        '    .Col = ColTodayIssued
                        '    mIssuedQty = Val(.Text)

                        '    'If RsCompany.Fields("ISSUE_TYPE").Value = "C" Then
                        '    '    If lblBookType.Text = "R" Then
                        '    '        If mTodayIssue + mTodayBalanceDemand + mDemandQty > mLineCapacity Then '
                        '    '            FieldsVarification = False
                        '    '            MsgInformation("Today Total Demanded Qty (" & mTodayIssue + mTodayBalanceDemand + mDemandQty & ") cann't be greater than Line Capacity (" & mLineCapacity & ") of Item Code : " & mItemCode)
                        '    '            MainClass.SetFocusToCell(SprdMain, cntRow, ColDemandQty)
                        '    '            Exit Function
                        '    '        End If
                        '    '    End If
                        '    'Else
                        '    If mDemandQty + mIssuedQty > mTodayPlanQty And RsCompany.Fields("StockBalCheck").Value = "Y" Then
                        '        FieldsVarification = False
                        '        MsgInformation("Demanded Qty cann't be greater than Planned & Issued Qty of Item Code : " & mItemCode & (mTodayPlanQty - mIssuedQty))
                        '        MainClass.SetFocusToCell(SprdMain, cntRow, ColDemandQty)
                        '        Exit Function
                        '    End If
                        '    'End If
                        '    'End If
                        'End If
                    End If
                End If
            Next
        End With

        If mDataTrue = False Then
            FieldsVarification = False
            MsgInformation("Nothing to Save.")
            MainClass.SetFocusToCell(SprdMain, cntRow, ColItemCode)
            Exit Function
        End If

        '    If BudgetValidation(Trim(txtDept.Text)) = False Then
        '        FieldsVarification = False
        '        Exit Function
        '    End If

        FieldsVarification = True
        If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "ItemCode Is Blank.") = False Then FieldsVarification = False : Exit Function
        '    If MainClass.ValidDataInGrid(SprdMain, ColDemandQty, "N", "Please Check Quantity.") = False Then FieldsVarification = False: Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColStockType, "S", "Please Check Stock Type.") = False Then FieldsVarification = False : Exit Function

        Exit Function
err_Renamed:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function


    Private Function CheckLotStockQty() As Boolean

        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim mItemCode As String
        Dim mLotNo As String
        Dim mAllStockQty As Double
        Dim mStockQty As Double
        Dim mLotQty As Double
        Dim mAutoQCIssue As String
        Dim mStockType As String = ""
        Dim mItemUOM As String = ""
        Dim mDivisionCode As Double
        Dim mCommonDivision As Double
        Dim I As Integer

        If cboDivision.SelectedIndex >= 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
        Else
            CheckLotStockQty = True
            Exit Function
        End If


        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow

                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColUom
                mItemUOM = Trim(.Text)

                .Col = ColStockType
                mStockType = Trim(.Text)


                .Col = ColBatchNo
                mLotNo = Trim(.Text)

                .Col = ColStockQty
                mStockQty = CDbl(Trim(.Text))

                '            .Col = ColIssueQty
                '            mLotQty = Trim(.Text)

                If mLotNo <> "" Then
                    mLotQty = 0
                    For I = 1 To .MaxRows - 1
                        .Row = I

                        .Col = ColItemCode
                        If mItemCode = Trim(.Text) Then
                            .Col = ColIssueQty
                            mLotQty = mLotQty + Val(.Text)
                        End If
                    Next

                    .Row = cntRow

                    If mLotQty <> 0 Then ''mStockQty > mLotQty And
                        If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "STOCKITEM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STOCKITEM='N'") = False Then

                            If MainClass.ValidateWithMasterTable(mItemCode, "AUTO_INDENT", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AUTO_INDENT='Y'") = True Then
                                mAutoQCIssue = "Y"
                            Else
                                mAutoQCIssue = "N"
                            End If

                            mCommonDivision = GetCommonDivCode()
                            mAllStockQty = GetBalanceStockQty(mItemCode, (txtReqDate.Text), mItemUOM, "STR", mStockType, "", ConWH, mDivisionCode, ConStockRefType_ISS, Val(txtReqNo.Text))

                            If RsCompany.Fields("COMPANY_CODE").Value = 1 And mAutoQCIssue = "N" Then
                                mAllStockQty = mAllStockQty + GetBalanceStockQty(mItemCode, (txtReqDate.Text), mItemUOM, "STR", "QC", "", ConWH, mDivisionCode, ConStockRefType_ISS, Val(txtReqNo.Text))
                            End If
                            If mDivisionCode <> mCommonDivision Then
                                If mCommonDivision > 0 Then
                                    mAllStockQty = mAllStockQty + GetBalanceStockQty(mItemCode, (txtReqDate.Text), mItemUOM, "STR", mStockType, "", ConWH, mCommonDivision, ConStockRefType_ISS, Val(txtReqNo.Text))
                                End If
                            End If

                            If mAllStockQty < mLotQty And mLotQty <> 0 Then
                                MsgInformation("You Have Not Enough Stock. For Item Code : " & mItemCode)
                                MainClass.SetFocusToCell(SprdMain, cntRow, ColIssueQty)
                                CheckLotStockQty = False
                                Exit Function
                            End If
                        End If
                    End If
                End If
NextRow:
            Next
        End With
        CheckLotStockQty = True
        Exit Function
ErrPart:
        CheckLotStockQty = False
    End Function
    Private Function GetLastEntryDate() As String

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        GetLastEntryDate = RsCompany.Fields("START_DATE").Value

        SqlStr = ""
        SqlStr = "SELECT Max(ISSUE_DATE) AS  ISSUE_DATE " & vbCrLf _
            & " FROM INV_ISSUE_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_ISS,LENGTH(AUTO_KEY_ISS)-5,4)=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf _
            & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "' AND ISSUE_STATUS='Y'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            GetLastEntryDate = IIf(IsDBNull(RsTemp.Fields("ISSUE_DATE").Value), "", RsTemp.Fields("ISSUE_DATE").Value)
        Else
            GetLastEntryDate = RsCompany.Fields("START_DATE").Value
        End If

        Exit Function
ErrPart:

    End Function
    Private Function CheckDieselConsumptionEntry() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        CheckDieselConsumptionEntry = False
        SqlStr = ""
        SqlStr = "SELECT * " & vbCrLf & " FROM MAN_DIESELCOSUMP_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND AUTO_KEY_ISSUE='" & Val(txtReqNo.Text) & "'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            CheckDieselConsumptionEntry = True
        End If

        Exit Function
ErrPart:
        CheckDieselConsumptionEntry = False
    End Function
    Private Function CheckMaterialIssue() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mQty As Double

        CheckMaterialIssue = False
        SqlStr = ""
        mQty = 0

        SqlStr = "SELECT SUM(ISSUE_QTY) AS ISSUE_QTY " & vbCrLf & " FROM INV_ISSUE_HDR IH, INV_ISSUE_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.AUTO_KEY_ISS=ID.AUTO_KEY_ISS " & vbCrLf & " AND IH.AUTO_KEY_ISS=" & Val(txtReqNo.Text) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


        If RsTemp.EOF = False Then
            mQty = IIf(IsDBNull(RsTemp.Fields("ISSUE_QTY").Value), 0, RsTemp.Fields("ISSUE_QTY").Value)
        End If

        If mQty > 0 Then
            CheckMaterialIssue = True
        End If

        Exit Function
ErrPart:
        CheckMaterialIssue = False
    End Function
    Private Function GetDemandedQty() As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mQty As Double
        Dim mFromDate As String
        Dim mToDate As String
        Dim mIssuTypeAppDate As String
        Dim mCheckDate As String
        Dim mSuppDate As String
        Dim mCheckWorkingDays As Integer


        GetDemandedQty = 0
        SqlStr = ""
        mQty = 0
        Exit Function

        mIssuTypeAppDate = IIf(IsDBNull(RsCompany.Fields("ISSUE_APPLICABLEDATE").Value), "", RsCompany.Fields("ISSUE_APPLICABLEDATE").Value)

        mCheckWorkingDays = GetLast7WorkingDays((txtReqDate.Text))
        mCheckDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -mCheckWorkingDays, CDate(txtReqDate.Text)))

        If CDate(mCheckDate) < CDate(mIssuTypeAppDate) Then
            If CDate(txtReqDate.Text) = CDate(mIssuTypeAppDate) Then
                mToDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 0, CDate(txtReqDate.Text)))
                mFromDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 0, CDate(mIssuTypeAppDate)))
            Else
                mToDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 0, CDate(txtReqDate.Text)))
                mFromDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mIssuTypeAppDate)))
            End If
        Else
            mToDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 0, CDate(txtReqDate.Text)))
            mFromDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -(mCheckWorkingDays - 1), CDate(txtReqDate.Text)))
        End If

        SqlStr = "SELECT SUM(PROD_PLAN_QTY) AS ISSUE_QTY " & vbCrLf _
            & " FROM INV_ISSUE_HDR IH" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND INHOUSE_CODE='" & MainClass.AllowSingleQuote((txtProductCode.Text)) & "' " & vbCrLf _
            & " AND IH.DEPT_CODE='" & MainClass.AllowSingleQuote((txtDept.Text)) & "'" & vbCrLf _
            & " AND IH.AUTO_KEY_ISS IN (" & vbCrLf _
            & " SELECT AH.AUTO_KEY_ISS " & vbCrLf _
            & " FROM INV_ISSUE_HDR AH, INV_ISSUE_DET AD " & vbCrLf _
            & " WHERE AH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND AH.AUTO_KEY_ISS=AD.AUTO_KEY_ISS"

        If Trim(lblIsSuppIssue.Text) = "N" Then
            SqlStr = SqlStr & vbCrLf & " AND AH.IS_SUPP_ISSUE='N'"
        End If

        If Val(CStr(Val(txtReqNo.Text))) <> 0 Then
            SqlStr = SqlStr & vbCrLf & " AND AH.AUTO_KEY_ISS<>" & Val(txtReqNo.Text) & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND AH.ISSUE_DATE>=TO_DATE('" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND AH.ISSUE_DATE<TO_DATE('" & VB6.Format(mToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " HAVING SUM(AD.ISSUE_QTY)<>0" & vbCrLf & " GROUP BY AH.AUTO_KEY_ISS)"

        If Trim(lblIsSuppIssue.Text) = "N" Then
            SqlStr = SqlStr & vbCrLf & " AND IS_SUPP_ISSUE='N'"
        End If

        If Val(CStr(Val(txtReqNo.Text))) <> 0 Then
            SqlStr = SqlStr & vbCrLf & " AND IH.AUTO_KEY_ISS<>" & Val(txtReqNo.Text) & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND IH.ISSUE_DATE>=TO_DATE('" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND IH.ISSUE_DATE<TO_DATE('" & VB6.Format(mToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


        If RsTemp.EOF = False Then
            GetDemandedQty = IIf(IsDBNull(RsTemp.Fields("ISSUE_QTY").Value), 0, RsTemp.Fields("ISSUE_QTY").Value)
        End If

        SqlStr = "SELECT SUM(PROD_PLAN_QTY) AS ISSUE_QTY " & vbCrLf & " FROM INV_ISSUE_HDR IH" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND INHOUSE_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "' " & vbCrLf & " AND IH.DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'"

        If Trim(lblIsSuppIssue.Text) = "N" Then
            SqlStr = SqlStr & vbCrLf & " AND IS_SUPP_ISSUE='N'"
        End If

        If Val(CStr(Val(txtReqNo.Text))) <> 0 Then
            SqlStr = SqlStr & vbCrLf & " AND IH.AUTO_KEY_ISS<>" & Val(txtReqNo.Text) & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND IH.ISSUE_DATE=TO_DATE('" & VB6.Format(mToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


        If RsTemp.EOF = False Then
            GetDemandedQty = GetDemandedQty + IIf(IsDBNull(RsTemp.Fields("ISSUE_QTY").Value), 0, RsTemp.Fields("ISSUE_QTY").Value)
        End If

        '' Check Supplementry Issue...

        mIssuTypeAppDate = IIf(IsDBNull(RsCompany.Fields("ISSUE_APPLICABLEDATE").Value), "", RsCompany.Fields("ISSUE_APPLICABLEDATE").Value)
        mCheckDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -3, CDate(txtReqDate.Text)))


        mToDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -3, CDate(txtReqDate.Text)))
        mFromDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -10, CDate(txtReqDate.Text)))


        SqlStr = "SELECT SUM(PROD_PLAN_QTY) AS ISSUE_QTY " & vbCrLf & " FROM INV_ISSUE_HDR IH" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND INHOUSE_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "' " & vbCrLf & " AND IH.DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'" & vbCrLf & " AND IH.AUTO_KEY_ISS IN (" & vbCrLf & " SELECT AH.AUTO_KEY_ISS " & vbCrLf & " FROM INV_ISSUE_HDR AH, INV_ISSUE_DET AD " & vbCrLf & " WHERE AH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND AH.AUTO_KEY_ISS=AD.AUTO_KEY_ISS"

        If Trim(lblIsSuppIssue.Text) = "N" Then
            SqlStr = SqlStr & vbCrLf & " AND AH.IS_SUPP_ISSUE='Y' AND AH.SUPP_REASON='1'"
        End If

        If Val(CStr(Val(txtReqNo.Text))) <> 0 Then
            SqlStr = SqlStr & vbCrLf & " AND AH.AUTO_KEY_ISS<>" & Val(txtReqNo.Text) & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND AH.ISSUE_DATE>=TO_DATE('" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND AH.ISSUE_DATE<TO_DATE('" & VB6.Format(mToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " HAVING SUM(AD.ISSUE_QTY)<>0" & vbCrLf & " GROUP BY AH.AUTO_KEY_ISS)"

        If Trim(lblIsSuppIssue.Text) = "N" Then
            SqlStr = SqlStr & vbCrLf & " AND IS_SUPP_ISSUE='Y' AND SUPP_REASON='1'"
        End If

        If Val(CStr(Val(txtReqNo.Text))) <> 0 Then
            SqlStr = SqlStr & vbCrLf & " AND IH.AUTO_KEY_ISS<>" & Val(txtReqNo.Text) & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND IH.ISSUE_DATE>=TO_DATE('" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND IH.ISSUE_DATE<TO_DATE('" & VB6.Format(mToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


        If RsTemp.EOF = False Then
            GetDemandedQty = GetDemandedQty + IIf(IsDBNull(RsTemp.Fields("ISSUE_QTY").Value), 0, RsTemp.Fields("ISSUE_QTY").Value)
        End If

        Exit Function
ErrPart:
        GetDemandedQty = 0
    End Function

    Private Function GetTodayIssueQtyOld(ByRef pItemCode As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing


        GetTodayIssueQtyOld = 0

        SqlStr = "SELECT SUM(ID.ISSUE_QTY) AS ISSUE_QTY " & vbCrLf & " FROM INV_ISSUE_HDR IH, INV_ISSUE_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_ISS=ID.AUTO_KEY_ISS" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf & " AND IH.DEPT_CODE='" & MainClass.AllowSingleQuote((txtDept.Text)) & "'" & vbCrLf
        '    SqlStr = SqlStr & vbCrLf & " AND IH.IS_SUPP_ISSUE='" & Trim(lblIsSuppIssue.text) & "'"

        '
        If Val(txtReqNo.Text) <> 0 Then
            SqlStr = SqlStr & vbCrLf & " AND IH.AUTO_KEY_ISS<>" & Val(txtReqNo.Text) & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND IH.ISSUE_DATE=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


        If RsTemp.EOF = False Then
            GetTodayIssueQtyOld = GetTodayIssueQtyOld + IIf(IsDBNull(RsTemp.Fields("ISSUE_QTY").Value), 0, RsTemp.Fields("ISSUE_QTY").Value)
        End If

        Exit Function
ErrPart:
        GetTodayIssueQtyOld = 0
    End Function
    Private Function GetTodayBalanceDemandQty(ByRef pItemCode As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing


        GetTodayBalanceDemandQty = 0

        SqlStr = "SELECT SUM(ID.DEMAND_QTY-ID.ISSUE_QTY) AS ISSUE_QTY " & vbCrLf _
            & " FROM INV_ISSUE_HDR IH, INV_ISSUE_DET ID" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.AUTO_KEY_ISS=ID.AUTO_KEY_ISS" & vbCrLf _
            & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
            & " AND IH.DEPT_CODE='" & MainClass.AllowSingleQuote((txtDept.Text)) & "'"

        If Val(txtReqNo.Text) <> 0 Then
            SqlStr = SqlStr & vbCrLf & " AND IH.AUTO_KEY_ISS<>" & Val(txtReqNo.Text) & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND IH.ISSUE_DATE=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


        If RsTemp.EOF = False Then
            GetTodayBalanceDemandQty = GetTodayBalanceDemandQty + IIf(IsDBNull(RsTemp.Fields("ISSUE_QTY").Value), 0, RsTemp.Fields("ISSUE_QTY").Value)
        End If

        Exit Function
ErrPart:
        GetTodayBalanceDemandQty = 0
    End Function
    Private Function GetDespatchQty() As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mQty As Double
        Dim mFromInvoiceDate As String
        Dim mToInVoiceDate As String
        Dim mIssuTypeAppDate As String
        Dim mCheckDate As String
        Dim mCheckWorkingDays As Integer

        GetDespatchQty = 0

        If RsCompany.Fields("ISSUE_TYPE").Value = "C" Then
            Exit Function
        End If
        SqlStr = ""

        mIssuTypeAppDate = IIf(IsDBNull(RsCompany.Fields("ISSUE_APPLICABLEDATE").Value), "", RsCompany.Fields("ISSUE_APPLICABLEDATE").Value)

        mCheckWorkingDays = GetLast7WorkingDays((txtReqDate.Text))

        mCheckDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -mCheckWorkingDays, CDate(txtReqDate.Text)))

        If CDate(mCheckDate) < CDate(mIssuTypeAppDate) Then
            If CDate(txtReqDate.Text) = CDate(mIssuTypeAppDate) Then
                mToInVoiceDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(txtReqDate.Text)))
                mFromInvoiceDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(txtReqDate.Text)))
            Else
                mToInVoiceDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(txtReqDate.Text)))
                mFromInvoiceDate = mIssuTypeAppDate
            End If
        Else
            mToInVoiceDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(txtReqDate.Text)))
            mFromInvoiceDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -mCheckWorkingDays, CDate(txtReqDate.Text)))
        End If

        'NextCheck:
        '    If CheckHolidays(mInvoiceDate) = True Then
        '        mInvoiceDate = DateAdd("d", -1, mInvoiceDate)
        '        GoTo NextCheck
        '    End If



        '    SELECT distinct
        ' TRN.PRODUCT_CODE,
        ' (
        ' SELECT
        ' Sum (ITEM_QTY)
        ' FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID
        ' Where IH.mKey = id.mKey
        ' AND IH.COMPANY_CODE=TRN.COMPANY_CODE
        ' AND TRIM(ID.ITEM_CODE) = TRIM(TRN.PRODUCT_CODE)
        ' AND IH.REF_DESP_TYPE IN ('P','E') AND IH.CANCELLED='N'
        ' AND IH.INVOICE_DATE>='12-Dec-2018'
        ' AND IH.INVOICE_DATE<='18-Dec-2018'
        ') AS DESP_QTY
        ' FROM VW_PRD_BOM_TRN TRN
        ' WHERE TRN.COMPANY_CODE=1 AND STATUS='O'
        ' START WITH  TRIM(RM_CODE) || '-' || TRN.COMPANY_CODE='MW0020-1' AND MAIN_ITEM='Y'
        ' CONNECT BY PRIOR (TRIM(RM_CODE) || COMPANY_CODE)=TRIM(PRODUCT_CODE) || COMPANY_CODE

        SqlStr = " SELECT DISTINCT TRN.PRODUCT_CODE, " & vbCrLf & " ("

        SqlStr = SqlStr & vbCrLf & " SELECT " & vbCrLf & " SUM(ITEM_QTY) " & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID" & vbCrLf & " WHERE IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=TRN.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE = TRN.PRODUCT_CODE" & vbCrLf & " AND IH.REF_DESP_TYPE IN ('P','E') AND IH.CANCELLED='N'" & vbCrLf & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(mFromInvoiceDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(mToInVoiceDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DPLAN_QTY, (STD_QTY+GROSS_WT_SCRAP) AS STD_QTY"

        SqlStr = SqlStr & vbCrLf & " FROM VW_PRD_BOM_TRN TRN " & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='O' " & vbCrLf & " START WITH  TRIM(PRODUCT_CODE) || '-' || TRN.COMPANY_CODE='" & MainClass.AllowSingleQuote((txtProductCode.Text)) & "-" & RsCompany.Fields("COMPANY_CODE").Value & "' AND MAIN_ITEM='Y'" & vbCrLf & " CONNECT BY PRIOR (TRIM(PRODUCT_CODE) || COMPANY_CODE)=TRIM(RM_CODE) || COMPANY_CODE "

        '    SqlStr = " SELECT " & vbCrLf _
        ''            & " ID.ITEM_CODE,SUM(ITEM_QTY) AS DPLAN_QTY " & vbCrLf _
        ''            & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID" & vbCrLf _
        ''            & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
        ''            & " AND IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & ""
        '
        '    SqlStr = SqlStr & vbCrLf & " AND IH.REF_DESP_TYPE IN ('P','E') AND IH.CANCELLED='N'"
        '
        '    SqlStr = SqlStr & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "'"
        '
        '    SqlStr = SqlStr & vbCrLf & " AND IH.INVOICE_DATE>='" & VB6.Format(mFromInvoiceDate, "DD-MMM-YYYY") & "' AND IH.INVOICE_DATE<='" & VB6.Format(mToInVoiceDate, "DD-MMM-YYYY") & "'"
        '
        '    SqlStr = SqlStr & vbCrLf & " GROUP BY ID.ITEM_CODE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                GetDespatchQty = GetDespatchQty + (IIf(IsDBNull(RsTemp.Fields("DPLAN_QTY").Value), 0, RsTemp.Fields("DPLAN_QTY").Value) * IIf(IsDBNull(RsTemp.Fields("STD_QTY").Value), 1, RsTemp.Fields("STD_QTY").Value))
                RsTemp.MoveNext()
            Loop
        End If

        SqlStr = " SELECT DISTINCT TRN.REF_ITEM_CODE AS PRODUCT_CODE, " & vbCrLf & " ("

        SqlStr = SqlStr & vbCrLf & " SELECT " & vbCrLf & " SUM(ITEM_QTY) " & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID" & vbCrLf & " WHERE IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=TRN.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE = TRN.REF_ITEM_CODE" & vbCrLf & " AND IH.REF_DESP_TYPE IN ('P','E') AND IH.CANCELLED='N'" & vbCrLf & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(mFromInvoiceDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(mToInVoiceDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DPLAN_QTY"

        SqlStr = SqlStr & vbCrLf & " FROM INV_ITEM_RELATIONSHIP_DET TRN " & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                GetDespatchQty = GetDespatchQty + IIf(IsDBNull(RsTemp.Fields("DPLAN_QTY").Value), 0, RsTemp.Fields("DPLAN_QTY").Value)
                RsTemp.MoveNext()
            Loop
        End If


        '    txtDespatchQty.Text = VB6.Format(GetDespatchQty, "0.00")

        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly
        '
        '
        '    If RsTemp.EOF = False Then
        '        GetDespatchQty = IIf(IsNull(RsTemp!DPLAN_QTY), 0, RsTemp!DPLAN_QTY)
        '    End If


        Exit Function
ErrPart:
        GetDespatchQty = 0
    End Function


    '    Private Function GetLineCapacityQty() As Double

    '        On Error GoTo ErrPart
    '        Dim SqlStr As String = ""
    '        Dim RsTemp As ADODB.Recordset = Nothing
    '        Dim mQty As Double
    '        Dim mCheckDate As String
    '        Dim mCheckWorkingDays As Integer

    '        GetLineCapacityQty = 0
    '        SqlStr = ""

    '        SqlStr = " SELECT " & vbCrLf & " SUM(CAPACITY_DAY) AS CAPACITY_DAY" & vbCrLf & " FROM INV_ITEMWISE_CAPACITY_HDR IH, INV_ITEMWISE_CAPACITY_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.PRODUCT_CODE = '" & MainClass.AllowSingleQuote((txtProductCode.Text)) & "'" & vbCrLf & " AND ID.DEPT_CODE = '" & MainClass.AllowSingleQuote((txtDept.Text)) & "'" & vbCrLf & " AND IH.WEF= (" & vbCrLf & " SELECT MAX(WEF) FROM INV_ITEMWISE_CAPACITY_HDR" & vbCrLf & " WHERE COMPANY_CODE=IH.COMPANY_CODE" & vbCrLf & " AND PRODUCT_CODE=IH.PRODUCT_CODE" & vbCrLf & " AND WEF<=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

    '        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

    '        If RsTemp.EOF = False Then
    '            GetLineCapacityQty = GetLineCapacityQty + IIf(IsDBNull(RsTemp.Fields("CAPACITY_DAY").Value), 0, RsTemp.Fields("CAPACITY_DAY").Value)
    '        End If

    '        Exit Function
    'ErrPart:
    '        GetLineCapacityQty = 0
    '    End Function

    Private Function GetLast7WorkingDays(ByRef pIssueDate As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim pCheckDate As String
        Dim mWorkingDaysCount As Integer

        Dim pCheckDays As Integer



        GetLast7WorkingDays = 0
        mWorkingDaysCount = 0
        pCheckDays = 7 ' IIf(pCheckType = "D", 7, 6)
        pCheckDate = pIssueDate

NextRec:
        SqlStr = " SELECT HOLIDAY_DATE FROM PAY_HOLIDAY_MST" & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND HOLIDAY_DATE=TO_DATE('" & VB6.Format(pCheckDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND APP_STAFF='Y' ORDER BY HOLIDAY_DATE DESC"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = True Then
            mWorkingDaysCount = mWorkingDaysCount + 1
        End If

        GetLast7WorkingDays = GetLast7WorkingDays + 1

        If mWorkingDaysCount < pCheckDays Then
            pCheckDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(pCheckDate)))
            GoTo NextRec
        End If
        Exit Function
ErrPart:
        GetLast7WorkingDays = 0
    End Function

    Private Function GetPlanedQty(ByRef pItemCode As String) As Double

        ''GetDespatchQty(pItemCode As String) As Double
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsRelTemp As ADODB.Recordset = Nothing
        Dim mParentcode As String
        Dim mChildCode As String
        Dim mStdQty As Double
        Dim mLevel As Integer
        Dim mDespQty As Double
        Dim pItemUOM As String = ""
        Dim mSqlStrRel As String
        Dim RsRel As ADODB.Recordset = Nothing
        Dim xProductRelCode As String
        Dim mCheckDate As String

        Dim mItemLevelStdQty() As Double

        ReDim mItemLevelStdQty(1000)


        'Dim mStdQty As Double
        'Dim mProductCode As String = ""

        GetPlanedQty = 0
        'If RsCompany.Fields("ISSUE_TYPE").Value = "C" Then
        '    Exit Function
        'End If

        SqlStr = ""

        mCheckDate = VB6.Format(txtReqDate.Text, "DD/MM/YYYY")


        SqlStr = " SELECT" & vbCrLf _
            & " LEVEL,TRN.PRODUCT_CODE, (TRN.STD_QTY+  GROSS_WT_SCRAP) *  DECODE(LEVEL,1,1,CONNECT_BY_ROOT STD_QTY) AS STD_QTY, DEPT_CODE," & vbCrLf _
            & " ( "
        SqlStr = SqlStr & vbCrLf _
            & " SELECT" & vbCrLf _
            & " SUM(DPLAN_QTY) " & vbCrLf _
            & " FROM PRD_PRODPLAN_HDR IH, PRD_PRODPLAN_MONTH_DET ID" & vbCrLf _
            & " WHERE IH.AUTO_KEY_PRODPLAN=ID.AUTO_KEY_PRODPLAN" & vbCrLf _
            & " AND IH.COMPANY_CODE=TRN.COMPANY_CODE" & vbCrLf _
            & " AND ID.INHOUSE_CODE = TRN.PRODUCT_CODE AND ID.DEPT_CODE='" & Trim(txtDept.Text) & "'" & vbCrLf _
            & " AND ID.SERIAL_DATE=TO_DATE('" & VB6.Format(mCheckDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & ") AS DESP_QTY"

        SqlStr = SqlStr & vbCrLf _
            & " FROM VW_PRD_BOM_TRN TRN" & vbCrLf _
            & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='O' AND TRN.DEPT_CODE='" & Trim(txtDept.Text) & "'" & vbCrLf _
            & " START WITH  TRIM(RM_CODE) || '-' || TRN.COMPANY_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "-" & RsCompany.Fields("COMPANY_CODE").Value & "' AND TRN.DEPT_CODE='" & Trim(txtDept.Text) & "'" & vbCrLf _
            & " CONNECT BY PRIOR (TRIM(PRODUCT_CODE) || COMPANY_CODE)=TRIM(RM_CODE) || COMPANY_CODE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                GetPlanedQty = GetPlanedQty + (IIf(IsDBNull(RsTemp.Fields("DESP_QTY").Value), 0, RsTemp.Fields("DESP_QTY").Value) * IIf(IsDBNull(RsTemp.Fields("STD_QTY").Value), 0, RsTemp.Fields("STD_QTY").Value))
                mParentcode = Trim(IIf(IsDBNull(RsTemp.Fields("PRODUCT_CODE").Value), "", RsTemp.Fields("PRODUCT_CODE").Value))
                mStdQty = IIf(IsDBNull(RsTemp.Fields("STD_QTY").Value), 0, RsTemp.Fields("STD_QTY").Value)

                SqlStr = " SELECT DISTINCT TRN.REF_ITEM_CODE AS PRODUCT_CODE, " & vbCrLf _
                    & " ("

                SqlStr = SqlStr & vbCrLf _
                    & " SELECT " & vbCrLf _
                    & " SUM(DPLAN_QTY) " & vbCrLf _
                    & " FROM PRD_PRODPLAN_HDR IH, PRD_PRODPLAN_MONTH_DET ID" & vbCrLf _
                    & " WHERE IH.AUTO_KEY_PRODPLAN=ID.AUTO_KEY_PRODPLAN" & vbCrLf _
                    & " AND IH.COMPANY_CODE=TRN.COMPANY_CODE" & vbCrLf _
                    & " AND ID.INHOUSE_CODE = TRN.REF_ITEM_CODE AND ID.DEPT_CODE='" & Trim(txtDept.Text) & "'" & vbCrLf _
                    & " AND ID.SERIAL_DATE=TO_DATE('" & VB6.Format(mCheckDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DPLAN_QTY"

                SqlStr = SqlStr & vbCrLf & " FROM INV_ITEM_RELATIONSHIP_DET TRN " & vbCrLf _
                    & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mParentcode) & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRelTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsRelTemp.EOF = False Then
                    Do While RsRelTemp.EOF = False
                        GetPlanedQty = GetPlanedQty + (IIf(IsDBNull(RsRelTemp.Fields("DPLAN_QTY").Value), 0, RsRelTemp.Fields("DPLAN_QTY").Value) * mStdQty)
                        RsRelTemp.MoveNext()
                    Loop
                End If

                RsTemp.MoveNext()
            Loop
        End If

        GetPlanedQty = CDbl(VB6.Format(GetPlanedQty, "0.00"))

        Exit Function
ErrPart:
        GetPlanedQty = 0
    End Function
    Private Function GetProductPlannedQty(ByRef pItemCode As String) As Double

        ''GetDespatchQty(pItemCode As String) As Double
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCheckDate As String


        'Dim mStdQty As Double
        'Dim mProductCode As String = ""

        GetProductPlannedQty = 0
        'If RsCompany.Fields("ISSUE_TYPE").Value = "C" Then
        '    Exit Function
        'End If

        SqlStr = ""

        mCheckDate = VB6.Format(txtReqDate.Text, "DD/MM/YYYY")


        SqlStr = " SELECT" & vbCrLf _
            & " SUM(DPLAN_QTY) AS DPLAN_QTY" & vbCrLf _
            & " FROM PRD_PRODPLAN_HDR IH, PRD_PRODPLAN_MONTH_DET ID" & vbCrLf _
            & " WHERE IH.AUTO_KEY_PRODPLAN=ID.AUTO_KEY_PRODPLAN" & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND ID.INHOUSE_CODE='" & pItemCode & "' AND ID.DEPT_CODE='" & Trim(txtDept.Text) & "'" & vbCrLf _
            & " AND ID.SERIAL_DATE=TO_DATE('" & VB6.Format(mCheckDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetProductPlannedQty = IIf(IsDBNull(RsTemp.Fields("DPLAN_QTY").Value), 0, RsTemp.Fields("DPLAN_QTY").Value)
        End If

        GetProductPlannedQty = CDbl(VB6.Format(GetProductPlannedQty, "0.00"))

        Exit Function
ErrPart:
        GetProductPlannedQty = 0
    End Function
    Private Function GetItemCapacity(ByRef pItemCode As String) As Double

        ''GetDespatchQty(pItemCode As String) As Double
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsRelTemp As ADODB.Recordset = Nothing
        Dim mParentcode As String
        Dim mChildCode As String
        Dim mStdQty As Double
        Dim mLevel As Integer
        Dim mDespQty As Double
        Dim pItemUOM As String = ""
        Dim mSqlStrRel As String
        Dim RsRel As ADODB.Recordset = Nothing
        Dim xProductRelCode As String


        Dim mFromInvoiceDate As String
        Dim mToInVoiceDate As String
        Dim mIssuTypeAppDate As String
        Dim mCheckDate As String


        Dim mItemLevelStdQty() As Double

        ReDim mItemLevelStdQty(1000)
        Dim mCheckWorkingDays As Integer


        GetItemCapacity = 0
        SqlStr = ""
        'txtReqDate.Text

        SqlStr = " SELECT" & vbCrLf & " LEVEL,TRN.PRODUCT_CODE, (TRN.STD_QTY+  GROSS_WT_SCRAP) *  DECODE(LEVEL,1,1,CONNECT_BY_ROOT STD_QTY) AS STD_QTY, DEPT_CODE," & vbCrLf & " ( "
        SqlStr = SqlStr & vbCrLf & " SELECT" & vbCrLf & " SUM(CAPACITY_DAY) " & vbCrLf & " FROM INV_ITEMWISE_CAPACITY_HDR IH, INV_ITEMWISE_CAPACITY_DET ID" & vbCrLf & " WHERE IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=TRN.COMPANY_CODE" & vbCrLf & " AND IH.PRODUCT_CODE = TRN.PRODUCT_CODE" & vbCrLf & " AND ID.DEPT_CODE = TRN.DEPT_CODE" & vbCrLf & " AND ID.DEPT_CODE = '" & Trim(txtDept.Text) & "'" & vbCrLf & " AND WEF = (SELECT MAX(WEF) FROM INV_ITEMWISE_CAPACITY_HDR" & vbCrLf & " WHERE COMPANY_CODE=IH.COMPANY_CODE" & vbCrLf & " AND PRODUCT_CODE=IH.PRODUCT_CODE" & vbCrLf & " AND WEF<=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))" & vbCrLf & ") AS DESP_QTY"

        SqlStr = SqlStr & vbCrLf & " FROM VW_PRD_BOM_TRN TRN" & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='O'" & vbCrLf & " START WITH  TRIM(RM_CODE) || '-' || TRN.COMPANY_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "-" & RsCompany.Fields("COMPANY_CODE").Value & "'" & vbCrLf & " CONNECT BY PRIOR (TRIM(PRODUCT_CODE) || COMPANY_CODE)=TRIM(RM_CODE) || COMPANY_CODE" & vbCrLf

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                GetItemCapacity = GetItemCapacity + (IIf(IsDBNull(RsTemp.Fields("DESP_QTY").Value), 0, RsTemp.Fields("DESP_QTY").Value) * IIf(IsDBNull(RsTemp.Fields("STD_QTY").Value), 0, RsTemp.Fields("STD_QTY").Value))

                RsTemp.MoveNext()
            Loop
        End If

        GetItemCapacity = CDbl(VB6.Format(GetItemCapacity, "0.00"))



        Exit Function
ErrPart:
        GetItemCapacity = 0
    End Function
    Private Function GetTodayIssueQty(ByRef pItemCode As String) As Double

        ''GetDespatchQty(pItemCode As String) As Double
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mParentcode As String
        Dim mChildCode As String
        Dim mStdQty As Double
        Dim mLevel As Integer
        Dim mDespQty As Double
        Dim pItemUOM As String = ""
        Dim mSqlStrRel As String
        Dim RsRel As ADODB.Recordset = Nothing
        Dim xProductRelCode As String


        Dim mFromInvoiceDate As String
        Dim mToInVoiceDate As String
        Dim mIssuTypeAppDate As String
        Dim mCheckDate As String


        Dim mItemLevelStdQty() As Double

        ReDim mItemLevelStdQty(1000)
        Dim mCheckWorkingDays As Integer


        GetTodayIssueQty = 0
        'If RsCompany.Fields("ISSUE_TYPE").Value = "C" Then
        '    Exit Function
        'End If

        SqlStr = ""

        mCheckDate = VB6.Format(txtReqDate.Text, "DD/MM/YYYY")

        SqlStr = "SELECT SUM(ID.DEMAND_QTY) AS ISSUE_QTY " & vbCrLf _
            & " FROM INV_ISSUE_HDR IH, INV_ISSUE_DET ID" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.AUTO_KEY_ISS=ID.AUTO_KEY_ISS" & vbCrLf _
            & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
            & " AND IH.DEPT_CODE='" & MainClass.AllowSingleQuote((txtDept.Text)) & "'"

        'SqlStr = SqlStr & vbCrLf & " AND IH.IS_SUPP_ISSUE='" & Trim(lblIsSuppIssue.Text) & "'"

        If Val(txtReqNo.Text) <> 0 Then
            SqlStr = SqlStr & vbCrLf & " AND IH.AUTO_KEY_ISS<>" & Val(txtReqNo.Text) & ""
        End If

        SqlStr = SqlStr & vbCrLf _
            & " AND IH.ISSUE_DATE=TO_DATE('" & VB6.Format(mCheckDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


        If RsTemp.EOF = False Then
            GetTodayIssueQty = IIf(IsDBNull(RsTemp.Fields("ISSUE_QTY").Value), 0, RsTemp.Fields("ISSUE_QTY").Value)
        End If

        'SqlStr = "SELECT SUM(ID.DEMAND_QTY) AS ISSUE_QTY " & vbCrLf _
        '    & " FROM INV_ISSUE_HDR IH, INV_ISSUE_DET ID" & vbCrLf _
        '    & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        '    & " AND IH.AUTO_KEY_ISS=ID.AUTO_KEY_ISS" & vbCrLf _
        '    & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
        '    & " AND IH.DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'"

        'SqlStr = SqlStr & vbCrLf & " AND IH.IS_SUPP_ISSUE='" & Trim(lblIsSuppIssue.Text) & "'"

        ''    If Trim(lblIsSuppIssue.text) = "N" Then
        ''        SqlStr = SqlStr & vbCrLf & " AND IH.IS_SUPP_ISSUE='N'"
        ''    End If

        'If Val(txtReqNo.Text) <> 0 Then
        '    SqlStr = SqlStr & vbCrLf & " AND IH.AUTO_KEY_ISS<>" & Val(txtReqNo.Text) & ""
        'End If

        'SqlStr = SqlStr & vbCrLf & " AND IH.ISSUE_DATE=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        'MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


        'If RsTemp.EOF = False Then
        '    GetTodayIssueQty = GetTodayIssueQty + IIf(IsDBNull(RsTemp.Fields("ISSUE_QTY").Value), 0, RsTemp.Fields("ISSUE_QTY").Value)
        'End If

        SqlStr = "SELECT SUM(ID.RTN_QTY) AS RTN_QTY " & vbCrLf _
            & " FROM INV_SRN_HDR IH, INV_SRN_DET ID" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.AUTO_KEY_SRN=ID.AUTO_KEY_SRN" & vbCrLf _
            & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
            & " AND IH.DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "' AND IH.STATUS='Y'"


        SqlStr = SqlStr & vbCrLf _
            & " AND IH.SRN_DATE=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


        If RsTemp.EOF = False Then
            GetTodayIssueQty = GetTodayIssueQty - IIf(IsDBNull(RsTemp.Fields("RTN_QTY").Value), 0, RsTemp.Fields("RTN_QTY").Value)
        End If

        Exit Function
ErrPart:
        GetTodayIssueQty = 0
    End Function

    Private Function CheckPendingReqSlip() As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        CheckPendingReqSlip = 0
        SqlStr = ""


        SqlStr = "SELECT COUNT(1) AS CNTREQ " & vbCrLf _
            & " FROM INV_ISSUE_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND DEPT_CODE='" & MainClass.AllowSingleQuote((txtDept.Text)) & "'" & vbCrLf _
            & " AND EMP_CODE='" & MainClass.AllowSingleQuote((txtEmp.Text)) & "'" & vbCrLf _
            & " AND ISSUE_STATUS='N'" & vbCrLf _
            & " AND ISSUE_DATE=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        If Val(txtReqNo.Text) > 0 Then
            SqlStr = SqlStr & vbCrLf & " AND AUTO_KEY_ISSUE<>" & Val(txtReqNo.Text) & ""
        End If


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            CheckPendingReqSlip = IIf(IsDBNull(RsTemp.Fields("CNTREQ").Value), 0, RsTemp.Fields("CNTREQ").Value)
        End If

        Exit Function
ErrPart:
        CheckPendingReqSlip = 0
    End Function
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Public Sub FrmStoreReqBOP_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'If lblBookType.Text = "I" Then
        '    Me.Text = "Material Issue Note " & IIf(lblIsSuppIssue.Text = "Y", "(Supplementary)", "(As per Despatch)")
        'Else
        '    Me.Text = "Store Requisition Note " & IIf(lblIsSuppIssue.Text = "Y", "(Supplementary)", "(As per Despatch)")
        'End If

        SqlStr = ""
        SqlStr = "Select * from INV_ISSUE_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsReqMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from INV_ISSUE_DET Where 1<>1"
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

        SqlStr = " SELECT  AUTO_KEY_ISS ISSUE_NO, ISSUE_DATE,DEPT_CODE DEPT, " & vbCrLf & " EMP_CODE EMP,DECODE(ISSUE_STATUS,'Y','COMPLETE','PENDING') AS STATUS, " & vbCrLf & " REMARKS "

        ''FROM CLAUSE...

        SqlStr = SqlStr & vbCrLf & " FROM INV_ISSUE_HDR "

        ''WHERE CLAUSE...

        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & vbCrLf & " AND SUBSTR(AUTO_KEY_ISS,LENGTH(AUTO_KEY_ISS)-5,4)=" & RsCompany.Fields("FYEAR").Value & " AND ISSUE_TYPE='N' AND IS_SUPP_ISSUE='" & lblIsSuppIssue.Text & "'"

        ''ORDER BY CLAUSE...

        SqlStr = SqlStr & vbCrLf & " Order by AUTO_KEY_ISS"

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
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim I As Integer

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
            'If lblBookType.Text = "R" Then
            .set_ColWidth(ColItemDesc, 30)
            'Else
            '    .set_ColWidth(ColItemDesc, 19)
            'End If

            .Col = ColUom
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ITEM_UOM", "INV_ISSUE_DET", PubDBCn)
            .set_ColWidth(ColUom, 4)

            .Col = ColStockType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = MainClass.SetMaxLength("FROM_STOCK_TYPE", "INV_ISSUE_DET", PubDBCn)
            .set_ColWidth(ColStockType, 4.5)

            .Col = ColHeatNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsReqDetail.Fields("HEAT_NO").DefinedSize
            .set_ColWidth(ColHeatNo, 6)
            .ColHidden = IIf(RsCompany.Fields("HEATNO_HIDE").Value = "N", False, True)

            .Col = ColBatchNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = RsReqDetail.Fields("BATCH_NO").DefinedSize
            .set_ColWidth(ColBatchNo, 6)
            .ColHidden = IIf(RsCompany.Fields("BATCHNO_HIDE").Value = "N", False, True)

            .Col = ColStockQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColStockQty, 10)

            .Col = ColDeptQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColDeptQty, 8)
            '        .ColHidden = True

            .Col = colStdQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(colStdQty, 7)
            .ColHidden = True

            .Col = ColDemandQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColDemandQty, 10)

            .Col = ColIssueQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColIssueQty, 10)
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
            .set_ColWidth(ColIssuedQty, 10)
            .ColHidden = True

            .Col = ColBalQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColBalQty, 10)
            If lblBookType.Text = "R" Then
                .ColHidden = True
            Else
                .ColHidden = False
            End If

            .Col = ColTodayPlanQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColTodayPlanQty, 10)

            .Col = ColItemCapacity
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColItemCapacity, 8)

            .Col = ColTodayIssued
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColTodayIssued, 10)

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = MainClass.SetMaxLength("REMARKS", "INV_ISSUE_DET", PubDBCn)
            'If lblBookType.Text = "R" Then
            .set_ColWidth(ColRemarks, 11)
            'Else
            '    .set_ColWidth(ColRemarks, 6)
            'End If

        End With

        If lblBookType.Text = "I" Then
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemCode, ColDemandQty)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColIssuedQty, ColBalQty)
        Else
            '        If lblIsSuppIssue.text = "N" Then
            '            MainClass.ProtectCell SprdMain, 1, SprdMain.MaxRows, ColItemCode, ColBalQty
            '        Else
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemDesc, ColUom)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColStockQty, colStdQty)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColIssueQty, ColBalQty)
            '        End If
        End If

        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColTodayPlanQty, ColTodayIssued)

        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then RsReqDetail.Requery() : Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        With RsReqMain
            txtReqDate.MaxLength = 10
            txtReqNo.MaxLength = .Fields("AUTO_KEY_ISS").Precision
            txtDept.MaxLength = .Fields("DEPT_CODE").DefinedSize
            txtEmp.MaxLength = .Fields("EMP_CODE").DefinedSize
            txtCost.MaxLength = .Fields("COST_CENTER_CODE").DefinedSize
            txtsubdept.MaxLength = .Fields("REMARKS").DefinedSize
            '        txtprod.MaxLength = .Fields("DAILY_PLAN_NO").Precision
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
        Dim mDespatchQty As Double
        Dim mLineCapacityQty As Double
        Dim pWIPLockQty As Double
        Dim pFGQty As Double
        Dim mSuppReason As String

        With RsReqMain
            If Not .EOF Then
                txtReqNo.Enabled = False
                lblMKey.Text = .Fields("AUTO_KEY_ISS").Value


                txtReqNo.Text = IIf(IsDBNull(.Fields("AUTO_KEY_ISS").Value), 0, .Fields("AUTO_KEY_ISS").Value)
                txtReqDate.Text = VB6.Format(IIf(IsDBNull(.Fields("ISSUE_DATE").Value), "", .Fields("ISSUE_DATE").Value), "DD/MM/YYYY")
                txtEntryDate.Text = VB6.Format(IIf(IsDBNull(.Fields("ADDDATE").Value), "", .Fields("ADDDATE").Value), "DD/MM/YYYY HH:MM")
                txtDept.Text = IIf(IsDBNull(.Fields("DEPT_CODE").Value), "", .Fields("DEPT_CODE").Value)
                txtEmp.Text = IIf(IsDBNull(.Fields("EMP_CODE").Value), "", .Fields("EMP_CODE").Value)
                txtCost.Text = IIf(IsDBNull(.Fields("COST_CENTER_CODE").Value), "", .Fields("COST_CENTER_CODE").Value)
                txtsubdept.Text = IIf(IsDBNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)
                '            txtprod.Text = IIf(IsNull(!DAILY_PLAN_NO), "", !DAILY_PLAN_NO)
                chkIssue.CheckState = IIf(.Fields("ISSUE_STATUS").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                chkIssue.Enabled = IIf(.Fields("ISSUE_STATUS").Value = "Y", False, True)

                cboShiftcd.Text = IIf(IsDBNull(.Fields("SHIFT_CODE").Value), "", .Fields("SHIFT_CODE").Value)

                If .Fields("ISSUE_FOR").Value = "G" Then
                    cboStockFor.SelectedIndex = 0
                ElseIf .Fields("ISSUE_FOR").Value = "P" Then
                    cboStockFor.SelectedIndex = 1
                ElseIf .Fields("ISSUE_FOR").Value = "S" Then
                    cboStockFor.SelectedIndex = 2
                ElseIf .Fields("ISSUE_FOR").Value = "N" Then
                    cboStockFor.SelectedIndex = 3
                Else
                    cboStockFor.SelectedIndex = 4
                End If

                mSuppReason = IIf(IsDBNull(.Fields("SUPP_REASON").Value), "0", .Fields("SUPP_REASON").Value)

                If mSuppReason = "1" Then
                    cboSuppReason.SelectedIndex = 0
                ElseIf mSuppReason = "2" Then
                    cboSuppReason.SelectedIndex = 1
                ElseIf mSuppReason = "3" Then
                    cboSuppReason.SelectedIndex = 2
                ElseIf mSuppReason = "4" Then
                    cboSuppReason.SelectedIndex = 3
                Else
                    cboSuppReason.SelectedIndex = -1
                End If


                If MainClass.ValidateWithMasterTable((txtDept.Text), "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    lblDeptname.Text = MasterNo
                Else
                    lblDeptname.Text = ""
                End If

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

                mDivisionCode = IIf(IsDBNull(.Fields("DIV_CODE").Value), "", .Fields("DIV_CODE").Value)
                If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivisionDesc = Trim(MasterNo)
                    cboDivision.Text = mDivisionDesc
                End If

                If lblBookType.Text = "I" Then
                    txtEmp.Enabled = False
                    txtCost.Enabled = False
                    cmdSearchEmp.Enabled = False
                    cmdSearchDept.Enabled = False
                    cmdSearchCC.Enabled = False
                End If

                txtDept.Enabled = False
                txtProductCode.Enabled = False
                cmdSearchProduct.Enabled = False
                cmdPopulate.Enabled = False
                cboStockFor.Enabled = False
                If lblIsSuppIssue.Text = "Y" Then
                    cboSuppReason.Enabled = False
                End If
                txtPlanQty.Enabled = False
                txtLineCapacity.Enabled = False

                cboDivision.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False)

                txtProductCode.Text = Trim(IIf(IsDBNull(.Fields("INHOUSE_CODE").Value), "", .Fields("INHOUSE_CODE").Value))

                If MainClass.ValidateWithMasterTable(Trim(txtProductCode.Text), "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    lblProductDesc.Text = Trim(MasterNo)
                End If

                If chkIssue.CheckState = System.Windows.Forms.CheckState.Checked Then
                    cmdUpdateIssue.Enabled = False
                End If

                mDespatchQty = Val(IIf(IsDBNull(.Fields("WEEKLY_DESP_QTY").Value), 0, .Fields("WEEKLY_DESP_QTY").Value))
                If mDespatchQty = 0 Then
                    mDespatchQty = GetProductPlannedQty(txtProductCode.Text)
                End If

                txtPlanQty.Text = VB6.Format(mDespatchQty, "0.00")

                mLineCapacityQty = GetLineCapacityQty(txtProductCode.Text, txtDept.Text, txtReqDate.Text)
                txtLineCapacity.Text = VB6.Format(mLineCapacityQty, "0.00")

                txtIssuedQty.Text = CStr(GetDemandedQty())
                txtWIPQty.Text = GetWIPQty(Trim(txtProductCode.Text), mDivisionCode, pWIPLockQty, pFGQty)
                txtWIPLockQty.Text = VB6.Format(pWIPLockQty, "0.00")

                txtFGQty.Text = VB6.Format(pFGQty, "0.00")

                lblAddUser.Text = IIf(IsDBNull(.Fields("ADDUSER").Value), "", .Fields("ADDUSER").Value)
                lblAddDate.Text = VB6.Format(IIf(IsDBNull(.Fields("ADDDATE").Value), "", .Fields("ADDDATE").Value), "dd/MM/yyyy")
                lblModUser.Text = IIf(IsDBNull(.Fields("MODUSER").Value), "", .Fields("MODUSER").Value)
                lblModDate.Text = VB6.Format(IIf(IsDBNull(.Fields("MODDATE").Value), "", .Fields("MODDATE").Value), "dd/MM/yyyy")

                txtRequestQty.Text = VB6.Format(IIf(IsDBNull(.Fields("PROD_PLAN_QTY").Value), "", .Fields("PROD_PLAN_QTY").Value), "0.00")
                '            If ShowProdPlan(-1) = False Then GoTo ERR1
                Call ShowDetail1(lblMKey.Text, mDivisionCode)
                '            txtprod.Enabled = False
            End If
        End With
        ADDMode = False
        MODIFYMode = False

        MainClass.ButtonStatus(Me, XRIGHT, RsReqMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)

        '    FormatSprdMain -1

        SprdMain.Enabled = True
        txtReqNo.Enabled = True
        cmdSearch.Enabled = True
        txtDept.Enabled = False
        cboStockFor.Enabled = False
        txtPlanQty.Enabled = False
        txtLineCapacity.Enabled = False
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub
    Private Sub ShowDetail1(ByVal pReqNum As Double, ByVal mDivisionCode As Double)

        On Error GoTo ERR1
        Dim I As Integer
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
        Dim mHeatNo As String
        Dim mBatchNo As String
        Dim mProdType As String
        Dim mCommonDivision As Double
        Dim mStockQty As Double
        Dim mAutoQCIssue As String
        Dim mTodayPlanQty As Double
        Dim mItemCapacity As Double
        Dim mTodayIssuedQty As Double
        Dim mTableName As String
        Dim mStockType As String = ""
        Dim mRefNo As String
        mTableName = ConInventoryTable

        mStockType = "('ST')"


        If Trim(txtReqNo.Text) = "" Then
            mRefNo = "ISS-1"
        Else
            mRefNo = "ISS" & Trim(txtReqNo.Text)
        End If

        SqlStr = ""
        SqlStr = " SELECT ID.*, INVMST.AUTO_INDENT, INVMST.ITEM_SHORT_DESC,  "

        SqlStr = SqlStr & vbCrLf & " (SELECT " & vbCrLf _
            & " --+ ORDERED INDEX (INV_STOCK_REC_TRN, IND_STK_REC_TRN_17) " & vbCrLf _
            & " SUM(ITEM_QTY*DECODE(ITEM_IO,'I',1,-1)) AS BALQTY" & vbCrLf _
            & " FROM " & mTableName & "" & vbCrLf _
            & " WHERE COMPANY_CODE = ID.COMPANY_CODE AND FYEAR = " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND ITEM_CODE= ID.ITEM_CODE AND STOCK_ID='WH'" & vbCrLf _
            & " AND DIV_CODE=" & mDivisionCode & " AND STATUS='O'" & vbCrLf _
            & " AND STOCK_TYPE IN " & mStockType & " AND REF_TYPE||REF_NO <> '" & mRefNo & "'" & vbCrLf _
            & " AND E_DATE<=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " ) AS STR_STOCK_QTY, "


        SqlStr = SqlStr & vbCrLf & " (SELECT " & vbCrLf _
            & " --+ ORDERED INDEX (INV_STOCK_REC_TRN, IND_STK_REC_TRN_17)" & vbCrLf _
            & " SUM(ITEM_QTY*DECODE(ITEM_IO,'I',1,-1)) AS BALQTY" & vbCrLf _
            & " FROM " & mTableName & "" & vbCrLf _
            & " WHERE COMPANY_CODE = ID.COMPANY_CODE AND FYEAR = " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND ITEM_CODE= ID.ITEM_CODE AND STOCK_ID='PH'" & vbCrLf & " AND DIV_CODE=" & mDivisionCode & " AND STATUS='O'" & vbCrLf _
            & " AND STOCK_TYPE IN 'ST' AND REF_TYPE||REF_NO <> '" & mRefNo & "'" & vbCrLf _
            & "  AND (" & vbCrLf & " DEPT_CODE_FROM='" & MainClass.AllowSingleQuote((txtDept.Text)) & "'" & vbCrLf _
            & " OR DEPT_CODE_TO='" & MainClass.AllowSingleQuote((txtDept.Text)) & "'" & vbCrLf _
            & " )" & vbCrLf _
            & " AND E_DATE<=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " ) as WIP_STOCK_QTY"

        SqlStr = SqlStr & vbCrLf & " FROM INV_ISSUE_DET ID, INV_ITEM_MST INVMST " & vbCrLf _
            & " WHERE ID.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND ID.AUTO_KEY_ISS = " & Val(CStr(pReqNum)) & "" & vbCrLf _
            & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf _
            & " ORDER BY ID.SERIAL_NO"

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
                SprdMain.Text = Trim(mItemCode)

                mAutoQCIssue = IIf(IsDBNull(.Fields("AUTO_INDENT").Value), "N", .Fields("AUTO_INDENT").Value)

                SprdMain.Col = ColItemDesc
                mItemDesc = IIf(IsDBNull(.Fields("ITEM_SHORT_DESC").Value), "", .Fields("ITEM_SHORT_DESC").Value) '' MasterNo
                SprdMain.Text = mItemDesc

                SprdMain.Col = ColUom
                mItemUOM = IIf(IsDBNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)
                SprdMain.Text = mItemUOM

                SprdMain.Col = ColStockType
                mStkType = IIf(IsDBNull(.Fields("FROM_STOCK_TYPE").Value), "", .Fields("FROM_STOCK_TYPE").Value)
                SprdMain.Text = mStkType

                mIssueQty = IIf(IsDBNull(.Fields("ISSUE_QTY").Value), 0, .Fields("ISSUE_QTY").Value)

                mDate = txtReqDate.Text

                SprdMain.Col = ColHeatNo
                SprdMain.Text = IIf(IsDBNull(.Fields("HEAT_NO").Value), "", .Fields("HEAT_NO").Value)

                SprdMain.Col = ColBatchNo
                SprdMain.Text = IIf(IsDBNull(.Fields("BATCH_NO").Value), "", .Fields("BATCH_NO").Value)

                SprdMain.Col = ColStockQty
                '            mCommonDivision = GetCommonDivCode
                mStockQty = IIf(IsDBNull(.Fields("STR_STOCK_QTY").Value), "0", .Fields("STR_STOCK_QTY").Value) ' GetBalanceStockQty(mItemCode, mDate, mItemUOM, "STR", mStkType, mBatchNo, ConWH, mDivisionCode, ConStockRefType_ISS, Val(txtReqNo.Text))
                '            If RsCompany.fields("COMPANY_CODE").value = 1 And mAutoQCIssue = "N" Then
                '                mStockQty = mStockQty + GetBalanceStockQty(mItemCode, mDate, mItemUOM, "STR", "QC", mBatchNo, ConWH, mDivisionCode, ConStockRefType_ISS, Val(txtReqNo.Text))
                '            End If
                '            If mDivisionCode <> mCommonDivision Then
                '                If mCommonDivision > 0 Then
                '                    mStockQty = mStockQty + GetBalanceStockQty(mItemCode, mDate, mItemUOM, "STR", mStkType, mBatchNo, ConWH, mCommonDivision, ConStockRefType_ISS, Val(txtReqNo.Text))
                '                End If
                '            End If
                mStockQty = mStockQty - GetUnApprovedQty(mItemCode, mDivisionCode)
                SprdMain.Text = CStr(mStockQty)

                SprdMain.Col = ColDeptQty
                mWIPStock = IIf(IsDBNull(.Fields("WIP_STOCK_QTY").Value), "0", .Fields("WIP_STOCK_QTY").Value) ' GetBalanceStockQty(mItemCode, mDate, mItemUOM, txtDept.Text, "ST", mBatchNo, ConPH, mDivisionCode, ConStockRefType_ISS, Val(txtReqNo.Text))
                '            mWIPStock = mWIPStock + GetProductionStock(mItemCode, txtDept.Text, mDivisionCode, mDate, mItemUOM) ''GetDeptStock(mItemCode, mDivisionCode)
                SprdMain.Text = VB6.Format(mWIPStock, "0.0000")

                SprdMain.Col = colStdQty
                '            mProdType = GetProductionType(mItemCode)
                '            If mProdType = "P" Or mProdType = "B" Or mProdType = "I" Then
                '                mStdQty = GetStdQty(mItemCode)
                '            Else
                mStdQty = CStr(0)
                '            End If
                SprdMain.Text = VB6.Format(mStdQty, "0.0000")

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

                SprdMain.Col = ColTodayPlanQty
                mTodayPlanQty = IIf(IsDBNull(.Fields("WEEK_BOP_DESP_QTY").Value), 0, .Fields("WEEK_BOP_DESP_QTY").Value)
                If mTodayPlanQty = 0 Then
                    mTodayPlanQty = GetTodayIssueQty(mItemCode)
                End If

                SprdMain.Text = VB6.Format(mTodayPlanQty, "0.00")

                SprdMain.Col = ColItemCapacity
                mItemCapacity = IIf(IsDBNull(.Fields("ITEM_CAPACITY").Value), 0, .Fields("ITEM_CAPACITY").Value)
                If mItemCapacity = 0 Then
                    mItemCapacity = GetItemCapacity(mItemCode)
                End If

                SprdMain.Text = VB6.Format(mItemCapacity, "0.00")

                SprdMain.Col = ColTodayIssued
                mTodayIssuedQty = GetTodayIssueQty(mItemCode)

                SprdMain.Text = VB6.Format(mTodayIssuedQty, "0.00")

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


    Private Function GetDeptStock(ByRef mItemCode As String, ByRef mDivisionCode As Double) As Double

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mProductCode As String = ""
        Dim mItemUOM As String = ""
        Dim mStdQty As String

        GetDeptStock = 0
        SqlStr = ""
        If Trim(txtProductCode.Text) = "" Then
            GetDeptStock = 0
            Exit Function
        End If
        '    SqlStr = " SELECT DISTINCT IH.PRODUCT_CODE " & vbCrLf _
        ''            & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID " & vbCrLf _
        ''            & " Where IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND IH.MKEY=ID.MKEY" & vbCrLf _
        ''            & " AND ID.RM_CODE='" & mItemCode & "' AND IH.STATUS='O'"

        SqlStr = " SELECT " & vbCrLf & " TRN.PRODUCT_CODE, TRN.RM_CODE, TRN.STD_QTY, DEPT_CODE, GROSS_WT_SCRAP" & vbCrLf & " FROM VW_PRD_BOM_TRN TRN" & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.PRODUCT_CODE='" & MainClass.AllowSingleQuote((txtProductCode.Text)) & "'" & vbCrLf & " AND TRN.DEPT_CODE='" & MainClass.AllowSingleQuote((txtDept.Text)) & "'" & vbCrLf & " AND TRN.RM_CODE='" & mItemCode & "'"

        '    SqlStr = SqlStr & vbCrLf _
        ''            & " START WITH  TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "'" & vbCrLf _
        ''            & " CONNECT BY PRIOR (TRIM(RM_CODE) || COMPANY_CODE || ' ')=TRIM(PRODUCT_CODE) || COMPANY_CODE || ' '"

        ''TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND PRIOR RM_CODE=PRODUCT_CODE
        ''" CONNECT BY PRIOR (TRIM(RM_CODE) || COMPANY_CODE || ' ')=TRIM(PRODUCT_CODE) || COMPANY_CODE || ' '"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mProductCode = Trim(IIf(IsDBNull(RsTemp.Fields("PRODUCT_CODE").Value), "", RsTemp.Fields("PRODUCT_CODE").Value))
                mStdQty = CStr(Val(IIf(IsDBNull(RsTemp.Fields("STD_QTY").Value), 0, RsTemp.Fields("STD_QTY").Value)) + Val(IIf(IsDBNull(RsTemp.Fields("GROSS_WT_SCRAP").Value), 0, RsTemp.Fields("GROSS_WT_SCRAP").Value)))

                If MainClass.ValidateWithMasterTable(mProductCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mItemUOM = MasterNo
                End If
                GetDeptStock = GetDeptStock + (GetBalanceStockQty(mProductCode, VB6.Format(txtReqDate.Text), mItemUOM, (txtDept.Text), "ST", "", ConPH, mDivisionCode) * CDbl(mStdQty))
                RsTemp.MoveNext()
            Loop
        End If

        Exit Function
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume
    End Function


    Private Function GetProductionStock(ByRef pItemCode As String, ByRef pDeptCode As String, ByRef pDivision As Double, ByRef pDate As String, ByRef pPackUnit As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsBalStock As ADODB.Recordset = Nothing
        Dim mBalQty As Double
        Dim mChildBalQty As Double

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mIssueUOM As String = ""
        Dim mPurchaseUOM As String = ""
        Dim mFactor As Double
        Dim mTableName As String
        Dim mChildItemCode As String

        pDeptCode = Trim(pDeptCode)

        SqlStr = ""

        SqlStr = "SELECT SUM(ITEM_QTY*DECODE(ITEM_IO,'I',-1,1)) AS BALQTY"

        mTableName = ConInventoryTable

        SqlStr = SqlStr & vbCrLf & " FROM " & mTableName & " "

        SqlStr = SqlStr & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        SqlStr = SqlStr & vbCrLf & "AND STOCK_ID='" & ConPH & "' AND REF_TYPE IN ('SRN','PMD','CON')"

        If pDivision <> -1 Then
            SqlStr = SqlStr & vbCrLf & "AND DIV_CODE=" & pDivision & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND STATUS='O'"

        SqlStr = SqlStr & vbCrLf & "AND (DEPT_CODE_FROM='" & pDeptCode & "' OR DEPT_CODE_TO='" & pDeptCode & "')"

        SqlStr = SqlStr & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        '    If Trim(txtProductCode.Text) <> "" Then
        '        SqlStr = SqlStr & vbCrLf & " AND REF_ITEM_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "'"
        '    End If

        SqlStr = SqlStr & vbCrLf & " AND E_DATE=TO_DATE('" & VB6.Format(pDate, "dd-mmm-yyyy") & "','DD-MON-YYYY')"

        '    SqlStr = SqlStr & vbCrLf & " AND REF_DATE=TO_DATE('" & VB6.Format((pDateTo), "DD-MMM-YYYY") & "')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBalStock, ADODB.LockTypeEnum.adLockReadOnly)

        If RsBalStock.EOF = False Then
            If IsDBNull(RsBalStock.Fields(0).Value) Then
                mBalQty = 0
            Else
                mBalQty = RsBalStock.Fields(0).Value
            End If
        Else
            mBalQty = 0
        End If

        RsBalStock = Nothing

        If mBalQty <> 0 Then
            RsTemp = Nothing

            SqlStr = " SELECT ISSUE_UOM, PURCHASE_UOM, UOM_FACTOR FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                mIssueUOM = IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)
                mPurchaseUOM = IIf(IsDBNull(RsTemp.Fields("PURCHASE_UOM").Value), "", RsTemp.Fields("PURCHASE_UOM").Value)
                mFactor = IIf(IsDBNull(RsTemp.Fields("UOM_FACTOR").Value) Or RsTemp.Fields("UOM_FACTOR").Value = 0, 1, RsTemp.Fields("UOM_FACTOR").Value)

                If pPackUnit = mPurchaseUOM Then
                    mBalQty = mBalQty / mFactor
                End If

                RsTemp = Nothing
                '            RsTemp.Close
            End If
        End If

        GetProductionStock = mBalQty

        Exit Function
ErrPart:
        GetProductionStock = 0
    End Function
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

        txtReqDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtReqNo.Text = ""
        txtDept.Text = ""
        '    lblInHouseStockQty.text = "0.00"
        lblProductDesc.Text = ""
        txtPlanQty.Text = "0.00"
        txtLineCapacity.Text = "0.00"
        txtIssuedQty.Text = "0.00"
        txtRequestQty.Text = "0.00"
        txtWIPQty.Text = "0.00"
        txtWIPLockQty.Text = "0.00"

        txtFGQty.Text = "0.00"

        If Trim(PubUserEMPCode) = "" Then
            txtEmp.Text = ""
            txtEmp.Enabled = True
            cmdSearchEmp.Enabled = True
            lblEmpname.Text = ""
        Else
            txtEmp.Text = PubUserEMPCode
            If MainClass.ValidateWithMasterTable((txtEmp.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblEmpname.Text = MasterNo
            Else
                lblEmpname.Text = ""
            End If
            cmdSearchEmp.Enabled = False
            txtEmp.Enabled = False
        End If

        cmdUpdateIssue.Enabled = IIf(lblBookType.Text = "R", False, True)

        txtCost.Text = ""
        txtsubdept.Text = ""
        cboShiftcd.SelectedIndex = 0

        cboDivision.Text = GetDefaultDivision()        'cboDivision.SelectedIndex = -1
        cboDivision.Enabled = True

        '    txtprod.Text = ""
        lblCostctr.Text = ""
        lblDeptname.Text = ""

        '    lblPlanningQty.text = ""
        txtProductCode.Text = ""
        txtEntryDate.Text = VB6.Format(PubCurrDate, "DD/MM/YYYY") & " " & GetServerTime()
        chkIssue.CheckState = System.Windows.Forms.CheckState.Unchecked

        txtReqDate.Enabled = IIf(PubUserID = "G0416", True, False) '' IIf(PubSuperUser = "S", True, False)

        txtDept.Enabled = True
        cboStockFor.Enabled = True
        txtCost.Enabled = True

        If lblIsSuppIssue.Text = "Y" Then
            cboSuppReason.Enabled = True
            cboSuppReason.Visible = True
            cboSuppReason.SelectedIndex = -1
            lblSuppReason.Visible = True
        Else
            cboSuppReason.Enabled = False
            cboSuppReason.Visible = False
            cboSuppReason.SelectedIndex = -1
            lblSuppReason.Visible = False
        End If

        lblAddUser.Text = ""
        lblAddDate.Text = ""
        lblModUser.Text = ""
        lblModDate.Text = ""


        cmdSearchDept.Enabled = True
        cmdSearchCC.Enabled = True
        cboStockFor.SelectedIndex = -1
        '    txtprod.Enabled = IIf(lblBookType.text = "R", True, False)
        chkIssue.Enabled = IIf(lblBookType.Text = "I", True, False)
        cboShiftcd.Enabled = IIf(lblBookType.Text = "R", True, False)

        txtProductCode.Enabled = True
        cmdSearchProduct.Enabled = True
        cmdPopulate.Enabled = True
        txtPlanQty.Enabled = False
        txtLineCapacity.Enabled = False
        pDataShow = False
        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsReqMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub FrmStoreReqBOP_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
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
    Public Sub FrmStoreReqBOP_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        Me.Top = 0
        Me.Left = 0

        CurrFormHeight = 7245
        CurrFormWidth = 11355

        'Me.Height = VB6.TwipsToPixelsY(7245)
        ''Me.Width = VB6.TwipsToPixelsX(11355)

        AdoDCMain.Visible = False
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

        If eventArgs.keyCode = System.Windows.Forms.Keys.Return Or eventArgs.keyCode = System.Windows.Forms.Keys.Tab Then
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
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemDesc, 0))
        SprdMain.Refresh()
    End Sub
    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        '    With SprdMain
        '        SprdMain_LeaveCell .ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False
        '    End With

    End Sub


    Private Sub txtCost_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCost.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCost_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCost.DoubleClick
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        If Trim(txtDept.Text) = "" Then
            MsgInformation("Please Select Dept. First.")
            txtDept.Focus()
            Exit Sub
        End If
        SqlStr = " SELECT IH.CC_CODE, IH.CC_DESC, ID.DEPT_CODE " & vbCrLf & " FROM FIN_CCENTER_HDR IH, FIN_CCENTER_DET ID " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=ID.COMPANY_CODE AND IH.CC_CODE=ID.CC_CODE" & vbCrLf & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote((txtDept.Text)) & "'"

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

    Private Function GetSTDQty(ByRef pItemCode As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mRMCode As String
        Dim mDept As String

        '    Call InsertTempTable(pItemCode)

        GetSTDQty = 0
        If Trim(txtProductCode.Text) = "" Then Exit Function

        '    SqlStr = " SELECT DISTINCT" & vbCrLf _
        ''            & " TRN.FG_CODE, TRN.DEPT_CODE, TRN.STD_QTY" & vbCrLf _
        ''            & " FROM TEMP_DESPVSISSUE TRN" & vbCrLf _
        ''            & " WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf _
        ''            & " AND CHILD_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "' AND "

        SqlStr = " SELECT " & vbCrLf & " TRN.PRODUCT_CODE, TRN.RM_CODE, TRN.STD_QTY, DEPT_CODE, GROSS_WT_SCRAP" & vbCrLf & " FROM VW_PRD_BOM_TRN TRN" & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND TRN.PRODUCT_CODE='" & MainClass.AllowSingleQuote((txtProductCode.Text)) & "'" & vbCrLf & " AND TRN.DEPT_CODE='" & MainClass.AllowSingleQuote((txtDept.Text)) & "'" & vbCrLf & " AND TRN.RM_CODE='" & pItemCode & "'"

        '    SqlStr = SqlStr & vbCrLf _
        ''            & " START WITH  TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "'" & vbCrLf _
        ''            & " CONNECT BY PRIOR (TRIM(RM_CODE) || COMPANY_CODE || ' ')=TRIM(PRODUCT_CODE) || COMPANY_CODE || ' '"

        ''TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND PRIOR RM_CODE=PRODUCT_CODE
        ''PRIOR (TRIM(RM_CODE) || COMPANY_CODE || ' ')=TRIM(PRODUCT_CODE) || COMPANY_CODE || ' '
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mDept = IIf(IsDBNull(RsTemp.Fields("DEPT_CODE").Value), "", RsTemp.Fields("DEPT_CODE").Value)
                If mDept = Trim(txtDept.Text) Then
                    GetSTDQty = IIf(IsDBNull(RsTemp.Fields("STD_QTY").Value), "", RsTemp.Fields("STD_QTY").Value)
                    Exit Function
                End If
                RsTemp.MoveNext()
            Loop
        End If

        Exit Function
ErrPart:
        GetSTDQty = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function InsertTempTable(ByRef mItemCode As String) As Boolean

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim RsRM As ADODB.Recordset = Nothing
        Dim xItemCode As String = ""
        Dim xSTDQty As Double
        Dim mLevel As Integer

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = " DELETE FROM TEMP_DESPVSISSUE WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = " INSERT INTO TEMP_DESPVSISSUE " & vbCrLf & " SELECT DISTINCT '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " '" & mItemCode & "', ID.RM_CODE, IH.PRODUCT_CODE, (STD_QTY + GROSS_WT_SCRAP)* DECODE(INVMST.ISSUE_UOM,'KGS',.001,DECODE(INVMST.ISSUE_UOM,'TON',.001*.001,1)) ,ID.DEPT_CODE, 1 " & vbCrLf & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY AND ID.COMPANY_CODE=INVMST.COMPANY_CODE AND ID.RM_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND ID.RM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "' AND STATUS='O'" ''& vbCrLf |            & " AND IH.WEF=("

        '    SqlStr = SqlStr & vbCrLf & " SELECT MAX(WEF) AS WEF" & vbCrLf _
        ''            & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID" & vbCrLf _
        ''            & " WHERE IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
        ''            & " AND IH.MKEY=ID.MKEY" & vbCrLf _
        ''            & " AND ID.RM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'" & vbCrLf _
        ''            & " AND WEF<='" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "')"

        PubDBCn.Execute(SqlStr)

        SqlStr = " INSERT INTO TEMP_DESPVSISSUE " & vbCrLf & " SELECT DISTINCT '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " '" & mItemCode & "',ID.ITEM_CODE, IH.PRODUCT_CODE, (ITEM_QTY + SCRAP_QTY),'J/W',  1 " & vbCrLf & " FROM PRD_OUTBOM_HDR IH, PRD_OUTBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY AND ID.COMPANY_CODE=INVMST.COMPANY_CODE AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "' AND STATUS='O'"

        PubDBCn.Execute(SqlStr)

        '    SqlStr = " INSERT INTO TEMP_DESPVSISSUE " & vbCrLf _
        ''            & " SELECT DISTINCT '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
        ''            & " '" & mItemCode & "', IA.ALTER_RM_CODE, IH.PRODUCT_CODE, (ALTER_STD_QTY + ALETRSCRAP)* DECODE(INVMST.ISSUE_UOM,'KGS',.001,DECODE(INVMST.ISSUE_UOM,'TON',.001*.001,1)) ,ID.DEPT_CODE, 1 " & vbCrLf _
        ''            & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID, PRD_BOM_ALTER_DET IA, INV_ITEM_MST INVMST" & vbCrLf _
        ''            & " WHERE IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
        ''            & " AND IH.MKEY=ID.MKEY AND IA.COMPANY_CODE=INVMST.COMPANY_CODE AND IA.ALTER_RM_CODE=INVMST.ITEM_CODE" & vbCrLf _
        ''            & " AND ID.MKEY=IA.MKEY" & vbCrLf _
        ''            & " AND ID.RM_CODE=IA.MAINITEM_CODE" & vbCrLf _
        ''            & " AND IA.ALTER_RM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "' AND STATUS='O'"
        '
        '    PubDBCn.Execute SqlStr

        mLevel = 1

        For mLevel = 1 To 5
            SqlStr = " SELECT *  FROM TEMP_DESPVSISSUE " & vbCrLf & " WHERE FG_LEVEL=" & mLevel & " AND USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRM, ADODB.LockTypeEnum.adLockReadOnly)

            If RsRM.EOF = False Then
                Do While Not RsRM.EOF
                    xItemCode = IIf(IsDBNull(RsRM.Fields("FG_CODE").Value), "", RsRM.Fields("FG_CODE").Value)
                    xSTDQty = IIf(IsDBNull(RsRM.Fields("STD_QTY").Value), 0, RsRM.Fields("STD_QTY").Value)

                    SqlStr = " INSERT INTO TEMP_DESPVSISSUE " & vbCrLf & " SELECT DISTINCT '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " '" & mItemCode & "',ID.RM_CODE, IH.PRODUCT_CODE, (STD_QTY + GROSS_WT_SCRAP)* DECODE(INVMST.ISSUE_UOM,'KGS',.001,DECODE(INVMST.ISSUE_UOM,'TON',.001*.001,1)) * " & xSTDQty & ",ID.DEPT_CODE,  " & mLevel + 1 & " " & vbCrLf & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY AND ID.COMPANY_CODE=INVMST.COMPANY_CODE AND ID.RM_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND ID.RM_CODE='" & MainClass.AllowSingleQuote(xItemCode) & "' AND STATUS='O'" ''& vbCrLf |                        & " AND IH.WEF=("

                    '                SqlStr = SqlStr & vbCrLf & " SELECT MAX(WEF) AS WEF" & vbCrLf _
                    ''                        & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID" & vbCrLf _
                    ''                        & " WHERE IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
                    ''                        & " AND IH.MKEY=ID.MKEY" & vbCrLf _
                    ''                        & " AND ID.RM_CODE='" & MainClass.AllowSingleQuote(xItemCode) & "'" & vbCrLf _
                    ''                        & " AND WEF<='" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "')"
                    '
                    PubDBCn.Execute(SqlStr)

                    RsRM.MoveNext()
                Loop
            End If
        Next

        PubDBCn.CommitTrans()
        Exit Function
LedgError:
        '    Resume
        PubDBCn.RollbackTrans()
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub ShowBOM(ByRef mProductCode As String, ByRef mProductPlanQty As Double, ByRef mDivisionCode As Double)

        On Error GoTo LedgError
        Dim RsMain As ADODB.Recordset = Nothing
        Dim RsShow As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim pWEF As String


        SqlStr = " SELECT " & vbCrLf & " PRODUCT_CODE, RM_CODE, (STD_QTY+  GROSS_WT_SCRAP)*NVL(PRIOR(STD_QTY),1) AS STD_QTY, DEPT_CODE, LEVEL" & vbCrLf & " FROM VW_PRD_BOM_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='O'"

        SqlStr = SqlStr & vbCrLf & " START WITH  TRIM(PRODUCT_CODE) || '-' || COMPANY_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "-" & RsCompany.Fields("COMPANY_CODE").Value & "'" & vbCrLf & " CONNECT BY PRIOR (TRIM(RM_CODE) || COMPANY_CODE || ' ')=TRIM(PRODUCT_CODE) || COMPANY_CODE || ' '"

        SqlStr = SqlStr & vbCrLf & " ORDER SIBLINGS BY PRODUCT_CODE"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

        I = 0
        mcntRow = 0
        If Not RsShow.EOF Then
            Do While Not RsShow.EOF
                Call FillGridCol(RsShow, mProductCode, mProductCode, mProductPlanQty, mDivisionCode)
                RsShow.MoveNext()

            Loop
        End If

        RsShow = Nothing
        Exit Sub
LedgError:
        ''    Resume
        MsgInformation(Err.Description)
    End Sub

    Private Sub ShowNewBOM(ByRef mProductCode As String, ByRef mProductPlanQty As Double, ByRef mDivisionCode As Double)

        On Error GoTo LedgError
        Dim RsMain As ADODB.Recordset = Nothing
        Dim RsShow As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim pWEF As String
        Dim mTableName As String

        Dim mRMCode As String
        Dim mItemDesc As String
        Dim mItemUOM As String = ""
        Dim mStockType As String = ""
        Dim mStockQty As Double
        Dim mDeptQty As Double
        Dim mStdQty As Double
        Dim mRefNo As String
        Dim pWIPLockQty As Double
        Dim pFGQty As Double

        mTableName = ConInventoryTable
        ''(TRN.STD_QTY+  GROSS_WT_SCRAP) *  DECODE(LEVEL,1,1,CONNECT_BY_ROOT STD_QTY)
        ''(TRN.STD_QTY+  TRN.GROSS_WT_SCRAP)*NVL(PRIOR(TRN.STD_QTY),1)

        'If RsCompany.Fields("COMPANY_CODE").Value = 1 Then
        '    mStockType = "('QC','ST')"
        'Else
        mStockType = "('ST')"
        'End If


        If Trim(txtReqNo.Text) = "" Then
            mRefNo = "ISS-1"
        Else
            mRefNo = "ISS" & Trim(txtReqNo.Text)
        End If

        SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, ID.RM_CODE, INVMST.ITEM_SHORT_DESC AS RM_NAME, INVMST.ISSUE_UOM,  " & vbCrLf & " ((ID.STD_QTY +  ID.GROSS_WT_SCRAP) * DECODE(INVMST.ISSUE_UOM,'KGS',0.001,DECODE(INVMST.ISSUE_UOM,'TON',0.001* 0.001,1))) AS STD_QTY, " & vbCrLf & " ID.DEPT_CODE, 'ST' AS STOCK_TYPE, "


        SqlStr = SqlStr & vbCrLf & " (SELECT " & vbCrLf & " --+ ORDERED INDEX (INV_STOCK_REC_TRN, IND_STK_REC_TRN_17) " & vbCrLf & " SUM(ITEM_QTY*DECODE(ITEM_IO,'I',1,-1)) AS BALQTY" & vbCrLf & " FROM " & mTableName & "" & vbCrLf & " WHERE COMPANY_CODE = IH.COMPANY_CODE AND FYEAR = " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND ITEM_CODE= ID.RM_CODE AND STOCK_ID='WH'" & vbCrLf & " AND DIV_CODE=" & mDivisionCode & " AND STATUS='O'" & vbCrLf & " AND STOCK_TYPE IN " & mStockType & " AND REF_TYPE||REF_NO <> '" & mRefNo & "'" & vbCrLf & " AND E_DATE<=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " ) AS STR_STOCK_QTY, "


        SqlStr = SqlStr & vbCrLf & " (SELECT " & vbCrLf & " --+ ORDERED INDEX (INV_STOCK_REC_TRN, IND_STK_REC_TRN_17)" & vbCrLf & " SUM(ITEM_QTY*DECODE(ITEM_IO,'I',1,-1)) AS BALQTY" & vbCrLf & " FROM " & mTableName & "" & vbCrLf & " WHERE COMPANY_CODE = IH.COMPANY_CODE AND FYEAR = " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND ITEM_CODE= ID.RM_CODE AND STOCK_ID='PH'" & vbCrLf & " AND DIV_CODE=" & mDivisionCode & " AND STATUS='O'" & vbCrLf & " AND STOCK_TYPE = 'ST' AND REF_TYPE||REF_NO <> '" & mRefNo & "'" & vbCrLf & "  AND (" & vbCrLf & " DEPT_CODE_FROM='" & MainClass.AllowSingleQuote((txtDept.Text)) & "'" & vbCrLf & " OR DEPT_CODE_TO='" & MainClass.AllowSingleQuote((txtDept.Text)) & "'" & vbCrLf & " )" & vbCrLf & " AND E_DATE<=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " ) as WIP_STOCK_QTY"

        SqlStr = SqlStr & vbCrLf & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND TRIM(ID.RM_CODE)=TRIM(INVMST.ITEM_CODE)" & vbCrLf & " AND  TRIM(IH.PRODUCT_CODE) = '" & MainClass.AllowSingleQuote(mProductCode) & "'" & vbCrLf & " AND IH.STATUS='O'" & vbCrLf & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote((txtDept.Text)) & "'"

        SqlStr = SqlStr & vbCrLf & " ORDER BY IH.PRODUCT_CODE, ID.RM_CODE"


        '        SqlStr = " SELECT " & vbCrLf _
        ''            & " TRN.PRODUCT_CODE, TRN.RM_CODE, INVMST.ITEM_SHORT_DESC AS RM_NAME, INVMST.ISSUE_UOM,  " & vbCrLf _
        ''            & " ((TRN.STD_QTY +  GROSS_WT_SCRAP) * DECODE(INVMST.ISSUE_UOM,'KGS',0.001,DECODE(INVMST.ISSUE_UOM,'TON',0.001* 0.001,1))) *  DECODE(LEVEL,1,1,CONNECT_BY_ROOT STD_QTY)  AS STD_QTY, " & vbCrLf _
        ''            & " TRN.DEPT_CODE, LEVEL, 'ST' AS STOCK_TYPE, "
        '
        '
        '        SqlStr = SqlStr & vbCrLf _
        ''            & " (SELECT " & vbCrLf _
        ''            & " --+ ORDERED INDEX (INV_STOCK_REC_TRN, IND_STK_REC_TRN_17) " & vbCrLf _
        ''            & " SUM(ITEM_QTY*DECODE(ITEM_IO,'I',1,-1)) AS BALQTY" & vbCrLf _
        ''            & " FROM " & mTableName & "" & vbCrLf _
        ''            & " WHERE COMPANY_CODE = TRN.COMPANY_CODE AND FYEAR = " & RsCompany.fields("FYEAR").value & "" & vbCrLf _
        ''            & " AND TRIM(ITEM_CODE)= TRIm(TRN.RM_CODE) AND STOCK_ID='WH'" & vbCrLf _
        ''            & " AND DIV_CODE=" & mDivisionCode & " AND STATUS='O'" & vbCrLf _
        ''            & " AND STOCK_TYPE IN " & mStockType & " AND REF_TYPE||REF_NO <> '" & mRefNo & "'" & vbCrLf _
        ''            & " AND E_DATE<=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "')" & vbCrLf _
        ''            & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "')" & vbCrLf _
        ''            & " ) AS STR_STOCK_QTY, "
        '
        '
        '        SqlStr = SqlStr & vbCrLf _
        ''            & " (SELECT " & vbCrLf _
        ''            & " --+ ORDERED INDEX (INV_STOCK_REC_TRN, IND_STK_REC_TRN_17)" & vbCrLf _
        ''            & " SUM(ITEM_QTY*DECODE(ITEM_IO,'I',1,-1)) AS BALQTY" & vbCrLf _
        ''            & " FROM " & mTableName & "" & vbCrLf _
        ''            & " WHERE COMPANY_CODE = TRN.COMPANY_CODE AND FYEAR = " & RsCompany.fields("FYEAR").value & "" & vbCrLf _
        ''            & " AND TRIM(ITEM_CODE)= TRIM(TRN.RM_CODE) AND STOCK_ID='PH'" & vbCrLf _
        ''            & " AND DIV_CODE=" & mDivisionCode & " AND STATUS='O'" & vbCrLf _
        ''            & " AND STOCK_TYPE = 'ST' AND REF_TYPE||REF_NO <> '" & mRefNo & "'" & vbCrLf _
        ''            & "  AND (" & vbCrLf _
        ''            & " DEPT_CODE_FROM='" & MainClass.AllowSingleQuote(txtDept.Text) & "'" & vbCrLf _
        ''            & " OR DEPT_CODE_TO='" & MainClass.AllowSingleQuote(txtDept.Text) & "'" & vbCrLf _
        ''            & " )" & vbCrLf _
        ''            & " AND E_DATE<=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "')" & vbCrLf _
        ''            & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "')" & vbCrLf _
        ''            & " ) as WIP_STOCK_QTY"
        '
        '        SqlStr = SqlStr & vbCrLf _
        ''            & " FROM VW_PRD_BOM_TRN TRN, INV_ITEM_MST INVMST" & vbCrLf _
        ''            & " WHERE TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
        ''            & " AND TRN.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
        ''            & " AND TRIM(TRN.RM_CODE)=TRIM(INVMST.ITEM_CODE)" & vbCrLf _
        ''            & " AND TRN.STATUS='O'" & vbCrLf _
        ''            & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'"
        '
        '        SqlStr = SqlStr & vbCrLf _
        ''            & " START WITH  TRIM(TRN.PRODUCT_CODE) || '-' || TRN.COMPANY_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "-" & RsCompany.fields("COMPANY_CODE").value & "'" & vbCrLf _
        ''            & " CONNECT BY PRIOR (TRIM(TRN.RM_CODE) || TRN.COMPANY_CODE || ' ')=TRIM(TRN.PRODUCT_CODE) || TRN.COMPANY_CODE || ' '"
        '
        '        SqlStr = SqlStr & vbCrLf _
        ''            & " ORDER SIBLINGS BY TRN.PRODUCT_CODE"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

        I = 0
        mcntRow = 0
        If Not RsShow.EOF Then
            Do While Not RsShow.EOF
                Call FillGridCol(RsShow, mProductCode, mProductCode, mProductPlanQty, mDivisionCode)
                RsShow.MoveNext()
            Loop
        End If

        txtWIPQty.Text = GetWIPQty(mProductCode, mDivisionCode, pWIPLockQty, pFGQty)
        txtWIPLockQty.Text = VB6.Format(pWIPLockQty, "0.00")

        txtFGQty.Text = VB6.Format(pFGQty, "0.00")

        RsShow = Nothing
        Exit Sub
LedgError:
        ''    Resume
        MsgInformation(Err.Description)
    End Sub

    Private Function GetWIPQty(ByRef mProductCode As String, ByRef mDivisionCode As Double, ByRef pLockQty As Double, ByRef pFGQty As Double) As Object

        On Error GoTo LedgError
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsShow As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mDeptCode As String
        Dim mDeptValidation As Boolean
        Dim mFirstDept As Boolean
        Dim mLevel As Integer
        Dim mItemUOM As String = ""
        Dim mDeptSeq As Integer
        Dim mRunningDeptSeq As Integer
        Dim mTableName As String
        Dim mStockType1 As String
        Dim mStockType2 As String
        Dim pDeptCode As String
        Dim pProdCode As String = ""
        Dim mPrevProdCode As String

        GetWIPQty = 0
        pLockQty = 0
        pFGQty = 0

        mTableName = ConInventoryTable
        mStockType1 = "'WP'"
        mStockType2 = "'-1'"

        If Trim(cboDivision.Text) = "" Then Exit Function
        If Trim(txtDept.Text) = "" Then Exit Function
        If Trim(txtProductCode.Text) = "" Then Exit Function

        mDeptSeq = GetProductSeqNo(mProductCode, Trim(txtDept.Text), (txtReqDate.Text))

        SqlStr = " SELECT DISTINCT LEVEL, TRN.PRODUCT_CODE, ID.SERIAL_NO, ID.DEPT_CODE,"


        SqlStr = SqlStr & vbCrLf & " (SELECT " & vbCrLf & " --+ ORDERED INDEX (INV_STOCK_REC_TRN, IND_STK_REC_TRN_17) " & vbCrLf & " SUM(ITEM_QTY*DECODE(ITEM_IO,'I',1,-1)) AS BALQTY" & vbCrLf & " FROM " & mTableName & "" & vbCrLf & " WHERE COMPANY_CODE = TRN.COMPANY_CODE AND FYEAR = " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND ITEM_CODE= TRN.PRODUCT_CODE AND STOCK_ID='WH'" & vbCrLf & " AND DIV_CODE=" & mDivisionCode & " AND STATUS='O'" & vbCrLf & " AND STOCK_TYPE ='ST' " & vbCrLf & " AND E_DATE<=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " ) AS STR_STOCK_QTY, "

        SqlStr = SqlStr & vbCrLf & " (SELECT " & vbCrLf & " --+ ORDERED INDEX (INV_STOCK_REC_TRN, IND_STK_REC_TRN_17) " & vbCrLf & " SUM(ITEM_QTY*DECODE(ITEM_IO,'I',1,-1)) AS BALQTY" & vbCrLf & " FROM " & mTableName & "" & vbCrLf & " WHERE COMPANY_CODE = TRN.COMPANY_CODE AND FYEAR = " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND ITEM_CODE= TRN.PRODUCT_CODE AND STOCK_ID='WH'" & vbCrLf & " AND DIV_CODE=" & mDivisionCode & " AND STATUS='O'" & vbCrLf & " AND STOCK_TYPE ='FG' " & vbCrLf & " AND E_DATE<=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " ) AS FG_STOCK_QTY, "


        SqlStr = SqlStr & vbCrLf & " (SELECT " & vbCrLf & " --+ ORDERED INDEX (INV_STOCK_REC_TRN, IND_STK_REC_TRN_17)" & vbCrLf & " SUM(ITEM_QTY*DECODE(ITEM_IO,'I',1,-1)) AS BALQTY" & vbCrLf & " FROM " & mTableName & "" & vbCrLf & " WHERE COMPANY_CODE = TRN.COMPANY_CODE AND FYEAR = " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND ITEM_CODE= TRN.PRODUCT_CODE AND STOCK_ID='PH'" & vbCrLf & " AND DIV_CODE=" & mDivisionCode & " AND STATUS='O'" & vbCrLf & " AND STOCK_TYPE <> CASE WHEN TRIM(ID.DEPT_CODE)='" & MainClass.AllowSingleQuote(Trim(txtDept.Text)) & "' THEN " & mStockType1 & " ELSE " & mStockType2 & " END" & vbCrLf & " AND STOCK_TYPE NOT IN ('WC','SC')" & vbCrLf & " AND (" & vbCrLf & " DEPT_CODE_FROM=ID.DEPT_CODE" & vbCrLf & " OR DEPT_CODE_TO=ID.DEPT_CODE" & vbCrLf & " )" & vbCrLf & " AND E_DATE<=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " ) as WIP_STOCK_QTY"

        SqlStr = SqlStr & vbCrLf & " FROM VW_PRD_BOM_TRN TRN, PRD_PRODSEQUENCE_DET ID" & vbCrLf & " WHERE ID.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.COMPANY_CODE=ID.COMPANY_CODE" & vbCrLf & " AND TRIM(TRN.PRODUCT_CODE)=TRIM(ID.PRODUCT_CODE)"

        SqlStr = SqlStr & vbCrLf & " AND ID.SERIAL_NO>= DECODE(LEVEL,1," & mDeptSeq & ",1)"

        SqlStr = SqlStr & vbCrLf & " AND ID.WEF = (" & vbCrLf & " SELECT MAX(WEF) AS WEF FROM PRD_PRODSEQUENCE_DET" & vbCrLf & " WHERE COMPANY_CODE = TRN.COMPANY_CODE" & vbCrLf & " AND PRODUCT_CODE=ID.PRODUCT_CODE" & vbCrLf & " AND WEF <=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        SqlStr = SqlStr & vbCrLf & " START WITH  TRIM(TRN.PRODUCT_CODE) || '-' || TRN.COMPANY_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "-" & RsCompany.Fields("COMPANY_CODE").Value & "'" & vbCrLf & " CONNECT BY  (TRIM(TRN.RM_CODE) || TRN.COMPANY_CODE || ' ')=PRIOR(TRIM(TRN.PRODUCT_CODE) || TRN.COMPANY_CODE || ' ')"

        SqlStr = SqlStr & vbCrLf & " ORDER SIBLINGS by PRODUCT_CODE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

        mPrevProdCode = "-1"

        If Not RsShow.EOF Then
            Do While Not RsShow.EOF
                pProdCode = Trim(IIf(IsDBNull(RsShow.Fields("PRODUCT_CODE").Value), "", RsShow.Fields("PRODUCT_CODE").Value))
                If pProdCode <> mPrevProdCode Then
                    GetWIPQty = GetWIPQty + IIf(IsDBNull(RsShow.Fields("STR_STOCK_QTY").Value), 0, RsShow.Fields("STR_STOCK_QTY").Value) + IIf(IsDBNull(RsShow.Fields("WIP_STOCK_QTY").Value), 0, RsShow.Fields("WIP_STOCK_QTY").Value)
                    pFGQty = pFGQty + IIf(IsDBNull(RsShow.Fields("FG_STOCK_QTY").Value), 0, RsShow.Fields("FG_STOCK_QTY").Value)
                Else
                    GetWIPQty = GetWIPQty + IIf(IsDBNull(RsShow.Fields("WIP_STOCK_QTY").Value), 0, RsShow.Fields("WIP_STOCK_QTY").Value)
                End If

                '                GetWIPQty = GetWIPQty + IIf(IsNull(RsShow!STR_STOCK_QTY), 0, RsShow!STR_STOCK_QTY) + IIf(IsNull(RsShow!WIP_STOCK_QTY), 0, RsShow!WIP_STOCK_QTY)

                pDeptCode = Trim(IIf(IsDBNull(RsShow.Fields("DEPT_CODE").Value), 0, RsShow.Fields("DEPT_CODE").Value))
                If pProdCode <> mPrevProdCode Then
                    pLockQty = pLockQty + GetWIPLockQty(pProdCode, pDeptCode, (txtReqDate.Text))
                End If
                mPrevProdCode = Trim(IIf(IsDBNull(RsShow.Fields("PRODUCT_CODE").Value), "", RsShow.Fields("PRODUCT_CODE").Value))
                RsShow.MoveNext()
            Loop
        End If

        pLockQty = pLockQty + GetWIPLockQty(pProdCode, "STR", (txtReqDate.Text))

        SqlStr = " SELECT DISTINCT TRN.REF_ITEM_CODE AS PRODUCT_CODE, "


        SqlStr = SqlStr & vbCrLf & " (SELECT " & vbCrLf & " --+ ORDERED INDEX (INV_STOCK_REC_TRN, IND_STK_REC_TRN_17) " & vbCrLf & " SUM(ITEM_QTY*DECODE(ITEM_IO,'I',1,-1)) AS BALQTY" & vbCrLf & " FROM " & mTableName & "" & vbCrLf & " WHERE COMPANY_CODE = TRN.COMPANY_CODE AND FYEAR = " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND ITEM_CODE= TRN.REF_ITEM_CODE AND STOCK_ID='WH'" & vbCrLf & " AND DIV_CODE=" & mDivisionCode & " AND STATUS='O'" & vbCrLf & " AND STOCK_TYPE ='ST' " & vbCrLf & " AND E_DATE<=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " ) AS STR_STOCK_QTY, "

        SqlStr = SqlStr & vbCrLf & " (SELECT " & vbCrLf & " --+ ORDERED INDEX (INV_STOCK_REC_TRN, IND_STK_REC_TRN_17) " & vbCrLf & " SUM(ITEM_QTY*DECODE(ITEM_IO,'I',1,-1)) AS BALQTY" & vbCrLf & " FROM " & mTableName & "" & vbCrLf & " WHERE COMPANY_CODE = TRN.COMPANY_CODE AND FYEAR = " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND ITEM_CODE= TRN.REF_ITEM_CODE AND STOCK_ID='WH'" & vbCrLf & " AND DIV_CODE=" & mDivisionCode & " AND STATUS='O'" & vbCrLf & " AND STOCK_TYPE ='FG' " & vbCrLf & " AND E_DATE<=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " ) AS FG_STOCK_QTY, "


        SqlStr = SqlStr & vbCrLf & " (SELECT " & vbCrLf & " --+ ORDERED INDEX (INV_STOCK_REC_TRN, IND_STK_REC_TRN_17)" & vbCrLf & " SUM(ITEM_QTY*DECODE(ITEM_IO,'I',1,-1)) AS BALQTY" & vbCrLf & " FROM " & mTableName & "" & vbCrLf & " WHERE COMPANY_CODE = TRN.COMPANY_CODE AND FYEAR = " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND ITEM_CODE= TRN.REF_ITEM_CODE AND STOCK_ID='PH'" & vbCrLf & " AND DIV_CODE=" & mDivisionCode & " AND STATUS='O'" & vbCrLf & " -- AND STOCK_TYPE <> CASE WHEN TRIM(ID.DEPT_CODE)='" & MainClass.AllowSingleQuote(Trim(txtDept.Text)) & "' THEN " & mStockType1 & " ELSE " & mStockType2 & " END" & vbCrLf & " AND STOCK_TYPE NOT IN ('WC','SC')" & vbCrLf & " -- AND (" & vbCrLf & " -- DEPT_CODE_FROM=ID.DEPT_CODE" & vbCrLf & " -- OR DEPT_CODE_TO=ID.DEPT_CODE" & vbCrLf & " -- )" & vbCrLf & " AND E_DATE<=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " ) as WIP_STOCK_QTY"

        SqlStr = SqlStr & vbCrLf & " FROM INV_ITEM_RELATIONSHIP_DET TRN" & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(Trim(mProductCode)) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)


        If Not RsShow.EOF Then
            Do While Not RsShow.EOF
                pProdCode = Trim(IIf(IsDBNull(RsShow.Fields("PRODUCT_CODE").Value), "", RsShow.Fields("PRODUCT_CODE").Value))
                GetWIPQty = GetWIPQty + IIf(IsDBNull(RsShow.Fields("STR_STOCK_QTY").Value), 0, RsShow.Fields("STR_STOCK_QTY").Value) + IIf(IsDBNull(RsShow.Fields("WIP_STOCK_QTY").Value), 0, RsShow.Fields("WIP_STOCK_QTY").Value)
                pFGQty = pFGQty + IIf(IsDBNull(RsShow.Fields("FG_STOCK_QTY").Value), 0, RsShow.Fields("FG_STOCK_QTY").Value)
                pDeptCode = "STR"
                pLockQty = pLockQty + GetWIPLockQty(pProdCode, pDeptCode, (txtReqDate.Text))

                RsShow.MoveNext()
            Loop
        End If


        ''---------------- Old query
        '        SqlStr = " SELECT DISTINCT LEVEL, TRN.PRODUCT_CODE FROM VW_PRD_BOM_TRN TRN"
        '
        '        SqlStr = SqlStr & vbCrLf _
        ''            & " START WITH  TRIM(TRN.PRODUCT_CODE) || '-' || TRN.COMPANY_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "-" & RsCompany.fields("COMPANY_CODE").value & "' AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'" & vbCrLf _
        ''            & " CONNECT BY  (TRIM(TRN.RM_CODE) || TRN.COMPANY_CODE || ' ')=PRIOR(TRIM(TRN.PRODUCT_CODE) || TRN.COMPANY_CODE || ' ')"
        '
        '        MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsShow, adLockReadOnly
        '
        '
        '        If Not RsShow.EOF Then
        '            Do While Not RsShow.EOF
        '                mProductCode = IIf(IsNull(RsShow!PRODUCT_CODE), "", RsShow!PRODUCT_CODE)
        '
        '                If MainClass.ValidateWithMasterTable(mProductCode, "ITEM_CODE", "ISSUE_UOM", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
        '                    mItemUOM = MasterNo
        '                End If
        '
        '                mDeptValidation = False
        '                mLevel = IIf(IsNull(RsShow!Level), 1, RsShow!Level)
        '                mFirstDept = IIf(IIf(IsNull(RsShow!Level), 1, RsShow!Level) = 1, True, False)
        '
        '                If mLevel = 1 Then
        '                    mDeptSeq = GetProductSeqNo(mProductCode, Trim(txtDept.Text), txtReqDate.Text)
        '                Else
        '                    mDeptSeq = 1
        '                End If
        '
        '                SqlStr = " SELECT SERIAL_NO, DEPT_CODE " & vbCrLf _
        ''                        & " FROM PRD_PRODSEQUENCE_DET " & vbCrLf _
        ''                        & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
        ''                        & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "' AND SERIAL_NO>=" & mDeptSeq & "" & vbCrLf _
        ''                        & " AND WEF = (" & vbCrLf _
        ''                        & " SELECT MAX(WEF) AS WEF FROM PRD_PRODSEQUENCE_DET" & vbCrLf _
        ''                        & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
        ''                        & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'" & vbCrLf _
        ''                        & " AND WEF <='" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "') ORDER BY SERIAL_NO"
        '
        '
        '                MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly
        '
        '
        '                If Not RsTemp.EOF Then
        '                    Do While Not RsTemp.EOF
        '                        mDeptCode = IIf(IsNull(RsTemp!DEPT_CODE), "", RsTemp!DEPT_CODE)
        '                        mRunningDeptSeq = IIf(IsNull(RsTemp!SERIAL_NO), 1, RsTemp!SERIAL_NO)
        '
        '                        GetWIPQty = GetWIPQty + GetBalanceStockQty(mProductCode, txtReqDate.Text, mItemUOM, mDeptCode, "", "", ConPH, mDivisionCode)
        '
        '
        '                        If mRunningDeptSeq = mDeptSeq And mLevel = 1 Then
        '                            GetWIPQty = GetWIPQty - GetBalanceStockQty(mProductCode, txtReqDate.Text, mItemUOM, txtReqDate.Text, "WP", "", ConPH, mDivisionCode)
        '                        End If
        '
        '
        '                        GetWIPQty = GetWIPQty - GetBalanceStockQty(mProductCode, txtReqDate.Text, mItemUOM, txtReqDate.Text, "WC", "", ConPH, mDivisionCode)
        '
        '                        RsTemp.MoveNext
        '                    Loop
        '                End If
        '
        '                GetWIPQty = GetWIPQty + GetBalanceStockQty(mProductCode, txtReqDate.Text, mItemUOM, txtReqDate.Text, "", "", ConWH, mDivisionCode)
        '                GetWIPQty = GetWIPQty - GetBalanceStockQty(mProductCode, txtReqDate.Text, mItemUOM, txtReqDate.Text, "WC", "", ConWH, mDivisionCode)
        '                RsShow.MoveNext
        '            Loop
        '        End If

        '-----------------------------------


        '        SqlStr = SqlStr & vbCrLf _
        ''                & " AND ID.SERIAL_NO>=CASE WHEN LEVEL=1 THEN" & vbCrLf _
        ''                & " ( SELECT SERIAL_NO " & vbCrLf _
        ''                & " FROM PRD_PRODSEQUENCE_DET SD" & vbCrLf _
        ''                & " WHERE SD.COMPANY_CODE = TRN.COMPANY_CODE" & vbCrLf _
        ''                & " AND TRIM(SD.PRODUCT_CODE)=TRIM(TRN.PRODUCT_CODE)" & vbCrLf _
        ''                & " AND SD.DEPT_CODE='" & Trim(txtDept.Text) & "'" & vbCrLf _
        ''                & " AND SD.WEF = (" & vbCrLf _
        ''                & " SELECT MAX(WEF) AS WEF FROM PRD_PRODSEQUENCE_DET" & vbCrLf _
        ''                & " WHERE COMPANY_CODE = SD.COMPANY_CODE" & vbCrLf _
        ''                & " AND TRIM(PRODUCT_CODE)=TRIM(SD.PRODUCT_CODE)" & vbCrLf _
        ''                & " AND WEF <='" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "'" & vbCrLf _
        ''                & " )" & vbCrLf _
        ''                & " ) ELSE (" & vbCrLf _
        ''                & " SELECT SERIAL_NO" & vbCrLf _
        ''                & " FROM PRD_PRODSEQUENCE_DET SD" & vbCrLf _
        ''                & " WHERE SD.COMPANY_CODE = TRN.COMPANY_CODE" & vbCrLf _
        ''                & " AND TRIM(SD.PRODUCT_CODE)=TRIM(TRN.PRODUCT_CODE)" & vbCrLf _
        ''                & " AND SD.DEPT_CODE=ID.DEPT_CODE" & vbCrLf _
        ''                & " AND SD.WEF = (" & vbCrLf _
        ''                & " SELECT MAX(WEF) AS WEF FROM PRD_PRODSEQUENCE_DET" & vbCrLf _
        ''                & " WHERE COMPANY_CODE = SD.COMPANY_CODE" & vbCrLf _
        ''                & " AND TRIM(PRODUCT_CODE)=TRIM(SD.PRODUCT_CODE)" & vbCrLf _
        ''                & " AND WEF <='" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "')) END"

        RsShow = Nothing
        Exit Function
LedgError:
        ''    Resume
        MsgInformation(Err.Description)
    End Function
    Private Sub FillGridCol(ByRef pRs As ADODB.Recordset, ByRef pProductCode As String, ByRef pParentCode As String, ByRef mProductPlanQty As Double, ByRef mDivisionCode As Double)
        On Error GoTo FillGERR
        Dim mRMCode As String
        Dim mItemUOM As String = ""
        Dim mStockQty As Double
        Dim mDeptCode As String
        Dim mWIPStock As Double
        Dim mProd_Type As Boolean
        Dim xAutoIssue As Boolean
        Dim pRow As Integer
        Dim mCommonDivision As Double
        Dim mAutoQCIssue As String
        Dim mItemDesc As String
        Dim mIsNewLine As Boolean
        Dim mShowProduct As Boolean
        Dim mCheckProdType As String
        Dim mTodayPlanQty As Double
        Dim mTodayIssuedQty As Double
        Dim mMaxIssueQty As Double
        Dim mItemCapacity As Double
        mIsNewLine = False
        With SprdMain

            mDeptCode = Trim(IIf(IsDBNull(pRs.Fields("DEPT_CODE").Value), "", pRs.Fields("DEPT_CODE").Value))
            mRMCode = Trim(IIf(IsDBNull(pRs.Fields("RM_CODE").Value), "", pRs.Fields("RM_CODE").Value))
            '        mItemUOM = IIf(IsNull(pRs!ISSUE_UOM), "", pRs!ISSUE_UOM)
            xAutoIssue = CheckAutoIssue((txtReqDate.Text), mRMCode)
            mCheckProdType = GetProductionType(mRMCode)

            If mCheckProdType = "P" Or mCheckProdType = "B" Or mCheckProdType = "I" Or mCheckProdType = "3" Then
                mShowProduct = True
            Else
                mShowProduct = False
            End If
            If mDeptCode = Trim(txtDept.Text) And mShowProduct = True Then

                mProd_Type = IsProductionItem(mRMCode)
                If xAutoIssue = True Then
                    If mProd_Type = True Then
                        GoTo NextRecd
                    End If
                End If
                pRow = 0
                If GetItemCodeAlreadyExists(IIf(IsDBNull(pRs.Fields("RM_CODE").Value), "", pRs.Fields("RM_CODE").Value), pRow) = True Then
                    .Row = pRow
                    GoTo NextRec
                Else
                    mIsNewLine = True
                    mcntRow = mcntRow + 1
                    .Row = mcntRow
                End If

                If MainClass.ValidateWithMasterTable(mRMCode, "ITEM_CODE", "AUTO_INDENT", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AUTO_INDENT='Y'") = True Then
                    mAutoQCIssue = "Y"
                Else
                    mAutoQCIssue = "N"
                End If

                .Col = ColItemCode
                .Text = IIf(IsDBNull(pRs.Fields("RM_CODE").Value), "", pRs.Fields("RM_CODE").Value)


                .Col = ColItemDesc
                .Text = IIf(IsDBNull(pRs.Fields("RM_NAME").Value), "", pRs.Fields("RM_NAME").Value) 'mItemDesc

                .Col = ColUom
                .Text = IIf(IsDBNull(pRs.Fields("ISSUE_UOM").Value), "", pRs.Fields("ISSUE_UOM").Value) 'mItemUOM 'IIf(IsNull(pRs!ISSUE_UOM), "", pRs!ISSUE_UOM)


                .Col = ColStockType
                .Text = "ST"

                .Col = ColStockQty
                mStockQty = IIf(IsDBNull(pRs.Fields("STR_STOCK_QTY").Value), 0, pRs.Fields("STR_STOCK_QTY").Value) ''19/12/2018
                mStockQty = mStockQty - GetUnApprovedQty(mRMCode, mDivisionCode)
                .Text = CStr(mStockQty)

                '            mCommonDivision = GetCommonDivCode
                '            mStockQty = GetBalanceStockQty(mRMCode, txtReqDate.Text, mItemUOM, "STR", "ST", "", ConWH, mDivisionCode, ConStockRefType_ISS, Val(txtReqNo.Text))
                '            If RsCompany.fields("COMPANY_CODE").value = 1 Then   'And mAutoQCIssue = "N"
                '                mStockQty = mStockQty + GetBalanceStockQty(mRMCode, txtReqDate.Text, mItemUOM, "STR", "QC", "", ConWH, mDivisionCode, ConStockRefType_ISS, Val(txtReqNo.Text))
                '            End If
                '            If mDivisionCode <> mCommonDivision Then
                '                If mCommonDivision > 0 Then
                '                    mStockQty = mStockQty + GetBalanceStockQty(mRMCode, txtReqDate.Text, mItemUOM, "STR", "ST", "", ConWH, mCommonDivision, ConStockRefType_ISS, Val(txtReqNo.Text))
                '                End If
                '            End If


                .Col = ColDeptQty
                mWIPStock = IIf(IsDBNull(pRs.Fields("WIP_STOCK_QTY").Value), 0, pRs.Fields("WIP_STOCK_QTY").Value) ''19/12/2018
                .Text = VB6.Format(mWIPStock, "0.0000")

                '            mWIPStock = GetBalanceStockQty(mRMCode, txtReqDate.Text, mItemUOM, txtDept.Text, "ST", "", ConPH, mDivisionCode, ConStockRefType_ISS, Val(txtReqNo.Text))
                '            mWIPStock = mWIPStock + GetProductionStock(mRMCode, txtDept.Text, mDivisionCode, txtReqDate.Text, mItemUOM)     ''GetDeptStock(mRMCode, mDivisionCode)


NextRec:
                SprdMain.Col = colStdQty
                .Text = CStr(Val(.Text) + CDbl(VB6.Format(Val(IIf(IsDBNull(pRs.Fields("STD_QTY").Value), "", pRs.Fields("STD_QTY").Value)), "0.0000")))

                SprdMain.Col = ColTodayPlanQty
                mTodayPlanQty = GetPlanedQty(IIf(IsDBNull(pRs.Fields("RM_CODE").Value), "", pRs.Fields("RM_CODE").Value))

                SprdMain.Text = VB6.Format(mTodayPlanQty, "0.00")

                SprdMain.Col = ColItemCapacity
                mItemCapacity = GetItemCapacity(IIf(IsDBNull(pRs.Fields("RM_CODE").Value), "", pRs.Fields("RM_CODE").Value))

                SprdMain.Text = VB6.Format(mItemCapacity, "0.00")

                SprdMain.Col = ColTodayIssued
                mTodayIssuedQty = GetTodayIssueQty(IIf(IsDBNull(pRs.Fields("RM_CODE").Value), "", pRs.Fields("RM_CODE").Value))

                SprdMain.Text = VB6.Format(mTodayIssuedQty, "0.00")

                'If RsCompany.Fields("ISSUE_TYPE").Value = "C" Then
                '    mMaxIssueQty = CDbl(VB6.Format(mItemCapacity, "0.00"))
                'Else
                mMaxIssueQty = CDbl(VB6.Format(mTodayPlanQty - mTodayIssuedQty, "0.00"))
                'End If

                If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 110 Then
                    mMaxIssueQty = CDbl(VB6.Format(mProductPlanQty * Val(IIf(IsDBNull(pRs.Fields("STD_QTY").Value), 0, pRs.Fields("STD_QTY").Value)), "0.00"))
                Else
                    mMaxIssueQty = IIf(mMaxIssueQty <= CDbl(VB6.Format(mProductPlanQty * Val(IIf(IsDBNull(pRs.Fields("STD_QTY").Value), "", pRs.Fields("STD_QTY").Value)), "0.00")), mMaxIssueQty, VB6.Format(mProductPlanQty * Val(IIf(IsDBNull(pRs.Fields("STD_QTY").Value), "", pRs.Fields("STD_QTY").Value)), "0.00"))
                    mMaxIssueQty = IIf(mMaxIssueQty < 0, 0, mMaxIssueQty)
                End If


                .Col = ColDemandQty
                .Text = VB6.Format(mMaxIssueQty, "0.00")

                .Col = ColIssueQty
                .Text = "0.00"

                .Col = ColIssuedQty
                .Text = "0.00"

                .Col = ColBalQty
                .Text = "0.00"

                .Col = ColRemarks
                .Text = ""



                '
                If mIsNewLine = True Then
                    .MaxRows = .MaxRows + 1
                End If
            End If
        End With
NextRecd:
        '    Call FillSubAlterRecord(mRMCode, "", pProductCode, mDeptCode, pParentCode, mProductPlanQty, mDivisionCode)
        '    Call FillSubRecord(mRMCode, "", pProductCode, mProductPlanQty, mDivisionCode)

        Exit Sub
FillGERR:
        '    Resume
        MsgBox(Err.Description)
    End Sub
    Private Function GetItemCodeAlreadyExists(ByRef pItemCode As Object, ByRef pRow As Object) As Boolean
        On Error GoTo FillGERR
        Dim xRow As Integer

        pRow = 0
        GetItemCodeAlreadyExists = False
        With SprdMain
            For xRow = 1 To .MaxRows
                .Row = xRow
                .Col = ColItemCode
                If Trim(.Text) <> "" Then
                    If Trim(.Text) = Trim(pItemCode) Then
                        pRow = xRow
                        GetItemCodeAlreadyExists = True
                    End If
                End If
            Next
        End With
        Exit Function
FillGERR:
        GetItemCodeAlreadyExists = False
    End Function
    Private Sub FillSubAlterRecord(ByRef pRMMainCode As String, ByRef pWEF As String, ByRef pMainProductCode As String, ByRef pDeptCode As String, ByRef pParentCode As String, ByRef mProductPlanQty As Double, ByRef mDivisionCode As Double)

        On Error GoTo FillERR
        Dim SqlStr As String = ""
        Dim RsShow As ADODB.Recordset = Nothing
        Dim mRMCode As String


        If pDeptCode <> "J/W" Then
            SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, " & vbCrLf & " ID.ALTER_RM_CODE AS RM_CODE, '(*) - ' || INVMST.ITEM_SHORT_DESC AS ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " ID.DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf & " ID.ALTER_STD_QTY AS STD_QTY, ID.ALETRSCRAP AS GROSS_WT_SCRAP," & vbCrLf & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM, MAXIMUM_QTY, MINIMUM_QTY "

            SqlStr = SqlStr & vbCrLf & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET IDET, PRD_BOM_ALTER_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY=IDET.MKEY " & vbCrLf & " AND IDET.MKEY=ID.MKEY " & vbCrLf & " AND IDET.RM_CODE=ID.MAINITEM_CODE " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.ALTER_RM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ID.MAINITEM_CODE='" & MainClass.AllowSingleQuote(pRMMainCode) & "'" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pMainProductCode) & "'" & vbCrLf & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_NEWBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pMainProductCode) & "') " '& vbCrLf |                & " AND WEF<= '" & VB6.Format(pWEF, "DD-MMM-YYYY") & "')" & vbCrLf |
            SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SUBROWNO"
        Else

            SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, " & vbCrLf & " ID.ALTER_ITEM_CODE AS RM_CODE, '(**) - ' || INVMST.ITEM_SHORT_DESC AS ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " 'J/W' AS DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf & " ID.ALTER_ITEM_QTY AS STD_QTY, ID.ALTER_SCRAP_QTY AS GROSS_WT_SCRAP," & vbCrLf & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM, MAXIMUM_QTY, MINIMUM_QTY "

            SqlStr = SqlStr & vbCrLf & " FROM PRD_OUTBOM_HDR IH, PRD_OUTBOM_ALTER_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY" & vbCrLf & " and ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.ALTER_ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND ID.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pRMMainCode) & "' " & vbCrLf & " AND ID.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pParentCode) & "' " & vbCrLf & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_OUTBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pParentCode) & "') "

            SqlStr = SqlStr & vbCrLf & " AND IH.IS_INHOUSE='N' AND IH.STATUS='O'"

            SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SERIAL_NO"
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsShow.EOF Then
            Do While Not RsShow.EOF
                mRMCode = Trim(IIf(IsDBNull(RsShow.Fields("RM_CODE").Value), "", RsShow.Fields("RM_CODE").Value))
                Call FillGridCol(RsShow, pMainProductCode, pRMMainCode, mProductPlanQty, mDivisionCode)
                RsShow.MoveNext()
            Loop
        End If
        RsShow = Nothing
        '        RsShow.Close

        Exit Sub
FillERR:
        MsgBox(Err.Description)
        '    Resume
    End Sub
    Private Sub FillSubRecord(ByRef pProductCode As String, ByRef pWEF As String, ByRef pMainProductCode As String, ByRef mProductPlanQty As Double, ByRef mDivisionCode As Double)

        On Error GoTo FillERR
        Dim SqlStr As String = ""
        Dim RsShow As ADODB.Recordset = Nothing
        Dim mRMCode As String

        SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, " & vbCrLf & " ID.RM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " ID.DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf & " ID.STD_QTY, ID.GROSS_WT_SCRAP," & vbCrLf & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM, MAXIMUM_QTY, MINIMUM_QTY "

        SqlStr = SqlStr & vbCrLf & " FROM PRD_NEWBOM_HDR IH,PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.RM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' " & vbCrLf & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_NEWBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "') " '& vbCrLf |            & " AND WEF<= '" & VB6.Format(pWEF, "DD-MMM-YYYY") & "')" & vbCrLf |
        SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SUBROWNO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsShow.EOF Then
            Do While Not RsShow.EOF
                mRMCode = Trim(IIf(IsDBNull(RsShow.Fields("RM_CODE").Value), "", RsShow.Fields("RM_CODE").Value))
                Call FillGridCol(RsShow, pMainProductCode, pProductCode, mProductPlanQty, mDivisionCode)
                RsShow.MoveNext()
            Loop
        Else

            SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, " & vbCrLf & " ID.ITEM_CODE AS RM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " 'J/W' AS DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf & " ID.ITEM_QTY AS STD_QTY, ID.SCRAP_QTY AS GROSS_WT_SCRAP," & vbCrLf & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM, MAXIMUM_QTY, MINIMUM_QTY "

            SqlStr = SqlStr & vbCrLf & " FROM PRD_OUTBOM_HDR IH,PRD_OUTBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' " & vbCrLf & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_OUTBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "') "

            SqlStr = SqlStr & vbCrLf & " AND IH.IS_INHOUSE='N' AND IH.STATUS='O'"

            SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SERIAL_NO"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

            If Not RsShow.EOF Then
                Do While Not RsShow.EOF
                    mRMCode = Trim(IIf(IsDBNull(RsShow.Fields("RM_CODE").Value), "", RsShow.Fields("RM_CODE").Value))
                    Call FillGridCol(RsShow, pMainProductCode, pProductCode, mProductPlanQty, mDivisionCode)
                    RsShow.MoveNext()
                Loop
            End If
        End If
        RsShow = Nothing
        '        RsShow.Close

        Exit Sub
FillERR:
        MsgBox(Err.Description)
        '    Resume
    End Sub

    Private Sub txtDespatchQty_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPlanQty.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtIssuedQty_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtIssuedQty.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtIssuedQty_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtIssuedQty.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtLineCapacity_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLineCapacity.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtProductCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProductCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtProductCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProductCode.DoubleClick
        Call cmdSearchProduct_Click(cmdSearchProduct, New System.EventArgs())
    End Sub

    Private Sub txtProductCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtProductCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtProductCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtProductCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtProductCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchProduct_Click(cmdSearchProduct, New System.EventArgs())
    End Sub

    Private Sub txtProductCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtProductCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mProductPlannedQty As Double
        Dim mDivisionCode As Double
        Dim pWIPLockQty As Double
        Dim mMainProductCode As String
        Dim pFGQty As Double
        Dim mLineCapacityQty As Double

        If Trim(txtReqDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtReqDate.Text) Then GoTo EventExitSub
        If Trim(txtProductCode.Text) = "" Then GoTo EventExitSub
        '    If lblIsSuppIssue.text = "Y" Then Exit Sub

        mMainProductCode = GetMainItemCode((txtProductCode.Text))
        txtProductCode.Text = mMainProductCode

        If MainClass.ValidateWithMasterTable(Trim(txtProductCode.Text), "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            lblProductDesc.Text = Trim(MasterNo)
        End If

        mProductPlannedQty = GetProductPlannedQty(mMainProductCode)
        txtPlanQty.Text = VB6.Format(mProductPlannedQty, "0.00")

        mLineCapacityQty = GetLineCapacityQty(txtProductCode.Text, txtDept.Text, txtReqDate.Text)
        txtLineCapacity.Text = VB6.Format(mLineCapacityQty, "0.00")

        txtIssuedQty.Text = CStr(GetDemandedQty())

        '    If lblIsSuppIssue.text = "N" Then
        '        txtRequestQty.Text = VB6.Format(Val(txtDespatchQty.Text) - Val(txtIssuedQty.Text), "0.00")
        '    End If

        mDivisionCode = CDbl("-1")

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        txtWIPQty.Text = GetWIPQty(Trim(txtProductCode.Text), mDivisionCode, pWIPLockQty, pFGQty)
        txtWIPLockQty.Text = VB6.Format(pWIPLockQty, "0.00")
        txtFGQty.Text = VB6.Format(pFGQty, "0.00")

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

        If FYChk((txtReqDate.Text)) = False Then
            Cancel = True
            GoTo EventExitSub
        End If

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
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , SqlStr) = True Then
            txtDept.Text = AcName1
            lblDeptname.Text = AcName
            '            txtDept_Validate False
            If txtDept.Enabled = True Then txtDept.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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

        If Trim(txtDept.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtDept.Text), "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            lblDeptname.Text = MasterNo
        Else
            MsgInformation("Invalid Depatment Code")
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
        Call cmdsearch_Click(cmdSearch, New System.EventArgs())
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdSearch, New System.EventArgs())
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

        If MODIFYMode = True And RsReqMain.EOF = False Then mReqnum = RsReqMain.Fields("AUTO_KEY_ISS").Value

        SqlStr = "Select * From INV_ISSUE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND LTRIM(RTRIM(AUTO_KEY_ISS))=" & Val(txtReqNo.Text) & " AND ISSUE_TYPE='N' AND IS_SUPP_ISSUE='" & lblIsSuppIssue.Text & "'"

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
                SqlStr = "Select * From INV_ISSUE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " LTRIM(RTRIM(AUTO_KEY_ISS))=" & Val(mReqnum) & " AND ISSUE_TYPE='N' AND IS_SUPP_ISSUE='" & lblIsSuppIssue.Text & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsReqMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRequestQty_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRequestQty.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRequestQty_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRequestQty.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSearchItem_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSearchItem.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        mSearchStartRow = 1
    End Sub

    Private Sub txtSearchItem_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSearchItem.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSearchItem.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub cmdSearchItem_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchItem.Click

        On Error GoTo ErrPart
        Dim mSearchItem As String
        Dim mFindItemName As String
        Dim I As Integer

        mSearchItem = Trim(txtSearchItem.Text)
        Dim counter As Short
        With SprdMain
            counter = mSearchStartRow
            For I = counter To .MaxRows
                .Row = I

                .Col = ColItemCode
                mFindItemName = Trim(.Text)

                '            If mSearchItem = mFindItemName Then
                If InStr(1, mFindItemName, mSearchItem, CompareMethod.Text) > 0 Then
                    If lblBookType.Text = "I" Then
                        MainClass.SetFocusToCell(SprdMain, I, ColIssueQty)
                    Else
                        MainClass.SetFocusToCell(SprdMain, I, ColDemandQty)
                    End If
                    mSearchStartRow = I + 1
                    GoTo NextRec
                End If

                .Col = ColItemDesc
                mFindItemName = Trim(.Text)

                '            If mSearchItem = mFindItemName Then
                If InStr(1, mFindItemName, mSearchItem, CompareMethod.Text) > 0 Then
                    If lblBookType.Text = "I" Then
                        MainClass.SetFocusToCell(SprdMain, I, ColIssueQty)
                    Else
                        MainClass.SetFocusToCell(SprdMain, I, ColDemandQty)
                    End If
                    mSearchStartRow = I + 1
                    GoTo NextRec
                End If
            Next
            mSearchStartRow = 1
NextRec:
        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtsubdept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtsubdept.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRequestQty_Validating(sender As Object, EventArgs As CancelEventArgs) Handles txtRequestQty.Validating
        Dim Cancel As Boolean = EventArgs.Cancel

        On Error GoTo ErrPart

        If Val(txtRequestQty.Text) = 0 Then GoTo EventExitSub


        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 110 Then

        Else
            If Val(txtRequestQty.Text) > Val(txtPlanQty.Text) Then
                MsgInformation("Request Qty Cann't be Greater than Plan Qty.")
                txtRequestQty.Focus()
                Cancel = True
                GoTo EventExitSub
            End If
        End If


        If Val(txtRequestQty.Text) > Val(txtLineCapacity.Text) Then
            MsgInformation("Request Qty Cann't be Greater than Line Capacity Qty.")
            txtRequestQty.Focus()
            Cancel = True
            GoTo EventExitSub
        End If

        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        EventArgs.Cancel = Cancel
    End Sub
    Public Function GetUnApprovedQty(ByVal pItemCode As String, ByVal pDivision As Double) As Double
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsBalStock As ADODB.Recordset = Nothing
        Dim mBalQty As Double

        GetUnApprovedQty = 0
        Exit Function

        SqlStr = ""
        SqlStr = "SELECT SUM(DEMAND_QTY-ISSUE_QTY) AS BALQTY" & vbCrLf _
            & " FROM INV_ISSUE_HDR IH, INV_ISSUE_DET ID " & vbCrLf _
            & " WHERE IH.AUTO_KEY_ISS=ID.AUTO_KEY_ISS AND  IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUBSTR(IH.AUTO_KEY_ISS,LENGTH(IH.AUTO_KEY_ISS)-5,4) = " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        If pDivision <> -1 Then
            SqlStr = SqlStr & vbCrLf & "AND IH.DIV_CODE=" & pDivision & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND IH.ISSUE_STATUS='N'"

        SqlStr = SqlStr & vbCrLf & "AND IH.DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'"

        If Val(txtReqNo.Text) > 0 Then
            SqlStr = SqlStr & vbCrLf & " AND  IH.AUTO_KEY_ISS<>" & Val(txtReqNo.Text) & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND IH.ISSUE_DATE<=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBalStock, ADODB.LockTypeEnum.adLockReadOnly)
        If RsBalStock.EOF = False Then
            If IsDBNull(RsBalStock.Fields(0).Value) Then
                mBalQty = 0
            Else
                mBalQty = RsBalStock.Fields(0).Value
            End If
        Else
            mBalQty = 0
        End If
        RsBalStock = Nothing

        GetUnApprovedQty = mBalQty
        Exit Function
ErrPart:
        GetUnApprovedQty = 0
    End Function

    Private Sub FrmStoreReqBOP_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 210, mReFormWidth - 210, mReFormWidth))
        FraFront.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        Frame6.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        SprdView.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 210, mReFormWidth - 210, mReFormWidth))
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
End Class
