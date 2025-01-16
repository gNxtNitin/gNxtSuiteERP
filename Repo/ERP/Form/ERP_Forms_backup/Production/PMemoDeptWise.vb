Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports AxFPSpreadADO
Imports System.IO

Friend Class FrmPMemoDeptWise
    Inherits System.Windows.Forms.Form
    Dim RsPMemoMain As ADODB.Recordset ''Recordset	
    Dim RsPMemoDetail As ADODB.Recordset ''Recordset	
    Private PvtDBCn As ADODB.Connection

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Dim FormActive As Boolean

    Private Const ConRowHeight As Short = 30
    Dim mSearchStartRow As Integer

    Private Const ColItemCode As Short = 1
    Private Const ColItemDesc As Short = 2
    Private Const ColItemPartNo As Short = 3
    Private Const ColUom As Short = 4
    Private Const ColChildStock As Short = 5
    Private Const ColPrevStockQty As Short = 6
    Private Const ColStockQty As Short = 7
    Private Const ColBatchNo As Short = 8
    Private Const ColProdQty As Short = 9
    Private Const ColBreakageQty As Short = 10
    Private Const ColCRQty As Short = 11
    Private Const ColMRQty As Short = 12
    Private Const ColOPR As Short = 13
    Private Const ColOPRERCode As Short = 14
    Private Const ColOPRERName As Short = 15
    Private Const ColStockType As Short = 16
    Private Const ColMachineNo As Short = 17
    Private Const ColToolNo As Short = 18
    Private Const ColMachineWorkingHours As Short = 19
    Private Const ColBreakDownTime As Short = 20
    Private Const ColNoTool As Short = 21
    Private Const ColNoMaterial As Short = 22
    Private Const ColNoOperator As Short = 23
    Private Const ColPowerCutTime As Short = 24
    Private Const ColToolChangeTime As Short = 25
    Private Const ColSetupChangeTime As Short = 26
    Private Const ColQAIssue As Short = 27
    Private Const ColReason As Short = 28
    Private Const ColRemarks As Short = 29
    Private Const ColCostPcs As Short = 30

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer

    Dim FileDBCn As ADODB.Connection
    Private Function GetDevelopmentItemProdQty(ByRef xProductCode As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        GetDevelopmentItemProdQty = 0

        SqlStr = " SELECT SUM(PROD_QTY) AS PROD_QTY " & vbCrLf _
            & " FROM PRD_PMEMODEPT_DET " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(xProductCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetDevelopmentItemProdQty = IIf(IsDBNull(RsTemp.Fields("PROD_QTY").Value), 0, RsTemp.Fields("PROD_QTY").Value)
        End If

        Exit Function
ErrPart:
        GetDevelopmentItemProdQty = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function


    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboLineNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboLineNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboLineNo_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboLineNo.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub cboShiftcd_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShiftcd.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboShiftcd_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShiftcd.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboShiftcd_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles cboShiftcd.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(cboShiftcd.Text) = "C" Then
            If Trim(txtPMemoDate.Text) <> "" Then
                txtProdDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(VB6.Format(txtPMemoDate.Text, "DD/MM/YYYY"))))
                txtProdDate.Text = VB6.Format(txtProdDate.Text, "DD/MM/YYYY")
            End If
        Else
            txtProdDate.Text = VB6.Format(txtPMemoDate.Text, "DD/MM/YYYY")
        End If
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub cboType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboType.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboType.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkApproved_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkApproved.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkSPD_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkSPD.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click

        On Error GoTo AddErr
        If cmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            SprdMain.Enabled = True
            txtPMemoNo.Enabled = False
            cmdSearch.Enabled = False
            cmdPopulate.Enabled = True
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
    Private Sub FillCbo()

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

        cboDivision.Text = GetDefaultDivision()         ''cboDivision.SelectedIndex = -1
        cboShiftcd.Items.Clear()
        cboShiftcd.Items.Add(("A"))
        cboShiftcd.Items.Add(("B"))
        cboShiftcd.Items.Add(("C"))

        cboShiftcd.SelectedIndex = 0

        cboLineNo.Items.Clear()
        cboLineNo.Items.Add(("1"))
        cboLineNo.Items.Add(("2"))
        cboLineNo.Items.Add(("3"))

        cboLineNo.SelectedIndex = 0

        cboType.Items.Clear()
        cboType.Items.Add(("Production"))
        cboType.Items.Add(("Jobwork"))


        If lblBookType.Text = "P" Then
            cboType.SelectedIndex = 0
        ElseIf lblBookType.Text = "J" Then
            cboType.SelectedIndex = 1
        End If

        Exit Sub
FillERR:
        MsgBox(Err.Description)
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDelete.Click
        On Error GoTo DelErrPart
        Dim mItemCode As String

        If ValidateBranchLocking((txtPMemoDate.Text)) = True Then
            Exit Sub
        End If
        If Trim(txtPMemoNo.Text) = "" Then MsgInformation("Nothing to Delete") : Exit Sub

        If Not RsPMemoMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()

                If InsertIntoDelAudit(PubDBCn, "PRD_PMEMODEPT_HDR", (txtPMemoNo.Text), RsPMemoMain, "AUTO_KEY_REF", "D") = False Then GoTo DelErrPart
                If InsertIntoDelAudit(PubDBCn, "PRD_PMEMODEPT_DET", (txtPMemoNo.Text), RsPMemoDetail, "AUTO_KEY_REF", "D") = False Then GoTo DelErrPart

                If InsertIntoDeleteTrn(PubDBCn, "PRD_PMEMODEPT_HDR", "AUTO_KEY_REF", (lblMKey.Text)) = False Then GoTo DelErrPart

                If DeleteStockTRN(PubDBCn, ConStockRefType_PMEMODEPT, (txtPMemoNo.Text)) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM PRD_PMEMODEPT_DET WHERE AUTO_KEY_REF=" & Val(lblMKey.Text) & "")
                PubDBCn.Execute("DELETE FROM PRD_PMEMODEPT_HDR  WHERE AUTO_KEY_REF=" & Val(lblMKey.Text) & " AND BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "'")
                PubDBCn.CommitTrans()
                RsPMemoMain.Requery()
                RsPMemoDetail.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        RsPMemoMain.Requery()
        RsPMemoDetail.Requery()
    End Sub
    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdModify.Click

        On Error GoTo ModifyErr
        If cmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsPMemoMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True
            txtPMemoNo.Enabled = False
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

        On Error GoTo ErrorHandler
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim i As Integer
        Dim mItemCode As String
        Dim pOprCode As String = ""
        If Trim(txtDept.Text) = "" Then Exit Sub
        If Trim(txtPMemoDate.Text) = "" Then Exit Sub
        If Not IsDate(txtPMemoDate.Text) Then Exit Sub
        If VB.Left(cboType.Text, 1) = "J" Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = " SELECT DISTINCT " & vbCrLf _
            & " IH.INHOUSE_CODE AS PRODUCT_CODE, INVMST.ITEM_SHORT_DESC, INVMST.ISSUE_UOM " & vbCrLf _
            & " FROM PRD_PRODPLAN_MONTH_DET IH, INV_ITEM_MST INVMST" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.COMPANY_CODe=INVMST.COMPANY_CODe " & vbCrLf _
            & " AND IH.PRODUCT_CODE=INVMST.ITEM_CODE " & vbCrLf _
            & " AND IH.DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'" & vbCrLf _
            & " AND IH.SERIAL_DATE=TO_DATE('" & VB6.Format(txtProdDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        i = 1

        If RsTemp.EOF = False Then
            With SprdMain
                Do While RsTemp.EOF = False
                    .Row = i
                    .Col = ColItemCode
                    .Text = Trim(IIf(IsDBNull(RsTemp.Fields("PRODUCT_CODE").Value), "", RsTemp.Fields("PRODUCT_CODE").Value))
                    mItemCode = Trim(IIf(IsDBNull(RsTemp.Fields("PRODUCT_CODE").Value), "", RsTemp.Fields("PRODUCT_CODE").Value))

                    '.Col = ColItemDesc
                    '.Text = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value))

                    '.Col = ColUom
                    '.Text = IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)

                    '.Col = ColProdQty
                    '.Text = "0.00"

                    '.Col = ColCRQty
                    '.Text = "0.00"

                    '.Col = ColMRQty
                    '.Text = "0.00"

                    '.Col = ColStockType
                    '.Text = "ST"

                    '.Col = ColCostPcs
                    '.Text = "0.00"
                    Dim mSqlStr As String = ""
                    Dim RsTempOPr As ADODB.Recordset = Nothing

                    mSqlStr = GetProductOperationSql(mItemCode, Trim(txtDept.Text), 0)
                    MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempOPr, ADODB.LockTypeEnum.adLockReadOnly)

                    If RsTempOPr.EOF = False Then
                        Do While RsTempOPr.EOF = False
                            pOprCode = IIf(IsDBNull(RsTempOPr.Fields("OPR_DESC").Value), "", RsTempOPr.Fields("OPR_DESC").Value)
                            Call FillItemDescPart(mItemCode, i, pOprCode)

                            RsTempOPr.MoveNext()
                            If RsTempOPr.EOF = False Then
                                i = i + 1
                                .MaxRows = i
                            End If
                        Loop
                    Else
                        pOprCode = ""
                        Call FillItemDescPart(mItemCode, i, pOprCode)
                    End If

                    i = i + 1
                    .MaxRows = i
                    RsTemp.MoveNext()
                Loop
                Call FormatSprdMain(-1)
            End With
        Else
            MsgInformation("No Plan Enter For Such Dept. &  Date")
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        cmdPopulate.Enabled = False
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        If Err.Description = "" Then Exit Sub
    End Sub

    Private Sub PopulateFromXLSFile(ByVal strXLSFile As String)

        On Error GoTo ErrPart
        Dim i As Integer
        Dim SqlStr As String = ""
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mUOM As String
        Dim mPartNo As String
        Dim mStockType As String
        Dim mStockQty As Double
        Dim mProdQty As Double
        Dim xSqlStr As String
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsFile As ADODB.Recordset
        Dim FileConnStr As String

        Dim strTemp As String
        Dim strWkShName As String
        Dim strError As String
        Dim mDivisionCode As Double
        Dim mTagNo As Double
        Dim mRow As Integer

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
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

                    xSqlStr = " SELECT ITEM_SHORT_DESC,ISSUE_UOM,CUSTOMER_PART_NO " & vbCrLf _
                        & " FROM INV_ITEM_MST " & vbCrLf _
                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND LTRIM(RTRIM(ITEM_CODE))='" & MainClass.AllowSingleQuote(mItemCode) & "'"

                    MainClass.UOpenRecordSet(xSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                    If RsTemp.EOF = False Then
                        mItemDesc = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value))
                        mUOM = Trim(IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value))
                        mPartNo = Trim(IIf(IsDBNull(RsTemp.Fields("CUSTOMER_PART_NO").Value), "", RsTemp.Fields("CUSTOMER_PART_NO").Value))
                    Else
                        GoTo NextRecord
                    End If
                    If DuplicateItem() = True Then GoTo NextRecord


                    mProdQty = Val(IIf(IsDBNull(RsFile.Fields(3).Value), 0, RsFile.Fields(3).Value))

                    If mProdQty = 0 Then GoTo NextRecord


                    SprdMain.Row = mRow '' SprdMain.MaxRows	

                    MainClass.SetFocusToCell(SprdMain, mRow, ColItemCode)

                    SprdMain.Col = ColItemCode
                    SprdMain.Text = mItemCode

                    SprdMain.Col = ColItemDesc
                    SprdMain.Text = mItemDesc

                    SprdMain.Col = ColItemPartNo
                    SprdMain.Text = mPartNo

                    SprdMain.Col = ColUom
                    SprdMain.Text = mUOM

                    SprdMain.Col = ColProdQty
                    SprdMain.Text = CStr(mProdQty)



                    SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColItemCode, mRow, ColItemCode, mRow, False))

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


    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        '    txtPMemoNo.Text = ""	
        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If UpdateMain1() = True Then
            ADDMode = False
            MODIFYMode = False
            txtPMemoNo_Validating(txtPMemoNo, New System.ComponentModel.CancelEventArgs(False))
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

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                & " AND BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "'"

        If MainClass.SearchGridMaster(txtPMemoNo.Text, "PRD_PMEMODEPT_HDR ", "AUTO_KEY_REF", "REF_DATE", , , SqlStr) = True Then
            txtPMemoNo.Text = AcName
            If txtPMemoNo.Enabled = True Then txtPMemoNo.Focus()
        End If

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdSearchDept_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSearchDept.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , SqlStr) = True Then
            txtDept.Text = AcName1
            lblDept.Text = AcName
            If txtDept.Enabled = True Then txtDept.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


    Private Sub cmdSearchEmp_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchEmp.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        If MainClass.SearchGridMaster("", "ATH_PASSWORD_MST", "EMP_NAME", "USER_ID", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtEmp.Text = AcName1
            lblEmp.Text = AcName
            If txtEmp.Enabled = True Then txtEmp.Focus()
        End If

        'SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_LEAVE_DATE IS NULL "
        'If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
        '    txtEmp.Text = AcName1
        '    lblEmp.Text = AcName
        '    If txtEmp.Enabled = True Then txtEmp.Focus()
        'End If

        'If MainClass.ValidateWithMasterTable(txtEmp.Text, "USER_ID", "EMP_NAME", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    lblEmp.Text = MasterNo
        'End If

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub



    Private Sub FrmPMemoDeptWise_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim xIName As String
        Dim SqlStr As String = ""
        Dim pOPRCode As String
        Dim mProductCode As String
        Dim pOPRDesc As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTableName As String

        Dim xICode As String
        Dim mBatchNo As String
        Dim mUOM As String
        Dim mStockType As String

        Dim mDivisionCode As Double



        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If


        If eventArgs.row = 0 And eventArgs.col = ColItemCode Then
            With SprdMain
                .Row = .ActiveRow

                .Col = ColItemCode

                If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
                    mTableName = "PRD_NEWBOM_DET"
                Else
                    mTableName = "PRD_PRODSEQUENCE_DET"
                End If
                SqlStr = " SELECT DISTINCT IH.PRODUCT_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO" & vbCrLf _
                    & " FROM " & mTableName & " IH, INV_ITEM_MST INVMST" & vbCrLf _
                    & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
                    & " AND IH.PRODUCT_CODE=INVMST.ITEM_CODE" & vbCrLf _
                    & " AND IH.DEPT_CODE='" & Trim(txtDept.Text) & "' AND INVMST.ITEM_STATUS='A'"


                ''PRD_PRODSEQUENCE_DET

                'SqlStr = " SELECT DISTINCT IH.PRODUCT_CODE, HMST.ITEM_SHORT_DESC, CONNECT_BY_ROOT  IH.PRODUCT_CODE AS TOP_PRODUCT_CODE, CONNECT_BY_ROOT  HMST.ITEM_SHORT_DESC AS TOP_PRODUCT_NAME" & vbCrLf _
                '    & " FROM VW_PRD_BOM_TRN IH, INV_ITEM_MST HMST" & vbCrLf _
                '    & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                '    & " AND IH.COMPANY_CODE=HMST.COMPANY_CODE" & vbCrLf _
                '    & " AND IH.PRODUCT_CODE=HMST.ITEM_CODE" & vbCrLf _
                '    & " START WITH IH.DEPT_CODE='" & Trim(txtDept.Text) & "'" & vbCrLf _
                '    & " CONNECT BY PRIOR  IH.RM_CODE =  IH.PRODUCT_CODE"

                '               Select Case distinct IH.PRODUCT_CODE, HMST.ITEM_SHORT_DESC, CONNECT_BY_ROOT  IH.PRODUCT_CODE
                'From VW_PRD_BOM_TRN IH, INV_ITEM_MST HMST
                'Where IH.COMPANY_CODE = 1
                'And IH.COMPANY_CODE=HMST.COMPANY_CODE
                'And IH.PRODUCT_CODE=HMST.ITEM_CODE 
                'And IH.DEPT_CODE='SHR'
                'CONNECT BY  PRIOR IH.RM_CODE = IH.PRODUCT_CODE

                '                Select Case
                '   SYS_CONNECT_BY_PATH(PRODUCT_CODE,'/') as PRODUCT_CODE,
                '   RM_CODE,
                '   CONNECT_BY_ISCYCLE
                'FROM
                '                    VW_PRD_BOM_TRN TRN  where CONNECT_BY_ISCYCLE=1
                'CONNECT BY
                'NOCYCLE
                '                    PRIOR RM_CODE = PRODUCT_CODE

                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    '            If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", "", "", "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
                    .Row = .ActiveRow

                    .Col = ColItemCode
                    .Text = Trim(AcName)

                    .Col = ColItemDesc
                    .Text = Trim(AcName1)

                    .Col = ColItemPartNo
                    .Text = Trim(AcName2)
                    'Call SprdMain_LeaveCell(ColItemCode, .ActiveRow, ColItemCode, .ActiveRow, False)
                    Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColItemCode, .ActiveRow, ColItemCode, .ActiveRow, False))

                End If

            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColItemDesc Then
            With SprdMain
                .Row = .ActiveRow

                .Col = ColItemDesc

                If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
                    mTableName = "PRD_NEWBOM_DET"
                Else
                    mTableName = "PRD_PRODSEQUENCE_DET"
                End If

                SqlStr = " SELECT DISTINCT INVMST.ITEM_SHORT_DESC, IH.PRODUCT_CODE, INVMST.CUSTOMER_PART_NO" & vbCrLf _
                        & " FROM " & mTableName & " IH, INV_ITEM_MST INVMST" & vbCrLf _
                        & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
                        & " AND IH.PRODUCT_CODE=INVMST.ITEM_CODE" & vbCrLf _
                        & " AND IH.DEPT_CODE='" & Trim(txtDept.Text) & "' AND INVMST.ITEM_STATUS='A'"

                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    '            If MainClass.SearchGridMaster("", "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", "", "", "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
                    .Row = .ActiveRow

                    .Col = ColItemCode
                    .Text = Trim(AcName1)

                    .Col = ColItemDesc
                    .Text = Trim(AcName)

                    .Col = ColItemPartNo
                    .Text = Trim(AcName2)

                    Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColItemCode, .ActiveRow, ColItemCode, .ActiveRow, False))
                End If

            End With
        End If
        If eventArgs.row = 0 And eventArgs.col = ColStockType Then
            With SprdMain
                .Row = .ActiveRow

                .Col = ColStockType
                If MainClass.SearchGridMaster(.Text, "INV_TYPE_MST", "STOCK_TYPE_CODE", "STOCK_TYPE_DESC", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow

                    .Col = ColStockType
                    .Text = Trim(AcName)
                End If
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColStockType, .ActiveRow, ColStockType, .ActiveRow, False))
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColBatchNo Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemCode
                xICode = Trim(.Text)


                .Col = ColUom
                mUOM = Trim(.Text)

                .Col = ColStockType
                mStockType = Trim(.Text)

                .Col = ColBatchNo
                mBatchNo = Trim(.Text)

                SqlStr = GetItemBatchWiseQry(xICode, (txtPMemoDate.Text), mUOM, Trim(txtDept.Text), mStockType, mBatchNo, ConPH, ConStockRefType_PMEMODEPT, Val(txtPMemoNo.Text))
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColBatchNo
                    .Text = Trim(AcName1)
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColBatchNo)
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColOPR Then
            With SprdMain
                .Row = .ActiveRow

                .Col = ColItemCode
                mProductCode = Trim(.Text)

                If Trim(mProductCode) <> "" Then
                    .Col = ColOPR

                    SqlStr = OperationQuery(Trim(mProductCode), Trim(txtDept.Text), "", Trim(.Text), Trim(txtPMemoDate.Text), "TRIM(TO_CHAR(OPR_SNO,'00')) || '-' || MST.OPR_DESC", "TRN.OPR_CODE", "TO_CHAR(OPR_SNO)")

                    '                SqlStr = " SELECT TRIM(TO_CHAR(OPR_SNO,'00')) || '-' || MST.OPR_DESC, TRN.OPR_CODE, TO_CHAR(OPR_SNO) " & vbCrLf _	
                    ''                        & " FROM PRD_OPR_TRN TRN, PRD_OPR_MST MST" & vbCrLf _	
                    ''                        & " WHERE TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _	
                    ''                        & " AND TRN.COMPANY_CODE=MST.COMPANY_CODE" & vbCrLf _	
                    ''                        & " AND TRN.OPR_CODE=MST.OPR_CODE " & vbCrLf _	
                    ''                        & " AND TRN.DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'"	
                    '	
                    '                If Trim(.Text) <> "" Then	
                    '                    SqlStr = SqlStr & vbCrLf & " AND MST.OPR_DESC='" & MainClass.AllowSingleQuote(.Text) & "'"	
                    '                End If	
                    '	
                    '                If Trim(mProductCode) <> "" Then	
                    '                    SqlStr = SqlStr & vbCrLf & " AND TRN.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'"	
                    '                End If	
                    '	
                    '                SqlStr = SqlStr & vbCrLf _	
                    ''                        & " AND WEF=( " & vbCrLf _	
                    ''                        & " SELECT MAX(WEF) FROM PRD_OPR_TRN " & vbCrLf _	
                    ''                        & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _	
                    ''                        & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'"	
                    '	
                    '                If Trim(mProductCode) <> "" Then	
                    '                    SqlStr = SqlStr & vbCrLf & " AND TRN.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'"	
                    '                End If	
                    '	
                    '                SqlStr = SqlStr & vbCrLf & ")"	
                    '                SqlStr = SqlStr & vbCrLf & " ORDER BY OPR_SNO"	

                    '            If MainClass.SearchGridMaster(.Text, "PRD_OPR_MST", "OPR_DESC", "OPR_CODE", , , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'") = True Then	
                    If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                        .Row = .ActiveRow

                        .Col = ColOPR
                        .Text = Trim(Mid(AcName, 4))
                    End If
                End If
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColOPR, .ActiveRow, ColOPR, .ActiveRow, False))
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColOPRERCode Then
            With SprdMain
                .Row = .ActiveRow

                '            .Col = ColItemCode	
                '            mProductCode = Trim(.Text)	

                .Col = ColOPRERCode
                SqlStr = " SELECT EMP_NAME, EMP_CODE " & vbCrLf _
                    & " FROM PAY_CONT_EMPLOYEE_MST" & vbCrLf _
                    & " WHERE " & vbCrLf _
                    & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'"


                SqlStr = SqlStr & vbCrLf _
                    & " AND (EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE='' OR EMP_LEAVE_DATE>=TO_DATE('" & VB6.Format(txtProdDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
                '' Search All Operator	
                '            If ADDMode = True Then	
                '                SqlStr = SqlStr & vbCrLf & " AND EMP_CAT='P'"	
                '            End If	
                '            SqlStr = SqlStr & vbCrLf & " ORDER BY EMP_NAME"	

                SqlStr = SqlStr & vbCrLf & " UNION "

                SqlStr = SqlStr & vbCrLf & " SELECT EMP_NAME, EMP_CODE " & vbCrLf & " FROM PAY_EMPLOYEE_MST" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "' AND EMP_TYPE='W'"


                SqlStr = SqlStr & vbCrLf & " AND (EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE='' OR EMP_LEAVE_DATE>=TO_DATE('" & VB6.Format(txtProdDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

                '            SqlStr = SqlStr & vbCrLf & " ORDER BY EMP_NAME"	

                '            If MainClass.SearchGridMaster(.Text, "PRD_OPR_MST", "OPR_DESC", "OPR_CODE", , , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'") = True Then	
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColOPRERCode
                    .Text = Trim(AcName1)
                End If
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColOPRERCode, .ActiveRow, ColOPRERCode, .ActiveRow, False))
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColToolNo Then
            With SprdMain
                .Row = .ActiveRow

                .Col = ColItemCode
                mProductCode = Trim(.Text)

                .Col = ColOPR
                pOPRDesc = Trim(.Text)

                SqlStr = " SELECT OPR_CODE " & vbCrLf _
                    & " FROM PRD_OPR_MST" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'" & vbCrLf & " AND OPR_DESC='" & MainClass.AllowSingleQuote(pOPRDesc) & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = False Then
                    pOPRCode = IIf(IsDBNull(RsTemp.Fields("OPR_CODE").Value), "", RsTemp.Fields("OPR_CODE").Value)
                Else
                    pOPRCode = "-1"
                End If

                .Col = ColToolNo
                If MainClass.SearchGridMaster(.Text, "TOL_TOOLINFO_MST", "TOOL_NO", "OPR_CODE", "DEPT_CODE", , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "' AND OPR_CODE='" & MainClass.AllowSingleQuote(pOPRCode) & "' AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "' AND TOOL_STATUS='O' AND TOOL_UB='N'") = True Then
                    .Row = .ActiveRow

                    .Col = ColToolNo
                    .Text = Trim(AcName)
                End If
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColToolNo, .ActiveRow, ColToolNo, .ActiveRow, False))
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColMachineNo Then
            With SprdMain
                .Row = .ActiveRow

                .Col = ColMachineNo

                If MainClass.SearchGridMaster(.Text, "MAN_MACHINE_MST", "MACHINE_NO", "MACHINE_DESC", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "' AND MACHINE_UB='N' AND STATUS='O' AND DIV_CODE=" & mDivisionCode & "") = True Then
                    .Row = .ActiveRow

                    .Col = ColMachineNo
                    .Text = Trim(AcName)
                End If
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColMachineNo, .ActiveRow, ColMachineNo, .ActiveRow, False))
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

    End Sub
    Private Function GetItemBatchWiseQry(ByRef pItemCode As String, ByRef pDateTo As String, ByRef pPackUnit As String, ByRef pDeptCode As String, ByRef pStockType As String, ByRef pLotNo As String, ByRef pStock_ID As String, Optional ByRef pRefType As String = "", Optional ByRef pRefNo As Double = 0) As String

        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        Dim mBalQty As Double

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mIssueUOM As String
        Dim mPurchaseUOM As String
        Dim mFactor As Double
        Dim mTableName As String
        Dim xItemCode As String

        Dim xSqlStr As String
        Dim RsTemp1 As ADODB.Recordset
        Dim mProdSeq As Integer
        Dim mPrevDept As String
        Dim xAutoProdIssue As Boolean
        Dim xStockType As String

        xAutoProdIssue = CheckAutoIssueProd((txtPMemoDate.Text), "")


        mProdSeq = GetProductSeqNo(pItemCode, Trim(txtDept.Text), (txtPMemoDate.Text))
        If mProdSeq = 1 Then
            mPrevDept = ""
        Else
            If xAutoProdIssue = False Then
                mPrevDept = GetProductDept(pItemCode, mProdSeq, (txtPMemoDate.Text))
                xStockType = "WP"
            Else
                mPrevDept = GetProductDept(pItemCode, mProdSeq - 1, (txtPMemoDate.Text))
                xStockType = pStockType
            End If
        End If

        SqlStr = ""

        SqlStr = "SELECT ITEM_CODE, TO_CHAR(CASE WHEN BATCH_NO<='0' OR BATCH_NO=NULL OR BATCH_NO='' THEN '-1' ELSE BATCH_NO END) BATCH_NO, SUM(ITEM_QTY*DECODE(ITEM_IO,'I',1,-1)) AS BALQTY"

        mTableName = ConInventoryTable

        SqlStr = SqlStr & vbCrLf & " FROM " & mTableName & " "

        SqlStr = SqlStr & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        SqlStr = SqlStr & vbCrLf & "AND STOCK_ID='" & pStock_ID & "'"

        SqlStr = SqlStr & vbCrLf & " AND STATUS='O'"

        SqlStr = SqlStr & vbCrLf & " AND ITEM_CODE = '" & MainClass.AllowSingleQuote(pItemCode) & "'"

        SqlStr = SqlStr & vbCrLf & "AND DEPT_CODE_FROM='" & mPrevDept & "'" ''pDeptCode	

        If pRefType <> "" And Val(CStr(pRefNo)) <> 0 Then
            SqlStr = SqlStr & vbCrLf & " AND REF_TYPE || REF_NO <> '" & pRefType & pRefNo & "'"
        End If

        If pStockType = "QC" Then
            SqlStr = SqlStr & vbCrLf & " AND (STOCK_TYPE='" & pStockType & "' OR E_DATE>TO_DATE('" & VB6.Format(pDateTo, "dd-mmm-yyyy") & "','DD-MON-YYYY'))"
        Else
            If pStockType = "" Then
                SqlStr = SqlStr & vbCrLf & " AND E_DATE<=TO_DATE('" & VB6.Format(pDateTo, "dd-mmm-yyyy") & "','DD-MON-YYYY')"
            Else

                SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE='" & xStockType & "'"

                SqlStr = SqlStr & vbCrLf & " AND E_DATE<=TO_DATE('" & VB6.Format(pDateTo, "dd-mmm-yyyy") & "','DD-MON-YYYY')"
            End If
        End If

        SqlStr = SqlStr & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " HAVING SUM(ITEM_QTY*DECODE(ITEM_IO,'I',1,-1))<>0"

        SqlStr = SqlStr & vbCrLf & " GROUP BY ITEM_CODE,TO_CHAR(CASE WHEN BATCH_NO<='0' OR BATCH_NO=NULL OR BATCH_NO='' THEN '-1' ELSE BATCH_NO END)"



        xSqlStr = " SELECT RM_CODE " & vbCrLf & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST, INV_GENERAL_MST GMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY" & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.RM_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND INVMST.COMPANY_CODE=GMST.COMPANY_CODE" & vbCrLf & " AND INVMST.CATEGORY_CODE=GMST.GEN_CODE" & vbCrLf & " AND GMST.GEN_TYPE='C' AND GMST.PRD_TYPE IN ('P','I') AND DSP_RPT_FLAG='Y' AND STATUS='O'" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "' AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(pDeptCode) & "'"

        MainClass.UOpenRecordSet(xSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp1, ADODB.LockTypeEnum.adLockReadOnly)

        xItemCode = ""
        If RsTemp1.EOF = False Then
            '        Do While RsTemp1.EOF = False	
            xItemCode = IIf(IsDBNull(RsTemp1.Fields("RM_CODE").Value), "", Trim(RsTemp1.Fields("RM_CODE").Value))

            If xAutoProdIssue = True Then
                mProdSeq = 1
            Else
                mProdSeq = GetProductSeqNo(xItemCode, Trim(txtDept.Text), (txtPMemoDate.Text)) ''1 change on 04/02/2020	
            End If
            mPrevDept = GetProductDept(xItemCode, mProdSeq, (txtPMemoDate.Text))
            '            RsTemp1.MoveNext	
            '        Loop	


            SqlStr = SqlStr & " UNION ALL"

            SqlStr = SqlStr & " SELECT ITEM_CODE, TO_CHAR(CASE WHEN BATCH_NO<='0' OR BATCH_NO=NULL OR BATCH_NO='' THEN '-1' ELSE BATCH_NO END) BATCH_NO, SUM(ITEM_QTY*DECODE(ITEM_IO,'I',1,-1)) AS BALQTY"

            mTableName = ConInventoryTable

            SqlStr = SqlStr & vbCrLf & " FROM " & mTableName & " "

            SqlStr = SqlStr & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

            SqlStr = SqlStr & vbCrLf & "AND STOCK_ID='" & pStock_ID & "'"

            SqlStr = SqlStr & vbCrLf & " AND STATUS='O'"

            SqlStr = SqlStr & vbCrLf & " AND ITEM_CODE ='" & MainClass.AllowSingleQuote(xItemCode) & "'"

            SqlStr = SqlStr & vbCrLf & "AND DEPT_CODE_FROM='" & mPrevDept & "'"


            '    SqlStr = SqlStr & vbCrLf & " AND (BATCH_NO>=0 OR BATCH_NO=" & Val(pLotNo) & ")"	

            If pRefType <> "" And Val(CStr(pRefNo)) <> 0 Then
                SqlStr = SqlStr & vbCrLf & " AND REF_TYPE || REF_NO <> '" & pRefType & pRefNo & "'"
            End If

            If pStockType = "QC" Then
                SqlStr = SqlStr & vbCrLf & " AND (STOCK_TYPE='" & pStockType & "' OR E_DATE>TO_DATE('" & VB6.Format(pDateTo, "dd-mmm-yyyy") & "','DD-MON-YYYY'))"
            Else
                If pStockType = "" Then
                    SqlStr = SqlStr & vbCrLf & " AND E_DATE<=TO_DATE('" & VB6.Format(pDateTo, "dd-mmm-yyyy") & "','DD-MON-YYYY')"
                Else
                    SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE='" & pStockType & "'"

                    '            If PubUserID <> "G0416" Then	
                    SqlStr = SqlStr & vbCrLf & " AND E_DATE<=TO_DATE('" & VB6.Format(pDateTo, "dd-mmm-yyyy") & "','DD-MON-YYYY')"
                    '            End If	
                End If
            End If

            '    If PubUserID <> "G0416" Then	
            SqlStr = SqlStr & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            '    End If	

            SqlStr = SqlStr & vbCrLf & " HAVING SUM(ITEM_QTY*DECODE(ITEM_IO,'I',1,-1))<>0"

            SqlStr = SqlStr & vbCrLf & " GROUP BY ITEM_CODE,TO_CHAR(CASE WHEN BATCH_NO<='0' OR BATCH_NO=NULL OR BATCH_NO='' THEN '-1' ELSE BATCH_NO END)"
        End If

        GetItemBatchWiseQry = SqlStr

        Exit Function
