Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmEmployee
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
    Public WithEvents txtBloodGroup As System.Windows.Forms.TextBox
    Public WithEvents txtHODName As System.Windows.Forms.TextBox
    Public WithEvents cboCorporate As System.Windows.Forms.ComboBox
    Public WithEvents cboDivision As System.Windows.Forms.ComboBox
    Public WithEvents txtRefNo As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchRef As System.Windows.Forms.Button
    Public WithEvents cboPcRateType As System.Windows.Forms.ComboBox
    Public WithEvents cboShift As System.Windows.Forms.ComboBox
    Public WithEvents cboType As System.Windows.Forms.ComboBox
    Public WithEvents cboCatgeory As System.Windows.Forms.ComboBox
    Public WithEvents cboMStatus As System.Windows.Forms.ComboBox
    Public WithEvents cboSex As System.Windows.Forms.ComboBox
    Public WithEvents TxtName As System.Windows.Forms.TextBox
    Public WithEvents cmdSearch As System.Windows.Forms.Button
    Public WithEvents cmdSelectPhoto As System.Windows.Forms.Button
    Public WithEvents ImagePhoto As System.Windows.Forms.PictureBox
    Public WithEvents PbPhoto As System.Windows.Forms.Panel
    Public WithEvents txtEmpNo As System.Windows.Forms.TextBox
    Public WithEvents txtFName As System.Windows.Forms.TextBox
    Public WithEvents txtDOB As System.Windows.Forms.TextBox
    Public cdgPhotoOpen As System.Windows.Forms.OpenFileDialog
    Public WithEvents txtContractor As System.Windows.Forms.TextBox
    Public WithEvents Label73 As System.Windows.Forms.Label
    Public WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents Label67 As System.Windows.Forms.Label
    Public WithEvents Label66 As System.Windows.Forms.Label
    Public WithEvents Label65 As System.Windows.Forms.Label
    Public WithEvents Label63 As System.Windows.Forms.Label
    Public WithEvents Label45 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label23 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents lblPhotoFileName As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label54 As System.Windows.Forms.Label
    Public WithEvents fraTop As System.Windows.Forms.GroupBox
    Public WithEvents txtCTC As System.Windows.Forms.TextBox
    Public WithEvents txtWEF As System.Windows.Forms.TextBox
    Public WithEvents txtNetSalary As System.Windows.Forms.TextBox
    Public WithEvents txtDeduction As System.Windows.Forms.TextBox
    Public WithEvents txtGSalary As System.Windows.Forms.TextBox
    Public WithEvents txtBSalary As System.Windows.Forms.TextBox
    Public WithEvents Label30 As System.Windows.Forms.Label
    Public WithEvents Label31 As System.Windows.Forms.Label
    Public WithEvents Label32 As System.Windows.Forms.Label
    Public WithEvents Label33 As System.Windows.Forms.Label
    Public WithEvents Label34 As System.Windows.Forms.Label
    Public WithEvents Label35 As System.Windows.Forms.Label
    Public WithEvents txtIFSCCode As System.Windows.Forms.TextBox
    Public WithEvents cboEmpCatType As System.Windows.Forms.ComboBox
    Public WithEvents txtCostCenter As System.Windows.Forms.TextBox
    Public WithEvents cboDept As System.Windows.Forms.ComboBox
    Public WithEvents txtBankName As System.Windows.Forms.TextBox
    Public WithEvents txtExperience As System.Windows.Forms.TextBox
    Public WithEvents txtLastCompany As System.Windows.Forms.TextBox
    Public WithEvents cbodesignation As System.Windows.Forms.ComboBox
    Public WithEvents cboPaymentMode As System.Windows.Forms.ComboBox
    Public WithEvents txtBankAcno As System.Windows.Forms.TextBox
    Public WithEvents Label78 As System.Windows.Forms.Label
    Public WithEvents Label61 As System.Windows.Forms.Label
    Public WithEvents Label57 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label47 As System.Windows.Forms.Label
    Public WithEvents Label40 As System.Windows.Forms.Label
    Public WithEvents Label37 As System.Windows.Forms.Label
    Public WithEvents Label24 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents lblBankAcno As System.Windows.Forms.Label
    Public WithEvents fraModePayment As System.Windows.Forms.GroupBox
    Public WithEvents txtNextIncDueDate As System.Windows.Forms.TextBox
    Public WithEvents txtGroupDOJ As System.Windows.Forms.TextBox
    Public WithEvents txtQualification As System.Windows.Forms.TextBox
    Public WithEvents chkRGPAuthorization As System.Windows.Forms.CheckBox
    Public WithEvents chkStopSal As System.Windows.Forms.CheckBox
    Public WithEvents chkGroupInsurance As System.Windows.Forms.CheckBox
    Public WithEvents txtWorkingFrom As System.Windows.Forms.TextBox
    Public WithEvents txtWorkingTo As System.Windows.Forms.TextBox
    Public WithEvents txtReasonForLeaving As System.Windows.Forms.TextBox
    Public WithEvents txtDOP As System.Windows.Forms.TextBox
    Public WithEvents txtDOL As System.Windows.Forms.TextBox
    Public WithEvents txtDOJ As System.Windows.Forms.TextBox
    Public WithEvents CboJoinDesignation As System.Windows.Forms.ComboBox
    Public WithEvents cboWeeklyOff As System.Windows.Forms.ComboBox
    Public WithEvents Label68 As System.Windows.Forms.Label
    Public WithEvents Label62 As System.Windows.Forms.Label
    Public WithEvents Label29 As System.Windows.Forms.Label
    Public WithEvents Label21 As System.Windows.Forms.Label
    Public WithEvents Label38 As System.Windows.Forms.Label
    Public WithEvents Label39 As System.Windows.Forms.Label
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents Label20 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Frame8 As System.Windows.Forms.GroupBox
    Public WithEvents _SSTab1_TabPage0 As System.Windows.Forms.TabPage
    Public WithEvents txtMobileOff As System.Windows.Forms.TextBox
    Public WithEvents txtPState As System.Windows.Forms.TextBox
    Public WithEvents txtPAddress As System.Windows.Forms.TextBox
    Public WithEvents txtPCity As System.Windows.Forms.TextBox
    Public WithEvents txtPPinCode As System.Windows.Forms.TextBox
    Public WithEvents txtPPhone As System.Windows.Forms.TextBox
    Public WithEvents chkPMetroCity As System.Windows.Forms.CheckBox
    Public WithEvents Label72 As System.Windows.Forms.Label
    Public WithEvents Label71 As System.Windows.Forms.Label
    Public WithEvents Label70 As System.Windows.Forms.Label
    Public WithEvents Label69 As System.Windows.Forms.Label
    Public WithEvents Label36 As System.Windows.Forms.Label
    Public WithEvents Frame9 As System.Windows.Forms.GroupBox
    Public WithEvents txtEmail As System.Windows.Forms.TextBox
    Public WithEvents txtOffeMail As System.Windows.Forms.TextBox
    Public WithEvents chkMetroCity As System.Windows.Forms.CheckBox
    Public WithEvents txtPhone As System.Windows.Forms.TextBox
    Public WithEvents txtPinCode As System.Windows.Forms.TextBox
    Public WithEvents txtCity As System.Windows.Forms.TextBox
    Public WithEvents txtAddress As System.Windows.Forms.TextBox
    Public WithEvents txtState As System.Windows.Forms.TextBox
    Public WithEvents Label27 As System.Windows.Forms.Label
    Public WithEvents Label26 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label22 As System.Windows.Forms.Label
    Public WithEvents fraPermAdd As System.Windows.Forms.GroupBox
    Public WithEvents Label77 As System.Windows.Forms.Label
    Public WithEvents Label28 As System.Windows.Forms.Label
    Public WithEvents Label64 As System.Windows.Forms.Label
    Public WithEvents _SSTab1_TabPage1 As System.Windows.Forms.TabPage
    Public WithEvents txtDOBActual As System.Windows.Forms.TextBox
    Public WithEvents txtDOM As System.Windows.Forms.TextBox
    Public WithEvents Label80 As System.Windows.Forms.Label
    Public WithEvents Label79 As System.Windows.Forms.Label
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents cboOverTime As System.Windows.Forms.ComboBox
    Public WithEvents Frame7 As System.Windows.Forms.GroupBox
    Public WithEvents chkBonusApp As System.Windows.Forms.CheckBox
    Public WithEvents chkLEApp As System.Windows.Forms.CheckBox
    Public WithEvents txtOTRate As System.Windows.Forms.TextBox
    Public WithEvents txtLTAAmount As System.Windows.Forms.TextBox
    Public WithEvents txtBonusPer As System.Windows.Forms.TextBox
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents Label56 As System.Windows.Forms.Label
    Public WithEvents Label55 As System.Windows.Forms.Label
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents txtITAmount As System.Windows.Forms.TextBox
    Public WithEvents txtBankLoan As System.Windows.Forms.TextBox
    Public WithEvents txtLICAmount As System.Windows.Forms.TextBox
    Public WithEvents Label53 As System.Windows.Forms.Label
    Public WithEvents Label52 As System.Windows.Forms.Label
    Public WithEvents Label51 As System.Windows.Forms.Label
    Public WithEvents Frame5 As System.Windows.Forms.GroupBox
    Public WithEvents txtLoanAcNo As System.Windows.Forms.TextBox
    Public WithEvents txtImprestAcName As System.Windows.Forms.TextBox
    Public WithEvents txtLoanAcName As System.Windows.Forms.TextBox
    Public WithEvents Label60 As System.Windows.Forms.Label
    Public WithEvents Label50 As System.Windows.Forms.Label
    Public WithEvents Label49 As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents cboPFPension As System.Windows.Forms.ComboBox
    Public WithEvents txtUIDNo As System.Windows.Forms.TextBox
    Public WithEvents txtPFNo As System.Windows.Forms.TextBox
    Public WithEvents Label75 As System.Windows.Forms.Label
    Public WithEvents Label74 As System.Windows.Forms.Label
    Public WithEvents lblPFNo As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents cboESIApp As System.Windows.Forms.ComboBox
    Public WithEvents txtESINo As System.Windows.Forms.TextBox
    Public WithEvents txtDispensary As System.Windows.Forms.TextBox
    Public WithEvents Label42 As System.Windows.Forms.Label
    Public WithEvents lblEsiNo As System.Windows.Forms.Label
    Public WithEvents lblDispensary As System.Windows.Forms.Label
    Public WithEvents ESI As System.Windows.Forms.GroupBox
    Public WithEvents txtAdhaarNo As System.Windows.Forms.TextBox
    Public WithEvents txtLICID As System.Windows.Forms.TextBox
    Public WithEvents txtPanNo As System.Windows.Forms.TextBox
    Public WithEvents Label76 As System.Windows.Forms.Label
    Public WithEvents Label25 As System.Windows.Forms.Label
    Public WithEvents Label46 As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents _SSTab1_TabPage2 As System.Windows.Forms.TabPage
    Public WithEvents sprdSpouse As AxFPSpreadADO.AxfpSpread
    Public WithEvents _SSTab1_TabPage3 As System.Windows.Forms.TabPage
    Public WithEvents sprdAssets As AxFPSpreadADO.AxfpSpread
    Public WithEvents _SSTab1_TabPage4 As System.Windows.Forms.TabPage
    Public WithEvents chkEL As System.Windows.Forms.CheckBox
    Public WithEvents sprdLeaves As AxFPSpreadADO.AxfpSpread
    Public WithEvents lblLeavesYear As System.Windows.Forms.Label
    Public WithEvents _SSTab1_TabPage5 As System.Windows.Forms.TabPage
    Public WithEvents sprdDeduct As AxFPSpreadADO.AxfpSpread
    Public WithEvents sprdEarn As AxFPSpreadADO.AxfpSpread
    Public WithEvents grdDeductions As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents _SSTab1_TabPage6 As System.Windows.Forms.TabPage
    Public WithEvents Label58 As System.Windows.Forms.Label
    Public WithEvents sprdPerks As AxFPSpreadADO.AxfpSpread
    Public WithEvents _SSTab1_TabPage7 As System.Windows.Forms.TabPage
    Public WithEvents SSTab1 As System.Windows.Forms.TabControl
    Public WithEvents Label59 As System.Windows.Forms.Label
    Public WithEvents Label48 As System.Windows.Forms.Label
    Public WithEvents Label43 As System.Windows.Forms.Label
    Public WithEvents Label41 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents FraBottom As System.Windows.Forms.GroupBox
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents CmdDelete As System.Windows.Forms.Button
    Public WithEvents CmdModify As System.Windows.Forms.Button
    Public WithEvents CmdAdd As System.Windows.Forms.Button
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
    Public WithEvents cmdPreview As System.Windows.Forms.Button
    Public WithEvents lblNextIncDue As System.Windows.Forms.Label
    Public WithEvents lblEmpType As System.Windows.Forms.Label
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents Label44 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEmployee))
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
        Me.cmdSearchRef = New System.Windows.Forms.Button()
        Me.cmdSearch = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.fraTop = New System.Windows.Forms.GroupBox()
        Me.Label86 = New System.Windows.Forms.Label()
        Me.txtAddEmpCode = New System.Windows.Forms.TextBox()
        Me.txtBloodGroup = New System.Windows.Forms.TextBox()
        Me.txtHODName = New System.Windows.Forms.TextBox()
        Me.cboCorporate = New System.Windows.Forms.ComboBox()
        Me.cboDivision = New System.Windows.Forms.ComboBox()
        Me.txtRefNo = New System.Windows.Forms.TextBox()
        Me.cboPcRateType = New System.Windows.Forms.ComboBox()
        Me.cboShift = New System.Windows.Forms.ComboBox()
        Me.cboType = New System.Windows.Forms.ComboBox()
        Me.cboCatgeory = New System.Windows.Forms.ComboBox()
        Me.cboMStatus = New System.Windows.Forms.ComboBox()
        Me.cboSex = New System.Windows.Forms.ComboBox()
        Me.TxtName = New System.Windows.Forms.TextBox()
        Me.cmdSelectPhoto = New System.Windows.Forms.Button()
        Me.PbPhoto = New System.Windows.Forms.Panel()
        Me.ImagePhoto = New System.Windows.Forms.PictureBox()
        Me.txtEmpNo = New System.Windows.Forms.TextBox()
        Me.txtFName = New System.Windows.Forms.TextBox()
        Me.txtDOB = New System.Windows.Forms.TextBox()
        Me.txtContractor = New System.Windows.Forms.TextBox()
        Me.Label73 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label67 = New System.Windows.Forms.Label()
        Me.Label66 = New System.Windows.Forms.Label()
        Me.Label65 = New System.Windows.Forms.Label()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblPhotoFileName = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.cdgPhotoOpen = New System.Windows.Forms.OpenFileDialog()
        Me.FraBottom = New System.Windows.Forms.GroupBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.optContCeilingGross = New System.Windows.Forms.RadioButton()
        Me.optContGross = New System.Windows.Forms.RadioButton()
        Me.optContBasic = New System.Windows.Forms.RadioButton()
        Me.optContCeiling = New System.Windows.Forms.RadioButton()
        Me.txtForm1CTC = New System.Windows.Forms.TextBox()
        Me.txtForm1NetSalary = New System.Windows.Forms.TextBox()
        Me.Label83 = New System.Windows.Forms.Label()
        Me.Label84 = New System.Windows.Forms.Label()
        Me.txtForm1GSalary = New System.Windows.Forms.TextBox()
        Me.Label82 = New System.Windows.Forms.Label()
        Me.Label81 = New System.Windows.Forms.Label()
        Me.txtForm1BSalary = New System.Windows.Forms.TextBox()
        Me.txtCTC = New System.Windows.Forms.TextBox()
        Me.txtWEF = New System.Windows.Forms.TextBox()
        Me.txtNetSalary = New System.Windows.Forms.TextBox()
        Me.txtDeduction = New System.Windows.Forms.TextBox()
        Me.txtGSalary = New System.Windows.Forms.TextBox()
        Me.txtBSalary = New System.Windows.Forms.TextBox()
        Me.SSTab1 = New System.Windows.Forms.TabControl()
        Me._SSTab1_TabPage0 = New System.Windows.Forms.TabPage()
        Me.fraModePayment = New System.Windows.Forms.GroupBox()
        Me.cboTaxRegime = New System.Windows.Forms.ComboBox()
        Me.Label89 = New System.Windows.Forms.Label()
        Me.cboMachineName = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.Label87 = New System.Windows.Forms.Label()
        Me.txtIFSCCode = New System.Windows.Forms.TextBox()
        Me.cboEmpCatType = New System.Windows.Forms.ComboBox()
        Me.txtCostCenter = New System.Windows.Forms.TextBox()
        Me.cboDept = New System.Windows.Forms.ComboBox()
        Me.txtBankName = New System.Windows.Forms.TextBox()
        Me.txtExperience = New System.Windows.Forms.TextBox()
        Me.txtLastCompany = New System.Windows.Forms.TextBox()
        Me.cbodesignation = New System.Windows.Forms.ComboBox()
        Me.cboPaymentMode = New System.Windows.Forms.ComboBox()
        Me.txtBankAcno = New System.Windows.Forms.TextBox()
        Me.Label78 = New System.Windows.Forms.Label()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblBankAcno = New System.Windows.Forms.Label()
        Me.Frame8 = New System.Windows.Forms.GroupBox()
        Me.chkDeptHOD = New System.Windows.Forms.CheckBox()
        Me.chkHRHOD = New System.Windows.Forms.CheckBox()
        Me.txtBonusDOJ = New System.Windows.Forms.TextBox()
        Me.Label90 = New System.Windows.Forms.Label()
        Me.chkWFH = New System.Windows.Forms.CheckBox()
        Me.txtWorkingHours = New System.Windows.Forms.TextBox()
        Me.Label85 = New System.Windows.Forms.Label()
        Me.txtNextIncDueDate = New System.Windows.Forms.TextBox()
        Me.txtGroupDOJ = New System.Windows.Forms.TextBox()
        Me.txtQualification = New System.Windows.Forms.TextBox()
        Me.chkRGPAuthorization = New System.Windows.Forms.CheckBox()
        Me.chkStopSal = New System.Windows.Forms.CheckBox()
        Me.chkGroupInsurance = New System.Windows.Forms.CheckBox()
        Me.txtWorkingFrom = New System.Windows.Forms.TextBox()
        Me.txtWorkingTo = New System.Windows.Forms.TextBox()
        Me.txtReasonForLeaving = New System.Windows.Forms.TextBox()
        Me.txtDOP = New System.Windows.Forms.TextBox()
        Me.txtDOL = New System.Windows.Forms.TextBox()
        Me.txtDOJ = New System.Windows.Forms.TextBox()
        Me.CboJoinDesignation = New System.Windows.Forms.ComboBox()
        Me.cboWeeklyOff = New System.Windows.Forms.ComboBox()
        Me.Label68 = New System.Windows.Forms.Label()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me._SSTab1_TabPage1 = New System.Windows.Forms.TabPage()
        Me.txtMobileOff = New System.Windows.Forms.TextBox()
        Me.Frame9 = New System.Windows.Forms.GroupBox()
        Me.txtPState = New System.Windows.Forms.TextBox()
        Me.txtPAddress = New System.Windows.Forms.TextBox()
        Me.txtPCity = New System.Windows.Forms.TextBox()
        Me.txtPPinCode = New System.Windows.Forms.TextBox()
        Me.txtPPhone = New System.Windows.Forms.TextBox()
        Me.chkPMetroCity = New System.Windows.Forms.CheckBox()
        Me.Label72 = New System.Windows.Forms.Label()
        Me.Label71 = New System.Windows.Forms.Label()
        Me.Label70 = New System.Windows.Forms.Label()
        Me.Label69 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.txtOffeMail = New System.Windows.Forms.TextBox()
        Me.fraPermAdd = New System.Windows.Forms.GroupBox()
        Me.chkMetroCity = New System.Windows.Forms.CheckBox()
        Me.txtPhone = New System.Windows.Forms.TextBox()
        Me.txtPinCode = New System.Windows.Forms.TextBox()
        Me.txtCity = New System.Windows.Forms.TextBox()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.txtState = New System.Windows.Forms.TextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label77 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label64 = New System.Windows.Forms.Label()
        Me._SSTab1_TabPage2 = New System.Windows.Forms.TabPage()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label88 = New System.Windows.Forms.Label()
        Me.txtDAAmount = New System.Windows.Forms.TextBox()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.txtDOBActual = New System.Windows.Forms.TextBox()
        Me.txtDOM = New System.Windows.Forms.TextBox()
        Me.Label80 = New System.Windows.Forms.Label()
        Me.Label79 = New System.Windows.Forms.Label()
        Me.Frame7 = New System.Windows.Forms.GroupBox()
        Me.cboOverTime = New System.Windows.Forms.ComboBox()
        Me.chkBonusApp = New System.Windows.Forms.CheckBox()
        Me.chkLEApp = New System.Windows.Forms.CheckBox()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.txtOTRate = New System.Windows.Forms.TextBox()
        Me.txtLTAAmount = New System.Windows.Forms.TextBox()
        Me.txtBonusPer = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me.txtITAmount = New System.Windows.Forms.TextBox()
        Me.txtBankLoan = New System.Windows.Forms.TextBox()
        Me.txtLICAmount = New System.Windows.Forms.TextBox()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.txtLoanAcNo = New System.Windows.Forms.TextBox()
        Me.txtImprestAcName = New System.Windows.Forms.TextBox()
        Me.txtLoanAcName = New System.Windows.Forms.TextBox()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.cboPFPension = New System.Windows.Forms.ComboBox()
        Me.txtUIDNo = New System.Windows.Forms.TextBox()
        Me.txtPFNo = New System.Windows.Forms.TextBox()
        Me.Label75 = New System.Windows.Forms.Label()
        Me.Label74 = New System.Windows.Forms.Label()
        Me.lblPFNo = New System.Windows.Forms.Label()
        Me.ESI = New System.Windows.Forms.GroupBox()
        Me.cboESIApp = New System.Windows.Forms.ComboBox()
        Me.txtESINo = New System.Windows.Forms.TextBox()
        Me.txtDispensary = New System.Windows.Forms.TextBox()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.lblEsiNo = New System.Windows.Forms.Label()
        Me.lblDispensary = New System.Windows.Forms.Label()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.txtAdhaarNo = New System.Windows.Forms.TextBox()
        Me.txtLICID = New System.Windows.Forms.TextBox()
        Me.txtPanNo = New System.Windows.Forms.TextBox()
        Me.Label76 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me._SSTab1_TabPage3 = New System.Windows.Forms.TabPage()
        Me.sprdSpouse = New AxFPSpreadADO.AxfpSpread()
        Me._SSTab1_TabPage4 = New System.Windows.Forms.TabPage()
        Me.sprdAssets = New AxFPSpreadADO.AxfpSpread()
        Me._SSTab1_TabPage5 = New System.Windows.Forms.TabPage()
        Me.chkEL = New System.Windows.Forms.CheckBox()
        Me.sprdLeaves = New AxFPSpreadADO.AxfpSpread()
        Me.lblLeavesYear = New System.Windows.Forms.Label()
        Me._SSTab1_TabPage6 = New System.Windows.Forms.TabPage()
        Me.sprdDeduct = New AxFPSpreadADO.AxfpSpread()
        Me.sprdEarn = New AxFPSpreadADO.AxfpSpread()
        Me.grdDeductions = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me._SSTab1_TabPage7 = New System.Windows.Forms.TabPage()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.sprdPerks = New AxFPSpreadADO.AxfpSpread()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.cmdPreview = New System.Windows.Forms.Button()
        Me.lblNextIncDue = New System.Windows.Forms.Label()
        Me.lblEmpType = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.fraTop.SuspendLayout()
        Me.PbPhoto.SuspendLayout()
        CType(Me.ImagePhoto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraBottom.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SSTab1.SuspendLayout()
        Me._SSTab1_TabPage0.SuspendLayout()
        Me.fraModePayment.SuspendLayout()
        CType(Me.cboMachineName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame8.SuspendLayout()
        Me._SSTab1_TabPage1.SuspendLayout()
        Me.Frame9.SuspendLayout()
        Me.fraPermAdd.SuspendLayout()
        Me._SSTab1_TabPage2.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.Frame4.SuspendLayout()
        Me.Frame7.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me.Frame5.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.ESI.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me._SSTab1_TabPage3.SuspendLayout()
        CType(Me.sprdSpouse, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage4.SuspendLayout()
        CType(Me.sprdAssets, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage5.SuspendLayout()
        CType(Me.sprdLeaves, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage6.SuspendLayout()
        CType(Me.sprdDeduct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sprdEarn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage7.SuspendLayout()
        CType(Me.sprdPerks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSearchRef
        '
        Me.cmdSearchRef.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchRef.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchRef.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchRef.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchRef.Image = CType(resources.GetObject("cmdSearchRef.Image"), System.Drawing.Image)
        Me.cmdSearchRef.Location = New System.Drawing.Point(608, 41)
        Me.cmdSearchRef.Name = "cmdSearchRef"
        Me.cmdSearchRef.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchRef.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearchRef.TabIndex = 172
        Me.cmdSearchRef.TabStop = False
        Me.cmdSearchRef.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchRef, "Search")
        Me.cmdSearchRef.UseVisualStyleBackColor = False
        '
        'cmdSearch
        '
        Me.cmdSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearch.Image = CType(resources.GetObject("cmdSearch.Image"), System.Drawing.Image)
        Me.cmdSearch.Location = New System.Drawing.Point(170, 12)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearch.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearch.TabIndex = 2
        Me.cmdSearch.TabStop = False
        Me.cmdSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearch, "Search")
        Me.cmdSearch.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(475, 10)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 6
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print List")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(679, 10)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(67, 37)
        Me.CmdClose.TabIndex = 7
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'CmdView
        '
        Me.CmdView.BackColor = System.Drawing.SystemColors.Control
        Me.CmdView.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdView.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
        Me.CmdView.Location = New System.Drawing.Point(611, 10)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(67, 37)
        Me.CmdView.TabIndex = 3
        Me.CmdView.Text = "List &View"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdView, "View List")
        Me.CmdView.UseVisualStyleBackColor = False
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.Location = New System.Drawing.Point(271, 10)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(67, 37)
        Me.CmdSave.TabIndex = 1
        Me.CmdSave.Text = "&Save"
        Me.CmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSave, "Save Record")
        Me.CmdSave.UseVisualStyleBackColor = False
        '
        'CmdDelete
        '
        Me.CmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.CmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdDelete.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdDelete.Image = CType(resources.GetObject("CmdDelete.Image"), System.Drawing.Image)
        Me.CmdDelete.Location = New System.Drawing.Point(407, 10)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(67, 37)
        Me.CmdDelete.TabIndex = 85
        Me.CmdDelete.Text = "&Delete"
        Me.CmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdDelete, "Delete Record")
        Me.CmdDelete.UseVisualStyleBackColor = False
        '
        'CmdModify
        '
        Me.CmdModify.BackColor = System.Drawing.SystemColors.Control
        Me.CmdModify.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdModify.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdModify.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdModify.Image = CType(resources.GetObject("CmdModify.Image"), System.Drawing.Image)
        Me.CmdModify.Location = New System.Drawing.Point(205, 10)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(67, 37)
        Me.CmdModify.TabIndex = 82
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
        Me.CmdAdd.Location = New System.Drawing.Point(139, 10)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(67, 37)
        Me.CmdAdd.TabIndex = 5
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'fraTop
        '
        Me.fraTop.BackColor = System.Drawing.SystemColors.Control
        Me.fraTop.Controls.Add(Me.Label86)
        Me.fraTop.Controls.Add(Me.txtAddEmpCode)
        Me.fraTop.Controls.Add(Me.txtBloodGroup)
        Me.fraTop.Controls.Add(Me.txtHODName)
        Me.fraTop.Controls.Add(Me.cboCorporate)
        Me.fraTop.Controls.Add(Me.cboDivision)
        Me.fraTop.Controls.Add(Me.txtRefNo)
        Me.fraTop.Controls.Add(Me.cmdSearchRef)
        Me.fraTop.Controls.Add(Me.cboPcRateType)
        Me.fraTop.Controls.Add(Me.cboShift)
        Me.fraTop.Controls.Add(Me.cboType)
        Me.fraTop.Controls.Add(Me.cboCatgeory)
        Me.fraTop.Controls.Add(Me.cboMStatus)
        Me.fraTop.Controls.Add(Me.cboSex)
        Me.fraTop.Controls.Add(Me.TxtName)
        Me.fraTop.Controls.Add(Me.cmdSearch)
        Me.fraTop.Controls.Add(Me.cmdSelectPhoto)
        Me.fraTop.Controls.Add(Me.PbPhoto)
        Me.fraTop.Controls.Add(Me.txtEmpNo)
        Me.fraTop.Controls.Add(Me.txtFName)
        Me.fraTop.Controls.Add(Me.txtDOB)
        Me.fraTop.Controls.Add(Me.txtContractor)
        Me.fraTop.Controls.Add(Me.Label73)
        Me.fraTop.Controls.Add(Me.Label19)
        Me.fraTop.Controls.Add(Me.Label67)
        Me.fraTop.Controls.Add(Me.Label66)
        Me.fraTop.Controls.Add(Me.Label65)
        Me.fraTop.Controls.Add(Me.Label63)
        Me.fraTop.Controls.Add(Me.Label45)
        Me.fraTop.Controls.Add(Me.Label6)
        Me.fraTop.Controls.Add(Me.Label23)
        Me.fraTop.Controls.Add(Me.Label13)
        Me.fraTop.Controls.Add(Me.lblPhotoFileName)
        Me.fraTop.Controls.Add(Me.Label1)
        Me.fraTop.Controls.Add(Me.Label2)
        Me.fraTop.Controls.Add(Me.Label3)
        Me.fraTop.Controls.Add(Me.Label12)
        Me.fraTop.Controls.Add(Me.Label54)
        Me.fraTop.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTop.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraTop.Location = New System.Drawing.Point(0, -5)
        Me.fraTop.Name = "fraTop"
        Me.fraTop.Padding = New System.Windows.Forms.Padding(0)
        Me.fraTop.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraTop.Size = New System.Drawing.Size(910, 158)
        Me.fraTop.TabIndex = 0
        Me.fraTop.TabStop = False
        '
        'Label86
        '
        Me.Label86.AutoSize = True
        Me.Label86.BackColor = System.Drawing.Color.Transparent
        Me.Label86.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label86.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label86.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label86.Location = New System.Drawing.Point(450, 16)
        Me.Label86.Name = "Label86"
        Me.Label86.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label86.Size = New System.Drawing.Size(94, 14)
        Me.Label86.TabIndex = 202
        Me.Label86.Text = "Addition Emp No. :"
        Me.Label86.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtAddEmpCode
        '
        Me.txtAddEmpCode.AcceptsReturn = True
        Me.txtAddEmpCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtAddEmpCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddEmpCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAddEmpCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddEmpCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAddEmpCode.Location = New System.Drawing.Point(556, 12)
        Me.txtAddEmpCode.MaxLength = 0
        Me.txtAddEmpCode.Name = "txtAddEmpCode"
        Me.txtAddEmpCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAddEmpCode.Size = New System.Drawing.Size(90, 20)
        Me.txtAddEmpCode.TabIndex = 2
        '
        'txtBloodGroup
        '
        Me.txtBloodGroup.AcceptsReturn = True
        Me.txtBloodGroup.BackColor = System.Drawing.SystemColors.Window
        Me.txtBloodGroup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBloodGroup.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBloodGroup.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBloodGroup.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtBloodGroup.Location = New System.Drawing.Point(434, 70)
        Me.txtBloodGroup.MaxLength = 10
        Me.txtBloodGroup.Name = "txtBloodGroup"
        Me.txtBloodGroup.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBloodGroup.Size = New System.Drawing.Size(55, 20)
        Me.txtBloodGroup.TabIndex = 7
        '
        'txtHODName
        '
        Me.txtHODName.AcceptsReturn = True
        Me.txtHODName.BackColor = System.Drawing.SystemColors.Window
        Me.txtHODName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtHODName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtHODName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHODName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtHODName.Location = New System.Drawing.Point(102, 98)
        Me.txtHODName.MaxLength = 25
        Me.txtHODName.Name = "txtHODName"
        Me.txtHODName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtHODName.Size = New System.Drawing.Size(231, 20)
        Me.txtHODName.TabIndex = 9
        '
        'cboCorporate
        '
        Me.cboCorporate.BackColor = System.Drawing.SystemColors.Window
        Me.cboCorporate.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCorporate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCorporate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCorporate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCorporate.Location = New System.Drawing.Point(558, 127)
        Me.cboCorporate.Name = "cboCorporate"
        Me.cboCorporate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCorporate.Size = New System.Drawing.Size(79, 22)
        Me.cboCorporate.TabIndex = 16
        '
        'cboDivision
        '
        Me.cboDivision.BackColor = System.Drawing.SystemColors.Window
        Me.cboDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivision.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDivision.Location = New System.Drawing.Point(265, 12)
        Me.cboDivision.Name = "cboDivision"
        Me.cboDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivision.Size = New System.Drawing.Size(179, 22)
        Me.cboDivision.TabIndex = 1
        '
        'txtRefNo
        '
        Me.txtRefNo.AcceptsReturn = True
        Me.txtRefNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtRefNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRefNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRefNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRefNo.Location = New System.Drawing.Point(556, 41)
        Me.txtRefNo.MaxLength = 0
        Me.txtRefNo.Name = "txtRefNo"
        Me.txtRefNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRefNo.Size = New System.Drawing.Size(51, 20)
        Me.txtRefNo.TabIndex = 4
        '
        'cboPcRateType
        '
        Me.cboPcRateType.BackColor = System.Drawing.SystemColors.Window
        Me.cboPcRateType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboPcRateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPcRateType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPcRateType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cboPcRateType.Location = New System.Drawing.Point(558, 98)
        Me.cboPcRateType.Name = "cboPcRateType"
        Me.cboPcRateType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboPcRateType.Size = New System.Drawing.Size(79, 22)
        Me.cboPcRateType.TabIndex = 11
        '
        'cboShift
        '
        Me.cboShift.BackColor = System.Drawing.SystemColors.Window
        Me.cboShift.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboShift.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboShift.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboShift.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboShift.Location = New System.Drawing.Point(430, 127)
        Me.cboShift.Name = "cboShift"
        Me.cboShift.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboShift.Size = New System.Drawing.Size(59, 22)
        Me.cboShift.TabIndex = 15
        '
        'cboType
        '
        Me.cboType.BackColor = System.Drawing.SystemColors.Window
        Me.cboType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboType.Location = New System.Drawing.Point(322, 127)
        Me.cboType.Name = "cboType"
        Me.cboType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboType.Size = New System.Drawing.Size(67, 22)
        Me.cboType.TabIndex = 14
        '
        'cboCatgeory
        '
        Me.cboCatgeory.BackColor = System.Drawing.SystemColors.Window
        Me.cboCatgeory.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCatgeory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCatgeory.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCatgeory.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCatgeory.Location = New System.Drawing.Point(400, 98)
        Me.cboCatgeory.Name = "cboCatgeory"
        Me.cboCatgeory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCatgeory.Size = New System.Drawing.Size(89, 22)
        Me.cboCatgeory.TabIndex = 10
        '
        'cboMStatus
        '
        Me.cboMStatus.BackColor = System.Drawing.SystemColors.Window
        Me.cboMStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboMStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMStatus.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboMStatus.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboMStatus.Location = New System.Drawing.Point(102, 127)
        Me.cboMStatus.Name = "cboMStatus"
        Me.cboMStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboMStatus.Size = New System.Drawing.Size(67, 22)
        Me.cboMStatus.TabIndex = 12
        '
        'cboSex
        '
        Me.cboSex.BackColor = System.Drawing.SystemColors.Window
        Me.cboSex.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboSex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSex.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboSex.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboSex.Location = New System.Drawing.Point(222, 127)
        Me.cboSex.Name = "cboSex"
        Me.cboSex.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboSex.Size = New System.Drawing.Size(59, 22)
        Me.cboSex.TabIndex = 13
        '
        'TxtName
        '
        Me.TxtName.AcceptsReturn = True
        Me.TxtName.BackColor = System.Drawing.SystemColors.Window
        Me.TxtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtName.Location = New System.Drawing.Point(102, 41)
        Me.TxtName.MaxLength = 0
        Me.TxtName.Name = "TxtName"
        Me.TxtName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtName.Size = New System.Drawing.Size(343, 20)
        Me.TxtName.TabIndex = 3
        '
        'cmdSelectPhoto
        '
        Me.cmdSelectPhoto.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSelectPhoto.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSelectPhoto.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSelectPhoto.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSelectPhoto.Location = New System.Drawing.Point(732, 129)
        Me.cmdSelectPhoto.Name = "cmdSelectPhoto"
        Me.cmdSelectPhoto.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSelectPhoto.Size = New System.Drawing.Size(91, 22)
        Me.cmdSelectPhoto.TabIndex = 98
        Me.cmdSelectPhoto.Text = "Select Photo"
        Me.cmdSelectPhoto.UseVisualStyleBackColor = False
        '
        'PbPhoto
        '
        Me.PbPhoto.BackColor = System.Drawing.SystemColors.Control
        Me.PbPhoto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PbPhoto.Controls.Add(Me.ImagePhoto)
        Me.PbPhoto.Cursor = System.Windows.Forms.Cursors.Default
        Me.PbPhoto.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PbPhoto.ForeColor = System.Drawing.SystemColors.ControlText
        Me.PbPhoto.Location = New System.Drawing.Point(736, 10)
        Me.PbPhoto.Name = "PbPhoto"
        Me.PbPhoto.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.PbPhoto.Size = New System.Drawing.Size(170, 118)
        Me.PbPhoto.TabIndex = 94
        Me.PbPhoto.TabStop = True
        '
        'ImagePhoto
        '
        Me.ImagePhoto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.ImagePhoto.Cursor = System.Windows.Forms.Cursors.Default
        Me.ImagePhoto.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ImagePhoto.Location = New System.Drawing.Point(0, 0)
        Me.ImagePhoto.Name = "ImagePhoto"
        Me.ImagePhoto.Size = New System.Drawing.Size(166, 114)
        Me.ImagePhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.ImagePhoto.TabIndex = 0
        Me.ImagePhoto.TabStop = False
        '
        'txtEmpNo
        '
        Me.txtEmpNo.AcceptsReturn = True
        Me.txtEmpNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmpNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmpNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmpNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEmpNo.Location = New System.Drawing.Point(102, 12)
        Me.txtEmpNo.MaxLength = 0
        Me.txtEmpNo.Name = "txtEmpNo"
        Me.txtEmpNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmpNo.Size = New System.Drawing.Size(67, 20)
        Me.txtEmpNo.TabIndex = 0
        '
        'txtFName
        '
        Me.txtFName.AcceptsReturn = True
        Me.txtFName.BackColor = System.Drawing.SystemColors.Window
        Me.txtFName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtFName.Location = New System.Drawing.Point(102, 70)
        Me.txtFName.MaxLength = 25
        Me.txtFName.Name = "txtFName"
        Me.txtFName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFName.Size = New System.Drawing.Size(231, 20)
        Me.txtFName.TabIndex = 6
        '
        'txtDOB
        '
        Me.txtDOB.AcceptsReturn = True
        Me.txtDOB.BackColor = System.Drawing.SystemColors.Window
        Me.txtDOB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDOB.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDOB.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDOB.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtDOB.Location = New System.Drawing.Point(558, 70)
        Me.txtDOB.MaxLength = 10
        Me.txtDOB.Name = "txtDOB"
        Me.txtDOB.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDOB.Size = New System.Drawing.Size(79, 20)
        Me.txtDOB.TabIndex = 8
        '
        'txtContractor
        '
        Me.txtContractor.AcceptsReturn = True
        Me.txtContractor.BackColor = System.Drawing.SystemColors.Window
        Me.txtContractor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtContractor.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtContractor.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtContractor.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtContractor.Location = New System.Drawing.Point(638, 41)
        Me.txtContractor.MaxLength = 0
        Me.txtContractor.Name = "txtContractor"
        Me.txtContractor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtContractor.Size = New System.Drawing.Size(90, 20)
        Me.txtContractor.TabIndex = 5
        Me.txtContractor.Visible = False
        '
        'Label73
        '
        Me.Label73.AutoSize = True
        Me.Label73.BackColor = System.Drawing.Color.Transparent
        Me.Label73.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label73.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label73.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label73.Location = New System.Drawing.Point(352, 74)
        Me.Label73.Name = "Label73"
        Me.Label73.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label73.Size = New System.Drawing.Size(73, 14)
        Me.Label73.TabIndex = 200
        Me.Label73.Text = "Blood Group :"
        Me.Label73.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label19.Location = New System.Drawing.Point(23, 103)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(73, 14)
        Me.Label19.TabIndex = 178
        Me.Label19.Text = "Reporting To :"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label67
        '
        Me.Label67.AutoSize = True
        Me.Label67.BackColor = System.Drawing.SystemColors.Control
        Me.Label67.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label67.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label67.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label67.Location = New System.Drawing.Point(491, 131)
        Me.Label67.Name = "Label67"
        Me.Label67.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label67.Size = New System.Drawing.Size(61, 14)
        Me.Label67.TabIndex = 176
        Me.Label67.Text = "Corporate :"
        Me.Label67.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label66
        '
        Me.Label66.AutoSize = True
        Me.Label66.BackColor = System.Drawing.SystemColors.Control
        Me.Label66.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label66.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label66.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label66.Location = New System.Drawing.Point(206, 16)
        Me.Label66.Name = "Label66"
        Me.Label66.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label66.Size = New System.Drawing.Size(50, 14)
        Me.Label66.TabIndex = 175
        Me.Label66.Text = "Division :"
        Me.Label66.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label65
        '
        Me.Label65.AutoSize = True
        Me.Label65.BackColor = System.Drawing.Color.Transparent
        Me.Label65.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label65.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label65.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label65.Location = New System.Drawing.Point(450, 45)
        Me.Label65.Name = "Label65"
        Me.Label65.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label65.Size = New System.Drawing.Size(93, 14)
        Me.Label65.TabIndex = 174
        Me.Label65.Text = "Selection Ref No :"
        Me.Label65.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label63
        '
        Me.Label63.AutoSize = True
        Me.Label63.BackColor = System.Drawing.Color.Transparent
        Me.Label63.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label63.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label63.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label63.Location = New System.Drawing.Point(499, 103)
        Me.Label63.Name = "Label63"
        Me.Label63.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label63.Size = New System.Drawing.Size(53, 14)
        Me.Label63.TabIndex = 171
        Me.Label63.Text = "Pc. Rate :"
        Me.Label63.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.BackColor = System.Drawing.Color.Transparent
        Me.Label45.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label45.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label45.Location = New System.Drawing.Point(394, 131)
        Me.Label45.Name = "Label45"
        Me.Label45.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label45.Size = New System.Drawing.Size(35, 14)
        Me.Label45.TabIndex = 144
        Me.Label45.Text = "Shift :"
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label6.Location = New System.Drawing.Point(285, 129)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(36, 14)
        Me.Label6.TabIndex = 119
        Me.Label6.Text = "Type :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.Color.Transparent
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label23.Location = New System.Drawing.Point(190, 129)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(32, 14)
        Me.Label23.TabIndex = 96
        Me.Label23.Text = "Sex :"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label13.Location = New System.Drawing.Point(38, 129)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(58, 14)
        Me.Label13.TabIndex = 95
        Me.Label13.Text = "M. Status :"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblPhotoFileName
        '
        Me.lblPhotoFileName.AutoSize = True
        Me.lblPhotoFileName.BackColor = System.Drawing.SystemColors.Control
        Me.lblPhotoFileName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPhotoFileName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPhotoFileName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPhotoFileName.Location = New System.Drawing.Point(830, 134)
        Me.lblPhotoFileName.Name = "lblPhotoFileName"
        Me.lblPhotoFileName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPhotoFileName.Size = New System.Drawing.Size(77, 14)
        Me.lblPhotoFileName.TabIndex = 97
        Me.lblPhotoFileName.Text = "PhotoFileName"
        Me.lblPhotoFileName.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label1.Location = New System.Drawing.Point(56, 45)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(40, 14)
        Me.Label1.TabIndex = 90
        Me.Label1.Text = "Name :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label2.Location = New System.Drawing.Point(14, 74)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(82, 14)
        Me.Label2.TabIndex = 93
        Me.Label2.Text = "Father's Name :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label3.Location = New System.Drawing.Point(517, 74)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(35, 14)
        Me.Label3.TabIndex = 92
        Me.Label3.Text = "DOB :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label12.Location = New System.Drawing.Point(41, 16)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(55, 14)
        Me.Label12.TabIndex = 91
        Me.Label12.Text = "Card No. :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label54
        '
        Me.Label54.AutoSize = True
        Me.Label54.BackColor = System.Drawing.Color.Transparent
        Me.Label54.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label54.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label54.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label54.Location = New System.Drawing.Point(341, 103)
        Me.Label54.Name = "Label54"
        Me.Label54.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label54.Size = New System.Drawing.Size(57, 14)
        Me.Label54.TabIndex = 159
        Me.Label54.Text = "Category :"
        Me.Label54.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'FraBottom
        '
        Me.FraBottom.BackColor = System.Drawing.SystemColors.Control
        Me.FraBottom.Controls.Add(Me.GroupBox1)
        Me.FraBottom.Controls.Add(Me.txtForm1CTC)
        Me.FraBottom.Controls.Add(Me.txtForm1NetSalary)
        Me.FraBottom.Controls.Add(Me.Label83)
        Me.FraBottom.Controls.Add(Me.Label84)
        Me.FraBottom.Controls.Add(Me.txtForm1GSalary)
        Me.FraBottom.Controls.Add(Me.Label82)
        Me.FraBottom.Controls.Add(Me.Label81)
        Me.FraBottom.Controls.Add(Me.txtForm1BSalary)
        Me.FraBottom.Controls.Add(Me.txtCTC)
        Me.FraBottom.Controls.Add(Me.txtWEF)
        Me.FraBottom.Controls.Add(Me.txtNetSalary)
        Me.FraBottom.Controls.Add(Me.txtDeduction)
        Me.FraBottom.Controls.Add(Me.txtGSalary)
        Me.FraBottom.Controls.Add(Me.txtBSalary)
        Me.FraBottom.Controls.Add(Me.SSTab1)
        Me.FraBottom.Controls.Add(Me.Label59)
        Me.FraBottom.Controls.Add(Me.Label48)
        Me.FraBottom.Controls.Add(Me.Label43)
        Me.FraBottom.Controls.Add(Me.Label41)
        Me.FraBottom.Controls.Add(Me.Label15)
        Me.FraBottom.Controls.Add(Me.Label7)
        Me.FraBottom.Controls.Add(Me.Label30)
        Me.FraBottom.Controls.Add(Me.Label31)
        Me.FraBottom.Controls.Add(Me.Label32)
        Me.FraBottom.Controls.Add(Me.Label33)
        Me.FraBottom.Controls.Add(Me.Label34)
        Me.FraBottom.Controls.Add(Me.Label35)
        Me.FraBottom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraBottom.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraBottom.Location = New System.Drawing.Point(0, 149)
        Me.FraBottom.Name = "FraBottom"
        Me.FraBottom.Padding = New System.Windows.Forms.Padding(0)
        Me.FraBottom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraBottom.Size = New System.Drawing.Size(910, 421)
        Me.FraBottom.TabIndex = 100
        Me.FraBottom.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.Controls.Add(Me.optContCeilingGross)
        Me.GroupBox1.Controls.Add(Me.optContGross)
        Me.GroupBox1.Controls.Add(Me.optContBasic)
        Me.GroupBox1.Controls.Add(Me.optContCeiling)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox1.Location = New System.Drawing.Point(575, 1)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox1.Size = New System.Drawing.Size(334, 40)
        Me.GroupBox1.TabIndex = 187
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "PF Contribution on"
        '
        'optContCeilingGross
        '
        Me.optContCeilingGross.AutoSize = True
        Me.optContCeilingGross.BackColor = System.Drawing.SystemColors.Control
        Me.optContCeilingGross.Cursor = System.Windows.Forms.Cursors.Default
        Me.optContCeilingGross.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optContCeilingGross.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optContCeilingGross.Location = New System.Drawing.Point(238, 15)
        Me.optContCeilingGross.Name = "optContCeilingGross"
        Me.optContCeilingGross.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optContCeilingGross.Size = New System.Drawing.Size(89, 18)
        Me.optContCeilingGross.TabIndex = 3
        Me.optContCeilingGross.Text = "Ceiling Gross"
        Me.optContCeilingGross.UseVisualStyleBackColor = False
        '
        'optContGross
        '
        Me.optContGross.AutoSize = True
        Me.optContGross.BackColor = System.Drawing.SystemColors.Control
        Me.optContGross.Cursor = System.Windows.Forms.Cursors.Default
        Me.optContGross.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optContGross.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optContGross.Location = New System.Drawing.Point(91, 15)
        Me.optContGross.Name = "optContGross"
        Me.optContGross.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optContGross.Size = New System.Drawing.Size(55, 18)
        Me.optContGross.TabIndex = 2
        Me.optContGross.Text = "Gross"
        Me.optContGross.UseVisualStyleBackColor = False
        '
        'optContBasic
        '
        Me.optContBasic.AutoSize = True
        Me.optContBasic.BackColor = System.Drawing.SystemColors.Control
        Me.optContBasic.Checked = True
        Me.optContBasic.Cursor = System.Windows.Forms.Cursors.Default
        Me.optContBasic.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optContBasic.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optContBasic.Location = New System.Drawing.Point(3, 15)
        Me.optContBasic.Name = "optContBasic"
        Me.optContBasic.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optContBasic.Size = New System.Drawing.Size(86, 18)
        Me.optContBasic.TabIndex = 0
        Me.optContBasic.TabStop = True
        Me.optContBasic.Text = "Basic Salary"
        Me.optContBasic.UseVisualStyleBackColor = False
        '
        'optContCeiling
        '
        Me.optContCeiling.AutoSize = True
        Me.optContCeiling.BackColor = System.Drawing.SystemColors.Control
        Me.optContCeiling.Cursor = System.Windows.Forms.Cursors.Default
        Me.optContCeiling.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optContCeiling.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optContCeiling.Location = New System.Drawing.Point(150, 15)
        Me.optContCeiling.Name = "optContCeiling"
        Me.optContCeiling.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optContCeiling.Size = New System.Drawing.Size(86, 18)
        Me.optContCeiling.TabIndex = 1
        Me.optContCeiling.Text = "Ceiling Basic"
        Me.optContCeiling.UseVisualStyleBackColor = False
        '
        'txtForm1CTC
        '
        Me.txtForm1CTC.AcceptsReturn = True
        Me.txtForm1CTC.BackColor = System.Drawing.SystemColors.Window
        Me.txtForm1CTC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtForm1CTC.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtForm1CTC.Enabled = False
        Me.txtForm1CTC.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtForm1CTC.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtForm1CTC.Location = New System.Drawing.Point(673, 396)
        Me.txtForm1CTC.MaxLength = 0
        Me.txtForm1CTC.Name = "txtForm1CTC"
        Me.txtForm1CTC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtForm1CTC.Size = New System.Drawing.Size(80, 20)
        Me.txtForm1CTC.TabIndex = 6
        Me.txtForm1CTC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtForm1NetSalary
        '
        Me.txtForm1NetSalary.AcceptsReturn = True
        Me.txtForm1NetSalary.BackColor = System.Drawing.SystemColors.Window
        Me.txtForm1NetSalary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtForm1NetSalary.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtForm1NetSalary.Enabled = False
        Me.txtForm1NetSalary.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtForm1NetSalary.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtForm1NetSalary.Location = New System.Drawing.Point(495, 396)
        Me.txtForm1NetSalary.MaxLength = 0
        Me.txtForm1NetSalary.Name = "txtForm1NetSalary"
        Me.txtForm1NetSalary.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtForm1NetSalary.Size = New System.Drawing.Size(80, 20)
        Me.txtForm1NetSalary.TabIndex = 4
        Me.txtForm1NetSalary.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label83
        '
        Me.Label83.AutoSize = True
        Me.Label83.BackColor = System.Drawing.SystemColors.Control
        Me.Label83.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label83.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label83.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label83.Location = New System.Drawing.Point(609, 399)
        Me.Label83.Name = "Label83"
        Me.Label83.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label83.Size = New System.Drawing.Size(62, 14)
        Me.Label83.TabIndex = 175
        Me.Label83.Text = "Pay C.T.C. :"
        '
        'Label84
        '
        Me.Label84.AutoSize = True
        Me.Label84.BackColor = System.Drawing.SystemColors.Control
        Me.Label84.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label84.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label84.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label84.Location = New System.Drawing.Point(405, 399)
        Me.Label84.Name = "Label84"
        Me.Label84.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label84.Size = New System.Drawing.Size(84, 14)
        Me.Label84.TabIndex = 173
        Me.Label84.Text = "Pay Net Salary :"
        '
        'txtForm1GSalary
        '
        Me.txtForm1GSalary.AcceptsReturn = True
        Me.txtForm1GSalary.BackColor = System.Drawing.SystemColors.Window
        Me.txtForm1GSalary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtForm1GSalary.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtForm1GSalary.Enabled = False
        Me.txtForm1GSalary.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtForm1GSalary.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtForm1GSalary.Location = New System.Drawing.Point(127, 396)
        Me.txtForm1GSalary.MaxLength = 0
        Me.txtForm1GSalary.Name = "txtForm1GSalary"
        Me.txtForm1GSalary.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtForm1GSalary.Size = New System.Drawing.Size(109, 20)
        Me.txtForm1GSalary.TabIndex = 1
        Me.txtForm1GSalary.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label82
        '
        Me.Label82.AutoSize = True
        Me.Label82.BackColor = System.Drawing.SystemColors.Control
        Me.Label82.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label82.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label82.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label82.Location = New System.Drawing.Point(24, 400)
        Me.Label82.Name = "Label82"
        Me.Label82.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label82.Size = New System.Drawing.Size(98, 14)
        Me.Label82.TabIndex = 171
        Me.Label82.Text = "Pay Gross Salary :"
        '
        'Label81
        '
        Me.Label81.AutoSize = True
        Me.Label81.BackColor = System.Drawing.SystemColors.Control
        Me.Label81.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label81.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label81.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label81.Location = New System.Drawing.Point(226, 17)
        Me.Label81.Name = "Label81"
        Me.Label81.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label81.Size = New System.Drawing.Size(95, 14)
        Me.Label81.TabIndex = 169
        Me.Label81.Text = "Pay Basic Salary :"
        '
        'txtForm1BSalary
        '
        Me.txtForm1BSalary.AcceptsReturn = True
        Me.txtForm1BSalary.BackColor = System.Drawing.SystemColors.Window
        Me.txtForm1BSalary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtForm1BSalary.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtForm1BSalary.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtForm1BSalary.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtForm1BSalary.Location = New System.Drawing.Point(339, 13)
        Me.txtForm1BSalary.MaxLength = 0
        Me.txtForm1BSalary.Name = "txtForm1BSalary"
        Me.txtForm1BSalary.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtForm1BSalary.Size = New System.Drawing.Size(94, 20)
        Me.txtForm1BSalary.TabIndex = 1
        Me.txtForm1BSalary.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCTC
        '
        Me.txtCTC.AcceptsReturn = True
        Me.txtCTC.BackColor = System.Drawing.SystemColors.Window
        Me.txtCTC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCTC.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCTC.Enabled = False
        Me.txtCTC.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCTC.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCTC.Location = New System.Drawing.Point(673, 369)
        Me.txtCTC.MaxLength = 0
        Me.txtCTC.Name = "txtCTC"
        Me.txtCTC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCTC.Size = New System.Drawing.Size(80, 20)
        Me.txtCTC.TabIndex = 5
        Me.txtCTC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtWEF
        '
        Me.txtWEF.AcceptsReturn = True
        Me.txtWEF.BackColor = System.Drawing.SystemColors.Window
        Me.txtWEF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWEF.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWEF.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWEF.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtWEF.Location = New System.Drawing.Point(477, 14)
        Me.txtWEF.MaxLength = 10
        Me.txtWEF.Name = "txtWEF"
        Me.txtWEF.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWEF.Size = New System.Drawing.Size(94, 20)
        Me.txtWEF.TabIndex = 2
        '
        'txtNetSalary
        '
        Me.txtNetSalary.AcceptsReturn = True
        Me.txtNetSalary.BackColor = System.Drawing.SystemColors.Window
        Me.txtNetSalary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNetSalary.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNetSalary.Enabled = False
        Me.txtNetSalary.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNetSalary.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNetSalary.Location = New System.Drawing.Point(495, 369)
        Me.txtNetSalary.MaxLength = 0
        Me.txtNetSalary.Name = "txtNetSalary"
        Me.txtNetSalary.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNetSalary.Size = New System.Drawing.Size(80, 20)
        Me.txtNetSalary.TabIndex = 3
        Me.txtNetSalary.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDeduction
        '
        Me.txtDeduction.AcceptsReturn = True
        Me.txtDeduction.BackColor = System.Drawing.SystemColors.Window
        Me.txtDeduction.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDeduction.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDeduction.Enabled = False
        Me.txtDeduction.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeduction.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDeduction.Location = New System.Drawing.Point(307, 369)
        Me.txtDeduction.MaxLength = 0
        Me.txtDeduction.Name = "txtDeduction"
        Me.txtDeduction.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDeduction.Size = New System.Drawing.Size(80, 20)
        Me.txtDeduction.TabIndex = 2
        Me.txtDeduction.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtGSalary
        '
        Me.txtGSalary.AcceptsReturn = True
        Me.txtGSalary.BackColor = System.Drawing.SystemColors.Window
        Me.txtGSalary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGSalary.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGSalary.Enabled = False
        Me.txtGSalary.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGSalary.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtGSalary.Location = New System.Drawing.Point(127, 369)
        Me.txtGSalary.MaxLength = 0
        Me.txtGSalary.Name = "txtGSalary"
        Me.txtGSalary.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGSalary.Size = New System.Drawing.Size(109, 20)
        Me.txtGSalary.TabIndex = 0
        Me.txtGSalary.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBSalary
        '
        Me.txtBSalary.AcceptsReturn = True
        Me.txtBSalary.BackColor = System.Drawing.SystemColors.Window
        Me.txtBSalary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBSalary.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBSalary.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBSalary.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBSalary.Location = New System.Drawing.Point(125, 14)
        Me.txtBSalary.MaxLength = 0
        Me.txtBSalary.Name = "txtBSalary"
        Me.txtBSalary.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBSalary.Size = New System.Drawing.Size(94, 20)
        Me.txtBSalary.TabIndex = 0
        Me.txtBSalary.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
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
        Me.SSTab1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SSTab1.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.SSTab1.ItemSize = New System.Drawing.Size(42, 21)
        Me.SSTab1.Location = New System.Drawing.Point(2, 44)
        Me.SSTab1.Name = "SSTab1"
        Me.SSTab1.SelectedIndex = 0
        Me.SSTab1.Size = New System.Drawing.Size(904, 320)
        Me.SSTab1.TabIndex = 10
        '
        '_SSTab1_TabPage0
        '
        Me._SSTab1_TabPage0.Controls.Add(Me.fraModePayment)
        Me._SSTab1_TabPage0.Controls.Add(Me.Frame8)
        Me._SSTab1_TabPage0.Location = New System.Drawing.Point(4, 25)
        Me._SSTab1_TabPage0.Name = "_SSTab1_TabPage0"
        Me._SSTab1_TabPage0.Size = New System.Drawing.Size(896, 291)
        Me._SSTab1_TabPage0.TabIndex = 0
        Me._SSTab1_TabPage0.Text = "Company"
        '
        'fraModePayment
        '
        Me.fraModePayment.BackColor = System.Drawing.SystemColors.Control
        Me.fraModePayment.Controls.Add(Me.cboTaxRegime)
        Me.fraModePayment.Controls.Add(Me.Label89)
        Me.fraModePayment.Controls.Add(Me.cboMachineName)
        Me.fraModePayment.Controls.Add(Me.Label87)
        Me.fraModePayment.Controls.Add(Me.txtIFSCCode)
        Me.fraModePayment.Controls.Add(Me.cboEmpCatType)
        Me.fraModePayment.Controls.Add(Me.txtCostCenter)
        Me.fraModePayment.Controls.Add(Me.cboDept)
        Me.fraModePayment.Controls.Add(Me.txtBankName)
        Me.fraModePayment.Controls.Add(Me.txtExperience)
        Me.fraModePayment.Controls.Add(Me.txtLastCompany)
        Me.fraModePayment.Controls.Add(Me.cbodesignation)
        Me.fraModePayment.Controls.Add(Me.cboPaymentMode)
        Me.fraModePayment.Controls.Add(Me.txtBankAcno)
        Me.fraModePayment.Controls.Add(Me.Label78)
        Me.fraModePayment.Controls.Add(Me.Label61)
        Me.fraModePayment.Controls.Add(Me.Label57)
        Me.fraModePayment.Controls.Add(Me.Label4)
        Me.fraModePayment.Controls.Add(Me.Label47)
        Me.fraModePayment.Controls.Add(Me.Label40)
        Me.fraModePayment.Controls.Add(Me.Label37)
        Me.fraModePayment.Controls.Add(Me.Label24)
        Me.fraModePayment.Controls.Add(Me.Label5)
        Me.fraModePayment.Controls.Add(Me.lblBankAcno)
        Me.fraModePayment.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraModePayment.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.fraModePayment.Location = New System.Drawing.Point(4, 0)
        Me.fraModePayment.Name = "fraModePayment"
        Me.fraModePayment.Padding = New System.Windows.Forms.Padding(0)
        Me.fraModePayment.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraModePayment.Size = New System.Drawing.Size(446, 290)
        Me.fraModePayment.TabIndex = 108
        Me.fraModePayment.TabStop = False
        '
        'cboTaxRegime
        '
        Me.cboTaxRegime.BackColor = System.Drawing.SystemColors.Window
        Me.cboTaxRegime.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboTaxRegime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTaxRegime.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboTaxRegime.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboTaxRegime.Location = New System.Drawing.Point(144, 265)
        Me.cboTaxRegime.Name = "cboTaxRegime"
        Me.cboTaxRegime.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboTaxRegime.Size = New System.Drawing.Size(93, 22)
        Me.cboTaxRegime.TabIndex = 214
        '
        'Label89
        '
        Me.Label89.BackColor = System.Drawing.Color.Transparent
        Me.Label89.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label89.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label89.ForeColor = System.Drawing.Color.Black
        Me.Label89.Location = New System.Drawing.Point(5, 268)
        Me.Label89.Name = "Label89"
        Me.Label89.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label89.Size = New System.Drawing.Size(136, 16)
        Me.Label89.TabIndex = 213
        Me.Label89.Text = "Tax Regime :"
        Me.Label89.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cboMachineName
        '
        Me.cboMachineName.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest
        Me.cboMachineName.AutoSize = False
        Me.cboMachineName.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains
        Appearance1.BackColor = System.Drawing.SystemColors.Window
        Appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption
        Me.cboMachineName.DisplayLayout.Appearance = Appearance1
        Me.cboMachineName.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.cboMachineName.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
        Appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder
        Appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance2.BorderColor = System.Drawing.SystemColors.Window
        Me.cboMachineName.DisplayLayout.GroupByBox.Appearance = Appearance2
        Appearance3.ForeColor = System.Drawing.SystemColors.GrayText
        Me.cboMachineName.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance3
        Me.cboMachineName.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance4.BackColor2 = System.Drawing.SystemColors.Control
        Appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance4.ForeColor = System.Drawing.SystemColors.GrayText
        Me.cboMachineName.DisplayLayout.GroupByBox.PromptAppearance = Appearance4
        Me.cboMachineName.DisplayLayout.MaxColScrollRegions = 1
        Me.cboMachineName.DisplayLayout.MaxRowScrollRegions = 1
        Appearance5.BackColor = System.Drawing.SystemColors.Window
        Appearance5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cboMachineName.DisplayLayout.Override.ActiveCellAppearance = Appearance5
        Appearance6.BackColor = System.Drawing.SystemColors.Highlight
        Appearance6.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.cboMachineName.DisplayLayout.Override.ActiveRowAppearance = Appearance6
        Me.cboMachineName.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
        Me.cboMachineName.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
        Appearance7.BackColor = System.Drawing.SystemColors.Window
        Me.cboMachineName.DisplayLayout.Override.CardAreaAppearance = Appearance7
        Appearance8.BorderColor = System.Drawing.Color.Silver
        Appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
        Me.cboMachineName.DisplayLayout.Override.CellAppearance = Appearance8
        Me.cboMachineName.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
        Me.cboMachineName.DisplayLayout.Override.CellPadding = 0
        Appearance9.BackColor = System.Drawing.SystemColors.Control
        Appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
        Appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance9.BorderColor = System.Drawing.SystemColors.Window
        Me.cboMachineName.DisplayLayout.Override.GroupByRowAppearance = Appearance9
        Appearance10.TextHAlignAsString = "Left"
        Me.cboMachineName.DisplayLayout.Override.HeaderAppearance = Appearance10
        Me.cboMachineName.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Me.cboMachineName.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
        Appearance11.BackColor = System.Drawing.SystemColors.Window
        Appearance11.BorderColor = System.Drawing.Color.Silver
        Me.cboMachineName.DisplayLayout.Override.RowAppearance = Appearance11
        Me.cboMachineName.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
        Appearance12.BackColor = System.Drawing.SystemColors.ControlLight
        Me.cboMachineName.DisplayLayout.Override.TemplateAddRowAppearance = Appearance12
        Me.cboMachineName.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cboMachineName.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cboMachineName.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
        Me.cboMachineName.Font = New System.Drawing.Font("Verdana", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboMachineName.Location = New System.Drawing.Point(144, 243)
        Me.cboMachineName.Name = "cboMachineName"
        Me.cboMachineName.Size = New System.Drawing.Size(225, 20)
        Me.cboMachineName.TabIndex = 10
        '
        'Label87
        '
        Me.Label87.BackColor = System.Drawing.Color.Transparent
        Me.Label87.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label87.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label87.ForeColor = System.Drawing.Color.Black
        Me.Label87.Location = New System.Drawing.Point(5, 247)
        Me.Label87.Name = "Label87"
        Me.Label87.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label87.Size = New System.Drawing.Size(136, 16)
        Me.Label87.TabIndex = 211
        Me.Label87.Text = "Machine Operate :"
        Me.Label87.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtIFSCCode
        '
        Me.txtIFSCCode.AcceptsReturn = True
        Me.txtIFSCCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtIFSCCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIFSCCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIFSCCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIFSCCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtIFSCCode.Location = New System.Drawing.Point(144, 218)
        Me.txtIFSCCode.MaxLength = 0
        Me.txtIFSCCode.Name = "txtIFSCCode"
        Me.txtIFSCCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIFSCCode.Size = New System.Drawing.Size(223, 20)
        Me.txtIFSCCode.TabIndex = 9
        '
        'cboEmpCatType
        '
        Me.cboEmpCatType.BackColor = System.Drawing.SystemColors.Window
        Me.cboEmpCatType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboEmpCatType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEmpCatType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboEmpCatType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cboEmpCatType.Location = New System.Drawing.Point(144, 12)
        Me.cboEmpCatType.Name = "cboEmpCatType"
        Me.cboEmpCatType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboEmpCatType.Size = New System.Drawing.Size(223, 22)
        Me.cboEmpCatType.TabIndex = 0
        '
        'txtCostCenter
        '
        Me.txtCostCenter.AcceptsReturn = True
        Me.txtCostCenter.BackColor = System.Drawing.SystemColors.Window
        Me.txtCostCenter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCostCenter.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCostCenter.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCostCenter.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtCostCenter.Location = New System.Drawing.Point(144, 66)
        Me.txtCostCenter.MaxLength = 0
        Me.txtCostCenter.Name = "txtCostCenter"
        Me.txtCostCenter.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCostCenter.Size = New System.Drawing.Size(223, 20)
        Me.txtCostCenter.TabIndex = 2
        '
        'cboDept
        '
        Me.cboDept.BackColor = System.Drawing.SystemColors.Window
        Me.cboDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDept.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cboDept.Location = New System.Drawing.Point(144, 39)
        Me.cboDept.Name = "cboDept"
        Me.cboDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDept.Size = New System.Drawing.Size(223, 22)
        Me.cboDept.TabIndex = 1
        '
        'txtBankName
        '
        Me.txtBankName.AcceptsReturn = True
        Me.txtBankName.BackColor = System.Drawing.SystemColors.Window
        Me.txtBankName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBankName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBankName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtBankName.Location = New System.Drawing.Point(144, 168)
        Me.txtBankName.MaxLength = 0
        Me.txtBankName.Name = "txtBankName"
        Me.txtBankName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBankName.Size = New System.Drawing.Size(223, 20)
        Me.txtBankName.TabIndex = 7
        '
        'txtExperience
        '
        Me.txtExperience.AcceptsReturn = True
        Me.txtExperience.BackColor = System.Drawing.SystemColors.Window
        Me.txtExperience.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtExperience.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtExperience.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExperience.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtExperience.Location = New System.Drawing.Point(144, 143)
        Me.txtExperience.MaxLength = 0
        Me.txtExperience.Name = "txtExperience"
        Me.txtExperience.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExperience.Size = New System.Drawing.Size(43, 20)
        Me.txtExperience.TabIndex = 5
        '
        'txtLastCompany
        '
        Me.txtLastCompany.AcceptsReturn = True
        Me.txtLastCompany.BackColor = System.Drawing.SystemColors.Window
        Me.txtLastCompany.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLastCompany.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLastCompany.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLastCompany.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtLastCompany.Location = New System.Drawing.Point(144, 118)
        Me.txtLastCompany.MaxLength = 0
        Me.txtLastCompany.Name = "txtLastCompany"
        Me.txtLastCompany.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLastCompany.Size = New System.Drawing.Size(223, 20)
        Me.txtLastCompany.TabIndex = 4
        '
        'cbodesignation
        '
        Me.cbodesignation.BackColor = System.Drawing.SystemColors.Window
        Me.cbodesignation.Cursor = System.Windows.Forms.Cursors.Default
        Me.cbodesignation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbodesignation.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbodesignation.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cbodesignation.Location = New System.Drawing.Point(144, 91)
        Me.cbodesignation.Name = "cbodesignation"
        Me.cbodesignation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cbodesignation.Size = New System.Drawing.Size(223, 22)
        Me.cbodesignation.TabIndex = 3
        '
        'cboPaymentMode
        '
        Me.cboPaymentMode.BackColor = System.Drawing.SystemColors.Window
        Me.cboPaymentMode.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboPaymentMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPaymentMode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPaymentMode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboPaymentMode.Location = New System.Drawing.Point(278, 141)
        Me.cboPaymentMode.Name = "cboPaymentMode"
        Me.cboPaymentMode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboPaymentMode.Size = New System.Drawing.Size(89, 22)
        Me.cboPaymentMode.TabIndex = 6
        '
        'txtBankAcno
        '
        Me.txtBankAcno.AcceptsReturn = True
        Me.txtBankAcno.BackColor = System.Drawing.SystemColors.Window
        Me.txtBankAcno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBankAcno.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBankAcno.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankAcno.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtBankAcno.Location = New System.Drawing.Point(144, 193)
        Me.txtBankAcno.MaxLength = 0
        Me.txtBankAcno.Name = "txtBankAcno"
        Me.txtBankAcno.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBankAcno.Size = New System.Drawing.Size(223, 20)
        Me.txtBankAcno.TabIndex = 8
        '
        'Label78
        '
        Me.Label78.BackColor = System.Drawing.Color.Transparent
        Me.Label78.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label78.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label78.ForeColor = System.Drawing.Color.Black
        Me.Label78.Location = New System.Drawing.Point(5, 223)
        Me.Label78.Name = "Label78"
        Me.Label78.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label78.Size = New System.Drawing.Size(136, 16)
        Me.Label78.TabIndex = 209
        Me.Label78.Text = "Bank IFSC Code:"
        Me.Label78.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label61
        '
        Me.Label61.AutoSize = True
        Me.Label61.BackColor = System.Drawing.Color.Transparent
        Me.Label61.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label61.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label61.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label61.Location = New System.Drawing.Point(35, 16)
        Me.Label61.Name = "Label61"
        Me.Label61.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label61.Size = New System.Drawing.Size(106, 14)
        Me.Label61.TabIndex = 169
        Me.Label61.Text = "Emp. Category Type:"
        Me.Label61.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label57
        '
        Me.Label57.AutoSize = True
        Me.Label57.BackColor = System.Drawing.Color.Transparent
        Me.Label57.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label57.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label57.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label57.Location = New System.Drawing.Point(73, 41)
        Me.Label57.Name = "Label57"
        Me.Label57.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label57.Size = New System.Drawing.Size(68, 14)
        Me.Label57.TabIndex = 164
        Me.Label57.Text = "Department :"
        Me.Label57.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label4.Location = New System.Drawing.Point(71, 69)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(70, 14)
        Me.Label4.TabIndex = 163
        Me.Label4.Text = "Cost Center :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label47
        '
        Me.Label47.BackColor = System.Drawing.Color.Transparent
        Me.Label47.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label47.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label47.ForeColor = System.Drawing.Color.Black
        Me.Label47.Location = New System.Drawing.Point(5, 170)
        Me.Label47.Name = "Label47"
        Me.Label47.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label47.Size = New System.Drawing.Size(136, 16)
        Me.Label47.TabIndex = 145
        Me.Label47.Text = "Bank Name :"
        Me.Label47.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label40
        '
        Me.Label40.BackColor = System.Drawing.Color.Transparent
        Me.Label40.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label40.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.ForeColor = System.Drawing.Color.Black
        Me.Label40.Location = New System.Drawing.Point(5, 145)
        Me.Label40.Name = "Label40"
        Me.Label40.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label40.Size = New System.Drawing.Size(136, 16)
        Me.Label40.TabIndex = 142
        Me.Label40.Text = "Experience (In months) :"
        Me.Label40.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label37
        '
        Me.Label37.BackColor = System.Drawing.Color.Transparent
        Me.Label37.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label37.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.ForeColor = System.Drawing.Color.Black
        Me.Label37.Location = New System.Drawing.Point(5, 120)
        Me.Label37.Name = "Label37"
        Me.Label37.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label37.Size = New System.Drawing.Size(136, 16)
        Me.Label37.TabIndex = 141
        Me.Label37.Text = "Last Company Name :"
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.Black
        Me.Label24.Location = New System.Drawing.Point(152, 145)
        Me.Label24.Name = "Label24"
        Me.Label24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label24.Size = New System.Drawing.Size(130, 16)
        Me.Label24.TabIndex = 130
        Me.Label24.Text = "Payment Mode :"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label5.Location = New System.Drawing.Point(72, 94)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(69, 14)
        Me.Label5.TabIndex = 128
        Me.Label5.Text = "Designation :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblBankAcno
        '
        Me.lblBankAcno.BackColor = System.Drawing.Color.Transparent
        Me.lblBankAcno.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBankAcno.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBankAcno.ForeColor = System.Drawing.Color.Black
        Me.lblBankAcno.Location = New System.Drawing.Point(5, 197)
        Me.lblBankAcno.Name = "lblBankAcno"
        Me.lblBankAcno.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBankAcno.Size = New System.Drawing.Size(136, 16)
        Me.lblBankAcno.TabIndex = 109
        Me.lblBankAcno.Text = "Bank A/c No :"
        Me.lblBankAcno.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame8
        '
        Me.Frame8.BackColor = System.Drawing.SystemColors.Control
        Me.Frame8.Controls.Add(Me.chkDeptHOD)
        Me.Frame8.Controls.Add(Me.chkHRHOD)
        Me.Frame8.Controls.Add(Me.txtBonusDOJ)
        Me.Frame8.Controls.Add(Me.Label90)
        Me.Frame8.Controls.Add(Me.chkWFH)
        Me.Frame8.Controls.Add(Me.txtWorkingHours)
        Me.Frame8.Controls.Add(Me.Label85)
        Me.Frame8.Controls.Add(Me.txtNextIncDueDate)
        Me.Frame8.Controls.Add(Me.txtGroupDOJ)
        Me.Frame8.Controls.Add(Me.txtQualification)
        Me.Frame8.Controls.Add(Me.chkRGPAuthorization)
        Me.Frame8.Controls.Add(Me.chkStopSal)
        Me.Frame8.Controls.Add(Me.chkGroupInsurance)
        Me.Frame8.Controls.Add(Me.txtWorkingFrom)
        Me.Frame8.Controls.Add(Me.txtWorkingTo)
        Me.Frame8.Controls.Add(Me.txtReasonForLeaving)
        Me.Frame8.Controls.Add(Me.txtDOP)
        Me.Frame8.Controls.Add(Me.txtDOL)
        Me.Frame8.Controls.Add(Me.txtDOJ)
        Me.Frame8.Controls.Add(Me.CboJoinDesignation)
        Me.Frame8.Controls.Add(Me.cboWeeklyOff)
        Me.Frame8.Controls.Add(Me.Label68)
        Me.Frame8.Controls.Add(Me.Label62)
        Me.Frame8.Controls.Add(Me.Label29)
        Me.Frame8.Controls.Add(Me.Label21)
        Me.Frame8.Controls.Add(Me.Label38)
        Me.Frame8.Controls.Add(Me.Label39)
        Me.Frame8.Controls.Add(Me.Label17)
        Me.Frame8.Controls.Add(Me.Label14)
        Me.Frame8.Controls.Add(Me.Label18)
        Me.Frame8.Controls.Add(Me.Label20)
        Me.Frame8.Controls.Add(Me.Label10)
        Me.Frame8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame8.Location = New System.Drawing.Point(453, -1)
        Me.Frame8.Name = "Frame8"
        Me.Frame8.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame8.Size = New System.Drawing.Size(438, 290)
        Me.Frame8.TabIndex = 120
        Me.Frame8.TabStop = False
        '
        'chkDeptHOD
        '
        Me.chkDeptHOD.AutoSize = True
        Me.chkDeptHOD.BackColor = System.Drawing.SystemColors.Control
        Me.chkDeptHOD.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkDeptHOD.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkDeptHOD.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDeptHOD.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkDeptHOD.Location = New System.Drawing.Point(288, 246)
        Me.chkDeptHOD.Name = "chkDeptHOD"
        Me.chkDeptHOD.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkDeptHOD.Size = New System.Drawing.Size(73, 18)
        Me.chkDeptHOD.TabIndex = 182
        Me.chkDeptHOD.Text = "Dept HOD"
        Me.chkDeptHOD.UseVisualStyleBackColor = False
        '
        'chkHRHOD
        '
        Me.chkHRHOD.AutoSize = True
        Me.chkHRHOD.BackColor = System.Drawing.SystemColors.Control
        Me.chkHRHOD.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkHRHOD.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkHRHOD.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkHRHOD.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkHRHOD.Location = New System.Drawing.Point(177, 246)
        Me.chkHRHOD.Name = "chkHRHOD"
        Me.chkHRHOD.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkHRHOD.Size = New System.Drawing.Size(65, 18)
        Me.chkHRHOD.TabIndex = 181
        Me.chkHRHOD.Text = "HR HOD"
        Me.chkHRHOD.UseVisualStyleBackColor = False
        '
        'txtBonusDOJ
        '
        Me.txtBonusDOJ.AcceptsReturn = True
        Me.txtBonusDOJ.BackColor = System.Drawing.SystemColors.Window
        Me.txtBonusDOJ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBonusDOJ.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBonusDOJ.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBonusDOJ.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtBonusDOJ.Location = New System.Drawing.Point(290, 38)
        Me.txtBonusDOJ.MaxLength = 10
        Me.txtBonusDOJ.Name = "txtBonusDOJ"
        Me.txtBonusDOJ.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBonusDOJ.Size = New System.Drawing.Size(69, 20)
        Me.txtBonusDOJ.TabIndex = 179
        '
        'Label90
        '
        Me.Label90.AutoSize = True
        Me.Label90.BackColor = System.Drawing.Color.Transparent
        Me.Label90.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label90.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label90.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label90.Location = New System.Drawing.Point(203, 40)
        Me.Label90.Name = "Label90"
        Me.Label90.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label90.Size = New System.Drawing.Size(84, 14)
        Me.Label90.TabIndex = 180
        Me.Label90.Text = "DOJ for Bonus :"
        Me.Label90.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'chkWFH
        '
        Me.chkWFH.AutoSize = True
        Me.chkWFH.BackColor = System.Drawing.SystemColors.Control
        Me.chkWFH.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkWFH.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkWFH.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkWFH.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkWFH.Location = New System.Drawing.Point(4, 246)
        Me.chkWFH.Name = "chkWFH"
        Me.chkWFH.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkWFH.Size = New System.Drawing.Size(108, 18)
        Me.chkWFH.TabIndex = 13
        Me.chkWFH.Text = "Work Form Home"
        Me.chkWFH.UseVisualStyleBackColor = False
        '
        'txtWorkingHours
        '
        Me.txtWorkingHours.AcceptsReturn = True
        Me.txtWorkingHours.BackColor = System.Drawing.SystemColors.Window
        Me.txtWorkingHours.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWorkingHours.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWorkingHours.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWorkingHours.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtWorkingHours.Location = New System.Drawing.Point(322, 191)
        Me.txtWorkingHours.MaxLength = 10
        Me.txtWorkingHours.Name = "txtWorkingHours"
        Me.txtWorkingHours.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWorkingHours.Size = New System.Drawing.Size(39, 20)
        Me.txtWorkingHours.TabIndex = 10
        '
        'Label85
        '
        Me.Label85.BackColor = System.Drawing.Color.Transparent
        Me.Label85.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label85.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label85.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label85.Location = New System.Drawing.Point(239, 194)
        Me.Label85.Name = "Label85"
        Me.Label85.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label85.Size = New System.Drawing.Size(84, 13)
        Me.Label85.TabIndex = 178
        Me.Label85.Text = "Working Hours :"
        Me.Label85.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtNextIncDueDate
        '
        Me.txtNextIncDueDate.AcceptsReturn = True
        Me.txtNextIncDueDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtNextIncDueDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNextIncDueDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNextIncDueDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNextIncDueDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtNextIncDueDate.Location = New System.Drawing.Point(132, 37)
        Me.txtNextIncDueDate.MaxLength = 10
        Me.txtNextIncDueDate.Name = "txtNextIncDueDate"
        Me.txtNextIncDueDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNextIncDueDate.Size = New System.Drawing.Size(69, 20)
        Me.txtNextIncDueDate.TabIndex = 2
        '
        'txtGroupDOJ
        '
        Me.txtGroupDOJ.AcceptsReturn = True
        Me.txtGroupDOJ.BackColor = System.Drawing.SystemColors.Window
        Me.txtGroupDOJ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGroupDOJ.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGroupDOJ.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGroupDOJ.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtGroupDOJ.Location = New System.Drawing.Point(290, 114)
        Me.txtGroupDOJ.MaxLength = 10
        Me.txtGroupDOJ.Name = "txtGroupDOJ"
        Me.txtGroupDOJ.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGroupDOJ.Size = New System.Drawing.Size(69, 20)
        Me.txtGroupDOJ.TabIndex = 6
        '
        'txtQualification
        '
        Me.txtQualification.AcceptsReturn = True
        Me.txtQualification.BackColor = System.Drawing.SystemColors.Window
        Me.txtQualification.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtQualification.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtQualification.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtQualification.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtQualification.Location = New System.Drawing.Point(132, 62)
        Me.txtQualification.MaxLength = 0
        Me.txtQualification.Name = "txtQualification"
        Me.txtQualification.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtQualification.Size = New System.Drawing.Size(227, 20)
        Me.txtQualification.TabIndex = 3
        '
        'chkRGPAuthorization
        '
        Me.chkRGPAuthorization.AutoSize = True
        Me.chkRGPAuthorization.BackColor = System.Drawing.SystemColors.Control
        Me.chkRGPAuthorization.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkRGPAuthorization.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkRGPAuthorization.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRGPAuthorization.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRGPAuthorization.Location = New System.Drawing.Point(259, 218)
        Me.chkRGPAuthorization.Name = "chkRGPAuthorization"
        Me.chkRGPAuthorization.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkRGPAuthorization.Size = New System.Drawing.Size(102, 18)
        Me.chkRGPAuthorization.TabIndex = 12
        Me.chkRGPAuthorization.Text = "RGP Authorised"
        Me.chkRGPAuthorization.UseVisualStyleBackColor = False
        '
        'chkStopSal
        '
        Me.chkStopSal.AutoSize = True
        Me.chkStopSal.BackColor = System.Drawing.SystemColors.Control
        Me.chkStopSal.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkStopSal.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkStopSal.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkStopSal.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkStopSal.Location = New System.Drawing.Point(30, 218)
        Me.chkStopSal.Name = "chkStopSal"
        Me.chkStopSal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkStopSal.Size = New System.Drawing.Size(82, 18)
        Me.chkStopSal.TabIndex = 38
        Me.chkStopSal.Text = "Stop Salary"
        Me.chkStopSal.UseVisualStyleBackColor = False
        '
        'chkGroupInsurance
        '
        Me.chkGroupInsurance.AutoSize = True
        Me.chkGroupInsurance.BackColor = System.Drawing.SystemColors.Control
        Me.chkGroupInsurance.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkGroupInsurance.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkGroupInsurance.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGroupInsurance.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkGroupInsurance.Location = New System.Drawing.Point(135, 218)
        Me.chkGroupInsurance.Name = "chkGroupInsurance"
        Me.chkGroupInsurance.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkGroupInsurance.Size = New System.Drawing.Size(107, 18)
        Me.chkGroupInsurance.TabIndex = 11
        Me.chkGroupInsurance.Text = "Group Insurance"
        Me.chkGroupInsurance.UseVisualStyleBackColor = False
        '
        'txtWorkingFrom
        '
        Me.txtWorkingFrom.AcceptsReturn = True
        Me.txtWorkingFrom.BackColor = System.Drawing.SystemColors.Window
        Me.txtWorkingFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWorkingFrom.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWorkingFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWorkingFrom.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtWorkingFrom.Location = New System.Drawing.Point(132, 191)
        Me.txtWorkingFrom.MaxLength = 10
        Me.txtWorkingFrom.Name = "txtWorkingFrom"
        Me.txtWorkingFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWorkingFrom.Size = New System.Drawing.Size(39, 20)
        Me.txtWorkingFrom.TabIndex = 9
        '
        'txtWorkingTo
        '
        Me.txtWorkingTo.AcceptsReturn = True
        Me.txtWorkingTo.BackColor = System.Drawing.SystemColors.Window
        Me.txtWorkingTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWorkingTo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWorkingTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWorkingTo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtWorkingTo.Location = New System.Drawing.Point(198, 191)
        Me.txtWorkingTo.MaxLength = 10
        Me.txtWorkingTo.Name = "txtWorkingTo"
        Me.txtWorkingTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWorkingTo.Size = New System.Drawing.Size(39, 20)
        Me.txtWorkingTo.TabIndex = 37
        '
        'txtReasonForLeaving
        '
        Me.txtReasonForLeaving.AcceptsReturn = True
        Me.txtReasonForLeaving.BackColor = System.Drawing.SystemColors.Window
        Me.txtReasonForLeaving.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReasonForLeaving.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtReasonForLeaving.Enabled = False
        Me.txtReasonForLeaving.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReasonForLeaving.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtReasonForLeaving.Location = New System.Drawing.Point(132, 139)
        Me.txtReasonForLeaving.MaxLength = 10
        Me.txtReasonForLeaving.Name = "txtReasonForLeaving"
        Me.txtReasonForLeaving.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtReasonForLeaving.Size = New System.Drawing.Size(227, 20)
        Me.txtReasonForLeaving.TabIndex = 7
        '
        'txtDOP
        '
        Me.txtDOP.AcceptsReturn = True
        Me.txtDOP.BackColor = System.Drawing.SystemColors.Window
        Me.txtDOP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDOP.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDOP.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDOP.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtDOP.Location = New System.Drawing.Point(290, 12)
        Me.txtDOP.MaxLength = 10
        Me.txtDOP.Name = "txtDOP"
        Me.txtDOP.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDOP.Size = New System.Drawing.Size(69, 20)
        Me.txtDOP.TabIndex = 1
        '
        'txtDOL
        '
        Me.txtDOL.AcceptsReturn = True
        Me.txtDOL.BackColor = System.Drawing.SystemColors.Window
        Me.txtDOL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDOL.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDOL.Enabled = False
        Me.txtDOL.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDOL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtDOL.Location = New System.Drawing.Point(132, 114)
        Me.txtDOL.MaxLength = 10
        Me.txtDOL.Name = "txtDOL"
        Me.txtDOL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDOL.Size = New System.Drawing.Size(69, 20)
        Me.txtDOL.TabIndex = 5
        '
        'txtDOJ
        '
        Me.txtDOJ.AcceptsReturn = True
        Me.txtDOJ.BackColor = System.Drawing.SystemColors.Window
        Me.txtDOJ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDOJ.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDOJ.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDOJ.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtDOJ.Location = New System.Drawing.Point(132, 12)
        Me.txtDOJ.MaxLength = 10
        Me.txtDOJ.Name = "txtDOJ"
        Me.txtDOJ.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDOJ.Size = New System.Drawing.Size(69, 20)
        Me.txtDOJ.TabIndex = 0
        '
        'CboJoinDesignation
        '
        Me.CboJoinDesignation.BackColor = System.Drawing.SystemColors.Window
        Me.CboJoinDesignation.Cursor = System.Windows.Forms.Cursors.Default
        Me.CboJoinDesignation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboJoinDesignation.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboJoinDesignation.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.CboJoinDesignation.Location = New System.Drawing.Point(132, 87)
        Me.CboJoinDesignation.Name = "CboJoinDesignation"
        Me.CboJoinDesignation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CboJoinDesignation.Size = New System.Drawing.Size(227, 22)
        Me.CboJoinDesignation.Sorted = True
        Me.CboJoinDesignation.TabIndex = 4
        '
        'cboWeeklyOff
        '
        Me.cboWeeklyOff.BackColor = System.Drawing.SystemColors.Window
        Me.cboWeeklyOff.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboWeeklyOff.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboWeeklyOff.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboWeeklyOff.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cboWeeklyOff.Location = New System.Drawing.Point(132, 164)
        Me.cboWeeklyOff.Name = "cboWeeklyOff"
        Me.cboWeeklyOff.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboWeeklyOff.Size = New System.Drawing.Size(229, 22)
        Me.cboWeeklyOff.Sorted = True
        Me.cboWeeklyOff.TabIndex = 8
        '
        'Label68
        '
        Me.Label68.BackColor = System.Drawing.Color.Transparent
        Me.Label68.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label68.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label68.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label68.Location = New System.Drawing.Point(2, 39)
        Me.Label68.Name = "Label68"
        Me.Label68.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label68.Size = New System.Drawing.Size(127, 13)
        Me.Label68.TabIndex = 177
        Me.Label68.Text = "Next Increment Date :"
        Me.Label68.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label62
        '
        Me.Label62.AutoSize = True
        Me.Label62.BackColor = System.Drawing.Color.Transparent
        Me.Label62.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label62.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label62.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label62.Location = New System.Drawing.Point(208, 120)
        Me.Label62.Name = "Label62"
        Me.Label62.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label62.Size = New System.Drawing.Size(76, 14)
        Me.Label62.TabIndex = 170
        Me.Label62.Text = "Group Joining:"
        Me.Label62.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.Transparent
        Me.Label29.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.Color.Black
        Me.Label29.Location = New System.Drawing.Point(9, 64)
        Me.Label29.Name = "Label29"
        Me.Label29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label29.Size = New System.Drawing.Size(120, 16)
        Me.Label29.TabIndex = 162
        Me.Label29.Text = "Qualification :"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(2, 167)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(127, 13)
        Me.Label21.TabIndex = 129
        Me.Label21.Text = "Weekly Off :"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label38
        '
        Me.Label38.BackColor = System.Drawing.Color.Transparent
        Me.Label38.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label38.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label38.Location = New System.Drawing.Point(2, 194)
        Me.Label38.Name = "Label38"
        Me.Label38.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label38.Size = New System.Drawing.Size(127, 13)
        Me.Label38.TabIndex = 127
        Me.Label38.Text = "Working Time From :"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.BackColor = System.Drawing.Color.Transparent
        Me.Label39.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label39.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label39.Location = New System.Drawing.Point(174, 194)
        Me.Label39.Name = "Label39"
        Me.Label39.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label39.Size = New System.Drawing.Size(24, 14)
        Me.Label39.TabIndex = 126
        Me.Label39.Text = "To :"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(2, 142)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(127, 13)
        Me.Label17.TabIndex = 125
        Me.Label17.Text = "Reason for Leaving :"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(203, 14)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(77, 14)
        Me.Label14.TabIndex = 124
        Me.Label14.Text = "Permanent Dt.:"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(2, 117)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(127, 13)
        Me.Label18.TabIndex = 123
        Me.Label18.Text = "Date of Leaving :"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(2, 92)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(127, 13)
        Me.Label20.TabIndex = 122
        Me.Label20.Text = "Joining Designation :"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(2, 14)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(127, 13)
        Me.Label10.TabIndex = 121
        Me.Label10.Text = "Date of Joining :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_SSTab1_TabPage1
        '
        Me._SSTab1_TabPage1.Controls.Add(Me.txtMobileOff)
        Me._SSTab1_TabPage1.Controls.Add(Me.Frame9)
        Me._SSTab1_TabPage1.Controls.Add(Me.txtEmail)
        Me._SSTab1_TabPage1.Controls.Add(Me.txtOffeMail)
        Me._SSTab1_TabPage1.Controls.Add(Me.fraPermAdd)
        Me._SSTab1_TabPage1.Controls.Add(Me.Label77)
        Me._SSTab1_TabPage1.Controls.Add(Me.Label28)
        Me._SSTab1_TabPage1.Controls.Add(Me.Label64)
        Me._SSTab1_TabPage1.Location = New System.Drawing.Point(4, 25)
        Me._SSTab1_TabPage1.Name = "_SSTab1_TabPage1"
        Me._SSTab1_TabPage1.Size = New System.Drawing.Size(896, 291)
        Me._SSTab1_TabPage1.TabIndex = 1
        Me._SSTab1_TabPage1.Text = "Mail"
        '
        'txtMobileOff
        '
        Me.txtMobileOff.AcceptsReturn = True
        Me.txtMobileOff.BackColor = System.Drawing.SystemColors.Window
        Me.txtMobileOff.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMobileOff.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMobileOff.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMobileOff.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtMobileOff.Location = New System.Drawing.Point(442, 155)
        Me.txtMobileOff.MaxLength = 30
        Me.txtMobileOff.Name = "txtMobileOff"
        Me.txtMobileOff.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMobileOff.Size = New System.Drawing.Size(257, 20)
        Me.txtMobileOff.TabIndex = 2
        '
        'Frame9
        '
        Me.Frame9.BackColor = System.Drawing.SystemColors.Control
        Me.Frame9.Controls.Add(Me.txtPState)
        Me.Frame9.Controls.Add(Me.txtPAddress)
        Me.Frame9.Controls.Add(Me.txtPCity)
        Me.Frame9.Controls.Add(Me.txtPPinCode)
        Me.Frame9.Controls.Add(Me.txtPPhone)
        Me.Frame9.Controls.Add(Me.chkPMetroCity)
        Me.Frame9.Controls.Add(Me.Label72)
        Me.Frame9.Controls.Add(Me.Label71)
        Me.Frame9.Controls.Add(Me.Label70)
        Me.Frame9.Controls.Add(Me.Label69)
        Me.Frame9.Controls.Add(Me.Label36)
        Me.Frame9.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Frame9.Location = New System.Drawing.Point(370, 3)
        Me.Frame9.Name = "Frame9"
        Me.Frame9.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame9.Size = New System.Drawing.Size(365, 151)
        Me.Frame9.TabIndex = 194
        Me.Frame9.TabStop = False
        Me.Frame9.Text = "Address-Permanent"
        '
        'txtPState
        '
        Me.txtPState.AcceptsReturn = True
        Me.txtPState.BackColor = System.Drawing.SystemColors.Window
        Me.txtPState.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPState.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPState.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtPState.Location = New System.Drawing.Point(72, 104)
        Me.txtPState.MaxLength = 20
        Me.txtPState.Name = "txtPState"
        Me.txtPState.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPState.Size = New System.Drawing.Size(289, 20)
        Me.txtPState.TabIndex = 1
        '
        'txtPAddress
        '
        Me.txtPAddress.AcceptsReturn = True
        Me.txtPAddress.BackColor = System.Drawing.SystemColors.Window
        Me.txtPAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPAddress.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPAddress.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPAddress.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtPAddress.Location = New System.Drawing.Point(72, 14)
        Me.txtPAddress.MaxLength = 30
        Me.txtPAddress.Multiline = True
        Me.txtPAddress.Name = "txtPAddress"
        Me.txtPAddress.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPAddress.Size = New System.Drawing.Size(289, 45)
        Me.txtPAddress.TabIndex = 4
        '
        'txtPCity
        '
        Me.txtPCity.AcceptsReturn = True
        Me.txtPCity.BackColor = System.Drawing.SystemColors.Window
        Me.txtPCity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPCity.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPCity.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPCity.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtPCity.Location = New System.Drawing.Point(72, 60)
        Me.txtPCity.MaxLength = 30
        Me.txtPCity.Name = "txtPCity"
        Me.txtPCity.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPCity.Size = New System.Drawing.Size(289, 20)
        Me.txtPCity.TabIndex = 3
        '
        'txtPPinCode
        '
        Me.txtPPinCode.AcceptsReturn = True
        Me.txtPPinCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtPPinCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPPinCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPPinCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPPinCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtPPinCode.Location = New System.Drawing.Point(72, 82)
        Me.txtPPinCode.MaxLength = 30
        Me.txtPPinCode.Name = "txtPPinCode"
        Me.txtPPinCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPPinCode.Size = New System.Drawing.Size(187, 20)
        Me.txtPPinCode.TabIndex = 2
        '
        'txtPPhone
        '
        Me.txtPPhone.AcceptsReturn = True
        Me.txtPPhone.BackColor = System.Drawing.SystemColors.Window
        Me.txtPPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPPhone.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPPhone.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPPhone.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtPPhone.Location = New System.Drawing.Point(72, 126)
        Me.txtPPhone.MaxLength = 30
        Me.txtPPhone.Name = "txtPPhone"
        Me.txtPPhone.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPPhone.Size = New System.Drawing.Size(289, 20)
        Me.txtPPhone.TabIndex = 0
        '
        'chkPMetroCity
        '
        Me.chkPMetroCity.AutoSize = True
        Me.chkPMetroCity.BackColor = System.Drawing.SystemColors.Control
        Me.chkPMetroCity.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPMetroCity.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPMetroCity.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPMetroCity.Location = New System.Drawing.Point(266, 84)
        Me.chkPMetroCity.Name = "chkPMetroCity"
        Me.chkPMetroCity.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPMetroCity.Size = New System.Drawing.Size(85, 18)
        Me.chkPMetroCity.TabIndex = 5
        Me.chkPMetroCity.Text = "Is Metro City"
        Me.chkPMetroCity.UseVisualStyleBackColor = False
        '
        'Label72
        '
        Me.Label72.AutoSize = True
        Me.Label72.BackColor = System.Drawing.SystemColors.Control
        Me.Label72.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label72.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label72.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label72.Location = New System.Drawing.Point(23, 106)
        Me.Label72.Name = "Label72"
        Me.Label72.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label72.Size = New System.Drawing.Size(38, 14)
        Me.Label72.TabIndex = 199
        Me.Label72.Text = "State :"
        Me.Label72.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label71
        '
        Me.Label71.AutoSize = True
        Me.Label71.BackColor = System.Drawing.SystemColors.Control
        Me.Label71.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label71.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label71.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label71.Location = New System.Drawing.Point(8, 16)
        Me.Label71.Name = "Label71"
        Me.Label71.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label71.Size = New System.Drawing.Size(55, 14)
        Me.Label71.TabIndex = 198
        Me.Label71.Text = "Address :"
        Me.Label71.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label70
        '
        Me.Label70.AutoSize = True
        Me.Label70.BackColor = System.Drawing.SystemColors.Control
        Me.Label70.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label70.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label70.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label70.Location = New System.Drawing.Point(32, 62)
        Me.Label70.Name = "Label70"
        Me.Label70.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label70.Size = New System.Drawing.Size(31, 14)
        Me.Label70.TabIndex = 197
        Me.Label70.Text = "City :"
        Me.Label70.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label69
        '
        Me.Label69.AutoSize = True
        Me.Label69.BackColor = System.Drawing.SystemColors.Control
        Me.Label69.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label69.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label69.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label69.Location = New System.Drawing.Point(3, 128)
        Me.Label69.Name = "Label69"
        Me.Label69.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label69.Size = New System.Drawing.Size(57, 14)
        Me.Label69.TabIndex = 196
        Me.Label69.Text = "Phone(s) :"
        Me.Label69.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.BackColor = System.Drawing.SystemColors.Control
        Me.Label36.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label36.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label36.Location = New System.Drawing.Point(2, 84)
        Me.Label36.Name = "Label36"
        Me.Label36.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label36.Size = New System.Drawing.Size(55, 14)
        Me.Label36.TabIndex = 195
        Me.Label36.Text = "Pin Code :"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtEmail
        '
        Me.txtEmail.AcceptsReturn = True
        Me.txtEmail.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmail.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmail.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtEmail.Location = New System.Drawing.Point(112, 155)
        Me.txtEmail.MaxLength = 30
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmail.Size = New System.Drawing.Size(257, 20)
        Me.txtEmail.TabIndex = 0
        '
        'txtOffeMail
        '
        Me.txtOffeMail.AcceptsReturn = True
        Me.txtOffeMail.BackColor = System.Drawing.SystemColors.Window
        Me.txtOffeMail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOffeMail.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOffeMail.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOffeMail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtOffeMail.Location = New System.Drawing.Point(112, 180)
        Me.txtOffeMail.MaxLength = 30
        Me.txtOffeMail.Name = "txtOffeMail"
        Me.txtOffeMail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOffeMail.Size = New System.Drawing.Size(257, 20)
        Me.txtOffeMail.TabIndex = 1
        '
        'fraPermAdd
        '
        Me.fraPermAdd.BackColor = System.Drawing.SystemColors.Control
        Me.fraPermAdd.Controls.Add(Me.chkMetroCity)
        Me.fraPermAdd.Controls.Add(Me.txtPhone)
        Me.fraPermAdd.Controls.Add(Me.txtPinCode)
        Me.fraPermAdd.Controls.Add(Me.txtCity)
        Me.fraPermAdd.Controls.Add(Me.txtAddress)
        Me.fraPermAdd.Controls.Add(Me.txtState)
        Me.fraPermAdd.Controls.Add(Me.Label27)
        Me.fraPermAdd.Controls.Add(Me.Label26)
        Me.fraPermAdd.Controls.Add(Me.Label9)
        Me.fraPermAdd.Controls.Add(Me.Label8)
        Me.fraPermAdd.Controls.Add(Me.Label22)
        Me.fraPermAdd.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraPermAdd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.fraPermAdd.Location = New System.Drawing.Point(4, 3)
        Me.fraPermAdd.Name = "fraPermAdd"
        Me.fraPermAdd.Padding = New System.Windows.Forms.Padding(0)
        Me.fraPermAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraPermAdd.Size = New System.Drawing.Size(365, 151)
        Me.fraPermAdd.TabIndex = 103
        Me.fraPermAdd.TabStop = False
        Me.fraPermAdd.Text = "Address-Current"
        '
        'chkMetroCity
        '
        Me.chkMetroCity.AutoSize = True
        Me.chkMetroCity.BackColor = System.Drawing.SystemColors.Control
        Me.chkMetroCity.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkMetroCity.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMetroCity.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkMetroCity.Location = New System.Drawing.Point(266, 84)
        Me.chkMetroCity.Name = "chkMetroCity"
        Me.chkMetroCity.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkMetroCity.Size = New System.Drawing.Size(85, 18)
        Me.chkMetroCity.TabIndex = 3
        Me.chkMetroCity.Text = "Is Metro City"
        Me.chkMetroCity.UseVisualStyleBackColor = False
        '
        'txtPhone
        '
        Me.txtPhone.AcceptsReturn = True
        Me.txtPhone.BackColor = System.Drawing.SystemColors.Window
        Me.txtPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPhone.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPhone.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPhone.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtPhone.Location = New System.Drawing.Point(72, 126)
        Me.txtPhone.MaxLength = 30
        Me.txtPhone.Name = "txtPhone"
        Me.txtPhone.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPhone.Size = New System.Drawing.Size(289, 20)
        Me.txtPhone.TabIndex = 5
        '
        'txtPinCode
        '
        Me.txtPinCode.AcceptsReturn = True
        Me.txtPinCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtPinCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPinCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPinCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPinCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtPinCode.Location = New System.Drawing.Point(72, 82)
        Me.txtPinCode.MaxLength = 30
        Me.txtPinCode.Name = "txtPinCode"
        Me.txtPinCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPinCode.Size = New System.Drawing.Size(187, 20)
        Me.txtPinCode.TabIndex = 2
        '
        'txtCity
        '
        Me.txtCity.AcceptsReturn = True
        Me.txtCity.BackColor = System.Drawing.SystemColors.Window
        Me.txtCity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCity.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCity.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCity.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtCity.Location = New System.Drawing.Point(72, 60)
        Me.txtCity.MaxLength = 30
        Me.txtCity.Name = "txtCity"
        Me.txtCity.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCity.Size = New System.Drawing.Size(289, 20)
        Me.txtCity.TabIndex = 1
        '
        'txtAddress
        '
        Me.txtAddress.AcceptsReturn = True
        Me.txtAddress.BackColor = System.Drawing.SystemColors.Window
        Me.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAddress.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtAddress.Location = New System.Drawing.Point(72, 14)
        Me.txtAddress.MaxLength = 30
        Me.txtAddress.Multiline = True
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAddress.Size = New System.Drawing.Size(289, 45)
        Me.txtAddress.TabIndex = 0
        '
        'txtState
        '
        Me.txtState.AcceptsReturn = True
        Me.txtState.BackColor = System.Drawing.SystemColors.Window
        Me.txtState.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtState.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtState.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtState.Location = New System.Drawing.Point(72, 104)
        Me.txtState.MaxLength = 20
        Me.txtState.Name = "txtState"
        Me.txtState.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtState.Size = New System.Drawing.Size(289, 20)
        Me.txtState.TabIndex = 4
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.SystemColors.Control
        Me.Label27.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label27.Location = New System.Drawing.Point(8, 84)
        Me.Label27.Name = "Label27"
        Me.Label27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label27.Size = New System.Drawing.Size(55, 14)
        Me.Label27.TabIndex = 140
        Me.Label27.Text = "Pin Code :"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.SystemColors.Control
        Me.Label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(9, 128)
        Me.Label26.Name = "Label26"
        Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label26.Size = New System.Drawing.Size(57, 14)
        Me.Label26.TabIndex = 139
        Me.Label26.Text = "Phone(s) :"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(38, 62)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(31, 14)
        Me.Label9.TabIndex = 138
        Me.Label9.Text = "City :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(14, 16)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(55, 14)
        Me.Label8.TabIndex = 104
        Me.Label8.Text = "Address :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.SystemColors.Control
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(29, 106)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(38, 14)
        Me.Label22.TabIndex = 105
        Me.Label22.Text = "State :"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label77
        '
        Me.Label77.AutoSize = True
        Me.Label77.BackColor = System.Drawing.SystemColors.Control
        Me.Label77.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label77.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label77.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label77.Location = New System.Drawing.Point(384, 158)
        Me.Label77.Name = "Label77"
        Me.Label77.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label77.Size = New System.Drawing.Size(62, 14)
        Me.Label77.TabIndex = 205
        Me.Label77.Text = "Mobile (O) :"
        Me.Label77.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.SystemColors.Control
        Me.Label28.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label28.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label28.Location = New System.Drawing.Point(6, 157)
        Me.Label28.Name = "Label28"
        Me.Label28.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label28.Size = New System.Drawing.Size(94, 14)
        Me.Label28.TabIndex = 193
        Me.Label28.Text = "Personal Email-id :"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label64
        '
        Me.Label64.AutoSize = True
        Me.Label64.BackColor = System.Drawing.SystemColors.Control
        Me.Label64.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label64.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label64.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label64.Location = New System.Drawing.Point(15, 182)
        Me.Label64.Name = "Label64"
        Me.Label64.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label64.Size = New System.Drawing.Size(86, 14)
        Me.Label64.TabIndex = 192
        Me.Label64.Text = "Official Email-id :"
        Me.Label64.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_SSTab1_TabPage2
        '
        Me._SSTab1_TabPage2.Controls.Add(Me.GroupBox2)
        Me._SSTab1_TabPage2.Controls.Add(Me.Frame4)
        Me._SSTab1_TabPage2.Controls.Add(Me.Frame7)
        Me._SSTab1_TabPage2.Controls.Add(Me.chkBonusApp)
        Me._SSTab1_TabPage2.Controls.Add(Me.chkLEApp)
        Me._SSTab1_TabPage2.Controls.Add(Me.Frame6)
        Me._SSTab1_TabPage2.Controls.Add(Me.Frame5)
        Me._SSTab1_TabPage2.Controls.Add(Me.Frame2)
        Me._SSTab1_TabPage2.Controls.Add(Me.Frame1)
        Me._SSTab1_TabPage2.Controls.Add(Me.ESI)
        Me._SSTab1_TabPage2.Controls.Add(Me.Frame3)
        Me._SSTab1_TabPage2.Location = New System.Drawing.Point(4, 25)
        Me._SSTab1_TabPage2.Name = "_SSTab1_TabPage2"
        Me._SSTab1_TabPage2.Size = New System.Drawing.Size(896, 291)
        Me._SSTab1_TabPage2.TabIndex = 2
        Me._SSTab1_TabPage2.Text = "PF/ESI Detail"
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox2.Controls.Add(Me.Label88)
        Me.GroupBox2.Controls.Add(Me.txtDAAmount)
        Me.GroupBox2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GroupBox2.Location = New System.Drawing.Point(635, 162)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox2.Size = New System.Drawing.Size(135, 38)
        Me.GroupBox2.TabIndex = 207
        Me.GroupBox2.TabStop = False
        '
        'Label88
        '
        Me.Label88.AutoSize = True
        Me.Label88.BackColor = System.Drawing.Color.Transparent
        Me.Label88.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label88.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label88.ForeColor = System.Drawing.Color.Black
        Me.Label88.Location = New System.Drawing.Point(5, 15)
        Me.Label88.Name = "Label88"
        Me.Label88.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label88.Size = New System.Drawing.Size(66, 14)
        Me.Label88.TabIndex = 191
        Me.Label88.Text = "DA Amount :"
        '
        'txtDAAmount
        '
        Me.txtDAAmount.AcceptsReturn = True
        Me.txtDAAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtDAAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDAAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDAAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDAAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtDAAmount.Location = New System.Drawing.Point(80, 12)
        Me.txtDAAmount.MaxLength = 1
        Me.txtDAAmount.Name = "txtDAAmount"
        Me.txtDAAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDAAmount.Size = New System.Drawing.Size(44, 20)
        Me.txtDAAmount.TabIndex = 76
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.txtDOBActual)
        Me.Frame4.Controls.Add(Me.txtDOM)
        Me.Frame4.Controls.Add(Me.Label80)
        Me.Frame4.Controls.Add(Me.Label79)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Frame4.Location = New System.Drawing.Point(372, 73)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(398, 38)
        Me.Frame4.TabIndex = 206
        Me.Frame4.TabStop = False
        Me.Frame4.Text = "Others Details"
        '
        'txtDOBActual
        '
        Me.txtDOBActual.AcceptsReturn = True
        Me.txtDOBActual.BackColor = System.Drawing.SystemColors.Window
        Me.txtDOBActual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDOBActual.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDOBActual.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDOBActual.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtDOBActual.Location = New System.Drawing.Point(85, 12)
        Me.txtDOBActual.MaxLength = 5
        Me.txtDOBActual.Name = "txtDOBActual"
        Me.txtDOBActual.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDOBActual.Size = New System.Drawing.Size(79, 20)
        Me.txtDOBActual.TabIndex = 68
        '
        'txtDOM
        '
        Me.txtDOM.AcceptsReturn = True
        Me.txtDOM.BackColor = System.Drawing.SystemColors.Window
        Me.txtDOM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDOM.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDOM.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDOM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtDOM.Location = New System.Drawing.Point(267, 12)
        Me.txtDOM.MaxLength = 5
        Me.txtDOM.Name = "txtDOM"
        Me.txtDOM.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDOM.Size = New System.Drawing.Size(126, 20)
        Me.txtDOM.TabIndex = 69
        '
        'Label80
        '
        Me.Label80.AutoSize = True
        Me.Label80.BackColor = System.Drawing.SystemColors.Control
        Me.Label80.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label80.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label80.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label80.Location = New System.Drawing.Point(10, 14)
        Me.Label80.Name = "Label80"
        Me.Label80.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label80.Size = New System.Drawing.Size(77, 14)
        Me.Label80.TabIndex = 208
        Me.Label80.Text = "DOB (Actual) :"
        Me.Label80.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label79
        '
        Me.Label79.AutoSize = True
        Me.Label79.BackColor = System.Drawing.SystemColors.Control
        Me.Label79.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label79.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label79.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label79.Location = New System.Drawing.Point(174, 14)
        Me.Label79.Name = "Label79"
        Me.Label79.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label79.Size = New System.Drawing.Size(93, 14)
        Me.Label79.TabIndex = 207
        Me.Label79.Text = "Date of Marriage :"
        Me.Label79.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame7
        '
        Me.Frame7.BackColor = System.Drawing.SystemColors.Control
        Me.Frame7.Controls.Add(Me.cboOverTime)
        Me.Frame7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Frame7.Location = New System.Drawing.Point(634, 114)
        Me.Frame7.Name = "Frame7"
        Me.Frame7.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame7.Size = New System.Drawing.Size(135, 46)
        Me.Frame7.TabIndex = 191
        Me.Frame7.TabStop = False
        Me.Frame7.Text = "Over Time"
        '
        'cboOverTime
        '
        Me.cboOverTime.BackColor = System.Drawing.SystemColors.Window
        Me.cboOverTime.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboOverTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOverTime.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboOverTime.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboOverTime.Location = New System.Drawing.Point(4, 16)
        Me.cboOverTime.Name = "cboOverTime"
        Me.cboOverTime.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboOverTime.Size = New System.Drawing.Size(128, 22)
        Me.cboOverTime.TabIndex = 76
        '
        'chkBonusApp
        '
        Me.chkBonusApp.AutoSize = True
        Me.chkBonusApp.BackColor = System.Drawing.SystemColors.Control
        Me.chkBonusApp.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkBonusApp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBonusApp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkBonusApp.Location = New System.Drawing.Point(4, 219)
        Me.chkBonusApp.Name = "chkBonusApp"
        Me.chkBonusApp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkBonusApp.Size = New System.Drawing.Size(120, 18)
        Me.chkBonusApp.TabIndex = 77
        Me.chkBonusApp.Text = "Is Bonus Applicable"
        Me.chkBonusApp.UseVisualStyleBackColor = False
        '
        'chkLEApp
        '
        Me.chkLEApp.AutoSize = True
        Me.chkLEApp.BackColor = System.Drawing.SystemColors.Control
        Me.chkLEApp.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkLEApp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkLEApp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkLEApp.Location = New System.Drawing.Point(4, 243)
        Me.chkLEApp.Name = "chkLEApp"
        Me.chkLEApp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkLEApp.Size = New System.Drawing.Size(107, 18)
        Me.chkLEApp.TabIndex = 78
        Me.chkLEApp.Text = "Is L.E. Applicable"
        Me.chkLEApp.UseVisualStyleBackColor = False
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.txtOTRate)
        Me.Frame6.Controls.Add(Me.txtLTAAmount)
        Me.Frame6.Controls.Add(Me.txtBonusPer)
        Me.Frame6.Controls.Add(Me.Label16)
        Me.Frame6.Controls.Add(Me.Label56)
        Me.Frame6.Controls.Add(Me.Label55)
        Me.Frame6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Frame6.Location = New System.Drawing.Point(515, 114)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(118, 86)
        Me.Frame6.TabIndex = 155
        Me.Frame6.TabStop = False
        Me.Frame6.Text = "Others"
        '
        'txtOTRate
        '
        Me.txtOTRate.AcceptsReturn = True
        Me.txtOTRate.BackColor = System.Drawing.SystemColors.Window
        Me.txtOTRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOTRate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOTRate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOTRate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtOTRate.Location = New System.Drawing.Point(73, 58)
        Me.txtOTRate.MaxLength = 1
        Me.txtOTRate.Name = "txtOTRate"
        Me.txtOTRate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOTRate.Size = New System.Drawing.Size(41, 20)
        Me.txtOTRate.TabIndex = 75
        '
        'txtLTAAmount
        '
        Me.txtLTAAmount.AcceptsReturn = True
        Me.txtLTAAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtLTAAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLTAAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLTAAmount.Enabled = False
        Me.txtLTAAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLTAAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtLTAAmount.Location = New System.Drawing.Point(73, 14)
        Me.txtLTAAmount.MaxLength = 5
        Me.txtLTAAmount.Name = "txtLTAAmount"
        Me.txtLTAAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLTAAmount.Size = New System.Drawing.Size(40, 20)
        Me.txtLTAAmount.TabIndex = 73
        Me.txtLTAAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBonusPer
        '
        Me.txtBonusPer.AcceptsReturn = True
        Me.txtBonusPer.BackColor = System.Drawing.SystemColors.Window
        Me.txtBonusPer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBonusPer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBonusPer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBonusPer.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtBonusPer.Location = New System.Drawing.Point(73, 36)
        Me.txtBonusPer.MaxLength = 5
        Me.txtBonusPer.Name = "txtBonusPer"
        Me.txtBonusPer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBonusPer.Size = New System.Drawing.Size(40, 20)
        Me.txtBonusPer.TabIndex = 74
        Me.txtBonusPer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(16, 61)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(52, 14)
        Me.Label16.TabIndex = 190
        Me.Label16.Text = "OT Rate :"
        '
        'Label56
        '
        Me.Label56.AutoSize = True
        Me.Label56.BackColor = System.Drawing.SystemColors.Control
        Me.Label56.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label56.Enabled = False
        Me.Label56.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label56.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label56.Location = New System.Drawing.Point(-1, 17)
        Me.Label56.Name = "Label56"
        Me.Label56.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label56.Size = New System.Drawing.Size(69, 14)
        Me.Label56.TabIndex = 157
        Me.Label56.Text = "LTA Amount :"
        Me.Label56.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label55
        '
        Me.Label55.AutoSize = True
        Me.Label55.BackColor = System.Drawing.SystemColors.Control
        Me.Label55.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label55.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label55.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label55.Location = New System.Drawing.Point(11, 39)
        Me.Label55.Name = "Label55"
        Me.Label55.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label55.Size = New System.Drawing.Size(57, 14)
        Me.Label55.TabIndex = 156
        Me.Label55.Text = "Bonus % :"
        Me.Label55.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me.txtITAmount)
        Me.Frame5.Controls.Add(Me.txtBankLoan)
        Me.Frame5.Controls.Add(Me.txtLICAmount)
        Me.Frame5.Controls.Add(Me.Label53)
        Me.Frame5.Controls.Add(Me.Label52)
        Me.Frame5.Controls.Add(Me.Label51)
        Me.Frame5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Frame5.Location = New System.Drawing.Point(372, 114)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(143, 86)
        Me.Frame5.TabIndex = 151
        Me.Frame5.TabStop = False
        Me.Frame5.Text = "Other Deduction"
        '
        'txtITAmount
        '
        Me.txtITAmount.AcceptsReturn = True
        Me.txtITAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtITAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtITAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtITAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtITAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtITAmount.Location = New System.Drawing.Point(80, 58)
        Me.txtITAmount.MaxLength = 5
        Me.txtITAmount.Name = "txtITAmount"
        Me.txtITAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtITAmount.Size = New System.Drawing.Size(59, 20)
        Me.txtITAmount.TabIndex = 72
        Me.txtITAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBankLoan
        '
        Me.txtBankLoan.AcceptsReturn = True
        Me.txtBankLoan.BackColor = System.Drawing.SystemColors.Window
        Me.txtBankLoan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBankLoan.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBankLoan.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankLoan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtBankLoan.Location = New System.Drawing.Point(80, 36)
        Me.txtBankLoan.MaxLength = 5
        Me.txtBankLoan.Name = "txtBankLoan"
        Me.txtBankLoan.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBankLoan.Size = New System.Drawing.Size(59, 20)
        Me.txtBankLoan.TabIndex = 71
        Me.txtBankLoan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtLICAmount
        '
        Me.txtLICAmount.AcceptsReturn = True
        Me.txtLICAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtLICAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLICAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLICAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLICAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtLICAmount.Location = New System.Drawing.Point(80, 14)
        Me.txtLICAmount.MaxLength = 5
        Me.txtLICAmount.Name = "txtLICAmount"
        Me.txtLICAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLICAmount.Size = New System.Drawing.Size(59, 20)
        Me.txtLICAmount.TabIndex = 70
        Me.txtLICAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.BackColor = System.Drawing.SystemColors.Control
        Me.Label53.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label53.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label53.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label53.Location = New System.Drawing.Point(10, 60)
        Me.Label53.Name = "Label53"
        Me.Label53.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label53.Size = New System.Drawing.Size(65, 14)
        Me.Label53.TabIndex = 154
        Me.Label53.Text = "I.T. Amount :"
        Me.Label53.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label52
        '
        Me.Label52.AutoSize = True
        Me.Label52.BackColor = System.Drawing.SystemColors.Control
        Me.Label52.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label52.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label52.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label52.Location = New System.Drawing.Point(11, 38)
        Me.Label52.Name = "Label52"
        Me.Label52.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label52.Size = New System.Drawing.Size(64, 14)
        Me.Label52.TabIndex = 153
        Me.Label52.Text = "Bank Loan :"
        Me.Label52.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label51
        '
        Me.Label51.AutoSize = True
        Me.Label51.BackColor = System.Drawing.SystemColors.Control
        Me.Label51.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label51.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label51.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label51.Location = New System.Drawing.Point(8, 16)
        Me.Label51.Name = "Label51"
        Me.Label51.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label51.Size = New System.Drawing.Size(67, 14)
        Me.Label51.TabIndex = 152
        Me.Label51.Text = "LIC Amount :"
        Me.Label51.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.txtLoanAcNo)
        Me.Frame2.Controls.Add(Me.txtImprestAcName)
        Me.Frame2.Controls.Add(Me.txtLoanAcName)
        Me.Frame2.Controls.Add(Me.Label60)
        Me.Frame2.Controls.Add(Me.Label50)
        Me.Frame2.Controls.Add(Me.Label49)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Frame2.Location = New System.Drawing.Point(4, 114)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(367, 86)
        Me.Frame2.TabIndex = 146
        Me.Frame2.TabStop = False
        '
        'txtLoanAcNo
        '
        Me.txtLoanAcNo.AcceptsReturn = True
        Me.txtLoanAcNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtLoanAcNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLoanAcNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLoanAcNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLoanAcNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtLoanAcNo.Location = New System.Drawing.Point(128, 58)
        Me.txtLoanAcNo.MaxLength = 6
        Me.txtLoanAcNo.Name = "txtLoanAcNo"
        Me.txtLoanAcNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLoanAcNo.Size = New System.Drawing.Size(234, 20)
        Me.txtLoanAcNo.TabIndex = 64
        '
        'txtImprestAcName
        '
        Me.txtImprestAcName.AcceptsReturn = True
        Me.txtImprestAcName.BackColor = System.Drawing.SystemColors.Window
        Me.txtImprestAcName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtImprestAcName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtImprestAcName.Enabled = False
        Me.txtImprestAcName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtImprestAcName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtImprestAcName.Location = New System.Drawing.Point(128, 36)
        Me.txtImprestAcName.MaxLength = 0
        Me.txtImprestAcName.Name = "txtImprestAcName"
        Me.txtImprestAcName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtImprestAcName.Size = New System.Drawing.Size(234, 20)
        Me.txtImprestAcName.TabIndex = 63
        '
        'txtLoanAcName
        '
        Me.txtLoanAcName.AcceptsReturn = True
        Me.txtLoanAcName.BackColor = System.Drawing.SystemColors.Window
        Me.txtLoanAcName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLoanAcName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLoanAcName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLoanAcName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtLoanAcName.Location = New System.Drawing.Point(128, 14)
        Me.txtLoanAcName.MaxLength = 6
        Me.txtLoanAcName.Name = "txtLoanAcName"
        Me.txtLoanAcName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLoanAcName.Size = New System.Drawing.Size(234, 20)
        Me.txtLoanAcName.TabIndex = 62
        '
        'Label60
        '
        Me.Label60.AutoSize = True
        Me.Label60.BackColor = System.Drawing.SystemColors.Control
        Me.Label60.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label60.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label60.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label60.Location = New System.Drawing.Point(24, 60)
        Me.Label60.Name = "Label60"
        Me.Label60.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label60.Size = New System.Drawing.Size(99, 14)
        Me.Label60.TabIndex = 168
        Me.Label60.Text = "Bank Loan A/c No :"
        Me.Label60.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label50
        '
        Me.Label50.AutoSize = True
        Me.Label50.BackColor = System.Drawing.Color.Transparent
        Me.Label50.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label50.Enabled = False
        Me.Label50.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label50.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label50.Location = New System.Drawing.Point(56, 38)
        Me.Label50.Name = "Label50"
        Me.Label50.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label50.Size = New System.Drawing.Size(67, 14)
        Me.Label50.TabIndex = 148
        Me.Label50.Text = "Imprest A/c :"
        Me.Label50.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label49
        '
        Me.Label49.AutoSize = True
        Me.Label49.BackColor = System.Drawing.SystemColors.Control
        Me.Label49.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label49.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label49.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label49.Location = New System.Drawing.Point(37, 16)
        Me.Label49.Name = "Label49"
        Me.Label49.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label49.Size = New System.Drawing.Size(86, 14)
        Me.Label49.TabIndex = 147
        Me.Label49.Text = "Bank Loan A/c  :"
        Me.Label49.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.cboPFPension)
        Me.Frame1.Controls.Add(Me.txtUIDNo)
        Me.Frame1.Controls.Add(Me.txtPFNo)
        Me.Frame1.Controls.Add(Me.Label75)
        Me.Frame1.Controls.Add(Me.Label74)
        Me.Frame1.Controls.Add(Me.lblPFNo)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Frame1.Location = New System.Drawing.Point(4, -1)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(367, 55)
        Me.Frame1.TabIndex = 136
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "PF"
        '
        'cboPFPension
        '
        Me.cboPFPension.BackColor = System.Drawing.SystemColors.Window
        Me.cboPFPension.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboPFPension.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPFPension.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPFPension.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboPFPension.Location = New System.Drawing.Point(121, 32)
        Me.cboPFPension.Name = "cboPFPension"
        Me.cboPFPension.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboPFPension.Size = New System.Drawing.Size(61, 22)
        Me.cboPFPension.TabIndex = 58
        '
        'txtUIDNo
        '
        Me.txtUIDNo.AcceptsReturn = True
        Me.txtUIDNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtUIDNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUIDNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUIDNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUIDNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtUIDNo.Location = New System.Drawing.Point(264, 12)
        Me.txtUIDNo.MaxLength = 5
        Me.txtUIDNo.Name = "txtUIDNo"
        Me.txtUIDNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUIDNo.Size = New System.Drawing.Size(99, 20)
        Me.txtUIDNo.TabIndex = 57
        '
        'txtPFNo
        '
        Me.txtPFNo.AcceptsReturn = True
        Me.txtPFNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPFNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPFNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPFNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPFNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtPFNo.Location = New System.Drawing.Point(54, 12)
        Me.txtPFNo.MaxLength = 5
        Me.txtPFNo.Name = "txtPFNo"
        Me.txtPFNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPFNo.Size = New System.Drawing.Size(153, 20)
        Me.txtPFNo.TabIndex = 56
        '
        'Label75
        '
        Me.Label75.AutoSize = True
        Me.Label75.BackColor = System.Drawing.Color.Transparent
        Me.Label75.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label75.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label75.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label75.Location = New System.Drawing.Point(2, 34)
        Me.Label75.Name = "Label75"
        Me.Label75.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label75.Size = New System.Drawing.Size(103, 14)
        Me.Label75.TabIndex = 202
        Me.Label75.Text = "Pension Applicable :"
        Me.Label75.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label74
        '
        Me.Label74.AutoSize = True
        Me.Label74.BackColor = System.Drawing.SystemColors.Control
        Me.Label74.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label74.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label74.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label74.Location = New System.Drawing.Point(209, 14)
        Me.Label74.Name = "Label74"
        Me.Label74.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label74.Size = New System.Drawing.Size(51, 14)
        Me.Label74.TabIndex = 201
        Me.Label74.Text = "UAN No :"
        Me.Label74.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblPFNo
        '
        Me.lblPFNo.AutoSize = True
        Me.lblPFNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblPFNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPFNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPFNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPFNo.Location = New System.Drawing.Point(2, 14)
        Me.lblPFNo.Name = "lblPFNo"
        Me.lblPFNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPFNo.Size = New System.Drawing.Size(50, 14)
        Me.lblPFNo.TabIndex = 137
        Me.lblPFNo.Text = "Number :"
        Me.lblPFNo.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ESI
        '
        Me.ESI.BackColor = System.Drawing.SystemColors.Control
        Me.ESI.Controls.Add(Me.cboESIApp)
        Me.ESI.Controls.Add(Me.txtESINo)
        Me.ESI.Controls.Add(Me.txtDispensary)
        Me.ESI.Controls.Add(Me.Label42)
        Me.ESI.Controls.Add(Me.lblEsiNo)
        Me.ESI.Controls.Add(Me.lblDispensary)
        Me.ESI.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ESI.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ESI.Location = New System.Drawing.Point(4, 55)
        Me.ESI.Name = "ESI"
        Me.ESI.Padding = New System.Windows.Forms.Padding(0)
        Me.ESI.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ESI.Size = New System.Drawing.Size(367, 59)
        Me.ESI.TabIndex = 133
        Me.ESI.TabStop = False
        Me.ESI.Text = "ESI"
        '
        'cboESIApp
        '
        Me.cboESIApp.BackColor = System.Drawing.SystemColors.Window
        Me.cboESIApp.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboESIApp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboESIApp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboESIApp.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboESIApp.Location = New System.Drawing.Point(108, 10)
        Me.cboESIApp.Name = "cboESIApp"
        Me.cboESIApp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboESIApp.Size = New System.Drawing.Size(61, 22)
        Me.cboESIApp.TabIndex = 59
        '
        'txtESINo
        '
        Me.txtESINo.AcceptsReturn = True
        Me.txtESINo.BackColor = System.Drawing.SystemColors.Window
        Me.txtESINo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtESINo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtESINo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtESINo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtESINo.Location = New System.Drawing.Point(228, 10)
        Me.txtESINo.MaxLength = 6
        Me.txtESINo.Name = "txtESINo"
        Me.txtESINo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtESINo.Size = New System.Drawing.Size(135, 20)
        Me.txtESINo.TabIndex = 60
        '
        'txtDispensary
        '
        Me.txtDispensary.AcceptsReturn = True
        Me.txtDispensary.BackColor = System.Drawing.SystemColors.Window
        Me.txtDispensary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDispensary.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDispensary.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDispensary.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDispensary.Location = New System.Drawing.Point(108, 34)
        Me.txtDispensary.MaxLength = 0
        Me.txtDispensary.Name = "txtDispensary"
        Me.txtDispensary.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDispensary.Size = New System.Drawing.Size(255, 20)
        Me.txtDispensary.TabIndex = 61
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.BackColor = System.Drawing.Color.Transparent
        Me.Label42.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label42.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label42.Location = New System.Drawing.Point(5, 12)
        Me.Label42.Name = "Label42"
        Me.Label42.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label42.Size = New System.Drawing.Size(87, 14)
        Me.Label42.TabIndex = 143
        Me.Label42.Text = "ESI Applicability :"
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblEsiNo
        '
        Me.lblEsiNo.AutoSize = True
        Me.lblEsiNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblEsiNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblEsiNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEsiNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblEsiNo.Location = New System.Drawing.Point(170, 12)
        Me.lblEsiNo.Name = "lblEsiNo"
        Me.lblEsiNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblEsiNo.Size = New System.Drawing.Size(50, 14)
        Me.lblEsiNo.TabIndex = 135
        Me.lblEsiNo.Text = "Number :"
        Me.lblEsiNo.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblDispensary
        '
        Me.lblDispensary.AutoSize = True
        Me.lblDispensary.BackColor = System.Drawing.Color.Transparent
        Me.lblDispensary.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDispensary.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDispensary.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDispensary.Location = New System.Drawing.Point(8, 34)
        Me.lblDispensary.Name = "lblDispensary"
        Me.lblDispensary.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDispensary.Size = New System.Drawing.Size(68, 14)
        Me.lblDispensary.TabIndex = 134
        Me.lblDispensary.Text = "Dispensary :"
        Me.lblDispensary.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.txtAdhaarNo)
        Me.Frame3.Controls.Add(Me.txtLICID)
        Me.Frame3.Controls.Add(Me.txtPanNo)
        Me.Frame3.Controls.Add(Me.Label76)
        Me.Frame3.Controls.Add(Me.Label25)
        Me.Frame3.Controls.Add(Me.Label46)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Frame3.Location = New System.Drawing.Point(372, -1)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(398, 74)
        Me.Frame3.TabIndex = 131
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "PAN / LIC / Adhaar"
        '
        'txtAdhaarNo
        '
        Me.txtAdhaarNo.AcceptsReturn = True
        Me.txtAdhaarNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtAdhaarNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAdhaarNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAdhaarNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdhaarNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtAdhaarNo.Location = New System.Drawing.Point(142, 30)
        Me.txtAdhaarNo.MaxLength = 5
        Me.txtAdhaarNo.Name = "txtAdhaarNo"
        Me.txtAdhaarNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAdhaarNo.Size = New System.Drawing.Size(249, 20)
        Me.txtAdhaarNo.TabIndex = 66
        '
        'txtLICID
        '
        Me.txtLICID.AcceptsReturn = True
        Me.txtLICID.BackColor = System.Drawing.SystemColors.Window
        Me.txtLICID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLICID.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLICID.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLICID.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtLICID.Location = New System.Drawing.Point(142, 51)
        Me.txtLICID.MaxLength = 5
        Me.txtLICID.Name = "txtLICID"
        Me.txtLICID.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLICID.Size = New System.Drawing.Size(249, 20)
        Me.txtLICID.TabIndex = 67
        '
        'txtPanNo
        '
        Me.txtPanNo.AcceptsReturn = True
        Me.txtPanNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPanNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPanNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPanNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPanNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtPanNo.Location = New System.Drawing.Point(142, 9)
        Me.txtPanNo.MaxLength = 5
        Me.txtPanNo.Name = "txtPanNo"
        Me.txtPanNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPanNo.Size = New System.Drawing.Size(249, 20)
        Me.txtPanNo.TabIndex = 65
        '
        'Label76
        '
        Me.Label76.BackColor = System.Drawing.SystemColors.Control
        Me.Label76.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label76.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label76.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label76.Location = New System.Drawing.Point(55, 32)
        Me.Label76.Name = "Label76"
        Me.Label76.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label76.Size = New System.Drawing.Size(87, 12)
        Me.Label76.TabIndex = 204
        Me.Label76.Text = "Adhaar Number :"
        Me.Label76.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.SystemColors.Control
        Me.Label25.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label25.Location = New System.Drawing.Point(60, 52)
        Me.Label25.Name = "Label25"
        Me.Label25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label25.Size = New System.Drawing.Size(82, 12)
        Me.Label25.TabIndex = 203
        Me.Label25.Text = "LIC ID Number :"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label46
        '
        Me.Label46.BackColor = System.Drawing.SystemColors.Control
        Me.Label46.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label46.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label46.Location = New System.Drawing.Point(60, 11)
        Me.Label46.Name = "Label46"
        Me.Label46.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label46.Size = New System.Drawing.Size(82, 12)
        Me.Label46.TabIndex = 132
        Me.Label46.Text = "PAN Number :"
        Me.Label46.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_SSTab1_TabPage3
        '
        Me._SSTab1_TabPage3.Controls.Add(Me.sprdSpouse)
        Me._SSTab1_TabPage3.Location = New System.Drawing.Point(4, 25)
        Me._SSTab1_TabPage3.Name = "_SSTab1_TabPage3"
        Me._SSTab1_TabPage3.Size = New System.Drawing.Size(896, 291)
        Me._SSTab1_TabPage3.TabIndex = 3
        Me._SSTab1_TabPage3.Text = "Family Details"
        '
        'sprdSpouse
        '
        Me.sprdSpouse.DataSource = Nothing
        Me.sprdSpouse.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sprdSpouse.Location = New System.Drawing.Point(0, 0)
        Me.sprdSpouse.Name = "sprdSpouse"
        Me.sprdSpouse.OcxState = CType(resources.GetObject("sprdSpouse.OcxState"), System.Windows.Forms.AxHost.State)
        Me.sprdSpouse.Size = New System.Drawing.Size(896, 291)
        Me.sprdSpouse.TabIndex = 188
        '
        '_SSTab1_TabPage4
        '
        Me._SSTab1_TabPage4.Controls.Add(Me.sprdAssets)
        Me._SSTab1_TabPage4.Location = New System.Drawing.Point(4, 25)
        Me._SSTab1_TabPage4.Name = "_SSTab1_TabPage4"
        Me._SSTab1_TabPage4.Size = New System.Drawing.Size(896, 291)
        Me._SSTab1_TabPage4.TabIndex = 4
        Me._SSTab1_TabPage4.Text = "Company Assets"
        '
        'sprdAssets
        '
        Me.sprdAssets.DataSource = Nothing
        Me.sprdAssets.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sprdAssets.Location = New System.Drawing.Point(0, 0)
        Me.sprdAssets.Name = "sprdAssets"
        Me.sprdAssets.OcxState = CType(resources.GetObject("sprdAssets.OcxState"), System.Windows.Forms.AxHost.State)
        Me.sprdAssets.Size = New System.Drawing.Size(896, 291)
        Me.sprdAssets.TabIndex = 189
        '
        '_SSTab1_TabPage5
        '
        Me._SSTab1_TabPage5.Controls.Add(Me.chkEL)
        Me._SSTab1_TabPage5.Controls.Add(Me.sprdLeaves)
        Me._SSTab1_TabPage5.Controls.Add(Me.lblLeavesYear)
        Me._SSTab1_TabPage5.Location = New System.Drawing.Point(4, 25)
        Me._SSTab1_TabPage5.Name = "_SSTab1_TabPage5"
        Me._SSTab1_TabPage5.Size = New System.Drawing.Size(896, 291)
        Me._SSTab1_TabPage5.TabIndex = 5
        Me._SSTab1_TabPage5.Text = "Opening Leave"
        '
        'chkEL
        '
        Me.chkEL.AutoSize = True
        Me.chkEL.BackColor = System.Drawing.SystemColors.Control
        Me.chkEL.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkEL.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEL.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkEL.Location = New System.Drawing.Point(572, 2)
        Me.chkEL.Name = "chkEL"
        Me.chkEL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkEL.Size = New System.Drawing.Size(138, 18)
        Me.chkEL.TabIndex = 185
        Me.chkEL.Text = "Only EL Carry Forward"
        Me.chkEL.UseVisualStyleBackColor = False
        '
        'sprdLeaves
        '
        Me.sprdLeaves.DataSource = Nothing
        Me.sprdLeaves.Location = New System.Drawing.Point(2, 22)
        Me.sprdLeaves.Name = "sprdLeaves"
        Me.sprdLeaves.OcxState = CType(resources.GetObject("sprdLeaves.OcxState"), System.Windows.Forms.AxHost.State)
        Me.sprdLeaves.Size = New System.Drawing.Size(892, 266)
        Me.sprdLeaves.TabIndex = 186
        '
        'lblLeavesYear
        '
        Me.lblLeavesYear.BackColor = System.Drawing.SystemColors.Control
        Me.lblLeavesYear.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLeavesYear.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLeavesYear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLeavesYear.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLeavesYear.Location = New System.Drawing.Point(4, 0)
        Me.lblLeavesYear.Name = "lblLeavesYear"
        Me.lblLeavesYear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLeavesYear.Size = New System.Drawing.Size(367, 19)
        Me.lblLeavesYear.TabIndex = 187
        Me.lblLeavesYear.Text = "Opening Leaves For Year :"
        '
        '_SSTab1_TabPage6
        '
        Me._SSTab1_TabPage6.Controls.Add(Me.sprdDeduct)
        Me._SSTab1_TabPage6.Controls.Add(Me.sprdEarn)
        Me._SSTab1_TabPage6.Controls.Add(Me.grdDeductions)
        Me._SSTab1_TabPage6.Controls.Add(Me.Label11)
        Me._SSTab1_TabPage6.Location = New System.Drawing.Point(4, 25)
        Me._SSTab1_TabPage6.Name = "_SSTab1_TabPage6"
        Me._SSTab1_TabPage6.Size = New System.Drawing.Size(896, 291)
        Me._SSTab1_TabPage6.TabIndex = 6
        Me._SSTab1_TabPage6.Text = "Salary"
        '
        'sprdDeduct
        '
        Me.sprdDeduct.DataSource = Nothing
        Me.sprdDeduct.Location = New System.Drawing.Point(450, 19)
        Me.sprdDeduct.Name = "sprdDeduct"
        Me.sprdDeduct.OcxState = CType(resources.GetObject("sprdDeduct.OcxState"), System.Windows.Forms.AxHost.State)
        Me.sprdDeduct.Size = New System.Drawing.Size(445, 269)
        Me.sprdDeduct.TabIndex = 181
        '
        'sprdEarn
        '
        Me.sprdEarn.DataSource = Nothing
        Me.sprdEarn.Location = New System.Drawing.Point(4, 19)
        Me.sprdEarn.Name = "sprdEarn"
        Me.sprdEarn.OcxState = CType(resources.GetObject("sprdEarn.OcxState"), System.Windows.Forms.AxHost.State)
        Me.sprdEarn.Size = New System.Drawing.Size(445, 269)
        Me.sprdEarn.TabIndex = 182
        '
        'grdDeductions
        '
        Me.grdDeductions.BackColor = System.Drawing.SystemColors.Control
        Me.grdDeductions.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdDeductions.Cursor = System.Windows.Forms.Cursors.Default
        Me.grdDeductions.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdDeductions.ForeColor = System.Drawing.SystemColors.ControlText
        Me.grdDeductions.Location = New System.Drawing.Point(449, 0)
        Me.grdDeductions.Name = "grdDeductions"
        Me.grdDeductions.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.grdDeductions.Size = New System.Drawing.Size(443, 19)
        Me.grdDeductions.TabIndex = 184
        Me.grdDeductions.Text = "Deductions"
        Me.grdDeductions.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(444, 19)
        Me.Label11.TabIndex = 183
        Me.Label11.Text = "Earnings"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        '_SSTab1_TabPage7
        '
        Me._SSTab1_TabPage7.Controls.Add(Me.Label58)
        Me._SSTab1_TabPage7.Controls.Add(Me.sprdPerks)
        Me._SSTab1_TabPage7.Location = New System.Drawing.Point(4, 25)
        Me._SSTab1_TabPage7.Name = "_SSTab1_TabPage7"
        Me._SSTab1_TabPage7.Size = New System.Drawing.Size(896, 291)
        Me._SSTab1_TabPage7.TabIndex = 7
        Me._SSTab1_TabPage7.Text = "Perks"
        '
        'Label58
        '
        Me.Label58.BackColor = System.Drawing.SystemColors.Control
        Me.Label58.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label58.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label58.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label58.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label58.Location = New System.Drawing.Point(4, 0)
        Me.Label58.Name = "Label58"
        Me.Label58.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label58.Size = New System.Drawing.Size(445, 19)
        Me.Label58.TabIndex = 180
        Me.Label58.Text = "Perks"
        Me.Label58.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'sprdPerks
        '
        Me.sprdPerks.DataSource = Nothing
        Me.sprdPerks.Location = New System.Drawing.Point(4, 19)
        Me.sprdPerks.Name = "sprdPerks"
        Me.sprdPerks.OcxState = CType(resources.GetObject("sprdPerks.OcxState"), System.Windows.Forms.AxHost.State)
        Me.sprdPerks.Size = New System.Drawing.Size(445, 269)
        Me.sprdPerks.TabIndex = 179
        '
        'Label59
        '
        Me.Label59.AutoSize = True
        Me.Label59.BackColor = System.Drawing.SystemColors.Control
        Me.Label59.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label59.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label59.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label59.Location = New System.Drawing.Point(594, 371)
        Me.Label59.Name = "Label59"
        Me.Label59.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label59.Size = New System.Drawing.Size(77, 14)
        Me.Label59.TabIndex = 167
        Me.Label59.Text = "Form 1 C.T.C. :"
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.BackColor = System.Drawing.Color.Transparent
        Me.Label48.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label48.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label48.Location = New System.Drawing.Point(438, 16)
        Me.Label48.Name = "Label48"
        Me.Label48.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label48.Size = New System.Drawing.Size(35, 14)
        Me.Label48.TabIndex = 150
        Me.Label48.Text = "WEF :"
        Me.Label48.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.BackColor = System.Drawing.SystemColors.Control
        Me.Label43.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label43.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label43.Location = New System.Drawing.Point(390, 371)
        Me.Label43.Name = "Label43"
        Me.Label43.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label43.Size = New System.Drawing.Size(99, 14)
        Me.Label43.TabIndex = 116
        Me.Label43.Text = "Form 1 Net Salary :"
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.BackColor = System.Drawing.SystemColors.Control
        Me.Label41.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label41.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label41.Location = New System.Drawing.Point(243, 371)
        Me.Label41.Name = "Label41"
        Me.Label41.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label41.Size = New System.Drawing.Size(61, 14)
        Me.Label41.TabIndex = 115
        Me.Label41.Text = "Deduction :"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label15.Location = New System.Drawing.Point(9, 372)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(113, 14)
        Me.Label15.TabIndex = 114
        Me.Label15.Text = "Form 1 Gross Salary :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label7.Location = New System.Drawing.Point(10, 17)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(110, 14)
        Me.Label7.TabIndex = 101
        Me.Label7.Text = "Form 1 Basic Salary :"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.BackColor = System.Drawing.Color.Transparent
        Me.Label30.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label30.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label30.Location = New System.Drawing.Point(-4904, 144)
        Me.Label30.Name = "Label30"
        Me.Label30.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label30.Size = New System.Drawing.Size(37, 14)
        Me.Label30.TabIndex = 111
        Me.Label30.Text = "Grade"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.Transparent
        Me.Label31.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label31.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label31.Location = New System.Drawing.Point(-4936, 96)
        Me.Label31.Name = "Label31"
        Me.Label31.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label31.Size = New System.Drawing.Size(70, 16)
        Me.Label31.TabIndex = 107
        Me.Label31.Text = "Department"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.BackColor = System.Drawing.Color.Transparent
        Me.Label32.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label32.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label32.Location = New System.Drawing.Point(-4936, 120)
        Me.Label32.Name = "Label32"
        Me.Label32.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label32.Size = New System.Drawing.Size(63, 14)
        Me.Label32.TabIndex = 110
        Me.Label32.Text = "Designation"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label33
        '
        Me.Label33.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label33.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label33.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.Color.Black
        Me.Label33.Location = New System.Drawing.Point(-4968, 188)
        Me.Label33.Name = "Label33"
        Me.Label33.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label33.Size = New System.Drawing.Size(50, 16)
        Me.Label33.TabIndex = 113
        Me.Label33.Text = "Pincode"
        '
        'Label34
        '
        Me.Label34.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label34.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label34.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.ForeColor = System.Drawing.Color.Black
        Me.Label34.Location = New System.Drawing.Point(-4968, 164)
        Me.Label34.Name = "Label34"
        Me.Label34.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label34.Size = New System.Drawing.Size(22, 16)
        Me.Label34.TabIndex = 112
        Me.Label34.Text = "City"
        '
        'Label35
        '
        Me.Label35.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label35.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label35.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.ForeColor = System.Drawing.Color.Black
        Me.Label35.Location = New System.Drawing.Point(-4968, 92)
        Me.Label35.Name = "Label35"
        Me.Label35.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label35.Size = New System.Drawing.Size(51, 16)
        Me.Label35.TabIndex = 106
        Me.Label35.Text = "Address"
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(910, 572)
        Me.SprdView.TabIndex = 161
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.Report1)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.CmdView)
        Me.FraMovement.Controls.Add(Me.CmdSave)
        Me.FraMovement.Controls.Add(Me.CmdDelete)
        Me.FraMovement.Controls.Add(Me.CmdModify)
        Me.FraMovement.Controls.Add(Me.CmdAdd)
        Me.FraMovement.Controls.Add(Me.cmdSavePrint)
        Me.FraMovement.Controls.Add(Me.cmdPreview)
        Me.FraMovement.Controls.Add(Me.lblNextIncDue)
        Me.FraMovement.Controls.Add(Me.lblEmpType)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 569)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(910, 51)
        Me.FraMovement.TabIndex = 0
        Me.FraMovement.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(8, 16)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 0
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.Location = New System.Drawing.Point(339, 10)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdSavePrint.TabIndex = 84
        Me.cmdSavePrint.Text = "SavePrint"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'cmdPreview
        '
        Me.cmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPreview.Image = CType(resources.GetObject("cmdPreview.Image"), System.Drawing.Image)
        Me.cmdPreview.Location = New System.Drawing.Point(543, 10)
        Me.cmdPreview.Name = "cmdPreview"
        Me.cmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.cmdPreview.TabIndex = 87
        Me.cmdPreview.Text = "Preview"
        Me.cmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdPreview.UseVisualStyleBackColor = False
        '
        'lblNextIncDue
        '
        Me.lblNextIncDue.BackColor = System.Drawing.SystemColors.Control
        Me.lblNextIncDue.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblNextIncDue.Enabled = False
        Me.lblNextIncDue.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNextIncDue.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblNextIncDue.Location = New System.Drawing.Point(759, 32)
        Me.lblNextIncDue.Name = "lblNextIncDue"
        Me.lblNextIncDue.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblNextIncDue.Size = New System.Drawing.Size(61, 13)
        Me.lblNextIncDue.TabIndex = 165
        Me.lblNextIncDue.Text = "lblNextIncDue"
        '
        'lblEmpType
        '
        Me.lblEmpType.BackColor = System.Drawing.SystemColors.Control
        Me.lblEmpType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblEmpType.Enabled = False
        Me.lblEmpType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmpType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblEmpType.Location = New System.Drawing.Point(757, 16)
        Me.lblEmpType.Name = "lblEmpType"
        Me.lblEmpType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblEmpType.Size = New System.Drawing.Size(45, 13)
        Me.lblEmpType.TabIndex = 160
        Me.lblEmpType.Text = "lblEmpType"
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.BackColor = System.Drawing.SystemColors.Menu
        Me.Label44.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label44.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label44.Location = New System.Drawing.Point(222, 42)
        Me.Label44.Name = "Label44"
        Me.Label44.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label44.Size = New System.Drawing.Size(32, 14)
        Me.Label44.TabIndex = 99
        Me.Label44.Text = "Sex :"
        '
        'frmEmployee
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(910, 621)
        Me.Controls.Add(Me.fraTop)
        Me.Controls.Add(Me.FraBottom)
        Me.Controls.Add(Me.SprdView)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.Label44)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 23)
        Me.MaximizeBox = False
        Me.Name = "frmEmployee"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Employee Master"
        Me.fraTop.ResumeLayout(False)
        Me.fraTop.PerformLayout()
        Me.PbPhoto.ResumeLayout(False)
        CType(Me.ImagePhoto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraBottom.ResumeLayout(False)
        Me.FraBottom.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.SSTab1.ResumeLayout(False)
        Me._SSTab1_TabPage0.ResumeLayout(False)
        Me.fraModePayment.ResumeLayout(False)
        Me.fraModePayment.PerformLayout()
        CType(Me.cboMachineName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame8.ResumeLayout(False)
        Me.Frame8.PerformLayout()
        Me._SSTab1_TabPage1.ResumeLayout(False)
        Me._SSTab1_TabPage1.PerformLayout()
        Me.Frame9.ResumeLayout(False)
        Me.Frame9.PerformLayout()
        Me.fraPermAdd.ResumeLayout(False)
        Me.fraPermAdd.PerformLayout()
        Me._SSTab1_TabPage2.ResumeLayout(False)
        Me._SSTab1_TabPage2.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me.Frame7.ResumeLayout(False)
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me.Frame5.ResumeLayout(False)
        Me.Frame5.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.ESI.ResumeLayout(False)
        Me.ESI.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me._SSTab1_TabPage3.ResumeLayout(False)
        CType(Me.sprdSpouse, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage4.ResumeLayout(False)
        CType(Me.sprdAssets, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage5.ResumeLayout(False)
        Me._SSTab1_TabPage5.PerformLayout()
        CType(Me.sprdLeaves, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage6.ResumeLayout(False)
        CType(Me.sprdDeduct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sprdEarn, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage7.ResumeLayout(False)
        CType(Me.sprdPerks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        ''SprdView.DataSource = CType(ADataGrid, MSDATASRC.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        SprdView.DataSource = Nothing
    End Sub

    Public WithEvents Label81 As Label
    Public WithEvents txtForm1BSalary As TextBox
    Public WithEvents txtForm1GSalary As TextBox
    Public WithEvents Label82 As Label
    Public WithEvents txtForm1CTC As TextBox
    Public WithEvents txtForm1NetSalary As TextBox
    Public WithEvents Label83 As Label
    Public WithEvents Label84 As Label
    Public WithEvents Label85 As Label
    Public WithEvents txtWorkingHours As TextBox
    Public WithEvents GroupBox1 As GroupBox
    Public WithEvents optContBasic As RadioButton
    Public WithEvents optContCeiling As RadioButton
    Public WithEvents Label86 As Label
    Public WithEvents txtAddEmpCode As TextBox
    Public WithEvents chkWFH As CheckBox
    Public WithEvents Label87 As Label
    Friend WithEvents cboMachineName As Infragistics.Win.UltraWinGrid.UltraCombo
    Public WithEvents optContGross As RadioButton
    Public WithEvents GroupBox2 As GroupBox
    Public WithEvents Label88 As Label
    Public WithEvents txtDAAmount As TextBox
    Public WithEvents Label89 As Label
    Public WithEvents cboTaxRegime As ComboBox
    Public WithEvents optContCeilingGross As RadioButton
    Public WithEvents txtBonusDOJ As TextBox
    Public WithEvents Label90 As Label
    Public WithEvents chkDeptHOD As CheckBox
    Public WithEvents chkHRHOD As CheckBox
#End Region
End Class