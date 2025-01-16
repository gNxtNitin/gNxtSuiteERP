Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmPFChallan
    Inherits System.Windows.Forms.Form
    Dim RsChallanMain As ADODB.Recordset
    Dim RsChallanDetail As ADODB.Recordset
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection
    Dim Shw As Boolean
    Dim FormActive As Boolean
    Dim xRefNo As Integer
    Dim SqlStr As String = ""
    Private Const ColDesc As Short = 1
    Private Const ColAC1 As Short = 2
    Private Const ColAC2 As Short = 3
    Private Const ColAC10 As Short = 4
    Private Const ColAC21 As Short = 5
    Private Const ColAC22 As Short = 6
    Private Const ColTotal As Short = 7


    Private Const RowHeight As Short = 12
    Private Sub settextlength()
        On Error GoTo ERR1

        txtPaymentDate.Maxlength = 10
        txtEmperDueDate.Maxlength = 10
        txtEmpDueDate.Maxlength = 10
        txtAccountGroupCode.Maxlength = RsChallanMain.Fields("GROUP_CODE").DefinedSize
        txtEstableCode.Maxlength = RsChallanMain.Fields("PF_ESTAB").DefinedSize
        txtRefDate.Maxlength = 10
        txtRefNo.Maxlength = RsChallanMain.Fields("REFNO").Precision
        txtTotWages_AC21.Maxlength = RsChallanMain.Fields("WAGES_DUE_AC_21").Precision
        txtTotWages_AC1.Maxlength = RsChallanMain.Fields("WAGES_DUE_AC_1").Precision
        txtTotWages_AC10.Maxlength = RsChallanMain.Fields("WAGES_DUE_AC_10").Precision
        txtTotSubs_AC21.Maxlength = RsChallanMain.Fields("SUBS_AC_21").Precision
        txtTotSubs_AC1.Maxlength = RsChallanMain.Fields("SUBS_AC_1").Precision
        txtTotSubs_AC10.Maxlength = RsChallanMain.Fields("SUBS_AC_10").Precision
        txtTotalAmount.Maxlength = RsChallanMain.Fields("TOTAL_AMOUNT").Precision
        txtDepositor.Maxlength = RsChallanMain.Fields("DEPOSITOR_NAME").DefinedSize
        txtChqNo.Maxlength = RsChallanMain.Fields("CHEQUE_NO").DefinedSize
        txtBankName.Maxlength = RsChallanMain.Fields("BANK_NAME").DefinedSize
        txtChqDate.Maxlength = 10

        txtTotWages_AC21B.Maxlength = RsChallanMain.Fields("WAGES_DUE_AC_21_B").Precision
        txtTotWages_AC1B.Maxlength = RsChallanMain.Fields("WAGES_DUE_AC_1_B").Precision
        txtTotWages_AC10B.Maxlength = RsChallanMain.Fields("WAGES_DUE_AC_10_B").Precision

        txtTotWages_AC21C.Maxlength = RsChallanMain.Fields("WAGES_DUE_AC_21_C").Precision
        txtTotWages_AC1C.Maxlength = RsChallanMain.Fields("WAGES_DUE_AC_1_C").Precision
        txtTotWages_AC10C.Maxlength = RsChallanMain.Fields("WAGES_DUE_AC_10_C").Precision

        txtTotSubs_AC21B.Maxlength = RsChallanMain.Fields("SUBS_AC_21_B").Precision
        txtTotSubs_AC1B.Maxlength = RsChallanMain.Fields("SUBS_AC_1_B").Precision
        txtTotSubs_AC10B.Maxlength = RsChallanMain.Fields("SUBS_AC_10_B").Precision

        txtTotSubs_AC21C.Maxlength = RsChallanMain.Fields("SUBS_AC_21_C").Precision
        txtTotSubs_AC1C.Maxlength = RsChallanMain.Fields("SUBS_AC_1_C").Precision
        txtTotSubs_AC10C.Maxlength = RsChallanMain.Fields("SUBS_AC_10_C").Precision

        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
    Private Sub Clear1()


        lblMKey.Text = ""
        cboPaidBy.SelectedIndex = -1
        txtPaymentDate.Text = ""
        txtEmperDueDate.Text = ""
        txtEmpDueDate.Text = ""
        txtAccountGroupCode.Text = IIf(IsDbNull(RsCompany.Fields("PFEST").Value), "", RsCompany.Fields("PFEST").Value)
        txtAccountGroupCode.Enabled = False
        txtEstableCode.Text = IIf(IsDbNull(RsCompany.Fields("PFEST").Value), "", RsCompany.Fields("PFEST").Value)
        txtEstableCode.Enabled = False

        txtRefDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtRefNo.Text = ""
        txtTotWages_AC21.Text = "0.00"
        txtTotWages_AC1.Text = "0.00"
        txtTotWages_AC10.Text = "0.00"
        txtTotSubs_AC21.Text = "0"
        txtTotSubs_AC1.Text = "0"
        txtTotSubs_AC10.Text = "0"
        txtTotalAmount.Text = "0.00"
        txtDepositor.Text = ""
        txtChqNo.Text = ""
        txtBankName.Text = ""
        txtChqDate.Text = ""

        txtTotSubs_AC21B.Text = "0"
        txtTotSubs_AC1B.Text = "0"
        txtTotSubs_AC10B.Text = "0"

        txtTotSubs_AC21C.Text = "0"
        txtTotSubs_AC1C.Text = "0"
        txtTotSubs_AC10C.Text = "0"

        txtTotWages_AC21B.Text = "0.00"
        txtTotWages_AC1B.Text = "0.00"
        txtTotWages_AC10B.Text = "0.00"

        txtTotWages_AC21C.Text = "0.00"
        txtTotWages_AC1C.Text = "0.00"
        txtTotWages_AC10C.Text = "0.00"

        txtRefNo.Enabled = True

        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain()
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
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
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

        If InsertIntoDelAudit(PubDBCn, "PAY_PFCHALLAN_HDR", (lblMKey.Text), RsChallanMain) = False Then GoTo DeleteErr
        If InsertIntoDeleteTrn(PubDBCn, "PAY_PFCHALLAN_HDR", "MKEY", (lblMKey.Text)) = False Then GoTo DeleteErr

        SqlStr = "Delete from PAY_PFCHALLAN_DET where MKey='" & lblMKey.Text & "' "
        PubDBCn.Execute(SqlStr)

        SqlStr = "Delete from PAY_PFCHALLAN_HDR where MKey='" & lblMKey.Text & "' "
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

    Private Sub FormatSprdMain()

        Dim cntCol As Integer
        With SprdMain
            .MaxCols = ColTotal
            .MaxRows = 7
            .set_RowHeight(0, RowHeight * 2)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeMaxEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColDesc, 30)

            For cntCol = ColAC1 To ColTotal
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.99")
                .TypeFloatMin = CDbl("-999999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
                .set_ColWidth(cntCol, 9)
            Next

            .ColsFrozen = ColDesc

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, 7, ColDesc, ColDesc)
            MainClass.ProtectCell(SprdMain, 1, 7, ColTotal, ColTotal)
            MainClass.ProtectCell(SprdMain, 7, 7, ColDesc, ColTotal)
            MainClass.ProtectCell(SprdMain, 1, 1, ColDesc, ColTotal)
            MainClass.ProtectCell(SprdMain, 2, 2, ColDesc, ColTotal)
            MainClass.ProtectCell(SprdMain, 3, 3, ColDesc, ColTotal)

            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)

            Call FillHeading()

        End With
    End Sub

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

        mSubTitle = "EMPLOYEES PROVIDENT FUND ORGANISATION"
        mTitle = "(USE SEPARATE CHALLAN FOR EACH MONTH)"

        Call ShowReport(SqlStr, "PFChallan.Rpt", Mode, mTitle, mSubTitle)

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


        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub
    Private Function MakeSQL() As String
        On Error GoTo refreshErrPart


        MakeSQL = " Select IH.*, ID.*  " & vbCrLf & " FROM PAY_PFCHALLAN_HDR IH, PAY_PFCHALLAN_DET ID " & vbCrLf & " WHERE " & vbCrLf & " IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY='" & MainClass.AllowSingleQuote((lblMKey.Text)) & "' "

        MakeSQL = MakeSQL & vbCrLf & "Order by ID.ROWNO"

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
    Private Sub frmPFChallan_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        If eventArgs.row = 0 Then Exit Sub
        SprdMain.Row = eventArgs.row
        Call CalcAmount()
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell
        Call CalcAmount()
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
        txtRefNo.Text = Trim(SprdView.Text)

        txtRefNo_Validating(txtRefNo, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())

    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdMain.ActiveCol, SprdMain.ActiveRow))
    End Sub

    Private Sub frmPFChallan_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        MainClass.UOpenRecordSet("Select * From PAY_PFCHALLAN_HDR Where 1<>1", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsChallanMain, ADODB.LockTypeEnum.adLockReadOnly)
        MainClass.UOpenRecordSet("Select * From PAY_PFCHALLAN_DET Where 1<>1", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsChallanDetail, ADODB.LockTypeEnum.adLockReadOnly)

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
    Private Sub frmPFChallan_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)
        FormatSprdMain()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmPFChallan_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        RsChallanMain = Nothing
        RsChallanDetail = Nothing
        ''Me = Nothing
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
                lblMKey.Text = RsChallanMain.Fields("mKey").Value
                xRefNo = RsChallanMain.Fields("REFNO").Value
                txtRefNo.Text = VB6.Format(IIf(IsDbNull(.Fields("REFNO").Value), "", .Fields("REFNO").Value), "00000")
                txtRefDate.Text = VB6.Format(IIf(IsDbNull(.Fields("REFDATE").Value), "", .Fields("REFDATE").Value), "DD/MM/YYYY")
                txtEstableCode.Text = IIf(IsDbNull(.Fields("PF_ESTAB").Value), "", .Fields("PF_ESTAB").Value)
                txtAccountGroupCode.Text = IIf(IsDbNull(.Fields("GROUP_CODE").Value), "", .Fields("GROUP_CODE").Value)
                txtPaymentDate.Text = VB6.Format(IIf(IsDbNull(.Fields("PAYMENT_DATE").Value), "", .Fields("PAYMENT_DATE").Value), "DD/MM/YYYY")
                txtEmpDueDate.Text = VB6.Format(IIf(IsDbNull(.Fields("DUE_DATE_EMP").Value), "", .Fields("DUE_DATE_EMP").Value), "DD/MM/YYYY")
                txtEmperDueDate.Text = VB6.Format(IIf(IsDbNull(.Fields("DUE_DATE_EMPER").Value), "", .Fields("DUE_DATE_EMPER").Value), "DD/MM/YYYY")

                txtTotSubs_AC1.Text = VB6.Format(IIf(IsDbNull(.Fields("SUBS_AC_1").Value), "0", .Fields("SUBS_AC_1").Value), "0")
                txtTotSubs_AC10.Text = VB6.Format(IIf(IsDbNull(.Fields("SUBS_AC_10").Value), "0", .Fields("SUBS_AC_10").Value), "0")
                txtTotSubs_AC21.Text = VB6.Format(IIf(IsDbNull(.Fields("SUBS_AC_21").Value), "0", .Fields("SUBS_AC_21").Value), "0")
                txtTotWages_AC1.Text = VB6.Format(IIf(IsDbNull(.Fields("WAGES_DUE_AC_1").Value), "0", .Fields("WAGES_DUE_AC_1").Value), "0.00")
                txtTotWages_AC10.Text = VB6.Format(IIf(IsDbNull(.Fields("WAGES_DUE_AC_10").Value), "0", .Fields("WAGES_DUE_AC_10").Value), "0.00")
                txtTotWages_AC21.Text = VB6.Format(IIf(IsDbNull(.Fields("WAGES_DUE_AC_21").Value), "0", .Fields("WAGES_DUE_AC_21").Value), "0.00")
                txtTotalAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("TOTAL_AMOUNT").Value), "0", .Fields("TOTAL_AMOUNT").Value), "0.00")

                txtTotWages_AC1B.Text = VB6.Format(IIf(IsDbNull(.Fields("WAGES_DUE_AC_1_B").Value), "0", .Fields("WAGES_DUE_AC_1_B").Value), "0.00")
                txtTotWages_AC10B.Text = VB6.Format(IIf(IsDbNull(.Fields("WAGES_DUE_AC_10_B").Value), "0", .Fields("WAGES_DUE_AC_10_B").Value), "0.00")
                txtTotWages_AC21B.Text = VB6.Format(IIf(IsDbNull(.Fields("WAGES_DUE_AC_21_B").Value), "0", .Fields("WAGES_DUE_AC_21_B").Value), "0.00")

                txtTotWages_AC1C.Text = VB6.Format(IIf(IsDbNull(.Fields("WAGES_DUE_AC_1_C").Value), "0", .Fields("WAGES_DUE_AC_1_C").Value), "0.00")
                txtTotWages_AC10C.Text = VB6.Format(IIf(IsDbNull(.Fields("WAGES_DUE_AC_10_C").Value), "0", .Fields("WAGES_DUE_AC_10_C").Value), "0.00")
                txtTotWages_AC21C.Text = VB6.Format(IIf(IsDbNull(.Fields("WAGES_DUE_AC_21_C").Value), "0", .Fields("WAGES_DUE_AC_21_C").Value), "0.00")

                txtTotSubs_AC1B.Text = VB6.Format(IIf(IsDbNull(.Fields("SUBS_AC_1_B").Value), "0", .Fields("SUBS_AC_1_B").Value), "0")
                txtTotSubs_AC10B.Text = VB6.Format(IIf(IsDbNull(.Fields("SUBS_AC_10_B").Value), "0", .Fields("SUBS_AC_10_B").Value), "0")
                txtTotSubs_AC21B.Text = VB6.Format(IIf(IsDbNull(.Fields("SUBS_AC_21_B").Value), "0", .Fields("SUBS_AC_21_B").Value), "0")

                txtTotSubs_AC1C.Text = VB6.Format(IIf(IsDbNull(.Fields("SUBS_AC_1_C").Value), "0", .Fields("SUBS_AC_1_C").Value), "0")
                txtTotSubs_AC10C.Text = VB6.Format(IIf(IsDbNull(.Fields("SUBS_AC_10_C").Value), "0", .Fields("SUBS_AC_10_C").Value), "0")
                txtTotSubs_AC21C.Text = VB6.Format(IIf(IsDbNull(.Fields("SUBS_AC_21_C").Value), "0", .Fields("SUBS_AC_21_C").Value), "0")

                txtDepositor.Text = IIf(IsDbNull(.Fields("DEPOSITOR_NAME").Value), "", .Fields("DEPOSITOR_NAME").Value)
                txtBankName.Text = IIf(IsDbNull(.Fields("BANK_NAME").Value), "", .Fields("BANK_NAME").Value)
                txtChqNo.Text = IIf(IsDbNull(.Fields("CHEQUE_NO").Value), "", .Fields("CHEQUE_NO").Value)
                txtChqDate.Text = VB6.Format(IIf(IsDbNull(.Fields("CHEQUE_DATE").Value), "", .Fields("CHEQUE_DATE").Value), "DD/MM/YYYY")
                cboPaidBy.Text = IIf(IsDbNull(.Fields("PAID_BY").Value), "", .Fields("PAID_BY").Value)

                Call ShowDetail1(RsChallanMain.Fields("mKey").Value)
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
    Private Sub ShowDetail1(ByRef pMkey As String)

        On Error GoTo ShowErr
        Dim SqlStr As String = ""

        SqlStr = "SELECT * " & vbCrLf & " FROM PAY_PFCHALLAN_DET " & vbCrLf & " WHERE MKEY= '" & pMkey & "' Order By ROWNO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsChallanDetail, ADODB.LockTypeEnum.adLockReadOnly)

        If RsChallanDetail.EOF = True Then Exit Sub

        Do While RsChallanDetail.EOF = False

            SprdMain.Row = RsChallanDetail.Fields("ROWNO").Value

            SprdMain.Col = ColDesc
            SprdMain.Text = IIf(IsDbNull(RsChallanDetail.Fields("Description").Value), "", RsChallanDetail.Fields("Description").Value)

            SprdMain.Col = ColAC1
            SprdMain.Text = VB6.Format(IIf(IsDbNull(RsChallanDetail.Fields("AC_1").Value), 0, RsChallanDetail.Fields("AC_1").Value), "0.00")

            SprdMain.Col = ColAC2
            SprdMain.Text = VB6.Format(IIf(IsDbNull(RsChallanDetail.Fields("AC_2").Value), 0, RsChallanDetail.Fields("AC_2").Value), "0.00")

            SprdMain.Col = ColAC10
            SprdMain.Text = VB6.Format(IIf(IsDbNull(RsChallanDetail.Fields("AC_10").Value), 0, RsChallanDetail.Fields("AC_10").Value), "0.00")

            SprdMain.Col = ColAC21
            SprdMain.Text = VB6.Format(IIf(IsDbNull(RsChallanDetail.Fields("AC_21").Value), 0, RsChallanDetail.Fields("AC_21").Value), "0.00")

            SprdMain.Col = ColAC22
            SprdMain.Text = VB6.Format(IIf(IsDbNull(RsChallanDetail.Fields("AC_22").Value), 0, RsChallanDetail.Fields("AC_22").Value), "0.00")

            SprdMain.Col = ColTotal
            SprdMain.Text = VB6.Format(IIf(IsDbNull(RsChallanDetail.Fields("TOTAL_AMOUNT").Value), 0, RsChallanDetail.Fields("TOTAL_AMOUNT").Value), "0.00")

            '        SprdMain.MaxRows = SprdMain.MaxRows + 1
            RsChallanDetail.MoveNext()
        Loop
        'FormatSprdMain -1
        Exit Sub
ShowErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
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

            SqlStr = "INSERT INTO PAY_PFCHALLAN_HDR ( " & vbCrLf & " MKEY, COMPANY_CODE, FYEAR, " & vbCrLf & " REFNO, REFDATE, PF_ESTAB, " & vbCrLf & " GROUP_CODE, PAID_BY, DUE_DATE_EMP, " & vbCrLf & " DUE_DATE_EMPER, PAYMENT_DATE, " & vbCrLf & " SUBS_AC_1, SUBS_AC_10, SUBS_AC_21,  " & vbCrLf & " SUBS_AC_1_B, SUBS_AC_10_B, SUBS_AC_21_B,  " & vbCrLf & " SUBS_AC_1_C, SUBS_AC_10_C, SUBS_AC_21_C,  " & vbCrLf & " WAGES_DUE_AC_1, WAGES_DUE_AC_10, WAGES_DUE_AC_21, " & vbCrLf & " WAGES_DUE_AC_1_B, WAGES_DUE_AC_10_B, WAGES_DUE_AC_21_B, " & vbCrLf & " WAGES_DUE_AC_1_C, WAGES_DUE_AC_10_C, WAGES_DUE_AC_21_C, " & vbCrLf & " TOTAL_AMOUNT, " & vbCrLf & " DEPOSITOR_NAME, BANK_NAME, BANK_CODE, " & vbCrLf & " CHEQUE_NO, CHEQUE_DATE, CHEQUE_PRESENT, " & vbCrLf & " CHEQUE_REALISATION,  " & vbCrLf & " ADDUSER, ADDDATE, MODUSER, MODDATE) VALUES ( "

            SqlStr = SqlStr & vbCrLf & " '" & pMkey & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf & " " & mRefNo & ", TO_DATE('" & VB6.Format(txtRefDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote((txtEstableCode.Text)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtAccountGroupCode.Text)) & "',  '" & cboPaidBy.Text & "', TO_DATE('" & VB6.Format(txtEmpDueDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " TO_DATE('" & VB6.Format(txtEmperDueDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), TO_DATE('" & VB6.Format(txtPaymentDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " " & Val(txtTotSubs_AC1.Text) & ", " & Val(txtTotSubs_AC10.Text) & ", " & Val(txtTotSubs_AC21.Text) & "," & vbCrLf & " " & Val(txtTotSubs_AC1B.Text) & ", " & Val(txtTotSubs_AC10B.Text) & ", " & Val(txtTotSubs_AC21B.Text) & "," & vbCrLf & " " & Val(txtTotSubs_AC1C.Text) & ", " & Val(txtTotSubs_AC10C.Text) & ", " & Val(txtTotSubs_AC21C.Text) & "," & vbCrLf & " " & Val(txtTotWages_AC1.Text) & ", " & Val(txtTotWages_AC10.Text) & ", " & Val(txtTotWages_AC21.Text) & ", " & vbCrLf & " " & Val(txtTotWages_AC1B.Text) & ", " & Val(txtTotWages_AC10B.Text) & ", " & Val(txtTotWages_AC21B.Text) & ", " & vbCrLf & " " & Val(txtTotWages_AC1C.Text) & ", " & Val(txtTotWages_AC10C.Text) & ", " & Val(txtTotWages_AC21C.Text) & ", " & vbCrLf & " " & Val(txtTotalAmount.Text) & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtDepositor.Text)) & "', '" & MainClass.AllowSingleQuote((txtBankName.Text)) & "', '', " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtChqNo.Text)) & "', TO_DATE('" & VB6.Format(txtChqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '', " & vbCrLf & " '', " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"

        Else
            SqlStr = "UPDATE PAY_PFCHALLAN_HDR SET " & vbCrLf & " REFNO=" & Val(txtRefNo.Text) & "," & vbCrLf & " REFDATE=TO_DATE('" & VB6.Format(txtRefDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " PF_ESTAB='" & MainClass.AllowSingleQuote((txtEstableCode.Text)) & "'," & vbCrLf & " GROUP_CODE='" & MainClass.AllowSingleQuote((txtAccountGroupCode.Text)) & "'," & vbCrLf & " PAID_BY='" & cboPaidBy.Text & "'," & vbCrLf & " DUE_DATE_EMP=TO_DATE('" & VB6.Format(txtEmpDueDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " DUE_DATE_EMPER=TO_DATE('" & VB6.Format(txtEmperDueDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " PAYMENT_DATE=TO_DATE('" & VB6.Format(txtPaymentDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),"

            SqlStr = SqlStr & vbCrLf & " SUBS_AC_1=" & Val(txtTotSubs_AC1.Text) & "," & vbCrLf & " SUBS_AC_10=" & Val(txtTotSubs_AC10.Text) & "," & vbCrLf & " SUBS_AC_21=" & Val(txtTotSubs_AC21.Text) & "," & vbCrLf & " SUBS_AC_1_B=" & Val(txtTotSubs_AC1B.Text) & "," & vbCrLf & " SUBS_AC_10_B=" & Val(txtTotSubs_AC10B.Text) & "," & vbCrLf & " SUBS_AC_21_B=" & Val(txtTotSubs_AC21B.Text) & "," & vbCrLf & " SUBS_AC_1_C=" & Val(txtTotSubs_AC1C.Text) & "," & vbCrLf & " SUBS_AC_10_C=" & Val(txtTotSubs_AC10C.Text) & "," & vbCrLf & " SUBS_AC_21_C=" & Val(txtTotSubs_AC21C.Text) & ","

            SqlStr = SqlStr & vbCrLf & " WAGES_DUE_AC_1=" & Val(txtTotWages_AC1.Text) & "," & vbCrLf & " WAGES_DUE_AC_10=" & Val(txtTotWages_AC10.Text) & "," & vbCrLf & " WAGES_DUE_AC_21=" & Val(txtTotWages_AC21.Text) & "," & vbCrLf & " WAGES_DUE_AC_1_B=" & Val(txtTotWages_AC1B.Text) & "," & vbCrLf & " WAGES_DUE_AC_10_B=" & Val(txtTotWages_AC10B.Text) & "," & vbCrLf & " WAGES_DUE_AC_21_B=" & Val(txtTotWages_AC21B.Text) & "," & vbCrLf & " WAGES_DUE_AC_1_C=" & Val(txtTotWages_AC1C.Text) & "," & vbCrLf & " WAGES_DUE_AC_10_C=" & Val(txtTotWages_AC10C.Text) & "," & vbCrLf & " WAGES_DUE_AC_21_C=" & Val(txtTotWages_AC21C.Text) & ","


            SqlStr = SqlStr & vbCrLf & " TOTAL_AMOUNT=" & Val(txtTotalAmount.Text) & "," & vbCrLf & " DEPOSITOR_NAME='" & MainClass.AllowSingleQuote((txtDepositor.Text)) & "'," & vbCrLf & " BANK_NAME='" & MainClass.AllowSingleQuote((txtBankName.Text)) & "'," & vbCrLf & " BANK_CODE=''," & vbCrLf & " CHEQUE_NO='" & MainClass.AllowSingleQuote((txtChqNo.Text)) & "'," & vbCrLf & " CHEQUE_DATE=TO_DATE('" & VB6.Format(txtChqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " CHEQUE_PRESENT=''," & vbCrLf & " CHEQUE_REALISATION='',"

            SqlStr = SqlStr & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " WHERE MKEY=" & lblMKey.Text & ""

            pMkey = lblMKey.Text
        End If

UpdatePart:
        PubDBCn.Execute(SqlStr)

        If UpdateDetail(pMkey) = False Then GoTo UpdateError

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
    Private Function UpdateDetail(ByRef mMKEY As String) As Boolean


        On Error GoTo ErrDetail

        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mDesc As String
        Dim mAC1 As Double
        Dim mAC2 As Double
        Dim mAC10 As Double
        Dim mAC21 As Double
        Dim mAC22 As Double
        Dim mTotal As Double

        SqlStr = "Delete From PAY_PFCHALLAN_DET Where Mkey='" & mMKEY & "'"
        PubDBCn.Execute(SqlStr)

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColDesc
                mDesc = .Text

                .Col = ColAC1
                mAC1 = Val(.Text)

                .Col = ColAC2
                mAC2 = Val(.Text)

                .Col = ColAC10
                mAC10 = Val(.Text)

                .Col = ColAC21
                mAC21 = Val(.Text)

                .Col = ColAC22
                mAC22 = Val(.Text)

                .Col = ColTotal
                mTotal = Val(.Text)

                If mDesc <> "" Then
                    SqlStr = "INSERT INTO PAY_PFCHALLAN_DET ( " & vbCrLf & " MKEY, ROWNO, DESCRIPTION,  " & vbCrLf & " AC_1, AC_2, AC_10, " & vbCrLf & " AC_21, AC_22, TOTAL_AMOUNT )" & vbCrLf & " VALUES ( " & vbCrLf & " '" & mMKEY & "', " & I & ", '" & MainClass.AllowSingleQuote(mDesc) & "', " & vbCrLf & " " & mAC1 & ", " & mAC2 & ", " & mAC10 & "," & vbCrLf & " " & mAC21 & ", " & mAC22 & ", " & mTotal & ")"

                    PubDBCn.Execute(SqlStr)
                End If
            Next I

        End With
        UpdateDetail = True
        Exit Function
ErrDetail:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateDetail = False
        'Resume
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

    Private Sub txtAccountGroupCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAccountGroupCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAccountGroupCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAccountGroupCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtAccountGroupCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
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

    Private Sub txtEmpDueDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpDueDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtEmpDueDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmpDueDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtEmpDueDate.Text) = "" Then GoTo EventExitSub

        If Not IsDate(txtEmpDueDate.Text) Then
            MsgBox("Invalid Due Date for Employees Share", MsgBoxStyle.Information)
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtEmperDueDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmperDueDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtEmperDueDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmperDueDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtEmperDueDate.Text) = "" Then GoTo EventExitSub

        If Not IsDate(txtEmperDueDate.Text) Then
            MsgBox("Invalid Due Date for Employer Share", MsgBoxStyle.Information)
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
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

        If Trim(txtEmpDueDate.Text) = "" Then
            txtEmpDueDate.Text = VB6.Format(txtRefDate.Text, "DD/MM/YYYY")
        End If

        If Trim(txtEmperDueDate.Text) = "" Then
            txtEmperDueDate.Text = VB6.Format(txtRefDate.Text, "DD/MM/YYYY")
        End If
        Call CalcPFDetail()
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
        SqlStr = "Select * from  PAY_PFCHALLAN_HDR Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " ANd RefNo=" & txtRefNo.Text & ""
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
                MainClass.UOpenRecordSet("Select * From PAY_PFCHALLAN_HDR Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND RefNo=" & xRefNo & "", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsChallanMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If

        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub FillHeading()


        With SprdMain
            .Row = 0
            .Col = ColDesc
            .Text = "Particulars"

            .Col = ColAC1
            .Text = "A/c No. 1"

            .Col = ColAC2
            .Text = "A/c No. 2"

            .Col = ColAC10
            .Text = "A/c No. 10"

            .Col = ColAC21
            .Text = "A/c No. 21"

            .Col = ColAC22
            .Text = "A/c No. 22"

            .Col = ColTotal
            .Text = "Total"

            .Col = ColDesc

            .Row = 1
            .Text = "Employer's Share of Contribution"

            .Row = 2
            .Text = "Employee's Share of Contribution"

            .Row = 3
            .Text = "Adm. Charges"

            .Row = 4
            .Text = "Insp. Charges"

            .Row = 5
            .Text = "Penal Damages"

            .Row = 6
            .Text = "Misc. Payment (Past Accumulations Only)"

            .Row = 7
            .Text = "Total"
        End With
    End Sub

    Private Sub CalcAmount()
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mColAmount As Double
        Dim mRowAmount As Double
        Dim mGrandAmount As Double

        txtTotSubs_AC1C.Text = VB6.Format(Val(txtTotSubs_AC1.Text) + Val(txtTotSubs_AC1B.Text), "0")
        txtTotSubs_AC10C.Text = VB6.Format(Val(txtTotSubs_AC10.Text) + Val(txtTotSubs_AC10B.Text), "0")
        txtTotSubs_AC21C.Text = VB6.Format(Val(txtTotSubs_AC21.Text) + Val(txtTotSubs_AC21B.Text), "0")
        txtTotWages_AC1C.Text = VB6.Format(Val(txtTotWages_AC1.Text) + Val(txtTotWages_AC1B.Text), "0")
        txtTotWages_AC10C.Text = VB6.Format(Val(txtTotWages_AC10.Text) + Val(txtTotWages_AC10B.Text), "0")
        txtTotWages_AC21C.Text = VB6.Format(Val(txtTotWages_AC21.Text) + Val(txtTotWages_AC21B.Text), "0")

        mRowAmount = 0
        mGrandAmount = 0

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                mColAmount = 0

                For cntCol = ColAC1 To ColAC22
                    .Col = cntCol
                    mColAmount = mColAmount + Val(.Text)
                Next

                .Row = cntRow
                .Col = ColTotal
                .Text = VB6.Format(mColAmount, "0.00")
            Next
        End With

        With SprdMain
            For cntCol = ColAC1 To ColTotal
                .Col = cntCol
                mRowAmount = 0
                mGrandAmount = 0

                For cntRow = 1 To 6
                    .Row = cntRow
                    mRowAmount = mRowAmount + Val(.Text)
                    If cntCol = ColTotal Then
                        mGrandAmount = mGrandAmount + Val(.Text)
                    End If
                Next

                .Row = .MaxRows
                .Col = cntCol
                .Text = VB6.Format(mRowAmount, "0.00")
            Next
        End With


        txtTotalAmount.Text = VB6.Format(mGrandAmount, "0.00")

        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Function MaxRefNo() As Integer

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = "SELECT MAX(REFNO) AS REFNO FROM PAY_PFCHALLAN_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

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

        SqlStr = " Select TO_CHAR(REFNO,'00000') AS REFNO,TO_CHAR(REFDATE,'DD/MM/YYYY') AS REFDATE, " & vbCrLf & " PAID_BY, TO_CHAR(PAYMENT_DATE,'DD/MM/YYYY') AS PAYMENT_DATE," & vbCrLf & " BANK_NAME, CHEQUE_NO, " & vbCrLf & " TO_CHAR(CHEQUE_DATE,'DD/MM/YYYY') AS CHEQUE_DATE, " & vbCrLf & " TO_CHAR(TOTAL_AMOUNT) As TOTAL_AMOUNT " & vbCrLf & " FROM PAY_PFCHALLAN_HDR" & vbCrLf & " WHERE " & vbCrLf & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " ORDER BY REFNO"

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

    Private Sub txtTotSubs_AC1_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTotSubs_AC1.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTotSubs_AC1_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTotSubs_AC1.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTotSubs_AC10_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTotSubs_AC10.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTotSubs_AC10_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTotSubs_AC10.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTotSubs_AC10B_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTotSubs_AC10B.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTotSubs_AC10B_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTotSubs_AC10B.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTotSubs_AC10B_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTotSubs_AC10B.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcAmount()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtTotSubs_AC10C_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTotSubs_AC10C.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTotSubs_AC10C_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTotSubs_AC10C.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTotSubs_AC1B_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTotSubs_AC1B.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTotSubs_AC1B_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTotSubs_AC1B.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTotSubs_AC1B_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTotSubs_AC1B.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcAmount()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtTotSubs_AC21_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTotSubs_AC21.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTotSubs_AC21_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTotSubs_AC21.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTotSubs_AC21B_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTotSubs_AC21B.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTotSubs_AC21B_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTotSubs_AC21B.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTotSubs_AC21B_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTotSubs_AC21B.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcAmount()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtTotSubs_AC21C_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTotSubs_AC21C.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTotSubs_AC21C_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTotSubs_AC21C.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTotSubs_AC1C_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTotSubs_AC1C.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTotSubs_AC1C_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTotSubs_AC1C.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTotWages_AC1_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTotWages_AC1.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTotWages_AC1_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTotWages_AC1.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTotWages_AC10_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTotWages_AC10.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTotWages_AC10_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTotWages_AC10.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTotWages_AC10B_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTotWages_AC10B.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTotWages_AC10B_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTotWages_AC10B.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTotWages_AC10B_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTotWages_AC10B.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcAmount()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtTotWages_AC10C_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTotWages_AC10C.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTotWages_AC10C_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTotWages_AC10C.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTotWages_AC1B_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTotWages_AC1B.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTotWages_AC1B_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTotWages_AC1B.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTotWages_AC1B_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTotWages_AC1B.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcAmount()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtTotWages_AC1C_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTotWages_AC1C.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTotWages_AC1C_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTotWages_AC1C.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTotWages_AC21_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTotWages_AC21.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTotWages_AC21_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTotWages_AC21.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTotWages_AC21B_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTotWages_AC21B.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTotWages_AC21B_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTotWages_AC21B.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTotWages_AC21B_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTotWages_AC21B.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcAmount()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtTotWages_AC21C_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTotWages_AC21C.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTotWages_AC21C_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTotWages_AC21C.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub CalcPFDetail()

        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mSubscriber As Double
        Dim mTotalBasic As Double
        Dim mWages8_33 As Double
        Dim mEmplor3_67 As Double
        Dim mEmplor8_33 As Double
        Dim mEmplor_AC21 As Double
        Dim mEmpAC_1 As Double
        Dim mAdmin_AC2 As Double
        Dim mAdmin_AC22 As Double
        Dim mEmpBasic As Double
        Dim mEmpPension As Double

        mSubscriber = GetSubscriber()

        txtTotSubs_AC1.Text = VB6.Format(mSubscriber, "0")
        txtTotSubs_AC10.Text = VB6.Format(mSubscriber, "0")
        txtTotSubs_AC21.Text = VB6.Format(mSubscriber, "0")

        '    MakeSQL = " SELECT  EMP.EMP_CODE, EMP.EMP_NAME, " & vbCrLf _
        ''            & " EMP.EMP_DESG_CODE, EMP.EMP_PF_ACNO, TO_CHAR(PFESI.WDAYS,'999.9'), " & vbCrLf _
        ''            & " PENSIONWAGES, PFESI.PFABLEAMT, PFAMT,PENSIONFUND,PFESI.EPFAMT, " & vbCrLf _
        ''            & " TO_CHAR((NVl(PFESI.PENSIONFUND,0)+NVL(PFESI.EPFAMT,0))) AS A, " & vbCrLf _
        ''            & " TO_CHAR(NVl(PFESI.VPFAMT,0)) AS B, " & vbCrLf _
        ''            & " TO_CHAR(NVL(PFESI.PFAMT,0)+NVL(PFESI.EPFAMT,0)) AS C" & vbCrLf _
        ''
        SqlStr = " SELECT  EMP_CODE, " & vbCrLf & " SUM(PENSIONWAGES) AS PENSIONWAGES , SUM(PFABLEAMT) AS PFABLEAMT, " & vbCrLf & " SUM(PFAMT) AS PFAMT, SUM(PENSIONFUND) AS PENSIONFUND, " & vbCrLf & " SUM(EPFAMT) AS EPFAMT" & vbCrLf & " FROM PAY_PFESI_TRN PFESI " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TO_CHAR(SAL_DATE,'MON-YYYY')='" & UCase(VB6.Format(txtRefDate.Text, "MMM-YYYY")) & "'"

        SqlStr = SqlStr & vbCrLf & " GROUP BY EMP_CODE"

        '    SqlStr = SqlStr & vbCrLf & "AND ISARREAR='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        mTotalBasic = 0
        mWages8_33 = 0
        mEmplor3_67 = 0
        mEmplor8_33 = 0
        mEmplor_AC21 = 0
        mEmpAC_1 = 0
        mAdmin_AC2 = 0
        mAdmin_AC22 = 0

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mTotalBasic = mTotalBasic + System.Math.Round(IIf(IsDbNull(RsTemp.Fields("PFABLEAMT").Value), 0, RsTemp.Fields("PFABLEAMT").Value), 0) ''Total Basic Wages
                mEmpBasic = System.Math.Round(IIf(IsDbNull(RsTemp.Fields("PFABLEAMT").Value), 0, RsTemp.Fields("PFABLEAMT").Value), 0) ''Total Basic Wages
                mWages8_33 = mWages8_33 + System.Math.Round(IIf(IsDbNull(RsTemp.Fields("PENSIONWAGES").Value), 0, RsTemp.Fields("PENSIONWAGES").Value), 0) 'Pension Wages
                mEmpPension = System.Math.Round(IIf(IsDbNull(RsTemp.Fields("PENSIONWAGES").Value), 0, RsTemp.Fields("PENSIONWAGES").Value), 0)
                mEmplor3_67 = mEmplor3_67 + System.Math.Round(IIf(IsDbNull(RsTemp.Fields("EPFAMT").Value), 0, RsTemp.Fields("EPFAMT").Value), 0) ''Round(IIf(IsNull(RsTemp!PENSIONFUND), 0, RsTemp!PENSIONFUND), 0)
                mEmplor8_33 = mEmplor8_33 + System.Math.Round(IIf(IsDbNull(RsTemp.Fields("PENSIONFUND").Value), 0, RsTemp.Fields("PENSIONFUND").Value), 0) ''Round(IIf(IsNull(RsTemp!EPFAMT), 0, RsTemp!EPFAMT), 0)
                mEmplor_AC21 = mEmplor_AC21 + System.Math.Round(mEmpBasic * 0.5 * 0.01, 0)
                mEmpAC_1 = mEmpAC_1 + System.Math.Round(IIf(IsDbNull(RsTemp.Fields("PFAMT").Value), 0, RsTemp.Fields("PFAMT").Value), 0)
                mAdmin_AC2 = mAdmin_AC2 + System.Math.Round(mEmpBasic * IIf(IsDbNull(RsCompany.Fields("PFADMINPER").Value), 0.85, RsCompany.Fields("PFADMINPER").Value) * 0.01, 0) ''01-Jul-2015
                mAdmin_AC22 = mAdmin_AC22 + System.Math.Round(mEmpPension * IIf(IsDbNull(RsCompany.Fields("PFADMINPER_22").Value), 0.01, RsCompany.Fields("PFADMINPER_22").Value) * 0.01, 0)
                '            mAdmin_AC22 = mAdmin_AC22 + Round(IIf(mAdmin_AC22 < 5, 5, mAdmin_AC22), 0)
                RsTemp.MoveNext()
            Loop
        Else
            mTotalBasic = CDbl("0.00")
            mWages8_33 = CDbl("0.00")
            mEmplor3_67 = CDbl("0.00")
            mEmplor8_33 = CDbl("0.00")
            mEmplor_AC21 = CDbl("0.00")
            mEmpAC_1 = CDbl("0.00")
            mAdmin_AC2 = CDbl("0.00")
            mAdmin_AC22 = CDbl("0.00")
        End If
        txtTotWages_AC1.Text = VB6.Format(mTotalBasic, "0.00")
        txtTotWages_AC10.Text = VB6.Format(mWages8_33, "0.00")
        txtTotWages_AC21.Text = VB6.Format(mTotalBasic, "0.00")

        With SprdMain
            .Row = 1
            .Col = 2 'A/c 1
            .Text = VB6.Format(mEmplor3_67, "0.00")

            .Col = 3 'A/c 2
            .Text = "" 'Format(mEmplor8_33, "0.00")

            .Col = 4 'A/c 10
            .Text = VB6.Format(mEmplor8_33, "0.00") '' Format(mEmplor_AC21, "0.00")

            .Row = 2
            .Col = 2
            .Text = VB6.Format(mEmpAC_1, "0.00")

            .Row = 3
            .Col = 3
            .Text = VB6.Format(mAdmin_AC2, "0.00")

            If RsCompany.Fields("COMPANY_CODE").Value = 16 Then
                .Col = 5
                .Text = VB6.Format(System.Math.Round(mWages8_33 * 0.5 * 0.01, 0), "0.00")
            End If

            .Col = 6
            .Text = VB6.Format(mAdmin_AC22, "0.00")

        End With
        Call CalcAmount()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Function GetSubscriber() As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing


        SqlStr = " SELECT  COUNT(DISTINCT EMP_CODE) AS CNT " & vbCrLf & " FROM PAY_PFESI_TRN PFESI " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " AND PFAMT>0" & vbCrLf & " AND TO_CHAR(SAL_DATE,'MON-YYYY')='" & UCase(VB6.Format(txtRefDate.Text, "MMM-YYYY")) & "'"

        '    SqlStr = SqlStr & vbCrLf & "AND ISARREAR='N'"

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
End Class
