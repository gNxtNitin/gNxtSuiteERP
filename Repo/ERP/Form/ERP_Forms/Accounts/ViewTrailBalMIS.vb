Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmViewTrailBalMIS
    Inherits System.Windows.Forms.Form
    Dim FormLoaded As Boolean
    'Private PvtDBCn As ADODB.Connection	

    Private Const RowHeight As Short = 12
    '	
    'Private Const ColPicMain = 1	
    'Private Const ColPicSub = 2	
    Private Const ColAccountCode As Short = 1
    Private Const ColAcmName As Short = 2
    Private Const ColGroupType As Short = 3
    Private Const ColGroupSNo As Short = 4
    Private Const ColAprBal As Short = 5
    Private Const ColMayBal As Short = 6
    Private Const ColJunBal As Short = 7
    Private Const ColJulBal As Short = 8
    Private Const ColAugBal As Short = 9
    Private Const ColSepBal As Short = 10
    Private Const ColOctBal As Short = 11
    Private Const ColNovBal As Short = 12
    Private Const ColDecBal As Short = 13
    Private Const ColJanBal As Short = 14
    Private Const ColFebBal As Short = 15
    Private Const ColMarBal As Short = 16
    Private Const ColTotBal As Short = 17
    'Private Const ColFlag = 20	

    Dim PrintFlag As Boolean
    Dim CurrFormHeight As Integer
    Dim CurrFormWidth As Integer
    Dim minuspict As System.Drawing.Image
    Dim pluspict As System.Drawing.Image
    Dim mIsGrouped As Boolean
    Private Sub PrintStatus()
        cmdPrint.Enabled = PrintFlag
        CmdPreview.Enabled = PrintFlag
    End Sub
    Private Sub ChkHideZeroBal_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkHideZeroBal.CheckStateChanged
        PrintFlag = False
        PrintStatus()
    End Sub
    Private Sub ChkHideZeroTrans_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkHideZeroTrans.CheckStateChanged
        PrintFlag = False
        PrintStatus()
    End Sub
    Private Sub cmdExit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdExit.Click
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub FillSprdTrail()
        On Error GoTo ERR1
        Dim I As Integer
        Dim mCol As Integer
        With SprdMain
            .Row = 0

            .Col = 0
            .Text = "S.No."

            .Col = ColAcmName
            .Text = "Account Name"

            mCol = ColGroupSNo
            For I = 4 To 12
                mCol = mCol + 1
                .Col = mCol
                .Text = MonthName(I, True) & " Balance (Rs.)"
            Next

            For I = 1 To 3
                mCol = mCol + 1
                .Col = mCol
                .Text = MonthName(I, True) & " Balance (Rs.)"
            Next

            .Col = ColGroupType
            .Text = "Type"

            .Col = ColAccountCode
            .Text = "Code"

            .Col = ColGroupSNo
            .Text = "SNo"

            .Col = ColTotBal
            .Text = "Total"

        End With
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume	
    End Sub
    Private Sub FormatSprdTrail(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim I As Integer

        With SprdMain
            .MaxCols = ColTotBal

            .set_RowHeight(0, 2.5 * RowHeight)

            .Row = -1
            .set_ColWidth(0, 4)

            '        .Col = ColPicMain	
            '        .CellType = CellTypePicture	
            '        .TypePictCenter = True	
            '        .TypePictMaintainScale = False	
            '        .TypePictStretch = False	
            '	
            '        .Col = ColPicSub	
            '        .CellType = CellTypePicture	
            '        .TypePictCenter = True	
            '        .TypePictMaintainScale = False	
            '        .TypePictStretch = False	
            '	
            .Col = ColAcmName
            .set_ColWidth(ColAcmName, 26)
            .ColsFrozen = ColAcmName

            For I = ColAprBal To ColMarBal
                .Col = I
                .set_ColWidth(I, 10)
                .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.99")
                .TypeFloatMin = CDbl("-999999999.99")
                '.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC	
            Next

            .Col = ColTotBal
            .set_ColWidth(ColTotBal, 10)
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")

            .Col = ColGroupType
            .ColHidden = True
            .set_ColWidth(ColGroupType, 0)


            .Col = ColAccountCode
            .ColHidden = True
            .set_ColWidth(ColAccountCode, 0)

            .Col = ColGroupSNo
            .ColHidden = True
            .set_ColWidth(ColGroupSNo, 10)

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            .GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ''= OperationModeSingle	

            SprdMain.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            'Show the grid lines over the color	
            '        SprdMain.BackColorStyle = BackColorStyleOverVertGridOnly	


        End With
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume	
    End Sub

    Private Sub RowFormat()
        On Error GoTo ERR1
        Dim mCategory As Boolean
        Dim CntRow As Integer
        Dim mParentcode As Integer

        '    With SprdMain	
        '        For cntRow = 1 To .MaxRows	
        '          .Row = cntRow	
        '	
        '          .Col = ColGroupType	
        '          mCategory = IIf(.Text = "G", True, False)	
        '	
        '          .Col = ColAcmName	
        '          .FontBold = mCategory	
        '	
        '          .Col = ColOpening	
        '          .FontBold = mCategory	
        '	
        '          .Col = ColCOpening	
        '          .FontBold = mCategory	
        '	
        '          .Col = ColDAmount	
        '          .FontBold = mCategory	
        '	
        '          .Col = ColCAmount	
        '          .FontBold = mCategory	
        '	
        '          .Col = ColDBAmount	
        '          .FontBold = mCategory	
        '	
        '          .Col = ColCBAmount	
        '         .FontBold = mCategory	
        '	
        '         .Col = ColGroupSNo	
        '         mParentcode = Val(.Text)	
        '	
        '         If mCategory = True Then	
        '             .Row = cntRow	
        '            .Row2 = cntRow	
        '            .Col = 1	
        '            .col2 = .MaxCols	
        '            .BlockMode = True	
        '	
        '            If mParentcode = -1 Then	
        '                .BackColor = &H8000000F         ''&H80FF80	
        '            Else	
        '                .BackColor = &H80000018	
        '            End If	
        '            .BlockMode = False	
        '        End If	
        '        Next	
        '	
        '    End With	
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume	
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForTrailBal(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForTrailBal(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportForTrailBal(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mRPTName As String


        SqlStr = ""

        'UPGRADE_WARNING: Untranslated statement in ReportForTrailBal. Please check source code.	


        'Select Record for print...	

        SqlStr = ""

        SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")

        mTitle = "Trial Balance (MIS)"

        mSubTitle = "From: " & VB6.Format(txtDate(0).Text, "DD MMM, YYYY") & " To: " & VB6.Format(txtDate(1).Text, "DD MMM, YYYY")
        mRPTName = "GroupTrailBalMIS.Rpt"

        Call ShowReport(SqlStr, mRPTName, Mode, mTitle, mSubTitle)

        Exit Sub
ERR1:
        If Err.Number = 32755 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        Else
            MsgInformation(Err.Description)
        End If
        ''Resume	
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)

        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub
    Public Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mGroupType As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        MainClass.ClearGrid(SprdMain)


        If ViewTrialSumm = False Then Exit Sub

        RowFormat()
        DisplayTotals()


        FillSprdTrail()
        '    GroupBySpread ColPicMain	

        SprdMain.Focus()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        PrintFlag = True
        PrintStatus()
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume	
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub DisplayTotals()

        On Error GoTo ERR1
        Dim mApr As Double
        Dim mMay As Double
        Dim mJun As Double
        Dim mJul As Double
        Dim mAug As Double
        Dim mSep As Double
        Dim mOct As Double
        Dim mNov As Double
        Dim mDec As Double
        Dim mJan As Double
        Dim mFeb As Double
        Dim mMar As Double
        Dim mTotal As Double

        Dim CntRow As Integer

        With SprdMain
            For CntRow = 1 To .MaxRows
                .Row = CntRow
                .Col = ColAprBal
                mApr = mApr + CDbl(IIf(IsNumeric(.Text), .Text, 0))

                .Col = ColMayBal
                mMay = mMay + CDbl(IIf(IsNumeric(.Text), .Text, 0))

                .Col = ColJunBal
                mJun = mJun + CDbl(IIf(IsNumeric(.Text), .Text, 0))

                .Col = ColJulBal
                mJul = mJul + CDbl(IIf(IsNumeric(.Text), .Text, 0))

                .Col = ColAugBal
                mAug = mAug + CDbl(IIf(IsNumeric(.Text), .Text, 0))

                .Col = ColSepBal
                mSep = mSep + CDbl(IIf(IsNumeric(.Text), .Text, 0))

                .Col = ColOctBal
                mOct = mOct + CDbl(IIf(IsNumeric(.Text), .Text, 0))

                .Col = ColNovBal
                mNov = mNov + CDbl(IIf(IsNumeric(.Text), .Text, 0))

                .Col = ColDecBal
                mDec = mDec + CDbl(IIf(IsNumeric(.Text), .Text, 0))

                .Col = ColJanBal
                mJan = mJan + CDbl(IIf(IsNumeric(.Text), .Text, 0))

                .Col = ColFebBal
                mFeb = mFeb + CDbl(IIf(IsNumeric(.Text), .Text, 0))

                .Col = ColMarBal
                mMar = mMar + CDbl(IIf(IsNumeric(.Text), .Text, 0))

                .Col = ColTotBal
                mTotal = mTotal + CDbl(IIf(IsNumeric(.Text), .Text, 0))
            Next

            Call MainClass.AddBlankfpSprdRow(SprdMain, ColAccountCode)
            .Row = .MaxRows
            .Col = ColAcmName
            .Text = "TOTAL :"
            .Font = VB6.FontChangeBold(.Font, True)

            .Row = .MaxRows
            .Row2 = .MaxRows
            .Col = 1
            .col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80	
            .BlockMode = False

            .Row = .MaxRows
            .Col = ColAprBal
            .Text = VB6.Format(mApr, "0.00")

            .Col = ColMayBal
            .Text = VB6.Format(mMay, "0.00")

            .Col = ColJunBal
            .Text = VB6.Format(mJun, "0.00")

            .Col = ColJulBal
            .Text = VB6.Format(mJul, "0.00")

            .Col = ColAugBal
            .Text = VB6.Format(mAug, "0.00")

            .Col = ColSepBal
            .Text = VB6.Format(mSep, "0.00")

            .Col = ColOctBal
            .Text = VB6.Format(mOct, "0.00")

            .Col = ColNovBal
            .Text = VB6.Format(mNov, "0.00")

            .Col = ColDecBal
            .Text = VB6.Format(mDec, "0.00")

            .Col = ColJanBal
            .Text = VB6.Format(mJan, "0.00")

            .Col = ColFebBal
            .Text = VB6.Format(mFeb, "0.00")

            .Col = ColMarBal
            .Text = VB6.Format(mMar, "0.00")

            .Col = ColTotBal
            .Text = VB6.Format(mTotal, "0.00")

            FormatSprdTrail(-1)

        End With

        PrintStatus()
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume	
    End Sub

    Private Function ViewTrialSumm() As Boolean

        On Error GoTo ViewTrialErr

        Dim SqlStr As String = ""
        Dim mDeptName As String
        Dim mCostCName As String
        Dim mConsolidated As String
        Dim mGroupOption As String


        ''********SELECTION..........	
        SqlStr = " SELECT TO_CHAR(ACMGROUP.GROUP_CODE), ACMGROUP.GROUP_NAME, GROUP_TYPE, TO_CHAR(GROUP_SEQNO), " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(VDATE,'MM') = '04' THEN AMOUNT * DECODE(DC,'C',1,-1) ELSE 0 END)) AS APRBAL, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(VDATE,'MM') = '05' THEN AMOUNT * DECODE(DC,'C',1,-1) ELSE 0 END)) AS MAYBAL, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(VDATE,'MM') = '06' THEN AMOUNT * DECODE(DC,'C',1,-1) ELSE 0 END)) AS JUNBAL, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(VDATE,'MM') = '07' THEN AMOUNT * DECODE(DC,'C',1,-1) ELSE 0 END)) AS JULBAL, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(VDATE,'MM') = '08' THEN AMOUNT * DECODE(DC,'C',1,-1) ELSE 0 END)) AS AUGBAL, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(VDATE,'MM') = '09' THEN AMOUNT * DECODE(DC,'C',1,-1) ELSE 0 END)) AS SEPBAL, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(VDATE,'MM') = '10' THEN AMOUNT * DECODE(DC,'C',1,-1) ELSE 0 END)) AS OCTBAL, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(VDATE,'MM') = '11' THEN AMOUNT * DECODE(DC,'C',1,-1) ELSE 0 END)) AS NOVBAL, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(VDATE,'MM') = '12' THEN AMOUNT * DECODE(DC,'C',1,-1) ELSE 0 END)) AS DECBAL, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(VDATE,'MM') = '01' THEN AMOUNT * DECODE(DC,'C',1,-1) ELSE 0 END)) AS JANBAL, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(VDATE,'MM') = '02' THEN AMOUNT * DECODE(DC,'C',1,-1) ELSE 0 END)) AS FEBBAL, " & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(VDATE,'MM') = '03' THEN AMOUNT * DECODE(DC,'C',1,-1) ELSE 0 END)) AS MARBAL, " & vbCrLf & " TO_CHAR(SUM(AMOUNT * DECODE(DC,'C',1,-1))) AS TOTAMOUNT "


        ''********TABLEs..........	
        SqlStr = SqlStr & vbCrLf & " FROM FIN_POSTED_TRN TRN,FIN_SUPP_CUST_MST ACM, FIN_MIS_GROUP_MST ACMGROUP "


        ''********Joining..........	

        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        SqlStr = SqlStr & vbCrLf & " AND TRN.COMPANY_CODE=ACM.COMPANY_CODE " & vbCrLf & " AND TRN.ACCOUNTCODE=ACM.SUPP_CUST_CODE " & vbCrLf & " AND ACM.COMPANY_CODE=ACMGROUP.COMPANY_CODE " & vbCrLf & " AND ACM.MIS_GROUP_CODE=ACMGROUP.GROUP_CODE "


        SqlStr = SqlStr & vbCrLf & " AND TRN.VDate >= TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND TRN.VDate <= TO_DATE('" & VB6.Format(txtDate(1).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        If ChkHideZeroBal.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & " HAVING SUM(AMOUNT * DECODE(DC,'D',1,-1))<>0"
        ElseIf ChkHideZeroTrans.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & " HAVING SUM(AMOUNT)<>0 "
        End If

        ''********GROUP BY CLAUSE..........	
        SqlStr = SqlStr & vbCrLf & " GROUP BY " & vbCrLf & " GROUP_TYPE , GROUP_SEQNO, ACMGROUP.GROUP_NAME, ACMGROUP.GROUP_CODE "


        ''********ORDER BY CLAUSE..........	
        SqlStr = SqlStr & vbCrLf & " ORDER BY " & vbCrLf & " GROUP_TYPE DESC, GROUP_SEQNO, ACMGROUP.GROUP_NAME "

        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")
        ViewTrialSumm = True
        Exit Function
ViewTrialErr:
        ViewTrialSumm = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume	
    End Function
    Private Sub frmViewTrailBalMIS_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        Dim BookType As String
        Dim SqlStr As String = ""

        If FormLoaded = True Then Exit Sub


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        txtDate(0).Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDate(1).Text = VB6.Format(RunDate, "DD/MM/YYYY")
        Me.Text = "Trial Balance"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ChkHideZeroTrans.CheckState = System.Windows.Forms.CheckState.Checked
        FormLoaded = True
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
        'Resume	
    End Sub
    Private Sub frmViewTrailBalMIS_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo err_Renamed
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        ''Set PvtDBCn = New ADODB.Connection	
        ''PvtDBCn.Open StrConn	

        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)


        MainClass.SetControlsColor(Me)
        FormatSprdTrail(-1)
        FillSprdTrail()

        FormLoaded = False
        Call frmViewTrailBalMIS_Activated(eventSender, eventArgs)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
