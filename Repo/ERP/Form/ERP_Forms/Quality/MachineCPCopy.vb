Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmMachineCPCopy
    Inherits System.Windows.Forms.Form
    Dim RsMachineCPHdr As ADODB.Recordset
    Dim PvtDBCn As ADODB.Connection
    Dim FormActive As Boolean
    Dim SqlStr As String
    Dim XRIGHT As String

    Private Sub cmdSearchCheckType_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchCheckType.Click

        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = " SELECT DISTINCT CHECK_TYPE FROM MAN_MACHINE_MAINT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & vbCrLf & " AND MACHINE_NO IN ( " & vbCrLf & " SELECT MACHINE_NO FROM MAN_MACHINE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & vbCrLf & " AND STATUS='O' AND MAINT_TYPE IN ('P','H') "
        If Trim(txtMachineDescNew.Text) <> "" Then
            SqlStr = SqlStr & " AND MACHINE_DESC='" & MainClass.AllowSingleQuote(txtMachineDescNew.Text) & "' "
        End If
        If Trim(txtMachineSpecNew.Text) <> "" Then
            SqlStr = SqlStr & " AND MACHINE_SPEC='" & MainClass.AllowSingleQuote(txtMachineSpecNew.Text) & "' "
        End If
        SqlStr = SqlStr & vbCrLf & " ) "

        If MainClass.SearchGridMasterBySQL2(txtCheckTypeNew.Text, SqlStr) = True Then
            txtCheckTypeNew.Text = AcName
        End If
        txtCheckTypeNew.Focus()
        Exit Sub
SrchProbERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchMachineDesc_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchMachineDesc.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='O' AND MAINT_TYPE IN ('P','H') "
        If MainClass.SearchGridMaster(txtMachineDescNew.Text, "MAN_MACHINE_MST", "MACHINE_DESC", , , , SqlStr) = True Then
            txtMachineDescNew.Text = AcName
        End If
        txtMachineDescNew.Focus()
        Exit Sub
SrchProbERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchMachineSpec_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchMachineSpec.Click

        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='O' AND MAINT_TYPE IN ('P','H') "
        If Trim(txtMachineDescNew.Text) <> "" Then
            SqlStr = SqlStr & " AND MACHINE_DESC='" & MainClass.AllowSingleQuote(txtMachineDescNew.Text) & "' "
        End If
        If MainClass.SearchGridMaster(txtMachineSpecNew.Text, "MAN_MACHINE_MST", "MACHINE_SPEC", , , , SqlStr) = True Then
            txtMachineSpecNew.Text = AcName
        End If
        txtMachineSpecNew.Focus()
        Exit Sub
SrchProbERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchNumber_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchNumber.Click
        On Error GoTo SrchProbERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value
        If MainClass.SearchGridMaster("", "MAN_MACHINE_CP_HDR", "Auto_Key_CP", "MACHINE_DESC", "MACHINE_SPEC", "CHECK_TYPE", SqlStr) = True Then
            txtNumber.Text = AcName
            txtNumber_Validating(txtNumber, New System.ComponentModel.CancelEventArgs(False)) ''__Validating(XXXX, New System.ComponentModel.CancelEventArgs(False)) ''_Validate(False)
        End If
        Exit Sub
SrchProbERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub frmMachineCPCopy_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Public Sub frmMachineCPCopy_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        Me.Text = "Copy Preventive Maintenance Check Points"

        SqlStr = " Select * From MAN_MACHINE_CP_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMachineCPHdr, ADODB.LockTypeEnum.adLockReadOnly)

        Call SetTextLengths()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '   Resume
    End Sub

    Private Sub frmMachineCPCopy_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Left = 0
        Me.Top = 0
        'Me.Height = VB6.TwipsToPixelsY(4755)
        'Me.Width = VB6.TwipsToPixelsX(8460)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmMachineCPCopy_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsMachineCPHdr.Close()
        RsMachineCPHdr = Nothing
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification = False Then Exit Sub
        If IsRecordExist = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Update1 = True Then
            CmdSave.Enabled = False
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
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        IsRecordExist = False
        SqlStr = " SELECT AUTO_KEY_CP " & vbCrLf _
                    & " From MAN_MACHINE_CP_HDR " & vbCrLf _
                    & " WHERE COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                    & " AND MACHINE_DESC ='" & MainClass.AllowSingleQuote(txtMachineDescNew.Text) & "' " & vbCrLf _
                    & " AND MACHINE_SPEC = '" & MainClass.AllowSingleQuote(txtMachineSpecNew.Text) & "' " & vbCrLf _
                    & " AND CHECK_TYPE= '" & MainClass.AllowSingleQuote(txtCheckTypeNew.Text) & "' "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With RsTemp
            If Not .EOF Then
                MsgBox("This entry already exist in Number : " & .Fields("AUTO_KEY_CP").Value)
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

        mAutoGen = 1
        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_CP)  " & vbCrLf & " FROM MAN_MACHINE_CP_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAutoGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsAutoGen
            If .EOF = False Then
                If Not IsDbNull(.Fields(0).Value) Then
                    '                mAutoGen = Mid(.Fields(0), 1, Len(.Fields(0)) - 6)
                    mAutoGen = .Fields(0).Value + 1
                Else
                    mAutoGen = 1
                End If
            End If
        End With
        AutoGenKeyNo = mAutoGen
        '    AutoGenKeyNo = mAutoGen & vb6.Format(RsCompany.fields("FYEAR").value, "0000") & vb6.Format(RsCompany.fields("COMPANY_CODE").value, "00")
        RsAutoGen.Close()
        RsAutoGen = Nothing
        Exit Function
