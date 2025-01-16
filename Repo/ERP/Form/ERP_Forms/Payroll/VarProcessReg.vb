Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmVarProcessReg
    Inherits System.Windows.Forms.Form

    Dim SqlStr As String = ""
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection

    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12
    Private Const ColSNO As Short = 0
    Private Const ColCode As Short = 1
    Private Const ColDept As Short = 2
    Private Const ColName As Short = 3
    Private Const ColWDays As Short = 4
    Private Const ColBSalary As Short = 5
    Private Const ColGSalary As Short = 6
    Private Const ColPaidGSalary As Short = 7
    Private Const ColAddDays As Short = 8
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim FileDBCn As ADODB.Connection
    Private Function CalcBSalary(ByRef mCode As String, ByRef pGrossSalary As Double, ByRef pPaidGrossSalary As Double) As Double

        On Error GoTo ERR1
        Dim RSSalDef As ADODB.Recordset
        Dim mDate As String


        CalcBSalary = 0
        pGrossSalary = 0
        mDate = MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text))) & "/" & VB6.Format(lblRunDate.Text, "MM/YYYY")


        SqlStr = " SELECT BASICSALARY , FORM1_BASICSALARY from PAY_SalaryDef_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE='" & mCode & "'" & vbCrLf _
            & " AND SALARY_APP_DATE=(SELECT MAX(SALARY_APP_DATE) From PAY_SalaryDef_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE='" & mCode & "'" & vbCrLf _
            & " AND SALARY_APP_DATE<= TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSSalDef, ADODB.LockTypeEnum.adLockOptimistic)

        If RSSalDef.EOF = False Then
            CalcBSalary = MainClass.FormatRupees(IIf(IsDBNull(RSSalDef.Fields("BASICSALARY").Value), 0, RSSalDef.Fields("BASICSALARY").Value))
            pGrossSalary = IIf(IsDBNull(RSSalDef.Fields("BASICSALARY").Value), 0, RSSalDef.Fields("BASICSALARY").Value)
            pPaidGrossSalary = IIf(IsDBNull(RSSalDef.Fields("FORM1_BASICSALARY").Value), 0, RSSalDef.Fields("FORM1_BASICSALARY").Value)
        End If


        SqlStr = " SELECT SUM(AMOUNT) AS AMOUNT, SUM(FORM1_AMOUNT) AS FORM1_AMOUNT " & vbCrLf _
            & " FROM PAY_SALARYDEF_MST SALDEF, PAY_SALARYHEAD_MST SMAST " & vbCrLf _
            & " WHERE SALDEF.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SALDEF.COMPANY_CODE=SMAST.COMPANY_CODE " & vbCrLf _
            & " AND SALDEF.ADD_DEDUCTCODE=SMAST.CODE " & vbCrLf _
            & " AND SMAST.ADDDEDUCT=" & ConEarning & " " & vbCrLf _
            & " AND SALDEF.EMP_CODE='" & mCode & "'" & vbCrLf _
            & " AND SALDEF.SALARY_APP_DATE=(SELECT MAX(SALARY_APP_DATE) From PAY_SALARYDEF_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE='" & mCode & "'" & vbCrLf _
            & " AND SALARY_APP_DATE<= TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSSalDef, ADODB.LockTypeEnum.adLockOptimistic)

        If RSSalDef.EOF = False Then
            pGrossSalary = pGrossSalary + IIf(IsDBNull(RSSalDef.Fields("Amount").Value), 0, RSSalDef.Fields("Amount").Value)
            pPaidGrossSalary = pPaidGrossSalary + IIf(IsDBNull(RSSalDef.Fields("FORM1_AMOUNT").Value), 0, RSSalDef.Fields("FORM1_AMOUNT").Value)
        End If

        Exit Function
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function CalcVariable(ByRef mCode As String, ByRef mRow As Integer, ByRef mLICAmount As Double, ByRef mBankLoan As Double, ByRef ITAmount As Double) As Boolean

        On Error GoTo ERR1
        Dim RSSalVar As ADODB.Recordset
        Dim cntCol As Integer
        Dim mHeadTitle As String

        CalcVariable = True
        SqlStr = " SELECT MONTHLYTRN.*, " & vbCrLf & " ADD_DEDUCT.NAME, " & vbCrLf & " ADD_DEDUCT.ADDDEDUCT, ADD_DEDUCT.CALC_ON, ADD_DEDUCT.TYPE, " & vbCrLf & " ADD_DEDUCT.SEQ,DEFAULT_AMT " & vbCrLf & " FROM PAY_MONTHLY_TRN MONTHLYTRN, PAY_SALARYHEAD_MST ADD_DEDUCT " & vbCrLf & " WHERE " & vbCrLf & " MONTHLYTRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND MONTHLYTRN.COMPANY_CODE=ADD_DEDUCT.COMPANY_CODE" & vbCrLf & " AND MONTHLYTRN.COMPANY_CODE=ADD_DEDUCT.COMPANY_CODE" & vbCrLf & " AND MONTHLYTRN.ADD_DEDUCTCODE=ADD_DEDUCT.CODE" & vbCrLf & " AND MONTHLYTRN.EMP_Code='" & mCode & "'" & vbCrLf & " AND TO_CHAR(SAL_MONTH,'MON-YYYY')='" & UCase(VB6.Format(lblRunDate.Text, "MMM-YYYY")) & "'" & vbCrLf & " AND SAL_FLAG='" & lblSalType.Text & "'" & vbCrLf & " ORDER BY ADD_DEDUCT.SEQ "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSSalVar, ADODB.LockTypeEnum.adLockOptimistic)

        If RSSalVar.EOF = False Then
            Do While Not RSSalVar.EOF
                sprdMain.Col = ColAddDays
                sprdMain.Row = mRow
                sprdMain.Text = CStr(IIf(IsDBNull(RSSalVar.Fields("ADDDAYS").Value), "", RSSalVar.Fields("ADDDAYS").Value))

                For cntCol = ColAddDays + 1 To sprdMain.MaxCols
                    sprdMain.Col = cntCol
                    sprdMain.Row = 0
                    If Trim(UCase(RSSalVar.Fields("Name").Value)) = Trim(UCase(sprdMain.Text)) Then
                        sprdMain.Row = mRow
                        sprdMain.Text = MainClass.FormatRupees(IIf(IsDBNull(RSSalVar.Fields("Amount").Value), 0, RSSalVar.Fields("Amount").Value))
                        GoTo NextRec
                    End If
                Next
NextRec:
                RSSalVar.MoveNext()
            Loop
        Else
            For cntCol = ColAddDays + 1 To sprdMain.MaxCols
                sprdMain.Col = cntCol
                sprdMain.Row = 0
                If lblSalType.Text = "S" Then
                    mHeadTitle = Trim(UCase(sprdMain.Text))

                    If CheckVariableType(mHeadTitle, ConAdvance) = True Then
                        sprdMain.Row = mRow
                        sprdMain.Text = CStr(0) '' MainClass.FormatRupees(GetCurrMonthLoanAmount(mCode))
                    End If
                    If CheckVariableType(mHeadTitle, ConLoan) = True Then
                        sprdMain.Row = mRow
                        sprdMain.Text = MainClass.FormatRupees(mBankLoan)
                    End If
                    If CheckVariableType(mHeadTitle, ConLIC) = True Then
                        sprdMain.Row = mRow
                        sprdMain.Text = MainClass.FormatRupees(mLICAmount)
                    End If
                    If CheckVariableType(mHeadTitle, ConIncomeTax) = True Then
                        sprdMain.Row = mRow
                        sprdMain.Text = MainClass.FormatRupees(ITAmount)
                    End If
                End If
            Next
        End If
        Exit Function
