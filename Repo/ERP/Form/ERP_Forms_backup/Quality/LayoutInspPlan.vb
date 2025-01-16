Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmLayoutInspPlan
    Inherits System.Windows.Forms.Form
    Dim RsLayoutInspPlan As ADODB.Recordset
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim PvtDBCn As ADODB.Connection
    Dim FormActive As Boolean
    Dim SqlStr As String
    Private Sub ViewGrid()

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        ADataGrid.Refresh
            FormatSprdView()
            SprdView.Refresh()
            SprdView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            SprdView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsLayoutInspPlan, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub Clear1()

        lblMkey.Text = ""
        txtNumber.Text = ""
        txtYear.Text = RsCompany.Fields("FYEAR").Value
        txtCustomer.Text = ""
        lblCustomer.Text = ""
        txtStage.Text = ""
        txtDeputed.Text = ""
        txtProduct.Text = ""
        lblProduct.Text = ""
        txtJanActual.Text = ""
        txtJanPlan.Text = ""
        txtFebActual.Text = ""
        txtFebPlan.Text = ""
        txtMarActual.Text = ""
        txtMarPlan.Text = ""
        txtAprActual.Text = ""
        txtAprPlan.Text = ""
        txtMayActual.Text = ""
        txtMayPlan.Text = ""
        txtJunPlan.Text = ""
        txtJunActual.Text = ""
        txtJulPlan.Text = ""
        txtJulActual.Text = ""
        txtAugPlan.Text = ""
        txtAugActual.Text = ""
        txtSepPlan.Text = ""
        txtSepActual.Text = ""
        txtOctPlan.Text = ""
        txtOctActual.Text = ""
        txtNovPlan.Text = ""
        txtNovActual.Text = ""
        txtDecPlan.Text = ""
        txtDecActual.Text = ""
        TxtPreparedBy.Text = ""
        lblPreparedBy.Text = ""
        Call MakeEnableDeField(True)
        MainClass.ButtonStatus(Me, XRIGHT, RsLayoutInspPlan, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub MakeEnableDeField(ByRef mMode As Boolean)
        txtYear.Enabled = False
        txtCustomer.Enabled = mMode
        cmdSearchCustomer.Enabled = mMode
        txtProduct.Enabled = mMode
        CmdSearchProduct.Enabled = mMode
        TxtPreparedBy.Enabled = mMode
        cmdSearchPrepBy.Enabled = mMode
        txtJanActual.Enabled = False
        txtFebActual.Enabled = False
        txtMarActual.Enabled = False
        txtAprActual.Enabled = False
        txtMayActual.Enabled = False
        txtJunActual.Enabled = False
        txtJulActual.Enabled = False
        txtAugActual.Enabled = False
        txtSepActual.Enabled = False
        txtOctActual.Enabled = False
        txtNovActual.Enabled = False
        txtDecActual.Enabled = False

    End Sub
    Private Function CheckDate(ByRef pTxtDate As System.Windows.Forms.TextBox) As Boolean
        CheckDate = True
        If Trim(pTxtDate.Text) = "" Then Exit Function
        If Not IsDate(pTxtDate.Text) Then
            MsgBox("Not a Valid Date")
            CheckDate = False
        Else
            Select Case pTxtDate.Name
                Case txtJanPlan.Name
                    If CDate(pTxtDate.Text) < CDate("01/01/" & txtYear.Text) Or CDate(pTxtDate.Text) > CDate("31/01/" & txtYear.Text) Then MsgBox("Date not with in the range") : CheckDate = False
                Case txtFebPlan.Name
                    If CDate(pTxtDate.Text) < CDate("01/02/" & txtYear.Text) Or CDate(pTxtDate.Text) > CDate("28/02/" & txtYear.Text) Then MsgBox("Date not with in the range") : CheckDate = False
                Case txtMarPlan.Name
                    If CDate(pTxtDate.Text) < CDate("01/03/" & txtYear.Text) Or CDate(pTxtDate.Text) > CDate("31/03/" & txtYear.Text) Then MsgBox("Date not with in the range") : CheckDate = False
                Case txtAprPlan.Name
                    If CDate(pTxtDate.Text) < CDate("01/04/" & txtYear.Text) Or CDate(pTxtDate.Text) > CDate("30/04/" & txtYear.Text) Then MsgBox("Date not with in the range") : CheckDate = False
                Case txtMayPlan.Name
                    If CDate(pTxtDate.Text) < CDate("01/05/" & txtYear.Text) Or CDate(pTxtDate.Text) > CDate("31/05/" & txtYear.Text) Then MsgBox("Date not with in the range") : CheckDate = False
                Case txtJunPlan.Name
                    If CDate(pTxtDate.Text) < CDate("01/06/" & txtYear.Text) Or CDate(pTxtDate.Text) > CDate("30/06/" & txtYear.Text) Then MsgBox("Date not with in the range") : CheckDate = False
                Case txtJulPlan.Name
                    If CDate(pTxtDate.Text) < CDate("01/07/" & txtYear.Text) Or CDate(pTxtDate.Text) > CDate("31/07/" & txtYear.Text) Then MsgBox("Date not with in the range") : CheckDate = False
                Case txtAugPlan.Name
                    If CDate(pTxtDate.Text) < CDate("01/08/" & txtYear.Text) Or CDate(pTxtDate.Text) > CDate("30/08/" & txtYear.Text) Then MsgBox("Date not with in the range") : CheckDate = False
                Case txtSepPlan.Name
                    If CDate(pTxtDate.Text) < CDate("01/09/" & txtYear.Text) Or CDate(pTxtDate.Text) > CDate("31/09/" & txtYear.Text) Then MsgBox("Date not with in the range") : CheckDate = False
                Case txtOctPlan.Name
                    If CDate(pTxtDate.Text) < CDate("01/10/" & txtYear.Text) Or CDate(pTxtDate.Text) > CDate("30/10/" & txtYear.Text) Then MsgBox("Date not with in the range") : CheckDate = False
                Case txtNovPlan.Name
                    If CDate(pTxtDate.Text) < CDate("01/11/" & txtYear.Text) Or CDate(pTxtDate.Text) > CDate("31/11/" & txtYear.Text) Then MsgBox("Date not with in the range") : CheckDate = False
                Case txtDecPlan.Name
                    If CDate(pTxtDate.Text) < CDate("01/12/" & txtYear.Text) Or CDate(pTxtDate.Text) > CDate("30/12/" & txtYear.Text) Then MsgBox("Date not with in the range") : CheckDate = False
            End Select
            pTxtDate.Text = VB6.Format(pTxtDate.Text, "DD/MM/YYYY")
        End If
    End Function
    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr
        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            txtNumber.Enabled = False
            cmdSearchNumber.Enabled = False
            MainClass.ButtonStatus(Me, XRIGHT, RsLayoutInspPlan, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPreview.Click
        Call ShowReport(Crystal.DestinationConstants.crptToWindow)
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        ShowReport(Crystal.DestinationConstants.crptToPrinter)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub CmdSearchCustomer_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSearchCustomer.Click
        SqlStr = "SELECT DISTINCT A.SUPP_CUST_CODE, A.SUPP_CUST_NAME " & vbCrLf & " FROM FIN_SUPP_CUST_MST A, FIN_SUPP_CUST_DET B " & vbCrLf & " WHERE A.COMPANY_CODE = B.COMPANY_CODE " & vbCrLf & " AND A.SUPP_CUST_CODE = B.SUPP_CUST_CODE " & vbCrLf & " AND A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " ORDER BY A.SUPP_CUST_CODE "
        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            txtCustomer.Text = AcName
            lblCustomer.text = AcName1
            txtCustomer_Validating(txtCustomer, New System.ComponentModel.CancelEventArgs(False)) ''_Validate(False)
        End If

    End Sub

    Private Sub cmdSearchNumber_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchNumber.Click
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(" & lblAutoKeyName.Text & ",LENGTH(" & lblAutoKeyName.Text & ")-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
        If MainClass.SearchGridMaster("", lblTableName.text, lblAutoKeyName.text, "CAL_YEAR", "SUPP_CUST_CODE", , SqlStr) = True Then
            txtNumber.Text = AcName
            txtNumber_Validating(txtNumber, New System.ComponentModel.CancelEventArgs(False)) ''_Validate(False)
        End If
    End Sub

    Private Sub cmdSearchPrepBy_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchPrepBy.Click
        On Error GoTo SrchERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND EMP_LEAVE_DATE IS NULL "
        If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            txtPreparedBy.Text = AcName1
            lblPreparedBy.text = AcName
        End If
        Exit Sub
SrchERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdSearchProduct_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSearchProduct.Click
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

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        Call ViewGrid()
    End Sub
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtNumber.Enabled = False
            cmdSearchNumber.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            If RsLayoutInspPlan.EOF = False Then RsLayoutInspPlan.MoveFirst()
            Show1()
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
        '    Resume
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart

        If txtNumber.Text = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        If Not RsLayoutInspPlan.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, (lblTableName.Text), (txtNumber.Text), RsLayoutInspPlan) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM " & lblTableName.Text & " WHERE " & lblAutoKeyName.Text & "=" & Val(lblMkey.Text) & "")
                PubDBCn.CommitTrans()
                RsLayoutInspPlan.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        '    PubDBCn.Errors.Clear
        RsLayoutInspPlan.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub frmLayoutInspPlan_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        If lblTableName.Text = "QAL_LAYOUT_PLAN_TRN" Then
            Me.Text = "Layout Inspection Plan"
        ElseIf lblTableName.Text = "QAL_PRODAUDIT_PLAN_TRN" Then
            Me.Text = "Product Audit Inspection Plan"
        End If
        SqlStr = " Select * From " & lblTableName.Text & " Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLayoutInspPlan, ADODB.LockTypeEnum.adLockReadOnly)
        Call SetTextLengths()
        Call AssignGrid(False)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        Clear1()
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Resume
    End Sub
    Private Sub frmLayoutInspPlan_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub frmLayoutInspPlan_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = eventArgs.row
        txtNumber.Text = SprdView.Text
        txtNumber_Validating(txtNumber, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub
    Private Sub frmLayoutInspPlan_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Left = 0
        Me.Top = 0
        'Me.Height = VB6.TwipsToPixelsY(8265)
        'Me.Width = VB6.TwipsToPixelsX(8295)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmLayoutInspPlan_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsLayoutInspPlan.Close()
        RsLayoutInspPlan = Nothing
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart

        If Not RsLayoutInspPlan.EOF Then
            lblMkey.Text = IIf(IsDbNull(RsLayoutInspPlan.Fields(lblAutoKeyName.Text).Value), "", RsLayoutInspPlan.Fields(lblAutoKeyName.Text).Value)
            txtNumber.Text = IIf(IsDbNull(RsLayoutInspPlan.Fields(lblAutoKeyName.Text).Value), "", RsLayoutInspPlan.Fields(lblAutoKeyName.Text).Value)
            txtYear.Text = IIf(IsDbNull(RsLayoutInspPlan.Fields("CAL_YEAR").Value), "", RsLayoutInspPlan.Fields("CAL_YEAR").Value)
            txtCustomer.Text = IIf(IsDbNull(RsLayoutInspPlan.Fields("SUPP_CUST_CODE").Value), "", RsLayoutInspPlan.Fields("SUPP_CUST_CODE").Value)
            txtCustomer_Validating(txtCustomer, New System.ComponentModel.CancelEventArgs(False))
            txtStage.Text = IIf(IsDbNull(RsLayoutInspPlan.Fields("STAGE").Value), "", RsLayoutInspPlan.Fields("STAGE").Value)
            txtDeputed.Text = IIf(IsDbNull(RsLayoutInspPlan.Fields("DEPUTED_PERSONS").Value), "", RsLayoutInspPlan.Fields("DEPUTED_PERSONS").Value)
            txtProduct.Text = IIf(IsDbNull(RsLayoutInspPlan.Fields("PRODUCT_CODE").Value), "", RsLayoutInspPlan.Fields("PRODUCT_CODE").Value)
            txtProduct_Validating(txtProduct, New System.ComponentModel.CancelEventArgs(False))
            txtJanActual.Text = IIf(IsDbNull(RsLayoutInspPlan.Fields("JAN_ACTUAL").Value), "", RsLayoutInspPlan.Fields("JAN_ACTUAL").Value)
            txtJanPlan.Text = IIf(IsDbNull(RsLayoutInspPlan.Fields("JAN_PLAN").Value), "", RsLayoutInspPlan.Fields("JAN_PLAN").Value)
            txtFebActual.Text = IIf(IsDbNull(RsLayoutInspPlan.Fields("FEB_ACTUAL").Value), "", RsLayoutInspPlan.Fields("FEB_ACTUAL").Value)
            txtFebPlan.Text = IIf(IsDbNull(RsLayoutInspPlan.Fields("FEB_PLAN").Value), "", RsLayoutInspPlan.Fields("FEB_PLAN").Value)
            txtMarActual.Text = IIf(IsDbNull(RsLayoutInspPlan.Fields("MAR_ACTUAL").Value), "", RsLayoutInspPlan.Fields("MAR_ACTUAL").Value)
            txtMarPlan.Text = IIf(IsDbNull(RsLayoutInspPlan.Fields("MAR_PLAN").Value), "", RsLayoutInspPlan.Fields("MAR_PLAN").Value)
            txtAprActual.Text = IIf(IsDbNull(RsLayoutInspPlan.Fields("APR_ACTUAL").Value), "", RsLayoutInspPlan.Fields("APR_ACTUAL").Value)
            txtAprPlan.Text = IIf(IsDbNull(RsLayoutInspPlan.Fields("APR_PLAN").Value), "", RsLayoutInspPlan.Fields("APR_PLAN").Value)
            txtMayActual.Text = IIf(IsDbNull(RsLayoutInspPlan.Fields("MAY_ACTUAL").Value), "", RsLayoutInspPlan.Fields("MAY_ACTUAL").Value)
            txtMayPlan.Text = IIf(IsDbNull(RsLayoutInspPlan.Fields("MAY_PLAN").Value), "", RsLayoutInspPlan.Fields("MAY_PLAN").Value)
            txtJunActual.Text = IIf(IsDbNull(RsLayoutInspPlan.Fields("JUN_ACTUAL").Value), "", RsLayoutInspPlan.Fields("JUN_ACTUAL").Value)
            txtJunPlan.Text = IIf(IsDbNull(RsLayoutInspPlan.Fields("JUN_PLAN").Value), "", RsLayoutInspPlan.Fields("JUN_PLAN").Value)
            txtJulActual.Text = IIf(IsDbNull(RsLayoutInspPlan.Fields("JUL_ACTUAL").Value), "", RsLayoutInspPlan.Fields("JUL_ACTUAL").Value)
            txtJulPlan.Text = IIf(IsDbNull(RsLayoutInspPlan.Fields("JUL_PLAN").Value), "", RsLayoutInspPlan.Fields("JUL_PLAN").Value)
            txtAugActual.Text = IIf(IsDbNull(RsLayoutInspPlan.Fields("AUG_ACTUAL").Value), "", RsLayoutInspPlan.Fields("AUG_ACTUAL").Value)
            txtAugPlan.Text = IIf(IsDbNull(RsLayoutInspPlan.Fields("AUG_PLAN").Value), "", RsLayoutInspPlan.Fields("AUG_PLAN").Value)
            txtSepActual.Text = IIf(IsDbNull(RsLayoutInspPlan.Fields("SEP_ACTUAL").Value), "", RsLayoutInspPlan.Fields("SEP_ACTUAL").Value)
            txtSepPlan.Text = IIf(IsDbNull(RsLayoutInspPlan.Fields("SEP_PLAN").Value), "", RsLayoutInspPlan.Fields("SEP_PLAN").Value)
            txtOctActual.Text = IIf(IsDbNull(RsLayoutInspPlan.Fields("OCT_ACTUAL").Value), "", RsLayoutInspPlan.Fields("OCT_ACTUAL").Value)
            txtOctPlan.Text = IIf(IsDbNull(RsLayoutInspPlan.Fields("OCT_PLAN").Value), "", RsLayoutInspPlan.Fields("OCT_PLAN").Value)
            txtNovActual.Text = IIf(IsDbNull(RsLayoutInspPlan.Fields("NOV_ACTUAL").Value), "", RsLayoutInspPlan.Fields("NOV_ACTUAL").Value)
            txtNovPlan.Text = IIf(IsDbNull(RsLayoutInspPlan.Fields("NOV_PLAN").Value), "", RsLayoutInspPlan.Fields("NOV_PLAN").Value)
            txtDecActual.Text = IIf(IsDbNull(RsLayoutInspPlan.Fields("DEC_ACTUAL").Value), "", RsLayoutInspPlan.Fields("DEC_ACTUAL").Value)
            txtDecPlan.Text = IIf(IsDbNull(RsLayoutInspPlan.Fields("DEC_PLAN").Value), "", RsLayoutInspPlan.Fields("DEC_PLAN").Value)
            TxtPreparedBy.Text = IIf(IsDbNull(RsLayoutInspPlan.Fields("PRE_EMP_CODE").Value), "", RsLayoutInspPlan.Fields("PRE_EMP_CODE").Value)
            TxtPreparedBy_Validating(TxtPreparedBy, New System.ComponentModel.CancelEventArgs(False))
            Call MakeEnableDeField(False)
        End If
        ADDMode = False
        MODIFYMode = False
        txtNumber.Enabled = True
        cmdSearchNumber.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsLayoutInspPlan, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        Resume
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If IsRecordExist = True Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default : Exit Sub
        End If
        If Update1 = True Then
            txtNumber_Validating(txtNumber, New System.ComponentModel.CancelEventArgs(False))
            If CmdAdd.Enabled = True Then CmdAdd.Focus()
        Else
            MsgInformation("Record not saved")
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub
    Private Function IsRecordExist() As Boolean

        On Error GoTo IsRecordExistERR
        Dim RsTemp As ADODB.Recordset

        IsRecordExist = False
        If MODIFYMode = True Then Exit Function
        SqlStr = " SELECT " & lblAutoKeyName.text & "  " & vbCrLf _
                & " FROM " & lblTableName.text & " " & vbCrLf _
                & " WHERE COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                & " AND CAL_YEAR =" & Val(txtYear.Text) & " " & vbCrLf _
                & " AND UPPER(LTRIM(RTRIM(STAGE))) ='" & MainClass.AllowSingleQuote(UCase(txtStage.Text)) & "'  " & vbCrLf _
                & " AND UPPER(LTRIM(RTRIM(PRODUCT_CODE))) ='" & MainClass.AllowSingleQuote(UCase(txtProduct.Text)) & "'  " & vbCrLf _
                & " AND UPPER(LTRIM(RTRIM(SUPP_CUST_CODE))) = '" & MainClass.AllowSingleQuote(UCase(txtCustomer.Text)) & "'  "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With RsTemp
            If Not .EOF Then
                MsgBox("This entry already exist in Number : " & .Fields(lblAutoKeyName.Text).Value)
                IsRecordExist = True
            End If
        End With
        Exit Function
IsRecordExistERR:
        IsRecordExist = True
        MsgBox(Err.Description)
    End Function
    Private Function AutoGenKeyNo() As Double

        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset
        Dim mAutoGen As Double
        Dim SqlStr As String
        Dim mMaxValue As String

        mAutoGen = 1
        SqlStr = ""
        SqlStr = "SELECT Max(" & lblAutoKeyName.Text & ")  " & vbCrLf & " FROM " & lblTableName.Text & " " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(" & lblAutoKeyName.Text & ",LENGTH(" & lblAutoKeyName.Text & ")-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

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
    Private Function Update1() As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mSlipNo As Double

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()


        SqlStr = ""
        mSlipNo = Val(txtNumber.Text)
        If Val(txtNumber.Text) = 0 Then
            mSlipNo = AutoGenKeyNo()
        End If
        txtNumber.Text = CStr(mSlipNo)
        If ADDMode = True Then
            lblMkey.Text = CStr(mSlipNo)
            SqlStr = " INSERT INTO " & lblTableName.text & " " & vbCrLf _
                            & " (COMPANY_CODE," & lblAutoKeyName.text & ",CAL_YEAR,SUPP_CUST_CODE, " & vbCrLf _
                            & " STAGE,DEPUTED_PERSONS,PRODUCT_CODE,PRE_EMP_CODE, " & vbCrLf _
                            & " JAN_ACTUAL,JAN_PLAN,FEB_ACTUAL,FEB_PLAN,MAR_ACTUAL,MAR_PLAN," & vbCrLf _
                            & " APR_ACTUAL,APR_PLAN,MAY_ACTUAL,MAY_PLAN,JUN_ACTUAL,JUN_PLAN, " & vbCrLf _
                            & " JUL_ACTUAL,JUL_PLAN,AUG_ACTUAL,AUG_PLAN,SEP_ACTUAL,SEP_PLAN, " & vbCrLf _
                            & " OCT_ACTUAL,OCT_PLAN,NOV_ACTUAL,NOV_PLAN,DEC_ACTUAL,DEC_PLAN, " & vbCrLf _
                            & " ADDUSER, ADDDATE, MODUSER, MODDATE) " & vbCrLf _
                            & " VALUES ( " & vbCrLf _
                            & " " & RsCompany.fields("COMPANY_CODE").value & "," & mSlipNo & "," & Val(txtYear.Text) & ", " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtCustomer.Text) & "','" & MainClass.AllowSingleQuote(txtStage.Text) & "'," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtDeputed.Text) & "','" & MainClass.AllowSingleQuote(txtProduct.Text) & "'," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtPreparedBy.Text) & "',"
            SqlStr = SqlStr & vbCrLf _
                    & " TO_DATE('" & vb6.Format(txtJanActual.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & vb6.Format(txtJanPlan.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " TO_DATE('" & vb6.Format(txtFebActual.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & vb6.Format(txtFebPlan.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " TO_DATE('" & vb6.Format(txtMarActual.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & vb6.Format(txtMarPlan.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " TO_DATE('" & vb6.Format(txtAprActual.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & vb6.Format(txtAprPlan.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " TO_DATE('" & vb6.Format(txtMayActual.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & vb6.Format(txtMayPlan.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " TO_DATE('" & vb6.Format(txtJunActual.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & vb6.Format(txtJunPlan.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " TO_DATE('" & vb6.Format(txtJulActual.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & vb6.Format(txtJulPlan.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " TO_DATE('" & vb6.Format(txtAugActual.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & vb6.Format(txtAugPlan.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " TO_DATE('" & vb6.Format(txtSepActual.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & vb6.Format(txtSepPlan.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " TO_DATE('" & vb6.Format(txtOctActual.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & vb6.Format(txtOctPlan.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " TO_DATE('" & vb6.Format(txtNovActual.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & vb6.Format(txtNovPlan.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " TO_DATE('" & vb6.Format(txtDecActual.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & vb6.Format(txtDecPlan.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"
        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE " & lblTableName.Text & " SET " & vbCrLf _
                    & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "," & lblAutoKeyName.Text & "=" & mSlipNo & ", " & vbCrLf _
                    & " CAL_YEAR=" & Val(txtYear.Text) & ",SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCustomer.Text) & "', " & vbCrLf _
                    & " STAGE='" & MainClass.AllowSingleQuote(txtStage.Text) & "', " & vbCrLf _
                    & " DEPUTED_PERSONS='" & MainClass.AllowSingleQuote(txtDeputed.Text) & "', " & vbCrLf _
                    & " PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProduct.Text) & "', " & vbCrLf _
                    & " PRE_EMP_CODE='" & MainClass.AllowSingleQuote(txtPreparedBy.Text) & "', " & vbCrLf _
                    & " JAN_ACTUAL=TO_DATE('" & vb6.Format(txtJanActual.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " JAN_PLAN=TO_DATE('" & vb6.Format(txtJanPlan.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " FEB_ACTUAL=TO_DATE('" & vb6.Format(txtFebActual.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " FEB_PLAN=TO_DATE('" & vb6.Format(txtFebPlan.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " MAR_ACTUAL=TO_DATE('" & vb6.Format(txtMarActual.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " MAR_PLAN=TO_DATE('" & vb6.Format(txtMarPlan.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " APR_ACTUAL=TO_DATE('" & vb6.Format(txtAprActual.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " APR_PLAN=TO_DATE('" & vb6.Format(txtAprPlan.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " MAY_ACTUAL=TO_DATE('" & vb6.Format(txtMayActual.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " MAY_PLAN=TO_DATE('" & vb6.Format(txtMayPlan.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " JUN_ACTUAL=TO_DATE('" & vb6.Format(txtJunActual.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " JUN_PLAN=TO_DATE('" & vb6.Format(txtJunPlan.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), "
            SqlStr = SqlStr & vbCrLf _
                    & " JUL_ACTUAL=TO_DATE('" & vb6.Format(txtJulActual.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " JUL_PLAN=TO_DATE('" & vb6.Format(txtJulPlan.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " AUG_ACTUAL=TO_DATE('" & vb6.Format(txtAugActual.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " AUG_PLAN=TO_DATE('" & vb6.Format(txtAugPlan.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " SEP_ACTUAL=TO_DATE('" & vb6.Format(txtSepActual.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " SEP_PLAN=TO_DATE('" & vb6.Format(txtSepPlan.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " OCT_ACTUAL=TO_DATE('" & vb6.Format(txtOctActual.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " OCT_PLAN=TO_DATE('" & vb6.Format(txtOctPlan.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " NOV_ACTUAL=TO_DATE('" & vb6.Format(txtNovActual.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " NOV_PLAN=TO_DATE('" & vb6.Format(txtNovPlan.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " DEC_ACTUAL=TO_DATE('" & vb6.Format(txtDecActual.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " DEC_PLAN=TO_DATE('" & vb6.Format(txtDecPlan.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                    & " MODDATE=TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND " & lblAutoKeyName.Text & " =" & Val(lblMkey.Text) & ""
        End If

        PubDBCn.Execute(SqlStr)
        Update1 = True
        PubDBCn.CommitTrans()
        txtNumber.Text = CStr(mSlipNo)
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsLayoutInspPlan.Requery()
        MsgBox(Err.Description)
        '    Resume
    End Function
    Private Function FieldsVarification() As Boolean
        On Error GoTo VarificationErr
        FieldsVarification = True

        If Trim(txtYear.Text) = "" Then
            MsgInformation("Cal Year is empty, So unable to Save")
            txtYear.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtCustomer.Text) = "" Then
            MsgInformation("Customer is empty, So unable to Save")
            txtCustomer.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtProduct.Text) = "" Then
            MsgInformation("Product is empty, So unable to Save")
            txtProduct.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(TxtPreparedBy.Text) = "" Then
            MsgInformation("Prepared By is empty, So unable to Save")
            TxtPreparedBy.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Machine Details or modify an existing item")
            FieldsVarification = False
        End If
        If MODIFYMode = True And RsLayoutInspPlan.EOF = True Then Exit Function
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Exit Function
VarificationErr:
        FieldsVarification = False
        MsgInformation(Err.Description)
    End Function
    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        SqlStr = " SELECT " & lblAutoKeyName.Text & ",CAL_YEAR,SUPP_CUST_CODE, " & vbCrLf & " STAGE,DEPUTED_PERSONS,PRODUCT_CODE,PRE_EMP_CODE " & vbCrLf & " FROM " & lblTableName.Text & " " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(" & lblAutoKeyName.Text & ",LENGTH(" & lblAutoKeyName.Text & ")-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " ORDER BY " & lblAutoKeyName.Text & ""
        Call MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        Call FormatSprdView()

    End Sub

    Private Sub ShowReport(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim mTitle As String
        mTitle = ""
        Report1.Reset()

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\LayoutInspPlan.rpt"
        If lblTableName.Text = "QAL_LAYOUT_PLAN_TRN" Then
            mTitle = "Layout Inspection Plan"
        ElseIf lblTableName.Text = "QAL_PRODAUDIT_PLAN_TRN" Then
            mTitle = "Product Audit Inspection Plan"
        End If

        SetCrpt(Report1, Mode, 1, mTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub txtAprActual_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAprActual.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAprActual_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAprActual.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtAprActual) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtAprPlan_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAprPlan.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAprPlan_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAprPlan.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtAprPlan) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtAugActual_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAugActual.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAugActual_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAugActual.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtAugActual) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtAugPlan_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAugPlan.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAugPlan_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAugPlan.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtAugPlan) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtCustomer_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustomer.Leave
        If Trim(txtCustomer.Text) = "" Then Exit Sub
        txtProduct.Focus()
    End Sub

    Private Sub txtDecActual_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDecActual.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDecActual_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDecActual.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtDecActual) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDecPlan_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDecPlan.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDecPlan_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDecPlan.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtDecPlan) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDeputed_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDeputed.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtFebActual_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFebActual.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtFebActual_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtFebActual.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtFebActual) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtFebPlan_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFebPlan.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtFebPlan_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtFebPlan.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtFebPlan) = False Then Cancel = True
        eventArgs.Cancel = Cancel
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

    Private Sub txtCustomer_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCustomer.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

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

    Private Sub txtJanActual_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtJanActual.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtJanActual_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtJanActual.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtJanActual) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtJanPlan_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtJanPlan.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtJanPlan_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtJanPlan.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtJanPlan) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtJulActual_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtJulActual.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtJulActual_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtJulActual.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtJulActual) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtJulPlan_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtJulPlan.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtJulPlan_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtJulPlan.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtJulPlan) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtJunActual_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtJunActual.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtJunActual_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtJunActual.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtJunActual) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtJunPlan_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtJunPlan.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtJunPlan_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtJunPlan.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtJunPlan) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtMarActual_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMarActual.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMarActual_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMarActual.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtMarActual) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtMarPlan_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMarPlan.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMarPlan_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMarPlan.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtMarPlan) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtMayActual_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMayActual.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMayActual_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMayActual.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtMayActual) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtMayPlan_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMayPlan.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMayPlan_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMayPlan.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtMayPlan) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtNovActual_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNovActual.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtNovActual_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtNovActual.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtNovActual) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtNovPlan_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNovPlan.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtNovPlan_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtNovPlan.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtNovPlan) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtNumber_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNumber.DoubleClick
        Call cmdSearchNumber_Click(cmdSearchNumber, New System.EventArgs())
    End Sub

    Private Sub txtNumber_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtNumber.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchNumber_Click(cmdSearchNumber, New System.EventArgs())
    End Sub

    Private Sub txtNumber_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtNumber.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMKey As Double
        If Trim(txtNumber.Text) = "" Then GoTo EventExitSub
        If MODIFYMode = True And RsLayoutInspPlan.EOF = False Then xMKey = RsLayoutInspPlan.Fields(lblAutoKeyName.Text).Value
        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM " & lblTableName.Text & " " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(" & lblAutoKeyName.Text & ",LENGTH(" & lblAutoKeyName.Text & ")-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND " & lblAutoKeyName.Text & "=" & Val(txtNumber.Text) & " "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLayoutInspPlan, ADODB.LockTypeEnum.adLockReadOnly)
        If RsLayoutInspPlan.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            xMKey = RsLayoutInspPlan.Fields(lblAutoKeyName.Text).Value
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Such Number Does Not Exist" & vbCrLf & "Click Add To Add for New.", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = " SELECT * " & vbCrLf & " FROM " & lblTableName.Text & "" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(" & lblAutoKeyName.Text & ",LENGTH(" & lblAutoKeyName.Text & ")-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND " & lblAutoKeyName.Text & "=" & xMKey & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLayoutInspPlan, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If

        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub SetTextLengths()
        On Error GoTo ERR1
        txtCustomer.Maxlength = RsLayoutInspPlan.Fields("SUPP_CUST_CODE").DefinedSize
        txtStage.Maxlength = RsLayoutInspPlan.Fields("STAGE").DefinedSize
        txtDeputed.Maxlength = RsLayoutInspPlan.Fields("DEPUTED_PERSONS").DefinedSize
        txtProduct.Maxlength = RsLayoutInspPlan.Fields("PRODUCT_CODE").DefinedSize
        txtProduct.Maxlength = RsLayoutInspPlan.Fields("PRODUCT_CODE").DefinedSize

        txtJanActual.Maxlength = RsLayoutInspPlan.Fields("JAN_ACTUAL").DefinedSize - 6
        txtJanPlan.Maxlength = RsLayoutInspPlan.Fields("JAN_PLAN").DefinedSize - 6
        txtFebActual.Maxlength = RsLayoutInspPlan.Fields("FEB_ACTUAL").DefinedSize - 6
        txtFebPlan.Maxlength = RsLayoutInspPlan.Fields("FEB_PLAN").DefinedSize - 6
        txtMarActual.Maxlength = RsLayoutInspPlan.Fields("MAR_ACTUAL").DefinedSize - 6
        txtMarPlan.Maxlength = RsLayoutInspPlan.Fields("MAR_PLAN").DefinedSize - 6
        txtAprActual.Maxlength = RsLayoutInspPlan.Fields("APR_ACTUAL").DefinedSize - 6
        txtAprPlan.Maxlength = RsLayoutInspPlan.Fields("APR_PLAN").DefinedSize - 6
        txtMayActual.Maxlength = RsLayoutInspPlan.Fields("MAY_ACTUAL").DefinedSize - 6
        txtMayPlan.Maxlength = RsLayoutInspPlan.Fields("MAY_PLAN").DefinedSize - 6
        txtJunPlan.Maxlength = RsLayoutInspPlan.Fields("JUN_PLAN").DefinedSize - 6
        txtJunActual.Maxlength = RsLayoutInspPlan.Fields("JUN_ACTUAL").DefinedSize - 6
        txtJulPlan.Maxlength = RsLayoutInspPlan.Fields("JUL_PLAN").DefinedSize - 6
        txtJulActual.Maxlength = RsLayoutInspPlan.Fields("JUL_ACTUAL").DefinedSize - 6
        txtAugPlan.Maxlength = RsLayoutInspPlan.Fields("AUG_PLAN").DefinedSize - 6
        txtAugActual.Maxlength = RsLayoutInspPlan.Fields("AUG_ACTUAL").DefinedSize - 6
        txtSepPlan.Maxlength = RsLayoutInspPlan.Fields("SEP_PLAN").DefinedSize - 6
        txtSepActual.Maxlength = RsLayoutInspPlan.Fields("SEP_ACTUAL").DefinedSize - 6
        txtOctPlan.Maxlength = RsLayoutInspPlan.Fields("OCT_PLAN").DefinedSize - 6
        txtOctActual.Maxlength = RsLayoutInspPlan.Fields("OCT_ACTUAL").DefinedSize - 6
        txtNovPlan.Maxlength = RsLayoutInspPlan.Fields("NOV_PLAN").DefinedSize - 6
        txtNovActual.Maxlength = RsLayoutInspPlan.Fields("NOV_ACTUAL").DefinedSize - 6
        txtDecPlan.Maxlength = RsLayoutInspPlan.Fields("DEC_PLAN").DefinedSize - 6
        txtDecActual.Maxlength = RsLayoutInspPlan.Fields("DEC_ACTUAL").DefinedSize - 6
        TxtPreparedBy.Maxlength = RsLayoutInspPlan.Fields("PRE_EMP_CODE").DefinedSize
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        Resume
    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_ColWidth(0, 0)
            .set_ColWidth(1, 500 * 3)
            .set_ColWidth(2, 500 * 2)
            .set_ColWidth(3, 500 * 3)
            .set_ColWidth(4, 500 * 5)
            .set_ColWidth(5, 500 * 5)
            .set_ColWidth(6, 500 * 3)
            .set_ColWidth(7, 500 * 3)
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub txtNumber_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNumber.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtNumber_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNumber.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtNumber.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtOctActual_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOctActual.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)

    End Sub

    Private Sub txtOctActual_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtOctActual.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtOctActual) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtOctPlan_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOctPlan.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOctPlan_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtOctPlan.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtOctPlan) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub TxtPreparedBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPreparedBy.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtPreparedBy_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPreparedBy.DoubleClick
        Call cmdSearchPrepBy_Click(cmdSearchPrepBy, New System.EventArgs())
    End Sub

    Private Sub TxtPreparedBy_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPreparedBy.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchPrepBy_Click(cmdSearchPrepBy, New System.EventArgs())
    End Sub

    Private Sub TxtPreparedBy_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPreparedBy.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValEMP
        Dim SqlStr As String
        If Trim(TxtPreparedBy.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND EMP_LEAVE_DATE IS NULL "
        If MainClass.ValidateWithMasterTable(txtPreparedBy, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Employee Does Not Exist In Master.")
            Cancel = True
        Else
            lblPreparedBy.text = MasterNo
        End If
        GoTo EventExitSub
ValEMP:
        MsgBox(Err.Description)
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
        If Trim(txtProduct.Text) = "" Then GoTo EventExitSub
        SqlStr = " SELECT A.ITEM_CODE,B.ITEM_SHORT_DESC " & vbCrLf _
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
            Else
                MsgBox("Not a valid Customer's Product.")
                txtProduct.Text = ""
                lblProduct.Text = ""
                Cancel = True
            End If
        End With
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSepActual_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSepActual.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSepActual_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSepActual.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtSepActual) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSepPlan_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSepPlan.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSepPlan_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSepPlan.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtSepPlan) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtStage_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtStage.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtYear_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtYear.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
End Class
