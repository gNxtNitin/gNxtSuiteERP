Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmAcmRequisition
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
    Public WithEvents txtRemarks As System.Windows.Forms.TextBox
    Public WithEvents _OptStatus_1 As System.Windows.Forms.RadioButton
    Public WithEvents _OptStatus_0 As System.Windows.Forms.RadioButton
    Public WithEvents FraStatus As System.Windows.Forms.GroupBox
    Public WithEvents _optBalMethod_0 As System.Windows.Forms.RadioButton
    Public WithEvents _optBalMethod_1 As System.Windows.Forms.RadioButton
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents txtName As System.Windows.Forms.TextBox
    Public WithEvents txtCode As System.Windows.Forms.TextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents fraTop1 As System.Windows.Forms.GroupBox
    Public WithEvents txtEmpCode As System.Windows.Forms.TextBox
    Public WithEvents txtCurrency As System.Windows.Forms.TextBox
    Public WithEvents txtGroupNameCr As System.Windows.Forms.TextBox
    Public WithEvents cboSupplierType As System.Windows.Forms.ComboBox
    Public WithEvents cboHeadType As System.Windows.Forms.ComboBox
    Public WithEvents cboCategory As System.Windows.Forms.ComboBox
    Public WithEvents txtGroupName As System.Windows.Forms.TextBox
    Public WithEvents Label26 As System.Windows.Forms.Label
    Public WithEvents Label24 As System.Windows.Forms.Label
    Public WithEvents Label23 As System.Windows.Forms.Label
    Public WithEvents Label21 As System.Windows.Forms.Label
    Public WithEvents lblHeadType As System.Windows.Forms.Label
    Public WithEvents LblCategory As System.Windows.Forms.Label
    Public WithEvents lblUnder As System.Windows.Forms.Label
    Public WithEvents FraAcctType As System.Windows.Forms.GroupBox
    Public WithEvents cboPaymentMode As System.Windows.Forms.ComboBox
    Public WithEvents txtChqFrequency As System.Windows.Forms.TextBox
    Public WithEvents txtPayment As System.Windows.Forms.TextBox
    Public WithEvents chkMonthWiseLdgr As System.Windows.Forms.CheckBox
    Public WithEvents txtVendorCode As System.Windows.Forms.TextBox
    Public WithEvents txtPaidDay4 As System.Windows.Forms.TextBox
    Public WithEvents txtPaidDay3 As System.Windows.Forms.TextBox
    Public WithEvents txtPaidDay2 As System.Windows.Forms.TextBox
    Public WithEvents txtPaidDay As System.Windows.Forms.TextBox
    Public WithEvents ChkPoRate As System.Windows.Forms.CheckBox
    Public WithEvents txtSeq As System.Windows.Forms.TextBox
    Public WithEvents txtAlias As System.Windows.Forms.TextBox
    Public WithEvents Label50 As System.Windows.Forms.Label
    Public WithEvents Label49 As System.Windows.Forms.Label
    Public WithEvents Label47 As System.Windows.Forms.Label
    Public WithEvents lblPaymentTerms As System.Windows.Forms.Label
    Public WithEvents Label43 As System.Windows.Forms.Label
    Public WithEvents Label41 As System.Windows.Forms.Label
    Public WithEvents Label40 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents Label22 As System.Windows.Forms.Label
    Public WithEvents Label31 As System.Windows.Forms.Label
    Public WithEvents Frame8 As System.Windows.Forms.GroupBox
    Public WithEvents chkStopGP As System.Windows.Forms.CheckBox
    Public WithEvents chkStopMRR As System.Windows.Forms.CheckBox
    Public WithEvents chkStopInvoice As System.Windows.Forms.CheckBox
    Public WithEvents chkStopBP As System.Windows.Forms.CheckBox
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents chkAuthorised As System.Windows.Forms.CheckBox
    Public WithEvents chkInterUnit As System.Windows.Forms.CheckBox
    Public WithEvents chkDistt As System.Windows.Forms.CheckBox
    Public WithEvents chkState As System.Windows.Forms.CheckBox
    Public WithEvents chkCountry As System.Windows.Forms.CheckBox
    Public WithEvents Frame5 As System.Windows.Forms.GroupBox
    Public WithEvents txtDistance As System.Windows.Forms.TextBox
    Public WithEvents txtaddress As System.Windows.Forms.TextBox
    Public WithEvents txtState As System.Windows.Forms.TextBox
    Public WithEvents txtPinCode As System.Windows.Forms.TextBox
    Public WithEvents txtCity As System.Windows.Forms.TextBox
    Public WithEvents txtCountry As System.Windows.Forms.TextBox
    Public WithEvents Label51 As System.Windows.Forms.Label
    Public WithEvents Label56 As System.Windows.Forms.Label
    Public WithEvents lblRegularized As System.Windows.Forms.Label
    Public WithEvents lblModDate As System.Windows.Forms.Label
    Public WithEvents Label48 As System.Windows.Forms.Label
    Public WithEvents lblAddDate As System.Windows.Forms.Label
    Public WithEvents Label45 As System.Windows.Forms.Label
    Public WithEvents lblModUser As System.Windows.Forms.Label
    Public WithEvents Label46 As System.Windows.Forms.Label
    Public WithEvents lblAddUser As System.Windows.Forms.Label
    Public WithEvents Label44 As System.Windows.Forms.Label
    Public WithEvents _Label25_1 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label27 As System.Windows.Forms.Label
    Public WithEvents Label39 As System.Windows.Forms.Label
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents _SSTInfo_TabPage0 As System.Windows.Forms.TabPage
    Public WithEvents txtContact As System.Windows.Forms.TextBox
    Public WithEvents txtFax As System.Windows.Forms.TextBox
    Public WithEvents txtMobile As System.Windows.Forms.TextBox
    Public WithEvents txtEmail As System.Windows.Forms.TextBox
    Public WithEvents txtPhone As System.Windows.Forms.TextBox
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents _Label25_0 As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents _SSTInfo_TabPage1 As System.Windows.Forms.TabPage
    Public WithEvents txtServProvided As System.Windows.Forms.TextBox
    Public WithEvents txtSrvRegnNo As System.Windows.Forms.TextBox
    Public WithEvents txtTINNo As System.Windows.Forms.TextBox
    Public WithEvents txtPan As System.Windows.Forms.TextBox
    Public WithEvents txtCstNo As System.Windows.Forms.TextBox
    Public WithEvents txtLSTNo As System.Windows.Forms.TextBox
    Public WithEvents Label53 As System.Windows.Forms.Label
    Public WithEvents Label32 As System.Windows.Forms.Label
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents _SSTInfo_TabPage2 As System.Windows.Forms.TabPage
    Public WithEvents _optRegd_0 As System.Windows.Forms.RadioButton
    Public WithEvents _optRegd_1 As System.Windows.Forms.RadioButton
    Public WithEvents Frame13 As System.Windows.Forms.GroupBox
    Public WithEvents txtDivision As System.Windows.Forms.TextBox
    Public WithEvents txtRange As System.Windows.Forms.TextBox
    Public WithEvents txtCommRate As System.Windows.Forms.TextBox
    Public WithEvents txtECCNo As System.Windows.Forms.TextBox
    Public WithEvents txtRegnNo As System.Windows.Forms.TextBox
    Public WithEvents _Label25_2 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Frame7 As System.Windows.Forms.GroupBox
    Public WithEvents _SSTInfo_TabPage3 As System.Windows.Forms.TabPage
    Public WithEvents chkLowerDeduction As System.Windows.Forms.CheckBox
    Public WithEvents txtLDCertiNo As System.Windows.Forms.TextBox
    Public WithEvents txtExptionCNo As System.Windows.Forms.TextBox
    Public WithEvents cboCType As System.Windows.Forms.ComboBox
    Public WithEvents txtESIPer As System.Windows.Forms.TextBox
    Public WithEvents txtSTDSPer As System.Windows.Forms.TextBox
    Public WithEvents CboTDSCategory As System.Windows.Forms.ComboBox
    Public WithEvents txtTDSPer As System.Windows.Forms.TextBox
    Public WithEvents txtSection As System.Windows.Forms.TextBox
    Public WithEvents _lblLabels_0 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_4 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_5 As System.Windows.Forms.Label
    Public WithEvents Label28 As System.Windows.Forms.Label
    Public WithEvents Label20 As System.Windows.Forms.Label
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents LblPayTerms As System.Windows.Forms.Label
    Public WithEvents lblDueDays As System.Windows.Forms.Label
    Public WithEvents Frame12 As System.Windows.Forms.GroupBox
    Public WithEvents _SSTInfo_TabPage4 As System.Windows.Forms.TabPage
    Public WithEvents txtPurchaseSTRecd As System.Windows.Forms.TextBox
    Public WithEvents txtPurchaseSTDue As System.Windows.Forms.TextBox
    Public WithEvents Label34 As System.Windows.Forms.Label
    Public WithEvents Label33 As System.Windows.Forms.Label
    Public WithEvents Frame9 As System.Windows.Forms.GroupBox
    Public WithEvents txtSaleSTDue As System.Windows.Forms.TextBox
    Public WithEvents txtSaleSTRecd As System.Windows.Forms.TextBox
    Public WithEvents Label30 As System.Windows.Forms.Label
    Public WithEvents Label29 As System.Windows.Forms.Label
    Public WithEvents Frame10 As System.Windows.Forms.GroupBox
    Public WithEvents _SSTInfo_TabPage5 As System.Windows.Forms.TabPage
    Public WithEvents txtExportPaymetTerms As System.Windows.Forms.TextBox
    Public WithEvents txtFinalDest As System.Windows.Forms.TextBox
    Public WithEvents txtCarriage As System.Windows.Forms.TextBox
    Public WithEvents txtDischargePort As System.Windows.Forms.TextBox
    Public WithEvents txtLoadingPort As System.Windows.Forms.TextBox
    Public WithEvents txtBuyerName As System.Windows.Forms.TextBox
    Public WithEvents Label36 As System.Windows.Forms.Label
    Public WithEvents _Label25_4 As System.Windows.Forms.Label
    Public WithEvents Label38 As System.Windows.Forms.Label
    Public WithEvents Label37 As System.Windows.Forms.Label
    Public WithEvents _Label25_3 As System.Windows.Forms.Label
    Public WithEvents Label35 As System.Windows.Forms.Label
    Public WithEvents Frame11 As System.Windows.Forms.GroupBox
    Public WithEvents _SSTInfo_TabPage6 As System.Windows.Forms.TabPage
    Public WithEvents txtBankName As System.Windows.Forms.TextBox
    Public WithEvents txtSwitCode As System.Windows.Forms.TextBox
    Public WithEvents txtIFSCCode As System.Windows.Forms.TextBox
    Public WithEvents txtBankBranch As System.Windows.Forms.TextBox
    Public WithEvents txtBankAccountNo As System.Windows.Forms.TextBox
    Public WithEvents _Label25_5 As System.Windows.Forms.Label
    Public WithEvents Label52 As System.Windows.Forms.Label
    Public WithEvents Label54 As System.Windows.Forms.Label
    Public WithEvents _Label25_6 As System.Windows.Forms.Label
    Public WithEvents Label55 As System.Windows.Forms.Label
    Public WithEvents Frame14 As System.Windows.Forms.GroupBox
    Public WithEvents _SSTInfo_TabPage7 As System.Windows.Forms.TabPage
    Public WithEvents _optGSTClassification_0 As System.Windows.Forms.RadioButton
    Public WithEvents _optGSTClassification_1 As System.Windows.Forms.RadioButton
    Public WithEvents FraGSTClass As System.Windows.Forms.GroupBox
    Public WithEvents _optGSTRegd_0 As System.Windows.Forms.RadioButton
    Public WithEvents _optGSTRegd_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optGSTRegd_2 As System.Windows.Forms.RadioButton
    Public WithEvents _optGSTRegd_3 As System.Windows.Forms.RadioButton
    Public WithEvents _optGSTRegd_4 As System.Windows.Forms.RadioButton
    Public WithEvents FraGSTStatus As System.Windows.Forms.GroupBox
    Public WithEvents txtGSTRegnNo As System.Windows.Forms.TextBox
    Public WithEvents Label58 As System.Windows.Forms.Label
    Public WithEvents Frame15 As System.Windows.Forms.GroupBox
    Public WithEvents _SSTInfo_TabPage8 As System.Windows.Forms.TabPage
    Public WithEvents SSTInfo As System.Windows.Forms.TabControl
    Public WithEvents FraView As System.Windows.Forms.GroupBox
    Public WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents FraTrn As System.Windows.Forms.GroupBox
    Public WithEvents ADataGrid As VB6.ADODC
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
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
    Public WithEvents Label25 As VB6.LabelArray
    Public WithEvents OptStatus As VB6.RadioButtonArray
    Public WithEvents lblLabels As VB6.LabelArray
    Public WithEvents optBalMethod As VB6.RadioButtonArray
    Public WithEvents optGSTClassification As VB6.RadioButtonArray
    Public WithEvents optGSTRegd As VB6.RadioButtonArray
    Public WithEvents optRegd As VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAcmRequisition))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
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
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.FraStatus = New System.Windows.Forms.GroupBox()
        Me._OptStatus_1 = New System.Windows.Forms.RadioButton()
        Me._OptStatus_0 = New System.Windows.Forms.RadioButton()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me._optBalMethod_0 = New System.Windows.Forms.RadioButton()
        Me._optBalMethod_1 = New System.Windows.Forms.RadioButton()
        Me.fraTop1 = New System.Windows.Forms.GroupBox()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.txtCode = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.FraAcctType = New System.Windows.Forms.GroupBox()
        Me.txtEmpCode = New System.Windows.Forms.TextBox()
        Me.txtCurrency = New System.Windows.Forms.TextBox()
        Me.txtGroupNameCr = New System.Windows.Forms.TextBox()
        Me.cboSupplierType = New System.Windows.Forms.ComboBox()
        Me.cboHeadType = New System.Windows.Forms.ComboBox()
        Me.cboCategory = New System.Windows.Forms.ComboBox()
        Me.txtGroupName = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.lblHeadType = New System.Windows.Forms.Label()
        Me.LblCategory = New System.Windows.Forms.Label()
        Me.lblUnder = New System.Windows.Forms.Label()
        Me.Frame8 = New System.Windows.Forms.GroupBox()
        Me.cboPaymentMode = New System.Windows.Forms.ComboBox()
        Me.txtChqFrequency = New System.Windows.Forms.TextBox()
        Me.txtPayment = New System.Windows.Forms.TextBox()
        Me.chkMonthWiseLdgr = New System.Windows.Forms.CheckBox()
        Me.txtVendorCode = New System.Windows.Forms.TextBox()
        Me.txtPaidDay4 = New System.Windows.Forms.TextBox()
        Me.txtPaidDay3 = New System.Windows.Forms.TextBox()
        Me.txtPaidDay2 = New System.Windows.Forms.TextBox()
        Me.txtPaidDay = New System.Windows.Forms.TextBox()
        Me.ChkPoRate = New System.Windows.Forms.CheckBox()
        Me.txtSeq = New System.Windows.Forms.TextBox()
        Me.txtAlias = New System.Windows.Forms.TextBox()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.lblPaymentTerms = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.FraView = New System.Windows.Forms.GroupBox()
        Me.SSTInfo = New System.Windows.Forms.TabControl()
        Me._SSTInfo_TabPage0 = New System.Windows.Forms.TabPage()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.chkStopGP = New System.Windows.Forms.CheckBox()
        Me.chkStopMRR = New System.Windows.Forms.CheckBox()
        Me.chkStopInvoice = New System.Windows.Forms.CheckBox()
        Me.chkStopBP = New System.Windows.Forms.CheckBox()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me.chkAuthorised = New System.Windows.Forms.CheckBox()
        Me.chkInterUnit = New System.Windows.Forms.CheckBox()
        Me.chkDistt = New System.Windows.Forms.CheckBox()
        Me.chkState = New System.Windows.Forms.CheckBox()
        Me.chkCountry = New System.Windows.Forms.CheckBox()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.txtDistance = New System.Windows.Forms.TextBox()
        Me.txtaddress = New System.Windows.Forms.TextBox()
        Me.txtState = New System.Windows.Forms.TextBox()
        Me.txtPinCode = New System.Windows.Forms.TextBox()
        Me.txtCity = New System.Windows.Forms.TextBox()
        Me.txtCountry = New System.Windows.Forms.TextBox()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.lblRegularized = New System.Windows.Forms.Label()
        Me.lblModDate = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.lblAddDate = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.lblModUser = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.lblAddUser = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me._Label25_1 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me._SSTInfo_TabPage1 = New System.Windows.Forms.TabPage()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.txtContact = New System.Windows.Forms.TextBox()
        Me.txtFax = New System.Windows.Forms.TextBox()
        Me.txtMobile = New System.Windows.Forms.TextBox()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.txtPhone = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me._Label25_0 = New System.Windows.Forms.Label()
        Me._SSTInfo_TabPage2 = New System.Windows.Forms.TabPage()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.txtServProvided = New System.Windows.Forms.TextBox()
        Me.txtSrvRegnNo = New System.Windows.Forms.TextBox()
        Me.txtTINNo = New System.Windows.Forms.TextBox()
        Me.txtPan = New System.Windows.Forms.TextBox()
        Me.txtCstNo = New System.Windows.Forms.TextBox()
        Me.txtLSTNo = New System.Windows.Forms.TextBox()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me._SSTInfo_TabPage3 = New System.Windows.Forms.TabPage()
        Me.Frame7 = New System.Windows.Forms.GroupBox()
        Me.Frame13 = New System.Windows.Forms.GroupBox()
        Me._optRegd_0 = New System.Windows.Forms.RadioButton()
        Me._optRegd_1 = New System.Windows.Forms.RadioButton()
        Me.txtDivision = New System.Windows.Forms.TextBox()
        Me.txtRange = New System.Windows.Forms.TextBox()
        Me.txtCommRate = New System.Windows.Forms.TextBox()
        Me.txtECCNo = New System.Windows.Forms.TextBox()
        Me.txtRegnNo = New System.Windows.Forms.TextBox()
        Me._Label25_2 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me._SSTInfo_TabPage4 = New System.Windows.Forms.TabPage()
        Me.Frame12 = New System.Windows.Forms.GroupBox()
        Me.chkLowerDeduction = New System.Windows.Forms.CheckBox()
        Me.txtLDCertiNo = New System.Windows.Forms.TextBox()
        Me.txtExptionCNo = New System.Windows.Forms.TextBox()
        Me.cboCType = New System.Windows.Forms.ComboBox()
        Me.txtESIPer = New System.Windows.Forms.TextBox()
        Me.txtSTDSPer = New System.Windows.Forms.TextBox()
        Me.CboTDSCategory = New System.Windows.Forms.ComboBox()
        Me.txtTDSPer = New System.Windows.Forms.TextBox()
        Me.txtSection = New System.Windows.Forms.TextBox()
        Me._lblLabels_0 = New System.Windows.Forms.Label()
        Me._lblLabels_4 = New System.Windows.Forms.Label()
        Me._lblLabels_5 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.LblPayTerms = New System.Windows.Forms.Label()
        Me.lblDueDays = New System.Windows.Forms.Label()
        Me._SSTInfo_TabPage5 = New System.Windows.Forms.TabPage()
        Me.Frame9 = New System.Windows.Forms.GroupBox()
        Me.txtPurchaseSTRecd = New System.Windows.Forms.TextBox()
        Me.txtPurchaseSTDue = New System.Windows.Forms.TextBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Frame10 = New System.Windows.Forms.GroupBox()
        Me.txtSaleSTDue = New System.Windows.Forms.TextBox()
        Me.txtSaleSTRecd = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me._SSTInfo_TabPage6 = New System.Windows.Forms.TabPage()
        Me.Frame11 = New System.Windows.Forms.GroupBox()
        Me.txtExportPaymetTerms = New System.Windows.Forms.TextBox()
        Me.txtFinalDest = New System.Windows.Forms.TextBox()
        Me.txtCarriage = New System.Windows.Forms.TextBox()
        Me.txtDischargePort = New System.Windows.Forms.TextBox()
        Me.txtLoadingPort = New System.Windows.Forms.TextBox()
        Me.txtBuyerName = New System.Windows.Forms.TextBox()
        Me.Label36 = New System.Windows.Forms.Label()
        Me._Label25_4 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me._Label25_3 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me._SSTInfo_TabPage7 = New System.Windows.Forms.TabPage()
        Me.Frame14 = New System.Windows.Forms.GroupBox()
        Me.txtBankName = New System.Windows.Forms.TextBox()
        Me.txtSwitCode = New System.Windows.Forms.TextBox()
        Me.txtIFSCCode = New System.Windows.Forms.TextBox()
        Me.txtBankBranch = New System.Windows.Forms.TextBox()
        Me.txtBankAccountNo = New System.Windows.Forms.TextBox()
        Me._Label25_5 = New System.Windows.Forms.Label()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.Label54 = New System.Windows.Forms.Label()
        Me._Label25_6 = New System.Windows.Forms.Label()
        Me.Label55 = New System.Windows.Forms.Label()
        Me._SSTInfo_TabPage8 = New System.Windows.Forms.TabPage()
        Me.FraGSTClass = New System.Windows.Forms.GroupBox()
        Me._optGSTClassification_0 = New System.Windows.Forms.RadioButton()
        Me._optGSTClassification_1 = New System.Windows.Forms.RadioButton()
        Me.Frame15 = New System.Windows.Forms.GroupBox()
        Me.FraGSTStatus = New System.Windows.Forms.GroupBox()
        Me._optGSTRegd_0 = New System.Windows.Forms.RadioButton()
        Me._optGSTRegd_1 = New System.Windows.Forms.RadioButton()
        Me._optGSTRegd_2 = New System.Windows.Forms.RadioButton()
        Me._optGSTRegd_3 = New System.Windows.Forms.RadioButton()
        Me._optGSTRegd_4 = New System.Windows.Forms.RadioButton()
        Me.txtGSTRegnNo = New System.Windows.Forms.TextBox()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.ADataGrid = New VB6.ADODC()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.lblMasterType = New System.Windows.Forms.Label()
        Me.Label25 = New VB6.LabelArray(Me.components)
        Me.OptStatus = New VB6.RadioButtonArray(Me.components)
        Me.lblLabels = New VB6.LabelArray(Me.components)
        Me.optBalMethod = New VB6.RadioButtonArray(Me.components)
        Me.optGSTClassification = New VB6.RadioButtonArray(Me.components)
        Me.optGSTRegd = New VB6.RadioButtonArray(Me.components)
        Me.optRegd = New VB6.RadioButtonArray(Me.components)
        Me.FraTrn.SuspendLayout()
        Me.FraStatus.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.fraTop1.SuspendLayout()
        Me.FraAcctType.SuspendLayout()
        Me.Frame8.SuspendLayout()
        Me.FraView.SuspendLayout()
        Me.SSTInfo.SuspendLayout()
        Me._SSTInfo_TabPage0.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.Frame5.SuspendLayout()
        Me.Frame4.SuspendLayout()
        Me._SSTInfo_TabPage1.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me._SSTInfo_TabPage2.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me._SSTInfo_TabPage3.SuspendLayout()
        Me.Frame7.SuspendLayout()
        Me.Frame13.SuspendLayout()
        Me._SSTInfo_TabPage4.SuspendLayout()
        Me.Frame12.SuspendLayout()
        Me._SSTInfo_TabPage5.SuspendLayout()
        Me.Frame9.SuspendLayout()
        Me.Frame10.SuspendLayout()
        Me._SSTInfo_TabPage6.SuspendLayout()
        Me.Frame11.SuspendLayout()
        Me._SSTInfo_TabPage7.SuspendLayout()
        Me.Frame14.SuspendLayout()
        Me._SSTInfo_TabPage8.SuspendLayout()
        Me.FraGSTClass.SuspendLayout()
        Me.Frame15.SuspendLayout()
        Me.FraGSTStatus.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.Label25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLabels, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optBalMethod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optGSTClassification, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optGSTRegd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optRegd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CmdAdd
        '
        Me.CmdAdd.BackColor = System.Drawing.SystemColors.Control
        Me.CmdAdd.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdAdd.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdAdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdAdd.Image = CType(resources.GetObject("CmdAdd.Image"), System.Drawing.Image)
        Me.CmdAdd.Location = New System.Drawing.Point(60, 11)
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
        Me.CmdModify.Location = New System.Drawing.Point(128, 11)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(67, 37)
        Me.CmdModify.TabIndex = 79
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
        Me.CmdSave.Location = New System.Drawing.Point(196, 11)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(67, 37)
        Me.CmdSave.TabIndex = 80
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
        Me.CmdDelete.Location = New System.Drawing.Point(330, 11)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(67, 37)
        Me.CmdDelete.TabIndex = 82
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
        Me.CmdView.Location = New System.Drawing.Point(532, 11)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(67, 37)
        Me.CmdView.TabIndex = 85
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
        Me.CmdClose.Location = New System.Drawing.Point(600, 11)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(67, 37)
        Me.CmdClose.TabIndex = 86
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
        Me.cmdPrint.Location = New System.Drawing.Point(398, 11)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 83
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
        Me.cmdSavePrint.Location = New System.Drawing.Point(264, 11)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdSavePrint.TabIndex = 81
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
        Me.CmdPreview.Location = New System.Drawing.Point(466, 11)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.TabIndex = 84
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Preview")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'FraTrn
        '
        Me.FraTrn.BackColor = System.Drawing.SystemColors.Control
        Me.FraTrn.Controls.Add(Me.txtRemarks)
        Me.FraTrn.Controls.Add(Me.FraStatus)
        Me.FraTrn.Controls.Add(Me.Frame1)
        Me.FraTrn.Controls.Add(Me.fraTop1)
        Me.FraTrn.Controls.Add(Me.FraAcctType)
        Me.FraTrn.Controls.Add(Me.Frame8)
        Me.FraTrn.Controls.Add(Me.FraView)
        Me.FraTrn.Controls.Add(Me.Label19)
        Me.FraTrn.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraTrn.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraTrn.Location = New System.Drawing.Point(0, -6)
        Me.FraTrn.Name = "FraTrn"
        Me.FraTrn.Padding = New System.Windows.Forms.Padding(0)
        Me.FraTrn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraTrn.Size = New System.Drawing.Size(753, 417)
        Me.FraTrn.TabIndex = 87
        Me.FraTrn.TabStop = False
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtRemarks.Location = New System.Drawing.Point(81, 366)
        Me.txtRemarks.MaxLength = 0
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.Size = New System.Drawing.Size(473, 45)
        Me.txtRemarks.TabIndex = 74
        '
        'FraStatus
        '
        Me.FraStatus.BackColor = System.Drawing.SystemColors.Control
        Me.FraStatus.Controls.Add(Me._OptStatus_1)
        Me.FraStatus.Controls.Add(Me._OptStatus_0)
        Me.FraStatus.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraStatus.Location = New System.Drawing.Point(556, 360)
        Me.FraStatus.Name = "FraStatus"
        Me.FraStatus.Padding = New System.Windows.Forms.Padding(0)
        Me.FraStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraStatus.Size = New System.Drawing.Size(83, 57)
        Me.FraStatus.TabIndex = 136
        Me.FraStatus.TabStop = False
        Me.FraStatus.Text = "Status"
        '
        '_OptStatus_1
        '
        Me._OptStatus_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptStatus_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptStatus_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptStatus_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptStatus.SetIndex(Me._OptStatus_1, CType(1, Short))
        Me._OptStatus_1.Location = New System.Drawing.Point(6, 38)
        Me._OptStatus_1.Name = "_OptStatus_1"
        Me._OptStatus_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptStatus_1.Size = New System.Drawing.Size(67, 16)
        Me._OptStatus_1.TabIndex = 76
        Me._OptStatus_1.TabStop = True
        Me._OptStatus_1.Text = "Close"
        Me._OptStatus_1.UseVisualStyleBackColor = False
        '
        '_OptStatus_0
        '
        Me._OptStatus_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptStatus_0.Checked = True
        Me._OptStatus_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptStatus_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptStatus_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptStatus.SetIndex(Me._OptStatus_0, CType(0, Short))
        Me._OptStatus_0.Location = New System.Drawing.Point(6, 18)
        Me._OptStatus_0.Name = "_OptStatus_0"
        Me._OptStatus_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptStatus_0.Size = New System.Drawing.Size(67, 17)
        Me._OptStatus_0.TabIndex = 75
        Me._OptStatus_0.TabStop = True
        Me._OptStatus_0.Text = "Open"
        Me._OptStatus_0.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me._optBalMethod_0)
        Me.Frame1.Controls.Add(Me._optBalMethod_1)
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(640, 360)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(113, 57)
        Me.Frame1.TabIndex = 135
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Balancing Method"
        '
        '_optBalMethod_0
        '
        Me._optBalMethod_0.BackColor = System.Drawing.SystemColors.Menu
        Me._optBalMethod_0.Checked = True
        Me._optBalMethod_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optBalMethod_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optBalMethod_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optBalMethod.SetIndex(Me._optBalMethod_0, CType(0, Short))
        Me._optBalMethod_0.Location = New System.Drawing.Point(6, 18)
        Me._optBalMethod_0.Name = "_optBalMethod_0"
        Me._optBalMethod_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optBalMethod_0.Size = New System.Drawing.Size(91, 16)
        Me._optBalMethod_0.TabIndex = 77
        Me._optBalMethod_0.TabStop = True
        Me._optBalMethod_0.Text = "Summarised"
        Me._optBalMethod_0.UseVisualStyleBackColor = False
        '
        '_optBalMethod_1
        '
        Me._optBalMethod_1.BackColor = System.Drawing.SystemColors.Menu
        Me._optBalMethod_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optBalMethod_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optBalMethod_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optBalMethod.SetIndex(Me._optBalMethod_1, CType(1, Short))
        Me._optBalMethod_1.Location = New System.Drawing.Point(8, 38)
        Me._optBalMethod_1.Name = "_optBalMethod_1"
        Me._optBalMethod_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optBalMethod_1.Size = New System.Drawing.Size(75, 16)
        Me._optBalMethod_1.TabIndex = 78
        Me._optBalMethod_1.TabStop = True
        Me._optBalMethod_1.Text = "Detailed"
        Me._optBalMethod_1.UseVisualStyleBackColor = False
        '
        'fraTop1
        '
        Me.fraTop1.BackColor = System.Drawing.SystemColors.Control
        Me.fraTop1.Controls.Add(Me.txtName)
        Me.fraTop1.Controls.Add(Me.txtCode)
        Me.fraTop1.Controls.Add(Me.Label2)
        Me.fraTop1.Controls.Add(Me.Label4)
        Me.fraTop1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTop1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraTop1.Location = New System.Drawing.Point(0, 0)
        Me.fraTop1.Name = "fraTop1"
        Me.fraTop1.Padding = New System.Windows.Forms.Padding(0)
        Me.fraTop1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraTop1.Size = New System.Drawing.Size(751, 37)
        Me.fraTop1.TabIndex = 88
        Me.fraTop1.TabStop = False
        '
        'txtName
        '
        Me.txtName.AcceptsReturn = True
        Me.txtName.BackColor = System.Drawing.SystemColors.Window
        Me.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtName.ForeColor = System.Drawing.Color.Blue
        Me.txtName.Location = New System.Drawing.Point(114, 12)
        Me.txtName.MaxLength = 0
        Me.txtName.Name = "txtName"
        Me.txtName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtName.Size = New System.Drawing.Size(371, 20)
        Me.txtName.TabIndex = 1
        '
        'txtCode
        '
        Me.txtCode.AcceptsReturn = True
        Me.txtCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCode.ForeColor = System.Drawing.Color.Blue
        Me.txtCode.Location = New System.Drawing.Point(680, 12)
        Me.txtCode.MaxLength = 0
        Me.txtCode.Name = "txtCode"
        Me.txtCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCode.Size = New System.Drawing.Size(67, 20)
        Me.txtCode.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(20, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(92, 14)
        Me.Label2.TabIndex = 89
        Me.Label2.Text = "Account Name :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(640, 14)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(42, 14)
        Me.Label4.TabIndex = 90
        Me.Label4.Text = "Code :"
        '
        'FraAcctType
        '
        Me.FraAcctType.BackColor = System.Drawing.SystemColors.Control
        Me.FraAcctType.Controls.Add(Me.txtEmpCode)
        Me.FraAcctType.Controls.Add(Me.txtCurrency)
        Me.FraAcctType.Controls.Add(Me.txtGroupNameCr)
        Me.FraAcctType.Controls.Add(Me.cboSupplierType)
        Me.FraAcctType.Controls.Add(Me.cboHeadType)
        Me.FraAcctType.Controls.Add(Me.cboCategory)
        Me.FraAcctType.Controls.Add(Me.txtGroupName)
        Me.FraAcctType.Controls.Add(Me.Label26)
        Me.FraAcctType.Controls.Add(Me.Label24)
        Me.FraAcctType.Controls.Add(Me.Label23)
        Me.FraAcctType.Controls.Add(Me.Label21)
        Me.FraAcctType.Controls.Add(Me.lblHeadType)
        Me.FraAcctType.Controls.Add(Me.LblCategory)
        Me.FraAcctType.Controls.Add(Me.lblUnder)
        Me.FraAcctType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraAcctType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraAcctType.Location = New System.Drawing.Point(0, 32)
        Me.FraAcctType.Name = "FraAcctType"
        Me.FraAcctType.Padding = New System.Windows.Forms.Padding(0)
        Me.FraAcctType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraAcctType.Size = New System.Drawing.Size(751, 89)
        Me.FraAcctType.TabIndex = 91
        Me.FraAcctType.TabStop = False
        '
        'txtEmpCode
        '
        Me.txtEmpCode.AcceptsReturn = True
        Me.txtEmpCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmpCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmpCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmpCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpCode.ForeColor = System.Drawing.Color.Blue
        Me.txtEmpCode.Location = New System.Drawing.Point(605, 60)
        Me.txtEmpCode.MaxLength = 0
        Me.txtEmpCode.Name = "txtEmpCode"
        Me.txtEmpCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmpCode.Size = New System.Drawing.Size(141, 20)
        Me.txtEmpCode.TabIndex = 12
        '
        'txtCurrency
        '
        Me.txtCurrency.AcceptsReturn = True
        Me.txtCurrency.BackColor = System.Drawing.SystemColors.Window
        Me.txtCurrency.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCurrency.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCurrency.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrency.ForeColor = System.Drawing.Color.Blue
        Me.txtCurrency.Location = New System.Drawing.Point(604, 36)
        Me.txtCurrency.MaxLength = 0
        Me.txtCurrency.Name = "txtCurrency"
        Me.txtCurrency.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCurrency.Size = New System.Drawing.Size(141, 20)
        Me.txtCurrency.TabIndex = 9
        '
        'txtGroupNameCr
        '
        Me.txtGroupNameCr.AcceptsReturn = True
        Me.txtGroupNameCr.BackColor = System.Drawing.SystemColors.Window
        Me.txtGroupNameCr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGroupNameCr.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGroupNameCr.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGroupNameCr.ForeColor = System.Drawing.Color.Blue
        Me.txtGroupNameCr.Location = New System.Drawing.Point(114, 60)
        Me.txtGroupNameCr.MaxLength = 0
        Me.txtGroupNameCr.Name = "txtGroupNameCr"
        Me.txtGroupNameCr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGroupNameCr.Size = New System.Drawing.Size(371, 20)
        Me.txtGroupNameCr.TabIndex = 10
        '
        'cboSupplierType
        '
        Me.cboSupplierType.BackColor = System.Drawing.SystemColors.Window
        Me.cboSupplierType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboSupplierType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSupplierType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboSupplierType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.cboSupplierType.Location = New System.Drawing.Point(606, 10)
        Me.cboSupplierType.Name = "cboSupplierType"
        Me.cboSupplierType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboSupplierType.Size = New System.Drawing.Size(141, 22)
        Me.cboSupplierType.TabIndex = 6
        '
        'cboHeadType
        '
        Me.cboHeadType.BackColor = System.Drawing.SystemColors.Window
        Me.cboHeadType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboHeadType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboHeadType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboHeadType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboHeadType.Location = New System.Drawing.Point(342, 10)
        Me.cboHeadType.Name = "cboHeadType"
        Me.cboHeadType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboHeadType.Size = New System.Drawing.Size(143, 22)
        Me.cboHeadType.TabIndex = 5
        '
        'cboCategory
        '
        Me.cboCategory.BackColor = System.Drawing.SystemColors.Window
        Me.cboCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCategory.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCategory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.cboCategory.Location = New System.Drawing.Point(114, 10)
        Me.cboCategory.Name = "cboCategory"
        Me.cboCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCategory.Size = New System.Drawing.Size(141, 22)
        Me.cboCategory.TabIndex = 4
        '
        'txtGroupName
        '
        Me.txtGroupName.AcceptsReturn = True
        Me.txtGroupName.BackColor = System.Drawing.SystemColors.Window
        Me.txtGroupName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGroupName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGroupName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGroupName.ForeColor = System.Drawing.Color.Blue
        Me.txtGroupName.Location = New System.Drawing.Point(114, 36)
        Me.txtGroupName.MaxLength = 0
        Me.txtGroupName.Name = "txtGroupName"
        Me.txtGroupName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGroupName.Size = New System.Drawing.Size(371, 20)
        Me.txtGroupName.TabIndex = 7
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.SystemColors.Control
        Me.Label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label26.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(539, 62)
        Me.Label26.Name = "Label26"
        Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label26.Size = New System.Drawing.Size(69, 14)
        Me.Label26.TabIndex = 120
        Me.Label26.Text = "Emp Code :"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.SystemColors.Control
        Me.Label24.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label24.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label24.Location = New System.Drawing.Point(545, 38)
        Me.Label24.Name = "Label24"
        Me.Label24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label24.Size = New System.Drawing.Size(64, 14)
        Me.Label24.TabIndex = 119
        Me.Label24.Text = "Currency :"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.SystemColors.Control
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(9, 62)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(105, 14)
        Me.Label23.TabIndex = 118
        Me.Label23.Text = "Group Name (Cr) :"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.SystemColors.Control
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(514, 13)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(92, 17)
        Me.Label21.TabIndex = 117
        Me.Label21.Text = "Supplier Type :"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblHeadType
        '
        Me.lblHeadType.AutoSize = True
        Me.lblHeadType.BackColor = System.Drawing.SystemColors.Control
        Me.lblHeadType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblHeadType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeadType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblHeadType.Location = New System.Drawing.Point(266, 13)
        Me.lblHeadType.Name = "lblHeadType"
        Me.lblHeadType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblHeadType.Size = New System.Drawing.Size(69, 14)
        Me.lblHeadType.TabIndex = 93
        Me.lblHeadType.Text = "Head Type :"
        Me.lblHeadType.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LblCategory
        '
        Me.LblCategory.BackColor = System.Drawing.SystemColors.Control
        Me.LblCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblCategory.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCategory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblCategory.Location = New System.Drawing.Point(20, 13)
        Me.LblCategory.Name = "LblCategory"
        Me.LblCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblCategory.Size = New System.Drawing.Size(92, 17)
        Me.LblCategory.TabIndex = 92
        Me.LblCategory.Text = "Category :"
        Me.LblCategory.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblUnder
        '
        Me.lblUnder.AutoSize = True
        Me.lblUnder.BackColor = System.Drawing.SystemColors.Control
        Me.lblUnder.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblUnder.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUnder.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.Location = New System.Drawing.Point(8, 38)
        Me.lblUnder.Name = "lblUnder"
        Me.lblUnder.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblUnder.Size = New System.Drawing.Size(104, 14)
        Me.lblUnder.TabIndex = 94
        Me.lblUnder.Text = "Group Name (Dr) :"
        Me.lblUnder.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame8
        '
        Me.Frame8.BackColor = System.Drawing.SystemColors.Control
        Me.Frame8.Controls.Add(Me.cboPaymentMode)
        Me.Frame8.Controls.Add(Me.txtChqFrequency)
        Me.Frame8.Controls.Add(Me.txtPayment)
        Me.Frame8.Controls.Add(Me.chkMonthWiseLdgr)
        Me.Frame8.Controls.Add(Me.txtVendorCode)
        Me.Frame8.Controls.Add(Me.txtPaidDay4)
        Me.Frame8.Controls.Add(Me.txtPaidDay3)
        Me.Frame8.Controls.Add(Me.txtPaidDay2)
        Me.Frame8.Controls.Add(Me.txtPaidDay)
        Me.Frame8.Controls.Add(Me.ChkPoRate)
        Me.Frame8.Controls.Add(Me.txtSeq)
        Me.Frame8.Controls.Add(Me.txtAlias)
        Me.Frame8.Controls.Add(Me.Label50)
        Me.Frame8.Controls.Add(Me.Label49)
        Me.Frame8.Controls.Add(Me.Label47)
        Me.Frame8.Controls.Add(Me.lblPaymentTerms)
        Me.Frame8.Controls.Add(Me.Label43)
        Me.Frame8.Controls.Add(Me.Label41)
        Me.Frame8.Controls.Add(Me.Label40)
        Me.Frame8.Controls.Add(Me.Label13)
        Me.Frame8.Controls.Add(Me.Label22)
        Me.Frame8.Controls.Add(Me.Label31)
        Me.Frame8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame8.Location = New System.Drawing.Point(0, 116)
        Me.Frame8.Name = "Frame8"
        Me.Frame8.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame8.Size = New System.Drawing.Size(751, 65)
        Me.Frame8.TabIndex = 145
        Me.Frame8.TabStop = False
        '
        'cboPaymentMode
        '
        Me.cboPaymentMode.BackColor = System.Drawing.SystemColors.Window
        Me.cboPaymentMode.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboPaymentMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPaymentMode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPaymentMode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.cboPaymentMode.Location = New System.Drawing.Point(510, 16)
        Me.cboPaymentMode.Name = "cboPaymentMode"
        Me.cboPaymentMode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboPaymentMode.Size = New System.Drawing.Size(95, 22)
        Me.cboPaymentMode.TabIndex = 16
        '
        'txtChqFrequency
        '
        Me.txtChqFrequency.AcceptsReturn = True
        Me.txtChqFrequency.BackColor = System.Drawing.SystemColors.Window
        Me.txtChqFrequency.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtChqFrequency.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtChqFrequency.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChqFrequency.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtChqFrequency.Location = New System.Drawing.Point(503, 40)
        Me.txtChqFrequency.MaxLength = 15
        Me.txtChqFrequency.Name = "txtChqFrequency"
        Me.txtChqFrequency.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtChqFrequency.Size = New System.Drawing.Size(39, 20)
        Me.txtChqFrequency.TabIndex = 21
        '
        'txtPayment
        '
        Me.txtPayment.AcceptsReturn = True
        Me.txtPayment.BackColor = System.Drawing.SystemColors.Window
        Me.txtPayment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPayment.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPayment.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPayment.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPayment.Location = New System.Drawing.Point(616, 40)
        Me.txtPayment.MaxLength = 15
        Me.txtPayment.Name = "txtPayment"
        Me.txtPayment.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPayment.Size = New System.Drawing.Size(43, 20)
        Me.txtPayment.TabIndex = 23
        '
        'chkMonthWiseLdgr
        '
        Me.chkMonthWiseLdgr.BackColor = System.Drawing.SystemColors.Control
        Me.chkMonthWiseLdgr.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkMonthWiseLdgr.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMonthWiseLdgr.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkMonthWiseLdgr.Location = New System.Drawing.Point(614, 24)
        Me.chkMonthWiseLdgr.Name = "chkMonthWiseLdgr"
        Me.chkMonthWiseLdgr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkMonthWiseLdgr.Size = New System.Drawing.Size(133, 16)
        Me.chkMonthWiseLdgr.TabIndex = 162
        Me.chkMonthWiseLdgr.Text = "Month Wise Ledger"
        Me.chkMonthWiseLdgr.UseVisualStyleBackColor = False
        '
        'txtVendorCode
        '
        Me.txtVendorCode.AcceptsReturn = True
        Me.txtVendorCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtVendorCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVendorCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVendorCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVendorCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtVendorCode.Location = New System.Drawing.Point(199, 16)
        Me.txtVendorCode.MaxLength = 15
        Me.txtVendorCode.Name = "txtVendorCode"
        Me.txtVendorCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVendorCode.Size = New System.Drawing.Size(81, 20)
        Me.txtVendorCode.TabIndex = 14
        '
        'txtPaidDay4
        '
        Me.txtPaidDay4.AcceptsReturn = True
        Me.txtPaidDay4.BackColor = System.Drawing.SystemColors.Window
        Me.txtPaidDay4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPaidDay4.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPaidDay4.Enabled = False
        Me.txtPaidDay4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaidDay4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPaidDay4.Location = New System.Drawing.Point(503, 40)
        Me.txtPaidDay4.MaxLength = 15
        Me.txtPaidDay4.Name = "txtPaidDay4"
        Me.txtPaidDay4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPaidDay4.Size = New System.Drawing.Size(39, 20)
        Me.txtPaidDay4.TabIndex = 22
        Me.txtPaidDay4.Visible = False
        '
        'txtPaidDay3
        '
        Me.txtPaidDay3.AcceptsReturn = True
        Me.txtPaidDay3.BackColor = System.Drawing.SystemColors.Window
        Me.txtPaidDay3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPaidDay3.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPaidDay3.Enabled = False
        Me.txtPaidDay3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaidDay3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPaidDay3.Location = New System.Drawing.Point(312, 40)
        Me.txtPaidDay3.MaxLength = 15
        Me.txtPaidDay3.Name = "txtPaidDay3"
        Me.txtPaidDay3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPaidDay3.Size = New System.Drawing.Size(39, 20)
        Me.txtPaidDay3.TabIndex = 20
        '
        'txtPaidDay2
        '
        Me.txtPaidDay2.AcceptsReturn = True
        Me.txtPaidDay2.BackColor = System.Drawing.SystemColors.Window
        Me.txtPaidDay2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPaidDay2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPaidDay2.Enabled = False
        Me.txtPaidDay2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaidDay2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPaidDay2.Location = New System.Drawing.Point(200, 40)
        Me.txtPaidDay2.MaxLength = 15
        Me.txtPaidDay2.Name = "txtPaidDay2"
        Me.txtPaidDay2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPaidDay2.Size = New System.Drawing.Size(39, 20)
        Me.txtPaidDay2.TabIndex = 19
        '
        'txtPaidDay
        '
        Me.txtPaidDay.AcceptsReturn = True
        Me.txtPaidDay.BackColor = System.Drawing.SystemColors.Window
        Me.txtPaidDay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPaidDay.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPaidDay.Enabled = False
        Me.txtPaidDay.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaidDay.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPaidDay.Location = New System.Drawing.Point(73, 40)
        Me.txtPaidDay.MaxLength = 15
        Me.txtPaidDay.Name = "txtPaidDay"
        Me.txtPaidDay.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPaidDay.Size = New System.Drawing.Size(39, 20)
        Me.txtPaidDay.TabIndex = 18
        '
        'ChkPoRate
        '
        Me.ChkPoRate.BackColor = System.Drawing.SystemColors.Control
        Me.ChkPoRate.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkPoRate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkPoRate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkPoRate.Location = New System.Drawing.Point(614, 11)
        Me.ChkPoRate.Name = "ChkPoRate"
        Me.ChkPoRate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkPoRate.Size = New System.Drawing.Size(125, 16)
        Me.ChkPoRate.TabIndex = 17
        Me.ChkPoRate.Text = "PO Rate Editable"
        Me.ChkPoRate.UseVisualStyleBackColor = False
        '
        'txtSeq
        '
        Me.txtSeq.AcceptsReturn = True
        Me.txtSeq.BackColor = System.Drawing.SystemColors.Window
        Me.txtSeq.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSeq.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSeq.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSeq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSeq.Location = New System.Drawing.Point(353, 16)
        Me.txtSeq.MaxLength = 15
        Me.txtSeq.Name = "txtSeq"
        Me.txtSeq.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSeq.Size = New System.Drawing.Size(39, 20)
        Me.txtSeq.TabIndex = 15
        Me.txtSeq.Visible = False
        '
        'txtAlias
        '
        Me.txtAlias.AcceptsReturn = True
        Me.txtAlias.BackColor = System.Drawing.SystemColors.Window
        Me.txtAlias.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAlias.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAlias.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAlias.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtAlias.Location = New System.Drawing.Point(73, 16)
        Me.txtAlias.MaxLength = 15
        Me.txtAlias.Name = "txtAlias"
        Me.txtAlias.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAlias.Size = New System.Drawing.Size(39, 20)
        Me.txtAlias.TabIndex = 13
        '
        'Label50
        '
        Me.Label50.BackColor = System.Drawing.SystemColors.Control
        Me.Label50.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label50.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label50.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label50.Location = New System.Drawing.Point(404, 18)
        Me.Label50.Name = "Label50"
        Me.Label50.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label50.Size = New System.Drawing.Size(104, 17)
        Me.Label50.TabIndex = 182
        Me.Label50.Text = "Mode of Payment :"
        '
        'Label49
        '
        Me.Label49.AutoSize = True
        Me.Label49.BackColor = System.Drawing.Color.Transparent
        Me.Label49.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label49.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label49.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label49.Location = New System.Drawing.Point(413, 42)
        Me.Label49.Name = "Label49"
        Me.Label49.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label49.Size = New System.Drawing.Size(90, 14)
        Me.Label49.TabIndex = 180
        Me.Label49.Text = "Chqs in Month:"
        Me.Label49.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.BackColor = System.Drawing.Color.Transparent
        Me.Label47.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label47.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label47.ForeColor = System.Drawing.Color.Black
        Me.Label47.Location = New System.Drawing.Point(547, 42)
        Me.Label47.Name = "Label47"
        Me.Label47.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label47.Size = New System.Drawing.Size(70, 14)
        Me.Label47.TabIndex = 173
        Me.Label47.Text = "Pay.Terms :"
        Me.Label47.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblPaymentTerms
        '
        Me.lblPaymentTerms.BackColor = System.Drawing.Color.Transparent
        Me.lblPaymentTerms.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPaymentTerms.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPaymentTerms.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPaymentTerms.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPaymentTerms.Location = New System.Drawing.Point(684, 40)
        Me.lblPaymentTerms.Name = "lblPaymentTerms"
        Me.lblPaymentTerms.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPaymentTerms.Size = New System.Drawing.Size(63, 19)
        Me.lblPaymentTerms.TabIndex = 25
        Me.lblPaymentTerms.Text = "lblPaymentTerms"
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.BackColor = System.Drawing.Color.Transparent
        Me.Label43.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label43.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label43.Location = New System.Drawing.Point(116, 18)
        Me.Label43.Name = "Label43"
        Me.Label43.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label43.Size = New System.Drawing.Size(85, 14)
        Me.Label43.TabIndex = 161
        Me.Label43.Text = "Vendor Code :"
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.BackColor = System.Drawing.Color.Transparent
        Me.Label41.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label41.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label41.Location = New System.Drawing.Point(241, 42)
        Me.Label41.Name = "Label41"
        Me.Label41.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label41.Size = New System.Drawing.Size(64, 14)
        Me.Label41.TabIndex = 158
        Me.Label41.Text = "Paid Day 3:"
        Me.Label41.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.BackColor = System.Drawing.Color.Transparent
        Me.Label40.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label40.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label40.Location = New System.Drawing.Point(133, 42)
        Me.Label40.Name = "Label40"
        Me.Label40.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label40.Size = New System.Drawing.Size(67, 14)
        Me.Label40.TabIndex = 157
        Me.Label40.Text = "Paid Day 2 :"
        Me.Label40.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(7, 42)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(64, 14)
        Me.Label13.TabIndex = 148
        Me.Label13.Text = "Paid Day 1:"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(284, 18)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(61, 14)
        Me.Label22.TabIndex = 147
        Me.Label22.Text = "DDR Seq. :"
        Me.Label22.Visible = False
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.BackColor = System.Drawing.Color.Transparent
        Me.Label31.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label31.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label31.Location = New System.Drawing.Point(32, 18)
        Me.Label31.Name = "Label31"
        Me.Label31.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label31.Size = New System.Drawing.Size(40, 14)
        Me.Label31.TabIndex = 146
        Me.Label31.Text = "Alias :"
        '
        'FraView
        '
        Me.FraView.BackColor = System.Drawing.SystemColors.Control
        Me.FraView.Controls.Add(Me.SSTInfo)
        Me.FraView.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraView.Location = New System.Drawing.Point(0, 176)
        Me.FraView.Name = "FraView"
        Me.FraView.Padding = New System.Windows.Forms.Padding(0)
        Me.FraView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraView.Size = New System.Drawing.Size(751, 185)
        Me.FraView.TabIndex = 95
        Me.FraView.TabStop = False
        '
        'SSTInfo
        '
        Me.SSTInfo.Controls.Add(Me._SSTInfo_TabPage0)
        Me.SSTInfo.Controls.Add(Me._SSTInfo_TabPage1)
        Me.SSTInfo.Controls.Add(Me._SSTInfo_TabPage2)
        Me.SSTInfo.Controls.Add(Me._SSTInfo_TabPage3)
        Me.SSTInfo.Controls.Add(Me._SSTInfo_TabPage4)
        Me.SSTInfo.Controls.Add(Me._SSTInfo_TabPage5)
        Me.SSTInfo.Controls.Add(Me._SSTInfo_TabPage6)
        Me.SSTInfo.Controls.Add(Me._SSTInfo_TabPage7)
        Me.SSTInfo.Controls.Add(Me._SSTInfo_TabPage8)
        Me.SSTInfo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SSTInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.SSTInfo.ItemSize = New System.Drawing.Size(42, 18)
        Me.SSTInfo.Location = New System.Drawing.Point(4, 8)
        Me.SSTInfo.Name = "SSTInfo"
        Me.SSTInfo.SelectedIndex = 0
        Me.SSTInfo.Size = New System.Drawing.Size(743, 174)
        Me.SSTInfo.TabIndex = 96
        '
        '_SSTInfo_TabPage0
        '
        Me._SSTInfo_TabPage0.Controls.Add(Me.Frame3)
        Me._SSTInfo_TabPage0.Controls.Add(Me.Frame5)
        Me._SSTInfo_TabPage0.Controls.Add(Me.Frame4)
        Me._SSTInfo_TabPage0.Location = New System.Drawing.Point(4, 22)
        Me._SSTInfo_TabPage0.Name = "_SSTInfo_TabPage0"
        Me._SSTInfo_TabPage0.Size = New System.Drawing.Size(735, 148)
        Me._SSTInfo_TabPage0.TabIndex = 0
        Me._SSTInfo_TabPage0.Text = "Mai&l"
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.chkStopGP)
        Me.Frame3.Controls.Add(Me.chkStopMRR)
        Me.Frame3.Controls.Add(Me.chkStopInvoice)
        Me.Frame3.Controls.Add(Me.chkStopBP)
        Me.Frame3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(560, 79)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(179, 69)
        Me.Frame3.TabIndex = 175
        Me.Frame3.TabStop = False
        '
        'chkStopGP
        '
        Me.chkStopGP.BackColor = System.Drawing.SystemColors.Control
        Me.chkStopGP.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkStopGP.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkStopGP.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkStopGP.Location = New System.Drawing.Point(6, 38)
        Me.chkStopGP.Name = "chkStopGP"
        Me.chkStopGP.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkStopGP.Size = New System.Drawing.Size(169, 15)
        Me.chkStopGP.TabIndex = 178
        Me.chkStopGP.Text = "Stop GatePass"
        Me.chkStopGP.UseVisualStyleBackColor = False
        '
        'chkStopMRR
        '
        Me.chkStopMRR.BackColor = System.Drawing.SystemColors.Control
        Me.chkStopMRR.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkStopMRR.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkStopMRR.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkStopMRR.Location = New System.Drawing.Point(6, 8)
        Me.chkStopMRR.Name = "chkStopMRR"
        Me.chkStopMRR.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkStopMRR.Size = New System.Drawing.Size(159, 17)
        Me.chkStopMRR.TabIndex = 176
        Me.chkStopMRR.Text = "Stop MRR Entry"
        Me.chkStopMRR.UseVisualStyleBackColor = False
        '
        'chkStopInvoice
        '
        Me.chkStopInvoice.BackColor = System.Drawing.SystemColors.Control
        Me.chkStopInvoice.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkStopInvoice.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkStopInvoice.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkStopInvoice.Location = New System.Drawing.Point(6, 24)
        Me.chkStopInvoice.Name = "chkStopInvoice"
        Me.chkStopInvoice.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkStopInvoice.Size = New System.Drawing.Size(153, 15)
        Me.chkStopInvoice.TabIndex = 177
        Me.chkStopInvoice.Text = "Stop Invoice"
        Me.chkStopInvoice.UseVisualStyleBackColor = False
        '
        'chkStopBP
        '
        Me.chkStopBP.BackColor = System.Drawing.SystemColors.Control
        Me.chkStopBP.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkStopBP.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkStopBP.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkStopBP.Location = New System.Drawing.Point(6, 52)
        Me.chkStopBP.Name = "chkStopBP"
        Me.chkStopBP.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkStopBP.Size = New System.Drawing.Size(169, 15)
        Me.chkStopBP.TabIndex = 179
        Me.chkStopBP.Text = "Stop Bank Payment"
        Me.chkStopBP.UseVisualStyleBackColor = False
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me.chkAuthorised)
        Me.Frame5.Controls.Add(Me.chkInterUnit)
        Me.Frame5.Controls.Add(Me.chkDistt)
        Me.Frame5.Controls.Add(Me.chkState)
        Me.Frame5.Controls.Add(Me.chkCountry)
        Me.Frame5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(560, 0)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(179, 81)
        Me.Frame5.TabIndex = 138
        Me.Frame5.TabStop = False
        '
        'chkAuthorised
        '
        Me.chkAuthorised.BackColor = System.Drawing.SystemColors.Control
        Me.chkAuthorised.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAuthorised.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAuthorised.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkAuthorised.Location = New System.Drawing.Point(6, 62)
        Me.chkAuthorised.Name = "chkAuthorised"
        Me.chkAuthorised.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAuthorised.Size = New System.Drawing.Size(97, 17)
        Me.chkAuthorised.TabIndex = 174
        Me.chkAuthorised.Text = "Authorised"
        Me.chkAuthorised.UseVisualStyleBackColor = False
        '
        'chkInterUnit
        '
        Me.chkInterUnit.BackColor = System.Drawing.SystemColors.Control
        Me.chkInterUnit.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkInterUnit.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkInterUnit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkInterUnit.Location = New System.Drawing.Point(6, 50)
        Me.chkInterUnit.Name = "chkInterUnit"
        Me.chkInterUnit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkInterUnit.Size = New System.Drawing.Size(105, 15)
        Me.chkInterUnit.TabIndex = 172
        Me.chkInterUnit.Text = "Inter Unit"
        Me.chkInterUnit.UseVisualStyleBackColor = False
        '
        'chkDistt
        '
        Me.chkDistt.BackColor = System.Drawing.SystemColors.Control
        Me.chkDistt.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkDistt.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDistt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkDistt.Location = New System.Drawing.Point(6, 22)
        Me.chkDistt.Name = "chkDistt"
        Me.chkDistt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkDistt.Size = New System.Drawing.Size(89, 15)
        Me.chkDistt.TabIndex = 33
        Me.chkDistt.Text = "Within Distt"
        Me.chkDistt.UseVisualStyleBackColor = False
        '
        'chkState
        '
        Me.chkState.BackColor = System.Drawing.SystemColors.Control
        Me.chkState.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkState.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkState.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkState.Location = New System.Drawing.Point(6, 8)
        Me.chkState.Name = "chkState"
        Me.chkState.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkState.Size = New System.Drawing.Size(95, 17)
        Me.chkState.TabIndex = 32
        Me.chkState.Text = "Within State"
        Me.chkState.UseVisualStyleBackColor = False
        '
        'chkCountry
        '
        Me.chkCountry.BackColor = System.Drawing.SystemColors.Control
        Me.chkCountry.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCountry.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCountry.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCountry.Location = New System.Drawing.Point(6, 36)
        Me.chkCountry.Name = "chkCountry"
        Me.chkCountry.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCountry.Size = New System.Drawing.Size(105, 15)
        Me.chkCountry.TabIndex = 34
        Me.chkCountry.Text = "Within Country"
        Me.chkCountry.UseVisualStyleBackColor = False
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.txtDistance)
        Me.Frame4.Controls.Add(Me.txtaddress)
        Me.Frame4.Controls.Add(Me.txtState)
        Me.Frame4.Controls.Add(Me.txtPinCode)
        Me.Frame4.Controls.Add(Me.txtCity)
        Me.Frame4.Controls.Add(Me.txtCountry)
        Me.Frame4.Controls.Add(Me.Label51)
        Me.Frame4.Controls.Add(Me.Label56)
        Me.Frame4.Controls.Add(Me.lblRegularized)
        Me.Frame4.Controls.Add(Me.lblModDate)
        Me.Frame4.Controls.Add(Me.Label48)
        Me.Frame4.Controls.Add(Me.lblAddDate)
        Me.Frame4.Controls.Add(Me.Label45)
        Me.Frame4.Controls.Add(Me.lblModUser)
        Me.Frame4.Controls.Add(Me.Label46)
        Me.Frame4.Controls.Add(Me.lblAddUser)
        Me.Frame4.Controls.Add(Me.Label44)
        Me.Frame4.Controls.Add(Me._Label25_1)
        Me.Frame4.Controls.Add(Me.Label9)
        Me.Frame4.Controls.Add(Me.Label10)
        Me.Frame4.Controls.Add(Me.Label27)
        Me.Frame4.Controls.Add(Me.Label39)
        Me.Frame4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(3, 0)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(555, 145)
        Me.Frame4.TabIndex = 139
        Me.Frame4.TabStop = False
        '
        'txtDistance
        '
        Me.txtDistance.AcceptsReturn = True
        Me.txtDistance.BackColor = System.Drawing.SystemColors.Window
        Me.txtDistance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDistance.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDistance.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDistance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDistance.Location = New System.Drawing.Point(345, 92)
        Me.txtDistance.MaxLength = 15
        Me.txtDistance.Name = "txtDistance"
        Me.txtDistance.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDistance.Size = New System.Drawing.Size(73, 20)
        Me.txtDistance.TabIndex = 31
        '
        'txtaddress
        '
        Me.txtaddress.AcceptsReturn = True
        Me.txtaddress.BackColor = System.Drawing.SystemColors.Window
        Me.txtaddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtaddress.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtaddress.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtaddress.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtaddress.Location = New System.Drawing.Point(12, 30)
        Me.txtaddress.MaxLength = 0
        Me.txtaddress.Multiline = True
        Me.txtaddress.Name = "txtaddress"
        Me.txtaddress.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtaddress.Size = New System.Drawing.Size(261, 81)
        Me.txtaddress.TabIndex = 26
        '
        'txtState
        '
        Me.txtState.AcceptsReturn = True
        Me.txtState.BackColor = System.Drawing.SystemColors.Window
        Me.txtState.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtState.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtState.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtState.Location = New System.Drawing.Point(344, 32)
        Me.txtState.MaxLength = 15
        Me.txtState.Name = "txtState"
        Me.txtState.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtState.Size = New System.Drawing.Size(203, 20)
        Me.txtState.TabIndex = 28
        '
        'txtPinCode
        '
        Me.txtPinCode.AcceptsReturn = True
        Me.txtPinCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtPinCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPinCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPinCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPinCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPinCode.Location = New System.Drawing.Point(344, 52)
        Me.txtPinCode.MaxLength = 7
        Me.txtPinCode.Name = "txtPinCode"
        Me.txtPinCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPinCode.Size = New System.Drawing.Size(203, 20)
        Me.txtPinCode.TabIndex = 29
        '
        'txtCity
        '
        Me.txtCity.AcceptsReturn = True
        Me.txtCity.BackColor = System.Drawing.SystemColors.Window
        Me.txtCity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCity.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCity.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCity.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtCity.Location = New System.Drawing.Point(344, 12)
        Me.txtCity.MaxLength = 15
        Me.txtCity.Name = "txtCity"
        Me.txtCity.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCity.Size = New System.Drawing.Size(203, 20)
        Me.txtCity.TabIndex = 27
        '
        'txtCountry
        '
        Me.txtCountry.AcceptsReturn = True
        Me.txtCountry.BackColor = System.Drawing.SystemColors.Window
        Me.txtCountry.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCountry.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCountry.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCountry.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtCountry.Location = New System.Drawing.Point(344, 72)
        Me.txtCountry.MaxLength = 15
        Me.txtCountry.Name = "txtCountry"
        Me.txtCountry.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCountry.Size = New System.Drawing.Size(203, 20)
        Me.txtCountry.TabIndex = 30
        '
        'Label51
        '
        Me.Label51.AutoSize = True
        Me.Label51.BackColor = System.Drawing.Color.Transparent
        Me.Label51.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label51.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label51.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label51.Location = New System.Drawing.Point(278, 96)
        Me.Label51.Name = "Label51"
        Me.Label51.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label51.Size = New System.Drawing.Size(60, 14)
        Me.Label51.TabIndex = 208
        Me.Label51.Text = "Distance :"
        Me.Label51.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label56
        '
        Me.Label56.AutoSize = True
        Me.Label56.BackColor = System.Drawing.Color.Transparent
        Me.Label56.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label56.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label56.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label56.Location = New System.Drawing.Point(421, 94)
        Me.Label56.Name = "Label56"
        Me.Label56.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label56.Size = New System.Drawing.Size(45, 14)
        Me.Label56.TabIndex = 207
        Me.Label56.Text = "(in KM)"
        Me.Label56.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblRegularized
        '
        Me.lblRegularized.BackColor = System.Drawing.SystemColors.Control
        Me.lblRegularized.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblRegularized.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRegularized.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRegularized.Location = New System.Drawing.Point(114, 10)
        Me.lblRegularized.Name = "lblRegularized"
        Me.lblRegularized.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblRegularized.Size = New System.Drawing.Size(75, 17)
        Me.lblRegularized.TabIndex = 183
        '
        'lblModDate
        '
        Me.lblModDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblModDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblModDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblModDate.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblModDate.Location = New System.Drawing.Point(477, 118)
        Me.lblModDate.Name = "lblModDate"
        Me.lblModDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblModDate.Size = New System.Drawing.Size(69, 19)
        Me.lblModDate.TabIndex = 170
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.BackColor = System.Drawing.SystemColors.Control
        Me.Label48.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label48.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label48.Location = New System.Drawing.Point(418, 120)
        Me.Label48.Name = "Label48"
        Me.Label48.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label48.Size = New System.Drawing.Size(63, 15)
        Me.Label48.TabIndex = 169
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
        Me.lblAddDate.Location = New System.Drawing.Point(205, 118)
        Me.lblAddDate.Name = "lblAddDate"
        Me.lblAddDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAddDate.Size = New System.Drawing.Size(69, 19)
        Me.lblAddDate.TabIndex = 168
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.BackColor = System.Drawing.SystemColors.Control
        Me.Label45.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label45.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label45.Location = New System.Drawing.Point(147, 120)
        Me.Label45.Name = "Label45"
        Me.Label45.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label45.Size = New System.Drawing.Size(64, 15)
        Me.Label45.TabIndex = 167
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
        Me.lblModUser.Location = New System.Drawing.Point(343, 118)
        Me.lblModUser.Name = "lblModUser"
        Me.lblModUser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblModUser.Size = New System.Drawing.Size(69, 19)
        Me.lblModUser.TabIndex = 166
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.BackColor = System.Drawing.SystemColors.Control
        Me.Label46.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label46.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label46.Location = New System.Drawing.Point(283, 120)
        Me.Label46.Name = "Label46"
        Me.Label46.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label46.Size = New System.Drawing.Size(64, 15)
        Me.Label46.TabIndex = 165
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
        Me.lblAddUser.Location = New System.Drawing.Point(69, 118)
        Me.lblAddUser.Name = "lblAddUser"
        Me.lblAddUser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAddUser.Size = New System.Drawing.Size(69, 19)
        Me.lblAddUser.TabIndex = 164
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.BackColor = System.Drawing.SystemColors.Control
        Me.Label44.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label44.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label44.Location = New System.Drawing.Point(10, 120)
        Me.Label44.Name = "Label44"
        Me.Label44.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label44.Size = New System.Drawing.Size(65, 15)
        Me.Label44.TabIndex = 163
        Me.Label44.Text = "Add User :"
        Me.Label44.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label25_1
        '
        Me._Label25_1.AutoSize = True
        Me._Label25_1.BackColor = System.Drawing.SystemColors.Control
        Me._Label25_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label25_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label25_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label25.SetIndex(Me._Label25_1, CType(1, Short))
        Me._Label25_1.Location = New System.Drawing.Point(274, 56)
        Me._Label25_1.Name = "_Label25_1"
        Me._Label25_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label25_1.Size = New System.Drawing.Size(30, 14)
        Me._Label25_1.TabIndex = 144
        Me._Label25_1.Text = "Pin :"
        Me._Label25_1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(4, 14)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(61, 14)
        Me.Label9.TabIndex = 143
        Me.Label9.Text = "Address :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(274, 32)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(41, 14)
        Me.Label10.TabIndex = 142
        Me.Label10.Text = "State :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.Color.Transparent
        Me.Label27.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label27.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label27.Location = New System.Drawing.Point(274, 14)
        Me.Label27.Name = "Label27"
        Me.Label27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label27.Size = New System.Drawing.Size(34, 14)
        Me.Label27.TabIndex = 141
        Me.Label27.Text = "City :"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.BackColor = System.Drawing.Color.Transparent
        Me.Label39.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label39.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label39.Location = New System.Drawing.Point(284, 76)
        Me.Label39.Name = "Label39"
        Me.Label39.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label39.Size = New System.Drawing.Size(57, 14)
        Me.Label39.TabIndex = 140
        Me.Label39.Text = "Country :"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_SSTInfo_TabPage1
        '
        Me._SSTInfo_TabPage1.Controls.Add(Me.Frame2)
        Me._SSTInfo_TabPage1.Location = New System.Drawing.Point(4, 22)
        Me._SSTInfo_TabPage1.Name = "_SSTInfo_TabPage1"
        Me._SSTInfo_TabPage1.Size = New System.Drawing.Size(735, 148)
        Me._SSTInfo_TabPage1.TabIndex = 1
        Me._SSTInfo_TabPage1.Text = "Communicatio&n"
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.txtContact)
        Me.Frame2.Controls.Add(Me.txtFax)
        Me.Frame2.Controls.Add(Me.txtMobile)
        Me.Frame2.Controls.Add(Me.txtEmail)
        Me.Frame2.Controls.Add(Me.txtPhone)
        Me.Frame2.Controls.Add(Me.Label6)
        Me.Frame2.Controls.Add(Me.Label5)
        Me.Frame2.Controls.Add(Me.Label3)
        Me.Frame2.Controls.Add(Me.Label1)
        Me.Frame2.Controls.Add(Me._Label25_0)
        Me.Frame2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(2, 3)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(737, 145)
        Me.Frame2.TabIndex = 103
        Me.Frame2.TabStop = False
        '
        'txtContact
        '
        Me.txtContact.AcceptsReturn = True
        Me.txtContact.BackColor = System.Drawing.SystemColors.Window
        Me.txtContact.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtContact.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtContact.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtContact.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtContact.Location = New System.Drawing.Point(116, 104)
        Me.txtContact.MaxLength = 15
        Me.txtContact.Name = "txtContact"
        Me.txtContact.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtContact.Size = New System.Drawing.Size(253, 20)
        Me.txtContact.TabIndex = 39
        '
        'txtFax
        '
        Me.txtFax.AcceptsReturn = True
        Me.txtFax.BackColor = System.Drawing.SystemColors.Window
        Me.txtFax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFax.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFax.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFax.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtFax.Location = New System.Drawing.Point(478, 22)
        Me.txtFax.MaxLength = 15
        Me.txtFax.Name = "txtFax"
        Me.txtFax.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFax.Size = New System.Drawing.Size(253, 20)
        Me.txtFax.TabIndex = 36
        '
        'txtMobile
        '
        Me.txtMobile.AcceptsReturn = True
        Me.txtMobile.BackColor = System.Drawing.SystemColors.Window
        Me.txtMobile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMobile.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMobile.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMobile.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtMobile.Location = New System.Drawing.Point(478, 62)
        Me.txtMobile.MaxLength = 7
        Me.txtMobile.Name = "txtMobile"
        Me.txtMobile.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMobile.Size = New System.Drawing.Size(253, 20)
        Me.txtMobile.TabIndex = 38
        '
        'txtEmail
        '
        Me.txtEmail.AcceptsReturn = True
        Me.txtEmail.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmail.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmail.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtEmail.Location = New System.Drawing.Point(116, 62)
        Me.txtEmail.MaxLength = 15
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmail.Size = New System.Drawing.Size(253, 20)
        Me.txtEmail.TabIndex = 37
        '
        'txtPhone
        '
        Me.txtPhone.AcceptsReturn = True
        Me.txtPhone.BackColor = System.Drawing.SystemColors.Window
        Me.txtPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPhone.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPhone.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPhone.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPhone.Location = New System.Drawing.Point(116, 22)
        Me.txtPhone.MaxLength = 0
        Me.txtPhone.Multiline = True
        Me.txtPhone.Name = "txtPhone"
        Me.txtPhone.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPhone.Size = New System.Drawing.Size(253, 19)
        Me.txtPhone.TabIndex = 35
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(5, 106)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(55, 14)
        Me.Label6.TabIndex = 108
        Me.Label6.Text = "Contact :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(370, 24)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(48, 14)
        Me.Label5.TabIndex = 107
        Me.Label5.Text = "Fax No :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(5, 64)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(46, 14)
        Me.Label3.TabIndex = 106
        Me.Label3.Text = "e-Mail :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(5, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(62, 14)
        Me.Label1.TabIndex = 105
        Me.Label1.Text = "Phone No:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label25_0
        '
        Me._Label25_0.AutoSize = True
        Me._Label25_0.BackColor = System.Drawing.SystemColors.Control
        Me._Label25_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label25_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label25_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label25.SetIndex(Me._Label25_0, CType(0, Short))
        Me._Label25_0.Location = New System.Drawing.Point(370, 64)
        Me._Label25_0.Name = "_Label25_0"
        Me._Label25_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label25_0.Size = New System.Drawing.Size(67, 14)
        Me._Label25_0.TabIndex = 104
        Me._Label25_0.Text = "Mobile No :"
        Me._Label25_0.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_SSTInfo_TabPage2
        '
        Me._SSTInfo_TabPage2.Controls.Add(Me.Frame6)
        Me._SSTInfo_TabPage2.Location = New System.Drawing.Point(4, 22)
        Me._SSTInfo_TabPage2.Name = "_SSTInfo_TabPage2"
        Me._SSTInfo_TabPage2.Size = New System.Drawing.Size(735, 148)
        Me._SSTInfo_TabPage2.TabIndex = 2
        Me._SSTInfo_TabPage2.Text = "Taxes"
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.txtServProvided)
        Me.Frame6.Controls.Add(Me.txtSrvRegnNo)
        Me.Frame6.Controls.Add(Me.txtTINNo)
        Me.Frame6.Controls.Add(Me.txtPan)
        Me.Frame6.Controls.Add(Me.txtCstNo)
        Me.Frame6.Controls.Add(Me.txtLSTNo)
        Me.Frame6.Controls.Add(Me.Label53)
        Me.Frame6.Controls.Add(Me.Label32)
        Me.Frame6.Controls.Add(Me.Label17)
        Me.Frame6.Controls.Add(Me.Label14)
        Me.Frame6.Controls.Add(Me.Label15)
        Me.Frame6.Controls.Add(Me.Label16)
        Me.Frame6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(4, 0)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(737, 145)
        Me.Frame6.TabIndex = 97
        Me.Frame6.TabStop = False
        '
        'txtServProvided
        '
        Me.txtServProvided.AcceptsReturn = True
        Me.txtServProvided.BackColor = System.Drawing.SystemColors.Window
        Me.txtServProvided.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtServProvided.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtServProvided.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtServProvided.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtServProvided.Location = New System.Drawing.Point(478, 104)
        Me.txtServProvided.MaxLength = 0
        Me.txtServProvided.Name = "txtServProvided"
        Me.txtServProvided.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtServProvided.Size = New System.Drawing.Size(253, 20)
        Me.txtServProvided.TabIndex = 45
        '
        'txtSrvRegnNo
        '
        Me.txtSrvRegnNo.AcceptsReturn = True
        Me.txtSrvRegnNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtSrvRegnNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSrvRegnNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSrvRegnNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSrvRegnNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSrvRegnNo.Location = New System.Drawing.Point(116, 104)
        Me.txtSrvRegnNo.MaxLength = 15
        Me.txtSrvRegnNo.Name = "txtSrvRegnNo"
        Me.txtSrvRegnNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSrvRegnNo.Size = New System.Drawing.Size(253, 20)
        Me.txtSrvRegnNo.TabIndex = 44
        '
        'txtTINNo
        '
        Me.txtTINNo.AcceptsReturn = True
        Me.txtTINNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtTINNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTINNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTINNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTINNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTINNo.Location = New System.Drawing.Point(478, 62)
        Me.txtTINNo.MaxLength = 15
        Me.txtTINNo.Name = "txtTINNo"
        Me.txtTINNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTINNo.Size = New System.Drawing.Size(253, 20)
        Me.txtTINNo.TabIndex = 43
        '
        'txtPan
        '
        Me.txtPan.AcceptsReturn = True
        Me.txtPan.BackColor = System.Drawing.SystemColors.Window
        Me.txtPan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPan.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPan.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPan.Location = New System.Drawing.Point(116, 62)
        Me.txtPan.MaxLength = 15
        Me.txtPan.Name = "txtPan"
        Me.txtPan.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPan.Size = New System.Drawing.Size(253, 20)
        Me.txtPan.TabIndex = 42
        '
        'txtCstNo
        '
        Me.txtCstNo.AcceptsReturn = True
        Me.txtCstNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtCstNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCstNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCstNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCstNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtCstNo.Location = New System.Drawing.Point(478, 22)
        Me.txtCstNo.MaxLength = 15
        Me.txtCstNo.Name = "txtCstNo"
        Me.txtCstNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCstNo.Size = New System.Drawing.Size(253, 20)
        Me.txtCstNo.TabIndex = 41
        '
        'txtLSTNo
        '
        Me.txtLSTNo.AcceptsReturn = True
        Me.txtLSTNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtLSTNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLSTNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLSTNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLSTNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtLSTNo.Location = New System.Drawing.Point(116, 22)
        Me.txtLSTNo.MaxLength = 15
        Me.txtLSTNo.Name = "txtLSTNo"
        Me.txtLSTNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLSTNo.Size = New System.Drawing.Size(253, 20)
        Me.txtLSTNo.TabIndex = 40
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.BackColor = System.Drawing.SystemColors.Control
        Me.Label53.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label53.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label53.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label53.Location = New System.Drawing.Point(372, 106)
        Me.Label53.Name = "Label53"
        Me.Label53.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label53.Size = New System.Drawing.Size(106, 14)
        Me.Label53.TabIndex = 171
        Me.Label53.Text = "Service Provided :"
        Me.Label53.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.BackColor = System.Drawing.Color.Transparent
        Me.Label32.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label32.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.ForeColor = System.Drawing.Color.Black
        Me.Label32.Location = New System.Drawing.Point(5, 106)
        Me.Label32.Name = "Label32"
        Me.Label32.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label32.Size = New System.Drawing.Size(92, 14)
        Me.Label32.TabIndex = 127
        Me.Label32.Text = "Service Tax No :"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(370, 64)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(47, 14)
        Me.Label17.TabIndex = 116
        Me.Label17.Text = "TIN No :"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(5, 24)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(51, 14)
        Me.Label14.TabIndex = 98
        Me.Label14.Text = "LST No :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(370, 24)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(52, 14)
        Me.Label15.TabIndex = 99
        Me.Label15.Text = "CST No :"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(5, 64)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(34, 14)
        Me.Label16.TabIndex = 100
        Me.Label16.Text = "PAN :"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_SSTInfo_TabPage3
        '
        Me._SSTInfo_TabPage3.Controls.Add(Me.Frame7)
        Me._SSTInfo_TabPage3.Location = New System.Drawing.Point(4, 22)
        Me._SSTInfo_TabPage3.Name = "_SSTInfo_TabPage3"
        Me._SSTInfo_TabPage3.Size = New System.Drawing.Size(735, 148)
        Me._SSTInfo_TabPage3.TabIndex = 3
        Me._SSTInfo_TabPage3.Text = "Excise Detail"
        '
        'Frame7
        '
        Me.Frame7.BackColor = System.Drawing.SystemColors.Control
        Me.Frame7.Controls.Add(Me.Frame13)
        Me.Frame7.Controls.Add(Me.txtDivision)
        Me.Frame7.Controls.Add(Me.txtRange)
        Me.Frame7.Controls.Add(Me.txtCommRate)
        Me.Frame7.Controls.Add(Me.txtECCNo)
        Me.Frame7.Controls.Add(Me.txtRegnNo)
        Me.Frame7.Controls.Add(Me._Label25_2)
        Me.Frame7.Controls.Add(Me.Label12)
        Me.Frame7.Controls.Add(Me.Label11)
        Me.Frame7.Controls.Add(Me.Label8)
        Me.Frame7.Controls.Add(Me.Label7)
        Me.Frame7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame7.Location = New System.Drawing.Point(4, 0)
        Me.Frame7.Name = "Frame7"
        Me.Frame7.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame7.Size = New System.Drawing.Size(737, 145)
        Me.Frame7.TabIndex = 109
        Me.Frame7.TabStop = False
        '
        'Frame13
        '
        Me.Frame13.BackColor = System.Drawing.SystemColors.Control
        Me.Frame13.Controls.Add(Me._optRegd_0)
        Me.Frame13.Controls.Add(Me._optRegd_1)
        Me.Frame13.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame13.Location = New System.Drawing.Point(476, 94)
        Me.Frame13.Name = "Frame13"
        Me.Frame13.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame13.Size = New System.Drawing.Size(253, 33)
        Me.Frame13.TabIndex = 160
        Me.Frame13.TabStop = False
        Me.Frame13.Text = "Dealer Status"
        '
        '_optRegd_0
        '
        Me._optRegd_0.BackColor = System.Drawing.SystemColors.Control
        Me._optRegd_0.Checked = True
        Me._optRegd_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optRegd_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optRegd_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optRegd.SetIndex(Me._optRegd_0, CType(0, Short))
        Me._optRegd_0.Location = New System.Drawing.Point(78, 12)
        Me._optRegd_0.Name = "_optRegd_0"
        Me._optRegd_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optRegd_0.Size = New System.Drawing.Size(67, 13)
        Me._optRegd_0.TabIndex = 51
        Me._optRegd_0.TabStop = True
        Me._optRegd_0.Text = "Regd."
        Me._optRegd_0.UseVisualStyleBackColor = False
        '
        '_optRegd_1
        '
        Me._optRegd_1.BackColor = System.Drawing.SystemColors.Control
        Me._optRegd_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optRegd_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optRegd_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optRegd.SetIndex(Me._optRegd_1, CType(1, Short))
        Me._optRegd_1.Location = New System.Drawing.Point(160, 12)
        Me._optRegd_1.Name = "_optRegd_1"
        Me._optRegd_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optRegd_1.Size = New System.Drawing.Size(87, 13)
        Me._optRegd_1.TabIndex = 52
        Me._optRegd_1.TabStop = True
        Me._optRegd_1.Text = "UnRegd."
        Me._optRegd_1.UseVisualStyleBackColor = False
        '
        'txtDivision
        '
        Me.txtDivision.AcceptsReturn = True
        Me.txtDivision.BackColor = System.Drawing.SystemColors.Window
        Me.txtDivision.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDivision.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDivision.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDivision.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDivision.Location = New System.Drawing.Point(116, 22)
        Me.txtDivision.MaxLength = 0
        Me.txtDivision.Multiline = True
        Me.txtDivision.Name = "txtDivision"
        Me.txtDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDivision.Size = New System.Drawing.Size(253, 19)
        Me.txtDivision.TabIndex = 46
        '
        'txtRange
        '
        Me.txtRange.AcceptsReturn = True
        Me.txtRange.BackColor = System.Drawing.SystemColors.Window
        Me.txtRange.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRange.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRange.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRange.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtRange.Location = New System.Drawing.Point(116, 62)
        Me.txtRange.MaxLength = 15
        Me.txtRange.Name = "txtRange"
        Me.txtRange.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRange.Size = New System.Drawing.Size(253, 20)
        Me.txtRange.TabIndex = 47
        '
        'txtCommRate
        '
        Me.txtCommRate.AcceptsReturn = True
        Me.txtCommRate.BackColor = System.Drawing.SystemColors.Window
        Me.txtCommRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCommRate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCommRate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCommRate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtCommRate.Location = New System.Drawing.Point(478, 62)
        Me.txtCommRate.MaxLength = 7
        Me.txtCommRate.Name = "txtCommRate"
        Me.txtCommRate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCommRate.Size = New System.Drawing.Size(253, 20)
        Me.txtCommRate.TabIndex = 50
        '
        'txtECCNo
        '
        Me.txtECCNo.AcceptsReturn = True
        Me.txtECCNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtECCNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtECCNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtECCNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtECCNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtECCNo.Location = New System.Drawing.Point(478, 22)
        Me.txtECCNo.MaxLength = 15
        Me.txtECCNo.Name = "txtECCNo"
        Me.txtECCNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtECCNo.Size = New System.Drawing.Size(253, 20)
        Me.txtECCNo.TabIndex = 49
        '
        'txtRegnNo
        '
        Me.txtRegnNo.AcceptsReturn = True
        Me.txtRegnNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtRegnNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegnNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRegnNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRegnNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtRegnNo.Location = New System.Drawing.Point(116, 104)
        Me.txtRegnNo.MaxLength = 15
        Me.txtRegnNo.Name = "txtRegnNo"
        Me.txtRegnNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRegnNo.Size = New System.Drawing.Size(253, 20)
        Me.txtRegnNo.TabIndex = 48
        '
        '_Label25_2
        '
        Me._Label25_2.AutoSize = True
        Me._Label25_2.BackColor = System.Drawing.SystemColors.Control
        Me._Label25_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label25_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label25_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label25.SetIndex(Me._Label25_2, CType(2, Short))
        Me._Label25_2.Location = New System.Drawing.Point(378, 64)
        Me._Label25_2.Name = "_Label25_2"
        Me._Label25_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label25_2.Size = New System.Drawing.Size(110, 14)
        Me._Label25_2.TabIndex = 114
        Me._Label25_2.Text = "Commissionerate:"
        Me._Label25_2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(5, 24)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(56, 14)
        Me.Label12.TabIndex = 113
        Me.Label12.Text = "Division :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(5, 64)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(47, 14)
        Me.Label11.TabIndex = 112
        Me.Label11.Text = "Range :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(370, 24)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(52, 14)
        Me.Label8.TabIndex = 111
        Me.Label8.Text = "ECC No :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(5, 106)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(57, 14)
        Me.Label7.TabIndex = 110
        Me.Label7.Text = "Reg. No. :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_SSTInfo_TabPage4
        '
        Me._SSTInfo_TabPage4.Controls.Add(Me.Frame12)
        Me._SSTInfo_TabPage4.Location = New System.Drawing.Point(4, 22)
        Me._SSTInfo_TabPage4.Name = "_SSTInfo_TabPage4"
        Me._SSTInfo_TabPage4.Size = New System.Drawing.Size(735, 148)
        Me._SSTInfo_TabPage4.TabIndex = 4
        Me._SSTInfo_TabPage4.Text = "TDS Details"
        '
        'Frame12
        '
        Me.Frame12.BackColor = System.Drawing.SystemColors.Control
        Me.Frame12.Controls.Add(Me.chkLowerDeduction)
        Me.Frame12.Controls.Add(Me.txtLDCertiNo)
        Me.Frame12.Controls.Add(Me.txtExptionCNo)
        Me.Frame12.Controls.Add(Me.cboCType)
        Me.Frame12.Controls.Add(Me.txtESIPer)
        Me.Frame12.Controls.Add(Me.txtSTDSPer)
        Me.Frame12.Controls.Add(Me.CboTDSCategory)
        Me.Frame12.Controls.Add(Me.txtTDSPer)
        Me.Frame12.Controls.Add(Me.txtSection)
        Me.Frame12.Controls.Add(Me._lblLabels_0)
        Me.Frame12.Controls.Add(Me._lblLabels_4)
        Me.Frame12.Controls.Add(Me._lblLabels_5)
        Me.Frame12.Controls.Add(Me.Label28)
        Me.Frame12.Controls.Add(Me.Label20)
        Me.Frame12.Controls.Add(Me.Label18)
        Me.Frame12.Controls.Add(Me.LblPayTerms)
        Me.Frame12.Controls.Add(Me.lblDueDays)
        Me.Frame12.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame12.Location = New System.Drawing.Point(3, 0)
        Me.Frame12.Name = "Frame12"
        Me.Frame12.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame12.Size = New System.Drawing.Size(737, 145)
        Me.Frame12.TabIndex = 149
        Me.Frame12.TabStop = False
        '
        'chkLowerDeduction
        '
        Me.chkLowerDeduction.BackColor = System.Drawing.SystemColors.Control
        Me.chkLowerDeduction.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkLowerDeduction.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkLowerDeduction.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkLowerDeduction.Location = New System.Drawing.Point(368, 84)
        Me.chkLowerDeduction.Name = "chkLowerDeduction"
        Me.chkLowerDeduction.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkLowerDeduction.Size = New System.Drawing.Size(135, 13)
        Me.chkLowerDeduction.TabIndex = 59
        Me.chkLowerDeduction.Text = "Lower Deduction"
        Me.chkLowerDeduction.UseVisualStyleBackColor = False
        '
        'txtLDCertiNo
        '
        Me.txtLDCertiNo.AcceptsReturn = True
        Me.txtLDCertiNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtLDCertiNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLDCertiNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLDCertiNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLDCertiNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLDCertiNo.Location = New System.Drawing.Point(114, 82)
        Me.txtLDCertiNo.MaxLength = 0
        Me.txtLDCertiNo.Name = "txtLDCertiNo"
        Me.txtLDCertiNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLDCertiNo.Size = New System.Drawing.Size(253, 20)
        Me.txtLDCertiNo.TabIndex = 58
        '
        'txtExptionCNo
        '
        Me.txtExptionCNo.AcceptsReturn = True
        Me.txtExptionCNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtExptionCNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtExptionCNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtExptionCNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExptionCNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtExptionCNo.Location = New System.Drawing.Point(478, 22)
        Me.txtExptionCNo.MaxLength = 0
        Me.txtExptionCNo.Name = "txtExptionCNo"
        Me.txtExptionCNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExptionCNo.Size = New System.Drawing.Size(253, 20)
        Me.txtExptionCNo.TabIndex = 55
        '
        'cboCType
        '
        Me.cboCType.BackColor = System.Drawing.SystemColors.Window
        Me.cboCType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCType.Location = New System.Drawing.Point(478, 52)
        Me.cboCType.Name = "cboCType"
        Me.cboCType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCType.Size = New System.Drawing.Size(253, 22)
        Me.cboCType.TabIndex = 57
        '
        'txtESIPer
        '
        Me.txtESIPer.AcceptsReturn = True
        Me.txtESIPer.BackColor = System.Drawing.SystemColors.Window
        Me.txtESIPer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtESIPer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtESIPer.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtESIPer.ForeColor = System.Drawing.Color.Blue
        Me.txtESIPer.Location = New System.Drawing.Point(640, 114)
        Me.txtESIPer.MaxLength = 0
        Me.txtESIPer.Name = "txtESIPer"
        Me.txtESIPer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtESIPer.Size = New System.Drawing.Size(49, 20)
        Me.txtESIPer.TabIndex = 62
        '
        'txtSTDSPer
        '
        Me.txtSTDSPer.AcceptsReturn = True
        Me.txtSTDSPer.BackColor = System.Drawing.SystemColors.Window
        Me.txtSTDSPer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSTDSPer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSTDSPer.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSTDSPer.ForeColor = System.Drawing.Color.Blue
        Me.txtSTDSPer.Location = New System.Drawing.Point(428, 114)
        Me.txtSTDSPer.MaxLength = 0
        Me.txtSTDSPer.Name = "txtSTDSPer"
        Me.txtSTDSPer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSTDSPer.Size = New System.Drawing.Size(49, 20)
        Me.txtSTDSPer.TabIndex = 61
        '
        'CboTDSCategory
        '
        Me.CboTDSCategory.BackColor = System.Drawing.SystemColors.Window
        Me.CboTDSCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.CboTDSCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboTDSCategory.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboTDSCategory.ForeColor = System.Drawing.SystemColors.WindowText
        Me.CboTDSCategory.Location = New System.Drawing.Point(116, 52)
        Me.CboTDSCategory.Name = "CboTDSCategory"
        Me.CboTDSCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CboTDSCategory.Size = New System.Drawing.Size(253, 22)
        Me.CboTDSCategory.TabIndex = 56
        '
        'txtTDSPer
        '
        Me.txtTDSPer.AcceptsReturn = True
        Me.txtTDSPer.BackColor = System.Drawing.SystemColors.Window
        Me.txtTDSPer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTDSPer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTDSPer.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTDSPer.ForeColor = System.Drawing.Color.Blue
        Me.txtTDSPer.Location = New System.Drawing.Point(116, 114)
        Me.txtTDSPer.MaxLength = 0
        Me.txtTDSPer.Name = "txtTDSPer"
        Me.txtTDSPer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTDSPer.Size = New System.Drawing.Size(49, 20)
        Me.txtTDSPer.TabIndex = 60
        '
        'txtSection
        '
        Me.txtSection.AcceptsReturn = True
        Me.txtSection.BackColor = System.Drawing.SystemColors.Window
        Me.txtSection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSection.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSection.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSection.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSection.Location = New System.Drawing.Point(116, 22)
        Me.txtSection.MaxLength = 0
        Me.txtSection.Name = "txtSection"
        Me.txtSection.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSection.Size = New System.Drawing.Size(225, 20)
        Me.txtSection.TabIndex = 53
        '
        '_lblLabels_0
        '
        Me._lblLabels_0.AutoSize = True
        Me._lblLabels_0.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_0, CType(0, Short))
        Me._lblLabels_0.Location = New System.Drawing.Point(6, 84)
        Me._lblLabels_0.Name = "_lblLabels_0"
        Me._lblLabels_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_0.Size = New System.Drawing.Size(126, 14)
        Me._lblLabels_0.TabIndex = 181
        Me._lblLabels_0.Text = "Lower Ded. Certi. No :"
        Me._lblLabels_0.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_4
        '
        Me._lblLabels_4.AutoSize = True
        Me._lblLabels_4.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_4, CType(4, Short))
        Me._lblLabels_4.Location = New System.Drawing.Point(370, 24)
        Me._lblLabels_4.Name = "_lblLabels_4"
        Me._lblLabels_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_4.Size = New System.Drawing.Size(121, 14)
        Me._lblLabels_4.TabIndex = 156
        Me._lblLabels_4.Text = "Exemption Certi. No :"
        Me._lblLabels_4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_5
        '
        Me._lblLabels_5.AutoSize = True
        Me._lblLabels_5.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_5, CType(5, Short))
        Me._lblLabels_5.Location = New System.Drawing.Point(5, 24)
        Me._lblLabels_5.Name = "_lblLabels_5"
        Me._lblLabels_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_5.Size = New System.Drawing.Size(54, 14)
        Me._lblLabels_5.TabIndex = 155
        Me._lblLabels_5.Text = "Section :"
        Me._lblLabels_5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.SystemColors.Control
        Me.Label28.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label28.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label28.Location = New System.Drawing.Point(370, 54)
        Me.Label28.Name = "Label28"
        Me.Label28.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label28.Size = New System.Drawing.Size(39, 14)
        Me.Label28.TabIndex = 154
        Me.Label28.Text = "Type :"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(531, 116)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(41, 14)
        Me.Label20.TabIndex = 153
        Me.Label20.Text = "ESI % :"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(306, 116)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(53, 14)
        Me.Label18.TabIndex = 152
        Me.Label18.Text = "STDS % :"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LblPayTerms
        '
        Me.LblPayTerms.AutoSize = True
        Me.LblPayTerms.BackColor = System.Drawing.SystemColors.Control
        Me.LblPayTerms.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblPayTerms.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPayTerms.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblPayTerms.Location = New System.Drawing.Point(5, 54)
        Me.LblPayTerms.Name = "LblPayTerms"
        Me.LblPayTerms.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblPayTerms.Size = New System.Drawing.Size(58, 14)
        Me.LblPayTerms.TabIndex = 151
        Me.LblPayTerms.Text = "TDS Cat. :"
        Me.LblPayTerms.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblDueDays
        '
        Me.lblDueDays.AutoSize = True
        Me.lblDueDays.BackColor = System.Drawing.SystemColors.Control
        Me.lblDueDays.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDueDays.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDueDays.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDueDays.Location = New System.Drawing.Point(5, 116)
        Me.lblDueDays.Name = "lblDueDays"
        Me.lblDueDays.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDueDays.Size = New System.Drawing.Size(46, 14)
        Me.lblDueDays.TabIndex = 150
        Me.lblDueDays.Text = "TDS % :"
        Me.lblDueDays.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_SSTInfo_TabPage5
        '
        Me._SSTInfo_TabPage5.Controls.Add(Me.Frame9)
        Me._SSTInfo_TabPage5.Controls.Add(Me.Frame10)
        Me._SSTInfo_TabPage5.Location = New System.Drawing.Point(4, 22)
        Me._SSTInfo_TabPage5.Name = "_SSTInfo_TabPage5"
        Me._SSTInfo_TabPage5.Size = New System.Drawing.Size(735, 148)
        Me._SSTInfo_TabPage5.TabIndex = 5
        Me._SSTInfo_TabPage5.Text = "S.T. Form Detail"
        '
        'Frame9
        '
        Me.Frame9.BackColor = System.Drawing.SystemColors.Control
        Me.Frame9.Controls.Add(Me.txtPurchaseSTRecd)
        Me.Frame9.Controls.Add(Me.txtPurchaseSTDue)
        Me.Frame9.Controls.Add(Me.Label34)
        Me.Frame9.Controls.Add(Me.Label33)
        Me.Frame9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame9.Location = New System.Drawing.Point(4, 0)
        Me.Frame9.Name = "Frame9"
        Me.Frame9.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame9.Size = New System.Drawing.Size(279, 145)
        Me.Frame9.TabIndex = 121
        Me.Frame9.TabStop = False
        Me.Frame9.Text = "Purchase"
        '
        'txtPurchaseSTRecd
        '
        Me.txtPurchaseSTRecd.AcceptsReturn = True
        Me.txtPurchaseSTRecd.BackColor = System.Drawing.SystemColors.Window
        Me.txtPurchaseSTRecd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPurchaseSTRecd.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPurchaseSTRecd.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPurchaseSTRecd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPurchaseSTRecd.Location = New System.Drawing.Point(104, 46)
        Me.txtPurchaseSTRecd.MaxLength = 15
        Me.txtPurchaseSTRecd.Name = "txtPurchaseSTRecd"
        Me.txtPurchaseSTRecd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPurchaseSTRecd.Size = New System.Drawing.Size(171, 20)
        Me.txtPurchaseSTRecd.TabIndex = 63
        '
        'txtPurchaseSTDue
        '
        Me.txtPurchaseSTDue.AcceptsReturn = True
        Me.txtPurchaseSTDue.BackColor = System.Drawing.SystemColors.Window
        Me.txtPurchaseSTDue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPurchaseSTDue.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPurchaseSTDue.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPurchaseSTDue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPurchaseSTDue.Location = New System.Drawing.Point(104, 80)
        Me.txtPurchaseSTDue.MaxLength = 15
        Me.txtPurchaseSTDue.Name = "txtPurchaseSTDue"
        Me.txtPurchaseSTDue.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPurchaseSTDue.Size = New System.Drawing.Size(171, 20)
        Me.txtPurchaseSTDue.TabIndex = 64
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.BackColor = System.Drawing.Color.Transparent
        Me.Label34.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label34.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.ForeColor = System.Drawing.Color.Black
        Me.Label34.Location = New System.Drawing.Point(37, 82)
        Me.Label34.Name = "Label34"
        Me.Label34.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label34.Size = New System.Drawing.Size(66, 14)
        Me.Label34.TabIndex = 123
        Me.Label34.Text = "Form Due :"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.BackColor = System.Drawing.Color.Transparent
        Me.Label33.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label33.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.Color.Black
        Me.Label33.Location = New System.Drawing.Point(6, 48)
        Me.Label33.Name = "Label33"
        Me.Label33.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label33.Size = New System.Drawing.Size(95, 14)
        Me.Label33.TabIndex = 122
        Me.Label33.Text = "Form Received :"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame10
        '
        Me.Frame10.BackColor = System.Drawing.SystemColors.Control
        Me.Frame10.Controls.Add(Me.txtSaleSTDue)
        Me.Frame10.Controls.Add(Me.txtSaleSTRecd)
        Me.Frame10.Controls.Add(Me.Label30)
        Me.Frame10.Controls.Add(Me.Label29)
        Me.Frame10.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame10.Location = New System.Drawing.Point(286, 0)
        Me.Frame10.Name = "Frame10"
        Me.Frame10.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame10.Size = New System.Drawing.Size(279, 145)
        Me.Frame10.TabIndex = 124
        Me.Frame10.TabStop = False
        Me.Frame10.Text = "Sale"
        '
        'txtSaleSTDue
        '
        Me.txtSaleSTDue.AcceptsReturn = True
        Me.txtSaleSTDue.BackColor = System.Drawing.SystemColors.Window
        Me.txtSaleSTDue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSaleSTDue.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSaleSTDue.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSaleSTDue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSaleSTDue.Location = New System.Drawing.Point(104, 80)
        Me.txtSaleSTDue.MaxLength = 15
        Me.txtSaleSTDue.Name = "txtSaleSTDue"
        Me.txtSaleSTDue.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSaleSTDue.Size = New System.Drawing.Size(171, 20)
        Me.txtSaleSTDue.TabIndex = 66
        '
        'txtSaleSTRecd
        '
        Me.txtSaleSTRecd.AcceptsReturn = True
        Me.txtSaleSTRecd.BackColor = System.Drawing.SystemColors.Window
        Me.txtSaleSTRecd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSaleSTRecd.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSaleSTRecd.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSaleSTRecd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSaleSTRecd.Location = New System.Drawing.Point(104, 46)
        Me.txtSaleSTRecd.MaxLength = 15
        Me.txtSaleSTRecd.Name = "txtSaleSTRecd"
        Me.txtSaleSTRecd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSaleSTRecd.Size = New System.Drawing.Size(171, 20)
        Me.txtSaleSTRecd.TabIndex = 65
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.BackColor = System.Drawing.Color.Transparent
        Me.Label30.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label30.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.ForeColor = System.Drawing.Color.Black
        Me.Label30.Location = New System.Drawing.Point(6, 48)
        Me.Label30.Name = "Label30"
        Me.Label30.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label30.Size = New System.Drawing.Size(95, 14)
        Me.Label30.TabIndex = 126
        Me.Label30.Text = "Form Received :"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.Color.Transparent
        Me.Label29.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label29.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.Color.Black
        Me.Label29.Location = New System.Drawing.Point(37, 82)
        Me.Label29.Name = "Label29"
        Me.Label29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label29.Size = New System.Drawing.Size(66, 14)
        Me.Label29.TabIndex = 125
        Me.Label29.Text = "Form Due :"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_SSTInfo_TabPage6
        '
        Me._SSTInfo_TabPage6.Controls.Add(Me.Frame11)
        Me._SSTInfo_TabPage6.Location = New System.Drawing.Point(4, 22)
        Me._SSTInfo_TabPage6.Name = "_SSTInfo_TabPage6"
        Me._SSTInfo_TabPage6.Size = New System.Drawing.Size(735, 148)
        Me._SSTInfo_TabPage6.TabIndex = 6
        Me._SSTInfo_TabPage6.Text = "Export Details"
        '
        'Frame11
        '
        Me.Frame11.BackColor = System.Drawing.SystemColors.Control
        Me.Frame11.Controls.Add(Me.txtExportPaymetTerms)
        Me.Frame11.Controls.Add(Me.txtFinalDest)
        Me.Frame11.Controls.Add(Me.txtCarriage)
        Me.Frame11.Controls.Add(Me.txtDischargePort)
        Me.Frame11.Controls.Add(Me.txtLoadingPort)
        Me.Frame11.Controls.Add(Me.txtBuyerName)
        Me.Frame11.Controls.Add(Me.Label36)
        Me.Frame11.Controls.Add(Me._Label25_4)
        Me.Frame11.Controls.Add(Me.Label38)
        Me.Frame11.Controls.Add(Me.Label37)
        Me.Frame11.Controls.Add(Me._Label25_3)
        Me.Frame11.Controls.Add(Me.Label35)
        Me.Frame11.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame11.Location = New System.Drawing.Point(4, 0)
        Me.Frame11.Name = "Frame11"
        Me.Frame11.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame11.Size = New System.Drawing.Size(735, 145)
        Me.Frame11.TabIndex = 128
        Me.Frame11.TabStop = False
        '
        'txtExportPaymetTerms
        '
        Me.txtExportPaymetTerms.AcceptsReturn = True
        Me.txtExportPaymetTerms.BackColor = System.Drawing.SystemColors.Window
        Me.txtExportPaymetTerms.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtExportPaymetTerms.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtExportPaymetTerms.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExportPaymetTerms.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtExportPaymetTerms.Location = New System.Drawing.Point(488, 74)
        Me.txtExportPaymetTerms.MaxLength = 15
        Me.txtExportPaymetTerms.Name = "txtExportPaymetTerms"
        Me.txtExportPaymetTerms.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExportPaymetTerms.Size = New System.Drawing.Size(240, 20)
        Me.txtExportPaymetTerms.TabIndex = 73
        '
        'txtFinalDest
        '
        Me.txtFinalDest.AcceptsReturn = True
        Me.txtFinalDest.BackColor = System.Drawing.SystemColors.Window
        Me.txtFinalDest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFinalDest.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFinalDest.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFinalDest.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtFinalDest.Location = New System.Drawing.Point(488, 48)
        Me.txtFinalDest.MaxLength = 7
        Me.txtFinalDest.Name = "txtFinalDest"
        Me.txtFinalDest.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFinalDest.Size = New System.Drawing.Size(240, 20)
        Me.txtFinalDest.TabIndex = 72
        '
        'txtCarriage
        '
        Me.txtCarriage.AcceptsReturn = True
        Me.txtCarriage.BackColor = System.Drawing.SystemColors.Window
        Me.txtCarriage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCarriage.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCarriage.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCarriage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtCarriage.Location = New System.Drawing.Point(112, 48)
        Me.txtCarriage.MaxLength = 15
        Me.txtCarriage.Name = "txtCarriage"
        Me.txtCarriage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCarriage.Size = New System.Drawing.Size(240, 20)
        Me.txtCarriage.TabIndex = 69
        '
        'txtDischargePort
        '
        Me.txtDischargePort.AcceptsReturn = True
        Me.txtDischargePort.BackColor = System.Drawing.SystemColors.Window
        Me.txtDischargePort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDischargePort.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDischargePort.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDischargePort.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDischargePort.Location = New System.Drawing.Point(488, 22)
        Me.txtDischargePort.MaxLength = 7
        Me.txtDischargePort.Name = "txtDischargePort"
        Me.txtDischargePort.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDischargePort.Size = New System.Drawing.Size(240, 20)
        Me.txtDischargePort.TabIndex = 71
        '
        'txtLoadingPort
        '
        Me.txtLoadingPort.AcceptsReturn = True
        Me.txtLoadingPort.BackColor = System.Drawing.SystemColors.Window
        Me.txtLoadingPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLoadingPort.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLoadingPort.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLoadingPort.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtLoadingPort.Location = New System.Drawing.Point(112, 74)
        Me.txtLoadingPort.MaxLength = 15
        Me.txtLoadingPort.Name = "txtLoadingPort"
        Me.txtLoadingPort.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLoadingPort.Size = New System.Drawing.Size(240, 20)
        Me.txtLoadingPort.TabIndex = 70
        '
        'txtBuyerName
        '
        Me.txtBuyerName.AcceptsReturn = True
        Me.txtBuyerName.BackColor = System.Drawing.SystemColors.Window
        Me.txtBuyerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBuyerName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBuyerName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBuyerName.ForeColor = System.Drawing.Color.Blue
        Me.txtBuyerName.Location = New System.Drawing.Point(112, 22)
        Me.txtBuyerName.MaxLength = 0
        Me.txtBuyerName.Name = "txtBuyerName"
        Me.txtBuyerName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBuyerName.Size = New System.Drawing.Size(240, 20)
        Me.txtBuyerName.TabIndex = 67
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.BackColor = System.Drawing.Color.Transparent
        Me.Label36.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label36.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label36.Location = New System.Drawing.Point(373, 76)
        Me.Label36.Name = "Label36"
        Me.Label36.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label36.Size = New System.Drawing.Size(111, 14)
        Me.Label36.TabIndex = 134
        Me.Label36.Text = "Export Pay. Terms :"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label25_4
        '
        Me._Label25_4.AutoSize = True
        Me._Label25_4.BackColor = System.Drawing.SystemColors.Control
        Me._Label25_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label25_4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label25_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label25.SetIndex(Me._Label25_4, CType(4, Short))
        Me._Label25_4.Location = New System.Drawing.Point(378, 50)
        Me._Label25_4.Name = "_Label25_4"
        Me._Label25_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label25_4.Size = New System.Drawing.Size(103, 14)
        Me._Label25_4.TabIndex = 133
        Me._Label25_4.Text = "Final Destination :"
        Me._Label25_4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.BackColor = System.Drawing.Color.Transparent
        Me.Label38.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label38.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label38.Location = New System.Drawing.Point(5, 50)
        Me.Label38.Name = "Label38"
        Me.Label38.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label38.Size = New System.Drawing.Size(83, 14)
        Me.Label38.TabIndex = 132
        Me.Label38.Text = "Pre-Carriage :"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.BackColor = System.Drawing.Color.Transparent
        Me.Label37.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label37.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label37.Location = New System.Drawing.Point(5, 76)
        Me.Label37.Name = "Label37"
        Me.Label37.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label37.Size = New System.Drawing.Size(97, 14)
        Me.Label37.TabIndex = 131
        Me.Label37.Text = "Port of Loading :"
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label25_3
        '
        Me._Label25_3.AutoSize = True
        Me._Label25_3.BackColor = System.Drawing.SystemColors.Control
        Me._Label25_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label25_3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label25_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label25.SetIndex(Me._Label25_3, CType(3, Short))
        Me._Label25_3.Location = New System.Drawing.Point(378, 24)
        Me._Label25_3.Name = "_Label25_3"
        Me._Label25_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label25_3.Size = New System.Drawing.Size(108, 14)
        Me._Label25_3.TabIndex = 130
        Me._Label25_3.Text = "Port of Discharge :"
        Me._Label25_3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.BackColor = System.Drawing.Color.Transparent
        Me.Label35.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label35.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.ForeColor = System.Drawing.Color.Black
        Me.Label35.Location = New System.Drawing.Point(5, 24)
        Me.Label35.Name = "Label35"
        Me.Label35.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label35.Size = New System.Drawing.Size(79, 14)
        Me.Label35.TabIndex = 129
        Me.Label35.Text = "Buyer Name :"
        Me.Label35.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_SSTInfo_TabPage7
        '
        Me._SSTInfo_TabPage7.Controls.Add(Me.Frame14)
        Me._SSTInfo_TabPage7.Location = New System.Drawing.Point(4, 22)
        Me._SSTInfo_TabPage7.Name = "_SSTInfo_TabPage7"
        Me._SSTInfo_TabPage7.Size = New System.Drawing.Size(735, 148)
        Me._SSTInfo_TabPage7.TabIndex = 7
        Me._SSTInfo_TabPage7.Text = "Bank Details"
        '
        'Frame14
        '
        Me.Frame14.BackColor = System.Drawing.SystemColors.Control
        Me.Frame14.Controls.Add(Me.txtBankName)
        Me.Frame14.Controls.Add(Me.txtSwitCode)
        Me.Frame14.Controls.Add(Me.txtIFSCCode)
        Me.Frame14.Controls.Add(Me.txtBankBranch)
        Me.Frame14.Controls.Add(Me.txtBankAccountNo)
        Me.Frame14.Controls.Add(Me._Label25_5)
        Me.Frame14.Controls.Add(Me.Label52)
        Me.Frame14.Controls.Add(Me.Label54)
        Me.Frame14.Controls.Add(Me._Label25_6)
        Me.Frame14.Controls.Add(Me.Label55)
        Me.Frame14.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame14.Location = New System.Drawing.Point(3, 0)
        Me.Frame14.Name = "Frame14"
        Me.Frame14.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame14.Size = New System.Drawing.Size(735, 145)
        Me.Frame14.TabIndex = 184
        Me.Frame14.TabStop = False
        '
        'txtBankName
        '
        Me.txtBankName.AcceptsReturn = True
        Me.txtBankName.BackColor = System.Drawing.SystemColors.Window
        Me.txtBankName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBankName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBankName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtBankName.Location = New System.Drawing.Point(488, 48)
        Me.txtBankName.MaxLength = 7
        Me.txtBankName.Name = "txtBankName"
        Me.txtBankName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBankName.Size = New System.Drawing.Size(240, 20)
        Me.txtBankName.TabIndex = 189
        '
        'txtSwitCode
        '
        Me.txtSwitCode.AcceptsReturn = True
        Me.txtSwitCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtSwitCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSwitCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSwitCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSwitCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSwitCode.Location = New System.Drawing.Point(112, 48)
        Me.txtSwitCode.MaxLength = 15
        Me.txtSwitCode.Name = "txtSwitCode"
        Me.txtSwitCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSwitCode.Size = New System.Drawing.Size(240, 20)
        Me.txtSwitCode.TabIndex = 188
        '
        'txtIFSCCode
        '
        Me.txtIFSCCode.AcceptsReturn = True
        Me.txtIFSCCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtIFSCCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIFSCCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIFSCCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIFSCCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtIFSCCode.Location = New System.Drawing.Point(488, 22)
        Me.txtIFSCCode.MaxLength = 7
        Me.txtIFSCCode.Name = "txtIFSCCode"
        Me.txtIFSCCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIFSCCode.Size = New System.Drawing.Size(240, 20)
        Me.txtIFSCCode.TabIndex = 187
        '
        'txtBankBranch
        '
        Me.txtBankBranch.AcceptsReturn = True
        Me.txtBankBranch.BackColor = System.Drawing.SystemColors.Window
        Me.txtBankBranch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBankBranch.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBankBranch.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankBranch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtBankBranch.Location = New System.Drawing.Point(112, 74)
        Me.txtBankBranch.MaxLength = 15
        Me.txtBankBranch.Name = "txtBankBranch"
        Me.txtBankBranch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBankBranch.Size = New System.Drawing.Size(240, 20)
        Me.txtBankBranch.TabIndex = 186
        '
        'txtBankAccountNo
        '
        Me.txtBankAccountNo.AcceptsReturn = True
        Me.txtBankAccountNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtBankAccountNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBankAccountNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBankAccountNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankAccountNo.ForeColor = System.Drawing.Color.Blue
        Me.txtBankAccountNo.Location = New System.Drawing.Point(112, 22)
        Me.txtBankAccountNo.MaxLength = 0
        Me.txtBankAccountNo.Name = "txtBankAccountNo"
        Me.txtBankAccountNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBankAccountNo.Size = New System.Drawing.Size(240, 20)
        Me.txtBankAccountNo.TabIndex = 185
        '
        '_Label25_5
        '
        Me._Label25_5.AutoSize = True
        Me._Label25_5.BackColor = System.Drawing.SystemColors.Control
        Me._Label25_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label25_5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label25_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label25.SetIndex(Me._Label25_5, CType(5, Short))
        Me._Label25_5.Location = New System.Drawing.Point(411, 50)
        Me._Label25_5.Name = "_Label25_5"
        Me._Label25_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label25_5.Size = New System.Drawing.Size(74, 14)
        Me._Label25_5.TabIndex = 194
        Me._Label25_5.Text = "Bank Name :"
        Me._Label25_5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label52
        '
        Me.Label52.AutoSize = True
        Me.Label52.BackColor = System.Drawing.Color.Transparent
        Me.Label52.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label52.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label52.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label52.Location = New System.Drawing.Point(42, 50)
        Me.Label52.Name = "Label52"
        Me.Label52.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label52.Size = New System.Drawing.Size(73, 14)
        Me.Label52.TabIndex = 193
        Me.Label52.Text = "Swift Code :"
        Me.Label52.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label54
        '
        Me.Label54.AutoSize = True
        Me.Label54.BackColor = System.Drawing.Color.Transparent
        Me.Label54.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label54.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label54.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label54.Location = New System.Drawing.Point(63, 76)
        Me.Label54.Name = "Label54"
        Me.Label54.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label54.Size = New System.Drawing.Size(51, 14)
        Me.Label54.TabIndex = 192
        Me.Label54.Text = "Branch :"
        Me.Label54.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label25_6
        '
        Me._Label25_6.AutoSize = True
        Me._Label25_6.BackColor = System.Drawing.SystemColors.Control
        Me._Label25_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label25_6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label25_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label25.SetIndex(Me._Label25_6, CType(6, Short))
        Me._Label25_6.Location = New System.Drawing.Point(416, 24)
        Me._Label25_6.Name = "_Label25_6"
        Me._Label25_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label25_6.Size = New System.Drawing.Size(69, 14)
        Me._Label25_6.TabIndex = 191
        Me._Label25_6.Text = "IFSC Code :"
        Me._Label25_6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label55
        '
        Me.Label55.AutoSize = True
        Me.Label55.BackColor = System.Drawing.Color.Transparent
        Me.Label55.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label55.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label55.ForeColor = System.Drawing.Color.Black
        Me.Label55.Location = New System.Drawing.Point(9, 24)
        Me.Label55.Name = "Label55"
        Me.Label55.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label55.Size = New System.Drawing.Size(105, 14)
        Me.Label55.TabIndex = 190
        Me.Label55.Text = "Account Number :"
        Me.Label55.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_SSTInfo_TabPage8
        '
        Me._SSTInfo_TabPage8.Controls.Add(Me.FraGSTClass)
        Me._SSTInfo_TabPage8.Controls.Add(Me.Frame15)
        Me._SSTInfo_TabPage8.Location = New System.Drawing.Point(4, 22)
        Me._SSTInfo_TabPage8.Name = "_SSTInfo_TabPage8"
        Me._SSTInfo_TabPage8.Size = New System.Drawing.Size(735, 148)
        Me._SSTInfo_TabPage8.TabIndex = 8
        Me._SSTInfo_TabPage8.Text = "GST Details"
        '
        'FraGSTClass
        '
        Me.FraGSTClass.BackColor = System.Drawing.SystemColors.Control
        Me.FraGSTClass.Controls.Add(Me._optGSTClassification_0)
        Me.FraGSTClass.Controls.Add(Me._optGSTClassification_1)
        Me.FraGSTClass.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraGSTClass.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraGSTClass.Location = New System.Drawing.Point(376, 0)
        Me.FraGSTClass.Name = "FraGSTClass"
        Me.FraGSTClass.Padding = New System.Windows.Forms.Padding(0)
        Me.FraGSTClass.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraGSTClass.Size = New System.Drawing.Size(357, 145)
        Me.FraGSTClass.TabIndex = 195
        Me.FraGSTClass.TabStop = False
        Me.FraGSTClass.Text = "Classification of taxability of Supply"
        '
        '_optGSTClassification_0
        '
        Me._optGSTClassification_0.BackColor = System.Drawing.SystemColors.Control
        Me._optGSTClassification_0.Checked = True
        Me._optGSTClassification_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optGSTClassification_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optGSTClassification_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optGSTClassification.SetIndex(Me._optGSTClassification_0, CType(0, Short))
        Me._optGSTClassification_0.Location = New System.Drawing.Point(78, 40)
        Me._optGSTClassification_0.Name = "_optGSTClassification_0"
        Me._optGSTClassification_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optGSTClassification_0.Size = New System.Drawing.Size(151, 19)
        Me._optGSTClassification_0.TabIndex = 197
        Me._optGSTClassification_0.TabStop = True
        Me._optGSTClassification_0.Text = "Forward Charge"
        Me._optGSTClassification_0.UseVisualStyleBackColor = False
        '
        '_optGSTClassification_1
        '
        Me._optGSTClassification_1.BackColor = System.Drawing.SystemColors.Control
        Me._optGSTClassification_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optGSTClassification_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optGSTClassification_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optGSTClassification.SetIndex(Me._optGSTClassification_1, CType(1, Short))
        Me._optGSTClassification_1.Location = New System.Drawing.Point(78, 64)
        Me._optGSTClassification_1.Name = "_optGSTClassification_1"
        Me._optGSTClassification_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optGSTClassification_1.Size = New System.Drawing.Size(171, 19)
        Me._optGSTClassification_1.TabIndex = 196
        Me._optGSTClassification_1.TabStop = True
        Me._optGSTClassification_1.Text = "Reverse Charge"
        Me._optGSTClassification_1.UseVisualStyleBackColor = False
        '
        'Frame15
        '
        Me.Frame15.BackColor = System.Drawing.SystemColors.Control
        Me.Frame15.Controls.Add(Me.FraGSTStatus)
        Me.Frame15.Controls.Add(Me.txtGSTRegnNo)
        Me.Frame15.Controls.Add(Me.Label58)
        Me.Frame15.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame15.Location = New System.Drawing.Point(0, 0)
        Me.Frame15.Name = "Frame15"
        Me.Frame15.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame15.Size = New System.Drawing.Size(375, 145)
        Me.Frame15.TabIndex = 198
        Me.Frame15.TabStop = False
        '
        'FraGSTStatus
        '
        Me.FraGSTStatus.BackColor = System.Drawing.SystemColors.Control
        Me.FraGSTStatus.Controls.Add(Me._optGSTRegd_0)
        Me.FraGSTStatus.Controls.Add(Me._optGSTRegd_1)
        Me.FraGSTStatus.Controls.Add(Me._optGSTRegd_2)
        Me.FraGSTStatus.Controls.Add(Me._optGSTRegd_3)
        Me.FraGSTStatus.Controls.Add(Me._optGSTRegd_4)
        Me.FraGSTStatus.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraGSTStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraGSTStatus.Location = New System.Drawing.Point(116, 36)
        Me.FraGSTStatus.Name = "FraGSTStatus"
        Me.FraGSTStatus.Padding = New System.Windows.Forms.Padding(0)
        Me.FraGSTStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraGSTStatus.Size = New System.Drawing.Size(255, 105)
        Me.FraGSTStatus.TabIndex = 200
        Me.FraGSTStatus.TabStop = False
        Me.FraGSTStatus.Text = "GST Status"
        '
        '_optGSTRegd_0
        '
        Me._optGSTRegd_0.BackColor = System.Drawing.SystemColors.Control
        Me._optGSTRegd_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optGSTRegd_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optGSTRegd_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optGSTRegd.SetIndex(Me._optGSTRegd_0, CType(0, Short))
        Me._optGSTRegd_0.Location = New System.Drawing.Point(78, 16)
        Me._optGSTRegd_0.Name = "_optGSTRegd_0"
        Me._optGSTRegd_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optGSTRegd_0.Size = New System.Drawing.Size(67, 18)
        Me._optGSTRegd_0.TabIndex = 205
        Me._optGSTRegd_0.TabStop = True
        Me._optGSTRegd_0.Text = "Regd."
        Me._optGSTRegd_0.UseVisualStyleBackColor = False
        '
        '_optGSTRegd_1
        '
        Me._optGSTRegd_1.BackColor = System.Drawing.SystemColors.Control
        Me._optGSTRegd_1.Checked = True
        Me._optGSTRegd_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optGSTRegd_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optGSTRegd_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optGSTRegd.SetIndex(Me._optGSTRegd_1, CType(1, Short))
        Me._optGSTRegd_1.Location = New System.Drawing.Point(78, 32)
        Me._optGSTRegd_1.Name = "_optGSTRegd_1"
        Me._optGSTRegd_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optGSTRegd_1.Size = New System.Drawing.Size(87, 18)
        Me._optGSTRegd_1.TabIndex = 204
        Me._optGSTRegd_1.TabStop = True
        Me._optGSTRegd_1.Text = "UnRegd."
        Me._optGSTRegd_1.UseVisualStyleBackColor = False
        '
        '_optGSTRegd_2
        '
        Me._optGSTRegd_2.BackColor = System.Drawing.SystemColors.Control
        Me._optGSTRegd_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optGSTRegd_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optGSTRegd_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optGSTRegd.SetIndex(Me._optGSTRegd_2, CType(2, Short))
        Me._optGSTRegd_2.Location = New System.Drawing.Point(78, 48)
        Me._optGSTRegd_2.Name = "_optGSTRegd_2"
        Me._optGSTRegd_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optGSTRegd_2.Size = New System.Drawing.Size(87, 18)
        Me._optGSTRegd_2.TabIndex = 203
        Me._optGSTRegd_2.TabStop = True
        Me._optGSTRegd_2.Text = "Exempted"
        Me._optGSTRegd_2.UseVisualStyleBackColor = False
        '
        '_optGSTRegd_3
        '
        Me._optGSTRegd_3.BackColor = System.Drawing.SystemColors.Control
        Me._optGSTRegd_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._optGSTRegd_3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optGSTRegd_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optGSTRegd.SetIndex(Me._optGSTRegd_3, CType(3, Short))
        Me._optGSTRegd_3.Location = New System.Drawing.Point(78, 64)
        Me._optGSTRegd_3.Name = "_optGSTRegd_3"
        Me._optGSTRegd_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optGSTRegd_3.Size = New System.Drawing.Size(87, 18)
        Me._optGSTRegd_3.TabIndex = 202
        Me._optGSTRegd_3.TabStop = True
        Me._optGSTRegd_3.Text = "Foreign"
        Me._optGSTRegd_3.UseVisualStyleBackColor = False
        '
        '_optGSTRegd_4
        '
        Me._optGSTRegd_4.BackColor = System.Drawing.SystemColors.Control
        Me._optGSTRegd_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._optGSTRegd_4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optGSTRegd_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optGSTRegd.SetIndex(Me._optGSTRegd_4, CType(4, Short))
        Me._optGSTRegd_4.Location = New System.Drawing.Point(78, 82)
        Me._optGSTRegd_4.Name = "_optGSTRegd_4"
        Me._optGSTRegd_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optGSTRegd_4.Size = New System.Drawing.Size(133, 18)
        Me._optGSTRegd_4.TabIndex = 201
        Me._optGSTRegd_4.TabStop = True
        Me._optGSTRegd_4.Text = "Composit Dealer"
        Me._optGSTRegd_4.UseVisualStyleBackColor = False
        '
        'txtGSTRegnNo
        '
        Me.txtGSTRegnNo.AcceptsReturn = True
        Me.txtGSTRegnNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtGSTRegnNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGSTRegnNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGSTRegnNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGSTRegnNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtGSTRegnNo.Location = New System.Drawing.Point(116, 11)
        Me.txtGSTRegnNo.MaxLength = 15
        Me.txtGSTRegnNo.Name = "txtGSTRegnNo"
        Me.txtGSTRegnNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGSTRegnNo.Size = New System.Drawing.Size(253, 20)
        Me.txtGSTRegnNo.TabIndex = 199
        '
        'Label58
        '
        Me.Label58.AutoSize = True
        Me.Label58.BackColor = System.Drawing.Color.Transparent
        Me.Label58.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label58.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label58.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label58.Location = New System.Drawing.Point(23, 13)
        Me.Label58.Name = "Label58"
        Me.Label58.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label58.Size = New System.Drawing.Size(82, 14)
        Me.Label58.TabIndex = 206
        Me.Label58.Text = "GST Reg. No. :"
        Me.Label58.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(8, 380)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(63, 14)
        Me.Label19.TabIndex = 137
        Me.Label19.Text = "Remarks :"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
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
        Me.ADataGrid.TabIndex = 88
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
        Me.Report1.TabIndex = 89
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(754, 411)
        Me.SprdView.TabIndex = 115
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
        Me.FraMovement.Location = New System.Drawing.Point(0, 406)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(753, 51)
        Me.FraMovement.TabIndex = 101
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
        Me.lblMasterType.TabIndex = 102
        Me.lblMasterType.Text = "lblMasterType"
        Me.lblMasterType.Visible = False
        '
        'OptStatus
        '
        '
        'optBalMethod
        '
        '
        'optGSTClassification
        '
        '
        'optGSTRegd
        '
        '
        'optRegd
        '
        '
        'frmAcmRequisition
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(754, 457)
        Me.Controls.Add(Me.FraTrn)
        Me.Controls.Add(Me.ADataGrid)
        Me.Controls.Add(Me.Report1)
        Me.Controls.Add(Me.SprdView)
        Me.Controls.Add(Me.FraMovement)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(-242, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAcmRequisition"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Account Master Requisition"
        Me.FraTrn.ResumeLayout(False)
        Me.FraTrn.PerformLayout()
        Me.FraStatus.ResumeLayout(False)
        Me.Frame1.ResumeLayout(False)
        Me.fraTop1.ResumeLayout(False)
        Me.fraTop1.PerformLayout()
        Me.FraAcctType.ResumeLayout(False)
        Me.FraAcctType.PerformLayout()
        Me.Frame8.ResumeLayout(False)
        Me.Frame8.PerformLayout()
        Me.FraView.ResumeLayout(False)
        Me.SSTInfo.ResumeLayout(False)
        Me._SSTInfo_TabPage0.ResumeLayout(False)
        Me.Frame3.ResumeLayout(False)
        Me.Frame5.ResumeLayout(False)
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me._SSTInfo_TabPage1.ResumeLayout(False)
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me._SSTInfo_TabPage2.ResumeLayout(False)
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me._SSTInfo_TabPage3.ResumeLayout(False)
        Me.Frame7.ResumeLayout(False)
        Me.Frame7.PerformLayout()
        Me.Frame13.ResumeLayout(False)
        Me._SSTInfo_TabPage4.ResumeLayout(False)
        Me.Frame12.ResumeLayout(False)
        Me.Frame12.PerformLayout()
        Me._SSTInfo_TabPage5.ResumeLayout(False)
        Me.Frame9.ResumeLayout(False)
        Me.Frame9.PerformLayout()
        Me.Frame10.ResumeLayout(False)
        Me.Frame10.PerformLayout()
        Me._SSTInfo_TabPage6.ResumeLayout(False)
        Me.Frame11.ResumeLayout(False)
        Me.Frame11.PerformLayout()
        Me._SSTInfo_TabPage7.ResumeLayout(False)
        Me.Frame14.ResumeLayout(False)
        Me.Frame14.PerformLayout()
        Me._SSTInfo_TabPage8.ResumeLayout(False)
        Me.FraGSTClass.ResumeLayout(False)
        Me.Frame15.ResumeLayout(False)
        Me.Frame15.PerformLayout()
        Me.FraGSTStatus.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.Label25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLabels, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optBalMethod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optGSTClassification, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optGSTRegd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optRegd, System.ComponentModel.ISupportInitialize).EndInit()
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
#End Region
End Class