ErrPart:
        GetItemBatchWiseQry = ""
    End Function

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim mRowNo As Integer
        Dim mItemCode As String
        Dim mBatchNo As String
        Dim mDivisionCode As Double
        Dim mOPRERName As String
        Dim mOPRERCode As String

        Dim SqlStr As String
        If eventArgs.newRow = -1 Then Exit Sub

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If


        SprdMain.Row = SprdMain.ActiveRow
        SprdMain.Col = ColItemCode
        If Trim(SprdMain.Text) = "" Then Exit Sub
        Select Case eventArgs.col
            Case ColItemCode
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColItemCode
                If DuplicateItem() = False Then
                    SprdMain.Row = SprdMain.ActiveRow
                    SprdMain.Col = ColItemCode
                    If FillItemDescPart(Trim(SprdMain.Text), SprdMain.Row, "") = False Then
                        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode) ' SprdMain.ActiveRow	
                        eventArgs.cancel = True
                        Exit Sub
                    Else
                        MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                        FormatSprdMain((SprdMain.MaxRows))
                    End If
                Else
                    MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode) '  SprdMain.ActiveRow, ColItemCode	
                    eventArgs.cancel = True
                    Exit Sub
                End If
            Case ColBatchNo '	
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColItemCode
                If DuplicateItem() = False Then
                    mRowNo = SprdMain.ActiveRow
                    SprdMain.Row = SprdMain.ActiveRow
                    SprdMain.Col = ColItemCode
                    mItemCode = Trim(SprdMain.Text)

                    SprdMain.Col = ColBatchNo
                    mBatchNo = Trim(SprdMain.Text)

                    If FillStock(mRowNo, mItemCode, mBatchNo) = False Then
                        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColBatchNo) ' SprdMain.ActiveRow	
                        eventArgs.cancel = True
                        Exit Sub
                    Else
                        MainClass.AddBlankSprdRow(SprdMain, ColBatchNo, ConRowHeight)
                        FormatSprdMain((SprdMain.MaxRows))
                    End If
                Else
                    MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColBatchNo) '  SprdMain.ActiveRow, ColItemCode	
                    eventArgs.cancel = True
                    Exit Sub
                End If


            Case ColProdQty
                If CheckQty() = True Then
                    '                MainClass.SetFocusToCell SprdMain, .ActiveRow, ColProdQty	
                    '                MainClass.AddBlankSprdRow SprdMain, ColItemCode, ConRowHeight	
                    '                FormatSprdMain SprdMain.MaxRows	
                End If
            Case ColStockType
                Call CheckStockType()
            Case ColOPR
                If DuplicateItem() = True Then
                    MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColOPR) ' SprdMain.ActiveRow	
                    eventArgs.cancel = True
                    Exit Sub
                End If
                Call CheckOPR()


            Case ColOPRERCode
                If DuplicateItem() = True Then
                    MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColOPRERCode) ' SprdMain.ActiveRow	
                    eventArgs.cancel = True
                    Exit Sub
                End If
                Call CheckOPERATOR()

                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColOPRERCode
                mOPRERCode = SprdMain.Text

                If MainClass.ValidateWithMasterTable(mOPRERCode, "EMP_CODE", "EMP_NAME", "PAY_CONT_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mOPRERName = MasterNo
                Else
                    If MainClass.ValidateWithMasterTable(mOPRERCode, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mOPRERName = MasterNo
                    Else
                        mOPRERName = ""
                    End If
                End If

                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColOPRERName
                SprdMain.Text = mOPRERName


            Case ColToolNo
                Call CheckToolNo()
            Case ColMachineNo
                SprdMain.Col = ColMachineNo
                If Trim(SprdMain.Text) = "" Then Exit Sub

                SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "' AND DIV_CODE=" & mDivisionCode & ""

                If ADDMode = True Then
                    SqlStr = SqlStr & " AND MACHINE_UB='N' AND STATUS='O'"
                End If
                If MainClass.ValidateWithMasterTable(Trim(SprdMain.Text), "MACHINE_NO", "MACHINE_NO", "MAN_MACHINE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
                    MsgInformation("Invalid Machine No.")
                    MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColMachineNo)
                    Exit Sub
                End If
        End Select
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Function DuplicateItem() As Boolean
        Dim cntRow As Integer
        Dim mCount As Byte
        Dim mCheckItemCode As String
        Dim mCheckOPR As String
        Dim mCheckOperatorCode As String
        Dim mCheckBatchNo As String

        Dim mItemCode As String
        Dim mOPR As String
        Dim mOperatorCode As String
        Dim mBatchNo As String

        With SprdMain
            .Row = .ActiveRow
            .Col = ColItemCode
            mCheckItemCode = Trim(UCase(.Text))

            .Col = ColOPR
            mCheckOPR = Trim(UCase(.Text))

            .Col = ColOPRERCode
            mCheckOperatorCode = Trim(UCase(.Text))

            .Col = ColBatchNo
            mCheckBatchNo = Trim(UCase(.Text))

            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColItemCode
                mItemCode = Trim(UCase(.Text))

                .Col = ColOPR
                mOPR = Trim(UCase(.Text))

                .Col = ColOPRERCode
                mOperatorCode = Trim(UCase(.Text))

                .Col = ColBatchNo
                mBatchNo = Trim(UCase(.Text))

                If (mCheckItemCode & "-" & mCheckOPR & "-" & mCheckOperatorCode & "-" & mCheckBatchNo = mItemCode & "-" & mOPR & "-" & mOperatorCode & "-" & mBatchNo And mCheckItemCode <> "") Then
                    mCount = mCount + 1
                End If

                If mCount > 1 Then
                    DuplicateItem = True
                    MsgInformation("Duplicate Item : " & mCheckItemCode & " Operation : " & mCheckOPR & " Operator : " & mCheckOperatorCode & " Batch No : " & mCheckBatchNo)
                    Exit Function
                End If
            Next
        End With
    End Function

    Private Function DuplicateOperation(ByRef mCheckItemCode As String, ByRef mCheckOPR As String, ByRef mCheckOperatorCode As String) As Boolean
        Dim cntRow As Integer
        Dim mCount As Byte
        'Dim mCheckItemCode As String	
        'Dim mCheckOPR As String	
        'Dim mCheckOperatorCode As String	
        Dim mItemCode As String
        Dim mOPR As String
        Dim mOperatorCode As String

        With SprdMain
            '        .Row = pRow	
            '        .Col = ColItemCode	
            '        mCheckItemCode = Trim(UCase(.Text))	
            '	
            '        .Col = ColOPR	
            '        mCheckOPR = Trim(UCase(.Text))	
            '	
            '        .Col = ColOPRERCode	
            '        mCheckOperatorCode = Trim(UCase(.Text))	

            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColItemCode
                mItemCode = Trim(UCase(.Text))

                .Col = ColOPR
                mOPR = Trim(UCase(.Text))

                .Col = ColOPRERCode
                mOperatorCode = Trim(UCase(.Text))

                If (mCheckItemCode & "-" & mCheckOPR & "-" & mCheckOperatorCode = mItemCode & "-" & mOPR & "-" & mOperatorCode And mCheckItemCode <> "") Then
                    mCount = mCount + 1
                End If

                If mCount > 1 Then
                    DuplicateOperation = True
                    '                MsgInformation "Duplicate Item : " & mCheckItemCode & " Operation : " & mCheckOPR & " Operator : " & mCheckOperatorCode	
                    Exit Function
                End If
            Next
        End With
    End Function

    Private Sub CheckStockType()

        On Error GoTo ChkERR
        Dim mStockType As String

        With SprdMain
            .Row = .ActiveRow
            .Col = ColStockType
            If Trim(.Text) = "" Then Exit Sub
            If MainClass.ValidateWithMasterTable(Trim(.Text), "STOCK_TYPE_CODE", "STOCK_TYPE_CODE", "INV_TYPE_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mStockType = MasterNo
                '            If Trim(mStockType) <> "FG" Then	
                '                MsgInformation "Please Select 'FG' Stock Type."	
                '                MainClass.SetFocusToCell SprdMain, .ActiveRow, ColStockType	
                '                Exit Sub	
                '            End If	
            Else
                MsgInformation("Invalid Stock Type.")
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColStockType)
                Exit Sub
            End If
        End With
        Exit Sub
ChkERR:
        MsgBox(Err.Description)
    End Sub
    Private Sub CheckOPR()

        On Error GoTo ChkERR
        Dim mOPR As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mProductCode As String

        With SprdMain
            .Row = .ActiveRow

            .Col = ColItemCode
            mProductCode = Trim(.Text)

            .Col = ColOPR
            If Trim(.Text) = "" Then Exit Sub

            SqlStr = OperationQuery(Trim(mProductCode), Trim(txtDept.Text), "", Trim(.Text), Trim(txtPMemoDate.Text), "TRN.OPR_CODE")

            '        SqlStr = " SELECT TRN.OPR_CODE " & vbCrLf _	
            ''                & " FROM PRD_OPR_TRN TRN, PRD_OPR_MST MST" & vbCrLf _	
            ''                & " WHERE " & vbCrLf _	
            ''                & " TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _	
            ''                & " AND TRN.COMPANY_CODE=MST.COMPANY_CODE " & vbCrLf _	
            ''                & " AND TRN.OPR_CODE=MST.OPR_CODE " & vbCrLf _	
            ''                & " AND TRN.DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'" & vbCrLf _	
            ''                & " AND MST.OPR_DESC='" & MainClass.AllowSingleQuote(.Text) & "'"	
            '	
            '        If Trim(mProductCode) <> "" Then	
            '            SqlStr = SqlStr & vbCrLf & " AND TRN.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'"	
            '        End If	
            '	
            '        SqlStr = SqlStr & vbCrLf _	
            ''            & " AND TRN.WEF = (SELECT MAX(WEF) AS WEF FROM PRD_OPR_TRN" & vbCrLf _	
            ''            & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _	
            ''            & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'"	
            '	
            '        If Trim(mProductCode) <> "" Then	
            '            SqlStr = SqlStr & vbCrLf & " AND TRN.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'"	
            '        End If	
            '	
            '        SqlStr = SqlStr & vbCrLf _	
            ''            & " AND WEF<= '" & VB6.Format(txtPMemoDate, "DD-MMM-YYYY") & "')"	

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = True Then
                MsgInformation("Invalid Operation for such Dept.")
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColOPR)
                Exit Sub
            End If

            '        If MainClass.ValidateWithMasterTable(Trim(.Text), "OPR_DESC", "OPR_CODE", "PRD_OPR_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.fields("COMPANY_CODE").value & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'") = True Then	
            '            mOPR = MasterNo	
            '        Else	
            '            MsgInformation "Invalid Operation for such Dept."	
            '            MainClass.SetFocusToCell SprdMain, .ActiveRow, ColOPR	
            '            Exit Sub	
            '        End If	
        End With
        Exit Sub
ChkERR:
        MsgBox(Err.Description)
    End Sub
    Private Sub CheckOPERATOR()

        On Error GoTo ChkERR
        Dim mOPR As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mProductCode As String

        With SprdMain
            .Row = .ActiveRow

            .Col = ColOPRERCode
            If Trim(.Text) = "" Then Exit Sub

            SqlStr = " SELECT EMP_NAME, EMP_CODE " & vbCrLf _
                   & " FROM PAY_CONT_EMPLOYEE_MST" & vbCrLf _
                   & " WHERE " & vbCrLf _
                   & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                   & " AND EMP_CODE='" & MainClass.AllowSingleQuote(.Text) & "'" & vbCrLf _
                   & " AND EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'"

            'Check All Workers	
            '        If ADDMode = True Then	
            '            SqlStr = SqlStr & vbCrLf & " AND EMP_CAT='P'"	
            '        End If	

            SqlStr = SqlStr & vbCrLf & " AND (EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE='' OR EMP_LEAVE_DATE>=TO_DATE('" & VB6.Format(txtProdDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

            SqlStr = SqlStr & vbCrLf & " UNION "

            SqlStr = SqlStr & vbCrLf & " SELECT EMP_NAME, EMP_CODE " & vbCrLf _
                    & " FROM PAY_EMPLOYEE_MST" & vbCrLf _
                    & " WHERE " & vbCrLf _
                    & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND EMP_CODE='" & MainClass.AllowSingleQuote(.Text) & "'" & vbCrLf _
                    & " AND EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'"


            SqlStr = SqlStr & vbCrLf & " AND EMP_TYPE='W'"


            SqlStr = SqlStr & vbCrLf & " AND (EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE='' OR EMP_LEAVE_DATE>=TO_DATE('" & VB6.Format(txtProdDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"


            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = True Then
                MsgInformation("Invalid Operator Name for such Dept.")
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColOPRERCode)
                Exit Sub
            End If

        End With
        Exit Sub
ChkERR:
        MsgBox(Err.Description)
    End Sub
    Private Sub CheckToolNo()

        On Error GoTo ChkERR
        Dim pOPRCode As String
        Dim pOPRDesc As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mProductCode As String


        Exit Sub

        With SprdMain
            .Row = .ActiveRow

            .Col = ColItemCode
            mProductCode = Trim(.Text)

            .Col = ColOPR
            pOPRDesc = Trim(.Text)

            SqlStr = OperationQuery(Trim(mProductCode), Trim(txtDept.Text), "", Trim(pOPRDesc), Trim(txtPMemoDate.Text), "TRN.OPR_CODE")

            '        SqlStr = " SELECT TRN.OPR_CODE " & vbCrLf _	
            ''                & " FROM PRD_OPR_TRN TRN, PRD_OPR_MST MST" & vbCrLf _	
            ''                & " WHERE " & vbCrLf _	
            ''                & " TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _	
            ''                & " AND TRN.COMPANY_CODE=MST.COMPANY_CODE " & vbCrLf _	
            ''                & " AND TRN.OPR_CODE=MST.OPR_CODE " & vbCrLf _	
            ''                & " AND TRN.DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'" & vbCrLf _	
            ''                & " AND TRN.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'" & vbCrLf _	
            ''                & " AND MST.OPR_DESC='" & MainClass.AllowSingleQuote(pOPRDesc) & "'"	
            '	
            '        SqlStr = SqlStr & vbCrLf _	
            ''            & " AND TRN.WEF = (SELECT MAX(WEF) AS WEF FROM PRD_OPR_TRN" & vbCrLf _	
            ''            & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _	
            ''            & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'" & vbCrLf _	
            ''            & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'" & vbCrLf _	
            ''            & " AND WEF<= '" & VB6.Format(txtPMemoDate, "DD-MMM-YYYY") & "')"	

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                pOPRCode = IIf(IsDBNull(RsTemp.Fields("OPR_CODE").Value), "", RsTemp.Fields("OPR_CODE").Value)
            Else
                pOPRCode = "-1"
            End If

            .Col = ColToolNo
            If Trim(.Text) = "" Then Exit Sub

            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "' AND OPR_CODE='" & MainClass.AllowSingleQuote(pOPRCode) & "' AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'"

            If ADDMode = True Then
                SqlStr = SqlStr & " AND TOOL_STATUS='O' AND TOOL_UB='N'"
            End If

            If MainClass.ValidateWithMasterTable(Trim(.Text), "TOOL_NO", "TOOL_NO", "TOL_TOOLINFO_MST", PubDBCn, MasterNo, , SqlStr) = False Then
                MsgInformation("Invalid Tool No.")
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColToolNo)
                Exit Sub
            End If



        End With

        Exit Sub
ChkERR:
        MsgBox(Err.Description)
    End Sub
    Private Function CheckQty() As Boolean
        On Error GoTo ERR1
        Dim mProdQty As Double

        CheckQty = True
        Exit Function

        With SprdMain
            .Row = .ActiveRow
            '        .Col = ColProdQty	
            '        mProdQty = Val(.Text)	
            '	
            '        .Col = ColOKQty	
            '        mOKQty = Val(.Text)	
            '	
            '        If mProdQty < mOKQty Then	
            '            CheckQty = False	
            '        Else	
            '            CheckQty = True	
            '        End If	
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function CheckWIPQty(ByRef mProductCode As String, ByRef mProductSeqNo As String, ByRef mDept As String, ByRef mPMemoDate As String, ByRef mProdQty As Double, ByRef mTotalWIPQty As Double, ByRef mDivisionCode As Double, ByRef mMessage As String, ByRef mProductionPlan As Double) As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mStdQty As Double
        Dim mItemUOM As String
        Dim mPlanQty As String
        Dim RsDept As ADODB.Recordset = Nothing
        Dim mProdDept As String
        Dim mSerialNo As Double
        Dim mDespatchQty As Double
        Dim xProdCode As String
        Dim xCustomerCode As String
        Dim mAlterMainItem As String
        Dim mBalancePlan As Double

        mTotalWIPQty = 0
        mMessage = ""

        If RsCompany.Fields("CHECK_BOP_STOCK").Value = "N" Then
            CheckWIPQty = True
            Exit Function
        End If

        If GetProductionType(mProductCode) = "J" Then
            CheckWIPQty = True
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable(mProductCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mItemUOM = MasterNo
        End If

        mTotalWIPQty = GetBalanceStockQty(mProductCode, VB6.Format(mPMemoDate, "DD/MM/YYYY"), mItemUOM, "STR", "X", "", ConWH, mDivisionCode)
        mTotalWIPQty = mTotalWIPQty - GetWIPLockQty(mProductCode, "STR", mPMemoDate)

        SqlStr = "SELECT DEPT_CODE, SERIAL_NO" & vbCrLf _
            & " FROM PRD_PRODSEQUENCE_DET " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'" & vbCrLf _
            & " AND SERIAL_NO>=" & Val(mProductSeqNo) & "" & vbCrLf _
            & " AND WEF = (" & vbCrLf _
            & " SELECT MAX(WEF) AS WEF FROM PRD_PRODSEQUENCE_DET" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'" & vbCrLf _
            & " AND WEF <=TO_DATE('" & VB6.Format(mPMemoDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))" & vbCrLf _
            & " ORDER BY SERIAL_NO"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockReadOnly)

        If RsDept.EOF = False Then
            Do While RsDept.EOF = False
                mProdDept = IIf(IsDBNull(RsDept.Fields("DEPT_CODE").Value), "", RsDept.Fields("DEPT_CODE").Value)
                mSerialNo = IIf(IsDBNull(RsDept.Fields("SERIAL_NO").Value), 0, RsDept.Fields("SERIAL_NO").Value)

                If Val(CStr(mSerialNo)) = Val(mProductSeqNo) Then
                    mTotalWIPQty = mTotalWIPQty + GetBalanceStockQty(mProductCode, VB6.Format(mPMemoDate, "DD/MM/YYYY"), mItemUOM, mProdDept, "ST", "X", ConPH, mDivisionCode, ConStockRefType_PMEMODEPT, Val(txtPMemoNo.Text))
                Else
                    mTotalWIPQty = mTotalWIPQty + GetBalanceStockQty(mProductCode, VB6.Format(mPMemoDate, "DD/MM/YYYY"), mItemUOM, mProdDept, "", "X", ConPH, mDivisionCode, ConStockRefType_PMEMODEPT, Val(txtPMemoNo.Text))
                End If
                mTotalWIPQty = mTotalWIPQty - GetWIPLockQty(mProductCode, mProdDept, mPMemoDate)
                RsDept.MoveNext()
            Loop
        End If



        '    mAlterMainItem = GetAlterMainItemCode(mProductCode)	
        'PRODUCT_CODE, SUPP_CUST_CODE,	

        '    SqlStr = "SELECT  SUM(IPLAN_QTY) AS IPLAN_QTY " & vbCrLf _	
        ''            & " FROM PRD_PRODPLAN_MONTH_DET " & vbCrLf _	
        ''            & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & ""	
        '	
        '    SqlStr = SqlStr & vbCrLf & " AND INHOUSE_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'"	

        '    SqlStr = SqlStr & vbCrLf & " AND INHOUSE_CODE IN " & mAlterMainItem & ""	

        '    If Trim(mAlterMainItem) = "" Then	
        '        SqlStr = SqlStr & vbCrLf & " AND INHOUSE_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'"	
        '    Else	
        '        SqlStr = SqlStr & vbCrLf & " AND INHOUSE_CODE IN ('" & MainClass.AllowSingleQuote(mProductCode) & "','" & MainClass.AllowSingleQuote(mAlterMainItem) & "')"	
        '    End If	

        '    SqlStr = SqlStr & vbCrLf _	
        ''            & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(mDept) & "'" & vbCrLf _	
        ''            & " AND SERIAL_DATE = '" & VB6.Format(mPMemoDate, "DD-MMM-YYYY") & "'" '' & vbCrLf _	
        ''            & " GROUP BY PRODUCT_CODE,SUPP_CUST_CODE"	
        '	
        '	
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly	
        '    mPlanQty = 0	
        '    mDespatchQty = 0	
        '	
        '    If RsTemp.EOF = False Then	
        '        Do While RsTemp.EOF = False	
        '            mPlanQty = mPlanQty + Val(IIf(IsNull(RsTemp!IPLAN_QTY), 0, RsTemp!IPLAN_QTY))	
        ''            xProdCode = Trim(IIf(IsNull(RsTemp!PRODUCT_CODE), "", RsTemp!PRODUCT_CODE))	
        ''            xCustomerCode = Trim(IIf(IsNull(RsTemp!SUPP_CUST_CODE), "", RsTemp!SUPP_CUST_CODE))	
        ''            mDespatchQty = mDespatchQty + GetDespatchQty(xProdCode, mPMemoDate, xCustomerCode)	
        '            RsTemp.MoveNext	
        '        Loop	
        '    End If	

        mBalancePlan = mProductionPlan * IIf(RsCompany.Fields("COMPANY_CODE").Value = 34, 4, 2) '' mPlanQty - mDespatchQty	
        '    mBalancePlan = Round(mPlanQty * 102 * 0.01, 0) ''Allow 2% Extra	

        ''mPlanQty = Round(mPlanQty * 3 / 25, 0)	


        ''Temp Lock
        'If mTotalWIPQty + mProdQty > mBalancePlan Then
        '    mMessage = " You have already enough stock of Product Code : " & mProductCode & vbCrLf & " Total WIP/FG Qty : " & mTotalWIPQty & vbCrLf & " Today Production Plan Qty : " & mProductionPlan & vbCrLf & " Max Production to be Enter : " & mBalancePlan - mTotalWIPQty & vbCrLf & " Production Qty : " & mProdQty

        '    CheckWIPQty = False
        'Else
        '    CheckWIPQty = True
        'End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function GetDespatchQty(ByRef pItemCode As String, ByRef pDate As String, ByRef xCustomerCode As String) As Double

        On Error GoTo ERR1
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim pDateFrom As String
        Dim pDateTo As String


        pDateFrom = "01/" & VB6.Format(pDate, "MM/YYYY")
        pDateTo = MainClass.LastDay(Month(CDate(pDate)), Year(CDate(pDate))) & "/" & VB6.Format(pDate, "MM/YYYY")

        GetDespatchQty = 0
        ''SELECT CLAUSE...	

        mSqlStr = " SELECT  SUM(ID.ITEM_QTY) AS ITEM_QTY"

        ''FROM CLAUSE...	
        mSqlStr = mSqlStr & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID, FIN_INVTYPE_MST TYPEMST"


        ''WHERE CLAUSE...	
        mSqlStr = mSqlStr & vbCrLf & " WHERE IH.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        mSqlStr = mSqlStr & vbCrLf & " AND IH.MKEY = ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=TYPEMST.COMPANY_CODE" & vbCrLf & " AND IH.TRNTYPE=TYPEMST.CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE ='" & MainClass.AllowSingleQuote(xCustomerCode) & "' " & vbCrLf & " AND ID.ITEM_CODE ='" & MainClass.AllowSingleQuote(pItemCode) & "' " & vbCrLf & " AND TYPEMST.ISSUPPBILL='N'" & vbCrLf & " AND TYPEMST.ISSALEJW='N'" & vbCrLf & " AND TYPEMST.ISSALERETURN='N' " & vbCrLf & " AND TYPEMST.ISSALECOMP='Y' " & vbCrLf & " AND IH.REJECTION='N' " & vbCrLf & " AND IH.AGTD3='N' "

        mSqlStr = mSqlStr & vbCrLf & " AND IH.INVOICE_DATE >= TO_DATE('" & VB6.Format(pDateFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND IH.INVOICE_DATE <= TO_DATE('" & VB6.Format(pDateTo, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            GetDespatchQty = IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)
        End If
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        GetDespatchQty = 0
    End Function
    Private Function FillItemDescPart(ByVal pItemCode As String, ByVal pRow As Long, ByVal pOprCode As String) As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCategoryCode As String
        Dim mStockType As String
        Dim mItemCode As String
        Dim mItemUOM As String
        Dim mProdSeq As Integer
        Dim mPrevDept As String
        Dim mDivisionCode As Double
        Dim xFGBatchNoReq As String
        Dim xFGBatchNo As String

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If

        FillItemDescPart = False
        If Trim(pItemCode) = "" Then Exit Function

        SqlStr = "SELECT ITEM_CODE, ITEM_SHORT_DESC,ISSUE_UOM,CUSTOMER_PART_NO,CATEGORY_CODE " & vbCrLf _
            & " FROM INV_ITEM_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            mCategoryCode = Trim(IIf(IsDBNull(RsTemp.Fields("CATEGORY_CODE").Value), "", RsTemp.Fields("CATEGORY_CODE").Value))

            '        If MainClass.ValidateWithMasterTable(mCategoryCode, "GEN_CODE", "STOCKTYPE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND GEN_TYPE='C'") = True Then	
            '            mStockType = MasterNo	
            ''            If mStockType <> "FG" Then	
            ''                MsgInformation "Not a Finish Good Item."	
            ''                FillItemDescPart = False	
            ''                Exit Function	
            ''            End If	
            '        Else	
            '            MsgInformation "Invalid Stock Type. Please Check Category For This Item"	
            '            FillItemDescPart = False	
            '            Exit Function	
            '        End If	


            With SprdMain
                .Row = pRow     ''.ActiveRow

                .Col = ColItemCode
                .Text = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))
                mItemCode = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))

                If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DSP_RPT_FLAG='Y'") = True Then
                    xFGBatchNoReq = "Y"
                    .Col = ColBatchNo
                    xFGBatchNo = Trim(.Text)
                Else
                    xFGBatchNoReq = "N"
                    xFGBatchNo = "X"
                End If


                .Col = ColItemDesc
                .Text = IIf(IsDBNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value)

                .Col = ColItemPartNo
                .Text = IIf(IsDBNull(RsTemp.Fields("CUSTOMER_PART_NO").Value), "", RsTemp.Fields("CUSTOMER_PART_NO").Value)

                ''
                .Col = ColUom
                .Text = IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)
                mItemUOM = IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)

                .Col = ColStockType
                .Text = IIf(Trim(.Text) = "", IIf(VB.Left(cboType.Text, 1) = "P", "ST", "CS"), Trim(.Text))

                mProdSeq = GetProductSeqNo(mItemCode, Trim(txtDept.Text), (txtPMemoDate.Text))
                If mProdSeq > 1 Then
                    mPrevDept = GetProductDept(mItemCode, mProdSeq - 1, (txtPMemoDate.Text))

                    ''GetBalanceStockQty(mItemCode, txtPMemoDate.Text, mItemUOM, Trim(mPrevDept), "ST", "", ConPH, mDivisionCode) +	
                    SprdMain.Col = ColPrevStockQty
                    SprdMain.Text = CStr(GetBalanceStockQty(mItemCode, (txtPMemoDate.Text), mItemUOM, Trim(mPrevDept), "ST", xFGBatchNo, ConPH, mDivisionCode, ConStockRefType_PMEMODEPT, Val(txtPMemoNo.Text), xFGBatchNoReq)) + CStr(GetBalanceStockQty(mItemCode, (txtPMemoDate.Text), mItemUOM, Trim(txtDept.Text), "WP", xFGBatchNo, ConPH, mDivisionCode, ConStockRefType_PMEMODEPT, Val(txtPMemoNo.Text), xFGBatchNoReq))


                    'GetBalanceStockQty(mItemCode, txtPMemoDate.Text, mItemUOM, Trim(mPrevDept), "ST", "X", ConPH, mDivisionCode, ConStockRefType_PMEMODEPT, Val(txtPMemoNo.Text))	
                    If lblBookType.Text = "J" Then      '' GetProductionType(mItemCode) = "J" Then
                        SprdMain.Text = CStr(Val(SprdMain.Text) + GetBalanceStockQty(mItemCode, (txtPMemoDate.Text), mItemUOM, Trim(mPrevDept), "CS", "", ConPH, mDivisionCode))
                    End If
                Else
                    SprdMain.Col = ColPrevStockQty
                    SprdMain.Text = "0.00"
                End If

                SprdMain.Col = ColStockQty ''mRecvQty +	
                SprdMain.Text = CStr(GetBalanceStockQty(mItemCode, (txtPMemoDate.Text), mItemUOM, Trim(txtDept.Text), "ST", xFGBatchNo, ConPH, mDivisionCode, , , xFGBatchNoReq)) '', ConStockRefType_PMEMODEPT, Val(lblMKey.text))	
                If lblBookType.Text = "J" Then      ''If GetProductionType(mItemCode) = "J" Then
                    SprdMain.Text = CStr(Val(SprdMain.Text) + GetBalanceStockQty(mItemCode, (txtPMemoDate.Text), mItemUOM, Trim(txtDept.Text), "CS", "", ConPH, mDivisionCode))
                End If

                If pOprCode <> "" Then
                    SprdMain.Col = ColOPR
                    SprdMain.Text = pOprCode
                End If

                FillItemDescPart = True
            End With
        Else
            MsgInformation("Invalid Item Code.")
            FillItemDescPart = False
        End If
        Exit Function
