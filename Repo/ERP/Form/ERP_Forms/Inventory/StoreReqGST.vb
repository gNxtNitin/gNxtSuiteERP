Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class FrmStoreReqGST
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
    Private Const ColLotNo As Short = 5
    Private Const ColStockQty As Short = 6
    Private Const ColDeptQty As Short = 7
    Private Const colStdQty As Short = 8
    Private Const ColDemandQty As Short = 9
    Private Const ColIssueQty As Short = 10
    Private Const ColIssuedQty As Short = 11
    Private Const ColBalQty As Short = 12
    Private Const ColRemarks As Short = 13

    Dim pDataShow As Boolean
    Dim FileDBCn As ADODB.Connection
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

        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DIV_DESC"
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
        Dim i As Integer
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
                    mItemCode = Trim(IIf(IsDbNull(RsFile.Fields(0).Value), "", RsFile.Fields(0).Value))

                    xSqlStr = " SELECT ITEM_SHORT_DESC,ISSUE_UOM " & vbCrLf & " FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND LTRIM(RTRIM(ITEM_CODE))='" & MainClass.AllowSingleQuote(mItemCode) & "'"
                    MainClass.UOpenRecordSet(xSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                    If RsTemp.EOF = False Then
                        mItemDesc = Trim(IIf(IsDbNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value))
                        mUOM = Trim(IIf(IsDbNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value))
                    Else
                        GoTo NextRecord
                    End If


                    mStockType = Trim(IIf(IsDbNull(RsFile.Fields(3).Value), "", RsFile.Fields(3).Value))
                    mDemandQty = Val(IIf(IsDbNull(RsFile.Fields(4).Value), 0, RsFile.Fields(4).Value))

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

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mTitle As String = ""

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        If lblBookType.Text = "I" Then
            mTitle = "Material Issue Note"
        Else
            mTitle = "Store Requisition Note"
        End If

        SqlStr = " SELECT INV_ISSUE_HDR.*,INV_ISSUE_DET.*,INV_ITEM_MST.ITEM_SHORT_DESC, " & vbCrLf & " PAY_EMPLOYEE_MST.EMP_NAME,PAY_DEPT_MST.DEPT_DESC " & vbCrLf & " FROM INV_ISSUE_HDR,INV_ISSUE_DET,INV_ITEM_MST, " & vbCrLf & " PAY_EMPLOYEE_MST,PAY_DEPT_MST " & vbCrLf & " WHERE INV_ISSUE_HDR.AUTO_KEY_ISS=INV_ISSUE_DET.AUTO_KEY_ISS(+) " & vbCrLf & " AND INV_ISSUE_DET.COMPANY_CODE=INV_ITEM_MST.COMPANY_CODE(+) " & vbCrLf & " AND INV_ISSUE_DET.ITEM_CODE=INV_ITEM_MST.ITEM_CODE(+) " & vbCrLf & " AND INV_ISSUE_HDR.COMPANY_CODE=PAY_EMPLOYEE_MST.COMPANY_CODE(+) " & vbCrLf & " AND INV_ISSUE_HDR.EMP_CODE=PAY_EMPLOYEE_MST.EMP_CODE(+) " & vbCrLf & " AND INV_ISSUE_HDR.COMPANY_CODE=PAY_DEPT_MST.COMPANY_CODE(+) " & vbCrLf & " AND INV_ISSUE_HDR.DEPT_CODE=PAY_DEPT_MST.DEPT_CODE(+) " & vbCrLf & " AND INV_ISSUE_HDR.AUTO_KEY_ISS=" & Val(txtReqNo.Text) & ""

        If lblBookType.Text = "I" Then
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\StoreIssue.rpt"
        Else
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\StoreReq.rpt"
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
        Dim mDivisionCode As Double

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ISSUE_STATUS='N' AND SUBSTR(AUTO_KEY_ISS,LENGTH(AUTO_KEY_ISS)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

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
        Call TxtDept_DoubleClick(TxtDept, New System.EventArgs())
    End Sub

    Private Sub cmdSearchCC_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchCC.Click
        Call txtCost_DoubleClick(txtCost, New System.EventArgs())
    End Sub

    Private Sub cmdSearchEmp_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchEmp.Click
        Call txtEmp_DoubleClick(txtEmp, New System.EventArgs())
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

    Private Sub FrmStoreReqGST_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Close()
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim xIName As String
        Dim SqlStr As String = ""
        Dim mItemCode As String
        Dim mLotNo As String
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
                    If Trim(txtDept.Text) <> "" And Val(txtprod.Text) <> 0 And Trim(lblProductCode.Text) <> "" Then
                        SqlStr = " SELECT ID.RM_CODE, ID.STD_QTY, INVMST.ITEM_SHORt_DESC" & vbCrLf & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.RM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote((lblProductCode.Text)) & "' " & vbCrLf & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote((txtDept.Text)) & "' " & vbCrLf & " AND IH.STATUS='O'"

                        SqlStr = SqlStr & vbCrLf & " UNION " & vbCrLf & " SELECT ID.ALTER_RM_CODE AS RM_CODE, ID.ALTER_STD_QTY AS STD_QTY, INVMST.ITEM_SHORt_DESC" & vbCrLf & " FROM PRD_NEWBOM_HDR IH, PRD_BOM_ALTER_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.MAINITEM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote((lblProductCode.Text)) & "' " & vbCrLf & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote((txtDept.Text)) & "' " & vbCrLf & " AND IH.STATUS='O'"

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
                    If Trim(txtDept.Text) <> "" And Val(txtprod.Text) <> 0 And Trim(lblProductCode.Text) <> "" Then
                        SqlStr = " SELECT INVMST.ITEM_SHORT_DESC, ID.ALTER_STD_QTY, ID.ALTER_RM_CODE" & vbCrLf & " FROM PRD_NEWBOM_HDR IH, PRD_BOM_ALTER_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.MAINITEM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(lblProductCode.Text) & "' " & vbCrLf & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "' " & vbCrLf & " AND IH.STATUS='O'"

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

                .Col = ColLotNo
                SqlStr = GetItemLotWiseQry(mItemCode, (txtReqDate.Text), mUOM, "STR", mStockType, mLotNo, ConWH, ConStockRefType_ISS, Val(txtReqNo.Text))
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
                        eventArgs.Cancel = True
                        Exit Sub
                    End If
                Else
                    eventArgs.Cancel = True
                    Exit Sub
                End If
            Case ColItemDesc
                SprdMain.Col = ColItemCode
                xItemDesc = Trim(SprdMain.Text)
                If xItemDesc = "" Then Exit Sub
                If FillItemDescPart(xItemDesc, False) = True Then
                    If DuplicateItem(ColItemCode) = True Then
                        eventArgs.Cancel = True
                        Exit Sub
                    End If
                Else
                    eventArgs.Cancel = True
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
                        eventArgs.Cancel = True
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
                    eventArgs.Cancel = True
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

                    If MainClass.ValidateWithMasterTable(xItemCode, "AUTO_INDENT", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AUTO_INDENT='Y'") = True Then
                        mAutoQCIssue = "Y"
                    Else
                        mAutoQCIssue = "N"
                    End If

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
                    mCommonDivision = GetCommonDivCode
                    mStockQty = GetBalanceStockQty(xItemCode, (txtReqDate.Text), xItemUOM, "STR", xStockType, xLotNo, ConWH, mDivisionCode, ConStockRefType_ISS, Val(txtReqNo.Text))
                    If RsCompany.Fields("COMPANY_CODE").Value = 1 And mAutoQCIssue = "N" Then
                        mStockQty = mStockQty + GetBalanceStockQty(xItemCode, (txtReqDate.Text), xItemUOM, "STR", "QC", xLotNo, ConWH, mDivisionCode, ConStockRefType_ISS, Val(txtReqNo.Text))
                    End If
                    If mDivisionCode <> mCommonDivision Then
                        If mCommonDivision > 0 Then
                            mStockQty = mStockQty + GetBalanceStockQty(xItemCode, (txtReqDate.Text), xItemUOM, "STR", xStockType, xLotNo, ConWH, mCommonDivision, ConStockRefType_ISS, Val(txtReqNo.Text))
                        End If
                    End If
                    SprdMain.Text = CStr(mStockQty)


                    SprdMain.Col = ColDeptQty
                    mWIPStock = GetBalanceStockQty(xItemCode, (txtReqDate.Text), xItemUOM, (txtDept.Text), "ST", "", ConPH, mDivisionCode, ConStockRefType_ISS, Val(txtReqNo.Text))
                    mWIPStock = mWIPStock + GetProductionStock(xItemCode, (txtDept.Text), mDivisionCode, (txtReqDate.Text), xItemUOM) ''GetDeptStock(xItemCode, mDivisionCode)
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

            Case ColStockType
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColItemCode
                xItemCode = Trim(SprdMain.Text)

                If MainClass.ValidateWithMasterTable(xItemCode, "AUTO_INDENT", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AUTO_INDENT='Y'") = True Then
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
                    MainClass.SetFocusToCell(SprdMain, eventArgs.Row, ColStockType)
                    eventArgs.Cancel = True
                    Exit Sub
                Else
                    If xStockType = "FG" Then
                        MsgInformation("Can't be Selected FG Stock Type")
                        MainClass.SetFocusToCell(SprdMain, eventArgs.Row, ColStockType)
                        eventArgs.Cancel = True
                        Exit Sub
                    End If

                    If xStockType = "CR" Then
                        MsgInformation("Can't be Selected CR Stock Type")
                        MainClass.SetFocusToCell(SprdMain, eventArgs.Row, ColStockType)
                        eventArgs.Cancel = True
                        Exit Sub
                    End If

                    If xStockType = "RJ" Then
                        MsgInformation("Can't be Selected RJ Stock Type")
                        MainClass.SetFocusToCell(SprdMain, eventArgs.Row, ColStockType)
                        eventArgs.Cancel = True
                        Exit Sub
                    End If

                    If xStockType = "QC" Then
                        MsgInformation("Can't be Selected QC Stock Type")
                        MainClass.SetFocusToCell(SprdMain, eventArgs.Row, ColStockType)
                        eventArgs.Cancel = True
                        Exit Sub
                    End If

                    SprdMain.Col = ColIssuedQty
                    mIssuedQty = Val(SprdMain.Text)

                    SprdMain.Col = ColStockQty
                    mCommonDivision = GetCommonDivCode
                    mStockQty = GetBalanceStockQty(xItemCode, (txtReqDate.Text), xItemUOM, "STR", xStockType, "", ConWH, mDivisionCode, ConStockRefType_ISS, Val(txtReqNo.Text))

                    If RsCompany.Fields("COMPANY_CODE").Value = 1 And mAutoQCIssue = "N" Then
                        mStockQty = mStockQty + GetBalanceStockQty(xItemCode, (txtReqDate.Text), xItemUOM, "STR", "QC", "", ConWH, mDivisionCode, ConStockRefType_ISS, Val(txtReqNo.Text))
                    End If

                    If mDivisionCode <> mCommonDivision Then
                        If mCommonDivision > 0 Then
                            mStockQty = mStockQty + GetBalanceStockQty(xItemCode, (txtReqDate.Text), xItemUOM, "STR", xStockType, "", ConWH, mCommonDivision, ConStockRefType_ISS, Val(txtReqNo.Text))
                        End If
                    End If
                    SprdMain.Text = CStr(mStockQty)

                    SprdMain.Col = ColDeptQty
                    mWIPStock = GetBalanceStockQty(xItemCode, (txtReqDate.Text), xItemUOM, (txtDept.Text), "ST", "", ConPH, mDivisionCode, ConStockRefType_ISS, Val(txtReqNo.Text))
                    mWIPStock = mWIPStock + GetProductionStock(xItemCode, (txtDept.Text), mDivisionCode, (txtReqDate.Text), xItemUOM) ''GetDeptStock(xItemCode, mDivisionCode)
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
            .Row = mRow
            .Col = ColItemCode
            mCheckItemCode = Trim(UCase(.Text))
            If IsChildItemExists(mCheckItemCode) = True Then
                DuplicateItem = True
                MsgInformation(mCheckItemCode & " : Item Code is parent and Child Code is Exists.")
                MainClass.SetFocusToCell(SprdMain, mRow, pCol)
                Exit Function
            End If
            If IsParentItemExists(mCheckItemCode) = True Then
                DuplicateItem = True
                MsgInformation(mCheckItemCode & " : Item Code is Child and Parent Code is Exists.")
                MainClass.SetFocusToCell(SprdMain, mRow, pCol)
                Exit Function
            End If
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
                mChildCode = Trim(IIf(IsDbNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))
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
                mParentcode = Trim(IIf(IsDbNull(RsTemp.Fields("PARENT_CODE").Value), "", RsTemp.Fields("PARENT_CODE").Value))
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
        Dim RsTempBOM As ADODB.Recordset = Nothing
        Dim xItemCode As String = ""
        Dim mItemClassification As String

        xAutoIssue = CheckAutoIssue((txtReqDate.Text), pItemCode)

        If Trim(pItemCode) = "" Then Exit Function
        With SprdMain
            SqlStr = "SELECT ITEM_CODE, ITEM_SHORT_DESC,ISSUE_UOM,ITEM_CLASSIFICATION  " & vbCrLf & " FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

            If pIsItemCode = True Then
                SqlStr = SqlStr & vbCrLf & " AND LTRIM(RTRIM(ITEM_CODE))='" & MainClass.AllowSingleQuote(pItemCode) & "'"
            Else
                SqlStr = SqlStr & vbCrLf & " AND LTRIM(RTRIM(ITEM_SHORT_DESC))='" & MainClass.AllowSingleQuote(pItemCode) & "'"
            End If

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                mItemClassification = Trim(IIf(IsDbNull(RsTemp.Fields("ITEM_CLASSIFICATION").Value), "", RsTemp.Fields("ITEM_CLASSIFICATION").Value))

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
                .Text = Trim(IIf(IsDbNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))
                xItemCode = Trim(IIf(IsDbNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))

                .Col = ColItemDesc
                .Text = IIf(IsDbNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value)

                .Col = ColUom
                .Text = IIf(IsDbNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)

                .Col = ColStockType
                .Text = IIf(Trim(.Text) = "", "ST", Trim(.Text))

                If Trim(txtDept.Text) <> "" And Val(txtprod.Text) <> 0 And Trim(lblProductCode.Text) <> "" Then
                    SqlStr = " SELECT ID.RM_CODE, ID.STD_QTY" & vbCrLf & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IH.MKEY=ID.MKEY " & vbCrLf & " AND " & vbCrLf & " IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(lblProductCode.Text) & "' " & vbCrLf & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "' " & vbCrLf & " AND ID.RM_CODE='" & MainClass.AllowSingleQuote(xItemCode) & "'" & vbCrLf & " AND IH.STATUS='O'"

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempBOM, ADODB.LockTypeEnum.adLockReadOnly)

                    If RsTempBOM.EOF = False Then
                        .Col = colStdQty
                        .Text = IIf(IsDbNull(RsTempBOM.Fields("STD_QTY").Value), 0, RsTempBOM.Fields("STD_QTY").Value)
                    Else

                        SqlStr = " SELECT ID.ALTER_STD_QTY" & vbCrLf & " FROM PRD_NEWBOM_HDR IH, PRD_BOM_ALTER_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(lblProductCode.Text) & "' " & vbCrLf & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "' " & vbCrLf & " AND ID.ALTER_RM_CODE='" & MainClass.AllowSingleQuote(xItemCode) & "'" & vbCrLf & " AND IH.STATUS='O'"

                        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempBOM, ADODB.LockTypeEnum.adLockReadOnly)
                        If RsTempBOM.EOF = False Then
                            .Col = colStdQty
                            .Text = IIf(IsDbNull(RsTempBOM.Fields("ALTER_STD_QTY").Value), 0, RsTempBOM.Fields("ALTER_STD_QTY").Value)
                        Else
                            If CDate(txtReqDate.Text) >= CDate("08/12/2014") Then
                                MsgInformation("Invalid Item Code for Product Code : " & lblProductCode.Text)
                                FillItemDescPart = False
                                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColItemCode)
                                Exit Function
                            End If
                        End If
                    End If
                End If
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

        mEntryDate = VB6.Format(PubCurrDate, "DD-MMM-YYYY") & " " & GetServerTime

        mDivisionDesc = cboDivision.Text
        If MainClass.ValidateWithMasterTable(mDivisionDesc, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Val(MasterNo)
        End If


        SqlStr = ""
        If ADDMode = True Then
            lblMKey.Text = CStr(mVNoSeq)
            SqlStr = "INSERT INTO INV_ISSUE_HDR (" & vbCrLf & " AUTO_KEY_ISS, " & vbCrLf & " COMPANY_CODE, " & vbCrLf & " ISSUE_DATE, " & vbCrLf & " DEPT_CODE, " & vbCrLf & " EMP_CODE, REMARKS, COST_CENTER_CODE, DAILY_PLAN_NO, " & vbCrLf & " SHIFT_CODE,ISSUE_STATUS, ISSUE_FOR, " & vbCrLf & " ADDUSER,ADDDATE,MODUSER,MODDATE,DIV_CODE,INHOUSE_CODE)" & vbCrLf & " VALUES( " & vbCrLf & " " & Val(CStr(mVNoSeq)) & "," & vbCrLf & " " & RsCompany.Fields("Company_Code").Value & "," & vbCrLf & " TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & MainClass.AllowSingleQuote((txtDept.Text)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtEmp.Text)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtsubdept.Text)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtCost.Text)) & "', " & vbCrLf & " " & Val(txtprod.Text) & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote((cboShiftcd.Text)) & "', " & vbCrLf & " '" & mStatus & "', '" & VB.Left(cboStockFor.Text, 1) & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & mEntryDate & "','DD-MON-YYYY HH24:MI'),'',''," & mDivisionCode & ",'" & MainClass.AllowSingleQuote((lblProductCode.Text)) & "')"

            ''VB6.Format(PubCurrDate, "DD-MMM-YYYY")
        ElseIf MODIFYMode = True Then
            SqlStr = ""
            SqlStr = "UPDATE INV_ISSUE_HDR SET ISSUE_DATE=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " DEPT_CODE='" & txtDept.Text & "', INHOUSE_CODE='" & MainClass.AllowSingleQuote((lblProductCode.Text)) & "'," & vbCrLf & " EMP_CODE ='" & txtEmp.Text & "', " & vbCrLf & " REMARKS ='" & MainClass.AllowSingleQuote((txtsubdept.Text)) & "'," & vbCrLf & " COST_CENTER_CODE ='" & txtCost.Text & "'," & vbCrLf & " DAILY_PLAN_NO =" & Val(txtprod.Text) & "," & vbCrLf & " SHIFT_CODE ='" & MainClass.AllowSingleQuote((cboShiftcd.Text)) & "', " & vbCrLf & " ISSUE_FOR ='" & VB.Left(cboStockFor.Text, 1) & "', " & vbCrLf & " ISSUE_STATUS ='" & mStatus & "'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),DIV_CODE=" & mDivisionCode & " " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND AUTO_KEY_ISS =" & Val(lblMKey.Text) & ""
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
        Dim i As Integer
        Dim mItemCode As String
        Dim mUOM As String = ""
        Dim mStockType As String = ""
        Dim mIssueQty As Double
        Dim mRemarks As String
        Dim mFirstTime As Boolean

        With SprdMain
            For i = 1 To .MaxRows - 1
                .Row = i

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
                        SqlStr = " INSERT INTO PRD_ISSREC_DET ( " & vbCrLf & " COMPANY_CODE,AUTO_KEY_ISSREC,SERIAL_NO,ITEM_CODE,ITEM_UOM,FROM_STOCK_TYPE, " & vbCrLf & " ISSUE_QTY,RECV_QTY,OPR_CODE,NEXTOPR_CODE,REMARKS,AUTO_KEY_ISS) " & vbCrLf & " VALUES ( " & RsCompany.Fields("COMPANY_CODE").Value & "," & pIssueNo & ", " & i & "," & vbCrLf & " '" & mItemCode & "','" & mUOM & "','" & mStockType & "', " & vbCrLf & " " & mIssueQty & ",0,'','', " & vbCrLf & " '" & mRemarks & "'," & Val(lblMKey.Text) & ") "
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
                If Not IsDbNull(.Fields(0).Value) Then
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
        Dim i As Integer
        Dim mItemCode As String
        Dim mUOM As String = ""
        Dim mStockType As String = ""
        Dim mQty As Double
        Dim mRemarks As String
        Dim mIssueQty As Double
        Dim mLotNoRequied As String
        Dim mProd_Type As String
        Dim mIsConsumable As String = "N"
        Dim mLotNo As String

        Dim cntRow As Integer
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

        SqlStr = " Delete From INV_ISSUE_DET WHERE AUTO_KEY_ISS=" & Val(lblMKey.Text) & ""
        PubDBCn.Execute(SqlStr)

        If lblBookType.Text = "I" Then
            If DeleteStockTRN(PubDBCn, ConStockRefType_ISS, (txtReqNo.Text)) = False Then GoTo UpdateDetail1Err

            If DeletePaintStockTRN(PubDBCn, ConStockRefType_ISS, (txtReqNo.Text)) = False Then GoTo UpdateDetail1Err

            PubDBCn.Execute("DELETE FROM DSP_PAINT57F4_TRN WHERE MKey='" & lblMKey.Text & "' AND BookType='D' AND BookSubType='O' AND TRNTYPE='I'")
        End If

        mSno = 5000
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

                SqlStr = ""

                If mItemCode <> "" And mQty > 0 Then
                    SqlStr = " INSERT INTO INV_ISSUE_DET ( " & vbCrLf & " AUTO_KEY_ISS,SERIAL_NO,ITEM_CODE,ITEM_UOM,REMARKS," & vbCrLf & " FROM_STOCK_TYPE,DEMAND_QTY,ISSUE_QTY, COMPANY_CODE,LOT_NO) "
                    SqlStr = SqlStr & vbCrLf & " VALUES (" & Val(lblMKey.Text) & ", " & i & "," & vbCrLf & " '" & MainClass.AllowSingleQuote(mItemCode) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mUOM) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mRemarks) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mStockType) & "', " & vbCrLf & " " & mQty & "," & mIssueQty & ", " & RsCompany.Fields("COMPANY_CODE").Value & ",'" & mLotNo & "') "
                    PubDBCn.Execute(SqlStr)

                    mLotNo = mLotNo

                    If lblBookType.Text = "I" Then

                        xSqlStr = "SELECT ITEM_CODE FROM INV_ITEM_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IS_CHILD='Y' AND PARENT_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
                        MainClass.UOpenRecordSet(xSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                        If RsTemp.EOF = False Then
                            cntStockSno = 500
                            Do While RsTemp.EOF = False
                                cntStockSno = cntStockSno + 1
                                If mBalIssueQty > 0 Then
                                    xItemCode = IIf(IsDbNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)
                                    xChildStock = GetBalanceStockQty(xItemCode, (txtReqDate.Text), mUOM, "STR", mStockType, mLotNo, ConWH, mDivisionCode, ConStockRefType_ISS, Val(txtReqNo.Text))
                                    If mBalIssueQty >= xChildStock Then
                                        xChildIssue = xChildStock
                                        mBalIssueQty = mBalIssueQty - xChildStock
                                    Else
                                        xChildIssue = mBalIssueQty
                                        mBalIssueQty = 0
                                    End If
                                    If UpdateStockTRN(PubDBCn, ConStockRefType_ISS, (txtReqNo.Text), i + cntStockSno, (txtReqDate.Text), (txtReqDate.Text), mStockType, xItemCode, mUOM, mLotNo, xChildIssue, 0, "O", 0, 0, "", "", "STR", (txtDept.Text), "", "N", "To : " & lblDeptname.Text, "-1", ConWH, mDivisionCode, "", "") = False Then GoTo UpdateDetail1Err

                                    If mProd_Type = "P" Or mProd_Type = "J" Or mProd_Type = "C" Or mProd_Type = "R" Or mProd_Type = "B" Or mProd_Type = "I" Or mProd_Type = "3" Then

                                        If RsCompany.Fields("COMPANY_CODE").Value = 1 And CDate(txtReqDate.Text) < CDate("01-DEC-2005") Then GoTo NextRec
                                        cntStockSno = cntStockSno + 1
                                        If UpdateStockTRN(PubDBCn, ConStockRefType_ISS, (txtReqNo.Text), i + cntStockSno, (txtReqDate.Text), (txtReqDate.Text), mStockType, xItemCode, mUOM, mLotNo, xChildIssue, 0, "I", 0, 0, "", "", (txtDept.Text), (txtDept.Text), "", "N", "From : STORE TO :" & lblDeptname.Text, "-1", ConPH, mDivisionCode, "", "") = False Then GoTo UpdateDetail1Err
                                    End If
                                End If
                                RsTemp.MoveNext()
                            Loop
                            If mBalIssueQty > 0 Then
                                If UpdateStockTRN(PubDBCn, ConStockRefType_ISS, (txtReqNo.Text), i, (txtReqDate.Text), (txtReqDate.Text), mStockType, mItemCode, mUOM, mLotNo, mBalIssueQty, 0, "O", 0, 0, "", "", "STR", (txtDept.Text), "", "N", "To : " & lblDeptname.Text, "-1", ConWH, mDivisionCode, "", "") = False Then GoTo UpdateDetail1Err

                                If mProd_Type = "P" Or mProd_Type = "J" Or mProd_Type = "C" Or mProd_Type = "R" Or mProd_Type = "B" Or mProd_Type = "I" Or mProd_Type = "3" Then

                                    If RsCompany.Fields("COMPANY_CODE").Value = 1 And CDate(txtReqDate.Text) < CDate("01-DEC-2005") Then GoTo NextRec
                                    If UpdateStockTRN(PubDBCn, ConStockRefType_ISS, (txtReqNo.Text), i, (txtReqDate.Text), (txtReqDate.Text), mStockType, mItemCode, mUOM, mLotNo, mBalIssueQty, 0, "I", 0, 0, "", "", (txtDept.Text), (txtDept.Text), "", "N", "From : STORE TO :" & lblDeptname.Text, "-1", ConPH, mDivisionCode, "", "") = False Then GoTo UpdateDetail1Err
                                End If
                            End If
                        Else
                            mCommonDivisionStock = 0
                            mBalQty = 0
                            mCommonDivision = GetCommonDivCode
                            mIssueDivisionStock = GetBalanceStockQty(mItemCode, (txtReqDate.Text), mUOM, "STR", mStockType, mLotNo, ConWH, mDivisionCode, ConStockRefType_ISS, Val(txtReqNo.Text))
                            If mDivisionCode <> mCommonDivision Then
                                If mCommonDivision > 0 Then
                                    mCommonDivisionStock = GetBalanceStockQty(mItemCode, (txtReqDate.Text), mUOM, "STR", mStockType, mLotNo, ConWH, mCommonDivision, ConStockRefType_ISS, Val(txtReqNo.Text))
                                End If
                            End If

                            If mIssueQty <= mIssueDivisionStock Then
                                If UpdateStockTRN(PubDBCn, ConStockRefType_ISS, (txtReqNo.Text), i, (txtReqDate.Text), (txtReqDate.Text), mStockType, mItemCode, mUOM, mLotNo, mIssueQty, 0, "O", 0, 0, "", "", "STR", (txtDept.Text), "", "N", "To : " & lblDeptname.Text & IIf(VB.Left(cboStockFor.Text, 1) = "N", " - For Delevopment", IIf(VB.Left(cboStockFor.Text, 1) = "C", " - Capitalized", "")), "-1", ConWH, mDivisionCode, "", "") = False Then GoTo UpdateDetail1Err
                            Else
                                If mCommonDivision > 0 And mDivisionCode <> mCommonDivision Then
                                    If mIssueDivisionStock > 0 Then
                                        If UpdateStockTRN(PubDBCn, ConStockRefType_ISS, (txtReqNo.Text), i, (txtReqDate.Text), (txtReqDate.Text), mStockType, mItemCode, mUOM, mLotNo, mIssueDivisionStock, 0, "O", 0, 0, "", "", "STR", (txtDept.Text), "", "N", "To : " & lblDeptname.Text & IIf(VB.Left(cboStockFor.Text, 1) = "N", " - For Delevopment", IIf(VB.Left(cboStockFor.Text, 1) = "C", " - Capitalized", "")), "-1", ConWH, mDivisionCode, "", "") = False Then GoTo UpdateDetail1Err

                                        mBalQty = mIssueQty - mIssueDivisionStock
                                    Else
                                        mBalQty = mIssueQty
                                    End If

                                    If mBalQty <= mCommonDivisionStock Then
                                        If UpdateStockTRN(PubDBCn, ConStockRefType_ISS, (txtReqNo.Text), i + mSno, (txtReqDate.Text), (txtReqDate.Text), mStockType, mItemCode, mUOM, mLotNo, mBalQty, 0, "O", 0, 0, "", "", "STR", (txtDept.Text), "", "N", "To : " & lblDeptname.Text & IIf(VB.Left(cboStockFor.Text, 1) = "N", " - For Delevopment", IIf(VB.Left(cboStockFor.Text, 1) = "C", " - Capitalized", "")), "-1", ConWH, mCommonDivision, "", "") = False Then GoTo UpdateDetail1Err
                                    End If
                                    mSno = mSno + 1
                                Else
                                    If UpdateStockTRN(PubDBCn, ConStockRefType_ISS, (txtReqNo.Text), i, (txtReqDate.Text), (txtReqDate.Text), mStockType, mItemCode, mUOM, mLotNo, mIssueQty, 0, "O", 0, 0, "", "", "STR", (txtDept.Text), "", "N", "To : " & lblDeptname.Text & IIf(VB.Left(cboStockFor.Text, 1) = "N", " - For Delevopment", IIf(VB.Left(cboStockFor.Text, 1) = "C", " - Capitalized", "")), "-1", ConWH, mDivisionCode, "", "") = False Then GoTo UpdateDetail1Err
                                End If
                            End If
                            If mProd_Type = "P" Or mProd_Type = "J" Or mProd_Type = "C" Or mProd_Type = "R" Or mProd_Type = "B" Or mProd_Type = "I" Or mProd_Type = "3" Then

                                If RsCompany.Fields("COMPANY_CODE").Value = 1 And CDate(txtReqDate.Text) < CDate("01-DEC-2005") Then GoTo NextRec
                                If UpdateStockTRN(PubDBCn, ConStockRefType_ISS, (txtReqNo.Text), i, (txtReqDate.Text), (txtReqDate.Text), mStockType, mItemCode, mUOM, mLotNo, mIssueQty, 0, "I", 0, 0, "", "", (txtDept.Text), (txtDept.Text), "", "N", "From : STORE TO :" & lblDeptname.Text, "-1", ConPH, mDivisionCode, "", "") = False Then GoTo UpdateDetail1Err
                            End If
                        End If
NextRec:
                        If VB.Left(cboStockFor.Text, 1) = "S" Then
                            If UpdateStockTRN(PubDBCn, ConStockRefType_ISS, (txtReqNo.Text), i, (txtReqDate.Text), (txtReqDate.Text), mStockType, mItemCode, mUOM, mLotNo, mIssueQty, 0, "I", 0, 0, "", "", (txtDept.Text), "STR", "", "N", "From : STORE TO : " & lblDeptname.Text, "-1", ConSH, mDivisionCode, "", "") = False Then GoTo UpdateDetail1Err
                        End If

                        mLotNoRequied = "N"
                        If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mLotNoRequied = MasterNo
                        End If

                        If mLotNoRequied = "Y" Then
                            If UpdateLotInPaintStock(i, (txtReqNo.Text), (txtReqDate.Text), mItemCode, mUOM, mIssueQty, (lblDeptname.Text)) = False Then GoTo UpdateDetail1Err
                        End If

                        If RsCompany.Fields("COMPANY_CODE").Value = 9 Then
                            If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "CONSUMABLE_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                                mIsConsumable = MasterNo
                            End If

                            If mIsConsumable = "Y" Then
                                cntRow = cntRow * 100
                                If UpdateF4Stock(mItemCode, mUOM, mIssueQty, cntRow) = False Then GoTo UpdateDetail1Err
                            End If
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
                pF4No = IIf(IsDbNull(RsTemp.Fields("PARTY_F4NO").Value), "0", RsTemp.Fields("PARTY_F4NO").Value)
                pF4Date = VB6.Format(IIf(IsDbNull(RsTemp.Fields("PARTY_F4DATE").Value), "", RsTemp.Fields("PARTY_F4DATE").Value), "DD/MM/YYYY")
                mF4Qty = IIf(IsDbNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)

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
            pPartyCode = VB6.Format(IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value))
            pPartyF4Date = VB6.Format(IIf(IsDbNull(RsTemp.Fields("PARTY_F4DATE").Value), "", RsTemp.Fields("PARTY_F4DATE").Value), "DD/MM/YYYY")
            pOurVDate = VB6.Format(IIf(IsDbNull(RsTemp.Fields("VDate").Value), "", RsTemp.Fields("VDate").Value), "DD/MM/YYYY")
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

        If lblBookType.Text = "R" And RsCompany.Fields("COMPANY_CODE").Value = 16 Then
            If CheckPendingReqSlip() >= 3 Then
                MsgBox("There are 3 Store Requisition Slips are pending, So that you cann't be made new Store Requisition.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
        End If

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

            mCommonDivision = GetCommonDivCode

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

        If PubSuperUser <> "S" Then
            If lblBookType.Text = "R" And MODIFYMode = True Then
                If CheckMaterialIssue() = True Then
                    MsgBox("Material Issue Against this Store Requisition, So cann't be Save.", MsgBoxStyle.Information)
                    FieldsVarification = False
                    Exit Function
                End If
            End If
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



        If MODIFYMode = True Then
            If CheckDieselConsumptionEntry = True Then
                MsgBox("You Cann't be Change This Entry, Data is Entered by Diesel Consumption.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
        End If
        ''RsCompany.fields("COMPANY_CODE").value = 1 Or         ''24-04-2011
        If MainClass.ValidateWithMasterTable(txtDept, "DEPT_CODE", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DEPT_TYPE='D'") = True Then
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

        If RsCompany.Fields("COMPANY_CODE").Value <> 16 Then
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
        End If

        If RsCompany.Fields("CHECK_BOP_STOCK").Value = "Y" Then
            With SprdMain
                If CDate(txtReqDate.Text) >= CDate("18/09/2008") Then
                    If ShowProdPlan(Val(txtprod.Text)) = False Then GoTo err_Renamed

                    For cntRow = 1 To .MaxRows
                        .Row = cntRow
                        .Col = ColItemCode
                        mItemCode = Trim(.Text)

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
                            mLotNoRequied = "N"
                            If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                                mLotNoRequied = MasterNo
                            End If
                            If mLotNoRequied = "Y" Then
                                .Col = ColLotNo
                                If Trim(.Text) = "" Then
                                    MsgInformation("Lot No. Must For Such Item.")
                                    FieldsVarification = False
                                    MainClass.SetFocusToCell(SprdMain, cntRow, ColLotNo)
                                    Exit Function
                                End If
                            End If

                            If GetUserPermission("ALLOW_EXCESS_ISSUE", "N", PubUserID, RsCompany.Fields("COMPANY_CODE").Value) = "N" Then '

                                '                           If mCheckProdType = "P" And IsProductionItem(mItemCode) = True Then
                                If mCheckProdType = "B" Or mCheckProdType = "P" Or mCheckProdType = "I" Or mCheckProdType = "3" Then ''mCheckProdType = "P" Or
                                    '' temp
                                    '                                If Val(txtprod.Text) = 0 Then
                                    '                                    FieldsVarification = False
                                    '                                    MsgInformation "Please Check Production Plan, Cann't be Saved."
                                    '                                    If txtprod.Enabled = True Then txtprod.SetFocus
                                    '                                    Exit Function
                                    '                                End If
                                    '
                                    '                                If Val(lblPlanningQty.text) = 0 Then
                                    '                                    FieldsVarification = False
                                    '                                    MsgInformation "Please Check Production Plan Qty, Cann't be Saved."
                                    '                                    If txtprod.Enabled = True Then txtprod.SetFocus
                                    '                                    Exit Function
                                    '                                End If

                                    .Row = cntRow
                                    '                                .Col = ColDeptQty
                                    '                                mDeptQty = Val(.Text)
                                    .Col = ColUom
                                    mUOM = Trim(.Text)

                                    mDeptQty = GetBalanceStockQty(mItemCode, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(txtReqDate.Text))), mUOM, (txtDept.Text), "ST", CStr(-1), ConPH, mDivisionCode) '', ConStockRefType_ISS, Val(txtReqNo.Text))

                                    .Col = colStdQty
                                    mStdQty = Val(.Text)


                                    .Col = ColDemandQty
                                    mDemandedQty = Val(.Text)

                                    .Col = ColIssueQty
                                    mIssueQty = Val(.Text)

                                    mTodayReq = GetTodayPlanning(mItemCode, mPlanning, mWIPStock)
                                    mMinReq = mPlanning * 0.5 ' GetMinInventory(mItemCode)
                                    mTodayDemanded = GetToDayDemanded(mItemCode)
                                    mTotTodayDemanded = mTodayDemanded + mDemandedQty
                                    mTodayIssue = GetToDayIssue(mItemCode) ''+ mIssueQty

                                    If lblBookType.Text = "I" Then
                                        mXCheck = mIssueQty
                                    Else
                                        mXCheck = mDemandedQty
                                    End If
                                    If mXCheck <> 0 Then

                                        '                                    If CheckReqAgtPlann(mItemCode, Val(txtprod.Text), (Val(lblPlanningQty.text) * mStdQty),iif(lblBookType.text = "R", mDemandedQty,mIssueQty)) = True Then
                                        If CheckReqAgtPlann(mItemCode, Val(txtprod.Text), mTodayReq + mMinReq, IIf(lblBookType.Text = "R", mDemandedQty, mIssueQty)) = True Then
                                            FieldsVarification = False
                                            '                                MsgInformation " Requisition already made against such Production Plan , so cann't be Saved"
                                            MainClass.SetFocusToCell(SprdMain, cntRow, ColDemandQty)
                                            Exit Function
                                        End If
                                        '                                If mDemandedQty > (Val(lblPlanningQty.text) * mSTDQty) - mDeptQty Then
                                        '                                If mDemandedQty > (mTodayReq - mDeptQty) Then      ''10/10/08

                                        If lblBookType.Text = "R" Then
                                            If mTodayReq + mMinReq < mDeptQty + mTotTodayDemanded Then ''mTodayIssue + mIssueQty
                                                FieldsVarification = False
                                                '                                    MsgInformation "Demanded Qty Cann't be Greater than (Planning Qty - WIP Qty), so cann't be Saved"
                                                mString = "Item Code : " & mItemCode
                                                mString = mString & vbNewLine & "(A) " & mWIPStock
                                                mString = mString & vbNewLine & "(B) Today Planning (" & mPlanning & ")"
                                                mString = mString & vbNewLine & "(C) Today Max. Requirement (" & mTodayReq & ")"
                                                mString = mString & vbNewLine & "(D) Minimum Qty (" & mMinReq & ")"
                                                mString = mString & vbNewLine & "(E) OP Dept Stock (" & mDeptQty & ")"
                                                mString = mString & vbNewLine & "(D=C+D-E) Today Max Demanded Qty (" & mMinReq + mTodayReq - mDeptQty & ")"
                                                mString = mString & vbNewLine & "Today Already Demanded Qty (" & mTodayDemanded & ")"
                                                mString = mString & vbNewLine & "Current Demanded Qty (" & mDemandedQty & ")"
                                                '                                    mString = mString & vbNewLine & "Demanded Qty Cann't be Greater than (Planning Qty - WIP Qty), so cann't be Saved"
                                                If mTodayReq > mMinReq + mTotTodayDemanded - mDeptQty Then
                                                    mString = mString & vbNewLine & "Now You can Demand Only : " & mMinReq + mTodayReq - mTodayDemanded - mDeptQty & " Qty."
                                                End If
                                                MsgInformation(mString)
                                                MainClass.SetFocusToCell(SprdMain, cntRow, ColDemandQty)
                                                Exit Function
                                            End If
                                            '                                    ElseIf lblBookType.text = "I" Then
                                            '                                        If mTodayReq + mMinReq < mDeptQty + mTodayIssue + mIssueQty Then
                                            '                                            FieldsVarification = False
                                            '        '                                    MsgInformation "Demanded Qty Cann't be Greater than (Planning Qty - WIP Qty), so cann't be Saved"
                                            '                                            mString = "Item Code : " & mItemCode
                                            '                                            mString = mString & vbNewLine & "(A) Today Requirement (" & mTodayReq & ") & (B) Minimum Qty (" & mMinReq & ")"
                                            '                                            mString = mString & vbNewLine & "(C) OP Department Stock (" & mDeptQty & ")"
                                            '                                            mString = mString & vbNewLine & "(D=A+B-C) Today Max Demanded Qty (" & mMinReq + mTodayReq - mDeptQty & ")"
                                            '                                            mString = mString & vbNewLine & "Today Already Issued Qty (" & mTodayIssue & ")"
                                            '                                            mString = mString & vbNewLine & "Current Issued Qty     : " & mIssueQty
                                            '        '                                    mString = mString & vbNewLine & "Demanded Qty Cann't be Greater than (Planning Qty - WIP Qty), so cann't be Saved"
                                            '                                            If mTodayReq > mMinReq + mTodayIssue + mIssueQty - mDeptQty Then
                                            '                                                mString = mString & vbNewLine & "Now You can Demand Only : " & mMinReq + mTodayIssue - mDeptQty & " Qty."
                                            '                                            End If
                                            '                                            MsgInformation mString
                                            '                                            MainClass.SetFocusToCell SprdMain, cntRow, ColDemandQty
                                            '                                            Exit Function
                                            '                                        End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    Next
                End If
            End With
        End If