AutogenErr:
        MsgBox(Err.Description)
    End Function

    Private Function Update1() As Boolean

        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset
        Dim SqlStr As String
        Dim mSlipNo As Double
        Dim NewSlipNo As Double
        Dim mRequirment As String
        Dim mCheckingPoint As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mSlipNo = Val(txtNumber.Text)
        NewSlipNo = AutoGenKeyNo()

        SqlStr = ""
        SqlStr = " INSERT INTO MAN_MACHINE_CP_HDR " & vbCrLf _
                    & " (COMPANY_CODE,AUTO_KEY_CP,MACHINE_DESC,MACHINE_SPEC,CHECK_TYPE, " & vbCrLf _
                    & " ADDUSER, ADDDATE, MODUSER, MODDATE) " & vbCrLf _
                    & " VALUES ( " & vbCrLf _
                    & " " & RsCompany.fields("COMPANY_CODE").value & "," & NewSlipNo & ", " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtMachineDescNew.Text) & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtMachineSpecNew.Text) & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtCheckTypeNew.Text) & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"


        PubDBCn.Execute(SqlStr)

        'SqlStr = ""
        'SqlStr = " SELECT * " & vbCrLf _
        '    & " FROM MAN_MACHINE_CP_DET " & vbCrLf _
        '    & " WHERE AUTO_KEY_CP=" & mSlipNo & " " & vbCrLf _
        '    & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value

        'MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        'With RsTemp
        '    If Not .EOF Then
        '        Do While Not .EOF

        SqlStr = " INSERT INTO  MAN_MACHINE_CP_DET ( " & vbCrLf _
                        & " COMPANY_CODE,AUTO_KEY_CP,SERIAL_NO,CATEGORY,CHECK_POINT,CHECK_REQUIRMENT, CHECK_METHOD ) " & vbCrLf _
                        & " SELECT COMPANY_CODE, " & NewSlipNo & ", " & vbCrLf _
                        & " SERIAL_NO,CATEGORY,CHECK_POINT,CHECK_REQUIRMENT, CHECK_METHOD " & vbCrLf _
                        & " FROM MAN_MACHINE_CP_DET " & vbCrLf _
                        & " WHERE AUTO_KEY_CP=" & mSlipNo & " " & vbCrLf _
                        & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""


        PubDBCn.Execute(SqlStr)

        '& " VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & "," & NewSlipNo & "," & .Fields("SERIAL_NO").Value & "," & vbCrLf _
        '& " '" & .Fields("CATEGORY").Value & "','" & .Fields("CHECK_POINT").Value & "')"




        '            .MoveNext()
        '        Loop
        '    End If
        'End With

        Update1 = True
        PubDBCn.CommitTrans()
        txtNumberNew.Text = CStr(NewSlipNo)
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsMachineCPHdr.Requery()
        MsgBox(Err.Description)
        '    Resume
    End Function

    Private Function FieldsVarification() As Boolean
        On Error GoTo VarificationErr
        FieldsVarification = True

        If Trim(txtNumber.Text) = "" Then
            MsgInformation("From : Number is empty, So unable to Save")
            txtNumber.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtMachineDescNew.Text) = "" Then
            MsgInformation("To : Machine Desc is empty, So unable to Save")
            txtMachineDescNew.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtMachineSpecNew.Text) = "" Then
            MsgInformation("To : Specification is empty, So unable to Save")
            txtMachineSpecNew.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtCheckTypeNew.Text) = "" Then
            MsgInformation("To : Check Type is empty, So unable to Save")
            txtCheckTypeNew.Focus()
            FieldsVarification = False
            Exit Function
        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Exit Function
