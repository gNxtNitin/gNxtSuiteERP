Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmAddFYNo
    Inherits System.Windows.Forms.Form
    'Dim PvtDBCN As ADODB.Connection
    Dim RsNewFYNo As ADODB.Recordset

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Dim xNewFYNo As Integer
    Dim FormActive As Boolean
    Dim SqlStr As String = ""
    Private Sub Clear1()
        ''
        txtNewFYNo.Text = ""
        txtNewFYDateFrom.Text = ""
        txtNewFYDateTo.Text = ""
        'MainClass.ButtonStatus(Me, XRIGHT, RsNewFYNo, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    '    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
    '        ''
    '        On Error GoTo ModifyErr
    '        If CmdModify.Text = ConcmdmodifyCaption Then
    '            ADDMode = False
    '            MODIFYMode = True
    '            'MainClass.ButtonStatus(Me, XRIGHT, RsNewFYNo, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    '        Else
    '            ADDMode = False
    '            MODIFYMode = False
    '            Show1()
    '        End If
    '        Exit Sub
    'ModifyErr:
    '        MsgBox(Err.Description)
    '    End Sub
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo err_Renamed
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            cmdSave.Enabled = True
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
err_Renamed:
        MsgInformation(Err.Description)
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide() ''me.hide 
        Me.Close()
    End Sub
    Private Function Delete1() As Boolean
        On Error GoTo DeleteErr
        SqlStr = ""
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        If InsertIntoDelAudit(PubDBCn, "GEN_CMPYRDTL_TRN", (txtNewFYNo.Text), RsNewFYNo) = False Then GoTo DeleteErr
        If InsertIntoDeleteTrn(PubDBCn, "GEN_CMPYRDTL_TRN", "FYEAR", Str(xNewFYNo)) = False Then GoTo DeleteErr

        SqlStr = "DELETE FROM GEN_CMPYRDTL_TRN " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & xNewFYNo & ""

        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        RsNewFYNo.Requery()
        RsCompany.Requery()
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans()
        RsNewFYNo.Requery()
        RsCompany.Requery()
        If Err.Number = -2147467259 Then
            MsgBox("Can't Delete Transaction Exists Against This Branch.")
            Exit Function
        End If
        MsgBox(Err.Description)
    End Function
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        On Error GoTo DelErrPart
        If Trim(txtNewFYNo.Text) = "" Then MsgExclamation("Nothing to delete") : Exit Sub
        If Not RsNewFYNo.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                If Delete1() = False Then GoTo DelErrPart
                If RsNewFYNo.EOF = True Then
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
    Private Sub frmAddFYNo_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        '
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        MainClass.UOpenRecordSet("Select * From GEN_CMPYRDTL_TRN WHERE FYEAR=" & RsCompany.Fields("COMPANY_CODE").Value & "", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsNewFYNo, ADODB.LockTypeEnum.adLockReadOnly)
        Call Clear1()
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmAddFYNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub frmAddFYNo_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        '
        On Error GoTo ErrPart
        Dim mDate As String
        Dim mDateTime As Date




        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call SetMainFormCordinate(Me)
        'Me.Left = 0
        'Me.Top = 0
        'Me.Height = 3390
        'Me.Width = 6270

        'Set PvtDBCN = New Connection
        'PvtDBCN.Open StrConn

        XRIGHT = MainClass.STRMenuRight(PubUserID, 1, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        ADDMode = False
        MODIFYMode = False

        TxtCompany.Text = RsCompany.Fields("Company_Name").Value
        txtCurrFYNo.Text = RsCompany.Fields("FYEAR").Value
        mDateTime = RsCompany.Fields("Start_Date").Value
        mDate = mDateTime.ToString("dd/MM/yyyy")
        txtCurrFYDateFrom.Text = mDate

        mDateTime = RsCompany.Fields("END_DATE").Value
        mDate = mDateTime.ToString("dd/MM/yyyy")
        txtCurrFYDateTo.Text = mDate
        lblCCode.Text = RsCompany.Fields("COMPANY_CODE").Value

        FraCurrent.Enabled = True
        FraNew.Enabled = False

        'CmdDelete.Visible = False
        'CmdModify.Visible = False
        'CmdPreview.Visible = False
        'cmdPrint.Visible = False
        'CmdView.Visible = False
        'cmdSavePrint.Visible = False

        'CmdDelete.Enabled = False
        'CmdModify.Enabled = False
        'CmdPreview.Enabled = False
        'cmdPrint.Enabled = False
        'CmdView.Enabled = False
        'cmdSavePrint.Enabled = False

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmAddFYNo_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsNewFYNo = Nothing
        Me.Hide()
        Me.Close()
        'PvtDBCN.Cancel
        'PvtDBCN.Close
        'Set PvtDBCN = Nothing
    End Sub
    Private Sub Show1()
        '
        On Error GoTo ShowErrPart
        'Dim mDate As String
        'Dim mDateTime As Date

        If RsNewFYNo.EOF = False Then
            xNewFYNo = RsNewFYNo.Fields("FYEAR").Value
            txtNewFYNo.Text = IIf(IsDbNull(RsNewFYNo.Fields("FYEAR").Value), "", RsNewFYNo.Fields("FYEAR").Value)
            'mDate = IIf(IsDBNull(RsNewFYNo.Fields("Start_Date").Value), "", RsNewFYNo.Fields("Start_Date").Value)

            txtNewFYDateFrom.Text = IIf(IsDBNull(RsNewFYNo.Fields("Start_Date").Value), "", RsNewFYNo.Fields("Start_Date").Value)
            txtNewFYDateTo.Text = IIf(IsDbNull(RsNewFYNo.Fields("END_DATE").Value), "", RsNewFYNo.Fields("END_DATE").Value)
        End If
        ADDMode = False
        MODIFYMode = False
        'MainClass.ButtonStatus(Me, XRIGHT, RsNewFYNo, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
            ADDMode = False
            MODIFYMode = False
            txtNewFYNo_Validating(txtNewFYNo, New System.ComponentModel.CancelEventArgs(False))
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
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xCompanyCode As Integer
        If Val(lblCCode.Text) <= 0 Then
            MsgBox("Company Not Selected")
            Update1 = False
            Exit Function
        End If
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        SqlStr = ""

        If CheckConsolidatedMaster("FIN_SUPP_CUST_MST") = True Then
            SqlStr = "SELECT COMPANY_CODE FROM GEN_COMPANY_MST"
        Else
            SqlStr = "SELECT COMPANY_CODE FROM GEN_COMPANY_MST WHERE COMPANY_CODE=" & Val(lblCCode.Text) & ""
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                xCompanyCode = RsTemp.Fields("COMPANY_CODE").Value
                If ADDMode = True Then
                    If InsertInToFYear(xCompanyCode) = False Then GoTo UpdateError
                End If
                RsTemp.MoveNext()
            Loop
        End If
        PubDBCn.CommitTrans()
        Update1 = True
        RsCompany.Requery()
        Exit Function
UpdateError:
        Update1 = False
        PubDBCn.RollbackTrans()
        If Err.Description <> "" Then
            MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        End If
        'PvtDBCN.Errors.Clear
        RsNewFYNo.Requery()
        RsCompany.Requery()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function InsertInToFYear(ByRef mCompanyCode As Integer) As Boolean
        On Error GoTo BErr
        Dim mFYNo As Integer
        Dim mFYDateFrom As Date
        Dim mFYDateTo As Date
        SqlStr = ""
        mFYNo = AutoGenFYNo(mCompanyCode)
        txtNewFYNo.Text = CStr(mFYNo)
        mFYDateFrom = AutoGenFYDateFrom(mCompanyCode)
        mFYDateTo = AutoGenFYDateTo(mCompanyCode)

        SqlStr = "INSERT INTO GEN_CMPYRDTL_TRN(COMPANY_CODE,FYEAR,START_DATE,END_DATE,CLOSING_FLAG) " & vbCrLf & " VALUES (" & mCompanyCode & "," & mFYNo & ", " & vbCrLf & " TO_DATE('" & mFYDateFrom.ToString("dd-MMM-yyyy") & "','DD-MON-YYYY'),TO_DATE('" & mFYDateTo.ToString("dd-MMM-yyyy") & "','DD-MON-YYYY'),'N')"

        PubDBCn.Execute(SqlStr)
        InsertInToFYear = True
        Exit Function
BErr:
        InsertInToFYear = False
        MsgBox(Err.Description)
    End Function
    Private Function AutoGenFYNo(ByRef mCompanyCode As Integer) As Integer
        '
        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset = Nothing
        Dim AutoGenCode As Integer
        SqlStr = ""
        SqlStr = "SELECT Max(FYEAR)  " & " FROM GEN_CMPYRDTL_TRN " & " WHERE Company_Code=" & mCompanyCode & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAutoGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsAutoGen
            If .EOF = False Then
                If Not IsDbNull(.Fields(0).Value) Then
                    AutoGenCode = .Fields(0).Value + 1
                Else
                    AutoGenCode = 1
                End If
            Else
                AutoGenCode = 1
            End If
        End With
        AutoGenFYNo = AutoGenCode
        RsAutoGen.Close()
        RsAutoGen = Nothing
        Exit Function
AutogenErr:
        MsgBox(Err.Description)
    End Function
    Private Function AutoGenFYDateFrom(ByRef mCompanyCode As Integer) As Date
        '
        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset = Nothing
        Dim AutoGen As Date
        SqlStr = ""
        SqlStr = "SELECT Max(TO_DATE(START_DATE))  " & " FROM GEN_CMPYRDTL_TRN " & " WHERE Company_Code=" & mCompanyCode & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAutoGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsAutoGen
            If .EOF = False Then
                If Not IsDbNull(.Fields(0).Value) Then
                    AutoGen = DateAdd(Microsoft.VisualBasic.DateInterval.Year, 1, .Fields(0).Value)
                Else
                    AutoGen = CDate(RsCompany.Fields("Start_Date").Value)
                End If
            Else
                AutoGen = CDate(RsCompany.Fields("Start_Date").Value)
            End If
        End With
        AutoGenFYDateFrom = AutoGen
        RsAutoGen.Close()
        RsAutoGen = Nothing
        Exit Function
AutogenErr:
        MsgBox(Err.Description)
    End Function
    Private Function AutoGenFYDateTo(ByRef mCompanyCode As Integer) As Date
        '
        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset = Nothing
        Dim AutoGen As Date
        SqlStr = ""
        SqlStr = "SELECT Max(TO_DATE(END_DATE))  " & " FROM GEN_CMPYRDTL_TRN " & " WHERE Company_Code=" & mCompanyCode & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAutoGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsAutoGen
            If .EOF = False Then
                If Not IsDbNull(.Fields(0).Value) Then
                    AutoGen = DateAdd(Microsoft.VisualBasic.DateInterval.Year, 1, .Fields(0).Value)
                Else
                    AutoGen = CDate(RsCompany.Fields("END_DATE").Value)
                End If
            Else
                AutoGen = CDate(RsCompany.Fields("END_DATE").Value)
            End If
        End With
        AutoGenFYDateTo = AutoGen
        RsAutoGen.Close()
        RsAutoGen = Nothing
        Exit Function
AutogenErr:
        MsgBox(Err.Description)
    End Function
    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        FieldsVarification = True
        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Or Modify To Add a New Company or Modify An Existing Company.")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And (RsNewFYNo.EOF = True) Then
            FieldsVarification = False
            Exit Function
        End If
        Exit Function
err_Renamed:
        FieldsVarification = False
        MsgBox(Err.Description)
    End Function
    Private Sub frmAddFYNo_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        '
        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub TxtCompany_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtCompany.TextChanged
        '
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtCompany_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtCompany.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        '
        KeyAscii = MainClass.UpperCase(KeyAscii, TxtCompany.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtNewFYNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtNewFYNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        '
        On Error GoTo ERR1
        SqlStr = ""
        If Trim(txtNewFYNo.Text) = "" Then GoTo EventExitSub
        If MODIFYMode = True And RsNewFYNo.EOF = False Then xNewFYNo = RsNewFYNo.Fields("FYNO").Value
        SqlStr = "SELECT * FROM GEN_CMPYRDTL_TRN " & " WHERE COMPANY_CODE=" & Val(lblCCode.Text) & " " & " AND FYEAR=" & Val(txtNewFYNo.Text) & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsNewFYNo, ADODB.LockTypeEnum.adLockReadOnly)
        If RsNewFYNo.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("FYNO Does Not Exist In Master" & vbCrLf & "Click Add For New.")
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = ""
                SqlStr = "SELECT * FROM GEN_CMPYRDTL_TRN " & " WHERE COMPANY_CODE=" & Val(lblCCode.Text) & " " & " AND FYEAR=" & xNewFYNo & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsNewFYNo, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
