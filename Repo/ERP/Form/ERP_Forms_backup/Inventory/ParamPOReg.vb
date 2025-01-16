Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamPOReg
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20

    Private Const ColLocked As Short = 1
    Private Const ColPONo As Short = 2
    Private Const ColPODate As Short = 3
    Private Const ColAmendNo As Short = 4
    Private Const colSupplier As Short = 5
    Private Const colSupplierShipped As Short = 6
    Private Const ColItemCode As Short = 7
    Private Const ColItemDesc As Short = 8
    Private Const ColUnit As Short = 9
    Private Const ColQty As Short = 10
    Private Const ColPrice As Short = 11
    Private Const ColDiscPer As Short = 12
    Private Const ColDiscRate As Short = 13
    Private Const ColAmount As Short = 14
    Private Const ColCategory As Short = 15
    Private Const ColNAVPONo As Short = 16
    Private Const ColShowPO As Short = 17
    Private Const ColOwner As Short = 18
    Private Const ColAssetsNo As Short = 19
    Private Const ColDeliverTo As Short = 20
    Private Const ColAddUser As Short = 21
    Private Const ColAddDate As Short = 22
    Private Const ColModUser As Short = 23
    Private Const ColModDate As Short = 24
    Private Const ColMKEY As Short = 25
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub cboClassification_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboClassification.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboClassification_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboClassification.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboExportItem_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboExportItem.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub cboExportItem_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboExportItem.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboItemLock_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboItemLock.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboItemLock_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboItemLock.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboItemType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboItemType.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboItemType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboItemType.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboOrderType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboOrderType.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboOrderType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboOrderType.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboPurType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboPurType.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboPurType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboPurType.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboShow_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShow.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboShow_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShow.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboStatus_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboStatus.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboStatus_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboStatus.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboSuppType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboSuppType.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboSuppType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboSuppType.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        Call PrintStatus(False)
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            TxtItemName.Enabled = False
            cmdSearch.Enabled = False
        Else
            TxtItemName.Enabled = True
            cmdSearch.Enabled = True
        End If
    End Sub

    Private Sub chkAllSupp_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllSupp.CheckStateChanged
        Call PrintStatus(False)
        If chkAllSupp.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtSupplier.Enabled = False
            cmdsearchSupp.Enabled = False
        Else
            txtSupplier.Enabled = True
            cmdsearchSupp.Enabled = True
        End If
    End Sub



    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonPO(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonPO(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportonPO(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ReportErr
        Dim SqlStr As String = ""
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""


        Report1.Reset()

        If chkCategoryWise.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = MakeSQL()
        Else
            If MainClass.FillPrintDummyDataFromSprd(SprdMain, 1, SprdMain.MaxRows, ColLocked, ColMKEY, PubDBCn) = False Then GoTo ReportErr
            SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")
        End If

        If cboPurType.Text = "ALL" Then
            mTitle = "Purchase Order Register (ALL)"
        Else
            mTitle = cboPurType.Text & " Register"
        End If

        mSubTitle = "From : " & VB6.Format(txtDateFrom.Text, "DD/MM/YYYY") & " To : " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY")

        If chkCategoryWise.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\POReg.RPT"
        Else
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\POReg_CatWise.RPT"
        End If


        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
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

    Private Sub cmdsearchSupp_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchSupp.Click
        SearchSupplier()
    End Sub


    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        If FieldsVerification() = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        If Show1() = False Then GoTo ErrPart
        Call PrintStatus(True)

        ''MainClass.SetFocusToCell SprdMain, mActiveRow, 4
        FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamPOReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Purchase Order Register"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamPOReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtItemName.Enabled = False
        cmdsearch.Enabled = False
        txtSupplier.Enabled = False
        cmdsearchSupp.Enabled = False

        chkAllCategory.CheckState = System.Windows.Forms.CheckState.Checked
        txtCategory.Enabled = False
        cmdsearchCategory.Enabled = False

        chkAllSubCat.CheckState = System.Windows.Forms.CheckState.Checked
        txtSubCategory.Enabled = False
        cmdSubCatsearch.Enabled = False

        Call PrintStatus(True)
        Call FillPOCombo()
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        FormatSprdMain(-1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamPOReg_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
    Private Sub frmParamPOReg_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub SprdMain_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdMain.ButtonClicked
        'Dim lExtension As String
        'Dim lType, lCommandLine As String
        'Dim I As Short
        'Dim mDocName As String
        'Dim mPONo As String
        'Dim mAmendNo As String
        'Dim mPONOStr As String

        'SprdMain.Row=eventArgs.Row
        'SprdMain.Col = ColPONo
        'mPONo = Trim(SprdMain.Text)

        'SprdMain.Col = ColAmendNo
        'mAmendNo = VB6.Format(SprdMain.Text, "000")

        'mPONOStr = mPONo & mAmendNo

        'mDocName = My.Application.Info.DirectoryPath & "\Document\PO\" & mPONOStr & ".pdf"

        'If Trim(mPONOStr) = "" Then Exit Sub

        'lExtension = "." & GetFileExtension(mDocName)

        'Dim sr As Boolean
        'If Len(lExtension) > 1 Then

        '    ' If mDocName contains at least one space, it's a long filename,
        '    ' we add " characters
        '    If InStr(1, mDocName, " ") <> 0 Then
        '        mDocName = """" & mDocName & """"
        '    End If

        '    ' Get the corresponding file type in the registry
        '    lType = regQuery_A_Key(HKEY_CLASSES_ROOT, lExtension, "")
        '    If lType = "" Then
        '        ' Unknown type
        '        Exit Sub
        '    End If

        '    ' Get the corresponding command line
        '    lCommandLine = regQuery_A_Key(HKEY_CLASSES_ROOT, lType & "\shell\open\command", "")

        '    ' MsgBox lCommandLine
        '    sr = StringReplace(mDocName, """", "")

        '    If lCommandLine = "" Then
        '        ' No application can open this file type
        '        MsgBox("No application can open this file type", MsgBoxStyle.Critical, "Error")
        '        Exit Sub
        '    End If

        '    ' Replace %1 with mDocName in lCommandLine

        '    If Not StringReplace(lCommandLine, "%1", mDocName) Then
        '        ' Add the file name at the end of the command line
        '        lCommandLine = lCommandLine & " " & mDocName
        '    End If

        '    Call Shell(lCommandLine, AppWinStyle.MaximizedFocus)
        'End If
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
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xPoNo As Double
        Dim xAmendPONo As Double
        Dim xPOWEF As String

        Dim ss1 As New frmPO_GST

        SprdMain.Row = SprdMain.ActiveRow

        If eventArgs.col = ColShowPO Then
            Call SprdMain_ButtonClicked(SprdMain, New AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent(eventArgs.col, eventArgs.row, 0))
            Exit Sub
        End If

        SprdMain.Col = ColPONo
        xPoNo = Val(SprdMain.Text)
        If xPoNo <= 0 Then Exit Sub

        SprdMain.Col = ColAmendNo
        xAmendPONo = Val(SprdMain.Text)

        SprdMain.Col = ColPODate
        xPOWEF = VB6.Format(SprdMain.Text, "DD/MM/YYYY")

        SqlStr = "SELECT * from PUR_PURCHASE_HDR WHERE AUTO_KEY_PO=" & xPoNo & " AND AMEND_NO=" & xAmendPONo & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


        If RsTemp.EOF = False Then
            ss1.MdiParent = Me.MdiParent

            ss1.lblBookType.Text = RsTemp.Fields("PUR_TYPE").Value & RsTemp.Fields("ORDER_TYPE").Value
            ss1.Show()
            ss1.frmPO_GST_Activated(Nothing, New System.EventArgs())

            ss1.txtPONo.Text = RsTemp.Fields("AUTO_KEY_PO").Value
            ss1.txtAmendNo.Text = RsTemp.Fields("AMEND_NO").Value

            ss1.txtAmendNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False)) ''txtPONO_Validate False

        End If

    End Sub

    Private Sub txtCategory_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCategory.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtCategory_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCategory.DoubleClick
        SearchCategory()
    End Sub
    Private Sub txtCategory_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCategory.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCategory.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtCategory_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCategory.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchCategory()
    End Sub
    Private Sub txtCategory_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCategory.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        lblAcCode.Text = ""
        If txtCategory.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'"

        If MainClass.ValidateWithMasterTable((txtCategory.Text), "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            lblAcCode.Text = MasterNo
            txtCategory.Text = UCase(Trim(txtCategory.Text))
        Else
            lblAcCode.Text = ""
            MsgInformation("No Such Account in Account Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub SearchCategory()
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'"

        If MainClass.SearchGridMaster((txtCategory.Text), "INV_GENERAL_MST", "GEN_DESC", "GEN_CODE", , , SqlStr) = True Then
            If AcName <> "" Then
                txtCategory.Text = AcName
            End If
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdSearchCategory_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchCategory.Click
        SearchCategory()
    End Sub

    Private Sub chkAllCategory_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllCategory.CheckStateChanged
        Call PrintStatus(False)
        If chkAllCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtCategory.Enabled = False
            cmdsearchCategory.Enabled = False
        Else
            txtCategory.Enabled = True
            cmdsearchCategory.Enabled = True
        End If
    End Sub

    Private Sub chkAllSubCat_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllSubCat.CheckStateChanged
        Call PrintStatus(False)
        If chkAllSubCat.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtSubCategory.Enabled = False
            cmdSubCatsearch.Enabled = False
        Else
            txtSubCategory.Enabled = True
            cmdSubCatsearch.Enabled = True
        End If
    End Sub
    Private Sub SearchSubCategory()
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mCatCode As String = ""

        If txtCategory.Text = "" Then
            MsgInformation("Please Select category .")
            txtCategory.Focus()
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable((txtCategory.Text), "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            mCatCode = MasterNo
        End If
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " And CATEGORY_CODE='" & mCatCode & "'"


        If MainClass.SearchGridMaster((txtSubCategory.Text), "INV_SUBCATEGORY_MST", "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", , , SqlStr) = True Then
            If AcName <> "" Then
                txtSubCategory.Text = AcName
            End If
        End If

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub txtSubCategory_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSubCategory.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtSubCategory_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSubCategory.DoubleClick
        SearchSubCategory()
    End Sub


    Private Sub txtSubCategory_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSubCategory.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSubCategory.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSubCategory_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSubCategory.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchSubCategory()
    End Sub

    Private Sub txtSubCategory_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSubCategory.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mCatCode As String = ""

        lblAcCode.Text = ""
        If txtSubCategory.Text = "" Then GoTo EventExitSub


        If txtCategory.Text = "" Then
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        Else
            If MainClass.ValidateWithMasterTable((txtCategory.Text), "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , SqlStr) = True Then
                mCatCode = MasterNo
            End If
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " And CATEGORY_CODE='" & mCatCode & "'"
        End If

        If MainClass.ValidateWithMasterTable((txtSubCategory.Text), "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgInformation("No Such Sub Category ")
            Cancel = True
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub cmdSubCatsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSubCatsearch.Click
        SearchSubCategory()
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

        Dim mAccountCode As String

        SqlStr = " SELECT DISTINCT ITEMMST.ITEM_SHORT_DESC, ID.ITEM_CODE,  ITEMMST.CUSTOMER_PART_NO "

        SqlStr = SqlStr & vbCrLf _
            & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID, INV_ITEM_MST ITEMMST"

        ''WHERE CLAUSE...
        SqlStr = SqlStr & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
            & " And ID.COMPANY_CODE=ITEMMST.COMPANY_CODE" & vbCrLf _
            & " And ID.ITEM_CODE=ITEMMST.ITEM_CODE"



        If chkAllSupp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mAccountCode) & "'"
            End If
        End If

        MainClass.SearchGridMasterBySQL2(TxtItemName.Text, SqlStr)
        If AcName <> "" Then
            TxtItemName.Text = AcName
        End If

        'SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        'MainClass.SearchGridMaster(TxtItemName.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr)
        'If AcName <> "" Then
        '    TxtItemName.Text = AcName
        'End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub SearchSupplier()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = " SELECT DISTINCT CMST.SUPP_CUST_NAME, IH.SUPP_CUST_CODE"

        SqlStr = SqlStr & vbCrLf _
            & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID, FIN_SUPP_CUST_MST CMST"

        ''WHERE CLAUSE...
        SqlStr = SqlStr & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
            & " And IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " And IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE"


        MainClass.SearchGridMasterBySQL2(txtSupplier.Text, SqlStr)
        If AcName <> "" Then
            txtSupplier.Text = AcName
        End If

        'SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"
        'MainClass.SearchGridMaster(txtSupplier.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr)
        'If AcName <> "" Then
        '    txtSupplier.Text = AcName
        'End If
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
    Private Sub TxtItemName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtItemName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        lblAcCode.Text = ""
        If TxtItemName.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable((TxtItemName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            lblAcCode.Text = MasterNo
            TxtItemName.Text = UCase(Trim(TxtItemName.Text))
        Else
            lblAcCode.Text = ""
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

        'Dim cntCol As Integer
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

            .Col = ColPONo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPONo, 9)

            .Col = ColAmendNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColAmendNo, 5)

            .Col = ColPODate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPODate, 9)

            .ColsFrozen = ColAmendNo

            .Col = colSupplier
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(colSupplier, 20)

            .Col = colSupplierShipped
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(colSupplierShipped, 20)

            .Col = ColOwner
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColOwner, 12)


            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemCode, 8)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemDesc, 30)

            .Col = ColUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColUnit, 4)

            .Col = ColQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColQty, 9)
            If cboOrderType.SelectedIndex = 2 Then
                .ColHidden = True
            Else
                .ColHidden = False
            End If

            .Col = ColPrice
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColPrice, 9)

            .Col = ColDiscPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColDiscPer, 9)

            .Col = ColDiscRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColDiscRate, 9)

            .Col = ColAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColAmount, 9)
            If cboOrderType.SelectedIndex = 2 Then
                .ColHidden = True
            Else
                .ColHidden = False
            End If

            .Col = ColCategory
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColCategory, 15)
            .ColHidden = False

            .Col = ColNAVPONo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColNAVPONo, 8)
            .ColHidden = True

            .Col = ColShowPO
            .CellType = SS_CELL_TYPE_BUTTON
            .TypeButtonText = "Show"
            .TypeButtonAlign = SS_CELL_BUTTON_ALIGN_LEFT
            .set_ColWidth(ColShowPO, 8)


            .Col = ColDeliverTo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColDeliverTo, 15)

            .Col = ColAddUser
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColAddUser, 10)

            .Col = ColAddDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColAddDate, 10)

            .Col = ColModUser
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColModUser, 10)

            .Col = ColModDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColModDate, 10)

            .Col = ColAssetsNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColAssetsNo, 12)

            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMKEY, 8)
            .ColHidden = True


            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ''OperationModeSingle
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

        If chkCategoryWise.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = MakeSQL()
        Else
            SqlStr = MakeCATSQL()
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

    Private Function MakeSQL() As String

        On Error GoTo ERR1
        Dim mDept As String
        Dim mSupplier As String
        Dim mItemCode As String
        Dim mCatCode As String = ""
        Dim mSubCatCode As String
        Dim mDivision As Double

        ''SELECT CLAUSE...

        MakeSQL = " Select ''," & vbCrLf _
            & " IH.AUTO_KEY_PO," & vbCrLf _
            & " TO_CHAR(ID.PO_WEF_DATE,'DD/MM/YYYY'),AMEND_NO," & vbCrLf _
            & " CMST.SUPP_CUST_NAME," & vbCrLf _
            & " SCMST.SUPP_CUST_NAME," & vbCrLf _
            & " ID.ITEM_CODE, INVMST.ITEM_SHORT_DESC," & vbCrLf _
            & " ID.ITEM_UOM, TO_CHAR(ITEM_QTY), " & vbCrLf _
            & " TO_CHAR(ITEM_PRICE*EXCHANGERATE),TO_CHAR(ITEM_DIS_PER), " & vbCrLf _
            & " TO_CHAR((NVL(ITEM_PRICE*EXCHANGERATE,0) - ROUND((NVL(ITEM_PRICE*EXCHANGERATE,0) * ITEM_DIS_PER)/100,2))) AS DISCrate, " & vbCrLf _
            & " TO_CHAR(GROSS_AMT*EXCHANGERATE), GMST.GEN_DESC, " & vbCrLf _
            & " IH.NAV_PO_NO,'',OMST.SUPP_CUST_NAME,  ID.ASSETS_NO,DELMST.SUPP_CUST_NAME, IH.ADDUSER, IH.ADDDATE,IH.MODUSER, IH.MODDATE, IH.AUTO_KEY_PO "

        ''FROM CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID," & vbCrLf _
            & " FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_MST SCMST, INV_ITEM_MST INVMST, INV_GENERAL_MST GMST, FIN_SUPP_CUST_MST OMST, FIN_SUPP_CUST_MST DELMST"

        ''WHERE CLAUSE...
        MakeSQL = MakeSQL & vbCrLf _
            & " WHERE " & vbCrLf _
            & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.Mkey=ID.Mkey" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf & " AND IH.COMPANY_CODE=SCMST.COMPANY_CODE" & vbCrLf & " AND DECODE(SHIPPED_TO_SAMEPARTY,'Y',IH.SUPP_CUST_CODE,IH.SHIPPED_TO_PARTY_CODE)=SCMST.SUPP_CUST_CODE " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND INVMST.COMPANY_CODE=GMST.COMPANY_CODE " & vbCrLf & " AND INVMST.CATEGORY_CODE=GMST.GEN_CODE AND GMST.GEN_TYPE='C' "

        MakeSQL = MakeSQL & vbCrLf _
            & " AND IH.COMPANY_CODE=OMST.COMPANY_CODE(+)" & vbCrLf _
            & " AND IH.OWNER_CODE=OMST.SUPP_CUST_CODE(+) "

        MakeSQL = MakeSQL & vbCrLf _
            & " AND IH.COMPANY_CODE=DELMST.COMPANY_CODE(+)" & vbCrLf _
            & " AND IH.DELIVERY_TO=DELMST.SUPP_CUST_CODE(+) "

        If chkAllCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtCategory.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                mCatCode = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND INVMST.CATEGORY_CODE='" & MainClass.AllowSingleQuote(mCatCode) & "'"
            End If
        End If

        If chkAllSubCat.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtSubCategory.Text, "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCatCode & "'") = True Then
                mSubCatCode = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND INVMST.SUBCATEGORY_CODE='" & MainClass.AllowSingleQuote(mSubCatCode) & "'"
            End If
        End If

        If chkAllSupp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSupplier = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSupplier) & "'"
            End If
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
            End If
        End If

        If chkCapital.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQL = MakeSQL & vbCrLf & "AND  ID.ASSETS_NO='Y'"
        End If



        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDivision.Text, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = Val(MasterNo)
                MakeSQL = MakeSQL & vbCrLf & "AND IH.DIV_CODE=" & mDivision & ""
            End If
        End If

        If cboShow.SelectedIndex = 1 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.ISCAPITAL='Y'"
        ElseIf cboShow.SelectedIndex = 2 Then

            MakeSQL = MakeSQL & vbCrLf & " AND IH.AUTO_KEY_PO NOT IN (" & vbCrLf & " SELECT DISTINCT REF_PO_NO FROM INV_GATE_DET" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND REF_TYPE='P' AND SUBSTR(AUTO_KEY_MRR,LENGTH(AUTO_KEY_MRR)-5,4)>=2007"

            If chkAllSupp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mSupplier = MasterNo
                    MakeSQL = MakeSQL & vbCrLf & "AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSupplier) & "'"
                End If
            End If

            If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                If MainClass.ValidateWithMasterTable(TxtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mItemCode = MasterNo
                    MakeSQL = MakeSQL & vbCrLf & "AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
                End If
            End If
            MakeSQL = MakeSQL & ")"

        ElseIf cboShow.SelectedIndex = 3 Then
            MakeSQL = MakeSQL & vbCrLf & "AND ID.IS_TENTATIVE_RATE='Y'"
        ElseIf cboShow.SelectedIndex = 4 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.IS_DEVELOPMENT='Y'"
        End If

        '    If chkCapitalPO.Value = vbChecked Then
        '        MakeSQL = MakeSQL & vbCrLf & "AND IH.ISCAPITAL='Y'"
        '    End If
        '
        '    If chkTentativeRate.Value = vbChecked Then
        '        MakeSQL = MakeSQL & vbCrLf & "AND ID.IS_TENTATIVE_RATE='Y'"
        '    End If

        If cboPurType.Text <> "ALL" Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.PUR_TYPE='" & VB.Left(cboPurType.Text, 1) & "'"
        End If

        If cboOrderType.Text <> "ALL" Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.ORDER_TYPE='" & VB.Left(cboOrderType.Text, 1) & "'"
        End If

        If cboStatus.SelectedIndex = 1 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.PO_STATUS='Y'"
        ElseIf cboStatus.SelectedIndex = 2 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.PO_STATUS='N'"
        End If

        If cboSuppType.SelectedIndex = 1 Then
            MakeSQL = MakeSQL & vbCrLf & "AND CMST.WITHIN_COUNTRY='Y'"
        ElseIf cboSuppType.SelectedIndex = 2 Then
            MakeSQL = MakeSQL & vbCrLf & "AND CMST.WITHIN_COUNTRY='N'"
        End If

        If cboItemType.SelectedIndex >= 1 Then
            MakeSQL = MakeSQL & vbCrLf & "AND INVMST.ITEM_TYPE='" & VB.Left(cboItemType.Text, 1) & "'"
        End If

        If cboExportItem.SelectedIndex >= 1 Then
            MakeSQL = MakeSQL & vbCrLf & "AND INVMST.IS_EXPORT_ITEM='" & VB.Left(cboExportItem.Text, 1) & "'"
        End If

        If cboClassification.SelectedIndex >= 1 Then
            MakeSQL = MakeSQL & vbCrLf & "AND INVMST.ITEM_CLASSIFICATION='" & VB.Left(cboClassification.Text, 1) & "'"
        End If

        If OptShow(0).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.PO_CLOSED='N'"
        ElseIf OptShow(1).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.PO_CLOSED='Y'"
        End If

        If chkRecdPo.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.RECD_AC_FLAG='Y'"
        End If

        If Val(txtPONo.Text) <> 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.AUTO_KEY_PO=" & Val(txtPONo.Text) & ""
        End If

        'If Trim(txtNAVPONo.Text) <> "" Then
        '    MakeSQL = MakeSQL & vbCrLf & "AND IH.NAV_PO_NO='" & Trim(txtNAVPONo.Text) & "'"
        'End If


        If cboItemLock.SelectedIndex = 1 Then
            MakeSQL = MakeSQL & vbCrLf & " AND IH.AUTO_KEY_PO || '-' || TRIM(ID.ITEM_CODE)  NOT IN (" & vbCrLf & " SELECT AUTO_KEY_PO || '-' || TRIM(ITEM_CODE) FROM INV_PO_ITEM_LOCK_DET" & vbCrLf & " WHERE COMPANY_CODE=IH.COMPANY_CODE " & vbCrLf & " AND AUTO_KEY_PO= IH.AUTO_KEY_PO" & vbCrLf & " AND ITEM_CODE= INVMST.ITEM_CODE)"

        ElseIf cboItemLock.SelectedIndex = 2 Then
            MakeSQL = MakeSQL & vbCrLf & " AND IH.AUTO_KEY_PO || '-' || TRIM(ID.ITEM_CODE)  IN (" & vbCrLf & " SELECT AUTO_KEY_PO || '-' || TRIM(ITEM_CODE) FROM INV_PO_ITEM_LOCK_DET" & vbCrLf & " WHERE COMPANY_CODE=IH.COMPANY_CODE " & vbCrLf & " AND AUTO_KEY_PO= IH.AUTO_KEY_PO" & vbCrLf & " AND ITEM_CODE= INVMST.ITEM_CODE)"
        End If

        If OptDate(0).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " AND ID.PO_WEF_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND ID.PO_WEF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        ElseIf OptDate(1).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " AND IH.PUR_ORD_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.PUR_ORD_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            MakeSQL = MakeSQL & vbCrLf & " AND IH.AMEND_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.AMEND_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        ''ORDER CLAUSE...
        If OptOrderBy(0).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "ORDER BY SUBSTR(IH.AUTO_KEY_PO,LENGTH(IH.AUTO_KEY_PO)-5,4),IH.MKEY,IH.SUPP_CUST_CODE, ID.PO_WEF_DATE,ID.SERIAL_NO"
        ElseIf OptOrderBy(1).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "ORDER BY INVMST.ITEM_SHORT_DESC,IH.MKEY, ID.PO_WEF_DATE"
        End If
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function MakeCATSQL() As String

        On Error GoTo ERR1
        Dim mDept As String
        Dim mSupplier As String
        Dim mItemCode As String
        Dim mCatCode As String = ""
        Dim mSubCatCode As String
        Dim mDivision As Double

        ''SELECT CLAUSE...

        MakeCATSQL = " Select DISTINCT ''," & vbCrLf _
            & " IH.AUTO_KEY_PO," & vbCrLf _
            & " TO_CHAR(ID.PO_WEF_DATE,'DD/MM/YYYY'),AMEND_NO," & vbCrLf _
            & " CMST.SUPP_CUST_NAME," & vbCrLf & " SCMST.SUPP_CUST_NAME," & vbCrLf _
            & " CAT_MST.SUBCATEGORY_CODE, CAT_MST.SUBCATEGORY_DESC," & vbCrLf _
            & " ID.ITEM_UOM, TO_CHAR(ITEM_QTY), " & vbCrLf & " TO_CHAR(ITEM_PRICE*EXCHANGERATE),TO_CHAR(ITEM_DIS_PER), " & vbCrLf _
            & " TO_CHAR((NVL(ITEM_PRICE*EXCHANGERATE,0) - ROUND((NVL(ITEM_PRICE*EXCHANGERATE,0) * ITEM_DIS_PER)/100,2))) AS DISCrate, " & vbCrLf _
            & " TO_CHAR(GROSS_AMT*EXCHANGERATE), " & vbCrLf _
            & " '','','','','','','','','',IH.AUTO_KEY_PO "


        ''FROM CLAUSE...
        MakeCATSQL = MakeCATSQL & vbCrLf & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID," & vbCrLf & " FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_MST SMST,INV_ITEM_MST INVMST, INV_SUBCATEGORY_MST CAT_MST" ',FIN_SUPP_CUST_MST OMST

        ''WHERE CLAUSE...
        MakeCATSQL = MakeCATSQL & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.Mkey=ID.Mkey" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf & " AND IH.COMPANY_CODE=SCMST.COMPANY_CODE" & vbCrLf & " AND DECODE(SHIPPED_TO_SAMEPARTY,'Y',IH.SUPP_CUST_CODE,IH.SHIPPED_TO_PARTY_CODE)=SCMST.SUPP_CUST_CODE " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND INVMST.COMPANY_CODE=CAT_MST.COMPANY_CODE " & vbCrLf & " AND INVMST.CATEGORY_CODE=CAT_MST.CATEGORY_CODE " & vbCrLf & " AND INVMST.SUBCATEGORY_CODE=CAT_MST.SUBCATEGORY_CODE"

        '    MakeSQL = MakeSQL & vbCrLf _
        ''            & " AND IH.COMPANY_CODE=OMST.COMPANY_CODE(+)" & vbCrLf _
        ''            & " AND IH.OWNER_CODE=OMST.SUPP_CUST_CODE(+) "

        If chkAllCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtCategory.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                mCatCode = MasterNo
                MakeCATSQL = MakeCATSQL & vbCrLf & "AND INVMST.CATEGORY_CODE='" & MainClass.AllowSingleQuote(mCatCode) & "'"
            End If
        End If

        If chkAllSubCat.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtSubCategory.Text, "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCatCode & "'") = True Then
                mSubCatCode = MasterNo
                MakeCATSQL = MakeCATSQL & vbCrLf & "AND INVMST.SUBCATEGORY_CODE='" & MainClass.AllowSingleQuote(mSubCatCode) & "'"
            End If
        End If

        If chkAllSupp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSupplier = MasterNo
                MakeCATSQL = MakeCATSQL & vbCrLf & "AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSupplier) & "'"
            End If
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = MasterNo
                MakeCATSQL = MakeCATSQL & vbCrLf & "AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
            End If
        End If

        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDivision.Text, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = Val(MasterNo)
                MakeCATSQL = MakeCATSQL & vbCrLf & "AND IH.DIV_CODE=" & mDivision & ""
            End If
        End If

        If cboPurType.Text <> "ALL" Then
            MakeCATSQL = MakeCATSQL & vbCrLf & "AND IH.PUR_TYPE='" & VB.Left(cboPurType.Text, 1) & "'"
        End If

        If cboOrderType.Text <> "ALL" Then
            MakeCATSQL = MakeCATSQL & vbCrLf & "AND IH.ORDER_TYPE='" & VB.Left(cboOrderType.Text, 1) & "'"
        End If

        '    If chkTentativeRate.Value = vbChecked Then
        '        MakeCATSQL = MakeCATSQL & vbCrLf & "AND ID.IS_TENTATIVE_RATE='Y'"
        '    End If

        If cboShow.SelectedIndex = 1 Then
            MakeCATSQL = MakeCATSQL & vbCrLf & "AND IH.ISCAPITAL='Y'"
        ElseIf cboShow.SelectedIndex = 2 Then

            MakeCATSQL = MakeCATSQL & vbCrLf & " AND IH.AUTO_KEY_PO NOT IN (" & vbCrLf & " SELECT DISTINCT REF_PO_NO FROM INV_GATE_DET" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND REF_TYPE='P' AND SUBSTR(AUTO_KEY_MRR,LENGTH(AUTO_KEY_MRR)-5,4)>=2007"

            If chkAllSupp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mSupplier = MasterNo
                    MakeCATSQL = MakeCATSQL & vbCrLf & "AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSupplier) & "'"
                End If
            End If

            If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                If MainClass.ValidateWithMasterTable(TxtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mItemCode = MasterNo
                    MakeCATSQL = MakeCATSQL & vbCrLf & "AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
                End If
            End If
            MakeCATSQL = MakeCATSQL & ")"

        ElseIf cboShow.SelectedIndex = 3 Then
            MakeCATSQL = MakeCATSQL & vbCrLf & "AND ID.IS_TENTATIVE_RATE='Y'"
        ElseIf cboShow.SelectedIndex = 4 Then
            MakeCATSQL = MakeCATSQL & vbCrLf & "AND IH.IS_DEVELOPMENT='Y'"
        End If
        If cboStatus.SelectedIndex = 1 Then
            MakeCATSQL = MakeCATSQL & vbCrLf & "AND IH.PO_STATUS='Y'"
        ElseIf cboStatus.SelectedIndex = 2 Then
            MakeCATSQL = MakeCATSQL & vbCrLf & "AND IH.PO_STATUS='N'"
        End If

        If OptShow(0).Checked = True Then
            MakeCATSQL = MakeCATSQL & vbCrLf & "AND IH.PO_CLOSED='N'"
        ElseIf OptShow(1).Checked = True Then
            MakeCATSQL = MakeCATSQL & vbCrLf & "AND IH.PO_CLOSED='Y'"
        End If

        If chkRecdPo.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeCATSQL = MakeCATSQL & vbCrLf & "AND IH.RECD_AC_FLAG='Y'"
        End If

        If Val(txtPONo.Text) <> 0 Then
            MakeCATSQL = MakeCATSQL & vbCrLf & "AND IH.AUTO_KEY_PO=" & Val(txtPONo.Text) & ""
        End If

        'If Trim(txtNAVPONo.Text) <> "" Then
        '    MakeCATSQL = MakeCATSQL & vbCrLf & "AND IH.NAV_PO_NO='" & Trim(txtNAVPONo.Text) & "'"
        'End If

        '     MakeCATSQL = MakeCATSQL & vbCrLf _
        ''            & " AND ID.PUR_ORD_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
        ''            & " AND ID.PUR_ORD_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        If OptDate(0).Checked = True Then
            MakeCATSQL = MakeCATSQL & vbCrLf & " AND ID.PO_WEF_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND ID.PO_WEF_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        ElseIf OptDate(1).Checked = True Then
            MakeCATSQL = MakeCATSQL & vbCrLf & " AND IH.PUR_ORD_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.PUR_ORD_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            MakeCATSQL = MakeCATSQL & vbCrLf & " AND IH.AMEND_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.AMEND_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        ''GROUP CLAUSE...

        MakeCATSQL = MakeCATSQL & " GROUP BY " & vbCrLf & " IH.AUTO_KEY_PO," & vbCrLf & " TO_CHAR(ID.PO_WEF_DATE,'DD/MM/YYYY'),AMEND_NO," & vbCrLf & " CMST.SUPP_CUST_NAME," & vbCrLf & " CAT_MST.SUBCATEGORY_CODE, CAT_MST.SUBCATEGORY_DESC," & vbCrLf & " ID.ITEM_UOM, TO_CHAR(ITEM_QTY), " & vbCrLf & " TO_CHAR(ITEM_PRICE),TO_CHAR(ITEM_DIS_PER), " & vbCrLf & " TO_CHAR((NVL(ITEM_PRICE,0) - ROUND((NVL(ITEM_PRICE,0) * ITEM_DIS_PER)/100,2))), " & vbCrLf & " TO_CHAR(GROSS_AMT)"

        ''ORDER CLAUSE...
        If OptOrderBy(0).Checked = True Then
            MakeCATSQL = MakeCATSQL & vbCrLf & "ORDER BY IH.AUTO_KEY_PO,AMEND_NO ,CMST.SUPP_CUST_NAME, TO_CHAR(ID.PO_WEF_DATE,'DD/MM/YYYY')"
        ElseIf OptOrderBy(1).Checked = True Then
            MakeCATSQL = MakeCATSQL & vbCrLf & "ORDER BY CAT_MST.SUBCATEGORY_DESC,IH.AUTO_KEY_PO,AMEND_NO, TO_CHAR(ID.PO_WEF_DATE,'DD/MM/YYYY')"
        End If
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then txtDateFrom.SetFocus
        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        '    If FYChk(CDate(txtDateTo.Text)) = False Then txtDateTo.SetFocus
        If chkAllSupp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtSupplier.Text) = "" Then
                MsgInformation("Invaild Supplier Name")
                txtSupplier.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invaild Supplier Name")
                txtSupplier.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(TxtItemName.Text) = "" Then
                MsgInformation("Invaild Item Name")
                TxtItemName.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((TxtItemName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblAcCode.Text = MasterNo
            Else
                MsgInformation("Invaild Item Name")
                TxtItemName.Focus()
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
    Private Sub txtdateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
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
    Private Sub FillPOCombo()

        On Error GoTo FillErr2
        Dim SqlStr As String = ""
        'Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing

        cboDivision.Items.Clear()

        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DIV_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        cboDivision.Items.Add("ALL")

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDivision.Items.Add(RS.Fields("DIV_DESC").Value)
                RS.MoveNext()
            Loop
        End If

        cboDivision.SelectedIndex = 0

        cboPurType.Items.Clear()
        cboPurType.Items.Add("ALL")
        cboPurType.Items.Add("Purchase Order")
        '    cboPurType.AddItem "Work Order"
        cboPurType.Items.Add("Job Order")
        cboPurType.Items.Add("Lease Order")
        cboPurType.SelectedIndex = 0

        cboItemLock.Items.Clear()
        cboItemLock.Items.Add("ALL")
        cboItemLock.Items.Add("Unlock Item Ony")
        cboItemLock.Items.Add("Lock Item Ony")
        cboItemLock.SelectedIndex = 0

        cboOrderType.Items.Clear()
        cboOrderType.Items.Add("ALL")
        cboOrderType.Items.Add("Close")
        cboOrderType.Items.Add("Open")
        cboOrderType.SelectedIndex = 0

        CboStatus.Items.Clear()
        CboStatus.Items.Add("BOTH")
        CboStatus.Items.Add("Approval")
        CboStatus.Items.Add("Non Approval")
        CboStatus.SelectedIndex = 0

        cboSuppType.Items.Clear()
        cboSuppType.Items.Add("ALL")
        cboSuppType.Items.Add("WithIn Country")
        cboSuppType.Items.Add("Outside Country")
        cboSuppType.SelectedIndex = 0

        CboItemType.Items.Clear()
        CboItemType.Items.Add("All")
        CboItemType.Items.Add("Local")
        CboItemType.Items.Add("Imported")
        CboItemType.SelectedIndex = 0

        cboExportItem.Items.Clear()
        cboExportItem.Items.Add("All")
        cboExportItem.Items.Add("Yes")
        cboExportItem.Items.Add("No")
        cboExportItem.SelectedIndex = 0

        cboClassification.Items.Clear()

        cboClassification.Items.Add("ALL")
        cboClassification.Items.Add("BOP")
        '    cboItemClassification.AddItem "In House"
        '    cboItemClassification.AddItem "Job Work"
        '    cboItemClassification.AddItem "Regular "
        '    cboItemClassification.AddItem "Development"
        cboClassification.Items.Add("Tool")
        cboClassification.Items.Add("Assets")
        '    cboItemClassification.AddItem "SPD"
        cboExportItem.SelectedIndex = 0

        cboShow.Items.Clear()
        cboShow.Items.Add("Show All PO")
        cboShow.Items.Add("Only Capital PO")
        cboShow.Items.Add("Non Transaction PO")
        cboShow.Items.Add("Tentative Rate PO")
        cboShow.Items.Add("Only Development PO")

        Exit Sub
FillErr2:
        MsgBox(Err.Description)
    End Sub

    Private Sub txtPONo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPONo.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtPONo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPONo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSupplier_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplier.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtSupplier_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplier.DoubleClick
        SearchSupplier()
    End Sub
    Private Sub txtSupplier_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSupplier.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSupplier.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSupplier_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSupplier.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchSupplier()
    End Sub
End Class