ERR1:
        FillItemDescPart = False
        MsgInformation(Err.Description)
    End Function

    Private Function FillStock(ByRef pRow As Integer, ByRef pItemCode As String, ByRef pBatchNo As String) As Boolean
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCategoryCode As String
        Dim mStockType As String
        Dim mItemCode As String
        Dim mItemUOM As String
        Dim mProdSeq As Integer
        Dim mPrevDept As String
        Dim mDivisionCode As Double
        Dim xFGBatchNoReq As String
        Dim xFGBatchNo As String

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If


        FillStock = False
        If Trim(pItemCode) = "" Then FillStock = True : Exit Function
        If Val(pBatchNo) <= 0 Then FillStock = True : Exit Function

        mProdSeq = GetProductSeqNo(mItemCode, Trim(txtDept.Text), (txtPMemoDate.Text))
        If mProdSeq <= 1 Then FillStock = True : Exit Function

        With SprdMain
            .Row = pRow

            If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DSP_RPT_FLAG='Y'") = True Then
                xFGBatchNoReq = "Y"
                .Col = ColBatchNo
                xFGBatchNo = Trim(.Text)
            Else
                xFGBatchNoReq = "N"
                xFGBatchNo = "X"
            End If


            .Col = ColItemDesc
            .Text = IIf(IsDBNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value)

            .Col = ColUom
            mItemUOM = Trim(.Text)


            If mProdSeq > 1 Then
                mPrevDept = GetProductDept(mItemCode, mProdSeq - 1, (txtPMemoDate.Text))
                SprdMain.Col = ColPrevStockQty
                SprdMain.Text = CStr(GetBalanceStockQty(mItemCode, (txtPMemoDate.Text), mItemUOM, Trim(txtDept.Text), "WP", xFGBatchNo, ConPH, mDivisionCode, xFGBatchNoReq))

                If GetProductionType(mItemCode) = "J" Then
                    SprdMain.Text = CStr(Val(SprdMain.Text) + GetBalanceStockQty(mItemCode, (txtPMemoDate.Text), mItemUOM, Trim(mPrevDept), "CS", "", ConPH, mDivisionCode))
                End If
            Else
                SprdMain.Col = ColPrevStockQty
                SprdMain.Text = "0.00"
            End If

            SprdMain.Col = ColStockQty ''mRecvQty +	
            SprdMain.Text = CStr(GetBalanceStockQty(mItemCode, (txtPMemoDate.Text), mItemUOM, Trim(txtDept.Text), "ST", xFGBatchNo, ConPH, mDivisionCode, xFGBatchNoReq)) '', ConStockRefType_PMEMODEPT, Val(lblMKey.text))	
            If GetProductionType(mItemCode) = "J" Then
                SprdMain.Text = CStr(Val(SprdMain.Text) + GetBalanceStockQty(mItemCode, (txtPMemoDate.Text), mItemUOM, Trim(txtDept.Text), "CS", "", ConPH, mDivisionCode))
            End If

            FillStock = True
        End With
        Exit Function
ERR1:
        FillStock = False
        MsgInformation(Err.Description)
    End Function
    Private Sub SprdView_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdView.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        With SprdView
            .Row = eventArgs.row
            .Col = 1
            txtPMemoNo.Text = .Text
            txtPMemoNo_Validating(txtPMemoNo, New System.ComponentModel.CancelEventArgs(False))
            If txtPMemoNo.Enabled = True Then txtPMemoNo.Focus()
            CmdView_Click(CmdView, New System.EventArgs())
        End With
    End Sub
    Private Function AutoGenMemoNo() As String

        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset = Nothing
        Dim mAutoGen As Double
        Dim SqlStr As String = ""
        Dim mMaxValue As String
        mAutoGen = 1
        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_REF)  " & vbCrLf & " FROM PRD_PMEMODEPT_HDR  " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAutoGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsAutoGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mMaxValue = .Fields(0).Value
                    mAutoGen = CDbl(Mid(mMaxValue, 1, Len(mMaxValue) - 6))
                    mAutoGen = mAutoGen + 1
                Else
                    mAutoGen = 1
                End If
            End If
        End With

        AutoGenMemoNo = mAutoGen & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        RsAutoGen.Close()
        RsAutoGen = Nothing
        Exit Function
