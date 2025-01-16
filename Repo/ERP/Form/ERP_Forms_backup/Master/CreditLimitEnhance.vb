Option Strict Off
Option Explicit On
Imports System.ComponentModel
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmCreditLimitEnhance
    Inherits System.Windows.Forms.Form
    Dim RsTransMain As ADODB.Recordset ''Recordset
    'Private PvtDBCn As ADODB.Connection

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim xMyMenu As String

    Dim FormActive As Boolean


    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click
        On Error GoTo AddErr
        If cmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
        Else
            cmdAdd.Text = ConCmdAddCaption
            ADDMode = False
            MODIFYMode = False
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
        Dim mSqlStr As String

        'If PubUserID <> "G0416" Then Exit Sub

        'If Not RsTransMain.EOF Then
        '    If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
        '        PubDBCn.Errors.Clear()
        '        PubDBCn.BeginTrans()
        '        If InsertIntoDelAudit(PubDBCn, "GEN_INVOICE_UNLOCK_TRN", (txtMRRNo.Text), RsTransMain, "AUTO_KEY_GATE") = False Then GoTo DelErrPart

        '        mSqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND AUTO_KEY_GATE=" & Val(txtMRRNo.Text) & ""

        '        PubDBCn.Execute("Delete from GEN_INVOICE_UNLOCK_TRN Where " & mSqlStr)

        '        PubDBCn.CommitTrans()
        '        RsTransMain.Requery() ''.Refresh
        '        Clear1()
        '    End If
        'End If
        Exit Sub
