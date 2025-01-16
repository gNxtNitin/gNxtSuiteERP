Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmGenRecord
    Inherits System.Windows.Forms.Form
    Dim RsGenRecHdr As ADODB.Recordset
    Dim RsGenRecDet As ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean

    Private Const ConRowHeight As Short = 14

    Private Const ColReadingDate As Short = 1
    Private Const ColReadingTime As Short = 2
    Private Const ColRPM As Short = 3
    Private Const ColHoursMtrReading As Short = 4
    Private Const ColUnitMtrReading As Short = 5
    Private Const ColOilPress As Short = 6
    Private Const ColOilTemp As Short = 7
    Private Const ColWaterTemp As Short = 8
    Private Const ColFrequency As Short = 9
    Private Const ColAMPS As Short = 10
    Private Const ColHSPLevel As Short = 11
    Private Const ColLoad As Short = 12
    Private Const ColRHM As Short = 13
    Private Const ColOilTempOut As Short = 14
    Private Const ColVoltage As Short = 15
    Private Const ColKWH As Short = 16
    Private Const ColTemp1 As Short = 17
    Private Const ColTemp2 As Short = 18
    Private Const ColTemp3 As Short = 19
    Private Const ColTemp4 As Short = 20
    Private Const ColTemp5 As Short = 21
    Private Const ColTemp6 As Short = 22
    Private Const ColOilLevel As Short = 23
    Private Const ColRemarks As Short = 24

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtNumber.Enabled = False
            cmdSearchNumber.Enabled = False
            SprdMain.Enabled = True
        Else
            ADDMode = False
            MODIFYMode = False
            If RsGenRecHdr.EOF = False Then RsGenRecHdr.MoveFirst()
            Show1()
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
        'Resume
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        On Error Resume Next
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart

        If txtNumber.Text = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        If Not RsGenRecHdr.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "MAN_GENREC_HDR", (txtNumber.Text), RsGenRecHdr) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM MAN_GENREC_DET WHERE AUTO_KEY_GENREC=" & Val(lblMkey.Text) & "")
                PubDBCn.Execute("DELETE FROM MAN_GENREC_HDR WHERE AUTO_KEY_GENREC=" & Val(lblMkey.Text) & "")
                PubDBCn.CommitTrans()
                RsGenRecHdr.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Errors.Clear()
        RsGenRecHdr.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsGenRecHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            txtNumber.Enabled = False
            cmdSearchNumber.Enabled = False
            SprdMain.Enabled = True
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
        If IsRecordExist = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Update1 = True Then
            ADDMode = False
            MODIFYMode = False
            txtNumber_Validating(txtNumber, New System.ComponentModel.CancelEventArgs(False))
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

    Private Function IsRecordExist() As Boolean

        On Error GoTo IsRecordExistERR
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        IsRecordExist = False

        SqlStr = " SELECT AUTO_KEY_GENREC " & vbCrLf _
                    & " From MAN_GENREC_HDR " & vbCrLf _
                    & " WHERE COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND MACHINE_NO ='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "' " & vbCrLf _
                    & " AND MACHINE_TYPE='" & lblType.Text & "' " & vbCrLf _
                    & " AND ON_DATE =TO_DATE('" & vb6.Format(txtOnDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " ''& vbCrLf _
        '& " AND ON_TIME = TO_DATE('" & txtOnTime.Text & "', 'HH24:MI')" & " "

        If Val(txtNumber.Text) <> 0 Then
            SqlStr = SqlStr & vbCrLf & " AND AUTO_KEY_GENREC<>" & Val(txtNumber.Text) & ""
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With RsTemp
            If Not .EOF Then
                MsgInformation("This entry already exist in Number : " & .Fields("AUTO_KEY_GENREC").Value)
                IsRecordExist = True
                Exit Function
            End If
        End With

        '    If MODIFYMode = True Then Exit Function

        SqlStr = " SELECT AUTO_KEY_GENREC " & vbCrLf & " From MAN_GENREC_HDR " & vbCrLf & " WHERE COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND MACHINE_NO ='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "' " & vbCrLf & " AND MACHINE_TYPE='" & lblType.Text & "' " & vbCrLf & " AND ON_DATE >TO_DATE('" & VB6.Format(txtOnDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With RsTemp
            If Not .EOF Then
                MsgInformation("Cann't be ADD/MODIFY back date entry.")
                IsRecordExist = True
                Exit Function
            End If
        End With



        Exit Function
IsRecordExistERR:
        IsRecordExist = True
        MsgBox(Err.Description)
    End Function

    Private Function Update1() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mNumber As Double

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = ""
        mNumber = Val(txtNumber.Text)
        If Val(txtNumber.Text) = 0 Then
            mNumber = AutoGenKeyNo()
        End If
        txtNumber.Text = CStr(mNumber)
        If ADDMode = True Then
            lblMkey.Text = CStr(mNumber)
            SqlStr = " INSERT INTO MAN_GENREC_HDR " & vbCrLf _
                            & " (AUTO_KEY_GENREC,COMPANY_CODE,MACHINE_NO, " & vbCrLf _
                            & " MACHINE_TYPE,ON_DATE,ON_TIME, " & vbCrLf _
                            & " OFF_DATE,OFF_TIME,TOTAL_TIME, " & vbCrLf _
                            & " DONE_BY,REMARKS, " & vbCrLf _
                            & " ADDUSER,ADDDATE,UNITS_GENERATED) " & vbCrLf _
                            & " VALUES ( " & vbCrLf _
                            & " " & mNumber & "," & RsCompany.Fields("COMPANY_CODE").Value & ",'" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "'," & vbCrLf _
                            & " '" & lblType.Text & "',TO_DATE('" & vb6.Format(txtOnDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), TO_DATE('" & txtOnTime.Text & "', 'HH24:MI')," & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtOffDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & txtOffTime.Text & "', 'HH24:MI')," & Val(txtTotalTime.Text) & "," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtDoneBy.Text) & "','" & MainClass.AllowSingleQuote(txtRemarks.Text) & "'," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                            & " " & Val(txtUnits.Text) & ")"
        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE MAN_GENREC_HDR SET " & vbCrLf & " AUTO_KEY_GENREC=" & mNumber & ", " & vbCrLf & " MACHINE_NO='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "', " & vbCrLf & " MACHINE_TYPE='" & (lblType.Text) & "', " & vbCrLf & " ON_DATE=TO_DATE('" & VB6.Format(txtOnDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " ON_TIME=TO_DATE('" & txtOnTime.Text & "', 'HH24:MI'), " & vbCrLf & " OFF_DATE=TO_DATE('" & VB6.Format(txtOffDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " OFF_TIME=TO_DATE('" & txtOffTime.Text & "', 'HH24:MI'), " & vbCrLf & " TOTAL_TIME=" & Val(txtTotalTime.Text) & "," & vbCrLf & " DONE_BY='" & MainClass.AllowSingleQuote(txtDoneBy.Text) & "'," & vbCrLf & " REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "'," & vbCrLf & " UNITS_GENERATED=" & Val(txtUnits.Text) & "," & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND AUTO_KEY_GENREC =" & Val(lblMkey.Text) & ""
        End If

        PubDBCn.Execute(SqlStr)
        If UpdateDetail = False Then GoTo ErrPart

        Update1 = True
        PubDBCn.CommitTrans()
        txtNumber.Text = CStr(mNumber)
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsGenRecHdr.Requery()
        RsGenRecDet.Requery()
        MsgBox(Err.Description)
        ''Resume
    End Function

    Private Function AutoGenKeyNo() As Double

        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset
        Dim mAutoGen As Double
        Dim SqlStr As String
        Dim mMaxValue As String

        If lblType.Text = "2" Then
            mAutoGen = 1
        ElseIf lblType.Text = "8" Then
            mAutoGen = 30001
        ElseIf lblType.Text = "C" Then
            mAutoGen = 60001
        End If

        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_GENREC)  " & vbCrLf & " FROM MAN_GENREC_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_GENREC,LENGTH(AUTO_KEY_GENREC)-5,4)=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND MACHINE_TYPE='" & lblType.Text & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAutoGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsAutoGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mMaxValue = .Fields(0).Value
                    mAutoGen = CDbl(Mid(mMaxValue, 1, Len(mMaxValue) - 6))
                    mAutoGen = mAutoGen + 1
                Else
                    mAutoGen = 1
                End If
            End If
        End With
        AutoGenKeyNo = CDbl(mAutoGen & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"))
        RsAutoGen.Close()
        RsAutoGen = Nothing
        Exit Function
AutogenErr:
        MsgBox(Err.Description)
    End Function

    Private Function UpdateDetail() As Boolean

        On Error GoTo UpdateDetailERR
        Dim SqlStr As String
        Dim I As Integer
        Dim mReadingDate As String
        Dim mReadingTime As String
        Dim mRPM As Double
        Dim mHoursMtrReading As Double
        Dim mUnitMtrReading As Double
        Dim mOilPress As Double
        Dim mOilTemp As Double
        Dim mWaterTemp As Double
        Dim mFrequency As Double
        Dim mAmps As Double
        Dim mHSPLevel As Double
        Dim mLoad As Double
        Dim mRHM As Double
        Dim mOilTempOut As Double
        Dim mVoltage As Double
        Dim mKWH As Double
        Dim mTemp1 As Double
        Dim mTemp2 As Double
        Dim mTemp3 As Double
        Dim mTemp4 As Double
        Dim mTemp5 As Double
        Dim mTemp6 As Double
        Dim mOilLevel As String
        Dim mRemarks As String

        PubDBCn.Execute("DELETE FROM MAN_GENREC_DET WHERE AUTO_KEY_GENREC=" & Val(lblMkey.Text) & "")

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColReadingDate
                mReadingDate = Trim(.Text)

                .Col = ColReadingTime
                mReadingTime = Trim(.Text)

                .Col = ColRPM
                mRPM = Val(.Text)

                .Col = ColHoursMtrReading
                mHoursMtrReading = Val(.Text)

                .Col = ColUnitMtrReading
                mUnitMtrReading = Val(.Text)

                .Col = ColOilPress
                mOilPress = Val(.Text)

                .Col = ColOilTemp
                mOilTemp = Val(.Text)

                .Col = ColWaterTemp
                mWaterTemp = Val(.Text)

                .Col = ColFrequency
                mFrequency = Val(.Text)

                .Col = ColAMPS
                mAmps = Val(.Text)

                .Col = ColHSPLevel
                mHSPLevel = Val(.Text)

                .Col = ColLoad
                mLoad = Val(.Text)

                .Col = ColRHM
                mRHM = Val(.Text)

                .Col = ColOilTempOut
                mOilTempOut = Val(.Text)

                .Col = ColVoltage
                mVoltage = Val(.Text)

                .Col = ColKWH
                mKWH = Val(.Text)

                .Col = ColTemp1
                mTemp1 = Val(.Text)

                .Col = ColTemp2
                mTemp2 = Val(.Text)

                .Col = ColTemp3
                mTemp3 = Val(.Text)

                .Col = ColTemp4
                mTemp4 = Val(.Text)

                .Col = ColTemp5
                mTemp5 = Val(.Text)

                .Col = ColTemp6
                mTemp6 = Val(.Text)

                .Col = ColOilLevel
                mOilLevel = MainClass.AllowSingleQuote(.Text)

                .Col = ColRemarks
                mRemarks = MainClass.AllowSingleQuote(.Text)

                SqlStr = ""

                If mReadingDate <> "" And mReadingTime <> "" Then
                    SqlStr = " INSERT INTO MAN_GENREC_DET ( " & vbCrLf & " AUTO_KEY_GENREC,SERIAL_NO,READING_DATE, " & vbCrLf & " READING_TIME,RPM,HRS_MTR_READING,UNIT_MTR_READING, " & vbCrLf & " OIL_PRESSURE,OIL_TEMP_IN,WATER_TEMP, " & vbCrLf & " FREQUENCY,AMPS,HSP_LEVEL, " & vbCrLf & " LOAD,RHM,OIL_TEMP_OUT, " & vbCrLf & " VOLTAGE,KWH,CYLINDER1_TEMP, " & vbCrLf & " CYLINDER2_TEMP,CYLINDER3_TEMP,CYLINDER4_TEMP, " & vbCrLf & " CYLINDER5_TEMP,CYLINDER6_TEMP,OIL_LEVEL,REMARKS ) " & vbCrLf & " VALUES ( " & vbCrLf & " " & Val(lblMkey.Text) & "," & I & ",TO_DATE('" & VB6.Format(mReadingDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " TO_DATE('" & mReadingTime & "', 'HH24:MI')," & mRPM & "," & mHoursMtrReading & "," & vbCrLf & " " & mUnitMtrReading & "," & mOilPress & "," & mOilTemp & "," & mWaterTemp & "," & vbCrLf & " " & mFrequency & "," & mAmps & "," & mHSPLevel & "," & vbCrLf & " " & mLoad & "," & mRHM & "," & mOilTempOut & "," & vbCrLf & " " & mVoltage & "," & mKWH & "," & mTemp1 & "," & vbCrLf & " " & mTemp2 & "," & mTemp3 & "," & mTemp4 & "," & vbCrLf & " " & mTemp5 & "," & mTemp6 & ",'" & mOilLevel & "','" & mRemarks & "') "

                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        UpdateDetail = True
        Exit Function
UpdateDetailERR:
        UpdateDetail = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Sub cmdSearchDoneBy_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchDoneBy.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND EMP_LEAVE_DATE IS NULL "
        If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            txtDoneBy.Text = AcName1
            lblDoneBy.text = AcName
        End If
    End Sub

    Private Sub cmdSearchMachine_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchMachine.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND MACHINE_UB='N' AND STATUS='O' "
        If MainClass.SearchGridMaster("", "MAN_MACHINE_MST", "MACHINE_DESC", "MACHINE_NO", , , SqlStr) = True Then
            txtMachineNo.Text = AcName1
            lblMachineNo.text = AcName
            If txtMachineNo.Enabled = True Then txtMachineNo.Focus()
        End If
    End Sub

    Private Sub cmdSearchNumber_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchNumber.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND MACHINE_TYPE='" & lblType.Text & "' "

        '            & " AND SUBSTR(AUTO_KEY_GENREC,LENGTH(AUTO_KEY_GENREC)-5,4)=" & RsCompany.fields("FYEAR").value & " " & vbCrLf _
        '
        If MainClass.SearchGridMaster(txtNumber.Text, "MAN_GENREC_HDR", "AUTO_KEY_GENREC", "MACHINE_NO", "ON_DATE", "TO_CHAR(ON_TIME,'HH24:MI') AS ON_TIME", SqlStr) = True Then
            txtNumber.Text = AcName
            Call txtNumber_Validating(txtNumber, New System.ComponentModel.CancelEventArgs(False)) ''_Validate(False)
        End If
    End Sub

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
            SprdView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsGenRecHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmGenRecord_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If lblType.Text = "2" Then
            Me.Text = "250 KVA Generators Data Recording"
        ElseIf lblType.Text = "8" Then
            Me.Text = "800 KVA Generators Data Recording"
        ElseIf lblType.Text = "C" Then
            Me.Text = "Compressor Data Recording"
            txtUnits.Enabled = False
            txtUnits.Visible = False
            lblUnits.Visible = False
            fraUnitMtrReading.Visible = False
        End If

        SqlStr = "Select * From MAN_GENREC_HDR WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGenRecHdr, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From MAN_GENREC_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGenRecDet, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)
        Call SetTextLengths()
        Call Clear1()
        Call SetSprdHeading()
        If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        Resume
    End Sub

    Private Sub SetSprdHeading()
        On Error GoTo ERR1
        Dim I As Short

        With SprdMain
            If lblType.Text = "2" Then
                For I = ColHSPLevel To ColOilLevel
                    .Col = I
                    .ColHidden = True
                Next
            ElseIf lblType.Text = "8" Then
                .Col = ColAMPS
                .ColHidden = True
                .Col = ColOilLevel
                .ColHidden = True
            ElseIf lblType.Text = "C" Then
                .Col = ColRPM
                .ColHidden = True
                For I = ColFrequency To ColTemp6
                    .Col = I
                    .ColHidden = True
                Next
                .Row = 0
                .Col = ColOilPress
                .Text = "Line Pressure"
                .Col = ColOilTemp
                .Text = "Discharge Temp."
                .Col = ColWaterTemp
                .Text = "Sump Pressure"
            End If
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub

    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = ""

        SqlStr = " SELECT " & vbCrLf & " AUTO_KEY_GENREC AS RECORDING_NUMBER,MACHINE_NO,ON_DATE,TO_CHAR(ON_TIME,'HH24:MI') AS ON_TIME, " & vbCrLf & " OFF_DATE,TO_CHAR(OFF_TIME,'HH24:MI') AS OFF_TIME,TOTAL_TIME,DONE_BY,REMARKS " & vbCrLf & " FROM MAN_GENREC_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND MACHINE_TYPE='" & lblType.Text & "' " & vbCrLf & " ORDER BY ON_DATE"

        '            & " AND SUBSTR(AUTO_KEY_GENREC,LENGTH(AUTO_KEY_GENREC)-5,4)=" & RsCompany.fields("FYEAR").value & "" & vbCrLf _
        '
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmGenRecord_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmGenRecord_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        Dim I As Integer
        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Left = 0
        Me.Top = 0
        Me.Height = VB6.TwipsToPixelsY(8010)
        Me.Width = VB6.TwipsToPixelsX(11595)

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
        txtNumber.Text = ""
        txtMachineNo.Text = ""
        lblMachineNo.Text = ""
        txtOnDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtOnTime.Text = GetServerTime
        txtOffDate.Text = ""
        txtOffTime.Text = ""
        txtTotalTime.Text = ""
        txtDoneBy.Text = ""
        lblDoneBy.Text = ""
        txtRemarks.Text = ""
        txtLastReading.Text = ""
        txtLastUnitReading.Text = ""
        txtUnits.Text = ""
        Call MakeEnableDesableField(True)
        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsGenRecHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ClearErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub

    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim I As Short

        With SprdMain
            .set_RowHeight(-1, ConRowHeight)
            .Row = Arow

            .Col = ColReadingDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateFormat = SS_CELL_DATE_FORMAT_DDMMYY
            .set_ColWidth(ColReadingDate, 8)

            .Col = ColReadingTime
            .CellType = SS_CELL_TYPE_TIME
            .TypeTime24Hour = SS_CELL_TIME_24_HOUR_CLOCK
            .set_ColWidth(ColReadingTime, 7)

            .ColsFrozen = ColReadingTime

            .Col = ColRPM
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999.99")
            .TypeFloatMin = CDbl("0")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalPlaces = 2
            .set_ColWidth(ColRPM, 7)

            .Col = ColHoursMtrReading
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999.99")
            .TypeFloatMin = CDbl("0")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalPlaces = 2
            .set_ColWidth(ColHoursMtrReading, 8)

            .Col = ColUnitMtrReading
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999.99")
            .TypeFloatMin = CDbl("0")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalPlaces = 2
            .set_ColWidth(ColUnitMtrReading, 8)

            For I = ColOilPress To ColKWH
                .Col = I
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999.99")
                .TypeFloatMin = CDbl("0")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .TypeFloatDecimalPlaces = 2
                .set_ColWidth(I, 7)
            Next

            For I = ColTemp1 To ColTemp6
                .Col = I
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999.99")
                .TypeFloatMin = CDbl("0")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .TypeFloatDecimalPlaces = 2
                .set_ColWidth(I, 9)
            Next

            .Col = ColOilLevel
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsGenRecDet.Fields("OIL_LEVEL").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColOilLevel, 10)

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsGenRecDet.Fields("REMARKS").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColRemarks, 20)

            If lblType.Text = "2" Then
                For I = ColHSPLevel To ColOilLevel
                    .Col = I
                    .ColHidden = True
                Next
            ElseIf lblType.Text = "8" Then
                .Col = ColAMPS
                .ColHidden = True
                .Col = ColOilLevel
                .ColHidden = True
            ElseIf lblType.Text = "C" Then
                .Col = ColUnitMtrReading
                .ColHidden = True
                .Col = ColRPM
                .ColHidden = True
                For I = ColFrequency To ColTemp6
                    .Col = I
                    .ColHidden = True
                Next
            End If
        End With
        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub

    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 600)
            .set_ColWidth(0, 500)
            .set_ColWidth(1, 1500)
            .set_ColWidth(2, 1500)
            .set_ColWidth(3, 1000)
            .set_ColWidth(4, 500)
            .set_ColWidth(5, 1000)
            .set_ColWidth(6, 500)
            .ColsFrozen = 2
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub

    Private Sub SetTextLengths()
        On Error GoTo ERR1
        txtNumber.Maxlength = RsGenRecHdr.Fields("AUTO_KEY_GENREC").Precision
        txtOnDate.Maxlength = RsGenRecHdr.Fields("ON_DATE").DefinedSize - 6
        txtOnTime.Maxlength = RsGenRecHdr.Fields("ON_TIME").DefinedSize - 11
        txtOffDate.Maxlength = RsGenRecHdr.Fields("OFF_DATE").DefinedSize - 6
        txtOffTime.Maxlength = RsGenRecHdr.Fields("OFF_TIME").DefinedSize - 11
        txtTotalTime.Maxlength = RsGenRecHdr.Fields("TOTAL_TIME").Precision
        txtDoneBy.Maxlength = RsGenRecHdr.Fields("DONE_BY").DefinedSize
        txtRemarks.Maxlength = RsGenRecHdr.Fields("REMARKS").DefinedSize
        txtUnits.Maxlength = RsGenRecHdr.Fields("UNITS_GENERATED").Precision
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub

    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim ii As Integer
        FieldsVarification = True

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsGenRecHdr.EOF = True Then Exit Function

        If Trim(txtOnDate.Text) = "" Then
            MsgInformation("On Date is empty, So unable to save.")
            txtOnDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtOnTime.Text) = "" Then
            MsgInformation("On Time is empty, So unable to save.")
            txtOnTime.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtDoneBy.Text) = "" Then
            MsgInformation("Done By is empty, So unable to save.")
            txtDoneBy.Focus()
            FieldsVarification = False
            Exit Function
        End If

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        ''Resume
    End Function

    Private Sub frmGenRecord_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        RsGenRecHdr.Close()
        RsGenRecHdr = Nothing
        RsGenRecDet.Close()
        RsGenRecDet = Nothing
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim SqlStr As String
        If eventArgs.Col = 0 And eventArgs.Row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdMain, eventArgs.Row, ColReadingDate)
            MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub SprdMain_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdMain.KeyDownEvent

        Dim mActiveCol As Integer
        Dim mDate As String
        Dim mTime As String

        mActiveCol = SprdMain.ActiveCol

        If eventArgs.KeyCode = System.Windows.Forms.Keys.Return Or eventArgs.KeyCode = System.Windows.Forms.Keys.Tab Then
            If mActiveCol = ColRemarks Then
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColReadingDate
                mDate = Trim(SprdMain.Text)
                SprdMain.Col = ColReadingTime
                mTime = Trim(SprdMain.Text)
                SprdMain.Col = ColRemarks
                If mDate <> "" And mTime <> "" And SprdMain.MaxRows = SprdMain.ActiveRow And SprdMain.MaxRows = 1 Then
                    MainClass.AddBlankSprdRow(SprdMain, ColReadingDate, ConRowHeight)
                End If
            End If
        End If
        eventArgs.KeyCode = 9999
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim mDate As String
        Dim mTime As String
        Dim I As Integer

        If eventArgs.NewRow = -1 Then Exit Sub

        With SprdMain
            .Row = .ActiveRow
            Select Case eventArgs.col
                Case ColReadingDate
                    .Col = ColReadingDate
                    If Trim(.Text) <> "" Then
                        If IsDate(.Text) = True Then
                            If CheckDateInGrid(.ActiveRow) = False Then
                                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColReadingDate)
                            End If
                        Else
                            MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColReadingDate)
                        End If
                    End If
                Case ColReadingTime
                    .Col = ColReadingTime
                    If Trim(.Text) = "" Then Exit Sub
                    If CheckTimeFormat(.Text) = True Then
                        If CheckTimeInGrid(.ActiveRow) = False Then
                            MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColReadingTime)
                        End If
                    Else
                        MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColReadingTime)
                    End If
                Case ColHoursMtrReading
                    .Col = ColHoursMtrReading
                    If Trim(.Text) <> "" Then
                        If CheckReadingInGrid(.ActiveRow, ColHoursMtrReading) = False Then
                            MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColHoursMtrReading)
                        End If
                    Else
                        MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColHoursMtrReading)
                    End If
                Case ColUnitMtrReading
                    .Col = ColUnitMtrReading
                    If Trim(.Text) <> "" Then
                        If CheckReadingInGrid(.ActiveRow, ColUnitMtrReading) = False Then
                            MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColUnitMtrReading)
                        Else
                            For I = .MaxRows To 1 Step -1
                                .Row = I
                                If Val(.Text) <> 0 Then
                                    txtUnits.Text = CStr(Val(.Text) - Val(txtLastUnitReading.Text))
                                    txtUnits.Text = VB6.Format(txtUnits.Text, "0.00")
                                    Exit For
                                End If
                            Next
                        End If
                    Else
                        MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColUnitMtrReading)
                    End If
            End Select
        End With
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Function CheckDateInGrid(ByVal pRow As Integer) As Boolean
        On Error GoTo err_Renamed
        Dim mFirstDate As String
        Dim mSecondDate As String

        CheckDateInGrid = True

        If pRow = 1 Then
            mFirstDate = Trim(txtOnDate.Text)
        Else
            SprdMain.Row = pRow - 1
            SprdMain.Col = ColReadingDate
            mFirstDate = Trim(SprdMain.Text)
        End If
        SprdMain.Row = pRow
        SprdMain.Col = ColReadingDate
        mSecondDate = Trim(SprdMain.Text)

        If mFirstDate = "" Or mFirstDate = "__/__/____" Or mFirstDate = "/  /" Then Exit Function
        If mSecondDate = "" Or mSecondDate = "__/__/____" Or mSecondDate = "/  /" Then Exit Function

        If CDate(mFirstDate) > CDate(mSecondDate) Then
            MsgBox("Date cann't be greater than Previous Date")
            CheckDateInGrid = False
            Exit Function
        End If
        Exit Function
