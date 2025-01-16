Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamShearingProdReg
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20


    Private Const ColRefNo As Short = 1
    Private Const ColRefDate As Short = 2
    Private Const ColProdDate As Short = 3
    Private Const ColDeptCode As Short = 4
    Private Const ColShift As Short = 5

    Private Const ColEmpCode As Short = 6
    Private Const ColRMCode As Short = 7
    Private Const ColRMDesc As Short = 8
    Private Const ColRMUOM As Short = 9
    Private Const ColRMQty As Short = 10
    Private Const ColRMThickness As Short = 11
    Private Const ColRMSQM As Short = 12
    Private Const ColRMNetWt As Short = 13
    Private Const ColCTLNetWt As Short = 14
    Private Const ColScrapNetWt As Short = 15

    Private Const ColSFCode As Short = 16
    Private Const ColSFSDesc As Short = 17
    Private Const ColSFUOM As Short = 18
    Private Const ColSFQty As Short = 19
    Private Const ColSFSQM As Short = 20
    Private Const ColSFScrapQty As Short = 21
    Private Const ColSFScrapSQM As Short = 22

    Private Const ColScrapWt As Short = 23

    Private Const ColCTLArea As Short = 24
    Private Const ColScrapArea As Short = 25

    Private Const ColSFNetWt As Short = 26
    Private Const ColSFCutLenWt As Short = 27
    Private Const ColBlockCode As Short = 28
    Private Const ColBlockArea As Short = 29
    Private Const ColRemarks As Short = 30
    Private Const ColCompanyName As Short = 31
    Private Const ColLocked As Short = 32
    Private Const ColDivCode As Short = 33
    Private Const ColProduction As Short = 34
    Private Const ColMKEY As Short = 35


    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Dim mClickProcess As Boolean

    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        Call PrintStatus(False)
        If ChkALL.CheckState = System.Windows.Forms.CheckState.Checked Then
            TxtItemName.Enabled = False
            cmdSearch.Enabled = False
        Else
            TxtItemName.Enabled = True
            cmdSearch.Enabled = True
        End If
    End Sub

    Private Sub chkTime_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkTime.CheckStateChanged
        Call PrintStatus(False)
        If chkTime.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtTMFrom.Enabled = False
            txtTMTo.Enabled = False
        Else
            txtTMFrom.Enabled = True
            txtTMTo.Enabled = True
        End If
        txtTMFrom.Text = GetServerTime
        txtTMTo.Text = GetServerTime
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnIssue(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportOnIssue(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportOnIssue(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String


        Report1.Reset()

        mSubTitle = "From : " & VB6.Format(txtDateFrom.Text, "DD/MM/YYYY") & " To : " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY")

        If OptDetSumm(0).Checked = True Then
            mTitle = "Production Shearing Register " & " [ Detailed ] "
        ElseIf OptDetSumm(1).Checked = True Then
            mTitle = "Production Shearing Register " & " [ Summaried RM Wise ] "
        ElseIf OptDetSumm(2).Checked = True Then
            mTitle = "Production Shearing Register " & " [ Summaried SF Wise ] "
        End If

        mTitle = mTitle & "[" & cboDept.Text & "]" & "[" & cboShift.Text & "]"
        '        If OptOrderBy(0).Value = True Then	
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\ProdShearingReg.rpt"

        SqlStr = ""
        PubDBCn.Execute("DELETE FROM TEMP_PRINTDUMMYDATA WHERE UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'")

        MainClass.ClearCRptFormulas(Report1)

        Call InsertIntoPrintdummyData()

        '*************** Fetching Record For Report ***************************	
        SqlStr = ""
        SqlStr = "SELECT * " & vbCrLf _
            & " FROM TEMP_PRINTDUMMYDATA " & vbCrLf _
            & " WHERE UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf _
            & " ORDER BY SUBROW"

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub InsertIntoPrintdummyData()

        Dim SqlStr As String = ""
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mInsertSQL As String
        Dim mValueSQL As String
        Dim mFieldStr As String
        Dim mValueStr As String

        On Error GoTo ERR1

        PubDBCn.Errors.Clear()

        PubDBCn.BeginTrans()

        SqlStr = ""
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                mInsertSQL = ""
                mValueSQL = ""
                SqlStr = ""

                mInsertSQL = "Insert into TEMP_PrintDummyData (UserID,SubRow,"
                mValueSQL = " Values (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " " & cntRow & ", "


                For cntCol = 1 To .MaxCols
                    .Col = cntCol

                    If cntCol = .MaxCols Then
                        mFieldStr = "FIELD" & cntCol
                        mValueStr = "'" & MainClass.AllowSingleQuote(VB.Left(.Text, 255)) & "'"
                    Else
                        mFieldStr = "FIELD" & cntCol & ","
                        mValueStr = "'" & MainClass.AllowSingleQuote(VB.Left(.Text, 255)) & "'" & ","
                    End If

                    mInsertSQL = mInsertSQL & mFieldStr
                    mValueSQL = mValueSQL & mValueStr


                Next
                mInsertSQL = mInsertSQL & ")"
                mValueSQL = mValueSQL & ")"

                SqlStr = mInsertSQL & vbCrLf & mValueSQL
                PubDBCn.Execute(SqlStr)
            Next
        End With
        PubDBCn.CommitTrans()
        Exit Sub
ERR1:
        'Resume	
        PubDBCn.RollbackTrans()
        MsgInformation(Err.Description)
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchItem()
    End Sub
    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        Dim mCurrRow As Integer
        If FieldsVerification = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        If Show1 = False Then GoTo ErrPart
        Call PrintStatus(True)

        ''MainClass.SetFocusToCell SprdMain, mActiveRow, 4	

        If OptOrderBy(0).Checked = True And OptDetSumm(0).Checked = True Then
            'SprdMain.MaxRows = SprdMain.MaxRows + 1
            'mCurrRow = SprdMain.MaxRows

            'SprdMain.Row = mCurrRow
            'SprdMain.Col = ColRMDesc
            'SprdMain.Text = "TOTAL :"
            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
                Call SubTotal()
            Else
                SprdMain.MaxRows = SprdMain.MaxRows + 1
                mCurrRow = SprdMain.MaxRows

                SprdMain.Row = mCurrRow
                SprdMain.Col = ColRMDesc
                SprdMain.Text = "TOTAL :"

                Call CalcRowTotal(SprdMain, ColRMNetWt, 1, ColRMNetWt, mCurrRow - 1, mCurrRow, ColRMNetWt)

                Call CalcRowTotal(SprdMain, ColCTLNetWt, 1, ColCTLNetWt, mCurrRow - 1, mCurrRow, ColCTLNetWt)
                Call CalcRowTotal(SprdMain, ColScrapNetWt, 1, ColScrapNetWt, mCurrRow - 1, mCurrRow, ColScrapNetWt)
                Call CalcRowTotal(SprdMain, ColSFQty, 1, ColSFQty, mCurrRow - 1, mCurrRow, ColSFQty)
                Call CalcRowTotal(SprdMain, ColSFSQM, 1, ColSFSQM, mCurrRow - 1, mCurrRow, ColSFSQM)
                Call CalcRowTotal(SprdMain, ColSFScrapQty, 1, ColSFScrapQty, mCurrRow - 1, mCurrRow, ColSFScrapQty)
                Call CalcRowTotal(SprdMain, ColSFScrapSQM, 1, ColSFScrapSQM, mCurrRow - 1, mCurrRow, ColSFScrapSQM)
                Call CalcRowTotal(SprdMain, ColScrapWt, 1, ColScrapWt, mCurrRow - 1, mCurrRow, ColScrapWt)
                Call CalcRowTotal(SprdMain, ColCTLArea, 1, ColCTLArea, mCurrRow - 1, mCurrRow, ColCTLArea)
                Call CalcRowTotal(SprdMain, ColScrapArea, 1, ColScrapArea, mCurrRow - 1, mCurrRow, ColScrapArea)
                Call CalcRowTotal(SprdMain, ColSFNetWt, 1, ColSFNetWt, mCurrRow - 1, mCurrRow, ColSFNetWt)
                Call CalcRowTotal(SprdMain, ColSFCutLenWt, 1, ColSFCutLenWt, mCurrRow - 1, mCurrRow, ColSFCutLenWt)
            End If

        Else
            SprdMain.MaxRows = SprdMain.MaxRows + 1
            mCurrRow = SprdMain.MaxRows

            SprdMain.Row = mCurrRow
            SprdMain.Col = ColRMDesc
            SprdMain.Text = "TOTAL :"

            Call CalcRowTotal(SprdMain, ColRMNetWt, 1, ColRMNetWt, mCurrRow - 1, mCurrRow, ColRMNetWt)

            Call CalcRowTotal(SprdMain, ColCTLNetWt, 1, ColCTLNetWt, mCurrRow - 1, mCurrRow, ColCTLNetWt)
            Call CalcRowTotal(SprdMain, ColScrapNetWt, 1, ColScrapNetWt, mCurrRow - 1, mCurrRow, ColScrapNetWt)
            Call CalcRowTotal(SprdMain, ColSFQty, 1, ColSFQty, mCurrRow - 1, mCurrRow, ColSFQty)
            Call CalcRowTotal(SprdMain, ColSFSQM, 1, ColSFSQM, mCurrRow - 1, mCurrRow, ColSFSQM)
            Call CalcRowTotal(SprdMain, ColSFScrapQty, 1, ColSFScrapQty, mCurrRow - 1, mCurrRow, ColSFScrapQty)
            Call CalcRowTotal(SprdMain, ColSFScrapSQM, 1, ColSFScrapSQM, mCurrRow - 1, mCurrRow, ColSFScrapSQM)
            Call CalcRowTotal(SprdMain, ColScrapWt, 1, ColScrapWt, mCurrRow - 1, mCurrRow, ColScrapWt)
            Call CalcRowTotal(SprdMain, ColCTLArea, 1, ColCTLArea, mCurrRow - 1, mCurrRow, ColCTLArea)
            Call CalcRowTotal(SprdMain, ColScrapArea, 1, ColScrapArea, mCurrRow - 1, mCurrRow, ColScrapArea)
            Call CalcRowTotal(SprdMain, ColSFNetWt, 1, ColSFNetWt, mCurrRow - 1, mCurrRow, ColSFNetWt)
            Call CalcRowTotal(SprdMain, ColSFCutLenWt, 1, ColSFCutLenWt, mCurrRow - 1, mCurrRow, ColSFCutLenWt)

        End If

        FormatSprdMain(-1)


        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SubTotal()
        On Error GoTo ERR1
        Dim mSFQty As Double
        Dim mSFSQM As Double
        Dim mSFScrapQty As Double
        Dim mSFScrapSQM As Double

        Dim cntRow As Integer
        Dim mSubRowCol As Boolean
        Dim mRefNo As Double


        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColSFCode
                If Trim(.Text) = "" Then
                    .Col = ColRefNo
                    mRefNo = Val(.Text)

                    mSFQty = 0
                    mSFSQM = 0
                    mSFScrapQty = 0
                    mSFScrapSQM = 0

                    If GroupSumQry(mRefNo, mSFQty, mSFSQM, mSFScrapQty, mSFScrapSQM) = True Then
                        .Col = ColSFQty
                        .Text = mSFQty

                        .Col = ColSFSQM
                        .Text = mSFSQM

                        .Col = ColSFScrapQty
                        .Text = mSFScrapQty

                        .Col = ColSFScrapSQM
                        .Text = mSFScrapSQM

                    End If

                    '.Row = cntRow
                    '.Col = ColRefNo
                    '.Text = "TOTAL :"
                    '.Font = VB6.FontChangeBold(.Font, True)
                    .Row = cntRow
                    .Row2 = cntRow
                    .Col = 1
                    .Col2 = .MaxCols
                    .BlockMode = True
                    .Font = VB6.FontChangeBold(.Font, True)
                    .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80
                    .BlockMode = False

                End If
            Next
        End With

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColProduction
                If Val(.Text) > 1 Then


                    '.Row = cntRow
                    '.Col = ColRefNo
                    '.Text = "TOTAL :"
                    '.Font = VB6.FontChangeBold(.Font, True)
                    .Row = cntRow
                    .Row2 = cntRow
                    .Col = 1
                    .Col2 = ColScrapNetWt
                    .BlockMode = True
                    .Text = ""
                    .BlockMode = False

                End If
            Next
        End With
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume
    End Sub
    Private Function GroupSumQry(ByRef mRefNo As Double, ByRef mSFQty As Double, ByRef mSFSQM As Double, ByRef mSFScrapQty As Double, ByRef mSFScrapSQM As Double) As Boolean
        On Error GoTo ViewTrialErr
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mCompanyName As String
        Dim mCompanyCode As String
        Dim mCompanyCodeStr As String
        ' GroupSumQry(mRefNo, , , , )

        SqlStr = ""

        SqlStr = " SELECT " & vbCrLf _
            & " SUM(IGD.SF_QTY) AS SF_QTY, SUM(ROUND(((IGD.SF_WIDTH/1000) * (IGD.SFLENGTH/1000)) * IGD.SF_QTY,2)) AS SF_SQM, " & vbCrLf _
            & " SUM(IGD.SCRAP_QTY) SF_SCRAP_QTY, SUM(ROUND(((IGD.SF_WIDTH/1000) * (IGD.SFLENGTH/1000)) * IGD.SCRAP_QTY,2)) AS SF_SCRAP_SQM, "

        SqlStr = SqlStr & vbCrLf & " MAX(BLOCK_AREA) AS BLOCK_AREA"

        ''FROM CLAUSE...	
        SqlStr = SqlStr & vbCrLf _
            & " FROM PRD_CUTTINGPLAN_HDR IGH, PRD_CUTTINGPLAN_DET IGD"


        ''WHERE CLAUSE...	
        SqlStr = SqlStr & vbCrLf _
            & " WHERE IGH.AUTO_KEY_REF=IGD.AUTO_KEY_REF"

        SqlStr = SqlStr & vbCrLf & " And IGH.AUTO_KEY_REF = " & mRefNo & ""


        Dim mSFItemCode As String = ""

        If chkAllSF.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtSFItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSFItemCode = MasterNo
                SqlStr = SqlStr & vbCrLf & "And IGD.SF_CODE='" & MainClass.AllowSingleQuote(mSFItemCode) & "'"
            End If
        End If


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then

            mSFSQM = IIf(IsDBNull(RsTemp.Fields("BLOCK_AREA").Value), 0, RsTemp.Fields("BLOCK_AREA").Value)
            mSFQty = IIf(mSFSQM > 0, 1, 0)

            mSFQty = mSFQty + IIf(IsDBNull(RsTemp.Fields("SF_QTY").Value), 0, RsTemp.Fields("SF_QTY").Value)
            mSFSQM = mSFSQM + IIf(IsDBNull(RsTemp.Fields("SF_SQM").Value), 0, RsTemp.Fields("SF_SQM").Value)
            mSFScrapQty = IIf(IsDBNull(RsTemp.Fields("SF_SCRAP_QTY").Value), 0, RsTemp.Fields("SF_SCRAP_QTY").Value)
            mSFScrapSQM = IIf(IsDBNull(RsTemp.Fields("SF_SCRAP_SQM").Value), 0, RsTemp.Fields("SF_SCRAP_SQM").Value)

        End If

        GroupSumQry = True
        Exit Function

ViewTrialErr:
        GroupSumQry = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Function
    Private Sub frmParamShearingProdReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Me.Text = "Production Register " ''& IIf(lblApproval.text = "N", " - Approval", "")	

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormatSprdMain(-1)
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamShearingProdReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)

        lblTrnType.Text = CStr(-1)

        ChkALL.CheckState = System.Windows.Forms.CheckState.Checked
        TxtItemName.Enabled = False
        cmdSearch.Enabled = False

        chkAllSF.CheckState = System.Windows.Forms.CheckState.Checked
        TxtSFItemName.Enabled = False
        cmdsearchSF.Enabled = False

        Call FillIssueCombo()

        Call PrintStatus(True)
        'Call FillPOCombo	
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamShearingProdReg_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
    Private Sub frmParamShearingProdReg_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub


    Private Sub OptDetSumm_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptDetSumm.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptDetSumm.GetIndex(eventSender)
            OptOrderBy(0).Text = IIf(Index = 0, "Ref No.", "Item Code")
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

        Dim SqlStr As String = ""
        Dim xIssueNo As Double

        If OptDetSumm(0).Checked = False Then Exit Sub

        SprdMain.Row = SprdMain.ActiveRow

        SprdMain.Col = ColRefNo
        xIssueNo = Val(SprdMain.Text)



        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 110 Then
            XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, "MNUPRODUCTIONCUTTINGPLAN", PubDBCn)
            If InStr(1, XRIGHT, "M", CompareMethod.Text) = 0 Then
                Exit Sub
            End If

            FrmPMemoCuttingPlan.MdiParent = Me.MdiParent
            FrmPMemoCuttingPlan.lblBookType.Text = "P"
            FrmPMemoCuttingPlan.Show()

            FrmPMemoCuttingPlan.FrmPMemoCuttingPlan_Activated(Nothing, New System.EventArgs())

            FrmPMemoCuttingPlan.txtPMemoNo.Text = CStr(xIssueNo)

            FrmPMemoCuttingPlan.txtPMemoNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
        Else
            XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, "MNUPRODDIACUTTINGPLAN", PubDBCn)
            If InStr(1, XRIGHT, "M", CompareMethod.Text) = 0 Then
                Exit Sub
            End If

            FrmPMemoDiaCuttingPlan.MdiParent = Me.MdiParent
            FrmPMemoDiaCuttingPlan.lblBookType.Text = "R"
            FrmPMemoDiaCuttingPlan.Show()

            FrmPMemoDiaCuttingPlan.FrmPMemoDiaCuttingPlan_Activated(Nothing, New System.EventArgs())

            FrmPMemoDiaCuttingPlan.txtPMemoNo.Text = CStr(xIssueNo)

            FrmPMemoDiaCuttingPlan.txtPMemoNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))

        End If

    End Sub


    Private Sub txtItemName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtItemName.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtItemName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtItemName.DoubleClick
        SearchItem()
    End Sub
    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtDateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub SearchItem()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.SearchGridMaster(TxtItemName.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr)
        If AcName <> "" Then
            TxtItemName.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub txtItemName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtItemName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtItemName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtItemName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtItemName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchItem()
    End Sub
    Private Sub txtItemName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtItemName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        lblAcCode.Text = ""
        If TxtItemName.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable(TxtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            lblAcCode.text = MasterNo
            TxtItemName.Text = UCase(Trim(TxtItemName.Text))
        Else
            lblAcCode.text = ""
            MsgInformation("No Such Item in Item Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        Dim cntCol As Integer
        With SprdMain
            .MaxCols = ColMKEY
            .set_RowHeight(0, RowHeight * 1.2)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColLocked
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColLocked, 15)
            .ColHidden = True

            .Col = ColRefNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColRefNo, 9)
            If OptDetSumm(0).Checked = True Then
                .ColHidden = False
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted      'MergeRestricted
            Else
                .ColHidden = True
            End If

            .Col = ColRefDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColRefDate, 9)
            If OptDetSumm(0).Checked = True Then
                .ColHidden = False
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted
            Else
                .ColHidden = True
            End If




            .Col = ColProdDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColProdDate, 9)
            If OptDetSumm(0).Checked = True Then
                .ColHidden = False
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted
            Else
                .ColHidden = True
            End If

            .Col = ColDeptCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColDeptCode, 4)
            .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

            .Col = ColShift
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColShift, 4)
            If OptDetSumm(0).Checked = True Then
                .ColHidden = False
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted
            Else
                .ColHidden = True
            End If

            .Col = ColDivCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColDivCode, 8)
            If OptDetSumm(0).Checked = True Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            .Col = ColEmpCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColEmpCode, 8)
            If OptDetSumm(0).Checked = True Then
                .ColHidden = False
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted
            Else
                .ColHidden = True
            End If

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColRemarks, 8)
            If OptDetSumm(0).Checked = True Then
                .ColHidden = False
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted
            Else
                .ColHidden = True
            End If

            .Col = ColRMCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColRMCode, 8)
            .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted
            'If OptDetSumm(0).Checked = True Then
            '    .ColHidden = False
            'ElseIf OptDetSumm(1).Checked = True Then
            '    .ColHidden = True
            'End If

            .Col = ColRMDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted
            'If OptDetSumm(0).Checked = True Then
            '    .set_ColWidth(ColRMDesc, 22)
            'Else
            '    .set_ColWidth(ColRMDesc, 25)
            'End If

            .Col = ColRMUOM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColRMUOM, 4)
            .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

            For cntCol = ColRMQty To ColScrapNetWt
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 3
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 8)
                'If OptDetSumm(0).Checked = True Then
                .ColHidden = False
                'ElseIf OptDetSumm(1).Checked = True Then
                '    .ColHidden = False
                'End If
            Next

            .Col = ColRMQty
            .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

            .Col = ColRMThickness
            .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

            .Col = ColRMSQM
            .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

            .Col = ColRMNetWt
            .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

            .Col = ColProduction
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColProduction, 8)
            'If OptDetSumm(0).Checked = True Then
            '    .ColHidden = False
            'Else
            .ColHidden = True
            'End If


            .Col = ColSFCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColSFCode, 8)
            If OptDetSumm(1).Checked = True Then
                .ColHidden = True
            Else
                .ColHidden = False
            End If

            .Col = ColSFSDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            If OptDetSumm(1).Checked = True Then
                .ColHidden = True
            Else
                .ColHidden = False
            End If

            .Col = ColSFUOM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColSFUOM, 4)
            If OptDetSumm(1).Checked = True Then
                .ColHidden = True
            Else
                .ColHidden = False
            End If

            For cntCol = ColSFQty To ColScrapWt
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 3
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 8)
                If OptDetSumm(1).Checked = True Then
                    .ColHidden = True
                Else
                    .ColHidden = False
                End If
            Next

            For cntCol = ColCTLArea To ColScrapArea
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 3
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 8)
            Next

            .Col = ColBlockCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBlockCode, 12)
            .ColHidden = True

            .Col = ColBlockArea
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColBlockArea, 8)
            .ColHidden = True

            .Col = ColCompanyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColCompanyName, 12)

            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMKEY, 8)
            .ColHidden = True

            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then

                .Col = ColRMNetWt
                .ColHidden = True
                .Col = ColCTLNetWt
                .ColHidden = True
                .Col = ColScrapNetWt
                .ColHidden = True
                .Col = ColSFNetWt
                .ColHidden = True
                .Col = ColSFCutLenWt
                .ColHidden = True
                .Col = ColScrapWt
                .ColHidden = True
                .Col = ColBlockCode
                .ColHidden = IIf(OptDetSumm(0).Checked = True, True, False)
                .Col = ColBlockArea
                .ColHidden = IIf(OptDetSumm(0).Checked = True, True, False)
            End If


            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ' = OperationModeSingle	
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Function Show1() As Boolean

        On Error GoTo LedgError
        Dim SqlStr As String = ""


        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If OptDetSumm(0).Checked = True Then
            SqlStr = MakeSQLDet
        Else
            SqlStr = MakeSQLSumm
        End If
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        '********************************	
        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function MakeSQLDet() As String

        On Error GoTo ERR1
        Dim mDept As String
        Dim mOPRCode As String
        Dim mItemCode As String
        Dim mSupplier As String
        Dim mEmployee As String
        Dim mCompanyCodeStr As String
        Dim mCompanyName As String
        Dim mCompanyCode As String
        Dim pCompanyCodeStr As String

        If lstCompanyName.GetItemChecked(0) = True Then
            mCompanyCodeStr = ""
        Else
            For CntLst = 1 To lstCompanyName.Items.Count - 1
                If lstCompanyName.GetItemChecked(CntLst) = True Then
                    mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
                    If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_SHORTNAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                        mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & "," & mCompanyCode)
                End If
            Next
        End If

        'If mCompanyCodeStr <> "" Then
        '    mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
        'End If

        MakeSQLDet = " SELECT " & vbCrLf _
            & " AUTO_KEY_REF," & vbCrLf _
            & " REF_DATE,  PROD_DATE," & vbCrLf _
            & " DEPT_CODE, SHIFT_CODE, EMP_CODE,  " & vbCrLf _
            & " RM_CODE, RM_ITEM_SHORT_DESC, RM_UOM, RM_QTY, " & vbCrLf _
            & " RM_THICKNESS, RM_SQM," & vbCrLf _
            & " RM_NET_WT, CTL_NET_WT, SCRAP_NET_WT,  " & vbCrLf _
            & " SF_CODE, SF_ITEM_SHORT_DESC, SF_UOM, " & vbCrLf _
            & " SF_QTY, SF_SQM, " & vbCrLf _
            & " SF_SCRAP_QTY, SF_SCRAP_SQM, " & vbCrLf _
            & " SCRAP_QTY,"

        MakeSQLDet = MakeSQLDet & vbCrLf _
            & " CTL_AREA, SCRAP_AREA,"

        MakeSQLDet = MakeSQLDet & vbCrLf _
            & " SF_NET_WT, CUT_LEN_WT, RM_BLOCK_CODE, BLOCK_AREA,"

        '-
        MakeSQLDet = MakeSQLDet & vbCrLf _
            & " REMARKS,COMPANY_SHORTNAME,PROD_TYPE, DIV_DESC, SERIAL_NO,  MKEY FROM ("

        MakeSQLDet = MakeSQLDet & vbCrLf _
            & " SELECT " & vbCrLf _
            & " IGH.AUTO_KEY_REF," & vbCrLf _
            & " TO_CHAR(IGH.REF_DATE,'DD/MM/YYYY') REF_DATE,  TO_CHAR(IGH.PROD_DATE,'DD/MM/YYYY') AS PROD_DATE," & vbCrLf _
            & " IGH.DEPT_CODE, IGH.SHIFT_CODE, IGH.EMP_CODE,  " & vbCrLf _
            & " IGH.RM_CODE, RMMST.ITEM_SHORT_DESC RM_ITEM_SHORT_DESC, IGH.RM_UOM, IGH.RM_QTY, " & vbCrLf _
            & " IGH.RM_THICKNESS, ROUND(((IGH.RM_LENGTH/1000) * (IGH.RM_WIDTH/1000)) * IGH.RM_QTY,2) AS RM_SQM," & vbCrLf _
            & " IGH.RM_NET_WT, IGH.CTL_NET_WT, IGH.SCRAP_NET_WT,  " & vbCrLf _
            & " IGD.SF_CODE, SFMST.ITEM_SHORT_DESC SF_ITEM_SHORT_DESC, IGD.SF_UOM, " & vbCrLf _
            & " IGD.SF_QTY, ROUND(((IGD.SF_WIDTH/1000) * (IGD.SFLENGTH/1000)) * IGD.SF_QTY,2) AS SF_SQM, " & vbCrLf _
            & " IGD.SCRAP_QTY SF_SCRAP_QTY, ROUND(((IGD.SF_WIDTH/1000) * (IGD.SFLENGTH/1000)) * IGD.SCRAP_QTY,2) AS SF_SCRAP_SQM, " & vbCrLf _
            & " NVL(DECODE(IGH.RM_NET_WT + IGH.CTL_NET_WT,0,0,(SELECT SUM(SCRAP_QTY) FROM PRD_CUTTINGPLAN_EXP WHERE AUTO_KEY_REF=IGH.AUTO_KEY_REF) * (IGD.SF_NET_WT+ IGD.CUT_LEN_WT)/(IGH.RM_NET_WT+IGH.CTL_NET_WT)),0) AS SCRAP_QTY,"

        MakeSQLDet = MakeSQLDet & vbCrLf _
            & " CTL_AREA As CTL_AREA, RM_AREA-CTL_AREA AS SCRAP_AREA,"

        MakeSQLDet = MakeSQLDet & vbCrLf _
            & " IGD.SF_NET_WT, IGD.CUT_LEN_WT, RM_BLOCK_CODE, BLOCK_AREA,"

        '-
        MakeSQLDet = MakeSQLDet & vbCrLf _
            & " IGH.REMARKS,GEN.COMPANY_SHORTNAME,IGH.PROD_TYPE, DIV.DIV_DESC, IGD.SERIAL_NO,  IGH.AUTO_KEY_REF MKEY"

        ''FROM CLAUSE...	
        MakeSQLDet = MakeSQLDet & vbCrLf _
            & " FROM PRD_CUTTINGPLAN_HDR IGH, PRD_CUTTINGPLAN_DET IGD," & vbCrLf _
            & " INV_ITEM_MST RMMST, INV_ITEM_MST SFMST, INV_DIVISION_MST DIV, GEN_COMPANY_MST GEN"


        ''WHERE CLAUSE...	
        MakeSQLDet = MakeSQLDet & vbCrLf _
            & " WHERE IGH.COMPANY_CODE=GEN.COMPANY_CODE" & vbCrLf _
            & " And IGH.AUTO_KEY_REF=IGD.AUTO_KEY_REF" & vbCrLf _
            & " And IGH.COMPANY_CODE=RMMST.COMPANY_CODE" & vbCrLf _
            & " And IGH.RM_CODE=RMMST.ITEM_CODE" & vbCrLf _
            & " And IGD.COMPANY_CODE=SFMST.COMPANY_CODE" & vbCrLf _
            & " And IGD.SF_CODE=SFMST.ITEM_CODE"

        MakeSQLDet = MakeSQLDet & vbCrLf _
            & " And IGH.COMPANY_CODE=DIV.COMPANY_CODE" & vbCrLf _
            & " And IGH.DIV_CODE=DIV.DIV_CODE "


        'If lstCompanyName.GetItemChecked(0) = True Then
        '    mCompanyCodeStr = ""
        'Else
        '    For CntLst = 1 To lstCompanyName.Items.Count - 1
        '        If lstCompanyName.GetItemChecked(CntLst) = True Then
        '            mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
        '            If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_SHORTNAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
        '                mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
        '            End If
        '            mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & "," & mCompanyCode)
        '        End If
        '    Next
        'End If

        If mCompanyCodeStr <> "" Then
            pCompanyCodeStr = "(" & mCompanyCodeStr & ")"
            MakeSQLDet = MakeSQLDet & vbCrLf & " And GEN.COMPANY_CODE IN " & pCompanyCodeStr & ""
        End If

        Dim mSFItemCode As String = ""

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = MasterNo
                MakeSQLDet = MakeSQLDet & vbCrLf & "And IGH.RM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
            End If
        End If

        If chkAllSF.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtSFItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSFItemCode = MasterNo
                MakeSQLDet = MakeSQLDet & vbCrLf & "And IGD.SF_CODE='" & MainClass.AllowSingleQuote(mSFItemCode) & "'"
            End If
        End If

        If cboDept.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDept = MasterNo
                MakeSQLDet = MakeSQLDet & vbCrLf & "AND IGH.DEPT_CODE='" & MainClass.AllowSingleQuote(mDept) & "'"
            End If
        End If

        If cboShift.Text <> "ALL" Then
            MakeSQLDet = MakeSQLDet & vbCrLf & "AND IGH.SHIFT_CODE='" & VB.Left(cboShift.Text, 1) & "'"
        End If

        If optDate(0).Checked = True Then
            MakeSQLDet = MakeSQLDet & vbCrLf & " AND IGH.PROD_DATE >=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IGH.PROD_DATE <=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            MakeSQLDet = MakeSQLDet & vbCrLf & " AND IGH.REF_DATE >=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IGH.REF_DATE <=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        If chkTime.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQLDet = MakeSQLDet & vbCrLf & " AND IGH.PREP_TIME >=TO_DATE('" & txtTMFrom.Text & "', 'HH24:MI')" & vbCrLf & " AND IGH.PREP_TIME <=TO_DATE('" & txtTMTo.Text & "', 'HH24:MI')"
        End If

        MakeSQLDet = MakeSQLDet & " UNION ALL"

        MakeSQLDet = MakeSQLDet & vbCrLf _
            & " SELECT " & vbCrLf _
            & " IGH.AUTO_KEY_REF," & vbCrLf _
            & " TO_CHAR(IGH.REF_DATE,'DD/MM/YYYY') REF_DATE,  TO_CHAR(IGH.PROD_DATE,'DD/MM/YYYY') AS PROD_DATE," & vbCrLf _
            & " IGH.DEPT_CODE, IGH.SHIFT_CODE, IGH.EMP_CODE,  " & vbCrLf _
            & " IGH.RM_CODE, RMMST.ITEM_SHORT_DESC RM_ITEM_SHORT_DESC, IGH.RM_UOM, IGH.RM_QTY, " & vbCrLf _
            & " IGH.RM_THICKNESS, ROUND(((IGH.RM_LENGTH/1000) * (IGH.RM_WIDTH/1000)) * IGH.RM_QTY,2) AS RM_SQM," & vbCrLf _
            & " IGH.RM_NET_WT, IGH.CTL_NET_WT, IGH.SCRAP_NET_WT,  " & vbCrLf _
            & " RM_BLOCK_CODE SF_CODE, SFMST.ITEM_SHORT_DESC SF_ITEM_SHORT_DESC, SFMST.ISSUE_UOM SF_UOM, " & vbCrLf _
            & " 1 SF_QTY, BLOCK_AREA AS SF_SQM, " & vbCrLf _
            & " 0 SF_SCRAP_QTY, 0 AS SF_SCRAP_SQM, " & vbCrLf _
            & " 0 AS SCRAP_QTY,"

        MakeSQLDet = MakeSQLDet & vbCrLf _
            & " CTL_AREA As CTL_AREA, RM_AREA-CTL_AREA AS SCRAP_AREA,"

        MakeSQLDet = MakeSQLDet & vbCrLf _
            & " 0 SF_NET_WT, 0 CUT_LEN_WT, RM_BLOCK_CODE, BLOCK_AREA,"

        '-
        MakeSQLDet = MakeSQLDet & vbCrLf _
            & " IGH.REMARKS,GEN.COMPANY_SHORTNAME,IGH.PROD_TYPE, DIV.DIV_DESC, 999 AS SERIAL_NO,  IGH.AUTO_KEY_REF MKEY"

        ''FROM CLAUSE...	
        MakeSQLDet = MakeSQLDet & vbCrLf _
            & " FROM PRD_CUTTINGPLAN_HDR IGH, " & vbCrLf _
            & " INV_ITEM_MST RMMST, INV_ITEM_MST SFMST, INV_DIVISION_MST DIV, GEN_COMPANY_MST GEN"


        ''WHERE CLAUSE...	
        MakeSQLDet = MakeSQLDet & vbCrLf _
            & " WHERE IGH.COMPANY_CODE=GEN.COMPANY_CODE" & vbCrLf _
            & " And IGH.COMPANY_CODE=RMMST.COMPANY_CODE" & vbCrLf _
            & " And IGH.RM_CODE=RMMST.ITEM_CODE" & vbCrLf _
            & " And IGH.COMPANY_CODE=SFMST.COMPANY_CODE" & vbCrLf _
            & " And IGH.RM_BLOCK_CODE=SFMST.ITEM_CODE"

        MakeSQLDet = MakeSQLDet & vbCrLf _
            & " And IGH.COMPANY_CODE=DIV.COMPANY_CODE" & vbCrLf _
            & " And IGH.DIV_CODE=DIV.DIV_CODE "


        'If lstCompanyName.GetItemChecked(0) = True Then
        '    mCompanyCodeStr = ""
        'Else
        '    For CntLst = 1 To lstCompanyName.Items.Count - 1
        '        If lstCompanyName.GetItemChecked(CntLst) = True Then
        '            mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
        '            If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_SHORTNAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
        '                mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
        '            End If
        '            mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & "," & mCompanyCode)
        '        End If
        '    Next
        'End If

        If mCompanyCodeStr <> "" Then
            pCompanyCodeStr = "(" & mCompanyCodeStr & ")"
            MakeSQLDet = MakeSQLDet & vbCrLf & " And GEN.COMPANY_CODE IN " & pCompanyCodeStr & ""
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = MasterNo
                MakeSQLDet = MakeSQLDet & vbCrLf & "And IGH.RM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
            End If
        End If

        If chkAllSF.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtSFItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSFItemCode = MasterNo
                MakeSQLDet = MakeSQLDet & vbCrLf & "And IGH.RM_BLOCK_CODE ='" & MainClass.AllowSingleQuote(mSFItemCode) & "'"
            End If
        End If

        If cboDept.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDept = MasterNo
                MakeSQLDet = MakeSQLDet & vbCrLf & "AND IGH.DEPT_CODE='" & MainClass.AllowSingleQuote(mDept) & "'"
            End If
        End If

        If cboShift.Text <> "ALL" Then
            MakeSQLDet = MakeSQLDet & vbCrLf & "AND IGH.SHIFT_CODE='" & VB.Left(cboShift.Text, 1) & "'"
        End If

        If optDate(0).Checked = True Then
            MakeSQLDet = MakeSQLDet & vbCrLf & " AND IGH.PROD_DATE >=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IGH.PROD_DATE <=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            MakeSQLDet = MakeSQLDet & vbCrLf & " AND IGH.REF_DATE >=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IGH.REF_DATE <=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        If chkTime.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQLDet = MakeSQLDet & vbCrLf & " AND IGH.PREP_TIME >=TO_DATE('" & txtTMFrom.Text & "', 'HH24:MI')" & vbCrLf & " AND IGH.PREP_TIME <=TO_DATE('" & txtTMTo.Text & "', 'HH24:MI')"
        End If

        If OptOrderBy(0).Checked = True Then
            MakeSQLDet = MakeSQLDet & " UNION ALL"

            MakeSQLDet = MakeSQLDet & vbCrLf _
                & " SELECT " & vbCrLf _
                & " IGH.AUTO_KEY_REF," & vbCrLf _
                & " TO_CHAR(IGH.REF_DATE,'DD/MM/YYYY') REF_DATE,  TO_CHAR(IGH.PROD_DATE,'DD/MM/YYYY') AS PROD_DATE," & vbCrLf _
                & " IGH.DEPT_CODE, IGH.SHIFT_CODE, IGH.EMP_CODE,  " & vbCrLf _
                & " IGH.RM_CODE, RMMST.ITEM_SHORT_DESC RM_ITEM_SHORT_DESC, IGH.RM_UOM, IGH.RM_QTY, " & vbCrLf _
                & " IGH.RM_THICKNESS, ROUND(((IGH.RM_LENGTH/1000) * (IGH.RM_WIDTH/1000)) * IGH.RM_QTY,2) AS RM_SQM," & vbCrLf _
                & " IGH.RM_NET_WT, IGH.CTL_NET_WT, IGH.SCRAP_NET_WT,  " & vbCrLf _
                & " ' ' SF_CODE, 'SUB TOTAL' SF_ITEM_SHORT_DESC, '' SF_UOM, " & vbCrLf _
                & " 0 SF_QTY, 0 AS SF_SQM, " & vbCrLf _
                & " 0 SF_SCRAP_QTY, 0 AS SF_SCRAP_SQM, " & vbCrLf _
                & " 0 AS SCRAP_QTY,"

            MakeSQLDet = MakeSQLDet & vbCrLf _
                & " 0 As CTL_AREA, 0 AS SCRAP_AREA,"

            MakeSQLDet = MakeSQLDet & vbCrLf _
                & " 0 SF_NET_WT, 0 CUT_LEN_WT, '' RM_BLOCK_CODE, 0 BLOCK_AREA,"

            '-
            MakeSQLDet = MakeSQLDet & vbCrLf _
                & " '' REMARKS,GEN.COMPANY_SHORTNAME,IGH.PROD_TYPE, DIV.DIV_DESC, 9999 AS SERIAL_NO,  IGH.AUTO_KEY_REF MKEY"

            ''FROM CLAUSE...	
            MakeSQLDet = MakeSQLDet & vbCrLf _
                & " FROM PRD_CUTTINGPLAN_HDR IGH, " & vbCrLf _
                & " INV_ITEM_MST RMMST, INV_DIVISION_MST DIV, GEN_COMPANY_MST GEN"


            ''WHERE CLAUSE...	
            MakeSQLDet = MakeSQLDet & vbCrLf _
                & " WHERE IGH.COMPANY_CODE=GEN.COMPANY_CODE" & vbCrLf _
                & " And IGH.COMPANY_CODE=RMMST.COMPANY_CODE" & vbCrLf _
                & " And IGH.RM_CODE=RMMST.ITEM_CODE"

            MakeSQLDet = MakeSQLDet & vbCrLf _
                & " And IGH.COMPANY_CODE=DIV.COMPANY_CODE" & vbCrLf _
                & " And IGH.DIV_CODE=DIV.DIV_CODE "


            'If lstCompanyName.GetItemChecked(0) = True Then
            '    mCompanyCodeStr = ""
            'Else
            '    For CntLst = 1 To lstCompanyName.Items.Count - 1
            '        If lstCompanyName.GetItemChecked(CntLst) = True Then
            '            mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
            '            If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_SHORTNAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
            '                mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
            '            End If
            '            mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & "," & mCompanyCode)
            '        End If
            '    Next
            'End If

            If mCompanyCodeStr <> "" Then
                pCompanyCodeStr = "(" & mCompanyCodeStr & ")"
                MakeSQLDet = MakeSQLDet & vbCrLf & " And GEN.COMPANY_CODE IN " & pCompanyCodeStr & ""
            End If

            If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                If MainClass.ValidateWithMasterTable(TxtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mItemCode = MasterNo
                    MakeSQLDet = MakeSQLDet & vbCrLf & "And IGH.RM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
                End If
            End If

            If chkAllSF.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                If MainClass.ValidateWithMasterTable(TxtSFItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mSFItemCode = MasterNo
                    MakeSQLDet = MakeSQLDet & vbCrLf & "And IGH.RM_BLOCK_CODE ='" & MainClass.AllowSingleQuote(mSFItemCode) & "'"
                End If
            End If

            If cboDept.Text <> "ALL" Then
                If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDept = MasterNo
                    MakeSQLDet = MakeSQLDet & vbCrLf & "AND IGH.DEPT_CODE='" & MainClass.AllowSingleQuote(mDept) & "'"
                End If
            End If

            If cboShift.Text <> "ALL" Then
                MakeSQLDet = MakeSQLDet & vbCrLf & "AND IGH.SHIFT_CODE='" & VB.Left(cboShift.Text, 1) & "'"
            End If

            If optDate(0).Checked = True Then
                MakeSQLDet = MakeSQLDet & vbCrLf & " AND IGH.PROD_DATE >=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IGH.PROD_DATE <=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            Else
                MakeSQLDet = MakeSQLDet & vbCrLf & " AND IGH.REF_DATE >=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IGH.REF_DATE <=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If

            If chkTime.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                MakeSQLDet = MakeSQLDet & vbCrLf & " AND IGH.PREP_TIME >=TO_DATE('" & txtTMFrom.Text & "', 'HH24:MI')" & vbCrLf & " AND IGH.PREP_TIME <=TO_DATE('" & txtTMTo.Text & "', 'HH24:MI')"
            End If

        End If

        MakeSQLDet = MakeSQLDet & vbCrLf & ")"

        If OptOrderBy(0).Checked = True Then
            MakeSQLDet = MakeSQLDet & vbCrLf & "ORDER BY REF_DATE ,AUTO_KEY_REF, SERIAL_NO"  '
        ElseIf OptOrderBy(1).Checked = True Then
            MakeSQLDet = MakeSQLDet & vbCrLf & "ORDER BY RM_ITEM_SHORT_DESC, REF_DATE,AUTO_KEY_REF"
        End If
        'End If	
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function MakeSQLSumm() As String
        On Error GoTo ERR1
        Dim mDept As String
        Dim mOPRCode As String
        Dim mItemCode As String
        Dim mSupplier As String
        Dim mEmployee As String
        Dim mCompanyCodeStr As String
        Dim mCompanyName As String
        Dim mCompanyCode As String


        ''IGH.RM_QTY

        MakeSQLSumm = " SELECT " & vbCrLf _
            & " ''," & vbCrLf _
            & " '',  ''," & vbCrLf _
            & " IGH.DEPT_CODE, '',  ''," & vbCrLf _
            & " IGH.RM_CODE, RMMST.ITEM_SHORT_DESC, IGH.RM_UOM, SUM(IGH.RM_QTY) AS RM_QTY, " & vbCrLf _
            & " IGH.RM_THICKNESS, SUM(ROUND(((IGH.RM_LENGTH/1000) * (IGH.RM_WIDTH/1000)) * IGH.RM_QTY,2)) AS RM_SQM, " & vbCrLf _
            & " SUM(IGH.RM_NET_WT) AS RM_NET_WT, SUM(IGH.CTL_NET_WT) AS CTL_NET_WT, SUM(IGH.SCRAP_NET_WT) AS SCRAP_NET_WT,  "

        If OptDetSumm(1).Checked = True Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf _
                & " '' SF_CODE, '' ITEM_SHORT_DESC, '' SF_UOM, '' AS SF_QTY, '' AS SF_SQM," & vbCrLf _
                & " '' AS SF_SCRAP_QTY, '' AS SF_SCRAP_SQM," & vbCrLf _
                & "0 AS SCRAP_QTY,"

            MakeSQLSumm = MakeSQLSumm & vbCrLf _
            & " SUM(CTL_AREA) As CTL_AREA, SUM(RM_AREA-CTL_AREA) AS SCRAP_AREA, "

            '-BLOCK_AREA
            MakeSQLSumm = MakeSQLSumm & vbCrLf & " '' AS SF_NET_WT, '' CUT_LEN_WT, RM_BLOCK_CODE, BLOCK_AREA,"


        Else
            MakeSQLSumm = MakeSQLSumm & vbCrLf _
                & " IGD.SF_CODE, SFMST.ITEM_SHORT_DESC, IGD.SF_UOM,  " & vbCrLf _
                & " SUM(IGD.SF_QTY) AS SF_QTY, SUM(ROUND(((IGD.SF_WIDTH/1000) * (IGD.SFLENGTH/1000)) * IGD.SF_QTY,2)) AS SF_SQM," & vbCrLf _
                & " SUM(IGD.SCRAP_QTY) AS SF_SCRAP_QTY, SUM(ROUND(((IGD.SF_WIDTH/1000) * (IGD.SFLENGTH/1000)) * IGD.SCRAP_QTY,2)) AS SF_SCRAP_SQM," & vbCrLf _
                & " NVL(SUM(DECODE(IGH.RM_NET_WT + IGH.CTL_NET_WT,0,0,(SELECT SUM(SCRAP_QTY) FROM PRD_CUTTINGPLAN_EXP WHERE AUTO_KEY_REF=IGH.AUTO_KEY_REF) * (IGD.SF_NET_WT+ IGD.CUT_LEN_WT)/(IGH.RM_NET_WT+IGH.CTL_NET_WT))),0) AS SCRAP_QTY," & vbCrLf _
                & " SUM(CTL_AREA) As CTL_AREA, SUM(RM_AREA-CTL_AREA) AS SCRAP_AREA, " & vbCrLf _
                & " SUM(IGD.SF_NET_WT) AS SF_NET_WT, SUM(IGD.CUT_LEN_WT) CUT_LEN_WT, RM_BLOCK_CODE, BLOCK_AREA, "
        End If

        MakeSQLSumm = MakeSQLSumm & vbCrLf _
                & "'', GEN.COMPANY_SHORTNAME, IGH.PROD_TYPE, DIV.DIV_DESC,IGH.PRODUCTION_FROM,''"



        '-BLOCK_AREA




        'MakeSQLDet = " SELECT IGH.PROD_TYPE," & vbCrLf _
        '    & " IGH.AUTO_KEY_REF," & vbCrLf _
        '    & " TO_CHAR(IGH.REF_DATE,'DD/MM/YYYY'),  TO_CHAR(IGH.PROD_DATE,'DD/MM/YYYY')," & vbCrLf _
        '    & " IGH.DEPT_CODE, IGH.SHIFT_CODE, DIV.DIV_DESC, IGH.EMP_CODE, IGH.REMARKS, " & vbCrLf _
        '    & " IGH.RM_CODE, RMMST.ITEM_SHORT_DESC, IGH.RM_UOM, IGH.RM_QTY, " & vbCrLf _
        '    & " IGH.RM_THICKNESS, ROUND(((IGH.RM_LENGTH/1000) * (IGH.RM_WIDTH/1000)) * IGH.RM_QTY,2) AS RM_SQM," & vbCrLf _
        '    & " IGH.RM_NET_WT, IGH.CTL_NET_WT, IGH.SCRAP_NET_WT, IGH.PRODUCTION_FROM, " & vbCrLf _

        ''ROUND(((IGH.RM_LENGTH/1000) * (IGH.RM_WIDTH/1000)) * IGH.RM_QTY,2) AS RM_SQM,
        ''SUM(ROUND(((IGD.SF_WIDTH/1000) * (IGD.SFLENGTH/1000)) * IGD.SF_QTY,2)) AS SF_SQM


        '& " (SELECT SUM(SCRAP_QTY)/ FROM PRD_CUTTINGPLAN_EXP WHERE AUTO_KEY_REF=IGH.AUTO_KEY_REF) AS SCRAP_QTY, " & vbCrLf _  ''SCRAP_NET_WT
        '& " '' "

        ''FROM CLAUSE...	

        If OptDetSumm(1).Checked = True Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf _
                   & " FROM PRD_CUTTINGPLAN_HDR IGH, " & vbCrLf _
                   & " INV_ITEM_MST RMMST, INV_DIVISION_MST DIV, GEN_COMPANY_MST GEN"

            ''WHERE CLAUSE...	
            MakeSQLSumm = MakeSQLSumm & vbCrLf _
                & " WHERE IGH.COMPANY_CODE=GEN.COMPANY_CODE" & vbCrLf _
                & " --IGH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " And IGH.COMPANY_CODE=RMMST.COMPANY_CODE" & vbCrLf _
                & " And IGH.RM_CODE=RMMST.ITEM_CODE"

        Else
            MakeSQLSumm = MakeSQLSumm & vbCrLf _
                   & " FROM PRD_CUTTINGPLAN_HDR IGH, PRD_CUTTINGPLAN_DET IGD," & vbCrLf _
                   & " INV_ITEM_MST RMMST, INV_ITEM_MST SFMST, INV_DIVISION_MST DIV, GEN_COMPANY_MST GEN"

            ''WHERE CLAUSE...	
            MakeSQLSumm = MakeSQLSumm & vbCrLf _
                & " WHERE IGH.COMPANY_CODE=GEN.COMPANY_CODE" & vbCrLf _
                & " --IGH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " And IGH.AUTO_KEY_REF=IGD.AUTO_KEY_REF" & vbCrLf _
                & " And IGH.COMPANY_CODE=RMMST.COMPANY_CODE" & vbCrLf _
                & " And IGH.RM_CODE=RMMST.ITEM_CODE" & vbCrLf _
                & " And IGD.COMPANY_CODE=SFMST.COMPANY_CODE" & vbCrLf _
                & " And IGD.SF_CODE=SFMST.ITEM_CODE"

        End If





        MakeSQLSumm = MakeSQLSumm & vbCrLf _
            & " AND IGH.COMPANY_CODE=DIV.COMPANY_CODE" & vbCrLf _
            & " AND IGH.DIV_CODE=DIV.DIV_CODE "

        If lstCompanyName.GetItemChecked(0) = True Then
            mCompanyCodeStr = ""
        Else
            For CntLst = 1 To lstCompanyName.Items.Count - 1
                If lstCompanyName.GetItemChecked(CntLst) = True Then
                    mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
                    If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_SHORTNAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                        mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & "," & mCompanyCode)
                End If
            Next
        End If

        If mCompanyCodeStr <> "" Then
            mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
            MakeSQLSumm = MakeSQLSumm & vbCrLf & " AND GEN.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = MasterNo
                MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND IGH.RM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
            End If
        End If
        Dim mSFItemCode As String = ""

        If OptDetSumm(2).Checked = True Then
            If chkAllSF.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                If MainClass.ValidateWithMasterTable(TxtSFItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mSFItemCode = MasterNo
                    MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND IGD.SF_CODE='" & MainClass.AllowSingleQuote(mSFItemCode) & "'"
                End If
            End If
        End If

        If cboDept.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDept = MasterNo
                MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND IGH.DEPT_CODE='" & MainClass.AllowSingleQuote(mDept) & "'"
            End If
        End If

        If cboShift.Text <> "ALL" Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND IGH.SHIFT_CODE='" & VB.Left(cboShift.Text, 1) & "'"
        End If


        'If cboApproved.Text <> "ALL" Then
        '    MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND IGH.IS_APPROVED='" & VB.Left(cboApproved.Text, 1) & "'"
        'End If

        'If cboType.Text <> "ALL" Then
        '    MakeSQLSumm = MakeSQLSumm & vbCrLf & "AND IGH.PROD_TYPE='" & VB.Left(cboType.Text, 1) & "'"
        'End If

        ''- DECODE(IGH.SHIFT_CODE,'C',1,0)	

        If optDate(0).Checked = True Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & " AND IGH.PROD_DATE >=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IGH.PROD_DATE <=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            MakeSQLSumm = MakeSQLSumm & vbCrLf & " AND IGH.REF_DATE >=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IGH.REF_DATE <=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        If chkTime.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & " AND IGH.PREP_TIME >=TO_DATE('" & txtTMFrom.Text & "', 'HH24:MI')" & vbCrLf & " AND IGH.PREP_TIME <=TO_DATE('" & txtTMTo.Text & "', 'HH24:MI')"
        End If

        If OptDetSumm(1).Checked = True Then
            MakeSQLSumm = MakeSQLSumm & vbCrLf & " GROUP BY IGH.PROD_TYPE," & vbCrLf _
                & " IGH.DEPT_CODE, DIV.DIV_DESC, " & vbCrLf _
                & " IGH.RM_CODE, RMMST.ITEM_SHORT_DESC, IGH.RM_THICKNESS, IGH.RM_UOM, IGH.PRODUCTION_FROM, " & vbCrLf _
                & " RM_BLOCK_CODE, BLOCK_AREA, GEN.COMPANY_SHORTNAME"

        Else
            MakeSQLSumm = MakeSQLSumm & vbCrLf & " GROUP BY IGH.PROD_TYPE," & vbCrLf _
                & " IGH.DEPT_CODE, DIV.DIV_DESC, " & vbCrLf _
                & " IGH.RM_CODE, RMMST.ITEM_SHORT_DESC, IGH.RM_THICKNESS, IGH.RM_UOM, IGH.PRODUCTION_FROM, " & vbCrLf _
                & " IGD.SF_CODE, SFMST.ITEM_SHORT_DESC, IGD.SF_UOM,RM_BLOCK_CODE, BLOCK_AREA,GEN.COMPANY_SHORTNAME"

        End If

        'If OptOrderBy(0).Checked = True Then
        MakeSQLSumm = MakeSQLSumm & vbCrLf & "ORDER BY IGH.RM_CODE, RMMST.ITEM_SHORT_DESC,IGH.DEPT_CODE"
        'ElseIf OptOrderBy(1).Checked = True Then
        '    MakeSQLSumm = MakeSQLSumm & vbCrLf & "ORDER BY RMMST.ITEM_SHORT_DESC,TO_CHAR(IGH.REF_DATE,'DD/MM/YYYY'),IGH.AUTO_KEY_REF"
        'End If
        'End If	
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then txtDateFrom.Focus()
        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then txtDateTo.Focus()

        If ChkALL.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(TxtItemName.Text) = "" Then
                MsgInformation("Invaild Item Name")
                TxtItemName.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable(TxtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblAcCode.text = MasterNo
            Else
                MsgInformation("Invaild Item Name")
                TxtItemName.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If

        If chkAllSF.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(TxtSFItemName.Text) = "" Then
                MsgInformation("Invaild SF Item Name")
                TxtSFItemName.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable(TxtSFItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                'lblAcCode.Text = MasterNo
            Else
                MsgInformation("Invaild SF Item Name")
                TxtSFItemName.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If

        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Private Sub txtdateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateFrom) = False Then
            txtDateFrom.Focus()
            Cancel = True
            Exit Sub
        End If
        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then
            txtDateFrom.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtdateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateTo) = False Then
            txtDateTo.Focus()
            Cancel = True
            Exit Sub
        End If
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then
            txtDateTo.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub FillIssueCombo()

        On Error GoTo FillErr2
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing
        Dim CntLst As Long

        cboDept.Items.Clear()

        SqlStr = "SELECT DEPT_DESC FROM PAY_DEPT_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DEPT_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        cboDept.Items.Add("ALL")

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDept.Items.Add(RS.Fields("DEPT_DESC").Value)
                RS.MoveNext()
            Loop
        End If

        cboDept.SelectedIndex = 0

        cboShift.Items.Clear()
        cboShift.Items.Add("ALL")
        cboShift.Items.Add("A")
        cboShift.Items.Add("B")
        cboShift.Items.Add("C")
        cboShift.SelectedIndex = 0


        lstCompanyName.Items.Clear()
        SqlStr = "SELECT COMPANY_SHORTNAME FROM GEN_COMPANY_MST " & vbCrLf _
            & " ORDER BY COMPANY_NAME"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        Dim mCompanyName As String
        If RS.EOF = False Then
            lstCompanyName.Items.Add("ALL")
            CntLst = CntLst + 1
            Do While RS.EOF = False
                lstCompanyName.Items.Add(RS.Fields("COMPANY_SHORTNAME").Value)
                mCompanyName = IIf(IsDBNull(RS.Fields("COMPANY_SHORTNAME").Value), "", RS.Fields("COMPANY_SHORTNAME").Value)
                lstCompanyName.SetItemChecked(CntLst, IIf(mCompanyName = RsCompany.Fields("COMPANY_SHORTNAME").Value, True, False))
                'lstCompanyName.SetItemChecked(CntLst, False)
                RS.MoveNext()
                CntLst = CntLst + 1
            Loop
        End If

        lstCompanyName.SelectedIndex = 0

        Exit Sub
FillErr2:
        MsgBox(Err.Description)
    End Sub
    Private Sub lstCompanyName_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles lstCompanyName.ItemCheck

        Try
            If mClickProcess = True Then Exit Sub
            mClickProcess = True

            If e.Index = 0 Then
                If e.NewValue = System.Windows.Forms.CheckState.Checked Then     ''lstCompanyName.GetItemChecked(0) = True Then
                    For I = 1 To lstCompanyName.Items.Count - 1
                        lstCompanyName.SetItemChecked(I, True)
                    Next
                Else
                    For I = 1 To lstCompanyName.Items.Count - 1
                        lstCompanyName.SetItemChecked(I, False)
                    Next
                End If
            Else
                If e.NewValue = System.Windows.Forms.CheckState.Unchecked Then      ''lstCompanyName.GetItemChecked(e.Index - 1) = False Then
                    lstCompanyName.SetItemChecked(0, False)
                End If
            End If
            mClickProcess = False
        Catch ex As Exception

        End Try
    End Sub
    Private Sub chkAllSF_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllSF.CheckStateChanged
        Call PrintStatus(False)
        If chkAllSF.CheckState = System.Windows.Forms.CheckState.Checked Then
            TxtSFItemName.Enabled = False
            cmdsearchSF.Enabled = False
        Else
            TxtSFItemName.Enabled = True
            cmdsearchSF.Enabled = True
        End If
    End Sub


    Private Sub TxtSFItemName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtSFItemName.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub TxtSFItemName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtSFItemName.DoubleClick
        SearchItemSF()
    End Sub

    Private Sub SearchItemSF()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.SearchGridMaster(TxtSFItemName.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr)
        If AcName <> "" Then
            TxtSFItemName.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub TxtSFItemName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtSFItemName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtSFItemName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub TxtSFItemName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtSFItemName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchItemSF()
    End Sub
    Private Sub TxtSFItemName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtSFItemName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        ''lblAcCode.Text = ""
        If TxtSFItemName.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable(TxtSFItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            ''lblAcCode.text = MasterNo
            TxtSFItemName.Text = UCase(Trim(TxtSFItemName.Text))
        Else
            ''lblAcCode.text = ""
            MsgInformation("No Such SF Item in Item Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub cmdsearchSF_Click(sender As Object, e As EventArgs) Handles cmdsearchSF.Click
        SearchItemSF()
    End Sub
End Class
