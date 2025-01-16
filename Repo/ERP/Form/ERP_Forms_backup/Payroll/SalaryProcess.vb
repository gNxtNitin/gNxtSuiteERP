Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class FrmSalaryProcess
    Inherits System.Windows.Forms.Form
    'Dim PvtDBCn As ADODB.Connection
    Dim mLoanDate As String
    Dim mLoanAmount As Double

    Dim mPFRate As Double
    Dim mPFEPFRate As Double
    Dim mPFPensionRate As Double
    Dim mPFCeiling As Double
    Dim mESICeiling As Double
    Dim mESIRate As Double
    Dim mEmplerPFCont As String
    Dim ConWorkDay As Double
    Private Const ConWorkHour As Short = 8
    Dim mCurrentFYNo As Integer
    Dim XRIGHT As String
    Private Function GetPreviousPer(ByRef xEmpCode As String, ByRef xAppDate As String, ByRef xSalHeadCode As Integer) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = " SELECT PERCENTAGE " & vbCrLf & " FROM PAY_SALARYDEF_MST " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE = '" & xEmpCode & "'" & vbCrLf & " AND ADD_DEDUCTCODE=" & xSalHeadCode & " " & vbCrLf & " AND SALARY_APP_DATE=( SELECT MAX(SALARY_APP_DATE)" & vbCrLf & " FROM PAY_SALARYDEF_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE = '" & xEmpCode & "'" & vbCrLf & " AND SALARY_APP_DATE < TO_DATE('" & VB6.Format(xAppDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) " & vbCrLf & " AND IS_ARREAR='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetPreviousPer = IIf(IsDbNull(RsTemp.Fields("PERCENTAGE").Value), 0, RsTemp.Fields("PERCENTAGE").Value)
        End If

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
    End Sub
    Private Sub cmdOk_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdOK.Click

        On Error GoTo ErrPart
        Dim mDate As String
        Dim mAuthorisation As String

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)

        lblNewDate.Text = VB6.Format("01/" & VB6.Format(txtMonth.Text, "MM/YYYY"))

        mAuthorisation = IIf(InStr(1, XRIGHT, "S") > 0, "Y", "N")
        'MsgBox("OK")
        If lblEmpType.Text = "D" Then

        Else
            'If mAuthorisation = "N" Then
            '    MsgBox("You have not Rights to Process it.", MsgBoxStyle.Critical)
            '    Exit Sub
            'End If
        End If

        If lblProcessType.Text = "SAL" Then
            If ValidateBookLocking(PubDBCn, CInt(ConLockEmpSalaryProcess), VB6.Format(lblNewDate.Text, "DD/MM/YYYY")) = True Then
                Exit Sub
            End If
        Else
            If ValidateBookLocking(PubDBCn, CInt(ConLockEmpOTProcess), VB6.Format(lblNewDate.Text, "DD/MM/YYYY")) = True Then
                Exit Sub
            End If
        End If

        If FieldVarification("S") = False Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        PBar.Visible = True
        mDate = MainClass.LastDay(Month(CDate(lblNewDate.Text)), Year(CDate(lblNewDate.Text))) & "/" & Month(CDate(lblNewDate.Text)) & "/" & Year(CDate(lblNewDate.Text))

        mCurrentFYNo = GetCurrentFYNo(PubDBCn, mDate)

        If mCurrentFYNo = -1 Then
            Exit Sub
        End If

        Call CheckPFRates(CDate(VB6.Format(mDate, "dd/mm/yyyy")))
        Call CheckESIRates(CDate(VB6.Format(mDate, "dd/mm/yyyy")))

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        If lblEmpType.Text = "D" Then
            If SalDummyProcess("A") = False Then GoTo ErrPart
            If OTDummyProcess("A") = False Then GoTo ErrPart
            If SalDummyProcess("F") = False Then GoTo ErrPart
        Else
            If SalProcess("A") = False Then GoTo ErrPart
            If OTProcess("A") = False Then GoTo ErrPart
            If SalProcess("F") = False Then GoTo ErrPart
        End If

        PubDBCn.CommitTrans()
        MsgBox("Salary Process Complete")

        PBar.Visible = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        PBar.Visible = False
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation("Salary Not Process.")
    End Sub
    Private Function FieldVarification(ByRef pType As String) As Boolean

        On Error GoTo ErrPart
        Dim mDate As String
        Dim mString As String
        Dim mYM As Integer
        Dim mVNo As String
        Dim mVDate As String
        Dim mVType As String
        Dim mVSeqNo As Integer
        Dim mVNoSuffix As String
        Dim mBankCode As Integer
        Dim mBType As String
        Dim mBSType As String
        Dim mDivisionCode As Double
        Dim mCurrentFYNo As Integer
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim RsSalTRN As ADODB.Recordset

        FieldVarification = True

        mDate = MainClass.LastDay(Month(CDate(lblNewDate.Text)), Year(CDate(lblNewDate.Text))) & "/" & Month(CDate(lblNewDate.Text)) & "/" & Year(CDate(lblNewDate.Text))

        mCurrentFYNo = GetCurrentFYNo(PubDBCn, mDate)


        If OptParti.Checked = True Then
            If TxtCardNo.Text = "" Then
                MsgBox("CardNo. Not Selected.")
                FieldVarification = False
                TxtCardNo.Focus()
                Exit Function
            End If
        End If

        If lblProcessType.Text = "SAL" Then
            If pType = "S" Then
                If CheckAttn(mDate, mString) = False Then
                    If mString <> "" Then
                        MsgBox("Please Mark the Leave or Present of the Following Employee : " & mString)
                    End If
                    FieldVarification = False
                    Exit Function
                End If
            End If
            mYM = CInt(VB6.Format(Year(CDate(mDate)), "0000") & VB6.Format(Month(CDate(mDate)), "00"))
            If CheckSalVoucherPost(mYM, mCurrentFYNo, mVNo, mVDate, mVType, mVSeqNo, mVNoSuffix, mBankCode, "S", mBSType, mDivisionCode) = True Then
                MsgInformation("Salary Already Post in Accounts, so you cann't be reprocess Salary. VNo is (" & mVNo & ").")
                FieldVarification = False
                Exit Function
            End If
        Else
            mYM = CInt(VB6.Format(Year(CDate(mDate)), "0000") & VB6.Format(Month(CDate(mDate)), "00"))
            If CheckSalVoucherPost(mYM, mCurrentFYNo, mVNo, mVDate, mVType, mVSeqNo, mVNoSuffix, mBankCode, "O", mBSType, mDivisionCode) = True Then
                MsgInformation("Salary Already Post in Accounts, so you cann't be reprocess OT. VNo is (" & mVNo & ").")
                FieldVarification = False
                Exit Function
            End If
        End If


        If lblEmpType.Text = "D" Then
            If CheckSalaryMade("", VB6.Format(mDate, "DD/MM/YYYY")) = True Then
                MsgInformation("Salary Made Againt This Month. So Cann't be Reprocess.")
                Exit Function
            End If
        End If

        SqlStr = "Select COUNT(1) AS CNTREC FROM PAY_SAL_TRN" & vbCrLf _
                    & " TRN, PAY_EMPLOYEE_MST EMP " & vbCrLf _
                    & " WHERE TRN.COMPANY_CODE=EMP.COMPANY_CODE" & vbCrLf _
                    & " AND TRN.EMP_CODE=EMP.EMP_CODE" & vbCrLf _
                    & " AND EMP.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND TRN.ISARREAR IN ('Y','N') AND IS_PAID='Y'" & vbCrLf _
                    & " AND TRN.SAL_DATE=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " AND CATEGORY<>'C'"

        If lblEmpType.Text = "D" Then
        Else
            If Trim(lblEmpType.Text) = "S" Then
                SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_CAT_TYPE='1'"
            Else
                SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_CAT_TYPE='2'"
            End If
        End If


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSalTRN, ADODB.LockTypeEnum.adLockOptimistic)
        If RsSalTRN.EOF = False Then
            If RsSalTRN.Fields("CNTREC").Value > 0 Then
                FieldVarification = False
                MsgInformation("Salary Already paid, so you cann't be reprocess Salary.")
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                Exit Function
            End If
        End If


        SqlStr = "Select COUNT(1) AS CNTREC From PAY_MONTHLY_OT_TRN " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND OT_DATE>TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf _
            & " AND EMP_CODE IN (SELECT EMP_CODE " & vbCrLf _
            & " FROM PAY_EMPLOYEE_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CATG<>'C')"


        If OptParti.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & TxtCardNo.Text & "'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
        If RsTemp.EOF = False Then
            If RsTemp.Fields("CNTREC").Value > 0 And PubSuperUser <> "S" Then
                FieldVarification = False
                MsgInformation("You Cann't Process back Salary.")
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                Exit Function
            End If
        End If

        Exit Function
ErrPart:
        FieldVarification = False
    End Function
    Private Function CheckAttn(ByRef mDate As String, ByRef mString As String) As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsEmployee As ADODB.Recordset
        Dim mDOJ As String
        Dim mDOL As String
        Dim mEmpCode As String
        Dim mEmpDOL As String
        Dim mEmpDOJ As String
        Dim cntCol As Integer
        Dim mCheckDate As String
        Dim mFMark As Integer
        Dim mSMark As Integer
        Dim mPFCheckDate As String
        Dim mESICheckDate As String
        Dim mPFNo As String
        Dim mESINo As String
        Dim mPFAmount As Double
        Dim mESIAmount As Double

        Dim pLayOffDateStart As String
        Dim pLayOffDateEnd As String
        Dim mMonthLastDate As String

        'Public Const ABSENT = 0
        'Public Const CASUAL = 1
        'Public Const EARN = 2
        'Public Const SICK = 3
        'Public Const MATERNITY = 4
        'Public Const CPLEARN = 5
        'Public Const WOPAY = 6
        'Public Const CPLAVAIL = 7
        'Public Const SUNDAY = 8
        'Public Const HOLIDAY = 9
        'Public Const PRESENT = 10

        CheckAttn = False
        mString = ""
        SqlStr = ""

        If CDate(mDate) < CDate("01/04/2014") Then
            CheckAttn = True
            Exit Function
        End If

        CheckAttn = True
        mDOJ = MainClass.LastDay(Month(CDate(lblNewDate.Text)), Year(CDate(lblNewDate.Text))) & "/" & VB6.Format(lblNewDate.Text, "MM/YYYY")
        mDOL = "01" & "/" & VB6.Format(lblNewDate.Text, "MM/YYYY")
        mPFCheckDate = "01" & "/" & VB6.Format(lblNewDate.Text, "MM/YYYY")
        mESICheckDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -7, CDate(mDOJ)))




        SqlStr = " SELECT * FROM " & vbCrLf & " PAY_EMPLOYEE_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_STOP_SALARY='N' AND " & vbCrLf _
            & " EMP_DOJ <=TO_DATE('" & VB6.Format(mDOJ, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND (EMP_LEAVE_DATE >TO_DATE('" & VB6.Format(mDOL, "dd-mmm-yyyy") & "','DD-MON-YYYY') OR EMP_LEAVE_DATE IS NULL) "

        SqlStr = SqlStr & vbCrLf & " AND EMP_CATG<>'C'"
        SqlStr = SqlStr & vbCrLf & " AND EMP_STOP_SALARY='N'"

        If lblEmpType.Text = "D" Then

        Else
            If Trim(lblEmpType.Text) = "S" Then
                SqlStr = SqlStr & vbCrLf & " AND EMP_CAT_TYPE='1'"
            Else
                SqlStr = SqlStr & vbCrLf & " AND EMP_CAT_TYPE='2'"
            End If
        End If

        If OptParti.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & TxtCardNo.Text & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND EMP_CODE NOT IN (SELECT EMP_CODE FROM " & vbCrLf _
            & " PAY_FFSETTLE_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND TO_CHAR(EMP_LEAVE_DATE,'MM-YYYY')='" & VB6.Format(mDOL, "MM-YYYY") & "' AND PAID_DAYS>0)"

        SqlStr = SqlStr & vbCrLf & " Order By EMP_CODE"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmployee, ADODB.LockTypeEnum.adLockOptimistic)

        If RsEmployee.EOF = False Then
            Do While RsEmployee.EOF = False
                mEmpCode = IIf(IsDbNull(RsEmployee.Fields("EMP_CODE").Value), "", RsEmployee.Fields("EMP_CODE").Value)
                mEmpDOJ = IIf(IsDbNull(RsEmployee.Fields("EMP_DOJ").Value), "", RsEmployee.Fields("EMP_DOJ").Value)
                If mEmpDOJ = "" Then
                    mEmpDOJ = "01" & "/" & VB6.Format(lblNewDate.Text, "MM/YYYY")
                End If

                If CDate(mEmpDOJ) < CDate("01" & "/" & VB6.Format(lblNewDate.Text, "MM/YYYY")) Then
                    mEmpDOJ = "01" & "/" & VB6.Format(lblNewDate.Text, "MM/YYYY")
                End If

                mEmpDOL = IIf(IsDBNull(RsEmployee.Fields("EMP_LEAVE_DATE").Value), "", RsEmployee.Fields("EMP_LEAVE_DATE").Value)
                mMonthLastDate = MainClass.LastDay(Month(CDate(lblNewDate.Text)), Year(CDate(lblNewDate.Text))) & "/" & VB6.Format(lblNewDate.Text, "MM/YYYY")

                If mEmpDOL = "" Then
                    mEmpDOL = mMonthLastDate
                Else
                    If CDate(mEmpDOL) > CDate(mMonthLastDate) Then
                        mEmpDOL = mMonthLastDate
                    End If
                End If

                If GetLayoffMonth(mEmpDOJ, pLayOffDateStart, pLayOffDateEnd) = True Then
                    mEmpDOJ = IIf(CDate(mEmpDOJ) >= CDate(pLayOffDateStart) And CDate(mEmpDOJ) <= CDate(pLayOffDateEnd), DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(pLayOffDateEnd)), mEmpDOJ)
                    mEmpDOL = IIf(CDate(mEmpDOL) < CDate(pLayOffDateEnd), mEmpDOL, DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(pLayOffDateEnd)))
                    '                mEmpDOL = DateAdd("d", -1, pLayOffDateStart)
                    If CDate(mEmpDOJ) > CDate(mDOJ) And CDate(mEmpDOL) > CDate(mDOJ) Then
                        CheckAttn = True
                        Exit Function
                    End If
                End If

                For cntCol = VB.Day(CDate(mEmpDOJ)) To VB.Day(CDate(mEmpDOL))
                    mCheckDate = cntCol & "/" & VB6.Format(lblNewDate.Text, "MM/YYYY")
                    mCheckDate = VB6.Format(mCheckDate, "DD/MM/YYYY")

                    SqlStr = " SELECT DISTINCT TRN.EMP_CODE,TRN.FIRSTHALF,TRN.SECONDHALF" & vbCrLf _
                        & " FROM PAY_ATTN_MST TRN" & vbCrLf _
                        & " WHERE TRN.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND TRN.EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf _
                        & " AND TRN.ATTN_DATE=TO_DATE('" & VB6.Format(mCheckDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                    If RsTemp.EOF = False Then
                        mFMark = IIf(IsDbNull(RsTemp.Fields("FIRSTHALF").Value), -1, RsTemp.Fields("FIRSTHALF").Value)
                        mSMark = IIf(IsDbNull(RsTemp.Fields("SECONDHALF").Value), -1, RsTemp.Fields("SECONDHALF").Value)

                        If mFMark < 0 Or mSMark < 0 Then ''If mFMark = 0 Or mSMark = 0 Then change in 26/09/2018
                            mString = IIf(mString = "", "", mString & ", ") & IIf(IsDbNull(RsTemp.Fields("EMP_CODE").Value), "", RsTemp.Fields("EMP_CODE").Value) & " Dt : " & mCheckDate
                            CheckAttn = False
                            Exit For
                        End If
                    Else
                        mString = IIf(mString = "", "", mString & ", ") & mEmpCode
                        CheckAttn = False
                        Exit For
                    End If
                Next
                RsEmployee.MoveNext()
            Loop
        End If

        ''                        & " AND (TRN.FIRSTHALF<0 OR TRN.SECONDHALF<0)" & vbCrLf _
        '
        Exit Function
ERR1:
        CheckAttn = False
        MsgInformation(Err.Description)
    End Function

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click
        On Error GoTo SearchErr
        Dim SqlStr As String = ""

        SqlStr = "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If lblEmpType.Text = "D" Then

        ElseIf Trim(lblEmpType.Text) = "S" Then
            SqlStr = SqlStr & vbCrLf & " AND EMP_CAT_TYPE='1'"
        ElseIf Trim(lblEmpType.Text) = "W" Then
            SqlStr = SqlStr & vbCrLf & " AND EMP_CAT_TYPE='2'"
        End If

        If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            TxtCardNo.Text = AcName1
            TxtName.Text = AcName
            TxtCardNo_Validating(TxtCardNo, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
SearchErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdUnProcess_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdUnProcess.Click

        Dim mDate As String
        Dim mAuthorisation As String

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)

        mAuthorisation = IIf(InStr(1, XRIGHT, "S") > 0, "Y", "N")

        lblNewDate.Text = VB6.Format("01/" & VB6.Format(txtMonth.Text, "MM/YYYY"))

        'If mAuthorisation = "N" Then
        '    MsgBox("You have not Rights to un Process it.", MsgBoxStyle.Critical)
        '    Exit Sub
        'End If


        '    If lblEmpType.Caption = "S" Then
        '        If XRIGHT <> "AMDVSP" Then
        '            MsgInformation "You have not Rights to Process it."
        '            Exit Sub
        '        End If
        '    Else
        '        If XRIGHT = "AMDVS" Or XRIGHT = "AMDV" Then
        '
        '        Else
        '            MsgInformation "You have not Rights to Process it."
        '            Exit Sub
        '        End If
        '    End If

        If lblProcessType.Text = "SAL" Then
            If ValidateBookLocking(PubDBCn, CInt(ConLockEmpSalaryProcess), VB6.Format(lblNewDate.Text, "DD/MM/YYYY")) = True Then
                Exit Sub
            End If
        Else
            If ValidateBookLocking(PubDBCn, CInt(ConLockEmpOTProcess), VB6.Format(lblNewDate.Text, "DD/MM/YYYY")) = True Then
                Exit Sub
            End If
        End If

        If FieldVarification("U") = False Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        PBar.Visible = False
        mDate = MainClass.LastDay(Month(CDate(lblNewDate.Text)), Year(CDate(lblNewDate.Text))) & "/" & Month(CDate(lblNewDate.Text)) & "/" & Year(CDate(lblNewDate.Text))
        mCurrentFYNo = GetCurrentFYNo(PubDBCn, mDate)

        If mCurrentFYNo = -1 Then
            Exit Sub
        End If

        'If lblProcessType.Text = "SAL" Then
        Call SalUnProcess()
        'Else
        Call OTUnProcess()
        'End If

        cmdUnProcess.Enabled = True
        PBar.Visible = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub FrmSalaryProcess_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        'If lblProcessType.Text = "SAL" Then
        '    Me.Text = "Salary Process"
        'Else
        '    Me.Text = "Over Time Process"
        'End If

        'Me.Text = Me.Text & IIf(lblEmpType.Text = "S", " (Staff)", IIf(lblEmpType.Text = "W", " (Workers)", ""))

    End Sub

    Private Sub FrmSalaryProcess_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        Me.Left = VB6.TwipsToPixelsX(25)
        Me.Top = VB6.TwipsToPixelsY(25)
        MainClass.SetControlsColor(Me)
        Me.Height = VB6.TwipsToPixelsY(3855)
        Me.Width = VB6.TwipsToPixelsX(5475)

        'txtMonth1.Enabled = False
        '    TxtYear.Enabled = False

        lblNewDate.Text = CStr(RunDate)

        If PubATHUSER = True Then
            cmdUnProcess.Enabled = True
        Else
            cmdUnProcess.Enabled = False
        End If


        'txtMonth1.Text = MonthName(Month(RunDate)) & ", " & Year(RunDate)
        '    TxtYear.Text = Year(RunDate)

        OptAll.Checked = True
        HideUnHide(False)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub
    Private Sub FrmSalaryProcess_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
    End Sub

    Private Sub optAll_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptAll.CheckedChanged
        If eventSender.Checked Then
            HideUnHide(False)
        End If
    End Sub

    Private Sub OptParti_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptParti.CheckedChanged
        If eventSender.Checked Then
            HideUnHide(True)
        End If
    End Sub

    Private Sub TxtCardNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtCardNo.DoubleClick
        cmdsearch_Click(cmdSearch, New System.EventArgs())
    End Sub
    Private Sub TxtCardNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtCardNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtCardNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub TxtCardNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtCardNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim SqlStr As String = ""

        If TxtCardNo.Text = "" Then GoTo EventExitSub
        TxtCardNo.Text = VB6.Format(TxtCardNo.Text, "000000")

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If Trim(lblEmpType.Text) = "S" Then
            SqlStr = SqlStr & vbCrLf & " AND EMP_CAT_TYPE='1'"
        ElseIf Trim(lblEmpType.Text) = "W" Then
            SqlStr = SqlStr & vbCrLf & " AND EMP_CAT_TYPE='2'"
        End If

        If MainClass.ValidateWithMasterTable((TxtCardNo.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("CardNo. Does Not Exist In Master.", MsgBoxStyle.Information)
            Cancel = True
        Else
            TxtName.Text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub



    'Private Sub UpDMonth_DownClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles UpDMonth.DownClick
    '    SetNewDate(-1)
    '    txtMonth.Text = MonthName(Month(CDate(lblNewDate.Text))) & ", " & Year(CDate(lblNewDate.Text))
    '    cmdUnProcess.Enabled = True
    'End Sub
    Sub SetNewDate(ByRef prmSpinDirection As Short)
        lblNewDate.Text = VB6.Format("01/" & VB6.Format(lblMonth.Text, "MM/YYYY"))    '' CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, prmSpinDirection, CDate(lblNewDate.Text)))
    End Sub
    'Private Sub UpDMonth_UpClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles UpDMonth.UpClick
    '    SetNewDate(1)
    '    txtMonth.Text = MonthName(Month(CDate(lblNewDate.Text))) & ", " & Year(CDate(lblNewDate.Text))
    '    cmdUnProcess.Enabled = True
    'End Sub


    Private Sub HideUnHide(ByRef mCheck As Boolean)
        TxtCardNo.Enabled = mCheck
        cmdSearch.Enabled = mCheck
    End Sub

    Private Function SalProcess(pSalaryType As String) As Boolean

        On Error GoTo ErrPart

        Dim RsSalTRN As ADODB.Recordset = Nothing
        Dim RsEmployee As ADODB.Recordset
        Dim RsDivision As ADODB.Recordset
        Dim SqlStr As String = ""
        Static mDOJ As String
        Static mDOL As String
        Dim mMonth As String
        Dim mSalDate As String
        Dim mCalcArrear As String
        Dim mAddDays As Double
        Dim mDeptDesc As String
        Dim pDesignation As String
        Dim mYYMM As Integer
        Dim mDOB As String
        Dim mAge As Double
        Dim mDivisionCode As Double

        Dim RsEmpCat As ADODB.Recordset
        Dim mCategory As String
        Dim mYM As Integer
        Dim mVNo As String
        Dim mVDate As String
        Dim mVType As String
        Dim mVSeqNo As Integer
        Dim mVNoSuffix As String
        Dim mBankCode As Integer
        Dim mBType As String
        Dim mBSType As String
        Dim mSalaryTable As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'PBar.Min = 0

        SqlStr = "DELETE FROM TEMP_PAY_SAL_TRN WHERE USERID='" & PubUserID & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = "DELETE FROM TEMP_PAY_PFESI_TRN WHERE USERID='" & PubUserID & "'"
        PubDBCn.Execute(SqlStr)

        mSalaryTable = IIf(pSalaryType = "F", "PAY_SAL_TRN", "PAY_ACTUAL_SAL_TRN")

        mMonth = UCase(VB6.Format(lblNewDate.Text, "MMM-YYYY"))

        SqlStr = ""
        mSalDate = MainClass.LastDay(Month(CDate(lblNewDate.Text)), Year(CDate(lblNewDate.Text))) & "/" & VB6.Format(lblNewDate.Text, "MM/YYYY")
        mDOJ = MainClass.LastDay(Month(CDate(lblNewDate.Text)), Year(CDate(lblNewDate.Text))) & "/" & VB6.Format(lblNewDate.Text, "MM/YYYY")
        mDOL = "01" & "/" & VB6.Format(lblNewDate.Text, "MM/YYYY")
        SalProcess = False

        ''---------------------------------------
        SqlStr = " SELECT * FROM " & vbCrLf _
            & " PAY_EMPLOYEE_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_STOP_SALARY='N' AND " & vbCrLf _
            & " EMP_DOJ <=TO_DATE('" & VB6.Format(mDOJ, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND (EMP_LEAVE_DATE >TO_DATE('" & VB6.Format(mDOL, "dd-mmm-yyyy") & "','DD-MON-YYYY') OR EMP_LEAVE_DATE IS NULL) "

        SqlStr = SqlStr & vbCrLf & " AND EMP_CATG<>'C'"

        If lblEmpType.Text = "D" Then
        Else
            If Trim(lblEmpType.Text) = "S" Then
                SqlStr = SqlStr & vbCrLf & " AND EMP_CAT_TYPE='1'"
            Else
                SqlStr = SqlStr & vbCrLf & " AND EMP_CAT_TYPE='2'"
            End If
        End If

        If OptParti.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & TxtCardNo.Text & "'"
        End If

        SqlStr = SqlStr & vbCrLf _
            & " AND EMP_CODE NOT IN (SELECT EMP_CODE FROM " & vbCrLf _
            & " PAY_FFSETTLE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND TO_CHAR(EMP_LEAVE_DATE,'MM-YYYY')='" & VB6.Format(mDOL, "MM-YYYY") & "' AND PAID_DAYS>0)"

        SqlStr = SqlStr & vbCrLf & " Order By EMP_NAME"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmployee, ADODB.LockTypeEnum.adLockOptimistic)

        SqlStr = "Select TRN.* From PAY_SAL_TRN TRN, PAY_EMPLOYEE_MST EMP " & vbCrLf _
            & " WHERE TRN.COMPANY_CODE=EMP.COMPANY_CODE" & vbCrLf _
            & " AND TRN.EMP_CODE=EMP.EMP_CODE" & vbCrLf _
            & " AND EMP.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND TO_CHAR(TRN.SAL_DATE,'MON-YYYY')='" & mMonth & "' AND TRN.ISARREAR IN ('Y','N')"

        If lblEmpType.Text = "D" Then
        Else
            If Trim(lblEmpType.Text) = "S" Then
                SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_CAT_TYPE='1'"
            Else
                SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_CAT_TYPE='2'"
            End If
        End If

        SqlStr = SqlStr & vbCrLf & " AND TRN.CATEGORY<>'C'"

        If OptParti.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_CODE='" & TxtCardNo.Text & "'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSalTRN, ADODB.LockTypeEnum.adLockOptimistic)

        If RsSalTRN.EOF = False Then
            SqlStr = CStr(MsgBox("Salary Already Processed For This Period ... " & vbNewLine & vbNewLine & "Want To Reprocess ...", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2))
            If SqlStr = CStr(MsgBoxResult.Yes) Then
                SqlStr = "DELETE FROM " & mSalaryTable & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND TO_CHAR(SAL_DATE,'MON-YYYY')='" & mMonth & "' AND ISARREAR IN ('Y','N')"

                SqlStr = SqlStr & vbCrLf & " AND CATEGORY<>'C'"

                If lblEmpType.Text = "D" Then
                Else
                    If Trim(lblEmpType.Text) = "S" Then
                        SqlStr = SqlStr & vbCrLf _
                            & " AND EMP_CODE IN (SELECT EMP_CODE FROM PAY_EMPLOYEE_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CAT_TYPE='1')"
                    Else
                        SqlStr = SqlStr & vbCrLf _
                            & " AND EMP_CODE IN (SELECT EMP_CODE FROM PAY_EMPLOYEE_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CAT_TYPE='2')"
                    End If
                End If

                If OptParti.Checked = True Then
                    SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & TxtCardNo.Text & "'"
                End If

                PubDBCn.Execute(SqlStr)

                SqlStr = "DELETE FROM PAY_ACTUAL_SAL_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND TO_CHAR(SAL_DATE,'MON-YYYY')='" & mMonth & "' AND ISARREAR IN ('Y','N')"

                SqlStr = SqlStr & vbCrLf & " AND CATEGORY<>'C'"

                If lblEmpType.Text = "D" Then
                Else
                    If Trim(lblEmpType.Text) = "S" Then
                        SqlStr = SqlStr & vbCrLf _
                            & " AND EMP_CODE IN (SELECT EMP_CODE FROM PAY_EMPLOYEE_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CAT_TYPE='1')"
                    Else
                        SqlStr = SqlStr & vbCrLf _
                            & " AND EMP_CODE IN (SELECT EMP_CODE FROM PAY_EMPLOYEE_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CAT_TYPE='2')"
                    End If
                End If

                If OptParti.Checked = True Then
                    SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & TxtCardNo.Text & "'"
                End If


                PubDBCn.Execute(SqlStr)
                If pSalaryType = "A" Then
                    SqlStr = "DELETE FROM PAY_PERKS_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND TO_CHAR(SAL_DATE,'MON-YYYY')='" & mMonth & "' AND BOOKTYPE IN ('S','A')"

                    If lblEmpType.Text = "D" Then
                    Else
                        If Trim(lblEmpType.Text) = "S" Then
                            SqlStr = SqlStr & vbCrLf & " AND EMP_CODE IN (SELECT EMP_CODE FROM PAY_EMPLOYEE_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CAT_TYPE='1')"
                        Else
                            SqlStr = SqlStr & vbCrLf & " AND EMP_CODE IN (SELECT EMP_CODE FROM PAY_EMPLOYEE_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CAT_TYPE='2')"
                        End If
                    End If
                    If OptParti.Checked = True Then
                        SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & TxtCardNo.Text & "'"
                    End If

                    PubDBCn.Execute(SqlStr)

                    SqlStr = " DELETE FROM PAY_PFESI_TRN " & vbCrLf _
                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND TO_CHAR(SAL_DATE,'MON-YYYY')='" & mMonth & "'" & vbCrLf _
                        & " AND ISARREAR IN('Y','N')"

                    '            SqlStr = SqlStr & vbCrLf _
                    ''                    & " AND EMP_CODE IN (SELECT EMP_CODE " & vbCrLf _
                    ''                    & " FROM PAY_EMPLOYEE_MST " & vbCrLf _
                    ''                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    ''                    & " AND EMP_CATG<>'C')"

                    If lblEmpType.Text = "D" Then
                    Else
                        If Trim(lblEmpType.Text) = "S" Then
                            SqlStr = SqlStr & vbCrLf & " AND EMP_CODE IN (SELECT EMP_CODE FROM PAY_EMPLOYEE_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CAT_TYPE='1')"
                        Else
                            SqlStr = SqlStr & vbCrLf & " AND EMP_CODE IN (SELECT EMP_CODE FROM PAY_EMPLOYEE_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CAT_TYPE='2')"
                        End If
                    End If

                    If OptParti.Checked = True Then
                        SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & TxtCardNo.Text & "'"
                    End If

                    PubDBCn.Execute(SqlStr)
                End If

            Else
                SalProcess = False
                PBar.Visible = False
                Exit Function
            End If
        End If

        If RsEmployee.EOF = False Then
            PBar.Visible = True

            PBar.Minimum = 0
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
            If OptParti.Checked = True Then
                SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & TxtCardNo.Text & "'"
            End If

            PBar.Maximum = MainClass.GetMaxRecord("PAY_EMPLOYEE_MST", PubDBCn, SqlStr)
            PBar.Value = PBar.Minimum
            Do While Not RsEmployee.EOF

                mAddDays = CalcAddDays(RsEmployee.Fields("EMP_CODE").Value)

                If MainClass.ValidateWithMasterTable(RsEmployee.Fields("EMP_DEPT_CODE").Value, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDeptDesc = MasterNo
                Else
                    mDeptDesc = "-1"
                End If

                mDOB = IIf(IsDBNull(RsEmployee.Fields("EMP_DOB").Value), "", RsEmployee.Fields("EMP_DOB").Value)

                If mDOB = "" Then
                    mAge = 18
                Else
                    mAge = DateDiff(Microsoft.VisualBasic.DateInterval.Month, CDate(mDOB), CDate(mSalDate)) / 12
                End If

                If UpdateSalTrn(pSalaryType, RsEmployee.Fields("EMP_CODE").Value, mDeptDesc, RsEmployee.Fields("EMP_CATG").Value, RsEmployee.Fields("PAYMENTMODE").Value, IIf(IsDBNull(RsEmployee.Fields("EMP_BANK_NO").Value), "", RsEmployee.Fields("EMP_BANK_NO").Value), IIf(IsDBNull(RsEmployee.Fields("EMPBANK_IFSC").Value), "", RsEmployee.Fields("EMPBANK_IFSC").Value), "N", mAddDays, RsEmployee.Fields("EMP_DOJ").Value, IIf(IsDBNull(RsEmployee.Fields("EMP_LEAVE_DATE").Value), "", RsEmployee.Fields("EMP_LEAVE_DATE").Value), mSalDate, pDesignation, mAge, RsEmployee.Fields("DIV_CODE").Value) = False Then GoTo ErrPart

                If pSalaryType = "F" Then
                    If UpdateArrearSalTrn(RsEmployee.Fields("EMP_CODE").Value, mDeptDesc, RsEmployee.Fields("EMP_CATG").Value, RsEmployee.Fields("PAYMENTMODE").Value, IIf(IsDBNull(RsEmployee.Fields("EMP_BANK_NO").Value), "", RsEmployee.Fields("EMP_BANK_NO").Value), IIf(IsDBNull(RsEmployee.Fields("EMPBANK_IFSC").Value), "", RsEmployee.Fields("EMPBANK_IFSC").Value), "Y", mAddDays, RsEmployee.Fields("EMP_DOJ").Value, IIf(IsDBNull(RsEmployee.Fields("EMP_LEAVE_DATE").Value), "", RsEmployee.Fields("EMP_LEAVE_DATE").Value), mSalDate, mAge, RsEmployee.Fields("DIV_CODE").Value) = False Then GoTo ErrPart
                End If


                RsEmployee.MoveNext()
                PBar.Value = PBar.Value + 1
            Loop

            mYYMM = CInt(VB6.Format(lblNewDate.Text, "YYYYMM"))

            If OptAll.Checked = True Then
                SqlStr = "SELECT DIV_CODE FROM INV_DIVISION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DIV_CODE"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDivision, ADODB.LockTypeEnum.adLockReadOnly)

                If RsDivision.EOF = False Then
                    Do While RsDivision.EOF = False
                        mDivisionCode = IIf(IsDBNull(RsDivision.Fields("DIV_CODE").Value), -1, RsDivision.Fields("DIV_CODE").Value)

                        SqlStr = "SELECT  DISTINCT CATEGORY_CODE FROM PAY_CATEGORY_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ORDER BY CATEGORY_CODE"
                        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmpCat, ADODB.LockTypeEnum.adLockReadOnly)

                        If RsEmpCat.EOF = False Then
                            Do While RsEmpCat.EOF = False
                                mCategory = IIf(IsDBNull(RsEmpCat.Fields("CATEGORY_CODE").Value), "", RsEmpCat.Fields("CATEGORY_CODE").Value)
                                If UpdateAccountPostingHead(mYYMM, "N", mCategory, "S", "S", mCategory, mDivisionCode) = False Then GoTo ErrPart
                                If UpdateAccountPostingHead(mYYMM, "Y", mCategory, "A", "A", mCategory, mDivisionCode) = False Then GoTo ErrPart

                                RsEmpCat.MoveNext()
                            Loop
                        End If
                        RsDivision.MoveNext()
                    Loop
                End If
            End If
            SalProcess = True
            'MsgBox("Salary Process Complete")
        Else
            SalProcess = False
            'MsgBox("No Record Found For Processing.")
        End If


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Function

ErrPart:
        ''MsgInformation err.Description
        ''Resume
        SalProcess = False
        'PubDBCn.RollbackTrans()
        'MsgInformation(Err.Description & ". Salary Process Not Complete, Try Again.")

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Private Function CheckSalVoucherPost(ByRef mYM As Integer, ByRef mCurrentFYNo As Integer, ByRef mVNo As String, ByRef mVDate As String, ByRef mVType As String, ByRef mVSeqNo As Integer, ByRef mVNoSuffix As String, ByRef mBankCode As Integer, ByRef mBookType As String, ByRef mBookSubType As String, ByRef mDivisionCode As Double, Optional ByRef mELYear As Integer = 0) As Boolean


        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim mKey As String

        CheckSalVoucherPost = False
        SqlStr = " SELECT * FROM FIN_SalVoucher_TRN  " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & mCurrentFYNo & ""

        SqlStr = SqlStr & vbCrLf & " AND BookType='" & mBookType & "'"

        SqlStr = SqlStr & vbCrLf & " AND BookSubType IN (SELECT DISTINCT CATEGORY_CODE  FROM PAY_CATEGORY_MST  WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_TYPE='" & lblEmpType.Text & "')" '" & mBookSubType & "'"

        '    SqlStr = SqlStr & vbCrLf & " AND DIV_CODE=" & mDivisionCode & ""

        '    If mBookType = "F" Or mBookType = "L" Then
        '        SqlStr = SqlStr & vbCrLf & " AND BANKCODE=" & Val(mBankCode) & " "
        '        If mELYear <> 0 And mBookType = "L" Then
        '            SqlStr = SqlStr & vbCrLf & " AND EL_YEAR=" & mELYear & ""
        '        End If
        '    ElseIf mBookType = "Q" Then
        '        SqlStr = SqlStr & vbCrLf & " AND BANKCODE=" & Val(mBankCode) & " AND YM=" & mYM & ""
        '    Else
        SqlStr = SqlStr & vbCrLf & " AND YM=" & mYM & ""
        '    End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc)
        If RsMisc.EOF = False Then
            mKey = IIf(IsDBNull(RsMisc.Fields("mKey").Value), "", RsMisc.Fields("mKey").Value)
            mBankCode = RsMisc.Fields("BANKCODE").Value

            If mKey <> "" Then
                'FYEAR=" & RsCompany.Fields("FYEAR").Value & "
                SqlStr = " SELECT * FROM FIN_VOUCHER_HDR  " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MKEY='" & mKey & "'" & vbCrLf & " AND CANCELLED='N'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc)
                If RsMisc.EOF = False Then
                    mVType = IIf(IsDBNull(RsMisc.Fields("VTYPE").Value), "", RsMisc.Fields("VTYPE").Value)
                    mVSeqNo = RsMisc.Fields("VNOSEQ").Value
                    mVNoSuffix = IIf(IsDBNull(RsMisc.Fields("VNOSUFFIX").Value), "", RsMisc.Fields("VNOSUFFIX").Value)
                    mVNo = RsMisc.Fields("VNO").Value
                    mVDate = RsMisc.Fields("VDATE").Value
                    CheckSalVoucherPost = True
                End If
            End If
        Else
            CheckSalVoucherPost = False
        End If

        Exit Function