AutogenErr:
        MsgBox(Err.Description)
    End Function
    Private Function UpdateMain1() As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mPMemoNo As Double
        Dim mEntryDate As String
        Dim mIsSPD As String
        Dim pErrorDesc As String
        'Dim RsTemp As ADODB.Recordset = Nothing	

        Dim mDivisionCode As Double
        Dim mApproved As String


        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mEntryDate = VB6.Format(PubCurrDate, "DD-MMM-YYYY") & " " & GetServerTime()
        mIsSPD = IIf(chkSPD.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mApproved = IIf(chkApproved.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        SqlStr = ""
        mPMemoNo = Val(txtPMemoNo.Text)
        If Val(txtPMemoNo.Text) = 0 Then
            mPMemoNo = CDbl(AutoGenMemoNo())
        End If
        txtPMemoNo.Text = CStr(mPMemoNo)

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If

        If MODIFYMode = True Then
            If InsertIntoDelAudit(PubDBCn, "PRD_PMEMODEPT_HDR", (txtPMemoNo.Text), RsPMemoMain, "AUTO_KEY_REF", "M") = False Then GoTo ErrPart
            If InsertIntoDelAudit(PubDBCn, "PRD_PMEMODEPT_DET", (txtPMemoNo.Text), RsPMemoDetail, "AUTO_KEY_REF", "M") = False Then GoTo ErrPart
        End If

        If ADDMode = True Then
            lblMKey.Text = CStr(mPMemoNo)
            SqlStr = " INSERT INTO PRD_PMEMODEPT_HDR  " & vbCrLf _
                    & " (COMPANY_CODE,FYEAR,AUTO_KEY_REF," & vbCrLf _
                    & " REF_DATE, PREP_TIME, PROD_DATE, DEPT_CODE, SHIFT_CODE, LINE_NO, PROD_TYPE," & vbCrLf _
                    & " EMP_CODE, REMARKS, IS_SPD, BOOKTYPE,  " & vbCrLf _
                    & " ADDUSER,ADDDATE,MODUSER,MODDATE,DIV_CODE,IS_APPROVED) " & vbCrLf _
                    & " VALUES ( " & vbCrLf _
                    & " " & RsCompany.Fields("COMPANY_CODE").Value & "," & RsCompany.Fields("FYEAR").Value & "," & mPMemoNo & ", " & vbCrLf _
                    & " TO_DATE('" & VB6.Format(txtPMemoDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & txtRefTM.Text & "','HH24:MI'), " & vbCrLf _
                    & " TO_DATE('" & VB6.Format(txtProdDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtDept.Text) & "', " & vbCrLf _
                    & " '" & cboShiftcd.Text & "', '" & cboLineNo.Text & "'," & vbCrLf _
                    & " '" & VB.Left(cboType.Text, 1) & "'," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtEmp.Text) & "','" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                    & " '" & mIsSPD & "', '" & VB.Left(lblBookType.Text, 1) & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & mEntryDate & "','DD-MON-YYYY HH24:MI'),'',''," & mDivisionCode & ",'" & mApproved & "')"

        ElseIf MODIFYMode = True Then
            mEntryDate = VB6.Format(PubCurrDate, "DD-MMM-YYYY") & " " & GetServerTime()
            SqlStr = " UPDATE PRD_PMEMODEPT_HDR  SET " & vbCrLf _
                    & " AUTO_KEY_REF=" & mPMemoNo & ", " & vbCrLf _
                    & " REF_DATE=TO_DATE('" & VB6.Format(txtPMemoDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " PROD_DATE=TO_DATE('" & VB6.Format(txtProdDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " PREP_TIME=TO_DATE('" & txtRefTM.Text & "','HH24:MI')," & vbCrLf _
                    & " DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "', " & vbCrLf _
                    & " SHIFT_CODE='" & cboShiftcd.Text & "', " & vbCrLf _
                    & " LINE_NO='" & cboLineNo.Text & "', " & vbCrLf _
                    & " PROD_TYPE= '" & VB.Left(cboType.Text, 1) & "'," & vbCrLf _
                    & " EMP_CODE='" & MainClass.AllowSingleQuote(txtEmp.Text) & "', " & vbCrLf _
                    & " REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                    & " IS_SPD='" & mIsSPD & "', IS_APPROVED='" & mApproved & "'," & vbCrLf _
                    & " BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "'," & vbCrLf _
                    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                    & " MODDATE=TO_DATE('" & mEntryDate & "','DD-MON-YYYY HH24:MI'), DIV_CODE=" & mDivisionCode & "" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND AUTO_KEY_REF=" & Val(lblMKey.Text) & ""
        End If

        PubDBCn.Execute(SqlStr)
        If UpdateDetail1(pErrorDesc, mDivisionCode) = False Then GoTo ErrPart


        UpdateMain1 = True
        PubDBCn.CommitTrans()
        txtPMemoNo.Text = CStr(mPMemoNo)
        Exit Function
ErrPart:

        UpdateMain1 = False
        PubDBCn.RollbackTrans()
        If pErrorDesc <> "" Then
            MsgInformation(pErrorDesc)
        End If
        RsPMemoMain.Requery()
        RsPMemoDetail.Requery()
        If Trim(Err.Description) <> "" Then
            MsgBox(Err.Description)
        End If
        If ADDMode = True Then
            lblMKey.Text = ""
            txtPMemoNo.Text = ""
        End If
        '    Resume	
    End Function
    Private Function UpdateDetail1(ByRef pErrorDesc As String, ByRef mDivisionCode As Double) As Boolean

        On Error GoTo UpdateDetail1Err
        Dim SqlStr As String = ""
        Dim i As Integer
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mUOM As String
        Dim mStockType As String
        Dim mProdQty As Double
        Dim mReworkQty As Double
        Dim mScrapQty As Double
        Dim mCRWokQty As Double
        Dim mCostPcs As Double
        Dim xStockRowNo As Integer
        Dim xItemCost As Double
        Dim mInCCCode As String
        Dim mWIPStock As Double
        Dim mWIPReworkStock As Double
        Dim mSqlStr As String
        Dim RsBOM As ADODB.Recordset
        Dim mProductSeqNo As Integer
        Dim mNextProductDept As String
        Dim mProductionDate As String
        'Dim mEntryDate As String	
        Dim mReason As String
        Dim mToolNo As String
        Dim mTotalOpr As Integer
        Dim mTotalOpr_WOOptional As Integer
        Dim mOprSeq As Integer
        Dim xOPStockType As String
        Dim pOPRCode As String
        Dim pOPRDesc As String
        Dim pSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mOperatorCode As String
        Dim mIsOptionalOPR As String
        Dim mDept As String
        Dim xAutoProductionIssue As Boolean
        Dim xFGBatchNo As String
        Dim mProdCodeStockType As String

        Dim mCRQty As Double = 0
        Dim mMRQty As Double = 0
        Dim mMachineNo As String = ""
        Dim mBreakDownTime As Double = 0
        Dim mNoTool As Double = 0
        Dim mNoMaterial As Double = 0
        Dim mNoOperator As Double = 0
        Dim mPowerCutTime As Double = 0
        Dim mToolChangeTime As Double = 0
        Dim mSetupChangeTime As Double = 0
        Dim mQAIssue As Double = 0
        Dim mRemarks As String = ""
        Dim mMachineWorkingHours As Double = 0

        Dim mProdDate As String



        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
            mProdDate = txtProdDate.Text
        Else
            mProdDate = txtPMemoDate.Text
        End If

        If MainClass.ValidateWithMasterTable(txtDept.Text, "DEPT_CODE", "CCCODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mInCCCode = IIf(IsDBNull(MasterNo), "-1", MasterNo)
        Else
            mInCCCode = "-1"
        End If
        '	
        '    mProductionDate = Format(txtPMemoDate.Text, "DD/MM/YYYY")	
        '    If Left(cboShiftcd.Text, 1) = "C" Then	
        '        mEntryDate = DateAdd("d", 1, mProductionDate)	
        '    Else	
        '        mEntryDate = mProductionDate	
        '    End If	

        SqlStr = " DELETE FROM PRD_PMEMODEPT_DET " & vbCrLf & " WHERE AUTO_KEY_REF=" & Val(lblMKey.Text) & " "
        PubDBCn.Execute(SqlStr)

        If DeleteStockTRN(PubDBCn, ConStockRefType_PMEMODEPT, (txtPMemoNo.Text)) = False Then GoTo UpdateDetail1Err
        xStockRowNo = 1

        With SprdMain
            For i = 1 To .MaxRows - 1
                .Row = i

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColItemDesc
                mItemDesc = MainClass.AllowSingleQuote(.Text)

                .Col = ColUom
                mUOM = MainClass.AllowSingleQuote(.Text)

                .Col = ColProdQty
                mProdQty = Val(.Text)

                .Col = ColBreakageQty
                mScrapQty = Val(.Text)

                .Col = ColCRQty
                mReworkQty = 0
                mCRQty = Val(.Text)

                .Col = ColMRQty
                mCRWokQty = 0
                mMRQty = Val(.Text)

                .Col = ColStockType
                mStockType = IIf(VB.Left(cboType.Text, 1) = "P", "ST", "CS") ''MainClass.AllowSingleQuote(.Text)	

                .Col = ColToolNo
                mToolNo = Trim(.Text)

                .Col = ColReason
                mReason = Trim(.Text)

                .Col = ColCostPcs
                mCostPcs = Val(.Text)

                .Col = ColOPRERCode
                mOperatorCode = Trim(.Text)

                .Col = ColOPR
                pOPRDesc = Trim(.Text)
                If Trim(pOPRDesc) = "" Then
                    pOPRCode = ""
                Else
                    pSqlStr = " SELECT OPR_CODE " & vbCrLf _
                        & " FROM PRD_OPR_MST MST" & vbCrLf _
                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'" & vbCrLf _
                        & " AND OPR_DESC='" & MainClass.AllowSingleQuote(pOPRDesc) & "'"

                    MainClass.UOpenRecordSet(pSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                    If RsTemp.EOF = False Then
                        pOPRCode = IIf(IsDBNull(RsTemp.Fields("OPR_CODE").Value), "", RsTemp.Fields("OPR_CODE").Value)
                    Else
                        pOPRCode = ""
                    End If
                End If

                .Col = ColMachineNo
                mMachineNo = Trim(.Text)

                .Col = ColMachineWorkingHours
                mMachineWorkingHours = Val(.Text)

                .Col = ColBreakDownTime
                mBreakDownTime = Val(.Text)

                .Col = ColNoTool
                mNoTool = Val(.Text) ''IIf(SprdMain.Value = System.Windows.Forms.CheckState.Checked, "Y", "N")

                .Col = ColNoMaterial
                mNoMaterial = Val(.Text) ''IIf(SprdMain.Value = System.Windows.Forms.CheckState.Checked, "Y", "N")

                .Col = ColNoOperator
                mNoOperator = Val(.Text) ''IIf(SprdMain.Value = System.Windows.Forms.CheckState.Checked, "Y", "N")

                .Col = ColPowerCutTime
                mPowerCutTime = Val(.Text)

                .Col = ColToolChangeTime
                mToolChangeTime = Val(.Text)

                .Col = ColSetupChangeTime
                mSetupChangeTime = Val(.Text)

                .Col = ColQAIssue
                mQAIssue = Val(.Text) ' IIf(SprdMain.Value = System.Windows.Forms.CheckState.Checked, "Y", "N")

                .Col = ColRemarks
                mRemarks = Trim(.Text)

                SqlStr = ""

                If mItemCode <> "" And (mProdQty + mReworkQty + mCRWokQty > 0 Or chkApproved.CheckState = System.Windows.Forms.CheckState.Unchecked) Then

                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DSP_RPT_FLAG='Y'") = True Then
                        .Col = ColBatchNo
                        '                    If ADDMode = True Then	
                        '                        xFGBatchNo = GenerateBatchNo(Format(txtProdDate, "DD/MM/YYYY"), Trim(cboShiftcd.Text), Trim(cboLineNo.Text))     ''Format(txtPMemoDate, "DDMMYYYY")	
                        '                    Else	
                        If Trim(.Text) <> "" Then
                            xFGBatchNo = Trim(.Text)
                        Else
                            xFGBatchNo = GenerateBatchNo(VB6.Format(txtProdDate.Text, "DD/MM/YYYY"), Trim(cboShiftcd.Text), Trim(cboLineNo.Text)) ''Format(txtPMemoDate, "DDMMYYYY")	
                        End If
                        '                    End If	
                    Else
                        xFGBatchNo = ""
                    End If

                    SqlStr = " INSERT INTO PRD_PMEMODEPT_DET ( " & vbCrLf _
                        & " COMPANY_CODE,AUTO_KEY_REF,SERIAL_NO,ITEM_CODE,ITEM_DESC, " & vbCrLf _
                        & " ITEM_UOM,STOCK_TYPE, PROD_QTY, REWORK_QTY, MR_QTY, COST_PCS, REASON, " & vbCrLf _
                        & " TOOL_NO, OPR_CODE, OPERATOR_CODE, BATCH_NO," & vbCrLf _
                        & " MACHINE_NO, BREAKDOWN_TIME, NO_TOOL, NO_MATERIAL, " & vbCrLf _
                        & " NO_OPERATOR, POWER_CUT_TIME, TOOL_CHANGE_TIME, SETUP_CHANGE_TIME, " & vbCrLf _
                        & " QA_ISSUE, REMARKS, SCRAP_QTY, MACHINE_WORKING_HOURS" & vbCrLf _
                        & " ) " & vbCrLf _
                        & " VALUES (" & RsCompany.Fields("COMPANY_CODE").Value & "," & Val(lblMKey.Text) & ", " & i & "," & vbCrLf _
                        & " '" & mItemCode & "','" & mItemDesc & "', " & vbCrLf & " '" & mUOM & "','" & mStockType & "', " & vbCrLf _
                        & " " & mProdQty & ", " & mCRQty & ",  " & mMRQty & "," & vbCrLf _
                        & " " & mCostPcs & ",'" & MainClass.AllowSingleQuote(mReason) & "'," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mToolNo) & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(pOPRCode) & "', '" & MainClass.AllowSingleQuote(mOperatorCode) & "'," & vbCrLf _
                        & " '" & xFGBatchNo & "'," & vbCrLf _
                        & " '" & mMachineNo & "', " & mBreakDownTime & ", " & mNoTool & ", " & mNoMaterial & ", " & vbCrLf _
                        & " " & mNoOperator & ", " & mPowerCutTime & ", " & mToolChangeTime & ", " & mSetupChangeTime & ", " & vbCrLf _
                        & " " & mQAIssue & ", '" & MainClass.AllowSingleQuote(mRemarks) & "'," & mScrapQty & "," & mMachineWorkingHours & " " & vbCrLf _
                        & " )"


                    PubDBCn.Execute(SqlStr)



                    'If RsCompany.Fields("COMPANY_CODE").Value = 9 Then
                    '    If chkApproved.CheckState = System.Windows.Forms.CheckState.Unchecked Then GoTo NextRec
                    'Else
                    If lblBookType.Text = "P" Or lblBookType.Text = "J" Then
                        mProductSeqNo = GetProductSeqNo(mItemCode, Trim(txtDept.Text), (mProdDate))
                        mNextProductDept = GetProductDept(mItemCode, mProductSeqNo + 1, (mProdDate))
                        If mProductSeqNo = 0 Then
                            '                    MsgBox "Product Sequence Not Defined. Item Code :" & mItemCode	
                            pErrorDesc = "Product Sequence Not Defined. Item Code :" & mItemCode
                            UpdateDetail1 = False
                            Exit Function
                        End If
                    Else
                        mProductSeqNo = 1
                        mNextProductDept = ""
                    End If

                    If chkApproved.CheckState = System.Windows.Forms.CheckState.Unchecked Then GoTo NextRec

                    mTotalOpr = GetTotalOperation(mItemCode, Trim(txtDept.Text), pOPRCode, (mProdDate))
                    mTotalOpr_WOOptional = GetTotalOperation(mItemCode, Trim(txtDept.Text), pOPRCode, (mProdDate), "N")


                    mOprSeq = GetOperationSeq(mItemCode, Trim(txtDept.Text), pOPRCode, (mProdDate))

                    mSqlStr = MakeBOMStockQty(mItemCode, (txtDept.Text), mOprSeq, pOPRCode)
                    If mSqlStr = "" Then
                        '                    MsgInformation "Cann't Saved"	
                        pErrorDesc = "Cann't Saved"
                        UpdateDetail1 = False
                        Exit Function
                    Else
                        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBOM, ADODB.LockTypeEnum.adLockReadOnly)
                    End If

                    '                    If RsBOM.EOF = True And lblBookType.text = "P" Then	
                    '                        pErrorDesc = "B.O.M. is Not Defined."	
                    '                        UpdateDetail1 = False	
                    '                        Exit Function	
                    '                    End If	



                    If RsBOM.EOF = False And chkSPD.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                        If lblBookType.Text = "P" Or lblBookType.Text = "J" Then
                            If mOprSeq > 1 And pOPRCode = "" Then
                                ''Not Updated...	
                            Else
                                If UpdateBOMStock(pErrorDesc, RsBOM, mItemCode, mProdQty, xStockRowNo, xStockRowNo, xItemCost, mInCCCode, mInCCCode, mDivisionCode, xFGBatchNo) = False Then GoTo UpdateDetail1Err
                            End If
                        End If
                    End If
                    ''19-11-2007	
                    If mOprSeq > 1 Then
                        If mProductSeqNo = 1 Then
                            xOPStockType = VB6.Format(mOprSeq - 1, "00")
                            mIsOptionalOPR = GetIsOptionalOPR(mItemCode, Trim(txtDept.Text), xOPStockType, (mProdDate))

                            If mIsOptionalOPR = "Y" Then GoTo NextStep
                            mWIPStock = GetWIPStockQty(mItemCode, VB6.Format(mProdDate, "DD/MM/YYYY"), mUOM, Trim(txtDept.Text), xOPStockType, xFGBatchNo, "PH", mDivisionCode)

                            '                            If PubUserID <> "G0416" Then  ''03/01/2015	
                            If mWIPStock < mProdQty Then
                                '                            MsgInformation "You have Not Enough WIP Stock For Finished Goods " & mItemCode & vbNewLine & "(Bal. Qty : " & mWIPStock & "))." & vbNewLine & " Cann't Save."	
                                pErrorDesc = "You have Not Enough WIP Stock For Finished Goods " & mItemCode & vbNewLine & "(Bal. Qty : " & mWIPStock & "))." & vbNewLine & " Cann't Save."
                                UpdateDetail1 = False
                                Exit Function
                            End If
                            '                            End If	

                            xStockRowNo = xStockRowNo + 1
                            If UpdateStockTRN(PubDBCn, ConStockRefType_PMEMODEPT, (txtPMemoNo.Text), xStockRowNo, (mProdDate), (mProdDate), xOPStockType, mItemCode, mUOM, xFGBatchNo, mProdQty, 0, "O", xItemCost, xItemCost, pOPRCode, "", (txtDept.Text), (txtDept.Text), mInCCCode, "N", "From : " & lblDept.Text & "  : (Production) -" & ConStockRefType_PMEMODEPT, "-1", ConPH, mDivisionCode, "", "") = False Then GoTo UpdateDetail1Err
                        End If
                    End If
NextStep:

                    If mProductSeqNo > 1 Then
                        If mOprSeq = 1 Then
                            xAutoProductionIssue = CheckAutoIssueProd((txtProdDate.Text), mItemCode)
                            If xAutoProductionIssue = False Then
                                xOPStockType = "WP"
                                mDept = Trim(txtDept.Text)
                            Else
                                xOPStockType = IIf(GetProductionType(mItemCode) = "J", "CS", "ST")
                                mDept = GetProductDept(mItemCode, mProductSeqNo - 1, (mProdDate))
                            End If
                        Else
                            xOPStockType = VB6.Format(mOprSeq - 1, "00")
                            mDept = Trim(txtDept.Text)
                            mIsOptionalOPR = GetIsOptionalOPR(mItemCode, Trim(txtDept.Text), xOPStockType, (mProdDate))

                            If mIsOptionalOPR = "Y" Then GoTo NextStep1
                        End If

                        mWIPStock = GetWIPStockQty(mItemCode, VB6.Format(mProdDate, "DD/MM/YYYY"), mUOM, mDept, xOPStockType, xFGBatchNo, "PH", mDivisionCode)

                        '                        If PubUserID <> "G0416" Then  ''03/01/2015	
                        If mWIPStock < mProdQty Then
                            pErrorDesc = "You have Not Enough WIP Stock For Finished Goods " & mItemCode & vbNewLine & "(Bal. Qty : " & mWIPStock & "))." & vbNewLine & " Cann't Save."
                            UpdateDetail1 = False
                            Exit Function
                        End If
                        '                        End If	

                        xStockRowNo = xStockRowNo + 1

                        If UpdateStockTRN(PubDBCn, ConStockRefType_PMEMODEPT, (txtPMemoNo.Text), xStockRowNo, (mProdDate), (mProdDate), xOPStockType, mItemCode, mUOM, xFGBatchNo, mProdQty, 0, "O", xItemCost, xItemCost, pOPRCode, "", mDept, mDept, mInCCCode, "N", "From : " & lblDept.Text & "  : (Production) -" & ConStockRefType_PMEMODEPT, "-1", ConPH, mDivisionCode, "", "") = False Then GoTo UpdateDetail1Err


                    End If
NextStep1:
                    If mReworkQty > 0 And mTotalOpr_WOOptional = mOprSeq Then '' 13-06-2009 ''mTotalOpr = mOprSeq Then	
                        mWIPReworkStock = GetWIPStockQty(mItemCode, VB6.Format(mProdDate, "DD/MM/YYYY"), mUOM, Trim(txtDept.Text), "WR", xFGBatchNo, "PH", mDivisionCode)
                        If mWIPReworkStock < mReworkQty Then
                            pErrorDesc = "You have Not Enough Rework Stock For Finished Goods " & mItemCode & vbNewLine & "(Bal. Qty : " & mWIPReworkStock & "))." & vbNewLine & " Cann't Save."
                            UpdateDetail1 = False
                            Exit Function
                        End If

                        xStockRowNo = xStockRowNo + 1
                        If UpdateStockTRN(PubDBCn, ConStockRefType_PMEMODEPT, (txtPMemoNo.Text), xStockRowNo, (mProdDate), (mProdDate), "WR", mItemCode, mUOM, xFGBatchNo, mReworkQty, 0, "O", xItemCost, xItemCost, pOPRCode, "", (txtDept.Text), (txtDept.Text), mInCCCode, "N", "From : " & lblDept.Text & "  : (Production) -" & ConStockRefType_PMEMODEPT, "-1", ConPH, mDivisionCode, "", "") = False Then GoTo UpdateDetail1Err
                    ElseIf mReworkQty > 0 And mTotalOpr_WOOptional <> mOprSeq Then  ''13-06-2009 mTotalOpr <> mOprSeq Then	
                        pErrorDesc = "Please Entered Rework Qty in Final Operation for Finished Goods " & mItemCode & "." & vbNewLine & " Cann't Save."
                        UpdateDetail1 = False
                        Exit Function
                    End If

                    If mCRWokQty > 0 And mTotalOpr_WOOptional = mOprSeq Then '' 13-06-2009 ''mTotalOpr = mOprSeq Then	
                        mWIPReworkStock = GetWIPStockQty(mItemCode, VB6.Format(mProdDate, "DD/MM/YYYY"), mUOM, Trim(txtDept.Text), "CR", xFGBatchNo, "PH", mDivisionCode)
                        If mWIPReworkStock < mCRWokQty Then
                            '                            MsgInformation "You have Not Enough Rework Stock For Finished Goods " & mItemCode & vbNewLine & "(Bal. Qty : " & mWIPReworkStock & "))." & vbNewLine & " Cann't Save."	
                            pErrorDesc = "You have Not Enough Customer Rejection Stock For Finished Goods " & mItemCode & vbNewLine & "(Bal. Qty : " & mWIPReworkStock & "))." & vbNewLine & " Cann't Save."
                            UpdateDetail1 = False
                            Exit Function
                        End If

                        xStockRowNo = xStockRowNo + 1
                        If UpdateStockTRN(PubDBCn, ConStockRefType_PMEMODEPT, (txtPMemoNo.Text), xStockRowNo, (mProdDate), (mProdDate), "CR", mItemCode, mUOM, CStr(-1), mCRWokQty, 0, "O", xItemCost, xItemCost, pOPRCode, "", (txtDept.Text), (txtDept.Text), mInCCCode, "N", "From : " & lblDept.Text & "  : (Production) -" & ConStockRefType_PMEMODEPT, "-1", ConPH, mDivisionCode, "", "") = False Then GoTo UpdateDetail1Err
                    ElseIf mCRWokQty > 0 And mTotalOpr_WOOptional <> mOprSeq Then  ''13-06-2009 mTotalOpr <> mOprSeq Then	
                        pErrorDesc = "Please Entered Customer Rejection Qty in Final Operation for Finished Goods " & mItemCode & "." & vbNewLine & " Cann't Save."
                        UpdateDetail1 = False
                        Exit Function
                    End If
                End If
                xStockRowNo = xStockRowNo + 1

                If mTotalOpr_WOOptional = mOprSeq Then '' 13-06-2009 '' mTotalOpr = mOprSeq Then	
                    If Mid(cboType.Text, 1, 1) = "J" Then '' If GetProductionType(mItemCode) = "J" Then ''VB.Left(cboType.Text, 1)
                        xOPStockType = "CS"
                    Else
                        xOPStockType = "ST"
                    End If
                Else
                    xOPStockType = VB6.Format(mOprSeq, "00")
                    mIsOptionalOPR = GetIsOptionalOPR(mItemCode, Trim(txtDept.Text), xOPStockType, (mProdDate))
                    If mIsOptionalOPR = "Y" Then GoTo NextStep2
                End If


                If UpdateStockTRN(PubDBCn, ConStockRefType_PMEMODEPT, (txtPMemoNo.Text), xStockRowNo, (mProdDate), (mProdDate), xOPStockType, mItemCode, mUOM, xFGBatchNo,
                                      mProdQty + mReworkQty + mCRWokQty - mMRQty - mScrapQty, 0, "I", xItemCost, xItemCost, pOPRCode, "", (txtDept.Text), (txtDept.Text),
                                      mInCCCode, "N", "From : " & lblDept.Text & "  : (Production) -" & ConStockRefType_PMEMODEPT, "-1", ConPH, mDivisionCode, "", "") = False Then GoTo UpdateDetail1Err


                xStockRowNo = xStockRowNo + 1

                If mScrapQty > 0 Then
                    If UpdateStockTRN(PubDBCn, ConStockRefType_PMEMODEPT, (txtPMemoNo.Text), xStockRowNo, (mProdDate), (mProdDate), "SC", mItemCode, mUOM, xFGBatchNo,
                                      mScrapQty, 0, "I", xItemCost, xItemCost, pOPRCode, "", (txtDept.Text), (txtDept.Text),
                                      mInCCCode, "N", "From : " & lblDept.Text & "  : (Production) -" & ConStockRefType_PMEMODEPT, "-1", ConPH, mDivisionCode, "", "") = False Then GoTo UpdateDetail1Err

                End If

                mProdCodeStockType = GetStockType(PubDBCn, mItemCode, mDivisionCode)

                If Trim(txtDept.Text) = "PDI" And xOPStockType = "ST" And mProdCodeStockType = "FG" Then
                    xStockRowNo = xStockRowNo + 1

                    If UpdateStockTRN(PubDBCn, ConStockRefType_PMEMODEPT, (txtPMemoNo.Text), xStockRowNo, (mProdDate), (mProdDate), xOPStockType, mItemCode, mUOM, xFGBatchNo,
                                      mProdQty + mReworkQty + mCRWokQty - mMRQty - mScrapQty, 0, "O", xItemCost, xItemCost, pOPRCode, "", (txtDept.Text), (txtDept.Text),
                                      mInCCCode, "N", "From : " & lblDept.Text & "  To : FG Store " & ConStockRefType_PMEMODEPT, "-1", ConPH, mDivisionCode, "", "") = False Then GoTo UpdateDetail1Err

                    xStockRowNo = xStockRowNo + 1

                    If UpdateStockTRN(PubDBCn, ConStockRefType_PMEMODEPT, (txtPMemoNo.Text), xStockRowNo, (mProdDate), (mProdDate), "FG", mItemCode, mUOM, xFGBatchNo,
                                      mProdQty + mReworkQty + mCRWokQty - mMRQty - mScrapQty, 0, "I", xItemCost, xItemCost, pOPRCode, "", "STR", txtDept.Text,
                                      mInCCCode, "N", "From : " & lblDept.Text & "  To : FG Store " & ConStockRefType_PMEMODEPT, "-1", ConWH, mDivisionCode, "", "") = False Then GoTo UpdateDetail1Err

                End If

                If mNextProductDept = "STR" And xOPStockType = "ST" Then
                    xStockRowNo = xStockRowNo + 1

                    If UpdateStockTRN(PubDBCn, ConStockRefType_PMEMODEPT, (txtPMemoNo.Text), xStockRowNo, (mProdDate), (mProdDate), xOPStockType, mItemCode, mUOM, xFGBatchNo,
                                      mProdQty + mReworkQty + mCRWokQty - mMRQty - mScrapQty, 0, "O", xItemCost, xItemCost, pOPRCode, "", (txtDept.Text), (txtDept.Text),
                                      mInCCCode, "N", "From : " & lblDept.Text & "  To : Store " & ConStockRefType_PMEMODEPT, "-1", ConPH, mDivisionCode, "", "") = False Then GoTo UpdateDetail1Err

                    xStockRowNo = xStockRowNo + 1

                    If UpdateStockTRN(PubDBCn, ConStockRefType_PMEMODEPT, (txtPMemoNo.Text), xStockRowNo, (mProdDate), (mProdDate), "ST", mItemCode, mUOM, xFGBatchNo,
                                      mProdQty + mReworkQty + mCRWokQty - mMRQty - mScrapQty, 0, "I", xItemCost, xItemCost, pOPRCode, "", "STR", txtDept.Text,
                                      mInCCCode, "N", "From : " & lblDept.Text & "  To : Store " & ConStockRefType_PMEMODEPT, "-1", ConWH, mDivisionCode, "", "") = False Then GoTo UpdateDetail1Err

                End If


NextStep2:
                'End If
NextRec:

            Next
        End With
        pErrorDesc = ""
        UpdateDetail1 = True
        Exit Function
UpdateDetail1Err:
        '    Resume	
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume	
    End Function
    Private Function UpdateBOMStock(ByRef pErrorDesc As String, ByRef pRsBOM As ADODB.Recordset, ByRef mFICode As String, ByRef mFQty As Double, ByRef mStockRowNo As Integer, ByRef mRetStockRowNo As Integer, ByRef mRetItemCost As Double, ByRef pInCCCode As String, ByRef pOutCCCode As String, ByRef mDivisionCode As Double, ByRef xFGBatchNo As String) As Boolean

        On Error GoTo BOMStockErr
        Dim SqlStr As String = ""
        Dim mStdQty As Double
        Dim mRMGrossQtyGram As Double
        Dim mRMGrossQtyKg As Double
        Dim mRMCostKg As Double
        Dim mScrpGrossQtyGram As Double
        Dim mScrpGrossQtyKg As Double
        Dim mScrpCostKg As Double
        Dim mSUOM As String
        Dim mProductionQty As Double
        Dim xProductionQty As Double
        Dim mStockQty As Double
        Dim mTotStockQty As Double
        Dim mScrapCode As String
        Dim mRMCode As String
        Dim mRMCodeStr As String
        Dim mRMUOM As String

        Dim pSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mMKEY As String
        Dim mBalFQty As Double
        Dim mUsedRMQty As Double
        Dim mUsedSFQty As Double
        Dim mStockType As String
        Dim mFromScrap As String
        Dim mUsedScrap As Double
        Dim xWareHouse As String
        Dim mISProd As Boolean
        Dim mIsInHouse As Boolean
        Dim mDeptCode As String
        Dim mOutputQty As Double
        Dim xFOutputQty As Double
        Dim mProducType As String

        Dim pFGBatchNo As String
        Dim xFGBatchNoReq As String
        Dim mProd_Type As String

        Dim mProdDate As String



        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
            mProdDate = txtProdDate.Text
        Else
            mProdDate = txtPMemoDate.Text
        End If

        If RsCompany.Fields("CHECK_BOP_STOCK").Value = "N" Then
            UpdateBOMStock = True
            Exit Function
        End If

        xFOutputQty = mFQty
        With pRsBOM
            mOutputQty = IIf(IsDBNull(.Fields("OUTPUT_QTY").Value) Or .Fields("OUTPUT_QTY").Value = 0, 1, .Fields("OUTPUT_QTY").Value)
            'If RsCompany.Fields("COMPANY_CODE").Value = 21 Or RsCompany.Fields("COMPANY_CODE").Value = 32 Then
            '    xFOutputQty = xFOutputQty / mOutputQty
            '    If xFOutputQty <> Int(xFOutputQty) Then
            '        MsgInformation("Please Enter the Qty in multiple of " & mOutputQty & ".")
            '        UpdateBOMStock = False
            '        Exit Function
            '    End If
            'End If

            '        mRetItemCost = Val(IIf(IsNull(!FINAL_COST), "", !FINAL_COST))	
            Do While Not .EOF
                mRMCode = Trim(IIf(IsDBNull(.Fields("RM_CODE").Value), "", .Fields("RM_CODE").Value))
                mDeptCode = Trim(txtDept.Text)
                If IsFGItem(mRMCode) = True Then
                    xWareHouse = ConPH
                Else
                    If CheckAutoIssue(VB6.Format(txtPMemoDate.Text, "DD/MM/YYYY"), mRMCode) = False Then ''RsCompany!AUTO_ISSUE = "N"	
                        xWareHouse = "PH"
                        mIsInHouse = IsInHouseItem(mRMCode)
                        'If RsCompany.Fields("COMPANY_CODE").Value = 32 Or RsCompany.Fields("COMPANY_CODE").Value = 1 Or RsCompany.Fields("COMPANY_CODE").Value = 34 Or RsCompany.Fields("COMPANY_CODE").Value = 42 Or RsCompany.Fields("COMPANY_CODE").Value = 43 Then ''02/07/2016	
                        If mIsInHouse = True And CheckAutoIssueProd(VB6.Format(txtPMemoDate.Text, "DD/MM/YYYY"), mRMCode) = True Then
                            mDeptCode = GetProductFinalDept(mRMCode, (txtPMemoDate.Text))
                            If GetDeptType(mDeptCode) = "3" Then
                                mDeptCode = Trim(txtDept.Text)
                                xWareHouse = "PH"
                            Else
                                If mDeptCode = "STR" Or mDeptCode = "" Then
                                    mDeptCode = Trim(txtDept.Text)
                                    xWareHouse = "WH"
                                End If
                            End If
                        End If
                        'End If
                    Else
                        mISProd = IsProductionItem(mRMCode)
                        If mISProd = True Then
                            If CDate(VB6.Format(txtPMemoDate.Text, "DD/MM/YYYY")) < CDate(VB6.Format(RsCompany.Fields("AUTO_ISSUE_DATE").Value, "DD/MM/YYYY")) Then
                                xWareHouse = "PH"
                            Else
                                mIsInHouse = IsInHouseItem(mRMCode)
                                If mIsInHouse = True Then
                                    mDeptCode = GetProductFinalDept(mRMCode, (txtPMemoDate.Text))
                                    If GetDeptType(mDeptCode) = "3" Then
                                        mDeptCode = Trim(txtDept.Text)
                                        xWareHouse = "PH"
                                    Else
                                        If mDeptCode = "STR" Or mDeptCode = "" Then
                                            mDeptCode = "STR"
                                            xWareHouse = "WH"
                                        Else
                                            xWareHouse = "PH"
                                        End If
                                    End If
                                Else
                                    xWareHouse = "WH"
                                End If
                            End If
                        Else
                            xWareHouse = "PH"
                        End If
                    End If
                End If

                mMKEY = IIf(IsDBNull(.Fields("mKey").Value), "", .Fields("mKey").Value)
                mFromScrap = IIf(IsDBNull(.Fields("FROM_SCRAP").Value), "N", .Fields("FROM_SCRAP").Value)
                '            mStockType = IIf(Left(cboType.Text, 1) = "P", "ST", "CS")	

                mProducType = GetProductionType(mRMCode)
                If Mid(cboType.Text, 1, 1) = "J" Then
                    mStockType = "CS" '' IIf(mFromScrap = "Y", "SC", mStockType)	
                Else
                    mStockType = IIf(IsDBNull(.Fields("STOCK_TYPE").Value), "ST", .Fields("STOCK_TYPE").Value)
                End If

                mStdQty = Val(IIf(IsDBNull(.Fields("STD_QTY").Value), "", .Fields("STD_QTY").Value)) + Val(IIf(IsDBNull(.Fields("GROSS_WT_SCRAP").Value), "", .Fields("GROSS_WT_SCRAP").Value))
                mRMCodeStr = mRMCode
                mRMUOM = IIf(IsDBNull(.Fields("ISSUE_UOM").Value), "", .Fields("ISSUE_UOM").Value)
                mScrapCode = IIf(IsDBNull(.Fields("SCRAP_ITEM_CODE").Value), "", .Fields("SCRAP_ITEM_CODE").Value)

                If UCase(Trim(mRMUOM)) = "TON" Then
                    mProductionQty = Val(CStr((mStdQty * xFOutputQty) / 1000))
                    mProductionQty = Val(CStr(mProductionQty / 1000))
                ElseIf UCase(Trim(mRMUOM)) = "KGS" Then
                    mProductionQty = Val(CStr(mStdQty / 1000)) * xFOutputQty
                Else
                    mProductionQty = mStdQty * xFOutputQty
                End If

                '            If RsCompany.fields("COMPANY_CODE").value = 21 Then	
                '                mProductionQty = mProductionQty / mOutputQty	
                '            End If	

                mScrpGrossQtyGram = Val(IIf(IsDBNull(.Fields("GROSS_WT_SCRAP").Value), "", .Fields("GROSS_WT_SCRAP").Value))
                mScrpGrossQtyKg = Val(CStr(mScrpGrossQtyGram / 1000))
                mScrpGrossQtyKg = Val(CStr(mScrpGrossQtyKg * xFOutputQty))


                If mStdQty > 0 Then
                    If CheckRMStock(pErrorDesc, mMKEY, mFICode, mRMCode, mRMUOM, mDeptCode, xFOutputQty, mStdQty, mProductionQty, mStockType, xWareHouse, mDivisionCode, xFGBatchNo) = False Then
                        '                    If PubUserID <> "G0416" Then  ''03/01/2015	
                        UpdateBOMStock = False
                        Exit Function
                        '                    End If	
                    End If
                End If

                If MainClass.ValidateWithMasterTable(mRMCode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DSP_RPT_FLAG='Y'") = True Then
                    mProd_Type = GetProductionType(mRMCode)
                    If mProd_Type = "I" Or mProd_Type = "P" Then
                        pFGBatchNo = xFGBatchNo
                        xFGBatchNoReq = "Y"
                    Else
                        pFGBatchNo = "X"
                        xFGBatchNoReq = "N"
                    End If
                Else
                    pFGBatchNo = "X"
                    xFGBatchNoReq = "N"
                End If

                mStockRowNo = mStockRowNo + 1
                mStockQty = GetBalanceStockQty(mRMCode, (txtPMemoDate.Text), mRMUOM, mDeptCode, mStockType, pFGBatchNo, xWareHouse, mDivisionCode, , , xFGBatchNoReq)

                If mStockQty <= 0 Then mStockQty = 0
                If UCase(Trim(mRMUOM)) = "TON" Then
                    mUsedSFQty = Val(CStr(mStockQty * 1000 * 1000))
                ElseIf UCase(Trim(mRMUOM)) = "KGS" Then
                    mUsedSFQty = Val(CStr(mStockQty * 1000))
                Else
                    mUsedSFQty = mStockQty
                End If

                mUsedSFQty = Int(mUsedSFQty / mStdQty)
                If xFOutputQty <= mUsedSFQty Then
                    mBalFQty = 0
                    mUsedSFQty = xFOutputQty
                Else
                    '                If PubUserID = "G0416" Then  ''03/01/2015	
                    '                    mBalFQty = 0	
                    '                    mUsedSFQty = xFOutputQty	
                    '                Else	
                    mBalFQty = xFOutputQty - mUsedSFQty
                    '                End If	
                End If

                If UCase(Trim(mRMUOM)) = "TON" Then
                    mUsedRMQty = Val(CStr((mStdQty * mUsedSFQty) / 1000))
                    mUsedRMQty = Val(CStr(mUsedRMQty / 1000))

                    mUsedScrap = Val(CStr((mScrpGrossQtyGram * mUsedSFQty) / 1000))
                    mUsedScrap = Val(CStr(mUsedScrap / 1000))
                ElseIf UCase(Trim(mRMUOM)) = "KGS" Then
                    mUsedRMQty = Val(CStr(mStdQty / 1000)) * mUsedSFQty

                    mUsedScrap = Val(CStr(mScrpGrossQtyGram / 1000)) * mUsedSFQty
                Else
                    mUsedRMQty = mStdQty * mUsedSFQty
                    mUsedScrap = mScrpGrossQtyGram * mUsedSFQty
                End If

                If mStdQty < 0 Then
                    mBalFQty = 0
                    mUsedSFQty = xFOutputQty
                    mUsedRMQty = mStdQty * mUsedSFQty * -1
                    If UCase(Trim(mRMUOM)) = "TON" Then
                        mUsedRMQty = mUsedRMQty / 1000
                        mUsedRMQty = mUsedRMQty / 1000
                    ElseIf UCase(Trim(mRMUOM)) = "KGS" Then
                        mUsedRMQty = mUsedRMQty / 1000
                    End If

                    '                GoTo UpdateRecd	
                    If UpdateStockTRN(PubDBCn, ConStockRefType_PMEMODEPT, (txtPMemoNo.Text), mStockRowNo, (mProdDate), (mProdDate), mStockType, mRMCode, mRMUOM, IIf(xFGBatchNo <= "0", "-1", xFGBatchNo), mUsedRMQty, 0, "I", mRMCostKg, mRMCostKg, "", "", Trim(txtDept.Text), Trim(txtDept.Text), pOutCCCode, "N", "TO : " & lblDept.Text & " (Production) -" & ConStockRefType_PMEMODEPT & "-" & mFICode, "-1", xWareHouse, mDivisionCode, "", mFICode) = False Then GoTo BOMStockErr
                    '                mStockQty = 0	
                End If
                If mStdQty > 0 Then ''mStockQty > 0 And	
UpdateRecd:
                    If UpdateStockTRN(PubDBCn, ConStockRefType_PMEMODEPT, (txtPMemoNo.Text), mStockRowNo, (mProdDate), (mProdDate), mStockType, mRMCode, mRMUOM, IIf(xFGBatchNo <= "0", "-1", xFGBatchNo), mUsedRMQty, 0, "O", mRMCostKg, mRMCostKg, "", "", mDeptCode, mDeptCode, pOutCCCode, "N", "TO : " & lblDept.Text & " (Production) -" & ConStockRefType_PMEMODEPT & "-" & mFICode, "-1", xWareHouse, mDivisionCode, "", mFICode) = False Then GoTo BOMStockErr
                End If

                If mUsedScrap > 0 Then 'UPDATING SCRAP INVENTORY	
                    '                If mScrapCode = "" Then	
                    '                    MsgInformation "Scrap Item Code Not Defined. For Item Code : " & mRMCode & "." & vbNewLine & " Cann't Save."	
                    '                    UpdateBOMStock = False	
                    '                    Exit Function	
                    '                End If	

                    '                If MainClass.ValidateWithMasterTable(mScrapCode, "SCRAP_ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " ") = True Then	
                    '                    mSUOM = IIf(IsNull(MasterNo), "", MasterNo)	
                    mStockRowNo = mStockRowNo + 1
                    If UpdateStockTRN(PubDBCn, ConStockRefType_PMEMODEPT, (txtPMemoNo.Text), mStockRowNo, (mProdDate), (mProdDate), "SC", mRMCode, mRMUOM, CStr(-1), mUsedScrap, 0, "I", mScrpCostKg, mScrpCostKg, "", "", Trim(txtDept.Text), Trim(txtDept.Text), pInCCCode, "N", "From : " & lblDept.Text & " (Production) -" & ConStockRefType_PMEMODEPT & "-" & mFICode, "-1", xWareHouse, mDivisionCode, "", mFICode) = False Then GoTo BOMStockErr
                    '                End If	
                End If

                If mBalFQty <= 0 Then GoTo NextRecd

                ''--Update Alter ItemCode	
                pSqlStr = " SELECT ID.ALTER_RM_CODE, ID.ALTER_STD_QTY, ID.ALETRSCRAP, " & vbCrLf _
                    & " INVMST.ISSUE_UOM, INVMST.SCRAP_ITEM_CODE,ALTER_STOCK_TYPE " & vbCrLf _
                    & " FROM PRD_BOM_ALTER_DET ID, INV_ITEM_MST INVMST " & vbCrLf _
                    & " WHERE ID.MKEY='" & mMKEY & "'" & vbCrLf _
                    & " AND ID.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
                    & " AND ID.ALTER_RM_CODE=INVMST.ITEM_CODE " & vbCrLf _
                    & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(UCase(Trim(txtDept.Text))) & "' " & vbCrLf _
                    & " AND ID.MAINITEM_CODE='" & MainClass.AllowSingleQuote(UCase(mRMCode)) & "'"

                MainClass.UOpenRecordSet(pSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                If RsTemp.EOF = False Then
                    Do While Not RsTemp.EOF
                        mStdQty = Val(IIf(IsDBNull(RsTemp.Fields("ALTER_STD_QTY").Value), "", RsTemp.Fields("ALTER_STD_QTY").Value)) + Val(IIf(IsDBNull(RsTemp.Fields("ALETRSCRAP").Value), "", RsTemp.Fields("ALETRSCRAP").Value))
                        mRMCode = Trim(IIf(IsDBNull(RsTemp.Fields("ALTER_RM_CODE").Value), "", RsTemp.Fields("ALTER_RM_CODE").Value))

                        mProducType = GetProductionType(mRMCode)
                        If Mid(cboType.Text, 1, 1) = "J" Then
                            mStockType = "CS"
                        Else
                            mStockType = IIf(IsDBNull(RsTemp.Fields("ALTER_STOCK_TYPE").Value), "ST", RsTemp.Fields("ALTER_STOCK_TYPE").Value)
                        End If

                        mDeptCode = Trim(txtDept.Text)
                        If IsFGItem(mRMCode) = True Then
                            xWareHouse = ConPH
                        Else
                            If CheckAutoIssue(VB6.Format(txtPMemoDate.Text, "DD/MM/YYYY"), mRMCode) = False Then ''RsCompany!AUTO_ISSUE = "N"	
                                xWareHouse = "PH"
                                mIsInHouse = IsInHouseItem(mRMCode)
                                If mIsInHouse = True And CheckAutoIssueProd(VB6.Format(txtPMemoDate.Text, "DD/MM/YYYY"), mRMCode) = True Then
                                    mDeptCode = GetProductFinalDept(mRMCode, (txtPMemoDate.Text))
                                    If GetDeptType(mDeptCode) = "3" Then
                                        mDeptCode = Trim(txtDept.Text)
                                    Else
                                        If mDeptCode = "STR" Or mDeptCode = "" Then
                                            mDeptCode = Trim(txtDept.Text)
                                        End If
                                    End If
                                End If
                            Else
                                mISProd = IsProductionItem(mRMCode)
                                If mISProd = True Then
                                    If CDate(VB6.Format(txtPMemoDate.Text, "DD/MM/YYYY")) < CDate(VB6.Format(RsCompany.Fields("AUTO_ISSUE_DATE").Value, "DD/MM/YYYY")) Then
                                        xWareHouse = "PH"
                                    Else
                                        mIsInHouse = IsInHouseItem(mRMCode)
                                        If mIsInHouse = True Then
                                            mDeptCode = GetProductFinalDept(mRMCode, (txtPMemoDate.Text))
                                            If GetDeptType(mDeptCode) = "3" Then
                                                mDeptCode = Trim(txtDept.Text)
                                                xWareHouse = "PH"
                                            Else
                                                If mDeptCode = "STR" Or mDeptCode = "" Then
                                                    mDeptCode = "STR"
                                                    xWareHouse = "WH"
                                                Else
                                                    xWareHouse = "PH"
                                                End If
                                            End If
                                        Else
                                            xWareHouse = "WH"
                                        End If
                                    End If
                                Else
                                    xWareHouse = "PH"
                                End If
                            End If
                        End If
                        If MainClass.ValidateWithMasterTable(mRMCode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mRMUOM = Trim(MasterNo)
                        End If
                        '                    mRMUOM = IIf(IsNull(RsTemp!ISSUE_UOM), "", RsTemp!ISSUE_UOM)	
                        mScrapCode = IIf(IsDBNull(RsTemp.Fields("SCRAP_ITEM_CODE").Value), "", RsTemp.Fields("SCRAP_ITEM_CODE").Value)

                        If UCase(Trim(mRMUOM)) = "TON" Then
                            mProductionQty = Val(CStr((mStdQty * mBalFQty) / 1000))
                            mProductionQty = Val(CStr(mProductionQty / 1000))
                        ElseIf UCase(Trim(mRMUOM)) = "KGS" Then
                            mProductionQty = Val(CStr(mStdQty / 1000)) * mBalFQty
                        Else
                            mProductionQty = mStdQty * mBalFQty
                        End If

                        '                    If RsCompany.fields("COMPANY_CODE").value = 21 Then	
                        '                        mProductionQty = mProductionQty / mOutputQty	
                        '                    End If	

                        mScrpGrossQtyGram = Val(IIf(IsDBNull(RsTemp.Fields("ALETRSCRAP").Value), "", RsTemp.Fields("ALETRSCRAP").Value))
                        mScrpGrossQtyKg = Val(CStr(mScrpGrossQtyGram / 1000))
                        mScrpGrossQtyKg = Val(CStr(mScrpGrossQtyKg * mBalFQty))

                        If MainClass.ValidateWithMasterTable(mRMCode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DSP_RPT_FLAG='Y'") = True Then
                            mProd_Type = GetProductionType(mRMCode)
                            If mProd_Type = "I" Or mProd_Type = "P" Then
                                pFGBatchNo = xFGBatchNo
                                xFGBatchNoReq = "Y"
                            Else
                                pFGBatchNo = "X"
                                xFGBatchNoReq = "N"
                            End If
                        Else
                            pFGBatchNo = "X"
                            xFGBatchNoReq = "N"
                        End If

                        mStockRowNo = mStockRowNo + 1
                        mStockQty = GetBalanceStockQty(mRMCode, (txtPMemoDate.Text), mRMUOM, mDeptCode, mStockType, pFGBatchNo, xWareHouse, mDivisionCode, , , xFGBatchNoReq)

                        If mStockQty <= 0 Then mStockQty = 0
                        If mStockQty < mProductionQty Then
                            If UCase(Trim(mRMUOM)) = "TON" Then
                                mUsedSFQty = Val(CStr(mStockQty * 1000 * 1000))
                            ElseIf UCase(Trim(mRMUOM)) = "KGS" Then
                                mUsedSFQty = Val(CStr(mStockQty * 1000))
                            Else
                                mUsedSFQty = mStockQty
                            End If

                            mUsedSFQty = Int(mUsedSFQty / mStdQty)
                            If mBalFQty <= mUsedSFQty Then
                                mBalFQty = 0
                                mUsedSFQty = mBalFQty
                            Else
                                mBalFQty = mBalFQty - mUsedSFQty
                            End If

                            If UCase(Trim(mRMUOM)) = "TON" Then
                                mUsedRMQty = Val(CStr((mStdQty * mUsedSFQty) / 1000))
                                mUsedRMQty = Val(CStr(mUsedRMQty / 1000))

                                mUsedScrap = Val(CStr((mScrpGrossQtyGram * mUsedSFQty) / 1000))
                                mUsedScrap = Val(CStr(mUsedScrap / 1000))

                            ElseIf UCase(Trim(mRMUOM)) = "KGS" Then
                                mUsedRMQty = Val(CStr(mStdQty / 1000)) * mUsedSFQty
                                mUsedScrap = Val(CStr(mScrpGrossQtyGram / 1000)) * mUsedSFQty
                            Else
                                mUsedRMQty = mStdQty * mUsedSFQty
                                mUsedScrap = mScrpGrossQtyGram * mUsedSFQty
                            End If
                        Else
                            mUsedRMQty = mProductionQty

                            If UCase(Trim(mRMUOM)) = "TON" Then
                                mUsedScrap = Val(CStr((mScrpGrossQtyGram * mBalFQty) / 1000))
                                mUsedScrap = Val(CStr(mUsedScrap / 1000))
                            ElseIf UCase(Trim(mRMUOM)) = "KGS" Then
                                mUsedScrap = Val(CStr(mScrpGrossQtyGram / 1000)) * mBalFQty
                            Else
                                mUsedScrap = mScrpGrossQtyGram * mBalFQty
                            End If

                            mBalFQty = 0
                        End If

                        If mStockQty > 0 Then
                            If UpdateStockTRN(PubDBCn, ConStockRefType_PMEMODEPT, (txtPMemoNo.Text), mStockRowNo, (mProdDate), (mProdDate), mStockType, mRMCode, mRMUOM, IIf(xFGBatchNo <= "0", "-1", xFGBatchNo), mUsedRMQty, 0, "O", mRMCostKg, mRMCostKg, "", "", mDeptCode, mDeptCode, pOutCCCode, "N", "To : " & lblDept.Text & " (Production) -" & ConStockRefType_PMEMODEPT & "-" & mFICode, "-1", xWareHouse, mDivisionCode, "", mFICode) = False Then GoTo BOMStockErr
                        End If


                        If mUsedScrap > 0 Then 'UPDATING SCRAP INVENTORY	
                            If mScrapCode = "" Then
                                MsgInformation("Scrap Item Code Not Defined. For Item Code : " & mRMCode & "." & vbNewLine & " Cann't Save.")
                                UpdateBOMStock = False
                                Exit Function
                            End If

                            '                        If MainClass.ValidateWithMasterTable(mScrapCode, "SCRAP_ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " ") = True Then	
                            '                            mSUOM = IIf(IsNull(MasterNo), "", MasterNo)	
                            mStockRowNo = mStockRowNo + 1
                            If UpdateStockTRN(PubDBCn, ConStockRefType_PMEMODEPT, (txtPMemoNo.Text), mStockRowNo, (mProdDate), (mProdDate), "SC", mRMCode, mRMUOM, CStr(-1), mUsedScrap, 0, "I", mScrpCostKg, mScrpCostKg, "", "", Trim(txtDept.Text), Trim(txtDept.Text), pInCCCode, "N", "From : " & lblDept.Text & " (Production) -" & ConStockRefType_PMEMODEPT & "-" & mFICode, "-1", xWareHouse, mDivisionCode, "", mFICode) = False Then GoTo BOMStockErr
                            '                        End If	
                        End If
                        RsTemp.MoveNext()
                        If mBalFQty = 0 Then Exit Do
                    Loop
                End If

                ''-------------	
NextRecd:
                pRsBOM.MoveNext()
            Loop
        End With
        mRetStockRowNo = mStockRowNo
        UpdateBOMStock = True
        Exit Function
BOMStockErr:
        UpdateBOMStock = False
        If Err.Description <> "" Then MsgBox(Err.Description)
        '    Resume	
    End Function

    Private Function CheckRMStock(ByRef pErrorDesc As String, ByRef mMKEY As String, ByRef mFICode As String, ByRef mRMCode As String, ByRef mRMUOM As String, ByRef mDeptCode As String, ByRef pFQty As Double, ByRef pStdQty As Double, ByRef mReqQty As Double, ByRef pStockType As String, ByRef xWareHouse As String, ByRef mDivisionCode As Double, ByRef xFGBatchNo As String) As Boolean

        On Error GoTo BOMStockErr
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mAlterRMCode As String
        Dim mAlterRMUOM As String
        Dim mAlterStdQty As Double
        Dim mStockQty As Double
        Dim mReqStockQty As Double
        Dim mTotStockQty As Double
        Dim pFQtyUsed As Double
        Dim pBalFQty As Double
        Dim mRMCodeStr As String
        Dim mFGUOM As String
        Dim mProd_Type As String
        Dim pWareHouse As String
        Dim mISProd As Boolean
        Dim mIsInHouse As Boolean
        Dim pFGBatchNo As String
        Dim xFGBatchNoReq As String

        If RsCompany.Fields("StockBalCheck").Value = "N" Then
            CheckRMStock = True
            Exit Function
        End If
        If MainClass.ValidateWithMasterTable(mRMCode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DSP_RPT_FLAG='Y'") = True Then
            mProd_Type = GetProductionType(mRMCode)
            If mProd_Type = "I" Or mProd_Type = "P" Then
                pFGBatchNo = xFGBatchNo
                xFGBatchNoReq = "Y"
            Else
                pFGBatchNo = "X"
                xFGBatchNoReq = "N"
            End If
        Else
            pFGBatchNo = "X"
            xFGBatchNoReq = "N"
        End If

        pWareHouse = xWareHouse
        mStockQty = GetBalanceStockQty(mRMCode, (txtPMemoDate.Text), mRMUOM, mDeptCode, pStockType, pFGBatchNo, pWareHouse, mDivisionCode, , , xFGBatchNoReq)


        mTotStockQty = mStockQty
        mRMCodeStr = mRMCode
        If UCase(Trim(mRMUOM)) = "TON" Then
            mStockQty = mStockQty * 1000 * 1000
        ElseIf UCase(Trim(mRMUOM)) = "KGS" Then
            mStockQty = mStockQty * 1000
        End If

        If MainClass.ValidateWithMasterTable(mFICode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mFGUOM = MasterNo
        End If

        If mFGUOM = "KGS" Or mFGUOM = "TON" Or mFGUOM = "LTR" Then
            pFQtyUsed = mStockQty / pStdQty
        Else
            pFQtyUsed = Int(mStockQty / pStdQty)
        End If

        pBalFQty = pFQty - pFQtyUsed

        If pBalFQty <= 0 Then
            CheckRMStock = True
            Exit Function
        End If
        SqlStr = " SELECT ID.ALTER_RM_CODE, ALTER_STD_QTY ,ALETRSCRAP, INVMST.ISSUE_UOM,ALTER_STOCK_TYPE " & vbCrLf _
            & " FROM PRD_BOM_ALTER_DET ID, INV_ITEM_MST INVMST " & vbCrLf _
            & " WHERE ID.MKEY='" & mMKEY & "'" & vbCrLf _
            & " AND ID.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(UCase(txtDept.Text)) & "' " & vbCrLf _
            & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
            & " AND ID.ALTER_RM_CODE=INVMST.ITEM_CODE " & vbCrLf _
            & " AND ID.MAINITEM_CODE='" & MainClass.AllowSingleQuote(UCase(mRMCode)) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While Not RsTemp.EOF
                mAlterRMCode = Trim(IIf(IsDBNull(RsTemp.Fields("ALTER_RM_CODE").Value), "", RsTemp.Fields("ALTER_RM_CODE").Value))
                mAlterRMUOM = Trim(IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value))
                mAlterStdQty = Val(IIf(IsDBNull(RsTemp.Fields("ALTER_STD_QTY").Value), 0, RsTemp.Fields("ALTER_STD_QTY").Value)) + Val(IIf(IsDBNull(RsTemp.Fields("ALETRSCRAP").Value), 0, RsTemp.Fields("ALETRSCRAP").Value))
                pStockType = Trim(IIf(IsDBNull(RsTemp.Fields("ALTER_STOCK_TYPE").Value), "", RsTemp.Fields("ALTER_STOCK_TYPE").Value))

                mDeptCode = Trim(txtDept.Text)


                If MainClass.ValidateWithMasterTable(mAlterRMCode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DSP_RPT_FLAG='Y'") = True Then
                    mProd_Type = GetProductionType(mAlterRMCode)
                    If mProd_Type = "I" Or mProd_Type = "P" Then
                        pFGBatchNo = xFGBatchNo
                        xFGBatchNoReq = "Y"
                    Else
                        pFGBatchNo = "X"
                        xFGBatchNoReq = "N"
                    End If
                Else
                    pFGBatchNo = "X"
                    xFGBatchNoReq = "N"
                End If

                If IsFGItem(mAlterRMCode) = True Then
                    pWareHouse = ConPH
                Else
                    If CheckAutoIssue(VB6.Format(txtPMemoDate.Text, "DD/MM/YYYY"), mAlterRMCode) = False Then ''RsCompany!AUTO_ISSUE = "N"	
                        pWareHouse = "PH"
                        mIsInHouse = IsInHouseItem(mAlterRMCode)
                        'If RsCompany.Fields("COMPANY_CODE").Value = 32 Or RsCompany.Fields("COMPANY_CODE").Value = 1 Or RsCompany.Fields("COMPANY_CODE").Value = 34 Or RsCompany.Fields("COMPANY_CODE").Value = 42 Or RsCompany.Fields("COMPANY_CODE").Value = 43 Then ''02/07/2016	
                        If mIsInHouse = True And CheckAutoIssueProd(VB6.Format(txtPMemoDate.Text, "DD/MM/YYYY"), mRMCode) = True Then
                            mDeptCode = GetProductFinalDept(mAlterRMCode, (txtPMemoDate.Text))
                            If mDeptCode = "STR" Or mDeptCode = "" Then
                                mDeptCode = Trim(txtDept.Text)
                            End If

                        End If
                        'End If
                    Else
                        mISProd = IsProductionItem(mAlterRMCode)
                        If mISProd = True Then
                            If CDate(VB6.Format(txtPMemoDate.Text, "DD/MM/YYYY")) < CDate(VB6.Format(RsCompany.Fields("AUTO_ISSUE_DATE").Value, "DD/MM/YYYY")) Then
                                pWareHouse = "PH"
                            Else
                                mIsInHouse = IsInHouseItem(mAlterRMCode)
                                If mIsInHouse = True Then
                                    mDeptCode = GetProductFinalDept(mAlterRMCode, (txtPMemoDate.Text))
                                    If GetDeptType(mDeptCode) = "3" Then
                                        mDeptCode = Trim(txtDept.Text)
                                        pWareHouse = "PH"
                                    Else
                                        If mDeptCode = "STR" Or mDeptCode = "" Then
                                            mDeptCode = "STR"
                                            pWareHouse = "WH"
                                        Else
                                            pWareHouse = "PH"
                                        End If
                                    End If
                                Else
                                    pWareHouse = "WH"
                                End If
                            End If
                        Else
                            pWareHouse = "PH"
                        End If
                    End If
                End If

                'If mAlterRMUOM = mRMUOM Then
                '    mRMCodeStr = mRMCodeStr & "," & mAlterRMCode

                '    mStockQty = GetBalanceStockQty(mAlterRMCode, (txtPMemoDate.Text), mAlterRMUOM, mDeptCode, pStockType, pFGBatchNo, pWareHouse, mDivisionCode, , , xFGBatchNoReq)
                'Else
                '    mStockQty = GetBalanceStockQty(mAlterRMCode, (txtPMemoDate.Text), mAlterRMUOM, mDeptCode, pStockType, pFGBatchNo, pWareHouse, mDivisionCode, , , xFGBatchNoReq)

                'End If


                mRMCodeStr = mRMCodeStr & "," & mAlterRMCode

                mStockQty = GetBalanceStockQty(mAlterRMCode, (txtPMemoDate.Text), mAlterRMUOM, mDeptCode, pStockType, pFGBatchNo, pWareHouse, mDivisionCode, , , xFGBatchNoReq)



                If mStockQty < 0 Then mStockQty = 0
                mTotStockQty = mTotStockQty + mStockQty

                If UCase(Trim(mAlterRMUOM)) = "TON" Then
                    mStockQty = mStockQty * 1000 * 1000
                ElseIf UCase(Trim(mAlterRMUOM)) = "KGS" Then
                    mStockQty = mStockQty * 1000
                End If
                pFQtyUsed = Int(mStockQty / mAlterStdQty)
                pBalFQty = pBalFQty - pFQtyUsed

                RsTemp.MoveNext()
                If pBalFQty <= 0 Then Exit Do
            Loop
        End If

        If pBalFQty <= 0 Then
            CheckRMStock = True
        Else
            '        MsgInformation "You have Not Enough Stock. For Finished Goods " & mFICode & vbNewLine & "(Item Code : " & mRMCodeStr & "( Req. Qty : " & mReqQty & " And Bal. Qty : " & mTotStockQty & "))." & vbNewLine & " Cann't Save."	
            pErrorDesc = "You have Not Enough Stock. For Finished Goods " & mFICode & vbNewLine & "(Item Code : " & mRMCodeStr & "( Req. Qty : " & mReqQty & " And Bal. Qty : " & mTotStockQty & "))." & vbNewLine & " Cann't Save."
            CheckRMStock = False
        End If
        Exit Function
BOMStockErr:
        CheckRMStock = False
        If Err.Description <> "" Then MsgBox(Err.Description)
        'Resume	
    End Function
    Private Function MakeBOMStockQty(ByRef mSFICode As String, ByRef mDeptCode As String, ByRef pOprSeq As Integer, ByRef pOPRCode As String) As String

        On Error GoTo BOMStockErr
        Dim SqlStr As String = ""


        SqlStr = "SELECT  IH.MKEY, IH.PRODUCT_CODE, " & vbCrLf & " ID.RM_CODE, ID.STD_QTY, " & vbCrLf & " ID.GROSS_WT_SCRAP, INVMST.ISSUE_UOM, INVMST.SCRAP_ITEM_CODE, FROM_SCRAP, ID.STOCK_TYPE,IH.OUTPUT_QTY "

        SqlStr = SqlStr & vbCrLf & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST"

        SqlStr = SqlStr & vbCrLf _
           & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
           & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
           & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
           & " AND ID.RM_CODE=INVMST.ITEM_CODE " & vbCrLf _
           & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(UCase(mSFICode)) & "' "

        '    If mDeptCode <> "TRD" Then	
        If lblBookType.Text = "P" Or lblBookType.Text = "J" Then
            SqlStr = SqlStr & vbCrLf & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(UCase(mDeptCode)) & "' "
        End If

        If pOprSeq = 1 Then
            SqlStr = SqlStr & vbCrLf & " AND (ID.OPR_CODE='" & MainClass.AllowSingleQuote(UCase(pOPRCode)) & "' OR ID.OPR_CODE='' OR ID.OPR_CODE IS NULL)"
        Else
            SqlStr = SqlStr & vbCrLf & " AND ID.OPR_CODE='" & MainClass.AllowSingleQuote(UCase(pOPRCode)) & "' "
        End If

        SqlStr = SqlStr & vbCrLf & " AND IH.WEF=( " & vbCrLf & " SELECT MAX(WEF) FROM PRD_NEWBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(UCase(mSFICode)) & "' " & vbCrLf & " AND WEF<= TO_DATE('" & VB6.Format(txtPMemoDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND IS_APPROVED='Y')"


        MakeBOMStockQty = SqlStr
        Exit Function
BOMStockErr:
        If Err.Description <> "" Then MsgBox(Err.Description)
        'Resume	
    End Function

    Private Function FieldsVarification() As Boolean

        On Error GoTo err_Renamed
        Dim mDeptCode As String
        Dim mCheckLastEntryDate As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim cntRow As Integer
        Dim mProductCode As String
        Dim mOPRCode As String
        Dim mOPRDesc As String
        Dim mTotalProduction As Double
        Dim mIsAuthorisedUser As String
        Dim mProductSeqNo As String
        Dim mTotalWIPQty As Double
        Dim mDivisionCode As Double
        Dim mProdQty As Double
        Dim pOPRCode As String
        Dim mMachineNo As String
        Dim pOPRDesc As String
        Dim mOprSeq As String
        Dim mMessage As String
        Dim mProductionPlan As Double
        Dim mCheckProdType As String
        Dim mTodayProductionPlan As Double
        Dim pMRRNo As Double
        Dim pSBSlipNo As Double
        Dim xBatchNoRequired As String

        FieldsVarification = True
        If ValidateBranchLocking((txtPMemoDate.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Memo No or modify an existing Memo No")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsPMemoMain.EOF = True Then Exit Function

        If txtPMemoDate.Text = "" Then
            MsgBox("Date is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtPMemoDate.Focus()
            Exit Function
        ElseIf FYChk((txtPMemoDate.Text)) = False Then
            FieldsVarification = False
            If txtPMemoDate.Enabled = True Then txtPMemoDate.Focus()
            Exit Function
        End If


        If Trim(cboShiftcd.Text) = "" Then
            MsgBox("Shift is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            cboShiftcd.Focus()
            Exit Function
        End If

        If Trim(cboShiftcd.Text) = "C" Then
            txtProdDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(VB6.Format(txtPMemoDate.Text, "DD/MM/YYYY"))))
            txtProdDate.Text = VB6.Format(txtProdDate.Text, "DD/MM/YYYY")
        Else
            txtProdDate.Text = VB6.Format(txtPMemoDate.Text, "DD/MM/YYYY")
        End If

        If Trim(txtDept.Text) = "" Then
            MsgBox("From Deptt is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtDept.Focus()
            Exit Function
        End If

        If Trim(cboLineNo.Text) = "" Then
            MsgBox("Line No is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            cboLineNo.Focus()
            Exit Function
        End If

        If CDate(txtProdDate.Text) > CDate(PubCurrDate) Then
            MsgBox("Production Date Cann't be Greater than Current Date", MsgBoxStyle.Information)
            FieldsVarification = False
            'txtProdDate.Focus()
            Exit Function
        End If

        If Trim(cboDivision.Text) = "" Then
            MsgBox("Division Name is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If cboDivision.Enabled = True Then cboDivision.Focus()
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If


        If cboType.Text = "" Then
            MsgBox("Production Type is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If cboType.Enabled = True Then cboType.Focus()
            Exit Function
        End If

        If txtEmp.Text = "" Then
            MsgBox("Employee is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtEmp.Focus()
            Exit Function
        End If

        If chkSPD.CheckState = System.Windows.Forms.CheckState.Checked And Trim(txtDept.Text) <> "ASY" Then
            MsgBox("Please Check in SPD", MsgBoxStyle.Information)
            FieldsVarification = False
            chkSPD.Focus()
            Exit Function
        End If


        ''Or RsCompany.Fields("StockBalCheck").Value = "Y"
        If PubSuperUser <> "S" Then
            mIsAuthorisedUser = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)

            '        If RsCompany.fields("COMPANY_CODE").value = 1 Or RsCompany.fields("COMPANY_CODE").value = 12 Then	
            '            If chkApproved.Value = vbChecked And chkApproved.Enabled = False Then	
            '                MsgBox "Approved Entry Cann't be Save.", vbInformation	
            '                FieldsVarification = False	
            '                Exit Function	
            '            End If	
            '        End If	
            If InStr(1, mIsAuthorisedUser, "S") = 0 Then
                mCheckLastEntryDate = GetLastEntryDate()

                If mCheckLastEntryDate <> "" Then
                    mCheckLastEntryDate = DateAdd("d", -1, mCheckLastEntryDate)
                    If CDate(txtPMemoDate.Text) < CDate(mCheckLastEntryDate) Then
                        MsgBox("Cann't be Add or Modify Back Entry", MsgBoxStyle.Information)
                        FieldsVarification = False
                        Exit Function
                    End If
                End If
            End If
        End If

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColItemCode
                mProductCode = Trim(.Text)


                If MainClass.ValidateWithMasterTable(mProductCode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DSP_RPT_FLAG='Y'") = True Then
                    xBatchNoRequired = "Y"
                Else
                    xBatchNoRequired = "N"
                End If

                If RsCompany.Fields("StockBalCheck").Value = "Y" Then
                    'If PendingReworkQty(mProductCode, (txtDept.Text), mDivisionCode, (txtPMemoDate.Text), pSBSlipNo) = True Then
                    '    MsgInformation("There are Pending WR for Item Code : " & mProductCode & " (Slip No :" & pSBSlipNo & ") for action, Please clear first Rework.")
                    '    FieldsVarification = False
                    '    MainClass.SetFocusToCell(SprdMain, cntRow, ColItemCode)
                    '    Exit Function
                    'End If

                    'If PendingCRQty(mProductCode, (txtDept.Text), mDivisionCode, (txtPMemoDate.Text), pMRRNo) = True Then
                    '    MsgInformation("There are Pending CR for Item Code : " & mProductCode & " (MRR NO :" & pMRRNo & ") for action, Please clear first CR.")
                    '    FieldsVarification = False
                    '    MainClass.SetFocusToCell(SprdMain, cntRow, ColItemCode)
                    '    Exit Function
                    'End If
                End If
                If mProductCode <> "" Then
                    If MainClass.ValidateWithMasterTable(mProductCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS='A'") = False Then
                        If MsgQuestion("Product Code : " & mProductCode & " is Inactive. Want to Proceed ?") = CStr(MsgBoxResult.No) Then
                            FieldsVarification = False
                            MainClass.SetFocusToCell(SprdMain, cntRow, ColItemCode)
                            Exit Function
                        End If
                    End If
                    mCheckProdType = GetProductionType(mProductCode)
                End If

                .Col = ColProdQty
                mProdQty = Val(.Text)

                '            If RsCompany.fields("COMPANY_CODE").value = 10 Then	
                '                .Col = ColProdQty	
                '                mProdQty = Val(.Text)	
                '                mMaxLevelQty = GetMaxLevel(mProductCode, Trim(txtDept.Text), Format(txtPMemoDate.Text, "DD/MM/YYYY"))	
                '                If mMaxLevelQty <> 0 Then	
                '                    If mProdQty > mMaxLevelQty Then	
                '                        MsgInformation "You can't be enter production more than Max Level (" & mMaxLevelQty & "). Cann't Be Saved"	
                '                        FieldsVarification = False	
                '                        MainClass.SetFocusToCell SprdMain, cntRow, ColProdQty	
                '                        Exit Function	
                '                    End If	
                '	
                '                End If	
                '            End If	

                If lblBookType.Text = "D" Then
                    .Col = ColProdQty
                    If Val(.Text) > 1000 And PubSuperUser <> "S" Then
                        MsgInformation("You cann't be Entered More Than 1000 Qty. Cann't Be Saved")
                        FieldsVarification = False
                        MainClass.SetFocusToCell(SprdMain, cntRow, ColItemCode)
                        Exit Function
                    Else
                        mTotalProduction = GetDevelopmentItemProdQty(mProductCode)
                        If mTotalProduction + Val(.Text) > 1000 And PubSuperUser <> "S" Then
                            MsgInformation("Please Regularized Product Code : " & mProductCode & ". Cann't Be Saved")
                            FieldsVarification = False
                            MainClass.SetFocusToCell(SprdMain, cntRow, ColItemCode)
                            Exit Function
                        End If
                    End If
                End If

                If mProductCode <> "" And (lblBookType.Text = "P" Or lblBookType.Text = "J") And mProdQty > 0 Then
                    SqlStr = " SELECT PRODUCT_CODE,IS_APPROVED " & vbCrLf _
                        & " FROM PRD_NEWBOM_HDR" & vbCrLf _
                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND STATUS='O'" & vbCrLf _
                        & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'"

                    SqlStr = SqlStr & vbCrLf _
                        & " AND WEF=( " & vbCrLf & " SELECT MAX(WEF) FROM PRD_NEWBOM_HDR " & vbCrLf _
                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                        & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(UCase(mProductCode)) & "') "

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                    If RsTemp.EOF = True Then
                        MsgInformation("Please Defined B.O.M. For Product Code : " & mProductCode & ". Cann't Be Saved")
                        FieldsVarification = False
                        '                MainClass.SetFocusToCell SprdMain, CntRow, ColItemCode	
                        Exit Function
                    Else
                        If RsTemp.Fields("IS_APPROVED").Value = "N" Then
                            MsgInformation("B.O.M. has not Approved for Product Code : " & mProductCode & ". Cann't Be Saved")
                            FieldsVarification = False
                            '                MainClass.SetFocusToCell SprdMain, CntRow, ColItemCode	
                            Exit Function
                        End If
                    End If

                    .Col = ColMachineNo
                    mMachineNo = Trim(.Text)

                    If mMachineNo <> "" Then
                        If ADDMode = True Then
                            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND MACHINE_UB='N' AND STATUS='O' AND DIV_CODE=" & mDivisionCode & " AND DEPT_CODE='" & txtDept.Text & "'"
                        Else
                            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DEPT_CODE='" & txtDept.Text & "'"
                        End If

                        If MainClass.ValidateWithMasterTable(mMachineNo, "MACHINE_NO", "MACHINE_DESC", "MAN_MACHINE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
                            MsgBox("Machine Does Not Exist In Master.")
                            FieldsVarification = False
                            MainClass.SetFocusToCell(SprdMain, cntRow, ColMachineNo)
                        End If
                    End If
                    .Col = ColOPR
                        mOPRDesc = Trim(.Text)
                        pOPRCode = ""
                        If mOPRDesc = "" Then
                            SqlStr = OperationQuery(Trim(mProductCode), Trim(txtDept.Text), "", "", Trim(txtPMemoDate.Text), "TRN.OPR_CODE")
                            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                            If RsTemp.EOF = False Then
                                MsgInformation("Operation Defined for Item Code : " & mProductCode & ". Cann't Be Saved")
                                FieldsVarification = False
                                MainClass.SetFocusToCell(SprdMain, cntRow, ColOPR)
                                Exit Function
                            End If
                        Else
                            SqlStr = OperationQuery(Trim(mProductCode), Trim(txtDept.Text), "", Trim(mOPRDesc), Trim(txtPMemoDate.Text), "TRN.OPR_CODE")
                            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                            If RsTemp.EOF = True Then
                                MsgInformation("Invalid Operation for Item Code : " & mProductCode & ". Cann't Be Saved")
                                FieldsVarification = False
                                MainClass.SetFocusToCell(SprdMain, cntRow, ColOPR)
                                Exit Function
                            Else
                                pOPRCode = IIf(IsDBNull(RsTemp.Fields("OPR_CODE").Value), "", RsTemp.Fields("OPR_CODE").Value)
                            End If
                        End If



                        If lblBookType.Text = "P" Or lblBookType.Text = "J" Then
                            mProductSeqNo = CStr(GetProductSeqNo(mProductCode, Trim(txtDept.Text), (txtPMemoDate.Text)))
                            If CDbl(mProductSeqNo) = 0 Then
                                MsgBox("Product Sequence Not Defined. Item Code :" & mProductCode)
                                FieldsVarification = False
                                Exit Function
                            End If

                            If CDbl(mProductSeqNo) > 1 And xBatchNoRequired = "Y" Then
                                .Col = ColBatchNo
                                If Trim(.Text) = "" Or Trim(.Text) = "0" Then
                                    MsgBox("Product Batch No is Required, Please select the Batch No of Item Code :" & mProductCode)
                                    FieldsVarification = False
                                    Exit Function
                                End If
                            End If


                            .Col = ColProdQty
                            mProdQty = Val(.Text)

                            If RsCompany.Fields("StockBalCheck").Value = "Y" Then
                                If RsCompany.Fields("CHECK_FG_STOCK").Value = "Y" And RsCompany.Fields("ISSUE_TYPE").Value = "P" Then
                                    If mCheckProdType = "P" Or mCheckProdType = "B" Or mCheckProdType = "3" Or mCheckProdType = "I" Then ''	
                                        If GetUserPermission("ALLOW_EXCESS_ISSUE", "N", PubUserID, RsCompany.Fields("COMPANY_CODE").Value) = "N" Then

                                            mOprSeq = CStr(GetOperationSeq(mProductCode, Trim(txtDept.Text), pOPRCode, (txtPMemoDate.Text)))

                                            If CDbl(mOprSeq) = 1 Then
                                                mTodayProductionPlan = 0
                                                mProductionPlan = GetBalanceProductionPlan(mProductCode, pOPRCode, mTodayProductionPlan)
                                                '                                    If mProdQty > mProductionPlan Then	
                                                '                                        MsgInformation "You cann't be enter production more than Production Planning (" & mProductionPlan & "). Cann't Be Saved"	
                                                '                                        FieldsVarification = False	
                                                '                                        MainClass.SetFocusToCell SprdMain, cntRow, ColProdQty	
                                                '                                        Exit Function	
                                                '                                    End If	
                                                '	
                                                If CDbl(mProductSeqNo) = 1 Then
                                                    If CheckWIPQty(mProductCode, mProductSeqNo, Trim(txtDept.Text), Trim(txtPMemoDate.Text), mProdQty, mTotalWIPQty, mDivisionCode, mMessage, mTodayProductionPlan) = False Then
                                                        MsgInformation(mMessage)
                                                        FieldsVarification = False
                                                        Exit Function
                                                    End If
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If

                        If mOPRDesc <> "" Then
                            .Col = ColOPRERCode

                            If Trim(.Text) = "" Then
                                '                        MsgInformation "Please Defined Operator Code for Item Code : " & mProductCode & ". Cann't Be Saved"	
                                '                        FieldsVarification = False	
                                '                        MainClass.SetFocusToCell SprdMain, cntRow, ColOPRERCode	
                                '                        Exit Function	
                            Else
                                SqlStr = " SELECT EMP_NAME, EMP_CODE " & vbCrLf & " FROM PAY_CONT_EMPLOYEE_MST" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(.Text) & "'" & vbCrLf & " AND EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'"

                                ''	
                                '                        If ADDMode = True Then	
                                '                            SqlStr = SqlStr & vbCrLf & " AND EMP_CAT='P'"	
                                '                        End If	

                                SqlStr = SqlStr & vbCrLf & " AND (EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE='' OR EMP_LEAVE_DATE>=TO_DATE('" & VB6.Format(txtProdDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

                                '                        SqlStr = SqlStr & vbCrLf & " ORDER BY EMP_NAME"	

                                SqlStr = SqlStr & vbCrLf & " UNION "

                                SqlStr = SqlStr & vbCrLf & " SELECT EMP_NAME, EMP_CODE " & vbCrLf & " FROM PAY_EMPLOYEE_MST" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(.Text) & "'" & vbCrLf & " AND EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'"

                                SqlStr = SqlStr & vbCrLf & " AND EMP_TYPE='W'"

                                SqlStr = SqlStr & vbCrLf & " AND (EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE='' OR EMP_LEAVE_DATE>=TO_DATE('" & VB6.Format(txtProdDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"


                                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                                If RsTemp.EOF = True Then
                                    MsgInformation("Invalid Operator for Item Code : " & mProductCode & ". Cann't Be Saved")
                                    FieldsVarification = False
                                    MainClass.SetFocusToCell(SprdMain, cntRow, ColOPRERCode)
                                    Exit Function
                                End If
                            End If
                        End If
                    End If
            Next
        End With

        If CheckRowCount() = False Then
            FieldsVarification = False
            Exit Function
        End If

        If ValidateDeptRight(PubUserID, Trim(txtDept.Text), UCase(Trim(lblDept.Text))) = False Then
            FieldsVarification = False
            Exit Function
        End If



        '     If PubSuperUser = "U" Then	
        '        If MainClass.ValidateWithMasterTable(PubUserEMPCode, "EMP_CODE", "EMP_DEPT_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then	
        '            mDeptCode = MasterNo	
        '            If UCase(Trim(txtDept.Text)) <> UCase(Trim(mDeptCode)) Then	
        '                MsgBox "You Are Not in This Dept.", vbInformation	
        '                FieldsVarification = False	
        '            End If	
        '        Else	
        '            MsgBox "Invalid Emp Code.", vbInformation	
        '            FieldsVarification = False	
        '        End If	
        '    End If	

        If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "ItemCode Is Blank.") = False Then FieldsVarification = False : Exit Function
        '    If MainClass.ValidDataInGrid(SprdMain, ColprodQty, "N", "Please Check Produce Quantity.") = False Then FieldsVarification = False: Exit Function	
        If MainClass.ValidDataInGrid(SprdMain, ColStockType, "S", "Please Check Stock Type.") = False Then FieldsVarification = False : Exit Function

        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume	
    End Function

    Public Function PendingCRQty(ByRef pItemCode As String, ByRef pDeptCode As String, ByRef pDivCode As Double, ByRef pProdDate As String, ByRef pMRRNo As Double) As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mClearDate As String

        PendingCRQty = False
        pMRRNo = 0

        If Trim(pItemCode) = "" Then Exit Function
        If Val(CStr(pDivCode)) = 0 Then Exit Function
        If Trim(pDeptCode) = "" Then Exit Function


        SqlStr = "SELECT AUTO_KEY_MRR, SUM(DECODE(ITEM_IO,'I',1,-1) * ITEM_QTY) As ITEM_QTY" & vbCrLf _
            & " FROM DSP_CR_TRN" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
            & " AND STOCK_TYPE IN ('WC','SR')" & vbCrLf _
            & " AND DIV_CODE=" & pDivCode & ""


        SqlStr = SqlStr & vbCrLf & " AND DEPT_CODE='" & pDeptCode & "' AND MRR_DATE<=TO_DATE('" & VB6.Format(pProdDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " AND AUTO_KEY_MRR IN ( " & vbCrLf _
                & " SELECT AUTO_KEY_MRR FROM DSP_CR_TRN" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
                & " AND DEPT_CODE='" & pDeptCode & "'" & vbCrLf _
                & " AND DIV_CODE=" & pDivCode & "" & vbCrLf _
                & " AND REF_TYPE ='MRR'" & vbCrLf _
                & " AND COMPLETION_DATE<=TO_DATE('" & VB6.Format(pProdDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " )"

        SqlStr = SqlStr & vbCrLf & " AND AUTO_KEY_MRR NOT IN ( " & vbCrLf _
                & " SELECT AUTO_KEY_MRR FROM GEN_CR_STOCK_LOCK" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
                & " AND DEPT_CODE='" & pDeptCode & "'" & vbCrLf _
                & " AND TILL_DATE>=TO_DATE('" & VB6.Format(pProdDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " )"
        SqlStr = SqlStr & vbCrLf & " GROUP BY AUTO_KEY_MRR " & vbCrLf & " HAVING SUM(DECODE(ITEM_IO,'I',1,-1) * ITEM_QTY)>0"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            pMRRNo = IIf(IsDBNull(RsTemp.Fields("AUTO_KEY_MRR").Value), 0, RsTemp.Fields("AUTO_KEY_MRR").Value)
            PendingCRQty = True
        End If
        Exit Function
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function


    Public Function PendingReworkQty(ByRef pItemCode As String, ByRef pDeptCode As String, ByRef pDivCode As Double, ByRef pProdDate As String, ByRef pSlipNo As Double) As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mClearDate As String

        PendingReworkQty = False
        pSlipNo = 0

        If Trim(pItemCode) = "" Then Exit Function
        If Val(CStr(pDivCode)) = 0 Then Exit Function
        If Trim(pDeptCode) = "" Then Exit Function


        SqlStr = "SELECT AUTO_KEY_SBRWK, SUM(DECODE(ITEM_IO,'I',1,-1) * ITEM_QTY) As ITEM_QTY" & vbCrLf _
                   & " FROM PRD_REWORK_TRN" & vbCrLf _
                   & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                   & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
                   & " AND STOCK_TYPE IN ('WR')" & vbCrLf _
                   & " AND DIV_CODE=" & pDivCode & ""


        SqlStr = SqlStr & vbCrLf & " AND DEPT_CODE='" & pDeptCode & "' AND SB_DATE<=TO_DATE('" & VB6.Format(pProdDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " AND AUTO_KEY_SBRWK IN ( " & vbCrLf _
                   & " SELECT AUTO_KEY_SBRWK FROM PRD_REWORK_TRN" & vbCrLf _
                   & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                   & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
                   & " AND DEPT_CODE='" & pDeptCode & "'" & vbCrLf _
                   & " AND DIV_CODE=" & pDivCode & "" & vbCrLf _
                   & " AND REF_TYPE ='" & ConStockRefType_RWK & "'" & vbCrLf _
                   & " AND COMPLETION_DATE<=TO_DATE('" & VB6.Format(pProdDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                   & " )"

        SqlStr = SqlStr & vbCrLf & " AND AUTO_KEY_SBRWK NOT IN ( " & vbCrLf _
                    & " SELECT AUTO_KEY_SBRWK FROM GEN_REWORK_STOCK_LOCK" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
                    & " AND DEPT_CODE='" & pDeptCode & "'" & vbCrLf _
                    & " AND TILL_DATE>=TO_DATE('" & VB6.Format(pProdDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " )"


        SqlStr = SqlStr & vbCrLf & " GROUP BY AUTO_KEY_SBRWK " & vbCrLf & " HAVING SUM(DECODE(ITEM_IO,'I',1,-1) * ITEM_QTY)>0"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            pSlipNo = IIf(IsDBNull(RsTemp.Fields("AUTO_KEY_SBRWK").Value), 0, RsTemp.Fields("AUTO_KEY_SBRWK").Value)
            PendingReworkQty = True
        End If
        Exit Function
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function



    Private Function GetBalanceProductionPlan(ByRef pItemCode As String, ByRef pOPRCode As String, ByRef mTodayProductionPlan As Double) As Double

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        GetBalanceProductionPlan = 0
        mTodayProductionPlan = 0
        SqlStr = " SELECT SUM(DPLAN_QTY) AS DPLAN_QTY " & vbCrLf & " FROM PRD_PRODPLAN_MONTH_DET IH" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.SERIAL_DATE=TO_DATE('" & VB6.Format(txtProdDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        SqlStr = SqlStr & vbCrLf & " AND IH.DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'"

        SqlStr = SqlStr & vbCrLf & " AND IH.INHOUSE_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            GetBalanceProductionPlan = IIf(IsDBNull(RsTemp.Fields("DPLAN_QTY").Value), 0, RsTemp.Fields("DPLAN_QTY").Value)
            mTodayProductionPlan = IIf(IsDBNull(RsTemp.Fields("DPLAN_QTY").Value), 0, RsTemp.Fields("DPLAN_QTY").Value)
        End If

        SqlStr = " SELECT SUM(PROD_QTY) AS PROD_QTY " & vbCrLf & " FROM PRD_PMEMODEPT_HDR IH, PRD_PMEMODEPT_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_REF=ID.AUTO_KEY_REF" & vbCrLf & " AND IH.DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf & " AND IH.PROD_DATE=TO_DATE('" & VB6.Format(txtProdDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If pOPRCode <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND ID.OPR_CODE='" & MainClass.AllowSingleQuote(pOPRCode) & "'"
        End If

        If Val(txtPMemoNo.Text) > 0 Then
            SqlStr = SqlStr & vbCrLf & " AND IH.AUTO_KEY_REF<>" & Val(txtPMemoNo.Text) & ""
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            GetBalanceProductionPlan = GetBalanceProductionPlan - IIf(IsDBNull(RsTemp.Fields("PROD_QTY").Value), 0, RsTemp.Fields("PROD_QTY").Value)
        End If


        Exit Function
ERR1:
        MsgInformation(Err.Description)
        GetBalanceProductionPlan = 0
    End Function
    Private Function GetProductOperationSql(ByRef mProductCode As String, ByRef pDeptCode As String, ByRef pOPRSNO As Integer) As String
        On Error GoTo err_Renamed
        Dim SqlStr As String = ""


        SqlStr = " SELECT TRN.OPR_CODE, MST.OPR_DESC, TRN.OPR_SNO " & vbCrLf _
                & " FROM PRD_OPR_TRN TRN, PRD_OPR_MST MST" & vbCrLf _
                & " WHERE " & vbCrLf _
                & " TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND TRN.COMPANY_CODE=MST.COMPANY_CODE " & vbCrLf _
                & " AND TRN.OPR_CODE=MST.OPR_CODE " & vbCrLf _
                & " AND TRN.DEPT_CODE='" & MainClass.AllowSingleQuote(pDeptCode) & "'" & vbCrLf _
                & " AND TRN.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'"


        SqlStr = SqlStr & vbCrLf _
                & " AND TRN.WEF = (SELECT MAX(WEF) AS WEF FROM PRD_OPR_TRN" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(pDeptCode) & "'" & vbCrLf _
                & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'" & vbCrLf _
                & " AND WEF<= TO_DATE('" & VB6.Format(txtPMemoDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"


        SqlStr = SqlStr & vbCrLf & " AND TRN.OPR_SNO> " & pOPRSNO & ""
        SqlStr = SqlStr & vbCrLf & " ORDER BY TRN.OPR_SNO"

        GetProductOperationSql = SqlStr
        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume	
    End Function

    Private Function GetProductOperationSeq(ByRef mProductCode As String, ByRef pDeptCode As String, ByRef pOPRDesc As String) As Integer

        On Error GoTo err_Renamed
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = OperationQuery(Trim(mProductCode), Trim(pDeptCode), "", Trim(pOPRDesc), Trim(txtPMemoDate.Text), "TRN.OPR_CODE", "MST.OPR_DESC", "TRN.OPR_SNO")

        '    SqlStr = " SELECT TRN.OPR_CODE, MST.OPR_DESC, TRN.OPR_SNO " & vbCrLf _	
        ''            & " FROM PRD_OPR_TRN TRN, PRD_OPR_MST MST" & vbCrLf _	
        ''            & " WHERE " & vbCrLf _	
        ''            & " TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _	
        ''            & " AND TRN.COMPANY_CODE=MST.COMPANY_CODE " & vbCrLf _	
        ''            & " AND TRN.OPR_CODE=MST.OPR_CODE " & vbCrLf _	
        ''            & " AND TRN.DEPT_CODE='" & MainClass.AllowSingleQuote(pDeptCode) & "'" & vbCrLf _	
        ''            & " AND TRN.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'" & vbCrLf _	
        ''            & " AND MST.OPR_DESC='" & MainClass.AllowSingleQuote(pOPRDesc) & "'"	
        '	
        '    SqlStr = SqlStr & vbCrLf _	
        ''            & " AND TRN.WEF = (SELECT MAX(WEF) AS WEF FROM PRD_OPR_TRN" & vbCrLf _	
        ''            & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _	
        ''            & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(pDeptCode) & "'" & vbCrLf _	
        ''            & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'" & vbCrLf _	
        ''            & " AND WEF<= '" & VB6.Format(txtPMemoDate.Text, "DD-MMM-YYYY") & "')"	

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetProductOperationSeq = IIf(IsDBNull(RsTemp.Fields("OPR_SNO").Value), 0, RsTemp.Fields("OPR_SNO").Value)
        Else
            GetProductOperationSeq = 0
        End If

        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume	
    End Function
    Private Function GetLastEntryDate() As String

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = ""

        SqlStr = "SELECT Max(REF_DATE) AS  REF_DATE " & vbCrLf _
            & " FROM PRD_PMEMODEPT_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf _
            & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'" & vbCrLf _
            & " AND PROD_TYPE='" & VB.Left(cboType.Text, 1) & "' AND IS_APPROVED='Y'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            GetLastEntryDate = IIf(IsDBNull(RsTemp.Fields("REF_DATE").Value), "", RsTemp.Fields("REF_DATE").Value)
        End If

        Exit Function
ErrPart:

    End Function
    Private Function CheckRowCount() As Boolean
        On Error GoTo ERR1
        Dim i As Integer
        Dim mItemCode As String
        Dim mQty As Double
        Dim mRowCount As Integer
        Dim mTotQty As Double
        Dim mMainItemCode As String

        mRowCount = 0
        mTotQty = 0
        CheckRowCount = True
        If chkApproved.CheckState = System.Windows.Forms.CheckState.Unchecked Then Exit Function

        With SprdMain
            For i = 1 To .MaxRows
                .Row = i
                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColProdQty
                mQty = Val(.Text)
                mTotQty = mTotQty + mQty

                .Col = ColCRQty
                mQty = mQty + Val(.Text)
                mTotQty = mTotQty + mQty

                .Col = ColMRQty
                mQty = mQty + Val(.Text)
                mTotQty = mTotQty + mQty

                mMainItemCode = GetMainItemCode(mItemCode)

                If mMainItemCode <> mItemCode And mQty > 0 Then
                    CheckRowCount = False
                    MsgInformation("Relationship made for Item : " & mItemCode & " with " & mMainItemCode & ". Cann't be save")
                    Exit Function
                End If
                If mItemCode <> "" And mQty > 0 Then
                    mRowCount = mRowCount + 1
                End If
            Next
        End With

        If mTotQty = 0 Then
            CheckRowCount = False
            MsgInformation("Nothing To Save.")
            Exit Function
        End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
        CheckRowCount = False
    End Function

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Public Sub FrmPMemoDeptWise_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'If lblBookType.Text = "P" Then
        '    Me.Text = "Department Wise Production Memo"
        'ElseIf lblBookType.Text = "M" Then
        '    Me.Text = "Supporting Manufacter Production Memo"
        'Else
        '    Me.Text = "Development Item Production Memo"
        'End If

        SqlStr = ""
        SqlStr = "Select * from PRD_PMEMODEPT_HDR  Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPMemoMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from PRD_PMEMODEPT_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPMemoDetail, ADODB.LockTypeEnum.adLockReadOnly)

        AssignGrid(False)
        Call SetTextLengths()
        Clear1()
        If cmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        On Error GoTo AssignGridErr
        Dim SqlStr As String = ""

        SqlStr = ""
        SqlStr = " SELECT  AUTO_KEY_REF MEMO_NO, TO_CHAR(REF_DATE,'DD/MM/YYYY') MEMO_DATE, " & vbCrLf & " DEPT_CODE FROM_DEPT,SHIFT_CODE,DECODE(PROD_TYPE,'P','Production','Jobwork') AS Prod_Type,REMARKS " & vbCrLf & " FROM PRD_PMEMODEPT_HDR  " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "' "

        SqlStr = SqlStr & vbCrLf & " ORDER BY REF_DATE,AUTO_KEY_REF"
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
            .set_ColWidth(6, 2500)


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
            .set_RowHeight(0, ConRowHeight)
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsPMemoDetail.Fields("ITEM_CODE").DefinedSize
            .set_ColWidth(.Col, 8)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = RsPMemoDetail.Fields("ITEM_DESC").DefinedSize
            .set_ColWidth(.Col, 21)


            .Col = ColItemPartNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            ''.TypeEditLen = RsPMemoDetail.Fields("ITEM_DESC").DefinedSize
            .set_ColWidth(.Col, 12)

            .Col = ColUom
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = RsPMemoDetail.Fields("ITEM_UOM").DefinedSize
            .set_ColWidth(.Col, 4)

            .Col = ColChildStock
            .CellType = SS_CELL_TYPE_BUTTON
            '.Lock = False
            .TypeButtonText = "Show Stock"
            .TypeButtonAlign = SS_CELL_BUTTON_ALIGN_LEFT
            .set_ColWidth(ColChildStock, 8)
            .ColsFrozen = ColChildStock

            .Col = ColPrevStockQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.9999")
            .TypeFloatMin = CDbl("-999999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 7)

            .Col = ColStockQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.9999")
            .TypeFloatMin = CDbl("-999999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 7)

            .Col = ColBatchNo
            '        .CellType = SS_CELL_TYPE_FLOAT	
            '        .TypeFloatDecimalPlaces = 0	
            '        .TypeFloatDecimalChar = Asc(".")	
            '        .TypeFloatMax = "999999999"	
            '        .TypeFloatMin = "-999999999"	
            '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC	
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsPMemoDetail.Fields("BATCH_NO").DefinedSize
            .set_ColWidth(.Col, 8)

            .Col = ColProdQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.9999")
            .TypeFloatMin = CDbl("-999999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 8)

            .Col = ColBreakageQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 7)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)

            .Col = ColCRQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 7)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 114, False, True)

            .Col = ColMRQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 7)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 114 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)

            .Col = ColStockType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = RsPMemoDetail.Fields("STOCK_TYPE").DefinedSize
            .set_ColWidth(.Col, 4)
            .ColHidden = True

            .Col = ColOPR
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = MainClass.SetMaxLength("OPR_DESC", "PRD_OPR_MST", PubDBCn)
            .set_ColWidth(.Col, 14) '' 7.5	

            .Col = ColOPRERCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = RsPMemoDetail.Fields("OPERATOR_CODE").DefinedSize
            .set_ColWidth(.Col, 7.5)
            .ColHidden = False

            .Col = ColOPRERName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = MainClass.SetMaxLength("EMP_NAME", "PAY_CONT_EMPLOYEE_MST", PubDBCn)
            .set_ColWidth(.Col, 10)
            .ColHidden = False

            .Col = ColReason
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = RsPMemoDetail.Fields("REASON").DefinedSize
            .set_ColWidth(.Col, 9)

            .Col = ColToolNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = RsPMemoDetail.Fields("TOOL_NO").DefinedSize
            .set_ColWidth(.Col, 7)
            .ColHidden = False '' True	

            .Col = ColCostPcs
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.9999")
            .TypeFloatMin = CDbl("-999999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .ColHidden = True
            .set_ColWidth(.Col, 6)

            .Col = ColMachineNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = RsPMemoDetail.Fields("MACHINE_NO").DefinedSize
            .set_ColWidth(.Col, 8)

            .Col = ColMachineWorkingHours
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .set_ColWidth(.Col, 8)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)

            .Col = ColBreakDownTime
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .set_ColWidth(.Col, 8)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 114, False, True)


            .Col = ColNoTool
            '.CellType = SS_CELL_TYPE_CHECKBOX
            '.TypeHAlign = SS_CELL_H_ALIGN_CENTER
            '.TypeVAlign = SS_CELL_STATIC_V_ALIGN_TOP
            '.set_ColWidth(.Col, 8)
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .set_ColWidth(.Col, 8)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 114, False, True)

            .Col = ColNoMaterial
            '.CellType = SS_CELL_TYPE_CHECKBOX
            '.TypeHAlign = SS_CELL_H_ALIGN_CENTER
            '.TypeVAlign = SS_CELL_STATIC_V_ALIGN_TOP
            '.set_ColWidth(.Col, 8)
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .set_ColWidth(.Col, 8)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 114, False, True)

            .Col = ColNoOperator
            '.CellType = SS_CELL_TYPE_CHECKBOX
            '.TypeHAlign = SS_CELL_H_ALIGN_CENTER
            '.TypeVAlign = SS_CELL_STATIC_V_ALIGN_TOP
            '.set_ColWidth(.Col, 8)
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .set_ColWidth(.Col, 8)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 114, False, True)

            .Col = ColQAIssue
            '.CellType = SS_CELL_TYPE_CHECKBOX
            '.TypeHAlign = SS_CELL_H_ALIGN_CENTER
            '.TypeVAlign = SS_CELL_STATIC_V_ALIGN_TOP
            '.set_ColWidth(.Col, 8)
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .set_ColWidth(.Col, 8)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 114, False, True)

            .Col = ColPowerCutTime
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .set_ColWidth(.Col, 8)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 114, False, True)

            .Col = ColToolChangeTime
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .set_ColWidth(.Col, 8)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 114, False, True)

            .Col = ColSetupChangeTime
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .set_ColWidth(.Col, 8)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 114, False, True)

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = RsPMemoDetail.Fields("REMARKS").DefinedSize
            .set_ColWidth(.Col, 18)

            'If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
            '    .Row = 0
            '    .Col = ColMRQty
            '    .Text = "Breakage Qty"
            'End If
        End With
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemDesc, ColUom)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColCostPcs, ColCostPcs)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColStockType, ColStockType)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColCRQty, ColCRQty)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColStockQty, ColStockQty)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColOPRERName, ColOPRERName)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPrevStockQty, ColPrevStockQty)
        MainClass.SetSpreadColor(SprdMain, Arow)


        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then RsPMemoDetail.Requery() : Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        With RsPMemoMain
            txtPMemoNo.MaxLength = .Fields("AUTO_KEY_REF").Precision
            txtPMemoDate.MaxLength = 10
            txtDept.MaxLength = .Fields("DEPT_CODE").DefinedSize
            txtEmp.MaxLength = .Fields("EMP_CODE").DefinedSize
            txtRemarks.MaxLength = .Fields("REMARKS").DefinedSize
            txtProdDate.MaxLength = 10
            txtRefTM.MaxLength = 5
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Show1()

        On Error GoTo ERR1
        Dim mProdType As String
        Dim mEntryDate As String
        Dim mDivisionCode As Double
        Dim mDivisionDesc As String

        With RsPMemoMain
            If Not .EOF Then
                lblMKey.Text = .Fields("AUTO_KEY_REF").Value
                txtPMemoNo.Text = IIf(IsDBNull(.Fields("AUTO_KEY_REF").Value), "", .Fields("AUTO_KEY_REF").Value)
                txtPMemoDate.Text = VB6.Format(IIf(IsDBNull(.Fields("REF_DATE").Value), "", .Fields("REF_DATE").Value), "DD/MM/YYYY")
                txtProdDate.Text = VB6.Format(IIf(IsDBNull(.Fields("PROD_DATE").Value), "", .Fields("PROD_DATE").Value), "DD/MM/YYYY")

                txtRefTM.Text = VB6.Format(IIf(IsDBNull(.Fields("PREP_TIME").Value), "", .Fields("PREP_TIME").Value), "HH:MM")

                '            txtEntryDate.Text = Format(IIf(IsNull(!ADDDATE), "", !ADDDATE), "DD/MM/YYYY HH:MM")	
                mEntryDate = IIf(IsDBNull(.Fields("ADDUSER").Value), "", .Fields("ADDUSER").Value) & " - " & VB6.Format(IIf(IsDBNull(.Fields("ADDDATE").Value), "", .Fields("ADDDATE").Value), "DD/MM/YYYY HH:MM")
                mEntryDate = mEntryDate & vbCrLf & IIf(IsDBNull(.Fields("MODUSER").Value), "", .Fields("MODUSER").Value) & " - " & VB6.Format(IIf(IsDBNull(.Fields("MODDATE").Value), "", .Fields("MODDATE").Value), "DD/MM/YYYY HH:MM")
                txtEntryDate.Text = mEntryDate

                txtDept.Text = Trim(IIf(IsDBNull(.Fields("DEPT_CODE").Value), "", .Fields("DEPT_CODE").Value))
                txtDept_Validating(txtDept, New System.ComponentModel.CancelEventArgs(False))
                cboShiftcd.Text = IIf(IsDBNull(.Fields("SHIFT_CODE").Value), "", .Fields("SHIFT_CODE").Value)
                cboLineNo.Text = IIf(IsDBNull(.Fields("LINE_NO").Value), "1", .Fields("LINE_NO").Value)

                mProdType = IIf(IsDBNull(.Fields("PROD_TYPE").Value), "P", .Fields("PROD_TYPE").Value)
                If mProdType = "P" Then
                    cboType.SelectedIndex = 0
                Else
                    cboType.SelectedIndex = 1
                End If

                chkSPD.CheckState = IIf(.Fields("IS_SPD").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                chkApproved.CheckState = IIf(.Fields("IS_APPROVED").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                If RsCompany.Fields("COMPANY_CODE").Value = 1 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Then
                    chkApproved.Enabled = IIf(.Fields("IS_APPROVED").Value = "Y", False, True)
                End If

                txtEmp.Text = IIf(IsDBNull(.Fields("EMP_CODE").Value), "", .Fields("EMP_CODE").Value)
                txtEmp_Validating(txtEmp, New System.ComponentModel.CancelEventArgs(False))
                txtRemarks.Text = IIf(IsDBNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)

                mDivisionCode = IIf(IsDBNull(.Fields("DIV_CODE").Value), -1, .Fields("DIV_CODE").Value)

                If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivisionDesc = Trim(MasterNo)
                    cboDivision.Text = mDivisionDesc
                End If
                cboDivision.Enabled = False

                Call ShowDetail1(mDivisionCode)
                Call MakeEnableDesableField(False)
                cmdPopulate.Enabled = False
            End If
        End With
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsPMemoMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        SprdMain.Enabled = True
        txtPMemoNo.Enabled = True
        cmdSearch.Enabled = True
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume	
    End Sub
    Private Sub ShowDetail1(ByRef mDivisionCode As Double)

        On Error GoTo ERR1
        Dim i As Integer
        Dim SqlStr As String = ""
        Dim mOPRCode As String
        Dim mOPRDesc As String
        Dim mItemCode As String
        Dim mItemUOM As String
        Dim mProdSeq As Integer
        Dim mPrevDept As String
        Dim mOPRERName As String
        Dim mOPRERCode As String
        Dim mBatchNo As String
        Dim xFGBatchNoReq As String

        SqlStr = " SELECT * " & vbCrLf _
            & " FROM PRD_PMEMODEPT_DET  " & vbCrLf _
            & " WHERE AUTO_KEY_REF = " & Val(lblMKey.Text) & " " & vbCrLf _
            & " ORDER BY  SERIAL_NO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPMemoDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsPMemoDetail
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            i = 1
            .MoveFirst()
            Do While Not .EOF
                SprdMain.Row = i

                SprdMain.Col = ColItemCode
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))
                mItemCode = Trim(IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))

                SprdMain.Col = ColItemDesc
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("ITEM_DESC").Value), "", .Fields("ITEM_DESC").Value))

                SprdMain.Col = ColItemPartNo
                If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "CUSTOMER_PART_NO", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    SprdMain.Text = MasterNo
                Else
                    SprdMain.Text = ""
                End If

                SprdMain.Col = ColUom
                SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)
                mItemUOM = IIf(IsDBNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)

                SprdMain.Col = ColBatchNo

                If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DSP_RPT_FLAG='Y'") = True Then
                    mBatchNo = Trim(IIf(IsDBNull(.Fields("BATCH_NO").Value), "", .Fields("BATCH_NO").Value))
                    SprdMain.Text = IIf(mBatchNo > "0", mBatchNo, IIf(mBatchNo = "-1", mBatchNo, ""))
                    xFGBatchNoReq = "Y"
                Else
                    mBatchNo = "X"
                    SprdMain.Text = ""
                    xFGBatchNoReq = "N"
                End If

                mProdSeq = GetProductSeqNo(mItemCode, Trim(txtDept.Text), (txtPMemoDate.Text))
                mPrevDept = GetProductDept(mItemCode, mProdSeq, (txtPMemoDate.Text))

                If mProdSeq > 1 Then
                    mPrevDept = GetProductDept(mItemCode, mProdSeq - 1, (txtPMemoDate.Text))

                    SprdMain.Col = ColPrevStockQty
                    SprdMain.Text = CStr(GetBalanceStockQty(mItemCode, (txtPMemoDate.Text), mItemUOM, Trim(mPrevDept), "ST", "X", ConPH, mDivisionCode, ConStockRefType_PMEMODEPT, Val(txtPMemoNo.Text)))

                    If GetProductionType(mItemCode) = "J" Then
                        SprdMain.Text = CStr(Val(SprdMain.Text) + GetBalanceStockQty(mItemCode, (txtPMemoDate.Text), mItemUOM, Trim(mPrevDept), "CS", "X", ConPH, mDivisionCode, ConStockRefType_PMEMODEPT, Val(txtPMemoNo.Text)))
                    End If
                Else
                    SprdMain.Col = ColPrevStockQty
                    SprdMain.Text = "0.00"
                End If

                SprdMain.Col = ColStockQty ''mRecvQty +	
                SprdMain.Text = CStr(GetBalanceStockQty(mItemCode, (txtPMemoDate.Text), mItemUOM, Trim(txtDept.Text), "ST", mBatchNo, ConPH, mDivisionCode, , , xFGBatchNoReq)) '', ConStockRefType_PMEMODEPT, Val(lblMKey.text)	

                If GetProductionType(mItemCode) = "J" Then
                    SprdMain.Text = CStr(Val(SprdMain.Text) + GetBalanceStockQty(mItemCode, (txtPMemoDate.Text), mItemUOM, Trim(txtDept.Text), "CS", "X", ConPH, mDivisionCode))
                End If



                SprdMain.Col = ColProdQty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("PROD_QTY").Value), "", .Fields("PROD_QTY").Value)))


                SprdMain.Col = ColBreakageQty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("SCRAP_QTY").Value), "", .Fields("SCRAP_QTY").Value)))

                SprdMain.Col = ColCRQty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("REWORK_QTY").Value), "", .Fields("REWORK_QTY").Value)))

                SprdMain.Col = ColMRQty
                SprdMain.Text = "0.00" 'Val(IIf(IsNull(.Fields("CR_QTY").Value), "", .Fields("CR_QTY").Value))	

                SprdMain.Col = ColStockType
                SprdMain.Text = IIf(IsDBNull(.Fields("STOCK_TYPE").Value), "", .Fields("STOCK_TYPE").Value)

                SprdMain.Col = ColCostPcs
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("COST_PCS").Value), "", .Fields("COST_PCS").Value)))

                SprdMain.Col = ColReason
                SprdMain.Text = IIf(IsDBNull(.Fields("REASON").Value), "", .Fields("REASON").Value)

                SprdMain.Col = ColToolNo
                SprdMain.Text = IIf(IsDBNull(.Fields("TOOL_NO").Value), "", .Fields("TOOL_NO").Value)

                mOPRCode = IIf(IsDBNull(.Fields("OPR_CODE").Value), "", .Fields("OPR_CODE").Value)

                If MainClass.ValidateWithMasterTable(mOPRCode, "OPR_CODE", "OPR_DESC", "PRD_OPR_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'") = True Then
                    mOPRDesc = MasterNo
                Else
                    mOPRDesc = ""
                End If
                SprdMain.Col = ColOPR
                SprdMain.Text = mOPRDesc

                SprdMain.Col = ColOPRERCode
                SprdMain.Text = IIf(IsDBNull(.Fields("OPERATOR_CODE").Value), "", .Fields("OPERATOR_CODE").Value)
                mOPRERCode = IIf(IsDBNull(.Fields("OPERATOR_CODE").Value), "", .Fields("OPERATOR_CODE").Value)

                If MainClass.ValidateWithMasterTable(mOPRERCode, "EMP_CODE", "EMP_NAME", "PAY_CONT_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mOPRERName = MasterNo
                Else
                    If MainClass.ValidateWithMasterTable(mOPRERCode, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mOPRERName = MasterNo
                    Else
                        mOPRERName = ""
                    End If
                End If
                SprdMain.Col = ColOPRERName
                SprdMain.Text = mOPRERName


                SprdMain.Col = ColMachineNo
                SprdMain.Text = IIf(IsDBNull(.Fields("MACHINE_NO").Value), "", .Fields("MACHINE_NO").Value)

                SprdMain.Col = ColBreakDownTime
                SprdMain.Text = IIf(IsDBNull(.Fields("BREAKDOWN_TIME").Value), 0, .Fields("BREAKDOWN_TIME").Value)

                SprdMain.Col = ColMachineWorkingHours
                SprdMain.Text = IIf(IsDBNull(.Fields("MACHINE_WORKING_HOURS").Value), 0, .Fields("MACHINE_WORKING_HOURS").Value)
                '

                SprdMain.Col = ColNoTool
                SprdMain.Text = IIf(IsDBNull(.Fields("NO_TOOL").Value), 0, .Fields("NO_TOOL").Value) ' IIf(.Fields("NO_TOOL").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)


                SprdMain.Col = ColNoMaterial
                SprdMain.Text = IIf(IsDBNull(.Fields("NO_MATERIAL").Value), 0, .Fields("NO_MATERIAL").Value) 'IIf(.Fields("NO_MATERIAL").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)


                SprdMain.Col = ColNoOperator
                SprdMain.Text = IIf(IsDBNull(.Fields("NO_OPERATOR").Value), 0, .Fields("NO_OPERATOR").Value) 'IIf(.Fields("NO_OPERATOR").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                SprdMain.Col = ColPowerCutTime
                SprdMain.Text = IIf(IsDBNull(.Fields("POWER_CUT_TIME").Value), 0, .Fields("POWER_CUT_TIME").Value)

                SprdMain.Col = ColToolChangeTime
                SprdMain.Text = IIf(IsDBNull(.Fields("TOOL_CHANGE_TIME").Value), 0, .Fields("TOOL_CHANGE_TIME").Value)

                SprdMain.Col = ColSetupChangeTime
                SprdMain.Text = IIf(IsDBNull(.Fields("SETUP_CHANGE_TIME").Value), 0, .Fields("SETUP_CHANGE_TIME").Value)

                SprdMain.Col = ColQAIssue
                SprdMain.Value = IIf(IsDBNull(.Fields("QA_ISSUE").Value), 0, .Fields("QA_ISSUE").Value) 'IIf(.Fields("QA_ISSUE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                SprdMain.Col = ColRemarks
                SprdMain.Text = IIf(IsDBNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)

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
        MainClass.ButtonStatus(Me, XRIGHT, RsPMemoMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub Clear1()

        lblMKey.Text = ""
        txtPMemoNo.Text = ""
        txtRefTM.Text = GetServerTime()

        '    If CDate(txtRefTM.Text) < CDate("09:00") Then	
        '        txtPMemoDate.Text = Format(RunDate - 1, "DD/MM/YYYY")	
        '    Else	

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
            txtPMemoDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
            txtProdDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        Else
            txtPMemoDate.Text = VB6.Format(PubCurrDate, "DD/MM/YYYY")
            txtProdDate.Text = VB6.Format(PubCurrDate, "DD/MM/YYYY")
        End If

        '    End If	

        txtDept.Text = ""
        lblDept.Text = ""
        cboShiftcd.SelectedIndex = 0
        cboLineNo.SelectedIndex = 0
        'cboType.SelectedIndex = 0
        If lblBookType.Text = "P" Then
            cboType.SelectedIndex = 0
        ElseIf lblBookType.Text = "J" Then
            cboType.SelectedIndex = 1
        End If
        cboType.Enabled = False

        txtEmp.Text = ""
        lblEmp.Text = ""

        txtEmp.Text = PubUserID
        lblEmp.Text = ""
        If MainClass.ValidateWithMasterTable(txtEmp.Text, "USER_ID", "EMP_NAME", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            lblEmp.Text = MasterNo
        End If

        txtRemarks.Text = ""
        chkSPD.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkSPD.Visible = IIf(RsCompany.Fields("COMPANY_CODE").Value = 1, True, False)
        chkSPD.Enabled = False

        chkApproved.CheckState = System.Windows.Forms.CheckState.Checked

        'If RsCompany.Fields("COMPANY_CODE").Value = 1 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Then
        '    chkApproved.Enabled = True
        '    chkApproved.Visible = True
        'Else
        chkApproved.Enabled = False
        chkApproved.Visible = True
        'End If
        cboDivision.Text = GetDefaultDivision()             ''cboDivision.SelectedIndex = -1
        cboDivision.Enabled = True

        txtEntryDate.Text = VB6.Format(PubCurrDate, "DD/MM/YYYY") & " " & GetServerTime()
        cmdPopulate.Enabled = False
        Call MakeEnableDesableField(True)
        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsPMemoMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub MakeEnableDesableField(ByRef mMode As Boolean)
        txtPMemoDate.Enabled = True '' IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, mMode)
        txtDept.Enabled = mMode
        CmdSearchDept.Enabled = mMode

    End Sub
    Private Sub FrmPMemoDeptWise_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub FrmPMemoDeptWise_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Public Sub FrmPMemoDeptWise_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Set PvtDBCn = New ADODB.Connection	
        'PvtDBCn.Open StrConn	

        CurrFormHeight = 7245
        CurrFormWidth = 11355

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)

        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(10935)
        Call FillCbo()
        AdoDCMain.Visible = False
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
        Dim mRow As Short
        Dim mPrevRow As Short
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mUOM As String
        Dim mProdQty As Double
        Dim mReworkQty As Double
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mOperationDesc As String
        Dim mOperationNewDesc As String
        Dim mOprSeq As Integer
        Dim mIsOptionalOPR As String
        Dim mProdSeq As Integer
        Dim mPrevDept As String
        Dim mDivisionCode As Double
        Dim mBatchNo As String
        Dim mItemPartNo As String

        mCol = SprdMain.ActiveCol
        mRow = SprdMain.ActiveRow

        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemDesc, 0))


        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If

        With SprdMain
            mPrevRow = mRow - 1
            .Row = mPrevRow
            .Col = ColItemCode
            mItemCode = Trim(.Text)

            .Col = ColItemDesc
            mItemDesc = Trim(.Text)

            .Col = ColItemPartNo
            mItemPartNo = Trim(.Text)

            .Col = ColUom
            mUOM = Trim(.Text)

            .Col = ColProdQty
            mProdQty = Val(.Text)

            .Col = ColBatchNo
            mBatchNo = Trim(.Text)

            .Col = ColOPR
            mOperationDesc = Trim(.Text)

            If mOperationDesc = "" Then
                mOprSeq = 0
            Else
                mOprSeq = GetProductOperationSeq(mItemCode, Trim(txtDept.Text), mOperationDesc)
            End If
            If eventArgs.keyCode = System.Windows.Forms.Keys.F5 And mRow > 1 And mOperationDesc <> "" Then

                mSqlStr = GetProductOperationSql(mItemCode, Trim(txtDept.Text), mOprSeq)
                MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                If RsTemp.EOF = False Then
                    Do While RsTemp.EOF = False
                        mOperationNewDesc = IIf(IsDBNull(RsTemp.Fields("OPR_DESC").Value), "", RsTemp.Fields("OPR_DESC").Value)
                        If DuplicateOperation(mItemCode, mOperationNewDesc, "") = True Then RsTemp.MoveNext() : GoTo NextRow
                        mOprSeq = GetProductOperationSeq(mItemCode, Trim(txtDept.Text), mOperationNewDesc)
                        mIsOptionalOPR = GetIsOptionalOPR(mItemCode, Trim(txtDept.Text), Str(mOprSeq), (txtPMemoDate.Text))
                        .Row = mRow
                        .Col = ColItemCode
                        .Text = mItemCode

                        .Col = ColItemDesc
                        .Text = mItemDesc

                        .Col = ColItemPartNo
                        .Text = mItemPartNo

                        .Col = ColUom
                        .Text = mUOM

                        .Col = ColProdQty
                        .Text = IIf(mIsOptionalOPR = "N", mProdQty, 0)

                        .Col = ColBatchNo
                        .Text = IIf(mBatchNo = "", "", Trim(mBatchNo))

                        .Col = ColOPR
                        .Text = mOperationNewDesc

                        .Col = ColStockType
                        .Text = IIf(Trim(.Text) = "", IIf(VB.Left(cboType.Text, 1) = "P", "ST", "CS"), Trim(.Text))

                        mProdSeq = GetProductSeqNo(mItemCode, Trim(txtDept.Text), (txtPMemoDate.Text))
                        If mProdSeq > 1 Then
                            mPrevDept = GetProductDept(mItemCode, mProdSeq - 1, (txtPMemoDate.Text))

                            .Col = ColPrevStockQty
                            .Text = CStr(GetBalanceStockQty(mItemCode, (txtPMemoDate.Text), mUOM, Trim(mPrevDept), "ST", "X", ConPH, mDivisionCode))

                            If GetProductionType(mItemCode) = "J" Then
                                SprdMain.Text = CStr(Val(SprdMain.Text) + GetBalanceStockQty(mItemCode, (txtPMemoDate.Text), mUOM, Trim(mPrevDept), "CS", "", ConPH, mDivisionCode))
                            End If


                        Else
                            SprdMain.Col = ColPrevStockQty
                            SprdMain.Text = "0.00"
                        End If

                        SprdMain.Col = ColStockQty ''mRecvQty +	
                        SprdMain.Text = CStr(GetBalanceStockQty(mItemCode, (txtPMemoDate.Text), mUOM, Trim(txtDept.Text), "ST", "X", ConPH, mDivisionCode)) '', ConStockRefType_PMEMODEPT, Val(lblMKey.text))	

                        If GetProductionType(mItemCode) = "J" Then
                            SprdMain.Text = CStr(Val(SprdMain.Text) + GetBalanceStockQty(mItemCode, (txtPMemoDate.Text), mUOM, Trim(txtDept.Text), "CS", "", ConPH, mDivisionCode))
                        End If


                        SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColItemCode, mRow, ColItemCode, mRow, False))
                        RsTemp.MoveNext()
                        If RsTemp.EOF = False Then
                            .MaxRows = .MaxRows + 1
                            mRow = mRow + 1
                        End If
