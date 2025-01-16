Imports Microsoft.VisualBasic.Compatibility

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmItemReqMaster
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
        '
        ''InventoryGST.Master.Show
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
    Public WithEvents cmdSearchHSN As System.Windows.Forms.Button
    Public WithEvents cboGSTClass As System.Windows.Forms.ComboBox
    Public WithEvents txtProdType As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchProdType As System.Windows.Forms.Button
    Public WithEvents txtColor As System.Windows.Forms.TextBox
    Public WithEvents txtItemMake As System.Windows.Forms.TextBox
    Public WithEvents txtModel As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchModel As System.Windows.Forms.Button
    Public WithEvents txtScrapItemCode As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchScrap As System.Windows.Forms.Button
    Public WithEvents CboStatus As System.Windows.Forms.ComboBox
    Public WithEvents txtPackingItemCode As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchPIC As System.Windows.Forms.Button
    Public WithEvents chkPOReqd As System.Windows.Forms.CheckBox
    Public WithEvents chkStockItem As System.Windows.Forms.CheckBox
    Public WithEvents chkRequired As System.Windows.Forms.CheckBox
    Public WithEvents chkAutoIndent As System.Windows.Forms.CheckBox
    Public WithEvents chkDrawing As System.Windows.Forms.CheckBox
    Public WithEvents chkConsumable As System.Windows.Forms.CheckBox
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents lblHSNName As System.Windows.Forms.Label
    Public WithEvents _lblUnder_58 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_59 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_47 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_23 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_24 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_25 As System.Windows.Forms.Label
    Public WithEvents Label44 As System.Windows.Forms.Label
    Public WithEvents lblAddUser As System.Windows.Forms.Label
    Public WithEvents Label46 As System.Windows.Forms.Label
    Public WithEvents lblModUser As System.Windows.Forms.Label
    Public WithEvents Label45 As System.Windows.Forms.Label
    Public WithEvents lblAddDate As System.Windows.Forms.Label
    Public WithEvents Label48 As System.Windows.Forms.Label
    Public WithEvents lblModDate As System.Windows.Forms.Label
    Public WithEvents lblScrapItemName As System.Windows.Forms.Label
    Public WithEvents _lblUnder_10 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_20 As System.Windows.Forms.Label
    Public WithEvents lblPackItemName As System.Windows.Forms.Label
    Public WithEvents _lblUnder_32 As System.Windows.Forms.Label
    Public WithEvents FraView As System.Windows.Forms.GroupBox
    Public WithEvents _SSTInfo_TabPage0 As System.Windows.Forms.TabPage
    Public WithEvents _lblUnder_3 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_26 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_28 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_29 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_33 As System.Windows.Forms.Label
    Public WithEvents lblTacks As System.Windows.Forms.Label
    Public WithEvents _lblUnder_41 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_12 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_31 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_42 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_43 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_44 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_45 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_13 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_34 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_38 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_37 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_35 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_36 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_40 As System.Windows.Forms.Label
    Public WithEvents txtSurfaceTreatment As System.Windows.Forms.TextBox
    Public WithEvents txtWeight As System.Windows.Forms.TextBox
    Public WithEvents txtDimention As System.Windows.Forms.TextBox
    Public WithEvents txtSpecification As System.Windows.Forms.TextBox
    Public WithEvents txtSurfaceArea As System.Windows.Forms.TextBox
    Public WithEvents txtTacks As System.Windows.Forms.TextBox
    Public WithEvents txtScrapWeight As System.Windows.Forms.TextBox
    Public WithEvents txtWLength As System.Windows.Forms.TextBox
    Public WithEvents txtMaterial As System.Windows.Forms.TextBox
    Public WithEvents txtThickness As System.Windows.Forms.TextBox
    Public WithEvents txtWidth As System.Windows.Forms.TextBox
    Public WithEvents txtLength As System.Windows.Forms.TextBox
    Public WithEvents txtDensity As System.Windows.Forms.TextBox
    Public WithEvents txtLocation As System.Windows.Forms.TextBox
    Public WithEvents txtInspectionNo As System.Windows.Forms.TextBox
    Public WithEvents txtIdMark As System.Windows.Forms.TextBox
    Public WithEvents txtDwgRevDate As System.Windows.Forms.TextBox
    Public WithEvents txtDwgNo As System.Windows.Forms.TextBox
    Public WithEvents txtDwgRevNo As System.Windows.Forms.TextBox
    Public WithEvents txtPackingStandard As System.Windows.Forms.TextBox
    Public WithEvents _SSTInfo_TabPage1 As System.Windows.Forms.TabPage
    Public WithEvents SSTInfo As System.Windows.Forms.TabControl
    Public WithEvents txtTechnicalDescription As System.Windows.Forms.TextBox
    Public WithEvents chkExportItem As System.Windows.Forms.CheckBox
    Public WithEvents txtItemName As System.Windows.Forms.TextBox
    Public WithEvents txtItemCode As System.Windows.Forms.TextBox
    Public WithEvents cmdsearch As System.Windows.Forms.Button
    Public WithEvents lblItemName As System.Windows.Forms.Label
    Public WithEvents _lblUnder_39 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents fraTop1 As System.Windows.Forms.GroupBox
    Public WithEvents txtPartNo As System.Windows.Forms.TextBox
    Public WithEvents txtUOMFactor As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchPurUom As System.Windows.Forms.Button
    Public WithEvents txtPurchaseUom As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchUom As System.Windows.Forms.Button
    Public WithEvents txtItemUom As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchSubCat As System.Windows.Forms.Button
    Public WithEvents txtSubCatName As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchCategory As System.Windows.Forms.Button
    Public WithEvents txtCatName As System.Windows.Forms.TextBox
    Public WithEvents lblPurUom As System.Windows.Forms.Label
    Public WithEvents lblItemUom As System.Windows.Forms.Label
    Public WithEvents lblSubCatName As System.Windows.Forms.Label
    Public WithEvents lblCatName As System.Windows.Forms.Label
    Public WithEvents _lblUnder_27 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_2 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_4 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_5 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_6 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_0 As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents txtItemClassQnty As System.Windows.Forms.TextBox
    Public WithEvents txtMaxQnty As System.Windows.Forms.TextBox
    Public WithEvents txtEcoQnty As System.Windows.Forms.TextBox
    Public WithEvents txtMinQnty As System.Windows.Forms.TextBox
    Public WithEvents txtPurchaseCost As System.Windows.Forms.TextBox
    Public WithEvents txtLeadTime As System.Windows.Forms.TextBox
    Public WithEvents txtReQnty As System.Windows.Forms.TextBox
    Public WithEvents txtSaleCost As System.Windows.Forms.TextBox
    Public WithEvents _lblUnder_22 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_7 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_8 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_9 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_11 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_14 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_15 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_21 As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents CboItemType As System.Windows.Forms.ComboBox
    Public WithEvents CboExciseFlag As System.Windows.Forms.ComboBox
    Public WithEvents cboItemClassification As System.Windows.Forms.ComboBox
    Public WithEvents CboItemClass As System.Windows.Forms.ComboBox
    Public WithEvents _lblUnder_16 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_17 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_18 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_19 As System.Windows.Forms.Label
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents FraTrn As System.Windows.Forms.GroupBox
    Public WithEvents ADataGrid As VB6.ADODC
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents CmdAdd As System.Windows.Forms.Button
    Public WithEvents CmdModify As System.Windows.Forms.Button
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents CmdDelete As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents lblType As System.Windows.Forms.Label
    Public WithEvents lblMasterType As System.Windows.Forms.Label
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents lblUnder As VB6.LabelArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmItemReqMaster))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearchHSN = New System.Windows.Forms.Button()
        Me.cmdSearchProdType = New System.Windows.Forms.Button()
        Me.cmdSearchModel = New System.Windows.Forms.Button()
        Me.cmdSearchScrap = New System.Windows.Forms.Button()
        Me.cmdSearchPIC = New System.Windows.Forms.Button()
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.cmdSearchPurUom = New System.Windows.Forms.Button()
        Me.cmdSearchUom = New System.Windows.Forms.Button()
        Me.cmdSearchSubCat = New System.Windows.Forms.Button()
        Me.cmdSearchCategory = New System.Windows.Forms.Button()
        Me.CboItemType = New System.Windows.Forms.ComboBox()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.FraTrn = New System.Windows.Forms.GroupBox()
        Me.SSTInfo = New System.Windows.Forms.TabControl()
        Me._SSTInfo_TabPage0 = New System.Windows.Forms.TabPage()
        Me.FraView = New System.Windows.Forms.GroupBox()
        Me.txtHSNCode = New System.Windows.Forms.TextBox()
        Me.cboGSTClass = New System.Windows.Forms.ComboBox()
        Me.txtProdType = New System.Windows.Forms.TextBox()
        Me.txtColor = New System.Windows.Forms.TextBox()
        Me.txtItemMake = New System.Windows.Forms.TextBox()
        Me.txtModel = New System.Windows.Forms.TextBox()
        Me.txtScrapItemCode = New System.Windows.Forms.TextBox()
        Me.CboStatus = New System.Windows.Forms.ComboBox()
        Me.txtPackingItemCode = New System.Windows.Forms.TextBox()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.chkPOReqd = New System.Windows.Forms.CheckBox()
        Me.chkStockItem = New System.Windows.Forms.CheckBox()
        Me.chkRequired = New System.Windows.Forms.CheckBox()
        Me.chkAutoIndent = New System.Windows.Forms.CheckBox()
        Me.chkDrawing = New System.Windows.Forms.CheckBox()
        Me.chkConsumable = New System.Windows.Forms.CheckBox()
        Me.lblHSNName = New System.Windows.Forms.Label()
        Me._lblUnder_58 = New System.Windows.Forms.Label()
        Me._lblUnder_59 = New System.Windows.Forms.Label()
        Me._lblUnder_47 = New System.Windows.Forms.Label()
        Me._lblUnder_23 = New System.Windows.Forms.Label()
        Me._lblUnder_24 = New System.Windows.Forms.Label()
        Me._lblUnder_25 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.lblAddUser = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.lblModUser = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.lblAddDate = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.lblModDate = New System.Windows.Forms.Label()
        Me.lblScrapItemName = New System.Windows.Forms.Label()
        Me._lblUnder_10 = New System.Windows.Forms.Label()
        Me._lblUnder_20 = New System.Windows.Forms.Label()
        Me.lblPackItemName = New System.Windows.Forms.Label()
        Me._lblUnder_32 = New System.Windows.Forms.Label()
        Me._SSTInfo_TabPage1 = New System.Windows.Forms.TabPage()
        Me._lblUnder_3 = New System.Windows.Forms.Label()
        Me._lblUnder_26 = New System.Windows.Forms.Label()
        Me._lblUnder_28 = New System.Windows.Forms.Label()
        Me._lblUnder_29 = New System.Windows.Forms.Label()
        Me._lblUnder_33 = New System.Windows.Forms.Label()
        Me.lblTacks = New System.Windows.Forms.Label()
        Me._lblUnder_41 = New System.Windows.Forms.Label()
        Me._lblUnder_12 = New System.Windows.Forms.Label()
        Me._lblUnder_31 = New System.Windows.Forms.Label()
        Me._lblUnder_42 = New System.Windows.Forms.Label()
        Me._lblUnder_43 = New System.Windows.Forms.Label()
        Me._lblUnder_44 = New System.Windows.Forms.Label()
        Me._lblUnder_45 = New System.Windows.Forms.Label()
        Me._lblUnder_13 = New System.Windows.Forms.Label()
        Me._lblUnder_34 = New System.Windows.Forms.Label()
        Me._lblUnder_38 = New System.Windows.Forms.Label()
        Me._lblUnder_37 = New System.Windows.Forms.Label()
        Me._lblUnder_35 = New System.Windows.Forms.Label()
        Me._lblUnder_36 = New System.Windows.Forms.Label()
        Me._lblUnder_40 = New System.Windows.Forms.Label()
        Me.txtSurfaceTreatment = New System.Windows.Forms.TextBox()
        Me.txtWeight = New System.Windows.Forms.TextBox()
        Me.txtDimention = New System.Windows.Forms.TextBox()
        Me.txtSpecification = New System.Windows.Forms.TextBox()
        Me.txtSurfaceArea = New System.Windows.Forms.TextBox()
        Me.txtTacks = New System.Windows.Forms.TextBox()
        Me.txtScrapWeight = New System.Windows.Forms.TextBox()
        Me.txtWLength = New System.Windows.Forms.TextBox()
        Me.txtMaterial = New System.Windows.Forms.TextBox()
        Me.txtThickness = New System.Windows.Forms.TextBox()
        Me.txtWidth = New System.Windows.Forms.TextBox()
        Me.txtLength = New System.Windows.Forms.TextBox()
        Me.txtDensity = New System.Windows.Forms.TextBox()
        Me.txtLocation = New System.Windows.Forms.TextBox()
        Me.txtInspectionNo = New System.Windows.Forms.TextBox()
        Me.txtIdMark = New System.Windows.Forms.TextBox()
        Me.txtDwgRevDate = New System.Windows.Forms.TextBox()
        Me.txtDwgNo = New System.Windows.Forms.TextBox()
        Me.txtDwgRevNo = New System.Windows.Forms.TextBox()
        Me.txtPackingStandard = New System.Windows.Forms.TextBox()
        Me.fraTop1 = New System.Windows.Forms.GroupBox()
        Me.txtTechnicalDescription = New System.Windows.Forms.TextBox()
        Me.chkExportItem = New System.Windows.Forms.CheckBox()
        Me.txtItemName = New System.Windows.Forms.TextBox()
        Me.txtItemCode = New System.Windows.Forms.TextBox()
        Me.lblItemName = New System.Windows.Forms.Label()
        Me._lblUnder_39 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.txtPartNo = New System.Windows.Forms.TextBox()
        Me.txtUOMFactor = New System.Windows.Forms.TextBox()
        Me.txtPurchaseUom = New System.Windows.Forms.TextBox()
        Me.txtItemUom = New System.Windows.Forms.TextBox()
        Me.txtSubCatName = New System.Windows.Forms.TextBox()
        Me.txtCatName = New System.Windows.Forms.TextBox()
        Me.lblPurUom = New System.Windows.Forms.Label()
        Me.lblItemUom = New System.Windows.Forms.Label()
        Me.lblSubCatName = New System.Windows.Forms.Label()
        Me.lblCatName = New System.Windows.Forms.Label()
        Me._lblUnder_27 = New System.Windows.Forms.Label()
        Me._lblUnder_2 = New System.Windows.Forms.Label()
        Me._lblUnder_4 = New System.Windows.Forms.Label()
        Me._lblUnder_5 = New System.Windows.Forms.Label()
        Me._lblUnder_6 = New System.Windows.Forms.Label()
        Me._lblUnder_0 = New System.Windows.Forms.Label()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.txtItemClassQnty = New System.Windows.Forms.TextBox()
        Me.txtMaxQnty = New System.Windows.Forms.TextBox()
        Me.txtEcoQnty = New System.Windows.Forms.TextBox()
        Me.txtMinQnty = New System.Windows.Forms.TextBox()
        Me.txtPurchaseCost = New System.Windows.Forms.TextBox()
        Me.txtLeadTime = New System.Windows.Forms.TextBox()
        Me.txtReQnty = New System.Windows.Forms.TextBox()
        Me.txtSaleCost = New System.Windows.Forms.TextBox()
        Me._lblUnder_22 = New System.Windows.Forms.Label()
        Me._lblUnder_7 = New System.Windows.Forms.Label()
        Me._lblUnder_8 = New System.Windows.Forms.Label()
        Me._lblUnder_9 = New System.Windows.Forms.Label()
        Me._lblUnder_11 = New System.Windows.Forms.Label()
        Me._lblUnder_14 = New System.Windows.Forms.Label()
        Me._lblUnder_15 = New System.Windows.Forms.Label()
        Me._lblUnder_21 = New System.Windows.Forms.Label()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.CboExciseFlag = New System.Windows.Forms.ComboBox()
        Me.cboItemClassification = New System.Windows.Forms.ComboBox()
        Me.CboItemClass = New System.Windows.Forms.ComboBox()
        Me._lblUnder_16 = New System.Windows.Forms.Label()
        Me._lblUnder_17 = New System.Windows.Forms.Label()
        Me._lblUnder_18 = New System.Windows.Forms.Label()
        Me._lblUnder_19 = New System.Windows.Forms.Label()
        Me.ADataGrid = New Microsoft.VisualBasic.Compatibility.VB6.ADODC()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.lblType = New System.Windows.Forms.Label()
        Me.lblMasterType = New System.Windows.Forms.Label()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.lblUnder = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.FraTrn.SuspendLayout()
        Me.SSTInfo.SuspendLayout()
        Me._SSTInfo_TabPage0.SuspendLayout()
        Me.FraView.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me._SSTInfo_TabPage1.SuspendLayout()
        Me.fraTop1.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.Frame4.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblUnder, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSearchHSN
        '
        Me.cmdSearchHSN.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchHSN.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchHSN.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchHSN.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchHSN.Image = CType(resources.GetObject("cmdSearchHSN.Image"), System.Drawing.Image)
        Me.cmdSearchHSN.Location = New System.Drawing.Point(206, 50)
        Me.cmdSearchHSN.Name = "cmdSearchHSN"
        Me.cmdSearchHSN.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchHSN.Size = New System.Drawing.Size(30, 24)
        Me.cmdSearchHSN.TabIndex = 33
        Me.cmdSearchHSN.TabStop = False
        Me.cmdSearchHSN.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchHSN, "Search")
        Me.cmdSearchHSN.UseVisualStyleBackColor = False
        '
        'cmdSearchProdType
        '
        Me.cmdSearchProdType.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchProdType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchProdType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchProdType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchProdType.Image = CType(resources.GetObject("cmdSearchProdType.Image"), System.Drawing.Image)
        Me.cmdSearchProdType.Location = New System.Drawing.Point(386, 182)
        Me.cmdSearchProdType.Name = "cmdSearchProdType"
        Me.cmdSearchProdType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchProdType.Size = New System.Drawing.Size(30, 24)
        Me.cmdSearchProdType.TabIndex = 153
        Me.cmdSearchProdType.TabStop = False
        Me.cmdSearchProdType.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchProdType, "Search")
        Me.cmdSearchProdType.UseVisualStyleBackColor = False
        '
        'cmdSearchModel
        '
        Me.cmdSearchModel.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchModel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchModel.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchModel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchModel.Image = CType(resources.GetObject("cmdSearchModel.Image"), System.Drawing.Image)
        Me.cmdSearchModel.Location = New System.Drawing.Point(206, 149)
        Me.cmdSearchModel.Name = "cmdSearchModel"
        Me.cmdSearchModel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchModel.Size = New System.Drawing.Size(30, 24)
        Me.cmdSearchModel.TabIndex = 40
        Me.cmdSearchModel.TabStop = False
        Me.cmdSearchModel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchModel, "Search")
        Me.cmdSearchModel.UseVisualStyleBackColor = False
        '
        'cmdSearchScrap
        '
        Me.cmdSearchScrap.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchScrap.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchScrap.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchScrap.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchScrap.Image = CType(resources.GetObject("cmdSearchScrap.Image"), System.Drawing.Image)
        Me.cmdSearchScrap.Location = New System.Drawing.Point(206, 116)
        Me.cmdSearchScrap.Name = "cmdSearchScrap"
        Me.cmdSearchScrap.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchScrap.Size = New System.Drawing.Size(30, 24)
        Me.cmdSearchScrap.TabIndex = 38
        Me.cmdSearchScrap.TabStop = False
        Me.cmdSearchScrap.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchScrap, "Search")
        Me.cmdSearchScrap.UseVisualStyleBackColor = False
        '
        'cmdSearchPIC
        '
        Me.cmdSearchPIC.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchPIC.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchPIC.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchPIC.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchPIC.Image = CType(resources.GetObject("cmdSearchPIC.Image"), System.Drawing.Image)
        Me.cmdSearchPIC.Location = New System.Drawing.Point(206, 83)
        Me.cmdSearchPIC.Name = "cmdSearchPIC"
        Me.cmdSearchPIC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchPIC.Size = New System.Drawing.Size(30, 24)
        Me.cmdSearchPIC.TabIndex = 36
        Me.cmdSearchPIC.TabStop = False
        Me.cmdSearchPIC.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchPIC, "Search")
        Me.cmdSearchPIC.UseVisualStyleBackColor = False
        '
        'cmdsearch
        '
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.Location = New System.Drawing.Point(744, 14)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(30, 24)
        Me.cmdsearch.TabIndex = 2
        Me.cmdsearch.TabStop = False
        Me.cmdsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearch, "Search")
        Me.cmdsearch.UseVisualStyleBackColor = False
        '
        'cmdSearchPurUom
        '
        Me.cmdSearchPurUom.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchPurUom.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchPurUom.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchPurUom.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchPurUom.Image = CType(resources.GetObject("cmdSearchPurUom.Image"), System.Drawing.Image)
        Me.cmdSearchPurUom.Location = New System.Drawing.Point(487, 10)
        Me.cmdSearchPurUom.Name = "cmdSearchPurUom"
        Me.cmdSearchPurUom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchPurUom.Size = New System.Drawing.Size(30, 24)
        Me.cmdSearchPurUom.TabIndex = 9
        Me.cmdSearchPurUom.TabStop = False
        Me.cmdSearchPurUom.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchPurUom, "Search")
        Me.cmdSearchPurUom.UseVisualStyleBackColor = False
        '
        'cmdSearchUom
        '
        Me.cmdSearchUom.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchUom.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchUom.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchUom.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchUom.Image = CType(resources.GetObject("cmdSearchUom.Image"), System.Drawing.Image)
        Me.cmdSearchUom.Location = New System.Drawing.Point(156, 12)
        Me.cmdSearchUom.Name = "cmdSearchUom"
        Me.cmdSearchUom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchUom.Size = New System.Drawing.Size(30, 24)
        Me.cmdSearchUom.TabIndex = 7
        Me.cmdSearchUom.TabStop = False
        Me.cmdSearchUom.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchUom, "Search")
        Me.cmdSearchUom.UseVisualStyleBackColor = False
        '
        'cmdSearchSubCat
        '
        Me.cmdSearchSubCat.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchSubCat.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchSubCat.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchSubCat.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchSubCat.Image = CType(resources.GetObject("cmdSearchSubCat.Image"), System.Drawing.Image)
        Me.cmdSearchSubCat.Location = New System.Drawing.Point(487, 43)
        Me.cmdSearchSubCat.Name = "cmdSearchSubCat"
        Me.cmdSearchSubCat.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchSubCat.Size = New System.Drawing.Size(30, 24)
        Me.cmdSearchSubCat.TabIndex = 14
        Me.cmdSearchSubCat.TabStop = False
        Me.cmdSearchSubCat.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchSubCat, "Search")
        Me.cmdSearchSubCat.UseVisualStyleBackColor = False
        '
        'cmdSearchCategory
        '
        Me.cmdSearchCategory.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchCategory.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchCategory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchCategory.Image = CType(resources.GetObject("cmdSearchCategory.Image"), System.Drawing.Image)
        Me.cmdSearchCategory.Location = New System.Drawing.Point(156, 43)
        Me.cmdSearchCategory.Name = "cmdSearchCategory"
        Me.cmdSearchCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchCategory.Size = New System.Drawing.Size(30, 24)
        Me.cmdSearchCategory.TabIndex = 12
        Me.cmdSearchCategory.TabStop = False
        Me.cmdSearchCategory.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchCategory, "Search")
        Me.cmdSearchCategory.UseVisualStyleBackColor = False
        '
        'CboItemType
        '
        Me.CboItemType.BackColor = System.Drawing.SystemColors.Window
        Me.CboItemType.Cursor = System.Windows.Forms.Cursors.Default
        Me.CboItemType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboItemType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboItemType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.CboItemType.Location = New System.Drawing.Point(102, 13)
        Me.CboItemType.Name = "CboItemType"
        Me.CboItemType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CboItemType.Size = New System.Drawing.Size(85, 21)
        Me.CboItemType.TabIndex = 24
        Me.ToolTip1.SetToolTip(Me.CboItemType, "IMPORTED OR LOCAL")
        '
        'CmdAdd
        '
        Me.CmdAdd.BackColor = System.Drawing.SystemColors.Control
        Me.CmdAdd.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdAdd.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdAdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdAdd.Image = CType(resources.GetObject("CmdAdd.Image"), System.Drawing.Image)
        Me.CmdAdd.Location = New System.Drawing.Point(176, 11)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(67, 37)
        Me.CmdAdd.TabIndex = 0
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'CmdModify
        '
        Me.CmdModify.BackColor = System.Drawing.SystemColors.Control
        Me.CmdModify.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdModify.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdModify.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdModify.Image = CType(resources.GetObject("CmdModify.Image"), System.Drawing.Image)
        Me.CmdModify.Location = New System.Drawing.Point(244, 11)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(67, 37)
        Me.CmdModify.TabIndex = 69
        Me.CmdModify.Text = "&Modify"
        Me.CmdModify.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdModify, "Modify Record")
        Me.CmdModify.UseVisualStyleBackColor = False
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.Location = New System.Drawing.Point(312, 11)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(67, 37)
        Me.CmdSave.TabIndex = 70
        Me.CmdSave.Text = "&Save"
        Me.CmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSave, "Save Record")
        Me.CmdSave.UseVisualStyleBackColor = False
        '
        'CmdDelete
        '
        Me.CmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.CmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdDelete.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdDelete.Image = CType(resources.GetObject("CmdDelete.Image"), System.Drawing.Image)
        Me.CmdDelete.Location = New System.Drawing.Point(446, 11)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(67, 37)
        Me.CmdDelete.TabIndex = 72
        Me.CmdDelete.Text = "&Delete"
        Me.CmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdDelete, "Delete Record")
        Me.CmdDelete.UseVisualStyleBackColor = False
        '
        'CmdView
        '
        Me.CmdView.BackColor = System.Drawing.SystemColors.Control
        Me.CmdView.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdView.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
        Me.CmdView.Location = New System.Drawing.Point(648, 11)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(67, 37)
        Me.CmdView.TabIndex = 75
        Me.CmdView.Text = "List &View"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdView, "List View")
        Me.CmdView.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(716, 11)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(67, 37)
        Me.CmdClose.TabIndex = 76
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close the Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(514, 11)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 73
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
        Me.cmdSavePrint.Location = New System.Drawing.Point(380, 11)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdSavePrint.TabIndex = 71
        Me.cmdSavePrint.Text = "Save&&Print"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save and Print Record")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(582, 11)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.TabIndex = 74
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Preview")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'FraTrn
        '
        Me.FraTrn.BackColor = System.Drawing.SystemColors.Control
        Me.FraTrn.Controls.Add(Me.SSTInfo)
        Me.FraTrn.Controls.Add(Me.fraTop1)
        Me.FraTrn.Controls.Add(Me.Frame2)
        Me.FraTrn.Controls.Add(Me.Frame3)
        Me.FraTrn.Controls.Add(Me.Frame4)
        Me.FraTrn.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraTrn.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraTrn.Location = New System.Drawing.Point(0, -6)
        Me.FraTrn.Name = "FraTrn"
        Me.FraTrn.Padding = New System.Windows.Forms.Padding(0)
        Me.FraTrn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraTrn.Size = New System.Drawing.Size(1006, 568)
        Me.FraTrn.TabIndex = 84
        Me.FraTrn.TabStop = False
        '
        'SSTInfo
        '
        Me.SSTInfo.Controls.Add(Me._SSTInfo_TabPage0)
        Me.SSTInfo.Controls.Add(Me._SSTInfo_TabPage1)
        Me.SSTInfo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SSTInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.SSTInfo.ItemSize = New System.Drawing.Size(42, 18)
        Me.SSTInfo.Location = New System.Drawing.Point(0, 256)
        Me.SSTInfo.Name = "SSTInfo"
        Me.SSTInfo.SelectedIndex = 0
        Me.SSTInfo.Size = New System.Drawing.Size(1004, 274)
        Me.SSTInfo.TabIndex = 112
        '
        '_SSTInfo_TabPage0
        '
        Me._SSTInfo_TabPage0.Controls.Add(Me.FraView)
        Me._SSTInfo_TabPage0.Location = New System.Drawing.Point(4, 22)
        Me._SSTInfo_TabPage0.Name = "_SSTInfo_TabPage0"
        Me._SSTInfo_TabPage0.Size = New System.Drawing.Size(996, 248)
        Me._SSTInfo_TabPage0.TabIndex = 0
        Me._SSTInfo_TabPage0.Text = "Status"
        '
        'FraView
        '
        Me.FraView.BackColor = System.Drawing.SystemColors.Control
        Me.FraView.Controls.Add(Me.txtHSNCode)
        Me.FraView.Controls.Add(Me.cmdSearchHSN)
        Me.FraView.Controls.Add(Me.cboGSTClass)
        Me.FraView.Controls.Add(Me.txtProdType)
        Me.FraView.Controls.Add(Me.cmdSearchProdType)
        Me.FraView.Controls.Add(Me.txtColor)
        Me.FraView.Controls.Add(Me.txtItemMake)
        Me.FraView.Controls.Add(Me.txtModel)
        Me.FraView.Controls.Add(Me.cmdSearchModel)
        Me.FraView.Controls.Add(Me.txtScrapItemCode)
        Me.FraView.Controls.Add(Me.cmdSearchScrap)
        Me.FraView.Controls.Add(Me.CboStatus)
        Me.FraView.Controls.Add(Me.txtPackingItemCode)
        Me.FraView.Controls.Add(Me.cmdSearchPIC)
        Me.FraView.Controls.Add(Me.Frame1)
        Me.FraView.Controls.Add(Me.lblHSNName)
        Me.FraView.Controls.Add(Me._lblUnder_58)
        Me.FraView.Controls.Add(Me._lblUnder_59)
        Me.FraView.Controls.Add(Me._lblUnder_47)
        Me.FraView.Controls.Add(Me._lblUnder_23)
        Me.FraView.Controls.Add(Me._lblUnder_24)
        Me.FraView.Controls.Add(Me._lblUnder_25)
        Me.FraView.Controls.Add(Me.Label44)
        Me.FraView.Controls.Add(Me.lblAddUser)
        Me.FraView.Controls.Add(Me.Label46)
        Me.FraView.Controls.Add(Me.lblModUser)
        Me.FraView.Controls.Add(Me.Label45)
        Me.FraView.Controls.Add(Me.lblAddDate)
        Me.FraView.Controls.Add(Me.Label48)
        Me.FraView.Controls.Add(Me.lblModDate)
        Me.FraView.Controls.Add(Me.lblScrapItemName)
        Me.FraView.Controls.Add(Me._lblUnder_10)
        Me.FraView.Controls.Add(Me._lblUnder_20)
        Me.FraView.Controls.Add(Me.lblPackItemName)
        Me.FraView.Controls.Add(Me._lblUnder_32)
        Me.FraView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FraView.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraView.Location = New System.Drawing.Point(0, 0)
        Me.FraView.Name = "FraView"
        Me.FraView.Padding = New System.Windows.Forms.Padding(0)
        Me.FraView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraView.Size = New System.Drawing.Size(996, 248)
        Me.FraView.TabIndex = 113
        Me.FraView.TabStop = False
        '
        'txtHSNCode
        '
        Me.txtHSNCode.AcceptsReturn = True
        Me.txtHSNCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtHSNCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtHSNCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtHSNCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHSNCode.ForeColor = System.Drawing.Color.Blue
        Me.txtHSNCode.Location = New System.Drawing.Point(124, 50)
        Me.txtHSNCode.MaxLength = 0
        Me.txtHSNCode.Name = "txtHSNCode"
        Me.txtHSNCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtHSNCode.Size = New System.Drawing.Size(81, 22)
        Me.txtHSNCode.TabIndex = 32
        '
        'cboGSTClass
        '
        Me.cboGSTClass.BackColor = System.Drawing.SystemColors.Window
        Me.cboGSTClass.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboGSTClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboGSTClass.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboGSTClass.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboGSTClass.Location = New System.Drawing.Point(124, 18)
        Me.cboGSTClass.Name = "cboGSTClass"
        Me.cboGSTClass.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboGSTClass.Size = New System.Drawing.Size(105, 21)
        Me.cboGSTClass.TabIndex = 31
        '
        'txtProdType
        '
        Me.txtProdType.AcceptsReturn = True
        Me.txtProdType.BackColor = System.Drawing.SystemColors.Window
        Me.txtProdType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtProdType.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtProdType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProdType.ForeColor = System.Drawing.Color.Blue
        Me.txtProdType.Location = New System.Drawing.Point(124, 182)
        Me.txtProdType.MaxLength = 0
        Me.txtProdType.Name = "txtProdType"
        Me.txtProdType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProdType.Size = New System.Drawing.Size(259, 22)
        Me.txtProdType.TabIndex = 154
        '
        'txtColor
        '
        Me.txtColor.AcceptsReturn = True
        Me.txtColor.BackColor = System.Drawing.SystemColors.Window
        Me.txtColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtColor.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtColor.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtColor.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtColor.Location = New System.Drawing.Point(476, 149)
        Me.txtColor.MaxLength = 7
        Me.txtColor.Name = "txtColor"
        Me.txtColor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtColor.Size = New System.Drawing.Size(105, 22)
        Me.txtColor.TabIndex = 42
        '
        'txtItemMake
        '
        Me.txtItemMake.AcceptsReturn = True
        Me.txtItemMake.BackColor = System.Drawing.SystemColors.Window
        Me.txtItemMake.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemMake.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItemMake.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemMake.ForeColor = System.Drawing.Color.Blue
        Me.txtItemMake.Location = New System.Drawing.Point(290, 149)
        Me.txtItemMake.MaxLength = 0
        Me.txtItemMake.Name = "txtItemMake"
        Me.txtItemMake.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemMake.Size = New System.Drawing.Size(127, 22)
        Me.txtItemMake.TabIndex = 41
        '
        'txtModel
        '
        Me.txtModel.AcceptsReturn = True
        Me.txtModel.BackColor = System.Drawing.SystemColors.Window
        Me.txtModel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModel.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtModel.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtModel.ForeColor = System.Drawing.Color.Blue
        Me.txtModel.Location = New System.Drawing.Point(124, 149)
        Me.txtModel.MaxLength = 0
        Me.txtModel.Name = "txtModel"
        Me.txtModel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtModel.Size = New System.Drawing.Size(81, 22)
        Me.txtModel.TabIndex = 39
        '
        'txtScrapItemCode
        '
        Me.txtScrapItemCode.AcceptsReturn = True
        Me.txtScrapItemCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtScrapItemCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtScrapItemCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtScrapItemCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtScrapItemCode.ForeColor = System.Drawing.Color.Blue
        Me.txtScrapItemCode.Location = New System.Drawing.Point(124, 116)
        Me.txtScrapItemCode.MaxLength = 0
        Me.txtScrapItemCode.Name = "txtScrapItemCode"
        Me.txtScrapItemCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtScrapItemCode.Size = New System.Drawing.Size(81, 22)
        Me.txtScrapItemCode.TabIndex = 37
        '
        'CboStatus
        '
        Me.CboStatus.BackColor = System.Drawing.SystemColors.Window
        Me.CboStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.CboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboStatus.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboStatus.ForeColor = System.Drawing.SystemColors.WindowText
        Me.CboStatus.Location = New System.Drawing.Point(473, 16)
        Me.CboStatus.Name = "CboStatus"
        Me.CboStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CboStatus.Size = New System.Drawing.Size(105, 21)
        Me.CboStatus.TabIndex = 43
        '
        'txtPackingItemCode
        '
        Me.txtPackingItemCode.AcceptsReturn = True
        Me.txtPackingItemCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtPackingItemCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPackingItemCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPackingItemCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPackingItemCode.ForeColor = System.Drawing.Color.Blue
        Me.txtPackingItemCode.Location = New System.Drawing.Point(124, 83)
        Me.txtPackingItemCode.MaxLength = 0
        Me.txtPackingItemCode.Name = "txtPackingItemCode"
        Me.txtPackingItemCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPackingItemCode.Size = New System.Drawing.Size(81, 22)
        Me.txtPackingItemCode.TabIndex = 35
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.chkPOReqd)
        Me.Frame1.Controls.Add(Me.chkStockItem)
        Me.Frame1.Controls.Add(Me.chkRequired)
        Me.Frame1.Controls.Add(Me.chkAutoIndent)
        Me.Frame1.Controls.Add(Me.chkDrawing)
        Me.Frame1.Controls.Add(Me.chkConsumable)
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(850, 10)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(149, 137)
        Me.Frame1.TabIndex = 83
        Me.Frame1.TabStop = False
        '
        'chkPOReqd
        '
        Me.chkPOReqd.BackColor = System.Drawing.SystemColors.Control
        Me.chkPOReqd.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPOReqd.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPOReqd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPOReqd.Location = New System.Drawing.Point(4, 116)
        Me.chkPOReqd.Name = "chkPOReqd"
        Me.chkPOReqd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPOReqd.Size = New System.Drawing.Size(141, 18)
        Me.chkPOReqd.TabIndex = 49
        Me.chkPOReqd.Text = "PO Required"
        Me.chkPOReqd.UseVisualStyleBackColor = False
        '
        'chkStockItem
        '
        Me.chkStockItem.BackColor = System.Drawing.SystemColors.Control
        Me.chkStockItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkStockItem.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkStockItem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkStockItem.Location = New System.Drawing.Point(4, 96)
        Me.chkStockItem.Name = "chkStockItem"
        Me.chkStockItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkStockItem.Size = New System.Drawing.Size(141, 18)
        Me.chkStockItem.TabIndex = 48
        Me.chkStockItem.Text = "Stock Item"
        Me.chkStockItem.UseVisualStyleBackColor = False
        '
        'chkRequired
        '
        Me.chkRequired.BackColor = System.Drawing.SystemColors.Control
        Me.chkRequired.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkRequired.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRequired.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRequired.Location = New System.Drawing.Point(4, 76)
        Me.chkRequired.Name = "chkRequired"
        Me.chkRequired.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkRequired.Size = New System.Drawing.Size(141, 18)
        Me.chkRequired.TabIndex = 47
        Me.chkRequired.Text = "Batch No Required"
        Me.chkRequired.UseVisualStyleBackColor = False
        '
        'chkAutoIndent
        '
        Me.chkAutoIndent.BackColor = System.Drawing.SystemColors.Control
        Me.chkAutoIndent.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAutoIndent.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAutoIndent.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAutoIndent.Location = New System.Drawing.Point(4, 56)
        Me.chkAutoIndent.Name = "chkAutoIndent"
        Me.chkAutoIndent.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAutoIndent.Size = New System.Drawing.Size(141, 18)
        Me.chkAutoIndent.TabIndex = 46
        Me.chkAutoIndent.Text = "Auto Indenting"
        Me.chkAutoIndent.UseVisualStyleBackColor = False
        '
        'chkDrawing
        '
        Me.chkDrawing.BackColor = System.Drawing.SystemColors.Control
        Me.chkDrawing.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkDrawing.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDrawing.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkDrawing.Location = New System.Drawing.Point(4, 36)
        Me.chkDrawing.Name = "chkDrawing"
        Me.chkDrawing.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkDrawing.Size = New System.Drawing.Size(141, 18)
        Me.chkDrawing.TabIndex = 45
        Me.chkDrawing.Text = "Drawing Available"
        Me.chkDrawing.UseVisualStyleBackColor = False
        '
        'chkConsumable
        '
        Me.chkConsumable.BackColor = System.Drawing.SystemColors.Control
        Me.chkConsumable.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkConsumable.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkConsumable.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkConsumable.Location = New System.Drawing.Point(4, 16)
        Me.chkConsumable.Name = "chkConsumable"
        Me.chkConsumable.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkConsumable.Size = New System.Drawing.Size(141, 18)
        Me.chkConsumable.TabIndex = 44
        Me.chkConsumable.Text = "Consumable"
        Me.chkConsumable.UseVisualStyleBackColor = False
        '
        'lblHSNName
        '
        Me.lblHSNName.BackColor = System.Drawing.SystemColors.Control
        Me.lblHSNName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblHSNName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblHSNName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHSNName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblHSNName.Location = New System.Drawing.Point(232, 50)
        Me.lblHSNName.Name = "lblHSNName"
        Me.lblHSNName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblHSNName.Size = New System.Drawing.Size(347, 19)
        Me.lblHSNName.TabIndex = 34
        Me.lblHSNName.Text = "lblHSNName"
        '
        '_lblUnder_58
        '
        Me._lblUnder_58.AutoSize = True
        Me._lblUnder_58.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_58.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_58.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_58.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_58, CType(58, Short))
        Me._lblUnder_58.Location = New System.Drawing.Point(54, 55)
        Me._lblUnder_58.Name = "_lblUnder_58"
        Me._lblUnder_58.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_58.Size = New System.Drawing.Size(65, 13)
        Me._lblUnder_58.TabIndex = 157
        Me._lblUnder_58.Text = "HSN Code :"
        Me._lblUnder_58.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_59
        '
        Me._lblUnder_59.AutoSize = True
        Me._lblUnder_59.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_59.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_59.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_59.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_59, CType(59, Short))
        Me._lblUnder_59.Location = New System.Drawing.Point(19, 22)
        Me._lblUnder_59.Name = "_lblUnder_59"
        Me._lblUnder_59.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_59.Size = New System.Drawing.Size(100, 13)
        Me._lblUnder_59.TabIndex = 156
        Me._lblUnder_59.Text = "GST Classification:"
        Me._lblUnder_59.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_47
        '
        Me._lblUnder_47.AutoSize = True
        Me._lblUnder_47.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_47.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_47.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_47.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_47, CType(47, Short))
        Me._lblUnder_47.Location = New System.Drawing.Point(40, 187)
        Me._lblUnder_47.Name = "_lblUnder_47"
        Me._lblUnder_47.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_47.Size = New System.Drawing.Size(79, 13)
        Me._lblUnder_47.TabIndex = 155
        Me._lblUnder_47.Text = "Product Type :"
        Me._lblUnder_47.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_23
        '
        Me._lblUnder_23.AutoSize = True
        Me._lblUnder_23.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_23.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_23.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_23, CType(23, Short))
        Me._lblUnder_23.Location = New System.Drawing.Point(73, 153)
        Me._lblUnder_23.Name = "_lblUnder_23"
        Me._lblUnder_23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_23.Size = New System.Drawing.Size(46, 13)
        Me._lblUnder_23.TabIndex = 148
        Me._lblUnder_23.Text = "Model :"
        Me._lblUnder_23.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_24
        '
        Me._lblUnder_24.AutoSize = True
        Me._lblUnder_24.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_24.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_24.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_24.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_24, CType(24, Short))
        Me._lblUnder_24.Location = New System.Drawing.Point(245, 153)
        Me._lblUnder_24.Name = "_lblUnder_24"
        Me._lblUnder_24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_24.Size = New System.Drawing.Size(41, 13)
        Me._lblUnder_24.TabIndex = 147
        Me._lblUnder_24.Text = "Make :"
        Me._lblUnder_24.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_25
        '
        Me._lblUnder_25.AutoSize = True
        Me._lblUnder_25.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_25.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_25.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_25.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_25, CType(25, Short))
        Me._lblUnder_25.Location = New System.Drawing.Point(430, 153)
        Me._lblUnder_25.Name = "_lblUnder_25"
        Me._lblUnder_25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_25.Size = New System.Drawing.Size(41, 13)
        Me._lblUnder_25.TabIndex = 146
        Me._lblUnder_25.Text = "Color :"
        Me._lblUnder_25.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.BackColor = System.Drawing.SystemColors.Control
        Me.Label44.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label44.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label44.Location = New System.Drawing.Point(4, 220)
        Me.Label44.Name = "Label44"
        Me.Label44.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label44.Size = New System.Drawing.Size(61, 15)
        Me.Label44.TabIndex = 133
        Me.Label44.Text = "Add User :"
        Me.Label44.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblAddUser
        '
        Me.lblAddUser.BackColor = System.Drawing.SystemColors.Control
        Me.lblAddUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAddUser.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAddUser.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddUser.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAddUser.Location = New System.Drawing.Point(72, 218)
        Me.lblAddUser.Name = "lblAddUser"
        Me.lblAddUser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAddUser.Size = New System.Drawing.Size(69, 19)
        Me.lblAddUser.TabIndex = 132
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.BackColor = System.Drawing.SystemColors.Control
        Me.Label46.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label46.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label46.Location = New System.Drawing.Point(297, 220)
        Me.Label46.Name = "Label46"
        Me.Label46.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label46.Size = New System.Drawing.Size(61, 15)
        Me.Label46.TabIndex = 131
        Me.Label46.Text = "Mod User:"
        Me.Label46.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblModUser
        '
        Me.lblModUser.BackColor = System.Drawing.SystemColors.Control
        Me.lblModUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblModUser.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblModUser.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModUser.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblModUser.Location = New System.Drawing.Point(373, 218)
        Me.lblModUser.Name = "lblModUser"
        Me.lblModUser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblModUser.Size = New System.Drawing.Size(69, 19)
        Me.lblModUser.TabIndex = 130
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.BackColor = System.Drawing.SystemColors.Control
        Me.Label45.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label45.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label45.Location = New System.Drawing.Point(147, 220)
        Me.Label45.Name = "Label45"
        Me.Label45.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label45.Size = New System.Drawing.Size(63, 15)
        Me.Label45.TabIndex = 129
        Me.Label45.Text = "Add Date :"
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblAddDate
        '
        Me.lblAddDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblAddDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAddDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAddDate.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAddDate.Location = New System.Drawing.Point(216, 218)
        Me.lblAddDate.Name = "lblAddDate"
        Me.lblAddDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAddDate.Size = New System.Drawing.Size(69, 19)
        Me.lblAddDate.TabIndex = 128
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.BackColor = System.Drawing.SystemColors.Control
        Me.Label48.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label48.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label48.Location = New System.Drawing.Point(448, 220)
        Me.Label48.Name = "Label48"
        Me.Label48.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label48.Size = New System.Drawing.Size(63, 15)
        Me.Label48.TabIndex = 127
        Me.Label48.Text = "Mod Date:"
        Me.Label48.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblModDate
        '
        Me.lblModDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblModDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblModDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblModDate.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblModDate.Location = New System.Drawing.Point(512, 218)
        Me.lblModDate.Name = "lblModDate"
        Me.lblModDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblModDate.Size = New System.Drawing.Size(69, 19)
        Me.lblModDate.TabIndex = 126
        '
        'lblScrapItemName
        '
        Me.lblScrapItemName.BackColor = System.Drawing.SystemColors.Control
        Me.lblScrapItemName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblScrapItemName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblScrapItemName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblScrapItemName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblScrapItemName.Location = New System.Drawing.Point(232, 116)
        Me.lblScrapItemName.Name = "lblScrapItemName"
        Me.lblScrapItemName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblScrapItemName.Size = New System.Drawing.Size(347, 19)
        Me.lblScrapItemName.TabIndex = 82
        Me.lblScrapItemName.Text = "lblScrapItemName"
        '
        '_lblUnder_10
        '
        Me._lblUnder_10.AutoSize = True
        Me._lblUnder_10.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_10.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_10.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_10, CType(10, Short))
        Me._lblUnder_10.Location = New System.Drawing.Point(22, 121)
        Me._lblUnder_10.Name = "_lblUnder_10"
        Me._lblUnder_10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_10.Size = New System.Drawing.Size(97, 13)
        Me._lblUnder_10.TabIndex = 117
        Me._lblUnder_10.Text = "Scrap Item Code :"
        Me._lblUnder_10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_20
        '
        Me._lblUnder_20.AutoSize = True
        Me._lblUnder_20.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_20.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_20.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_20, CType(20, Short))
        Me._lblUnder_20.Location = New System.Drawing.Point(429, 20)
        Me._lblUnder_20.Name = "_lblUnder_20"
        Me._lblUnder_20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_20.Size = New System.Drawing.Size(41, 13)
        Me._lblUnder_20.TabIndex = 116
        Me._lblUnder_20.Text = "Status:"
        Me._lblUnder_20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblPackItemName
        '
        Me.lblPackItemName.BackColor = System.Drawing.SystemColors.Control
        Me.lblPackItemName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPackItemName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPackItemName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPackItemName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPackItemName.Location = New System.Drawing.Point(232, 83)
        Me.lblPackItemName.Name = "lblPackItemName"
        Me.lblPackItemName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPackItemName.Size = New System.Drawing.Size(347, 19)
        Me.lblPackItemName.TabIndex = 81
        Me.lblPackItemName.Text = "lblPackItemName"
        '
        '_lblUnder_32
        '
        Me._lblUnder_32.AutoSize = True
        Me._lblUnder_32.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_32.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_32.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_32.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_32, CType(32, Short))
        Me._lblUnder_32.Location = New System.Drawing.Point(11, 88)
        Me._lblUnder_32.Name = "_lblUnder_32"
        Me._lblUnder_32.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_32.Size = New System.Drawing.Size(108, 13)
        Me._lblUnder_32.TabIndex = 115
        Me._lblUnder_32.Text = "Packing Item Code :"
        Me._lblUnder_32.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_SSTInfo_TabPage1
        '
        Me._SSTInfo_TabPage1.Controls.Add(Me._lblUnder_3)
        Me._SSTInfo_TabPage1.Controls.Add(Me._lblUnder_26)
        Me._SSTInfo_TabPage1.Controls.Add(Me._lblUnder_28)
        Me._SSTInfo_TabPage1.Controls.Add(Me._lblUnder_29)
        Me._SSTInfo_TabPage1.Controls.Add(Me._lblUnder_33)
        Me._SSTInfo_TabPage1.Controls.Add(Me.lblTacks)
        Me._SSTInfo_TabPage1.Controls.Add(Me._lblUnder_41)
        Me._SSTInfo_TabPage1.Controls.Add(Me._lblUnder_12)
        Me._SSTInfo_TabPage1.Controls.Add(Me._lblUnder_31)
        Me._SSTInfo_TabPage1.Controls.Add(Me._lblUnder_42)
        Me._SSTInfo_TabPage1.Controls.Add(Me._lblUnder_43)
        Me._SSTInfo_TabPage1.Controls.Add(Me._lblUnder_44)
        Me._SSTInfo_TabPage1.Controls.Add(Me._lblUnder_45)
        Me._SSTInfo_TabPage1.Controls.Add(Me._lblUnder_13)
        Me._SSTInfo_TabPage1.Controls.Add(Me._lblUnder_34)
        Me._SSTInfo_TabPage1.Controls.Add(Me._lblUnder_38)
        Me._SSTInfo_TabPage1.Controls.Add(Me._lblUnder_37)
        Me._SSTInfo_TabPage1.Controls.Add(Me._lblUnder_35)
        Me._SSTInfo_TabPage1.Controls.Add(Me._lblUnder_36)
        Me._SSTInfo_TabPage1.Controls.Add(Me._lblUnder_40)
        Me._SSTInfo_TabPage1.Controls.Add(Me.txtSurfaceTreatment)
        Me._SSTInfo_TabPage1.Controls.Add(Me.txtWeight)
        Me._SSTInfo_TabPage1.Controls.Add(Me.txtDimention)
        Me._SSTInfo_TabPage1.Controls.Add(Me.txtSpecification)
        Me._SSTInfo_TabPage1.Controls.Add(Me.txtSurfaceArea)
        Me._SSTInfo_TabPage1.Controls.Add(Me.txtTacks)
        Me._SSTInfo_TabPage1.Controls.Add(Me.txtScrapWeight)
        Me._SSTInfo_TabPage1.Controls.Add(Me.txtWLength)
        Me._SSTInfo_TabPage1.Controls.Add(Me.txtMaterial)
        Me._SSTInfo_TabPage1.Controls.Add(Me.txtThickness)
        Me._SSTInfo_TabPage1.Controls.Add(Me.txtWidth)
        Me._SSTInfo_TabPage1.Controls.Add(Me.txtLength)
        Me._SSTInfo_TabPage1.Controls.Add(Me.txtDensity)
        Me._SSTInfo_TabPage1.Controls.Add(Me.txtLocation)
        Me._SSTInfo_TabPage1.Controls.Add(Me.txtInspectionNo)
        Me._SSTInfo_TabPage1.Controls.Add(Me.txtIdMark)
        Me._SSTInfo_TabPage1.Controls.Add(Me.txtDwgRevDate)
        Me._SSTInfo_TabPage1.Controls.Add(Me.txtDwgNo)
        Me._SSTInfo_TabPage1.Controls.Add(Me.txtDwgRevNo)
        Me._SSTInfo_TabPage1.Controls.Add(Me.txtPackingStandard)
        Me._SSTInfo_TabPage1.Location = New System.Drawing.Point(4, 22)
        Me._SSTInfo_TabPage1.Name = "_SSTInfo_TabPage1"
        Me._SSTInfo_TabPage1.Size = New System.Drawing.Size(996, 248)
        Me._SSTInfo_TabPage1.TabIndex = 1
        Me._SSTInfo_TabPage1.Text = "Technical"
        '
        '_lblUnder_3
        '
        Me._lblUnder_3.AutoSize = True
        Me._lblUnder_3.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_3, CType(3, Short))
        Me._lblUnder_3.Location = New System.Drawing.Point(38, 138)
        Me._lblUnder_3.Name = "_lblUnder_3"
        Me._lblUnder_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_3.Size = New System.Drawing.Size(104, 13)
        Me._lblUnder_3.TabIndex = 124
        Me._lblUnder_3.Text = "Surface Treatment :"
        Me._lblUnder_3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_26
        '
        Me._lblUnder_26.AutoSize = True
        Me._lblUnder_26.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_26.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_26.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_26, CType(26, Short))
        Me._lblUnder_26.Location = New System.Drawing.Point(36, 111)
        Me._lblUnder_26.Name = "_lblUnder_26"
        Me._lblUnder_26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_26.Size = New System.Drawing.Size(106, 13)
        Me._lblUnder_26.TabIndex = 118
        Me._lblUnder_26.Text = "Weight (In Grams) :"
        Me._lblUnder_26.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_28
        '
        Me._lblUnder_28.AutoSize = True
        Me._lblUnder_28.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_28.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_28.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_28.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_28, CType(28, Short))
        Me._lblUnder_28.Location = New System.Drawing.Point(313, 84)
        Me._lblUnder_28.Name = "_lblUnder_28"
        Me._lblUnder_28.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_28.Size = New System.Drawing.Size(66, 13)
        Me._lblUnder_28.TabIndex = 119
        Me._lblUnder_28.Text = "Dimention :"
        Me._lblUnder_28.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_29
        '
        Me._lblUnder_29.AutoSize = True
        Me._lblUnder_29.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_29.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_29.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_29.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_29, CType(29, Short))
        Me._lblUnder_29.Location = New System.Drawing.Point(552, 84)
        Me._lblUnder_29.Name = "_lblUnder_29"
        Me._lblUnder_29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_29.Size = New System.Drawing.Size(78, 13)
        Me._lblUnder_29.TabIndex = 120
        Me._lblUnder_29.Text = "Specification :"
        Me._lblUnder_29.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_33
        '
        Me._lblUnder_33.AutoSize = True
        Me._lblUnder_33.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_33.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_33.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_33.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_33, CType(33, Short))
        Me._lblUnder_33.Location = New System.Drawing.Point(554, 111)
        Me._lblUnder_33.Name = "_lblUnder_33"
        Me._lblUnder_33.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_33.Size = New System.Drawing.Size(76, 13)
        Me._lblUnder_33.TabIndex = 121
        Me._lblUnder_33.Text = "Surface Area :"
        Me._lblUnder_33.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTacks
        '
        Me.lblTacks.AutoSize = True
        Me.lblTacks.BackColor = System.Drawing.SystemColors.Control
        Me.lblTacks.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTacks.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTacks.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTacks.Location = New System.Drawing.Point(596, 138)
        Me.lblTacks.Name = "lblTacks"
        Me.lblTacks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTacks.Size = New System.Drawing.Size(34, 13)
        Me.lblTacks.TabIndex = 122
        Me.lblTacks.Text = "Tacks"
        Me.lblTacks.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_41
        '
        Me._lblUnder_41.AutoSize = True
        Me._lblUnder_41.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_41.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_41.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_41.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_41, CType(41, Short))
        Me._lblUnder_41.Location = New System.Drawing.Point(298, 111)
        Me._lblUnder_41.Name = "_lblUnder_41"
        Me._lblUnder_41.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_41.Size = New System.Drawing.Size(81, 13)
        Me._lblUnder_41.TabIndex = 123
        Me._lblUnder_41.Text = "Scrap Weight :"
        Me._lblUnder_41.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_12
        '
        Me._lblUnder_12.AutoSize = True
        Me._lblUnder_12.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_12.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_12.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_12, CType(12, Short))
        Me._lblUnder_12.Location = New System.Drawing.Point(271, 138)
        Me._lblUnder_12.Name = "_lblUnder_12"
        Me._lblUnder_12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_12.Size = New System.Drawing.Size(108, 13)
        Me._lblUnder_12.TabIndex = 125
        Me._lblUnder_12.Text = "Weld Length (inch) :"
        Me._lblUnder_12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_31
        '
        Me._lblUnder_31.AutoSize = True
        Me._lblUnder_31.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_31.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_31.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_31.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_31, CType(31, Short))
        Me._lblUnder_31.Location = New System.Drawing.Point(26, 30)
        Me._lblUnder_31.Name = "_lblUnder_31"
        Me._lblUnder_31.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_31.Size = New System.Drawing.Size(116, 13)
        Me._lblUnder_31.TabIndex = 135
        Me._lblUnder_31.Text = "Material Description :"
        Me._lblUnder_31.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_42
        '
        Me._lblUnder_42.AutoSize = True
        Me._lblUnder_42.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_42.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_42.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_42.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_42, CType(42, Short))
        Me._lblUnder_42.Location = New System.Drawing.Point(569, 57)
        Me._lblUnder_42.Name = "_lblUnder_42"
        Me._lblUnder_42.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_42.Size = New System.Drawing.Size(61, 13)
        Me._lblUnder_42.TabIndex = 136
        Me._lblUnder_42.Text = "Thickness :"
        Me._lblUnder_42.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_43
        '
        Me._lblUnder_43.AutoSize = True
        Me._lblUnder_43.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_43.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_43.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_43.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_43, CType(43, Short))
        Me._lblUnder_43.Location = New System.Drawing.Point(335, 57)
        Me._lblUnder_43.Name = "_lblUnder_43"
        Me._lblUnder_43.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_43.Size = New System.Drawing.Size(44, 13)
        Me._lblUnder_43.TabIndex = 137
        Me._lblUnder_43.Text = "Width :"
        Me._lblUnder_43.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_44
        '
        Me._lblUnder_44.AutoSize = True
        Me._lblUnder_44.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_44.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_44.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_44.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_44, CType(44, Short))
        Me._lblUnder_44.Location = New System.Drawing.Point(95, 57)
        Me._lblUnder_44.Name = "_lblUnder_44"
        Me._lblUnder_44.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_44.Size = New System.Drawing.Size(47, 13)
        Me._lblUnder_44.TabIndex = 138
        Me._lblUnder_44.Text = "Length :"
        Me._lblUnder_44.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_45
        '
        Me._lblUnder_45.AutoSize = True
        Me._lblUnder_45.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_45.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_45.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_45.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_45, CType(45, Short))
        Me._lblUnder_45.Location = New System.Drawing.Point(91, 84)
        Me._lblUnder_45.Name = "_lblUnder_45"
        Me._lblUnder_45.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_45.Size = New System.Drawing.Size(51, 13)
        Me._lblUnder_45.TabIndex = 139
        Me._lblUnder_45.Text = "Density :"
        Me._lblUnder_45.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_13
        '
        Me._lblUnder_13.AutoSize = True
        Me._lblUnder_13.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_13.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_13.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_13, CType(13, Short))
        Me._lblUnder_13.Location = New System.Drawing.Point(574, 219)
        Me._lblUnder_13.Name = "_lblUnder_13"
        Me._lblUnder_13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_13.Size = New System.Drawing.Size(56, 13)
        Me._lblUnder_13.TabIndex = 140
        Me._lblUnder_13.Text = "Location :"
        Me._lblUnder_13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_34
        '
        Me._lblUnder_34.AutoSize = True
        Me._lblUnder_34.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_34.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_34.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_34.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_34, CType(34, Short))
        Me._lblUnder_34.Location = New System.Drawing.Point(90, 192)
        Me._lblUnder_34.Name = "_lblUnder_34"
        Me._lblUnder_34.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_34.Size = New System.Drawing.Size(52, 13)
        Me._lblUnder_34.TabIndex = 141
        Me._lblUnder_34.Text = "QAS No :"
        Me._lblUnder_34.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_38
        '
        Me._lblUnder_38.AutoSize = True
        Me._lblUnder_38.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_38.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_38.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_38.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_38, CType(38, Short))
        Me._lblUnder_38.Location = New System.Drawing.Point(308, 219)
        Me._lblUnder_38.Name = "_lblUnder_38"
        Me._lblUnder_38.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_38.Size = New System.Drawing.Size(71, 13)
        Me._lblUnder_38.TabIndex = 142
        Me._lblUnder_38.Text = "Ident. Mark :"
        Me._lblUnder_38.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_37
        '
        Me._lblUnder_37.AutoSize = True
        Me._lblUnder_37.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_37.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_37.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_37.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_37, CType(37, Short))
        Me._lblUnder_37.Location = New System.Drawing.Point(529, 165)
        Me._lblUnder_37.Name = "_lblUnder_37"
        Me._lblUnder_37.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_37.Size = New System.Drawing.Size(101, 13)
        Me._lblUnder_37.TabIndex = 143
        Me._lblUnder_37.Text = "Drawing Eff. Date :"
        Me._lblUnder_37.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_35
        '
        Me._lblUnder_35.AutoSize = True
        Me._lblUnder_35.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_35.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_35.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_35.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_35, CType(35, Short))
        Me._lblUnder_35.Location = New System.Drawing.Point(69, 165)
        Me._lblUnder_35.Name = "_lblUnder_35"
        Me._lblUnder_35.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_35.Size = New System.Drawing.Size(73, 13)
        Me._lblUnder_35.TabIndex = 144
        Me._lblUnder_35.Text = "Drawing No :"
        Me._lblUnder_35.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_36
        '
        Me._lblUnder_36.AutoSize = True
        Me._lblUnder_36.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_36.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_36.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_36.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_36, CType(36, Short))
        Me._lblUnder_36.Location = New System.Drawing.Point(284, 165)
        Me._lblUnder_36.Name = "_lblUnder_36"
        Me._lblUnder_36.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_36.Size = New System.Drawing.Size(95, 13)
        Me._lblUnder_36.TabIndex = 145
        Me._lblUnder_36.Text = "Drawing Rev No :"
        Me._lblUnder_36.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_40
        '
        Me._lblUnder_40.AutoSize = True
        Me._lblUnder_40.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_40.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_40.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_40.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_40, CType(40, Short))
        Me._lblUnder_40.Location = New System.Drawing.Point(41, 219)
        Me._lblUnder_40.Name = "_lblUnder_40"
        Me._lblUnder_40.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_40.Size = New System.Drawing.Size(101, 13)
        Me._lblUnder_40.TabIndex = 152
        Me._lblUnder_40.Text = "Packing Standard :"
        Me._lblUnder_40.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtSurfaceTreatment
        '
        Me.txtSurfaceTreatment.AcceptsReturn = True
        Me.txtSurfaceTreatment.BackColor = System.Drawing.SystemColors.Window
        Me.txtSurfaceTreatment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSurfaceTreatment.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSurfaceTreatment.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSurfaceTreatment.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSurfaceTreatment.Location = New System.Drawing.Point(146, 134)
        Me.txtSurfaceTreatment.MaxLength = 7
        Me.txtSurfaceTreatment.Name = "txtSurfaceTreatment"
        Me.txtSurfaceTreatment.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSurfaceTreatment.Size = New System.Drawing.Size(105, 22)
        Me.txtSurfaceTreatment.TabIndex = 60
        '
        'txtWeight
        '
        Me.txtWeight.AcceptsReturn = True
        Me.txtWeight.BackColor = System.Drawing.SystemColors.Window
        Me.txtWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWeight.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWeight.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWeight.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtWeight.Location = New System.Drawing.Point(146, 107)
        Me.txtWeight.MaxLength = 7
        Me.txtWeight.Name = "txtWeight"
        Me.txtWeight.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWeight.Size = New System.Drawing.Size(105, 22)
        Me.txtWeight.TabIndex = 57
        '
        'txtDimention
        '
        Me.txtDimention.AcceptsReturn = True
        Me.txtDimention.BackColor = System.Drawing.SystemColors.Window
        Me.txtDimention.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDimention.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDimention.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDimention.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDimention.Location = New System.Drawing.Point(384, 80)
        Me.txtDimention.MaxLength = 7
        Me.txtDimention.Name = "txtDimention"
        Me.txtDimention.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDimention.Size = New System.Drawing.Size(127, 22)
        Me.txtDimention.TabIndex = 55
        '
        'txtSpecification
        '
        Me.txtSpecification.AcceptsReturn = True
        Me.txtSpecification.BackColor = System.Drawing.SystemColors.Window
        Me.txtSpecification.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSpecification.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSpecification.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSpecification.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSpecification.Location = New System.Drawing.Point(634, 80)
        Me.txtSpecification.MaxLength = 7
        Me.txtSpecification.Name = "txtSpecification"
        Me.txtSpecification.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSpecification.Size = New System.Drawing.Size(105, 22)
        Me.txtSpecification.TabIndex = 56
        '
        'txtSurfaceArea
        '
        Me.txtSurfaceArea.AcceptsReturn = True
        Me.txtSurfaceArea.BackColor = System.Drawing.SystemColors.Window
        Me.txtSurfaceArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSurfaceArea.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSurfaceArea.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSurfaceArea.ForeColor = System.Drawing.Color.Blue
        Me.txtSurfaceArea.Location = New System.Drawing.Point(634, 107)
        Me.txtSurfaceArea.MaxLength = 0
        Me.txtSurfaceArea.Name = "txtSurfaceArea"
        Me.txtSurfaceArea.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSurfaceArea.Size = New System.Drawing.Size(105, 22)
        Me.txtSurfaceArea.TabIndex = 59
        '
        'txtTacks
        '
        Me.txtTacks.AcceptsReturn = True
        Me.txtTacks.BackColor = System.Drawing.SystemColors.Window
        Me.txtTacks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTacks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTacks.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTacks.ForeColor = System.Drawing.Color.Blue
        Me.txtTacks.Location = New System.Drawing.Point(634, 134)
        Me.txtTacks.MaxLength = 0
        Me.txtTacks.Name = "txtTacks"
        Me.txtTacks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTacks.Size = New System.Drawing.Size(105, 22)
        Me.txtTacks.TabIndex = 62
        '
        'txtScrapWeight
        '
        Me.txtScrapWeight.AcceptsReturn = True
        Me.txtScrapWeight.BackColor = System.Drawing.SystemColors.Window
        Me.txtScrapWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtScrapWeight.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtScrapWeight.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtScrapWeight.ForeColor = System.Drawing.Color.Blue
        Me.txtScrapWeight.Location = New System.Drawing.Point(384, 107)
        Me.txtScrapWeight.MaxLength = 0
        Me.txtScrapWeight.Name = "txtScrapWeight"
        Me.txtScrapWeight.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtScrapWeight.Size = New System.Drawing.Size(127, 22)
        Me.txtScrapWeight.TabIndex = 58
        '
        'txtWLength
        '
        Me.txtWLength.AcceptsReturn = True
        Me.txtWLength.BackColor = System.Drawing.SystemColors.Window
        Me.txtWLength.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWLength.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWLength.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWLength.ForeColor = System.Drawing.Color.Blue
        Me.txtWLength.Location = New System.Drawing.Point(384, 134)
        Me.txtWLength.MaxLength = 0
        Me.txtWLength.Name = "txtWLength"
        Me.txtWLength.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWLength.Size = New System.Drawing.Size(127, 22)
        Me.txtWLength.TabIndex = 61
        '
        'txtMaterial
        '
        Me.txtMaterial.AcceptsReturn = True
        Me.txtMaterial.BackColor = System.Drawing.SystemColors.Window
        Me.txtMaterial.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMaterial.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMaterial.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMaterial.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtMaterial.Location = New System.Drawing.Point(146, 26)
        Me.txtMaterial.MaxLength = 7
        Me.txtMaterial.Name = "txtMaterial"
        Me.txtMaterial.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMaterial.Size = New System.Drawing.Size(591, 22)
        Me.txtMaterial.TabIndex = 50
        '
        'txtThickness
        '
        Me.txtThickness.AcceptsReturn = True
        Me.txtThickness.BackColor = System.Drawing.SystemColors.Window
        Me.txtThickness.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtThickness.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtThickness.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtThickness.ForeColor = System.Drawing.Color.Blue
        Me.txtThickness.Location = New System.Drawing.Point(634, 53)
        Me.txtThickness.MaxLength = 0
        Me.txtThickness.Name = "txtThickness"
        Me.txtThickness.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtThickness.Size = New System.Drawing.Size(105, 22)
        Me.txtThickness.TabIndex = 53
        '
        'txtWidth
        '
        Me.txtWidth.AcceptsReturn = True
        Me.txtWidth.BackColor = System.Drawing.SystemColors.Window
        Me.txtWidth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWidth.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWidth.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWidth.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtWidth.Location = New System.Drawing.Point(384, 53)
        Me.txtWidth.MaxLength = 7
        Me.txtWidth.Name = "txtWidth"
        Me.txtWidth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWidth.Size = New System.Drawing.Size(127, 22)
        Me.txtWidth.TabIndex = 52
        '
        'txtLength
        '
        Me.txtLength.AcceptsReturn = True
        Me.txtLength.BackColor = System.Drawing.SystemColors.Window
        Me.txtLength.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLength.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLength.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLength.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtLength.Location = New System.Drawing.Point(146, 53)
        Me.txtLength.MaxLength = 7
        Me.txtLength.Name = "txtLength"
        Me.txtLength.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLength.Size = New System.Drawing.Size(105, 22)
        Me.txtLength.TabIndex = 51
        '
        'txtDensity
        '
        Me.txtDensity.AcceptsReturn = True
        Me.txtDensity.BackColor = System.Drawing.SystemColors.Window
        Me.txtDensity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDensity.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDensity.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDensity.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDensity.Location = New System.Drawing.Point(146, 80)
        Me.txtDensity.MaxLength = 7
        Me.txtDensity.Name = "txtDensity"
        Me.txtDensity.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDensity.Size = New System.Drawing.Size(105, 22)
        Me.txtDensity.TabIndex = 54
        '
        'txtLocation
        '
        Me.txtLocation.AcceptsReturn = True
        Me.txtLocation.BackColor = System.Drawing.SystemColors.Window
        Me.txtLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLocation.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLocation.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.ForeColor = System.Drawing.Color.Blue
        Me.txtLocation.Location = New System.Drawing.Point(634, 215)
        Me.txtLocation.MaxLength = 0
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLocation.Size = New System.Drawing.Size(105, 22)
        Me.txtLocation.TabIndex = 68
        '
        'txtInspectionNo
        '
        Me.txtInspectionNo.AcceptsReturn = True
        Me.txtInspectionNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtInspectionNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInspectionNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInspectionNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInspectionNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtInspectionNo.Location = New System.Drawing.Point(146, 188)
        Me.txtInspectionNo.MaxLength = 7
        Me.txtInspectionNo.Name = "txtInspectionNo"
        Me.txtInspectionNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInspectionNo.Size = New System.Drawing.Size(105, 22)
        Me.txtInspectionNo.TabIndex = 66
        '
        'txtIdMark
        '
        Me.txtIdMark.AcceptsReturn = True
        Me.txtIdMark.BackColor = System.Drawing.SystemColors.Window
        Me.txtIdMark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIdMark.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIdMark.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIdMark.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtIdMark.Location = New System.Drawing.Point(384, 215)
        Me.txtIdMark.MaxLength = 7
        Me.txtIdMark.Name = "txtIdMark"
        Me.txtIdMark.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIdMark.Size = New System.Drawing.Size(127, 22)
        Me.txtIdMark.TabIndex = 67
        '
        'txtDwgRevDate
        '
        Me.txtDwgRevDate.AcceptsReturn = True
        Me.txtDwgRevDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtDwgRevDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDwgRevDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDwgRevDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDwgRevDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDwgRevDate.Location = New System.Drawing.Point(634, 161)
        Me.txtDwgRevDate.MaxLength = 7
        Me.txtDwgRevDate.Name = "txtDwgRevDate"
        Me.txtDwgRevDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDwgRevDate.Size = New System.Drawing.Size(105, 22)
        Me.txtDwgRevDate.TabIndex = 65
        '
        'txtDwgNo
        '
        Me.txtDwgNo.AcceptsReturn = True
        Me.txtDwgNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtDwgNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDwgNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDwgNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDwgNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDwgNo.Location = New System.Drawing.Point(146, 161)
        Me.txtDwgNo.MaxLength = 7
        Me.txtDwgNo.Name = "txtDwgNo"
        Me.txtDwgNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDwgNo.Size = New System.Drawing.Size(105, 22)
        Me.txtDwgNo.TabIndex = 63
        '
        'txtDwgRevNo
        '
        Me.txtDwgRevNo.AcceptsReturn = True
        Me.txtDwgRevNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtDwgRevNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDwgRevNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDwgRevNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDwgRevNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDwgRevNo.Location = New System.Drawing.Point(384, 161)
        Me.txtDwgRevNo.MaxLength = 7
        Me.txtDwgRevNo.Name = "txtDwgRevNo"
        Me.txtDwgRevNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDwgRevNo.Size = New System.Drawing.Size(127, 22)
        Me.txtDwgRevNo.TabIndex = 64
        '
        'txtPackingStandard
        '
        Me.txtPackingStandard.AcceptsReturn = True
        Me.txtPackingStandard.BackColor = System.Drawing.SystemColors.Window
        Me.txtPackingStandard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPackingStandard.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPackingStandard.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPackingStandard.ForeColor = System.Drawing.Color.Blue
        Me.txtPackingStandard.Location = New System.Drawing.Point(145, 215)
        Me.txtPackingStandard.MaxLength = 0
        Me.txtPackingStandard.Name = "txtPackingStandard"
        Me.txtPackingStandard.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPackingStandard.Size = New System.Drawing.Size(105, 22)
        Me.txtPackingStandard.TabIndex = 151
        '
        'fraTop1
        '
        Me.fraTop1.BackColor = System.Drawing.SystemColors.Control
        Me.fraTop1.Controls.Add(Me.txtTechnicalDescription)
        Me.fraTop1.Controls.Add(Me.chkExportItem)
        Me.fraTop1.Controls.Add(Me.txtItemName)
        Me.fraTop1.Controls.Add(Me.txtItemCode)
        Me.fraTop1.Controls.Add(Me.cmdsearch)
        Me.fraTop1.Controls.Add(Me.lblItemName)
        Me.fraTop1.Controls.Add(Me._lblUnder_39)
        Me.fraTop1.Controls.Add(Me.Label2)
        Me.fraTop1.Controls.Add(Me.Label4)
        Me.fraTop1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTop1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraTop1.Location = New System.Drawing.Point(0, 0)
        Me.fraTop1.Name = "fraTop1"
        Me.fraTop1.Padding = New System.Windows.Forms.Padding(0)
        Me.fraTop1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraTop1.Size = New System.Drawing.Size(1004, 78)
        Me.fraTop1.TabIndex = 85
        Me.fraTop1.TabStop = False
        '
        'txtTechnicalDescription
        '
        Me.txtTechnicalDescription.AcceptsReturn = True
        Me.txtTechnicalDescription.BackColor = System.Drawing.SystemColors.Window
        Me.txtTechnicalDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTechnicalDescription.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTechnicalDescription.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTechnicalDescription.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTechnicalDescription.Location = New System.Drawing.Point(140, 43)
        Me.txtTechnicalDescription.MaxLength = 7
        Me.txtTechnicalDescription.Name = "txtTechnicalDescription"
        Me.txtTechnicalDescription.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTechnicalDescription.Size = New System.Drawing.Size(602, 22)
        Me.txtTechnicalDescription.TabIndex = 4
        '
        'chkExportItem
        '
        Me.chkExportItem.BackColor = System.Drawing.SystemColors.Control
        Me.chkExportItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkExportItem.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkExportItem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkExportItem.Location = New System.Drawing.Point(872, 43)
        Me.chkExportItem.Name = "chkExportItem"
        Me.chkExportItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkExportItem.Size = New System.Drawing.Size(103, 17)
        Me.chkExportItem.TabIndex = 5
        Me.chkExportItem.Text = "Export Item"
        Me.chkExportItem.UseVisualStyleBackColor = False
        '
        'txtItemName
        '
        Me.txtItemName.AcceptsReturn = True
        Me.txtItemName.BackColor = System.Drawing.SystemColors.Window
        Me.txtItemName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItemName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemName.ForeColor = System.Drawing.Color.Blue
        Me.txtItemName.Location = New System.Drawing.Point(140, 14)
        Me.txtItemName.MaxLength = 0
        Me.txtItemName.Name = "txtItemName"
        Me.txtItemName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemName.Size = New System.Drawing.Size(602, 22)
        Me.txtItemName.TabIndex = 1
        '
        'txtItemCode
        '
        Me.txtItemCode.AcceptsReturn = True
        Me.txtItemCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtItemCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItemCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemCode.ForeColor = System.Drawing.Color.Blue
        Me.txtItemCode.Location = New System.Drawing.Point(875, 14)
        Me.txtItemCode.MaxLength = 0
        Me.txtItemCode.Name = "txtItemCode"
        Me.txtItemCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemCode.Size = New System.Drawing.Size(101, 22)
        Me.txtItemCode.TabIndex = 3
        '
        'lblItemName
        '
        Me.lblItemName.BackColor = System.Drawing.SystemColors.Control
        Me.lblItemName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblItemName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItemName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblItemName.Location = New System.Drawing.Point(152, 16)
        Me.lblItemName.Name = "lblItemName"
        Me.lblItemName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblItemName.Size = New System.Drawing.Size(411, 15)
        Me.lblItemName.TabIndex = 150
        Me.lblItemName.Text = "lblItemName"
        Me.lblItemName.Visible = False
        '
        '_lblUnder_39
        '
        Me._lblUnder_39.AutoSize = True
        Me._lblUnder_39.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_39.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_39.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_39.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_39, CType(39, Short))
        Me._lblUnder_39.Location = New System.Drawing.Point(16, 48)
        Me._lblUnder_39.Name = "_lblUnder_39"
        Me._lblUnder_39.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_39.Size = New System.Drawing.Size(119, 13)
        Me._lblUnder_39.TabIndex = 134
        Me._lblUnder_39.Text = "Technical Description :"
        Me._lblUnder_39.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(66, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(69, 13)
        Me.Label2.TabIndex = 86
        Me.Label2.Text = "Item Name :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(829, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(40, 13)
        Me.Label4.TabIndex = 87
        Me.Label4.Text = "Code :"
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.Label1)
        Me.Frame2.Controls.Add(Me.txtPartNo)
        Me.Frame2.Controls.Add(Me.txtUOMFactor)
        Me.Frame2.Controls.Add(Me.cmdSearchPurUom)
        Me.Frame2.Controls.Add(Me.txtPurchaseUom)
        Me.Frame2.Controls.Add(Me.cmdSearchUom)
        Me.Frame2.Controls.Add(Me.txtItemUom)
        Me.Frame2.Controls.Add(Me.cmdSearchSubCat)
        Me.Frame2.Controls.Add(Me.txtSubCatName)
        Me.Frame2.Controls.Add(Me.cmdSearchCategory)
        Me.Frame2.Controls.Add(Me.txtCatName)
        Me.Frame2.Controls.Add(Me.lblPurUom)
        Me.Frame2.Controls.Add(Me.lblItemUom)
        Me.Frame2.Controls.Add(Me.lblSubCatName)
        Me.Frame2.Controls.Add(Me.lblCatName)
        Me.Frame2.Controls.Add(Me._lblUnder_27)
        Me.Frame2.Controls.Add(Me._lblUnder_2)
        Me.Frame2.Controls.Add(Me._lblUnder_4)
        Me.Frame2.Controls.Add(Me._lblUnder_5)
        Me.Frame2.Controls.Add(Me._lblUnder_6)
        Me.Frame2.Controls.Add(Me._lblUnder_0)
        Me.Frame2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 75)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(1006, 75)
        Me.Frame2.TabIndex = 91
        Me.Frame2.TabStop = False
        '
        'txtPartNo
        '
        Me.txtPartNo.AcceptsReturn = True
        Me.txtPartNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPartNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPartNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPartNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPartNo.ForeColor = System.Drawing.Color.Blue
        Me.txtPartNo.Location = New System.Drawing.Point(875, 43)
        Me.txtPartNo.MaxLength = 0
        Me.txtPartNo.Name = "txtPartNo"
        Me.txtPartNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPartNo.Size = New System.Drawing.Size(105, 22)
        Me.txtPartNo.TabIndex = 15
        '
        'txtUOMFactor
        '
        Me.txtUOMFactor.AcceptsReturn = True
        Me.txtUOMFactor.BackColor = System.Drawing.SystemColors.Window
        Me.txtUOMFactor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUOMFactor.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUOMFactor.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUOMFactor.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtUOMFactor.Location = New System.Drawing.Point(875, 12)
        Me.txtUOMFactor.MaxLength = 7
        Me.txtUOMFactor.Name = "txtUOMFactor"
        Me.txtUOMFactor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUOMFactor.Size = New System.Drawing.Size(105, 22)
        Me.txtUOMFactor.TabIndex = 10
        '
        'txtPurchaseUom
        '
        Me.txtPurchaseUom.AcceptsReturn = True
        Me.txtPurchaseUom.BackColor = System.Drawing.SystemColors.Window
        Me.txtPurchaseUom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPurchaseUom.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPurchaseUom.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPurchaseUom.ForeColor = System.Drawing.Color.Blue
        Me.txtPurchaseUom.Location = New System.Drawing.Point(450, 12)
        Me.txtPurchaseUom.MaxLength = 0
        Me.txtPurchaseUom.Name = "txtPurchaseUom"
        Me.txtPurchaseUom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPurchaseUom.Size = New System.Drawing.Size(37, 22)
        Me.txtPurchaseUom.TabIndex = 8
        '
        'txtItemUom
        '
        Me.txtItemUom.AcceptsReturn = True
        Me.txtItemUom.BackColor = System.Drawing.SystemColors.Window
        Me.txtItemUom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemUom.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItemUom.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemUom.ForeColor = System.Drawing.Color.Blue
        Me.txtItemUom.Location = New System.Drawing.Point(118, 12)
        Me.txtItemUom.MaxLength = 0
        Me.txtItemUom.Name = "txtItemUom"
        Me.txtItemUom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemUom.Size = New System.Drawing.Size(37, 22)
        Me.txtItemUom.TabIndex = 6
        '
        'txtSubCatName
        '
        Me.txtSubCatName.AcceptsReturn = True
        Me.txtSubCatName.BackColor = System.Drawing.SystemColors.Window
        Me.txtSubCatName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSubCatName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSubCatName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubCatName.ForeColor = System.Drawing.Color.Blue
        Me.txtSubCatName.Location = New System.Drawing.Point(450, 43)
        Me.txtSubCatName.MaxLength = 0
        Me.txtSubCatName.Name = "txtSubCatName"
        Me.txtSubCatName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSubCatName.Size = New System.Drawing.Size(37, 22)
        Me.txtSubCatName.TabIndex = 13
        '
        'txtCatName
        '
        Me.txtCatName.AcceptsReturn = True
        Me.txtCatName.BackColor = System.Drawing.SystemColors.Window
        Me.txtCatName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCatName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCatName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCatName.ForeColor = System.Drawing.Color.Blue
        Me.txtCatName.Location = New System.Drawing.Point(118, 43)
        Me.txtCatName.MaxLength = 0
        Me.txtCatName.Name = "txtCatName"
        Me.txtCatName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCatName.Size = New System.Drawing.Size(37, 22)
        Me.txtCatName.TabIndex = 11
        '
        'lblPurUom
        '
        Me.lblPurUom.BackColor = System.Drawing.SystemColors.Control
        Me.lblPurUom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPurUom.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPurUom.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPurUom.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPurUom.Location = New System.Drawing.Point(520, 12)
        Me.lblPurUom.Name = "lblPurUom"
        Me.lblPurUom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPurUom.Size = New System.Drawing.Size(262, 19)
        Me.lblPurUom.TabIndex = 78
        Me.lblPurUom.Text = "lblPurUom"
        '
        'lblItemUom
        '
        Me.lblItemUom.BackColor = System.Drawing.SystemColors.Control
        Me.lblItemUom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblItemUom.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblItemUom.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItemUom.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblItemUom.Location = New System.Drawing.Point(180, 14)
        Me.lblItemUom.Name = "lblItemUom"
        Me.lblItemUom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblItemUom.Size = New System.Drawing.Size(176, 19)
        Me.lblItemUom.TabIndex = 77
        Me.lblItemUom.Text = "lblItemUom"
        '
        'lblSubCatName
        '
        Me.lblSubCatName.BackColor = System.Drawing.SystemColors.Control
        Me.lblSubCatName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSubCatName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSubCatName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubCatName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSubCatName.Location = New System.Drawing.Point(520, 48)
        Me.lblSubCatName.Name = "lblSubCatName"
        Me.lblSubCatName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSubCatName.Size = New System.Drawing.Size(262, 19)
        Me.lblSubCatName.TabIndex = 80
        Me.lblSubCatName.Text = "lblSubCatName"
        '
        'lblCatName
        '
        Me.lblCatName.BackColor = System.Drawing.SystemColors.Control
        Me.lblCatName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCatName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCatName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCatName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCatName.Location = New System.Drawing.Point(180, 48)
        Me.lblCatName.Name = "lblCatName"
        Me.lblCatName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCatName.Size = New System.Drawing.Size(176, 17)
        Me.lblCatName.TabIndex = 79
        Me.lblCatName.Text = "lblCatName"
        '
        '_lblUnder_27
        '
        Me._lblUnder_27.AutoSize = True
        Me._lblUnder_27.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_27.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_27.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_27.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_27, CType(27, Short))
        Me._lblUnder_27.Location = New System.Drawing.Point(815, 48)
        Me._lblUnder_27.Name = "_lblUnder_27"
        Me._lblUnder_27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_27.Size = New System.Drawing.Size(54, 13)
        Me._lblUnder_27.TabIndex = 97
        Me._lblUnder_27.Text = "Part No. :"
        Me._lblUnder_27.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_2
        '
        Me._lblUnder_2.AutoSize = True
        Me._lblUnder_2.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_2, CType(2, Short))
        Me._lblUnder_2.Location = New System.Drawing.Point(4, 48)
        Me._lblUnder_2.Name = "_lblUnder_2"
        Me._lblUnder_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_2.Size = New System.Drawing.Size(93, 13)
        Me._lblUnder_2.TabIndex = 96
        Me._lblUnder_2.Text = "Category Name :"
        Me._lblUnder_2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_4
        '
        Me._lblUnder_4.AutoSize = True
        Me._lblUnder_4.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_4, CType(4, Short))
        Me._lblUnder_4.Location = New System.Drawing.Point(3, 14)
        Me._lblUnder_4.Name = "_lblUnder_4"
        Me._lblUnder_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_4.Size = New System.Drawing.Size(65, 13)
        Me._lblUnder_4.TabIndex = 95
        Me._lblUnder_4.Text = "Item UOM :"
        Me._lblUnder_4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_5
        '
        Me._lblUnder_5.AutoSize = True
        Me._lblUnder_5.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_5, CType(5, Short))
        Me._lblUnder_5.Location = New System.Drawing.Point(360, 16)
        Me._lblUnder_5.Name = "_lblUnder_5"
        Me._lblUnder_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_5.Size = New System.Drawing.Size(86, 13)
        Me._lblUnder_5.TabIndex = 94
        Me._lblUnder_5.Text = "Purchase UOM :"
        Me._lblUnder_5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_6
        '
        Me._lblUnder_6.AutoSize = True
        Me._lblUnder_6.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_6, CType(6, Short))
        Me._lblUnder_6.Location = New System.Drawing.Point(76, -160)
        Me._lblUnder_6.Name = "_lblUnder_6"
        Me._lblUnder_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_6.Size = New System.Drawing.Size(71, 13)
        Me._lblUnder_6.TabIndex = 93
        Me._lblUnder_6.Text = "UOM Factor:"
        Me._lblUnder_6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_0
        '
        Me._lblUnder_0.AutoSize = True
        Me._lblUnder_0.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_0, CType(0, Short))
        Me._lblUnder_0.Location = New System.Drawing.Point(364, 48)
        Me._lblUnder_0.Name = "_lblUnder_0"
        Me._lblUnder_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_0.Size = New System.Drawing.Size(82, 13)
        Me._lblUnder_0.TabIndex = 92
        Me._lblUnder_0.Text = "Sub Category :"
        Me._lblUnder_0.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.txtItemClassQnty)
        Me.Frame3.Controls.Add(Me.txtMaxQnty)
        Me.Frame3.Controls.Add(Me.txtEcoQnty)
        Me.Frame3.Controls.Add(Me.txtMinQnty)
        Me.Frame3.Controls.Add(Me.txtPurchaseCost)
        Me.Frame3.Controls.Add(Me.txtLeadTime)
        Me.Frame3.Controls.Add(Me.txtReQnty)
        Me.Frame3.Controls.Add(Me.txtSaleCost)
        Me.Frame3.Controls.Add(Me._lblUnder_22)
        Me.Frame3.Controls.Add(Me._lblUnder_7)
        Me.Frame3.Controls.Add(Me._lblUnder_8)
        Me.Frame3.Controls.Add(Me._lblUnder_9)
        Me.Frame3.Controls.Add(Me._lblUnder_11)
        Me.Frame3.Controls.Add(Me._lblUnder_14)
        Me.Frame3.Controls.Add(Me._lblUnder_15)
        Me.Frame3.Controls.Add(Me._lblUnder_21)
        Me.Frame3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 148)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(1006, 69)
        Me.Frame3.TabIndex = 98
        Me.Frame3.TabStop = False
        '
        'txtItemClassQnty
        '
        Me.txtItemClassQnty.AcceptsReturn = True
        Me.txtItemClassQnty.BackColor = System.Drawing.SystemColors.Window
        Me.txtItemClassQnty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemClassQnty.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItemClassQnty.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemClassQnty.ForeColor = System.Drawing.Color.Blue
        Me.txtItemClassQnty.Location = New System.Drawing.Point(292, 12)
        Me.txtItemClassQnty.MaxLength = 0
        Me.txtItemClassQnty.Name = "txtItemClassQnty"
        Me.txtItemClassQnty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemClassQnty.Size = New System.Drawing.Size(85, 22)
        Me.txtItemClassQnty.TabIndex = 17
        '
        'txtMaxQnty
        '
        Me.txtMaxQnty.AcceptsReturn = True
        Me.txtMaxQnty.BackColor = System.Drawing.SystemColors.Window
        Me.txtMaxQnty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMaxQnty.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMaxQnty.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMaxQnty.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtMaxQnty.Location = New System.Drawing.Point(474, 40)
        Me.txtMaxQnty.MaxLength = 7
        Me.txtMaxQnty.Name = "txtMaxQnty"
        Me.txtMaxQnty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMaxQnty.Size = New System.Drawing.Size(85, 22)
        Me.txtMaxQnty.TabIndex = 22
        '
        'txtEcoQnty
        '
        Me.txtEcoQnty.AcceptsReturn = True
        Me.txtEcoQnty.BackColor = System.Drawing.SystemColors.Window
        Me.txtEcoQnty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEcoQnty.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEcoQnty.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEcoQnty.ForeColor = System.Drawing.Color.Blue
        Me.txtEcoQnty.Location = New System.Drawing.Point(660, 12)
        Me.txtEcoQnty.MaxLength = 0
        Me.txtEcoQnty.Name = "txtEcoQnty"
        Me.txtEcoQnty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEcoQnty.Size = New System.Drawing.Size(85, 22)
        Me.txtEcoQnty.TabIndex = 19
        '
        'txtMinQnty
        '
        Me.txtMinQnty.AcceptsReturn = True
        Me.txtMinQnty.BackColor = System.Drawing.SystemColors.Window
        Me.txtMinQnty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMinQnty.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMinQnty.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMinQnty.ForeColor = System.Drawing.Color.Blue
        Me.txtMinQnty.Location = New System.Drawing.Point(474, 12)
        Me.txtMinQnty.MaxLength = 0
        Me.txtMinQnty.Name = "txtMinQnty"
        Me.txtMinQnty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMinQnty.Size = New System.Drawing.Size(85, 22)
        Me.txtMinQnty.TabIndex = 18
        '
        'txtPurchaseCost
        '
        Me.txtPurchaseCost.AcceptsReturn = True
        Me.txtPurchaseCost.BackColor = System.Drawing.SystemColors.Window
        Me.txtPurchaseCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPurchaseCost.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPurchaseCost.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPurchaseCost.ForeColor = System.Drawing.Color.Blue
        Me.txtPurchaseCost.Location = New System.Drawing.Point(102, 40)
        Me.txtPurchaseCost.MaxLength = 0
        Me.txtPurchaseCost.Name = "txtPurchaseCost"
        Me.txtPurchaseCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPurchaseCost.Size = New System.Drawing.Size(85, 22)
        Me.txtPurchaseCost.TabIndex = 20
        '
        'txtLeadTime
        '
        Me.txtLeadTime.AcceptsReturn = True
        Me.txtLeadTime.BackColor = System.Drawing.SystemColors.Window
        Me.txtLeadTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLeadTime.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLeadTime.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLeadTime.ForeColor = System.Drawing.Color.Blue
        Me.txtLeadTime.Location = New System.Drawing.Point(102, 12)
        Me.txtLeadTime.MaxLength = 0
        Me.txtLeadTime.Name = "txtLeadTime"
        Me.txtLeadTime.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLeadTime.Size = New System.Drawing.Size(85, 22)
        Me.txtLeadTime.TabIndex = 16
        '
        'txtReQnty
        '
        Me.txtReQnty.AcceptsReturn = True
        Me.txtReQnty.BackColor = System.Drawing.SystemColors.Window
        Me.txtReQnty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReQnty.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtReQnty.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReQnty.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtReQnty.Location = New System.Drawing.Point(660, 40)
        Me.txtReQnty.MaxLength = 7
        Me.txtReQnty.Name = "txtReQnty"
        Me.txtReQnty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtReQnty.Size = New System.Drawing.Size(85, 22)
        Me.txtReQnty.TabIndex = 23
        '
        'txtSaleCost
        '
        Me.txtSaleCost.AcceptsReturn = True
        Me.txtSaleCost.BackColor = System.Drawing.SystemColors.Window
        Me.txtSaleCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSaleCost.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSaleCost.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSaleCost.ForeColor = System.Drawing.Color.Blue
        Me.txtSaleCost.Location = New System.Drawing.Point(292, 40)
        Me.txtSaleCost.MaxLength = 0
        Me.txtSaleCost.Name = "txtSaleCost"
        Me.txtSaleCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSaleCost.Size = New System.Drawing.Size(85, 22)
        Me.txtSaleCost.TabIndex = 21
        '
        '_lblUnder_22
        '
        Me._lblUnder_22.AutoSize = True
        Me._lblUnder_22.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_22.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_22.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_22, CType(22, Short))
        Me._lblUnder_22.Location = New System.Drawing.Point(199, 14)
        Me._lblUnder_22.Name = "_lblUnder_22"
        Me._lblUnder_22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_22.Size = New System.Drawing.Size(86, 13)
        Me._lblUnder_22.TabIndex = 106
        Me._lblUnder_22.Text = "Item Class Qty :"
        Me._lblUnder_22.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_7
        '
        Me._lblUnder_7.AutoSize = True
        Me._lblUnder_7.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_7, CType(7, Short))
        Me._lblUnder_7.Location = New System.Drawing.Point(30, 14)
        Me._lblUnder_7.Name = "_lblUnder_7"
        Me._lblUnder_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_7.Size = New System.Drawing.Size(65, 13)
        Me._lblUnder_7.TabIndex = 105
        Me._lblUnder_7.Text = "Lead Time :"
        Me._lblUnder_7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_8
        '
        Me._lblUnder_8.AutoSize = True
        Me._lblUnder_8.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_8.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_8, CType(8, Short))
        Me._lblUnder_8.Location = New System.Drawing.Point(7, 42)
        Me._lblUnder_8.Name = "_lblUnder_8"
        Me._lblUnder_8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_8.Size = New System.Drawing.Size(83, 13)
        Me._lblUnder_8.TabIndex = 104
        Me._lblUnder_8.Text = "Purchase Cost :"
        Me._lblUnder_8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_9
        '
        Me._lblUnder_9.AutoSize = True
        Me._lblUnder_9.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_9.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_9, CType(9, Short))
        Me._lblUnder_9.Location = New System.Drawing.Point(392, 14)
        Me._lblUnder_9.Name = "_lblUnder_9"
        Me._lblUnder_9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_9.Size = New System.Drawing.Size(82, 13)
        Me._lblUnder_9.TabIndex = 103
        Me._lblUnder_9.Text = "Minimum Qty :"
        Me._lblUnder_9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_11
        '
        Me._lblUnder_11.AutoSize = True
        Me._lblUnder_11.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_11.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_11.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_11, CType(11, Short))
        Me._lblUnder_11.Location = New System.Drawing.Point(563, 14)
        Me._lblUnder_11.Name = "_lblUnder_11"
        Me._lblUnder_11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_11.Size = New System.Drawing.Size(89, 13)
        Me._lblUnder_11.TabIndex = 102
        Me._lblUnder_11.Text = "Inventory Days :"
        Me._lblUnder_11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_14
        '
        Me._lblUnder_14.AutoSize = True
        Me._lblUnder_14.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_14.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_14.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_14, CType(14, Short))
        Me._lblUnder_14.Location = New System.Drawing.Point(389, 42)
        Me._lblUnder_14.Name = "_lblUnder_14"
        Me._lblUnder_14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_14.Size = New System.Drawing.Size(85, 13)
        Me._lblUnder_14.TabIndex = 101
        Me._lblUnder_14.Text = "Maximum Qty :"
        Me._lblUnder_14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_15
        '
        Me._lblUnder_15.AutoSize = True
        Me._lblUnder_15.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_15.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_15.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_15, CType(15, Short))
        Me._lblUnder_15.Location = New System.Drawing.Point(580, 42)
        Me._lblUnder_15.Name = "_lblUnder_15"
        Me._lblUnder_15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_15.Size = New System.Drawing.Size(75, 13)
        Me._lblUnder_15.TabIndex = 100
        Me._lblUnder_15.Text = "Reorder Qty :"
        Me._lblUnder_15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_21
        '
        Me._lblUnder_21.AutoSize = True
        Me._lblUnder_21.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_21.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_21.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_21, CType(21, Short))
        Me._lblUnder_21.Location = New System.Drawing.Point(226, 42)
        Me._lblUnder_21.Name = "_lblUnder_21"
        Me._lblUnder_21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_21.Size = New System.Drawing.Size(60, 13)
        Me._lblUnder_21.TabIndex = 99
        Me._lblUnder_21.Text = "Sale Cost :"
        Me._lblUnder_21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.CboItemType)
        Me.Frame4.Controls.Add(Me.CboExciseFlag)
        Me.Frame4.Controls.Add(Me.cboItemClassification)
        Me.Frame4.Controls.Add(Me.CboItemClass)
        Me.Frame4.Controls.Add(Me._lblUnder_16)
        Me.Frame4.Controls.Add(Me._lblUnder_17)
        Me.Frame4.Controls.Add(Me._lblUnder_18)
        Me.Frame4.Controls.Add(Me._lblUnder_19)
        Me.Frame4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, 212)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(1004, 42)
        Me.Frame4.TabIndex = 107
        Me.Frame4.TabStop = False
        '
        'CboExciseFlag
        '
        Me.CboExciseFlag.BackColor = System.Drawing.SystemColors.Window
        Me.CboExciseFlag.Cursor = System.Windows.Forms.Cursors.Default
        Me.CboExciseFlag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboExciseFlag.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboExciseFlag.ForeColor = System.Drawing.SystemColors.WindowText
        Me.CboExciseFlag.Location = New System.Drawing.Point(474, 13)
        Me.CboExciseFlag.Name = "CboExciseFlag"
        Me.CboExciseFlag.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CboExciseFlag.Size = New System.Drawing.Size(85, 21)
        Me.CboExciseFlag.TabIndex = 26
        '
        'cboItemClassification
        '
        Me.cboItemClassification.BackColor = System.Drawing.SystemColors.Window
        Me.cboItemClassification.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboItemClassification.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboItemClassification.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboItemClassification.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboItemClassification.Location = New System.Drawing.Point(660, 13)
        Me.cboItemClassification.Name = "cboItemClassification"
        Me.cboItemClassification.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboItemClassification.Size = New System.Drawing.Size(85, 21)
        Me.cboItemClassification.TabIndex = 27
        '
        'CboItemClass
        '
        Me.CboItemClass.BackColor = System.Drawing.SystemColors.Window
        Me.CboItemClass.Cursor = System.Windows.Forms.Cursors.Default
        Me.CboItemClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboItemClass.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboItemClass.ForeColor = System.Drawing.SystemColors.WindowText
        Me.CboItemClass.Location = New System.Drawing.Point(292, 13)
        Me.CboItemClass.Name = "CboItemClass"
        Me.CboItemClass.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CboItemClass.Size = New System.Drawing.Size(85, 21)
        Me.CboItemClass.TabIndex = 25
        '
        '_lblUnder_16
        '
        Me._lblUnder_16.AutoSize = True
        Me._lblUnder_16.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_16.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_16.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_16, CType(16, Short))
        Me._lblUnder_16.Location = New System.Drawing.Point(31, 17)
        Me._lblUnder_16.Name = "_lblUnder_16"
        Me._lblUnder_16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_16.Size = New System.Drawing.Size(63, 13)
        Me._lblUnder_16.TabIndex = 111
        Me._lblUnder_16.Text = "Item Type :"
        Me._lblUnder_16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_17
        '
        Me._lblUnder_17.AutoSize = True
        Me._lblUnder_17.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_17.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_17.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_17, CType(17, Short))
        Me._lblUnder_17.Location = New System.Drawing.Point(221, 17)
        Me._lblUnder_17.Name = "_lblUnder_17"
        Me._lblUnder_17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_17.Size = New System.Drawing.Size(65, 13)
        Me._lblUnder_17.TabIndex = 110
        Me._lblUnder_17.Text = "Item Class :"
        Me._lblUnder_17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_18
        '
        Me._lblUnder_18.AutoSize = True
        Me._lblUnder_18.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_18.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_18.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_18, CType(18, Short))
        Me._lblUnder_18.Location = New System.Drawing.Point(391, 17)
        Me._lblUnder_18.Name = "_lblUnder_18"
        Me._lblUnder_18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_18.Size = New System.Drawing.Size(75, 13)
        Me._lblUnder_18.TabIndex = 109
        Me._lblUnder_18.Text = "Excise Flage :"
        Me._lblUnder_18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_19
        '
        Me._lblUnder_19.AutoSize = True
        Me._lblUnder_19.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_19.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_19.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_19, CType(19, Short))
        Me._lblUnder_19.Location = New System.Drawing.Point(572, 15)
        Me._lblUnder_19.Name = "_lblUnder_19"
        Me._lblUnder_19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_19.Size = New System.Drawing.Size(80, 13)
        Me._lblUnder_19.TabIndex = 108
        Me._lblUnder_19.Text = "Classification :"
        Me._lblUnder_19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ADataGrid
        '
        Me.ADataGrid.BackColor = System.Drawing.SystemColors.Window
        Me.ADataGrid.CommandTimeout = 0
        Me.ADataGrid.CommandType = ADODB.CommandTypeEnum.adCmdUnknown
        Me.ADataGrid.ConnectionString = Nothing
        Me.ADataGrid.CursorType = ADODB.CursorTypeEnum.adOpenStatic
        Me.ADataGrid.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ADataGrid.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ADataGrid.Location = New System.Drawing.Point(0, 56)
        Me.ADataGrid.LockType = ADODB.LockTypeEnum.adLockOptimistic
        Me.ADataGrid.Name = "ADataGrid"
        Me.ADataGrid.Size = New System.Drawing.Size(113, 23)
        Me.ADataGrid.TabIndex = 85
        Me.ADataGrid.Text = "Adodc1"
        Me.ADataGrid.Visible = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(0, 0)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 86
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.CmdAdd)
        Me.FraMovement.Controls.Add(Me.CmdModify)
        Me.FraMovement.Controls.Add(Me.CmdSave)
        Me.FraMovement.Controls.Add(Me.CmdDelete)
        Me.FraMovement.Controls.Add(Me.CmdView)
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.cmdSavePrint)
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.lblType)
        Me.FraMovement.Controls.Add(Me.lblMasterType)
        Me.FraMovement.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(1, 560)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(1003, 51)
        Me.FraMovement.TabIndex = 88
        Me.FraMovement.TabStop = False
        '
        'lblType
        '
        Me.lblType.BackColor = System.Drawing.SystemColors.Control
        Me.lblType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblType.Location = New System.Drawing.Point(8, 32)
        Me.lblType.Name = "lblType"
        Me.lblType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblType.Size = New System.Drawing.Size(43, 15)
        Me.lblType.TabIndex = 149
        Me.lblType.Text = "lblType"
        Me.lblType.Visible = False
        '
        'lblMasterType
        '
        Me.lblMasterType.BackColor = System.Drawing.SystemColors.Control
        Me.lblMasterType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMasterType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMasterType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMasterType.Location = New System.Drawing.Point(4, 18)
        Me.lblMasterType.Name = "lblMasterType"
        Me.lblMasterType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMasterType.Size = New System.Drawing.Size(67, 15)
        Me.lblMasterType.TabIndex = 89
        Me.lblMasterType.Text = "lblMasterType"
        Me.lblMasterType.Visible = False
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(1006, 560)
        Me.SprdView.TabIndex = 90
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(795, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(74, 13)
        Me.Label1.TabIndex = 98
        Me.Label1.Text = "UOM Factor :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'frmItemReqMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1006, 613)
        Me.Controls.Add(Me.FraTrn)
        Me.Controls.Add(Me.ADataGrid)
        Me.Controls.Add(Me.Report1)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.SprdView)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(-242, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmItemReqMaster"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Item Requisition Master"
        Me.FraTrn.ResumeLayout(False)
        Me.SSTInfo.ResumeLayout(False)
        Me._SSTInfo_TabPage0.ResumeLayout(False)
        Me.FraView.ResumeLayout(False)
        Me.FraView.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me._SSTInfo_TabPage1.ResumeLayout(False)
        Me._SSTInfo_TabPage1.PerformLayout()
        Me.fraTop1.ResumeLayout(False)
        Me.fraTop1.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblUnder, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        'SprdView.DataSource = CType(ADataGrid, msdatasrc.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        SprdView.DataSource = Nothing
    End Sub

    Public WithEvents Label1 As Label
#End Region
End Class