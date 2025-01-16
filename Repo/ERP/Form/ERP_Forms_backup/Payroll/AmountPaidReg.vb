Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmAmountPaidReg
    Inherits System.Windows.Forms.Form

    Dim SqlStr As String = ""
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As Connection

    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12

    Private Const ColSNO As Short = 0
    Private Const ColCode As Short = 1
    Private Const ColName As Short = 2
    Private Const ColAPApr As Short = 3
    Private Const ColAPMay As Short = 4
    Private Const ColAPJune As Short = 5
    Private Const ColAPJuly As Short = 6
    Private Const ColAPAug As Short = 7
    Private Const ColAPSep As Short = 8
    Private Const ColAPOct As Short = 9
    Private Const ColAPNov As Short = 10
    Private Const ColAPDec As Short = 11
    Private Const ColAPJan As Short = 12
    Private Const ColAPFeb As Short = 13
    Private Const ColAPMar As Short = 14
    Private Const ColTotalAmountPaid As Short = 15
    Private Const ColComputaionAmount As Short = 16
    Private Const ColDiff As Short = 17

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Private Sub FormatSprd(ByRef mRow As Integer)

        On Error GoTo ERR1
        Dim cntCol As Integer

        With sprdAddDeduct
            .MaxCols = ColDiff
            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight * 2)

            .Col = ColCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColCode, 6)
            .TypeMaxEditLen = 5000

            .Col = ColName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColName, 18)
            .TypeMaxEditLen = 5000

            .ColsFrozen = ColName

            For cntCol = ColAPApr To ColDiff
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("9999999.99")
                .TypeFloatMin = CDbl("-9999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
                .set_ColWidth(cntCol, 9)
            Next

        End With

        Call FillHeading()
        MainClass.ProtectCell(sprdAddDeduct, 1, sprdAddDeduct.MaxRows, 0, sprdAddDeduct.MaxCols)
        sprdAddDeduct.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
        MainClass.SetSpreadColor(sprdAddDeduct, mRow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub FillHeading()
        With sprdAddDeduct
            .MaxCols = ColDiff
            .Row = 0

            .Col = ColSNO
            .Text = "S. No."

            .Col = ColCode
            .Text = "Card No"

            .Col = ColName
            .Text = "Employees' Name "

            .Col = ColAPApr
            .Text = "Amount Paid (Apr)"

            .Col = ColAPMay
            .Text = "Amount Paid (May)"

            .Col = ColAPJune
            .Text = "Amount Paid (Jun)"

            .Col = ColAPJuly
            .Text = "Amount Paid (Jul)"

            .Col = ColAPAug
            .Text = "Amount Paid (Aug)"

            .Col = ColAPSep
            .Text = "Amount Paid (Sep)"

            .Col = ColAPOct
            .Text = "Amount Paid (Oct)"

            .Col = ColAPNov
            .Text = "Amount Paid (Nov)"

            .Col = ColAPDec
            .Text = "Amount Paid (Dec)"

            .Col = ColAPJan
            .Text = "Amount Paid (Jan)"

            .Col = ColAPFeb
            .Text = "Amount Paid (Feb)"

            .Col = ColAPMar
            .Text = "Amount Paid (Mar)"

            .Col = ColTotalAmountPaid
            .Text = "Amount Paid"

            .Col = ColComputaionAmount
            .Text = "Computation Amount"

            .Col = ColDiff
            .Text = "Diff. Amount"
        End With
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

        Dim mRptFileName As String
        Dim mBankName As String
        Dim mChequeNo As String
        Dim mChequeDate As String
        Dim mChequeAmount As String


        'Insert Data from Grid to PrintDummyData Table...

        Call MainClass.ClearCRptFormulas(Report1)

        mTitle = "Amount Paid -  Register"

        mRptFileName = "AmountPaidReg.Rpt"
        If FillPrintDummyData(sprdAddDeduct, 1, sprdAddDeduct.MaxRows, 0, sprdAddDeduct.MaxCols, PubDBCn) = False Then GoTo ERR1

        mSubTitle = "FROM : " & txtFrom.Text & " To " & txtTo.Text

        'Select Record for print...

        SqlStr = ""
        SqlStr = FetchRecordForReport(SqlStr)
        Call ShowReport(SqlStr, mRptFileName, Mode, mTitle, mSubTitle)
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
    Private Sub PrintCommand(ByRef mPrintEnable As Object)
        CmdPreview.Enabled = mPrintEnable
        cmdPrint.Enabled = mPrintEnable
    End Sub
    Private Sub cmdRefresh_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRefresh.Click

        Dim cntRow As Integer
        Dim mEmpCode As String
        Dim mAmountPaid As Double
        Dim mComputaionAmount As Double
        Dim mDiff As Double

        MainClass.ClearGrid(sprdAddDeduct)

        If optAll(1).Checked = True Then
            If txtEmpCode.Text = "" Then
                MsgInformation("Please select the Employee Code.")
                txtEmpCode.Focus()
                Exit Sub
            End If
        End If

        RefreshScreen()
        FormatSprd(-1)

        With sprdAddDeduct
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColCode
                mEmpCode = Trim(.Text)

                mComputaionAmount = GetComputationAmount(mEmpCode)

                .Col = ColComputaionAmount
                .Text = VB6.Format(mComputaionAmount, "0.00")

                .Col = ColTotalAmountPaid
                mAmountPaid = Val(.Text)

                mDiff = mComputaionAmount - mAmountPaid

                .Col = ColDiff
                .Text = VB6.Format(mDiff, "0.00")
            Next
        End With
    End Sub
    Private Function GetComputationAmount(ByRef mEmpCode As String) As Double

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim cntRow As Integer


        GetComputationAmount = 0
        SqlStr = " SELECT SUM(IH.TOTALAMOUNT) AS TOTALAMOUNT" & vbCrLf & " FROM PAY_ITCOMP_TRN IH" & vbCrLf & " WHERE IH.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR = " & RsCompany.Fields("FYEAR").Value & ""

        SqlStr = SqlStr & vbCrLf & " AND IH.SUBROWNO=65"
        SqlStr = SqlStr & vbCrLf & " AND IH.EMP_CODE ='" & MainClass.AllowSingleQuote(mEmpCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetComputationAmount = CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("TotalAmount").Value), 0, RsTemp.Fields("TotalAmount").Value), "0.00"))
        End If

        Exit Function