NextRow:
                    Loop
                End If
                MainClass.SetFocusToCell(SprdMain, mRow, ColOPR)
            End If
        End With

        '    SprdMain.Refresh	
    End Sub
    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        '    With SprdMain	
        '        SprdMain_LeaveCell .ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False	
        '    End With	

    End Sub

    Private Sub txtEntryDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEntryDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtPMemoDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPMemoDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPMemoDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPMemoDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtPMemoDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtPMemoDate.Text) Then
            MsgInformation("Invalid Date")
            Cancel = True
            GoTo EventExitSub
        End If

        If Trim(cboShiftcd.Text) = "C" Then
            txtProdDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(VB6.Format(txtPMemoDate.Text, "DD/MM/YYYY"))))
            txtProdDate.Text = VB6.Format(txtProdDate.Text, "DD/MM/YYYY")
        Else
            txtProdDate.Text = VB6.Format(txtPMemoDate.Text, "DD/MM/YYYY")
        End If

        If FYChk((txtProdDate.Text)) = False Then
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchDept_Click(CmdSearchDept, New System.EventArgs())
    End Sub

    Private Sub txtDept_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDept.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        If Trim(txtDept.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable(txtDept.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            lblDept.Text = MasterNo
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
        Call cmdSearchEmp_Click(cmdSearchEmp, New System.EventArgs())
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchEmp_Click(cmdSearchEmp, New System.EventArgs())
    End Sub

    Private Sub txtEmp_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmp.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        If Trim(txtEmp.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtEmp.Text, "USER_ID", "EMP_NAME", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            lblEmp.Text = MasterNo
        End If

        'If ADDMode = True Then
        '    SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_LEAVE_DATE IS NULL "
        'Else
        '    SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        'End If

        'txtEmp.Text = VB6.Format(txtEmp.Text, "000000")
        'If MainClass.ValidateWithMasterTable(txtEmp.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = True Then
        '    lblEmp.Text = MasterNo
        'Else
        '    MsgInformation("Invalid Employee Code")
        '    Cancel = True
        'End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPMemoNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPMemoNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtPMemoNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPMemoNo.DoubleClick
        Call cmdsearch_Click(cmdSearch, New System.EventArgs())
    End Sub
    Private Sub txtPMemoNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPMemoNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPMemoNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtPMemoNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPMemoNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdSearch, New System.EventArgs())
    End Sub
    Public Sub txtPMemoNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPMemoNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mPMemoNo As Double

        If Trim(txtPMemoNo.Text) = "" Then GoTo EventExitSub

        If Len(txtPMemoNo.Text) < 6 Then
            txtPMemoNo.Text = Val(txtPMemoNo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        If MODIFYMode = True And RsPMemoMain.EOF = False Then mPMemoNo = RsPMemoMain.Fields("AUTO_KEY_REF").Value

        SqlStr = "Select * From PRD_PMEMODEPT_HDR  " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_REF=" & Val(txtPMemoNo.Text) & "" & vbCrLf & " AND BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPMemoMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsPMemoMain.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such P.Memo.", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "Select * From PRD_PMEMODEPT_HDR  " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_REF=" & Val(CStr(mPMemoNo)) & "" & vbCrLf & " AND BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "' "

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPMemoMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtProdDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProdDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtProdDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtProdDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtProdDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtProdDate.Text) Then
            MsgInformation("Invalid Date")
            Cancel = True
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRefTM_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRefTM.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdPopulateDept_Click(sender As Object, e As EventArgs) Handles cmdPopulateDept.Click

        On Error GoTo ErrorHandler
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim i As Integer
        Dim mItemCode As String
        Dim pOprCode As String = ""
        If Trim(txtDept.Text) = "" Then Exit Sub
        If Trim(txtPMemoDate.Text) = "" Then Exit Sub
        If Not IsDate(txtPMemoDate.Text) Then Exit Sub
        If VB.Left(cboType.Text, 1) = "J" Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = " SELECT DISTINCT " & vbCrLf _
            & " IH.PRODUCT_CODE AS PRODUCT_CODE, INVMST.ITEM_SHORT_DESC, INVMST.ISSUE_UOM " & vbCrLf _
            & " FROM PRD_PRODSEQUENCE_DET IH, INV_ITEM_MST INVMST" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.COMPANY_CODe=INVMST.COMPANY_CODe " & vbCrLf _
            & " AND IH.PRODUCT_CODE=INVMST.ITEM_CODE " & vbCrLf _
            & " AND IH.DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        i = 1

        If RsTemp.EOF = False Then
            With SprdMain
                Do While RsTemp.EOF = False
                    .Row = i
                    .Col = ColItemCode
                    .Text = Trim(IIf(IsDBNull(RsTemp.Fields("PRODUCT_CODE").Value), "", RsTemp.Fields("PRODUCT_CODE").Value))
                    mItemCode = Trim(IIf(IsDBNull(RsTemp.Fields("PRODUCT_CODE").Value), "", RsTemp.Fields("PRODUCT_CODE").Value))

                    '.Col = ColItemDesc
                    '.Text = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value))

                    '.Col = ColUom
                    '.Text = IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)

                    '.Col = ColProdQty
                    '.Text = "0.00"

                    '.Col = ColCRQty
                    '.Text = "0.00"

                    '.Col = ColMRQty
                    '.Text = "0.00"

                    '.Col = ColStockType
                    '.Text = "ST"

                    '.Col = ColCostPcs
                    '.Text = "0.00"
                    Dim mSqlStr As String = ""
                    Dim RsTempOPr As ADODB.Recordset = Nothing

                    mSqlStr = GetProductOperationSql(mItemCode, Trim(txtDept.Text), 0)
                    MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempOPr, ADODB.LockTypeEnum.adLockReadOnly)

                    If RsTempOPr.EOF = False Then
                        Do While RsTempOPr.EOF = False
                            pOprCode = IIf(IsDBNull(RsTempOPr.Fields("OPR_DESC").Value), "", RsTempOPr.Fields("OPR_DESC").Value)
                            Call FillItemDescPart(mItemCode, i, pOprCode)

                            RsTempOPr.MoveNext()
                            If RsTempOPr.EOF = False Then
                                i = i + 1
                                .MaxRows = i
                            End If
                        Loop
                    Else
                        pOprCode = ""
                        Call FillItemDescPart(mItemCode, i, pOprCode)
                    End If

                    i = i + 1
                    .MaxRows = i
                    RsTemp.MoveNext()
                Loop
                Call FormatSprdMain(-1)
            End With
        Else
            MsgInformation("No BOM Found For Such Dept.")
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        'cmdPopulate.Enabled = False
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        If Err.Description = "" Then Exit Sub
    End Sub

    Private Sub cmdSearchItem_Click(sender As Object, e As EventArgs) Handles cmdSearchItem.Click

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
                    MainClass.SetFocusToCell(SprdMain, I, ColItemCode)
                    mSearchStartRow = I + 1
                    GoTo NextRec
                End If

                .Col = ColItemDesc
                mFindItemName = Trim(.Text)

                '            If mSearchItem = mFindItemName Then			
                If InStr(1, mFindItemName, mSearchItem, CompareMethod.Text) > 0 Then
                    MainClass.SetFocusToCell(SprdMain, I, ColItemCode)
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
    Private Sub SprdMain_ButtonClicked(sender As Object, e As _DSpreadEvents_ButtonClickedEvent) Handles SprdMain.ButtonClicked

        If e.col = ColChildStock And e.row > 0 Then
            Call ShowFormChildStock(e.col, e.row)
        End If
    End Sub
    Private Sub ShowFormChildStock(ByRef pCol As Integer, ByRef pRow As Integer)
        Dim mItemCode As String
        Dim mDivisionCode As Double

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If

        With SprdMain
            .Row = pRow

            .Col = ColItemCode
            mItemCode = .Text
        End With
        If mItemCode = "" Then Exit Sub


        With frmChildBOMStock
            .lblItemCode.Text = mItemCode

            .lblProductionType.Text = lblBookType.Text
            .lblDate.Text = VB6.Format(txtPMemoDate.Text, "DD/MM/YYYY")
            .lblDeptCode.Text = txtDept.Text

            .lblDivision.Text = mDivisionCode
            .lblRefNo.Text = txtPMemoNo.Text
            .lblRefType.Text = ConStockRefType_PMEMODEPT
            .ShowDialog()
        End With
        frmChildBOMStock.Hide()
        frmChildBOMStock.Dispose()
        frmChildBOMStock.Close()

    End Sub

    Private Sub FrmPMemoDeptWise_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 210, mReFormWidth - 210, mReFormWidth))
        SprdView.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        FraFront.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
End Class