ERR1:
        CalcVariable = False
    End Function
    Private Sub FillHeading()

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim cntCol As Integer
        Dim mAddDeduct As Integer

        MainClass.ClearGrid(sprdMain)

        With sprdMain
            .MaxCols = ColAddDays

            .Row = 0
            .set_RowHeight(0, ConRowHeight * 2)

            .Col = ColSNO
            .Text = "S. No."

            .Col = ColCode
            .Text = "Emp Card No"

            .Col = ColName
            .Text = "Employees' Name "
            .ColsFrozen = ColName

            .Col = ColDept
            .Text = "Employees' Dept "

            .Col = ColBSalary
            .Text = "Basic Salary"

            .Col = ColGSalary
            .Text = "Gross Salary"

            .Col = ColPaidGSalary
            .Text = "Paid Actual Salary"

            .Col = ColWDays
            .Text = "Working Salary"

            .Col = ColAddDays
            .Text = "Add. Days"

            SqlStr = " SELECT NAME,ADDDEDUCT " & vbCrLf & " FROM PAY_SALARYHEAD_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CALC_ON=" & ConCalcVariable & ""

            '        If RsCompany!PRINTOTINPAYSLIP = "N" Then
            '            SqlStr = SqlStr & vbCrLf & " AND TYPE<>" & ConOT & " "
            '        End If

            SqlStr = SqlStr & vbCrLf & " ORDER BY ADDDEDUCT,SEQ "

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

            If RsTemp.EOF = False Then
                .MaxCols = .MaxCols + 1
                cntCol = 1
                Do While Not RsTemp.EOF
                    .Col = ColAddDays + cntCol
                    .Text = RsTemp.Fields("Name").Value
                    cntCol = cntCol + 1
                    RsTemp.MoveNext()
                    If RsTemp.EOF = False Then
                        .MaxCols = .MaxCols + 1
                    End If
                Loop
            End If
            MainClass.ProtectCell(sprdMain, 0, sprdMain.MaxRows, 0, ColPaidGSalary)
        End With
    End Sub

    Private Sub chkALL_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboDept.Enabled = False
        Else
            cboDept.Enabled = True
        End If
    End Sub

    Private Sub chkCategory_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCategory.CheckStateChanged
        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboCategory.Enabled = False
        Else
            cboCategory.Enabled = True
        End If
    End Sub

    Private Sub chkGrade_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkGrade.CheckStateChanged
        If chkGrade.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboGrade.Enabled = False
        Else
            cboGrade.Enabled = True
        End If
    End Sub

    Private Sub cmdAdvance_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdvance.Click

        On Error GoTo ErrPart
        Dim cntCol As Integer
        Dim cntRow As Integer
        Dim mCode As String
        Dim mHeadTitle As String

        For cntRow = 1 To sprdMain.MaxRows - 1
            sprdMain.Row = cntRow
            sprdMain.Col = ColCode
            mCode = Trim(sprdMain.Text)

            For cntCol = ColAddDays + 1 To sprdMain.MaxCols
                sprdMain.Col = cntCol
                sprdMain.Row = 0
                If lblSalType.Text = "S" Then
                    mHeadTitle = Trim(UCase(sprdMain.Text))

                    If CheckVariableType(mHeadTitle, ConAdvance) = True Then
                        sprdMain.Row = cntRow
                        sprdMain.Text = MainClass.FormatRupees(GetCurrMonthLoanAmount(mCode))
                    End If
                End If
            Next
        Next
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
NormalExit:
    End Sub

    Private Sub cmdAttnAward_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAttnAward.Click

        On Error GoTo ErrPart
        Dim I As Integer
        Dim mCategory As String
        Dim mMonthDay As Integer
        Dim mLeaves As Integer
        Dim mAttnPer As Double
        Dim mAttnAward As Double
        Dim mEmpCode As String
        Dim mSalDate As String
        Dim mColAttnAward As Integer
        Dim mHeadName As String
        Dim mType As String
        Dim mEmpDOJ As String
        Dim mHoliday As Double
        Dim mDesgCode As String

        mColAttnAward = 0
        With sprdMain
            For I = ColAddDays + 1 To .MaxCols
                .Row = 0
                .Col = I
                mHeadName = Trim(.Text)
                If MainClass.ValidateWithMasterTable(mHeadName, "NAME", "TYPE", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mType = MasterNo
                End If
                If Val(mType) = ConAttendanceAllw Then
                    mColAttnAward = I
                    Exit For
                End If
            Next
        End With

        If mColAttnAward = 0 Then Exit Sub

        mSalDate = VB6.Format(lblRunDate.Text, "DD/MM/YYYY")
        mSalDate = MainClass.LastDay(Month(CDate(mSalDate)), Year(CDate(mSalDate))) & "/" & VB6.Format(mSalDate, "MM/YYYY")
        mMonthDay = MainClass.LastDay(Month(CDate(mSalDate)), Year(CDate(mSalDate))) ''& "/" & vb6.Format(mSalDate, "MM/YYYY")
        With sprdMain
            For I = 1 To .MaxRows - 2
                .Row = I
                .Col = ColCode
                mEmpCode = Trim(.Text)
                '            If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_CATG", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '                mCategory = MasterNo
                '            End If

                '            If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_DESG_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '                mDesgCode = MasterNo
                '            End If

                mDesgCode = GetDesgCode(RsCompany.Fields("COMPANY_CODE").Value, mEmpCode, mSalDate)

                If RsCompany.Fields("COMPANY_CODE").Value = 16 Then
                    If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_CAT_TYPE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mCategory = MasterNo
                    End If
                    '                If MainClass.ValidateWithMasterTable(mDesgCode, "DESG_CODE", "GRADE_CODE", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    '                    mCategory = MasterNo
                    '                End If
                    If VB.Left(mCategory, 1) = "2" Then ''Or Left(mCategory, 1) = "T" Then
                    Else
                        GoTo NextRow
                    End If
                Else
                    '                If MainClass.ValidateWithMasterTable(mDesgCode, "DESG_CODE", "GRADE_CODE", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    '                    mCategory = MasterNo
                    '                End If

                    If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_CATG", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mCategory = MasterNo
                    End If

                    If MainClass.ValidateWithMasterTable(mCategory, "CATEGORY_CODE", "CATEGORY_TYPE", "PAY_CATEGORY_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_TYPE='W'") = True Then

                    Else
                        GoTo NextRow
                    End If
                End If

                If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_DOJ", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mEmpDOJ = MasterNo
                End If

                mHoliday = GetMonthHolidays(mSalDate, mEmpCode, mEmpDOJ)
                mLeaves = CalcLeave(mEmpCode, mSalDate)
                mAttnPer = 100 - (mLeaves * 100 / (mMonthDay - mHoliday))
                mAttnAward = 0

                If mAttnPer = 100 Then
                    .Row = I
                    .Col = ColPaidGSalary
                    mAttnAward = VB6.Format(Val(.Text) / mMonthDay, "0")
                End If

                'If RsCompany.Fields("COMPANY_CODE").Value = 16 Then
                '    If mLeaves = 0 Then
                '        mAttnAward = 500
                '    ElseIf mLeaves = 1 Then
                '        mAttnAward = 300
                '    Else
                '        mAttnAward = 0
                '    End If
                'Else
                '    If RsCompany.Fields("COMPANY_CODE").Value = 1 And CDate(mSalDate) >= CDate("01/10/2019") Then
                '        If mAttnPer = 100 Then
                '            mAttnAward = 900
                '        ElseIf mAttnPer >= 90 Then
                '            mAttnAward = 700
                '        Else
                '            mAttnAward = 0
                '        End If
                '    ElseIf RsCompany.Fields("COMPANY_CODE").Value = 1 And CDate(mSalDate) >= CDate("01/01/2017") Then
                '        If mAttnPer = 100 Then
                '            mAttnAward = 700
                '        ElseIf mAttnPer >= 90 Then
                '            mAttnAward = 600
                '        Else
                '            mAttnAward = 0
                '        End If
                '    ElseIf RsCompany.Fields("COMPANY_CODE").Value = 1 And CDate(mSalDate) >= CDate("01/12/2013") Then
                '        If mAttnPer = 100 Then
                '            mAttnAward = 500
                '        ElseIf mAttnPer >= 90 Then
                '            mAttnAward = 300
                '        Else
                '            mAttnAward = 0
                '        End If
                '    Else
                '        If mAttnPer = 100 Then
                '            mAttnAward = 300
                '        ElseIf mAttnPer >= 90 Then
                '            mAttnAward = 150
                '        Else
                '            mAttnAward = 0
                '        End If
                '    End If
                'End If
                .Row = I
                .Col = mColAttnAward
                .Text = VB6.Format(mAttnAward, "0.00")
NextRow:
            Next
        End With
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Function CalcLeave(ByRef mCode As String, ByRef mSalDate As String) As Double

        On Error GoTo CalcAttnErr
        Dim RsTempAttn As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim mFHalf As Double
        Dim mSHalf As Double
        Dim mDate As String
        Dim xDOJ As Integer
        Dim xDOL As Integer


        mDate = MainClass.LastDay(Month(CDate(mSalDate)), Year(CDate(mSalDate))) & "/" & VB6.Format(mSalDate, "MM/YYYY")

        SqlStr = " SELECT FIRSTHALF,SECONDHALF " & vbCrLf _
            & " FROM PAY_ATTN_MST WHERE " & vbCrLf _
            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE = '" & mCode & "'" & vbCrLf & " AND TO_CHAR(Attn_Date,'MON-YYYY')=TO_CHAR('" & UCase(VB6.Format(mSalDate, "MMM-YYYY")) & "')"

        '    If IsDate(mAttnDate) = True Then
        '        SqlStr = SqlStr & vbCrLf & " AND Attn_Date>='" & UCase(Format(mAttnDate, "DD-MMM-YYYY")) & "'"
        '    End If
        '
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempAttn, ADODB.LockTypeEnum.adLockOptimistic)


        '    xDOJ = DateDiff("d", mDOJ, Format(mDate, "dd/mm/yyyy"))
        '    If mDOL <> "" Then
        '        xDOL = DateDiff("d", mDOL, Format(mDate, "dd/mm/yyyy"))
        '    End If
        '    If Format(mDOJ, "mm yyyy") = Format(mDOL, "mm yyyy") Then
        '        xDOJ = xDOJ - xDOL + 1
        '    ElseIf Format(mDOJ, "mm yyyy") = Format(mDate, "mm yyyy") Then
        '        xDOJ = xDOJ + 1
        '    ElseIf Format(mDOL, "mm yyyy") = Format(mDate, "mm yyyy") Then
        '        xDOJ = MainClass.LastDay(Month(mSalDate), Year(mSalDate)) - xDOL
        '    End If
        If RsTempAttn.EOF = False Then
            Do While Not RsTempAttn.EOF
                If RsTempAttn.Fields("FIRSTHALF").Value = ABSENT Or RsTempAttn.Fields("FIRSTHALF").Value = WOPAY Or RsTempAttn.Fields("FIRSTHALF").Value = CASUAL Or RsTempAttn.Fields("FIRSTHALF").Value = EARN Or RsTempAttn.Fields("FIRSTHALF").Value = SICK Or RsTempAttn.Fields("FIRSTHALF").Value = MATERNITY Then
                    mFHalf = mFHalf + 0.5
                End If

                If RsTempAttn.Fields("FIRSTHALF").Value = ABSENT Or RsTempAttn.Fields("FIRSTHALF").Value = WOPAY Or RsTempAttn.Fields("FIRSTHALF").Value = CASUAL Or RsTempAttn.Fields("FIRSTHALF").Value = EARN Or RsTempAttn.Fields("FIRSTHALF").Value = SICK Or RsTempAttn.Fields("FIRSTHALF").Value = MATERNITY Then
                    mSHalf = mSHalf + 0.5
                End If

                RsTempAttn.MoveNext()
            Loop
        End If

        '    If IsDate(mAttnDate) = True Then
        '        CalcAttn = pAddDate
        '    ElseIf MainClass.LastDay(Month(mSalDate), Year(mSalDate)) > xDOJ Then
        '        CalcAttn = xDOJ
        '    Else
        '        CalcAttn = MainClass.LastDay(Month(mSalDate), Year(mSalDate))
        '    End If

        CalcLeave = (mFHalf + mSHalf)
        Exit Function
