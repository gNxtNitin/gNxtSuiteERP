Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmPerksPaymentEntry
    Inherits System.Windows.Forms.Form
    Dim RsPerks As ADODB.Recordset

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection

    Private Const ColCode As Short = 1
    Private Const ColName As Short = 2
    Private Const ColPayment As Short = 3
    Private Const ColClaim As Short = 4

    Dim SqlStr As String = ""
    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12
    Dim xEmpCode As String
    'Private Const mBookType = "P"
    Private Const mDC As String = "D"



    Private Sub cboPaidWeek_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles cboPaidWeek.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call txtEmpCode_Validating(txtEmpCode, New System.ComponentModel.CancelEventArgs(True))
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub cboPaymentType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboPaymentType.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboPaymentType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboPaymentType.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
    End Sub
    Private Sub FormatMain()

        Dim cntCol As Integer
        '    MainClass.ClearGrid sprdHoliday

        Call FillPerksHead()

        With SprdMain
            .MaxCols = ColClaim

            .set_RowHeight(0, ConRowHeight * 1.5)
            .set_RowHeight(-1, ConRowHeight * 1.5)
            .Row = -1

            .Col = ColCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColCode, 6)
            .ColHidden = True

            .Col = ColName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColName, 36)

            For cntCol = ColPayment To ColClaim
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("9999999.99")
                .TypeFloatMin = CDbl("-9999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
                .set_ColWidth(cntCol, 12)
            Next

            MainClass.ProtectCell(SprdMain, 1, .MaxRows, ColCode, ColPayment)
            MainClass.SetSpreadColor(SprdMain, -1)
            '        sprdHoliday.OperationMode = OperationModeSingle
        End With
    End Sub
    Private Sub FillPerksHead()

        Dim cntRow As Integer
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCode As String
        Dim mName As String

        SqlStr = " SELECT CODE, NAME, ADDDEDUCT " & vbCrLf & " FROM PAY_SALARYHEAD_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ADDDEDUCT=" & ConPerks & " AND PAYMENT_TYPE='M'"

        If lblBookType.Text = "P" Then
            SqlStr = SqlStr & vbCrLf & " AND CALC_ON <> " & ConCalcVariable & ""
        Else
            SqlStr = SqlStr & vbCrLf & " AND CALC_ON = " & ConCalcVariable & ""
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY ADDDEDUCT,SEQ "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
        cntRow = 1

        If RsTemp.EOF = False Then
            With SprdMain
                Do While RsTemp.EOF = False
                    .MaxRows = cntRow
                    mCode = IIf(IsDbNull(RsTemp.Fields("CODE").Value), "", RsTemp.Fields("CODE").Value)
                    mName = IIf(IsDbNull(RsTemp.Fields("Name").Value), "", RsTemp.Fields("Name").Value)

                    .Row = cntRow
                    .Col = ColCode
                    .Text = Trim(mCode)

                    .Col = ColName
                    .Text = Trim(mName)
                    RsTemp.MoveNext()
                    cntRow = cntRow + 1
                Loop
            End With
        End If
    End Sub
    Private Sub frmPerksPaymentEntry_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub frmPerksPaymentEntry_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Function Update1() As Boolean
        On Error GoTo UpdateError
        Dim cntRow As Integer
        Dim mAddDedCode As Double
        Dim mDate As String
        Dim mClaimAmount As Double
        Dim mDivisionCode As Double

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        '& " AND TO_CHAR(SAL_DATE,'YYYYMM')='" & VB6.Format(txtRefDate.Text, "YYYYMM") & "'"

        If MainClass.ValidateWithMasterTable(Trim(TxtEmpCode.Text), "EMP_CODE", "DIV_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        mDate = "01/" & VB6.Format(txtRefDate.Text, "MM/YYYY")

        SqlStr = "DELETE FROM PAY_PERKS_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & TxtEmpCode.Text & "'" & vbCrLf & " AND SAL_DATE=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND BOOKTYPE='" & lblBookType.Text & "'" & vbCrLf & " AND PAID_WEEK='" & Val(cboPaidWeek.Text) & "'"

        PubDBCn.Execute(SqlStr)

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColCode
                mAddDedCode = CDbl(Trim(.Text))

                .Col = ColClaim
                mClaimAmount = Val(.Text)

                If mClaimAmount <> 0 Then
                    SqlStr = " INSERT INTO PAY_PERKS_TRN ( " & vbCrLf & " COMPANY_CODE, SAL_DATE, " & vbCrLf & " EMP_CODE, ADD_DEDUCTCODE, AMOUNT,BOOKTYPE,DC,PAYMENT_TYPE,ADDUSER,ADDDATE,PAID_WEEK,DIV_CODE) VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & txtEmpCode.Text & "', " & mAddDedCode & ", " & mClaimAmount & ",'" & lblBookType.Text & "'," & vbCrLf & " '" & mDC & "', '" & VB.Left(cboPaymentType.Text, 1) & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & Val(cboPaidWeek.Text) & "'," & mDivisionCode & ") "

                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        PubDBCn.CommitTrans()
        Update1 = True
        '    Unload Me
        Exit Function
UpdateError:
        PubDBCn.RollbackTrans()
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        PubDBCn.Errors.Clear()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function CheckPerksMade(ByRef xEmpCode As String, ByRef xSalDate As String) As Boolean

        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCheckDate As String
        Dim mLastDate As String
        Dim mSalDate As String
        Dim mWeek As Integer

        CheckPerksMade = False
        '    mCheckDate = MainClass.LastDay(Month(xSalDate), Year(xSalDate)) & "/" & vb6.Format(xSalDate, "MM/YYYY")
        ''AND EMP_CODE='" & MainClass.AllowSingleQuote(xEmpCode) & "'

        mCheckDate = "01/" & VB6.Format(xSalDate, "MM/YYYY")
        mLastDate = MainClass.LastDay(Month(CDate(xSalDate)), Year(CDate(xSalDate))) & "/" & VB6.Format(xSalDate, "MM/YYYY")

        SqlStr = " SELECT DISTINCT SAL_DATE, PAID_WEEK  FROM PAY_PERKS_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SAL_DATE>=TO_DATE('" & VB6.Format(mCheckDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND BOOKTYPE='P'" & vbCrLf & " ORDER BY SAL_DATE, PAID_WEEK"

        ''PAID_WEEK>" & Val(cboPaidWeek.Text) & "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mSalDate = VB6.Format(IIf(IsDbNull(RsTemp.Fields("SAL_DATE").Value), "", RsTemp.Fields("SAL_DATE").Value), "DD/MM/YYYY")
                mWeek = IIf(IsDbNull(RsTemp.Fields("PAID_WEEK").Value), 0, RsTemp.Fields("PAID_WEEK").Value)

                If CDate(mSalDate) > CDate(mLastDate) Then
                    CheckPerksMade = True
                    Exit Function
                Else
                    If mWeek > Val(cboPaidWeek.Text) Then
                        CheckPerksMade = True
                        Exit Function
                    End If
                End If
                RsTemp.MoveNext()
            Loop
        End If

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        Dim mPayableAmount As Double
        Dim mClaimAmount As Double

        If eventArgs.NewRow = -1 Then Exit Sub
        If lblBookType.Text <> "P" Then Exit Sub

        SprdMain.Row = SprdMain.ActiveRow

        Select Case eventArgs.col
            Case ColClaim
                SprdMain.Col = ColPayment
                mPayableAmount = Val(SprdMain.Text)

                SprdMain.Col = ColClaim
                mClaimAmount = Val(SprdMain.Text)

                '            If mClaimAmount < 0 Then
                '                MsgInformation "Claim Amount Cann't be Less Than Zero."
                '                MainClass.SetFocusToCell SprdMain, Row, ColClaim
                '                Cancel = True
                '                Exit Sub
                '            End If

                If mClaimAmount > mPayableAmount And mClaimAmount <> 0 Then
                    MsgInformation("Claim Amount Cann't be Greater Than Payable Amount")
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColClaim)
                    eventArgs.cancel = True
                    Exit Sub
                End If

        End Select
        Call CalcTotals()
        '    MainClass.SetFocusToCell SprdMain, SprdMain.Row + 1, ColFH
    End Sub

    Private Sub ViewGrid()

        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        ADataMain.Refresh
            FormatSprdView()
            SprdView.Refresh()

            SprdView.Focus()
            FraGridView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            FraGridView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsPerks, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub Clear1()


        txtRefDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        TxtEmpCode.Text = ""
        TxtEmpName.Text = ""
        txtDept.Text = ""

        cboPaymentType.Items.Clear()
        cboPaymentType.Items.Add("1. Cheque")
        cboPaymentType.Items.Add("2. Cash")
        cboPaymentType.SelectedIndex = 0
        lblTotal.Text = "0.00"
        cboPaidWeek.SelectedIndex = 0
        cboPaidWeek.Enabled = True
        MainClass.ClearGrid(SprdMain)
        Call FormatMain()
        MainClass.ButtonStatus(Me, XRIGHT, RsPerks, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub CalcTotals()
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim mTotal As Double

        mTotal = 0
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColClaim
                mTotal = mTotal + Val(.Text)
            Next
        End With
        lblTotal.Text = VB6.Format(mTotal, "0.00")
        Exit Sub
ErrPart:

    End Sub
    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsPerks, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        ShowReport(Crystal.DestinationConstants.crptToWindow)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        ShowReport(Crystal.DestinationConstants.crptToPrinter)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click
        Dim SqlStr As String = ""

        SqlStr = ""

        If MainClass.SearchGridMaster((TxtEmpCode.Text), "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            TxtEmpCode.Text = AcName1
            TxtEmpName.Text = AcName
            txtEmpCode_Validating(txtEmpCode, New System.ComponentModel.CancelEventArgs(False))
        End If

        Exit Sub
    End Sub

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
        Else
            ADDMode = False
            MODIFYMode = False
            If RsPerks.EOF = False Then RsPerks.MoveFirst()
            Show1()
        End If
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        '    If txtTDSName.Text = "" Then MsgExclamation "Nothing to delete": Exit Sub

        If CheckPerksMade((TxtEmpCode.Text), VB6.Format(txtRefDate.Text, "DD/MM/YYYY")) = True Then
            MsgInformation("Back Perks Entry Cann't be deleted.")
            Exit Sub
        End If

        If Not RsPerks.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                If Delete1 = False Then GoTo DelErrPart
                If RsPerks.EOF = True Then
                    Clear1()
                Else
                    Clear1()
                    Show1()
                End If
            End If
        End If
        Exit Sub
DelErrPart:
        MsgBox("Record Not Deleted")
    End Sub


    Private Sub SprdMain_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles SprdMain.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim mPayableAmount As Double
        Dim mClaimAmount As Double
        Dim cntRow As Integer

        If lblBookType.Text = "P" Then
            For cntRow = 1 To SprdMain.MaxRows - 1
                SprdMain.Row = cntRow


                SprdMain.Col = ColPayment
                mPayableAmount = Val(SprdMain.Text)

                SprdMain.Col = ColClaim
                mClaimAmount = Val(SprdMain.Text)

                '            If mClaimAmount < 0 Then
                '                MsgInformation "Claim Amount Cann't be Less Than Zero."
                '                MainClass.SetFocusToCell SprdMain, cntRow, ColClaim
                '                Cancel = True
                '                Exit Sub
                '            End If

                If mClaimAmount > mPayableAmount And mClaimAmount <> 0 Then
                    MsgInformation("Claim Amount Cann't be Greater Than Payable Amount")
                    MainClass.SetFocusToCell(SprdMain, cntRow, ColClaim)
                    Cancel = True
                    GoTo EventExitSub
                End If
            Next
        End If
        Call CalcTotals()
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick


        SprdView.Row = SprdView.ActiveRow
        SprdView.Col = 1
        txtRefDate.Text = VB6.Format(SprdView.Text, "DD/MM/YYYY")

        SprdView.Col = 2
        TxtEmpCode.Text = VB6.Format(SprdView.Text, "000000")

        SprdView.Col = 6
        cboPaidWeek.Text = CStr(Val(SprdView.Text))

        txtEmpCode_Validating(txtEmpCode, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Private Sub frmPerksPaymentEntry_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub

        If lblBookType.Text = "P" Then
            Me.Text = "Perks Payment Entry"
        Else
            Me.Text = "Perks Payment Entry (Variable)"
        End If
        SqlStr = "SELECT * FROM PAY_PERKS_TRN WHERE  1<>1"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPerks, ADODB.LockTypeEnum.adLockReadOnly)


        cboPaidWeek.Items.Clear()
        cboPaidWeek.Items.Add("1")
        cboPaidWeek.Items.Add("2")
        cboPaidWeek.Items.Add("3")
        '    cboPaidWeek.AddItem "4"
        '    cboPaidWeek.AddItem "5"
        cboPaidWeek.SelectedIndex = 0

        Clear1()
        Call AssignGrid(False)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        settextlength()

        Show1()

        If RsPerks.EOF = True Then
            If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        End If
        Call FormatMain()
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmPerksPaymentEntry_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        Me.Height = VB6.TwipsToPixelsY(7485)
        Me.Width = VB6.TwipsToPixelsX(8340)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmPerksPaymentEntry_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsPerks = Nothing
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mEmpCode As String
        Dim cntRow As Integer
        Dim mAddDedCode As String
        Dim mAddDedName As String
        Dim mSalDate As String
        Dim mAmount As Double
        Dim mPayableAmount As Double


        If Not RsPerks.EOF Then

            txtRefDate.Text = "01/" & VB6.Format(IIf(IsDbNull(RsPerks.Fields("SAL_DATE").Value), "", RsPerks.Fields("SAL_DATE").Value), "MM/YYYY")

            TxtEmpCode.Text = IIf(IsDbNull(RsPerks.Fields("EMP_CODE").Value), "", RsPerks.Fields("EMP_CODE").Value)
            mEmpCode = IIf(IsDbNull(RsPerks.Fields("EMP_CODE").Value), "", RsPerks.Fields("EMP_CODE").Value)
            If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                TxtEmpName.Text = MasterNo
            End If

            If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_DEPT_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtDept.Text = MasterNo
            End If
            If RsPerks.Fields("PAYMENT_TYPE").Value = "1" Then
                cboPaymentType.SelectedIndex = 0
            Else
                cboPaymentType.SelectedIndex = 1
            End If
            cboPaidWeek.Text = CStr(Val(IIf(IsDbNull(RsPerks.Fields("PAID_WEEK").Value), 0, RsPerks.Fields("PAID_WEEK").Value)))
            RsPerks.MoveFirst()
            cboPaidWeek.Enabled = False
        End If

        mEmpCode = Trim(TxtEmpCode.Text)
        mSalDate = "01/" & VB6.Format(txtRefDate.Text, "MM/YYYY")
        If mEmpCode = "" Then GoTo NextLine

        For cntRow = 1 To SprdMain.MaxRows
            SprdMain.Row = cntRow
            SprdMain.Col = ColCode
            mAddDedCode = Trim(SprdMain.Text)

            mPayableAmount = GetPayableAmount(mEmpCode, mAddDedCode, mSalDate)
            mAmount = 0
            SqlStr = " SELECT * " & vbCrLf & " FROM PAY_PERKS_TRN WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE ='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf & " AND ADD_DEDUCTCODE ='" & MainClass.AllowSingleQuote(mAddDedCode) & "'" & vbCrLf & " AND TO_CHAR(SAL_DATE,'YYYYMM')='" & VB6.Format(mSalDate, "YYYYMM") & "' " & vbCrLf & " AND BOOKTYPE='" & lblBookType.Text & "'" & vbCrLf & " AND PAID_WEEK='" & Trim(cboPaidWeek.Text) & "'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                mAmount = IIf(IsDbNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
            End If

            SprdMain.Row = cntRow
            SprdMain.Col = ColPayment
            SprdMain.Text = VB6.Format(mPayableAmount, "0.00")

            SprdMain.Col = ColClaim
            SprdMain.Text = VB6.Format(mAmount, "0.00")

        Next

        CalcTotals()

        If Not RsPerks.EOF Then
NextLine:
            ADDMode = False
            MODIFYMode = False

            MainClass.ButtonStatus(Me, XRIGHT, RsPerks, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        End If

        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Function GetPayableAmount(ByRef pEmpCode As String, ByRef pAddDedCode As String, ByRef pSalDate As String) As Double

        On Error GoTo ShowErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mOPDate As String

        GetPayableAmount = 0

        mOPDate = GetOpeningPerksDate()

        SqlStr = " SELECT SUM(AMOUNT * DECODE(DC,'C',1,-1) * CASE WHEN (BOOKTYPE='P' OR BOOKTYPE='V') AND TO_CHAR(SAL_DATE,'YYYYMM')='" & VB6.Format(pSalDate, "YYYYMM") & "' THEN CASE WHEN BOOKTYPE='P' THEN 0 ELSE 1 END ELSE 1 END) AS AMOUNT " & vbCrLf & " FROM PAY_PERKS_TRN WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE ='" & MainClass.AllowSingleQuote(pEmpCode) & "'" & vbCrLf & " AND ADD_DEDUCTCODE ='" & MainClass.AllowSingleQuote(pAddDedCode) & "'" & vbCrLf & " AND TO_CHAR(SAL_DATE,'YYYYMM')<='" & VB6.Format(pSalDate, "YYYYMM") & "'"

        If mOPDate <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(SAL_DATE,'YYYYMM')>='" & VB6.Format(mOPDate, "YYYYMM") & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY SAL_DATE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetPayableAmount = IIf(IsDbNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
        End If

        SqlStr = " SELECT SUM(Amount) AS Amount " & vbCrLf & " FROM PAY_PERKS_TRN WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE ='" & MainClass.AllowSingleQuote(pEmpCode) & "'" & vbCrLf & " AND ADD_DEDUCTCODE ='" & MainClass.AllowSingleQuote(pAddDedCode) & "'" & vbCrLf & " AND TO_CHAR(SAL_DATE,'YYYYMM')='" & VB6.Format(pSalDate, "YYYYMM") & "' " & vbCrLf & " AND BOOKTYPE='" & lblBookType.Text & "'" & vbCrLf & " AND PAID_WEEK<>'" & Trim(cboPaidWeek.Text) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            GetPayableAmount = GetPayableAmount - IIf(IsDbNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
        End If

        Exit Function
ShowErrPart:
        MsgBox(Err.Description)
        GetPayableAmount = 0
    End Function

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If Update1 = True Then
            ADDMode = False
            MODIFYMode = False
            txtEmpCode_Validating(txtEmpCode, New System.ComponentModel.CancelEventArgs(False))
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

    Private Function FieldsVarification() As Boolean
        On Error GoTo ERR1

        FieldsVarification = True


        If ValidateBookLocking(PubDBCn, CInt(ConLockPerksProcess), VB6.Format(txtRefDate.Text, "DD/MM/YYYY")) = True Then
            FieldsVarification = False
            Exit Function
        End If


        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If

        If TxtEmpCode.Text = "" Then
            MsgInformation("Please Entered Emp Code.")
            TxtEmpCode.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If txtRefDate.Text = "" Then
            MsgInformation("Please Entered Ref Date.")
            txtRefDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(cboPaymentType.Text) = "" Then
            MsgInformation("Please Select Payment Type.")
            cboPaymentType.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If PubUserID <> "G0416" Then
            If CheckPerksMade((TxtEmpCode.Text), VB6.Format(txtRefDate.Text, "DD/MM/YYYY")) = True Then
                MsgInformation("Back Perks Entry Cann't be Made. So Cann't be Modified")
                FieldsVarification = False
                Exit Function
            End If
        End If

        If MODIFYMode = True And RsPerks.EOF = True Then Exit Function
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Sub settextlength()

        On Error GoTo ERR1

        txtRefDate.MaxLength = 10
        TxtEmpCode.Maxlength = RsPerks.Fields("EMP_CODE").DefinedSize
        TxtEmpName.Maxlength = MainClass.SetMaxLength("EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn)
        txtDept.Maxlength = MainClass.SetMaxLength("EMP_DEPT_CODE", "PAY_EMPLOYEE_MST", PubDBCn)
        '    txtPlace.MaxLength = RsPerks.Fields("PLACE_VISIT").DefinedSize

        Exit Sub
ERR1:
        MsgBox(Err.Description)
        '' Resume
    End Sub

    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        Dim SqlStr As String = ""

        SqlStr = " SELECT TRN.SAL_DATE, TRN.EMP_CODE, EMP.EMP_NAME, SH.NAME, TRN.AMOUNT, PAID_WEEK" & vbCrLf & " FROM PAY_PERKS_TRN TRN, PAY_EMPLOYEE_MST EMP, PAY_SALARYHEAD_MST SH " & vbCrLf & " WHERE TRN.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.COMPANY_CODE = EMP.COMPANY_CODE" & vbCrLf & " AND TRN.EMP_CODE = EMP.EMP_CODE" & vbCrLf & " AND TRN.COMPANY_CODE = SH.COMPANY_CODE" & vbCrLf & " AND TRN.ADD_DEDUCTCODE = SH.CODE AND BOOKTYPE='" & lblBookType.Text & "'" & vbCrLf & " ORDER BY TRN.SAL_DATE, PAID_WEEK, EMP.EMP_NAME"


        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 12)
            .set_ColWidth(0, 5)
            .set_ColWidth(1, 8)
            .set_ColWidth(2, 8)
            .set_ColWidth(3, 25)
            .set_ColWidth(4, 6)
            .set_ColWidth(5, 6)

            .ColsFrozen = 1
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub

    Private Function Delete1() As Boolean
        On Error GoTo DeleteErr
        Dim SqlStr As String = ""

        SqlStr = ""
        '     If IsFieldExist = True Then Delete1 = False: Exit Function

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        If InsertIntoDelAudit(PubDBCn, "PAY_PERKS_TRN", (txtRefDate.Text), RsPerks) = False Then GoTo DeleteErr
        If InsertIntoDeleteTrn(PubDBCn, "PAY_PERKS_TRN", "EMP_CODE || ':' || TO_CHAR(SAL_DATE,'YYYYMM')", TxtEmpCode.Text & ":" & VB6.Format(txtRefDate.Text, "YYYYMM")) = False Then GoTo DeleteErr

        SqlStr = " DELETE " & vbCrLf & " FROM PAY_PERKS_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE=" & MainClass.AllowSingleQuote((TxtEmpCode.Text)) & "" & vbCrLf & " AND TO_CHAR(SAL_DATE,'YYYYMM')=" & VB6.Format(txtRefDate.Text, "YYYYMM") & "" & vbCrLf & " AND BOOKTYPE='" & lblBookType.Text & "'" & vbCrLf & " AND PAID_WEEK=" & Val(cboPaidWeek.Text) & ""

        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()
        RsPerks.Requery()
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans()
        RsPerks.Requery()
        If Err.Number = -2147467259 Then
            MsgBox("Can't Delete Transaction Exists Against this Code")
            Exit Function
        End If
        MsgBox(Err.Description)
    End Function

    Private Sub ShowReport(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim mTitle As String
        Dim mSubTitle As String

        Report1.Reset()
        mTitle = "EMPLOYEE PERKS SLIP"
        mSubTitle = "From the Month: " & VB6.Format(txtRefDate.Text, "MMMM-YYYY")

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\PerksSlip.rpt"
        SetCrpt(Report1, Mode, 1, mTitle, mSubTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub



    Private Sub TxtDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtDept_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDept.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDept.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEmpCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtEmpCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpCode.DoubleClick
        cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub

    Private Sub txtEmpCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtEmpCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub

    Private Sub txtEmpCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmpCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing

        If Trim(TxtEmpCode.Text) = "" Then GoTo EventExitSub


        TxtEmpCode.Text = VB6.Format(TxtEmpCode.Text, "000000")

        SqlStr = "SELECT * FROM PAY_EMPLOYEE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote((TxtEmpCode.Text)) & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockOptimistic)

        If RS.EOF = False Then
            TxtEmpCode.Text = RS.Fields("EMP_CODE").Value
            TxtEmpName.Text = IIf(IsDbNull(RS.Fields("EMP_NAME").Value), "", RS.Fields("EMP_NAME").Value)
            txtDept.Text = IIf(IsDbNull(RS.Fields("EMP_DEPT_CODE").Value), "", RS.Fields("EMP_DEPT_CODE").Value)
        Else
            MsgBox("This Employee Code does not exsits in Master.", MsgBoxStyle.Critical)
            Cancel = True
            GoTo EventExitSub
        End If

        If Trim(txtRefDate.Text) = "" Then GoTo EventExitSub

        If MODIFYMode = True And RsPerks.EOF = False Then xEmpCode = RsPerks.Fields("EMP_CODE").Value

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM PAY_PERKS_TRN WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE ='" & TxtEmpCode.Text & "'" & vbCrLf & " AND TO_CHAR(SAL_DATE,'YYYYMM')='" & VB6.Format(txtRefDate.Text, "YYYYMM") & "'" & vbCrLf & " AND BOOKTYPE = '" & lblBookType.Text & "'" & vbCrLf & " AND PAID_WEEK=" & Val(cboPaidWeek.Text) & ""

        SqlStr = SqlStr & vbCrLf & " ORDER BY SAL_DATE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPerks, ADODB.LockTypeEnum.adLockReadOnly)
        If RsPerks.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Click Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                MainClass.UOpenRecordSet("Select * From PAY_PERKS_TRN Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(xEmpCode) & "'" & vbCrLf & " AND BOOKTYPE='" & lblBookType.Text & "'" & vbCrLf & " AND PAID_WEEK=" & Val(cboPaidWeek.Text) & "" & vbCrLf & " AND TO_CHAR(SAL_DATE,'YYYYMM')='" & VB6.Format(txtRefDate.Text, "YYYY") & "'", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPerks, ADODB.LockTypeEnum.adLockReadOnly)
            Else
                Show1()
            End If
        End If

        GoTo EventExitSub
ERR1:
        '   Resume
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtEmpName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtEmpName.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtEmpName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtEmpName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtEmpName.Text)
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

        If Trim(txtRefDate.Text) = "" Or Trim(txtRefDate.Text) = "__/__/____" Then GoTo EventExitSub

        If Not IsDate(txtRefDate.Text) Then
            MsgBox("Invalid Date.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If
        txtRefDate.Text = "01/" & VB6.Format(txtRefDate.Text, "MM/YYYY")

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
