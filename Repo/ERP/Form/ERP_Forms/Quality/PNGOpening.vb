Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmPNGOpening
    Inherits System.Windows.Forms.Form
    Dim RsPowerOpen As ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean


    Private Sub cboMeterNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboMeterNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboMeterNo_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboMeterNo.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
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
            If RsPowerOpen.EOF = False Then RsPowerOpen.MoveFirst()
            Show1()
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
        'Resume
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        On Error Resume Next
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart

        If txtOpenDate.Text = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        If Not RsPowerOpen.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "MAN_PNG_OPEN", (txtOpenDate.Text), RsPowerOpen) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM MAN_PNG_OPEN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND OPEN_DATE=TO_DATE('" & VB6.Format(txtOpenDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')")
                PubDBCn.CommitTrans()
                RsPowerOpen.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Errors.Clear()
        RsPowerOpen.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsPowerOpen, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
            txtOpenDate_Validating(txtOpenDate, New System.ComponentModel.CancelEventArgs(False))
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

        Dim mMeterCode As Double

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()


        If MainClass.ValidateWithMasterTable(Trim(cboMeterNo.Text), "METER_NAME", "METER_CODE", "MAN_PNG_METER_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mMeterCode = Trim(MasterNo)
        End If

        SqlStr = ""
        If ADDMode = True Then
            lblMkey.Text = txtOpenDate.Text
            SqlStr = " INSERT INTO MAN_PNG_OPEN " & vbCrLf _
                            & " (COMPANY_CODE, OPEN_DATE, " & vbCrLf _
                            & " OPEN_READING, " & vbCrLf _
                            & " ADDUSER,ADDDATE,MODUSER,MODDATE, " & vbCrLf _
                            & " METER_CODE) " & vbCrLf _
                            & " VALUES ( " & vbCrLf _
                            & " " & RsCompany.Fields("COMPANY_CODE").Value & ",TO_DATE('" & vb6.Format(txtOpenDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " " & Val(txtOpenReading) & ", " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','', " & vbCrLf _
                            & " " & Val(mMeterCode) & ")"

        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE MAN_PNG_OPEN SET " & vbCrLf _
                    & " OPEN_DATE=TO_DATE('" & vb6.Format(txtOpenDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " OPEN_READING=" & Val(txtOpenReading.Text) & ", " & vbCrLf _
                    & " METER_CODE=" & Val(mMeterCode) & "," & vbCrLf _
                    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                    & " MODDATE=TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND OPEN_DATE=TO_DATE('" & vb6.Format(txtOpenDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        PubDBCn.Execute(SqlStr)
        Update1 = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsPowerOpen.Requery()
        MsgBox(Err.Description)
        '    Resume
    End Function

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub

    Private Sub ViewGrid()

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            Call AssignGrid(True)
            '        ADataGrid.Refresh
            FormatSprdView()
            SprdView.Refresh()
            SprdView.Focus()
            SprdView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            SprdView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsPowerOpen, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmPNGOpening_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "PNG Opening Record"

        SqlStr = "Select * From MAN_PNG_OPEN WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPowerOpen, ADODB.LockTypeEnum.adLockReadOnly)

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

        SqlStr = " SELECT METER_CODE," & vbCrLf & " TO_CHAR(OPEN_DATE,'DD/MM/YYYY') AS OPEN_DATE, " & vbCrLf & " OPEN_READING " & vbCrLf & " FROM MAN_PNG_OPEN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " ORDER BY OPEN_DATE"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmPNGOpening_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmPNGOpening_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim Rs As ADODB.Recordset

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Left = 0
        Me.Top = 0
        Me.Height = VB6.TwipsToPixelsY(3450)
        Me.Width = VB6.TwipsToPixelsX(9285)


        cboMeterNo.Items.Clear()

        SqlStr = "SELECT METER_NAME FROM MAN_PNG_METER_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY METER_NAME"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, Rs, ADODB.LockTypeEnum.adLockReadOnly)

        If Rs.EOF = False Then
            Do While Rs.EOF = False
                cboMeterNo.Items.Add(Rs.Fields("METER_NAME").Value)
                Rs.MoveNext()
            Loop
        End If

        cboMeterNo.SelectedIndex = -1

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
        txtOpenDate.Text = ""
        txtOpenDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtOpenReading.Text = ""


        cboMeterNo.SelectedIndex = -1
        cboMeterNo.Enabled = True


        Call MakeEnableDesableField(True)
        MainClass.ButtonStatus(Me, XRIGHT, RsPowerOpen, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ClearErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub

    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 600)
            .set_ColWidth(0, 500 * 1)
            .set_ColWidth(1, 500 * 2)
            .set_ColWidth(2, 500 * 4)
            .set_ColWidth(3, 500 * 4)
            .set_ColWidth(4, 500 * 2)
            .set_ColWidth(5, 500 * 2)
            .set_ColWidth(6, 500 * 2)
            .set_ColWidth(7, 500 * 2)
            '        .ColWidth(8) = 500 * 2
            '        .ColWidth(9) = 500 * 4
            '        .ColWidth(10) = 500 * 4
            .ColsFrozen = 2
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub

    Private Sub SetTextLengths()
        On Error GoTo ERR1

        txtOpenDate.Maxlength = RsPowerOpen.Fields("OPEN_DATE").DefinedSize - 6
        txtOpenReading.Maxlength = RsPowerOpen.Fields("OPEN_READING").Precision
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub

    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim ii As Integer
        Dim mMeterCode As Double

        FieldsVarification = True

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsPowerOpen.EOF = True Then Exit Function

        If Trim(txtOpenDate.Text) = "" Then
            MsgInformation("As on Date is empty, So unable to save.")
            txtOpenDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(cboMeterNo.Text) = "" Then
            MsgBox("Meter No is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If cboMeterNo.Enabled = True Then cboMeterNo.Focus()
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboMeterNo.Text), "METER_NAME", "METER_CODE", "MAN_PNG_METER_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mMeterCode = Trim(MasterNo)
        Else
            MsgInformation("Please select Meter No So unable to save.")
            If cboMeterNo.Enabled = True Then cboMeterNo.Focus()
            FieldsVarification = False
            Exit Function
        End If


        If Trim(txtOpenReading.Text) = "" Then
            MsgInformation("Reading is empty, So unable to save.")
            txtOpenReading.Focus()
            FieldsVarification = False
            Exit Function
        End If


        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        ''Resume
    End Function

    Private Sub frmPNGOpening_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        RsPowerOpen.Close()
        RsPowerOpen = Nothing
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 2
        SprdView.Row = SprdView.ActiveRow
        txtOpenDate.Text = SprdView.Text
        txtOpenDate_Validating(txtOpenDate, New System.ComponentModel.CancelEventArgs(False))
        Call CmdView_Click(CmdView, New System.EventArgs())
    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub
    Private Sub txtOpenReading_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOpenReading.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOpenReading_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOpenReading.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtOpenDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOpenDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOpenDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtOpenDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMKey As Date
        Dim mDate As Date
        Dim SqlStr As String

        If Trim(txtOpenDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtOpenDate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
            GoTo EventExitSub
        End If
        mDate = CDate(VB6.Format(txtOpenDate.Text, "DD/MM/YYYY"))

        If MODIFYMode = True And RsPowerOpen.BOF = False Then xMKey = RsPowerOpen.Fields("OPEN_DATE").Value

        SqlStr = "SELECT * FROM MAN_PNG_OPEN " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND OPEN_DATE=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPowerOpen, ADODB.LockTypeEnum.adLockReadOnly)
        If RsPowerOpen.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Date. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM MAN_PNG_OPEN " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND OPEN_DATE=TO_DATE('" & VB6.Format(xMKey, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPowerOpen, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim mMeterCode As Double
        Dim mMeterDesc As String

        If Not RsPowerOpen.EOF Then
            lblMkey.Text = IIf(IsDbNull(RsPowerOpen.Fields("OPEN_DATE").Value), "", RsPowerOpen.Fields("OPEN_DATE").Value)
            txtOpenDate.Text = IIf(IsDbNull(RsPowerOpen.Fields("OPEN_DATE").Value), "", RsPowerOpen.Fields("OPEN_DATE").Value)
            txtOpenReading.Text = IIf(IsDbNull(RsPowerOpen.Fields("OPEN_READING").Value), "", RsPowerOpen.Fields("OPEN_READING").Value)

            mMeterCode = IIf(IsDbNull(RsPowerOpen.Fields("METER_CODE").Value), -1, RsPowerOpen.Fields("METER_CODE").Value)

            If MainClass.ValidateWithMasterTable(mMeterCode, "METER_CODE", "METER_NAME", "MAN_PNG_METER_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mMeterDesc = Trim(MasterNo)
                cboMeterNo.Text = mMeterDesc
            End If
            cboMeterNo.Enabled = False

            Call MakeEnableDesableField(False)
        End If
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsPowerOpen, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub

    Private Sub MakeEnableDesableField(ByRef mMode As Boolean)

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
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        '    Call ReportOnPNGOpening(crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        '    Call ReportOnPNGOpening(crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
End Class