VarificationErr:
        FieldsVarification = False
        MsgInformation(Err.Description)
    End Function

    Private Sub txtCheckTypeNew_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCheckTypeNew.DoubleClick
        Call cmdSearchCheckType_Click(cmdSearchCheckType, New System.EventArgs())
    End Sub

    Private Sub txtCheckTypeNew_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCheckTypeNew.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCheckTypeNew.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCheckTypeNew_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCheckTypeNew.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchCheckType_Click(cmdSearchCheckType, New System.EventArgs())
    End Sub

    Private Sub txtCheckTypeNew_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCheckTypeNew.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ValProbERR
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        If Trim(txtCheckTypeNew.Text) = "" Then GoTo EventExitSub
        SqlStr = " SELECT DISTINCT CHECK_TYPE FROM MAN_MACHINE_MAINT_TRN " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & vbCrLf _
                    & " AND CHECK_TYPE='" & MainClass.AllowSingleQuote(txtCheckTypeNew.Text) & "' " & vbCrLf _
                    & " AND MACHINE_NO IN ( " & vbCrLf _
                    & " SELECT MACHINE_NO FROM MAN_MACHINE_MST " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & vbCrLf _
                    & " AND STATUS='O' AND MAINT_TYPE IN ('P','H') "
        If Trim(txtMachineDescNew.Text) <> "" Then
            SqlStr = SqlStr & " AND MACHINE_DESC='" & MainClass.AllowSingleQuote(txtMachineDescNew.Text) & "' "
        End If
        If Trim(txtMachineSpecNew.Text) <> "" Then
            SqlStr = SqlStr & " AND MACHINE_SPEC='" & MainClass.AllowSingleQuote(txtMachineSpecNew.Text) & "' "
        End If
        SqlStr = SqlStr & vbCrLf & " ) "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF Then
            MsgBox("Not a valid Check Type", MsgBoxStyle.Information)
            Cancel = True
        End If
        GoTo EventExitSub