CalcAttnErr:
        CalcLeave = 0
    End Function

    Private Sub cmdCanteenAmount_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCanteenAmount.Click

        On Error GoTo ErrPart
        Dim I As Integer
        Dim mCategory As String
        Dim mMonthDay As Integer
        Dim mLeaves As Integer
        Dim mCanteenAmount As Double
        Dim mEmpCode As String
        Dim mSalDate As String
        Dim mColCanteen As Integer
        Dim mHeadName As String
        Dim mType As String
        Dim mEmpDOJ As String
        Dim mHoliday As Double
        Dim mDesgCode As String
        Dim mDefaultAmount As Double
        Dim mPresentDays As Double

        mColCanteen = 0
        With sprdMain
            For I = ColAddDays + 1 To .MaxCols
                .Row = 0
                .Col = I
                mHeadName = Trim(.Text)
                If MainClass.ValidateWithMasterTable(mHeadName, "NAME", "TYPE", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mType = MasterNo
                End If
                If Val(mType) = ConCanteen Then
                    mColCanteen = I
                    Exit For
                End If
            Next
        End With

        If mColCanteen = 0 Then Exit Sub

        mDefaultAmount = 0

        If MainClass.ValidateWithMasterTable(mHeadName, "NAME", "DEFAULT_AMT", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDefaultAmount = Val(MasterNo)
        End If

        mSalDate = VB6.Format(lblRunDate.Text, "DD/MM/YYYY")
        mSalDate = MainClass.LastDay(Month(CDate(mSalDate)), Year(CDate(mSalDate))) & "/" & VB6.Format(mSalDate, "MM/YYYY")
        mMonthDay = MainClass.LastDay(Month(CDate(mSalDate)), Year(CDate(mSalDate))) ''& "/" & vb6.Format(mSalDate, "MM/YYYY")
        With sprdMain
            For I = 1 To .MaxRows - 2
                .Row = I
                .Col = ColCode
                mEmpCode = Trim(.Text)

                If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_DOJ", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mEmpDOJ = MasterNo
                End If
                mPresentDays = CheckPresentAttnData(mEmpCode, mSalDate)
                mCanteenAmount = mPresentDays * mDefaultAmount        ''CountCanteenTime(mEmpCode, mSalDate) * mDefaultAmount

                '            mHoliday = GetMonthHolidays(mSalDate, mEMPDOJ)
                '            mLeaves = CalcLeave(mEmpCode, mSalDate)

                .Row = I
                .Col = mColCanteen
                .Text = VB6.Format(mCanteenAmount, "0.00")
NextRow:
            Next
        End With
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Function CountCanteenTime(ByRef mEmpCode As String, ByRef mToDate As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mEmpInTime As String
        Dim mEmpOutTime As String
        Dim mFromDate As String
        Dim mEmpInTimeStr As String
        Dim mEmpOutTimeStr As String
        Dim mIsRound As String
        Dim mTotTime As Double
        Dim mIsHoliday As Boolean
        Dim mHolidayType As String
        Dim mSalDate As String
        Dim mCanteenData As String

        mEmpInTime = ""
        mEmpOutTime = ""
        mFromDate = "01/" & VB6.Format(mToDate, "MM/YYYY")

        '    If RsCompany.Fields("COMPANY_CODE").Value = 12 Then
        '        mCanteenData = "CANTEEN.TEMPDATA"
        '    Else
        mCanteenData = "CANTEENTEMPDATA"
        '    End If

        If CDbl(VB6.Format(lblYear.Text, "YYYYMM")) < 201310 Then
            SqlStr = " SELECT ATTN_DATE,IN_TIME, OUT_TIME " & vbCrLf & " FROM PAY_DALIY_ATTN_TRN ATTN " & vbCrLf & " WHERE ATTN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND ATTN.EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf & " AND ATTN_DATE>=TO_DATE('" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND ATTN_DATE<=TO_DATE('" & VB6.Format(mToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

            If RsTemp.EOF = False Then
                Do While RsTemp.EOF = False
                    mEmpInTime = VB6.Format(IIf(IsDBNull(RsTemp.Fields("IN_TIME").Value), "", RsTemp.Fields("IN_TIME").Value), "HH:MM")
                    mEmpOutTime = VB6.Format(IIf(IsDBNull(RsTemp.Fields("OUT_TIME").Value), "", RsTemp.Fields("OUT_TIME").Value), "HH:MM")
                    mSalDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("ATTN_DATE").Value), "", RsTemp.Fields("ATTN_DATE").Value), "DD/MM/YYYY")

                    mEmpInTimeStr = VB6.Format(DateSerial(Year(CDate(mSalDate)), Month(CDate(mSalDate)), VB.Day(CDate(mSalDate))) & " " & TimeSerial(Hour(CDate(mEmpInTime)), Minute(CDate(mEmpInTime)), 0), "DD/MM/YYYY HH:MM")
                    mIsRound = "N"
                    If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "ROUND_CLOCK", "PAY_SHIFT_TRN", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SHIFT_DATE=TO_DATE('" & VB6.Format(mEmpInTimeStr, "DD-MMM-YYYY") & "','DD-MON-YYYY')") = True Then
                        mIsRound = Trim(MasterNo)
                    End If

                    mEmpOutTimeStr = VB6.Format(IIf(IsDBNull(RsTemp.Fields("OUT_TIME").Value), "", RsTemp.Fields("OUT_TIME").Value), "DD/MM/YYYY HH:MM")

                    If mIsRound = "Y" Then
                        mEmpOutTimeStr = VB6.Format(DateSerial(Year(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mSalDate))), Month(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mSalDate))), VB.Day(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, CDate(mSalDate)))) & " " & TimeSerial(Hour(CDate(mEmpOutTime)), Minute(CDate(mEmpOutTime)), 0), "DD/MM/YYYY HH:MM")
                    Else
                        mEmpOutTimeStr = VB6.Format(DateSerial(Year(CDate(mSalDate)), Month(CDate(mSalDate)), VB.Day(CDate(mSalDate))) & " " & TimeSerial(Hour(CDate(mEmpOutTime)), Minute(CDate(mEmpOutTime)), 0), "DD/MM/YYYY HH:MM")
                    End If
                    mTotTime = DateDiff(Microsoft.VisualBasic.DateInterval.Hour, CDate(mEmpInTimeStr), CDate(mEmpOutTimeStr))

                    mIsHoliday = GetIsHolidays(VB6.Format(RsTemp.Fields("ATTN_DATE").Value, "DD/MM/YYYY"), mHolidayType, mEmpCode, "", "Y")

                    If mIsHoliday = False Then
                        '                If mTotTime >= 4 And CDate(mEmpInTime) <= "17:30" Then
                        '                    CountCanteenTime = CountCanteenTime + IIf(mTotTime > 12.3, 2, 1)
                        '                End If
                        If mEmpInTime = "00:00" And mEmpOutTime = "00:00" Then

                        Else
                            If mTotTime > 12 Then
                                If CDate(mEmpOutTime) <= CDate("19:00") Then
                                    CountCanteenTime = CountCanteenTime + 1
                                Else
                                    CountCanteenTime = CountCanteenTime + 2
                                End If
                            Else
                                If mTotTime >= 4 And CDate(mEmpInTime) <= CDate("19:00") Then
                                    CountCanteenTime = CountCanteenTime + 1
                                End If
                            End If
                        End If
                    End If
                    RsTemp.MoveNext()
                Loop
            End If
        Else
            SqlStr = " SELECT COUNT(1) AS ATTN_COUNT FROM " & mCanteenData & " " & vbCrLf & " WHERE TRIM(CARDNO)='" & Trim(mEmpCode) & "'" & vbCrLf & " AND TO_CHAR(OFFICEPUNCH,'YYYYMMDD') >= '" & VB6.Format(mFromDate, "YYYYMMDD") & "'" & vbCrLf & " AND TO_CHAR(OFFICEPUNCH,'YYYYMMDD')<='" & VB6.Format(mToDate, "YYYYMMDD") & "'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
            If RsTemp.EOF = False Then
                CountCanteenTime = IIf(IsDBNull(RsTemp.Fields("ATTN_COUNT").Value), 0, RsTemp.Fields("ATTN_COUNT").Value)
            End If
        End If
        Exit Function
ErrPart:
        CountCanteenTime = 0
    End Function
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub
    Private Function Update1() As Boolean
        On Error GoTo UpdateError
        Dim cntRow As Integer
        Dim mCode As String
        Dim mSalary As Double

        SqlStr = ""
        PubDBCn.BeginTrans()

        For cntRow = 1 To sprdMain.MaxRows
            sprdMain.Col = ColCode
            sprdMain.Row = cntRow
            mCode = sprdMain.Text

            sprdMain.Col = ColBSalary
            If IsNumeric(sprdMain.Text) Then
                mSalary = CDbl(sprdMain.Text)
                If UpdateMonthTrn(mCode, mSalary, cntRow) = False Then GoTo UpdateError
            End If
        Next

        PubDBCn.CommitTrans()

        Update1 = True
        Exit Function
