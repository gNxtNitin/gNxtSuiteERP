Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmMRR
#Region "Windows Form Designer generated code "
    <System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'This form is an MDI child.
        'This code simulates the VB6 
        ' functionality of automatically
        ' loading and showing an MDI
        ' child's parent.

        'InventoryGST.Master.Show
        VB6_AddADODataBinding()
    End Sub
    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
        If Disposing Then
            VB6_RemoveADODataBinding()
            If Not components Is Nothing Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(Disposing)
    End Sub
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Public ToolTip1 As System.Windows.Forms.ToolTip
    Public WithEvents cmdShow As System.Windows.Forms.Button
    Public WithEvents chkGSTStatus As System.Windows.Forms.CheckBox
    Public WithEvents chkServiceTaxClaim As System.Windows.Forms.CheckBox
    Public WithEvents chkRejRtn As System.Windows.Forms.CheckBox
    Public WithEvents txtSendDate As System.Windows.Forms.TextBox
    Public WithEvents chkBillPassing As System.Windows.Forms.CheckBox
    Public WithEvents chkSTStatus As System.Windows.Forms.CheckBox
    Public WithEvents chkExciseStatus As System.Windows.Forms.CheckBox
    Public WithEvents chkDNote As System.Windows.Forms.CheckBox
    Public WithEvents chkScheRej As System.Windows.Forms.CheckBox
    Public WithEvents chkFOC As System.Windows.Forms.CheckBox
    Public WithEvents chkPacking As System.Windows.Forms.CheckBox
    Public WithEvents chkMrrSend As System.Windows.Forms.CheckBox
    Public WithEvents lblModDate As System.Windows.Forms.Label
    Public WithEvents Label48 As System.Windows.Forms.Label
    Public WithEvents lblAddDate As System.Windows.Forms.Label
    Public WithEvents Label45 As System.Windows.Forms.Label
    Public WithEvents lblModUser As System.Windows.Forms.Label
    Public WithEvents Label46 As System.Windows.Forms.Label
    Public WithEvents lblAddUser As System.Windows.Forms.Label
    Public WithEvents Label44 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents FraDetail As System.Windows.Forms.GroupBox
    Public WithEvents txtScanning As System.Windows.Forms.TextBox
    Public WithEvents cboRefType As System.Windows.Forms.ComboBox
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents chkUnderChallan As System.Windows.Forms.CheckBox
    Public WithEvents cmdResetMRR As System.Windows.Forms.Button
    Public WithEvents txtGateDate As System.Windows.Forms.TextBox
    Public WithEvents txtGateNo As System.Windows.Forms.TextBox
    Public WithEvents cmdGateSearch As System.Windows.Forms.Button
    Public WithEvents cboDivision As System.Windows.Forms.ComboBox
    Public WithEvents TxtRemarks As System.Windows.Forms.TextBox
    Public WithEvents cmdsearch As System.Windows.Forms.Button
    Public WithEvents cmdMRRSearch As System.Windows.Forms.Button
    Public WithEvents chkQC As System.Windows.Forms.CheckBox
    Public WithEvents chkCancelled As System.Windows.Forms.CheckBox
    Public WithEvents txtEwayBillNo As System.Windows.Forms.TextBox
    Public WithEvents TxtItemDesc As System.Windows.Forms.TextBox
    Public WithEvents TxtSupplier As System.Windows.Forms.TextBox
    Public WithEvents txtMRRNo As System.Windows.Forms.TextBox
    Public WithEvents txtMRRDate As System.Windows.Forms.TextBox
    Public WithEvents txtBillNo As System.Windows.Forms.TextBox
    Public WithEvents txtBillDate As System.Windows.Forms.TextBox
    Public WithEvents Label20 As System.Windows.Forms.Label
    Public WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label25 As System.Windows.Forms.Label
    Public WithEvents lblEntryDate As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label23 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents LblMkey As System.Windows.Forms.Label
    Public WithEvents Frasupp As System.Windows.Forms.GroupBox
    Public WithEvents CboPONo As System.Windows.Forms.ComboBox
    Public WithEvents FraPO As System.Windows.Forms.GroupBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents _SSTab1_TabPage0 As System.Windows.Forms.TabPage
    Public WithEvents txtST38No As System.Windows.Forms.TextBox
    Public WithEvents txtShippedTo As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchShippedTo As System.Windows.Forms.Button
    Public WithEvents chkShipTo As System.Windows.Forms.CheckBox
    Public WithEvents chkPrimiumFreight As System.Windows.Forms.CheckBox
    Public WithEvents txtTripDate As System.Windows.Forms.TextBox
    Public WithEvents txtTripNo As System.Windows.Forms.TextBox
    Public WithEvents cboMode As System.Windows.Forms.ComboBox
    Public WithEvents txtGRNo As System.Windows.Forms.TextBox
    Public WithEvents txtGRDate As System.Windows.Forms.TextBox
    Public WithEvents TxtTransporter As System.Windows.Forms.TextBox
    Public WithEvents txtDocsThru As System.Windows.Forms.TextBox
    Public WithEvents _OptFreight_0 As System.Windows.Forms.RadioButton
    Public WithEvents _OptFreight_1 As System.Windows.Forms.RadioButton
    Public WithEvents txtFreight As System.Windows.Forms.TextBox
    Public WithEvents txtFormDetail As System.Windows.Forms.TextBox
    Public WithEvents txtVehicle As System.Windows.Forms.TextBox
    Public WithEvents Label26 As System.Windows.Forms.Label
    Public WithEvents Label24 As System.Windows.Forms.Label
    Public WithEvents Label22 As System.Windows.Forms.Label
    Public WithEvents Label21 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label27 As System.Windows.Forms.Label
    Public WithEvents Label28 As System.Windows.Forms.Label
    Public WithEvents Label29 As System.Windows.Forms.Label
    Public WithEvents Label31 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents fraFreight As System.Windows.Forms.GroupBox
    Public WithEvents _SSTab1_TabPage1 As System.Windows.Forms.TabPage
    Public WithEvents cmdTPShow As System.Windows.Forms.Button
    Public WithEvents cmdTCShow As System.Windows.Forms.Button
    Public WithEvents chkTPRAvailable As System.Windows.Forms.CheckBox
    Public WithEvents chkTCAvailable As System.Windows.Forms.CheckBox
    Public WithEvents cmdTPRI As System.Windows.Forms.Button
    Public WithEvents txtTPRPath As System.Windows.Forms.TextBox
    Public WithEvents cmdTC As System.Windows.Forms.Button
    Public WithEvents txtTCPath As System.Windows.Forms.TextBox
    Public cdgFilePathOpen As System.Windows.Forms.OpenFileDialog
    Public cdgFilePathSave As System.Windows.Forms.SaveFileDialog
    Public cdgFilePathFont As System.Windows.Forms.FontDialog
    Public cdgFilePathColor As System.Windows.Forms.ColorDialog
    Public cdgFilePathPrint As System.Windows.Forms.PrintDialog
    Public WithEvents Label36 As System.Windows.Forms.Label
    Public WithEvents Label35 As System.Windows.Forms.Label
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents _SSTab1_TabPage2 As System.Windows.Forms.TabPage
    Public WithEvents SSTab1 As System.Windows.Forms.TabControl
    Public WithEvents SprdExp As AxFPSpreadADO.AxfpSpread
    Public WithEvents lblSaleReturn As System.Windows.Forms.Label
    Public WithEvents lblEDUAmount As System.Windows.Forms.Label
    Public WithEvents lblEDUPercent As System.Windows.Forms.Label
    Public WithEvents lblTotPackQtyCap As System.Windows.Forms.Label
    Public WithEvents lblTotQty As System.Windows.Forms.Label
    Public WithEvents lblNetAmount As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents lblTotItemValue As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents lblCGST As System.Windows.Forms.Label
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents lblSGST As System.Windows.Forms.Label
    Public WithEvents Label34 As System.Windows.Forms.Label
    Public WithEvents lblTotCharges As System.Windows.Forms.Label
    Public WithEvents lblTotFreight As System.Windows.Forms.Label
    Public WithEvents lblTotExpAmt As System.Windows.Forms.Label
    Public WithEvents lblEDPercentage As System.Windows.Forms.Label
    Public WithEvents lblSTPercentage As System.Windows.Forms.Label
    Public WithEvents lblTotTaxableAmt As System.Windows.Forms.Label
    Public WithEvents lblDiscount As System.Windows.Forms.Label
    Public WithEvents lblSurcharge As System.Windows.Forms.Label
    Public WithEvents lblRO As System.Windows.Forms.Label
    Public WithEvents lblMSC As System.Windows.Forms.Label
    Public WithEvents Frasprd As System.Windows.Forms.GroupBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents FraFront As System.Windows.Forms.GroupBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents cmdDispcrepancy As System.Windows.Forms.Button
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
    Public WithEvents cmdDetail As System.Windows.Forms.Button
    Public WithEvents cmdDelete As System.Windows.Forms.Button
    Public WithEvents cmdSave As System.Windows.Forms.Button
    Public WithEvents cmdModify As System.Windows.Forms.Button
    Public WithEvents cmdAdd As System.Windows.Forms.Button
    Public WithEvents lblBookType As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents OptFreight As VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMRR))
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance11 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance12 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.cmdResetMRR = New System.Windows.Forms.Button()
        Me.cmdGateSearch = New System.Windows.Forms.Button()
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.cmdMRRSearch = New System.Windows.Forms.Button()
        Me.cmdSearchShippedTo = New System.Windows.Forms.Button()
        Me.cmdTPRI = New System.Windows.Forms.Button()
        Me.cmdTC = New System.Windows.Forms.Button()
        Me.cmdDispcrepancy = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.cmdDetail = New System.Windows.Forms.Button()
        Me.cmdDelete = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdModify = New System.Windows.Forms.Button()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.FraFront = New System.Windows.Forms.GroupBox()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.cboRefType = New System.Windows.Forms.ComboBox()
        Me.Frasupp = New System.Windows.Forms.GroupBox()
        Me.txtBillTo = New System.Windows.Forms.TextBox()
        Me.chkUnderChallan = New System.Windows.Forms.CheckBox()
        Me.txtGateDate = New System.Windows.Forms.TextBox()
        Me.txtGateNo = New System.Windows.Forms.TextBox()
        Me.cboDivision = New System.Windows.Forms.ComboBox()
        Me.TxtRemarks = New System.Windows.Forms.TextBox()
        Me.chkQC = New System.Windows.Forms.CheckBox()
        Me.chkCancelled = New System.Windows.Forms.CheckBox()
        Me.txtEwayBillNo = New System.Windows.Forms.TextBox()
        Me.TxtItemDesc = New System.Windows.Forms.TextBox()
        Me.TxtSupplier = New System.Windows.Forms.TextBox()
        Me.txtMRRNo = New System.Windows.Forms.TextBox()
        Me.txtMRRDate = New System.Windows.Forms.TextBox()
        Me.txtBillNo = New System.Windows.Forms.TextBox()
        Me.txtBillDate = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.lblEntryDate = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.LblMkey = New System.Windows.Forms.Label()
        Me.FraPO = New System.Windows.Forms.GroupBox()
        Me.CboPONo = New System.Windows.Forms.ComboBox()
        Me.Frasprd = New System.Windows.Forms.GroupBox()
        Me.FraDetail = New System.Windows.Forms.GroupBox()
        Me.chkGSTStatus = New System.Windows.Forms.CheckBox()
        Me.chkServiceTaxClaim = New System.Windows.Forms.CheckBox()
        Me.chkRejRtn = New System.Windows.Forms.CheckBox()
        Me.txtSendDate = New System.Windows.Forms.TextBox()
        Me.chkBillPassing = New System.Windows.Forms.CheckBox()
        Me.chkSTStatus = New System.Windows.Forms.CheckBox()
        Me.chkExciseStatus = New System.Windows.Forms.CheckBox()
        Me.chkDNote = New System.Windows.Forms.CheckBox()
        Me.chkScheRej = New System.Windows.Forms.CheckBox()
        Me.chkFOC = New System.Windows.Forms.CheckBox()
        Me.chkPacking = New System.Windows.Forms.CheckBox()
        Me.chkMrrSend = New System.Windows.Forms.CheckBox()
        Me.lblModDate = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.lblAddDate = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.lblModUser = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.lblAddUser = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblIGST = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.SSTab1 = New System.Windows.Forms.TabControl()
        Me._SSTab1_TabPage0 = New System.Windows.Forms.TabPage()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me._SSTab1_TabPage1 = New System.Windows.Forms.TabPage()
        Me.fraFreight = New System.Windows.Forms.GroupBox()
        Me.txtDeliveryToLoc = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtDeliveryTo = New System.Windows.Forms.TextBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.TxtShipTo = New System.Windows.Forms.TextBox()
        Me.txtST38No = New System.Windows.Forms.TextBox()
        Me.txtShippedTo = New System.Windows.Forms.TextBox()
        Me.chkShipTo = New System.Windows.Forms.CheckBox()
        Me.chkPrimiumFreight = New System.Windows.Forms.CheckBox()
        Me.txtTripDate = New System.Windows.Forms.TextBox()
        Me.txtTripNo = New System.Windows.Forms.TextBox()
        Me.cboMode = New System.Windows.Forms.ComboBox()
        Me.txtGRNo = New System.Windows.Forms.TextBox()
        Me.txtGRDate = New System.Windows.Forms.TextBox()
        Me.TxtTransporter = New System.Windows.Forms.TextBox()
        Me.txtDocsThru = New System.Windows.Forms.TextBox()
        Me._OptFreight_0 = New System.Windows.Forms.RadioButton()
        Me._OptFreight_1 = New System.Windows.Forms.RadioButton()
        Me.txtFreight = New System.Windows.Forms.TextBox()
        Me.txtFormDetail = New System.Windows.Forms.TextBox()
        Me.txtVehicle = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me._SSTab1_TabPage2 = New System.Windows.Forms.TabPage()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.cmdTPShow = New System.Windows.Forms.Button()
        Me.cmdTCShow = New System.Windows.Forms.Button()
        Me.chkTPRAvailable = New System.Windows.Forms.CheckBox()
        Me.chkTCAvailable = New System.Windows.Forms.CheckBox()
        Me.txtTPRPath = New System.Windows.Forms.TextBox()
        Me.txtTCPath = New System.Windows.Forms.TextBox()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.SprdExp = New AxFPSpreadADO.AxfpSpread()
        Me.lblSaleReturn = New System.Windows.Forms.Label()
        Me.lblEDUAmount = New System.Windows.Forms.Label()
        Me.lblEDUPercent = New System.Windows.Forms.Label()
        Me.lblTotPackQtyCap = New System.Windows.Forms.Label()
        Me.lblTotQty = New System.Windows.Forms.Label()
        Me.lblNetAmount = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblTotItemValue = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblCGST = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.lblSGST = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.lblTotCharges = New System.Windows.Forms.Label()
        Me.lblTotFreight = New System.Windows.Forms.Label()
        Me.lblTotExpAmt = New System.Windows.Forms.Label()
        Me.lblEDPercentage = New System.Windows.Forms.Label()
        Me.lblSTPercentage = New System.Windows.Forms.Label()
        Me.lblTotTaxableAmt = New System.Windows.Forms.Label()
        Me.lblDiscount = New System.Windows.Forms.Label()
        Me.lblSurcharge = New System.Windows.Forms.Label()
        Me.lblRO = New System.Windows.Forms.Label()
        Me.lblMSC = New System.Windows.Forms.Label()
        Me.txtScanning = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cdgFilePathOpen = New System.Windows.Forms.OpenFileDialog()
        Me.cdgFilePathSave = New System.Windows.Forms.SaveFileDialog()
        Me.cdgFilePathFont = New System.Windows.Forms.FontDialog()
        Me.cdgFilePathColor = New System.Windows.Forms.ColorDialog()
        Me.cdgFilePathPrint = New System.Windows.Forms.PrintDialog()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.OptFreight = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.UltraGrid1 = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.FraFront.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.Frasupp.SuspendLayout()
        Me.FraPO.SuspendLayout()
        Me.Frasprd.SuspendLayout()
        Me.FraDetail.SuspendLayout()
        Me.SSTab1.SuspendLayout()
        Me._SSTab1_TabPage0.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage1.SuspendLayout()
        Me.fraFreight.SuspendLayout()
        Me._SSTab1_TabPage2.SuspendLayout()
        Me.Frame4.SuspendLayout()
        CType(Me.SprdExp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        CType(Me.OptFreight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdShow
        '
        Me.cmdShow.BackColor = System.Drawing.SystemColors.Control
        Me.cmdShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdShow.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdShow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdShow.Image = CType(resources.GetObject("cmdShow.Image"), System.Drawing.Image)
        Me.cmdShow.Location = New System.Drawing.Point(714, 546)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShow.Size = New System.Drawing.Size(38, 22)
        Me.cmdShow.TabIndex = 97
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'cmdResetMRR
        '
        Me.cmdResetMRR.BackColor = System.Drawing.SystemColors.Control
        Me.cmdResetMRR.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdResetMRR.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdResetMRR.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdResetMRR.Image = CType(resources.GetObject("cmdResetMRR.Image"), System.Drawing.Image)
        Me.cmdResetMRR.Location = New System.Drawing.Point(314, 13)
        Me.cmdResetMRR.Name = "cmdResetMRR"
        Me.cmdResetMRR.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdResetMRR.Size = New System.Drawing.Size(28, 23)
        Me.cmdResetMRR.TabIndex = 6
        Me.cmdResetMRR.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdResetMRR, "Preview")
        Me.cmdResetMRR.UseVisualStyleBackColor = False
        '
        'cmdGateSearch
        '
        Me.cmdGateSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdGateSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdGateSearch.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdGateSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdGateSearch.Image = CType(resources.GetObject("cmdGateSearch.Image"), System.Drawing.Image)
        Me.cmdGateSearch.Location = New System.Drawing.Point(164, 13)
        Me.cmdGateSearch.Name = "cmdGateSearch"
        Me.cmdGateSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdGateSearch.Size = New System.Drawing.Size(28, 23)
        Me.cmdGateSearch.TabIndex = 4
        Me.cmdGateSearch.TabStop = False
        Me.cmdGateSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdGateSearch, "Search")
        Me.cmdGateSearch.UseVisualStyleBackColor = False
        '
        'cmdsearch
        '
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.Location = New System.Drawing.Point(448, 73)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(28, 23)
        Me.cmdsearch.TabIndex = 12
        Me.cmdsearch.TabStop = False
        Me.cmdsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearch, "Search")
        Me.cmdsearch.UseVisualStyleBackColor = False
        '
        'cmdMRRSearch
        '
        Me.cmdMRRSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdMRRSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdMRRSearch.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdMRRSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMRRSearch.Image = CType(resources.GetObject("cmdMRRSearch.Image"), System.Drawing.Image)
        Me.cmdMRRSearch.Location = New System.Drawing.Point(164, 42)
        Me.cmdMRRSearch.Name = "cmdMRRSearch"
        Me.cmdMRRSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdMRRSearch.Size = New System.Drawing.Size(28, 23)
        Me.cmdMRRSearch.TabIndex = 9
        Me.cmdMRRSearch.TabStop = False
        Me.cmdMRRSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdMRRSearch, "Search")
        Me.cmdMRRSearch.UseVisualStyleBackColor = False
        '
        'cmdSearchShippedTo
        '
        Me.cmdSearchShippedTo.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchShippedTo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchShippedTo.Enabled = False
        Me.cmdSearchShippedTo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchShippedTo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchShippedTo.Image = CType(resources.GetObject("cmdSearchShippedTo.Image"), System.Drawing.Image)
        Me.cmdSearchShippedTo.Location = New System.Drawing.Point(371, 175)
        Me.cmdSearchShippedTo.Name = "cmdSearchShippedTo"
        Me.cmdSearchShippedTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchShippedTo.Size = New System.Drawing.Size(28, 23)
        Me.cmdSearchShippedTo.TabIndex = 131
        Me.cmdSearchShippedTo.TabStop = False
        Me.cmdSearchShippedTo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchShippedTo, "Search")
        Me.cmdSearchShippedTo.UseVisualStyleBackColor = False
        '
        'cmdTPRI
        '
        Me.cmdTPRI.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdTPRI.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdTPRI.Enabled = False
        Me.cmdTPRI.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdTPRI.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdTPRI.Image = CType(resources.GetObject("cmdTPRI.Image"), System.Drawing.Image)
        Me.cmdTPRI.Location = New System.Drawing.Point(594, 102)
        Me.cmdTPRI.Name = "cmdTPRI"
        Me.cmdTPRI.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdTPRI.Size = New System.Drawing.Size(28, 23)
        Me.cmdTPRI.TabIndex = 139
        Me.cmdTPRI.TabStop = False
        Me.cmdTPRI.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdTPRI, "Search")
        Me.cmdTPRI.UseVisualStyleBackColor = False
        '
        'cmdTC
        '
        Me.cmdTC.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdTC.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdTC.Enabled = False
        Me.cmdTC.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdTC.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdTC.Image = CType(resources.GetObject("cmdTC.Image"), System.Drawing.Image)
        Me.cmdTC.Location = New System.Drawing.Point(594, 44)
        Me.cmdTC.Name = "cmdTC"
        Me.cmdTC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdTC.Size = New System.Drawing.Size(28, 23)
        Me.cmdTC.TabIndex = 137
        Me.cmdTC.TabStop = False
        Me.cmdTC.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdTC, "Search")
        Me.cmdTC.UseVisualStyleBackColor = False
        '
        'cmdDispcrepancy
        '
        Me.cmdDispcrepancy.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDispcrepancy.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDispcrepancy.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDispcrepancy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDispcrepancy.Image = CType(resources.GetObject("cmdDispcrepancy.Image"), System.Drawing.Image)
        Me.cmdDispcrepancy.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdDispcrepancy.Location = New System.Drawing.Point(452, 14)
        Me.cmdDispcrepancy.Name = "cmdDispcrepancy"
        Me.cmdDispcrepancy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDispcrepancy.Size = New System.Drawing.Size(67, 37)
        Me.cmdDispcrepancy.TabIndex = 41
        Me.cmdDispcrepancy.Text = "Discrepancy"
        Me.cmdDispcrepancy.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdDispcrepancy, "Save & Print")
        Me.cmdDispcrepancy.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdClose.Location = New System.Drawing.Point(718, 14)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(67, 37)
        Me.cmdClose.TabIndex = 46
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdClose, "Close the form")
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'CmdView
        '
        Me.CmdView.BackColor = System.Drawing.SystemColors.Menu
        Me.CmdView.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdView.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
        Me.CmdView.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdView.Location = New System.Drawing.Point(652, 14)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(67, 37)
        Me.CmdView.TabIndex = 45
        Me.CmdView.Text = "List &View"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdView, "View Listing")
        Me.CmdView.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdPreview.Location = New System.Drawing.Point(586, 14)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.TabIndex = 44
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Preview")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdPrint.Location = New System.Drawing.Point(519, 14)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 43
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.Location = New System.Drawing.Point(388, 13)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdSavePrint.TabIndex = 42
        Me.cmdSavePrint.Text = "Save && Print"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save & Print")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'cmdDetail
        '
        Me.cmdDetail.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDetail.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDetail.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDetail.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDetail.Image = CType(resources.GetObject("cmdDetail.Image"), System.Drawing.Image)
        Me.cmdDetail.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdDetail.Location = New System.Drawing.Point(386, 14)
        Me.cmdDetail.Name = "cmdDetail"
        Me.cmdDetail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDetail.Size = New System.Drawing.Size(67, 37)
        Me.cmdDetail.TabIndex = 40
        Me.cmdDetail.Text = "Detail"
        Me.cmdDetail.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdDetail, "Save & Print")
        Me.cmdDetail.UseVisualStyleBackColor = False
        '
        'cmdDelete
        '
        Me.cmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDelete.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDelete.Image = CType(resources.GetObject("cmdDelete.Image"), System.Drawing.Image)
        Me.cmdDelete.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdDelete.Location = New System.Drawing.Point(319, 14)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDelete.Size = New System.Drawing.Size(67, 37)
        Me.cmdDelete.TabIndex = 39
        Me.cmdDelete.Text = "&Delete"
        Me.cmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdDelete, "Delete")
        Me.cmdDelete.UseVisualStyleBackColor = False
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Image = CType(resources.GetObject("cmdSave.Image"), System.Drawing.Image)
        Me.cmdSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdSave.Location = New System.Drawing.Point(252, 14)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(67, 37)
        Me.cmdSave.TabIndex = 38
        Me.cmdSave.Text = "&Save"
        Me.cmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSave, "Save Current Record")
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'cmdModify
        '
        Me.cmdModify.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdModify.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdModify.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdModify.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdModify.Image = CType(resources.GetObject("cmdModify.Image"), System.Drawing.Image)
        Me.cmdModify.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdModify.Location = New System.Drawing.Point(185, 14)
        Me.cmdModify.Name = "cmdModify"
        Me.cmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdModify.Size = New System.Drawing.Size(67, 37)
        Me.cmdModify.TabIndex = 37
        Me.cmdModify.Text = "&Modify"
        Me.cmdModify.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdModify, "Modify ")
        Me.cmdModify.UseVisualStyleBackColor = False
        '
        'cmdAdd
        '
        Me.cmdAdd.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdAdd.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAdd.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAdd.Image = CType(resources.GetObject("cmdAdd.Image"), System.Drawing.Image)
        Me.cmdAdd.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdAdd.Location = New System.Drawing.Point(118, 14)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAdd.Size = New System.Drawing.Size(67, 37)
        Me.cmdAdd.TabIndex = 0
        Me.cmdAdd.Text = "&Add"
        Me.cmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdAdd, "Add New")
        Me.cmdAdd.UseVisualStyleBackColor = False
        '
        'FraFront
        '
        Me.FraFront.BackColor = System.Drawing.SystemColors.Control
        Me.FraFront.Controls.Add(Me.Frame1)
        Me.FraFront.Controls.Add(Me.Frasupp)
        Me.FraFront.Controls.Add(Me.FraPO)
        Me.FraFront.Controls.Add(Me.Frasprd)
        Me.FraFront.Controls.Add(Me.cmdShow)
        Me.FraFront.Controls.Add(Me.txtScanning)
        Me.FraFront.Controls.Add(Me.Label2)
        Me.FraFront.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraFront.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraFront.Location = New System.Drawing.Point(-1, -4)
        Me.FraFront.Name = "FraFront"
        Me.FraFront.Padding = New System.Windows.Forms.Padding(0)
        Me.FraFront.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraFront.Size = New System.Drawing.Size(913, 572)
        Me.FraFront.TabIndex = 50
        Me.FraFront.TabStop = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.cboRefType)
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 4)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(178, 42)
        Me.Frame1.TabIndex = 61
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Ref Type"
        '
        'cboRefType
        '
        Me.cboRefType.BackColor = System.Drawing.SystemColors.Window
        Me.cboRefType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboRefType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRefType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboRefType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboRefType.Location = New System.Drawing.Point(6, 14)
        Me.cboRefType.Name = "cboRefType"
        Me.cboRefType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboRefType.Size = New System.Drawing.Size(168, 21)
        Me.cboRefType.TabIndex = 1
        '
        'Frasupp
        '
        Me.Frasupp.BackColor = System.Drawing.SystemColors.Control
        Me.Frasupp.Controls.Add(Me.txtBillTo)
        Me.Frasupp.Controls.Add(Me.chkUnderChallan)
        Me.Frasupp.Controls.Add(Me.cmdResetMRR)
        Me.Frasupp.Controls.Add(Me.txtGateDate)
        Me.Frasupp.Controls.Add(Me.txtGateNo)
        Me.Frasupp.Controls.Add(Me.cmdGateSearch)
        Me.Frasupp.Controls.Add(Me.cboDivision)
        Me.Frasupp.Controls.Add(Me.TxtRemarks)
        Me.Frasupp.Controls.Add(Me.cmdsearch)
        Me.Frasupp.Controls.Add(Me.cmdMRRSearch)
        Me.Frasupp.Controls.Add(Me.chkQC)
        Me.Frasupp.Controls.Add(Me.chkCancelled)
        Me.Frasupp.Controls.Add(Me.txtEwayBillNo)
        Me.Frasupp.Controls.Add(Me.TxtItemDesc)
        Me.Frasupp.Controls.Add(Me.TxtSupplier)
        Me.Frasupp.Controls.Add(Me.txtMRRNo)
        Me.Frasupp.Controls.Add(Me.txtMRRDate)
        Me.Frasupp.Controls.Add(Me.txtBillNo)
        Me.Frasupp.Controls.Add(Me.txtBillDate)
        Me.Frasupp.Controls.Add(Me.Label20)
        Me.Frasupp.Controls.Add(Me.Label19)
        Me.Frasupp.Controls.Add(Me.Label12)
        Me.Frasupp.Controls.Add(Me.Label25)
        Me.Frasupp.Controls.Add(Me.lblEntryDate)
        Me.Frasupp.Controls.Add(Me.Label7)
        Me.Frasupp.Controls.Add(Me.Label23)
        Me.Frasupp.Controls.Add(Me.Label14)
        Me.Frasupp.Controls.Add(Me.Label15)
        Me.Frasupp.Controls.Add(Me.Label5)
        Me.Frasupp.Controls.Add(Me.Label6)
        Me.Frasupp.Controls.Add(Me.Label4)
        Me.Frasupp.Controls.Add(Me.LblMkey)
        Me.Frasupp.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frasupp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frasupp.Location = New System.Drawing.Point(180, 4)
        Me.Frasupp.Name = "Frasupp"
        Me.Frasupp.Padding = New System.Windows.Forms.Padding(0)
        Me.Frasupp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frasupp.Size = New System.Drawing.Size(728, 162)
        Me.Frasupp.TabIndex = 53
        Me.Frasupp.TabStop = False
        '
        'txtBillTo
        '
        Me.txtBillTo.AcceptsReturn = True
        Me.txtBillTo.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillTo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillTo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillTo.ForeColor = System.Drawing.Color.Blue
        Me.txtBillTo.Location = New System.Drawing.Point(476, 74)
        Me.txtBillTo.MaxLength = 0
        Me.txtBillTo.Name = "txtBillTo"
        Me.txtBillTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillTo.Size = New System.Drawing.Size(110, 22)
        Me.txtBillTo.TabIndex = 146
        '
        'chkUnderChallan
        '
        Me.chkUnderChallan.BackColor = System.Drawing.SystemColors.Control
        Me.chkUnderChallan.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkUnderChallan.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkUnderChallan.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkUnderChallan.Location = New System.Drawing.Point(609, 85)
        Me.chkUnderChallan.Name = "chkUnderChallan"
        Me.chkUnderChallan.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkUnderChallan.Size = New System.Drawing.Size(103, 15)
        Me.chkUnderChallan.TabIndex = 13
        Me.chkUnderChallan.Text = "Under Challan"
        Me.chkUnderChallan.UseVisualStyleBackColor = False
        '
        'txtGateDate
        '
        Me.txtGateDate.AcceptsReturn = True
        Me.txtGateDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtGateDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGateDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGateDate.Enabled = False
        Me.txtGateDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGateDate.ForeColor = System.Drawing.Color.Blue
        Me.txtGateDate.Location = New System.Drawing.Point(230, 13)
        Me.txtGateDate.MaxLength = 0
        Me.txtGateDate.Name = "txtGateDate"
        Me.txtGateDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGateDate.Size = New System.Drawing.Size(81, 22)
        Me.txtGateDate.TabIndex = 5
        '
        'txtGateNo
        '
        Me.txtGateNo.AcceptsReturn = True
        Me.txtGateNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtGateNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGateNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGateNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGateNo.ForeColor = System.Drawing.Color.Blue
        Me.txtGateNo.Location = New System.Drawing.Point(78, 13)
        Me.txtGateNo.MaxLength = 0
        Me.txtGateNo.Name = "txtGateNo"
        Me.txtGateNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGateNo.Size = New System.Drawing.Size(85, 22)
        Me.txtGateNo.TabIndex = 3
        '
        'cboDivision
        '
        Me.cboDivision.BackColor = System.Drawing.SystemColors.Window
        Me.cboDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivision.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDivision.Location = New System.Drawing.Point(514, 13)
        Me.cboDivision.Name = "cboDivision"
        Me.cboDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivision.Size = New System.Drawing.Size(195, 21)
        Me.cboDivision.TabIndex = 7
        '
        'TxtRemarks
        '
        Me.TxtRemarks.AcceptsReturn = True
        Me.TxtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.TxtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtRemarks.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRemarks.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtRemarks.Location = New System.Drawing.Point(514, 133)
        Me.TxtRemarks.MaxLength = 0
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtRemarks.Size = New System.Drawing.Size(195, 22)
        Me.TxtRemarks.TabIndex = 20
        '
        'chkQC
        '
        Me.chkQC.BackColor = System.Drawing.SystemColors.Control
        Me.chkQC.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkQC.Enabled = False
        Me.chkQC.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkQC.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkQC.Location = New System.Drawing.Point(609, 43)
        Me.chkQC.Name = "chkQC"
        Me.chkQC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkQC.Size = New System.Drawing.Size(79, 15)
        Me.chkQC.TabIndex = 14
        Me.chkQC.Text = "QC Done"
        Me.chkQC.UseVisualStyleBackColor = False
        '
        'chkCancelled
        '
        Me.chkCancelled.BackColor = System.Drawing.SystemColors.Control
        Me.chkCancelled.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCancelled.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCancelled.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCancelled.Location = New System.Drawing.Point(609, 64)
        Me.chkCancelled.Name = "chkCancelled"
        Me.chkCancelled.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCancelled.Size = New System.Drawing.Size(117, 15)
        Me.chkCancelled.TabIndex = 15
        Me.chkCancelled.Text = "MRR Cancelled "
        Me.chkCancelled.UseVisualStyleBackColor = False
        '
        'txtEwayBillNo
        '
        Me.txtEwayBillNo.AcceptsReturn = True
        Me.txtEwayBillNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtEwayBillNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEwayBillNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEwayBillNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEwayBillNo.ForeColor = System.Drawing.Color.Blue
        Me.txtEwayBillNo.Location = New System.Drawing.Point(514, 103)
        Me.txtEwayBillNo.MaxLength = 0
        Me.txtEwayBillNo.Name = "txtEwayBillNo"
        Me.txtEwayBillNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEwayBillNo.Size = New System.Drawing.Size(195, 22)
        Me.txtEwayBillNo.TabIndex = 18
        '
        'TxtItemDesc
        '
        Me.TxtItemDesc.AcceptsReturn = True
        Me.TxtItemDesc.BackColor = System.Drawing.SystemColors.Window
        Me.TxtItemDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtItemDesc.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtItemDesc.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtItemDesc.ForeColor = System.Drawing.Color.Blue
        Me.TxtItemDesc.Location = New System.Drawing.Point(78, 133)
        Me.TxtItemDesc.MaxLength = 0
        Me.TxtItemDesc.Name = "TxtItemDesc"
        Me.TxtItemDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtItemDesc.Size = New System.Drawing.Size(268, 22)
        Me.TxtItemDesc.TabIndex = 19
        '
        'TxtSupplier
        '
        Me.TxtSupplier.AcceptsReturn = True
        Me.TxtSupplier.BackColor = System.Drawing.SystemColors.Window
        Me.TxtSupplier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtSupplier.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtSupplier.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSupplier.ForeColor = System.Drawing.Color.Blue
        Me.TxtSupplier.Location = New System.Drawing.Point(78, 73)
        Me.TxtSupplier.MaxLength = 0
        Me.TxtSupplier.Name = "TxtSupplier"
        Me.TxtSupplier.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtSupplier.Size = New System.Drawing.Size(369, 22)
        Me.TxtSupplier.TabIndex = 11
        '
        'txtMRRNo
        '
        Me.txtMRRNo.AcceptsReturn = True
        Me.txtMRRNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtMRRNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMRRNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMRRNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMRRNo.ForeColor = System.Drawing.Color.Blue
        Me.txtMRRNo.Location = New System.Drawing.Point(78, 43)
        Me.txtMRRNo.MaxLength = 0
        Me.txtMRRNo.Name = "txtMRRNo"
        Me.txtMRRNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMRRNo.Size = New System.Drawing.Size(85, 22)
        Me.txtMRRNo.TabIndex = 8
        '
        'txtMRRDate
        '
        Me.txtMRRDate.AcceptsReturn = True
        Me.txtMRRDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtMRRDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMRRDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMRRDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMRRDate.ForeColor = System.Drawing.Color.Blue
        Me.txtMRRDate.Location = New System.Drawing.Point(230, 43)
        Me.txtMRRDate.MaxLength = 0
        Me.txtMRRDate.Name = "txtMRRDate"
        Me.txtMRRDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMRRDate.Size = New System.Drawing.Size(81, 22)
        Me.txtMRRDate.TabIndex = 10
        '
        'txtBillNo
        '
        Me.txtBillNo.AcceptsReturn = True
        Me.txtBillNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillNo.ForeColor = System.Drawing.Color.Blue
        Me.txtBillNo.Location = New System.Drawing.Point(78, 103)
        Me.txtBillNo.MaxLength = 0
        Me.txtBillNo.Name = "txtBillNo"
        Me.txtBillNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillNo.Size = New System.Drawing.Size(115, 22)
        Me.txtBillNo.TabIndex = 16
        '
        'txtBillDate
        '
        Me.txtBillDate.AcceptsReturn = True
        Me.txtBillDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillDate.ForeColor = System.Drawing.Color.Blue
        Me.txtBillDate.Location = New System.Drawing.Point(230, 103)
        Me.txtBillDate.MaxLength = 0
        Me.txtBillDate.Name = "txtBillDate"
        Me.txtBillDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillDate.Size = New System.Drawing.Size(81, 22)
        Me.txtBillDate.TabIndex = 17
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(194, 16)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(37, 13)
        Me.Label20.TabIndex = 124
        Me.Label20.Text = "Date :"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(20, 16)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(55, 13)
        Me.Label19.TabIndex = 123
        Me.Label19.Text = "Gate No :"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(456, 17)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(54, 13)
        Me.Label12.TabIndex = 122
        Me.Label12.Text = "Division :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.SystemColors.Control
        Me.Label25.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label25.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label25.Location = New System.Drawing.Point(453, 137)
        Me.Label25.Name = "Label25"
        Me.Label25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label25.Size = New System.Drawing.Size(57, 13)
        Me.Label25.TabIndex = 115
        Me.Label25.Text = "Remarks :"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblEntryDate
        '
        Me.lblEntryDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblEntryDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblEntryDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblEntryDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEntryDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblEntryDate.Location = New System.Drawing.Point(314, 43)
        Me.lblEntryDate.Name = "lblEntryDate"
        Me.lblEntryDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblEntryDate.Size = New System.Drawing.Size(157, 19)
        Me.lblEntryDate.TabIndex = 101
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(431, 107)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(79, 13)
        Me.Label7.TabIndex = 62
        Me.Label7.Text = "eWay Bill No :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.SystemColors.Control
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(12, 137)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(63, 13)
        Me.Label23.TabIndex = 60
        Me.Label23.Text = "Item Desc :"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(20, 48)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(55, 13)
        Me.Label14.TabIndex = 59
        Me.Label14.Text = "MRR No :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(194, 45)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(37, 13)
        Me.Label15.TabIndex = 58
        Me.Label15.Text = "Date :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(28, 105)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(47, 13)
        Me.Label5.TabIndex = 57
        Me.Label5.Text = "Bill No :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(194, 105)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(37, 13)
        Me.Label6.TabIndex = 56
        Me.Label6.Text = "Date :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(20, 75)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(55, 13)
        Me.Label4.TabIndex = 55
        Me.Label4.Text = "Supplier :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LblMkey
        '
        Me.LblMkey.BackColor = System.Drawing.SystemColors.Control
        Me.LblMkey.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblMkey.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMkey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblMkey.Location = New System.Drawing.Point(558, 44)
        Me.LblMkey.Name = "LblMkey"
        Me.LblMkey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblMkey.Size = New System.Drawing.Size(31, 11)
        Me.LblMkey.TabIndex = 54
        Me.LblMkey.Text = "MKEY"
        Me.LblMkey.Visible = False
        '
        'FraPO
        '
        Me.FraPO.BackColor = System.Drawing.SystemColors.Control
        Me.FraPO.Controls.Add(Me.CboPONo)
        Me.FraPO.Enabled = False
        Me.FraPO.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraPO.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraPO.Location = New System.Drawing.Point(0, 43)
        Me.FraPO.Name = "FraPO"
        Me.FraPO.Padding = New System.Windows.Forms.Padding(0)
        Me.FraPO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraPO.Size = New System.Drawing.Size(178, 123)
        Me.FraPO.TabIndex = 52
        Me.FraPO.TabStop = False
        Me.FraPO.Text = "Po.No.(s) "
        '
        'CboPONo
        '
        Me.CboPONo.BackColor = System.Drawing.SystemColors.Window
        Me.CboPONo.Cursor = System.Windows.Forms.Cursors.Default
        Me.CboPONo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple
        Me.CboPONo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboPONo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.CboPONo.IntegralHeight = False
        Me.CboPONo.Location = New System.Drawing.Point(2, 14)
        Me.CboPONo.Name = "CboPONo"
        Me.CboPONo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CboPONo.Size = New System.Drawing.Size(172, 104)
        Me.CboPONo.TabIndex = 2
        '
        'Frasprd
        '
        Me.Frasprd.BackColor = System.Drawing.SystemColors.Control
        Me.Frasprd.Controls.Add(Me.FraDetail)
        Me.Frasprd.Controls.Add(Me.lblIGST)
        Me.Frasprd.Controls.Add(Me.Label32)
        Me.Frasprd.Controls.Add(Me.SSTab1)
        Me.Frasprd.Controls.Add(Me.SprdExp)
        Me.Frasprd.Controls.Add(Me.lblSaleReturn)
        Me.Frasprd.Controls.Add(Me.lblEDUAmount)
        Me.Frasprd.Controls.Add(Me.lblEDUPercent)
        Me.Frasprd.Controls.Add(Me.lblTotPackQtyCap)
        Me.Frasprd.Controls.Add(Me.lblTotQty)
        Me.Frasprd.Controls.Add(Me.lblNetAmount)
        Me.Frasprd.Controls.Add(Me.Label13)
        Me.Frasprd.Controls.Add(Me.lblTotItemValue)
        Me.Frasprd.Controls.Add(Me.Label16)
        Me.Frasprd.Controls.Add(Me.lblCGST)
        Me.Frasprd.Controls.Add(Me.Label17)
        Me.Frasprd.Controls.Add(Me.lblSGST)
        Me.Frasprd.Controls.Add(Me.Label34)
        Me.Frasprd.Controls.Add(Me.lblTotCharges)
        Me.Frasprd.Controls.Add(Me.lblTotFreight)
        Me.Frasprd.Controls.Add(Me.lblTotExpAmt)
        Me.Frasprd.Controls.Add(Me.lblEDPercentage)
        Me.Frasprd.Controls.Add(Me.lblSTPercentage)
        Me.Frasprd.Controls.Add(Me.lblTotTaxableAmt)
        Me.Frasprd.Controls.Add(Me.lblDiscount)
        Me.Frasprd.Controls.Add(Me.lblSurcharge)
        Me.Frasprd.Controls.Add(Me.lblRO)
        Me.Frasprd.Controls.Add(Me.lblMSC)
        Me.Frasprd.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frasprd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frasprd.Location = New System.Drawing.Point(-1, 160)
        Me.Frasprd.Name = "Frasprd"
        Me.Frasprd.Padding = New System.Windows.Forms.Padding(0)
        Me.Frasprd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frasprd.Size = New System.Drawing.Size(909, 382)
        Me.Frasprd.TabIndex = 47
        Me.Frasprd.TabStop = False
        '
        'FraDetail
        '
        Me.FraDetail.BackColor = System.Drawing.SystemColors.Control
        Me.FraDetail.Controls.Add(Me.chkGSTStatus)
        Me.FraDetail.Controls.Add(Me.chkServiceTaxClaim)
        Me.FraDetail.Controls.Add(Me.chkRejRtn)
        Me.FraDetail.Controls.Add(Me.txtSendDate)
        Me.FraDetail.Controls.Add(Me.chkBillPassing)
        Me.FraDetail.Controls.Add(Me.chkSTStatus)
        Me.FraDetail.Controls.Add(Me.chkExciseStatus)
        Me.FraDetail.Controls.Add(Me.chkDNote)
        Me.FraDetail.Controls.Add(Me.chkScheRej)
        Me.FraDetail.Controls.Add(Me.chkFOC)
        Me.FraDetail.Controls.Add(Me.chkPacking)
        Me.FraDetail.Controls.Add(Me.chkMrrSend)
        Me.FraDetail.Controls.Add(Me.lblModDate)
        Me.FraDetail.Controls.Add(Me.Label48)
        Me.FraDetail.Controls.Add(Me.lblAddDate)
        Me.FraDetail.Controls.Add(Me.Label45)
        Me.FraDetail.Controls.Add(Me.lblModUser)
        Me.FraDetail.Controls.Add(Me.Label46)
        Me.FraDetail.Controls.Add(Me.lblAddUser)
        Me.FraDetail.Controls.Add(Me.Label44)
        Me.FraDetail.Controls.Add(Me.Label9)
        Me.FraDetail.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraDetail.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraDetail.Location = New System.Drawing.Point(2, 270)
        Me.FraDetail.Name = "FraDetail"
        Me.FraDetail.Padding = New System.Windows.Forms.Padding(0)
        Me.FraDetail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraDetail.Size = New System.Drawing.Size(425, 139)
        Me.FraDetail.TabIndex = 84
        Me.FraDetail.TabStop = False
        Me.FraDetail.Text = "Other Detail"
        '
        'chkGSTStatus
        '
        Me.chkGSTStatus.BackColor = System.Drawing.SystemColors.Control
        Me.chkGSTStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkGSTStatus.Enabled = False
        Me.chkGSTStatus.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGSTStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkGSTStatus.Location = New System.Drawing.Point(12, 76)
        Me.chkGSTStatus.Name = "chkGSTStatus"
        Me.chkGSTStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkGSTStatus.Size = New System.Drawing.Size(91, 16)
        Me.chkGSTStatus.TabIndex = 129
        Me.chkGSTStatus.Text = "GST Status"
        Me.chkGSTStatus.UseVisualStyleBackColor = False
        '
        'chkServiceTaxClaim
        '
        Me.chkServiceTaxClaim.BackColor = System.Drawing.SystemColors.Control
        Me.chkServiceTaxClaim.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkServiceTaxClaim.Enabled = False
        Me.chkServiceTaxClaim.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkServiceTaxClaim.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkServiceTaxClaim.Location = New System.Drawing.Point(288, 58)
        Me.chkServiceTaxClaim.Name = "chkServiceTaxClaim"
        Me.chkServiceTaxClaim.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkServiceTaxClaim.Size = New System.Drawing.Size(127, 16)
        Me.chkServiceTaxClaim.TabIndex = 100
        Me.chkServiceTaxClaim.Text = "Service Tax Claim"
        Me.chkServiceTaxClaim.UseVisualStyleBackColor = False
        '
        'chkRejRtn
        '
        Me.chkRejRtn.BackColor = System.Drawing.SystemColors.Control
        Me.chkRejRtn.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkRejRtn.Enabled = False
        Me.chkRejRtn.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRejRtn.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRejRtn.Location = New System.Drawing.Point(144, 40)
        Me.chkRejRtn.Name = "chkRejRtn"
        Me.chkRejRtn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkRejRtn.Size = New System.Drawing.Size(133, 16)
        Me.chkRejRtn.TabIndex = 95
        Me.chkRejRtn.Text = "Rejection Returned"
        Me.chkRejRtn.UseVisualStyleBackColor = False
        '
        'txtSendDate
        '
        Me.txtSendDate.AcceptsReturn = True
        Me.txtSendDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtSendDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSendDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSendDate.Enabled = False
        Me.txtSendDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSendDate.ForeColor = System.Drawing.Color.Blue
        Me.txtSendDate.Location = New System.Drawing.Point(200, 18)
        Me.txtSendDate.MaxLength = 0
        Me.txtSendDate.Name = "txtSendDate"
        Me.txtSendDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSendDate.Size = New System.Drawing.Size(81, 22)
        Me.txtSendDate.TabIndex = 93
        '
        'chkBillPassing
        '
        Me.chkBillPassing.BackColor = System.Drawing.SystemColors.Control
        Me.chkBillPassing.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkBillPassing.Enabled = False
        Me.chkBillPassing.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBillPassing.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkBillPassing.Location = New System.Drawing.Point(288, 76)
        Me.chkBillPassing.Name = "chkBillPassing"
        Me.chkBillPassing.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkBillPassing.Size = New System.Drawing.Size(117, 16)
        Me.chkBillPassing.TabIndex = 92
        Me.chkBillPassing.Text = "Bill Passing"
        Me.chkBillPassing.UseVisualStyleBackColor = False
        '
        'chkSTStatus
        '
        Me.chkSTStatus.BackColor = System.Drawing.SystemColors.Control
        Me.chkSTStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkSTStatus.Enabled = False
        Me.chkSTStatus.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSTStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkSTStatus.Location = New System.Drawing.Point(144, 58)
        Me.chkSTStatus.Name = "chkSTStatus"
        Me.chkSTStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkSTStatus.Size = New System.Drawing.Size(119, 16)
        Me.chkSTStatus.TabIndex = 91
        Me.chkSTStatus.Text = "Sale Tax Status"
        Me.chkSTStatus.UseVisualStyleBackColor = False
        '
        'chkExciseStatus
        '
        Me.chkExciseStatus.BackColor = System.Drawing.SystemColors.Control
        Me.chkExciseStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkExciseStatus.Enabled = False
        Me.chkExciseStatus.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkExciseStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkExciseStatus.Location = New System.Drawing.Point(12, 58)
        Me.chkExciseStatus.Name = "chkExciseStatus"
        Me.chkExciseStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkExciseStatus.Size = New System.Drawing.Size(119, 16)
        Me.chkExciseStatus.TabIndex = 90
        Me.chkExciseStatus.Text = "Excise Status"
        Me.chkExciseStatus.UseVisualStyleBackColor = False
        '
        'chkDNote
        '
        Me.chkDNote.BackColor = System.Drawing.SystemColors.Control
        Me.chkDNote.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkDNote.Enabled = False
        Me.chkDNote.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDNote.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkDNote.Location = New System.Drawing.Point(144, 76)
        Me.chkDNote.Name = "chkDNote"
        Me.chkDNote.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkDNote.Size = New System.Drawing.Size(141, 16)
        Me.chkDNote.TabIndex = 89
        Me.chkDNote.Text = "Discepancy Note"
        Me.chkDNote.UseVisualStyleBackColor = False
        '
        'chkScheRej
        '
        Me.chkScheRej.BackColor = System.Drawing.SystemColors.Control
        Me.chkScheRej.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkScheRej.Enabled = False
        Me.chkScheRej.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScheRej.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkScheRej.Location = New System.Drawing.Point(288, 40)
        Me.chkScheRej.Name = "chkScheRej"
        Me.chkScheRej.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkScheRej.Size = New System.Drawing.Size(131, 16)
        Me.chkScheRej.TabIndex = 88
        Me.chkScheRej.Text = "Schedule Rejected"
        Me.chkScheRej.UseVisualStyleBackColor = False
        '
        'chkFOC
        '
        Me.chkFOC.BackColor = System.Drawing.SystemColors.Control
        Me.chkFOC.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkFOC.Enabled = False
        Me.chkFOC.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFOC.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkFOC.Location = New System.Drawing.Point(12, 96)
        Me.chkFOC.Name = "chkFOC"
        Me.chkFOC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkFOC.Size = New System.Drawing.Size(91, 16)
        Me.chkFOC.TabIndex = 87
        Me.chkFOC.Text = "F.O.C. MRR"
        Me.chkFOC.UseVisualStyleBackColor = False
        '
        'chkPacking
        '
        Me.chkPacking.BackColor = System.Drawing.SystemColors.Control
        Me.chkPacking.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPacking.Enabled = False
        Me.chkPacking.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPacking.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPacking.Location = New System.Drawing.Point(12, 40)
        Me.chkPacking.Name = "chkPacking"
        Me.chkPacking.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPacking.Size = New System.Drawing.Size(141, 16)
        Me.chkPacking.TabIndex = 86
        Me.chkPacking.Text = "Package Material"
        Me.chkPacking.UseVisualStyleBackColor = False
        '
        'chkMrrSend
        '
        Me.chkMrrSend.BackColor = System.Drawing.SystemColors.Control
        Me.chkMrrSend.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkMrrSend.Enabled = False
        Me.chkMrrSend.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMrrSend.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkMrrSend.Location = New System.Drawing.Point(12, 20)
        Me.chkMrrSend.Name = "chkMrrSend"
        Me.chkMrrSend.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkMrrSend.Size = New System.Drawing.Size(121, 16)
        Me.chkMrrSend.TabIndex = 85
        Me.chkMrrSend.Text = "Send To A/c"
        Me.chkMrrSend.UseVisualStyleBackColor = False
        '
        'lblModDate
        '
        Me.lblModDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblModDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblModDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblModDate.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblModDate.Location = New System.Drawing.Point(347, 116)
        Me.lblModDate.Name = "lblModDate"
        Me.lblModDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblModDate.Size = New System.Drawing.Size(69, 19)
        Me.lblModDate.TabIndex = 109
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.BackColor = System.Drawing.SystemColors.Control
        Me.Label48.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label48.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label48.Location = New System.Drawing.Point(288, 118)
        Me.Label48.Name = "Label48"
        Me.Label48.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label48.Size = New System.Drawing.Size(63, 15)
        Me.Label48.TabIndex = 108
        Me.Label48.Text = "Mod Date:"
        Me.Label48.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblAddDate
        '
        Me.lblAddDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblAddDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAddDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAddDate.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAddDate.Location = New System.Drawing.Point(347, 94)
        Me.lblAddDate.Name = "lblAddDate"
        Me.lblAddDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAddDate.Size = New System.Drawing.Size(69, 19)
        Me.lblAddDate.TabIndex = 107
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.BackColor = System.Drawing.SystemColors.Control
        Me.Label45.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label45.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label45.Location = New System.Drawing.Point(287, 96)
        Me.Label45.Name = "Label45"
        Me.Label45.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label45.Size = New System.Drawing.Size(63, 15)
        Me.Label45.TabIndex = 106
        Me.Label45.Text = "Add Date :"
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblModUser
        '
        Me.lblModUser.BackColor = System.Drawing.SystemColors.Control
        Me.lblModUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblModUser.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblModUser.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModUser.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblModUser.Location = New System.Drawing.Point(205, 116)
        Me.lblModUser.Name = "lblModUser"
        Me.lblModUser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblModUser.Size = New System.Drawing.Size(69, 19)
        Me.lblModUser.TabIndex = 105
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.BackColor = System.Drawing.SystemColors.Control
        Me.Label46.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label46.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label46.Location = New System.Drawing.Point(145, 118)
        Me.Label46.Name = "Label46"
        Me.Label46.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label46.Size = New System.Drawing.Size(61, 15)
        Me.Label46.TabIndex = 104
        Me.Label46.Text = "Mod User:"
        Me.Label46.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblAddUser
        '
        Me.lblAddUser.BackColor = System.Drawing.SystemColors.Control
        Me.lblAddUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAddUser.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAddUser.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddUser.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAddUser.Location = New System.Drawing.Point(205, 94)
        Me.lblAddUser.Name = "lblAddUser"
        Me.lblAddUser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAddUser.Size = New System.Drawing.Size(69, 19)
        Me.lblAddUser.TabIndex = 103
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.BackColor = System.Drawing.SystemColors.Control
        Me.Label44.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label44.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label44.Location = New System.Drawing.Point(146, 96)
        Me.Label44.Name = "Label44"
        Me.Label44.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label44.Size = New System.Drawing.Size(61, 15)
        Me.Label44.TabIndex = 102
        Me.Label44.Text = "Add User :"
        Me.Label44.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(146, 20)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(37, 13)
        Me.Label9.TabIndex = 94
        Me.Label9.Text = "Date :"
        '
        'lblIGST
        '
        Me.lblIGST.BackColor = System.Drawing.SystemColors.Control
        Me.lblIGST.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblIGST.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblIGST.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIGST.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblIGST.Location = New System.Drawing.Point(818, 336)
        Me.lblIGST.Name = "lblIGST"
        Me.lblIGST.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblIGST.Size = New System.Drawing.Size(85, 19)
        Me.lblIGST.TabIndex = 129
        Me.lblIGST.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.BackColor = System.Drawing.SystemColors.Control
        Me.Label32.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label32.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label32.Location = New System.Drawing.Point(777, 340)
        Me.Label32.Name = "Label32"
        Me.Label32.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label32.Size = New System.Drawing.Size(36, 13)
        Me.Label32.TabIndex = 128
        Me.Label32.Text = "IGST :"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'SSTab1
        '
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage0)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage1)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage2)
        Me.SSTab1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SSTab1.ItemSize = New System.Drawing.Size(42, 18)
        Me.SSTab1.Location = New System.Drawing.Point(4, 8)
        Me.SSTab1.Name = "SSTab1"
        Me.SSTab1.SelectedIndex = 0
        Me.SSTab1.Size = New System.Drawing.Size(902, 258)
        Me.SSTab1.TabIndex = 110
        '
        '_SSTab1_TabPage0
        '
        Me._SSTab1_TabPage0.Controls.Add(Me.SprdMain)
        Me._SSTab1_TabPage0.Location = New System.Drawing.Point(4, 22)
        Me._SSTab1_TabPage0.Name = "_SSTab1_TabPage0"
        Me._SSTab1_TabPage0.Size = New System.Drawing.Size(894, 232)
        Me._SSTab1_TabPage0.TabIndex = 0
        Me._SSTab1_TabPage0.Text = "Item Details"
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 4)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(890, 226)
        Me.SprdMain.TabIndex = 21
        '
        '_SSTab1_TabPage1
        '
        Me._SSTab1_TabPage1.Controls.Add(Me.fraFreight)
        Me._SSTab1_TabPage1.Location = New System.Drawing.Point(4, 22)
        Me._SSTab1_TabPage1.Name = "_SSTab1_TabPage1"
        Me._SSTab1_TabPage1.Size = New System.Drawing.Size(894, 232)
        Me._SSTab1_TabPage1.TabIndex = 1
        Me._SSTab1_TabPage1.Text = "Freight Details"
        '
        'fraFreight
        '
        Me.fraFreight.BackColor = System.Drawing.SystemColors.Control
        Me.fraFreight.Controls.Add(Me.txtDeliveryToLoc)
        Me.fraFreight.Controls.Add(Me.Label18)
        Me.fraFreight.Controls.Add(Me.txtDeliveryTo)
        Me.fraFreight.Controls.Add(Me.Label33)
        Me.fraFreight.Controls.Add(Me.Label30)
        Me.fraFreight.Controls.Add(Me.TxtShipTo)
        Me.fraFreight.Controls.Add(Me.txtST38No)
        Me.fraFreight.Controls.Add(Me.txtShippedTo)
        Me.fraFreight.Controls.Add(Me.cmdSearchShippedTo)
        Me.fraFreight.Controls.Add(Me.chkShipTo)
        Me.fraFreight.Controls.Add(Me.chkPrimiumFreight)
        Me.fraFreight.Controls.Add(Me.txtTripDate)
        Me.fraFreight.Controls.Add(Me.txtTripNo)
        Me.fraFreight.Controls.Add(Me.cboMode)
        Me.fraFreight.Controls.Add(Me.txtGRNo)
        Me.fraFreight.Controls.Add(Me.txtGRDate)
        Me.fraFreight.Controls.Add(Me.TxtTransporter)
        Me.fraFreight.Controls.Add(Me.txtDocsThru)
        Me.fraFreight.Controls.Add(Me._OptFreight_0)
        Me.fraFreight.Controls.Add(Me._OptFreight_1)
        Me.fraFreight.Controls.Add(Me.txtFreight)
        Me.fraFreight.Controls.Add(Me.txtFormDetail)
        Me.fraFreight.Controls.Add(Me.txtVehicle)
        Me.fraFreight.Controls.Add(Me.Label26)
        Me.fraFreight.Controls.Add(Me.Label24)
        Me.fraFreight.Controls.Add(Me.Label22)
        Me.fraFreight.Controls.Add(Me.Label21)
        Me.fraFreight.Controls.Add(Me.Label11)
        Me.fraFreight.Controls.Add(Me.Label10)
        Me.fraFreight.Controls.Add(Me.Label27)
        Me.fraFreight.Controls.Add(Me.Label28)
        Me.fraFreight.Controls.Add(Me.Label29)
        Me.fraFreight.Controls.Add(Me.Label31)
        Me.fraFreight.Controls.Add(Me.Label1)
        Me.fraFreight.Controls.Add(Me.Label8)
        Me.fraFreight.Controls.Add(Me.Label3)
        Me.fraFreight.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraFreight.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraFreight.Location = New System.Drawing.Point(1, -4)
        Me.fraFreight.Name = "fraFreight"
        Me.fraFreight.Padding = New System.Windows.Forms.Padding(0)
        Me.fraFreight.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraFreight.Size = New System.Drawing.Size(895, 234)
        Me.fraFreight.TabIndex = 111
        Me.fraFreight.TabStop = False
        '
        'txtDeliveryToLoc
        '
        Me.txtDeliveryToLoc.AcceptsReturn = True
        Me.txtDeliveryToLoc.BackColor = System.Drawing.SystemColors.Window
        Me.txtDeliveryToLoc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDeliveryToLoc.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDeliveryToLoc.Enabled = False
        Me.txtDeliveryToLoc.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeliveryToLoc.ForeColor = System.Drawing.Color.Blue
        Me.txtDeliveryToLoc.Location = New System.Drawing.Point(570, 204)
        Me.txtDeliveryToLoc.MaxLength = 0
        Me.txtDeliveryToLoc.Name = "txtDeliveryToLoc"
        Me.txtDeliveryToLoc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDeliveryToLoc.Size = New System.Drawing.Size(102, 22)
        Me.txtDeliveryToLoc.TabIndex = 147
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(450, 209)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(116, 13)
        Me.Label18.TabIndex = 148
        Me.Label18.Text = "Delivery To Location :"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtDeliveryTo
        '
        Me.txtDeliveryTo.AcceptsReturn = True
        Me.txtDeliveryTo.BackColor = System.Drawing.SystemColors.Window
        Me.txtDeliveryTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDeliveryTo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDeliveryTo.Enabled = False
        Me.txtDeliveryTo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeliveryTo.ForeColor = System.Drawing.Color.Blue
        Me.txtDeliveryTo.Location = New System.Drawing.Point(102, 204)
        Me.txtDeliveryTo.MaxLength = 0
        Me.txtDeliveryTo.Name = "txtDeliveryTo"
        Me.txtDeliveryTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDeliveryTo.Size = New System.Drawing.Size(267, 22)
        Me.txtDeliveryTo.TabIndex = 145
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.BackColor = System.Drawing.Color.Transparent
        Me.Label33.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label33.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.Color.Black
        Me.Label33.Location = New System.Drawing.Point(28, 209)
        Me.Label33.Name = "Label33"
        Me.Label33.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label33.Size = New System.Drawing.Size(70, 13)
        Me.Label33.TabIndex = 146
        Me.Label33.Text = "Delivery To :"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.BackColor = System.Drawing.Color.Transparent
        Me.Label30.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label30.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.ForeColor = System.Drawing.Color.Black
        Me.Label30.Location = New System.Drawing.Point(510, 183)
        Me.Label30.Name = "Label30"
        Me.Label30.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label30.Size = New System.Drawing.Size(56, 13)
        Me.Label30.TabIndex = 140
        Me.Label30.Text = "Location :"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TxtShipTo
        '
        Me.TxtShipTo.AcceptsReturn = True
        Me.TxtShipTo.BackColor = System.Drawing.SystemColors.Window
        Me.TxtShipTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtShipTo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtShipTo.Enabled = False
        Me.TxtShipTo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtShipTo.ForeColor = System.Drawing.Color.Blue
        Me.TxtShipTo.Location = New System.Drawing.Point(570, 177)
        Me.TxtShipTo.MaxLength = 0
        Me.TxtShipTo.Name = "TxtShipTo"
        Me.TxtShipTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtShipTo.Size = New System.Drawing.Size(102, 22)
        Me.TxtShipTo.TabIndex = 139
        '
        'txtST38No
        '
        Me.txtST38No.AcceptsReturn = True
        Me.txtST38No.BackColor = System.Drawing.SystemColors.Window
        Me.txtST38No.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtST38No.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtST38No.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtST38No.ForeColor = System.Drawing.Color.Blue
        Me.txtST38No.Location = New System.Drawing.Point(570, 13)
        Me.txtST38No.MaxLength = 0
        Me.txtST38No.Name = "txtST38No"
        Me.txtST38No.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtST38No.Size = New System.Drawing.Size(308, 22)
        Me.txtST38No.TabIndex = 29
        '
        'txtShippedTo
        '
        Me.txtShippedTo.AcceptsReturn = True
        Me.txtShippedTo.BackColor = System.Drawing.SystemColors.Window
        Me.txtShippedTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtShippedTo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtShippedTo.Enabled = False
        Me.txtShippedTo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShippedTo.ForeColor = System.Drawing.Color.Blue
        Me.txtShippedTo.Location = New System.Drawing.Point(102, 177)
        Me.txtShippedTo.MaxLength = 0
        Me.txtShippedTo.Name = "txtShippedTo"
        Me.txtShippedTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtShippedTo.Size = New System.Drawing.Size(267, 22)
        Me.txtShippedTo.TabIndex = 132
        '
        'chkShipTo
        '
        Me.chkShipTo.BackColor = System.Drawing.SystemColors.Control
        Me.chkShipTo.Checked = True
        Me.chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkShipTo.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkShipTo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShipTo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkShipTo.Location = New System.Drawing.Point(570, 154)
        Me.chkShipTo.Name = "chkShipTo"
        Me.chkShipTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkShipTo.Size = New System.Drawing.Size(293, 16)
        Me.chkShipTo.TabIndex = 130
        Me.chkShipTo.Text = "'Shipped From' Same as 'Billed From' (Yes / No)"
        Me.chkShipTo.UseVisualStyleBackColor = False
        '
        'chkPrimiumFreight
        '
        Me.chkPrimiumFreight.BackColor = System.Drawing.SystemColors.Control
        Me.chkPrimiumFreight.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPrimiumFreight.Enabled = False
        Me.chkPrimiumFreight.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPrimiumFreight.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPrimiumFreight.Location = New System.Drawing.Point(256, 18)
        Me.chkPrimiumFreight.Name = "chkPrimiumFreight"
        Me.chkPrimiumFreight.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPrimiumFreight.Size = New System.Drawing.Size(121, 18)
        Me.chkPrimiumFreight.TabIndex = 128
        Me.chkPrimiumFreight.Text = "Primium Freight"
        Me.chkPrimiumFreight.UseVisualStyleBackColor = False
        '
        'txtTripDate
        '
        Me.txtTripDate.AcceptsReturn = True
        Me.txtTripDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtTripDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTripDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTripDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTripDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTripDate.Location = New System.Drawing.Point(570, 127)
        Me.txtTripDate.MaxLength = 0
        Me.txtTripDate.Name = "txtTripDate"
        Me.txtTripDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTripDate.Size = New System.Drawing.Size(308, 22)
        Me.txtTripDate.TabIndex = 34
        '
        'txtTripNo
        '
        Me.txtTripNo.AcceptsReturn = True
        Me.txtTripNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtTripNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTripNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTripNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTripNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTripNo.Location = New System.Drawing.Point(570, 99)
        Me.txtTripNo.MaxLength = 0
        Me.txtTripNo.Name = "txtTripNo"
        Me.txtTripNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTripNo.Size = New System.Drawing.Size(308, 22)
        Me.txtTripNo.TabIndex = 33
        '
        'cboMode
        '
        Me.cboMode.BackColor = System.Drawing.SystemColors.Window
        Me.cboMode.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboMode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboMode.Location = New System.Drawing.Point(102, 43)
        Me.cboMode.Name = "cboMode"
        Me.cboMode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboMode.Size = New System.Drawing.Size(267, 21)
        Me.cboMode.TabIndex = 24
        '
        'txtGRNo
        '
        Me.txtGRNo.AcceptsReturn = True
        Me.txtGRNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtGRNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGRNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGRNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGRNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtGRNo.Location = New System.Drawing.Point(570, 43)
        Me.txtGRNo.MaxLength = 0
        Me.txtGRNo.Name = "txtGRNo"
        Me.txtGRNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGRNo.Size = New System.Drawing.Size(140, 22)
        Me.txtGRNo.TabIndex = 30
        '
        'txtGRDate
        '
        Me.txtGRDate.AcceptsReturn = True
        Me.txtGRDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtGRDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGRDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGRDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGRDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtGRDate.Location = New System.Drawing.Point(777, 43)
        Me.txtGRDate.MaxLength = 0
        Me.txtGRDate.Name = "txtGRDate"
        Me.txtGRDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGRDate.Size = New System.Drawing.Size(101, 22)
        Me.txtGRDate.TabIndex = 31
        '
        'TxtTransporter
        '
        Me.TxtTransporter.AcceptsReturn = True
        Me.TxtTransporter.BackColor = System.Drawing.SystemColors.Window
        Me.TxtTransporter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtTransporter.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtTransporter.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTransporter.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtTransporter.Location = New System.Drawing.Point(102, 96)
        Me.TxtTransporter.MaxLength = 0
        Me.TxtTransporter.Name = "TxtTransporter"
        Me.TxtTransporter.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtTransporter.Size = New System.Drawing.Size(267, 22)
        Me.TxtTransporter.TabIndex = 26
        '
        'txtDocsThru
        '
        Me.txtDocsThru.AcceptsReturn = True
        Me.txtDocsThru.BackColor = System.Drawing.SystemColors.Window
        Me.txtDocsThru.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDocsThru.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDocsThru.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDocsThru.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDocsThru.Location = New System.Drawing.Point(102, 123)
        Me.txtDocsThru.MaxLength = 0
        Me.txtDocsThru.Name = "txtDocsThru"
        Me.txtDocsThru.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDocsThru.Size = New System.Drawing.Size(267, 22)
        Me.txtDocsThru.TabIndex = 27
        '
        '_OptFreight_0
        '
        Me._OptFreight_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptFreight_0.Checked = True
        Me._OptFreight_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptFreight_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptFreight_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptFreight.SetIndex(Me._OptFreight_0, CType(0, Short))
        Me._OptFreight_0.Location = New System.Drawing.Point(102, 18)
        Me._OptFreight_0.Name = "_OptFreight_0"
        Me._OptFreight_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptFreight_0.Size = New System.Drawing.Size(65, 20)
        Me._OptFreight_0.TabIndex = 22
        Me._OptFreight_0.TabStop = True
        Me._OptFreight_0.Text = "To Pay"
        Me._OptFreight_0.UseVisualStyleBackColor = False
        '
        '_OptFreight_1
        '
        Me._OptFreight_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptFreight_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptFreight_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptFreight_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptFreight.SetIndex(Me._OptFreight_1, CType(1, Short))
        Me._OptFreight_1.Location = New System.Drawing.Point(181, 18)
        Me._OptFreight_1.Name = "_OptFreight_1"
        Me._OptFreight_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptFreight_1.Size = New System.Drawing.Size(49, 20)
        Me._OptFreight_1.TabIndex = 23
        Me._OptFreight_1.TabStop = True
        Me._OptFreight_1.Text = "Paid"
        Me._OptFreight_1.UseVisualStyleBackColor = False
        '
        'txtFreight
        '
        Me.txtFreight.AcceptsReturn = True
        Me.txtFreight.BackColor = System.Drawing.SystemColors.Window
        Me.txtFreight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFreight.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFreight.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFreight.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFreight.Location = New System.Drawing.Point(570, 71)
        Me.txtFreight.MaxLength = 0
        Me.txtFreight.Name = "txtFreight"
        Me.txtFreight.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFreight.Size = New System.Drawing.Size(308, 22)
        Me.txtFreight.TabIndex = 32
        '
        'txtFormDetail
        '
        Me.txtFormDetail.AcceptsReturn = True
        Me.txtFormDetail.BackColor = System.Drawing.SystemColors.Window
        Me.txtFormDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFormDetail.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFormDetail.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFormDetail.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFormDetail.Location = New System.Drawing.Point(102, 150)
        Me.txtFormDetail.MaxLength = 0
        Me.txtFormDetail.Name = "txtFormDetail"
        Me.txtFormDetail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFormDetail.Size = New System.Drawing.Size(267, 22)
        Me.txtFormDetail.TabIndex = 28
        '
        'txtVehicle
        '
        Me.txtVehicle.AcceptsReturn = True
        Me.txtVehicle.BackColor = System.Drawing.SystemColors.Window
        Me.txtVehicle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVehicle.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVehicle.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVehicle.ForeColor = System.Drawing.Color.Black
        Me.txtVehicle.Location = New System.Drawing.Point(102, 69)
        Me.txtVehicle.MaxLength = 0
        Me.txtVehicle.Name = "txtVehicle"
        Me.txtVehicle.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVehicle.Size = New System.Drawing.Size(267, 22)
        Me.txtVehicle.TabIndex = 25
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.SystemColors.Control
        Me.Label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label26.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(493, 18)
        Me.Label26.Name = "Label26"
        Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label26.Size = New System.Drawing.Size(73, 13)
        Me.Label26.TabIndex = 134
        Me.Label26.Text = "ST 38/16 No :"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label24.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.Black
        Me.Label24.Location = New System.Drawing.Point(36, 183)
        Me.Label24.Name = "Label24"
        Me.Label24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label24.Size = New System.Drawing.Size(65, 13)
        Me.Label24.TabIndex = 133
        Me.Label24.Text = "Ship From :"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.SystemColors.Control
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(507, 131)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(59, 13)
        Me.Label22.TabIndex = 126
        Me.Label22.Text = "Trip Date :"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.SystemColors.Control
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(516, 98)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(50, 13)
        Me.Label21.TabIndex = 125
        Me.Label21.Text = "Trip No :"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(520, 44)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(46, 13)
        Me.Label11.TabIndex = 121
        Me.Label11.Text = "GR No :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(718, 47)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(55, 13)
        Me.Label10.TabIndex = 120
        Me.Label10.Text = "GR Date :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.SystemColors.Control
        Me.Label27.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label27.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label27.Location = New System.Drawing.Point(58, 48)
        Me.Label27.Name = "Label27"
        Me.Label27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label27.Size = New System.Drawing.Size(43, 13)
        Me.Label27.TabIndex = 119
        Me.Label27.Text = "Mode :"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.SystemColors.Control
        Me.Label28.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label28.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label28.Location = New System.Drawing.Point(52, 21)
        Me.Label28.Name = "Label28"
        Me.Label28.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label28.Size = New System.Drawing.Size(49, 13)
        Me.Label28.TabIndex = 118
        Me.Label28.Text = "Freight :"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.SystemColors.Control
        Me.Label29.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label29.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label29.Location = New System.Drawing.Point(30, 101)
        Me.Label29.Name = "Label29"
        Me.Label29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label29.Size = New System.Drawing.Size(71, 13)
        Me.Label29.TabIndex = 117
        Me.Label29.Text = "Transporter :"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.BackColor = System.Drawing.SystemColors.Control
        Me.Label31.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label31.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label31.Location = New System.Drawing.Point(38, 128)
        Me.Label31.Name = "Label31"
        Me.Label31.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label31.Size = New System.Drawing.Size(63, 13)
        Me.Label31.TabIndex = 116
        Me.Label31.Text = "Docs Thru :"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(474, 72)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(92, 13)
        Me.Label1.TabIndex = 114
        Me.Label1.Text = "Freight Amount :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(23, 156)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(78, 13)
        Me.Label8.TabIndex = 113
        Me.Label8.Text = "Form Details :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(53, 73)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(48, 13)
        Me.Label3.TabIndex = 112
        Me.Label3.Text = "Vehicle :"
        '
        '_SSTab1_TabPage2
        '
        Me._SSTab1_TabPage2.Controls.Add(Me.Frame4)
        Me._SSTab1_TabPage2.Location = New System.Drawing.Point(4, 22)
        Me._SSTab1_TabPage2.Name = "_SSTab1_TabPage2"
        Me._SSTab1_TabPage2.Size = New System.Drawing.Size(894, 232)
        Me._SSTab1_TabPage2.TabIndex = 2
        Me._SSTab1_TabPage2.Text = "TC && Third Party Report"
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.cmdTPShow)
        Me.Frame4.Controls.Add(Me.cmdTCShow)
        Me.Frame4.Controls.Add(Me.chkTPRAvailable)
        Me.Frame4.Controls.Add(Me.chkTCAvailable)
        Me.Frame4.Controls.Add(Me.cmdTPRI)
        Me.Frame4.Controls.Add(Me.txtTPRPath)
        Me.Frame4.Controls.Add(Me.cmdTC)
        Me.Frame4.Controls.Add(Me.txtTCPath)
        Me.Frame4.Controls.Add(Me.Label36)
        Me.Frame4.Controls.Add(Me.Label35)
        Me.Frame4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, 0)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(890, 228)
        Me.Frame4.TabIndex = 135
        Me.Frame4.TabStop = False
        '
        'cmdTPShow
        '
        Me.cmdTPShow.BackColor = System.Drawing.SystemColors.Control
        Me.cmdTPShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdTPShow.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdTPShow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdTPShow.Location = New System.Drawing.Point(628, 100)
        Me.cmdTPShow.Name = "cmdTPShow"
        Me.cmdTPShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdTPShow.Size = New System.Drawing.Size(77, 24)
        Me.cmdTPShow.TabIndex = 143
        Me.cmdTPShow.Text = "TPR Show"
        Me.cmdTPShow.UseVisualStyleBackColor = False
        '
        'cmdTCShow
        '
        Me.cmdTCShow.BackColor = System.Drawing.SystemColors.Control
        Me.cmdTCShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdTCShow.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdTCShow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdTCShow.Location = New System.Drawing.Point(628, 45)
        Me.cmdTCShow.Name = "cmdTCShow"
        Me.cmdTCShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdTCShow.Size = New System.Drawing.Size(77, 23)
        Me.cmdTCShow.TabIndex = 142
        Me.cmdTCShow.Text = "TC Show"
        Me.cmdTCShow.UseVisualStyleBackColor = False
        '
        'chkTPRAvailable
        '
        Me.chkTPRAvailable.BackColor = System.Drawing.SystemColors.Control
        Me.chkTPRAvailable.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkTPRAvailable.Enabled = False
        Me.chkTPRAvailable.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTPRAvailable.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkTPRAvailable.Location = New System.Drawing.Point(164, 82)
        Me.chkTPRAvailable.Name = "chkTPRAvailable"
        Me.chkTPRAvailable.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkTPRAvailable.Size = New System.Drawing.Size(275, 18)
        Me.chkTPRAvailable.TabIndex = 141
        Me.chkTPRAvailable.Text = "Third Party Report Available (Yes / No)"
        Me.chkTPRAvailable.UseVisualStyleBackColor = False
        '
        'chkTCAvailable
        '
        Me.chkTCAvailable.BackColor = System.Drawing.SystemColors.Control
        Me.chkTCAvailable.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkTCAvailable.Enabled = False
        Me.chkTCAvailable.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTCAvailable.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkTCAvailable.Location = New System.Drawing.Point(164, 26)
        Me.chkTCAvailable.Name = "chkTCAvailable"
        Me.chkTCAvailable.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkTCAvailable.Size = New System.Drawing.Size(197, 18)
        Me.chkTCAvailable.TabIndex = 140
        Me.chkTCAvailable.Text = "TC Available (Yes / No)"
        Me.chkTCAvailable.UseVisualStyleBackColor = False
        '
        'txtTPRPath
        '
        Me.txtTPRPath.AcceptsReturn = True
        Me.txtTPRPath.BackColor = System.Drawing.SystemColors.Window
        Me.txtTPRPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTPRPath.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTPRPath.Enabled = False
        Me.txtTPRPath.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTPRPath.ForeColor = System.Drawing.Color.Blue
        Me.txtTPRPath.Location = New System.Drawing.Point(162, 102)
        Me.txtTPRPath.MaxLength = 0
        Me.txtTPRPath.Name = "txtTPRPath"
        Me.txtTPRPath.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTPRPath.Size = New System.Drawing.Size(431, 22)
        Me.txtTPRPath.TabIndex = 138
        '
        'txtTCPath
        '
        Me.txtTCPath.AcceptsReturn = True
        Me.txtTCPath.BackColor = System.Drawing.SystemColors.Window
        Me.txtTCPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTCPath.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTCPath.Enabled = False
        Me.txtTCPath.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTCPath.ForeColor = System.Drawing.Color.Blue
        Me.txtTCPath.Location = New System.Drawing.Point(162, 44)
        Me.txtTCPath.MaxLength = 0
        Me.txtTCPath.Name = "txtTCPath"
        Me.txtTCPath.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTCPath.Size = New System.Drawing.Size(431, 22)
        Me.txtTCPath.TabIndex = 136
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.BackColor = System.Drawing.Color.Transparent
        Me.Label36.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label36.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.ForeColor = System.Drawing.Color.Black
        Me.Label36.Location = New System.Drawing.Point(-1, 104)
        Me.Label36.Name = "Label36"
        Me.Label36.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label36.Size = New System.Drawing.Size(147, 13)
        Me.Label36.TabIndex = 145
        Me.Label36.Text = "Third Party Report Upload :"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.BackColor = System.Drawing.Color.Transparent
        Me.Label35.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label35.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.ForeColor = System.Drawing.Color.Black
        Me.Label35.Location = New System.Drawing.Point(87, 46)
        Me.Label35.Name = "Label35"
        Me.Label35.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label35.Size = New System.Drawing.Size(67, 13)
        Me.Label35.TabIndex = 144
        Me.Label35.Text = "TC Upload :"
        Me.Label35.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'SprdExp
        '
        Me.SprdExp.DataSource = Nothing
        Me.SprdExp.Location = New System.Drawing.Point(4, 268)
        Me.SprdExp.Name = "SprdExp"
        Me.SprdExp.OcxState = CType(resources.GetObject("SprdExp.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdExp.Size = New System.Drawing.Size(416, 110)
        Me.SprdExp.TabIndex = 35
        '
        'lblSaleReturn
        '
        Me.lblSaleReturn.BackColor = System.Drawing.SystemColors.Control
        Me.lblSaleReturn.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSaleReturn.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSaleReturn.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSaleReturn.Location = New System.Drawing.Point(552, 358)
        Me.lblSaleReturn.Name = "lblSaleReturn"
        Me.lblSaleReturn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSaleReturn.Size = New System.Drawing.Size(69, 17)
        Me.lblSaleReturn.TabIndex = 127
        Me.lblSaleReturn.Text = "lblSaleReturn"
        '
        'lblEDUAmount
        '
        Me.lblEDUAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblEDUAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblEDUAmount.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEDUAmount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblEDUAmount.Location = New System.Drawing.Point(598, 306)
        Me.lblEDUAmount.Name = "lblEDUAmount"
        Me.lblEDUAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblEDUAmount.Size = New System.Drawing.Size(71, 13)
        Me.lblEDUAmount.TabIndex = 99
        Me.lblEDUAmount.Text = "lblEDUAmount"
        Me.lblEDUAmount.Visible = False
        '
        'lblEDUPercent
        '
        Me.lblEDUPercent.BackColor = System.Drawing.SystemColors.Control
        Me.lblEDUPercent.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblEDUPercent.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEDUPercent.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblEDUPercent.Location = New System.Drawing.Point(598, 332)
        Me.lblEDUPercent.Name = "lblEDUPercent"
        Me.lblEDUPercent.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblEDUPercent.Size = New System.Drawing.Size(71, 13)
        Me.lblEDUPercent.TabIndex = 98
        Me.lblEDUPercent.Text = "lblEDUPercent"
        Me.lblEDUPercent.Visible = False
        '
        'lblTotPackQtyCap
        '
        Me.lblTotPackQtyCap.AutoSize = True
        Me.lblTotPackQtyCap.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotPackQtyCap.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotPackQtyCap.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotPackQtyCap.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotPackQtyCap.Location = New System.Drawing.Point(520, 279)
        Me.lblTotPackQtyCap.Name = "lblTotPackQtyCap"
        Me.lblTotPackQtyCap.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotPackQtyCap.Size = New System.Drawing.Size(59, 13)
        Me.lblTotPackQtyCap.TabIndex = 82
        Me.lblTotPackQtyCap.Text = "Total Qty :"
        Me.lblTotPackQtyCap.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotQty
        '
        Me.lblTotQty.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotQty.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotQty.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotQty.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotQty.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotQty.Location = New System.Drawing.Point(618, 278)
        Me.lblTotQty.Name = "lblTotQty"
        Me.lblTotQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotQty.Size = New System.Drawing.Size(99, 17)
        Me.lblTotQty.TabIndex = 81
        Me.lblTotQty.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblNetAmount
        '
        Me.lblNetAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblNetAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNetAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblNetAmount.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblNetAmount.Location = New System.Drawing.Point(818, 357)
        Me.lblNetAmount.Name = "lblNetAmount"
        Me.lblNetAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblNetAmount.Size = New System.Drawing.Size(85, 19)
        Me.lblNetAmount.TabIndex = 80
        Me.lblNetAmount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label13.Location = New System.Drawing.Point(739, 360)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(74, 13)
        Me.Label13.TabIndex = 79
        Me.Label13.Text = "Net Amount :"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotItemValue
        '
        Me.lblTotItemValue.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotItemValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotItemValue.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotItemValue.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotItemValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotItemValue.Location = New System.Drawing.Point(818, 276)
        Me.lblTotItemValue.Name = "lblTotItemValue"
        Me.lblTotItemValue.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotItemValue.Size = New System.Drawing.Size(85, 17)
        Me.lblTotItemValue.TabIndex = 78
        Me.lblTotItemValue.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label16.Location = New System.Drawing.Point(747, 277)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(66, 13)
        Me.Label16.TabIndex = 77
        Me.Label16.Text = "Item Value :"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCGST
        '
        Me.lblCGST.BackColor = System.Drawing.SystemColors.Control
        Me.lblCGST.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCGST.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCGST.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCGST.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCGST.Location = New System.Drawing.Point(818, 295)
        Me.lblCGST.Name = "lblCGST"
        Me.lblCGST.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCGST.Size = New System.Drawing.Size(85, 19)
        Me.lblCGST.TabIndex = 76
        Me.lblCGST.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label17.Location = New System.Drawing.Point(773, 299)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(40, 13)
        Me.Label17.TabIndex = 75
        Me.Label17.Text = "CGST :"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblSGST
        '
        Me.lblSGST.BackColor = System.Drawing.SystemColors.Control
        Me.lblSGST.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSGST.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSGST.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSGST.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSGST.Location = New System.Drawing.Point(818, 315)
        Me.lblSGST.Name = "lblSGST"
        Me.lblSGST.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSGST.Size = New System.Drawing.Size(85, 19)
        Me.lblSGST.TabIndex = 74
        Me.lblSGST.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.BackColor = System.Drawing.SystemColors.Control
        Me.Label34.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label34.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label34.Location = New System.Drawing.Point(774, 319)
        Me.Label34.Name = "Label34"
        Me.Label34.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label34.Size = New System.Drawing.Size(39, 13)
        Me.Label34.TabIndex = 73
        Me.Label34.Text = "SGST :"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotCharges
        '
        Me.lblTotCharges.AutoSize = True
        Me.lblTotCharges.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotCharges.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotCharges.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotCharges.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTotCharges.Location = New System.Drawing.Point(532, 294)
        Me.lblTotCharges.Name = "lblTotCharges"
        Me.lblTotCharges.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotCharges.Size = New System.Drawing.Size(13, 13)
        Me.lblTotCharges.TabIndex = 72
        Me.lblTotCharges.Text = "0"
        Me.lblTotCharges.Visible = False
        '
        'lblTotFreight
        '
        Me.lblTotFreight.AutoSize = True
        Me.lblTotFreight.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotFreight.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotFreight.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotFreight.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTotFreight.Location = New System.Drawing.Point(574, 294)
        Me.lblTotFreight.Name = "lblTotFreight"
        Me.lblTotFreight.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotFreight.Size = New System.Drawing.Size(13, 13)
        Me.lblTotFreight.TabIndex = 71
        Me.lblTotFreight.Text = "0"
        Me.lblTotFreight.Visible = False
        '
        'lblTotExpAmt
        '
        Me.lblTotExpAmt.AutoSize = True
        Me.lblTotExpAmt.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotExpAmt.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotExpAmt.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotExpAmt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTotExpAmt.Location = New System.Drawing.Point(530, 312)
        Me.lblTotExpAmt.Name = "lblTotExpAmt"
        Me.lblTotExpAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotExpAmt.Size = New System.Drawing.Size(13, 13)
        Me.lblTotExpAmt.TabIndex = 70
        Me.lblTotExpAmt.Text = "0"
        Me.lblTotExpAmt.Visible = False
        '
        'lblEDPercentage
        '
        Me.lblEDPercentage.AutoSize = True
        Me.lblEDPercentage.BackColor = System.Drawing.SystemColors.Control
        Me.lblEDPercentage.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblEDPercentage.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEDPercentage.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblEDPercentage.Location = New System.Drawing.Point(574, 314)
        Me.lblEDPercentage.Name = "lblEDPercentage"
        Me.lblEDPercentage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblEDPercentage.Size = New System.Drawing.Size(13, 13)
        Me.lblEDPercentage.TabIndex = 69
        Me.lblEDPercentage.Text = "0"
        Me.lblEDPercentage.Visible = False
        '
        'lblSTPercentage
        '
        Me.lblSTPercentage.AutoSize = True
        Me.lblSTPercentage.BackColor = System.Drawing.SystemColors.Control
        Me.lblSTPercentage.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSTPercentage.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSTPercentage.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSTPercentage.Location = New System.Drawing.Point(534, 330)
        Me.lblSTPercentage.Name = "lblSTPercentage"
        Me.lblSTPercentage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSTPercentage.Size = New System.Drawing.Size(13, 13)
        Me.lblSTPercentage.TabIndex = 68
        Me.lblSTPercentage.Text = "0"
        Me.lblSTPercentage.Visible = False
        '
        'lblTotTaxableAmt
        '
        Me.lblTotTaxableAmt.AutoSize = True
        Me.lblTotTaxableAmt.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotTaxableAmt.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotTaxableAmt.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotTaxableAmt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTotTaxableAmt.Location = New System.Drawing.Point(574, 334)
        Me.lblTotTaxableAmt.Name = "lblTotTaxableAmt"
        Me.lblTotTaxableAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotTaxableAmt.Size = New System.Drawing.Size(13, 13)
        Me.lblTotTaxableAmt.TabIndex = 67
        Me.lblTotTaxableAmt.Text = "0"
        Me.lblTotTaxableAmt.Visible = False
        '
        'lblDiscount
        '
        Me.lblDiscount.BackColor = System.Drawing.SystemColors.Control
        Me.lblDiscount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDiscount.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDiscount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDiscount.Location = New System.Drawing.Point(468, 292)
        Me.lblDiscount.Name = "lblDiscount"
        Me.lblDiscount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDiscount.Size = New System.Drawing.Size(59, 11)
        Me.lblDiscount.TabIndex = 66
        Me.lblDiscount.Text = "lblDiscount"
        Me.lblDiscount.Visible = False
        '
        'lblSurcharge
        '
        Me.lblSurcharge.BackColor = System.Drawing.SystemColors.Control
        Me.lblSurcharge.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSurcharge.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSurcharge.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSurcharge.Location = New System.Drawing.Point(468, 308)
        Me.lblSurcharge.Name = "lblSurcharge"
        Me.lblSurcharge.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSurcharge.Size = New System.Drawing.Size(47, 11)
        Me.lblSurcharge.TabIndex = 65
        Me.lblSurcharge.Text = "lblSurcharge"
        Me.lblSurcharge.Visible = False
        '
        'lblRO
        '
        Me.lblRO.BackColor = System.Drawing.SystemColors.Control
        Me.lblRO.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblRO.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRO.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRO.Location = New System.Drawing.Point(468, 324)
        Me.lblRO.Name = "lblRO"
        Me.lblRO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblRO.Size = New System.Drawing.Size(41, 11)
        Me.lblRO.TabIndex = 64
        Me.lblRO.Text = "lblRO"
        Me.lblRO.Visible = False
        '
        'lblMSC
        '
        Me.lblMSC.BackColor = System.Drawing.SystemColors.Control
        Me.lblMSC.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMSC.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMSC.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMSC.Location = New System.Drawing.Point(468, 336)
        Me.lblMSC.Name = "lblMSC"
        Me.lblMSC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMSC.Size = New System.Drawing.Size(49, 11)
        Me.lblMSC.TabIndex = 63
        Me.lblMSC.Text = "lblMSC"
        Me.lblMSC.Visible = False
        '
        'txtScanning
        '
        Me.txtScanning.AcceptsReturn = True
        Me.txtScanning.BackColor = System.Drawing.SystemColors.Window
        Me.txtScanning.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtScanning.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtScanning.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtScanning.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtScanning.Location = New System.Drawing.Point(120, 548)
        Me.txtScanning.MaxLength = 0
        Me.txtScanning.Multiline = True
        Me.txtScanning.Name = "txtScanning"
        Me.txtScanning.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtScanning.Size = New System.Drawing.Size(591, 19)
        Me.txtScanning.TabIndex = 36
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(6, 550)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(105, 13)
        Me.Label2.TabIndex = 83
        Me.Label2.Text = "BarCode Scanning :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(0, 0)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 51
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.cmdDispcrepancy)
        Me.Frame3.Controls.Add(Me.cmdClose)
        Me.Frame3.Controls.Add(Me.CmdView)
        Me.Frame3.Controls.Add(Me.CmdPreview)
        Me.Frame3.Controls.Add(Me.cmdPrint)
        Me.Frame3.Controls.Add(Me.cmdDetail)
        Me.Frame3.Controls.Add(Me.cmdDelete)
        Me.Frame3.Controls.Add(Me.cmdSave)
        Me.Frame3.Controls.Add(Me.cmdModify)
        Me.Frame3.Controls.Add(Me.cmdAdd)
        Me.Frame3.Controls.Add(Me.lblBookType)
        Me.Frame3.Controls.Add(Me.cmdSavePrint)
        Me.Frame3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 562)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(908, 58)
        Me.Frame3.TabIndex = 48
        Me.Frame3.TabStop = False
        '
        'lblBookType
        '
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(4, 18)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(33, 17)
        Me.lblBookType.TabIndex = 96
        Me.lblBookType.Text = "lblBookType"
        '
        'OptFreight
        '
        '
        'UltraGrid1
        '
        Appearance1.BackColor = System.Drawing.SystemColors.Window
        Appearance1.BorderColor = System.Drawing.Color.White
        Me.UltraGrid1.DisplayLayout.Appearance = Appearance1
        Me.UltraGrid1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.UltraGrid1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
        Appearance2.BackColor = System.Drawing.Color.White
        Appearance2.BackColor2 = System.Drawing.Color.White
        Appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance2.BorderColor = System.Drawing.SystemColors.Window
        Me.UltraGrid1.DisplayLayout.GroupByBox.Appearance = Appearance2
        Appearance3.ForeColor = System.Drawing.SystemColors.GrayText
        Me.UltraGrid1.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance3
        Me.UltraGrid1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.UltraGrid1.DisplayLayout.GroupByBox.Hidden = True
        Appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance4.BackColor2 = System.Drawing.SystemColors.Control
        Appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance4.ForeColor = System.Drawing.SystemColors.GrayText
        Me.UltraGrid1.DisplayLayout.GroupByBox.PromptAppearance = Appearance4
        Me.UltraGrid1.DisplayLayout.MaxColScrollRegions = 1
        Me.UltraGrid1.DisplayLayout.MaxRowScrollRegions = 1
        Appearance5.BackColor = System.Drawing.SystemColors.Window
        Appearance5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.UltraGrid1.DisplayLayout.Override.ActiveCellAppearance = Appearance5
        Appearance6.BackColor = System.Drawing.SystemColors.Highlight
        Appearance6.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.UltraGrid1.DisplayLayout.Override.ActiveRowAppearance = Appearance6
        Me.UltraGrid1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
        Me.UltraGrid1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
        Appearance7.BackColor = System.Drawing.SystemColors.Window
        Me.UltraGrid1.DisplayLayout.Override.CardAreaAppearance = Appearance7
        Appearance8.BorderColor = System.Drawing.Color.Silver
        Appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
        Me.UltraGrid1.DisplayLayout.Override.CellAppearance = Appearance8
        Me.UltraGrid1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
        Me.UltraGrid1.DisplayLayout.Override.CellPadding = 0
        Appearance9.BackColor = System.Drawing.SystemColors.Control
        Appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
        Appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance9.BorderColor = System.Drawing.SystemColors.Window
        Me.UltraGrid1.DisplayLayout.Override.GroupByRowAppearance = Appearance9
        Appearance10.TextHAlignAsString = "Left"
        Me.UltraGrid1.DisplayLayout.Override.HeaderAppearance = Appearance10
        Me.UltraGrid1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Me.UltraGrid1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
        Appearance11.BackColor = System.Drawing.SystemColors.Window
        Appearance11.BorderColor = System.Drawing.Color.Silver
        Me.UltraGrid1.DisplayLayout.Override.RowAppearance = Appearance11
        Me.UltraGrid1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
        Appearance12.BackColor = System.Drawing.SystemColors.ControlLight
        Me.UltraGrid1.DisplayLayout.Override.TemplateAddRowAppearance = Appearance12
        Me.UltraGrid1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.UltraGrid1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.UltraGrid1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraGrid1.Location = New System.Drawing.Point(1, 1)
        Me.UltraGrid1.Name = "UltraGrid1"
        Me.UltraGrid1.Size = New System.Drawing.Size(908, 564)
        Me.UltraGrid1.TabIndex = 78
        '
        'FrmMRR
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(910, 621)
        Me.Controls.Add(Me.FraFront)
        Me.Controls.Add(Me.Report1)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.UltraGrid1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmMRR"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "MRR - Gate Entry"
        Me.FraFront.ResumeLayout(False)
        Me.FraFront.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frasupp.ResumeLayout(False)
        Me.Frasupp.PerformLayout()
        Me.FraPO.ResumeLayout(False)
        Me.Frasprd.ResumeLayout(False)
        Me.Frasprd.PerformLayout()
        Me.FraDetail.ResumeLayout(False)
        Me.FraDetail.PerformLayout()
        Me.SSTab1.ResumeLayout(False)
        Me._SSTab1_TabPage0.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage1.ResumeLayout(False)
        Me.fraFreight.ResumeLayout(False)
        Me.fraFreight.PerformLayout()
        Me._SSTab1_TabPage2.ResumeLayout(False)
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        CType(Me.SprdExp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame3.ResumeLayout(False)
        CType(Me.OptFreight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        'SprdView.DataSource = CType(AdataItem, msdatasrc.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        'SprdView.DataSource = Nothing
    End Sub
    Public WithEvents lblIGST As System.Windows.Forms.Label
    Public WithEvents Label32 As System.Windows.Forms.Label
    Public WithEvents txtBillTo As TextBox
    Public WithEvents Label30 As Label
    Public WithEvents TxtShipTo As TextBox
    Friend WithEvents UltraGrid1 As Infragistics.Win.UltraWinGrid.UltraGrid
    Public WithEvents txtDeliveryToLoc As TextBox
    Public WithEvents Label18 As Label
    Public WithEvents txtDeliveryTo As TextBox
    Public WithEvents Label33 As Label
#End Region
End Class