ErrPart:
        MsgInformation(Err.Description)
        CheckSalVoucherPost = False
    End Function

    Private Function SalDummyProcess(ByRef pSalaryType As String) As Boolean

        On Error GoTo ErrPart

        Dim RsSalTRN As ADODB.Recordset = Nothing
        Dim RsEmployee As ADODB.Recordset
        Dim RsDivision As ADODB.Recordset
        Dim SqlStr As String = ""
        Static mDOJ As String
        Static mDOL As String
        Dim mMonth As String
        Dim mSalDate As String
        Dim mCalcArrear As String
        Dim mAddDays As Double
        Dim mDeptDesc As String
        Dim pDesignation As String
        Dim mYYMM As Integer
        Dim mDOB As String
        Dim mAge As Double
        Dim mDivisionCode As Double

        Dim RsEmpCat As ADODB.Recordset
        Dim mCategory As String


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'PBar.Min = 0

        SalDummyProcess = False
        mMonth = UCase(VB6.Format(lblNewDate.Text, "MMM-YYYY"))

        SqlStr = ""
        mSalDate = MainClass.LastDay(Month(CDate(lblNewDate.Text)), Year(CDate(lblNewDate.Text))) & "/" & VB6.Format(lblNewDate.Text, "MM/YYYY")
        mDOJ = MainClass.LastDay(Month(CDate(lblNewDate.Text)), Year(CDate(lblNewDate.Text))) & "/" & VB6.Format(lblNewDate.Text, "MM/YYYY")
        mDOL = "01" & "/" & VB6.Format(lblNewDate.Text, "MM/YYYY")

        ''Check Validation

        ''---------------------------------------
        SqlStr = " SELECT * FROM " & vbCrLf _
            & " PAY_EMPLOYEE_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_STOP_SALARY='N' AND " & vbCrLf _
            & " EMP_DOJ <=TO_DATE('" & VB6.Format(mDOJ, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND (EMP_LEAVE_DATE >TO_DATE('" & VB6.Format(mDOL, "dd-mmm-yyyy") & "','DD-MON-YYYY') OR EMP_LEAVE_DATE IS NULL) "

        SqlStr = SqlStr & vbCrLf & " AND EMP_CATG<>'C'"

        If OptParti.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & TxtCardNo.Text & "'"
        End If

        SqlStr = SqlStr & vbCrLf _
            & " AND EMP_CODE NOT IN (SELECT EMP_CODE FROM " & vbCrLf _
            & " PAY_FFSETTLE_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND TO_CHAR(EMP_LEAVE_DATE,'MM-YYYY')='" & VB6.Format(mDOL, "MM-YYYY") & "' AND PAID_DAYS>0) "

        SqlStr = SqlStr & vbCrLf & " Order By EMP_NAME"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmployee, ADODB.LockTypeEnum.adLockOptimistic)


        If pSalaryType = "A" Then
            SqlStr = "DELETE FROM PAY_DUMMYSAL_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" ''& vbCrLf |            & " AND TO_CHAR(SAL_DATE,'MON-YYYY')='" & mMonth & "' AND ISARREAR IN ('Y','N')"

            SqlStr = SqlStr & vbCrLf & " AND CATEGORY<>'C'"

            If OptParti.Checked = True Then
                SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & TxtCardNo.Text & "'"
            End If

            PubDBCn.Execute(SqlStr)

            SqlStr = "DELETE FROM PAY_DUMMYACTUAL_SAL_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" ''& vbCrLf |            & " AND TO_CHAR(SAL_DATE,'MON-YYYY')='" & mMonth & "' AND ISARREAR IN ('Y','N')"

            SqlStr = SqlStr & vbCrLf & " AND CATEGORY<>'C'"

            If OptParti.Checked = True Then
                SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & TxtCardNo.Text & "'"
            End If

            PubDBCn.Execute(SqlStr)

            SqlStr = "DELETE FROM PAY_MONTHLY_DUMMY_OT_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" ''& vbCrLf |                    & " AND TO_CHAR(SAL_DATE,'MON-YYYY')='" & mMonth & "' AND ISARREAR IN ('Y','N')"

            'SqlStr = SqlStr & vbCrLf & " AND CATEGORY<>'C'"

            If OptParti.Checked = True Then
                SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & TxtCardNo.Text & "'"
            End If

            PubDBCn.Execute(SqlStr)
        End If

        If RsEmployee.EOF = False Then
            PBar.Visible = True

            PBar.Minimum = 0
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
            If OptParti.Checked = True Then
                SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & TxtCardNo.Text & "'"
            End If

            PBar.Maximum = MainClass.GetMaxRecord("PAY_EMPLOYEE_MST", PubDBCn, SqlStr)
            PBar.Value = PBar.Minimum
            Do While Not RsEmployee.EOF

                mAddDays = CalcAddDays(RsEmployee.Fields("EMP_CODE").Value)

                If MainClass.ValidateWithMasterTable(RsEmployee.Fields("EMP_DEPT_CODE").Value, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDeptDesc = MasterNo
                Else
                    mDeptDesc = "-1"
                End If

                mDOB = IIf(IsDBNull(RsEmployee.Fields("EMP_DOB").Value), "", RsEmployee.Fields("EMP_DOB").Value)

                If mDOB = "" Then
                    mAge = 18
                Else
                    mAge = DateDiff(Microsoft.VisualBasic.DateInterval.Month, CDate(mDOB), CDate(mSalDate)) / 12
                End If

                If UpdateSalTrn(pSalaryType, RsEmployee.Fields("EMP_CODE").Value, mDeptDesc, RsEmployee.Fields("EMP_CATG").Value, RsEmployee.Fields("PAYMENTMODE").Value, IIf(IsDBNull(RsEmployee.Fields("EMP_BANK_NO").Value), "", RsEmployee.Fields("EMP_BANK_NO").Value), IIf(IsDBNull(RsEmployee.Fields("EMPBANK_IFSC").Value), "", RsEmployee.Fields("EMPBANK_IFSC").Value), "N", mAddDays, RsEmployee.Fields("EMP_DOJ").Value, IIf(IsDBNull(RsEmployee.Fields("EMP_LEAVE_DATE").Value), "", RsEmployee.Fields("EMP_LEAVE_DATE").Value), mSalDate, pDesignation, mAge, RsEmployee.Fields("DIV_CODE").Value) = False Then GoTo ErrPart

                RsEmployee.MoveNext()
                PBar.Value = PBar.Value + 1
            Loop

            'MsgBox("Salary Process Complete")
        Else
            'MsgBox("No Record Found For Processing.")
        End If

        'PubDBCn.CommitTrans()
        SalDummyProcess = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Function

ErrPart:
        SalDummyProcess = False
        'Resume
        'MsgInformation("Salary Process Not Complete, Try Again.")
        'PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Private Sub SalUnProcess()

        On Error GoTo ErrPart

        Dim RsSalTRN As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mMonth As String
        Dim mSalDate As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'PBar.Min = 0


        mMonth = UCase(VB6.Format(lblNewDate.Text, "MMM-YYYY"))

        SqlStr = ""
        mSalDate = MainClass.LastDay(Month(CDate(lblNewDate.Text)), Year(CDate(lblNewDate.Text))) & "/" & VB6.Format(lblNewDate.Text, "MM/YYYY")

        ''Check Validation

        If lblEmpType.Text = "D" Then
            PubDBCn.Errors.Clear()
            PubDBCn.BeginTrans()

            SqlStr = "DELETE FROM PAY_DUMMYSAL_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" ''& vbCrLf |                    & " AND TO_CHAR(SAL_DATE,'MON-YYYY')='" & mMonth & "' AND ISARREAR IN ('Y','N')"

            SqlStr = SqlStr & vbCrLf & " AND CATEGORY<>'C'"

            If OptParti.Checked = True Then
                SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & TxtCardNo.Text & "'"
            End If

            PubDBCn.Execute(SqlStr)

            SqlStr = "DELETE FROM PAY_DUMMYACTUAL_SAL_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" ''& vbCrLf |                    & " AND TO_CHAR(SAL_DATE,'MON-YYYY')='" & mMonth & "' AND ISARREAR IN ('Y','N')"

            SqlStr = SqlStr & vbCrLf & " AND CATEGORY<>'C'"

            If OptParti.Checked = True Then
                SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & TxtCardNo.Text & "'"
            End If

            PubDBCn.Execute(SqlStr)

            SqlStr = "DELETE FROM PAY_MONTHLY_DUMMY_OT_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" ''& vbCrLf |                    & " AND TO_CHAR(SAL_DATE,'MON-YYYY')='" & mMonth & "' AND ISARREAR IN ('Y','N')"

            'SqlStr = SqlStr & vbCrLf & " AND CATEGORY<>'C'"

            If OptParti.Checked = True Then
                SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & TxtCardNo.Text & "'"
            End If

            PubDBCn.Execute(SqlStr)

            PubDBCn.CommitTrans()
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        SqlStr = "Select COUNT(1) AS CNTREC From PAY_SAL_TRN " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SAL_DATE>TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND ISARREAR IN ('Y','N')"

        If Trim(lblEmpType.Text) = "S" Then
            SqlStr = SqlStr & vbCrLf & " AND EMP_CODE IN (SELECT EMP_CODE FROM PAY_EMPLOYEE_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CAT_TYPE='1')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND EMP_CODE IN (SELECT EMP_CODE FROM PAY_EMPLOYEE_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CAT_TYPE='2')"
        End If

        SqlStr = SqlStr & vbCrLf & " AND CATEGORY<>'C'"

        If OptParti.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & TxtCardNo.Text & "'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSalTRN, ADODB.LockTypeEnum.adLockOptimistic)
        If RsSalTRN.EOF = False Then
            If RsSalTRN.Fields("CNTREC").Value > 0 Then
                MsgInformation("You Cann't Process back Salary.")
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                Exit Sub
            End If
        End If

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        ''---------------------------------------
        SqlStr = "Select * From PAY_SAL_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TO_CHAR(SAL_DATE,'MON-YYYY')='" & mMonth & "' AND ISARREAR IN ('Y','N')"

        SqlStr = SqlStr & vbCrLf & " AND CATEGORY<>'C'"

        If Trim(lblEmpType.Text) = "S" Then
            SqlStr = SqlStr & vbCrLf & " AND EMP_CODE IN (SELECT EMP_CODE FROM PAY_EMPLOYEE_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CAT_TYPE='1')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND EMP_CODE IN (SELECT EMP_CODE FROM PAY_EMPLOYEE_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CAT_TYPE='2')"
        End If

        If OptParti.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & TxtCardNo.Text & "'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSalTRN, ADODB.LockTypeEnum.adLockOptimistic)

        If RsSalTRN.EOF = False Then
            SqlStr = CStr(MsgBox("Are you want Un-Processed Salary For This Period ... " & vbNewLine & vbNewLine & "Want To Reprocess ...", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2))
            If SqlStr = CStr(MsgBoxResult.Yes) Then
                SqlStr = "DELETE FROM PAY_SAL_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TO_CHAR(SAL_DATE,'MON-YYYY')='" & mMonth & "' AND ISARREAR IN ('Y','N')"

                SqlStr = SqlStr & vbCrLf & " AND CATEGORY<>'C'"

                If Trim(lblEmpType.Text) = "S" Then
                    SqlStr = SqlStr & vbCrLf & " AND EMP_CODE IN (SELECT EMP_CODE FROM PAY_EMPLOYEE_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CAT_TYPE='1')"
                Else
                    SqlStr = SqlStr & vbCrLf & " AND EMP_CODE IN (SELECT EMP_CODE FROM PAY_EMPLOYEE_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CAT_TYPE='2')"
                End If

                If OptParti.Checked = True Then
                    SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & TxtCardNo.Text & "'"
                End If

                PubDBCn.Execute(SqlStr)

                SqlStr = "DELETE FROM PAY_PERKS_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TO_CHAR(SAL_DATE,'MON-YYYY')='" & mMonth & "' AND BOOKTYPE IN ('S','A')"

                If Trim(lblEmpType.Text) = "S" Then
                    SqlStr = SqlStr & vbCrLf & " AND EMP_CODE IN (SELECT EMP_CODE FROM PAY_EMPLOYEE_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CAT_TYPE='1')"
                Else
                    SqlStr = SqlStr & vbCrLf & " AND EMP_CODE IN (SELECT EMP_CODE FROM PAY_EMPLOYEE_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CAT_TYPE='2')"
                End If

                If OptParti.Checked = True Then
                    SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & TxtCardNo.Text & "'"
                End If

                PubDBCn.Execute(SqlStr)

                SqlStr = "DELETE FROM PAY_PFESI_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TO_CHAR(SAL_DATE,'MON-YYYY')='" & mMonth & "'" & vbCrLf & " AND ISARREAR IN ('Y','N')"

                '            SqlStr = SqlStr & vbCrLf _
                ''                    & " AND EMP_CODE IN (" & vbCrLf _
                ''                    & " SELECT EMP_CODE FROM PAY_EMPLOYEE_MST " & vbCrLf _
                ''                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                ''                    & " AND EMP_CATG<>'C')"

                If Trim(lblEmpType.Text) = "S" Then
                    SqlStr = SqlStr & vbCrLf & " AND EMP_CODE IN (SELECT EMP_CODE FROM PAY_EMPLOYEE_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CAT_TYPE='1')"
                Else
                    SqlStr = SqlStr & vbCrLf & " AND EMP_CODE IN (SELECT EMP_CODE FROM PAY_EMPLOYEE_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CAT_TYPE='2')"
                End If

                If OptParti.Checked = True Then
                    SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & Trim(TxtCardNo.Text) & "'"
                End If

                PubDBCn.Execute(SqlStr)

                SqlStr = "DELETE FROM PAY_ACTUAL_SAL_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TO_CHAR(SAL_DATE,'MON-YYYY')='" & mMonth & "' AND ISARREAR IN ('Y','N')"

                SqlStr = SqlStr & vbCrLf & " AND CATEGORY<>'C'"

                If Trim(lblEmpType.Text) = "S" Then
                    SqlStr = SqlStr & vbCrLf & " AND EMP_CODE IN (SELECT EMP_CODE FROM PAY_EMPLOYEE_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CAT_TYPE='1')"
                Else
                    SqlStr = SqlStr & vbCrLf & " AND EMP_CODE IN (SELECT EMP_CODE FROM PAY_EMPLOYEE_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CAT_TYPE='2')"
                End If

                If OptParti.Checked = True Then
                    SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & TxtCardNo.Text & "'"
                End If

                PubDBCn.Execute(SqlStr)
            End If
        End If

        PubDBCn.CommitTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub

ErrPart:
        'Resume
        MsgInformation("Salary Un-Process Not Complete, Try Again.")
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Function OTProcess(ByRef pSalaryType As String) As Boolean

        On Error GoTo ErrPart

        Dim RsSalTRN As ADODB.Recordset = Nothing
        Dim RsEmployee As ADODB.Recordset
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsDivision As ADODB.Recordset
        Dim mDivisionCode As Double
        Dim SqlStr As String = ""
        Static mDOJ As String
        Static mDOL As String
        Dim mMonth As String
        Dim mSalDate As String
        Dim mCalcArrear As String
        Dim mAddDays As Double
        Dim mDeptDesc As String
        Dim mDesgDesc As String
        Dim RsOTTRN As ADODB.Recordset
        Dim RsADJOTTRN As ADODB.Recordset
        Dim mOTHour As Double
        Dim mOTMin As Double
        Dim mADjOTMin As Double
        Dim mADjOTHr As Double
        Dim mYYMM As Integer
        Dim RsEmpCat As ADODB.Recordset
        Dim mCategory As String
        Dim mEmpCode As String
        Dim mOverTimeAppType As String
        Dim mSalaryType As String
        Dim mTotalOTinMin As Double

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        OTProcess = False
        mMonth = UCase(VB6.Format(lblNewDate.Text, "MMM-YYYY"))

        SqlStr = ""
        mSalDate = MainClass.LastDay(Month(CDate(lblNewDate.Text)), Year(CDate(lblNewDate.Text))) & "/" & VB6.Format(lblNewDate.Text, "MM/YYYY")
        mDOJ = MainClass.LastDay(Month(CDate(lblNewDate.Text)), Year(CDate(lblNewDate.Text))) & "/" & VB6.Format(lblNewDate.Text, "MM/YYYY")
        mDOL = "01" & "/" & VB6.Format(lblNewDate.Text, "MM/YYYY")
        ConWorkDay = MainClass.LastDay(Month(CDate(lblNewDate.Text)), Year(CDate(lblNewDate.Text)))

        SqlStr = " SELECT EMP.EMP_NAME, EMP.EMP_CODE, " & vbCrLf _
            & " EMP.EMP_BANK_NO, EMP.PAYMENTMODE, EMP.EMP_DEPT_CODE,EMP_RATE_TYPE " & vbCrLf _
            & " FROM PAY_EMPLOYEE_MST EMP " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " EMP.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP.EMP_STOP_SALARY='N' " & vbCrLf _
            & " AND EMP.EMP_DOJ<=TO_DATE('" & VB6.Format(mDOJ, "dd-mmm-yyyy") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND (EMP_LEAVE_DATE>TO_DATE('" & VB6.Format(mDOL, "dd-mmm-yyyy") & "','DD-MON-YYYY') OR EMP_LEAVE_DATE IS NULL)"

        SqlStr = SqlStr & vbCrLf & " AND EMP_CATG<>'C'"

        If OptParti.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_CODE='" & TxtCardNo.Text & "'"
        End If

        SqlStr = SqlStr & vbCrLf _
            & " AND EMP.EMP_CODE NOT IN (SELECT EMP_CODE FROM " & vbCrLf _
            & " PAY_FFSETTLE_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND TO_CHAR(EMP_LEAVE_DATE,'MM-YYYY')='" & VB6.Format(mDOL, "MM-YYYY") & "' AND PAID_DAYS>0)"

        SqlStr = SqlStr & vbCrLf _
            & "GROUP BY EMP.EMP_NAME, EMP.EMP_CODE, " & vbCrLf _
            & " EMP.EMP_BANK_NO, EMP.PAYMENTMODE,EMP.EMP_DEPT_CODE,EMP_RATE_TYPE "

        SqlStr = SqlStr & vbCrLf & " Order By EMP_CODE"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmployee, ADODB.LockTypeEnum.adLockOptimistic)

        SqlStr = "Select * From PAY_MONTHLY_OT_TRN " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND TO_CHAR(OT_Date,'MON-YYYY')='" & mMonth & "'"

        SqlStr = SqlStr & vbCrLf _
            & " AND EMP_CODE IN (SELECT EMP_CODE " & vbCrLf _
            & " FROM PAY_EMPLOYEE_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CATG<>'C')"

        If OptParti.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & TxtCardNo.Text & "'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSalTRN, ADODB.LockTypeEnum.adLockOptimistic)

        If RsSalTRN.EOF = False Then
            SqlStr = CStr(MsgBoxResult.Yes)     ''CStr(MsgBox("Over Time Already Processed For This Period ... " & vbNewLine & vbNewLine & "Want To Reprocess ...", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2))
            If SqlStr = CStr(MsgBoxResult.Yes) Then

                SqlStr = "DELETE FROM PAY_MONTHLY_OT_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND TO_CHAR(OT_DATE,'MON-YYYY')='" & mMonth & "'"

                SqlStr = SqlStr & vbCrLf _
                    & " AND EMP_CODE IN (SELECT EMP_CODE " & vbCrLf _
                    & " FROM PAY_EMPLOYEE_MST " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CATG<>'C')"

                If OptParti.Checked = True Then
                    SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & TxtCardNo.Text & "'"
                End If

                PubDBCn.Execute(SqlStr)

                SqlStr = "DELETE FROM PAY_PFESI_TRN " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND TO_CHAR(SAL_DATE,'MON-YYYY')='" & mMonth & "'" & vbCrLf & " AND ISARREAR IN('O','X')"

                SqlStr = SqlStr & vbCrLf & " AND EMP_CODE IN (SELECT EMP_CODE " & vbCrLf & " FROM PAY_EMPLOYEE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CATG<>'C')"

                If OptParti.Checked = True Then
                    SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & TxtCardNo.Text & "'"
                End If

                PubDBCn.Execute(SqlStr)
            Else
                OTProcess = True
                PBar.Visible = False
                Exit Function
            End If
        End If


        If RsEmployee.EOF = False Then
            PBar.Visible = True

            PBar.Minimum = 0
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
            If OptParti.Checked = True Then
                SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & TxtCardNo.Text & "'"
            End If

            PBar.Maximum = MainClass.GetMaxRecord("PAY_EMPLOYEE_MST", PubDBCn, SqlStr)
            PBar.Value = PBar.Minimum
            Do While Not RsEmployee.EOF
                mEmpCode = IIf(IsDBNull(RsEmployee.Fields("EMP_CODE").Value), "", RsEmployee.Fields("EMP_CODE").Value)
                mSalaryType = IIf(IsDBNull(RsEmployee.Fields("EMP_RATE_TYPE").Value), "G", RsEmployee.Fields("EMP_RATE_TYPE").Value)

                If mSalaryType = "P" Then
                    If UpdateOTTrn(mEmpCode, 0, 0, mSalDate) = False Then GoTo ErrPart
                Else
                    SqlStr = " SELECT " & vbCrLf _
                        & " SUM(OT.OTHOUR+OT.PREV_OTHOUR) AS OTHOUR , SUM(OT.OTMIN+OT.PREV_OTMIN)AS OTMIN " & vbCrLf _
                        & " FROM PAY_OVERTIME_MST OT " & vbCrLf _
                        & " WHERE " & vbCrLf _
                        & " OT.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND OT.EMP_CODE = '" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf _
                        & " AND TO_CHAR(OT.OT_DATE,'MON-YYYY')='" & UCase(VB6.Format(lblNewDate.Text, "MMM-YYYY")) & "'"

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOTTRN, ADODB.LockTypeEnum.adLockOptimistic)

                    If RsOTTRN.EOF = False Then
                        mOverTimeAppType = "0"
                        If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "OVERTIME_APP", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mOverTimeAppType = MasterNo
                        End If
                        If mOverTimeAppType = "1" Or mOverTimeAppType = "3" Then
                            mOTHour = IIf(IsDBNull(RsOTTRN.Fields("OTHOUR").Value), 0, RsOTTRN.Fields("OTHOUR").Value)
                            mOTMin = IIf(IsDBNull(RsOTTRN.Fields("OTMIN").Value), 0, RsOTTRN.Fields("OTMIN").Value)
                            mADjOTMin = 0
                            '' where 

                            If CDate(lblNewDate.Text) < CDate("01/10/2023") Then
                                SqlStr = " SELECT " & vbCrLf _
                                        & " SUM(OT_HOURS) AS OTMIN " & vbCrLf _
                                        & " FROM PAY_MOVEMENT_TRN OT " & vbCrLf _
                                        & " WHERE " & vbCrLf _
                                        & " OT.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                                        & " AND OT.EMP_CODE = '" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf _
                                        & " AND TO_CHAR(OT.REF_DATE,'MON-YYYY')='" & UCase(VB6.Format(lblNewDate.Text, "MMM-YYYY")) & "'" & vbCrLf _
                                        & " AND HR_APPROVAL='Y' AND AGT_OT='Y'"

                                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsADJOTTRN, ADODB.LockTypeEnum.adLockOptimistic)

                                If RsADJOTTRN.EOF = False Then
                                    mTotalOTinMin = (mOTHour * 60) + mOTMin
                                    mADjOTMin = IIf(IsDBNull(RsADJOTTRN.Fields("OTMIN").Value), 0, RsADJOTTRN.Fields("OTMIN").Value)
                                    mTotalOTinMin = mTotalOTinMin - mADjOTMin

                                    mOTHour = Int(mTotalOTinMin / 60)
                                    mOTMin = mTotalOTinMin - (Int(mTotalOTinMin / 60) * 60)

                                End If
                            End If
                            If mOTHour + mOTMin > 0 Or GetIncentive_Adj(mEmpCode, mSalDate) <> 0 Then
                                    If UpdateOTTrn(mEmpCode, mOTHour, mOTMin, mSalDate) = False Then GoTo ErrPart
                                End If
                            Else
                                'If pSalaryType <> "D" Then
                                '    If GetIncentive_Adj(mEmpCode, mSalDate) <> 0 Then
                                '        If UpdateOTTrn(mEmpCode, 0, 0, mSalDate) = False Then GoTo ErrPart
                                '    End If
                                'End If


                            End If
                    End If

                    If UpdateArrearOTTrn(mEmpCode, mSalDate) = False Then GoTo ErrPart
                End If

                RsEmployee.MoveNext()
                PBar.Value = PBar.Value + 1
            Loop

            mYYMM = CInt(VB6.Format(lblNewDate.Text, "YYYYMM"))

            If OptAll.Checked = True Then
                SqlStr = "SELECT DIV_CODE FROM INV_DIVISION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DIV_CODE"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDivision, ADODB.LockTypeEnum.adLockReadOnly)

                If RsDivision.EOF = False Then
                    Do While RsDivision.EOF = False
                        mDivisionCode = IIf(IsDBNull(RsDivision.Fields("DIV_CODE").Value), -1, RsDivision.Fields("DIV_CODE").Value)

                        SqlStr = "SELECT  DISTINCT CATEGORY_CODE FROM PAY_CATEGORY_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ORDER BY CATEGORY_CODE"
                        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmpCat, ADODB.LockTypeEnum.adLockReadOnly)

                        If RsEmpCat.EOF = False Then
                            Do While RsEmpCat.EOF = False
                                mCategory = IIf(IsDBNull(RsEmpCat.Fields("CATEGORY_CODE").Value), "", RsEmpCat.Fields("CATEGORY_CODE").Value)
                                If UpdateOTAccountPostingHead(mYYMM, "N", mCategory, "O", "O", mCategory, mDivisionCode) = False Then GoTo ErrPart
                                If UpdateOTAccountPostingHead(mYYMM, "Y", mCategory, "O", "X", mCategory, mDivisionCode) = False Then GoTo ErrPart
                                RsEmpCat.MoveNext()
                            Loop
                        End If

                        RsDivision.MoveNext()
                    Loop
                End If
            End If

            'MsgBox("Over Time Process Complete")
        Else
            'MsgBox("No Record Found For Processing.")
        End If

        OTProcess = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Function

ErrPart:
        OTProcess = False
        'PubDBCn.RollbackTrans()
        'MsgInformation("Over Time Process Not Complete, Try Again.")

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function



    Private Function OTDummyProcess(ByRef pSalaryType As String) As Boolean

        On Error GoTo ErrPart

        Dim RsSalTRN As ADODB.Recordset = Nothing
        Dim RsEmployee As ADODB.Recordset
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsDivision As ADODB.Recordset
        Dim mDivisionCode As Double
        Dim SqlStr As String = ""
        Dim mDOJ As String
        Dim mDOL As String
        Dim mMonth As String
        Dim mSalDate As String
        Dim mCalcArrear As String
        Dim mAddDays As Double
        Dim mDeptDesc As String
        Dim mDesgDesc As String
        Dim RsOTTRN As ADODB.Recordset
        Dim mOTHour As Double
        Dim mOTMin As Double
        Dim mYYMM As Integer
        Dim RsEmpCat As ADODB.Recordset
        Dim mCategory As String
        Dim pEmpCode As String
        Dim mSalaryType As String
        Dim mOverTimeAppType As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        OTDummyProcess = False
        mMonth = UCase(VB6.Format(lblNewDate.Text, "MMM-YYYY"))

        SqlStr = ""
        mSalDate = MainClass.LastDay(Month(CDate(lblNewDate.Text)), Year(CDate(lblNewDate.Text))) & "/" & VB6.Format(lblNewDate.Text, "MM/YYYY")
        mDOJ = MainClass.LastDay(Month(CDate(lblNewDate.Text)), Year(CDate(lblNewDate.Text))) & "/" & VB6.Format(lblNewDate.Text, "MM/YYYY")
        mDOL = "01" & "/" & VB6.Format(lblNewDate.Text, "MM/YYYY")
        ConWorkDay = MainClass.LastDay(Month(CDate(lblNewDate.Text)), Year(CDate(lblNewDate.Text)))

        SqlStr = " SELECT EMP.EMP_NAME, EMP.EMP_CODE, EMP_RATE_TYPE," & vbCrLf _
            & " EMP.EMP_BANK_NO, EMP.PAYMENTMODE, EMP.EMP_DEPT_CODE " & vbCrLf _
            & " FROM PAY_EMPLOYEE_MST EMP " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " EMP.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP.EMP_STOP_SALARY='N' " & vbCrLf _
            & " AND EMP.EMP_DOJ<=TO_DATE('" & VB6.Format(mDOJ, "dd-mmm-yyyy") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND (EMP_LEAVE_DATE>TO_DATE('" & VB6.Format(mDOL, "dd-mmm-yyyy") & "','DD-MON-YYYY') OR EMP_LEAVE_DATE IS NULL)"

        SqlStr = SqlStr & vbCrLf & " AND EMP_CATG<>'C'"

        If OptParti.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_CODE='" & TxtCardNo.Text & "'"
        End If

        SqlStr = SqlStr & vbCrLf _
            & " AND EMP.EMP_CODE NOT IN (SELECT EMP_CODE FROM " & vbCrLf _
            & " PAY_FFSETTLE_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND TO_CHAR(EMP_LEAVE_DATE,'MM-YYYY')='" & VB6.Format(mDOL, "MM-YYYY") & "' AND PAID_DAYS>0)"

        SqlStr = SqlStr & vbCrLf _
            & "GROUP BY EMP.EMP_NAME, EMP.EMP_CODE, EMP_RATE_TYPE," & vbCrLf _
            & " EMP.EMP_BANK_NO, EMP.PAYMENTMODE,EMP.EMP_DEPT_CODE "

        SqlStr = SqlStr & vbCrLf & " Order By EMP_CODE"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmployee, ADODB.LockTypeEnum.adLockOptimistic)

        SqlStr = "DELETE FROM PAY_MONTHLY_DUMMY_OT_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND TO_CHAR(OT_DATE,'MON-YYYY')='" & mMonth & "'"

        SqlStr = SqlStr & vbCrLf & " AND EMP_CODE IN (SELECT EMP_CODE " & vbCrLf _
            & " FROM PAY_EMPLOYEE_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CATG<>'C')"

        If OptParti.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & TxtCardNo.Text & "'"
        End If

        PubDBCn.Execute(SqlStr)


        If RsEmployee.EOF = False Then
            PBar.Visible = True

            PBar.Minimum = 0
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
            If OptParti.Checked = True Then
                SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & TxtCardNo.Text & "'"
            End If

            PBar.Maximum = MainClass.GetMaxRecord("PAY_EMPLOYEE_MST", PubDBCn, SqlStr)
            PBar.Value = PBar.Minimum
            Do While Not RsEmployee.EOF

                pEmpCode = IIf(IsDBNull(RsEmployee.Fields("EMP_CODE").Value), "", RsEmployee.Fields("EMP_CODE").Value)
                mSalaryType = IIf(IsDBNull(RsEmployee.Fields("EMP_RATE_TYPE").Value), "G", RsEmployee.Fields("EMP_RATE_TYPE").Value)

                If mSalaryType = "P" Then
                    If UpdateOTTrn(pEmpCode, 0, 0, mSalDate) = False Then GoTo ErrPart
                Else
                    SqlStr = " SELECT " & vbCrLf _
                        & " SUM(OT.OTHOUR+OT.PREV_OTHOUR) AS OTHOUR , SUM(OT.OTMIN+OT.PREV_OTMIN) AS OTMIN " & vbCrLf _
                        & " FROM PAY_OVERTIME_MST OT " & vbCrLf _
                        & " WHERE " & vbCrLf _
                        & " OT.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND OT.EMP_CODE = '" & MainClass.AllowSingleQuote(pEmpCode) & "'" & vbCrLf _
                        & " AND TO_CHAR(OT.OT_DATE,'MON-YYYY')='" & UCase(VB6.Format(lblNewDate.Text, "MMM-YYYY")) & "'"

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOTTRN, ADODB.LockTypeEnum.adLockOptimistic)

                    If RsOTTRN.EOF = False Then
                        mOverTimeAppType = "0"
                        If MainClass.ValidateWithMasterTable(pEmpCode, "EMP_CODE", "OVERTIME_APP", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mOverTimeAppType = MasterNo
                        End If
                        If mOverTimeAppType = "1" Or mOverTimeAppType = "3" Then
                            mOTHour = IIf(IsDBNull(RsOTTRN.Fields("OTHOUR").Value), 0, RsOTTRN.Fields("OTHOUR").Value)
                            mOTMin = IIf(IsDBNull(RsOTTRN.Fields("OTMIN").Value), 0, RsOTTRN.Fields("OTMIN").Value)
                            If mOTHour + mOTMin > 0 Then
                                If UpdateOTTrn(pEmpCode, mOTHour, mOTMin, mSalDate) = False Then GoTo ErrPart
                            End If
                        End If
                    End If
                End If

                ''For Dummy Process not required.
                '            If UpdateArrearOTTrn(RsEmployee!EMP_CODE, mSalDate) = False Then GoTo ErrPart

                RsEmployee.MoveNext()
                PBar.Value = PBar.Value + 1
            Loop

            'MsgBox("Checking Purpose Over Time Process Complete")
        Else
            'MsgBox("No Record Found For Processing.")
        End If

        'PubDBCn.CommitTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        OTDummyProcess = True
        Exit Function

ErrPart:
        'MsgInformation("Over Time Process Not Complete, Try Again.")
        OTDummyProcess = False
        'PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Sub OTUnProcess()

        On Error GoTo ErrPart

        Dim RsSalTRN As ADODB.Recordset = Nothing
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mMonth As String
        Dim mSalDate As String
        Dim mCalcArrear As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        mMonth = UCase(VB6.Format(lblNewDate.Text, "MMM-YYYY"))

        SqlStr = ""
        mSalDate = MainClass.LastDay(Month(CDate(lblNewDate.Text)), Year(CDate(lblNewDate.Text))) & "/" & VB6.Format(lblNewDate.Text, "MM/YYYY")


        SqlStr = "Select COUNT(1) AS CNTREC From PAY_MONTHLY_OT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND OT_DATE>TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " AND EMP_CODE IN (" & vbCrLf & " SELECT EMP_CODE FROM PAY_EMPLOYEE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CATG<>'C')"

        If OptParti.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & TxtCardNo.Text & "'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
        If RsTemp.EOF = False Then
            If RsTemp.Fields("CNTREC").Value > 0 Then
                MsgInformation("You Cann't Un-Process back Salary.")
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                Exit Sub
            End If
        End If

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If lblEmpType.Text = "D" Then

            SqlStr = "DELETE FROM PAY_MONTHLY_DUMMY_OT_TRN " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND TO_CHAR(OT_DATE,'MON-YYYY')='" & mMonth & "'"

            SqlStr = SqlStr & vbCrLf & " AND EMP_CODE IN (" & vbCrLf _
                & " SELECT EMP_CODE FROM PAY_EMPLOYEE_MST " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND EMP_CATG<>'C')"

            If OptParti.Checked = True Then
                SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & TxtCardNo.Text & "'"
            End If

            PubDBCn.Execute(SqlStr)

        End If


        SqlStr = "Select * From PAY_MONTHLY_OT_TRN " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND TO_CHAR(OT_Date,'MON-YYYY')='" & mMonth & "'"

        SqlStr = SqlStr & vbCrLf _
            & " AND EMP_CODE IN (" & vbCrLf _
            & " SELECT EMP_CODE FROM PAY_EMPLOYEE_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CATG<>'C')"

        If OptParti.Checked = True Then
            SqlStr = SqlStr & vbCrLf _
                & " AND EMP_CODE='" & TxtCardNo.Text & "'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSalTRN, ADODB.LockTypeEnum.adLockOptimistic)

        If RsSalTRN.EOF = False Then
            SqlStr = CStr(MsgBox("Are you Un-Processed Over Time For This Period ... " & vbNewLine & vbNewLine & "Want To Reprocess ...", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2))
            If SqlStr = CStr(MsgBoxResult.Yes) Then
                SqlStr = "DELETE FROM PAY_MONTHLY_OT_TRN " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND TO_CHAR(OT_DATE,'MON-YYYY')='" & mMonth & "'"

                SqlStr = SqlStr & vbCrLf _
                    & " AND EMP_CODE IN (" & vbCrLf _
                    & " SELECT EMP_CODE FROM PAY_EMPLOYEE_MST " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND EMP_CATG<>'C')"

                If OptParti.Checked = True Then
                    SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & TxtCardNo.Text & "'"
                End If

                PubDBCn.Execute(SqlStr)

                SqlStr = "DELETE FROM PAY_PFESI_TRN " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND TO_CHAR(SAL_DATE,'MON-YYYY')='" & mMonth & "'" & vbCrLf _
                    & " AND ISARREAR IN ('O','X')"

                SqlStr = SqlStr & vbCrLf & " AND EMP_CODE IN (" & vbCrLf _
                    & " SELECT EMP_CODE FROM PAY_EMPLOYEE_MST " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND EMP_CATG<>'C')"

                If OptParti.Checked = True Then
                    SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & TxtCardNo.Text & "'"
                End If

                PubDBCn.Execute(SqlStr)

            End If
        End If

        PubDBCn.CommitTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub

