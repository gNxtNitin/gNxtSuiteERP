Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class FrmLTAArrearProcess
    Inherits System.Windows.Forms.Form
    'Dim PvtDBCn As ADODB.Connection
    Dim ConWorkDay As Double
    Private Const ConWorkHour As Short = 8
    Dim mCurrentFYNo As Integer
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
    End Sub
    Private Sub cmdOk_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdOK.Click

        On Error GoTo ErrPart
        Dim mDate As String
        If FieldVarification = False Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        PBar.Visible = True
        mDate = MainClass.LastDay(Month(CDate(lblNewDate.Text)), Year(CDate(lblNewDate.Text))) & "/" & Month(CDate(lblNewDate.Text)) & "/" & Year(CDate(lblNewDate.Text))

        mCurrentFYNo = GetCurrentFYNo(PubDBCn, mDate)

        If mCurrentFYNo = -1 Then
            Exit Sub
        End If

        Call LTAArrearProcess()


        PBar.Visible = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        PBar.Visible = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation("Salary Not Process.")
    End Sub
    Private Function FieldVarification() As Boolean
        FieldVarification = True

        If OptParti.Checked = True Then
            If TxtCardNo.Text = "" Then
                MsgBox("CardNo. Not Selected.")
                FieldVarification = False
                TxtCardNo.Focus()
                Exit Function
            End If
        End If
    End Function
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click
        On Error GoTo SearchErr
        Dim SqlStr As String = ""

        SqlStr = "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & ""
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
        If FieldVarification = False Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        PBar.Visible = False
        mDate = MainClass.LastDay(Month(CDate(lblNewDate.Text)), Year(CDate(lblNewDate.Text))) & "/" & Month(CDate(lblNewDate.Text)) & "/" & Year(CDate(lblNewDate.Text))
        mCurrentFYNo = GetCurrentFYNo(PubDBCn, mDate)

        If mCurrentFYNo = -1 Then
            Exit Sub
        End If


        Call SalUnProcess()

        cmdUnProcess.Enabled = False
        PBar.Visible = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub FrmLTAArrearProcess_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        Me.Text = "Employee LTA Arear Process"

    End Sub

    Private Sub FrmLTAArrearProcess_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        Me.Left = VB6.TwipsToPixelsX(25)
        Me.Top = VB6.TwipsToPixelsY(25)
        MainClass.SetControlsColor(Me)
        Me.Height = VB6.TwipsToPixelsY(3855)
        Me.Width = VB6.TwipsToPixelsX(5475)

        txtMonth.Enabled = False
        '    TxtYear.Enabled = False

        lblNewDate.Text = CStr(RunDate)

        If PubATHUSER = True Then
            cmdUnProcess.Enabled = True
        Else
            cmdUnProcess.Enabled = False
        End If


        txtMonth.Text = MonthName(Month(RunDate)) & ", " & Year(RunDate)
        '    TxtYear.Text = Year(RunDate)

        OptAll.Checked = True
        HideUnHide(False)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub
    Private Sub FrmLTAArrearProcess_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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
        cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Private Sub TxtCardNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtCardNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdsearch, New System.EventArgs())
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub TxtCardNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtCardNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If TxtCardNo.Text = "" Then GoTo EventExitSub
        TxtCardNo.Text = VB6.Format(TxtCardNo.Text, "000000")
        If MainClass.ValidateWithMasterTable((TxtCardNo.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
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
        lblNewDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, prmSpinDirection, CDate(lblNewDate.Text)))
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

    Private Sub LTAArrearProcess()

        On Error GoTo ErrPart
        Dim RsLTATRN As ADODB.Recordset
        Dim RsEmployee As ADODB.Recordset
        Static mDOJ As String
        Static mDOL As String
        Dim mMonth As String
        Dim mSalDate As String
        Dim mEmpCode As String
        Dim SqlStr As String = ""

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mMonth = UCase(VB6.Format(lblNewDate.Text, "MMM-YYYY"))
        mSalDate = MainClass.LastDay(Month(CDate(lblNewDate.Text)), Year(CDate(lblNewDate.Text))) & "/" & VB6.Format(lblNewDate.Text, "MM/YYYY")
        mDOJ = MainClass.LastDay(Month(CDate(lblNewDate.Text)), Year(CDate(lblNewDate.Text))) & "/" & VB6.Format(lblNewDate.Text, "MM/YYYY")
        mDOL = "01" & "/" & VB6.Format(lblNewDate.Text, "MM/YYYY")

        SqlStr = ""

        SqlStr = "Select * From PAY_LTA_ARREAR_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TO_CHAR(ARREAR_DATE,'MON-YYYY')='" & mMonth & "'"

        If OptParti.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & TxtCardNo.Text & "'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLTATRN, ADODB.LockTypeEnum.adLockOptimistic)

        If RsLTATRN.EOF = False Then
            SqlStr = CStr(MsgBox("LTA Arrear Already Processed For This Period ... " & vbNewLine & vbNewLine & "Want To Reprocess ...", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2))
            If SqlStr = CStr(MsgBoxResult.Yes) Then
                SqlStr = "DELETE FROM PAY_LTA_ARREAR_DET WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TO_CHAR(ARREAR_DATE,'MON-YYYY')='" & mMonth & "'"

                If OptParti.Checked = True Then
                    SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & TxtCardNo.Text & "'"
                End If

                PubDBCn.Execute(SqlStr)

                SqlStr = "DELETE FROM PAY_LTA_ARREAR_HDR WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TO_CHAR(ARREAR_DATE,'MON-YYYY')='" & mMonth & "'"

                If OptParti.Checked = True Then
                    SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & TxtCardNo.Text & "'"
                End If

                PubDBCn.Execute(SqlStr)
            Else
                PBar.Visible = False
                Exit Sub
            End If
        End If

        ''---------------------------------------
        SqlStr = " SELECT * FROM " & vbCrLf & " PAY_EMPLOYEE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_STOP_SALARY='N' AND " & vbCrLf & " EMP_DOJ <=TO_DATE('" & VB6.Format(mDOJ, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " AND (EMP_LEAVE_DATE >TO_DATE('" & VB6.Format(mDOL, "dd-mmm-yyyy") & "','DD-MON-YYYY') OR EMP_LEAVE_DATE IS NULL) "

        If OptParti.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & TxtCardNo.Text & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND EMP_CODE NOT IN (SELECT EMP_CODE FROM " & vbCrLf & " PAY_FFSETTLE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TO_CHAR(EMP_LEAVE_DATE,'MON-YYYY')='" & mMonth & "')"

        SqlStr = SqlStr & vbCrLf & " AND EMP_CODE IN (SELECT EMP_CODE FROM " & vbCrLf & " PAY_SALARYDEF_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IS_ARREAR='Y'" & vbCrLf & " AND TO_CHAR(ARREAR_DATE,'MON-YYYY')='" & mMonth & "')"

        SqlStr = SqlStr & vbCrLf & " Order By EMP_NAME"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmployee, ADODB.LockTypeEnum.adLockOptimistic)

        If RsEmployee.EOF = False Then
            Do While RsEmployee.EOF = False
                mEmpCode = IIf(IsDbNull(RsEmployee.Fields("EMP_CODE").Value), "", RsEmployee.Fields("EMP_CODE").Value)
                If UpdateSalTrn(mEmpCode, mSalDate) = False Then GoTo ErrPart
                RsEmployee.MoveNext()
            Loop
            MsgBox("LTA Arrear Process Complete")
        Else
            MsgBox("No Record Found For Processing.")
        End If

        PubDBCn.CommitTrans()


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub

ErrPart:
        'Resume
        MsgInformation("LTA Arrear Process Not Complete, Try Again.")
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

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

        SqlStr = "Select COUNT(1) AS CNTREC From PAY_LTA_ARREAR_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ARREAR_DATE>TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

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
        SqlStr = "Select * From PAY_LTA_ARREAR_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TO_CHAR(ARREAR_DATE,'MON-YYYY')='" & mMonth & "'"

        If OptParti.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & TxtCardNo.Text & "'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSalTRN, ADODB.LockTypeEnum.adLockOptimistic)

        If RsSalTRN.EOF = False Then
            SqlStr = CStr(MsgBox("Are you want Un-Processed LTA Arear For This Period ... " & vbNewLine & vbNewLine & "Want To Reprocess ...", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2))
            If SqlStr = CStr(MsgBoxResult.Yes) Then

                SqlStr = "DELETE FROM PAY_LTA_ARREAR_DET WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TO_CHAR(ARREAR_DATE,'MON-YYYY')='" & mMonth & "'"

                If OptParti.Checked = True Then
                    SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & TxtCardNo.Text & "'"
                End If

                PubDBCn.Execute(SqlStr)

                SqlStr = "DELETE FROM PAY_LTA_ARREAR_HDR WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TO_CHAR(ARREAR_DATE,'MON-YYYY')='" & mMonth & "'"

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
    Private Function UpdateSalTrn(ByRef mCode As String, ByRef mSalDate As String) As Boolean


        On Error GoTo UpDateSalTrnErr
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mNewLTAAmount As Double
        Dim mOLDLTAAmount As Double
        Dim SqlStr As String = ""
        Dim mINCWEFFrom As String
        Dim mSerialNo As Integer
        Dim mLTAMonth As String
        Dim mWDays As Double
        Dim mPaidDays As Double
        Dim mPrevLTA As Double
        Dim mActualLTA As Double
        Dim mPaidAmount As Double
        Dim mFromDate As String
        Dim mToDate As String
        Dim mUpdateDetail As Boolean
        Dim mTotPaidAmount As Double
        Dim mMonthLastDate As Integer

        mINCWEFFrom = GetWEFDate(mCode, mSalDate)
        mSerialNo = 0
        mUpdateDetail = False
        mTotPaidAmount = 0

        ''AND IH.FYEAR=" & mCurrentFYNo & "

        SqlStr = " SELECT IH.*, " & vbCrLf & " ID.SERIAL_NO, ID.LTA_MONTH, ID.WDAYS, ID.PAID_DAYS, " & vbCrLf & " ID.ACTUAL_AMOUNT, ID.PAID_AMOUNT" & vbCrLf & " FROM PAY_LTA_HDR IH, PAY_LTA_DET ID" & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " " & vbCrLf & " AND IH.EMP_CODE = '" & mCode & "'" & vbCrLf & " AND IH.COMPANY_CODE=ID.COMPANY_CODE" & vbCrLf & " AND IH.FYEAR=ID.FYEAR" & vbCrLf & " AND IH.EMP_CODE=ID.EMP_CODE"

        SqlStr = SqlStr & vbCrLf & " AND LTA_MONTH >=TO_DATE('" & VB6.Format(mINCWEFFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY')" ''& vbCrLf |            & " AND TO_DATE <='" & VB6.Format(mINCWEFFrom, "DD-MMM-YYYY") & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            mFromDate = IIf(IsDbNull(RsTemp.Fields("FROM_DATE").Value), "", RsTemp.Fields("FROM_DATE").Value)
            mToDate = IIf(IsDbNull(RsTemp.Fields("TO_DATE").Value), "", RsTemp.Fields("TO_DATE").Value)

            SqlStr = " INSERT INTO PAY_LTA_ARREAR_HDR ( " & vbCrLf & " COMPANY_CODE, FYEAR, EMP_CODE, " & vbCrLf & " FROM_DATE, TO_DATE, ARREAR_DATE, CHQ_NO, CHQ_DATE, BANK_NAME, REMARKS ,  " & vbCrLf & " AC_POSTING , NET_LTA_AMOUNT ,  " & vbCrLf & " ADDUSER , ADDDATE ) VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & mCurrentFYNo & ", " & vbCrLf & " '" & mCode & "',TO_DATE('" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), TO_DATE('" & VB6.Format(mToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'', '','','', " & vbCrLf & " 'N',0,'', '') "
            PubDBCn.Execute(SqlStr)

            Do While RsTemp.EOF = False

                mLTAMonth = VB6.Format(IIf(IsDbNull(RsTemp.Fields("LTA_MONTH").Value), "", RsTemp.Fields("LTA_MONTH").Value), "DD/MM/YYYY")
                mMonthLastDate = MainClass.LastDay(Month(CDate(mLTAMonth)), Year(CDate(mLTAMonth)))
                mWDays = IIf(IsDbNull(RsTemp.Fields("WDAYS").Value), 0, RsTemp.Fields("WDAYS").Value)
                mPaidDays = IIf(IsDbNull(RsTemp.Fields("PAID_DAYS").Value), 0, RsTemp.Fields("PAID_DAYS").Value)
                mPrevLTA = IIf(IsDbNull(RsTemp.Fields("PAID_AMOUNT").Value), "", RsTemp.Fields("PAID_AMOUNT").Value) ''ACTUAL_AMOUNT
                mActualLTA = GetNewLTA(mCode, mLTAMonth)
                mActualLTA = mActualLTA * mPaidDays / mMonthLastDate
                mActualLTA = System.Math.Round(mActualLTA, 2)
                mPaidAmount = System.Math.Round(mActualLTA - mPrevLTA, 0) ''Round((mActualLTA - mPrevLTA) * mPaidDays / mWDays, 0)

                If mPaidAmount > 0 Then
                    mSerialNo = mSerialNo + 1
                    mTotPaidAmount = mTotPaidAmount + mPaidAmount
                    SqlStr = " INSERT INTO PAY_LTA_ARREAR_DET ( " & vbCrLf & " COMPANY_CODE, FYEAR, EMP_CODE, ARREAR_DATE," & vbCrLf & " SERIAL_NO, LTA_MONTH, WDAYS,    " & vbCrLf & " PAID_DAYS, PREV_LTA_AMOUNT,  " & vbCrLf & " ACTUAL_AMOUNT , PAID_AMOUNT ) VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & mCurrentFYNo & ", '" & mCode & "', " & vbCrLf & " TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & mSerialNo & ", TO_DATE('" & VB6.Format(mLTAMonth, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " " & mWDays & ", " & mPaidDays & "," & mPrevLTA & "," & mActualLTA & ", " & vbCrLf & " " & mPaidAmount & ") "
                    PubDBCn.Execute(SqlStr)
                    mUpdateDetail = True
                End If
                RsTemp.MoveNext()
            Loop

            If mUpdateDetail = False Then
                SqlStr = "DELETE FROM PAY_LTA_ARREAR_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND EMP_CODE='" & mCode & "'" & vbCrLf & " AND ARREAR_DATE=TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                PubDBCn.Execute(SqlStr)
            Else
                SqlStr = " UPDATE PAY_LTA_ARREAR_HDR SET NET_LTA_AMOUNT =" & mTotPaidAmount & "" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND EMP_CODE='" & mCode & "'" & vbCrLf & " AND ARREAR_DATE=TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                PubDBCn.Execute(SqlStr)
            End If


        End If

        UpdateSalTrn = True

        Exit Function
UpDateSalTrnErr:
        'Resume
        MsgBox(Err.Description)
        UpdateSalTrn = False
    End Function

    Private Function GetNewLTA(ByRef mCode As String, ByRef mSalDate As String) As Double


        On Error GoTo UpDateSalTrnErr
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        GetNewLTA = 0
        SqlStr = " SELECT SALARYDEF.*,ADD_DEDUCT.CODE, ADD_DEDUCT.TYPE, ADD_DEDUCT.CALC_ON," & vbCrLf & " ADD_DEDUCT.INCLUDEDPF,ADD_DEDUCT.INCLUDEDESI, " & vbCrLf & " ADD_DEDUCT.ROUNDING AS ROUNDING,SALARYDEF.EMP_CONT " & vbCrLf & " FROM PAY_SALARYDEF_MST SALARYDEF, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf & " WHERE " & vbCrLf & " SALARYDEF.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ADD_DEDUCT.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SALARYDEF.COMPANY_CODE=ADD_DEDUCT.COMPANY_CODE" & vbCrLf & " AND SALARYDEF.ADD_DEDUCTCODE=ADD_DEDUCT.CODE" & vbCrLf & " AND SALARYDEF.EMP_CODE = '" & mCode & "'" & vbCrLf & " AND ADD_DEDUCT.TYPE = " & ConLTA & "" & vbCrLf & " AND ADD_DEDUCT.CALC_ON<>" & ConCalcVariable & "" & vbCrLf & " AND SALARYDEF.SALARY_EFF_DATE=( SELECT MAX(SALARY_EFF_DATE)" & vbCrLf & " FROM PAY_SALARYDEF_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE = '" & mCode & "'" & vbCrLf & " AND SALARY_EFF_DATE <= TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

        SqlStr = SqlStr & vbCrLf & " AND ADD_DEDUCT.CODE IN (" & vbCrLf & " SELECT CODE FROM PAY_SALARYHEAD_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ADDDEDUCT IN (" & ConPerks & ")" & vbCrLf & " AND STATUS='O' AND (CLOSED_DATE IS NULL OR CLOSED_DATE<=TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))" & vbCrLf & " UNION " & vbCrLf & " SELECT CODE FROM PAY_SALARYHEAD_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ADDDEDUCT IN (" & ConPerks & ")" & vbCrLf & " AND STATUS='C' AND CLOSED_DATE>TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            GetNewLTA = IIf(IsDbNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
        End If

        Exit Function
UpDateSalTrnErr:
        'Resume
        MsgBox(Err.Description)
        GetNewLTA = 0
    End Function

    Private Function GetWEFDate(ByRef mCode As String, ByRef mSalDate As String) As String


        On Error GoTo UpDateSalTrnErr
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        GetWEFDate = ""
        SqlStr = " SELECT SALARYDEF.*,ADD_DEDUCT.CODE, ADD_DEDUCT.TYPE, ADD_DEDUCT.CALC_ON," & vbCrLf & " ADD_DEDUCT.INCLUDEDPF,ADD_DEDUCT.INCLUDEDESI, " & vbCrLf & " ADD_DEDUCT.ROUNDING AS ROUNDING,SALARYDEF.EMP_CONT " & vbCrLf & " FROM PAY_SALARYDEF_MST SALARYDEF, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf & " WHERE " & vbCrLf & " SALARYDEF.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ADD_DEDUCT.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SALARYDEF.COMPANY_CODE=ADD_DEDUCT.COMPANY_CODE" & vbCrLf & " AND SALARYDEF.ADD_DEDUCTCODE=ADD_DEDUCT.CODE" & vbCrLf & " AND SALARYDEF.EMP_CODE = '" & mCode & "'" & vbCrLf & " AND ADD_DEDUCT.TYPE = " & ConLTA & "" & vbCrLf & " AND ADD_DEDUCT.CALC_ON<>" & ConCalcVariable & "" & vbCrLf & " AND SALARYDEF.SALARY_EFF_DATE=( SELECT MAX(SALARY_EFF_DATE)" & vbCrLf & " FROM PAY_SALARYDEF_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE = '" & mCode & "'" & vbCrLf & " AND SALARY_EFF_DATE <= TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

        SqlStr = SqlStr & vbCrLf & " AND ADD_DEDUCT.CODE IN (" & vbCrLf & " SELECT CODE FROM PAY_SALARYHEAD_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ADDDEDUCT IN (" & ConPerks & ")" & vbCrLf & " AND STATUS='O' AND (CLOSED_DATE IS NULL OR CLOSED_DATE<=TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))" & vbCrLf & " UNION " & vbCrLf & " SELECT CODE FROM PAY_SALARYHEAD_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ADDDEDUCT IN (" & ConPerks & ")" & vbCrLf & " AND STATUS='C' AND CLOSED_DATE>TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            GetWEFDate = VB6.Format(RsTemp.Fields("SALARY_EFF_DATE").Value, "DD/MM/YYYY") ''"01/04/2018" ''
        End If

        Exit Function
UpDateSalTrnErr:
        'Resume
        MsgBox(Err.Description)
        GetWEFDate = ""
    End Function
End Class
