Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmItemMaster
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
        'Me.MDIParent = Admin.Master
        'Admin.Master.Show
        VB6_AddADODataBinding()
    End Sub
    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
        If Disposing Then
            'VB6_RemoveADODataBinding()
            If Not components Is Nothing Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(Disposing)
    End Sub
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Public ToolTip1 As System.Windows.Forms.ToolTip
    Public WithEvents cboGSTClass As System.Windows.Forms.ComboBox
    Public WithEvents txtHSNCode As System.Windows.Forms.TextBox
    Public WithEvents txtProdType As System.Windows.Forms.TextBox
    Public WithEvents txtColor As System.Windows.Forms.TextBox
    Public WithEvents txtItemMake As System.Windows.Forms.TextBox
    Public WithEvents txtModel As System.Windows.Forms.TextBox
    Public WithEvents txtScrapItemCode As System.Windows.Forms.TextBox
    Public WithEvents CboStatus As System.Windows.Forms.ComboBox
    Public WithEvents txtPackingItemCode As System.Windows.Forms.TextBox
    Public WithEvents chkPOReqd As System.Windows.Forms.CheckBox
    Public WithEvents chkStockItem As System.Windows.Forms.CheckBox
    Public WithEvents chkRequired As System.Windows.Forms.CheckBox
    Public WithEvents chkAutoIssue As System.Windows.Forms.CheckBox
    Public WithEvents chkDrawing As System.Windows.Forms.CheckBox
    Public WithEvents chkConsumable As System.Windows.Forms.CheckBox
    Public WithEvents FraChecks As System.Windows.Forms.GroupBox
    Public WithEvents chkMRRLocking As System.Windows.Forms.CheckBox
    Public WithEvents chkMRRLockingOM As System.Windows.Forms.CheckBox
    Public WithEvents chkScheduleLocking As System.Windows.Forms.CheckBox
    Public WithEvents FraLock As System.Windows.Forms.GroupBox
    Public WithEvents _lblUnder_59 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_58 As System.Windows.Forms.Label
    Public WithEvents lblHSNName As System.Windows.Forms.Label
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
    Public WithEvents _lblUnder_10 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_20 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_32 As System.Windows.Forms.Label
    Public WithEvents FraView As System.Windows.Forms.GroupBox
    Public WithEvents _SSTInfo_TabPage0 As System.Windows.Forms.TabPage
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents _lblUnder_26 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_28 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_29 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_40 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_41 As System.Windows.Forms.Label
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
    Public WithEvents _lblUnder_46 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_63 As System.Windows.Forms.Label
    Public WithEvents txtWeight As System.Windows.Forms.TextBox
    Public WithEvents txtDimention As System.Windows.Forms.TextBox
    Public WithEvents txtSpecification As System.Windows.Forms.TextBox
    Public WithEvents txtPackingStandard As System.Windows.Forms.TextBox
    Public WithEvents txtScrapWeight As System.Windows.Forms.TextBox
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
    Public WithEvents txtParentItemName As System.Windows.Forms.TextBox
    Public WithEvents chkChildItem As System.Windows.Forms.CheckBox
    Public WithEvents txtWtPerStrip As System.Windows.Forms.TextBox
    Public WithEvents _SSTInfo_TabPage1 As System.Windows.Forms.TabPage
    Public WithEvents txtTacks As System.Windows.Forms.TextBox
    Public WithEvents txtTIGLen As System.Windows.Forms.TextBox
    Public WithEvents txtBrazingLen As System.Windows.Forms.TextBox
    Public WithEvents txtSeamLen As System.Windows.Forms.TextBox
    Public WithEvents txtSpotNos As System.Windows.Forms.TextBox
    Public WithEvents _lblUnder_1 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_54 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_55 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_56 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_57 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents chkSB As System.Windows.Forms.CheckBox
    Public WithEvents chkGrinding As System.Windows.Forms.CheckBox
    Public WithEvents txtSSWLength As System.Windows.Forms.TextBox
    Public WithEvents txtWLength As System.Windows.Forms.TextBox
    Public WithEvents _lblUnder_49 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_12 As System.Windows.Forms.Label
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents txtSSWLengthCust As System.Windows.Forms.TextBox
    Public WithEvents txtWLengthCust As System.Windows.Forms.TextBox
    Public WithEvents _lblUnder_62 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_60 As System.Windows.Forms.Label
    Public WithEvents Frame5 As System.Windows.Forms.GroupBox
    Public WithEvents cboWeldingLine As System.Windows.Forms.ComboBox
    Public WithEvents _lblUnder_48 As System.Windows.Forms.Label
    Public WithEvents FraWeld As System.Windows.Forms.GroupBox
    Public WithEvents txtAddSurfaceAreaPLT As System.Windows.Forms.TextBox
    Public WithEvents txtAddSurfaceAreaNPC As System.Windows.Forms.TextBox
    Public WithEvents txtAddSurfaceAreaPPS As System.Windows.Forms.TextBox
    Public WithEvents txtSurfaceArea As System.Windows.Forms.TextBox
    Public WithEvents cboSurfaceTreatment As System.Windows.Forms.ComboBox
    Public WithEvents txtSurfaceAreaInner As System.Windows.Forms.TextBox
    Public WithEvents _lblUnder_53 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_52 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_51 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_3 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_33 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_50 As System.Windows.Forms.Label
    Public WithEvents FraSurface As System.Windows.Forms.GroupBox
    Public WithEvents cboPressLine As System.Windows.Forms.ComboBox
    Public WithEvents _lblUnder_61 As System.Windows.Forms.Label
    Public WithEvents FraPress As System.Windows.Forms.GroupBox
    Public WithEvents _SSTInfo_TabPage2 As System.Windows.Forms.TabPage
    Public WithEvents SSTInfo As System.Windows.Forms.TabControl
    Public WithEvents txtGUID As System.Windows.Forms.TextBox
    Public WithEvents txtTechnicalDescription As System.Windows.Forms.TextBox
    Public WithEvents chkExportItem As System.Windows.Forms.CheckBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_39 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents fraTop1 As System.Windows.Forms.GroupBox
    Public WithEvents txtPartNo As System.Windows.Forms.TextBox
    Public WithEvents txtUOMFactor As System.Windows.Forms.TextBox
    Public WithEvents txtPurchaseUom As System.Windows.Forms.TextBox
    Public WithEvents txtItemUom As System.Windows.Forms.TextBox
    Public WithEvents txtSubCatName As System.Windows.Forms.TextBox
    Public WithEvents txtCatName As System.Windows.Forms.TextBox
    Public WithEvents lblPurUom As System.Windows.Forms.Label
    Public WithEvents lblItemUom As System.Windows.Forms.Label
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
    Public WithEvents lblMasterType As System.Windows.Forms.Label
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents lblUnder As VB6.LabelArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmItemMaster))
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
        Dim Appearance13 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance14 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance15 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance16 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance17 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance18 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance19 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance20 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance21 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance22 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance23 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance24 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance25 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance26 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance27 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance28 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance29 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance30 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance31 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance32 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance33 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance34 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance35 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance36 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cboWeldingLine = New System.Windows.Forms.ComboBox()
        Me.cboSurfaceTreatment = New System.Windows.Forms.ComboBox()
        Me.cboPressLine = New System.Windows.Forms.ComboBox()
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
        Me.cmdSearchCategory = New System.Windows.Forms.Button()
        Me.cmdSearchSubCat = New System.Windows.Forms.Button()
        Me.cmdSearchPIC = New System.Windows.Forms.Button()
        Me.cmdSearchScrap = New System.Windows.Forms.Button()
        Me.cmdSearchHSN = New System.Windows.Forms.Button()
        Me.cmdSearchPackType = New System.Windows.Forms.Button()
        Me.cmdSearchUom = New System.Windows.Forms.Button()
        Me.cmdSearchPurUom = New System.Windows.Forms.Button()
        Me.cmdSearchJWUom = New System.Windows.Forms.Button()
        Me.FraTrn = New System.Windows.Forms.GroupBox()
        Me.SSTInfo = New System.Windows.Forms.TabControl()
        Me._SSTInfo_TabPage0 = New System.Windows.Forms.TabPage()
        Me.FraView = New System.Windows.Forms.GroupBox()
        Me.FraLock = New System.Windows.Forms.GroupBox()
        Me.chkMRRLockingOM = New System.Windows.Forms.CheckBox()
        Me.chkMRRLocking = New System.Windows.Forms.CheckBox()
        Me.chkScheduleLocking = New System.Windows.Forms.CheckBox()
        Me.FraChecks = New System.Windows.Forms.GroupBox()
        Me.chkHeatReq = New System.Windows.Forms.CheckBox()
        Me.chkAutoQC = New System.Windows.Forms.CheckBox()
        Me.chkStockItem = New System.Windows.Forms.CheckBox()
        Me.chkAutoIssue = New System.Windows.Forms.CheckBox()
        Me.chkDrawing = New System.Windows.Forms.CheckBox()
        Me.chkConsumable = New System.Windows.Forms.CheckBox()
        Me.chkPOReqd = New System.Windows.Forms.CheckBox()
        Me.chkRequired = New System.Windows.Forms.CheckBox()
        Me.txtPackType = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cboGSTClass = New System.Windows.Forms.ComboBox()
        Me.txtHSNCode = New System.Windows.Forms.TextBox()
        Me.txtProdType = New System.Windows.Forms.TextBox()
        Me.txtColor = New System.Windows.Forms.TextBox()
        Me.txtItemMake = New System.Windows.Forms.TextBox()
        Me.txtModel = New System.Windows.Forms.TextBox()
        Me.txtScrapItemCode = New System.Windows.Forms.TextBox()
        Me.CboStatus = New System.Windows.Forms.ComboBox()
        Me.txtPackingItemCode = New System.Windows.Forms.TextBox()
        Me._lblUnder_59 = New System.Windows.Forms.Label()
        Me._lblUnder_58 = New System.Windows.Forms.Label()
        Me.lblHSNName = New System.Windows.Forms.Label()
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
        Me._lblUnder_10 = New System.Windows.Forms.Label()
        Me._lblUnder_20 = New System.Windows.Forms.Label()
        Me._lblUnder_32 = New System.Windows.Forms.Label()
        Me._SSTInfo_TabPage1 = New System.Windows.Forms.TabPage()
        Me._lblUnder_26 = New System.Windows.Forms.Label()
        Me._lblUnder_28 = New System.Windows.Forms.Label()
        Me._lblUnder_29 = New System.Windows.Forms.Label()
        Me._lblUnder_40 = New System.Windows.Forms.Label()
        Me._lblUnder_41 = New System.Windows.Forms.Label()
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
        Me._lblUnder_46 = New System.Windows.Forms.Label()
        Me._lblUnder_63 = New System.Windows.Forms.Label()
        Me.txtWeight = New System.Windows.Forms.TextBox()
        Me.txtDimention = New System.Windows.Forms.TextBox()
        Me.txtSpecification = New System.Windows.Forms.TextBox()
        Me.txtPackingStandard = New System.Windows.Forms.TextBox()
        Me.txtScrapWeight = New System.Windows.Forms.TextBox()
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
        Me.txtParentItemName = New System.Windows.Forms.TextBox()
        Me.chkChildItem = New System.Windows.Forms.CheckBox()
        Me.txtWtPerStrip = New System.Windows.Forms.TextBox()
        Me._lblUnder_31 = New System.Windows.Forms.Label()
        Me._SSTInfo_TabPage2 = New System.Windows.Forms.TabPage()
        Me.chkSB = New System.Windows.Forms.CheckBox()
        Me.chkGrinding = New System.Windows.Forms.CheckBox()
        Me.FraWeld = New System.Windows.Forms.GroupBox()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.txtSSWLength = New System.Windows.Forms.TextBox()
        Me.txtWLength = New System.Windows.Forms.TextBox()
        Me._lblUnder_49 = New System.Windows.Forms.Label()
        Me._lblUnder_12 = New System.Windows.Forms.Label()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me.txtSSWLengthCust = New System.Windows.Forms.TextBox()
        Me.txtWLengthCust = New System.Windows.Forms.TextBox()
        Me._lblUnder_62 = New System.Windows.Forms.Label()
        Me._lblUnder_60 = New System.Windows.Forms.Label()
        Me._lblUnder_48 = New System.Windows.Forms.Label()
        Me.FraSurface = New System.Windows.Forms.GroupBox()
        Me.txtAddSurfaceAreaPLT = New System.Windows.Forms.TextBox()
        Me.txtAddSurfaceAreaNPC = New System.Windows.Forms.TextBox()
        Me.txtAddSurfaceAreaPPS = New System.Windows.Forms.TextBox()
        Me.txtSurfaceArea = New System.Windows.Forms.TextBox()
        Me.txtSurfaceAreaInner = New System.Windows.Forms.TextBox()
        Me._lblUnder_53 = New System.Windows.Forms.Label()
        Me._lblUnder_52 = New System.Windows.Forms.Label()
        Me._lblUnder_51 = New System.Windows.Forms.Label()
        Me._lblUnder_3 = New System.Windows.Forms.Label()
        Me._lblUnder_33 = New System.Windows.Forms.Label()
        Me._lblUnder_50 = New System.Windows.Forms.Label()
        Me.FraPress = New System.Windows.Forms.GroupBox()
        Me._lblUnder_61 = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me._lblUnder_55 = New System.Windows.Forms.Label()
        Me.txtTacks = New System.Windows.Forms.TextBox()
        Me.txtTIGLen = New System.Windows.Forms.TextBox()
        Me.txtBrazingLen = New System.Windows.Forms.TextBox()
        Me.txtSeamLen = New System.Windows.Forms.TextBox()
        Me.txtSpotNos = New System.Windows.Forms.TextBox()
        Me._lblUnder_1 = New System.Windows.Forms.Label()
        Me._lblUnder_54 = New System.Windows.Forms.Label()
        Me._lblUnder_56 = New System.Windows.Forms.Label()
        Me._lblUnder_57 = New System.Windows.Forms.Label()
        Me._SSTInfo_TabPage3 = New System.Windows.Forms.TabPage()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.txtPartNo = New System.Windows.Forms.TextBox()
        Me.txtUOMFactor = New System.Windows.Forms.TextBox()
        Me.txtPurchaseUom = New System.Windows.Forms.TextBox()
        Me.txtItemUom = New System.Windows.Forms.TextBox()
        Me.txtSubCatName = New System.Windows.Forms.TextBox()
        Me.txtCatName = New System.Windows.Forms.TextBox()
        Me.lblPurUom = New System.Windows.Forms.Label()
        Me.lblItemUom = New System.Windows.Forms.Label()
        Me._lblUnder_27 = New System.Windows.Forms.Label()
        Me._lblUnder_2 = New System.Windows.Forms.Label()
        Me._lblUnder_4 = New System.Windows.Forms.Label()
        Me._lblUnder_5 = New System.Windows.Forms.Label()
        Me._lblUnder_6 = New System.Windows.Forms.Label()
        Me._lblUnder_0 = New System.Windows.Forms.Label()
        Me.fraTop1 = New System.Windows.Forms.GroupBox()
        Me.txtItemCode = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.txtItemName = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.txtGUID = New System.Windows.Forms.TextBox()
        Me.txtTechnicalDescription = New System.Windows.Forms.TextBox()
        Me.chkExportItem = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me._lblUnder_39 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.txtPurchaseCost = New System.Windows.Forms.TextBox()
        Me.txtItemClassQnty = New System.Windows.Forms.TextBox()
        Me.txtMaxQnty = New System.Windows.Forms.TextBox()
        Me.txtEcoQnty = New System.Windows.Forms.TextBox()
        Me.txtMinQnty = New System.Windows.Forms.TextBox()
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
        Me.txtOldPartNo = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtJWUOM = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CboExciseFlag = New System.Windows.Forms.ComboBox()
        Me.cboItemClassification = New System.Windows.Forms.ComboBox()
        Me.CboItemClass = New System.Windows.Forms.ComboBox()
        Me._lblUnder_16 = New System.Windows.Forms.Label()
        Me._lblUnder_17 = New System.Windows.Forms.Label()
        Me._lblUnder_18 = New System.Windows.Forms.Label()
        Me._lblUnder_19 = New System.Windows.Forms.Label()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.lblMasterType = New System.Windows.Forms.Label()
        Me.lblUnder = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.UltraGrid1 = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.FraTrn.SuspendLayout()
        Me.SSTInfo.SuspendLayout()
        Me._SSTInfo_TabPage0.SuspendLayout()
        Me.FraView.SuspendLayout()
        Me.FraLock.SuspendLayout()
        Me.FraChecks.SuspendLayout()
        Me._SSTInfo_TabPage1.SuspendLayout()
        Me._SSTInfo_TabPage2.SuspendLayout()
        Me.FraWeld.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me.Frame5.SuspendLayout()
        Me.FraSurface.SuspendLayout()
        Me.FraPress.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me._SSTInfo_TabPage3.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame2.SuspendLayout()
        Me.fraTop1.SuspendLayout()
        CType(Me.txtItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtItemName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        Me.Frame4.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.lblUnder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cboWeldingLine
        '
        Me.cboWeldingLine.BackColor = System.Drawing.SystemColors.Window
        Me.cboWeldingLine.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboWeldingLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboWeldingLine.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboWeldingLine.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboWeldingLine.Location = New System.Drawing.Point(133, 11)
        Me.cboWeldingLine.Name = "cboWeldingLine"
        Me.cboWeldingLine.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboWeldingLine.Size = New System.Drawing.Size(107, 21)
        Me.cboWeldingLine.TabIndex = 73
        Me.ToolTip1.SetToolTip(Me.cboWeldingLine, "IMPORTED OR LOCAL")
        '
        'cboSurfaceTreatment
        '
        Me.cboSurfaceTreatment.BackColor = System.Drawing.SystemColors.Window
        Me.cboSurfaceTreatment.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboSurfaceTreatment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSurfaceTreatment.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboSurfaceTreatment.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboSurfaceTreatment.Location = New System.Drawing.Point(201, 16)
        Me.cboSurfaceTreatment.Name = "cboSurfaceTreatment"
        Me.cboSurfaceTreatment.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboSurfaceTreatment.Size = New System.Drawing.Size(109, 21)
        Me.cboSurfaceTreatment.TabIndex = 83
        Me.ToolTip1.SetToolTip(Me.cboSurfaceTreatment, "IMPORTED OR LOCAL")
        '
        'cboPressLine
        '
        Me.cboPressLine.BackColor = System.Drawing.SystemColors.Window
        Me.cboPressLine.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboPressLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPressLine.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPressLine.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboPressLine.Location = New System.Drawing.Point(172, 16)
        Me.cboPressLine.Name = "cboPressLine"
        Me.cboPressLine.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboPressLine.Size = New System.Drawing.Size(109, 21)
        Me.cboPressLine.TabIndex = 89
        Me.ToolTip1.SetToolTip(Me.cboPressLine, "IMPORTED OR LOCAL")
        '
        'CboItemType
        '
        Me.CboItemType.BackColor = System.Drawing.SystemColors.Window
        Me.CboItemType.Cursor = System.Windows.Forms.Cursors.Default
        Me.CboItemType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboItemType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboItemType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.CboItemType.Location = New System.Drawing.Point(75, 15)
        Me.CboItemType.Name = "CboItemType"
        Me.CboItemType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CboItemType.Size = New System.Drawing.Size(71, 21)
        Me.CboItemType.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.CboItemType, "IMPORTED OR LOCAL")
        '
        'CmdAdd
        '
        Me.CmdAdd.BackColor = System.Drawing.SystemColors.Control
        Me.CmdAdd.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdAdd.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdAdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdAdd.Image = CType(resources.GetObject("CmdAdd.Image"), System.Drawing.Image)
        Me.CmdAdd.Location = New System.Drawing.Point(158, 14)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(73, 37)
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
        Me.CmdModify.Location = New System.Drawing.Point(230, 14)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(73, 37)
        Me.CmdModify.TabIndex = 90
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
        Me.CmdSave.Location = New System.Drawing.Point(302, 14)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(73, 37)
        Me.CmdSave.TabIndex = 91
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
        Me.CmdDelete.Location = New System.Drawing.Point(446, 14)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(73, 37)
        Me.CmdDelete.TabIndex = 93
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
        Me.CmdView.Location = New System.Drawing.Point(662, 14)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(73, 37)
        Me.CmdView.TabIndex = 96
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
        Me.CmdClose.Location = New System.Drawing.Point(734, 14)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(73, 37)
        Me.CmdClose.TabIndex = 97
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
        Me.cmdPrint.Location = New System.Drawing.Point(518, 14)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(73, 37)
        Me.cmdPrint.TabIndex = 94
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
        Me.cmdSavePrint.Location = New System.Drawing.Point(374, 14)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(73, 37)
        Me.cmdSavePrint.TabIndex = 92
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
        Me.CmdPreview.Location = New System.Drawing.Point(590, 14)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(73, 37)
        Me.CmdPreview.TabIndex = 95
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Preview")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'cmdSearchCategory
        '
        Me.cmdSearchCategory.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchCategory.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchCategory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchCategory.Image = CType(resources.GetObject("cmdSearchCategory.Image"), System.Drawing.Image)
        Me.cmdSearchCategory.Location = New System.Drawing.Point(365, 40)
        Me.cmdSearchCategory.Name = "cmdSearchCategory"
        Me.cmdSearchCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchCategory.Size = New System.Drawing.Size(30, 24)
        Me.cmdSearchCategory.TabIndex = 119
        Me.cmdSearchCategory.TabStop = False
        Me.cmdSearchCategory.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchCategory, "Search")
        Me.cmdSearchCategory.UseVisualStyleBackColor = False
        '
        'cmdSearchSubCat
        '
        Me.cmdSearchSubCat.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchSubCat.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchSubCat.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchSubCat.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchSubCat.Image = CType(resources.GetObject("cmdSearchSubCat.Image"), System.Drawing.Image)
        Me.cmdSearchSubCat.Location = New System.Drawing.Point(720, 39)
        Me.cmdSearchSubCat.Name = "cmdSearchSubCat"
        Me.cmdSearchSubCat.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchSubCat.Size = New System.Drawing.Size(30, 23)
        Me.cmdSearchSubCat.TabIndex = 120
        Me.cmdSearchSubCat.TabStop = False
        Me.cmdSearchSubCat.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchSubCat, "Search")
        Me.cmdSearchSubCat.UseVisualStyleBackColor = False
        '
        'cmdSearchPIC
        '
        Me.cmdSearchPIC.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchPIC.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchPIC.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchPIC.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchPIC.Image = CType(resources.GetObject("cmdSearchPIC.Image"), System.Drawing.Image)
        Me.cmdSearchPIC.Location = New System.Drawing.Point(372, 74)
        Me.cmdSearchPIC.Name = "cmdSearchPIC"
        Me.cmdSearchPIC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchPIC.Size = New System.Drawing.Size(30, 24)
        Me.cmdSearchPIC.TabIndex = 5
        Me.cmdSearchPIC.TabStop = False
        Me.cmdSearchPIC.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchPIC, "Search")
        Me.cmdSearchPIC.UseVisualStyleBackColor = False
        '
        'cmdSearchScrap
        '
        Me.cmdSearchScrap.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchScrap.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchScrap.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchScrap.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchScrap.Image = CType(resources.GetObject("cmdSearchScrap.Image"), System.Drawing.Image)
        Me.cmdSearchScrap.Location = New System.Drawing.Point(565, 105)
        Me.cmdSearchScrap.Name = "cmdSearchScrap"
        Me.cmdSearchScrap.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchScrap.Size = New System.Drawing.Size(30, 26)
        Me.cmdSearchScrap.TabIndex = 188
        Me.cmdSearchScrap.TabStop = False
        Me.cmdSearchScrap.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchScrap, "Search")
        Me.cmdSearchScrap.UseVisualStyleBackColor = False
        '
        'cmdSearchHSN
        '
        Me.cmdSearchHSN.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchHSN.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchHSN.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchHSN.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchHSN.Image = CType(resources.GetObject("cmdSearchHSN.Image"), System.Drawing.Image)
        Me.cmdSearchHSN.Location = New System.Drawing.Point(251, 45)
        Me.cmdSearchHSN.Name = "cmdSearchHSN"
        Me.cmdSearchHSN.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchHSN.Size = New System.Drawing.Size(30, 26)
        Me.cmdSearchHSN.TabIndex = 2
        Me.cmdSearchHSN.TabStop = False
        Me.cmdSearchHSN.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchHSN, "Search")
        Me.cmdSearchHSN.UseVisualStyleBackColor = False
        '
        'cmdSearchPackType
        '
        Me.cmdSearchPackType.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchPackType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchPackType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchPackType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchPackType.Image = CType(resources.GetObject("cmdSearchPackType.Image"), System.Drawing.Image)
        Me.cmdSearchPackType.Location = New System.Drawing.Point(569, 74)
        Me.cmdSearchPackType.Name = "cmdSearchPackType"
        Me.cmdSearchPackType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchPackType.Size = New System.Drawing.Size(30, 24)
        Me.cmdSearchPackType.TabIndex = 7
        Me.cmdSearchPackType.TabStop = False
        Me.cmdSearchPackType.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchPackType, "Search")
        Me.cmdSearchPackType.UseVisualStyleBackColor = False
        '
        'cmdSearchUom
        '
        Me.cmdSearchUom.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchUom.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchUom.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchUom.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchUom.Image = CType(resources.GetObject("cmdSearchUom.Image"), System.Drawing.Image)
        Me.cmdSearchUom.Location = New System.Drawing.Point(176, 12)
        Me.cmdSearchUom.Name = "cmdSearchUom"
        Me.cmdSearchUom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchUom.Size = New System.Drawing.Size(30, 24)
        Me.cmdSearchUom.TabIndex = 1
        Me.cmdSearchUom.TabStop = False
        Me.cmdSearchUom.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchUom, "Search")
        Me.cmdSearchUom.UseVisualStyleBackColor = False
        '
        'cmdSearchPurUom
        '
        Me.cmdSearchPurUom.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchPurUom.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchPurUom.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchPurUom.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchPurUom.Image = CType(resources.GetObject("cmdSearchPurUom.Image"), System.Drawing.Image)
        Me.cmdSearchPurUom.Location = New System.Drawing.Point(530, 10)
        Me.cmdSearchPurUom.Name = "cmdSearchPurUom"
        Me.cmdSearchPurUom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchPurUom.Size = New System.Drawing.Size(30, 24)
        Me.cmdSearchPurUom.TabIndex = 4
        Me.cmdSearchPurUom.TabStop = False
        Me.cmdSearchPurUom.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchPurUom, "Search")
        Me.cmdSearchPurUom.UseVisualStyleBackColor = False
        '
        'cmdSearchJWUom
        '
        Me.cmdSearchJWUom.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchJWUom.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchJWUom.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchJWUom.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchJWUom.Image = CType(resources.GetObject("cmdSearchJWUom.Image"), System.Drawing.Image)
        Me.cmdSearchJWUom.Location = New System.Drawing.Point(774, 15)
        Me.cmdSearchJWUom.Name = "cmdSearchJWUom"
        Me.cmdSearchJWUom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchJWUom.Size = New System.Drawing.Size(30, 24)
        Me.cmdSearchJWUom.TabIndex = 134
        Me.cmdSearchJWUom.TabStop = False
        Me.cmdSearchJWUom.Text = "5"
        Me.cmdSearchJWUom.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchJWUom, "Search")
        Me.cmdSearchJWUom.UseVisualStyleBackColor = False
        '
        'FraTrn
        '
        Me.FraTrn.BackColor = System.Drawing.SystemColors.Control
        Me.FraTrn.Controls.Add(Me.SSTInfo)
        Me.FraTrn.Controls.Add(Me.Frame2)
        Me.FraTrn.Controls.Add(Me.fraTop1)
        Me.FraTrn.Controls.Add(Me.Frame3)
        Me.FraTrn.Controls.Add(Me.Frame4)
        Me.FraTrn.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraTrn.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraTrn.Location = New System.Drawing.Point(1, -1)
        Me.FraTrn.Name = "FraTrn"
        Me.FraTrn.Padding = New System.Windows.Forms.Padding(0)
        Me.FraTrn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraTrn.Size = New System.Drawing.Size(1004, 560)
        Me.FraTrn.TabIndex = 105
        Me.FraTrn.TabStop = False
        '
        'SSTInfo
        '
        Me.SSTInfo.Controls.Add(Me._SSTInfo_TabPage0)
        Me.SSTInfo.Controls.Add(Me._SSTInfo_TabPage1)
        Me.SSTInfo.Controls.Add(Me._SSTInfo_TabPage2)
        Me.SSTInfo.Controls.Add(Me._SSTInfo_TabPage3)
        Me.SSTInfo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SSTInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.SSTInfo.ItemSize = New System.Drawing.Size(42, 18)
        Me.SSTInfo.Location = New System.Drawing.Point(0, 250)
        Me.SSTInfo.Name = "SSTInfo"
        Me.SSTInfo.SelectedIndex = 0
        Me.SSTInfo.Size = New System.Drawing.Size(1000, 312)
        Me.SSTInfo.TabIndex = 2
        '
        '_SSTInfo_TabPage0
        '
        Me._SSTInfo_TabPage0.Controls.Add(Me.FraView)
        Me._SSTInfo_TabPage0.Location = New System.Drawing.Point(4, 22)
        Me._SSTInfo_TabPage0.Name = "_SSTInfo_TabPage0"
        Me._SSTInfo_TabPage0.Size = New System.Drawing.Size(992, 286)
        Me._SSTInfo_TabPage0.TabIndex = 0
        Me._SSTInfo_TabPage0.Text = "Status"
        '
        'FraView
        '
        Me.FraView.BackColor = System.Drawing.SystemColors.Control
        Me.FraView.Controls.Add(Me.FraLock)
        Me.FraView.Controls.Add(Me.FraChecks)
        Me.FraView.Controls.Add(Me.cmdSearchPackType)
        Me.FraView.Controls.Add(Me.txtPackType)
        Me.FraView.Controls.Add(Me.Label3)
        Me.FraView.Controls.Add(Me.cmdSearchHSN)
        Me.FraView.Controls.Add(Me.cmdSearchScrap)
        Me.FraView.Controls.Add(Me.cmdSearchPIC)
        Me.FraView.Controls.Add(Me.cboGSTClass)
        Me.FraView.Controls.Add(Me.txtHSNCode)
        Me.FraView.Controls.Add(Me.txtProdType)
        Me.FraView.Controls.Add(Me.txtColor)
        Me.FraView.Controls.Add(Me.txtItemMake)
        Me.FraView.Controls.Add(Me.txtModel)
        Me.FraView.Controls.Add(Me.txtScrapItemCode)
        Me.FraView.Controls.Add(Me.CboStatus)
        Me.FraView.Controls.Add(Me.txtPackingItemCode)
        Me.FraView.Controls.Add(Me._lblUnder_59)
        Me.FraView.Controls.Add(Me._lblUnder_58)
        Me.FraView.Controls.Add(Me.lblHSNName)
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
        Me.FraView.Controls.Add(Me._lblUnder_10)
        Me.FraView.Controls.Add(Me._lblUnder_20)
        Me.FraView.Controls.Add(Me._lblUnder_32)
        Me.FraView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FraView.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraView.Location = New System.Drawing.Point(0, 0)
        Me.FraView.Name = "FraView"
        Me.FraView.Padding = New System.Windows.Forms.Padding(0)
        Me.FraView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraView.Size = New System.Drawing.Size(992, 286)
        Me.FraView.TabIndex = 134
        Me.FraView.TabStop = False
        '
        'FraLock
        '
        Me.FraLock.BackColor = System.Drawing.SystemColors.Control
        Me.FraLock.Controls.Add(Me.chkMRRLockingOM)
        Me.FraLock.Controls.Add(Me.chkMRRLocking)
        Me.FraLock.Controls.Add(Me.chkScheduleLocking)
        Me.FraLock.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraLock.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraLock.Location = New System.Drawing.Point(733, 182)
        Me.FraLock.Name = "FraLock"
        Me.FraLock.Padding = New System.Windows.Forms.Padding(0)
        Me.FraLock.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraLock.Size = New System.Drawing.Size(213, 92)
        Me.FraLock.TabIndex = 168
        Me.FraLock.TabStop = False
        '
        'chkMRRLockingOM
        '
        Me.chkMRRLockingOM.AutoSize = True
        Me.chkMRRLockingOM.BackColor = System.Drawing.SystemColors.Control
        Me.chkMRRLockingOM.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkMRRLockingOM.Enabled = False
        Me.chkMRRLockingOM.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMRRLockingOM.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkMRRLockingOM.Location = New System.Drawing.Point(4, 42)
        Me.chkMRRLockingOM.Name = "chkMRRLockingOM"
        Me.chkMRRLockingOM.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkMRRLockingOM.Size = New System.Drawing.Size(128, 17)
        Me.chkMRRLockingOM.TabIndex = 1
        Me.chkMRRLockingOM.Text = "MRR Lock Over Max"
        Me.chkMRRLockingOM.UseVisualStyleBackColor = False
        '
        'chkMRRLocking
        '
        Me.chkMRRLocking.AutoSize = True
        Me.chkMRRLocking.BackColor = System.Drawing.SystemColors.Control
        Me.chkMRRLocking.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkMRRLocking.Enabled = False
        Me.chkMRRLocking.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMRRLocking.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkMRRLocking.Location = New System.Drawing.Point(4, 16)
        Me.chkMRRLocking.Name = "chkMRRLocking"
        Me.chkMRRLocking.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkMRRLocking.Size = New System.Drawing.Size(76, 17)
        Me.chkMRRLocking.TabIndex = 0
        Me.chkMRRLocking.Text = "MRR Lock"
        Me.chkMRRLocking.UseVisualStyleBackColor = False
        '
        'chkScheduleLocking
        '
        Me.chkScheduleLocking.AutoSize = True
        Me.chkScheduleLocking.BackColor = System.Drawing.SystemColors.Control
        Me.chkScheduleLocking.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkScheduleLocking.Enabled = False
        Me.chkScheduleLocking.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScheduleLocking.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkScheduleLocking.Location = New System.Drawing.Point(4, 65)
        Me.chkScheduleLocking.Name = "chkScheduleLocking"
        Me.chkScheduleLocking.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkScheduleLocking.Size = New System.Drawing.Size(97, 17)
        Me.chkScheduleLocking.TabIndex = 2
        Me.chkScheduleLocking.Text = "Schedule Lock"
        Me.chkScheduleLocking.UseVisualStyleBackColor = False
        '
        'FraChecks
        '
        Me.FraChecks.BackColor = System.Drawing.SystemColors.Control
        Me.FraChecks.Controls.Add(Me.chkHeatReq)
        Me.FraChecks.Controls.Add(Me.chkAutoQC)
        Me.FraChecks.Controls.Add(Me.chkStockItem)
        Me.FraChecks.Controls.Add(Me.chkAutoIssue)
        Me.FraChecks.Controls.Add(Me.chkDrawing)
        Me.FraChecks.Controls.Add(Me.chkConsumable)
        Me.FraChecks.Controls.Add(Me.chkPOReqd)
        Me.FraChecks.Controls.Add(Me.chkRequired)
        Me.FraChecks.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraChecks.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraChecks.Location = New System.Drawing.Point(733, 0)
        Me.FraChecks.Name = "FraChecks"
        Me.FraChecks.Padding = New System.Windows.Forms.Padding(0)
        Me.FraChecks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraChecks.Size = New System.Drawing.Size(212, 176)
        Me.FraChecks.TabIndex = 104
        Me.FraChecks.TabStop = False
        '
        'chkHeatReq
        '
        Me.chkHeatReq.AutoSize = True
        Me.chkHeatReq.BackColor = System.Drawing.SystemColors.Control
        Me.chkHeatReq.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkHeatReq.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkHeatReq.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkHeatReq.Location = New System.Drawing.Point(5, 146)
        Me.chkHeatReq.Name = "chkHeatReq"
        Me.chkHeatReq.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkHeatReq.Size = New System.Drawing.Size(117, 17)
        Me.chkHeatReq.TabIndex = 53
        Me.chkHeatReq.Text = "Heat No Required"
        Me.chkHeatReq.UseVisualStyleBackColor = False
        '
        'chkAutoQC
        '
        Me.chkAutoQC.AutoSize = True
        Me.chkAutoQC.BackColor = System.Drawing.SystemColors.Control
        Me.chkAutoQC.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAutoQC.Enabled = False
        Me.chkAutoQC.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAutoQC.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAutoQC.Location = New System.Drawing.Point(4, 126)
        Me.chkAutoQC.Name = "chkAutoQC"
        Me.chkAutoQC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAutoQC.Size = New System.Drawing.Size(68, 17)
        Me.chkAutoQC.TabIndex = 52
        Me.chkAutoQC.Text = "Auto QC"
        Me.chkAutoQC.UseVisualStyleBackColor = False
        '
        'chkStockItem
        '
        Me.chkStockItem.AutoSize = True
        Me.chkStockItem.BackColor = System.Drawing.SystemColors.Control
        Me.chkStockItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkStockItem.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkStockItem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkStockItem.Location = New System.Drawing.Point(4, 87)
        Me.chkStockItem.Name = "chkStockItem"
        Me.chkStockItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkStockItem.Size = New System.Drawing.Size(80, 17)
        Me.chkStockItem.TabIndex = 3
        Me.chkStockItem.Text = "Stock Item"
        Me.chkStockItem.UseVisualStyleBackColor = False
        '
        'chkAutoIssue
        '
        Me.chkAutoIssue.AutoSize = True
        Me.chkAutoIssue.BackColor = System.Drawing.SystemColors.Control
        Me.chkAutoIssue.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAutoIssue.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAutoIssue.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAutoIssue.Location = New System.Drawing.Point(4, 49)
        Me.chkAutoIssue.Name = "chkAutoIssue"
        Me.chkAutoIssue.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAutoIssue.Size = New System.Drawing.Size(78, 17)
        Me.chkAutoIssue.TabIndex = 2
        Me.chkAutoIssue.Text = "Auto Issue"
        Me.chkAutoIssue.UseVisualStyleBackColor = False
        '
        'chkDrawing
        '
        Me.chkDrawing.AutoSize = True
        Me.chkDrawing.BackColor = System.Drawing.SystemColors.Control
        Me.chkDrawing.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkDrawing.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDrawing.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkDrawing.Location = New System.Drawing.Point(4, 30)
        Me.chkDrawing.Name = "chkDrawing"
        Me.chkDrawing.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkDrawing.Size = New System.Drawing.Size(118, 17)
        Me.chkDrawing.TabIndex = 1
        Me.chkDrawing.Text = "Drawing Available"
        Me.chkDrawing.UseVisualStyleBackColor = False
        '
        'chkConsumable
        '
        Me.chkConsumable.AutoSize = True
        Me.chkConsumable.BackColor = System.Drawing.SystemColors.Control
        Me.chkConsumable.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkConsumable.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkConsumable.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkConsumable.Location = New System.Drawing.Point(4, 11)
        Me.chkConsumable.Name = "chkConsumable"
        Me.chkConsumable.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkConsumable.Size = New System.Drawing.Size(89, 17)
        Me.chkConsumable.TabIndex = 0
        Me.chkConsumable.Text = "Consumable"
        Me.chkConsumable.UseVisualStyleBackColor = False
        '
        'chkPOReqd
        '
        Me.chkPOReqd.AutoSize = True
        Me.chkPOReqd.BackColor = System.Drawing.SystemColors.Control
        Me.chkPOReqd.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPOReqd.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPOReqd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPOReqd.Location = New System.Drawing.Point(4, 106)
        Me.chkPOReqd.Name = "chkPOReqd"
        Me.chkPOReqd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPOReqd.Size = New System.Drawing.Size(89, 17)
        Me.chkPOReqd.TabIndex = 4
        Me.chkPOReqd.Text = "PO Required"
        Me.chkPOReqd.UseVisualStyleBackColor = False
        '
        'chkRequired
        '
        Me.chkRequired.AutoSize = True
        Me.chkRequired.BackColor = System.Drawing.SystemColors.Control
        Me.chkRequired.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkRequired.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRequired.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRequired.Location = New System.Drawing.Point(4, 68)
        Me.chkRequired.Name = "chkRequired"
        Me.chkRequired.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkRequired.Size = New System.Drawing.Size(121, 17)
        Me.chkRequired.TabIndex = 51
        Me.chkRequired.Text = "Batch No Required"
        Me.chkRequired.UseVisualStyleBackColor = False
        '
        'txtPackType
        '
        Me.txtPackType.AcceptsReturn = True
        Me.txtPackType.BackColor = System.Drawing.SystemColors.Window
        Me.txtPackType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPackType.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPackType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPackType.ForeColor = System.Drawing.Color.Blue
        Me.txtPackType.Location = New System.Drawing.Point(453, 74)
        Me.txtPackType.MaxLength = 0
        Me.txtPackType.Name = "txtPackType"
        Me.txtPackType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPackType.Size = New System.Drawing.Size(114, 22)
        Me.txtPackType.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(409, 74)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(37, 13)
        Me.Label3.TabIndex = 194
        Me.Label3.Text = "Type :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cboGSTClass
        '
        Me.cboGSTClass.BackColor = System.Drawing.SystemColors.Window
        Me.cboGSTClass.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboGSTClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboGSTClass.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboGSTClass.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboGSTClass.Location = New System.Drawing.Point(143, 17)
        Me.cboGSTClass.Name = "cboGSTClass"
        Me.cboGSTClass.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboGSTClass.Size = New System.Drawing.Size(105, 21)
        Me.cboGSTClass.TabIndex = 0
        '
        'txtHSNCode
        '
        Me.txtHSNCode.AcceptsReturn = True
        Me.txtHSNCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtHSNCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtHSNCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtHSNCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHSNCode.ForeColor = System.Drawing.Color.Blue
        Me.txtHSNCode.Location = New System.Drawing.Point(143, 45)
        Me.txtHSNCode.MaxLength = 0
        Me.txtHSNCode.Name = "txtHSNCode"
        Me.txtHSNCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtHSNCode.Size = New System.Drawing.Size(106, 22)
        Me.txtHSNCode.TabIndex = 1
        '
        'txtProdType
        '
        Me.txtProdType.AcceptsReturn = True
        Me.txtProdType.BackColor = System.Drawing.SystemColors.Window
        Me.txtProdType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtProdType.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtProdType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProdType.ForeColor = System.Drawing.Color.Blue
        Me.txtProdType.Location = New System.Drawing.Point(143, 164)
        Me.txtProdType.MaxLength = 0
        Me.txtProdType.Name = "txtProdType"
        Me.txtProdType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProdType.Size = New System.Drawing.Size(259, 22)
        Me.txtProdType.TabIndex = 12
        '
        'txtColor
        '
        Me.txtColor.AcceptsReturn = True
        Me.txtColor.BackColor = System.Drawing.SystemColors.Window
        Me.txtColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtColor.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtColor.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtColor.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtColor.Location = New System.Drawing.Point(504, 136)
        Me.txtColor.MaxLength = 7
        Me.txtColor.Name = "txtColor"
        Me.txtColor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtColor.Size = New System.Drawing.Size(107, 22)
        Me.txtColor.TabIndex = 11
        '
        'txtItemMake
        '
        Me.txtItemMake.AcceptsReturn = True
        Me.txtItemMake.BackColor = System.Drawing.SystemColors.Window
        Me.txtItemMake.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemMake.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItemMake.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemMake.ForeColor = System.Drawing.Color.Blue
        Me.txtItemMake.Location = New System.Drawing.Point(328, 136)
        Me.txtItemMake.MaxLength = 0
        Me.txtItemMake.Name = "txtItemMake"
        Me.txtItemMake.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemMake.Size = New System.Drawing.Size(107, 22)
        Me.txtItemMake.TabIndex = 10
        '
        'txtModel
        '
        Me.txtModel.AcceptsReturn = True
        Me.txtModel.BackColor = System.Drawing.SystemColors.Window
        Me.txtModel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModel.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtModel.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtModel.ForeColor = System.Drawing.Color.Blue
        Me.txtModel.Location = New System.Drawing.Point(143, 136)
        Me.txtModel.MaxLength = 0
        Me.txtModel.Name = "txtModel"
        Me.txtModel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtModel.Size = New System.Drawing.Size(107, 22)
        Me.txtModel.TabIndex = 9
        '
        'txtScrapItemCode
        '
        Me.txtScrapItemCode.AcceptsReturn = True
        Me.txtScrapItemCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtScrapItemCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtScrapItemCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtScrapItemCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtScrapItemCode.ForeColor = System.Drawing.Color.Blue
        Me.txtScrapItemCode.Location = New System.Drawing.Point(143, 106)
        Me.txtScrapItemCode.MaxLength = 0
        Me.txtScrapItemCode.Name = "txtScrapItemCode"
        Me.txtScrapItemCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtScrapItemCode.Size = New System.Drawing.Size(420, 22)
        Me.txtScrapItemCode.TabIndex = 8
        '
        'CboStatus
        '
        Me.CboStatus.BackColor = System.Drawing.SystemColors.Window
        Me.CboStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.CboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboStatus.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboStatus.ForeColor = System.Drawing.SystemColors.WindowText
        Me.CboStatus.Location = New System.Drawing.Point(504, 164)
        Me.CboStatus.Name = "CboStatus"
        Me.CboStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CboStatus.Size = New System.Drawing.Size(105, 21)
        Me.CboStatus.TabIndex = 13
        '
        'txtPackingItemCode
        '
        Me.txtPackingItemCode.AcceptsReturn = True
        Me.txtPackingItemCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtPackingItemCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPackingItemCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPackingItemCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPackingItemCode.ForeColor = System.Drawing.Color.Blue
        Me.txtPackingItemCode.Location = New System.Drawing.Point(143, 74)
        Me.txtPackingItemCode.MaxLength = 0
        Me.txtPackingItemCode.Name = "txtPackingItemCode"
        Me.txtPackingItemCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPackingItemCode.Size = New System.Drawing.Size(228, 22)
        Me.txtPackingItemCode.TabIndex = 4
        '
        '_lblUnder_59
        '
        Me._lblUnder_59.AutoSize = True
        Me._lblUnder_59.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_59.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_59.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_59.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_59, CType(59, Short))
        Me._lblUnder_59.Location = New System.Drawing.Point(38, 20)
        Me._lblUnder_59.Name = "_lblUnder_59"
        Me._lblUnder_59.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_59.Size = New System.Drawing.Size(100, 13)
        Me._lblUnder_59.TabIndex = 186
        Me._lblUnder_59.Text = "GST Classification:"
        Me._lblUnder_59.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_58
        '
        Me._lblUnder_58.AutoSize = True
        Me._lblUnder_58.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_58.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_58.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_58.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_58, CType(58, Short))
        Me._lblUnder_58.Location = New System.Drawing.Point(73, 45)
        Me._lblUnder_58.Name = "_lblUnder_58"
        Me._lblUnder_58.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_58.Size = New System.Drawing.Size(65, 13)
        Me._lblUnder_58.TabIndex = 185
        Me._lblUnder_58.Text = "HSN Code :"
        Me._lblUnder_58.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblHSNName
        '
        Me.lblHSNName.BackColor = System.Drawing.SystemColors.Control
        Me.lblHSNName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblHSNName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblHSNName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHSNName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblHSNName.Location = New System.Drawing.Point(286, 45)
        Me.lblHSNName.Name = "lblHSNName"
        Me.lblHSNName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblHSNName.Size = New System.Drawing.Size(280, 19)
        Me.lblHSNName.TabIndex = 3
        Me.lblHSNName.Text = "lblHSNName"
        '
        '_lblUnder_47
        '
        Me._lblUnder_47.AutoSize = True
        Me._lblUnder_47.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_47.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_47.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_47.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_47, CType(47, Short))
        Me._lblUnder_47.Location = New System.Drawing.Point(59, 169)
        Me._lblUnder_47.Name = "_lblUnder_47"
        Me._lblUnder_47.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_47.Size = New System.Drawing.Size(79, 13)
        Me._lblUnder_47.TabIndex = 167
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
        Me._lblUnder_23.Location = New System.Drawing.Point(92, 140)
        Me._lblUnder_23.Name = "_lblUnder_23"
        Me._lblUnder_23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_23.Size = New System.Drawing.Size(46, 13)
        Me._lblUnder_23.TabIndex = 165
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
        Me._lblUnder_24.Location = New System.Drawing.Point(279, 141)
        Me._lblUnder_24.Name = "_lblUnder_24"
        Me._lblUnder_24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_24.Size = New System.Drawing.Size(41, 13)
        Me._lblUnder_24.TabIndex = 164
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
        Me._lblUnder_25.Location = New System.Drawing.Point(454, 141)
        Me._lblUnder_25.Name = "_lblUnder_25"
        Me._lblUnder_25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_25.Size = New System.Drawing.Size(41, 13)
        Me._lblUnder_25.TabIndex = 163
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
        Me.Label44.Location = New System.Drawing.Point(17, 223)
        Me.Label44.Name = "Label44"
        Me.Label44.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label44.Size = New System.Drawing.Size(61, 15)
        Me.Label44.TabIndex = 150
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
        Me.lblAddUser.Location = New System.Drawing.Point(82, 219)
        Me.lblAddUser.Name = "lblAddUser"
        Me.lblAddUser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAddUser.Size = New System.Drawing.Size(87, 23)
        Me.lblAddUser.TabIndex = 149
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.BackColor = System.Drawing.SystemColors.Control
        Me.Label46.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label46.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label46.Location = New System.Drawing.Point(357, 223)
        Me.Label46.Name = "Label46"
        Me.Label46.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label46.Size = New System.Drawing.Size(61, 15)
        Me.Label46.TabIndex = 148
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
        Me.lblModUser.Location = New System.Drawing.Point(422, 219)
        Me.lblModUser.Name = "lblModUser"
        Me.lblModUser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblModUser.Size = New System.Drawing.Size(87, 23)
        Me.lblModUser.TabIndex = 147
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.BackColor = System.Drawing.SystemColors.Control
        Me.Label45.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label45.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label45.Location = New System.Drawing.Point(185, 223)
        Me.Label45.Name = "Label45"
        Me.Label45.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label45.Size = New System.Drawing.Size(63, 15)
        Me.Label45.TabIndex = 146
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
        Me.lblAddDate.Location = New System.Drawing.Point(252, 220)
        Me.lblAddDate.Name = "lblAddDate"
        Me.lblAddDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAddDate.Size = New System.Drawing.Size(87, 23)
        Me.lblAddDate.TabIndex = 145
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.BackColor = System.Drawing.SystemColors.Control
        Me.Label48.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label48.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label48.Location = New System.Drawing.Point(525, 223)
        Me.Label48.Name = "Label48"
        Me.Label48.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label48.Size = New System.Drawing.Size(63, 15)
        Me.Label48.TabIndex = 144
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
        Me.lblModDate.Location = New System.Drawing.Point(592, 218)
        Me.lblModDate.Name = "lblModDate"
        Me.lblModDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblModDate.Size = New System.Drawing.Size(87, 23)
        Me.lblModDate.TabIndex = 143
        '
        '_lblUnder_10
        '
        Me._lblUnder_10.AutoSize = True
        Me._lblUnder_10.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_10.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_10.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_10, CType(10, Short))
        Me._lblUnder_10.Location = New System.Drawing.Point(38, 106)
        Me._lblUnder_10.Name = "_lblUnder_10"
        Me._lblUnder_10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_10.Size = New System.Drawing.Size(100, 13)
        Me._lblUnder_10.TabIndex = 137
        Me._lblUnder_10.Text = "Scrap Item Name :"
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
        Me._lblUnder_20.Location = New System.Drawing.Point(452, 169)
        Me._lblUnder_20.Name = "_lblUnder_20"
        Me._lblUnder_20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_20.Size = New System.Drawing.Size(41, 13)
        Me._lblUnder_20.TabIndex = 46
        Me._lblUnder_20.Text = "Status:"
        Me._lblUnder_20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_32
        '
        Me._lblUnder_32.AutoSize = True
        Me._lblUnder_32.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_32.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_32.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_32.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_32, CType(32, Short))
        Me._lblUnder_32.Location = New System.Drawing.Point(27, 74)
        Me._lblUnder_32.Name = "_lblUnder_32"
        Me._lblUnder_32.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_32.Size = New System.Drawing.Size(111, 13)
        Me._lblUnder_32.TabIndex = 136
        Me._lblUnder_32.Text = "Packing Item Name :"
        Me._lblUnder_32.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_SSTInfo_TabPage1
        '
        Me._SSTInfo_TabPage1.Controls.Add(Me._lblUnder_26)
        Me._SSTInfo_TabPage1.Controls.Add(Me._lblUnder_28)
        Me._SSTInfo_TabPage1.Controls.Add(Me._lblUnder_29)
        Me._SSTInfo_TabPage1.Controls.Add(Me._lblUnder_40)
        Me._SSTInfo_TabPage1.Controls.Add(Me._lblUnder_41)
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
        Me._SSTInfo_TabPage1.Controls.Add(Me._lblUnder_46)
        Me._SSTInfo_TabPage1.Controls.Add(Me._lblUnder_63)
        Me._SSTInfo_TabPage1.Controls.Add(Me.txtWeight)
        Me._SSTInfo_TabPage1.Controls.Add(Me.txtDimention)
        Me._SSTInfo_TabPage1.Controls.Add(Me.txtSpecification)
        Me._SSTInfo_TabPage1.Controls.Add(Me.txtPackingStandard)
        Me._SSTInfo_TabPage1.Controls.Add(Me.txtScrapWeight)
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
        Me._SSTInfo_TabPage1.Controls.Add(Me.txtParentItemName)
        Me._SSTInfo_TabPage1.Controls.Add(Me.chkChildItem)
        Me._SSTInfo_TabPage1.Controls.Add(Me.txtWtPerStrip)
        Me._SSTInfo_TabPage1.Controls.Add(Me._lblUnder_31)
        Me._SSTInfo_TabPage1.Location = New System.Drawing.Point(4, 22)
        Me._SSTInfo_TabPage1.Name = "_SSTInfo_TabPage1"
        Me._SSTInfo_TabPage1.Size = New System.Drawing.Size(992, 286)
        Me._SSTInfo_TabPage1.TabIndex = 1
        Me._SSTInfo_TabPage1.Text = "Technical"
        '
        '_lblUnder_26
        '
        Me._lblUnder_26.AutoSize = True
        Me._lblUnder_26.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_26.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_26.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_26, CType(26, Short))
        Me._lblUnder_26.Location = New System.Drawing.Point(27, 98)
        Me._lblUnder_26.Name = "_lblUnder_26"
        Me._lblUnder_26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_26.Size = New System.Drawing.Size(106, 13)
        Me._lblUnder_26.TabIndex = 138
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
        Me._lblUnder_28.Location = New System.Drawing.Point(426, 68)
        Me._lblUnder_28.Name = "_lblUnder_28"
        Me._lblUnder_28.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_28.Size = New System.Drawing.Size(66, 13)
        Me._lblUnder_28.TabIndex = 139
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
        Me._lblUnder_29.Location = New System.Drawing.Point(723, 68)
        Me._lblUnder_29.Name = "_lblUnder_29"
        Me._lblUnder_29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_29.Size = New System.Drawing.Size(78, 13)
        Me._lblUnder_29.TabIndex = 140
        Me._lblUnder_29.Text = "Specification :"
        Me._lblUnder_29.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_40
        '
        Me._lblUnder_40.AutoSize = True
        Me._lblUnder_40.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_40.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_40.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_40.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_40, CType(40, Short))
        Me._lblUnder_40.Location = New System.Drawing.Point(391, 149)
        Me._lblUnder_40.Name = "_lblUnder_40"
        Me._lblUnder_40.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_40.Size = New System.Drawing.Size(101, 13)
        Me._lblUnder_40.TabIndex = 141
        Me._lblUnder_40.Text = "Packing Standard :"
        Me._lblUnder_40.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_41
        '
        Me._lblUnder_41.AutoSize = True
        Me._lblUnder_41.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_41.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_41.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_41.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_41, CType(41, Short))
        Me._lblUnder_41.Location = New System.Drawing.Point(411, 98)
        Me._lblUnder_41.Name = "_lblUnder_41"
        Me._lblUnder_41.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_41.Size = New System.Drawing.Size(81, 13)
        Me._lblUnder_41.TabIndex = 142
        Me._lblUnder_41.Text = "Scrap Weight :"
        Me._lblUnder_41.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_42
        '
        Me._lblUnder_42.AutoSize = True
        Me._lblUnder_42.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_42.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_42.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_42.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_42, CType(42, Short))
        Me._lblUnder_42.Location = New System.Drawing.Point(431, 39)
        Me._lblUnder_42.Name = "_lblUnder_42"
        Me._lblUnder_42.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_42.Size = New System.Drawing.Size(61, 13)
        Me._lblUnder_42.TabIndex = 153
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
        Me._lblUnder_43.Location = New System.Drawing.Point(226, 39)
        Me._lblUnder_43.Name = "_lblUnder_43"
        Me._lblUnder_43.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_43.Size = New System.Drawing.Size(44, 13)
        Me._lblUnder_43.TabIndex = 154
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
        Me._lblUnder_44.Location = New System.Drawing.Point(86, 39)
        Me._lblUnder_44.Name = "_lblUnder_44"
        Me._lblUnder_44.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_44.Size = New System.Drawing.Size(47, 13)
        Me._lblUnder_44.TabIndex = 155
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
        Me._lblUnder_45.Location = New System.Drawing.Point(82, 68)
        Me._lblUnder_45.Name = "_lblUnder_45"
        Me._lblUnder_45.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_45.Size = New System.Drawing.Size(51, 13)
        Me._lblUnder_45.TabIndex = 156
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
        Me._lblUnder_13.Location = New System.Drawing.Point(745, 149)
        Me._lblUnder_13.Name = "_lblUnder_13"
        Me._lblUnder_13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_13.Size = New System.Drawing.Size(56, 13)
        Me._lblUnder_13.TabIndex = 157
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
        Me._lblUnder_34.Location = New System.Drawing.Point(81, 149)
        Me._lblUnder_34.Name = "_lblUnder_34"
        Me._lblUnder_34.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_34.Size = New System.Drawing.Size(52, 13)
        Me._lblUnder_34.TabIndex = 158
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
        Me._lblUnder_38.Location = New System.Drawing.Point(730, 98)
        Me._lblUnder_38.Name = "_lblUnder_38"
        Me._lblUnder_38.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_38.Size = New System.Drawing.Size(71, 13)
        Me._lblUnder_38.TabIndex = 159
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
        Me._lblUnder_37.Location = New System.Drawing.Point(700, 125)
        Me._lblUnder_37.Name = "_lblUnder_37"
        Me._lblUnder_37.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_37.Size = New System.Drawing.Size(101, 13)
        Me._lblUnder_37.TabIndex = 160
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
        Me._lblUnder_35.Location = New System.Drawing.Point(60, 125)
        Me._lblUnder_35.Name = "_lblUnder_35"
        Me._lblUnder_35.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_35.Size = New System.Drawing.Size(73, 13)
        Me._lblUnder_35.TabIndex = 161
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
        Me._lblUnder_36.Location = New System.Drawing.Point(397, 125)
        Me._lblUnder_36.Name = "_lblUnder_36"
        Me._lblUnder_36.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_36.Size = New System.Drawing.Size(95, 13)
        Me._lblUnder_36.TabIndex = 162
        Me._lblUnder_36.Text = "Drawing Rev No :"
        Me._lblUnder_36.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_46
        '
        Me._lblUnder_46.AutoSize = True
        Me._lblUnder_46.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_46.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_46.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_46.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_46, CType(46, Short))
        Me._lblUnder_46.Location = New System.Drawing.Point(29, 177)
        Me._lblUnder_46.Name = "_lblUnder_46"
        Me._lblUnder_46.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_46.Size = New System.Drawing.Size(104, 13)
        Me._lblUnder_46.TabIndex = 166
        Me._lblUnder_46.Text = "Parent Item Name :"
        Me._lblUnder_46.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_63
        '
        Me._lblUnder_63.AutoSize = True
        Me._lblUnder_63.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_63.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_63.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_63.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_63, CType(63, Short))
        Me._lblUnder_63.Location = New System.Drawing.Point(693, 39)
        Me._lblUnder_63.Name = "_lblUnder_63"
        Me._lblUnder_63.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_63.Size = New System.Drawing.Size(108, 13)
        Me._lblUnder_63.TabIndex = 199
        Me._lblUnder_63.Text = "Wt. / Strip (In Kgs) :"
        Me._lblUnder_63.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtWeight
        '
        Me.txtWeight.AcceptsReturn = True
        Me.txtWeight.BackColor = System.Drawing.SystemColors.Window
        Me.txtWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWeight.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWeight.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWeight.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtWeight.Location = New System.Drawing.Point(142, 91)
        Me.txtWeight.MaxLength = 7
        Me.txtWeight.Name = "txtWeight"
        Me.txtWeight.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWeight.Size = New System.Drawing.Size(105, 22)
        Me.txtWeight.TabIndex = 62
        '
        'txtDimention
        '
        Me.txtDimention.AcceptsReturn = True
        Me.txtDimention.BackColor = System.Drawing.SystemColors.Window
        Me.txtDimention.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDimention.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDimention.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDimention.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDimention.Location = New System.Drawing.Point(498, 63)
        Me.txtDimention.MaxLength = 7
        Me.txtDimention.Name = "txtDimention"
        Me.txtDimention.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDimention.Size = New System.Drawing.Size(109, 22)
        Me.txtDimention.TabIndex = 60
        '
        'txtSpecification
        '
        Me.txtSpecification.AcceptsReturn = True
        Me.txtSpecification.BackColor = System.Drawing.SystemColors.Window
        Me.txtSpecification.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSpecification.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSpecification.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSpecification.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSpecification.Location = New System.Drawing.Point(806, 63)
        Me.txtSpecification.MaxLength = 7
        Me.txtSpecification.Name = "txtSpecification"
        Me.txtSpecification.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSpecification.Size = New System.Drawing.Size(115, 22)
        Me.txtSpecification.TabIndex = 61
        '
        'txtPackingStandard
        '
        Me.txtPackingStandard.AcceptsReturn = True
        Me.txtPackingStandard.BackColor = System.Drawing.SystemColors.Window
        Me.txtPackingStandard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPackingStandard.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPackingStandard.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPackingStandard.ForeColor = System.Drawing.Color.Blue
        Me.txtPackingStandard.Location = New System.Drawing.Point(499, 147)
        Me.txtPackingStandard.MaxLength = 0
        Me.txtPackingStandard.Name = "txtPackingStandard"
        Me.txtPackingStandard.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPackingStandard.Size = New System.Drawing.Size(109, 22)
        Me.txtPackingStandard.TabIndex = 69
        '
        'txtScrapWeight
        '
        Me.txtScrapWeight.AcceptsReturn = True
        Me.txtScrapWeight.BackColor = System.Drawing.SystemColors.Window
        Me.txtScrapWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtScrapWeight.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtScrapWeight.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtScrapWeight.ForeColor = System.Drawing.Color.Blue
        Me.txtScrapWeight.Location = New System.Drawing.Point(498, 91)
        Me.txtScrapWeight.MaxLength = 0
        Me.txtScrapWeight.Name = "txtScrapWeight"
        Me.txtScrapWeight.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtScrapWeight.Size = New System.Drawing.Size(109, 22)
        Me.txtScrapWeight.TabIndex = 63
        '
        'txtMaterial
        '
        Me.txtMaterial.AcceptsReturn = True
        Me.txtMaterial.BackColor = System.Drawing.SystemColors.Window
        Me.txtMaterial.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMaterial.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMaterial.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMaterial.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtMaterial.Location = New System.Drawing.Point(142, 7)
        Me.txtMaterial.MaxLength = 7
        Me.txtMaterial.Name = "txtMaterial"
        Me.txtMaterial.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMaterial.Size = New System.Drawing.Size(603, 22)
        Me.txtMaterial.TabIndex = 54
        '
        'txtThickness
        '
        Me.txtThickness.AcceptsReturn = True
        Me.txtThickness.BackColor = System.Drawing.SystemColors.Window
        Me.txtThickness.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtThickness.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtThickness.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtThickness.ForeColor = System.Drawing.Color.Blue
        Me.txtThickness.Location = New System.Drawing.Point(498, 35)
        Me.txtThickness.MaxLength = 0
        Me.txtThickness.Name = "txtThickness"
        Me.txtThickness.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtThickness.Size = New System.Drawing.Size(69, 22)
        Me.txtThickness.TabIndex = 57
        '
        'txtWidth
        '
        Me.txtWidth.AcceptsReturn = True
        Me.txtWidth.BackColor = System.Drawing.SystemColors.Window
        Me.txtWidth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWidth.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWidth.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWidth.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtWidth.Location = New System.Drawing.Point(283, 35)
        Me.txtWidth.MaxLength = 7
        Me.txtWidth.Name = "txtWidth"
        Me.txtWidth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWidth.Size = New System.Drawing.Size(69, 22)
        Me.txtWidth.TabIndex = 56
        '
        'txtLength
        '
        Me.txtLength.AcceptsReturn = True
        Me.txtLength.BackColor = System.Drawing.SystemColors.Window
        Me.txtLength.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLength.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLength.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLength.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtLength.Location = New System.Drawing.Point(142, 35)
        Me.txtLength.MaxLength = 7
        Me.txtLength.Name = "txtLength"
        Me.txtLength.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLength.Size = New System.Drawing.Size(69, 22)
        Me.txtLength.TabIndex = 55
        '
        'txtDensity
        '
        Me.txtDensity.AcceptsReturn = True
        Me.txtDensity.BackColor = System.Drawing.SystemColors.Window
        Me.txtDensity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDensity.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDensity.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDensity.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDensity.Location = New System.Drawing.Point(142, 63)
        Me.txtDensity.MaxLength = 7
        Me.txtDensity.Name = "txtDensity"
        Me.txtDensity.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDensity.Size = New System.Drawing.Size(105, 22)
        Me.txtDensity.TabIndex = 59
        '
        'txtLocation
        '
        Me.txtLocation.AcceptsReturn = True
        Me.txtLocation.BackColor = System.Drawing.SystemColors.Window
        Me.txtLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLocation.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLocation.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.ForeColor = System.Drawing.Color.Blue
        Me.txtLocation.Location = New System.Drawing.Point(806, 147)
        Me.txtLocation.MaxLength = 0
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLocation.Size = New System.Drawing.Size(115, 22)
        Me.txtLocation.TabIndex = 70
        '
        'txtInspectionNo
        '
        Me.txtInspectionNo.AcceptsReturn = True
        Me.txtInspectionNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtInspectionNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInspectionNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInspectionNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInspectionNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtInspectionNo.Location = New System.Drawing.Point(142, 147)
        Me.txtInspectionNo.MaxLength = 7
        Me.txtInspectionNo.Name = "txtInspectionNo"
        Me.txtInspectionNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInspectionNo.Size = New System.Drawing.Size(105, 22)
        Me.txtInspectionNo.TabIndex = 68
        '
        'txtIdMark
        '
        Me.txtIdMark.AcceptsReturn = True
        Me.txtIdMark.BackColor = System.Drawing.SystemColors.Window
        Me.txtIdMark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIdMark.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIdMark.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIdMark.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtIdMark.Location = New System.Drawing.Point(806, 91)
        Me.txtIdMark.MaxLength = 7
        Me.txtIdMark.Name = "txtIdMark"
        Me.txtIdMark.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIdMark.Size = New System.Drawing.Size(115, 22)
        Me.txtIdMark.TabIndex = 64
        '
        'txtDwgRevDate
        '
        Me.txtDwgRevDate.AcceptsReturn = True
        Me.txtDwgRevDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtDwgRevDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDwgRevDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDwgRevDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDwgRevDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDwgRevDate.Location = New System.Drawing.Point(806, 119)
        Me.txtDwgRevDate.MaxLength = 7
        Me.txtDwgRevDate.Name = "txtDwgRevDate"
        Me.txtDwgRevDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDwgRevDate.Size = New System.Drawing.Size(115, 22)
        Me.txtDwgRevDate.TabIndex = 67
        '
        'txtDwgNo
        '
        Me.txtDwgNo.AcceptsReturn = True
        Me.txtDwgNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtDwgNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDwgNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDwgNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDwgNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDwgNo.Location = New System.Drawing.Point(142, 119)
        Me.txtDwgNo.MaxLength = 7
        Me.txtDwgNo.Name = "txtDwgNo"
        Me.txtDwgNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDwgNo.Size = New System.Drawing.Size(105, 22)
        Me.txtDwgNo.TabIndex = 65
        '
        'txtDwgRevNo
        '
        Me.txtDwgRevNo.AcceptsReturn = True
        Me.txtDwgRevNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtDwgRevNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDwgRevNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDwgRevNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDwgRevNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDwgRevNo.Location = New System.Drawing.Point(498, 119)
        Me.txtDwgRevNo.MaxLength = 7
        Me.txtDwgRevNo.Name = "txtDwgRevNo"
        Me.txtDwgRevNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDwgRevNo.Size = New System.Drawing.Size(109, 22)
        Me.txtDwgRevNo.TabIndex = 66
        '
        'txtParentItemName
        '
        Me.txtParentItemName.AcceptsReturn = True
        Me.txtParentItemName.BackColor = System.Drawing.SystemColors.Window
        Me.txtParentItemName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtParentItemName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtParentItemName.Enabled = False
        Me.txtParentItemName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtParentItemName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtParentItemName.Location = New System.Drawing.Point(142, 175)
        Me.txtParentItemName.MaxLength = 7
        Me.txtParentItemName.Name = "txtParentItemName"
        Me.txtParentItemName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtParentItemName.Size = New System.Drawing.Size(259, 22)
        Me.txtParentItemName.TabIndex = 71
        '
        'chkChildItem
        '
        Me.chkChildItem.BackColor = System.Drawing.SystemColors.Control
        Me.chkChildItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkChildItem.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkChildItem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkChildItem.Location = New System.Drawing.Point(407, 176)
        Me.chkChildItem.Name = "chkChildItem"
        Me.chkChildItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkChildItem.Size = New System.Drawing.Size(95, 22)
        Me.chkChildItem.TabIndex = 72
        Me.chkChildItem.Text = "Is Child Item"
        Me.chkChildItem.UseVisualStyleBackColor = False
        '
        'txtWtPerStrip
        '
        Me.txtWtPerStrip.AcceptsReturn = True
        Me.txtWtPerStrip.BackColor = System.Drawing.SystemColors.Window
        Me.txtWtPerStrip.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWtPerStrip.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWtPerStrip.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWtPerStrip.ForeColor = System.Drawing.Color.Blue
        Me.txtWtPerStrip.Location = New System.Drawing.Point(806, 35)
        Me.txtWtPerStrip.MaxLength = 0
        Me.txtWtPerStrip.Name = "txtWtPerStrip"
        Me.txtWtPerStrip.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWtPerStrip.Size = New System.Drawing.Size(87, 22)
        Me.txtWtPerStrip.TabIndex = 58
        '
        '_lblUnder_31
        '
        Me._lblUnder_31.AutoSize = True
        Me._lblUnder_31.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_31.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_31.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_31.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_31, CType(31, Short))
        Me._lblUnder_31.Location = New System.Drawing.Point(17, 8)
        Me._lblUnder_31.Name = "_lblUnder_31"
        Me._lblUnder_31.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_31.Size = New System.Drawing.Size(116, 13)
        Me._lblUnder_31.TabIndex = 152
        Me._lblUnder_31.Text = "Material Description :"
        Me._lblUnder_31.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_SSTInfo_TabPage2
        '
        Me._SSTInfo_TabPage2.Controls.Add(Me.chkSB)
        Me._SSTInfo_TabPage2.Controls.Add(Me.chkGrinding)
        Me._SSTInfo_TabPage2.Controls.Add(Me.FraWeld)
        Me._SSTInfo_TabPage2.Controls.Add(Me.FraSurface)
        Me._SSTInfo_TabPage2.Controls.Add(Me.FraPress)
        Me._SSTInfo_TabPage2.Controls.Add(Me.Frame1)
        Me._SSTInfo_TabPage2.Location = New System.Drawing.Point(4, 22)
        Me._SSTInfo_TabPage2.Name = "_SSTInfo_TabPage2"
        Me._SSTInfo_TabPage2.Size = New System.Drawing.Size(992, 286)
        Me._SSTInfo_TabPage2.TabIndex = 2
        Me._SSTInfo_TabPage2.Text = "Process"
        '
        'chkSB
        '
        Me.chkSB.BackColor = System.Drawing.SystemColors.Control
        Me.chkSB.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkSB.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSB.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkSB.Location = New System.Drawing.Point(258, 199)
        Me.chkSB.Name = "chkSB"
        Me.chkSB.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkSB.Size = New System.Drawing.Size(167, 19)
        Me.chkSB.TabIndex = 184
        Me.chkSB.Text = "Shot Blasting (Yes / No)"
        Me.chkSB.UseVisualStyleBackColor = False
        '
        'chkGrinding
        '
        Me.chkGrinding.BackColor = System.Drawing.SystemColors.Control
        Me.chkGrinding.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkGrinding.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGrinding.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkGrinding.Location = New System.Drawing.Point(258, 138)
        Me.chkGrinding.Name = "chkGrinding"
        Me.chkGrinding.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkGrinding.Size = New System.Drawing.Size(191, 22)
        Me.chkGrinding.TabIndex = 181
        Me.chkGrinding.Text = "Grinding / Buffing (Yes / No)"
        Me.chkGrinding.UseVisualStyleBackColor = False
        '
        'FraWeld
        '
        Me.FraWeld.BackColor = System.Drawing.SystemColors.Control
        Me.FraWeld.Controls.Add(Me.Frame6)
        Me.FraWeld.Controls.Add(Me.Frame5)
        Me.FraWeld.Controls.Add(Me.cboWeldingLine)
        Me.FraWeld.Controls.Add(Me._lblUnder_48)
        Me.FraWeld.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraWeld.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraWeld.Location = New System.Drawing.Point(3, 1)
        Me.FraWeld.Name = "FraWeld"
        Me.FraWeld.Padding = New System.Windows.Forms.Padding(0)
        Me.FraWeld.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraWeld.Size = New System.Drawing.Size(247, 223)
        Me.FraWeld.TabIndex = 173
        Me.FraWeld.TabStop = False
        Me.FraWeld.Text = "Welding"
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.txtSSWLength)
        Me.Frame6.Controls.Add(Me.txtWLength)
        Me.Frame6.Controls.Add(Me._lblUnder_49)
        Me.Frame6.Controls.Add(Me._lblUnder_12)
        Me.Frame6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(0, 37)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(247, 77)
        Me.Frame6.TabIndex = 196
        Me.Frame6.TabStop = False
        Me.Frame6.Text = "Inhouse (Length in Inch)"
        '
        'txtSSWLength
        '
        Me.txtSSWLength.AcceptsReturn = True
        Me.txtSSWLength.BackColor = System.Drawing.SystemColors.Window
        Me.txtSSWLength.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSSWLength.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSSWLength.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSSWLength.ForeColor = System.Drawing.Color.Blue
        Me.txtSSWLength.Location = New System.Drawing.Point(135, 46)
        Me.txtSSWLength.MaxLength = 0
        Me.txtSSWLength.Name = "txtSSWLength"
        Me.txtSSWLength.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSSWLength.Size = New System.Drawing.Size(105, 22)
        Me.txtSSWLength.TabIndex = 75
        '
        'txtWLength
        '
        Me.txtWLength.AcceptsReturn = True
        Me.txtWLength.BackColor = System.Drawing.SystemColors.Window
        Me.txtWLength.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWLength.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWLength.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWLength.ForeColor = System.Drawing.Color.Blue
        Me.txtWLength.Location = New System.Drawing.Point(135, 24)
        Me.txtWLength.MaxLength = 0
        Me.txtWLength.Name = "txtWLength"
        Me.txtWLength.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWLength.Size = New System.Drawing.Size(105, 22)
        Me.txtWLength.TabIndex = 74
        '
        '_lblUnder_49
        '
        Me._lblUnder_49.AutoSize = True
        Me._lblUnder_49.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_49.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_49.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_49.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_49, CType(49, Short))
        Me._lblUnder_49.Location = New System.Drawing.Point(19, 48)
        Me._lblUnder_49.Name = "_lblUnder_49"
        Me._lblUnder_49.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_49.Size = New System.Drawing.Size(92, 13)
        Me._lblUnder_49.TabIndex = 198
        Me._lblUnder_49.Text = "SS Weld Length :"
        '
        '_lblUnder_12
        '
        Me._lblUnder_12.AutoSize = True
        Me._lblUnder_12.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_12.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_12.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_12, CType(12, Short))
        Me._lblUnder_12.Location = New System.Drawing.Point(9, 26)
        Me._lblUnder_12.Name = "_lblUnder_12"
        Me._lblUnder_12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_12.Size = New System.Drawing.Size(101, 13)
        Me._lblUnder_12.TabIndex = 197
        Me._lblUnder_12.Text = "MIG Weld Length :"
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me.txtSSWLengthCust)
        Me.Frame5.Controls.Add(Me.txtWLengthCust)
        Me.Frame5.Controls.Add(Me._lblUnder_62)
        Me.Frame5.Controls.Add(Me._lblUnder_60)
        Me.Frame5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(0, 122)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(247, 78)
        Me.Frame5.TabIndex = 193
        Me.Frame5.TabStop = False
        Me.Frame5.Text = "Given by Customer (Length in Inch)"
        '
        'txtSSWLengthCust
        '
        Me.txtSSWLengthCust.AcceptsReturn = True
        Me.txtSSWLengthCust.BackColor = System.Drawing.SystemColors.Window
        Me.txtSSWLengthCust.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSSWLengthCust.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSSWLengthCust.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSSWLengthCust.ForeColor = System.Drawing.Color.Blue
        Me.txtSSWLengthCust.Location = New System.Drawing.Point(135, 45)
        Me.txtSSWLengthCust.MaxLength = 0
        Me.txtSSWLengthCust.Name = "txtSSWLengthCust"
        Me.txtSSWLengthCust.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSSWLengthCust.Size = New System.Drawing.Size(105, 22)
        Me.txtSSWLengthCust.TabIndex = 77
        '
        'txtWLengthCust
        '
        Me.txtWLengthCust.AcceptsReturn = True
        Me.txtWLengthCust.BackColor = System.Drawing.SystemColors.Window
        Me.txtWLengthCust.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWLengthCust.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWLengthCust.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWLengthCust.ForeColor = System.Drawing.Color.Blue
        Me.txtWLengthCust.Location = New System.Drawing.Point(135, 21)
        Me.txtWLengthCust.MaxLength = 0
        Me.txtWLengthCust.Name = "txtWLengthCust"
        Me.txtWLengthCust.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWLengthCust.Size = New System.Drawing.Size(105, 22)
        Me.txtWLengthCust.TabIndex = 76
        '
        '_lblUnder_62
        '
        Me._lblUnder_62.AutoSize = True
        Me._lblUnder_62.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_62.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_62.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_62.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_62, CType(62, Short))
        Me._lblUnder_62.Location = New System.Drawing.Point(22, 48)
        Me._lblUnder_62.Name = "_lblUnder_62"
        Me._lblUnder_62.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_62.Size = New System.Drawing.Size(92, 13)
        Me._lblUnder_62.TabIndex = 195
        Me._lblUnder_62.Text = "SS Weld Length :"
        '
        '_lblUnder_60
        '
        Me._lblUnder_60.AutoSize = True
        Me._lblUnder_60.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_60.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_60.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_60.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_60, CType(60, Short))
        Me._lblUnder_60.Location = New System.Drawing.Point(11, 23)
        Me._lblUnder_60.Name = "_lblUnder_60"
        Me._lblUnder_60.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_60.Size = New System.Drawing.Size(101, 13)
        Me._lblUnder_60.TabIndex = 194
        Me._lblUnder_60.Text = "MIG Weld Length :"
        '
        '_lblUnder_48
        '
        Me._lblUnder_48.AutoSize = True
        Me._lblUnder_48.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_48.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_48.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_48.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_48, CType(48, Short))
        Me._lblUnder_48.Location = New System.Drawing.Point(36, 14)
        Me._lblUnder_48.Name = "_lblUnder_48"
        Me._lblUnder_48.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_48.Size = New System.Drawing.Size(79, 13)
        Me._lblUnder_48.TabIndex = 174
        Me._lblUnder_48.Text = "Welding Line :"
        '
        'FraSurface
        '
        Me.FraSurface.BackColor = System.Drawing.SystemColors.Control
        Me.FraSurface.Controls.Add(Me.txtAddSurfaceAreaPLT)
        Me.FraSurface.Controls.Add(Me.txtAddSurfaceAreaNPC)
        Me.FraSurface.Controls.Add(Me.txtAddSurfaceAreaPPS)
        Me.FraSurface.Controls.Add(Me.txtSurfaceArea)
        Me.FraSurface.Controls.Add(Me.cboSurfaceTreatment)
        Me.FraSurface.Controls.Add(Me.txtSurfaceAreaInner)
        Me.FraSurface.Controls.Add(Me._lblUnder_53)
        Me.FraSurface.Controls.Add(Me._lblUnder_52)
        Me.FraSurface.Controls.Add(Me._lblUnder_51)
        Me.FraSurface.Controls.Add(Me._lblUnder_3)
        Me.FraSurface.Controls.Add(Me._lblUnder_33)
        Me.FraSurface.Controls.Add(Me._lblUnder_50)
        Me.FraSurface.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraSurface.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraSurface.Location = New System.Drawing.Point(572, 0)
        Me.FraSurface.Name = "FraSurface"
        Me.FraSurface.Padding = New System.Windows.Forms.Padding(0)
        Me.FraSurface.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraSurface.Size = New System.Drawing.Size(365, 154)
        Me.FraSurface.TabIndex = 172
        Me.FraSurface.TabStop = False
        Me.FraSurface.Text = "Surface Area (DM2)"
        '
        'txtAddSurfaceAreaPLT
        '
        Me.txtAddSurfaceAreaPLT.AcceptsReturn = True
        Me.txtAddSurfaceAreaPLT.BackColor = System.Drawing.SystemColors.Window
        Me.txtAddSurfaceAreaPLT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddSurfaceAreaPLT.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAddSurfaceAreaPLT.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddSurfaceAreaPLT.ForeColor = System.Drawing.Color.Blue
        Me.txtAddSurfaceAreaPLT.Location = New System.Drawing.Point(201, 118)
        Me.txtAddSurfaceAreaPLT.MaxLength = 0
        Me.txtAddSurfaceAreaPLT.Name = "txtAddSurfaceAreaPLT"
        Me.txtAddSurfaceAreaPLT.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAddSurfaceAreaPLT.Size = New System.Drawing.Size(109, 22)
        Me.txtAddSurfaceAreaPLT.TabIndex = 88
        '
        'txtAddSurfaceAreaNPC
        '
        Me.txtAddSurfaceAreaNPC.AcceptsReturn = True
        Me.txtAddSurfaceAreaNPC.BackColor = System.Drawing.SystemColors.Window
        Me.txtAddSurfaceAreaNPC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddSurfaceAreaNPC.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAddSurfaceAreaNPC.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddSurfaceAreaNPC.ForeColor = System.Drawing.Color.Blue
        Me.txtAddSurfaceAreaNPC.Location = New System.Drawing.Point(201, 98)
        Me.txtAddSurfaceAreaNPC.MaxLength = 0
        Me.txtAddSurfaceAreaNPC.Name = "txtAddSurfaceAreaNPC"
        Me.txtAddSurfaceAreaNPC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAddSurfaceAreaNPC.Size = New System.Drawing.Size(109, 22)
        Me.txtAddSurfaceAreaNPC.TabIndex = 87
        '
        'txtAddSurfaceAreaPPS
        '
        Me.txtAddSurfaceAreaPPS.AcceptsReturn = True
        Me.txtAddSurfaceAreaPPS.BackColor = System.Drawing.SystemColors.Window
        Me.txtAddSurfaceAreaPPS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddSurfaceAreaPPS.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAddSurfaceAreaPPS.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddSurfaceAreaPPS.ForeColor = System.Drawing.Color.Blue
        Me.txtAddSurfaceAreaPPS.Location = New System.Drawing.Point(201, 78)
        Me.txtAddSurfaceAreaPPS.MaxLength = 0
        Me.txtAddSurfaceAreaPPS.Name = "txtAddSurfaceAreaPPS"
        Me.txtAddSurfaceAreaPPS.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAddSurfaceAreaPPS.Size = New System.Drawing.Size(109, 22)
        Me.txtAddSurfaceAreaPPS.TabIndex = 86
        '
        'txtSurfaceArea
        '
        Me.txtSurfaceArea.AcceptsReturn = True
        Me.txtSurfaceArea.BackColor = System.Drawing.SystemColors.Window
        Me.txtSurfaceArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSurfaceArea.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSurfaceArea.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSurfaceArea.ForeColor = System.Drawing.Color.Blue
        Me.txtSurfaceArea.Location = New System.Drawing.Point(201, 38)
        Me.txtSurfaceArea.MaxLength = 0
        Me.txtSurfaceArea.Name = "txtSurfaceArea"
        Me.txtSurfaceArea.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSurfaceArea.Size = New System.Drawing.Size(109, 22)
        Me.txtSurfaceArea.TabIndex = 84
        '
        'txtSurfaceAreaInner
        '
        Me.txtSurfaceAreaInner.AcceptsReturn = True
        Me.txtSurfaceAreaInner.BackColor = System.Drawing.SystemColors.Window
        Me.txtSurfaceAreaInner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSurfaceAreaInner.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSurfaceAreaInner.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSurfaceAreaInner.ForeColor = System.Drawing.Color.Blue
        Me.txtSurfaceAreaInner.Location = New System.Drawing.Point(201, 58)
        Me.txtSurfaceAreaInner.MaxLength = 0
        Me.txtSurfaceAreaInner.Name = "txtSurfaceAreaInner"
        Me.txtSurfaceAreaInner.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSurfaceAreaInner.Size = New System.Drawing.Size(109, 22)
        Me.txtSurfaceAreaInner.TabIndex = 85
        '
        '_lblUnder_53
        '
        Me._lblUnder_53.AutoSize = True
        Me._lblUnder_53.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_53.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_53.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_53.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_53, CType(53, Short))
        Me._lblUnder_53.Location = New System.Drawing.Point(15, 118)
        Me._lblUnder_53.Name = "_lblUnder_53"
        Me._lblUnder_53.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_53.Size = New System.Drawing.Size(149, 13)
        Me._lblUnder_53.TabIndex = 180
        Me._lblUnder_53.Text = "Add. Surface Area (Plating) :"
        '
        '_lblUnder_52
        '
        Me._lblUnder_52.AutoSize = True
        Me._lblUnder_52.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_52.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_52.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_52.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_52, CType(52, Short))
        Me._lblUnder_52.Location = New System.Drawing.Point(13, 98)
        Me._lblUnder_52.Name = "_lblUnder_52"
        Me._lblUnder_52.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_52.Size = New System.Drawing.Size(152, 13)
        Me._lblUnder_52.TabIndex = 179
        Me._lblUnder_52.Text = "Add. Surface Area (Powder) :"
        '
        '_lblUnder_51
        '
        Me._lblUnder_51.AutoSize = True
        Me._lblUnder_51.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_51.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_51.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_51.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_51, CType(51, Short))
        Me._lblUnder_51.Location = New System.Drawing.Point(26, 78)
        Me._lblUnder_51.Name = "_lblUnder_51"
        Me._lblUnder_51.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_51.Size = New System.Drawing.Size(139, 13)
        Me._lblUnder_51.TabIndex = 178
        Me._lblUnder_51.Text = "Add. Surface Area (Paint) :"
        '
        '_lblUnder_3
        '
        Me._lblUnder_3.AutoSize = True
        Me._lblUnder_3.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_3, CType(3, Short))
        Me._lblUnder_3.Location = New System.Drawing.Point(67, 18)
        Me._lblUnder_3.Name = "_lblUnder_3"
        Me._lblUnder_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_3.Size = New System.Drawing.Size(104, 13)
        Me._lblUnder_3.TabIndex = 177
        Me._lblUnder_3.Text = "Surface Treatment :"
        '
        '_lblUnder_33
        '
        Me._lblUnder_33.AutoSize = True
        Me._lblUnder_33.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_33.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_33.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_33.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_33, CType(33, Short))
        Me._lblUnder_33.Location = New System.Drawing.Point(38, 38)
        Me._lblUnder_33.Name = "_lblUnder_33"
        Me._lblUnder_33.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_33.Size = New System.Drawing.Size(128, 13)
        Me._lblUnder_33.TabIndex = 176
        Me._lblUnder_33.Text = "Surface Area (External) :"
        '
        '_lblUnder_50
        '
        Me._lblUnder_50.AutoSize = True
        Me._lblUnder_50.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_50.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_50.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_50.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_50, CType(50, Short))
        Me._lblUnder_50.Location = New System.Drawing.Point(41, 58)
        Me._lblUnder_50.Name = "_lblUnder_50"
        Me._lblUnder_50.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_50.Size = New System.Drawing.Size(125, 13)
        Me._lblUnder_50.TabIndex = 175
        Me._lblUnder_50.Text = "Surface Area (Internal) :"
        '
        'FraPress
        '
        Me.FraPress.BackColor = System.Drawing.SystemColors.Control
        Me.FraPress.Controls.Add(Me.cboPressLine)
        Me.FraPress.Controls.Add(Me._lblUnder_61)
        Me.FraPress.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraPress.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraPress.Location = New System.Drawing.Point(454, 159)
        Me.FraPress.Name = "FraPress"
        Me.FraPress.Padding = New System.Windows.Forms.Padding(0)
        Me.FraPress.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraPress.Size = New System.Drawing.Size(291, 48)
        Me.FraPress.TabIndex = 182
        Me.FraPress.TabStop = False
        Me.FraPress.Text = "Press Operation"
        '
        '_lblUnder_61
        '
        Me._lblUnder_61.AutoSize = True
        Me._lblUnder_61.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_61.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_61.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_61.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_61, CType(61, Short))
        Me._lblUnder_61.Location = New System.Drawing.Point(95, 20)
        Me._lblUnder_61.Name = "_lblUnder_61"
        Me._lblUnder_61.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_61.Size = New System.Drawing.Size(62, 13)
        Me._lblUnder_61.TabIndex = 183
        Me._lblUnder_61.Text = "Press Line :"
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me._lblUnder_55)
        Me.Frame1.Controls.Add(Me.txtTacks)
        Me.Frame1.Controls.Add(Me.txtTIGLen)
        Me.Frame1.Controls.Add(Me.txtBrazingLen)
        Me.Frame1.Controls.Add(Me.txtSeamLen)
        Me.Frame1.Controls.Add(Me.txtSpotNos)
        Me.Frame1.Controls.Add(Me._lblUnder_1)
        Me.Frame1.Controls.Add(Me._lblUnder_54)
        Me.Frame1.Controls.Add(Me._lblUnder_56)
        Me.Frame1.Controls.Add(Me._lblUnder_57)
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(328, 2)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(231, 121)
        Me.Frame1.TabIndex = 187
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Weld Len (in Inch)"
        '
        '_lblUnder_55
        '
        Me._lblUnder_55.AutoSize = True
        Me._lblUnder_55.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_55.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_55.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_55.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_55, CType(55, Short))
        Me._lblUnder_55.Location = New System.Drawing.Point(6, 58)
        Me._lblUnder_55.Name = "_lblUnder_55"
        Me._lblUnder_55.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_55.Size = New System.Drawing.Size(118, 13)
        Me._lblUnder_55.TabIndex = 190
        Me._lblUnder_55.Text = "Brazing Weld Length :"
        '
        'txtTacks
        '
        Me.txtTacks.AcceptsReturn = True
        Me.txtTacks.BackColor = System.Drawing.SystemColors.Window
        Me.txtTacks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTacks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTacks.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTacks.ForeColor = System.Drawing.Color.Blue
        Me.txtTacks.Location = New System.Drawing.Point(161, 14)
        Me.txtTacks.MaxLength = 0
        Me.txtTacks.Name = "txtTacks"
        Me.txtTacks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTacks.Size = New System.Drawing.Size(63, 22)
        Me.txtTacks.TabIndex = 78
        '
        'txtTIGLen
        '
        Me.txtTIGLen.AcceptsReturn = True
        Me.txtTIGLen.BackColor = System.Drawing.SystemColors.Window
        Me.txtTIGLen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTIGLen.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTIGLen.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTIGLen.ForeColor = System.Drawing.Color.Blue
        Me.txtTIGLen.Location = New System.Drawing.Point(161, 34)
        Me.txtTIGLen.MaxLength = 0
        Me.txtTIGLen.Name = "txtTIGLen"
        Me.txtTIGLen.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTIGLen.Size = New System.Drawing.Size(63, 22)
        Me.txtTIGLen.TabIndex = 79
        '
        'txtBrazingLen
        '
        Me.txtBrazingLen.AcceptsReturn = True
        Me.txtBrazingLen.BackColor = System.Drawing.SystemColors.Window
        Me.txtBrazingLen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBrazingLen.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBrazingLen.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBrazingLen.ForeColor = System.Drawing.Color.Blue
        Me.txtBrazingLen.Location = New System.Drawing.Point(161, 54)
        Me.txtBrazingLen.MaxLength = 0
        Me.txtBrazingLen.Name = "txtBrazingLen"
        Me.txtBrazingLen.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBrazingLen.Size = New System.Drawing.Size(63, 22)
        Me.txtBrazingLen.TabIndex = 80
        '
        'txtSeamLen
        '
        Me.txtSeamLen.AcceptsReturn = True
        Me.txtSeamLen.BackColor = System.Drawing.SystemColors.Window
        Me.txtSeamLen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSeamLen.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSeamLen.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSeamLen.ForeColor = System.Drawing.Color.Blue
        Me.txtSeamLen.Location = New System.Drawing.Point(161, 74)
        Me.txtSeamLen.MaxLength = 0
        Me.txtSeamLen.Name = "txtSeamLen"
        Me.txtSeamLen.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSeamLen.Size = New System.Drawing.Size(63, 22)
        Me.txtSeamLen.TabIndex = 81
        '
        'txtSpotNos
        '
        Me.txtSpotNos.AcceptsReturn = True
        Me.txtSpotNos.BackColor = System.Drawing.SystemColors.Window
        Me.txtSpotNos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSpotNos.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSpotNos.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSpotNos.ForeColor = System.Drawing.Color.Blue
        Me.txtSpotNos.Location = New System.Drawing.Point(161, 94)
        Me.txtSpotNos.MaxLength = 0
        Me.txtSpotNos.Name = "txtSpotNos"
        Me.txtSpotNos.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSpotNos.Size = New System.Drawing.Size(63, 22)
        Me.txtSpotNos.TabIndex = 82
        '
        '_lblUnder_1
        '
        Me._lblUnder_1.AutoSize = True
        Me._lblUnder_1.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_1, CType(1, Short))
        Me._lblUnder_1.Location = New System.Drawing.Point(62, 16)
        Me._lblUnder_1.Name = "_lblUnder_1"
        Me._lblUnder_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_1.Size = New System.Drawing.Size(72, 13)
        Me._lblUnder_1.TabIndex = 192
        Me._lblUnder_1.Text = "No of Tacks :"
        '
        '_lblUnder_54
        '
        Me._lblUnder_54.AutoSize = True
        Me._lblUnder_54.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_54.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_54.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_54.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_54, CType(54, Short))
        Me._lblUnder_54.Location = New System.Drawing.Point(32, 38)
        Me._lblUnder_54.Name = "_lblUnder_54"
        Me._lblUnder_54.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_54.Size = New System.Drawing.Size(97, 13)
        Me._lblUnder_54.TabIndex = 191
        Me._lblUnder_54.Text = "TIG Weld Length :"
        '
        '_lblUnder_56
        '
        Me._lblUnder_56.AutoSize = True
        Me._lblUnder_56.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_56.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_56.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_56.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_56, CType(56, Short))
        Me._lblUnder_56.Location = New System.Drawing.Point(18, 78)
        Me._lblUnder_56.Name = "_lblUnder_56"
        Me._lblUnder_56.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_56.Size = New System.Drawing.Size(108, 13)
        Me._lblUnder_56.TabIndex = 189
        Me._lblUnder_56.Text = "Seam Weld Length :"
        '
        '_lblUnder_57
        '
        Me._lblUnder_57.AutoSize = True
        Me._lblUnder_57.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_57.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_57.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_57.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_57, CType(57, Short))
        Me._lblUnder_57.Location = New System.Drawing.Point(30, 98)
        Me._lblUnder_57.Name = "_lblUnder_57"
        Me._lblUnder_57.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_57.Size = New System.Drawing.Size(98, 13)
        Me._lblUnder_57.TabIndex = 188
        Me._lblUnder_57.Text = "Spot Weld (Nos) :"
        '
        '_SSTInfo_TabPage3
        '
        Me._SSTInfo_TabPage3.Controls.Add(Me.SprdMain)
        Me._SSTInfo_TabPage3.Location = New System.Drawing.Point(4, 22)
        Me._SSTInfo_TabPage3.Name = "_SSTInfo_TabPage3"
        Me._SSTInfo_TabPage3.Size = New System.Drawing.Size(992, 286)
        Me._SSTInfo_TabPage3.TabIndex = 3
        Me._SSTInfo_TabPage3.Text = "Inventory Parameter"
        Me._SSTInfo_TabPage3.UseVisualStyleBackColor = True
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 4)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(988, 278)
        Me.SprdMain.TabIndex = 0
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.cmdSearchPurUom)
        Me.Frame2.Controls.Add(Me.cmdSearchUom)
        Me.Frame2.Controls.Add(Me.cmdSearchSubCat)
        Me.Frame2.Controls.Add(Me.cmdSearchCategory)
        Me.Frame2.Controls.Add(Me.txtPartNo)
        Me.Frame2.Controls.Add(Me.txtUOMFactor)
        Me.Frame2.Controls.Add(Me.txtPurchaseUom)
        Me.Frame2.Controls.Add(Me.txtItemUom)
        Me.Frame2.Controls.Add(Me.txtSubCatName)
        Me.Frame2.Controls.Add(Me.txtCatName)
        Me.Frame2.Controls.Add(Me.lblPurUom)
        Me.Frame2.Controls.Add(Me.lblItemUom)
        Me.Frame2.Controls.Add(Me._lblUnder_27)
        Me.Frame2.Controls.Add(Me._lblUnder_2)
        Me.Frame2.Controls.Add(Me._lblUnder_4)
        Me.Frame2.Controls.Add(Me._lblUnder_5)
        Me.Frame2.Controls.Add(Me._lblUnder_6)
        Me.Frame2.Controls.Add(Me._lblUnder_0)
        Me.Frame2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 65)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(1002, 70)
        Me.Frame2.TabIndex = 112
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
        Me.txtPartNo.Location = New System.Drawing.Point(828, 40)
        Me.txtPartNo.MaxLength = 0
        Me.txtPartNo.Name = "txtPartNo"
        Me.txtPartNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPartNo.Size = New System.Drawing.Size(140, 22)
        Me.txtPartNo.TabIndex = 9
        '
        'txtUOMFactor
        '
        Me.txtUOMFactor.AcceptsReturn = True
        Me.txtUOMFactor.BackColor = System.Drawing.SystemColors.Window
        Me.txtUOMFactor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUOMFactor.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUOMFactor.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUOMFactor.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtUOMFactor.Location = New System.Drawing.Point(828, 13)
        Me.txtUOMFactor.MaxLength = 7
        Me.txtUOMFactor.Name = "txtUOMFactor"
        Me.txtUOMFactor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUOMFactor.Size = New System.Drawing.Size(140, 22)
        Me.txtUOMFactor.TabIndex = 6
        '
        'txtPurchaseUom
        '
        Me.txtPurchaseUom.AcceptsReturn = True
        Me.txtPurchaseUom.BackColor = System.Drawing.SystemColors.Window
        Me.txtPurchaseUom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPurchaseUom.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPurchaseUom.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPurchaseUom.ForeColor = System.Drawing.Color.Blue
        Me.txtPurchaseUom.Location = New System.Drawing.Point(482, 13)
        Me.txtPurchaseUom.MaxLength = 0
        Me.txtPurchaseUom.Name = "txtPurchaseUom"
        Me.txtPurchaseUom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPurchaseUom.Size = New System.Drawing.Size(46, 22)
        Me.txtPurchaseUom.TabIndex = 3
        '
        'txtItemUom
        '
        Me.txtItemUom.AcceptsReturn = True
        Me.txtItemUom.BackColor = System.Drawing.SystemColors.Window
        Me.txtItemUom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemUom.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItemUom.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemUom.ForeColor = System.Drawing.Color.Blue
        Me.txtItemUom.Location = New System.Drawing.Point(118, 13)
        Me.txtItemUom.MaxLength = 0
        Me.txtItemUom.Name = "txtItemUom"
        Me.txtItemUom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemUom.Size = New System.Drawing.Size(56, 22)
        Me.txtItemUom.TabIndex = 0
        '
        'txtSubCatName
        '
        Me.txtSubCatName.AcceptsReturn = True
        Me.txtSubCatName.BackColor = System.Drawing.SystemColors.Window
        Me.txtSubCatName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSubCatName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSubCatName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubCatName.ForeColor = System.Drawing.Color.Blue
        Me.txtSubCatName.Location = New System.Drawing.Point(482, 40)
        Me.txtSubCatName.MaxLength = 0
        Me.txtSubCatName.Name = "txtSubCatName"
        Me.txtSubCatName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSubCatName.Size = New System.Drawing.Size(236, 22)
        Me.txtSubCatName.TabIndex = 8
        '
        'txtCatName
        '
        Me.txtCatName.AcceptsReturn = True
        Me.txtCatName.BackColor = System.Drawing.SystemColors.Window
        Me.txtCatName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCatName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCatName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCatName.ForeColor = System.Drawing.Color.Blue
        Me.txtCatName.Location = New System.Drawing.Point(118, 40)
        Me.txtCatName.MaxLength = 0
        Me.txtCatName.Name = "txtCatName"
        Me.txtCatName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCatName.Size = New System.Drawing.Size(246, 22)
        Me.txtCatName.TabIndex = 7
        '
        'lblPurUom
        '
        Me.lblPurUom.BackColor = System.Drawing.SystemColors.Control
        Me.lblPurUom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPurUom.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPurUom.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPurUom.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPurUom.Location = New System.Drawing.Point(563, 14)
        Me.lblPurUom.Name = "lblPurUom"
        Me.lblPurUom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPurUom.Size = New System.Drawing.Size(153, 19)
        Me.lblPurUom.TabIndex = 5
        Me.lblPurUom.Text = "lblPurUom"
        '
        'lblItemUom
        '
        Me.lblItemUom.BackColor = System.Drawing.SystemColors.Control
        Me.lblItemUom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblItemUom.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblItemUom.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItemUom.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblItemUom.Location = New System.Drawing.Point(210, 14)
        Me.lblItemUom.Name = "lblItemUom"
        Me.lblItemUom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblItemUom.Size = New System.Drawing.Size(153, 19)
        Me.lblItemUom.TabIndex = 2
        Me.lblItemUom.Text = "lblItemUom"
        '
        '_lblUnder_27
        '
        Me._lblUnder_27.AutoSize = True
        Me._lblUnder_27.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_27.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_27.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_27.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_27, CType(27, Short))
        Me._lblUnder_27.Location = New System.Drawing.Point(769, 44)
        Me._lblUnder_27.Name = "_lblUnder_27"
        Me._lblUnder_27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_27.Size = New System.Drawing.Size(54, 13)
        Me._lblUnder_27.TabIndex = 118
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
        Me._lblUnder_2.Location = New System.Drawing.Point(20, 44)
        Me._lblUnder_2.Name = "_lblUnder_2"
        Me._lblUnder_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_2.Size = New System.Drawing.Size(93, 13)
        Me._lblUnder_2.TabIndex = 117
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
        Me._lblUnder_4.Location = New System.Drawing.Point(48, 18)
        Me._lblUnder_4.Name = "_lblUnder_4"
        Me._lblUnder_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_4.Size = New System.Drawing.Size(65, 13)
        Me._lblUnder_4.TabIndex = 116
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
        Me._lblUnder_5.Location = New System.Drawing.Point(392, 18)
        Me._lblUnder_5.Name = "_lblUnder_5"
        Me._lblUnder_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_5.Size = New System.Drawing.Size(86, 13)
        Me._lblUnder_5.TabIndex = 115
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
        Me._lblUnder_6.Location = New System.Drawing.Point(752, 18)
        Me._lblUnder_6.Name = "_lblUnder_6"
        Me._lblUnder_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_6.Size = New System.Drawing.Size(71, 13)
        Me._lblUnder_6.TabIndex = 114
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
        Me._lblUnder_0.Location = New System.Drawing.Point(396, 44)
        Me._lblUnder_0.Name = "_lblUnder_0"
        Me._lblUnder_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_0.Size = New System.Drawing.Size(82, 13)
        Me._lblUnder_0.TabIndex = 113
        Me._lblUnder_0.Text = "Sub Category :"
        Me._lblUnder_0.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'fraTop1
        '
        Me.fraTop1.BackColor = System.Drawing.SystemColors.Control
        Me.fraTop1.Controls.Add(Me.txtItemCode)
        Me.fraTop1.Controls.Add(Me.txtItemName)
        Me.fraTop1.Controls.Add(Me.txtGUID)
        Me.fraTop1.Controls.Add(Me.txtTechnicalDescription)
        Me.fraTop1.Controls.Add(Me.chkExportItem)
        Me.fraTop1.Controls.Add(Me.Label1)
        Me.fraTop1.Controls.Add(Me._lblUnder_39)
        Me.fraTop1.Controls.Add(Me.Label2)
        Me.fraTop1.Controls.Add(Me.Label4)
        Me.fraTop1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTop1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraTop1.Location = New System.Drawing.Point(3, -2)
        Me.fraTop1.Name = "fraTop1"
        Me.fraTop1.Padding = New System.Windows.Forms.Padding(0)
        Me.fraTop1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraTop1.Size = New System.Drawing.Size(1002, 68)
        Me.fraTop1.TabIndex = 106
        Me.fraTop1.TabStop = False
        '
        'txtItemCode
        '
        Me.txtItemCode.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest
        Me.txtItemCode.AutoSize = False
        Me.txtItemCode.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains
        Appearance1.BackColor = System.Drawing.SystemColors.Window
        Appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption
        Me.txtItemCode.DisplayLayout.Appearance = Appearance1
        Me.txtItemCode.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.txtItemCode.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
        Appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder
        Appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance2.BorderColor = System.Drawing.SystemColors.Window
        Me.txtItemCode.DisplayLayout.GroupByBox.Appearance = Appearance2
        Appearance3.ForeColor = System.Drawing.SystemColors.GrayText
        Me.txtItemCode.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance3
        Me.txtItemCode.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance4.BackColor2 = System.Drawing.SystemColors.Control
        Appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance4.ForeColor = System.Drawing.SystemColors.GrayText
        Me.txtItemCode.DisplayLayout.GroupByBox.PromptAppearance = Appearance4
        Me.txtItemCode.DisplayLayout.MaxColScrollRegions = 1
        Me.txtItemCode.DisplayLayout.MaxRowScrollRegions = 1
        Appearance5.BackColor = System.Drawing.SystemColors.Window
        Appearance5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtItemCode.DisplayLayout.Override.ActiveCellAppearance = Appearance5
        Appearance6.BackColor = System.Drawing.SystemColors.Highlight
        Appearance6.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.txtItemCode.DisplayLayout.Override.ActiveRowAppearance = Appearance6
        Me.txtItemCode.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
        Me.txtItemCode.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
        Appearance7.BackColor = System.Drawing.SystemColors.Window
        Me.txtItemCode.DisplayLayout.Override.CardAreaAppearance = Appearance7
        Appearance8.BorderColor = System.Drawing.Color.Silver
        Appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
        Me.txtItemCode.DisplayLayout.Override.CellAppearance = Appearance8
        Me.txtItemCode.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
        Me.txtItemCode.DisplayLayout.Override.CellPadding = 0
        Appearance9.BackColor = System.Drawing.SystemColors.Control
        Appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
        Appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance9.BorderColor = System.Drawing.SystemColors.Window
        Me.txtItemCode.DisplayLayout.Override.GroupByRowAppearance = Appearance9
        Appearance10.TextHAlignAsString = "Left"
        Me.txtItemCode.DisplayLayout.Override.HeaderAppearance = Appearance10
        Me.txtItemCode.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Me.txtItemCode.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
        Appearance11.BackColor = System.Drawing.SystemColors.Window
        Appearance11.BorderColor = System.Drawing.Color.Silver
        Me.txtItemCode.DisplayLayout.Override.RowAppearance = Appearance11
        Me.txtItemCode.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
        Appearance12.BackColor = System.Drawing.SystemColors.ControlLight
        Me.txtItemCode.DisplayLayout.Override.TemplateAddRowAppearance = Appearance12
        Me.txtItemCode.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.txtItemCode.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.txtItemCode.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
        Me.txtItemCode.Font = New System.Drawing.Font("Verdana", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemCode.Location = New System.Drawing.Point(828, 14)
        Me.txtItemCode.Name = "txtItemCode"
        Me.txtItemCode.Size = New System.Drawing.Size(140, 22)
        Me.txtItemCode.TabIndex = 202
        '
        'txtItemName
        '
        Me.txtItemName.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest
        Me.txtItemName.AutoSize = False
        Me.txtItemName.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains
        Appearance13.BackColor = System.Drawing.SystemColors.Window
        Appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption
        Me.txtItemName.DisplayLayout.Appearance = Appearance13
        Me.txtItemName.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.txtItemName.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
        Appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder
        Appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance14.BorderColor = System.Drawing.SystemColors.Window
        Me.txtItemName.DisplayLayout.GroupByBox.Appearance = Appearance14
        Appearance15.ForeColor = System.Drawing.SystemColors.GrayText
        Me.txtItemName.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance15
        Me.txtItemName.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance16.BackColor2 = System.Drawing.SystemColors.Control
        Appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance16.ForeColor = System.Drawing.SystemColors.GrayText
        Me.txtItemName.DisplayLayout.GroupByBox.PromptAppearance = Appearance16
        Me.txtItemName.DisplayLayout.MaxColScrollRegions = 1
        Me.txtItemName.DisplayLayout.MaxRowScrollRegions = 1
        Appearance17.BackColor = System.Drawing.SystemColors.Window
        Appearance17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtItemName.DisplayLayout.Override.ActiveCellAppearance = Appearance17
        Appearance18.BackColor = System.Drawing.SystemColors.Highlight
        Appearance18.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.txtItemName.DisplayLayout.Override.ActiveRowAppearance = Appearance18
        Me.txtItemName.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
        Me.txtItemName.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
        Appearance19.BackColor = System.Drawing.SystemColors.Window
        Me.txtItemName.DisplayLayout.Override.CardAreaAppearance = Appearance19
        Appearance20.BorderColor = System.Drawing.Color.Silver
        Appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
        Me.txtItemName.DisplayLayout.Override.CellAppearance = Appearance20
        Me.txtItemName.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
        Me.txtItemName.DisplayLayout.Override.CellPadding = 0
        Appearance21.BackColor = System.Drawing.SystemColors.Control
        Appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
        Appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance21.BorderColor = System.Drawing.SystemColors.Window
        Me.txtItemName.DisplayLayout.Override.GroupByRowAppearance = Appearance21
        Appearance22.TextHAlignAsString = "Left"
        Me.txtItemName.DisplayLayout.Override.HeaderAppearance = Appearance22
        Me.txtItemName.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Me.txtItemName.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
        Appearance23.BackColor = System.Drawing.SystemColors.Window
        Appearance23.BorderColor = System.Drawing.Color.Silver
        Me.txtItemName.DisplayLayout.Override.RowAppearance = Appearance23
        Me.txtItemName.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
        Appearance24.BackColor = System.Drawing.SystemColors.ControlLight
        Me.txtItemName.DisplayLayout.Override.TemplateAddRowAppearance = Appearance24
        Me.txtItemName.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.txtItemName.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.txtItemName.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
        Me.txtItemName.Font = New System.Drawing.Font("Verdana", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemName.Location = New System.Drawing.Point(157, 14)
        Me.txtItemName.Name = "txtItemName"
        Me.txtItemName.Size = New System.Drawing.Size(455, 20)
        Me.txtItemName.TabIndex = 201
        '
        'txtGUID
        '
        Me.txtGUID.AcceptsReturn = True
        Me.txtGUID.BackColor = System.Drawing.SystemColors.Window
        Me.txtGUID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGUID.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGUID.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGUID.ForeColor = System.Drawing.Color.Blue
        Me.txtGUID.Location = New System.Drawing.Point(828, 39)
        Me.txtGUID.MaxLength = 0
        Me.txtGUID.Name = "txtGUID"
        Me.txtGUID.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGUID.Size = New System.Drawing.Size(140, 22)
        Me.txtGUID.TabIndex = 5
        '
        'txtTechnicalDescription
        '
        Me.txtTechnicalDescription.AcceptsReturn = True
        Me.txtTechnicalDescription.BackColor = System.Drawing.SystemColors.Window
        Me.txtTechnicalDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTechnicalDescription.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTechnicalDescription.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTechnicalDescription.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTechnicalDescription.Location = New System.Drawing.Point(157, 39)
        Me.txtTechnicalDescription.MaxLength = 7
        Me.txtTechnicalDescription.Name = "txtTechnicalDescription"
        Me.txtTechnicalDescription.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTechnicalDescription.Size = New System.Drawing.Size(367, 22)
        Me.txtTechnicalDescription.TabIndex = 3
        '
        'chkExportItem
        '
        Me.chkExportItem.BackColor = System.Drawing.SystemColors.Control
        Me.chkExportItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkExportItem.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkExportItem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkExportItem.Location = New System.Drawing.Point(531, 39)
        Me.chkExportItem.Name = "chkExportItem"
        Me.chkExportItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkExportItem.Size = New System.Drawing.Size(88, 21)
        Me.chkExportItem.TabIndex = 4
        Me.chkExportItem.Text = "Export Item"
        Me.chkExportItem.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(774, 41)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(40, 13)
        Me.Label1.TabIndex = 200
        Me.Label1.Text = "GUID :"
        '
        '_lblUnder_39
        '
        Me._lblUnder_39.AutoSize = True
        Me._lblUnder_39.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_39.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_39.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_39.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_39, CType(39, Short))
        Me._lblUnder_39.Location = New System.Drawing.Point(22, 40)
        Me._lblUnder_39.Name = "_lblUnder_39"
        Me._lblUnder_39.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_39.Size = New System.Drawing.Size(119, 13)
        Me._lblUnder_39.TabIndex = 151
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
        Me.Label2.Location = New System.Drawing.Point(72, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(69, 13)
        Me.Label2.TabIndex = 107
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
        Me.Label4.Location = New System.Drawing.Point(774, 14)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(40, 13)
        Me.Label4.TabIndex = 108
        Me.Label4.Text = "Code :"
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.txtPurchaseCost)
        Me.Frame3.Controls.Add(Me.txtItemClassQnty)
        Me.Frame3.Controls.Add(Me.txtMaxQnty)
        Me.Frame3.Controls.Add(Me.txtEcoQnty)
        Me.Frame3.Controls.Add(Me.txtMinQnty)
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
        Me.Frame3.Location = New System.Drawing.Point(0, 130)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(1002, 69)
        Me.Frame3.TabIndex = 119
        Me.Frame3.TabStop = False
        '
        'txtPurchaseCost
        '
        Me.txtPurchaseCost.AcceptsReturn = True
        Me.txtPurchaseCost.BackColor = System.Drawing.SystemColors.Window
        Me.txtPurchaseCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPurchaseCost.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPurchaseCost.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPurchaseCost.ForeColor = System.Drawing.Color.Blue
        Me.txtPurchaseCost.Location = New System.Drawing.Point(128, 41)
        Me.txtPurchaseCost.MaxLength = 0
        Me.txtPurchaseCost.Name = "txtPurchaseCost"
        Me.txtPurchaseCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPurchaseCost.Size = New System.Drawing.Size(123, 22)
        Me.txtPurchaseCost.TabIndex = 4
        '
        'txtItemClassQnty
        '
        Me.txtItemClassQnty.AcceptsReturn = True
        Me.txtItemClassQnty.BackColor = System.Drawing.SystemColors.Window
        Me.txtItemClassQnty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemClassQnty.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItemClassQnty.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemClassQnty.ForeColor = System.Drawing.Color.Blue
        Me.txtItemClassQnty.Location = New System.Drawing.Point(368, 13)
        Me.txtItemClassQnty.MaxLength = 0
        Me.txtItemClassQnty.Name = "txtItemClassQnty"
        Me.txtItemClassQnty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemClassQnty.Size = New System.Drawing.Size(123, 22)
        Me.txtItemClassQnty.TabIndex = 1
        '
        'txtMaxQnty
        '
        Me.txtMaxQnty.AcceptsReturn = True
        Me.txtMaxQnty.BackColor = System.Drawing.SystemColors.Window
        Me.txtMaxQnty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMaxQnty.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMaxQnty.Enabled = False
        Me.txtMaxQnty.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMaxQnty.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtMaxQnty.Location = New System.Drawing.Point(611, 41)
        Me.txtMaxQnty.MaxLength = 7
        Me.txtMaxQnty.Name = "txtMaxQnty"
        Me.txtMaxQnty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMaxQnty.Size = New System.Drawing.Size(123, 22)
        Me.txtMaxQnty.TabIndex = 6
        Me.txtMaxQnty.Visible = False
        '
        'txtEcoQnty
        '
        Me.txtEcoQnty.AcceptsReturn = True
        Me.txtEcoQnty.BackColor = System.Drawing.SystemColors.Window
        Me.txtEcoQnty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEcoQnty.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEcoQnty.Enabled = False
        Me.txtEcoQnty.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEcoQnty.ForeColor = System.Drawing.Color.Blue
        Me.txtEcoQnty.Location = New System.Drawing.Point(857, 13)
        Me.txtEcoQnty.MaxLength = 0
        Me.txtEcoQnty.Name = "txtEcoQnty"
        Me.txtEcoQnty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEcoQnty.Size = New System.Drawing.Size(123, 22)
        Me.txtEcoQnty.TabIndex = 3
        Me.txtEcoQnty.Visible = False
        '
        'txtMinQnty
        '
        Me.txtMinQnty.AcceptsReturn = True
        Me.txtMinQnty.BackColor = System.Drawing.SystemColors.Window
        Me.txtMinQnty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMinQnty.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMinQnty.Enabled = False
        Me.txtMinQnty.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMinQnty.ForeColor = System.Drawing.Color.Blue
        Me.txtMinQnty.Location = New System.Drawing.Point(611, 13)
        Me.txtMinQnty.MaxLength = 0
        Me.txtMinQnty.Name = "txtMinQnty"
        Me.txtMinQnty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMinQnty.Size = New System.Drawing.Size(123, 22)
        Me.txtMinQnty.TabIndex = 2
        Me.txtMinQnty.Visible = False
        '
        'txtLeadTime
        '
        Me.txtLeadTime.AcceptsReturn = True
        Me.txtLeadTime.BackColor = System.Drawing.SystemColors.Window
        Me.txtLeadTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLeadTime.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLeadTime.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLeadTime.ForeColor = System.Drawing.Color.Blue
        Me.txtLeadTime.Location = New System.Drawing.Point(128, 13)
        Me.txtLeadTime.MaxLength = 0
        Me.txtLeadTime.Name = "txtLeadTime"
        Me.txtLeadTime.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLeadTime.Size = New System.Drawing.Size(123, 22)
        Me.txtLeadTime.TabIndex = 0
        '
        'txtReQnty
        '
        Me.txtReQnty.AcceptsReturn = True
        Me.txtReQnty.BackColor = System.Drawing.SystemColors.Window
        Me.txtReQnty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReQnty.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtReQnty.Enabled = False
        Me.txtReQnty.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReQnty.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtReQnty.Location = New System.Drawing.Point(858, 41)
        Me.txtReQnty.MaxLength = 7
        Me.txtReQnty.Name = "txtReQnty"
        Me.txtReQnty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtReQnty.Size = New System.Drawing.Size(122, 22)
        Me.txtReQnty.TabIndex = 7
        Me.txtReQnty.Visible = False
        '
        'txtSaleCost
        '
        Me.txtSaleCost.AcceptsReturn = True
        Me.txtSaleCost.BackColor = System.Drawing.SystemColors.Window
        Me.txtSaleCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSaleCost.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSaleCost.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSaleCost.ForeColor = System.Drawing.Color.Blue
        Me.txtSaleCost.Location = New System.Drawing.Point(368, 41)
        Me.txtSaleCost.MaxLength = 0
        Me.txtSaleCost.Name = "txtSaleCost"
        Me.txtSaleCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSaleCost.Size = New System.Drawing.Size(123, 22)
        Me.txtSaleCost.TabIndex = 5
        '
        '_lblUnder_22
        '
        Me._lblUnder_22.AutoSize = True
        Me._lblUnder_22.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_22.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_22.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_22, CType(22, Short))
        Me._lblUnder_22.Location = New System.Drawing.Point(277, 17)
        Me._lblUnder_22.Name = "_lblUnder_22"
        Me._lblUnder_22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_22.Size = New System.Drawing.Size(86, 13)
        Me._lblUnder_22.TabIndex = 127
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
        Me._lblUnder_7.Location = New System.Drawing.Point(59, 17)
        Me._lblUnder_7.Name = "_lblUnder_7"
        Me._lblUnder_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_7.Size = New System.Drawing.Size(65, 13)
        Me._lblUnder_7.TabIndex = 126
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
        Me._lblUnder_8.Location = New System.Drawing.Point(41, 45)
        Me._lblUnder_8.Name = "_lblUnder_8"
        Me._lblUnder_8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_8.Size = New System.Drawing.Size(83, 13)
        Me._lblUnder_8.TabIndex = 125
        Me._lblUnder_8.Text = "Purchase Cost :"
        Me._lblUnder_8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_9
        '
        Me._lblUnder_9.AutoSize = True
        Me._lblUnder_9.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_9.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_9.Enabled = False
        Me._lblUnder_9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_9, CType(9, Short))
        Me._lblUnder_9.Location = New System.Drawing.Point(524, 17)
        Me._lblUnder_9.Name = "_lblUnder_9"
        Me._lblUnder_9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_9.Size = New System.Drawing.Size(82, 13)
        Me._lblUnder_9.TabIndex = 124
        Me._lblUnder_9.Text = "Minimum Qty :"
        Me._lblUnder_9.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me._lblUnder_9.Visible = False
        '
        '_lblUnder_11
        '
        Me._lblUnder_11.AutoSize = True
        Me._lblUnder_11.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_11.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_11.Enabled = False
        Me._lblUnder_11.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_11, CType(11, Short))
        Me._lblUnder_11.Location = New System.Drawing.Point(761, 17)
        Me._lblUnder_11.Name = "_lblUnder_11"
        Me._lblUnder_11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_11.Size = New System.Drawing.Size(89, 13)
        Me._lblUnder_11.TabIndex = 123
        Me._lblUnder_11.Text = "Inventory Days :"
        Me._lblUnder_11.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me._lblUnder_11.Visible = False
        '
        '_lblUnder_14
        '
        Me._lblUnder_14.AutoSize = True
        Me._lblUnder_14.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_14.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_14.Enabled = False
        Me._lblUnder_14.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_14, CType(14, Short))
        Me._lblUnder_14.Location = New System.Drawing.Point(521, 45)
        Me._lblUnder_14.Name = "_lblUnder_14"
        Me._lblUnder_14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_14.Size = New System.Drawing.Size(85, 13)
        Me._lblUnder_14.TabIndex = 122
        Me._lblUnder_14.Text = "Maximum Qty :"
        Me._lblUnder_14.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me._lblUnder_14.Visible = False
        '
        '_lblUnder_15
        '
        Me._lblUnder_15.AutoSize = True
        Me._lblUnder_15.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_15.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_15.Enabled = False
        Me._lblUnder_15.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_15, CType(15, Short))
        Me._lblUnder_15.Location = New System.Drawing.Point(775, 45)
        Me._lblUnder_15.Name = "_lblUnder_15"
        Me._lblUnder_15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_15.Size = New System.Drawing.Size(75, 13)
        Me._lblUnder_15.TabIndex = 121
        Me._lblUnder_15.Text = "Reorder Qty :"
        Me._lblUnder_15.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me._lblUnder_15.Visible = False
        '
        '_lblUnder_21
        '
        Me._lblUnder_21.AutoSize = True
        Me._lblUnder_21.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_21.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_21.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_21, CType(21, Short))
        Me._lblUnder_21.Location = New System.Drawing.Point(303, 45)
        Me._lblUnder_21.Name = "_lblUnder_21"
        Me._lblUnder_21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_21.Size = New System.Drawing.Size(60, 13)
        Me._lblUnder_21.TabIndex = 120
        Me._lblUnder_21.Text = "Sale Cost :"
        Me._lblUnder_21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.txtOldPartNo)
        Me.Frame4.Controls.Add(Me.Label5)
        Me.Frame4.Controls.Add(Me.cmdSearchJWUom)
        Me.Frame4.Controls.Add(Me.txtJWUOM)
        Me.Frame4.Controls.Add(Me.Label6)
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
        Me.Frame4.Location = New System.Drawing.Point(0, 193)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(1002, 48)
        Me.Frame4.TabIndex = 128
        Me.Frame4.TabStop = False
        '
        'txtOldPartNo
        '
        Me.txtOldPartNo.AcceptsReturn = True
        Me.txtOldPartNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtOldPartNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOldPartNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOldPartNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOldPartNo.ForeColor = System.Drawing.Color.Blue
        Me.txtOldPartNo.Location = New System.Drawing.Point(883, 16)
        Me.txtOldPartNo.MaxLength = 0
        Me.txtOldPartNo.Name = "txtOldPartNo"
        Me.txtOldPartNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOldPartNo.Size = New System.Drawing.Size(113, 22)
        Me.txtOldPartNo.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(807, 20)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(75, 13)
        Me.Label5.TabIndex = 138
        Me.Label5.Text = "Old Part No. :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtJWUOM
        '
        Me.txtJWUOM.AcceptsReturn = True
        Me.txtJWUOM.BackColor = System.Drawing.SystemColors.Window
        Me.txtJWUOM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtJWUOM.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtJWUOM.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtJWUOM.ForeColor = System.Drawing.Color.Blue
        Me.txtJWUOM.Location = New System.Drawing.Point(727, 16)
        Me.txtJWUOM.MaxLength = 0
        Me.txtJWUOM.Name = "txtJWUOM"
        Me.txtJWUOM.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtJWUOM.Size = New System.Drawing.Size(46, 22)
        Me.txtJWUOM.TabIndex = 4
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(637, 19)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(88, 13)
        Me.Label6.TabIndex = 136
        Me.Label6.Text = "J/W Rate UOM :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'CboExciseFlag
        '
        Me.CboExciseFlag.BackColor = System.Drawing.SystemColors.Window
        Me.CboExciseFlag.Cursor = System.Windows.Forms.Cursors.Default
        Me.CboExciseFlag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboExciseFlag.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboExciseFlag.ForeColor = System.Drawing.SystemColors.WindowText
        Me.CboExciseFlag.Location = New System.Drawing.Point(375, 15)
        Me.CboExciseFlag.Name = "CboExciseFlag"
        Me.CboExciseFlag.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CboExciseFlag.Size = New System.Drawing.Size(81, 21)
        Me.CboExciseFlag.TabIndex = 2
        '
        'cboItemClassification
        '
        Me.cboItemClassification.BackColor = System.Drawing.SystemColors.Window
        Me.cboItemClassification.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboItemClassification.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboItemClassification.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboItemClassification.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboItemClassification.Location = New System.Drawing.Point(548, 15)
        Me.cboItemClassification.Name = "cboItemClassification"
        Me.cboItemClassification.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboItemClassification.Size = New System.Drawing.Size(85, 21)
        Me.cboItemClassification.TabIndex = 3
        '
        'CboItemClass
        '
        Me.CboItemClass.BackColor = System.Drawing.SystemColors.Window
        Me.CboItemClass.Cursor = System.Windows.Forms.Cursors.Default
        Me.CboItemClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboItemClass.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboItemClass.ForeColor = System.Drawing.SystemColors.WindowText
        Me.CboItemClass.Location = New System.Drawing.Point(221, 15)
        Me.CboItemClass.Name = "CboItemClass"
        Me.CboItemClass.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CboItemClass.Size = New System.Drawing.Size(75, 21)
        Me.CboItemClass.TabIndex = 1
        '
        '_lblUnder_16
        '
        Me._lblUnder_16.AutoSize = True
        Me._lblUnder_16.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_16.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_16.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_16, CType(16, Short))
        Me._lblUnder_16.Location = New System.Drawing.Point(9, 19)
        Me._lblUnder_16.Name = "_lblUnder_16"
        Me._lblUnder_16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_16.Size = New System.Drawing.Size(63, 13)
        Me._lblUnder_16.TabIndex = 132
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
        Me._lblUnder_17.Location = New System.Drawing.Point(154, 19)
        Me._lblUnder_17.Name = "_lblUnder_17"
        Me._lblUnder_17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_17.Size = New System.Drawing.Size(65, 13)
        Me._lblUnder_17.TabIndex = 131
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
        Me._lblUnder_18.Location = New System.Drawing.Point(303, 19)
        Me._lblUnder_18.Name = "_lblUnder_18"
        Me._lblUnder_18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_18.Size = New System.Drawing.Size(69, 13)
        Me._lblUnder_18.TabIndex = 130
        Me._lblUnder_18.Text = "Excise Flag :"
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
        Me._lblUnder_19.Location = New System.Drawing.Point(467, 19)
        Me._lblUnder_19.Name = "_lblUnder_19"
        Me._lblUnder_19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_19.Size = New System.Drawing.Size(80, 13)
        Me._lblUnder_19.TabIndex = 129
        Me._lblUnder_19.Text = "Classification :"
        Me._lblUnder_19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(0, 0)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 107
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
        Me.FraMovement.Controls.Add(Me.lblMasterType)
        Me.FraMovement.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 557)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(1007, 56)
        Me.FraMovement.TabIndex = 109
        Me.FraMovement.TabStop = False
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
        Me.lblMasterType.TabIndex = 110
        Me.lblMasterType.Text = "lblMasterType"
        Me.lblMasterType.Visible = False
        '
        'UltraGrid1
        '
        Appearance25.BackColor = System.Drawing.SystemColors.Window
        Appearance25.BorderColor = System.Drawing.Color.White
        Me.UltraGrid1.DisplayLayout.Appearance = Appearance25
        Me.UltraGrid1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.UltraGrid1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
        Appearance26.BackColor = System.Drawing.Color.White
        Appearance26.BackColor2 = System.Drawing.Color.White
        Appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance26.BorderColor = System.Drawing.SystemColors.Window
        Me.UltraGrid1.DisplayLayout.GroupByBox.Appearance = Appearance26
        Appearance27.ForeColor = System.Drawing.SystemColors.GrayText
        Me.UltraGrid1.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance27
        Me.UltraGrid1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.UltraGrid1.DisplayLayout.GroupByBox.Hidden = True
        Appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance28.BackColor2 = System.Drawing.SystemColors.Control
        Appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance28.ForeColor = System.Drawing.SystemColors.GrayText
        Me.UltraGrid1.DisplayLayout.GroupByBox.PromptAppearance = Appearance28
        Me.UltraGrid1.DisplayLayout.MaxColScrollRegions = 1
        Me.UltraGrid1.DisplayLayout.MaxRowScrollRegions = 1
        Appearance29.BackColor = System.Drawing.SystemColors.Window
        Appearance29.ForeColor = System.Drawing.SystemColors.ControlText
        Me.UltraGrid1.DisplayLayout.Override.ActiveCellAppearance = Appearance29
        Appearance30.BackColor = System.Drawing.SystemColors.Highlight
        Appearance30.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.UltraGrid1.DisplayLayout.Override.ActiveRowAppearance = Appearance30
        Me.UltraGrid1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
        Me.UltraGrid1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
        Appearance31.BackColor = System.Drawing.SystemColors.Window
        Me.UltraGrid1.DisplayLayout.Override.CardAreaAppearance = Appearance31
        Appearance32.BorderColor = System.Drawing.Color.Silver
        Appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
        Me.UltraGrid1.DisplayLayout.Override.CellAppearance = Appearance32
        Me.UltraGrid1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
        Me.UltraGrid1.DisplayLayout.Override.CellPadding = 0
        Appearance33.BackColor = System.Drawing.SystemColors.Control
        Appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
        Appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance33.BorderColor = System.Drawing.SystemColors.Window
        Me.UltraGrid1.DisplayLayout.Override.GroupByRowAppearance = Appearance33
        Appearance34.TextHAlignAsString = "Left"
        Me.UltraGrid1.DisplayLayout.Override.HeaderAppearance = Appearance34
        Me.UltraGrid1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Me.UltraGrid1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
        Appearance35.BackColor = System.Drawing.SystemColors.Window
        Appearance35.BorderColor = System.Drawing.Color.Silver
        Me.UltraGrid1.DisplayLayout.Override.RowAppearance = Appearance35
        Me.UltraGrid1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
        Appearance36.BackColor = System.Drawing.SystemColors.ControlLight
        Me.UltraGrid1.DisplayLayout.Override.TemplateAddRowAppearance = Appearance36
        Me.UltraGrid1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.UltraGrid1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.UltraGrid1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraGrid1.Location = New System.Drawing.Point(0, 6)
        Me.UltraGrid1.Name = "UltraGrid1"
        Me.UltraGrid1.Size = New System.Drawing.Size(1003, 545)
        Me.UltraGrid1.TabIndex = 110
        '
        'frmItemMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1006, 613)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.FraTrn)
        Me.Controls.Add(Me.Report1)
        Me.Controls.Add(Me.UltraGrid1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmItemMaster"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Item Master"
        Me.FraTrn.ResumeLayout(False)
        Me.SSTInfo.ResumeLayout(False)
        Me._SSTInfo_TabPage0.ResumeLayout(False)
        Me.FraView.ResumeLayout(False)
        Me.FraView.PerformLayout()
        Me.FraLock.ResumeLayout(False)
        Me.FraLock.PerformLayout()
        Me.FraChecks.ResumeLayout(False)
        Me.FraChecks.PerformLayout()
        Me._SSTInfo_TabPage1.ResumeLayout(False)
        Me._SSTInfo_TabPage1.PerformLayout()
        Me._SSTInfo_TabPage2.ResumeLayout(False)
        Me.FraWeld.ResumeLayout(False)
        Me.FraWeld.PerformLayout()
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me.Frame5.ResumeLayout(False)
        Me.Frame5.PerformLayout()
        Me.FraSurface.ResumeLayout(False)
        Me.FraSurface.PerformLayout()
        Me.FraPress.ResumeLayout(False)
        Me.FraPress.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me._SSTInfo_TabPage3.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.fraTop1.ResumeLayout(False)
        Me.fraTop1.PerformLayout()
        CType(Me.txtItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtItemName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.lblUnder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        'SprdView.DataSource = CType(ADataGrid, msdatasrc.DataSource)
    End Sub
    'Public Sub VB6_RemoveADODataBinding()
    '    SprdView.DataSource = Nothing
    'End Sub
    Public WithEvents cmdSearchScrap As Button
    Public WithEvents cmdSearchPIC As Button
    Public WithEvents cmdSearchSubCat As Button
    Public WithEvents cmdSearchCategory As Button
    Public WithEvents cmdSearchHSN As Button
    Public WithEvents cmdSearchPackType As Button
    Public WithEvents txtPackType As TextBox
    Public WithEvents Label3 As Label
    Public WithEvents cmdSearchPurUom As Button
    Public WithEvents cmdSearchUom As Button
    Public WithEvents cmdSearchJWUom As Button
    Public WithEvents txtJWUOM As TextBox
    Public WithEvents Label6 As Label
    Public WithEvents chkAutoQC As CheckBox
    Public WithEvents chkHeatReq As CheckBox
    Public WithEvents txtOldPartNo As TextBox
    Public WithEvents Label5 As Label
    Friend WithEvents _SSTInfo_TabPage3 As TabPage
    Friend WithEvents txtItemCode As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents txtItemName As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents UltraGrid1 As Infragistics.Win.UltraWinGrid.UltraGrid
#End Region
End Class