ErrPart:
        MsgInformation("Over Time Process Not Complete, Try Again.")
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Function CalcAllowance(ByRef mCode As String, ByRef pWEFDate As String, ByRef pADDDeduct As Integer, mField As String) As Double

        On Error GoTo ErrGetLTAAmount
        Dim SqlStr As String

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mBonusAmount As Double

        SqlStr = " SELECT SUM(" & mField & ") AS AMOUNT " & vbCrLf _
            & " FROM PAY_SALARYDEF_MST A, PAY_SALARYHEAD_MST B" & vbCrLf _
            & " WHERE A.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND A.COMPANY_CODE = B.COMPANY_CODE" & vbCrLf _
            & " AND A.ADD_DEDUCTCODE=B.CODE" & vbCrLf & " AND A.EMP_CODE = '" & mCode & "'"

        '' AND B.NAME='" & MainClass.AllowSingleQuote(mSalHeadName) & "'
        SqlStr = SqlStr & vbCrLf & " AND B.ADDDEDUCT=" & pADDDeduct & " AND B.ISSALPART='N'"

        'If RsCompany.Fields("COMPANY_CODE").Value = 16 And pADDDeduct = 3 Then
        '    SqlStr = SqlStr & vbCrLf & " AND B.TYPE <> " & ConBonus & ""
        'End If

        SqlStr = SqlStr & vbCrLf _
            & " AND SALARY_EFF_DATE=(SELECT MAX(SALARY_EFF_DATE) " & vbCrLf _
            & " FROM PAY_SALARYDEF_MST" & vbCrLf _
            & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE = '" & mCode & "'" & vbCrLf _
            & " AND SALARY_EFF_DATE<=TO_DATE('" & VB6.Format(pWEFDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            CalcAllowance = IIf(IsDBNull(RsTemp.Fields("Amount").Value), "", RsTemp.Fields("Amount").Value)
        Else
            CalcAllowance = 0
        End If

        'mBonusAmount = 0

        'If RsCompany.Fields("COMPANY_CODE").Value = 16 And pADDDeduct = 3 Then
        '    mBonusAmount = (GetBonusCeilingAmount(mCode, pWEFDate)) ' * mMonthWDays / MainClass.LastDay(Month(mFromDate), Year(mFromDate)
        'End If

        'CalcAllowance = CalcAllowance + mBonusAmount
        CalcAllowance = System.Math.Round(CalcAllowance, 0)

        Exit Function
ErrGetLTAAmount:
        CalcAllowance = 0
    End Function
    Private Function UpdateSalTrn(ByRef pSalaryType As String, ByRef mCode As String, ByRef mDepartment As String, ByRef mCategory As String, ByRef mPaymentMode As Integer, ByRef mBankAcctNo As String, ByRef mBankIFSCCode As String, ByRef mArrearCalc As String, ByRef mAddDays As Double, ByRef mEmpDOJ As String, ByRef mDOL As String, ByRef mSalDate As String, ByRef pDesignation As String, ByRef pAge As Double, ByRef mDivisionCode As Double) As Boolean


        On Error GoTo UpDateSalTrnErr
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsVar As ADODB.Recordset

        Dim SqlStr As String = ""
        Dim mSalary As Double
        Dim mPayableSalary As Double
        Dim mActPayableSalary As Double

        'Dim mSalary As Double
        'Dim mPayableSalary As Double

        Dim mWOLayoffPayableSalary As Double
        Dim mWDays As Double
        Dim mActualWDays As Double
        Dim mSalHeadCode As Integer
        Dim mAmount As Double
        Dim mActualAmount As Double
        Dim mRound As String
        Dim mPayablePFSalary As Double
        Dim mPayableESISalary As Double
        Dim mPayableWelfareSalary As Double
        Dim mActualWelFareSalary As Double
        Dim mActualPFSalary As Double
        Dim mActualESISalary As Double
        Dim mPayablePensionWages As Double
        Dim mRounding As Double
        Dim mPFAmt As Double
        Dim mNewPFRate As Double
        Dim mPensionFund As Double
        Dim mEmpCont As Double
        Dim mESIAmt As Double
        Dim mTotalWop_Absent As Double

        Dim mWOP As Double
        Dim mAbsent As Double

        Dim mPFRounding As Double
        Dim mESIRounding As Double
        Dim mTempPFCeiling As Double
        Dim mDesignation As String
        Dim mEmployer_PF As Double
        Dim mEmpContOn As String
        Dim mOPDate As String
        Dim mVPFRate As Double
        Dim mVPFAmount As Double
        Dim mPayableVariableESI As Double
        Dim mTable As String
        Dim mSaboPayableSalary As Double
        Dim mLayoffDays As Double
        Dim pLayOffDateStart As String
        Dim pLayOffDateEnd As String
        Dim mMonthStart As String
        Dim mWDays_Layoff As Double
        Dim mTotalWop_Absent_Layoff As Double
        Dim mWOP_Layoff As Double
        Dim mAbsent_Layoff As Double
        Dim mActualTable As String
        Dim mActSalary As String
        Dim mOTAmount As Double

        If lblEmpType.Text = "D" Then
            mTable = IIf(pSalaryType = "F", "PAY_DUMMYSAL_TRN", "PAY_DUMMYACTUAL_SAL_TRN")        ''"PAY_DUMMYSAL_TRN"
        Else
            mTable = IIf(pSalaryType = "F", "PAY_SAL_TRN", "PAY_ACTUAL_SAL_TRN")
        End If

        If lblEmpType.Text = "D" Then
            mActualTable = "PAY_DUMMYACTUAL_SAL_TRN"
        Else
            mActualTable = "PAY_ACTUAL_SAL_TRN"
        End If


        If pSalaryType = "F" Then
            SqlStr = " SELECT SALARYDEF.*, DECODE(SALARYDEF.FORM1_BASICSALARY,0,SALARYDEF.BASICSALARY,SALARYDEF.FORM1_BASICSALARY) AS ACT_BASICSALARY,"
        Else
            SqlStr = " SELECT SALARYDEF.COMPANY_CODE, SALARYDEF.FYEAR, SALARYDEF.EMP_CODE, SALARYDEF.SALARY_EFF_DATE, SALARYDEF.ADD_DEDUCTCODE, SALARYDEF.PERCENTAGE, SALARYDEF.SALARY_APP_DATE, SALARYDEF.PREVIOUS_BASICSALARY, SALARYDEF.PREVIOUS_AMOUNT, SALARYDEF.ARREAR_DATE, SALARYDEF.TOT_ARR_MONTH, SALARYDEF.IS_ARREAR, SALARYDEF.EMP_DESG_CODE, SALARYDEF.ADDDAYS_IN, SALARYDEF.ADDUSER, SALARYDEF.ADDDATE, SALARYDEF.MODUSER, SALARYDEF.MODDATE, SALARYDEF.EMP_CONT, SALARYDEF.NEXT_INC_DATE, DECODE(SALARYDEF.FORM1_BASICSALARY,0,SALARYDEF.BASICSALARY,SALARYDEF.FORM1_BASICSALARY) AS BASICSALARY, DECODE(FORM1_BASICSALARY,0,SALARYDEF.AMOUNT,SALARYDEF.FORM1_AMOUNT) AS AMOUNT, SALARYDEF.PREVIOUS_FORM1_BASICSALARY, SALARYDEF.PREVIOUS_FORM1_AMOUNT,DECODE(SALARYDEF.FORM1_BASICSALARY,0,SALARYDEF.BASICSALARY,SALARYDEF.FORM1_BASICSALARY) AS ACT_BASICSALARY,"
        End If

        SqlStr = SqlStr & vbCrLf _
            & " ADD_DEDUCT.CODE, ADD_DEDUCT.TYPE, ADD_DEDUCT.CALC_ON, ADD_DEDUCT.ADDDEDUCT," & vbCrLf _
            & " ADD_DEDUCT.INCLUDEDPF,ADD_DEDUCT.INCLUDEDESI, " & vbCrLf _
            & " ADD_DEDUCT.ROUNDING AS ROUNDING,EMP_CONT " & vbCrLf _
            & " FROM PAY_SALARYDEF_MST SALARYDEF, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf _
            & " WHERE " & vbCrLf _
            & " SALARYDEF.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " And ADD_DEDUCT.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " And SALARYDEF.COMPANY_CODE=ADD_DEDUCT.COMPANY_CODE" & vbCrLf _
            & " And SALARYDEF.ADD_DEDUCTCODE=ADD_DEDUCT.CODE" & vbCrLf _
            & " And SALARYDEF.EMP_CODE = '" & mCode & "'" & vbCrLf _
            & " AND ADD_DEDUCT.CALC_ON<>" & ConCalcVariable & "" & vbCrLf _
            & " AND SALARYDEF.SALARY_EFF_DATE=( SELECT MAX(SALARY_EFF_DATE)" & vbCrLf _
            & " FROM PAY_SALARYDEF_MST WHERE " & vbCrLf _
            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE = '" & mCode & "'" & vbCrLf _
            & " AND SALARY_APP_DATE <= TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

        SqlStr = SqlStr & vbCrLf & " AND ADD_DEDUCT.CODE IN (" & vbCrLf & " SELECT CODE FROM PAY_SALARYHEAD_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ADDDEDUCT IN (" & ConEarning & "," & ConDeduct & ")" & vbCrLf & " AND STATUS='O' AND (CLOSED_DATE IS NULL OR CLOSED_DATE<=TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))" & vbCrLf & " UNION " & vbCrLf & " SELECT CODE FROM PAY_SALARYHEAD_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ADDDEDUCT IN (" & ConEarning & "," & ConDeduct & ")" & vbCrLf & " AND STATUS='C' AND CLOSED_DATE>TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            If IsDBNull(RsTemp.Fields("BASICSALARY").Value) Then GoTo NextRec
            mSalary = RsTemp.Fields("BASICSALARY").Value
            mActSalary = RsTemp.Fields("ACT_BASICSALARY").Value

            mTotalWop_Absent = 0
            mWOP = 0
            mAbsent = 0

            mWDays_Layoff = 0
            mTotalWop_Absent_Layoff = 0
            mWOP_Layoff = 0
            mAbsent_Layoff = 0

            If GetLayoffMonth(mSalDate, pLayOffDateStart, pLayOffDateEnd) = True Then
                mMonthStart = VB6.Format("01-" & VB6.Format(mSalDate, "MMM-YYYY"), "DD/MM/YYYY")

                pLayOffDateStart = IIf(CDate(pLayOffDateStart) < CDate(mMonthStart), mMonthStart, pLayOffDateStart)
                pLayOffDateEnd = IIf(CDate(pLayOffDateEnd) > CDate(mSalDate), mSalDate, pLayOffDateEnd)
                mLayoffDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(pLayOffDateStart), CDate(pLayOffDateEnd)) + 1
                '            mEmpDOJ =IIf(CDate(mEmpDOJ) < CDate("01-" & vb6.Format(mSalDate, "MMM-YYYY")), "01-" & vb6.Format(mSalDate, "MMM-YYYY"), pLayOffDateStart)
                mWDays_Layoff = CalcAttn(mCode, pLayOffDateStart, pLayOffDateEnd, mSalDate, mTotalWop_Absent_Layoff, , , mWOP_Layoff, mAbsent_Layoff)
                mLayoffDays = mLayoffDays - mTotalWop_Absent_Layoff
                If CDate(pLayOffDateStart) = CDate(mMonthStart) And CDate(pLayOffDateEnd) = CDate(mSalDate) Then
                    mWDays = 0
                Else
                    mWDays = mAddDays + CalcAttn(mCode, mEmpDOJ, CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(pLayOffDateStart))), mSalDate, mTotalWop_Absent, , , mWOP, mAbsent)
                End If


            Else
                mLayoffDays = 0
                mWDays = mAddDays + CalcAttn(mCode, mEmpDOJ, mDOL, mSalDate, mTotalWop_Absent, , , mWOP, mAbsent, pSalaryType)
                'mWDays = Int(mWDays) + IIf(mWDays - Int(mWDays) >= 0.5, 0.5, 0)
            End If

            mEmpContOn = IIf(IsDBNull(RsTemp.Fields("EMP_CONT").Value), "B", RsTemp.Fields("EMP_CONT").Value)

            If MainClass.ValidateWithMasterTable(RsTemp.Fields("EMP_DESG_CODE").Value, "DESG_CODE", "DESG_DESC", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDesignation = MasterNo
            Else
                mDesignation = "-1"
            End If

            '        mTotalWop_Absent = MainClass.LastDay(Month(lblNewDate.Caption), Year(lblNewDate.Caption)) - mWDays


            mPayableSalary = CDbl(VB6.Format(mSalary * mWDays / MainClass.LastDay(Month(CDate(mSalDate)), Year(CDate(mSalDate))), "0.00"))
            mWOLayoffPayableSalary = mPayableSalary
            mPayableSalary = mPayableSalary + CDbl(VB6.Format(mSalary * mLayoffDays * 0.5 / MainClass.LastDay(Month(CDate(mSalDate)), Year(CDate(mSalDate))), "0.00"))

            mPayableSalary = CDbl(VB6.Format(mSalary * mWDays / MainClass.LastDay(Month(CDate(mSalDate)), Year(CDate(mSalDate))), "0"))
            mPayableSalary = mPayableSalary + CDbl(VB6.Format(mSalary * mLayoffDays * 0.5 / MainClass.LastDay(Month(CDate(mSalDate)), Year(CDate(mSalDate))), "0.00"))
            mPayableWelfareSalary = mPayableSalary

            mActPayableSalary = CDbl(VB6.Format(mActSalary * mWDays / MainClass.LastDay(Month(CDate(mSalDate)), Year(CDate(mSalDate))), "0"))
            mActualWelFareSalary = mActSalary

            Do While Not RsTemp.EOF
                If RsTemp.Fields("INCLUDEDPF").Value = "Y" Then
                    If mEmpContOn = "G" Or mEmpContOn = "E" Then
                        mActualPFSalary = mActualPFSalary + (IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value))

                        If mLayoffDays = 0 Then
                            mPayablePFSalary = mPayablePFSalary + (IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value) * mPayableSalary) / mSalary
                        Else
                            If RsTemp.Fields("Type").Value = ConDA Then
                                mPayablePFSalary = mPayablePFSalary + (IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value) * mPayableSalary) / mSalary
                            Else
                                mPayablePFSalary = mPayablePFSalary
                            End If
                        End If
                    End If
                End If
                If RsTemp.Fields("INCLUDEDESI").Value = "Y" Then
                    '                If mLayoffDays = 0 Then
                    mActualESISalary = mActualESISalary + (IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value))
                    '                Else
                    '                    mActualESISalary = mActualESISalary + Format(mSalary * mWDays / MainClass.LastDay(Month(mSalDate), Year(mSalDate)), "0.00") + Format(mSalary * mWDays / MainClass.LastDay(Month(mSalDate), Year(mSalDate)), "0.00")
                    '                End If
                    If mSalary = 0 Then
                        mPayableESISalary = 0

                    Else
                        If mSalary = 0 Then
                            mPayableESISalary = 0
                        Else
                            If mLayoffDays = 0 Then
                                mPayableESISalary = mPayableESISalary + (IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value) * mPayableSalary) / mSalary
                            Else
                                If RsTemp.Fields("Type").Value = ConDA Then
                                    mPayableESISalary = mPayableESISalary + ((IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value) * mPayableSalary) / mSalary)
                                Else
                                    mPayableESISalary = mPayableESISalary + ((IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value) * mPayableSalary) / mSalary) ''
                                End If
                            End If
                        End If


                    End If
                End If

                If (RsTemp.Fields("CALC_ON").Value = ConCalcBSalary Or RsTemp.Fields("CALC_ON").Value = ConCalcFixed) And RsTemp.Fields("ADDDEDUCT").Value = ConEarning Then

                    mActualWelFareSalary = mActualWelFareSalary + (IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value))
                    If mSalary = 0 Then
                        mPayableWelfareSalary = mPayableWelfareSalary
                    Else
                        mPayableWelfareSalary = mPayableWelfareSalary + ((IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value) * mPayableSalary) / mSalary)
                    End If

                End If

                RsTemp.MoveNext()
            Loop

            mOTAmount = GetOverTimeAmount(mCode, mSalDate)

            mActualWelFareSalary = mActualWelFareSalary + mOTAmount
            mPayableWelfareSalary = mPayableWelfareSalary + mOTAmount

            mActualWelFareSalary = mActualWelFareSalary + GetVariableAmount(mCode, mSalDate, "W")
            mPayableWelfareSalary = mPayableWelfareSalary + GetVariableAmount(mCode, mSalDate, "W")

            mPayableVariableESI = GetVariableAmount(mCode, mSalDate, "E")
            mPayableESISalary = mPayableESISalary + mPayableVariableESI

            mPayablePFSalary = mPayableSalary + mPayablePFSalary
            mPayablePFSalary = CDbl(VB6.Format(mPayablePFSalary, "0"))

            ''Deduct PF ON Ceiling
            'If mPayablePFSalary >= mPFCeiling Then
            '    mTempPFCeiling = CDbl(VB6.Format(mPFCeiling, "0.00"))
            'Else
            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
                If mPayablePFSalary >= mPFCeiling Then
                    mTempPFCeiling = CDbl(VB6.Format(mPFCeiling, "0"))
                Else
                    mTempPFCeiling = CDbl(VB6.Format(mPayablePFSalary, "0")) 'CDbl(VB6.Format(mTempPFCeiling * (mWDays + (mLayoffDays * 0.5)) / MainClass.LastDay(Month(CDate(mSalDate)), Year(CDate(mSalDate))), "0")) ''Format(mPFCeiling, "0.00")
                End If
            Else
                mTempPFCeiling = CDbl(VB6.Format(mPFCeiling * (mWDays + (mLayoffDays * 0.5)) / MainClass.LastDay(Month(CDate(mSalDate)), Year(CDate(mSalDate))), "0")) ''Format(mPFCeiling, "0.00")
            End If

            'End If

            mTempPFCeiling = System.Math.Round(mTempPFCeiling, 0)
            If mEmpContOn = "C" Or mEmpContOn = "E" Then
                mPayablePFSalary = IIf(mPayablePFSalary > mTempPFCeiling, mTempPFCeiling, mPayablePFSalary)
                'ElseIf mEmpContOn = "G" And RsCompany.Fields("ERP_CUSTOMER_ID").Value <> 110 Then
                '    mPayablePFSalary = IIf(mPayablePFSalary > mTempPFCeiling, mTempPFCeiling, mPayablePFSalary)
            End If

            mActualPFSalary = mSalary + mActualPFSalary
            mActualPFSalary = CDbl(VB6.Format(mActualPFSalary, "0"))

            If mEmpContOn = "C" Or mEmpContOn = "E" Then
                mActualPFSalary = IIf(mActualPFSalary > mTempPFCeiling, mTempPFCeiling, mActualPFSalary)
                'ElseIf mEmpContOn = "G" And RsCompany.Fields("ERP_CUSTOMER_ID").Value <> 110 Then
                '    mActualPFSalary = IIf(mActualPFSalary > mTempPFCeiling, mTempPFCeiling, mActualPFSalary)
            End If

            mPayablePensionWages = IIf(mTempPFCeiling <= mPayablePFSalary, mTempPFCeiling, mPayablePFSalary) ''05/12/2016  mPFCeiling

            ''06/04/2017
            If mSalary > mPFCeiling And mPayablePFSalary < mPFCeiling Then
                mPayablePensionWages = mPayablePFSalary
                mTempPFCeiling = mPayablePFSalary
            End If
            'End If

            If pAge > 58 Then
                mPayablePensionWages = 0
            Else
                mPayablePensionWages = CDbl(VB6.Format(mPayablePensionWages, "0"))
            End If

            mPayableESISalary = mPayableSalary + mPayableESISalary
            mPayableESISalary = CDbl(VB6.Format(mPayableESISalary, "0"))


            mActualESISalary = mActualESISalary + mSalary
            mActualESISalary = IIf(mESICeiling < mActualESISalary, 0, mActualESISalary)

            mPayableESISalary = IIf(mESICeiling < mActualESISalary, 0, mPayableESISalary)
            mPayableESISalary = CDbl(VB6.Format(mPayableESISalary, "0"))

            RsTemp.MoveFirst()
            Do While Not RsTemp.EOF
                mSalHeadCode = RsTemp.Fields("ADD_DEDUCTCODE").Value
                If RsTemp.Fields("Type").Value = ConPF Then
                    If pSalaryType = "A" Then
                        mAmount = 0     ''GetForm1Amount(ConPF, mCode, mSalDate)
                        mActualAmount = 0   ''mAmount
                    Else
                        If RsTemp.Fields("PERCENTAGE").Value = 0 Then
                            Dim xPer As Double
                            mActualAmount = RsTemp.Fields("Amount").Value
                            mActualAmount = Math.Round(mActualAmount, 0)
                            If mActualPFSalary = 0 Then
                                xPer = 0
                            Else
                                xPer = mActualAmount * 100 / mActualPFSalary  ''mPayablePFSalary
                            End If
                            mAmount = mPayablePFSalary * xPer / 100 ''RsTemp.Fields("Amount").Value
                            mAmount = Math.Round(mAmount, 0)
                            If mEmplerPFCont = "B" Then
                                mEmployer_PF = mPayablePFSalary * xPer / 100
                                mEmployer_PF = System.Math.Round(mEmployer_PF, 0)
                            Else
                                mEmployer_PF = IIf(mPayablePFSalary > mTempPFCeiling, mTempPFCeiling, mPayablePFSalary) * xPer / 100
                                mEmployer_PF = System.Math.Round(mEmployer_PF, 0)
                            End If

                        Else
                            '                    mAmount = mPayablePFSalary * RsTemp!PERCENTAGE / 100
                            '                    mActualAmount = mActualPFSalary * RsTemp!PERCENTAGE / 100

                            mAmount = mPayablePFSalary * mPFRate / 100
                            mActualAmount = mActualPFSalary * mPFRate / 100

                            mAmount = Math.Round(mAmount, 0)
                            mActualAmount = Math.Round(mActualAmount, 0)

                            'mEmployer_PF = Round(, 0)

                            If mEmplerPFCont = "B" Then
                                mEmployer_PF = mPayablePFSalary * mPFRate / 100
                                mEmployer_PF = System.Math.Round(mEmployer_PF, 0)
                            Else
                                mEmployer_PF = IIf(mPayablePFSalary > mTempPFCeiling, mTempPFCeiling, mPayablePFSalary) * mPFRate / 100
                                mEmployer_PF = System.Math.Round(mEmployer_PF, 0)
                            End If

                            If mPFRate <> RsTemp.Fields("PERCENTAGE").Value And RsTemp.Fields("PERCENTAGE").Value > 0 Then
                                mRounding = CDbl(Replace(RsTemp.Fields("ROUNDING").Value, "1", "0"))
                                mAmount = CDbl(VB6.Format(mAmount, CStr(mRounding))) * RsTemp.Fields("PERCENTAGE").Value / mPFRate
                                mActualAmount = CDbl(VB6.Format(mActualAmount, CStr(mRounding))) * RsTemp.Fields("PERCENTAGE").Value / mPFRate
                            End If
                        End If
                    End If

                    mPFAmt = mAmount
                    mNewPFRate = RsTemp.Fields("PERCENTAGE").Value
                    If mActualPFSalary <= mPFCeiling Then
                        If pAge > 58 Then
                            mPensionFund = 0
                        Else
                            mPensionFund = mPayablePFSalary * mPFPensionRate / 100
                        End If
                        mRounding = CDbl(Replace(RsTemp.Fields("ROUNDING").Value, "1", "0"))
                        mPensionFund = CDbl(VB6.Format(mPensionFund, CStr(mRounding)))
                        mEmpCont = mEmployer_PF - mPensionFund
                    Else
                        If pAge > 58 Then
                            mPensionFund = 0
                        Else
                            mPensionFund = mTempPFCeiling * mPFPensionRate / 100
                        End If
                        mRounding = CDbl(Replace(RsTemp.Fields("ROUNDING").Value, "1", "0"))
                        mPensionFund = CDbl(VB6.Format(mPensionFund, CStr(mRounding)))
                        mEmpCont = mEmployer_PF - mPensionFund
                    End If
                    mRounding = RsTemp.Fields("ROUNDING").Value
                    mPFRounding = RsTemp.Fields("ROUNDING").Value
                ElseIf RsTemp.Fields("Type").Value = ConVPFAllw Then
                    mVPFRate = IIf(IsDBNull(RsTemp.Fields("PERCENTAGE").Value), 0, RsTemp.Fields("PERCENTAGE").Value)
                    If mVPFRate = 0 Then
                        mAmount = RsTemp.Fields("Amount").Value
                        mActualAmount = RsTemp.Fields("Amount").Value
                    Else
                        If mVPFRate <= 12 Then
                            mAmount = mPayablePFSalary * mVPFRate / 100
                            mActualAmount = mActualPFSalary * mVPFRate / 100
                        Else
                            mAmount = mPayablePFSalary * 12 / 100
                            mActualAmount = mActualPFSalary * 12 / 100

                            mRounding = RsTemp.Fields("ROUNDING").Value
                            If mRounding = CDbl("0.05") Then
                                mAmount = PaiseRound(mAmount, 0.05)
                                mActualAmount = PaiseRound(mActualAmount, 0.05)
                            ElseIf mRounding = CDbl("10") Then
                                mAmount = Int(mAmount) + IIf(mAmount > Int(mAmount), 1, 0)
                                mActualAmount = Int(mActualAmount) + IIf(mActualAmount > Int(mActualAmount), 1, 0)
                            Else
                                mRound = Replace(RsTemp.Fields("ROUNDING").Value, "1", "0")
                                mAmount = CDbl(VB6.Format(mAmount, mRound))
                                mActualAmount = CDbl(VB6.Format(mActualAmount, mRound))
                            End If

                            mAmount = mAmount + (mPayablePFSalary * (mVPFRate - 12) / 100)
                            mActualAmount = mActualAmount + (mActualPFSalary * (mVPFRate - 12) / 100)

                            mAmount = Math.Round(mAmount, 0)
                            mActualAmount = Math.Round(mActualAmount, 0)
                        End If

                    End If
                    mVPFAmount = mAmount
                    mRounding = RsTemp.Fields("ROUNDING").Value
                    mPFRounding = RsTemp.Fields("ROUNDING").Value
                ElseIf RsTemp.Fields("Type").Value = ConESI Then
                    If pSalaryType = "A" Then
                        mAmount = 0     'GetForm1Amount(ConESI, mCode, mSalDate)
                        mActualAmount = 0        ' mAmount
                    Else
                        If RsTemp.Fields("PERCENTAGE").Value = 0 Then
                            mAmount = RsTemp.Fields("Amount").Value
                            mActualAmount = RsTemp.Fields("Amount").Value
                        Else
                            mAmount = CDbl(VB6.Format(mPayableESISalary * mESIRate / 100, "0.00"))
                            mActualAmount = mActualESISalary * mESIRate / 100
                        End If


                        ''Sandeep
                        'If mAmount > Int(mAmount) Then
                        '    mAmount = Int(mAmount) + 1
                        'Else
                        '    mAmount = System.Math.Round(mAmount, 0)
                        'End If

                    End If
                    mESIAmt = mAmount
                    mRounding = IIf(CDate(mSalDate) > CDate("01/12/2004"), "10", RsTemp.Fields("ROUNDING").Value)
                    mESIRounding = IIf(CDate(mSalDate) > CDate("01/12/2004"), "10", RsTemp.Fields("ROUNDING").Value)
                ElseIf RsTemp.Fields("Type").Value = ConWelfare Then
                    If pSalaryType = "A" Then
                        mAmount = 0     ''GetForm1Amount(ConPF, mCode, mSalDate)
                        mActualAmount = 0   ''mAmount
                    Else
                        mAmount = GetWelfareAmount(mSalDate, mPayableWelfareSalary)
                        mActualAmount = GetWelfareAmount(mSalDate, mActualWelFareSalary)
                    End If
                    mAmount = Math.Round(mAmount, 0)
                    mActualAmount = Math.Round(mActualAmount, 0)
                Else
                    'If pSalaryType = "A" Then
                    '    mAmount = 0     ''GetForm1Amount(ConPF, mCode, mSalDate)
                    '    mActualAmount = 0   ''mAmount
                    'Else
                    If RsTemp.Fields("CALC_ON").Value = ConCalcBSalary Then
                        If RsTemp.Fields("PERCENTAGE").Value = 0 Then
                            If mSalary = 0 Then
                                mAmount = 0
                                mActualAmount = RsTemp.Fields("Amount").Value
                            Else
                                If mLayoffDays = 0 Then
                                    mAmount = (RsTemp.Fields("Amount").Value * mPayableSalary) / mSalary
                                Else
                                    If RsTemp.Fields("Type").Value = ConDA Then
                                        mAmount = (RsTemp.Fields("Amount").Value * mPayableSalary) / mSalary
                                    Else
                                        mAmount = (RsTemp.Fields("Amount").Value * mPayableSalary) / mSalary
                                    End If
                                End If

                                mActualAmount = RsTemp.Fields("Amount").Value
                            End If
                        Else
                            If mLayoffDays = 0 Then
                                mAmount = mPayableSalary * RsTemp.Fields("PERCENTAGE").Value / 100
                            Else
                                If RsTemp.Fields("Type").Value = ConDA Then
                                    mAmount = mPayableSalary * RsTemp.Fields("PERCENTAGE").Value / 100
                                Else
                                    mAmount = mWOLayoffPayableSalary * RsTemp.Fields("PERCENTAGE").Value / 100
                                End If
                            End If

                            mActualAmount = mSalary * RsTemp.Fields("PERCENTAGE").Value / 100
                        End If

                    ElseIf RsTemp.Fields("CALC_ON").Value = ConCalcFixed Then
                        If mPayableSalary = 0 Then
                            mAmount = 0
                            mActualAmount = 0
                        Else
                            If mLayoffDays = 0 Then
                                mAmount = IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
                            Else
                                If RsTemp.Fields("Type").Value = ConDA Then
                                    mAmount = IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
                                Else
                                    mAmount = (RsTemp.Fields("Amount").Value * mPayableSalary) / mSalary
                                End If
                            End If
                            mActualAmount = (IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value))
                        End If
                    End If
                    'End If
                    mRounding = RsTemp.Fields("ROUNDING").Value
                End If

                If RsTemp.Fields("Type").Value = ConESI Then

                Else
                    If mRounding = CDbl("0.05") Then
                        mAmount = PaiseRound(mAmount, 0.05)
                        mActualAmount = PaiseRound(mActualAmount, 0.05)
                    ElseIf mRounding = CDbl("10") Then
                        mAmount = Int(mAmount) + IIf(mAmount > Int(mAmount), 1, 0)
                        mActualAmount = Int(mActualAmount) + IIf(mActualAmount > Int(mActualAmount), 1, 0)
                    Else
                        'mRound = Replace(RsTemp.Fields("ROUNDING").Value, "1", "0")
                        'mAmount = CDbl(VB6.Format(mAmount, mRound))
                        'mActualAmount = CDbl(VB6.Format(mActualAmount, mRound))

                        mAmount = Math.Round(mAmount, 0)
                        mActualAmount = Math.Round(mActualAmount, 0)
                    End If
                End If


                If pSalaryType = "A" Then
                    If RsTemp.Fields("Type").Value = ConPF Or RsTemp.Fields("Type").Value = ConESI Or RsTemp.Fields("Type").Value = ConWelfare Then
                        GoTo NextRowItem
                    End If
                End If
                SqlStr = " INSERT INTO " & mTable & " (" & vbCrLf _
                    & " COMPANY_CODE, FYEAR, EMP_CODE, " & vbCrLf _
                    & " SAL_DATE, BASICSALARY, PAYABLESALARY, " & vbCrLf _
                    & " WDAYS, SALHEADCODE , PAYABLEAMOUNT, " & vbCrLf _
                    & " ACTUALAMOUNT, DEPARTMENT, " & vbCrLf _
                    & " CATEGORY, PAYMENTMODE, BANKACCTNO,ISARREAR,DESG_DESC,DIV_CODE,BANKIFSCCODE, IS_PAID " & vbCrLf _
                    & " ) VALUES ( " & vbCrLf _
                    & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & mCurrentFYNo & ", " & vbCrLf _
                    & " '" & mCode & "',TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " " & mSalary & ", " & mPayableSalary & ", " & vbCrLf _
                    & " " & mWDays & ", " & mSalHeadCode & ", " & mAmount & ", " & vbCrLf _
                    & " " & mActualAmount & ", '" & mDepartment & "', " & vbCrLf _
                    & " '" & mCategory & "', '" & mPaymentMode & "',  " & vbCrLf _
                    & " '" & mBankAcctNo & "','" & mArrearCalc & "','" & mDesignation & "'," & mDivisionCode & ",'" & mBankIFSCCode & "','N' " & vbCrLf _
                    & " )"

                PubDBCn.Execute(SqlStr)

                If pSalaryType = "F" Then
                    If RsTemp.Fields("Type").Value = ConPF Or RsTemp.Fields("Type").Value = ConESI Or RsTemp.Fields("Type").Value = ConWelfare Then
                        SqlStr = " INSERT INTO " & mActualTable & " (" & vbCrLf _
                                & " COMPANY_CODE, FYEAR, EMP_CODE, " & vbCrLf _
                                & " SAL_DATE, BASICSALARY, PAYABLESALARY, " & vbCrLf _
                                & " WDAYS, SALHEADCODE , PAYABLEAMOUNT, " & vbCrLf _
                                & " ACTUALAMOUNT, DEPARTMENT, " & vbCrLf _
                                & " CATEGORY, PAYMENTMODE, BANKACCTNO,ISARREAR,DESG_DESC,DIV_CODE,BANKIFSCCODE, IS_PAID " & vbCrLf _
                                & " ) VALUES ( " & vbCrLf _
                                & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & mCurrentFYNo & ", " & vbCrLf _
                                & " '" & mCode & "',TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                                & " " & mActSalary & ", " & mActPayableSalary & ", " & vbCrLf _
                                & " " & mWDays & ", " & mSalHeadCode & ", " & mAmount & ", " & vbCrLf _
                                & " " & mActualAmount & ", '" & mDepartment & "', " & vbCrLf _
                                & " '" & mCategory & "', '" & mPaymentMode & "',  " & vbCrLf _
                                & " '" & mBankAcctNo & "','" & mArrearCalc & "','" & mDesignation & "'," & mDivisionCode & ",'" & mBankIFSCCode & "','N' " & vbCrLf _
                                & " )"

                        PubDBCn.Execute(SqlStr)
                    End If
                End If
