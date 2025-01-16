Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility

Imports VB = Microsoft.VisualBasic
Imports System.Data.SqlClient   '' System.Data.OleDb					
Imports System.Data.OleDb

Friend Class frmAddDeductReg
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
    Private Const ColName As Short = 2
    Private Const ColPeriod As Short = 3
    Private Const ColTotEarn As Short = 4
    Private Const ColTotDeduct As Short = 5
    Private Const ColNetpay As Short = 6
    Private Sub FormatSprd(ByRef mRow As Integer)

        On Error GoTo ERR1

        With sprdAddDeduct
            .MaxCols = ColNetpay
            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight * 2.5)

            .Col = ColCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColCode, 7.8)
            .TypeMaxEditLen = 5000

            .Col = ColName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColName, 21)
            .TypeMaxEditLen = 5000

            .Col = ColPeriod
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft
            .set_ColWidth(ColPeriod, 8)
            .TypeMaxEditLen = 5000

            .Col = ColTotEarn
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColTotEarn, 12)
            .TypeMaxEditLen = 5000

            .Col = ColTotDeduct
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColTotDeduct, 10)
            .TypeMaxEditLen = 5000

            .Col = ColNetpay
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColNetpay, 10)
            .TypeMaxEditLen = 5000
        End With

        MainClass.ProtectCell(sprdAddDeduct, 1, sprdAddDeduct.MaxRows, 1, sprdAddDeduct.MaxCols)
        MainClass.SetSpreadColor(sprdAddDeduct, mRow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub FillHeading()

        Dim RsTemp As ADODB.Recordset = Nothing
        'Dim cntCol As Integer
        'Dim mAddDeduct As Integer

        MainClass.ClearGrid(sprdAddDeduct)

        With sprdAddDeduct
            .MaxCols = ColNetpay
            .Row = 0

            .Col = ColSNO
            .Text = "S. No."

            .Col = ColCode
            .Text = "Card No"

            .Col = ColName
            .Text = "Employees' Name "

            .Col = ColPeriod
            .Text = "Period"

            .Col = ColTotEarn
            .Text = "Total Earning"

            .Col = ColTotDeduct
            .Text = "Total Deduction"

            .Col = ColNetpay
            .Text = "Net Pay"
        End With
    End Sub

    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboDept.Enabled = False
        Else
            cboDept.Enabled = True
        End If
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
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
        PubDBCn.Errors.Clear()


        'Insert Data from Grid to PrintDummyData Table...					


        If FillPrintDummyData(sprdAddDeduct, 0, sprdAddDeduct.MaxRows, 0, sprdAddDeduct.MaxCols, PubDBCn) = False Then GoTo ERR1



        'Select Record for print...					

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)

        mSubTitle = "FROM : " & txtFrom.Text & " To " & txtTo.Text
        mTitle = "Earning and Deduction Register"
        Call ShowReport(SqlStr, "EarnDeductReg.Rpt", Mode, mTitle, mSubTitle)

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
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        ' Report1.CopiesToPrinter = PrintCopies					
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub

    Private Sub cmdRefresh_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRefresh.Click
        RefreshScreen()
    End Sub
    Private Sub frmAddDeductReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        'RefreshScreen					
    End Sub
    Private Sub frmAddDeductReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection					
        'PvtDBCn.Open StrConn					
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False

        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)
        OptName.Checked = True
        FillHeading()
        FillDeptCombo()
        txtFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "dd/mm/yyyy")
        txtTo.Text = VB6.Format(RsCompany.Fields("END_DATE").Value, "dd/mm/yyyy")
        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        FormatSprd(-1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub
    Private Sub RefreshScreen()

        On Error GoTo refreshErrPart
        Dim RsEmp As ADODB.Recordset = Nothing
        Dim mStartMonth As Short
        Dim mStartYear As Short
        Dim mEndMonth As Short  ''TO_DATE('" & VB6.Format(txtFrom.Text, "DD-MMM-YYYY") & "')
        Dim mEndYear As Short
        Dim mDeptCode As String
        Dim cntRow As Integer
        Dim mDOJ As Date
        Dim mDOL As Date
        Dim AddRow As Boolean

        MainClass.ClearGrid(sprdAddDeduct)

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboDept.Text = "" Then
                MsgInformation("Please select the Department Name.")
                cboDept.Focus()
                Exit Sub
            End If
        End If

        '    mStartMonth = Month(RsCompany!FYDateFrom)					
        '    mStartYear = Year(RsCompany!FYDateFrom)					
        '					
        '    mEndMonth = Month(RsCompany!FYDateTo)					
        '    mEndYear = Year(RsCompany!FYDateTo)					

        SqlStr = " Select * " & vbCrLf & " From PAY_EMPLOYEE_MST " & vbCrLf & " WHERE EMP_STOP_SALARY='N' AND (EMP_LEAVE_DATE >=TO_DATE('" & VB6.Format(txtFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') OR EMP_LEAVE_DATE IS NULL) " & vbCrLf & " AND EMP_DOJ <=TO_DATE('" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "


        SqlStr = SqlStr & vbCrLf & "AND EMP_CATG<>'C'"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDeptCode = MasterNo
                SqlStr = SqlStr & vbCrLf & " AND EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(Trim(mDeptCode)) & "' "
            End If
        End If

        If OptName.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " Order by EMP_NAME"
        Else
            SqlStr = SqlStr & vbCrLf & " Order by EMP_CODE"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsEmp.EOF = False Then
            With sprdAddDeduct
                Do While Not RsEmp.EOF
                    .Row = .MaxRows
                    AddRow = False
                    If FillDataInSprd(RsEmp.Fields("EMP_Code").Value, (sprdAddDeduct.Row)) = True Then
                        .Col = ColCode
                        .Text = IIf(IsDBNull(RsEmp.Fields("EMP_Code").Value), "", RsEmp.Fields("EMP_Code").Value)

                        .Col = ColName
                        .Text = RsEmp.Fields("EMP_NAME").Value
                        AddRow = True
                    End If
                    RsEmp.MoveNext()
                    If Not RsEmp.EOF And AddRow = True Then
                        .MaxRows = .MaxRows + 1
                    End If
                Loop
                MainClass.ProtectCell(sprdAddDeduct, 0, .MaxRows, 0, .MaxCols)
            End With
        End If
        Exit Sub
refreshErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub FillDeptCombo()

        Dim RsDept As ADODB.Recordset = Nothing
        SqlStr = " Select DEPT_DESC FROM PAY_DEPT_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " Order by DEPT_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockOptimistic)

        If RsDept.EOF = False Then
            Do While Not RsDept.EOF
                cboDept.Items.Add(RsDept.Fields("DEPT_DESC").Value)
                RsDept.MoveNext()
            Loop
        End If
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Function FillDataInSprd(ByRef mCode As String, ByRef mRow As Integer) As Boolean


        Dim RsEmpSal As ADODB.Recordset = Nothing
        Dim mEarn As Double
        Dim mDeduct As Double
        'Dim mNetPay As Double
        Dim mBasicSalary As Double
        Dim mTotEarn As Double
        Dim mTotDeduct As Double
        'Dim TotalPay As Double
        Dim mSalDate As String
        Dim mPeriod As String
        Dim mArrear As String

        FillDataInSprd = False

        SqlStr = " SELECT SALTRN.*,ADD_DEDUCT.ADDDEDUCT" & vbCrLf & " FROM PAY_SAL_TRN SALTRN, PAY_SALARYHEAD_MST ADD_DEDUCT" & vbCrLf & " WHERE " & vbCrLf & " SALTRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SALTRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SALTRN.COMPANY_CODE=ADD_DEDUCT.COMPANY_CODE" & vbCrLf & " AND SALTRN.SALHEADCODE=ADD_DEDUCT.CODE" & vbCrLf & " AND SALTRN.EMP_CODE = '" & mCode & "'"

        SqlStr = SqlStr & vbCrLf & " AND SALTRN.SAL_DATE>=TO_DATE('" & VB6.Format(txtFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND SALTRN.SAL_DATE<=TO_DATE('" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        ''& " AND ADD_DEDUCT.CALC_ON<>" & ConCalcVariable & "" & vbCrLf _					
        '					
        SqlStr = SqlStr & vbCrLf & " ORDER BY SAL_DATE,IsArrear"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmpSal, ADODB.LockTypeEnum.adLockOptimistic)

        sprdAddDeduct.Row = mRow

        If RsEmpSal.EOF = False Then
            FillDataInSprd = True
            sprdAddDeduct.Row = mRow
            '        mYM = RsEmpSal!YM					
            Do While Not RsEmpSal.EOF
                mSalDate = RsEmpSal.Fields("SAL_DATE").Value
                mBasicSalary = RsEmpSal.Fields("PAYABLESALARY").Value
                mPeriod = VB6.Format(RsEmpSal.Fields("SAL_DATE").Value, "MMM-YYYY")
                mArrear = RsEmpSal.Fields("IsArrear").Value

                If RsEmpSal.Fields("ADDDEDUCT").Value = ConEarning Then
                    mEarn = mEarn + RsEmpSal.Fields("PAYABLEAMOUNT").Value
                ElseIf RsEmpSal.Fields("ADDDEDUCT").Value = ConDeduct Then
                    mDeduct = mDeduct + RsEmpSal.Fields("PAYABLEAMOUNT").Value
                End If
                RsEmpSal.MoveNext()
                If Not RsEmpSal.EOF Then
                    If mSalDate & mArrear = RsEmpSal.Fields("SAL_DATE").Value & RsEmpSal.Fields("IsArrear").Value Then
                        GoTo NextRecset
                    End If
                End If
                With sprdAddDeduct
                    .Col = ColPeriod
                    .Text = IIf(.Text = "", "", .Text & Chr(13)) & mPeriod & IIf(mArrear = "Y", "-A", "")

                    .Col = ColTotEarn
                    .Text = IIf(.Text = "", "", .Text & Chr(13)) & MainClass.FormatRupees(mBasicSalary + mEarn)
                    mTotEarn = mTotEarn + mBasicSalary + mEarn

                    .Col = ColTotDeduct
                    .Text = IIf(.Text = "", "", .Text & Chr(13)) & MainClass.FormatRupees(mDeduct)
                    mTotDeduct = mTotDeduct + mDeduct

                    .Col = ColNetpay
                    .Text = IIf(.Text = "", "", .Text & Chr(13)) & MainClass.FormatRupees(mBasicSalary + mEarn - mDeduct)
                End With
                mEarn = 0
                mDeduct = 0
NextRecset:
            Loop
            With sprdAddDeduct
                .Col = ColPeriod
                .Text = .Text & Chr(13) & Chr(13) & "Total :"

                .Col = ColTotEarn
                .Text = .Text & Chr(13) & Chr(13) & MainClass.FormatRupees(mTotEarn)

                .Col = ColTotDeduct
                .Text = .Text & Chr(13) & Chr(13) & MainClass.FormatRupees(mTotDeduct)

                .Col = ColNetpay
                .Text = .Text & Chr(13) & Chr(13) & MainClass.FormatRupees(mTotEarn - mTotDeduct)
            End With
            sprdAddDeduct.set_RowHeight(mRow, sprdAddDeduct.get_MaxTextRowHeight(mRow))
        End If
    End Function

    Private Sub txtFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Not IsDate(txtFrom.Text) Then
            MsgInformation("Please enter the vaild date.")
            Cancel = True
            GoTo EventExitSub
        ElseIf FYChk((txtFrom.Text)) = False Then
            Cancel = True
        End If
        txtFrom.Text = VB6.Format(txtFrom.Text, "dd/mm/yyyy")
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Not IsDate(txtTo.Text) Then
            MsgInformation("Please enter the vaild date.")
            Cancel = True
            GoTo EventExitSub
        ElseIf FYChk((txtTo.Text)) = False Then
            Cancel = True
        End If
        txtTo.Text = VB6.Format(txtTo.Text, "dd/mm/yyyy")
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
