Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmLTAEntry
    Inherits System.Windows.Forms.Form
    Dim RsLTAMain As ADODB.Recordset
    Dim RsLTADetail As ADODB.Recordset

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Dim Shw As Boolean
    Dim xCode As String

    Dim SqlStr As String = ""
    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12

    Private Const ColMonth As Short = 1
    Private Const ColWDays As Short = 2
    Private Const ColDays As Short = 3
    Private Const ColActual As Short = 4
    Private Const ColPayable As Short = 5
    Private Sub ViewGrid()

        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            AssignGrid(True)
            '        AdoDCMain.Refresh
            FormatSprdView()
            SprdView.Focus()
            fraMain.SendToBack()
        Else
            CmdView.Text = ConCmdGridViewCaption
            fraMain.BringToFront()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsLTAMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)

    End Sub
    Private Sub Clear1()

        txtEmpNo.Text = ""
        TxtName.Text = ""
        txtFName.Text = ""
        txtDOJ.Text = ""
        txtFrom.Text = ""
        txtTo.Text = ""
        txtChqDate.Text = ""
        txtChqNo.Text = ""
        txtBankName.Text = ""
        txtRemarks.Text = ""
        txtOthers.Text = ""
        txtNetLTA.Text = ""

        chkAccountPosting.CheckState = System.Windows.Forms.CheckState.Unchecked
        cmdAccountPosting.Enabled = False
        cmdAccountPosting.Visible = True
        cbodesignation.SelectedIndex = -1

        cmdPopulate.Enabled = True
        txtFrom.Enabled = True
        txtTo.Enabled = True

        MainClass.ButtonStatus(Me, XRIGHT, RsLTAMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        MainClass.ClearGrid(SprdMain)

    End Sub
    Private Sub cbodesignation_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cbodesignation.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cbodesignation_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cbodesignation.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub chkAccountPosting_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAccountPosting.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cmdAccountPosting_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAccountPosting.Click
        Dim mVNo As String
        Dim mVDate As String
        Dim mBankCode As Integer
        Dim mYM As Integer
        Dim mBType As String
        Dim mBSType As String
        Dim mm As New frmAtrn
        Dim mVType As String
        Dim mVSeqNo As Integer
        Dim mVNoSuffix As String
        Dim mCategory As String
        Dim mDivisionCode As Double

        If Trim(txtChqDate.Text) = "" Then
            MsgBox("Please Enter the Cheque Date.")
            Exit Sub
        End If

        If CDate(txtChqDate.Text) < CDate(txtTo.Text) Then
            MsgBox("Cheque Date cann't be less than To Date.")
            Exit Sub
        End If

        '    myMenu = "mnuJournal"

        If GetCurrentFYNo(PubDBCn, (txtChqDate.Text)) <> RsCompany.Fields("FYEAR").Value Then
            MsgBox("Cheque Date is not Current Year.")
            Exit Sub
        End If
        mm.lblBookType.Text = ConBankPayment '' ConJournal
        mm.MdiParent = Me.MdiParent

        If Trim(txtEmpNo.Text) = "" Then Exit Sub
        '    If Trim(txtChqDate.Text) = "" Then Exit Sub
        mm.txtVDate.Text = VB6.Format(txtChqDate.Text, "DD/MM/YYYY")
        mYM = CInt(VB6.Format(Year(CDate(txtChqDate.Text)), "0000") & VB6.Format(Month(CDate(txtChqDate.Text)), "00"))
        mm.lblYM.Text = CStr(mYM)
        mBType = "L"

        If MainClass.ValidateWithMasterTable((txtEmpNo.Text), "EMP_CODE", "EMP_CATG", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mBSType = MasterNo
        End If

        If MainClass.ValidateWithMasterTable((txtEmpNo.Text), "EMP_CODE", "DIV_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Val(MasterNo)
        End If

        mm.lblSR.Text = mBType & mBSType & mDivisionCode

        mm.Show()
        If CheckSalVoucher(mYM, mVNo, mVDate, mVType, mVSeqNo, mVNoSuffix, Val(txtEmpNo.Text), mBType, mBSType, mDivisionCode, RsCompany.Fields("FYEAR").Value) = True Then

            mm.frmAtrn_Activated(Nothing, New System.EventArgs())
            mm.txtVDate.Text = VB6.Format(mVDate, "dd/mm/yyyy")
            mm.txtVType.Text = mVType
            mm.txtVNo.Text = VB6.Format(mVSeqNo, "00000")
            mm.txtVNoSuffix.Text = mVNoSuffix
            mm.lblEmpCode.Text = Trim(txtEmpNo.Text)
            mm.lblELYear.Text = RsCompany.Fields("FYEAR").Value
            mm.TxtVNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(True))
            mm.CmdAdd.Enabled = False
        Else
            mm.frmAtrn_Activated(Nothing, New System.EventArgs())
            mm.txtVDate.Text = VB6.Format(txtChqDate.Text, "DD/MM/YYYY") 'PubCurrDate   ''Format(MainClass.LastDay(Month(lblRunDate), Year(lblRunDate)) & "/" & vb6.Format(Month(lblRunDate), "00") & "/" & Year(lblRunDate), "dd/mm/yyyy")
            mm.lblEmpCode.Text = Trim(txtEmpNo.Text)
            mm.lblELYear.Text = RsCompany.Fields("FYEAR").Value
            mm.TxtVNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(True))
        End If
    End Sub
    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click


        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsLTAMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            Call Show1()
        End If
    End Sub
    Private Sub cmdPopulate_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPopulate.Click

        On Error GoTo ErrPart
        Dim mStartingDate As String
        Dim mEndingDate As String
        Dim mLastDay As Integer
        Dim CntCurrentMonth As String
        Dim CntToMonth As String
        Dim mTotalWorkingDays As Double
        Dim mAbsent As Double
        Dim mActualLTA As Double
        Dim mPaidLTA As Double
        Dim mLeaveDate As String

        MainClass.ClearGrid(SprdMain)

        CntCurrentMonth = "01/" & VB6.Format(txtFrom.Text, "MM/YYYY")
        CntToMonth = "01/" & VB6.Format(txtTo.Text, "MM/YYYY")

        If MainClass.ValidateWithMasterTable(Trim(txtEmpNo.Text), "EMP_CODE", "EMP_LEAVE_DATE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mLeaveDate = MasterNo
        End If

        With SprdMain
            Do While VB6.Format(CntCurrentMonth, "YYYYMM") <= VB6.Format(CntToMonth, "YYYYMM")
                mAbsent = 0
                mLastDay = MainClass.LastDay(Month(CDate(CntCurrentMonth)), Year(CDate(CntCurrentMonth)))

                .Row = .MaxRows
                .Col = ColMonth
                .Text = VB6.Format(CntCurrentMonth, "MMM-YYYY")

                If .MaxRows = 1 Then
                    mStartingDate = VB6.Format(txtFrom.Text, "DD/MM/YYYY")
                Else
                    mStartingDate = "01/" & VB6.Format(CntCurrentMonth, "MM/YYYY")
                End If

                If VB6.Format(CntCurrentMonth, "YYYYMM") = VB6.Format(CntToMonth, "YYYYMM") Then
                    mEndingDate = VB6.Format(txtTo.Text, "DD/MM/YYYY")
                Else
                    mEndingDate = VB6.Format(mLastDay, "00") & "/" & VB6.Format(CntCurrentMonth, "MM/YYYY")
                End If

                If Trim(mLeaveDate) <> "" Then
                    If CDate(mLeaveDate) < CDate(mEndingDate) Then
                        mEndingDate = mLeaveDate
                    End If
                End If

                mTotalWorkingDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mStartingDate), CDate(mEndingDate)) + 1
                mTotalWorkingDays = IIf(mTotalWorkingDays < 0, 0, mTotalWorkingDays)
                mAbsent = GetAbsentData(Trim(txtEmpNo.Text), mStartingDate, mEndingDate)
                mActualLTA = GetLTAAmount(Trim(txtEmpNo.Text), mStartingDate)

                mPaidLTA = mActualLTA * (mAbsent) / mLastDay
                ''mPaidLTA = mActualLTA * (mTotalWorkingDays - mAbsent) / mLastDay

                .Col = ColWDays
                .Text = VB6.Format(mTotalWorkingDays, "0.00")


                .Col = ColDays
                .Text = VB6.Format(mAbsent, "0.00") ''Format(mTotalWorkingDays - mAbsent, "0.00")

                .Col = ColActual
                .Text = VB6.Format(mActualLTA, "0.00")

                .Col = ColPayable
                .Text = VB6.Format(mPaidLTA, "0.00")

                CntCurrentMonth = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, CDate(CntCurrentMonth)))
                .MaxRows = .MaxRows + 1

            Loop
        End With

        FormatSprd(-1)
        Call CalcGrid()
        Exit Sub
