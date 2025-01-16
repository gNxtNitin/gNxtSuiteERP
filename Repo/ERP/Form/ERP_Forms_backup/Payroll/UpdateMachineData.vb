Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmUpdateMachineData
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    'Dim PvtDBCn As Connection
    Private Const RowHeight As Short = 12

    Dim mActiveRow As Integer

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.hide()
    End Sub


    Private Sub cmdOk_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOK.Click
        On Error GoTo DataErr

        PBar.Visible = True
        lblStatus.Visible = True


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Me.Height = VB6.TwipsToPixelsY(4020)
        cmdOK.Enabled = False

        If Update1 = False Then GoTo DataErr


        PBar.Visible = False
        lblStatus.Text = "Successfully Update."

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub

DataErr:
        '    Resume
        MsgInformation(" Unable to Processed")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        PBar.Visible = False
        lblStatus.Text = ""
        lblStatus.Visible = False
        Me.Height = VB6.TwipsToPixelsY(3015)
        ''Resume Next
    End Sub
    Private Sub frmUpdateMachineData_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmUpdateMachineData_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(3015)
        Me.Width = VB6.TwipsToPixelsX(5775)
        txtDateFrom.Text = CStr(RunDate)
        txtDateTo.Text = CStr(RunDate)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub



    Private Function Update1() As Boolean
        On Error GoTo ErrPart1

        If FieldsVarification = False Then GoTo ErrPart1

        If chkEmployee.CheckState = System.Windows.Forms.CheckState.Checked Then
            If UpdateEmployee = False Then GoTo ErrPart1
        End If

        Update1 = True
        Exit Function