UpdateError:
        Update1 = False
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        '    Resume
        PubDBCn.Errors.Clear()
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function UpdateMonthTrn(ByRef xCode As String, ByRef xSalary As Double, ByRef xRow As Integer) As Boolean


        On Error GoTo UpdateMonthTrnErr
        Dim SqlStr As String = ""
        Dim cntCol As Integer
        Dim xMonth As String
        Dim xTypeCode As Integer
        Dim xLoanType As Integer
        Dim xAmount As Double
        Dim xAddDays As Double
        Dim mMonthDate As String
        Dim RsTemp As ADODB.Recordset = Nothing

        xMonth = UCase(VB6.Format(lblRunDate.Text, "MMM-YYYY"))
        mMonthDate = "01-" & xMonth


        SqlStr = " DELETE FROM PAY_MONTHLY_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & xCode & "'" & vbCrLf & " AND TO_CHAR(SAL_Month,'MON-YYYY')='" & xMonth & "' AND SAL_FLAG='" & lblSalType.Text & "'"

        PubDBCn.Execute(SqlStr)

        SqlStr = ""

        sprdMain.Col = ColAddDays
        sprdMain.Row = xRow
        xAddDays = CDbl(IIf(IsNumeric(sprdMain.Text), sprdMain.Text, 0))

        For cntCol = ColAddDays + 1 To sprdMain.MaxCols
            sprdMain.Row = 0
            sprdMain.Col = cntCol

            SqlStr = " SELECT CODE,TYPE FROM PAY_SALARYHEAD_MST" & vbCrLf _
                & " WHERE  COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND NAME = '" & MainClass.AllowSingleQuote(Trim(sprdMain.Text)) & "'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                xTypeCode = RsTemp.Fields("Code").Value
                xLoanType = RsTemp.Fields("Type").Value

                sprdMain.Row = xRow
                xAmount = CDbl(IIf(IsNumeric(sprdMain.Text), sprdMain.Text, 0))
            Else
                GoTo NextCol
            End If

            SqlStr = " INSERT INTO PAY_MONTHLY_TRN ( " & vbCrLf _
                & " COMPANY_CODE, PAYYEAR, " & vbCrLf _
                & " EMP_CODE, BASICSALARY, " & vbCrLf _
                & " SAL_MONTH, ADD_DEDUCTCODE, PERCENTAGE, AMOUNT, ADDDAYS,SAL_FLAG) VALUES ( " & vbCrLf _
                & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & Year(CDate(lblRunDate.Text)) & ", " & vbCrLf _
                & " '" & xCode & "', " & xSalary & ", " & vbCrLf _
                & " TO_DATE('" & VB6.Format(mMonthDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & xTypeCode & ", 0, " & vbCrLf & " " & xAmount & "," & xAddDays & ",'" & lblSalType.Text & "') "

            PubDBCn.Execute(SqlStr)

            '06-04-2007
            '        If lblSalType.Caption = "S" Then
            '            If UpdateLoanMaster(xCode, xTypeCode, xLoanType, mMonthDate, Month(lblRunDate.Caption), Year(lblRunDate.Caption), xAmount) = False Then GoTo UpdateMonthTrnErr
            '        End If
NextCol:
        Next
        UpdateMonthTrn = True
        Exit Function
UpdateMonthTrnErr:
        ''Resume
        MsgBox(Err.Description)
        UpdateMonthTrn = False
    End Function
    Private Function UpdateLoanMaster(ByRef xCode As String, ByRef xTypeCode As Integer, ByRef xLoanType As Integer, ByRef xDeductDate As String, ByRef xMonth As Short, ByRef xYear As Short, ByRef xAmount As Double) As Boolean


        On Error GoTo ErrUpdateLoanMaster
        Dim RsBalLoan As ADODB.Recordset
        Dim mCurrMonAmount As Double
        Dim SqlStr As String = ""
        Dim xMkey As String = ""
        Dim xLOANAMOUNT As Double
        Dim xLOANDATE As Date
        Dim xSTARTINGMONTH As Short
        Dim xSTARTINGYEAR As Short
        Dim xINSTALMENTAMT As Double
        Dim xINTERESTCALC As String
        Dim xINTERESTRATE As Double
        Dim xDeductAMT As Double
        Dim xThisMonthAMT As Double
        Dim mBalAmount As Double
        Dim mPaidAmount As Double
        Dim mTotPaidAmount As Double
        Dim mTotBalAmount As Double
        Dim mCurrBalAmount As Double

        ''Insert Balnce Amount
        SqlStr = ""
        SqlStr = " SELECT MKey,LOANAMOUNT,LOANDATE, " & vbCrLf & " STARTINGMONTH,STARTINGYEAR,INSTALMENTAMT, " & vbCrLf & " INTERESTCALC,INTERESTRATE, SUM(DEDUCT_AMOUNT) AS DEDUCT_AMOUNT, " & vbCrLf & " SUM(BALANCE_AMOUNT) AS BALANCE_AMOUNT,SUM(PAID_AMOUNT) AS PAID_AMOUNT FROM PAY_LOAN_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf & " EMP_CODE='" & xCode & "' AND " & vbCrLf & " ADD_DEDUCTCODE=" & xTypeCode & " AND " & vbCrLf & " LOANTYPE=" & xLoanType & " " & vbCrLf & " GROUP BY MKey,LOANAMOUNT,LOANDATE, " & vbCrLf & " STARTINGMONTH,STARTINGYEAR,INSTALMENTAMT, " & vbCrLf & " INTERESTCALC,INTERESTRATE "

        ''& " AND DEDUCT_DATE<=TO_DATE('" & xDeductDate & "') " & vbCrLf _
        '
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBalLoan, ADODB.LockTypeEnum.adLockOptimistic)
        With RsBalLoan
            If .EOF = False Then
                Do While Not .EOF
                    xMkey = IIf(IsDBNull(.Fields("mKey").Value), 0, .Fields("mKey").Value)
                    xLOANAMOUNT = IIf(IsDBNull(.Fields("LOANAMOUNT").Value), 0, .Fields("LOANAMOUNT").Value)
                    xLOANDATE = IIf(IsDate(.Fields("LoanDate").Value), .Fields("LoanDate").Value, ConBlankDate)
                    xSTARTINGMONTH = IIf(IsDBNull(.Fields("StartingMonth").Value), 0, .Fields("StartingMonth").Value)
                    xSTARTINGYEAR = IIf(IsDBNull(.Fields("StartingYear").Value), 0, .Fields("StartingYear").Value)
                    xINSTALMENTAMT = IIf(IsDBNull(.Fields("InstalmentAmt").Value), 0, .Fields("InstalmentAmt").Value)
                    xDeductAMT = IIf(IsDBNull(.Fields("Deduct_Amount").Value), 0, .Fields("Deduct_Amount").Value)
                    mTotPaidAmount = IIf(IsDBNull(.Fields("PAID_AMOUNT").Value), 0, .Fields("PAID_AMOUNT").Value)
                    xThisMonthAMT = IIf(IsDBNull(.Fields("Balance_Amount").Value), 0, .Fields("Balance_Amount").Value)
                    xINTERESTCALC = IIf(IsDBNull(.Fields("InterestCalc").Value), "N", .Fields("InterestCalc").Value)
                    xINTERESTRATE = IIf(IsDBNull(.Fields("InterestRate").Value), 0, .Fields("InterestRate").Value)


                    mCurrMonAmount = GETCURRLOANAMT(xMkey, "PAID_AMOUNT", xDeductDate, xCode)
                    mCurrBalAmount = GETCURRLOANAMT(xMkey, "BALANCE_AMOUNT", xDeductDate, xCode)

                    mBalAmount = xDeductAMT - mTotPaidAmount + mCurrMonAmount

                    '                If xAmount > mBalAmount And mBalAmount = 0 Then
                    '                    mPaidAmount = xAmount
                    '                    mBalAmount = mTotPaidAmount - xDeductAMT
                    '                    xAmount = 0
                    '                Else
                    If xAmount > mBalAmount Then
                        mPaidAmount = mBalAmount
                        xAmount = xAmount - mBalAmount
                    Else
                        mPaidAmount = xAmount
                        mBalAmount = IIf(mPaidAmount = 0, mBalAmount, mPaidAmount)
                        xAmount = 0
                    End If
                    mTotBalAmount = xDeductAMT - (xThisMonthAMT - (mCurrBalAmount - mPaidAmount))

                    SqlStr = " UPDATE PAY_LOAN_MST SET " & vbCrLf & " BALANCE_AMOUNT=" & mBalAmount & ", PAID_AMOUNT=" & mPaidAmount & " " & vbCrLf & " WHERE MKEY='" & xMkey & "' AND " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf & " EMP_CODE='" & xCode & "' AND " & vbCrLf & " ADD_DEDUCTCODE=" & xTypeCode & " AND " & vbCrLf & " LOANTYPE=" & xLoanType & " AND  " & vbCrLf & " DEDUCT_DATE=TO_DATE('" & xDeductDate & "')"

                    PubDBCn.Execute(SqlStr)

                    If mTotBalAmount > 0 Then
                        If CheckBalLoanAmt(xCode, xTypeCode, xMkey, xLOANAMOUNT, xLOANDATE, xLoanType, xDeductDate, xSTARTINGMONTH, xSTARTINGYEAR, xINSTALMENTAMT, xINTERESTCALC, xINTERESTRATE, mTotBalAmount) = False Then GoTo ErrUpdateLoanMaster
                    End If
                    If ReCalcLoanMaster(xCode, xMkey, xLOANAMOUNT) = False Then GoTo ErrUpdateLoanMaster
                    .MoveNext()
                Loop

                If xAmount > 0 Then
                    SqlStr = " UPDATE PAY_LOAN_MST SET " & vbCrLf & " PAID_AMOUNT=PAID_AMOUNT+" & xAmount & " " & vbCrLf & " WHERE MKEY='" & xMkey & "' AND " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf & " EMP_CODE='" & xCode & "' AND " & vbCrLf & " ADD_DEDUCTCODE=" & xTypeCode & " AND " & vbCrLf & " LOANTYPE=" & xLoanType & " AND  " & vbCrLf & " DEDUCT_DATE=TO_DATE('" & xDeductDate & "')"

                    PubDBCn.Execute(SqlStr)
                End If

            End If
        End With
        UpdateLoanMaster = True
        Exit Function
