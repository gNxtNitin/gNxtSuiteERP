Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamEmpRecruitment
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20

    Dim mAccountCode As Integer
    Private Const ColIntimationDate As Short = 1
    Private Const ColMachName As Short = 2
    Private Const ColSurveyorName As Short = 3
    Private Const ColRefNo As Short = 4
    Private Const ColBillNo As Short = 5
    Private Const ColBillDate As Short = 6
    Private Const colSupplier As Short = 7
    Private Const ColBillAmount As Short = 8
    Private Const ColChqNo As Short = 9
    Private Const ColSettledAmt As Short = 10
    Private Const ColDiff As Short = 11
    Private Const ColMKEY As Short = 12

    Private Const mPageWidth As Short = 232

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonST(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonST(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
        Dim MainClass_Renamed As Object
        On Error GoTo ErrPart
        If FieldsVerification() = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        If Show1() = False Then GoTo ErrPart
        Call PrintStatus(True)
        CalcSprdTotal()
        FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamEmpRecruitment_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Insurance Claim Register"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamEmpRecruitment_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Dim MainClass_Renamed As Object
        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)

        Call PrintStatus(True)
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamEmpRecruitment_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        Dim MainClass_Renamed As Object
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame4.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11379.7, 749)
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamEmpRecruitment_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub OptStatus_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptStatus.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptStatus.GetIndex(eventSender)
            Call PrintStatus(False)
        End If
    End Sub

    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
    End Sub

    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdMain.DblClick
        Dim MainClass_Renamed As Object
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        Dim xVDate As String
        Dim xMkey As String
        Dim xVNo As String



        SprdMain.Row = SprdMain.ActiveRow

        SprdMain.Col = ColMKEY
        xMkey = Me.SprdMain.Text

        SqlStr = "SELECT * FROM DOC_INS_CLAIM_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND MKEY='" & xMkey & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            xVDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("REF_DATE").Value), "", RsTemp.Fields("REF_DATE").Value), "DD/MM/YYYY")
            xVNo = IIf(IsDBNull(RsTemp.Fields("REF_NO").Value), "", RsTemp.Fields("REF_NO").Value)

            frmInsDocEntry.lblMkey.Text = xMkey
            frmInsDocEntry.Show()
            frmInsDocEntry.frmInsDocEntry_Activated(Nothing, New System.EventArgs())
            frmInsDocEntry.txtVNo.Text = xVNo
            frmInsDocEntry.txtVDate.Text = xVDate
            frmInsDocEntry.TxtVNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))

            '        Call ShowTrn(xMkey, xVDate, xVTYPE, xVNo, xBookType, xBookSubType)				
        End If
    End Sub
    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtDateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub FormatSprdMain(ByRef Arow As Integer)
        Dim MainClass_Renamed As Object
        Dim cntCol As Integer
        With SprdMain
            .MaxCols = ColMKEY
            .set_RowHeight(0, RowHeight * 1.5)
            .set_ColWidth(0, 3)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColIntimationDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColIntimationDate, 10)

            .Col = ColMachName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMachName, 22)

            .Col = ColSurveyorName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColSurveyorName, 22)

            .Col = ColRefNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColRefNo, 12)
            .ColsFrozen = ColRefNo

            .Col = ColBillNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBillNo, 10)

            .Col = ColBillDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBillDate, 12)

            .Col = colSupplier
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(colSupplier, 25)

            .Col = ColBillAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColBillAmount, 10)

            .Col = ColChqNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColChqNo, 20)

            .Col = ColSettledAmt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColSettledAmt, 10)

            .Col = ColDiff
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColDiff, 10)

            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMKEY, 8)
            .ColHidden = True


            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '''OperationModeSingle				
            SprdMain.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Function Show1() As Boolean
        Dim MainClass_Renamed As Object
        On Error GoTo LedgError
        Dim RsOP As ADODB.Recordset
        Dim mOpening As Double
        Dim mOpDr As Double
        Dim mOpCr As Double
        Dim SqlStr As String


        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = MakeSQL()
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        '''********************************				
        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function MakeSQL() As String
        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset
        Dim SqlStr As String
        Dim I As Integer


        MakeSQL = ""
        ''''SELECT CLAUSE...				

        MakeSQL = " SELECT OUR_REF_DATE, MACH_NAME, SURVEYOR_NAME, " & vbCrLf _
            & " OUR_REF_NO, BILL_NO, " & vbCrLf _
            & " TO_CHAR(BILL_DATE,'DD/MM/YYYY'), " & vbCrLf _
            & " SUPPLIER, BILL_AMT, " & vbCrLf _
            & " DECODE(CHQ_NO,NULL,'',CHQ_NO || ' & ' || TO_CHAR(CHQ_DATE,'DD/MM/YYYY')), " & vbCrLf _
            & " TO_CHAR(SETTLED_AMOUNT) AS SETTLED_AMOUNT, " & vbCrLf _
            & " TO_CHAR(CLAIM_AMOUNT-SETTLED_AMOUNT) AS SETTLED_AMOUNT, " & vbCrLf _
            & " IH.MKEY"


        ''''FROM CLAUSE...				
        MakeSQL = MakeSQL & vbCrLf _
            & " FROM " & vbCrLf _
            & " DOC_INS_CLAIM_HDR IH, DOC_INS_CLAIM_DET ID"

        ''''WHERE CLAUSE...				
        MakeSQL = MakeSQL & vbCrLf _
            & " WHERE " & vbCrLf _
            & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.MKEY=ID.MKEY(+)"

        ''& " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _				
        '				
        MakeSQL = MakeSQL & vbCrLf _
            & " AND IH.REF_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND IH.REF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If OptStatus(1).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " AND STATUS='Y'"
        ElseIf OptStatus(2).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " AND STATUS='N'"
        End If

        ''''ORDER CLAUSE...				

        MakeSQL = MakeSQL & vbCrLf & "ORDER BY IH.OUR_REF_NO,IH.OUR_REF_DATE"

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Sub ReportonST(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mSubTitle1 As String
        Dim mHeading As String


        Report1.Reset()
        mTitle = Me.Text
        mSubTitle = ""
        mHeading = VB6.Format(txtDateTo.Text, "DD/MM/YYYY")

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\InsClaim.RPT"

        SqlStr = MakeSQL()
        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mHeading)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        'Resume				
    End Sub

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef pHeading As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
    End Sub
    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then txtDateFrom.SetFocus				
        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        '    If FYChk(CDate(txtDateTo.Text)) = False Then txtDateTo.SetFocus				
        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Private Sub CalcSprdTotal()
        Dim MainClass_Renamed As Object
        On Error GoTo ErrPart
        Dim cntRow As Integer

        Dim mClaimAmount As Double
        Dim mSettledAmount As Double
        Dim mDiffAmount As Double

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColBillAmount
                mClaimAmount = mClaimAmount + CDbl(IIf(IsNumeric(.Text), .Text, 0))

                .Col = ColSettledAmt
                mSettledAmount = mSettledAmount + CDbl(IIf(IsNumeric(.Text), .Text, 0))

                .Col = ColDiff
                mDiffAmount = mDiffAmount + CDbl(IIf(IsNumeric(.Text), .Text, 0))
            Next

            Call MainClass.AddBlankfpSprdRow(SprdMain, ColIntimationDate)
            .Col = colSupplier
            .Row = .MaxRows
            .Text = "GRAND TOTAL :"
            .Font = VB6.FontChangeBold(.Font, True)

            .Row = .MaxRows
            .Row2 = .MaxRows
            .Col = 1
            .Col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80				
            .BlockMode = False

            .Row = .MaxRows

            .Col = ColBillAmount
            .Text = VB6.Format(mClaimAmount, "0.00")

            .Col = ColSettledAmt
            .Text = VB6.Format(mSettledAmount, "0.00")

            .Col = ColDiff
            .Text = VB6.Format(mDiffAmount, "0.00")
        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtdateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateFrom) = False Then
            txtDateFrom.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then				
        '        txtDateFrom.SetFocus				
        '        Cancel = True				
        '        Exit Sub				
        '    End If				
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtDateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateTo) = False Then
            txtDateTo.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        '    If FYChk(CDate(txtDateTo.Text)) = False Then				
        '        txtDateTo.SetFocus				
        '        Cancel = True				
        '        Exit Sub				
        '    End If				
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
