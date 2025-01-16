Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class FrmPFMergeing
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
    Dim ConWorkDay As Double
    Private Const ConWorkHour As Short = 8
    Dim mEmplerPFCont As String

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
    End Sub
    Private Sub cmdOk_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdOK.Click
        Dim mDate As String
        If FieldVarification = False Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        PBar.Visible = True

        Call MergingProcess()

        PBar.Visible = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Function FieldVarification() As Boolean
        FieldVarification = True

        If OptParti.Checked = True Then
            If txtCardNo.Text = "" Then
                MsgBox("CardNo. Not Selected.")
                FieldVarification = False
                txtCardNo.Focus()
                Exit Function
            End If
        End If

        If Not IsDate(VB6.Format(txtFrom.Text, "DD/MM/YYYY")) Then
            MsgBox("Invalid Date")
            FieldVarification = False
            txtFrom.Focus()
            Exit Function
        End If

        If Not IsDate(txtTo.Text) Then
            MsgBox("Invalid Date")
            FieldVarification = False
            txtTo.Focus()
            Exit Function
        End If

    End Function
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click
        On Error GoTo SearchErr
        Dim SqlStr As String = ""
        Dim mTableName As String

        SqlStr = "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If optEmpType(0).Checked = True Then
            mTableName = "PAY_EMPLOYEE_MST"
        Else
            mTableName = "PAY_CONT_EMPLOYEE_MST"
        End If

        If MainClass.SearchGridMaster("", mTableName, "EMP_NAME", "EMP_PF_ACNO", , , SqlStr) = True Then
            txtCardNo.Text = AcName1
            txtName.Text = AcName
            TxtCardNo_Validating(TxtCardNo, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
SearchErr:
        MsgBox(Err.Description)
    End Sub


    Private Sub cmdSearchCont_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchCont.Click
        On Error GoTo SearchErr
        Dim SqlStr As String = ""
        Dim mTable As String


        mTable = "PAY_CONTRACTOR_MST"
        SqlStr = "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster("", mTable, "CON_NAME", , , , SqlStr) = True Then
            txtContractorName.Text = AcName
        End If
        Exit Sub
SearchErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub FrmPFMergeing_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        Me.Text = "Merging Process"
    End Sub

    Private Sub FrmPFMergeing_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        Me.Left = VB6.TwipsToPixelsX(25)
        Me.Top = VB6.TwipsToPixelsY(25)
        MainClass.SetControlsColor(Me)
        Me.Height = VB6.TwipsToPixelsY(4770)
        Me.Width = VB6.TwipsToPixelsX(5475)

        txtFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtTo.Text = VB6.Format(RsCompany.Fields("END_DATE").Value, "DD/MM/YYYY")

        optEmpType(0).Checked = True
        FraContractor.Enabled = False
        optAll.Checked = True
        txtContractorName.Enabled = False
        cmdSearchCont.Enabled = False

        HideUnHide(False)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub
    Private Sub FrmPFMergeing_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
    End Sub

    Private Sub optAll_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptAll.CheckedChanged
        If eventSender.Checked Then
            HideUnHide(False)
        End If
    End Sub

    Private Sub optEmpType_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optEmpType.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optEmpType.GetIndex(eventSender)
            FraContractor.Enabled = IIf(optEmpType(0).Checked = True, False, True)
            If Index = 0 Then
                optContAll.Checked = True
            End If
        End If
    End Sub

    Private Sub txtContractorName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtContractorName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtContractorName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub optContParti_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optContParti.CheckedChanged
        If eventSender.Checked Then
            txtContractorName.Enabled = True
            cmdSearchCont.Enabled = True
        End If
    End Sub
    Private Sub optContAll_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optContAll.CheckedChanged
        If eventSender.Checked Then
            txtContractorName.Enabled = False
            cmdSearchCont.Enabled = False
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
    Private Sub TxtCardNo_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtCardNo.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Private Sub TxtCardNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtCardNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim mTableName As String

        If txtCardNo.Text = "" Then GoTo EventExitSub
        txtCardNo.Text = VB6.Format(txtCardNo.Text, "000000")

        If optEmpType(0).Checked = True Then
            mTableName = "PAY_EMPLOYEE_MST"
        Else
            mTableName = "PAY_CONT_EMPLOYEE_MST"
        End If
        If MainClass.ValidateWithMasterTable((txtCardNo.Text), "EMP_PF_ACNO", "EMP_NAME", mTableName, PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("PF No. Does Not Exist In Master.", MsgBoxStyle.Information)
            Cancel = True
        Else
            txtName.Text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub



    Private Sub HideUnHide(ByRef mCheck As Boolean)
        txtCardNo.Enabled = mCheck
        cmdsearch.Enabled = mCheck
    End Sub

    Private Sub MergingProcess()

        On Error GoTo ErrPart

        Dim RsSalTRN As ADODB.Recordset = Nothing
        Dim RsEmployee As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim pPFNo As String
        Dim pDateLeave As String
        Dim pMonthName As String
        Dim pEDate As String
        Dim pXDate As String
        Dim pEmpName As String
        Dim pEmpFName As String
        Dim pTotWages As Double
        Dim pEPFAmount As Double
        Dim pEmplerPFAmount As Double
        Dim pEPF_367 As Double
        Dim pEPF_833 As Double
        Dim pIsArrear As String
        Dim pFromDate As String
        Dim pToDate As String
        Dim pContCode As String
        Dim pContName As String
        Dim mVPFAmount As Double
        Dim mVPFRate As Double
        Dim mAge As Double
        Dim mDOB As String
        Dim pEmpCode As String
        Dim mEmpContOn As String
        Dim mCompanyPFEst As String
        Dim mTempPFCeiling As Double
        Dim mWDays As Double
        Dim mMonthDays As Double
        Dim xContCode As Integer
        Dim mDOJ As String
        Dim mNPCDays As Double
        Dim mPrevEPF_833 As Double
        Dim pEPF_833Diff As Double
        Dim mPensionConst As Double

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        mCompanyPFEst = IIf(IsDbNull(RsCompany.Fields("PFEST").Value), "", RsCompany.Fields("PFEST").Value)



        pFromDate = "01/" & VB6.Format(txtFrom.Text, "MMM-YYYY")
        pFromDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, CDate(pFromDate)))

        pToDate = VB6.Format(txtTo.Text, "DD-MMM-YYYY")
        pToDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, CDate(pToDate)))

        'PBar.Min = 0

        SqlStr = ""

        ''Check Validation

        SqlStr = " SELECT Count(1) AS CNTREC FROM PAY_CONTSALARY_TRN " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND EDATE BETWEEN TO_DATE('" & VB6.Format(pFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(pToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        '    If optEmpType(0).Value = True Then
        '        SqlStr = SqlStr & vbCrLf & " AND CONT_NAME='EMPLOYEE'"
        '    Else
        '        SqlStr = SqlStr & vbCrLf & " AND CONT_NAME<>'EMPLOYEE'"
        '    End If

        If optEmpType(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND CONT_NAME='EMPLOYEE'"
        Else
            If optContParti.Checked = True Then
                SqlStr = SqlStr & vbCrLf & " AND CONT_NAME='" & MainClass.AllowSingleQuote(txtContractorName.Text) & "'"
            Else
                SqlStr = SqlStr & vbCrLf & " AND CONT_NAME<>'EMPLOYEE'"
            End If
        End If

        If OptParti.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND PFAC_CODE='" & txtCardNo.Text & "'"
        End If



        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSalTRN, ADODB.LockTypeEnum.adLockOptimistic)
        If RsSalTRN.EOF = False Then
            If RsSalTRN.Fields("CNTREC").Value > 0 Then
                SqlStr = CStr(MsgBox("Merging Already Processed For This Period ... " & vbNewLine & vbNewLine & "Want To Reprocess ...", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2))
                If SqlStr = CStr(MsgBoxResult.No) Then
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                    Exit Sub
                End If
            End If
        End If


        SqlStr = " DELETE FROM PAY_CONTSALARY_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        '    If optEmpType(0).Value = True Then
        '        SqlStr = SqlStr & vbCrLf & " AND CONT_NAME='EMPLOYEE'"
        '    Else
        '        SqlStr = SqlStr & vbCrLf & " AND CONT_NAME<>'EMPLOYEE'"
        '    End If

        If optEmpType(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND CONT_NAME='EMPLOYEE'"
        Else
            If optContParti.Checked = True Then
                SqlStr = SqlStr & vbCrLf & " AND CONT_NAME='" & MainClass.AllowSingleQuote(txtContractorName.Text) & "'"
            Else
                SqlStr = SqlStr & vbCrLf & " AND CONT_NAME<>'EMPLOYEE'"
            End If
        End If

        If OptParti.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND PFAC_CODE='" & txtCardNo.Text & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND EDATE BETWEEN TO_DATE('" & VB6.Format(pFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(pToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        PubDBCn.Execute(SqlStr)

        ''---------------------------------------

        SqlStr = " SELECT EMP.EMP_PF_ACNO, EMP_LEAVE_DATE, SAL_DATE, SUM(WDAYS) WDAYS," & vbCrLf & " EMP.EMP_CODE, EMP.EMP_NAME, EMP.EMP_FNAME, " & vbCrLf & " SUM(PFABLEAMT) AS TOTWAGES, SUM(PFAMT) AS PFAMT, " & vbCrLf & " SUM(EPFAMT) AS EPFAMT, SUM(PENSIONFUND) AS PENSIONFUND, DECODE(ISARREAR,'V','N',ISARREAR) AS ISARREAR, SUM(VPFAMT) AS VPFAMT, SUM(VPFRATE) AS VPFRATE," & vbCrLf

        If optEmpType(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " 0 AS CONTRACTOR_CODE " & vbCrLf & " FROM PAY_PFESI_TRN PFESITRN,PAY_EMPLOYEE_MST EMP" & vbCrLf & " WHERE PFESITRN.COMPANY_CODE =EMP.COMPANY_CODE AND  " & vbCrLf & " PFESITRN.EMP_CODE =EMP.EMP_CODE "
        Else
            SqlStr = SqlStr & " CONTRACTOR_CODE " & vbCrLf & " FROM PAY_CONT_PFESI_TRN PFESITRN,PAY_CONT_EMPLOYEE_MST EMP " & vbCrLf & " WHERE PFESITRN.COMPANY_CODE =EMP.COMPANY_CODE AND  " & vbCrLf & " PFESITRN.EMP_CODE =EMP.EMP_CODE "
        End If

        SqlStr = SqlStr & vbCrLf & " AND PFESITRN.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " AND  " & vbCrLf & " SAL_DATE BETWEEN TO_DATE('" & VB6.Format(txtFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND TO_DATE('" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If OptParti.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_PF_ACNO='" & txtCardNo.Text & "'"
        End If

        If optContParti.Checked = True And optEmpType(1).Checked = True Then
            If MainClass.ValidateWithMasterTable(txtContractorName, "CON_NAME", "CON_CODE", "PAY_CONTRACTOR_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                xContCode = MasterNo
                SqlStr = SqlStr & vbCrLf & " AND EMP.CONTRACTOR_CODE=" & xContCode & ""
            End If
        End If

        SqlStr = SqlStr & vbCrLf & " AND (EMP.EMP_PF_ACNO IS NOT NULL OR LENGTH(EMP.EMP_PF_ACNO)<>'')"

        SqlStr = SqlStr & vbCrLf & " GROUP BY EMP.EMP_PF_ACNO, " & vbCrLf & " EMP_LEAVE_DATE, SAL_DATE, EMP.EMP_CODE, EMP.EMP_NAME, EMP.EMP_FNAME,DECODE(ISARREAR,'V','N',ISARREAR),CONTRACTOR_CODE" '',WDAYS

        SqlStr = SqlStr & vbCrLf & " HAVING SUM(PFAMT)>0"

        SqlStr = SqlStr & vbCrLf & " Order by EMP.EMP_PF_ACNO, SAL_DATE, DECODE(ISARREAR,'V','N',ISARREAR)"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmployee, ADODB.LockTypeEnum.adLockOptimistic)


        If RsEmployee.EOF = False Then
            PBar.Visible = True

            'PBar.Min = 0
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
            If OptParti.Checked = True Then
                SqlStr = SqlStr & vbCrLf & " AND EMP_PF_ACNO='" & txtCardNo.Text & "'"
            End If

            'PBar.Max = MainClass.GetMaxRecord("PAY_EMPLOYEE_MST", PubDBCn, SqlStr) * 24
            'PBar.Value = PBar.Min
            Do While Not RsEmployee.EOF

                pPFNo = IIf(IsDbNull(RsEmployee.Fields("EMP_PF_ACNO").Value), "", RsEmployee.Fields("EMP_PF_ACNO").Value)
                pEmpCode = IIf(IsDbNull(RsEmployee.Fields("EMP_CODE").Value), "", RsEmployee.Fields("EMP_CODE").Value)


                If optEmpType(0).Checked = True Then
                    pContName = "EMPLOYEE"
                    If MainClass.ValidateWithMasterTable(pPFNo, "EMP_PF_ACNO", "EMP_DOB", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mDOB = MasterNo
                    Else
                        mDOB = ""
                    End If

                    If MainClass.ValidateWithMasterTable(pPFNo, "EMP_PF_ACNO", "EMP_DOJ", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mDOJ = MasterNo
                    Else
                        mDOJ = ""
                    End If

                Else
                    pContCode = IIf(IsDbNull(RsEmployee.Fields("CONTRACTOR_CODE").Value), "", RsEmployee.Fields("CONTRACTOR_CODE").Value)
                    If MainClass.ValidateWithMasterTable(pContCode, "CON_CODE", "CON_NAME", "PAY_CONTRACTOR_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        pContName = MasterNo
                    End If
                    If MainClass.ValidateWithMasterTable(pPFNo, "EMP_PF_ACNO", "EMP_DOB", "PAY_CONT_EMPLOYEE_MST", PubDBCn, MasterNo, "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mDOB = MasterNo
                    Else
                        mDOB = ""
                    End If

                    If MainClass.ValidateWithMasterTable(pPFNo, "EMP_PF_ACNO", "EMP_DOJ", "PAY_CONT_EMPLOYEE_MST", PubDBCn, MasterNo, "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mDOJ = MasterNo
                    Else
                        mDOJ = ""
                    End If

                End If


                pDateLeave = IIf(IsDbNull(RsEmployee.Fields("EMP_LEAVE_DATE").Value), "", RsEmployee.Fields("EMP_LEAVE_DATE").Value)
                pEDate = IIf(IsDbNull(RsEmployee.Fields("SAL_DATE").Value), "", RsEmployee.Fields("SAL_DATE").Value)
                pEDate = "01/" & VB6.Format(pEDate, "MM/YYYY")

                mMonthDays = MainClass.LastDay(Month(CDate(pEDate)), Year(CDate(pEDate)))
                mWDays = IIf(IsDbNull(RsEmployee.Fields("WDAYS").Value), 0, RsEmployee.Fields("WDAYS").Value)
                pIsArrear = IIf(IsDbNull(RsEmployee.Fields("IsArrear").Value), "", RsEmployee.Fields("IsArrear").Value)

                If pIsArrear = "N" Then

                    If pDateLeave <> "" Then
                        If VB6.Format(CDate(pDateLeave), "YYYYMM") = VB6.Format(CDate(pEDate), "YYYYMM") Then
                            mNPCDays = mMonthDays - IIf(IsDbNull(RsEmployee.Fields("WDAYS").Value), 0, RsEmployee.Fields("WDAYS").Value)
                            mNPCDays = mNPCDays - (mMonthDays - VB.Day(CDate(pDateLeave)))
                        Else
                            mNPCDays = mMonthDays - IIf(IsDbNull(RsEmployee.Fields("WDAYS").Value), 0, RsEmployee.Fields("WDAYS").Value)
                        End If
                    Else
                        mNPCDays = mMonthDays - IIf(IsDbNull(RsEmployee.Fields("WDAYS").Value), 0, RsEmployee.Fields("WDAYS").Value)
                    End If

                    If VB6.Format(CDate(mDOJ), "YYYYMM") = VB6.Format(CDate(pEDate), "YYYYMM") Then
                        mNPCDays = mNPCDays - (VB.Day(CDate(mDOJ)) - 1)
                    End If

                    '                If pDateLeave <> "" Then
                    '                    If Format(CDate(pDateLeave), "YYYYMM") = Format(CDate(pEDate), "YYYYMM") Then
                    '                        mNPCDays = mNPCDays - (mMonthDays - Day(pDateLeave))
                    '                    End If
                    '                End If
                Else
                    mNPCDays = 0
                End If
                mEmpContOn = GetEmployeePFContOn(pEmpCode, pEDate)

                If Trim(mDOB) = "" Then
                    mAge = 0
                Else
                    mAge = DateDiff(Microsoft.VisualBasic.DateInterval.Month, CDate(mDOB), CDate(pEDate)) / 12 '' DateDiff("yyyy", mDOB, pEDate)       ''pEDate - mDOB
                End If

                pEDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, CDate(pEDate)))


                If VB6.Format(pEDate, "MM") = "04" Then
                    pMonthName = "March (Paid In April) " ''& IIf(pIsArrear = "Y", " Arrear", IIf(pIsArrear = "E", "ENCASH", ""))
                ElseIf VB6.Format(pEDate, "MM") = "03" Then
                    pMonthName = "February (Paid In March) " ''& IIf(pIsArrear = "Y", " Arrear", IIf(pIsArrear = "E", "ENCASH", ""))
                Else
                    pXDate = pEDate ''DateAdd("m", -1, pEDate)
                    pMonthName = VB6.Format(pXDate, "MMMM") ''& IIf(pIsArrear = "Y", " Arrear", IIf(pIsArrear = "E", "ENCASH", "")) '& " - " & vb6.Format(xDate, "YYYY")
                End If

                If pIsArrear = "Y" Then
                    pMonthName = pMonthName & " Arrear"
                ElseIf pIsArrear = "E" Then
                    pMonthName = pMonthName & " Encash"
                ElseIf pIsArrear = "P" Then
                    pMonthName = pMonthName & " Encash Arrear"
                ElseIf pIsArrear = "C" Then
                    pMonthName = pMonthName & " CPL"
                ElseIf pIsArrear = "O" Then
                    pMonthName = pMonthName & " OT"
                End If


                pEmpName = IIf(IsDbNull(RsEmployee.Fields("EMP_NAME").Value), "", RsEmployee.Fields("EMP_NAME").Value)
                pEmpFName = IIf(IsDbNull(RsEmployee.Fields("EMP_FNAME").Value), "", RsEmployee.Fields("EMP_FNAME").Value)
                pTotWages = IIf(IsDbNull(RsEmployee.Fields("TOTWAGES").Value), 0, RsEmployee.Fields("TOTWAGES").Value)
                '            pEPFAmount = IIf(IsNull(RsEmployee!PFAMT), 0, RsEmployee!PFAMT)
                '            pEPF_367 = IIf(IsNull(RsEmployee!EPFAMT), 0, RsEmployee!EPFAMT)
                '            pEPF_833 = IIf(IsNull(RsEmployee!PENSIONFUND), 0, RsEmployee!PENSIONFUND)

                mVPFAmount = IIf(IsDbNull(RsEmployee.Fields("VPFAMT").Value), 0, RsEmployee.Fields("VPFAMT").Value)
                mVPFRate = IIf(IsDbNull(RsEmployee.Fields("VPFRATE").Value), 0, RsEmployee.Fields("VPFRATE").Value)


                Call CheckPFRates(CDate(pEDate))

                If pIsArrear = "N" Then
                    mTempPFCeiling = System.Math.Round(mPFCeiling * mWDays / mMonthDays, 0)
                Else
                    mTempPFCeiling = mPFCeiling
                End If

                If pIsArrear = "Y" Then
                    If mEmplerPFCont = "B" Then
                        pEPFAmount = System.Math.Round(pTotWages * 0.12)
                        pEPF_833 = System.Math.Round(pTotWages * 0.0833)
                        pEPF_367 = System.Math.Round(pTotWages * 0.0367)
                    Else
                        If mEmpContOn = "C" Then
                            pEPFAmount = 0
                        Else
                            pEPFAmount = System.Math.Round(pTotWages * 0.12)
                        End If
                        pEmplerPFAmount = 0
                        pEPF_833 = 0
                        pEPF_367 = 0
                    End If
                Else
                    If mPFCeiling > pTotWages Then
                        pEPFAmount = System.Math.Round(pTotWages * 0.12)
                        If mEmplerPFCont = "B" Then
                            pEPF_833 = System.Math.Round(pTotWages * 0.0833)
                            pEPF_367 = pEPFAmount - pEPF_833 ''Round(pTotWages * 0.0367)
                        Else
                            pEmplerPFAmount = System.Math.Round(mTempPFCeiling * 0.12)
                            pEPF_833 = System.Math.Round(mTempPFCeiling * 0.0833)
                            pEPF_367 = pEmplerPFAmount - pEPF_833
                        End If
                    Else
                        If mEmpContOn = "C" Then
                            pEPFAmount = System.Math.Round(mTempPFCeiling * 0.12)
                        Else
                            pEPFAmount = System.Math.Round(pTotWages * 0.12)
                        End If
                        If mEmplerPFCont = "B" Then
                            pEmplerPFAmount = System.Math.Round(pTotWages * 0.12)
                            pEPF_833 = System.Math.Round(mTempPFCeiling * 0.0833)
                            pEPF_367 = pEmplerPFAmount - pEPF_833 ''Round(xTotWages * 0.0367)
                        Else
                            pEmplerPFAmount = System.Math.Round(mTempPFCeiling * 0.12)
                            pEPF_833 = System.Math.Round(mTempPFCeiling * 0.0833)
                            pEPF_367 = pEmplerPFAmount - pEPF_833
                        End If
                    End If
                End If

                If mAge > 58 Then
                    pEPF_367 = pEPF_833 + pEPF_367
                    pEPF_833 = 0
                End If

                pTotWages = System.Math.Round(pTotWages, 0)
                pEPFAmount = System.Math.Round(pEPFAmount, 0)
                pEPF_367 = System.Math.Round(pEPF_367, 0)
                pEPF_833 = System.Math.Round(pEPF_833, 0)

                mPrevEPF_833 = GetEPFAmount833(pPFNo, pEDate)

                mPensionConst = System.Math.Round(mPFCeiling * 8.33 * 0.01, 0)

                If mPrevEPF_833 <> 0 Then
                    If mPrevEPF_833 >= mPensionConst Then
                        pEPF_367 = pEPF_367 + pEPF_833
                        pEPF_833 = 0
                    Else
                        pEPF_833Diff = mPensionConst - mPrevEPF_833

                        If pEPF_833Diff < pEPF_833 Then
                            pEPF_367 = pEPF_367 + (pEPF_833 - pEPF_833Diff)
                            pEPF_833 = pEPF_833Diff
                        End If
                    End If
                End If

                If Trim(UCase(pPFNo)) <> Trim(UCase(mCompanyPFEst)) Then
                    '            If Trim(UCase(pPFNo)) = "HR/5415/3397" Then GoTo NextRow
                    If UpdateMergingPF(pPFNo, pDateLeave, pMonthName, pEDate, pEmpName, pEmpFName, pTotWages, pEPFAmount, pEPF_367, pEPF_833, pIsArrear, pContName, mVPFAmount, mVPFRate, mNPCDays) = False Then GoTo ErrPart
                End If
NextRow:
                RsEmployee.MoveNext()
                PBar.Value = PBar.Value + 1
            Loop


            MsgBox("Merging Process Complete")
        Else
            MsgBox("No Record Found For Processing.")
        End If

        PubDBCn.CommitTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub

ErrPart:
        ''Resume
        MsgInformation("Merging Process Not Complete, Try Again.")
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Function GetEPFAmount833(ByRef pPFNo As Object, ByRef pEDate As Object) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = " SELECT SUM(EPF_833) AS EPF_833" & vbCrLf & " FROM PAY_CONTSALARY_TRN " & vbCrLf & " WHERE COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PFAC_CODE='" & pPFNo & "'" & vbCrLf & " AND EDATE=TO_DATE('" & VB6.Format(pEDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)


        If RsTemp.EOF = False Then
            GetEPFAmount833 = IIf(IsDbNull(RsTemp.Fields("EPF_833").Value), 0, RsTemp.Fields("EPF_833").Value)
        Else
            GetEPFAmount833 = 0
        End If

        Exit Function

ErrPart:
        ''Resume
        GetEPFAmount833 = 0
    End Function
    Private Sub CheckPFRates(ByRef mDate As Date)

        Dim RsCeiling As ADODB.Recordset
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mSqlStr As String
        mSqlStr = ""
        mSqlStr = " SELECT MAX(WEF) FROM PAY_PFESICEILING_MST" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE= " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CODE=" & ConPF & "" & vbCrLf & " AND WEF<=TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

        SqlStr = " SELECT * FROM PAY_PFESICEILING_MST WHERE " & vbCrLf & " COMPANY_CODE= " & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " CODE=" & ConPF & " AND WEF=(" & mSqlStr & ") "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCeiling, ADODB.LockTypeEnum.adLockOptimistic)
        If RsCeiling.EOF = False Then
            mPFCeiling = IIf(IsDbNull(RsCeiling.Fields("ceiling").Value), 0, RsCeiling.Fields("ceiling").Value)
            mPFRate = IIf(IsDbNull(RsCeiling.Fields("Rate").Value), 0, RsCeiling.Fields("Rate").Value)
            mPFEPFRate = IIf(IsDbNull(RsCeiling.Fields("EPF").Value), 0, RsCeiling.Fields("EPF").Value)
            mPFPensionRate = IIf(IsDbNull(RsCeiling.Fields("PFUND").Value), 0, RsCeiling.Fields("PFUND").Value)
            mEmplerPFCont = IIf(IsDbNull(RsCeiling.Fields("EMPER_CONT").Value), "B", RsCeiling.Fields("EMPER_CONT").Value)
        Else
            mPFCeiling = 6500
            mPFRate = 12
            mPFEPFRate = 3.67
            mPFPensionRate = 8.33
            mEmplerPFCont = "B"
        End If
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub

    Private Function UpdateMergingPF(ByRef mPFNo As String, ByRef mDateLeave As String, ByRef mMonthName As String, ByRef mEDate As String, ByRef mEmpName As String, ByRef mEmpFName As String, ByRef mTotWages As Double, ByRef mEPFAmount As Double, ByRef mEPF_367 As Double, ByRef mEPF_833 As Double, ByRef mIsArrear As String, ByRef mContName As String, ByRef pVPFAmount As Double, ByRef pVPFRate As Double, ByRef pWDays As Double) As Boolean

        On Error GoTo UpDateSalTrnErr
        Dim SqlStr As String = ""

        '        SqlStr = " DELETE FROM PAY_CONTSALARY_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''                & " AND PFAC_CODE='" & MainClass.AllowSingleQuote(mPFNo) & "'" & vbCrLf _
        ''                & " AND TO_CHAR(EDATE,'MON-YYYY') ='" & UCase(Format(mEDate, "MMM-YYYY")) & "' AND ISARREAR='" & mIsArrear & "'"
        '
        '        PubDBCn.Execute SqlStr


        If Trim(mPFNo) <> "" And Trim(mEDate) <> "" Then
            SqlStr = "INSERT INTO PAY_CONTSALARY_TRN( " & vbCrLf & " COMPANY_CODE, CONT_NAME,LEAVEDATE," & vbCrLf & " MONTH_DESC, EDATE, " & vbCrLf & " PFAC_CODE, EMP_NAME," & vbCrLf & " EMP_FNAME, TOT_WAGES," & vbCrLf & " EPF_AMT, EPF_367,  EPF_833, ISARREAR,VPFAMT,VPFRATE,WDAYS) "

            SqlStr = SqlStr & vbCrLf & " VALUES(" & RsCompany.Fields("Company_Code").Value & ", " & vbCrLf & " '" & mContName & "',TO_DATE('" & VB6.Format(mDateLeave, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & MainClass.AllowSingleQuote(mMonthName) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(mEDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & MainClass.AllowSingleQuote(mPFNo) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mEmpName) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mEmpFName) & "', " & vbCrLf & " " & Val(CStr(mTotWages)) & ", " & vbCrLf & " " & Val(CStr(mEPFAmount)) & ", " & vbCrLf & " " & Val(CStr(mEPF_367)) & ", " & vbCrLf & " " & Val(CStr(mEPF_833)) & ",'" & mIsArrear & "'," & pVPFAmount & ", " & pVPFRate & "," & pWDays & ")"

            PubDBCn.Execute(SqlStr)
        End If

NextRec:
        UpdateMergingPF = True

        Exit Function
UpDateSalTrnErr:
        'Resume Next
        MsgBox(Err.Description)
        UpdateMergingPF = False
    End Function
    Private Sub txtContractorName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtContractorName.DoubleClick
        cmdSearchCont_Click(cmdSearchCont, New System.EventArgs())
    End Sub
    Private Sub txtContractorName_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtContractorName.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchCont_Click(cmdSearchCont, New System.EventArgs())
    End Sub
    Private Sub txtContractorName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtContractorName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        If txtContractorName.Text = "" Then GoTo EventExitSub
        '    txtContractorName.Text = Format(txtContractorName.Text, "000000")
        If MainClass.ValidateWithMasterTable((txtContractorName.Text), "CONT_NAME", "CONT_NAME", "PAY_CONTRACTOR_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Contractor Name Does Not Exist In Master.", MsgBoxStyle.Information)
            Cancel = True
        Else
            txtContractorName.Text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If IsDate(VB6.Format(CDate(txtFrom.Text), "dd/mm/yyyy")) = False Then
            MsgBox("Invalid Date")
            Cancel = True
        End If
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If IsDate(VB6.Format(CDate(txtTo.Text), "dd/mm/yyyy")) = False Then
            MsgBox("Invalid Date")
            Cancel = True
        End If
        eventArgs.Cancel = Cancel
    End Sub
End Class
