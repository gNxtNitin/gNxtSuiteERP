Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinDataSource
'Imports Infragistics.Win.UltraWinTabControl
Imports System.Data.OleDb
Friend Class frmParamDespatchNoteReg
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20

    Private Const ColLocked As Short = 1
    Private Const ColNo As Short = 2
    Private Const ColDate As Short = 3
    Private Const ColType As Short = 4
    Private Const ColPONo As Short = 5
    Private Const ColPODate As Short = 6
    Private Const ColOurSoNo As Short = 7
    Private Const ColOurSoDate As Short = 8
    Private Const colSupplier As Short = 9
    Private Const ColItemCode As Short = 10
    Private Const ColItemDesc As Short = 11
    Private Const ColItemPartNo As Short = 12
    Private Const ColUnit As Short = 13
    Private Const ColQty As Short = 14
    Private Const ColNoOfPkt As Short = 15

    Private Const ColPackNo As Short = 16
    Private Const ColPackType As Short = 17
    Private Const ColTransporter As Short = 18
    Private Const ColVehicleNo As Short = 19

    Private Const ColRefNo As Short = 20
    Private Const ColRefDate As Short = 21
    Private Const ColStockType As Short = 22
    Private Const ColPrepareby As Short = 23
    Private Const ColMKEY As Short = 24
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        Call PrintStatus(False)
        If ChkALL.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtItemName.Enabled = False
            cmdSearch.Enabled = False
        Else
            txtItemName.Enabled = True
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
        ReportONDNR(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportONDNR(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportONDNR(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String = ""
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""


        Report1.Reset()
        mTitle = "Despatch Note Register"
        mSubTitle = "From : " & VB6.Format(txtDateFrom.Text, "DD/MM/YYYY") & " To : " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY")
        If optOrderBy(0).Checked Then
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\DspNoteReg.rpt"
        Else
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\DspNoteItemWise.rpt"
        End If

        SqlStr = MakeSQL("S")
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
        Report1.WindowShowPrintBtn = True '' IIf(PubSuperUser = "S", True, False)
        Report1.WindowShowPrintSetupBtn = True ''IIf(PubSuperUser = "S", True, False)
        '    Report1.PrinterName = "Microsoft Print to PDF"
        Report1.WindowShowExportBtn = True
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
        If FieldsVerification = False Then Exit Sub
        'MainClass.ClearGrid(SprdMain, RowHeight)
        If Show1("S") = False Then GoTo ErrPart
        Call PrintStatus(True)

        ''MainClass.SetFocusToCell SprdMain, mActiveRow, 4
        'FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamDespatchNoteReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Despatch Note Register"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamDespatchNoteReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        ''Me.Width = VB6.TwipsToPixelsX(11355)

        lblTrnType.Text = CStr(-1)

        ChkALL.CheckState = System.Windows.Forms.CheckState.Checked
        txtItemName.Enabled = False
        cmdSearch.Enabled = False
        txtSupplier.Enabled = False
        cmdsearchSupp.Enabled = False

        txtEmpName.Enabled = False
        cmdsearchEmp.Enabled = False

        Call PrintStatus(True)
        'Call FillPOCombo
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")


        cboRefType.Items.Clear()
        cboRefType.Items.Add("ALL")
        cboRefType.Items.Add("Production")
        cboRefType.Items.Add("General")
        cboRefType.Items.Add("Job Work")
        cboRefType.Items.Add("QC Rejection")
        cboRefType.Items.Add("Line Rejection")
        cboRefType.Items.Add("Sale Rejection")
        cboRefType.Items.Add("Supplementry")
        cboRefType.Items.Add("Export")
        cboRefType.Items.Add("Rejection (Job Work)")
        cboRefType.Items.Add("Fixed Assets")

        cboRefType.SelectedIndex = 0

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

        Call Show1("L")
        UltraGridColumnChooser1.SourceGrid = UltraGrid1
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamDespatchNoteReg_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        UltraGrid1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        'SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame4.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11379.7, 749)
        CurrFormWidth = mReFormWidth

        'MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamDespatchNoteReg_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub UltraGrid1_DoubleClick(sender As Object, e As EventArgs) Handles UltraGrid1.DoubleClick

        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xDNo As Double
        'Dim xQCStatus As String
        Dim xMRRDate As String

        Dim mRow As UltraGridRow

        If OptShow(1).Checked = True Then Exit Sub

        If Me.UltraGrid1.ActiveRow.Index < 0 Then Exit Sub
        mRow = Me.UltraGrid1.Rows(Me.UltraGrid1.ActiveRow.Index)

        xDNo = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColNo - 1))
        SqlStr = "SELECT * from DSP_DESPATCH_HDR WHERE AUTO_KEY_DESP=" & xDNo & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            FrmDespatchNote.MdiParent = Me.MdiParent
            FrmDespatchNote.lblDespType.Text = RsTemp.Fields("DESPATCHTYPE").Value
            FrmDespatchNote.Show()



            FrmDespatchNote.FrmDespatchNote_Activated(Nothing, New System.EventArgs())

            FrmDespatchNote.txtDNNo.Text = CStr(xDNo)
            FrmDespatchNote.txtDNNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))

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
            txtItemName.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub SearchSupplier()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"
        MainClass.SearchGridMaster(txtSupplier.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr)
        If AcName <> "" Then
            txtSupplier.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub txtItemName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtItemName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtItemName.Text)
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
        If txtItemName.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable((txtItemName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            lblAcCode.Text = MasterNo
            txtItemName.Text = UCase(Trim(txtItemName.Text))
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

    Private Function Show1(pShowType As String) As Boolean

        On Error GoTo LedgError
        Dim SqlStr As String = ""


        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = MakeSQL(pShowType)
        'MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        FillUltraGrid(SqlStr)

        '********************************
        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function MakeSQL(pShowType As String) As String

        On Error GoTo ERR1
        Dim mDept As String
        Dim mSupplier As String
        Dim mEmployee As String
        Dim mDivision As Double

        ''SELECT CLAUSE...

        MakeSQL = " SELECT ''," & vbCrLf & " IH.AUTO_KEY_DESP," & vbCrLf _
            & " TO_CHAR(IH.DESP_DATE,'DD/MM/YYYY')," & vbCrLf _
            & " CASE WHEN IH.DESP_TYPE='G' THEN 'GENERAL' WHEN IH.DESP_TYPE='J' THEN 'JOB WORK'" & vbCrLf _
            & " WHEN IH.DESP_TYPE='P' THEN 'PRODUCTION'" & vbCrLf _
            & " WHEN IH.DESP_TYPE='Q' THEN 'QC REJECTION' WHEN IH.DESP_TYPE='L' THEN 'LINE REJECTION'" & vbCrLf _
            & " WHEN IH.DESP_TYPE='E' THEN 'EXPORT' WHEN IH.DESP_TYPE='F' THEN 'FIXED ASSETS'" & vbCrLf _
            & " WHEN IH.DESP_TYPE='S' THEN 'SALE REJECTION' WHEN IH.DESP_TYPE='U' THEN 'SUPPLEMENTARY' ELSE 'OTHERS' END," & vbCrLf _
            & " IH.VENDOR_PO,IH.VENDOR_PO_DATE, AUTO_KEY_SO, SO_DATE," & vbCrLf _
            & " CMST.SUPP_CUST_NAME," & vbCrLf _
            & " ID.ITEM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO," & vbCrLf _
            & " ID.ITEM_UOM, TO_CHAR(ID.PACKED_QTY), TO_CHAR(ID.NO_OF_PACKETS), TO_CHAR(ID.INNER_PACK_QTY), ID.PACK_TYPE," & vbCrLf _
            & " IH.TRANSPORTER_NAME, IH.VEHICLE_NO, ID.REF_NO, ID.REF_DATE, ID.STOCK_TYPE, IH.ADDUSER, IH.AUTO_KEY_DESP"

        ''FROM CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " FROM DSP_DESPATCH_HDR IH, DSP_DESPATCH_DET ID," & vbCrLf & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST"

        ''WHERE CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_DESP=ID.AUTO_KEY_DESP" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE "


        If chkAllSupp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSupplier = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSupplier) & "'"
            End If
        End If

        If chkAllEmp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            'If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            '    mSupplier = MasterNo
            MakeSQL = MakeSQL & vbCrLf & "AND IH.ADDUSER='" & MainClass.AllowSingleQuote(txtEmpName.Text) & "'"
            'End If
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQL = MakeSQL & vbCrLf & "AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(lblAcCode.Text) & "'"
        End If



        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDivision.Text, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND IH.DIV_CODE=" & mDivision & ""
            End If
        End If

        If Trim(txtVehicleNo.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.VEHICLE_NO='" & Trim(txtVehicleNo.Text) & "'"
        End If


        If cboRefType.SelectedIndex = 7 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.DESP_TYPE='U' "
        ElseIf cboRefType.SelectedIndex <> 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.DESP_TYPE='" & VB.Left(cboRefType.Text, 1) & "' "
        End If

        If OptShow(0).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " AND DESP_STATUS IN (0,1)"
        ElseIf OptShow(1).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " AND DESP_STATUS=0"
        ElseIf OptShow(2).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " AND DESP_STATUS=1"
        ElseIf OptShow(3).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " AND DESP_STATUS=2"
        End If

        If pShowType = "L" Then
            MakeSQL = MakeSQL & vbCrLf & " AND 1=2"
        End If

        MakeSQL = MakeSQL & vbCrLf & " AND IH.DESP_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND IH.DESP_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        'ORDER CLAUSE...

        If OptOrderBy(0).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "ORDER BY IH.AUTO_KEY_DESP, IH.DESP_DATE"
        ElseIf OptOrderBy(1).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "ORDER BY INVMST.ITEM_SHORT_DESC"
        End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        '    If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then txtDateFrom.SetFocus
        '    If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
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

        If chkAllEmp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtEmpName.Text) = "" Then
                MsgInformation("Invaild User Code")
                txtEmpName.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((txtEmpName.Text), "USER_ID", "USER_ID", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invaild User Code")
                txtEmpName.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If

        If ChkALL.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtItemName.Text) = "" Then
                MsgInformation("Invaild Item Name")
                txtItemName.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((txtItemName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblAcCode.Text = MasterNo
            Else
                MsgInformation("Invaild Item Name")
                txtItemName.Focus()
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
    'Private Sub FillPOCombo()
    'On Error GoTo FillErr2
    'Dim SqlStr As String = ""
    'Dim RS As ADODB.Recordset=Nothing
    '
    ''    cboPurType.Clear
    ''    cboPurType.AddItem "ALL"
    ''    cboPurType.AddItem "Purchase Order"
    ''    cboPurType.AddItem "Work Order"
    ''    cboPurType.AddItem "Job Order"
    ''    cboPurType.ListIndex = 0
    ''
    ''    cboOrderType.Clear
    ''    cboOrderType.AddItem "ALL"
    ''    cboOrderType.AddItem "Close"
    ''    cboOrderType.AddItem "Open"
    ''    cboOrderType.ListIndex = 0
    '
    '
    ''    Exit Sub
    'FillErr2:
    '    MsgBox err.Description
    'End Sub

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
    Private Sub txtEmpName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpName.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtEmpName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpName.DoubleClick
        SearchEmp()
    End Sub
    Private Sub txtEmpName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEmpName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtEmpName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtEmpName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtEmpName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchEmp()
    End Sub
    Private Sub chkAllEmp_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllEmp.CheckStateChanged
        Call PrintStatus(False)
        If chkAllEmp.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtEmpName.Enabled = False
            cmdsearchEmp.Enabled = False
        Else
            txtEmpName.Enabled = True
            cmdsearchEmp.Enabled = True
        End If
    End Sub
    Private Sub SearchEmp()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.SearchGridMaster(txtEmpName.Text, "ATH_PASSWORD_MST", "USER_ID", "EMP_NAME", , , SqlStr)
        If AcName <> "" Then
            txtEmpName.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdsearchEmp_Click(sender As Object, e As EventArgs) Handles cmdsearchEmp.Click
        SearchEmp()
    End Sub
    Private Sub CreateGridHeader()
        '----------------------------------------------------------------------------
        'Argument       :   Nil
        'Return Value   :   Nil
        'Function       :   to create the grid header
        'Comments       :   Nil
        '----------------------------------------------------------------------------
        Try
            Dim inti As Integer
            'create column header


            UltraGrid1.DisplayLayout.Bands(0).Columns(0).RowLayoutColumnInfo.PreferredLabelSize = New System.Drawing.Size(0, 40)
            UltraGrid1.DisplayLayout.Override.WrapHeaderText = Infragistics.Win.DefaultableBoolean.True

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Header.Caption = "Locked"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColNo - 1).Header.Caption = "No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColDate - 1).Header.Caption = "Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColType - 1).Header.Caption = "Type"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPONo - 1).Header.Caption = "PO No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPODate - 1).Header.Caption = "PO Date"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColOurSoNo - 1).Header.Caption = "Our SO No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColOurSoDate - 1).Header.Caption = "Our SO Date"

            UltraGrid1.DisplayLayout.Bands(0).Columns(colSupplier - 1).Header.Caption = "Name of The Party"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemCode - 1).Header.Caption = "Item Code"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemDesc - 1).Header.Caption = "Item Description"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemPartNo - 1).Header.Caption = "Part No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColUnit - 1).Header.Caption = "Unit"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColQty - 1).Header.Caption = "Qnty"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColNoOfPkt - 1).Header.Caption = "No of Pkt"


            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPackNo - 1).Header.Caption = "Pack Qty"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPackType - 1).Header.Caption = "Packing Type"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColTransporter - 1).Header.Caption = "Transporter Name"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColVehicleNo - 1).Header.Caption = "Vehicle No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColRefNo - 1).Header.Caption = "Ref No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColRefDate - 1).Header.Caption = "Ref Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColStockType - 1).Header.Caption = "Stock Type"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPrepareby - 1).Header.Caption = "Prepare By"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMKEY - 1).Header.Caption = "Mkey"




            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMKEY - 1).Header.Caption = "MKey"



            'UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Header.Appearance.TextHAlign = HAlign.Right

            ''enable/disable the columns
            For inti = 0 To UltraGrid1.DisplayLayout.Bands(0).Columns.Count - 1
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).CellActivation = Activation.NoEdit  ''  .AllowEdit
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).Header.Appearance.TextHAlign = HAlign.Center

                ''UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.SubDepartmentName).Style = UltraWinGrid.ColumnStyle.DropDown
                '' UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.Qty).Style = UltraWinGrid.ColumnStyle.DoubleNonNegative
                ''UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.SubDepartmentName).EditorComponent = cmbDepartment
            Next

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColQty - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColNoOfPkt - 1).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPackNo - 1).Style = UltraWinGrid.ColumnStyle.Double


            UltraGrid1.DisplayLayout.Bands(0).Columns(ColQty - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColNoOfPkt - 1).CellAppearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPackNo - 1).CellAppearance.TextHAlign = HAlign.Right



            ''for hidden
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Hidden = True
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMKEY - 1).Hidden = True


            UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColNo - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColDate - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColType - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPONo - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPODate - 1).Width = 100

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColOurSoNo - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColOurSoDate - 1).Width = 100

            UltraGrid1.DisplayLayout.Bands(0).Columns(colSupplier - 1).Width = 250
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemCode - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemDesc - 1).Width = 250
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemPartNo - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColUnit - 1).Width = 60

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColQty - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColNoOfPkt - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPackNo - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPackType - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColTransporter - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColVehicleNo - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColRefNo - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColRefDate - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColStockType - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPrepareby - 1).Width = 100


            MainClass.SetInfragisticsGrid(UltraGrid1, -1, "Filter Row", "Group Row")
        Catch ex As Exception
            ErrorMsg(ex.Message, "")
            Me.Cursor = Windows.Forms.Cursors.Default
        End Try
    End Sub
    Private Sub FillUltraGrid(pMakeSql As String)
        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim sql As String
        Dim i As Integer
        Dim inti As Integer

        'UltraGrid1.DataSource.Rows.Clear()
        Me.UltraGrid1.DataSource = Nothing
        oledbCnn = New OleDbConnection(StrConn)
        Try

            ClearGroupFromUltraGrid(UltraGrid1)
            ClearFilterFromUltraGrid(UltraGrid1)
            oledbCnn.Open()
            oledbAdapter = New OleDbDataAdapter(pMakeSql, oledbCnn)

            oledbAdapter.Fill(ds)

            ' Set the data source and data member to bind the grid.
            Me.UltraGrid1.DataSource = ds
            Me.UltraGrid1.DataMember = ""

            CreateGridHeader()

            oledbAdapter.Dispose()
            oledbCnn.Close()
        Catch ex As Exception
            MsgBox("Can not open connection ! ")
        End Try
    End Sub
    Private Sub cmdExport_Click(sender As Object, e As EventArgs) Handles cmdExport.Click

        Dim lngLoop As Integer  'loop variable

        Dim objMode As Object 'to store the mode of the row
        Dim objChk As Object 'to get the check status of the first column
        Dim strSplit() As String 'split variable
        Dim intAns As Integer ' to store the result from msgbox
        Dim lngRow As Long
        Try


            'If m_blnChangeInData = True Then
            '    MessageFromResFile(7284, MessageType.Information)
            '    GridSetFocus(UltraGrid1.ActiveRow.Tag.ToString, UltraGrid1.ActiveCell.Column.Index)
            '    Exit Sub
            'End If

            ''Please provide a location where you whould like to export the data to
            'MessageFromResFile(7304, MessageType.Information, GetLabelDes("7305"))

            Try
                SaveFileDialog1.FileName = Me.Text
            Catch
                SaveFileDialog1.FileName = "File1"
            End Try

            Dim strAction As String = ""
            Try
                strAction = SaveFileDialog1.ShowDialog()
            Catch
                SaveFileDialog1.FileName = "File1"
                strAction = SaveFileDialog1.ShowDialog()
            End Try

            If strAction = "1" Then
                ExportToExcel(SaveFileDialog1.FileName)
            End If
            'Me.Cursor = Cursors.Default

        Catch ex As Exception
            ErrorMsg(Err.Description, Err.Number)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub ExportToExcel(ByVal strFileName As String)
        '----------------------------------------------------------------------------
        'Argument       :   Nil
        'Return Value   :   Nil
        'Function       :   To export the report to excel/csv/text file
        'Comments       :   This function will be called from clickinvoked and enterpressed events
        '                   THIS FUNCTION HAS TO BE OVERRIDED IN THE DERIVED FORM   
        '----------------------------------------------------------------------------
        Me.Cursor = Cursors.WaitCursor
        Dim start As DateTime
        'Dim timespan As TimeSpan
        start = DateTime.Now
        Try
            Me.UltraGridExcelExporter1.FileLimitBehaviour = ExcelExport.FileLimitBehaviour.TruncateData
            Me.UltraGridExcelExporter1.ExportAsync(Me.UltraGrid1, strFileName & ".xls")
            ' timespan = DateTime.Now.Subtract(start)
            'Exported To File : 
            '  MessageFromResFile(7292, MessageType.Information, strFileName)
        Catch
            'Specified Path Does Not Exist,Invalid File Name
            ErrorMsg(Err.Description, Err.Number)
        End Try
        Me.Cursor = Cursors.Default
    End Sub
    Private Sub UltraGrid1_InitializeLayout(sender As Object, e As InitializeLayoutEventArgs) Handles UltraGrid1.InitializeLayout

        ''Allowing Summaries in the UltraGrid 
        e.Layout.Override.AllowRowSummaries = AllowRowSummaries.True
        '' Setting the Sum Summary for the desired column

        e.Layout.Bands(0).Summaries.Add("ColQty", SummaryType.Sum, e.Layout.Bands(0).Columns(ColQty - 1))
        e.Layout.Bands(0).Summaries.Add("ColNoOfPkt", SummaryType.Sum, e.Layout.Bands(0).Columns(ColNoOfPkt - 1))

        ''Set the display format to be just the number 
        e.Layout.Bands(0).Summaries("ColQty").DisplayFormat = "{0:###0.00}"
        e.Layout.Bands(0).Summaries("ColNoOfPkt").DisplayFormat = "{0:###0.00}"


        ''Hide the SummaryFooterCaption row 
        e.Layout.Bands(0).Override.SummaryFooterCaptionVisible = Infragistics.Win.DefaultableBoolean.False

        'band.SummaryFooterCaption = "Subtotal:"

        e.Layout.Bands(0).Override.SummaryFooterCaptionAppearance.FontData.Bold = DefaultableBoolean.True

        e.Layout.Bands(0).Override.SummaryFooterCaptionAppearance.BackColor = Color.LightSteelBlue

        e.Layout.Bands(0).Override.SummaryFooterCaptionAppearance.ForeColor = Color.Black
        '     / Here, I want to add grand total

        e.Layout.Bands(0).Summaries("ColQty").Appearance.TextHAlign = HAlign.Right
        e.Layout.Bands(0).Summaries("ColNoOfPkt").Appearance.TextHAlign = HAlign.Right


        'Disable grid default highlight

        'UltraGrid1.DisplayLayout.Override.ResetActiveRowAppearance()

        'UltraGrid1.DisplayLayout.Override.ResetActiveCellAppearance()

        'UltraGrid1.DisplayLayout.Override.ActiveAppearancesEnabled = DefaultableBoolean.False

        e.Layout.ViewStyleBand = ViewStyleBand.OutlookGroupBy
    End Sub
End Class