ValProbERR:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtMachineDescNew_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMachineDescNew.DoubleClick
        Call cmdSearchMachineDesc_Click(cmdSearchMachineDesc, New System.EventArgs())
    End Sub

    Private Sub txtMachineDescNew_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMachineDescNew.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtMachineDescNew.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtMachineDescNew_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtMachineDescNew.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchMachineDesc_Click(cmdSearchMachineDesc, New System.EventArgs())
    End Sub

    Private Sub txtMachineDescNew_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMachineDescNew.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValProbERR
        Dim SqlStr As String
        If Trim(txtMachineDescNew.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='O' AND MAINT_TYPE IN ('P','H') "
        If MainClass.ValidateWithMasterTable(txtMachineDescNew.Text, "MACHINE_DESC", "MACHINE_DESC", "MAN_MACHINE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Not a valid Machine Desc", vbInformation)
            Cancel = True
        End If
        GoTo EventExitSub
ValProbERR:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtMachineSpecNew_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMachineSpecNew.DoubleClick
        Call cmdSearchMachineSpec_Click(cmdSearchMachineSpec, New System.EventArgs())
    End Sub

    Private Sub txtMachineSpecNew_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMachineSpecNew.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtMachineSpecNew.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtMachineSpecNew_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtMachineSpecNew.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchMachineSpec_Click(cmdSearchMachineSpec, New System.EventArgs())
    End Sub

    Private Sub txtMachineSpecNew_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMachineSpecNew.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ValProbERR
        Dim SqlStr As String
        If Trim(txtMachineSpecNew.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='O' AND MAINT_TYPE IN ('P','H') "
        If Trim(txtMachineDescNew.Text) <> "" Then
            SqlStr = SqlStr & " AND MACHINE_DESC='" & MainClass.AllowSingleQuote(txtMachineDescNew.Text) & "' "
        End If
        If MainClass.ValidateWithMasterTable(txtMachineSpecNew.Text, "MACHINE_SPEC", "MACHINE_SPEC", "MAN_MACHINE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Not a valid Specification", MsgBoxStyle.Information)
            Cancel = True
        End If
        GoTo EventExitSub
ValProbERR:
        MsgBox(Err.Description)
EventExitSub:
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

    Public Sub txtNumber_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtNumber.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMKey As Double
        Dim mSlipNo As Double
        Dim SqlStr As String

        If Trim(txtNumber.Text) = "" Then GoTo EventExitSub
        mSlipNo = Val(txtNumber.Text)

        SqlStr = "SELECT * FROM MAN_MACHINE_CP_HDR " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND AUTO_KEY_CP=" & mSlipNo & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMachineCPHdr, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMachineCPHdr.EOF = False Then
            txtMachineDesc.Text = IIf(IsDbNull(RsMachineCPHdr.Fields("MACHINE_DESC").Value), "", RsMachineCPHdr.Fields("MACHINE_DESC").Value)
            txtMachineSpec.Text = IIf(IsDbNull(RsMachineCPHdr.Fields("MACHINE_SPEC").Value), "", RsMachineCPHdr.Fields("MACHINE_SPEC").Value)
            txtCheckType.Text = IIf(IsDbNull(RsMachineCPHdr.Fields("CHECK_TYPE").Value), "", RsMachineCPHdr.Fields("CHECK_TYPE").Value)
        Else
            MsgInformation("No Such Number.")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub SetTextLengths()
        On Error GoTo ERR1
        txtNumber.Maxlength = RsMachineCPHdr.Fields("AUTO_KEY_CP").DefinedSize
        txtMachineDesc.Maxlength = RsMachineCPHdr.Fields("MACHINE_DESC").DefinedSize
        txtMachineSpec.Maxlength = RsMachineCPHdr.Fields("MACHINE_SPEC").DefinedSize
        txtCheckType.Maxlength = RsMachineCPHdr.Fields("CHECK_TYPE").DefinedSize
        txtNumberNew.Maxlength = RsMachineCPHdr.Fields("AUTO_KEY_CP").DefinedSize
        txtMachineDescNew.Maxlength = RsMachineCPHdr.Fields("MACHINE_DESC").DefinedSize
        txtMachineSpecNew.Maxlength = RsMachineCPHdr.Fields("MACHINE_SPEC").DefinedSize
        txtCheckTypeNew.Maxlength = RsMachineCPHdr.Fields("CHECK_TYPE").DefinedSize
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub txtNumber_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNumber.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
End Class