err_Renamed:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmViewTrailBalMIS_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(mReFormWidth - 100)
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
    End Sub

    Private Sub frmViewTrailBalMIS_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdMain.DblClick
        On Error GoTo ErrPart
        Dim mRow As Integer
        Dim mCol As Integer
        Dim mGroupCode As Double
        Dim mGroupName As String
        If SprdMain.ActiveRow <= 0 Then Exit Sub

        With SprdMain
            .Row = eventArgs.Row
            .Col = ColAccountCode
            mGroupCode = Val(.Text)
            .Col = ColAcmName
            mGroupName = Trim(.Text)
            frmViewTrailBalMISAcct.MdiParent = Me.MdiParent
            frmViewTrailBalMISAcct.lblGroupCode.Text = CStr(mGroupCode)
            frmViewTrailBalMISAcct.lblDateFrom.Text = VB6.Format(txtDate(0).Text, "DD/MM/YYYY")
            frmViewTrailBalMISAcct.lblDateTo.Text = VB6.Format(txtDate(1).Text, "DD/MM/YYYY")
            frmViewTrailBalMISAcct.lblAccountName.Text = mGroupName
            frmViewTrailBalMISAcct.Show()

            frmViewTrailBalMISAcct.frmViewTrailBalMISAcct_Activated(Nothing, New System.EventArgs())
            '        frmViewLedger.cmdShow_Click	

        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)

    End Sub


    Private Sub txtDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDate.TextChanged
        Dim Index As Short = txtDate.GetIndex(eventSender)
        PrintFlag = False
        PrintStatus()
    End Sub
    Private Sub txtDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim Index As Short = txtDate.GetIndex(eventSender)
        If txtDate(Index).Text = "" Then GoTo EventExitSub
        'UPGRADE_WARNING: Untranslated statement in txtDate_Validate. Please check source code.	
        If FYChk(CStr(CDate(txtDate(Index).Text))) = False Then Cancel = True : txtDate(Index).Focus() : GoTo EventExitSub
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
