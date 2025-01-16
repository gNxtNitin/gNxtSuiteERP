Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmRejProdAnalysis
    Inherits System.Windows.Forms.Form
    Dim RsRejProdAnalysis As ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean


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
            If RsRejProdAnalysis.EOF = False Then RsRejProdAnalysis.MoveFirst()
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
        If Not RsRejProdAnalysis.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "QAL_REJPRODANALY_TRN", (txtNumber.Text), RsRejProdAnalysis) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM QAL_REJPRODANALY_TRN WHERE AUTO_KEY_REJPROD=" & Val(lblMkey.Text) & "")
                PubDBCn.CommitTrans()
                RsRejProdAnalysis.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Errors.Clear()
        RsRejProdAnalysis.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr


        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsRejProdAnalysis, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
        Dim mProdIs As String
        Dim mCAPANeed As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mProdIs = IIf(chkProdIs.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mCAPANeed = IIf(chkCAPANeed.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        SqlStr = ""
        mSlipNo = Val(txtNumber.Text)
        If Val(txtNumber.Text) = 0 Then
            mSlipNo = AutoGenKeyNo()
        End If
        txtNumber.Text = CStr(mSlipNo)
        If ADDMode = True Then
            lblMkey.Text = CStr(mSlipNo)
            SqlStr = " INSERT INTO QAL_REJPRODANALY_TRN " & vbCrLf _
                            & " (AUTO_KEY_REJPROD,COMPANY_CODE," & vbCrLf _
                            & " REC_DATE,SUPP_CUST_CODE,ITEM_CODE,CMP_INV_NO,CMP_INV_DATE,REF_NO,REF_DATE," & vbCrLf _
                            & " DEFECT_REPT_CUST,PROD_ANALY_REPT,PRODUCT_STATUS,ITEM_QTY,PROD_DESPOSITION,TARGET_DATE, " & vbCrLf _
                            & " ACTION_DATE,RESP_DESPOSITION,ACTION_COMPL_DATE,NEED_FOR_CP_ACTION, " & vbCrLf _
                            & " INVEST_DATE,INVEST_EMP_CODE,ADDUSER, ADDDATE, MODUSER, MODDATE) " & vbCrLf _
                            & " VALUES ( " & vbCrLf _
                            & " " & mSlipNo & "," & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(txtCustomer.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtProduct.Text) & "'," & Val(TxtInvoiceNo.Text) & " ,TO_DATE('" & vb6.Format(lblInvoiceDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtRefNo.Text) & "',TO_DATE('" & vb6.Format(txtRefDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtCustDefReported.Text) & "','" & MainClass.AllowSingleQuote(txtProdAnalysisRep.Text) & "'," & vbCrLf _
                            & " '" & mProdIs & "'," & Val(txtQuantity.Text) & ",'" & MainClass.AllowSingleQuote(txtProdDespos.Text) & "', " & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtTargetDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & vb6.Format(txtActionDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtResponsibility.Text) & "',TO_DATE('" & vb6.Format(txtCompletingDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " '" & mCAPANeed & "',TO_DATE('" & vb6.Format(txtInvestDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtInvestBy.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"
        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE QAL_REJPRODANALY_TRN SET " & vbCrLf _
                    & " AUTO_KEY_REJPROD=" & mSlipNo & ",COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                    & " REC_DATE=TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCustomer.Text) & "', " & vbCrLf _
                    & " ITEM_CODE='" & MainClass.AllowSingleQuote(txtProduct.Text) & "',CMP_INV_NO=" & Val(TxtInvoiceNo.Text) & ", " & vbCrLf _
                    & " CMP_INV_DATE=TO_DATE('" & vb6.Format(lblInvoiceDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),REF_NO='" & MainClass.AllowSingleQuote(txtRefNo.Text) & "', " & vbCrLf _
                    & " REF_DATE=TO_DATE('" & vb6.Format(txtRefDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " DEFECT_REPT_CUST='" & MainClass.AllowSingleQuote(txtCustDefReported.Text) & "',PROD_ANALY_REPT='" & MainClass.AllowSingleQuote(txtProdAnalysisRep.Text) & "', " & vbCrLf _
                    & " PRODUCT_STATUS='" & mProdIs & "',ITEM_QTY=" & Val(txtQuantity.Text) & ", " & vbCrLf _
                    & " PROD_DESPOSITION='" & MainClass.AllowSingleQuote(txtProdDespos.Text) & "', " & vbCrLf _
                    & " TARGET_DATE=TO_DATE('" & vb6.Format(txtTargetDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " ACTION_DATE=TO_DATE('" & vb6.Format(txtActionDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " RESP_DESPOSITION='" & MainClass.AllowSingleQuote(txtResponsibility.Text) & "', " & vbCrLf _
                    & " ACTION_COMPL_DATE=TO_DATE('" & vb6.Format(txtCompletingDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " NEED_FOR_CP_ACTION='" & mCAPANeed & "', " & vbCrLf _
                    & " INVEST_DATE=TO_DATE('" & vb6.Format(txtInvestDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " INVEST_EMP_CODE='" & MainClass.AllowSingleQuote(txtInvestBy.Text) & "', " & vbCrLf _
                    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                    & " MODDATE=TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND AUTO_KEY_REJPROD =" & Val(lblMkey.Text) & ""
        End If

        PubDBCn.Execute(SqlStr)
        Update1 = True
        PubDBCn.CommitTrans()
        txtNumber.Text = CStr(mSlipNo)
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsRejProdAnalysis.Requery()
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
        SqlStr = "SELECT Max(AUTO_KEY_REJPROD)  " & vbCrLf & " FROM QAL_REJPRODANALY_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_REJPROD,LENGTH(AUTO_KEY_REJPROD)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

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

    Private Sub cmdSearchInvestBy_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchInvestBy.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND EMP_LEAVE_DATE IS NULL "
        If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            txtInvestBy.Text = AcName1
            lblInvestBy.text = AcName
        End If
    End Sub

    Private Sub CmdSearchCustomer_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSearchCustomer.Click
        Dim SqlStr As String

        SqlStr = "SELECT DISTINCT A.SUPP_CUST_CODE, A.SUPP_CUST_NAME " & vbCrLf & " FROM FIN_SUPP_CUST_MST A, FIN_SUPP_CUST_DET B " & vbCrLf & " WHERE A.COMPANY_CODE = B.COMPANY_CODE " & vbCrLf & " AND A.SUPP_CUST_CODE = B.SUPP_CUST_CODE " & vbCrLf & " AND A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " ORDER BY A.SUPP_CUST_CODE "
        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            txtCustomer.Text = AcName
            lblCustomer.text = AcName1
            txtCustomer_Validating(txtCustomer, New System.ComponentModel.CancelEventArgs(False)) ''_Validate(False)
        End If

    End Sub

    Private Sub CmdSearchInvoice_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSearchInvoice.Click
        Dim SqlStr As String
        SqlStr = "SELECT DISTINCT FIN_INVOICE_HDR.AUTO_KEY_INVOICE,FIN_INVOICE_HDR.INVOICE_DATE, " & vbCrLf _
                & " FIN_INVOICE_DET.ITEM_CODE,FIN_INVOICE_DET.ITEM_QTY " & vbCrLf _
                & " FROM FIN_INVOICE_HDR,FIN_INVOICE_DET  " & vbCrLf _
                & " WHERE FIN_INVOICE_HDR.AUTO_KEY_INVOICE=FIN_INVOICE_DET.AUTO_KEY_INVOICE " & vbCrLf _
                & " AND FIN_INVOICE_HDR.COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                & " AND FIN_INVOICE_DET.ITEM_CODE ='" & MainClass.AllowSingleQuote(txtProduct.Text) & "' " & vbCrLf _
                & " AND FIN_INVOICE_HDR.SUPP_CUST_CODE ='" & MainClass.AllowSingleQuote(txtCustomer.Text) & "' " & vbCrLf _
                & " ORDER BY FIN_INVOICE_HDR.AUTO_KEY_INVOICE, FIN_INVOICE_HDR.INVOICE_DATE   "
        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            TxtInvoiceNo.Text = AcName
            lblInvoiceDate.text = AcName1
            txtInvoiceNo_Validating(TxtInvoiceNo, New System.ComponentModel.CancelEventArgs(False)) ''_Validate(False)
        End If

    End Sub

    Private Sub cmdSearchNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchNo.Click
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_REJPROD,LENGTH(AUTO_KEY_REJPROD)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
        If MainClass.SearchGridMaster(txtNumber.Text, "QAL_REJPRODANALY_TRN", "AUTO_KEY_REJPROD", "REC_DATE", "SUPP_CUST_CODE", "ITEM_CODE", SqlStr) = True Then
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
        MainClass.ButtonStatus(Me, XRIGHT, RsRejProdAnalysis, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmRejProdAnalysis_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Rejected Product Analysis"

        SqlStr = "Select * From QAL_REJPRODANALY_TRN WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRejProdAnalysis, ADODB.LockTypeEnum.adLockReadOnly)


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

        SqlStr = " SELECT " & vbCrLf & " AUTO_KEY_REJPROD AS SLIP_NUMBER,TO_CHAR(REC_DATE,'DD/MM/YYYY') AS REC_DATE, " & vbCrLf & " SUPP_CUST_CODE,ITEM_CODE,CMP_INV_NO,CMP_INV_DATE,REF_NO,REF_DATE,  " & vbCrLf & " DEFECT_REPT_CUST,PROD_ANALY_REPT,PRODUCT_STATUS,ITEM_QTY,PROD_DESPOSITION, " & vbCrLf & " TARGET_DATE,ACTION_DATE,RESP_DESPOSITION,ACTION_COMPL_DATE,NEED_FOR_CP_ACTION, " & vbCrLf & " INVEST_DATE,INVEST_EMP_CODE " & vbCrLf & " FROM QAL_REJPRODANALY_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_REJPROD,LENGTH(AUTO_KEY_REJPROD)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " ORDER BY AUTO_KEY_REJPROD"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmRejProdAnalysis_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmRejProdAnalysis_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Left = 0
        Me.Top = 0
        Me.Height = VB6.TwipsToPixelsY(6330)
        Me.Width = VB6.TwipsToPixelsX(9285)
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
        txtProduct.Text = ""
        lblProduct.Text = ""
        txtInvoiceNo.Text = ""
        lblInvoiceDate.Text = ""
        txtRefNo.Text = ""
        txtRefDate.Text = ""
        txtCustDefReported.Text = ""
        txtProdAnalysisRep.Text = ""
        chkProdIs.CheckState = System.Windows.Forms.CheckState.Unchecked
        txtQuantity.Text = ""
        txtProdDespos.Text = ""
        txtTargetDate.Text = ""
        txtActionDate.Text = ""
        txtResponsibility.Text = ""
        txtCompletingDate.Text = ""
        chkCAPANeed.CheckState = System.Windows.Forms.CheckState.Unchecked
        txtInvestBy.Text = ""
        lblInvestBy.Text = ""
        Call MakeEnableDesableField(True)
        MainClass.ButtonStatus(Me, XRIGHT, RsRejProdAnalysis, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
            .set_ColWidth(1, 500 * 4)
            .set_ColWidth(2, 500 * 3)
            .set_ColWidth(3, 500 * 3)
            .set_ColWidth(4, 500 * 3)
            .set_ColWidth(5, 500 * 3)
            .set_ColWidth(6, 500 * 3)
            .set_ColWidth(7, 500 * 3)
            .set_ColWidth(8, 500 * 3)
            .set_ColWidth(9, 500 * 5)
            .set_ColWidth(10, 500 * 5)
            .set_ColWidth(11, 500 * 4)
            .set_ColWidth(12, 500 * 3)
            .set_ColWidth(13, 500 * 5)
            .set_ColWidth(14, 500 * 3)
            .set_ColWidth(15, 500 * 3)
            .set_ColWidth(16, 500 * 5)
            .set_ColWidth(17, 500 * 3)
            .set_ColWidth(18, 500 * 4)
            .set_ColWidth(19, 500 * 3)
            .set_ColWidth(20, 500 * 3)
            .ColsFrozen = 2
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1

        txtNumber.Maxlength = RsRejProdAnalysis.Fields("AUTO_KEY_REJPROD").Precision
        txtDate.Maxlength = RsRejProdAnalysis.Fields("REC_DATE").DefinedSize - 6
        txtCustomer.Maxlength = RsRejProdAnalysis.Fields("SUPP_CUST_CODE").DefinedSize
        txtProduct.Maxlength = RsRejProdAnalysis.Fields("ITEM_CODE").DefinedSize
        txtInvoiceNo.Maxlength = RsRejProdAnalysis.Fields("CMP_INV_NO").DefinedSize
        txtInvestDate.Maxlength = RsRejProdAnalysis.Fields("CMP_INV_DATE").DefinedSize - 6
        txtRefNo.Maxlength = RsRejProdAnalysis.Fields("REF_NO").DefinedSize
        txtRefDate.Maxlength = RsRejProdAnalysis.Fields("REF_DATE").DefinedSize - 6
        txtCustDefReported.Maxlength = RsRejProdAnalysis.Fields("DEFECT_REPT_CUST").DefinedSize
        txtProdAnalysisRep.Maxlength = RsRejProdAnalysis.Fields("PROD_ANALY_REPT").DefinedSize
        txtQuantity.Maxlength = RsRejProdAnalysis.Fields("ITEM_QTY").Precision
        txtProdDespos.Maxlength = RsRejProdAnalysis.Fields("PROD_DESPOSITION").DefinedSize
        txtTargetDate.Maxlength = RsRejProdAnalysis.Fields("TARGET_DATE").DefinedSize - 6
        txtActionDate.Maxlength = RsRejProdAnalysis.Fields("ACTION_DATE").DefinedSize - 6
        txtResponsibility.Maxlength = RsRejProdAnalysis.Fields("RESP_DESPOSITION").DefinedSize
        txtCompletingDate.Maxlength = RsRejProdAnalysis.Fields("ACTION_COMPL_DATE").DefinedSize - 6
        txtInvestDate.Maxlength = RsRejProdAnalysis.Fields("INVEST_DATE").DefinedSize - 6
        txtInvestBy.Maxlength = RsRejProdAnalysis.Fields("INVEST_EMP_CODE").DefinedSize
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
        If MODIFYMode = True And RsRejProdAnalysis.EOF = True Then Exit Function

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
        If Trim(txtProduct.Text) = "" Then
            MsgInformation("Product Code is empty, So unable to save.")
            txtProduct.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtInvoiceNo.Text) = "" Then
            MsgInformation("Invoice No. is empty, So unable to save.")
            txtInvoiceNo.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtRefNo.Text) = "" Then
            MsgInformation("Ref No. is empty, So unable to save.")
            txtRefNo.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtRefDate.Text) = "" Then
            MsgInformation("Ref Date is empty, So unable to save.")
            txtRefDate.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtActionDate.Text) = "" Then
            MsgInformation("Action Date is empty, So unable to save.")
            txtActionDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtCompletingDate.Text) = "" Then
            MsgInformation("Completing Date is empty, So unable to save.")
            txtCompletingDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtInvestBy.Text) = "" Then
            MsgInformation("Investigation By is empty, So unable to save.")
            txtInvestBy.Focus()
            FieldsVarification = False
            Exit Function
        End If

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        ''Resume
    End Function

    Private Sub frmRejProdAnalysis_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        RsRejProdAnalysis.Close()
        RsRejProdAnalysis = Nothing
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

    Private Sub txtCustDefReported_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustDefReported.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtInvestBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInvestBy.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtInvestBy_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInvestBy.DoubleClick
        Call cmdSearchInvestBy_Click(cmdSearchInvestBy, New System.EventArgs())
    End Sub

    Private Sub txtInvestBy_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtInvestBy.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchInvestBy_Click(cmdSearchInvestBy, New System.EventArgs())
    End Sub

    Private Sub txtInvestBy_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtInvestBy.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim SqlStr As String
        If Trim(txtInvestBy.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND EMP_LEAVE_DATE IS NULL "
        If MainClass.ValidateWithMasterTable(txtInvestBy.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("EMPLOYEE Does Not Exist In Master.")
            Cancel = True
        Else
            lblInvestBy.text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtInvestDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInvestDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtInvoiceNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtInvoiceNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtInvoiceNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtInvoiceNo.DoubleClick
        Call CmdSearchInvoice_Click(CmdSearchInvoice, New System.EventArgs())
    End Sub

    Private Sub txtInvoiceNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtInvoiceNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call CmdSearchInvoice_Click(CmdSearchInvoice, New System.EventArgs())
    End Sub

    Private Sub txtInvoiceNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtInvoiceNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim mRsTemp As ADODB.Recordset
        Dim SqlStr As String
        If Trim(txtInvoiceNo.Text) = "" Then GoTo EventExitSub

        SqlStr = "SELECT FIN_INVOICE_HDR.AUTO_KEY_INVOICE,FIN_INVOICE_HDR.INVOICE_DATE, " & vbCrLf _
                & " FIN_INVOICE_DET.ITEM_CODE,FIN_INVOICE_DET.ITEM_QTY " & vbCrLf _
                & " FROM FIN_INVOICE_HDR,FIN_INVOICE_DET  " & vbCrLf _
                & " WHERE FIN_INVOICE_HDR.AUTO_KEY_INVOICE=FIN_INVOICE_DET.AUTO_KEY_INVOICE " & vbCrLf _
                & " AND FIN_INVOICE_HDR.COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                & " AND FIN_INVOICE_DET.ITEM_CODE ='" & MainClass.AllowSingleQuote(txtProduct.Text) & "' " & vbCrLf _
                & " AND FIN_INVOICE_HDR.SUPP_CUST_CODE ='" & MainClass.AllowSingleQuote(txtCustomer.Text) & "' " & vbCrLf _
                & " AND FIN_INVOICE_HDR.AUTO_KEY_INVOICE =" & Val(TxtInvoiceNo.Text) & " "


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With mRsTemp
            If Not .EOF Then
                txtInvoiceNo.Text = IIf(IsDbNull(mRsTemp.Fields("AUTO_KEY_INVOICE").Value), "", .Fields("AUTO_KEY_INVOICE").Value)
                lblInvoiceDate.Text = IIf(IsDbNull(mRsTemp.Fields("INVOICE_DATE").Value), "", .Fields("INVOICE_DATE").Value)
                lblInvoiceQty.Text = CStr(Val(IIf(IsDbNull(mRsTemp.Fields("ITEM_QTY").Value), "", .Fields("ITEM_QTY").Value)))
            Else
                MsgBox("Not a valid Customer's Invoice.")
                txtInvoiceNo.Text = ""
                lblInvoiceDate.Text = ""
                lblInvoiceQty.Text = ""
                Cancel = True
            End If
        End With
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtProdAnalysisRep_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProdAnalysisRep.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtProdDespos_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProdDespos.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtQuantity_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtQuantity.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtQuantity_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtQuantity.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtQuantity_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtQuantity.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Val(txtQuantity.Text) > Val(lblInvoiceQty.Text) Then
            MsgBox("Quantity Cann't Be Greater Than Invoice Qty : " & lblInvoiceQty.Text)
            Cancel = True
        End If
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRefDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRefDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRefDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRefDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtRefDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtRefDate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
        Else

        End If
EventExitSub:
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

    Private Sub txtResponsibility_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtResponsibility.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTargetDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTargetDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTargetDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTargetDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtTargetDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtTargetDate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtNumber_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNumber.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart

        If Not RsRejProdAnalysis.EOF Then
            lblMkey.Text = IIf(IsDbNull(RsRejProdAnalysis.Fields("AUTO_KEY_REJPROD").Value), "", RsRejProdAnalysis.Fields("AUTO_KEY_REJPROD").Value)
            txtNumber.Text = IIf(IsDbNull(RsRejProdAnalysis.Fields("AUTO_KEY_REJPROD").Value), "", RsRejProdAnalysis.Fields("AUTO_KEY_REJPROD").Value)
            txtDate.Text = IIf(IsDbNull(RsRejProdAnalysis.Fields("REC_DATE").Value), "", RsRejProdAnalysis.Fields("REC_DATE").Value)
            txtCustomer.Text = IIf(IsDbNull(RsRejProdAnalysis.Fields("SUPP_CUST_CODE").Value), "", RsRejProdAnalysis.Fields("SUPP_CUST_CODE").Value)
            txtCustomer_Validating(txtCustomer, New System.ComponentModel.CancelEventArgs(False))
            txtProduct.Text = IIf(IsDbNull(RsRejProdAnalysis.Fields("ITEM_CODE").Value), "", RsRejProdAnalysis.Fields("ITEM_CODE").Value)
            txtProduct_Validating(txtProduct, New System.ComponentModel.CancelEventArgs(False))
            txtInvoiceNo.Text = IIf(IsDbNull(RsRejProdAnalysis.Fields("CMP_INV_NO").Value), "", RsRejProdAnalysis.Fields("CMP_INV_NO").Value)
            lblInvoiceDate.Text = IIf(IsDbNull(RsRejProdAnalysis.Fields("CMP_INV_DATE").Value), "", RsRejProdAnalysis.Fields("CMP_INV_DATE").Value)
            txtRefNo.Text = IIf(IsDbNull(RsRejProdAnalysis.Fields("REF_NO").Value), "", RsRejProdAnalysis.Fields("REF_NO").Value)
            txtRefDate.Text = IIf(IsDbNull(RsRejProdAnalysis.Fields("REF_DATE").Value), "", RsRejProdAnalysis.Fields("REF_DATE").Value)
            txtCustDefReported.Text = IIf(IsDbNull(RsRejProdAnalysis.Fields("DEFECT_REPT_CUST").Value), "", RsRejProdAnalysis.Fields("DEFECT_REPT_CUST").Value)
            txtProdAnalysisRep.Text = IIf(IsDbNull(RsRejProdAnalysis.Fields("PROD_ANALY_REPT").Value), "", RsRejProdAnalysis.Fields("PROD_ANALY_REPT").Value)
            chkProdIs.CheckState = IIf(IsDbNull(RsRejProdAnalysis.Fields("PRODUCT_STATUS").Value) Or (RsRejProdAnalysis.Fields("PRODUCT_STATUS")).Value = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
            txtQuantity.Text = CStr(Val(IIf(IsDbNull(RsRejProdAnalysis.Fields("ITEM_QTY").Value), "", RsRejProdAnalysis.Fields("ITEM_QTY").Value)))
            txtProdDespos.Text = IIf(IsDbNull(RsRejProdAnalysis.Fields("PROD_DESPOSITION").Value), "", RsRejProdAnalysis.Fields("PROD_DESPOSITION").Value)
            txtTargetDate.Text = IIf(IsDbNull(RsRejProdAnalysis.Fields("TARGET_DATE").Value), "", RsRejProdAnalysis.Fields("TARGET_DATE").Value)
            txtActionDate.Text = IIf(IsDbNull(RsRejProdAnalysis.Fields("ACTION_DATE").Value), "", RsRejProdAnalysis.Fields("ACTION_DATE").Value)
            txtResponsibility.Text = IIf(IsDbNull(RsRejProdAnalysis.Fields("RESP_DESPOSITION").Value), "", RsRejProdAnalysis.Fields("RESP_DESPOSITION").Value)
            txtCompletingDate.Text = IIf(IsDbNull(RsRejProdAnalysis.Fields("ACTION_COMPL_DATE").Value), "", RsRejProdAnalysis.Fields("ACTION_COMPL_DATE").Value)
            chkCAPANeed.CheckState = IIf(IsDbNull(RsRejProdAnalysis.Fields("NEED_FOR_CP_ACTION").Value) Or (RsRejProdAnalysis.Fields("NEED_FOR_CP_ACTION")).Value = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
            txtInvestDate.Text = IIf(IsDbNull(RsRejProdAnalysis.Fields("INVEST_DATE").Value), "", RsRejProdAnalysis.Fields("INVEST_DATE").Value)
            txtInvestBy.Text = IIf(IsDbNull(RsRejProdAnalysis.Fields("INVEST_EMP_CODE").Value), "", RsRejProdAnalysis.Fields("INVEST_EMP_CODE").Value)
            txtInvestBy_Validating(txtInvestBy, New System.ComponentModel.CancelEventArgs(False))
            Call MakeEnableDesableField(False)
        End If
        ADDMode = False
        MODIFYMode = False
        txtNumber.Enabled = True
        cmdSearchNo.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsRejProdAnalysis, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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

        If MODIFYMode = True And RsRejProdAnalysis.BOF = False Then xMKey = RsRejProdAnalysis.Fields("AUTO_KEY_REJPROD").Value

        SqlStr = "SELECT * FROM QAL_REJPRODANALY_TRN " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_REJPROD,LENGTH(AUTO_KEY_REJPROD)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_REJPROD=" & mSlipNo & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRejProdAnalysis, ADODB.LockTypeEnum.adLockReadOnly)
        If RsRejProdAnalysis.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Number. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM QAL_REJPRODANALY_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_REJPROD,LENGTH(AUTO_KEY_REJPROD)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_REJPROD=" & Val(CStr(xMKey)) & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRejProdAnalysis, ADODB.LockTypeEnum.adLockReadOnly)
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
        txtInvoiceNo.Enabled = mMode
        CmdSearchInvoice.Enabled = mMode
        txtInvestBy.Enabled = mMode
        cmdSearchInvestBy.Enabled = mMode
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
    Private Sub ReportOnRejProdAnalysis(ByRef Mode As Crystal.DestinationConstants)

    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnRejProdAnalysis(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnRejProdAnalysis(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
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

    Private Sub txtRefNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRefNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtActionDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtActionDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtActionDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtActionDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtActionDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtActionDate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
        Else
            If Trim(txtActionDate.Text) = "" Or Trim(txtCompletingDate.Text) = "" Then GoTo EventExitSub
            If CDate(txtActionDate.Text) > CDate(txtCompletingDate.Text) Then
                MsgBox("Action Date should be less than Completing Date")
                Cancel = True
            End If
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtCompletingDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCompletingDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCompletingDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCompletingDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtCompletingDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtCompletingDate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
        Else
            If Trim(txtActionDate.Text) = "" Or Trim(txtCompletingDate.Text) = "" Then GoTo EventExitSub
            If CDate(txtCompletingDate.Text) < CDate(txtActionDate.Text) Then
                MsgBox("Completing Date should be greater than Action Date.")
                Cancel = True
            End If
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
