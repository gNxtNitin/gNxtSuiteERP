Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmPayMovementReg
    Inherits System.Windows.Forms.Form

    Dim SqlStr As String = ""
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection

    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12
    Private Const ColSNO As Short = 0
    Private Const ColRefNo As Short = 1
    Private Const ColRefDate As Short = 2
    Private Const ColCard As Short = 3
    Private Const ColName As Short = 4
    Private Const ColDept As Short = 5
    Private Const ColVisitPlace As Short = 6
    Private Const ColVisitFrom As Short = 7
    Private Const ColVisitDistance As Short = 8
    Private Const ColVehicleMode As Short = 9
    Private Const ColFromTime As Short = 10
    Private Const ColToTime As Short = 11
    Private Const ColTotHours As Short = 12
    Private Const ColODTYpe As Short = 13
    Private Const ColAdjustWithLeave As Short = 14
    Private Const ColAdjustWithOT As Short = 15
    Private Const ColAdjustOT As Short = 16
    Private Const ColApprovalBy As Short = 17

    Private Const ColHRApp As Short = 18

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer

    Private Sub FillHeading()
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim cntCol As Integer

        With sprdAttn
            .MaxCols = ColHRApp

            .Row = 0
            .set_RowHeight(0, ConRowHeight * 2)

            .set_RowHeight(-1, ConRowHeight * 1.5)
            .Col = ColSNO
            .Text = "S. No."

            .Col = ColRefNo
            .Text = "Ref No"

            .Col = ColRefDate
            .Text = "RefDate"

            .Col = ColCard
            .Text = "Emp Card No"

            .Col = ColName
            .Text = "Employees' Name "

            .Col = ColDept
            .Text = "Dept"

            .Col = ColVisitPlace
            .Text = "Place of Visit"

            .Col = ColVisitFrom
            .Text = "Visit From"

            .Col = ColVisitDistance
            .Text = "Distance"

            .Col = ColVehicleMode
            .Text = "Mode of Vehicle"

            .Col = ColFromTime
            .Text = "From Time"

            .Col = ColToTime
            .Text = "TO Time"

            .Col = ColTotHours
            .Text = "Total Hours"

            .Col = ColApprovalBy
            .Text = "Approved By"

            .Col = ColODTYpe
            .Text = "OD Type"

            .Col = ColAdjustWithLeave
            .Text = "Adjust With Leave"

            .Col = ColAdjustWithOT
            .Text = "Adjust With OT"

            .Col = ColAdjustOT
            .Text = "Adjust OT (In Mintues)"


            .Col = ColHRApp
            .Text = "HR Approval"

            FormatSprd(-1)
        End With
    End Sub

    Private Sub chkAllEmp_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllEmp.CheckStateChanged
        txtEmpCode.Enabled = IIf(chkAllEmp.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        cmdSearch.Enabled = IIf(chkAllEmp.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForSalary(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportForSalary(ByRef Mode As Crystal.DestinationConstants)


        On Error GoTo ERR1
        Dim All As Boolean
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        Dim ColStartRow As Integer
        Dim ColEndRow As Integer
        Dim cntRow As Integer
        Dim mBankName As String
        Dim mRptFileName As String

        PubDBCn.Errors.Clear()


        Call MainClass.ClearCRptFormulas(Report1)

        'Insert Data from Grid to PrintDummyData Table...

        mSubTitle = ""


        If FillPrintDummyData(sprdAttn, 1, sprdAttn.MaxRows, ColRefNo, sprdAttn.MaxCols, PubDBCn) = False Then GoTo ERR1

        SqlStr = ""
        SqlStr = FetchRecordForReport(SqlStr)
        mRptFileName = "MovementReg.Rpt"

        mTitle = "Movement Register"

        If chkAllEmp.CheckState = System.Windows.Forms.CheckState.UnChecked Then
            mTitle = mTitle & " (Emp Name : " & txtEmpCode.Text & " - " & TxtName.Text & ")"
        End If

        If optMoveType(0).Checked = True Then
            mTitle = mTitle & " (Official Gate Pass)"
        ElseIf optMoveType(1).Checked = True Then
            mTitle = mTitle & " (Personal Gate Pass)"
        End If

        If optHRApp(1).Checked = True Then
            mTitle = mTitle & " (HR Approved)"
        ElseIf optHRApp(2).Checked = True Then
            mTitle = mTitle & " (Pending For HR Approved)"
        End If

        mSubTitle = "From Date : " & VB6.Format(txtFrom.Text, "DD/MM/YYYY") & " To Date : " & VB6.Format(txtTo.Text, "DD/MM/YYYY")

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

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForSalary(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdRefresh_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRefresh.Click

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        MainClass.ClearGrid(sprdAttn)
        RefreshScreen()
        FillHeading()
        FormatSprd(-1)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmPayMovementReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        'RefreshScreen
    End Sub
    Private Sub frmPayMovementReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        FillHeading()

        txtFrom.Text = VB6.Format(RunDate, "dd/mm/yyyy")
        txtTo.Text = VB6.Format(RunDate, "dd/mm/yyyy")

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub
    Private Sub RefreshScreen()

        On Error GoTo refreshErrPart
        Dim RsAttn As ADODB.Recordset = Nothing
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mCode As String


        SqlStr = " SELECT IH.AUTO_KEY_NO, IH.REF_DATE, IH.EMP_CODE, EMP.EMP_NAME, " & vbCrLf _
            & " EMP.EMP_DEPT_CODE, PLACE_VISIT,  " & vbCrLf _
            & " CASE WHEN VISIT_FROM=1 THEN 'N/A' WHEN VISIT_FROM=2 THEN 'Office' WHEN VISIT_FROM=3 THEN 'Home' ELSE 'Others' END, VISIT_DISTANCE, " & vbCrLf _
            & " CASE WHEN VEHICLE_MODE=1 THEN 'N/A' " & vbCrLf _
            & " WHEN VEHICLE_MODE=2 THEN 'Two Wheeler' " & vbCrLf _
            & " WHEN VEHICLE_MODE=3 THEN 'Four Wheeler' " & vbCrLf _
            & " WHEN VEHICLE_MODE=4 THEN 'Self Paid Cab' " & vbCrLf _
            & " WHEN VEHICLE_MODE=5 THEN 'Company Paid Cab' " & vbCrLf _
            & " WHEN VEHICLE_MODE=6 THEN 'Office Cab' ELSE 'Others' END, " & vbCrLf _
            & " TO_CHAR(TIME_FROM,'HH24:MI') AS TIME_FROM, TO_CHAR(TIME_TO,'HH24:MI') AS TIME_TO,  " & vbCrLf _
            & " TO_CHAR(TOTAL_HRS,'HH24:MI') AS TOTAL_HRS, " & vbCrLf _
            & " DECODE(MOVE_TYPE,'O','OFFICIAL',DECODE(MOVE_TYPE,'M','MANUAL','PERSONAL')) AS MOVE_TYPE, "

        SqlStr = SqlStr & vbCrLf _
            & "  DECODE(AGT_LEAVE,'Y','Yes','No') AS AGT_LEAVE, DECODE(AGT_OT,'Y','Yes','No') AS AGT_OT , OT_HOURS, "

        SqlStr = SqlStr & vbCrLf _
            & " ATH_CODE, DECODE(HR_APPROVAL,'Y','YES','NO') AS HR_APPROVAL"


        SqlStr = SqlStr & vbCrLf _
            & " FROM PAY_MOVEMENT_TRN IH, PAY_EMPLOYEE_MST EMP" & vbCrLf _
            & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.COMPANY_CODE =EMP.COMPANY_CODE" & vbCrLf & " AND IH.EMP_CODE =EMP.EMP_CODE " & vbCrLf _
            & " AND REF_DATE>=TO_DATE('" & VB6.Format(txtFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If chkAllEmp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND EMP.EMP_CODE ='" & Trim(txtEmpCode.Text) & "'"
        End If

        If optMoveType(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND IH.MOVE_TYPE='O' "
        ElseIf optMoveType(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND IH.MOVE_TYPE='P' "
        ElseIf optMoveType(2).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND IH.MOVE_TYPE='M' "
        ElseIf optMoveType(3).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND IH.MOVE_TYPE='P' AND IH.AGT_LEAVE='N' AND IH.AGT_OT='N'"
        End If

        If optHRApp(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND IH.HR_APPROVAL='Y' "
        ElseIf optHRApp(2).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND IH.HR_APPROVAL='N' "
        End If



        '    SqlStr = SqlStr & vbCrLf & " AND REF_DATE>=TO_DATE('" & VB6.Format(txtFrom, "DD-MMM-YYYY") & "')" & vbCrLf _
        ''            & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtTo, "DD-MMM-YYYY") & "')" & vbCrLf _
        '
        '    SqlStr = SqlStr & vbCrLf & " GROUP BY IH.EMP_CODE, EMP.EMP_NAME "
        SqlStr = SqlStr & vbCrLf & " ORDER BY IH.EMP_CODE, EMP.EMP_NAME,IH.REF_DATE,IH.AUTO_KEY_NO "

        MainClass.AssignDataInSprd8(SqlStr, sprdAttn, StrConn, "Y")

        CmdPreview.Enabled = True
        cmdPrint.Enabled = True

        Exit Sub
refreshErrPart:
        'Resume
        MsgBox(Err.Description)
    End Sub

    Private Sub FormatSprd(ByRef mRow As Integer)

        On Error GoTo ERR1
        Dim cntCol As Integer

        With sprdAttn
            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight * 1.5)

            .set_ColWidth(ColSNO, 5)


            '
            .Col = ColRefNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColRefNo, 10)

            .Col = ColRefDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColRefDate, 10)

            .Col = ColCard
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColCard, 7)

            .Col = ColName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColName, 15)
            .ColsFrozen = ColName

            .Col = ColDept
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColDept, 15)

            .Col = ColVisitPlace
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColVisitPlace, 15)

            .Col = ColVisitFrom
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColVisitFrom, 6)

            .Col = ColVisitDistance
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColVisitDistance, 6)

            .Col = ColVehicleMode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColVehicleMode, 6)

            .Col = ColFromTime
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColFromTime, 5)

            .Col = ColToTime
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColToTime, 5)

            .Col = ColTotHours
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColTotHours, 6)

            .Col = ColApprovalBy
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColApprovalBy, 10)

            .Col = ColODTYpe
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColODTYpe, 10)

            .Col = ColAdjustWithLeave
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColAdjustWithLeave, 10)

            .Col = ColAdjustWithOT
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColAdjustWithOT, 10)

            .Col = ColAdjustOT
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColAdjustOT, 10)

            .Col = ColHRApp
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColHRApp, 10)

        End With

        '    MainClass.ProtectCell sprdAttn, 1, sprdAttn.MaxRows, 0, sprdAttn.MaxRows
        '    sprdAttn.OperationMode = OperationModeSingle
        '    MainClass.SetSpreadColor sprdAttn, mRow
        '
        MainClass.ProtectCell(sprdAttn, 1, sprdAttn.MaxRows, 0, sprdAttn.MaxRows)
        sprdAttn.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' OperationModeSingle
        sprdAttn.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
        MainClass.SetSpreadColor(sprdAttn, mRow)

        Exit Sub
