Imports Microsoft.VisualBasic.Compatibility

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmGateEntry
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
    Public WithEvents lblModDate As System.Windows.Forms.Label
    Public WithEvents Label48 As System.Windows.Forms.Label
    Public WithEvents lblAddDate As System.Windows.Forms.Label
    Public WithEvents Label45 As System.Windows.Forms.Label
    Public WithEvents lblModUser As System.Windows.Forms.Label
    Public WithEvents Label46 As System.Windows.Forms.Label
    Public WithEvents lblAddUser As System.Windows.Forms.Label
    Public WithEvents Label44 As System.Windows.Forms.Label
    Public WithEvents FraDetail As System.Windows.Forms.GroupBox
    Public WithEvents txtScanning As System.Windows.Forms.TextBox
    Public WithEvents cboRefType As System.Windows.Forms.ComboBox
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents chkUnderChallan As System.Windows.Forms.CheckBox
    Public WithEvents txtEwayBillNo As System.Windows.Forms.TextBox
    Public WithEvents cboDivision As System.Windows.Forms.ComboBox
    Public WithEvents TxtRemarks As System.Windows.Forms.TextBox
    Public WithEvents cmdsearch As System.Windows.Forms.Button
    Public WithEvents chkMRRMade As System.Windows.Forms.CheckBox
    Public WithEvents TxtItemDesc As System.Windows.Forms.TextBox
    Public WithEvents TxtSupplier As System.Windows.Forms.TextBox
    Public WithEvents txtMRRNo As System.Windows.Forms.TextBox
    Public WithEvents txtMRRDate As System.Windows.Forms.TextBox
    Public WithEvents txtBillNo As System.Windows.Forms.TextBox
    Public WithEvents txtBillDate As System.Windows.Forms.TextBox
    Public WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label25 As System.Windows.Forms.Label
    Public WithEvents lblEntryDate As System.Windows.Forms.Label
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
    Public WithEvents chkShipTo As System.Windows.Forms.CheckBox
    Public WithEvents cmdSearchShippedTo As System.Windows.Forms.Button
    Public WithEvents txtShippedTo As System.Windows.Forms.TextBox
    Public WithEvents cboMode As System.Windows.Forms.ComboBox
    Public WithEvents txtGRNo As System.Windows.Forms.TextBox
    Public WithEvents txtGRDate As System.Windows.Forms.TextBox
    Public WithEvents txtVehicle As System.Windows.Forms.TextBox
    Public WithEvents txtDocsThru As System.Windows.Forms.TextBox
    Public WithEvents _OptFreight_0 As System.Windows.Forms.RadioButton
    Public WithEvents _OptFreight_1 As System.Windows.Forms.RadioButton
    Public WithEvents txtFreight As System.Windows.Forms.TextBox
    Public WithEvents txtFormDetail As System.Windows.Forms.TextBox
    Public WithEvents TxtTransporter As System.Windows.Forms.TextBox
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
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
    Public WithEvents txtTCPath As System.Windows.Forms.TextBox
    Public WithEvents cmdTC As System.Windows.Forms.Button
    Public WithEvents txtTPRPath As System.Windows.Forms.TextBox
    Public WithEvents cmdTPRI As System.Windows.Forms.Button
    Public WithEvents chkTCAvailable As System.Windows.Forms.CheckBox
    Public WithEvents chkTPRAvailable As System.Windows.Forms.CheckBox
    Public WithEvents cmdTCShow As System.Windows.Forms.Button
    Public WithEvents cmdTPShow As System.Windows.Forms.Button
    Public cdgFilePathOpen As System.Windows.Forms.OpenFileDialog
    Public cdgFilePathSave As System.Windows.Forms.SaveFileDialog
    Public cdgFilePathFont As System.Windows.Forms.FontDialog
    Public cdgFilePathColor As System.Windows.Forms.ColorDialog
    Public cdgFilePathPrint As System.Windows.Forms.PrintDialog
    Public WithEvents Label35 As System.Windows.Forms.Label
    Public WithEvents Label36 As System.Windows.Forms.Label
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents _SSTab1_TabPage2 As System.Windows.Forms.TabPage
    Public WithEvents SSTab1 As System.Windows.Forms.TabControl
    Public WithEvents SprdExp As AxFPSpreadADO.AxfpSpread
    Public WithEvents lblEDUAmount As System.Windows.Forms.Label
    Public WithEvents lblEDUPercent As System.Windows.Forms.Label
    Public WithEvents lblTotPackQtyCap As System.Windows.Forms.Label
    Public WithEvents lblTotQty As System.Windows.Forms.Label
    Public WithEvents lblNetAmount As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents lblTotItemValue As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents lblTotCGST As System.Windows.Forms.Label
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents lblTotSGST As System.Windows.Forms.Label
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
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents OptFreight As VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmGateEntry))
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
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.cmdSearchShippedTo = New System.Windows.Forms.Button()
        Me.cmdTC = New System.Windows.Forms.Button()
        Me.cmdTPRI = New System.Windows.Forms.Button()
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
        Me.cmdBillToSearch = New System.Windows.Forms.Button()
        Me.cmdShipToSearch = New System.Windows.Forms.Button()
        Me.cmdInterUnitBill = New System.Windows.Forms.Button()
        Me.cmdDeliveryToLocSearch = New System.Windows.Forms.Button()
        Me.cmdSearchDeliveryTo = New System.Windows.Forms.Button()
        Me.FraFront = New System.Windows.Forms.GroupBox()
        Me.txtScanning = New System.Windows.Forms.TextBox()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.cboRefType = New System.Windows.Forms.ComboBox()
        Me.Frasupp = New System.Windows.Forms.GroupBox()
        Me.txtOldERPNo = New System.Windows.Forms.TextBox()
        Me.txtOldERPDate = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtBillTo = New System.Windows.Forms.TextBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.chkUnderChallan = New System.Windows.Forms.CheckBox()
        Me.txtEwayBillNo = New System.Windows.Forms.TextBox()
        Me.cboDivision = New System.Windows.Forms.ComboBox()
        Me.TxtRemarks = New System.Windows.Forms.TextBox()
        Me.chkMRRMade = New System.Windows.Forms.CheckBox()
        Me.TxtItemDesc = New System.Windows.Forms.TextBox()
        Me.TxtSupplier = New System.Windows.Forms.TextBox()
        Me.txtMRRNo = New System.Windows.Forms.TextBox()
        Me.txtMRRDate = New System.Windows.Forms.TextBox()
        Me.txtBillNo = New System.Windows.Forms.TextBox()
        Me.txtBillDate = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.lblEntryDate = New System.Windows.Forms.Label()
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
        Me.lblTotIGST = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.SSTab1 = New System.Windows.Forms.TabControl()
        Me._SSTab1_TabPage0 = New System.Windows.Forms.TabPage()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me._SSTab1_TabPage1 = New System.Windows.Forms.TabPage()
        Me.fraFreight = New System.Windows.Forms.GroupBox()
        Me.txtDeliveryToLoc = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtDeliveryTo = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.TxtShipTo = New System.Windows.Forms.TextBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.txtST38No = New System.Windows.Forms.TextBox()
        Me.chkShipTo = New System.Windows.Forms.CheckBox()
        Me.txtShippedTo = New System.Windows.Forms.TextBox()
        Me.cboMode = New System.Windows.Forms.ComboBox()
        Me.txtGRNo = New System.Windows.Forms.TextBox()
        Me.txtGRDate = New System.Windows.Forms.TextBox()
        Me.txtVehicle = New System.Windows.Forms.TextBox()
        Me.txtDocsThru = New System.Windows.Forms.TextBox()
        Me._OptFreight_0 = New System.Windows.Forms.RadioButton()
        Me._OptFreight_1 = New System.Windows.Forms.RadioButton()
        Me.txtFreight = New System.Windows.Forms.TextBox()
        Me.txtFormDetail = New System.Windows.Forms.TextBox()
        Me.TxtTransporter = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
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
        Me.txtTCPath = New System.Windows.Forms.TextBox()
        Me.txtTPRPath = New System.Windows.Forms.TextBox()
        Me.chkTCAvailable = New System.Windows.Forms.CheckBox()
        Me.chkTPRAvailable = New System.Windows.Forms.CheckBox()
        Me.cmdTCShow = New System.Windows.Forms.Button()
        Me.cmdTPShow = New System.Windows.Forms.Button()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.SprdExp = New AxFPSpreadADO.AxfpSpread()
        Me.lblEDUAmount = New System.Windows.Forms.Label()
        Me.lblEDUPercent = New System.Windows.Forms.Label()
        Me.lblTotPackQtyCap = New System.Windows.Forms.Label()
        Me.lblTotQty = New System.Windows.Forms.Label()
        Me.lblNetAmount = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblTotItemValue = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblTotCGST = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.lblTotSGST = New System.Windows.Forms.Label()
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
        Me.Label2 = New System.Windows.Forms.Label()
        Me.FraDetail = New System.Windows.Forms.GroupBox()
        Me.lblModDate = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.lblAddDate = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.lblModUser = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.lblAddUser = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.cdgFilePathOpen = New System.Windows.Forms.OpenFileDialog()
        Me.cdgFilePathSave = New System.Windows.Forms.SaveFileDialog()
        Me.cdgFilePathFont = New System.Windows.Forms.FontDialog()
        Me.cdgFilePathColor = New System.Windows.Forms.ColorDialog()
        Me.cdgFilePathPrint = New System.Windows.Forms.PrintDialog()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.OptFreight = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.UltraGrid1 = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.FraFront.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.Frasupp.SuspendLayout()
        Me.FraPO.SuspendLayout()
        Me.Frasprd.SuspendLayout()
        Me.SSTab1.SuspendLayout()
        Me._SSTab1_TabPage0.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage1.SuspendLayout()
        Me.fraFreight.SuspendLayout()
        Me._SSTab1_TabPage2.SuspendLayout()
        Me.Frame4.SuspendLayout()
        CType(Me.SprdExp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraDetail.SuspendLayout()
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
        Me.cmdShow.Location = New System.Drawing.Point(710, 548)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShow.Size = New System.Drawing.Size(37, 19)
        Me.cmdShow.TabIndex = 77
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'cmdsearch
        '
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.Location = New System.Drawing.Point(463, 60)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(28, 23)
        Me.cmdsearch.TabIndex = 8
        Me.cmdsearch.TabStop = False
        Me.cmdsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearch, "Search")
        Me.cmdsearch.UseVisualStyleBackColor = False
        '
        'cmdSearchShippedTo
        '
        Me.cmdSearchShippedTo.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchShippedTo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchShippedTo.Enabled = False
        Me.cmdSearchShippedTo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchShippedTo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchShippedTo.Image = CType(resources.GetObject("cmdSearchShippedTo.Image"), System.Drawing.Image)
        Me.cmdSearchShippedTo.Location = New System.Drawing.Point(360, 180)
        Me.cmdSearchShippedTo.Name = "cmdSearchShippedTo"
        Me.cmdSearchShippedTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchShippedTo.Size = New System.Drawing.Size(28, 23)
        Me.cmdSearchShippedTo.TabIndex = 102
        Me.cmdSearchShippedTo.TabStop = False
        Me.cmdSearchShippedTo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchShippedTo, "Search")
        Me.cmdSearchShippedTo.UseVisualStyleBackColor = False
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
        Me.cmdTC.TabIndex = 113
        Me.cmdTC.TabStop = False
        Me.cmdTC.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdTC, "Search")
        Me.cmdTC.UseVisualStyleBackColor = False
        '
        'cmdTPRI
        '
        Me.cmdTPRI.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdTPRI.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdTPRI.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdTPRI.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdTPRI.Image = CType(resources.GetObject("cmdTPRI.Image"), System.Drawing.Image)
        Me.cmdTPRI.Location = New System.Drawing.Point(594, 102)
        Me.cmdTPRI.Name = "cmdTPRI"
        Me.cmdTPRI.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdTPRI.Size = New System.Drawing.Size(28, 23)
        Me.cmdTPRI.TabIndex = 111
        Me.cmdTPRI.TabStop = False
        Me.cmdTPRI.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdTPRI, "Search")
        Me.cmdTPRI.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.Location = New System.Drawing.Point(704, 13)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(67, 37)
        Me.cmdClose.TabIndex = 9
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
        Me.CmdView.Location = New System.Drawing.Point(638, 13)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(67, 37)
        Me.CmdView.TabIndex = 8
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
        Me.CmdPreview.Location = New System.Drawing.Point(572, 13)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.TabIndex = 7
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
        Me.cmdPrint.Location = New System.Drawing.Point(505, 13)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 6
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
        Me.cmdSavePrint.Location = New System.Drawing.Point(439, 13)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdSavePrint.TabIndex = 5
        Me.cmdSavePrint.Text = "Discrepancy"
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
        Me.cmdDetail.Location = New System.Drawing.Point(372, 13)
        Me.cmdDetail.Name = "cmdDetail"
        Me.cmdDetail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDetail.Size = New System.Drawing.Size(67, 37)
        Me.cmdDetail.TabIndex = 4
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
        Me.cmdDelete.Location = New System.Drawing.Point(305, 13)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDelete.Size = New System.Drawing.Size(67, 37)
        Me.cmdDelete.TabIndex = 3
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
        Me.cmdSave.Location = New System.Drawing.Point(238, 13)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(67, 37)
        Me.cmdSave.TabIndex = 2
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
        Me.cmdModify.Location = New System.Drawing.Point(171, 13)
        Me.cmdModify.Name = "cmdModify"
        Me.cmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdModify.Size = New System.Drawing.Size(67, 37)
        Me.cmdModify.TabIndex = 1
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
        Me.cmdAdd.Location = New System.Drawing.Point(104, 13)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAdd.Size = New System.Drawing.Size(67, 37)
        Me.cmdAdd.TabIndex = 0
        Me.cmdAdd.Text = "&Add"
        Me.cmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdAdd, "Add New")
        Me.cmdAdd.UseVisualStyleBackColor = False
        '
        'cmdBillToSearch
        '
        Me.cmdBillToSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdBillToSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdBillToSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdBillToSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdBillToSearch.Image = CType(resources.GetObject("cmdBillToSearch.Image"), System.Drawing.Image)
        Me.cmdBillToSearch.Location = New System.Drawing.Point(664, 60)
        Me.cmdBillToSearch.Name = "cmdBillToSearch"
        Me.cmdBillToSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdBillToSearch.Size = New System.Drawing.Size(28, 23)
        Me.cmdBillToSearch.TabIndex = 147
        Me.cmdBillToSearch.TabStop = False
        Me.cmdBillToSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdBillToSearch, "Search")
        Me.cmdBillToSearch.UseVisualStyleBackColor = False
        '
        'cmdShipToSearch
        '
        Me.cmdShipToSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdShipToSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdShipToSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdShipToSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdShipToSearch.Image = CType(resources.GetObject("cmdShipToSearch.Image"), System.Drawing.Image)
        Me.cmdShipToSearch.Location = New System.Drawing.Point(692, 180)
        Me.cmdShipToSearch.Name = "cmdShipToSearch"
        Me.cmdShipToSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShipToSearch.Size = New System.Drawing.Size(28, 23)
        Me.cmdShipToSearch.TabIndex = 140
        Me.cmdShipToSearch.TabStop = False
        Me.cmdShipToSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShipToSearch, "Search")
        Me.cmdShipToSearch.UseVisualStyleBackColor = False
        '
        'cmdInterUnitBill
        '
        Me.cmdInterUnitBill.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdInterUnitBill.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdInterUnitBill.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdInterUnitBill.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdInterUnitBill.Location = New System.Drawing.Point(607, 100)
        Me.cmdInterUnitBill.Name = "cmdInterUnitBill"
        Me.cmdInterUnitBill.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdInterUnitBill.Size = New System.Drawing.Size(114, 42)
        Me.cmdInterUnitBill.TabIndex = 12
        Me.cmdInterUnitBill.TabStop = False
        Me.cmdInterUnitBill.Text = "Search Pending InterUnit Bill"
        Me.ToolTip1.SetToolTip(Me.cmdInterUnitBill, "Search")
        Me.cmdInterUnitBill.UseVisualStyleBackColor = False
        '
        'cmdDeliveryToLocSearch
        '
        Me.cmdDeliveryToLocSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdDeliveryToLocSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDeliveryToLocSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDeliveryToLocSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDeliveryToLocSearch.Image = CType(resources.GetObject("cmdDeliveryToLocSearch.Image"), System.Drawing.Image)
        Me.cmdDeliveryToLocSearch.Location = New System.Drawing.Point(692, 209)
        Me.cmdDeliveryToLocSearch.Name = "cmdDeliveryToLocSearch"
        Me.cmdDeliveryToLocSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDeliveryToLocSearch.Size = New System.Drawing.Size(28, 23)
        Me.cmdDeliveryToLocSearch.TabIndex = 146
        Me.cmdDeliveryToLocSearch.TabStop = False
        Me.cmdDeliveryToLocSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdDeliveryToLocSearch, "Search")
        Me.cmdDeliveryToLocSearch.UseVisualStyleBackColor = False
        '
        'cmdSearchDeliveryTo
        '
        Me.cmdSearchDeliveryTo.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchDeliveryTo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchDeliveryTo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchDeliveryTo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchDeliveryTo.Image = CType(resources.GetObject("cmdSearchDeliveryTo.Image"), System.Drawing.Image)
        Me.cmdSearchDeliveryTo.Location = New System.Drawing.Point(360, 209)
        Me.cmdSearchDeliveryTo.Name = "cmdSearchDeliveryTo"
        Me.cmdSearchDeliveryTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchDeliveryTo.Size = New System.Drawing.Size(28, 23)
        Me.cmdSearchDeliveryTo.TabIndex = 145
        Me.cmdSearchDeliveryTo.TabStop = False
        Me.cmdSearchDeliveryTo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchDeliveryTo, "Search")
        Me.cmdSearchDeliveryTo.UseVisualStyleBackColor = False
        '
        'FraFront
        '
        Me.FraFront.BackColor = System.Drawing.SystemColors.Control
        Me.FraFront.Controls.Add(Me.cmdShow)
        Me.FraFront.Controls.Add(Me.txtScanning)
        Me.FraFront.Controls.Add(Me.Frame1)
        Me.FraFront.Controls.Add(Me.Frasupp)
        Me.FraFront.Controls.Add(Me.FraPO)
        Me.FraFront.Controls.Add(Me.Frasprd)
        Me.FraFront.Controls.Add(Me.Label2)
        Me.FraFront.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraFront.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraFront.Location = New System.Drawing.Point(0, -6)
        Me.FraFront.Name = "FraFront"
        Me.FraFront.Padding = New System.Windows.Forms.Padding(0)
        Me.FraFront.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraFront.Size = New System.Drawing.Size(908, 574)
        Me.FraFront.TabIndex = 43
        Me.FraFront.TabStop = False
        '
        'txtScanning
        '
        Me.txtScanning.AcceptsReturn = True
        Me.txtScanning.BackColor = System.Drawing.SystemColors.Window
        Me.txtScanning.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtScanning.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtScanning.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtScanning.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtScanning.Location = New System.Drawing.Point(116, 548)
        Me.txtScanning.MaxLength = 0
        Me.txtScanning.Multiline = True
        Me.txtScanning.Name = "txtScanning"
        Me.txtScanning.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtScanning.Size = New System.Drawing.Size(591, 19)
        Me.txtScanning.TabIndex = 30
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
        Me.Frame1.Size = New System.Drawing.Size(172, 42)
        Me.Frame1.TabIndex = 54
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
        Me.cboRefType.Location = New System.Drawing.Point(2, 14)
        Me.cboRefType.Name = "cboRefType"
        Me.cboRefType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboRefType.Size = New System.Drawing.Size(163, 21)
        Me.cboRefType.TabIndex = 0
        '
        'Frasupp
        '
        Me.Frasupp.BackColor = System.Drawing.SystemColors.Control
        Me.Frasupp.Controls.Add(Me.txtOldERPNo)
        Me.Frasupp.Controls.Add(Me.txtOldERPDate)
        Me.Frasupp.Controls.Add(Me.Label18)
        Me.Frasupp.Controls.Add(Me.Label20)
        Me.Frasupp.Controls.Add(Me.cmdInterUnitBill)
        Me.Frasupp.Controls.Add(Me.cmdBillToSearch)
        Me.Frasupp.Controls.Add(Me.txtBillTo)
        Me.Frasupp.Controls.Add(Me.Label37)
        Me.Frasupp.Controls.Add(Me.chkUnderChallan)
        Me.Frasupp.Controls.Add(Me.txtEwayBillNo)
        Me.Frasupp.Controls.Add(Me.cboDivision)
        Me.Frasupp.Controls.Add(Me.TxtRemarks)
        Me.Frasupp.Controls.Add(Me.cmdsearch)
        Me.Frasupp.Controls.Add(Me.chkMRRMade)
        Me.Frasupp.Controls.Add(Me.TxtItemDesc)
        Me.Frasupp.Controls.Add(Me.TxtSupplier)
        Me.Frasupp.Controls.Add(Me.txtMRRNo)
        Me.Frasupp.Controls.Add(Me.txtMRRDate)
        Me.Frasupp.Controls.Add(Me.txtBillNo)
        Me.Frasupp.Controls.Add(Me.txtBillDate)
        Me.Frasupp.Controls.Add(Me.Label19)
        Me.Frasupp.Controls.Add(Me.Label12)
        Me.Frasupp.Controls.Add(Me.Label25)
        Me.Frasupp.Controls.Add(Me.lblEntryDate)
        Me.Frasupp.Controls.Add(Me.Label23)
        Me.Frasupp.Controls.Add(Me.Label14)
        Me.Frasupp.Controls.Add(Me.Label15)
        Me.Frasupp.Controls.Add(Me.Label5)
        Me.Frasupp.Controls.Add(Me.Label6)
        Me.Frasupp.Controls.Add(Me.Label4)
        Me.Frasupp.Controls.Add(Me.LblMkey)
        Me.Frasupp.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frasupp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frasupp.Location = New System.Drawing.Point(177, 0)
        Me.Frasupp.Name = "Frasupp"
        Me.Frasupp.Padding = New System.Windows.Forms.Padding(0)
        Me.Frasupp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frasupp.Size = New System.Drawing.Size(730, 160)
        Me.Frasupp.TabIndex = 46
        Me.Frasupp.TabStop = False
        '
        'txtOldERPNo
        '
        Me.txtOldERPNo.AcceptsReturn = True
        Me.txtOldERPNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtOldERPNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOldERPNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOldERPNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOldERPNo.ForeColor = System.Drawing.Color.Blue
        Me.txtOldERPNo.Location = New System.Drawing.Point(78, 133)
        Me.txtOldERPNo.MaxLength = 0
        Me.txtOldERPNo.Name = "txtOldERPNo"
        Me.txtOldERPNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOldERPNo.Size = New System.Drawing.Size(91, 22)
        Me.txtOldERPNo.TabIndex = 10
        '
        'txtOldERPDate
        '
        Me.txtOldERPDate.AcceptsReturn = True
        Me.txtOldERPDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtOldERPDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOldERPDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOldERPDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOldERPDate.ForeColor = System.Drawing.Color.Blue
        Me.txtOldERPDate.Location = New System.Drawing.Point(230, 133)
        Me.txtOldERPDate.MaxLength = 0
        Me.txtOldERPDate.Name = "txtOldERPDate"
        Me.txtOldERPDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOldERPDate.Size = New System.Drawing.Size(81, 22)
        Me.txtOldERPDate.TabIndex = 11
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(4, 137)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(71, 13)
        Me.Label18.TabIndex = 152
        Me.Label18.Text = "Old ERP No :"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(194, 135)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(37, 13)
        Me.Label20.TabIndex = 151
        Me.Label20.Text = "Date :"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtBillTo
        '
        Me.txtBillTo.AcceptsReturn = True
        Me.txtBillTo.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillTo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillTo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillTo.ForeColor = System.Drawing.Color.Blue
        Me.txtBillTo.Location = New System.Drawing.Point(554, 61)
        Me.txtBillTo.MaxLength = 0
        Me.txtBillTo.Name = "txtBillTo"
        Me.txtBillTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillTo.Size = New System.Drawing.Size(110, 22)
        Me.txtBillTo.TabIndex = 4
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.BackColor = System.Drawing.SystemColors.Control
        Me.Label37.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label37.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label37.Location = New System.Drawing.Point(493, 66)
        Me.Label37.Name = "Label37"
        Me.Label37.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label37.Size = New System.Drawing.Size(56, 13)
        Me.Label37.TabIndex = 146
        Me.Label37.Text = "Location :"
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'chkUnderChallan
        '
        Me.chkUnderChallan.BackColor = System.Drawing.SystemColors.Control
        Me.chkUnderChallan.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkUnderChallan.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkUnderChallan.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkUnderChallan.Location = New System.Drawing.Point(554, 37)
        Me.chkUnderChallan.Name = "chkUnderChallan"
        Me.chkUnderChallan.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkUnderChallan.Size = New System.Drawing.Size(103, 20)
        Me.chkUnderChallan.TabIndex = 9
        Me.chkUnderChallan.Text = "Under Challan"
        Me.chkUnderChallan.UseVisualStyleBackColor = False
        '
        'txtEwayBillNo
        '
        Me.txtEwayBillNo.AcceptsReturn = True
        Me.txtEwayBillNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtEwayBillNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEwayBillNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEwayBillNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEwayBillNo.ForeColor = System.Drawing.Color.Blue
        Me.txtEwayBillNo.Location = New System.Drawing.Point(398, 85)
        Me.txtEwayBillNo.MaxLength = 0
        Me.txtEwayBillNo.Name = "txtEwayBillNo"
        Me.txtEwayBillNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEwayBillNo.Size = New System.Drawing.Size(164, 22)
        Me.txtEwayBillNo.TabIndex = 7
        '
        'cboDivision
        '
        Me.cboDivision.BackColor = System.Drawing.SystemColors.Window
        Me.cboDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivision.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDivision.Location = New System.Drawing.Point(78, 13)
        Me.cboDivision.Name = "cboDivision"
        Me.cboDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivision.Size = New System.Drawing.Size(392, 21)
        Me.cboDivision.TabIndex = 0
        '
        'TxtRemarks
        '
        Me.TxtRemarks.AcceptsReturn = True
        Me.TxtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.TxtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtRemarks.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRemarks.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtRemarks.Location = New System.Drawing.Point(398, 109)
        Me.TxtRemarks.MaxLength = 0
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtRemarks.Size = New System.Drawing.Size(166, 22)
        Me.TxtRemarks.TabIndex = 9
        '
        'chkMRRMade
        '
        Me.chkMRRMade.AutoCheck = False
        Me.chkMRRMade.BackColor = System.Drawing.SystemColors.Control
        Me.chkMRRMade.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkMRRMade.Enabled = False
        Me.chkMRRMade.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMRRMade.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkMRRMade.Location = New System.Drawing.Point(552, 13)
        Me.chkMRRMade.Name = "chkMRRMade"
        Me.chkMRRMade.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkMRRMade.Size = New System.Drawing.Size(111, 20)
        Me.chkMRRMade.TabIndex = 6
        Me.chkMRRMade.Text = "MRR Made"
        Me.chkMRRMade.UseVisualStyleBackColor = False
        '
        'TxtItemDesc
        '
        Me.TxtItemDesc.AcceptsReturn = True
        Me.TxtItemDesc.BackColor = System.Drawing.SystemColors.Window
        Me.TxtItemDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtItemDesc.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtItemDesc.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtItemDesc.ForeColor = System.Drawing.Color.Blue
        Me.TxtItemDesc.Location = New System.Drawing.Point(78, 109)
        Me.TxtItemDesc.MaxLength = 0
        Me.TxtItemDesc.Name = "TxtItemDesc"
        Me.TxtItemDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtItemDesc.Size = New System.Drawing.Size(233, 22)
        Me.TxtItemDesc.TabIndex = 8
        '
        'TxtSupplier
        '
        Me.TxtSupplier.AcceptsReturn = True
        Me.TxtSupplier.BackColor = System.Drawing.SystemColors.Window
        Me.TxtSupplier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtSupplier.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtSupplier.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSupplier.ForeColor = System.Drawing.Color.Blue
        Me.TxtSupplier.Location = New System.Drawing.Point(78, 61)
        Me.TxtSupplier.MaxLength = 0
        Me.TxtSupplier.Name = "TxtSupplier"
        Me.TxtSupplier.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtSupplier.Size = New System.Drawing.Size(384, 22)
        Me.TxtSupplier.TabIndex = 3
        '
        'txtMRRNo
        '
        Me.txtMRRNo.AcceptsReturn = True
        Me.txtMRRNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtMRRNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMRRNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMRRNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMRRNo.ForeColor = System.Drawing.Color.Blue
        Me.txtMRRNo.Location = New System.Drawing.Point(78, 37)
        Me.txtMRRNo.MaxLength = 0
        Me.txtMRRNo.Name = "txtMRRNo"
        Me.txtMRRNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMRRNo.Size = New System.Drawing.Size(85, 22)
        Me.txtMRRNo.TabIndex = 1
        '
        'txtMRRDate
        '
        Me.txtMRRDate.AcceptsReturn = True
        Me.txtMRRDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtMRRDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMRRDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMRRDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMRRDate.ForeColor = System.Drawing.Color.Blue
        Me.txtMRRDate.Location = New System.Drawing.Point(230, 37)
        Me.txtMRRDate.MaxLength = 0
        Me.txtMRRDate.Name = "txtMRRDate"
        Me.txtMRRDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMRRDate.Size = New System.Drawing.Size(81, 22)
        Me.txtMRRDate.TabIndex = 2
        '
        'txtBillNo
        '
        Me.txtBillNo.AcceptsReturn = True
        Me.txtBillNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillNo.ForeColor = System.Drawing.Color.Blue
        Me.txtBillNo.Location = New System.Drawing.Point(78, 85)
        Me.txtBillNo.MaxLength = 0
        Me.txtBillNo.Name = "txtBillNo"
        Me.txtBillNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillNo.Size = New System.Drawing.Size(91, 22)
        Me.txtBillNo.TabIndex = 5
        '
        'txtBillDate
        '
        Me.txtBillDate.AcceptsReturn = True
        Me.txtBillDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillDate.ForeColor = System.Drawing.Color.Blue
        Me.txtBillDate.Location = New System.Drawing.Point(230, 85)
        Me.txtBillDate.MaxLength = 0
        Me.txtBillDate.Name = "txtBillDate"
        Me.txtBillDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillDate.Size = New System.Drawing.Size(81, 22)
        Me.txtBillDate.TabIndex = 6
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(315, 90)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(79, 13)
        Me.Label19.TabIndex = 104
        Me.Label19.Text = "eWay Bill No :"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(19, 14)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(54, 13)
        Me.Label12.TabIndex = 101
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
        Me.Label25.Location = New System.Drawing.Point(337, 112)
        Me.Label25.Name = "Label25"
        Me.Label25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label25.Size = New System.Drawing.Size(57, 13)
        Me.Label25.TabIndex = 94
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
        Me.lblEntryDate.Location = New System.Drawing.Point(314, 37)
        Me.lblEntryDate.Name = "lblEntryDate"
        Me.lblEntryDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblEntryDate.Size = New System.Drawing.Size(157, 19)
        Me.lblEntryDate.TabIndex = 80
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.SystemColors.Control
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(10, 112)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(63, 13)
        Me.Label23.TabIndex = 53
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
        Me.Label14.Location = New System.Drawing.Point(18, 40)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(55, 13)
        Me.Label14.TabIndex = 52
        Me.Label14.Text = "Gate No :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(194, 41)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(37, 13)
        Me.Label15.TabIndex = 51
        Me.Label15.Text = "Date :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(26, 91)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(47, 13)
        Me.Label5.TabIndex = 50
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
        Me.Label6.Location = New System.Drawing.Point(194, 87)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(37, 13)
        Me.Label6.TabIndex = 49
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
        Me.Label4.Location = New System.Drawing.Point(18, 65)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(55, 13)
        Me.Label4.TabIndex = 48
        Me.Label4.Text = "Supplier :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LblMkey
        '
        Me.LblMkey.BackColor = System.Drawing.SystemColors.Control
        Me.LblMkey.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblMkey.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMkey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblMkey.Location = New System.Drawing.Point(558, 16)
        Me.LblMkey.Name = "LblMkey"
        Me.LblMkey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblMkey.Size = New System.Drawing.Size(31, 11)
        Me.LblMkey.TabIndex = 47
        Me.LblMkey.Text = "MKEY"
        Me.LblMkey.Visible = False
        '
        'FraPO
        '
        Me.FraPO.BackColor = System.Drawing.SystemColors.Control
        Me.FraPO.Controls.Add(Me.CboPONo)
        Me.FraPO.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraPO.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraPO.Location = New System.Drawing.Point(0, 50)
        Me.FraPO.Name = "FraPO"
        Me.FraPO.Padding = New System.Windows.Forms.Padding(0)
        Me.FraPO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraPO.Size = New System.Drawing.Size(172, 110)
        Me.FraPO.TabIndex = 45
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
        Me.CboPONo.Size = New System.Drawing.Size(166, 90)
        Me.CboPONo.TabIndex = 0
        '
        'Frasprd
        '
        Me.Frasprd.BackColor = System.Drawing.SystemColors.Control
        Me.Frasprd.Controls.Add(Me.lblTotIGST)
        Me.Frasprd.Controls.Add(Me.Label21)
        Me.Frasprd.Controls.Add(Me.SSTab1)
        Me.Frasprd.Controls.Add(Me.SprdExp)
        Me.Frasprd.Controls.Add(Me.lblEDUAmount)
        Me.Frasprd.Controls.Add(Me.lblEDUPercent)
        Me.Frasprd.Controls.Add(Me.lblTotPackQtyCap)
        Me.Frasprd.Controls.Add(Me.lblTotQty)
        Me.Frasprd.Controls.Add(Me.lblNetAmount)
        Me.Frasprd.Controls.Add(Me.Label13)
        Me.Frasprd.Controls.Add(Me.lblTotItemValue)
        Me.Frasprd.Controls.Add(Me.Label16)
        Me.Frasprd.Controls.Add(Me.lblTotCGST)
        Me.Frasprd.Controls.Add(Me.Label17)
        Me.Frasprd.Controls.Add(Me.lblTotSGST)
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
        Me.Frasprd.Location = New System.Drawing.Point(0, 155)
        Me.Frasprd.Name = "Frasprd"
        Me.Frasprd.Padding = New System.Windows.Forms.Padding(0)
        Me.Frasprd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frasprd.Size = New System.Drawing.Size(902, 391)
        Me.Frasprd.TabIndex = 40
        Me.Frasprd.TabStop = False
        '
        'lblTotIGST
        '
        Me.lblTotIGST.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotIGST.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotIGST.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotIGST.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotIGST.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotIGST.Location = New System.Drawing.Point(814, 348)
        Me.lblTotIGST.Name = "lblTotIGST"
        Me.lblTotIGST.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotIGST.Size = New System.Drawing.Size(85, 19)
        Me.lblTotIGST.TabIndex = 91
        Me.lblTotIGST.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.SystemColors.Control
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label21.Location = New System.Drawing.Point(774, 352)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(36, 13)
        Me.Label21.TabIndex = 90
        Me.Label21.Text = "IGST :"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'SSTab1
        '
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage0)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage1)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage2)
        Me.SSTab1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SSTab1.ItemSize = New System.Drawing.Size(42, 18)
        Me.SSTab1.Location = New System.Drawing.Point(2, 11)
        Me.SSTab1.Name = "SSTab1"
        Me.SSTab1.SelectedIndex = 2
        Me.SSTab1.Size = New System.Drawing.Size(900, 264)
        Me.SSTab1.TabIndex = 3
        '
        '_SSTab1_TabPage0
        '
        Me._SSTab1_TabPage0.Controls.Add(Me.SprdMain)
        Me._SSTab1_TabPage0.Location = New System.Drawing.Point(4, 22)
        Me._SSTab1_TabPage0.Name = "_SSTab1_TabPage0"
        Me._SSTab1_TabPage0.Size = New System.Drawing.Size(892, 238)
        Me._SSTab1_TabPage0.TabIndex = 0
        Me._SSTab1_TabPage0.Text = "Item Details"
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 3)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(888, 234)
        Me.SprdMain.TabIndex = 0
        '
        '_SSTab1_TabPage1
        '
        Me._SSTab1_TabPage1.Controls.Add(Me.fraFreight)
        Me._SSTab1_TabPage1.Location = New System.Drawing.Point(4, 22)
        Me._SSTab1_TabPage1.Name = "_SSTab1_TabPage1"
        Me._SSTab1_TabPage1.Size = New System.Drawing.Size(892, 238)
        Me._SSTab1_TabPage1.TabIndex = 1
        Me._SSTab1_TabPage1.Text = "Freight Details"
        '
        'fraFreight
        '
        Me.fraFreight.BackColor = System.Drawing.SystemColors.Control
        Me.fraFreight.Controls.Add(Me.cmdDeliveryToLocSearch)
        Me.fraFreight.Controls.Add(Me.cmdSearchDeliveryTo)
        Me.fraFreight.Controls.Add(Me.txtDeliveryToLoc)
        Me.fraFreight.Controls.Add(Me.Label22)
        Me.fraFreight.Controls.Add(Me.txtDeliveryTo)
        Me.fraFreight.Controls.Add(Me.Label24)
        Me.fraFreight.Controls.Add(Me.cmdShipToSearch)
        Me.fraFreight.Controls.Add(Me.TxtShipTo)
        Me.fraFreight.Controls.Add(Me.Label38)
        Me.fraFreight.Controls.Add(Me.txtST38No)
        Me.fraFreight.Controls.Add(Me.chkShipTo)
        Me.fraFreight.Controls.Add(Me.cmdSearchShippedTo)
        Me.fraFreight.Controls.Add(Me.txtShippedTo)
        Me.fraFreight.Controls.Add(Me.cboMode)
        Me.fraFreight.Controls.Add(Me.txtGRNo)
        Me.fraFreight.Controls.Add(Me.txtGRDate)
        Me.fraFreight.Controls.Add(Me.txtVehicle)
        Me.fraFreight.Controls.Add(Me.txtDocsThru)
        Me.fraFreight.Controls.Add(Me._OptFreight_0)
        Me.fraFreight.Controls.Add(Me._OptFreight_1)
        Me.fraFreight.Controls.Add(Me.txtFreight)
        Me.fraFreight.Controls.Add(Me.txtFormDetail)
        Me.fraFreight.Controls.Add(Me.TxtTransporter)
        Me.fraFreight.Controls.Add(Me.Label7)
        Me.fraFreight.Controls.Add(Me.Label9)
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
        Me.fraFreight.Location = New System.Drawing.Point(2, -2)
        Me.fraFreight.Name = "fraFreight"
        Me.fraFreight.Padding = New System.Windows.Forms.Padding(0)
        Me.fraFreight.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraFreight.Size = New System.Drawing.Size(888, 236)
        Me.fraFreight.TabIndex = 90
        Me.fraFreight.TabStop = False
        '
        'txtDeliveryToLoc
        '
        Me.txtDeliveryToLoc.AcceptsReturn = True
        Me.txtDeliveryToLoc.BackColor = System.Drawing.SystemColors.Window
        Me.txtDeliveryToLoc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDeliveryToLoc.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDeliveryToLoc.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeliveryToLoc.ForeColor = System.Drawing.Color.Blue
        Me.txtDeliveryToLoc.Location = New System.Drawing.Point(589, 209)
        Me.txtDeliveryToLoc.MaxLength = 0
        Me.txtDeliveryToLoc.Name = "txtDeliveryToLoc"
        Me.txtDeliveryToLoc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDeliveryToLoc.Size = New System.Drawing.Size(102, 22)
        Me.txtDeliveryToLoc.TabIndex = 143
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.SystemColors.Control
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(469, 213)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(116, 13)
        Me.Label22.TabIndex = 144
        Me.Label22.Text = "Delivery To Location :"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtDeliveryTo
        '
        Me.txtDeliveryTo.AcceptsReturn = True
        Me.txtDeliveryTo.BackColor = System.Drawing.SystemColors.Window
        Me.txtDeliveryTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDeliveryTo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDeliveryTo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeliveryTo.ForeColor = System.Drawing.Color.Blue
        Me.txtDeliveryTo.Location = New System.Drawing.Point(104, 209)
        Me.txtDeliveryTo.MaxLength = 0
        Me.txtDeliveryTo.Name = "txtDeliveryTo"
        Me.txtDeliveryTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDeliveryTo.Size = New System.Drawing.Size(254, 22)
        Me.txtDeliveryTo.TabIndex = 141
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label24.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.Black
        Me.Label24.Location = New System.Drawing.Point(30, 213)
        Me.Label24.Name = "Label24"
        Me.Label24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label24.Size = New System.Drawing.Size(70, 13)
        Me.Label24.TabIndex = 142
        Me.Label24.Text = "Delivery To :"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.TopRight
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
        Me.TxtShipTo.Location = New System.Drawing.Point(589, 181)
        Me.TxtShipTo.MaxLength = 0
        Me.TxtShipTo.Name = "TxtShipTo"
        Me.TxtShipTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtShipTo.Size = New System.Drawing.Size(102, 22)
        Me.TxtShipTo.TabIndex = 138
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.BackColor = System.Drawing.SystemColors.Control
        Me.Label38.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label38.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label38.Location = New System.Drawing.Point(495, 185)
        Me.Label38.Name = "Label38"
        Me.Label38.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label38.Size = New System.Drawing.Size(90, 13)
        Me.Label38.TabIndex = 139
        Me.Label38.Text = "Bill To Location :"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtST38No
        '
        Me.txtST38No.AcceptsReturn = True
        Me.txtST38No.BackColor = System.Drawing.SystemColors.Window
        Me.txtST38No.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtST38No.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtST38No.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtST38No.ForeColor = System.Drawing.Color.Blue
        Me.txtST38No.Location = New System.Drawing.Point(588, 13)
        Me.txtST38No.MaxLength = 0
        Me.txtST38No.Name = "txtST38No"
        Me.txtST38No.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtST38No.Size = New System.Drawing.Size(254, 22)
        Me.txtST38No.TabIndex = 23
        '
        'chkShipTo
        '
        Me.chkShipTo.BackColor = System.Drawing.SystemColors.Control
        Me.chkShipTo.Checked = True
        Me.chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkShipTo.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkShipTo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShipTo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkShipTo.Location = New System.Drawing.Point(104, 159)
        Me.chkShipTo.Name = "chkShipTo"
        Me.chkShipTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkShipTo.Size = New System.Drawing.Size(293, 16)
        Me.chkShipTo.TabIndex = 27
        Me.chkShipTo.Text = "'Shipped From' Same as 'Billed From' (Yes / No)"
        Me.chkShipTo.UseVisualStyleBackColor = False
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
        Me.txtShippedTo.Location = New System.Drawing.Point(104, 181)
        Me.txtShippedTo.MaxLength = 0
        Me.txtShippedTo.Name = "txtShippedTo"
        Me.txtShippedTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtShippedTo.Size = New System.Drawing.Size(254, 22)
        Me.txtShippedTo.TabIndex = 28
        '
        'cboMode
        '
        Me.cboMode.BackColor = System.Drawing.SystemColors.Window
        Me.cboMode.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboMode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboMode.Location = New System.Drawing.Point(104, 43)
        Me.cboMode.Name = "cboMode"
        Me.cboMode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboMode.Size = New System.Drawing.Size(267, 21)
        Me.cboMode.TabIndex = 18
        '
        'txtGRNo
        '
        Me.txtGRNo.AcceptsReturn = True
        Me.txtGRNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtGRNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGRNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGRNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGRNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtGRNo.Location = New System.Drawing.Point(588, 43)
        Me.txtGRNo.MaxLength = 0
        Me.txtGRNo.Name = "txtGRNo"
        Me.txtGRNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGRNo.Size = New System.Drawing.Size(254, 22)
        Me.txtGRNo.TabIndex = 24
        '
        'txtGRDate
        '
        Me.txtGRDate.AcceptsReturn = True
        Me.txtGRDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtGRDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGRDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGRDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGRDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtGRDate.Location = New System.Drawing.Point(588, 73)
        Me.txtGRDate.MaxLength = 0
        Me.txtGRDate.Name = "txtGRDate"
        Me.txtGRDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGRDate.Size = New System.Drawing.Size(254, 22)
        Me.txtGRDate.TabIndex = 25
        '
        'txtVehicle
        '
        Me.txtVehicle.AcceptsReturn = True
        Me.txtVehicle.BackColor = System.Drawing.SystemColors.Window
        Me.txtVehicle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVehicle.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVehicle.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVehicle.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtVehicle.Location = New System.Drawing.Point(104, 103)
        Me.txtVehicle.MaxLength = 0
        Me.txtVehicle.Name = "txtVehicle"
        Me.txtVehicle.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVehicle.Size = New System.Drawing.Size(267, 22)
        Me.txtVehicle.TabIndex = 20
        '
        'txtDocsThru
        '
        Me.txtDocsThru.AcceptsReturn = True
        Me.txtDocsThru.BackColor = System.Drawing.SystemColors.Window
        Me.txtDocsThru.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDocsThru.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDocsThru.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDocsThru.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDocsThru.Location = New System.Drawing.Point(104, 133)
        Me.txtDocsThru.MaxLength = 0
        Me.txtDocsThru.Name = "txtDocsThru"
        Me.txtDocsThru.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDocsThru.Size = New System.Drawing.Size(267, 22)
        Me.txtDocsThru.TabIndex = 21
        '
        '_OptFreight_0
        '
        Me._OptFreight_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptFreight_0.Checked = True
        Me._OptFreight_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptFreight_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptFreight_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptFreight.SetIndex(Me._OptFreight_0, CType(0, Short))
        Me._OptFreight_0.Location = New System.Drawing.Point(104, 16)
        Me._OptFreight_0.Name = "_OptFreight_0"
        Me._OptFreight_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptFreight_0.Size = New System.Drawing.Size(65, 18)
        Me._OptFreight_0.TabIndex = 16
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
        Me._OptFreight_1.Location = New System.Drawing.Point(218, 16)
        Me._OptFreight_1.Name = "_OptFreight_1"
        Me._OptFreight_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptFreight_1.Size = New System.Drawing.Size(49, 18)
        Me._OptFreight_1.TabIndex = 17
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
        Me.txtFreight.Location = New System.Drawing.Point(588, 103)
        Me.txtFreight.MaxLength = 0
        Me.txtFreight.Name = "txtFreight"
        Me.txtFreight.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFreight.Size = New System.Drawing.Size(254, 22)
        Me.txtFreight.TabIndex = 26
        '
        'txtFormDetail
        '
        Me.txtFormDetail.AcceptsReturn = True
        Me.txtFormDetail.BackColor = System.Drawing.SystemColors.Window
        Me.txtFormDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFormDetail.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFormDetail.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFormDetail.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFormDetail.Location = New System.Drawing.Point(588, 133)
        Me.txtFormDetail.MaxLength = 0
        Me.txtFormDetail.Name = "txtFormDetail"
        Me.txtFormDetail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFormDetail.Size = New System.Drawing.Size(254, 22)
        Me.txtFormDetail.TabIndex = 22
        '
        'TxtTransporter
        '
        Me.TxtTransporter.AcceptsReturn = True
        Me.TxtTransporter.BackColor = System.Drawing.SystemColors.Window
        Me.TxtTransporter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtTransporter.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtTransporter.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTransporter.ForeColor = System.Drawing.Color.Black
        Me.TxtTransporter.Location = New System.Drawing.Point(104, 73)
        Me.TxtTransporter.MaxLength = 0
        Me.TxtTransporter.Name = "TxtTransporter"
        Me.TxtTransporter.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtTransporter.Size = New System.Drawing.Size(267, 22)
        Me.TxtTransporter.TabIndex = 19
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(512, 17)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(73, 13)
        Me.Label7.TabIndex = 105
        Me.Label7.Text = "ST 38/16 No :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(35, 185)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(65, 13)
        Me.Label9.TabIndex = 103
        Me.Label9.Text = "Ship From :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(539, 47)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(46, 13)
        Me.Label11.TabIndex = 100
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
        Me.Label10.Location = New System.Drawing.Point(530, 77)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(55, 13)
        Me.Label10.TabIndex = 99
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
        Me.Label27.Location = New System.Drawing.Point(57, 46)
        Me.Label27.Name = "Label27"
        Me.Label27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label27.Size = New System.Drawing.Size(43, 13)
        Me.Label27.TabIndex = 98
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
        Me.Label28.Location = New System.Drawing.Point(51, 10)
        Me.Label28.Name = "Label28"
        Me.Label28.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label28.Size = New System.Drawing.Size(49, 13)
        Me.Label28.TabIndex = 97
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
        Me.Label29.Location = New System.Drawing.Point(52, 102)
        Me.Label29.Name = "Label29"
        Me.Label29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label29.Size = New System.Drawing.Size(48, 13)
        Me.Label29.TabIndex = 96
        Me.Label29.Text = "Vehicle :"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.BackColor = System.Drawing.SystemColors.Control
        Me.Label31.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label31.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label31.Location = New System.Drawing.Point(37, 137)
        Me.Label31.Name = "Label31"
        Me.Label31.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label31.Size = New System.Drawing.Size(63, 13)
        Me.Label31.TabIndex = 95
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
        Me.Label1.Location = New System.Drawing.Point(493, 108)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(92, 13)
        Me.Label1.TabIndex = 93
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
        Me.Label8.Location = New System.Drawing.Point(507, 137)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(78, 13)
        Me.Label8.TabIndex = 92
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
        Me.Label3.Location = New System.Drawing.Point(29, 72)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(71, 13)
        Me.Label3.TabIndex = 91
        Me.Label3.Text = "Transporter :"
        '
        '_SSTab1_TabPage2
        '
        Me._SSTab1_TabPage2.Controls.Add(Me.Frame4)
        Me._SSTab1_TabPage2.Location = New System.Drawing.Point(4, 22)
        Me._SSTab1_TabPage2.Name = "_SSTab1_TabPage2"
        Me._SSTab1_TabPage2.Size = New System.Drawing.Size(892, 238)
        Me._SSTab1_TabPage2.TabIndex = 2
        Me._SSTab1_TabPage2.Text = "TC && Third Party Report"
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.txtTCPath)
        Me.Frame4.Controls.Add(Me.cmdTC)
        Me.Frame4.Controls.Add(Me.txtTPRPath)
        Me.Frame4.Controls.Add(Me.cmdTPRI)
        Me.Frame4.Controls.Add(Me.chkTCAvailable)
        Me.Frame4.Controls.Add(Me.chkTPRAvailable)
        Me.Frame4.Controls.Add(Me.cmdTCShow)
        Me.Frame4.Controls.Add(Me.cmdTPShow)
        Me.Frame4.Controls.Add(Me.Label35)
        Me.Frame4.Controls.Add(Me.Label36)
        Me.Frame4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(2, -2)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(890, 236)
        Me.Frame4.TabIndex = 106
        Me.Frame4.TabStop = False
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
        Me.txtTCPath.TabIndex = 114
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
        Me.txtTPRPath.TabIndex = 112
        '
        'chkTCAvailable
        '
        Me.chkTCAvailable.BackColor = System.Drawing.SystemColors.Control
        Me.chkTCAvailable.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkTCAvailable.Enabled = False
        Me.chkTCAvailable.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTCAvailable.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkTCAvailable.Location = New System.Drawing.Point(164, 25)
        Me.chkTCAvailable.Name = "chkTCAvailable"
        Me.chkTCAvailable.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkTCAvailable.Size = New System.Drawing.Size(197, 17)
        Me.chkTCAvailable.TabIndex = 110
        Me.chkTCAvailable.Text = "TC Available (Yes / No)"
        Me.chkTCAvailable.UseVisualStyleBackColor = False
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
        Me.chkTPRAvailable.Size = New System.Drawing.Size(275, 17)
        Me.chkTPRAvailable.TabIndex = 109
        Me.chkTPRAvailable.Text = "Third Party Report Available (Yes / No)"
        Me.chkTPRAvailable.UseVisualStyleBackColor = False
        '
        'cmdTCShow
        '
        Me.cmdTCShow.BackColor = System.Drawing.SystemColors.Control
        Me.cmdTCShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdTCShow.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdTCShow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdTCShow.Location = New System.Drawing.Point(628, 43)
        Me.cmdTCShow.Name = "cmdTCShow"
        Me.cmdTCShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdTCShow.Size = New System.Drawing.Size(77, 23)
        Me.cmdTCShow.TabIndex = 108
        Me.cmdTCShow.Text = "TC Show"
        Me.cmdTCShow.UseVisualStyleBackColor = False
        '
        'cmdTPShow
        '
        Me.cmdTPShow.BackColor = System.Drawing.SystemColors.Control
        Me.cmdTPShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdTPShow.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdTPShow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdTPShow.Location = New System.Drawing.Point(628, 101)
        Me.cmdTPShow.Name = "cmdTPShow"
        Me.cmdTPShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdTPShow.Size = New System.Drawing.Size(77, 24)
        Me.cmdTPShow.TabIndex = 107
        Me.cmdTPShow.Text = "TPR Show"
        Me.cmdTPShow.UseVisualStyleBackColor = False
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
        Me.Label35.TabIndex = 116
        Me.Label35.Text = "TC Upload :"
        Me.Label35.TextAlign = System.Drawing.ContentAlignment.TopRight
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
        Me.Label36.TabIndex = 115
        Me.Label36.Text = "Third Party Report Upload :"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'SprdExp
        '
        Me.SprdExp.DataSource = Nothing
        Me.SprdExp.Location = New System.Drawing.Point(2, 283)
        Me.SprdExp.Name = "SprdExp"
        Me.SprdExp.OcxState = CType(resources.GetObject("SprdExp.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdExp.Size = New System.Drawing.Size(394, 103)
        Me.SprdExp.TabIndex = 1
        '
        'lblEDUAmount
        '
        Me.lblEDUAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblEDUAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblEDUAmount.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEDUAmount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblEDUAmount.Location = New System.Drawing.Point(570, 330)
        Me.lblEDUAmount.Name = "lblEDUAmount"
        Me.lblEDUAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblEDUAmount.Size = New System.Drawing.Size(71, 13)
        Me.lblEDUAmount.TabIndex = 79
        Me.lblEDUAmount.Text = "lblEDUAmount"
        Me.lblEDUAmount.Visible = False
        '
        'lblEDUPercent
        '
        Me.lblEDUPercent.BackColor = System.Drawing.SystemColors.Control
        Me.lblEDUPercent.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblEDUPercent.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEDUPercent.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblEDUPercent.Location = New System.Drawing.Point(570, 356)
        Me.lblEDUPercent.Name = "lblEDUPercent"
        Me.lblEDUPercent.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblEDUPercent.Size = New System.Drawing.Size(71, 13)
        Me.lblEDUPercent.TabIndex = 78
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
        Me.lblTotPackQtyCap.Location = New System.Drawing.Point(516, 291)
        Me.lblTotPackQtyCap.Name = "lblTotPackQtyCap"
        Me.lblTotPackQtyCap.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotPackQtyCap.Size = New System.Drawing.Size(59, 13)
        Me.lblTotPackQtyCap.TabIndex = 74
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
        Me.lblTotQty.Location = New System.Drawing.Point(614, 290)
        Me.lblTotQty.Name = "lblTotQty"
        Me.lblTotQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotQty.Size = New System.Drawing.Size(99, 17)
        Me.lblTotQty.TabIndex = 73
        Me.lblTotQty.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblNetAmount
        '
        Me.lblNetAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblNetAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNetAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblNetAmount.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblNetAmount.Location = New System.Drawing.Point(814, 369)
        Me.lblNetAmount.Name = "lblNetAmount"
        Me.lblNetAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblNetAmount.Size = New System.Drawing.Size(85, 19)
        Me.lblNetAmount.TabIndex = 72
        Me.lblNetAmount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label13.Location = New System.Drawing.Point(736, 373)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(74, 13)
        Me.Label13.TabIndex = 71
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
        Me.lblTotItemValue.Location = New System.Drawing.Point(814, 288)
        Me.lblTotItemValue.Name = "lblTotItemValue"
        Me.lblTotItemValue.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotItemValue.Size = New System.Drawing.Size(85, 17)
        Me.lblTotItemValue.TabIndex = 70
        Me.lblTotItemValue.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label16.Location = New System.Drawing.Point(744, 289)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(66, 13)
        Me.Label16.TabIndex = 69
        Me.Label16.Text = "Item Value :"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotCGST
        '
        Me.lblTotCGST.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotCGST.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotCGST.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotCGST.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotCGST.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotCGST.Location = New System.Drawing.Point(814, 307)
        Me.lblTotCGST.Name = "lblTotCGST"
        Me.lblTotCGST.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotCGST.Size = New System.Drawing.Size(85, 19)
        Me.lblTotCGST.TabIndex = 68
        Me.lblTotCGST.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label17.Location = New System.Drawing.Point(770, 311)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(40, 13)
        Me.Label17.TabIndex = 67
        Me.Label17.Text = "CGST :"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotSGST
        '
        Me.lblTotSGST.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotSGST.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotSGST.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotSGST.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotSGST.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotSGST.Location = New System.Drawing.Point(814, 327)
        Me.lblTotSGST.Name = "lblTotSGST"
        Me.lblTotSGST.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotSGST.Size = New System.Drawing.Size(85, 19)
        Me.lblTotSGST.TabIndex = 66
        Me.lblTotSGST.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.BackColor = System.Drawing.SystemColors.Control
        Me.Label34.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label34.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label34.Location = New System.Drawing.Point(771, 331)
        Me.Label34.Name = "Label34"
        Me.Label34.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label34.Size = New System.Drawing.Size(39, 13)
        Me.Label34.TabIndex = 65
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
        Me.lblTotCharges.Location = New System.Drawing.Point(504, 318)
        Me.lblTotCharges.Name = "lblTotCharges"
        Me.lblTotCharges.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotCharges.Size = New System.Drawing.Size(13, 13)
        Me.lblTotCharges.TabIndex = 64
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
        Me.lblTotFreight.Location = New System.Drawing.Point(546, 318)
        Me.lblTotFreight.Name = "lblTotFreight"
        Me.lblTotFreight.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotFreight.Size = New System.Drawing.Size(13, 13)
        Me.lblTotFreight.TabIndex = 63
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
        Me.lblTotExpAmt.Location = New System.Drawing.Point(502, 336)
        Me.lblTotExpAmt.Name = "lblTotExpAmt"
        Me.lblTotExpAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotExpAmt.Size = New System.Drawing.Size(13, 13)
        Me.lblTotExpAmt.TabIndex = 62
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
        Me.lblEDPercentage.Location = New System.Drawing.Point(546, 338)
        Me.lblEDPercentage.Name = "lblEDPercentage"
        Me.lblEDPercentage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblEDPercentage.Size = New System.Drawing.Size(13, 13)
        Me.lblEDPercentage.TabIndex = 61
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
        Me.lblSTPercentage.Location = New System.Drawing.Point(506, 354)
        Me.lblSTPercentage.Name = "lblSTPercentage"
        Me.lblSTPercentage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSTPercentage.Size = New System.Drawing.Size(13, 13)
        Me.lblSTPercentage.TabIndex = 60
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
        Me.lblTotTaxableAmt.Location = New System.Drawing.Point(546, 358)
        Me.lblTotTaxableAmt.Name = "lblTotTaxableAmt"
        Me.lblTotTaxableAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotTaxableAmt.Size = New System.Drawing.Size(13, 13)
        Me.lblTotTaxableAmt.TabIndex = 59
        Me.lblTotTaxableAmt.Text = "0"
        Me.lblTotTaxableAmt.Visible = False
        '
        'lblDiscount
        '
        Me.lblDiscount.BackColor = System.Drawing.SystemColors.Control
        Me.lblDiscount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDiscount.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDiscount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDiscount.Location = New System.Drawing.Point(440, 316)
        Me.lblDiscount.Name = "lblDiscount"
        Me.lblDiscount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDiscount.Size = New System.Drawing.Size(59, 11)
        Me.lblDiscount.TabIndex = 58
        Me.lblDiscount.Text = "lblDiscount"
        Me.lblDiscount.Visible = False
        '
        'lblSurcharge
        '
        Me.lblSurcharge.BackColor = System.Drawing.SystemColors.Control
        Me.lblSurcharge.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSurcharge.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSurcharge.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSurcharge.Location = New System.Drawing.Point(440, 332)
        Me.lblSurcharge.Name = "lblSurcharge"
        Me.lblSurcharge.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSurcharge.Size = New System.Drawing.Size(47, 11)
        Me.lblSurcharge.TabIndex = 57
        Me.lblSurcharge.Text = "lblSurcharge"
        Me.lblSurcharge.Visible = False
        '
        'lblRO
        '
        Me.lblRO.BackColor = System.Drawing.SystemColors.Control
        Me.lblRO.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblRO.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRO.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRO.Location = New System.Drawing.Point(440, 348)
        Me.lblRO.Name = "lblRO"
        Me.lblRO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblRO.Size = New System.Drawing.Size(41, 11)
        Me.lblRO.TabIndex = 56
        Me.lblRO.Text = "lblRO"
        Me.lblRO.Visible = False
        '
        'lblMSC
        '
        Me.lblMSC.BackColor = System.Drawing.SystemColors.Control
        Me.lblMSC.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMSC.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMSC.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMSC.Location = New System.Drawing.Point(440, 360)
        Me.lblMSC.Name = "lblMSC"
        Me.lblMSC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMSC.Size = New System.Drawing.Size(49, 11)
        Me.lblMSC.TabIndex = 55
        Me.lblMSC.Text = "lblMSC"
        Me.lblMSC.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(2, 550)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(105, 13)
        Me.Label2.TabIndex = 75
        Me.Label2.Text = "BarCode Scanning :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'FraDetail
        '
        Me.FraDetail.BackColor = System.Drawing.SystemColors.Control
        Me.FraDetail.Controls.Add(Me.lblModDate)
        Me.FraDetail.Controls.Add(Me.Label48)
        Me.FraDetail.Controls.Add(Me.lblAddDate)
        Me.FraDetail.Controls.Add(Me.Label45)
        Me.FraDetail.Controls.Add(Me.lblModUser)
        Me.FraDetail.Controls.Add(Me.Label46)
        Me.FraDetail.Controls.Add(Me.lblAddUser)
        Me.FraDetail.Controls.Add(Me.Label44)
        Me.FraDetail.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraDetail.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraDetail.Location = New System.Drawing.Point(2, 510)
        Me.FraDetail.Name = "FraDetail"
        Me.FraDetail.Padding = New System.Windows.Forms.Padding(0)
        Me.FraDetail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraDetail.Size = New System.Drawing.Size(279, 57)
        Me.FraDetail.TabIndex = 76
        Me.FraDetail.TabStop = False
        Me.FraDetail.Text = "Other Detail"
        '
        'lblModDate
        '
        Me.lblModDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblModDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblModDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblModDate.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblModDate.Location = New System.Drawing.Point(205, 34)
        Me.lblModDate.Name = "lblModDate"
        Me.lblModDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblModDate.Size = New System.Drawing.Size(69, 19)
        Me.lblModDate.TabIndex = 88
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.BackColor = System.Drawing.SystemColors.Control
        Me.Label48.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label48.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label48.Location = New System.Drawing.Point(146, 36)
        Me.Label48.Name = "Label48"
        Me.Label48.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label48.Size = New System.Drawing.Size(63, 15)
        Me.Label48.TabIndex = 87
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
        Me.lblAddDate.Location = New System.Drawing.Point(205, 12)
        Me.lblAddDate.Name = "lblAddDate"
        Me.lblAddDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAddDate.Size = New System.Drawing.Size(69, 19)
        Me.lblAddDate.TabIndex = 86
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.BackColor = System.Drawing.SystemColors.Control
        Me.Label45.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label45.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label45.Location = New System.Drawing.Point(145, 14)
        Me.Label45.Name = "Label45"
        Me.Label45.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label45.Size = New System.Drawing.Size(63, 15)
        Me.Label45.TabIndex = 85
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
        Me.lblModUser.Location = New System.Drawing.Point(63, 34)
        Me.lblModUser.Name = "lblModUser"
        Me.lblModUser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblModUser.Size = New System.Drawing.Size(69, 19)
        Me.lblModUser.TabIndex = 84
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.BackColor = System.Drawing.SystemColors.Control
        Me.Label46.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label46.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label46.Location = New System.Drawing.Point(3, 36)
        Me.Label46.Name = "Label46"
        Me.Label46.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label46.Size = New System.Drawing.Size(61, 15)
        Me.Label46.TabIndex = 83
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
        Me.lblAddUser.Location = New System.Drawing.Point(63, 12)
        Me.lblAddUser.Name = "lblAddUser"
        Me.lblAddUser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAddUser.Size = New System.Drawing.Size(69, 19)
        Me.lblAddUser.TabIndex = 82
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.BackColor = System.Drawing.SystemColors.Control
        Me.Label44.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label44.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label44.Location = New System.Drawing.Point(4, 14)
        Me.Label44.Name = "Label44"
        Me.Label44.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label44.Size = New System.Drawing.Size(61, 15)
        Me.Label44.TabIndex = 81
        Me.Label44.Text = "Add User :"
        Me.Label44.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(0, 0)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 44
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.cmdClose)
        Me.Frame3.Controls.Add(Me.CmdView)
        Me.Frame3.Controls.Add(Me.CmdPreview)
        Me.Frame3.Controls.Add(Me.cmdPrint)
        Me.Frame3.Controls.Add(Me.cmdSavePrint)
        Me.Frame3.Controls.Add(Me.cmdDetail)
        Me.Frame3.Controls.Add(Me.cmdDelete)
        Me.Frame3.Controls.Add(Me.cmdSave)
        Me.Frame3.Controls.Add(Me.cmdModify)
        Me.Frame3.Controls.Add(Me.cmdAdd)
        Me.Frame3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 566)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(910, 54)
        Me.Frame3.TabIndex = 41
        Me.Frame3.TabStop = False
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
        Me.UltraGrid1.Location = New System.Drawing.Point(2, 3)
        Me.UltraGrid1.Name = "UltraGrid1"
        Me.UltraGrid1.Size = New System.Drawing.Size(905, 567)
        Me.UltraGrid1.TabIndex = 77
        '
        'FrmGateEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(910, 621)
        Me.Controls.Add(Me.FraDetail)
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
        Me.Name = "FrmGateEntry"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Gate Entry"
        Me.FraFront.ResumeLayout(False)
        Me.FraFront.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frasupp.ResumeLayout(False)
        Me.Frasupp.PerformLayout()
        Me.FraPO.ResumeLayout(False)
        Me.Frasprd.ResumeLayout(False)
        Me.Frasprd.PerformLayout()
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
        Me.FraDetail.ResumeLayout(False)
        Me.FraDetail.PerformLayout()
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
    Public WithEvents lblTotIGST As System.Windows.Forms.Label
    Public WithEvents Label21 As System.Windows.Forms.Label
    Public WithEvents cmdBillToSearch As Button
    Public WithEvents txtBillTo As TextBox
    Public WithEvents Label37 As Label
    Public WithEvents cmdShipToSearch As Button
    Public WithEvents TxtShipTo As TextBox
    Public WithEvents Label38 As Label
    Public WithEvents cmdInterUnitBill As Button
    Friend WithEvents UltraGrid1 As Infragistics.Win.UltraWinGrid.UltraGrid
    Public WithEvents txtOldERPNo As TextBox
    Public WithEvents txtOldERPDate As TextBox
    Public WithEvents Label18 As Label
    Public WithEvents Label20 As Label
    Public WithEvents txtDeliveryToLoc As TextBox
    Public WithEvents Label22 As Label
    Public WithEvents txtDeliveryTo As TextBox
    Public WithEvents Label24 As Label
    Public WithEvents cmdDeliveryToLocSearch As Button
    Public WithEvents cmdSearchDeliveryTo As Button
#End Region
End Class