ErrUpdateLoanMaster:
        UpdateLoanMaster = False
    End Function

    Private Sub cmdExcel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdExcel.Click
        On Error GoTo ErrPart
        Dim strFilePath As String

        frmShowVarDed.ShowDialog()

        If G_ShowVar = False Then
            Exit Sub
        End If

        Call PopulateFromXLSFile(frmShowVarDed.lblFilePath.Text, (frmShowVarDed.txtDeductionName).Text)

        frmShowVarDed.Close()

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
NormalExit:
    End Sub
    Private Sub PopulateFromXLSFile(ByVal strXLSFile As String, ByRef mDeductionName As String)
        On Error GoTo ErrPart
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mEmpCode As String
        Dim mAmount As Double
        Dim xSqlStr As String
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsFile As ADODB.Recordset
        Dim FileConnStr As String

        Dim strTemp As String
        Dim strWkShName As String
        Dim strError As String
        Dim cntRow As Integer
        Dim mCol As Integer

        Dim cntCol As Integer
        '    MainClass.ClearGrid SprdMain
        '    FormatSprdMain -1

        With sprdMain
            For cntCol = 1 To .MaxCols
                .Row = 0
                .Col = cntCol
                If Trim(UCase(.Text)) = Trim(UCase(mDeductionName)) Then
                    mCol = cntCol
                    Exit For
                End If
            Next
        End With
        FileConnStr = "Provider=MSDASQL.1;Connect Timeout=15;Extended Properties='DSN=Excel Files;DBQ=XXLSFILEX;DefaultDir=XXLSDIRX;DriverId=790;FIL=excel 8.0;MaxBufferSize=2048;PageTimeout=5;UID=admin;';Locale Identifier=1033"
        FileConnStr = Replace(FileConnStr, "XXLSFILEX", strXLSFile)
        strTemp = Mid(strXLSFile, 1, InStrRev(strXLSFile, "\") - 1)
        FileConnStr = Replace(FileConnStr, "XXLSDIRX", strTemp)

        If Not XLSConnect(Trim(FileConnStr), FileDBCn) Then
            GoTo ErrPart
        End If

        RsFile = FileDBCn.OpenSchema(ADODB.SchemaEnum.adSchemaTables)
        strWkShName = RsFile.Fields("Table_Name").Value

        mSqlStr = "SELECT * FROM ""XWKSHTX"" " ''WHERE F1 <> NULL"
        mSqlStr = Replace(mSqlStr, "XWKSHTX", strWkShName)

        If OpenExcelRecordSet(mSqlStr, RsFile, strError, FileDBCn, False) = 0 Then

            If RsFile.EOF = False Then
                Do While Not RsFile.EOF
                    mEmpCode = Trim(IIf(IsDBNull(RsFile.Fields(0).Value), "", RsFile.Fields(0).Value))
                    mAmount = Val(IIf(IsDBNull(RsFile.Fields(2).Value), 0, RsFile.Fields(2).Value))

                    For cntRow = 1 To sprdMain.MaxRows
                        sprdMain.Row = cntRow
                        sprdMain.Col = ColCode
                        If UCase(Trim(sprdMain.Text)) = UCase(Trim(mEmpCode)) Then    '' If VB6.Format(Trim(sprdMain.Text), "000000") = VB6.Format(Trim(mEmpCode), "000000") Then
                            sprdMain.Row = cntRow
                            sprdMain.Col = mCol
                            sprdMain.Text = VB6.Format(mAmount, "0.00")
                            Exit For
                        End If
                    Next

NextRecord:
                    RsFile.MoveNext()
                Loop
            End If
        End If

        If RsFile.State = ADODB.ObjectStateEnum.adStateOpen Then RsFile.Close()
        RsFile = Nothing

        If FileDBCn.State = ADODB.ObjectStateEnum.adStateOpen Then
            FileDBCn.Close()
            FileDBCn = Nothing
        End If

        '    CmdPopFromFile.Enabled = False
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '        Resume
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click

        On Error GoTo ErrorHandler
        Dim mYM As Integer

        If lblSalType.Text = "S" Then
            If ValidateBookLocking(PubDBCn, CInt(ConLockEmpSalaryProcess), VB6.Format(lblYear.Text, "DD/MM/YYYY")) = True Then
                Exit Sub
            End If
        Else
            If ValidateBookLocking(PubDBCn, CInt(ConLockEmpOTProcess), VB6.Format(lblYear.Text, "DD/MM/YYYY")) = True Then
                Exit Sub
            End If
        End If
        '    If PubSuperUser = "U" Then
        mYM = CInt(VB6.Format(Year(CDate(lblRunDate.Text)), "0000") & VB6.Format(Month(CDate(lblRunDate.Text)), "00"))
        If SalAlreadyProcess(mYM) = False Then
            MsgBox("You are enable to process. ", MsgBoxStyle.Critical)
            Exit Sub
        End If
        '    End If

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)

        Dim mAuthorisation As String

        mAuthorisation = IIf(InStr(1, XRIGHT, "S") > 0, "Y", "N")
        If mAuthorisation = "N" Then
            MsgBox("You have no Right to Save it. ", MsgBoxStyle.Critical)
            Exit Sub
        End If

        '    If XRIGHT <> "AMDVS" Then
        '        MsgInformation "You have not Rights to Save it."
        '        Exit Sub
        '    End If


        If Update1() = True Then
            cmdSave.Enabled = False
        Else
            cmdSave.Enabled = True
            MsgInformation("Record not saved")
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub

    Private Function SalAlreadyProcess(ByRef mYM As Integer) As Boolean

        On Error GoTo ErrSalProcess
        Dim RsMain As ADODB.Recordset
        Dim SqlStr As String = ""

        SalAlreadyProcess = True
        SqlStr = " SELECT EMP_CODE FROM PAY_SAL_TRN WHERE " & vbCrLf & " TO_CHAR(SAL_DATE,'YYYYMM') >= " & mYM & "" & vbCrLf & " AND Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ISARREAR IN ('N','Y')"

        '        If lblEmpType.Caption = "S" Then
        SqlStr = SqlStr & vbCrLf & " AND CATEGORY IN (" & vbCrLf & " SELECT DISTINCT CATEGORY_CODE  " & vbCrLf & " FROM PAY_CATEGORY_MST  " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CATEGORY_TYPE='" & lblEmpType.Text & "')"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMain, ADODB.LockTypeEnum.adLockOptimistic)

        If RsMain.EOF = False Then
            SalAlreadyProcess = False
        End If
        Exit Function