NextRowItem:
                RsTemp.MoveNext()
            Loop


            ''CALC VARIABLES........
            Dim mHeadType As Long

            SqlStr = "SELECT A.ADD_DEDUCTCODE, A.PERCENTAGE, SUM(A.AMOUNT) As AMOUNT1 FROM PAY_MONTHLY_TRN A WHERE " & vbCrLf _
                & " A.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND A.PAYYEAR=" & Year(CDate(lblNewDate.Text)) & " " & vbCrLf _
                & " AND A.EMP_CODE= '" & mCode & "'" & vbCrLf _
                & " AND TO_CHAR(A.Sal_MONTH,'MON-YYYY')=TO_CHAR('" & UCase(VB6.Format(mSalDate, "MMM-YYYY")) & "')" & vbCrLf _
                & " AND A.SAL_FLAG='S' GROUP BY A.ADD_DEDUCTCODE, A.PERCENTAGE"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsVar, ADODB.LockTypeEnum.adLockOptimistic)

            If RsVar.EOF = False Then
                Do While Not RsVar.EOF
                    mSalHeadCode = IIf(IsDBNull(RsVar.Fields("ADD_DEDUCTCODE").Value), -1, RsVar.Fields("ADD_DEDUCTCODE").Value)


                    If MainClass.ValidateWithMasterTable(mSalHeadCode, "CODE", "TYPE", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mHeadType = Val(MasterNo)
                    Else
                        mHeadType = -1
                    End If

                    If mHeadType = ConWelfare Then
                        If pSalaryType = "A" Then
                            mAmount = 0     ''GetForm1Amount(ConPF, mCode, mSalDate)
                            mActualAmount = 0   ''mAmount
                        Else
                            mAmount = GetWelfareAmount(mSalDate, mPayableWelfareSalary)
                            mActualAmount = GetWelfareAmount(mSalDate, mActualWelFareSalary)
                        End If
                        mAmount = Math.Round(mAmount, 0)
                        mActualAmount = Math.Round(mActualAmount, 0)
                    Else
                        mAmount = IIf(IsDBNull(RsVar.Fields("AMOUNT1").Value), 0, RsVar.Fields("AMOUNT1").Value)
                        mActualAmount = mAmount
                    End If

                    'If pSalaryType = "F" Then
                    '    mAmount = mAmount - GetAttnAwardAmount_Adj(mSalHeadCode, mCode, mSalDate, "S")
                    'End If

                    If mRounding = CDbl("0.05") Then
                        mAmount = PaiseRound(mAmount, 0.05)
                        mActualAmount = PaiseRound(mActualAmount, 0.05)
                    ElseIf mRounding = CDbl("10") Then
                        mAmount = Int(mAmount) + IIf(mAmount > Int(mAmount), 1, 0)
                        mActualAmount = Int(mAmount) + IIf(mActualAmount > Int(mActualAmount), 1, 0)
                    Else
                        mRound = Replace(CStr(mRounding), "1", "0")
                        mAmount = CDbl(VB6.Format(mAmount, mRound))
                        mActualAmount = CDbl(VB6.Format(mActualAmount, mRound))
                    End If

                    If mSalHeadCode <> -1 Then
                        SqlStr = " INSERT INTO " & mTable & " (" & vbCrLf _
                        & " COMPANY_CODE, FYEAR, EMP_CODE, " & vbCrLf _
                        & " SAL_DATE, BASICSALARY, PAYABLESALARY, " & vbCrLf _
                        & " WDAYS, SALHEADCODE , PAYABLEAMOUNT, " & vbCrLf _
                        & " ACTUALAMOUNT, DEPARTMENT, " & vbCrLf _
                        & " CATEGORY, PAYMENTMODE, BANKACCTNO,ISARREAR,DESG_DESC,DIV_CODE,BANKIFSCCODE, IS_PAID) VALUES ( " & vbCrLf _
                        & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & mCurrentFYNo & ", " & vbCrLf & " '" & mCode & "',TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                        & " " & mSalary & ", " & mPayableSalary & ", " & vbCrLf _
                        & " " & mWDays & ", " & mSalHeadCode & ", " & mAmount & ", " & vbCrLf _
                        & " " & mActualAmount & ", '" & mDepartment & "', " & vbCrLf _
                        & " '" & mCategory & "', '" & mPaymentMode & "',  " & vbCrLf _
                        & " '" & mBankAcctNo & "','" & mArrearCalc & "','" & mDesignation & "'," & mDivisionCode & ",'" & mBankIFSCCode & "','N')"

                        PubDBCn.Execute(SqlStr)


                    End If
                    RsVar.MoveNext()
                Loop
            End If


            'PF ESI Calc....
            If lblEmpType.Text = "D" Then GoTo NextDummyData

            mRound = Replace(CStr(mPFRounding), "1", "0")

            mPFAmt = IIf(mPFAmt = 0, 0, System.Math.Round(mPFAmt, 0))
            mVPFAmount = IIf(mVPFAmount = 0, 0, System.Math.Round(mVPFAmount, 0))

            If pAge > 58 Then
                mPensionFund = 0
                mPayablePensionWages = 0
            Else
                mPensionFund = IIf(mPensionFund = 0, 0, System.Math.Round(mPensionFund, 0))
            End If
            mEmpCont = IIf(mEmpCont = 0, 0, System.Math.Round(mEmpCont, 0))
            mPayablePensionWages = IIf(mPayablePensionWages = 0, 0, System.Math.Round(mPayablePensionWages, 0))



            mRound = CStr(mESIRounding)
            If mRound = "0.05" Then
                mESIAmt = PaiseRound(mESIAmt, 0.05)
            ElseIf mRound = "10" Then
                mESIAmt = Int(mESIAmt) + IIf(mESIAmt > Int(mESIAmt), 1, 0)
            Else
                mRound = Replace(CStr(mESIRounding), "1", "0")
                mESIAmt = IIf(mESIAmt = 0, 0, VB6.Format(mESIAmt, mRound))
            End If

            If pSalaryType = "F" Then

                SqlStr = " INSERT INTO PAY_PFESI_TRN ( " & vbCrLf _
                    & " COMPANY_CODE, FYEAR, EMP_CODE, " & vbCrLf _
                    & " SAL_DATE, BasicSalary, PFABLEAMT, PENSIONWAGES, PFAMT, PFRate ,  " & vbCrLf _
                    & " ESIABLEAMT , ESIAMT, ESIRATE, PENSIONFUND, EPFAMT ,  " & vbCrLf _
                    & " LEAVEWOP , WDAYS, ISARREAR, VPFAMT, VPFRATE ) VALUES ( " & vbCrLf _
                    & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & mCurrentFYNo & ", " & vbCrLf _
                    & " '" & mCode & "',TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " " & mSalary & ", " & mPayablePFSalary & "," & mPayablePensionWages & "," & mPFAmt & "," & mNewPFRate & ", " & vbCrLf _
                    & " " & mPayableESISalary & "," & mESIAmt & "," & mESIRate & ", " & vbCrLf _
                    & " " & mPensionFund & ", " & mEmpCont & "," & mTotalWop_Absent & "," & vbCrLf _
                    & " " & mWDays & ", " & vbCrLf _
                    & " '" & mArrearCalc & "', " & mVPFAmount & ", " & mVPFRate & ") "

                PubDBCn.Execute(SqlStr)
            End If
        End If
NextRec:

        If pSalaryType = "F" Then
            mOPDate = GetOpeningPerksDate()
            If VB6.Format(mOPDate, "YYYYMM") <= VB6.Format(mSalDate, "YYYYMM") Then
                If UpdatePerksTrn(mCode, mSalDate, mWDays + IIf(RsCompany.Fields("COMPANY_CODE").Value = 16, mWOP, 0), mDivisionCode) = False Then GoTo UpDateSalTrnErr
                If UpdatePerksArrearTrn(mCode, mSalDate, mDivisionCode) = False Then GoTo UpDateSalTrnErr
            End If
        End If

NextDummyData:
        pDesignation = mDesignation
        UpdateSalTrn = True

        Exit Function
UpDateSalTrnErr:
        'Resume
        MsgBox(Err.Description)
        UpdateSalTrn = False
    End Function
    Private Function GetOverTimeAmount(ByRef mCode As String, ByRef mSalDate As String) As Double

        On Error GoTo UpDateSalTrnErr
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mTable As String = ""

        If lblEmpType.Text = "D" Then
            mTable = "PAY_MONTHLY_DUMMY_OT_TRN"
        Else
            mTable = "PAY_MONTHLY_OT_TRN"
        End If

        GetOverTimeAmount = 0

        SqlStr = "SELECT  SUM(OT_AMOUNT) As AMOUNT1 " & vbCrLf _
            & " FROM " & mTable & " TRN" & vbCrLf _
            & " WHERE " & vbCrLf _
            & " TRN.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " --AND TRN.PAYYEAR=" & Year(CDate(lblNewDate.Text)) & " " & vbCrLf _
            & " AND TRN.EMP_CODE= '" & mCode & "'" & vbCrLf _
            & " AND TO_CHAR(TRN.OT_DATE,'MON-YYYY')=TO_CHAR('" & UCase(VB6.Format(mSalDate, "MMM-YYYY")) & "')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)


        If RsTemp.EOF = False Then
            GetOverTimeAmount = IIf(IsDBNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value)
        End If

        Exit Function
UpDateSalTrnErr:
        'Resume
        MsgBox(Err.Description)
        GetOverTimeAmount = 0
    End Function

    Private Function GetVariableAmount(ByRef mCode As String, ByRef mSalDate As String, ByRef mType As String) As Double

        On Error GoTo UpDateSalTrnErr
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        SqlStr = "SELECT  SUM(TRN.AMOUNT) As AMOUNT1 " & vbCrLf _
            & " FROM PAY_MONTHLY_TRN TRN, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf _
            & " WHERE " & vbCrLf _
            & " TRN.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND TRN.PAYYEAR=" & Year(CDate(lblNewDate.Text)) & " " & vbCrLf _
            & " AND TRN.EMP_CODE= '" & mCode & "'" & vbCrLf _
            & " AND TO_CHAR(TRN.Sal_MONTH,'MON-YYYY')=TO_CHAR('" & UCase(VB6.Format(mSalDate, "MMM-YYYY")) & "')" & vbCrLf _
            & " AND TRN.COMPANY_CODE=ADD_DEDUCT.COMPANY_CODE" & vbCrLf & " AND TRN.ADD_DEDUCTCODE=ADD_DEDUCT.CODE" & vbCrLf _
            & " AND ADDDEDUCT =" & ConEarning & "" & vbCrLf _
            & " AND ADD_DEDUCT.CALC_ON=" & ConCalcVariable & "" & vbCrLf _
            & " AND SAL_FLAG='S' "

        If mType = "E" Then
            SqlStr = SqlStr & vbCrLf & " AND INCLUDEDESI='Y'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)


        If RsTemp.EOF = False Then
            GetVariableAmount = IIf(IsDBNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value)
        End If

        Exit Function
UpDateSalTrnErr:
        'Resume
        MsgBox(Err.Description)
        GetVariableAmount = 0
    End Function

    Private Function GetPFESIAmount(ByRef mCode As String, ByRef pHeadCode As Integer, ByRef pWEFDate As String) As Double

        On Error GoTo ErrGetLTAAmount
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        SqlStr = " SELECT SUM(AMOUNT) AS AMOUNT " & vbCrLf _
            & " FROM PAY_SALARYDEF_MST A, PAY_SALARYHEAD_MST B" & vbCrLf _
            & " WHERE A.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND A.COMPANY_CODE = B.COMPANY_CODE" & vbCrLf _
            & " AND A.ADD_DEDUCTCODE=B.CODE" & vbCrLf _
            & " AND A.EMP_CODE = '" & mCode & "'"


        SqlStr = SqlStr & vbCrLf & " AND B.TYPE=" & pHeadCode & ""

        SqlStr = SqlStr & vbCrLf & " AND SALARY_EFF_DATE=(SELECT MAX(SALARY_EFF_DATE) " & vbCrLf _
            & " FROM PAY_SALARYDEF_MST" & vbCrLf _
            & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE = '" & mCode & "'" & vbCrLf _
            & " AND SALARY_EFF_DATE<=TO_DATE('" & VB6.Format(pWEFDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            GetPFESIAmount = IIf(IsDBNull(RsTemp.Fields("Amount").Value), "", RsTemp.Fields("Amount").Value)
        Else
            GetPFESIAmount = 0
        End If

        GetPFESIAmount = System.Math.Round(GetPFESIAmount, 0)

        Exit Function
ErrGetLTAAmount:
        GetPFESIAmount = 0
    End Function

    Private Function UpdatePerksTrn(ByRef mCode As String, ByRef mSalDate As String, ByRef mWDays As Double, ByRef mDivisionCode As Double) As Boolean


        On Error GoTo UpDateSalTrnErr
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsVar As ADODB.Recordset

        Dim SqlStr As String = ""
        Dim mSalHeadCode As Integer
        Dim mAmount As Double

        SqlStr = " SELECT SALARYDEF.*,ADD_DEDUCT.CODE, ADD_DEDUCT.TYPE, ADD_DEDUCT.CALC_ON," & vbCrLf _
            & " ADD_DEDUCT.INCLUDEDPF,ADD_DEDUCT.INCLUDEDESI, " & vbCrLf _
            & " ADD_DEDUCT.ROUNDING AS ROUNDING,SALARYDEF.EMP_CONT " & vbCrLf _
            & " FROM PAY_SALARYDEF_MST SALARYDEF, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf _
            & " WHERE " & vbCrLf _
            & " SALARYDEF.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND ADD_DEDUCT.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SALARYDEF.COMPANY_CODE=ADD_DEDUCT.COMPANY_CODE" & vbCrLf _
            & " AND SALARYDEF.ADD_DEDUCTCODE=ADD_DEDUCT.CODE" & vbCrLf _
            & " AND SALARYDEF.EMP_CODE = '" & mCode & "'" & vbCrLf _
            & " AND ADD_DEDUCT.CALC_ON<>" & ConCalcVariable & "" & vbCrLf _
            & " AND SALARYDEF.SALARY_EFF_DATE=( SELECT MAX(SALARY_EFF_DATE)" & vbCrLf _
            & " FROM PAY_SALARYDEF_MST WHERE " & vbCrLf _
            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE = '" & mCode & "'" & vbCrLf _
            & " AND SALARY_APP_DATE <= TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

        SqlStr = SqlStr & vbCrLf & " AND ADD_DEDUCT.CODE IN (" & vbCrLf & " SELECT CODE FROM PAY_SALARYHEAD_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ADDDEDUCT IN (" & ConPerks & ") AND PAYMENT_TYPE='M'" & vbCrLf & " AND STATUS='O' AND (CLOSED_DATE IS NULL OR CLOSED_DATE<=TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))" & vbCrLf & " UNION " & vbCrLf & " SELECT CODE FROM PAY_SALARYHEAD_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ADDDEDUCT IN (" & ConPerks & ") AND PAYMENT_TYPE='M'" & vbCrLf & " AND STATUS='C' AND CLOSED_DATE>TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            Do While Not RsTemp.EOF
                mSalHeadCode = RsTemp.Fields("ADD_DEDUCTCODE").Value
                mAmount = RsTemp.Fields("Amount").Value
                mAmount = CDbl(VB6.Format(mAmount * mWDays / MainClass.LastDay(Month(CDate(mSalDate)), Year(CDate(mSalDate))), "0.00"))

                If mAmount <> 0 Then
                    SqlStr = " INSERT INTO PAY_PERKS_TRN ( " & vbCrLf & " COMPANY_CODE, SAL_DATE, " & vbCrLf & " EMP_CODE, ADD_DEDUCTCODE, AMOUNT,BOOKTYPE,DC,PAYMENT_TYPE,ADDUSER,ADDDATE,DIV_CODE) VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & mCode & "', " & mSalHeadCode & ", " & mAmount & ",'S'," & vbCrLf & " 'C', '','" & MainClass.AllowSingleQuote(PubUserID) & "', TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & mDivisionCode & ") "

                    PubDBCn.Execute(SqlStr)
                End If
                RsTemp.MoveNext()
            Loop
        End If
NextRec:
        UpdatePerksTrn = True

        Exit Function
UpDateSalTrnErr:
        'Resume
        MsgBox(Err.Description)
        UpdatePerksTrn = False
    End Function
    Private Function UpdatePerksArrearTrn(ByRef mCode As String, ByRef mSalDate As String, ByRef mDivisionCode As Double) As Boolean


        On Error GoTo UpDateSalTrnErr
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsVar As ADODB.Recordset

        Dim SqlStr As String = ""
        Dim mSalHeadCode As Integer
        Dim mAmount As Double
        Dim cntMonthFrom As String
        Dim cntMonthTo As String
        Dim mArrearMonth As Double
        Dim mAddDays As Double
        Dim mWDays As Double
        Dim mDiffAmount As Double
        Dim mTotWDays As Double
        Dim mEmpDOJ As String
        Dim mDOL As String
        Dim mLeaveWop As Double
        Dim mTotalMonth As Integer
        Dim xAddMonthFrom As String
        Dim mWOP As Double
        Dim mAbsent As Double

        SqlStr = " SELECT SALARYDEF.*,ADD_DEDUCT.CODE, ADD_DEDUCT.TYPE, ADD_DEDUCT.CALC_ON," & vbCrLf _
            & " ADD_DEDUCT.INCLUDEDPF,ADD_DEDUCT.INCLUDEDESI, " & vbCrLf _
            & " ADD_DEDUCT.ROUNDING AS ROUNDING,SALARYDEF.EMP_CONT, EMP.EMP_DOJ,EMP.EMP_LEAVE_DATE " & vbCrLf _
            & " FROM PAY_SALARYDEF_MST SALARYDEF, PAY_SALARYHEAD_MST ADD_DEDUCT, PAY_EMPLOYEE_MST EMP" & vbCrLf _
            & " WHERE " & vbCrLf _
            & " SALARYDEF.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND ADD_DEDUCT.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SALARYDEF.COMPANY_CODE=ADD_DEDUCT.COMPANY_CODE" & vbCrLf _
            & " AND SALARYDEF.ADD_DEDUCTCODE=ADD_DEDUCT.CODE AND SALARYDEF.COMPANY_CODE=EMP.COMPANY_CODE AND SALARYDEF.EMP_CODE=EMP.EMP_CODE" & vbCrLf _
            & " AND SALARYDEF.EMP_CODE = '" & mCode & "'" & vbCrLf _
            & " AND ADD_DEDUCT.CALC_ON<>" & ConCalcVariable & "" & vbCrLf _
            & " AND SALARYDEF.SALARY_EFF_DATE=( SELECT MAX(SALARY_EFF_DATE)" & vbCrLf _
            & " FROM PAY_SALARYDEF_MST WHERE " & vbCrLf _
            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE = '" & mCode & "'" & vbCrLf _
            & " AND SALARY_APP_DATE <= TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

        SqlStr = SqlStr & vbCrLf _
            & " AND IS_ARREAR='Y' AND TO_CHAR(ARREAR_DATE,'MON-YYYY') ='" & UCase(VB6.Format(mSalDate, "MMM-YYYY")) & "'"

        SqlStr = SqlStr & vbCrLf _
            & " AND ADD_DEDUCT.CODE IN (" & vbCrLf _
            & " SELECT CODE FROM PAY_SALARYHEAD_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND ADDDEDUCT IN (" & ConPerks & ") AND PAYMENT_TYPE='M'" & vbCrLf _
            & " AND STATUS='O' AND (CLOSED_DATE IS NULL OR CLOSED_DATE<=TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))" & vbCrLf _
            & " UNION " & vbCrLf _
            & " SELECT CODE FROM PAY_SALARYHEAD_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND ADDDEDUCT IN (" & ConPerks & ") AND PAYMENT_TYPE='M'" & vbCrLf _
            & " AND STATUS='C' AND CLOSED_DATE>TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)


        If RsTemp.EOF = False Then
            Do While Not RsTemp.EOF
                mSalHeadCode = RsTemp.Fields("ADD_DEDUCTCODE").Value
                mWDays = 0
                mAmount = 0
                xAddMonthFrom = ""
                mWOP = 0
                mAbsent = 0

                mDiffAmount = RsTemp.Fields("Amount").Value - RsTemp.Fields("PREVIOUS_AMOUNT").Value
                If mDiffAmount <> 0 Then
                    cntMonthFrom = CStr(CDate(VB6.Format(RsTemp.Fields("SALARY_EFF_DATE").Value, "DD/MM/YYYY")))
                    cntMonthTo = CStr(CDate(VB6.Format(RsTemp.Fields("SALARY_APP_DATE").Value, "DD/MM/YYYY")))
                    mArrearMonth = (IIf(IsDBNull(RsTemp.Fields("TOT_ARR_MONTH").Value), 0, RsTemp.Fields("TOT_ARR_MONTH").Value))
                    mAddDays = (IIf(IsDBNull(RsTemp.Fields("ADDDAYS_IN").Value), 0, RsTemp.Fields("ADDDAYS_IN").Value))

                    If Val(CStr(mAddDays)) > 0 Then
                        cntMonthFrom = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1 * mAddDays, CDate(cntMonthFrom)))
                        xAddMonthFrom = cntMonthFrom
                    End If
                    mEmpDOJ = VB6.Format(IIf(IsDBNull(RsTemp.Fields("EMP_DOJ").Value), "", RsTemp.Fields("EMP_DOJ").Value), "DD/MM/YYYY")
                    mDOL = VB6.Format(IIf(IsDBNull(RsTemp.Fields("EMP_LEAVE_DATE").Value), "", RsTemp.Fields("EMP_LEAVE_DATE").Value), "DD/MM/YYYY")

                    Do While CDate(cntMonthFrom) < CDate(cntMonthTo)
                        If xAddMonthFrom <> "" And xAddMonthFrom = cntMonthFrom Then
                            mWDays = CalcAttn(mCode, mEmpDOJ, mDOL, cntMonthFrom, mLeaveWop, xAddMonthFrom, mAddDays, mWOP, mAbsent)
                        Else
                            mWDays = CalcAttn(mCode, mEmpDOJ, mDOL, cntMonthFrom, mLeaveWop, , , mWOP, mAbsent)
                        End If
                        mTotWDays = mTotWDays + mWDays + IIf(RsCompany.Fields("COMPANY_CODE").Value = 16, mWOP, 0)
                        mAmount = mAmount + CDbl(VB6.Format(mDiffAmount * mWDays / MainClass.LastDay(Month(CDate(cntMonthFrom)), Year(CDate(cntMonthFrom))), "0.00"))

                        cntMonthFrom = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, CDate(cntMonthFrom)))
                        mTotalMonth = mTotalMonth + 1

                    Loop

                    If mAmount <> 0 Then
                        mAmount = System.Math.Round(mAmount, 0)
                        SqlStr = " INSERT INTO PAY_PERKS_TRN ( " & vbCrLf _
                            & " COMPANY_CODE, SAL_DATE, " & vbCrLf _
                            & " EMP_CODE, ADD_DEDUCTCODE, AMOUNT,BOOKTYPE,DC,PAYMENT_TYPE,ADDUSER,ADDDATE,DIV_CODE) VALUES ( " & vbCrLf _
                            & " " & RsCompany.Fields("COMPANY_CODE").Value & ", TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " '" & mCode & "', " & mSalHeadCode & ", " & mAmount & ",'A'," & vbCrLf _
                            & " 'C', '','" & MainClass.AllowSingleQuote(PubUserID) & "', TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & mDivisionCode & ") "

                        PubDBCn.Execute(SqlStr)
                    End If
                End If
                RsTemp.MoveNext()
            Loop
        End If
NextRec:
        UpdatePerksArrearTrn = True

        Exit Function
UpDateSalTrnErr:
        'Resume
        MsgBox(Err.Description)
        UpdatePerksArrearTrn = False
    End Function

    Private Function UpdateArrearSalTrn(ByRef mCode As String, ByRef mDepartment As String, ByRef mCategory As String, ByRef mPaymentMode As Integer, ByRef mBankAcctNo As String, ByRef mBankIFSCCode As String, ByRef mArrearCalc As String, ByRef pAddDays As Double, ByRef mEmpDOJ As String, ByRef mDOL As String, ByRef mSalDate As String, ByRef pAge As Double, ByRef mDivisionCode As Double) As Boolean


        On Error GoTo UpDateSalTrnErr
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsVar As ADODB.Recordset

        Dim SqlStr As String = ""
        Dim mSalary As Double
        Dim mBSalary As Double
        Dim mPayableSalary As Double
        Dim mWDays As Double
        Dim mSalHeadCode As Integer
        Dim mAmount As Double
        Dim mActualAmount As Double
        Dim mRound As String
        Dim mPayablePFSalary As Double
        Dim mPayableESISalary As Double
        Dim mActualPFSalary As Double
        Dim mActualESISalary As Double
        Dim mPayablePensionWages As Double
        Dim mRounding As Double
        Dim mPFAmt As Double
        Dim mNewPFRate As Double
        Dim mPensionFund As Double
        Dim mEmpCont As Double
        Dim mESIAmt As Double
        Dim mLeaveWop As Double
        Dim mPFRounding As Double
        Dim mESIRounding As Double
        Dim mTempPFCeiling As Double

        Dim mArrearMonth As Double
        Dim cntMonthFrom As String
        Dim cntMonthTo As String
        Dim mTotalDays As Double
        Dim mDesignation As String
        Dim mPrevPercent As Double
        Dim mAddDays As Double
        Dim mAddMonthDays As Double
        Dim xAttnDate As String
        Dim mEmployer_PF As Double
        Dim mEmpContOn As String

        Dim mVPFRate As Double
        Dim mVPFAmount As Double
        Dim mTotalMonth As Double
        Dim CntMonth As Integer
        Dim xAddDays As Integer
        Dim cntPrevMonth As String

        Dim mPrevPensionFund As Double
        Dim pPensionDiff As Double
        Dim mPensionFundConst As Double
        Dim mWOP As Double
        Dim mAbsent As Double

        SqlStr = " SELECT SALARYDEF.*,ADD_DEDUCT.CODE, ADD_DEDUCT.TYPE, ADD_DEDUCT.CALC_ON," & vbCrLf & " ADD_DEDUCT.INCLUDEDPF,ADD_DEDUCT.INCLUDEDESI, " & vbCrLf & " ADD_DEDUCT.ROUNDING AS ROUNDING,SALARYDEF.EMP_CONT " & vbCrLf & " FROM PAY_SALARYDEF_MST SALARYDEF, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf & " WHERE " & vbCrLf & " SALARYDEF.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ADD_DEDUCT.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SALARYDEF.COMPANY_CODE=ADD_DEDUCT.COMPANY_CODE" & vbCrLf & " AND SALARYDEF.ADD_DEDUCTCODE=ADD_DEDUCT.CODE" & vbCrLf & " AND SALARYDEF.EMP_CODE = '" & mCode & "'" & vbCrLf & " AND ADD_DEDUCT.CALC_ON<>" & ConCalcVariable & " " & vbCrLf & " AND SALARYDEF.SALARY_EFF_DATE=( SELECT MAX(SALARY_EFF_DATE)" & vbCrLf & " FROM PAY_SALARYDEF_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE = '" & mCode & "'" & vbCrLf & " AND SALARY_APP_DATE <= TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) " & vbCrLf & " AND IS_ARREAR='Y' AND TO_CHAR(ARREAR_DATE,'MON-YYYY') ='" & UCase(VB6.Format(mSalDate, "MMM-YYYY")) & "'"

        '    SqlStr = SqlStr & vbCrLf & ""
        ''AND ADD_DEDUCT.TYPE NOT IN (" & ConAdvance & ", " & ConIncomeTax & ", " & ConLoan & ", " & ConOthers & ", " & ConImprest & ", " & ConOT & ", " & ConTDS & ", " & ConLIC & ")

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)


        If RsTemp.EOF = False Then
            If IsDBNull(RsTemp.Fields("BASICSALARY").Value) Then GoTo NextRec
            mArrearMonth = (IIf(IsDBNull(RsTemp.Fields("TOT_ARR_MONTH").Value), 0, RsTemp.Fields("TOT_ARR_MONTH").Value))
            mAddDays = (IIf(IsDBNull(RsTemp.Fields("ADDDAYS_IN").Value), 0, RsTemp.Fields("ADDDAYS_IN").Value))
            mEmpContOn = IIf(IsDBNull(RsTemp.Fields("EMP_CONT").Value), "B", RsTemp.Fields("EMP_CONT").Value)

            mSalary = RsTemp.Fields("BASICSALARY").Value - IIf(IsDBNull(RsTemp.Fields("PREVIOUS_BASICSALARY").Value), 0, RsTemp.Fields("PREVIOUS_BASICSALARY").Value)
            mSalary = mSalary
            mBSalary = RsTemp.Fields("BASICSALARY").Value


            cntMonthFrom = CStr(CDate(VB6.Format(RsTemp.Fields("SALARY_EFF_DATE").Value, "DD/MM/YYYY")))
            cntMonthTo = CStr(CDate(VB6.Format(RsTemp.Fields("SALARY_APP_DATE").Value, "DD/MM/YYYY")))

            If MainClass.ValidateWithMasterTable(Trim(RsTemp.Fields("EMP_DESG_CODE").Value), "DESG_CODE", "DESG_DESC", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDesignation = MasterNo
            Else
                mDesignation = "-1"
            End If

            mLeaveWop = 0
            mWOP = 0
            mAbsent = 0

            '        mLeaveWop = MainClass.LastDay(Month(lblNewDate.Caption), Year(lblNewDate.Caption)) - mWDays
            '        mPayableSalary = Format(mSalary * mWDays / MainClass.LastDay(Month(mSalDate), Year(mSalDate)), "0.00")

            mTotalMonth = 0
            Do While CDate(cntMonthFrom) < CDate(cntMonthTo)
                mWDays = mWDays + CalcAttn(mCode, mEmpDOJ, mDOL, cntMonthFrom, mLeaveWop, , , mWOP, mAbsent)
                mTotalDays = mTotalDays + MainClass.LastDay(Month(CDate(cntMonthFrom)), Year(CDate(cntMonthFrom)))
                cntMonthFrom = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, CDate(cntMonthFrom)))
                mTotalMonth = mTotalMonth + 1
            Loop

            If Val(CStr(mAddDays)) > 0 Then
                cntMonthFrom = VB6.Format(RsTemp.Fields("SALARY_EFF_DATE").Value, "DD/MM/YYYY")          ''CStr(CDate(VB6.Format(RsTemp.Fields("SALARY_EFF_DATE").Value - 1), "DD/MM/YYYY"))
                cntMonthFrom = DateAdd("d", -1, cntMonthFrom)

                xAddDays = Val(CStr(mAddDays))
                xAttnDate = VB6.Format(RsTemp.Fields("SALARY_EFF_DATE").Value, "DD/MM/YYYY")
                CntMonth = 0
                Do While xAddDays > 0
                    xAttnDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -1, CDate(xAttnDate)))
                    If xAddDays >= MainClass.LastDay(Month(CDate(xAttnDate)), Year(CDate(xAttnDate))) Then
                        xAddDays = xAddDays - MainClass.LastDay(Month(CDate(xAttnDate)), Year(CDate(xAttnDate)))
                        CntMonth = CntMonth + 1
                    Else
                        xAttnDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, MainClass.LastDay(Month(CDate(xAttnDate)), Year(CDate(xAttnDate))) - xAddDays, CDate(xAttnDate)))
                        Exit Do
                    End If
                Loop

                '            xAttnDate = Format(CVDate(cntMonthFrom) - (mAddDays - 1), "DD/MM/YYYY")       ''+ 1
                mWDays = mWDays + CalcAttn(mCode, mEmpDOJ, mDOL, cntMonthFrom, mLeaveWop, xAttnDate, mAddDays, mWOP, mAbsent)
                mTotalDays = mTotalDays + mAddDays
                mAddMonthDays = MainClass.LastDay(Month(CDate(xAttnDate)), Year(CDate(xAttnDate)))
                mAddMonthDays = xAddDays / mAddMonthDays
                mArrearMonth = mArrearMonth + CntMonth + mAddMonthDays
                mTotalMonth = mTotalMonth + CntMonth
            End If

            If mTotalDays = 0 Then
                mArrearMonth = 0 '(mWDays + IIf(RsCompany.Fields("COMPANY_CODE").Value = 16, mWOP, 0))
            Else
                mArrearMonth = mArrearMonth * mWDays / mTotalDays '(mWDays + IIf(RsCompany.Fields("COMPANY_CODE").Value = 16, mWOP, 0))
            End If

            mPayableSalary = mArrearMonth * mSalary

            '        mPayableSalary = mArrearMonth * mSalary * mWDays / mTotalDays


            Do While Not RsTemp.EOF
                If RsTemp.Fields("INCLUDEDPF").Value = "Y" Then
                    mActualPFSalary = mActualPFSalary + (IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value) - IIf(IsDBNull(RsTemp.Fields("PREVIOUS_AMOUNT").Value), 0, RsTemp.Fields("PREVIOUS_AMOUNT").Value))
                    mPayablePFSalary = mPayablePFSalary + ((IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value) - IIf(IsDBNull(RsTemp.Fields("PREVIOUS_AMOUNT").Value), 0, RsTemp.Fields("PREVIOUS_AMOUNT").Value)) * mPayableSalary) / IIf(mSalary = 0, 1, mSalary)
                End If
                If CDate(mSalDate) >= CDate("01/11/2011") Then
                    mActualESISalary = 0
                    mPayableESISalary = 0
                Else
                    If RsTemp.Fields("INCLUDEDESI").Value = "Y" Then
                        mActualESISalary = mActualESISalary + (IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value) - IIf(IsDBNull(RsTemp.Fields("PREVIOUS_AMOUNT").Value), 0, RsTemp.Fields("PREVIOUS_AMOUNT").Value))
                        ''11.06.2007
                        '                If mSalary = 0 Then
                        '                    mPayableESISalary = 0
                        '                Else
                        If mPayableSalary = 0 Then
                            mPayableESISalary = mPayableESISalary + (mArrearMonth * (IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value) - IIf(IsDBNull(RsTemp.Fields("PREVIOUS_AMOUNT").Value), 0, RsTemp.Fields("PREVIOUS_AMOUNT").Value)) * mWDays / mTotalDays)
                        Else
                            mPayableESISalary = mPayableESISalary + ((IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value) - IIf(IsDBNull(RsTemp.Fields("PREVIOUS_AMOUNT").Value), 0, RsTemp.Fields("PREVIOUS_AMOUNT").Value)) * mPayableSalary) / IIf(mSalary = 0, 1, mSalary)
                        End If
                        '                End If
                    End If
                End If
                RsTemp.MoveNext()
            Loop
            RsTemp.MoveFirst()
            mPayablePFSalary = mPayableSalary + mPayablePFSalary
            mPayablePFSalary = Val(VB6.Format(mPayablePFSalary, "0"))

            mActualPFSalary = mSalary + mActualPFSalary
            mActualPFSalary = Val(VB6.Format(mActualPFSalary, "0"))

            If mEmpContOn = "C" Or mEmpContOn = "E" Then
                mPayablePFSalary = IIf(mPayablePFSalary > mPFCeiling, mPFCeiling, mPayablePFSalary)
                mActualPFSalary = IIf(mActualPFSalary > mPFCeiling, mPFCeiling, mActualPFSalary)
            End If

            If mPFCeiling <= RsTemp.Fields("PREVIOUS_BASICSALARY").Value Then
                mTempPFCeiling = 0
                mPayablePensionWages = 0
            Else
                If mPFCeiling <= RsTemp.Fields("BASICSALARY").Value Then
                    mTempPFCeiling = CDbl(VB6.Format((mPFCeiling - RsTemp.Fields("PREVIOUS_BASICSALARY").Value) * mWDays / MainClass.LastDay(Month(CDate(mSalDate)), Year(CDate(mSalDate))), "0.00"))
                    mPayablePensionWages = IIf(mPFCeiling <= RsTemp.Fields("BASICSALARY").Value, mTempPFCeiling, mPayablePFSalary)
                Else
                    mTempPFCeiling = Val(VB6.Format(mPFCeiling * mWDays / MainClass.LastDay(Month(CDate(mSalDate)), Year(CDate(mSalDate))), "0.00"))
                    mPayablePensionWages = IIf(mPFCeiling <= mActualPFSalary, mTempPFCeiling, mPayablePFSalary)
                End If
            End If

            '        mPayablePensionWages = IIf(mPFCeiling <= mActualPFSalary, mTempPFCeiling, mPayablePFSalary)
            If pAge > 58 Then
                mPayablePensionWages = 0
            Else
                mPayablePensionWages = Val(VB6.Format(mPayablePensionWages, "0"))
            End If

            If CDate(mSalDate) >= CDate("01/11/2011") Then
                mPayableESISalary = 0
                mActualESISalary = 0
            Else
                mPayableESISalary = mPayableSalary + mPayableESISalary
                mPayableESISalary = Val(VB6.Format(mPayableESISalary, "0"))


                mActualESISalary = mActualESISalary + mSalary
                mActualESISalary = IIf(mESICeiling < mActualESISalary, 0, mActualESISalary)

                mPayableESISalary = IIf(mESICeiling < mActualESISalary, 0, mPayableESISalary)
                mPayableESISalary = Val(VB6.Format(mPayableESISalary, "0"))
            End If

            Do While Not RsTemp.EOF
                mSalHeadCode = RsTemp.Fields("ADD_DEDUCTCODE").Value
                If RsTemp.Fields("Type").Value = ConPF Then
                    If RsTemp.Fields("PERCENTAGE").Value = 0 Then
                        mAmount = mArrearMonth * (RsTemp.Fields("Amount").Value - IIf(IsDBNull(RsTemp.Fields("PREVIOUS_AMOUNT").Value), 0, RsTemp.Fields("PREVIOUS_AMOUNT").Value))
                        mActualAmount = mArrearMonth * (RsTemp.Fields("Amount").Value - IIf(IsDBNull(RsTemp.Fields("PREVIOUS_AMOUNT").Value), 0, RsTemp.Fields("PREVIOUS_AMOUNT").Value))
                    Else
                        '                    mAmount = mPayablePFSalary * RsTemp!PERCENTAGE / 100
                        '                    mActualAmount = mActualPFSalary * RsTemp!PERCENTAGE / 100
                        mAmount = mPayablePFSalary * mPFRate / 100
                        mActualAmount = mActualPFSalary * mPFRate / 100

                        If mEmplerPFCont = "B" Then
                            mEmployer_PF = mPayablePFSalary * mPFRate / 100
                            mEmployer_PF = System.Math.Round(mEmployer_PF, 0)
                        Else
                            mEmployer_PF = IIf(mPayablePFSalary > mPFCeiling, mPFCeiling, mPayablePFSalary) * mPFRate / 100
                            mEmployer_PF = System.Math.Round(mEmployer_PF, 0)
                        End If

                        If mPFRate <> RsTemp.Fields("PERCENTAGE").Value Then
                            mRounding = Val(Replace(RsTemp.Fields("ROUNDING").Value, "1", "0"))
                            mAmount = Val(VB6.Format(mAmount, CStr(mRounding))) * RsTemp.Fields("PERCENTAGE").Value / mPFRate
                            mActualAmount = Val(VB6.Format(mActualAmount, CStr(mRounding))) * RsTemp.Fields("PERCENTAGE").Value / mPFRate
                        End If
                    End If
                    mPFAmt = mAmount
                    mNewPFRate = RsTemp.Fields("PERCENTAGE").Value
                    If RsTemp.Fields("PREVIOUS_BASICSALARY").Value <= mPFCeiling Then
                        '                If mActualPFSalary <= mPFCeiling Then
                        If pAge > 58 Then
                            mPensionFund = 0
                        Else
                            mPensionFund = mPayablePFSalary * mPFPensionRate / 100
                        End If
                        mRounding = Val(Replace(RsTemp.Fields("ROUNDING").Value, "1", "0"))
                        mPensionFund = Val(VB6.Format(mPensionFund, CStr(mRounding)))
                        mEmpCont = mEmployer_PF - mPensionFund 'mPFAmt
                    Else
                        If pAge > 58 Then
                            mPensionFund = 0
                        Else
                            mPensionFund = mTempPFCeiling * mPFPensionRate / 100
                        End If
                        mRounding = Val(Replace(RsTemp.Fields("ROUNDING").Value, "1", "0"))
                        mPensionFund = Val(VB6.Format(mPensionFund, CStr(mRounding)))
                        mEmpCont = mEmployer_PF - mPensionFund ''mPFAmt
                    End If

                    mRounding = RsTemp.Fields("ROUNDING").Value
                    mPFRounding = RsTemp.Fields("ROUNDING").Value
                ElseIf RsTemp.Fields("Type").Value = ConVPFAllw Then
                    mVPFRate = IIf(IsDBNull(RsTemp.Fields("PERCENTAGE").Value), 0, RsTemp.Fields("PERCENTAGE").Value)
                    If mVPFRate = 0 Then
                        mAmount = mArrearMonth * (RsTemp.Fields("Amount").Value - IIf(IsDBNull(RsTemp.Fields("PREVIOUS_AMOUNT").Value), 0, RsTemp.Fields("PREVIOUS_AMOUNT").Value))
                        mActualAmount = mArrearMonth * (RsTemp.Fields("Amount").Value - IIf(IsDBNull(RsTemp.Fields("PREVIOUS_AMOUNT").Value), 0, RsTemp.Fields("PREVIOUS_AMOUNT").Value))
                    Else
                        If mVPFRate <= 12 Then
                            mAmount = mPayablePFSalary * mVPFRate / 100
                            mActualAmount = mActualPFSalary * mVPFRate / 100
                        Else
                            mAmount = mPayablePFSalary * 12 / 100
                            mActualAmount = mActualPFSalary * 12 / 100

                            mRounding = RsTemp.Fields("ROUNDING").Value
                            If mRounding = Val("0.05") Then
                                mAmount = PaiseRound(mAmount, 0.05)
                                mActualAmount = PaiseRound(mActualAmount, 0.05)
                            ElseIf mRounding = Val("10") Then
                                mAmount = Int(mAmount) + IIf(mAmount > Int(mAmount), 1, 0)
                                mActualAmount = Int(mActualAmount) + IIf(mActualAmount > Int(mActualAmount), 1, 0)
                            Else
                                mRound = Replace(RsTemp.Fields("ROUNDING").Value, "1", "0")
                                mAmount = Val(VB6.Format(mAmount, mRound))
                                mActualAmount = Val(VB6.Format(mActualAmount, mRound))
                            End If

                            mAmount = mAmount + (mPayablePFSalary * (mVPFRate - 12) / 100)
                            mActualAmount = mActualAmount + (mActualPFSalary * (mVPFRate - 12) / 100)
                        End If
                    End If
                    mVPFAmount = mAmount
                    mRounding = RsTemp.Fields("ROUNDING").Value
                    mPFRounding = RsTemp.Fields("ROUNDING").Value
                ElseIf RsTemp.Fields("Type").Value = ConESI Then
                    If CDate(mSalDate) >= CDate("01/11/2011") Then
                        mAmount = 0
                        mActualAmount = 0
                        mESIAmt = 0
                    Else
                        If RsTemp.Fields("PERCENTAGE").Value = 0 Then
                            mAmount = mArrearMonth * (RsTemp.Fields("Amount").Value - IIf(IsDBNull(RsTemp.Fields("PREVIOUS_AMOUNT").Value), 0, RsTemp.Fields("PREVIOUS_AMOUNT").Value))
                            mActualAmount = mArrearMonth * (RsTemp.Fields("Amount").Value - IIf(IsDBNull(RsTemp.Fields("PREVIOUS_AMOUNT").Value), 0, RsTemp.Fields("PREVIOUS_AMOUNT").Value))
                        Else
                            mAmount = mPayableESISalary * RsTemp.Fields("PERCENTAGE").Value / 100
                            mActualAmount = mActualESISalary * RsTemp.Fields("PERCENTAGE").Value / 100
                        End If
                        mAmount = IIf(mAmount < 0, 0, mAmount)
                        mActualAmount = IIf(mActualAmount < 0, 0, mActualAmount)
                        mESIAmt = mAmount
                        If CDate(mSalDate) >= CDate("01/04/2006") Then
                            If mESIAmt > Int(mESIAmt) Then
                                mESIAmt = Int(mESIAmt) + 1
                            Else
                                mESIAmt = System.Math.Round(mESIAmt, 0)
                            End If
                        Else
                            mRounding = IIf(CDate(mSalDate) > CDate("01/12/2004"), "10", RsTemp.Fields("ROUNDING").Value)
                            mESIRounding = IIf(CDate(mSalDate) > CDate("01/12/2004"), "10", RsTemp.Fields("ROUNDING").Value)
                        End If
                    End If
                    mAmount = mESIAmt
                Else
                    If RsTemp.Fields("CALC_ON").Value = ConCalcBSalary Then
                        '                    If RsTemp!PERCENTAGE = 0 Then
                        If mSalary = 0 Then
                            ''24-10-2011
                            '                            mAmount = (RsTemp!Amount - IIf(IsNull(RsTemp!PREVIOUS_AMOUNT), 0, RsTemp!PREVIOUS_AMOUNT)) * mWDays * mTotalMonth / mTotalDays
                            '                            mActualAmount = (RsTemp!Amount - IIf(IsNull(RsTemp!PREVIOUS_AMOUNT), 0, RsTemp!PREVIOUS_AMOUNT)) * mTotalDays * mTotalMonth / mTotalDays

                            mAmount = (RsTemp.Fields("Amount").Value - IIf(IsDBNull(RsTemp.Fields("PREVIOUS_AMOUNT").Value), 0, RsTemp.Fields("PREVIOUS_AMOUNT").Value)) * mWDays * mArrearMonth / mTotalDays
                            mActualAmount = (RsTemp.Fields("Amount").Value - IIf(IsDBNull(RsTemp.Fields("PREVIOUS_AMOUNT").Value), 0, RsTemp.Fields("PREVIOUS_AMOUNT").Value)) * mTotalDays * mArrearMonth / mTotalDays

                        Else
                            mAmount = ((RsTemp.Fields("Amount").Value - IIf(IsDBNull(RsTemp.Fields("PREVIOUS_AMOUNT").Value), 0, RsTemp.Fields("PREVIOUS_AMOUNT").Value)) * mPayableSalary) / mSalary
                            mActualAmount = (RsTemp.Fields("Amount").Value - IIf(IsDBNull(RsTemp.Fields("PREVIOUS_AMOUNT").Value), 0, RsTemp.Fields("PREVIOUS_AMOUNT").Value))
                        End If
                        '                    Else
                        '
                        '                        mPrevPercent = GetPreviousPer(mCode, RsTemp!SALARY_APP_DATE, mSalHeadCode)
                        '
                        '                        mAmount = mPayableSalary * RsTemp!PERCENTAGE / 100
                        '                        mActualAmount = mSalary * RsTemp!PERCENTAGE / 100
                        '                        If RsTemp!PERCENTAGE - mPrevPercent > 0 Then
                        '                            mAmount = mAmount + (IIf(IsNull(RsTemp!PREVIOUS_BASICSALARY), 0, RsTemp!PREVIOUS_BASICSALARY) * (RsTemp!PERCENTAGE - mPrevPercent) * 0.01 * mArrearMonth)
                        '                            mActualAmount = mActualAmount + (IIf(IsNull(RsTemp!PREVIOUS_BASICSALARY), 0, RsTemp!PREVIOUS_BASICSALARY) * (RsTemp!PERCENTAGE - mPrevPercent) * 0.01 * mArrearMonth)
                        '                        End If
                        '                    End If
                    ElseIf RsTemp.Fields("CALC_ON").Value = ConCalcFixed Then
                        '                    If mPayableSalary = 0 Then
                        '                        mAmount = 0
                        '                        mActualAmount = 0
                        '                    Else
                        mAmount = mArrearMonth * (IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value) - IIf(IsDBNull(RsTemp.Fields("PREVIOUS_AMOUNT").Value), 0, RsTemp.Fields("PREVIOUS_AMOUNT").Value))
                        mActualAmount = mArrearMonth * (IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value) - IIf(IsDBNull(RsTemp.Fields("PREVIOUS_AMOUNT").Value), 0, RsTemp.Fields("PREVIOUS_AMOUNT").Value))
                        '                    End If
                        '                ElseIf RsTemp!CALC_ON = ConCalcVariable Then
                        '                    mAmount = GetMonthlyVarAmount(mCode, RsTemp!Code)
                        '                    mActualAmount = mAmount
                    End If
                End If

                If RsTemp.Fields("ROUNDING").Value = "0.05" Then
                    mAmount = PaiseRound(mAmount, 0.05)
                    mActualAmount = PaiseRound(mActualAmount, 0.05)
                ElseIf mRounding = Val("10") Then
                    mAmount = Int(mAmount) + IIf(mAmount > Int(mAmount), 1, 0)
                    mActualAmount = Int(mActualAmount) + IIf(mActualAmount > Int(mActualAmount), 1, 0)
                Else
                    mRound = Replace(RsTemp.Fields("ROUNDING").Value, "1", "0")
                    mAmount = Val(VB6.Format(mAmount, mRound))
                    mActualAmount = Val(VB6.Format(mActualAmount, mRound))
                End If


                SqlStr = " INSERT INTO PAY_SAL_TRN (" & vbCrLf _
                    & " COMPANY_CODE, FYEAR, EMP_CODE, " & vbCrLf _
                    & " SAL_DATE, BASICSALARY, PAYABLESALARY, " & vbCrLf _
                    & " WDAYS, SALHEADCODE , PAYABLEAMOUNT, " & vbCrLf _
                    & " ACTUALAMOUNT, DEPARTMENT, " & vbCrLf _
                    & " CATEGORY, PAYMENTMODE, BANKACCTNO,ISARREAR,DESG_DESC,DIV_CODE,BANKIFSCCODE,IS_PAID ) VALUES ( " & vbCrLf _
                    & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & mCurrentFYNo & ", " & vbCrLf _
                    & " '" & mCode & "',TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " " & mSalary & ", " & mPayableSalary & ", " & vbCrLf _
                    & " " & mWDays & ", " & mSalHeadCode & ", " & mAmount & ", " & vbCrLf _
                    & " " & mActualAmount & ", '" & mDepartment & "', " & vbCrLf _
                    & " '" & mCategory & "', '" & mPaymentMode & "',  " & vbCrLf _
                    & " '" & mBankAcctNo & "','" & mArrearCalc & "','" & mDesignation & "'," & mDivisionCode & ",'" & mBankIFSCCode & "','N')"

                PubDBCn.Execute(SqlStr)

                RsTemp.MoveNext()
            Loop


            ''CALC VARIABLES........

            SqlStr = "SELECT ADD_DEDUCTCODE, PERCENTAGE, SUM(AMOUNT) As AMOUNT1 FROM PAY_MONTHLY_TRN WHERE " & vbCrLf & " Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PAYYEAR=" & Year(CDate(lblNewDate.Text)) & " " & vbCrLf & " AND EMP_CODE= '" & mCode & "'" & vbCrLf & " AND TO_CHAR(Sal_MONTH,'MON-YYYY')=TO_CHAR('" & UCase(VB6.Format(mSalDate, "MMM-YYYY")) & "')" & vbCrLf & " AND SAL_FLAG='A' GROUP BY ADD_DEDUCTCODE, PERCENTAGE"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsVar, ADODB.LockTypeEnum.adLockOptimistic)

            If RsVar.EOF = False Then
                Do While Not RsVar.EOF
                    mSalHeadCode = IIf(IsDBNull(RsVar.Fields("ADD_DEDUCTCODE").Value), -1, RsVar.Fields("ADD_DEDUCTCODE").Value)
                    mAmount = IIf(IsDBNull(RsVar.Fields("AMOUNT1").Value), 0, RsVar.Fields("AMOUNT1").Value)
                    mActualAmount = mAmount

                    If mRounding = Val("0.05") Then
                        mAmount = PaiseRound(mAmount, 0.05)
                        mActualAmount = PaiseRound(mActualAmount, 0.05)
                    ElseIf mRounding = Val("10") Then
                        mAmount = Int(mAmount) + IIf(mAmount > Int(mAmount), 1, 0)
                        mActualAmount = Int(mAmount) + IIf(mActualAmount > Int(mActualAmount), 1, 0)
                    Else
                        mRound = Replace(CStr(mRounding), "1", "0")
                        mAmount = Val(VB6.Format(mAmount, mRound))
                        mActualAmount = Val(VB6.Format(mActualAmount, mRound))
                    End If

                    If mSalHeadCode <> -1 And mAmount <> 0 Then
                        SqlStr = " INSERT INTO PAY_SAL_TRN (" & vbCrLf & " COMPANY_CODE, FYEAR, EMP_CODE, " & vbCrLf & " SAL_DATE, BASICSALARY, PAYABLESALARY, " & vbCrLf & " WDAYS, SALHEADCODE , PAYABLEAMOUNT, " & vbCrLf & " ACTUALAMOUNT, DEPARTMENT, " & vbCrLf & " CATEGORY, PAYMENTMODE, BANKACCTNO,ISARREAR,DESG_DESC,DIV_CODE,BANKIFSCCODE,IS_PAID) VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & mCurrentFYNo & ", " & vbCrLf & " '" & mCode & "',TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " " & mSalary & ", " & mPayableSalary & ", " & vbCrLf & " " & mWDays & ", " & mSalHeadCode & ", " & mAmount & ", " & vbCrLf & " " & mActualAmount & ", '" & mDepartment & "', " & vbCrLf & " '" & mCategory & "', '" & mPaymentMode & "',  " & vbCrLf & " '" & mBankAcctNo & "','" & mArrearCalc & "','" & mDesignation & "'," & mDivisionCode & ",'" & mBankIFSCCode & "','N')"

                        PubDBCn.Execute(SqlStr)
                    End If
                    RsVar.MoveNext()
                Loop
            End If

            'PF ESI Calc....

            mRound = Replace(CStr(mPFRounding), "1", "0")

            mPFAmt = IIf(mPFAmt = 0, 0, VB6.Format(mPFAmt, mRound))
            mVPFAmount = IIf(mVPFAmount = 0, 0, VB6.Format(mVPFAmount, mRound))
            mPensionFund = IIf(mPensionFund = 0, 0, VB6.Format(mPensionFund, mRound))
            mEmpCont = IIf(mEmpCont = 0, 0, VB6.Format(mEmpCont, mRound))
            mPayablePensionWages = IIf(mPayablePensionWages = 0, 0, VB6.Format(mPayablePensionWages, mRound))

            mRound = CStr(mESIRounding)
            If mRound = "0.05" Then
                mESIAmt = PaiseRound(mESIAmt, 0.05)
            ElseIf mRound = "10" Then
                mESIAmt = Int(mESIAmt) + IIf(mESIAmt > Int(mESIAmt), 1, 0)
            Else
                mRound = Replace(CStr(mESIRounding), "1", "0")
                mESIAmt = IIf(mESIAmt = 0, 0, VB6.Format(mESIAmt, mRound))
            End If


            'save Actual Salary.....
            mSalary = mBSalary



            mPrevPensionFund = GetPensionFund(mCode, mSalDate)
            mPensionFundConst = System.Math.Round(mPFCeiling * 8.33 * 0.01, 0)
            If mPrevPensionFund <> 0 Then
                If mPrevPensionFund >= mPensionFundConst Then
                    mEmpCont = mEmpCont + mPensionFund
                    mPensionFund = 0
                Else
                    pPensionDiff = mPensionFundConst - mPrevPensionFund

                    If pPensionDiff < mPensionFund Then
                        mEmpCont = mEmpCont + (mPensionFund - pPensionDiff)
                        mPensionFund = pPensionDiff
                    End If
                End If
            End If

            SqlStr = " INSERT INTO PAY_PFESI_TRN ( " & vbCrLf & " COMPANY_CODE, FYEAR, EMP_CODE, " & vbCrLf & " SAL_DATE, BasicSalary, PFABLEAMT, PENSIONWAGES, PFAMT, PFRate ,  " & vbCrLf & " ESIABLEAMT , ESIAMT, ESIRATE, PENSIONFUND, EPFAMT ,  " & vbCrLf & " LEAVEWOP , WDAYS, ISARREAR, VPFAMT, VPFRATE ) VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & mCurrentFYNo & ", " & vbCrLf & " '" & mCode & "',TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " " & mSalary & ", " & mPayablePFSalary & "," & mPayablePensionWages & "," & mPFAmt & "," & mNewPFRate & ", " & vbCrLf & " " & mPayableESISalary & "," & mESIAmt & "," & mESIRate & ", " & vbCrLf & " " & mPensionFund & ", " & mEmpCont & "," & mLeaveWop & "," & vbCrLf & " " & mWDays & ", " & vbCrLf & " '" & mArrearCalc & "', " & mVPFAmount & ", " & mVPFRate & ") "

            PubDBCn.Execute(SqlStr)

        End If