ErrPart:
        'Resume
    End Sub

    Private Function GetLTAAmount(ByRef mCode As String, ByRef mDate As String) As Double


        On Error GoTo UpDateSalTrnErr
        Dim RsTemp As ADODB.Recordset = Nothing

        Dim mToEmpCompany As Integer
        Dim mToEmpCode As String
        Dim mFromEmpLeaveDate As String

        Dim mFromEmpCompany As Integer
        Dim mFromEmpCode As String

        GetLTAAmount = 0

        mToEmpCompany = RsCompany.Fields("COMPANY_CODE").Value
        mToEmpCode = mCode

SearchRow:
        SqlStr = GetEmpTransferSQL(mToEmpCode, mToEmpCompany)
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then

            mFromEmpCompany = IIf(IsDbNull(RsTemp.Fields("FROM_COMPANY_CODE").Value), "", RsTemp.Fields("FROM_COMPANY_CODE").Value)
            mFromEmpCode = IIf(IsDbNull(RsTemp.Fields("FROM_EMP_CODE").Value), "", RsTemp.Fields("FROM_EMP_CODE").Value)

            If MainClass.ValidateWithMasterTable(mFromEmpCode, "EMP_CODE", "EMP_LEAVE_DATE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & mFromEmpCompany & "") = True Then
                mFromEmpLeaveDate = VB6.Format(Trim(MasterNo), "DD/MM/YYYY")
            End If

            If CDate(mDate) <= CDate(mFromEmpLeaveDate) Then
                mToEmpCompany = mFromEmpCompany
                mToEmpCode = mFromEmpCode
            End If
        End If

        SqlStr = " SELECT SALARYDEF.*,ADD_DEDUCT.CODE, ADD_DEDUCT.TYPE, ADD_DEDUCT.CALC_ON," & vbCrLf & " ADD_DEDUCT.INCLUDEDPF,ADD_DEDUCT.INCLUDEDESI, " & vbCrLf & " ADD_DEDUCT.ROUNDING AS ROUNDING,SALARYDEF.EMP_CONT " & vbCrLf & " FROM PAY_SALARYDEF_MST SALARYDEF, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf & " WHERE " & vbCrLf & " SALARYDEF.COMPANY_CODE=" & mToEmpCompany & "" & vbCrLf & " AND ADD_DEDUCT.COMPANY_CODE=" & mToEmpCompany & "" & vbCrLf & " AND SALARYDEF.COMPANY_CODE=ADD_DEDUCT.COMPANY_CODE" & vbCrLf & " AND SALARYDEF.ADD_DEDUCTCODE=ADD_DEDUCT.CODE" & vbCrLf & " AND SALARYDEF.EMP_CODE = '" & mToEmpCode & "'" & vbCrLf & " AND ADD_DEDUCT.TYPE = " & ConLTA & "" & vbCrLf & " AND ADD_DEDUCT.CALC_ON<>" & ConCalcVariable & "" & vbCrLf & " AND SALARYDEF.SALARY_EFF_DATE=( SELECT MAX(SALARY_EFF_DATE)" & vbCrLf & " FROM PAY_SALARYDEF_MST WHERE " & vbCrLf & " COMPANY_CODE=" & mToEmpCompany & "" & vbCrLf & " AND EMP_CODE = '" & mToEmpCode & "'" & vbCrLf & " AND TO_CHAR(SALARY_EFF_DATE - ADDDAYS_IN,'YYYYMM') <= '" & VB6.Format(mDate, "YYYYMM") & "') "

        '    SqlStr = SqlStr & vbCrLf _
        ''            & " AND ADD_DEDUCT.CODE IN (" & vbCrLf _
        ''            & " SELECT CODE FROM PAY_SALARYHEAD_MST " & vbCrLf _
        ''            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''            & " AND ADDDEDUCT IN (" & ConPerks & ")" & vbCrLf _
        ''            & " AND STATUS='O' AND (CLOSED_DATE IS NULL OR CLOSED_DATE<='" & VB6.Format(mDate, "DD-MMM-YYYY") & "')" & vbCrLf _
        ''            & " UNION " & vbCrLf _
        ''            & " SELECT CODE FROM PAY_SALARYHEAD_MST " & vbCrLf _
        ''            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''            & " AND ADDDEDUCT IN (" & ConPerks & ")" & vbCrLf _
        ''            & " AND STATUS='C' AND CLOSED_DATE>'" & VB6.Format(mDate, "DD-MMM-YYYY") & "')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            GetLTAAmount = IIf(IsDbNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
        End If

        Exit Function
UpDateSalTrnErr:
        'Resume
        MsgBox(Err.Description)
        GetLTAAmount = 0
    End Function

    Private Function GetBasicSalaryAmount(ByRef mCode As String, ByRef mDate As String) As Double


        On Error GoTo UpDateSalTrnErr
        Dim RsTemp As ADODB.Recordset = Nothing

        GetBasicSalaryAmount = 0
        SqlStr = " SELECT SALARYDEF.*,ADD_DEDUCT.CODE, ADD_DEDUCT.TYPE, ADD_DEDUCT.CALC_ON," & vbCrLf & " ADD_DEDUCT.INCLUDEDPF,ADD_DEDUCT.INCLUDEDESI, " & vbCrLf & " ADD_DEDUCT.ROUNDING AS ROUNDING,SALARYDEF.EMP_CONT " & vbCrLf & " FROM PAY_SALARYDEF_MST SALARYDEF, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf & " WHERE " & vbCrLf & " SALARYDEF.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ADD_DEDUCT.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SALARYDEF.COMPANY_CODE=ADD_DEDUCT.COMPANY_CODE" & vbCrLf & " AND SALARYDEF.ADD_DEDUCTCODE=ADD_DEDUCT.CODE" & vbCrLf & " AND SALARYDEF.EMP_CODE = '" & mCode & "'" & vbCrLf & " AND ADD_DEDUCT.TYPE = " & ConLTA & "" & vbCrLf & " AND ADD_DEDUCT.CALC_ON<>" & ConCalcVariable & "" & vbCrLf & " AND SALARYDEF.SALARY_EFF_DATE=( SELECT MAX(SALARY_EFF_DATE)" & vbCrLf & " FROM PAY_SALARYDEF_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE = '" & mCode & "'" & vbCrLf & " AND SALARY_EFF_DATE- ADDDAYS_IN <= TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

        SqlStr = SqlStr & vbCrLf & " AND ADD_DEDUCT.CODE IN (" & vbCrLf & " SELECT CODE FROM PAY_SALARYHEAD_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ADDDEDUCT IN (" & ConPerks & ")" & vbCrLf & " AND STATUS='O' AND (CLOSED_DATE IS NULL OR CLOSED_DATE<=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))" & vbCrLf & " UNION " & vbCrLf & " SELECT CODE FROM PAY_SALARYHEAD_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ADDDEDUCT IN (" & ConPerks & ")" & vbCrLf & " AND STATUS='C' AND CLOSED_DATE>TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')))"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            GetBasicSalaryAmount = IIf(IsDbNull(RsTemp.Fields("BASICSALARY").Value), 0, RsTemp.Fields("BASICSALARY").Value)
        End If

        Exit Function
UpDateSalTrnErr:
        'Resume
        MsgBox(Err.Description)
        GetBasicSalaryAmount = 0
    End Function

    Private Function GetAbsentData(ByRef mCode As String, ByRef mFromDate As String, ByRef mToDate As String) As Double

        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset = Nothing

        Dim mToEmpCompany As Integer
        Dim mToEmpCode As String
        Dim mFromEmpLeaveDate As String

        Dim mFromEmpCompany As Integer
        Dim mFromEmpCode As String

        GetAbsentData = 0

        mToEmpCompany = RsCompany.Fields("COMPANY_CODE").Value
        mToEmpCode = mCode

SearchRow:
        SqlStr = GetEmpTransferSQL(mToEmpCode, mToEmpCompany)
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then

            mFromEmpCompany = IIf(IsDbNull(RsTemp.Fields("FROM_COMPANY_CODE").Value), "", RsTemp.Fields("FROM_COMPANY_CODE").Value)
            mFromEmpCode = IIf(IsDbNull(RsTemp.Fields("FROM_EMP_CODE").Value), "", RsTemp.Fields("FROM_EMP_CODE").Value)

            If MainClass.ValidateWithMasterTable(mFromEmpCode, "EMP_CODE", "EMP_LEAVE_DATE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & mFromEmpCompany & "") = True Then
                mFromEmpLeaveDate = VB6.Format(Trim(MasterNo), "DD/MM/YYYY")
            End If

            If CDate(mToDate) <= CDate(mFromEmpLeaveDate) Then
                mToEmpCompany = mFromEmpCompany
                mToEmpCode = mFromEmpCode
            End If
        End If


        SqlStr = " SELECT * " & vbCrLf & " FROM PAY_ATTN_MST WHERE" & vbCrLf & " COMPANY_CODE =" & mToEmpCompany & "" & vbCrLf & " AND PAYYEAR=" & Year(CDate(mFromDate)) & "" & vbCrLf & " AND EMP_CODE ='" & mToEmpCode & "'" & vbCrLf & " AND ATTN_DATE>=TO_DATE('" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND ATTN_DATE<=TO_DATE('" & VB6.Format(mToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " ORDER BY ATTN_DATE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            Do While Not RsTemp.EOF
                If (RsTemp.Fields("FIRSTHALF").Value = SUNDAY) Or (RsTemp.Fields("FIRSTHALF").Value = HOLIDAY) Or (RsTemp.Fields("FIRSTHALF").Value = PRESENT) Or (RsTemp.Fields("FIRSTHALF").Value = CASUAL) Or (RsTemp.Fields("FIRSTHALF").Value = EARN) Or (RsTemp.Fields("FIRSTHALF").Value = SICK) Or (RsTemp.Fields("FIRSTHALF").Value = MATERNITY) Or (RsTemp.Fields("FIRSTHALF").Value = CPLEARN) Or (RsTemp.Fields("FIRSTHALF").Value = CPLAVAIL) Then
                    GetAbsentData = GetAbsentData + 0.5
                End If

                If (RsTemp.Fields("SECONDHALF").Value = SUNDAY) Or (RsTemp.Fields("SECONDHALF").Value = HOLIDAY) Or (RsTemp.Fields("SECONDHALF").Value = PRESENT) Or (RsTemp.Fields("SECONDHALF").Value = CASUAL) Or (RsTemp.Fields("SECONDHALF").Value = EARN) Or (RsTemp.Fields("SECONDHALF").Value = SICK) Or (RsTemp.Fields("SECONDHALF").Value = MATERNITY) Or (RsTemp.Fields("SECONDHALF").Value = CPLEARN) Or (RsTemp.Fields("SECONDHALF").Value = CPLAVAIL) Then
                    GetAbsentData = GetAbsentData + 0.5
                End If

                '            If RsTemp!FIRSTHALF = ABSENT Then
                '                GetAbsentData = GetAbsentData + 0.5
                '            ElseIf RsTemp!FIRSTHALF = WOPAY Then
                '                GetAbsentData = GetAbsentData + 0.5
                '            End If
                '
                '            If RsTemp!SECONDHALF = ABSENT Then
                '                GetAbsentData = GetAbsentData + 0.5
                '            ElseIf RsTemp!SECONDHALF = WOPAY Then
                '                GetAbsentData = GetAbsentData + 0.5
                '            End If

                RsTemp.MoveNext()
            Loop
        End If
        Exit Function
ErrPart:
        GetAbsentData = 0
    End Function

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPreview.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForPrint(Crystal.DestinationConstants.crptToWindow)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForPrint(Crystal.DestinationConstants.crptToPrinter)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportForPrint(ByRef Mode As Crystal.DestinationConstants)


        On Error GoTo ERR1
        Dim All As Boolean
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mRptFileName As String

        PubDBCn.Errors.Clear()

        Call MainClass.ClearCRptFormulas(Report1)

        SqlStr = "SELECT * FROM " & vbCrLf & " PAY_LTA_HDR IH, PAY_LTA_DET ID, " & vbCrLf & " PAY_EMPLOYEE_MST EMP " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=ID.COMPANY_CODE" & vbCrLf & " AND IH.FYEAR=ID.FYEAR" & vbCrLf & " AND IH.EMP_CODE=ID.EMP_CODE " & vbCrLf & " AND IH.COMPANY_CODE=EMP.COMPANY_CODE" & vbCrLf & " AND IH.EMP_CODE=EMP.EMP_CODE " & vbCrLf & " AND IH.EMP_CODE='" & MainClass.AllowSingleQuote(txtEmpNo.Text) & "'"

        SqlStr = SqlStr & vbCrLf & " ORDER BY SERIAL_NO"

        mRptFileName = "EMPLTA.Rpt"

        mTitle = "L.T.A. FORMAT"
        mSubTitle = ""

        Call ShowReport(SqlStr, mRptFileName, Mode, mTitle, mSubTitle)
        Exit Sub
ERR1:
        'Resume
        MsgInformation(Err.Description)
        If Err.Number = 32755 Or Err.Number = 20507 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
    End Sub
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtEmpNo.Focus()
        Else
            CmdAdd.Text = ConCmdAddCaption
            ADDMode = False
            MODIFYMode = False
            Call Show1()
        End If
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click

        On Error GoTo DelErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mAcPosting As String

        If TxtName.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub

        SqlStr = " SELECT AC_POSTING FROM PAY_LTA_HDR" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND EMP_CODE='" & VB6.Format(txtEmpNo.Text, "000000") & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)
        If RsTemp.EOF = False Then
            mAcPosting = IIf(IsDbNull(RsTemp.Fields("AC_POSTING").Value), "N", RsTemp.Fields("AC_POSTING").Value)

            If mAcPosting = "Y" Then
                MsgInformation("Account Posting Done, so Cann't be Deleted.")
                Exit Sub
            End If
        End If

        If Not RsLTAMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                If Delete1 = False Then GoTo DelErrPart
                Clear1()
                '            If RsLTAMain.EOF = True Then
                '                Clear1
                '            Else
                '                Show1
                '            End If
            End If
        End If
        Exit Sub
DelErrPart:
        MsgBox("Record Not Deleted")
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click
        Dim mProdCode As String
        SqlStr = ""

        If MainClass.SearchGridMaster((txtEmpNo.Text), "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtEmpNo.Text = AcName1
            TxtName.Text = AcName
            TxtEmpNo_Validating(TxtEmpNo, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub

    End Sub
    Private Sub frmLTAEntry_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles sprdMain.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles sprdMain.LeaveCell
        On Error GoTo ErrPart
        Dim xPer As Double

        If eventArgs.NewRow = -1 Then Exit Sub

        CalcGrid()
        Exit Sub
ErrPart:
        'Resume
        MsgBox(Err.Description)
    End Sub
    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles sprdMain.Leave
        With SprdMain
            SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub
    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        Dim xMonth As Short
        Dim xYear As Short

        SqlStr = ""
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
        txtEmpNo.Text = SprdView.Text

        TxtEmpNo_Validating(TxtEmpNo, New System.ComponentModel.CancelEventArgs(True))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub
    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub
    Private Sub txtBankName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBankName.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtBankName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBankName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtBankName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtChqNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtChqNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtChqNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtChqNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtChqNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtDOJ_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDOJ.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtFrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFrom.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        If Trim(txtFrom.Text) = "" Then GoTo EventExitSub

        If Not IsDate(txtFrom.Text) Then
            MsgInformation("Invalid Date.")
            Cancel = True
            GoTo EventExitSub
        End If

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtEmpNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtEmpNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEmpNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtEmpNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtFName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFName.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtNetLTA_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNetLTA.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtNetLTA_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNetLTA.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub TxtName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtName.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub frmLTAEntry_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        SqlStr = ""
        If FormActive = True Then Exit Sub

        SqlStr = "Select * From PAY_LTA_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLTAMain, ADODB.LockTypeEnum.adLockOptimistic)

        SqlStr = "Select * From PAY_LTA_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLTADetail, ADODB.LockTypeEnum.adLockOptimistic)

        AssignGrid(False)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        settextlength()

        Clear1()
        FormActive = True
        If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '    Resume
    End Sub
    Private Sub frmLTAEntry_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)
        Me.Left = 0
        Me.Top = 0

        Call FillComboMst()
        FormatSprd(-1)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub
    Private Sub frmLTAEntry_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        'PvtDBCn.Cancel
        'PvtDBCn.Close
        RsLTAMain = Nothing
        'Set PvtDBCn = Nothing
    End Sub
    Private Sub Show1()

        On Error GoTo ERR1
        Dim RsEmp As ADODB.Recordset = Nothing
        Dim mTypeCode As Object
        Dim xSubkey As Integer
        Dim mEarnAmt As Object
        Dim mDeductAmt As Decimal
        Dim cntRow As Integer
        Dim mEmpDesg As String
        Dim pEmpCode As String
        Dim mPaidDays As Double
        Dim RsTemp As ADODB.Recordset = Nothing

        If RsLTAMain.EOF = False Then
            pEmpCode = Trim(IIf(IsDbNull(RsLTAMain.Fields("EMP_CODE").Value), 0, RsLTAMain.Fields("EMP_CODE").Value))
            txtEmpNo.Text = pEmpCode

            SqlStr = "SELECT * FROM PAY_EMPLOYEE_MST " & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(pEmpCode) & "'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsEmp.EOF = False Then
                TxtName.Text = IIf(IsDbNull(RsEmp.Fields("EMP_NAME").Value), "", RsEmp.Fields("EMP_NAME").Value)
                txtFName.Text = IIf(IsDbNull(RsEmp.Fields("EMP_FNAME").Value), "", RsEmp.Fields("EMP_FNAME").Value)

                '            If MainClass.ValidateWithMasterTable(pEmpCode, "EMP_CODE", "EMP_DESG_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '                mEmpDesg = IIf(IsNull(MasterNo), "-1", MasterNo)
                '                If MainClass.ValidateWithMasterTable(mEmpDesg, "DESG_CODE", "DESG_DESC", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '                    lblDesg.Caption = MasterNo
                '                End If
                '            End If
                '
                '            If MainClass.ValidateWithMasterTable(lblDesg.Caption, "DESG_DESC", "DESG_DESC", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '                cbodesignation.Text = MasterNo
                '            End If
            End If

            txtFrom.Text = VB6.Format(IIf(IsDbNull(RsLTAMain.Fields("FROM_DATE").Value), "", RsLTAMain.Fields("FROM_DATE").Value), "DD/MM/YYYY")
            txtTo.Text = VB6.Format(IIf(IsDbNull(RsLTAMain.Fields("TO_DATE").Value), "", RsLTAMain.Fields("TO_DATE").Value), "DD/MM/YYYY")

            SqlStr = " SELECT GETEMPDESG(" & RsCompany.Fields("COMPANY_CODE").Value & ",'" & pEmpCode & "',TO_DATE('" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DESG_DESC FROM DUAL"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
            If RsTemp.EOF = False Then
                cbodesignation.Text = IIf(IsDbNull(RsTemp.Fields("DESG_DESC").Value), "", RsTemp.Fields("DESG_DESC").Value)
                lblDesg.Text = IIf(IsDbNull(RsTemp.Fields("DESG_DESC").Value), "", RsTemp.Fields("DESG_DESC").Value)
            End If

            txtChqNo.Text = IIf(IsDbNull(RsLTAMain.Fields("CHQ_NO").Value), "", RsLTAMain.Fields("CHQ_NO").Value)
            txtChqDate.Text = IIf(IsDbNull(RsLTAMain.Fields("CHQ_DATE").Value), "", RsLTAMain.Fields("CHQ_DATE").Value)
            txtBankName.Text = IIf(IsDbNull(RsLTAMain.Fields("BANK_NAME").Value), "", RsLTAMain.Fields("BANK_NAME").Value)
            txtRemarks.Text = IIf(IsDbNull(RsLTAMain.Fields("Remarks").Value), "", RsLTAMain.Fields("Remarks").Value)

            txtOthers.Text = VB6.Format(IIf(IsDbNull(RsLTAMain.Fields("OTH_AMOUNT").Value), 0, RsLTAMain.Fields("OTH_AMOUNT").Value), "0.00")


            chkAccountPosting.CheckState = IIf(RsLTAMain.Fields("AC_POSTING").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkAccountPosting.Enabled = IIf(RsLTAMain.Fields("AC_POSTING").Value = "Y", False, True)

            Call ShowDetail1(pEmpCode)

            cmdPopulate.Enabled = False
            txtFrom.Enabled = False
            txtTo.Enabled = False

            ADDMode = False
            MODIFYMode = False
            cmdAccountPosting.Enabled = True
        End If

        FormatSprd(-1)
        '    txtBSalary.Enabled = True

        CalcGrid()

        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColMonth, ColPayable)

        MainClass.ButtonStatus(Me, XRIGHT, RsLTAMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)

        Exit Sub
ERR1:
        'Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)

    End Sub
    Private Sub ShowDetail1(ByRef pEmpCode As String)

        On Error GoTo ERR1
        Dim cntRow As Integer

        SqlStr = "SELECT * FROM PAY_LTA_DET " & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR = " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(pEmpCode) & "'" & vbCrLf & " ORDER BY SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLTADetail, ADODB.LockTypeEnum.adLockReadOnly)

        If RsLTADetail.EOF = False Then
            cntRow = 1
            With SprdMain
                Do While RsLTADetail.EOF = False
                    .Row = cntRow
                    .Col = 1

                    .Col = ColMonth
                    .Text = VB6.Format(IIf(IsDbNull(RsLTADetail.Fields("LTA_MONTH").Value), "", RsLTADetail.Fields("LTA_MONTH").Value), "MMM-YYYY")

                    .Col = ColWDays
                    .Text = VB6.Format(IIf(IsDbNull(RsLTADetail.Fields("WDAYS").Value), "0", RsLTADetail.Fields("WDAYS").Value), "0.00")

                    .Col = ColDays
                    .Text = VB6.Format(IIf(IsDbNull(RsLTADetail.Fields("PAID_DAYS").Value), "0", RsLTADetail.Fields("PAID_DAYS").Value), "0.00")

                    .Col = ColActual
                    .Text = VB6.Format(IIf(IsDbNull(RsLTADetail.Fields("ACTUAL_AMOUNT").Value), "0", RsLTADetail.Fields("ACTUAL_AMOUNT").Value), "0.00")

                    .Col = ColPayable
                    .Text = VB6.Format(IIf(IsDbNull(RsLTADetail.Fields("PAID_AMOUNT").Value), "0", RsLTADetail.Fields("PAID_AMOUNT").Value), "0.00")

                    RsLTADetail.MoveNext()
                    cntRow = cntRow + 1
                    .MaxRows = .MaxRows + 1
                Loop
            End With
        End If
        FormatSprd(-1)
        Call CalcGrid()
        Exit Sub
ERR1:
        'Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)

    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If Update1 = True Then
            TxtEmpNo_Validating(TxtEmpNo, New System.ComponentModel.CancelEventArgs(False))
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
        Dim mCode As String
        Dim mISACPosting As String

        If Trim(txtEmpNo.Text) = "" Then
            Update1 = False
            Exit Function
        End If

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()



        mCode = txtEmpNo.Text
        mISACPosting = IIf(chkAccountPosting.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        SqlStr = ""

        If ADDMode = True Then
            SqlStr = " INSERT INTO PAY_LTA_HDR ( " & vbCrLf & " COMPANY_CODE, FYEAR, EMP_CODE, " & vbCrLf & " FROM_DATE, TO_DATE, NET_LTA_AMOUNT," & vbCrLf & " CHQ_NO, CHQ_DATE, BANK_NAME, REMARKS, AC_POSTING, OTH_AMOUNT," & vbCrLf & " ADDUSER, ADDDATE, MODUSER, MODDATE) VALUES ( "

            SqlStr = SqlStr & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & ", '" & mCode & "', " & vbCrLf & " TO_DATE('" & VB6.Format(txtFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), TO_DATE('" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & Val(txtNetLTA.Text) & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtChqNo.Text)) & "', TO_DATE('" & VB6.Format(txtChqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote((txtBankName.Text)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtRemarks.Text)) & "', '" & mISACPosting & "', " & Val(txtOthers.Text) & "," & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '', '')"

        Else
            SqlStr = "UPDATE  PAY_LTA_HDR SET " & vbCrLf & " FROM_DATE=TO_DATE('" & VB6.Format(txtFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " TO_DATE=TO_DATE('" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " CHQ_DATE=TO_DATE('" & VB6.Format(txtChqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),  " & vbCrLf & " CHQ_NO='" & MainClass.AllowSingleQuote((txtChqNo.Text)) & "', NET_LTA_AMOUNT=" & Val(txtNetLTA.Text) & "," & vbCrLf & " BANK_NAME='" & MainClass.AllowSingleQuote((txtBankName.Text)) & "', " & vbCrLf & " REMARKS='" & MainClass.AllowSingleQuote((txtRemarks.Text)) & "'," & vbCrLf & " AC_POSTING='" & mISACPosting & "'," & vbCrLf & " OTH_AMOUNT=" & Val(txtOthers.Text) & "," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " ModDate=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            SqlStr = SqlStr & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND EMP_CODE='" & mCode & "'"
        End If

UpdatePart:
        PubDBCn.Execute(SqlStr)

        If UpdateDetail1(mCode) = False Then GoTo UpdateError

        PubDBCn.CommitTrans()
        RsLTAMain.Requery()
        Update1 = True
        Exit Function
UpdateError:
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        Update1 = False
        PubDBCn.RollbackTrans()
        RsLTAMain.Requery()
        RsLTADetail.Requery()
        PubDBCn.Errors.Clear()
        ''   Resume
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function UpdateDetail1(ByRef pEmpCode As String) As Boolean
        On Error GoTo UpdateError

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mMonth As String
        Dim mWDays As Double
        Dim mDays As Double
        Dim mActual As Double
        Dim mPayable As Double
        Dim cntRow As Integer

        SqlStr = ""
        SqlStr = " DELETE FROM PAY_LTA_DET " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(pEmpCode) & "'"

        PubDBCn.Execute(SqlStr)

        SqlStr = ""

        With SprdMain
            For cntRow = 1 To .MaxRows

                .Row = cntRow
                .Col = ColMonth
                mMonth = VB6.Format(.Text, "DD-MMM-YYYY")

                .Col = ColWDays
                mWDays = Val(.Text)

                .Col = ColDays
                mDays = Val(.Text)

                .Col = ColActual
                mActual = Val(.Text)

                .Col = ColPayable
                mPayable = Val(.Text)

                If mPayable > 0 Then
                    SqlStr = " INSERT INTO PAY_LTA_DET (" & vbCrLf & " COMPANY_CODE, FYEAR, EMP_CODE, SERIAL_NO, " & vbCrLf & " LTA_MONTH, WDAYS, PAID_DAYS, " & vbCrLf & " ACTUAL_AMOUNT, PAID_AMOUNT " & vbCrLf & " ) VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & ", '" & Trim(pEmpCode) & "', " & Val(CStr(cntRow)) & ", " & vbCrLf & " TO_DATE('" & VB6.Format(mMonth, "DD-MMM-YYYY") & "'), " & Val(CStr(mWDays)) & ", " & vbCrLf & " " & mDays & ", " & Val(CStr(mActual)) & ", " & mPayable & ")"

                    PubDBCn.Execute(SqlStr)

                End If
            Next
        End With

        UpdateDetail1 = True
        Exit Function
UpdateError:
        UpdateDetail1 = False
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
    End Function
    Private Sub TxtEmpNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpNo.DoubleClick
        cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Private Sub txtName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub TxtEmpNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtEmpNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            cmdsearch_Click(cmdsearch, New System.EventArgs())
        End If
    End Sub
    Private Function FieldsVarification() As Boolean

        On Error GoTo ERR1
        Dim xAmount As Decimal
        Dim mAppDate As Date
        Dim mWef As Date
        Dim mArrearDate As Date
        Dim mDate As String
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mAcPosting As String
        Dim mLeaveDate As String

        FieldsVarification = True
        If Trim(TxtName.Text) = "" Then
            MsgInformation("Name is empty. Cannot Save")
            TxtName.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtEmpNo.Text) = "" Then
            MsgInformation("Employee Code is empty. Cannot Save")
            txtEmpNo.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtFrom.Text) = "" Then
            MsgInformation("LTA From Date is empty. Cannot Save")
            If txtFrom.Enabled = True Then txtFrom.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtTo.Text) = "" Then
            MsgInformation("LTA To Date is empty. Cannot Save")
            If txtTo.Enabled = True Then txtTo.Focus()
            FieldsVarification = False
            Exit Function
        End If

        '    If Trim(txtChqDate.Text) = "" Then
        '        MsgInformation "LTA Paid Date is empty. Cannot Save"
        '        If txtChqDate.Enabled = True Then txtChqDate.SetFocus
        '        FieldsVarification = False
        '        Exit Function
        '    End If

        If FYChk((txtTo.Text)) = False Then
            If txtTo.Enabled = True Then txtTo.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If CDate(txtFrom.Text) > CDate(txtTo.Text) Then
            MsgInformation("To Date Cann't be less than From Date.")
            If txtTo.Enabled = True Then txtTo.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable(Trim(txtEmpNo.Text), "EMP_CODE", "EMP_LEAVE_DATE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mLeaveDate = MasterNo
        Else
            MsgInformation("Invaild Employee Code.")
            txtTo.Text = ""
            Exit Function
        End If


        '    If Trim(mLeaveDate) <> "" Then
        '        If CVDate(mLeaveDate) < CVDate(txtTo.Text) Then
        '            MsgInformation "Invalid To Date."
        '            If txtTo.Enabled = True Then txtTo.SetFocus
        '            FieldsVarification = False
        '            Exit Function
        '        End If
        '    End If


        '    If chkAccountPosting.Value = vbChecked Then
        '        If Trim(txtChqNo.Text) = "" Then
        '            MsgInformation "Cheque No is empty. Cannot Save"
        '            txtChqNo.SetFocus
        '            FieldsVarification = False
        '            Exit Function
        '        End If
        '    End If

        CalcGrid()


        If MODIFYMode = True Then
            SqlStr = " SELECT AC_POSTING FROM PAY_LTA_HDR" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & VB6.Format(txtEmpNo.Text, "000000") & "'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)
            If RsTemp.EOF = False Then
                mAcPosting = IIf(IsDbNull(RsTemp.Fields("AC_POSTING").Value), "N", RsTemp.Fields("AC_POSTING").Value)

                If mAcPosting = "Y" Then
                    MsgInformation("Account Posting Done, so Cann't be modify.")
                    FieldsVarification = False
                    Exit Function
                End If
            End If
        End If

        If MainClass.ValidDataInGrid(SprdMain, ColMonth, "S", "Please check. Nothing to Save") = False Then FieldsVarification = False : Exit Function

        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And (RsLTAMain.EOF = True Or RsLTAMain.EOF = True) Then Exit Function
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        FieldsVarification = False
        'Resume
    End Function
    Private Sub settextlength()

        On Error GoTo ERR1
        TxtName.Maxlength = MainClass.SetMaxLength("EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn)

        txtEmpNo.Maxlength = RsLTAMain.Fields("EMP_CODE").DefinedSize
        txtFrom.Maxlength = 10
        txtTo.Maxlength = 10
        txtChqNo.Maxlength = RsLTAMain.Fields("CHQ_NO").DefinedSize
        txtChqDate.Maxlength = 10
        txtBankName.Maxlength = RsLTAMain.Fields("BANK_NAME").DefinedSize
        txtRemarks.Maxlength = RsLTAMain.Fields("REMARKS").DefinedSize

        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        Dim SqlStr As String = ""

        MainClass.ClearGrid(SprdView)
        SqlStr = " SELECT DISTINCT EMP.EMP_CODE, EMP.EMP_NAME AS NAME, IH.FROM_DATE,IH.TO_DATE," & vbCrLf & " IH.NET_LTA_AMOUNT " & vbCrLf & " FROM PAY_LTA_HDR IH, PAY_EMPLOYEE_MST EMP" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=EMP.COMPANY_CODE  " & vbCrLf & " AND IH.EMP_CODE=EMP.EMP_CODE  " & vbCrLf & " ORDER BY EMP.EMP_NAME, EMP.EMP_CODE"

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()

    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1

            .set_RowHeight(0, 600)

            .set_ColWidth(0, 576 * 0)
            .set_ColWidth(1, 576 * 2)
            .set_ColWidth(2, 576 * 7)
            .set_ColWidth(3, 576 * 3)
            .set_ColWidth(4, 576 * 2)

            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            SprdView.set_RowHeight(-1, 300)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Function Delete1() As Boolean
        On Error GoTo DeleteErr

        If Trim(txtEmpNo.Text) = "" Then
            MsgInformation("Nothing to Delete.")
            Exit Function
        End If

        SqlStr = ""


        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "Delete from PAY_LTA_DET " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND EMP_CODE='" & Trim(txtEmpNo.Text) & "'"

        PubDBCn.Execute(SqlStr)

        SqlStr = " DELETE FROM PAY_LTA_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote((txtEmpNo.Text)) & "'"

        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()
        RsLTAMain.Requery()
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans()
        RsLTAMain.Requery()
        MsgBox(Err.Description)
    End Function
    Private Sub TxtEmpNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmpNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim mEmpCode As String
        Dim mName As String
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing
        Dim mDesgName As String
        Dim RsTemp As ADODB.Recordset = Nothing

        Dim RsEmpTrf As ADODB.Recordset
        Dim RsTempEmp As ADODB.Recordset

        Dim xSqlStr As String
        Dim mFromCompanyCode As Integer
        Dim mFromEmpCode As String
        'Dim mToCompanyCode As Long
        'Dim mToEmpCode As String

        If Trim(txtEmpNo.Text) = "" Then GoTo EventExitSub

        txtEmpNo.Text = VB6.Format(txtEmpNo.Text, "000000")
        SqlStr = "SELECT * FROM PAY_EMPLOYEE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote((txtEmpNo.Text)) & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockOptimistic)

        SqlStr = ""

        If RS.EOF = False Then
            Clear1()
            txtEmpNo.Text = RS.Fields("EMP_CODE").Value
            TxtName.Text = IIf(IsDbNull(RS.Fields("EMP_NAME").Value), "", RS.Fields("EMP_NAME").Value)
            txtFName.Text = IIf(IsDbNull(RS.Fields("EMP_FNAME").Value), "", RS.Fields("EMP_FNAME").Value)

            mFromCompanyCode = IIf(IsDbNull(RS.Fields("COMPANY_CODE").Value), "", RS.Fields("COMPANY_CODE").Value)
            mFromEmpCode = IIf(IsDbNull(RS.Fields("EMP_CODE").Value), "", RS.Fields("EMP_CODE").Value)
SearchRow:
            xSqlStr = GetEmpTransferSQL(mFromEmpCode, mFromCompanyCode)
            MainClass.UOpenRecordSet(xSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmpTrf, ADODB.LockTypeEnum.adLockOptimistic)

            If RsEmpTrf.EOF = False Then
                mFromCompanyCode = IIf(IsDbNull(RsEmpTrf.Fields("FROM_COMPANY_CODE").Value), "", RsEmpTrf.Fields("FROM_COMPANY_CODE").Value)
                mFromEmpCode = IIf(IsDbNull(RsEmpTrf.Fields("FROM_EMP_CODE").Value), "", RsEmpTrf.Fields("FROM_EMP_CODE").Value)
                GoTo SearchRow
            End If

            xSqlStr = " SELECT EMP_DOJ,EMP_LEAVE_DATE " & vbCrLf & " FROM PAY_EMPLOYEE_MST" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE = " & mFromCompanyCode & "" & vbCrLf & " AND EMP_CODE = '" & mFromEmpCode & "'"


            MainClass.UOpenRecordSet(xSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempEmp, ADODB.LockTypeEnum.adLockOptimistic)

            If RsTempEmp.EOF = False Then
                txtDOJ.Text = VB6.Format(IIf(IsDbNull(RsTempEmp.Fields("EMP_DOJ").Value), "", RsTempEmp.Fields("EMP_DOJ").Value), "DD/MM/YYYY")
            End If


            '        txtDOJ.Text = Format(IIf(IsNull(RS!EMP_DOJ), "", RS!EMP_DOJ), "DD/MM/YYYY")
            '        txtDOL.Text = Format(IIf(IsNull(RS!EMP_LEAVE_DATE), "", RS!EMP_LEAVE_DATE), "DD/MM/YYYY")

            '        If MainClass.ValidateWithMasterTable(Trim(RS!EMP_DESG_CODE), "DESG_CODE", "DESG_DESC", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            '            mDesgName = MasterNo
            '            cbodesignation.Text = mDesgName
            '        End If
            mEmpCode = RS.Fields("EMP_CODE").Value

            SqlStr = " SELECT GETEMPDESG(" & RsCompany.Fields("COMPANY_CODE").Value & ",'" & mEmpCode & "',TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS DESG_DESC FROM DUAL"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
            If RsTemp.EOF = False Then
                cbodesignation.Text = IIf(IsDbNull(RsTemp.Fields("DESG_DESC").Value), "", RsTemp.Fields("DESG_DESC").Value)
            End If

            If MODIFYMode = True And RsLTAMain.EOF = False Then xCode = RsLTAMain.Fields("EMP_CODE").Value

            SqlStr = " SELECT * FROM PAY_LTA_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLTAMain, ADODB.LockTypeEnum.adLockReadOnly)

            If RsLTAMain.EOF = False Then
                '            Clear1
                Call Show1()
            Else
                If ADDMode = False And MODIFYMode = False Then
                    MsgBox("No Such Employee, Use add Button to New.", MsgBoxStyle.Information)
                    Cancel = True
                    GoTo EventExitSub
                ElseIf MODIFYMode = True Then
                    SqlStr = "SELECT * FROM PAY_LTA_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND EMP_CODE='" & xCode & "'"
                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLTAMain, ADODB.LockTypeEnum.adLockReadOnly)
                    GoTo EventExitSub
                End If
            End If
        Else
            MsgBox("This Employee Code does not exsits in Master.", MsgBoxStyle.Critical)
            Cancel = True
            GoTo EventExitSub
        End If
        GoTo EventExitSub
ERR1:

        MsgInformation(Err.Description)
        '    Resume
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub FillComboMst()


        cbodesignation.Items.Clear()

        MainClass.FillCombo(cbodesignation, "PAY_DESG_MST", "DESG_DESC", , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub FormatSprd(ByRef mRow As Integer)

        On Error GoTo ERR1
        With SprdMain
            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight * 1.25)


            .Col = ColMonth
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColMonth, 25)

            .Col = ColDays
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeNumberDecPlaces = 2
            .set_ColWidth(ColDays, 10)

            .Col = ColWDays
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeNumberDecPlaces = 2
            .set_ColWidth(ColWDays, 10)

            .Col = ColActual
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeNumberDecPlaces = 2
            .set_ColWidth(ColActual, 20)

            .Col = ColPayable
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeNumberDecPlaces = 2
            .set_ColWidth(ColPayable, 20)

            .Row = 0
            .Col = ColMonth
            .Text = "Month"

            .Col = ColWDays
            .Text = "Working Days"

            .Col = ColDays
            .Text = "Paid Days"

            .Col = ColActual
            .Text = "Actual LTA"

            .Col = ColPayable
            .Text = "Paid LTA"

        End With

        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColMonth, ColPayable)
        MainClass.SetSpreadColor(SprdMain, mRow)

        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Function CalcWDays(ByRef pEmpCode As String, ByRef pRunDate As String) As Double

        On Error GoTo ErrPart
        Dim RsBalEL As ADODB.Recordset
        Dim RsEmp As ADODB.Recordset = Nothing
        Dim mFHalf As Double
        Dim mSHalf As Double
        Dim xRunDate As String
        Dim mTotalLeaves As Double
        Dim mTotalHoliDays As Double
        Dim mTotalRunningDays As Double
        Dim mDOJ As String
        Dim mDOL As String
        Dim mStartingDate As String
        Dim mEndingDate As String
        Dim SqlStr As String = ""

        CalcWDays = 0

        xRunDate = VB6.Format(pRunDate, "DD/MM/YYYY")

        mStartingDate = "01/01/" & Year(CDate(xRunDate))
        mEndingDate = MainClass.LastDay(Month(CDate(xRunDate)), Year(CDate(xRunDate))) & "/" & VB6.Format(xRunDate, "MM/YYYY")
        '    mEndingDate = "31/12/" & Year(xRunDate)


        mDOJ = VB6.Format(txtDOJ.Text, "DD/MM/YYYY")
        '    mDOL = Format(txtDOL.Text, "DD/MM/YYYY")

        If mDOJ = "" Then

        ElseIf CDate(mStartingDate) < CDate(mDOJ) Then
            mStartingDate = mDOJ
        End If

        If mDOL = "" Then

        ElseIf CDate(mEndingDate) > CDate(mDOL) Then
            mEndingDate = mDOL
        End If

        mTotalRunningDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(mStartingDate), CDate(mEndingDate)) + 1

        SqlStr = " SELECT FIRSTHALF, SECONDHALF " & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PAYYEAR=" & Year(CDate(xRunDate)) & " " & vbCrLf & " AND EMP_CODE ='" & pEmpCode & "'" & vbCrLf & " AND ATTN_DATE<=TO_DATE('" & VB6.Format(mEndingDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBalEL, ADODB.LockTypeEnum.adLockOptimistic)

        If RsBalEL.EOF = False Then
            Do While Not RsBalEL.EOF
                If RsBalEL.Fields("FIRSTHALF").Value <> -1 Then
                    If RsBalEL.Fields("FIRSTHALF").Value = CPLEARN Or RsBalEL.Fields("FIRSTHALF").Value = CPLAVAIL Then

                    ElseIf RsBalEL.Fields("FIRSTHALF").Value = SUNDAY Or RsBalEL.Fields("FIRSTHALF").Value = HOLIDAY Then
                        mTotalHoliDays = mTotalHoliDays + 0.5
                    Else
                        mFHalf = mFHalf + 0.5
                    End If
                End If

                If RsBalEL.Fields("SECONDHALF").Value <> -1 Then
                    If RsBalEL.Fields("SECONDHALF").Value = CPLEARN Or RsBalEL.Fields("SECONDHALF").Value = CPLAVAIL Then

                    ElseIf RsBalEL.Fields("SECONDHALF").Value = SUNDAY Or RsBalEL.Fields("SECONDHALF").Value = HOLIDAY Then
                        mTotalHoliDays = mTotalHoliDays + 0.5
                    Else
                        mSHalf = mSHalf + 0.5
                    End If
                End If
                RsBalEL.MoveNext()
            Loop
        End If

        mTotalLeaves = mFHalf + mSHalf

        '    SqlStr = " SELECT COUNT(1) AS HOLIDAYCNT " & vbCrLf _
        ''            & " FROM PAY_HOLIDAY_MST WHERE " & vbCrLf _
        ''            & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''            & " AND HOLIDAY_DATE>='" & VB6.Format(mStartingDate, "DD-MMM-YYYY") & "' AND HOLIDAY_DATE<='" & VB6.Format(mEndingDate, "DD-MMM-YYYY") & "' "
        '
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsBalEL, adLockOptimistic
        '
        '    If RsBalEL.EOF = False Then
        '        mTotalHoliDays = IIf(IsNull(RsBalEL!HOLIDAYCNT), 0, RsBalEL!HOLIDAYCNT)
        '    End If

        CalcWDays = mTotalRunningDays - mTotalLeaves - mTotalHoliDays

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Public Sub CalcGrid()
        Dim mcntRow As Integer
        Dim xPaidLTA As Double

        If Trim(txtTo.Text) = "" Then Exit Sub
        xPaidLTA = 0

        For mcntRow = 1 To SprdMain.MaxRows
            SprdMain.Row = mcntRow

            SprdMain.Col = ColPayable
            xPaidLTA = xPaidLTA + IIf(IsNumeric(SprdMain.Text), SprdMain.Text, 0)
        Next

        xPaidLTA = xPaidLTA + Val(txtOthers.Text)

        txtNetLTA.Text = VB6.Format(System.Math.Round(xPaidLTA, 0), "0.00")

    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)

        Dim SqlStrSub As String
        Dim mRemarks As String
        Dim mAmountInword As String
        Dim mFromBasicSalary As Double
        Dim mToBasicSalary As Double

        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        MainClass.AssignCRptFormulas(Report1, "Desg=""" & Trim(cbodesignation.Text) & """")

        mAmountInword = MainClass.RupeesConversion(txtNetLTA.Text)

        MainClass.AssignCRptFormulas(Report1, "Remarks=""" & mAmountInword & """")


        mFromBasicSalary = GetBasicSalaryAmount(Trim(txtEmpNo.Text), VB6.Format(txtFrom.Text, "DD/MM/YYYY"))
        mToBasicSalary = GetBasicSalaryAmount(Trim(txtEmpNo.Text), VB6.Format(txtTo.Text, "DD/MM/YYYY"))

        MainClass.AssignCRptFormulas(Report1, "FROMBASICSAL=""" & VB6.Format(mFromBasicSalary, "0.00") & """")
        MainClass.AssignCRptFormulas(Report1, "TOBASICSAL=""" & VB6.Format(mToBasicSalary, "0.00") & """")


        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName

        '    ''"SubReport
        '
        '    SqlStrSub = "SELECT * FROM " & vbCrLf _
        ''            & " PAY_LTA_DET , PAY_SALARYHEAD_MST " & vbCrLf _
        ''            & " WHERE PAY_LTA_DET.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''            & " AND PAY_LTA_DET.COMPANY_CODE=PAY_SALARYHEAD_MST.COMPANY_CODE" & vbCrLf _
        ''            & " AND PAY_LTA_DET.SALHEADCODE=PAY_SALARYHEAD_MST.CODE " & vbCrLf _
        ''            & " AND PAY_SALARYHEAD_MST.ADDDEDUCT = 2 " & vbCrLf _
        ''            & " AND PAY_LTA_DET.EMP_CODE='" & MainClass.AllowSingleQuote(txtEmpNo.Text) & "'"
        '
        '    Report1.SubreportToChange = Report1.GetNthSubreportName(0)
        '    Report1.Connect = STRRptConn
        '    Report1.SQLQuery = SqlStrSub
        '
        '    Report1.SubreportToChange = ""

        Report1.Action = 1
        Report1.Reset()
        Report1.ReportFileName = ""

    End Sub

    Private Sub txtChqDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtChqDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtChqDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtChqDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtChqDate.Text) = "" Then GoTo EventExitSub

        If Not IsDate(txtChqDate.Text) Then
            MsgInformation("Invalid Date.")
            Cancel = True
            GoTo EventExitSub
        End If

        '    If FYChk(txtChqDate.Text) = False Then
        '        Cancel = True
        '        Exit Sub
        '    End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtOthers_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOthers.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOthers_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOthers.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtOthers_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtOthers.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcGrid()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtRemarks_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRemarks.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtRemarks.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim mLeaveDate As String

        If Trim(txtEmpNo.Text) = "" Then
            MsgInformation("Please Enter Empcode First.")
            txtTo.Text = ""
            GoTo EventExitSub
        End If

        If Trim(txtTo.Text) = "" Then GoTo EventExitSub

        If Not IsDate(txtTo.Text) Then
            MsgInformation("Invalid Date.")
            Cancel = True
            GoTo EventExitSub
        End If

        If FYChk((txtTo.Text)) = False Then
            Cancel = True
            GoTo EventExitSub
        End If

        If MainClass.ValidateWithMasterTable(Trim(txtEmpNo.Text), "EMP_CODE", "EMP_LEAVE_DATE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mLeaveDate = MasterNo
        Else
            MsgInformation("Invaild Employee Code.")
            txtTo.Text = ""
            GoTo EventExitSub
        End If

        '    If PubUserID <> "G0416" Then
        '        If Trim(mLeaveDate) <> "" Then
        '            If CVDate(mLeaveDate) < CVDate(txtTo.Text) Then
        '                MsgInformation "Invalid To Date."
        '                Cancel = True
        '                Exit Sub
        '            End If
        '        End If
        '    End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