err_Renamed:
        CheckDateInGrid = False
    End Function

    Private Function CheckTimeInGrid(ByVal pRow As Integer) As Boolean
        On Error GoTo err_Renamed
        Dim mFirstDate As String
        Dim mFirstTime As String
        Dim mSecondDate As String
        Dim mSecondTime As String

        CheckTimeInGrid = True

        If pRow = 1 Then
            mFirstDate = Trim(txtOnDate.Text)
            mFirstTime = Trim(txtOnTime.Text)
        Else
            SprdMain.Row = pRow - 1
            SprdMain.Col = ColReadingDate
            mFirstDate = Trim(SprdMain.Text)
            SprdMain.Col = ColReadingTime
            mFirstTime = Trim(SprdMain.Text)
        End If
        SprdMain.Row = pRow
        SprdMain.Col = ColReadingDate
        mSecondDate = Trim(SprdMain.Text)
        SprdMain.Col = ColReadingTime
        mSecondTime = Trim(SprdMain.Text)

        If mFirstDate = "" Or mFirstDate = "__/__/____" Then Exit Function
        If mFirstTime = "" Or mFirstTime = "__:__" Then Exit Function
        If mSecondDate = "" Or mSecondDate = "__/__/____" Then Exit Function
        If mSecondTime = "" Or mSecondTime = "__:__" Then Exit Function

        If CDate(mFirstDate) = CDate(mSecondDate) Then
            If Val(Replace(mFirstTime, ":", ".")) > 0 Then
                If Val(Replace(mFirstTime, ":", ".")) > Val(Replace(mSecondTime, ":", ".")) Then
                    MsgBox("Time cann't be greater than Previous Time")
                    CheckTimeInGrid = False
                    Exit Function
                End If
            End If
        End If
        Exit Function