NextRec:
        UpdateArrearSalTrn = True

        Exit Function
UpDateSalTrnErr:
        'Resume
        MsgBox(Err.Description)
        UpdateArrearSalTrn = False
    End Function


    Private Function UpdateKJArrearSalTrn(ByRef mCode As String, ByRef mDepartment As String, ByRef mCategory As String, ByRef mPaymentMode As Integer, ByRef mBankAcctNo As String, ByRef mBankIFSCCode As String, ByRef mArrearCalc As String, ByRef pAddDays As Double, ByRef mEmpDOJ As String, ByRef mDOL As String, ByRef mSalDate As String, ByRef pAge As Double, ByRef mDivisionCode As Double) As Boolean


        On Error GoTo UpDateSalTrnErr
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RSSalDef As ADODB.Recordset
        Dim RsVar As ADODB.Recordset

        Dim SqlStr As String = ""
        Dim mSalary As Double
        Dim mBSalary As Double
        Dim mPayableSalary As Double
        Dim mWDays As Double
        Dim mSalHeadCode As Integer
        Dim mAmount As Double
        Dim mActualAmount As Double
        Dim mRound As String
        Dim mPayablePFSalary As Double
        Dim mPayableESISalary As Double
        Dim mActualPFSalary As Double
        Dim mActualESISalary As Double
        Dim mPayablePensionWages As Double
        Dim mRounding As Double
        Dim mPFAmt As Double
        Dim mNewPFRate As Double
        Dim mPensionFund As Double
        Dim mEmpCont As Double
        Dim mESIAmt As Double
        Dim mLeaveWop As Double
        Dim mPFRounding As Double
        Dim mESIRounding As Double
        Dim mTempPFCeiling As Double

        Dim mArrearMonth As Double
        Dim cntMonthFrom As String
        Dim cntMonthTo As String
        Dim mTotalDays As Double
        Dim mDesignation As String
        Dim mPrevPercent As Double
        Dim mAddDays As Double
        Dim mAddMonthDays As Double
        Dim xAttnDate As String
        Dim xSalDate As String
        Dim mEmployer_PF As Double

        Dim mVPFRate As Double
        Dim mVPFAmount As Double

        Dim mTotalMonth As Double
        Dim xAddDays As Double
        Dim CntMonth As Double

        SqlStr = " SELECT DISTINCT SALARYDEF.SALARY_EFF_DATE " & vbCrLf & " FROM PAY_SALARYDEF_MST SALARYDEF" & vbCrLf & " WHERE " & vbCrLf & " SALARYDEF.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SALARYDEF.EMP_CODE = '" & mCode & "'" & vbCrLf & " AND SALARYDEF.SALARY_APP_DATE=( SELECT MAX(SALARY_APP_DATE)" & vbCrLf & " FROM PAY_SALARYDEF_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE = '" & mCode & "'" & vbCrLf & " AND SALARY_APP_DATE <= TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) " & vbCrLf & " AND IS_ARREAR='Y' AND TO_CHAR(ARREAR_DATE,'MON-YYYY') ='" & UCase(VB6.Format(mSalDate, "MMM-YYYY")) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSSalDef, ADODB.LockTypeEnum.adLockOptimistic)

        If RSSalDef.EOF = False Then
            Do While Not RSSalDef.EOF
                mWDays = 0
                mTotalDays = 0
                xSalDate = (IIf(IsDBNull(RSSalDef.Fields("SALARY_EFF_DATE").Value), "", RSSalDef.Fields("SALARY_EFF_DATE").Value))

                SqlStr = " SELECT SALARYDEF.*,ADD_DEDUCT.CODE, ADD_DEDUCT.TYPE, ADD_DEDUCT.CALC_ON," & vbCrLf & " ADD_DEDUCT.INCLUDEDPF,ADD_DEDUCT.INCLUDEDESI, " & vbCrLf & " ADD_DEDUCT.ROUNDING AS ROUNDING " & vbCrLf & " FROM PAY_SALARYDEF_MST SALARYDEF, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf & " WHERE " & vbCrLf & " SALARYDEF.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ADD_DEDUCT.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SALARYDEF.COMPANY_CODE=ADD_DEDUCT.COMPANY_CODE" & vbCrLf & " AND SALARYDEF.ADD_DEDUCTCODE=ADD_DEDUCT.CODE" & vbCrLf & " AND SALARYDEF.EMP_CODE = '" & mCode & "'" & vbCrLf & " AND ADD_DEDUCT.CALC_ON<>" & ConCalcVariable & " " & vbCrLf & " AND SALARYDEF.SALARY_EFF_DATE=( SELECT MAX(SALARY_EFF_DATE)" & vbCrLf & " FROM PAY_SALARYDEF_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE = '" & mCode & "'" & vbCrLf & " AND SALARY_EFF_DATE <= TO_DATE('" & VB6.Format(xSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) " & vbCrLf & " AND IS_ARREAR='Y' AND TO_CHAR(ARREAR_DATE,'MON-YYYY') ='" & UCase(VB6.Format(mSalDate, "MMM-YYYY")) & "'"

                '    SqlStr = SqlStr & vbCrLf & ""
                ''AND ADD_DEDUCT.TYPE NOT IN (" & ConAdvance & ", " & ConIncomeTax & ", " & ConLoan & ", " & ConOthers & ", " & ConImprest & ", " & ConOT & ", " & ConTDS & ", " & ConLIC & ")

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)


                If RsTemp.EOF = False Then
                    If IsDBNull(RsTemp.Fields("BASICSALARY").Value) Then GoTo NextRec
                    mArrearMonth = (IIf(IsDBNull(RsTemp.Fields("TOT_ARR_MONTH").Value), 0, RsTemp.Fields("TOT_ARR_MONTH").Value))
                    mAddDays = (IIf(IsDBNull(RsTemp.Fields("ADDDAYS_IN").Value), 0, RsTemp.Fields("ADDDAYS_IN").Value))

                    mSalary = RsTemp.Fields("BASICSALARY").Value - IIf(IsDBNull(RsTemp.Fields("PREVIOUS_BASICSALARY").Value), 0, RsTemp.Fields("PREVIOUS_BASICSALARY").Value)
                    mSalary = mSalary
                    mBSalary = RsTemp.Fields("BASICSALARY").Value


                    cntMonthFrom = CStr(CDate(VB6.Format(RsTemp.Fields("SALARY_EFF_DATE").Value, "DD/MM/YYYY")))
                    cntMonthTo = CStr(CDate(VB6.Format(RsTemp.Fields("SALARY_APP_DATE").Value, "DD/MM/YYYY")))

                    If MainClass.ValidateWithMasterTable(Trim(RsTemp.Fields("EMP_DESG_CODE").Value), "DESG_CODE", "DESG_DESC", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mDesignation = MasterNo
                    Else
                        mDesignation = "-1"
                    End If

                    mLeaveWop = 0

                    '        mLeaveWop = MainClass.LastDay(Month(lblNewDate.Caption), Year(lblNewDate.Caption)) - mWDays
                    '        mPayableSalary = Format(mSalary * mWDays / MainClass.LastDay(Month(mSalDate), Year(mSalDate)), "0.00")

                    If CDate(cntMonthFrom) = CDate(cntMonthTo) Then
                        mWDays = 0
                        mTotalDays = 0
                    Else
                        mTotalMonth = 0
                        Do While CDate(cntMonthFrom) < CDate(cntMonthTo)
                            mWDays = mWDays + CalcAttn(mCode, mEmpDOJ, mDOL, cntMonthFrom, mLeaveWop)
                            mTotalDays = mTotalDays + MainClass.LastDay(Month(CDate(cntMonthFrom)), Year(CDate(cntMonthFrom)))
                            cntMonthFrom = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, CDate(cntMonthFrom)))
                            mTotalMonth = mTotalMonth + 1
                        Loop
                    End If

                    If Val(CStr(mAddDays)) > 0 Then
                        cntMonthFrom = CStr(CDate(VB6.Format(RsTemp.Fields("SALARY_EFF_DATE").Value - 1, "DD/MM/YYYY")))
                        xAddDays = Val(CStr(mAddDays))
                        xAttnDate = VB6.Format(System.DateTime.FromOADate(CDate(cntMonthFrom).ToOADate - (mAddDays + 1)), "DD/MM/YYYY")
                        CntMonth = 0

                        Do While xAddDays > 0
                            xAttnDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -1, CDate(xAttnDate)))
                            If xAddDays >= MainClass.LastDay(Month(CDate(xAttnDate)), Year(CDate(xAttnDate))) Then
                                xAddDays = xAddDays - MainClass.LastDay(Month(CDate(xAttnDate)), Year(CDate(xAttnDate)))
                                CntMonth = CntMonth + 1
                            Else
                                xAttnDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, MainClass.LastDay(Month(CDate(xAttnDate)), Year(CDate(xAttnDate))) - xAddDays, CDate(xAttnDate)))
                                Exit Do
                            End If
                        Loop
                        '                    xAttnDate = Format(CVDate(cntMonthFrom) - (mAddDays - 1), "DD/MM/YYYY")       ''+ 1

                        mWDays = mWDays + CalcAttn(mCode, mEmpDOJ, mDOL, cntMonthFrom, mLeaveWop, xAttnDate, mAddDays)
                        mTotalDays = mTotalDays + mAddDays
                        mAddMonthDays = MainClass.LastDay(Month(CDate(cntMonthFrom)), Year(CDate(cntMonthFrom)))
                        mAddMonthDays = xAddDays / mAddMonthDays
                        mArrearMonth = mArrearMonth + CntMonth + mAddMonthDays
                    End If

                    mArrearMonth = mArrearMonth * mWDays / mTotalDays
                    mPayableSalary = mArrearMonth * mSalary
                    '                mPayableSalary = mArrearMonth * mSalary * mWDays / mTotalDays


                    Do While Not RsTemp.EOF
                        If RsTemp.Fields("INCLUDEDPF").Value = "Y" Then
                            mActualPFSalary = mActualPFSalary + (IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value) - IIf(IsDBNull(RsTemp.Fields("PREVIOUS_AMOUNT").Value), 0, RsTemp.Fields("PREVIOUS_AMOUNT").Value))
                            If mActualPFSalary <> 0 And mSalary = 0 Then
                                mPayablePFSalary = mPayablePFSalary + (IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value) - IIf(IsDBNull(RsTemp.Fields("PREVIOUS_AMOUNT").Value), 0, RsTemp.Fields("PREVIOUS_AMOUNT").Value)) * mArrearMonth
                            Else
                                mPayablePFSalary = mPayablePFSalary + ((IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value) - IIf(IsDBNull(RsTemp.Fields("PREVIOUS_AMOUNT").Value), 0, RsTemp.Fields("PREVIOUS_AMOUNT").Value)) * mPayableSalary) / IIf(mSalary = 0, 1, mSalary)
                            End If
                        End If
                        If CDate(mSalDate) >= CDate("01/11/2011") Then
                            mActualESISalary = 0
                            mPayableESISalary = 0
                        Else
                            If RsTemp.Fields("INCLUDEDESI").Value = "Y" Then
                                mActualESISalary = mActualESISalary + (IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value) - IIf(IsDBNull(RsTemp.Fields("PREVIOUS_AMOUNT").Value), 0, RsTemp.Fields("PREVIOUS_AMOUNT").Value))
                                If mActualESISalary <> 0 And mSalary = 0 Then
                                    mPayableESISalary = mPayableESISalary + (IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value) - IIf(IsDBNull(RsTemp.Fields("PREVIOUS_AMOUNT").Value), 0, RsTemp.Fields("PREVIOUS_AMOUNT").Value)) * mArrearMonth
                                Else
                                    mPayableESISalary = mPayableESISalary + ((IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value) - IIf(IsDBNull(RsTemp.Fields("PREVIOUS_AMOUNT").Value), 0, RsTemp.Fields("PREVIOUS_AMOUNT").Value)) * mPayableSalary) / IIf(mSalary = 0, 1, mSalary)
                                End If
                            End If
                        End If
                        RsTemp.MoveNext()
                    Loop
                    RsTemp.MoveFirst()
                    mPayablePFSalary = mPayableSalary + mPayablePFSalary
                    mPayablePFSalary = Val(VB6.Format(mPayablePFSalary, "0"))

                    mActualPFSalary = mSalary + mActualPFSalary
                    mActualPFSalary = Val(VB6.Format(mActualPFSalary, "0"))

                    If mPFCeiling <= RsTemp.Fields("PREVIOUS_BASICSALARY").Value Then
                        mTempPFCeiling = 0
                        mPayablePensionWages = 0
                    Else
                        If mPFCeiling <= RsTemp.Fields("BASICSALARY").Value Then
                            mTempPFCeiling = Val(VB6.Format((mPFCeiling - RsTemp.Fields("PREVIOUS_BASICSALARY").Value) * mWDays / MainClass.LastDay(Month(CDate(mSalDate)), Year(CDate(mSalDate))), "0.00"))
                            mPayablePensionWages = IIf(mPFCeiling <= RsTemp.Fields("BASICSALARY").Value, mTempPFCeiling, mPayablePFSalary)
                        Else
                            mTempPFCeiling = Val(VB6.Format(mPFCeiling * mWDays / MainClass.LastDay(Month(CDate(mSalDate)), Year(CDate(mSalDate))), "0.00"))
                            mPayablePensionWages = IIf(mPFCeiling <= mActualPFSalary, mTempPFCeiling, mPayablePFSalary)
                        End If
                    End If

                    '        mPayablePensionWages = IIf(mPFCeiling <= mActualPFSalary, mTempPFCeiling, mPayablePFSalary)
                    If pAge > 58 Then
                        mPayablePensionWages = 0
                    Else
                        mPayablePensionWages = Val(VB6.Format(mPayablePensionWages, "0"))
                    End If

                    mPayableESISalary = mPayableSalary + mPayableESISalary
                    mPayableESISalary = Val(VB6.Format(mPayableESISalary, "0"))

                    If CDate(mSalDate) >= CDate("01/11/2011") Then
                        mActualESISalary = 0
                        mPayableESISalary = 0
                    Else
                        mActualESISalary = mActualESISalary + mSalary
                        mActualESISalary = IIf(mESICeiling < mActualESISalary, 0, mActualESISalary)

                        mPayableESISalary = IIf(mESICeiling < mActualESISalary, 0, mPayableESISalary)
                        mPayableESISalary = Val(VB6.Format(mPayableESISalary, "0"))
                    End If

                    Do While Not RsTemp.EOF
                        mSalHeadCode = RsTemp.Fields("ADD_DEDUCTCODE").Value
                        If RsTemp.Fields("Type").Value = ConPF Then
                            If RsTemp.Fields("PERCENTAGE").Value = 0 Then
                                mAmount = mArrearMonth * (RsTemp.Fields("Amount").Value - IIf(IsDBNull(RsTemp.Fields("PREVIOUS_AMOUNT").Value), 0, RsTemp.Fields("PREVIOUS_AMOUNT").Value)) '' (mWDays * mTotalMonth / mTotalDays)
                                mActualAmount = mArrearMonth * (RsTemp.Fields("Amount").Value - IIf(IsDBNull(RsTemp.Fields("PREVIOUS_AMOUNT").Value), 0, RsTemp.Fields("PREVIOUS_AMOUNT").Value))
                            Else
                                '                            mAmount = mPayablePFSalary * RsTemp!PERCENTAGE / 100
                                '                            mActualAmount = mActualPFSalary * RsTemp!PERCENTAGE / 100
                                mAmount = mPayablePFSalary * mPFRate / 100
                                mActualAmount = mActualPFSalary * mPFRate / 100

                                mEmployer_PF = System.Math.Round(mAmount, 0)

                                If mPFRate <> RsTemp.Fields("PERCENTAGE").Value Then
                                    mRounding = Val(Replace(RsTemp.Fields("ROUNDING").Value, "1", "0"))
                                    mAmount = CDbl(VB6.Format(mAmount, CStr(mRounding))) * RsTemp.Fields("PERCENTAGE").Value / mPFRate
                                    mActualAmount = CDbl(VB6.Format(mActualAmount, CStr(mRounding))) * RsTemp.Fields("PERCENTAGE").Value / mPFRate
                                End If
                            End If
                            mPFAmt = mAmount
                            mNewPFRate = RsTemp.Fields("PERCENTAGE").Value
                            If RsTemp.Fields("PREVIOUS_BASICSALARY").Value <= mPFCeiling Then
                                '                If mActualPFSalary <= mPFCeiling Then
                                If pAge > 58 Then
                                    mPensionFund = 0
                                Else
                                    mPensionFund = mPayablePFSalary * mPFPensionRate / 100
                                End If
                                mRounding = CDbl(Replace(RsTemp.Fields("ROUNDING").Value, "1", "0"))
                                mPensionFund = CDbl(VB6.Format(mPensionFund, CStr(mRounding)))
                                mEmpCont = mEmployer_PF - mPensionFund ''mPFAmt
                            Else
                                If pAge > 58 Then
                                    mPensionFund = 0
                                Else
                                    mPensionFund = mTempPFCeiling * mPFPensionRate / 100
                                End If
                                mRounding = CDbl(Replace(RsTemp.Fields("ROUNDING").Value, "1", "0"))
                                mPensionFund = CDbl(VB6.Format(mPensionFund, CStr(mRounding)))
                                mEmpCont = mEmployer_PF - mPensionFund ''mPFAmt
                            End If

                            mRounding = RsTemp.Fields("ROUNDING").Value
                            mPFRounding = RsTemp.Fields("ROUNDING").Value
                        ElseIf RsTemp.Fields("Type").Value = ConVPFAllw Then
                            mVPFRate = IIf(IsDBNull(RsTemp.Fields("PERCENTAGE").Value), 0, RsTemp.Fields("PERCENTAGE").Value)
                            If mVPFRate = 0 Then
                                mAmount = mArrearMonth * (RsTemp.Fields("Amount").Value - IIf(IsDBNull(RsTemp.Fields("PREVIOUS_AMOUNT").Value), 0, RsTemp.Fields("PREVIOUS_AMOUNT").Value))
                                mActualAmount = mArrearMonth * (RsTemp.Fields("Amount").Value - IIf(IsDBNull(RsTemp.Fields("PREVIOUS_AMOUNT").Value), 0, RsTemp.Fields("PREVIOUS_AMOUNT").Value))
                            Else
                                If mVPFRate <= 12 Then
                                    mAmount = mPayablePFSalary * mVPFRate / 100
                                    mActualAmount = mActualPFSalary * mVPFRate / 100
                                Else
                                    mAmount = mPayablePFSalary * 12 / 100
                                    mActualAmount = mActualPFSalary * 12 / 100

                                    mRounding = RsTemp.Fields("ROUNDING").Value
                                    If mRounding = CDbl("0.05") Then
                                        mAmount = PaiseRound(mAmount, 0.05)
                                        mActualAmount = PaiseRound(mActualAmount, 0.05)
                                    ElseIf mRounding = CDbl("10") Then
                                        mAmount = Int(mAmount) + IIf(mAmount > Int(mAmount), 1, 0)
                                        mActualAmount = Int(mActualAmount) + IIf(mActualAmount > Int(mActualAmount), 1, 0)
                                    Else
                                        mRound = Replace(RsTemp.Fields("ROUNDING").Value, "1", "0")
                                        mAmount = CDbl(VB6.Format(mAmount, mRound))
                                        mActualAmount = CDbl(VB6.Format(mActualAmount, mRound))
                                    End If

                                    mAmount = mAmount + (mPayablePFSalary * (mVPFRate - 12) / 100)
                                    mActualAmount = mActualAmount + (mActualPFSalary * (mVPFRate - 12) / 100)
                                End If
                            End If
                            mVPFAmount = mAmount
                            mRounding = RsTemp.Fields("ROUNDING").Value
                            mPFRounding = RsTemp.Fields("ROUNDING").Value
                        ElseIf RsTemp.Fields("Type").Value = ConESI Then
                            If CDate(mSalDate) >= CDate("01/11/2011") Then
                                mAmount = 0
                                mActualAmount = 0
                                mESIAmt = 0
                            Else
                                If RsTemp.Fields("PERCENTAGE").Value = 0 Then
                                    mAmount = mArrearMonth * (RsTemp.Fields("Amount").Value - IIf(IsDBNull(RsTemp.Fields("PREVIOUS_AMOUNT").Value), 0, RsTemp.Fields("PREVIOUS_AMOUNT").Value))
                                    mActualAmount = mArrearMonth * (RsTemp.Fields("Amount").Value - IIf(IsDBNull(RsTemp.Fields("PREVIOUS_AMOUNT").Value), 0, RsTemp.Fields("PREVIOUS_AMOUNT").Value))
                                Else
                                    mAmount = mPayableESISalary * RsTemp.Fields("PERCENTAGE").Value / 100
                                    mActualAmount = mActualESISalary * RsTemp.Fields("PERCENTAGE").Value / 100
                                End If
                                mAmount = IIf(mAmount < 0, 0, mAmount)
                                mActualAmount = IIf(mActualAmount < 0, 0, mActualAmount)
                                mESIAmt = mAmount
                                If CDate(mSalDate) >= CDate("01/04/2006") Then
                                    If mESIAmt > Int(mESIAmt) Then
                                        mESIAmt = Int(mESIAmt) + 1
                                    Else
                                        mESIAmt = System.Math.Round(mESIAmt, 0)
                                    End If
                                Else
                                    mRounding = IIf(CDate(mSalDate) > CDate("01/12/2004"), "10", RsTemp.Fields("ROUNDING").Value)
                                    mESIRounding = IIf(CDate(mSalDate) > CDate("01/12/2004"), "10", RsTemp.Fields("ROUNDING").Value)
                                End If
                            End If
                        Else
                            If RsTemp.Fields("CALC_ON").Value = ConCalcBSalary Then
                                '                    If RsTemp!PERCENTAGE = 0 Then
                                If mSalary = 0 Then
                                    mAmount = mArrearMonth * (RsTemp.Fields("Amount").Value - IIf(IsDBNull(RsTemp.Fields("PREVIOUS_AMOUNT").Value), 0, RsTemp.Fields("PREVIOUS_AMOUNT").Value)) ''(mWDays * mTotalMonth / mTotalDays)
                                    mActualAmount = (RsTemp.Fields("Amount").Value - IIf(IsDBNull(RsTemp.Fields("PREVIOUS_AMOUNT").Value), 0, RsTemp.Fields("PREVIOUS_AMOUNT").Value))
                                Else
                                    mAmount = ((RsTemp.Fields("Amount").Value - IIf(IsDBNull(RsTemp.Fields("PREVIOUS_AMOUNT").Value), 0, RsTemp.Fields("PREVIOUS_AMOUNT").Value)) * mPayableSalary) / mSalary
                                    mActualAmount = (RsTemp.Fields("Amount").Value - IIf(IsDBNull(RsTemp.Fields("PREVIOUS_AMOUNT").Value), 0, RsTemp.Fields("PREVIOUS_AMOUNT").Value))
                                End If
                                '                    Else
                                '
                                '                        mPrevPercent = GetPreviousPer(mCode, RsTemp!SALARY_APP_DATE, mSalHeadCode)
                                '
                                '                        mAmount = mPayableSalary * RsTemp!PERCENTAGE / 100
                                '                        mActualAmount = mSalary * RsTemp!PERCENTAGE / 100
                                '                        If RsTemp!PERCENTAGE - mPrevPercent > 0 Then
                                '                            mAmount = mAmount + (IIf(IsNull(RsTemp!PREVIOUS_BASICSALARY), 0, RsTemp!PREVIOUS_BASICSALARY) * (RsTemp!PERCENTAGE - mPrevPercent) * 0.01 * mArrearMonth)
                                '                            mActualAmount = mActualAmount + (IIf(IsNull(RsTemp!PREVIOUS_BASICSALARY), 0, RsTemp!PREVIOUS_BASICSALARY) * (RsTemp!PERCENTAGE - mPrevPercent) * 0.01 * mArrearMonth)
                                '                        End If
                                '                    End If
                            ElseIf RsTemp.Fields("CALC_ON").Value = ConCalcFixed Then
                                If mPayableSalary = 0 Then
                                    mAmount = 0
                                    mActualAmount = 0
                                Else
                                    mAmount = mArrearMonth * (IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value) - IIf(IsDBNull(RsTemp.Fields("PREVIOUS_AMOUNT").Value), 0, RsTemp.Fields("PREVIOUS_AMOUNT").Value))
                                    mActualAmount = mArrearMonth * (IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value) - IIf(IsDBNull(RsTemp.Fields("PREVIOUS_AMOUNT").Value), 0, RsTemp.Fields("PREVIOUS_AMOUNT").Value))
                                End If
                                '                ElseIf RsTemp!CALC_ON = ConCalcVariable Then
                                '                    mAmount = GetMonthlyVarAmount(mCode, RsTemp!Code)
                                '                    mActualAmount = mAmount
                            End If
                        End If

                        If RsTemp.Fields("ROUNDING").Value = "0.05" Then
                            mAmount = PaiseRound(mAmount, 0.05)
                            mActualAmount = PaiseRound(mActualAmount, 0.05)
                        ElseIf mRounding = CDbl("10") Then
                            mAmount = Int(mAmount) + IIf(mAmount > Int(mAmount), 1, 0)
                            mActualAmount = Int(mActualAmount) + IIf(mActualAmount > Int(mActualAmount), 1, 0)
                        Else
                            mRound = Replace(RsTemp.Fields("ROUNDING").Value, "1", "0")
                            mAmount = CDbl(VB6.Format(mAmount, mRound))
                            mActualAmount = CDbl(VB6.Format(mActualAmount, mRound))
                        End If

                        '                    If mAmount + mActualAmount <> 0 Then
                        SqlStr = " INSERT INTO TEMP_PAY_SAL_TRN (" & vbCrLf & " USERID, " & vbCrLf & " COMPANY_CODE, FYEAR, EMP_CODE, " & vbCrLf & " SAL_DATE, BASICSALARY, PAYABLESALARY, " & vbCrLf & " WDAYS, SALHEADCODE , PAYABLEAMOUNT, " & vbCrLf & " ACTUALAMOUNT, DEPARTMENT, " & vbCrLf & " CATEGORY, PAYMENTMODE, BANKACCTNO,ISARREAR,DESG_DESC,DIV_CODE,BANKIFSCCODE,IS_PAID ) VALUES ( " & vbCrLf & " '" & PubUserID & "', " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & mCurrentFYNo & ", " & vbCrLf & " '" & mCode & "',TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " " & mSalary & ", " & mPayableSalary & ", " & vbCrLf & " " & mWDays & ", " & mSalHeadCode & ", " & mAmount & ", " & vbCrLf & " " & mActualAmount & ", '" & mDepartment & "', " & vbCrLf & " '" & mCategory & "', '" & mPaymentMode & "',  " & vbCrLf & " '" & mBankAcctNo & "','" & mArrearCalc & "','" & mDesignation & "'," & mDivisionCode & ",'" & mBankIFSCCode & "','N')"

                        PubDBCn.Execute(SqlStr)
                        '                    End If

                        RsTemp.MoveNext()
                    Loop

                    ''CALC VARIABLES........

                    SqlStr = "SELECT ADD_DEDUCTCODE, PERCENTAGE, SUM(AMOUNT) As AMOUNT1 FROM PAY_MONTHLY_TRN WHERE " & vbCrLf & " Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PAYYEAR=" & Year(CDate(lblNewDate.Text)) & " " & vbCrLf & " AND EMP_CODE= '" & mCode & "'" & vbCrLf & " AND TO_CHAR(Sal_MONTH,'MON-YYYY')=TO_CHAR('" & UCase(VB6.Format(mSalDate, "MMM-YYYY")) & "')" & vbCrLf & " AND SAL_FLAG='A' GROUP BY ADD_DEDUCTCODE, PERCENTAGE"

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsVar, ADODB.LockTypeEnum.adLockOptimistic)

                    If RsVar.EOF = False Then
                        Do While Not RsVar.EOF
                            mSalHeadCode = IIf(IsDBNull(RsVar.Fields("ADD_DEDUCTCODE").Value), -1, RsVar.Fields("ADD_DEDUCTCODE").Value)
                            mAmount = IIf(IsDBNull(RsVar.Fields("AMOUNT1").Value), 0, RsVar.Fields("AMOUNT1").Value)
                            mActualAmount = mAmount

                            If mRounding = CDbl("0.05") Then
                                mAmount = PaiseRound(mAmount, 0.05)
                                mActualAmount = PaiseRound(mActualAmount, 0.05)
                            ElseIf mRounding = CDbl("10") Then
                                mAmount = Int(mAmount) + IIf(mAmount > Int(mAmount), 1, 0)
                                mActualAmount = Int(mAmount) + IIf(mActualAmount > Int(mActualAmount), 1, 0)
                            Else
                                mRound = Replace(CStr(mRounding), "1", "0")
                                mAmount = CDbl(VB6.Format(mAmount, mRound))
                                mActualAmount = CDbl(VB6.Format(mActualAmount, mRound))
                            End If

                            If mSalHeadCode <> -1 Then
                                SqlStr = " INSERT INTO TEMP_PAY_SAL_TRN (USERID," & vbCrLf & " COMPANY_CODE, FYEAR, EMP_CODE, " & vbCrLf & " SAL_DATE, BASICSALARY, PAYABLESALARY, " & vbCrLf & " WDAYS, SALHEADCODE , PAYABLEAMOUNT, " & vbCrLf & " ACTUALAMOUNT, DEPARTMENT, " & vbCrLf & " CATEGORY, PAYMENTMODE, BANKACCTNO,ISARREAR,DESG_DESC,DIV_CODE,BANKIFSCCODE,IS_PAID) VALUES ( " & vbCrLf & " '" & PubUserID & "'," & RsCompany.Fields("COMPANY_CODE").Value & ", " & mCurrentFYNo & ", " & vbCrLf & " '" & mCode & "',TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " " & mSalary & ", " & mPayableSalary & ", " & vbCrLf & " " & mWDays & ", " & mSalHeadCode & ", " & mAmount & ", " & vbCrLf & " " & mActualAmount & ", '" & mDepartment & "', " & vbCrLf & " '" & mCategory & "', '" & mPaymentMode & "',  " & vbCrLf & " '" & mBankAcctNo & "','" & mArrearCalc & "','" & mDesignation & "'," & mDivisionCode & ",'" & mBankIFSCCode & "','N')"

                                PubDBCn.Execute(SqlStr)


                            End If
                            RsVar.MoveNext()
                        Loop
                    End If


                    'PF ESI Calc....

                    mRound = Replace(CStr(mPFRounding), "1", "0")

                    mPFAmt = IIf(mPFAmt = 0, 0, VB6.Format(mPFAmt, mRound))
                    mVPFAmount = IIf(mVPFAmount = 0, 0, VB6.Format(mVPFAmount, mRound))
                    mPensionFund = IIf(mPensionFund = 0, 0, VB6.Format(mPensionFund, mRound))
                    mEmpCont = IIf(mEmpCont = 0, 0, VB6.Format(mEmpCont, mRound))
                    mPayablePensionWages = IIf(mPayablePensionWages = 0, 0, VB6.Format(mPayablePensionWages, mRound))

                    mRound = CStr(mESIRounding)
                    If mRound = "0.05" Then
                        mESIAmt = PaiseRound(mESIAmt, 0.05)
                    ElseIf mRound = "10" Then
                        mESIAmt = Int(mESIAmt) + IIf(mESIAmt > Int(mESIAmt), 1, 0)
                    Else
                        mRound = Replace(CStr(mESIRounding), "1", "0")
                        mESIAmt = IIf(mESIAmt = 0, 0, VB6.Format(mESIAmt, mRound))
                    End If

                    'save Actual Salary.....
                    mSalary = mBSalary

                    SqlStr = " INSERT INTO TEMP_PAY_PFESI_TRN ( " & vbCrLf & " USERID, " & vbCrLf & " COMPANY_CODE, FYEAR, EMP_CODE, " & vbCrLf & " SAL_DATE, BasicSalary, PFABLEAMT, PENSIONWAGES, PFAMT, PFRate ,  " & vbCrLf & " ESIABLEAMT , ESIAMT, ESIRATE, PENSIONFUND, EPFAMT ,  " & vbCrLf & " LEAVEWOP , WDAYS, ISARREAR, VPFAMT, VPFRATE ) VALUES ( " & vbCrLf & " '" & PubUserID & "', " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & mCurrentFYNo & ", " & vbCrLf & " '" & mCode & "',TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " " & mSalary & ", " & mPayablePFSalary & "," & mPayablePensionWages & "," & mPFAmt & "," & mNewPFRate & ", " & vbCrLf & " " & mPayableESISalary & "," & mESIAmt & "," & mESIRate & ", " & vbCrLf & " " & mPensionFund & ", " & mEmpCont & "," & mLeaveWop & "," & vbCrLf & " " & mWDays & ", " & vbCrLf & " '" & mArrearCalc & "', " & mVPFAmount & ", " & mVPFRate & ") "

                    PubDBCn.Execute(SqlStr)

                End If
                RSSalDef.MoveNext()
            Loop
        End If

