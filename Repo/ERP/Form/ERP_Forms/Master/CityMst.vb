Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmCityMst
    Inherits System.Windows.Forms.Form
    Dim RsCity As ADODB.Recordset
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Private PvtDBCn As ADODB.Connection	

    Dim xCode As String
    Dim FormActive As Boolean
    Dim Shw As Boolean
    Dim MasterNo As Object
    Dim SqlStr As String = ""
    Private Sub ViewGrid()

        On Error GoTo ErrorPart
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
        MainClass.ButtonStatus(Me, XRIGHT, RsCity, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ErrorPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Clear1()

        txtStateName.Text = ""
        txtCityName.Text = ""
        txtCountryCode.Text = ""
        txtCountryCode.Enabled = False
        Call AutoCompleteSearch("GEN_CITY_MST", "CITY_NAME", "", txtCityName)
        Call AutoCompleteSearch("GEN_STATE_MST", "NAME", "", txtStateName)
        Call AutoCompleteSearch("GEN_COUNTRY_MST", "COUNTRY_NAME", "", txtCountryCode)

        MainClass.ButtonStatus(Me, XRIGHT, RsCity, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub CmdModify_Click(sender As Object, e As System.EventArgs) Handles CmdModify.Click
        On Error GoTo ModifyErr
        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsCity, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdPreview_Click(sender As Object, e As System.EventArgs) Handles CmdPreview.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        ShowReport(Crystal.DestinationConstants.crptToWindow)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(sender As Object, e As System.EventArgs) Handles cmdPrint.Click
        Me.Cursor = System.Windows.Forms.Cursors.Arrow
        ShowReport(Crystal.DestinationConstants.crptToPrinter)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub CmdView_Click(sender As Object, e As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub

    Private Sub CmdClose_Click(sender As Object, e As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub
    Private Function Delete1() As Boolean
        On Error GoTo DeleteErr
        SqlStr = ""

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        If InsertIntoDelAudit(PubDBCn, "GEN_CITY_MST", (txtCityName.Text), RsCity, "CITY_NAME") = False Then GoTo DeleteErr
        If InsertIntoDeleteTrn(PubDBCn, "GEN_CITY_MST", "CITY_NAME", (txtCityName.Text)) = False Then GoTo DeleteErr

        SqlStr = "DELETE FROM GEN_CITY_MST " & vbCrLf _
            & "WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & "AND CITY_NAME='" & MainClass.AllowSingleQuote(UCase((txtCityName.Text))) & "'"

        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        RsCity.Requery() ''.Refresh		
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans() ''		
        RsCity.Requery() ''.Refresh		
        If Err.Number = -2147467259 Then
            MsgBox("Can't Delete, Transactions Exists Against This Expense Head.", MsgBoxStyle.Information)
            Exit Function
        End If
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Sub CmdDelete_Click(sender As Object, e As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If txtCityName.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub

        If CheckConsolidatedMaster("FIN_SUPP_CUST_MST") = True Then
            MsgInformation("Cann't be Delete.")
            Exit Sub
        End If

        If Not RsCity.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.		
                If Delete1() = False Then GoTo DelErrPart
                If RsCity.EOF = True Then
                    Clear1()
                Else
                    Show1()
                End If
            End If
        End If
        Exit Sub
DelErrPart:
        ErrorMsg("Record Not Deleted", "DELETE", MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdView_DblClick(sender As Object, e As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
        txtCityName.Text = Trim(SprdView.Text)
        txtCityName_Validating(txtCityName, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub

    Private Sub SprdView_KeyPressEvent(sender As Object, EventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If EventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Private Sub txtCountryCode_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCountryCode.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCountryCode.Text)
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCountryCode_TextChanged(sender As Object, e As System.EventArgs) Handles txtCountryCode.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCountryCode_Validating(sender As Object, EventArgs As System.ComponentModel.CancelEventArgs) Handles txtCountryCode.Validating
        Dim Cancel As Boolean = EventArgs.Cancel

        If txtCountryCode.Text = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtCountryCode.Text, "COUNTRY_NAME", "COUNTRY_CODE", "GEN_COUNTRY_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invalid Country Name. Cannot Save")
            txtCountryCode.Focus()
            Cancel = False
            Exit Sub
        End If
EventExitSub:
        EventArgs.Cancel = Cancel
    End Sub

    Private Sub txtStateName_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtStateName.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtStateName.Text)
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub

    Private Sub txtStateName_TextChanged(sender As Object, e As System.EventArgs) Handles txtStateName.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtStateName_Validating(sender As Object, EventArgs As System.ComponentModel.CancelEventArgs) Handles txtStateName.Validating
        Dim Cancel As Boolean = EventArgs.Cancel

        On Error GoTo ERR1
        Dim mCountryCode As String = ""
        Dim mCountryName As String = ""

        SqlStr = ""
        If txtStateName.Text = "" Then Exit Sub

        If MainClass.ValidateWithMasterTable(txtStateName.Text, "NAME", "COUNTRY_CODE", "GEN_STATE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invalid State Name. Cannot Save")
            txtStateName.Focus()
            Cancel = False
            Exit Sub
        Else
            mCountryCode = MasterNo

            If MainClass.ValidateWithMasterTable(mCountryCode, "COUNTRY_CODE", "COUNTRY_NAME", "GEN_COUNTRY_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mCountryName = MasterNo
            End If

            txtCountryCode.Text = mCountryName
        End If


        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        EventArgs.Cancel = Cancel
    End Sub


    Private Sub txtCityName_Validating(sender As Object, EventArgs As System.ComponentModel.CancelEventArgs) Handles txtCityName.Validating
        Dim Cancel As Boolean = EventArgs.Cancel

        On Error GoTo ERR1
        SqlStr = ""
        If Trim(txtCityName.Text) = "" Then GoTo EventExitSub
        If MODIFYMode = True And RsCity.EOF = False Then xCode = RsCity.Fields("CITY_NAME").Value

        SqlStr = "SELECT * FROM GEN_CITY_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND CITY_NAME='" & MainClass.AllowSingleQuote(UCase((Trim(txtCityName.Text)))) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCity, ADODB.LockTypeEnum.adLockReadOnly)

        If RsCity.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("City Name Does Not Exist In Master" & vbCrLf & "Click Add To Add In Master")
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = ""
                SqlStr = "SELECT * FROM GEN_CITY_MST " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND CITY_NAME='" & xCode & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCity, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        EventArgs.Cancel = Cancel
    End Sub
    Private Sub frmCityMst_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        MainClass.UOpenRecordSet("Select * From GEN_CITY_MST Where 1<>1 ", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCity, ADODB.LockTypeEnum.adLockReadOnly)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        AssignGrid(False)
        SetTextLengths()
        Clear1()
        If CmdAdd.Enabled = True Then CmdAdd_Click(CmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmCityMst_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsCity = Nothing
        RsCity.Close()
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim mCountryCode As String
        Dim mCountryName As String = ""

        Dim mStateCode As String
        Dim mStateName As String = ""

        If Not RsCity.EOF Then

            txtCityName.Text = IIf(IsDBNull(RsCity.Fields("CITY_NAME").Value), "", RsCity.Fields("CITY_NAME").Value)

            mStateCode = IIf(IsDBNull(RsCity.Fields("STATE_CODE").Value), "", RsCity.Fields("STATE_CODE").Value)

            If MainClass.ValidateWithMasterTable(mStateCode, "CODE", "NAME", "GEN_STATE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mStateName = MasterNo
            End If

            txtStateName.Text = mStateName


            mCountryCode = IIf(IsDBNull(RsCity.Fields("COUNTRY_CODE").Value), "", RsCity.Fields("COUNTRY_CODE").Value)
            mCountryName = ""

            If MainClass.ValidateWithMasterTable(mCountryCode, "COUNTRY_CODE", "COUNTRY_NAME", "GEN_COUNTRY_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mCountryName = MasterNo
            End If

            txtCountryCode.Text = mCountryName

            xCode = RsCity.Fields("CITY_NAME").Value
        End If
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsCity, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
        'Resume
    End Sub

    Private Sub frmCityMst_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub



    Private Sub frmCityMst_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        ''Set PvtDBCn = New ADODB.Connection		
        ''PvtDBCn.Open StrConn		

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CmdView.Text = ConCmdGridViewCaption
        ADDMode = False
        MODIFYMode = False
        Call SetMainFormCordinate(Me)
        'Me.Left = 0
        'Me.Top = 0
        'Me.Height = VB6.TwipsToPixelsY(5220)
        ''Me.Width = VB6.TwipsToPixelsX(8265)
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub CmdSave_Click(sender As Object, e As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        If Update1() = True Then
            ADDMode = False
            MODIFYMode = False
            txtCityName_Validating(txtCityName, New System.ComponentModel.CancelEventArgs(False))
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
        Dim mCode As Integer

        Dim mStateCode As String = ""
        Dim mCountryCode As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xCompanyCode As Long

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If CheckConsolidatedMaster("FIN_SUPP_CUST_MST") = True Then
            SqlStr = "SELECT COMPANY_CODE FROM GEN_COMPANY_MST"
        Else
            SqlStr = "SELECT COMPANY_CODE FROM GEN_COMPANY_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                xCompanyCode = RsTemp.Fields("COMPANY_CODE").Value
                If MainClass.ValidateWithMasterTable(txtStateName.Text, "NAME", "CODE", "GEN_STATE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCompanyCode & "") = True Then
                    mStateCode = MasterNo
                End If

                If MainClass.ValidateWithMasterTable(txtCountryCode.Text, "COUNTRY_NAME", "COUNTRY_CODE", "GEN_COUNTRY_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCompanyCode & "") = True Then
                    mCountryCode = MasterNo
                End If

                SqlStr = ""
                If ADDMode = True Then
                    SqlStr = "INSERT INTO GEN_CITY_MST (" & vbCrLf & " COMPANY_CODE, CITY_NAME, STATE_CODE, COUNTRY_CODE, ADDUSER, ADDDATE) VALUES ( " & vbCrLf _
                        & " " & xCompanyCode & ", " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(txtCityName.Text) & "', " & vbCrLf _
                        & " '" & mStateCode & "', " & vbCrLf _
                        & " '" & mCountryCode & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(PubUserID) & "', TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'))"

                Else
                    SqlStr = " UPDATE GEN_CITY_MST  SET " & vbCrLf _
                        & " CITY_NAME='" & MainClass.AllowSingleQuote(txtCityName.Text) & "', " & vbCrLf _
                        & " STATE_CODE='" & mStateCode & "', " & vbCrLf _
                        & " COUNTRY_CODE='" & MainClass.AllowSingleQuote(mCountryCode) & "', " & vbCrLf _
                        & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                        & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
                        & " WHERE COMPANY_CODE=" & xCompanyCode & "" & vbCrLf _
                        & " AND CITY_NAME= '" & xCode & "'"
                End If
UpdatePart:
                PubDBCn.Execute(SqlStr)
                RsTemp.MoveNext()
            Loop
        End If
        PubDBCn.CommitTrans()
        Update1 = True
        Exit Function
UpdateError:
        Update1 = False
        PubDBCn.RollbackTrans() ''		
        RsCity.Requery() ''.Refresh		
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Sub SetTextLengths()

        On Error GoTo ERR1

        txtCityName.MaxLength = RsCity.Fields("CITY_NAME").DefinedSize

        txtStateName.MaxLength = MainClass.SetMaxLength("NAME", "GEN_STATE_MST", PubDBCn)

        txtCountryCode.MaxLength = MainClass.SetMaxLength("COUNTRY_NAME", "GEN_COUNTRY_MST", PubDBCn)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed


        FieldsVarification = True

        If Trim(txtCityName.Text) = "" Then
            MsgInformation("City Name is empty. Cannot Save")
            txtCityName.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtStateName.Text) = "" Then
            MsgInformation(" Name is empty. Cannot Save")
            txtStateName.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtCountryCode.Text) = "" Then
            MsgInformation("Country Name is empty. Cannot Save")
            'txtCountryCode.Focus()
            FieldsVarification = False
            Exit Function
        End If


        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
        End If
        If MODIFYMode = True And RsCity.EOF = True Then Exit Function
        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function
    Private Sub TxtStateName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtStateName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        Dim SqlStr As String = ""

        SqlStr = ""

        SqlStr = " SELECT A.CITY_NAME, B.NAME, C.COUNTRY_NAME " & vbCrLf _
            & " FROM GEN_CITY_MST A, GEN_STATE_MST B, GEN_COUNTRY_MST C" & vbCrLf _
            & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND A.COMPANY_CODE=B.COMPANY_CODE" & vbCrLf _
            & " AND A.STATE_CODE=B.CODE" & vbCrLf _
            & " AND B.COMPANY_CODE=C.COMPANY_CODE" & vbCrLf _
            & " AND B.COUNTRY_CODE=C.COUNTRY_CODE"


        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
    End Sub

    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 12)
            .set_ColWidth(0, 5)
            .set_ColWidth(1, 30)
            .set_ColWidth(2, 12)
            .set_ColWidth(3, 12)
            .set_ColWidth(4, 12)
            .set_ColWidth(5, 12)
            .ColsFrozen = 1
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub ShowReport(ByRef Mode As Crystal.DestinationConstants)
        Dim mTitle As String = ""
        On Error GoTo ERR1
        mTitle = ""
        Report1.Reset()
        mTitle = "City Master"
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\CityMst.rpt"
        SetCrpt(Report1, Mode, 1, mTitle)

        Report1.WindowShowGroupTree = False

        Report1.Action = 1
        Report1.ReportFileName = ""

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub txtCityName_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCityName.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCityName.Text)
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCityName_TextChanged(sender As Object, e As System.EventArgs) Handles txtCityName.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub CmdAdd_Click(sender As Object, e As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo ERR1
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtCityName.Focus()
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
End Class