err_Renamed:
        CheckTimeInGrid = False
    End Function

    Private Function CheckReadingInGrid(ByVal pRow As Integer, ByVal pCol As Integer) As Boolean
        On Error GoTo err_Renamed
        Dim mFirstReading As Double
        Dim mSecondReading As Double

        CheckReadingInGrid = True

        If pRow = 1 Then
            mFirstReading = Val(IIf(pCol = ColHoursMtrReading, txtLastReading.Text, txtLastUnitReading.Text))
        Else
            SprdMain.Row = pRow - 1
            SprdMain.Col = pCol
            mFirstReading = Val(SprdMain.Text)
        End If
        SprdMain.Row = pRow
        SprdMain.Col = pCol
        mSecondReading = Val(SprdMain.Text)

        If mFirstReading = mSecondReading Then
            MsgBox("Meter Reading cann't be same with Previous Reading")
            CheckReadingInGrid = False
            Exit Function
        ElseIf mFirstReading > mSecondReading Then
            MsgBox("Meter Reading cann't be greater than Previous Reading")
            CheckReadingInGrid = False
            Exit Function
        End If
        Exit Function
err_Renamed:
        CheckReadingInGrid = False
    End Function

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = eventArgs.row
        txtNumber.Text = SprdView.Text
        txtNumber_Validating(txtNumber, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Private Sub txtDoneBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDoneBy.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDoneBy_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDoneBy.DoubleClick
        Call cmdSearchDoneBy_Click(cmdSearchDoneBy, New System.EventArgs())
    End Sub

    Private Sub txtDoneBy_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDoneBy.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchDoneBy_Click(cmdSearchDoneBy, New System.EventArgs())
    End Sub

    Private Sub txtDoneBy_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDoneBy.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim SqlStr As String
        If Trim(txtDoneBy.Text) = "" Then GoTo EventExitSub
        txtDoneBy.Text = VB6.Format(txtDoneBy.Text, "000000")

        If ADDMode = True Then
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND EMP_LEAVE_DATE IS NULL "
        End If

        If MainClass.ValidateWithMasterTable(txtDoneBy.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("EMPLOYEE Does Not Exist In Master.")
            Cancel = True
        Else
            lblDoneBy.text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub Show1()

        On Error GoTo ShowErrPart

        Clear1()

        If Not RsGenRecHdr.EOF Then
            lblMkey.Text = IIf(IsDbNull(RsGenRecHdr.Fields("AUTO_KEY_GENREC").Value), "", RsGenRecHdr.Fields("AUTO_KEY_GENREC").Value)
            txtNumber.Text = IIf(IsDbNull(RsGenRecHdr.Fields("AUTO_KEY_GENREC").Value), "", RsGenRecHdr.Fields("AUTO_KEY_GENREC").Value)
            txtMachineNo.Text = IIf(IsDbNull(RsGenRecHdr.Fields("MACHINE_NO").Value), "", RsGenRecHdr.Fields("MACHINE_NO").Value)
            txtOnDate.Text = IIf(IsDbNull(RsGenRecHdr.Fields("ON_DATE").Value), "", VB6.Format(RsGenRecHdr.Fields("ON_DATE").Value, "DD/MM/YYYY"))
            txtOnTime.Text = IIf(IsDbNull(RsGenRecHdr.Fields("ON_TIME").Value), "", VB6.Format(RsGenRecHdr.Fields("ON_TIME").Value, "HH:MM"))
            txtOffDate.Text = IIf(IsDbNull(RsGenRecHdr.Fields("OFF_DATE").Value), "", VB6.Format(RsGenRecHdr.Fields("OFF_DATE").Value, "DD/MM/YYYY"))
            txtOffTime.Text = IIf(IsDbNull(RsGenRecHdr.Fields("OFF_TIME").Value), "", VB6.Format(RsGenRecHdr.Fields("OFF_TIME").Value, "HH:MM"))
            txtTotalTime.Text = IIf(IsDbNull(RsGenRecHdr.Fields("TOTAL_TIME").Value), "", RsGenRecHdr.Fields("TOTAL_TIME").Value)
            txtDoneBy.Text = IIf(IsDbNull(RsGenRecHdr.Fields("DONE_BY").Value), "", RsGenRecHdr.Fields("DONE_BY").Value)
            txtDoneBy_Validating(txtDoneBy, New System.ComponentModel.CancelEventArgs(False))
            txtRemarks.Text = IIf(IsDbNull(RsGenRecHdr.Fields("REMARKS").Value), "", RsGenRecHdr.Fields("REMARKS").Value)
            txtUnits.Text = IIf(IsDbNull(RsGenRecHdr.Fields("UNITS_GENERATED").Value), "", RsGenRecHdr.Fields("UNITS_GENERATED").Value)
            txtMachineNo_Validating(txtMachineNo, New System.ComponentModel.CancelEventArgs(False))

            Call ShowDetail1()
            Call MakeEnableDesableField(False)
        End If
        ADDMode = False
        MODIFYMode = False
        SprdMain.Enabled = True    '' False Sandeep 15/05/2022
        txtNumber.Enabled = True
        cmdSearchNumber.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsGenRecHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub

    Private Sub ShowDetail1()

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM MAN_GENREC_DET " & vbCrLf & " WHERE AUTO_KEY_GENREC=" & Val(lblMkey.Text) & "" & vbCrLf & " ORDER BY SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGenRecDet, ADODB.LockTypeEnum.adLockReadOnly)
        With RsGenRecDet
            If .EOF = True Then Exit Sub
            I = 1
            Do While Not .EOF
                SprdMain.Row = I

                SprdMain.Col = ColReadingDate
                SprdMain.Text = IIf(IsDbNull(.Fields("READING_DATE").Value), "", VB6.Format(.Fields("READING_DATE").Value, "DD/MM/YYYY"))

                SprdMain.Col = ColReadingTime
                SprdMain.Text = IIf(IsDbNull(.Fields("READING_TIME").Value), "", VB6.Format(.Fields("READING_TIME").Value, "HH:MM"))

                SprdMain.Col = ColRPM
                SprdMain.Text = IIf(IsDbNull(.Fields("RPM").Value), "", CStr(.Fields("RPM").Value))

                SprdMain.Col = ColHoursMtrReading
                SprdMain.Text = IIf(IsDbNull(.Fields("HRS_MTR_READING").Value), "", CStr(.Fields("HRS_MTR_READING").Value))

                SprdMain.Col = ColUnitMtrReading
                SprdMain.Text = IIf(IsDbNull(.Fields("UNIT_MTR_READING").Value), "", CStr(.Fields("UNIT_MTR_READING").Value))

                SprdMain.Col = ColOilPress
                SprdMain.Text = IIf(IsDbNull(.Fields("OIL_PRESSURE").Value), "", CStr(.Fields("OIL_PRESSURE").Value))

                SprdMain.Col = ColOilTemp
                SprdMain.Text = IIf(IsDbNull(.Fields("OIL_TEMP_IN").Value), "", CStr(.Fields("OIL_TEMP_IN").Value))

                SprdMain.Col = ColWaterTemp
                SprdMain.Text = IIf(IsDbNull(.Fields("WATER_TEMP").Value), "", CStr(.Fields("WATER_TEMP").Value))

                SprdMain.Col = ColFrequency
                SprdMain.Text = IIf(IsDbNull(.Fields("FREQUENCY").Value), "", CStr(.Fields("FREQUENCY").Value))
                SprdMain.Col = ColAMPS
                SprdMain.Text = IIf(IsDbNull(.Fields("AMPS").Value), "", CStr(.Fields("AMPS").Value))

                SprdMain.Col = ColHSPLevel
                SprdMain.Text = IIf(IsDbNull(.Fields("HSP_LEVEL").Value), "", CStr(.Fields("HSP_LEVEL").Value))

                SprdMain.Col = ColLoad
                SprdMain.Text = IIf(IsDbNull(.Fields("Load").Value), "", CStr(.Fields("Load").Value))

                SprdMain.Col = ColRHM
                SprdMain.Text = IIf(IsDbNull(.Fields("RHM").Value), "", CStr(.Fields("RHM").Value))

                SprdMain.Col = ColOilTempOut
                SprdMain.Text = IIf(IsDbNull(.Fields("OIL_TEMP_OUT").Value), "", CStr(.Fields("OIL_TEMP_OUT").Value))

                SprdMain.Col = ColVoltage
                SprdMain.Text = IIf(IsDbNull(.Fields("VOLTAGE").Value), "", CStr(.Fields("VOLTAGE").Value))

                SprdMain.Col = ColKWH
                SprdMain.Text = IIf(IsDbNull(.Fields("KWH").Value), "", CStr(.Fields("KWH").Value))

                SprdMain.Col = ColTemp1
                SprdMain.Text = IIf(IsDbNull(.Fields("CYLINDER1_TEMP").Value), "", CStr(.Fields("CYLINDER1_TEMP").Value))

                SprdMain.Col = ColTemp2
                SprdMain.Text = IIf(IsDbNull(.Fields("CYLINDER2_TEMP").Value), "", CStr(.Fields("CYLINDER2_TEMP").Value))

                SprdMain.Col = ColTemp3
                SprdMain.Text = IIf(IsDbNull(.Fields("CYLINDER3_TEMP").Value), "", CStr(.Fields("CYLINDER3_TEMP").Value))

                SprdMain.Col = ColTemp4
                SprdMain.Text = IIf(IsDbNull(.Fields("CYLINDER4_TEMP").Value), "", CStr(.Fields("CYLINDER4_TEMP").Value))

                SprdMain.Col = ColTemp5
                SprdMain.Text = IIf(IsDbNull(.Fields("CYLINDER5_TEMP").Value), "", CStr(.Fields("CYLINDER5_TEMP").Value))

                SprdMain.Col = ColTemp6
                SprdMain.Text = IIf(IsDbNull(.Fields("CYLINDER6_TEMP").Value), "", CStr(.Fields("CYLINDER6_TEMP").Value))

                SprdMain.Col = ColOilLevel
                SprdMain.Text = IIf(IsDbNull(.Fields("OIL_LEVEL").Value), "", .Fields("OIL_LEVEL").Value)

                SprdMain.Col = ColRemarks
                SprdMain.Text = IIf(IsDbNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)

                .MoveNext()
                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With
        FormatSprdMain(-1)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtMachineNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMachineNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMachineNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMachineNo.DoubleClick
        Call cmdSearchMachine_Click(cmdSearchMachine, New System.EventArgs())
    End Sub

    Private Sub txtMachineNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtMachineNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchMachine_Click(cmdSearchMachine, New System.EventArgs())
    End Sub

    Private Sub txtMachineNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMachineNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ValMacERR
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mOpenDate As String
        Dim mOpenReading As Double
        Dim mOpenUnitReading As Double
        Dim mMaxReading As Double
        Dim mMaxUnitReading As Double

        If Trim(txtMachineNo.Text) = "" Then GoTo EventExitSub

        If ADDMode = True Then
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND MACHINE_UB='N' AND STATUS='O' "
        Else
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND MACHINE_UB='N' "
        End If
        If MainClass.ValidateWithMasterTable(txtMachineNo.Text, "MACHINE_NO", "MACHINE_DESC", "MAN_MACHINE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Machine Does Not Exist In Master.")
            Cancel = True
        Else
            lblMachineNo.Text = MasterNo

            SqlStr = " SELECT OPEN_DATE,OPEN_READING,OPEN_UNIT_READING FROM MAN_GENREC_OPEN " & vbCrLf _
                            & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                            & " AND MACHINE_NO='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "' " & vbCrLf _
                            & " AND OPEN_DATE= (" & vbCrLf _
                            & " SELECT MAX(OPEN_DATE) FROM MAN_GENREC_OPEN " & vbCrLf _
                            & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                            & " AND MACHINE_NO='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "' "
            If Trim(txtOnDate.Text) <> "" Then
                SqlStr = SqlStr & vbCrLf & " AND OPEN_DATE<=TO_DATE('" & VB6.Format(txtOnDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
            End If
            SqlStr = SqlStr & ")"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                mOpenDate = IIf(IsDbNull(RsTemp.Fields("OPEN_DATE").Value), "", RsTemp.Fields("OPEN_DATE").Value)
                mOpenReading = IIf(IsDbNull(RsTemp.Fields("OPEN_READING").Value), "", RsTemp.Fields("OPEN_READING").Value)
                mOpenUnitReading = IIf(IsDbNull(RsTemp.Fields("OPEN_UNIT_READING").Value), "", RsTemp.Fields("OPEN_UNIT_READING").Value)
            End If
            SqlStr = " SELECT MAX(HRS_MTR_READING) AS HRS_MTR_READING, MAX(UNIT_MTR_READING) AS UNIT_MTR_READING FROM MAN_GENREC_DET " & vbCrLf & " WHERE AUTO_KEY_GENREC IN (" & vbCrLf & " SELECT AUTO_KEY_GENREC FROM MAN_GENREC_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND MACHINE_NO='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "' "
            If Trim(mOpenDate) <> "" Then
                SqlStr = SqlStr & vbCrLf & " AND ON_DATE>=TO_DATE('" & VB6.Format(mOpenDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
            End If
            If Trim(txtOnDate.Text) <> "" Then
                SqlStr = SqlStr & vbCrLf & " AND ON_DATE<=TO_DATE('" & VB6.Format(txtOnDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
            End If
            If Val(lblMkey.Text) <> 0 Then
                SqlStr = SqlStr & vbCrLf & " AND AUTO_KEY_GENREC<>" & Val(txtNumber.Text)
            End If
            SqlStr = SqlStr & ")"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                mMaxReading = IIf(IsDbNull(RsTemp.Fields("HRS_MTR_READING").Value), 0, RsTemp.Fields("HRS_MTR_READING").Value)
                mMaxUnitReading = IIf(IsDbNull(RsTemp.Fields("UNIT_MTR_READING").Value), 0, RsTemp.Fields("UNIT_MTR_READING").Value)
            End If
            txtLastReading.Text = IIf(mMaxReading > mOpenReading, mMaxReading, mOpenReading)
            txtLastReading.Text = VB6.Format(txtLastReading.Text, "0.00")
            txtLastUnitReading.Text = IIf(mMaxUnitReading > mOpenUnitReading, mMaxUnitReading, mOpenUnitReading)
            txtLastUnitReading.Text = VB6.Format(txtLastUnitReading.Text, "0.00")
        End If
        GoTo EventExitSub
ValMacERR:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtNumber_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNumber.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtNumber_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNumber.DoubleClick
        Call cmdSearchNumber_Click(cmdSearchNumber, New System.EventArgs())
    End Sub

    Private Sub txtNumber_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtNumber.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchNumber_Click(cmdSearchNumber, New System.EventArgs())
    End Sub

    Private Sub txtNumber_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtNumber.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMKey As Double
        Dim mSlipNo As Double
        Dim SqlStr As String

        If Trim(txtNumber.Text) = "" Then GoTo EventExitSub

        If Len(txtNumber.Text) < 6 Then
            txtNumber.Text = txtNumber.Text & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If
        mSlipNo = Val(txtNumber.Text)

        If MODIFYMode = True And RsGenRecHdr.BOF = False Then xMKey = RsGenRecHdr.Fields("AUTO_KEY_GENREC").Value

        SqlStr = "SELECT * FROM MAN_GENREC_HDR " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND MACHINE_TYPE='" & lblType.Text & "' " & vbCrLf & " AND AUTO_KEY_GENREC=" & mSlipNo & ""

        '            & " AND SUBSTR(AUTO_KEY_GENREC,LENGTH(AUTO_KEY_GENREC)-5,4)=" & RsCompany.fields("FYEAR").value & " " & vbCrLf _
        '
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGenRecHdr, ADODB.LockTypeEnum.adLockReadOnly)
        If RsGenRecHdr.EOF = False Then
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Number. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM MAN_GENREC_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND MACHINE_TYPE='" & lblType.Text & "' " & vbCrLf & " AND AUTO_KEY_GENREC=" & xMKey & " "

                '                & " AND SUBSTR(AUTO_KEY_GENREC,LENGTH(AUTO_KEY_GENREC)-5,4)=" & RsCompany.fields("FYEAR").value & " " & vbCrLf _
                '
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGenRecHdr, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub MakeEnableDesableField(ByRef mMode As Boolean)
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnGenRec(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnGenRec(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportOnGenRec(ByRef Mode As Crystal.DestinationConstants)
    End Sub

    Private Sub txtOffDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOffDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOffDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtOffDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtOffDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtOffDate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
        Else
            If CheckDate(txtOffDate) = False Then Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtOffTime_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOffTime.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOffTime_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtOffTime.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If txtOffTime.Text = "" Then GoTo EventExitSub
        If Not CheckTimeFormat((txtOffTime.Text)) Then
            Cancel = True
        Else
            txtOffTime.Text = VB6.Format(txtOffTime.Text, "HH:MM")
            If CheckTime(txtOffTime) = False Then
                Cancel = True
            End If
            Call CalcTot()
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtOnDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOnDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOnDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtOnDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtOnDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtOnDate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
        Else
            If CheckDate(txtOnDate) = False Then Cancel = True
        End If
        Call txtMachineNo_Validating(txtMachineNo, New System.ComponentModel.CancelEventArgs(False))
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Function CheckDate(ByRef pTextDate As System.Windows.Forms.TextBox) As Boolean
        CheckDate = True
        If Trim(txtOnDate.Text) = "" Or Trim(txtOffDate.Text) = "" Then Exit Function

        If pTextDate.Name = txtOnDate.Name Then
            If CDate(txtOnDate.Text) > CDate(txtOffDate.Text) Then
                MsgBox("On Date cann't be greater than Off Date")
                CheckDate = False
                Exit Function
            End If
        ElseIf pTextDate.Name = txtOffDate.Name Then
            If CDate(txtOffDate.Text) < CDate(txtOnDate.Text) Then
                MsgBox("Off Date cann't be less than On Date")
                CheckDate = False
                Exit Function
            End If
        End If
    End Function

    Private Sub txtOnTime_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOnTime.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOnTime_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtOnTime.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If txtOnTime.Text = "" Then GoTo EventExitSub
        If Not CheckTimeFormat((txtOnTime.Text)) Then
            Cancel = True
            GoTo EventExitSub
        Else
            txtOnTime.Text = VB6.Format(txtOnTime.Text, "HH:MM")
            If CheckTime(txtOnTime) = False Then
                Cancel = True
                GoTo EventExitSub
            End If

        End If
        Call txtMachineNo_Validating(txtMachineNo, New System.ComponentModel.CancelEventArgs(False))
        Call CalcTot()
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Function CheckTimeFormat(ByRef pTime As String) As Boolean
        CheckTimeFormat = True
        If InStr(1, pTime, ":", CompareMethod.Text) <= 0 Then
            MsgBox("Time should be in format of HH24:MI with numeric value")
            CheckTimeFormat = False
        ElseIf InStr(1, pTime, ":", CompareMethod.Text) > 0 Then
            If Not IsNumeric(VB.Left(pTime, InStr(1, pTime, ":", CompareMethod.Text) - 1)) = True Or Not IsNumeric(Mid(pTime, InStr(1, pTime, ":", CompareMethod.Text) + 1)) = True Then
                MsgBox("Time should be in format of HH24:MI with numeric value")
                CheckTimeFormat = False
            ElseIf Val(VB.Left(pTime, InStr(1, pTime, ":", CompareMethod.Text) - 1)) > 23 Then
                MsgBox("HH cann't be greater than 23")
                CheckTimeFormat = False
            ElseIf Val(Mid(pTime, InStr(1, pTime, ":", CompareMethod.Text) + 1)) > 59 Then
                MsgBox("MM cann't be greater than 59")
                CheckTimeFormat = False
            End If
        End If
    End Function

    Private Function CheckTime(ByRef pTextTime As System.Windows.Forms.TextBox) As Boolean
        CheckTime = True
        If Trim(txtOnDate.Text) = "" Or Trim(txtOffDate.Text) = "" Then Exit Function
        If Trim(txtOnTime.Text) = "" Or Trim(txtOffTime.Text) = "" Then Exit Function

        If pTextTime.Text = txtOnTime.Text Then
            If CDate(txtOnDate.Text) = CDate(txtOffDate.Text) Then
                If Val(Replace(txtOnTime.Text, ":", ".")) > 0 Then
                    If Val(Replace(txtOnTime.Text, ":", ".")) > Val(Replace(txtOffTime.Text, ":", ".")) Then
                        MsgBox("On Time cann't be greater than Off Time")
                        CheckTime = False
                        Exit Function
                    End If
                End If
            End If
        ElseIf pTextTime.Text = txtOffTime.Text Then
            If CDate(txtOnDate.Text) = CDate(txtOffDate.Text) Then
                If Val(Replace(txtOffTime.Text, ":", ".")) > 0 Then
                    If Val(Replace(txtOffTime.Text, ":", ".")) < Val(Replace(txtOnTime.Text, ":", ".")) Then
                        MsgBox("Off Time cann't be less than On Time")
                        CheckTime = False
                        Exit Function
                    End If
                End If
            End If
        End If
    End Function

    Private Sub CalcTot()
        Dim mOnDate As String
        Dim mOnTime As String
        Dim mOnDateTime As String
        Dim mOffDate As String
        Dim mOffTime As String
        Dim mOffDateTime As String
        Dim mTotHour As Double
        Dim mTotMin As Double
        Dim mTotTime As Double

        mOnDate = Trim(txtOnDate.Text)
        mOnTime = Trim(txtOnTime.Text)
        mOffDate = Trim(txtOffDate.Text)
        mOffTime = Trim(txtOffTime.Text)

        If mOnDate = "" Or mOnDate = "__/__/____" Then Exit Sub
        If mOnTime = "" Or mOnTime = "__:__" Then Exit Sub
        If mOffDate = "" Or mOffDate = "__/__/____" Then Exit Sub
        If mOffTime = "" Or mOffTime = "__:__" Then Exit Sub
        mOnDateTime = mOnDate & " " & mOnTime
        mOffDateTime = mOffDate & " " & mOffTime

        mTotHour = Int(DateDiff(Microsoft.VisualBasic.DateInterval.Minute, CDate(mOnDateTime), CDate(mOffDateTime)) / 60)
        mTotMin = DateDiff(Microsoft.VisualBasic.DateInterval.Minute, CDate(mOnDateTime), CDate(mOffDateTime)) Mod 60
        If mTotMin < 10 Then
            mTotMin = mTotMin / 100
            mTotTime = mTotHour + mTotMin
        Else
            mTotTime = CDbl(mTotHour & "." & mTotMin)
        End If

        txtTotalTime.Text = CStr(Val(CStr(mTotTime)))
    End Sub

    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTotalTime_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTotalTime.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtUnits_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtUnits.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
End Class
