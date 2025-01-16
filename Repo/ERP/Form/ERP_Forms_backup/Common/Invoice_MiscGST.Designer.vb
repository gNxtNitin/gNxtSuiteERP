Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmInvoice_MiscGST
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
        'Me.MDIParent = SalesGST.Master

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
    Public WithEvents txtHSNCode As System.Windows.Forms.TextBox
    Public WithEvents txtServProvided As System.Windows.Forms.TextBox
    Public WithEvents chkChallanMade As System.Windows.Forms.CheckBox
    Public WithEvents cboDivision As System.Windows.Forms.ComboBox
    Public WithEvents SprdPostingDetail As AxFPSpreadADO.AxfpSpread
    Public WithEvents FraPostingDtl As System.Windows.Forms.GroupBox
    Public WithEvents txtAuthDate As System.Windows.Forms.TextBox
    Public WithEvents txtAuth As System.Windows.Forms.TextBox
    Public WithEvents txtPONo As System.Windows.Forms.TextBox
    Public WithEvents txtPODate As System.Windows.Forms.TextBox
    Public WithEvents chkFOC As System.Windows.Forms.CheckBox
    Public WithEvents chkCancelled As System.Windows.Forms.CheckBox
    Public WithEvents txtTotItemValue As System.Windows.Forms.TextBox
    Public WithEvents txtRemarks As System.Windows.Forms.TextBox
    Public WithEvents chkStockTrf As System.Windows.Forms.CheckBox
    Public WithEvents SprdExp As AxFPSpreadADO.AxfpSpread
    Public WithEvents lblInvoiceSeq As System.Windows.Forms.Label
    Public WithEvents Label46 As System.Windows.Forms.Label
    Public WithEvents lblSGSTPer As System.Windows.Forms.Label
    Public WithEvents Label44 As System.Windows.Forms.Label
    Public WithEvents lblIGSTPer As System.Windows.Forms.Label
    Public WithEvents Label42 As System.Windows.Forms.Label
    Public WithEvents lblCGSTPer As System.Windows.Forms.Label
    Public WithEvents Label62 As System.Windows.Forms.Label
    Public WithEvents lblOtherExp As System.Windows.Forms.Label
    Public WithEvents lblSGSTAmount As System.Windows.Forms.Label
    Public WithEvents Label41 As System.Windows.Forms.Label
    Public WithEvents lblIGSTAmount As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label26 As System.Windows.Forms.Label
    Public WithEvents lblTCS As System.Windows.Forms.Label
    Public WithEvents lblTCSPercentage As System.Windows.Forms.Label
    Public WithEvents lblMSC As System.Windows.Forms.Label
    Public WithEvents lblRO As System.Windows.Forms.Label
    Public WithEvents lblSurcharge As System.Windows.Forms.Label
    Public WithEvents lblDiscount As System.Windows.Forms.Label
    Public WithEvents lblTotTaxableAmt As System.Windows.Forms.Label
    Public WithEvents lblSTPercentage As System.Windows.Forms.Label
    Public WithEvents lblTotExpAmt As System.Windows.Forms.Label
    Public WithEvents lblTotFreight As System.Windows.Forms.Label
    Public WithEvents lblTotCharges As System.Windows.Forms.Label
    Public WithEvents LblBookCode As System.Windows.Forms.Label
    Public WithEvents LblMKey As System.Windows.Forms.Label
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents lblCGSTAmount As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents lblNetAmount As System.Windows.Forms.Label
    Public WithEvents lblTotQty As System.Windows.Forms.Label
    Public WithEvents lblTotPackQtyCap As System.Windows.Forms.Label
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents _TabMain_TabPage0 As System.Windows.Forms.TabPage
    Public WithEvents cmpPrinteInvoice As System.Windows.Forms.Button
    Public WithEvents txtIRNNo As System.Windows.Forms.TextBox
    Public WithEvents txteInvAckNo As System.Windows.Forms.TextBox
    Public WithEvents txteInvAckDate As System.Windows.Forms.TextBox
    Public WithEvents cmdeInvoice As System.Windows.Forms.Button
    Public WithEvents cmdQRCode As System.Windows.Forms.Button
    Public WithEvents Label51 As System.Windows.Forms.Label
    Public WithEvents Label54 As System.Windows.Forms.Label
    Public WithEvents Label58 As System.Windows.Forms.Label
    Public WithEvents Frame14 As System.Windows.Forms.GroupBox
    Public WithEvents _OptFreight_0 As System.Windows.Forms.RadioButton
    Public WithEvents _OptFreight_1 As System.Windows.Forms.RadioButton
    Public WithEvents Frame8 As System.Windows.Forms.GroupBox
    Public WithEvents _txtCreditDays_0 As System.Windows.Forms.TextBox
    Public WithEvents _txtCreditDays_1 As System.Windows.Forms.TextBox
    Public WithEvents Label33 As System.Windows.Forms.Label
    Public WithEvents Label35 As System.Windows.Forms.Label
    Public WithEvents Frame7 As System.Windows.Forms.GroupBox
    Public WithEvents txtTariff As System.Windows.Forms.TextBox
    Public WithEvents chkPackmat As System.Windows.Forms.CheckBox
    Public WithEvents txtNarration As System.Windows.Forms.TextBox
    Public WithEvents txtDocsThru As System.Windows.Forms.TextBox
    Public WithEvents txtCarriers As System.Windows.Forms.TextBox
    Public WithEvents txtVehicle As System.Windows.Forms.TextBox
    Public WithEvents txtMode As System.Windows.Forms.TextBox
    Public WithEvents txtItemType As System.Windows.Forms.TextBox
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label32 As System.Windows.Forms.Label
    Public WithEvents Label31 As System.Windows.Forms.Label
    Public WithEvents Label30 As System.Windows.Forms.Label
    Public WithEvents Label29 As System.Windows.Forms.Label
    Public WithEvents Label27 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents _TabMain_TabPage1 As System.Windows.Forms.TabPage
    Public WithEvents TabMain As System.Windows.Forms.TabControl
    Public WithEvents txtCreditAccount As System.Windows.Forms.TextBox
    Public WithEvents txtCustomer As System.Windows.Forms.TextBox
    Public WithEvents txtBillDate As System.Windows.Forms.TextBox
    Public WithEvents TxtBillTm As System.Windows.Forms.TextBox
    Public WithEvents cboInvType As System.Windows.Forms.ComboBox
    Public WithEvents txtBillNoPrefix As System.Windows.Forms.TextBox
    Public WithEvents txtBillNoSuffix As System.Windows.Forms.TextBox
    Public WithEvents txtBillNo As System.Windows.Forms.TextBox
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label53 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents lblInvHeading As System.Windows.Forms.Label
    Public WithEvents Label39 As System.Windows.Forms.Label
    Public WithEvents Label38 As System.Windows.Forms.Label
    Public WithEvents Label36 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents lblCust As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents FraFront As System.Windows.Forms.GroupBox
    Public WithEvents AdoDCMain As VB6.ADODC
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdPostingHead As System.Windows.Forms.Button
    Public WithEvents cmdBarCode As System.Windows.Forms.Button
    Public WithEvents cmdDelete As System.Windows.Forms.Button
    Public WithEvents cmdSave As System.Windows.Forms.Button
    Public WithEvents cmdModify As System.Windows.Forms.Button
    Public WithEvents cmdAdd As System.Windows.Forms.Button
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents lblSODates As System.Windows.Forms.Label
    Public WithEvents lblSONos As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents OptFreight As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents txtCreditDays As Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmInvoice_MiscGST))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmpPrinteInvoice = New System.Windows.Forms.Button()
        Me.cmdeInvoice = New System.Windows.Forms.Button()
        Me.cmdQRCode = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdPostingHead = New System.Windows.Forms.Button()
        Me.cmdBarCode = New System.Windows.Forms.Button()
        Me.cmdDelete = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdModify = New System.Windows.Forms.Button()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.FraFront = New System.Windows.Forms.GroupBox()
        Me.txtDCNo = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtVendorCode = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.chkWoGST = New System.Windows.Forms.CheckBox()
        Me.txtBillTo = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtHSNCode = New System.Windows.Forms.TextBox()
        Me.txtServProvided = New System.Windows.Forms.TextBox()
        Me.chkChallanMade = New System.Windows.Forms.CheckBox()
        Me.cboDivision = New System.Windows.Forms.ComboBox()
        Me.FraPostingDtl = New System.Windows.Forms.GroupBox()
        Me.SprdPostingDetail = New AxFPSpreadADO.AxfpSpread()
        Me.txtAuthDate = New System.Windows.Forms.TextBox()
        Me.txtAuth = New System.Windows.Forms.TextBox()
        Me.txtPONo = New System.Windows.Forms.TextBox()
        Me.txtPODate = New System.Windows.Forms.TextBox()
        Me.chkFOC = New System.Windows.Forms.CheckBox()
        Me.chkCancelled = New System.Windows.Forms.CheckBox()
        Me.TabMain = New System.Windows.Forms.TabControl()
        Me._TabMain_TabPage0 = New System.Windows.Forms.TabPage()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.txtTotItemValue = New System.Windows.Forms.TextBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.chkStockTrf = New System.Windows.Forms.CheckBox()
        Me.SprdExp = New AxFPSpreadADO.AxfpSpread()
        Me.lblInvoiceSeq = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.lblSGSTPer = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.lblIGSTPer = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.lblCGSTPer = New System.Windows.Forms.Label()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.lblOtherExp = New System.Windows.Forms.Label()
        Me.lblSGSTAmount = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.lblIGSTAmount = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.lblTCS = New System.Windows.Forms.Label()
        Me.lblTCSPercentage = New System.Windows.Forms.Label()
        Me.lblMSC = New System.Windows.Forms.Label()
        Me.lblRO = New System.Windows.Forms.Label()
        Me.lblSurcharge = New System.Windows.Forms.Label()
        Me.lblDiscount = New System.Windows.Forms.Label()
        Me.lblTotTaxableAmt = New System.Windows.Forms.Label()
        Me.lblSTPercentage = New System.Windows.Forms.Label()
        Me.lblTotExpAmt = New System.Windows.Forms.Label()
        Me.lblTotFreight = New System.Windows.Forms.Label()
        Me.lblTotCharges = New System.Windows.Forms.Label()
        Me.LblBookCode = New System.Windows.Forms.Label()
        Me.LblMKey = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.lblCGSTAmount = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblNetAmount = New System.Windows.Forms.Label()
        Me.lblTotQty = New System.Windows.Forms.Label()
        Me.lblTotPackQtyCap = New System.Windows.Forms.Label()
        Me._TabMain_TabPage1 = New System.Windows.Forms.TabPage()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.Frame14 = New System.Windows.Forms.GroupBox()
        Me.txtIRNNo = New System.Windows.Forms.TextBox()
        Me.txteInvAckNo = New System.Windows.Forms.TextBox()
        Me.txteInvAckDate = New System.Windows.Forms.TextBox()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.Frame8 = New System.Windows.Forms.GroupBox()
        Me._OptFreight_0 = New System.Windows.Forms.RadioButton()
        Me._OptFreight_1 = New System.Windows.Forms.RadioButton()
        Me.Frame7 = New System.Windows.Forms.GroupBox()
        Me._txtCreditDays_0 = New System.Windows.Forms.TextBox()
        Me._txtCreditDays_1 = New System.Windows.Forms.TextBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.txtTariff = New System.Windows.Forms.TextBox()
        Me.chkPackmat = New System.Windows.Forms.CheckBox()
        Me.txtNarration = New System.Windows.Forms.TextBox()
        Me.txtDocsThru = New System.Windows.Forms.TextBox()
        Me.txtCarriers = New System.Windows.Forms.TextBox()
        Me.txtVehicle = New System.Windows.Forms.TextBox()
        Me.txtMode = New System.Windows.Forms.TextBox()
        Me.txtItemType = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtCreditAccount = New System.Windows.Forms.TextBox()
        Me.txtCustomer = New System.Windows.Forms.TextBox()
        Me.txtBillDate = New System.Windows.Forms.TextBox()
        Me.TxtBillTm = New System.Windows.Forms.TextBox()
        Me.cboInvType = New System.Windows.Forms.ComboBox()
        Me.txtBillNoPrefix = New System.Windows.Forms.TextBox()
        Me.txtBillNoSuffix = New System.Windows.Forms.TextBox()
        Me.txtBillNo = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.lblInvHeading = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblCust = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.AdoDCMain = New Microsoft.VisualBasic.Compatibility.VB6.ADODC()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.lblSODates = New System.Windows.Forms.Label()
        Me.lblSONos = New System.Windows.Forms.Label()
        Me.OptFreight = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.txtCreditDays = New Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray(Me.components)
        Me.txtDCDate = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.FraFront.SuspendLayout()
        Me.FraPostingDtl.SuspendLayout()
        CType(Me.SprdPostingDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabMain.SuspendLayout()
        Me._TabMain_TabPage0.SuspendLayout()
        Me.Frame6.SuspendLayout()
        CType(Me.SprdExp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._TabMain_TabPage1.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.Frame14.SuspendLayout()
        Me.Frame8.SuspendLayout()
        Me.Frame7.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptFreight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCreditDays, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmpPrinteInvoice
        '
        Me.cmpPrinteInvoice.BackColor = System.Drawing.SystemColors.Control
        Me.cmpPrinteInvoice.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmpPrinteInvoice.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmpPrinteInvoice.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmpPrinteInvoice.Image = CType(resources.GetObject("cmpPrinteInvoice.Image"), System.Drawing.Image)
        Me.cmpPrinteInvoice.Location = New System.Drawing.Point(12, 164)
        Me.cmpPrinteInvoice.Name = "cmpPrinteInvoice"
        Me.cmpPrinteInvoice.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmpPrinteInvoice.Size = New System.Drawing.Size(136, 29)
        Me.cmpPrinteInvoice.TabIndex = 122
        Me.cmpPrinteInvoice.Text = "Print e-Invoice"
        Me.cmpPrinteInvoice.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmpPrinteInvoice, "Delete")
        Me.cmpPrinteInvoice.UseVisualStyleBackColor = False
        '
        'cmdeInvoice
        '
        Me.cmdeInvoice.BackColor = System.Drawing.SystemColors.Control
        Me.cmdeInvoice.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdeInvoice.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdeInvoice.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdeInvoice.Location = New System.Drawing.Point(12, 128)
        Me.cmdeInvoice.Name = "cmdeInvoice"
        Me.cmdeInvoice.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdeInvoice.Size = New System.Drawing.Size(136, 29)
        Me.cmdeInvoice.TabIndex = 115
        Me.cmdeInvoice.Text = "Generate IRN && QR Code"
        Me.ToolTip1.SetToolTip(Me.cmdeInvoice, "Delete")
        Me.cmdeInvoice.UseVisualStyleBackColor = False
        '
        'cmdQRCode
        '
        Me.cmdQRCode.BackColor = System.Drawing.SystemColors.Control
        Me.cmdQRCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdQRCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdQRCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdQRCode.Image = CType(resources.GetObject("cmdQRCode.Image"), System.Drawing.Image)
        Me.cmdQRCode.Location = New System.Drawing.Point(198, 128)
        Me.cmdQRCode.Name = "cmdQRCode"
        Me.cmdQRCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdQRCode.Size = New System.Drawing.Size(100, 29)
        Me.cmdQRCode.TabIndex = 114
        Me.cmdQRCode.Text = "Generate QR Code"
        Me.cmdQRCode.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdQRCode, "Delete")
        Me.cmdQRCode.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.Location = New System.Drawing.Point(782, 10)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(60, 37)
        Me.cmdClose.TabIndex = 38
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdClose, "Close the form")
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'CmdView
        '
        Me.CmdView.BackColor = System.Drawing.SystemColors.Menu
        Me.CmdView.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdView.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
        Me.CmdView.Location = New System.Drawing.Point(722, 10)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(60, 37)
        Me.CmdView.TabIndex = 37
        Me.CmdView.Text = "List &View"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdView, "View Listing")
        Me.CmdView.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(662, 10)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(60, 37)
        Me.CmdPreview.TabIndex = 36
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Preview")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(601, 10)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(60, 37)
        Me.cmdPrint.TabIndex = 35
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'cmdPostingHead
        '
        Me.cmdPostingHead.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPostingHead.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPostingHead.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPostingHead.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPostingHead.Image = CType(resources.GetObject("cmdPostingHead.Image"), System.Drawing.Image)
        Me.cmdPostingHead.Location = New System.Drawing.Point(542, 10)
        Me.cmdPostingHead.Name = "cmdPostingHead"
        Me.cmdPostingHead.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPostingHead.Size = New System.Drawing.Size(60, 37)
        Me.cmdPostingHead.TabIndex = 96
        Me.cmdPostingHead.Text = "&Posting Detail"
        Me.cmdPostingHead.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPostingHead, "Delete")
        Me.cmdPostingHead.UseVisualStyleBackColor = False
        '
        'cmdBarCode
        '
        Me.cmdBarCode.BackColor = System.Drawing.SystemColors.Control
        Me.cmdBarCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdBarCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdBarCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdBarCode.Image = CType(resources.GetObject("cmdBarCode.Image"), System.Drawing.Image)
        Me.cmdBarCode.Location = New System.Drawing.Point(422, 10)
        Me.cmdBarCode.Name = "cmdBarCode"
        Me.cmdBarCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdBarCode.Size = New System.Drawing.Size(60, 37)
        Me.cmdBarCode.TabIndex = 33
        Me.cmdBarCode.Text = "&Barcode"
        Me.cmdBarCode.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdBarCode, "Delete")
        Me.cmdBarCode.UseVisualStyleBackColor = False
        '
        'cmdDelete
        '
        Me.cmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDelete.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDelete.Image = CType(resources.GetObject("cmdDelete.Image"), System.Drawing.Image)
        Me.cmdDelete.Location = New System.Drawing.Point(363, 10)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDelete.Size = New System.Drawing.Size(60, 37)
        Me.cmdDelete.TabIndex = 32
        Me.cmdDelete.Text = "&Delete"
        Me.cmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdDelete, "Delete")
        Me.cmdDelete.UseVisualStyleBackColor = False
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Image = CType(resources.GetObject("cmdSave.Image"), System.Drawing.Image)
        Me.cmdSave.Location = New System.Drawing.Point(304, 10)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(60, 37)
        Me.cmdSave.TabIndex = 31
        Me.cmdSave.Text = "&Save"
        Me.cmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSave, "Save Current Record")
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'cmdModify
        '
        Me.cmdModify.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdModify.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdModify.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdModify.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdModify.Image = CType(resources.GetObject("cmdModify.Image"), System.Drawing.Image)
        Me.cmdModify.Location = New System.Drawing.Point(245, 10)
        Me.cmdModify.Name = "cmdModify"
        Me.cmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdModify.Size = New System.Drawing.Size(60, 37)
        Me.cmdModify.TabIndex = 30
        Me.cmdModify.Text = "&Modify"
        Me.cmdModify.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdModify, "Modify ")
        Me.cmdModify.UseVisualStyleBackColor = False
        '
        'cmdAdd
        '
        Me.cmdAdd.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdAdd.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAdd.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAdd.Image = CType(resources.GetObject("cmdAdd.Image"), System.Drawing.Image)
        Me.cmdAdd.Location = New System.Drawing.Point(186, 10)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAdd.Size = New System.Drawing.Size(60, 37)
        Me.cmdAdd.TabIndex = 0
        Me.cmdAdd.Text = "&Add"
        Me.cmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdAdd, "Add New")
        Me.cmdAdd.UseVisualStyleBackColor = False
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.Location = New System.Drawing.Point(483, 10)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(60, 37)
        Me.cmdSavePrint.TabIndex = 34
        Me.cmdSavePrint.Text = "Paint- Print"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save & Print")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'FraFront
        '
        Me.FraFront.BackColor = System.Drawing.SystemColors.Control
        Me.FraFront.Controls.Add(Me.txtDCDate)
        Me.FraFront.Controls.Add(Me.Label10)
        Me.FraFront.Controls.Add(Me.txtDCNo)
        Me.FraFront.Controls.Add(Me.Label8)
        Me.FraFront.Controls.Add(Me.txtVendorCode)
        Me.FraFront.Controls.Add(Me.Label7)
        Me.FraFront.Controls.Add(Me.chkWoGST)
        Me.FraFront.Controls.Add(Me.txtBillTo)
        Me.FraFront.Controls.Add(Me.Label6)
        Me.FraFront.Controls.Add(Me.txtHSNCode)
        Me.FraFront.Controls.Add(Me.txtServProvided)
        Me.FraFront.Controls.Add(Me.chkChallanMade)
        Me.FraFront.Controls.Add(Me.cboDivision)
        Me.FraFront.Controls.Add(Me.FraPostingDtl)
        Me.FraFront.Controls.Add(Me.txtAuthDate)
        Me.FraFront.Controls.Add(Me.txtAuth)
        Me.FraFront.Controls.Add(Me.txtPONo)
        Me.FraFront.Controls.Add(Me.txtPODate)
        Me.FraFront.Controls.Add(Me.chkFOC)
        Me.FraFront.Controls.Add(Me.chkCancelled)
        Me.FraFront.Controls.Add(Me.TabMain)
        Me.FraFront.Controls.Add(Me.txtCreditAccount)
        Me.FraFront.Controls.Add(Me.txtCustomer)
        Me.FraFront.Controls.Add(Me.txtBillDate)
        Me.FraFront.Controls.Add(Me.TxtBillTm)
        Me.FraFront.Controls.Add(Me.cboInvType)
        Me.FraFront.Controls.Add(Me.txtBillNoPrefix)
        Me.FraFront.Controls.Add(Me.txtBillNoSuffix)
        Me.FraFront.Controls.Add(Me.txtBillNo)
        Me.FraFront.Controls.Add(Me.Label4)
        Me.FraFront.Controls.Add(Me.Label53)
        Me.FraFront.Controls.Add(Me.Label15)
        Me.FraFront.Controls.Add(Me.lblInvHeading)
        Me.FraFront.Controls.Add(Me.Label39)
        Me.FraFront.Controls.Add(Me.Label38)
        Me.FraFront.Controls.Add(Me.Label36)
        Me.FraFront.Controls.Add(Me.Label12)
        Me.FraFront.Controls.Add(Me.Label3)
        Me.FraFront.Controls.Add(Me.lblCust)
        Me.FraFront.Controls.Add(Me.Label5)
        Me.FraFront.Controls.Add(Me.Label2)
        Me.FraFront.Controls.Add(Me.Label1)
        Me.FraFront.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraFront.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraFront.Location = New System.Drawing.Point(0, -6)
        Me.FraFront.Name = "FraFront"
        Me.FraFront.Padding = New System.Windows.Forms.Padding(0)
        Me.FraFront.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraFront.Size = New System.Drawing.Size(1106, 573)
        Me.FraFront.TabIndex = 44
        Me.FraFront.TabStop = False
        '
        'txtDCNo
        '
        Me.txtDCNo.AcceptsReturn = True
        Me.txtDCNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtDCNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDCNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDCNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDCNo.ForeColor = System.Drawing.Color.Blue
        Me.txtDCNo.Location = New System.Drawing.Point(108, 186)
        Me.txtDCNo.MaxLength = 0
        Me.txtDCNo.Name = "txtDCNo"
        Me.txtDCNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDCNo.Size = New System.Drawing.Size(96, 20)
        Me.txtDCNo.TabIndex = 245
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(61, 188)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(43, 14)
        Me.Label8.TabIndex = 246
        Me.Label8.Text = "DC No :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtVendorCode
        '
        Me.txtVendorCode.AcceptsReturn = True
        Me.txtVendorCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtVendorCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVendorCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVendorCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVendorCode.ForeColor = System.Drawing.Color.Blue
        Me.txtVendorCode.Location = New System.Drawing.Point(108, 161)
        Me.txtVendorCode.MaxLength = 0
        Me.txtVendorCode.Name = "txtVendorCode"
        Me.txtVendorCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVendorCode.Size = New System.Drawing.Size(96, 20)
        Me.txtVendorCode.TabIndex = 243
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(28, 163)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(76, 14)
        Me.Label7.TabIndex = 244
        Me.Label7.Text = "Vendor Code :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'chkWoGST
        '
        Me.chkWoGST.AutoSize = True
        Me.chkWoGST.BackColor = System.Drawing.SystemColors.Control
        Me.chkWoGST.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkWoGST.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkWoGST.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkWoGST.Location = New System.Drawing.Point(320, 139)
        Me.chkWoGST.Name = "chkWoGST"
        Me.chkWoGST.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkWoGST.Size = New System.Drawing.Size(86, 18)
        Me.chkWoGST.TabIndex = 242
        Me.chkWoGST.Text = "Without GST"
        Me.chkWoGST.UseVisualStyleBackColor = False
        '
        'txtBillTo
        '
        Me.txtBillTo.AcceptsReturn = True
        Me.txtBillTo.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillTo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillTo.Enabled = False
        Me.txtBillTo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillTo.ForeColor = System.Drawing.Color.Blue
        Me.txtBillTo.Location = New System.Drawing.Point(644, 61)
        Me.txtBillTo.MaxLength = 0
        Me.txtBillTo.Name = "txtBillTo"
        Me.txtBillTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillTo.Size = New System.Drawing.Size(155, 22)
        Me.txtBillTo.TabIndex = 240
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Enabled = False
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(584, 63)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(56, 13)
        Me.Label6.TabIndex = 241
        Me.Label6.Text = "Location :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtHSNCode
        '
        Me.txtHSNCode.AcceptsReturn = True
        Me.txtHSNCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtHSNCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtHSNCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtHSNCode.Enabled = False
        Me.txtHSNCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHSNCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtHSNCode.Location = New System.Drawing.Point(108, 113)
        Me.txtHSNCode.MaxLength = 0
        Me.txtHSNCode.Name = "txtHSNCode"
        Me.txtHSNCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtHSNCode.Size = New System.Drawing.Size(97, 20)
        Me.txtHSNCode.TabIndex = 10
        '
        'txtServProvided
        '
        Me.txtServProvided.AcceptsReturn = True
        Me.txtServProvided.BackColor = System.Drawing.SystemColors.Window
        Me.txtServProvided.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtServProvided.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtServProvided.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtServProvided.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtServProvided.Location = New System.Drawing.Point(108, 62)
        Me.txtServProvided.MaxLength = 0
        Me.txtServProvided.Multiline = True
        Me.txtServProvided.Name = "txtServProvided"
        Me.txtServProvided.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtServProvided.Size = New System.Drawing.Size(288, 46)
        Me.txtServProvided.TabIndex = 8
        '
        'chkChallanMade
        '
        Me.chkChallanMade.AutoSize = True
        Me.chkChallanMade.BackColor = System.Drawing.SystemColors.Control
        Me.chkChallanMade.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkChallanMade.Enabled = False
        Me.chkChallanMade.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkChallanMade.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkChallanMade.Location = New System.Drawing.Point(288, 113)
        Me.chkChallanMade.Name = "chkChallanMade"
        Me.chkChallanMade.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkChallanMade.Size = New System.Drawing.Size(90, 18)
        Me.chkChallanMade.TabIndex = 109
        Me.chkChallanMade.Text = "Challan Made"
        Me.chkChallanMade.UseVisualStyleBackColor = False
        '
        'cboDivision
        '
        Me.cboDivision.BackColor = System.Drawing.SystemColors.Window
        Me.cboDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivision.Enabled = False
        Me.cboDivision.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDivision.Location = New System.Drawing.Point(108, 38)
        Me.cboDivision.Name = "cboDivision"
        Me.cboDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivision.Size = New System.Drawing.Size(190, 22)
        Me.cboDivision.TabIndex = 101
        '
        'FraPostingDtl
        '
        Me.FraPostingDtl.BackColor = System.Drawing.SystemColors.Control
        Me.FraPostingDtl.Controls.Add(Me.SprdPostingDetail)
        Me.FraPostingDtl.Enabled = False
        Me.FraPostingDtl.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraPostingDtl.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraPostingDtl.Location = New System.Drawing.Point(7, 369)
        Me.FraPostingDtl.Name = "FraPostingDtl"
        Me.FraPostingDtl.Padding = New System.Windows.Forms.Padding(0)
        Me.FraPostingDtl.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraPostingDtl.Size = New System.Drawing.Size(414, 196)
        Me.FraPostingDtl.TabIndex = 97
        Me.FraPostingDtl.TabStop = False
        Me.FraPostingDtl.Visible = False
        '
        'SprdPostingDetail
        '
        Me.SprdPostingDetail.DataSource = Nothing
        Me.SprdPostingDetail.Location = New System.Drawing.Point(2, 11)
        Me.SprdPostingDetail.Name = "SprdPostingDetail"
        Me.SprdPostingDetail.OcxState = CType(resources.GetObject("SprdPostingDetail.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdPostingDetail.Size = New System.Drawing.Size(408, 181)
        Me.SprdPostingDetail.TabIndex = 98
        '
        'txtAuthDate
        '
        Me.txtAuthDate.AcceptsReturn = True
        Me.txtAuthDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtAuthDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAuthDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAuthDate.Enabled = False
        Me.txtAuthDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAuthDate.ForeColor = System.Drawing.Color.Blue
        Me.txtAuthDate.Location = New System.Drawing.Point(828, 137)
        Me.txtAuthDate.MaxLength = 0
        Me.txtAuthDate.Name = "txtAuthDate"
        Me.txtAuthDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAuthDate.Size = New System.Drawing.Size(91, 20)
        Me.txtAuthDate.TabIndex = 12
        '
        'txtAuth
        '
        Me.txtAuth.AcceptsReturn = True
        Me.txtAuth.BackColor = System.Drawing.SystemColors.Window
        Me.txtAuth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAuth.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAuth.Enabled = False
        Me.txtAuth.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAuth.ForeColor = System.Drawing.Color.Blue
        Me.txtAuth.Location = New System.Drawing.Point(644, 137)
        Me.txtAuth.MaxLength = 0
        Me.txtAuth.Name = "txtAuth"
        Me.txtAuth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAuth.Size = New System.Drawing.Size(136, 20)
        Me.txtAuth.TabIndex = 11
        '
        'txtPONo
        '
        Me.txtPONo.AcceptsReturn = True
        Me.txtPONo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPONo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPONo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPONo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPONo.ForeColor = System.Drawing.Color.Blue
        Me.txtPONo.Location = New System.Drawing.Point(108, 137)
        Me.txtPONo.MaxLength = 0
        Me.txtPONo.Name = "txtPONo"
        Me.txtPONo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPONo.Size = New System.Drawing.Size(96, 20)
        Me.txtPONo.TabIndex = 13
        '
        'txtPODate
        '
        Me.txtPODate.AcceptsReturn = True
        Me.txtPODate.BackColor = System.Drawing.SystemColors.Window
        Me.txtPODate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPODate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPODate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPODate.ForeColor = System.Drawing.Color.Blue
        Me.txtPODate.Location = New System.Drawing.Point(243, 137)
        Me.txtPODate.MaxLength = 0
        Me.txtPODate.Name = "txtPODate"
        Me.txtPODate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPODate.Size = New System.Drawing.Size(73, 20)
        Me.txtPODate.TabIndex = 14
        '
        'chkFOC
        '
        Me.chkFOC.AutoSize = True
        Me.chkFOC.BackColor = System.Drawing.SystemColors.Control
        Me.chkFOC.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkFOC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFOC.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkFOC.Location = New System.Drawing.Point(337, 38)
        Me.chkFOC.Name = "chkFOC"
        Me.chkFOC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkFOC.Size = New System.Drawing.Size(55, 18)
        Me.chkFOC.TabIndex = 16
        Me.chkFOC.Text = "F.O.C."
        Me.chkFOC.UseVisualStyleBackColor = False
        Me.chkFOC.Visible = False
        '
        'chkCancelled
        '
        Me.chkCancelled.AutoSize = True
        Me.chkCancelled.BackColor = System.Drawing.SystemColors.Control
        Me.chkCancelled.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCancelled.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCancelled.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkCancelled.Location = New System.Drawing.Point(210, 113)
        Me.chkCancelled.Name = "chkCancelled"
        Me.chkCancelled.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCancelled.Size = New System.Drawing.Size(73, 18)
        Me.chkCancelled.TabIndex = 15
        Me.chkCancelled.Text = "Cancelled"
        Me.chkCancelled.UseVisualStyleBackColor = False
        '
        'TabMain
        '
        Me.TabMain.Controls.Add(Me._TabMain_TabPage0)
        Me.TabMain.Controls.Add(Me._TabMain_TabPage1)
        Me.TabMain.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabMain.ItemSize = New System.Drawing.Size(42, 18)
        Me.TabMain.Location = New System.Drawing.Point(1, 280)
        Me.TabMain.Name = "TabMain"
        Me.TabMain.SelectedIndex = 1
        Me.TabMain.Size = New System.Drawing.Size(1101, 289)
        Me.TabMain.TabIndex = 49
        '
        '_TabMain_TabPage0
        '
        Me._TabMain_TabPage0.Controls.Add(Me.Frame6)
        Me._TabMain_TabPage0.Location = New System.Drawing.Point(4, 22)
        Me._TabMain_TabPage0.Name = "_TabMain_TabPage0"
        Me._TabMain_TabPage0.Size = New System.Drawing.Size(1093, 263)
        Me._TabMain_TabPage0.TabIndex = 0
        Me._TabMain_TabPage0.Text = "Item Details"
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.txtTotItemValue)
        Me.Frame6.Controls.Add(Me.txtRemarks)
        Me.Frame6.Controls.Add(Me.chkStockTrf)
        Me.Frame6.Controls.Add(Me.SprdExp)
        Me.Frame6.Controls.Add(Me.lblInvoiceSeq)
        Me.Frame6.Controls.Add(Me.Label46)
        Me.Frame6.Controls.Add(Me.lblSGSTPer)
        Me.Frame6.Controls.Add(Me.Label44)
        Me.Frame6.Controls.Add(Me.lblIGSTPer)
        Me.Frame6.Controls.Add(Me.Label42)
        Me.Frame6.Controls.Add(Me.lblCGSTPer)
        Me.Frame6.Controls.Add(Me.Label62)
        Me.Frame6.Controls.Add(Me.lblOtherExp)
        Me.Frame6.Controls.Add(Me.lblSGSTAmount)
        Me.Frame6.Controls.Add(Me.Label41)
        Me.Frame6.Controls.Add(Me.lblIGSTAmount)
        Me.Frame6.Controls.Add(Me.Label14)
        Me.Frame6.Controls.Add(Me.Label26)
        Me.Frame6.Controls.Add(Me.lblTCS)
        Me.Frame6.Controls.Add(Me.lblTCSPercentage)
        Me.Frame6.Controls.Add(Me.lblMSC)
        Me.Frame6.Controls.Add(Me.lblRO)
        Me.Frame6.Controls.Add(Me.lblSurcharge)
        Me.Frame6.Controls.Add(Me.lblDiscount)
        Me.Frame6.Controls.Add(Me.lblTotTaxableAmt)
        Me.Frame6.Controls.Add(Me.lblSTPercentage)
        Me.Frame6.Controls.Add(Me.lblTotExpAmt)
        Me.Frame6.Controls.Add(Me.lblTotFreight)
        Me.Frame6.Controls.Add(Me.lblTotCharges)
        Me.Frame6.Controls.Add(Me.LblBookCode)
        Me.Frame6.Controls.Add(Me.LblMKey)
        Me.Frame6.Controls.Add(Me.Label17)
        Me.Frame6.Controls.Add(Me.lblCGSTAmount)
        Me.Frame6.Controls.Add(Me.Label16)
        Me.Frame6.Controls.Add(Me.Label13)
        Me.Frame6.Controls.Add(Me.lblNetAmount)
        Me.Frame6.Controls.Add(Me.lblTotQty)
        Me.Frame6.Controls.Add(Me.lblTotPackQtyCap)
        Me.Frame6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Frame6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(0, 0)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(1093, 263)
        Me.Frame6.TabIndex = 57
        Me.Frame6.TabStop = False
        '
        'txtTotItemValue
        '
        Me.txtTotItemValue.AcceptsReturn = True
        Me.txtTotItemValue.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotItemValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotItemValue.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotItemValue.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotItemValue.ForeColor = System.Drawing.Color.Blue
        Me.txtTotItemValue.Location = New System.Drawing.Point(620, 12)
        Me.txtTotItemValue.MaxLength = 0
        Me.txtTotItemValue.Name = "txtTotItemValue"
        Me.txtTotItemValue.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotItemValue.Size = New System.Drawing.Size(101, 20)
        Me.txtTotItemValue.TabIndex = 91
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRemarks.Location = New System.Drawing.Point(80, 12)
        Me.txtRemarks.MaxLength = 0
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.Size = New System.Drawing.Size(489, 53)
        Me.txtRemarks.TabIndex = 88
        '
        'chkStockTrf
        '
        Me.chkStockTrf.AutoSize = True
        Me.chkStockTrf.BackColor = System.Drawing.SystemColors.Control
        Me.chkStockTrf.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkStockTrf.Enabled = False
        Me.chkStockTrf.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkStockTrf.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkStockTrf.Location = New System.Drawing.Point(478, 214)
        Me.chkStockTrf.Name = "chkStockTrf"
        Me.chkStockTrf.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkStockTrf.Size = New System.Drawing.Size(67, 18)
        Me.chkStockTrf.TabIndex = 80
        Me.chkStockTrf.Text = "StockTrf"
        Me.chkStockTrf.UseVisualStyleBackColor = False
        Me.chkStockTrf.Visible = False
        '
        'SprdExp
        '
        Me.SprdExp.DataSource = Nothing
        Me.SprdExp.Location = New System.Drawing.Point(2, 66)
        Me.SprdExp.Name = "SprdExp"
        Me.SprdExp.OcxState = CType(resources.GetObject("SprdExp.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdExp.Size = New System.Drawing.Size(356, 190)
        Me.SprdExp.TabIndex = 17
        '
        'lblInvoiceSeq
        '
        Me.lblInvoiceSeq.AutoSize = True
        Me.lblInvoiceSeq.BackColor = System.Drawing.SystemColors.Control
        Me.lblInvoiceSeq.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblInvoiceSeq.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvoiceSeq.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInvoiceSeq.Location = New System.Drawing.Point(628, 250)
        Me.lblInvoiceSeq.Name = "lblInvoiceSeq"
        Me.lblInvoiceSeq.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblInvoiceSeq.Size = New System.Drawing.Size(70, 14)
        Me.lblInvoiceSeq.TabIndex = 112
        Me.lblInvoiceSeq.Text = "lblInvoiceSeq"
        '
        'Label46
        '
        Me.Label46.BackColor = System.Drawing.SystemColors.Control
        Me.Label46.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label46.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label46.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label46.Location = New System.Drawing.Point(602, 90)
        Me.Label46.Name = "Label46"
        Me.Label46.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label46.Size = New System.Drawing.Size(17, 19)
        Me.Label46.TabIndex = 108
        Me.Label46.Text = "%"
        Me.Label46.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblSGSTPer
        '
        Me.lblSGSTPer.BackColor = System.Drawing.SystemColors.Control
        Me.lblSGSTPer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSGSTPer.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSGSTPer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSGSTPer.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSGSTPer.Location = New System.Drawing.Point(564, 90)
        Me.lblSGSTPer.Name = "lblSGSTPer"
        Me.lblSGSTPer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSGSTPer.Size = New System.Drawing.Size(37, 19)
        Me.lblSGSTPer.TabIndex = 107
        Me.lblSGSTPer.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label44
        '
        Me.Label44.BackColor = System.Drawing.SystemColors.Control
        Me.Label44.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label44.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label44.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label44.Location = New System.Drawing.Point(602, 114)
        Me.Label44.Name = "Label44"
        Me.Label44.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label44.Size = New System.Drawing.Size(17, 19)
        Me.Label44.TabIndex = 106
        Me.Label44.Text = "%"
        Me.Label44.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblIGSTPer
        '
        Me.lblIGSTPer.BackColor = System.Drawing.SystemColors.Control
        Me.lblIGSTPer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblIGSTPer.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblIGSTPer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIGSTPer.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblIGSTPer.Location = New System.Drawing.Point(564, 114)
        Me.lblIGSTPer.Name = "lblIGSTPer"
        Me.lblIGSTPer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblIGSTPer.Size = New System.Drawing.Size(37, 19)
        Me.lblIGSTPer.TabIndex = 105
        Me.lblIGSTPer.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label42
        '
        Me.Label42.BackColor = System.Drawing.SystemColors.Control
        Me.Label42.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label42.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label42.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label42.Location = New System.Drawing.Point(602, 68)
        Me.Label42.Name = "Label42"
        Me.Label42.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label42.Size = New System.Drawing.Size(17, 19)
        Me.Label42.TabIndex = 104
        Me.Label42.Text = "%"
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblCGSTPer
        '
        Me.lblCGSTPer.BackColor = System.Drawing.SystemColors.Control
        Me.lblCGSTPer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCGSTPer.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCGSTPer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCGSTPer.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCGSTPer.Location = New System.Drawing.Point(564, 68)
        Me.lblCGSTPer.Name = "lblCGSTPer"
        Me.lblCGSTPer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCGSTPer.Size = New System.Drawing.Size(37, 19)
        Me.lblCGSTPer.TabIndex = 103
        Me.lblCGSTPer.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label62
        '
        Me.Label62.AutoSize = True
        Me.Label62.BackColor = System.Drawing.SystemColors.Control
        Me.Label62.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label62.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label62.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label62.Location = New System.Drawing.Point(518, 138)
        Me.Label62.Name = "Label62"
        Me.Label62.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label62.Size = New System.Drawing.Size(91, 14)
        Me.Label62.TabIndex = 100
        Me.Label62.Text = "Other Expenses :"
        Me.Label62.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblOtherExp
        '
        Me.lblOtherExp.BackColor = System.Drawing.SystemColors.Control
        Me.lblOtherExp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblOtherExp.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblOtherExp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOtherExp.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblOtherExp.Location = New System.Drawing.Point(620, 138)
        Me.lblOtherExp.Name = "lblOtherExp"
        Me.lblOtherExp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblOtherExp.Size = New System.Drawing.Size(99, 19)
        Me.lblOtherExp.TabIndex = 99
        Me.lblOtherExp.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblSGSTAmount
        '
        Me.lblSGSTAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblSGSTAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSGSTAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSGSTAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSGSTAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSGSTAmount.Location = New System.Drawing.Point(620, 90)
        Me.lblSGSTAmount.Name = "lblSGSTAmount"
        Me.lblSGSTAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSGSTAmount.Size = New System.Drawing.Size(99, 19)
        Me.lblSGSTAmount.TabIndex = 95
        Me.lblSGSTAmount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.BackColor = System.Drawing.SystemColors.Control
        Me.Label41.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label41.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label41.Location = New System.Drawing.Point(506, 94)
        Me.Label41.Name = "Label41"
        Me.Label41.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label41.Size = New System.Drawing.Size(52, 14)
        Me.Label41.TabIndex = 94
        Me.Label41.Text = "SGST :@"
        Me.Label41.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblIGSTAmount
        '
        Me.lblIGSTAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblIGSTAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblIGSTAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblIGSTAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIGSTAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblIGSTAmount.Location = New System.Drawing.Point(620, 114)
        Me.lblIGSTAmount.Name = "lblIGSTAmount"
        Me.lblIGSTAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblIGSTAmount.Size = New System.Drawing.Size(99, 19)
        Me.lblIGSTAmount.TabIndex = 93
        Me.lblIGSTAmount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label14.Location = New System.Drawing.Point(510, 118)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(47, 14)
        Me.Label14.TabIndex = 92
        Me.Label14.Text = "IGST :@"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.SystemColors.Control
        Me.Label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(4, 14)
        Me.Label26.Name = "Label26"
        Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label26.Size = New System.Drawing.Size(67, 14)
        Me.Label26.TabIndex = 89
        Me.Label26.Text = "Description :"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTCS
        '
        Me.lblTCS.BackColor = System.Drawing.SystemColors.Control
        Me.lblTCS.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTCS.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTCS.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTCS.Location = New System.Drawing.Point(412, 218)
        Me.lblTCS.Name = "lblTCS"
        Me.lblTCS.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTCS.Size = New System.Drawing.Size(43, 15)
        Me.lblTCS.TabIndex = 82
        Me.lblTCS.Text = "lblTCS"
        Me.lblTCS.Visible = False
        '
        'lblTCSPercentage
        '
        Me.lblTCSPercentage.BackColor = System.Drawing.SystemColors.Control
        Me.lblTCSPercentage.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTCSPercentage.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTCSPercentage.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTCSPercentage.Location = New System.Drawing.Point(410, 196)
        Me.lblTCSPercentage.Name = "lblTCSPercentage"
        Me.lblTCSPercentage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTCSPercentage.Size = New System.Drawing.Size(39, 13)
        Me.lblTCSPercentage.TabIndex = 81
        Me.lblTCSPercentage.Text = "lblTCSPercentage"
        Me.lblTCSPercentage.Visible = False
        '
        'lblMSC
        '
        Me.lblMSC.BackColor = System.Drawing.SystemColors.Control
        Me.lblMSC.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMSC.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMSC.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMSC.Location = New System.Drawing.Point(304, 240)
        Me.lblMSC.Name = "lblMSC"
        Me.lblMSC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMSC.Size = New System.Drawing.Size(49, 11)
        Me.lblMSC.TabIndex = 79
        Me.lblMSC.Text = "lblMSC"
        Me.lblMSC.Visible = False
        '
        'lblRO
        '
        Me.lblRO.BackColor = System.Drawing.SystemColors.Control
        Me.lblRO.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblRO.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRO.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRO.Location = New System.Drawing.Point(304, 228)
        Me.lblRO.Name = "lblRO"
        Me.lblRO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblRO.Size = New System.Drawing.Size(41, 11)
        Me.lblRO.TabIndex = 78
        Me.lblRO.Text = "lblRO"
        Me.lblRO.Visible = False
        '
        'lblSurcharge
        '
        Me.lblSurcharge.BackColor = System.Drawing.SystemColors.Control
        Me.lblSurcharge.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSurcharge.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSurcharge.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSurcharge.Location = New System.Drawing.Point(304, 212)
        Me.lblSurcharge.Name = "lblSurcharge"
        Me.lblSurcharge.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSurcharge.Size = New System.Drawing.Size(47, 11)
        Me.lblSurcharge.TabIndex = 77
        Me.lblSurcharge.Text = "lblSurcharge"
        Me.lblSurcharge.Visible = False
        '
        'lblDiscount
        '
        Me.lblDiscount.BackColor = System.Drawing.SystemColors.Control
        Me.lblDiscount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDiscount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDiscount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDiscount.Location = New System.Drawing.Point(304, 196)
        Me.lblDiscount.Name = "lblDiscount"
        Me.lblDiscount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDiscount.Size = New System.Drawing.Size(59, 11)
        Me.lblDiscount.TabIndex = 76
        Me.lblDiscount.Text = "lblDiscount"
        Me.lblDiscount.Visible = False
        '
        'lblTotTaxableAmt
        '
        Me.lblTotTaxableAmt.AutoSize = True
        Me.lblTotTaxableAmt.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotTaxableAmt.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotTaxableAmt.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotTaxableAmt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTotTaxableAmt.Location = New System.Drawing.Point(470, 236)
        Me.lblTotTaxableAmt.Name = "lblTotTaxableAmt"
        Me.lblTotTaxableAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotTaxableAmt.Size = New System.Drawing.Size(13, 14)
        Me.lblTotTaxableAmt.TabIndex = 75
        Me.lblTotTaxableAmt.Text = "0"
        Me.lblTotTaxableAmt.Visible = False
        '
        'lblSTPercentage
        '
        Me.lblSTPercentage.AutoSize = True
        Me.lblSTPercentage.BackColor = System.Drawing.SystemColors.Control
        Me.lblSTPercentage.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSTPercentage.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSTPercentage.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSTPercentage.Location = New System.Drawing.Point(384, 232)
        Me.lblSTPercentage.Name = "lblSTPercentage"
        Me.lblSTPercentage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSTPercentage.Size = New System.Drawing.Size(13, 14)
        Me.lblSTPercentage.TabIndex = 74
        Me.lblSTPercentage.Text = "0"
        Me.lblSTPercentage.Visible = False
        '
        'lblTotExpAmt
        '
        Me.lblTotExpAmt.AutoSize = True
        Me.lblTotExpAmt.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotExpAmt.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotExpAmt.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotExpAmt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTotExpAmt.Location = New System.Drawing.Point(380, 214)
        Me.lblTotExpAmt.Name = "lblTotExpAmt"
        Me.lblTotExpAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotExpAmt.Size = New System.Drawing.Size(13, 14)
        Me.lblTotExpAmt.TabIndex = 73
        Me.lblTotExpAmt.Text = "0"
        Me.lblTotExpAmt.Visible = False
        '
        'lblTotFreight
        '
        Me.lblTotFreight.AutoSize = True
        Me.lblTotFreight.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotFreight.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotFreight.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotFreight.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTotFreight.Location = New System.Drawing.Point(460, 196)
        Me.lblTotFreight.Name = "lblTotFreight"
        Me.lblTotFreight.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotFreight.Size = New System.Drawing.Size(13, 14)
        Me.lblTotFreight.TabIndex = 72
        Me.lblTotFreight.Text = "0"
        Me.lblTotFreight.Visible = False
        '
        'lblTotCharges
        '
        Me.lblTotCharges.AutoSize = True
        Me.lblTotCharges.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotCharges.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotCharges.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotCharges.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTotCharges.Location = New System.Drawing.Point(382, 196)
        Me.lblTotCharges.Name = "lblTotCharges"
        Me.lblTotCharges.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotCharges.Size = New System.Drawing.Size(13, 14)
        Me.lblTotCharges.TabIndex = 71
        Me.lblTotCharges.Text = "0"
        Me.lblTotCharges.Visible = False
        '
        'LblBookCode
        '
        Me.LblBookCode.AutoSize = True
        Me.LblBookCode.BackColor = System.Drawing.SystemColors.Control
        Me.LblBookCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblBookCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBookCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblBookCode.Location = New System.Drawing.Point(304, 164)
        Me.LblBookCode.Name = "LblBookCode"
        Me.LblBookCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblBookCode.Size = New System.Drawing.Size(70, 14)
        Me.LblBookCode.TabIndex = 66
        Me.LblBookCode.Text = "LblBookCode"
        Me.LblBookCode.Visible = False
        '
        'LblMKey
        '
        Me.LblMKey.AutoSize = True
        Me.LblMKey.BackColor = System.Drawing.SystemColors.Control
        Me.LblMKey.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblMKey.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMKey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblMKey.Location = New System.Drawing.Point(304, 180)
        Me.LblMKey.Name = "LblMKey"
        Me.LblMKey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblMKey.Size = New System.Drawing.Size(48, 14)
        Me.LblMKey.TabIndex = 65
        Me.LblMKey.Text = "LblMKey"
        Me.LblMKey.Visible = False
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label17.Location = New System.Drawing.Point(506, 70)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(52, 14)
        Me.Label17.TabIndex = 64
        Me.Label17.Text = "CGST :@"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCGSTAmount
        '
        Me.lblCGSTAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblCGSTAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCGSTAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCGSTAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCGSTAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCGSTAmount.Location = New System.Drawing.Point(620, 68)
        Me.lblCGSTAmount.Name = "lblCGSTAmount"
        Me.lblCGSTAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCGSTAmount.Size = New System.Drawing.Size(99, 19)
        Me.lblCGSTAmount.TabIndex = 63
        Me.lblCGSTAmount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label16.Location = New System.Drawing.Point(577, 14)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(40, 14)
        Me.Label16.TabIndex = 62
        Me.Label16.Text = "Value :"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label13.Location = New System.Drawing.Point(522, 164)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(68, 14)
        Me.Label13.TabIndex = 61
        Me.Label13.Text = "Net Amount :"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblNetAmount
        '
        Me.lblNetAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblNetAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNetAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblNetAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblNetAmount.Location = New System.Drawing.Point(620, 160)
        Me.lblNetAmount.Name = "lblNetAmount"
        Me.lblNetAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblNetAmount.Size = New System.Drawing.Size(99, 19)
        Me.lblNetAmount.TabIndex = 60
        Me.lblNetAmount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotQty
        '
        Me.lblTotQty.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotQty.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotQty.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotQty.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotQty.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotQty.Location = New System.Drawing.Point(416, 165)
        Me.lblTotQty.Name = "lblTotQty"
        Me.lblTotQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotQty.Size = New System.Drawing.Size(99, 17)
        Me.lblTotQty.TabIndex = 59
        Me.lblTotQty.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblTotQty.Visible = False
        '
        'lblTotPackQtyCap
        '
        Me.lblTotPackQtyCap.AutoSize = True
        Me.lblTotPackQtyCap.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotPackQtyCap.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotPackQtyCap.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotPackQtyCap.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotPackQtyCap.Location = New System.Drawing.Point(318, 166)
        Me.lblTotPackQtyCap.Name = "lblTotPackQtyCap"
        Me.lblTotPackQtyCap.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotPackQtyCap.Size = New System.Drawing.Size(55, 14)
        Me.lblTotPackQtyCap.TabIndex = 58
        Me.lblTotPackQtyCap.Text = "Total Qty :"
        Me.lblTotPackQtyCap.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblTotPackQtyCap.Visible = False
        '
        '_TabMain_TabPage1
        '
        Me._TabMain_TabPage1.Controls.Add(Me.Frame1)
        Me._TabMain_TabPage1.Location = New System.Drawing.Point(4, 22)
        Me._TabMain_TabPage1.Name = "_TabMain_TabPage1"
        Me._TabMain_TabPage1.Size = New System.Drawing.Size(1093, 263)
        Me._TabMain_TabPage1.TabIndex = 1
        Me._TabMain_TabPage1.Text = "Other Details"
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.Frame14)
        Me.Frame1.Controls.Add(Me.Frame8)
        Me.Frame1.Controls.Add(Me.Frame7)
        Me.Frame1.Controls.Add(Me.txtTariff)
        Me.Frame1.Controls.Add(Me.chkPackmat)
        Me.Frame1.Controls.Add(Me.txtNarration)
        Me.Frame1.Controls.Add(Me.txtDocsThru)
        Me.Frame1.Controls.Add(Me.txtCarriers)
        Me.Frame1.Controls.Add(Me.txtVehicle)
        Me.Frame1.Controls.Add(Me.txtMode)
        Me.Frame1.Controls.Add(Me.txtItemType)
        Me.Frame1.Controls.Add(Me.Label9)
        Me.Frame1.Controls.Add(Me.Label32)
        Me.Frame1.Controls.Add(Me.Label31)
        Me.Frame1.Controls.Add(Me.Label30)
        Me.Frame1.Controls.Add(Me.Label29)
        Me.Frame1.Controls.Add(Me.Label27)
        Me.Frame1.Controls.Add(Me.Label11)
        Me.Frame1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(1093, 263)
        Me.Frame1.TabIndex = 50
        Me.Frame1.TabStop = False
        '
        'Frame14
        '
        Me.Frame14.BackColor = System.Drawing.SystemColors.Control
        Me.Frame14.Controls.Add(Me.cmpPrinteInvoice)
        Me.Frame14.Controls.Add(Me.txtIRNNo)
        Me.Frame14.Controls.Add(Me.txteInvAckNo)
        Me.Frame14.Controls.Add(Me.txteInvAckDate)
        Me.Frame14.Controls.Add(Me.cmdeInvoice)
        Me.Frame14.Controls.Add(Me.cmdQRCode)
        Me.Frame14.Controls.Add(Me.Label51)
        Me.Frame14.Controls.Add(Me.Label54)
        Me.Frame14.Controls.Add(Me.Label58)
        Me.Frame14.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame14.Location = New System.Drawing.Point(436, 0)
        Me.Frame14.Name = "Frame14"
        Me.Frame14.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame14.Size = New System.Drawing.Size(303, 207)
        Me.Frame14.TabIndex = 113
        Me.Frame14.TabStop = False
        Me.Frame14.Text = "Generate e-Invoice"
        '
        'txtIRNNo
        '
        Me.txtIRNNo.AcceptsReturn = True
        Me.txtIRNNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtIRNNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIRNNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIRNNo.Enabled = False
        Me.txtIRNNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIRNNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIRNNo.Location = New System.Drawing.Point(69, 22)
        Me.txtIRNNo.MaxLength = 0
        Me.txtIRNNo.Multiline = True
        Me.txtIRNNo.Name = "txtIRNNo"
        Me.txtIRNNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIRNNo.Size = New System.Drawing.Size(229, 49)
        Me.txtIRNNo.TabIndex = 118
        '
        'txteInvAckNo
        '
        Me.txteInvAckNo.AcceptsReturn = True
        Me.txteInvAckNo.BackColor = System.Drawing.SystemColors.Window
        Me.txteInvAckNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txteInvAckNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txteInvAckNo.Enabled = False
        Me.txteInvAckNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txteInvAckNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txteInvAckNo.Location = New System.Drawing.Point(69, 74)
        Me.txteInvAckNo.MaxLength = 0
        Me.txteInvAckNo.Name = "txteInvAckNo"
        Me.txteInvAckNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txteInvAckNo.Size = New System.Drawing.Size(229, 20)
        Me.txteInvAckNo.TabIndex = 117
        '
        'txteInvAckDate
        '
        Me.txteInvAckDate.AcceptsReturn = True
        Me.txteInvAckDate.BackColor = System.Drawing.SystemColors.Window
        Me.txteInvAckDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txteInvAckDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txteInvAckDate.Enabled = False
        Me.txteInvAckDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txteInvAckDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txteInvAckDate.Location = New System.Drawing.Point(69, 96)
        Me.txteInvAckDate.MaxLength = 0
        Me.txteInvAckDate.Name = "txteInvAckDate"
        Me.txteInvAckDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txteInvAckDate.Size = New System.Drawing.Size(229, 20)
        Me.txteInvAckDate.TabIndex = 116
        '
        'Label51
        '
        Me.Label51.AutoSize = True
        Me.Label51.BackColor = System.Drawing.SystemColors.Control
        Me.Label51.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label51.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label51.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label51.Location = New System.Drawing.Point(35, 24)
        Me.Label51.Name = "Label51"
        Me.Label51.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label51.Size = New System.Drawing.Size(29, 14)
        Me.Label51.TabIndex = 121
        Me.Label51.Text = "IRN :"
        '
        'Label54
        '
        Me.Label54.AutoSize = True
        Me.Label54.BackColor = System.Drawing.SystemColors.Control
        Me.Label54.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label54.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label54.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label54.Location = New System.Drawing.Point(16, 78)
        Me.Label54.Name = "Label54"
        Me.Label54.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label54.Size = New System.Drawing.Size(48, 14)
        Me.Label54.TabIndex = 120
        Me.Label54.Text = "Ack No :"
        '
        'Label58
        '
        Me.Label58.AutoSize = True
        Me.Label58.BackColor = System.Drawing.SystemColors.Control
        Me.Label58.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label58.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label58.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label58.Location = New System.Drawing.Point(7, 100)
        Me.Label58.Name = "Label58"
        Me.Label58.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label58.Size = New System.Drawing.Size(57, 14)
        Me.Label58.TabIndex = 119
        Me.Label58.Text = "Ack Date :"
        '
        'Frame8
        '
        Me.Frame8.BackColor = System.Drawing.SystemColors.Control
        Me.Frame8.Controls.Add(Me._OptFreight_0)
        Me.Frame8.Controls.Add(Me._OptFreight_1)
        Me.Frame8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame8.Location = New System.Drawing.Point(28, 164)
        Me.Frame8.Name = "Frame8"
        Me.Frame8.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame8.Size = New System.Drawing.Size(175, 41)
        Me.Frame8.TabIndex = 87
        Me.Frame8.TabStop = False
        Me.Frame8.Text = "Freight"
        '
        '_OptFreight_0
        '
        Me._OptFreight_0.AutoSize = True
        Me._OptFreight_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptFreight_0.Checked = True
        Me._OptFreight_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptFreight_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptFreight_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptFreight.SetIndex(Me._OptFreight_0, CType(0, Short))
        Me._OptFreight_0.Location = New System.Drawing.Point(36, 18)
        Me._OptFreight_0.Name = "_OptFreight_0"
        Me._OptFreight_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptFreight_0.Size = New System.Drawing.Size(57, 18)
        Me._OptFreight_0.TabIndex = 25
        Me._OptFreight_0.TabStop = True
        Me._OptFreight_0.Text = "To Pay"
        Me._OptFreight_0.UseVisualStyleBackColor = False
        '
        '_OptFreight_1
        '
        Me._OptFreight_1.AutoSize = True
        Me._OptFreight_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptFreight_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptFreight_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptFreight_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptFreight.SetIndex(Me._OptFreight_1, CType(1, Short))
        Me._OptFreight_1.Location = New System.Drawing.Point(108, 18)
        Me._OptFreight_1.Name = "_OptFreight_1"
        Me._OptFreight_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptFreight_1.Size = New System.Drawing.Size(45, 18)
        Me._OptFreight_1.TabIndex = 26
        Me._OptFreight_1.TabStop = True
        Me._OptFreight_1.Text = "Paid"
        Me._OptFreight_1.UseVisualStyleBackColor = False
        '
        'Frame7
        '
        Me.Frame7.BackColor = System.Drawing.SystemColors.Control
        Me.Frame7.Controls.Add(Me._txtCreditDays_0)
        Me.Frame7.Controls.Add(Me._txtCreditDays_1)
        Me.Frame7.Controls.Add(Me.Label33)
        Me.Frame7.Controls.Add(Me.Label35)
        Me.Frame7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame7.Location = New System.Drawing.Point(204, 164)
        Me.Frame7.Name = "Frame7"
        Me.Frame7.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame7.Size = New System.Drawing.Size(175, 41)
        Me.Frame7.TabIndex = 84
        Me.Frame7.TabStop = False
        Me.Frame7.Text = "Credit Days"
        '
        '_txtCreditDays_0
        '
        Me._txtCreditDays_0.AcceptsReturn = True
        Me._txtCreditDays_0.BackColor = System.Drawing.SystemColors.Window
        Me._txtCreditDays_0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me._txtCreditDays_0.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtCreditDays_0.Enabled = False
        Me._txtCreditDays_0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtCreditDays_0.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCreditDays.SetIndex(Me._txtCreditDays_0, CType(0, Short))
        Me._txtCreditDays_0.Location = New System.Drawing.Point(52, 16)
        Me._txtCreditDays_0.MaxLength = 0
        Me._txtCreditDays_0.Name = "_txtCreditDays_0"
        Me._txtCreditDays_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtCreditDays_0.Size = New System.Drawing.Size(37, 20)
        Me._txtCreditDays_0.TabIndex = 27
        '
        '_txtCreditDays_1
        '
        Me._txtCreditDays_1.AcceptsReturn = True
        Me._txtCreditDays_1.BackColor = System.Drawing.SystemColors.Window
        Me._txtCreditDays_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me._txtCreditDays_1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtCreditDays_1.Enabled = False
        Me._txtCreditDays_1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtCreditDays_1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCreditDays.SetIndex(Me._txtCreditDays_1, CType(1, Short))
        Me._txtCreditDays_1.Location = New System.Drawing.Point(130, 16)
        Me._txtCreditDays_1.MaxLength = 0
        Me._txtCreditDays_1.Name = "_txtCreditDays_1"
        Me._txtCreditDays_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtCreditDays_1.Size = New System.Drawing.Size(37, 20)
        Me._txtCreditDays_1.TabIndex = 28
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.BackColor = System.Drawing.SystemColors.Control
        Me.Label33.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label33.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label33.Location = New System.Drawing.Point(14, 18)
        Me.Label33.Name = "Label33"
        Me.Label33.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label33.Size = New System.Drawing.Size(37, 14)
        Me.Label33.TabIndex = 86
        Me.Label33.Text = "From :"
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.BackColor = System.Drawing.SystemColors.Control
        Me.Label35.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label35.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label35.Location = New System.Drawing.Point(102, 18)
        Me.Label35.Name = "Label35"
        Me.Label35.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label35.Size = New System.Drawing.Size(24, 14)
        Me.Label35.TabIndex = 85
        Me.Label35.Text = "To :"
        '
        'txtTariff
        '
        Me.txtTariff.AcceptsReturn = True
        Me.txtTariff.BackColor = System.Drawing.SystemColors.Window
        Me.txtTariff.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTariff.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTariff.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTariff.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTariff.Location = New System.Drawing.Point(114, 12)
        Me.txtTariff.MaxLength = 0
        Me.txtTariff.Name = "txtTariff"
        Me.txtTariff.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTariff.Size = New System.Drawing.Size(267, 20)
        Me.txtTariff.TabIndex = 18
        '
        'chkPackmat
        '
        Me.chkPackmat.AutoSize = True
        Me.chkPackmat.BackColor = System.Drawing.SystemColors.Control
        Me.chkPackmat.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPackmat.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPackmat.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPackmat.Location = New System.Drawing.Point(116, 212)
        Me.chkPackmat.Name = "chkPackmat"
        Me.chkPackmat.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPackmat.Size = New System.Drawing.Size(103, 18)
        Me.chkPackmat.TabIndex = 29
        Me.chkPackmat.Text = "Packing Material"
        Me.chkPackmat.UseVisualStyleBackColor = False
        '
        'txtNarration
        '
        Me.txtNarration.AcceptsReturn = True
        Me.txtNarration.BackColor = System.Drawing.SystemColors.Window
        Me.txtNarration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNarration.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNarration.Enabled = False
        Me.txtNarration.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNarration.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNarration.Location = New System.Drawing.Point(114, 144)
        Me.txtNarration.MaxLength = 0
        Me.txtNarration.Name = "txtNarration"
        Me.txtNarration.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNarration.Size = New System.Drawing.Size(267, 20)
        Me.txtNarration.TabIndex = 24
        Me.txtNarration.Visible = False
        '
        'txtDocsThru
        '
        Me.txtDocsThru.AcceptsReturn = True
        Me.txtDocsThru.BackColor = System.Drawing.SystemColors.Window
        Me.txtDocsThru.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDocsThru.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDocsThru.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDocsThru.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDocsThru.Location = New System.Drawing.Point(114, 78)
        Me.txtDocsThru.MaxLength = 0
        Me.txtDocsThru.Name = "txtDocsThru"
        Me.txtDocsThru.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDocsThru.Size = New System.Drawing.Size(267, 20)
        Me.txtDocsThru.TabIndex = 21
        '
        'txtCarriers
        '
        Me.txtCarriers.AcceptsReturn = True
        Me.txtCarriers.BackColor = System.Drawing.SystemColors.Window
        Me.txtCarriers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCarriers.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCarriers.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCarriers.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCarriers.Location = New System.Drawing.Point(114, 122)
        Me.txtCarriers.MaxLength = 0
        Me.txtCarriers.Name = "txtCarriers"
        Me.txtCarriers.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCarriers.Size = New System.Drawing.Size(267, 20)
        Me.txtCarriers.TabIndex = 23
        '
        'txtVehicle
        '
        Me.txtVehicle.AcceptsReturn = True
        Me.txtVehicle.BackColor = System.Drawing.SystemColors.Window
        Me.txtVehicle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVehicle.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVehicle.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVehicle.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtVehicle.Location = New System.Drawing.Point(114, 100)
        Me.txtVehicle.MaxLength = 0
        Me.txtVehicle.Name = "txtVehicle"
        Me.txtVehicle.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVehicle.Size = New System.Drawing.Size(267, 20)
        Me.txtVehicle.TabIndex = 22
        '
        'txtMode
        '
        Me.txtMode.AcceptsReturn = True
        Me.txtMode.BackColor = System.Drawing.SystemColors.Window
        Me.txtMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMode.Location = New System.Drawing.Point(114, 56)
        Me.txtMode.MaxLength = 0
        Me.txtMode.Name = "txtMode"
        Me.txtMode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMode.Size = New System.Drawing.Size(267, 20)
        Me.txtMode.TabIndex = 20
        '
        'txtItemType
        '
        Me.txtItemType.AcceptsReturn = True
        Me.txtItemType.BackColor = System.Drawing.SystemColors.Window
        Me.txtItemType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemType.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItemType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtItemType.Location = New System.Drawing.Point(114, 34)
        Me.txtItemType.MaxLength = 0
        Me.txtItemType.Name = "txtItemType"
        Me.txtItemType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemType.Size = New System.Drawing.Size(267, 20)
        Me.txtItemType.TabIndex = 19
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(71, 14)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(38, 14)
        Me.Label9.TabIndex = 83
        Me.Label9.Text = "Tariff :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.BackColor = System.Drawing.SystemColors.Control
        Me.Label32.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label32.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label32.Location = New System.Drawing.Point(52, 146)
        Me.Label32.Name = "Label32"
        Me.Label32.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label32.Size = New System.Drawing.Size(57, 14)
        Me.Label32.TabIndex = 56
        Me.Label32.Text = "Narration :"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label32.Visible = False
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.BackColor = System.Drawing.SystemColors.Control
        Me.Label31.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label31.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label31.Location = New System.Drawing.Point(46, 80)
        Me.Label31.Name = "Label31"
        Me.Label31.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label31.Size = New System.Drawing.Size(63, 14)
        Me.Label31.TabIndex = 55
        Me.Label31.Text = "Docs Thru :"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.BackColor = System.Drawing.SystemColors.Control
        Me.Label30.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label30.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label30.Location = New System.Drawing.Point(57, 124)
        Me.Label30.Name = "Label30"
        Me.Label30.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label30.Size = New System.Drawing.Size(52, 14)
        Me.Label30.TabIndex = 54
        Me.Label30.Text = "Carriers :"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.SystemColors.Control
        Me.Label29.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label29.Location = New System.Drawing.Point(61, 102)
        Me.Label29.Name = "Label29"
        Me.Label29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label29.Size = New System.Drawing.Size(48, 14)
        Me.Label29.TabIndex = 53
        Me.Label29.Text = "Vehicle :"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.SystemColors.Control
        Me.Label27.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label27.Location = New System.Drawing.Point(70, 58)
        Me.Label27.Name = "Label27"
        Me.Label27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label27.Size = New System.Drawing.Size(39, 14)
        Me.Label27.TabIndex = 52
        Me.Label27.Text = "Mode :"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(51, 36)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(58, 14)
        Me.Label11.TabIndex = 51
        Me.Label11.Text = "Item Type :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtCreditAccount
        '
        Me.txtCreditAccount.AcceptsReturn = True
        Me.txtCreditAccount.BackColor = System.Drawing.SystemColors.Window
        Me.txtCreditAccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCreditAccount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCreditAccount.Enabled = False
        Me.txtCreditAccount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCreditAccount.ForeColor = System.Drawing.Color.Blue
        Me.txtCreditAccount.Location = New System.Drawing.Point(644, 113)
        Me.txtCreditAccount.MaxLength = 0
        Me.txtCreditAccount.Name = "txtCreditAccount"
        Me.txtCreditAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCreditAccount.Size = New System.Drawing.Size(274, 20)
        Me.txtCreditAccount.TabIndex = 9
        '
        'txtCustomer
        '
        Me.txtCustomer.AcceptsReturn = True
        Me.txtCustomer.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustomer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomer.ForeColor = System.Drawing.Color.Blue
        Me.txtCustomer.Location = New System.Drawing.Point(644, 38)
        Me.txtCustomer.MaxLength = 0
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustomer.Size = New System.Drawing.Size(274, 20)
        Me.txtCustomer.TabIndex = 7
        '
        'txtBillDate
        '
        Me.txtBillDate.AcceptsReturn = True
        Me.txtBillDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillDate.ForeColor = System.Drawing.Color.Blue
        Me.txtBillDate.Location = New System.Drawing.Point(290, 12)
        Me.txtBillDate.MaxLength = 0
        Me.txtBillDate.Name = "txtBillDate"
        Me.txtBillDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillDate.Size = New System.Drawing.Size(71, 20)
        Me.txtBillDate.TabIndex = 5
        '
        'TxtBillTm
        '
        Me.TxtBillTm.AcceptsReturn = True
        Me.TxtBillTm.BackColor = System.Drawing.SystemColors.Window
        Me.TxtBillTm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtBillTm.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtBillTm.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBillTm.ForeColor = System.Drawing.Color.Blue
        Me.TxtBillTm.Location = New System.Drawing.Point(363, 12)
        Me.TxtBillTm.MaxLength = 0
        Me.TxtBillTm.Name = "TxtBillTm"
        Me.TxtBillTm.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtBillTm.Size = New System.Drawing.Size(31, 20)
        Me.TxtBillTm.TabIndex = 6
        '
        'cboInvType
        '
        Me.cboInvType.BackColor = System.Drawing.SystemColors.Window
        Me.cboInvType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboInvType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboInvType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboInvType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboInvType.Location = New System.Drawing.Point(644, 12)
        Me.cboInvType.Name = "cboInvType"
        Me.cboInvType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboInvType.Size = New System.Drawing.Size(276, 22)
        Me.cboInvType.TabIndex = 1
        '
        'txtBillNoPrefix
        '
        Me.txtBillNoPrefix.AcceptsReturn = True
        Me.txtBillNoPrefix.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillNoPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillNoPrefix.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillNoPrefix.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillNoPrefix.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtBillNoPrefix.Location = New System.Drawing.Point(108, 12)
        Me.txtBillNoPrefix.MaxLength = 0
        Me.txtBillNoPrefix.Name = "txtBillNoPrefix"
        Me.txtBillNoPrefix.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillNoPrefix.Size = New System.Drawing.Size(60, 20)
        Me.txtBillNoPrefix.TabIndex = 2
        '
        'txtBillNoSuffix
        '
        Me.txtBillNoSuffix.AcceptsReturn = True
        Me.txtBillNoSuffix.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillNoSuffix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillNoSuffix.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillNoSuffix.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillNoSuffix.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtBillNoSuffix.Location = New System.Drawing.Point(301, 37)
        Me.txtBillNoSuffix.MaxLength = 0
        Me.txtBillNoSuffix.Name = "txtBillNoSuffix"
        Me.txtBillNoSuffix.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillNoSuffix.Size = New System.Drawing.Size(17, 20)
        Me.txtBillNoSuffix.TabIndex = 4
        Me.txtBillNoSuffix.Visible = False
        '
        'txtBillNo
        '
        Me.txtBillNo.AcceptsReturn = True
        Me.txtBillNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtBillNo.Location = New System.Drawing.Point(170, 12)
        Me.txtBillNo.MaxLength = 0
        Me.txtBillNo.Name = "txtBillNo"
        Me.txtBillNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillNo.Size = New System.Drawing.Size(79, 20)
        Me.txtBillNo.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(39, 117)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(65, 14)
        Me.Label4.TabIndex = 111
        Me.Label4.Text = "HSN / SAC :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.BackColor = System.Drawing.SystemColors.Control
        Me.Label53.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label53.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label53.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label53.Location = New System.Drawing.Point(9, 64)
        Me.Label53.Name = "Label53"
        Me.Label53.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label53.Size = New System.Drawing.Size(95, 14)
        Me.Label53.TabIndex = 110
        Me.Label53.Text = "Service Provided :"
        Me.Label53.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(54, 38)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(50, 14)
        Me.Label15.TabIndex = 102
        Me.Label15.Text = "Division :"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblInvHeading
        '
        Me.lblInvHeading.BackColor = System.Drawing.SystemColors.Control
        Me.lblInvHeading.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblInvHeading.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvHeading.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInvHeading.Location = New System.Drawing.Point(899, 64)
        Me.lblInvHeading.Name = "lblInvHeading"
        Me.lblInvHeading.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblInvHeading.Size = New System.Drawing.Size(59, 15)
        Me.lblInvHeading.TabIndex = 90
        Me.lblInvHeading.Text = "lblInvHeading"
        Me.lblInvHeading.Visible = False
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.BackColor = System.Drawing.SystemColors.Control
        Me.Label39.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label39.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label39.Location = New System.Drawing.Point(790, 139)
        Me.Label39.Name = "Label39"
        Me.Label39.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label39.Size = New System.Drawing.Size(35, 14)
        Me.Label39.TabIndex = 70
        Me.Label39.Text = "Date :"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.BackColor = System.Drawing.SystemColors.Control
        Me.Label38.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label38.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label38.Location = New System.Drawing.Point(581, 139)
        Me.Label38.Name = "Label38"
        Me.Label38.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label38.Size = New System.Drawing.Size(59, 14)
        Me.Label38.TabIndex = 69
        Me.Label38.Text = "Pre-Auth. :"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.BackColor = System.Drawing.SystemColors.Control
        Me.Label36.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label36.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label36.Location = New System.Drawing.Point(61, 139)
        Me.Label36.Name = "Label36"
        Me.Label36.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label36.Size = New System.Drawing.Size(43, 14)
        Me.Label36.TabIndex = 68
        Me.Label36.Text = "PO No :"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(208, 139)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(35, 14)
        Me.Label12.TabIndex = 67
        Me.Label12.Text = "Date :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(580, 117)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(60, 14)
        Me.Label3.TabIndex = 48
        Me.Label3.Text = "Credit A/c :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCust
        '
        Me.lblCust.AutoSize = True
        Me.lblCust.BackColor = System.Drawing.SystemColors.Control
        Me.lblCust.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCust.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCust.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCust.Location = New System.Drawing.Point(581, 42)
        Me.lblCust.Name = "lblCust"
        Me.lblCust.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCust.Size = New System.Drawing.Size(59, 14)
        Me.lblCust.TabIndex = 47
        Me.lblCust.Text = "Customer :"
        Me.lblCust.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(251, 16)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(35, 14)
        Me.Label5.TabIndex = 46
        Me.Label5.Text = "Date :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(567, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(73, 14)
        Me.Label2.TabIndex = 39
        Me.Label2.Text = "Invoice Type :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(-2, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(106, 13)
        Me.Label1.TabIndex = 45
        Me.Label1.Text = "Invoice No :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'AdoDCMain
        '
        Me.AdoDCMain.BackColor = System.Drawing.SystemColors.Window
        Me.AdoDCMain.CommandTimeout = 0
        Me.AdoDCMain.CommandType = ADODB.CommandTypeEnum.adCmdUnknown
        Me.AdoDCMain.ConnectionString = Nothing
        Me.AdoDCMain.CursorType = ADODB.CursorTypeEnum.adOpenStatic
        Me.AdoDCMain.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AdoDCMain.ForeColor = System.Drawing.SystemColors.WindowText
        Me.AdoDCMain.Location = New System.Drawing.Point(56, 270)
        Me.AdoDCMain.LockType = ADODB.LockTypeEnum.adLockOptimistic
        Me.AdoDCMain.Name = "AdoDCMain"
        Me.AdoDCMain.Size = New System.Drawing.Size(313, 33)
        Me.AdoDCMain.TabIndex = 45
        Me.AdoDCMain.Text = "Adodc1"
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(1106, 567)
        Me.SprdView.TabIndex = 41
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.cmdClose)
        Me.Frame3.Controls.Add(Me.CmdView)
        Me.Frame3.Controls.Add(Me.CmdPreview)
        Me.Frame3.Controls.Add(Me.cmdPrint)
        Me.Frame3.Controls.Add(Me.cmdPostingHead)
        Me.Frame3.Controls.Add(Me.cmdBarCode)
        Me.Frame3.Controls.Add(Me.cmdDelete)
        Me.Frame3.Controls.Add(Me.cmdSave)
        Me.Frame3.Controls.Add(Me.cmdModify)
        Me.Frame3.Controls.Add(Me.cmdAdd)
        Me.Frame3.Controls.Add(Me.cmdSavePrint)
        Me.Frame3.Controls.Add(Me.Report1)
        Me.Frame3.Controls.Add(Me.lblSODates)
        Me.Frame3.Controls.Add(Me.lblSONos)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 566)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(1106, 51)
        Me.Frame3.TabIndex = 40
        Me.Frame3.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(14, 12)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 97
        '
        'lblSODates
        '
        Me.lblSODates.BackColor = System.Drawing.SystemColors.Control
        Me.lblSODates.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSODates.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSODates.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSODates.Location = New System.Drawing.Point(596, 32)
        Me.lblSODates.Name = "lblSODates"
        Me.lblSODates.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSODates.Size = New System.Drawing.Size(17, 9)
        Me.lblSODates.TabIndex = 43
        Me.lblSODates.Text = "lblSODates"
        Me.lblSODates.Visible = False
        '
        'lblSONos
        '
        Me.lblSONos.BackColor = System.Drawing.SystemColors.Control
        Me.lblSONos.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSONos.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSONos.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSONos.Location = New System.Drawing.Point(590, 14)
        Me.lblSONos.Name = "lblSONos"
        Me.lblSONos.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSONos.Size = New System.Drawing.Size(23, 9)
        Me.lblSONos.TabIndex = 42
        Me.lblSONos.Text = "lblSONos"
        Me.lblSONos.Visible = False
        '
        'OptFreight
        '
        '
        'txtCreditDays
        '
        '
        'txtDCDate
        '
        Me.txtDCDate.AcceptsReturn = True
        Me.txtDCDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtDCDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDCDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDCDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDCDate.ForeColor = System.Drawing.Color.Blue
        Me.txtDCDate.Location = New System.Drawing.Point(245, 186)
        Me.txtDCDate.MaxLength = 0
        Me.txtDCDate.Name = "txtDCDate"
        Me.txtDCDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDCDate.Size = New System.Drawing.Size(73, 20)
        Me.txtDCDate.TabIndex = 247
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(210, 188)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(35, 14)
        Me.Label10.TabIndex = 248
        Me.Label10.Text = "Date :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'FrmInvoice_MiscGST
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1108, 621)
        Me.Controls.Add(Me.FraFront)
        Me.Controls.Add(Me.AdoDCMain)
        Me.Controls.Add(Me.SprdView)
        Me.Controls.Add(Me.Frame3)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmInvoice_MiscGST"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Service / Rental Invoice (GST)"
        Me.FraFront.ResumeLayout(False)
        Me.FraFront.PerformLayout()
        Me.FraPostingDtl.ResumeLayout(False)
        CType(Me.SprdPostingDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabMain.ResumeLayout(False)
        Me._TabMain_TabPage0.ResumeLayout(False)
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        CType(Me.SprdExp, System.ComponentModel.ISupportInitialize).EndInit()
        Me._TabMain_TabPage1.ResumeLayout(False)
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.Frame14.ResumeLayout(False)
        Me.Frame14.PerformLayout()
        Me.Frame8.ResumeLayout(False)
        Me.Frame8.PerformLayout()
        Me.Frame7.ResumeLayout(False)
        Me.Frame7.PerformLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame3.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptFreight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCreditDays, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        'SprdView.DataSource = CType(AdoDCMain, MSDATASRC.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        SprdView.DataSource = Nothing
    End Sub

    Public WithEvents txtBillTo As TextBox
    Public WithEvents Label6 As Label
    Public WithEvents chkWoGST As CheckBox
    Public WithEvents txtDCNo As TextBox
    Public WithEvents Label8 As Label
    Public WithEvents txtVendorCode As TextBox
    Public WithEvents Label7 As Label
    Public WithEvents txtDCDate As TextBox
    Public WithEvents Label10 As Label
#End Region
End Class