NextRec:
        UpdateKJArrearSalTrn = True

        Exit Function
UpDateSalTrnErr:
        Resume
        MsgBox(Err.Description)
        UpdateKJArrearSalTrn = False
    End Function
    Private Function GetPcRateWages(ByRef xCode As String, ByRef xRunDate As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset

        GetPcRateWages = 0
        SqlStr = " SELECT AMOUNT " & vbCrLf _
                & " FROM PAY_PCRATE_WAGES_TRN " & vbCrLf _
                & " WHERE " & vbCrLf _
                & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND EMP_CODE='" & xCode & "'" & vbCrLf _
                & " AND TO_CHAR(SAL_MONTH,'MMYYYY') = TO_CHAR('" & VB6.Format(xRunDate, "MMYYYY") & "') "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            GetPcRateWages = IIf(IsDBNull(RsTemp.Fields("AMOUNT").Value), 0, RsTemp.Fields("AMOUNT").Value)
        End If

        Exit Function
ErrPart:
        GetPcRateWages = 0
    End Function
    Private Function GetSalaryAmount(ByRef xCode As String, ByRef xRunDate As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTable As String

        GetSalaryAmount = 0

        If lblEmpType.Text = "D" Then
            mTable = "PAY_DUMMYACTUAL_SAL_TRN"        ''"PAY_DUMMYSAL_TRN"
        Else
            mTable = "PAY_ACTUAL_SAL_TRN"
        End If

        'If lblEmpType.Text = "D" Then
        '    mTable = "PAY_DUMMYSAL_TRN"        ''"PAY_DUMMYSAL_TRN"
        'Else
        '    mTable = "PAY_SAL_TRN"
        'End If

        'If lblEmpType.Text = "D" Then
        '    mTable = IIf(pSalaryType = "F", "PAY_DUMMYSAL_TRN", "PAY_DUMMYACTUAL_SAL_TRN")        ''"PAY_DUMMYSAL_TRN"
        'Else
        '    mTable = IIf(pSalaryType = "F", "PAY_SAL_TRN", "PAY_ACTUAL_SAL_TRN")
        'End If

        SqlStr = " SELECT PAYABLESALARY, PAYABLEAMOUNT AS AMOUNT " & vbCrLf _
                & " FROM " & mTable & " A, PAY_SALARYHEAD_MST B " & vbCrLf _
                & " WHERE " & vbCrLf _
                & " A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND A.EMP_CODE='" & xCode & "'" & vbCrLf _
                & " AND TO_CHAR(SAL_DATE,'MMYYYY')=TO_CHAR('" & VB6.Format(xRunDate, "MMYYYY") & "') " & vbCrLf _
                & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf _
                & " And A.SALHEADCODE=B.CODE AND B.ADDDEDUCT=" & ConEarning & " AND ISARREAR='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            GetSalaryAmount = IIf(IsDBNull(RsTemp.Fields("PAYABLESALARY").Value), 0, RsTemp.Fields("PAYABLESALARY").Value)
            Do While RsTemp.EOF = False
                GetSalaryAmount = GetSalaryAmount + IIf(IsDBNull(RsTemp.Fields("AMOUNT").Value), 0, RsTemp.Fields("AMOUNT").Value)
                RsTemp.MoveNext()
            Loop
        End If


        Exit Function
ErrPart:
        GetSalaryAmount = 0
    End Function
    Private Function UpdateOTTrn(ByRef mCode As String, ByRef mOTHour As Double, ByRef mOTMin As Double, ByRef mSalDate As String) As Boolean

        On Error GoTo UpDateSalTrnErr

        Dim SqlStr As String = ""
        Dim mOTRate As Double
        Dim mTOTOverTime As Double
        Dim mOTAmount As Double
        Dim mESIApp As Boolean
        Dim mBasicSalary As Double
        Dim mDate As Date

        Dim mTotal As Double
        Dim mESIAmount As Double
        Dim mNetAmount As Double
        Dim mESIRound As Double
        Dim RsVar As ADODB.Recordset
        Dim mAdvAmount As Double
        Dim mTable As String
        Dim mOverTimeAppType As String
        'Dim mPrevPensionFund As Double
        'Dim pPensionDiff As Double
        Dim mOTFactor As Double
        Dim mEmpRateType As String
        Dim mPCRateWages As Double
        Dim mSalaryWages As Double
        Dim mWdays As Double
        Dim mTotalWop_Absent As Double

        Dim mWOP As Double
        Dim mAbsent As Double
        Dim mEmpDOJ As String
        Dim mDOL As String
        Dim mLastDay As Double
        Dim mSqlstr As String = ""
        Dim RsTemp As ADODB.Recordset
        Dim mGrossSalary As Double
        ''CALC VARIABLES........

        If lblEmpType.Text = "D" Then
            mTable = "PAY_MONTHLY_DUMMY_OT_TRN"
        Else
            mTable = "PAY_MONTHLY_OT_TRN"
        End If


        mOTRate = 0

        mOTFactor = 1
        If MainClass.ValidateWithMasterTable(mCode, "EMP_CODE", "EMP_OT_RATE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mOTFactor = MasterNo
        End If

        mEmpRateType = "G"
        If MainClass.ValidateWithMasterTable(mCode, "EMP_CODE", "EMP_RATE_TYPE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mEmpRateType = MasterNo
        End If

        If MainClass.ValidateWithMasterTable(mCode, "EMP_CODE", "EMP_DOJ", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mEmpDOJ = MasterNo
        End If

        If MainClass.ValidateWithMasterTable(mCode, "EMP_CODE", "EMP_LEAVE_DATE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDOL = MasterNo
        End If

        SqlStr = "SELECT SUM(AMOUNT) As AMOUNT1 FROM PAY_MONTHLY_TRN WHERE " & vbCrLf _
            & " Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND PAYYEAR=" & Year(CDate(lblNewDate.Text)) & " " & vbCrLf _
            & " AND EMP_CODE= '" & mCode & "'" & vbCrLf _
            & " AND TO_CHAR(Sal_MONTH,'MON-YYYY')=TO_CHAR('" & UCase(VB6.Format(mSalDate, "MMM-YYYY")) & "')" & vbCrLf _
            & " AND SAL_FLAG='O' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsVar, ADODB.LockTypeEnum.adLockOptimistic)

        If RsVar.EOF = False Then
            mAdvAmount = IIf(IsDBNull(RsVar.Fields("AMOUNT1").Value), 0, RsVar.Fields("AMOUNT1").Value)
        End If

        ConWorkDay = MainClass.LastDay(Month(CDate(lblNewDate.Text)), Year(CDate(lblNewDate.Text)))

        mOTRate = CDbl(VB6.Format(GetOTRate(mCode, mSalDate, mESIApp, mBasicSalary, mESIRound, False, ""), "0.00"))
        mOTRate = mOTRate * IIf(IsDBNull(mOTFactor) Or Val(CStr(mOTFactor)) = 0, 1, Val(CStr(mOTFactor)))

        If mEmpRateType = "G" Then
            mTOTOverTime = CDbl(VB6.Format(GetTOTOverTime(mOTHour, mOTMin), "0.00"))

            mOTAmount = mTOTOverTime * CDbl(VB6.Format(mOTRate, "0.00"))
        Else

            mLastDay = MainClass.LastDay(Month(CDate(lblNewDate.Text)), Year(CDate(lblNewDate.Text)))

            mTOTOverTime = 0

            mPCRateWages = GetPcRateWages(mCode, mSalDate)
            mSalaryWages = GetSalaryAmount(mCode, mSalDate)

            If lblEmpType.Text = "D" Then
            Else
                'mSqlstr = " SELECT COUNT(1) AS CNTROW FROM PAY_ATTN_MST  " & vbCrLf _
                '        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                '        & " AND TO_CHAR(ATTN_DATE,'MON-YYYY')=TO_CHAR('" & UCase(VB6.Format(mSalDate, "MMM-YYYY")) & "')" & vbCrLf _
                '        & " AND EMP_CODE= '" & mCode & "'" & vbCrLf _
                '        & " AND EXTRA_LEAVE='Y'"

                'mSqlstr = mSqlstr & vbCrLf & " UNION ALL"

                'mSqlstr = mSqlstr & vbCrLf _
                '        & " SELECT COUNT(1) AS CNTROW FROM PAY_ATTN_MST  " & vbCrLf _
                '        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                '         & " AND EMP_CODE= '" & mCode & "'" & vbCrLf _
                '        & " AND TO_CHAR(ATTN_DATE,'MON-YYYY')=TO_CHAR('" & UCase(VB6.Format(mSalDate, "MMM-YYYY")) & "')" & vbCrLf _
                '        & " AND EXTRA_LEAVE_2='Y'"

                'MainClass.UOpenRecordSet(mSqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

                mWdays = 0
                'If RsTemp.EOF = False Then
                '    Do While RsTemp.EOF = False
                '        mWdays = mWdays + IIf(IsDBNull(RsTemp.Fields("CNTROW").Value), 0, RsTemp.Fields("CNTROW").Value)
                '        RsTemp.MoveNext()
                '    Loop
                'End If
                'If mWdays > 0 Then
                '    mWdays = mWdays / 2
                '    mGrossSalary = GetGrossSalaryActual(mCode, mSalDate)
                '    mSalaryWages = mSalaryWages - VB6.Format(mGrossSalary * mWdays / mLastDay, "0.00")
                'End If

                mWdays = CalcAttn(mCode, mEmpDOJ, mDOL, mSalDate, mTotalWop_Absent, , , mWOP, mAbsent, "F")
                If mWdays > 0 Then
                    mGrossSalary = GetGrossSalary(mCode, mSalDate)
                    mSalaryWages = VB6.Format(mGrossSalary * mWdays / mLastDay, "0.00")
                End If
            End If

            mOTAmount = mPCRateWages - mSalaryWages
            If lblEmpType.Text = "D" Then
            Else
                mOTAmount = IIf(mOTAmount <= 0, 0, mOTAmount)
            End If
        End If

        If lblEmpType.Text <> "D" Then
            mOTAmount = mOTAmount - GetAttnAwardAmount_Adj(-1, mCode, mSalDate, "I")
            mOTAmount = mOTAmount - GetIncentive_Adj(mCode, mSalDate)
        End If

        mOTAmount = Math.Round(mOTAmount, 0)


        If mESIApp = True And mOTAmount > 0 Then
            mDate = CDate(MainClass.LastDay(Month(CDate(mSalDate)), Year(CDate(mSalDate))) & "/" & Month(CDate(mSalDate)) & "/" & Year(CDate(mSalDate)))
            mESIAmount = mOTAmount * mESIRate / 100      ''mTOTOverTime * mOTRate * mESIRate / 100

            ''Sandeep
            'If mESIAmount > Int(mESIAmount) Then
            '    mESIAmount = Int(mESIAmount) + 1
            'Else
            '    mESIAmount = System.Math.Round(mESIAmount, 0)
            'End If

        Else
            mESIAmount = 0
        End If
        mNetAmount = CDbl(VB6.Format(mOTAmount - mESIAmount - mAdvAmount, "0"))


        '        If mNetAmount <> 0 Then
        SqlStr = " INSERT INTO " & mTable & "  (" & vbCrLf _
            & " COMPANY_CODE, PAYYEAR, EMP_CODE, " & vbCrLf _
            & " BASICSALARY, OT_DATE, " & vbCrLf _
            & " OT_HOUR, RATE , OT_AMOUNT, " & vbCrLf _
            & " ESIC_AMOUNT, NET_AMOUNT, ADV_AMOUNT, IS_ARREAR,  " & vbCrLf _
            & " ADDUSER, ADDDATE) VALUES ( " & vbCrLf _
            & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & mCurrentFYNo & ", " & vbCrLf _
            & " '" & mCode & "', " & mBasicSalary & ", " & vbCrLf _
            & " TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
            & "  " & mTOTOverTime & ", " & vbCrLf _
            & " " & mOTRate & ", " & mOTAmount & ", " & vbCrLf _
            & " " & mESIAmount & ", " & mNetAmount & ", " & mAdvAmount & ", 'N'," & vbCrLf _
            & "'" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
            & " TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        PubDBCn.Execute(SqlStr)
        '        End If

        '        'PF ESI Calc....
        '
        '        mPrevPensionFund = GetPensionFund(mCode, mSalDate)
        '
        '        If mPrevPensionFund <> 0 Then
        '            If mPrevPensionFund >= 541 Then
        '                mEmpCont = mEmpCont + mPensionFund
        '                mPensionFund = 0
        '            Else
        '                pPensionDiff = 541 - mPrevPensionFund
        '
        '                If pPensionDiff < mPensionFund Then
        '                    mEmpCont = mEmpCont + (mPensionFund - pPensionDiff)
        '                    mPensionFund = pPensionDiff
        '                End If
        '            End If
        '        End If

        If lblEmpType.Text <> "D" Then
            If mESIAmount <> 0 Then
                SqlStr = " INSERT INTO PAY_PFESI_TRN ( " & vbCrLf _
                    & " COMPANY_CODE, FYEAR, EMP_CODE, " & vbCrLf _
                    & " SAL_DATE, BasicSalary, PFABLEAMT, PENSIONWAGES, PFAMT, PFRate ,  " & vbCrLf _
                    & " ESIABLEAMT , ESIAMT, ESIRATE, PENSIONFUND, EPFAMT ,  " & vbCrLf _
                    & " LEAVEWOP , WDAYS, ISARREAR, VPFAMT, VPFRATE ) VALUES ( " & vbCrLf _
                    & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & mCurrentFYNo & ", " & vbCrLf _
                    & " '" & mCode & "',TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " " & mBasicSalary & ", 0,0,0,0, " & vbCrLf & " " & mOTAmount & "," & mESIAmount & "," & mESIRate & ", " & vbCrLf _
                    & " 0, 0,0," & vbCrLf & " 0, " & vbCrLf & " 'O',0,0) "

                PubDBCn.Execute(SqlStr)
            End If
        End If

        UpdateOTTrn = True

        Exit Function
UpDateSalTrnErr:
        'Resume
        MsgBox(Err.Description)
        UpdateOTTrn = False
    End Function

    Private Function UpdateArrearOTTrn(ByRef mCode As String, ByRef mSalDate As String) As Boolean

        On Error GoTo UpDateSalTrnErr

        Dim SqlStr As String = ""
        Dim mESIApp As Boolean
        Dim mBasicSalary As Double
        Dim mDate As Date

        Dim mTotal As Double

        Dim mESIRound As Double
        Dim RsVar As ADODB.Recordset
        Dim RsTempOT As ADODB.Recordset

        Dim mAdvAmount As Double

        Dim cntMonthFrom As String
        Dim cntMonthTo As String
        Dim pOTHour As Double
        Dim pOTMin As Double


        Dim mOverTime As Double
        Dim mOTRate As Double
        Dim mOTAmount As Double
        Dim mESIAmount As Double
        Dim mNetAmount As Double

        Dim mTOTOverTime As Double
        Dim mTotOTRate As Double
        Dim mTotOTAmount As Double
        Dim mTotESIAmount As Double
        Dim mTotNetAmount As Double
        Dim mArrearMonth As Double
        Dim mAddDays As Double
        Dim mOverTimeAppType As String
        ''CALC VARIABLES........

        mTOTOverTime = 0
        mTotOTRate = 0
        mTotOTAmount = 0
        mTotESIAmount = 0
        mTotNetAmount = 0
        mAddDays = 0

        SqlStr = " SELECT DISTINCT SALARY_APP_DATE, SALARY_EFF_DATE,TOT_ARR_MONTH,EMP_DESG_CODE,ADDDAYS_IN FROM " & vbCrLf & " PAY_SALARYDEF_MST SD " & vbCrLf & " WHERE " & vbCrLf & " SD.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SD.EMP_CODE='" & mCode & "'" '& vbCrLf |
        SqlStr = SqlStr & vbCrLf & " AND SD.IS_ARREAR='Y' " & vbCrLf & " AND TO_CHAR(SD.ARREAR_DATE,'MON-YYYY') ='" & UCase(VB6.Format(mSalDate, "MMM-YYYY")) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsVar, ADODB.LockTypeEnum.adLockOptimistic)

        If RsVar.EOF = False Then
            If MainClass.ValidateWithMasterTable(RsVar.Fields("EMP_DESG_CODE").Value, "DESG_CODE", "DESG_CAT", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DESG_CAT IN ('D','M')") = True Then
                UpdateArrearOTTrn = True
                Exit Function
            End If

            cntMonthFrom = CStr(CDate(VB6.Format(RsVar.Fields("SALARY_EFF_DATE").Value, "DD/MM/YYYY")))
            cntMonthTo = CStr(CDate(VB6.Format(RsVar.Fields("SALARY_APP_DATE").Value, "DD/MM/YYYY")))
            mArrearMonth = (IIf(IsDBNull(RsVar.Fields("TOT_ARR_MONTH").Value), 0, RsVar.Fields("TOT_ARR_MONTH").Value))
            mAddDays = (IIf(IsDBNull(RsVar.Fields("ADDDAYS_IN").Value), 0, RsVar.Fields("ADDDAYS_IN").Value))

            If Val(CStr(mAddDays)) > 0 Then
                cntMonthFrom = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1 * mAddDays, CDate(cntMonthFrom)))
            End If

            Do While CDate(cntMonthFrom) < CDate(cntMonthTo)
                SqlStr = " SELECT " & vbCrLf _
                    & " SUM(OT.OTHOUR + OT.PREV_OTHOUR) AS OTHOUR , SUM(OT.OTMIN + OT.PREV_OTMIN)AS OTMIN " & vbCrLf _
                    & " FROM PAY_OVERTIME_MST OT " & vbCrLf _
                    & " WHERE " & vbCrLf _
                    & " OT.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND TO_CHAR(OT.OT_DATE,'MON-YYYY')='" & UCase(VB6.Format(cntMonthFrom, "MMM-YYYY")) & "'" & vbCrLf _
                    & " AND OT.EMP_CODE='" & mCode & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempOT, ADODB.LockTypeEnum.adLockOptimistic)

                mOverTime = 0
                mOTRate = 0
                mOTAmount = 0
                mESIAmount = 0
                mNetAmount = 0
                mAdvAmount = 0

                If RsTempOT.EOF = False Then
                    mAdvAmount = 0
                    ConWorkDay = MainClass.LastDay(Month(CDate(cntMonthFrom)), Year(CDate(cntMonthFrom)))

                    If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 17 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Or RsCompany.Fields("COMPANY_CODE").Value = 28 Or RsCompany.Fields("COMPANY_CODE").Value = 33 Then
                        If MainClass.ValidateWithMasterTable(mCode, "EMP_CODE", "OVERTIME_APP", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mOverTimeAppType = MasterNo
                        End If
                        If mOverTimeAppType = "1" Or mOverTimeAppType = "3" Then
                            mOTRate = CDbl(VB6.Format(GetOTRate(mCode, mSalDate, mESIApp, mBasicSalary, mESIRound, True, mOverTimeAppType), "0.00"))
                        End If
                    Else
                        mOTRate = CDbl(VB6.Format(GetOTRate(mCode, mSalDate, mESIApp, mBasicSalary, mESIRound, True, ""), "0.00"))
                    End If
                    pOTHour = IIf(IsDBNull(RsTempOT.Fields("OTHOUR").Value), 0, RsTempOT.Fields("OTHOUR").Value)
                    pOTMin = IIf(IsDBNull(RsTempOT.Fields("OTMIN").Value), 0, RsTempOT.Fields("OTMIN").Value)

                    mOverTime = CDbl(VB6.Format(GetTOTOverTime(pOTHour, pOTMin), "0.00"))

                    If MainClass.ValidateWithMasterTable(mCode, "EMP_CODE", "EMP_OT_RATE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mOverTime = mOverTime * IIf(IsDBNull(MasterNo) Or Val(MasterNo) = 0, 2, Val(MasterNo))
                    End If

                    mOTAmount = mOverTime * CDbl(VB6.Format(mOTRate, "0.00"))

                    If CDate(mSalDate) >= CDate("01/10/2012") Then ''No arrear in ESI..
                        mESIApp = False
                    End If

                    If mESIApp = True Then
                        mDate = CDate(MainClass.LastDay(Month(CDate(mSalDate)), Year(CDate(mSalDate))) & "/" & Month(CDate(mSalDate)) & "/" & Year(CDate(mSalDate)))
                        mESIAmount = mOTAmount * mESIRate / 100
                        '                    If mESIRound = "0.05" Then
                        '                        mESIAmount = PaiseRound(mESIAmount, 0.05)
                        '                    ElseIf mESIRound = "10" Then
                        '                        mESIAmount = Int(mESIAmount) + IIf(mESIAmount > Int(mESIAmount), 1, 0)
                        '                    Else
                        '                        mESIRound = Replace(mESIRound, "1", "0")
                        '                        mESIAmount = IIf(mESIAmount = 0, 0, Format(mESIAmount, mESIRound))
                        '                    End If

                        '                    mESIRound = Replace(mESIRound, "1", "0")
                        '                    mESIAmount = Round(mESIAmount, 1)
                    Else
                        mESIAmount = 0
                    End If
                    '                mNetAmount = Format(mOTAmount - mESIAmount - mAdvAmount, "0")

                    mNetAmount = CDbl(VB6.Format(mOTAmount - mESIAmount - mAdvAmount, "0.00"))

                    mTOTOverTime = mTOTOverTime + mOverTime
                    mTotOTRate = mTotOTRate + mOTRate
                    mTotOTAmount = mTotOTAmount + mOTAmount
                    mTotESIAmount = mTotESIAmount + mESIAmount
                    mTotNetAmount = mTotNetAmount + mNetAmount

                End If
                cntMonthFrom = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, CDate(cntMonthFrom)))
            Loop

            If mArrearMonth = 0 Then
                mTotOTRate = 0
            Else
                mTotOTRate = mTotOTRate / mArrearMonth
            End If

            mTotOTAmount = CDbl(VB6.Format(mTotOTAmount, "0.00"))
            mTotESIAmount = CDbl(VB6.Format(mTotESIAmount, "0"))
            mTotNetAmount = CDbl(VB6.Format(mTotNetAmount, "0"))

            If CDate(mSalDate) >= CDate("01/04/2006") Then
                If mTotESIAmount > Int(mTotESIAmount) Then
                    mTotESIAmount = Int(mTotESIAmount) + 1
                Else
                    mTotESIAmount = System.Math.Round(mTotESIAmount, 0)
                End If
            End If

            SqlStr = " INSERT INTO PAY_MONTHLY_OT_TRN (" & vbCrLf & " COMPANY_CODE, PAYYEAR, EMP_CODE, " & vbCrLf & " BASICSALARY, OT_DATE, " & vbCrLf & " OT_HOUR, RATE , OT_AMOUNT, " & vbCrLf & " ESIC_AMOUNT, NET_AMOUNT, ADV_AMOUNT, IS_ARREAR, " & vbCrLf & " ADDUSER, ADDDATE) VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & mCurrentFYNo & ", " & vbCrLf & " '" & mCode & "', " & mBasicSalary & ", " & vbCrLf & " TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & "  " & mTOTOverTime & ", " & vbCrLf & " " & mTotOTRate & ", " & mTotOTAmount & ", " & vbCrLf & " " & mTotESIAmount & ", " & mTotNetAmount & ", " & mAdvAmount & ", 'Y'," & vbCrLf & "'" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

            PubDBCn.Execute(SqlStr)

            SqlStr = " INSERT INTO PAY_PFESI_TRN ( " & vbCrLf & " COMPANY_CODE, FYEAR, EMP_CODE, " & vbCrLf & " SAL_DATE, BasicSalary, PFABLEAMT, PENSIONWAGES, PFAMT, PFRate ,  " & vbCrLf & " ESIABLEAMT , ESIAMT, ESIRATE, PENSIONFUND, EPFAMT ,  " & vbCrLf & " LEAVEWOP , WDAYS, ISARREAR, VPFAMT, VPFRATE ) VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & mCurrentFYNo & ", " & vbCrLf & " '" & mCode & "',TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " " & mBasicSalary & ", 0,0,0,0, " & vbCrLf & " " & mTotNetAmount & "," & mTotESIAmount & "," & mESIRate & ", " & vbCrLf & " 0, 0,0," & vbCrLf & " 0, " & vbCrLf & " 'X',0,0) "

            PubDBCn.Execute(SqlStr)

        End If


        UpdateArrearOTTrn = True

        Exit Function
UpDateSalTrnErr:
        'Resume
        MsgBox(Err.Description)
        UpdateArrearOTTrn = False
    End Function
    Private Function GetTOTOverTime(ByRef xTotOTHOUR As Double, ByRef xTotOTMIN As Double) As Double
        On Error GoTo ErrPart
        Dim mHour As Double
        Dim mTempMin As Double
        Dim mMin As Double
        Dim mFactor As Double

        mHour = xTotOTHOUR
        mTempMin = xTotOTMIN

        mHour = mHour + Int(mTempMin / 60)
        mMin = (mTempMin Mod 60)
        mFactor = IIf(IsDBNull(RsCompany.Fields("OTFACTOR").Value), 0, RsCompany.Fields("OTFACTOR").Value)
        mMin = Int(mMin / mFactor) * mFactor

        If mMin <> 0 Then
            mMin = mMin / 60
        End If

        GetTOTOverTime = mHour + mMin

        Exit Function
ErrPart:
        GetTOTOverTime = 0
    End Function
    Private Function GetOTRate(ByRef xCode As String, ByRef xRunDate As String, ByRef mESIApp As Boolean, ByRef mBasicSalary As Double, ByRef mESIRound As Double, ByRef IsArrear As Boolean, ByRef mOverTimeAppType As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsOTRate As ADODB.Recordset
        Dim mRound As String
        Dim mGrossSalary As Double
        Dim mWorkHour As Double

        mWorkHour = 8
        If MainClass.ValidateWithMasterTable(xCode, "EMP_CODE", "WORKING_HOURS", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mWorkHour = Val(MasterNo)
        End If

        mWorkHour = IIf(mWorkHour = 0, 8, mWorkHour)

        SqlStr = " SELECT "

        If IsArrear = True Then
            SqlStr = SqlStr & vbCrLf & " (BASICSALARY-PREVIOUS_BASICSALARY) AS BASICSALARY, " & vbCrLf _
                & " (AMOUNT-PREVIOUS_AMOUNT) AS AMOUNT, (AMOUNT-PREVIOUS_AMOUNT) AS ACT_AMOUNT, "
        Else
            SqlStr = SqlStr & vbCrLf _
                & " DECODE(FORM1_BASICSALARY,0,BASICSALARY,FORM1_BASICSALARY) AS BASICSALARY, FORM1_AMOUNT AS AMOUNT, AMOUNT AS ACT_AMOUNT,"
        End If

        SqlStr = SqlStr & vbCrLf _
            & " ADD_DEDUCTCODE, ADDDEDUCT,TYPE, ROUNDING, EMP_DESG_CODE"

        SqlStr = SqlStr & vbCrLf _
            & " FROM PAY_SALARYDEF_MST SD, PAY_SALARYHEAD_MST SH " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " SD.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SD.COMPANY_CODE=SH.COMPANY_CODE" & vbCrLf _
            & " AND SD.ADD_DEDUCTCODE=SH.CODE" & vbCrLf _
            & " AND SD.EMP_CODE='" & xCode & "'" & vbCrLf _
            & " AND SD.SALARY_APP_DATE=(SELECT MAX(SALARY_APP_DATE) From PAY_SalaryDef_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE='" & xCode & "'" & vbCrLf _
            & " AND SALARY_APP_DATE<= TO_DATE('" & VB6.Format(xRunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

        If IsArrear = True Then
            SqlStr = SqlStr & vbCrLf _
                & " AND SD.IS_ARREAR='Y' AND TO_CHAR(SD.ARREAR_DATE,'MON-YYYY') ='" & UCase(VB6.Format(xRunDate, "MMM-YYYY")) & "'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOTRate, ADODB.LockTypeEnum.adLockOptimistic)

        If RsOTRate.EOF = False Then
            ''Manager or Director then exit function dt. 15-09-2006.............
            If MainClass.ValidateWithMasterTable(RsOTRate.Fields("EMP_DESG_CODE").Value, "DESG_CODE", "DESG_CAT", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DESG_CAT IN ('D','M')") = True Then
                GetOTRate = 0
                mBasicSalary = 0
                mESIApp = False
                Exit Function
            End If

            mBasicSalary = IIf(IsDBNull(RsOTRate.Fields("BASICSALARY").Value), 0, RsOTRate.Fields("BASICSALARY").Value)
            mGrossSalary = mBasicSalary

            If RsCompany.Fields("OVERTIME_ON").Value = "B" Then
                Do While Not RsOTRate.EOF
                    If RsOTRate.Fields("Type").Value = ConESI Then
                        '                    mESIRound = RsOTRate!ROUNDING
                        mESIRound = IIf(CDate(xRunDate) > CDate("01/12/2004"), "10", RsOTRate.Fields("ROUNDING").Value)
                        If RsOTRate.Fields("ACT_AMOUNT").Value = 0 Then
                            mESIApp = False
                        Else
                            mESIApp = True
                        End If
                    End If

                    RsOTRate.MoveNext()
                Loop
            Else
                Do While Not RsOTRate.EOF
                    If RsOTRate.Fields("ADDDEDUCT").Value = 1 Then
                        'If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 17 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Or RsCompany.Fields("COMPANY_CODE").Value = 28 Or RsCompany.Fields("COMPANY_CODE").Value = 33 Then
                        '    If mOverTimeAppType = "3" Then
                        '        mGrossSalary = mGrossSalary + IIf(IsDBNull(RsOTRate.Fields("Amount").Value), 0, RsOTRate.Fields("Amount").Value)
                        '    End If
                        'Else
                        mGrossSalary = mGrossSalary + IIf(IsDBNull(RsOTRate.Fields("Amount").Value), 0, RsOTRate.Fields("Amount").Value)
                        'End If
                    Else
                        '                mGrossSalary = mGrossSalary - IIf(IsNull(RsOTRate!AMOUNT), 0, RsOTRate!AMOUNT)
                        If RsOTRate.Fields("Type").Value = ConESI Then
                            '                    mESIRound = RsOTRate!ROUNDING
                            mESIRound = IIf(CDate(xRunDate) > CDate("01/12/2004"), "10", RsOTRate.Fields("ROUNDING").Value)
                            If RsOTRate.Fields("ACT_AMOUNT").Value = 0 Then
                                mESIApp = False
                            Else
                                mESIApp = True
                            End If
                        End If
                    End If
                    RsOTRate.MoveNext()
                Loop
            End If
            'If RsCompany.Fields("COMPANY_CODE").Value = 16 And CDate(xRunDate) >= CDate("01/12/2015") Then
            '    GetOTRate = mGrossSalary / (26 * ConWorkHour)
            'Else
            GetOTRate = mGrossSalary / (ConWorkDay * mWorkHour)
            'End If
        Else
            GetOTRate = 0
            mBasicSalary = 0
            mESIApp = False
        End If


        If MainClass.ValidateWithMasterTable(ConOT, "Type", "Rounding", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE= " & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            'If CDate(xRunDate) > CDate("01/12/2004") Then
            GetOTRate = Int(GetOTRate) + IIf(GetOTRate > Int(GetOTRate), 1, 0)
            'Else
            '    If MasterNo = "0.05" Then
            '        GetOTRate = PaiseRound(GetOTRate, 0.05)
            '    Else
            '        mRound = Replace(MasterNo, "1", "0")
            '        GetOTRate = CDbl(VB6.Format(GetOTRate, mRound))
            '    End If
            'End If
        Else
            GetOTRate = CDbl(VB6.Format(GetOTRate, "0.00"))
        End If
        mBasicSalary = mGrossSalary
        Exit Function
ErrPart:
        GetOTRate = 0
        mBasicSalary = 0
        mESIApp = False
    End Function

    Private Sub CheckPFRates(ByRef mDate As Date)

        Dim RsCeiling As ADODB.Recordset
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mSqlStr As String
        mSqlStr = ""

        mSqlStr = " SELECT MAX(WEF) FROM PAY_PFESICEILING_MST" & vbCrLf _
            & " WHERE " & vbCrLf _
            & " COMPANY_CODE= " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND CODE=" & ConPF & "" & vbCrLf _
            & " AND WEF<=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

        SqlStr = " SELECT * FROM PAY_PFESICEILING_MST WHERE " & vbCrLf & " COMPANY_CODE= " & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " CODE=" & ConPF & " AND WEF=(" & mSqlStr & ") "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCeiling, ADODB.LockTypeEnum.adLockOptimistic)
        If RsCeiling.EOF = False Then
            mPFCeiling = IIf(IsDBNull(RsCeiling.Fields("CEILING").Value), 0, RsCeiling.Fields("CEILING").Value)
            mPFRate = IIf(IsDBNull(RsCeiling.Fields("Rate").Value), 0, RsCeiling.Fields("Rate").Value)
            mPFEPFRate = IIf(IsDBNull(RsCeiling.Fields("EPF").Value), 0, RsCeiling.Fields("EPF").Value)
            mPFPensionRate = IIf(IsDBNull(RsCeiling.Fields("PFUND").Value), 0, RsCeiling.Fields("PFUND").Value)
            mEmplerPFCont = IIf(IsDBNull(RsCeiling.Fields("EMPER_CONT").Value), "B", RsCeiling.Fields("EMPER_CONT").Value)
        Else
            mPFCeiling = 1500
            mPFRate = 12
            mPFEPFRate = 3.67
            mPFPensionRate = 8.33
            mEmplerPFCont = "B"
        End If
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
    Private Sub CheckESIRates(ByRef mDate As Date)

        Dim RsCeiling As ADODB.Recordset
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mSqlStr As String

        mSqlStr = " SELECT MAX(WEF) FROM PAY_PFESICEILING_MST" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE= " & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " CODE=" & ConESI & " AND WEF<=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

        SqlStr = " SELECT * FROM PAY_PFESICEILING_MST WHERE " & vbCrLf & " COMPANY_CODE= " & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " CODE=" & ConESI & " AND WEF=(" & mSqlStr & ") "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCeiling, ADODB.LockTypeEnum.adLockOptimistic)
        If RsCeiling.EOF = False Then
            mESICeiling = IIf(IsDBNull(RsCeiling.Fields("CEILING").Value), 0, RsCeiling.Fields("CEILING").Value)
            mESIRate = IIf(IsDBNull(RsCeiling.Fields("Rate").Value), 0, RsCeiling.Fields("Rate").Value)
        Else
            If CDate(mDate) >= CDate("01/07/2019") Then
                mESICeiling = 21000
                mESIRate = 0.75
            Else
                mESICeiling = 7500
                mESIRate = 1.75
            End If
        End If
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub


    Private Function CalcLoan(ByRef mCode As Integer, ByRef mMonth As Short, ByRef mYear As Short) As Double

        On Error GoTo CalcLoanErr
        Dim RsTempLoan As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim mFHalf As Double
        Dim mSHalf As Double


        SqlStr = " SELECT * FROM LOANMASTER WHERE " & vbCrLf & " COMPANYCODE=" & RsCompany.Fields("CompanyCode").Value & " AND EMPCODE = " & mCode & " AND " & vbCrLf & " DEDUCT_MONTH=" & mMonth & " AND DEDUCT_YEAR=" & mYear & " AND " & vbCrLf & " ADD_DEDUCTCODE= " & ConLoan & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempLoan, ADODB.LockTypeEnum.adLockOptimistic)
        CalcLoan = 0
        If RsTempLoan.EOF = False Then
            mLoanDate = RsTempLoan.Fields("LoanDate").Value
            mLoanAmount = RsTempLoan.Fields("LOANAMOUNT").Value
            Do While Not RsTempLoan.EOF
                CalcLoan = CalcLoan + IIf(IsDBNull(RsTempLoan.Fields("Deduct_Amount").Value), 0, RsTempLoan.Fields("Deduct_Amount").Value)
                RsTempLoan.MoveNext()
            Loop
        End If
        Exit Function
CalcLoanErr:
        CalcLoan = 0
    End Function


    Private Function CalcAddDays(ByRef xCode As String) As Double

        On Error GoTo ErrCalcAddDays
        Dim RsCalcAddDays As ADODB.Recordset
        Dim SqlStr As String = ""

        CalcAddDays = 0
        SqlStr = "SELECT ADDDAYS FROM PAY_MONTHLY_TRN WHERE " & vbCrLf & " Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PAYYEAR=" & Year(CDate(lblNewDate.Text)) & " " & vbCrLf & " AND EMP_CODE= '" & xCode & " ' AND SAL_FLAG='S'" & vbCrLf & " AND TO_CHAR(Sal_MONTH,'MON-YYYY')=TO_CHAR('" & UCase(VB6.Format(lblNewDate.Text, "MMM-YYYY")) & "')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCalcAddDays, ADODB.LockTypeEnum.adLockOptimistic)

        If RsCalcAddDays.EOF = False Then
            CalcAddDays = IIf(IsDBNull(RsCalcAddDays.Fields("ADDDAYS").Value), 0, RsCalcAddDays.Fields("ADDDAYS").Value)
        End If
        Exit Function
ErrCalcAddDays:
        CalcAddDays = 0
    End Function

    Private Function UpdateAccountBSal(ByRef pYM As Integer, ByRef mCategory As String, ByRef mSALType As String, ByRef mArrear As String, ByRef mBookType As String, ByRef mBookSubType As String, ByRef mGrossPayableAmount As Double, ByRef mPayableAmount As Double, ByRef mDivisionCode As Double) As Boolean

        On Error GoTo UpdateAccountBSalErr
        Dim SqlStr As String = ""
        Dim RsSalPost As ADODB.Recordset
        Dim mSalary As Double
        Dim mTotSalary As Double
        Dim mAddDeduct As Integer
        Dim mAccountCode As String
        Dim mDC As String
        Dim mAmount As Double

        SqlStr = ""
        SqlStr = " Select DISTINCT EMP_CODE, PAYABLESALARY AS Amount " & vbCrLf & " FROM PAY_SAL_TRN " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR =" & mCurrentFYNo & " " & vbCrLf & " AND TO_CHAR(SAL_DATE,'YYYYMM')=" & pYM & "" & vbCrLf & " AND CATEGORY='" & mCategory & "' AND DIV_CODE=" & mDivisionCode & "" & vbCrLf & " AND ISARREAR='" & mArrear & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSalPost, ADODB.LockTypeEnum.adLockOptimistic)

        If RsSalPost.EOF = False Then
            Do While Not RsSalPost.EOF
                mSalary = IIf(IsDBNull(RsSalPost.Fields("Amount").Value), 0, RsSalPost.Fields("Amount").Value)
                mTotSalary = mTotSalary + mSalary
                RsSalPost.MoveNext()
            Loop
        End If

        '
        '

        mAmount = mGrossPayableAmount + mTotSalary
        mAccountCode = GetCategoryAcctCode(mBookSubType, "SD")
        '    If mCategory = "G" Then
        '        mAccountCode = IIf(IsNull(RsCompany!POSTSTAFFDACCOUNTCODE), -1, RsCompany!POSTSTAFFDACCOUNTCODE)
        '    ElseIf mCategory = "P" Then
        '        mAccountCode = IIf(IsNull(RsCompany!POSTPRODDACCOUNTCODE), -1, RsCompany!POSTPRODDACCOUNTCODE)
        '    ElseIf mCategory = "E" Then
        '        mAccountCode = IIf(IsNull(RsCompany!POSTEXPDACCOUNTCODE), -1, RsCompany!POSTEXPDACCOUNTCODE)
        '    ElseIf mCategory = "R" Then
        '        mAccountCode = IIf(IsNull(RsCompany!POSTWORKERDACCOUNTCODE), -1, RsCompany!POSTWORKERDACCOUNTCODE)
        '    ElseIf mCategory = "S" Then
        '        mAccountCode = IIf(IsNull(RsCompany!POSTRNDDACCOUNTCODE), -1, RsCompany!POSTRNDDACCOUNTCODE)
        '    ElseIf mCategory = "D" Then
        '        mAccountCode = IIf(IsNull(RsCompany!POSTDIRECTOR_DEBITCODE), -1, RsCompany!POSTDIRECTOR_DEBITCODE)
        '    ElseIf mCategory = "T" Then
        '        mAccountCode = IIf(IsNull(RsCompany!PostTRNDAccountCode), -1, RsCompany!PostTRNDAccountCode)
        '    End If

        If CDbl(mAccountCode) <> -1 Then
            If UpdateTMSal(pYM, mAccountCode, "DR", mAmount, mCategory, mSALType, mArrear, mBookType, mBookSubType, "", mDivisionCode) = False Then GoTo UpdateAccountBSalErr
        End If

        mAmount = mPayableAmount + mTotSalary
        mAccountCode = GetCategoryAcctCode(mBookSubType, "SC")
        '    If mCategory = "G" Then
        '        mAccountCode = IIf(IsNull(RsCompany!POSTSTAFFCACCOUNTCODE), -1, RsCompany!POSTSTAFFCACCOUNTCODE)
        '    ElseIf mCategory = "P" Then
        '        mAccountCode = IIf(IsNull(RsCompany!POSTPRODCACCOUNTCODE), -1, RsCompany!POSTPRODCACCOUNTCODE)
        '    ElseIf mCategory = "E" Then
        '        mAccountCode = IIf(IsNull(RsCompany!POSTEXPCACCOUNTCODE), -1, RsCompany!POSTEXPCACCOUNTCODE)
        '    ElseIf mCategory = "R" Then
        '        mAccountCode = IIf(IsNull(RsCompany!POSTWORKERCACCOUNTCODE), -1, RsCompany!POSTWORKERCACCOUNTCODE)
        '    ElseIf mCategory = "S" Then
        '        mAccountCode = IIf(IsNull(RsCompany!POSTRNDCACCOUNTCODE), -1, RsCompany!POSTRNDCACCOUNTCODE)
        '    ElseIf mCategory = "D" Then
        '        mAccountCode = IIf(IsNull(RsCompany!POSTDIRECTOR_CREDITCODE), -1, RsCompany!POSTDIRECTOR_CREDITCODE)
        '    ElseIf mCategory = "T" Then
        '        mAccountCode = IIf(IsNull(RsCompany!PostTRNCAccountCode), -1, RsCompany!PostTRNCAccountCode)
        '    End If

        If CDbl(mAccountCode) <> -1 Then
            If UpdateTMSal(pYM, mAccountCode, "CR", mAmount, mCategory, mSALType, mArrear, mBookType, mBookSubType, "", mDivisionCode) = False Then GoTo UpdateAccountBSalErr
        End If

        UpdateAccountBSal = True
        Exit Function
UpdateAccountBSalErr:
        UpdateAccountBSal = False
    End Function

    Private Function UpdateAccountBSalEmpWise(ByRef pYM As Integer, ByRef mCategory As String, ByRef mSALType As String, ByRef mArrear As String, ByRef mBookType As String, ByRef mBookSubType As String, ByRef mGrossPayableAmount As Double, ByRef mPayableAmount As Double, ByRef mDivisionCode As Double) As Boolean

        On Error GoTo UpdateAccountBSalEmpWiseErr
        Dim SqlStr As String = ""
        Dim RsSalPost As ADODB.Recordset
        Dim RsTemp As ADODB.Recordset
        Dim mSalary As Double
        Dim mTotSalary As Double
        Dim mAddDeduct As Integer
        Dim mAccountCode As String
        Dim mDC As String
        Dim mAmount As Double
        Dim mEmpNetAmount As Double
        Dim mEmpCode As String

        SqlStr = ""
        SqlStr = " Select DISTINCT EMP_CODE, PAYABLESALARY AS Amount " & vbCrLf _
            & " FROM PAY_SAL_TRN " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND FYEAR =" & mCurrentFYNo & " " & vbCrLf _
            & " AND TO_CHAR(SAL_DATE,'YYYYMM')=" & pYM & "" & vbCrLf _
            & " AND CATEGORY='" & mCategory & "' AND DIV_CODE=" & mDivisionCode & "" & vbCrLf _
            & " AND ISARREAR='" & mArrear & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSalPost, ADODB.LockTypeEnum.adLockOptimistic)


        If RsSalPost.EOF = False Then
            Do While Not RsSalPost.EOF
                mSalary = IIf(IsDBNull(RsSalPost.Fields("Amount").Value), 0, RsSalPost.Fields("Amount").Value)
                mTotSalary = mTotSalary + mSalary
                RsSalPost.MoveNext()
            Loop
        End If

        mAmount = mGrossPayableAmount + mTotSalary
        mAccountCode = GetCategoryAcctCode(mBookSubType, "SD")

        If mAccountCode <> -1 Then
            If UpdateTMSal(pYM, mAccountCode, "DR", mAmount, mCategory, mSALType, mArrear, mBookType, mBookSubType, "", mDivisionCode) = False Then GoTo UpdateAccountBSalEmpWiseErr
        End If

        SqlStr = ""
        SqlStr = " Select DISTINCT EMP_CODE, PAYABLESALARY AS Amount " & vbCrLf _
            & " FROM PAY_SAL_TRN " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND FYEAR =" & mCurrentFYNo & " " & vbCrLf _
            & " AND TO_CHAR(SAL_DATE,'YYYYMM')=" & pYM & "" & vbCrLf _
            & " AND CATEGORY='" & mCategory & "' AND DIV_CODE=" & mDivisionCode & "" & vbCrLf _
            & " AND ISARREAR='" & mArrear & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSalPost, ADODB.LockTypeEnum.adLockOptimistic)


        If RsSalPost.EOF = False Then
            Do While Not RsSalPost.EOF

                mEmpNetAmount = 0
                mEmpNetAmount = IIf(IsDBNull(RsSalPost.Fields("Amount").Value), 0, RsSalPost.Fields("Amount").Value)
                mEmpCode = IIf(IsDBNull(RsSalPost.Fields("EMP_CODE").Value), "", RsSalPost.Fields("EMP_CODE").Value)
                mAccountCode = GetEmpSalaryAcctCode(mEmpCode)

                SqlStr = " Select SUM(TRN.PAYABLEAMOUNT * DECODE(ADDDEDUCT,1,1,-1)) AS Amount " & vbCrLf _
                    & " FROM PAY_SAL_TRN TRN, PAY_SALARYHEAD_MST SALHEAD  " & vbCrLf _
                    & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TRN.FYEAR =" & mCurrentFYNo & " " & vbCrLf _
                    & " AND TRN.COMPANY_CODE=SALHEAD.COMPANY_CODE AND TRN.SALHEADCODE=SALHEAD.CODE " & vbCrLf _
                    & " AND TRN.EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf _
                    & " AND TO_CHAR(TRN.SAL_DATE,'YYYYMM')=" & pYM & "" & vbCrLf _
                    & " AND TRN.CATEGORY='" & mCategory & "' AND TRN.DIV_CODE=" & mDivisionCode & "" & vbCrLf _
                    & " AND TRN.ISARREAR='" & mArrear & "' AND ADDDEDUCT IN (1,2)"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)


                If RsTemp.EOF = False Then
                    mEmpNetAmount = mEmpNetAmount + IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
                End If

                If mEmpNetAmount > 0 And (mAccountCode = "-1" Or mAccountCode = "") Then
                    MsgInformation("Employee Salary Head Code in Not Defined. Emp Code : " & mEmpCode)
                    UpdateAccountBSalEmpWise = False
                    Exit Function
                Else
                    If UpdateTMSal(pYM, mAccountCode, "CR", mEmpNetAmount, mCategory, mSALType, mArrear, mBookType, mBookSubType, "", mDivisionCode) = False Then GoTo UpdateAccountBSalEmpWiseErr
                End If

                RsSalPost.MoveNext()
            Loop
        End If

        'SqlStr = ""
        'SqlStr = " Select DISTINCT EMP_CODE, PAYABLESALARY AS Amount " & vbCrLf & " FROM PAY_SAL_TRN " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR =" & mCurrentFYNo & " " & vbCrLf & " AND TO_CHAR(SAL_DATE,'YYYYMM')=" & pYM & "" & vbCrLf & " AND CATEGORY='" & mCategory & "' AND DIV_CODE=" & mDivisionCode & "" & vbCrLf & " AND ISARREAR='" & mArrear & "' "

        'MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSalPost, ADODB.LockTypeEnum.adLockOptimistic)

        'If RsSalPost.EOF = False Then
        '    Do While Not RsSalPost.EOF
        '        mSalary = IIf(IsDBNull(RsSalPost.Fields("Amount").Value), 0, RsSalPost.Fields("Amount").Value)
        '        mTotSalary = mTotSalary + mSalary
        '        RsSalPost.MoveNext()
        '    Loop
        'End If

        'mAmount = mGrossPayableAmount + mTotSalary
        'mAccountCode = GetCategoryAcctCode(mBookSubType, "SD")
        'If CDbl(mAccountCode) <> -1 Then
        '    If UpdateTMSal(pYM, mAccountCode, "DR", mAmount, mCategory, mSALType, mArrear, mBookType, mBookSubType, "", mDivisionCode) = False Then GoTo UpdateAccountBSalEmpWiseErr
        'End If

        'mAmount = mPayableAmount + mTotSalary
        'mAccountCode = GetCategoryAcctCode(mBookSubType, "SC")

        'If CDbl(mAccountCode) <> -1 Then
        '    If UpdateTMSal(pYM, mAccountCode, "CR", mAmount, mCategory, mSALType, mArrear, mBookType, mBookSubType, "", mDivisionCode) = False Then GoTo UpdateAccountBSalEmpWiseErr
        'End If

        UpdateAccountBSalEmpWise = True
        Exit Function
UpdateAccountBSalEmpWiseErr:
        UpdateAccountBSalEmpWise = False
    End Function
    Public Function GetEmpSalaryAcctCode(ByRef pEmpCode As String) As String
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim RS As ADODB.Recordset
        GetEmpSalaryAcctCode = "-1"

        SqlStr = "SELECT SUPP_CUST_CODE FROM FIN_SUPP_CUST_MST" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND HEADTYPE='5'" & vbCrLf _
            & " AND EMP_CODE='" & MainClass.AllowSingleQuote(pEmpCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockOptimistic)

        If RS.EOF = False Then
            GetEmpSalaryAcctCode = IIf(IsDBNull(RS.Fields("SUPP_CUST_CODE").Value), "-1", RS.Fields("SUPP_CUST_CODE").Value)
        End If

        Exit Function
ERR1:
        ErrorMsg(Err.Description, Err.Number, vbCritical)
    End Function

    Private Function UpdateAccountPostingHead(ByRef pYM As Integer, ByRef mArrear As String, ByRef mCategory As String, ByRef mSALType As String, ByRef mBookType As String, ByRef mBookSubType As String, ByRef mDivisionCode As Double) As Boolean


        On Error GoTo UpdateAccountPostingHeadErr
        Dim SqlStr As String = ""
        Dim RsAcctPost As ADODB.Recordset
        Dim mAddDeduct As Integer
        Dim mAccountCode As String
        Dim mDC As String
        Dim mAmount As Double
        Dim mPayableAmount As Double
        Dim mGrossPayableAmount As Double
        Dim mEmpCode As String
        Dim mSqlCond As String

        Dim mAmount2 As Double
        Dim mParticulars As String
        Dim mParticulars2 As String

        Dim mAmount3 As Double
        Dim mParticulars3 As String

        Dim mBasicSalary As Double
        Dim mSalDate As String
        Dim mPFAdminCharge As String
        Dim mEmpWisePosting As Boolean

        ''FYEAR =" & mCurrentFYNo & "

        SqlStr = " Delete from FIN_TMSal_TRN " & vbCrLf & " Where " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND  " & vbCrLf & " YM=" & pYM & "" & vbCrLf & " AND CATEGORY='" & mCategory & "'" & vbCrLf & " AND SALTYPE='" & mSALType & "'" & vbCrLf & " AND ISARREAR='" & mArrear & "' " & vbCrLf & " AND BookType='" & mBookType & "'" & vbCrLf & " AND BookSubType='" & mBookSubType & "'" & vbCrLf & " AND DIV_CODE=" & mDivisionCode & ""

        PubDBCn.Execute(SqlStr)

        '    SqlStr = " SELECT DISTINCT EMP_CODE, PAYABLESALARY " & vbCrLf _
        ''            & " FROM PAY_SAL_TRN TRN " & vbCrLf _
        ''            & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
        ''            & " AND TRN.FYEAR =" & mCurrentFYNo & " " & vbCrLf _
        ''            & " AND TO_CHAR(TRN.SAL_DATE,'YYYYMM')=" & pYM & "" & vbCrLf _
        ''            & " AND CATEGORY='" & mCategory & "'" & vbCrLf _
        ''            & " AND ISARREAR='" & mArrear & "'"
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsAcctPost, adLockOptimistic
        '    If RsAcctPost.EOF = False Then
        '        Do While RsAcctPost.EOF = False
        '            mBasicSalary = mBasicSalary + IIf(IsNull(RsAcctPost!PAYABLESALARY), 0, RsAcctPost!PAYABLESALARY)
        '            RsAcctPost.MoveNext
        '        Loop
        '    End If
        '
        'Salary Head........
        SqlStr = ""
        SqlStr = " SELECT SALHEAD.NAME, TRN.SALHEADCODE, SUM(PAYABLEAMOUNT) As AMOUNT, " & vbCrLf _
            & " SALHEAD.ADDDEDUCT,SALHEAD.ACCOUNTCODEPOST,SALHEAD.DC,TYPE " & vbCrLf _
            & " FROM PAY_SAL_TRN TRN, PAY_SALARYHEAD_MST SALHEAD " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND TRN.FYEAR =" & mCurrentFYNo & " " & vbCrLf _
            & " AND TRN.COMPANY_CODE=SALHEAD.COMPANY_CODE" & vbCrLf _
            & " AND TRN.SALHEADCODE=SALHEAD.CODE " & vbCrLf _
            & " AND TO_CHAR(TRN.SAL_DATE,'YYYYMM')=" & pYM & "" & vbCrLf _
            & " AND CATEGORY='" & mCategory & "'" & vbCrLf _
            & " AND ISARREAR='" & mArrear & "'" & vbCrLf _
            & " AND DIV_CODE=" & mDivisionCode & ""

        mSalDate = "01/" & VB.Right(CStr(pYM), 2) & "/" & VB.Left(CStr(pYM), 4)

        SqlStr = SqlStr & vbCrLf _
            & " AND SALHEAD.CODE IN (" & vbCrLf _
            & " SELECT CODE FROM PAY_SALARYHEAD_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND ADDDEDUCT IN (" & ConEarning & "," & ConDeduct & ")" & vbCrLf _
            & " AND STATUS='O' AND (CLOSED_DATE IS NULL OR CLOSED_DATE<=TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))" & vbCrLf _
            & " UNION " & vbCrLf _
            & " SELECT CODE FROM PAY_SALARYHEAD_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND ADDDEDUCT IN (" & ConEarning & "," & ConDeduct & ")" & vbCrLf _
            & " AND STATUS='C' AND CLOSED_DATE>TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"


        SqlStr = SqlStr & vbCrLf & " GROUP BY SALHEAD.NAME, TRN.SALHEADCODE, SALHEAD.ADDDEDUCT,SALHEAD.ACCOUNTCODEPOST,SALHEAD.DC,TYPE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAcctPost, ADODB.LockTypeEnum.adLockOptimistic)

        If RsAcctPost.EOF = False Then
            Do While Not RsAcctPost.EOF
                mAddDeduct = IIf(IsDBNull(RsAcctPost.Fields("ADDDEDUCT").Value), ConDeduct, RsAcctPost.Fields("ADDDEDUCT").Value)
                mDC = IIf(IsDBNull(RsAcctPost.Fields("DC").Value), "DR", RsAcctPost.Fields("DC").Value)
                mAmount = IIf(IsDBNull(RsAcctPost.Fields("Amount").Value), 0, RsAcctPost.Fields("Amount").Value)

                mParticulars = IIf(IsDBNull(RsAcctPost.Fields("Name").Value), 0, RsAcctPost.Fields("Name").Value)

                mAccountCode = IIf(IsDBNull(RsAcctPost.Fields("ACCOUNTCODEPOST").Value), "-1", RsAcctPost.Fields("ACCOUNTCODEPOST").Value)

                If RsAcctPost.Fields("Type").Value = ConAdvance Or RsAcctPost.Fields("Type").Value = ConImprest Or RsAcctPost.Fields("Type").Value = ConLoan Then
                    mAccountCode = "-1"
                End If

                If mAccountCode <> "-1" Then
                    If UpdateTMSal(pYM, mAccountCode, mDC, mAmount, mCategory, mSALType, mArrear, mBookType, mBookSubType, mParticulars, mDivisionCode) = False Then GoTo UpdateAccountPostingHeadErr
                End If

                mGrossPayableAmount = mGrossPayableAmount + (mAmount * IIf(mAddDeduct = ConDeduct, 0, 1))
                mPayableAmount = mPayableAmount + (mAmount * IIf(mAddDeduct = ConDeduct, -1, 1))
                RsAcctPost.MoveNext()
            Loop
        Else
            UpdateAccountPostingHead = True
            Exit Function
        End If


        If pYM <= 202103 Then
            mEmpWisePosting = False
        Else
            mEmpWisePosting = True
        End If

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 110 Then
            If UpdateAccountBSalEmpWise(pYM, mCategory, mSALType, mArrear, mBookType, mBookSubType, mGrossPayableAmount, mPayableAmount, mDivisionCode) = False Then GoTo UpdateAccountPostingHeadErr
        Else
            If UpdateAccountBSal(pYM, mCategory, mSALType, mArrear, mBookType, mBookSubType, mGrossPayableAmount, mPayableAmount, mDivisionCode) = False Then GoTo UpdateAccountPostingHeadErr
        End If

        'If UpdateAccountBSal(pYM, mCategory, mSALType, mArrear, mBookType, mBookSubType, mGrossPayableAmount, mPayableAmount, mDivisionCode) = False Then GoTo UpdateAccountPostingHeadErr

        'Advance And Imprest........
        SqlStr = ""
        SqlStr = " SELECT TRN.EMP_CODE, SUM(PAYABLEAMOUNT) As AMOUNT, " & vbCrLf & " TYPE " & vbCrLf & " FROM PAY_SAL_TRN TRN, PAY_SALARYHEAD_MST SALHEAD " & vbCrLf & " WHERE " & vbCrLf & " TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND TRN.FYEAR =" & mCurrentFYNo & " " & vbCrLf & " AND TRN.COMPANY_CODE=SALHEAD.COMPANY_CODE" & vbCrLf & " AND TRN.SALHEADCODE=SALHEAD.CODE " & vbCrLf & " AND TYPE IN (" & ConAdvance & ", " & ConImprest & ")" & vbCrLf & " AND TO_CHAR(TRN.SAL_DATE,'YYYYMM')=" & pYM & "" & vbCrLf & " AND CATEGORY='" & mCategory & "'" & vbCrLf & " AND ISARREAR='" & mArrear & "' " & vbCrLf & " AND DIV_CODE=" & mDivisionCode & " HAVING SUM(PAYABLEAMOUNT)<>0" & vbCrLf & " GROUP BY TRN.EMP_CODE, TYPE "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAcctPost, ADODB.LockTypeEnum.adLockOptimistic)

        If RsAcctPost.EOF = False Then
            Do While Not RsAcctPost.EOF
                mDC = "Cr"
                mAmount = IIf(IsDBNull(RsAcctPost.Fields("Amount").Value), 0, RsAcctPost.Fields("Amount").Value)
                mEmpCode = IIf(IsDBNull(RsAcctPost.Fields("EMP_CODE").Value), "-1", RsAcctPost.Fields("EMP_CODE").Value)

                If RsAcctPost.Fields("Type").Value = ConAdvance Then
                    mSqlCond = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND HEADTYPE='L'"
                ElseIf RsAcctPost.Fields("Type").Value = ConImprest Then
                    mSqlCond = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND HEADTYPE='I'"
                End If

                If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , mSqlCond) = True Then
                    mAccountCode = MasterNo
                Else
                    mAccountCode = "-1"
                End If

                If mAccountCode <> "-1" Then
                    If UpdateTMSal(pYM, mAccountCode, mDC, mAmount, mCategory, mSALType, mArrear, mBookType, mBookSubType, "", mDivisionCode) = False Then GoTo UpdateAccountPostingHeadErr
                End If
                RsAcctPost.MoveNext()
            Loop
        End If

        'Bank Loan........
        SqlStr = ""
        SqlStr = " SELECT EMP.ADV_ACCOUNT_CODE, SUM(PAYABLEAMOUNT) As AMOUNT " & vbCrLf & " FROM PAY_SAL_TRN TRN, PAY_SALARYHEAD_MST SALHEAD, PAY_EMPLOYEE_MST EMP " & vbCrLf & " WHERE " & vbCrLf & " TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND TRN.FYEAR =" & mCurrentFYNo & " " & vbCrLf & " AND TRN.COMPANY_CODE=EMP.COMPANY_CODE" & vbCrLf & " AND TRN.EMP_CODE=EMP.EMP_CODE " & vbCrLf & " AND TRN.COMPANY_CODE=SALHEAD.COMPANY_CODE" & vbCrLf & " AND TRN.SALHEADCODE=SALHEAD.CODE " & vbCrLf & " AND TYPE = " & ConLoan & "" & vbCrLf & " AND TO_CHAR(TRN.SAL_DATE,'YYYYMM')=" & pYM & "" & vbCrLf & " AND CATEGORY='" & mCategory & "'" & vbCrLf & " AND TRN.DIV_CODE=" & mDivisionCode & "" & vbCrLf & " AND ISARREAR='" & mArrear & "' HAVING SUM(PAYABLEAMOUNT)<>0" & vbCrLf & " GROUP BY EMP.ADV_ACCOUNT_CODE "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAcctPost, ADODB.LockTypeEnum.adLockOptimistic)

        If RsAcctPost.EOF = False Then
            Do While Not RsAcctPost.EOF
                mDC = "Cr"
                mAmount = IIf(IsDBNull(RsAcctPost.Fields("Amount").Value), 0, RsAcctPost.Fields("Amount").Value)
                mAccountCode = IIf(IsDBNull(RsAcctPost.Fields("ADV_ACCOUNT_CODE").Value), "-1", RsAcctPost.Fields("ADV_ACCOUNT_CODE").Value)
                mSqlCond = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

                If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , mSqlCond) = True Then
                    mAccountCode = MasterNo
                Else
                    mAccountCode = "-1"
                End If

                If mAccountCode <> "-1" Then
                    If UpdateTMSal(pYM, mAccountCode, mDC, mAmount, mCategory, mSALType, mArrear, mBookType, mBookSubType, "", mDivisionCode) = False Then GoTo UpdateAccountPostingHeadErr
                End If
                RsAcctPost.MoveNext()
            Loop
        End If


        ''PF ADMIN Charges...

        SqlStr = ""
        SqlStr = " SELECT TRN.SALHEADCODE, SUM(PAYABLEAMOUNT) As AMOUNT, " & vbCrLf & " SALHEAD.ADDDEDUCT,SALHEAD.ACCOUNTCODEPOST,SALHEAD.DC,TYPE " & vbCrLf & " FROM PAY_SAL_TRN TRN, PAY_SALARYHEAD_MST SALHEAD " & vbCrLf & " WHERE " & vbCrLf & " TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND TRN.FYEAR =" & mCurrentFYNo & " " & vbCrLf & " AND TRN.COMPANY_CODE=SALHEAD.COMPANY_CODE" & vbCrLf & " AND TRN.SALHEADCODE=SALHEAD.CODE " & vbCrLf & " AND TO_CHAR(TRN.SAL_DATE,'YYYYMM')=" & pYM & "" & vbCrLf & " AND CATEGORY='" & mCategory & "'" & vbCrLf & " AND ISARREAR='" & mArrear & "' AND TYPE='" & ConPF & "'" & vbCrLf & " AND DIV_CODE=" & mDivisionCode & "" & vbCrLf & " GROUP BY TRN.SALHEADCODE," & vbCrLf & " SALHEAD.ADDDEDUCT,SALHEAD.ACCOUNTCODEPOST,SALHEAD.DC,TYPE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAcctPost, ADODB.LockTypeEnum.adLockOptimistic)

        If RsAcctPost.EOF = False Then
            Do While Not RsAcctPost.EOF
                ''mBasicSalary
                mAmount = GetPFGrossAmount(pYM, mCategory, mArrear, mDivisionCode) '' IIf(IsNull(RsAcctPost!Amount), 0, RsAcctPost!Amount) * 100 / 12

                '            If RsCompany.Fields("COMPANY_CODE").Value = 16 Or RsCompany.Fields("FYEAR").Value >= 2015 Then
                '                mAmount = Round(mAmount * (RsCompany!PFADMINPER) / 100, 0)
                '            Else
                '                mAmount = mAmount * (RsCompany!PFADMINPER + RsCompany!PFADMINPER_22) / 100
                '            End If



                If RsCompany.Fields("COMPANY_CODE").Value = 16 Or RsCompany.Fields("FYEAR").Value >= 2015 Then
                    mPFAdminCharge = CStr(GetPFAdminCharge(pYM, mCategory, mArrear, mDivisionCode))
                    '                mPFAdminCharge = IIf(mPFAdminCharge = Int(mPFAdminCharge), mPFAdminCharge, Int(mPFAdminCharge) + 1) + IIf(RsCompany.Fields("COMPANY_CODE").Value = 16, 1, 0)
                    mAmount = mAmount + CDbl(mPFAdminCharge)
                End If

                If RsCompany.Fields("COMPANY_CODE").Value = 16 Then
                    mParticulars = "PF"
                Else
                    mParticulars = "Admin Charges on PF : " & VB6.Format(RsCompany.Fields("PFADMINPER").Value + RsCompany.Fields("PFADMINPER_22").Value, "0.00") & " of Basic Salary"
                End If


                ''EDLI Charges...
                If RsCompany.Fields("COMPANY_CODE").Value = 16 Or RsCompany.Fields("FYEAR").Value >= 2015 Then
                    mAmount2 = GetPensionWages(pYM, mCategory, mArrear, mDivisionCode)
                Else
                    mAmount2 = IIf(IsDBNull(RsAcctPost.Fields("Amount").Value), 0, RsAcctPost.Fields("Amount").Value) * 100 / 12
                End If

                mAmount2 = System.Math.Round(mAmount2 * IIf(IsDBNull(RsCompany.Fields("PFEDLIPER").Value), 0, RsCompany.Fields("PFEDLIPER").Value) / 100, 0)

                If RsCompany.Fields("COMPANY_CODE").Value = 16 Then
                    mParticulars2 = "PF"
                Else
                    mParticulars2 = "Contribution to EDLI : " & VB6.Format(IIf(IsDBNull(RsCompany.Fields("PFEDLIPER").Value), 0, RsCompany.Fields("PFEDLIPER").Value), "0.00") & " of Basic Salary"
                End If

                ''PF EMPLOYER CONTRIBUTION
                mAmount3 = System.Math.Round(IIf(IsDBNull(RsAcctPost.Fields("Amount").Value), 0, RsAcctPost.Fields("Amount").Value), 0)

                If RsCompany.Fields("COMPANY_CODE").Value = 16 Then
                    mParticulars3 = "PF"
                Else
                    mParticulars3 = "Employer Contribution : Equal to Employee Contribution"
                End If

                mDC = "DR"
                mAccountCode = GetCategoryAcctCode(mBookSubType, "P")

                If mAccountCode <> "-1" Then
                    If mAmount <> 0 Then
                        If UpdateTMSal(pYM, mAccountCode, mDC, mAmount, mCategory, mSALType, mArrear, mBookType, mBookSubType, mParticulars, mDivisionCode) = False Then GoTo UpdateAccountPostingHeadErr
                    End If
                    If mAmount2 <> 0 Then
                        If UpdateTMSal(pYM, mAccountCode, mDC, mAmount2, mCategory, mSALType, mArrear, mBookType, mBookSubType, mParticulars2, mDivisionCode) = False Then GoTo UpdateAccountPostingHeadErr
                    End If
                    If mAmount3 <> 0 Then
                        If UpdateTMSal(pYM, mAccountCode, mDC, mAmount3, mCategory, mSALType, mArrear, mBookType, mBookSubType, mParticulars3, mDivisionCode) = False Then GoTo UpdateAccountPostingHeadErr
                    End If
                End If

                mAccountCode = IIf(IsDBNull(RsAcctPost.Fields("ACCOUNTCODEPOST").Value), "-1", RsAcctPost.Fields("ACCOUNTCODEPOST").Value)
                mDC = "CR"
                If mAccountCode <> "-1" Then
                    If mAmount <> 0 Then
                        If UpdateTMSal(pYM, mAccountCode, mDC, mAmount, mCategory, mSALType, mArrear, mBookType, mBookSubType, mParticulars, mDivisionCode) = False Then GoTo UpdateAccountPostingHeadErr
                    End If
                    If mAmount2 <> 0 Then
                        If UpdateTMSal(pYM, mAccountCode, mDC, mAmount2, mCategory, mSALType, mArrear, mBookType, mBookSubType, mParticulars2, mDivisionCode) = False Then GoTo UpdateAccountPostingHeadErr
                    End If
                    If mAmount3 <> 0 Then
                        If UpdateTMSal(pYM, mAccountCode, mDC, mAmount3, mCategory, mSALType, mArrear, mBookType, mBookSubType, mParticulars3, mDivisionCode) = False Then GoTo UpdateAccountPostingHeadErr
                    End If
                End If
                RsAcctPost.MoveNext()
            Loop
        End If

        ''ESI Charges...

        SqlStr = ""

        SqlStr = " SELECT TRN.SALHEADCODE, ESI.ESIABLEAMT As AMOUNT, " & vbCrLf & " SALHEAD.ACCOUNTCODEPOST " & vbCrLf & " FROM PAY_SAL_TRN TRN, PAY_PFESI_TRN ESI, PAY_SALARYHEAD_MST SALHEAD " & vbCrLf & " WHERE " & vbCrLf & " TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND TRN.FYEAR =" & mCurrentFYNo & " " & vbCrLf & " AND TRN.COMPANY_CODE=ESI.COMPANY_CODE" & vbCrLf & " AND TRN.SAL_DATE=ESI.SAL_DATE " & vbCrLf & " AND TRN.ISARREAR=ESI.ISARREAR " & vbCrLf & " AND TRN.EMP_CODE=ESI.EMP_CODE " & vbCrLf & " AND TRN.COMPANY_CODE=SALHEAD.COMPANY_CODE" & vbCrLf & " AND TRN.SALHEADCODE=SALHEAD.CODE " & vbCrLf & " AND TO_CHAR(TRN.SAL_DATE,'YYYYMM')=" & pYM & "" & vbCrLf & " AND TRN.CATEGORY='" & mCategory & "'" & vbCrLf & " AND DIV_CODE=" & mDivisionCode & "" & vbCrLf & " AND TRN.ISARREAR='" & mArrear & "' AND TYPE='" & ConESI & "' AND ESI.ESIAMT>0" ''& vbCrLf |        & " GROUP BY TRN.SALHEADCODE," & vbCrLf |        & " SALHEAD.ADDDEDUCT,SALHEAD.ACCOUNTCODEPOST,SALHEAD.DC,TYPE"



        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAcctPost, ADODB.LockTypeEnum.adLockOptimistic)

        If RsAcctPost.EOF = False Then
            Do While Not RsAcctPost.EOF
                mAmount = IIf(IsDBNull(RsAcctPost.Fields("Amount").Value), 0, RsAcctPost.Fields("Amount").Value)
                If mESIRate = 0 Then
                    mAmount = 0
                Else
                    '                mAmount = Format(mAmount * IIf(IsNull(RsCompany!EMPLOYERESIPER), 0, RsCompany!EMPLOYERESIPER) / mESIRate, "0.00")
                    mAmount = CDbl(VB6.Format(mAmount * IIf(IsDBNull(RsCompany.Fields("EMPLOYERESIPER").Value), 0, RsCompany.Fields("EMPLOYERESIPER").Value) / 100, "0.00"))
                    If RsCompany.Fields("COMPANY_CODE").Value = 16 Then

                    Else
                        mAmount = Int(mAmount) + IIf(mAmount > Int(mAmount), 1, 0)
                    End If
                End If


                mParticulars = "Employer Contribution : " & VB6.Format(IIf(IsDBNull(RsCompany.Fields("EMPLOYERESIPER").Value), 0, RsCompany.Fields("EMPLOYERESIPER").Value), "0.00") & " of ESI Deduction"


                mDC = "DR"

                mAccountCode = GetCategoryAcctCode(mBookSubType, "E")

                '            If mCategory = "G" Then
                '                mAccountCode = IIf(IsNull(RsCompany!POSTESICONTCR), -1, RsCompany!POSTESICONTCR)
                '            ElseIf mCategory = "P" Then
                '                mAccountCode = IIf(IsNull(RsCompany!POSTESIPRODCONTCR), -1, RsCompany!POSTESIPRODCONTCR)
                '            ElseIf mCategory = "E" Then
                '                mAccountCode = IIf(IsNull(RsCompany!POSTESIEXPORTCONTCR), -1, RsCompany!POSTESIEXPORTCONTCR)
                '            ElseIf mCategory = "R" Then
                '                mAccountCode = IIf(IsNull(RsCompany!POSTESIWCONTCR), -1, RsCompany!POSTESIWCONTCR)
                '            ElseIf mCategory = "S" Then
                '                mAccountCode = IIf(IsNull(RsCompany!POSTESIRNDCONTCR), -1, RsCompany!POSTESIRNDCONTCR)
                '            ElseIf mCategory = "D" Then
                '                mAccountCode = "-1"
                '            ElseIf mCategory = "T" Then
                '                mAccountCode = IIf(IsNull(RsCompany!PostTRNESIContCr), -1, RsCompany!PostTRNESIContCr)
                '            End If

                If mAccountCode <> "-1" Then
                    If mAmount <> 0 Then
                        If UpdateTMSal(pYM, mAccountCode, mDC, mAmount, mCategory, mSALType, mArrear, mBookType, mBookSubType, mParticulars, mDivisionCode) = False Then GoTo UpdateAccountPostingHeadErr
                    End If
                End If

                mAccountCode = IIf(IsDBNull(RsAcctPost.Fields("ACCOUNTCODEPOST").Value), "-1", RsAcctPost.Fields("ACCOUNTCODEPOST").Value)
                mDC = "CR"
                If mAccountCode <> "-1" Then
                    If mAmount <> 0 Then
                        If UpdateTMSal(pYM, mAccountCode, mDC, mAmount, mCategory, mSALType, mArrear, mBookType, mBookSubType, mParticulars, mDivisionCode) = False Then GoTo UpdateAccountPostingHeadErr
                    End If
                End If
                RsAcctPost.MoveNext()
            Loop
        End If

        ''Welfare Charges...

        SqlStr = ""
        SqlStr = " SELECT TRN.SALHEADCODE, SUM(PAYABLEAMOUNT) As AMOUNT, " & vbCrLf & " SALHEAD.ADDDEDUCT,SALHEAD.ACCOUNTCODEPOST,SALHEAD.DC,TYPE " & vbCrLf & " FROM PAY_SAL_TRN TRN, PAY_SALARYHEAD_MST SALHEAD " & vbCrLf & " WHERE " & vbCrLf & " TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND TRN.FYEAR =" & mCurrentFYNo & " " & vbCrLf & " AND TRN.COMPANY_CODE=SALHEAD.COMPANY_CODE" & vbCrLf & " AND TRN.SALHEADCODE=SALHEAD.CODE " & vbCrLf & " AND TO_CHAR(TRN.SAL_DATE,'YYYYMM')=" & pYM & "" & vbCrLf & " AND CATEGORY='" & mCategory & "'" & vbCrLf & " AND DIV_CODE=" & mDivisionCode & "" & vbCrLf & " AND ISARREAR='" & mArrear & "' AND TYPE='" & ConWelfare & "'" & vbCrLf & " GROUP BY TRN.SALHEADCODE," & vbCrLf & " SALHEAD.ADDDEDUCT,SALHEAD.ACCOUNTCODEPOST,SALHEAD.DC,TYPE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAcctPost, ADODB.LockTypeEnum.adLockOptimistic)

        If RsAcctPost.EOF = False Then
            Do While Not RsAcctPost.EOF
                mAmount = IIf(IsDBNull(RsAcctPost.Fields("Amount").Value), 0, RsAcctPost.Fields("Amount").Value)
                '            If mESIRate = 0 Then
                '                mAmount = 0
                '            Else
                mAmount = mAmount * IIf(IsDBNull(RsCompany.Fields("WELFAREPER").Value), 0, RsCompany.Fields("WELFAREPER").Value) / 100
                '            End If
                mParticulars = "Employer Contribution:Double of Employee deduction"

                mDC = "DR"

                mAccountCode = GetCategoryAcctCode(mBookSubType, "W")

                '            If mCategory = "G" Then
                '                mAccountCode = IIf(IsNull(RsCompany!WELFARE_GS), -1, RsCompany!WELFARE_GS)
                '            ElseIf mCategory = "P" Then
                '                mAccountCode = IIf(IsNull(RsCompany!WELFARE_PS), -1, RsCompany!WELFARE_PS)
                '            ElseIf mCategory = "E" Then
                '                mAccountCode = IIf(IsNull(RsCompany!WELFARE_ES), -1, RsCompany!WELFARE_ES)
                '            ElseIf mCategory = "R" Then
                '                mAccountCode = IIf(IsNull(RsCompany!WELFARE_WS), -1, RsCompany!WELFARE_WS)
                '            ElseIf mCategory = "S" Then
                '                mAccountCode = IIf(IsNull(RsCompany!WELFARE_RS), -1, RsCompany!WELFARE_RS)
                '            ElseIf mCategory = "D" Then
                '                mAccountCode = IIf(IsNull(RsCompany!WELFARE_D), -1, RsCompany!WELFARE_D)
                '            ElseIf mCategory = "T" Then
                '                mAccountCode = IIf(IsNull(RsCompany!Welfare_TRN), -1, RsCompany!Welfare_TRN)
                '            End If

                If mAccountCode <> "-1" Then
                    If mAmount <> 0 Then
                        If UpdateTMSal(pYM, mAccountCode, mDC, mAmount, mCategory, mSALType, mArrear, mBookType, mBookSubType, mParticulars, mDivisionCode) = False Then GoTo UpdateAccountPostingHeadErr
                    End If
                End If

                mAccountCode = IIf(IsDBNull(RsAcctPost.Fields("ACCOUNTCODEPOST").Value), "-1", RsAcctPost.Fields("ACCOUNTCODEPOST").Value)
                mDC = "CR"
                If mAccountCode <> "-1" Then
                    If mAmount <> 0 Then
                        If UpdateTMSal(pYM, mAccountCode, mDC, mAmount, mCategory, mSALType, mArrear, mBookType, mBookSubType, "", mDivisionCode) = False Then GoTo UpdateAccountPostingHeadErr
                    End If
                End If
                RsAcctPost.MoveNext()
            Loop
        End If


        UpdateAccountPostingHead = True
        Exit Function