DelErrPart:
        ''Resume
        PubDBCn.RollbackTrans() ''
        RsTransMain.Requery() ''.Refresh
        If Err.Description <> "" Then
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdModify.Click

        On Error GoTo ModifyErr

        If cmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsTransMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ModifyErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportONPrint(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportONPrint(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportONPrint(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""
        Dim SqlStr As String = ""
        Dim mRptFileName As String
        Dim mVNo As String

        '    Report1.Reset
        '    MainClass.ClearCrptFormulas Report1
        '
        '    SqlStr = ""
        '
        '    Call MainClass.ClearCrptFormulas(Report1)
        '
        '
        '    mTitle = "Item RelationShip"
        '    mSubTitle = ""
        '    mRptFileName = "IR.rpt"
        '
        '    Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String)
        On Error GoTo ErrPart

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle, True, xMyMenu)
        Report1.ReportFileName = PubReportFolderPath & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False

        Report1.Action = 1
        Report1.Reset()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
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
            txtSupplier_Validating(txtSupplier, New System.ComponentModel.CancelEventArgs(False))
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
    Private Sub frmCreditLimitEnhance_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub
    Private Sub SprdView_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdView.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        With SprdView
            .Row = eventArgs.row
            .Col = 1
            txtSupplier.Text = .Text

            .Col = 3
            txtAppDate.Text = VB6.Format(.Text, "DD/MM/YYYY")

            txtSupplier_Validating(txtSupplier, New System.ComponentModel.CancelEventArgs(False))
            If txtSupplier.Enabled = True Then txtSupplier.Focus()
            CmdView_Click(CmdView, New System.EventArgs())
        End With
    End Sub
    Private Function UpdateMain1() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()


        SqlStr = ""


        If ADDMode = True Then
            SqlStr = "INSERT INTO GEN_INVOICE_UNLOCK_TRN (" & vbCrLf _
                & " COMPANY_CODE, SUPP_CUST_CODE, APP_DATE, REQUEST_BY, " & vbCrLf _
                & " AUTH_GIVEN_BY, CREDIT_LIMIT, REMARKS, " & vbCrLf _
                & " ADDUSER,ADDDATE,MODUSER,MODDATE)" & vbCrLf _
                & " VALUES( " & vbCrLf _
                & " " & RsCompany.Fields("Company_Code").Value & ", '" & MainClass.AllowSingleQuote(txtSupplier.Text) & "', " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtAppDate.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtRequestName.Text)) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtAuthorityName.Text)) & "', " & vbCrLf _
                & " " & Val(txtCreditLimit.Text) & ", " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtReason.Text)) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'','')"
        ElseIf MODIFYMode = True Then
            SqlStr = ""
            SqlStr = "UPDATE GEN_INVOICE_UNLOCK_TRN SET " & vbCrLf _
                & " REQUEST_BY='" & MainClass.AllowSingleQuote(txtRequestName.Text) & "'," & vbCrLf _
                & " CREDIT_LIMIT=" & Val(txtCreditLimit.Text) & "," & vbCrLf _
                & " AUTH_GIVEN_BY='" & MainClass.AllowSingleQuote(txtAuthorityName.Text) & "'," & vbCrLf _
                & " REMARKS='" & MainClass.AllowSingleQuote(txtReason.Text) & "'," & vbCrLf _
                & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtSupplier.Text) & "'" & vbCrLf _
                & " AND APP_DATE=TO_DATE('" & VB6.Format(txtAppDate.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY')"
        End If

        PubDBCn.Execute(SqlStr)

        UpdateMain1 = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        '    Resume
        UpdateMain1 = False
        PubDBCn.RollbackTrans() ''
        RsTransMain.Requery() ''.Refresh
        If Err.Description = "" Then Exit Function
        If Err.Number = -2147217900 Then
            ErrorMsg("Duplicate Ref Generated, Save Again", "Duplicate", MsgBoxStyle.Critical)
        Else
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If

    End Function
    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim mRow As Integer
        Dim mItemCode As String
        Dim mRGPDate As String
        Dim mDetailFromDate As String
        Dim mDetailToDate As String

        FieldsVarification = True

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsTransMain.EOF = True Then Exit Function

        If Trim(txtSupplier.Text) = "" Then
            MsgInformation("Customer Name can not be Blank")
            FieldsVarification = False
            Exit Function
        End If

        If txtRequestName.Text = "" Then
            MsgBox("Request By Name is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtRequestName.Focus()
            Exit Function
        End If

        If txtAuthorityName.Text = "" Then
            MsgBox("Authority Name is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtAuthorityName.Focus()
            Exit Function
        End If

        If txtAppDate.Text = "" Then
            MsgBox("Approval date is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtAppDate.Focus()
            Exit Function
        End If

        If Not IsDate(txtAppDate.Text) Then
            MsgBox("Approval date is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtAppDate.Focus()
            Exit Function

        End If
        If Val(txtCreditLimit.Text) = 0 Then
            MsgBox("Credit Limit is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtCreditLimit.Focus()
            Exit Function
        End If

        If Val(txtCreditLimit.Text) < Val(lblCurrentLimit.Text) Then
            MsgBox("Credit Limit cann't be update less than current limit", MsgBoxStyle.Information)
            FieldsVarification = False
            txtCreditLimit.Focus()
            Exit Function
        End If

        If CDate(txtAppDate.Text) < CDate(PubCurrDate) Then
            MsgBox("Approval Date Cann't be Less than Current Date.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Function

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub

    Public Sub frmCreditLimitEnhance_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = ""
        SqlStr = "Select * from GEN_INVOICE_UNLOCK_TRN Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTransMain, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)
        '    Call SetTextLengths

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

        SqlStr = "SELECT IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, " & vbCrLf _
                & " IH.APP_DATE, IH.REQUEST_BY, IH.AUTH_GIVEN_BY, " & vbCrLf _
                & " IH.CREDIT_LIMIT, IH.REMARKS, IH.ADDUSER, IH.ADDDATE," & vbCrLf _
                & " IH.MODUSER, IH.MODDATE"

        ''FROM CLAUSE...

        SqlStr = SqlStr & vbCrLf _
            & " FROM GEN_INVOICE_UNLOCK_TRN IH,  FIN_SUPP_CUST_MST CMST"

        ''WHERE CLAUSE...

        SqlStr = SqlStr & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE"

        ''ORDER BY CLAUSE...

        SqlStr = SqlStr & vbCrLf & " Order by IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, IH.APP_DATE"

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
            .set_ColWidth(1, 1000)
            .set_ColWidth(2, 2500)
            .set_ColWidth(3, 1000)
            .set_ColWidth(4, 1000)
            .set_ColWidth(5, 1000)
            .set_ColWidth(6, 1000)
            .set_ColWidth(7, 1000)
            .set_ColWidth(8, 1000)
            .set_ColWidth(9, 1000)
            .set_ColWidth(10, 1000)
            .set_ColWidth(11, 1000)

            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            SprdView.set_RowHeight(-1, 300)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        With RsTransMain
            txtAuthorityName.MaxLength = .Fields("AUTH_GIVEN_BY").DefinedSize
            txtReason.MaxLength = .Fields("REMARKS").DefinedSize



            txtAppDate.MaxLength = 10
            txtSupplier.MaxLength = .Fields("SUPP_CUST_CODE").DefinedSize

            txtCreditLimit.MaxLength = .Fields("CREDIT_LIMIT").Precision ' VB6.Format(IIf(IsDBNull(.Fields("CREDIT_LIMIT").Value), 0, .Fields("CREDIT_LIMIT").Value), "0.00")

            txtRequestName.MaxLength = .Fields("REQUEST_BY").DefinedSize



        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub Show1()

        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        With RsTransMain
            If Not .EOF Then

                'SqlStr = " SELECT IH.*, CMST.SUPP_CUST_NAME " & vbCrLf _
                '    & " FROM GEN_INVOICE_UNLOCK_TRN IH, FIN_SUPP_CUST_MST CMST" & vbCrLf _
                '    & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                '    & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
                '    & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
                '    & " AND IH.SUPP_CUST_CODE='" & Trim(txtSupplier.Text) & "' " & vbCrLf _
                '    & " AND APP_DATE=TO_DATE('" & VB6.Format(txtAppDate.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY')"

                'MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


                txtAppDate.Text = VB6.Format(IIf(IsDBNull(.Fields("APP_DATE").Value), "", .Fields("APP_DATE").Value), "DD/MM/YYYY")
                txtSupplier.Text = Trim(IIf(IsDBNull(.Fields("SUPP_CUST_CODE").Value), "", .Fields("SUPP_CUST_CODE").Value))
                lblSupplierName.Text = Trim(IIf(IsDBNull(.Fields("SUPP_CUST_NAME").Value), "", .Fields("SUPP_CUST_NAME").Value))

                txtCreditLimit.Text = VB6.Format(IIf(IsDBNull(.Fields("CREDIT_LIMIT").Value), 0, .Fields("CREDIT_LIMIT").Value), "0.00")

                txtRequestName.Text = IIf(IsDBNull(.Fields("REQUEST_BY").Value), "", .Fields("REQUEST_BY").Value)
                txtAuthorityName.Text = IIf(IsDBNull(.Fields("AUTH_GIVEN_BY").Value), "", .Fields("AUTH_GIVEN_BY").Value)
                txtReason.Text = IIf(IsDBNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)


                If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_CODE", "CREDIT_LIMIT", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    lblCurrentLimit.Text = MasterNo
                End If


                lblLedgerBal.Text = GetOpeningBal(txtSupplier.Text, "",,, "", "Y", "")


                txtAuthorityName.Enabled = False
                txtAppDate.Enabled = False
                txtSupplier.Enabled = False
                txtReason.Enabled = True
                txtCreditLimit.Enabled = True


            End If
        End With
        ADDMode = False
        MODIFYMode = False

        MainClass.ButtonStatus(Me, XRIGHT, RsTransMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
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
        MainClass.ButtonStatus(Me, XRIGHT, RsTransMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub Clear1()


        txtCreditLimit.Text = ""
        txtAppDate.Text = ""
        txtSupplier.Text = ""
        lblSupplierName.Text = ""
        txtRequestName.Text = ""
        txtAuthorityName.Text = ""
        txtReason.Text = ""

        lblCurrentLimit.Text = ""
        lblLedgerBal.Text = ""
        txtAuthorityName.Enabled = True
        txtAppDate.Enabled = True
        txtSupplier.Enabled = True
        txtReason.Enabled = True
        txtCreditLimit.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsTransMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub frmCreditLimitEnhance_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmCreditLimitEnhance_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        '    MainClass.DoFunctionKey Me, KeyCode
    End Sub

    Public Sub frmCreditLimitEnhance_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
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
        'Me.Height = VB6.TwipsToPixelsY(3420)
        ''Me.Width = VB6.TwipsToPixelsX(9915)

        AdoDCMain.Visible = False

        FormActive = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub txtAuthorityName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAuthorityName.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAuthorityName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAuthorityName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtAuthorityName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtCreditLimit_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCreditLimit.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCreditLimit_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCreditLimit.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtReason_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtReason.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtReason_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtReason.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtReason.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSupplier_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplier.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtSupplier_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSupplier.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSupplier.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSupplier_Validating(sender As Object, EventArgs As CancelEventArgs) Handles txtSupplier.Validating
        Dim Cancel As Boolean = EventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        Dim mSupplierCode As String
        Dim mSupplierName As String = ""

        If Trim(txtSupplier.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            lblSupplierName.Text = MasterNo
        Else
            Cancel = True
            GoTo EventExitSub
        End If

        If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_CODE", "CREDIT_LIMIT", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            lblCurrentLimit.Text = MasterNo
        End If


        lblLedgerBal.Text = GetOpeningBal(txtSupplier.Text, "",,, "", "Y", "")

        If Trim(txtAppDate.Text) = "" Then GoTo EventExitSub

        If Not IsDate(txtAppDate.Text) Then GoTo EventExitSub



        SqlStr = " SELECT IH.*, CMST.SUPP_CUST_NAME " & vbCrLf _
                    & " FROM GEN_INVOICE_UNLOCK_TRN IH, FIN_SUPP_CUST_MST CMST" & vbCrLf _
                    & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
                    & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
                    & " AND IH.SUPP_CUST_CODE='" & Trim(txtSupplier.Text) & "' " & vbCrLf _
                    & " AND APP_DATE=TO_DATE('" & VB6.Format(txtAppDate.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTransMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTransMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Customer, Use Generate Option To add", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM GEN_INVOICE_UNLOCK_TRN " & " WHERE SUPP_CUST_CODE='" & Trim(txtSupplier.Text) & "' AND  APP_DATE=TO_DATE('" & VB6.Format(txtAppDate.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY')"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTransMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        EventArgs.Cancel = Cancel
    End Sub

    Private Sub txtAppDate_TextChanged(sender As Object, e As EventArgs) Handles txtAppDate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAppDate_Validating(sender As Object, EventArgs As CancelEventArgs) Handles txtAppDate.Validating
        Dim Cancel As Boolean = EventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        Dim mSupplierCode As String
        Dim mSupplierName As String = ""

        If Trim(txtSupplier.Text) = "" Then GoTo EventExitSub
        If Trim(txtAppDate.Text) = "" Then GoTo EventExitSub

        If Not IsDate(txtAppDate.Text) Then
            Cancel = True
            GoTo EventExitSub
        End If

        'If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
        '    Cancel = True
        '    GoTo EventExitSub
        'End If


        SqlStr = " SELECT IH.*, CMST.SUPP_CUST_NAME " & vbCrLf _
                    & " FROM GEN_INVOICE_UNLOCK_TRN IH, FIN_SUPP_CUST_MST CMST" & vbCrLf _
                    & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
                    & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
                    & " AND IH.SUPP_CUST_CODE='" & Trim(txtSupplier.Text) & "' " & vbCrLf _
                    & " AND APP_DATE=TO_DATE('" & VB6.Format(txtAppDate.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTransMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTransMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Customer, Use Generate Option To add", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM GEN_INVOICE_UNLOCK_TRN " & " WHERE SUPP_CUST_CODE='" & Trim(txtSupplier.Text) & "' AND  APP_DATE=TO_DATE('" & VB6.Format(txtAppDate.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY')"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTransMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        EventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRequestName_TextChanged(sender As Object, e As EventArgs) Handles txtRequestName.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtRequestName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRequestName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtRequestName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSupplier_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSupplier.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccount(txtSupplier)
    End Sub
    Private Sub txtSupplier_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplier.DoubleClick
        Call SearchAccount(txtSupplier)
    End Sub
    Private Sub SearchAccount(ByRef mTextBox As System.Windows.Forms.TextBox)
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('C','S')"
        If MainClass.SearchGridMaster(mTextBox.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_CODE", "SUPP_CUST_NAME", , , SqlStr) = True Then
            mTextBox.Text = AcName
        End If
        txtSupplier_Validating(txtSupplier, New System.ComponentModel.CancelEventArgs(False))
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
End Class