ErrSalProcess:
        SalAlreadyProcess = False
    End Function

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForDeduction(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportForDeduction(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim All As Boolean
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String


        PubDBCn.Errors.Clear()


        'Insert Data from Grid to PrintDummyData Table...


        If FillPrintDummyData(sprdMain, 0, sprdMain.MaxRows, ColCode, sprdMain.MaxCols, PubDBCn) = False Then GoTo ERR1


        'Select Record for print...

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)

        mSubTitle = "For the Month : " & MonthName(Month(CDate(lblRunDate.Text))) & ", " & Year(CDate(lblRunDate.Text))
        mTitle = "Deduction List "
        If lblSalType.Text = "O" Then
            mTitle = mTitle & "(Over Time)"
        ElseIf lblSalType.Text = "E" Then
            mTitle = mTitle & "(Encashment)"
        ElseIf lblSalType.Text = "C" Then
            mTitle = mTitle & "(CPL)"
        End If

        Call ShowReport(SqlStr, "MonthlyVar.Rpt", Mode, mTitle, mSubTitle)

        Exit Sub
ERR1:
        'Resume
        MsgInformation(Err.Description)
        If Err.Number = 32755 Or Err.Number = 20507 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        'Resume
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForDeduction(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdRefresh_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRefresh.Click

        SetDate(CDate(lblRunDate.Text))
        MainClass.ClearGrid(sprdMain)

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboDept.Text = "" Then
                MsgInformation("Please select the Department Name.")
                cboDept.Focus()
                Exit Sub
            End If
        End If

        RefreshScreen()
        CmdPreview.Enabled = True
        cmdPrint.Enabled = True
    End Sub

    Private Sub cmdWelFare_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdWelfare.Click

        On Error GoTo ErrPart
        Dim cntCol As Integer
        Dim mCol As Integer
        Dim mDefaultValue As Double
        Dim cntRow As Integer
        Dim mDeductName As String
        Dim mEmpCode As String
        Dim xWEF As String
        Dim mDays As Double
        Dim mGrossSalary As Double
        Dim xWelfareSalaryAmt As Double
        Dim mMonthDays As Integer

        xWEF = VB6.Format(lblRunDate.Text, "DD/MM/YYYY")
        mMonthDays = MainClass.LastDay(Month(CDate(lblRunDate.Text)), Year(CDate(lblRunDate.Text)))

        cntCol = 0
        With sprdMain
            For cntCol = ColAddDays + 1 To .MaxCols
                .Row = 0
                .Col = cntCol
                mDeductName = Trim(UCase(.Text))


                If MainClass.ValidateWithMasterTable(mDeductName, "NAME", "DEFAULT_AMT", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CALC_ON=" & ConCalcVariable & " AND PAYMENT_TYPE='M' and TYPE='" & ConWelfare & "' AND STATUS='O'") = True Then
                    mCol = cntCol
                    mDefaultValue = Val(MasterNo)
                    Exit For
                End If

            Next
        End With

        If cntCol <> 0 Then
            With sprdMain
                For cntRow = 1 To .MaxRows
                    .Row = cntRow
                    .Col = ColCode
                    mEmpCode = Trim(UCase(.Text))


                    If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        .Row = cntRow
                        .Col = ColWDays
                        mDays = CDbl(VB6.Format(.Text, "0.00"))

                        .Col = ColGSalary
                        mGrossSalary = CDbl(VB6.Format(.Text, "0.00"))

                        xWelfareSalaryAmt = mGrossSalary * mDays / mMonthDays
                        mDefaultValue = GetWelfareAmount(xWEF, xWelfareSalaryAmt)
                        .Col = mCol
                        .Text = VB6.Format(mDefaultValue, "0.00")
                    End If
                Next
            End With
        End If

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
NormalExit:
        '    Resume
    End Sub

    Private Sub frmVarProcessReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        If FormActive = True Then Exit Sub
        If lblSalType.Text = "S" Then
            Me.Text = "Monthly Salary Variable Process"
        ElseIf lblSalType.Text = "A" Then
            Me.Text = "Monthly Arrear Variable Process"
        ElseIf lblSalType.Text = "O" Then
            Me.Text = "Monthly (Over Time) Variable Process"
        ElseIf lblSalType.Text = "E" Then
            Me.Text = "Leave Encashment Variable Process"
        ElseIf lblSalType.Text = "C" Then
            Me.Text = "CPL Variable Process"
        End If

        Me.Text = Me.Text & IIf(Trim(lblEmpType.Text) = "S", " (Staff)", IIf(Trim(lblEmpType.Text) = "W", " (Worker)", ""))

        FillDeptCombo()
        FormActive = True
    End Sub
    Private Sub frmVarProcessReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        CurrFormHeight = 7245
        CurrFormWidth = 11355
        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)
        lblRunDate.Text = CStr(RunDate)
        OptName.Checked = True
        SetDate(CDate(lblRunDate.Text))
        FillHeading()
        FormatSprd(-1)

        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        cboDept.Enabled = False
        chkCategory.CheckState = System.Windows.Forms.CheckState.Checked
        cboCategory.Enabled = False

        chkGrade.CheckState = System.Windows.Forms.CheckState.Checked
        cboGrade.Enabled = False

        cmdAttnAward.Enabled = IIf(RsCompany.Fields("COMPANY_CODE").Value = 1 Or RsCompany.Fields("COMPANY_CODE").Value = 16, True, False)
        cmdAttnAward.Visible = IIf(RsCompany.Fields("COMPANY_CODE").Value = 1 Or RsCompany.Fields("COMPANY_CODE").Value = 16, True, False)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '    Resume
    End Sub
    Private Sub frmVarProcessReg_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        sprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth))

        Frame1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        CurrFormWidth = mReFormWidth

        '    MainClass.SetSpreadColor SprdMain, -1
        '    MainClass.SetSpreadColor SprdOption, -1
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles sprdMain.LeaveCell
        '    cmdSave.Enabled = True
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim TotCol As Double

        If eventArgs.col >= ColAddDays + 1 Then
            TotCol = 0
            With sprdMain
                For cntRow = 1 To .MaxRows - 2
                    .Col = eventArgs.col
                    .Row = cntRow
                    TotCol = TotCol + IIf(IsNumeric(.Text), .Text, 0)
                Next
                .Col = eventArgs.col
                .Row = .MaxRows
                .Text = VB6.Format(TotCol, "0.00")
            End With
        End If
        If PubUserID = "G0416" Then
            cmdSave.Enabled = True
        End If
        Exit Sub
ErrPart:

    End Sub
    Private Sub RefreshScreen()

        On Error GoTo ErrRefreshScreen

        Dim RsEmpSal As ADODB.Recordset
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mCode As String
        Dim mMonth As Short
        Dim mYear As Short
        Dim mYYMM As Integer
        Dim mDOJ As String
        Dim mDOL As String
        Dim mDept As String
        Dim mBasicSalary As Double
        Dim mWDays As Double
        Dim mGrossSalary As Double
        Dim mPaidGrossSalary As Double

        Dim mEmpDOJ As String
        Dim mEmpDOL As String
        Dim mTotalWop_Absent As Double
        Dim mWOP As Double
        Dim mAbsent As Double
        Dim mStartDate As String

        mMonth = Month(CDate(lblRunDate.Text))
        mYear = Year(CDate(lblRunDate.Text))
        mYYMM = Val(Str(mYear) & VB6.Format(mMonth, "00"))


        mDOJ = MainClass.LastDay(mMonth, mYear) & "/" & mMonth & "/" & mYear
        mDOL = "01" & "/" & mMonth & "/" & mYear
        mStartDate = "01" & "/" & mMonth & "/" & mYear

        SqlStr = " SELECT EMP.EMP_NAME,EMP.EMP_CODE,EMP_DEPT_CODE,EMP.BNKLOAN_DED, EMP.LIC_DED, EMP.ITAX_DED,EMP.EMP_DOJ,EMP.EMP_LEAVE_DATE " & vbCrLf _
            & " FROM PAY_EMPLOYEE_MST EMP, PAY_DESG_MST DMST " & vbCrLf _
            & " WHERE EMP.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP.COMPANY_CODE =DMST.COMPANY_CODE " & vbCrLf _
            & " AND GETEMPDESG (EMP.COMPANY_CODE, EMP.EMP_CODE, TO_DATE('" & VB6.Format(mDOJ, "DD-MMM-YYYY") & "','DD-MON-YYYY')) = DMST.DESG_DESC " & vbCrLf _
            & " AND EMP.EMP_DOJ <=TO_DATE('" & VB6.Format(mDOJ, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND (EMP.EMP_LEAVE_DATE >TO_DATE('" & VB6.Format(mDOL, "dd-mmm-yyyy") & "','DD-MON-YYYY') OR EMP.EMP_LEAVE_DATE IS NULL) " & vbCrLf _
            & " AND EMP.EMP_STOP_SALARY='N'"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDept = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(Trim(mDept)) & "' "
            End If
        End If

        '    SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CODE='000003' "

        If Trim(lblEmpType.Text) = "S" Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CAT_TYPE='1' "
        ElseIf Trim(lblEmpType.Text) = "W" Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CAT_TYPE='2' "
        End If

        If chkCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CATG<>'C' "
        ElseIf chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked And cboCategory.SelectedIndex <> -1 Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CATG='" & VB.Left(cboCategory.Text, 1) & "' "
        End If

        If chkGrade.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND DMST.GRADE_CODE='" & Trim(cboGrade.Text) & "' "
            ' GETEMPDESG (mCompanyCode NUMBER, mEmpCode CHAR, mSalDate Char)
        End If

        SqlStr = SqlStr & vbCrLf _
            & " AND EMP.EMP_CODE NOT IN (SELECT EMP_CODE FROM " & vbCrLf _
            & " PAY_FFSETTLE_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND TO_CHAR(EMP_LEAVE_DATE,'MM-YYYY')='" & VB6.Format(mDOL, "MM-YYYY") & "' AND PAID_DAYS>0)"


        If OptName.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order by EMP.EMP_NAME"
        ElseIf optCardNo.Checked = True Then
            SqlStr = SqlStr & vbCrLf & "Order by EMP.EMP_CODE"
        Else
            SqlStr = SqlStr & vbCrLf & "Order by EMP.EMP_DEPT_CODE,EMP.EMP_CODE"
        End If


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmpSal, ADODB.LockTypeEnum.adLockOptimistic)

        If RsEmpSal.EOF = False Then
            With sprdMain
                cntRow = 1
                .MaxRows = cntRow
                Do While Not RsEmpSal.EOF
                    .Row = cntRow

                    .Col = ColCode
                    mCode = RsEmpSal.Fields("EMP_CODE").Value
                    .Text = CStr(mCode)

                    .Col = ColName
                    .Text = RsEmpSal.Fields("EMP_NAME").Value

                    .Col = ColDept
                    .Text = IIf(IsDBNull(RsEmpSal.Fields("EMP_DEPT_CODE").Value), "", RsEmpSal.Fields("EMP_DEPT_CODE").Value)

                    mBasicSalary = CalcBSalary(mCode, mGrossSalary, mPaidGrossSalary)
                    mEmpDOJ = IIf(IsDBNull(RsEmpSal.Fields("EMP_DOJ").Value), "", RsEmpSal.Fields("EMP_DOJ").Value)
                    mEmpDOL = IIf(IsDBNull(RsEmpSal.Fields("EMP_LEAVE_DATE").Value), "", RsEmpSal.Fields("EMP_LEAVE_DATE").Value)
                    mTotalWop_Absent = 0
                    mWOP = 0
                    mAbsent = 0

                    mWDays = CalcAttn(mCode, mEmpDOJ, mEmpDOL, mStartDate, mTotalWop_Absent, , , mWOP, mAbsent)

                    .Col = ColWDays
                    .Text = VB6.Format(mWDays, "0.00")

                    .Col = ColBSalary
                    .Text = VB6.Format(mBasicSalary, "0.00")

                    .Col = ColGSalary
                    .Text = VB6.Format(mGrossSalary, "0.00")

                    .Col = ColPaidGSalary
                    .Text = VB6.Format(mPaidGrossSalary, "0.00")


                    If CalcVariable(mCode, cntRow, IIf(IsDBNull(RsEmpSal.Fields("LIC_DED").Value), 0, RsEmpSal.Fields("LIC_DED").Value), IIf(IsDBNull(RsEmpSal.Fields("BNKLOAN_DED").Value), 0, RsEmpSal.Fields("BNKLOAN_DED").Value), IIf(IsDBNull(RsEmpSal.Fields("ITAX_DED").Value), 0, RsEmpSal.Fields("ITAX_DED").Value)) = False Then GoTo NextRow

