Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmESICChallan
    Inherits System.Windows.Forms.Form
    Dim RsChallanMain As ADODB.Recordset
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection
    Dim Shw As Boolean
    Dim FormActive As Boolean
    Dim xRefNo As Integer
    Dim SqlStr As String = ""
    Private Sub settextlength()
        On Error GoTo ERR1

        txtPaymentDate.Maxlength = 10
        txtEstableCode.Maxlength = RsChallanMain.Fields("ESI_ESTAB").DefinedSize
        txtRefDate.Maxlength = 10
        txtRefNo.Maxlength = RsChallanMain.Fields("REFNO").Precision
        txtTotalAmount.Maxlength = RsChallanMain.Fields("TOTAL_AMOUNT").Precision
        txtDepositor.Maxlength = RsChallanMain.Fields("DEPOSITOR_NAME").DefinedSize
        txtChqNo.Maxlength = RsChallanMain.Fields("CHEQUE_NO").DefinedSize
        txtBankName.Maxlength = RsChallanMain.Fields("BANK_NAME").DefinedSize
        txtChqDate.Maxlength = 10

        txtChallanNo.Maxlength = RsChallanMain.Fields("CHALLAN_NO").DefinedSize
        txtOnRollEmp.Maxlength = RsChallanMain.Fields("TOT_EMP1").Precision
        txtOtherEmp.Maxlength = RsChallanMain.Fields("TOT_EMP2").Precision
        txtTotEmp.Maxlength = RsChallanMain.Fields("TOT_EMP").Precision
        txtOnRollWages.Maxlength = RsChallanMain.Fields("TOT_WAGES1").Precision
        txtOtherWages.Maxlength = RsChallanMain.Fields("TOT_WAGES2").Precision
        txtTotWages.Maxlength = RsChallanMain.Fields("TOT_WAGES").Precision
        txtOnRollEmpCont.Maxlength = RsChallanMain.Fields("TOT_EMP_CONT1").Precision
        txtOtherEmpCont.Maxlength = RsChallanMain.Fields("TOT_EMP_CONT2").Precision
        txtTotalEmpCont.Maxlength = RsChallanMain.Fields("TOT_EMP_CONT").Precision
        txtOnRollEmperCont.Maxlength = RsChallanMain.Fields("TOT_EMPER_CONT1").Precision
        txtOtherEmperCont.Maxlength = RsChallanMain.Fields("TOT_EMPER_CONT2").Precision
        txtTotalEmperCont.Maxlength = RsChallanMain.Fields("TOT_EMPER_CONT").Precision
        txtTotalContribution.Maxlength = RsChallanMain.Fields("TOT_CONTRIBUTION").Precision

        txtDepositorCode.Maxlength = RsChallanMain.Fields("DEPOSITOR_CODE").DefinedSize
        txtBankSLNo.Maxlength = RsChallanMain.Fields("BANK_SLNO").DefinedSize
        txtBankDate.Text = CStr(10)

        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
    Private Sub Clear1()


        LblMKey.Text = ""
        cboPaidBy.SelectedIndex = -1
        txtRefNo.Text = ""
        txtRefDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtEstableCode.Text = IIf(IsDbNull(RsCompany.Fields("ESIEST").Value), "", RsCompany.Fields("ESIEST").Value)
        txtEstableCode.Enabled = False
        txtPaymentDate.Text = ""
        txtChallanNo.Text = ""
        txtOnRollEmp.Text = "0"
        txtOtherEmp.Text = "0"
        txtTotEmp.Text = "0"
        txtOnRollWages.Text = "0.00"
        txtOtherWages.Text = "0.00"
        txtTotWages.Text = "0.00"
        txtOnRollEmpCont.Text = "0.00"
        txtOtherEmpCont.Text = "0.00"
        txtTotalEmpCont.Text = "0.00"
        txtOnRollEmperCont.Text = "0.00"
        txtOtherEmperCont.Text = "0.00"
        txtTotalEmperCont.Text = "0.00"
        txtTotalContribution.Text = "0.00"
        txtTotalAmount.Text = "0.00"
        txtDepositorCode.Text = ""
        txtDepositor.Text = ""
        txtBankName.Text = ""
        txtChqNo.Text = ""
        txtChqDate.Text = ""
        txtBankSLNo.Text = ""
        txtBankDate.Text = ""

        txtRefNo.Enabled = True

        Call CalcAmount()
        MainClass.ButtonStatus(Me, XRIGHT, RsChallanMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub cboPaidBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboPaidBy.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboPaidBy_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboPaidBy.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr
        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            txtRefNo.Enabled = False
            MainClass.ButtonStatus(Me, XRIGHT, RsChallanMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            txtRefNo.Enabled = True
            Show1()
        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
        Else
            ADDMode = False
            MODIFYMode = False
            txtRefNo.Enabled = True
            If RsChallanMain.EOF = False Then RsChallanMain.MoveFirst()
            Show1()
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
        'Resume
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If txtRefNo.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub
        If Not RsChallanMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                If Delete1 = False Then GoTo DelErrPart
                If RsChallanMain.EOF = True Then
                    Clear1()
                Else
                    Show1()
                End If
            End If
        End If
        Exit Sub
DelErrPart:
        MsgBox("Record Not Deleted")
    End Sub
    Private Function Delete1() As Boolean
        On Error GoTo DeleteErr

        SqlStr = ""
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If InsertIntoDelAudit(PubDBCn, "PAY_ESICCHALLAN_TRN", (LblMKey.Text), RsChallanMain) = False Then GoTo DeleteErr
        If InsertIntoDeleteTrn(PubDBCn, "PAY_ESICCHALLAN_TRN", "MKEY", (LblMKey.Text)) = False Then GoTo DeleteErr

        SqlStr = "Delete from PAY_ESICCHALLAN_TRN where MKey='" & LblMKey.Text & "' "
        PubDBCn.Execute(SqlStr)


        PubDBCn.CommitTrans()
        RsChallanMain.Requery()
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans()
        RsChallanMain.Requery()
        MsgBox(Err.Description)
    End Function


    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForPrint(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForPrint(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportForPrint(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        PubDBCn.Errors.Clear()

        'Select Record for print...

        SqlStr = ""

        SqlStr = MakeSQL


        mTitle = "EMPLOYEES STATE INSURANCE FUN ACCOUNT No. - 1"
        mSubTitle = "PAY-IN-SLIP FOR CONTRIBUTION"

        Call ShowReport(SqlStr, "ESICChallan.Rpt", Mode, mTitle, mSubTitle)

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        If Err.Number = 32755 Or Err.Number = 20507 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        'Resume
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)

        Dim mAmountInword As String

        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        mAmountInword = MainClass.RupeesConversion(CDbl(IIf(Val(txtTotalAmount.Text) = 0, 0, txtTotalAmount.Text)))

        MainClass.AssignCRptFormulas(Report1, "AmountInWord=""" & mAmountInword & """")


        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub
    Private Function MakeSQL() As String
        On Error GoTo refreshErrPart


        MakeSQL = " Select IH.*  " & vbCrLf & " FROM PAY_ESICCHALLAN_TRN IH " & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY='" & MainClass.AllowSingleQuote((LblMKey.Text)) & "' "

        Exit Function
refreshErrPart:
        MsgBox(Err.Description)
        'Resume
    End Function
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
            FraView.BringToFront()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsChallanMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmESICChallan_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub


    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
        txtRefNo.Text = Trim(SprdView.Text)

        txtRefNo_Validating(txtRefNo, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())

    End Sub
    Private Sub frmESICChallan_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        MainClass.UOpenRecordSet("Select * From PAY_ESICCHALLAN_TRN Where 1<>1", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsChallanMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        settextlength()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call AssignGrid(False)

        cboPaidBy.Items.Clear()
        cboPaidBy.Items.Add("Cheque")
        cboPaidBy.Items.Add("Cash")
        cboPaidBy.SelectedIndex = -1

        Clear1()
        If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmESICChallan_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Left = 0
        Me.Top = 0
        Me.Height = VB6.TwipsToPixelsY(5070)
        Me.Width = VB6.TwipsToPixelsX(11355)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmESICChallan_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        RsChallanMain = Nothing
        frmPFChallan = Nothing
        '    PubDBCn.Cancel
        '    PvtDBCn.Close
        '    Set PvtDBCn = Nothing
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim mSection As String

        Shw = True
        If Not RsChallanMain.EOF Then
            txtRefNo.Enabled = True
            With RsChallanMain
                LblMKey.Text = RsChallanMain.Fields("mKey").Value
                xRefNo = RsChallanMain.Fields("REFNO").Value
                txtRefNo.Text = VB6.Format(IIf(IsDbNull(.Fields("REFNO").Value), "", .Fields("REFNO").Value), "00000")
                txtRefDate.Text = VB6.Format(IIf(IsDbNull(.Fields("REFDATE").Value), "", .Fields("REFDATE").Value), "DD/MM/YYYY")
                txtEstableCode.Text = IIf(IsDbNull(.Fields("ESI_ESTAB").Value), "", .Fields("ESI_ESTAB").Value)
                txtPaymentDate.Text = VB6.Format(IIf(IsDbNull(.Fields("PAYMENT_DATE").Value), "", .Fields("PAYMENT_DATE").Value), "DD/MM/YYYY")

                txtDepositor.Text = IIf(IsDbNull(.Fields("DEPOSITOR_NAME").Value), "", .Fields("DEPOSITOR_NAME").Value)
                txtBankName.Text = IIf(IsDbNull(.Fields("BANK_NAME").Value), "", .Fields("BANK_NAME").Value)
                txtChqNo.Text = IIf(IsDbNull(.Fields("CHEQUE_NO").Value), "", .Fields("CHEQUE_NO").Value)
                txtChqDate.Text = VB6.Format(IIf(IsDbNull(.Fields("CHEQUE_DATE").Value), "", .Fields("CHEQUE_DATE").Value), "DD/MM/YYYY")
                cboPaidBy.Text = IIf(IsDbNull(.Fields("PAID_BY").Value), "", .Fields("PAID_BY").Value)

                txtChallanNo.Text = IIf(IsDbNull(.Fields("CHALLAN_NO").Value), "", .Fields("CHALLAN_NO").Value)
                txtOnRollEmp.Text = VB6.Format(IIf(IsDbNull(.Fields("TOT_EMP1").Value), "0", .Fields("TOT_EMP1").Value), "0")
                txtOtherEmp.Text = VB6.Format(IIf(IsDbNull(.Fields("TOT_EMP2").Value), "0", .Fields("TOT_EMP2").Value), "0")
                txtTotEmp.Text = VB6.Format(IIf(IsDbNull(.Fields("TOT_EMP").Value), "0", .Fields("TOT_EMP").Value), "0")
                txtOnRollWages.Text = VB6.Format(IIf(IsDbNull(.Fields("TOT_WAGES1").Value), "0", .Fields("TOT_WAGES1").Value), "0.00")
                txtOtherWages.Text = VB6.Format(IIf(IsDbNull(.Fields("TOT_WAGES2").Value), "0", .Fields("TOT_WAGES2").Value), "0.00")
                txtTotWages.Text = VB6.Format(IIf(IsDbNull(.Fields("TOT_WAGES").Value), "0", .Fields("TOT_WAGES").Value), "0.00")
                txtOnRollEmpCont.Text = VB6.Format(IIf(IsDbNull(.Fields("TOT_EMP_CONT1").Value), "0", .Fields("TOT_EMP_CONT1").Value), "0.00")
                txtOtherEmpCont.Text = VB6.Format(IIf(IsDbNull(.Fields("TOT_EMP_CONT2").Value), "0", .Fields("TOT_EMP_CONT2").Value), "0.00")
                txtTotalEmpCont.Text = VB6.Format(IIf(IsDbNull(.Fields("TOT_EMP_CONT").Value), "0", .Fields("TOT_EMP_CONT").Value), "0.00")
                txtOnRollEmperCont.Text = VB6.Format(IIf(IsDbNull(.Fields("TOT_EMPER_CONT1").Value), "0", .Fields("TOT_EMPER_CONT1").Value), "0.00")
                txtOtherEmperCont.Text = VB6.Format(IIf(IsDbNull(.Fields("TOT_EMPER_CONT2").Value), "0", .Fields("TOT_EMPER_CONT2").Value), "0.00")
                txtTotalEmperCont.Text = VB6.Format(IIf(IsDbNull(.Fields("TOT_EMPER_CONT").Value), "0", .Fields("TOT_EMPER_CONT").Value), "0.00")
                txtTotalContribution.Text = VB6.Format(IIf(IsDbNull(.Fields("TOT_CONTRIBUTION").Value), "0", .Fields("TOT_CONTRIBUTION").Value), "0.00")
                txtTotalAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("TOTAL_AMOUNT").Value), "0", .Fields("TOTAL_AMOUNT").Value), "0.00")
                txtDepositorCode.Text = IIf(IsDbNull(.Fields("DEPOSITOR_CODE").Value), "", .Fields("DEPOSITOR_CODE").Value)
                txtBankSLNo.Text = IIf(IsDbNull(.Fields("BANK_SLNO").Value), "", .Fields("BANK_SLNO").Value)
                txtBankDate.Text = VB6.Format(IIf(IsDbNull(.Fields("CHEQUE_PRESENT").Value), "", .Fields("CHEQUE_PRESENT").Value), "DD/MM/YYYY")


            End With
        End If
        Shw = False
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsChallanMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        If Update1 = True Then
            txtRefNo_Validating(txtRefNo, New System.ComponentModel.CancelEventArgs(False))
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
    Private Function Update1() As Boolean

        On Error GoTo UpdateError
        Dim mRefNo As Integer
        Dim pMkey As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = ""

        If ADDMode = True Then
            mRefNo = MaxRefNo()
            pMkey = RsCompany.Fields("COMPANY_CODE").Value & RsCompany.Fields("FYEAR").Value & mRefNo

            txtRefNo.Text = CStr(mRefNo)

            SqlStr = "INSERT INTO PAY_ESICCHALLAN_TRN ( " & vbCrLf & " MKEY, COMPANY_CODE, FYEAR, " & vbCrLf & " REFNO, REFDATE, ESI_ESTAB, " & vbCrLf & " PAID_BY, CHALLAN_NO, TOT_EMP1, " & vbCrLf & " TOT_EMP2, TOT_EMP, TOT_WAGES1, " & vbCrLf & " TOT_WAGES2, TOT_WAGES, TOT_EMP_CONT1, " & vbCrLf & " TOT_EMP_CONT2, TOT_EMP_CONT, TOT_EMPER_CONT1, " & vbCrLf & " TOT_EMPER_CONT2, TOT_EMPER_CONT, TOT_CONTRIBUTION, " & vbCrLf & " TOTAL_AMOUNT, DEPOSITOR_CODE, DEPOSITOR_NAME, " & vbCrLf & " BANK_NAME, BANK_CODE, CHEQUE_NO, " & vbCrLf & " CHEQUE_DATE, BANK_SLNO, CHEQUE_PRESENT, " & vbCrLf & " CHEQUE_REALISATION, PAYMENT_DATE, " & vbCrLf & " ADDUSER, ADDDATE, MODUSER, MODDATE) VALUES ( "

            SqlStr = SqlStr & vbCrLf & " '" & pMkey & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf & " " & mRefNo & ", TO_DATE('" & VB6.Format(txtRefDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote((txtEstableCode.Text)) & "', " & vbCrLf & " '" & cboPaidBy.Text & "', '" & MainClass.AllowSingleQuote((txtChallanNo.Text)) & "', " & Val(txtOnRollEmp.Text) & ", " & vbCrLf & " " & Val(txtOtherEmp.Text) & ", " & Val(txtTotEmp.Text) & ", " & Val(txtOnRollWages.Text) & ", " & vbCrLf & " " & Val(txtOtherWages.Text) & ", " & Val(txtTotWages.Text) & ", " & Val(txtOnRollEmpCont.Text) & ", " & vbCrLf & " " & Val(txtOtherEmpCont.Text) & ", " & Val(txtTotalEmpCont.Text) & ", " & Val(txtOnRollEmperCont.Text) & ", " & vbCrLf & " " & Val(txtOtherEmperCont.Text) & ", " & Val(txtTotalEmperCont.Text) & ", " & Val(txtTotalContribution.Text) & ", " & vbCrLf & " " & Val(txtTotalAmount.Text) & ", '" & MainClass.AllowSingleQuote((txtDepositorCode.Text)) & "', '" & MainClass.AllowSingleQuote((txtDepositor.Text)) & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote((txtBankName.Text)) & "', '', '" & MainClass.AllowSingleQuote((txtChqNo.Text)) & "', " & vbCrLf & "  TO_DATE('" & VB6.Format(txtChqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote((txtBankSLNo.Text)) & "', TO_DATE('" & VB6.Format(txtBankDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '', TO_DATE('" & VB6.Format(txtPaymentDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"

        Else
            SqlStr = "UPDATE PAY_ESICCHALLAN_TRN SET " & vbCrLf & " REFNO=" & Val(txtRefNo.Text) & "," & vbCrLf & " REFDATE=TO_DATE('" & VB6.Format(txtRefDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " ESI_ESTAB='" & MainClass.AllowSingleQuote((txtEstableCode.Text)) & "'," & vbCrLf & " PAID_BY='" & cboPaidBy.Text & "'," & vbCrLf & " PAYMENT_DATE=TO_DATE('" & VB6.Format(txtPaymentDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " CHALLAN_NO='" & MainClass.AllowSingleQuote((txtChallanNo.Text)) & "'," & vbCrLf & " TOT_EMP1=" & Val(txtOnRollEmp.Text) & "," & vbCrLf & " TOT_EMP2=" & Val(txtOtherEmp.Text) & "," & vbCrLf & " TOT_EMP=" & Val(txtTotEmp.Text) & "," & vbCrLf & " TOT_WAGES1=" & Val(txtOnRollWages.Text) & "," & vbCrLf & " TOT_WAGES2=" & Val(txtOtherWages.Text) & "," & vbCrLf & " TOT_WAGES=" & Val(txtTotWages.Text) & "," & vbCrLf & " TOT_EMP_CONT1=" & Val(txtOnRollEmpCont.Text) & "," & vbCrLf & " TOT_EMP_CONT2=" & Val(txtOtherEmpCont.Text) & ","

            SqlStr = SqlStr & vbCrLf & " TOT_EMP_CONT=" & Val(txtTotalEmpCont.Text) & "," & vbCrLf & " TOT_EMPER_CONT1=" & Val(txtOnRollEmperCont.Text) & "," & vbCrLf & " TOT_EMPER_CONT2=" & Val(txtOtherEmperCont.Text) & "," & vbCrLf & " TOT_EMPER_CONT=" & Val(txtTotalEmperCont.Text) & "," & vbCrLf & " TOT_CONTRIBUTION=" & Val(txtTotalContribution.Text) & "," & vbCrLf & " DEPOSITOR_CODE='" & MainClass.AllowSingleQuote((txtDepositorCode.Text)) & "'," & vbCrLf & " BANK_SLNO='" & MainClass.AllowSingleQuote((txtBankSLNo.Text)) & "'," & vbCrLf & " CHEQUE_PRESENT=TO_DATE('" & VB6.Format(txtBankDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),"

            SqlStr = SqlStr & vbCrLf & " TOTAL_AMOUNT=" & Val(txtTotalAmount.Text) & "," & vbCrLf & " DEPOSITOR_NAME='" & MainClass.AllowSingleQuote((txtDepositor.Text)) & "'," & vbCrLf & " BANK_NAME='" & MainClass.AllowSingleQuote((txtBankName.Text)) & "'," & vbCrLf & " BANK_CODE=''," & vbCrLf & " CHEQUE_NO='" & MainClass.AllowSingleQuote((txtChqNo.Text)) & "'," & vbCrLf & " CHEQUE_DATE=TO_DATE('" & VB6.Format(txtChqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " CHEQUE_REALISATION='',"

            SqlStr = SqlStr & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " WHERE MKEY=" & LblMKey.Text & ""

            pMkey = LblMKey.Text
        End If

UpdatePart:
        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()
        RsChallanMain.Requery()
        Update1 = True
        Exit Function
UpdateError:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsChallanMain.Requery()
        If Err.Number = -2147467259 Then
            MsgBox("Can't Modify Transaction Exists Against this Code")
            Exit Function
        End If
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        ''Resume
        PubDBCn.Errors.Clear()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function FieldsVarification() As Boolean
        On Error GoTo VarificationErr
        FieldsVarification = True

        If Trim(txtBankName.Text) = "" Then
            MsgInformation("Bank Name is empty. Cannot Save")
            txtBankName.Focus()
            FieldsVarification = False
            Exit Function
        End If


        If Trim(txtPaymentDate.Text) = "" Then
            MsgInformation("Payment Date is empty. Cannot Save")
            txtPaymentDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtChallanNo.Text) = "" Then
            MsgInformation("Challan No is empty. Cannot Save")
            txtChallanNo.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(cboPaidBy.Text) = "" Then
            MsgInformation("Paid By is empty. Cannot Save")
            cboPaidBy.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Val(txtTotalAmount.Text) = 0 Then
            MsgInformation("Total Amount is Zero. Cannot Save")
            '        txtTotalAmount.SetFocus
            FieldsVarification = False
            Exit Function
        End If
        '    If Trim(txtBankCode) = "" Then
        '        MsgInformation "Bank Code is empty. Cannot Save"
        '        txtBankCode.SetFocus
        '        FieldsVarification = False
        '        Exit Function
        '    End If
        '
        '     If Len(txtBankCode) <> 7 Then
        '        MsgInformation "Invalid Bank Code. Cannot Save"
        '        txtBankCode.SetFocus
        '        FieldsVarification = False
        '        Exit Function
        '    End If
        '    If Val(txtAmountPaid) = 0 Then
        '        MsgInformation "Deduction Amount is zero. Cannot Save"
        '        SprdMain.SetFocus
        '        FieldsVarification = False
        '        Exit Function
        '    End If

        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
        End If
        ''If MODIFYMode = True And (RsChallanMain.EOF=true Or RsChallanMain.EOF = True) Then Exit Function
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Function
VarificationErr:
        FieldsVarification = False
        MsgInformation(Err.Description)
    End Function
    Private Sub txtBankDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBankDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtBankDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBankDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtBankDate.Text) = "" Then GoTo EventExitSub

        If Not IsDate(txtBankDate.Text) Then
            MsgBox("Invalid Bank Date", MsgBoxStyle.Information)
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtBankName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBankName.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtBankName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBankName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtBankName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtBankSLNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBankSLNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBankSLNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBankSLNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtChallanNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtChallanNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtChallanNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtChallanNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDepositor_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDepositor.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDepositor_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDepositor.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDepositor.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDepositorCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDepositorCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDepositorCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDepositorCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDepositorCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEstableCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEstableCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtEstableCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEstableCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtEstableCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtOnRollEmp_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOnRollEmp.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOnRollEmpCont_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOnRollEmpCont.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOnRollEmpCont_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOnRollEmpCont.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtOnRollEmperCont_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOnRollEmperCont.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOnRollEmperCont_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOnRollEmperCont.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtOnRollWages_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOnRollWages.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOnRollWages_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOnRollWages.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtOtherEmp_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOtherEmp.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOtherEmp_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOtherEmp.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtOtherEmpCont_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOtherEmpCont.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOtherEmpCont_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOtherEmpCont.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtOtherEmperCont_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOtherEmperCont.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOtherEmperCont_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOtherEmperCont.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtOtherWages_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOtherWages.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOtherWages_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOtherWages.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPaymentDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPaymentDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtPaymentDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPaymentDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtPaymentDate.Text) = "" Then GoTo EventExitSub

        If Not IsDate(txtPaymentDate.Text) Then
            MsgBox("Invalid Payment Date", MsgBoxStyle.Information)
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtChqDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtChqDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtChqDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtChqDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtChqDate.Text) = "" Then GoTo EventExitSub

        If Not IsDate(txtChqDate.Text) Then
            MsgBox("Invalid Cheque / DD Date", MsgBoxStyle.Information)
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtChqNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtChqNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtChqNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtChqNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtRefDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRefDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtRefDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRefDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtRefDate.Text) = "" Then GoTo EventExitSub

        If Not IsDate(txtRefDate.Text) Then
            MsgBox("Invalid Ref Date", MsgBoxStyle.Information)
        End If

        Call CalcESIDetail()
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtRefNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRefNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRefNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRefNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRefNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRefNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1

        If Trim(txtRefNo.Text) = "" Then GoTo EventExitSub
        If MODIFYMode = True And RsChallanMain.EOF = False Then xRefNo = RsChallanMain.Fields("REFNO").Value

        SqlStr = ""
        SqlStr = "Select * from  PAY_ESICCHALLAN_TRN Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " ANd RefNo=" & txtRefNo.Text & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsChallanMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsChallanMain.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Click Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                MainClass.UOpenRecordSet("Select * From PAY_ESICCHALLAN_TRN Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND RefNo=" & xRefNo & "", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsChallanMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If

        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub CalcAmount()
        On Error GoTo ErrPart

        txtTotEmp.Text = VB6.Format(Val(txtOnRollEmp.Text) + Val(txtOtherEmp.Text), "0")
        txtTotWages.Text = VB6.Format(Val(txtOnRollWages.Text) + Val(txtOtherWages.Text), "0")
        txtTotalEmpCont.Text = VB6.Format(Val(txtOnRollEmpCont.Text) + Val(txtOtherEmpCont.Text), "0")
        txtTotalEmperCont.Text = VB6.Format(Val(txtOnRollEmperCont.Text) + Val(txtOtherEmperCont.Text), "0")
        txtTotalContribution.Text = VB6.Format(Val(txtTotalEmpCont.Text) + Val(txtTotalEmperCont.Text), "0")
        txtTotalAmount.Text = VB6.Format(txtTotalContribution.Text, "0.00")

        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Function MaxRefNo() As Integer

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = "SELECT MAX(REFNO) AS REFNO FROM PAY_ESICCHALLAN_TRN " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = True Then
            MaxRefNo = 1
        Else
            MaxRefNo = IIf(IsDbNull(RsTemp.Fields("REFNO").Value), 1, RsTemp.Fields("REFNO").Value + 1)
        End If

        Exit Function
ErrPart:
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Function
    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        On Error GoTo ERR1
        SqlStr = ""

        SqlStr = " Select TO_CHAR(REFNO,'00000') AS REFNO,TO_CHAR(REFDATE,'DD/MM/YYYY') AS REFDATE, " & vbCrLf & " PAID_BY, TO_CHAR(PAYMENT_DATE,'DD/MM/YYYY') AS PAYMENT_DATE," & vbCrLf & " BANK_NAME, CHEQUE_NO, " & vbCrLf & " TO_CHAR(CHEQUE_DATE,'DD/MM/YYYY') AS CHEQUE_DATE, " & vbCrLf & " TO_CHAR(TOTAL_AMOUNT) As TOTAL_AMOUNT " & vbCrLf & " FROM PAY_ESICCHALLAN_TRN" & vbCrLf & " WHERE " & vbCrLf & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " ORDER BY REFNO"

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 10)
            .set_ColWidth(0, 0)
            .set_ColWidth(1, 20)
            .set_ColWidth(2, 10)
            .set_ColWidth(3, 10)
            .set_ColWidth(4, 10)
            .set_ColWidth(5, 10)
            .set_ColWidth(6, 10)
            .set_ColWidth(7, 10)
            .ColsFrozen = 1
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub

    Private Sub txtTotalAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTotalAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub CalcESIDetail()

        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mSubscriber As Double
        Dim mESIWages As Double
        Dim mESIAmount As Double


        mSubscriber = GetSubscriber()

        txtOnRollEmp.Text = VB6.Format(mSubscriber, "0")

        SqlStr = " SELECT SUM(ESIABLEAMT) AS ESIWAGES, " & vbCrLf & " SUM(ESIAMT) AS ESIAMOUNT " & vbCrLf & " FROM PAY_PFESI_TRN PFESI " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TO_CHAR(SAL_DATE,'MON-YYYY')='" & UCase(VB6.Format(txtRefDate.Text, "MMM-YYYY")) & "'"

        SqlStr = SqlStr & vbCrLf & "AND ISARREAR='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mESIAmount = System.Math.Round(IIf(IsDbNull(RsTemp.Fields("ESIAMOUNT").Value), 0, RsTemp.Fields("ESIAMOUNT").Value), 0)
            mESIWages = System.Math.Round(IIf(IsDbNull(RsTemp.Fields("ESIWAGES").Value), 0, RsTemp.Fields("ESIWAGES").Value), 0)
        Else
            mESIAmount = CDbl("0.00")
            mESIWages = CDbl("0.00")
        End If
        txtOnRollWages.Text = VB6.Format(mESIWages, "0.00")
        txtOnRollEmpCont.Text = VB6.Format(mESIAmount, "0.00")
        txtOnRollEmperCont.Text = VB6.Format(mESIAmount, "0.00")

        Call CalcAmount()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        '    Resume
    End Sub
    Private Function GetSubscriber() As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing


        SqlStr = " SELECT  COUNT(1) AS CNT " & vbCrLf & " FROM PAY_PFESI_TRN PFESI " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TO_CHAR(SAL_DATE,'MON-YYYY')='" & UCase(VB6.Format(txtRefDate.Text, "MMM-YYYY")) & "'"

        SqlStr = SqlStr & vbCrLf & "AND ISARREAR='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetSubscriber = IIf(IsDbNull(RsTemp.Fields("CNT").Value), 0, RsTemp.Fields("CNT").Value)
        Else
            GetSubscriber = 0
        End If
        Exit Function
ErrPart:
        MsgBox(Err.Description)
    End Function

    Private Sub txtTotalContribution_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTotalContribution.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTotalContribution_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTotalContribution.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTotalEmpCont_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTotalEmpCont.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTotalEmpCont_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTotalEmpCont.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTotalEmperCont_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTotalEmperCont.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTotalEmperCont_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTotalEmperCont.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTotEmp_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTotEmp.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTotEmp_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTotEmp.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTotWages_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTotWages.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTotWages_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTotWages.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
End Class