NextLine1:


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

            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
            SqlStr = SqlStr & vbCrLf & " AND (EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE='' OR EMP_LEAVE_DATE>=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"


            If MainClass.ValidateWithMasterTable(txtEmp.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
                MsgBox("Invalid Employee Code. Cann't Save", MsgBoxStyle.Information)
                FieldsVarification = False
                txtEmp.Focus()
                Exit Function
            End If
        End If

        If PubUserID <> "G0416" Then
            mCheckLastEntryDate = GetLastEntryDate
            If mCheckLastEntryDate <> "" Then
                If CDate(txtReqDate.Text) < CDate(mCheckLastEntryDate) Then
                    MsgBox("Cann't be Add or Modify Back Entry", MsgBoxStyle.Information)
                    FieldsVarification = False
                    Exit Function
                End If
            End If
        End If

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

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColItemCode
                If Trim(.Text) <> "" Then
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
                    If Val(.Text) > 0 Then
                        mDataTrue = True
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

        If BudgetValidation(Trim(txtDept.Text)) = False Then
            FieldsVarification = False
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
    Private Function BudgetValidation(ByRef pDeptCode As String) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim cntRow As Integer
        Dim pItemCode As String
        Dim mDivisionCode As Double
        Dim mIssueQty As Double
        Dim mIssuedQty As Double
        Dim mBudgetQty As Double

        BudgetValidation = False

        If cboDivision.SelectedIndex >= 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
        Else
            Exit Function
        End If

        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                mIssueQty = 0
                mIssuedQty = 0
                mBudgetQty = 0

                .Row = cntRow
                .Col = ColItemCode
                pItemCode = Trim(.Text)

                .Col = ColIssueQty
                mIssueQty = Val(.Text)

                SqlStr = "SELECT SUM(ITEM_QTY) As ITEM_QTY " & vbCrLf & " FROM INV_BUDGET_HDR IH, INV_BUDGET_DET ID " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.AUTO_KEY_REF=ID.AUTO_KEY_REF " & vbCrLf & " AND IH.DEPT_CODE='" & MainClass.AllowSingleQuote(pDeptCode) & "'" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf & " AND IH.DIV_CODE=" & Val(CStr(mDivisionCode)) & "" & vbCrLf & " AND BUDGET_STATUS='Y' AND BUDGET_CLOSED='N'" & vbCrLf & " AND AMEND_WEF_DATE = (" & vbCrLf & " SELECT MAX(AMEND_WEF_DATE) " & vbCrLf & " FROM INV_BUDGET_HDR" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(pDeptCode) & "'" & vbCrLf & " AND DIV_CODE=" & Val(CStr(mDivisionCode)) & "" & vbCrLf & " AND BUDGET_STATUS='Y' AND BUDGET_CLOSED='N'" & vbCrLf & " AND AMEND_WEF_DATE<=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                If RsTemp.EOF = False Then
                    mBudgetQty = IIf(IsDbNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)
                End If

                If mBudgetQty > 0 Then
                    SqlStr = "SELECT SUM(ISSUE_QTY) As ISSUE_QTY " & vbCrLf & " FROM INV_ISSUE_HDR IH, INV_ISSUE_DET ID " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.AUTO_KEY_ISS=ID.AUTO_KEY_ISS " & vbCrLf & " AND IH.DEPT_CODE='" & MainClass.AllowSingleQuote(pDeptCode) & "'" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf & " AND IH.DIV_CODE=" & Val(CStr(mDivisionCode)) & "" & vbCrLf & " AND TO_CHAR(IH.ISSUE_DATE,'YYYYMM')='" & VB6.Format(txtReqDate.Text, "YYYYMM") & "'"

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                    If RsTemp.EOF = False Then
                        mIssuedQty = IIf(IsDbNull(RsTemp.Fields("ISSUE_QTY").Value), 0, RsTemp.Fields("ISSUE_QTY").Value)
                    End If
                    mIssuedQty = mIssuedQty + mIssueQty

                    If mBudgetQty < mIssuedQty Then
                        MsgInformation("Issued Qty (" & mIssuedQty & ") is Already Exceed from Budgeted Qty (" & mBudgetQty & ") for Item Code : " & pItemCode & " , So Cann't be Saved.")
                        BudgetValidation = False
                        Exit Function
                    End If
                End If
            Next
        End With

        BudgetValidation = True

        Exit Function
ErrPart:
        BudgetValidation = False
    End Function

    Private Function CheckReqAgtPlann(ByRef pItemCode As String, ByRef pProductionNo As Double, ByRef pPlanQty As Double, ByRef pNewDemandedQty As Double) As Boolean

        On Error GoTo err_Renamed
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mAlreadyDemandQty As Double
        Dim mSlipNo As String = ""
        Dim mField As String
        CheckReqAgtPlann = False

        If lblBookType.Text = "R" Then
            mSqlStr = "SELECT IH.AUTO_KEY_ISS, SUM(CASE WHEN IH.ISSUE_STATUS='N' THEN DEMAND_QTY ELSE ISSUE_QTY END) AS DEMAND_QTY" & vbCrLf & " FROM INV_ISSUE_HDR IH, INV_ISSUE_DET ID" & vbCrLf & " WHERE IH.AUTO_KEY_ISS=ID.AUTO_KEY_ISS" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.ISSUE_DATE=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.DEPT_CODE='" & Trim(txtDept.Text) & "'" & vbCrLf & " AND ID.ITEM_CODE='" & Trim(pItemCode) & "'"

            If Val(CStr(pProductionNo)) > 0 Then
                mSqlStr = mSqlStr & vbCrLf & " AND IH.DAILY_PLAN_NO=" & pProductionNo & ""
            End If

            If Val(txtReqNo.Text) <> 0 Then
                mSqlStr = mSqlStr & vbCrLf & " AND IH.AUTO_KEY_ISS<>" & Val(txtReqNo.Text) & ""
            End If

            mSqlStr = mSqlStr & vbCrLf & " GROUP BY IH.AUTO_KEY_ISS"


        Else
            mSqlStr = "SELECT IH.AUTO_KEY_ISS, SUM(ISSUE_QTY) AS DEMAND_QTY" & vbCrLf & " FROM INV_ISSUE_HDR IH, INV_ISSUE_DET ID" & vbCrLf & " WHERE IH.AUTO_KEY_ISS=ID.AUTO_KEY_ISS" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.ISSUE_DATE=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.DEPT_CODE='" & Trim(txtDept.Text) & "'" & vbCrLf & " AND ID.ITEM_CODE='" & Trim(pItemCode) & "'"

            If Val(CStr(pProductionNo)) > 0 Then
                mSqlStr = mSqlStr & vbCrLf & " AND IH.DAILY_PLAN_NO=" & pProductionNo & ""
            End If

            If Val(txtReqNo.Text) <> 0 Then
                mSqlStr = mSqlStr & vbCrLf & " AND IH.AUTO_KEY_ISS<>" & Val(txtReqNo.Text) & ""
            End If

            mSqlStr = mSqlStr & vbCrLf & " GROUP BY IH.AUTO_KEY_ISS"
        End If
        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        mAlreadyDemandQty = 0
        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                If mSlipNo = "" Then
                    mSlipNo = IIf(IsDbNull(RsTemp.Fields("AUTO_KEY_ISS").Value), 0, RsTemp.Fields("AUTO_KEY_ISS").Value)
                Else
                    mSlipNo = mSlipNo & ", " & IIf(IsDbNull(RsTemp.Fields("AUTO_KEY_ISS").Value), 0, RsTemp.Fields("AUTO_KEY_ISS").Value)
                End If
                mAlreadyDemandQty = mAlreadyDemandQty + IIf(IsDbNull(RsTemp.Fields("DEMAND_QTY").Value), 0, RsTemp.Fields("DEMAND_QTY").Value)
                RsTemp.MoveNext()
            Loop
        End If


        If pPlanQty < (mAlreadyDemandQty + pNewDemandedQty) And mAlreadyDemandQty > 0 Then
            MsgInformation(" Already made Requisition of Qty : " & mAlreadyDemandQty & " of Max Demand Qty " & pPlanQty & " for Item Code : " & pItemCode & vbNewLine & " Slip No " & mSlipNo & " against such Production Plan , so cann't be Saved")
            CheckReqAgtPlann = True
            Exit Function

        Else
            If pPlanQty < pNewDemandedQty And pNewDemandedQty > 0 Then
                MsgInformation(" Requisition Qty : " & pNewDemandedQty & " for Item Code : " & pItemCode & " is greater than Plan Qty, so cann't be Saved")
                CheckReqAgtPlann = True
                Exit Function
            End If
        End If
        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function

    Private Function GetToDayDemanded(ByRef pItemCode As String) As Double

        On Error GoTo err_Renamed
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mField As String

        GetToDayDemanded = 0

        '    If lblBookType.text = "R" Then
        '        mField = "DEMAND_QTY"
        '    Else
        '        mField = "ISSUE_QTY"
        '    End If
        ''SUM(" & mField & ")

        mSqlStr = "SELECT SUM(CASE WHEN IH.ISSUE_STATUS='N' THEN DEMAND_QTY ELSE ISSUE_QTY END) AS DEMAND_QTY" & vbCrLf & " FROM INV_ISSUE_HDR IH, INV_ISSUE_DET ID" & vbCrLf & " WHERE IH.AUTO_KEY_ISS=ID.AUTO_KEY_ISS" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.ISSUE_DATE=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.DEPT_CODE='" & Trim(txtDept.Text) & "'" & vbCrLf & " AND ID.ITEM_CODE='" & Trim(pItemCode) & "'"

        If Val(txtReqNo.Text) <> 0 Then
            mSqlStr = mSqlStr & vbCrLf & " AND IH.AUTO_KEY_ISS<>" & Val(txtReqNo.Text) & ""
        End If

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetToDayDemanded = IIf(IsDbNull(RsTemp.Fields("DEMAND_QTY").Value), 0, RsTemp.Fields("DEMAND_QTY").Value)
        End If

        Exit Function
err_Renamed:
        GetToDayDemanded = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function
    Private Function GetToDayIssue(ByRef pItemCode As String) As Double

        On Error GoTo err_Renamed
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing

        GetToDayIssue = 0

        mSqlStr = "SELECT SUM(ISSUE_QTY) AS ISSUE_QTY" & vbCrLf & " FROM INV_ISSUE_HDR IH, INV_ISSUE_DET ID" & vbCrLf & " WHERE IH.AUTO_KEY_ISS=ID.AUTO_KEY_ISS" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.ISSUE_DATE=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.DEPT_CODE='" & Trim(txtDept.Text) & "'" & vbCrLf & " AND ID.ITEM_CODE='" & Trim(pItemCode) & "'"

        If Val(txtReqNo.Text) <> 0 Then
            mSqlStr = mSqlStr & vbCrLf & " AND IH.AUTO_KEY_ISS<>" & Val(txtReqNo.Text) & ""
        End If

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetToDayIssue = IIf(IsDbNull(RsTemp.Fields("ISSUE_QTY").Value), 0, RsTemp.Fields("ISSUE_QTY").Value)
        End If

        mSqlStr = "SELECT SUM(RTN_QTY) AS RTN_QTY" & vbCrLf & " FROM INV_SRN_HDR IH, INV_SRN_DET ID" & vbCrLf & " WHERE IH.AUTO_KEY_SRN=ID.AUTO_KEY_SRN" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.SRN_DATE=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.DEPT_CODE='" & Trim(txtDept.Text) & "'" & vbCrLf & " AND ID.ITEM_CODE='" & Trim(pItemCode) & "' AND STATUS='Y'"

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetToDayIssue = GetToDayIssue - IIf(IsDbNull(RsTemp.Fields("RTN_QTY").Value), 0, RsTemp.Fields("RTN_QTY").Value)
        End If

        Exit Function
err_Renamed:
        GetToDayIssue = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function
    Private Function GetLastEntryDate() As String

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        GetLastEntryDate = ""
        SqlStr = ""
        SqlStr = "SELECT Max(ISSUE_DATE) AS  ISSUE_DATE " & vbCrLf & " FROM INV_ISSUE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_ISS,LENGTH(AUTO_KEY_ISS)-5,4)=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "' AND ISSUE_STATUS='Y'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            GetLastEntryDate = IIf(IsDbNull(RsTemp.Fields("ISSUE_DATE").Value), "", RsTemp.Fields("ISSUE_DATE").Value)
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
            mQty = IIf(IsDbNull(RsTemp.Fields("ISSUE_QTY").Value), 0, RsTemp.Fields("ISSUE_QTY").Value)
        End If

        If mQty > 0 Then
            CheckMaterialIssue = True
        End If

        Exit Function
ErrPart:
        CheckMaterialIssue = False
    End Function
    Private Function CheckPendingReqSlip() As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        CheckPendingReqSlip = 0
        SqlStr = ""


        SqlStr = "SELECT COUNT(1) AS CNTREQ " & vbCrLf & " FROM INV_ISSUE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND DEPT_CODE='" & MainClass.AllowSingleQuote((txtDept.Text)) & "'" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote((txtEmp.Text)) & "'" & vbCrLf & " AND ISSUE_STATUS='N'" & vbCrLf & " AND ISSUE_DATE=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        If Val(txtReqNo.Text) > 0 Then
            SqlStr = SqlStr & vbCrLf & " AND AUTO_KEY_ISSUE<>" & Val(txtReqNo.Text) & ""
        End If


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            CheckPendingReqSlip = IIf(IsDbNull(RsTemp.Fields("CNTREQ").Value), 0, RsTemp.Fields("CNTREQ").Value)
        End If

        Exit Function
ErrPart:
        CheckPendingReqSlip = 0
    End Function
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Public Sub FrmStoreReqGST_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If lblBookType.Text = "I" Then
            Me.Text = "Material Issue Note"
        Else
            Me.Text = "Store Requisition Note"
        End If

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

        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & vbCrLf & " AND SUBSTR(AUTO_KEY_ISS,LENGTH(AUTO_KEY_ISS)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

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
            .TypeEditLen = MainClass.SetMaxLength("ITEM_UOM", "INV_ISSUE_DET", PubDBCn)
            .set_ColWidth(ColUom, 4)

            .Col = ColStockType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = MainClass.SetMaxLength("FROM_STOCK_TYPE", "INV_ISSUE_DET", PubDBCn)
            .set_ColWidth(ColStockType, 4.5)

            .Col = ColLotNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            '        .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("LOT_NO", "INV_ISSUE_DET", PubDBCn)
            .set_ColWidth(ColLotNo, 6)

            .Col = ColStockQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColStockQty, 8)

            .Col = ColDeptQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColDeptQty, 7)
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
            .TypeEditLen = MainClass.SetMaxLength("REMARKS", "INV_ISSUE_DET", PubDBCn)
            If lblBookType.Text = "R" Then
                .set_ColWidth(ColRemarks, 11)
            Else
                .set_ColWidth(ColRemarks, 6)
            End If

        End With
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemDesc, ColUom)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColStockQty, colStdQty)
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
            txtReqNo.Maxlength = .Fields("AUTO_KEY_ISS").Precision
            txtDept.Maxlength = .Fields("DEPT_CODE").DefinedSize
            txtEmp.Maxlength = .Fields("EMP_CODE").DefinedSize
            txtCost.Maxlength = .Fields("COST_CENTER_CODE").DefinedSize
            txtsubdept.Maxlength = .Fields("REMARKS").DefinedSize
            txtprod.Maxlength = .Fields("DAILY_PLAN_NO").Precision
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

        With RsReqMain
            If Not .EOF Then
                txtReqNo.Enabled = False
                lblMKey.Text = .Fields("AUTO_KEY_ISS").Value


                txtReqNo.Text = IIf(IsDbNull(.Fields("AUTO_KEY_ISS").Value), 0, .Fields("AUTO_KEY_ISS").Value)
                txtReqDate.Text = VB6.Format(IIf(IsDbNull(.Fields("ISSUE_DATE").Value), "", .Fields("ISSUE_DATE").Value), "DD/MM/YYYY")
                txtEntryDate.Text = VB6.Format(IIf(IsDbNull(.Fields("ADDDATE").Value), "", .Fields("ADDDATE").Value), "DD/MM/YYYY HH:MM")
                txtDept.Text = IIf(IsDbNull(.Fields("DEPT_CODE").Value), "", .Fields("DEPT_CODE").Value)
                txtEmp.Text = IIf(IsDbNull(.Fields("EMP_CODE").Value), "", .Fields("EMP_CODE").Value)
                txtCost.Text = IIf(IsDbNull(.Fields("COST_CENTER_CODE").Value), "", .Fields("COST_CENTER_CODE").Value)
                txtsubdept.Text = IIf(IsDbNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)
                txtprod.Text = IIf(IsDbNull(.Fields("DAILY_PLAN_NO").Value), "", .Fields("DAILY_PLAN_NO").Value)
                chkIssue.CheckState = IIf(.Fields("ISSUE_STATUS").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                chkIssue.Enabled = IIf(.Fields("ISSUE_STATUS").Value = "Y", False, True)

                cboShiftcd.Text = IIf(IsDbNull(.Fields("SHIFT_CODE").Value), "", .Fields("SHIFT_CODE").Value)

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

                mDivisionCode = IIf(IsDbNull(.Fields("DIV_CODE").Value), "", .Fields("DIV_CODE").Value)
                If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivisionDesc = Trim(MasterNo)
                    cboDivision.Text = mDivisionDesc
                End If

                If lblBookType.Text = "I" Then
                    txtDept.Enabled = False
                    txtEmp.Enabled = False
                    txtCost.Enabled = False
                    cmdSearchEmp.Enabled = False
                    cmdSearchDept.Enabled = False
                    cmdSearchCC.Enabled = False
                End If

                cboDivision.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False)

                lblProductCode.Text = Trim(IIf(IsDbNull(.Fields("INHOUSE_CODE").Value), "", .Fields("INHOUSE_CODE").Value))

                If chkIssue.CheckState = System.Windows.Forms.CheckState.Checked Then
                    cmdUpdateIssue.Enabled = False
                End If

                If ShowProdPlan(Val(txtprod.Text)) = False Then GoTo ERR1
                Call ShowDetail1(.Fields("AUTO_KEY_ISS").Value, mDivisionCode)
                txtprod.Enabled = False
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
        'Resume
    End Sub
    Private Sub ShowDetail1(ByRef pReqNum As Double, ByRef mDivisionCode As Double)

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
        Dim mProdType As String
        Dim mCommonDivision As Double
        Dim mStockQty As Double
        Dim mAutoQCIssue As String

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM INV_ISSUE_DET  " & vbCrLf & " Where AUTO_KEY_ISS = " & Val(CStr(pReqNum)) & "" & vbCrLf & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsReqDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsReqDetail
            If .EOF = True Then Exit Sub
            '        FormatSprdMain -1
            i = 1
            .MoveFirst()

            Do While Not .EOF

                SprdMain.Row = i

                SprdMain.Col = ColItemCode
                mItemCode = IIf(IsDbNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)
                SprdMain.Text = Trim(mItemCode)

                If MainClass.ValidateWithMasterTable(mItemCode, "AUTO_INDENT", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AUTO_INDENT='Y'") = True Then
                    mAutoQCIssue = "Y"
                Else
                    mAutoQCIssue = "N"
                End If


                SprdMain.Col = ColItemDesc
                MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mItemDesc = MasterNo
                SprdMain.Text = mItemDesc

                SprdMain.Col = ColUom
                mItemUOM = IIf(IsDbNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)
                SprdMain.Text = mItemUOM

                SprdMain.Col = ColStockType
                mStkType = IIf(IsDbNull(.Fields("FROM_STOCK_TYPE").Value), "", .Fields("FROM_STOCK_TYPE").Value)
                SprdMain.Text = mStkType

                mIssueQty = IIf(IsDbNull(.Fields("ISSUE_QTY").Value), 0, .Fields("ISSUE_QTY").Value)

                '            If Left(cboShiftcd.Text, 1) = "C" Then
                '                mDate = DateAdd("d", 1, txtReqDate.Text)
                '            Else
                mDate = txtReqDate.Text
                '            End If

                SprdMain.Col = ColLotNo
                SprdMain.Text = VB6.Format(IIf(IsDbNull(.Fields("LOT_NO").Value) Or .Fields("LOT_NO").Value <= 0, "", .Fields("LOT_NO").Value))
                mLotNo = IIf(IsDbNull(.Fields("LOT_NO").Value), "", .Fields("LOT_NO").Value)

                SprdMain.Col = ColStockQty
                mCommonDivision = GetCommonDivCode
                mStockQty = GetBalanceStockQty(mItemCode, mDate, mItemUOM, "STR", mStkType, mLotNo, ConWH, mDivisionCode, ConStockRefType_ISS, Val(txtReqNo.Text))
                If RsCompany.Fields("COMPANY_CODE").Value = 1 And mAutoQCIssue = "N" Then
                    mStockQty = mStockQty + GetBalanceStockQty(mItemCode, mDate, mItemUOM, "STR", "QC", mLotNo, ConWH, mDivisionCode, ConStockRefType_ISS, Val(txtReqNo.Text))
                End If
                If mDivisionCode <> mCommonDivision Then
                    If mCommonDivision > 0 Then
                        mStockQty = mStockQty + GetBalanceStockQty(mItemCode, mDate, mItemUOM, "STR", mStkType, mLotNo, ConWH, mCommonDivision, ConStockRefType_ISS, Val(txtReqNo.Text))
                    End If
                End If
                SprdMain.Text = CStr(mStockQty)

                SprdMain.Col = ColDeptQty
                mWIPStock = CStr(GetBalanceStockQty(mItemCode, mDate, mItemUOM, (txtDept.Text), "ST", mLotNo, ConPH, mDivisionCode, ConStockRefType_ISS, Val(txtReqNo.Text)))
                mWIPStock = CStr(CDbl(mWIPStock) + GetProductionStock(mItemCode, (txtDept.Text), mDivisionCode, mDate, mItemUOM)) ''GetDeptStock(mItemCode, mDivisionCode)
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
                mDemandQty = IIf(IsDbNull(.Fields("DEMAND_QTY").Value), 0, .Fields("DEMAND_QTY").Value)
                SprdMain.Text = mDemandQty

                SprdMain.Col = ColIssueQty
                SprdMain.Text = mIssueQty

                SprdMain.Col = ColIssuedQty
                SprdMain.Text = mIssueQty

                SprdMain.Col = ColBalQty
                SprdMain.Text = CStr(Val(CStr(CDbl(mDemandQty) - CDbl(mIssueQty))))

                SprdMain.Col = ColRemarks
                mRemarks = IIf(IsDbNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)
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


    Private Function GetDeptStock(ByRef mItemCode As String, ByRef mDivisionCode As Double) As Double

        On Error GoTo ERR1
        Dim i As Integer
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mProductCode As String = ""
        Dim mItemUOM As String = ""
        Dim mStdQty As String

        GetDeptStock = 0
        SqlStr = ""
        If Trim(lblProductCode.Text) = "" Then
            GetDeptStock = 0
            Exit Function
        End If
        '    SqlStr = " SELECT DISTINCT IH.PRODUCT_CODE " & vbCrLf _
        ''            & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID " & vbCrLf _
        ''            & " Where IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND IH.MKEY=ID.MKEY" & vbCrLf _
        ''            & " AND ID.RM_CODE='" & mItemCode & "' AND IH.STATUS='O'"

        SqlStr = " SELECT " & vbCrLf & " TRN.PRODUCT_CODE, TRN.RM_CODE, TRN.STD_QTY, DEPT_CODE, GROSS_WT_SCRAP" & vbCrLf & " FROM VW_PRD_BOM_TRN TRN" & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.PRODUCT_CODE='" & MainClass.AllowSingleQuote((lblProductCode.Text)) & "'" & vbCrLf & " AND TRN.DEPT_CODE='" & MainClass.AllowSingleQuote((txtDept.Text)) & "'" & vbCrLf & " AND TRN.RM_CODE='" & mItemCode & "'"

        '    SqlStr = SqlStr & vbCrLf _
        ''            & " START WITH  TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(lblProductCode.text) & "'" & vbCrLf _
        ''            & " CONNECT BY PRIOR (TRIM(RM_CODE) || COMPANY_CODE || ' ')=TRIM(PRODUCT_CODE) || COMPANY_CODE || ' '"

        ''TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND PRIOR RM_CODE=PRODUCT_CODE
        ''" CONNECT BY PRIOR (TRIM(RM_CODE) || COMPANY_CODE || ' ')=TRIM(PRODUCT_CODE) || COMPANY_CODE || ' '"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mProductCode = Trim(IIf(IsDbNull(RsTemp.Fields("PRODUCT_CODE").Value), "", RsTemp.Fields("PRODUCT_CODE").Value))
                mStdQty = CStr(Val(IIf(IsDbNull(RsTemp.Fields("STD_QTY").Value), 0, RsTemp.Fields("STD_QTY").Value)) + Val(IIf(IsDbNull(RsTemp.Fields("GROSS_WT_SCRAP").Value), 0, RsTemp.Fields("GROSS_WT_SCRAP").Value)))

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

        '    If Trim(lblProductCode.text) <> "" Then
        '        SqlStr = SqlStr & vbCrLf & " AND REF_ITEM_CODE='" & MainClass.AllowSingleQuote(lblProductCode.text) & "'"
        '    End If

        SqlStr = SqlStr & vbCrLf & " AND E_DATE=TO_DATE('" & VB6.Format(pDate, "dd-mmm-yyyy") & "')"

        '    SqlStr = SqlStr & vbCrLf & " AND REF_DATE=TO_DATE('" & VB6.Format((pDateTo), "DD-MMM-YYYY") & "')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBalStock, ADODB.LockTypeEnum.adLockReadOnly)

        If RsBalStock.EOF = False Then
            If IsDbNull(RsBalStock.Fields(0).Value) Then
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
                mIssueUOM = IIf(IsDbNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)
                mPurchaseUOM = IIf(IsDbNull(RsTemp.Fields("PURCHASE_UOM").Value), "", RsTemp.Fields("PURCHASE_UOM").Value)
                mFactor = IIf(IsDbNull(RsTemp.Fields("UOM_FACTOR").Value) Or RsTemp.Fields("UOM_FACTOR").Value = 0, 1, RsTemp.Fields("UOM_FACTOR").Value)

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
        MainClass.ButtonStatus(Me, XRIGHT, RsReqMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub Clear1()


        lblMKey.Text = ""

        txtReqDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtReqNo.Text = ""
        txtDept.Text = ""
        lblInHouseStockQty.Text = "0.00"
        lblProductDesc.Text = ""
        lblDemandQty.Text = "0.00"

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

        txtprod.Text = ""
        lblCostctr.Text = ""
        lblDeptname.Text = ""

        lblPlanningQty.Text = ""
        lblProductCode.Text = ""
        txtEntryDate.Text = VB6.Format(PubCurrDate, "DD/MM/YYYY") & " " & GetServerTime
        chkIssue.CheckState = System.Windows.Forms.CheckState.Unchecked

        txtReqDate.Enabled = IIf(PubUserID = "G0416", True, False) '' IIf(PubSuperUser = "S", True, False)

        txtDept.Enabled = True

        txtCost.Enabled = True

        cmdSearchDept.Enabled = True
        cmdSearchCC.Enabled = True
        cboStockFor.SelectedIndex = -1
        txtprod.Enabled = IIf(lblBookType.Text = "R", True, False)
        chkIssue.Enabled = IIf(lblBookType.Text = "I", True, False)
        cboShiftcd.Enabled = IIf(lblBookType.Text = "R", True, False)

        pDataShow = False
        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsReqMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub FrmStoreReqGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
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
    Public Sub FrmStoreReqGST_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        With SprdMain
            SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With

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
            lblCostctr.Text = IIf(IsDbNull(RsTemp.Fields("CC_DESC").Value), "", RsTemp.Fields("CC_DESC").Value)
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

    Private Sub txtprod_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtprod.DoubleClick
        Call SearchProductionPlan()
    End Sub

    Private Sub txtprod_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtprod.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchProductionPlan()
    End Sub

    Private Sub txtprod_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtprod.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

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
        Dim xItemUOM As String = ""
        Dim mDemandQty As Double

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        '    If pDataShow = True Then Exit Sub

        xAutoIssue = CheckAutoIssue((txtReqDate.Text), "")


        '    mProd_Type = IsProductionItem(pItemCode)

        If xAutoIssue = True Then
            GoTo EventExitSub
        End If

        If Val(txtprod.Text) = 0 Then GoTo EventExitSub
        If Trim(txtDept.Text) = "" Then GoTo EventExitSub
        If Trim(txtReqDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtReqDate.Text) Then GoTo EventExitSub
        If VB.Left(cboStockFor.Text, 1) <> "P" Then GoTo EventExitSub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        MainClass.ClearGrid(SprdMain)

        '    mIsChild = False
        'AgainCheck:
        ''& " AND IH.AUTO_KEY_PRODPLAN =" & Val(txtprod.Text) & " " & vbCrLf

        SqlStr = " SELECT " & vbCrLf & " IH.INHOUSE_CODE,SUM(DPLAN_QTY) AS DPLAN_QTY " & vbCrLf & " FROM PRD_PRODPLAN_MONTH_DET IH" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.SERIAL_DATE=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        SqlStr = SqlStr & vbCrLf & " AND IH.DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'"

        SqlStr = SqlStr & vbCrLf & " AND IH.INHOUSE_CODE='" & MainClass.AllowSingleQuote(lblProductCode.Text) & "'"
        SqlStr = SqlStr & vbCrLf & " GROUP BY IH.INHOUSE_CODE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        cntRow = 1
        If RsTemp.EOF = False Then
            mProductCode = Trim(IIf(IsDbNull(RsTemp.Fields("INHOUSE_CODE").Value), "", RsTemp.Fields("INHOUSE_CODE").Value))
            mProductPlanQty = Val(IIf(IsDbNull(RsTemp.Fields("DPLAN_QTY").Value), 0, RsTemp.Fields("DPLAN_QTY").Value))
            If MainClass.ValidateWithMasterTable(Trim(mProductCode), "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblProductDesc.Text = Trim(MasterNo)
            End If

            If MainClass.ValidateWithMasterTable(Trim(mProductCode), "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                xItemUOM = Trim(MasterNo)
            End If

            lblProductCode.Text = Trim(mProductCode)
            lblPlanningQty.Text = VB6.Format(mProductPlanQty, "0.0000")
            mStockQty = GetBalanceStockQty(mProductCode, (txtReqDate.Text), xItemUOM, (txtDept.Text), "", "", ConPH, mDivisionCode)
            mStockQty = mStockQty - GetWIPLockQty(mProductCode, (txtDept.Text), (txtReqDate.Text))
            mStockQty = IIf(mStockQty < 0, 0, mStockQty)
            lblInHouseStockQty.Text = VB6.Format(mStockQty, "0.000")
            mDemandQty = (Val(lblPlanningQty.Text) * 1.5) - mStockQty
            mDemandQty = CDbl(VB6.Format(IIf(mDemandQty < 0, 0, IIf(mDemandQty > mProductPlanQty, mProductPlanQty, mDemandQty)), "0.000"))
            mDemandQty = System.Math.Round(mDemandQty, 0)
            lblDemandQty.Text = VB6.Format(mDemandQty, "0.000")

            Call ShowBOM(mProductCode, mDemandQty, mDivisionCode)
        Else
            '        If mIsChild = False Then
            '            mIsChild = True
            '            GoTo AgainCheck
            '        End If
        End If

        FormatSprdMain(-1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        pDataShow = True
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Function ShowProdPlan(ByRef pProdPlannNo As Double) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mProductCode As String = ""
        Dim mProductPlanQty As Double
        Dim mStockQty As Double
        Dim xItemUOM As String = ""

        Dim mDivisionCode As Double
        Dim mDemandQty As Double


        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        ''IH.AUTO_KEY_PRODPLAN =" & Val(txtprod.Text) & "

        SqlStr = " SELECT " & vbCrLf & " IH.INHOUSE_CODE,  SUM(DPLAN_QTY) AS DPLAN_QTY " & vbCrLf & " FROM PRD_PRODPLAN_MONTH_DET IH" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.SERIAL_DATE=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.DEPT_CODE='" & MainClass.AllowSingleQuote((txtDept.Text)) & "'"

        '    SqlStr = SqlStr & vbCrLf & " "

        '    If Trim(lblProductCode.text) <> "" Then
        SqlStr = SqlStr & vbCrLf & " AND IH.INHOUSE_CODE='" & MainClass.AllowSingleQuote(lblProductCode.Text) & "'"
        '    End If

        SqlStr = SqlStr & vbCrLf & " GROUP BY IH.INHOUSE_CODE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mProductCode = Trim(IIf(IsDbNull(RsTemp.Fields("INHOUSE_CODE").Value), "", RsTemp.Fields("INHOUSE_CODE").Value))
            mProductPlanQty = Val(IIf(IsDbNull(RsTemp.Fields("DPLAN_QTY").Value), 0, RsTemp.Fields("DPLAN_QTY").Value))
            lblProductCode.Text = Trim(mProductCode)
            lblPlanningQty.Text = VB6.Format(mProductPlanQty, "0.0000")
            If MainClass.ValidateWithMasterTable(Trim(mProductCode), "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblProductDesc.Text = Trim(MasterNo)
            End If
            If MainClass.ValidateWithMasterTable(Trim(mProductCode), "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                xItemUOM = Trim(MasterNo)
            End If

            mStockQty = GetBalanceStockQty(mProductCode, (txtReqDate.Text), xItemUOM, (txtDept.Text), "", "", ConPH, mDivisionCode)
            lblInHouseStockQty.Text = VB6.Format(mStockQty, "0.000")
            mDemandQty = (Val(lblPlanningQty.Text) * 1.5) - mStockQty
            lblDemandQty.Text = VB6.Format(IIf(mDemandQty < 0, 0, lblDemandQty.Text), "0.000")
        End If
        ShowProdPlan = True
        Exit Function
ErrPart:
        ShowProdPlan = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function GetSTDQty(ByRef pItemCode As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mRMCode As String
        Dim mDept As String

        '    Call InsertTempTable(pItemCode)

        GetSTDQty = 0
        If Trim(lblProductCode.Text) = "" Then Exit Function

        '    SqlStr = " SELECT DISTINCT" & vbCrLf _
        ''            & " TRN.FG_CODE, TRN.DEPT_CODE, TRN.STD_QTY" & vbCrLf _
        ''            & " FROM TEMP_DESPVSISSUE TRN" & vbCrLf _
        ''            & " WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf _
        ''            & " AND CHILD_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "' AND "

        SqlStr = " SELECT " & vbCrLf & " TRN.PRODUCT_CODE, TRN.RM_CODE, TRN.STD_QTY, DEPT_CODE, GROSS_WT_SCRAP" & vbCrLf & " FROM VW_PRD_BOM_TRN TRN" & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND TRN.PRODUCT_CODE='" & MainClass.AllowSingleQuote((lblProductCode.Text)) & "'" & vbCrLf & " AND TRN.DEPT_CODE='" & MainClass.AllowSingleQuote((txtDept.Text)) & "'" & vbCrLf & " AND TRN.RM_CODE='" & pItemCode & "'"

        '    SqlStr = SqlStr & vbCrLf _
        ''            & " START WITH  TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(lblProductCode.text) & "'" & vbCrLf _
        ''            & " CONNECT BY PRIOR (TRIM(RM_CODE) || COMPANY_CODE || ' ')=TRIM(PRODUCT_CODE) || COMPANY_CODE || ' '"

        ''TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND PRIOR RM_CODE=PRODUCT_CODE
        ''PRIOR (TRIM(RM_CODE) || COMPANY_CODE || ' ')=TRIM(PRODUCT_CODE) || COMPANY_CODE || ' '
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mDept = IIf(IsDbNull(RsTemp.Fields("DEPT_CODE").Value), "", RsTemp.Fields("DEPT_CODE").Value)
                If mDept = Trim(txtDept.Text) Then
                    GetSTDQty = IIf(IsDbNull(RsTemp.Fields("STD_QTY").Value), "", RsTemp.Fields("STD_QTY").Value)
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
                    xItemCode = IIf(IsDbNull(RsRM.Fields("FG_CODE").Value), "", RsRM.Fields("FG_CODE").Value)
                    xSTDQty = IIf(IsDbNull(RsRM.Fields("STD_QTY").Value), 0, RsRM.Fields("STD_QTY").Value)

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
        Dim i As Integer
        Dim pWEF As String

        SqlStr = " SELECT DISTINCT IH.PRODUCT_CODE, WEF " & vbCrLf & " FROM PRD_NEWBOM_HDR IH, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND IH.PRODUCT_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND INVMST.ITEM_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'"

        SqlStr = SqlStr & vbCrLf & " AND IH.STATUS='O'"

        SqlStr = SqlStr & vbCrLf & " ORDER BY IH.PRODUCT_CODE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMain, ADODB.LockTypeEnum.adLockReadOnly)
        mcntRow = 0

        If RsMain.EOF = False Then
            Do While Not RsMain.EOF
                pWEF = Trim(IIf(IsDbNull(RsMain.Fields("WEF").Value), "", RsMain.Fields("WEF").Value))

                SqlStr = ""
                SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, " & vbCrLf & " ID.RM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " ID.DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf & " ID.STD_QTY, ID.GROSS_WT_SCRAP, " & vbCrLf & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM, MINIMUM_QTY, MAXIMUM_QTY "

                SqlStr = SqlStr & vbCrLf & " FROM PRD_NEWBOM_HDR IH,PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.RM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

                SqlStr = SqlStr & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "' " & vbCrLf & " AND IH.WEF=TO_DATE('" & VB6.Format(pWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                SqlStr = SqlStr & vbCrLf & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'"

                SqlStr = SqlStr & vbCrLf & " ORDER BY ID.PRODUCT_CODE, ID.SUBROWNO"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

                i = 0

                If Not RsShow.EOF Then
                    Do While Not RsShow.EOF
                        Call FillGridCol(RsShow, mProductCode, mProductCode, mProductPlanQty, mDivisionCode)
                        RsShow.MoveNext()

                    Loop
                End If
                RsMain.MoveNext()
            Loop
        End If
        RsShow = Nothing
        Exit Sub
LedgError:
        ''    Resume
        MsgInformation(Err.Description)
    End Sub

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

        With SprdMain

            mDeptCode = IIf(IsDbNull(pRs.Fields("DEPT_CODE").Value), "", pRs.Fields("DEPT_CODE").Value)
            mRMCode = Trim(IIf(IsDbNull(pRs.Fields("RM_CODE").Value), "", pRs.Fields("RM_CODE").Value))
            mItemUOM = IIf(IsDbNull(pRs.Fields("ISSUE_UOM").Value), "", pRs.Fields("ISSUE_UOM").Value)
            xAutoIssue = CheckAutoIssue((txtReqDate.Text), mRMCode)

            If MainClass.ValidateWithMasterTable(mRMCode, "AUTO_INDENT", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AUTO_INDENT='Y'") = True Then
                mAutoQCIssue = "Y"
            Else
                mAutoQCIssue = "N"
            End If


            If mDeptCode = Trim(txtDept.Text) Then
                mProd_Type = IsProductionItem(mRMCode)
                If xAutoIssue = True Then
                    If mProd_Type = True Then
                        GoTo NextRecd
                    End If
                End If
                pRow = 0
                If GetItemCodeAlreadyExists(IIf(IsDbNull(pRs.Fields("RM_CODE").Value), "", pRs.Fields("RM_CODE").Value), pRow) = True Then
                    .Row = pRow
                    GoTo NextRec
                Else
                    mcntRow = mcntRow + 1
                    .Row = mcntRow
                End If
                .Col = ColItemCode
                .Text = IIf(IsDbNull(pRs.Fields("RM_CODE").Value), "", pRs.Fields("RM_CODE").Value)


                .Col = ColItemDesc
                .Text = IIf(IsDbNull(pRs.Fields("ITEM_SHORT_DESC").Value), "", pRs.Fields("ITEM_SHORT_DESC").Value)

                .Col = ColUom
                .Text = IIf(IsDbNull(pRs.Fields("ISSUE_UOM").Value), "", pRs.Fields("ISSUE_UOM").Value)


                .Col = ColStockType
                .Text = "ST"

                .Col = ColStockQty
                mCommonDivision = GetCommonDivCode
                mStockQty = GetBalanceStockQty(mRMCode, (txtReqDate.Text), mItemUOM, "STR", "ST", "", ConWH, mDivisionCode, ConStockRefType_ISS, Val(txtReqNo.Text))
                If RsCompany.Fields("COMPANY_CODE").Value = 1 And mAutoQCIssue = "N" Then
                    mStockQty = mStockQty + GetBalanceStockQty(mRMCode, (txtReqDate.Text), mItemUOM, "STR", "QC", "", ConWH, mDivisionCode, ConStockRefType_ISS, Val(txtReqNo.Text))
                End If
                If mDivisionCode <> mCommonDivision Then
                    If mCommonDivision > 0 Then
                        mStockQty = mStockQty + GetBalanceStockQty(mRMCode, (txtReqDate.Text), mItemUOM, "STR", "ST", "", ConWH, mCommonDivision, ConStockRefType_ISS, Val(txtReqNo.Text))
                    End If
                End If
                .Text = CStr(mStockQty)

                .Col = ColDeptQty
                mWIPStock = GetBalanceStockQty(mRMCode, (txtReqDate.Text), mItemUOM, (txtDept.Text), "ST", "", ConPH, mDivisionCode, ConStockRefType_ISS, Val(txtReqNo.Text))
                mWIPStock = mWIPStock + GetProductionStock(mRMCode, (txtDept.Text), mDivisionCode, (txtReqDate.Text), mItemUOM) ''GetDeptStock(mRMCode, mDivisionCode)
                .Text = VB6.Format(mWIPStock, "0.0000")

NextRec:
                SprdMain.Col = colStdQty
                .Text = CStr(Val(.Text) + CDbl(VB6.Format(Val(IIf(IsDbNull(pRs.Fields("STD_QTY").Value), "", pRs.Fields("STD_QTY").Value)), "0.0000")))

                .Col = ColDemandQty
                .Text = CStr(Val(.Text) + CDbl(VB6.Format(mProductPlanQty * Val(IIf(IsDbNull(pRs.Fields("STD_QTY").Value), "", pRs.Fields("STD_QTY").Value)), "0.00")))

                .Col = ColIssueQty
                .Text = "0.00"

                .Col = ColIssuedQty
                .Text = "0.00"

                .Col = ColBalQty
                .Text = "0.00"

                .Col = ColRemarks
                .Text = ""

                .MaxRows = .MaxRows + 1
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
                mRMCode = Trim(IIf(IsDbNull(RsShow.Fields("RM_CODE").Value), "", RsShow.Fields("RM_CODE").Value))
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
                mRMCode = Trim(IIf(IsDbNull(RsShow.Fields("RM_CODE").Value), "", RsShow.Fields("RM_CODE").Value))
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
                    mRMCode = Trim(IIf(IsDbNull(RsShow.Fields("RM_CODE").Value), "", RsShow.Fields("RM_CODE").Value))
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then TxtDept_DoubleClick(TxtDept, New System.EventArgs())
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

    Private Sub txtprod_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtprod.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtprod_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtprod.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
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

        If MODIFYMode = True And RsReqMain.EOF = False Then mReqnum = RsReqMain.Fields("AUTO_KEY_ISS").Value

        SqlStr = "Select * From INV_ISSUE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND LTRIM(RTRIM(AUTO_KEY_ISS))=" & Val(txtReqNo.Text) & ""

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
                SqlStr = "Select * From INV_ISSUE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " LTRIM(RTRIM(AUTO_KEY_ISS))=" & Val(mReqnum) & ""

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsReqMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
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
        Dim i As Integer

        mSearchItem = Trim(txtSearchItem.Text)
        Dim counter As Short
        With SprdMain
            counter = mSearchStartRow
            For i = counter To .MaxRows
                .Row = i

                .Col = ColItemCode
                mFindItemName = Trim(.Text)

                '            If mSearchItem = mFindItemName Then
                If InStr(1, mFindItemName, mSearchItem, CompareMethod.Text) > 0 Then
                    If lblBookType.Text = "I" Then
                        MainClass.SetFocusToCell(SprdMain, i, ColIssueQty)
                    Else
                        MainClass.SetFocusToCell(SprdMain, i, ColDemandQty)
                    End If
                    mSearchStartRow = i + 1
                    GoTo NextRec
                End If

                .Col = ColItemDesc
                mFindItemName = Trim(.Text)

                '            If mSearchItem = mFindItemName Then
                If InStr(1, mFindItemName, mSearchItem, CompareMethod.Text) > 0 Then
                    If lblBookType.Text = "I" Then
                        MainClass.SetFocusToCell(SprdMain, i, ColIssueQty)
                    Else
                        MainClass.SetFocusToCell(SprdMain, i, ColDemandQty)
                    End If
                    mSearchStartRow = i + 1
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
    Private Sub SearchProductionPlan()

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing


        If Trim(txtDept.Text) = "" Then Exit Sub
        If Trim(txtReqDate.Text) = "" Then Exit Sub
        If Not IsDate(txtReqDate.Text) Then Exit Sub
        If VB.Left(cboStockFor.Text, 1) <> "P" Then Exit Sub

        SqlStr = " SELECT DISTINCT " & vbCrLf & " IH.AUTO_KEY_PRODPLAN, IH.INHOUSE_CODE, IH.PRODUCT_CODE,  " & vbCrLf & " IH.SUPP_CUST_CODE, INVMST.ITEM_SHORT_DESC, DPLAN_QTY " & vbCrLf & " FROM PRD_PRODPLAN_MONTH_DET IH, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODe=INVMST.COMPANY_CODe " & vbCrLf & " AND IH.PRODUCT_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.SERIAL_DATE=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        '    If RsCompany.fields("COMPANY_CODE").value = 3 Then
        '        ''Show all Planning Ref....
        '    Else
        SqlStr = SqlStr & vbCrLf & " AND IH.DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'"
        '    End If

        If MainClass.SearchGridMasterBySQL2(txtprod.Text, SqlStr) = True Then
            txtprod.Text = Trim(AcName)
            lblProductCode.Text = Trim(AcName1)
            txtprod_Validating(txtprod, New System.ComponentModel.CancelEventArgs(False))
            MainClass.SetFocusToCell(SprdMain, 1, ColDemandQty)
        End If

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Function GetTodayPlanning(ByRef pItemCode As String, ByRef mPlanning As Double, ByRef mWIPStock As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsTemp1 As ADODB.Recordset = Nothing
        Dim mStdQty As Double
        Dim xPlanning As Double
        Dim mProductCode As String = ""
        Dim mLevel As Double
        Dim mTotDeptQty As Double
        Dim mDeptQty As Double
        Dim xItemUOM As String = ""
        Dim mDivisionCode As Double



        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If


        ''TRN.PRODUCT_CODE='" & MainClass.AllowSingleQuote(lblProductCode.text) & "'

        SqlStr = " SELECT " & vbCrLf & " TRN.PRODUCT_CODE, TRN.RM_CODE, (TRN.STD_QTY + GROSS_WT_SCRAP) AS STD_QTY , DEPT_CODE " & vbCrLf & " FROM VW_PRD_BOM_TRN TRN" & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND " & vbCrLf & " TRN.DEPT_CODE='" & MainClass.AllowSingleQuote((txtDept.Text)) & "'" & vbCrLf & " AND TRN.RM_CODE='" & pItemCode & "'"

        '    SqlStr = SqlStr & vbCrLf _
        ''            & " START WITH  TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND RM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
        ''            & " CONNECT BY PRIOR (TRIM(PRODUCT_CODE) || COMPANY_CODE || ' ')=TRIM(RM_CODE) || COMPANY_CODE || ' '"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        mTotDeptQty = 0
        mPlanning = 0
        mWIPStock = ""
        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mProductCode = IIf(IsDbNull(RsTemp.Fields("PRODUCT_CODE").Value), "", RsTemp.Fields("PRODUCT_CODE").Value)

                mLevel = 1 ''IIf(IsNull(RsTemp!Level), 0, RsTemp!Level)
                If mLevel = 1 Then
                    mStdQty = IIf(IsDbNull(RsTemp.Fields("STD_QTY").Value), 0, RsTemp.Fields("STD_QTY").Value)
                Else
                    mStdQty = mStdQty * IIf(IsDbNull(RsTemp.Fields("STD_QTY").Value), 0, RsTemp.Fields("STD_QTY").Value)
                End If

                SqlStr = " SELECT " & vbCrLf & " SUM(DPLAN_QTY) AS DPLAN_QTY " & vbCrLf & " FROM PRD_PRODPLAN_MONTH_DET IH" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.INHOUSE_CODE ='" & MainClass.AllowSingleQuote(mProductCode) & "' " & vbCrLf & " AND IH.SERIAL_DATE=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                SqlStr = SqlStr & vbCrLf & " AND IH.DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp1, ADODB.LockTypeEnum.adLockReadOnly)
                If RsTemp1.EOF = False Then
                    mPlanning = mPlanning + (Val(IIf(IsDbNull(RsTemp1.Fields("DPLAN_QTY").Value), 0, RsTemp1.Fields("DPLAN_QTY").Value)) * mStdQty)

                    If MainClass.ValidateWithMasterTable(Trim(mProductCode), "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        xItemUOM = Trim(MasterNo)
                    End If
                    mDeptQty = (GetBalanceStockQty(mProductCode, (txtReqDate.Text), xItemUOM, (txtDept.Text), "", "", ConPH, mDivisionCode) * mStdQty)
                    mDeptQty = mDeptQty - (GetWIPLockQty(mProductCode, (txtDept.Text), (txtReqDate.Text)) * mStdQty)
                    mTotDeptQty = mTotDeptQty + mDeptQty

                    If mDeptQty <> 0 Then
                        mWIPStock = IIf(mWIPStock = "", "", mWIPStock & ",") & mProductCode & " : " & mDeptQty
                    End If
                End If
                RsTemp.MoveNext()
            Loop
        Else
            SqlStr = " SELECT " & vbCrLf & " SUM(DPLAN_QTY) AS DPLAN_QTY " & vbCrLf & " FROM PRD_PRODPLAN_MONTH_DET IH" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.INHOUSE_CODE ='" & MainClass.AllowSingleQuote(pItemCode) & "' " & vbCrLf & " AND IH.SERIAL_DATE=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            SqlStr = SqlStr & vbCrLf & " AND IH.DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp1, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp1.EOF = False Then
                mPlanning = mPlanning + (Val(IIf(IsDbNull(RsTemp1.Fields("DPLAN_QTY").Value), 0, RsTemp1.Fields("DPLAN_QTY").Value)))

                If MainClass.ValidateWithMasterTable(Trim(pItemCode), "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    xItemUOM = Trim(MasterNo)
                End If
                mDeptQty = 0
                '            mDeptQty = (GetBalanceStockQty(pItemCode, txtReqDate.Text, xItemUOM, txtDept.Text, "", "", ConPH, mDivisionCode))
                '            mDeptQty = mDeptQty - GetWIPLockQty(pItemCode, txtDept.Text, txtReqDate.Text)
                '            mTotDeptQty = mTotDeptQty + mDeptQty

                If mDeptQty <> 0 Then
                    mWIPStock = IIf(mWIPStock = "", "", mWIPStock & ",") & pItemCode & " : " & mDeptQty
                End If
            End If
        End If

        mWIPStock = "WIP Stock : " & mTotDeptQty & " (" & mWIPStock & ")"
        If RsCompany.Fields("CHECK_FG_STOCK").Value = "N" Then
            xPlanning = mPlanning
        Else
            xPlanning = (mPlanning * 1.5) - mTotDeptQty
            xPlanning = IIf(xPlanning < 0, 0, xPlanning)
        End If
        GetTodayPlanning = xPlanning
        Exit Function
ErrPart:
        GetTodayPlanning = 0
    End Function

    Private Function GetMinInventory(ByRef pItemCode As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsTemp1 As ADODB.Recordset = Nothing
        Dim mStdQty As Double
        Dim mMinInv As Double
        Dim mProductCode As String = ""
        Dim mLevel As Double

        '    If RsCompany.fields("COMPANY_CODE").value = 1 Then
        '        SqlStr = " SELECT MAXIMUM_QTY FROM INV_ITEM_MST" & vbCrLf _
        ''                & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
        ''                & " AND ITEM_CODE ='" & MainClass.AllowSingleQuote(pItemCode) & "' "
        '
        '        MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp1, adLockReadOnly
        '        If RsTemp1.EOF = False Then
        '            mMinInv = (Val(IIf(IsNull(RsTemp1!MAXIMUM_QTY), 0, RsTemp1!MAXIMUM_QTY)))
        '        End If
        '    Else
        ''            & " AND TRN.PRODUCT_CODE='" & MainClass.AllowSingleQuote(lblProductCode.text) & "'" & vbCrLf _
        '
        SqlStr = " SELECT " & vbCrLf & " TRN.PRODUCT_CODE, TRN.RM_CODE, (TRN.STD_QTY + GROSS_WT_SCRAP) AS STD_QTY , DEPT_CODE " & vbCrLf & " FROM VW_PRD_BOM_TRN TRN" & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.DEPT_CODE='" & MainClass.AllowSingleQuote((txtDept.Text)) & "'" & vbCrLf & " AND TRN.RM_CODE='" & pItemCode & "'"

        '    SqlStr = SqlStr & vbCrLf _
        ''            & " START WITH  TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND RM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
        ''            & " CONNECT BY PRIOR (TRIM(PRODUCT_CODE) || COMPANY_CODE || ' ')=TRIM(RM_CODE) || COMPANY_CODE || ' '"


        'TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND PRIOR PRODUCT_CODE=RM_CODE

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mProductCode = IIf(IsDbNull(RsTemp.Fields("PRODUCT_CODE").Value), "", RsTemp.Fields("PRODUCT_CODE").Value)
                mLevel = 1 '' IIf(IsNull(RsTemp!Level), 0, RsTemp!Level)
                If mLevel = 1 Then
                    mStdQty = IIf(IsDbNull(RsTemp.Fields("STD_QTY").Value), 0, RsTemp.Fields("STD_QTY").Value)
                Else
                    mStdQty = mStdQty * IIf(IsDbNull(RsTemp.Fields("STD_QTY").Value), 0, RsTemp.Fields("STD_QTY").Value)
                End If

                SqlStr = " SELECT " & vbCrLf & " SUM(MIN_QTY) AS MIN_QTY " & vbCrLf & " FROM PRD_PRODSEQUENCE_DET IH" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE ='" & MainClass.AllowSingleQuote(mProductCode) & "' " ''& vbCrLf |                        & " AND IH.SERIAL_DATE=TO_DATE('" & VB6.Format(txtReqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                SqlStr = SqlStr & vbCrLf & " AND IH.DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp1, ADODB.LockTypeEnum.adLockReadOnly)
                If RsTemp1.EOF = False Then
                    mMinInv = mMinInv + (Val(IIf(IsDbNull(RsTemp1.Fields("MIN_QTY").Value), 0, RsTemp1.Fields("MIN_QTY").Value)) * mStdQty)
                End If
                RsTemp.MoveNext()
            Loop
        End If
        '    End If
        GetMinInventory = mMinInv
        Exit Function
ErrPart:
        GetMinInventory = 0
    End Function
End Class
