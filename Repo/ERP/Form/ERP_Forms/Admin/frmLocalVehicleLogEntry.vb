Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmLocalVehicleLogEntry
    Inherits System.Windows.Forms.Form
    Dim RsLogEntry As ADODB.Recordset

    'Dim PvtDBCn As ADODB.Connection

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Dim FormActive As Boolean

    Dim CurMKey As String
    Dim SqlStr As String = ""

    Private Const ConRowHeight As Short = 15


    Dim pMenu As String
    Private Sub chkStatus_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkStatus.CheckStateChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtSlipNo.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            If RsLogEntry.EOF = False Then RsLogEntry.MoveFirst()
            Show1()
            '        ShowInvoiceData
            txtSlipNo.Enabled = True
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
        'Resume
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If ValidateBranchLocking((txtSlipDate.Text)) = True Then
            Exit Sub
        End If

        If txtSlipNo.Text = "" Then MsgInformation("Nothing to Delete") : Exit Sub

        If chkStatus.CheckState = System.Windows.Forms.CheckState.Checked Then MsgInformation("Closed Slip Cann't be Delete.") : Exit Sub

        If Not RsLogEntry.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Cancel()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "DSP_VEHICLE_LOG_ENTRY", (txtSlipNo.Text), RsLogEntry) = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "DSP_VEHICLE_LOG_ENTRY", "AUTO_KEY_SLIP", (txtSlipNo.Text)) = False Then GoTo DelErrPart

                PubDBCn.Execute("DELETE FROM DSP_VEHICLE_LOG_ENTRY WHERE AUTO_KEY_SLIP=" & Val(txtSlipNo.Text) & "")

                PubDBCn.CommitTrans()
                RsLogEntry.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Cancel()
        RsLogEntry.Requery()
        MsgBox(Err.Description)
    End Sub
    Private Sub cmdLoaction_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdLoaction.Click
        SearchTransporterMaster()
    End Sub
    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr
        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsLogEntry, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            txtSlipNo.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String)
        On Error GoTo ErrPart

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle, False, pMenu)
        Report1.ReportFileName = PubReportFolderPath & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False

        Report1.Action = 1
        Report1.Reset()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub ShowTermsReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.ReportFileName = My.Application.Info.DirectoryPath & mRPTName
        'Report1.SQLQuery = mSqlStr
        'Report1.WindowShowGroupTree = False
        Report1.Action = 1

    End Sub

    Private Function PendingCollection(ByRef pRefNo As Double, ByRef pLoadingNo As Double) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim I As Integer

        PendingCollection = False
        pLoadingNo = 0

        SqlStr = " SELECT DISTINCT IH.AUTO_KEY_SLIP" & vbCrLf _
            & " FROM DSP_VEHICLE_LOG_ENTRY IH" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        SqlStr = SqlStr & vbCrLf _
            & " AND IH.VEHICLE_NO = '" & txtVehicleNo.Text & "'"


        SqlStr = SqlStr & vbCrLf _
            & " AND (IH.IN_TIME IS NULL OR IH.IN_TIME='')"

        If pRefNo <> 0 Then
            SqlStr = SqlStr & vbCrLf _
                & " AND IH.AUTO_KEY_SLIP<>" & Val(CStr(pRefNo)) & ""
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


        If RsTemp.EOF = False Then
            pLoadingNo = IIf(IsDBNull(RsTemp.Fields("AUTO_KEY_SLIP").Value), 0, RsTemp.Fields("AUTO_KEY_SLIP").Value)
            PendingCollection = True
        End If

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportonDespatch(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportonDespatch(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If UpdateMain1() = True Then
            ADDMode = False
            MODIFYMode = False
            txtSlipNo_Validating(txtSlipNo, New System.ComponentModel.CancelEventArgs(False))
            If CmdAdd.Enabled = True And CmdAdd.Visible = True Then CmdAdd.Focus()
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
    Private Sub ReportonDespatch(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""
        Dim SqlStr As String = ""
        Dim mRptFileName As String
        Dim mReportPrint As Boolean

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        SqlStr = ""

        Call MainClass.ClearCRptFormulas(Report1)

        Call SelectQryForDespatch(SqlStr)


        mTitle = "Vehicle Movement Log Book"
        mSubTitle = ""
        mRptFileName = "VehicleMovement.rpt"

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)



        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Function SelectQryForDespatch(ByRef mSqlStr As String) As String

        ''SELECT CLAUSE...

        mSqlStr = " SELECT " & vbCrLf _
            & " IH.*"

        ''FROM CLAUSE...

        mSqlStr = mSqlStr & vbCrLf _
            & " FROM DSP_VEHICLE_LOG_ENTRY IH"

        ''WHERE CLAUSE...
        mSqlStr = mSqlStr & vbCrLf _
            & " WHERE " & vbCrLf _
            & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.AUTO_KEY_SLIP=" & Val(txtSlipNo.Text) & ""

        ''ORDER CLAUSE...
        '
        '    mSqlStr = mSqlStr & vbCrLf & "ORDER BY ID.SERIAL_NO"

        SelectQryForDespatch = mSqlStr
    End Function
    Private Function UpdateMain1() As Boolean
        On Error GoTo ErrPart
        Dim I As Short
        Dim SqlStr As String = ""
        Dim nMkey As String
        Dim mVNoSeq As Double
        Dim mDivisionCode As Double
        Dim mEntryDate As String
        Dim mStatus As String
        Dim mThirdParty As String
        Dim mDriverImpCode As String = ""
        Dim mINTime As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mEntryDate = VB6.Format(PubCurrDate, "DD-MMM-YYYY") & " " & GetServerTime()

        If Val(txtSlipNo.Text) = 0 Then
            mVNoSeq = CDbl(AutoGenSeqNo())
        Else
            mVNoSeq = Val(txtSlipNo.Text)
        End If

        txtSlipNo.Text = CStr(Val(CStr(mVNoSeq)))

        SqlStr = ""

        If chkStatus.CheckState = System.Windows.Forms.CheckState.Checked Then
            mStatus = IIf(chkStatus.CheckState = System.Windows.Forms.CheckState.Checked, "C", "O")
        ElseIf IsDate(txtINTime.Text) = True Then
            mStatus = "C"
        Else
            mStatus = IIf(chkStatus.CheckState = System.Windows.Forms.CheckState.Checked, "C", "O")
        End If

        If Trim(Replace(Replace(Replace(txtINTime.Text, "/", ""), ":", ""), "_", "")) = "" Then
            mINTime = ""
        Else
            mINTime = VB6.Format(txtINTime.Text, "DD-MMM-YYYY HH:MM")
        End If

        If ADDMode = True Then
            mStatus = "O" ''Addmode Status Always Open.

            lblMKey.Text = CStr(mVNoSeq)
            SqlStr = "INSERT INTO DSP_VEHICLE_LOG_ENTRY( " & vbCrLf _
                & " COMPANY_CODE, FYEAR, AUTO_KEY_SLIP, " & vbCrLf _
                & " ENTRY_DATE, VEHICLE_NO, TRANSPORTER_NAME, " & vbCrLf _
                & " OUTTIME_DIESEL, OUTTIME_READINING, INTIME_DIESEL, " & vbCrLf _
                & " INTIME_READINING, FROM_LOCATION, TO_LOCATION, " & vbCrLf _
                & " DRIVER_NAME, INCHARGE_NAME, OUT_TIME, " & vbCrLf _
                & " IN_TIME, REMARKS, TOT_RUNNING, " & vbCrLf _
                & " TOT_DIESEL, ADDUSER, ADDDATE, MODUSER, MODDATE, STATUS " & vbCrLf _
                & " ) "

            SqlStr = SqlStr & vbCrLf _
                & " VALUES ( " & vbCrLf _
                & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf _
                & " " & Val(CStr(mVNoSeq)) & ",  TO_DATE('" & mEntryDate & "','DD-MON-YYYY HH24:MI'), " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtVehicleNo.Text)) & "', '" & MainClass.AllowSingleQuote(txtTransporterName.Text) & "'," & vbCrLf _
                & " " & Val(txtOutTimeDiesel.Text) & "," & Val(txtOutReading.Text) & ", " & Val(txtINTimeDiesel.Text) & "," & Val(txtINReading.Text) & "," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtFromLocation.Text) & "', '" & MainClass.AllowSingleQuote(txtToLocation.Text) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtMainDriver.Text) & "', '" & MainClass.AllowSingleQuote(txtIncharge.Text) & "', " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtOutTime.Text, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI'), TO_DATE('" & VB6.Format(mINTime, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI')," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & Val(txtINTime.Text) - Val(txtOutReading.Text) & ", " & vbCrLf _
                & " " & Val(txtINTimeDiesel.Text) - Val(txtOutTimeDiesel.Text) & ", " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'','','" & mStatus & "'" & vbCrLf _
                & " )"


        ElseIf MODIFYMode = True Then

            SqlStr = ""
            SqlStr = "UPDATE DSP_VEHICLE_LOG_ENTRY SET " & vbCrLf _
                & " AUTO_KEY_SLIP = " & Val(CStr(mVNoSeq)) & ",  ENTRY_DATE = TO_DATE('" & mEntryDate & "','DD-MON-YYYY HH24:MI'), " & vbCrLf _
                & " VEHICLE_NO = '" & MainClass.AllowSingleQuote((txtVehicleNo.Text)) & "', TRANSPORTER_NAME = '" & MainClass.AllowSingleQuote(txtTransporterName.Text) & "'," & vbCrLf _
                & " OUTTIME_DIESEL = " & Val(txtOutTimeDiesel.Text) & ",OUTTIME_READINING = " & Val(txtOutReading.Text) & ", INTIME_DIESEL = " & Val(txtINTimeDiesel.Text) & ",INTIME_READINING = " & Val(txtINReading.Text) & "," & vbCrLf _
                & " FROM_LOCATION = '" & MainClass.AllowSingleQuote(txtFromLocation.Text) & "', TO_LOCATION = '" & MainClass.AllowSingleQuote(txtToLocation.Text) & "'," & vbCrLf _
                & " DRIVER_NAME = '" & MainClass.AllowSingleQuote(txtMainDriver.Text) & "', INCHARGE_NAME = '" & MainClass.AllowSingleQuote(txtIncharge.Text) & "', " & vbCrLf _
                & " OUT_TIME = TO_DATE('" & VB6.Format(txtOutTime.Text, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI'), IN_TIME = TO_DATE('" & VB6.Format(mINTime, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI')," & vbCrLf _
                & " REMARKS = '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', TOT_RUNNING = " & Val(txtINTime.Text) - Val(txtOutReading.Text) & ", " & vbCrLf _
                & " TOT_DIESEL = " & Val(txtINTimeDiesel.Text) - Val(txtOutTimeDiesel.Text) & ", STATUS='" & mStatus & "'," & vbCrLf _
                & " MODUSER = '" & MainClass.AllowSingleQuote(PubUserID) & "',MODDATE = TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')" & vbCrLf _
                & " WHERE AUTO_KEY_SLIP ='" & MainClass.AllowSingleQuote((lblMKey.Text)) & "'"

        End If


        PubDBCn.Execute(SqlStr)

        UpdateMain1 = True
        PubDBCn.CommitTrans()

        Exit Function
ErrPart:
        UpdateMain1 = False
        PubDBCn.RollbackTrans() ''
        RsLogEntry.Requery() ''.Refresh

        If Err.Description = "" Then Exit Function
        If Err.Number = -2147217900 Then
            ErrorMsg("Duplicate Load No. Generated, Save Again", "Duplicate", MsgBoxStyle.Critical)
        Else
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        ''Resume
    End Function
    Private Function AutoGenSeqNo() As String

        On Error GoTo AutoGenSeqNoErr
        Dim RsDNMainGen As ADODB.Recordset = Nothing
        Dim mNewSeqNo As Integer
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mStartingSNo As Integer
        Dim mMaxValue As String

        SqlStr = ""
        mStartingSNo = 1
        mNewSeqNo = mStartingSNo

        SqlStr = "SELECT Max(AUTO_KEY_SLIP)  " & vbCrLf _
            & " FROM DSP_VEHICLE_LOG_ENTRY " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_SLIP,LENGTH(AUTO_KEY_SLIP)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDNMainGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsDNMainGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mMaxValue = .Fields(0).Value
                    mNewSeqNo = CInt(Mid(mMaxValue, 1, Len(mMaxValue) - 6))
                    mNewSeqNo = mNewSeqNo + 1
                Else
                    mNewSeqNo = mStartingSNo '' 1
                End If
            End If
        End With
        AutoGenSeqNo = mNewSeqNo & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        Exit Function
AutoGenSeqNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchVehicleMaster()
    End Sub
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ViewGrid()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Public Sub frmLocalVehicleLogEntry_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        'Me.Text = "Collection Gate Pass" '' IIf(lblBookType.text = "L", "Loading Slip", "Unloading Slip")

        SqlStr = ""
        SqlStr = "Select * from DSP_VEHICLE_LOG_ENTRY Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLogEntry, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call SetTextLengths()
        If CmdAdd.Enabled = True Then cmdAdd_Click(CmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub
    Private Sub frmLocalVehicleLogEntry_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmLocalVehicleLogEntry_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        pMenu = myMenu
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        txtSlipDate.Enabled = False
        ADDMode = False
        MODIFYMode = False
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(4905)
        ''Me.Width = VB6.TwipsToPixelsX(9090)


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub SetTextLengths()

        On Error GoTo SetTextLengthsErr
        txtSlipNo.MaxLength = RsLogEntry.Fields("AUTO_KEY_SLIP").Precision
        txtSlipDate.MaxLength = 20

        txtINReading.MaxLength = RsLogEntry.Fields("INTIME_READINING").Precision
        txtINTimeDiesel.MaxLength = RsLogEntry.Fields("INTIME_DIESEL").Precision
        txtINTime.MaxLength = 20
        txtToLocation.MaxLength = RsLogEntry.Fields("TO_LOCATION").DefinedSize
        txtOutTime.MaxLength = 20
        txtOutReading.MaxLength = RsLogEntry.Fields("OUTTIME_READINING").Precision
        txtIncharge.MaxLength = RsLogEntry.Fields("INCHARGE_NAME").DefinedSize
        txtMainDriver.MaxLength = RsLogEntry.Fields("DRIVER_NAME").DefinedSize
        txtOutTimeDiesel.MaxLength = RsLogEntry.Fields("OUTTIME_DIESEL").Precision
        txtFromLocation.MaxLength = RsLogEntry.Fields("FROM_LOCATION").DefinedSize
        txtTransporterName.MaxLength = MainClass.SetMaxLength("TRANSPORTER_NAME", "FIN_TRANSPORTER_MST", PubDBCn)
        txtVehicleNo.MaxLength = MainClass.SetMaxLength("NAME", "FIN_VEHICLE_MST", PubDBCn)
        txtRemarks.MaxLength = RsLogEntry.Fields("REMARKS").DefinedSize



        Exit Sub
SetTextLengthsErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub Clear1()


        txtSlipNo.Text = ""
        txtSlipDate.Text = VB6.Format(PubCurrDate, "DD/MM/YYYY") & " " & GetServerTime()

        txtINReading.Text = ""
        txtINTimeDiesel.Text = ""
        txtINTime.Text = ""
        txtToLocation.Text = ""
        txtOutTime.Text = ""
        txtOutReading.Text = ""
        txtIncharge.Text = ""
        txtMainDriver.Text = ""
        txtOutTimeDiesel.Text = ""
        txtFromLocation.Text = ""
        txtTransporterName.Text = ""
        txtVehicleNo.Text = ""
        txtRemarks.Text = ""


        txtOutTime.Enabled = True
        txtOutReading.Enabled = True
        txtOutTimeDiesel.Enabled = True


        txtINTime.Enabled = True
        txtINReading.Enabled = True
        txtINTimeDiesel.Enabled = True


        chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkStatus.Enabled = True

        lblAddUser.Text = ""
        lblAddDate.Text = ""
        lblModUser.Text = ""
        lblModDate.Text = ""


        MainClass.ButtonStatus(Me, XRIGHT, RsLogEntry, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub


    Private Sub Show1()

        On Error GoTo ERR1
        Dim mDivisionCode As Double
        Dim mDivisionDesc As String

        With RsLogEntry
            If Not .EOF Then
                lblMKey.Text = .Fields("AUTO_KEY_SLIP").Value
                txtSlipNo.Text = IIf(IsDBNull(.Fields("AUTO_KEY_SLIP").Value), "", .Fields("AUTO_KEY_SLIP").Value)
                txtSlipDate.Text = VB6.Format(IIf(IsDBNull(.Fields("ENTRY_DATE").Value), "", .Fields("ENTRY_DATE").Value), "DD/MM/YYYY HH:MM") '' VB6.Format(IIf(IsNull(.Fields("TRIP_DATE").Value), "", .Fields("TRIP_DATE").Value), "DD/MM/YYYY")

                txtVehicleNo.Text = IIf(IsDBNull(.Fields("VEHICLE_NO").Value), "", .Fields("VEHICLE_NO").Value)
                txtTransporterName.Text = IIf(IsDBNull(.Fields("TRANSPORTER_NAME").Value), "", .Fields("TRANSPORTER_NAME").Value)

                txtOutTime.Text = VB6.Format(IIf(IsDBNull(.Fields("OUT_TIME").Value), "", .Fields("OUT_TIME").Value), "DD/MM/YYYY HH:MM")
                txtOutReading.Text = VB6.Format(IIf(IsDBNull(.Fields("OUTTIME_READINING").Value), 0, .Fields("OUTTIME_READINING").Value), "0.0")
                txtOutTimeDiesel.Text = VB6.Format(IIf(IsDBNull(.Fields("OUTTIME_DIESEL").Value), 0, .Fields("OUTTIME_DIESEL").Value), "0")

                txtINTime.Text = VB6.Format(IIf(IsDBNull(.Fields("IN_TIME").Value), "", .Fields("IN_TIME").Value), "DD/MM/YYYY HH:MM")
                txtINReading.Text = VB6.Format(IIf(IsDBNull(.Fields("INTIME_READINING").Value), 0, .Fields("INTIME_READINING").Value), "0.0")
                txtINTimeDiesel.Text = VB6.Format(IIf(IsDBNull(.Fields("INTIME_DIESEL").Value), 0, .Fields("INTIME_DIESEL").Value), "0")

                txtOutTime.Enabled = False
                txtOutReading.Enabled = False
                txtOutTimeDiesel.Enabled = False


                txtToLocation.Text = IIf(IsDBNull(.Fields("TO_LOCATION").Value), "", .Fields("TO_LOCATION").Value)
                txtIncharge.Text = IIf(IsDBNull(.Fields("INCHARGE_NAME").Value), "", .Fields("INCHARGE_NAME").Value)
                txtMainDriver.Text = IIf(IsDBNull(.Fields("DRIVER_NAME").Value), "", .Fields("DRIVER_NAME").Value)
                txtFromLocation.Text = IIf(IsDBNull(.Fields("FROM_LOCATION").Value), "", .Fields("FROM_LOCATION").Value)
                txtRemarks.Text = IIf(IsDBNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)

                chkStatus.CheckState = IIf(.Fields("Status").Value = "O", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
                chkStatus.Enabled = IIf(.Fields("Status").Value = "O", True, False)

                lblAddUser.Text = IIf(IsDBNull(.Fields("ADDUSER").Value), "", .Fields("ADDUSER").Value)
                lblAddDate.Text = VB6.Format(IIf(IsDBNull(.Fields("ADDDATE").Value), "", .Fields("ADDDATE").Value), "DD/MM/YYYY")
                lblModUser.Text = IIf(IsDBNull(.Fields("MODUSER").Value), "", .Fields("MODUSER").Value)
                lblModDate.Text = VB6.Format(IIf(IsDBNull(.Fields("MODDATE").Value), "", .Fields("MODDATE").Value), "DD/MM/YYYY")

            End If
        End With
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsLogEntry, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)

        txtSlipNo.Enabled = True

        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim pLoadingNo As Double

        FieldsVarification = True
        If ValidateBranchLocking((txtSlipDate.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsLogEntry.EOF = True Then Exit Function

        If MODIFYMode = True And Trim(txtSlipNo.Text) = "" Then
            MsgInformation("Slip No. is Blank")
            FieldsVarification = False
            Exit Function
        End If


        If Trim(txtSlipDate.Text) = "" Then
            MsgInformation(" Slip Date is empty. Cannot Save")
            txtSlipDate.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtSlipDate.Text) <> "" Then
            If IsDate(txtSlipDate.Text) = False Then
                MsgInformation(" Invalid Slip Date. Cannot Save")
                txtSlipDate.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If FYChk(VB6.Format(txtSlipDate.Text, "DD/MM/YYYY")) = False Then
            FieldsVarification = False
            If txtSlipDate.Enabled = True Then txtSlipDate.Focus()
            Exit Function
        End If

        If Trim(txtVehicleNo.Text) = "" Then
            MsgInformation("Vehicle No is Blank. Cannot Save")
            If txtVehicleNo.Enabled = True Then txtVehicleNo.Focus()
            FieldsVarification = False
            Exit Function
        End If


        If Trim(txtMainDriver.Text) = "" Then
            MsgInformation("Main Driver is empty. Cannot Save")
            txtMainDriver.Focus()
            FieldsVarification = False
            Exit Function
        End If

        'If Val(txtOutTimeDiesel.Text) = 0 Then
        '    MsgInformation("Out Time Diesel is Zero. Cannot Save")
        '    txtOutTimeDiesel.Focus()
        '    FieldsVarification = False
        '    Exit Function
        'End If

        If Val(txtOutReading.Text) = 0 Then
            MsgInformation("Out Time Reading is Zero. Cannot Save")
            txtOutReading.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If chkStatus.CheckState = System.Windows.Forms.CheckState.Checked And chkStatus.Enabled = False Then
            MsgInformation("Slip Already Closed, so cann't be Save.")
            If CmdSave.Enabled = True Then CmdSave.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtOutTime.Text) = "" Then
            MsgInformation("Out Time Date is empty. Cannot Save")
            txtOutTime.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtOutTime.Text) <> "" Then
            If IsDate(txtOutTime.Text) = False Then
                MsgInformation(" Invalid Out Time Date. Cannot Save")
                txtOutTime.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If Trim(Replace(Replace(Replace(txtINTime.Text, "/", ""), ":", ""), "_", "")) <> "" Then
            If IsDate(txtINTime.Text) = False Then
                MsgInformation(" Invalid IN Time Date. Cannot Save")
                txtINTime.Focus()
                FieldsVarification = False
                Exit Function
            End If

            If CDate(txtINTime.Text) < CDate(txtOutTime.Text) Then
                MsgInformation("IN Time Date cann't be less than Out date Time . Cannot Save")
                txtINTime.Focus()
                FieldsVarification = False
                Exit Function
            End If

            If Val(txtINReading.Text) = 0 Then
                MsgInformation("IN Reading is Zero. Cannot Save")
                txtINReading.Focus()
                FieldsVarification = False
                Exit Function
            End If

            If Val(txtINReading.Text) < Val(txtOutReading.Text) Then
                MsgInformation("IN Reading cann't be Less than Out Time Reading. Cannot Save")
                txtINReading.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If PendingCollection(Val(txtSlipNo.Text), pLoadingNo) = True Then
            MsgInformation("Trip Already Open for this Vehicle, Last Trip No : " & pLoadingNo & ". Cannot Modify")
            If CmdSave.Enabled = True Then CmdSave.Focus()
            FieldsVarification = False
            Exit Function
        End If


        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function
    Private Sub ViewGrid()

        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            AssignGrid(True)
            '        ADataPPOMain.Refresh
            SprdView.Refresh()
            SprdView.Focus()
            FraTop.Visible = False
            Frabot.Visible = False
            SprdView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            FraTop.Visible = True
            Frabot.Visible = True
            SprdView.SendToBack()
        End If
        Call FormatSprdView()
        MainClass.ButtonStatus(Me, XRIGHT, RsLogEntry, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub frmLocalVehicleLogEntry_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsLogEntry.Close()
        'PvtDBCn.Close
        RsLogEntry = Nothing
        'Set PvtDBCn = Nothing
    End Sub
    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
        txtSlipNo.Text = SprdView.Text

        txtSlipNo_Validating(txtSlipNo, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub
    Private Sub SearchTransporterMaster()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""


        If MainClass.SearchGridMaster((txtTransporterName.Text), "FIN_TRANSPORTER_MST", "TRANSPORTER_NAME", "TRANSPORTER_ID", , , SqlStr) = True Then
            txtTransporterName.Text = AcName
            txtTransporterName_Validating(txtTransporterName, New System.ComponentModel.CancelEventArgs(False))
        End If

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtRemarks_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRemarks.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtRemarks.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSlipDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSlipDate.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtSlipNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSlipNo.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtSlipNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSlipNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Public Sub txtSlipNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSlipNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMkey As String = ""
        Dim mMRRNo As String
        Dim SqlStr As String = ""

        If Trim(txtSlipNo.Text) = "" Then GoTo EventExitSub

        If Len(txtSlipNo.Text) < 6 Then
            txtSlipNo.Text = VB6.Format(Val(txtSlipNo.Text), "00000") & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        If MODIFYMode = True And RsLogEntry.EOF = False Then xMkey = RsLogEntry.Fields("mKey").Value
        mMRRNo = Trim(txtSlipNo.Text)

        SqlStr = " SELECT * FROM DSP_VEHICLE_LOG_ENTRY " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_SLIP,LENGTH(AUTO_KEY_SLIP)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND AUTO_KEY_SLIP=" & Val(mMRRNo) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLogEntry, ADODB.LockTypeEnum.adLockReadOnly)

        If RsLogEntry.EOF = False Then
            Clear1()
            Show1()
            '        TxtCustomerName.Enabled = True
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Loading Note, Use Generate Despatch Note Option To add", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM DSP_VEHICLE_LOG_ENTRY " & " WHERE AUTO_KEY_SLIP=" & Val(xMkey) & ""
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLogEntry, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub frmLocalVehicleLogEntry_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        SqlStr = ""
        MainClass.ClearGrid(SprdView)

        SqlStr = " SELECT DISTINCT " & vbCrLf _
            & " AUTO_KEY_SLIP, ENTRY_DATE, VEHICLE_NO, TRANSPORTER_NAME," & vbCrLf _
            & " OUTTIME_DIESEL, OUTTIME_READINING, INTIME_DIESEL, INTIME_READINING," & vbCrLf _
            & " FROM_LOCATION, TO_LOCATION, DRIVER_NAME, INCHARGE_NAME, OUT_TIME," & vbCrLf _
            & " IN_TIME, REMARKS, TOT_RUNNING, TOT_DIESEL," & vbCrLf _
            & " DECODE(STATUS,'O','OPEN','CLOSED') AS STATUS,ADDUSER, ADDDATE " & vbCrLf _
            & " FROM DSP_VEHICLE_LOG_ENTRY A " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_SLIP,LENGTH(AUTO_KEY_SLIP)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " ORDER BY AUTO_KEY_SLIP,ENTRY_DATE"




        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        Call FormatSprdView()
    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 20)
            .set_ColWidth(0, 5)
            .set_ColWidth(1, 14)
            .set_ColWidth(2, 14)
            .set_ColWidth(3, 20)
            .set_ColWidth(4, 20)
            .set_ColWidth(5, 10)
            .set_ColWidth(6, 10)
            .set_ColWidth(7, 10)
            .set_ColWidth(8, 10)
            .set_ColWidth(9, 25)
            .set_ColWidth(10, 25)
            .set_ColWidth(11, 20)
            .set_ColWidth(12, 20)
            .set_ColWidth(13, 10)
            .set_ColWidth(14, 10)
            .set_ColWidth(15, 25)
            .set_ColWidth(16, 10)
            .set_ColWidth(17, 10)
            .set_ColWidth(18, 10)
            .set_ColWidth(19, 10)
            .set_ColWidth(20, 10)


            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub SearchVehicleMaster()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((txtVehicleNo.Text), "FIN_VEHICLE_MST", "NAME", "TRANSPORTER_NAME", "VEHICLE_TYPE", , SqlStr) = True Then
            txtVehicleNo.Text = AcName
            txtVehicleNo_Validating(txtVehicleNo, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtVehicleNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVehicleNo.DoubleClick
        SearchVehicleMaster()
    End Sub

    Private Sub txtVehicleNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtVehicleNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtVehicleNo.Text) = "" Then GoTo EventExitSub


        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable((txtVehicleNo.Text), "NAME", "TRANSPORTER_NAME", "FIN_VEHICLE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgInformation("Invalid Vehicle No")
            Cancel = True
            GoTo EventExitSub
        Else
            txtTransporterName.Text = MasterNo
        End If

        If MainClass.ValidateWithMasterTable((txtVehicleNo.Text), "NAME", "VEHICLE_TYPE", "FIN_VEHICLE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgInformation("Invalid Vehicle No")
            Cancel = True
            GoTo EventExitSub
        End If


        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtTransporterName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTransporterName.DoubleClick
        SearchTransporterMaster()
    End Sub
    Private Sub txtTransporterName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTransporterName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mAmount As Double

        If Trim(txtTransporterName.Text) = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If Trim(txtVehicleNo.Text) = "" Then GoTo EventExitSub

        SqlStr = SqlStr & vbCrLf & " AND NAME='" & Trim(txtVehicleNo.Text) & "'"
        If MainClass.ValidateWithMasterTable((txtTransporterName.Text), "TRANSPORTER_NAME", "TRANSPORTER_NAME", "FIN_VEHICLE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgInformation("Invalid Transporter Name")
            Cancel = True
        End If


        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtINReading_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtINReading.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub

    Private Sub txtINReading_TextChanged(sender As Object, e As System.EventArgs) Handles txtINReading.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtOutReading_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOutReading.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub

    Private Sub txtOutReading_TextChanged(sender As Object, e As System.EventArgs) Handles txtOutReading.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtINTimeDiesel_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtINTimeDiesel.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub

    Private Sub txtINTimeDiesel_TextChanged(sender As Object, e As System.EventArgs) Handles txtINTimeDiesel.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtOutTimeDiesel_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOutTimeDiesel.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub

    Private Sub txtOutTimeDiesel_TextChanged(sender As Object, e As System.EventArgs) Handles txtOutTimeDiesel.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtMainDriver_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMainDriver.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtMainDriver.Text)
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub

    Private Sub txtMainDriver_KeyUp(sender As Object, EventArgs As System.Windows.Forms.KeyEventArgs) Handles txtMainDriver.KeyUp
        Dim KeyCode As Short = EventArgs.KeyCode
        Dim Shift As Short = EventArgs.KeyData \ &H10000
    End Sub

    Private Sub txtMainDriver_TextChanged(sender As Object, e As System.EventArgs) Handles txtMainDriver.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtINTime_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtINTime.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtINTime.Text)
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub

    Private Sub txtINTime_KeyUp(sender As Object, EventArgs As System.Windows.Forms.KeyEventArgs) Handles txtINTime.KeyUp
        Dim KeyCode As Short = EventArgs.KeyCode
        Dim Shift As Short = EventArgs.KeyData \ &H10000
    End Sub

    Private Sub txtINTime_TextChanged(sender As Object, e As System.EventArgs) Handles txtINTime.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtToLocation_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtToLocation.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtToLocation.Text)
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub

    Private Sub txtToLocation_KeyUp(sender As Object, EventArgs As System.Windows.Forms.KeyEventArgs) Handles txtToLocation.KeyUp
        Dim KeyCode As Short = EventArgs.KeyCode
        Dim Shift As Short = EventArgs.KeyData \ &H10000
    End Sub

    Private Sub txtToLocation_TextChanged(sender As Object, e As System.EventArgs) Handles txtToLocation.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOutTime_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOutTime.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtOutTime.Text)
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub

    Private Sub txtOutTime_KeyUp(sender As Object, EventArgs As System.Windows.Forms.KeyEventArgs) Handles txtOutTime.KeyUp
        Dim KeyCode As Short = EventArgs.KeyCode
        Dim Shift As Short = EventArgs.KeyData \ &H10000
    End Sub

    Private Sub txtOutTime_TextChanged(sender As Object, e As System.EventArgs) Handles txtOutTime.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtIncharge_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtIncharge.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtIncharge.Text)
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub

    Private Sub txtIncharge_KeyUp(sender As Object, EventArgs As System.Windows.Forms.KeyEventArgs) Handles txtIncharge.KeyUp
        Dim KeyCode As Short = EventArgs.KeyCode
        Dim Shift As Short = EventArgs.KeyData \ &H10000
    End Sub

    Private Sub txtIncharge_TextChanged(sender As Object, e As System.EventArgs) Handles txtIncharge.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtFromLocation_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtFromLocation.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtFromLocation.Text)
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub

    Private Sub txtFromLocation_KeyUp(sender As Object, EventArgs As System.Windows.Forms.KeyEventArgs) Handles txtFromLocation.KeyUp
        Dim KeyCode As Short = EventArgs.KeyCode
        Dim Shift As Short = EventArgs.KeyData \ &H10000
    End Sub

    Private Sub txtFromLocation_TextChanged(sender As Object, e As System.EventArgs) Handles txtFromLocation.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTransporterName_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTransporterName.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtTransporterName.Text)
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTransporterName_KeyUp(sender As Object, EventArgs As System.Windows.Forms.KeyEventArgs) Handles txtTransporterName.KeyUp
        Dim KeyCode As Short = EventArgs.KeyCode
        Dim Shift As Short = EventArgs.KeyData \ &H10000
    End Sub

    Private Sub txtTransporterName_TextChanged(sender As Object, e As System.EventArgs) Handles txtTransporterName.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtVehicleNo_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVehicleNo.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtVehicleNo.Text)
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub

    Private Sub txtVehicleNo_KeyUp(sender As Object, EventArgs As System.Windows.Forms.KeyEventArgs) Handles txtVehicleNo.KeyUp
        Dim KeyCode As Short = EventArgs.KeyCode
        Dim Shift As Short = EventArgs.KeyData \ &H10000
    End Sub

    Private Sub txtVehicleNo_TextChanged(sender As Object, e As System.EventArgs) Handles txtVehicleNo.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
End Class
