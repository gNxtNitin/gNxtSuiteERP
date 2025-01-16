Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmCustComplaint
    Inherits System.Windows.Forms.Form
    Dim RsCustComplaint As ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean

    Private Sub cboCAPA_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCAPA.SelectedIndexChanged
        If cboCAPA.Text = "No" Then
            txtStartDate.Text = ""
            txtCloseDate.Text = ""
            txtStartDate.Enabled = False
            txtCloseDate.Enabled = False
        Else
            txtStartDate.Enabled = True
            txtCloseDate.Enabled = True
        End If
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtNumber.Enabled = False
            cmdSearchNo.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            If RsCustComplaint.EOF = False Then RsCustComplaint.MoveFirst()
            Show1()
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
        'Resume
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        On Error Resume Next
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart

        If txtNumber.Text = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        If Not RsCustComplaint.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "QAL_CUST_COMPLAINT_TRN", (txtNumber.Text), RsCustComplaint) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM QAL_CUST_COMPLAINT_TRN WHERE AUTO_KEY_CCOMP=" & Val(lblMkey.Text) & "")
                PubDBCn.CommitTrans()
                RsCustComplaint.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Errors.Clear()
        RsCustComplaint.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr


        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsCustComplaint, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            txtNumber.Enabled = False
            cmdSearchNo.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification = False Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Update1 = True Then
            ADDMode = False
            MODIFYMode = False
            txtNumber_Validating(txtNumber, New System.ComponentModel.CancelEventArgs(False))
            If CmdAdd.Enabled = True Then CmdAdd.Focus()
        Else
            MsgInformation("Record not saved")
            GoTo ErrorHandler
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Function Update1() As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mSlipNo As Double
        Dim mNeedCapa As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mNeedCapa = VB.Left(cboCAPA.Text, 1)

        SqlStr = ""
        mSlipNo = Val(txtNumber.Text)
        If Val(txtNumber.Text) = 0 Then
            mSlipNo = AutoGenKeyNo()
        End If
        txtNumber.Text = CStr(mSlipNo)
        If ADDMode = True Then
            lblMkey.Text = CStr(mSlipNo)
            SqlStr = " INSERT INTO QAL_CUST_COMPLAINT_TRN " & vbCrLf _
                            & " (AUTO_KEY_CCOMP,COMPANY_CODE," & vbCrLf _
                            & " COMPLAINT_DATE,SUPP_CUST_CODE,MODE_OF_COMPL,PRODUCT_CODE,REF_NO," & vbCrLf _
                            & " PART_NO,CUST_PROB_STATED,DESP_DATE,BATCH_NO,REPORT_NO,OTH_INVEST, " & vbCrLf _
                            & " INVEST_CUST_END,PRODUCT_ACTION,CAPA_ACTION,START_DATE,SIGN_DATE, " & vbCrLf _
                            & " CLOSE_DATE,PRE_EMP_CODE,SIGN_EMP_CODE,ADDUSER, ADDDATE, MODUSER, MODDATE) " & vbCrLf _
                            & " VALUES ( " & vbCrLf _
                            & " " & mSlipNo & "," & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                            & " TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(txtCustomer.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtModeComplaint.Text) & "','" & MainClass.AllowSingleQuote(txtProduct.Text) & "'," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtRefNo.Text) & "','" & MainClass.AllowSingleQuote(lblPartNo.Text) & "'," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtCustComplaint.Text) & "',TO_DATE('" & VB6.Format(txtDespatchDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtBatchNo.Text) & "','" & MainClass.AllowSingleQuote(txtReportNo.Text) & "'," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtInhouseInvest.Text) & "','" & MainClass.AllowSingleQuote(txtCustomerInvest.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtDisConAction.Text) & "','" & mNeedCapa & "'," & vbCrLf _
                            & " TO_DATE('" & VB6.Format(txtStartDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(txtSignDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " TO_DATE('" & VB6.Format(txtCloseDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(TxtPreparedBy.Text) & "','" & MainClass.AllowSingleQuote(txtApprovedBy.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"
        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE QAL_CUST_COMPLAINT_TRN SET " & vbCrLf _
                    & " AUTO_KEY_CCOMP=" & mSlipNo & ",COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                    & " COMPLAINT_DATE=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCustomer.Text) & "', " & vbCrLf _
                    & " MODE_OF_COMPL='" & MainClass.AllowSingleQuote(txtModeComplaint.Text) & "', " & vbCrLf _
                    & " PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProduct.Text) & "',REF_NO='" & MainClass.AllowSingleQuote(txtRefNo.Text) & "', " & vbCrLf _
                    & " PART_NO='" & MainClass.AllowSingleQuote(lblPartNo.Text) & "',CUST_PROB_STATED='" & MainClass.AllowSingleQuote(txtCustComplaint.Text) & "', " & vbCrLf _
                    & " DESP_DATE=TO_DATE('" & VB6.Format(txtDespatchDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),BATCH_NO='" & MainClass.AllowSingleQuote(txtBatchNo.Text) & "', " & vbCrLf _
                    & " REPORT_NO='" & MainClass.AllowSingleQuote(txtReportNo.Text) & "',OTH_INVEST='" & MainClass.AllowSingleQuote(txtInhouseInvest.Text) & "', " & vbCrLf _
                    & " INVEST_CUST_END='" & MainClass.AllowSingleQuote(txtCustomerInvest.Text) & "',PRODUCT_ACTION='" & MainClass.AllowSingleQuote(txtDisConAction.Text) & "', " & vbCrLf _
                    & " CAPA_ACTION='" & mNeedCapa & "',START_DATE=TO_DATE('" & VB6.Format(txtStartDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " SIGN_DATE=TO_DATE('" & VB6.Format(txtSignDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " CLOSE_DATE=TO_DATE('" & VB6.Format(txtCloseDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " PRE_EMP_CODE='" & MainClass.AllowSingleQuote(TxtPreparedBy.Text) & "', " & vbCrLf _
                    & " SIGN_EMP_CODE='" & MainClass.AllowSingleQuote(txtApprovedBy.Text) & "', " & vbCrLf _
                    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                    & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND AUTO_KEY_CCOMP =" & Val(lblMkey.Text) & ""
        End If

        PubDBCn.Execute(SqlStr)
        Update1 = True
        PubDBCn.CommitTrans()
        txtNumber.Text = CStr(mSlipNo)
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsCustComplaint.Requery()
        MsgBox(Err.Description)
        '    Resume
    End Function
    Private Function AutoGenKeyNo() As Double

        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset
        Dim mAutoGen As Double
        Dim SqlStr As String
        Dim mMaxValue As String

        mAutoGen = 1
        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_CCOMP)  " & vbCrLf & " FROM QAL_CUST_COMPLAINT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_CCOMP,LENGTH(AUTO_KEY_CCOMP)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

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
        AutoGenKeyNo = CDbl(mAutoGen & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"))
        RsAutoGen.Close()
        RsAutoGen = Nothing
        Exit Function
AutogenErr:
        MsgBox(Err.Description)
    End Function

    Private Sub cmdSearchAppBy_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchAppBy.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND EMP_LEAVE_DATE IS NULL "
        If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            txtApprovedBy.Text = AcName1
            lblApprovedBy.text = AcName
        End If
    End Sub

    Private Sub CmdSearchCustomer_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSearchCustomer.Click
        Dim SqlStr As String
        SqlStr = "SELECT DISTINCT A.SUPP_CUST_NAME,A.SUPP_CUST_CODE " & vbCrLf & " FROM FIN_SUPP_CUST_MST A, FIN_SUPP_CUST_DET B " & vbCrLf & " WHERE A.COMPANY_CODE = B.COMPANY_CODE " & vbCrLf & " AND A.SUPP_CUST_CODE = B.SUPP_CUST_CODE " & vbCrLf & " AND A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " ORDER BY A.SUPP_CUST_CODE "
        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            txtCustomer.Text = AcName1
            lblCustomer.text = AcName
            txtCustomer_Validating(txtCustomer, New System.ComponentModel.CancelEventArgs(False)) ''_Validate(False)
        End If


    End Sub

    Private Sub cmdSearchNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchNo.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_CCOMP,LENGTH(AUTO_KEY_CCOMP)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
        If MainClass.SearchGridMaster(txtNumber.Text, "QAL_CUST_COMPLAINT_TRN", "AUTO_KEY_CCOMP", "COMPLAINT_DATE", "SUPP_CUST_CODE", , SqlStr) = True Then
            txtNumber.Text = AcName
            Call txtNumber_Validating(txtNumber, New System.ComponentModel.CancelEventArgs(False)) ''_Validate(False)
        End If
    End Sub

    Private Sub CmdSearchProduct_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSearchProduct.Click
        Dim SqlStr As String
        SqlStr = "SELECT A.ITEM_CODE, B.ITEM_SHORT_DESC, B.CUSTOMER_PART_NO " & vbCrLf _
                & " FROM FIN_SUPP_CUST_DET A ,INV_ITEM_MST B  " & vbCrLf _
                & " WHERE A.COMPANY_CODE =B.COMPANY_CODE " & vbCrLf _
                & " AND A.ITEM_CODE =  B.ITEM_CODE " & vbCrLf _
                & " AND A.COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                & " AND A.SUPP_CUST_CODE ='" & MainClass.AllowSingleQuote(txtCustomer.Text) & "' " & vbCrLf _
                & " ORDER BY A.ITEM_CODE   "
        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            txtProduct.Text = AcName
            lblProduct.text = AcName1
            txtProduct_Validating(txtProduct, New System.ComponentModel.CancelEventArgs(False)) ''_Validate(False)
        End If
    End Sub

    Private Sub CmdSearchPrpBy_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSearchPrpBy.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND EMP_LEAVE_DATE IS NULL "
        If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            TxtPreparedBy.Text = AcName1
            lblPreparedBy.text = AcName
        End If
    End Sub

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Private Sub ViewGrid()

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        ADataGrid.Refresh
            FormatSprdView()
            SprdView.Refresh()
            SprdView.Focus()
            SprdView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            SprdView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsCustComplaint, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmCustComplaint_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Customer Complaint Form"

        SqlStr = "Select * From QAL_CUST_COMPLAINT_TRN WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCustComplaint, ADODB.LockTypeEnum.adLockReadOnly)


        Call AssignGrid(False)
        Call SetTextLengths()
        Call Clear1()
        If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        Resume
    End Sub

    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = ""

        SqlStr = " SELECT " & vbCrLf & " AUTO_KEY_CCOMP AS COMPLAINT_NUMBER,TO_CHAR(COMPLAINT_DATE,'DD/MM/YYYY') AS COMPLAINT_DATE, " & vbCrLf & " SUPP_CUST_CODE,MODE_OF_COMPL,PRODUCT_CODE,REF_NO,PART_NO,  " & vbCrLf & " CUST_PROB_STATED,TO_CHAR(DESP_DATE,'DD/MM/YYYY') AS DESP_DATE,BATCH_NO,REPORT_NO " & vbCrLf & " FROM QAL_CUST_COMPLAINT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_CCOMP,LENGTH(AUTO_KEY_CCOMP)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " ORDER BY AUTO_KEY_CCOMP"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmCustComplaint_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmCustComplaint_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Left = 0
        Me.Top = 0
        Me.Height = VB6.TwipsToPixelsY(6765)
        Me.Width = VB6.TwipsToPixelsX(9285)
        cboCAPA.Items.Add("Yes")
        cboCAPA.Items.Add("No")
        ADDMode = False
        MODIFYMode = False
        FormActive = False
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub Clear1()

        On Error GoTo ClearErr

        lblMkey.Text = ""
        txtNumber.Text = ""
        txtDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtCustomer.Text = ""
        lblCustomer.Text = ""
        txtModeComplaint.Text = ""
        txtProduct.Text = ""
        lblProduct.Text = ""
        txtRefNo.Text = ""
        lblPartNo.Text = ""
        txtCustComplaint.Text = ""
        txtDespatchDate.Text = ""
        txtBatchNo.Text = ""
        txtReportNo.Text = ""
        txtInhouseInvest.Text = ""
        txtCustomerInvest.Text = ""
        txtDisConAction.Text = ""
        cboCAPA.SelectedIndex = 0
        txtStartDate.Text = ""
        txtSignDate.Text = ""
        txtCloseDate.Text = ""
        TxtPreparedBy.Text = ""
        lblPreparedBy.Text = ""
        txtApprovedBy.Text = ""
        lblApprovedBy.Text = ""
        Call MakeEnableDesableField(True)
        MainClass.ButtonStatus(Me, XRIGHT, RsCustComplaint, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ClearErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 600)
            .set_ColWidth(0, 500 * 2)
            .set_ColWidth(1, 500 * 2)
            .set_ColWidth(2, 500 * 2)
            .set_ColWidth(3, 500 * 2)
            .set_ColWidth(4, 500 * 4)
            .set_ColWidth(5, 500 * 2)
            .set_ColWidth(6, 500 * 2)
            .set_ColWidth(7, 500 * 2)
            .set_ColWidth(8, 500 * 4)
            .set_ColWidth(9, 500 * 2)
            .set_ColWidth(10, 500 * 2)
            .set_ColWidth(11, 500 * 3)
            .ColsFrozen = 2
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1

        txtNumber.Maxlength = RsCustComplaint.Fields("AUTO_KEY_CCOMP").Precision
        txtDate.Maxlength = RsCustComplaint.Fields("COMPLAINT_DATE").DefinedSize - 6
        txtCustomer.Maxlength = RsCustComplaint.Fields("SUPP_CUST_CODE").DefinedSize
        txtModeComplaint.Maxlength = RsCustComplaint.Fields("MODE_OF_COMPL").DefinedSize
        txtProduct.Maxlength = RsCustComplaint.Fields("PRODUCT_CODE").DefinedSize
        txtRefNo.Maxlength = RsCustComplaint.Fields("REF_NO").DefinedSize
        txtCustComplaint.Maxlength = RsCustComplaint.Fields("CUST_PROB_STATED").DefinedSize
        txtDespatchDate.Maxlength = RsCustComplaint.Fields("DESP_DATE").DefinedSize - 6
        txtBatchNo.Maxlength = RsCustComplaint.Fields("BATCH_NO").DefinedSize
        txtReportNo.Maxlength = RsCustComplaint.Fields("REPORT_NO").DefinedSize
        txtInhouseInvest.Maxlength = RsCustComplaint.Fields("OTH_INVEST").DefinedSize
        txtCustomerInvest.Maxlength = RsCustComplaint.Fields("INVEST_CUST_END").DefinedSize
        txtDisConAction.Maxlength = RsCustComplaint.Fields("PRODUCT_ACTION").DefinedSize
        txtStartDate.Maxlength = RsCustComplaint.Fields("START_DATE").DefinedSize - 6
        txtSignDate.Maxlength = RsCustComplaint.Fields("SIGN_DATE").DefinedSize - 6
        txtCloseDate.Maxlength = RsCustComplaint.Fields("CLOSE_DATE").DefinedSize - 6
        TxtPreparedBy.Maxlength = RsCustComplaint.Fields("PRE_EMP_CODE").DefinedSize
        txtApprovedBy.Maxlength = RsCustComplaint.Fields("SIGN_EMP_CODE").DefinedSize
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub

    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim ii As Integer
        FieldsVarification = True

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsCustComplaint.EOF = True Then Exit Function

        If Trim(txtDate.Text) = "" Then
            MsgInformation("Date is empty, So unable to save.")
            txtDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtCustomer.Text) = "" Then
            MsgInformation("Customer is empty, So unable to save.")
            txtCustomer.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtModeComplaint.Text) = "" Then
            MsgInformation("Mode of Complaint is empty, So unable to save.")
            txtModeComplaint.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtProduct.Text) = "" Then
            MsgInformation("Product Code is empty, So unable to save.")
            txtProduct.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtRefNo.Text) = "" Then
            MsgInformation("Reference No. is empty, So unable to save.")
            txtRefNo.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtCustComplaint.Text) = "" Then
            MsgInformation("Customer Complaint is empty, So unable to save.")
            txtCustComplaint.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtDespatchDate.Text) = "" Then
            MsgInformation("Despatch Date is empty, So unable to save.")
            txtDespatchDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtBatchNo.Text) = "" Then
            MsgInformation("Batch No. is empty, So unable to save.")
            txtBatchNo.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtReportNo.Text) = "" Then
            MsgInformation("Report No. is empty, So unable to save.")
            txtReportNo.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtInhouseInvest.Text) = "" Then
            MsgInformation("Other Inhouse Investigations is empty, So unable to save.")
            txtInhouseInvest.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtDisConAction.Text) = "" Then
            MsgInformation("Product Disposal / Containment Action is empty, So unable to save.")
            txtDisConAction.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(cboCAPA.Text) = "Yes" Then
            If Trim(txtStartDate.Text) = "" Then
                MsgInformation("Start Date is empty, So unable to save.")
                txtStartDate.Focus()
                FieldsVarification = False
                Exit Function
            End If
            If Trim(txtCloseDate.Text) = "" Then
                MsgInformation("Close Date is empty, So unable to save.")
                txtCloseDate.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If Trim(txtSignDate.Text) = "" Then
            MsgInformation("Signature Date is empty, So unable to save.")
            txtSignDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(TxtPreparedBy.Text) = "" Then
            MsgInformation("Prepared By is empty, So unable to save.")
            TxtPreparedBy.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtApprovedBy.Text) = "" Then
            MsgInformation("Approved By is empty, So unable to save.")
            txtApprovedBy.Focus()
            FieldsVarification = False
            Exit Function
        End If

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        ''Resume
    End Function

    Private Sub frmCustComplaint_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        RsCustComplaint.Close()
        RsCustComplaint = Nothing
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
        txtNumber.Text = SprdView.Text
        txtNumber_Validating(txtNumber, New System.ComponentModel.CancelEventArgs(False))
        Call CmdView_Click(CmdView, New System.EventArgs())
    End Sub
    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Private Sub txtApprovedBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtApprovedBy.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtApprovedBy_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtApprovedBy.DoubleClick
        Call cmdSearchAppBy_Click(cmdSearchAppBy, New System.EventArgs())
    End Sub

    Private Sub txtApprovedBy_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtApprovedBy.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchAppBy_Click(cmdSearchAppBy, New System.EventArgs())
    End Sub

    Private Sub txtApprovedBy_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtApprovedBy.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim SqlStr As String
        If Trim(txtApprovedBy.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND EMP_LEAVE_DATE IS NULL "
        If MainClass.ValidateWithMasterTable(txtApprovedBy.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("EMPLOYEE Does Not Exist In Master.")
            Cancel = True
        Else
            lblApprovedBy.text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtBatchNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBatchNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCloseDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCloseDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCloseDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCloseDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtCloseDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtCloseDate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
        Else
            If Trim(txtStartDate.Text) = "" Or Trim(txtCloseDate.Text) = "" Then GoTo EventExitSub
            If cboCAPA.Text = "Yes" Then
                If CDate(txtCloseDate.Text) < CDate(txtStartDate.Text) Then
                    MsgBox("Close Date should be greater than Start Date")
                    Cancel = True
                End If
            End If
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtCustComplaint_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustComplaint.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCustomer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustomer.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCustomer_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustomer.DoubleClick
        Call CmdSearchCustomer_Click(CmdSearchCustomer, New System.EventArgs())
    End Sub

    Private Sub txtCustomer_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCustomer.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call CmdSearchCustomer_Click(CmdSearchCustomer, New System.EventArgs())
    End Sub

    Private Sub txtCustomer_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustomer.Leave
        If Trim(txtCustomer.Text) = "" Then Exit Sub
        txtProduct.Focus()
    End Sub

    Private Sub txtCustomer_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCustomer.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim SqlStr As String
        Dim mRsTemp As ADODB.Recordset
        If Trim(txtCustomer.Text) = "" Then GoTo EventExitSub

        SqlStr = " SELECT DISTINCT A.SUPP_CUST_NAME,A.SUPP_CUST_CODE " & vbCrLf _
                    & " FROM FIN_SUPP_CUST_MST A, FIN_SUPP_CUST_DET B " & vbCrLf _
                    & " WHERE A.COMPANY_CODE = B.COMPANY_CODE " & vbCrLf _
                    & " AND A.SUPP_CUST_CODE = B.SUPP_CUST_CODE " & vbCrLf _
                    & " AND A.COMPANY_CODE = " & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                    & " AND A.SUPP_CUST_CODE ='" & MainClass.AllowSingleQuote(txtCustomer.Text) & "'  "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With mRsTemp
            If Not .EOF Then
                txtCustomer.Text = IIf(IsDbNull(mRsTemp.Fields("SUPP_CUST_CODE").Value), "", .Fields("SUPP_CUST_CODE").Value)
                lblCustomer.Text = IIf(IsDbNull(mRsTemp.Fields("SUPP_CUST_NAME").Value), "", .Fields("SUPP_CUST_NAME").Value)
            Else
                MsgBox("Not a valid Customer")
                txtCustomer.Text = ""
                lblCustomer.Text = ""
                Cancel = True
            End If
        End With
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtCustomerInvest_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustomerInvest.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtDate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDespatchDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDespatchDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDespatchDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDespatchDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtDespatchDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtDespatchDate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDisConAction_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDisConAction.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtInhouseInvest_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInhouseInvest.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtModeComplaint_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtModeComplaint.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtNumber_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNumber.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart

        If Not RsCustComplaint.EOF Then
            lblMkey.Text = IIf(IsDbNull(RsCustComplaint.Fields("AUTO_KEY_CCOMP").Value), "", RsCustComplaint.Fields("AUTO_KEY_CCOMP").Value)
            txtNumber.Text = IIf(IsDbNull(RsCustComplaint.Fields("AUTO_KEY_CCOMP").Value), "", RsCustComplaint.Fields("AUTO_KEY_CCOMP").Value)
            txtDate.Text = IIf(IsDbNull(RsCustComplaint.Fields("COMPLAINT_DATE").Value), "", RsCustComplaint.Fields("COMPLAINT_DATE").Value)
            txtCustomer.Text = IIf(IsDbNull(RsCustComplaint.Fields("SUPP_CUST_CODE").Value), "", RsCustComplaint.Fields("SUPP_CUST_CODE").Value)
            txtCustomer_Validating(txtCustomer, New System.ComponentModel.CancelEventArgs(False))
            txtModeComplaint.Text = IIf(IsDbNull(RsCustComplaint.Fields("MODE_OF_COMPL").Value), "", RsCustComplaint.Fields("MODE_OF_COMPL").Value)
            txtProduct.Text = IIf(IsDbNull(RsCustComplaint.Fields("PRODUCT_CODE").Value), "", RsCustComplaint.Fields("PRODUCT_CODE").Value)
            txtProduct_Validating(txtProduct, New System.ComponentModel.CancelEventArgs(False))
            txtRefNo.Text = IIf(IsDbNull(RsCustComplaint.Fields("REF_NO").Value), "", RsCustComplaint.Fields("REF_NO").Value)
            lblPartNo.Text = IIf(IsDbNull(RsCustComplaint.Fields("PART_NO").Value), "", RsCustComplaint.Fields("PART_NO").Value)
            txtCustComplaint.Text = IIf(IsDbNull(RsCustComplaint.Fields("CUST_PROB_STATED").Value), "", RsCustComplaint.Fields("CUST_PROB_STATED").Value)
            txtDespatchDate.Text = IIf(IsDbNull(RsCustComplaint.Fields("DESP_DATE").Value), "", RsCustComplaint.Fields("DESP_DATE").Value)
            txtBatchNo.Text = IIf(IsDbNull(RsCustComplaint.Fields("BATCH_NO").Value), "", RsCustComplaint.Fields("BATCH_NO").Value)
            txtReportNo.Text = IIf(IsDbNull(RsCustComplaint.Fields("REPORT_NO").Value), "", RsCustComplaint.Fields("REPORT_NO").Value)
            txtInhouseInvest.Text = IIf(IsDbNull(RsCustComplaint.Fields("OTH_INVEST").Value), "", RsCustComplaint.Fields("OTH_INVEST").Value)
            txtCustomerInvest.Text = IIf(IsDbNull(RsCustComplaint.Fields("INVEST_CUST_END").Value), "", RsCustComplaint.Fields("INVEST_CUST_END").Value)
            txtDisConAction.Text = IIf(IsDbNull(RsCustComplaint.Fields("PRODUCT_ACTION").Value), "", RsCustComplaint.Fields("PRODUCT_ACTION").Value)
            cboCAPA.Text = IIf(IsDbNull(RsCustComplaint.Fields("CAPA_ACTION").Value) Or RsCustComplaint.Fields("CAPA_ACTION").Value = "N", "No", "Yes")
            txtStartDate.Text = IIf(IsDbNull(RsCustComplaint.Fields("START_DATE").Value), "", RsCustComplaint.Fields("START_DATE").Value)
            txtSignDate.Text = IIf(IsDbNull(RsCustComplaint.Fields("SIGN_DATE").Value), "", RsCustComplaint.Fields("SIGN_DATE").Value)
            txtCloseDate.Text = IIf(IsDbNull(RsCustComplaint.Fields("CLOSE_DATE").Value), "", RsCustComplaint.Fields("CLOSE_DATE").Value)
            TxtPreparedBy.Text = IIf(IsDbNull(RsCustComplaint.Fields("PRE_EMP_CODE").Value), "", RsCustComplaint.Fields("PRE_EMP_CODE").Value)
            TxtPreparedBy_Validating(TxtPreparedBy, New System.ComponentModel.CancelEventArgs(False))
            txtApprovedBy.Text = IIf(IsDbNull(RsCustComplaint.Fields("SIGN_EMP_CODE").Value), "", RsCustComplaint.Fields("SIGN_EMP_CODE").Value)
            txtApprovedBy_Validating(txtApprovedBy, New System.ComponentModel.CancelEventArgs(False))
            Call MakeEnableDesableField(False)
        End If
        ADDMode = False
        MODIFYMode = False
        txtNumber.Enabled = True
        cmdSearchNo.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsCustComplaint, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
    Private Sub txtNumber_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNumber.DoubleClick
        Call cmdSearchNo_Click(cmdSearchNo, New System.EventArgs())
    End Sub

    Private Sub txtNumber_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtNumber.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchNo_Click(cmdSearchNo, New System.EventArgs())
    End Sub
    Private Sub txtNumber_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtNumber.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMKey As Double
        Dim mSlipNo As Double
        Dim SqlStr As String


        If Trim(txtNumber.Text) = "" Then GoTo EventExitSub
        mSlipNo = Val(txtNumber.Text)

        If MODIFYMode = True And RsCustComplaint.BOF = False Then xMKey = RsCustComplaint.Fields("AUTO_KEY_CCOMP").Value

        SqlStr = "SELECT * FROM QAL_CUST_COMPLAINT_TRN " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_CCOMP,LENGTH(AUTO_KEY_CCOMP)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_CCOMP=" & mSlipNo & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCustComplaint, ADODB.LockTypeEnum.adLockReadOnly)
        If RsCustComplaint.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Number. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM QAL_CUST_COMPLAINT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_CCOMP,LENGTH(AUTO_KEY_CCOMP)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_CCOMP=" & Val(CStr(xMKey)) & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCustComplaint, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub MakeEnableDesableField(ByRef mMode As Boolean)
        txtDate.Enabled = mMode
        txtCustomer.Enabled = mMode
        cmdSearchCustomer.Enabled = mMode
        txtProduct.Enabled = mMode
        CmdSearchProduct.Enabled = mMode
        cboCAPA.Enabled = mMode
        txtStartDate.Enabled = mMode
        txtCloseDate.Enabled = mMode
        TxtPreparedBy.Enabled = mMode
        CmdSearchPrpBy.Enabled = mMode
        txtApprovedBy.Enabled = mMode
        cmdSearchAppBy.Enabled = mMode
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String)
        On Error GoTo ErrPart
        Dim mAmountInword As String

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\" & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False

        Report1.Action = 1
        Report1.Reset()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub ReportOnCustCompaint(ByRef Mode As Crystal.DestinationConstants)

    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnCustCompaint(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnCustCompaint(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub TxtPreparedBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtPreparedBy.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtPreparedBy_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtPreparedBy.DoubleClick
        Call CmdSearchPrpBy_Click(CmdSearchPrpBy, New System.EventArgs())
    End Sub

    Private Sub TxtPreparedBy_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtPreparedBy.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call CmdSearchPrpBy_Click(CmdSearchPrpBy, New System.EventArgs())
    End Sub

    Private Sub TxtPreparedBy_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtPreparedBy.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim SqlStr As String
        If Trim(TxtPreparedBy.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND EMP_LEAVE_DATE IS NULL "
        If MainClass.ValidateWithMasterTable(TxtPreparedBy.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("EMPLOYEE Does Not Exist In Master.")
            Cancel = True
        Else
            lblPreparedBy.text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtProduct_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProduct.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtProduct_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProduct.DoubleClick
        Call CmdSearchProduct_Click(CmdSearchProduct, New System.EventArgs())
    End Sub

    Private Sub txtProduct_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtProduct.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call CmdSearchProduct_Click(CmdSearchProduct, New System.EventArgs())
    End Sub

    Private Sub txtProduct_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtProduct.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim mRsTemp As ADODB.Recordset
        Dim SqlStr As String
        If Trim(txtProduct.Text) = "" Then GoTo EventExitSub
        SqlStr = " SELECT A.ITEM_CODE,B.ITEM_SHORT_DESC,B.CUSTOMER_PART_NO " & vbCrLf _
                & " FROM FIN_SUPP_CUST_DET A,INV_ITEM_MST B  " & vbCrLf _
                & " WHERE A.COMPANY_CODE = B.COMPANY_CODE " & vbCrLf _
                & " AND A.ITEM_CODE = B.ITEM_CODE " & vbCrLf _
                & " AND A.COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                & " AND A.ITEM_CODE ='" & MainClass.AllowSingleQuote(txtProduct.Text) & "' " & vbCrLf _
                & " AND A.SUPP_CUST_CODE ='" & MainClass.AllowSingleQuote(txtCustomer.Text) & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With mRsTemp
            If Not .EOF Then
                txtProduct.Text = IIf(IsDbNull(mRsTemp.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)
                lblProduct.Text = IIf(IsDbNull(mRsTemp.Fields("Item_Short_Desc").Value), "", .Fields("Item_Short_Desc").Value)
                lblPartNo.Text = IIf(IsDbNull(mRsTemp.Fields("CUSTOMER_PART_NO").Value), "", .Fields("CUSTOMER_PART_NO").Value)
            Else
                MsgBox("Not a valid Customer's Product.")
                txtProduct.Text = ""
                lblProduct.Text = ""
                lblPartNo.Text = ""
                Cancel = True
            End If
        End With
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRefNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRefNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtReportNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtReportNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSignDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSignDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSignDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSignDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtSignDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtSignDate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtStartDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtStartDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtStartDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtStartDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtStartDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtStartDate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
        Else
            If Trim(txtStartDate.Text) = "" Or Trim(txtCloseDate.Text) = "" Then GoTo EventExitSub
            If cboCAPA.Text = "Yes" Then
                If CDate(txtStartDate.Text) > CDate(txtCloseDate.Text) Then
                    MsgBox("Start Date should be less than Close Date")
                    Cancel = True
                End If
            End If
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
