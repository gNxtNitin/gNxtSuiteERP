Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmITComputation2018
    Inherits System.Windows.Forms.Form
    Dim RsITEmp As ADODB.Recordset
    Dim RsITTRN As ADODB.Recordset

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection							

    Dim Shw As Boolean
    Dim xCode As String
    Dim SqlStr As String
    Dim FormActive As Boolean

    Dim mDataChange As Boolean

    Private Const ConRowHeight As Short = 12

    Private Const ColSNO As Short = 0
    Private Const ColDesc As Short = 1
    Private Const ColAmt1 As Short = 2
    Private Const ColAmt2 As Short = 3
    Private Const ColAmt3 As Short = 4
    Private Const ColAmt4 As Short = 5
    Private Const ColTotal As Short = 6

    Dim mTaxRegime As String = "O"

    Dim RowGrossSalary As Integer
    Dim RowGrossAmount As Integer
    Dim RowExemptSalary As Integer
    Dim RowTaxableSalaryBeforeSD As Integer
    Dim RowStandardDedection As Integer
    Dim RowTaxableSalary As Integer
    Dim RowIncomeOS As Integer
    Dim RowTotalIncomeOS As Integer
    Dim RowTotalIncome As Integer
    Dim RowSection6A As Integer
    Dim RowExempt80D As Integer
    Dim RowExempt80G As Integer
    Dim RowExempt80CCF As Integer
    Dim RowExempt80C As Integer
    Dim RowTotalExempt80C As Integer
    Dim RowTotalSection6A As Integer
    Dim RowTaxableIncome As Integer
    Dim RowTaxSlab As Integer
    Dim RowTotalTaxSlab As Integer
    Dim RowSurcharge As Integer
    Dim RowCessableAmount As Integer
    Dim RowCessAmount As Integer
    Dim RowTaxableAmount As Integer
    Dim RowPrepaidAmount As Integer
    Dim RowBalanceAmount As Integer
    Dim RowNetPerMonth As Integer
    Dim RowThisMonth As Integer
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
        MainClass.ButtonStatus(Me, XRIGHT, RsITEmp, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub Clear1()

        MainClass.ClearGrid(sprdIT)
        FillSprdGrid()
        TxtName.Text = ""
        txtEmpCode.Text = ""
        txtPrevSalary.Text = ""
        txtPrevChallan.Text = ""
        txtTaxRegime.Text = ""

        txtFName.Text = ""
        txtDOJ.Text = ""
        txtPANNo.Text = ""

        'txtDate.Text = Format(RunDate, "DD/MM/YYYY")							
        sprdIT.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsITEmp, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
    End Sub


    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsITEmp, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
            sprdIT.Enabled = True
        Else
            ADDMode = False
            MODIFYMode = False
            txtEmpCode_Validating(txtEmpCode, New System.ComponentModel.CancelEventArgs(True))
            sprdIT.Enabled = False
        End If
    End Sub

    Private Sub cmdResetSalary_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdResetSalary.Click

        Call ResetScreen("S")
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
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
            If RsITEmp.EOF = False Then RsITEmp.MoveFirst()
            Show1()
        End If
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Close()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If TxtName.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub
        If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then
            If Delete1() = False Then GoTo DelErrPart
        End If
        Clear1()
        Exit Sub
DelErrPart:
        MsgBox("Record Not Deleted")
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click
        SqlStr = ""

        If MainClass.SearchGridMaster((txtEmpCode.Text), "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", "EMP_FNAME", "EMP_PANNO", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            TxtName.Text = AcName
            txtEmpCode.Text = AcName1
            txtEmpCode_Validating(txtEmpCode, New System.ComponentModel.CancelEventArgs(False))
            txtDate.Focus()
        End If
        Exit Sub


    End Sub

    Private Sub frmITComputation2018_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub Reset_Renamed_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Reset_Renamed.Click

        Call ResetScreen("A")
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub sprdIT_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles sprdIT.Change

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub sprdIT_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles sprdIT.LeaveCell
        On Error GoTo ErrPart
        Dim mAmount1 As Double

        If eventArgs.newRow = -1 Then Exit Sub
        sprdIT.Row = eventArgs.row
        sprdIT.Col = eventArgs.col
        If eventArgs.col <> ColDesc Then
            sprdIT.Text = IIf(sprdIT.Text = "", "", VB6.Format(sprdIT.Text, "0.00"))
        End If

        sprdIT.Row = eventArgs.row
        If (eventArgs.row >= 50 And eventArgs.row <= 52) Or (eventArgs.row >= 54 And eventArgs.row <= 62) Then
            Select Case eventArgs.col
                Case ColAmt1
                    sprdIT.Col = ColAmt1
                    mAmount1 = Val(sprdIT.Text)

                    sprdIT.Row = eventArgs.row
                    sprdIT.Col = ColAmt2
                    If Val(sprdIT.Text) = 0 Then
                        sprdIT.Text = CStr(mAmount1)
                    End If
            End Select
        End If

        CalcGridTotal()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        'Resume							
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SqlStr = ""
        SprdView.Row = SprdView.ActiveRow

        SprdView.Col = 1
        txtEmpCode.Text = SprdView.Text

        SprdView.Col = 3
        txtDate.Text = SprdView.Text

        txtEmpCode_Validating(txtEmpCode, New System.ComponentModel.CancelEventArgs(True))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub
    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub
    Private Sub txtDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDate.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        mDataChange = True
    End Sub

    Private Sub txtDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtDate.Text) = "" Then
            MsgBox("Date cann't be blank.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        ElseIf Not IsDate(txtDate.Text) Then
            MsgBox("Invaild Date.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If

        If FYChk((txtDate.Text)) = False Then
            Cancel = True
            GoTo EventExitSub
        End If
        txtDate.Text = VB6.Format(txtDate.Text, "DD/MM/YYYY")

        '    If mDataChange = True Then							
        '        txtEmpCode_Validate True							
        '        ResetScreen							
        mDataChange = False
        '    End If							
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtEmpCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpCode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        mDataChange = True
    End Sub

    Private Sub TxtName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        mDataChange = True
    End Sub
    Private Sub frmITComputation2018_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        SqlStr = ""
        If FormActive = True Then Exit Sub

        SqlStr = " SELECT * FROM PAY_ITCOMP_HDR WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsITEmp)

        SqlStr = " SELECT * FROM PAY_ITCOMP_TRN WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsITTRN)


        AssignGrid(False)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        settextlength()
        Clear1()
        txtDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        lblTitle.Text = "Computation of Income Tax for the F.Y. " & Year(RsCompany.Fields("START_DATE").Value) & "-" & Year(RsCompany.Fields("END_DATE").Value) & " A.Y. " & Year(RsCompany.Fields("END_DATE").Value) & "-" & Year(RsCompany.Fields("END_DATE").Value) + 1
        FormActive = True
        If CmdAdd.Enabled = True Then cmdAdd_Click(CmdAdd, New System.EventArgs())
        mDataChange = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '    Resume							
    End Sub
    Private Sub frmITComputation2018_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        FillSprdGrid()
        'CellFormat							
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        'Resume							
    End Sub
    Private Sub frmITComputation2018_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        '    'PvtDBCn.Cancel							
        '    'PvtDBCn.Close							
        RsITEmp = Nothing
        '    'Set PvtDBCn = Nothing							
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim cntRow As Integer
        Clear1()

        If RsITEmp.EOF = False Then
            txtEmpCode.Text = RsITEmp.Fields("EMP_CODE").Value
            If MainClass.ValidateWithMasterTable(RsITEmp.Fields("EMP_CODE").Value, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                TxtName.Text = MasterNo
            End If

            If MainClass.ValidateWithMasterTable(RsITEmp.Fields("EMP_CODE").Value, "EMP_CODE", "EMP_FNAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtFName.Text = MasterNo
            End If

            If MainClass.ValidateWithMasterTable(RsITEmp.Fields("EMP_CODE").Value, "EMP_CODE", "EMP_PANNO", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtPANNo.Text = MasterNo
            End If

            If MainClass.ValidateWithMasterTable(RsITEmp.Fields("EMP_CODE").Value, "EMP_CODE", "EMP_DOJ", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtDOJ.Text = MasterNo
            End If



            mTaxRegime = "O"
            If MainClass.ValidateWithMasterTable(RsITEmp.Fields("EMP_CODE").Value, "EMP_CODE", "EMP_TAX_REGIME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then

                mTaxRegime = MasterNo
                txtTaxRegime.Text = IIf(mTaxRegime = "O", "OLD", "NEW")
            End If

            txtDate.Text = VB6.Format(RsITEmp.Fields("VDATE").Value, "DD/MM/YYYY")

            txtPrevSalary.Text = IIf(IsDBNull(RsITEmp.Fields("TAXABLE_INCOME_PE").Value), 0, RsITEmp.Fields("TAXABLE_INCOME_PE").Value)
            txtPrevChallan.Text = IIf(IsDBNull(RsITEmp.Fields("TDS_PE").Value), 0, RsITEmp.Fields("TDS_PE").Value)


            SqlStr = " SELECT * FROM PAY_ITCOMP_TRN WHERE " & vbCrLf & " Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf & " EMP_CODE='" & RsITEmp.Fields("EMP_CODE").Value & "' ORDER BY SUBROWNo"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsITTRN, ADODB.LockTypeEnum.adLockOptimistic)

            If RsITTRN.EOF = False Then
                With RsITTRN
                    cntRow = 1
                    Do While Not RsITTRN.EOF
                        sprdIT.Row = IIf(IsDBNull(.Fields("SUBROWNO").Value), "", .Fields("SUBROWNO").Value) ''cntRow							
                        sprdIT.Col = ColDesc
                        sprdIT.Text = IIf(IsDBNull(.Fields("Description").Value), "", .Fields("Description").Value)
                        If cntRow = 44 And Trim(sprdIT.Text) = "" Then
                            sprdIT.Text = "Interest on Self Occupied Property"
                        End If
                        sprdIT.Col = ColAmt1
                        sprdIT.Text = CStr(IIf(.Fields("AMOUNT1").Value = 0, "", .Fields("AMOUNT1").Value))
                        sprdIT.Col = ColAmt2
                        sprdIT.Text = CStr(IIf(.Fields("AMOUNT2").Value = 0, "", .Fields("AMOUNT2").Value))
                        sprdIT.Col = ColAmt3
                        sprdIT.Text = CStr(IIf(.Fields("AMOUNT3").Value = 0, "", .Fields("AMOUNT3").Value))
                        sprdIT.Col = ColAmt4
                        sprdIT.Text = CStr(IIf(.Fields("AMOUNT4").Value = 0, "", .Fields("AMOUNT4").Value))
                        sprdIT.Col = ColTotal
                        sprdIT.Text = CStr(IIf(.Fields("TotalAmount").Value = 0, "", .Fields("TotalAmount").Value))
                        cntRow = cntRow + 1
                        RsITTRN.MoveNext()
                    Loop
                End With
                sprdIT.Enabled = False
                RsITTRN.MoveFirst()
            Else
                Call ResetScreen("A")
            End If
        End If

        ADDMode = False
        MODIFYMode = False

        MainClass.ButtonStatus(Me, XRIGHT, RsITEmp, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
        'Resume							
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        If Update1() = True Then
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
        Dim cntRow As Integer
        Dim mGrossSalaryA As Double
        Dim mGrossSalaryB As Double
        Dim mGrossSalaryC As Double
        Dim mGSalary As Double
        Dim mExemptSalary As Double
        Dim mGSAfterExempt As Double
        Dim mTaxableSalaryBeforeSD As Double
        Dim mSDeduction As Double
        Dim mTaxableSalary As Double
        Dim mOtherIncome As Double
        Dim mGIncome As Double
        Dim mDed_VIA As Double
        Dim mTax_Income As Double
        Dim mTotal_Tax As Double
        Dim mRebate_VIII As Double
        Dim mRebate_88B As Double
        Dim mRebate_88C As Double
        Dim mSurcharge As Double
        Dim mTAX_AMT As Double
        Dim mTAX_DED As Double
        Dim mTAX_PAYABLE As Double
        Dim mCessableAmount As Double
        Dim mCESSAmount As Double
        Dim mRebate_80D As Double
        Dim mRebate_80G As Double
        Dim mRebate_80CCF As Double
        Dim mRebate_80C As Double


        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = " DELETE FROM PAY_ITCOMP_TRN WHERE " & vbCrLf & " Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf & " Emp_Code='" & Trim(txtEmpCode.Text) & "'"

        PubDBCn.Execute(SqlStr)

        sprdIT.Col = ColTotal
        sprdIT.Row = RowGrossAmount
        mGrossSalaryA = Val(sprdIT.Text)

        sprdIT.Row = RowGrossAmount
        mGSalary = Val(sprdIT.Text)

        sprdIT.Row = RowExemptSalary
        mExemptSalary = Val(sprdIT.Text)

        mGSAfterExempt = 0
        mSDeduction = 0
        mTaxableSalaryBeforeSD = 0

        sprdIT.Row = RowTaxableSalaryBeforeSD
        mTaxableSalaryBeforeSD = Val(sprdIT.Text)

        sprdIT.Row = RowStandardDedection
        sprdIT.Col = ColAmt1
        mSDeduction = Val(sprdIT.Text)

        sprdIT.Row = RowTaxableSalary
        sprdIT.Col = ColTotal
        mTaxableSalary = Val(sprdIT.Text)

        sprdIT.Row = RowTotalIncomeOS
        mOtherIncome = Val(sprdIT.Text)

        sprdIT.Row = RowTotalIncome
        mGIncome = Val(sprdIT.Text)

        sprdIT.Row = RowTotalSection6A
        mDed_VIA = Val(sprdIT.Text)

        sprdIT.Row = RowTaxableIncome
        mTax_Income = Val(sprdIT.Text)

        sprdIT.Row = RowTotalTaxSlab
        mTotal_Tax = Val(sprdIT.Text)

        sprdIT.Row = RowTotalSection6A
        mRebate_VIII = Val(sprdIT.Text)

        sprdIT.Row = RowSurcharge
        mSurcharge = Val(sprdIT.Text)

        sprdIT.Row = RowCessableAmount
        mCessableAmount = Val(sprdIT.Text)

        sprdIT.Row = RowCessAmount
        mCESSAmount = Val(sprdIT.Text)

        sprdIT.Row = RowTaxableAmount
        mTAX_AMT = Val(sprdIT.Text)

        sprdIT.Row = RowPrepaidAmount
        mTAX_DED = Val(sprdIT.Text)

        sprdIT.Row = RowBalanceAmount
        mTAX_PAYABLE = Val(sprdIT.Text)

        sprdIT.Row = RowExempt80D
        sprdIT.Col = ColAmt2
        mRebate_80D = Val(sprdIT.Text)

        sprdIT.Row = RowExempt80G
        sprdIT.Col = ColAmt2
        mRebate_80G = Val(sprdIT.Text)

        sprdIT.Row = RowExempt80CCF
        sprdIT.Col = ColAmt2
        mRebate_80CCF = Val(sprdIT.Text)

        sprdIT.Row = RowTotalExempt80C
        sprdIT.Col = ColAmt2
        mRebate_80C = Val(sprdIT.Text)

        If ADDMode = True Then
            SqlStr = "INSERT INTO PAY_ITCOMP_HDR ( " & vbCrLf & " COMPANY_CODE, FYEAR, EMP_CODE,  " & vbCrLf & " VDATE, GSALARY_A, GSALARY_B, GSALARY_C,  " & vbCrLf & " GSALARY, ExemptSalary, GSAfterExempt, " & vbCrLf & " SDEDUCTION, TAXABLESALARY,  " & vbCrLf & " OTHERINCOME, GINCOME, DED_VIA,  " & vbCrLf & " TAX_INCOME, TOTAL_TAX, REBATE_VIII,  " & vbCrLf & " REBATE_88B, REBATE_88C,  " & vbCrLf & " SURCHARGE, TAX_AMT, TAX_DED, TAX_PAYABLE,  " & vbCrLf & " CESSABLEAMOUNT, CESSAMOUNT, " & vbCrLf & " REBATE_80D, REBATE_80G, REBATE_80CCF, REBATE_80C, " & vbCrLf & " ADDUSER, ADDDATE,TAXABLE_INCOME_PE, TDS_PE ) " & vbCrLf & " VALUES ( "

            SqlStr = SqlStr & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & ", '" & Trim(txtEmpCode.Text) & "',  " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & Val(CStr(mGrossSalaryA)) & ", " & Val(CStr(mGrossSalaryB)) & ", " & Val(CStr(mGrossSalaryC)) & ", " & vbCrLf & " " & Val(CStr(mGSalary)) & "," & Val(CStr(mExemptSalary)) & "," & Val(CStr(mGSAfterExempt)) & ", " & vbCrLf & " " & Val(CStr(mSDeduction)) & ", " & Val(CStr(mTaxableSalary)) & ",  " & vbCrLf & " " & Val(CStr(mOtherIncome)) & ", " & Val(CStr(mGIncome)) & ", " & Val(CStr(mDed_VIA)) & ", " & vbCrLf & " " & Val(CStr(mTax_Income)) & ", " & Val(CStr(mTotal_Tax)) & ", " & Val(CStr(mRebate_VIII)) & ", " & vbCrLf & " " & Val(CStr(mRebate_88B)) & "," & Val(CStr(mRebate_88C)) & ",  " & vbCrLf & " " & Val(CStr(mSurcharge)) & ", " & Val(CStr(mTAX_AMT)) & ", " & Val(CStr(mTAX_DED)) & ", " & Val(CStr(mTAX_PAYABLE)) & ", " & vbCrLf & " " & Val(CStr(mCessableAmount)) & ", " & Val(CStr(mCESSAmount)) & ", " & vbCrLf & " " & Val(CStr(mRebate_80D)) & "," & Val(CStr(mRebate_80G)) & ",  " & Val(CStr(mRebate_80CCF)) & ", " & Val(CStr(mRebate_80C)) & ", " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " " & Val(txtPrevSalary.Text) & ", " & Val(txtPrevChallan.Text) & ")"

        Else
            SqlStr = "UPDATE PAY_ITCOMP_HDR SET " & vbCrLf _
                & " VDATE=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " GSALARY_A=" & Val(CStr(mGrossSalaryA)) & ", " & vbCrLf & " GSALARY_B=" & Val(CStr(mGrossSalaryB)) & ", " & vbCrLf & " GSALARY_C=" & Val(CStr(mGrossSalaryC)) & ",  " & vbCrLf & " GSALARY=" & Val(CStr(mGSalary)) & ", " & vbCrLf & " ExemptSalary=" & Val(CStr(mExemptSalary)) & ", " & vbCrLf & " GSAfterExempt=" & Val(CStr(mGSAfterExempt)) & ", " & vbCrLf & " SDEDUCTION=" & Val(CStr(mSDeduction)) & ", " & vbCrLf & " TAXABLESALARY=" & Val(CStr(mTaxableSalary)) & ",  " & vbCrLf & " OTHERINCOME=" & Val(CStr(mOtherIncome)) & ", " & vbCrLf & " GINCOME=" & Val(CStr(mGIncome)) & ", " & vbCrLf & " DED_VIA=" & Val(CStr(mDed_VIA)) & ",  " & vbCrLf & " TAX_INCOME=" & Val(CStr(mTax_Income)) & ", " & vbCrLf & " TOTAL_TAX=" & Val(CStr(mTotal_Tax)) & ", " & vbCrLf & " REBATE_VIII=" & Val(CStr(mRebate_VIII)) & ",  " & vbCrLf & " TAXABLE_INCOME_PE=" & Val(txtPrevSalary.Text) & "," & vbCrLf & " TDS_PE=" & Val(txtPrevChallan.Text) & ","

            SqlStr = SqlStr & vbCrLf & " REBATE_88B=" & Val(CStr(mRebate_88B)) & ", " & vbCrLf & " REBATE_88C=" & Val(CStr(mRebate_88C)) & ",  " & vbCrLf & " SURCHARGE=" & Val(CStr(mSurcharge)) & ", " & vbCrLf & " TAX_AMT=" & Val(CStr(mTAX_AMT)) & ", " & vbCrLf & " TAX_DED=" & Val(CStr(mTAX_DED)) & ", TAX_PAYABLE=" & Val(CStr(mTAX_PAYABLE)) & "," & vbCrLf & " CESSABLEAMOUNT=" & Val(CStr(mCessableAmount)) & ", CESSAMOUNT=" & Val(CStr(mCESSAmount)) & ",  " & vbCrLf & " REBATE_80D=" & Val(CStr(mRebate_80D)) & "," & vbCrLf & " REBATE_80G=" & Val(CStr(mRebate_80G)) & ", REBATE_80CCF=" & Val(CStr(mRebate_80CCF)) & "," & vbCrLf & " REBATE_80C=" & Val(CStr(mRebate_80C)) & ", " & vbCrLf _
                & " ADDUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', ADDDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " WHERE COMPANY_CODE= " & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR= " & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND EMP_CODE='" & Trim(txtEmpCode.Text) & "'"

        End If

        PubDBCn.Execute(SqlStr)

        If UpdateTrn1() = False Then GoTo UpdateError
        PubDBCn.CommitTrans()
        RsITEmp.Requery()
        Update1 = True
        Exit Function
UpdateError:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsITEmp.Requery()
        If Err.Number = -2147467259 Then
            MsgBox("Can't Modify Transaction Exists Against this Code")
            Exit Function
        End If
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        PubDBCn.Errors.Clear()
        'Resume							
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Private Function UpdateTrn1() As Boolean

        On Error GoTo UpdateError
        Dim cntRow As Integer
        Dim mDesc As Object
        Dim mAmount1 As Double
        Dim mAmount2 As Double
        Dim mAmount3 As Double
        Dim mAmount4 As Double
        Dim TotalAmount As Double
        Dim mIsTaxable As String

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

                .Col = ColAmt4
                mAmount4 = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColTotal
                TotalAmount = IIf(IsNumeric(.Text), .Text, 0)

                If cntRow = .MaxRows Then
                    mIsTaxable = "Y"
                ElseIf cntRow = .MaxRows - 1 Then
                    mIsTaxable = "T"
                Else
                    mIsTaxable = "N"
                End If

                SqlStr = " INSERT INTO PAY_ITCOMP_TRN " & vbCrLf & " ( Company_Code , FYEAR, SUBROWNO, " & vbCrLf _
                    & " EMP_CODE,VDATE,DESCRIPTION,AMOUNT1,AMOUNT2, " & vbCrLf _
                    & " AMOUNT3, AMOUNT4,TOTALAMOUNT,ISTAXABLEAMOUNT, ADDUSER, ADDDATE )  VALUES " & vbCrLf _
                    & " (" & RsCompany.Fields("COMPANY_CODE").Value & "," & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf _
                    & " " & cntRow & ",'" & Trim(txtEmpCode.Text) & "',TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & "  '" & mDesc & "'," & mAmount1 & "," & mAmount2 & ", " & vbCrLf & " " & mAmount3 & "," & mAmount4 & ", " & vbCrLf & " " & TotalAmount & ",'" & mIsTaxable & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

                PubDBCn.Execute(SqlStr)
            Next
        End With

        UpdateTrn1 = True
        Exit Function
UpdateError:
        UpdateTrn1 = False
        'Resume							
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Private Sub txtEmpCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpCode.DoubleClick
        cmdsearch_Click(cmdSearch, New System.EventArgs())
    End Sub

    Private Sub txtEmpCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEmpCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEmpCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtEmpCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            cmdsearch_Click(cmdSearch, New System.EventArgs())
        End If
    End Sub
    Private Function FieldsVarification() As Boolean
        On Error GoTo ERR1
        Dim xAmount As Decimal

        FieldsVarification = True

        If RsCompany.Fields("FYEAR").Value < 2005 Then
            MsgInformation("Invalid FY")
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtEmpCode.Text) = "" Then
            MsgInformation("Code is empty. Cannot Save")
            txtEmpCode.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtDate.Text) = "" Then
            MsgInformation("Date is empty. Cannot Save")
            txtDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Not IsDate(txtDate.Text) Then
            MsgInformation("Invaild Date. Cannot Save")
            txtDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If CheckLastYearLTAPaid() = True Then
            If MsgQuestion("Last Year LTA already Claimed. Want to Process this year also?") = CStr(MsgBoxResult.No) Then
                If CmdSave.Enabled = True Then CmdSave.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If MainClass.ValidateWithMasterTable(Trim(txtEmpCode.Text), "EMP_CODE", "EMP_CODE", "PAY_ITForm16_HDR", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "") = True Then
            MsgInformation("Form 16 already made. Cann't Save")
            If CmdSave.Enabled = True Then CmdSave.Focus()
            FieldsVarification = False
            Exit Function
        End If


        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
        End If
        If MODIFYMode = True And (RsITEmp.RecordCount = 0 Or RsITEmp.EOF = True) Then Exit Function
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        FieldsVarification = False
        'Resume							
    End Function
    Private Sub settextlength()

        On Error GoTo ERR1

        txtEmpCode.MaxLength = MainClass.SetMaxLength("EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn)

        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
    Sub AssignGrid(ByRef mRefresh As Boolean)

        Dim SqlStr As String
        SqlStr = " SELECT EMP.EMP_CODE,EMP_NAME," & vbCrLf _
            & " TO_CHAR(VDATE,'DD/MM/YYYY') AS V_DATE, " & vbCrLf _
            & " TAX_PAYABLE As TAX_AMOUNT " & vbCrLf _
            & " FROM PAY_EMPLOYEE_MST EMP,PAY_ITCOMP_HDR ITComp WHERE " & vbCrLf & " EMP.COMPANY_CODE=ITComp.COMPANY_CODE AND " & vbCrLf & " EMP.EMP_CODE=ITComp.EMP_CODE AND " & vbCrLf & " ITComp.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " ITComp.FYEAR=" & RsCompany.Fields("FYEAR").Value & " ORDER BY EMP.EMP_CODE"

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
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle							
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Function Delete1() As Boolean
        On Error GoTo DeleteErr
        SqlStr = ""


        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = " DELETE FROM PAY_ITCOMP_TRN WHERE " & vbCrLf & " Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf & " Emp_Code='" & Trim(txtEmpCode.Text) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = " DELETE FROM PAY_ITCOMP_HDR WHERE " & vbCrLf & " Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf & " Emp_Code='" & Trim(txtEmpCode.Text) & "'"
        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()
        RsITEmp.Requery()
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans()
        RsITEmp.Requery()
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
            .MaxCols = ColTotal
            .MaxRows = 1
            .set_RowHeight(mRow, ConRowHeight * 1.5)

            .Col = ColDesc
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColDesc, 50)


            .Col = ColAmt1
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColAmt1, 13)

            .Col = ColAmt2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColAmt2, 13)

            .Col = ColAmt3
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColAmt3, 13)

            .Col = ColAmt4
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColAmt4, 13)

            .Col = ColTotal
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColTotal, 13)

        End With
        MainClass.SetSpreadColor(sprdIT, mRow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub FillSprdGrid()
        Dim cntRow As Double

        RowThisMonth = 81

        With sprdIT
            .MaxCols = ColTotal
            .MaxRows = RowThisMonth

            RowGrossSalary = 1
            .Row = RowGrossSalary
            .Col = ColDesc

            .Text = "Basic Salary *"

            .Row = RowGrossSalary + 1
            .Text = "HRA"

            .Row = RowGrossSalary + 2
            .Text = "Conveyance Allowance"

            .Row = RowGrossSalary + 3
            .Text = "C.E. Allowance"

            .Row = RowGrossSalary + 4
            .Text = "Bonus"

            .Row = RowGrossSalary + 5
            .Text = "Leave Travel Concession"

            .Row = RowGrossSalary + 6
            .Text = "Medical Reimbursement"

            .Row = RowGrossSalary + 7
            .Text = "Leave Encashment"

            .Row = RowGrossSalary + 8
            .Text = "Production Incentive"

            .Row = RowGrossSalary + 9
            .Text = "Increment Arrear"

            If RsCompany.Fields("COMPANY_CODE").Value = 3 Then
                .Row = RowGrossSalary + 10
                .Text = "Other Allw. (Taxable)"
            ElseIf RsCompany.Fields("COMPANY_CODE").Value = 2 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Then
                .Row = RowGrossSalary + 10
                .Text = "D.A."
            Else
                .Row = RowGrossSalary + 10
                .Text = "Others Allow."
            End If

            If RsCompany.Fields("COMPANY_CODE").Value = 2 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Then
                .Row = RowGrossSalary + 11
                .Text = "V.D.A."

                .Row = RowGrossSalary + 12
                .Text = "Other Allowance / Inaam / Sp. Allow."

                .Row = RowGrossSalary + 13
                .Text = "Incentive"

                .Row = RowGrossSalary + 14
                .Text = "Attandance Allowance"

                .Row = RowGrossSalary + 15
                .Text = "Tour Allowance"

                .Row = RowGrossSalary + 16
                .Text = "Milk Allowance"

                .Row = RowGrossSalary + 17
                .Text = "Award to Employee (Long Service)"

                .Row = RowGrossSalary + 18
                .Text = "Gift Allowance"

                .Row = RowGrossSalary + 19
                .Text = "Washing Allowance"

                .Row = RowGrossSalary + 20
                .Text = ""
            ElseIf RsCompany.Fields("COMPANY_CODE").Value = 11 Then
                .Row = RowGrossSalary + 15
                .Text = "C.C.A."

                .Row = RowGrossSalary + 16
                .Text = "Special Allowance"

                .Row = RowGrossSalary + 17
                .Text = "Transport Allowance"

                .Row = RowGrossSalary + 18
                .Text = "EX-Gratia"
            Else
                .Row = RowGrossSalary + 11
                .Text = ""

                .Row = RowGrossSalary + 12
                .Text = ""

                .Row = RowGrossSalary + 13
                .Text = ""

                .Row = RowGrossSalary + 14
                .Text = ""

                .Row = RowGrossSalary + 15
                .Text = ""

                .Row = RowGrossSalary + 16
                .Text = "Attendance Award"

                .Row = RowGrossSalary + 17
                If RsCompany.Fields("COMPANY_CODE").Value = 16 Then
                    .Text = "EX-Gratia"
                Else
                    If RsCompany.Fields("FYEAR").Value < 2020 Then
                        .Text = "Inaam"
                    Else
                        .Text = "Deduction (Lockdown)"
                    End If
                End If

                .Row = RowGrossSalary + 18
                If RsCompany.Fields("COMPANY_CODE").Value = 16 Then
                    .Text = "Special Allowance"
                Else
                    .Text = "Special Allowance"
                End If

                .Row = RowGrossSalary + 19
                .Text = "Uniform Allowance"

                .Row = RowGrossSalary + 20
                .Text = ""
            End If

            .Row = RowGrossSalary + 21
            .Text = "Medical Allowance"

            ''28-03-2007							
            .Row = RowGrossSalary + 22
            .Text = ""

            .Row = RowGrossSalary + 23
            .Text = "Perquisite value of car"

            '        .Row = RowGrossSalary + 24							
            '        .Text = ""							

            RowGrossAmount = RowGrossSalary + 24

            .Row = RowGrossAmount
            .Text = "Gross Amount of Salary"

            RowExemptSalary = RowGrossAmount + 1
            .Row = RowExemptSalary
            .Text = "(i) Exemption On Salary (U/s 10):"

            .Row = RowExemptSalary + 1
            .Text = "Less HRA exempt from tax Least of the following"

            .Row = RowExemptSalary + 2
            .Col = ColDesc
            .Text = "a) Excess of rent paid over 10% of Basic Salary Rent Paid"

            .Row = RowExemptSalary + 3
            .Text = "10% of Basic Salary"

            .Row = RowExemptSalary + 4
            .Text = ""

            .Row = RowExemptSalary + 5
            .Text = "b) 40 or 50% of Salary"

            .Row = RowExemptSalary + 6
            .Text = "c) Actual HRA Received"

            .Row = RowExemptSalary + 7
            .Text = " (a) Exempt HRA" ''"LESS : Medical"							

            .Row = RowExemptSalary + 8
            .Text = " (b) Conveyance Allowance" ''"LESS : L.T.A."							

            .Row = RowExemptSalary + 9
            .Text = " (c) Children Education Allowance"

            .Row = RowExemptSalary + 10
            .Text = " (d) Leave Travel Concession"

            .Row = RowExemptSalary + 11
            .Text = " (e) Gratuity "

            .Row = RowExemptSalary + 12
            .Text = " (f) Uniform Maint Allowance "

            .Row = RowExemptSalary + 13
            .Text = " (g) "

            .Row = RowExemptSalary + 14
            .Text = " (h) Leave Encashment "

            .Row = RowExemptSalary + 15
            .Text = "(ii) Exemption on Professional Tax u/s 16(iii) "

            RowTaxableSalaryBeforeSD = RowExemptSalary + 16
            .Row = RowTaxableSalaryBeforeSD
            .Text = "Taxable Salary (Before Standard Dedection) :"

            RowStandardDedection = RowTaxableSalaryBeforeSD + 1
            .Row = RowStandardDedection
            .Text = "Standard Dedection :"

            RowTaxableSalary = RowStandardDedection + 1
            .Row = RowTaxableSalary
            .Text = "Taxable Salary :"

            RowIncomeOS = RowTaxableSalary + 1
            .Row = RowIncomeOS
            .Text = "Income From Other Sources"

            .Row = RowIncomeOS + 1
            .Text = "INTEREST ON SELF OCCUPIED PROPERTY"

            .Row = RowIncomeOS + 2
            .Text = ""

            .Row = RowIncomeOS + 3
            .Text = ""

            ''28-03-2007							

            '        .Row = RowIncomeOS + 4							
            '        .Text = ""							
            '							
            '        .Row = RowIncomeOS + 5							
            '        .Text = ""							
            '							
            '        .Row = RowIncomeOS + 6							
            '        .Text = ""							

            RowTotalIncomeOS = RowIncomeOS + 4
            .Row = RowTotalIncomeOS
            .Text = "Total Income (From Other Source) :"

            RowTotalIncome = RowTotalIncomeOS + 1
            .Row = RowTotalIncome
            .Text = "Gross Total Income :"

            RowSection6A = RowTotalIncome + 1
            .Row = RowSection6A
            .Text = "Less deduction Under Chapter VIA :"

            RowExempt80D = RowSection6A + 1
            .Row = RowExempt80D
            .Text = "(a) Under 80D"

            RowExempt80G = RowExempt80D + 1
            .Row = RowExempt80G
            .Text = "(b) Under 80G"

            RowExempt80CCF = RowExempt80G + 1
            .Row = RowExempt80CCF
            .Text = "(c) Under 80CCF"

            RowExempt80C = RowExempt80CCF + 1
            .Row = RowExempt80C
            .Text = "(d) Under 80C"

            .Col = ColDesc
            .Row = RowExempt80C + 1
            .Text = "(i). P.F. / V.P.F."

            .Row = RowExempt80C + 2
            .Text = "(ii). PPF"

            .Row = RowExempt80C + 3
            .Text = "(iii). LIP"

            .Row = RowExempt80C + 4
            .Text = "(iv). Repayment Housing Loan"

            .Row = RowExempt80C + 5
            .Text = "(v). N.S.C."

            .Row = RowExempt80C + 6
            .Text = "(vi). INT. on Old N.S.C."

            .Row = RowExempt80C + 7
            .Text = "(vii). IDBI/ICICI ETC. Bonds"

            .Row = RowExempt80C + 8
            .Text = "(viii). "

            .Row = RowExempt80C + 9
            .Text = "(ix). "

            RowTotalExempt80C = RowExempt80C + 10
            .Row = RowTotalExempt80C
            .Text = "Total [(i) to (ix)]"

            RowTotalSection6A = RowTotalExempt80C + 1
            .Row = RowTotalSection6A
            .Text = "Total Deduction Under Chapter VIA"

            RowTaxableIncome = RowTotalSection6A + 1
            .Row = RowTaxableIncome
            .Text = "Taxable Income :"

            RowTaxSlab = RowTaxableIncome + 1
            .Row = RowTaxSlab
            .Text = "Calculation of Tax"

            .Row = RowTaxSlab + 1
            .Text = "Tax Slabs"
            FillTaxSlabs(RowTaxSlab + 1)

            RowSurcharge = RowTotalTaxSlab + 1
            .Col = ColDesc
            .Row = RowSurcharge
            .Text = "Surcharge"

            RowCessableAmount = RowSurcharge + 1
            .Row = RowCessableAmount
            .Text = "Tax Amount(Before Cess)"

            RowCessAmount = RowCessableAmount + 1
            .Row = RowCessAmount
            If RsCompany.Fields("FYEAR").Value >= 2018 Then
                .Text = "Add Education Cess 4%"
            ElseIf RsCompany.Fields("FYEAR").Value >= 2007 Then
                .Text = "Add Education Cess 3%"
            Else
                .Text = "Add Education Cess 2%"
            End If

            RowTaxableAmount = RowCessAmount + 1
            .Row = RowTaxableAmount
            .Text = "Tax Amount"

            RowPrepaidAmount = RowTaxableAmount + 1
            .Row = RowPrepaidAmount
            .Text = "Tax already Deducted"

            RowBalanceAmount = RowPrepaidAmount + 1
            .Row = RowBalanceAmount
            .Text = "Balance"

            RowNetPerMonth = RowBalanceAmount + 1
            .Row = RowNetPerMonth
            .Text = "Tax to be deducted per Month"

            RowThisMonth = RowNetPerMonth + 1
            .Row = RowThisMonth
            .Text = "Tax to be deducted This Month"


            '        For cntRow = 1 To 3							
            '            .Row = cntRow							
            '            .Col = ColAmt4							
            '            .Text = 12							
            '        Next							
        End With
        CellFormat()
    End Sub

    Private Sub CalcSalary(ByRef mEmpCode As String, ByRef mActualSalary As Double, ByRef mActualHRA As Double, ByRef mActualConvAll As Double, ByRef mActualCEWAllowance As Double, ByRef mEstimateSalary As Double, ByRef mEstimateHRA As Double, ByRef mEstimateConvAll As Double, ByRef mEstimateCEWAllowance As Double, ByRef mActualDA As Double, ByRef mActualVDA As Double, ByRef mActualOthers As Double, ByRef mActualIncentive As Double, ByRef mActualAttnAllw As Double, ByRef mActualTourAllw As Double, ByRef mActualMedicalAllw As Double, ByRef mActualMilkAllw As Double, ByRef mActualAwardAllw As Double, ByRef mActualGiftAllw As Double, ByRef mActualWashAllw As Double, ByRef mEstimatDA As Double, ByRef mEstimatVDA As Double, ByRef mEstimatOthers As Double, ByRef mEstimatIncentive As Double, ByRef mEstimatAttnAllw As Double, ByRef mEstimatTourAllw As Double, ByRef mEstimatMedicalAllw As Double, ByRef mEstimatMilkAllw As Double, ByRef mEstimatAwardAllw As Double, ByRef mEstimatGiftAllw As Double, ByRef mEstimatWashAllw As Double, ByRef mActualCCAAllw As Double, ByRef mEstimatCCAAllw As Double, ByRef mActualSPAllw As Double, ByRef mEstimatSPAllw As Double, ByRef mActualTRANSAllw As Double, ByRef mEstimatTRANSAllw As Double, ByRef mActualEXGRATIAAllw As Double, ByRef mEstimatEXGRATIAAllw As Double, ByRef mInaam As Double, ByRef mLockDownAmt As Double)


        On Error GoTo ShowErrPart
        Dim RsSal As ADODB.Recordset
        Dim RsTemp As ADODB.Recordset
        Dim mSalMonth As Integer
        Dim cntRow As Integer
        Dim mBalMonth As Integer
        Dim mSalDate As String
        Dim xDOJ As String
        Dim xDOL As String

        Dim mFromEmpCompany As Integer
        Dim mFromEmpCode As String
        Dim mFromEmpLeaveDate As String

        Dim mToEmpCompany As Integer
        Dim mToEmpCode As String

        mBalMonth = DateDiff(Microsoft.VisualBasic.DateInterval.Month, CDate(txtDate.Text), RsCompany.Fields("END_DATE").Value)
        mActualOthers = 0
        mEstimatOthers = 0

        SqlStr = " SELECT EMP_DOJ, EMP_LEAVE_DATE " & vbCrLf & " FROM PAY_EMPLOYEE_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote((txtEmpCode.Text)) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)
        If RsTemp.EOF = False Then
            xDOJ = IIf(IsDBNull(RsTemp.Fields("EMP_DOJ").Value), "", RsTemp.Fields("EMP_DOJ").Value)
            xDOL = IIf(IsDBNull(RsTemp.Fields("EMP_LEAVE_DATE").Value), "", RsTemp.Fields("EMP_LEAVE_DATE").Value)
            If xDOL <> "" Then
                xDOL = IIf(CDate(RsCompany.Fields("END_DATE").Value) < CDate(xDOL), "", xDOL)
            End If
        End If

        xDOJ = IIf(CDate(xDOJ) < CDate(RsCompany.Fields("START_DATE").Value), RsCompany.Fields("START_DATE").Value, xDOJ)
        If xDOL <> "" Then
            mBalMonth = DateDiff(Microsoft.VisualBasic.DateInterval.Month, CDate(txtDate.Text), CDate(xDOL))

            mBalMonth = IIf(mBalMonth < 0, 0, mBalMonth)
        End If


        '    If Day(txtDate.Text) <> MainClass.LastDay(Month(txtDate.Text), Year(txtDate.Text)) Then							
        '        mBalMonth = mBalMonth + 1							
        '    End If							
        mSalDate = MainClass.LastDay(Month(CDate(txtDate.Text)), Year(CDate(txtDate.Text))) & "/" & VB6.Format(txtDate.Text, "MM/YYYY")

        '    SqlStr = " SELECT SUM(BASICSALARY) AS BASICSALARY1,SUM(PayableAmount) AS AMOUNT1," & vbCrLf _							
        ''            & " SALHEADCODE,TYPE " & vbCrLf _							
        ''            & " FROM PAY_SAL_TRN SALTRN, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf _							
        ''            & " WHERE " & vbCrLf _							
        ''            & " SALTRN.Company_Code = " & RsCompany!COMPANY_CODE & "" & vbCrLf _							
        ''            & " AND SALTRN.Company_Code = ADD_DEDUCT.Company_Code(+)" & vbCrLf _							
        ''            & " AND SALTRN.SALHEADCODE = ADD_DEDUCT.CODE(+) " & vbCrLf _							
        ''            & " AND EMP_CODE = '" & mEmpCode & "'" & vbCrLf _							
        ''            & " AND ADDDEDUCT IN (" & ConEarning & "," & ConPerks & ")" & vbCrLf _							
        ''            & " AND SAL_DATE=(SELECT MAX(SAL_DATE) FROM PAY_SAL_TRN " & vbCrLf _							
        ''            & " WHERE Company_Code = " & RsCompany!COMPANY_CODE & "" & vbCrLf _							
        ''            & " AND EMP_CODE = '" & mEmpCode & "'" & vbCrLf _							
        ''            & " AND SAL_DATE>='" & vb6.Format(RsCompany!START_DATE, "DD-MMM-YYYY") & "'" & vbCrLf _							
        ''            & " AND SAL_DATE<='" & vb6.Format(mSalDate, "DD-MMM-YYYY") & "')" & vbCrLf _							
        ''            & " AND ISARREAR='N' " & vbCrLf _							
        ''            & " GROUP BY SALHEADCODE,TYPE"							

        SqlStr = " SELECT SUM(BASICSALARY) AS BASICSALARY1,SUM(AMOUNT) AS AMOUNT1," & vbCrLf & " ADD_DEDUCTCODE,TYPE " & vbCrLf _
            & " FROM PAY_SALARYDEF_MST SALTRN, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf _
            & " WHERE " & vbCrLf & " SALTRN.Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SALTRN.Company_Code = ADD_DEDUCT.Company_Code(+)" & vbCrLf & " AND SALTRN.ADD_DEDUCTCODE = ADD_DEDUCT.CODE(+) " & vbCrLf _
            & " AND EMP_CODE = '" & mEmpCode & "'" & vbCrLf & " AND ADDDEDUCT IN (" & ConEarning & ")" & vbCrLf _
            & " AND SALARY_EFF_DATE=(SELECT MAX(SALARY_EFF_DATE) FROM PAY_SALARYDEF_MST " & vbCrLf _
            & " WHERE Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE = '" & mEmpCode & "'" & vbCrLf _
            & " AND SALARY_APP_DATE<=TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))" & vbCrLf & " GROUP BY ADD_DEDUCTCODE,TYPE"

        ''," & ConPerks & "							
        '& " AND SALTRN.FYEAR = " & RsCompany!FYEAR & "" & vbCrLf _							
        '							
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSal, ADODB.LockTypeEnum.adLockOptimistic)

        If RsSal.EOF = False Then
            '            mBalMonth = DateDiff("M", RsSal!SAL_DATE1, RsCompany!END_DATE)							
            mEstimateSalary = IIf(IsDBNull(RsSal.Fields("BASICSALARY1").Value), 0, RsSal.Fields("BASICSALARY1").Value) * mBalMonth
            Do While Not RsSal.EOF
                If RsSal.Fields("Type").Value = ConHRA Then
                    mEstimateHRA = RsSal.Fields("AMOUNT1").Value * mBalMonth
                End If
                If RsSal.Fields("Type").Value = ConConveyance Then
                    mEstimateConvAll = RsSal.Fields("AMOUNT1").Value * mBalMonth
                End If
                If RsSal.Fields("Type").Value = ConChildrenAllw Then
                    mEstimateCEWAllowance = RsSal.Fields("AMOUNT1").Value * mBalMonth
                End If
                If RsSal.Fields("Type").Value = ConDA Then
                    mEstimatDA = RsSal.Fields("AMOUNT1").Value * mBalMonth
                End If
                ''*******							

                If RsSal.Fields("Type").Value = ConVDA Then
                    mEstimatVDA = RsSal.Fields("AMOUNT1").Value * mBalMonth
                End If

                If RsSal.Fields("Type").Value = ConOthers Then
                    mEstimatOthers = mEstimatOthers + (RsSal.Fields("AMOUNT1").Value * mBalMonth)
                End If

                If RsSal.Fields("Type").Value = ConIncentiveAllw Then
                    mEstimatIncentive = RsSal.Fields("AMOUNT1").Value * mBalMonth
                End If

                If RsSal.Fields("Type").Value = ConAttendanceAllw Then
                    mEstimatAttnAllw = RsSal.Fields("AMOUNT1").Value * mBalMonth
                End If
                If RsSal.Fields("Type").Value = ConTourAllw Then
                    mEstimatTourAllw = RsSal.Fields("AMOUNT1").Value * mBalMonth
                End If

                If RsSal.Fields("Type").Value = ConMedicalAllw Then
                    mEstimatMedicalAllw = RsSal.Fields("AMOUNT1").Value * mBalMonth
                End If
                If RsSal.Fields("Type").Value = ConMilkAllw Then
                    mEstimatMilkAllw = RsSal.Fields("AMOUNT1").Value * mBalMonth
                End If

                If RsSal.Fields("Type").Value = ConAwardAllw Then
                    mEstimatAwardAllw = RsSal.Fields("AMOUNT1").Value * mBalMonth
                End If
                If RsSal.Fields("Type").Value = ConGiftAllw Then
                    mEstimatGiftAllw = RsSal.Fields("AMOUNT1").Value * mBalMonth
                End If

                If RsSal.Fields("Type").Value = ConWashAllw Then
                    mEstimatWashAllw = RsSal.Fields("AMOUNT1").Value * mBalMonth
                End If

                If RsSal.Fields("Type").Value = ConCCAAllw Then
                    mEstimatCCAAllw = RsSal.Fields("AMOUNT1").Value * mBalMonth
                End If

                If RsSal.Fields("Type").Value = ConSpecialAllw Then
                    mEstimatSPAllw = RsSal.Fields("AMOUNT1").Value * mBalMonth
                End If

                If RsSal.Fields("Type").Value = ConTransportAllw Then
                    mEstimatTRANSAllw = RsSal.Fields("AMOUNT1").Value * mBalMonth
                End If

                If RsSal.Fields("Type").Value = ConExGratiaAllw Then
                    mEstimatEXGRATIAAllw = RsSal.Fields("AMOUNT1").Value * mBalMonth
                End If

                RsSal.MoveNext()
            Loop
        End If

        SqlStr = " SELECT SUM(PayableAmount) AS AMOUNT1," & vbCrLf & " SALHEADCODE,TYPE " & vbCrLf & " FROM PAY_SAL_TRN SALTRN, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf & " WHERE " & vbCrLf & " SALTRN.Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SALTRN.Company_Code = ADD_DEDUCT.Company_Code(+)" & vbCrLf & " AND SALTRN.SALHEADCODE = ADD_DEDUCT.CODE(+) " & vbCrLf & " AND EMP_CODE = '" & mEmpCode & "'" & vbCrLf & " AND ADDDEDUCT IN (" & ConEarning & "," & ConPerks & ")" & vbCrLf _
            & " AND SAL_DATE>=TO_DATE('" & VB6.Format(RsCompany.Fields("START_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND SAL_DATE<=TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND ISARREAR<>'Y'" & vbCrLf _
            & " GROUP BY SALHEADCODE,TYPE HAVING SUM(PayableAmount)>0"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSal, ADODB.LockTypeEnum.adLockOptimistic)

        If RsSal.EOF = False Then
            Do While Not RsSal.EOF
                If RsSal.Fields("Type").Value = ConHRA Then
                    mActualHRA = RsSal.Fields("AMOUNT1").Value
                End If
                If RsSal.Fields("Type").Value = ConConveyance Then
                    mActualConvAll = RsSal.Fields("AMOUNT1").Value
                End If
                If RsSal.Fields("Type").Value = ConChildrenAllw Then
                    mActualCEWAllowance = RsSal.Fields("AMOUNT1").Value
                End If
                If RsSal.Fields("Type").Value = ConDA Then
                    mActualDA = RsSal.Fields("AMOUNT1").Value
                End If
                '''							

                If RsSal.Fields("Type").Value = ConVDA Then
                    mActualVDA = RsSal.Fields("AMOUNT1").Value
                End If

                If RsSal.Fields("Type").Value = ConOthers Then
                    mActualOthers = mActualOthers + RsSal.Fields("AMOUNT1").Value
                End If

                If RsSal.Fields("Type").Value = ConOtherEarningVar Then
                    mActualOthers = mActualOthers + RsSal.Fields("AMOUNT1").Value
                End If


                If RsSal.Fields("Type").Value = ConIncentiveAllw Then
                    mActualIncentive = RsSal.Fields("AMOUNT1").Value
                End If

                If RsSal.Fields("Type").Value = ConAttendanceAllw Then
                    mActualAttnAllw = RsSal.Fields("AMOUNT1").Value
                End If

                If RsSal.Fields("Type").Value = ConTourAllw Then
                    mActualTourAllw = RsSal.Fields("AMOUNT1").Value
                End If

                If RsSal.Fields("Type").Value = ConMedicalAllw Then
                    mActualMedicalAllw = RsSal.Fields("AMOUNT1").Value
                End If

                If RsSal.Fields("Type").Value = ConMilkAllw Then
                    mActualMilkAllw = RsSal.Fields("AMOUNT1").Value
                End If

                If RsSal.Fields("Type").Value = ConAwardAllw Then
                    mActualAwardAllw = RsSal.Fields("AMOUNT1").Value
                End If

                If RsSal.Fields("Type").Value = ConGiftAllw Then
                    mActualGiftAllw = RsSal.Fields("AMOUNT1").Value
                End If

                If RsSal.Fields("Type").Value = ConWashAllw Then
                    mActualWashAllw = RsSal.Fields("AMOUNT1").Value
                End If

                If RsSal.Fields("Type").Value = ConCCAAllw Then
                    mActualCCAAllw = RsSal.Fields("AMOUNT1").Value
                End If

                If RsSal.Fields("Type").Value = ConSpecialAllw Then
                    mActualSPAllw = RsSal.Fields("AMOUNT1").Value
                End If

                If RsSal.Fields("Type").Value = ConTransportAllw Then
                    mActualTRANSAllw = RsSal.Fields("AMOUNT1").Value
                End If

                If RsSal.Fields("Type").Value = ConExGratiaAllw Then
                    mActualEXGRATIAAllw = RsSal.Fields("AMOUNT1").Value
                End If

                If RsSal.Fields("Type").Value = ConINAAM Then
                    mInaam = RsSal.Fields("AMOUNT1").Value
                End If

                RsSal.MoveNext()
            Loop
        End If
        ''------------------LockDown Deduct Amount							

        SqlStr = " SELECT SUM(PayableAmount) AS AMOUNT1" & vbCrLf & " FROM PAY_SAL_TRN SALTRN, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf & " WHERE " & vbCrLf & " SALTRN.Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SALTRN.Company_Code = ADD_DEDUCT.Company_Code(+)" & vbCrLf & " AND SALTRN.SALHEADCODE = ADD_DEDUCT.CODE(+) " & vbCrLf & " AND EMP_CODE = '" & mEmpCode & "'" & vbCrLf & " AND ADDDEDUCT = " & ConDeduct & "" & vbCrLf & " AND TYPE = " & ConOthers & "" & vbCrLf _
            & " AND SAL_DATE>=TO_DATE('" & VB6.Format("01/04/2020", "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND SAL_DATE<=TO_DATE('" & VB6.Format("31/05/2020", "DD-MMM-YYYY") & "','DD-MON-YYYY') AND ISARREAR<>'Y'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSal, ADODB.LockTypeEnum.adLockOptimistic)

        mLockDownAmt = 0
        If RsSal.EOF = False Then
            mLockDownAmt = IIf(IsDBNull(RsSal.Fields("AMOUNT1").Value), 0, RsSal.Fields("AMOUNT1").Value) * -1
        End If


        ''-------------------------							

        ''Basic Salary SUM(PAYABLESALARY) AS BASICSALARY1							
        SqlStr = " SELECT DISTINCT PAYABLESALARY, SAL_DATE, " & vbCrLf & " ISARREAR " & vbCrLf & " FROM PAY_SAL_TRN SALTRN, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf & " WHERE " & vbCrLf & " SALTRN.Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SALTRN.Company_Code = ADD_DEDUCT.Company_Code(+)" & vbCrLf & " AND SALTRN.SALHEADCODE = ADD_DEDUCT.CODE(+) " & vbCrLf & " AND EMP_CODE = '" & mEmpCode & "'" & vbCrLf & " AND ADDDEDUCT IN (" & ConEarning & "," & ConPerks & ")" & vbCrLf _
            & " AND SAL_DATE>=TO_DATE('" & VB6.Format(RsCompany.Fields("START_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND SAL_DATE<=TO_DATE('" & VB6.Format(mSalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND ISARREAR<>'Y' " '& vbCrLf |            & " GROUP BY SALHEADCODE,TYPE HAVING SUM(PayableAmount)>0"							

        ''AND PayableAmount<>0  '20-03-2012  ''Marino Vitali (Not Pick Due to No other earning)							

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSal, ADODB.LockTypeEnum.adLockOptimistic)

        If RsSal.EOF = False Then
            Do While Not RsSal.EOF
                mActualSalary = mActualSalary + IIf(IsDBNull(RsSal.Fields("PAYABLESALARY").Value), 0, RsSal.Fields("PAYABLESALARY").Value)
                RsSal.MoveNext()
            Loop
        Else
            mActualSalary = 0
        End If


        '''Transfer Emp Data ...........							

        '    SqlStr = " SELECT * " & vbCrLf _							
        ''            & " FROM PAY_EMP_TRF_MST" & vbCrLf _							
        ''            & " WHERE " & vbCrLf _							
        ''            & " TO_COMPANY_CODE = " & RsCompany!COMPANY_CODE & "" & vbCrLf _							
        ''            & " AND TO_EMP_CODE = '" & mEmpCode & "'"							

        mToEmpCompany = RsCompany.Fields("COMPANY_CODE").Value
        mToEmpCode = mEmpCode

SearchRow:
        SqlStr = GetEmpTransferSQL(mToEmpCode, mToEmpCompany)
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then

            mFromEmpCompany = IIf(IsDBNull(RsTemp.Fields("FROM_COMPANY_CODE").Value), "", RsTemp.Fields("FROM_COMPANY_CODE").Value)
            mFromEmpCode = IIf(IsDBNull(RsTemp.Fields("FROM_EMP_CODE").Value), "", RsTemp.Fields("FROM_EMP_CODE").Value)

            If mFromEmpCode = "000727" And mFromEmpCompany = 1 Then
                mFromEmpLeaveDate = RsCompany.Fields("END_DATE").Value
            Else
                If MainClass.ValidateWithMasterTable(mFromEmpCode, "EMP_CODE", "EMP_LEAVE_DATE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & mFromEmpCompany & "") = True Then
                    mFromEmpLeaveDate = VB6.Format(Trim(MasterNo), "DD/MM/YYYY")
                End If
            End If

            SqlStr = " SELECT SUM(PayableAmount) AS AMOUNT1," & vbCrLf & " SALHEADCODE,TYPE " & vbCrLf & " FROM PAY_SAL_TRN SALTRN, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf & " WHERE " & vbCrLf & " SALTRN.Company_Code = " & mFromEmpCompany & "" & vbCrLf & " AND SALTRN.Company_Code = ADD_DEDUCT.Company_Code(+)" & vbCrLf & " AND SALTRN.SALHEADCODE = ADD_DEDUCT.CODE(+) " & vbCrLf & " AND EMP_CODE = '" & mFromEmpCode & "'" & vbCrLf & " AND ADDDEDUCT IN (" & ConEarning & "," & ConPerks & ")" & vbCrLf _
                & " AND SAL_DATE>=TO_DATE('" & VB6.Format(RsCompany.Fields("START_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND SAL_DATE<=TO_DATE('" & VB6.Format(mFromEmpLeaveDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND ISARREAR<>'Y'" & vbCrLf & " GROUP BY SALHEADCODE,TYPE HAVING SUM(PayableAmount)<>0 "


            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSal, ADODB.LockTypeEnum.adLockOptimistic)

            If RsSal.EOF = False Then
                Do While Not RsSal.EOF
                    If RsSal.Fields("Type").Value = ConHRA Then
                        mActualHRA = mActualHRA + RsSal.Fields("AMOUNT1").Value
                    End If

                    If RsSal.Fields("Type").Value = ConConveyance Then
                        mActualConvAll = mActualConvAll + RsSal.Fields("AMOUNT1").Value
                    End If

                    If RsSal.Fields("Type").Value = ConChildrenAllw Then
                        mActualCEWAllowance = mActualCEWAllowance + RsSal.Fields("AMOUNT1").Value
                    End If

                    If RsSal.Fields("Type").Value = ConDA Then
                        mActualDA = mActualDA + RsSal.Fields("AMOUNT1").Value
                    End If
                    '''							

                    If RsSal.Fields("Type").Value = ConVDA Then
                        mActualVDA = mActualVDA + RsSal.Fields("AMOUNT1").Value
                    End If

                    If RsSal.Fields("Type").Value = ConOthers Then
                        mActualOthers = mActualOthers + RsSal.Fields("AMOUNT1").Value
                    End If

                    If RsSal.Fields("Type").Value = ConOtherEarningVar Then
                        mActualOthers = mActualOthers + RsSal.Fields("AMOUNT1").Value
                    End If

                    If RsSal.Fields("Type").Value = ConIncentiveAllw Then
                        mActualIncentive = mActualIncentive + RsSal.Fields("AMOUNT1").Value
                    End If

                    If RsSal.Fields("Type").Value = ConAttendanceAllw Then
                        mActualAttnAllw = mActualAttnAllw + RsSal.Fields("AMOUNT1").Value
                    End If

                    If RsSal.Fields("Type").Value = ConTourAllw Then
                        mActualTourAllw = mActualTourAllw + RsSal.Fields("AMOUNT1").Value
                    End If

                    If RsSal.Fields("Type").Value = ConMedicalAllw Then
                        mActualMedicalAllw = mActualMedicalAllw + RsSal.Fields("AMOUNT1").Value
                    End If

                    If RsSal.Fields("Type").Value = ConMilkAllw Then
                        mActualMilkAllw = mActualMilkAllw + RsSal.Fields("AMOUNT1").Value
                    End If

                    If RsSal.Fields("Type").Value = ConAwardAllw Then
                        mActualAwardAllw = mActualAwardAllw + RsSal.Fields("AMOUNT1").Value
                    End If

                    If RsSal.Fields("Type").Value = ConGiftAllw Then
                        mActualGiftAllw = mActualGiftAllw + RsSal.Fields("AMOUNT1").Value
                    End If

                    If RsSal.Fields("Type").Value = ConWashAllw Then
                        mActualWashAllw = mActualWashAllw + RsSal.Fields("AMOUNT1").Value
                    End If

                    If RsSal.Fields("Type").Value = ConCCAAllw Then
                        mActualCCAAllw = mActualCCAAllw + RsSal.Fields("AMOUNT1").Value
                    End If

                    If RsSal.Fields("Type").Value = ConSpecialAllw Then
                        mActualSPAllw = mActualSPAllw + RsSal.Fields("AMOUNT1").Value
                    End If

                    If RsSal.Fields("Type").Value = ConTransportAllw Then
                        mActualTRANSAllw = mActualTRANSAllw + RsSal.Fields("AMOUNT1").Value
                    End If

                    If RsSal.Fields("Type").Value = ConExGratiaAllw Then
                        mActualEXGRATIAAllw = mActualEXGRATIAAllw + RsSal.Fields("AMOUNT1").Value
                    End If

                    If RsSal.Fields("Type").Value = ConINAAM Then
                        mInaam = mInaam + RsSal.Fields("AMOUNT1").Value
                    End If

                    RsSal.MoveNext()
                Loop
            End If

            ''------------------LockDown Deduct Amount							

            SqlStr = " SELECT SUM(PayableAmount) AS AMOUNT1" & vbCrLf & " FROM PAY_SAL_TRN SALTRN, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf & " WHERE " & vbCrLf & " SALTRN.Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SALTRN.Company_Code = ADD_DEDUCT.Company_Code(+)" & vbCrLf & " AND SALTRN.SALHEADCODE = ADD_DEDUCT.CODE(+) " & vbCrLf & " AND EMP_CODE = '" & mFromEmpCode & "'" & vbCrLf & " AND ADDDEDUCT = " & ConDeduct & "" & vbCrLf & " AND TYPE = " & ConOthers & "" & vbCrLf _
                & " AND SAL_DATE>=TO_DATE('" & VB6.Format("01/04/2020", "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND SAL_DATE<=TO_DATE('" & VB6.Format("31/05/2020", "DD-MMM-YYYY") & "','DD-MON-YYYY') AND ISARREAR<>'Y'"


            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSal, ADODB.LockTypeEnum.adLockOptimistic)

            If RsSal.EOF = False Then
                mLockDownAmt = mLockDownAmt + (IIf(IsDBNull(RsSal.Fields("AMOUNT1").Value), 0, RsSal.Fields("AMOUNT1").Value) * -1)
            End If

            SqlStr = " SELECT DISTINCT PAYABLESALARY, SAL_DATE, " & vbCrLf & " ISARREAR " & vbCrLf & " FROM PAY_SAL_TRN SALTRN, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf & " WHERE " & vbCrLf & " SALTRN.Company_Code = " & mFromEmpCompany & "" & vbCrLf & " AND SALTRN.Company_Code = ADD_DEDUCT.Company_Code(+)" & vbCrLf & " AND SALTRN.SALHEADCODE = ADD_DEDUCT.CODE(+) " & vbCrLf & " AND EMP_CODE = '" & mFromEmpCode & "'" & vbCrLf & " AND ADDDEDUCT IN (" & ConEarning & "," & ConPerks & ")" & vbCrLf _
                & " AND SAL_DATE>=TO_DATE('" & VB6.Format(RsCompany.Fields("START_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND SAL_DATE<=TO_DATE('" & VB6.Format(mFromEmpLeaveDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND ISARREAR<>'Y'" ''& vbCrLf |                & " GROUP BY SALHEADCODE,TYPE,ISARREAR "							


            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSal, ADODB.LockTypeEnum.adLockOptimistic)

            If RsSal.EOF = False Then
                Do While Not RsSal.EOF
                    mActualSalary = mActualSalary + IIf(IsDBNull(RsSal.Fields("PAYABLESALARY").Value), 0, RsSal.Fields("PAYABLESALARY").Value)
                    RsSal.MoveNext()
                Loop
            End If
            mToEmpCompany = mFromEmpCompany
            mToEmpCode = mFromEmpCode
            GoTo SearchRow
        End If

        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
        '    Resume							
    End Sub

    Private Sub CalcLastUnitSalary(ByRef mEmpCode As String, ByRef mLastUnitActualSalary As Double, ByRef mLastUnitActualHRA As Double)


        On Error GoTo ShowErrPart
        Dim RsSal As ADODB.Recordset
        Dim RsTemp As ADODB.Recordset
        Dim mFromEmpCompany As Integer
        Dim mFromEmpCode As String
        Dim mFromEmpLeaveDate As String
        Dim mToEmpCompany As Integer
        Dim mToEmpCode As String


        mLastUnitActualSalary = 0
        mLastUnitActualHRA = 0

        '''Transfer Emp Data ...........							

        '    SqlStr = " SELECT * " & vbCrLf _							
        ''            & " FROM PAY_EMP_TRF_MST" & vbCrLf _							
        ''            & " WHERE " & vbCrLf _							
        ''            & " TO_COMPANY_CODE = " & RsCompany!COMPANY_CODE & "" & vbCrLf _							
        ''            & " AND TO_EMP_CODE = '" & mEmpCode & "'"							

        mToEmpCompany = RsCompany.Fields("COMPANY_CODE").Value
        mToEmpCode = mEmpCode

SearchRow:
        SqlStr = GetEmpTransferSQL(mToEmpCode, mToEmpCompany)
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then

            mFromEmpCompany = IIf(IsDBNull(RsTemp.Fields("FROM_COMPANY_CODE").Value), "", RsTemp.Fields("FROM_COMPANY_CODE").Value)
            mFromEmpCode = IIf(IsDBNull(RsTemp.Fields("FROM_EMP_CODE").Value), "", RsTemp.Fields("FROM_EMP_CODE").Value)

            If mFromEmpCode = "000727" And mFromEmpCompany = 1 Then
                mFromEmpLeaveDate = RsCompany.Fields("END_DATE").Value
            Else
                If MainClass.ValidateWithMasterTable(mFromEmpCode, "EMP_CODE", "EMP_LEAVE_DATE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & mFromEmpCompany & "") = True Then
                    mFromEmpLeaveDate = VB6.Format(Trim(MasterNo), "DD/MM/YYYY")
                End If
            End If

            SqlStr = " SELECT SUM(PAYABLESALARY) AS BASICSALARY1,SUM(PayableAmount) AS AMOUNT1," & vbCrLf & " SALHEADCODE,TYPE " & vbCrLf & " FROM PAY_SAL_TRN SALTRN, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf & " WHERE " & vbCrLf & " SALTRN.Company_Code = " & mFromEmpCompany & "" & vbCrLf & " AND SALTRN.Company_Code = ADD_DEDUCT.Company_Code(+)" & vbCrLf & " AND SALTRN.SALHEADCODE = ADD_DEDUCT.CODE(+) " & vbCrLf & " AND EMP_CODE = '" & mFromEmpCode & "'" & vbCrLf & " AND ADDDEDUCT IN (" & ConEarning & "," & ConPerks & ")" & vbCrLf _
                & " AND SAL_DATE>=TO_DATE('" & VB6.Format(RsCompany.Fields("START_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND SAL_DATE<=TO_DATE('" & VB6.Format(mFromEmpLeaveDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND ISARREAR<>'Y'" & vbCrLf _
                & " GROUP BY SALHEADCODE,TYPE "


            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSal, ADODB.LockTypeEnum.adLockOptimistic)

            If RsSal.EOF = False Then
                mLastUnitActualSalary = mLastUnitActualSalary + IIf(IsDBNull(RsSal.Fields("BASICSALARY1").Value), 0, RsSal.Fields("BASICSALARY1").Value)
                Do While Not RsSal.EOF
                    If RsSal.Fields("Type").Value = ConHRA Then
                        mLastUnitActualHRA = mLastUnitActualHRA + RsSal.Fields("AMOUNT1").Value
                    End If

                    RsSal.MoveNext()
                Loop
            End If
            mToEmpCompany = mFromEmpCompany
            mToEmpCode = mFromEmpCode
            GoTo SearchRow
        End If

        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
        '    Resume							
    End Sub
    Private Function CalcArrearSalary(ByRef mEmpCode As String, ByRef xArrearSal As Double) As Double

        On Error GoTo ShowErrPart
        Dim RsSal As ADODB.Recordset
        Dim RsTemp As ADODB.Recordset
        Dim mPayableSalary As Double
        Dim mHRA As Double
        Dim mConvAll As Double
        Dim mCEWAllowance As Double
        Dim mMediAllowance As Double

        Dim mFromEmpCompany As Integer
        Dim mFromEmpCode As String
        Dim mFromEmpLeaveDate As String

        Dim mToEmpCompany As Integer
        Dim mToEmpCode As String
        Dim mOthetrsAllowance As Double

        CalcArrearSalary = 0

        SqlStr = " SELECT SUM(PAYABLESALARY) AS BASICSALARY1,SUM(PayableAmount) AS AMOUNT1," & vbCrLf & " SALHEADCODE,TYPE " & vbCrLf & " FROM PAY_SAL_TRN SALTRN, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf & " WHERE " & vbCrLf & " SALTRN.Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SALTRN.Company_Code = ADD_DEDUCT.Company_Code(+)" & vbCrLf & " AND SALTRN.SALHEADCODE = ADD_DEDUCT.CODE(+) " & vbCrLf & " AND EMP_CODE = '" & mEmpCode & "'" & vbCrLf & " AND ADDDEDUCT=" & ConEarning & "" & vbCrLf _
            & " AND SAL_DATE>=TO_DATE('" & VB6.Format(RsCompany.Fields("START_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND SAL_DATE<=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND ISARREAR IN ('Y','O')" & vbCrLf & " GROUP BY SALHEADCODE,TYPE "


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSal, ADODB.LockTypeEnum.adLockOptimistic)

        If RsSal.EOF = False Then
            mPayableSalary = IIf(IsDBNull(RsSal.Fields("BASICSALARY1").Value), 0, RsSal.Fields("BASICSALARY1").Value)
            Do While Not RsSal.EOF
                If RsSal.Fields("Type").Value = ConHRA Then
                    mHRA = RsSal.Fields("AMOUNT1").Value
                ElseIf RsSal.Fields("Type").Value = ConConveyance Then
                    mConvAll = RsSal.Fields("AMOUNT1").Value
                ElseIf RsSal.Fields("Type").Value = ConChildrenAllw Then
                    mCEWAllowance = RsSal.Fields("AMOUNT1").Value
                ElseIf RsSal.Fields("Type").Value = ConMedicalAllw Then
                    mMediAllowance = RsSal.Fields("AMOUNT1").Value
                Else
                    mOthetrsAllowance = mOthetrsAllowance + RsSal.Fields("AMOUNT1").Value
                End If
                RsSal.MoveNext()
            Loop
        End If

        '''Transfer Emp							

        '    SqlStr = " SELECT * " & vbCrLf _							
        ''            & " FROM PAY_EMP_TRF_MST" & vbCrLf _							
        ''            & " WHERE " & vbCrLf _							
        ''            & " TO_COMPANY_CODE = " & RsCompany!COMPANY_CODE & "" & vbCrLf _							
        ''            & " AND TO_EMP_CODE = '" & mEmpCode & "'"							

        mToEmpCompany = RsCompany.Fields("COMPANY_CODE").Value
        mToEmpCode = mEmpCode

SearchRow:
        SqlStr = GetEmpTransferSQL(mToEmpCode, mToEmpCompany)
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then

            mFromEmpCompany = IIf(IsDBNull(RsTemp.Fields("FROM_COMPANY_CODE").Value), "", RsTemp.Fields("FROM_COMPANY_CODE").Value)
            mFromEmpCode = IIf(IsDBNull(RsTemp.Fields("FROM_EMP_CODE").Value), "", RsTemp.Fields("FROM_EMP_CODE").Value)

            If MainClass.ValidateWithMasterTable(mFromEmpCode, "EMP_CODE", "EMP_LEAVE_DATE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & mFromEmpCompany & "") = True Then
                mFromEmpLeaveDate = VB6.Format(Trim(MasterNo), "DD/MM/YYYY")
            End If

            SqlStr = " SELECT SUM(PAYABLESALARY) AS BASICSALARY1,SUM(PayableAmount) AS AMOUNT1," & vbCrLf & " SALHEADCODE,TYPE " & vbCrLf & " FROM PAY_SAL_TRN SALTRN, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf & " WHERE " & vbCrLf & " SALTRN.Company_Code = " & mFromEmpCompany & "" & vbCrLf & " AND SALTRN.Company_Code = ADD_DEDUCT.Company_Code(+)" & vbCrLf & " AND SALTRN.SALHEADCODE = ADD_DEDUCT.CODE(+) " & vbCrLf & " AND EMP_CODE = '" & mFromEmpCode & "'" & vbCrLf & " AND ADDDEDUCT=" & ConEarning & "" & vbCrLf _
                & " AND SAL_DATE>=TO_DATE('" & VB6.Format(RsCompany.Fields("START_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND SAL_DATE<=TO_DATE('" & VB6.Format(mFromEmpLeaveDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND ISARREAR IN ('Y','O')" & vbCrLf _
                & " GROUP BY SALHEADCODE,TYPE "


            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSal, ADODB.LockTypeEnum.adLockOptimistic)

            If RsSal.EOF = False Then
                mPayableSalary = mPayableSalary + IIf(IsDBNull(RsSal.Fields("BASICSALARY1").Value), 0, RsSal.Fields("BASICSALARY1").Value)
                Do While Not RsSal.EOF
                    If RsSal.Fields("Type").Value = ConHRA Then
                        mHRA = mHRA + RsSal.Fields("AMOUNT1").Value
                    ElseIf RsSal.Fields("Type").Value = ConConveyance Then
                        mConvAll = mConvAll + RsSal.Fields("AMOUNT1").Value
                    ElseIf RsSal.Fields("Type").Value = ConChildrenAllw Then
                        mCEWAllowance = mCEWAllowance + RsSal.Fields("AMOUNT1").Value
                    ElseIf RsSal.Fields("Type").Value = ConMedicalAllw Then
                        mMediAllowance = mMediAllowance + RsSal.Fields("AMOUNT1").Value
                    Else
                        mOthetrsAllowance = mOthetrsAllowance + RsSal.Fields("AMOUNT1").Value
                    End If
                    RsSal.MoveNext()
                Loop
            End If
            mToEmpCompany = mFromEmpCompany
            mToEmpCode = mFromEmpCode

            GoTo SearchRow
        End If

        CalcArrearSalary = mPayableSalary + mHRA + mConvAll + mCEWAllowance + mMediAllowance + mOthetrsAllowance
        xArrearSal = mPayableSalary

        Exit Function
ShowErrPart:
        MsgBox(Err.Description)
        'Resume							
    End Function
    Private Function CalcPaidIT(ByRef mEmpCode As String) As Double

        On Error GoTo ShowErrPart
        Dim RsIT As ADODB.Recordset
        Dim mSalDate As String
        Dim mFromEmpCompany As Integer
        Dim mFromEmpCode As String
        Dim mFromEmpLeaveDate As String
        Dim RsTemp As ADODB.Recordset

        Dim mToEmpCompany As Integer
        Dim mToEmpCode As String

        CalcPaidIT = 0
        mSalDate = MainClass.LastDay(Month(CDate(txtDate.Text)), Year(CDate(txtDate.Text))) & "/" & VB6.Format(txtDate.Text, "MM/YYYY")

        '    SqlStr = " SELECT SUM(PayableAmount) AS AMOUNT1 " & vbCrLf _							
        ''            & " FROM PAY_SAL_TRN SALTRN, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf _							
        ''            & " WHERE " & vbCrLf _							
        ''            & " SALTRN.Company_Code = " & RsCompany!COMPANY_CODE & "" & vbCrLf _							
        ''            & " AND SALTRN.Company_Code = ADD_DEDUCT.Company_Code(+)" & vbCrLf _							
        ''            & " AND SALTRN.SALHEADCODE = ADD_DEDUCT.CODE(+) " & vbCrLf _							
        ''            & " AND EMP_CODE = '" & mEmpCode & "'" & vbCrLf _							
        ''            & " AND TYPE=" & ConIncomeTax & "" & vbCrLf _							
        ''            & " AND SAL_DATE>='" & vb6.Format(RsCompany!START_DATE, "DD-MMM-YYYY") & "'" & vbCrLf _							
        ''            & " AND SAL_DATE<='" & vb6.Format(mSalDate, "DD-MMM-YYYY") & "'"							
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
        ''            & " SALTRN.Company_Code = " & RsCompany!COMPANY_CODE & "" & vbCrLf _							
        ''            & " AND SALTRN.Company_Code = ADD_DEDUCT.Company_Code" & vbCrLf _							
        ''            & " AND SALTRN.ADD_DEDUCTCODE = ADD_DEDUCT.CODE " & vbCrLf _							
        ''            & " AND SALTRN.SAL_FLAG='E' AND EMP_CODE = '" & mEmpCode & "'" & vbCrLf _							
        ''            & " AND TYPE=" & ConIncomeTax & "" & vbCrLf _							
        ''            & " AND SAL_MONTH>='" & vb6.Format(RsCompany!START_DATE, "DD-MMM-YYYY") & "'" & vbCrLf _							
        ''            & " AND SAL_MONTH<='" & vb6.Format(mSalDate, "DD-MMM-YYYY") & "'"							
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
            CalcPaidIT = CalcPaidIT + IIf(IsDBNull(RsIT.Fields("TDS_AMOUNT").Value), 0, RsIT.Fields("TDS_AMOUNT").Value)
        End If

        '''Transfer Emp Data ...........							

        '    SqlStr = " SELECT * " & vbCrLf _							
        ''            & " FROM PAY_EMP_TRF_MST" & vbCrLf _							
        ''            & " WHERE " & vbCrLf _							
        ''            & " TO_COMPANY_CODE = " & RsCompany!COMPANY_CODE & "" & vbCrLf _							
        ''            & " AND TO_EMP_CODE = '" & mEmpCode & "'"							

        mToEmpCompany = RsCompany.Fields("COMPANY_CODE").Value
        mToEmpCode = mEmpCode

SearchRow:
        SqlStr = GetEmpTransferSQL(mToEmpCode, mToEmpCompany)
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then


            mFromEmpCompany = IIf(IsDBNull(RsTemp.Fields("FROM_COMPANY_CODE").Value), "", RsTemp.Fields("FROM_COMPANY_CODE").Value)
            mFromEmpCode = IIf(IsDBNull(RsTemp.Fields("FROM_EMP_CODE").Value), "", RsTemp.Fields("FROM_EMP_CODE").Value)

            If MainClass.ValidateWithMasterTable(mFromEmpCode, "EMP_CODE", "EMP_LEAVE_DATE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & mFromEmpCompany & "") = True Then
                mFromEmpLeaveDate = VB6.Format(Trim(MasterNo), "DD/MM/YYYY")
            End If

            SqlStr = " SELECT SUM(PayableAmount) AS AMOUNT1 " & vbCrLf & " FROM PAY_SAL_TRN SALTRN, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf & " WHERE " & vbCrLf & " SALTRN.Company_Code = " & mFromEmpCompany & "" & vbCrLf & " AND SALTRN.Company_Code = ADD_DEDUCT.Company_Code(+)" & vbCrLf & " AND SALTRN.SALHEADCODE = ADD_DEDUCT.CODE(+) " & vbCrLf & " AND EMP_CODE = '" & mFromEmpCode & "'" & vbCrLf & " AND TYPE=" & ConIncomeTax & "" & vbCrLf _
                & " AND SAL_DATE>=TO_DATE('" & VB6.Format(RsCompany.Fields("START_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND SAL_DATE<=TO_DATE('" & VB6.Format(mFromEmpLeaveDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsIT, ADODB.LockTypeEnum.adLockOptimistic)


            If RsIT.EOF = False Then
                CalcPaidIT = CalcPaidIT + IIf(IsDBNull(RsIT.Fields("AMOUNT1").Value), 0, RsIT.Fields("AMOUNT1").Value)
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
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mCode As Integer


        '''''Insert Data from Grid to PrintDummyData Table...							


        If FillPrintDummyData(sprdIT, 1, sprdIT.MaxRows, 0, sprdIT.MaxCols, PubDBCn) = False Then GoTo ERR1

        SqlStr = " UPDATE TEMP_PrintDummyData set FIELD10='M' WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)


        '''''Select Record for print...							

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)

        mSubTitle = ""
        mTitle = "Computation of Income Tax for the F.Y. " & Year(RsCompany.Fields("START_DATE").Value) & "-" & Year(RsCompany.Fields("END_DATE").Value) & " A.Y. " & Year(RsCompany.Fields("END_DATE").Value) & "-" & Year(RsCompany.Fields("END_DATE").Value) + 1

        Call ShowReport(SqlStr, "ITComp.rpt", Mode, mTitle, mSubTitle)

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

        Dim mCode As String

        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        mCode = txtEmpCode.Text & " - " & Trim(TxtName.Text)
        MainClass.AssignCRptFormulas(Report1, "Name='" & mCode & "'")
        ' Report1.CopiesToPrinter = PrintCopies							
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub



    Private Sub txtEmpCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmpCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim RsEmp As ADODB.Recordset
        Dim mName As String
        Dim mEmpCode As String

        If Trim(txtEmpCode.Text) = "" Then GoTo EventExitSub
        txtEmpCode.Text = VB6.Format(txtEmpCode.Text, "000000")
        mEmpCode = txtEmpCode.Text

        If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mName = MasterNo

            If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_TAX_REGIME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mTaxRegime = MasterNo
            Else
                mTaxRegime = "O"
            End If

            SqlStr = " SELECT * FROM PAY_ITCOMP_HDR WHERE " & vbCrLf & " Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf & " EMP_CODE='" & mEmpCode & "'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsITEmp, ADODB.LockTypeEnum.adLockOptimistic)

            If RsITEmp.EOF = False Then
                ADDMode = False
                MODIFYMode = False
                Show1()
            Else

                Clear1()
                txtEmpCode.Text = mEmpCode
                TxtName.Text = mName
                txtTaxRegime.Text = IIf(mTaxRegime = "O", "OLD", "NEW")

                If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_FNAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtFName.Text = MasterNo
                End If

                If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_PANNO", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtPANNo.Text = MasterNo
                End If

                If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_DOJ", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtDOJ.Text = MasterNo
                End If


                Call ResetScreen("A")
            End If
            CalcGridTotal()
        Else
            Clear1()
            MsgBox("Employee Code Does Not Exist In Master.", MsgBoxStyle.Information)
            Cancel = True
        End If

        mDataChange = False
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub CalcGridTotal()
        On Error GoTo ErrPart

        Dim mTotalHeading As Double
        Dim cntRow As Integer
        Dim mAmt1 As Double
        Dim mAmt3 As Double
        Dim mMonth As Double
        Dim mIncentive As Double
        Dim mBSal As Double
        Dim mSalary As Double
        Dim mMedical As Double
        Dim mConveyance As Double
        Dim mConveyanceAllw As Double
        Dim mCEW As Double
        Dim mLTA As Double
        Dim mHRA As Double
        Dim mLeastHRA As Double
        Dim RentApp As Double
        Dim mNetTaxAmt As Double
        Dim mRentpaid As Double
        Dim mDeduction As Double
        Dim mGrossTaxableSalary As Double
        Dim mTaxableSalaryBeforeSD As Double
        Dim mStandardDedection As Double
        Dim mTaxableSalary As Double
        Dim mIncomeFromOS As Double
        Dim mGrossTotalIncome As Double
        Dim mDedcUnder6A As Double
        Dim mTaxableIncome As Double
        Dim mUniformAllow As Double

        Dim mSlabMin As Double
        Dim mSlabMax As Double
        Dim mTempSlab As Double
        Dim mTaxSlab As Double
        Dim SlabTotal As Double
        Dim mSlabRate As Double
        'Dim mRelief As Double							
        'Dim mTempRelief As Double							
        'Dim mReliefBond As Double							
        'Dim mReliefAmount As Double							
        'Dim mReliefRate As Double							
        Dim mSurcharges As Double
        Dim mSurchargesRate As Double
        Dim mTDSSal As Double
        Dim mPeriod As Integer
        Dim mBalEL As Double
        Dim mSD1 As Double
        Dim mSD2 As Double
        Dim mOtherDed1 As Double
        Dim mOtherDed2 As Double
        Dim mOtherDed3 As Double
        Dim mOtherDed4 As Double
        Dim mOtherDed5 As Double
        Dim mOtherDed As Double
        Dim mBalMonth As Double
        Dim mGetTotWorkingMon As Double
        Dim mMetroCity As String
        Dim mSalDed As Double
        Dim mReliefi As Object
        Dim mReliefii As Double
        Dim mCessableAmount As Object
        Dim mCESSAmount As Double
        'Dim mRelief88B As Double							
        'Dim mRelief88C As Double							
        'Dim mRelief88D As Double							
        Dim mGross80D As Double
        Dim mGross80G As Double
        Dim mGross80CCF As Double
        Dim mGross80C As Double

        Dim mQualifying80D As Double
        Dim mQualifying80G As Double
        Dim mQualifying80CCF As Double
        Dim mQualifying80C As Double

        Dim mReliefOth1 As Double
        Dim mReliefOth2 As Double
        Dim mReliefTotal As Double
        Dim mWorkingMonth As Integer
        Dim mDOJ As String
        Dim mDOL As String
        Dim mLastUnitWorkingMonth As Integer
        Dim mTotalTaxableAmount As Double
        Dim mLastUnitActualSalary As Double
        Dim mLastUnitActualHRA As Double
        Dim mRebeatUnder87A As Double
        Dim m80CSlab As Double


        If Trim(txtEmpCode.Text) = "" Then Exit Sub

        mLastUnitWorkingMonth = GetLastUnitWorkingMonth((txtEmpCode.Text))
        Call CalcLastUnitSalary((txtEmpCode.Text), mLastUnitActualSalary, mLastUnitActualHRA)

        m80CSlab = GetChapterVISlab("80C")

        mWorkingMonth = 0
        If MainClass.ValidateWithMasterTable((txtEmpCode.Text), "EMP_CODE", "EMP_DOJ", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDOJ = MasterNo
        Else
            mDOJ = RsCompany.Fields("START_DATE").Value
        End If

        If MainClass.ValidateWithMasterTable((txtEmpCode.Text), "EMP_CODE", "EMP_LEAVE_DATE ", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDOL = Trim(MasterNo)
            If mDOL <> "" Then
                mDOL = IIf(CDate(RsCompany.Fields("END_DATE").Value) < CDate(mDOL), RsCompany.Fields("END_DATE").Value, mDOL)
            End If
        Else
            mDOL = RsCompany.Fields("END_DATE").Value
        End If
        mDOL = IIf(mDOL = "", RsCompany.Fields("END_DATE").Value, mDOL)

        If CDate(mDOJ) > CDate(RsCompany.Fields("START_DATE").Value) Then
            If CDate(mDOL) < CDate(RsCompany.Fields("END_DATE").Value) Then
                mWorkingMonth = (DateDiff(Microsoft.VisualBasic.DateInterval.Month, CDate(mDOJ), CDate(mDOL)) + 1)
            Else
                mWorkingMonth = DateDiff(Microsoft.VisualBasic.DateInterval.Month, CDate(RsCompany.Fields("START_DATE").Value), CDate(mDOJ))
                mWorkingMonth = 12 - mWorkingMonth
            End If
        ElseIf CDate(mDOL) < CDate(RsCompany.Fields("END_DATE").Value) Then
            mWorkingMonth = DateDiff(Microsoft.VisualBasic.DateInterval.Month, CDate(RsCompany.Fields("START_DATE").Value), CDate(mDOL)) + 1
        Else
            mWorkingMonth = 12
        End If
        mWorkingMonth = System.Math.Round(mWorkingMonth, 0)
        mGrossTaxableSalary = 0

        '''mPeriod = DateDiff("m", CVDate(txtDate.Text), CVDate(RsCompany!FYDateTo))							
        mPeriod = 12

        With sprdIT
            For cntRow = RowGrossSalary To RowGrossAmount - 1
                .Row = cntRow
                .Col = ColAmt1
                mAmt1 = Val(.Text)

                .Col = ColAmt3
                mAmt3 = Val(.Text)

                .Col = ColTotal
                If .Row = 1 Then
                    mSalary = mAmt1 + mAmt3
                    mBSal = mAmt1 + mAmt3
                ElseIf .Row = 2 Then
                    mHRA = mAmt1 + mAmt3
                ElseIf .Row = 3 Then
                    mConveyanceAllw = mAmt1 + mAmt3
                ElseIf .Row = 20 Then
                    mUniformAllow = mAmt1 + mAmt3
                End If


                .Text = VB6.Format(mAmt1 + mAmt3, "0.00")
                mTotalHeading = mTotalHeading + Val(.Text)
            Next

            .Row = RowGrossAmount
            .Text = VB6.Format(mTotalHeading, "0.00")


            .Row = RowExemptSalary + 2
            .Col = ColAmt1
            mAmt3 = Val(.Text)

            .Col = ColAmt2
            mMonth = Val(.Text)

            .Col = ColAmt3
            .Text = VB6.Format(mAmt3 * mMonth, "0.00")
            mRentpaid = mAmt3 * mMonth

            .Row = RowExemptSalary + 3
            .Col = ColAmt3

            ''- mLastUnitActualSalary							

            If (mWorkingMonth + mLastUnitWorkingMonth) = 0 Then
                RentApp = 0
            Else
                RentApp = ((mSalary) * mMonth / (mWorkingMonth + mLastUnitWorkingMonth)) * 0.1
            End If
            .Text = VB6.Format(RentApp, "0.00")

            .Row = RowExemptSalary + 4
            .Col = ColAmt3
            .Text = VB6.Format(mRentpaid - RentApp, "0.00")
            mLeastHRA = mRentpaid - RentApp

            .Row = RowExemptSalary + 5
            .Col = ColAmt3
            If MainClass.ValidateWithMasterTable((txtEmpCode.Text), "EMP_CODE", "ISMETROCITY", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mMetroCity = MasterNo
            Else
                mMetroCity = "N"
            End If
            ''- mLastUnitActualSalary							
            If (mWorkingMonth + mLastUnitWorkingMonth) = 0 Then
                mSalDed = 0
            Else
                mSalDed = ((mSalary) * mMonth / (mWorkingMonth + mLastUnitWorkingMonth)) * (IIf(mMetroCity = "Y", 0.5, 0.4))
            End If

            .Text = VB6.Format(mSalDed, "0.00")
            mLeastHRA = IIf(mSalDed < mLeastHRA, mSalDed, mLeastHRA)

            .Row = RowExemptSalary + 6
            .Col = ColAmt3
            ''' - mLastUnitActualSalary							
            If (mWorkingMonth + mLastUnitWorkingMonth) = 0 Then
                mHRA = 0
            Else
                mHRA = (mHRA) * mMonth / (mWorkingMonth + mLastUnitWorkingMonth)
            End If
            .Text = VB6.Format(mHRA, "0.00")
            mLeastHRA = IIf(mHRA < mLeastHRA, mHRA, mLeastHRA)

            .Row = RowExemptSalary + 7
            .Col = ColAmt4
            mLeastHRA = IIf(mLeastHRA < 0, 0, mLeastHRA)
            .Text = VB6.Format(mLeastHRA, "0.00")

            .Row = RowExemptSalary + 8
            .Col = ColAmt4

            mConveyance = GetConveyanceAllw(mConveyanceAllw)
            .Text = VB6.Format(mConveyance, "0.00")

            .Row = RowExemptSalary + 9
            .Col = ColAmt4
            mCEW = Val(.Text)

            .Row = RowExemptSalary + 10
            .Col = ColAmt4
            mLTA = Val(.Text)

            .Row = RowExemptSalary + 11
            .Col = ColAmt4
            mOtherDed1 = Val(.Text)

            .Row = RowExemptSalary + 12
            .Col = ColAmt4
            .Text = VB6.Format(mUniformAllow, "0.00")
            mOtherDed2 = Val(.Text)

            .Row = RowExemptSalary + 13
            .Col = ColAmt4
            mOtherDed3 = Val(.Text)

            .Row = RowExemptSalary + 14
            .Col = ColAmt4
            mOtherDed4 = Val(.Text)

            .Row = RowExemptSalary + 15
            .Col = ColAmt4
            mOtherDed5 = Val(.Text)

            mOtherDed = (mLeastHRA + mConveyance + mCEW + mLTA + mOtherDed1 + mOtherDed2 + mOtherDed3 + mOtherDed4 + mOtherDed5)
            .Col = ColTotal
            .Text = VB6.Format(mOtherDed, "0.00")

            mTaxableSalaryBeforeSD = mTotalHeading - mOtherDed

            .Row = RowTaxableSalaryBeforeSD
            .Col = ColTotal
            .Text = VB6.Format(mTaxableSalaryBeforeSD, "0.00")

            .Row = RowStandardDedection
            .Col = ColAmt1
            mStandardDedection = GetStandardDedection(mTaxableSalaryBeforeSD)
            .Text = VB6.Format(mStandardDedection, "0.00")


            mTaxableSalary = mTaxableSalaryBeforeSD - mStandardDedection

            .Row = RowTaxableSalary
            .Col = ColTotal
            .Text = VB6.Format(mTaxableSalary, "0.00")

            mIncomeFromOS = 0
            For cntRow = RowIncomeOS + 1 To RowIncomeOS + 3
                .Row = cntRow
                .Col = ColAmt1
                mIncomeFromOS = mIncomeFromOS + Val(.Text)
            Next

            .Row = RowTotalIncomeOS
            .Col = ColTotal
            .Text = CStr(mIncomeFromOS)

            mGrossTotalIncome = mTaxableSalary + mIncomeFromOS

            .Row = RowTotalIncome
            .Col = ColTotal
            .Text = CStr(mGrossTotalIncome)

            .Row = RowExempt80D
            .Col = ColAmt1
            mGross80D = Val(.Text)

            .Col = ColAmt2
            If RsCompany.Fields("FYEAR").Value <= 2014 Then
                mQualifying80D = IIf(mGross80D <= 15000, mGross80D, 15000)
            Else
                mQualifying80D = IIf(mGross80D <= 30000, mGross80D, 30000)
            End If
            .Text = VB6.Format(mQualifying80D, "0.00")

            .Row = RowExempt80G
            .Col = ColAmt1
            mGross80G = Val(.Text)

            .Col = ColAmt2
            mQualifying80G = Val(.Text)

            .Row = RowExempt80CCF
            .Col = ColAmt1
            mGross80CCF = Val(.Text)

            .Col = ColAmt2
            mQualifying80CCF = IIf(mGross80CCF <= 20000, mGross80CCF, 20000)
            .Text = VB6.Format(mQualifying80CCF, "0.00")

            mGross80C = 0
            For cntRow = RowExempt80C To RowTotalExempt80C - 1
                .Row = cntRow
                .Col = ColAmt1
                mGross80C = mGross80C + Val(.Text)

                .Col = ColAmt2
                mQualifying80C = mQualifying80C + Val(.Text)
            Next

            .Row = RowTotalExempt80C
            .Col = ColAmt1
            .Text = CStr(mGross80C)

            .Col = ColAmt2
            mQualifying80C = IIf(mQualifying80C <= m80CSlab, mQualifying80C, m80CSlab)
            .Text = CStr(mQualifying80C)

            mDedcUnder6A = 0

            .Row = RowTotalSection6A
            .Col = ColTotal
            mDedcUnder6A = mQualifying80D + mQualifying80G + mQualifying80CCF + mQualifying80C
            .Text = CStr(mDedcUnder6A)

            mTaxableIncome = mGrossTotalIncome - mDedcUnder6A
            .Row = RowTaxableIncome
            .Col = ColTotal
            .Text = CStr(mTaxableIncome)

            mTempSlab = mTaxableIncome
            FillTaxSlabs(RowTaxSlab + 1)
            For cntRow = RowTaxSlab + 2 To RowTotalTaxSlab
                .Row = cntRow
                .Col = ColDesc
                mSlabMin = Val(Mid(.Text, 1, InStr(1, .Text, "-")))
                mSlabMax = Val(Mid(.Text, InStr(1, .Text, "-") + 1, Len(.Text)))
                mTaxSlab = mSlabMax - mSlabMin + 1

                .Col = ColAmt1
                If mTaxSlab <= mTempSlab Then
                    .Text = VB6.Format(mTaxSlab, "0.00")
                Else
                    If mTempSlab >= 0 Then
                        .Text = VB6.Format(mTempSlab, "0.00")
                    Else
                        .Text = CStr(0)
                    End If
                    mTaxSlab = mTempSlab
                End If
                mTempSlab = mTempSlab - (mTaxSlab)

                .Col = ColAmt2
                mSlabRate = Val(.Text)

                .Col = ColAmt3
                SlabTotal = SlabTotal + System.Math.Round(mTaxSlab * mSlabRate, 0)
                .Text = VB6.Format(System.Math.Round(mTaxSlab * mSlabRate, 0), "0.00")
            Next
            .Row = RowTotalTaxSlab
            .Col = ColTotal
            .Text = VB6.Format(SlabTotal, "0.00")

            .Row = RowSurcharge
            mSurchargesRate = GetSurchargeRate(mTaxableIncome) ''SlabTotal - mReliefAmount)							
            mSurcharges = System.Math.Round(SlabTotal, 0) * (mSurchargesRate / 100)
            mSurcharges = System.Math.Round(mSurcharges, 0)

            .Col = ColDesc
            .Text = "Surcharge @" & mSurchargesRate & "%"

            .Col = ColTotal
            .Text = CStr(mSurcharges)

            .Row = RowCessableAmount

            If RsCompany.Fields("FYEAR").Value >= 2014 Then
                .Col = ColAmt1
                If mTaxableIncome < 500001 Then
                    mRebeatUnder87A = Val(.Text)
                Else
                    .Text = CStr(0)
                    mRebeatUnder87A = 0
                End If
            Else
                mRebeatUnder87A = 0
            End If

            .Col = ColTotal
            mCessableAmount = SlabTotal + mSurcharges - mRebeatUnder87A
            mCessableAmount = IIf(mCessableAmount < 0, 0, mCessableAmount)

            .Text = mCessableAmount

            .Row = RowCessAmount
            .Col = ColTotal
            If RsCompany.Fields("FYEAR").Value >= 2018 Then
                mCESSAmount = mCessableAmount * 0.04
            ElseIf RsCompany.Fields("FYEAR").Value >= 2007 Then
                mCESSAmount = mCessableAmount * 0.03
            Else
                mCESSAmount = mCessableAmount * 0.02
            End If

            mCESSAmount = System.Math.Round(mCESSAmount, 0)
            .Text = CStr(mCESSAmount)

            .Row = RowTaxableAmount

            If RsCompany.Fields("FYEAR").Value < 2014 Then
                .Col = ColAmt1
                If mTaxableIncome < 500001 Then
                    mRebeatUnder87A = Val(.Text)
                Else
                    .Text = CStr(0)
                    mRebeatUnder87A = 0
                End If
            Else
                mRebeatUnder87A = 0
            End If

            .Col = ColTotal
            mTotalTaxableAmount = mCessableAmount + mCESSAmount - mRebeatUnder87A
            mTotalTaxableAmount = System.Math.Round(mTotalTaxableAmount, 0)
            .Text = CStr(mTotalTaxableAmount)

            .Row = RowPrepaidAmount
            .Col = ColAmt1
            mTDSSal = Val(.Text)

            .Col = ColTotal
            .Text = CStr(mTDSSal)

            .Row = RowBalanceAmount
            .Col = ColTotal
            .Text = CStr(mTotalTaxableAmount - mTDSSal)


            .Row = RowNetPerMonth
            .Col = ColAmt2
            mBalMonth = Val(.Text)

            .Col = ColTotal
            If mBalMonth = 0 Then
                .Text = CStr(0)
            Else
                .Text = CStr((mTotalTaxableAmount - mTDSSal) / mBalMonth)
            End If

        End With
        CellFormat()
        Exit Sub
ErrPart:
        '    Resume							
        MsgBox(Err.Description)
    End Sub

    Private Function GetLastUnitWorkingMonth(ByRef pToEmpCode As String) As Integer

        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset
        Dim RsTempEmp As ADODB.Recordset
        Dim mFromEmpCode As String
        Dim mFromCompanyCode As Integer
        Dim mWorkingMonth As Integer
        Dim mDOJ As String
        Dim mDOL As String
        Dim mToEmpCode As String
        Dim mToEmpCompany As Integer

        GetLastUnitWorkingMonth = 0
        '    SqlStr = " SELECT * " & vbCrLf _							
        ''            & " FROM PAY_EMP_TRF_MST" & vbCrLf _							
        ''            & " WHERE " & vbCrLf _							
        ''            & " TO_COMPANY_CODE = " & RsCompany!COMPANY_CODE & "" & vbCrLf _							
        ''            & " AND TO_EMP_CODE = '" & pToEmpCode & "'"							

        mToEmpCompany = RsCompany.Fields("COMPANY_CODE").Value
        mToEmpCode = pToEmpCode

SearchRow:
        SqlStr = GetEmpTransferSQL(mToEmpCode, mToEmpCompany)
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            mFromCompanyCode = IIf(IsDBNull(RsTemp.Fields("FROM_COMPANY_CODE").Value), "", RsTemp.Fields("FROM_COMPANY_CODE").Value)
            mFromEmpCode = IIf(IsDBNull(RsTemp.Fields("FROM_EMP_CODE").Value), "", RsTemp.Fields("FROM_EMP_CODE").Value)

            SqlStr = " SELECT EMP_DOJ,EMP_LEAVE_DATE " & vbCrLf & " FROM PAY_EMPLOYEE_MST" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE = " & mFromCompanyCode & "" & vbCrLf & " AND EMP_CODE = '" & mFromEmpCode & "'"


            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempEmp, ADODB.LockTypeEnum.adLockOptimistic)

            If RsTempEmp.EOF = False Then
                mDOJ = IIf(IsDBNull(RsTempEmp.Fields("EMP_DOJ").Value), "", RsTempEmp.Fields("EMP_DOJ").Value)
                mDOL = IIf(IsDBNull(RsTempEmp.Fields("EMP_LEAVE_DATE").Value), "", RsTempEmp.Fields("EMP_LEAVE_DATE").Value)

                If mDOL <> "" Then
                    If CDate(mDOL) < CDate(RsCompany.Fields("START_DATE").Value) Then GoTo NextSearch
                    mDOL = IIf(CDate(RsCompany.Fields("END_DATE").Value) < CDate(mDOL), RsCompany.Fields("END_DATE").Value, mDOL)
                End If
            Else
                mDOJ = RsCompany.Fields("START_DATE").Value
                mDOL = RsCompany.Fields("END_DATE").Value
            End If

            mDOL = IIf(mDOL = "", RsCompany.Fields("END_DATE").Value, mDOL)

            If CDate(mDOJ) > CDate(RsCompany.Fields("START_DATE").Value) Then
                If CDate(mDOL) < CDate(RsCompany.Fields("END_DATE").Value) Then
                    mWorkingMonth = DateDiff(Microsoft.VisualBasic.DateInterval.Month, CDate(mDOJ), CDate(mDOL)) + 1
                Else
                    mWorkingMonth = DateDiff(Microsoft.VisualBasic.DateInterval.Month, CDate(RsCompany.Fields("START_DATE").Value), CDate(mDOJ))
                    mWorkingMonth = 12 - mWorkingMonth
                End If
            ElseIf CDate(mDOL) < CDate(RsCompany.Fields("END_DATE").Value) Then
                mWorkingMonth = DateDiff(Microsoft.VisualBasic.DateInterval.Month, CDate(RsCompany.Fields("START_DATE").Value), CDate(mDOL)) + 1
            Else
                mWorkingMonth = 12
            End If
            If mWorkingMonth < 0 Then
                GetLastUnitWorkingMonth = 0
            Else
                GetLastUnitWorkingMonth = GetLastUnitWorkingMonth + System.Math.Round(mWorkingMonth, 0)
            End If
NextSearch:

            mToEmpCompany = mFromCompanyCode
            mToEmpCode = mFromEmpCode
            GoTo SearchRow
        End If

        Exit Function
ErrPart:
        '    Resume							
        GetLastUnitWorkingMonth = 0
        MsgBox(Err.Description)
    End Function

    Private Function CalcPF(ByRef mCode As String) As Double

        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset
        Dim RsAttn As ADODB.Recordset
        Dim mBalMonth As Integer
        Dim mDOL As String
        Dim mFromEmpCompany As Integer
        Dim mFromEmpCode As String
        Dim mFromEmpLeaveDate As String

        Dim mToEmpCompany As Integer
        Dim mToEmpCode As String


        If MainClass.ValidateWithMasterTable(mCode, "EMP_CODE", "EMP_LEAVE_DATE ", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDOL = Trim(MasterNo)
            If mDOL <> "" Then
                mDOL = IIf(CDate(RsCompany.Fields("END_DATE").Value) < CDate(mDOL), RsCompany.Fields("END_DATE").Value, mDOL)
            End If
        Else
            mDOL = RsCompany.Fields("END_DATE").Value
        End If
        mDOL = IIf(mDOL = "", RsCompany.Fields("END_DATE").Value, mDOL)

        mBalMonth = DateDiff(Microsoft.VisualBasic.DateInterval.Month, CDate(txtDate.Text), CDate(mDOL))

        If VB.Day(CDate(txtDate.Text)) <> MainClass.LastDay(Month(CDate(txtDate.Text)), Year(CDate(txtDate.Text))) Then
            mBalMonth = mBalMonth + 1
        End If

        SqlStr = " SELECT SUM(PFAMT+VPFAMT) AS PFAMT1 " & vbCrLf & " FROM PAY_PFESI_TRN WHERE " & vbCrLf & " Company_Code =" & RsCompany.Fields("COMPANY_CODE").Value & " AND  " & vbCrLf _
            & " EMP_CODE = '" & mCode & "' AND SAL_DATE>=TO_DATE('" & VB6.Format(RsCompany.Fields("START_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND SAL_DATE<=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')  AND PFAMT+VPFAMT>0"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAttn, ADODB.LockTypeEnum.adLockOptimistic)

        If RsAttn.EOF = False Then
            CalcPF = IIf(IsDBNull(RsAttn.Fields("PFAMT1").Value), 0, RsAttn.Fields("PFAMT1").Value)
        End If

        '    SqlStr = " SELECT PFAMT " & vbCrLf _							
        ''            & " FROM PAY_PFESI_TRN WHERE " & vbCrLf _							
        ''            & " Company_Code =" & RsCompany!COMPANY_CODE & " AND  " & vbCrLf _							
        ''            & " EMP_CODE = '" & mCode & "' AND ISARREAR='N' AND SAL_DATE= ( " & vbCrLf _							
        ''            & " SELECT MAX(SAL_DATE) " & vbCrLf _							
        ''            & " FROM PAY_PFESI_TRN WHERE " & vbCrLf _							
        ''            & " Company_Code =" & RsCompany!COMPANY_CODE & " AND  " & vbCrLf _							
        ''            & " EMP_CODE = '" & mCode & "' AND ISARREAR='N'" & vbCrLf _							
        ''            & " AND SAL_DATE<='" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "' )"							

        SqlStr = " SELECT AMOUNT AS PFAMT FROM PAY_SalaryDef_MST SD, PAY_SALARYHEAD_MST SMST" & vbCrLf & " WHERE SD.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SD.COMPANY_CODE=SMST.COMPANY_CODE" & vbCrLf & " AND SD.ADD_DEDUCTCODE=SMST.CODE " & vbCrLf & " AND SMST.TYPE=" & ConPF & "" & vbCrLf & " AND SD.EMP_CODE='" & mCode & "'" & vbCrLf & " AND SD.SALARY_EFF_DATE=(SELECT MAX(SALARY_EFF_DATE) FROM PAY_SalaryDef_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & mCode & "'" & vbCrLf _
            & " AND SALARY_EFF_DATE< TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAttn, ADODB.LockTypeEnum.adLockOptimistic)
        If RsAttn.EOF = False Then
            '        mBalMonth = DateDiff("M", RsAttn!SAL_DATE1, RsCompany!END_DATE)							
            CalcPF = CalcPF + (IIf(IsDBNull(RsAttn.Fields("PFAMT").Value), 0, RsAttn.Fields("PFAMT").Value) * mBalMonth)
        End If

        '''VPF ..							
        SqlStr = " SELECT AMOUNT AS PFAMT FROM PAY_SalaryDef_MST SD, PAY_SALARYHEAD_MST SMST" & vbCrLf & " WHERE SD.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SD.COMPANY_CODE=SMST.COMPANY_CODE" & vbCrLf & " AND SD.ADD_DEDUCTCODE=SMST.CODE " & vbCrLf & " AND SMST.TYPE=" & ConVPFAllw & "" & vbCrLf & " AND SD.EMP_CODE='" & mCode & "'" & vbCrLf & " AND SD.SALARY_EFF_DATE=(SELECT MAX(SALARY_EFF_DATE) FROM PAY_SalaryDef_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE='" & mCode & "'" & vbCrLf _
            & " AND SALARY_EFF_DATE< TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAttn, ADODB.LockTypeEnum.adLockOptimistic)
        If RsAttn.EOF = False Then
            '        mBalMonth = DateDiff("M", RsAttn!SAL_DATE1, RsCompany!END_DATE)							
            CalcPF = CalcPF + (IIf(IsDBNull(RsAttn.Fields("PFAMT").Value), 0, RsAttn.Fields("PFAMT").Value) * mBalMonth)
        End If

        '''Transfer Emp Data ...........							

        '    SqlStr = " SELECT * " & vbCrLf _							
        ''            & " FROM PAY_EMP_TRF_MST" & vbCrLf _							
        ''            & " WHERE " & vbCrLf _							
        ''            & " TO_COMPANY_CODE = " & RsCompany!COMPANY_CODE & "" & vbCrLf _							
        ''            & " AND TO_EMP_CODE = '" & mCode & "'"							

        mToEmpCompany = RsCompany.Fields("COMPANY_CODE").Value
        mToEmpCode = mCode

SearchRow:
        SqlStr = GetEmpTransferSQL(mToEmpCode, mToEmpCompany)

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            mFromEmpCompany = IIf(IsDBNull(RsTemp.Fields("FROM_COMPANY_CODE").Value), "", RsTemp.Fields("FROM_COMPANY_CODE").Value)
            mFromEmpCode = IIf(IsDBNull(RsTemp.Fields("FROM_EMP_CODE").Value), "", RsTemp.Fields("FROM_EMP_CODE").Value)

            If MainClass.ValidateWithMasterTable(mFromEmpCode, "EMP_CODE", "EMP_LEAVE_DATE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & mFromEmpCompany & "") = True Then
                mFromEmpLeaveDate = VB6.Format(Trim(MasterNo), "DD/MM/YYYY")
            End If

            SqlStr = " SELECT SUM(PFAMT+VPFAMT) AS PFAMT1 " & vbCrLf & " FROM PAY_PFESI_TRN WHERE " & vbCrLf & " Company_Code =" & mFromEmpCompany & " AND  " & vbCrLf _
                & " EMP_CODE = '" & mFromEmpCode & "' AND SAL_DATE>=TO_DATE('" & VB6.Format(RsCompany.Fields("START_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND SAL_DATE<=TO_DATE('" & VB6.Format(mFromEmpLeaveDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')  AND PFAMT>0"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAttn, ADODB.LockTypeEnum.adLockOptimistic)

            If RsAttn.EOF = False Then
                CalcPF = CalcPF + IIf(IsDBNull(RsAttn.Fields("PFAMT1").Value), 0, RsAttn.Fields("PFAMT1").Value)
            End If
            mToEmpCompany = mFromEmpCompany
            mToEmpCode = mFromEmpCode
            GoTo SearchRow
        End If


        ''''*****************VPF							


        '  SqlStr = " SELECT SUM(PAYABLEAMOUNT) AS PFAMT1" & vbCrLf _							
        ''            & " FROM PAY_SAL_TRN SALTRN, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf _							
        ''            & " WHERE " & vbCrLf _							
        ''            & " SALTRN.Company_Code = " & RsCompany!COMPANY_CODE & "" & vbCrLf _							
        ''            & " AND SALTRN.Company_Code = ADD_DEDUCT.Company_Code" & vbCrLf _							
        ''            & " AND SALTRN.SALHEADCODE = ADD_DEDUCT.CODE " & vbCrLf _							
        ''            & " AND EMP_CODE = '" & mCode & "'" & vbCrLf _							
        ''            & " AND ADD_DEDUCT.TYPE=" & ConVPFAllw & "" & vbCrLf _							
        ''            & " AND SAL_DATE>='" & vb6.Format(RsCompany!START_DATE, "DD-MMM-YYYY") & "'" & vbCrLf _							
        ''            & " AND SAL_DATE<='" & vb6.Format(txtDate, "DD-MMM-YYYY") & "'"							
        '							
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsAttn, adLockOptimistic							
        '							
        '    If RsAttn.EOF = False Then							
        '        CalcPF = CalcPF + IIf(IsNull(RsAttn!PFAMT1), 0, RsAttn!PFAMT1)							
        '    End If							
        '							
        '    SqlStr = " SELECT AMOUNT AS PFAMT FROM PAY_SalaryDef_MST SD, PAY_SALARYHEAD_MST SMST" & vbCrLf _							
        ''            & " WHERE SD.COMPANY_CODE=" & RsCompany!COMPANY_CODE & "" & vbCrLf _							
        ''            & " AND SD.COMPANY_CODE=SMST.COMPANY_CODE" & vbCrLf _							
        ''            & " AND SD.ADD_DEDUCTCODE=SMST.CODE " & vbCrLf _							
        ''            & " AND SMST.TYPE=" & ConVPFAllw & "" & vbCrLf _							
        ''            & " AND SD.EMP_CODE='" & mCode & "'" & vbCrLf _							
        ''            & " AND SD.SALARY_EFF_DATE=(SELECT MAX(SALARY_EFF_DATE) FROM PAY_SalaryDef_MST " & vbCrLf _							
        ''            & " WHERE COMPANY_CODE=" & RsCompany!COMPANY_CODE & "" & vbCrLf _							
        ''            & " AND EMP_CODE='" & mCode & "'" & vbCrLf _							
        ''            & " AND SALARY_EFF_DATE< TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "')) "							
        '							
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsAttn, adLockOptimistic							
        '    If RsAttn.EOF = False Then							
        ''        mBalMonth = DateDiff("M", RsAttn!SAL_DATE1, RsCompany!END_DATE)							
        '        CalcPF = CalcPF + (IIf(IsNull(RsAttn!PFAMT), 0, RsAttn!PFAMT) * mBalMonth)							
        '    End If							


        Exit Function
ErrPart:
        CalcPF = 0
    End Function
    Private Function CalcBonus(ByRef mCode As String, ByRef mBasicSalary As Double) As Double

        On Error GoTo ErrCalcBonus
        Dim mBonusPer As Double
        Dim mBonusAmount As Double
        Dim RsSal As ADODB.Recordset
        Dim RsTemp As ADODB.Recordset
        Dim RsTempPC As ADODB.Recordset
        Dim mFromDate As String
        Dim mToDate As String

        Dim mToEmpCompany As Integer
        Dim mToEmpCode As String
        Dim mFromEmpCompany As Integer
        Dim mFromEmpCode As String

        mFromDate = "01/04/" & Year(RsCompany.Fields("START_DATE").Value) - 1
        mToDate = "31/03/" & Year(RsCompany.Fields("START_DATE").Value)

        ''							

        '    If RsCompany!COMPANY_CODE = 2 Then							
        '        SqlStr = " SELECT DISTINCT PAYABLESALARY + CASE WHEN ISARREAR='N' THEN GETPayableBonusAmount (COMPANY_CODE, '" & mCode & "',SAL_DATE, 'N') ELSE 0 END AS PAYABLESALARY, "							
        '    Else							
        SqlStr = " SELECT DISTINCT PAYABLESALARY, "
        '    End If							

        SqlStr = SqlStr & vbCrLf & " SAL_DATE, " & vbCrLf & " ISARREAR " & vbCrLf & " FROM PAY_SAL_TRN SALTRN" & vbCrLf & " WHERE " & vbCrLf & " SALTRN.Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE = '" & mCode & "'" & vbCrLf _
            & " AND SAL_DATE>=TO_DATE('" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND SAL_DATE<=TO_DATE('" & VB6.Format(mToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSal, ADODB.LockTypeEnum.adLockOptimistic)

        If RsSal.EOF = False Then
            Do While Not RsSal.EOF
                mBonusAmount = mBonusAmount + IIf(IsDBNull(RsSal.Fields("PAYABLESALARY").Value), 0, RsSal.Fields("PAYABLESALARY").Value)
                RsSal.MoveNext()
            Loop
        Else

            mToEmpCompany = RsCompany.Fields("COMPANY_CODE").Value
            mToEmpCode = mCode

SearchRow:
            SqlStr = GetEmpTransferSQL(mToEmpCode, mToEmpCompany)
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempPC, ADODB.LockTypeEnum.adLockOptimistic)

            If RsTempPC.EOF = False Then

                mFromEmpCompany = IIf(IsDBNull(RsTempPC.Fields("FROM_COMPANY_CODE").Value), "", RsTempPC.Fields("FROM_COMPANY_CODE").Value)
                mFromEmpCode = IIf(IsDBNull(RsTempPC.Fields("FROM_EMP_CODE").Value), "", RsTempPC.Fields("FROM_EMP_CODE").Value)

                SqlStr = " SELECT DISTINCT PAYABLESALARY, " & vbCrLf & " SAL_DATE, " & vbCrLf & " ISARREAR " & vbCrLf & " FROM PAY_SAL_TRN SALTRN" & vbCrLf & " WHERE " & vbCrLf & " SALTRN.Company_Code = " & mFromEmpCompany & "" & vbCrLf & " AND EMP_CODE = '" & mFromEmpCode & "'" & vbCrLf _
                    & " AND SAL_DATE>=TO_DATE('" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " AND SAL_DATE<=TO_DATE('" & VB6.Format(mToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSal, ADODB.LockTypeEnum.adLockOptimistic)

                If RsSal.EOF = False Then
                    Do While Not RsSal.EOF
                        mBonusAmount = mBonusAmount + IIf(IsDBNull(RsSal.Fields("PAYABLESALARY").Value), 0, RsSal.Fields("PAYABLESALARY").Value)
                        RsSal.MoveNext()
                    Loop
                Else
                    mBonusAmount = 0
                End If
            Else
                mBonusAmount = 0
            End If
        End If

        If MainClass.ValidateWithMasterTable(mCode, "EMP_CODE", "BONUS_PER", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mBonusPer = Val(MasterNo)
        Else
            mBonusPer = 0
        End If

        CalcBonus = (mBonusAmount * mBonusPer) / 100
        CalcBonus = System.Math.Round(CalcBonus, 0)
        Exit Function
ErrCalcBonus:
        CalcBonus = 0
    End Function

    Private Function CalcTourAllw(ByRef mCode As String, ByRef mBasicSalary As Double) As Double

        On Error GoTo ErrCalcTourAllw
        Dim mTourAmount As Double
        Dim RsSal As ADODB.Recordset
        Dim mFromDate As String
        Dim mToDate As String
        Dim mTourPer As Double

        CalcTourAllw = 0
        If MainClass.ValidateWithMasterTable(mCode, "EMP_CODE", "EMP_CATG", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            If MasterNo <> "R" Then
                Exit Function
            End If
        End If

        If RsCompany.Fields("FYEAR").Value >= 2006 Then
            SqlStr = " SELECT SUM(AMOUNT) AS AMOUNT " & vbCrLf & " FROM PAY_TOUR_TRN " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & "  AND FYEAR = " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND EMP_CODE = '" & mCode & "'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSal, ADODB.LockTypeEnum.adLockOptimistic)

            If RsSal.EOF = False Then
                CalcTourAllw = IIf(IsDBNull(RsSal.Fields("Amount").Value), 0, RsSal.Fields("Amount").Value)
            End If
        Else
            If MainClass.ValidateWithMasterTable(mCode, "EMP_CODE", "SALARY_TYPE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                If MasterNo = "T" Then
                    Exit Function
                End If
            End If


            mFromDate = "01/02/" & Year(RsCompany.Fields("START_DATE").Value)
            mToDate = "31/01/" & Year(RsCompany.Fields("END_DATE").Value)

            SqlStr = " SELECT DISTINCT PAYABLESALARY, SAL_DATE, " & vbCrLf & " ISARREAR " & vbCrLf & " FROM PAY_SAL_TRN SALTRN" & vbCrLf & " WHERE " & vbCrLf & " SALTRN.Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE = '" & mCode & "'" & vbCrLf _
                & " AND SAL_DATE>=TO_DATE('" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " AND SAL_DATE<=TO_DATE('" & VB6.Format(mToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSal, ADODB.LockTypeEnum.adLockOptimistic)

            If RsSal.EOF = False Then
                Do While Not RsSal.EOF
                    mTourAmount = mTourAmount + IIf(IsDBNull(RsSal.Fields("PAYABLESALARY").Value), 0, RsSal.Fields("PAYABLESALARY").Value)
                    RsSal.MoveNext()
                Loop
            Else
                mTourAmount = mBasicSalary
            End If

            mTourPer = 8.33

            CalcTourAllw = (mTourAmount * mTourPer) / 100
        End If

        CalcTourAllw = System.Math.Round(CalcTourAllw, 0)

        Exit Function
ErrCalcTourAllw:
        CalcTourAllw = 0
    End Function
    Private Function CheckLastYearLTAPaid() As Boolean

        On Error GoTo ErrCalcTourAllw
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mCurrentLTAAmount As Double
        Dim mLastLTAAmount As Double
        CheckLastYearLTAPaid = False

        sprdIT.Row = 36
        sprdIT.Col = ColAmt4
        mCurrentLTAAmount = Val(sprdIT.Text)

        If mCurrentLTAAmount = 0 Then
            CheckLastYearLTAPaid = False
            Exit Function
        End If

        SqlStr = " SELECT SUM(AMOUNT4) AS AMOUNT " & vbCrLf & " FROM PAY_ITCOMP_TRN " & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR = " & RsCompany.Fields("FYEAR").Value - 1 & "" & vbCrLf & " AND EMP_CODE = '" & MainClass.AllowSingleQuote((txtEmpCode.Text)) & "'" & vbCrLf & " AND SUBROWNO=36"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            mLastLTAAmount = IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
        End If

        CheckLastYearLTAPaid = IIf(mLastLTAAmount = 0, False, True)

        Exit Function
ErrCalcTourAllw:
        CheckLastYearLTAPaid = False
    End Function
    Private Function GetLTAAmount(ByRef mCode As String, ByRef pPayableSalary As Double, ByRef mResetType As String) As Double

        On Error GoTo ErrGetLTAAmount
        Dim RsTemp As ADODB.Recordset
        Dim mFromDate As String
        Dim mBSalary As Double
        Dim mCat As String
        Dim mEmpCat As String
        Dim xDesgCode As String
        Dim mEmpDOJ As String
        Dim mLTAMonth As Integer
        Dim mBaseOn As String
        Dim mLTAPer As Double
        Dim mWLTAPer As Double
        Dim mLTAAmt As Double
        Dim mLTAFrom As String
        Dim mLTATo As String
        Dim mLTAPaidMonth As Integer

        mFromDate = RsCompany.Fields("START_DATE").Value
        GetLTAAmount = 0

        SqlStr = " SELECT NET_LTA_AMOUNT FROM PAY_LTA_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mCode) & "'"

        SqlStr = SqlStr & vbCrLf & " UNION " & vbCrLf & " SELECT NET_LTA_AMOUNT FROM PAY_LTA_ARREAR_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mCode) & "'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                GetLTAAmount = GetLTAAmount + CDbl(VB6.Format(System.Math.Round(IIf(IsDBNull(RsTemp.Fields("NET_LTA_AMOUNT").Value), 0, RsTemp.Fields("NET_LTA_AMOUNT").Value), 0), "0.00"))
                RsTemp.MoveNext()
            Loop
            Exit Function
        End If

        If mResetType = "A" Then Exit Function

        '    SqlStr = " SELECT BASICSALARY,EMP_DESG_CODE " & vbCrLf _							
        ''            & " FROM PAY_SALARYDEF_MST" & vbCrLf _							
        ''            & " WHERE COMPANY_CODE = " & RsCompany!COMPANY_CODE & "" & vbCrLf _							
        ''            & " AND EMP_CODE = '" & mCode & "'" & vbCrLf _							
        ''            & " AND SALARY_APP_DATE=(SELECT MAX(SALARY_APP_DATE) " & vbCrLf _							
        ''            & " FROM PAY_SALARYDEF_MST" & vbCrLf _							
        ''            & " WHERE COMPANY_CODE = " & RsCompany!COMPANY_CODE & "" & vbCrLf _							
        ''            & " AND EMP_CODE = '" & mCode & "'" & vbCrLf _							
        ''            & " AND SALARY_APP_DATE<='" & vb6.Format(mFromDate, "DD-MMM-YYYY") & "')"							

        SqlStr = " SELECT IH.BASICSALARY, IH.EMP_DESG_CODE, IH.TOT_ARR_MONTH, IH.SALARY_EFF_DATE,IH.PERCENTAGE, IH.AMOUNT" & vbCrLf & " FROM PAY_SALARYDEF_MST IH, PAY_SALARYHEAD_MST SMST" & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=SMST.COMPANY_CODE" & vbCrLf & " AND IH.ADD_DEDUCTCODE=SMST.CODE" & vbCrLf & " AND IH.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.EMP_CODE = '" & mCode & "'" & vbCrLf & " AND TYPE=" & ConLTA & "" & vbCrLf & " AND IH.SALARY_APP_DATE=(SELECT MAX(SALARY_APP_DATE) " & vbCrLf & " FROM PAY_SALARYDEF_MST" & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE = '" & mCode & "'" & vbCrLf _
            & " AND SALARY_APP_DATE<=TO_DATE('" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            mBSalary = IIf(IsDBNull(RsTemp.Fields("BASICSALARY").Value), 0, RsTemp.Fields("BASICSALARY").Value)
            xDesgCode = IIf(IsDBNull(RsTemp.Fields("EMP_DESG_CODE").Value), "", RsTemp.Fields("EMP_DESG_CODE").Value)

            '       SqlStr = " SELECT * " & vbCrLf _							
            ''            & " FROM PAY_LTA_MST " & vbCrLf _							
            ''            & " WHERE COMPANY_CODE = " & RsCompany!COMPANY_CODE & "" & vbCrLf _							
            ''            & " AND MINLIMIT<=" & Val(mBSalary) & " AND MAXLIMIT>=" & Val(mBSalary) & " " & vbCrLf _							
            ''            & " AND WEF_DATE=(SELECT MAX(WEF_DATE) " & vbCrLf _							
            ''            & " FROM PAY_LTA_MST " & vbCrLf _							
            ''            & " WHERE COMPANY_CODE = " & RsCompany!COMPANY_CODE & "" & vbCrLf _							
            ''            & " AND WEF_DATE<='" & vb6.Format(mFromDate, "DD-MMM-YYYY") & "')"							
            '							
            '        MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockOptimistic							
            '							
            '        If RsTemp.EOF = False Then							
            '            mBaseOn = IIf(IsNull(RsTemp!LTA_WORK_BASE_ON), "A", RsTemp!LTA_WORK_BASE_ON)							
            '            mLTAPer = IIf(IsNull(RsTemp!LTA_PER), 0, RsTemp!LTA_PER)							
            '            mWLTAPer = IIf(IsNull(RsTemp!LTA_WORK_PER), 0, RsTemp!LTA_WORK_PER)							
            '            mLTAAmt = IIf(IsNull(RsTemp!LTAAMT), 0, RsTemp!LTAAMT)							

            mLTAPer = IIf(IsDBNull(RsTemp.Fields("PERCENTAGE").Value), 0, RsTemp.Fields("PERCENTAGE").Value)
            mLTAAmt = IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
            If mLTAPer = 0 Then
                mBaseOn = "A"
            End If

            If MainClass.ValidateWithMasterTable(mCode, "EMP_CODE", "EMP_CATG", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mEmpCat = MasterNo
            End If

            If mEmpCat = "R" Then
                If mBaseOn = "A" Then
                    '                    GetLTAAmount = IIf(IsNull(RsTemp!LTA_WORK_AMT), 0, RsTemp!LTA_WORK_AMT)							
                    GetLTAAmount = CDbl(VB6.Format(mLTAAmt * 12, "0.00")) '' Format(mLTAAmt * Round(mLTAPaidMonth, 0) / 12, "0.00")							
                    '                    If mLTAPaidMonth - Round(mLTAPaidMonth, 0) > 0 Then							
                    '                        mLTAPaidMonth = Format(mLTAPaidMonth - Round(mLTAPaidMonth, 0), "0.00") * 100							
                    '                        GetLTCAmount = GetLTCAmount + Format(mLTAAmt * mLTAPaidMonth / MainClass.LastDay(Month(txtDOL.Text), Year(txtDOL.Text)), "0.00")							
                    '                    End If							
                Else
                    If MainClass.ValidateWithMasterTable(mCode, "EMP_CODE", "EMP_DOJ", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mEmpDOJ = MasterNo

                        If IsDate(mEmpDOJ) Then
                            mLTAMonth = Month(CDate(mEmpDOJ))
                        Else
                            mLTAMonth = Month(RsCompany.Fields("START_DATE").Value)
                        End If

                    End If

                    If RsCompany.Fields("COMPANY_CODE").Value = 2 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Then
                        mLTAFrom = VB6.Format("01/02/" & Year(RsCompany.Fields("START_DATE").Value), "DD/MM/YYYY")
                        mLTATo = VB6.Format("31/01/" & Year(RsCompany.Fields("END_DATE").Value), "DD/MM/YYYY")
                    Else
                        Select Case mLTAMonth
                            Case 1, 2, 3
                                mLTAFrom = VB6.Format("01/" & mLTAMonth & "/" & Year(RsCompany.Fields("START_DATE").Value), "DD/MM/YYYY")
                                If mLTAMonth = 1 Then
                                    mLTATo = VB6.Format("31/12/" & Year(RsCompany.Fields("START_DATE").Value), "DD/MM/YYYY")
                                Else
                                    mLTATo = MainClass.LastDay(Val(CStr(mLTAMonth - 1)), Year(RsCompany.Fields("END_DATE").Value)) & "/" & VB6.Format(mLTAMonth - 1 & "/" & Year(RsCompany.Fields("END_DATE").Value), "MM/YYYY")
                                End If
                            Case 4, 5, 6, 7, 8, 9, 10, 11, 12
                                mLTAFrom = VB6.Format("01/" & mLTAMonth & "/" & Year(RsCompany.Fields("START_DATE").Value) - 1, "DD/MM/YYYY")
                                mLTATo = MainClass.LastDay(Val(CStr(mLTAMonth - 1)), Year(RsCompany.Fields("START_DATE").Value)) & "/" & VB6.Format(mLTAMonth - 1 & "/" & Year(RsCompany.Fields("START_DATE").Value), "MM/YYYY")
                        End Select
                    End If

                    SqlStr = " SELECT DISTINCT SAL_DATE,ISARREAR, PAYABLESALARY AS BASICSALARY1" & vbCrLf _
                        & " FROM PAY_SAL_TRN SALTRN" & vbCrLf & " WHERE " & vbCrLf & " SALTRN.Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE = '" & mCode & "'" & vbCrLf _
                        & " AND SAL_DATE>=TO_DATE('" & VB6.Format(mLTAFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                        & " AND SAL_DATE<=TO_DATE('" & VB6.Format(mLTATo, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

                    If RsTemp.EOF = False Then
                        pPayableSalary = 0
                        Do While Not RsTemp.EOF
                            pPayableSalary = pPayableSalary + IIf(IsDBNull(RsTemp.Fields("BASICSALARY1").Value), 0, RsTemp.Fields("BASICSALARY1").Value)
                            If RsTemp.Fields("IsArrear").Value = "N" Then
                                mLTAPaidMonth = mLTAPaidMonth + 1
                            End If
                            RsTemp.MoveNext()
                        Loop
                    End If
                    pPayableSalary = pPayableSalary + (mBSalary * (12 - mLTAPaidMonth))
                    GetLTAAmount = pPayableSalary * mLTAPer * 0.01
                End If
            Else
                If MainClass.ValidateWithMasterTable(xDesgCode, "DESG_CODE", "DESG_CAT", "PAY_DESG_MST", PubDBCn, MasterNo,  , "COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mCat = MasterNo
                End If

                If mCat = "M" Or mCat = "D" Then ''mBSalary							

                    If MainClass.ValidateWithMasterTable(mCode, "EMP_CODE", "EMP_DOJ", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mEmpDOJ = MasterNo

                        If IsDate(mEmpDOJ) Then
                            mLTAMonth = Month(CDate(mEmpDOJ))
                        Else
                            mLTAMonth = Month(RsCompany.Fields("START_DATE").Value)
                        End If

                    End If

                    Select Case mLTAMonth
                        Case 1, 2, 3
                            mLTAFrom = VB6.Format("01/" & mLTAMonth & "/" & Year(RsCompany.Fields("START_DATE").Value), "DD/MM/YYYY")
                            If mLTAMonth = 1 Then
                                mLTATo = VB6.Format("31/12/" & Year(RsCompany.Fields("START_DATE").Value), "DD/MM/YYYY")
                            Else
                                mLTATo = MainClass.LastDay(Val(CStr(mLTAMonth - 1)), Year(RsCompany.Fields("END_DATE").Value)) & "/" & VB6.Format(mLTAMonth - 1 & "/" & Year(RsCompany.Fields("END_DATE").Value), "MM/YYYY")
                            End If
                        Case 4, 5, 6, 7, 8, 9, 10, 11, 12
                            mLTAFrom = VB6.Format("01/" & mLTAMonth & "/" & Year(RsCompany.Fields("START_DATE").Value) - 1, "DD/MM/YYYY")
                            mLTATo = MainClass.LastDay(Val(CStr(mLTAMonth - 1)), Year(RsCompany.Fields("START_DATE").Value)) & "/" & VB6.Format(mLTAMonth - 1 & "/" & Year(RsCompany.Fields("START_DATE").Value), "MM/YYYY")
                    End Select

                    '                    If mLTAMonth < Month(RsCompany!START_DATE) Then							
                    '                        mLTAFrom = Format("01/" & mLTAMonth & "/" & Year(RsCompany!START_DATE), "DD/MM/YYYY")							
                    '                        mLTATo = Format("01/" & mLTAMonth & "/" & Year(RsCompany!START_DATE) + 1, "DD/MM/YYYY")							
                    '                    Else							
                    '                        mLTAFrom = Format("01/" & mLTAMonth & "/" & Year(RsCompany!START_DATE), "DD/MM/YYYY")							
                    '                        mLTATo = Format("01/" & mLTAMonth & "/" & Year(RsCompany!START_DATE) + 1, "DD/MM/YYYY")							
                    '                    End If							

                    SqlStr = " SELECT DISTINCT SAL_DATE,ISARREAR, PAYABLESALARY AS BASICSALARY1" & vbCrLf & " FROM PAY_SAL_TRN SALTRN" & vbCrLf & " WHERE " & vbCrLf & " SALTRN.Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE = '" & mCode & "'" & vbCrLf _
                        & " AND SAL_DATE>=TO_DATE('" & VB6.Format(mLTAFrom, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                        & " AND SAL_DATE<=TO_DATE('" & VB6.Format(mLTATo, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

                    If RsTemp.EOF = False Then
                        pPayableSalary = 0
                        Do While Not RsTemp.EOF
                            pPayableSalary = pPayableSalary + IIf(IsDBNull(RsTemp.Fields("BASICSALARY1").Value), 0, RsTemp.Fields("BASICSALARY1").Value)
                            If RsTemp.Fields("IsArrear").Value = "N" Then
                                mLTAPaidMonth = mLTAPaidMonth + 1
                            End If
                            RsTemp.MoveNext()
                        Loop
                    End If
                    pPayableSalary = pPayableSalary + (mBSalary * (12 - mLTAPaidMonth))
                    GetLTAAmount = pPayableSalary * mLTAPer * 0.01
                ElseIf mCat = "S" Then
                    GetLTAAmount = mLTAAmt * 12
                End If
            End If
            '        Else							
            '            GetLTAAmount = 0							
            '        End If							
        Else
            GetLTAAmount = 0
        End If


        Exit Function
ErrGetLTAAmount:
        GetLTAAmount = 0
    End Function
    Private Function GetMedicalPer(ByRef mCode As String) As Double
        On Error GoTo ErrGetMedicalPer
        Dim RsTemp As ADODB.Recordset
        Dim mFromDate As String
        Dim mBSalary As Double
        Dim mCat As String
        Dim mEmpCat As String
        Dim xDesgCode As String

        mFromDate = (txtDate.Text)
        '							
        '    SqlStr = " SELECT BASICSALARY,EMP_DESG_CODE " & vbCrLf _							
        ''            & " FROM PAY_SALARYDEF_MST" & vbCrLf _							
        ''            & " WHERE COMPANY_CODE = " & RsCompany!COMPANY_CODE & "" & vbCrLf _							
        ''            & " AND EMP_CODE = '" & mCode & "'" & vbCrLf _							
        ''            & " AND SALARY_APP_DATE=(SELECT MAX(SALARY_APP_DATE) " & vbCrLf _							
        ''            & " FROM PAY_SALARYDEF_MST" & vbCrLf _							
        ''            & " WHERE COMPANY_CODE = " & RsCompany!COMPANY_CODE & "" & vbCrLf _							
        ''            & " AND EMP_CODE = '" & mCode & "'" & vbCrLf _							
        ''            & " AND SALARY_APP_DATE<='" & vb6.Format(mFromDate, "DD-MMM-YYYY") & "')"							
        '							
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockOptimistic							
        '							
        '    If RsTemp.EOF = False Then							
        '       mBSalary = IIf(IsNull(RsTemp!BASICSALARY), 0, RsTemp!BASICSALARY)							
        '       xDesgCode = IIf(IsNull(RsTemp!EMP_DESG_CODE), "", RsTemp!EMP_DESG_CODE)							
        '							
        '       SqlStr = " SELECT * " & vbCrLf _							
        ''            & " FROM PAY_LTA_MST " & vbCrLf _							
        ''            & " WHERE COMPANY_CODE = " & RsCompany!COMPANY_CODE & "" & vbCrLf _							
        ''            & " AND MINLIMIT<=" & Val(mBSalary) & " AND MAXLIMIT>=" & Val(mBSalary) & " " & vbCrLf _							
        ''            & " AND WEF_DATE=(SELECT MAX(WEF_DATE) " & vbCrLf _							
        ''            & " FROM PAY_LTA_MST " & vbCrLf _							
        ''            & " WHERE COMPANY_CODE = " & RsCompany!COMPANY_CODE & "" & vbCrLf _							
        ''            & " AND WEF_DATE<='" & vb6.Format(mFromDate, "DD-MMM-YYYY") & "')"							
        '							
        '        MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockOptimistic							
        '							
        '        If RsTemp.EOF = False Then							
        '            If MainClass.ValidateWithMasterTable(mCode, "EMP_CODE", "EMP_CATG", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE = " & RsCompany!COMPANY_CODE & "") = True Then							
        '                mEmpCat = MasterNo							
        '            End If							
        '							
        '            If mEmpCat = "R" Then							
        '                GetMedicalPer = IIf(IsNull(RsTemp!LTA_WORK_AMT), 0, RsTemp!LTA_WORK_AMT)							
        '            Else							
        '                If MainClass.ValidateWithMasterTable(xDesgCode, "DESG_CODE", "DESG_CAT", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE = " & RsCompany!COMPANY_CODE & "") = True Then							
        '                    mCat = MasterNo							
        '                End If							
        '							
        '                If mCat = "M" Or mCat = "D" Then							
        '                    GetMedicalPer = mBSalary * IIf(IsNull(RsTemp!LTA_PER), 0, RsTemp!LTA_PER) * 0.01 * 12							
        '                ElseIf mCat = "S" Then							
        '                    GetMedicalPer = IIf(IsNull(RsTemp!LTAAMT), 0, RsTemp!LTAAMT)							
        '                End If							
        '            End If							
        '        Else							
        '            GetMedicalPer = 0							
        '        End If							
        '    Else							
        '        GetMedicalPer = 0							
        '    End If							


        Exit Function
ErrGetMedicalPer:
        GetMedicalPer = 0
    End Function

    Private Sub CellFormat()

        Dim cntRow As Integer
        With sprdIT
            .Row = 1
            .Row2 = .MaxRows
            .Col = ColAmt1
            .Col2 = .MaxCols
            .BlockMode = True
            .CellType = SS_CELL_TYPE_STATIC_TEXT
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0C0C0)
            .BlockMode = False

            For cntRow = RowGrossSalary To RowGrossSalary + 23
                .Row = cntRow

                .Col = ColAmt1
                .CellType = SS_CELL_TYPE_FLOAT

                .Col = ColAmt3
                .CellType = SS_CELL_TYPE_FLOAT
                .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)

                .Col = ColTotal
                .CellType = SS_CELL_TYPE_FLOAT
                .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF) ' &HFFFFFF							

                If cntRow <= RowGrossSalary + 18 Then '''+19 ''SK Dated : 07-11-05							
                    MainClass.ProtectCell(sprdIT, cntRow, cntRow, ColDesc, ColDesc)
                Else
                    If (RsCompany.Fields("COMPANY_CODE").Value = 2 Or RsCompany.Fields("COMPANY_CODE").Value = 12) And cntRow = 20 Then
                        MainClass.ProtectCell(sprdIT, cntRow, cntRow, ColDesc, ColDesc)
                    Else
                        If cntRow = 22 Then
                            MainClass.ProtectCell(sprdIT, cntRow, cntRow, ColDesc, ColDesc)
                        Else
                            MainClass.UnProtectCell(sprdIT, cntRow, cntRow, ColDesc, ColDesc)
                        End If
                    End If
                End If
                MainClass.ProtectCell(sprdIT, cntRow, cntRow, ColAmt1, ColAmt1)
                MainClass.ProtectCell(sprdIT, cntRow, cntRow, ColTotal, ColTotal)
            Next

            .Row = RowGrossAmount
            .Col = ColTotal
            .CellType = SS_CELL_TYPE_FLOAT
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF) ' &HFFFFFF							
            MainClass.ProtectCell(sprdIT, .Row, .Row, ColDesc, ColDesc)
            MainClass.ProtectCell(sprdIT, .Row, .Row, ColTotal, ColTotal)

            For cntRow = RowExemptSalary To RowExemptSalary + 10
                .Row = cntRow
                MainClass.ProtectCell(sprdIT, .Row, .Row, ColDesc, ColDesc)
            Next

            .Row = RowExemptSalary + 2
            .Col = ColAmt1
            .CellType = SS_CELL_TYPE_FLOAT
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)

            .Col = ColAmt2
            .CellType = SS_CELL_TYPE_FLOAT
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)

            .Col = ColAmt3
            .CellType = SS_CELL_TYPE_FLOAT
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)

            MainClass.ProtectCell(sprdIT, .Row, .Row, ColAmt3, ColAmt3)

            For cntRow = RowExemptSalary + 3 To RowExemptSalary + 6
                .Row = cntRow
                .Col = ColAmt3
                .CellType = SS_CELL_TYPE_FLOAT
                .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF) ' &HFFFFFF							
                MainClass.ProtectCell(sprdIT, cntRow, cntRow, ColAmt3, ColAmt3)
            Next

            .Row = RowExemptSalary + 7
            .Col = ColAmt4
            .CellType = SS_CELL_TYPE_FLOAT
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF) ' &HFFFFFF							
            MainClass.ProtectCell(sprdIT, .Row, .Row, ColDesc, ColDesc)
            MainClass.ProtectCell(sprdIT, .Row, .Row, ColTotal, ColTotal)

            For cntRow = RowExemptSalary + 8 To RowExemptSalary + 11 + 3
                .Row = cntRow
                .Col = ColAmt4
                .CellType = SS_CELL_TYPE_FLOAT
                .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
                MainClass.UnProtectCell(sprdIT, cntRow, cntRow, ColAmt4, ColAmt4)
                MainClass.ProtectCell(sprdIT, cntRow, cntRow, ColDesc, ColAmt3)
                MainClass.ProtectCell(sprdIT, cntRow, cntRow, ColTotal, ColTotal)
            Next

            For cntRow = RowExemptSalary + 12 + 3 To RowExemptSalary + 12 + 3
                .Row = cntRow
                .Col = ColAmt4
                .CellType = SS_CELL_TYPE_FLOAT
                .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
                MainClass.UnProtectCell(sprdIT, cntRow, cntRow, ColDesc, ColTotal)
                MainClass.ProtectCell(sprdIT, cntRow, cntRow, ColAmt1, ColAmt3)
                MainClass.ProtectCell(sprdIT, cntRow, cntRow, ColTotal, ColTotal)

                .Col = ColTotal
                .CellType = SS_CELL_TYPE_FLOAT
                .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF) ' &HFFFFFF							
                MainClass.ProtectCell(sprdIT, .Row, .Row, ColDesc, ColDesc)
                MainClass.ProtectCell(sprdIT, .Row, .Row, ColTotal, ColTotal)
            Next


            For cntRow = RowExemptSalary + 13 + 3 To RowExemptSalary + 13 + 3
                .Row = cntRow
                .Col = ColTotal
                .CellType = SS_CELL_TYPE_FLOAT
                .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
                MainClass.UnProtectCell(sprdIT, cntRow, cntRow, ColTotal, ColTotal)
                MainClass.ProtectCell(sprdIT, cntRow, cntRow, ColDesc, ColAmt4)
                '            MainClass.ProtectCell sprdIT, cntRow, cntRow, ColTotal, ColTotal							
            Next

            For cntRow = RowExemptSalary + 13 To RowExemptSalary + 13
                .Row = cntRow
                .Col = ColDesc
                MainClass.UnProtectCell(sprdIT, cntRow, cntRow, ColDesc, ColDesc)
            Next

            For cntRow = RowExemptSalary + 15 To RowExemptSalary + 15
                .Row = cntRow
                .Col = ColDesc
                MainClass.UnProtectCell(sprdIT, cntRow, cntRow, ColDesc, ColDesc)
            Next


            '        cntRow = RowExemptSalary + 11							
            '        .Row = cntRow							
            '        .Col = ColAmt4							
            '        .CellType = SS_CELL_TYPE_FLOAT							
            '        .BackColor = &HFFFFFF							
            '							
            '        .Col = ColTotal							
            '        .CellType = SS_CELL_TYPE_FLOAT							
            '        .BackColor = &HC0FFFF     ' &HFFFFFF							
            '        MainClass.UnProtectCell sprdIT, cntRow, cntRow, ColDesc, ColTotal							
            '        MainClass.ProtectCell sprdIT, cntRow, cntRow, ColAmt1, ColAmt3							
            '        MainClass.ProtectCell sprdIT, cntRow, cntRow, ColTotal, ColTotal							
            '							
            For cntRow = RowTaxableSalaryBeforeSD To RowTaxableSalaryBeforeSD
                .Row = cntRow
                .Col = ColTotal
                .CellType = SS_CELL_TYPE_FLOAT
                .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF) ' &HFFFFFF							
                MainClass.ProtectCell(sprdIT, cntRow, cntRow, ColDesc, ColTotal)
            Next

            For cntRow = RowStandardDedection To RowStandardDedection
                .Row = cntRow
                .Col = ColAmt1
                .CellType = SS_CELL_TYPE_FLOAT
                .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
                MainClass.UnProtectCell(sprdIT, cntRow, cntRow, ColDesc, ColTotal)
                '            MainClass.ProtectCell sprdIT, cntRow, cntRow, ColTotal, ColTotal							
                MainClass.ProtectCell(sprdIT, cntRow, cntRow, ColAmt2, ColTotal)
            Next

            For cntRow = RowTaxableSalary To RowTaxableSalary
                .Row = cntRow
                .Col = ColTotal
                .CellType = SS_CELL_TYPE_FLOAT
                .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF) ' &HFFFFFF							
                MainClass.ProtectCell(sprdIT, cntRow, cntRow, ColDesc, ColTotal)
            Next


            .Row = RowIncomeOS
            MainClass.ProtectCell(sprdIT, RowIncomeOS, RowIncomeOS, ColDesc, ColDesc)

            For cntRow = RowIncomeOS + 1 To RowIncomeOS + 3
                .Row = cntRow
                .Col = ColAmt1
                .CellType = SS_CELL_TYPE_FLOAT
                .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
                MainClass.UnProtectCell(sprdIT, cntRow, cntRow, ColDesc, ColTotal)
                '            MainClass.ProtectCell sprdIT, cntRow, cntRow, ColTotal, ColTotal							
                MainClass.ProtectCell(sprdIT, cntRow, cntRow, ColAmt2, ColTotal)
            Next

            For cntRow = RowTotalIncomeOS To RowTotalIncome
                .Row = cntRow
                .Col = ColTotal
                .CellType = SS_CELL_TYPE_FLOAT
                .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF) ' &HFFFFFF							
                MainClass.ProtectCell(sprdIT, cntRow, cntRow, ColDesc, ColTotal)
            Next


            .Row = RowSection6A
            MainClass.ProtectCell(sprdIT, RowSection6A, RowSection6A, ColDesc, ColDesc)

            For cntRow = RowExempt80D To RowExempt80CCF
                .Row = cntRow
                .Col = ColAmt1
                .CellType = SS_CELL_TYPE_FLOAT
                .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)

                .Col = ColAmt2
                .CellType = SS_CELL_TYPE_FLOAT
                .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)

                MainClass.UnProtectCell(sprdIT, cntRow, cntRow, ColDesc, ColTotal)
                MainClass.ProtectCell(sprdIT, cntRow, cntRow, ColAmt3, ColTotal)
            Next

            For cntRow = RowExempt80C + 1 To RowExempt80C + 9
                .Row = cntRow
                .Col = ColAmt1
                .CellType = SS_CELL_TYPE_FLOAT
                .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)

                .Col = ColAmt2
                .CellType = SS_CELL_TYPE_FLOAT
                .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)

                MainClass.UnProtectCell(sprdIT, cntRow, cntRow, ColDesc, ColTotal)
                MainClass.ProtectCell(sprdIT, cntRow, cntRow, ColAmt3, ColTotal)
            Next

            For cntRow = RowTotalExempt80C To RowTotalExempt80C
                .Row = cntRow
                .Col = ColAmt1
                .CellType = SS_CELL_TYPE_FLOAT
                .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF) ' &HFFFFFF							

                .Col = ColAmt2
                .CellType = SS_CELL_TYPE_FLOAT
                .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF) ' &HFFFFFF							

                MainClass.ProtectCell(sprdIT, cntRow, cntRow, ColDesc, ColTotal)
            Next

            For cntRow = RowTotalSection6A To RowTotalSection6A
                .Row = cntRow
                .Col = ColTotal
                .CellType = SS_CELL_TYPE_FLOAT
                .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF) ' &HFFFFFF							

                MainClass.UnProtectCell(sprdIT, cntRow, cntRow, ColDesc, ColTotal)
                '            MainClass.UnProtectCell sprdIT, cntRow, cntRow, ColAmt4, ColAmt4							
                MainClass.ProtectCell(sprdIT, cntRow, cntRow, ColAmt2, ColTotal)
                '            MainClass.ProtectCell sprdIT, cntRow, cntRow, ColTotal, ColTotal							
            Next

            For cntRow = RowTaxableIncome To RowTaxableIncome
                .Row = cntRow
                .Col = ColTotal
                .CellType = SS_CELL_TYPE_FLOAT
                .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF) ' &HFFFFFF							
                MainClass.ProtectCell(sprdIT, cntRow, cntRow, ColDesc, ColTotal)
            Next

            .Row = RowTaxSlab
            MainClass.ProtectCell(sprdIT, RowTaxSlab, RowTaxSlab, ColDesc, ColDesc)

            .Row = RowTaxSlab + 1
            MainClass.ProtectCell(sprdIT, RowTaxSlab + 1, RowTaxSlab + 1, ColDesc, ColDesc)

            For cntRow = RowTaxSlab + 2 To RowTotalTaxSlab
                .Row = cntRow
                .Col = ColAmt1
                .CellType = SS_CELL_TYPE_FLOAT
                .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF) ' &HFFFFFF							

                .Col = ColAmt2
                .CellType = SS_CELL_TYPE_FLOAT
                .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF) ' &HFFFFFF							

                .Col = ColAmt3
                .CellType = SS_CELL_TYPE_FLOAT
                .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF) ' &HFFFFFF							

                If cntRow = RowTotalTaxSlab Then
                    .Col = ColTotal
                    .CellType = SS_CELL_TYPE_FLOAT
                    .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF) ' &HFFFFFF							
                End If

                MainClass.ProtectCell(sprdIT, cntRow, cntRow, ColDesc, ColTotal)
            Next

            .Row = RowExempt80D
            MainClass.ProtectCell(sprdIT, RowExempt80D, RowExempt80D, ColDesc, ColDesc)

            '        For cntRow = RowExempt88 + 1 To RowTotalExempt88 - 1							
            '            .Row = cntRow							
            '            .Col = ColAmt1							
            '            .CellType = SS_CELL_TYPE_FLOAT							
            '            .BackColor = &HFFFFFF							
            '							
            '            .Col = ColAmt2							
            '            .CellType = SS_CELL_TYPE_FLOAT							
            '            .BackColor = &HFFFFFF							
            '							
            ''            .Col = ColAmt3							
            ''            .CellType = SS_CELL_TYPE_FLOAT							
            ''            .BackColor = &HFFFFFF							
            ''							
            '            MainClass.UnProtectCell sprdIT, cntRow, cntRow, ColDesc, ColTotal							
            ''            MainClass.UnProtectCell sprdIT, cntRow, cntRow, ColAmt4, ColAmt4							
            '            MainClass.ProtectCell sprdIT, cntRow, cntRow, ColTotal, ColTotal							
            ''            MainClass.ProtectCell sprdIT, cntRow, cntRow, ColTotal, ColTotal							
            '        Next							

            '        .Row = RowExempt88 + 1							
            '        MainClass.ProtectCell sprdIT, RowExempt88 + 1, RowExempt88 + 1, ColDesc, ColDesc							

            '        .Row = RowTotalExempt88							
            '        .Col = ColAmt1							
            '        .CellType = SS_CELL_TYPE_FLOAT							
            '        .BackColor = &HC0FFFF     ' &HFFFFFF							
            '							
            '        .Col = ColAmt2							
            '        .CellType = SS_CELL_TYPE_FLOAT							
            '        .BackColor = &HC0FFFF     ' &HFFFFFF							
            '							
            ''        .Col = ColAmt3							
            ''        .CellType = SS_CELL_TYPE_FLOAT							
            ''        .BackColor = &HC0FFFF     ' &HFFFFFF							
            '							
            '        MainClass.ProtectCell sprdIT, RowTotalExempt88, RowTotalExempt88, ColDesc, ColTotal							
            '							
            '        .Row = RowCalcEmempt88							
            '        .Col = ColAmt1							
            '        .CellType = SS_CELL_TYPE_FLOAT							
            '        .BackColor = &HC0FFFF     ' &HFFFFFF							
            '							
            '        .Col = ColTotal							
            '        .CellType = SS_CELL_TYPE_FLOAT							
            '        .BackColor = &HC0FFFF     ' &HFFFFFF							
            '							
            '        MainClass.ProtectCell sprdIT, RowCalcEmempt88, RowCalcEmempt88, ColDesc, ColTotal							

            For cntRow = RowSurcharge To RowTaxableAmount
                .Row = cntRow
                .Col = ColTotal
                .CellType = SS_CELL_TYPE_FLOAT
                .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF) ' &HFFFFFF							

                MainClass.ProtectCell(sprdIT, cntRow, cntRow, ColDesc, ColTotal)
            Next


            If RsCompany.Fields("FYEAR").Value >= 2014 Then
                .Row = RowCessableAmount
                .Col = ColAmt1
                .CellType = SS_CELL_TYPE_FLOAT
                .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
                MainClass.UnProtectCell(sprdIT, RowCessableAmount, RowCessableAmount, ColDesc, ColTotal)
                MainClass.ProtectCell(sprdIT, RowCessableAmount, RowCessableAmount, ColDesc, ColDesc)
                MainClass.ProtectCell(sprdIT, RowCessableAmount, RowCessableAmount, ColAmt2, ColTotal)
            Else
                .Row = RowTaxableAmount
                .Col = ColAmt1
                .CellType = SS_CELL_TYPE_FLOAT
                .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
                MainClass.UnProtectCell(sprdIT, RowTaxableAmount, RowTaxableAmount, ColDesc, ColTotal)
                MainClass.ProtectCell(sprdIT, RowTaxableAmount, RowTaxableAmount, ColDesc, ColDesc)
                MainClass.ProtectCell(sprdIT, RowTaxableAmount, RowTaxableAmount, ColAmt2, ColTotal)
            End If


            .Row = RowPrepaidAmount
            .Col = ColAmt1
            .CellType = SS_CELL_TYPE_FLOAT
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)

            .Col = ColTotal
            .CellType = SS_CELL_TYPE_FLOAT
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF) ' &HFFFFFF							

            MainClass.UnProtectCell(sprdIT, RowPrepaidAmount, RowPrepaidAmount, ColDesc, ColTotal)
            MainClass.ProtectCell(sprdIT, RowPrepaidAmount, RowPrepaidAmount, ColDesc, ColDesc)
            MainClass.ProtectCell(sprdIT, RowPrepaidAmount, RowPrepaidAmount, ColAmt2, ColTotal)

            .Row = RowBalanceAmount
            .Col = ColTotal
            .CellType = SS_CELL_TYPE_FLOAT
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF) ' &HFFFFFF							

            MainClass.ProtectCell(sprdIT, RowBalanceAmount, RowBalanceAmount, ColDesc, ColTotal)

            .Row = RowNetPerMonth
            .Col = ColAmt2
            .CellType = SS_CELL_TYPE_FLOAT
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)

            .Col = ColTotal
            .CellType = SS_CELL_TYPE_FLOAT
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF) ' &HFFFFFF							

            MainClass.UnProtectCell(sprdIT, RowNetPerMonth, RowNetPerMonth, ColDesc, ColTotal)
            MainClass.ProtectCell(sprdIT, RowNetPerMonth, RowNetPerMonth, ColDesc, ColAmt1)
            MainClass.ProtectCell(sprdIT, RowNetPerMonth, RowNetPerMonth, ColAmt3, ColTotal)


            .Row = RowThisMonth
            .Col = ColTotal
            .CellType = SS_CELL_TYPE_FLOAT
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)

            MainClass.UnProtectCell(sprdIT, RowThisMonth, RowThisMonth, ColDesc, ColTotal)
            MainClass.ProtectCell(sprdIT, RowThisMonth, RowThisMonth, ColDesc, ColAmt4)
        End With
    End Sub
    Private Sub FillTaxSlabs(ByRef mRow As Integer)

        Dim SqlStr As String
        Dim RsITRate As ADODB.Recordset
        Dim cntRow As Integer
        Dim mSqlStr As String
        Dim mSex As String
        Dim mDOB As String
        Dim mCheckDate As String
        Dim mAge As Double

        mSex = "M"

        If MainClass.ValidateWithMasterTable((txtEmpCode.Text), "EMP_CODE", "EMP_SEX", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSex = MasterNo
        End If

        If MainClass.ValidateWithMasterTable((txtEmpCode.Text), "EMP_CODE", "EMP_DOB", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDOB = MasterNo
        End If

        mAge = 0
        mCheckDate = RsCompany.Fields("START_DATE").Value

        If Trim(txtDate.Text) <> "" Then
            If IsDate(txtDate.Text) = True Then
                mCheckDate = VB6.Format(txtDate.Text, "DD/MM/YYYY")
            End If
        End If

        If Trim(mDOB) <> "" Then
            If IsDate(mDOB) = True Then
                mAge = DateDiff(Microsoft.VisualBasic.DateInterval.Month, CDate(mDOB), CDate(VB6.Format(mCheckDate, "DD/MM/YYYY"))) / 12
            End If
        End If

        If mAge > 60 Then
            mSex = "S"
        End If

        SqlStr = " SELECT *  FROM PAY_ITRATE_MST WHERE" & vbCrLf _
            & " Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf _
            & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND ITTYPE='IT' " & vbCrLf _
            & " AND SEX='" & mSex & "' AND TAX_REGIME='" & mTaxRegime & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsITRate, ADODB.LockTypeEnum.adLockOptimistic)

        If RsITRate.EOF = False Then
            sprdIT.Row = mRow
            Do While Not RsITRate.EOF
                RowTotalTaxSlab = sprdIT.Row + 1
                sprdIT.Row = sprdIT.Row + 1
                sprdIT.Col = ColDesc
                sprdIT.Text = IIf(IsDBNull(RsITRate.Fields("MINLIMIT").Value), "", RsITRate.Fields("MINLIMIT").Value) & " - " & IIf(IsDBNull(RsITRate.Fields("MAXLIMIT").Value), "", RsITRate.Fields("MAXLIMIT").Value)

                sprdIT.Col = ColAmt2
                sprdIT.Text = VB6.Format(IIf(IsDBNull(RsITRate.Fields("TAXPER").Value), 0, RsITRate.Fields("TAXPER").Value) / 100, "0.00")

                RsITRate.MoveNext()
            Loop
        Else
            RowTotalTaxSlab = mRow
        End If

    End Sub

    Private Function GetSurchargeRate(ByRef mAmount As Double) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsITRate As ADODB.Recordset

        SqlStr = " SELECT *  FROM PAY_ITRATE_MST WHERE " & vbCrLf & " Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND ITTYPE='SR' AND " & vbCrLf & " MaxLimit = (" & vbCrLf & " SELECT MIN(MaxLimit)  FROM PAY_ITRATE_MST WHERE " & vbCrLf & " Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND ITTYPE='SR' AND MaxLimit >=" & mAmount & ")"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsITRate, ADODB.LockTypeEnum.adLockOptimistic)

        If RsITRate.EOF = False Then
            GetSurchargeRate = IIf(IsDBNull(RsITRate.Fields("TAXPER").Value), 0, RsITRate.Fields("TAXPER").Value)
        End If

        Exit Function