ErrPart1:
        '    Resume
        If Err.Description <> "" Then
            MsgBox(Err.Description)
        End If
        Update1 = False
    End Function

    Private Function UpdateEmployee() As Boolean

        On Error GoTo UpdateError
        Dim SqlStr As String = ""
        Dim RsEmp As ADODB.Recordset = Nothing

        Dim mActive As String
        Dim mStopSalary As String
        Dim mEmpType As String
        Dim mPayCode As String
        Dim mEmpName As String
        Dim mFName As String
        Dim mDOB As String
        Dim mDOJ As String
        Dim mDeptAlias As String
        Dim mDeptCode As String
        Dim mCategory As String
        Dim mSex As String
        Dim mMaritalStatus As String
        Dim mQualification As String
        Dim mExperience As String
        Dim mDesgAlias As String
        Dim mDesgCode As String
        Dim mADD1 As String
        Dim mPinCode1 As String
        Dim mTele1 As String
        Dim mEMail As String
        Dim mADD2 As String
        Dim mPinCode2 As String
        Dim mTele2 As String
        Dim mLeavingDate As String
        Dim mLeavingReason As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "SELECT * FROM SAVIOR40.tblEmployee WHERE COMPANYCODE =" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsEmp.EOF = False Then
            Do While Not RsEmp.EOF

                mActive = IIf(IsDbNull(RsEmp.Fields("ACTIVE").Value), "Y", RsEmp.Fields("ACTIVE").Value)
                mStopSalary = IIf(mActive = "Y", "N", "Y")
                mPayCode = IIf(IsDbNull(RsEmp.Fields("PAYCODE").Value), "", RsEmp.Fields("PAYCODE").Value)
                mEmpName = IIf(IsDbNull(RsEmp.Fields("EMPNAME").Value), "", RsEmp.Fields("EMPNAME").Value)
                mFName = IIf(IsDbNull(RsEmp.Fields("GUARDIANNAME").Value), "", RsEmp.Fields("GUARDIANNAME").Value)
                mDOB = IIf(IsDbNull(RsEmp.Fields("DATEOFBIRTH").Value), "", RsEmp.Fields("DATEOFBIRTH").Value)
                mDOJ = IIf(IsDbNull(RsEmp.Fields("DATEOFJOIN").Value), "", RsEmp.Fields("DATEOFJOIN").Value)
                mDeptCode = IIf(IsDbNull(RsEmp.Fields("DEPARTMENTCODE").Value), "", RsEmp.Fields("DEPARTMENTCODE").Value)
                mCategory = IIf(IsDbNull(RsEmp.Fields("CAT").Value), "", RsEmp.Fields("CAT").Value)
                mSex = IIf(IsDbNull(RsEmp.Fields("SEX").Value), "M", RsEmp.Fields("SEX").Value)
                mMaritalStatus = IIf(IsDbNull(RsEmp.Fields("ISMARRIED").Value), "N", RsEmp.Fields("ISMARRIED").Value)
                mQualification = IIf(IsDbNull(RsEmp.Fields("QUALIFICATION").Value), "", RsEmp.Fields("QUALIFICATION").Value)
                mExperience = IIf(IsDbNull(RsEmp.Fields("EXPERIENCE").Value), "", RsEmp.Fields("EXPERIENCE").Value)
                mDesgAlias = IIf(IsDbNull(RsEmp.Fields("DESIGNATION").Value), "", RsEmp.Fields("DESIGNATION").Value)
                mADD1 = IIf(IsDbNull(RsEmp.Fields("ADDRESS1").Value), "", RsEmp.Fields("ADDRESS1").Value)
                mPinCode1 = IIf(IsDbNull(RsEmp.Fields("PINCODE1").Value), "", RsEmp.Fields("PINCODE1").Value)
                mTele1 = IIf(IsDbNull(RsEmp.Fields("TELEPHONE1").Value), "", RsEmp.Fields("TELEPHONE1").Value)
                mEMail = IIf(IsDbNull(RsEmp.Fields("E_MAIL1").Value), "", RsEmp.Fields("E_MAIL1").Value)
                mADD2 = IIf(IsDbNull(RsEmp.Fields("ADDRESS2").Value), "", RsEmp.Fields("ADDRESS2").Value)
                mPinCode2 = IIf(IsDbNull(RsEmp.Fields("PINCODE2").Value), "", RsEmp.Fields("PINCODE2").Value)
                mTele2 = IIf(IsDbNull(RsEmp.Fields("TELEPHONE2").Value), "", RsEmp.Fields("TELEPHONE2").Value)
                mLeavingDate = IIf(IsDbNull(RsEmp.Fields("LEAVINGDATE").Value), "", RsEmp.Fields("LEAVINGDATE").Value)
                mLeavingReason = IIf(IsDbNull(RsEmp.Fields("LEAVINGREASON").Value), "", RsEmp.Fields("LEAVINGREASON").Value)


                mEmpType = "P"

                If MainClass.ValidateWithMasterTable(mPayCode, "EMP_CODE", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                    SqlStr = "INSERT INTO PAY_EMPLOYEE_MST ( " & vbCrLf & " COMPANY_CODE, EMP_TYPE, EMP_CODE,  " & vbCrLf & " EMP_NAME, EMP_ADDR, EMP_CITY,  " & vbCrLf & " EMP_STATE, EMP_PIN, EMP_PHONE_NO,  " & vbCrLf & " EMP_MOBILE_NO, EMP_EMAILID, EMP_CONTACT_PERSON,  " & vbCrLf & " EMP_DEPT_CODE, EMP_MARITAL_STATUS, EMP_SEX,  " & vbCrLf & " EMP_DESG_CODE, EMP_LAST_COMPANY, EMP_QUALIFICATION,  " & vbCrLf & " EMP_TOTEXP, EMP_DOB, EMP_DOJ,  " & vbCrLf & " SHIFT_CODE, SALARY_TYPE, EMP_DOC,  " & vbCrLf & " EMP_PF_ACNO, EMP_PF_DATE, EMP_LEAVE_DATE,  " & vbCrLf & " EMP_LEAVE_REASON, EMP_BANK_NO, EMP_ESI_FLAG,  " & vbCrLf & " EMP_PROH_EXT, COST_CENTER_CODE, EMP_CATG,  " & vbCrLf & " GROSS_SALARY, BASIC_SALARY, HRA_ALW,  " & vbCrLf & " HRA_ALW_PERCENT, CONV_ALW, CHILD_EDU_ALW,  " & vbCrLf & " CHILD_EDU_ALW_PERCENT, OTHERS1_ALW, OTHERS2_ALW,  " & vbCrLf & " OTHERS3_ALW, PF_DED, ESI_DED,  "

                    SqlStr = SqlStr & vbCrLf & " ADV_LOAN_DED, BNKLOAN_DED, LIC_DED,  " & vbCrLf & " ITAX_DED, OTHER_DED, CPF_PER,  " & vbCrLf & " BONUS_PER, LTA_AMT, LTA_PER,  " & vbCrLf & " IMPREST_DED, SALARY_EFF_DATE, CONV_ALW_PERCENT,  " & vbCrLf & " EMP_FNAME, EMP_BANK_NAME, WORKINGTIMEFROM,  " & vbCrLf & " WORKINGTIMETO, EMP_OT_RATE, EMP_SPOUSE_NAME,  " & vbCrLf & " EMP_ESI_NO, ESI_DISPENSARY, EMP_PANNO,  " & vbCrLf & " EMP_LICNO, WEEKLYOFF, JOININGDESIGN,  " & vbCrLf & " PAYMENTMODE, EMP_GROUP_INSURANCE, EMP_STOP_SALARY,  " & vbCrLf & " ADV_ACCOUNT_CODE, IMPREST_ACCOUNT_CODE, " & vbCrLf & " ADDUSER, ADDDATE,ISMETROCITY ) " & vbCrLf & " VALUES ( "

                    SqlStr = SqlStr & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ",'" & mEmpType & "', '" & mPayCode & "',  " & vbCrLf & " '" & MainClass.AllowSingleQuote(mEmpName) & "', '" & MainClass.AllowSingleQuote(mADD1) & "', '" & MainClass.AllowSingleQuote(mADD2) & "',  " & vbCrLf & " '', '" & MainClass.AllowSingleQuote(mPinCode1) & "', '" & MainClass.AllowSingleQuote(mTele1) & "',  " & vbCrLf & " '', '" & MainClass.AllowSingleQuote(mEMail) & "', '',  " & vbCrLf & " '" & mDeptCode & "', '" & mMaritalStatus & "', '" & mSex & "',  " & vbCrLf & " '" & mDesgCode & "', '', '" & MainClass.AllowSingleQuote(mQualification) & "',  " & vbCrLf & " " & Val(mExperience) & ", TO_DATE('" & VB6.Format(mDOB, "DD-MMM-YYYY") & "','DD-MON-YYYY'), TO_DATE('" & VB6.Format(mDOJ, "DD-MMM-YYYY") & "','DD-MON-YYYY'),  " & vbCrLf & " '', '" & mEmpType & "', TO_DATE('" & VB6.Format(mDOJ, "DD-MMM-YYYY") & "','DD-MON-YYYY'),  " & vbCrLf & " '', '', TO_DATE('" & VB6.Format(mLeavingDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),  " & vbCrLf & " '" & MainClass.AllowSingleQuote(mLeavingReason) & "', '', 'N',  " & vbCrLf & " '', '001', '" & mCategory & "',  " & vbCrLf & " 0, 0, 0,  " & vbCrLf & " 0, 0, 0,  " & vbCrLf & " 0, 0, 0,  " & vbCrLf & " 0, 0, 0,  "

                    SqlStr = SqlStr & vbCrLf & " 0, 0, 0,  " & vbCrLf & " 0, 0, 0,  " & vbCrLf & " 15, 0, 0,  " & vbCrLf & " 0, TO_DATE('" & VB6.Format(mDOJ, "DD-MMM-YYYY") & "','DD-MON-YYYY'), 0,  " & vbCrLf & " '" & MainClass.AllowSingleQuote(mFName) & "', '', '9.00',  " & vbCrLf & " '5.30', 2, '',  " & vbCrLf & " '', '', '',  " & vbCrLf & " '', 'SUNDAY', '',  " & vbCrLf & " 1, 'N', '" & mStopSalary & "',  " & vbCrLf & " '', '', " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " 'N') "
                Else

                    SqlStr = "UPDATE  PAY_EMPLOYEE_MST SET " & vbCrLf & " EMP_CODE='" & mPayCode & "',  " & vbCrLf & " EMP_NAME='" & MainClass.AllowSingleQuote(mEmpName) & "', " & vbCrLf & " EMP_ADDR='" & MainClass.AllowSingleQuote(mADD1) & "', " & vbCrLf & " EMP_CITY='" & MainClass.AllowSingleQuote(mADD2) & "',  " & vbCrLf & " EMP_PIN='" & MainClass.AllowSingleQuote(mPinCode1) & "', " & vbCrLf & " EMP_PHONE_NO='" & MainClass.AllowSingleQuote(mTele1) & "' , " & vbCrLf & " EMP_EMAILID='" & MainClass.AllowSingleQuote(mEMail) & "', " & vbCrLf & " EMP_DEPT_CODE='" & mDeptCode & "', " & vbCrLf & " EMP_MARITAL_STATUS='" & mMaritalStatus & "', " & vbCrLf & " EMP_SEX='" & mSex & "' , " & vbCrLf & " EMP_DESG_CODE='" & mDesgCode & "', " & vbCrLf & " EMP_QUALIFICATION='" & MainClass.AllowSingleQuote(mQualification) & "',  " & vbCrLf & " EMP_TOTEXP=" & Val(mExperience) & ", " & vbCrLf & " EMP_DOB=TO_DATE('" & VB6.Format(mDOB, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " EMP_DOJ=TO_DATE('" & VB6.Format(mDOJ, "DD-MMM-YYYY") & "','DD-MON-YYYY'),  " & vbCrLf & " EMP_LEAVE_DATE=TO_DATE('" & VB6.Format(mLeavingDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),  " & vbCrLf & " EMP_LEAVE_REASON='" & MainClass.AllowSingleQuote(mLeavingReason) & "', " & vbCrLf & " EMP_CATG='" & mCategory & "',  " & vbCrLf & " EMP_FNAME='" & MainClass.AllowSingleQuote(mFName) & "', " & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " ModDate=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

                    SqlStr = SqlStr & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & mPayCode & "'"
                End If

                PubDBCn.Execute(SqlStr)
                RsEmp.MoveNext()
            Loop
        End If

        PubDBCn.CommitTrans()
        RsEmp.Requery()
        UpdateEmployee = True
        Exit Function
UpdateError:
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        UpdateEmployee = False
        PubDBCn.RollbackTrans()
        RsEmp.Requery()
        PubDBCn.Errors.Clear()
        ''Resume
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function


    Private Function FieldsVarification() As Boolean
        On Error GoTo ErrPart1

        lblStatus.Text = "Check All Information. "
        System.Windows.Forms.Application.DoEvents()

        If Trim(txtDateFrom.Text) = "" Then
            MsgInformation("From Date cann't be blank.")
            txtDateFrom.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Not IsDate(txtDateFrom.Text) Then
            MsgInformation("Invalid From Date.")
            txtDateFrom.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtDateTo.Text) = "" Then
            MsgInformation("To Date cann't be blank.")
            txtDateTo.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Not IsDate(txtDateTo.Text) Then
            MsgInformation("Invalid To Date.")
            txtDateTo.Focus()
            FieldsVarification = False
            Exit Function
        End If

        FieldsVarification = True
        Exit Function
ErrPart1:
        MsgBox(Err.Description)
        FieldsVarification = False
    End Function
End Class
