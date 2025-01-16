Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmFGCostingCustomerWise
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
	Public WithEvents txtCopyProductCode As System.Windows.Forms.TextBox
	Public WithEvents txtCopyCustCode As System.Windows.Forms.TextBox
	Public WithEvents txtCopyProductDesc As System.Windows.Forms.TextBox
	Public WithEvents cmdSearchCopyProdCode As System.Windows.Forms.Button
	Public WithEvents cmdPopulate As System.Windows.Forms.Button
	Public WithEvents txtAmendNo As System.Windows.Forms.TextBox
	Public WithEvents txtUnit As System.Windows.Forms.TextBox
	Public WithEvents txtCustPartNo As System.Windows.Forms.TextBox
	Public WithEvents txtSuppCustName As System.Windows.Forms.TextBox
	Public WithEvents txtModelNo As System.Windows.Forms.TextBox
	Public WithEvents txtProductDesc As System.Windows.Forms.TextBox
	Public WithEvents txtSuppCustCode As System.Windows.Forms.TextBox
	Public WithEvents cmdSearchCust As System.Windows.Forms.Button
	Public WithEvents cmdSearchProdCode As System.Windows.Forms.Button
	Public WithEvents txtProductCode As System.Windows.Forms.TextBox
	Public WithEvents cmdSearchWEF As System.Windows.Forms.Button
	Public WithEvents txtWEF As System.Windows.Forms.TextBox
	Public WithEvents Label18 As System.Windows.Forms.Label
	Public WithEvents Label6 As System.Windows.Forms.Label
	Public WithEvents lblMKey As System.Windows.Forms.Label
	Public WithEvents Label7 As System.Windows.Forms.Label
	Public WithEvents Label3 As System.Windows.Forms.Label
	Public WithEvents Label4 As System.Windows.Forms.Label
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents Label9 As System.Windows.Forms.Label
	Public WithEvents Frame1 As System.Windows.Forms.GroupBox
	Public WithEvents txtTransportCost As System.Windows.Forms.TextBox
	Public WithEvents txtTotWeldCost As System.Windows.Forms.TextBox
	Public WithEvents txtOverheadCost As System.Windows.Forms.TextBox
	Public WithEvents txtOverheadPer As System.Windows.Forms.TextBox
	Public WithEvents txtTotProdCost As System.Windows.Forms.TextBox
	Public WithEvents txtTotValueAdd As System.Windows.Forms.TextBox
	Public WithEvents txtTotPdrCost As System.Windows.Forms.TextBox
	Public WithEvents txtTotPntCost As System.Windows.Forms.TextBox
	Public WithEvents txtTotPltCost As System.Windows.Forms.TextBox
	Public WithEvents txtTotProcessCost As System.Windows.Forms.TextBox
	Public WithEvents txtTotBOPCost As System.Windows.Forms.TextBox
	Public WithEvents txtTotRMCost As System.Windows.Forms.TextBox
	Public WithEvents Label17 As System.Windows.Forms.Label
	Public WithEvents Label43 As System.Windows.Forms.Label
	Public WithEvents Label56 As System.Windows.Forms.Label
	Public WithEvents Label52 As System.Windows.Forms.Label
	Public WithEvents Label51 As System.Windows.Forms.Label
	Public WithEvents lblPackMaterialCost As System.Windows.Forms.Label
	Public WithEvents lblInterest As System.Windows.Forms.Label
	Public WithEvents lblToolCost As System.Windows.Forms.Label
	Public WithEvents lblHandlingCode As System.Windows.Forms.Label
	Public WithEvents lblOperationCost As System.Windows.Forms.Label
	Public WithEvents Label37 As System.Windows.Forms.Label
	Public WithEvents Label28 As System.Windows.Forms.Label
	Public WithEvents Label11 As System.Windows.Forms.Label
	Public WithEvents Label10 As System.Windows.Forms.Label
	Public WithEvents Label8 As System.Windows.Forms.Label
	Public WithEvents Label36 As System.Windows.Forms.Label
	Public WithEvents Label32 As System.Windows.Forms.Label
	Public WithEvents Label31 As System.Windows.Forms.Label
	Public WithEvents Label30 As System.Windows.Forms.Label
	Public WithEvents Label29 As System.Windows.Forms.Label
	Public WithEvents Label27 As System.Windows.Forms.Label
	Public WithEvents Label26 As System.Windows.Forms.Label
	Public WithEvents Label25 As System.Windows.Forms.Label
	Public WithEvents Label24 As System.Windows.Forms.Label
	Public WithEvents Label23 As System.Windows.Forms.Label
	Public WithEvents Label22 As System.Windows.Forms.Label
	Public WithEvents Label21 As System.Windows.Forms.Label
	Public WithEvents Label20 As System.Windows.Forms.Label
	Public WithEvents Label19 As System.Windows.Forms.Label
	Public WithEvents Frame2 As System.Windows.Forms.GroupBox
	Public WithEvents txtPMCost As System.Windows.Forms.TextBox
	Public WithEvents txtICC As System.Windows.Forms.TextBox
	Public WithEvents txtHandling As System.Windows.Forms.TextBox
	Public WithEvents txtToolCost As System.Windows.Forms.TextBox
	Public WithEvents txtDiscount As System.Windows.Forms.TextBox
	Public WithEvents txtTotSaleCost As System.Windows.Forms.TextBox
	Public WithEvents txtRejPer As System.Windows.Forms.TextBox
	Public WithEvents txtRejCost As System.Windows.Forms.TextBox
	Public WithEvents txtProfitPer As System.Windows.Forms.TextBox
	Public WithEvents txtProfitCost As System.Windows.Forms.TextBox
	Public WithEvents txtTotSalePrice As System.Windows.Forms.TextBox
	Public WithEvents txtTotPriceSettelled As System.Windows.Forms.TextBox
	Public WithEvents txtCustPONo As System.Windows.Forms.TextBox
	Public WithEvents txtCustPODate As System.Windows.Forms.TextBox
	Public WithEvents Label59 As System.Windows.Forms.Label
	Public WithEvents Label58 As System.Windows.Forms.Label
	Public WithEvents Label57 As System.Windows.Forms.Label
	Public WithEvents Label55 As System.Windows.Forms.Label
	Public WithEvents Label54 As System.Windows.Forms.Label
	Public WithEvents Label44 As System.Windows.Forms.Label
	Public WithEvents Label53 As System.Windows.Forms.Label
	Public WithEvents Label50 As System.Windows.Forms.Label
	Public WithEvents Label38 As System.Windows.Forms.Label
	Public WithEvents Label46 As System.Windows.Forms.Label
	Public WithEvents Label48 As System.Windows.Forms.Label
	Public WithEvents Label47 As System.Windows.Forms.Label
	Public WithEvents Label45 As System.Windows.Forms.Label
	Public WithEvents Label42 As System.Windows.Forms.Label
	Public WithEvents Label41 As System.Windows.Forms.Label
	Public WithEvents Label39 As System.Windows.Forms.Label
	Public WithEvents Label35 As System.Windows.Forms.Label
	Public WithEvents Label34 As System.Windows.Forms.Label
	Public WithEvents Label33 As System.Windows.Forms.Label
	Public WithEvents Label16 As System.Windows.Forms.Label
	Public WithEvents Label15 As System.Windows.Forms.Label
	Public WithEvents Label12 As System.Windows.Forms.Label
	Public WithEvents Frame4 As System.Windows.Forms.GroupBox
	Public WithEvents _SSTab1_TabPage0 As System.Windows.Forms.TabPage
	Public WithEvents SprdRM As AxFPSpreadADO.AxfpSpread
	Public WithEvents _SSTab1_TabPage1 As System.Windows.Forms.TabPage
	Public WithEvents SprdBOP As AxFPSpreadADO.AxfpSpread
	Public WithEvents _SSTab1_TabPage2 As System.Windows.Forms.TabPage
	Public WithEvents SprdWeld As AxFPSpreadADO.AxfpSpread
	Public WithEvents _SSTab1_TabPage3 As System.Windows.Forms.TabPage
	Public WithEvents SprdOpr As AxFPSpreadADO.AxfpSpread
	Public WithEvents _SSTab1_TabPage4 As System.Windows.Forms.TabPage
	Public WithEvents CboPlatingType As System.Windows.Forms.ComboBox
	Public WithEvents SprdPlt As AxFPSpreadADO.AxfpSpread
	Public WithEvents Label40 As System.Windows.Forms.Label
	Public WithEvents _SSTab1_TabPage5 As System.Windows.Forms.TabPage
	Public WithEvents SprdPnt As AxFPSpreadADO.AxfpSpread
	Public WithEvents _SSTab1_TabPage6 As System.Windows.Forms.TabPage
	Public WithEvents cboPowderType As System.Windows.Forms.ComboBox
	Public WithEvents SprdPdr As AxFPSpreadADO.AxfpSpread
	Public WithEvents Label49 As System.Windows.Forms.Label
	Public WithEvents _SSTab1_TabPage7 As System.Windows.Forms.TabPage
	Public WithEvents SprdPack As AxFPSpreadADO.AxfpSpread
	Public WithEvents _SSTab1_TabPage8 As System.Windows.Forms.TabPage
	Public WithEvents SSTab1 As System.Windows.Forms.TabControl
	Public WithEvents chkStatus As System.Windows.Forms.CheckBox
	Public WithEvents txtRemarks As System.Windows.Forms.TextBox
	Public WithEvents txtAppBy As System.Windows.Forms.TextBox
	Public WithEvents txtPrepBy As System.Windows.Forms.TextBox
	Public WithEvents cmdSearchPrepBy As System.Windows.Forms.Button
	Public WithEvents cmdSearchAppBy As System.Windows.Forms.Button
	Public WithEvents lblTotPackCost As System.Windows.Forms.Label
	Public WithEvents Label14 As System.Windows.Forms.Label
	Public WithEvents lblAppBy As System.Windows.Forms.Label
	Public WithEvents Label13 As System.Windows.Forms.Label
	Public WithEvents lblPrepBy As System.Windows.Forms.Label
	Public WithEvents Label5 As System.Windows.Forms.Label
	Public WithEvents fraCosting As System.Windows.Forms.GroupBox
	Public WithEvents fraBase As System.Windows.Forms.GroupBox
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents CmdClose As System.Windows.Forms.Button
	Public WithEvents CmdView As System.Windows.Forms.Button
	Public WithEvents CmdPreview As System.Windows.Forms.Button
	Public WithEvents Report1 As AxCrystal.AxCrystalReport
	Public WithEvents cmdPrint As System.Windows.Forms.Button
	Public WithEvents CmdDelete As System.Windows.Forms.Button
	Public WithEvents cmdSavePrint As System.Windows.Forms.Button
	Public WithEvents CmdSave As System.Windows.Forms.Button
	Public WithEvents cmdAmend As System.Windows.Forms.Button
	Public WithEvents CmdModify As System.Windows.Forms.Button
	Public WithEvents CmdAdd As System.Windows.Forms.Button
	Public WithEvents Frame3 As System.Windows.Forms.GroupBox
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFGCostingCustomerWise))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtCopyProductCode = New System.Windows.Forms.TextBox()
        Me.txtCopyCustCode = New System.Windows.Forms.TextBox()
        Me.txtCopyProductDesc = New System.Windows.Forms.TextBox()
        Me.cmdSearchCopyProdCode = New System.Windows.Forms.Button()
        Me.txtAmendNo = New System.Windows.Forms.TextBox()
        Me.txtUnit = New System.Windows.Forms.TextBox()
        Me.txtCustPartNo = New System.Windows.Forms.TextBox()
        Me.txtSuppCustName = New System.Windows.Forms.TextBox()
        Me.txtModelNo = New System.Windows.Forms.TextBox()
        Me.txtProductDesc = New System.Windows.Forms.TextBox()
        Me.txtSuppCustCode = New System.Windows.Forms.TextBox()
        Me.cmdSearchCust = New System.Windows.Forms.Button()
        Me.cmdSearchProdCode = New System.Windows.Forms.Button()
        Me.txtProductCode = New System.Windows.Forms.TextBox()
        Me.cmdSearchWEF = New System.Windows.Forms.Button()
        Me.txtWEF = New System.Windows.Forms.TextBox()
        Me.cmdSearchPrepBy = New System.Windows.Forms.Button()
        Me.cmdSearchAppBy = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.cmdAmend = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.fraBase = New System.Windows.Forms.GroupBox()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.cmdPopulate = New System.Windows.Forms.Button()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblMKey = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.SSTab1 = New System.Windows.Forms.TabControl()
        Me._SSTab1_TabPage0 = New System.Windows.Forms.TabPage()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.txtTransportCost = New System.Windows.Forms.TextBox()
        Me.txtTotWeldCost = New System.Windows.Forms.TextBox()
        Me.txtOverheadCost = New System.Windows.Forms.TextBox()
        Me.txtOverheadPer = New System.Windows.Forms.TextBox()
        Me.txtTotProdCost = New System.Windows.Forms.TextBox()
        Me.txtTotValueAdd = New System.Windows.Forms.TextBox()
        Me.txtTotPdrCost = New System.Windows.Forms.TextBox()
        Me.txtTotPntCost = New System.Windows.Forms.TextBox()
        Me.txtTotPltCost = New System.Windows.Forms.TextBox()
        Me.txtTotProcessCost = New System.Windows.Forms.TextBox()
        Me.txtTotBOPCost = New System.Windows.Forms.TextBox()
        Me.txtTotRMCost = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.lblPackMaterialCost = New System.Windows.Forms.Label()
        Me.lblInterest = New System.Windows.Forms.Label()
        Me.lblToolCost = New System.Windows.Forms.Label()
        Me.lblHandlingCode = New System.Windows.Forms.Label()
        Me.lblOperationCost = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.txtPMCost = New System.Windows.Forms.TextBox()
        Me.txtICC = New System.Windows.Forms.TextBox()
        Me.txtHandling = New System.Windows.Forms.TextBox()
        Me.txtToolCost = New System.Windows.Forms.TextBox()
        Me.txtDiscount = New System.Windows.Forms.TextBox()
        Me.txtTotSaleCost = New System.Windows.Forms.TextBox()
        Me.txtRejPer = New System.Windows.Forms.TextBox()
        Me.txtRejCost = New System.Windows.Forms.TextBox()
        Me.txtProfitPer = New System.Windows.Forms.TextBox()
        Me.txtProfitCost = New System.Windows.Forms.TextBox()
        Me.txtTotSalePrice = New System.Windows.Forms.TextBox()
        Me.txtTotPriceSettelled = New System.Windows.Forms.TextBox()
        Me.txtCustPONo = New System.Windows.Forms.TextBox()
        Me.txtCustPODate = New System.Windows.Forms.TextBox()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me._SSTab1_TabPage1 = New System.Windows.Forms.TabPage()
        Me.SprdRM = New AxFPSpreadADO.AxfpSpread()
        Me._SSTab1_TabPage2 = New System.Windows.Forms.TabPage()
        Me.SprdBOP = New AxFPSpreadADO.AxfpSpread()
        Me._SSTab1_TabPage3 = New System.Windows.Forms.TabPage()
        Me.SprdWeld = New AxFPSpreadADO.AxfpSpread()
        Me._SSTab1_TabPage4 = New System.Windows.Forms.TabPage()
        Me.SprdOpr = New AxFPSpreadADO.AxfpSpread()
        Me._SSTab1_TabPage5 = New System.Windows.Forms.TabPage()
        Me.CboPlatingType = New System.Windows.Forms.ComboBox()
        Me.SprdPlt = New AxFPSpreadADO.AxfpSpread()
        Me.Label40 = New System.Windows.Forms.Label()
        Me._SSTab1_TabPage6 = New System.Windows.Forms.TabPage()
        Me.SprdPnt = New AxFPSpreadADO.AxfpSpread()
        Me._SSTab1_TabPage7 = New System.Windows.Forms.TabPage()
        Me.cboPowderType = New System.Windows.Forms.ComboBox()
        Me.SprdPdr = New AxFPSpreadADO.AxfpSpread()
        Me.Label49 = New System.Windows.Forms.Label()
        Me._SSTab1_TabPage8 = New System.Windows.Forms.TabPage()
        Me.SprdPack = New AxFPSpreadADO.AxfpSpread()
        Me.fraCosting = New System.Windows.Forms.GroupBox()
        Me.chkStatus = New System.Windows.Forms.CheckBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtAppBy = New System.Windows.Forms.TextBox()
        Me.txtPrepBy = New System.Windows.Forms.TextBox()
        Me.lblTotPackCost = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblAppBy = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblPrepBy = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.txtToolQty = New System.Windows.Forms.TextBox()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.txtToolCostPerPc = New System.Windows.Forms.TextBox()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.txtCostReduction = New System.Windows.Forms.TextBox()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.fraBase.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.SSTab1.SuspendLayout()
        Me._SSTab1_TabPage0.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame4.SuspendLayout()
        Me._SSTab1_TabPage1.SuspendLayout()
        CType(Me.SprdRM, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage2.SuspendLayout()
        CType(Me.SprdBOP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage3.SuspendLayout()
        CType(Me.SprdWeld, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage4.SuspendLayout()
        CType(Me.SprdOpr, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage5.SuspendLayout()
        CType(Me.SprdPlt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage6.SuspendLayout()
        CType(Me.SprdPnt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage7.SuspendLayout()
        CType(Me.SprdPdr, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage8.SuspendLayout()
        CType(Me.SprdPack, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraCosting.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtCopyProductCode
        '
        Me.txtCopyProductCode.AcceptsReturn = True
        Me.txtCopyProductCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtCopyProductCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCopyProductCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCopyProductCode.Enabled = False
        Me.txtCopyProductCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCopyProductCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCopyProductCode.Location = New System.Drawing.Point(180, 70)
        Me.txtCopyProductCode.MaxLength = 0
        Me.txtCopyProductCode.Name = "txtCopyProductCode"
        Me.txtCopyProductCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCopyProductCode.Size = New System.Drawing.Size(81, 20)
        Me.txtCopyProductCode.TabIndex = 13
        Me.ToolTip1.SetToolTip(Me.txtCopyProductCode, "Press F1 For Help")
        '
        'txtCopyCustCode
        '
        Me.txtCopyCustCode.AcceptsReturn = True
        Me.txtCopyCustCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtCopyCustCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCopyCustCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCopyCustCode.Enabled = False
        Me.txtCopyCustCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCopyCustCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCopyCustCode.Location = New System.Drawing.Point(98, 70)
        Me.txtCopyCustCode.MaxLength = 0
        Me.txtCopyCustCode.Name = "txtCopyCustCode"
        Me.txtCopyCustCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCopyCustCode.Size = New System.Drawing.Size(81, 20)
        Me.txtCopyCustCode.TabIndex = 12
        Me.ToolTip1.SetToolTip(Me.txtCopyCustCode, "Press F1 For Help")
        '
        'txtCopyProductDesc
        '
        Me.txtCopyProductDesc.AcceptsReturn = True
        Me.txtCopyProductDesc.BackColor = System.Drawing.SystemColors.Window
        Me.txtCopyProductDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCopyProductDesc.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCopyProductDesc.Enabled = False
        Me.txtCopyProductDesc.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCopyProductDesc.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCopyProductDesc.Location = New System.Drawing.Point(290, 70)
        Me.txtCopyProductDesc.MaxLength = 0
        Me.txtCopyProductDesc.Name = "txtCopyProductDesc"
        Me.txtCopyProductDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCopyProductDesc.Size = New System.Drawing.Size(271, 20)
        Me.txtCopyProductDesc.TabIndex = 15
        Me.ToolTip1.SetToolTip(Me.txtCopyProductDesc, "Press F1 For Help")
        '
        'cmdSearchCopyProdCode
        '
        Me.cmdSearchCopyProdCode.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchCopyProdCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchCopyProdCode.Enabled = False
        Me.cmdSearchCopyProdCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchCopyProdCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchCopyProdCode.Image = CType(resources.GetObject("cmdSearchCopyProdCode.Image"), System.Drawing.Image)
        Me.cmdSearchCopyProdCode.Location = New System.Drawing.Point(262, 70)
        Me.cmdSearchCopyProdCode.Name = "cmdSearchCopyProdCode"
        Me.cmdSearchCopyProdCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchCopyProdCode.Size = New System.Drawing.Size(27, 19)
        Me.cmdSearchCopyProdCode.TabIndex = 14
        Me.cmdSearchCopyProdCode.TabStop = False
        Me.cmdSearchCopyProdCode.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchCopyProdCode, "Search")
        Me.cmdSearchCopyProdCode.UseVisualStyleBackColor = False
        '
        'txtAmendNo
        '
        Me.txtAmendNo.AcceptsReturn = True
        Me.txtAmendNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtAmendNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmendNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAmendNo.Enabled = False
        Me.txtAmendNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmendNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAmendNo.Location = New System.Drawing.Point(283, 50)
        Me.txtAmendNo.MaxLength = 0
        Me.txtAmendNo.Name = "txtAmendNo"
        Me.txtAmendNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAmendNo.Size = New System.Drawing.Size(81, 20)
        Me.txtAmendNo.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.txtAmendNo, "Press F1 For Help")
        '
        'txtUnit
        '
        Me.txtUnit.AcceptsReturn = True
        Me.txtUnit.BackColor = System.Drawing.SystemColors.Window
        Me.txtUnit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUnit.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUnit.Enabled = False
        Me.txtUnit.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUnit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUnit.Location = New System.Drawing.Point(616, 11)
        Me.txtUnit.MaxLength = 0
        Me.txtUnit.Name = "txtUnit"
        Me.txtUnit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUnit.Size = New System.Drawing.Size(127, 20)
        Me.txtUnit.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.txtUnit, "Press F1 For Help")
        '
        'txtCustPartNo
        '
        Me.txtCustPartNo.AcceptsReturn = True
        Me.txtCustPartNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustPartNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustPartNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustPartNo.Enabled = False
        Me.txtCustPartNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustPartNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCustPartNo.Location = New System.Drawing.Point(616, 30)
        Me.txtCustPartNo.MaxLength = 0
        Me.txtCustPartNo.Name = "txtCustPartNo"
        Me.txtCustPartNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustPartNo.Size = New System.Drawing.Size(127, 20)
        Me.txtCustPartNo.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.txtCustPartNo, "Press F1 For Help")
        '
        'txtSuppCustName
        '
        Me.txtSuppCustName.AcceptsReturn = True
        Me.txtSuppCustName.BackColor = System.Drawing.SystemColors.Window
        Me.txtSuppCustName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSuppCustName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSuppCustName.Enabled = False
        Me.txtSuppCustName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSuppCustName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSuppCustName.Location = New System.Drawing.Point(208, 30)
        Me.txtSuppCustName.MaxLength = 0
        Me.txtSuppCustName.Name = "txtSuppCustName"
        Me.txtSuppCustName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSuppCustName.Size = New System.Drawing.Size(353, 20)
        Me.txtSuppCustName.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.txtSuppCustName, "Press F1 For Help")
        '
        'txtModelNo
        '
        Me.txtModelNo.AcceptsReturn = True
        Me.txtModelNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtModelNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModelNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtModelNo.Enabled = False
        Me.txtModelNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtModelNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtModelNo.Location = New System.Drawing.Point(446, 50)
        Me.txtModelNo.MaxLength = 0
        Me.txtModelNo.Name = "txtModelNo"
        Me.txtModelNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtModelNo.Size = New System.Drawing.Size(115, 20)
        Me.txtModelNo.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.txtModelNo, "Press F1 For Help")
        '
        'txtProductDesc
        '
        Me.txtProductDesc.AcceptsReturn = True
        Me.txtProductDesc.BackColor = System.Drawing.SystemColors.Window
        Me.txtProductDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtProductDesc.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtProductDesc.Enabled = False
        Me.txtProductDesc.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProductDesc.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtProductDesc.Location = New System.Drawing.Point(208, 10)
        Me.txtProductDesc.MaxLength = 0
        Me.txtProductDesc.Name = "txtProductDesc"
        Me.txtProductDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProductDesc.Size = New System.Drawing.Size(353, 20)
        Me.txtProductDesc.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.txtProductDesc, "Press F1 For Help")
        '
        'txtSuppCustCode
        '
        Me.txtSuppCustCode.AcceptsReturn = True
        Me.txtSuppCustCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtSuppCustCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSuppCustCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSuppCustCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSuppCustCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSuppCustCode.Location = New System.Drawing.Point(98, 30)
        Me.txtSuppCustCode.MaxLength = 0
        Me.txtSuppCustCode.Name = "txtSuppCustCode"
        Me.txtSuppCustCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSuppCustCode.Size = New System.Drawing.Size(81, 20)
        Me.txtSuppCustCode.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.txtSuppCustCode, "Press F1 For Help")
        '
        'cmdSearchCust
        '
        Me.cmdSearchCust.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchCust.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchCust.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchCust.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchCust.Image = CType(resources.GetObject("cmdSearchCust.Image"), System.Drawing.Image)
        Me.cmdSearchCust.Location = New System.Drawing.Point(180, 30)
        Me.cmdSearchCust.Name = "cmdSearchCust"
        Me.cmdSearchCust.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchCust.Size = New System.Drawing.Size(27, 19)
        Me.cmdSearchCust.TabIndex = 5
        Me.cmdSearchCust.TabStop = False
        Me.cmdSearchCust.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchCust, "Search")
        Me.cmdSearchCust.UseVisualStyleBackColor = False
        '
        'cmdSearchProdCode
        '
        Me.cmdSearchProdCode.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchProdCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchProdCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchProdCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchProdCode.Image = CType(resources.GetObject("cmdSearchProdCode.Image"), System.Drawing.Image)
        Me.cmdSearchProdCode.Location = New System.Drawing.Point(180, 10)
        Me.cmdSearchProdCode.Name = "cmdSearchProdCode"
        Me.cmdSearchProdCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchProdCode.Size = New System.Drawing.Size(27, 19)
        Me.cmdSearchProdCode.TabIndex = 1
        Me.cmdSearchProdCode.TabStop = False
        Me.cmdSearchProdCode.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchProdCode, "Search")
        Me.cmdSearchProdCode.UseVisualStyleBackColor = False
        '
        'txtProductCode
        '
        Me.txtProductCode.AcceptsReturn = True
        Me.txtProductCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtProductCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtProductCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtProductCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProductCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtProductCode.Location = New System.Drawing.Point(98, 10)
        Me.txtProductCode.MaxLength = 0
        Me.txtProductCode.Name = "txtProductCode"
        Me.txtProductCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProductCode.Size = New System.Drawing.Size(81, 20)
        Me.txtProductCode.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.txtProductCode, "Press F1 For Help")
        '
        'cmdSearchWEF
        '
        Me.cmdSearchWEF.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchWEF.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchWEF.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchWEF.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchWEF.Image = CType(resources.GetObject("cmdSearchWEF.Image"), System.Drawing.Image)
        Me.cmdSearchWEF.Location = New System.Drawing.Point(180, 51)
        Me.cmdSearchWEF.Name = "cmdSearchWEF"
        Me.cmdSearchWEF.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchWEF.Size = New System.Drawing.Size(27, 19)
        Me.cmdSearchWEF.TabIndex = 9
        Me.cmdSearchWEF.TabStop = False
        Me.cmdSearchWEF.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchWEF, "Search")
        Me.cmdSearchWEF.UseVisualStyleBackColor = False
        '
        'txtWEF
        '
        Me.txtWEF.AcceptsReturn = True
        Me.txtWEF.BackColor = System.Drawing.SystemColors.Window
        Me.txtWEF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWEF.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWEF.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWEF.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtWEF.Location = New System.Drawing.Point(98, 50)
        Me.txtWEF.MaxLength = 0
        Me.txtWEF.Name = "txtWEF"
        Me.txtWEF.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWEF.Size = New System.Drawing.Size(81, 20)
        Me.txtWEF.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.txtWEF, "Press F1 For Help")
        '
        'cmdSearchPrepBy
        '
        Me.cmdSearchPrepBy.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchPrepBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchPrepBy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchPrepBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchPrepBy.Image = CType(resources.GetObject("cmdSearchPrepBy.Image"), System.Drawing.Image)
        Me.cmdSearchPrepBy.Location = New System.Drawing.Point(158, 31)
        Me.cmdSearchPrepBy.Name = "cmdSearchPrepBy"
        Me.cmdSearchPrepBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchPrepBy.Size = New System.Drawing.Size(23, 17)
        Me.cmdSearchPrepBy.TabIndex = 47
        Me.cmdSearchPrepBy.TabStop = False
        Me.cmdSearchPrepBy.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchPrepBy, "Search")
        Me.cmdSearchPrepBy.UseVisualStyleBackColor = False
        '
        'cmdSearchAppBy
        '
        Me.cmdSearchAppBy.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchAppBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchAppBy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchAppBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchAppBy.Image = CType(resources.GetObject("cmdSearchAppBy.Image"), System.Drawing.Image)
        Me.cmdSearchAppBy.Location = New System.Drawing.Point(530, 31)
        Me.cmdSearchAppBy.Name = "cmdSearchAppBy"
        Me.cmdSearchAppBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchAppBy.Size = New System.Drawing.Size(23, 17)
        Me.cmdSearchAppBy.TabIndex = 49
        Me.cmdSearchAppBy.TabStop = False
        Me.cmdSearchAppBy.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchAppBy, "Search")
        Me.cmdSearchAppBy.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(761, 10)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(78, 34)
        Me.CmdClose.TabIndex = 68
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close the Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'CmdView
        '
        Me.CmdView.BackColor = System.Drawing.SystemColors.Control
        Me.CmdView.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdView.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
        Me.CmdView.Location = New System.Drawing.Point(683, 10)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(78, 34)
        Me.CmdView.TabIndex = 67
        Me.CmdView.Text = "List &View"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdView, "List View")
        Me.CmdView.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(605, 10)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(78, 34)
        Me.CmdPreview.TabIndex = 66
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Print PO")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(527, 10)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(78, 34)
        Me.cmdPrint.TabIndex = 65
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print List")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'CmdDelete
        '
        Me.CmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.CmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdDelete.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdDelete.Image = CType(resources.GetObject("CmdDelete.Image"), System.Drawing.Image)
        Me.CmdDelete.Location = New System.Drawing.Point(449, 10)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(78, 34)
        Me.CmdDelete.TabIndex = 64
        Me.CmdDelete.Text = "&Delete"
        Me.CmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdDelete, "Delete Record")
        Me.CmdDelete.UseVisualStyleBackColor = False
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.Location = New System.Drawing.Point(371, 10)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(78, 34)
        Me.cmdSavePrint.TabIndex = 63
        Me.cmdSavePrint.Text = "Save&&Print"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save and Print the Current Bill")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.Location = New System.Drawing.Point(293, 10)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(78, 34)
        Me.CmdSave.TabIndex = 62
        Me.CmdSave.Text = "&Save"
        Me.CmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSave, "Save Record")
        Me.CmdSave.UseVisualStyleBackColor = False
        '
        'cmdAmend
        '
        Me.cmdAmend.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAmend.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAmend.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAmend.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAmend.Image = CType(resources.GetObject("cmdAmend.Image"), System.Drawing.Image)
        Me.cmdAmend.Location = New System.Drawing.Point(215, 10)
        Me.cmdAmend.Name = "cmdAmend"
        Me.cmdAmend.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAmend.Size = New System.Drawing.Size(78, 34)
        Me.cmdAmend.TabIndex = 85
        Me.cmdAmend.Text = "&Amendment"
        Me.cmdAmend.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdAmend, "Modify Record")
        Me.cmdAmend.UseVisualStyleBackColor = False
        '
        'CmdModify
        '
        Me.CmdModify.BackColor = System.Drawing.SystemColors.Control
        Me.CmdModify.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdModify.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdModify.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdModify.Image = CType(resources.GetObject("CmdModify.Image"), System.Drawing.Image)
        Me.CmdModify.Location = New System.Drawing.Point(137, 10)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(78, 34)
        Me.CmdModify.TabIndex = 61
        Me.CmdModify.Text = "&Modify"
        Me.CmdModify.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdModify, "Modify Record")
        Me.CmdModify.UseVisualStyleBackColor = False
        '
        'CmdAdd
        '
        Me.CmdAdd.BackColor = System.Drawing.SystemColors.Control
        Me.CmdAdd.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdAdd.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdAdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdAdd.Image = CType(resources.GetObject("CmdAdd.Image"), System.Drawing.Image)
        Me.CmdAdd.Location = New System.Drawing.Point(59, 10)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(78, 34)
        Me.CmdAdd.TabIndex = 60
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'fraBase
        '
        Me.fraBase.BackColor = System.Drawing.SystemColors.Control
        Me.fraBase.Controls.Add(Me.Frame1)
        Me.fraBase.Controls.Add(Me.SSTab1)
        Me.fraBase.Controls.Add(Me.fraCosting)
        Me.fraBase.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraBase.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraBase.Location = New System.Drawing.Point(0, -4)
        Me.fraBase.Name = "fraBase"
        Me.fraBase.Padding = New System.Windows.Forms.Padding(0)
        Me.fraBase.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraBase.Size = New System.Drawing.Size(909, 573)
        Me.fraBase.TabIndex = 70
        Me.fraBase.TabStop = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.txtCopyProductCode)
        Me.Frame1.Controls.Add(Me.txtCopyCustCode)
        Me.Frame1.Controls.Add(Me.txtCopyProductDesc)
        Me.Frame1.Controls.Add(Me.cmdSearchCopyProdCode)
        Me.Frame1.Controls.Add(Me.cmdPopulate)
        Me.Frame1.Controls.Add(Me.txtAmendNo)
        Me.Frame1.Controls.Add(Me.txtUnit)
        Me.Frame1.Controls.Add(Me.txtCustPartNo)
        Me.Frame1.Controls.Add(Me.txtSuppCustName)
        Me.Frame1.Controls.Add(Me.txtModelNo)
        Me.Frame1.Controls.Add(Me.txtProductDesc)
        Me.Frame1.Controls.Add(Me.txtSuppCustCode)
        Me.Frame1.Controls.Add(Me.cmdSearchCust)
        Me.Frame1.Controls.Add(Me.cmdSearchProdCode)
        Me.Frame1.Controls.Add(Me.txtProductCode)
        Me.Frame1.Controls.Add(Me.cmdSearchWEF)
        Me.Frame1.Controls.Add(Me.txtWEF)
        Me.Frame1.Controls.Add(Me.Label18)
        Me.Frame1.Controls.Add(Me.Label6)
        Me.Frame1.Controls.Add(Me.lblMKey)
        Me.Frame1.Controls.Add(Me.Label7)
        Me.Frame1.Controls.Add(Me.Label3)
        Me.Frame1.Controls.Add(Me.Label4)
        Me.Frame1.Controls.Add(Me.Label2)
        Me.Frame1.Controls.Add(Me.Label1)
        Me.Frame1.Controls.Add(Me.Label9)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(909, 95)
        Me.Frame1.TabIndex = 76
        Me.Frame1.TabStop = False
        '
        'cmdPopulate
        '
        Me.cmdPopulate.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPopulate.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPopulate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPopulate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPopulate.Location = New System.Drawing.Point(616, 52)
        Me.cmdPopulate.Name = "cmdPopulate"
        Me.cmdPopulate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPopulate.Size = New System.Drawing.Size(127, 21)
        Me.cmdPopulate.TabIndex = 16
        Me.cmdPopulate.Text = "Populate"
        Me.cmdPopulate.UseVisualStyleBackColor = False
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(21, 73)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(73, 14)
        Me.Label18.TabIndex = 120
        Me.Label18.Text = "Copy From :"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(212, 52)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(70, 14)
        Me.Label6.TabIndex = 84
        Me.Label6.Text = "Amend No :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMKey
        '
        Me.lblMKey.AutoSize = True
        Me.lblMKey.BackColor = System.Drawing.SystemColors.Control
        Me.lblMKey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMKey.Enabled = False
        Me.lblMKey.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMKey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMKey.Location = New System.Drawing.Point(700, 36)
        Me.lblMKey.Name = "lblMKey"
        Me.lblMKey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMKey.Size = New System.Drawing.Size(44, 14)
        Me.lblMKey.TabIndex = 83
        Me.lblMKey.Text = "lblMKey"
        Me.lblMKey.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(578, 14)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(34, 14)
        Me.Label7.TabIndex = 82
        Me.Label7.Text = "Unit :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(25, 32)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(69, 14)
        Me.Label3.TabIndex = 81
        Me.Label3.Text = "Customer :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(562, 32)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(52, 14)
        Me.Label4.TabIndex = 80
        Me.Label4.Text = "Part No :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(381, 52)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(64, 14)
        Me.Label2.TabIndex = 79
        Me.Label2.Text = "Model No :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(6, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(88, 14)
        Me.Label1.TabIndex = 78
        Me.Label1.Text = "Product Code :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(52, 52)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(42, 14)
        Me.Label9.TabIndex = 77
        Me.Label9.Text = "W.E.F. :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'SSTab1
        '
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage0)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage1)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage2)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage3)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage4)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage5)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage6)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage7)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage8)
        Me.SSTab1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SSTab1.ItemSize = New System.Drawing.Size(42, 24)
        Me.SSTab1.Location = New System.Drawing.Point(2, 98)
        Me.SSTab1.Name = "SSTab1"
        Me.SSTab1.SelectedIndex = 1
        Me.SSTab1.Size = New System.Drawing.Size(905, 418)
        Me.SSTab1.TabIndex = 59
        '
        '_SSTab1_TabPage0
        '
        Me._SSTab1_TabPage0.Controls.Add(Me.Frame2)
        Me._SSTab1_TabPage0.Controls.Add(Me.Frame4)
        Me._SSTab1_TabPage0.Location = New System.Drawing.Point(4, 28)
        Me._SSTab1_TabPage0.Name = "_SSTab1_TabPage0"
        Me._SSTab1_TabPage0.Size = New System.Drawing.Size(897, 386)
        Me._SSTab1_TabPage0.TabIndex = 0
        Me._SSTab1_TabPage0.Text = "Summary"
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.txtTransportCost)
        Me.Frame2.Controls.Add(Me.txtTotWeldCost)
        Me.Frame2.Controls.Add(Me.txtOverheadCost)
        Me.Frame2.Controls.Add(Me.txtOverheadPer)
        Me.Frame2.Controls.Add(Me.txtTotProdCost)
        Me.Frame2.Controls.Add(Me.txtTotValueAdd)
        Me.Frame2.Controls.Add(Me.txtTotPdrCost)
        Me.Frame2.Controls.Add(Me.txtTotPntCost)
        Me.Frame2.Controls.Add(Me.txtTotPltCost)
        Me.Frame2.Controls.Add(Me.txtTotProcessCost)
        Me.Frame2.Controls.Add(Me.txtTotBOPCost)
        Me.Frame2.Controls.Add(Me.txtTotRMCost)
        Me.Frame2.Controls.Add(Me.Label17)
        Me.Frame2.Controls.Add(Me.Label43)
        Me.Frame2.Controls.Add(Me.Label56)
        Me.Frame2.Controls.Add(Me.Label52)
        Me.Frame2.Controls.Add(Me.Label51)
        Me.Frame2.Controls.Add(Me.lblPackMaterialCost)
        Me.Frame2.Controls.Add(Me.lblInterest)
        Me.Frame2.Controls.Add(Me.lblToolCost)
        Me.Frame2.Controls.Add(Me.lblHandlingCode)
        Me.Frame2.Controls.Add(Me.lblOperationCost)
        Me.Frame2.Controls.Add(Me.Label37)
        Me.Frame2.Controls.Add(Me.Label28)
        Me.Frame2.Controls.Add(Me.Label11)
        Me.Frame2.Controls.Add(Me.Label10)
        Me.Frame2.Controls.Add(Me.Label8)
        Me.Frame2.Controls.Add(Me.Label36)
        Me.Frame2.Controls.Add(Me.Label32)
        Me.Frame2.Controls.Add(Me.Label31)
        Me.Frame2.Controls.Add(Me.Label30)
        Me.Frame2.Controls.Add(Me.Label29)
        Me.Frame2.Controls.Add(Me.Label27)
        Me.Frame2.Controls.Add(Me.Label26)
        Me.Frame2.Controls.Add(Me.Label25)
        Me.Frame2.Controls.Add(Me.Label24)
        Me.Frame2.Controls.Add(Me.Label23)
        Me.Frame2.Controls.Add(Me.Label22)
        Me.Frame2.Controls.Add(Me.Label21)
        Me.Frame2.Controls.Add(Me.Label20)
        Me.Frame2.Controls.Add(Me.Label19)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(3, -3)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(368, 334)
        Me.Frame2.TabIndex = 86
        Me.Frame2.TabStop = False
        '
        'txtTransportCost
        '
        Me.txtTransportCost.AcceptsReturn = True
        Me.txtTransportCost.BackColor = System.Drawing.SystemColors.Window
        Me.txtTransportCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTransportCost.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTransportCost.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransportCost.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTransportCost.Location = New System.Drawing.Point(281, 236)
        Me.txtTransportCost.MaxLength = 0
        Me.txtTransportCost.Name = "txtTransportCost"
        Me.txtTransportCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTransportCost.Size = New System.Drawing.Size(75, 20)
        Me.txtTransportCost.TabIndex = 29
        Me.txtTransportCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotWeldCost
        '
        Me.txtTotWeldCost.AcceptsReturn = True
        Me.txtTotWeldCost.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotWeldCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotWeldCost.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotWeldCost.Enabled = False
        Me.txtTotWeldCost.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotWeldCost.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotWeldCost.Location = New System.Drawing.Point(281, 52)
        Me.txtTotWeldCost.MaxLength = 0
        Me.txtTotWeldCost.Name = "txtTotWeldCost"
        Me.txtTotWeldCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotWeldCost.Size = New System.Drawing.Size(75, 20)
        Me.txtTotWeldCost.TabIndex = 19
        Me.txtTotWeldCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtOverheadCost
        '
        Me.txtOverheadCost.AcceptsReturn = True
        Me.txtOverheadCost.BackColor = System.Drawing.SystemColors.Window
        Me.txtOverheadCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOverheadCost.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOverheadCost.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOverheadCost.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOverheadCost.Location = New System.Drawing.Point(281, 215)
        Me.txtOverheadCost.MaxLength = 0
        Me.txtOverheadCost.Name = "txtOverheadCost"
        Me.txtOverheadCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOverheadCost.Size = New System.Drawing.Size(75, 20)
        Me.txtOverheadCost.TabIndex = 28
        Me.txtOverheadCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtOverheadPer
        '
        Me.txtOverheadPer.AcceptsReturn = True
        Me.txtOverheadPer.BackColor = System.Drawing.SystemColors.Window
        Me.txtOverheadPer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOverheadPer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOverheadPer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOverheadPer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOverheadPer.Location = New System.Drawing.Point(199, 215)
        Me.txtOverheadPer.MaxLength = 0
        Me.txtOverheadPer.Name = "txtOverheadPer"
        Me.txtOverheadPer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOverheadPer.Size = New System.Drawing.Size(51, 20)
        Me.txtOverheadPer.TabIndex = 27
        Me.txtOverheadPer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotProdCost
        '
        Me.txtTotProdCost.AcceptsReturn = True
        Me.txtTotProdCost.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotProdCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotProdCost.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotProdCost.Enabled = False
        Me.txtTotProdCost.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotProdCost.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotProdCost.Location = New System.Drawing.Point(281, 195)
        Me.txtTotProdCost.MaxLength = 0
        Me.txtTotProdCost.Name = "txtTotProdCost"
        Me.txtTotProdCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotProdCost.Size = New System.Drawing.Size(75, 20)
        Me.txtTotProdCost.TabIndex = 26
        Me.txtTotProdCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotValueAdd
        '
        Me.txtTotValueAdd.AcceptsReturn = True
        Me.txtTotValueAdd.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotValueAdd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotValueAdd.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotValueAdd.Enabled = False
        Me.txtTotValueAdd.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotValueAdd.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotValueAdd.Location = New System.Drawing.Point(281, 175)
        Me.txtTotValueAdd.MaxLength = 0
        Me.txtTotValueAdd.Name = "txtTotValueAdd"
        Me.txtTotValueAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotValueAdd.Size = New System.Drawing.Size(75, 20)
        Me.txtTotValueAdd.TabIndex = 25
        Me.txtTotValueAdd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotPdrCost
        '
        Me.txtTotPdrCost.AcceptsReturn = True
        Me.txtTotPdrCost.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotPdrCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotPdrCost.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotPdrCost.Enabled = False
        Me.txtTotPdrCost.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotPdrCost.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotPdrCost.Location = New System.Drawing.Point(281, 155)
        Me.txtTotPdrCost.MaxLength = 0
        Me.txtTotPdrCost.Name = "txtTotPdrCost"
        Me.txtTotPdrCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotPdrCost.Size = New System.Drawing.Size(75, 20)
        Me.txtTotPdrCost.TabIndex = 24
        Me.txtTotPdrCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotPntCost
        '
        Me.txtTotPntCost.AcceptsReturn = True
        Me.txtTotPntCost.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotPntCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotPntCost.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotPntCost.Enabled = False
        Me.txtTotPntCost.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotPntCost.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotPntCost.Location = New System.Drawing.Point(281, 135)
        Me.txtTotPntCost.MaxLength = 0
        Me.txtTotPntCost.Name = "txtTotPntCost"
        Me.txtTotPntCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotPntCost.Size = New System.Drawing.Size(75, 20)
        Me.txtTotPntCost.TabIndex = 23
        Me.txtTotPntCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotPltCost
        '
        Me.txtTotPltCost.AcceptsReturn = True
        Me.txtTotPltCost.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotPltCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotPltCost.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotPltCost.Enabled = False
        Me.txtTotPltCost.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotPltCost.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotPltCost.Location = New System.Drawing.Point(281, 115)
        Me.txtTotPltCost.MaxLength = 0
        Me.txtTotPltCost.Name = "txtTotPltCost"
        Me.txtTotPltCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotPltCost.Size = New System.Drawing.Size(75, 20)
        Me.txtTotPltCost.TabIndex = 22
        Me.txtTotPltCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotProcessCost
        '
        Me.txtTotProcessCost.AcceptsReturn = True
        Me.txtTotProcessCost.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotProcessCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotProcessCost.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotProcessCost.Enabled = False
        Me.txtTotProcessCost.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotProcessCost.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotProcessCost.Location = New System.Drawing.Point(281, 73)
        Me.txtTotProcessCost.MaxLength = 0
        Me.txtTotProcessCost.Name = "txtTotProcessCost"
        Me.txtTotProcessCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotProcessCost.Size = New System.Drawing.Size(75, 20)
        Me.txtTotProcessCost.TabIndex = 20
        Me.txtTotProcessCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotBOPCost
        '
        Me.txtTotBOPCost.AcceptsReturn = True
        Me.txtTotBOPCost.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotBOPCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotBOPCost.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotBOPCost.Enabled = False
        Me.txtTotBOPCost.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotBOPCost.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotBOPCost.Location = New System.Drawing.Point(281, 31)
        Me.txtTotBOPCost.MaxLength = 0
        Me.txtTotBOPCost.Name = "txtTotBOPCost"
        Me.txtTotBOPCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotBOPCost.Size = New System.Drawing.Size(75, 20)
        Me.txtTotBOPCost.TabIndex = 18
        Me.txtTotBOPCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotRMCost
        '
        Me.txtTotRMCost.AcceptsReturn = True
        Me.txtTotRMCost.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotRMCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotRMCost.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotRMCost.Enabled = False
        Me.txtTotRMCost.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotRMCost.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotRMCost.Location = New System.Drawing.Point(281, 11)
        Me.txtTotRMCost.MaxLength = 0
        Me.txtTotRMCost.Name = "txtTotRMCost"
        Me.txtTotRMCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotRMCost.Size = New System.Drawing.Size(75, 20)
        Me.txtTotRMCost.TabIndex = 17
        Me.txtTotRMCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(8, 236)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(27, 14)
        Me.Label17.TabIndex = 138
        Me.Label17.Text = "VIII."
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.BackColor = System.Drawing.SystemColors.Control
        Me.Label43.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label43.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label43.Location = New System.Drawing.Point(40, 236)
        Me.Label43.Name = "Label43"
        Me.Label43.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label43.Size = New System.Drawing.Size(123, 14)
        Me.Label43.TabIndex = 137
        Me.Label43.Text = "Transportation Cost :"
        '
        'Label56
        '
        Me.Label56.AutoSize = True
        Me.Label56.BackColor = System.Drawing.SystemColors.Control
        Me.Label56.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label56.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label56.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label56.Location = New System.Drawing.Point(252, 218)
        Me.Label56.Name = "Label56"
        Me.Label56.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label56.Size = New System.Drawing.Size(16, 14)
        Me.Label56.TabIndex = 132
        Me.Label56.Text = "%"
        '
        'Label52
        '
        Me.Label52.AutoSize = True
        Me.Label52.BackColor = System.Drawing.SystemColors.Control
        Me.Label52.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label52.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label52.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label52.Location = New System.Drawing.Point(40, 55)
        Me.Label52.Name = "Label52"
        Me.Label52.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label52.Size = New System.Drawing.Size(86, 14)
        Me.Label52.TabIndex = 128
        Me.Label52.Text = "Welding Cost :"
        '
        'Label51
        '
        Me.Label51.AutoSize = True
        Me.Label51.BackColor = System.Drawing.SystemColors.Control
        Me.Label51.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label51.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label51.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label51.Location = New System.Drawing.Point(8, 55)
        Me.Label51.Name = "Label51"
        Me.Label51.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label51.Size = New System.Drawing.Size(19, 14)
        Me.Label51.TabIndex = 127
        Me.Label51.Text = "III."
        '
        'lblPackMaterialCost
        '
        Me.lblPackMaterialCost.BackColor = System.Drawing.SystemColors.Control
        Me.lblPackMaterialCost.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPackMaterialCost.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPackMaterialCost.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPackMaterialCost.Location = New System.Drawing.Point(210, 166)
        Me.lblPackMaterialCost.Name = "lblPackMaterialCost"
        Me.lblPackMaterialCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPackMaterialCost.Size = New System.Drawing.Size(43, 17)
        Me.lblPackMaterialCost.TabIndex = 124
        Me.lblPackMaterialCost.Text = "0"
        Me.lblPackMaterialCost.Visible = False
        '
        'lblInterest
        '
        Me.lblInterest.BackColor = System.Drawing.SystemColors.Control
        Me.lblInterest.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblInterest.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInterest.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInterest.Location = New System.Drawing.Point(200, 142)
        Me.lblInterest.Name = "lblInterest"
        Me.lblInterest.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblInterest.Size = New System.Drawing.Size(79, 13)
        Me.lblInterest.TabIndex = 123
        Me.lblInterest.Text = "0"
        Me.lblInterest.Visible = False
        '
        'lblToolCost
        '
        Me.lblToolCost.BackColor = System.Drawing.SystemColors.Control
        Me.lblToolCost.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblToolCost.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToolCost.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblToolCost.Location = New System.Drawing.Point(164, 118)
        Me.lblToolCost.Name = "lblToolCost"
        Me.lblToolCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblToolCost.Size = New System.Drawing.Size(105, 11)
        Me.lblToolCost.TabIndex = 122
        Me.lblToolCost.Text = "0"
        Me.lblToolCost.Visible = False
        '
        'lblHandlingCode
        '
        Me.lblHandlingCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblHandlingCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblHandlingCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHandlingCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblHandlingCode.Location = New System.Drawing.Point(160, 98)
        Me.lblHandlingCode.Name = "lblHandlingCode"
        Me.lblHandlingCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblHandlingCode.Size = New System.Drawing.Size(99, 13)
        Me.lblHandlingCode.TabIndex = 121
        Me.lblHandlingCode.Text = "0"
        Me.lblHandlingCode.Visible = False
        '
        'lblOperationCost
        '
        Me.lblOperationCost.BackColor = System.Drawing.SystemColors.Control
        Me.lblOperationCost.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblOperationCost.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblOperationCost.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOperationCost.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblOperationCost.Location = New System.Drawing.Point(282, 92)
        Me.lblOperationCost.Name = "lblOperationCost"
        Me.lblOperationCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblOperationCost.Size = New System.Drawing.Size(74, 17)
        Me.lblOperationCost.TabIndex = 21
        Me.lblOperationCost.Text = "0"
        Me.lblOperationCost.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.BackColor = System.Drawing.SystemColors.Control
        Me.Label37.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label37.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label37.Location = New System.Drawing.Point(8, 218)
        Me.Label37.Name = "Label37"
        Me.Label37.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label37.Size = New System.Drawing.Size(24, 14)
        Me.Label37.TabIndex = 105
        Me.Label37.Text = "VII."
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.SystemColors.Control
        Me.Label28.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label28.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label28.Location = New System.Drawing.Point(40, 218)
        Me.Label28.Name = "Label28"
        Me.Label28.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label28.Size = New System.Drawing.Size(113, 14)
        Me.Label28.TabIndex = 104
        Me.Label28.Text = "Overhead Amount :"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(40, 158)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(18, 14)
        Me.Label11.TabIndex = 103
        Me.Label11.Text = "C."
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(40, 138)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(17, 14)
        Me.Label10.TabIndex = 102
        Me.Label10.Text = "B."
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(40, 118)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(18, 14)
        Me.Label8.TabIndex = 101
        Me.Label8.Text = "A."
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.BackColor = System.Drawing.SystemColors.Control
        Me.Label36.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label36.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label36.Location = New System.Drawing.Point(8, 198)
        Me.Label36.Name = "Label36"
        Me.Label36.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label36.Size = New System.Drawing.Size(21, 14)
        Me.Label36.TabIndex = 100
        Me.Label36.Text = "VI."
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.BackColor = System.Drawing.SystemColors.Control
        Me.Label32.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label32.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label32.Location = New System.Drawing.Point(8, 102)
        Me.Label32.Name = "Label32"
        Me.Label32.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label32.Size = New System.Drawing.Size(17, 14)
        Me.Label32.TabIndex = 99
        Me.Label32.Text = "V."
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.BackColor = System.Drawing.SystemColors.Control
        Me.Label31.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label31.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label31.Location = New System.Drawing.Point(8, 76)
        Me.Label31.Name = "Label31"
        Me.Label31.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label31.Size = New System.Drawing.Size(20, 14)
        Me.Label31.TabIndex = 98
        Me.Label31.Text = "IV."
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.BackColor = System.Drawing.SystemColors.Control
        Me.Label30.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label30.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label30.Location = New System.Drawing.Point(8, 34)
        Me.Label30.Name = "Label30"
        Me.Label30.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label30.Size = New System.Drawing.Size(16, 14)
        Me.Label30.TabIndex = 97
        Me.Label30.Text = "II."
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.SystemColors.Control
        Me.Label29.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label29.Location = New System.Drawing.Point(40, 14)
        Me.Label29.Name = "Label29"
        Me.Label29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label29.Size = New System.Drawing.Size(112, 14)
        Me.Label29.TabIndex = 96
        Me.Label29.Text = "Raw Material Cost :"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.SystemColors.Control
        Me.Label27.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label27.Location = New System.Drawing.Point(40, 198)
        Me.Label27.Name = "Label27"
        Me.Label27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label27.Size = New System.Drawing.Size(158, 14)
        Me.Label27.TabIndex = 95
        Me.Label27.Text = "Cost of Production (I to IV) :"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.SystemColors.Control
        Me.Label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(40, 178)
        Me.Label26.Name = "Label26"
        Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label26.Size = New System.Drawing.Size(121, 14)
        Me.Label26.TabIndex = 94
        Me.Label26.Text = "Total Value Addition :"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.SystemColors.Control
        Me.Label25.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label25.Location = New System.Drawing.Point(64, 158)
        Me.Label25.Name = "Label25"
        Me.Label25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label25.Size = New System.Drawing.Size(130, 14)
        Me.Label25.TabIndex = 93
        Me.Label25.Text = "Powder Coating Cost :"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.SystemColors.Control
        Me.Label24.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label24.Location = New System.Drawing.Point(64, 138)
        Me.Label24.Name = "Label24"
        Me.Label24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label24.Size = New System.Drawing.Size(86, 14)
        Me.Label24.TabIndex = 92
        Me.Label24.Text = "Painting Cost :"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.SystemColors.Control
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(64, 118)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(79, 14)
        Me.Label23.TabIndex = 91
        Me.Label23.Text = "Plating Cost :"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.SystemColors.Control
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(40, 102)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(86, 14)
        Me.Label22.TabIndex = 90
        Me.Label22.Text = "Value Addition"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.SystemColors.Control
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(40, 76)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(88, 14)
        Me.Label21.TabIndex = 89
        Me.Label21.Text = "Process Cost :"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(40, 34)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(99, 14)
        Me.Label20.TabIndex = 88
        Me.Label20.Text = "BOP Items Cost :"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(8, 14)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(13, 14)
        Me.Label19.TabIndex = 87
        Me.Label19.Text = "I."
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.txtCostReduction)
        Me.Frame4.Controls.Add(Me.Label62)
        Me.Frame4.Controls.Add(Me.Label63)
        Me.Frame4.Controls.Add(Me.txtToolCostPerPc)
        Me.Frame4.Controls.Add(Me.Label61)
        Me.Frame4.Controls.Add(Me.txtToolQty)
        Me.Frame4.Controls.Add(Me.Label60)
        Me.Frame4.Controls.Add(Me.txtPMCost)
        Me.Frame4.Controls.Add(Me.txtICC)
        Me.Frame4.Controls.Add(Me.txtHandling)
        Me.Frame4.Controls.Add(Me.txtToolCost)
        Me.Frame4.Controls.Add(Me.txtDiscount)
        Me.Frame4.Controls.Add(Me.txtTotSaleCost)
        Me.Frame4.Controls.Add(Me.txtRejPer)
        Me.Frame4.Controls.Add(Me.txtRejCost)
        Me.Frame4.Controls.Add(Me.txtProfitPer)
        Me.Frame4.Controls.Add(Me.txtProfitCost)
        Me.Frame4.Controls.Add(Me.txtTotSalePrice)
        Me.Frame4.Controls.Add(Me.txtTotPriceSettelled)
        Me.Frame4.Controls.Add(Me.txtCustPONo)
        Me.Frame4.Controls.Add(Me.txtCustPODate)
        Me.Frame4.Controls.Add(Me.Label59)
        Me.Frame4.Controls.Add(Me.Label58)
        Me.Frame4.Controls.Add(Me.Label57)
        Me.Frame4.Controls.Add(Me.Label55)
        Me.Frame4.Controls.Add(Me.Label54)
        Me.Frame4.Controls.Add(Me.Label44)
        Me.Frame4.Controls.Add(Me.Label53)
        Me.Frame4.Controls.Add(Me.Label50)
        Me.Frame4.Controls.Add(Me.Label38)
        Me.Frame4.Controls.Add(Me.Label46)
        Me.Frame4.Controls.Add(Me.Label48)
        Me.Frame4.Controls.Add(Me.Label47)
        Me.Frame4.Controls.Add(Me.Label45)
        Me.Frame4.Controls.Add(Me.Label42)
        Me.Frame4.Controls.Add(Me.Label41)
        Me.Frame4.Controls.Add(Me.Label39)
        Me.Frame4.Controls.Add(Me.Label35)
        Me.Frame4.Controls.Add(Me.Label34)
        Me.Frame4.Controls.Add(Me.Label33)
        Me.Frame4.Controls.Add(Me.Label16)
        Me.Frame4.Controls.Add(Me.Label15)
        Me.Frame4.Controls.Add(Me.Label12)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(373, -3)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(527, 334)
        Me.Frame4.TabIndex = 106
        Me.Frame4.TabStop = False
        '
        'txtPMCost
        '
        Me.txtPMCost.AcceptsReturn = True
        Me.txtPMCost.BackColor = System.Drawing.SystemColors.Window
        Me.txtPMCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPMCost.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPMCost.Enabled = False
        Me.txtPMCost.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPMCost.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPMCost.Location = New System.Drawing.Point(281, 50)
        Me.txtPMCost.MaxLength = 0
        Me.txtPMCost.Name = "txtPMCost"
        Me.txtPMCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPMCost.Size = New System.Drawing.Size(75, 20)
        Me.txtPMCost.TabIndex = 32
        Me.txtPMCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtICC
        '
        Me.txtICC.AcceptsReturn = True
        Me.txtICC.BackColor = System.Drawing.SystemColors.Window
        Me.txtICC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtICC.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtICC.Enabled = False
        Me.txtICC.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtICC.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtICC.Location = New System.Drawing.Point(281, 10)
        Me.txtICC.MaxLength = 0
        Me.txtICC.Name = "txtICC"
        Me.txtICC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtICC.Size = New System.Drawing.Size(75, 20)
        Me.txtICC.TabIndex = 30
        Me.txtICC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtHandling
        '
        Me.txtHandling.AcceptsReturn = True
        Me.txtHandling.BackColor = System.Drawing.SystemColors.Window
        Me.txtHandling.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtHandling.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtHandling.Enabled = False
        Me.txtHandling.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHandling.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtHandling.Location = New System.Drawing.Point(281, 30)
        Me.txtHandling.MaxLength = 0
        Me.txtHandling.Name = "txtHandling"
        Me.txtHandling.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtHandling.Size = New System.Drawing.Size(75, 20)
        Me.txtHandling.TabIndex = 31
        Me.txtHandling.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtToolCost
        '
        Me.txtToolCost.AcceptsReturn = True
        Me.txtToolCost.BackColor = System.Drawing.SystemColors.Window
        Me.txtToolCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtToolCost.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtToolCost.Enabled = False
        Me.txtToolCost.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToolCost.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtToolCost.Location = New System.Drawing.Point(281, 70)
        Me.txtToolCost.MaxLength = 0
        Me.txtToolCost.Name = "txtToolCost"
        Me.txtToolCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtToolCost.Size = New System.Drawing.Size(75, 20)
        Me.txtToolCost.TabIndex = 33
        Me.txtToolCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDiscount
        '
        Me.txtDiscount.AcceptsReturn = True
        Me.txtDiscount.BackColor = System.Drawing.SystemColors.Window
        Me.txtDiscount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDiscount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDiscount.Enabled = False
        Me.txtDiscount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDiscount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDiscount.Location = New System.Drawing.Point(281, 215)
        Me.txtDiscount.MaxLength = 0
        Me.txtDiscount.Name = "txtDiscount"
        Me.txtDiscount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDiscount.Size = New System.Drawing.Size(75, 20)
        Me.txtDiscount.TabIndex = 41
        Me.txtDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotSaleCost
        '
        Me.txtTotSaleCost.AcceptsReturn = True
        Me.txtTotSaleCost.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotSaleCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotSaleCost.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotSaleCost.Enabled = False
        Me.txtTotSaleCost.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotSaleCost.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotSaleCost.Location = New System.Drawing.Point(281, 111)
        Me.txtTotSaleCost.MaxLength = 0
        Me.txtTotSaleCost.Name = "txtTotSaleCost"
        Me.txtTotSaleCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotSaleCost.Size = New System.Drawing.Size(75, 20)
        Me.txtTotSaleCost.TabIndex = 36
        Me.txtTotSaleCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtRejPer
        '
        Me.txtRejPer.AcceptsReturn = True
        Me.txtRejPer.BackColor = System.Drawing.SystemColors.Window
        Me.txtRejPer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRejPer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRejPer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRejPer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRejPer.Location = New System.Drawing.Point(217, 91)
        Me.txtRejPer.MaxLength = 0
        Me.txtRejPer.Name = "txtRejPer"
        Me.txtRejPer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRejPer.Size = New System.Drawing.Size(43, 20)
        Me.txtRejPer.TabIndex = 34
        Me.txtRejPer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtRejCost
        '
        Me.txtRejCost.AcceptsReturn = True
        Me.txtRejCost.BackColor = System.Drawing.SystemColors.Window
        Me.txtRejCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRejCost.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRejCost.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRejCost.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRejCost.Location = New System.Drawing.Point(281, 91)
        Me.txtRejCost.MaxLength = 0
        Me.txtRejCost.Name = "txtRejCost"
        Me.txtRejCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRejCost.Size = New System.Drawing.Size(75, 20)
        Me.txtRejCost.TabIndex = 35
        Me.txtRejCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtProfitPer
        '
        Me.txtProfitPer.AcceptsReturn = True
        Me.txtProfitPer.BackColor = System.Drawing.SystemColors.Window
        Me.txtProfitPer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtProfitPer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtProfitPer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProfitPer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtProfitPer.Location = New System.Drawing.Point(217, 131)
        Me.txtProfitPer.MaxLength = 0
        Me.txtProfitPer.Name = "txtProfitPer"
        Me.txtProfitPer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProfitPer.Size = New System.Drawing.Size(43, 20)
        Me.txtProfitPer.TabIndex = 37
        Me.txtProfitPer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtProfitCost
        '
        Me.txtProfitCost.AcceptsReturn = True
        Me.txtProfitCost.BackColor = System.Drawing.SystemColors.Window
        Me.txtProfitCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtProfitCost.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtProfitCost.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProfitCost.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtProfitCost.Location = New System.Drawing.Point(281, 131)
        Me.txtProfitCost.MaxLength = 0
        Me.txtProfitCost.Name = "txtProfitCost"
        Me.txtProfitCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProfitCost.Size = New System.Drawing.Size(75, 20)
        Me.txtProfitCost.TabIndex = 38
        Me.txtProfitCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotSalePrice
        '
        Me.txtTotSalePrice.AcceptsReturn = True
        Me.txtTotSalePrice.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotSalePrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotSalePrice.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotSalePrice.Enabled = False
        Me.txtTotSalePrice.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotSalePrice.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotSalePrice.Location = New System.Drawing.Point(281, 174)
        Me.txtTotSalePrice.MaxLength = 0
        Me.txtTotSalePrice.Name = "txtTotSalePrice"
        Me.txtTotSalePrice.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotSalePrice.Size = New System.Drawing.Size(75, 20)
        Me.txtTotSalePrice.TabIndex = 39
        Me.txtTotSalePrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotPriceSettelled
        '
        Me.txtTotPriceSettelled.AcceptsReturn = True
        Me.txtTotPriceSettelled.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotPriceSettelled.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotPriceSettelled.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotPriceSettelled.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotPriceSettelled.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotPriceSettelled.Location = New System.Drawing.Point(281, 195)
        Me.txtTotPriceSettelled.MaxLength = 0
        Me.txtTotPriceSettelled.Name = "txtTotPriceSettelled"
        Me.txtTotPriceSettelled.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotPriceSettelled.Size = New System.Drawing.Size(75, 20)
        Me.txtTotPriceSettelled.TabIndex = 40
        Me.txtTotPriceSettelled.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCustPONo
        '
        Me.txtCustPONo.AcceptsReturn = True
        Me.txtCustPONo.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustPONo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustPONo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustPONo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustPONo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCustPONo.Location = New System.Drawing.Point(200, 235)
        Me.txtCustPONo.MaxLength = 0
        Me.txtCustPONo.Name = "txtCustPONo"
        Me.txtCustPONo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustPONo.Size = New System.Drawing.Size(75, 20)
        Me.txtCustPONo.TabIndex = 42
        '
        'txtCustPODate
        '
        Me.txtCustPODate.AcceptsReturn = True
        Me.txtCustPODate.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustPODate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustPODate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustPODate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustPODate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCustPODate.Location = New System.Drawing.Point(281, 235)
        Me.txtCustPODate.MaxLength = 0
        Me.txtCustPODate.Name = "txtCustPODate"
        Me.txtCustPODate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustPODate.Size = New System.Drawing.Size(75, 20)
        Me.txtCustPODate.TabIndex = 43
        '
        'Label59
        '
        Me.Label59.AutoSize = True
        Me.Label59.BackColor = System.Drawing.SystemColors.Control
        Me.Label59.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label59.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label59.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label59.Location = New System.Drawing.Point(40, 50)
        Me.Label59.Name = "Label59"
        Me.Label59.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label59.Size = New System.Drawing.Size(132, 14)
        Me.Label59.TabIndex = 136
        Me.Label59.Text = "Packing Material Cost :"
        '
        'Label58
        '
        Me.Label58.AutoSize = True
        Me.Label58.BackColor = System.Drawing.SystemColors.Control
        Me.Label58.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label58.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label58.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label58.Location = New System.Drawing.Point(8, 50)
        Me.Label58.Name = "Label58"
        Me.Label58.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label58.Size = New System.Drawing.Size(20, 14)
        Me.Label58.TabIndex = 135
        Me.Label58.Text = "XI."
        '
        'Label57
        '
        Me.Label57.AutoSize = True
        Me.Label57.BackColor = System.Drawing.SystemColors.Control
        Me.Label57.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label57.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label57.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label57.Location = New System.Drawing.Point(40, 10)
        Me.Label57.Name = "Label57"
        Me.Label57.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label57.Size = New System.Drawing.Size(141, 14)
        Me.Label57.TabIndex = 134
        Me.Label57.Text = "Inventory Carrying Cost:"
        '
        'Label55
        '
        Me.Label55.AutoSize = True
        Me.Label55.BackColor = System.Drawing.SystemColors.Control
        Me.Label55.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label55.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label55.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label55.Location = New System.Drawing.Point(8, 10)
        Me.Label55.Name = "Label55"
        Me.Label55.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label55.Size = New System.Drawing.Size(20, 14)
        Me.Label55.TabIndex = 133
        Me.Label55.Text = "IX."
        '
        'Label54
        '
        Me.Label54.AutoSize = True
        Me.Label54.BackColor = System.Drawing.SystemColors.Control
        Me.Label54.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label54.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label54.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label54.Location = New System.Drawing.Point(8, 30)
        Me.Label54.Name = "Label54"
        Me.Label54.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label54.Size = New System.Drawing.Size(17, 14)
        Me.Label54.TabIndex = 131
        Me.Label54.Text = "X."
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.BackColor = System.Drawing.SystemColors.Control
        Me.Label44.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label44.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label44.Location = New System.Drawing.Point(40, 30)
        Me.Label44.Name = "Label44"
        Me.Label44.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label44.Size = New System.Drawing.Size(114, 14)
        Me.Label44.TabIndex = 130
        Me.Label44.Text = "BOP Handling Cost :"
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.BackColor = System.Drawing.SystemColors.Control
        Me.Label53.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label53.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label53.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label53.Location = New System.Drawing.Point(260, 134)
        Me.Label53.Name = "Label53"
        Me.Label53.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label53.Size = New System.Drawing.Size(16, 14)
        Me.Label53.TabIndex = 129
        Me.Label53.Text = "%"
        '
        'Label50
        '
        Me.Label50.AutoSize = True
        Me.Label50.BackColor = System.Drawing.SystemColors.Control
        Me.Label50.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label50.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label50.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label50.Location = New System.Drawing.Point(8, 175)
        Me.Label50.Name = "Label50"
        Me.Label50.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label50.Size = New System.Drawing.Size(31, 14)
        Me.Label50.TabIndex = 126
        Me.Label50.Text = "XVII."
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.BackColor = System.Drawing.SystemColors.Control
        Me.Label38.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label38.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label38.Location = New System.Drawing.Point(40, 70)
        Me.Label38.Name = "Label38"
        Me.Label38.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label38.Size = New System.Drawing.Size(65, 14)
        Me.Label38.TabIndex = 125
        Me.Label38.Text = "Tool Cost :"
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.BackColor = System.Drawing.SystemColors.Control
        Me.Label46.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label46.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label46.Location = New System.Drawing.Point(40, 111)
        Me.Label46.Name = "Label46"
        Me.Label46.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label46.Size = New System.Drawing.Size(139, 14)
        Me.Label46.TabIndex = 119
        Me.Label46.Text = "Cost of Sales (V to VIII) :"
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.BackColor = System.Drawing.SystemColors.Control
        Me.Label48.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label48.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label48.Location = New System.Drawing.Point(40, 91)
        Me.Label48.Name = "Label48"
        Me.Label48.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label48.Size = New System.Drawing.Size(168, 14)
        Me.Label48.TabIndex = 118
        Me.Label48.Text = "Rejection && Rework Amount :"
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.BackColor = System.Drawing.SystemColors.Control
        Me.Label47.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label47.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label47.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label47.Location = New System.Drawing.Point(262, 94)
        Me.Label47.Name = "Label47"
        Me.Label47.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label47.Size = New System.Drawing.Size(16, 14)
        Me.Label47.TabIndex = 117
        Me.Label47.Text = "%"
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.BackColor = System.Drawing.SystemColors.Control
        Me.Label45.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label45.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label45.Location = New System.Drawing.Point(40, 131)
        Me.Label45.Name = "Label45"
        Me.Label45.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label45.Size = New System.Drawing.Size(139, 14)
        Me.Label45.TabIndex = 116
        Me.Label45.Text = "Profit (Margin) Amount :"
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.BackColor = System.Drawing.SystemColors.Control
        Me.Label42.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label42.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label42.Location = New System.Drawing.Point(40, 175)
        Me.Label42.Name = "Label42"
        Me.Label42.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label42.Size = New System.Drawing.Size(106, 14)
        Me.Label42.TabIndex = 115
        Me.Label42.Text = "Sale Price (C && F) :"
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.BackColor = System.Drawing.SystemColors.Control
        Me.Label41.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label41.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label41.Location = New System.Drawing.Point(40, 198)
        Me.Label41.Name = "Label41"
        Me.Label41.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label41.Size = New System.Drawing.Size(93, 14)
        Me.Label41.TabIndex = 114
        Me.Label41.Text = "Price Settelled :"
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.BackColor = System.Drawing.SystemColors.Control
        Me.Label39.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label39.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label39.Location = New System.Drawing.Point(8, 91)
        Me.Label39.Name = "Label39"
        Me.Label39.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label39.Size = New System.Drawing.Size(26, 14)
        Me.Label39.TabIndex = 113
        Me.Label39.Text = "XIII."
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.BackColor = System.Drawing.SystemColors.Control
        Me.Label35.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label35.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label35.Location = New System.Drawing.Point(10, 111)
        Me.Label35.Name = "Label35"
        Me.Label35.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label35.Size = New System.Drawing.Size(27, 14)
        Me.Label35.TabIndex = 112
        Me.Label35.Text = "XIV."
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.BackColor = System.Drawing.SystemColors.Control
        Me.Label34.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label34.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label34.Location = New System.Drawing.Point(8, 70)
        Me.Label34.Name = "Label34"
        Me.Label34.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label34.Size = New System.Drawing.Size(23, 14)
        Me.Label34.TabIndex = 111
        Me.Label34.Text = "XII."
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.BackColor = System.Drawing.SystemColors.Control
        Me.Label33.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label33.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label33.Location = New System.Drawing.Point(8, 131)
        Me.Label33.Name = "Label33"
        Me.Label33.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label33.Size = New System.Drawing.Size(24, 14)
        Me.Label33.TabIndex = 110
        Me.Label33.Text = "XV."
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(40, 218)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(61, 14)
        Me.Label16.TabIndex = 109
        Me.Label16.Text = "Discount :"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(8, 198)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(34, 14)
        Me.Label15.TabIndex = 108
        Me.Label15.Text = "XVIII."
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(40, 238)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(145, 14)
        Me.Label12.TabIndex = 107
        Me.Label12.Text = "Customer PO No. && Date :"
        '
        '_SSTab1_TabPage1
        '
        Me._SSTab1_TabPage1.Controls.Add(Me.SprdRM)
        Me._SSTab1_TabPage1.Location = New System.Drawing.Point(4, 28)
        Me._SSTab1_TabPage1.Name = "_SSTab1_TabPage1"
        Me._SSTab1_TabPage1.Size = New System.Drawing.Size(897, 386)
        Me._SSTab1_TabPage1.TabIndex = 1
        Me._SSTab1_TabPage1.Text = "I - Raw Materials"
        '
        'SprdRM
        '
        Me.SprdRM.DataSource = Nothing
        Me.SprdRM.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SprdRM.Location = New System.Drawing.Point(0, 0)
        Me.SprdRM.Name = "SprdRM"
        Me.SprdRM.OcxState = CType(resources.GetObject("SprdRM.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdRM.Size = New System.Drawing.Size(897, 386)
        Me.SprdRM.TabIndex = 144
        '
        '_SSTab1_TabPage2
        '
        Me._SSTab1_TabPage2.Controls.Add(Me.SprdBOP)
        Me._SSTab1_TabPage2.Location = New System.Drawing.Point(4, 28)
        Me._SSTab1_TabPage2.Name = "_SSTab1_TabPage2"
        Me._SSTab1_TabPage2.Size = New System.Drawing.Size(897, 386)
        Me._SSTab1_TabPage2.TabIndex = 2
        Me._SSTab1_TabPage2.Text = "II - BOP Items"
        '
        'SprdBOP
        '
        Me.SprdBOP.DataSource = Nothing
        Me.SprdBOP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SprdBOP.Location = New System.Drawing.Point(0, 0)
        Me.SprdBOP.Name = "SprdBOP"
        Me.SprdBOP.OcxState = CType(resources.GetObject("SprdBOP.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdBOP.Size = New System.Drawing.Size(897, 386)
        Me.SprdBOP.TabIndex = 50
        '
        '_SSTab1_TabPage3
        '
        Me._SSTab1_TabPage3.Controls.Add(Me.SprdWeld)
        Me._SSTab1_TabPage3.Location = New System.Drawing.Point(4, 28)
        Me._SSTab1_TabPage3.Name = "_SSTab1_TabPage3"
        Me._SSTab1_TabPage3.Size = New System.Drawing.Size(897, 386)
        Me._SSTab1_TabPage3.TabIndex = 3
        Me._SSTab1_TabPage3.Text = "III - Welding Cost"
        '
        'SprdWeld
        '
        Me.SprdWeld.DataSource = Nothing
        Me.SprdWeld.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SprdWeld.Location = New System.Drawing.Point(0, 0)
        Me.SprdWeld.Name = "SprdWeld"
        Me.SprdWeld.OcxState = CType(resources.GetObject("SprdWeld.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdWeld.Size = New System.Drawing.Size(897, 386)
        Me.SprdWeld.TabIndex = 51
        '
        '_SSTab1_TabPage4
        '
        Me._SSTab1_TabPage4.Controls.Add(Me.SprdOpr)
        Me._SSTab1_TabPage4.Location = New System.Drawing.Point(4, 28)
        Me._SSTab1_TabPage4.Name = "_SSTab1_TabPage4"
        Me._SSTab1_TabPage4.Size = New System.Drawing.Size(897, 386)
        Me._SSTab1_TabPage4.TabIndex = 4
        Me._SSTab1_TabPage4.Text = "IV - Process Cost"
        '
        'SprdOpr
        '
        Me.SprdOpr.DataSource = Nothing
        Me.SprdOpr.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SprdOpr.Location = New System.Drawing.Point(0, 0)
        Me.SprdOpr.Name = "SprdOpr"
        Me.SprdOpr.OcxState = CType(resources.GetObject("SprdOpr.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdOpr.Size = New System.Drawing.Size(897, 386)
        Me.SprdOpr.TabIndex = 52
        '
        '_SSTab1_TabPage5
        '
        Me._SSTab1_TabPage5.Controls.Add(Me.CboPlatingType)
        Me._SSTab1_TabPage5.Controls.Add(Me.SprdPlt)
        Me._SSTab1_TabPage5.Controls.Add(Me.Label40)
        Me._SSTab1_TabPage5.Location = New System.Drawing.Point(4, 28)
        Me._SSTab1_TabPage5.Name = "_SSTab1_TabPage5"
        Me._SSTab1_TabPage5.Size = New System.Drawing.Size(897, 386)
        Me._SSTab1_TabPage5.TabIndex = 5
        Me._SSTab1_TabPage5.Text = "V-A - Plating Cost"
        '
        'CboPlatingType
        '
        Me.CboPlatingType.BackColor = System.Drawing.SystemColors.Window
        Me.CboPlatingType.Cursor = System.Windows.Forms.Cursors.Default
        Me.CboPlatingType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboPlatingType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboPlatingType.ForeColor = System.Drawing.Color.Blue
        Me.CboPlatingType.Location = New System.Drawing.Point(85, 3)
        Me.CboPlatingType.Name = "CboPlatingType"
        Me.CboPlatingType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CboPlatingType.Size = New System.Drawing.Size(161, 22)
        Me.CboPlatingType.TabIndex = 140
        '
        'SprdPlt
        '
        Me.SprdPlt.DataSource = Nothing
        Me.SprdPlt.Location = New System.Drawing.Point(1, 25)
        Me.SprdPlt.Name = "SprdPlt"
        Me.SprdPlt.OcxState = CType(resources.GetObject("SprdPlt.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdPlt.Size = New System.Drawing.Size(892, 357)
        Me.SprdPlt.TabIndex = 53
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.BackColor = System.Drawing.SystemColors.Control
        Me.Label40.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label40.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label40.Location = New System.Drawing.Point(3, 5)
        Me.Label40.Name = "Label40"
        Me.Label40.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label40.Size = New System.Drawing.Size(79, 14)
        Me.Label40.TabIndex = 141
        Me.Label40.Text = "Plating Type :"
        '
        '_SSTab1_TabPage6
        '
        Me._SSTab1_TabPage6.Controls.Add(Me.SprdPnt)
        Me._SSTab1_TabPage6.Location = New System.Drawing.Point(4, 28)
        Me._SSTab1_TabPage6.Name = "_SSTab1_TabPage6"
        Me._SSTab1_TabPage6.Size = New System.Drawing.Size(897, 386)
        Me._SSTab1_TabPage6.TabIndex = 6
        Me._SSTab1_TabPage6.Text = "V-B - Painting Cost"
        '
        'SprdPnt
        '
        Me.SprdPnt.DataSource = Nothing
        Me.SprdPnt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SprdPnt.Location = New System.Drawing.Point(0, 0)
        Me.SprdPnt.Name = "SprdPnt"
        Me.SprdPnt.OcxState = CType(resources.GetObject("SprdPnt.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdPnt.Size = New System.Drawing.Size(897, 386)
        Me.SprdPnt.TabIndex = 54
        '
        '_SSTab1_TabPage7
        '
        Me._SSTab1_TabPage7.Controls.Add(Me.cboPowderType)
        Me._SSTab1_TabPage7.Controls.Add(Me.SprdPdr)
        Me._SSTab1_TabPage7.Controls.Add(Me.Label49)
        Me._SSTab1_TabPage7.Location = New System.Drawing.Point(4, 28)
        Me._SSTab1_TabPage7.Name = "_SSTab1_TabPage7"
        Me._SSTab1_TabPage7.Size = New System.Drawing.Size(897, 386)
        Me._SSTab1_TabPage7.TabIndex = 7
        Me._SSTab1_TabPage7.Text = "V-C - Powder Coating Cost"
        '
        'cboPowderType
        '
        Me.cboPowderType.BackColor = System.Drawing.SystemColors.Window
        Me.cboPowderType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboPowderType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPowderType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPowderType.ForeColor = System.Drawing.Color.Blue
        Me.cboPowderType.Location = New System.Drawing.Point(86, 2)
        Me.cboPowderType.Name = "cboPowderType"
        Me.cboPowderType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboPowderType.Size = New System.Drawing.Size(161, 22)
        Me.cboPowderType.TabIndex = 142
        '
        'SprdPdr
        '
        Me.SprdPdr.DataSource = Nothing
        Me.SprdPdr.Location = New System.Drawing.Point(2, 24)
        Me.SprdPdr.Name = "SprdPdr"
        Me.SprdPdr.OcxState = CType(resources.GetObject("SprdPdr.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdPdr.Size = New System.Drawing.Size(891, 358)
        Me.SprdPdr.TabIndex = 55
        '
        'Label49
        '
        Me.Label49.AutoSize = True
        Me.Label49.BackColor = System.Drawing.SystemColors.Control
        Me.Label49.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label49.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label49.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label49.Location = New System.Drawing.Point(4, 4)
        Me.Label49.Name = "Label49"
        Me.Label49.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label49.Size = New System.Drawing.Size(85, 14)
        Me.Label49.TabIndex = 143
        Me.Label49.Text = "Powder Type :"
        '
        '_SSTab1_TabPage8
        '
        Me._SSTab1_TabPage8.Controls.Add(Me.SprdPack)
        Me._SSTab1_TabPage8.Location = New System.Drawing.Point(4, 28)
        Me._SSTab1_TabPage8.Name = "_SSTab1_TabPage8"
        Me._SSTab1_TabPage8.Size = New System.Drawing.Size(897, 386)
        Me._SSTab1_TabPage8.TabIndex = 8
        Me._SSTab1_TabPage8.Text = "VIII - Others"
        '
        'SprdPack
        '
        Me.SprdPack.DataSource = Nothing
        Me.SprdPack.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SprdPack.Location = New System.Drawing.Point(0, 0)
        Me.SprdPack.Name = "SprdPack"
        Me.SprdPack.OcxState = CType(resources.GetObject("SprdPack.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdPack.Size = New System.Drawing.Size(897, 386)
        Me.SprdPack.TabIndex = 56
        '
        'fraCosting
        '
        Me.fraCosting.BackColor = System.Drawing.SystemColors.Control
        Me.fraCosting.Controls.Add(Me.chkStatus)
        Me.fraCosting.Controls.Add(Me.txtRemarks)
        Me.fraCosting.Controls.Add(Me.txtAppBy)
        Me.fraCosting.Controls.Add(Me.txtPrepBy)
        Me.fraCosting.Controls.Add(Me.cmdSearchPrepBy)
        Me.fraCosting.Controls.Add(Me.cmdSearchAppBy)
        Me.fraCosting.Controls.Add(Me.lblTotPackCost)
        Me.fraCosting.Controls.Add(Me.Label14)
        Me.fraCosting.Controls.Add(Me.lblAppBy)
        Me.fraCosting.Controls.Add(Me.Label13)
        Me.fraCosting.Controls.Add(Me.lblPrepBy)
        Me.fraCosting.Controls.Add(Me.Label5)
        Me.fraCosting.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraCosting.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraCosting.Location = New System.Drawing.Point(2, 514)
        Me.fraCosting.Name = "fraCosting"
        Me.fraCosting.Padding = New System.Windows.Forms.Padding(0)
        Me.fraCosting.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraCosting.Size = New System.Drawing.Size(747, 55)
        Me.fraCosting.TabIndex = 71
        Me.fraCosting.TabStop = False
        '
        'chkStatus
        '
        Me.chkStatus.AutoSize = True
        Me.chkStatus.BackColor = System.Drawing.SystemColors.Control
        Me.chkStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkStatus.Enabled = False
        Me.chkStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkStatus.Location = New System.Drawing.Point(460, 8)
        Me.chkStatus.Name = "chkStatus"
        Me.chkStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkStatus.Size = New System.Drawing.Size(149, 18)
        Me.chkStatus.TabIndex = 45
        Me.chkStatus.Text = "Status (Open / Closed)"
        Me.chkStatus.UseVisualStyleBackColor = False
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRemarks.Location = New System.Drawing.Point(88, 10)
        Me.txtRemarks.MaxLength = 0
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.Size = New System.Drawing.Size(283, 19)
        Me.txtRemarks.TabIndex = 44
        '
        'txtAppBy
        '
        Me.txtAppBy.AcceptsReturn = True
        Me.txtAppBy.BackColor = System.Drawing.SystemColors.Window
        Me.txtAppBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAppBy.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAppBy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAppBy.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAppBy.Location = New System.Drawing.Point(460, 30)
        Me.txtAppBy.MaxLength = 0
        Me.txtAppBy.Name = "txtAppBy"
        Me.txtAppBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAppBy.Size = New System.Drawing.Size(69, 20)
        Me.txtAppBy.TabIndex = 48
        '
        'txtPrepBy
        '
        Me.txtPrepBy.AcceptsReturn = True
        Me.txtPrepBy.BackColor = System.Drawing.SystemColors.Window
        Me.txtPrepBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPrepBy.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPrepBy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPrepBy.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPrepBy.Location = New System.Drawing.Point(88, 30)
        Me.txtPrepBy.MaxLength = 0
        Me.txtPrepBy.Name = "txtPrepBy"
        Me.txtPrepBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPrepBy.Size = New System.Drawing.Size(69, 20)
        Me.txtPrepBy.TabIndex = 46
        '
        'lblTotPackCost
        '
        Me.lblTotPackCost.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotPackCost.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotPackCost.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotPackCost.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTotPackCost.Location = New System.Drawing.Point(660, 12)
        Me.lblTotPackCost.Name = "lblTotPackCost"
        Me.lblTotPackCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotPackCost.Size = New System.Drawing.Size(73, 17)
        Me.lblTotPackCost.TabIndex = 139
        Me.lblTotPackCost.Text = "0"
        Me.lblTotPackCost.Visible = False
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(375, 33)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(83, 14)
        Me.Label14.TabIndex = 75
        Me.Label14.Text = "Approved By :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblAppBy
        '
        Me.lblAppBy.BackColor = System.Drawing.SystemColors.Control
        Me.lblAppBy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAppBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAppBy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAppBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAppBy.Location = New System.Drawing.Point(554, 30)
        Me.lblAppBy.Name = "lblAppBy"
        Me.lblAppBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAppBy.Size = New System.Drawing.Size(185, 19)
        Me.lblAppBy.TabIndex = 58
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(6, 33)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(80, 14)
        Me.Label13.TabIndex = 74
        Me.Label13.Text = "Prepared By :"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblPrepBy
        '
        Me.lblPrepBy.BackColor = System.Drawing.SystemColors.Control
        Me.lblPrepBy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPrepBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPrepBy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPrepBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPrepBy.Location = New System.Drawing.Point(182, 30)
        Me.lblPrepBy.Name = "lblPrepBy"
        Me.lblPrepBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPrepBy.Size = New System.Drawing.Size(185, 19)
        Me.lblPrepBy.TabIndex = 57
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(26, 13)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(63, 14)
        Me.Label5.TabIndex = 73
        Me.Label5.Text = "Remarks :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(909, 570)
        Me.SprdView.TabIndex = 72
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.CmdClose)
        Me.Frame3.Controls.Add(Me.CmdView)
        Me.Frame3.Controls.Add(Me.CmdPreview)
        Me.Frame3.Controls.Add(Me.Report1)
        Me.Frame3.Controls.Add(Me.cmdPrint)
        Me.Frame3.Controls.Add(Me.CmdDelete)
        Me.Frame3.Controls.Add(Me.cmdSavePrint)
        Me.Frame3.Controls.Add(Me.CmdSave)
        Me.Frame3.Controls.Add(Me.cmdAmend)
        Me.Frame3.Controls.Add(Me.CmdModify)
        Me.Frame3.Controls.Add(Me.CmdAdd)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(2, 571)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(915, 47)
        Me.Frame3.TabIndex = 69
        Me.Frame3.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(592, 12)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 69
        '
        'txtToolQty
        '
        Me.txtToolQty.AcceptsReturn = True
        Me.txtToolQty.BackColor = System.Drawing.SystemColors.Window
        Me.txtToolQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtToolQty.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtToolQty.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToolQty.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtToolQty.Location = New System.Drawing.Point(186, 70)
        Me.txtToolQty.MaxLength = 0
        Me.txtToolQty.Name = "txtToolQty"
        Me.txtToolQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtToolQty.Size = New System.Drawing.Size(74, 20)
        Me.txtToolQty.TabIndex = 137
        '
        'Label60
        '
        Me.Label60.AutoSize = True
        Me.Label60.BackColor = System.Drawing.SystemColors.Control
        Me.Label60.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label60.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label60.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label60.Location = New System.Drawing.Point(110, 70)
        Me.Label60.Name = "Label60"
        Me.Label60.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label60.Size = New System.Drawing.Size(74, 14)
        Me.Label60.TabIndex = 138
        Me.Label60.Text = "@ Total Qty :"
        Me.Label60.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtToolCostPerPc
        '
        Me.txtToolCostPerPc.AcceptsReturn = True
        Me.txtToolCostPerPc.BackColor = System.Drawing.SystemColors.Window
        Me.txtToolCostPerPc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtToolCostPerPc.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtToolCostPerPc.Enabled = False
        Me.txtToolCostPerPc.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToolCostPerPc.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtToolCostPerPc.Location = New System.Drawing.Point(445, 70)
        Me.txtToolCostPerPc.MaxLength = 0
        Me.txtToolCostPerPc.Name = "txtToolCostPerPc"
        Me.txtToolCostPerPc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtToolCostPerPc.Size = New System.Drawing.Size(75, 20)
        Me.txtToolCostPerPc.TabIndex = 139
        '
        'Label61
        '
        Me.Label61.AutoSize = True
        Me.Label61.BackColor = System.Drawing.SystemColors.Control
        Me.Label61.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label61.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label61.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label61.Location = New System.Drawing.Point(356, 70)
        Me.Label61.Name = "Label61"
        Me.Label61.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label61.Size = New System.Drawing.Size(87, 14)
        Me.Label61.TabIndex = 140
        Me.Label61.Text = "Tool Cost / Pc.:"
        Me.Label61.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtCostReduction
        '
        Me.txtCostReduction.AcceptsReturn = True
        Me.txtCostReduction.BackColor = System.Drawing.SystemColors.Window
        Me.txtCostReduction.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCostReduction.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCostReduction.Enabled = False
        Me.txtCostReduction.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCostReduction.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCostReduction.Location = New System.Drawing.Point(281, 153)
        Me.txtCostReduction.MaxLength = 0
        Me.txtCostReduction.Name = "txtCostReduction"
        Me.txtCostReduction.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCostReduction.Size = New System.Drawing.Size(75, 20)
        Me.txtCostReduction.TabIndex = 141
        Me.txtCostReduction.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label62
        '
        Me.Label62.AutoSize = True
        Me.Label62.BackColor = System.Drawing.SystemColors.Control
        Me.Label62.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label62.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label62.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label62.Location = New System.Drawing.Point(8, 154)
        Me.Label62.Name = "Label62"
        Me.Label62.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label62.Size = New System.Drawing.Size(28, 14)
        Me.Label62.TabIndex = 143
        Me.Label62.Text = "XVI."
        '
        'Label63
        '
        Me.Label63.AutoSize = True
        Me.Label63.BackColor = System.Drawing.SystemColors.Control
        Me.Label63.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label63.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label63.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label63.Location = New System.Drawing.Point(40, 154)
        Me.Label63.Name = "Label63"
        Me.Label63.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label63.Size = New System.Drawing.Size(94, 14)
        Me.Label63.TabIndex = 142
        Me.Label63.Text = "Cost Reduction:"
        '
        'frmFGCostingCustomerWise
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(911, 621)
        Me.Controls.Add(Me.fraBase)
        Me.Controls.Add(Me.SprdView)
        Me.Controls.Add(Me.Frame3)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.Black
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(4, 23)
        Me.MaximizeBox = False
        Me.Name = "frmFGCostingCustomerWise"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Finished Goods Costing - Customer Wise"
        Me.fraBase.ResumeLayout(False)
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.SSTab1.ResumeLayout(False)
        Me._SSTab1_TabPage0.ResumeLayout(False)
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me._SSTab1_TabPage1.ResumeLayout(False)
        CType(Me.SprdRM, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage2.ResumeLayout(False)
        CType(Me.SprdBOP, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage3.ResumeLayout(False)
        CType(Me.SprdWeld, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage4.ResumeLayout(False)
        CType(Me.SprdOpr, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage5.ResumeLayout(False)
        Me._SSTab1_TabPage5.PerformLayout()
        CType(Me.SprdPlt, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage6.ResumeLayout(False)
        CType(Me.SprdPnt, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage7.ResumeLayout(False)
        Me._SSTab1_TabPage7.PerformLayout()
        CType(Me.SprdPdr, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage8.ResumeLayout(False)
        CType(Me.SprdPack, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraCosting.ResumeLayout(False)
        Me.fraCosting.PerformLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame3.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        'SprdView.DataSource = CType(ADataGrid, MSDATASRC.DataSource)
    End Sub
	Public Sub VB6_RemoveADODataBinding()
		SprdView.DataSource = Nothing
	End Sub

    Public WithEvents txtToolQty As TextBox
    Public WithEvents Label60 As Label
    Public WithEvents txtToolCostPerPc As TextBox
    Public WithEvents Label61 As Label
    Public WithEvents txtCostReduction As TextBox
    Public WithEvents Label62 As Label
    Public WithEvents Label63 As Label
#End Region
End Class