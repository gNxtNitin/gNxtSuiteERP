Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmCust_SaleGST
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
        'Me.MDIParent = AccountGST.Master
        'AccountGST.Master.Show
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
    Public WithEvents txtPartyDNNo As System.Windows.Forms.TextBox
    Public WithEvents txtPartyDNDate As System.Windows.Forms.TextBox
    Public WithEvents txtRecdDate As System.Windows.Forms.TextBox
    Public WithEvents cboReason As System.Windows.Forms.ComboBox
    Public WithEvents txtOBillDate As System.Windows.Forms.TextBox
    Public WithEvents txtOBillNo As System.Windows.Forms.TextBox
    Public WithEvents chkGSTApplicable As System.Windows.Forms.CheckBox
    Public WithEvents txtVNoPrefix As System.Windows.Forms.TextBox
    Public WithEvents cboDivision As System.Windows.Forms.ComboBox
    Public WithEvents cmdShowPO As System.Windows.Forms.Button
    Public WithEvents CmdSearchAmend As System.Windows.Forms.Button
    Public WithEvents txtPOAmendNo As System.Windows.Forms.TextBox
    Public WithEvents txtToDate As System.Windows.Forms.TextBox
    Public WithEvents cmdReCalculate As System.Windows.Forms.Button
    Public WithEvents SprdPostingDetail As AxFPSpreadADO.AxfpSpread
    Public WithEvents SprdPaymentDetail As AxFPSpreadADO.AxfpSpread
    Public WithEvents FraPostingDtl As System.Windows.Forms.GroupBox
    Public WithEvents CmdSearchPO As System.Windows.Forms.Button
    Public WithEvents txtWEFDate As System.Windows.Forms.TextBox
    Public WithEvents chkFinalPost As System.Windows.Forms.CheckBox
    Public WithEvents txtVNo As System.Windows.Forms.TextBox
    Public WithEvents txtVDate As System.Windows.Forms.TextBox
    Public WithEvents txtPONo As System.Windows.Forms.TextBox
    Public WithEvents txtPODate As System.Windows.Forms.TextBox
    Public WithEvents chkCancelled As System.Windows.Forms.CheckBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents txtSACCode As System.Windows.Forms.TextBox
    Public WithEvents txtTotItemValue As System.Windows.Forms.TextBox
    Public WithEvents txtRemarks1 As System.Windows.Forms.TextBox
    Public WithEvents Label24 As System.Windows.Forms.Label
    Public WithEvents Label23 As System.Windows.Forms.Label
    Public WithEvents Label22 As System.Windows.Forms.Label
    Public WithEvents fraService As System.Windows.Forms.GroupBox
    Public WithEvents sprdAcctPostDetail As AxFPSpreadADO.AxfpSpread
    Public WithEvents FraAcctPostDetail As System.Windows.Forms.GroupBox
    Public WithEvents SprdExp As AxFPSpreadADO.AxfpSpread
    Public WithEvents lblIGSTPer As System.Windows.Forms.Label
    Public WithEvents lblSGSTPer As System.Windows.Forms.Label
    Public WithEvents lblCGSTPer As System.Windows.Forms.Label
    Public WithEvents lblTotTaxableAmt As System.Windows.Forms.Label
    Public WithEvents lblTotCGST As System.Windows.Forms.Label
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents Label47 As System.Windows.Forms.Label
    Public WithEvents lblTotIGST As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents lblTotSGST As System.Windows.Forms.Label
    Public WithEvents lblOthersAmount As System.Windows.Forms.Label
    Public WithEvents Label29 As System.Windows.Forms.Label
    Public WithEvents lblCDPer As System.Windows.Forms.Label
    Public WithEvents lblTotExportExp As System.Windows.Forms.Label
    Public WithEvents lblMSC As System.Windows.Forms.Label
    Public WithEvents lblRO As System.Windows.Forms.Label
    Public WithEvents lblSurcharge As System.Windows.Forms.Label
    Public WithEvents lblDiscount As System.Windows.Forms.Label
    Public WithEvents lblTotExpAmt As System.Windows.Forms.Label
    Public WithEvents lblTotFreight As System.Windows.Forms.Label
    Public WithEvents lblTotCharges As System.Windows.Forms.Label
    Public WithEvents LblMKey As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents lblTotItemValue As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents lblNetAmount As System.Windows.Forms.Label
    Public WithEvents lblTotQty As System.Windows.Forms.Label
    Public WithEvents lblTotPackQtyCap As System.Windows.Forms.Label
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents _SSTab1_TabPage0 As System.Windows.Forms.TabPage
    Public WithEvents txtPaymentdate As System.Windows.Forms.TextBox
    Public WithEvents txtTariff As System.Windows.Forms.TextBox
    Public WithEvents txtNarration As System.Windows.Forms.TextBox
    Public WithEvents txtRemarks As System.Windows.Forms.TextBox
    Public WithEvents txtItemType As System.Windows.Forms.TextBox
    Public WithEvents lblGoodService As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label32 As System.Windows.Forms.Label
    Public WithEvents Label26 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents _SSTab1_TabPage1 As System.Windows.Forms.TabPage
    Public WithEvents SSTab1 As System.Windows.Forms.TabControl
    Public WithEvents txtDebitAccount As System.Windows.Forms.TextBox
    Public WithEvents txtSupplier As System.Windows.Forms.TextBox
    Public WithEvents txtBillDate As System.Windows.Forms.TextBox
    Public WithEvents cboInvType As System.Windows.Forms.ComboBox
    Public WithEvents txtBillNo As System.Windows.Forms.TextBox
    Public WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents Label20 As System.Windows.Forms.Label
    Public WithEvents Label21 As System.Windows.Forms.Label
    Public WithEvents lblReason As System.Windows.Forms.Label
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents LblBookCode As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label27 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents lblPurchaseVNo As System.Windows.Forms.Label
    Public WithEvents lblVNo As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents lblCust As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents lblInvType As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents FraFront As System.Windows.Forms.GroupBox
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents cmdPostingHead As System.Windows.Forms.Button
    Public WithEvents cmdShow As System.Windows.Forms.Button
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
    Public WithEvents cmdDelete As System.Windows.Forms.Button
    Public WithEvents cmdSave As System.Windows.Forms.Button
    Public WithEvents cmdModify As System.Windows.Forms.Button
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents cmdAdd As System.Windows.Forms.Button
    Public WithEvents lblBookType As System.Windows.Forms.Label
    Public WithEvents lblPMKey As System.Windows.Forms.Label
    Public WithEvents lblSODates As System.Windows.Forms.Label
    Public WithEvents lblSONos As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmCust_SaleGST))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdShowPO = New System.Windows.Forms.Button()
        Me.CmdSearchAmend = New System.Windows.Forms.Button()
        Me.CmdSearchPO = New System.Windows.Forms.Button()
        Me.cmdPostingHead = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.cmdDelete = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdModify = New System.Windows.Forms.Button()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.cmdBillToSearch = New System.Windows.Forms.Button()
        Me.cmpPrinteInvoice = New System.Windows.Forms.Button()
        Me.cmdeInvoice = New System.Windows.Forms.Button()
        Me.cmdQRCode = New System.Windows.Forms.Button()
        Me.FraFront = New System.Windows.Forms.GroupBox()
        Me.chkItemDetails = New System.Windows.Forms.CheckBox()
        Me.CmdPopFromFile = New System.Windows.Forms.Button()
        Me.txtBillTo = New System.Windows.Forms.TextBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.txtPartyDNNo = New System.Windows.Forms.TextBox()
        Me.txtPartyDNDate = New System.Windows.Forms.TextBox()
        Me.txtRecdDate = New System.Windows.Forms.TextBox()
        Me.cboReason = New System.Windows.Forms.ComboBox()
        Me.txtOBillDate = New System.Windows.Forms.TextBox()
        Me.txtOBillNo = New System.Windows.Forms.TextBox()
        Me.chkGSTApplicable = New System.Windows.Forms.CheckBox()
        Me.txtVNoPrefix = New System.Windows.Forms.TextBox()
        Me.cboDivision = New System.Windows.Forms.ComboBox()
        Me.txtPOAmendNo = New System.Windows.Forms.TextBox()
        Me.txtToDate = New System.Windows.Forms.TextBox()
        Me.txtWEFDate = New System.Windows.Forms.TextBox()
        Me.chkFinalPost = New System.Windows.Forms.CheckBox()
        Me.txtVNo = New System.Windows.Forms.TextBox()
        Me.txtVDate = New System.Windows.Forms.TextBox()
        Me.txtPONo = New System.Windows.Forms.TextBox()
        Me.txtPODate = New System.Windows.Forms.TextBox()
        Me.chkCancelled = New System.Windows.Forms.CheckBox()
        Me.SSTab1 = New System.Windows.Forms.TabControl()
        Me._SSTab1_TabPage0 = New System.Windows.Forms.TabPage()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.lblDiffAmt = New System.Windows.Forms.Label()
        Me.FraAcctPostDetail = New System.Windows.Forms.GroupBox()
        Me.sprdAcctPostDetail = New AxFPSpreadADO.AxfpSpread()
        Me.FraPostingDtl = New System.Windows.Forms.GroupBox()
        Me.cmdReCalculate = New System.Windows.Forms.Button()
        Me.SprdPostingDetail = New AxFPSpreadADO.AxfpSpread()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.SprdExp = New AxFPSpreadADO.AxfpSpread()
        Me.lblIGSTPer = New System.Windows.Forms.Label()
        Me.lblSGSTPer = New System.Windows.Forms.Label()
        Me.lblCGSTPer = New System.Windows.Forms.Label()
        Me.lblTotTaxableAmt = New System.Windows.Forms.Label()
        Me.lblTotCGST = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.lblTotIGST = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblTotSGST = New System.Windows.Forms.Label()
        Me.lblOthersAmount = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.lblCDPer = New System.Windows.Forms.Label()
        Me.lblTotExportExp = New System.Windows.Forms.Label()
        Me.lblMSC = New System.Windows.Forms.Label()
        Me.lblRO = New System.Windows.Forms.Label()
        Me.lblSurcharge = New System.Windows.Forms.Label()
        Me.lblDiscount = New System.Windows.Forms.Label()
        Me.lblTotExpAmt = New System.Windows.Forms.Label()
        Me.lblTotFreight = New System.Windows.Forms.Label()
        Me.lblTotCharges = New System.Windows.Forms.Label()
        Me.LblMKey = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblTotItemValue = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblNetAmount = New System.Windows.Forms.Label()
        Me.lblTotQty = New System.Windows.Forms.Label()
        Me.lblTotPackQtyCap = New System.Windows.Forms.Label()
        Me.fraService = New System.Windows.Forms.GroupBox()
        Me.txtSACCode = New System.Windows.Forms.TextBox()
        Me.txtTotItemValue = New System.Windows.Forms.TextBox()
        Me.txtRemarks1 = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me._SSTab1_TabPage1 = New System.Windows.Forms.TabPage()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.CmdPopPaymentFromFile = New System.Windows.Forms.Button()
        Me.lblPaymentDC = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.lblPaymentTotal = New System.Windows.Forms.Label()
        Me.CmdUpdatePayment = New System.Windows.Forms.Button()
        Me.fraPayment = New System.Windows.Forms.GroupBox()
        Me.SprdPaymentDetail = New AxFPSpreadADO.AxfpSpread()
        Me.Frame14 = New System.Windows.Forms.GroupBox()
        Me.txtIRNNo = New System.Windows.Forms.TextBox()
        Me.txteInvAckNo = New System.Windows.Forms.TextBox()
        Me.txteInvAckDate = New System.Windows.Forms.TextBox()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.txtPaymentdate = New System.Windows.Forms.TextBox()
        Me.txtTariff = New System.Windows.Forms.TextBox()
        Me.txtNarration = New System.Windows.Forms.TextBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtItemType = New System.Windows.Forms.TextBox()
        Me.lblGoodService = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtDebitAccount = New System.Windows.Forms.TextBox()
        Me.txtSupplier = New System.Windows.Forms.TextBox()
        Me.txtBillDate = New System.Windows.Forms.TextBox()
        Me.cboInvType = New System.Windows.Forms.ComboBox()
        Me.txtBillNo = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.lblReason = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.LblBookCode = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblPurchaseVNo = New System.Windows.Forms.Label()
        Me.lblVNo = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblCust = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblInvType = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.lblPMKey = New System.Windows.Forms.Label()
        Me.lblSODates = New System.Windows.Forms.Label()
        Me.lblSONos = New System.Windows.Forms.Label()
        Me.CommonDialogFont = New System.Windows.Forms.FontDialog()
        Me.CommonDialogColor = New System.Windows.Forms.ColorDialog()
        Me.CommonDialogPrint = New System.Windows.Forms.PrintDialog()
        Me.CommonDialogSave = New System.Windows.Forms.SaveFileDialog()
        Me.CommonDialogOpen = New System.Windows.Forms.OpenFileDialog()
        Me.FraFront.SuspendLayout()
        Me.SSTab1.SuspendLayout()
        Me._SSTab1_TabPage0.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me.FraAcctPostDetail.SuspendLayout()
        CType(Me.sprdAcctPostDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraPostingDtl.SuspendLayout()
        CType(Me.SprdPostingDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdExp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraService.SuspendLayout()
        Me._SSTab1_TabPage1.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.fraPayment.SuspendLayout()
        CType(Me.SprdPaymentDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame14.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdShowPO
        '
        Me.cmdShowPO.AutoSize = True
        Me.cmdShowPO.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdShowPO.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdShowPO.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdShowPO.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdShowPO.Location = New System.Drawing.Point(82, 158)
        Me.cmdShowPO.Name = "cmdShowPO"
        Me.cmdShowPO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShowPO.Size = New System.Drawing.Size(75, 24)
        Me.cmdShowPO.TabIndex = 14
        Me.cmdShowPO.TabStop = False
        Me.cmdShowPO.Text = "Populate"
        Me.cmdShowPO.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShowPO, "Show PO Detail")
        Me.cmdShowPO.UseVisualStyleBackColor = False
        '
        'CmdSearchAmend
        '
        Me.CmdSearchAmend.BackColor = System.Drawing.SystemColors.Menu
        Me.CmdSearchAmend.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSearchAmend.Enabled = False
        Me.CmdSearchAmend.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSearchAmend.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSearchAmend.Image = CType(resources.GetObject("CmdSearchAmend.Image"), System.Drawing.Image)
        Me.CmdSearchAmend.Location = New System.Drawing.Point(448, 60)
        Me.CmdSearchAmend.Name = "CmdSearchAmend"
        Me.CmdSearchAmend.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSearchAmend.Size = New System.Drawing.Size(23, 20)
        Me.CmdSearchAmend.TabIndex = 10
        Me.CmdSearchAmend.TabStop = False
        Me.CmdSearchAmend.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSearchAmend, "Seach Pending DC")
        Me.CmdSearchAmend.UseVisualStyleBackColor = False
        Me.CmdSearchAmend.Visible = False
        '
        'CmdSearchPO
        '
        Me.CmdSearchPO.BackColor = System.Drawing.SystemColors.Menu
        Me.CmdSearchPO.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSearchPO.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSearchPO.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSearchPO.Image = CType(resources.GetObject("CmdSearchPO.Image"), System.Drawing.Image)
        Me.CmdSearchPO.Location = New System.Drawing.Point(202, 60)
        Me.CmdSearchPO.Name = "CmdSearchPO"
        Me.CmdSearchPO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSearchPO.Size = New System.Drawing.Size(23, 19)
        Me.CmdSearchPO.TabIndex = 7
        Me.CmdSearchPO.TabStop = False
        Me.CmdSearchPO.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSearchPO, "Seach Pending DC")
        Me.CmdSearchPO.UseVisualStyleBackColor = False
        '
        'cmdPostingHead
        '
        Me.cmdPostingHead.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPostingHead.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPostingHead.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPostingHead.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPostingHead.Image = CType(resources.GetObject("cmdPostingHead.Image"), System.Drawing.Image)
        Me.cmdPostingHead.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdPostingHead.Location = New System.Drawing.Point(432, 12)
        Me.cmdPostingHead.Name = "cmdPostingHead"
        Me.cmdPostingHead.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPostingHead.Size = New System.Drawing.Size(75, 34)
        Me.cmdPostingHead.TabIndex = 97
        Me.cmdPostingHead.Text = "&Posting"
        Me.cmdPostingHead.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPostingHead, "Delete")
        Me.cmdPostingHead.UseVisualStyleBackColor = False
        '
        'cmdShow
        '
        Me.cmdShow.BackColor = System.Drawing.SystemColors.Control
        Me.cmdShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdShow.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdShow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdShow.Image = CType(resources.GetObject("cmdShow.Image"), System.Drawing.Image)
        Me.cmdShow.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdShow.Location = New System.Drawing.Point(358, 12)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShow.Size = New System.Drawing.Size(75, 34)
        Me.cmdShow.TabIndex = 77
        Me.cmdShow.Text = "S&ummary"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdClose.Location = New System.Drawing.Point(802, 12)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(75, 34)
        Me.cmdClose.TabIndex = 36
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
        Me.CmdView.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdView.Location = New System.Drawing.Point(728, 12)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(75, 34)
        Me.CmdView.TabIndex = 35
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
        Me.CmdPreview.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdPreview.Location = New System.Drawing.Point(654, 12)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(75, 34)
        Me.CmdPreview.TabIndex = 34
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
        Me.cmdPrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdPrint.Location = New System.Drawing.Point(580, 12)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(75, 34)
        Me.cmdPrint.TabIndex = 33
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdSavePrint.Location = New System.Drawing.Point(506, 12)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(75, 34)
        Me.cmdSavePrint.TabIndex = 32
        Me.cmdSavePrint.Text = "Save&&Print"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save & Print")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'cmdDelete
        '
        Me.cmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDelete.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDelete.Image = CType(resources.GetObject("cmdDelete.Image"), System.Drawing.Image)
        Me.cmdDelete.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdDelete.Location = New System.Drawing.Point(284, 12)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDelete.Size = New System.Drawing.Size(75, 34)
        Me.cmdDelete.TabIndex = 31
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
        Me.cmdSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdSave.Location = New System.Drawing.Point(210, 12)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(75, 34)
        Me.cmdSave.TabIndex = 30
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
        Me.cmdModify.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdModify.Location = New System.Drawing.Point(136, 12)
        Me.cmdModify.Name = "cmdModify"
        Me.cmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdModify.Size = New System.Drawing.Size(75, 34)
        Me.cmdModify.TabIndex = 29
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
        Me.cmdAdd.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdAdd.Location = New System.Drawing.Point(62, 12)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAdd.Size = New System.Drawing.Size(75, 34)
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
        Me.cmdBillToSearch.Enabled = False
        Me.cmdBillToSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdBillToSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdBillToSearch.Image = CType(resources.GetObject("cmdBillToSearch.Image"), System.Drawing.Image)
        Me.cmdBillToSearch.Location = New System.Drawing.Point(828, 84)
        Me.cmdBillToSearch.Name = "cmdBillToSearch"
        Me.cmdBillToSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdBillToSearch.Size = New System.Drawing.Size(28, 23)
        Me.cmdBillToSearch.TabIndex = 150
        Me.cmdBillToSearch.TabStop = False
        Me.cmdBillToSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdBillToSearch, "Search")
        Me.cmdBillToSearch.UseVisualStyleBackColor = False
        '
        'cmpPrinteInvoice
        '
        Me.cmpPrinteInvoice.BackColor = System.Drawing.SystemColors.Control
        Me.cmpPrinteInvoice.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmpPrinteInvoice.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmpPrinteInvoice.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmpPrinteInvoice.Image = CType(resources.GetObject("cmpPrinteInvoice.Image"), System.Drawing.Image)
        Me.cmpPrinteInvoice.Location = New System.Drawing.Point(319, 102)
        Me.cmpPrinteInvoice.Name = "cmpPrinteInvoice"
        Me.cmpPrinteInvoice.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmpPrinteInvoice.Size = New System.Drawing.Size(136, 27)
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
        Me.cmdeInvoice.Location = New System.Drawing.Point(12, 102)
        Me.cmdeInvoice.Name = "cmdeInvoice"
        Me.cmdeInvoice.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdeInvoice.Size = New System.Drawing.Size(136, 27)
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
        Me.cmdQRCode.Location = New System.Drawing.Point(198, 102)
        Me.cmdQRCode.Name = "cmdQRCode"
        Me.cmdQRCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdQRCode.Size = New System.Drawing.Size(100, 27)
        Me.cmdQRCode.TabIndex = 114
        Me.cmdQRCode.Text = "Generate QR Code"
        Me.cmdQRCode.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdQRCode, "Delete")
        Me.cmdQRCode.UseVisualStyleBackColor = False
        '
        'FraFront
        '
        Me.FraFront.BackColor = System.Drawing.SystemColors.Control
        Me.FraFront.Controls.Add(Me.chkItemDetails)
        Me.FraFront.Controls.Add(Me.CmdPopFromFile)
        Me.FraFront.Controls.Add(Me.cmdBillToSearch)
        Me.FraFront.Controls.Add(Me.txtBillTo)
        Me.FraFront.Controls.Add(Me.Label37)
        Me.FraFront.Controls.Add(Me.txtPartyDNNo)
        Me.FraFront.Controls.Add(Me.txtPartyDNDate)
        Me.FraFront.Controls.Add(Me.txtRecdDate)
        Me.FraFront.Controls.Add(Me.cboReason)
        Me.FraFront.Controls.Add(Me.txtOBillDate)
        Me.FraFront.Controls.Add(Me.txtOBillNo)
        Me.FraFront.Controls.Add(Me.chkGSTApplicable)
        Me.FraFront.Controls.Add(Me.txtVNoPrefix)
        Me.FraFront.Controls.Add(Me.cboDivision)
        Me.FraFront.Controls.Add(Me.cmdShowPO)
        Me.FraFront.Controls.Add(Me.CmdSearchAmend)
        Me.FraFront.Controls.Add(Me.txtPOAmendNo)
        Me.FraFront.Controls.Add(Me.txtToDate)
        Me.FraFront.Controls.Add(Me.CmdSearchPO)
        Me.FraFront.Controls.Add(Me.txtWEFDate)
        Me.FraFront.Controls.Add(Me.chkFinalPost)
        Me.FraFront.Controls.Add(Me.txtVNo)
        Me.FraFront.Controls.Add(Me.txtVDate)
        Me.FraFront.Controls.Add(Me.txtPONo)
        Me.FraFront.Controls.Add(Me.txtPODate)
        Me.FraFront.Controls.Add(Me.chkCancelled)
        Me.FraFront.Controls.Add(Me.SSTab1)
        Me.FraFront.Controls.Add(Me.txtDebitAccount)
        Me.FraFront.Controls.Add(Me.txtSupplier)
        Me.FraFront.Controls.Add(Me.txtBillDate)
        Me.FraFront.Controls.Add(Me.cboInvType)
        Me.FraFront.Controls.Add(Me.txtBillNo)
        Me.FraFront.Controls.Add(Me.Label19)
        Me.FraFront.Controls.Add(Me.Label20)
        Me.FraFront.Controls.Add(Me.Label21)
        Me.FraFront.Controls.Add(Me.lblReason)
        Me.FraFront.Controls.Add(Me.Label18)
        Me.FraFront.Controls.Add(Me.Label10)
        Me.FraFront.Controls.Add(Me.LblBookCode)
        Me.FraFront.Controls.Add(Me.Label14)
        Me.FraFront.Controls.Add(Me.Label8)
        Me.FraFront.Controls.Add(Me.Label27)
        Me.FraFront.Controls.Add(Me.Label15)
        Me.FraFront.Controls.Add(Me.Label4)
        Me.FraFront.Controls.Add(Me.lblPurchaseVNo)
        Me.FraFront.Controls.Add(Me.lblVNo)
        Me.FraFront.Controls.Add(Me.Label6)
        Me.FraFront.Controls.Add(Me.Label12)
        Me.FraFront.Controls.Add(Me.Label3)
        Me.FraFront.Controls.Add(Me.lblCust)
        Me.FraFront.Controls.Add(Me.Label5)
        Me.FraFront.Controls.Add(Me.lblInvType)
        Me.FraFront.Controls.Add(Me.Label1)
        Me.FraFront.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraFront.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraFront.Location = New System.Drawing.Point(1, -4)
        Me.FraFront.Name = "FraFront"
        Me.FraFront.Padding = New System.Windows.Forms.Padding(0)
        Me.FraFront.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraFront.Size = New System.Drawing.Size(907, 572)
        Me.FraFront.TabIndex = 42
        Me.FraFront.TabStop = False
        '
        'chkItemDetails
        '
        Me.chkItemDetails.AutoSize = True
        Me.chkItemDetails.BackColor = System.Drawing.SystemColors.Control
        Me.chkItemDetails.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkItemDetails.Enabled = False
        Me.chkItemDetails.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkItemDetails.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkItemDetails.Location = New System.Drawing.Point(415, 161)
        Me.chkItemDetails.Name = "chkItemDetails"
        Me.chkItemDetails.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkItemDetails.Size = New System.Drawing.Size(91, 18)
        Me.chkItemDetails.TabIndex = 152
        Me.chkItemDetails.Text = "Item Details"
        Me.chkItemDetails.UseVisualStyleBackColor = False
        '
        'CmdPopFromFile
        '
        Me.CmdPopFromFile.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPopFromFile.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPopFromFile.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPopFromFile.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPopFromFile.Location = New System.Drawing.Point(240, 158)
        Me.CmdPopFromFile.Name = "CmdPopFromFile"
        Me.CmdPopFromFile.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPopFromFile.Size = New System.Drawing.Size(134, 23)
        Me.CmdPopFromFile.TabIndex = 151
        Me.CmdPopFromFile.Text = "Populate From File"
        Me.CmdPopFromFile.UseVisualStyleBackColor = False
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
        Me.txtBillTo.Location = New System.Drawing.Point(596, 84)
        Me.txtBillTo.MaxLength = 0
        Me.txtBillTo.Name = "txtBillTo"
        Me.txtBillTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillTo.Size = New System.Drawing.Size(228, 22)
        Me.txtBillTo.TabIndex = 148
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.BackColor = System.Drawing.SystemColors.Control
        Me.Label37.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label37.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label37.Location = New System.Drawing.Point(535, 87)
        Me.Label37.Name = "Label37"
        Me.Label37.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label37.Size = New System.Drawing.Size(56, 13)
        Me.Label37.TabIndex = 149
        Me.Label37.Text = "Location :"
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtPartyDNNo
        '
        Me.txtPartyDNNo.AcceptsReturn = True
        Me.txtPartyDNNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPartyDNNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPartyDNNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPartyDNNo.Enabled = False
        Me.txtPartyDNNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPartyDNNo.ForeColor = System.Drawing.Color.Blue
        Me.txtPartyDNNo.Location = New System.Drawing.Point(596, 134)
        Me.txtPartyDNNo.MaxLength = 0
        Me.txtPartyDNNo.Name = "txtPartyDNNo"
        Me.txtPartyDNNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPartyDNNo.Size = New System.Drawing.Size(97, 20)
        Me.txtPartyDNNo.TabIndex = 106
        '
        'txtPartyDNDate
        '
        Me.txtPartyDNDate.AcceptsReturn = True
        Me.txtPartyDNDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtPartyDNDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPartyDNDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPartyDNDate.Enabled = False
        Me.txtPartyDNDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPartyDNDate.ForeColor = System.Drawing.Color.Blue
        Me.txtPartyDNDate.Location = New System.Drawing.Point(818, 134)
        Me.txtPartyDNDate.MaxLength = 0
        Me.txtPartyDNDate.Name = "txtPartyDNDate"
        Me.txtPartyDNDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPartyDNDate.Size = New System.Drawing.Size(85, 20)
        Me.txtPartyDNDate.TabIndex = 105
        '
        'txtRecdDate
        '
        Me.txtRecdDate.AcceptsReturn = True
        Me.txtRecdDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtRecdDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRecdDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRecdDate.Enabled = False
        Me.txtRecdDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRecdDate.ForeColor = System.Drawing.Color.Blue
        Me.txtRecdDate.Location = New System.Drawing.Point(596, 158)
        Me.txtRecdDate.MaxLength = 0
        Me.txtRecdDate.Name = "txtRecdDate"
        Me.txtRecdDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRecdDate.Size = New System.Drawing.Size(97, 20)
        Me.txtRecdDate.TabIndex = 104
        '
        'cboReason
        '
        Me.cboReason.BackColor = System.Drawing.SystemColors.Window
        Me.cboReason.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboReason.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboReason.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboReason.Location = New System.Drawing.Point(83, 108)
        Me.cboReason.Name = "cboReason"
        Me.cboReason.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboReason.Size = New System.Drawing.Size(261, 22)
        Me.cboReason.TabIndex = 11
        '
        'txtOBillDate
        '
        Me.txtOBillDate.AcceptsReturn = True
        Me.txtOBillDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtOBillDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOBillDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOBillDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOBillDate.ForeColor = System.Drawing.Color.Blue
        Me.txtOBillDate.Location = New System.Drawing.Point(278, 134)
        Me.txtOBillDate.MaxLength = 0
        Me.txtOBillDate.Name = "txtOBillDate"
        Me.txtOBillDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOBillDate.Size = New System.Drawing.Size(65, 20)
        Me.txtOBillDate.TabIndex = 13
        '
        'txtOBillNo
        '
        Me.txtOBillNo.AcceptsReturn = True
        Me.txtOBillNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtOBillNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOBillNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOBillNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOBillNo.ForeColor = System.Drawing.Color.Blue
        Me.txtOBillNo.Location = New System.Drawing.Point(83, 134)
        Me.txtOBillNo.MaxLength = 0
        Me.txtOBillNo.Name = "txtOBillNo"
        Me.txtOBillNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOBillNo.Size = New System.Drawing.Size(118, 20)
        Me.txtOBillNo.TabIndex = 12
        '
        'chkGSTApplicable
        '
        Me.chkGSTApplicable.AutoSize = True
        Me.chkGSTApplicable.BackColor = System.Drawing.SystemColors.Control
        Me.chkGSTApplicable.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkGSTApplicable.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGSTApplicable.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkGSTApplicable.Location = New System.Drawing.Point(416, 111)
        Me.chkGSTApplicable.Name = "chkGSTApplicable"
        Me.chkGSTApplicable.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkGSTApplicable.Size = New System.Drawing.Size(102, 18)
        Me.chkGSTApplicable.TabIndex = 19
        Me.chkGSTApplicable.Text = "GST (Yes / No)"
        Me.chkGSTApplicable.UseVisualStyleBackColor = False
        '
        'txtVNoPrefix
        '
        Me.txtVNoPrefix.AcceptsReturn = True
        Me.txtVNoPrefix.BackColor = System.Drawing.SystemColors.Window
        Me.txtVNoPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVNoPrefix.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVNoPrefix.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVNoPrefix.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtVNoPrefix.Location = New System.Drawing.Point(83, 12)
        Me.txtVNoPrefix.MaxLength = 0
        Me.txtVNoPrefix.Name = "txtVNoPrefix"
        Me.txtVNoPrefix.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVNoPrefix.Size = New System.Drawing.Size(81, 20)
        Me.txtVNoPrefix.TabIndex = 1
        '
        'cboDivision
        '
        Me.cboDivision.BackColor = System.Drawing.SystemColors.Window
        Me.cboDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivision.Enabled = False
        Me.cboDivision.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDivision.Location = New System.Drawing.Point(596, 36)
        Me.cboDivision.Name = "cboDivision"
        Me.cboDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivision.Size = New System.Drawing.Size(308, 22)
        Me.cboDivision.TabIndex = 3
        '
        'txtPOAmendNo
        '
        Me.txtPOAmendNo.AcceptsReturn = True
        Me.txtPOAmendNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPOAmendNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPOAmendNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPOAmendNo.Enabled = False
        Me.txtPOAmendNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPOAmendNo.ForeColor = System.Drawing.Color.Blue
        Me.txtPOAmendNo.Location = New System.Drawing.Point(415, 60)
        Me.txtPOAmendNo.MaxLength = 0
        Me.txtPOAmendNo.Name = "txtPOAmendNo"
        Me.txtPOAmendNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPOAmendNo.Size = New System.Drawing.Size(33, 20)
        Me.txtPOAmendNo.TabIndex = 9
        Me.txtPOAmendNo.Visible = False
        '
        'txtToDate
        '
        Me.txtToDate.AcceptsReturn = True
        Me.txtToDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtToDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtToDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToDate.ForeColor = System.Drawing.Color.Blue
        Me.txtToDate.Location = New System.Drawing.Point(279, 84)
        Me.txtToDate.MaxLength = 0
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtToDate.Size = New System.Drawing.Size(65, 20)
        Me.txtToDate.TabIndex = 10
        '
        'txtWEFDate
        '
        Me.txtWEFDate.AcceptsReturn = True
        Me.txtWEFDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtWEFDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWEFDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWEFDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWEFDate.ForeColor = System.Drawing.Color.Blue
        Me.txtWEFDate.Location = New System.Drawing.Point(83, 84)
        Me.txtWEFDate.MaxLength = 0
        Me.txtWEFDate.Name = "txtWEFDate"
        Me.txtWEFDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWEFDate.Size = New System.Drawing.Size(118, 20)
        Me.txtWEFDate.TabIndex = 9
        '
        'chkFinalPost
        '
        Me.chkFinalPost.AutoSize = True
        Me.chkFinalPost.BackColor = System.Drawing.SystemColors.Control
        Me.chkFinalPost.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkFinalPost.Enabled = False
        Me.chkFinalPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFinalPost.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkFinalPost.Location = New System.Drawing.Point(416, 133)
        Me.chkFinalPost.Name = "chkFinalPost"
        Me.chkFinalPost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkFinalPost.Size = New System.Drawing.Size(76, 18)
        Me.chkFinalPost.TabIndex = 21
        Me.chkFinalPost.Text = "FinalPost"
        Me.chkFinalPost.UseVisualStyleBackColor = False
        '
        'txtVNo
        '
        Me.txtVNo.AcceptsReturn = True
        Me.txtVNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtVNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVNo.ForeColor = System.Drawing.Color.Blue
        Me.txtVNo.Location = New System.Drawing.Point(166, 12)
        Me.txtVNo.MaxLength = 0
        Me.txtVNo.Name = "txtVNo"
        Me.txtVNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVNo.Size = New System.Drawing.Size(87, 20)
        Me.txtVNo.TabIndex = 0
        '
        'txtVDate
        '
        Me.txtVDate.AcceptsReturn = True
        Me.txtVDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtVDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVDate.ForeColor = System.Drawing.Color.Blue
        Me.txtVDate.Location = New System.Drawing.Point(325, 12)
        Me.txtVDate.MaxLength = 0
        Me.txtVDate.Name = "txtVDate"
        Me.txtVDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVDate.Size = New System.Drawing.Size(65, 20)
        Me.txtVDate.TabIndex = 1
        '
        'txtPONo
        '
        Me.txtPONo.AcceptsReturn = True
        Me.txtPONo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPONo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPONo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPONo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPONo.ForeColor = System.Drawing.Color.Blue
        Me.txtPONo.Location = New System.Drawing.Point(83, 60)
        Me.txtPONo.MaxLength = 0
        Me.txtPONo.Name = "txtPONo"
        Me.txtPONo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPONo.Size = New System.Drawing.Size(118, 20)
        Me.txtPONo.TabIndex = 7
        '
        'txtPODate
        '
        Me.txtPODate.AcceptsReturn = True
        Me.txtPODate.BackColor = System.Drawing.SystemColors.Window
        Me.txtPODate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPODate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPODate.Enabled = False
        Me.txtPODate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPODate.ForeColor = System.Drawing.Color.Blue
        Me.txtPODate.Location = New System.Drawing.Point(279, 60)
        Me.txtPODate.MaxLength = 0
        Me.txtPODate.Name = "txtPODate"
        Me.txtPODate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPODate.Size = New System.Drawing.Size(65, 20)
        Me.txtPODate.TabIndex = 8
        '
        'chkCancelled
        '
        Me.chkCancelled.AutoSize = True
        Me.chkCancelled.BackColor = System.Drawing.SystemColors.Control
        Me.chkCancelled.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCancelled.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCancelled.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkCancelled.Location = New System.Drawing.Point(416, 87)
        Me.chkCancelled.Name = "chkCancelled"
        Me.chkCancelled.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCancelled.Size = New System.Drawing.Size(80, 18)
        Me.chkCancelled.TabIndex = 20
        Me.chkCancelled.Text = "Cancelled"
        Me.chkCancelled.UseVisualStyleBackColor = False
        '
        'SSTab1
        '
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage0)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage1)
        Me.SSTab1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SSTab1.ItemSize = New System.Drawing.Size(42, 18)
        Me.SSTab1.Location = New System.Drawing.Point(2, 189)
        Me.SSTab1.Name = "SSTab1"
        Me.SSTab1.SelectedIndex = 0
        Me.SSTab1.Size = New System.Drawing.Size(908, 382)
        Me.SSTab1.TabIndex = 47
        '
        '_SSTab1_TabPage0
        '
        Me._SSTab1_TabPage0.Controls.Add(Me.Frame6)
        Me._SSTab1_TabPage0.Location = New System.Drawing.Point(4, 22)
        Me._SSTab1_TabPage0.Name = "_SSTab1_TabPage0"
        Me._SSTab1_TabPage0.Size = New System.Drawing.Size(900, 356)
        Me._SSTab1_TabPage0.TabIndex = 0
        Me._SSTab1_TabPage0.Text = "Item Details"
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.lblDiffAmt)
        Me.Frame6.Controls.Add(Me.FraAcctPostDetail)
        Me.Frame6.Controls.Add(Me.FraPostingDtl)
        Me.Frame6.Controls.Add(Me.SprdMain)
        Me.Frame6.Controls.Add(Me.SprdExp)
        Me.Frame6.Controls.Add(Me.lblIGSTPer)
        Me.Frame6.Controls.Add(Me.lblSGSTPer)
        Me.Frame6.Controls.Add(Me.lblCGSTPer)
        Me.Frame6.Controls.Add(Me.lblTotTaxableAmt)
        Me.Frame6.Controls.Add(Me.lblTotCGST)
        Me.Frame6.Controls.Add(Me.Label17)
        Me.Frame6.Controls.Add(Me.Label47)
        Me.Frame6.Controls.Add(Me.lblTotIGST)
        Me.Frame6.Controls.Add(Me.Label2)
        Me.Frame6.Controls.Add(Me.lblTotSGST)
        Me.Frame6.Controls.Add(Me.lblOthersAmount)
        Me.Frame6.Controls.Add(Me.Label29)
        Me.Frame6.Controls.Add(Me.lblCDPer)
        Me.Frame6.Controls.Add(Me.lblTotExportExp)
        Me.Frame6.Controls.Add(Me.lblMSC)
        Me.Frame6.Controls.Add(Me.lblRO)
        Me.Frame6.Controls.Add(Me.lblSurcharge)
        Me.Frame6.Controls.Add(Me.lblDiscount)
        Me.Frame6.Controls.Add(Me.lblTotExpAmt)
        Me.Frame6.Controls.Add(Me.lblTotFreight)
        Me.Frame6.Controls.Add(Me.lblTotCharges)
        Me.Frame6.Controls.Add(Me.LblMKey)
        Me.Frame6.Controls.Add(Me.Label16)
        Me.Frame6.Controls.Add(Me.lblTotItemValue)
        Me.Frame6.Controls.Add(Me.Label13)
        Me.Frame6.Controls.Add(Me.lblNetAmount)
        Me.Frame6.Controls.Add(Me.lblTotQty)
        Me.Frame6.Controls.Add(Me.lblTotPackQtyCap)
        Me.Frame6.Controls.Add(Me.fraService)
        Me.Frame6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(0, -3)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(896, 357)
        Me.Frame6.TabIndex = 52
        Me.Frame6.TabStop = False
        '
        'lblDiffAmt
        '
        Me.lblDiffAmt.AutoSize = True
        Me.lblDiffAmt.Location = New System.Drawing.Point(602, 258)
        Me.lblDiffAmt.Name = "lblDiffAmt"
        Me.lblDiffAmt.Size = New System.Drawing.Size(53, 14)
        Me.lblDiffAmt.TabIndex = 121
        Me.lblDiffAmt.Text = "lblDiffAmt"
        '
        'FraAcctPostDetail
        '
        Me.FraAcctPostDetail.BackColor = System.Drawing.SystemColors.Control
        Me.FraAcctPostDetail.Controls.Add(Me.sprdAcctPostDetail)
        Me.FraAcctPostDetail.Enabled = False
        Me.FraAcctPostDetail.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraAcctPostDetail.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraAcctPostDetail.Location = New System.Drawing.Point(2, 150)
        Me.FraAcctPostDetail.Name = "FraAcctPostDetail"
        Me.FraAcctPostDetail.Padding = New System.Windows.Forms.Padding(0)
        Me.FraAcctPostDetail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraAcctPostDetail.Size = New System.Drawing.Size(467, 187)
        Me.FraAcctPostDetail.TabIndex = 95
        Me.FraAcctPostDetail.TabStop = False
        Me.FraAcctPostDetail.Visible = False
        '
        'sprdAcctPostDetail
        '
        Me.sprdAcctPostDetail.DataSource = Nothing
        Me.sprdAcctPostDetail.Location = New System.Drawing.Point(2, 11)
        Me.sprdAcctPostDetail.Name = "sprdAcctPostDetail"
        Me.sprdAcctPostDetail.OcxState = CType(resources.GetObject("sprdAcctPostDetail.OcxState"), System.Windows.Forms.AxHost.State)
        Me.sprdAcctPostDetail.Size = New System.Drawing.Size(462, 175)
        Me.sprdAcctPostDetail.TabIndex = 96
        '
        'FraPostingDtl
        '
        Me.FraPostingDtl.BackColor = System.Drawing.SystemColors.Control
        Me.FraPostingDtl.Controls.Add(Me.cmdReCalculate)
        Me.FraPostingDtl.Controls.Add(Me.SprdPostingDetail)
        Me.FraPostingDtl.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraPostingDtl.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraPostingDtl.Location = New System.Drawing.Point(-1, 132)
        Me.FraPostingDtl.Name = "FraPostingDtl"
        Me.FraPostingDtl.Padding = New System.Windows.Forms.Padding(0)
        Me.FraPostingDtl.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraPostingDtl.Size = New System.Drawing.Size(457, 225)
        Me.FraPostingDtl.TabIndex = 78
        Me.FraPostingDtl.TabStop = False
        Me.FraPostingDtl.Visible = False
        '
        'cmdReCalculate
        '
        Me.cmdReCalculate.BackColor = System.Drawing.SystemColors.Control
        Me.cmdReCalculate.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdReCalculate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdReCalculate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdReCalculate.Location = New System.Drawing.Point(2, 205)
        Me.cmdReCalculate.Name = "cmdReCalculate"
        Me.cmdReCalculate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdReCalculate.Size = New System.Drawing.Size(91, 21)
        Me.cmdReCalculate.TabIndex = 82
        Me.cmdReCalculate.Text = "Re-Calculate"
        Me.cmdReCalculate.UseVisualStyleBackColor = False
        '
        'SprdPostingDetail
        '
        Me.SprdPostingDetail.DataSource = Nothing
        Me.SprdPostingDetail.Location = New System.Drawing.Point(4, 10)
        Me.SprdPostingDetail.Name = "SprdPostingDetail"
        Me.SprdPostingDetail.OcxState = CType(resources.GetObject("SprdPostingDetail.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdPostingDetail.Size = New System.Drawing.Size(449, 193)
        Me.SprdPostingDetail.TabIndex = 79
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(4, 50)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(890, 179)
        Me.SprdMain.TabIndex = 86
        '
        'SprdExp
        '
        Me.SprdExp.DataSource = Nothing
        Me.SprdExp.Location = New System.Drawing.Point(2, 232)
        Me.SprdExp.Name = "SprdExp"
        Me.SprdExp.OcxState = CType(resources.GetObject("SprdExp.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdExp.Size = New System.Drawing.Size(474, 122)
        Me.SprdExp.TabIndex = 23
        '
        'lblIGSTPer
        '
        Me.lblIGSTPer.BackColor = System.Drawing.SystemColors.Control
        Me.lblIGSTPer.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblIGSTPer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIGSTPer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblIGSTPer.Location = New System.Drawing.Point(546, 230)
        Me.lblIGSTPer.Name = "lblIGSTPer"
        Me.lblIGSTPer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblIGSTPer.Size = New System.Drawing.Size(29, 21)
        Me.lblIGSTPer.TabIndex = 120
        Me.lblIGSTPer.Text = "lblIGSTPer"
        Me.lblIGSTPer.Visible = False
        '
        'lblSGSTPer
        '
        Me.lblSGSTPer.BackColor = System.Drawing.SystemColors.Control
        Me.lblSGSTPer.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSGSTPer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSGSTPer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSGSTPer.Location = New System.Drawing.Point(536, 202)
        Me.lblSGSTPer.Name = "lblSGSTPer"
        Me.lblSGSTPer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSGSTPer.Size = New System.Drawing.Size(41, 17)
        Me.lblSGSTPer.TabIndex = 119
        Me.lblSGSTPer.Text = "lblSGSTPer"
        Me.lblSGSTPer.Visible = False
        '
        'lblCGSTPer
        '
        Me.lblCGSTPer.BackColor = System.Drawing.SystemColors.Control
        Me.lblCGSTPer.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCGSTPer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCGSTPer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCGSTPer.Location = New System.Drawing.Point(500, 178)
        Me.lblCGSTPer.Name = "lblCGSTPer"
        Me.lblCGSTPer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCGSTPer.Size = New System.Drawing.Size(49, 13)
        Me.lblCGSTPer.TabIndex = 118
        Me.lblCGSTPer.Text = "lblCGSTPer"
        Me.lblCGSTPer.Visible = False
        '
        'lblTotTaxableAmt
        '
        Me.lblTotTaxableAmt.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotTaxableAmt.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotTaxableAmt.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotTaxableAmt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTotTaxableAmt.Location = New System.Drawing.Point(474, 184)
        Me.lblTotTaxableAmt.Name = "lblTotTaxableAmt"
        Me.lblTotTaxableAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotTaxableAmt.Size = New System.Drawing.Size(45, 15)
        Me.lblTotTaxableAmt.TabIndex = 99
        Me.lblTotTaxableAmt.Text = "0"
        Me.lblTotTaxableAmt.Visible = False
        '
        'lblTotCGST
        '
        Me.lblTotCGST.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotCGST.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotCGST.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotCGST.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotCGST.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotCGST.Location = New System.Drawing.Point(808, 255)
        Me.lblTotCGST.Name = "lblTotCGST"
        Me.lblTotCGST.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotCGST.Size = New System.Drawing.Size(85, 20)
        Me.lblTotCGST.TabIndex = 94
        Me.lblTotCGST.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label17.Location = New System.Drawing.Point(760, 260)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(43, 14)
        Me.Label17.TabIndex = 93
        Me.Label17.Text = "CGST :"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.BackColor = System.Drawing.SystemColors.Control
        Me.Label47.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label47.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label47.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label47.Location = New System.Drawing.Point(762, 299)
        Me.Label47.Name = "Label47"
        Me.Label47.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label47.Size = New System.Drawing.Size(41, 14)
        Me.Label47.TabIndex = 92
        Me.Label47.Text = "IGST : "
        Me.Label47.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotIGST
        '
        Me.lblTotIGST.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotIGST.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotIGST.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotIGST.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotIGST.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotIGST.Location = New System.Drawing.Point(808, 295)
        Me.lblTotIGST.Name = "lblTotIGST"
        Me.lblTotIGST.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotIGST.Size = New System.Drawing.Size(85, 20)
        Me.lblTotIGST.TabIndex = 91
        Me.lblTotIGST.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(761, 279)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(42, 14)
        Me.Label2.TabIndex = 90
        Me.Label2.Text = "SGST :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotSGST
        '
        Me.lblTotSGST.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotSGST.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotSGST.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotSGST.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotSGST.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotSGST.Location = New System.Drawing.Point(808, 275)
        Me.lblTotSGST.Name = "lblTotSGST"
        Me.lblTotSGST.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotSGST.Size = New System.Drawing.Size(85, 20)
        Me.lblTotSGST.TabIndex = 89
        Me.lblTotSGST.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblOthersAmount
        '
        Me.lblOthersAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblOthersAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblOthersAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblOthersAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOthersAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblOthersAmount.Location = New System.Drawing.Point(808, 315)
        Me.lblOthersAmount.Name = "lblOthersAmount"
        Me.lblOthersAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblOthersAmount.Size = New System.Drawing.Size(85, 20)
        Me.lblOthersAmount.TabIndex = 88
        Me.lblOthersAmount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.SystemColors.Control
        Me.Label29.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label29.Location = New System.Drawing.Point(749, 319)
        Me.Label29.Name = "Label29"
        Me.Label29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label29.Size = New System.Drawing.Size(54, 14)
        Me.Label29.TabIndex = 87
        Me.Label29.Text = "Others : "
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCDPer
        '
        Me.lblCDPer.BackColor = System.Drawing.SystemColors.Control
        Me.lblCDPer.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCDPer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCDPer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCDPer.Location = New System.Drawing.Point(482, 208)
        Me.lblCDPer.Name = "lblCDPer"
        Me.lblCDPer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCDPer.Size = New System.Drawing.Size(71, 21)
        Me.lblCDPer.TabIndex = 84
        Me.lblCDPer.Text = "lblCDPer"
        Me.lblCDPer.Visible = False
        '
        'lblTotExportExp
        '
        Me.lblTotExportExp.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotExportExp.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotExportExp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotExportExp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTotExportExp.Location = New System.Drawing.Point(496, 232)
        Me.lblTotExportExp.Name = "lblTotExportExp"
        Me.lblTotExportExp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotExportExp.Size = New System.Drawing.Size(39, 23)
        Me.lblTotExportExp.TabIndex = 83
        Me.lblTotExportExp.Text = "lblTotExportExp"
        Me.lblTotExportExp.Visible = False
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
        Me.lblMSC.TabIndex = 67
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
        Me.lblRO.TabIndex = 66
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
        Me.lblSurcharge.TabIndex = 65
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
        Me.lblDiscount.TabIndex = 64
        Me.lblDiscount.Text = "lblDiscount"
        Me.lblDiscount.Visible = False
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
        Me.lblTotExpAmt.TabIndex = 63
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
        Me.lblTotFreight.Location = New System.Drawing.Point(392, 196)
        Me.lblTotFreight.Name = "lblTotFreight"
        Me.lblTotFreight.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotFreight.Size = New System.Drawing.Size(13, 14)
        Me.lblTotFreight.TabIndex = 62
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
        Me.lblTotCharges.Location = New System.Drawing.Point(368, 196)
        Me.lblTotCharges.Name = "lblTotCharges"
        Me.lblTotCharges.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotCharges.Size = New System.Drawing.Size(13, 14)
        Me.lblTotCharges.TabIndex = 61
        Me.lblTotCharges.Text = "0"
        Me.lblTotCharges.Visible = False
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
        Me.LblMKey.TabIndex = 59
        Me.LblMKey.Text = "LblMKey"
        Me.LblMKey.Visible = False
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label16.Location = New System.Drawing.Point(732, 237)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(71, 14)
        Me.Label16.TabIndex = 58
        Me.Label16.Text = "Item Value :"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotItemValue
        '
        Me.lblTotItemValue.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotItemValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotItemValue.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotItemValue.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotItemValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotItemValue.Location = New System.Drawing.Point(808, 236)
        Me.lblTotItemValue.Name = "lblTotItemValue"
        Me.lblTotItemValue.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotItemValue.Size = New System.Drawing.Size(85, 17)
        Me.lblTotItemValue.TabIndex = 57
        Me.lblTotItemValue.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label13.Location = New System.Drawing.Point(725, 339)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(81, 14)
        Me.Label13.TabIndex = 56
        Me.Label13.Text = "Net Amount : "
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblNetAmount
        '
        Me.lblNetAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblNetAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNetAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblNetAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblNetAmount.Location = New System.Drawing.Point(808, 335)
        Me.lblNetAmount.Name = "lblNetAmount"
        Me.lblNetAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblNetAmount.Size = New System.Drawing.Size(85, 19)
        Me.lblNetAmount.TabIndex = 55
        Me.lblNetAmount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotQty
        '
        Me.lblTotQty.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotQty.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotQty.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotQty.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotQty.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotQty.Location = New System.Drawing.Point(654, 234)
        Me.lblTotQty.Name = "lblTotQty"
        Me.lblTotQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotQty.Size = New System.Drawing.Size(74, 17)
        Me.lblTotQty.TabIndex = 54
        Me.lblTotQty.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotPackQtyCap
        '
        Me.lblTotPackQtyCap.AutoSize = True
        Me.lblTotPackQtyCap.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotPackQtyCap.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotPackQtyCap.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotPackQtyCap.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotPackQtyCap.Location = New System.Drawing.Point(590, 235)
        Me.lblTotPackQtyCap.Name = "lblTotPackQtyCap"
        Me.lblTotPackQtyCap.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotPackQtyCap.Size = New System.Drawing.Size(60, 14)
        Me.lblTotPackQtyCap.TabIndex = 53
        Me.lblTotPackQtyCap.Text = "Total Qty :"
        Me.lblTotPackQtyCap.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'fraService
        '
        Me.fraService.BackColor = System.Drawing.SystemColors.Control
        Me.fraService.Controls.Add(Me.txtSACCode)
        Me.fraService.Controls.Add(Me.txtTotItemValue)
        Me.fraService.Controls.Add(Me.txtRemarks1)
        Me.fraService.Controls.Add(Me.Label24)
        Me.fraService.Controls.Add(Me.Label23)
        Me.fraService.Controls.Add(Me.Label22)
        Me.fraService.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraService.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraService.Location = New System.Drawing.Point(0, 3)
        Me.fraService.Name = "fraService"
        Me.fraService.Padding = New System.Windows.Forms.Padding(0)
        Me.fraService.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraService.Size = New System.Drawing.Size(896, 45)
        Me.fraService.TabIndex = 111
        Me.fraService.TabStop = False
        '
        'txtSACCode
        '
        Me.txtSACCode.AcceptsReturn = True
        Me.txtSACCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtSACCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSACCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSACCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSACCode.ForeColor = System.Drawing.Color.Blue
        Me.txtSACCode.Location = New System.Drawing.Point(596, 14)
        Me.txtSACCode.MaxLength = 0
        Me.txtSACCode.Name = "txtSACCode"
        Me.txtSACCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSACCode.Size = New System.Drawing.Size(111, 20)
        Me.txtSACCode.TabIndex = 113
        '
        'txtTotItemValue
        '
        Me.txtTotItemValue.AcceptsReturn = True
        Me.txtTotItemValue.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotItemValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotItemValue.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotItemValue.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotItemValue.ForeColor = System.Drawing.Color.Blue
        Me.txtTotItemValue.Location = New System.Drawing.Point(762, 14)
        Me.txtTotItemValue.MaxLength = 0
        Me.txtTotItemValue.Name = "txtTotItemValue"
        Me.txtTotItemValue.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotItemValue.Size = New System.Drawing.Size(129, 20)
        Me.txtTotItemValue.TabIndex = 114
        '
        'txtRemarks1
        '
        Me.txtRemarks1.AcceptsReturn = True
        Me.txtRemarks1.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRemarks1.Location = New System.Drawing.Point(89, 14)
        Me.txtRemarks1.MaxLength = 0
        Me.txtRemarks1.Multiline = True
        Me.txtRemarks1.Name = "txtRemarks1"
        Me.txtRemarks1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks1.Size = New System.Drawing.Size(461, 20)
        Me.txtRemarks1.TabIndex = 112
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.SystemColors.Control
        Me.Label24.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label24.Location = New System.Drawing.Point(556, 14)
        Me.Label24.Name = "Label24"
        Me.Label24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label24.Size = New System.Drawing.Size(34, 14)
        Me.Label24.TabIndex = 117
        Me.Label24.Text = "HSN :"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.SystemColors.Control
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label23.Location = New System.Drawing.Point(713, 14)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(43, 14)
        Me.Label23.TabIndex = 115
        Me.Label23.Text = "Value :"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.SystemColors.Control
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(6, 14)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(76, 14)
        Me.Label22.TabIndex = 113
        Me.Label22.Text = "Description :"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_SSTab1_TabPage1
        '
        Me._SSTab1_TabPage1.Controls.Add(Me.Frame1)
        Me._SSTab1_TabPage1.Location = New System.Drawing.Point(4, 22)
        Me._SSTab1_TabPage1.Name = "_SSTab1_TabPage1"
        Me._SSTab1_TabPage1.Size = New System.Drawing.Size(900, 356)
        Me._SSTab1_TabPage1.TabIndex = 1
        Me._SSTab1_TabPage1.Text = "Other Details"
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.CmdPopPaymentFromFile)
        Me.Frame1.Controls.Add(Me.lblPaymentDC)
        Me.Frame1.Controls.Add(Me.Label25)
        Me.Frame1.Controls.Add(Me.lblPaymentTotal)
        Me.Frame1.Controls.Add(Me.CmdUpdatePayment)
        Me.Frame1.Controls.Add(Me.fraPayment)
        Me.Frame1.Controls.Add(Me.Frame14)
        Me.Frame1.Controls.Add(Me.txtPaymentdate)
        Me.Frame1.Controls.Add(Me.txtTariff)
        Me.Frame1.Controls.Add(Me.txtNarration)
        Me.Frame1.Controls.Add(Me.txtRemarks)
        Me.Frame1.Controls.Add(Me.txtItemType)
        Me.Frame1.Controls.Add(Me.lblGoodService)
        Me.Frame1.Controls.Add(Me.Label7)
        Me.Frame1.Controls.Add(Me.Label9)
        Me.Frame1.Controls.Add(Me.Label32)
        Me.Frame1.Controls.Add(Me.Label26)
        Me.Frame1.Controls.Add(Me.Label11)
        Me.Frame1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(900, 356)
        Me.Frame1.TabIndex = 48
        Me.Frame1.TabStop = False
        '
        'CmdPopPaymentFromFile
        '
        Me.CmdPopPaymentFromFile.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPopPaymentFromFile.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPopPaymentFromFile.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPopPaymentFromFile.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPopPaymentFromFile.Location = New System.Drawing.Point(519, 218)
        Me.CmdPopPaymentFromFile.Name = "CmdPopPaymentFromFile"
        Me.CmdPopPaymentFromFile.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPopPaymentFromFile.Size = New System.Drawing.Size(157, 25)
        Me.CmdPopPaymentFromFile.TabIndex = 212
        Me.CmdPopPaymentFromFile.Text = "Populate From File"
        Me.CmdPopPaymentFromFile.UseVisualStyleBackColor = False
        '
        'lblPaymentDC
        '
        Me.lblPaymentDC.BackColor = System.Drawing.SystemColors.Control
        Me.lblPaymentDC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPaymentDC.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPaymentDC.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPaymentDC.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblPaymentDC.Location = New System.Drawing.Point(824, 222)
        Me.lblPaymentDC.Name = "lblPaymentDC"
        Me.lblPaymentDC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPaymentDC.Size = New System.Drawing.Size(56, 19)
        Me.lblPaymentDC.TabIndex = 211
        Me.lblPaymentDC.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.SystemColors.Control
        Me.Label25.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label25.Location = New System.Drawing.Point(682, 225)
        Me.Label25.Name = "Label25"
        Me.Label25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label25.Size = New System.Drawing.Size(42, 14)
        Me.Label25.TabIndex = 210
        Me.Label25.Text = "Total : "
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblPaymentTotal
        '
        Me.lblPaymentTotal.BackColor = System.Drawing.SystemColors.Control
        Me.lblPaymentTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPaymentTotal.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPaymentTotal.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPaymentTotal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblPaymentTotal.Location = New System.Drawing.Point(726, 222)
        Me.lblPaymentTotal.Name = "lblPaymentTotal"
        Me.lblPaymentTotal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPaymentTotal.Size = New System.Drawing.Size(92, 19)
        Me.lblPaymentTotal.TabIndex = 209
        Me.lblPaymentTotal.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'CmdUpdatePayment
        '
        Me.CmdUpdatePayment.BackColor = System.Drawing.SystemColors.Control
        Me.CmdUpdatePayment.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdUpdatePayment.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdUpdatePayment.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdUpdatePayment.Location = New System.Drawing.Point(758, 277)
        Me.CmdUpdatePayment.Name = "CmdUpdatePayment"
        Me.CmdUpdatePayment.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdUpdatePayment.Size = New System.Drawing.Size(134, 23)
        Me.CmdUpdatePayment.TabIndex = 208
        Me.CmdUpdatePayment.Text = "Update Payment Only"
        Me.CmdUpdatePayment.UseVisualStyleBackColor = False
        '
        'fraPayment
        '
        Me.fraPayment.Controls.Add(Me.SprdPaymentDetail)
        Me.fraPayment.Location = New System.Drawing.Point(374, 0)
        Me.fraPayment.Name = "fraPayment"
        Me.fraPayment.Size = New System.Drawing.Size(524, 218)
        Me.fraPayment.TabIndex = 115
        Me.fraPayment.TabStop = False
        Me.fraPayment.Text = "Payment Details"
        '
        'SprdPaymentDetail
        '
        Me.SprdPaymentDetail.DataSource = Nothing
        Me.SprdPaymentDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SprdPaymentDetail.Location = New System.Drawing.Point(3, 16)
        Me.SprdPaymentDetail.Name = "SprdPaymentDetail"
        Me.SprdPaymentDetail.OcxState = CType(resources.GetObject("SprdPaymentDetail.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdPaymentDetail.Size = New System.Drawing.Size(518, 199)
        Me.SprdPaymentDetail.TabIndex = 143
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
        Me.Frame14.Location = New System.Drawing.Point(-2, 216)
        Me.Frame14.Name = "Frame14"
        Me.Frame14.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame14.Size = New System.Drawing.Size(518, 136)
        Me.Frame14.TabIndex = 114
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
        Me.txteInvAckDate.Location = New System.Drawing.Point(363, 74)
        Me.txteInvAckDate.MaxLength = 0
        Me.txteInvAckDate.Name = "txteInvAckDate"
        Me.txteInvAckDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txteInvAckDate.Size = New System.Drawing.Size(150, 20)
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
        Me.Label58.Location = New System.Drawing.Point(301, 78)
        Me.Label58.Name = "Label58"
        Me.Label58.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label58.Size = New System.Drawing.Size(57, 14)
        Me.Label58.TabIndex = 119
        Me.Label58.Text = "Ack Date :"
        '
        'txtPaymentdate
        '
        Me.txtPaymentdate.AcceptsReturn = True
        Me.txtPaymentdate.BackColor = System.Drawing.SystemColors.Window
        Me.txtPaymentdate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPaymentdate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPaymentdate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaymentdate.ForeColor = System.Drawing.Color.Blue
        Me.txtPaymentdate.Location = New System.Drawing.Point(98, 175)
        Me.txtPaymentdate.MaxLength = 0
        Me.txtPaymentdate.Name = "txtPaymentdate"
        Me.txtPaymentdate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPaymentdate.Size = New System.Drawing.Size(73, 20)
        Me.txtPaymentdate.TabIndex = 28
        '
        'txtTariff
        '
        Me.txtTariff.AcceptsReturn = True
        Me.txtTariff.BackColor = System.Drawing.SystemColors.Window
        Me.txtTariff.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTariff.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTariff.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTariff.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTariff.Location = New System.Drawing.Point(98, 16)
        Me.txtTariff.MaxLength = 0
        Me.txtTariff.Name = "txtTariff"
        Me.txtTariff.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTariff.Size = New System.Drawing.Size(76, 20)
        Me.txtTariff.TabIndex = 24
        '
        'txtNarration
        '
        Me.txtNarration.AcceptsReturn = True
        Me.txtNarration.BackColor = System.Drawing.SystemColors.Window
        Me.txtNarration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNarration.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNarration.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNarration.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNarration.Location = New System.Drawing.Point(98, 100)
        Me.txtNarration.MaxLength = 0
        Me.txtNarration.Multiline = True
        Me.txtNarration.Name = "txtNarration"
        Me.txtNarration.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNarration.Size = New System.Drawing.Size(267, 71)
        Me.txtNarration.TabIndex = 27
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRemarks.Location = New System.Drawing.Point(98, 40)
        Me.txtRemarks.MaxLength = 0
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.Size = New System.Drawing.Size(267, 56)
        Me.txtRemarks.TabIndex = 26
        '
        'txtItemType
        '
        Me.txtItemType.AcceptsReturn = True
        Me.txtItemType.BackColor = System.Drawing.SystemColors.Window
        Me.txtItemType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemType.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItemType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtItemType.Location = New System.Drawing.Point(258, 17)
        Me.txtItemType.MaxLength = 0
        Me.txtItemType.Name = "txtItemType"
        Me.txtItemType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemType.Size = New System.Drawing.Size(113, 20)
        Me.txtItemType.TabIndex = 25
        '
        'lblGoodService
        '
        Me.lblGoodService.BackColor = System.Drawing.SystemColors.Control
        Me.lblGoodService.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblGoodService.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGoodService.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblGoodService.Location = New System.Drawing.Point(186, 187)
        Me.lblGoodService.Name = "lblGoodService"
        Me.lblGoodService.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblGoodService.Size = New System.Drawing.Size(107, 25)
        Me.lblGoodService.TabIndex = 110
        Me.lblGoodService.Text = "lblGoodService"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(4, 178)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(88, 14)
        Me.Label7.TabIndex = 73
        Me.Label7.Text = "Payment Date :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(51, 19)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(41, 14)
        Me.Label9.TabIndex = 72
        Me.Label9.Text = "Tariff :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.BackColor = System.Drawing.SystemColors.Control
        Me.Label32.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label32.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label32.Location = New System.Drawing.Point(29, 102)
        Me.Label32.Name = "Label32"
        Me.Label32.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label32.Size = New System.Drawing.Size(63, 14)
        Me.Label32.TabIndex = 51
        Me.Label32.Text = "Narration :"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.SystemColors.Control
        Me.Label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(29, 43)
        Me.Label26.Name = "Label26"
        Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label26.Size = New System.Drawing.Size(63, 14)
        Me.Label26.TabIndex = 50
        Me.Label26.Text = "Remarks :"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(185, 19)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(67, 14)
        Me.Label11.TabIndex = 49
        Me.Label11.Text = "Item Type :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtDebitAccount
        '
        Me.txtDebitAccount.AcceptsReturn = True
        Me.txtDebitAccount.BackColor = System.Drawing.SystemColors.Window
        Me.txtDebitAccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDebitAccount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDebitAccount.Enabled = False
        Me.txtDebitAccount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDebitAccount.ForeColor = System.Drawing.Color.Blue
        Me.txtDebitAccount.Location = New System.Drawing.Point(596, 108)
        Me.txtDebitAccount.MaxLength = 0
        Me.txtDebitAccount.Name = "txtDebitAccount"
        Me.txtDebitAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDebitAccount.Size = New System.Drawing.Size(308, 20)
        Me.txtDebitAccount.TabIndex = 18
        '
        'txtSupplier
        '
        Me.txtSupplier.AcceptsReturn = True
        Me.txtSupplier.BackColor = System.Drawing.SystemColors.Window
        Me.txtSupplier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplier.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSupplier.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplier.ForeColor = System.Drawing.Color.Blue
        Me.txtSupplier.Location = New System.Drawing.Point(596, 60)
        Me.txtSupplier.MaxLength = 0
        Me.txtSupplier.Name = "txtSupplier"
        Me.txtSupplier.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSupplier.Size = New System.Drawing.Size(308, 20)
        Me.txtSupplier.TabIndex = 4
        '
        'txtBillDate
        '
        Me.txtBillDate.AcceptsReturn = True
        Me.txtBillDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillDate.ForeColor = System.Drawing.Color.Blue
        Me.txtBillDate.Location = New System.Drawing.Point(279, 36)
        Me.txtBillDate.MaxLength = 0
        Me.txtBillDate.Name = "txtBillDate"
        Me.txtBillDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillDate.Size = New System.Drawing.Size(65, 20)
        Me.txtBillDate.TabIndex = 6
        '
        'cboInvType
        '
        Me.cboInvType.BackColor = System.Drawing.SystemColors.Window
        Me.cboInvType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboInvType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboInvType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboInvType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboInvType.Location = New System.Drawing.Point(596, 12)
        Me.cboInvType.Name = "cboInvType"
        Me.cboInvType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboInvType.Size = New System.Drawing.Size(308, 22)
        Me.cboInvType.TabIndex = 2
        '
        'txtBillNo
        '
        Me.txtBillNo.AcceptsReturn = True
        Me.txtBillNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtBillNo.Location = New System.Drawing.Point(83, 36)
        Me.txtBillNo.MaxLength = 0
        Me.txtBillNo.Name = "txtBillNo"
        Me.txtBillNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillNo.Size = New System.Drawing.Size(118, 20)
        Me.txtBillNo.TabIndex = 5
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(505, 137)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(86, 14)
        Me.Label19.TabIndex = 109
        Me.Label19.Text = "Party Note No :"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(779, 137)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(37, 14)
        Me.Label20.TabIndex = 108
        Me.Label20.Text = "Date :"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.SystemColors.Control
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(524, 161)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(67, 14)
        Me.Label21.TabIndex = 107
        Me.Label21.Text = "Recd Date :"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblReason
        '
        Me.lblReason.AutoSize = True
        Me.lblReason.BackColor = System.Drawing.SystemColors.Control
        Me.lblReason.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblReason.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReason.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblReason.Location = New System.Drawing.Point(29, 111)
        Me.lblReason.Name = "lblReason"
        Me.lblReason.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblReason.Size = New System.Drawing.Size(54, 14)
        Me.lblReason.TabIndex = 103
        Me.lblReason.Text = "Reason :"
        Me.lblReason.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(240, 137)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(37, 14)
        Me.Label18.TabIndex = 101
        Me.Label18.Text = "Date :"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(4, 137)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(76, 13)
        Me.Label10.TabIndex = 100
        Me.Label10.Text = "Original Bill :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LblBookCode
        '
        Me.LblBookCode.BackColor = System.Drawing.SystemColors.Control
        Me.LblBookCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblBookCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBookCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblBookCode.Location = New System.Drawing.Point(848, 172)
        Me.LblBookCode.Name = "LblBookCode"
        Me.LblBookCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblBookCode.Size = New System.Drawing.Size(44, 11)
        Me.LblBookCode.TabIndex = 98
        Me.LblBookCode.Text = "LblBookCode"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(535, 42)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(56, 14)
        Me.Label14.TabIndex = 85
        Me.Label14.Text = "Division :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Enabled = False
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(339, 68)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(76, 13)
        Me.Label8.TabIndex = 75
        Me.Label8.Text = "Amend No :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label8.Visible = False
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.SystemColors.Control
        Me.Label27.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label27.Location = New System.Drawing.Point(221, 87)
        Me.Label27.Name = "Label27"
        Me.Label27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label27.Size = New System.Drawing.Size(53, 14)
        Me.Label27.TabIndex = 80
        Me.Label27.Text = "To Date :"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(15, 87)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(69, 14)
        Me.Label15.TabIndex = 76
        Me.Label15.Text = "From Date :"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(5, 68)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(76, 13)
        Me.Label4.TabIndex = 74
        Me.Label4.Text = "PO No :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblPurchaseVNo
        '
        Me.lblPurchaseVNo.AutoSize = True
        Me.lblPurchaseVNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblPurchaseVNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPurchaseVNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPurchaseVNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPurchaseVNo.Location = New System.Drawing.Point(194, 14)
        Me.lblPurchaseVNo.Name = "lblPurchaseVNo"
        Me.lblPurchaseVNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPurchaseVNo.Size = New System.Drawing.Size(0, 14)
        Me.lblPurchaseVNo.TabIndex = 71
        '
        'lblVNo
        '
        Me.lblVNo.AutoSize = True
        Me.lblVNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblVNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblVNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblVNo.Location = New System.Drawing.Point(5, 16)
        Me.lblVNo.Name = "lblVNo"
        Me.lblVNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblVNo.Size = New System.Drawing.Size(76, 14)
        Me.lblVNo.TabIndex = 69
        Me.lblVNo.Text = "Voucher No :"
        Me.lblVNo.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(283, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(37, 14)
        Me.Label6.TabIndex = 68
        Me.Label6.Text = "Date :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(237, 68)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(37, 14)
        Me.Label12.TabIndex = 60
        Me.Label12.Text = "Date :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(530, 111)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(61, 14)
        Me.Label3.TabIndex = 46
        Me.Label3.Text = "Debit A/c :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCust
        '
        Me.lblCust.AutoSize = True
        Me.lblCust.BackColor = System.Drawing.SystemColors.Control
        Me.lblCust.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCust.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCust.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCust.Location = New System.Drawing.Point(522, 68)
        Me.lblCust.Name = "lblCust"
        Me.lblCust.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCust.Size = New System.Drawing.Size(69, 14)
        Me.lblCust.TabIndex = 45
        Me.lblCust.Text = "Customer :"
        Me.lblCust.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(237, 42)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(37, 14)
        Me.Label5.TabIndex = 44
        Me.Label5.Text = "Date :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblInvType
        '
        Me.lblInvType.AutoSize = True
        Me.lblInvType.BackColor = System.Drawing.SystemColors.Control
        Me.lblInvType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblInvType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInvType.Location = New System.Drawing.Point(510, 16)
        Me.lblInvType.Name = "lblInvType"
        Me.lblInvType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblInvType.Size = New System.Drawing.Size(81, 14)
        Me.lblInvType.TabIndex = 37
        Me.lblInvType.Text = "Invoice Type :"
        Me.lblInvType.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(5, 42)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(76, 13)
        Me.Label1.TabIndex = 43
        Me.Label1.Text = "Bill No :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(908, 568)
        Me.SprdView.TabIndex = 39
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.cmdPostingHead)
        Me.Frame3.Controls.Add(Me.cmdShow)
        Me.Frame3.Controls.Add(Me.cmdClose)
        Me.Frame3.Controls.Add(Me.CmdView)
        Me.Frame3.Controls.Add(Me.CmdPreview)
        Me.Frame3.Controls.Add(Me.cmdPrint)
        Me.Frame3.Controls.Add(Me.cmdSavePrint)
        Me.Frame3.Controls.Add(Me.cmdDelete)
        Me.Frame3.Controls.Add(Me.cmdSave)
        Me.Frame3.Controls.Add(Me.cmdModify)
        Me.Frame3.Controls.Add(Me.Report1)
        Me.Frame3.Controls.Add(Me.cmdAdd)
        Me.Frame3.Controls.Add(Me.lblBookType)
        Me.Frame3.Controls.Add(Me.lblPMKey)
        Me.Frame3.Controls.Add(Me.lblSODates)
        Me.Frame3.Controls.Add(Me.lblSONos)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(2, 566)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(904, 52)
        Me.Frame3.TabIndex = 38
        Me.Frame3.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(14, 12)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 98
        '
        'lblBookType
        '
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(720, 22)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(25, 19)
        Me.lblBookType.TabIndex = 81
        Me.lblBookType.Text = "lblBookType"
        Me.lblBookType.Visible = False
        '
        'lblPMKey
        '
        Me.lblPMKey.BackColor = System.Drawing.SystemColors.Control
        Me.lblPMKey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPMKey.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPMKey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPMKey.Location = New System.Drawing.Point(6, 36)
        Me.lblPMKey.Name = "lblPMKey"
        Me.lblPMKey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPMKey.Size = New System.Drawing.Size(37, 11)
        Me.lblPMKey.TabIndex = 70
        Me.lblPMKey.Text = "lblPMKey"
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
        Me.lblSODates.TabIndex = 41
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
        Me.lblSONos.TabIndex = 40
        Me.lblSONos.Text = "lblSONos"
        Me.lblSONos.Visible = False
        '
        'FrmCust_SaleGST
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(910, 621)
        Me.Controls.Add(Me.FraFront)
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
        Me.Name = "FrmCust_SaleGST"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Customer Debit Note  / Our Credit Note (Rate Diff/Shortage/Others)"
        Me.FraFront.ResumeLayout(False)
        Me.FraFront.PerformLayout()
        Me.SSTab1.ResumeLayout(False)
        Me._SSTab1_TabPage0.ResumeLayout(False)
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me.FraAcctPostDetail.ResumeLayout(False)
        CType(Me.sprdAcctPostDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraPostingDtl.ResumeLayout(False)
        CType(Me.SprdPostingDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdExp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraService.ResumeLayout(False)
        Me.fraService.PerformLayout()
        Me._SSTab1_TabPage1.ResumeLayout(False)
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.fraPayment.ResumeLayout(False)
        CType(Me.SprdPaymentDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame14.ResumeLayout(False)
        Me.Frame14.PerformLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame3.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
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

    Public WithEvents cmdBillToSearch As Button
    Public WithEvents txtBillTo As TextBox
    Public WithEvents Label37 As Label
    Public WithEvents CmdPopFromFile As Button
    Public WithEvents CommonDialogFont As FontDialog
    Public WithEvents CommonDialogColor As ColorDialog
    Public WithEvents CommonDialogPrint As PrintDialog
    Public WithEvents CommonDialogSave As SaveFileDialog
    Public WithEvents CommonDialogOpen As OpenFileDialog
    Public WithEvents Frame14 As GroupBox
    Public WithEvents cmpPrinteInvoice As Button
    Public WithEvents txtIRNNo As TextBox
    Public WithEvents txteInvAckNo As TextBox
    Public WithEvents txteInvAckDate As TextBox
    Public WithEvents cmdeInvoice As Button
    Public WithEvents cmdQRCode As Button
    Public WithEvents Label51 As Label
    Public WithEvents Label54 As Label
    Public WithEvents Label58 As Label
    Friend WithEvents fraPayment As GroupBox
    Friend WithEvents lblDiffAmt As Label
    Public WithEvents CmdUpdatePayment As Button
    Public WithEvents chkItemDetails As CheckBox
    Public WithEvents lblPaymentDC As Label
    Public WithEvents Label25 As Label
    Public WithEvents lblPaymentTotal As Label
    Public WithEvents CmdPopPaymentFromFile As Button
#End Region
End Class