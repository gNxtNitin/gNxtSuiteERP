Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmESIChallan
    Inherits System.Windows.Forms.Form
    Dim RsEmp As ADODB.Recordset = Nothing

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection


    Dim SqlStr As String = ""
    Dim FormActive As Boolean

    Private Sub cboEmployee_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboEmployee.TextChanged
        Show1()
    End Sub

    Private Sub cboEmployee_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboEmployee.SelectedIndexChanged
        Show1()
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
    End Sub

    Private Sub cmdOk_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOk.Click
        If Not IsDate(txtChallanDate.Text) Then
            MsgInformation("Please Enter Vaild Challan Date.")
            txtChallanDate.Focus()
            Exit Sub
        End If

        Update1()
    End Sub

    Private Sub frmESIChallan_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        Show1()
    End Sub

    Private Sub frmESIChallan_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        'Set PvtDBCn = New Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Me.Height = VB6.TwipsToPixelsY(4125)
        Me.Width = VB6.TwipsToPixelsX(4455)
        Me.Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(Me.Width)) / 2)
        Me.Top = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - VB6.PixelsToTwipsY(Me.Height)) / 2)
        lblNewDate.Text = CStr(RunDate)
        txtMonth.Text = MonthName(Month(RunDate))
        TxtYear.Text = CStr(Year(RunDate))
        Call FillDeptCombo()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub

    Private Function Update1() As Boolean
        On Error GoTo UpdateError
        Dim mYM As Integer

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM PAY_ESICHALLAN_TRN WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " CHALLANMONTH=" & Month(CDate(lblNewDate.Text)) & " " & vbCrLf & " AND CHALLANYEAR= " & TxtYear.Text & " " & vbCrLf & " AND CONT_NAME='" & MainClass.AllowSingleQuote(cboEmployee.Text) & "'"

        PubDBCn.Execute(SqlStr)

        mYM = CInt(TxtYear.Text & VB6.Format(Month(CDate(lblNewDate.Text)), "00"))

        SqlStr = "INSERT INTO PAY_ESICHALLAN_TRN (COMPANY_CODE , FYEAR, CHALLANMONTH," & vbCrLf & " CHALLANYEAR , CHALLANDATE, EMPERSHARE, EMPSHARE, " & vbCrLf & " TOTALAMOUNT , YM, " & vbCrLf & " CONT_NAME, " & vbCrLf & " ADDUSER, ADDDATE) VALUES " & vbCrLf & " (" & RsCompany.Fields("COMPANY_CODE").Value & "," & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf & " " & Month(CDate(lblNewDate.Text)) & ", " & TxtYear.Text & ", " & vbCrLf & " TO_DATE('" & VB6.Format(txtChallanDate.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY'), " & Val(txtEmperShare.Text) & ", " & vbCrLf & "  " & Val(txtEmpShare.Text) & ", " & Val(txtTotAmount.Text) & ", " & mYM & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(cboEmployee.Text) & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()
        Me.hide()
        Exit Function
UpdateError:
        PubDBCn.RollbackTrans()
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        PubDBCn.Errors.Clear()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Sub FillDeptCombo()

        Dim RsDept As ADODB.Recordset = Nothing
        SqlStr = "Select DISTINCT CON_NAME from PAY_CONTRACTOR_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " Order by CON_NAME"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockOptimistic)

        cboEmployee.Items.Add("EMPLOYEE")

        If RsDept.EOF = False Then
            Do While Not RsDept.EOF
                cboEmployee.Items.Add(RsDept.Fields("CON_NAME").Value)
                RsDept.MoveNext()
            Loop
        End If
        cboEmployee.SelectedIndex = 0
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub Show1()

        Dim RSChallan As ADODB.Recordset
        Dim cntRow As Integer
        Dim mCode As Integer

        Clear1()
        SqlStr = " SELECT * FROM PAY_ESICHALLAN_TRN WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CHALLANMONTH=" & Month(CDate(lblNewDate.Text)) & "" & vbCrLf & " AND CHALLANYEAR= " & TxtYear.Text & " " & vbCrLf & " AND CONT_NAME='" & MainClass.AllowSingleQuote(cboEmployee.Text) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSChallan, ADODB.LockTypeEnum.adLockOptimistic)

        If RSChallan.EOF = False Then
            With RSChallan
                txtEmperShare.Text = VB6.Format(IIf(IsDbNull(.Fields("EMPERSHARE").Value), "", .Fields("EMPERSHARE").Value), "0.00")
                txtEmpShare.Text = VB6.Format(IIf(IsDbNull(.Fields("EMPSHARE").Value), "", .Fields("EMPSHARE").Value), "0.00")
                txtTotAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("TotalAmount").Value), "", .Fields("TotalAmount").Value), "0.00")
                txtChallanDate.Text = IIf(IsDate(.Fields("CHALLANDATE").Value), .Fields("CHALLANDATE").Value, "")
            End With
        Else
            CalcEmpShare()
        End If
    End Sub
    Private Sub txtChallanDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtChallanDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtChallanDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtChallanDate.Text) Then
            MsgInformation("Please enter vaild date.")
            Cancel = True
        Else
            txtChallanDate.Text = VB6.Format(txtChallanDate.Text, "dd/mm/yyyy")
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtEmperShare_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEmperShare.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtEmperShare_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmperShare.Leave
        CalcTotAmount()
    End Sub

    Private Sub txtEmpShare_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEmpShare.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtEmpShare_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpShare.Leave
        CalcTotAmount()
    End Sub

    Private Sub txtTotAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTotAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    'Private Sub UpDMonth_DownClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles UpDMonth.DownClick
    '    SetNewDate(-1)
    '    txtMonth.Text = MonthName(Month(CDate(lblNewDate.Text)))
    '    TxtYear.Text = CStr(Year(CDate(lblNewDate.Text)))
    '    Show1()
    'End Sub
    Sub SetNewDate(ByRef prmSpinDirection As Short)
        lblNewDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, prmSpinDirection, CDate(lblNewDate.Text)))
    End Sub
    'Private Sub UpDMonth_UpClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles UpDMonth.UpClick
    '    SetNewDate(1)
    '    txtMonth.Text = MonthName(Month(CDate(lblNewDate.Text)))
    '    TxtYear.Text = CStr(Year(CDate(lblNewDate.Text)))
    '    Show1()
    'End Sub
    'Private Sub UpDYear_DownClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles UpDYear.DownClick
    '    TxtYear.Text = CStr(CDbl(TxtYear.Text) - 1)
    '    Show1()
    'End Sub
    'Private Sub UpDYear_UpClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles UpDYear.UpClick
    '    TxtYear.Text = CStr(CDbl(TxtYear.Text) + 1)
    '    Show1()
    'End Sub

    Private Sub Clear1()
        txtEmperShare.Text = ""
        txtEmpShare.Text = ""
        txtTotAmount.Text = ""
        txtChallanDate.Text = ""
    End Sub

    Private Sub CalcTotAmount()
        txtTotAmount.Text = VB6.Format(IIf(IsNumeric(txtEmperShare.Text), Val(txtEmperShare.Text), 0) + IIf(IsNumeric(txtEmpShare.Text), Val(txtEmpShare.Text), 0), "0.00")
    End Sub
    Private Sub CalcEmpShare()

        Dim RsAttn As ADODB.Recordset = Nothing
        Dim cntRow As Integer
        Dim cntCol As Integer

        SqlStr = " SELECT SUM(ESI_AMT) " & vbCrLf & " FROM PAY_CONTESI_TRN WHERE" & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CONT_NAME='" & MainClass.AllowSingleQuote((cboEmployee.Text)) & "'" & vbCrLf & " AND TO_CHAR(EDATE,'MON-YYYY')='" & UCase(VB6.Format(lblNewDate.Text, "MMM-YYYY")) & "'" & vbCrLf & " AND ESI_AMT>0"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAttn, ADODB.LockTypeEnum.adLockOptimistic)

        If RsAttn.EOF = False Then
            txtEmpShare.Text = VB6.Format(IIf(IsDbNull(RsAttn.Fields(0).Value), 0, RsAttn.Fields(0).Value), "0.00")
        End If
    End Sub
End Class