LedgError:
        'Resume
        MsgInformation(Err.Description)
    End Function
    Private Sub frmAmountPaidReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        'RefreshScreen
    End Sub
    Private Sub frmAmountPaidReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        CurrFormHeight = 7245
        CurrFormWidth = 11355

        ADDMode = False
        MODIFYMode = False

        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)
        OptName.Checked = True
        FillHeading()
        txtFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "dd/mm/yyyy")
        txtTo.Text = VB6.Format(RsCompany.Fields("END_DATE").Value, "dd/mm/yyyy")

        optAll(0).Checked = True
        txtEmpCode.Enabled = False
        cmdsearch.Enabled = False

        FormatSprd(-1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub
    Private Sub frmAmountPaidReg_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        sprdAddDeduct.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth))

        Frame1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        CurrFormWidth = mReFormWidth

        '    MainClass.SetSpreadColor SprdMain, -1
        '    MainClass.SetSpreadColor SprdOption, -1
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtEmpCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmpCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtEmpCode.Text) = "" Then GoTo EventExitSub
        txtEmpCode.Text = VB6.Format(txtEmpCode.Text, "000000")
        If MainClass.ValidateWithMasterTable((txtEmpCode.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invalid Employee Code ")
            Cancel = True
        Else
            txtName.Text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        If MainClass.SearchGridMaster((txtEmpCode.Text), "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtEmpCode.Text = AcName1
            txtName.Text = AcName
        End If
    End Sub

    Private Sub optAll_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optAll.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optAll.GetIndex(eventSender)
            If optAll(0).Checked = True Then
                txtEmpCode.Enabled = False
                cmdsearch.Enabled = False
            ElseIf optAll(1).Checked = True Then
                txtEmpCode.Enabled = True
                cmdsearch.Enabled = True
            End If
        End If
    End Sub

    Private Sub RefreshScreen()

        On Error GoTo refreshErrPart

        MainClass.ClearGrid(sprdAddDeduct)

        SqlStr = " Select EMP.EMP_CODE, EMP.EMP_NAME, "

        SqlStr = SqlStr & vbCrLf & " SUM(CASE WHEN TO_CHAR(IH.VDATE,'MM')='04' THEN AMOUNT_PAID END) AS AMT_PAID_APR," & vbCrLf & " SUM(CASE WHEN TO_CHAR(IH.VDATE,'MM')='05' THEN AMOUNT_PAID END) AS AMT_PAID_MAY," & vbCrLf & " SUM(CASE WHEN TO_CHAR(IH.VDATE,'MM')='06' THEN AMOUNT_PAID END) AS AMT_PAID_JUN," & vbCrLf & " SUM(CASE WHEN TO_CHAR(IH.VDATE,'MM')='07' THEN AMOUNT_PAID END) AS AMT_PAID_JUL," & vbCrLf & " SUM(CASE WHEN TO_CHAR(IH.VDATE,'MM')='08' THEN AMOUNT_PAID END) AS AMT_PAID_AUG," & vbCrLf & " SUM(CASE WHEN TO_CHAR(IH.VDATE,'MM')='09' THEN AMOUNT_PAID END) AS AMT_PAID_SEP," & vbCrLf & " SUM(CASE WHEN TO_CHAR(IH.VDATE,'MM')='10' THEN AMOUNT_PAID END) AS AMT_PAID_OCT," & vbCrLf & " SUM(CASE WHEN TO_CHAR(IH.VDATE,'MM')='11' THEN AMOUNT_PAID END) AS AMT_PAID_NOV," & vbCrLf & " SUM(CASE WHEN TO_CHAR(IH.VDATE,'MM')='12' THEN AMOUNT_PAID END) AS AMT_PAID_DEC," & vbCrLf & " SUM(CASE WHEN TO_CHAR(IH.VDATE,'MM')='01' THEN AMOUNT_PAID END) AS AMT_PAID_JAN," & vbCrLf & " SUM(CASE WHEN TO_CHAR(IH.VDATE,'MM')='02' THEN AMOUNT_PAID END) AS AMT_PAID_FEB," & vbCrLf & " SUM(CASE WHEN TO_CHAR(IH.VDATE,'MM')='03' THEN AMOUNT_PAID END) AS AMT_PAID_MAR," & vbCrLf & " SUM(AMOUNT_PAID) AS AMT_PAID," & vbCrLf & " 0,0 "

        SqlStr = SqlStr & vbCrLf & " From PAY_ITChallan_HDR IH, PAY_ITChallan_DET ID, PAY_EMPLOYEE_MST EMP " & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND IH.AUTO_KEY_REFNO=ID.AUTO_KEY_REFNO " & vbCrLf & " AND ID.COMPANY_CODE=EMP.COMPANY_CODE " & vbCrLf & " AND ID.EMP_CODE=EMP.EMP_CODE "

        If optShow(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & "AND IH.BOOKTYPE='R' "
        ElseIf optShow(2).Checked = True Then
            SqlStr = SqlStr & vbCrLf & "AND IH.BOOKTYPE='O' "
        End If

        If optAll(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & "AND EMP.EMP_CODE='" & MainClass.AllowSingleQuote(Trim(txtEmpCode.Text)) & "' "
        End If

        SqlStr = SqlStr & vbCrLf & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " GROUP by EMP.EMP_CODE,EMP.EMP_NAME"
        If OptName.Checked = True Then
            SqlStr = SqlStr & vbCrLf & " Order by EMP.EMP_NAME"
        Else
            SqlStr = SqlStr & vbCrLf & " Order by EMP.EMP_CODE"
        End If

        MainClass.AssignDataInSprd8(SqlStr, sprdAddDeduct, StrConn, "Y")
        Call PrintCommand(True)
        Exit Sub
refreshErrPart:
        'Resume
        MsgBox(Err.Description)
    End Sub
    Private Sub optCardNo_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optCardNo.CheckedChanged
        If eventSender.Checked Then
            Call PrintCommand(False)
        End If
    End Sub
    Private Sub txtFrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFrom.TextChanged
        Call PrintCommand(False)
    End Sub
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
    Private Sub txtTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTo.TextChanged
        Call PrintCommand(False)
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
