Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmPMSchd
    Inherits System.Windows.Forms.Form
    Dim RsMachineSchdHdr As ADODB.Recordset
    Dim RsMachineSchdDet As ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean

    Private Const ConRowHeight As Short = 14

    Private Const ColMachineNo As Short = 1
    Private Const ColMachineDesc As Short = 2
    Private Const ColCheckType As Short = 3
    Private Const ColPMDue As Short = 4
    Private Const ColResponsibility As Short = 5
    Private Const ColRemarks As Short = 6
    Private Const ColPMDone As Short = 7
    Private Const ColNotAch As Short = 8
    Private Const ColNextDue As Short = 9

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtNumber.Enabled = False
            cmdSearchNumber.Enabled = False
            cboSchdMonth.Enabled = True
            cboSchdYear.Enabled = True
            SprdMain.Enabled = True
            cmdPopulate.Enabled = True
        Else
            ADDMode = False
            MODIFYMode = False
            If RsMachineSchdHdr.EOF = False Then RsMachineSchdHdr.MoveFirst()
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
        If Not RsMachineSchdHdr.EOF Then
            If PubSuperUser = "U" Then
                If RsMachineSchdHdr.Fields("APP_BY").Value <> "" Then MsgBox("Number been approved, So cann't be deleted") : Exit Sub
            End If
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "MAN_MACHINE_SCHD_HDR", (txtNumber.Text), RsMachineSchdHdr) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM MAN_MACHINE_SCHD_DET WHERE AUTO_KEY_SCHD=" & Val(lblMkey.Text) & "")
                PubDBCn.Execute("DELETE FROM MAN_MACHINE_SCHD_HDR WHERE AUTO_KEY_SCHD=" & Val(lblMkey.Text) & "")
                PubDBCn.CommitTrans()
                RsMachineSchdHdr.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Errors.Clear()
        RsMachineSchdHdr.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr

        If CmdModify.Text = ConcmdmodifyCaption Then
            If PubSuperUser = "U" Then
                If RsMachineSchdHdr.Fields("APP_BY").Value <> "" Then MsgBox("Number been approved, So cann't be modified") : Exit Sub
            End If
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsMachineSchdHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            txtNumber.Enabled = False
            cmdSearchNumber.Enabled = False
            cboSchdMonth.Enabled = False
            cboSchdYear.Enabled = False
            txtAppBy.Enabled = True
            cmdSearchAppBy.Enabled = True
            SprdMain.Enabled = True
            cmdPopulate.Enabled = True
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdPopulate_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPopulate.Click

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String
        Dim mStartDate As Date
        Dim mEndDate As Date
        Dim RsTemp As ADODB.Recordset
        Dim RsTemp1 As ADODB.Recordset
        Dim RsTemp2 As ADODB.Recordset
        Dim mMachineNo As String
        Dim mCheckType As String

        If Trim(cboSchdMonth.Text) = "" Then MsgInformation("Please Select the Month") : Exit Sub
        If Trim(cboSchdYear.Text) = "" Then MsgInformation("Please Select the Year") : Exit Sub
        If IsRecordExist = True Then Exit Sub

        cmdPopulate.Enabled = False

        mStartDate = CDate("01/" & MonthValue((cboSchdMonth.Text)) & "/" & Val(cboSchdYear.Text))
        mEndDate = CDate(MainClass.LastDay(MonthValue((cboSchdMonth.Text)), Val(cboSchdYear.Text)) & "/" & MonthValue((cboSchdMonth.Text)) & "/" & Val(cboSchdYear.Text))

        SqlStr = ""
        SqlStr = " SELECT MAN_MACHINE_MST.*,MAN_MACHINE_MAINT_TRN.CHECK_TYPE,MAN_MACHINE_MAINT_TRN.DUE_PM " & vbCrLf & " FROM MAN_MACHINE_MST,MAN_MACHINE_MAINT_TRN " & vbCrLf & " WHERE MAN_MACHINE_MST.COMPANY_CODE=MAN_MACHINE_MAINT_TRN.COMPANY_CODE " & vbCrLf & " AND MAN_MACHINE_MST.MACHINE_NO=MAN_MACHINE_MAINT_TRN.MACHINE_NO " & vbCrLf & " AND MAN_MACHINE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MAN_MACHINE_MAINT_TRN.DUE_PM BETWEEN TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND STATUS='O' " & vbCrLf & " AND MAINT_TYPE='P' " & vbCrLf & " AND MAN_MACHINE_MAINT_TRN.MACHINE_NO || '-' || MAN_MACHINE_MAINT_TRN.CHECK_TYPE NOT IN ( " & vbCrLf & " SELECT MACHINE_NO || '-' || CHECK_TYPE FROM MAN_MACHINE_SCHD_HDR,MAN_MACHINE_SCHD_DET " & vbCrLf & " WHERE MAN_MACHINE_SCHD_HDR.AUTO_KEY_SCHD=MAN_MACHINE_SCHD_DET.AUTO_KEY_SCHD " & vbCrLf & " AND MAN_MACHINE_SCHD_HDR.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SCHD_MONTH=" & MonthValue((cboSchdMonth.Text)) & vbCrLf & " AND SCHD_YEAR=" & Val(cboSchdYear.Text) & " ) " & vbCrLf & " ORDER BY MAN_MACHINE_MAINT_TRN.DUE_PM"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With RsTemp
            If .EOF = False Then
                I = SprdMain.MaxRows
                Do While Not .EOF
                    SprdMain.Row = I

                    SprdMain.Col = ColMachineNo
                    SprdMain.Text = IIf(IsDbNull(.Fields("MACHINE_NO").Value), "", .Fields("MACHINE_NO").Value)

                    SprdMain.Col = ColMachineDesc
                    SprdMain.Text = IIf(IsDbNull(.Fields("MACHINE_DESC").Value), "", .Fields("MACHINE_DESC").Value)

                    SprdMain.Col = ColCheckType
                    SprdMain.Text = IIf(IsDbNull(.Fields("CHECK_TYPE").Value), "", .Fields("CHECK_TYPE").Value)

                    SprdMain.Col = ColPMDue
                    SprdMain.Text = IIf(IsDbNull(.Fields("DUE_PM").Value), "", .Fields("DUE_PM").Value)

                    .MoveNext()
                    I = I + 1
                    SprdMain.MaxRows = I
                Loop
            End If
        End With

        SqlStr = " SELECT MAN_MACHINE_MST.*,MAN_MACHINE_MAINT_TRN.CHECK_TYPE,MAN_MACHINE_MAINT_TRN.DUE_PM " & vbCrLf & " FROM MAN_MACHINE_MST,MAN_MACHINE_MAINT_TRN " & vbCrLf & " WHERE MAN_MACHINE_MST.COMPANY_CODE=MAN_MACHINE_MAINT_TRN.COMPANY_CODE " & vbCrLf & " AND MAN_MACHINE_MST.MACHINE_NO=MAN_MACHINE_MAINT_TRN.MACHINE_NO " & vbCrLf & " AND MAN_MACHINE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND STATUS='O' " & vbCrLf & " AND MAINT_TYPE='H' " & vbCrLf & " AND MAN_MACHINE_MAINT_TRN.CHECK_HOURS<=( " & vbCrLf & " SELECT SUM(TOTAL_TIME) FROM MAN_GENREC_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MACHINE_NO=MAN_MACHINE_MST.MACHINE_NO " & vbCrLf & " AND ON_DATE>MAN_MACHINE_MAINT_TRN.LAST_PM " & vbCrLf & " AND ON_DATE<=TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) " & vbCrLf & " AND MAN_MACHINE_MAINT_TRN.MACHINE_NO || '-' || MAN_MACHINE_MAINT_TRN.CHECK_TYPE NOT IN ( " & vbCrLf & " SELECT MACHINE_NO || '-' || CHECK_TYPE FROM MAN_MACHINE_SCHD_HDR,MAN_MACHINE_SCHD_DET " & vbCrLf & " WHERE MAN_MACHINE_SCHD_HDR.AUTO_KEY_SCHD=MAN_MACHINE_SCHD_DET.AUTO_KEY_SCHD " & vbCrLf & " AND MAN_MACHINE_SCHD_HDR.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SCHD_MONTH=" & MonthValue((cboSchdMonth.Text)) & vbCrLf & " AND SCHD_YEAR=" & Val(cboSchdYear.Text) & " ) " & vbCrLf & " ORDER BY MAN_MACHINE_MST.MACHINE_NO "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With RsTemp
            If .EOF = False Then
                I = SprdMain.MaxRows
                Do While Not .EOF
                    SprdMain.Row = I

                    mMachineNo = IIf(IsDbNull(.Fields("MACHINE_NO").Value), "", .Fields("MACHINE_NO").Value)
                    mCheckType = IIf(IsDbNull(.Fields("CHECK_TYPE").Value), "", .Fields("CHECK_TYPE").Value)

                    SqlStr = " SELECT MAX(ON_DATE) AS DUE_DATE FROM MAN_GENREC_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MACHINE_NO='" & MainClass.AllowSingleQuote(mMachineNo) & "' " & vbCrLf & " AND ON_DATE BETWEEN TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp1, ADODB.LockTypeEnum.adLockReadOnly)
                    If Not RsTemp1.EOF Then
                        If IsDbNull(RsTemp1.Fields("DUE_DATE").Value) Then
                            SqlStr = " SELECT LAST_PM, DUE_PM FROM MAN_MACHINE_MAINT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MACHINE_NO='" & MainClass.AllowSingleQuote(mMachineNo) & "' " & vbCrLf & " AND CHECK_TYPE='" & MainClass.AllowSingleQuote(mCheckType) & "' " & vbCrLf & " AND DUE_PM BETWEEN TO_DATE('" & VB6.Format(mStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(mEndDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

                            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp2, ADODB.LockTypeEnum.adLockReadOnly)
                            If Not RsTemp2.EOF Then
                                If Not IsDbNull(RsTemp2.Fields("LAST_PM").Value) And Not IsDbNull(RsTemp2.Fields("DUE_PM").Value) Then
                                    If RsTemp2.Fields("DUE_PM").Value >= RsTemp2.Fields("LAST_PM").Value Then
                                        SprdMain.Col = ColMachineNo
                                        SprdMain.Text = IIf(IsDbNull(.Fields("MACHINE_NO").Value), "", .Fields("MACHINE_NO").Value)

                                        SprdMain.Col = ColMachineDesc
                                        SprdMain.Text = IIf(IsDbNull(.Fields("MACHINE_DESC").Value), "", .Fields("MACHINE_DESC").Value)

                                        SprdMain.Col = ColCheckType
                                        SprdMain.Text = IIf(IsDbNull(.Fields("CHECK_TYPE").Value), "", .Fields("CHECK_TYPE").Value)

                                        SprdMain.Col = ColPMDue
                                        SprdMain.Text = IIf(IsDbNull(RsTemp2.Fields("DUE_PM").Value), "", RsTemp2.Fields("DUE_PM").Value)

                                        I = I + 1
                                    End If
                                End If
                            End If
                        Else
                            SprdMain.Col = ColMachineNo
                            SprdMain.Text = IIf(IsDbNull(.Fields("MACHINE_NO").Value), "", .Fields("MACHINE_NO").Value)

                            SprdMain.Col = ColMachineDesc
                            SprdMain.Text = IIf(IsDbNull(.Fields("MACHINE_DESC").Value), "", .Fields("MACHINE_DESC").Value)

                            SprdMain.Col = ColCheckType
                            SprdMain.Text = IIf(IsDbNull(.Fields("CHECK_TYPE").Value), "", .Fields("CHECK_TYPE").Value)

                            SprdMain.Col = ColPMDue
                            SprdMain.Text = IIf(IsDbNull(RsTemp1.Fields("DUE_DATE").Value), "", RsTemp1.Fields("DUE_DATE").Value + 1)

                            I = I + 1
                        End If
                    End If

                    .MoveNext()
                    SprdMain.MaxRows = I
                Loop
            End If
        End With
        FormatSprdMain(-1)
        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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
        If MODIFYMode = True Then Exit Function
        SqlStr = " SELECT AUTO_KEY_SCHD " & vbCrLf & " From MAN_MACHINE_SCHD_HDR " & vbCrLf & " WHERE COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SCHD_MONTH =" & MonthValue((cboSchdMonth.Text)) & " " & vbCrLf & " AND SCHD_YEAR =" & Val(cboSchdYear.Text) & " "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With RsTemp
            If Not .EOF Then
                MsgBox("This entry already exist in Number : " & .Fields("AUTO_KEY_SCHD").Value)
                IsRecordExist = True
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
            SqlStr = " INSERT INTO MAN_MACHINE_SCHD_HDR " & vbCrLf _
                            & " (AUTO_KEY_SCHD,COMPANY_CODE, " & vbCrLf _
                            & " SCHD_MONTH,SCHD_YEAR, " & vbCrLf _
                            & " PREP_BY,APP_BY, " & vbCrLf _
                            & " ADDUSER,ADDDATE,MODUSER,MODDATE) " & vbCrLf _
                            & " VALUES ( " & vbCrLf _
                            & " " & mNumber & "," & RsCompany.fields("COMPANY_CODE").value & ", " & vbCrLf _
                            & " " & MonthValue(cboSchdMonth.Text) & ", " & Val(cboSchdYear.Text) & ", " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtPrepBy.Text) & "','" & MainClass.AllowSingleQuote(txtAppBy.Text) & "'," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"
        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE MAN_MACHINE_SCHD_HDR SET " & vbCrLf _
                    & " AUTO_KEY_SCHD=" & mNumber & ", " & vbCrLf _
                    & " SCHD_MONTH=" & MonthValue(cboSchdMonth.Text) & ", " & vbCrLf _
                    & " SCHD_YEAR=" & Val(cboSchdYear.Text) & ", " & vbCrLf _
                    & " PREP_BY='" & MainClass.AllowSingleQuote(txtPrepBy.Text) & "'," & vbCrLf _
                    & " APP_BY='" & MainClass.AllowSingleQuote(txtAppBy.Text) & "'," & vbCrLf _
                    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                    & " MODDATE=TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                    & " AND AUTO_KEY_SCHD =" & Val(lblMkey.text) & ""
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
        RsMachineSchdHdr.Requery()
        RsMachineSchdDet.Requery()
        MsgBox(Err.Description)
        ''Resume
    End Function

    Private Function AutoGenKeyNo() As Double

        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset
        Dim mAutoGen As Double
        Dim SqlStr As String
        Dim mMaxValue As String

        mAutoGen = 1
        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_SCHD)  " & vbCrLf & " FROM MAN_MACHINE_SCHD_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_SCHD,LENGTH(AUTO_KEY_SCHD)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

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
        Dim mMachineNo As String
        Dim mCheckType As String
        Dim mResponsibility As String
        Dim mRemarks As String
        Dim mPMDue As String
        Dim mPMDone As String
        Dim mNotAch As String
        Dim mNextDue As String

        PubDBCn.Execute("DELETE FROM MAN_MACHINE_SCHD_DET WHERE AUTO_KEY_SCHD=" & Val(lblMkey.Text) & "")

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColMachineNo
                mMachineNo = MainClass.AllowSingleQuote(.Text)

                .Col = ColCheckType
                mCheckType = MainClass.AllowSingleQuote(.Text)

                .Col = ColResponsibility
                mResponsibility = MainClass.AllowSingleQuote(.Text)

                .Col = ColRemarks
                mRemarks = MainClass.AllowSingleQuote(.Text)

                .Col = ColPMDue
                mPMDue = Trim(.Text)

                .Col = ColPMDone
                mPMDone = Trim(.Text)

                .Col = ColNotAch
                mNotAch = Trim(.Text)

                .Col = ColNextDue
                mNextDue = Trim(.Text)

                SqlStr = ""

                If mMachineNo <> "" Then
                    SqlStr = " INSERT INTO MAN_MACHINE_SCHD_DET ( " & vbCrLf & " COMPANY_CODE,AUTO_KEY_SCHD,MACHINE_NO,CHECK_TYPE,RESPONSIBILITY, " & vbCrLf & " REMARKS,PM_DUE,PM_DONE,NOT_ACH_REASON,NEXT_DUE ) " & vbCrLf & " VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & "," & Val(lblMkey.Text) & ",'" & mMachineNo & "','" & mCheckType & "', " & vbCrLf & " '" & mResponsibility & "','" & mRemarks & "', " & vbCrLf & " TO_DATE('" & VB6.Format(mPMDue, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(mPMDone, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & mNotAch & "',TO_DATE('" & VB6.Format(mNextDue, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

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

    Private Sub cmdSearchAppBy_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchAppBy.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND EMP_LEAVE_DATE IS NULL "
        If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            txtAppBy.Text = AcName1
            lblAppBy.text = AcName
        End If
    End Sub

    Private Sub cmdSearchPrepBy_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchPrepBy.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND EMP_LEAVE_DATE IS NULL "
        If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            txtPrepBy.Text = AcName1
            lblPrepBy.text = AcName
        End If
    End Sub

    Private Sub cmdSearchNumber_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchNumber.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        '            & " AND SUBSTR(AUTO_KEY_SCHD,LENGTH(AUTO_KEY_SCHD)-5,4)=" & RsCompany.fields("FYEAR").value & ""
        If MainClass.SearchGridMaster(txtNumber.Text, "MAN_MACHINE_SCHD_HDR", "AUTO_KEY_SCHD", "SCHD_MONTH", "SCHD_YEAR", , SqlStr) = True Then
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
        MainClass.ButtonStatus(Me, XRIGHT, RsMachineSchdHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmPMSchd_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Preventive Maintenance Schedule"

        SqlStr = "Select * From MAN_MACHINE_SCHD_HDR WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMachineSchdHdr, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From MAN_MACHINE_SCHD_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMachineSchdDet, ADODB.LockTypeEnum.adLockReadOnly)

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

        SqlStr = " SELECT " & vbCrLf _
            & " AUTO_KEY_SCHD AS SCHD_NUMBER,SCHD_MONTH AS MONTH,SCHD_YEAR AS YEAR, " & vbCrLf _
            & " PREP_BY ,APP_BY " & vbCrLf _
            & " FROM MAN_MACHINE_SCHD_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " ORDER BY SCHD_YEAR, SCHD_MONTH "

        '            & " AND SUBSTR(AUTO_KEY_SCHD,LENGTH(AUTO_KEY_SCHD)-5,4)=" & RsCompany.fields("FYEAR").value & "" & vbCrLf _
        '
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmPMSchd_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmPMSchd_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        Me.Height = VB6.TwipsToPixelsY(7395)
        Me.Width = VB6.TwipsToPixelsX(11520)

        cboSchdMonth.Items.Clear()
        For I = 1 To 12
            cboSchdMonth.Items.Add(MonthName(I))
        Next

        cboSchdYear.Items.Clear()
        For I = 1970 To 2200
            cboSchdYear.Items.Add(CStr(I))
        Next

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
        cboSchdMonth.Text = MonthName(Month(RunDate))
        cboSchdYear.Text = CStr(Year(RunDate))
        txtPrepBy.Text = ""
        lblPrepBy.Text = ""
        txtAppBy.Text = ""
        lblAppBy.Text = ""
        Call MakeEnableDesableField(True)
        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsMachineSchdHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ClearErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub

    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim SqlStr As String

        With SprdMain
            .set_RowHeight(-1, ConRowHeight)
            .Row = Arow

            .Col = ColMachineNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsMachineSchdDet.Fields("MACHINE_NO").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False

            .Col = ColMachineDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = 255
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColCheckType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsMachineSchdDet.Fields("CHECK_TYPE").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColPMDue
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditMultiLine = False

            .Col = ColResponsibility
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsMachineSchdDet.Fields("RESPONSIBILITY").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsMachineSchdDet.Fields("REMARKS").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColPMDone
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditMultiLine = False

            .Col = ColNotAch
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditMultiLine = False
            .ColHidden = True

            .Col = ColNextDue
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditMultiLine = False
            .ColHidden = True

            MainClass.ProtectCell(SprdMain, 1, .MaxRows, ColMachineNo, ColPMDue)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, ColPMDone, ColPMDone)
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
            .set_ColWidth(4, 1500)
            .set_ColWidth(5, 1500)
            .set_ColWidth(6, 1500)
            .ColsFrozen = 2


            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub

    Private Sub SetTextLengths()
        On Error GoTo ERR1
        txtNumber.Maxlength = RsMachineSchdHdr.Fields("AUTO_KEY_SCHD").Precision
        txtPrepBy.Maxlength = RsMachineSchdHdr.Fields("PREP_BY").DefinedSize
        txtAppBy.Maxlength = RsMachineSchdHdr.Fields("APP_BY").DefinedSize
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
        If MODIFYMode = True And RsMachineSchdHdr.EOF = True Then Exit Function

        If Trim(cboSchdMonth.Text) = "" Then
            MsgInformation("Month is empty, So unable to save.")
            cboSchdMonth.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(cboSchdYear.Text) = "" Then
            MsgInformation("Year is empty, So unable to save.")
            cboSchdYear.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtPrepBy.Text) = "" Then
            MsgInformation("Prepared By is empty, So unable to save.")
            txtPrepBy.Focus()
            FieldsVarification = False
            Exit Function
        End If

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        ''Resume
    End Function

    Private Sub frmPMSchd_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        RsMachineSchdHdr.Close()
        RsMachineSchdHdr = Nothing
        RsMachineSchdDet.Close()
        RsMachineSchdDet = Nothing
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

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

    Private Sub txtAppBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAppBy.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAppBy_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAppBy.DoubleClick
        Call cmdSearchAppBy_Click(cmdSearchAppBy, New System.EventArgs())
    End Sub

    Private Sub txtAppBy_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtAppBy.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchAppBy_Click(cmdSearchAppBy, New System.EventArgs())
    End Sub

    Private Sub txtAppBy_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAppBy.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim SqlStr As String
        If Trim(txtAppBy.Text) = "" Then lblAppBy.Text = "" : GoTo EventExitSub
        txtAppBy.Text = VB6.Format(txtAppBy.Text, "000000")
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND EMP_LEAVE_DATE IS NULL "
        If MainClass.ValidateWithMasterTable(txtAppBy.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("EMPLOYEE Does Not Exist In Master.")
            Cancel = True
        Else
            lblAppBy.text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPrepBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPrepBy.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPrepBy_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPrepBy.DoubleClick
        Call cmdSearchPrepBy_Click(cmdSearchPrepBy, New System.EventArgs())
    End Sub

    Private Sub txtPrepBy_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPrepBy.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchPrepBy_Click(cmdSearchPrepBy, New System.EventArgs())
    End Sub

    Private Sub txtPrepBy_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPrepBy.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim SqlStr As String
        If Trim(txtPrepBy.Text) = "" Then GoTo EventExitSub
        txtPrepBy.Text = VB6.Format(txtPrepBy.Text, "000000")
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND EMP_LEAVE_DATE IS NULL "
        If MainClass.ValidateWithMasterTable(txtPrepBy.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("EMPLOYEE Does Not Exist In Master.")
            Cancel = True
        Else
            lblPrepBy.text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtNumber_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNumber.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub Show1()

        On Error GoTo ShowErrPart

        Clear1()

        If Not RsMachineSchdHdr.EOF Then
            lblMkey.Text = IIf(IsDbNull(RsMachineSchdHdr.Fields("AUTO_KEY_SCHD").Value), "", RsMachineSchdHdr.Fields("AUTO_KEY_SCHD").Value)
            txtNumber.Text = IIf(IsDbNull(RsMachineSchdHdr.Fields("AUTO_KEY_SCHD").Value), "", RsMachineSchdHdr.Fields("AUTO_KEY_SCHD").Value)
            cboSchdMonth.Text = IIf(IsDbNull(RsMachineSchdHdr.Fields("SCHD_MONTH").Value), "", MonthName(RsMachineSchdHdr.Fields("SCHD_MONTH").Value))
            cboSchdYear.Text = IIf(IsDbNull(RsMachineSchdHdr.Fields("SCHD_YEAR").Value), "", RsMachineSchdHdr.Fields("SCHD_YEAR").Value)
            txtPrepBy.Text = IIf(IsDbNull(RsMachineSchdHdr.Fields("PREP_BY").Value), "", RsMachineSchdHdr.Fields("PREP_BY").Value)
            txtPrepBy_Validating(txtPrepBy, New System.ComponentModel.CancelEventArgs(False))
            txtAppBy.Text = IIf(IsDbNull(RsMachineSchdHdr.Fields("APP_BY").Value), "", RsMachineSchdHdr.Fields("APP_BY").Value)
            txtAppBy_Validating(txtAppBy, New System.ComponentModel.CancelEventArgs(False))

            Call ShowDetail1()
            Call MakeEnableDesableField(False)
        End If
        ADDMode = False
        MODIFYMode = False
        SprdMain.Enabled = True    '' False Sandeep 15/05/2022
        cmdPopulate.Enabled = False
        txtNumber.Enabled = True
        cmdSearchNumber.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsMachineSchdHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub

    Private Sub ShowDetail1()

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String
        Dim mMachineNo As String

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM MAN_MACHINE_SCHD_DET " & vbCrLf & " WHERE AUTO_KEY_SCHD=" & Val(lblMkey.Text) & "" & vbCrLf & " ORDER BY PM_DUE, MACHINE_NO, CHECK_TYPE "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMachineSchdDet, ADODB.LockTypeEnum.adLockReadOnly)
        With RsMachineSchdDet
            If .EOF = True Then Exit Sub
            I = 1
            Do While Not .EOF
                SprdMain.Row = I

                SprdMain.Col = ColMachineNo
                SprdMain.Text = IIf(IsDbNull(.Fields("MACHINE_NO").Value), "", .Fields("MACHINE_NO").Value)
                mMachineNo = SprdMain.Text

                SprdMain.Col = ColMachineDesc
                MainClass.ValidateWithMasterTable(mMachineNo, "MACHINE_NO", "MACHINE_DESC", "MAN_MACHINE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                SprdMain.Text = MasterNo

                SprdMain.Col = ColCheckType
                SprdMain.Text = IIf(IsDbNull(.Fields("CHECK_TYPE").Value), "", .Fields("CHECK_TYPE").Value)

                SprdMain.Col = ColResponsibility
                SprdMain.Text = IIf(IsDbNull(.Fields("RESPONSIBILITY").Value), "", .Fields("RESPONSIBILITY").Value)

                SprdMain.Col = ColRemarks
                SprdMain.Text = IIf(IsDbNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)

                SprdMain.Col = ColPMDue
                SprdMain.Text = IIf(IsDbNull(.Fields("PM_DUE").Value), "", .Fields("PM_DUE").Value)

                SprdMain.Col = ColPMDone
                SprdMain.Text = IIf(IsDbNull(.Fields("PM_DONE").Value), "", .Fields("PM_DONE").Value)

                SprdMain.Col = ColNotAch
                SprdMain.Text = IIf(IsDbNull(.Fields("NOT_ACH_REASON").Value), "", .Fields("NOT_ACH_REASON").Value)

                SprdMain.Col = ColNextDue
                SprdMain.Text = IIf(IsDbNull(.Fields("NEXT_DUE").Value), "", .Fields("NEXT_DUE").Value)

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

        If MODIFYMode = True And RsMachineSchdHdr.BOF = False Then xMKey = RsMachineSchdHdr.Fields("AUTO_KEY_SCHD").Value

        SqlStr = "SELECT * FROM MAN_MACHINE_SCHD_HDR " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND AUTO_KEY_SCHD=" & mSlipNo & ""

        '            & " AND SUBSTR(AUTO_KEY_SCHD,LENGTH(AUTO_KEY_SCHD)-5,4)=" & RsCompany.fields("FYEAR").value & " " & vbCrLf _
        '
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMachineSchdHdr, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMachineSchdHdr.EOF = False Then
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Number. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM MAN_MACHINE_SCHD_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND AUTO_KEY_SCHD=" & xMKey & " "

                '                & " AND SUBSTR(AUTO_KEY_SCHD,LENGTH(AUTO_KEY_SCHD)-5,4)=" & RsCompany.fields("FYEAR").value & " " & vbCrLf _
                '
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMachineSchdHdr, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub MakeEnableDesableField(ByRef mMode As Boolean)
        txtPrepBy.Enabled = mMode
        cmdSearchPrepBy.Enabled = mMode
        txtAppBy.Enabled = mMode
        cmdSearchAppBy.Enabled = mMode
    End Sub

    Private Sub ReportOnPMSchd(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String

        frmPrintPMSchd.ShowDialog()

        If G_PrintLedg = False Then
            Exit Sub
        End If

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        mTitle = "Preventive Maintenance Schedule"

        SqlStr = " SELECT MAN_MACHINE_SCHD_HDR.*, MAN_MACHINE_SCHD_DET.*, MAN_MACHINE_MST.*, " & vbCrLf & " PREP.EMP_NAME,APP.EMP_NAME " & vbCrLf & " FROM MAN_MACHINE_SCHD_HDR, MAN_MACHINE_SCHD_DET, MAN_MACHINE_MST, " & vbCrLf & " PAY_EMPLOYEE_MST PREP, PAY_EMPLOYEE_MST APP " & vbCrLf & " WHERE MAN_MACHINE_SCHD_HDR.AUTO_KEY_SCHD=MAN_MACHINE_SCHD_DET.AUTO_KEY_SCHD(+) " & vbCrLf & " AND MAN_MACHINE_SCHD_HDR.COMPANY_CODE=PREP.COMPANY_CODE(+) " & vbCrLf & " AND MAN_MACHINE_SCHD_HDR.PREP_BY=PREP.EMP_CODE(+) " & vbCrLf & " AND MAN_MACHINE_SCHD_HDR.COMPANY_CODE=APP.COMPANY_CODE(+) " & vbCrLf & " AND MAN_MACHINE_SCHD_HDR.APP_BY=APP.EMP_CODE(+) " & vbCrLf & " AND SUBSTR(MAN_MACHINE_SCHD_DET.AUTO_KEY_SCHD,LENGTH(MAN_MACHINE_SCHD_DET.AUTO_KEY_SCHD)-1,2)=MAN_MACHINE_MST.COMPANY_CODE (+) " & vbCrLf & " AND MAN_MACHINE_SCHD_DET.MACHINE_NO=MAN_MACHINE_MST.MACHINE_NO (+) " & vbCrLf & " AND MAN_MACHINE_SCHD_HDR.AUTO_KEY_SCHD=" & Val(txtNumber.Text) & ""

        If frmPrintPMSchd.optPMDone.Checked = True Then
            mSubTitle = " [Machines for which PM is Done] "
            SqlStr = SqlStr & vbCrLf & " AND MAN_MACHINE_SCHD_DET.PM_DONE IS NOT NULL "
        ElseIf frmPrintPMSchd.optPMNotDone.Checked = True Then
            mSubTitle = " [Machines for which PM is not Done] "
            SqlStr = SqlStr & vbCrLf & " AND MAN_MACHINE_SCHD_DET.PM_DONE IS NULL "
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY MAN_MACHINE_SCHD_DET.PM_DUE "

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\PMSchd.rpt"

        SetCrpt(Report1, Mode, 1, mTitle, mSubTitle, True)
        Report1.SQLQuery = SqlStr
        Report1.WindowShowGroupTree = False
        Report1.Action = 1

        frmPrintPMSchd.Close()

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnPMSchd(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnPMSchd(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
End Class