ErrPart:
        GetSurchargeRate = 0
    End Function

    Private Function GetStandardDedection(ByRef mAmount As Double) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsITRate As ADODB.Recordset

        SqlStr = " SELECT SURCHARGE  FROM PAY_ITRATE_MST WHERE " & vbCrLf & " Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND ITTYPE='SD'" & vbCrLf & " AND MINLIMIT<=" & mAmount & "" & vbCrLf & " AND MAXLIMIT>=" & mAmount & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsITRate, ADODB.LockTypeEnum.adLockOptimistic)

        If RsITRate.EOF = False Then
            GetStandardDedection = IIf(IsDBNull(RsITRate.Fields("SURCHARGE").Value), 0, RsITRate.Fields("SURCHARGE").Value)
        End If

        Exit Function
ErrPart:
        GetStandardDedection = 0
    End Function



    Private Sub ResetScreen(ByRef mResetType As String)
        On Error GoTo ErrPart

        Dim mActualSalary As Double
        Dim mActualHRA As Double
        Dim mActualConvAll As Double
        Dim mActualCEWAllowance As Double
        Dim mEstimateSalary As Double
        Dim mEstimateHRA As Double
        Dim mEstimateConvAll As Double
        Dim mEstimateCEWAllowance As Double
        Dim mLTA As Double

        Dim mMedicalReimburement As Double

        Dim mActualDA As Double
        Dim mActualVDA As Double
        Dim mActualOthers As Double
        Dim mActualIncentive As Double

        Dim mActualAttnAllw As Double
        Dim mActualTourAllw As Double
        Dim mActualMedicalAllw As Double
        Dim mActualMilkAllw As Double
        Dim mActualAwardAllw As Double
        Dim mActualGiftAllw As Double
        Dim mEstimatDA As Double
        Dim mEstimatVDA As Double
        Dim mEstimatOthers As Double
        Dim mEstimatIncentive As Double
        Dim mEstimatAttnAllw As Double
        Dim mEstimatTourAllw As Double
        Dim mEstimatMedicalAllw As Double
        Dim mEstimatMilkAllw As Double
        Dim mEstimatAwardAllw As Double
        Dim mEstimatGiftAllw As Double

        Dim mMedicalPer As Double
        Dim mLeaveEncash As Double
        Dim mOT As Double
        Dim mBonus As Double
        Dim mPF As Double
        Dim mConvAll As Double
        Dim mBalMonth As Double
        Dim mPaidIT As Double
        Dim mLeaveDate As String
        Dim mArrear As Double
        Dim mArrearSal As Double
        Dim mCurrDate As String
        Dim mEstimatWashAllw As Double
        Dim mActualWashAllw As Double

        Dim mActualCCAAllw As Double
        Dim mEstimatCCAAllw As Double
        Dim mActualSPAllw As Double
        Dim mEstimatSPAllw As Double
        Dim mActualTRANSAllw As Double
        Dim mEstimatTRANSAllw As Double
        Dim mActualEXGRATIAAllw As Double
        Dim mEstimatEXGRATIAAllw As Double
        Dim mISFF_IN_CY As Boolean
        Dim mGratuity As Double
        Dim mNotice As Double

        Dim mInaam As Double
        Dim mLockDownAmt As Double
        '            If MainClass.ValidateWithMasterTable(txtEmpCode.Text, "EMP_CODE", "EMP_LEAVE_DATE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany!COMPANY_CODE & "") = True Then							
        '                mLeaveDate = Trim(IIf(IsNull(MasterNo), "", MasterNo))							
        '                If IsDate(mLeaveDate) Then							
        '                    Exit Sub							
        '                End If							
        '            End If							

        If MainClass.ValidateWithMasterTable((txtEmpCode.Text), "EMP_CODE", "EMP_LEAVE_DATE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND (EMP_LEAVE_DATE IS NOT NULL OR EMP_LEAVE_DATE<>'')") = True Then
            mLeaveDate = MasterNo
            If CDate(mLeaveDate) >= CDate(RsCompany.Fields("START_DATE").Value) And CDate(mLeaveDate) <= CDate(RsCompany.Fields("END_DATE").Value) Then
                mISFF_IN_CY = True
            Else
                mISFF_IN_CY = False
            End If
        Else
            mISFF_IN_CY = False
        End If

        If mResetType = "S" Then
            mCurrDate = GetMaxSalMadeDate()
            If mCurrDate <> "" Then
                txtDate.Text = mCurrDate
            End If
        End If

        Call CalcSalary((txtEmpCode.Text), mActualSalary, mActualHRA, mActualConvAll, mActualCEWAllowance, mEstimateSalary, mEstimateHRA, mEstimateConvAll, mEstimateCEWAllowance, mActualDA, mActualVDA, mActualOthers, mActualIncentive, mActualAttnAllw, mActualTourAllw, mActualMedicalAllw, mActualMilkAllw, mActualAwardAllw, mActualGiftAllw, mActualWashAllw, mEstimatDA, mEstimatVDA, mEstimatOthers, mEstimatIncentive, mEstimatAttnAllw, mEstimatTourAllw, mEstimatMedicalAllw, mEstimatMilkAllw, mEstimatAwardAllw, mEstimatGiftAllw, mEstimatWashAllw, mActualCCAAllw, mEstimatCCAAllw, mActualSPAllw, mEstimatSPAllw, mActualTRANSAllw, mEstimatTRANSAllw, mActualEXGRATIAAllw, mEstimatEXGRATIAAllw, mInaam, mLockDownAmt)

        With sprdIT
            .Row = RowGrossSalary
            .Col = ColAmt1
            .Text = VB6.Format(mActualSalary, "0.00")

            .Col = ColAmt3
            .Text = VB6.Format(mEstimateSalary, "0.00")

            .Col = ColTotal
            .Text = VB6.Format(mActualSalary + mEstimateSalary, "0.00")

            .Row = RowGrossSalary + 1
            .Col = ColAmt1
            .Text = VB6.Format(mActualHRA, "0.00")

            .Col = ColAmt3
            .Text = VB6.Format(mEstimateHRA, "0.00")

            .Col = ColTotal
            .Text = VB6.Format(mActualHRA + mEstimateHRA, "0.00")

            .Row = RowGrossSalary + 2
            .Col = ColAmt1
            .Text = VB6.Format(mActualConvAll + mActualTRANSAllw, "0.00")

            .Col = ColAmt3
            .Text = VB6.Format(mEstimateConvAll + mEstimatTRANSAllw, "0.00")

            .Col = ColTotal
            mConvAll = mActualConvAll + mEstimateConvAll + mActualTRANSAllw + mEstimatTRANSAllw
            .Text = VB6.Format(mConvAll, "0.00")


            .Row = RowGrossSalary + 3
            .Col = ColAmt1
            .Text = VB6.Format(mActualCEWAllowance, "0.00")

            .Col = ColAmt3
            .Text = VB6.Format(mEstimateCEWAllowance, "0.00")

            .Col = ColTotal
            .Text = VB6.Format(mActualCEWAllowance + mEstimateCEWAllowance, "0.00")

            '                .Col = ColAmt1							
            '                mBonus = CalcBonus(txtEmpCode.Text, mActualSalary)							
            '                .Text = mBonus							
            mArrearSal = 0
            mArrear = CalcArrearSalary((txtEmpCode.Text), mArrearSal)


            .Row = RowGrossSalary + 4
            .Col = ColAmt3
            If mISFF_IN_CY = True Then
                mBonus = GetFromFF((txtEmpCode.Text), "BONUS_FORYEAR")
                If mBonus = 0 Then
                    If mResetType = "A" Then
                        mBonus = CalcBonus((txtEmpCode.Text), mActualSalary + mEstimateSalary + mArrearSal)
                    End If
                End If
                mBonus = mBonus + GetFromFF((txtEmpCode.Text), "BONUS_CURRYEAR")
            Else
                If mResetType = "A" Then
                    mBonus = CalcBonus((txtEmpCode.Text), mActualSalary + mEstimateSalary + mArrearSal)
                Else
                    mBonus = Val(.Text)
                End If
            End If
            .Text = CStr(mBonus)


            .Row = RowGrossSalary + 5
            ''Mannual ''Kamal... 21-05-2009							
            '                If mISFF_IN_CY = True Then							
            '                    mLTA = GetFromFF(txtEmpCode.Text, "LTC_AMOUNT")							
            '                Else							
            If mResetType = "A" Or mResetType = "S" Then
                mLTA = GetLTAAmount((txtEmpCode.Text), Val(CStr(mActualSalary + mEstimateSalary)), mResetType)
                If mLTA = 0 Then
                    mLTA = Val(.Text)
                End If
            Else

                .Col = ColAmt3
                mLTA = Val(.Text)
            End If
            '                End If							

            .Col = ColAmt3
            .Text = CStr(mLTA)

            .Row = RowGrossSalary + 6
            mMedicalReimburement = GetMedicalReimburement((txtEmpCode.Text))

            .Col = ColAmt3
            .Text = VB6.Format(mMedicalReimburement, "0.00")

            .Col = ColTotal
            .Text = VB6.Format(mMedicalReimburement, "0.00")

            .Row = RowGrossSalary + 7
            .Col = ColAmt3
            If mISFF_IN_CY = True Then
                mLeaveEncash = GetFromFF((txtEmpCode.Text), "EL_AMOUNT")
                If mResetType = "A" Then
                    mLeaveEncash = mLeaveEncash + CalcLeaveEncash((txtEmpCode.Text))
                End If
            Else
                If mResetType = "A" Then
                    mLeaveEncash = CalcLeaveEncash((txtEmpCode.Text))
                Else
                    mLeaveEncash = Val(.Text)
                End If
            End If
            .Text = CStr(mLeaveEncash)


            .Row = RowGrossSalary + 8
            .Col = ColAmt3
            mOT = CalcOT((txtEmpCode.Text))
            .Text = CStr(mOT)

            .Row = RowGrossSalary + 9
            .Col = ColAmt3
            .Text = CStr(mArrear)


            If RsCompany.Fields("COMPANY_CODE").Value = 2 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Then
                .Row = RowGrossSalary + 10
                .Col = ColAmt1
                .Text = VB6.Format(mActualDA, "0.00")

                .Col = ColAmt3
                .Text = VB6.Format(mEstimatDA, "0.00")

                .Col = ColTotal
                .Text = VB6.Format(mActualDA + mEstimatDA, "0.00")
                '''********							
                .Row = RowGrossSalary + 11
                .Col = ColAmt1
                .Text = VB6.Format(mActualVDA, "0.00")

                .Col = ColAmt3
                .Text = VB6.Format(mEstimatVDA, "0.00")

                .Col = ColTotal
                .Text = VB6.Format(mActualVDA + mEstimatVDA, "0.00")

                .Row = RowGrossSalary + 12
                .Col = ColAmt1
                .Text = VB6.Format(mActualOthers + mInaam + mActualSPAllw, "0.00")

                .Col = ColAmt3
                .Text = VB6.Format(mEstimatOthers + mEstimatSPAllw, "0.00")

                .Col = ColTotal
                .Text = VB6.Format(mActualOthers + mEstimatOthers + mInaam + mActualSPAllw + mEstimatSPAllw, "0.00")


                .Row = RowGrossSalary + 13
                .Col = ColAmt1
                .Text = VB6.Format(mActualIncentive, "0.00")

                .Col = ColAmt3
                .Text = VB6.Format(mEstimatIncentive, "0.00")

                .Col = ColTotal
                .Text = VB6.Format(mActualIncentive + mEstimatIncentive, "0.00")

                .Row = RowGrossSalary + 14
                .Col = ColAmt1
                .Text = VB6.Format(mActualAttnAllw, "0.00")

                .Col = ColAmt3
                .Text = VB6.Format(mEstimatAttnAllw, "0.00")

                .Col = ColTotal
                .Text = VB6.Format(mActualAttnAllw + mEstimatAttnAllw, "0.00")

                .Row = RowGrossSalary + 15
                .Col = ColAmt1
                .Text = "" ''Format(mActualTourAllw, "0.00")							

                .Col = ColAmt3
                '                    .Text = Format(mEstimatTourAllw, "0.00")							
                mActualTourAllw = CalcTourAllw((txtEmpCode.Text), mActualSalary + mEstimateSalary + mArrearSal)
                .Text = VB6.Format(mActualTourAllw, "0.00")

                .Row = RowGrossSalary + 16
                .Col = ColAmt1

                If RsCompany.Fields("FYEAR").Value < 2020 Then
                    .Text = VB6.Format(mActualMilkAllw, "0.00")
                Else
                    .Text = VB6.Format(0, "0.00")
                End If

                .Col = ColAmt3
                If RsCompany.Fields("FYEAR").Value < 2020 Then
                    .Text = VB6.Format(mEstimatMilkAllw, "0.00")
                Else
                    If mResetType = "S" Then ''mResetType = "A" Or							
                        .Text = VB6.Format(mLockDownAmt, "0.00")
                    End If
                End If

                .Col = ColTotal

                If RsCompany.Fields("FYEAR").Value < 2020 Then
                    .Text = VB6.Format(mActualMilkAllw + mEstimatMilkAllw, "0.00")
                Else
                    If mResetType = "S" Then
                        .Text = VB6.Format(mLockDownAmt, "0.00")
                    End If
                End If

                .Row = RowGrossSalary + 17
                .Col = ColAmt1
                .Text = VB6.Format(mActualAwardAllw, "0.00")

                .Col = ColAmt3
                .Text = VB6.Format(mEstimatAwardAllw, "0.00")

                .Col = ColTotal
                .Text = VB6.Format(mActualAwardAllw + mEstimatAwardAllw, "0.00")

                .Row = RowGrossSalary + 18
                .Col = ColAmt1
                .Text = VB6.Format(mActualGiftAllw, "0.00")

                .Col = ColAmt3
                .Text = VB6.Format(mEstimatGiftAllw, "0.00")

                .Col = ColTotal
                .Text = VB6.Format(mActualGiftAllw + mEstimatGiftAllw, "0.00")

            ElseIf RsCompany.Fields("COMPANY_CODE").Value = 11 Then
                .Row = RowGrossSalary + 15
                .Col = ColAmt1
                .Text = VB6.Format(mActualCCAAllw, "0.00")

                .Col = ColAmt3
                .Text = VB6.Format(mEstimatCCAAllw, "0.00")

                .Col = ColTotal
                .Text = VB6.Format(mActualCCAAllw + mEstimatCCAAllw, "0.00")

                .Row = RowGrossSalary + 16
                .Col = ColAmt1
                .Text = VB6.Format(mActualSPAllw, "0.00")

                .Col = ColAmt3
                .Text = VB6.Format(mEstimatSPAllw, "0.00")

                .Col = ColTotal
                .Text = VB6.Format(mActualSPAllw + mEstimatSPAllw, "0.00")

                .Row = RowGrossSalary + 17
                .Col = ColAmt1
                .Text = "0.00" ''Format(mActualTRANSAllw, "0.00")							

                .Col = ColAmt3
                .Text = "0.00" ''Format(mEstimatTRANSAllw, "0.00")							

                .Col = ColTotal
                .Text = "0.00" ''Format(mActualTRANSAllw + mEstimatTRANSAllw, "0.00")							

                .Row = RowGrossSalary + 18
                .Col = ColAmt1
                .Text = VB6.Format(mActualEXGRATIAAllw, "0.00")

                .Col = ColAmt3
                .Text = VB6.Format(mEstimatEXGRATIAAllw, "0.00")

                .Col = ColTotal
                .Text = VB6.Format(mActualEXGRATIAAllw + mEstimatEXGRATIAAllw, "0.00")
            Else
                .Row = RowGrossSalary + 10
                .Col = ColAmt1
                .Text = VB6.Format(mActualOthers, "0.00")

                .Col = ColAmt3
                .Text = VB6.Format(mEstimatOthers, "0.00")

                .Col = ColTotal
                .Text = VB6.Format(mActualOthers + mEstimatOthers, "0.00")

                If RsCompany.Fields("COMPANY_CODE").Value = 16 Then
                    .Row = RowGrossSalary + 17
                    .Col = ColAmt1
                    .Text = VB6.Format(mActualEXGRATIAAllw, "0.00")

                    .Col = ColAmt3
                    .Text = VB6.Format(mEstimatEXGRATIAAllw, "0.00")

                    .Col = ColTotal
                    .Text = VB6.Format(mActualEXGRATIAAllw + mEstimatEXGRATIAAllw, "0.00")

                    .Row = RowGrossSalary + 18
                    .Col = ColAmt1
                    .Text = VB6.Format(mActualSPAllw, "0.00")

                    .Col = ColAmt3
                    .Text = VB6.Format(mEstimatSPAllw, "0.00")

                    .Col = ColTotal
                    .Text = VB6.Format(mActualSPAllw + mEstimatSPAllw, "0.00")
                Else
                    .Row = RowGrossSalary + 16
                    .Col = ColAmt1
                    .Text = VB6.Format(mActualAttnAllw, "0.00")

                    .Col = ColAmt3
                    .Text = VB6.Format(mEstimatAttnAllw, "0.00")

                    .Col = ColTotal
                    .Text = VB6.Format(mActualAttnAllw + mEstimatAttnAllw, "0.00")

                    .Row = RowGrossSalary + 17
                    .Col = ColAmt1
                    If RsCompany.Fields("FYEAR").Value < 2020 Then
                        .Text = VB6.Format(mInaam, "0.00")
                    Else
                        .Text = VB6.Format(0, "0.00")
                    End If

                    .Col = ColAmt3

                    If RsCompany.Fields("FYEAR").Value < 2020 Then
                        .Text = VB6.Format(0, "0.00")
                    Else
                        If mResetType = "S" Then ''mResetType = "A" Or							
                            .Text = VB6.Format(mLockDownAmt, "0.00")
                        End If
                    End If

                    .Col = ColTotal
                    If RsCompany.Fields("FYEAR").Value < 2020 Then
                        .Text = VB6.Format(mInaam, "0.00")
                    Else
                        If mResetType = "S" Then
                            .Text = VB6.Format(mLockDownAmt, "0.00")
                        End If
                    End If

                    .Row = RowGrossSalary + 18
                    .Col = ColAmt1
                    .Text = VB6.Format(mActualSPAllw, "0.00")

                    .Col = ColAmt3
                    .Text = VB6.Format(mEstimatSPAllw, "0.00")

                    .Col = ColTotal
                    .Text = VB6.Format(mActualSPAllw + mEstimatSPAllw, "0.00")
                End If
            End If


            .Row = RowGrossSalary + 19
            .Col = ColAmt1
            .Text = VB6.Format(mActualWashAllw, "0.00")

            .Col = ColAmt3
            .Text = VB6.Format(mEstimatWashAllw, "0.00")

            .Col = ColTotal
            .Text = VB6.Format(mActualWashAllw + mEstimatWashAllw, "0.00")


            .Row = RowGrossSalary + 21
            .Col = ColAmt1
            .Text = VB6.Format(mActualMedicalAllw, "0.00")

            .Col = ColAmt3
            .Text = VB6.Format(mEstimatMedicalAllw, "0.00")

            .Col = ColTotal
            .Text = VB6.Format(mActualMedicalAllw + mEstimatMedicalAllw, "0.00")

            '''*************							


            .Row = RowGrossSalary + 22
            .Col = ColAmt3
            If mISFF_IN_CY = True Then
                mGratuity = GetFromFF((txtEmpCode.Text), "GRATUITY_AMOUNT")
            End If
            .Text = CStr(mGratuity)

            .Row = RowGrossSalary + 23
            .Col = ColAmt3
            If mISFF_IN_CY = True Then
                mNotice = GetFromFF((txtEmpCode.Text), "NOTICE_AMOUNT")
                mNotice = mNotice + GetFromFF((txtEmpCode.Text), "OTHERS_AMOUNT")
            End If
            .Text = IIf(mNotice = 0, .Text, mNotice)

            .Row = RowExempt80C + 1
            .Col = ColAmt1
            mPF = CalcPF((txtEmpCode.Text))
            .Text = VB6.Format(mPF, "0.00")

            .Col = ColAmt2
            .Text = VB6.Format(mPF, "0.00")
            '							
            .Row = RowPrepaidAmount
            .Col = ColAmt1
            mPaidIT = CalcPaidIT((txtEmpCode.Text))
            .Text = VB6.Format(mPaidIT, "0.00")


            .Row = RowNetPerMonth
            .Col = ColAmt2
            mBalMonth = DateDiff(Microsoft.VisualBasic.DateInterval.Month, CDate(txtDate.Text), RsCompany.Fields("END_DATE").Value)
            If VB.Day(CDate(txtDate.Text)) <> MainClass.LastDay(Month(CDate(txtDate.Text)), Year(CDate(txtDate.Text))) Then
                mBalMonth = mBalMonth + 1
            End If
            .Text = VB6.Format(mBalMonth, "0.00")

        End With

        '        CalcGridTotal							
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Function GetMaxSalMadeDate() As String

        On Error GoTo ErrPart

        Dim RsTemp As ADODB.Recordset
        Dim SqlStr As String

        GetMaxSalMadeDate = ""


        ''FYEAR=" & RsCompany!FYEAR & "							

        SqlStr = "Select MAX(SAL_DATE) as SAL_DATE From PAY_SAL_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND  SAL_DATE>=TO_DATE('" & VB6.Format(RsCompany.Fields("START_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND SAL_DATE<=TO_DATE('" & VB6.Format(RsCompany.Fields("END_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND ISARREAR IN ('N','V','F')"

        SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & txtEmpCode.Text & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)
        If RsTemp.EOF = False Then
            GetMaxSalMadeDate = IIf(IsDBNull(RsTemp.Fields("SAL_DATE").Value), "", RsTemp.Fields("SAL_DATE").Value)
        End If

        Exit Function

ErrPart:
        GetMaxSalMadeDate = ""
    End Function
    Private Function GetConveyanceAllw(ByRef xConveyanceAllw As Double) As Double

        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim xDOJ As String
        Dim xDOL As String
        Dim mMonth As Integer
        Dim mDays1 As Double
        Dim mTempConv As Double
        Dim mVoucherConv As Double
        Dim mLastSalMade As String
        Dim mLastUnitConveyance As Double
        Dim mPrevLastSalMade As String
        Dim mCurrLastSalMade As String

        Dim mConvAmountLimit As Double

        If RsCompany.Fields("FYEAR").Value >= 2018 Then
            mConvAmountLimit = 0
            GetConveyanceAllw = 0
            Exit Function
        ElseIf RsCompany.Fields("FYEAR").Value >= 2015 Then
            mConvAmountLimit = 1600
        Else
            mConvAmountLimit = 800
        End If

        GetConveyanceAllw = 0
        '    LastDateofMon = MainClass.LastDay(mMonth, Year(txtDate.Text)) & "/" & Month(txtDate.Text) & "/" & Year(txtDate.Text)							
        '    mDOJ = MainClass.LastDay(mMonth, Year(txtDate.Text)) & "/" & mMonth & "/" & Year(txtDate.Text)							
        '    mDOL = "01" & "/" & mMonth & "/" & Year(txtDate.Text)							

        mSqlStr = " SELECT EMP_DOJ, EMP_LEAVE_DATE " & vbCrLf & " FROM PAY_EMPLOYEE_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote((txtEmpCode.Text)) & "'"

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)
        If RsTemp.EOF = False Then
            xDOJ = IIf(IsDBNull(RsTemp.Fields("EMP_DOJ").Value), "", RsTemp.Fields("EMP_DOJ").Value)
            xDOL = IIf(IsDBNull(RsTemp.Fields("EMP_LEAVE_DATE").Value), "", RsTemp.Fields("EMP_LEAVE_DATE").Value)
            If xDOL <> "" Then
                xDOL = IIf(CDate(RsCompany.Fields("END_DATE").Value) < CDate(xDOL), "", xDOL)
            End If
        End If

        xDOJ = IIf(CDate(xDOJ) < CDate(RsCompany.Fields("START_DATE").Value), RsCompany.Fields("START_DATE").Value, xDOJ)

        If xDOL = "" Then
            mMonth = DateDiff(Microsoft.VisualBasic.DateInterval.Month, CDate(txtDate.Text), RsCompany.Fields("END_DATE").Value)
        Else
            If VB6.Format(CDate(xDOL), "MM/YYYY") = VB6.Format(CDate(txtDate.Text), "MM/YYYY") Then
                mMonth = 0
            Else
                mMonth = DateDiff(Microsoft.VisualBasic.DateInterval.Month, CDate(xDOL), CDate(txtDate.Text)) + 1
            End If

            mDays1 = DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(txtDate.Text), CDate(xDOL))
            mDays1 = mDays1 / MainClass.LastDay(Month(CDate(xDOL)), Year(CDate(xDOL)))
        End If

        '',ISARREAR							
        SqlStr = " SELECT SUM(PayableAmount) AS AMOUNT1," & vbCrLf & " TO_CHAR(SAL_DATE,'MM/YYYY') AS SAL_DATE" & vbCrLf _
            & " FROM PAY_SAL_TRN SALTRN, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf & " WHERE " & vbCrLf & " SALTRN.Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SALTRN.Company_Code = ADD_DEDUCT.Company_Code(+)" & vbCrLf & " AND SALTRN.SALHEADCODE = ADD_DEDUCT.CODE(+) " & vbCrLf & " AND EMP_CODE = '" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "'" & vbCrLf & " AND ADDDEDUCT=" & ConEarning & " AND TYPE=" & ConConveyance & "" & vbCrLf _
            & " AND SAL_DATE>=TO_DATE('" & VB6.Format(RsCompany.Fields("START_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND SAL_DATE<=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND (ISARREAR='N' OR ISARREAR='V')" & vbCrLf _
            & " GROUP BY TO_CHAR(SAL_DATE,'MM/YYYY')" '',ISARREAR "							

        ''& " AND SALTRN.FYEAR= " & RsCompany!FYEAR & "" & vbCrLf _							
        '							
        '    SqlStr = SqlStr & vbCrLf & " ORDER BY TO_CHAR(SAL_DATE,'YYYYMM')"							
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        mPrevLastSalMade = ""
        If RsTemp.EOF = False Then
            Do While Not RsTemp.EOF
                '                mLastSalMade = RsTemp!SAL_DATE							
                mCurrLastSalMade = "01/" & RsTemp.Fields("SAL_DATE").Value
                mCurrLastSalMade = MainClass.LastDay(Month(CDate(mCurrLastSalMade)), Year(CDate(mCurrLastSalMade))) & "/" & VB6.Format(mCurrLastSalMade, "MM/YYYY")

                If mLastSalMade = "" Then
                    mLastSalMade = mCurrLastSalMade
                ElseIf CDate(mCurrLastSalMade) > CDate(mLastSalMade) Then
                    mLastSalMade = mCurrLastSalMade
                End If
                '                If RsTemp!IsArrear = "N" Or RsTemp!IsArrear = "V" Then							
                If RsTemp.Fields("AMOUNT1").Value < mConvAmountLimit Then
                    '                        mVoucherConv = GetConveyanceAllwFromVoucher(txtEmpCode.Text, RsTemp!SAL_DATE, RsCompany!COMPANY_CODE)							
                    If (RsTemp.Fields("AMOUNT1").Value + mVoucherConv) < mConvAmountLimit Then
                        GetConveyanceAllw = GetConveyanceAllw + RsTemp.Fields("AMOUNT1").Value + mVoucherConv
                    Else
                        GetConveyanceAllw = GetConveyanceAllw + mConvAmountLimit
                    End If
                Else
                    GetConveyanceAllw = GetConveyanceAllw + mConvAmountLimit
                End If
                mTempConv = RsTemp.Fields("AMOUNT1").Value
                '                End If							
                '                mPrevLastSalMade = mCurrLastSalMade							
                RsTemp.MoveNext()
            Loop
        End If

        If mLastSalMade = "" Then
            mVoucherConv = GetConveyanceAllwFromVoucher((txtEmpCode.Text), "", RsCompany.Fields("COMPANY_CODE").Value)
            If mVoucherConv < mConvAmountLimit Then
                GetConveyanceAllw = GetConveyanceAllw + mVoucherConv
            Else
                GetConveyanceAllw = GetConveyanceAllw + mConvAmountLimit
            End If
        ElseIf CDate(mLastSalMade) < CDate(txtDate.Text) Then
            mVoucherConv = GetConveyanceAllwFromVoucher((txtEmpCode.Text), (txtDate.Text), RsCompany.Fields("COMPANY_CODE").Value)
            If mVoucherConv < mConvAmountLimit Then
                GetConveyanceAllw = GetConveyanceAllw + mVoucherConv
            Else
                GetConveyanceAllw = GetConveyanceAllw + mConvAmountLimit
            End If
        End If

        If mMonth > 0 Then
            If mTempConv < mConvAmountLimit Then
                GetConveyanceAllw = GetConveyanceAllw + (mMonth * mTempConv)
            Else
                GetConveyanceAllw = GetConveyanceAllw + (mMonth * mConvAmountLimit)
            End If
        End If


        SqlStr = " SELECT MAX(SALARY_APP_DATE), AMOUNT " & vbCrLf & " FROM PAY_SALARYDEF_MST SALTRN, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf & " WHERE " & vbCrLf & " SALTRN.Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SALTRN.FYEAR= " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND SALTRN.Company_Code = ADD_DEDUCT.Company_Code(+)" & vbCrLf & " AND SALTRN.ADD_DEDUCTCode = ADD_DEDUCT.CODE(+) " & vbCrLf & " AND EMP_CODE = '" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "'" & vbCrLf & " AND ADDDEDUCT=" & ConEarning & " AND TYPE=" & ConConveyance & " GROUP BY AMOUNT"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            If mDays1 > 0 Then
                GetConveyanceAllw = GetConveyanceAllw + CDbl(VB6.Format(mDays1 * RsTemp.Fields("Amount").Value, "0.00"))
            End If
        End If

        mLastUnitConveyance = GetLastConveyanceAllw()
        GetConveyanceAllw = GetConveyanceAllw + mLastUnitConveyance
        Exit Function
ErrPart:
        GetConveyanceAllw = 0
    End Function

    Private Function GetLastConveyanceAllw() As Double

        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim xDOJ As String
        Dim xDOL As String
        Dim mMonth As Integer
        Dim mDays1 As Double
        Dim mTempConv As Double
        Dim mVoucherConv As Double
        Dim mLastSalMade As String
        Dim mLastUnitConveyance As Double
        Dim mFromEmpCode As String
        Dim mFromCompanyCode As Integer

        Dim mArrearConv As Double
        Dim mMonthCount As Integer
        Dim mToEmpCompany As Integer
        Dim mToEmpCode As String
        Dim mCurrLastSalMade As String
        Dim mConvAmountLimit As Double

        If RsCompany.Fields("FYEAR").Value >= 2015 Then
            mConvAmountLimit = 1600
        Else
            mConvAmountLimit = 800
        End If

        GetLastConveyanceAllw = 0

        '    SqlStr = " SELECT * " & vbCrLf _							
        ''            & " FROM PAY_EMP_TRF_MST" & vbCrLf _							
        ''            & " WHERE " & vbCrLf _							
        ''            & " TO_COMPANY_CODE = " & RsCompany!COMPANY_CODE & "" & vbCrLf _							
        ''            & " AND TO_EMP_CODE = '" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "'"							

        mToEmpCompany = RsCompany.Fields("COMPANY_CODE").Value
        mToEmpCode = MainClass.AllowSingleQuote(txtEmpCode.Text)

SearchRow:
        SqlStr = GetEmpTransferSQL(mToEmpCode, mToEmpCompany)
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            mFromCompanyCode = IIf(IsDBNull(RsTemp.Fields("FROM_COMPANY_CODE").Value), "", RsTemp.Fields("FROM_COMPANY_CODE").Value)
            mFromEmpCode = IIf(IsDBNull(RsTemp.Fields("FROM_EMP_CODE").Value), "", RsTemp.Fields("FROM_EMP_CODE").Value)
        Else
            Exit Function
        End If

        mSqlStr = " SELECT EMP_DOJ, EMP_LEAVE_DATE " & vbCrLf & " FROM PAY_EMPLOYEE_MST" & vbCrLf & " WHERE COMPANY_CODE=" & mFromCompanyCode & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mFromEmpCode) & "'"

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)
        If RsTemp.EOF = False Then
            xDOJ = IIf(IsDBNull(RsTemp.Fields("EMP_DOJ").Value), "", RsTemp.Fields("EMP_DOJ").Value)
            xDOL = IIf(IsDBNull(RsTemp.Fields("EMP_LEAVE_DATE").Value), "", RsTemp.Fields("EMP_LEAVE_DATE").Value)
            '        If xDOL <> "" Then							
            '            xDOL = IIf(CDate(RsCompany!END_DATE) < CDate(xDOL), "", xDOL)							
            '        End If							
        End If

        xDOJ = IIf(CDate(xDOJ) < CDate(RsCompany.Fields("START_DATE").Value), RsCompany.Fields("START_DATE").Value, xDOJ)

        If xDOL = "" Then
            mMonth = DateDiff(Microsoft.VisualBasic.DateInterval.Month, CDate(txtDate.Text), RsCompany.Fields("END_DATE").Value)
        Else
            If VB6.Format(CDate(xDOL), "YYYYMM") <= VB6.Format(CDate(txtDate.Text), "YYYYMM") Then
                mMonth = 0
            Else
                mMonth = DateDiff(Microsoft.VisualBasic.DateInterval.Month, CDate(xDOL), CDate(txtDate.Text)) + 1
            End If

            '        mDays1 = DateDiff("D", CDate(txtDate.Text), CDate(xDOL))							
            '        mDays1 = mDays1 / MainClass.LastDay(Month(xDOL), Year(xDOL))							
        End If

        '',ISARREAR							
        SqlStr = " SELECT SUM(PayableAmount) AS AMOUNT1," & vbCrLf _
            & " TO_CHAR(SAL_DATE,'MM/YYYY') AS SAL_DATE" & vbCrLf _
            & " FROM PAY_SAL_TRN SALTRN, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf _
            & " WHERE " & vbCrLf _
            & " SALTRN.Company_Code = " & mFromCompanyCode & "" & vbCrLf _
            & " AND SALTRN.Company_Code = ADD_DEDUCT.Company_Code(+)" & vbCrLf _
            & " AND SALTRN.SALHEADCODE = ADD_DEDUCT.CODE(+) " & vbCrLf _
            & " AND EMP_CODE = '" & MainClass.AllowSingleQuote(mFromEmpCode) & "'" & vbCrLf _
            & " AND ADDDEDUCT=" & ConEarning & " AND TYPE=" & ConConveyance & "" & vbCrLf _
            & " AND SAL_DATE>=TO_DATE('" & VB6.Format(RsCompany.Fields("START_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND SAL_DATE<=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND (ISARREAR='N' OR ISARREAR='V')" & vbCrLf _
            & " " '',ISARREAR "    ''							

        ''& " AND SALTRN.FYEAR= " & RsCompany!FYEAR & "" & vbCrLf _							
        '							
        If IsDate(xDOL) Then
            SqlStr = SqlStr & vbCrLf & " AND SAL_DATE<=TO_DATE('" & VB6.Format(xDOL, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        End If

        SqlStr = SqlStr & vbCrLf & " GROUP BY TO_CHAR(SAL_DATE,'MM/YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            Do While Not RsTemp.EOF
                '                mLastSalMade = "01/" & RsTemp!SAL_DATE							
                '                mLastSalMade = MainClass.LastDay(Month(CVDate(mLastSalMade)), Year(CVDate(mLastSalMade))) & "/" & vb6.Format(mLastSalMade, "MM/YYYY")							
                '							
                mCurrLastSalMade = "01/" & RsTemp.Fields("SAL_DATE").Value
                mCurrLastSalMade = MainClass.LastDay(Month(CDate(mCurrLastSalMade)), Year(CDate(mCurrLastSalMade))) & "/" & VB6.Format(mCurrLastSalMade, "MM/YYYY")

                If mLastSalMade = "" Then
                    mLastSalMade = mCurrLastSalMade
                ElseIf CDate(mCurrLastSalMade) > CDate(mLastSalMade) Then
                    mLastSalMade = mCurrLastSalMade
                End If

                If RsTemp.Fields("AMOUNT1").Value < mConvAmountLimit Then
                    mVoucherConv = GetConveyanceAllwFromVoucher(mFromEmpCode, RsTemp.Fields("SAL_DATE").Value, mFromCompanyCode)
                    If (RsTemp.Fields("AMOUNT1").Value + mVoucherConv) < mConvAmountLimit Then
                        GetLastConveyanceAllw = GetLastConveyanceAllw + RsTemp.Fields("AMOUNT1").Value + mVoucherConv
                    Else
                        GetLastConveyanceAllw = GetLastConveyanceAllw + mConvAmountLimit
                    End If
                Else
                    GetLastConveyanceAllw = GetLastConveyanceAllw + mConvAmountLimit
                End If
                mMonthCount = mMonthCount + 1
                mTempConv = RsTemp.Fields("AMOUNT1").Value

                RsTemp.MoveNext()
            Loop
        End If


        ''Check Arrear							
        '    SqlStr = " SELECT SUM(PayableAmount) AS AMOUNT1," & vbCrLf _							
        ''            & " TO_CHAR(SAL_DATE,'MM/YYYY') AS SAL_DATE" & vbCrLf _							
        ''            & " FROM PAY_SAL_TRN SALTRN, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf _							
        ''            & " WHERE " & vbCrLf _							
        ''            & " SALTRN.Company_Code = " & mFromCompanyCode & "" & vbCrLf _							
        ''            & " AND SALTRN.Company_Code = ADD_DEDUCT.Company_Code(+)" & vbCrLf _							
        ''            & " AND SALTRN.SALHEADCODE = ADD_DEDUCT.CODE(+) " & vbCrLf _							
        ''            & " AND EMP_CODE = '" & MainClass.AllowSingleQuote(mFromEmpCode) & "'" & vbCrLf _							
        ''            & " AND ADDDEDUCT=" & ConEarning & " AND TYPE=" & ConConveyance & "" & vbCrLf _							
        ''            & " AND SAL_DATE>='" & vb6.Format(RsCompany!START_DATE, "DD-MMM-YYYY") & "'" & vbCrLf _							
        ''            & " AND SAL_DATE<='" & vb6.Format(txtDate, "DD-MMM-YYYY") & "' AND ISARREAR='Y'" & vbCrLf _							
        ''            & " GROUP BY TO_CHAR(SAL_DATE,'MM/YYYY')"          '',ISARREAR "    ''							
        '							
        '    ''& " AND SALTRN.FYEAR= " & RsCompany!FYEAR & "" & vbCrLf _							
        ''							
        '							
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockOptimistic							
        '							
        '    If RsTemp.EOF = False Then							
        '        mArrearConv = RsTemp!AMOUNT1							
        '    End If							

        If mLastSalMade = "" Then
            mVoucherConv = GetConveyanceAllwFromVoucher(mFromEmpCode, "", mFromCompanyCode)
            If mVoucherConv < mConvAmountLimit Then
                GetLastConveyanceAllw = GetLastConveyanceAllw + mVoucherConv
            Else
                GetLastConveyanceAllw = GetLastConveyanceAllw + mConvAmountLimit
            End If
        ElseIf CDate(mLastSalMade) < CDate(xDOL) Then
            mVoucherConv = GetConveyanceAllwFromVoucher(mFromEmpCode, (txtDate.Text), mFromCompanyCode)
            If mVoucherConv < mConvAmountLimit Then
                GetLastConveyanceAllw = GetLastConveyanceAllw + mVoucherConv
            Else
                GetLastConveyanceAllw = GetLastConveyanceAllw + mConvAmountLimit
            End If
        End If

        If mMonth > 0 Then
            If mTempConv < mConvAmountLimit Then
                GetLastConveyanceAllw = GetLastConveyanceAllw + (mMonth * mTempConv)
            Else
                GetLastConveyanceAllw = GetLastConveyanceAllw + (mMonth * mConvAmountLimit)
            End If
        End If


        SqlStr = " SELECT MAX(SALARY_APP_DATE), AMOUNT " & vbCrLf & " FROM PAY_SALARYDEF_MST SALTRN, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf & " WHERE " & vbCrLf & " SALTRN.Company_Code = " & mFromCompanyCode & "" & vbCrLf & " AND SALTRN.FYEAR= " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND SALTRN.Company_Code = ADD_DEDUCT.Company_Code(+)" & vbCrLf & " AND SALTRN.ADD_DEDUCTCode = ADD_DEDUCT.CODE(+) " & vbCrLf & " AND EMP_CODE = '" & MainClass.AllowSingleQuote(mFromEmpCode) & "'" & vbCrLf & " AND ADDDEDUCT=" & ConEarning & " AND TYPE=" & ConConveyance & " GROUP BY AMOUNT"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            If mDays1 > 0 Then
                GetLastConveyanceAllw = GetLastConveyanceAllw + CDbl(VB6.Format(mDays1 * RsTemp.Fields("Amount").Value, "0.00"))
            End If
        End If

        mToEmpCompany = mFromCompanyCode
        mToEmpCode = mFromEmpCode
        GoTo SearchRow
        Exit Function
ErrPart:
        GetLastConveyanceAllw = 0
    End Function

    Private Function GetConveyanceAllwFromVoucher(ByRef pEmpCode As String, ByRef pSalDate As String, ByRef pCompany_Code As Integer) As Double

        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim xDOJ As String
        Dim xDOL As String
        Dim mMonth As Integer
        Dim mDays1 As Double
        Dim mTempConv As Double

        GetConveyanceAllwFromVoucher = 0

        SqlStr = " SELECT SUM(PAYABLEAMOUNT) AS AMOUNT1" & vbCrLf & " FROM PAY_SALVOUCHER_TRN SALTRN, " & vbCrLf & " PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf & " WHERE " & vbCrLf & " SALTRN.Company_Code = " & pCompany_Code & "" & vbCrLf & " AND SALTRN.Company_Code = ADD_DEDUCT.Company_Code" & vbCrLf & " AND SALTRN.SALHEADCODE = ADD_DEDUCT.CODE " & vbCrLf & " AND EMP_CODE = '" & MainClass.AllowSingleQuote(pEmpCode) & "'" & vbCrLf & " AND ADDDEDUCT=" & ConEarning & " AND TYPE=" & ConConveyance & "" & vbCrLf & " AND SALTRN.SAL_TYPE='S'"

        If pSalDate = "" Then
            SqlStr = SqlStr & vbCrLf & " AND SAL_DATE>=TO_DATE('" & VB6.Format(RsCompany.Fields("START_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            SqlStr = SqlStr & vbCrLf & " AND SAL_DATE<=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(SAL_DATE,'MON-YYYY')='" & UCase(VB6.Format(pSalDate, "MMM-YYYY")) & "'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            GetConveyanceAllwFromVoucher = IIf(IsDBNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value)
        End If

        Exit Function
ErrPart:
        GetConveyanceAllwFromVoucher = 0
    End Function
    Private Function CalcLeaveEncash(ByRef xEmpCode As String) As Double

        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset
        Dim mYear As Integer
        'Dim mLeaveDate As String							
        'Dim mISFFCurrentYear As Boolean							

        CalcLeaveEncash = 0

        If Not IsDate(txtDate.Text) Then Exit Function
        mYear = Year(RsCompany.Fields("START_DATE").Value)

        '    If MainClass.ValidateWithMasterTable(xEmpCode, "EMP_CODE", "EMP_LEAVE_DATE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany!COMPANY_CODE & " AND (EMP_LEAVE_DATE IS NOT NULL OR EMP_LEAVE_DATE<>'')") = True Then							
        '        mLeaveDate = MasterNo							
        '        If CVDate(mLeaveDate) >= CVDate(RsCompany!START_DATE) And CVDate(mLeaveDate) <= CVDate(RsCompany!END_DATE) Then							
        '            mISFFCurrentYear = True							
        '        Else							
        '            mISFFCurrentYear = False							
        '        End If							
        '    Else							
        '        mISFFCurrentYear = False							
        '    End If							
        '							
        '    If mISFFCurrentYear = False Then							
        SqlStr = " SELECT SUM(GROSS_AMOUNT) AS GROSS_AMOUNT FROM PAY_ENCASH_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PAYYEAR= " & mYear & "" & vbCrLf & " AND EMP_CODE='" & xEmpCode & "' AND BOOKTYPE='E'"
        '    Else							
        '        SqlStr = " SELECT SUM(EL_AMOUNT) AS GROSS_AMOUNT FROM PAY_FFSETTLE_HDR " & vbCrLf _							
        ''                & " WHERE COMPANY_CODE=" & RsCompany!COMPANY_CODE & "" & vbCrLf _							
        ''                & " AND EMP_CODE='" & xEmpCode & "'"							
        '    End If							
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            CalcLeaveEncash = IIf(IsDBNull(RsTemp.Fields("GROSS_AMOUNT").Value), 0, RsTemp.Fields("GROSS_AMOUNT").Value)
        End If

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number))
    End Function
    Private Function GetFromFF(ByRef xEmpCode As String, ByRef mFieldName As Object) As Double

        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset

        GetFromFF = 0
        SqlStr = " SELECT SUM(" & mFieldName & ") AS GROSS_AMOUNT FROM PAY_FFSETTLE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & xEmpCode & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetFromFF = IIf(IsDBNull(RsTemp.Fields("GROSS_AMOUNT").Value), 0, RsTemp.Fields("GROSS_AMOUNT").Value)
        End If

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number))
    End Function

    Private Function GetMedicalReimburement(ByRef xEmpCode As String) As Double

        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset
        Dim mFromDate As String
        Dim mToDate As String

        GetMedicalReimburement = 0

        mFromDate = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD-MMM-YYYY")
        mToDate = VB6.Format(RsCompany.Fields("END_DATE").Value, "DD-MMM-YYYY")

        SqlStr = " SELECT SUM(AMOUNT) AS AMOUNT " & vbCrLf & " FROM PAY_PERKS_TRN SALTRN,  PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf & " WHERE SALTRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SALTRN.EMP_CODE='" & xEmpCode & "'" & vbCrLf & " AND SALTRN.COMPANY_CODE =ADD_DEDUCT.COMPANY_CODE" & vbCrLf & " AND SALTRN.ADD_DEDUCTCODE =ADD_DEDUCT.CODE" & vbCrLf & " AND ADDDEDUCT = " & ConPerks & "" & vbCrLf & " AND BOOKTYPE = 'P' AND ADD_DEDUCT.TYPE=" & ConMedicalReimbursement & ""

        SqlStr = SqlStr & vbCrLf & " AND SAL_DATE>=TO_DATE('" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND SAL_DATE<=TO_DATE('" & VB6.Format(mToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetMedicalReimburement = IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
        End If

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number))
    End Function
    Private Function CalcOT(ByRef xEmpCode As String) As Double

        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset

        CalcOT = 0

        SqlStr = " SELECT SUM(OT_AMOUNT) AS AMOUNT FROM PAY_MONTHLY_OT_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND OT_Date>= TO_DATE('" & VB6.Format(RsCompany.Fields("START_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND OT_Date<= TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND EMP_CODE='" & xEmpCode & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            CalcOT = IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
        End If



        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number))
    End Function


    Private Sub txtPrevChallan_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPrevChallan.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPrevSalary_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPrevSalary.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
End Class