UpdateAccountPostingHeadErr:
        UpdateAccountPostingHead = False
        MsgInformation(Err.Description)
        '    Resume
    End Function

    Private Function GetPensionWages(ByRef pYM As Integer, ByRef mCategory As String, ByRef mArrear As String, ByRef mDivisionCode As Double) As Double

        On Error GoTo UpdateAccountPostingHeadErr
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        GetPensionWages = 0
        SqlStr = ""
        SqlStr = " SELECT  SUM(PENSIONWAGES) As AMOUNT " & vbCrLf & " FROM PAY_PFESI_TRN " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR =" & mCurrentFYNo & " " & vbCrLf & " AND TO_CHAR(SAL_DATE,'YYYYMM')=" & pYM & "" & vbCrLf & " AND ISARREAR='" & mArrear & "'" & vbCrLf & " AND EMP_CODE IN ("

        SqlStr = SqlStr & vbCrLf & " SELECT DISTINCT EMP_CODE " & vbCrLf & " FROM PAY_SAL_TRN " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR =" & mCurrentFYNo & " " & vbCrLf & " AND TO_CHAR(SAL_DATE,'YYYYMM')=" & pYM & "" & vbCrLf & " AND CATEGORY='" & mCategory & "'" & vbCrLf & " AND ISARREAR='" & mArrear & "'" & vbCrLf & " AND DIV_CODE=" & mDivisionCode & ")"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            GetPensionWages = IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
        End If

        Exit Function
