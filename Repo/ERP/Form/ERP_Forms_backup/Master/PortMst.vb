Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility

Imports System
Imports System.Net
Imports System.Text
Imports System.IO
Imports System.Web
Friend Class frmPortMst
    Inherits System.Windows.Forms.Form
    Dim RsPort As ADODB.Recordset
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
        MainClass.ButtonStatus(Me, XRIGHT, RsPort, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ErrorPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Clear1()

        txtStateName.Text = ""
        txtCityName.Text = ""
        txtCountryCode.Text = ""

        txtPortCode.Text = ""
        txtPortAdd1.Text = ""
        txtPortAdd2.Text = ""
        txtPinCode.Text = ""

        txtCountryCode.Enabled = False
        Call AutoCompleteSearch("GEN_PORT_MST", "PORT_CODE", "", txtCityName)
        Call AutoCompleteSearch("GEN_CITY_MST", "CITY_NAME", "", txtCityName)
        Call AutoCompleteSearch("GEN_STATE_MST", "NAME", "", txtStateName)
        Call AutoCompleteSearch("GEN_COUNTRY_MST", "COUNTRY_NAME", "", txtCountryCode)

        MainClass.ButtonStatus(Me, XRIGHT, RsPort, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub CmdModify_Click(sender As Object, e As System.EventArgs) Handles CmdModify.Click
        On Error GoTo ModifyErr
        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsPort, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
        If InsertIntoDelAudit(PubDBCn, "GEN_PORT_MST", (txtPortCode.Text), RsPort, "PORT_CODE") = False Then GoTo DeleteErr
        If InsertIntoDeleteTrn(PubDBCn, "GEN_PORT_MST", "PORT_CODE", (txtPortCode.Text)) = False Then GoTo DeleteErr

        SqlStr = "DELETE FROM GEN_PORT_MST " & vbCrLf _
            & "WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND PORT_CODE='" & MainClass.AllowSingleQuote(UCase((txtPortCode.Text))) & "'"

        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        RsPort.Requery() ''.Refresh		
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans() ''		
        RsPort.Requery() ''.Refresh		
        If Err.Number = -2147467259 Then
            MsgBox("Can't Delete, Transactions Exists Against This Expense Head.", MsgBoxStyle.Information)
            Exit Function
        End If
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Sub CmdDelete_Click(sender As Object, e As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If txtCityName.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub

        'If CheckConsolidatedMaster("FIN_SUPP_CUST_MST") = True Then
        '    MsgInformation("Cann't be Delete.")
        '    Exit Sub
        'End If

        If Not RsPort.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.		
                If Delete1() = False Then GoTo DelErrPart
                If RsPort.EOF = True Then
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
        txtPortCode.Text = Trim(SprdView.Text)
        txtPortCode_Validating(txtPortCode, New System.ComponentModel.CancelEventArgs(False))
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
    Private Sub txtPortCode_Validating(sender As Object, EventArgs As System.ComponentModel.CancelEventArgs) Handles txtPortCode.Validating
        Dim Cancel As Boolean = EventArgs.Cancel

        On Error GoTo ERR1
        SqlStr = ""
        If Trim(txtPortCode.Text) = "" Then GoTo EventExitSub
        If MODIFYMode = True And RsPort.EOF = False Then xCode = RsPort.Fields("PORT_CODE").Value

        SqlStr = "SELECT * FROM GEN_PORT_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND PORT_CODE='" & MainClass.AllowSingleQuote(UCase((Trim(txtPortCode.Text)))) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPort, ADODB.LockTypeEnum.adLockReadOnly)

        If RsPort.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Port Code Does Not Exist In Master" & vbCrLf & "Click Add To Add In Master")
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = ""
                SqlStr = "SELECT * FROM GEN_PORT_MST " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND PORT_CODE='" & xCode & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPort, ADODB.LockTypeEnum.adLockReadOnly)
            End If
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
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = ""
        If Trim(txtCityName.Text) = "" Then GoTo EventExitSub

        SqlStr = "SELECT * FROM GEN_CITY_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND CITY_NAME='" & MainClass.AllowSingleQuote(UCase((Trim(txtCityName.Text)))) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = True Then
            MsgBox("City Name Does Not Exist In Master.")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        EventArgs.Cancel = Cancel
    End Sub
    Private Sub frmPortMst_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        MainClass.UOpenRecordSet("Select * From GEN_PORT_MST Where 1<>1 ", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPort, ADODB.LockTypeEnum.adLockReadOnly)

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

    Private Sub frmPortMst_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsPort = Nothing
        RsPort.Close()
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim mCountryCode As String
        Dim mCountryName As String = ""

        Dim mStateCode As String
        Dim mStateName As String = ""

        If Not RsPort.EOF Then





            txtPortCode.Text = IIf(IsDBNull(RsPort.Fields("PORT_CODE").Value), "", RsPort.Fields("PORT_CODE").Value)
            txtPortAdd1.Text = IIf(IsDBNull(RsPort.Fields("PORT_ADDRESS_1").Value), "", RsPort.Fields("PORT_ADDRESS_1").Value)
            txtPortAdd2.Text = IIf(IsDBNull(RsPort.Fields("PORT_ADDRESS_2").Value), "", RsPort.Fields("PORT_ADDRESS_2").Value)
            txtPinCode.Text = IIf(IsDBNull(RsPort.Fields("PORT_PINCODE").Value), "", RsPort.Fields("PORT_PINCODE").Value)

            txtCityName.Text = IIf(IsDBNull(RsPort.Fields("PORT_CITY").Value), "", RsPort.Fields("PORT_CITY").Value)

            mStateCode = IIf(IsDBNull(RsPort.Fields("PORT_STATE_CODE").Value), "", RsPort.Fields("PORT_STATE_CODE").Value)

            If MainClass.ValidateWithMasterTable(mStateCode, "CODE", "NAME", "GEN_STATE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mStateName = MasterNo
            End If

            txtStateName.Text = mStateName


            mCountryCode = IIf(IsDBNull(RsPort.Fields("PORT_COUNTRY_CODE").Value), "", RsPort.Fields("PORT_COUNTRY_CODE").Value)
            mCountryName = ""

            If MainClass.ValidateWithMasterTable(mCountryCode, "COUNTRY_CODE", "COUNTRY_NAME", "GEN_COUNTRY_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mCountryName = MasterNo
            End If

            txtCountryCode.Text = mCountryName

            xCode = RsPort.Fields("PORT_CODE").Value
        End If
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsPort, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
        'Resume
    End Sub

    Private Sub frmPortMst_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub



    Private Sub frmPortMst_Load(sender As Object, e As System.EventArgs) Handles Me.Load
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
            txtPortCode_Validating(txtCityName, New System.ComponentModel.CancelEventArgs(False))
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
                    SqlStr = "INSERT INTO GEN_PORT_MST (" & vbCrLf _
                        & " COMPANY_CODE, PORT_CODE, PORT_ADDRESS_1, PORT_ADDRESS_2, PORT_PINCODE, " & vbCrLf _
                        & " PORT_CITY, PORT_STATE_CODE, PORT_COUNTRY_CODE, ADDUSER, ADDDATE) VALUES ( " & vbCrLf _
                        & " " & xCompanyCode & ", " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(txtPortCode.Text) & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(txtPortAdd1.Text) & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(txtPortAdd2.Text) & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(txtPinCode.Text) & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(txtCityName.Text) & "', " & vbCrLf _
                        & " '" & mStateCode & "', " & vbCrLf _
                        & " '" & mCountryCode & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(PubUserID) & "', TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'))"


                Else
                    SqlStr = " UPDATE GEN_PORT_MST  SET " & vbCrLf _
                        & " PORT_CODE='" & MainClass.AllowSingleQuote(txtPortCode.Text) & "', " & vbCrLf _
                        & " PORT_ADDRESS_1='" & MainClass.AllowSingleQuote(txtPortAdd1.Text) & "', " & vbCrLf _
                        & " PORT_ADDRESS_2='" & MainClass.AllowSingleQuote(txtPortAdd2.Text) & "', " & vbCrLf _
                        & " PORT_PINCODE='" & MainClass.AllowSingleQuote(txtPinCode.Text) & "', " & vbCrLf _
                        & " PORT_CITY='" & MainClass.AllowSingleQuote(txtCityName.Text) & "', " & vbCrLf _
                        & " PORT_STATE_CODE='" & mStateCode & "', " & vbCrLf _
                        & " PORT_COUNTRY_CODE='" & MainClass.AllowSingleQuote(mCountryCode) & "', " & vbCrLf _
                        & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                        & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
                        & " WHERE COMPANY_CODE=" & xCompanyCode & "" & vbCrLf _
                        & " AND PORT_CODE= '" & xCode & "'"
                End If
UpdatePart:
                PubDBCn.Execute(SqlStr)
                RsTemp.MoveNext()
            Loop
        End If
        PubDBCn.CommitTrans()
        'Call WhatupSMS()
        Update1 = True
        Exit Function
UpdateError:
        Update1 = False
        PubDBCn.RollbackTrans() ''		
        RsPort.Requery() ''.Refresh		
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Sub SetTextLengths()

        On Error GoTo ERR1

        txtCityName.MaxLength = RsPort.Fields("PORT_CITY").DefinedSize

        txtPortCode.MaxLength = RsPort.Fields("PORT_CODE").DefinedSize
        txtPortAdd1.MaxLength = RsPort.Fields("PORT_ADDRESS_1").DefinedSize
        txtPortAdd2.MaxLength = RsPort.Fields("PORT_ADDRESS_2").DefinedSize
        txtPinCode.MaxLength = RsPort.Fields("PORT_PINCODE").DefinedSize



        txtStateName.MaxLength = MainClass.SetMaxLength("NAME", "GEN_STATE_MST", PubDBCn)

        txtCountryCode.MaxLength = MainClass.SetMaxLength("COUNTRY_NAME", "GEN_COUNTRY_MST", PubDBCn)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed


        FieldsVarification = True


        If Trim(txtPortCode.Text) = "" Then
            MsgInformation("Port Code is empty. Cannot Save")
            txtPortCode.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtPortAdd1.Text) = "" Then
            MsgInformation("Address 1 is empty. Cannot Save")
            txtPortAdd1.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtPortAdd2.Text) = "" Then
            MsgInformation("Address 2 is empty. Cannot Save")
            txtPortAdd2.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtPinCode.Text) = "" Then
            MsgInformation("Pincode is empty. Cannot Save")
            txtPinCode.Focus()
            FieldsVarification = False
            Exit Function
        End If

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
        If MODIFYMode = True And RsPort.EOF = True Then Exit Function
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


        SqlStr = " SELECT A.PORT_CODE, PORT_ADDRESS_1, PORT_ADDRESS_2, PORT_PINCODE, A.PORT_CITY, B.NAME, C.COUNTRY_NAME " & vbCrLf _
            & " FROM GEN_PORT_MST A, GEN_STATE_MST B, GEN_COUNTRY_MST C" & vbCrLf _
            & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND A.COMPANY_CODE=B.COMPANY_CODE" & vbCrLf _
            & " AND A.PORT_STATE_CODE=B.CODE" & vbCrLf _
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
            .set_ColWidth(1, 12)
            .set_ColWidth(2, 20)
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
        'mTitle = "City Master"
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\PortMst.rpt"
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
    Private Sub txtPortCode_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPortCode.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPortCode.Text)
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPortCode_TextChanged(sender As Object, e As System.EventArgs) Handles txtPortCode.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)

    End Sub
    Private Sub txtPortAdd1_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPortAdd1.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPortAdd1.Text)
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPortAdd1_TextChanged(sender As Object, e As System.EventArgs) Handles txtPortAdd1.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtPortAdd2_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPortAdd2.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPortAdd2.Text)
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPortAdd2_TextChanged(sender As Object, e As System.EventArgs) Handles txtPortAdd2.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPincode_TextChanged(sender As Object, e As System.EventArgs) Handles txtPinCode.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtPincode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPinCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub WhatupSMS()
        'Dim WebRequest As HttpWebRequest
        'Dim instance_id As String = "instance62531"
        'Dim token As String = "ykh33jm55lholmml"
        'Dim mobile_number As String = "+919810932931"
        ''Dim ultramsgApiUrl As String = "https://api.ultramsg.com/" + instance_id + "/messages/chat"

        'Dim ultramsgApiUrl As String = "https://api.ultramsg.com/instance62531/messages/chat?token=ykh33jm55lholmml&to=+919810932931&body=WhatsApp+API+on+UltraMsg.com+works+good&priority=10"

        '''https://api.ultramsg.com/instance62531/
        '''

        'WebRequest = HttpWebRequest.Create(ultramsgApiUrl)
        'Dim postdata As String = "token=" + token + "&to=" + mobile_number + "&body=WhatsApp API on UltraMsg.com works good"
        'Dim enc As UTF8Encoding = New System.Text.UTF8Encoding()
        'Dim postdatabytes As Byte() = enc.GetBytes(postdata)
        'WebRequest.Method = "POST"
        'WebRequest.ContentType = "application/x-www-form-urlencoded"
        ''WebRequest.GetRequestStream().Write(postdatabytes)
        'WebRequest.GetRequestStream().Write(postdatabytes, 0, postdatabytes.Length)
        'Dim ret As New System.IO.StreamReader(WebRequest.GetResponse().GetResponseStream())
        'Console.WriteLine(ret.ReadToEnd())

        Dim WebRequest As HttpWebRequest
        WebRequest = HttpWebRequest.Create("https://api.ultramsg.com/instance62531/messages/chat")
        Dim postdata As String = "token=ykh33jm55lholmml&to=+919810932931&body=WhatsApp API on UltraMsg.com works good"
        Dim enc As UTF8Encoding = New System.Text.UTF8Encoding()
        Dim postdatabytes As Byte() = enc.GetBytes(postdata)
        WebRequest.Method = "POST"
        WebRequest.ContentType = "application/x-www-form-urlencoded"
        'WebRequest.GetRequestStream().Write(postdatabytes)
        WebRequest.GetRequestStream().Write(postdatabytes, 0, postdatabytes.Length)
        Dim ret As New System.IO.StreamReader(WebRequest.GetResponse().GetResponseStream())
        Console.WriteLine(ret.ReadToEnd())

    End Sub
End Class
