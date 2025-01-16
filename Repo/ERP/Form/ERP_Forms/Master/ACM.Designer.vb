Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmAcm
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
    Public WithEvents txtGUID As System.Windows.Forms.TextBox
    Public WithEvents Label62 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents fraTop1 As System.Windows.Forms.GroupBox
    Public WithEvents cboCustGroup As System.Windows.Forms.ComboBox
    Public WithEvents txtEmpCode As System.Windows.Forms.TextBox
    Public WithEvents txtCurrency As System.Windows.Forms.TextBox
    Public WithEvents txtGroupNameCr As System.Windows.Forms.TextBox
    Public WithEvents cboSupplierType As System.Windows.Forms.ComboBox
    Public WithEvents cboHeadType As System.Windows.Forms.ComboBox
    Public WithEvents cboCategory As System.Windows.Forms.ComboBox
    Public WithEvents txtGroupName As System.Windows.Forms.TextBox
    Public WithEvents Label57 As System.Windows.Forms.Label
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
    Public WithEvents Label42 As System.Windows.Forms.Label
    Public WithEvents Label41 As System.Windows.Forms.Label
    Public WithEvents Label40 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents Label22 As System.Windows.Forms.Label
    Public WithEvents Label31 As System.Windows.Forms.Label
    Public WithEvents Frame8 As System.Windows.Forms.GroupBox
    Public WithEvents txtDistance As System.Windows.Forms.TextBox
    Public WithEvents txtaddress As System.Windows.Forms.TextBox
    Public WithEvents txtState As System.Windows.Forms.TextBox
    Public WithEvents txtPinCode As System.Windows.Forms.TextBox
    Public WithEvents txtCity As System.Windows.Forms.TextBox
    Public WithEvents txtCountry As System.Windows.Forms.TextBox
    Public WithEvents Label56 As System.Windows.Forms.Label
    Public WithEvents Label51 As System.Windows.Forms.Label
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
    Public WithEvents chkAuthorised As System.Windows.Forms.CheckBox
    Public WithEvents chkInterUnit As System.Windows.Forms.CheckBox
    Public WithEvents chkDistt As System.Windows.Forms.CheckBox
    Public WithEvents chkState As System.Windows.Forms.CheckBox
    Public WithEvents chkCountry As System.Windows.Forms.CheckBox
    Public WithEvents Frame5 As System.Windows.Forms.GroupBox
    Public WithEvents chkStopPO As System.Windows.Forms.CheckBox
    Public WithEvents chkStopGP As System.Windows.Forms.CheckBox
    Public WithEvents chkStopMRR As System.Windows.Forms.CheckBox
    Public WithEvents chkStopInvoice As System.Windows.Forms.CheckBox
    Public WithEvents chkStopBP As System.Windows.Forms.CheckBox
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents _SSTInfo_TabPage0 As System.Windows.Forms.TabPage
    Public WithEvents cboNature As System.Windows.Forms.ComboBox
    Public WithEvents txtPolicyNo As System.Windows.Forms.TextBox
    Public WithEvents txtContact As System.Windows.Forms.TextBox
    Public WithEvents txtFax As System.Windows.Forms.TextBox
    Public WithEvents txtMobile As System.Windows.Forms.TextBox
    Public WithEvents txtEmail As System.Windows.Forms.TextBox
    Public WithEvents txtPhone As System.Windows.Forms.TextBox
    Public WithEvents Label63 As System.Windows.Forms.Label
    Public WithEvents _Label25_7 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents _Label25_0 As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents _SSTInfo_TabPage1 As System.Windows.Forms.TabPage
    Public WithEvents chkSMEStatus As System.Windows.Forms.CheckBox
    Public WithEvents chkSMERegd As System.Windows.Forms.CheckBox
    Public WithEvents txtUdyogAahaarNo As System.Windows.Forms.TextBox
    Public WithEvents cboSymbol As System.Windows.Forms.ComboBox
    Public WithEvents cboEnterpriseType As System.Windows.Forms.ComboBox
    Public WithEvents Label61 As System.Windows.Forms.Label
    Public WithEvents Label60 As System.Windows.Forms.Label
    Public WithEvents Label59 As System.Windows.Forms.Label
    Public WithEvents Frame16 As System.Windows.Forms.GroupBox
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
    Public WithEvents chkSEZ As System.Windows.Forms.CheckBox
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
    Public WithEvents txtLenderBank As System.Windows.Forms.TextBox
    Public WithEvents Frame17 As System.Windows.Forms.GroupBox
    Public WithEvents txtBankAccountNo As System.Windows.Forms.TextBox
    Public WithEvents txtBankBranch As System.Windows.Forms.TextBox
    Public WithEvents txtIFSCCode As System.Windows.Forms.TextBox
    Public WithEvents txtSwitCode As System.Windows.Forms.TextBox
    Public WithEvents txtBankName As System.Windows.Forms.TextBox
    Public WithEvents Label55 As System.Windows.Forms.Label
    Public WithEvents _Label25_6 As System.Windows.Forms.Label
    Public WithEvents Label54 As System.Windows.Forms.Label
    Public WithEvents Label52 As System.Windows.Forms.Label
    Public WithEvents _Label25_5 As System.Windows.Forms.Label
    Public WithEvents Frame14 As System.Windows.Forms.GroupBox
    Public WithEvents _SSTInfo_TabPage7 As System.Windows.Forms.TabPage
    Public WithEvents txtGSTRegnNo As System.Windows.Forms.TextBox
    Public WithEvents _optGSTRegd_4 As System.Windows.Forms.RadioButton
    Public WithEvents _optGSTRegd_3 As System.Windows.Forms.RadioButton
    Public WithEvents _optGSTRegd_2 As System.Windows.Forms.RadioButton
    Public WithEvents _optGSTRegd_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optGSTRegd_0 As System.Windows.Forms.RadioButton
    Public WithEvents FraGSTStatus As System.Windows.Forms.GroupBox
    Public WithEvents Label58 As System.Windows.Forms.Label
    Public WithEvents Frame15 As System.Windows.Forms.GroupBox
    Public WithEvents _optGSTClassification_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optGSTClassification_0 As System.Windows.Forms.RadioButton
    Public WithEvents FraGSTClass As System.Windows.Forms.GroupBox
    Public WithEvents chkTCSApplicable As System.Windows.Forms.CheckBox
    Public WithEvents Frame18 As System.Windows.Forms.GroupBox
    Public WithEvents txtCurrencyCode As System.Windows.Forms.TextBox
    Public WithEvents txtCountryCode As System.Windows.Forms.TextBox
    Public WithEvents Label65 As System.Windows.Forms.Label
    Public WithEvents Label64 As System.Windows.Forms.Label
    Public WithEvents Frame19 As System.Windows.Forms.GroupBox
    Public WithEvents _SSTInfo_TabPage8 As System.Windows.Forms.TabPage
    Public WithEvents SSTInfo As System.Windows.Forms.TabControl
    Public WithEvents FraView As System.Windows.Forms.GroupBox
    Public WithEvents lblRemarks As System.Windows.Forms.Label
    Public WithEvents FraTrn As System.Windows.Forms.GroupBox
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
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAcm))
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
        Me.cmdSearchHead = New System.Windows.Forms.Button()
        Me.cmdSearchHeadCr = New System.Windows.Forms.Button()
        Me.cmdPaySearch = New System.Windows.Forms.Button()
        Me.FraTrn = New System.Windows.Forms.GroupBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.FraStatus = New System.Windows.Forms.GroupBox()
        Me._OptStatus_1 = New System.Windows.Forms.RadioButton()
        Me._OptStatus_0 = New System.Windows.Forms.RadioButton()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me._optBalMethod_0 = New System.Windows.Forms.RadioButton()
        Me._optBalMethod_1 = New System.Windows.Forms.RadioButton()
        Me.fraTop1 = New System.Windows.Forms.GroupBox()
        Me.txtCode = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.txtName = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.txtGUID = New System.Windows.Forms.TextBox()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.FraAcctType = New System.Windows.Forms.GroupBox()
        Me.chkAccountHide = New System.Windows.Forms.CheckBox()
        Me.cboCustGroup = New System.Windows.Forms.ComboBox()
        Me.txtEmpCode = New System.Windows.Forms.TextBox()
        Me.txtCurrency = New System.Windows.Forms.TextBox()
        Me.txtGroupNameCr = New System.Windows.Forms.TextBox()
        Me.cboSupplierType = New System.Windows.Forms.ComboBox()
        Me.cboHeadType = New System.Windows.Forms.ComboBox()
        Me.cboCategory = New System.Windows.Forms.ComboBox()
        Me.txtGroupName = New System.Windows.Forms.TextBox()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.lblHeadType = New System.Windows.Forms.Label()
        Me.LblCategory = New System.Windows.Forms.Label()
        Me.lblUnder = New System.Windows.Forms.Label()
        Me.Frame8 = New System.Windows.Forms.GroupBox()
        Me.txtReceiptDays = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtShortName = New System.Windows.Forms.TextBox()
        Me.Label66 = New System.Windows.Forms.Label()
        Me.cboPaymentMode = New System.Windows.Forms.ComboBox()
        Me.txtChqFrequency = New System.Windows.Forms.TextBox()
        Me.txtPayment = New System.Windows.Forms.TextBox()
        Me.chkMonthWiseLdgr = New System.Windows.Forms.CheckBox()
        Me.txtVendorCode = New System.Windows.Forms.TextBox()
        Me.txtPaidDay4 = New System.Windows.Forms.TextBox()
        Me.txtPaidDay3 = New System.Windows.Forms.TextBox()
        Me.txtPaidDay2 = New System.Windows.Forms.TextBox()
        Me.txtPaidDay = New System.Windows.Forms.TextBox()
        Me.txtSeq = New System.Windows.Forms.TextBox()
        Me.txtAlias = New System.Windows.Forms.TextBox()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.lblPaymentTerms = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.FraView = New System.Windows.Forms.GroupBox()
        Me.SSTInfo = New System.Windows.Forms.TabControl()
        Me._SSTInfo_TabPage0 = New System.Windows.Forms.TabPage()
        Me.chkGroupLimit = New System.Windows.Forms.CheckBox()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.txtResponsiblePerson = New System.Windows.Forms.TextBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.txtCreditLimit = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.txtCompanyName = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.txtDistance = New System.Windows.Forms.TextBox()
        Me.txtaddress = New System.Windows.Forms.TextBox()
        Me.txtState = New System.Windows.Forms.TextBox()
        Me.txtPinCode = New System.Windows.Forms.TextBox()
        Me.txtCity = New System.Windows.Forms.TextBox()
        Me.txtCountry = New System.Windows.Forms.TextBox()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.Label51 = New System.Windows.Forms.Label()
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
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me.chkAuthorised = New System.Windows.Forms.CheckBox()
        Me.chkInterUnit = New System.Windows.Forms.CheckBox()
        Me.chkDistt = New System.Windows.Forms.CheckBox()
        Me.chkState = New System.Windows.Forms.CheckBox()
        Me.chkCountry = New System.Windows.Forms.CheckBox()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.chkStopPO = New System.Windows.Forms.CheckBox()
        Me.chkStopGP = New System.Windows.Forms.CheckBox()
        Me.chkStopMRR = New System.Windows.Forms.CheckBox()
        Me.chkStopInvoice = New System.Windows.Forms.CheckBox()
        Me.chkStopBP = New System.Windows.Forms.CheckBox()
        Me.ChkPoRate = New System.Windows.Forms.CheckBox()
        Me._SSTInfo_TabPage9 = New System.Windows.Forms.TabPage()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me._SSTInfo_TabPage1 = New System.Windows.Forms.TabPage()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.cboNature = New System.Windows.Forms.ComboBox()
        Me.txtPolicyNo = New System.Windows.Forms.TextBox()
        Me.txtContact = New System.Windows.Forms.TextBox()
        Me.txtFax = New System.Windows.Forms.TextBox()
        Me.txtMobile = New System.Windows.Forms.TextBox()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.txtPhone = New System.Windows.Forms.TextBox()
        Me.Label63 = New System.Windows.Forms.Label()
        Me._Label25_7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me._Label25_0 = New System.Windows.Forms.Label()
        Me._SSTInfo_TabPage2 = New System.Windows.Forms.TabPage()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.Frame16 = New System.Windows.Forms.GroupBox()
        Me.chkSMEStatus = New System.Windows.Forms.CheckBox()
        Me.chkSMERegd = New System.Windows.Forms.CheckBox()
        Me.txtUdyogAahaarNo = New System.Windows.Forms.TextBox()
        Me.cboSymbol = New System.Windows.Forms.ComboBox()
        Me.cboEnterpriseType = New System.Windows.Forms.ComboBox()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.Label59 = New System.Windows.Forms.Label()
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
        Me.chkTDSNotDeduct = New System.Windows.Forms.CheckBox()
        Me.chkRtnDeclaration = New System.Windows.Forms.CheckBox()
        Me.chkTDSDeduct = New System.Windows.Forms.CheckBox()
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
        Me._SSTInfo_TabPage6 = New System.Windows.Forms.TabPage()
        Me.Frame11 = New System.Windows.Forms.GroupBox()
        Me.chkSEZ = New System.Windows.Forms.CheckBox()
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
        Me.txtSecurityChqNo = New System.Windows.Forms.TextBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.txtSecurityAmount = New System.Windows.Forms.TextBox()
        Me.lblSecurityAmount = New System.Windows.Forms.Label()
        Me.chkSecurityChq = New System.Windows.Forms.CheckBox()
        Me.Frame14 = New System.Windows.Forms.GroupBox()
        Me.Frame17 = New System.Windows.Forms.GroupBox()
        Me.txtLenderBank = New System.Windows.Forms.TextBox()
        Me.txtBankAccountNo = New System.Windows.Forms.TextBox()
        Me.txtBankBranch = New System.Windows.Forms.TextBox()
        Me.txtIFSCCode = New System.Windows.Forms.TextBox()
        Me.txtSwitCode = New System.Windows.Forms.TextBox()
        Me.txtBankName = New System.Windows.Forms.TextBox()
        Me.Label55 = New System.Windows.Forms.Label()
        Me._Label25_6 = New System.Windows.Forms.Label()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.Label52 = New System.Windows.Forms.Label()
        Me._Label25_5 = New System.Windows.Forms.Label()
        Me._SSTInfo_TabPage8 = New System.Windows.Forms.TabPage()
        Me.Frame15 = New System.Windows.Forms.GroupBox()
        Me.chkPlaceofSupply = New System.Windows.Forms.CheckBox()
        Me.txtGSTRegnNo = New System.Windows.Forms.TextBox()
        Me.FraGSTStatus = New System.Windows.Forms.GroupBox()
        Me._optGSTRegd_4 = New System.Windows.Forms.RadioButton()
        Me._optGSTRegd_3 = New System.Windows.Forms.RadioButton()
        Me._optGSTRegd_2 = New System.Windows.Forms.RadioButton()
        Me._optGSTRegd_1 = New System.Windows.Forms.RadioButton()
        Me._optGSTRegd_0 = New System.Windows.Forms.RadioButton()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.FraGSTClass = New System.Windows.Forms.GroupBox()
        Me._optGSTClassification_1 = New System.Windows.Forms.RadioButton()
        Me._optGSTClassification_0 = New System.Windows.Forms.RadioButton()
        Me.Frame18 = New System.Windows.Forms.GroupBox()
        Me.chkTCSNotApplicable = New System.Windows.Forms.CheckBox()
        Me.chkTCSApplicable = New System.Windows.Forms.CheckBox()
        Me.Frame19 = New System.Windows.Forms.GroupBox()
        Me.txtCurrencyCode = New System.Windows.Forms.TextBox()
        Me.txtCountryCode = New System.Windows.Forms.TextBox()
        Me.Label65 = New System.Windows.Forms.Label()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.lblRemarks = New System.Windows.Forms.Label()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.lblMasterType = New System.Windows.Forms.Label()
        Me.Label25 = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.OptStatus = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.lblLabels = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.optBalMethod = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optGSTClassification = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optGSTRegd = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optRegd = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.FraTrn.SuspendLayout()
        Me.FraStatus.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.fraTop1.SuspendLayout()
        CType(Me.txtCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraAcctType.SuspendLayout()
        Me.Frame8.SuspendLayout()
        Me.FraView.SuspendLayout()
        Me.SSTInfo.SuspendLayout()
        Me._SSTInfo_TabPage0.SuspendLayout()
        Me.Frame4.SuspendLayout()
        Me.Frame5.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me._SSTInfo_TabPage9.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTInfo_TabPage1.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me._SSTInfo_TabPage2.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me.Frame16.SuspendLayout()
        Me._SSTInfo_TabPage3.SuspendLayout()
        Me.Frame7.SuspendLayout()
        Me.Frame13.SuspendLayout()
        Me._SSTInfo_TabPage4.SuspendLayout()
        Me.Frame12.SuspendLayout()
        Me._SSTInfo_TabPage6.SuspendLayout()
        Me.Frame11.SuspendLayout()
        Me._SSTInfo_TabPage7.SuspendLayout()
        Me.Frame14.SuspendLayout()
        Me.Frame17.SuspendLayout()
        Me._SSTInfo_TabPage8.SuspendLayout()
        Me.Frame15.SuspendLayout()
        Me.FraGSTStatus.SuspendLayout()
        Me.FraGSTClass.SuspendLayout()
        Me.Frame18.SuspendLayout()
        Me.Frame19.SuspendLayout()
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
        Me.CmdAdd.ImageAlign = System.Drawing.ContentAlignment.TopCenter
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
        Me.CmdModify.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdModify.Location = New System.Drawing.Point(128, 11)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(67, 37)
        Me.CmdModify.TabIndex = 91
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
        Me.CmdSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdSave.Location = New System.Drawing.Point(196, 11)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(67, 37)
        Me.CmdSave.TabIndex = 92
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
        Me.CmdDelete.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdDelete.Location = New System.Drawing.Point(330, 11)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(67, 37)
        Me.CmdDelete.TabIndex = 94
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
        Me.CmdView.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdView.Location = New System.Drawing.Point(532, 11)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(67, 37)
        Me.CmdView.TabIndex = 97
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
        Me.CmdClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdClose.Location = New System.Drawing.Point(600, 11)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(67, 37)
        Me.CmdClose.TabIndex = 98
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
        Me.cmdPrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdPrint.Location = New System.Drawing.Point(398, 11)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 95
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
        Me.cmdSavePrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdSavePrint.Location = New System.Drawing.Point(264, 11)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdSavePrint.TabIndex = 93
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
        Me.CmdPreview.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdPreview.Location = New System.Drawing.Point(466, 11)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.TabIndex = 96
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Preview")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'cmdSearchHead
        '
        Me.cmdSearchHead.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchHead.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchHead.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchHead.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchHead.Image = CType(resources.GetObject("cmdSearchHead.Image"), System.Drawing.Image)
        Me.cmdSearchHead.Location = New System.Drawing.Point(790, 42)
        Me.cmdSearchHead.Name = "cmdSearchHead"
        Me.cmdSearchHead.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchHead.Size = New System.Drawing.Size(29, 22)
        Me.cmdSearchHead.TabIndex = 230
        Me.cmdSearchHead.TabStop = False
        Me.cmdSearchHead.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchHead, "Search")
        Me.cmdSearchHead.UseVisualStyleBackColor = False
        '
        'cmdSearchHeadCr
        '
        Me.cmdSearchHeadCr.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchHeadCr.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchHeadCr.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchHeadCr.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchHeadCr.Image = CType(resources.GetObject("cmdSearchHeadCr.Image"), System.Drawing.Image)
        Me.cmdSearchHeadCr.Location = New System.Drawing.Point(790, 74)
        Me.cmdSearchHeadCr.Name = "cmdSearchHeadCr"
        Me.cmdSearchHeadCr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchHeadCr.Size = New System.Drawing.Size(29, 22)
        Me.cmdSearchHeadCr.TabIndex = 231
        Me.cmdSearchHeadCr.TabStop = False
        Me.cmdSearchHeadCr.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchHeadCr, "Search")
        Me.cmdSearchHeadCr.UseVisualStyleBackColor = False
        '
        'cmdPaySearch
        '
        Me.cmdPaySearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdPaySearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPaySearch.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPaySearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPaySearch.Image = CType(resources.GetObject("cmdPaySearch.Image"), System.Drawing.Image)
        Me.cmdPaySearch.Location = New System.Drawing.Point(852, 12)
        Me.cmdPaySearch.Name = "cmdPaySearch"
        Me.cmdPaySearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPaySearch.Size = New System.Drawing.Size(29, 22)
        Me.cmdPaySearch.TabIndex = 232
        Me.cmdPaySearch.TabStop = False
        Me.cmdPaySearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPaySearch, "Search")
        Me.cmdPaySearch.UseVisualStyleBackColor = False
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
        Me.FraTrn.Controls.Add(Me.lblRemarks)
        Me.FraTrn.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraTrn.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraTrn.Location = New System.Drawing.Point(-1, -4)
        Me.FraTrn.Name = "FraTrn"
        Me.FraTrn.Padding = New System.Windows.Forms.Padding(0)
        Me.FraTrn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraTrn.Size = New System.Drawing.Size(1106, 576)
        Me.FraTrn.TabIndex = 99
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
        Me.txtRemarks.Location = New System.Drawing.Point(145, 502)
        Me.txtRemarks.MaxLength = 0
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.Size = New System.Drawing.Size(637, 68)
        Me.txtRemarks.TabIndex = 86
        '
        'FraStatus
        '
        Me.FraStatus.BackColor = System.Drawing.SystemColors.Control
        Me.FraStatus.Controls.Add(Me._OptStatus_1)
        Me.FraStatus.Controls.Add(Me._OptStatus_0)
        Me.FraStatus.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraStatus.Location = New System.Drawing.Point(952, 493)
        Me.FraStatus.Name = "FraStatus"
        Me.FraStatus.Padding = New System.Windows.Forms.Padding(0)
        Me.FraStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraStatus.Size = New System.Drawing.Size(150, 75)
        Me.FraStatus.TabIndex = 148
        Me.FraStatus.TabStop = False
        Me.FraStatus.Text = "Status"
        '
        '_OptStatus_1
        '
        Me._OptStatus_1.AutoSize = True
        Me._OptStatus_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptStatus_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptStatus_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptStatus_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptStatus.SetIndex(Me._OptStatus_1, CType(1, Short))
        Me._OptStatus_1.Location = New System.Drawing.Point(44, 48)
        Me._OptStatus_1.Name = "_OptStatus_1"
        Me._OptStatus_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptStatus_1.Size = New System.Drawing.Size(53, 17)
        Me._OptStatus_1.TabIndex = 88
        Me._OptStatus_1.TabStop = True
        Me._OptStatus_1.Text = "Close"
        Me._OptStatus_1.UseVisualStyleBackColor = False
        '
        '_OptStatus_0
        '
        Me._OptStatus_0.AutoSize = True
        Me._OptStatus_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptStatus_0.Checked = True
        Me._OptStatus_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptStatus_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptStatus_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptStatus.SetIndex(Me._OptStatus_0, CType(0, Short))
        Me._OptStatus_0.Location = New System.Drawing.Point(44, 25)
        Me._OptStatus_0.Name = "_OptStatus_0"
        Me._OptStatus_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptStatus_0.Size = New System.Drawing.Size(52, 17)
        Me._OptStatus_0.TabIndex = 87
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
        Me.Frame1.Location = New System.Drawing.Point(784, 494)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(166, 76)
        Me.Frame1.TabIndex = 147
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Balancing Method"
        '
        '_optBalMethod_0
        '
        Me._optBalMethod_0.AutoSize = True
        Me._optBalMethod_0.BackColor = System.Drawing.SystemColors.Menu
        Me._optBalMethod_0.Checked = True
        Me._optBalMethod_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optBalMethod_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optBalMethod_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optBalMethod.SetIndex(Me._optBalMethod_0, CType(0, Short))
        Me._optBalMethod_0.Location = New System.Drawing.Point(41, 22)
        Me._optBalMethod_0.Name = "_optBalMethod_0"
        Me._optBalMethod_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optBalMethod_0.Size = New System.Drawing.Size(88, 17)
        Me._optBalMethod_0.TabIndex = 89
        Me._optBalMethod_0.TabStop = True
        Me._optBalMethod_0.Text = "Summarised"
        Me._optBalMethod_0.UseVisualStyleBackColor = False
        '
        '_optBalMethod_1
        '
        Me._optBalMethod_1.AutoSize = True
        Me._optBalMethod_1.BackColor = System.Drawing.SystemColors.Menu
        Me._optBalMethod_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optBalMethod_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optBalMethod_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optBalMethod.SetIndex(Me._optBalMethod_1, CType(1, Short))
        Me._optBalMethod_1.Location = New System.Drawing.Point(43, 49)
        Me._optBalMethod_1.Name = "_optBalMethod_1"
        Me._optBalMethod_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optBalMethod_1.Size = New System.Drawing.Size(68, 17)
        Me._optBalMethod_1.TabIndex = 90
        Me._optBalMethod_1.TabStop = True
        Me._optBalMethod_1.Text = "Detailed"
        Me._optBalMethod_1.UseVisualStyleBackColor = False
        '
        'fraTop1
        '
        Me.fraTop1.BackColor = System.Drawing.SystemColors.Control
        Me.fraTop1.Controls.Add(Me.txtCode)
        Me.fraTop1.Controls.Add(Me.txtName)
        Me.fraTop1.Controls.Add(Me.txtGUID)
        Me.fraTop1.Controls.Add(Me.Label62)
        Me.fraTop1.Controls.Add(Me.Label2)
        Me.fraTop1.Controls.Add(Me.Label4)
        Me.fraTop1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTop1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraTop1.Location = New System.Drawing.Point(0, 0)
        Me.fraTop1.Name = "fraTop1"
        Me.fraTop1.Padding = New System.Windows.Forms.Padding(0)
        Me.fraTop1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraTop1.Size = New System.Drawing.Size(1106, 46)
        Me.fraTop1.TabIndex = 100
        Me.fraTop1.TabStop = False
        '
        'txtCode
        '
        Me.txtCode.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest
        Me.txtCode.AutoSize = False
        Me.txtCode.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains
        Appearance1.BackColor = System.Drawing.SystemColors.Window
        Appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption
        Me.txtCode.DisplayLayout.Appearance = Appearance1
        Me.txtCode.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.txtCode.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
        Appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder
        Appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance2.BorderColor = System.Drawing.SystemColors.Window
        Me.txtCode.DisplayLayout.GroupByBox.Appearance = Appearance2
        Appearance3.ForeColor = System.Drawing.SystemColors.GrayText
        Me.txtCode.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance3
        Me.txtCode.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance4.BackColor2 = System.Drawing.SystemColors.Control
        Appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance4.ForeColor = System.Drawing.SystemColors.GrayText
        Me.txtCode.DisplayLayout.GroupByBox.PromptAppearance = Appearance4
        Me.txtCode.DisplayLayout.MaxColScrollRegions = 1
        Me.txtCode.DisplayLayout.MaxRowScrollRegions = 1
        Appearance5.BackColor = System.Drawing.SystemColors.Window
        Appearance5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtCode.DisplayLayout.Override.ActiveCellAppearance = Appearance5
        Appearance6.BackColor = System.Drawing.SystemColors.Highlight
        Appearance6.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.txtCode.DisplayLayout.Override.ActiveRowAppearance = Appearance6
        Me.txtCode.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
        Me.txtCode.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
        Appearance7.BackColor = System.Drawing.SystemColors.Window
        Me.txtCode.DisplayLayout.Override.CardAreaAppearance = Appearance7
        Appearance8.BorderColor = System.Drawing.Color.Silver
        Appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
        Me.txtCode.DisplayLayout.Override.CellAppearance = Appearance8
        Me.txtCode.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
        Me.txtCode.DisplayLayout.Override.CellPadding = 0
        Appearance9.BackColor = System.Drawing.SystemColors.Control
        Appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
        Appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance9.BorderColor = System.Drawing.SystemColors.Window
        Me.txtCode.DisplayLayout.Override.GroupByRowAppearance = Appearance9
        Appearance10.TextHAlignAsString = "Left"
        Me.txtCode.DisplayLayout.Override.HeaderAppearance = Appearance10
        Me.txtCode.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Me.txtCode.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
        Appearance11.BackColor = System.Drawing.SystemColors.Window
        Appearance11.BorderColor = System.Drawing.Color.Silver
        Me.txtCode.DisplayLayout.Override.RowAppearance = Appearance11
        Me.txtCode.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
        Appearance12.BackColor = System.Drawing.SystemColors.ControlLight
        Me.txtCode.DisplayLayout.Override.TemplateAddRowAppearance = Appearance12
        Me.txtCode.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.txtCode.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.txtCode.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
        Me.txtCode.Font = New System.Drawing.Font("Verdana", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCode.Location = New System.Drawing.Point(835, 12)
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(140, 22)
        Me.txtCode.TabIndex = 230
        '
        'txtName
        '
        Me.txtName.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest
        Me.txtName.AutoSize = False
        Me.txtName.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains
        Appearance13.BackColor = System.Drawing.SystemColors.Window
        Appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption
        Me.txtName.DisplayLayout.Appearance = Appearance13
        Me.txtName.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.txtName.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
        Appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder
        Appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance14.BorderColor = System.Drawing.SystemColors.Window
        Me.txtName.DisplayLayout.GroupByBox.Appearance = Appearance14
        Appearance15.ForeColor = System.Drawing.SystemColors.GrayText
        Me.txtName.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance15
        Me.txtName.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance16.BackColor2 = System.Drawing.SystemColors.Control
        Appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance16.ForeColor = System.Drawing.SystemColors.GrayText
        Me.txtName.DisplayLayout.GroupByBox.PromptAppearance = Appearance16
        Me.txtName.DisplayLayout.MaxColScrollRegions = 1
        Me.txtName.DisplayLayout.MaxRowScrollRegions = 1
        Appearance17.BackColor = System.Drawing.SystemColors.Window
        Appearance17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtName.DisplayLayout.Override.ActiveCellAppearance = Appearance17
        Appearance18.BackColor = System.Drawing.SystemColors.Highlight
        Appearance18.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.txtName.DisplayLayout.Override.ActiveRowAppearance = Appearance18
        Me.txtName.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
        Me.txtName.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
        Appearance19.BackColor = System.Drawing.SystemColors.Window
        Me.txtName.DisplayLayout.Override.CardAreaAppearance = Appearance19
        Appearance20.BorderColor = System.Drawing.Color.Silver
        Appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
        Me.txtName.DisplayLayout.Override.CellAppearance = Appearance20
        Me.txtName.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
        Me.txtName.DisplayLayout.Override.CellPadding = 0
        Appearance21.BackColor = System.Drawing.SystemColors.Control
        Appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
        Appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance21.BorderColor = System.Drawing.SystemColors.Window
        Me.txtName.DisplayLayout.Override.GroupByRowAppearance = Appearance21
        Appearance22.TextHAlignAsString = "Left"
        Me.txtName.DisplayLayout.Override.HeaderAppearance = Appearance22
        Me.txtName.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Me.txtName.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
        Appearance23.BackColor = System.Drawing.SystemColors.Window
        Appearance23.BorderColor = System.Drawing.Color.Silver
        Me.txtName.DisplayLayout.Override.RowAppearance = Appearance23
        Me.txtName.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
        Appearance24.BackColor = System.Drawing.SystemColors.ControlLight
        Me.txtName.DisplayLayout.Override.TemplateAddRowAppearance = Appearance24
        Me.txtName.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.txtName.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.txtName.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
        Me.txtName.Font = New System.Drawing.Font("Verdana", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtName.Location = New System.Drawing.Point(114, 12)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(654, 22)
        Me.txtName.TabIndex = 229
        '
        'txtGUID
        '
        Me.txtGUID.AcceptsReturn = True
        Me.txtGUID.BackColor = System.Drawing.SystemColors.Window
        Me.txtGUID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGUID.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGUID.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGUID.ForeColor = System.Drawing.Color.Blue
        Me.txtGUID.Location = New System.Drawing.Point(1026, 13)
        Me.txtGUID.MaxLength = 0
        Me.txtGUID.Name = "txtGUID"
        Me.txtGUID.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGUID.Size = New System.Drawing.Size(67, 22)
        Me.txtGUID.TabIndex = 227
        '
        'Label62
        '
        Me.Label62.AutoSize = True
        Me.Label62.BackColor = System.Drawing.SystemColors.Control
        Me.Label62.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label62.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label62.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label62.Location = New System.Drawing.Point(982, 16)
        Me.Label62.Name = "Label62"
        Me.Label62.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label62.Size = New System.Drawing.Size(40, 13)
        Me.Label62.TabIndex = 228
        Me.Label62.Text = "GUID :"
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
        Me.Label2.Size = New System.Drawing.Size(86, 13)
        Me.Label2.TabIndex = 101
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
        Me.Label4.Location = New System.Drawing.Point(788, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(40, 13)
        Me.Label4.TabIndex = 102
        Me.Label4.Text = "Code :"
        '
        'FraAcctType
        '
        Me.FraAcctType.BackColor = System.Drawing.SystemColors.Control
        Me.FraAcctType.Controls.Add(Me.chkAccountHide)
        Me.FraAcctType.Controls.Add(Me.cmdSearchHeadCr)
        Me.FraAcctType.Controls.Add(Me.cmdSearchHead)
        Me.FraAcctType.Controls.Add(Me.cboCustGroup)
        Me.FraAcctType.Controls.Add(Me.txtEmpCode)
        Me.FraAcctType.Controls.Add(Me.txtCurrency)
        Me.FraAcctType.Controls.Add(Me.txtGroupNameCr)
        Me.FraAcctType.Controls.Add(Me.cboSupplierType)
        Me.FraAcctType.Controls.Add(Me.cboHeadType)
        Me.FraAcctType.Controls.Add(Me.cboCategory)
        Me.FraAcctType.Controls.Add(Me.txtGroupName)
        Me.FraAcctType.Controls.Add(Me.Label57)
        Me.FraAcctType.Controls.Add(Me.Label26)
        Me.FraAcctType.Controls.Add(Me.Label24)
        Me.FraAcctType.Controls.Add(Me.Label23)
        Me.FraAcctType.Controls.Add(Me.Label21)
        Me.FraAcctType.Controls.Add(Me.lblHeadType)
        Me.FraAcctType.Controls.Add(Me.LblCategory)
        Me.FraAcctType.Controls.Add(Me.lblUnder)
        Me.FraAcctType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraAcctType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraAcctType.Location = New System.Drawing.Point(0, 42)
        Me.FraAcctType.Name = "FraAcctType"
        Me.FraAcctType.Padding = New System.Windows.Forms.Padding(0)
        Me.FraAcctType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraAcctType.Size = New System.Drawing.Size(1108, 106)
        Me.FraAcctType.TabIndex = 103
        Me.FraAcctType.TabStop = False
        '
        'chkAccountHide
        '
        Me.chkAccountHide.AutoSize = True
        Me.chkAccountHide.BackColor = System.Drawing.SystemColors.Control
        Me.chkAccountHide.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAccountHide.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAccountHide.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAccountHide.Location = New System.Drawing.Point(776, 14)
        Me.chkAccountHide.Name = "chkAccountHide"
        Me.chkAccountHide.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAccountHide.Size = New System.Drawing.Size(93, 17)
        Me.chkAccountHide.TabIndex = 233
        Me.chkAccountHide.Text = "Account Hide"
        Me.chkAccountHide.UseVisualStyleBackColor = False
        Me.chkAccountHide.Visible = False
        '
        'cboCustGroup
        '
        Me.cboCustGroup.BackColor = System.Drawing.SystemColors.Window
        Me.cboCustGroup.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCustGroup.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCustGroup.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.cboCustGroup.Location = New System.Drawing.Point(948, 43)
        Me.cboCustGroup.Name = "cboCustGroup"
        Me.cboCustGroup.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCustGroup.Size = New System.Drawing.Size(141, 21)
        Me.cboCustGroup.TabIndex = 10
        Me.cboCustGroup.Text = "cboCustGroup"
        '
        'txtEmpCode
        '
        Me.txtEmpCode.AcceptsReturn = True
        Me.txtEmpCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmpCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmpCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmpCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpCode.ForeColor = System.Drawing.Color.Blue
        Me.txtEmpCode.Location = New System.Drawing.Point(948, 75)
        Me.txtEmpCode.MaxLength = 0
        Me.txtEmpCode.Name = "txtEmpCode"
        Me.txtEmpCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmpCode.Size = New System.Drawing.Size(141, 22)
        Me.txtEmpCode.TabIndex = 13
        '
        'txtCurrency
        '
        Me.txtCurrency.AcceptsReturn = True
        Me.txtCurrency.BackColor = System.Drawing.SystemColors.Window
        Me.txtCurrency.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCurrency.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCurrency.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrency.ForeColor = System.Drawing.Color.Blue
        Me.txtCurrency.Location = New System.Drawing.Point(948, 13)
        Me.txtCurrency.MaxLength = 0
        Me.txtCurrency.Name = "txtCurrency"
        Me.txtCurrency.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCurrency.Size = New System.Drawing.Size(141, 22)
        Me.txtCurrency.TabIndex = 7
        '
        'txtGroupNameCr
        '
        Me.txtGroupNameCr.AcceptsReturn = True
        Me.txtGroupNameCr.BackColor = System.Drawing.SystemColors.Window
        Me.txtGroupNameCr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGroupNameCr.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGroupNameCr.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGroupNameCr.ForeColor = System.Drawing.Color.Blue
        Me.txtGroupNameCr.Location = New System.Drawing.Point(114, 75)
        Me.txtGroupNameCr.MaxLength = 0
        Me.txtGroupNameCr.Name = "txtGroupNameCr"
        Me.txtGroupNameCr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGroupNameCr.Size = New System.Drawing.Size(674, 22)
        Me.txtGroupNameCr.TabIndex = 11
        '
        'cboSupplierType
        '
        Me.cboSupplierType.BackColor = System.Drawing.SystemColors.Window
        Me.cboSupplierType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboSupplierType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSupplierType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboSupplierType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.cboSupplierType.Location = New System.Drawing.Point(664, 13)
        Me.cboSupplierType.Name = "cboSupplierType"
        Me.cboSupplierType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboSupplierType.Size = New System.Drawing.Size(105, 21)
        Me.cboSupplierType.TabIndex = 6
        '
        'cboHeadType
        '
        Me.cboHeadType.BackColor = System.Drawing.SystemColors.Window
        Me.cboHeadType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboHeadType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboHeadType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboHeadType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboHeadType.Location = New System.Drawing.Point(418, 13)
        Me.cboHeadType.Name = "cboHeadType"
        Me.cboHeadType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboHeadType.Size = New System.Drawing.Size(123, 21)
        Me.cboHeadType.TabIndex = 5
        '
        'cboCategory
        '
        Me.cboCategory.BackColor = System.Drawing.SystemColors.Window
        Me.cboCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCategory.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCategory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.cboCategory.Location = New System.Drawing.Point(114, 13)
        Me.cboCategory.Name = "cboCategory"
        Me.cboCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCategory.Size = New System.Drawing.Size(119, 21)
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
        Me.txtGroupName.Location = New System.Drawing.Point(114, 43)
        Me.txtGroupName.MaxLength = 0
        Me.txtGroupName.Name = "txtGroupName"
        Me.txtGroupName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGroupName.Size = New System.Drawing.Size(674, 22)
        Me.txtGroupName.TabIndex = 8
        '
        'Label57
        '
        Me.Label57.AutoSize = True
        Me.Label57.BackColor = System.Drawing.SystemColors.Control
        Me.Label57.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label57.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label57.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label57.Location = New System.Drawing.Point(847, 45)
        Me.Label57.Name = "Label57"
        Me.Label57.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label57.Size = New System.Drawing.Size(97, 13)
        Me.Label57.TabIndex = 220
        Me.Label57.Text = "Customer Group :"
        Me.Label57.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.SystemColors.Control
        Me.Label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label26.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(880, 79)
        Me.Label26.Name = "Label26"
        Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label26.Size = New System.Drawing.Size(66, 13)
        Me.Label26.TabIndex = 132
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
        Me.Label24.Location = New System.Drawing.Point(885, 16)
        Me.Label24.Name = "Label24"
        Me.Label24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label24.Size = New System.Drawing.Size(57, 13)
        Me.Label24.TabIndex = 131
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
        Me.Label23.Location = New System.Drawing.Point(18, 77)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(100, 13)
        Me.Label23.TabIndex = 130
        Me.Label23.Text = "Group Name (Cr) :"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.SystemColors.Control
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(576, 16)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(82, 13)
        Me.Label21.TabIndex = 129
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
        Me.lblHeadType.Location = New System.Drawing.Point(345, 16)
        Me.lblHeadType.Name = "lblHeadType"
        Me.lblHeadType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblHeadType.Size = New System.Drawing.Size(67, 13)
        Me.lblHeadType.TabIndex = 105
        Me.lblHeadType.Text = "Head Type :"
        Me.lblHeadType.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LblCategory
        '
        Me.LblCategory.BackColor = System.Drawing.SystemColors.Control
        Me.LblCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblCategory.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCategory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblCategory.Location = New System.Drawing.Point(20, 16)
        Me.LblCategory.Name = "LblCategory"
        Me.LblCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblCategory.Size = New System.Drawing.Size(92, 17)
        Me.LblCategory.TabIndex = 104
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
        Me.lblUnder.Location = New System.Drawing.Point(17, 45)
        Me.lblUnder.Name = "lblUnder"
        Me.lblUnder.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblUnder.Size = New System.Drawing.Size(101, 13)
        Me.lblUnder.TabIndex = 106
        Me.lblUnder.Text = "Group Name (Dr) :"
        Me.lblUnder.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame8
        '
        Me.Frame8.BackColor = System.Drawing.SystemColors.Control
        Me.Frame8.Controls.Add(Me.txtReceiptDays)
        Me.Frame8.Controls.Add(Me.Label19)
        Me.Frame8.Controls.Add(Me.txtShortName)
        Me.Frame8.Controls.Add(Me.Label66)
        Me.Frame8.Controls.Add(Me.cmdPaySearch)
        Me.Frame8.Controls.Add(Me.cboPaymentMode)
        Me.Frame8.Controls.Add(Me.txtChqFrequency)
        Me.Frame8.Controls.Add(Me.txtPayment)
        Me.Frame8.Controls.Add(Me.chkMonthWiseLdgr)
        Me.Frame8.Controls.Add(Me.txtVendorCode)
        Me.Frame8.Controls.Add(Me.txtPaidDay4)
        Me.Frame8.Controls.Add(Me.txtPaidDay3)
        Me.Frame8.Controls.Add(Me.txtPaidDay2)
        Me.Frame8.Controls.Add(Me.txtPaidDay)
        Me.Frame8.Controls.Add(Me.txtSeq)
        Me.Frame8.Controls.Add(Me.txtAlias)
        Me.Frame8.Controls.Add(Me.Label50)
        Me.Frame8.Controls.Add(Me.Label49)
        Me.Frame8.Controls.Add(Me.Label47)
        Me.Frame8.Controls.Add(Me.lblPaymentTerms)
        Me.Frame8.Controls.Add(Me.Label43)
        Me.Frame8.Controls.Add(Me.Label42)
        Me.Frame8.Controls.Add(Me.Label41)
        Me.Frame8.Controls.Add(Me.Label40)
        Me.Frame8.Controls.Add(Me.Label13)
        Me.Frame8.Controls.Add(Me.Label22)
        Me.Frame8.Controls.Add(Me.Label31)
        Me.Frame8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame8.Location = New System.Drawing.Point(0, 144)
        Me.Frame8.Name = "Frame8"
        Me.Frame8.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame8.Size = New System.Drawing.Size(1106, 76)
        Me.Frame8.TabIndex = 157
        Me.Frame8.TabStop = False
        '
        'txtReceiptDays
        '
        Me.txtReceiptDays.AcceptsReturn = True
        Me.txtReceiptDays.BackColor = System.Drawing.SystemColors.Window
        Me.txtReceiptDays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReceiptDays.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtReceiptDays.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReceiptDays.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtReceiptDays.Location = New System.Drawing.Point(1044, 43)
        Me.txtReceiptDays.MaxLength = 15
        Me.txtReceiptDays.Name = "txtReceiptDays"
        Me.txtReceiptDays.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtReceiptDays.Size = New System.Drawing.Size(45, 22)
        Me.txtReceiptDays.TabIndex = 235
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(980, 47)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(74, 13)
        Me.Label19.TabIndex = 236
        Me.Label19.Text = "Receipt Day :"
        '
        'txtShortName
        '
        Me.txtShortName.AcceptsReturn = True
        Me.txtShortName.BackColor = System.Drawing.SystemColors.Window
        Me.txtShortName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtShortName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtShortName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShortName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtShortName.Location = New System.Drawing.Point(421, 12)
        Me.txtShortName.MaxLength = 15
        Me.txtShortName.Name = "txtShortName"
        Me.txtShortName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtShortName.Size = New System.Drawing.Size(102, 22)
        Me.txtShortName.TabIndex = 16
        '
        'Label66
        '
        Me.Label66.AutoSize = True
        Me.Label66.BackColor = System.Drawing.Color.Transparent
        Me.Label66.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label66.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label66.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label66.Location = New System.Drawing.Point(346, 15)
        Me.Label66.Name = "Label66"
        Me.Label66.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label66.Size = New System.Drawing.Size(73, 13)
        Me.Label66.TabIndex = 234
        Me.Label66.Text = "Short Name :"
        '
        'cboPaymentMode
        '
        Me.cboPaymentMode.BackColor = System.Drawing.SystemColors.Window
        Me.cboPaymentMode.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboPaymentMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPaymentMode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPaymentMode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.cboPaymentMode.Location = New System.Drawing.Point(637, 13)
        Me.cboPaymentMode.Name = "cboPaymentMode"
        Me.cboPaymentMode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboPaymentMode.Size = New System.Drawing.Size(90, 21)
        Me.cboPaymentMode.TabIndex = 17
        '
        'txtChqFrequency
        '
        Me.txtChqFrequency.AcceptsReturn = True
        Me.txtChqFrequency.BackColor = System.Drawing.SystemColors.Window
        Me.txtChqFrequency.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtChqFrequency.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtChqFrequency.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChqFrequency.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtChqFrequency.Location = New System.Drawing.Point(809, 43)
        Me.txtChqFrequency.MaxLength = 15
        Me.txtChqFrequency.Name = "txtChqFrequency"
        Me.txtChqFrequency.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtChqFrequency.Size = New System.Drawing.Size(39, 22)
        Me.txtChqFrequency.TabIndex = 22
        '
        'txtPayment
        '
        Me.txtPayment.AcceptsReturn = True
        Me.txtPayment.BackColor = System.Drawing.SystemColors.Window
        Me.txtPayment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPayment.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPayment.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPayment.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPayment.Location = New System.Drawing.Point(809, 13)
        Me.txtPayment.MaxLength = 15
        Me.txtPayment.Name = "txtPayment"
        Me.txtPayment.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPayment.Size = New System.Drawing.Size(43, 22)
        Me.txtPayment.TabIndex = 24
        '
        'chkMonthWiseLdgr
        '
        Me.chkMonthWiseLdgr.AutoSize = True
        Me.chkMonthWiseLdgr.BackColor = System.Drawing.SystemColors.Control
        Me.chkMonthWiseLdgr.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkMonthWiseLdgr.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMonthWiseLdgr.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkMonthWiseLdgr.Location = New System.Drawing.Point(976, 14)
        Me.chkMonthWiseLdgr.Name = "chkMonthWiseLdgr"
        Me.chkMonthWiseLdgr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkMonthWiseLdgr.Size = New System.Drawing.Size(125, 17)
        Me.chkMonthWiseLdgr.TabIndex = 174
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
        Me.txtVendorCode.Location = New System.Drawing.Point(249, 13)
        Me.txtVendorCode.MaxLength = 15
        Me.txtVendorCode.Name = "txtVendorCode"
        Me.txtVendorCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVendorCode.Size = New System.Drawing.Size(90, 22)
        Me.txtVendorCode.TabIndex = 15
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
        Me.txtPaidDay4.Location = New System.Drawing.Point(615, 43)
        Me.txtPaidDay4.MaxLength = 15
        Me.txtPaidDay4.Name = "txtPaidDay4"
        Me.txtPaidDay4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPaidDay4.Size = New System.Drawing.Size(90, 22)
        Me.txtPaidDay4.TabIndex = 23
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
        Me.txtPaidDay3.Location = New System.Drawing.Point(421, 43)
        Me.txtPaidDay3.MaxLength = 15
        Me.txtPaidDay3.Name = "txtPaidDay3"
        Me.txtPaidDay3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPaidDay3.Size = New System.Drawing.Size(102, 22)
        Me.txtPaidDay3.TabIndex = 21
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
        Me.txtPaidDay2.Location = New System.Drawing.Point(247, 43)
        Me.txtPaidDay2.MaxLength = 15
        Me.txtPaidDay2.Name = "txtPaidDay2"
        Me.txtPaidDay2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPaidDay2.Size = New System.Drawing.Size(90, 22)
        Me.txtPaidDay2.TabIndex = 20
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
        Me.txtPaidDay.Location = New System.Drawing.Point(71, 43)
        Me.txtPaidDay.MaxLength = 15
        Me.txtPaidDay.Name = "txtPaidDay"
        Me.txtPaidDay.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPaidDay.Size = New System.Drawing.Size(90, 22)
        Me.txtPaidDay.TabIndex = 19
        '
        'txtSeq
        '
        Me.txtSeq.AcceptsReturn = True
        Me.txtSeq.BackColor = System.Drawing.SystemColors.Window
        Me.txtSeq.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSeq.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSeq.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSeq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSeq.Location = New System.Drawing.Point(920, 43)
        Me.txtSeq.MaxLength = 15
        Me.txtSeq.Name = "txtSeq"
        Me.txtSeq.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSeq.Size = New System.Drawing.Size(45, 22)
        Me.txtSeq.TabIndex = 16
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
        Me.txtAlias.Location = New System.Drawing.Point(71, 13)
        Me.txtAlias.MaxLength = 15
        Me.txtAlias.Name = "txtAlias"
        Me.txtAlias.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAlias.Size = New System.Drawing.Size(90, 22)
        Me.txtAlias.TabIndex = 14
        '
        'Label50
        '
        Me.Label50.BackColor = System.Drawing.SystemColors.Control
        Me.Label50.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label50.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label50.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label50.Location = New System.Drawing.Point(531, 16)
        Me.Label50.Name = "Label50"
        Me.Label50.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label50.Size = New System.Drawing.Size(104, 17)
        Me.Label50.TabIndex = 195
        Me.Label50.Text = "Mode of Payment :"
        '
        'Label49
        '
        Me.Label49.AutoSize = True
        Me.Label49.BackColor = System.Drawing.Color.Transparent
        Me.Label49.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label49.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label49.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label49.Location = New System.Drawing.Point(721, 47)
        Me.Label49.Name = "Label49"
        Me.Label49.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label49.Size = New System.Drawing.Size(86, 13)
        Me.Label49.TabIndex = 193
        Me.Label49.Text = "Chqs in Month :"
        Me.Label49.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.BackColor = System.Drawing.Color.Transparent
        Me.Label47.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label47.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label47.ForeColor = System.Drawing.Color.Black
        Me.Label47.Location = New System.Drawing.Point(742, 18)
        Me.Label47.Name = "Label47"
        Me.Label47.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label47.Size = New System.Drawing.Size(63, 13)
        Me.Label47.TabIndex = 185
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
        Me.lblPaymentTerms.Location = New System.Drawing.Point(881, 13)
        Me.lblPaymentTerms.Name = "lblPaymentTerms"
        Me.lblPaymentTerms.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPaymentTerms.Size = New System.Drawing.Size(85, 21)
        Me.lblPaymentTerms.TabIndex = 26
        Me.lblPaymentTerms.Text = "lblPaymentTerms"
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.BackColor = System.Drawing.Color.Transparent
        Me.Label43.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label43.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label43.Location = New System.Drawing.Point(167, 16)
        Me.Label43.Name = "Label43"
        Me.Label43.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label43.Size = New System.Drawing.Size(79, 13)
        Me.Label43.TabIndex = 173
        Me.Label43.Text = "Vendor Code :"
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.BackColor = System.Drawing.Color.Transparent
        Me.Label42.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label42.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label42.Location = New System.Drawing.Point(547, 47)
        Me.Label42.Name = "Label42"
        Me.Label42.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label42.Size = New System.Drawing.Size(67, 13)
        Me.Label42.TabIndex = 171
        Me.Label42.Text = "Paid Day 4 :"
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label42.Visible = False
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.BackColor = System.Drawing.Color.Transparent
        Me.Label41.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label41.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label41.Location = New System.Drawing.Point(355, 46)
        Me.Label41.Name = "Label41"
        Me.Label41.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label41.Size = New System.Drawing.Size(64, 13)
        Me.Label41.TabIndex = 170
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
        Me.Label40.Location = New System.Drawing.Point(177, 48)
        Me.Label40.Name = "Label40"
        Me.Label40.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label40.Size = New System.Drawing.Size(67, 13)
        Me.Label40.TabIndex = 169
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
        Me.Label13.Location = New System.Drawing.Point(2, 48)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(62, 13)
        Me.Label13.TabIndex = 160
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
        Me.Label22.Location = New System.Drawing.Point(856, 47)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(61, 13)
        Me.Label22.TabIndex = 159
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
        Me.Label31.Size = New System.Drawing.Size(37, 13)
        Me.Label31.TabIndex = 158
        Me.Label31.Text = "Alias :"
        '
        'FraView
        '
        Me.FraView.BackColor = System.Drawing.SystemColors.Control
        Me.FraView.Controls.Add(Me.SSTInfo)
        Me.FraView.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraView.Location = New System.Drawing.Point(0, 216)
        Me.FraView.Name = "FraView"
        Me.FraView.Padding = New System.Windows.Forms.Padding(0)
        Me.FraView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraView.Size = New System.Drawing.Size(1106, 276)
        Me.FraView.TabIndex = 107
        Me.FraView.TabStop = False
        '
        'SSTInfo
        '
        Me.SSTInfo.Controls.Add(Me._SSTInfo_TabPage0)
        Me.SSTInfo.Controls.Add(Me._SSTInfo_TabPage9)
        Me.SSTInfo.Controls.Add(Me._SSTInfo_TabPage1)
        Me.SSTInfo.Controls.Add(Me._SSTInfo_TabPage2)
        Me.SSTInfo.Controls.Add(Me._SSTInfo_TabPage3)
        Me.SSTInfo.Controls.Add(Me._SSTInfo_TabPage4)
        Me.SSTInfo.Controls.Add(Me._SSTInfo_TabPage6)
        Me.SSTInfo.Controls.Add(Me._SSTInfo_TabPage7)
        Me.SSTInfo.Controls.Add(Me._SSTInfo_TabPage8)
        Me.SSTInfo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SSTInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.SSTInfo.ItemSize = New System.Drawing.Size(42, 18)
        Me.SSTInfo.Location = New System.Drawing.Point(2, 11)
        Me.SSTInfo.Name = "SSTInfo"
        Me.SSTInfo.SelectedIndex = 8
        Me.SSTInfo.Size = New System.Drawing.Size(1104, 261)
        Me.SSTInfo.TabIndex = 108
        '
        '_SSTInfo_TabPage0
        '
        Me._SSTInfo_TabPage0.Controls.Add(Me.chkGroupLimit)
        Me._SSTInfo_TabPage0.Controls.Add(Me.Frame4)
        Me._SSTInfo_TabPage0.Controls.Add(Me.Frame5)
        Me._SSTInfo_TabPage0.Controls.Add(Me.Frame3)
        Me._SSTInfo_TabPage0.Location = New System.Drawing.Point(4, 22)
        Me._SSTInfo_TabPage0.Name = "_SSTInfo_TabPage0"
        Me._SSTInfo_TabPage0.Size = New System.Drawing.Size(1096, 235)
        Me._SSTInfo_TabPage0.TabIndex = 0
        Me._SSTInfo_TabPage0.Text = "Business Adress"
        '
        'chkGroupLimit
        '
        Me.chkGroupLimit.AutoSize = True
        Me.chkGroupLimit.BackColor = System.Drawing.SystemColors.Control
        Me.chkGroupLimit.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkGroupLimit.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGroupLimit.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkGroupLimit.Location = New System.Drawing.Point(898, 87)
        Me.chkGroupLimit.Name = "chkGroupLimit"
        Me.chkGroupLimit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkGroupLimit.Size = New System.Drawing.Size(98, 21)
        Me.chkGroupLimit.TabIndex = 188
        Me.chkGroupLimit.Text = "Group Limit"
        Me.chkGroupLimit.UseVisualStyleBackColor = False
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.txtResponsiblePerson)
        Me.Frame4.Controls.Add(Me.Label33)
        Me.Frame4.Controls.Add(Me.txtCreditLimit)
        Me.Frame4.Controls.Add(Me.Label30)
        Me.Frame4.Controls.Add(Me.txtCompanyName)
        Me.Frame4.Controls.Add(Me.Label29)
        Me.Frame4.Controls.Add(Me.txtDistance)
        Me.Frame4.Controls.Add(Me.txtaddress)
        Me.Frame4.Controls.Add(Me.txtState)
        Me.Frame4.Controls.Add(Me.txtPinCode)
        Me.Frame4.Controls.Add(Me.txtCity)
        Me.Frame4.Controls.Add(Me.txtCountry)
        Me.Frame4.Controls.Add(Me.Label56)
        Me.Frame4.Controls.Add(Me.Label51)
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
        Me.Frame4.Location = New System.Drawing.Point(0, 3)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(890, 231)
        Me.Frame4.TabIndex = 151
        Me.Frame4.TabStop = False
        '
        'txtResponsiblePerson
        '
        Me.txtResponsiblePerson.AcceptsReturn = True
        Me.txtResponsiblePerson.BackColor = System.Drawing.SystemColors.Window
        Me.txtResponsiblePerson.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtResponsiblePerson.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtResponsiblePerson.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtResponsiblePerson.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtResponsiblePerson.Location = New System.Drawing.Point(61, 160)
        Me.txtResponsiblePerson.MaxLength = 0
        Me.txtResponsiblePerson.Name = "txtResponsiblePerson"
        Me.txtResponsiblePerson.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtResponsiblePerson.Size = New System.Drawing.Size(313, 22)
        Me.txtResponsiblePerson.TabIndex = 224
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.BackColor = System.Drawing.SystemColors.Control
        Me.Label33.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label33.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label33.Location = New System.Drawing.Point(0, 140)
        Me.Label33.Name = "Label33"
        Me.Label33.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label33.Size = New System.Drawing.Size(94, 13)
        Me.Label33.TabIndex = 225
        Me.Label33.Text = "Our Sales Person:"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtCreditLimit
        '
        Me.txtCreditLimit.AcceptsReturn = True
        Me.txtCreditLimit.BackColor = System.Drawing.SystemColors.Window
        Me.txtCreditLimit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCreditLimit.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCreditLimit.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCreditLimit.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtCreditLimit.Location = New System.Drawing.Point(775, 136)
        Me.txtCreditLimit.MaxLength = 15
        Me.txtCreditLimit.Name = "txtCreditLimit"
        Me.txtCreditLimit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCreditLimit.Size = New System.Drawing.Size(109, 22)
        Me.txtCreditLimit.TabIndex = 33
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.BackColor = System.Drawing.Color.Transparent
        Me.Label30.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label30.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label30.Location = New System.Drawing.Point(667, 141)
        Me.Label30.Name = "Label30"
        Me.Label30.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label30.Size = New System.Drawing.Size(110, 13)
        Me.Label30.TabIndex = 223
        Me.Label30.Text = "Credit Limit (in Rs.) :"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtCompanyName
        '
        Me.txtCompanyName.AcceptsReturn = True
        Me.txtCompanyName.BackColor = System.Drawing.SystemColors.Window
        Me.txtCompanyName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCompanyName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCompanyName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompanyName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCompanyName.Location = New System.Drawing.Point(529, 164)
        Me.txtCompanyName.MaxLength = 0
        Me.txtCompanyName.Name = "txtCompanyName"
        Me.txtCompanyName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCompanyName.Size = New System.Drawing.Size(355, 22)
        Me.txtCompanyName.TabIndex = 32
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.SystemColors.Control
        Me.Label29.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label29.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label29.Location = New System.Drawing.Point(359, 167)
        Me.Label29.Name = "Label29"
        Me.Label29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label29.Size = New System.Drawing.Size(167, 15)
        Me.Label29.TabIndex = 221
        Me.Label29.Text = "Inter Unit Company Name :"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtDistance
        '
        Me.txtDistance.AcceptsReturn = True
        Me.txtDistance.BackColor = System.Drawing.SystemColors.Window
        Me.txtDistance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDistance.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDistance.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDistance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDistance.Location = New System.Drawing.Point(529, 136)
        Me.txtDistance.MaxLength = 15
        Me.txtDistance.Name = "txtDistance"
        Me.txtDistance.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDistance.Size = New System.Drawing.Size(73, 22)
        Me.txtDistance.TabIndex = 32
        '
        'txtaddress
        '
        Me.txtaddress.AcceptsReturn = True
        Me.txtaddress.BackColor = System.Drawing.SystemColors.Window
        Me.txtaddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtaddress.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtaddress.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtaddress.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtaddress.Location = New System.Drawing.Point(61, 16)
        Me.txtaddress.MaxLength = 0
        Me.txtaddress.Multiline = True
        Me.txtaddress.Name = "txtaddress"
        Me.txtaddress.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtaddress.Size = New System.Drawing.Size(372, 121)
        Me.txtaddress.TabIndex = 27
        '
        'txtState
        '
        Me.txtState.AcceptsReturn = True
        Me.txtState.BackColor = System.Drawing.SystemColors.Window
        Me.txtState.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtState.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtState.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtState.Location = New System.Drawing.Point(529, 46)
        Me.txtState.MaxLength = 15
        Me.txtState.Name = "txtState"
        Me.txtState.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtState.Size = New System.Drawing.Size(355, 22)
        Me.txtState.TabIndex = 29
        '
        'txtPinCode
        '
        Me.txtPinCode.AcceptsReturn = True
        Me.txtPinCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtPinCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPinCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPinCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPinCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPinCode.Location = New System.Drawing.Point(529, 76)
        Me.txtPinCode.MaxLength = 7
        Me.txtPinCode.Name = "txtPinCode"
        Me.txtPinCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPinCode.Size = New System.Drawing.Size(355, 22)
        Me.txtPinCode.TabIndex = 30
        '
        'txtCity
        '
        Me.txtCity.AcceptsReturn = True
        Me.txtCity.BackColor = System.Drawing.SystemColors.Window
        Me.txtCity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCity.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCity.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCity.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtCity.Location = New System.Drawing.Point(529, 16)
        Me.txtCity.MaxLength = 15
        Me.txtCity.Name = "txtCity"
        Me.txtCity.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCity.Size = New System.Drawing.Size(355, 22)
        Me.txtCity.TabIndex = 28
        '
        'txtCountry
        '
        Me.txtCountry.AcceptsReturn = True
        Me.txtCountry.BackColor = System.Drawing.SystemColors.Window
        Me.txtCountry.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCountry.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCountry.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCountry.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtCountry.Location = New System.Drawing.Point(529, 106)
        Me.txtCountry.MaxLength = 15
        Me.txtCountry.Name = "txtCountry"
        Me.txtCountry.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCountry.Size = New System.Drawing.Size(355, 22)
        Me.txtCountry.TabIndex = 31
        '
        'Label56
        '
        Me.Label56.AutoSize = True
        Me.Label56.BackColor = System.Drawing.Color.Transparent
        Me.Label56.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label56.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label56.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label56.Location = New System.Drawing.Point(605, 140)
        Me.Label56.Name = "Label56"
        Me.Label56.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label56.Size = New System.Drawing.Size(44, 13)
        Me.Label56.TabIndex = 219
        Me.Label56.Text = "(in KM)"
        Me.Label56.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label51
        '
        Me.Label51.AutoSize = True
        Me.Label51.BackColor = System.Drawing.Color.Transparent
        Me.Label51.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label51.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label51.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label51.Location = New System.Drawing.Point(470, 141)
        Me.Label51.Name = "Label51"
        Me.Label51.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label51.Size = New System.Drawing.Size(56, 13)
        Me.Label51.TabIndex = 218
        Me.Label51.Text = "Distance :"
        Me.Label51.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblModDate
        '
        Me.lblModDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblModDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblModDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblModDate.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblModDate.Location = New System.Drawing.Point(770, 196)
        Me.lblModDate.Name = "lblModDate"
        Me.lblModDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblModDate.Size = New System.Drawing.Size(108, 19)
        Me.lblModDate.TabIndex = 182
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.BackColor = System.Drawing.SystemColors.Control
        Me.Label48.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label48.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label48.Location = New System.Drawing.Point(705, 199)
        Me.Label48.Name = "Label48"
        Me.Label48.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label48.Size = New System.Drawing.Size(63, 15)
        Me.Label48.TabIndex = 181
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
        Me.lblAddDate.Location = New System.Drawing.Point(304, 196)
        Me.lblAddDate.Name = "lblAddDate"
        Me.lblAddDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAddDate.Size = New System.Drawing.Size(108, 19)
        Me.lblAddDate.TabIndex = 180
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.BackColor = System.Drawing.SystemColors.Control
        Me.Label45.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label45.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label45.Location = New System.Drawing.Point(238, 198)
        Me.Label45.Name = "Label45"
        Me.Label45.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label45.Size = New System.Drawing.Size(63, 15)
        Me.Label45.TabIndex = 179
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
        Me.lblModUser.Location = New System.Drawing.Point(560, 196)
        Me.lblModUser.Name = "lblModUser"
        Me.lblModUser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblModUser.Size = New System.Drawing.Size(108, 19)
        Me.lblModUser.TabIndex = 178
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.BackColor = System.Drawing.SystemColors.Control
        Me.Label46.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label46.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label46.Location = New System.Drawing.Point(498, 198)
        Me.Label46.Name = "Label46"
        Me.Label46.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label46.Size = New System.Drawing.Size(61, 15)
        Me.Label46.TabIndex = 177
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
        Me.lblAddUser.Location = New System.Drawing.Point(70, 196)
        Me.lblAddUser.Name = "lblAddUser"
        Me.lblAddUser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAddUser.Size = New System.Drawing.Size(108, 19)
        Me.lblAddUser.TabIndex = 176
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.BackColor = System.Drawing.SystemColors.Control
        Me.Label44.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label44.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label44.Location = New System.Drawing.Point(7, 198)
        Me.Label44.Name = "Label44"
        Me.Label44.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label44.Size = New System.Drawing.Size(61, 15)
        Me.Label44.TabIndex = 175
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
        Me._Label25_1.Location = New System.Drawing.Point(498, 80)
        Me._Label25_1.Name = "_Label25_1"
        Me._Label25_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label25_1.Size = New System.Drawing.Size(28, 13)
        Me._Label25_1.TabIndex = 156
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
        Me.Label9.Size = New System.Drawing.Size(54, 13)
        Me.Label9.TabIndex = 155
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
        Me.Label10.Location = New System.Drawing.Point(487, 48)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(39, 13)
        Me.Label10.TabIndex = 154
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
        Me.Label27.Location = New System.Drawing.Point(493, 21)
        Me.Label27.Name = "Label27"
        Me.Label27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label27.Size = New System.Drawing.Size(33, 13)
        Me.Label27.TabIndex = 153
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
        Me.Label39.Location = New System.Drawing.Point(473, 110)
        Me.Label39.Name = "Label39"
        Me.Label39.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label39.Size = New System.Drawing.Size(53, 13)
        Me.Label39.TabIndex = 152
        Me.Label39.Text = "Country :"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.TopRight
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
        Me.Frame5.Location = New System.Drawing.Point(890, 2)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(205, 83)
        Me.Frame5.TabIndex = 150
        Me.Frame5.TabStop = False
        '
        'chkAuthorised
        '
        Me.chkAuthorised.AutoSize = True
        Me.chkAuthorised.BackColor = System.Drawing.SystemColors.Control
        Me.chkAuthorised.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAuthorised.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAuthorised.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkAuthorised.Location = New System.Drawing.Point(6, 60)
        Me.chkAuthorised.Name = "chkAuthorised"
        Me.chkAuthorised.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAuthorised.Size = New System.Drawing.Size(94, 21)
        Me.chkAuthorised.TabIndex = 186
        Me.chkAuthorised.Text = "Authorised"
        Me.chkAuthorised.UseVisualStyleBackColor = False
        '
        'chkInterUnit
        '
        Me.chkInterUnit.AutoSize = True
        Me.chkInterUnit.BackColor = System.Drawing.SystemColors.Control
        Me.chkInterUnit.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkInterUnit.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkInterUnit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkInterUnit.Location = New System.Drawing.Point(114, 38)
        Me.chkInterUnit.Name = "chkInterUnit"
        Me.chkInterUnit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkInterUnit.Size = New System.Drawing.Size(73, 17)
        Me.chkInterUnit.TabIndex = 184
        Me.chkInterUnit.Text = "Inter Unit"
        Me.chkInterUnit.UseVisualStyleBackColor = False
        '
        'chkDistt
        '
        Me.chkDistt.AutoSize = True
        Me.chkDistt.BackColor = System.Drawing.SystemColors.Control
        Me.chkDistt.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkDistt.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDistt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkDistt.Location = New System.Drawing.Point(114, 16)
        Me.chkDistt.Name = "chkDistt"
        Me.chkDistt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkDistt.Size = New System.Drawing.Size(86, 17)
        Me.chkDistt.TabIndex = 34
        Me.chkDistt.Text = "Within Distt"
        Me.chkDistt.UseVisualStyleBackColor = False
        '
        'chkState
        '
        Me.chkState.AutoSize = True
        Me.chkState.BackColor = System.Drawing.SystemColors.Control
        Me.chkState.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkState.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkState.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkState.Location = New System.Drawing.Point(6, 14)
        Me.chkState.Name = "chkState"
        Me.chkState.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkState.Size = New System.Drawing.Size(88, 17)
        Me.chkState.TabIndex = 33
        Me.chkState.Text = "Within State"
        Me.chkState.UseVisualStyleBackColor = False
        '
        'chkCountry
        '
        Me.chkCountry.AutoSize = True
        Me.chkCountry.BackColor = System.Drawing.SystemColors.Control
        Me.chkCountry.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCountry.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCountry.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCountry.Location = New System.Drawing.Point(6, 38)
        Me.chkCountry.Name = "chkCountry"
        Me.chkCountry.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCountry.Size = New System.Drawing.Size(102, 17)
        Me.chkCountry.TabIndex = 35
        Me.chkCountry.Text = "Within Country"
        Me.chkCountry.UseVisualStyleBackColor = False
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.chkStopPO)
        Me.Frame3.Controls.Add(Me.chkStopGP)
        Me.Frame3.Controls.Add(Me.chkStopMRR)
        Me.Frame3.Controls.Add(Me.chkStopInvoice)
        Me.Frame3.Controls.Add(Me.chkStopBP)
        Me.Frame3.Controls.Add(Me.ChkPoRate)
        Me.Frame3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(892, 102)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(205, 129)
        Me.Frame3.TabIndex = 187
        Me.Frame3.TabStop = False
        '
        'chkStopPO
        '
        Me.chkStopPO.AutoSize = True
        Me.chkStopPO.BackColor = System.Drawing.SystemColors.Control
        Me.chkStopPO.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkStopPO.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkStopPO.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkStopPO.Location = New System.Drawing.Point(6, 94)
        Me.chkStopPO.Name = "chkStopPO"
        Me.chkStopPO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkStopPO.Size = New System.Drawing.Size(165, 17)
        Me.chkStopPO.TabIndex = 192
        Me.chkStopPO.Text = "Stop PO/ Delivery Schedule"
        Me.chkStopPO.UseVisualStyleBackColor = False
        '
        'chkStopGP
        '
        Me.chkStopGP.AutoSize = True
        Me.chkStopGP.BackColor = System.Drawing.SystemColors.Control
        Me.chkStopGP.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkStopGP.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkStopGP.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkStopGP.Location = New System.Drawing.Point(6, 54)
        Me.chkStopGP.Name = "chkStopGP"
        Me.chkStopGP.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkStopGP.Size = New System.Drawing.Size(99, 17)
        Me.chkStopGP.TabIndex = 190
        Me.chkStopGP.Text = "Stop GatePass"
        Me.chkStopGP.UseVisualStyleBackColor = False
        '
        'chkStopMRR
        '
        Me.chkStopMRR.AutoSize = True
        Me.chkStopMRR.BackColor = System.Drawing.SystemColors.Control
        Me.chkStopMRR.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkStopMRR.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkStopMRR.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkStopMRR.Location = New System.Drawing.Point(6, 14)
        Me.chkStopMRR.Name = "chkStopMRR"
        Me.chkStopMRR.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkStopMRR.Size = New System.Drawing.Size(106, 17)
        Me.chkStopMRR.TabIndex = 188
        Me.chkStopMRR.Text = "Stop MRR Entry"
        Me.chkStopMRR.UseVisualStyleBackColor = False
        '
        'chkStopInvoice
        '
        Me.chkStopInvoice.AutoSize = True
        Me.chkStopInvoice.BackColor = System.Drawing.SystemColors.Control
        Me.chkStopInvoice.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkStopInvoice.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkStopInvoice.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkStopInvoice.Location = New System.Drawing.Point(6, 34)
        Me.chkStopInvoice.Name = "chkStopInvoice"
        Me.chkStopInvoice.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkStopInvoice.Size = New System.Drawing.Size(89, 17)
        Me.chkStopInvoice.TabIndex = 189
        Me.chkStopInvoice.Text = "Stop Invoice"
        Me.chkStopInvoice.UseVisualStyleBackColor = False
        '
        'chkStopBP
        '
        Me.chkStopBP.AutoSize = True
        Me.chkStopBP.BackColor = System.Drawing.SystemColors.Control
        Me.chkStopBP.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkStopBP.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkStopBP.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkStopBP.Location = New System.Drawing.Point(6, 74)
        Me.chkStopBP.Name = "chkStopBP"
        Me.chkStopBP.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkStopBP.Size = New System.Drawing.Size(125, 17)
        Me.chkStopBP.TabIndex = 191
        Me.chkStopBP.Text = "Stop Bank Payment"
        Me.chkStopBP.UseVisualStyleBackColor = False
        '
        'ChkPoRate
        '
        Me.ChkPoRate.AutoSize = True
        Me.ChkPoRate.BackColor = System.Drawing.SystemColors.Control
        Me.ChkPoRate.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkPoRate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkPoRate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkPoRate.Location = New System.Drawing.Point(6, 114)
        Me.ChkPoRate.Name = "ChkPoRate"
        Me.ChkPoRate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkPoRate.Size = New System.Drawing.Size(111, 17)
        Me.ChkPoRate.TabIndex = 18
        Me.ChkPoRate.Text = "PO Rate Editable"
        Me.ChkPoRate.UseVisualStyleBackColor = False
        Me.ChkPoRate.Visible = False
        '
        '_SSTInfo_TabPage9
        '
        Me._SSTInfo_TabPage9.Controls.Add(Me.SprdMain)
        Me._SSTInfo_TabPage9.Location = New System.Drawing.Point(4, 22)
        Me._SSTInfo_TabPage9.Name = "_SSTInfo_TabPage9"
        Me._SSTInfo_TabPage9.Size = New System.Drawing.Size(1096, 235)
        Me._SSTInfo_TabPage9.TabIndex = 9
        Me._SSTInfo_TabPage9.Text = "Add More Business Address"
        Me._SSTInfo_TabPage9.UseVisualStyleBackColor = True
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SprdMain.Location = New System.Drawing.Point(0, 0)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(1096, 235)
        Me.SprdMain.TabIndex = 21
        '
        '_SSTInfo_TabPage1
        '
        Me._SSTInfo_TabPage1.Controls.Add(Me.Frame2)
        Me._SSTInfo_TabPage1.Location = New System.Drawing.Point(4, 22)
        Me._SSTInfo_TabPage1.Name = "_SSTInfo_TabPage1"
        Me._SSTInfo_TabPage1.Size = New System.Drawing.Size(1096, 235)
        Me._SSTInfo_TabPage1.TabIndex = 1
        Me._SSTInfo_TabPage1.Text = "Communicatio&n"
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.cboNature)
        Me.Frame2.Controls.Add(Me.txtPolicyNo)
        Me.Frame2.Controls.Add(Me.txtContact)
        Me.Frame2.Controls.Add(Me.txtFax)
        Me.Frame2.Controls.Add(Me.txtMobile)
        Me.Frame2.Controls.Add(Me.txtEmail)
        Me.Frame2.Controls.Add(Me.txtPhone)
        Me.Frame2.Controls.Add(Me.Label63)
        Me.Frame2.Controls.Add(Me._Label25_7)
        Me.Frame2.Controls.Add(Me.Label6)
        Me.Frame2.Controls.Add(Me.Label5)
        Me.Frame2.Controls.Add(Me.Label3)
        Me.Frame2.Controls.Add(Me.Label1)
        Me.Frame2.Controls.Add(Me._Label25_0)
        Me.Frame2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(4, 0)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(1092, 230)
        Me.Frame2.TabIndex = 115
        Me.Frame2.TabStop = False
        '
        'cboNature
        '
        Me.cboNature.BackColor = System.Drawing.SystemColors.Window
        Me.cboNature.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboNature.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboNature.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboNature.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.cboNature.Location = New System.Drawing.Point(179, 196)
        Me.cboNature.Name = "cboNature"
        Me.cboNature.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboNature.Size = New System.Drawing.Size(479, 21)
        Me.cboNature.TabIndex = 42
        '
        'txtPolicyNo
        '
        Me.txtPolicyNo.AcceptsReturn = True
        Me.txtPolicyNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPolicyNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPolicyNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPolicyNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPolicyNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPolicyNo.Location = New System.Drawing.Point(179, 166)
        Me.txtPolicyNo.MaxLength = 7
        Me.txtPolicyNo.Name = "txtPolicyNo"
        Me.txtPolicyNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPolicyNo.Size = New System.Drawing.Size(479, 22)
        Me.txtPolicyNo.TabIndex = 41
        '
        'txtContact
        '
        Me.txtContact.AcceptsReturn = True
        Me.txtContact.BackColor = System.Drawing.SystemColors.Window
        Me.txtContact.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtContact.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtContact.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtContact.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtContact.Location = New System.Drawing.Point(179, 76)
        Me.txtContact.MaxLength = 15
        Me.txtContact.Name = "txtContact"
        Me.txtContact.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtContact.Size = New System.Drawing.Size(479, 22)
        Me.txtContact.TabIndex = 40
        '
        'txtFax
        '
        Me.txtFax.AcceptsReturn = True
        Me.txtFax.BackColor = System.Drawing.SystemColors.Window
        Me.txtFax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFax.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFax.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFax.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtFax.Location = New System.Drawing.Point(179, 136)
        Me.txtFax.MaxLength = 15
        Me.txtFax.Name = "txtFax"
        Me.txtFax.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFax.Size = New System.Drawing.Size(479, 22)
        Me.txtFax.TabIndex = 37
        '
        'txtMobile
        '
        Me.txtMobile.AcceptsReturn = True
        Me.txtMobile.BackColor = System.Drawing.SystemColors.Window
        Me.txtMobile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMobile.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMobile.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMobile.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtMobile.Location = New System.Drawing.Point(179, 106)
        Me.txtMobile.MaxLength = 7
        Me.txtMobile.Name = "txtMobile"
        Me.txtMobile.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMobile.Size = New System.Drawing.Size(479, 22)
        Me.txtMobile.TabIndex = 39
        '
        'txtEmail
        '
        Me.txtEmail.AcceptsReturn = True
        Me.txtEmail.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmail.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmail.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtEmail.Location = New System.Drawing.Point(179, 46)
        Me.txtEmail.MaxLength = 15
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmail.Size = New System.Drawing.Size(479, 22)
        Me.txtEmail.TabIndex = 38
        '
        'txtPhone
        '
        Me.txtPhone.AcceptsReturn = True
        Me.txtPhone.BackColor = System.Drawing.SystemColors.Window
        Me.txtPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPhone.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPhone.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPhone.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPhone.Location = New System.Drawing.Point(179, 16)
        Me.txtPhone.MaxLength = 0
        Me.txtPhone.Multiline = True
        Me.txtPhone.Name = "txtPhone"
        Me.txtPhone.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPhone.Size = New System.Drawing.Size(479, 19)
        Me.txtPhone.TabIndex = 36
        '
        'Label63
        '
        Me.Label63.AutoSize = True
        Me.Label63.BackColor = System.Drawing.SystemColors.Control
        Me.Label63.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label63.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label63.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label63.Location = New System.Drawing.Point(130, 199)
        Me.Label63.Name = "Label63"
        Me.Label63.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label63.Size = New System.Drawing.Size(44, 13)
        Me.Label63.TabIndex = 229
        Me.Label63.Text = "Nature:"
        Me.Label63.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label25_7
        '
        Me._Label25_7.AutoSize = True
        Me._Label25_7.BackColor = System.Drawing.SystemColors.Control
        Me._Label25_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label25_7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label25_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label25.SetIndex(Me._Label25_7, CType(7, Short))
        Me._Label25_7.Location = New System.Drawing.Point(68, 170)
        Me._Label25_7.Name = "_Label25_7"
        Me._Label25_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label25_7.Size = New System.Drawing.Size(107, 13)
        Me._Label25_7.TabIndex = 208
        Me._Label25_7.Text = "Policy No && Details:"
        Me._Label25_7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(123, 79)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(52, 13)
        Me.Label6.TabIndex = 120
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
        Me.Label5.Location = New System.Drawing.Point(127, 140)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(49, 13)
        Me.Label5.TabIndex = 119
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
        Me.Label3.Location = New System.Drawing.Point(130, 50)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(45, 13)
        Me.Label3.TabIndex = 118
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
        Me.Label1.Location = New System.Drawing.Point(116, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(59, 13)
        Me.Label1.TabIndex = 117
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
        Me._Label25_0.Location = New System.Drawing.Point(109, 110)
        Me._Label25_0.Name = "_Label25_0"
        Me._Label25_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label25_0.Size = New System.Drawing.Size(67, 13)
        Me._Label25_0.TabIndex = 116
        Me._Label25_0.Text = "Mobile No :"
        Me._Label25_0.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_SSTInfo_TabPage2
        '
        Me._SSTInfo_TabPage2.Controls.Add(Me.Frame6)
        Me._SSTInfo_TabPage2.Location = New System.Drawing.Point(4, 22)
        Me._SSTInfo_TabPage2.Name = "_SSTInfo_TabPage2"
        Me._SSTInfo_TabPage2.Size = New System.Drawing.Size(1096, 235)
        Me._SSTInfo_TabPage2.TabIndex = 2
        Me._SSTInfo_TabPage2.Text = "Taxes"
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.Frame16)
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
        Me.Frame6.Location = New System.Drawing.Point(2, 0)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(1094, 232)
        Me.Frame6.TabIndex = 109
        Me.Frame6.TabStop = False
        '
        'Frame16
        '
        Me.Frame16.BackColor = System.Drawing.SystemColors.Control
        Me.Frame16.Controls.Add(Me.chkSMEStatus)
        Me.Frame16.Controls.Add(Me.chkSMERegd)
        Me.Frame16.Controls.Add(Me.txtUdyogAahaarNo)
        Me.Frame16.Controls.Add(Me.cboSymbol)
        Me.Frame16.Controls.Add(Me.cboEnterpriseType)
        Me.Frame16.Controls.Add(Me.Label61)
        Me.Frame16.Controls.Add(Me.Label60)
        Me.Frame16.Controls.Add(Me.Label59)
        Me.Frame16.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame16.Location = New System.Drawing.Point(2, 138)
        Me.Frame16.Name = "Frame16"
        Me.Frame16.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame16.Size = New System.Drawing.Size(737, 90)
        Me.Frame16.TabIndex = 221
        Me.Frame16.TabStop = False
        Me.Frame16.Text = "SME Details"
        '
        'chkSMEStatus
        '
        Me.chkSMEStatus.BackColor = System.Drawing.SystemColors.Control
        Me.chkSMEStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkSMEStatus.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSMEStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkSMEStatus.Location = New System.Drawing.Point(604, 44)
        Me.chkSMEStatus.Name = "chkSMEStatus"
        Me.chkSMEStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkSMEStatus.Size = New System.Drawing.Size(125, 20)
        Me.chkSMEStatus.TabIndex = 226
        Me.chkSMEStatus.Text = "SME Status"
        Me.chkSMEStatus.UseVisualStyleBackColor = False
        '
        'chkSMERegd
        '
        Me.chkSMERegd.BackColor = System.Drawing.SystemColors.Control
        Me.chkSMERegd.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkSMERegd.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSMERegd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkSMERegd.Location = New System.Drawing.Point(478, 44)
        Me.chkSMERegd.Name = "chkSMERegd"
        Me.chkSMERegd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkSMERegd.Size = New System.Drawing.Size(119, 20)
        Me.chkSMERegd.TabIndex = 225
        Me.chkSMERegd.Text = "SME Registed"
        Me.chkSMERegd.UseVisualStyleBackColor = False
        '
        'txtUdyogAahaarNo
        '
        Me.txtUdyogAahaarNo.AcceptsReturn = True
        Me.txtUdyogAahaarNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtUdyogAahaarNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUdyogAahaarNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUdyogAahaarNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUdyogAahaarNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtUdyogAahaarNo.Location = New System.Drawing.Point(121, 44)
        Me.txtUdyogAahaarNo.MaxLength = 15
        Me.txtUdyogAahaarNo.Name = "txtUdyogAahaarNo"
        Me.txtUdyogAahaarNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUdyogAahaarNo.Size = New System.Drawing.Size(253, 22)
        Me.txtUdyogAahaarNo.TabIndex = 51
        '
        'cboSymbol
        '
        Me.cboSymbol.BackColor = System.Drawing.SystemColors.Window
        Me.cboSymbol.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboSymbol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSymbol.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboSymbol.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.cboSymbol.Location = New System.Drawing.Point(478, 12)
        Me.cboSymbol.Name = "cboSymbol"
        Me.cboSymbol.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboSymbol.Size = New System.Drawing.Size(139, 21)
        Me.cboSymbol.TabIndex = 50
        '
        'cboEnterpriseType
        '
        Me.cboEnterpriseType.BackColor = System.Drawing.SystemColors.Window
        Me.cboEnterpriseType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboEnterpriseType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEnterpriseType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboEnterpriseType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.cboEnterpriseType.Location = New System.Drawing.Point(122, 14)
        Me.cboEnterpriseType.Name = "cboEnterpriseType"
        Me.cboEnterpriseType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboEnterpriseType.Size = New System.Drawing.Size(251, 21)
        Me.cboEnterpriseType.TabIndex = 49
        '
        'Label61
        '
        Me.Label61.AutoSize = True
        Me.Label61.BackColor = System.Drawing.Color.Transparent
        Me.Label61.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label61.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label61.ForeColor = System.Drawing.Color.Black
        Me.Label61.Location = New System.Drawing.Point(14, 48)
        Me.Label61.Name = "Label61"
        Me.Label61.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label61.Size = New System.Drawing.Size(104, 13)
        Me.Label61.TabIndex = 224
        Me.Label61.Text = "Udyog Aahaar No :"
        Me.Label61.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label60
        '
        Me.Label60.AutoSize = True
        Me.Label60.BackColor = System.Drawing.SystemColors.Control
        Me.Label60.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label60.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label60.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label60.Location = New System.Drawing.Point(13, 17)
        Me.Label60.Name = "Label60"
        Me.Label60.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label60.Size = New System.Drawing.Size(105, 13)
        Me.Label60.TabIndex = 223
        Me.Label60.Text = "Type of Enterprise :"
        Me.Label60.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label59
        '
        Me.Label59.AutoSize = True
        Me.Label59.BackColor = System.Drawing.SystemColors.Control
        Me.Label59.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label59.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label59.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label59.Location = New System.Drawing.Point(427, 15)
        Me.Label59.Name = "Label59"
        Me.Label59.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label59.Size = New System.Drawing.Size(52, 13)
        Me.Label59.TabIndex = 222
        Me.Label59.Text = "Symbol :"
        Me.Label59.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtServProvided
        '
        Me.txtServProvided.AcceptsReturn = True
        Me.txtServProvided.BackColor = System.Drawing.SystemColors.Window
        Me.txtServProvided.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtServProvided.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtServProvided.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtServProvided.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtServProvided.Location = New System.Drawing.Point(672, 73)
        Me.txtServProvided.MaxLength = 0
        Me.txtServProvided.Name = "txtServProvided"
        Me.txtServProvided.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtServProvided.Size = New System.Drawing.Size(390, 22)
        Me.txtServProvided.TabIndex = 48
        '
        'txtSrvRegnNo
        '
        Me.txtSrvRegnNo.AcceptsReturn = True
        Me.txtSrvRegnNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtSrvRegnNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSrvRegnNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSrvRegnNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSrvRegnNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSrvRegnNo.Location = New System.Drawing.Point(116, 73)
        Me.txtSrvRegnNo.MaxLength = 15
        Me.txtSrvRegnNo.Name = "txtSrvRegnNo"
        Me.txtSrvRegnNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSrvRegnNo.Size = New System.Drawing.Size(390, 22)
        Me.txtSrvRegnNo.TabIndex = 47
        '
        'txtTINNo
        '
        Me.txtTINNo.AcceptsReturn = True
        Me.txtTINNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtTINNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTINNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTINNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTINNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTINNo.Location = New System.Drawing.Point(672, 43)
        Me.txtTINNo.MaxLength = 15
        Me.txtTINNo.Name = "txtTINNo"
        Me.txtTINNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTINNo.Size = New System.Drawing.Size(390, 22)
        Me.txtTINNo.TabIndex = 46
        '
        'txtPan
        '
        Me.txtPan.AcceptsReturn = True
        Me.txtPan.BackColor = System.Drawing.SystemColors.Window
        Me.txtPan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPan.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPan.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPan.Location = New System.Drawing.Point(116, 43)
        Me.txtPan.MaxLength = 15
        Me.txtPan.Name = "txtPan"
        Me.txtPan.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPan.Size = New System.Drawing.Size(390, 22)
        Me.txtPan.TabIndex = 45
        '
        'txtCstNo
        '
        Me.txtCstNo.AcceptsReturn = True
        Me.txtCstNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtCstNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCstNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCstNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCstNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtCstNo.Location = New System.Drawing.Point(672, 13)
        Me.txtCstNo.MaxLength = 15
        Me.txtCstNo.Name = "txtCstNo"
        Me.txtCstNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCstNo.Size = New System.Drawing.Size(390, 22)
        Me.txtCstNo.TabIndex = 44
        '
        'txtLSTNo
        '
        Me.txtLSTNo.AcceptsReturn = True
        Me.txtLSTNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtLSTNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLSTNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLSTNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLSTNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtLSTNo.Location = New System.Drawing.Point(116, 13)
        Me.txtLSTNo.MaxLength = 15
        Me.txtLSTNo.Name = "txtLSTNo"
        Me.txtLSTNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLSTNo.Size = New System.Drawing.Size(390, 22)
        Me.txtLSTNo.TabIndex = 43
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.BackColor = System.Drawing.SystemColors.Control
        Me.Label53.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label53.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label53.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label53.Location = New System.Drawing.Point(572, 78)
        Me.Label53.Name = "Label53"
        Me.Label53.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label53.Size = New System.Drawing.Size(98, 13)
        Me.Label53.TabIndex = 183
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
        Me.Label32.Location = New System.Drawing.Point(25, 77)
        Me.Label32.Name = "Label32"
        Me.Label32.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label32.Size = New System.Drawing.Size(87, 13)
        Me.Label32.TabIndex = 139
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
        Me.Label17.Location = New System.Drawing.Point(621, 47)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(48, 13)
        Me.Label17.TabIndex = 128
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
        Me.Label14.Location = New System.Drawing.Point(65, 16)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(48, 13)
        Me.Label14.TabIndex = 110
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
        Me.Label15.Location = New System.Drawing.Point(618, 18)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(50, 13)
        Me.Label15.TabIndex = 111
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
        Me.Label16.Location = New System.Drawing.Point(80, 48)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(33, 13)
        Me.Label16.TabIndex = 112
        Me.Label16.Text = "PAN :"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_SSTInfo_TabPage3
        '
        Me._SSTInfo_TabPage3.Controls.Add(Me.Frame7)
        Me._SSTInfo_TabPage3.Location = New System.Drawing.Point(4, 22)
        Me._SSTInfo_TabPage3.Name = "_SSTInfo_TabPage3"
        Me._SSTInfo_TabPage3.Size = New System.Drawing.Size(1096, 235)
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
        Me.Frame7.Location = New System.Drawing.Point(0, 0)
        Me.Frame7.Name = "Frame7"
        Me.Frame7.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame7.Size = New System.Drawing.Size(737, 145)
        Me.Frame7.TabIndex = 121
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
        Me.Frame13.Size = New System.Drawing.Size(253, 40)
        Me.Frame13.TabIndex = 172
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
        Me._optRegd_0.Size = New System.Drawing.Size(67, 20)
        Me._optRegd_0.TabIndex = 57
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
        Me._optRegd_1.Size = New System.Drawing.Size(87, 20)
        Me._optRegd_1.TabIndex = 58
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
        Me.txtDivision.TabIndex = 52
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
        Me.txtRange.Size = New System.Drawing.Size(253, 22)
        Me.txtRange.TabIndex = 53
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
        Me.txtCommRate.Size = New System.Drawing.Size(253, 22)
        Me.txtCommRate.TabIndex = 56
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
        Me.txtECCNo.Size = New System.Drawing.Size(253, 22)
        Me.txtECCNo.TabIndex = 55
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
        Me.txtRegnNo.Size = New System.Drawing.Size(253, 22)
        Me.txtRegnNo.TabIndex = 54
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
        Me._Label25_2.Size = New System.Drawing.Size(99, 13)
        Me._Label25_2.TabIndex = 126
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
        Me.Label12.Size = New System.Drawing.Size(54, 13)
        Me.Label12.TabIndex = 125
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
        Me.Label11.Size = New System.Drawing.Size(45, 13)
        Me.Label11.TabIndex = 124
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
        Me.Label8.Size = New System.Drawing.Size(51, 13)
        Me.Label8.TabIndex = 123
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
        Me.Label7.Size = New System.Drawing.Size(57, 13)
        Me.Label7.TabIndex = 122
        Me.Label7.Text = "Reg. No. :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_SSTInfo_TabPage4
        '
        Me._SSTInfo_TabPage4.Controls.Add(Me.Frame12)
        Me._SSTInfo_TabPage4.Location = New System.Drawing.Point(4, 22)
        Me._SSTInfo_TabPage4.Name = "_SSTInfo_TabPage4"
        Me._SSTInfo_TabPage4.Size = New System.Drawing.Size(1096, 235)
        Me._SSTInfo_TabPage4.TabIndex = 4
        Me._SSTInfo_TabPage4.Text = "TDS Details"
        '
        'Frame12
        '
        Me.Frame12.BackColor = System.Drawing.SystemColors.Control
        Me.Frame12.Controls.Add(Me.chkTDSNotDeduct)
        Me.Frame12.Controls.Add(Me.chkRtnDeclaration)
        Me.Frame12.Controls.Add(Me.chkTDSDeduct)
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
        Me.Frame12.Location = New System.Drawing.Point(0, 0)
        Me.Frame12.Name = "Frame12"
        Me.Frame12.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame12.Size = New System.Drawing.Size(737, 198)
        Me.Frame12.TabIndex = 161
        Me.Frame12.TabStop = False
        '
        'chkTDSNotDeduct
        '
        Me.chkTDSNotDeduct.AutoSize = True
        Me.chkTDSNotDeduct.BackColor = System.Drawing.SystemColors.Control
        Me.chkTDSNotDeduct.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkTDSNotDeduct.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTDSNotDeduct.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkTDSNotDeduct.Location = New System.Drawing.Point(271, 113)
        Me.chkTDSNotDeduct.Name = "chkTDSNotDeduct"
        Me.chkTDSNotDeduct.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkTDSNotDeduct.Size = New System.Drawing.Size(171, 17)
        Me.chkTDSNotDeduct.TabIndex = 197
        Me.chkTDSNotDeduct.Text = "TDS Not Deduct Under 194 Q"
        Me.chkTDSNotDeduct.UseVisualStyleBackColor = False
        '
        'chkRtnDeclaration
        '
        Me.chkRtnDeclaration.AutoSize = True
        Me.chkRtnDeclaration.BackColor = System.Drawing.SystemColors.Control
        Me.chkRtnDeclaration.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkRtnDeclaration.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRtnDeclaration.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRtnDeclaration.Location = New System.Drawing.Point(478, 113)
        Me.chkRtnDeclaration.Name = "chkRtnDeclaration"
        Me.chkRtnDeclaration.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkRtnDeclaration.Size = New System.Drawing.Size(170, 17)
        Me.chkRtnDeclaration.TabIndex = 196
        Me.chkRtnDeclaration.Text = "Return Declaration provided"
        Me.chkRtnDeclaration.UseVisualStyleBackColor = False
        '
        'chkTDSDeduct
        '
        Me.chkTDSDeduct.AutoSize = True
        Me.chkTDSDeduct.BackColor = System.Drawing.SystemColors.Control
        Me.chkTDSDeduct.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkTDSDeduct.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTDSDeduct.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkTDSDeduct.Location = New System.Drawing.Point(116, 113)
        Me.chkTDSDeduct.Name = "chkTDSDeduct"
        Me.chkTDSDeduct.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkTDSDeduct.Size = New System.Drawing.Size(149, 17)
        Me.chkTDSDeduct.TabIndex = 195
        Me.chkTDSDeduct.Text = "TDS Deduct Under 194 Q"
        Me.chkTDSDeduct.UseVisualStyleBackColor = False
        '
        'chkLowerDeduction
        '
        Me.chkLowerDeduction.BackColor = System.Drawing.SystemColors.Control
        Me.chkLowerDeduction.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkLowerDeduction.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkLowerDeduction.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkLowerDeduction.Location = New System.Drawing.Point(478, 84)
        Me.chkLowerDeduction.Name = "chkLowerDeduction"
        Me.chkLowerDeduction.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkLowerDeduction.Size = New System.Drawing.Size(135, 17)
        Me.chkLowerDeduction.TabIndex = 65
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
        Me.txtLDCertiNo.Location = New System.Drawing.Point(116, 82)
        Me.txtLDCertiNo.MaxLength = 0
        Me.txtLDCertiNo.Name = "txtLDCertiNo"
        Me.txtLDCertiNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLDCertiNo.Size = New System.Drawing.Size(253, 22)
        Me.txtLDCertiNo.TabIndex = 64
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
        Me.txtExptionCNo.Size = New System.Drawing.Size(253, 22)
        Me.txtExptionCNo.TabIndex = 61
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
        Me.cboCType.Size = New System.Drawing.Size(253, 21)
        Me.cboCType.TabIndex = 63
        '
        'txtESIPer
        '
        Me.txtESIPer.AcceptsReturn = True
        Me.txtESIPer.BackColor = System.Drawing.SystemColors.Window
        Me.txtESIPer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtESIPer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtESIPer.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtESIPer.ForeColor = System.Drawing.Color.Blue
        Me.txtESIPer.Location = New System.Drawing.Point(640, 138)
        Me.txtESIPer.MaxLength = 0
        Me.txtESIPer.Name = "txtESIPer"
        Me.txtESIPer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtESIPer.Size = New System.Drawing.Size(49, 22)
        Me.txtESIPer.TabIndex = 68
        '
        'txtSTDSPer
        '
        Me.txtSTDSPer.AcceptsReturn = True
        Me.txtSTDSPer.BackColor = System.Drawing.SystemColors.Window
        Me.txtSTDSPer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSTDSPer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSTDSPer.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSTDSPer.ForeColor = System.Drawing.Color.Blue
        Me.txtSTDSPer.Location = New System.Drawing.Point(428, 138)
        Me.txtSTDSPer.MaxLength = 0
        Me.txtSTDSPer.Name = "txtSTDSPer"
        Me.txtSTDSPer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSTDSPer.Size = New System.Drawing.Size(49, 22)
        Me.txtSTDSPer.TabIndex = 67
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
        Me.CboTDSCategory.Size = New System.Drawing.Size(253, 21)
        Me.CboTDSCategory.TabIndex = 62
        '
        'txtTDSPer
        '
        Me.txtTDSPer.AcceptsReturn = True
        Me.txtTDSPer.BackColor = System.Drawing.SystemColors.Window
        Me.txtTDSPer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTDSPer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTDSPer.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTDSPer.ForeColor = System.Drawing.Color.Blue
        Me.txtTDSPer.Location = New System.Drawing.Point(116, 138)
        Me.txtTDSPer.MaxLength = 0
        Me.txtTDSPer.Name = "txtTDSPer"
        Me.txtTDSPer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTDSPer.Size = New System.Drawing.Size(49, 22)
        Me.txtTDSPer.TabIndex = 66
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
        Me.txtSection.Size = New System.Drawing.Size(225, 22)
        Me.txtSection.TabIndex = 59
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
        Me._lblLabels_0.Size = New System.Drawing.Size(118, 13)
        Me._lblLabels_0.TabIndex = 194
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
        Me._lblLabels_4.Size = New System.Drawing.Size(116, 13)
        Me._lblLabels_4.TabIndex = 168
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
        Me._lblLabels_5.Size = New System.Drawing.Size(50, 13)
        Me._lblLabels_5.TabIndex = 167
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
        Me.Label28.Location = New System.Drawing.Point(425, 55)
        Me.Label28.Name = "Label28"
        Me.Label28.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label28.Size = New System.Drawing.Size(37, 13)
        Me.Label28.TabIndex = 166
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
        Me.Label20.Location = New System.Drawing.Point(531, 140)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(40, 13)
        Me.Label20.TabIndex = 165
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
        Me.Label18.Location = New System.Drawing.Point(306, 140)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(51, 13)
        Me.Label18.TabIndex = 164
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
        Me.LblPayTerms.Size = New System.Drawing.Size(56, 13)
        Me.LblPayTerms.TabIndex = 163
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
        Me.lblDueDays.Location = New System.Drawing.Point(5, 140)
        Me.lblDueDays.Name = "lblDueDays"
        Me.lblDueDays.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDueDays.Size = New System.Drawing.Size(45, 13)
        Me.lblDueDays.TabIndex = 162
        Me.lblDueDays.Text = "TDS % :"
        Me.lblDueDays.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_SSTInfo_TabPage6
        '
        Me._SSTInfo_TabPage6.Controls.Add(Me.Frame11)
        Me._SSTInfo_TabPage6.Location = New System.Drawing.Point(4, 22)
        Me._SSTInfo_TabPage6.Name = "_SSTInfo_TabPage6"
        Me._SSTInfo_TabPage6.Size = New System.Drawing.Size(1096, 235)
        Me._SSTInfo_TabPage6.TabIndex = 6
        Me._SSTInfo_TabPage6.Text = "Export Details"
        '
        'Frame11
        '
        Me.Frame11.BackColor = System.Drawing.SystemColors.Control
        Me.Frame11.Controls.Add(Me.chkSEZ)
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
        Me.Frame11.Location = New System.Drawing.Point(0, 3)
        Me.Frame11.Name = "Frame11"
        Me.Frame11.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame11.Size = New System.Drawing.Size(735, 145)
        Me.Frame11.TabIndex = 140
        Me.Frame11.TabStop = False
        '
        'chkSEZ
        '
        Me.chkSEZ.BackColor = System.Drawing.SystemColors.Control
        Me.chkSEZ.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkSEZ.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSEZ.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkSEZ.Location = New System.Drawing.Point(488, 100)
        Me.chkSEZ.Name = "chkSEZ"
        Me.chkSEZ.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkSEZ.Size = New System.Drawing.Size(171, 17)
        Me.chkSEZ.TabIndex = 80
        Me.chkSEZ.Text = "SEZ (Yes / No)"
        Me.chkSEZ.UseVisualStyleBackColor = False
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
        Me.txtExportPaymetTerms.Size = New System.Drawing.Size(240, 22)
        Me.txtExportPaymetTerms.TabIndex = 79
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
        Me.txtFinalDest.Size = New System.Drawing.Size(240, 22)
        Me.txtFinalDest.TabIndex = 78
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
        Me.txtCarriage.Size = New System.Drawing.Size(240, 22)
        Me.txtCarriage.TabIndex = 75
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
        Me.txtDischargePort.Size = New System.Drawing.Size(240, 22)
        Me.txtDischargePort.TabIndex = 77
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
        Me.txtLoadingPort.Size = New System.Drawing.Size(240, 22)
        Me.txtLoadingPort.TabIndex = 76
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
        Me.txtBuyerName.Size = New System.Drawing.Size(240, 22)
        Me.txtBuyerName.TabIndex = 73
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
        Me.Label36.Size = New System.Drawing.Size(103, 13)
        Me.Label36.TabIndex = 146
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
        Me._Label25_4.Size = New System.Drawing.Size(98, 13)
        Me._Label25_4.TabIndex = 145
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
        Me.Label38.Size = New System.Drawing.Size(76, 13)
        Me.Label38.TabIndex = 144
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
        Me.Label37.Size = New System.Drawing.Size(92, 13)
        Me.Label37.TabIndex = 143
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
        Me._Label25_3.Size = New System.Drawing.Size(101, 13)
        Me._Label25_3.TabIndex = 142
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
        Me.Label35.Size = New System.Drawing.Size(75, 13)
        Me.Label35.TabIndex = 141
        Me.Label35.Text = "Buyer Name :"
        Me.Label35.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_SSTInfo_TabPage7
        '
        Me._SSTInfo_TabPage7.Controls.Add(Me.txtSecurityChqNo)
        Me._SSTInfo_TabPage7.Controls.Add(Me.Label34)
        Me._SSTInfo_TabPage7.Controls.Add(Me.txtSecurityAmount)
        Me._SSTInfo_TabPage7.Controls.Add(Me.lblSecurityAmount)
        Me._SSTInfo_TabPage7.Controls.Add(Me.chkSecurityChq)
        Me._SSTInfo_TabPage7.Controls.Add(Me.Frame14)
        Me._SSTInfo_TabPage7.Location = New System.Drawing.Point(4, 22)
        Me._SSTInfo_TabPage7.Name = "_SSTInfo_TabPage7"
        Me._SSTInfo_TabPage7.Size = New System.Drawing.Size(1096, 235)
        Me._SSTInfo_TabPage7.TabIndex = 7
        Me._SSTInfo_TabPage7.Text = "Bank Details"
        '
        'txtSecurityChqNo
        '
        Me.txtSecurityChqNo.AcceptsReturn = True
        Me.txtSecurityChqNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtSecurityChqNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSecurityChqNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSecurityChqNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSecurityChqNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSecurityChqNo.Location = New System.Drawing.Point(854, 73)
        Me.txtSecurityChqNo.MaxLength = 7
        Me.txtSecurityChqNo.Name = "txtSecurityChqNo"
        Me.txtSecurityChqNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSecurityChqNo.Size = New System.Drawing.Size(126, 22)
        Me.txtSecurityChqNo.TabIndex = 203
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.BackColor = System.Drawing.SystemColors.Control
        Me.Label34.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label34.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label34.Location = New System.Drawing.Point(737, 78)
        Me.Label34.Name = "Label34"
        Me.Label34.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label34.Size = New System.Drawing.Size(112, 13)
        Me.Label34.TabIndex = 204
        Me.Label34.Text = "Security Cheque No :"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtSecurityAmount
        '
        Me.txtSecurityAmount.AcceptsReturn = True
        Me.txtSecurityAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtSecurityAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSecurityAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSecurityAmount.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSecurityAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSecurityAmount.Location = New System.Drawing.Point(854, 46)
        Me.txtSecurityAmount.MaxLength = 7
        Me.txtSecurityAmount.Name = "txtSecurityAmount"
        Me.txtSecurityAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSecurityAmount.Size = New System.Drawing.Size(126, 22)
        Me.txtSecurityAmount.TabIndex = 201
        '
        'lblSecurityAmount
        '
        Me.lblSecurityAmount.AutoSize = True
        Me.lblSecurityAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblSecurityAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSecurityAmount.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSecurityAmount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSecurityAmount.Location = New System.Drawing.Point(756, 49)
        Me.lblSecurityAmount.Name = "lblSecurityAmount"
        Me.lblSecurityAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSecurityAmount.Size = New System.Drawing.Size(93, 13)
        Me.lblSecurityAmount.TabIndex = 202
        Me.lblSecurityAmount.Text = "Security Amount:"
        Me.lblSecurityAmount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'chkSecurityChq
        '
        Me.chkSecurityChq.AutoSize = True
        Me.chkSecurityChq.BackColor = System.Drawing.SystemColors.Control
        Me.chkSecurityChq.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkSecurityChq.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSecurityChq.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkSecurityChq.Location = New System.Drawing.Point(838, 23)
        Me.chkSecurityChq.Name = "chkSecurityChq"
        Me.chkSecurityChq.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkSecurityChq.Size = New System.Drawing.Size(155, 17)
        Me.chkSecurityChq.TabIndex = 197
        Me.chkSecurityChq.Text = "Security Cheques Deposit"
        Me.chkSecurityChq.UseVisualStyleBackColor = False
        '
        'Frame14
        '
        Me.Frame14.BackColor = System.Drawing.SystemColors.Control
        Me.Frame14.Controls.Add(Me.Frame17)
        Me.Frame14.Controls.Add(Me.txtBankAccountNo)
        Me.Frame14.Controls.Add(Me.txtBankBranch)
        Me.Frame14.Controls.Add(Me.txtIFSCCode)
        Me.Frame14.Controls.Add(Me.txtSwitCode)
        Me.Frame14.Controls.Add(Me.txtBankName)
        Me.Frame14.Controls.Add(Me.Label55)
        Me.Frame14.Controls.Add(Me._Label25_6)
        Me.Frame14.Controls.Add(Me.Label54)
        Me.Frame14.Controls.Add(Me.Label52)
        Me.Frame14.Controls.Add(Me._Label25_5)
        Me.Frame14.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame14.Location = New System.Drawing.Point(0, 3)
        Me.Frame14.Name = "Frame14"
        Me.Frame14.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame14.Size = New System.Drawing.Size(735, 145)
        Me.Frame14.TabIndex = 196
        Me.Frame14.TabStop = False
        '
        'Frame17
        '
        Me.Frame17.BackColor = System.Drawing.SystemColors.Control
        Me.Frame17.Controls.Add(Me.txtLenderBank)
        Me.Frame17.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame17.Location = New System.Drawing.Point(0, 102)
        Me.Frame17.Name = "Frame17"
        Me.Frame17.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame17.Size = New System.Drawing.Size(735, 43)
        Me.Frame17.TabIndex = 230
        Me.Frame17.TabStop = False
        Me.Frame17.Text = "Payment Through Bank"
        '
        'txtLenderBank
        '
        Me.txtLenderBank.AcceptsReturn = True
        Me.txtLenderBank.BackColor = System.Drawing.SystemColors.Window
        Me.txtLenderBank.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLenderBank.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLenderBank.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLenderBank.ForeColor = System.Drawing.Color.Blue
        Me.txtLenderBank.Location = New System.Drawing.Point(144, 14)
        Me.txtLenderBank.MaxLength = 0
        Me.txtLenderBank.Name = "txtLenderBank"
        Me.txtLenderBank.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLenderBank.Size = New System.Drawing.Size(357, 22)
        Me.txtLenderBank.TabIndex = 231
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
        Me.txtBankAccountNo.Size = New System.Drawing.Size(240, 22)
        Me.txtBankAccountNo.TabIndex = 81
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
        Me.txtBankBranch.Size = New System.Drawing.Size(240, 22)
        Me.txtBankBranch.TabIndex = 85
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
        Me.txtIFSCCode.Size = New System.Drawing.Size(240, 22)
        Me.txtIFSCCode.TabIndex = 82
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
        Me.txtSwitCode.Size = New System.Drawing.Size(240, 22)
        Me.txtSwitCode.TabIndex = 83
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
        Me.txtBankName.Size = New System.Drawing.Size(240, 22)
        Me.txtBankName.TabIndex = 84
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
        Me.Label55.Size = New System.Drawing.Size(97, 13)
        Me.Label55.TabIndex = 201
        Me.Label55.Text = "Account Number :"
        Me.Label55.TextAlign = System.Drawing.ContentAlignment.TopRight
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
        Me._Label25_6.Size = New System.Drawing.Size(65, 13)
        Me._Label25_6.TabIndex = 200
        Me._Label25_6.Text = "IFSC Code :"
        Me._Label25_6.TextAlign = System.Drawing.ContentAlignment.TopRight
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
        Me.Label54.Size = New System.Drawing.Size(47, 13)
        Me.Label54.TabIndex = 199
        Me.Label54.Text = "Branch :"
        Me.Label54.TextAlign = System.Drawing.ContentAlignment.TopRight
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
        Me.Label52.Size = New System.Drawing.Size(68, 13)
        Me.Label52.TabIndex = 198
        Me.Label52.Text = "Swift Code :"
        Me.Label52.TextAlign = System.Drawing.ContentAlignment.TopRight
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
        Me._Label25_5.Size = New System.Drawing.Size(71, 13)
        Me._Label25_5.TabIndex = 197
        Me._Label25_5.Text = "Bank Name :"
        Me._Label25_5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_SSTInfo_TabPage8
        '
        Me._SSTInfo_TabPage8.Controls.Add(Me.Frame15)
        Me._SSTInfo_TabPage8.Controls.Add(Me.FraGSTClass)
        Me._SSTInfo_TabPage8.Controls.Add(Me.Frame18)
        Me._SSTInfo_TabPage8.Controls.Add(Me.Frame19)
        Me._SSTInfo_TabPage8.Location = New System.Drawing.Point(4, 22)
        Me._SSTInfo_TabPage8.Name = "_SSTInfo_TabPage8"
        Me._SSTInfo_TabPage8.Size = New System.Drawing.Size(1096, 235)
        Me._SSTInfo_TabPage8.TabIndex = 8
        Me._SSTInfo_TabPage8.Text = "GST Details"
        '
        'Frame15
        '
        Me.Frame15.BackColor = System.Drawing.SystemColors.Control
        Me.Frame15.Controls.Add(Me.chkPlaceofSupply)
        Me.Frame15.Controls.Add(Me.txtGSTRegnNo)
        Me.Frame15.Controls.Add(Me.FraGSTStatus)
        Me.Frame15.Controls.Add(Me.Label58)
        Me.Frame15.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame15.Location = New System.Drawing.Point(0, 0)
        Me.Frame15.Name = "Frame15"
        Me.Frame15.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame15.Size = New System.Drawing.Size(375, 180)
        Me.Frame15.TabIndex = 202
        Me.Frame15.TabStop = False
        '
        'chkPlaceofSupply
        '
        Me.chkPlaceofSupply.AutoSize = True
        Me.chkPlaceofSupply.BackColor = System.Drawing.SystemColors.Control
        Me.chkPlaceofSupply.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPlaceofSupply.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPlaceofSupply.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPlaceofSupply.Location = New System.Drawing.Point(116, 147)
        Me.chkPlaceofSupply.Name = "chkPlaceofSupply"
        Me.chkPlaceofSupply.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPlaceofSupply.Size = New System.Drawing.Size(144, 17)
        Me.chkPlaceofSupply.TabIndex = 217
        Me.chkPlaceofSupply.Text = "Bill To : Place of Supply"
        Me.chkPlaceofSupply.UseVisualStyleBackColor = False
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
        Me.txtGSTRegnNo.Size = New System.Drawing.Size(253, 22)
        Me.txtGSTRegnNo.TabIndex = 206
        '
        'FraGSTStatus
        '
        Me.FraGSTStatus.BackColor = System.Drawing.SystemColors.Control
        Me.FraGSTStatus.Controls.Add(Me._optGSTRegd_4)
        Me.FraGSTStatus.Controls.Add(Me._optGSTRegd_3)
        Me.FraGSTStatus.Controls.Add(Me._optGSTRegd_2)
        Me.FraGSTStatus.Controls.Add(Me._optGSTRegd_1)
        Me.FraGSTStatus.Controls.Add(Me._optGSTRegd_0)
        Me.FraGSTStatus.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraGSTStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraGSTStatus.Location = New System.Drawing.Point(116, 36)
        Me.FraGSTStatus.Name = "FraGSTStatus"
        Me.FraGSTStatus.Padding = New System.Windows.Forms.Padding(0)
        Me.FraGSTStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraGSTStatus.Size = New System.Drawing.Size(255, 105)
        Me.FraGSTStatus.TabIndex = 203
        Me.FraGSTStatus.TabStop = False
        Me.FraGSTStatus.Text = "GST Status"
        '
        '_optGSTRegd_4
        '
        Me._optGSTRegd_4.BackColor = System.Drawing.SystemColors.Control
        Me._optGSTRegd_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._optGSTRegd_4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optGSTRegd_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optGSTRegd.SetIndex(Me._optGSTRegd_4, CType(4, Short))
        Me._optGSTRegd_4.Location = New System.Drawing.Point(78, 78)
        Me._optGSTRegd_4.Name = "_optGSTRegd_4"
        Me._optGSTRegd_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optGSTRegd_4.Size = New System.Drawing.Size(133, 17)
        Me._optGSTRegd_4.TabIndex = 211
        Me._optGSTRegd_4.TabStop = True
        Me._optGSTRegd_4.Text = "Composit Dealer"
        Me._optGSTRegd_4.UseVisualStyleBackColor = False
        '
        '_optGSTRegd_3
        '
        Me._optGSTRegd_3.BackColor = System.Drawing.SystemColors.Control
        Me._optGSTRegd_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._optGSTRegd_3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optGSTRegd_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optGSTRegd.SetIndex(Me._optGSTRegd_3, CType(3, Short))
        Me._optGSTRegd_3.Location = New System.Drawing.Point(78, 60)
        Me._optGSTRegd_3.Name = "_optGSTRegd_3"
        Me._optGSTRegd_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optGSTRegd_3.Size = New System.Drawing.Size(87, 17)
        Me._optGSTRegd_3.TabIndex = 210
        Me._optGSTRegd_3.TabStop = True
        Me._optGSTRegd_3.Text = "Foreign"
        Me._optGSTRegd_3.UseVisualStyleBackColor = False
        '
        '_optGSTRegd_2
        '
        Me._optGSTRegd_2.BackColor = System.Drawing.SystemColors.Control
        Me._optGSTRegd_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optGSTRegd_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optGSTRegd_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optGSTRegd.SetIndex(Me._optGSTRegd_2, CType(2, Short))
        Me._optGSTRegd_2.Location = New System.Drawing.Point(78, 44)
        Me._optGSTRegd_2.Name = "_optGSTRegd_2"
        Me._optGSTRegd_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optGSTRegd_2.Size = New System.Drawing.Size(87, 17)
        Me._optGSTRegd_2.TabIndex = 209
        Me._optGSTRegd_2.TabStop = True
        Me._optGSTRegd_2.Text = "Exempted"
        Me._optGSTRegd_2.UseVisualStyleBackColor = False
        '
        '_optGSTRegd_1
        '
        Me._optGSTRegd_1.BackColor = System.Drawing.SystemColors.Control
        Me._optGSTRegd_1.Checked = True
        Me._optGSTRegd_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optGSTRegd_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optGSTRegd_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optGSTRegd.SetIndex(Me._optGSTRegd_1, CType(1, Short))
        Me._optGSTRegd_1.Location = New System.Drawing.Point(78, 28)
        Me._optGSTRegd_1.Name = "_optGSTRegd_1"
        Me._optGSTRegd_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optGSTRegd_1.Size = New System.Drawing.Size(87, 17)
        Me._optGSTRegd_1.TabIndex = 205
        Me._optGSTRegd_1.TabStop = True
        Me._optGSTRegd_1.Text = "UnRegd."
        Me._optGSTRegd_1.UseVisualStyleBackColor = False
        '
        '_optGSTRegd_0
        '
        Me._optGSTRegd_0.BackColor = System.Drawing.SystemColors.Control
        Me._optGSTRegd_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optGSTRegd_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optGSTRegd_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optGSTRegd.SetIndex(Me._optGSTRegd_0, CType(0, Short))
        Me._optGSTRegd_0.Location = New System.Drawing.Point(78, 12)
        Me._optGSTRegd_0.Name = "_optGSTRegd_0"
        Me._optGSTRegd_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optGSTRegd_0.Size = New System.Drawing.Size(67, 17)
        Me._optGSTRegd_0.TabIndex = 204
        Me._optGSTRegd_0.TabStop = True
        Me._optGSTRegd_0.Text = "Regd."
        Me._optGSTRegd_0.UseVisualStyleBackColor = False
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
        Me.Label58.Size = New System.Drawing.Size(80, 13)
        Me.Label58.TabIndex = 207
        Me.Label58.Text = "GST Reg. No. :"
        Me.Label58.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'FraGSTClass
        '
        Me.FraGSTClass.BackColor = System.Drawing.SystemColors.Control
        Me.FraGSTClass.Controls.Add(Me._optGSTClassification_1)
        Me.FraGSTClass.Controls.Add(Me._optGSTClassification_0)
        Me.FraGSTClass.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraGSTClass.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraGSTClass.Location = New System.Drawing.Point(379, 0)
        Me.FraGSTClass.Name = "FraGSTClass"
        Me.FraGSTClass.Padding = New System.Windows.Forms.Padding(0)
        Me.FraGSTClass.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraGSTClass.Size = New System.Drawing.Size(357, 57)
        Me.FraGSTClass.TabIndex = 212
        Me.FraGSTClass.TabStop = False
        Me.FraGSTClass.Text = "Classification of taxability of Supply"
        '
        '_optGSTClassification_1
        '
        Me._optGSTClassification_1.BackColor = System.Drawing.SystemColors.Control
        Me._optGSTClassification_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optGSTClassification_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optGSTClassification_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optGSTClassification.SetIndex(Me._optGSTClassification_1, CType(1, Short))
        Me._optGSTClassification_1.Location = New System.Drawing.Point(78, 38)
        Me._optGSTClassification_1.Name = "_optGSTClassification_1"
        Me._optGSTClassification_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optGSTClassification_1.Size = New System.Drawing.Size(171, 17)
        Me._optGSTClassification_1.TabIndex = 214
        Me._optGSTClassification_1.TabStop = True
        Me._optGSTClassification_1.Text = "Reverse Charge"
        Me._optGSTClassification_1.UseVisualStyleBackColor = False
        '
        '_optGSTClassification_0
        '
        Me._optGSTClassification_0.BackColor = System.Drawing.SystemColors.Control
        Me._optGSTClassification_0.Checked = True
        Me._optGSTClassification_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optGSTClassification_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optGSTClassification_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optGSTClassification.SetIndex(Me._optGSTClassification_0, CType(0, Short))
        Me._optGSTClassification_0.Location = New System.Drawing.Point(78, 20)
        Me._optGSTClassification_0.Name = "_optGSTClassification_0"
        Me._optGSTClassification_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optGSTClassification_0.Size = New System.Drawing.Size(151, 17)
        Me._optGSTClassification_0.TabIndex = 213
        Me._optGSTClassification_0.TabStop = True
        Me._optGSTClassification_0.Text = "Forward Charge"
        Me._optGSTClassification_0.UseVisualStyleBackColor = False
        '
        'Frame18
        '
        Me.Frame18.BackColor = System.Drawing.SystemColors.Control
        Me.Frame18.Controls.Add(Me.chkTCSNotApplicable)
        Me.Frame18.Controls.Add(Me.chkTCSApplicable)
        Me.Frame18.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame18.Location = New System.Drawing.Point(380, 62)
        Me.Frame18.Name = "Frame18"
        Me.Frame18.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame18.Size = New System.Drawing.Size(357, 37)
        Me.Frame18.TabIndex = 233
        Me.Frame18.TabStop = False
        Me.Frame18.Text = "TCS Applicable"
        '
        'chkTCSNotApplicable
        '
        Me.chkTCSNotApplicable.BackColor = System.Drawing.SystemColors.Control
        Me.chkTCSNotApplicable.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkTCSNotApplicable.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTCSNotApplicable.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkTCSNotApplicable.Location = New System.Drawing.Point(215, 14)
        Me.chkTCSNotApplicable.Name = "chkTCSNotApplicable"
        Me.chkTCSNotApplicable.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkTCSNotApplicable.Size = New System.Drawing.Size(139, 16)
        Me.chkTCSNotApplicable.TabIndex = 216
        Me.chkTCSNotApplicable.Text = "TCS Not Applicable"
        Me.chkTCSNotApplicable.UseVisualStyleBackColor = False
        '
        'chkTCSApplicable
        '
        Me.chkTCSApplicable.BackColor = System.Drawing.SystemColors.Control
        Me.chkTCSApplicable.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkTCSApplicable.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTCSApplicable.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkTCSApplicable.Location = New System.Drawing.Point(81, 14)
        Me.chkTCSApplicable.Name = "chkTCSApplicable"
        Me.chkTCSApplicable.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkTCSApplicable.Size = New System.Drawing.Size(117, 16)
        Me.chkTCSApplicable.TabIndex = 215
        Me.chkTCSApplicable.Text = "TCS Applicable"
        Me.chkTCSApplicable.UseVisualStyleBackColor = False
        '
        'Frame19
        '
        Me.Frame19.BackColor = System.Drawing.SystemColors.Control
        Me.Frame19.Controls.Add(Me.txtCurrencyCode)
        Me.Frame19.Controls.Add(Me.txtCountryCode)
        Me.Frame19.Controls.Add(Me.Label65)
        Me.Frame19.Controls.Add(Me.Label64)
        Me.Frame19.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame19.Location = New System.Drawing.Point(380, 96)
        Me.Frame19.Name = "Frame19"
        Me.Frame19.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame19.Size = New System.Drawing.Size(357, 49)
        Me.Frame19.TabIndex = 234
        Me.Frame19.TabStop = False
        Me.Frame19.Text = "For e-Invoice"
        '
        'txtCurrencyCode
        '
        Me.txtCurrencyCode.AcceptsReturn = True
        Me.txtCurrencyCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtCurrencyCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCurrencyCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCurrencyCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrencyCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtCurrencyCode.Location = New System.Drawing.Point(103, 18)
        Me.txtCurrencyCode.MaxLength = 15
        Me.txtCurrencyCode.Name = "txtCurrencyCode"
        Me.txtCurrencyCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCurrencyCode.Size = New System.Drawing.Size(73, 22)
        Me.txtCurrencyCode.TabIndex = 216
        '
        'txtCountryCode
        '
        Me.txtCountryCode.AcceptsReturn = True
        Me.txtCountryCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtCountryCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCountryCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCountryCode.Enabled = False
        Me.txtCountryCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCountryCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtCountryCode.Location = New System.Drawing.Point(287, 18)
        Me.txtCountryCode.MaxLength = 15
        Me.txtCountryCode.Name = "txtCountryCode"
        Me.txtCountryCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCountryCode.Size = New System.Drawing.Size(65, 22)
        Me.txtCountryCode.TabIndex = 217
        '
        'Label65
        '
        Me.Label65.AutoSize = True
        Me.Label65.BackColor = System.Drawing.Color.Transparent
        Me.Label65.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label65.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label65.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label65.Location = New System.Drawing.Point(10, 20)
        Me.Label65.Name = "Label65"
        Me.Label65.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label65.Size = New System.Drawing.Size(87, 13)
        Me.Label65.TabIndex = 236
        Me.Label65.Text = "Currency Code :"
        '
        'Label64
        '
        Me.Label64.AutoSize = True
        Me.Label64.BackColor = System.Drawing.Color.Transparent
        Me.Label64.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label64.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label64.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label64.Location = New System.Drawing.Point(198, 20)
        Me.Label64.Name = "Label64"
        Me.Label64.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label64.Size = New System.Drawing.Size(83, 13)
        Me.Label64.TabIndex = 235
        Me.Label64.Text = "Country Code :"
        Me.Label64.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblRemarks
        '
        Me.lblRemarks.BackColor = System.Drawing.Color.Transparent
        Me.lblRemarks.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblRemarks.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRemarks.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRemarks.Location = New System.Drawing.Point(5, 503)
        Me.lblRemarks.Name = "lblRemarks"
        Me.lblRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblRemarks.Size = New System.Drawing.Size(135, 50)
        Me.lblRemarks.TabIndex = 149
        Me.lblRemarks.Text = "Remarks / Additional Address :"
        Me.lblRemarks.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(0, 0)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 101
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(1108, 572)
        Me.SprdView.TabIndex = 127
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
        Me.FraMovement.Location = New System.Drawing.Point(0, 568)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(1106, 51)
        Me.FraMovement.TabIndex = 113
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
        Me.lblMasterType.TabIndex = 114
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
        'frmAcm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1108, 621)
        Me.Controls.Add(Me.FraTrn)
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
        Me.Name = "frmAcm"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Account Master"
        Me.FraTrn.ResumeLayout(False)
        Me.FraTrn.PerformLayout()
        Me.FraStatus.ResumeLayout(False)
        Me.FraStatus.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.fraTop1.ResumeLayout(False)
        Me.fraTop1.PerformLayout()
        CType(Me.txtCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraAcctType.ResumeLayout(False)
        Me.FraAcctType.PerformLayout()
        Me.Frame8.ResumeLayout(False)
        Me.Frame8.PerformLayout()
        Me.FraView.ResumeLayout(False)
        Me.SSTInfo.ResumeLayout(False)
        Me._SSTInfo_TabPage0.ResumeLayout(False)
        Me._SSTInfo_TabPage0.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me.Frame5.ResumeLayout(False)
        Me.Frame5.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me._SSTInfo_TabPage9.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTInfo_TabPage1.ResumeLayout(False)
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me._SSTInfo_TabPage2.ResumeLayout(False)
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me.Frame16.ResumeLayout(False)
        Me.Frame16.PerformLayout()
        Me._SSTInfo_TabPage3.ResumeLayout(False)
        Me.Frame7.ResumeLayout(False)
        Me.Frame7.PerformLayout()
        Me.Frame13.ResumeLayout(False)
        Me._SSTInfo_TabPage4.ResumeLayout(False)
        Me.Frame12.ResumeLayout(False)
        Me.Frame12.PerformLayout()
        Me._SSTInfo_TabPage6.ResumeLayout(False)
        Me.Frame11.ResumeLayout(False)
        Me.Frame11.PerformLayout()
        Me._SSTInfo_TabPage7.ResumeLayout(False)
        Me._SSTInfo_TabPage7.PerformLayout()
        Me.Frame14.ResumeLayout(False)
        Me.Frame14.PerformLayout()
        Me.Frame17.ResumeLayout(False)
        Me.Frame17.PerformLayout()
        Me._SSTInfo_TabPage8.ResumeLayout(False)
        Me.Frame15.ResumeLayout(False)
        Me.Frame15.PerformLayout()
        Me.FraGSTStatus.ResumeLayout(False)
        Me.FraGSTClass.ResumeLayout(False)
        Me.Frame18.ResumeLayout(False)
        Me.Frame19.ResumeLayout(False)
        Me.Frame19.PerformLayout()
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

    Friend WithEvents _SSTInfo_TabPage9 As TabPage
    Public WithEvents txtCompanyName As TextBox
    Public WithEvents Label29 As Label
    Public WithEvents cmdSearchHeadCr As Button
    Public WithEvents cmdSearchHead As Button
    Public WithEvents cmdPaySearch As Button
    Public WithEvents chkRtnDeclaration As CheckBox
    Public WithEvents chkTDSDeduct As CheckBox
    Friend WithEvents txtCode As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents txtName As Infragistics.Win.UltraWinGrid.UltraCombo
    Public WithEvents txtCreditLimit As TextBox
    Public WithEvents Label30 As Label
    Public WithEvents chkTCSNotApplicable As CheckBox
    Public WithEvents chkPlaceofSupply As CheckBox
    Public WithEvents txtResponsiblePerson As TextBox
    Public WithEvents Label33 As Label
    Public WithEvents txtSecurityAmount As TextBox
    Public WithEvents lblSecurityAmount As Label
    Public WithEvents chkSecurityChq As CheckBox
    Public WithEvents txtSecurityChqNo As TextBox
    Public WithEvents Label34 As Label
    Public WithEvents chkTDSNotDeduct As CheckBox
    Public WithEvents chkGroupLimit As CheckBox
    Public WithEvents chkAccountHide As CheckBox
    Public WithEvents txtShortName As TextBox
    Public WithEvents Label66 As Label
    Public WithEvents txtReceiptDays As TextBox
    Public WithEvents Label19 As Label
#End Region
End Class