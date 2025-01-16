Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmITForm12BA
    Inherits System.Windows.Forms.Form
    Dim RsITMain As ADODB.Recordset
    Dim RsITDetail As ADODB.Recordset

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection

    Dim Shw As Boolean
    Dim xCode As String
    Dim SqlStr As String = ""
    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12

    Private Const ColSNo As Short = 0
    Private Const ColDesc As Short = 1
    Private Const ColAmt1 As Short = 2
    Private Const ColAmt2 As Short = 3
    Private Const ColAmt3 As Short = 4

    Private Sub ViewGrid()

        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        ADataMain.Refresh
            FormatSprdView()
            SprdView.Refresh()

            SprdView.Focus()
            SprdView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            SprdView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsITMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub Clear1()

        MainClass.ClearGrid(sprdIT)
        FillSprdGrid()

        txtEmpCode.Text = ""
        txtName.Text = ""
        txtDesignation.Text = ""
        txtTaxDeduct.Text = ""
        txtTaxPaid.Text = ""
        txtTotalTaxPaid.Text = ""
        txtTaxPayment.Text = ""

        MainClass.ButtonStatus(Me, XRIGHT, RsITMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsITMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            Call Show1()
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
            txtEmpCode.Focus()
        Else
            ADDMode = False
            MODIFYMode = False
            If RsITMain.EOF = False Then RsITMain.MoveFirst()
            Show1()
        End If
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If txtEmpCode.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub
        If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then
            If Delete1 = False Then GoTo DelErrPart
        End If

        Exit Sub
DelErrPart:
        MsgBox("Record Not Deleted")
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click
        SqlStr = ""

        If MainClass.SearchGridMaster((txtEmpCode.Text), "PAY_EMPLOYEE_MST", "EMP_CODE", "EMP_NAME", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtEmpCode.Text = AcName
            txtName.Text = AcName1
            txtEmpCode_Validating(txtEmpCode, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub

    End Sub
    Private Sub frmITForm12BA_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub sprdIT_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles sprdIT.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub sprdIT_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles sprdIT.LeaveCell
        On Error GoTo ErrPart


        If eventArgs.NewRow = -1 Then Exit Sub
        sprdIT.Row = eventArgs.row
        CalcGrid()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        'Resume
    End Sub
    Private Sub frmITForm12BA_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        SqlStr = ""
        If FormActive = True Then Exit Sub
        SqlStr = "Select * From PAY_ITFORM12BA_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsITMain, ADODB.LockTypeEnum.adLockOptimistic)

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
        '' Resume
    End Sub
    Private Sub frmITForm12BA_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)
        Me.Left = 0
        Me.Top = 0

        FormatSprd(-1)

        'CellFormat
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub
    Private Sub frmITForm12BA_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        '    'PvtDBCn.Cancel
        '    'PvtDBCn.Close
        RsITMain = Nothing
        '    'Set PvtDBCn = Nothing
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim cntRow As Integer
        Dim mDesigationCode As String
        Dim RsDesig As ADODB.Recordset
        Dim RsTemp As ADODB.Recordset = Nothing

        If RsITMain.EOF = False Then
            With RsITMain
                txtEmpCode.Text = IIf(IsDbNull(.Fields("EMP_CODE").Value), "", .Fields("EMP_CODE").Value)
                xCode = IIf(IsDbNull(.Fields("EMP_CODE").Value), "", .Fields("EMP_CODE").Value)

                If MainClass.ValidateWithMasterTable((txtEmpCode.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtName.Text = MasterNo
                End If


                '            If MainClass.ValidateWithMasterTable(txtEmpCode.Text, "EMP_CODE", "EMP_DESG_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '                mDesigationCode = MasterNo
                '            End If

                '            If MainClass.ValidateWithMasterTable(mDesigationCode, "DESG_CODE", "DESG_DESC", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '                txtDesignation.Text = MasterNo
                '            Else
                '                txtDesignation.Text = ""
                '            End If

                SqlStr = " SELECT GETEMPDESG(" & RsCompany.Fields("COMPANY_CODE").Value & ",'" & Trim(txtEmpCode.Text) & "',TO_DATE('" & VB6.Format(RsCompany.Fields("END_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS EMP_DESG FROM DUAL"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = False Then
                    txtDesignation.Text = IIf(IsDbNull(RsTemp.Fields("EMP_DESG").Value), "", RsTemp.Fields("EMP_DESG").Value)
                End If

                '            '18-05-2005  'Kapil Jain
                '
                '            SqlStr = " SELECT * from PAY_SalaryDef_MST " & vbCrLf _
                ''                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                ''                    & " AND EMP_CODE='" & txtEmpCode.Text & "'" & vbCrLf _
                ''                    & " AND SALARY_EFF_DATE=(SELECT MAX(SALARY_EFF_DATE) From PAY_SalaryDef_MST " & vbCrLf _
                ''                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                ''                    & " AND EMP_CODE='" & txtEmpCode.Text & "'" & vbCrLf _
                ''                    & " AND SALARY_EFF_DATE<= TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "')) "
                '
                '            MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsDesig, adLockOptimistic
                '
                '            If RsDesig!EMP_DESG_CODE <> "" Then
                '                If MainClass.ValidateWithMasterTable(RsDesig!EMP_DESG_CODE, "DESG_CODE", "DESG_DESC", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '                    txtDesignation.Text = MasterNo
                '                End If
                '            End If

                txtTaxDeduct.Text = CStr(CalcPaidIT((txtEmpCode.Text))) ''IIf(IsNull(!TAX_DEDUCT), "", !TAX_DEDUCT)
                txtTaxPaid.Text = IIf(IsDbNull(.Fields("TAX_PAID").Value), "", .Fields("TAX_PAID").Value)

                txtTaxDeduct.Text = VB6.Format(Val(txtTaxDeduct.Text), "0.00")
                txtTaxPaid.Text = VB6.Format(Val(txtTaxPaid.Text), "0.00")
                txtTotalTaxPaid.Text = VB6.Format(Val(txtTaxDeduct.Text) + Val(txtTaxPaid.Text), "0.00")


                '            txtTotalTaxPaid.Text = IIf(IsNull(!TOTAL_TAX_PAID), "", !TOTAL_TAX_PAID)
                txtTaxPayment.Text = IIf(IsDbNull(.Fields("TAX_PAYMENT_DATE").Value), "", .Fields("TAX_PAYMENT_DATE").Value)

                Call ShowDetail1(.Fields("EMP_CODE").Value)
            End With
        End If
        Shw = True


        Shw = False
        ADDMode = False
        MODIFYMode = False

        MainClass.ButtonStatus(Me, XRIGHT, RsITMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
        'Resume
    End Sub

    Private Function CalcPaidIT(ByRef mEmpCode As String) As Double

        On Error GoTo ShowErrPart
        Dim RsIT As ADODB.Recordset
        Dim mSalDate As String
        Dim mFromEmpCompany As Integer
        Dim mFromEmpCode As String
        Dim mFromEmpLeaveDate As String
        Dim RsTemp As ADODB.Recordset = Nothing

        Dim mToEmpCompany As Integer
        Dim mToEmpCode As String

        CalcPaidIT = 0
        mSalDate = VB6.Format(RsCompany.Fields("END_DATE").Value, "DD/MM/YYYY") ''MainClass.LastDay(Month(txtDate.Text), Year(txtDate.Text)) & "/" & vb6.Format(txtDate.Text, "MM/YYYY")

        '    SqlStr = " SELECT SUM(PayableAmount) AS AMOUNT1 " & vbCrLf _
        ''            & " FROM PAY_SAL_TRN SALTRN, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf _
        ''            & " WHERE " & vbCrLf _
        ''            & " SALTRN.Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''            & " AND SALTRN.Company_Code = ADD_DEDUCT.Company_Code(+)" & vbCrLf _
        ''            & " AND SALTRN.SALHEADCODE = ADD_DEDUCT.CODE(+) " & vbCrLf _
        ''            & " AND EMP_CODE = '" & mEmpCode & "'" & vbCrLf _
        ''            & " AND TYPE=" & ConIncomeTax & "" & vbCrLf _
        ''            & " AND SAL_DATE>='" & VB6.Format(RsCompany!START_DATE, "DD-MMM-YYYY") & "'" & vbCrLf _
        ''            & " AND SAL_DATE<='" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "'"
        '
        '
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsIT, adLockOptimistic
        '
        '    If RsIT.EOF = False Then
        '           CalcPaidIT = IIf(IsNull(RsIT!AMOUNT1), 0, RsIT!AMOUNT1)
        '    End If

        ''Leave EnCash.....

        '    SqlStr = " SELECT SUM(AMOUNT) AS AMOUNT1 " & vbCrLf _
        ''            & " FROM PAY_MONTHLY_TRN SALTRN, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf _
        ''            & " WHERE " & vbCrLf _
        ''            & " SALTRN.Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''            & " AND SALTRN.Company_Code = ADD_DEDUCT.Company_Code" & vbCrLf _
        ''            & " AND SALTRN.ADD_DEDUCTCODE = ADD_DEDUCT.CODE " & vbCrLf _
        ''            & " AND SALTRN.SAL_FLAG='E' AND EMP_CODE = '" & mEmpCode & "'" & vbCrLf _
        ''            & " AND TYPE=" & ConIncomeTax & "" & vbCrLf _
        ''            & " AND SAL_MONTH>='" & VB6.Format(RsCompany!START_DATE, "DD-MMM-YYYY") & "'" & vbCrLf _
        ''            & " AND SAL_MONTH<='" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "'"
        '
        '
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsIT, adLockOptimistic
        '
        '    If RsIT.EOF = False Then
        '           CalcPaidIT = CalcPaidIT + IIf(IsNull(RsIT!AMOUNT1), 0, RsIT!AMOUNT1)
        '    End If

        ''Previous Employer Deduction ..... ''all

        SqlStr = " SELECT SUM(ID.AMOUNT) AS TDS_AMOUNT " & vbCrLf & " FROM PAY_ITCHALLAN_HDR IH, PAY_ITCHALLAN_DET ID " & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=ID.COMPANY_CODE " & vbCrLf & " AND IH.AUTO_KEY_REFNO=ID.AUTO_KEY_REFNO " & vbCrLf & " AND ID.EMP_CODE='" & mEmpCode & "'" '' AND IH.BOOKTYPE='O'

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsIT, ADODB.LockTypeEnum.adLockOptimistic)

        If RsIT.EOF = False Then
            CalcPaidIT = CalcPaidIT + IIf(IsDbNull(RsIT.Fields("TDS_AMOUNT").Value), 0, RsIT.Fields("TDS_AMOUNT").Value)
        End If

        'Transfer Emp Data ...........

        '    SqlStr = " SELECT * " & vbCrLf _
        ''            & " FROM PAY_EMP_TRF_MST" & vbCrLf _
        ''            & " WHERE " & vbCrLf _
        ''            & " TO_COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''            & " AND TO_EMP_CODE = '" & mEmpCode & "'"

        mToEmpCompany = RsCompany.Fields("COMPANY_CODE").Value
        mToEmpCode = mEmpCode

SearchRow:
        SqlStr = GetEmpTransferSQL(mToEmpCode, mToEmpCompany)
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then


            mFromEmpCompany = IIf(IsDbNull(RsTemp.Fields("FROM_COMPANY_CODE").Value), "", RsTemp.Fields("FROM_COMPANY_CODE").Value)
            mFromEmpCode = IIf(IsDbNull(RsTemp.Fields("FROM_EMP_CODE").Value), "", RsTemp.Fields("FROM_EMP_CODE").Value)

            If MainClass.ValidateWithMasterTable(mFromEmpCode, "EMP_CODE", "EMP_LEAVE_DATE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & mFromEmpCompany & "") = True Then
                mFromEmpLeaveDate = VB6.Format(Trim(MasterNo), "DD/MM/YYYY")
            End If

            SqlStr = " SELECT SUM(PayableAmount) AS AMOUNT1 " & vbCrLf & " FROM PAY_SAL_TRN SALTRN, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf & " WHERE " & vbCrLf & " SALTRN.Company_Code = " & mFromEmpCompany & "" & vbCrLf & " AND SALTRN.Company_Code = ADD_DEDUCT.Company_Code(+)" & vbCrLf & " AND SALTRN.SALHEADCODE = ADD_DEDUCT.CODE(+) " & vbCrLf & " AND EMP_CODE = '" & mFromEmpCode & "'" & vbCrLf & " AND TYPE=" & ConIncomeTax & "" & vbCrLf & " AND SAL_DATE>=TO_DATE('" & VB6.Format(RsCompany.Fields("START_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND SAL_DATE<=TO_DATE('" & VB6.Format(mFromEmpLeaveDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsIT, ADODB.LockTypeEnum.adLockOptimistic)


            If RsIT.EOF = False Then
                CalcPaidIT = CalcPaidIT + IIf(IsDbNull(RsIT.Fields("AMOUNT1").Value), 0, RsIT.Fields("AMOUNT1").Value)
            End If

            mToEmpCompany = mFromEmpCompany
            mToEmpCode = mFromEmpCode
            GoTo SearchRow
        End If
        Exit Function
ShowErrPart:
        CalcPaidIT = 0
        MsgBox(Err.Description)
        'Resume
    End Function
    Private Sub ShowDetail1(ByRef xEmpCode As String)

        On Error GoTo ShowErrPart
        Dim cntRow As Integer

        SqlStr = " SELECT * " & vbCrLf & " FROM PAY_ITFORM12BA_DET WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND EMP_CODE='" & xEmpCode & "' ORDER BY SUBROW"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsITDetail, ADODB.LockTypeEnum.adLockReadOnly)

        If RsITDetail.EOF = False Then
            With RsITDetail
                cntRow = 1
                Do While Not RsITDetail.EOF
                    sprdIT.Row = cntRow
                    sprdIT.Col = ColDesc
                    sprdIT.Text = IIf(IsDbNull(.Fields("Description").Value), "", .Fields("Description").Value)
                    sprdIT.Col = ColAmt1
                    sprdIT.Text = CStr(IIf(.Fields("AMOUNT1").Value = 0, "", .Fields("AMOUNT1").Value))
                    sprdIT.Col = ColAmt2
                    sprdIT.Text = CStr(IIf(.Fields("AMOUNT2").Value = 0, "", .Fields("AMOUNT2").Value))
                    sprdIT.Col = ColAmt3
                    sprdIT.Text = CStr(IIf(.Fields("AMOUNT3").Value = 0, "", .Fields("AMOUNT3").Value))
                    cntRow = cntRow + 1
                    RsITDetail.MoveNext()
                Loop
            End With
        End If
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
        'Resume
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        If Update1 = True Then
            txtEmpCode_Validating(txtEmpCode, New System.ComponentModel.CancelEventArgs(False))
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
        Dim cntRow As Integer
        Dim mTAX_PAYABLE As Double
        Dim mEmpDesg As String
        Dim RsTemp As ADODB.Recordset = Nothing

        If MainClass.ValidateWithMasterTable((txtEmpCode.Text), "EMP_CODE", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCode = MasterNo
        Else
            MsgInformation("Employee Name is not exsits in Master.")
            Update1 = False
            Exit Function
        End If

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = " SELECT GETEMPDESG(" & RsCompany.Fields("COMPANY_CODE").Value & ",'" & Trim(txtEmpCode.Text) & "',TO_DATE('" & VB6.Format(RsCompany.Fields("END_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS EMP_DESG FROM DUAL"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mEmpDesg = IIf(IsDbNull(RsTemp.Fields("EMP_DESG").Value), "", RsTemp.Fields("EMP_DESG").Value)
        End If

        SqlStr = " DELETE FROM  PAY_ITFORM12BA_DET WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf & " Emp_Code='" & mCode & "' "
        PubDBCn.Execute(SqlStr)

        If ADDMode = True Then
            SqlStr = "INSERT INTO  PAY_ITFORM12BA_HDR ( " & vbCrLf & " COMPANY_CODE, FYEAR, EMP_CODE, " & vbCrLf & " TAX_DEDUCT, TAX_PAID, " & vbCrLf & " TOTAL_TAX_PAID, TAX_PAYMENT_DATE, DESG_DESC, " & vbCrLf & " ADDUSER, ADDDATE ) " & vbCrLf & " VALUES ( "

            SqlStr = SqlStr & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & ", '" & Trim(txtEmpCode.Text) & "',  " & vbCrLf & " " & Val(txtTaxDeduct.Text) & ", " & Val(txtTaxPaid.Text) & "," & vbCrLf & " " & Val(txtTotalTaxPaid.Text) & ", TO_DATE('" & VB6.Format(txtTaxPayment.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(mEmpDesg) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        Else

            SqlStr = "UPDATE  PAY_ITFORM12BA_HDR SET " & vbCrLf & " TAX_DEDUCT=" & Val(txtTaxDeduct.Text) & ", " & vbCrLf & " TAX_PAID=" & Val(txtTaxPaid.Text) & ", " & vbCrLf & " TOTAL_TAX_PAID=" & Val(txtTotalTaxPaid.Text) & ", " & vbCrLf & " TAX_PAYMENT_DATE=TO_DATE('" & VB6.Format(txtTaxPayment.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),  " & vbCrLf & " DESG_DESC='" & MainClass.AllowSingleQuote(mEmpDesg) & "',  " & vbCrLf & " ADDUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', ADDDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE= " & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR= " & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND EMP_CODE='" & Trim(txtEmpCode.Text) & "'"

        End If

        PubDBCn.Execute(SqlStr)

        If UpdateDetail = False Then GoTo UpdateError
        PubDBCn.CommitTrans()
        RsITMain.Requery()
        Update1 = True
        Exit Function
UpdateError:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsITMain.Requery()
        If Err.Number = -2147467259 Then
            MsgBox("Can't Modify Transaction Exists Against this Code")
            Exit Function
        End If
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        PubDBCn.Errors.Clear()
        'Resume
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Private Function UpdateDetail() As Boolean

        On Error GoTo UpdateError
        Dim mCode As String
        Dim cntRow As Integer
        Dim mDesc As Object
        Dim mAmount1 As Double
        Dim mAmount2 As Double
        Dim mAmount3 As Double

        With sprdIT
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColDesc
                mDesc = MainClass.AllowSingleQuote(.Text)

                .Col = ColAmt1
                mAmount1 = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColAmt2
                mAmount2 = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColAmt3
                mAmount3 = IIf(IsNumeric(.Text), .Text, 0)

                SqlStr = " INSERT INTO PAY_ITFORM12BA_DET " & vbCrLf & " ( COMPANY_CODE , FYEAR, EMP_CODE, " & vbCrLf & " SUBROW, Description, AMOUNT1, " & vbCrLf & " AMOUNT2 , AMOUNT3)  VALUES " & vbCrLf & " (" & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf & " '" & Trim(txtEmpCode.Text) & "', " & vbCrLf & " " & cntRow & ", '" & mDesc & "'," & vbCrLf & " " & mAmount1 & "," & mAmount2 & "," & mAmount3 & ")"

                PubDBCn.Execute(SqlStr)
            Next
        End With

        UpdateDetail = True
        Exit Function
UpdateError:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateDetail = False
    End Function


    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SqlStr = ""
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
        txtEmpCode.Text = SprdView.Text

        txtEmpCode_Validating(txtEmpCode, New System.ComponentModel.CancelEventArgs(True))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub
    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub
    Private Sub txtDesignation_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDesignation.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtEmpCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtEmpCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpCode.DoubleClick
        cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub

    Private Sub txtEmpCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEmpCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.TitleCase(KeyAscii, txtEmpCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEmpCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtEmpCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            cmdsearch_Click(cmdsearch, New System.EventArgs())
        End If
    End Sub
    Private Function FieldsVarification() As Boolean
        On Error GoTo ERR1
        Dim xAmount As Decimal

        FieldsVarification = True
        If Trim(txtEmpCode.Text) = "" Then
            MsgInformation("Employee Code is empty. Cannot Save")
            txtEmpCode.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
        End If
        If MODIFYMode = True And RsITMain.EOF = True Then Exit Function
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        FieldsVarification = False
        'Resume
    End Function
    Private Sub settextlength()

        On Error GoTo ERR1

        txtEmpCode.Maxlength = MainClass.SetMaxLength("EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn)
        txtTaxDeduct.Maxlength = RsITMain.Fields("TAX_DEDUCT").Precision
        txtTaxPaid.Maxlength = RsITMain.Fields("TAX_PAID").Precision
        txtTotalTaxPaid.Maxlength = RsITMain.Fields("TOTAL_TAX_PAID").Precision
        txtTaxPayment.Maxlength = 10

        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)


        SqlStr = " SELECT IH.EMP_CODE, EMP_NAME, TOTAL_TAX_PAID, TAX_PAYMENT_DATE " & vbCrLf _
            & " FROM PAY_EMPLOYEE_MST EMP,PAY_ITFORM12BA_HDR IH " & vbCrLf _
            & " WHERE " & vbCrLf & " EMP.COMPANY_CODE=IH.COMPANY_CODE " & vbCrLf _
            & " AND EMP.EMP_CODE=IH.EMP_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()

    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 12)
            .set_ColWidth(0, 5)
            .set_ColWidth(1, 8)
            .set_ColWidth(2, 16)
            .set_ColWidth(3, 12)
            .set_ColWidth(4, 12)

            .ColsFrozen = 1
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Function Delete1() As Boolean
        On Error GoTo DeleteErr

        SqlStr = ""
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = " Delete from PAY_ITFORM12BA_DET WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND Emp_Code='" & MainClass.AllowSingleQuote((txtEmpCode.Text)) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = " Delete from PAY_ITFORM12BA_HDR WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND Emp_Code='" & MainClass.AllowSingleQuote((txtEmpCode.Text)) & "'"
        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()
        Clear1()
        RsITMain.Requery()
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans()
        RsITMain.Requery()
        If Err.Number = -2147467259 Then
            MsgBox("Can't Delete Transaction Exists Against This Employee.")
            Exit Function
        End If
        MsgBox(Err.Description)
    End Function
    Private Sub FormatSprd(ByRef mRow As Integer)

        On Error GoTo ERR1
        With sprdIT
            .Row = mRow
            .MaxCols = ColAmt3
            .MaxRows = 19
            .set_RowHeight(mRow, ConRowHeight * 1.5)

            .Col = ColDesc
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeMaxEditLen = 255
            .set_ColWidth(ColDesc, 40)


            .Col = ColAmt1
            .CellType = SS_CELL_TYPE_FLOAT
            .set_ColWidth(ColAmt1, 12)

            .Col = ColAmt2
            .CellType = SS_CELL_TYPE_FLOAT
            .set_ColWidth(ColAmt2, 12)

            .Col = ColAmt3
            .CellType = SS_CELL_TYPE_FLOAT
            .set_ColWidth(ColAmt3, 12)
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF) ' &HFFFFFF

            '        MainClass.ProtectCell sprdIT, 1, .MaxRows, ColDesc, ColDesc
            '        MainClass.ProtectCell sprdIT, 1, .MaxRows, ColAmt3, ColAmt3

        End With
        MainClass.SetSpreadColor(sprdIT, mRow)
        FillSprdGrid()
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub FillSprdGrid()

        With sprdIT
            .MaxCols = ColAmt3
            .MaxRows = 19
            .Row = 1
            .Col = ColSNo
            .Text = "1."
            .Col = ColDesc
            .Text = "Accommodation"

            .Row = .Row + 1
            .Col = ColSNo
            .Text = "2."
            .Col = ColDesc
            .Text = "Cars/Other automotive"


            .Row = .Row + 1
            .Col = ColSNo
            .Text = "3."
            .Col = ColDesc
            .Text = "Sweeper, gardener, watchman or personal attendant"


            .Row = .Row + 1
            .Col = ColSNo
            .Text = "4."
            .Col = ColDesc
            .Text = "Gas, electricity, water"


            .Row = .Row + 1
            .Col = ColSNo
            .Text = "5."
            .Col = ColDesc
            .Text = "Interest free or concessional loans"


            .Row = .Row + 1
            .Col = ColSNo
            .Text = "6."
            .Col = ColDesc
            .Text = "Holiday expenses"


            .Row = .Row + 1
            .Col = ColSNo
            .Text = "7."
            .Col = ColDesc
            .Text = "Free or concessional travel"


            .Row = .Row + 1
            .Col = ColSNo
            .Text = "8."
            .Col = ColDesc
            .Text = "Free meals"


            .Row = .Row + 1
            .Col = ColSNo
            .Text = "9."
            .Col = ColDesc
            .Text = "Free education"


            .Row = .Row + 1
            .Col = ColSNo
            .Text = "10."
            .Col = ColDesc
            .Text = "Gifts, vouchers, etc."


            .Row = .Row + 1
            .Col = ColSNo
            .Text = "11."
            .Col = ColDesc
            .Text = "Credit card expenses"


            .Row = .Row + 1
            .Col = ColSNo
            .Text = "12."
            .Col = ColDesc
            .Text = "Club expenses"


            .Row = .Row + 1
            .Col = ColSNo
            .Text = "13."
            .Col = ColDesc
            .Text = "Use of movable assets by employees"


            .Row = .Row + 1
            .Col = ColSNo
            .Text = "14."
            .Col = ColDesc
            .Text = "Transfer of assets to employees"


            .Row = .Row + 1
            .Col = ColSNo
            .Text = "15."
            .Col = ColDesc
            .Text = "Value of any other benefit / amenity / service / privilege"


            .Row = .Row + 1
            .Col = ColSNo
            .Text = "16."
            .Col = ColDesc
            .Text = "Stock options (non-qualified option)"


            .Row = .Row + 1
            .Col = ColSNo
            .Text = "17."
            .Col = ColDesc
            .Text = "Other benefits or amenities"


            .Row = .Row + 1
            .Col = ColSNo
            .Text = "18."
            .Col = ColDesc
            .Text = "Total value of perquisites"

            .Row = .Row + 1
            .Col = ColSNo
            .Text = "19."
            .Col = ColDesc
            .Text = "Total value of Profits in lieu of salary as per section 17(3)"

            MainClass.ProtectCell(sprdIT, 1, .MaxRows, ColDesc, ColDesc)
            MainClass.ProtectCell(sprdIT, 1, .MaxRows, ColAmt3, ColAmt3)
        End With
    End Sub

    Private Sub CalcGrid()
        Dim cntRow As Integer
        Dim mTotValue_A As Double
        Dim mTotValue_B As Double
        Dim mAmt1 As Double
        Dim mAmt2 As Double
        Dim mAmt3 As Double

        With sprdIT
            For cntRow = 1 To 17
                .Row = cntRow
                .Col = ColAmt1
                mAmt1 = Val(.Text)

                .Col = ColAmt2
                mAmt2 = Val(.Text)

                mAmt3 = mAmt1 - mAmt2
                .Col = ColAmt3
                .Text = CStr(mAmt3)

                mTotValue_A = mTotValue_A + mAmt1
                mTotValue_B = mTotValue_B + mAmt2
            Next
            .Row = 18
            .Col = ColAmt1
            .Text = CStr(mTotValue_A)

            .Col = ColAmt2
            .Text = CStr(mTotValue_B)

            .Col = ColAmt3
            .Text = CStr(mTotValue_A - mTotValue_B)

        End With
    End Sub


    Private Sub txtEmpCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmpCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim RsEmp As ADODB.Recordset = Nothing
        Dim mName As String

        If Trim(txtEmpCode.Text) = "" Then GoTo EventExitSub
        txtEmpCode.Text = VB6.Format(Trim(txtEmpCode.Text), "000000")

        If MainClass.ValidateWithMasterTable((txtEmpCode.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtName.Text = MasterNo
        Else
            MsgInformation("Invalid Emp Code")
            Cancel = True
            GoTo EventExitSub
        End If

        SqlStr = " SELECT * FROM PAY_ITFORM12BA_HDR WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf & " Emp_Code='" & txtEmpCode.Text & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsITMain, ADODB.LockTypeEnum.adLockOptimistic)

        If RsITMain.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Call Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Click Add Button.", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = " SELECT * FROM PAY_ITFORM12BA_HDR WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf & " EmpCode='" & txtEmpCode.Text & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsITMain, ADODB.LockTypeEnum.adLockOptimistic)

            End If
        End If

        CalcGrid()
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForPrint(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForPrint(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportForPrint(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mCode As Integer

        SqlStr = " SELECT " & vbCrLf & " EMP.*, " & vbCrLf & " IH.*, ID.* " & vbCrLf & " FROM " & vbCrLf & " PAY_ITFORM12BA_HDR IH, " & vbCrLf & " PAY_ITFORM12BA_DET ID, " & vbCrLf & " PAY_EMPLOYEE_MST EMP " & vbCrLf & " WHERE IH.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " AND IH.FYEAR =" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND IH.COMPANY_CODE = ID.COMPANY_CODE " & vbCrLf & " AND IH.FYEAR = ID.FYEAR " & vbCrLf & " AND IH.EMP_CODE = ID.EMP_CODE " & vbCrLf & " AND IH.COMPANY_CODE = EMP.COMPANY_CODE " & vbCrLf & " AND IH.EMP_CODE = EMP.EMP_CODE AND IH.EMP_CODE='" & MainClass.AllowSingleQuote((txtEmpCode.Text)) & "'" & vbCrLf & " ORDER BY ID.SUBROW"

        mSubTitle = ""
        mTitle = "Form No. 12BA"
        Call ShowReport(SqlStr, "ITForm12BA.Rpt", Mode, mTitle, mSubTitle)

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        If Err.Number = 32755 Or Err.Number = 20507 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        'Resume
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)

        Dim mCode As Integer
        Dim Str_Renamed As String
        Dim mFName As String
        Dim mDesignation As String
        Dim mRegdAddress As String
        Dim mAuthoSign As String
        Dim mAuthoDesg As String
        Dim mAuthoFName As String

        Report1.SQLQuery = mSqlStr

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        MainClass.AssignCRptFormulas(Report1, "Name='" & txtName.Text & "'")
        MainClass.AssignCRptFormulas(Report1, "mTANNo='" & IIf(IsDbNull(RsCompany.Fields("TDSACNO").Value), "", RsCompany.Fields("TDSACNO").Value) & "'")
        MainClass.AssignCRptFormulas(Report1, "mCircle='" & IIf(IsDbNull(RsCompany.Fields("TDSCIRCLE").Value), "", RsCompany.Fields("TDSCIRCLE").Value) & "'")
        '    MainClass.AssignCRptFormulas Report1, "Designation='" & txtDesignation.Text & "'"
        MainClass.AssignCRptFormulas(Report1, "mFYEAR='" & Year(RsCompany.Fields("START_DATE").Value) & "-" & Year(RsCompany.Fields("END_DATE").Value) & "'")

        If InStr(1, Trim(UCase(txtDesignation.Text)), "DIRECTOR", CompareMethod.Text) > 0 Then
            '    If Trim(UCase(txtDesignation.Text)) = "DIRECTOR" Then
            MainClass.AssignCRptFormulas(Report1, "IsDirector='Yes'")
        Else
            MainClass.AssignCRptFormulas(Report1, "IsDirector='No'")
        End If

        mRegdAddress = IIf(IsDbNull(RsCompany.Fields("REGD_ADDR1").Value), "", RsCompany.Fields("REGD_ADDR1").Value)
        mRegdAddress = mRegdAddress & " " & IIf(IsDbNull(RsCompany.Fields("REGD_ADDR2").Value), "", RsCompany.Fields("REGD_ADDR2").Value)
        mRegdAddress = mRegdAddress & " " & IIf(IsDbNull(RsCompany.Fields("REGD_CITY").Value), "", RsCompany.Fields("REGD_CITY").Value)
        '    mRegdAddress = mRegdAddress & " " & IIf(IsNull(RsCompany!REGD_STATE), "", RsCompany!REGD_STATE)
        mRegdAddress = mRegdAddress & " - " & IIf(IsDbNull(RsCompany.Fields("REGD_PIN").Value), "", RsCompany.Fields("REGD_PIN").Value)

        MainClass.AssignCRptFormulas(Report1, "RegdAddress=""" & mRegdAddress & """")


        mAuthoSign = IIf(IsDbNull(RsCompany.Fields("TDSAUTHORIZED").Value), "", RsCompany.Fields("TDSAUTHORIZED").Value)
        mAuthoFName = IIf(IsDbNull(RsCompany.Fields("TDSAUTHORIZED_FNAME").Value), "", RsCompany.Fields("TDSAUTHORIZED_FNAME").Value)
        mAuthoDesg = IIf(IsDbNull(RsCompany.Fields("TDSAUTHORIZED_DESIG").Value), "", RsCompany.Fields("TDSAUTHORIZED_DESIG").Value)

        MainClass.AssignCRptFormulas(Report1, "FullName='" & mAuthoSign & "'")
        MainClass.AssignCRptFormulas(Report1, "AuthDesg='" & mAuthoDesg & "'")
        MainClass.AssignCRptFormulas(Report1, "AuthoFName='" & mAuthoFName & "'")

        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub

    Private Sub TxtName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtName.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTaxDeduct_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTaxDeduct.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtTaxDeduct_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTaxDeduct.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtTaxDeduct_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTaxDeduct.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtTaxDeduct.Text = VB6.Format(Val(txtTaxDeduct.Text), "0.00")
        txtTaxPaid.Text = VB6.Format(Val(txtTaxPaid.Text), "0.00")
        txtTotalTaxPaid.Text = VB6.Format(Val(txtTaxDeduct.Text) + Val(txtTaxPaid.Text), "0.00")
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtTaxPaid_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTaxPaid.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtTaxPaid_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTaxPaid.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtTaxPaid_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTaxPaid.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtTaxDeduct.Text = VB6.Format(Val(txtTaxDeduct.Text), "0.00")
        txtTaxPaid.Text = VB6.Format(Val(txtTaxPaid.Text), "0.00")
        txtTotalTaxPaid.Text = VB6.Format(Val(txtTaxDeduct.Text) + Val(txtTaxPaid.Text), "0.00")
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtTaxPayment_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTaxPayment.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub



    Private Sub txtTaxPayment_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTaxPayment.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtTaxPayment.Text) = "" Then GoTo EventExitSub

        If Not IsDate(txtTaxPayment.Text) Then
            MsgInformation("Invalid date.")
            Cancel = True
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtTotalTaxPaid_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTotalTaxPaid.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtTotalTaxPaid_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTotalTaxPaid.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtTotalTaxPaid_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTotalTaxPaid.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtTaxDeduct.Text = VB6.Format(Val(txtTaxDeduct.Text), "0.00")
        txtTaxPaid.Text = VB6.Format(Val(txtTaxPaid.Text), "0.00")
        txtTotalTaxPaid.Text = VB6.Format(Val(txtTaxDeduct.Text) + Val(txtTaxPaid.Text), "0.00")
        eventArgs.Cancel = Cancel
    End Sub
End Class