NextRow:
                    cntRow = cntRow + 1
                    RsEmpSal.MoveNext()
                    If RsEmpSal.EOF = False Then
                        .MaxRows = .MaxRows + 1
                    End If
                Loop

                ColTotal(sprdMain, ColAddDays + 1, .MaxCols)
                .Col = ColName
                .Row = .MaxRows
                .Text = "TOTAL :"

                FormatSprd(-1)

                MainClass.ProtectCell(sprdMain, .MaxRows, .MaxRows, 0, .MaxCols)
            End With
        End If
        cmdSave.Enabled = True
        Exit Sub

ErrRefreshScreen:
        MsgInformation(Err.Description)
    End Sub
    Private Sub FillDeptCombo()

        Dim RsDept As ADODB.Recordset = Nothing

        SqlStr = "Select DEPT_DESC FROM PAY_DEPT_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " Order by DEPT_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockOptimistic)

        cboDept.Items.Clear()
        If RsDept.EOF = False Then
            Do While Not RsDept.EOF
                cboDept.Items.Add(RsDept.Fields("DEPT_DESC").Value)
                RsDept.MoveNext()
            Loop

            cboDept.SelectedIndex = 0
        End If

        SqlStr = "Select GRADE_CODE FROM PAY_GRADE_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " Order by GRADE_CODE"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockOptimistic)

        cboGrade.Items.Clear()
        If RsDept.EOF = False Then
            Do While Not RsDept.EOF
                cboGrade.Items.Add(RsDept.Fields("GRADE_CODE").Value)
                RsDept.MoveNext()
            Loop
            cboGrade.SelectedIndex = 0
        End If

        SqlStr = "Select CATEGORY_DESC FROM PAY_CATEGORY_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If lblSalType.Text = "S" Or lblSalType.Text = "A" Then
            If lblEmpType.Text = "S" Then
                SqlStr = SqlStr & vbCrLf & " AND CATEGORY_TYPE='S'"
            Else
                SqlStr = SqlStr & vbCrLf & " AND CATEGORY_TYPE='W'"
            End If
        End If

        SqlStr = SqlStr & vbCrLf & " Order by CATEGORY_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockOptimistic)

        cboCategory.Items.Clear()
        If RsDept.EOF = False Then
            Do While Not RsDept.EOF
                cboCategory.Items.Add(RsDept.Fields("CATEGORY_DESC").Value)
                RsDept.MoveNext()
            Loop
        End If
        cboCategory.SelectedIndex = 0

        '    cboCategory.Clear
        '    cboCategory.AddItem "General Staff"
        '    cboCategory.AddItem "Production Staff"
        '    cboCategory.AddItem "Export Staff"
        '    cboCategory.AddItem "Regular Worker"
        '    cboCategory.AddItem "Staff R & D"
        ''    cboCategory.AddItem "Contratcor Staff"
        '    cboCategory.AddItem "Director"
        '    cboCategory.AddItem "Trainee Staff"
        '    cboCategory.ListIndex = 0

        Exit Sub

ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub SetDate(ByRef xDate As Date)

        Dim Daysinmonth As Integer
        Dim Tempdate As String
        Dim NewDate As Date

        Tempdate = "01/" & Month(xDate) & "/" & Year(xDate)
        NewDate = CDate(VB6.Format(Tempdate, "dd/mm/yyyy"))
        lblRunDate.Text = CStr(NewDate)

        lblYear.Text = MonthName(Month(NewDate)) & ", " & Year(NewDate)

        Daysinmonth = MainClass.LastDay(VB6.Format(xDate, "mm"), VB6.Format(xDate, "yyyy"))
    End Sub
    Private Function FillDataIntoPrintDummy(ByRef GridName As AxFPSpreadADO.AxfpSpread, ByVal prmStartGridRow As Integer, ByVal prmEndGridRow As Integer, ByRef mPaymentType As String) As Boolean
        ' This procedure fills the Grid Data into PrintDummy table for printing...
        On Error GoTo PrintDummyErr

        FillDataIntoPrintDummy = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Function
PrintDummyErr:
        FillDataIntoPrintDummy = False
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Sub FormatSprd(ByRef mRow As Integer)

        Dim cntCol As Integer

        On Error GoTo ERR1
        With sprdMain
            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight)

            .set_ColWidth(ColSNO, 4)

            .Col = ColCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColCode, 6)

            .Col = ColName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(ColName, 22)

            .Col = ColWDays
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMin = CDbl("-99.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColWDays, 6)

            .Col = ColBSalary
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColBSalary, 8)

            .Col = ColGSalary
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColGSalary, 8)

            .Col = ColPaidGSalary
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColPaidGSalary, 8)

            .Col = ColAddDays
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColAddDays, 6)

            For cntCol = ColAddDays + 1 To .MaxCols
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("9999999.99")
                .TypeFloatMin = CDbl("-9999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
                .set_ColWidth(cntCol, 8)
            Next

        End With
        MainClass.ProtectCell(sprdMain, 0, sprdMain.MaxRows, 0, ColPaidGSalary)
        MainClass.SetSpreadColor(sprdMain, mRow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub

    Private Function CheckVariableType(ByRef mSalHeadName As String, ByRef mVariableType As Integer) As Boolean

        On Error GoTo ErrCheck
        Dim RsCheck As ADODB.Recordset
        CheckVariableType = False

        SqlStr = " SELECT TYPE FROM PAY_SALARYHEAD_MST WHERE " & vbCrLf & " NAME = '" & MainClass.AllowSingleQuote(mSalHeadName) & "' AND " & vbCrLf & " TYPE IN (" & mVariableType & ") AND Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCheck, ADODB.LockTypeEnum.adLockOptimistic)
        If RsCheck.EOF = False Then
            CheckVariableType = True
        End If
        Exit Function
ErrCheck:
        CheckVariableType = False
        MsgBox(Err.Description)
    End Function
    Private Function GetCurrMonthLoanAmount(ByRef mCode As String) As Double

        On Error GoTo ErrCheck
        Dim RsCheck As ADODB.Recordset
        Dim mMonth As Short
        Dim mYear As Short

        GetCurrMonthLoanAmount = 0
        mMonth = CShort(VB6.Format(lblRunDate.Text, "MM"))
        mYear = CShort(VB6.Format(lblRunDate.Text, "YYYY"))

        SqlStr = " SELECT SUM(LOANMASTER.DEDUCT_AMOUNT) AS DEDUCT_AMOUNT" & vbCrLf & " FROM PAY_LOAN_MST LOANMASTER" & vbCrLf & " WHERE " & vbCrLf & " LOANMASTER.Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND LOANMASTER.EMP_CODE = '" & mCode & "'" & vbCrLf & " AND TO_CHAR(LOANMASTER.DEDUCT_DATE,'MMYYYY')=" & VB6.Format(lblRunDate.Text, "MMYYYY") & "" '& vbCrLf |            & " AND LOANMASTER.DEDUCT_YEAR = " & mYear & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCheck, ADODB.LockTypeEnum.adLockOptimistic)
        If RsCheck.EOF = False Then
            GetCurrMonthLoanAmount = IIf(IsDBNull(RsCheck.Fields("Deduct_Amount").Value), 0, RsCheck.Fields("Deduct_Amount").Value)
        End If
        Exit Function
ErrCheck:
        GetCurrMonthLoanAmount = 0
        MsgBox(Err.Description)
    End Function



    Private Function CheckBalLoanAmt(ByRef xCode As String, ByRef xTypeCode As Integer, ByRef xMKey As String, ByRef xLOANAMOUNT As Double, ByRef xLOANDATE As Date, ByRef xLoanType As Integer, ByRef xDeductDate As String, ByRef xSTARTINGMONTH As Short, ByRef xSTARTINGYEAR As Short, ByRef xINSTALMENTAMT As Double, ByRef xINTERESTCALC As String, ByRef xINTERESTRATE As Double, ByRef xBalAmount As Double) As Boolean

        On Error GoTo ErrInsertBalLoan
        Dim RsLoan As ADODB.Recordset

        Dim mSubKey As Integer
        Dim mDEDUCT_MONTH As Short
        Dim mDEDUCT_YEAR As Short
        Dim mDEDUCT_AMOUNT As Double
        Dim mLoanType As Integer
        Dim mSubRowNo As Integer
        Dim xDate As String

        CheckBalLoanAmt = True



        Call GetMaxDeductDate(xCode, xMKey, xDate, mSubRowNo)
        xDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, CDate(xDate)))
        mSubRowNo = mSubRowNo + 1
        mDEDUCT_MONTH = Month(CDate(xDate))
        mDEDUCT_YEAR = Year(CDate(xDate))

        SqlStr = " INSERT INTO PAY_LOAN_MST (" & vbCrLf & " MKEY, COMPANY_CODE, FYEAR, " & vbCrLf & " EMP_CODE, SUBROWNO, ADD_DEDUCTCODE, LOANTYPE , " & vbCrLf & " LOANAMOUNT, LOANDATE, DEDUCT_AMOUNT, DEDUCT_DATE, " & vbCrLf & " DEDUCT_MONTH, DEDUCT_YEAR, " & vbCrLf & " STARTINGMONTH, STARTINGYEAR, INSTALMENTAMT, " & vbCrLf & " INTERESTCALC,INTERESTRATE,BALANCE_AMOUNT,PAID_AMOUNT ) VALUES " & vbCrLf & " ('" & xMKey & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " " & RsCompany.Fields("FYEAR").Value & ",'" & xCode & "', " & mSubRowNo & ", " & vbCrLf & " " & xTypeCode & "," & xLoanType & ", " & vbCrLf & " " & xLOANAMOUNT & ",TO_DATE('" & VB6.Format(xLOANDATE, "dd-mmm-yyyy") & "','DD-MON-YYYY')), " & vbCrLf & " 0, TO_DATE('" & VB6.Format(xDate, "dd-mmm-yyyy") & "','DD-MON-YYYY'), " & " " & mDEDUCT_MONTH & "," & mDEDUCT_YEAR & ", " & vbCrLf & " " & xSTARTINGMONTH & ", " & xSTARTINGYEAR & " ," & vbCrLf & " " & xINSTALMENTAMT & ",'" & xINTERESTCALC & "', " & xINTERESTRATE & ", "

        If xBalAmount > 0 Then
            SqlStr = SqlStr & vbCrLf & " " & xBalAmount & ",0)"
        Else
            SqlStr = SqlStr & vbCrLf & " 0," & System.Math.Abs(xBalAmount) & ")"
        End If

        PubDBCn.Execute(SqlStr)


        Exit Function