ERR1:

        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub PrintCommand(ByRef mPrintEnable As Object)
        cmdPreview.Enabled = mPrintEnable
        cmdPrint.Enabled = mPrintEnable
    End Sub

    Private Sub frmPayMovementReg_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        sprdAttn.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth))

        Frame1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        CurrFormWidth = mReFormWidth

        '    MainClass.SetSpreadColor SprdMain, -1
        '    MainClass.SetSpreadColor SprdOption, -1
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub sprdAttn_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles sprdAttn.DblClick

        Dim SqlStr As String = ""
        Dim xIssueNo As Double
        Dim mMoveType As String

        sprdAttn.Row = sprdAttn.ActiveRow

        sprdAttn.Col = ColRefNo
        xIssueNo = Val(sprdAttn.Text)

        sprdAttn.Col = ColODTYpe
        mMoveType = VB.Left(sprdAttn.Text, 1)
        mMoveType = IIf(mMoveType = "M", "A", "M")

        myMenu = "mnuMovementSlipEntry"
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, "mnuMovementSlipEntry", PubDBCn)
        If InStr(1, XRIGHT, "M", CompareMethod.Text) = 0 Then
            Exit Sub
        End If
        frmPayMovementSlip.MdiParent = Me.MdiParent
        frmPayMovementSlip.Show()

        frmPayMovementSlip.frmPayMovementSlip_Activated(Nothing, New System.EventArgs())
        frmPayMovementSlip.lblBookType.Text = "H"

        'If mMoveType = "A" Then
        '    frmPayMovementSlip.optMoveType(0).Enabled = False
        '    frmPayMovementSlip.optMoveType(1).Enabled = False
        '    frmPayMovementSlip.optMoveType(2).Enabled = True
        '    frmPayMovementSlip.optMoveType(2).Checked = True
        'Else
        frmPayMovementSlip.optMoveType(0).Enabled = True
        frmPayMovementSlip.optMoveType(1).Enabled = True
        frmPayMovementSlip.optMoveType(2).Enabled = True
        frmPayMovementSlip.optMoveType(0).Checked = True
        'End If

        frmPayMovementSlip.lblMovementType.Text = mMoveType

        frmPayMovementSlip.txtRefNo.Text = CStr(xIssueNo)
        frmPayMovementSlip.txtRefNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
    End Sub


    Private Sub txtEmpCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmpCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtEmpCode.Text) = "" Then GoTo EventExitSub
        txtEmpCode.Text = VB6.Format(txtEmpCode.Text, "000000")
        If MainClass.ValidateWithMasterTable((txtEmpCode.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invalid Employee Code ")
            Cancel = True
        Else
            TxtName.Text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        If MainClass.SearchGridMaster((txtEmpCode.Text), "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtEmpCode.Text = AcName1
            TxtName.Text = AcName
        End If
    End Sub
End Class