UpdateAccountPostingHeadErr:
        GetPensionWages = 0
        MsgInformation(Err.Description)
        '    Resume
    End Function

    Private Function GetPFAdminCharge(ByRef pYM As Integer, ByRef mCategory As String, ByRef mArrear As String, ByRef mDivisionCode As Double) As Double

        On Error GoTo UpdateAccountPostingHeadErr
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mAddAmount As Double
        Dim mPFAmin22Per As Double

        GetPFAdminCharge = 0


        mPFAmin22Per = IIf(IsDBNull(RsCompany.Fields("PFADMINPER_22").Value), 0.01, RsCompany.Fields("PFADMINPER_22").Value)

        If RsCompany.Fields("COMPANY_CODE").Value = 16 Then
            mAddAmount = IIf(mPFAmin22Per = 0, 0, 1)
        Else
            mAddAmount = 0
        End If

        SqlStr = ""
        SqlStr = " SELECT  CEIL(PFABLEAMT * " & mPFAmin22Per & " * 0.01) + " & mAddAmount & " AS AMOUNT, EMP_CODE " & vbCrLf & " FROM PAY_PFESI_TRN " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR =" & mCurrentFYNo & " " & vbCrLf & " AND TO_CHAR(SAL_DATE,'YYYYMM')=" & pYM & "" & vbCrLf & " AND ISARREAR='" & mArrear & "'" & vbCrLf & " AND EMP_CODE IN ("

        SqlStr = SqlStr & vbCrLf & " SELECT DISTINCT EMP_CODE " & vbCrLf & " FROM PAY_SAL_TRN " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR =" & mCurrentFYNo & " " & vbCrLf & " AND TO_CHAR(SAL_DATE,'YYYYMM')=" & pYM & "" & vbCrLf & " AND CATEGORY='" & mCategory & "'" & vbCrLf & " AND ISARREAR='" & mArrear & "'" & vbCrLf & " AND DIV_CODE=" & mDivisionCode & ")"

        '    SqlStr = SqlStr & vbCrLf & " GROUP BY EMP_CODE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                GetPFAdminCharge = GetPFAdminCharge + IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
                RsTemp.MoveNext()
            Loop
        End If


        Exit Function
UpdateAccountPostingHeadErr:
        GetPFAdminCharge = 0
        MsgInformation(Err.Description)
        '    Resume
    End Function
    Private Function GetPFGrossAmount(ByRef pYM As Integer, ByRef mCategory As String, ByRef mArrear As String, ByRef mDivisionCode As Double) As Double

        On Error GoTo UpdateAccountPostingHeadErr
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mAddAmount As Double
        Dim mPFAminPer As Double

        GetPFGrossAmount = 0
        'RsCompany!PFADMINPER

        mPFAminPer = IIf(IsDBNull(RsCompany.Fields("PFADMINPER").Value), 0.01, RsCompany.Fields("PFADMINPER").Value)

        SqlStr = ""
        SqlStr = " SELECT  ROUND(PFABLEAMT * " & mPFAminPer & " * 0.01,0) AS AMOUNT, EMP_CODE " & vbCrLf & " FROM PAY_PFESI_TRN " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR =" & mCurrentFYNo & " " & vbCrLf & " AND TO_CHAR(SAL_DATE,'YYYYMM')=" & pYM & "" & vbCrLf & " AND ISARREAR='" & mArrear & "'" & vbCrLf & " AND EMP_CODE IN ("

        SqlStr = SqlStr & vbCrLf & " SELECT DISTINCT EMP_CODE " & vbCrLf & " FROM PAY_SAL_TRN " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR =" & mCurrentFYNo & " " & vbCrLf & " AND TO_CHAR(SAL_DATE,'YYYYMM')=" & pYM & "" & vbCrLf & " AND CATEGORY='" & mCategory & "'" & vbCrLf & " AND ISARREAR='" & mArrear & "'" & vbCrLf & " AND DIV_CODE=" & mDivisionCode & ")"

        '    SqlStr = SqlStr & vbCrLf & " GROUP BY EMP_CODE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                GetPFGrossAmount = GetPFGrossAmount + IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
                RsTemp.MoveNext()
            Loop
        End If


        Exit Function
UpdateAccountPostingHeadErr:
        GetPFGrossAmount = 0
        MsgInformation(Err.Description)
        '    Resume
    End Function
    Private Function UpdateOTAccountPostingHead(ByRef pYM As Integer, ByRef mArrear As String, ByRef mCategory As String, ByRef mSALType As String, ByRef mBookType As String, ByRef mBookSubType As String, ByRef mDivisionCode As Double) As Boolean


        On Error GoTo UpdateAccountPostingHeadErr
        Dim SqlStr As String = ""
        Dim RsAcctPost As ADODB.Recordset
        Dim mAddDeduct As Integer
        Dim mAccountCode As String
        Dim mESIAccountCode As String
        Dim mDC As String
        Dim mAmount As Double
        Dim mPayableAmount As Double
        Dim mGrossPayableAmount As Double
        Dim mEmpCode As String
        Dim mSqlCond As String
        Dim mESIAmount As Double
        Dim mESIParticulars As String

        ''FYEAR=" & mCurrentFYNo & "

        SqlStr = " Delete from FIN_TMSal_TRN " & vbCrLf & " Where " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND " & vbCrLf & " YM=" & pYM & "" & vbCrLf & " AND CATEGORY='" & mCategory & "'" & vbCrLf & " AND SALTYPE='" & mSALType & "'" & vbCrLf & " AND ISARREAR='" & mArrear & "' " & vbCrLf & " AND BookType='" & mBookType & "'" & vbCrLf & " AND BookSubType='" & mBookSubType & "'" & vbCrLf & " AND DIV_CODE=" & mDivisionCode & ""

        PubDBCn.Execute(SqlStr)

        'Salary Head........
        SqlStr = ""

        SqlStr = " SELECT SUM(OT_AMOUNT) As OT_AMOUNT, " & vbCrLf & " SUM(ESIC_AMOUNT) As ESIC_AMOUNT, " & vbCrLf & " SUM(NET_AMOUNT) As NET_AMOUNT " & vbCrLf & " FROM PAY_MONTHLY_OT_TRN TRN, PAY_EMPLOYEE_MST EMP " & vbCrLf & " WHERE " & vbCrLf & " TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND TRN.COMPANY_CODE=EMP.COMPANY_CODE " & vbCrLf & " AND TRN.EMP_CODE=EMP.EMP_CODE " & vbCrLf & " AND TO_CHAR(TRN.OT_DATE,'YYYYMM')=" & pYM & "" & vbCrLf & " AND EMP_CATG='" & mCategory & "'" & vbCrLf & " AND IS_ARREAR='" & mArrear & "' AND EMP.DIV_CODE=" & mDivisionCode & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAcctPost, ADODB.LockTypeEnum.adLockOptimistic)

        If RsAcctPost.EOF = False Then
            'PAYABLE OT
            mDC = "DR"
            mAmount = IIf(IsDBNull(RsAcctPost.Fields("OT_AMOUNT").Value), 0, RsAcctPost.Fields("OT_AMOUNT").Value)

            mAccountCode = GetCategoryAcctCode(mBookSubType, "ID")

            '        If mCategory = "G" Then
            '            mAccountCode = IIf(IsNull(RsCompany!POSTSTAFFINC_DEBITCODE), -1, RsCompany!POSTSTAFFINC_DEBITCODE)
            '        ElseIf mCategory = "P" Then
            '            mAccountCode = IIf(IsNull(RsCompany!POSTPRODINC_DEBITCODE), -1, RsCompany!POSTPRODINC_DEBITCODE)
            '        ElseIf mCategory = "E" Then
            '            mAccountCode = IIf(IsNull(RsCompany!POSTEXPORTINC_DEBITCODE), -1, RsCompany!POSTEXPORTINC_DEBITCODE)
            '        ElseIf mCategory = "R" Then
            '            mAccountCode = IIf(IsNull(RsCompany!POSTWINC_DEBITCODE), -1, RsCompany!POSTWINC_DEBITCODE)
            '        ElseIf mCategory = "S" Then
            '            mAccountCode = IIf(IsNull(RsCompany!POSTRNDINC_DEBITCODE), -1, RsCompany!POSTRNDINC_DEBITCODE)
            '        ElseIf mCategory = "D" Then
            '            mAccountCode = IIf(IsNull(RsCompany!POSTDIRECTORINC_DEBITCODE), -1, RsCompany!POSTDIRECTORINC_DEBITCODE)
            '        ElseIf mCategory = "T" Then
            '            mAccountCode = IIf(IsNull(RsCompany!POSTTRNINC_DEBITCODE), -1, RsCompany!POSTTRNINC_DEBITCODE)
            '        End If
            If UpdateTMSal(pYM, mAccountCode, mDC, mAmount, mCategory, mSALType, mArrear, mBookType, mBookSubType, "", mDivisionCode) = False Then GoTo UpdateAccountPostingHeadErr



            'ESI

            mAmount = IIf(IsDBNull(RsAcctPost.Fields("ESIC_AMOUNT").Value), 0, RsAcctPost.Fields("ESIC_AMOUNT").Value)
            mESIAmount = IIf(IsDBNull(RsAcctPost.Fields("ESIC_AMOUNT").Value), 0, RsAcctPost.Fields("ESIC_AMOUNT").Value)

            mDC = "CR"
            mAmount = CDbl(VB6.Format(mESIAmount, "0.00"))
            mESIParticulars = "Employee Contribution : "
            mESIAccountCode = GetCategoryAcctCode(mBookSubType, "E")

            '        If mCategory = "G" Then
            '            mESIAccountCode = IIf(IsNull(RsCompany!POSTESICONTCR), -1, RsCompany!POSTESICONTCR)
            '        ElseIf mCategory = "P" Then
            '            mESIAccountCode = IIf(IsNull(RsCompany!POSTESIPRODCONTCR), -1, RsCompany!POSTESIPRODCONTCR)
            '        ElseIf mCategory = "E" Then
            '            mESIAccountCode = IIf(IsNull(RsCompany!POSTESIEXPORTCONTCR), -1, RsCompany!POSTESIEXPORTCONTCR)
            '        ElseIf mCategory = "R" Then
            '            mESIAccountCode = IIf(IsNull(RsCompany!POSTESIWCONTCR), -1, RsCompany!POSTESIWCONTCR)
            '        ElseIf mCategory = "S" Then
            '            mESIAccountCode = IIf(IsNull(RsCompany!POSTESIRNDCONTCR), -1, RsCompany!POSTESIRNDCONTCR)
            '        ElseIf mCategory = "D" Then
            '            mESIAccountCode = "-1"
            '        ElseIf mCategory = "T" Then
            '            mESIAccountCode = IIf(IsNull(RsCompany!PostTRNESIContCr), -1, RsCompany!PostTRNESIContCr)
            '        End If

            If MainClass.ValidateWithMasterTable(ConESI, "TYPE", "ACCOUNTCODEPOST", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = MasterNo ''IIf(IsNull(RsAcctPost!ACCOUNTCODEPOST), "-1", RsAcctPost!ACCOUNTCODEPOST)
            Else
                mAccountCode = "-1"
            End If

            If UpdateTMSal(pYM, mAccountCode, mDC, mAmount, mCategory, mSALType, mArrear, mBookType, mBookSubType, mESIParticulars, mDivisionCode) = False Then GoTo UpdateAccountPostingHeadErr

            mESIParticulars = "Employer Contribution : " & VB6.Format(IIf(IsDBNull(RsCompany.Fields("EMPLOYERESIPER").Value), 0, RsCompany.Fields("EMPLOYERESIPER").Value), "0.00") & " of ESI Deduction"
            If mAccountCode <> "-1" Then
                If mAmount <> 0 Then
                    mDC = "DR"
                    mAmount = CDbl(VB6.Format((mESIAmount * 100 / 1.75) * CDbl(VB6.Format(IIf(IsDBNull(RsCompany.Fields("EMPLOYERESIPER").Value), 0, RsCompany.Fields("EMPLOYERESIPER").Value), "0.00")) / 100, "0.00"))
                    If UpdateTMSal(pYM, mESIAccountCode, mDC, mAmount, mCategory, mSALType, mArrear, mBookType, mBookSubType, mESIParticulars, mDivisionCode) = False Then GoTo UpdateAccountPostingHeadErr

                    mDC = "CR"
                    mAmount = CDbl(VB6.Format((mESIAmount * 100 / 1.75) * CDbl(VB6.Format(IIf(IsDBNull(RsCompany.Fields("EMPLOYERESIPER").Value), 0, RsCompany.Fields("EMPLOYERESIPER").Value), "0.00")) / 100, "0.00"))
                    If UpdateTMSal(pYM, mAccountCode, mDC, mAmount, mCategory, mSALType, mArrear, mBookType, mBookSubType, mESIParticulars, mDivisionCode) = False Then GoTo UpdateAccountPostingHeadErr
                End If
            End If



            'NET OT
            mDC = "CR"
            mAmount = IIf(IsDBNull(RsAcctPost.Fields("NET_AMOUNT").Value), 0, RsAcctPost.Fields("NET_AMOUNT").Value)

            mAccountCode = GetCategoryAcctCode(mBookSubType, "IC")

            '        If mCategory = "G" Then
            '            mAccountCode = IIf(IsNull(RsCompany!POSTSTAFFINC_CREDITCODE), -1, RsCompany!POSTSTAFFINC_CREDITCODE)
            '        ElseIf mCategory = "P" Then
            '            mAccountCode = IIf(IsNull(RsCompany!POSTPRODINC_CREDITCODE), -1, RsCompany!POSTPRODINC_CREDITCODE)
            '        ElseIf mCategory = "E" Then
            '            mAccountCode = IIf(IsNull(RsCompany!POSTEXPORTINC_CREDITCODE), -1, RsCompany!POSTEXPORTINC_CREDITCODE)
            '        ElseIf mCategory = "R" Then
            '            mAccountCode = IIf(IsNull(RsCompany!POSTWINC_CREDITCODE), -1, RsCompany!POSTWINC_CREDITCODE)
            '        ElseIf mCategory = "S" Then
            '            mAccountCode = IIf(IsNull(RsCompany!POSTRNDINC_CREDITCODE), -1, RsCompany!POSTRNDINC_CREDITCODE)
            '        End If
            If UpdateTMSal(pYM, mAccountCode, mDC, mAmount, mCategory, mSALType, mArrear, mBookType, mBookSubType, "", mDivisionCode) = False Then GoTo UpdateAccountPostingHeadErr

        End If

        'Advance
        SqlStr = ""

        SqlStr = " SELECT TRN.EMP_CODE, SUM(ADV_AMOUNT) As ADV_AMOUNT " & vbCrLf & " FROM PAY_MONTHLY_OT_TRN TRN, PAY_EMPLOYEE_MST EMP " & vbCrLf & " WHERE " & vbCrLf & " TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND TRN.COMPANY_CODE=EMP.COMPANY_CODE " & vbCrLf & " AND TRN.EMP_CODE=EMP.EMP_CODE " & vbCrLf & " AND TO_CHAR(TRN.OT_DATE,'YYYYMM')=" & pYM & "" & vbCrLf & " AND EMP_CATG='" & mCategory & "'" & vbCrLf & " AND IS_ARREAR='" & mArrear & "'" & vbCrLf & " AND DIV_CODE=" & mDivisionCode & "" & vbCrLf & " HAVING SUM(ADV_AMOUNT)<>0" & vbCrLf & " GROUP BY TRN.EMP_CODE "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAcctPost, ADODB.LockTypeEnum.adLockOptimistic)

        If RsAcctPost.EOF = False Then
            Do While Not RsAcctPost.EOF
                mDC = "Cr"
                mAmount = IIf(IsDBNull(RsAcctPost.Fields("ADV_AMOUNT").Value), 0, RsAcctPost.Fields("ADV_AMOUNT").Value)
                mEmpCode = IIf(IsDBNull(RsAcctPost.Fields("EMP_CODE").Value), "-1", RsAcctPost.Fields("EMP_CODE").Value)

                mSqlCond = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND HEADTYPE='L'"

                If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , mSqlCond) = True Then
                    mAccountCode = MasterNo
                Else
                    mAccountCode = "-1"
                End If

                If mAccountCode <> "-1" Then
                    If UpdateTMSal(pYM, mAccountCode, mDC, mAmount, mCategory, mSALType, mArrear, mBookType, mBookSubType, "", mDivisionCode) = False Then GoTo UpdateAccountPostingHeadErr
                End If
                RsAcctPost.MoveNext()
            Loop
        End If

        UpdateOTAccountPostingHead = True
        Exit Function
UpdateAccountPostingHeadErr:
        UpdateOTAccountPostingHead = False
        MsgInformation(Err.Description)
        '    Resume
    End Function
    Private Function UpdatePFESIPostingHead(ByRef pYM As Integer, ByRef mArrear As String, ByRef mBookType As String, ByRef mBookSubType As String) As Boolean
        'On Error GoTo UpdatePFESIPostingHeadErr
        'Dim SqlStr As String=""=""
        'Dim RsAcctPost As Recordset
        '
        'Dim mProdStaff As String
        '
        'Dim mPFDrCode As Long
        'Dim mPFCrCode As Long
        'Dim mESIDrCode As Long
        'Dim mESICrCode As Long
        '
        'Dim mPFAmount As Double
        'Dim mESIAmount As Double
        '
        '
        '    SqlStr = " Delete from TMSAL " & vbCrLf _
        ''        & " Where " & vbCrLf _
        ''        & " Companycode=" & RsCompany!CompanyCode & " And " & vbCrLf _
        ''        & " BranchCode=" & RsCompany!BranchCode & " And " & vbCrLf _
        ''        & " FYNo =" & RsCompany!FYNO & " And " & vbCrLf _
        ''        & " YM=" & pYM & " AND ISARREAR='" & mArrear & "' AND " & vbCrLf _
        ''        & " BookType='" & mBookType & "' AND BookSubType='" & mBookSubType & "'"
        '    PubDBCn.Execute SqlStr
        '
        '    SqlStr = ""
        '    SqlStr = " Select SUM(PFABLEAMT) AS PFABLEAMT,SUM(PFAMT) As PFAMT,SUM(ESIAMT) As ESIAMT, " & vbCrLf _
        ''        & " SUM(ESIABLEAMT) as ESIABLEAMT from PFESITRN " & vbCrLf _
        ''        & " Where " & vbCrLf _
        ''        & " Companycode=" & RsCompany!CompanyCode & " And " & vbCrLf _
        ''        & " FYNo =" & RsCompany!FYNO & " And " & vbCrLf _
        ''        & " YM=" & pYM & " AND ISARREAR='" & mArrear & "'"
        '
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsAcctPost, adLockOptimistic
        '
        '    If RsAcctPost.EOF = False Then
        '        mPFAmount = IIf(IsNull(RsAcctPost!PFAMT), 0, RsAcctPost!PFAMT) + (IIf(IsNull(RsAcctPost!PFABLEAMT), 0, RsAcctPost!PFABLEAMT) * 1.61 / 100)
        '        mESIAmount = IIf(IsNull(RsAcctPost!ESIABLEAMT), 0, RsAcctPost!ESIABLEAMT) * 4.75 / 100      'IIf(IsNull(RsAcctPost!ESIAMT), 0, RsAcctPost!ESIAMT)
        '
        '        mPFDrCode = IIf(IsNull(RsCompany!POSTPFCONTDR), -1, RsCompany!POSTPFCONTDR)
        '        mPFCrCode = IIf(IsNull(RsCompany!POSTPFCONTCR), -1, RsCompany!POSTPFCONTCR)
        '        mESIDrCode = IIf(IsNull(RsCompany!POSTESICONTDR), -1, RsCompany!POSTESICONTDR)
        '        mESICrCode = IIf(IsNull(RsCompany!POSTESICONTCR), -1, RsCompany!POSTESICONTCR)
        '
        '
        '        If mPFDrCode <> -1 Then
        '            If UpdateTMSal(pYM, mPFDrCode, "Dr", mPFAmount, mArrear, mBookType, mBookSubType) = False Then GoTo UpdatePFESIPostingHeadErr
        '        End If
        '
        '        If mPFCrCode <> -1 Then
        '            If UpdateTMSal(pYM, mPFCrCode, "Cr", mPFAmount, mArrear, mBookType, mBookSubType) = False Then GoTo UpdatePFESIPostingHeadErr
        '        End If
        '
        '        If mESIDrCode <> -1 Then
        '            If UpdateTMSal(pYM, mESIDrCode, "Dr", mESIAmount, mArrear, mBookType, mBookSubType) = False Then GoTo UpdatePFESIPostingHeadErr
        '        End If
        '
        '        If mESICrCode <> -1 Then
        '            If UpdateTMSal(pYM, mESICrCode, "Cr", mESIAmount, mArrear, mBookType, mBookSubType) = False Then GoTo UpdatePFESIPostingHeadErr
        '        End If
        '
        '    End If
        '    UpdatePFESIPostingHead = True
        '    Exit Function
        'UpdatePFESIPostingHeadErr:
        'MsgBox err.Description
        '    UpdatePFESIPostingHead = False
    End Function
    Private Function UpdateTMSal(ByRef pYM As Integer, ByRef mAccountCode As String, ByRef mDC As String, ByRef mAmount As Double, ByRef mCategory As String, ByRef mSALType As String, ByRef mArrear As String, ByRef mBookType As String, ByRef mBookSubType As String, ByRef mParticulars As String, ByRef mDivisionCode As Double) As Boolean

        On Error GoTo UpdateTMSalErr
        Dim SqlStr As String = ""
        Dim mFYear As Integer
        Dim mSalDate As String

        '    mSalDate = "01/" & Right(pYM, 2) & "/" & Left(pYM, 4)
        '
        '    mFYear = GetCurrentFYNo(PubDBCn, Format(mSalDate, "DD/MM/YYYY"))

        If mAccountCode <> "-1" And mAmount <> 0 Then
            SqlStr = " INSERT INTO FIN_TMSal_TRN ( " & vbCrLf & " COMPANY_CODE, FYEAR, YM, " & vbCrLf & " ACCOUNTCODE, DC, AMOUNT, " & vbCrLf & " CATEGORY, SALTYPE, ISARREAR, " & vbCrLf & " BOOKTYPE, BOOKSUBTYPE,PARTICULARS,DIV_CODE  " & vbCrLf & " ) Values (" & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & mCurrentFYNo & ", " & pYM & "," & vbCrLf & " '" & mAccountCode & "', '" & UCase(mDC) & "', " & mAmount & ", " & vbCrLf & " '" & mCategory & "', '" & mSALType & "', '" & mArrear & "'," & vbCrLf & " '" & mBookType & "','" & mBookSubType & "', '" & MainClass.AllowSingleQuote(mParticulars) & "'," & mDivisionCode & ")"

            PubDBCn.Execute(SqlStr)
        End If

        UpdateTMSal = True

        Exit Function
UpdateTMSalErr:
        MsgBox(Err.Description)
        UpdateTMSal = False
    End Function

    Private Function GetMonthlyVarAmount(ByRef xCode As String, ByRef xSalHeadCode As Integer) As Double

        On Error GoTo ErrPart
        Dim RsCalcAddDays As ADODB.Recordset
        Dim SqlStr As String = ""

        GetMonthlyVarAmount = 0
        SqlStr = "SELECT SUM(AMOUNT) As AMOUNT1 FROM PAY_MONTHLY_TRN WHERE " & vbCrLf _
            & " Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND PAYYEAR=" & Year(CDate(lblNewDate.Text)) & " " & vbCrLf _
            & " AND EMP_CODE= '" & xCode & "'" & vbCrLf _
            & " AND ADD_DEDUCTCODE= " & xSalHeadCode & " AND SAL_FLAG='S'" & vbCrLf _
            & " AND TO_CHAR(Sal_MONTH,'MON-YYYY')=TO_CHAR('" & UCase(VB6.Format(lblNewDate.Text, "MMM-YYYY")) & "')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCalcAddDays, ADODB.LockTypeEnum.adLockOptimistic)

        If RsCalcAddDays.EOF = False Then
            GetMonthlyVarAmount = IIf(IsDBNull(RsCalcAddDays.Fields("AMOUNT1").Value), 0, RsCalcAddDays.Fields("AMOUNT1").Value)
        End If
        Exit Function
ErrPart:
        GetMonthlyVarAmount = 0
    End Function

    Private Sub cmdAdjustLeave_Click(sender As Object, e As EventArgs) Handles cmdAdjustLeave.Click
        On Error GoTo ErrPart
        Dim CntRow As Long
        Dim mAmount As Double
        Dim mGrossSalary As Double
        Dim mMonthDays As Long
        Dim mDeductedDays As Double
        Dim mEmpCode As String
        Dim mSalaryMonth As String
        Dim mSalaryStartMonth As String

        Dim mSqlstr As String
        Dim RsTemp As ADODB.Recordset
        Dim mAttnFlag As Long
        Dim mBalanceAttn As Double
        Dim mAttnDate As String
        Dim mEmpDOJ As String
        Dim mDOL As String
        Dim mTotalWop_Absent, mWOP, mAbsent As Double
        Dim pSalaryType As String
        Dim mWDays As Double
        Dim mSalaryTable As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        mMonthDays = MainClass.LastDay(Month(txtMonth.Text), Year(txtMonth.Text))
        mSalaryMonth = VB6.Format(mMonthDays & "/" & VB6.Format(txtMonth.Text, "MM/YYYY"), "DD/MM/YYYY")

        mSalaryStartMonth = VB6.Format("01/" & VB6.Format(txtMonth.Text, "MM/YYYY"), "DD/MM/YYYY")

        mSalaryTable = "PAY_DUMMYACTUAL_SAL_TRN"     ''IIf(pSalaryType = "F", "PAY_SAL_TRN", "PAY_ACTUAL_SAL_TRN")

        mSqlstr = "Select COUNT(1) AS CNTREC From " & mSalaryTable _
                    & " TRN, PAY_EMPLOYEE_MST EMP " & vbCrLf _
                    & " WHERE TRN.COMPANY_CODE=EMP.COMPANY_CODE" & vbCrLf _
                    & " AND TRN.EMP_CODE=EMP.EMP_CODE" & vbCrLf _
                    & " AND EMP.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND TRN.ISARREAR IN ('Y','N') " & vbCrLf _
                    & " AND TRN.SAL_DATE=TO_DATE('" & VB6.Format(mSalaryMonth, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " AND CATEGORY<>'C'"

        'If lblEmpType.Text = "D" Then
        'Else
        '    If Trim(lblEmpType.Text) = "S" Then
        '        mSqlstr = mSqlstr & vbCrLf & " AND EMP.EMP_CAT_TYPE='1'"
        '    Else
        '        mSqlstr = mSqlstr & vbCrLf & " AND EMP.EMP_CAT_TYPE='2'"
        '    End If
        'End If

        MainClass.UOpenRecordSet(mSqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
        If RsTemp.EOF = False Then
            If RsTemp.Fields("CNTREC").Value > 0 Then
                MsgInformation("Salary Already Process, so you cann't be unprocess Leave.")
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                Exit Sub
            End If
        End If

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()




        mSqlstr = " UPDATE PAY_ATTN_MST SET EXTRA_LEAVE='N', EXTRA_LEAVE_2='N' " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND ATTN_DATE>=TO_DATE('" & VB6.Format(mSalaryStartMonth, "DD/MMM/YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND ATTN_DATE<=TO_DATE('" & VB6.Format(mSalaryMonth, "DD/MMM/YYYY") & "','DD-MON-YYYY')"

        If OptParti.Checked = True Then
            mSqlstr = mSqlstr & vbCrLf & " AND EMP_CODE='" & TxtCardNo.Text & "'"
        End If


        PubDBCn.Execute(mSqlstr)


        mSqlstr = "DELETE FROM PAY_MONTHLY_AA_TRN" & vbCrLf _
                & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND PAYYEAR = " & Year(CDate(mSalaryStartMonth)) & " " & vbCrLf _
                & " AND TO_CHAR(SAL_MONTH,'MMYYYY')=TO_CHAR('" & VB6.Format(mSalaryMonth, "MMYYYY") & "') " & vbCrLf _
                & " AND SAL_FLAG IN ('S','I') "

        PubDBCn.Execute(mSqlstr)

        mSqlstr = " DELETE FROM PAY_OVERTIME_ADJ_MST " & vbCrLf _
                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND TO_CHAR(OT_DATE,'MMYYYY')=TO_CHAR('" & VB6.Format(mSalaryMonth, "MMYYYY") & "') "

        PubDBCn.Execute(mSqlstr)

        PubDBCn.CommitTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation("Leave Unprocessed")

        Exit Sub
ErrPart:

        PubDBCn.RollbackTrans()
    End Sub

    Private Sub TxtCardNo_TextChanged(sender As Object, e As EventArgs) Handles TxtCardNo.TextChanged

    End Sub

    Private Sub TxtCardNo_KeyUp(sender As Object, EventArgs As KeyEventArgs) Handles TxtCardNo.KeyUp
        Dim KeyCode As Short = EventArgs.KeyCode
        Dim Shift As Short = EventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            cmdsearch_Click(cmdSearch, New System.EventArgs())
        End If
        'Dim KeyAscii As Short = Asc(EventArgs.KeyChar)
        'If KeyAscii = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdSearch, New System.EventArgs())
        'EventArgs.KeyChar = Chr(KeyAscii)
        'If KeyAscii = 0 Then
        '    EventArgs.Handled = True
        'End If
    End Sub
End Class