ErrInsertBalLoan:
        MsgInformation(Err.Description)
        CheckBalLoanAmt = False
        '    Resume

    End Function

    Private Function CalcMonthYear(ByRef xSubkey As Integer, ByRef mType As String) As Short
        Dim mMonth As Short
        Dim mYear As Short
        xSubkey = CInt(VB6.Format(xSubkey, "000000"))
        mMonth = CShort(Mid(CStr(xSubkey), 5, 6))
        mYear = CShort(Mid(CStr(xSubkey), 1, 4))
        If mType = "M" Then
            CalcMonthYear = IIf(mMonth > 12, 1, mMonth)
        ElseIf mType = "Y" Then
            CalcMonthYear = IIf(mMonth > 12, mYear + 1, mYear)
        End If
    End Function

    Private Function ReCalcLoanMaster(ByRef xCode As String, ByRef xMKey As String, ByRef xLOANAMOUNT As Double) As Boolean

        On Error GoTo ErrReCalcLoanMaster
        Dim RsReLoan As ADODB.Recordset
        Dim mBalanceAmount As Double

        ReCalcLoanMaster = True

        mBalanceAmount = xLOANAMOUNT
        SqlStr = " SELECT BALANCE_AMOUNT,DEDUCT_DATE FROM PAY_LOAN_MST " & vbCrLf & " WHERE MKEY='" & xMKey & "' AND" & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf & " EMP_CODE='" & xCode & "'" & vbCrLf & " ORDER BY DEDUCT_DATE "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsReLoan, ADODB.LockTypeEnum.adLockOptimistic)
        If RsReLoan.EOF = False Then
            Do While Not RsReLoan.EOF
                mBalanceAmount = mBalanceAmount - IIf(IsDBNull(RsReLoan.Fields("Balance_Amount").Value), 0, RsReLoan.Fields("Balance_Amount").Value)
                If mBalanceAmount < 0 Then
                    SqlStr = " DELETE FROM PAY_LOAN_MST " & vbCrLf & " WHERE MKEY='" & xMKey & "' AND " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf & " EMP_CODE='" & xCode & "' AND " & vbCrLf & " DEDUCT_DATE = TO_DATE('" & VB6.Format(RsReLoan.Fields("DEDUCT_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
                    PubDBCn.Execute(SqlStr)
                End If
                RsReLoan.MoveNext()
            Loop
        End If
        Exit Function
ErrReCalcLoanMaster:
        ReCalcLoanMaster = False
    End Function

    Private Sub GetMaxDeductDate(ByRef xCode As String, ByRef xMKey As String, ByRef xDate As String, ByRef xSubRow As Integer)

        On Error GoTo ErrPart
        Dim RsBalLoan As ADODB.Recordset

        SqlStr = ""
        SqlStr = " SELECT Max(DEDUCT_DATE) AS DDate,Max(SUBROWNO) AS SUBROWNO" & vbCrLf & " FROM PAY_LOAN_MST " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf & " EMP_CODE='" & xCode & "' AND " & vbCrLf & " MKEY=" & xMKey & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBalLoan, ADODB.LockTypeEnum.adLockOptimistic)

        If RsBalLoan.EOF = False Then
            xDate = IIf(IsDBNull(RsBalLoan.Fields("DDate").Value), "", RsBalLoan.Fields("DDate").Value)
            xSubRow = IIf(IsDBNull(RsBalLoan.Fields("SUBROWNO").Value), "-1", RsBalLoan.Fields("SUBROWNO").Value)
        End If
        Exit Sub
ErrPart:
        xDate = ""
        xSubRow = -1
    End Sub

    Private Function GETCURRLOANAMT(ByRef xMKey As String, ByRef mField As String, ByRef xDeductDate As String, ByRef xCode As String) As Double

        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset = Nothing

        GETCURRLOANAMT = 0
        SqlStr = ""
        SqlStr = " SELECT " & mField & " " & vbCrLf & " FROM PAY_LOAN_MST " & vbCrLf & " WHERE MKEY='" & xMKey & "' AND" & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf & " EMP_CODE='" & xCode & "' AND " & vbCrLf & " DEDUCT_DATE=TO_DATE('" & xDeductDate & "') "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
        With RsTemp
            If .EOF = False Then
                GETCURRLOANAMT = IIf(IsDBNull(RsTemp.Fields(mField).Value), 0, RsTemp.Fields(mField).Value)
            End If
        End With

        Exit Function
ErrPart:
        GETCURRLOANAMT = 0
    End Function

    Private Sub lblYear_TextChanged(sender As Object, e As EventArgs) Handles lblYear.TextChanged
        lblRunDate.Text = VB6.Format(lblYear.Text, "DD/MM/YYYY")
        SetDate(CDate(lblRunDate.Text))
    End Sub

    Private Sub cmdProfessionalTax_Click(sender As Object, e As EventArgs) Handles cmdProfessionalTax.Click

        On Error GoTo ErrPart
        Dim I As Integer

        Dim mEmpCode As String
        Dim mSalDate As String
        Dim mGrossSalary As Double
        Dim mColProfessionalTax As Long
        Dim mHeadName As String
        Dim mType As String
        Dim mProfessionalTax As Double = 0
        Dim mWDays As Double

        mColProfessionalTax = 0
        With sprdMain
            For I = ColAddDays + 1 To .MaxCols
                .Row = 0
                .Col = I
                mHeadName = Trim(.Text)
                If MainClass.ValidateWithMasterTable(mHeadName, "NAME", "TYPE", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mType = MasterNo
                End If
                If Val(mType) = ConProfessionalTax Then
                    mColProfessionalTax = I
                    Exit For
                End If
            Next
        End With

        If mColProfessionalTax = 0 Then Exit Sub


        With sprdMain
            For I = 1 To .MaxRows - 2
                .Row = I
                .Col = ColCode
                mEmpCode = Trim(.Text)

                .Col = ColWDays
                mWDays = Val(.Text)

                .Col = ColGSalary
                mGrossSalary = Val(.Text)

                mProfessionalTax = 0

                If mEmpCode <> "" And mGrossSalary > 0 And mWDays > 0 Then
                    If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
                        If RsCompany.Fields("COMPANY_CODE").Value = 4 Then
                            If mGrossSalary <= 3500 Then
                                mProfessionalTax = 0
                            ElseIf mGrossSalary <= 5000 Then
                                mProfessionalTax = 17
                            ElseIf mGrossSalary <= 7500 Then
                                mProfessionalTax = 42
                            ElseIf mGrossSalary <= 10000 Then
                                mProfessionalTax = 85
                            ElseIf mGrossSalary <= 12500 Then
                                mProfessionalTax = 125
                            ElseIf mGrossSalary > 12500 Then
                                mProfessionalTax = 169
                            End If
                        ElseIf RsCompany.Fields("COMPANY_CODE").Value = 5 Then
                            If mGrossSalary <= 3500 Then
                                mProfessionalTax = 0
                            ElseIf mGrossSalary <= 5000 Then
                                mProfessionalTax = 21
                            ElseIf mGrossSalary <= 7500 Then
                                mProfessionalTax = 53
                            ElseIf mGrossSalary <= 10000 Then
                                mProfessionalTax = 103
                            ElseIf mGrossSalary <= 12500 Then
                                mProfessionalTax = 155
                            ElseIf mGrossSalary > 12500 Then
                                mProfessionalTax = 205
                            End If
                        End If
                    End If
                End If


                .Row = I
                .Col = mColProfessionalTax
                .Text = VB6.Format(mProfessionalTax, "0.00")
NextRow:
            Next
        End With
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
End Class
