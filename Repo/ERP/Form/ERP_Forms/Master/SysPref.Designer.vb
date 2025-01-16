Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmSysPref
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
    End Sub
    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
        If Disposing Then
            If Not components Is Nothing Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(Disposing)
    End Sub
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Public ToolTip1 As System.Windows.Forms.ToolTip
    Public WithEvents chkAttMc As System.Windows.Forms.CheckBox
    Public WithEvents Frame10 As System.Windows.Forms.GroupBox
    Public WithEvents chkMaintMaxLevel As System.Windows.Forms.CheckBox
    Public WithEvents chkConsMaxLevel As System.Windows.Forms.CheckBox
    Public WithEvents chkRMMaxLevel As System.Windows.Forms.CheckBox
    Public WithEvents chkBOPMaxLevel As System.Windows.Forms.CheckBox
    Public WithEvents Frame21 As System.Windows.Forms.GroupBox
    Public WithEvents chkPurchase As System.Windows.Forms.CheckBox
    Public WithEvents chkMRR As System.Windows.Forms.CheckBox
    Public WithEvents chkDespatch As System.Windows.Forms.CheckBox
    Public WithEvents chkGatepass As System.Windows.Forms.CheckBox
    Public WithEvents chkInvoice As System.Windows.Forms.CheckBox
    Public WithEvents Frame15 As System.Windows.Forms.GroupBox
    Public WithEvents chkPurPlanning As System.Windows.Forms.CheckBox
    Public WithEvents Frame18 As System.Windows.Forms.GroupBox
    Public WithEvents chkPOCheckInGE As System.Windows.Forms.CheckBox
    Public WithEvents _Label12_6 As System.Windows.Forms.Label
    Public WithEvents Frame17 As System.Windows.Forms.GroupBox
    Public WithEvents chkOnLine As System.Windows.Forms.CheckBox
    Public WithEvents Frame9 As System.Windows.Forms.GroupBox
    Public WithEvents chkInvTableCC As System.Windows.Forms.CheckBox
    Public WithEvents chkInvTableFYear As System.Windows.Forms.CheckBox
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents CmdDefaultMargin As System.Windows.Forms.Button
    Public WithEvents _txtMargin_3 As System.Windows.Forms.TextBox
    Public WithEvents _txtMargin_2 As System.Windows.Forms.TextBox
    Public WithEvents _txtMargin_1 As System.Windows.Forms.TextBox
    Public WithEvents _txtMargin_0 As System.Windows.Forms.TextBox
    Public WithEvents _Label12_3 As System.Windows.Forms.Label
    Public WithEvents _Label12_2 As System.Windows.Forms.Label
    Public WithEvents _Label12_1 As System.Windows.Forms.Label
    Public WithEvents _Label12_0 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents fraMargin As System.Windows.Forms.GroupBox
    Public WithEvents chkPrintTopCompanyAddress As System.Windows.Forms.CheckBox
    Public WithEvents chkPrintTopCompanyPhone As System.Windows.Forms.CheckBox
    Public WithEvents _OptPrintCompanyFull_ShortName_0 As System.Windows.Forms.RadioButton
    Public WithEvents _OptPrintCompanyFull_ShortName_1 As System.Windows.Forms.RadioButton
    Public WithEvents Frame12 As System.Windows.Forms.GroupBox
    Public WithEvents ChkPrintTopCompanyName As System.Windows.Forms.CheckBox
    Public WithEvents Frame11 As System.Windows.Forms.GroupBox
    Public WithEvents ChkPrintBotCompanyName As System.Windows.Forms.CheckBox
    Public WithEvents chkPrintBotCompanyAddress As System.Windows.Forms.CheckBox
    Public WithEvents chkPrintBotCompanyPhone As System.Windows.Forms.CheckBox
    Public WithEvents Frame7 As System.Windows.Forms.GroupBox
    Public WithEvents ChkPrintPAgeNo As System.Windows.Forms.CheckBox
    Public WithEvents ChkPrintRunDate As System.Windows.Forms.CheckBox
    Public WithEvents chkPrintUser As System.Windows.Forms.CheckBox
    Public WithEvents Frame5 As System.Windows.Forms.GroupBox
    Public WithEvents chkGateEntry As System.Windows.Forms.CheckBox
    Public WithEvents _Label12_5 As System.Windows.Forms.Label
    Public WithEvents Frame16 As System.Windows.Forms.GroupBox
    Public WithEvents chkWeeklySchd As System.Windows.Forms.CheckBox
    Public WithEvents _Label12_7 As System.Windows.Forms.Label
    Public WithEvents Frame19 As System.Windows.Forms.GroupBox
    Public WithEvents _FraBorder_8 As System.Windows.Forms.GroupBox
    Public WithEvents _SSTab1_TabPage0 As System.Windows.Forms.TabPage
    Public WithEvents txtAuthorizedFName As System.Windows.Forms.TextBox
    Public WithEvents txtESI As System.Windows.Forms.TextBox
    Public WithEvents txtSTDS As System.Windows.Forms.TextBox
    Public WithEvents txtTDSCreditAcct As System.Windows.Forms.TextBox
    Public WithEvents txtPANNo As System.Windows.Forms.TextBox
    Public WithEvents txtTDSAcNo As System.Windows.Forms.TextBox
    Public WithEvents txtTDSCircle As System.Windows.Forms.TextBox
    Public WithEvents txtAuthorized As System.Windows.Forms.TextBox
    Public WithEvents txtDesignation As System.Windows.Forms.TextBox
    Public WithEvents Label33 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label53 As System.Windows.Forms.Label
    Public WithEvents Label52 As System.Windows.Forms.Label
    Public WithEvents Label51 As System.Windows.Forms.Label
    Public WithEvents Label54 As System.Windows.Forms.Label
    Public WithEvents Label55 As System.Windows.Forms.Label
    Public WithEvents _FraBorder_5 As System.Windows.Forms.GroupBox
    Public WithEvents _SSTab1_TabPage1 As System.Windows.Forms.TabPage
    Public WithEvents _SSTab1_TabPage3 As System.Windows.Forms.TabPage
    Public WithEvents txtMaxBillAmount As System.Windows.Forms.TextBox
    Public WithEvents Frame32 As System.Windows.Forms.GroupBox
    Public WithEvents chkRJDespatchNote As System.Windows.Forms.CheckBox
    Public WithEvents Frame24 As System.Windows.Forms.GroupBox
    Public WithEvents chkAutoProdIssue As System.Windows.Forms.CheckBox
    Public WithEvents chkAutoIssue As System.Windows.Forms.CheckBox
    Public WithEvents _optInvPostingType_0 As System.Windows.Forms.RadioButton
    Public WithEvents _optInvPostingType_1 As System.Windows.Forms.RadioButton
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents _optPurPostingType_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optPurPostingType_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents chkRateDiffCN_App As System.Windows.Forms.CheckBox
    Public WithEvents chkRateDiffDN_App As System.Windows.Forms.CheckBox
    Public WithEvents chkRejection_App As System.Windows.Forms.CheckBox
    Public WithEvents chkShortage_App As System.Windows.Forms.CheckBox
    Public WithEvents chkRateDiffCN As System.Windows.Forms.CheckBox
    Public WithEvents chkRateDiffDN As System.Windows.Forms.CheckBox
    Public WithEvents chkRejection As System.Windows.Forms.CheckBox
    Public WithEvents chkShortage As System.Windows.Forms.CheckBox
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents txtMRRExcessPer As System.Windows.Forms.TextBox
    Public WithEvents chkStockBal As System.Windows.Forms.CheckBox
    Public WithEvents txtPDIRCrAccount As System.Windows.Forms.TextBox
    Public WithEvents txtPDIRAccount As System.Windows.Forms.TextBox
    Public WithEvents txtPDIRAmount As System.Windows.Forms.TextBox
    Public WithEvents txtAutoIssueDate As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtAutoProdIssueDate As System.Windows.Forms.MaskedTextBox
    Public WithEvents Label56 As System.Windows.Forms.Label
    Public WithEvents Label49 As System.Windows.Forms.Label
    Public WithEvents Label20 As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents _FraBorder_2 As System.Windows.Forms.GroupBox
    Public WithEvents _SSTab1_TabPage4 As System.Windows.Forms.TabPage
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents SprdCategory As AxFPSpreadADO.AxfpSpread
    Public WithEvents _FraBorder_6 As System.Windows.Forms.GroupBox
    Public WithEvents _SSTab1_TabPage6 As System.Windows.Forms.TabPage
    Public WithEvents txtPendingIndentNo As System.Windows.Forms.TextBox
    Public WithEvents Frame34 As System.Windows.Forms.GroupBox
    Public WithEvents txtMaxPOItems As System.Windows.Forms.TextBox
    Public WithEvents Frame33 As System.Windows.Forms.GroupBox
    Public WithEvents _optSOLocking_0 As System.Windows.Forms.RadioButton
    Public WithEvents _optSOLocking_1 As System.Windows.Forms.RadioButton
    Public WithEvents Frame31 As System.Windows.Forms.GroupBox
    Public WithEvents _optPOLocking_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optPOLocking_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame30 As System.Windows.Forms.GroupBox
    Public WithEvents txtQCDays As System.Windows.Forms.TextBox
    Public WithEvents chkMaxInvInGate As System.Windows.Forms.CheckBox
    Public WithEvents _optInvGen_0 As System.Windows.Forms.RadioButton
    Public WithEvents _optInvGen_1 As System.Windows.Forms.RadioButton
    Public WithEvents txtInvTmFrom As System.Windows.Forms.TextBox
    Public WithEvents txtInvTmTo As System.Windows.Forms.TextBox
    Public WithEvents Label68 As System.Windows.Forms.Label
    Public WithEvents Label71 As System.Windows.Forms.Label
    Public WithEvents Frame25 As System.Windows.Forms.GroupBox
    Public WithEvents _optLoadingSlip_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optLoadingSlip_0 As System.Windows.Forms.RadioButton
    Public WithEvents txtLoadingAppDate As System.Windows.Forms.MaskedTextBox
    Public WithEvents Label72 As System.Windows.Forms.Label
    Public WithEvents Frame26 As System.Windows.Forms.GroupBox
    Public WithEvents _optProductionPlanLocking_0 As System.Windows.Forms.RadioButton
    Public WithEvents _optProductionPlanLocking_1 As System.Windows.Forms.RadioButton
    Public WithEvents txtPlanLockDays As System.Windows.Forms.TextBox
    Public WithEvents Label73 As System.Windows.Forms.Label
    Public WithEvents Frame27 As System.Windows.Forms.GroupBox
    Public WithEvents chkPOPrintApproval As System.Windows.Forms.CheckBox
    Public WithEvents _optPOPrint_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optPOPrint_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame28 As System.Windows.Forms.GroupBox
    Public WithEvents chkBOPCheck As System.Windows.Forms.CheckBox
    Public WithEvents chkFGCheck As System.Windows.Forms.CheckBox
    Public WithEvents Frame29 As System.Windows.Forms.GroupBox
    Public WithEvents _Label12_4 As System.Windows.Forms.Label
    Public WithEvents Label66 As System.Windows.Forms.Label
    Public WithEvents Label67 As System.Windows.Forms.Label
    Public WithEvents _FraBorder_9 As System.Windows.Forms.GroupBox
    Public WithEvents _SSTab1_TabPage7 As System.Windows.Forms.TabPage
    Public WithEvents SSTab1 As System.Windows.Forms.TabControl
    Public WithEvents Frame13 As System.Windows.Forms.GroupBox
    Public WithEvents cmdSave As System.Windows.Forms.Button
    Public WithEvents cmdcancel As System.Windows.Forms.Button
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
    Public WithEvents Frame8 As System.Windows.Forms.GroupBox
    Public WithEvents FraBorder As VB6.GroupBoxArray
    Public WithEvents Label12 As VB6.LabelArray
    Public WithEvents OptPrintCompanyFull_ShortName As VB6.RadioButtonArray
    Public WithEvents optInvGen As VB6.RadioButtonArray
    Public WithEvents optInvPostingType As VB6.RadioButtonArray
    Public WithEvents optLoadingSlip As VB6.RadioButtonArray
    Public WithEvents optPOLocking As VB6.RadioButtonArray
    Public WithEvents optPOPrint As VB6.RadioButtonArray
    Public WithEvents optProductionPlanLocking As VB6.RadioButtonArray
    Public WithEvents optPurPostingType As VB6.RadioButtonArray
    Public WithEvents optSOLocking As VB6.RadioButtonArray
    Public WithEvents txtMargin As VB6.TextBoxArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSysPref))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdcancel = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.Frame13 = New System.Windows.Forms.GroupBox()
        Me.SSTab1 = New System.Windows.Forms.TabControl()
        Me._SSTab1_TabPage0 = New System.Windows.Forms.TabPage()
        Me._FraBorder_8 = New System.Windows.Forms.GroupBox()
        Me.GroupBox31 = New System.Windows.Forms.GroupBox()
        Me.ChkOuterPackCol = New System.Windows.Forms.CheckBox()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.GroupBox30 = New System.Windows.Forms.GroupBox()
        Me.ChkBom = New System.Windows.Forms.CheckBox()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.GroupBox29 = New System.Windows.Forms.GroupBox()
        Me.ChkSaleScheduleReq = New System.Windows.Forms.CheckBox()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.GroupBox28 = New System.Windows.Forms.GroupBox()
        Me.ChkQtyCheck = New System.Windows.Forms.CheckBox()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.GroupBox27 = New System.Windows.Forms.GroupBox()
        Me.ChkINVFromStock = New System.Windows.Forms.CheckBox()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.GroupBox26 = New System.Windows.Forms.GroupBox()
        Me.ChkMRPSaleOrder = New System.Windows.Forms.CheckBox()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.GroupBox25 = New System.Windows.Forms.GroupBox()
        Me.ChkLockPayterms = New System.Windows.Forms.CheckBox()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.GroupBox24 = New System.Windows.Forms.GroupBox()
        Me.ChkSaleOrderIndent = New System.Windows.Forms.CheckBox()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.GroupBox23 = New System.Windows.Forms.GroupBox()
        Me.ChkDoubleSalary = New System.Windows.Forms.CheckBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.GroupBox22 = New System.Windows.Forms.GroupBox()
        Me.ChkElFixed = New System.Windows.Forms.CheckBox()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.GroupBox21 = New System.Windows.Forms.GroupBox()
        Me.ChkAfterConfirm = New System.Windows.Forms.CheckBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.GroupBox20 = New System.Windows.Forms.GroupBox()
        Me.ChkWarHouse = New System.Windows.Forms.CheckBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.GroupBox19 = New System.Windows.Forms.GroupBox()
        Me.ChkAutoGenCode = New System.Windows.Forms.CheckBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.GroupBox18 = New System.Windows.Forms.GroupBox()
        Me.ChkDCInLedger = New System.Windows.Forms.CheckBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.GroupBox17 = New System.Windows.Forms.GroupBox()
        Me.ChkBilladj = New System.Windows.Forms.CheckBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.ChkDivlocation = New System.Windows.Forms.CheckBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.chkAcPRAutoJv = New System.Windows.Forms.CheckBox()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.ChkAWavailable = New System.Windows.Forms.CheckBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.GroupBox16 = New System.Windows.Forms.GroupBox()
        Me.ChkInnerPackCol = New System.Windows.Forms.CheckBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.ChkPrintOTInPayslip = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtCompAc = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox14 = New System.Windows.Forms.GroupBox()
        Me.ChkTCS = New System.Windows.Forms.CheckBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Frame10 = New System.Windows.Forms.GroupBox()
        Me.chkAttMc = New System.Windows.Forms.CheckBox()
        Me.GroupBox13 = New System.Windows.Forms.GroupBox()
        Me.ChkPacketCol = New System.Windows.Forms.CheckBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Frame21 = New System.Windows.Forms.GroupBox()
        Me.chkMaintMaxLevel = New System.Windows.Forms.CheckBox()
        Me.chkConsMaxLevel = New System.Windows.Forms.CheckBox()
        Me.chkRMMaxLevel = New System.Windows.Forms.CheckBox()
        Me.chkBOPMaxLevel = New System.Windows.Forms.CheckBox()
        Me.Frame15 = New System.Windows.Forms.GroupBox()
        Me.chkPurchase = New System.Windows.Forms.CheckBox()
        Me.chkMRR = New System.Windows.Forms.CheckBox()
        Me.chkDespatch = New System.Windows.Forms.CheckBox()
        Me.chkGatepass = New System.Windows.Forms.CheckBox()
        Me.chkInvoice = New System.Windows.Forms.CheckBox()
        Me.GroupBox11 = New System.Windows.Forms.GroupBox()
        Me.ChkEInvoiceApp = New System.Windows.Forms.CheckBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Frame18 = New System.Windows.Forms.GroupBox()
        Me.chkPurPlanning = New System.Windows.Forms.CheckBox()
        Me.Frame17 = New System.Windows.Forms.GroupBox()
        Me.chkCheckPORate = New System.Windows.Forms.CheckBox()
        Me.chkPOCheckInGE = New System.Windows.Forms.CheckBox()
        Me._Label12_6 = New System.Windows.Forms.Label()
        Me.Frame9 = New System.Windows.Forms.GroupBox()
        Me.chkOnLine = New System.Windows.Forms.CheckBox()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.ChkGSTSeparate = New System.Windows.Forms.CheckBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.chkInvTableCC = New System.Windows.Forms.CheckBox()
        Me.chkInvTableFYear = New System.Windows.Forms.CheckBox()
        Me.fraMargin = New System.Windows.Forms.GroupBox()
        Me.CmdDefaultMargin = New System.Windows.Forms.Button()
        Me._txtMargin_3 = New System.Windows.Forms.TextBox()
        Me._txtMargin_2 = New System.Windows.Forms.TextBox()
        Me._txtMargin_1 = New System.Windows.Forms.TextBox()
        Me._txtMargin_0 = New System.Windows.Forms.TextBox()
        Me._Label12_3 = New System.Windows.Forms.Label()
        Me._Label12_2 = New System.Windows.Forms.Label()
        Me._Label12_1 = New System.Windows.Forms.Label()
        Me._Label12_0 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Frame11 = New System.Windows.Forms.GroupBox()
        Me.chkPrintTopCompanyAddress = New System.Windows.Forms.CheckBox()
        Me.chkPrintTopCompanyPhone = New System.Windows.Forms.CheckBox()
        Me.Frame12 = New System.Windows.Forms.GroupBox()
        Me._OptPrintCompanyFull_ShortName_0 = New System.Windows.Forms.RadioButton()
        Me._OptPrintCompanyFull_ShortName_1 = New System.Windows.Forms.RadioButton()
        Me.ChkPrintTopCompanyName = New System.Windows.Forms.CheckBox()
        Me.Frame7 = New System.Windows.Forms.GroupBox()
        Me.ChkPrintBotCompanyName = New System.Windows.Forms.CheckBox()
        Me.chkPrintBotCompanyAddress = New System.Windows.Forms.CheckBox()
        Me.chkPrintBotCompanyPhone = New System.Windows.Forms.CheckBox()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me.ChkPrintPAgeNo = New System.Windows.Forms.CheckBox()
        Me.ChkPrintRunDate = New System.Windows.Forms.CheckBox()
        Me.chkPrintUser = New System.Windows.Forms.CheckBox()
        Me.Frame16 = New System.Windows.Forms.GroupBox()
        Me.chkGateEntry = New System.Windows.Forms.CheckBox()
        Me._Label12_5 = New System.Windows.Forms.Label()
        Me.Frame19 = New System.Windows.Forms.GroupBox()
        Me.chkWeeklySchd = New System.Windows.Forms.CheckBox()
        Me._Label12_7 = New System.Windows.Forms.Label()
        Me._SSTab1_TabPage1 = New System.Windows.Forms.TabPage()
        Me._FraBorder_5 = New System.Windows.Forms.GroupBox()
        Me.txtAuthorizedFName = New System.Windows.Forms.TextBox()
        Me.txtESI = New System.Windows.Forms.TextBox()
        Me.txtSTDS = New System.Windows.Forms.TextBox()
        Me.txtTDSCreditAcct = New System.Windows.Forms.TextBox()
        Me.txtPANNo = New System.Windows.Forms.TextBox()
        Me.txtTDSAcNo = New System.Windows.Forms.TextBox()
        Me.txtTDSCircle = New System.Windows.Forms.TextBox()
        Me.txtAuthorized = New System.Windows.Forms.TextBox()
        Me.txtDesignation = New System.Windows.Forms.TextBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.Label55 = New System.Windows.Forms.Label()
        Me._SSTab1_TabPage3 = New System.Windows.Forms.TabPage()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.txtCreditBank = New System.Windows.Forms.TextBox()
        Me.txtFurtherBank = New System.Windows.Forms.TextBox()
        Me.txtADCode = New System.Windows.Forms.TextBox()
        Me.txtCreditBankAddress = New System.Windows.Forms.TextBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me._SSTab1_TabPage4 = New System.Windows.Forms.TabPage()
        Me._FraBorder_2 = New System.Windows.Forms.GroupBox()
        Me.txtInvoiceDigit = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.optA4 = New System.Windows.Forms.RadioButton()
        Me.optA3 = New System.Windows.Forms.RadioButton()
        Me.optPrintLandScape = New System.Windows.Forms.RadioButton()
        Me.optPrintPortrait = New System.Windows.Forms.RadioButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.optYearlySeq = New System.Windows.Forms.RadioButton()
        Me.optMonthlySeq = New System.Windows.Forms.RadioButton()
        Me.optDailySeq = New System.Windows.Forms.RadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkHideHeatNo = New System.Windows.Forms.CheckBox()
        Me.chkHideBatchNo = New System.Windows.Forms.CheckBox()
        Me.Frame32 = New System.Windows.Forms.GroupBox()
        Me.txtMaxBillAmount = New System.Windows.Forms.TextBox()
        Me.Frame24 = New System.Windows.Forms.GroupBox()
        Me.chkRJDespatchNote = New System.Windows.Forms.CheckBox()
        Me.chkAutoProdIssue = New System.Windows.Forms.CheckBox()
        Me.chkAutoIssue = New System.Windows.Forms.CheckBox()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me._optInvPostingType_0 = New System.Windows.Forms.RadioButton()
        Me._optInvPostingType_1 = New System.Windows.Forms.RadioButton()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me._optPurPostingType_1 = New System.Windows.Forms.RadioButton()
        Me._optPurPostingType_0 = New System.Windows.Forms.RadioButton()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.chkRateDiffCrWithGST = New System.Windows.Forms.CheckBox()
        Me.chkRateDiffDrWithGST = New System.Windows.Forms.CheckBox()
        Me.chkRejectionWithGST = New System.Windows.Forms.CheckBox()
        Me.chkShortageWithGST = New System.Windows.Forms.CheckBox()
        Me.chkRateDiffCN_App = New System.Windows.Forms.CheckBox()
        Me.chkRateDiffDN_App = New System.Windows.Forms.CheckBox()
        Me.chkRejection_App = New System.Windows.Forms.CheckBox()
        Me.chkShortage_App = New System.Windows.Forms.CheckBox()
        Me.chkRateDiffCN = New System.Windows.Forms.CheckBox()
        Me.chkRateDiffDN = New System.Windows.Forms.CheckBox()
        Me.chkRejection = New System.Windows.Forms.CheckBox()
        Me.chkShortage = New System.Windows.Forms.CheckBox()
        Me.txtMRRExcessPer = New System.Windows.Forms.TextBox()
        Me.chkStockBal = New System.Windows.Forms.CheckBox()
        Me.txtPDIRCrAccount = New System.Windows.Forms.TextBox()
        Me.txtPDIRAccount = New System.Windows.Forms.TextBox()
        Me.txtPDIRAmount = New System.Windows.Forms.TextBox()
        Me.txtAutoIssueDate = New System.Windows.Forms.MaskedTextBox()
        Me.txtAutoProdIssueDate = New System.Windows.Forms.MaskedTextBox()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me._SSTab1_TabPage6 = New System.Windows.Forms.TabPage()
        Me._FraBorder_6 = New System.Windows.Forms.GroupBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me._SSTab1_TabPage7 = New System.Windows.Forms.TabPage()
        Me._FraBorder_9 = New System.Windows.Forms.GroupBox()
        Me.chkCreditLimit = New System.Windows.Forms.CheckBox()
        Me.Frame34 = New System.Windows.Forms.GroupBox()
        Me.txtPendingIndentNo = New System.Windows.Forms.TextBox()
        Me.Frame33 = New System.Windows.Forms.GroupBox()
        Me.txtMaxPOItems = New System.Windows.Forms.TextBox()
        Me.Frame31 = New System.Windows.Forms.GroupBox()
        Me._optSOLocking_0 = New System.Windows.Forms.RadioButton()
        Me._optSOLocking_1 = New System.Windows.Forms.RadioButton()
        Me.Frame30 = New System.Windows.Forms.GroupBox()
        Me._optPOLocking_1 = New System.Windows.Forms.RadioButton()
        Me._optPOLocking_0 = New System.Windows.Forms.RadioButton()
        Me.txtQCDays = New System.Windows.Forms.TextBox()
        Me.chkMaxInvInGate = New System.Windows.Forms.CheckBox()
        Me.Frame25 = New System.Windows.Forms.GroupBox()
        Me._optInvGen_0 = New System.Windows.Forms.RadioButton()
        Me._optInvGen_1 = New System.Windows.Forms.RadioButton()
        Me.txtInvTmFrom = New System.Windows.Forms.TextBox()
        Me.txtInvTmTo = New System.Windows.Forms.TextBox()
        Me.Label68 = New System.Windows.Forms.Label()
        Me.Label71 = New System.Windows.Forms.Label()
        Me.Frame26 = New System.Windows.Forms.GroupBox()
        Me._optLoadingSlip_1 = New System.Windows.Forms.RadioButton()
        Me._optLoadingSlip_0 = New System.Windows.Forms.RadioButton()
        Me.txtLoadingAppDate = New System.Windows.Forms.MaskedTextBox()
        Me.Label72 = New System.Windows.Forms.Label()
        Me.Frame27 = New System.Windows.Forms.GroupBox()
        Me._optProductionPlanLocking_0 = New System.Windows.Forms.RadioButton()
        Me._optProductionPlanLocking_1 = New System.Windows.Forms.RadioButton()
        Me.txtPlanLockDays = New System.Windows.Forms.TextBox()
        Me.Label73 = New System.Windows.Forms.Label()
        Me.Frame28 = New System.Windows.Forms.GroupBox()
        Me.chkPOPrintApproval = New System.Windows.Forms.CheckBox()
        Me._optPOPrint_1 = New System.Windows.Forms.RadioButton()
        Me._optPOPrint_0 = New System.Windows.Forms.RadioButton()
        Me.Frame29 = New System.Windows.Forms.GroupBox()
        Me.chkBOPCheck = New System.Windows.Forms.CheckBox()
        Me.chkFGCheck = New System.Windows.Forms.CheckBox()
        Me._Label12_4 = New System.Windows.Forms.Label()
        Me.Label66 = New System.Windows.Forms.Label()
        Me.Label67 = New System.Windows.Forms.Label()
        Me._SSTab1_TabPage8 = New System.Windows.Forms.TabPage()
        Me.fraCategory = New System.Windows.Forms.GroupBox()
        Me.SprdCategory = New AxFPSpreadADO.AxfpSpread()
        Me.Frame8 = New System.Windows.Forms.GroupBox()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.FraBorder = New Microsoft.VisualBasic.Compatibility.VB6.GroupBoxArray(Me.components)
        Me.Label12 = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.OptPrintCompanyFull_ShortName = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optInvGen = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optInvPostingType = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optLoadingSlip = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optPOLocking = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optPOPrint = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optProductionPlanLocking = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optPurPostingType = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optSOLocking = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.txtMargin = New Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray(Me.components)
        Me.optCBJSeq = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.Frame13.SuspendLayout()
        Me.SSTab1.SuspendLayout()
        Me._SSTab1_TabPage0.SuspendLayout()
        Me._FraBorder_8.SuspendLayout()
        Me.GroupBox31.SuspendLayout()
        Me.GroupBox30.SuspendLayout()
        Me.GroupBox29.SuspendLayout()
        Me.GroupBox28.SuspendLayout()
        Me.GroupBox27.SuspendLayout()
        Me.GroupBox26.SuspendLayout()
        Me.GroupBox25.SuspendLayout()
        Me.GroupBox24.SuspendLayout()
        Me.GroupBox23.SuspendLayout()
        Me.GroupBox22.SuspendLayout()
        Me.GroupBox21.SuspendLayout()
        Me.GroupBox20.SuspendLayout()
        Me.GroupBox19.SuspendLayout()
        Me.GroupBox18.SuspendLayout()
        Me.GroupBox17.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.GroupBox16.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox14.SuspendLayout()
        Me.Frame10.SuspendLayout()
        Me.GroupBox13.SuspendLayout()
        Me.Frame21.SuspendLayout()
        Me.Frame15.SuspendLayout()
        Me.GroupBox11.SuspendLayout()
        Me.Frame18.SuspendLayout()
        Me.Frame17.SuspendLayout()
        Me.Frame9.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me.fraMargin.SuspendLayout()
        Me.Frame11.SuspendLayout()
        Me.Frame12.SuspendLayout()
        Me.Frame7.SuspendLayout()
        Me.Frame5.SuspendLayout()
        Me.Frame16.SuspendLayout()
        Me.Frame19.SuspendLayout()
        Me._SSTab1_TabPage1.SuspendLayout()
        Me._FraBorder_5.SuspendLayout()
        Me._SSTab1_TabPage3.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me._SSTab1_TabPage4.SuspendLayout()
        Me._FraBorder_2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.Frame32.SuspendLayout()
        Me.Frame24.SuspendLayout()
        Me.Frame4.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me._SSTab1_TabPage6.SuspendLayout()
        Me._FraBorder_6.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage7.SuspendLayout()
        Me._FraBorder_9.SuspendLayout()
        Me.Frame34.SuspendLayout()
        Me.Frame33.SuspendLayout()
        Me.Frame31.SuspendLayout()
        Me.Frame30.SuspendLayout()
        Me.Frame25.SuspendLayout()
        Me.Frame26.SuspendLayout()
        Me.Frame27.SuspendLayout()
        Me.Frame28.SuspendLayout()
        Me.Frame29.SuspendLayout()
        Me._SSTab1_TabPage8.SuspendLayout()
        Me.fraCategory.SuspendLayout()
        CType(Me.SprdCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame8.SuspendLayout()
        CType(Me.FraBorder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptPrintCompanyFull_ShortName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optInvGen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optInvPostingType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optLoadingSlip, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optPOLocking, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optPOPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optProductionPlanLocking, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optPurPostingType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optSOLocking, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMargin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optCBJSeq, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdcancel
        '
        Me.cmdcancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdcancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdcancel.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdcancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdcancel.Image = CType(resources.GetObject("cmdcancel.Image"), System.Drawing.Image)
        Me.cmdcancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdcancel.Location = New System.Drawing.Point(748, 10)
        Me.cmdcancel.Name = "cmdcancel"
        Me.cmdcancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdcancel.Size = New System.Drawing.Size(69, 34)
        Me.cmdcancel.TabIndex = 0
        Me.cmdcancel.Text = "&Close"
        Me.cmdcancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.cmdcancel, "Cancel & Close Setup")
        Me.cmdcancel.UseVisualStyleBackColor = False
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Location = New System.Drawing.Point(4, 10)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(38, 25)
        Me.cmdSavePrint.TabIndex = 3
        Me.cmdSavePrint.Text = "Save&&Print"
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save and Print the Current Bill")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        Me.cmdSavePrint.Visible = False
        '
        'Frame13
        '
        Me.Frame13.BackColor = System.Drawing.SystemColors.Control
        Me.Frame13.Controls.Add(Me.SSTab1)
        Me.Frame13.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame13.Location = New System.Drawing.Point(0, -7)
        Me.Frame13.Name = "Frame13"
        Me.Frame13.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame13.Size = New System.Drawing.Size(830, 548)
        Me.Frame13.TabIndex = 1
        Me.Frame13.TabStop = False
        '
        'SSTab1
        '
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage0)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage1)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage3)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage4)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage6)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage7)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage8)
        Me.SSTab1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SSTab1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SSTab1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.SSTab1.ItemSize = New System.Drawing.Size(42, 24)
        Me.SSTab1.Location = New System.Drawing.Point(0, 15)
        Me.SSTab1.Name = "SSTab1"
        Me.SSTab1.SelectedIndex = 7
        Me.SSTab1.Size = New System.Drawing.Size(830, 533)
        Me.SSTab1.TabIndex = 4
        '
        '_SSTab1_TabPage0
        '
        Me._SSTab1_TabPage0.Controls.Add(Me._FraBorder_8)
        Me._SSTab1_TabPage0.Location = New System.Drawing.Point(4, 28)
        Me._SSTab1_TabPage0.Name = "_SSTab1_TabPage0"
        Me._SSTab1_TabPage0.Size = New System.Drawing.Size(822, 501)
        Me._SSTab1_TabPage0.TabIndex = 0
        Me._SSTab1_TabPage0.Text = "Reports"
        '
        '_FraBorder_8
        '
        Me._FraBorder_8.BackColor = System.Drawing.SystemColors.Control
        Me._FraBorder_8.Controls.Add(Me.GroupBox31)
        Me._FraBorder_8.Controls.Add(Me.GroupBox30)
        Me._FraBorder_8.Controls.Add(Me.GroupBox29)
        Me._FraBorder_8.Controls.Add(Me.GroupBox28)
        Me._FraBorder_8.Controls.Add(Me.GroupBox27)
        Me._FraBorder_8.Controls.Add(Me.GroupBox26)
        Me._FraBorder_8.Controls.Add(Me.GroupBox25)
        Me._FraBorder_8.Controls.Add(Me.GroupBox24)
        Me._FraBorder_8.Controls.Add(Me.GroupBox23)
        Me._FraBorder_8.Controls.Add(Me.GroupBox22)
        Me._FraBorder_8.Controls.Add(Me.GroupBox21)
        Me._FraBorder_8.Controls.Add(Me.GroupBox20)
        Me._FraBorder_8.Controls.Add(Me.GroupBox19)
        Me._FraBorder_8.Controls.Add(Me.GroupBox18)
        Me._FraBorder_8.Controls.Add(Me.GroupBox17)
        Me._FraBorder_8.Controls.Add(Me.GroupBox6)
        Me._FraBorder_8.Controls.Add(Me.chkAcPRAutoJv)
        Me._FraBorder_8.Controls.Add(Me.GroupBox8)
        Me._FraBorder_8.Controls.Add(Me.GroupBox16)
        Me._FraBorder_8.Controls.Add(Me.GroupBox5)
        Me._FraBorder_8.Controls.Add(Me.txtCompAc)
        Me._FraBorder_8.Controls.Add(Me.Label1)
        Me._FraBorder_8.Controls.Add(Me.GroupBox14)
        Me._FraBorder_8.Controls.Add(Me.Frame10)
        Me._FraBorder_8.Controls.Add(Me.GroupBox13)
        Me._FraBorder_8.Controls.Add(Me.Frame21)
        Me._FraBorder_8.Controls.Add(Me.Frame15)
        Me._FraBorder_8.Controls.Add(Me.GroupBox11)
        Me._FraBorder_8.Controls.Add(Me.Frame18)
        Me._FraBorder_8.Controls.Add(Me.Frame17)
        Me._FraBorder_8.Controls.Add(Me.Frame9)
        Me._FraBorder_8.Controls.Add(Me.GroupBox7)
        Me._FraBorder_8.Controls.Add(Me.Frame6)
        Me._FraBorder_8.Controls.Add(Me.fraMargin)
        Me._FraBorder_8.Controls.Add(Me.Frame11)
        Me._FraBorder_8.Controls.Add(Me.Frame7)
        Me._FraBorder_8.Controls.Add(Me.Frame5)
        Me._FraBorder_8.Controls.Add(Me.Frame16)
        Me._FraBorder_8.Controls.Add(Me.Frame19)
        Me._FraBorder_8.Dock = System.Windows.Forms.DockStyle.Fill
        Me._FraBorder_8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._FraBorder_8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraBorder.SetIndex(Me._FraBorder_8, CType(8, Short))
        Me._FraBorder_8.Location = New System.Drawing.Point(0, 0)
        Me._FraBorder_8.Name = "_FraBorder_8"
        Me._FraBorder_8.Padding = New System.Windows.Forms.Padding(0)
        Me._FraBorder_8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._FraBorder_8.Size = New System.Drawing.Size(822, 501)
        Me._FraBorder_8.TabIndex = 5
        Me._FraBorder_8.TabStop = False
        '
        'GroupBox31
        '
        Me.GroupBox31.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox31.Controls.Add(Me.ChkOuterPackCol)
        Me.GroupBox31.Controls.Add(Me.Label46)
        Me.GroupBox31.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox31.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GroupBox31.Location = New System.Drawing.Point(206, 448)
        Me.GroupBox31.Name = "GroupBox31"
        Me.GroupBox31.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox31.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox31.Size = New System.Drawing.Size(190, 37)
        Me.GroupBox31.TabIndex = 137
        Me.GroupBox31.TabStop = False
        Me.GroupBox31.Text = "Show Outer Pack Col"
        '
        'ChkOuterPackCol
        '
        Me.ChkOuterPackCol.BackColor = System.Drawing.SystemColors.Control
        Me.ChkOuterPackCol.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkOuterPackCol.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkOuterPackCol.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkOuterPackCol.Location = New System.Drawing.Point(4, 16)
        Me.ChkOuterPackCol.Name = "ChkOuterPackCol"
        Me.ChkOuterPackCol.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkOuterPackCol.Size = New System.Drawing.Size(83, 16)
        Me.ChkOuterPackCol.TabIndex = 106
        Me.ChkOuterPackCol.Text = "(Yes / No)"
        Me.ChkOuterPackCol.UseVisualStyleBackColor = False
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.BackColor = System.Drawing.SystemColors.Control
        Me.Label46.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label46.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label46.Location = New System.Drawing.Point(132, 66)
        Me.Label46.Name = "Label46"
        Me.Label46.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label46.Size = New System.Drawing.Size(0, 13)
        Me.Label46.TabIndex = 107
        '
        'GroupBox30
        '
        Me.GroupBox30.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox30.Controls.Add(Me.ChkBom)
        Me.GroupBox30.Controls.Add(Me.Label45)
        Me.GroupBox30.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox30.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GroupBox30.Location = New System.Drawing.Point(4, 448)
        Me.GroupBox30.Name = "GroupBox30"
        Me.GroupBox30.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox30.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox30.Size = New System.Drawing.Size(190, 37)
        Me.GroupBox30.TabIndex = 136
        Me.GroupBox30.TabStop = False
        Me.GroupBox30.Text = "Check Bom"
        '
        'ChkBom
        '
        Me.ChkBom.BackColor = System.Drawing.SystemColors.Control
        Me.ChkBom.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkBom.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkBom.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkBom.Location = New System.Drawing.Point(4, 16)
        Me.ChkBom.Name = "ChkBom"
        Me.ChkBom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkBom.Size = New System.Drawing.Size(83, 16)
        Me.ChkBom.TabIndex = 106
        Me.ChkBom.Text = "(Yes / No)"
        Me.ChkBom.UseVisualStyleBackColor = False
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.BackColor = System.Drawing.SystemColors.Control
        Me.Label45.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label45.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label45.Location = New System.Drawing.Point(132, 66)
        Me.Label45.Name = "Label45"
        Me.Label45.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label45.Size = New System.Drawing.Size(0, 13)
        Me.Label45.TabIndex = 107
        '
        'GroupBox29
        '
        Me.GroupBox29.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox29.Controls.Add(Me.ChkSaleScheduleReq)
        Me.GroupBox29.Controls.Add(Me.Label44)
        Me.GroupBox29.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox29.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GroupBox29.Location = New System.Drawing.Point(417, 448)
        Me.GroupBox29.Name = "GroupBox29"
        Me.GroupBox29.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox29.Size = New System.Drawing.Size(190, 37)
        Me.GroupBox29.TabIndex = 135
        Me.GroupBox29.TabStop = False
        Me.GroupBox29.Text = "Sale Schedule Required"
        '
        'ChkSaleScheduleReq
        '
        Me.ChkSaleScheduleReq.BackColor = System.Drawing.SystemColors.Control
        Me.ChkSaleScheduleReq.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkSaleScheduleReq.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkSaleScheduleReq.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkSaleScheduleReq.Location = New System.Drawing.Point(4, 16)
        Me.ChkSaleScheduleReq.Name = "ChkSaleScheduleReq"
        Me.ChkSaleScheduleReq.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkSaleScheduleReq.Size = New System.Drawing.Size(83, 16)
        Me.ChkSaleScheduleReq.TabIndex = 106
        Me.ChkSaleScheduleReq.Text = "(Yes / No)"
        Me.ChkSaleScheduleReq.UseVisualStyleBackColor = False
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.BackColor = System.Drawing.SystemColors.Control
        Me.Label44.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label44.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label44.Location = New System.Drawing.Point(132, 66)
        Me.Label44.Name = "Label44"
        Me.Label44.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label44.Size = New System.Drawing.Size(0, 13)
        Me.Label44.TabIndex = 107
        '
        'GroupBox28
        '
        Me.GroupBox28.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox28.Controls.Add(Me.ChkQtyCheck)
        Me.GroupBox28.Controls.Add(Me.Label43)
        Me.GroupBox28.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox28.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GroupBox28.Location = New System.Drawing.Point(4, 406)
        Me.GroupBox28.Name = "GroupBox28"
        Me.GroupBox28.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox28.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox28.Size = New System.Drawing.Size(190, 37)
        Me.GroupBox28.TabIndex = 134
        Me.GroupBox28.TabStop = False
        Me.GroupBox28.Text = "Minimum Qty Check"
        '
        'ChkQtyCheck
        '
        Me.ChkQtyCheck.BackColor = System.Drawing.SystemColors.Control
        Me.ChkQtyCheck.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkQtyCheck.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkQtyCheck.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkQtyCheck.Location = New System.Drawing.Point(4, 16)
        Me.ChkQtyCheck.Name = "ChkQtyCheck"
        Me.ChkQtyCheck.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkQtyCheck.Size = New System.Drawing.Size(83, 16)
        Me.ChkQtyCheck.TabIndex = 106
        Me.ChkQtyCheck.Text = "(Yes / No)"
        Me.ChkQtyCheck.UseVisualStyleBackColor = False
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.BackColor = System.Drawing.SystemColors.Control
        Me.Label43.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label43.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label43.Location = New System.Drawing.Point(132, 66)
        Me.Label43.Name = "Label43"
        Me.Label43.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label43.Size = New System.Drawing.Size(0, 13)
        Me.Label43.TabIndex = 107
        '
        'GroupBox27
        '
        Me.GroupBox27.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox27.Controls.Add(Me.ChkINVFromStock)
        Me.GroupBox27.Controls.Add(Me.Label42)
        Me.GroupBox27.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox27.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GroupBox27.Location = New System.Drawing.Point(619, 448)
        Me.GroupBox27.Name = "GroupBox27"
        Me.GroupBox27.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox27.Size = New System.Drawing.Size(190, 37)
        Me.GroupBox27.TabIndex = 133
        Me.GroupBox27.TabStop = False
        Me.GroupBox27.Text = "INV From Stock"
        '
        'ChkINVFromStock
        '
        Me.ChkINVFromStock.BackColor = System.Drawing.SystemColors.Control
        Me.ChkINVFromStock.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkINVFromStock.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkINVFromStock.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkINVFromStock.Location = New System.Drawing.Point(4, 16)
        Me.ChkINVFromStock.Name = "ChkINVFromStock"
        Me.ChkINVFromStock.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkINVFromStock.Size = New System.Drawing.Size(83, 16)
        Me.ChkINVFromStock.TabIndex = 106
        Me.ChkINVFromStock.Text = "(Yes / No)"
        Me.ChkINVFromStock.UseVisualStyleBackColor = False
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.BackColor = System.Drawing.SystemColors.Control
        Me.Label42.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label42.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label42.Location = New System.Drawing.Point(132, 66)
        Me.Label42.Name = "Label42"
        Me.Label42.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label42.Size = New System.Drawing.Size(0, 13)
        Me.Label42.TabIndex = 107
        '
        'GroupBox26
        '
        Me.GroupBox26.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox26.Controls.Add(Me.ChkMRPSaleOrder)
        Me.GroupBox26.Controls.Add(Me.Label41)
        Me.GroupBox26.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox26.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GroupBox26.Location = New System.Drawing.Point(619, 406)
        Me.GroupBox26.Name = "GroupBox26"
        Me.GroupBox26.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox26.Size = New System.Drawing.Size(190, 37)
        Me.GroupBox26.TabIndex = 132
        Me.GroupBox26.TabStop = False
        Me.GroupBox26.Text = "Check MRP Sale Order"
        '
        'ChkMRPSaleOrder
        '
        Me.ChkMRPSaleOrder.BackColor = System.Drawing.SystemColors.Control
        Me.ChkMRPSaleOrder.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkMRPSaleOrder.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkMRPSaleOrder.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkMRPSaleOrder.Location = New System.Drawing.Point(4, 16)
        Me.ChkMRPSaleOrder.Name = "ChkMRPSaleOrder"
        Me.ChkMRPSaleOrder.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkMRPSaleOrder.Size = New System.Drawing.Size(83, 16)
        Me.ChkMRPSaleOrder.TabIndex = 106
        Me.ChkMRPSaleOrder.Text = "(Yes / No)"
        Me.ChkMRPSaleOrder.UseVisualStyleBackColor = False
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.BackColor = System.Drawing.SystemColors.Control
        Me.Label41.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label41.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label41.Location = New System.Drawing.Point(132, 66)
        Me.Label41.Name = "Label41"
        Me.Label41.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label41.Size = New System.Drawing.Size(0, 13)
        Me.Label41.TabIndex = 107
        '
        'GroupBox25
        '
        Me.GroupBox25.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox25.Controls.Add(Me.ChkLockPayterms)
        Me.GroupBox25.Controls.Add(Me.Label36)
        Me.GroupBox25.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox25.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GroupBox25.Location = New System.Drawing.Point(619, 363)
        Me.GroupBox25.Name = "GroupBox25"
        Me.GroupBox25.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox25.Size = New System.Drawing.Size(190, 37)
        Me.GroupBox25.TabIndex = 131
        Me.GroupBox25.TabStop = False
        Me.GroupBox25.Text = "Lock Invoice Payterms"
        '
        'ChkLockPayterms
        '
        Me.ChkLockPayterms.BackColor = System.Drawing.SystemColors.Control
        Me.ChkLockPayterms.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkLockPayterms.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkLockPayterms.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkLockPayterms.Location = New System.Drawing.Point(4, 16)
        Me.ChkLockPayterms.Name = "ChkLockPayterms"
        Me.ChkLockPayterms.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkLockPayterms.Size = New System.Drawing.Size(83, 16)
        Me.ChkLockPayterms.TabIndex = 106
        Me.ChkLockPayterms.Text = "(Yes / No)"
        Me.ChkLockPayterms.UseVisualStyleBackColor = False
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.BackColor = System.Drawing.SystemColors.Control
        Me.Label36.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label36.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label36.Location = New System.Drawing.Point(132, 66)
        Me.Label36.Name = "Label36"
        Me.Label36.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label36.Size = New System.Drawing.Size(0, 13)
        Me.Label36.TabIndex = 107
        '
        'GroupBox24
        '
        Me.GroupBox24.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox24.Controls.Add(Me.ChkSaleOrderIndent)
        Me.GroupBox24.Controls.Add(Me.Label35)
        Me.GroupBox24.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox24.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GroupBox24.Location = New System.Drawing.Point(619, 317)
        Me.GroupBox24.Name = "GroupBox24"
        Me.GroupBox24.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox24.Size = New System.Drawing.Size(190, 37)
        Me.GroupBox24.TabIndex = 130
        Me.GroupBox24.TabStop = False
        Me.GroupBox24.Text = "Sale Order Wise Indent"
        '
        'ChkSaleOrderIndent
        '
        Me.ChkSaleOrderIndent.BackColor = System.Drawing.SystemColors.Control
        Me.ChkSaleOrderIndent.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkSaleOrderIndent.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkSaleOrderIndent.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkSaleOrderIndent.Location = New System.Drawing.Point(4, 16)
        Me.ChkSaleOrderIndent.Name = "ChkSaleOrderIndent"
        Me.ChkSaleOrderIndent.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkSaleOrderIndent.Size = New System.Drawing.Size(83, 16)
        Me.ChkSaleOrderIndent.TabIndex = 106
        Me.ChkSaleOrderIndent.Text = "(Yes / No)"
        Me.ChkSaleOrderIndent.UseVisualStyleBackColor = False
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.BackColor = System.Drawing.SystemColors.Control
        Me.Label35.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label35.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label35.Location = New System.Drawing.Point(132, 66)
        Me.Label35.Name = "Label35"
        Me.Label35.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label35.Size = New System.Drawing.Size(0, 13)
        Me.Label35.TabIndex = 107
        '
        'GroupBox23
        '
        Me.GroupBox23.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox23.Controls.Add(Me.ChkDoubleSalary)
        Me.GroupBox23.Controls.Add(Me.Label34)
        Me.GroupBox23.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox23.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GroupBox23.Location = New System.Drawing.Point(619, 274)
        Me.GroupBox23.Name = "GroupBox23"
        Me.GroupBox23.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox23.Size = New System.Drawing.Size(190, 37)
        Me.GroupBox23.TabIndex = 129
        Me.GroupBox23.TabStop = False
        Me.GroupBox23.Text = "Double Salary"
        '
        'ChkDoubleSalary
        '
        Me.ChkDoubleSalary.BackColor = System.Drawing.SystemColors.Control
        Me.ChkDoubleSalary.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkDoubleSalary.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkDoubleSalary.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkDoubleSalary.Location = New System.Drawing.Point(4, 16)
        Me.ChkDoubleSalary.Name = "ChkDoubleSalary"
        Me.ChkDoubleSalary.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkDoubleSalary.Size = New System.Drawing.Size(83, 16)
        Me.ChkDoubleSalary.TabIndex = 106
        Me.ChkDoubleSalary.Text = "(Yes / No)"
        Me.ChkDoubleSalary.UseVisualStyleBackColor = False
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.BackColor = System.Drawing.SystemColors.Control
        Me.Label34.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label34.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label34.Location = New System.Drawing.Point(132, 66)
        Me.Label34.Name = "Label34"
        Me.Label34.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label34.Size = New System.Drawing.Size(0, 13)
        Me.Label34.TabIndex = 107
        '
        'GroupBox22
        '
        Me.GroupBox22.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox22.Controls.Add(Me.ChkElFixed)
        Me.GroupBox22.Controls.Add(Me.Label32)
        Me.GroupBox22.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox22.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GroupBox22.Location = New System.Drawing.Point(619, 229)
        Me.GroupBox22.Name = "GroupBox22"
        Me.GroupBox22.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox22.Size = New System.Drawing.Size(190, 37)
        Me.GroupBox22.TabIndex = 128
        Me.GroupBox22.TabStop = False
        Me.GroupBox22.Text = "IS EL Fixed"
        '
        'ChkElFixed
        '
        Me.ChkElFixed.BackColor = System.Drawing.SystemColors.Control
        Me.ChkElFixed.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkElFixed.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkElFixed.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkElFixed.Location = New System.Drawing.Point(4, 16)
        Me.ChkElFixed.Name = "ChkElFixed"
        Me.ChkElFixed.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkElFixed.Size = New System.Drawing.Size(83, 16)
        Me.ChkElFixed.TabIndex = 106
        Me.ChkElFixed.Text = "(Yes / No)"
        Me.ChkElFixed.UseVisualStyleBackColor = False
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.BackColor = System.Drawing.SystemColors.Control
        Me.Label32.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label32.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label32.Location = New System.Drawing.Point(132, 66)
        Me.Label32.Name = "Label32"
        Me.Label32.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label32.Size = New System.Drawing.Size(0, 13)
        Me.Label32.TabIndex = 107
        '
        'GroupBox21
        '
        Me.GroupBox21.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox21.Controls.Add(Me.ChkAfterConfirm)
        Me.GroupBox21.Controls.Add(Me.Label31)
        Me.GroupBox21.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox21.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GroupBox21.Location = New System.Drawing.Point(619, 187)
        Me.GroupBox21.Name = "GroupBox21"
        Me.GroupBox21.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox21.Size = New System.Drawing.Size(190, 37)
        Me.GroupBox21.TabIndex = 127
        Me.GroupBox21.TabStop = False
        Me.GroupBox21.Text = "Entitle After Confirmation"
        '
        'ChkAfterConfirm
        '
        Me.ChkAfterConfirm.BackColor = System.Drawing.SystemColors.Control
        Me.ChkAfterConfirm.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkAfterConfirm.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkAfterConfirm.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkAfterConfirm.Location = New System.Drawing.Point(4, 16)
        Me.ChkAfterConfirm.Name = "ChkAfterConfirm"
        Me.ChkAfterConfirm.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkAfterConfirm.Size = New System.Drawing.Size(83, 16)
        Me.ChkAfterConfirm.TabIndex = 106
        Me.ChkAfterConfirm.Text = "(Yes / No)"
        Me.ChkAfterConfirm.UseVisualStyleBackColor = False
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.BackColor = System.Drawing.SystemColors.Control
        Me.Label31.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label31.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label31.Location = New System.Drawing.Point(132, 66)
        Me.Label31.Name = "Label31"
        Me.Label31.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label31.Size = New System.Drawing.Size(0, 13)
        Me.Label31.TabIndex = 107
        '
        'GroupBox20
        '
        Me.GroupBox20.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox20.Controls.Add(Me.ChkWarHouse)
        Me.GroupBox20.Controls.Add(Me.Label30)
        Me.GroupBox20.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox20.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GroupBox20.Location = New System.Drawing.Point(619, 144)
        Me.GroupBox20.Name = "GroupBox20"
        Me.GroupBox20.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox20.Size = New System.Drawing.Size(190, 37)
        Me.GroupBox20.TabIndex = 126
        Me.GroupBox20.TabStop = False
        Me.GroupBox20.Text = "Is WareHouse"
        '
        'ChkWarHouse
        '
        Me.ChkWarHouse.BackColor = System.Drawing.SystemColors.Control
        Me.ChkWarHouse.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkWarHouse.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkWarHouse.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkWarHouse.Location = New System.Drawing.Point(4, 16)
        Me.ChkWarHouse.Name = "ChkWarHouse"
        Me.ChkWarHouse.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkWarHouse.Size = New System.Drawing.Size(83, 16)
        Me.ChkWarHouse.TabIndex = 106
        Me.ChkWarHouse.Text = "(Yes / No)"
        Me.ChkWarHouse.UseVisualStyleBackColor = False
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.BackColor = System.Drawing.SystemColors.Control
        Me.Label30.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label30.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label30.Location = New System.Drawing.Point(132, 66)
        Me.Label30.Name = "Label30"
        Me.Label30.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label30.Size = New System.Drawing.Size(0, 13)
        Me.Label30.TabIndex = 107
        '
        'GroupBox19
        '
        Me.GroupBox19.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox19.Controls.Add(Me.ChkAutoGenCode)
        Me.GroupBox19.Controls.Add(Me.Label29)
        Me.GroupBox19.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox19.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GroupBox19.Location = New System.Drawing.Point(619, 101)
        Me.GroupBox19.Name = "GroupBox19"
        Me.GroupBox19.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox19.Size = New System.Drawing.Size(190, 37)
        Me.GroupBox19.TabIndex = 125
        Me.GroupBox19.TabStop = False
        Me.GroupBox19.Text = "Auto Gen Code"
        '
        'ChkAutoGenCode
        '
        Me.ChkAutoGenCode.BackColor = System.Drawing.SystemColors.Control
        Me.ChkAutoGenCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkAutoGenCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkAutoGenCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkAutoGenCode.Location = New System.Drawing.Point(4, 16)
        Me.ChkAutoGenCode.Name = "ChkAutoGenCode"
        Me.ChkAutoGenCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkAutoGenCode.Size = New System.Drawing.Size(83, 16)
        Me.ChkAutoGenCode.TabIndex = 106
        Me.ChkAutoGenCode.Text = "(Yes / No)"
        Me.ChkAutoGenCode.UseVisualStyleBackColor = False
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.SystemColors.Control
        Me.Label29.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label29.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label29.Location = New System.Drawing.Point(132, 66)
        Me.Label29.Name = "Label29"
        Me.Label29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label29.Size = New System.Drawing.Size(0, 13)
        Me.Label29.TabIndex = 107
        '
        'GroupBox18
        '
        Me.GroupBox18.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox18.Controls.Add(Me.ChkDCInLedger)
        Me.GroupBox18.Controls.Add(Me.Label28)
        Me.GroupBox18.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox18.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GroupBox18.Location = New System.Drawing.Point(619, 57)
        Me.GroupBox18.Name = "GroupBox18"
        Me.GroupBox18.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox18.Size = New System.Drawing.Size(190, 37)
        Me.GroupBox18.TabIndex = 124
        Me.GroupBox18.TabStop = False
        Me.GroupBox18.Text = "IS Post DC In_Ledger"
        '
        'ChkDCInLedger
        '
        Me.ChkDCInLedger.BackColor = System.Drawing.SystemColors.Control
        Me.ChkDCInLedger.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkDCInLedger.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkDCInLedger.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkDCInLedger.Location = New System.Drawing.Point(4, 16)
        Me.ChkDCInLedger.Name = "ChkDCInLedger"
        Me.ChkDCInLedger.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkDCInLedger.Size = New System.Drawing.Size(83, 16)
        Me.ChkDCInLedger.TabIndex = 106
        Me.ChkDCInLedger.Text = "(Yes / No)"
        Me.ChkDCInLedger.UseVisualStyleBackColor = False
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.SystemColors.Control
        Me.Label28.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label28.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label28.Location = New System.Drawing.Point(132, 66)
        Me.Label28.Name = "Label28"
        Me.Label28.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label28.Size = New System.Drawing.Size(0, 13)
        Me.Label28.TabIndex = 107
        '
        'GroupBox17
        '
        Me.GroupBox17.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox17.Controls.Add(Me.ChkBilladj)
        Me.GroupBox17.Controls.Add(Me.Label27)
        Me.GroupBox17.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GroupBox17.Location = New System.Drawing.Point(619, 11)
        Me.GroupBox17.Name = "GroupBox17"
        Me.GroupBox17.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox17.Size = New System.Drawing.Size(190, 37)
        Me.GroupBox17.TabIndex = 123
        Me.GroupBox17.TabStop = False
        Me.GroupBox17.Text = "Mannual Bill Adjustment"
        '
        'ChkBilladj
        '
        Me.ChkBilladj.BackColor = System.Drawing.SystemColors.Control
        Me.ChkBilladj.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkBilladj.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkBilladj.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkBilladj.Location = New System.Drawing.Point(4, 16)
        Me.ChkBilladj.Name = "ChkBilladj"
        Me.ChkBilladj.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkBilladj.Size = New System.Drawing.Size(83, 16)
        Me.ChkBilladj.TabIndex = 106
        Me.ChkBilladj.Text = "(Yes / No)"
        Me.ChkBilladj.UseVisualStyleBackColor = False
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.SystemColors.Control
        Me.Label27.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label27.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label27.Location = New System.Drawing.Point(132, 66)
        Me.Label27.Name = "Label27"
        Me.Label27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label27.Size = New System.Drawing.Size(0, 13)
        Me.Label27.TabIndex = 107
        '
        'GroupBox6
        '
        Me.GroupBox6.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox6.Controls.Add(Me.ChkDivlocation)
        Me.GroupBox6.Controls.Add(Me.Label7)
        Me.GroupBox6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GroupBox6.Location = New System.Drawing.Point(417, 406)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox6.Size = New System.Drawing.Size(190, 37)
        Me.GroupBox6.TabIndex = 118
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Div Location"
        '
        'ChkDivlocation
        '
        Me.ChkDivlocation.BackColor = System.Drawing.SystemColors.Control
        Me.ChkDivlocation.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkDivlocation.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkDivlocation.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkDivlocation.Location = New System.Drawing.Point(4, 16)
        Me.ChkDivlocation.Name = "ChkDivlocation"
        Me.ChkDivlocation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkDivlocation.Size = New System.Drawing.Size(83, 16)
        Me.ChkDivlocation.TabIndex = 106
        Me.ChkDivlocation.Text = "(Yes / No)"
        Me.ChkDivlocation.UseVisualStyleBackColor = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(132, 66)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(0, 13)
        Me.Label7.TabIndex = 107
        '
        'chkAcPRAutoJv
        '
        Me.chkAcPRAutoJv.BackColor = System.Drawing.SystemColors.Control
        Me.chkAcPRAutoJv.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAcPRAutoJv.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAcPRAutoJv.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAcPRAutoJv.Location = New System.Drawing.Point(317, 294)
        Me.chkAcPRAutoJv.Name = "chkAcPRAutoJv"
        Me.chkAcPRAutoJv.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAcPRAutoJv.Size = New System.Drawing.Size(285, 16)
        Me.chkAcPRAutoJv.TabIndex = 122
        Me.chkAcPRAutoJv.Text = "In Voucher Payment / Receive Auto JV For Diff. Unit"
        Me.chkAcPRAutoJv.UseVisualStyleBackColor = False
        '
        'GroupBox8
        '
        Me.GroupBox8.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox8.Controls.Add(Me.ChkAWavailable)
        Me.GroupBox8.Controls.Add(Me.Label17)
        Me.GroupBox8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GroupBox8.Location = New System.Drawing.Point(3, 363)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox8.Size = New System.Drawing.Size(190, 37)
        Me.GroupBox8.TabIndex = 109
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "EW Available"
        '
        'ChkAWavailable
        '
        Me.ChkAWavailable.BackColor = System.Drawing.SystemColors.Control
        Me.ChkAWavailable.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkAWavailable.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkAWavailable.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkAWavailable.Location = New System.Drawing.Point(4, 16)
        Me.ChkAWavailable.Name = "ChkAWavailable"
        Me.ChkAWavailable.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkAWavailable.Size = New System.Drawing.Size(83, 16)
        Me.ChkAWavailable.TabIndex = 106
        Me.ChkAWavailable.Text = "(Yes / No)"
        Me.ChkAWavailable.UseVisualStyleBackColor = False
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(132, 66)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(0, 13)
        Me.Label17.TabIndex = 107
        '
        'GroupBox16
        '
        Me.GroupBox16.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox16.Controls.Add(Me.ChkInnerPackCol)
        Me.GroupBox16.Controls.Add(Me.Label26)
        Me.GroupBox16.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GroupBox16.Location = New System.Drawing.Point(206, 406)
        Me.GroupBox16.Name = "GroupBox16"
        Me.GroupBox16.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox16.Size = New System.Drawing.Size(190, 37)
        Me.GroupBox16.TabIndex = 117
        Me.GroupBox16.TabStop = False
        Me.GroupBox16.Text = "Show Inner Pack Col"
        '
        'ChkInnerPackCol
        '
        Me.ChkInnerPackCol.BackColor = System.Drawing.SystemColors.Control
        Me.ChkInnerPackCol.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkInnerPackCol.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkInnerPackCol.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkInnerPackCol.Location = New System.Drawing.Point(4, 16)
        Me.ChkInnerPackCol.Name = "ChkInnerPackCol"
        Me.ChkInnerPackCol.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkInnerPackCol.Size = New System.Drawing.Size(83, 16)
        Me.ChkInnerPackCol.TabIndex = 106
        Me.ChkInnerPackCol.Text = "(Yes / No)"
        Me.ChkInnerPackCol.UseVisualStyleBackColor = False
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.SystemColors.Control
        Me.Label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label26.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(132, 66)
        Me.Label26.Name = "Label26"
        Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label26.Size = New System.Drawing.Size(0, 13)
        Me.Label26.TabIndex = 107
        '
        'GroupBox5
        '
        Me.GroupBox5.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox5.Controls.Add(Me.ChkPrintOTInPayslip)
        Me.GroupBox5.Controls.Add(Me.Label3)
        Me.GroupBox5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GroupBox5.Location = New System.Drawing.Point(3, 317)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox5.Size = New System.Drawing.Size(190, 37)
        Me.GroupBox5.TabIndex = 106
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Print OT In Payslip"
        '
        'ChkPrintOTInPayslip
        '
        Me.ChkPrintOTInPayslip.BackColor = System.Drawing.SystemColors.Control
        Me.ChkPrintOTInPayslip.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkPrintOTInPayslip.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkPrintOTInPayslip.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkPrintOTInPayslip.Location = New System.Drawing.Point(4, 16)
        Me.ChkPrintOTInPayslip.Name = "ChkPrintOTInPayslip"
        Me.ChkPrintOTInPayslip.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkPrintOTInPayslip.Size = New System.Drawing.Size(83, 16)
        Me.ChkPrintOTInPayslip.TabIndex = 106
        Me.ChkPrintOTInPayslip.Text = "(Yes / No)"
        Me.ChkPrintOTInPayslip.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(132, 66)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(0, 13)
        Me.Label3.TabIndex = 107
        '
        'txtCompAc
        '
        Me.txtCompAc.AcceptsReturn = True
        Me.txtCompAc.BackColor = System.Drawing.SystemColors.Window
        Me.txtCompAc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCompAc.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCompAc.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompAc.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCompAc.Location = New System.Drawing.Point(317, 267)
        Me.txtCompAc.MaxLength = 0
        Me.txtCompAc.Name = "txtCompAc"
        Me.txtCompAc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCompAc.Size = New System.Drawing.Size(288, 22)
        Me.txtCompAc.TabIndex = 120
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(203, 269)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(113, 15)
        Me.Label1.TabIndex = 121
        Me.Label1.Text = "Company Account :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'GroupBox14
        '
        Me.GroupBox14.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox14.Controls.Add(Me.ChkTCS)
        Me.GroupBox14.Controls.Add(Me.Label24)
        Me.GroupBox14.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox14.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GroupBox14.Location = New System.Drawing.Point(417, 362)
        Me.GroupBox14.Name = "GroupBox14"
        Me.GroupBox14.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox14.Size = New System.Drawing.Size(190, 37)
        Me.GroupBox14.TabIndex = 115
        Me.GroupBox14.TabStop = False
        Me.GroupBox14.Text = "TCS Applicable"
        '
        'ChkTCS
        '
        Me.ChkTCS.BackColor = System.Drawing.SystemColors.Control
        Me.ChkTCS.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkTCS.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkTCS.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkTCS.Location = New System.Drawing.Point(4, 16)
        Me.ChkTCS.Name = "ChkTCS"
        Me.ChkTCS.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkTCS.Size = New System.Drawing.Size(83, 16)
        Me.ChkTCS.TabIndex = 106
        Me.ChkTCS.Text = "(Yes / No)"
        Me.ChkTCS.UseVisualStyleBackColor = False
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.SystemColors.Control
        Me.Label24.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label24.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label24.Location = New System.Drawing.Point(132, 66)
        Me.Label24.Name = "Label24"
        Me.Label24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label24.Size = New System.Drawing.Size(0, 13)
        Me.Label24.TabIndex = 107
        '
        'Frame10
        '
        Me.Frame10.BackColor = System.Drawing.SystemColors.Control
        Me.Frame10.Controls.Add(Me.chkAttMc)
        Me.Frame10.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Frame10.Location = New System.Drawing.Point(426, 112)
        Me.Frame10.Name = "Frame10"
        Me.Frame10.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame10.Size = New System.Drawing.Size(177, 45)
        Me.Frame10.TabIndex = 92
        Me.Frame10.TabStop = False
        Me.Frame10.Text = "Attendance Data From M/c"
        '
        'chkAttMc
        '
        Me.chkAttMc.BackColor = System.Drawing.SystemColors.Control
        Me.chkAttMc.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAttMc.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAttMc.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAttMc.Location = New System.Drawing.Point(12, 16)
        Me.chkAttMc.Name = "chkAttMc"
        Me.chkAttMc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAttMc.Size = New System.Drawing.Size(128, 17)
        Me.chkAttMc.TabIndex = 93
        Me.chkAttMc.Text = "Yes / No"
        Me.chkAttMc.UseVisualStyleBackColor = False
        '
        'GroupBox13
        '
        Me.GroupBox13.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox13.Controls.Add(Me.ChkPacketCol)
        Me.GroupBox13.Controls.Add(Me.Label23)
        Me.GroupBox13.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GroupBox13.Location = New System.Drawing.Point(206, 363)
        Me.GroupBox13.Name = "GroupBox13"
        Me.GroupBox13.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox13.Size = New System.Drawing.Size(190, 37)
        Me.GroupBox13.TabIndex = 114
        Me.GroupBox13.TabStop = False
        Me.GroupBox13.Text = "Show Packets Column"
        '
        'ChkPacketCol
        '
        Me.ChkPacketCol.BackColor = System.Drawing.SystemColors.Control
        Me.ChkPacketCol.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkPacketCol.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkPacketCol.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkPacketCol.Location = New System.Drawing.Point(4, 16)
        Me.ChkPacketCol.Name = "ChkPacketCol"
        Me.ChkPacketCol.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkPacketCol.Size = New System.Drawing.Size(83, 16)
        Me.ChkPacketCol.TabIndex = 106
        Me.ChkPacketCol.Text = "(Yes / No)"
        Me.ChkPacketCol.UseVisualStyleBackColor = False
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.SystemColors.Control
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(132, 66)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(0, 13)
        Me.Label23.TabIndex = 107
        '
        'Frame21
        '
        Me.Frame21.BackColor = System.Drawing.SystemColors.Control
        Me.Frame21.Controls.Add(Me.chkMaintMaxLevel)
        Me.Frame21.Controls.Add(Me.chkConsMaxLevel)
        Me.Frame21.Controls.Add(Me.chkRMMaxLevel)
        Me.Frame21.Controls.Add(Me.chkBOPMaxLevel)
        Me.Frame21.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame21.Location = New System.Drawing.Point(426, 156)
        Me.Frame21.Name = "Frame21"
        Me.Frame21.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame21.Size = New System.Drawing.Size(179, 105)
        Me.Frame21.TabIndex = 119
        Me.Frame21.TabStop = False
        Me.Frame21.Text = "Gate Entry Validate Max Level"
        '
        'chkMaintMaxLevel
        '
        Me.chkMaintMaxLevel.BackColor = System.Drawing.SystemColors.Control
        Me.chkMaintMaxLevel.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkMaintMaxLevel.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMaintMaxLevel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkMaintMaxLevel.Location = New System.Drawing.Point(6, 71)
        Me.chkMaintMaxLevel.Name = "chkMaintMaxLevel"
        Me.chkMaintMaxLevel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkMaintMaxLevel.Size = New System.Drawing.Size(167, 16)
        Me.chkMaintMaxLevel.TabIndex = 123
        Me.chkMaintMaxLevel.Text = "Maintaince (Yes / No)"
        Me.chkMaintMaxLevel.UseVisualStyleBackColor = False
        '
        'chkConsMaxLevel
        '
        Me.chkConsMaxLevel.BackColor = System.Drawing.SystemColors.Control
        Me.chkConsMaxLevel.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkConsMaxLevel.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkConsMaxLevel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkConsMaxLevel.Location = New System.Drawing.Point(6, 53)
        Me.chkConsMaxLevel.Name = "chkConsMaxLevel"
        Me.chkConsMaxLevel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkConsMaxLevel.Size = New System.Drawing.Size(167, 15)
        Me.chkConsMaxLevel.TabIndex = 122
        Me.chkConsMaxLevel.Text = "Consumable (Yes / No)"
        Me.chkConsMaxLevel.UseVisualStyleBackColor = False
        '
        'chkRMMaxLevel
        '
        Me.chkRMMaxLevel.BackColor = System.Drawing.SystemColors.Control
        Me.chkRMMaxLevel.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkRMMaxLevel.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRMMaxLevel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRMMaxLevel.Location = New System.Drawing.Point(6, 33)
        Me.chkRMMaxLevel.Name = "chkRMMaxLevel"
        Me.chkRMMaxLevel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkRMMaxLevel.Size = New System.Drawing.Size(167, 16)
        Me.chkRMMaxLevel.TabIndex = 121
        Me.chkRMMaxLevel.Text = "Raw Material (Yes / No)"
        Me.chkRMMaxLevel.UseVisualStyleBackColor = False
        '
        'chkBOPMaxLevel
        '
        Me.chkBOPMaxLevel.BackColor = System.Drawing.SystemColors.Control
        Me.chkBOPMaxLevel.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkBOPMaxLevel.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBOPMaxLevel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkBOPMaxLevel.Location = New System.Drawing.Point(6, 18)
        Me.chkBOPMaxLevel.Name = "chkBOPMaxLevel"
        Me.chkBOPMaxLevel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkBOPMaxLevel.Size = New System.Drawing.Size(167, 16)
        Me.chkBOPMaxLevel.TabIndex = 120
        Me.chkBOPMaxLevel.Text = "BOP (Yes / No)"
        Me.chkBOPMaxLevel.UseVisualStyleBackColor = False
        '
        'Frame15
        '
        Me.Frame15.BackColor = System.Drawing.SystemColors.Control
        Me.Frame15.Controls.Add(Me.chkPurchase)
        Me.Frame15.Controls.Add(Me.chkMRR)
        Me.Frame15.Controls.Add(Me.chkDespatch)
        Me.Frame15.Controls.Add(Me.chkGatepass)
        Me.Frame15.Controls.Add(Me.chkInvoice)
        Me.Frame15.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame15.Location = New System.Drawing.Point(200, 156)
        Me.Frame15.Name = "Frame15"
        Me.Frame15.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame15.Size = New System.Drawing.Size(194, 108)
        Me.Frame15.TabIndex = 99
        Me.Frame15.TabStop = False
        Me.Frame15.Text = "Division Wise Separate Series"
        '
        'chkPurchase
        '
        Me.chkPurchase.BackColor = System.Drawing.SystemColors.Control
        Me.chkPurchase.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPurchase.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPurchase.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPurchase.Location = New System.Drawing.Point(6, 89)
        Me.chkPurchase.Name = "chkPurchase"
        Me.chkPurchase.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPurchase.Size = New System.Drawing.Size(185, 16)
        Me.chkPurchase.TabIndex = 104
        Me.chkPurchase.Text = "Purchase Voucher (Yes / No)"
        Me.chkPurchase.UseVisualStyleBackColor = False
        '
        'chkMRR
        '
        Me.chkMRR.BackColor = System.Drawing.SystemColors.Control
        Me.chkMRR.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkMRR.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMRR.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkMRR.Location = New System.Drawing.Point(6, 18)
        Me.chkMRR.Name = "chkMRR"
        Me.chkMRR.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkMRR.Size = New System.Drawing.Size(174, 16)
        Me.chkMRR.TabIndex = 103
        Me.chkMRR.Text = "MRR (Yes / No)"
        Me.chkMRR.UseVisualStyleBackColor = False
        '
        'chkDespatch
        '
        Me.chkDespatch.BackColor = System.Drawing.SystemColors.Control
        Me.chkDespatch.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkDespatch.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDespatch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkDespatch.Location = New System.Drawing.Point(6, 33)
        Me.chkDespatch.Name = "chkDespatch"
        Me.chkDespatch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkDespatch.Size = New System.Drawing.Size(175, 16)
        Me.chkDespatch.TabIndex = 102
        Me.chkDespatch.Text = "Despatch Note  (Yes / No)"
        Me.chkDespatch.UseVisualStyleBackColor = False
        '
        'chkGatepass
        '
        Me.chkGatepass.BackColor = System.Drawing.SystemColors.Control
        Me.chkGatepass.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkGatepass.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGatepass.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkGatepass.Location = New System.Drawing.Point(6, 52)
        Me.chkGatepass.Name = "chkGatepass"
        Me.chkGatepass.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkGatepass.Size = New System.Drawing.Size(176, 16)
        Me.chkGatepass.TabIndex = 101
        Me.chkGatepass.Text = "RGP / NRGP  (Yes / No)"
        Me.chkGatepass.UseVisualStyleBackColor = False
        '
        'chkInvoice
        '
        Me.chkInvoice.BackColor = System.Drawing.SystemColors.Control
        Me.chkInvoice.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkInvoice.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkInvoice.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkInvoice.Location = New System.Drawing.Point(6, 71)
        Me.chkInvoice.Name = "chkInvoice"
        Me.chkInvoice.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkInvoice.Size = New System.Drawing.Size(175, 16)
        Me.chkInvoice.TabIndex = 100
        Me.chkInvoice.Text = "Invoice (Yes / No)"
        Me.chkInvoice.UseVisualStyleBackColor = False
        '
        'GroupBox11
        '
        Me.GroupBox11.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox11.Controls.Add(Me.ChkEInvoiceApp)
        Me.GroupBox11.Controls.Add(Me.Label21)
        Me.GroupBox11.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GroupBox11.Location = New System.Drawing.Point(206, 316)
        Me.GroupBox11.Name = "GroupBox11"
        Me.GroupBox11.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox11.Size = New System.Drawing.Size(190, 37)
        Me.GroupBox11.TabIndex = 112
        Me.GroupBox11.TabStop = False
        Me.GroupBox11.Text = "E_Invoice_App"
        '
        'ChkEInvoiceApp
        '
        Me.ChkEInvoiceApp.BackColor = System.Drawing.SystemColors.Control
        Me.ChkEInvoiceApp.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkEInvoiceApp.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkEInvoiceApp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkEInvoiceApp.Location = New System.Drawing.Point(4, 16)
        Me.ChkEInvoiceApp.Name = "ChkEInvoiceApp"
        Me.ChkEInvoiceApp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkEInvoiceApp.Size = New System.Drawing.Size(83, 16)
        Me.ChkEInvoiceApp.TabIndex = 106
        Me.ChkEInvoiceApp.Text = "(Yes / No)"
        Me.ChkEInvoiceApp.UseVisualStyleBackColor = False
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.SystemColors.Control
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(132, 66)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(0, 13)
        Me.Label21.TabIndex = 107
        '
        'Frame18
        '
        Me.Frame18.BackColor = System.Drawing.SystemColors.Control
        Me.Frame18.Controls.Add(Me.chkPurPlanning)
        Me.Frame18.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame18.Location = New System.Drawing.Point(1, 235)
        Me.Frame18.Name = "Frame18"
        Me.Frame18.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame18.Size = New System.Drawing.Size(189, 38)
        Me.Frame18.TabIndex = 111
        Me.Frame18.TabStop = False
        Me.Frame18.Text = "Purchase Schedule From Planning"
        '
        'chkPurPlanning
        '
        Me.chkPurPlanning.BackColor = System.Drawing.SystemColors.Control
        Me.chkPurPlanning.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPurPlanning.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPurPlanning.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPurPlanning.Location = New System.Drawing.Point(3, 16)
        Me.chkPurPlanning.Name = "chkPurPlanning"
        Me.chkPurPlanning.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPurPlanning.Size = New System.Drawing.Size(95, 16)
        Me.chkPurPlanning.TabIndex = 112
        Me.chkPurPlanning.Text = "( Yes / No )"
        Me.chkPurPlanning.UseVisualStyleBackColor = False
        '
        'Frame17
        '
        Me.Frame17.BackColor = System.Drawing.SystemColors.Control
        Me.Frame17.Controls.Add(Me.chkCheckPORate)
        Me.Frame17.Controls.Add(Me.chkPOCheckInGE)
        Me.Frame17.Controls.Add(Me._Label12_6)
        Me.Frame17.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Frame17.Location = New System.Drawing.Point(1, 197)
        Me.Frame17.Name = "Frame17"
        Me.Frame17.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame17.Size = New System.Drawing.Size(189, 35)
        Me.Frame17.TabIndex = 108
        Me.Frame17.TabStop = False
        Me.Frame17.Text = "PO Check in Gate Entry"
        '
        'chkCheckPORate
        '
        Me.chkCheckPORate.BackColor = System.Drawing.SystemColors.Control
        Me.chkCheckPORate.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCheckPORate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCheckPORate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCheckPORate.Location = New System.Drawing.Point(85, 16)
        Me.chkCheckPORate.Name = "chkCheckPORate"
        Me.chkCheckPORate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCheckPORate.Size = New System.Drawing.Size(103, 16)
        Me.chkCheckPORate.TabIndex = 111
        Me.chkCheckPORate.Text = "Rate (Yes / No)"
        Me.chkCheckPORate.UseVisualStyleBackColor = False
        '
        'chkPOCheckInGE
        '
        Me.chkPOCheckInGE.BackColor = System.Drawing.SystemColors.Control
        Me.chkPOCheckInGE.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPOCheckInGE.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPOCheckInGE.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPOCheckInGE.Location = New System.Drawing.Point(3, 16)
        Me.chkPOCheckInGE.Name = "chkPOCheckInGE"
        Me.chkPOCheckInGE.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPOCheckInGE.Size = New System.Drawing.Size(83, 16)
        Me.chkPOCheckInGE.TabIndex = 109
        Me.chkPOCheckInGE.Text = "(Yes / No)"
        Me.chkPOCheckInGE.UseVisualStyleBackColor = False
        '
        '_Label12_6
        '
        Me._Label12_6.AutoSize = True
        Me._Label12_6.BackColor = System.Drawing.SystemColors.Control
        Me._Label12_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label12_6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label12_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.SetIndex(Me._Label12_6, CType(6, Short))
        Me._Label12_6.Location = New System.Drawing.Point(132, 66)
        Me._Label12_6.Name = "_Label12_6"
        Me._Label12_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label12_6.Size = New System.Drawing.Size(0, 13)
        Me._Label12_6.TabIndex = 110
        '
        'Frame9
        '
        Me.Frame9.BackColor = System.Drawing.SystemColors.Control
        Me.Frame9.Controls.Add(Me.chkOnLine)
        Me.Frame9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Frame9.Location = New System.Drawing.Point(426, 68)
        Me.Frame9.Name = "Frame9"
        Me.Frame9.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame9.Size = New System.Drawing.Size(177, 43)
        Me.Frame9.TabIndex = 90
        Me.Frame9.TabStop = False
        Me.Frame9.Text = "Online Status"
        '
        'chkOnLine
        '
        Me.chkOnLine.BackColor = System.Drawing.SystemColors.Control
        Me.chkOnLine.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkOnLine.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOnLine.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkOnLine.Location = New System.Drawing.Point(12, 18)
        Me.chkOnLine.Name = "chkOnLine"
        Me.chkOnLine.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkOnLine.Size = New System.Drawing.Size(125, 19)
        Me.chkOnLine.TabIndex = 91
        Me.chkOnLine.Text = "Yes / No"
        Me.chkOnLine.UseVisualStyleBackColor = False
        '
        'GroupBox7
        '
        Me.GroupBox7.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox7.Controls.Add(Me.ChkGSTSeparate)
        Me.GroupBox7.Controls.Add(Me.Label13)
        Me.GroupBox7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GroupBox7.Location = New System.Drawing.Point(417, 316)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox7.Size = New System.Drawing.Size(190, 37)
        Me.GroupBox7.TabIndex = 108
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "GST_Separate"
        '
        'ChkGSTSeparate
        '
        Me.ChkGSTSeparate.BackColor = System.Drawing.SystemColors.Control
        Me.ChkGSTSeparate.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkGSTSeparate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkGSTSeparate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkGSTSeparate.Location = New System.Drawing.Point(4, 16)
        Me.ChkGSTSeparate.Name = "ChkGSTSeparate"
        Me.ChkGSTSeparate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkGSTSeparate.Size = New System.Drawing.Size(83, 16)
        Me.ChkGSTSeparate.TabIndex = 106
        Me.ChkGSTSeparate.Text = "(Yes / No)"
        Me.ChkGSTSeparate.UseVisualStyleBackColor = False
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(132, 66)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(0, 13)
        Me.Label13.TabIndex = 107
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.chkInvTableCC)
        Me.Frame6.Controls.Add(Me.chkInvTableFYear)
        Me.Frame6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Frame6.Location = New System.Drawing.Point(426, 9)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(177, 57)
        Me.Frame6.TabIndex = 87
        Me.Frame6.TabStop = False
        Me.Frame6.Text = "Inventory Table"
        '
        'chkInvTableCC
        '
        Me.chkInvTableCC.BackColor = System.Drawing.SystemColors.Control
        Me.chkInvTableCC.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkInvTableCC.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkInvTableCC.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkInvTableCC.Location = New System.Drawing.Point(12, 14)
        Me.chkInvTableCC.Name = "chkInvTableCC"
        Me.chkInvTableCC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkInvTableCC.Size = New System.Drawing.Size(125, 19)
        Me.chkInvTableCC.TabIndex = 89
        Me.chkInvTableCC.Text = "Company Code"
        Me.chkInvTableCC.UseVisualStyleBackColor = False
        '
        'chkInvTableFYear
        '
        Me.chkInvTableFYear.BackColor = System.Drawing.SystemColors.Control
        Me.chkInvTableFYear.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkInvTableFYear.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkInvTableFYear.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkInvTableFYear.Location = New System.Drawing.Point(12, 34)
        Me.chkInvTableFYear.Name = "chkInvTableFYear"
        Me.chkInvTableFYear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkInvTableFYear.Size = New System.Drawing.Size(129, 17)
        Me.chkInvTableFYear.TabIndex = 88
        Me.chkInvTableFYear.Text = "FYear"
        Me.chkInvTableFYear.UseVisualStyleBackColor = False
        '
        'fraMargin
        '
        Me.fraMargin.BackColor = System.Drawing.SystemColors.Control
        Me.fraMargin.Controls.Add(Me.CmdDefaultMargin)
        Me.fraMargin.Controls.Add(Me._txtMargin_3)
        Me.fraMargin.Controls.Add(Me._txtMargin_2)
        Me.fraMargin.Controls.Add(Me._txtMargin_1)
        Me.fraMargin.Controls.Add(Me._txtMargin_0)
        Me.fraMargin.Controls.Add(Me._Label12_3)
        Me.fraMargin.Controls.Add(Me._Label12_2)
        Me.fraMargin.Controls.Add(Me._Label12_1)
        Me.fraMargin.Controls.Add(Me._Label12_0)
        Me.fraMargin.Controls.Add(Me.Label11)
        Me.fraMargin.Controls.Add(Me.Label10)
        Me.fraMargin.Controls.Add(Me.Label9)
        Me.fraMargin.Controls.Add(Me.Label8)
        Me.fraMargin.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraMargin.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.fraMargin.Location = New System.Drawing.Point(302, 9)
        Me.fraMargin.Name = "fraMargin"
        Me.fraMargin.Padding = New System.Windows.Forms.Padding(0)
        Me.fraMargin.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraMargin.Size = New System.Drawing.Size(121, 147)
        Me.fraMargin.TabIndex = 21
        Me.fraMargin.TabStop = False
        Me.fraMargin.Text = "Margin"
        '
        'CmdDefaultMargin
        '
        Me.CmdDefaultMargin.BackColor = System.Drawing.SystemColors.Control
        Me.CmdDefaultMargin.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdDefaultMargin.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdDefaultMargin.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdDefaultMargin.Location = New System.Drawing.Point(38, 106)
        Me.CmdDefaultMargin.Name = "CmdDefaultMargin"
        Me.CmdDefaultMargin.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDefaultMargin.Size = New System.Drawing.Size(47, 31)
        Me.CmdDefaultMargin.TabIndex = 26
        Me.CmdDefaultMargin.Text = "Set Default"
        Me.CmdDefaultMargin.UseVisualStyleBackColor = False
        '
        '_txtMargin_3
        '
        Me._txtMargin_3.AcceptsReturn = True
        Me._txtMargin_3.BackColor = System.Drawing.SystemColors.Window
        Me._txtMargin_3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me._txtMargin_3.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtMargin_3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtMargin_3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMargin.SetIndex(Me._txtMargin_3, CType(3, Short))
        Me._txtMargin_3.Location = New System.Drawing.Point(50, 82)
        Me._txtMargin_3.MaxLength = 0
        Me._txtMargin_3.Name = "_txtMargin_3"
        Me._txtMargin_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtMargin_3.Size = New System.Drawing.Size(25, 22)
        Me._txtMargin_3.TabIndex = 25
        '
        '_txtMargin_2
        '
        Me._txtMargin_2.AcceptsReturn = True
        Me._txtMargin_2.BackColor = System.Drawing.SystemColors.Window
        Me._txtMargin_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me._txtMargin_2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtMargin_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtMargin_2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMargin.SetIndex(Me._txtMargin_2, CType(2, Short))
        Me._txtMargin_2.Location = New System.Drawing.Point(50, 60)
        Me._txtMargin_2.MaxLength = 0
        Me._txtMargin_2.Name = "_txtMargin_2"
        Me._txtMargin_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtMargin_2.Size = New System.Drawing.Size(25, 22)
        Me._txtMargin_2.TabIndex = 24
        '
        '_txtMargin_1
        '
        Me._txtMargin_1.AcceptsReturn = True
        Me._txtMargin_1.BackColor = System.Drawing.SystemColors.Window
        Me._txtMargin_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me._txtMargin_1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtMargin_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtMargin_1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMargin.SetIndex(Me._txtMargin_1, CType(1, Short))
        Me._txtMargin_1.Location = New System.Drawing.Point(50, 38)
        Me._txtMargin_1.MaxLength = 0
        Me._txtMargin_1.Name = "_txtMargin_1"
        Me._txtMargin_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtMargin_1.Size = New System.Drawing.Size(25, 22)
        Me._txtMargin_1.TabIndex = 23
        '
        '_txtMargin_0
        '
        Me._txtMargin_0.AcceptsReturn = True
        Me._txtMargin_0.BackColor = System.Drawing.SystemColors.Window
        Me._txtMargin_0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me._txtMargin_0.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtMargin_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtMargin_0.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMargin.SetIndex(Me._txtMargin_0, CType(0, Short))
        Me._txtMargin_0.Location = New System.Drawing.Point(50, 16)
        Me._txtMargin_0.MaxLength = 0
        Me._txtMargin_0.Name = "_txtMargin_0"
        Me._txtMargin_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtMargin_0.Size = New System.Drawing.Size(25, 22)
        Me._txtMargin_0.TabIndex = 22
        '
        '_Label12_3
        '
        Me._Label12_3.AutoSize = True
        Me._Label12_3.BackColor = System.Drawing.SystemColors.Control
        Me._Label12_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label12_3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label12_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.SetIndex(Me._Label12_3, CType(3, Short))
        Me._Label12_3.Location = New System.Drawing.Point(82, 84)
        Me._Label12_3.Name = "_Label12_3"
        Me._Label12_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label12_3.Size = New System.Drawing.Size(27, 13)
        Me._Label12_3.TabIndex = 34
        Me._Label12_3.Text = "Inch"
        '
        '_Label12_2
        '
        Me._Label12_2.AutoSize = True
        Me._Label12_2.BackColor = System.Drawing.SystemColors.Control
        Me._Label12_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label12_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label12_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.SetIndex(Me._Label12_2, CType(2, Short))
        Me._Label12_2.Location = New System.Drawing.Point(82, 64)
        Me._Label12_2.Name = "_Label12_2"
        Me._Label12_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label12_2.Size = New System.Drawing.Size(27, 13)
        Me._Label12_2.TabIndex = 33
        Me._Label12_2.Text = "Inch"
        '
        '_Label12_1
        '
        Me._Label12_1.AutoSize = True
        Me._Label12_1.BackColor = System.Drawing.SystemColors.Control
        Me._Label12_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label12_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label12_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.SetIndex(Me._Label12_1, CType(1, Short))
        Me._Label12_1.Location = New System.Drawing.Point(82, 44)
        Me._Label12_1.Name = "_Label12_1"
        Me._Label12_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label12_1.Size = New System.Drawing.Size(27, 13)
        Me._Label12_1.TabIndex = 32
        Me._Label12_1.Text = "Inch"
        '
        '_Label12_0
        '
        Me._Label12_0.AutoSize = True
        Me._Label12_0.BackColor = System.Drawing.SystemColors.Control
        Me._Label12_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label12_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label12_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.SetIndex(Me._Label12_0, CType(0, Short))
        Me._Label12_0.Location = New System.Drawing.Point(82, 18)
        Me._Label12_0.Name = "_Label12_0"
        Me._Label12_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label12_0.Size = New System.Drawing.Size(27, 13)
        Me._Label12_0.TabIndex = 31
        Me._Label12_0.Text = "Inch"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(6, 84)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(33, 13)
        Me.Label11.TabIndex = 30
        Me.Label11.Text = "Right"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(6, 62)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(33, 13)
        Me.Label10.TabIndex = 29
        Me.Label10.Text = "Left"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(6, 40)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(41, 11)
        Me.Label9.TabIndex = 28
        Me.Label9.Text = "Bot"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(6, 16)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(31, 13)
        Me.Label8.TabIndex = 27
        Me.Label8.Text = "Top"
        '
        'Frame11
        '
        Me.Frame11.BackColor = System.Drawing.SystemColors.Control
        Me.Frame11.Controls.Add(Me.chkPrintTopCompanyAddress)
        Me.Frame11.Controls.Add(Me.chkPrintTopCompanyPhone)
        Me.Frame11.Controls.Add(Me.Frame12)
        Me.Frame11.Controls.Add(Me.ChkPrintTopCompanyName)
        Me.Frame11.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Frame11.Location = New System.Drawing.Point(0, 9)
        Me.Frame11.Name = "Frame11"
        Me.Frame11.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame11.Size = New System.Drawing.Size(149, 147)
        Me.Frame11.TabIndex = 14
        Me.Frame11.TabStop = False
        Me.Frame11.Text = "Top of Report"
        '
        'chkPrintTopCompanyAddress
        '
        Me.chkPrintTopCompanyAddress.BackColor = System.Drawing.SystemColors.Control
        Me.chkPrintTopCompanyAddress.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPrintTopCompanyAddress.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPrintTopCompanyAddress.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPrintTopCompanyAddress.Location = New System.Drawing.Point(4, 84)
        Me.chkPrintTopCompanyAddress.Name = "chkPrintTopCompanyAddress"
        Me.chkPrintTopCompanyAddress.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPrintTopCompanyAddress.Size = New System.Drawing.Size(129, 17)
        Me.chkPrintTopCompanyAddress.TabIndex = 20
        Me.chkPrintTopCompanyAddress.Text = "Company Address"
        Me.chkPrintTopCompanyAddress.UseVisualStyleBackColor = False
        '
        'chkPrintTopCompanyPhone
        '
        Me.chkPrintTopCompanyPhone.BackColor = System.Drawing.SystemColors.Control
        Me.chkPrintTopCompanyPhone.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPrintTopCompanyPhone.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPrintTopCompanyPhone.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPrintTopCompanyPhone.Location = New System.Drawing.Point(4, 112)
        Me.chkPrintTopCompanyPhone.Name = "chkPrintTopCompanyPhone"
        Me.chkPrintTopCompanyPhone.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPrintTopCompanyPhone.Size = New System.Drawing.Size(143, 17)
        Me.chkPrintTopCompanyPhone.TabIndex = 19
        Me.chkPrintTopCompanyPhone.Text = "Company Phone Nos."
        Me.chkPrintTopCompanyPhone.UseVisualStyleBackColor = False
        '
        'Frame12
        '
        Me.Frame12.BackColor = System.Drawing.SystemColors.Control
        Me.Frame12.Controls.Add(Me._OptPrintCompanyFull_ShortName_0)
        Me.Frame12.Controls.Add(Me._OptPrintCompanyFull_ShortName_1)
        Me.Frame12.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame12.Location = New System.Drawing.Point(1, 30)
        Me.Frame12.Name = "Frame12"
        Me.Frame12.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame12.Size = New System.Drawing.Size(145, 51)
        Me.Frame12.TabIndex = 16
        Me.Frame12.TabStop = False
        '
        '_OptPrintCompanyFull_ShortName_0
        '
        Me._OptPrintCompanyFull_ShortName_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptPrintCompanyFull_ShortName_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptPrintCompanyFull_ShortName_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptPrintCompanyFull_ShortName_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptPrintCompanyFull_ShortName.SetIndex(Me._OptPrintCompanyFull_ShortName_0, CType(0, Short))
        Me._OptPrintCompanyFull_ShortName_0.Location = New System.Drawing.Point(2, 12)
        Me._OptPrintCompanyFull_ShortName_0.Name = "_OptPrintCompanyFull_ShortName_0"
        Me._OptPrintCompanyFull_ShortName_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptPrintCompanyFull_ShortName_0.Size = New System.Drawing.Size(133, 17)
        Me._OptPrintCompanyFull_ShortName_0.TabIndex = 18
        Me._OptPrintCompanyFull_ShortName_0.TabStop = True
        Me._OptPrintCompanyFull_ShortName_0.Text = "Company Full Name"
        Me._OptPrintCompanyFull_ShortName_0.UseVisualStyleBackColor = False
        '
        '_OptPrintCompanyFull_ShortName_1
        '
        Me._OptPrintCompanyFull_ShortName_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptPrintCompanyFull_ShortName_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptPrintCompanyFull_ShortName_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptPrintCompanyFull_ShortName_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptPrintCompanyFull_ShortName.SetIndex(Me._OptPrintCompanyFull_ShortName_1, CType(1, Short))
        Me._OptPrintCompanyFull_ShortName_1.Location = New System.Drawing.Point(2, 30)
        Me._OptPrintCompanyFull_ShortName_1.Name = "_OptPrintCompanyFull_ShortName_1"
        Me._OptPrintCompanyFull_ShortName_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptPrintCompanyFull_ShortName_1.Size = New System.Drawing.Size(127, 16)
        Me._OptPrintCompanyFull_ShortName_1.TabIndex = 17
        Me._OptPrintCompanyFull_ShortName_1.TabStop = True
        Me._OptPrintCompanyFull_ShortName_1.Text = "Short Name"
        Me._OptPrintCompanyFull_ShortName_1.UseVisualStyleBackColor = False
        '
        'ChkPrintTopCompanyName
        '
        Me.ChkPrintTopCompanyName.BackColor = System.Drawing.SystemColors.Control
        Me.ChkPrintTopCompanyName.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkPrintTopCompanyName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkPrintTopCompanyName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkPrintTopCompanyName.Location = New System.Drawing.Point(4, 14)
        Me.ChkPrintTopCompanyName.Name = "ChkPrintTopCompanyName"
        Me.ChkPrintTopCompanyName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkPrintTopCompanyName.Size = New System.Drawing.Size(125, 19)
        Me.ChkPrintTopCompanyName.TabIndex = 15
        Me.ChkPrintTopCompanyName.Text = "Company Name"
        Me.ChkPrintTopCompanyName.UseVisualStyleBackColor = False
        '
        'Frame7
        '
        Me.Frame7.BackColor = System.Drawing.SystemColors.Control
        Me.Frame7.Controls.Add(Me.ChkPrintBotCompanyName)
        Me.Frame7.Controls.Add(Me.chkPrintBotCompanyAddress)
        Me.Frame7.Controls.Add(Me.chkPrintBotCompanyPhone)
        Me.Frame7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Frame7.Location = New System.Drawing.Point(150, 9)
        Me.Frame7.Name = "Frame7"
        Me.Frame7.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame7.Size = New System.Drawing.Size(151, 71)
        Me.Frame7.TabIndex = 10
        Me.Frame7.TabStop = False
        Me.Frame7.Text = "Bottom of Reports"
        '
        'ChkPrintBotCompanyName
        '
        Me.ChkPrintBotCompanyName.BackColor = System.Drawing.SystemColors.Control
        Me.ChkPrintBotCompanyName.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkPrintBotCompanyName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkPrintBotCompanyName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkPrintBotCompanyName.Location = New System.Drawing.Point(4, 14)
        Me.ChkPrintBotCompanyName.Name = "ChkPrintBotCompanyName"
        Me.ChkPrintBotCompanyName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkPrintBotCompanyName.Size = New System.Drawing.Size(119, 19)
        Me.ChkPrintBotCompanyName.TabIndex = 13
        Me.ChkPrintBotCompanyName.Text = "Company Name"
        Me.ChkPrintBotCompanyName.UseVisualStyleBackColor = False
        '
        'chkPrintBotCompanyAddress
        '
        Me.chkPrintBotCompanyAddress.BackColor = System.Drawing.SystemColors.Control
        Me.chkPrintBotCompanyAddress.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPrintBotCompanyAddress.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPrintBotCompanyAddress.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPrintBotCompanyAddress.Location = New System.Drawing.Point(4, 32)
        Me.chkPrintBotCompanyAddress.Name = "chkPrintBotCompanyAddress"
        Me.chkPrintBotCompanyAddress.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPrintBotCompanyAddress.Size = New System.Drawing.Size(127, 17)
        Me.chkPrintBotCompanyAddress.TabIndex = 12
        Me.chkPrintBotCompanyAddress.Text = "Company Address"
        Me.chkPrintBotCompanyAddress.UseVisualStyleBackColor = False
        '
        'chkPrintBotCompanyPhone
        '
        Me.chkPrintBotCompanyPhone.BackColor = System.Drawing.SystemColors.Control
        Me.chkPrintBotCompanyPhone.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPrintBotCompanyPhone.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPrintBotCompanyPhone.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPrintBotCompanyPhone.Location = New System.Drawing.Point(4, 48)
        Me.chkPrintBotCompanyPhone.Name = "chkPrintBotCompanyPhone"
        Me.chkPrintBotCompanyPhone.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPrintBotCompanyPhone.Size = New System.Drawing.Size(143, 17)
        Me.chkPrintBotCompanyPhone.TabIndex = 11
        Me.chkPrintBotCompanyPhone.Text = "Company Phone Nos."
        Me.chkPrintBotCompanyPhone.UseVisualStyleBackColor = False
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me.ChkPrintPAgeNo)
        Me.Frame5.Controls.Add(Me.ChkPrintRunDate)
        Me.Frame5.Controls.Add(Me.chkPrintUser)
        Me.Frame5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Frame5.Location = New System.Drawing.Point(151, 78)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(149, 77)
        Me.Frame5.TabIndex = 6
        Me.Frame5.TabStop = False
        Me.Frame5.Text = "Print"
        '
        'ChkPrintPAgeNo
        '
        Me.ChkPrintPAgeNo.BackColor = System.Drawing.SystemColors.Control
        Me.ChkPrintPAgeNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkPrintPAgeNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkPrintPAgeNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkPrintPAgeNo.Location = New System.Drawing.Point(10, 54)
        Me.ChkPrintPAgeNo.Name = "ChkPrintPAgeNo"
        Me.ChkPrintPAgeNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkPrintPAgeNo.Size = New System.Drawing.Size(123, 21)
        Me.ChkPrintPAgeNo.TabIndex = 9
        Me.ChkPrintPAgeNo.Text = "Page No."
        Me.ChkPrintPAgeNo.UseVisualStyleBackColor = False
        '
        'ChkPrintRunDate
        '
        Me.ChkPrintRunDate.BackColor = System.Drawing.SystemColors.Control
        Me.ChkPrintRunDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkPrintRunDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkPrintRunDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkPrintRunDate.Location = New System.Drawing.Point(10, 32)
        Me.ChkPrintRunDate.Name = "ChkPrintRunDate"
        Me.ChkPrintRunDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkPrintRunDate.Size = New System.Drawing.Size(127, 25)
        Me.ChkPrintRunDate.TabIndex = 8
        Me.ChkPrintRunDate.Text = "Run Date"
        Me.ChkPrintRunDate.UseVisualStyleBackColor = False
        '
        'chkPrintUser
        '
        Me.chkPrintUser.BackColor = System.Drawing.SystemColors.Control
        Me.chkPrintUser.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPrintUser.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPrintUser.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPrintUser.Location = New System.Drawing.Point(10, 16)
        Me.chkPrintUser.Name = "chkPrintUser"
        Me.chkPrintUser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPrintUser.Size = New System.Drawing.Size(135, 19)
        Me.chkPrintUser.TabIndex = 7
        Me.chkPrintUser.Text = "User ID"
        Me.chkPrintUser.UseVisualStyleBackColor = False
        '
        'Frame16
        '
        Me.Frame16.BackColor = System.Drawing.SystemColors.Control
        Me.Frame16.Controls.Add(Me.chkGateEntry)
        Me.Frame16.Controls.Add(Me._Label12_5)
        Me.Frame16.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Frame16.Location = New System.Drawing.Point(0, 156)
        Me.Frame16.Name = "Frame16"
        Me.Frame16.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame16.Size = New System.Drawing.Size(190, 37)
        Me.Frame16.TabIndex = 105
        Me.Frame16.TabStop = False
        Me.Frame16.Text = "Gate Entry"
        '
        'chkGateEntry
        '
        Me.chkGateEntry.BackColor = System.Drawing.SystemColors.Control
        Me.chkGateEntry.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkGateEntry.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGateEntry.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkGateEntry.Location = New System.Drawing.Point(4, 16)
        Me.chkGateEntry.Name = "chkGateEntry"
        Me.chkGateEntry.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkGateEntry.Size = New System.Drawing.Size(83, 16)
        Me.chkGateEntry.TabIndex = 106
        Me.chkGateEntry.Text = "(Yes / No)"
        Me.chkGateEntry.UseVisualStyleBackColor = False
        '
        '_Label12_5
        '
        Me._Label12_5.AutoSize = True
        Me._Label12_5.BackColor = System.Drawing.SystemColors.Control
        Me._Label12_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label12_5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label12_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.SetIndex(Me._Label12_5, CType(5, Short))
        Me._Label12_5.Location = New System.Drawing.Point(132, 66)
        Me._Label12_5.Name = "_Label12_5"
        Me._Label12_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label12_5.Size = New System.Drawing.Size(0, 13)
        Me._Label12_5.TabIndex = 107
        '
        'Frame19
        '
        Me.Frame19.BackColor = System.Drawing.SystemColors.Control
        Me.Frame19.Controls.Add(Me.chkWeeklySchd)
        Me.Frame19.Controls.Add(Me._Label12_7)
        Me.Frame19.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame19.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Frame19.Location = New System.Drawing.Point(0, 273)
        Me.Frame19.Name = "Frame19"
        Me.Frame19.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame19.Size = New System.Drawing.Size(189, 38)
        Me.Frame19.TabIndex = 113
        Me.Frame19.TabStop = False
        Me.Frame19.Text = "Check Weekly Schedule"
        '
        'chkWeeklySchd
        '
        Me.chkWeeklySchd.BackColor = System.Drawing.SystemColors.Control
        Me.chkWeeklySchd.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkWeeklySchd.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkWeeklySchd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkWeeklySchd.Location = New System.Drawing.Point(4, 16)
        Me.chkWeeklySchd.Name = "chkWeeklySchd"
        Me.chkWeeklySchd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkWeeklySchd.Size = New System.Drawing.Size(83, 15)
        Me.chkWeeklySchd.TabIndex = 114
        Me.chkWeeklySchd.Text = "(Yes / No)"
        Me.chkWeeklySchd.UseVisualStyleBackColor = False
        '
        '_Label12_7
        '
        Me._Label12_7.AutoSize = True
        Me._Label12_7.BackColor = System.Drawing.SystemColors.Control
        Me._Label12_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label12_7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label12_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.SetIndex(Me._Label12_7, CType(7, Short))
        Me._Label12_7.Location = New System.Drawing.Point(132, 66)
        Me._Label12_7.Name = "_Label12_7"
        Me._Label12_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label12_7.Size = New System.Drawing.Size(0, 13)
        Me._Label12_7.TabIndex = 115
        '
        '_SSTab1_TabPage1
        '
        Me._SSTab1_TabPage1.Controls.Add(Me._FraBorder_5)
        Me._SSTab1_TabPage1.Location = New System.Drawing.Point(4, 28)
        Me._SSTab1_TabPage1.Name = "_SSTab1_TabPage1"
        Me._SSTab1_TabPage1.Size = New System.Drawing.Size(822, 533)
        Me._SSTab1_TabPage1.TabIndex = 1
        Me._SSTab1_TabPage1.Text = "TDS"
        '
        '_FraBorder_5
        '
        Me._FraBorder_5.BackColor = System.Drawing.SystemColors.Control
        Me._FraBorder_5.Controls.Add(Me.txtAuthorizedFName)
        Me._FraBorder_5.Controls.Add(Me.txtESI)
        Me._FraBorder_5.Controls.Add(Me.txtSTDS)
        Me._FraBorder_5.Controls.Add(Me.txtTDSCreditAcct)
        Me._FraBorder_5.Controls.Add(Me.txtPANNo)
        Me._FraBorder_5.Controls.Add(Me.txtTDSAcNo)
        Me._FraBorder_5.Controls.Add(Me.txtTDSCircle)
        Me._FraBorder_5.Controls.Add(Me.txtAuthorized)
        Me._FraBorder_5.Controls.Add(Me.txtDesignation)
        Me._FraBorder_5.Controls.Add(Me.Label33)
        Me._FraBorder_5.Controls.Add(Me.Label6)
        Me._FraBorder_5.Controls.Add(Me.Label5)
        Me._FraBorder_5.Controls.Add(Me.Label4)
        Me._FraBorder_5.Controls.Add(Me.Label53)
        Me._FraBorder_5.Controls.Add(Me.Label52)
        Me._FraBorder_5.Controls.Add(Me.Label51)
        Me._FraBorder_5.Controls.Add(Me.Label54)
        Me._FraBorder_5.Controls.Add(Me.Label55)
        Me._FraBorder_5.Dock = System.Windows.Forms.DockStyle.Fill
        Me._FraBorder_5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._FraBorder_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraBorder.SetIndex(Me._FraBorder_5, CType(5, Short))
        Me._FraBorder_5.Location = New System.Drawing.Point(0, 0)
        Me._FraBorder_5.Name = "_FraBorder_5"
        Me._FraBorder_5.Padding = New System.Windows.Forms.Padding(0)
        Me._FraBorder_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._FraBorder_5.Size = New System.Drawing.Size(822, 533)
        Me._FraBorder_5.TabIndex = 35
        Me._FraBorder_5.TabStop = False
        '
        'txtAuthorizedFName
        '
        Me.txtAuthorizedFName.AcceptsReturn = True
        Me.txtAuthorizedFName.BackColor = System.Drawing.SystemColors.Window
        Me.txtAuthorizedFName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAuthorizedFName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAuthorizedFName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAuthorizedFName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAuthorizedFName.Location = New System.Drawing.Point(166, 232)
        Me.txtAuthorizedFName.MaxLength = 0
        Me.txtAuthorizedFName.Name = "txtAuthorizedFName"
        Me.txtAuthorizedFName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAuthorizedFName.Size = New System.Drawing.Size(385, 22)
        Me.txtAuthorizedFName.TabIndex = 72
        '
        'txtESI
        '
        Me.txtESI.AcceptsReturn = True
        Me.txtESI.BackColor = System.Drawing.SystemColors.Window
        Me.txtESI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtESI.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtESI.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtESI.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtESI.Location = New System.Drawing.Point(166, 14)
        Me.txtESI.MaxLength = 0
        Me.txtESI.Name = "txtESI"
        Me.txtESI.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtESI.Size = New System.Drawing.Size(385, 22)
        Me.txtESI.TabIndex = 36
        '
        'txtSTDS
        '
        Me.txtSTDS.AcceptsReturn = True
        Me.txtSTDS.BackColor = System.Drawing.SystemColors.Window
        Me.txtSTDS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSTDS.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSTDS.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSTDS.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSTDS.Location = New System.Drawing.Point(166, 42)
        Me.txtSTDS.MaxLength = 0
        Me.txtSTDS.Name = "txtSTDS"
        Me.txtSTDS.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSTDS.Size = New System.Drawing.Size(385, 22)
        Me.txtSTDS.TabIndex = 37
        '
        'txtTDSCreditAcct
        '
        Me.txtTDSCreditAcct.AcceptsReturn = True
        Me.txtTDSCreditAcct.BackColor = System.Drawing.SystemColors.Window
        Me.txtTDSCreditAcct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTDSCreditAcct.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTDSCreditAcct.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTDSCreditAcct.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTDSCreditAcct.Location = New System.Drawing.Point(166, 72)
        Me.txtTDSCreditAcct.MaxLength = 0
        Me.txtTDSCreditAcct.Name = "txtTDSCreditAcct"
        Me.txtTDSCreditAcct.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTDSCreditAcct.Size = New System.Drawing.Size(385, 22)
        Me.txtTDSCreditAcct.TabIndex = 38
        '
        'txtPANNo
        '
        Me.txtPANNo.AcceptsReturn = True
        Me.txtPANNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPANNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPANNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPANNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPANNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPANNo.Location = New System.Drawing.Point(166, 167)
        Me.txtPANNo.MaxLength = 0
        Me.txtPANNo.Name = "txtPANNo"
        Me.txtPANNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPANNo.Size = New System.Drawing.Size(385, 22)
        Me.txtPANNo.TabIndex = 41
        '
        'txtTDSAcNo
        '
        Me.txtTDSAcNo.AcceptsReturn = True
        Me.txtTDSAcNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtTDSAcNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTDSAcNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTDSAcNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTDSAcNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTDSAcNo.Location = New System.Drawing.Point(166, 135)
        Me.txtTDSAcNo.MaxLength = 0
        Me.txtTDSAcNo.Name = "txtTDSAcNo"
        Me.txtTDSAcNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTDSAcNo.Size = New System.Drawing.Size(385, 22)
        Me.txtTDSAcNo.TabIndex = 40
        '
        'txtTDSCircle
        '
        Me.txtTDSCircle.AcceptsReturn = True
        Me.txtTDSCircle.BackColor = System.Drawing.SystemColors.Window
        Me.txtTDSCircle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTDSCircle.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTDSCircle.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTDSCircle.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTDSCircle.Location = New System.Drawing.Point(166, 103)
        Me.txtTDSCircle.MaxLength = 0
        Me.txtTDSCircle.Name = "txtTDSCircle"
        Me.txtTDSCircle.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTDSCircle.Size = New System.Drawing.Size(385, 22)
        Me.txtTDSCircle.TabIndex = 39
        '
        'txtAuthorized
        '
        Me.txtAuthorized.AcceptsReturn = True
        Me.txtAuthorized.BackColor = System.Drawing.SystemColors.Window
        Me.txtAuthorized.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAuthorized.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAuthorized.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAuthorized.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAuthorized.Location = New System.Drawing.Point(166, 199)
        Me.txtAuthorized.MaxLength = 0
        Me.txtAuthorized.Name = "txtAuthorized"
        Me.txtAuthorized.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAuthorized.Size = New System.Drawing.Size(385, 22)
        Me.txtAuthorized.TabIndex = 43
        '
        'txtDesignation
        '
        Me.txtDesignation.AcceptsReturn = True
        Me.txtDesignation.BackColor = System.Drawing.SystemColors.Window
        Me.txtDesignation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDesignation.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDesignation.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesignation.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDesignation.Location = New System.Drawing.Point(166, 263)
        Me.txtDesignation.MaxLength = 0
        Me.txtDesignation.Name = "txtDesignation"
        Me.txtDesignation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDesignation.Size = New System.Drawing.Size(385, 22)
        Me.txtDesignation.TabIndex = 45
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.BackColor = System.Drawing.SystemColors.Control
        Me.Label33.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label33.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label33.Location = New System.Drawing.Point(10, 236)
        Me.Label33.Name = "Label33"
        Me.Label33.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label33.Size = New System.Drawing.Size(144, 13)
        Me.Label33.TabIndex = 73
        Me.Label33.Text = "Authorized's Father Name :"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(34, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(82, 13)
        Me.Label6.TabIndex = 51
        Me.Label6.Text = "ESI Credit A/c :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(34, 44)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(93, 13)
        Me.Label5.TabIndex = 50
        Me.Label5.Text = "STDS Credit A/c :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(34, 74)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(87, 13)
        Me.Label4.TabIndex = 49
        Me.Label4.Text = "TDS Credit A/c :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.BackColor = System.Drawing.SystemColors.Control
        Me.Label53.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label53.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label53.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label53.Location = New System.Drawing.Point(34, 171)
        Me.Label53.Name = "Label53"
        Me.Label53.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label53.Size = New System.Drawing.Size(74, 13)
        Me.Label53.TabIndex = 48
        Me.Label53.Text = "PAN/GIR No :"
        Me.Label53.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label52
        '
        Me.Label52.AutoSize = True
        Me.Label52.BackColor = System.Drawing.SystemColors.Control
        Me.Label52.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label52.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label52.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label52.Location = New System.Drawing.Point(34, 137)
        Me.Label52.Name = "Label52"
        Me.Label52.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label52.Size = New System.Drawing.Size(71, 13)
        Me.Label52.TabIndex = 47
        Me.Label52.Text = "TDS A/c No :"
        Me.Label52.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label51
        '
        Me.Label51.AutoSize = True
        Me.Label51.BackColor = System.Drawing.SystemColors.Control
        Me.Label51.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label51.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label51.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label51.Location = New System.Drawing.Point(34, 105)
        Me.Label51.Name = "Label51"
        Me.Label51.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label51.Size = New System.Drawing.Size(64, 13)
        Me.Label51.TabIndex = 46
        Me.Label51.Text = "TDS Circle :"
        Me.Label51.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label54
        '
        Me.Label54.AutoSize = True
        Me.Label54.BackColor = System.Drawing.SystemColors.Control
        Me.Label54.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label54.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label54.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label54.Location = New System.Drawing.Point(34, 203)
        Me.Label54.Name = "Label54"
        Me.Label54.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label54.Size = New System.Drawing.Size(68, 13)
        Me.Label54.TabIndex = 44
        Me.Label54.Text = "Authorized :"
        Me.Label54.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label55
        '
        Me.Label55.AutoSize = True
        Me.Label55.BackColor = System.Drawing.SystemColors.Control
        Me.Label55.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label55.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label55.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label55.Location = New System.Drawing.Point(34, 267)
        Me.Label55.Name = "Label55"
        Me.Label55.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label55.Size = New System.Drawing.Size(74, 13)
        Me.Label55.TabIndex = 42
        Me.Label55.Text = "Designation :"
        Me.Label55.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_SSTab1_TabPage3
        '
        Me._SSTab1_TabPage3.Controls.Add(Me.Frame2)
        Me._SSTab1_TabPage3.Location = New System.Drawing.Point(4, 28)
        Me._SSTab1_TabPage3.Name = "_SSTab1_TabPage3"
        Me._SSTab1_TabPage3.Size = New System.Drawing.Size(822, 533)
        Me._SSTab1_TabPage3.TabIndex = 3
        Me._SSTab1_TabPage3.Text = "Export Detail"
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.txtCreditBank)
        Me.Frame2.Controls.Add(Me.txtFurtherBank)
        Me.Frame2.Controls.Add(Me.txtADCode)
        Me.Frame2.Controls.Add(Me.txtCreditBankAddress)
        Me.Frame2.Controls.Add(Me.Label37)
        Me.Frame2.Controls.Add(Me.Label38)
        Me.Frame2.Controls.Add(Me.Label39)
        Me.Frame2.Controls.Add(Me.Label40)
        Me.Frame2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Frame2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 0)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(822, 533)
        Me.Frame2.TabIndex = 196
        Me.Frame2.TabStop = False
        '
        'txtCreditBank
        '
        Me.txtCreditBank.AcceptsReturn = True
        Me.txtCreditBank.BackColor = System.Drawing.SystemColors.Window
        Me.txtCreditBank.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCreditBank.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCreditBank.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCreditBank.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCreditBank.Location = New System.Drawing.Point(116, 24)
        Me.txtCreditBank.MaxLength = 0
        Me.txtCreditBank.Name = "txtCreditBank"
        Me.txtCreditBank.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCreditBank.Size = New System.Drawing.Size(485, 22)
        Me.txtCreditBank.TabIndex = 199
        '
        'txtFurtherBank
        '
        Me.txtFurtherBank.AcceptsReturn = True
        Me.txtFurtherBank.BackColor = System.Drawing.SystemColors.Window
        Me.txtFurtherBank.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFurtherBank.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFurtherBank.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFurtherBank.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFurtherBank.Location = New System.Drawing.Point(116, 85)
        Me.txtFurtherBank.MaxLength = 0
        Me.txtFurtherBank.Name = "txtFurtherBank"
        Me.txtFurtherBank.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFurtherBank.Size = New System.Drawing.Size(485, 22)
        Me.txtFurtherBank.TabIndex = 198
        '
        'txtADCode
        '
        Me.txtADCode.AcceptsReturn = True
        Me.txtADCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtADCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtADCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtADCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtADCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtADCode.Location = New System.Drawing.Point(116, 115)
        Me.txtADCode.MaxLength = 0
        Me.txtADCode.Name = "txtADCode"
        Me.txtADCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtADCode.Size = New System.Drawing.Size(485, 22)
        Me.txtADCode.TabIndex = 197
        '
        'txtCreditBankAddress
        '
        Me.txtCreditBankAddress.AcceptsReturn = True
        Me.txtCreditBankAddress.BackColor = System.Drawing.SystemColors.Window
        Me.txtCreditBankAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCreditBankAddress.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCreditBankAddress.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCreditBankAddress.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCreditBankAddress.Location = New System.Drawing.Point(116, 54)
        Me.txtCreditBankAddress.MaxLength = 0
        Me.txtCreditBankAddress.Name = "txtCreditBankAddress"
        Me.txtCreditBankAddress.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCreditBankAddress.Size = New System.Drawing.Size(485, 22)
        Me.txtCreditBankAddress.TabIndex = 196
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.BackColor = System.Drawing.SystemColors.Control
        Me.Label37.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label37.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label37.Location = New System.Drawing.Point(21, 26)
        Me.Label37.Name = "Label37"
        Me.Label37.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label37.Size = New System.Drawing.Size(87, 13)
        Me.Label37.TabIndex = 203
        Me.Label37.Text = "Credit Bank To :"
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.BackColor = System.Drawing.SystemColors.Control
        Me.Label38.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label38.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label38.Location = New System.Drawing.Point(6, 87)
        Me.Label38.Name = "Label38"
        Me.Label38.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label38.Size = New System.Drawing.Size(101, 13)
        Me.Label38.TabIndex = 202
        Me.Label38.Text = "Further Credit To  :"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.BackColor = System.Drawing.SystemColors.Control
        Me.Label39.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label39.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label39.Location = New System.Drawing.Point(52, 117)
        Me.Label39.Name = "Label39"
        Me.Label39.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label39.Size = New System.Drawing.Size(61, 13)
        Me.Label39.TabIndex = 201
        Me.Label39.Text = "AD Code  :"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.BackColor = System.Drawing.SystemColors.Control
        Me.Label40.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label40.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label40.Location = New System.Drawing.Point(20, 56)
        Me.Label40.Name = "Label40"
        Me.Label40.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label40.Size = New System.Drawing.Size(87, 13)
        Me.Label40.TabIndex = 200
        Me.Label40.Text = "Credit Bank To :"
        Me.Label40.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_SSTab1_TabPage4
        '
        Me._SSTab1_TabPage4.Controls.Add(Me._FraBorder_2)
        Me._SSTab1_TabPage4.Location = New System.Drawing.Point(4, 28)
        Me._SSTab1_TabPage4.Name = "_SSTab1_TabPage4"
        Me._SSTab1_TabPage4.Size = New System.Drawing.Size(822, 533)
        Me._SSTab1_TabPage4.TabIndex = 4
        Me._SSTab1_TabPage4.Text = "Others"
        '
        '_FraBorder_2
        '
        Me._FraBorder_2.BackColor = System.Drawing.SystemColors.Control
        Me._FraBorder_2.Controls.Add(Me.txtInvoiceDigit)
        Me._FraBorder_2.Controls.Add(Me.Label2)
        Me._FraBorder_2.Controls.Add(Me.GroupBox3)
        Me._FraBorder_2.Controls.Add(Me.GroupBox2)
        Me._FraBorder_2.Controls.Add(Me.GroupBox1)
        Me._FraBorder_2.Controls.Add(Me.Frame32)
        Me._FraBorder_2.Controls.Add(Me.Frame24)
        Me._FraBorder_2.Controls.Add(Me.chkAutoProdIssue)
        Me._FraBorder_2.Controls.Add(Me.chkAutoIssue)
        Me._FraBorder_2.Controls.Add(Me.Frame4)
        Me._FraBorder_2.Controls.Add(Me.Frame3)
        Me._FraBorder_2.Controls.Add(Me.Frame1)
        Me._FraBorder_2.Controls.Add(Me.txtMRRExcessPer)
        Me._FraBorder_2.Controls.Add(Me.chkStockBal)
        Me._FraBorder_2.Controls.Add(Me.txtPDIRCrAccount)
        Me._FraBorder_2.Controls.Add(Me.txtPDIRAccount)
        Me._FraBorder_2.Controls.Add(Me.txtPDIRAmount)
        Me._FraBorder_2.Controls.Add(Me.txtAutoIssueDate)
        Me._FraBorder_2.Controls.Add(Me.txtAutoProdIssueDate)
        Me._FraBorder_2.Controls.Add(Me.Label56)
        Me._FraBorder_2.Controls.Add(Me.Label49)
        Me._FraBorder_2.Controls.Add(Me.Label20)
        Me._FraBorder_2.Controls.Add(Me.Label16)
        Me._FraBorder_2.Controls.Add(Me.Label14)
        Me._FraBorder_2.Controls.Add(Me.Label15)
        Me._FraBorder_2.Dock = System.Windows.Forms.DockStyle.Fill
        Me._FraBorder_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._FraBorder_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraBorder.SetIndex(Me._FraBorder_2, CType(2, Short))
        Me._FraBorder_2.Location = New System.Drawing.Point(0, 0)
        Me._FraBorder_2.Name = "_FraBorder_2"
        Me._FraBorder_2.Padding = New System.Windows.Forms.Padding(0)
        Me._FraBorder_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._FraBorder_2.Size = New System.Drawing.Size(822, 533)
        Me._FraBorder_2.TabIndex = 52
        Me._FraBorder_2.TabStop = False
        '
        'txtInvoiceDigit
        '
        Me.txtInvoiceDigit.AcceptsReturn = True
        Me.txtInvoiceDigit.BackColor = System.Drawing.SystemColors.Window
        Me.txtInvoiceDigit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInvoiceDigit.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInvoiceDigit.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvoiceDigit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtInvoiceDigit.Location = New System.Drawing.Point(502, 80)
        Me.txtInvoiceDigit.MaxLength = 0
        Me.txtInvoiceDigit.Name = "txtInvoiceDigit"
        Me.txtInvoiceDigit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInvoiceDigit.Size = New System.Drawing.Size(57, 22)
        Me.txtInvoiceDigit.TabIndex = 271
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(420, 82)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(77, 13)
        Me.Label2.TabIndex = 272
        Me.Label2.Text = "Invoice Digit :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox3.Controls.Add(Me.GroupBox4)
        Me.GroupBox3.Controls.Add(Me.optPrintLandScape)
        Me.GroupBox3.Controls.Add(Me.optPrintPortrait)
        Me.GroupBox3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox3.Location = New System.Drawing.Point(444, 100)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox3.Size = New System.Drawing.Size(176, 80)
        Me.GroupBox3.TabIndex = 270
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Invoice Print"
        '
        'GroupBox4
        '
        Me.GroupBox4.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox4.Controls.Add(Me.optA4)
        Me.GroupBox4.Controls.Add(Me.optA3)
        Me.GroupBox4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox4.Location = New System.Drawing.Point(1, 43)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox4.Size = New System.Drawing.Size(174, 36)
        Me.GroupBox4.TabIndex = 84
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Paper"
        '
        'optA4
        '
        Me.optA4.BackColor = System.Drawing.SystemColors.Control
        Me.optA4.Checked = True
        Me.optA4.Cursor = System.Windows.Forms.Cursors.Default
        Me.optA4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optA4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optA4.Location = New System.Drawing.Point(20, 16)
        Me.optA4.Name = "optA4"
        Me.optA4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optA4.Size = New System.Drawing.Size(54, 16)
        Me.optA4.TabIndex = 80
        Me.optA4.TabStop = True
        Me.optA4.Text = "A4"
        Me.optA4.UseVisualStyleBackColor = False
        '
        'optA3
        '
        Me.optA3.BackColor = System.Drawing.SystemColors.Control
        Me.optA3.Cursor = System.Windows.Forms.Cursors.Default
        Me.optA3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optA3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optA3.Location = New System.Drawing.Point(106, 16)
        Me.optA3.Name = "optA3"
        Me.optA3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optA3.Size = New System.Drawing.Size(54, 16)
        Me.optA3.TabIndex = 79
        Me.optA3.TabStop = True
        Me.optA3.Text = "A3"
        Me.optA3.UseVisualStyleBackColor = False
        '
        'optPrintLandScape
        '
        Me.optPrintLandScape.BackColor = System.Drawing.SystemColors.Control
        Me.optPrintLandScape.Cursor = System.Windows.Forms.Cursors.Default
        Me.optPrintLandScape.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optPrintLandScape.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPrintLandScape.Location = New System.Drawing.Point(83, 19)
        Me.optPrintLandScape.Name = "optPrintLandScape"
        Me.optPrintLandScape.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optPrintLandScape.Size = New System.Drawing.Size(85, 16)
        Me.optPrintLandScape.TabIndex = 83
        Me.optPrintLandScape.Text = "LandScape"
        Me.optPrintLandScape.UseVisualStyleBackColor = False
        '
        'optPrintPortrait
        '
        Me.optPrintPortrait.BackColor = System.Drawing.SystemColors.Control
        Me.optPrintPortrait.Checked = True
        Me.optPrintPortrait.Cursor = System.Windows.Forms.Cursors.Default
        Me.optPrintPortrait.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optPrintPortrait.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPrintPortrait.Location = New System.Drawing.Point(12, 19)
        Me.optPrintPortrait.Name = "optPrintPortrait"
        Me.optPrintPortrait.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optPrintPortrait.Size = New System.Drawing.Size(85, 16)
        Me.optPrintPortrait.TabIndex = 82
        Me.optPrintPortrait.TabStop = True
        Me.optPrintPortrait.Text = "Portrait"
        Me.optPrintPortrait.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox2.Controls.Add(Me.optYearlySeq)
        Me.GroupBox2.Controls.Add(Me.optMonthlySeq)
        Me.GroupBox2.Controls.Add(Me.optDailySeq)
        Me.GroupBox2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox2.Location = New System.Drawing.Point(446, 250)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox2.Size = New System.Drawing.Size(176, 75)
        Me.GroupBox2.TabIndex = 269
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Voucher Seq (C/B/J)"
        '
        'optYearlySeq
        '
        Me.optYearlySeq.BackColor = System.Drawing.SystemColors.Control
        Me.optYearlySeq.Checked = True
        Me.optYearlySeq.Cursor = System.Windows.Forms.Cursors.Default
        Me.optYearlySeq.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optYearlySeq.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCBJSeq.SetIndex(Me.optYearlySeq, CType(2, Short))
        Me.optYearlySeq.Location = New System.Drawing.Point(14, 55)
        Me.optYearlySeq.Name = "optYearlySeq"
        Me.optYearlySeq.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optYearlySeq.Size = New System.Drawing.Size(85, 16)
        Me.optYearlySeq.TabIndex = 83
        Me.optYearlySeq.TabStop = True
        Me.optYearlySeq.Text = "Yearly"
        Me.optYearlySeq.UseVisualStyleBackColor = False
        '
        'optMonthlySeq
        '
        Me.optMonthlySeq.BackColor = System.Drawing.SystemColors.Control
        Me.optMonthlySeq.Cursor = System.Windows.Forms.Cursors.Default
        Me.optMonthlySeq.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optMonthlySeq.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCBJSeq.SetIndex(Me.optMonthlySeq, CType(1, Short))
        Me.optMonthlySeq.Location = New System.Drawing.Point(14, 38)
        Me.optMonthlySeq.Name = "optMonthlySeq"
        Me.optMonthlySeq.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optMonthlySeq.Size = New System.Drawing.Size(85, 16)
        Me.optMonthlySeq.TabIndex = 82
        Me.optMonthlySeq.Text = "Monthly"
        Me.optMonthlySeq.UseVisualStyleBackColor = False
        '
        'optDailySeq
        '
        Me.optDailySeq.BackColor = System.Drawing.SystemColors.Control
        Me.optDailySeq.Cursor = System.Windows.Forms.Cursors.Default
        Me.optDailySeq.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optDailySeq.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCBJSeq.SetIndex(Me.optDailySeq, CType(0, Short))
        Me.optDailySeq.Location = New System.Drawing.Point(14, 21)
        Me.optDailySeq.Name = "optDailySeq"
        Me.optDailySeq.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optDailySeq.Size = New System.Drawing.Size(85, 16)
        Me.optDailySeq.TabIndex = 81
        Me.optDailySeq.Text = "Daily"
        Me.optDailySeq.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.Controls.Add(Me.chkHideHeatNo)
        Me.GroupBox1.Controls.Add(Me.chkHideBatchNo)
        Me.GroupBox1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox1.Location = New System.Drawing.Point(446, 183)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox1.Size = New System.Drawing.Size(176, 64)
        Me.GroupBox1.TabIndex = 268
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Hide Column In Inventory"
        '
        'chkHideHeatNo
        '
        Me.chkHideHeatNo.BackColor = System.Drawing.SystemColors.Control
        Me.chkHideHeatNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkHideHeatNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkHideHeatNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkHideHeatNo.Location = New System.Drawing.Point(8, 42)
        Me.chkHideHeatNo.Name = "chkHideHeatNo"
        Me.chkHideHeatNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkHideHeatNo.Size = New System.Drawing.Size(139, 16)
        Me.chkHideHeatNo.TabIndex = 156
        Me.chkHideHeatNo.Text = "Hide HeatNo"
        Me.chkHideHeatNo.UseVisualStyleBackColor = False
        '
        'chkHideBatchNo
        '
        Me.chkHideBatchNo.BackColor = System.Drawing.SystemColors.Control
        Me.chkHideBatchNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkHideBatchNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkHideBatchNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkHideBatchNo.Location = New System.Drawing.Point(7, 20)
        Me.chkHideBatchNo.Name = "chkHideBatchNo"
        Me.chkHideBatchNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkHideBatchNo.Size = New System.Drawing.Size(139, 16)
        Me.chkHideBatchNo.TabIndex = 155
        Me.chkHideBatchNo.Text = "Hide BatchNo"
        Me.chkHideBatchNo.UseVisualStyleBackColor = False
        '
        'Frame32
        '
        Me.Frame32.BackColor = System.Drawing.SystemColors.Control
        Me.Frame32.Controls.Add(Me.txtMaxBillAmount)
        Me.Frame32.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame32.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame32.Location = New System.Drawing.Point(242, 159)
        Me.Frame32.Name = "Frame32"
        Me.Frame32.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame32.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame32.Size = New System.Drawing.Size(198, 36)
        Me.Frame32.TabIndex = 267
        Me.Frame32.TabStop = False
        Me.Frame32.Text = "Max Sale Amount in Single Bill"
        '
        'txtMaxBillAmount
        '
        Me.txtMaxBillAmount.AcceptsReturn = True
        Me.txtMaxBillAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtMaxBillAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMaxBillAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMaxBillAmount.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMaxBillAmount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMaxBillAmount.Location = New System.Drawing.Point(106, 14)
        Me.txtMaxBillAmount.MaxLength = 0
        Me.txtMaxBillAmount.Name = "txtMaxBillAmount"
        Me.txtMaxBillAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMaxBillAmount.Size = New System.Drawing.Size(85, 22)
        Me.txtMaxBillAmount.TabIndex = 268
        '
        'Frame24
        '
        Me.Frame24.BackColor = System.Drawing.SystemColors.Control
        Me.Frame24.Controls.Add(Me.chkRJDespatchNote)
        Me.Frame24.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame24.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame24.Location = New System.Drawing.Point(8, 159)
        Me.Frame24.Name = "Frame24"
        Me.Frame24.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame24.Size = New System.Drawing.Size(230, 36)
        Me.Frame24.TabIndex = 154
        Me.Frame24.TabStop = False
        Me.Frame24.Text = "Auto Generated Rejection Despatch Note"
        '
        'chkRJDespatchNote
        '
        Me.chkRJDespatchNote.BackColor = System.Drawing.SystemColors.Control
        Me.chkRJDespatchNote.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkRJDespatchNote.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRJDespatchNote.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRJDespatchNote.Location = New System.Drawing.Point(50, 16)
        Me.chkRJDespatchNote.Name = "chkRJDespatchNote"
        Me.chkRJDespatchNote.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkRJDespatchNote.Size = New System.Drawing.Size(119, 16)
        Me.chkRJDespatchNote.TabIndex = 155
        Me.chkRJDespatchNote.Text = "( Yes / No )"
        Me.chkRJDespatchNote.UseVisualStyleBackColor = False
        '
        'chkAutoProdIssue
        '
        Me.chkAutoProdIssue.BackColor = System.Drawing.SystemColors.Control
        Me.chkAutoProdIssue.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAutoProdIssue.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAutoProdIssue.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAutoProdIssue.Location = New System.Drawing.Point(170, 132)
        Me.chkAutoProdIssue.Name = "chkAutoProdIssue"
        Me.chkAutoProdIssue.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAutoProdIssue.Size = New System.Drawing.Size(87, 15)
        Me.chkAutoProdIssue.TabIndex = 84
        Me.chkAutoProdIssue.Text = "(Yes / No)"
        Me.chkAutoProdIssue.UseVisualStyleBackColor = False
        '
        'chkAutoIssue
        '
        Me.chkAutoIssue.BackColor = System.Drawing.SystemColors.Control
        Me.chkAutoIssue.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAutoIssue.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAutoIssue.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAutoIssue.Location = New System.Drawing.Point(170, 108)
        Me.chkAutoIssue.Name = "chkAutoIssue"
        Me.chkAutoIssue.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAutoIssue.Size = New System.Drawing.Size(87, 16)
        Me.chkAutoIssue.TabIndex = 81
        Me.chkAutoIssue.Text = "(Yes / No)"
        Me.chkAutoIssue.UseVisualStyleBackColor = False
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me._optInvPostingType_0)
        Me.Frame4.Controls.Add(Me._optInvPostingType_1)
        Me.Frame4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(242, 292)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(195, 36)
        Me.Frame4.TabIndex = 78
        Me.Frame4.TabStop = False
        Me.Frame4.Text = "Invoice Posting Type"
        '
        '_optInvPostingType_0
        '
        Me._optInvPostingType_0.BackColor = System.Drawing.SystemColors.Control
        Me._optInvPostingType_0.Checked = True
        Me._optInvPostingType_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optInvPostingType_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optInvPostingType_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optInvPostingType.SetIndex(Me._optInvPostingType_0, CType(0, Short))
        Me._optInvPostingType_0.Location = New System.Drawing.Point(20, 16)
        Me._optInvPostingType_0.Name = "_optInvPostingType_0"
        Me._optInvPostingType_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optInvPostingType_0.Size = New System.Drawing.Size(85, 16)
        Me._optInvPostingType_0.TabIndex = 80
        Me._optInvPostingType_0.TabStop = True
        Me._optInvPostingType_0.Text = "Bill Wise"
        Me._optInvPostingType_0.UseVisualStyleBackColor = False
        '
        '_optInvPostingType_1
        '
        Me._optInvPostingType_1.BackColor = System.Drawing.SystemColors.Control
        Me._optInvPostingType_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optInvPostingType_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optInvPostingType_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optInvPostingType.SetIndex(Me._optInvPostingType_1, CType(1, Short))
        Me._optInvPostingType_1.Location = New System.Drawing.Point(106, 16)
        Me._optInvPostingType_1.Name = "_optInvPostingType_1"
        Me._optInvPostingType_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optInvPostingType_1.Size = New System.Drawing.Size(85, 16)
        Me._optInvPostingType_1.TabIndex = 79
        Me._optInvPostingType_1.TabStop = True
        Me._optInvPostingType_1.Text = "Item Wise"
        Me._optInvPostingType_1.UseVisualStyleBackColor = False
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me._optPurPostingType_1)
        Me.Frame3.Controls.Add(Me._optPurPostingType_0)
        Me.Frame3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(5, 292)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(230, 36)
        Me.Frame3.TabIndex = 75
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "Purchase Posting Type"
        '
        '_optPurPostingType_1
        '
        Me._optPurPostingType_1.BackColor = System.Drawing.SystemColors.Control
        Me._optPurPostingType_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optPurPostingType_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optPurPostingType_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPurPostingType.SetIndex(Me._optPurPostingType_1, CType(1, Short))
        Me._optPurPostingType_1.Location = New System.Drawing.Point(114, 16)
        Me._optPurPostingType_1.Name = "_optPurPostingType_1"
        Me._optPurPostingType_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optPurPostingType_1.Size = New System.Drawing.Size(85, 16)
        Me._optPurPostingType_1.TabIndex = 77
        Me._optPurPostingType_1.TabStop = True
        Me._optPurPostingType_1.Text = "Item Wise"
        Me._optPurPostingType_1.UseVisualStyleBackColor = False
        '
        '_optPurPostingType_0
        '
        Me._optPurPostingType_0.BackColor = System.Drawing.SystemColors.Control
        Me._optPurPostingType_0.Checked = True
        Me._optPurPostingType_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optPurPostingType_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optPurPostingType_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPurPostingType.SetIndex(Me._optPurPostingType_0, CType(0, Short))
        Me._optPurPostingType_0.Location = New System.Drawing.Point(20, 16)
        Me._optPurPostingType_0.Name = "_optPurPostingType_0"
        Me._optPurPostingType_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optPurPostingType_0.Size = New System.Drawing.Size(85, 16)
        Me._optPurPostingType_0.TabIndex = 76
        Me._optPurPostingType_0.TabStop = True
        Me._optPurPostingType_0.Text = "Bill Wise"
        Me._optPurPostingType_0.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.chkRateDiffCrWithGST)
        Me.Frame1.Controls.Add(Me.chkRateDiffDrWithGST)
        Me.Frame1.Controls.Add(Me.chkRejectionWithGST)
        Me.Frame1.Controls.Add(Me.chkShortageWithGST)
        Me.Frame1.Controls.Add(Me.chkRateDiffCN_App)
        Me.Frame1.Controls.Add(Me.chkRateDiffDN_App)
        Me.Frame1.Controls.Add(Me.chkRejection_App)
        Me.Frame1.Controls.Add(Me.chkShortage_App)
        Me.Frame1.Controls.Add(Me.chkRateDiffCN)
        Me.Frame1.Controls.Add(Me.chkRateDiffDN)
        Me.Frame1.Controls.Add(Me.chkRejection)
        Me.Frame1.Controls.Add(Me.chkShortage)
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(5, 198)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(435, 93)
        Me.Frame1.TabIndex = 74
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Auto Update Debit / Credit Note"
        '
        'chkRateDiffCrWithGST
        '
        Me.chkRateDiffCrWithGST.BackColor = System.Drawing.SystemColors.Control
        Me.chkRateDiffCrWithGST.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkRateDiffCrWithGST.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRateDiffCrWithGST.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRateDiffCrWithGST.Location = New System.Drawing.Point(327, 74)
        Me.chkRateDiffCrWithGST.Name = "chkRateDiffCrWithGST"
        Me.chkRateDiffCrWithGST.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkRateDiffCrWithGST.Size = New System.Drawing.Size(145, 19)
        Me.chkRateDiffCrWithGST.TabIndex = 69
        Me.chkRateDiffCrWithGST.Text = "With GST (Y / N)"
        Me.chkRateDiffCrWithGST.UseVisualStyleBackColor = False
        '
        'chkRateDiffDrWithGST
        '
        Me.chkRateDiffDrWithGST.BackColor = System.Drawing.SystemColors.Control
        Me.chkRateDiffDrWithGST.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkRateDiffDrWithGST.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRateDiffDrWithGST.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRateDiffDrWithGST.Location = New System.Drawing.Point(327, 54)
        Me.chkRateDiffDrWithGST.Name = "chkRateDiffDrWithGST"
        Me.chkRateDiffDrWithGST.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkRateDiffDrWithGST.Size = New System.Drawing.Size(145, 19)
        Me.chkRateDiffDrWithGST.TabIndex = 68
        Me.chkRateDiffDrWithGST.Text = "With GST (Y / N)"
        Me.chkRateDiffDrWithGST.UseVisualStyleBackColor = False
        '
        'chkRejectionWithGST
        '
        Me.chkRejectionWithGST.BackColor = System.Drawing.SystemColors.Control
        Me.chkRejectionWithGST.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkRejectionWithGST.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRejectionWithGST.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRejectionWithGST.Location = New System.Drawing.Point(327, 34)
        Me.chkRejectionWithGST.Name = "chkRejectionWithGST"
        Me.chkRejectionWithGST.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkRejectionWithGST.Size = New System.Drawing.Size(145, 19)
        Me.chkRejectionWithGST.TabIndex = 67
        Me.chkRejectionWithGST.Text = "With GST (Y / N)"
        Me.chkRejectionWithGST.UseVisualStyleBackColor = False
        '
        'chkShortageWithGST
        '
        Me.chkShortageWithGST.BackColor = System.Drawing.SystemColors.Control
        Me.chkShortageWithGST.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkShortageWithGST.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShortageWithGST.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkShortageWithGST.Location = New System.Drawing.Point(327, 16)
        Me.chkShortageWithGST.Name = "chkShortageWithGST"
        Me.chkShortageWithGST.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkShortageWithGST.Size = New System.Drawing.Size(145, 19)
        Me.chkShortageWithGST.TabIndex = 66
        Me.chkShortageWithGST.Text = "With GST (Y / N)"
        Me.chkShortageWithGST.UseVisualStyleBackColor = False
        '
        'chkRateDiffCN_App
        '
        Me.chkRateDiffCN_App.BackColor = System.Drawing.SystemColors.Control
        Me.chkRateDiffCN_App.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkRateDiffCN_App.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRateDiffCN_App.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRateDiffCN_App.Location = New System.Drawing.Point(186, 74)
        Me.chkRateDiffCN_App.Name = "chkRateDiffCN_App"
        Me.chkRateDiffCN_App.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkRateDiffCN_App.Size = New System.Drawing.Size(145, 19)
        Me.chkRateDiffCN_App.TabIndex = 65
        Me.chkRateDiffCN_App.Text = "Approved (Yes / No)"
        Me.chkRateDiffCN_App.UseVisualStyleBackColor = False
        '
        'chkRateDiffDN_App
        '
        Me.chkRateDiffDN_App.BackColor = System.Drawing.SystemColors.Control
        Me.chkRateDiffDN_App.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkRateDiffDN_App.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRateDiffDN_App.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRateDiffDN_App.Location = New System.Drawing.Point(186, 54)
        Me.chkRateDiffDN_App.Name = "chkRateDiffDN_App"
        Me.chkRateDiffDN_App.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkRateDiffDN_App.Size = New System.Drawing.Size(145, 19)
        Me.chkRateDiffDN_App.TabIndex = 63
        Me.chkRateDiffDN_App.Text = "Approved (Yes / No)"
        Me.chkRateDiffDN_App.UseVisualStyleBackColor = False
        '
        'chkRejection_App
        '
        Me.chkRejection_App.BackColor = System.Drawing.SystemColors.Control
        Me.chkRejection_App.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkRejection_App.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRejection_App.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRejection_App.Location = New System.Drawing.Point(186, 34)
        Me.chkRejection_App.Name = "chkRejection_App"
        Me.chkRejection_App.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkRejection_App.Size = New System.Drawing.Size(145, 19)
        Me.chkRejection_App.TabIndex = 61
        Me.chkRejection_App.Text = "Approved (Yes / No)"
        Me.chkRejection_App.UseVisualStyleBackColor = False
        '
        'chkShortage_App
        '
        Me.chkShortage_App.BackColor = System.Drawing.SystemColors.Control
        Me.chkShortage_App.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkShortage_App.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShortage_App.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkShortage_App.Location = New System.Drawing.Point(186, 16)
        Me.chkShortage_App.Name = "chkShortage_App"
        Me.chkShortage_App.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkShortage_App.Size = New System.Drawing.Size(145, 19)
        Me.chkShortage_App.TabIndex = 59
        Me.chkShortage_App.Text = "Approved (Yes / No)"
        Me.chkShortage_App.UseVisualStyleBackColor = False
        '
        'chkRateDiffCN
        '
        Me.chkRateDiffCN.BackColor = System.Drawing.SystemColors.Control
        Me.chkRateDiffCN.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkRateDiffCN.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRateDiffCN.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRateDiffCN.Location = New System.Drawing.Point(50, 74)
        Me.chkRateDiffCN.Name = "chkRateDiffCN"
        Me.chkRateDiffCN.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkRateDiffCN.Size = New System.Drawing.Size(149, 15)
        Me.chkRateDiffCN.TabIndex = 64
        Me.chkRateDiffCN.Text = "Rate Diff Credit Note"
        Me.chkRateDiffCN.UseVisualStyleBackColor = False
        '
        'chkRateDiffDN
        '
        Me.chkRateDiffDN.BackColor = System.Drawing.SystemColors.Control
        Me.chkRateDiffDN.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkRateDiffDN.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRateDiffDN.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRateDiffDN.Location = New System.Drawing.Point(50, 54)
        Me.chkRateDiffDN.Name = "chkRateDiffDN"
        Me.chkRateDiffDN.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkRateDiffDN.Size = New System.Drawing.Size(149, 17)
        Me.chkRateDiffDN.TabIndex = 62
        Me.chkRateDiffDN.Text = "Rate Diff Debit Note"
        Me.chkRateDiffDN.UseVisualStyleBackColor = False
        '
        'chkRejection
        '
        Me.chkRejection.BackColor = System.Drawing.SystemColors.Control
        Me.chkRejection.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkRejection.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRejection.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRejection.Location = New System.Drawing.Point(50, 34)
        Me.chkRejection.Name = "chkRejection"
        Me.chkRejection.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkRejection.Size = New System.Drawing.Size(141, 19)
        Me.chkRejection.TabIndex = 60
        Me.chkRejection.Text = "Rejection"
        Me.chkRejection.UseVisualStyleBackColor = False
        '
        'chkShortage
        '
        Me.chkShortage.BackColor = System.Drawing.SystemColors.Control
        Me.chkShortage.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkShortage.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShortage.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkShortage.Location = New System.Drawing.Point(50, 16)
        Me.chkShortage.Name = "chkShortage"
        Me.chkShortage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkShortage.Size = New System.Drawing.Size(145, 19)
        Me.chkShortage.TabIndex = 58
        Me.chkShortage.Text = "Shortage"
        Me.chkShortage.UseVisualStyleBackColor = False
        '
        'txtMRRExcessPer
        '
        Me.txtMRRExcessPer.AcceptsReturn = True
        Me.txtMRRExcessPer.BackColor = System.Drawing.SystemColors.Window
        Me.txtMRRExcessPer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMRRExcessPer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMRRExcessPer.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMRRExcessPer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMRRExcessPer.Location = New System.Drawing.Point(168, 80)
        Me.txtMRRExcessPer.MaxLength = 0
        Me.txtMRRExcessPer.Name = "txtMRRExcessPer"
        Me.txtMRRExcessPer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMRRExcessPer.Size = New System.Drawing.Size(57, 22)
        Me.txtMRRExcessPer.TabIndex = 57
        '
        'chkStockBal
        '
        Me.chkStockBal.BackColor = System.Drawing.SystemColors.Control
        Me.chkStockBal.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkStockBal.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkStockBal.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkStockBal.Location = New System.Drawing.Point(230, 82)
        Me.chkStockBal.Name = "chkStockBal"
        Me.chkStockBal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkStockBal.Size = New System.Drawing.Size(196, 17)
        Me.chkStockBal.TabIndex = 56
        Me.chkStockBal.Text = "Stock Balance Check  ( Yes / No )"
        Me.chkStockBal.UseVisualStyleBackColor = False
        '
        'txtPDIRCrAccount
        '
        Me.txtPDIRCrAccount.AcceptsReturn = True
        Me.txtPDIRCrAccount.BackColor = System.Drawing.SystemColors.Window
        Me.txtPDIRCrAccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPDIRCrAccount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPDIRCrAccount.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPDIRCrAccount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPDIRCrAccount.Location = New System.Drawing.Point(168, 36)
        Me.txtPDIRCrAccount.MaxLength = 0
        Me.txtPDIRCrAccount.Name = "txtPDIRCrAccount"
        Me.txtPDIRCrAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPDIRCrAccount.Size = New System.Drawing.Size(391, 22)
        Me.txtPDIRCrAccount.TabIndex = 54
        '
        'txtPDIRAccount
        '
        Me.txtPDIRAccount.AcceptsReturn = True
        Me.txtPDIRAccount.BackColor = System.Drawing.SystemColors.Window
        Me.txtPDIRAccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPDIRAccount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPDIRAccount.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPDIRAccount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPDIRAccount.Location = New System.Drawing.Point(168, 14)
        Me.txtPDIRAccount.MaxLength = 0
        Me.txtPDIRAccount.Name = "txtPDIRAccount"
        Me.txtPDIRAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPDIRAccount.Size = New System.Drawing.Size(391, 22)
        Me.txtPDIRAccount.TabIndex = 53
        '
        'txtPDIRAmount
        '
        Me.txtPDIRAmount.AcceptsReturn = True
        Me.txtPDIRAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtPDIRAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPDIRAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPDIRAmount.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPDIRAmount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPDIRAmount.Location = New System.Drawing.Point(168, 58)
        Me.txtPDIRAmount.MaxLength = 0
        Me.txtPDIRAmount.Name = "txtPDIRAmount"
        Me.txtPDIRAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPDIRAmount.Size = New System.Drawing.Size(391, 22)
        Me.txtPDIRAmount.TabIndex = 55
        '
        'txtAutoIssueDate
        '
        Me.txtAutoIssueDate.AllowPromptAsInput = False
        Me.txtAutoIssueDate.Enabled = False
        Me.txtAutoIssueDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAutoIssueDate.Location = New System.Drawing.Point(268, 104)
        Me.txtAutoIssueDate.Mask = "##/##/####"
        Me.txtAutoIssueDate.Name = "txtAutoIssueDate"
        Me.txtAutoIssueDate.Size = New System.Drawing.Size(89, 22)
        Me.txtAutoIssueDate.TabIndex = 83
        '
        'txtAutoProdIssueDate
        '
        Me.txtAutoProdIssueDate.AllowPromptAsInput = False
        Me.txtAutoProdIssueDate.Enabled = False
        Me.txtAutoProdIssueDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAutoProdIssueDate.Location = New System.Drawing.Point(268, 128)
        Me.txtAutoProdIssueDate.Mask = "##/##/####"
        Me.txtAutoProdIssueDate.Name = "txtAutoProdIssueDate"
        Me.txtAutoProdIssueDate.Size = New System.Drawing.Size(89, 22)
        Me.txtAutoProdIssueDate.TabIndex = 85
        '
        'Label56
        '
        Me.Label56.BackColor = System.Drawing.SystemColors.Control
        Me.Label56.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label56.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label56.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label56.Location = New System.Drawing.Point(6, 130)
        Me.Label56.Name = "Label56"
        Me.Label56.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label56.Size = New System.Drawing.Size(160, 15)
        Me.Label56.TabIndex = 86
        Me.Label56.Text = "Issue From Production Slip:"
        Me.Label56.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label49
        '
        Me.Label49.BackColor = System.Drawing.SystemColors.Control
        Me.Label49.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label49.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label49.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label49.Location = New System.Drawing.Point(6, 106)
        Me.Label49.Name = "Label49"
        Me.Label49.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label49.Size = New System.Drawing.Size(160, 15)
        Me.Label49.TabIndex = 82
        Me.Label49.Text = "Issue From Production :"
        Me.Label49.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(6, 82)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(160, 15)
        Me.Label20.TabIndex = 70
        Me.Label20.Text = "Excess % of PO Qty in MRR :"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(6, 38)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(160, 15)
        Me.Label16.TabIndex = 68
        Me.Label16.Text = "PDIR Credit Account :"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(6, 16)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(160, 15)
        Me.Label14.TabIndex = 67
        Me.Label14.Text = "PDIR Exp Account :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(6, 60)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(160, 15)
        Me.Label15.TabIndex = 66
        Me.Label15.Text = "PDIR Deduct Amount :"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_SSTab1_TabPage6
        '
        Me._SSTab1_TabPage6.Controls.Add(Me._FraBorder_6)
        Me._SSTab1_TabPage6.Location = New System.Drawing.Point(4, 28)
        Me._SSTab1_TabPage6.Name = "_SSTab1_TabPage6"
        Me._SSTab1_TabPage6.Size = New System.Drawing.Size(822, 533)
        Me._SSTab1_TabPage6.TabIndex = 6
        Me._SSTab1_TabPage6.Text = "Carry Forward Option"
        '
        '_FraBorder_6
        '
        Me._FraBorder_6.BackColor = System.Drawing.SystemColors.Control
        Me._FraBorder_6.Controls.Add(Me.SprdMain)
        Me._FraBorder_6.Dock = System.Windows.Forms.DockStyle.Fill
        Me._FraBorder_6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._FraBorder_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraBorder.SetIndex(Me._FraBorder_6, CType(6, Short))
        Me._FraBorder_6.Location = New System.Drawing.Point(0, 0)
        Me._FraBorder_6.Name = "_FraBorder_6"
        Me._FraBorder_6.Padding = New System.Windows.Forms.Padding(0)
        Me._FraBorder_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._FraBorder_6.Size = New System.Drawing.Size(822, 533)
        Me._FraBorder_6.TabIndex = 224
        Me._FraBorder_6.TabStop = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SprdMain.Location = New System.Drawing.Point(0, 15)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(822, 518)
        Me.SprdMain.TabIndex = 225
        '
        '_SSTab1_TabPage7
        '
        Me._SSTab1_TabPage7.Controls.Add(Me._FraBorder_9)
        Me._SSTab1_TabPage7.Location = New System.Drawing.Point(4, 28)
        Me._SSTab1_TabPage7.Name = "_SSTab1_TabPage7"
        Me._SSTab1_TabPage7.Size = New System.Drawing.Size(822, 533)
        Me._SSTab1_TabPage7.TabIndex = 7
        Me._SSTab1_TabPage7.Text = "Parameter"
        '
        '_FraBorder_9
        '
        Me._FraBorder_9.BackColor = System.Drawing.SystemColors.Control
        Me._FraBorder_9.Controls.Add(Me.chkCreditLimit)
        Me._FraBorder_9.Controls.Add(Me.Frame34)
        Me._FraBorder_9.Controls.Add(Me.Frame33)
        Me._FraBorder_9.Controls.Add(Me.Frame31)
        Me._FraBorder_9.Controls.Add(Me.Frame30)
        Me._FraBorder_9.Controls.Add(Me.txtQCDays)
        Me._FraBorder_9.Controls.Add(Me.chkMaxInvInGate)
        Me._FraBorder_9.Controls.Add(Me.Frame25)
        Me._FraBorder_9.Controls.Add(Me.Frame26)
        Me._FraBorder_9.Controls.Add(Me.Frame27)
        Me._FraBorder_9.Controls.Add(Me.Frame28)
        Me._FraBorder_9.Controls.Add(Me.Frame29)
        Me._FraBorder_9.Controls.Add(Me._Label12_4)
        Me._FraBorder_9.Controls.Add(Me.Label66)
        Me._FraBorder_9.Controls.Add(Me.Label67)
        Me._FraBorder_9.Dock = System.Windows.Forms.DockStyle.Fill
        Me._FraBorder_9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._FraBorder_9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraBorder.SetIndex(Me._FraBorder_9, CType(9, Short))
        Me._FraBorder_9.Location = New System.Drawing.Point(0, 0)
        Me._FraBorder_9.Name = "_FraBorder_9"
        Me._FraBorder_9.Padding = New System.Windows.Forms.Padding(0)
        Me._FraBorder_9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._FraBorder_9.Size = New System.Drawing.Size(822, 533)
        Me._FraBorder_9.TabIndex = 226
        Me._FraBorder_9.TabStop = False
        '
        'chkCreditLimit
        '
        Me.chkCreditLimit.BackColor = System.Drawing.SystemColors.Control
        Me.chkCreditLimit.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCreditLimit.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCreditLimit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCreditLimit.Location = New System.Drawing.Point(358, 10)
        Me.chkCreditLimit.Name = "chkCreditLimit"
        Me.chkCreditLimit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCreditLimit.Size = New System.Drawing.Size(263, 17)
        Me.chkCreditLimit.TabIndex = 272
        Me.chkCreditLimit.Text = "Credit Limit Check (Yes / No)"
        Me.chkCreditLimit.UseVisualStyleBackColor = False
        '
        'Frame34
        '
        Me.Frame34.BackColor = System.Drawing.SystemColors.Control
        Me.Frame34.Controls.Add(Me.txtPendingIndentNo)
        Me.Frame34.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame34.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame34.Location = New System.Drawing.Point(330, 262)
        Me.Frame34.Name = "Frame34"
        Me.Frame34.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame34.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame34.Size = New System.Drawing.Size(273, 47)
        Me.Frame34.TabIndex = 271
        Me.Frame34.TabStop = False
        Me.Frame34.Text = "Indent Lock if pending more than Nos (0 for Unlimited)"
        '
        'txtPendingIndentNo
        '
        Me.txtPendingIndentNo.AcceptsReturn = True
        Me.txtPendingIndentNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPendingIndentNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPendingIndentNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPendingIndentNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPendingIndentNo.ForeColor = System.Drawing.Color.Blue
        Me.txtPendingIndentNo.Location = New System.Drawing.Point(106, 18)
        Me.txtPendingIndentNo.MaxLength = 5
        Me.txtPendingIndentNo.Name = "txtPendingIndentNo"
        Me.txtPendingIndentNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPendingIndentNo.Size = New System.Drawing.Size(45, 22)
        Me.txtPendingIndentNo.TabIndex = 272
        '
        'Frame33
        '
        Me.Frame33.BackColor = System.Drawing.SystemColors.Control
        Me.Frame33.Controls.Add(Me.txtMaxPOItems)
        Me.Frame33.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame33.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame33.Location = New System.Drawing.Point(330, 214)
        Me.Frame33.Name = "Frame33"
        Me.Frame33.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame33.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame33.Size = New System.Drawing.Size(273, 47)
        Me.Frame33.TabIndex = 269
        Me.Frame33.TabStop = False
        Me.Frame33.Text = "Max PO Items (0 for Unlimited)"
        '
        'txtMaxPOItems
        '
        Me.txtMaxPOItems.AcceptsReturn = True
        Me.txtMaxPOItems.BackColor = System.Drawing.SystemColors.Window
        Me.txtMaxPOItems.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMaxPOItems.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMaxPOItems.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMaxPOItems.ForeColor = System.Drawing.Color.Blue
        Me.txtMaxPOItems.Location = New System.Drawing.Point(106, 18)
        Me.txtMaxPOItems.MaxLength = 5
        Me.txtMaxPOItems.Name = "txtMaxPOItems"
        Me.txtMaxPOItems.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMaxPOItems.Size = New System.Drawing.Size(45, 22)
        Me.txtMaxPOItems.TabIndex = 270
        '
        'Frame31
        '
        Me.Frame31.BackColor = System.Drawing.SystemColors.Control
        Me.Frame31.Controls.Add(Me._optSOLocking_0)
        Me.Frame31.Controls.Add(Me._optSOLocking_1)
        Me.Frame31.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame31.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame31.Location = New System.Drawing.Point(330, 166)
        Me.Frame31.Name = "Frame31"
        Me.Frame31.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame31.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame31.Size = New System.Drawing.Size(273, 47)
        Me.Frame31.TabIndex = 264
        Me.Frame31.TabStop = False
        Me.Frame31.Text = "Sales Order Locking with Costing"
        '
        '_optSOLocking_0
        '
        Me._optSOLocking_0.BackColor = System.Drawing.SystemColors.Control
        Me._optSOLocking_0.Checked = True
        Me._optSOLocking_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optSOLocking_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optSOLocking_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optSOLocking.SetIndex(Me._optSOLocking_0, CType(0, Short))
        Me._optSOLocking_0.Location = New System.Drawing.Point(14, 20)
        Me._optSOLocking_0.Name = "_optSOLocking_0"
        Me._optSOLocking_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optSOLocking_0.Size = New System.Drawing.Size(53, 16)
        Me._optSOLocking_0.TabIndex = 266
        Me._optSOLocking_0.TabStop = True
        Me._optSOLocking_0.Text = "Yes"
        Me._optSOLocking_0.UseVisualStyleBackColor = False
        '
        '_optSOLocking_1
        '
        Me._optSOLocking_1.BackColor = System.Drawing.SystemColors.Control
        Me._optSOLocking_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optSOLocking_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optSOLocking_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optSOLocking.SetIndex(Me._optSOLocking_1, CType(1, Short))
        Me._optSOLocking_1.Location = New System.Drawing.Point(106, 20)
        Me._optSOLocking_1.Name = "_optSOLocking_1"
        Me._optSOLocking_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optSOLocking_1.Size = New System.Drawing.Size(53, 15)
        Me._optSOLocking_1.TabIndex = 265
        Me._optSOLocking_1.TabStop = True
        Me._optSOLocking_1.Text = "No"
        Me._optSOLocking_1.UseVisualStyleBackColor = False
        '
        'Frame30
        '
        Me.Frame30.BackColor = System.Drawing.SystemColors.Control
        Me.Frame30.Controls.Add(Me._optPOLocking_1)
        Me.Frame30.Controls.Add(Me._optPOLocking_0)
        Me.Frame30.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame30.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame30.Location = New System.Drawing.Point(328, 118)
        Me.Frame30.Name = "Frame30"
        Me.Frame30.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame30.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame30.Size = New System.Drawing.Size(277, 47)
        Me.Frame30.TabIndex = 260
        Me.Frame30.TabStop = False
        Me.Frame30.Text = "Purchase Order Locking with Costing"
        '
        '_optPOLocking_1
        '
        Me._optPOLocking_1.BackColor = System.Drawing.SystemColors.Control
        Me._optPOLocking_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optPOLocking_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optPOLocking_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPOLocking.SetIndex(Me._optPOLocking_1, CType(1, Short))
        Me._optPOLocking_1.Location = New System.Drawing.Point(106, 20)
        Me._optPOLocking_1.Name = "_optPOLocking_1"
        Me._optPOLocking_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optPOLocking_1.Size = New System.Drawing.Size(53, 15)
        Me._optPOLocking_1.TabIndex = 262
        Me._optPOLocking_1.TabStop = True
        Me._optPOLocking_1.Text = "No"
        Me._optPOLocking_1.UseVisualStyleBackColor = False
        '
        '_optPOLocking_0
        '
        Me._optPOLocking_0.BackColor = System.Drawing.SystemColors.Control
        Me._optPOLocking_0.Checked = True
        Me._optPOLocking_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optPOLocking_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optPOLocking_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPOLocking.SetIndex(Me._optPOLocking_0, CType(0, Short))
        Me._optPOLocking_0.Location = New System.Drawing.Point(14, 20)
        Me._optPOLocking_0.Name = "_optPOLocking_0"
        Me._optPOLocking_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optPOLocking_0.Size = New System.Drawing.Size(53, 17)
        Me._optPOLocking_0.TabIndex = 261
        Me._optPOLocking_0.TabStop = True
        Me._optPOLocking_0.Text = "Yes"
        Me._optPOLocking_0.UseVisualStyleBackColor = False
        '
        'txtQCDays
        '
        Me.txtQCDays.AcceptsReturn = True
        Me.txtQCDays.BackColor = System.Drawing.SystemColors.Window
        Me.txtQCDays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtQCDays.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtQCDays.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtQCDays.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtQCDays.Location = New System.Drawing.Point(244, 14)
        Me.txtQCDays.MaxLength = 0
        Me.txtQCDays.Name = "txtQCDays"
        Me.txtQCDays.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtQCDays.Size = New System.Drawing.Size(25, 22)
        Me.txtQCDays.TabIndex = 251
        '
        'chkMaxInvInGate
        '
        Me.chkMaxInvInGate.BackColor = System.Drawing.SystemColors.Control
        Me.chkMaxInvInGate.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkMaxInvInGate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMaxInvInGate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkMaxInvInGate.Location = New System.Drawing.Point(246, 36)
        Me.chkMaxInvInGate.Name = "chkMaxInvInGate"
        Me.chkMaxInvInGate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkMaxInvInGate.Size = New System.Drawing.Size(83, 20)
        Me.chkMaxInvInGate.TabIndex = 250
        Me.chkMaxInvInGate.Text = "(Yes / No)"
        Me.chkMaxInvInGate.UseVisualStyleBackColor = False
        '
        'Frame25
        '
        Me.Frame25.BackColor = System.Drawing.SystemColors.Control
        Me.Frame25.Controls.Add(Me._optInvGen_0)
        Me.Frame25.Controls.Add(Me._optInvGen_1)
        Me.Frame25.Controls.Add(Me.txtInvTmFrom)
        Me.Frame25.Controls.Add(Me.txtInvTmTo)
        Me.Frame25.Controls.Add(Me.Label68)
        Me.Frame25.Controls.Add(Me.Label71)
        Me.Frame25.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame25.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame25.Location = New System.Drawing.Point(0, 58)
        Me.Frame25.Name = "Frame25"
        Me.Frame25.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame25.Size = New System.Drawing.Size(327, 59)
        Me.Frame25.TabIndex = 243
        Me.Frame25.TabStop = False
        Me.Frame25.Text = "24 Hours Invoice Generate"
        '
        '_optInvGen_0
        '
        Me._optInvGen_0.BackColor = System.Drawing.SystemColors.Control
        Me._optInvGen_0.Checked = True
        Me._optInvGen_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optInvGen_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optInvGen_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optInvGen.SetIndex(Me._optInvGen_0, CType(0, Short))
        Me._optInvGen_0.Location = New System.Drawing.Point(130, 16)
        Me._optInvGen_0.Name = "_optInvGen_0"
        Me._optInvGen_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optInvGen_0.Size = New System.Drawing.Size(53, 16)
        Me._optInvGen_0.TabIndex = 247
        Me._optInvGen_0.TabStop = True
        Me._optInvGen_0.Text = "Yes"
        Me._optInvGen_0.UseVisualStyleBackColor = False
        '
        '_optInvGen_1
        '
        Me._optInvGen_1.BackColor = System.Drawing.SystemColors.Control
        Me._optInvGen_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optInvGen_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optInvGen_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optInvGen.SetIndex(Me._optInvGen_1, CType(1, Short))
        Me._optInvGen_1.Location = New System.Drawing.Point(222, 15)
        Me._optInvGen_1.Name = "_optInvGen_1"
        Me._optInvGen_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optInvGen_1.Size = New System.Drawing.Size(53, 16)
        Me._optInvGen_1.TabIndex = 246
        Me._optInvGen_1.TabStop = True
        Me._optInvGen_1.Text = "No"
        Me._optInvGen_1.UseVisualStyleBackColor = False
        '
        'txtInvTmFrom
        '
        Me.txtInvTmFrom.AcceptsReturn = True
        Me.txtInvTmFrom.BackColor = System.Drawing.SystemColors.Window
        Me.txtInvTmFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInvTmFrom.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInvTmFrom.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvTmFrom.ForeColor = System.Drawing.Color.Blue
        Me.txtInvTmFrom.Location = New System.Drawing.Point(130, 34)
        Me.txtInvTmFrom.MaxLength = 5
        Me.txtInvTmFrom.Name = "txtInvTmFrom"
        Me.txtInvTmFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInvTmFrom.Size = New System.Drawing.Size(31, 22)
        Me.txtInvTmFrom.TabIndex = 245
        '
        'txtInvTmTo
        '
        Me.txtInvTmTo.AcceptsReturn = True
        Me.txtInvTmTo.BackColor = System.Drawing.SystemColors.Window
        Me.txtInvTmTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInvTmTo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInvTmTo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvTmTo.ForeColor = System.Drawing.Color.Blue
        Me.txtInvTmTo.Location = New System.Drawing.Point(222, 34)
        Me.txtInvTmTo.MaxLength = 5
        Me.txtInvTmTo.Name = "txtInvTmTo"
        Me.txtInvTmTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInvTmTo.Size = New System.Drawing.Size(31, 22)
        Me.txtInvTmTo.TabIndex = 244
        '
        'Label68
        '
        Me.Label68.AutoSize = True
        Me.Label68.BackColor = System.Drawing.SystemColors.Control
        Me.Label68.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label68.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label68.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label68.Location = New System.Drawing.Point(92, 36)
        Me.Label68.Name = "Label68"
        Me.Label68.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label68.Size = New System.Drawing.Size(40, 13)
        Me.Label68.TabIndex = 249
        Me.Label68.Text = "From :"
        '
        'Label71
        '
        Me.Label71.AutoSize = True
        Me.Label71.BackColor = System.Drawing.SystemColors.Control
        Me.Label71.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label71.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label71.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label71.Location = New System.Drawing.Point(194, 36)
        Me.Label71.Name = "Label71"
        Me.Label71.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label71.Size = New System.Drawing.Size(25, 13)
        Me.Label71.TabIndex = 248
        Me.Label71.Text = "To :"
        '
        'Frame26
        '
        Me.Frame26.BackColor = System.Drawing.SystemColors.Control
        Me.Frame26.Controls.Add(Me._optLoadingSlip_1)
        Me.Frame26.Controls.Add(Me._optLoadingSlip_0)
        Me.Frame26.Controls.Add(Me.txtLoadingAppDate)
        Me.Frame26.Controls.Add(Me.Label72)
        Me.Frame26.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame26.Location = New System.Drawing.Point(-2, 118)
        Me.Frame26.Name = "Frame26"
        Me.Frame26.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame26.Size = New System.Drawing.Size(329, 61)
        Me.Frame26.TabIndex = 238
        Me.Frame26.TabStop = False
        Me.Frame26.Text = "Loading Slip Required"
        '
        '_optLoadingSlip_1
        '
        Me._optLoadingSlip_1.BackColor = System.Drawing.SystemColors.Control
        Me._optLoadingSlip_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optLoadingSlip_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optLoadingSlip_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optLoadingSlip.SetIndex(Me._optLoadingSlip_1, CType(1, Short))
        Me._optLoadingSlip_1.Location = New System.Drawing.Point(222, 12)
        Me._optLoadingSlip_1.Name = "_optLoadingSlip_1"
        Me._optLoadingSlip_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optLoadingSlip_1.Size = New System.Drawing.Size(53, 20)
        Me._optLoadingSlip_1.TabIndex = 240
        Me._optLoadingSlip_1.TabStop = True
        Me._optLoadingSlip_1.Text = "No"
        Me._optLoadingSlip_1.UseVisualStyleBackColor = False
        '
        '_optLoadingSlip_0
        '
        Me._optLoadingSlip_0.BackColor = System.Drawing.SystemColors.Control
        Me._optLoadingSlip_0.Checked = True
        Me._optLoadingSlip_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optLoadingSlip_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optLoadingSlip_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optLoadingSlip.SetIndex(Me._optLoadingSlip_0, CType(0, Short))
        Me._optLoadingSlip_0.Location = New System.Drawing.Point(130, 12)
        Me._optLoadingSlip_0.Name = "_optLoadingSlip_0"
        Me._optLoadingSlip_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optLoadingSlip_0.Size = New System.Drawing.Size(53, 20)
        Me._optLoadingSlip_0.TabIndex = 239
        Me._optLoadingSlip_0.TabStop = True
        Me._optLoadingSlip_0.Text = "Yes"
        Me._optLoadingSlip_0.UseVisualStyleBackColor = False
        '
        'txtLoadingAppDate
        '
        Me.txtLoadingAppDate.AllowPromptAsInput = False
        Me.txtLoadingAppDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLoadingAppDate.Location = New System.Drawing.Point(130, 34)
        Me.txtLoadingAppDate.Mask = "##/##/####"
        Me.txtLoadingAppDate.Name = "txtLoadingAppDate"
        Me.txtLoadingAppDate.Size = New System.Drawing.Size(89, 22)
        Me.txtLoadingAppDate.TabIndex = 241
        '
        'Label72
        '
        Me.Label72.AutoSize = True
        Me.Label72.BackColor = System.Drawing.SystemColors.Control
        Me.Label72.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label72.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label72.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label72.Location = New System.Drawing.Point(30, 38)
        Me.Label72.Name = "Label72"
        Me.Label72.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label72.Size = New System.Drawing.Size(97, 13)
        Me.Label72.TabIndex = 242
        Me.Label72.Text = "Applicable From :"
        '
        'Frame27
        '
        Me.Frame27.BackColor = System.Drawing.SystemColors.Control
        Me.Frame27.Controls.Add(Me._optProductionPlanLocking_0)
        Me.Frame27.Controls.Add(Me._optProductionPlanLocking_1)
        Me.Frame27.Controls.Add(Me.txtPlanLockDays)
        Me.Frame27.Controls.Add(Me.Label73)
        Me.Frame27.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame27.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame27.Location = New System.Drawing.Point(0, 178)
        Me.Frame27.Name = "Frame27"
        Me.Frame27.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame27.Size = New System.Drawing.Size(329, 61)
        Me.Frame27.TabIndex = 233
        Me.Frame27.TabStop = False
        Me.Frame27.Text = "Production Plan Locking"
        '
        '_optProductionPlanLocking_0
        '
        Me._optProductionPlanLocking_0.AutoSize = True
        Me._optProductionPlanLocking_0.BackColor = System.Drawing.SystemColors.Control
        Me._optProductionPlanLocking_0.Checked = True
        Me._optProductionPlanLocking_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optProductionPlanLocking_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optProductionPlanLocking_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optProductionPlanLocking.SetIndex(Me._optProductionPlanLocking_0, CType(0, Short))
        Me._optProductionPlanLocking_0.Location = New System.Drawing.Point(130, 16)
        Me._optProductionPlanLocking_0.Name = "_optProductionPlanLocking_0"
        Me._optProductionPlanLocking_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optProductionPlanLocking_0.Size = New System.Drawing.Size(41, 17)
        Me._optProductionPlanLocking_0.TabIndex = 236
        Me._optProductionPlanLocking_0.TabStop = True
        Me._optProductionPlanLocking_0.Text = "Yes"
        Me._optProductionPlanLocking_0.UseVisualStyleBackColor = False
        '
        '_optProductionPlanLocking_1
        '
        Me._optProductionPlanLocking_1.AutoSize = True
        Me._optProductionPlanLocking_1.BackColor = System.Drawing.SystemColors.Control
        Me._optProductionPlanLocking_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optProductionPlanLocking_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optProductionPlanLocking_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optProductionPlanLocking.SetIndex(Me._optProductionPlanLocking_1, CType(1, Short))
        Me._optProductionPlanLocking_1.Location = New System.Drawing.Point(222, 16)
        Me._optProductionPlanLocking_1.Name = "_optProductionPlanLocking_1"
        Me._optProductionPlanLocking_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optProductionPlanLocking_1.Size = New System.Drawing.Size(40, 17)
        Me._optProductionPlanLocking_1.TabIndex = 235
        Me._optProductionPlanLocking_1.TabStop = True
        Me._optProductionPlanLocking_1.Text = "No"
        Me._optProductionPlanLocking_1.UseVisualStyleBackColor = False
        '
        'txtPlanLockDays
        '
        Me.txtPlanLockDays.AcceptsReturn = True
        Me.txtPlanLockDays.BackColor = System.Drawing.SystemColors.Window
        Me.txtPlanLockDays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPlanLockDays.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPlanLockDays.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPlanLockDays.ForeColor = System.Drawing.Color.Blue
        Me.txtPlanLockDays.Location = New System.Drawing.Point(130, 34)
        Me.txtPlanLockDays.MaxLength = 5
        Me.txtPlanLockDays.Name = "txtPlanLockDays"
        Me.txtPlanLockDays.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPlanLockDays.Size = New System.Drawing.Size(31, 22)
        Me.txtPlanLockDays.TabIndex = 234
        '
        'Label73
        '
        Me.Label73.AutoSize = True
        Me.Label73.BackColor = System.Drawing.SystemColors.Control
        Me.Label73.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label73.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label73.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label73.Location = New System.Drawing.Point(48, 36)
        Me.Label73.Name = "Label73"
        Me.Label73.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label73.Size = New System.Drawing.Size(75, 13)
        Me.Label73.TabIndex = 237
        Me.Label73.Text = "Days Before :"
        '
        'Frame28
        '
        Me.Frame28.BackColor = System.Drawing.SystemColors.Control
        Me.Frame28.Controls.Add(Me.chkPOPrintApproval)
        Me.Frame28.Controls.Add(Me._optPOPrint_1)
        Me.Frame28.Controls.Add(Me._optPOPrint_0)
        Me.Frame28.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame28.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame28.Location = New System.Drawing.Point(0, 240)
        Me.Frame28.Name = "Frame28"
        Me.Frame28.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame28.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame28.Size = New System.Drawing.Size(329, 71)
        Me.Frame28.TabIndex = 230
        Me.Frame28.TabStop = False
        Me.Frame28.Text = "Purchase Order Printing"
        '
        'chkPOPrintApproval
        '
        Me.chkPOPrintApproval.BackColor = System.Drawing.SystemColors.Control
        Me.chkPOPrintApproval.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPOPrintApproval.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPOPrintApproval.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPOPrintApproval.Location = New System.Drawing.Point(48, 45)
        Me.chkPOPrintApproval.Name = "chkPOPrintApproval"
        Me.chkPOPrintApproval.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPOPrintApproval.Size = New System.Drawing.Size(263, 17)
        Me.chkPOPrintApproval.TabIndex = 263
        Me.chkPOPrintApproval.Text = "Approval Must for PO Printing (Yes / No)"
        Me.chkPOPrintApproval.UseVisualStyleBackColor = False
        '
        '_optPOPrint_1
        '
        Me._optPOPrint_1.BackColor = System.Drawing.SystemColors.Control
        Me._optPOPrint_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optPOPrint_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optPOPrint_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPOPrint.SetIndex(Me._optPOPrint_1, CType(1, Short))
        Me._optPOPrint_1.Location = New System.Drawing.Point(222, 20)
        Me._optPOPrint_1.Name = "_optPOPrint_1"
        Me._optPOPrint_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optPOPrint_1.Size = New System.Drawing.Size(85, 17)
        Me._optPOPrint_1.TabIndex = 232
        Me._optPOPrint_1.TabStop = True
        Me._optPOPrint_1.Text = "Plain A4"
        Me._optPOPrint_1.UseVisualStyleBackColor = False
        '
        '_optPOPrint_0
        '
        Me._optPOPrint_0.BackColor = System.Drawing.SystemColors.Control
        Me._optPOPrint_0.Checked = True
        Me._optPOPrint_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optPOPrint_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optPOPrint_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPOPrint.SetIndex(Me._optPOPrint_0, CType(0, Short))
        Me._optPOPrint_0.Location = New System.Drawing.Point(46, 20)
        Me._optPOPrint_0.Name = "_optPOPrint_0"
        Me._optPOPrint_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optPOPrint_0.Size = New System.Drawing.Size(147, 16)
        Me._optPOPrint_0.TabIndex = 231
        Me._optPOPrint_0.TabStop = True
        Me._optPOPrint_0.Text = "Pre-Print Stationary"
        Me._optPOPrint_0.UseVisualStyleBackColor = False
        '
        'Frame29
        '
        Me.Frame29.BackColor = System.Drawing.SystemColors.Control
        Me.Frame29.Controls.Add(Me.chkBOPCheck)
        Me.Frame29.Controls.Add(Me.chkFGCheck)
        Me.Frame29.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame29.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame29.Location = New System.Drawing.Point(328, 58)
        Me.Frame29.Name = "Frame29"
        Me.Frame29.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame29.Size = New System.Drawing.Size(277, 59)
        Me.Frame29.TabIndex = 227
        Me.Frame29.TabStop = False
        Me.Frame29.Text = "Store Requisition Check"
        '
        'chkBOPCheck
        '
        Me.chkBOPCheck.BackColor = System.Drawing.SystemColors.Control
        Me.chkBOPCheck.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkBOPCheck.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBOPCheck.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkBOPCheck.Location = New System.Drawing.Point(12, 20)
        Me.chkBOPCheck.Name = "chkBOPCheck"
        Me.chkBOPCheck.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkBOPCheck.Size = New System.Drawing.Size(177, 18)
        Me.chkBOPCheck.TabIndex = 229
        Me.chkBOPCheck.Text = "BOP Check (Yes / No)"
        Me.chkBOPCheck.UseVisualStyleBackColor = False
        '
        'chkFGCheck
        '
        Me.chkFGCheck.BackColor = System.Drawing.SystemColors.Control
        Me.chkFGCheck.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkFGCheck.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFGCheck.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkFGCheck.Location = New System.Drawing.Point(12, 36)
        Me.chkFGCheck.Name = "chkFGCheck"
        Me.chkFGCheck.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkFGCheck.Size = New System.Drawing.Size(171, 18)
        Me.chkFGCheck.TabIndex = 228
        Me.chkFGCheck.Text = "FG Check (Yes / No)"
        Me.chkFGCheck.UseVisualStyleBackColor = False
        '
        '_Label12_4
        '
        Me._Label12_4.AutoSize = True
        Me._Label12_4.BackColor = System.Drawing.SystemColors.Control
        Me._Label12_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label12_4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label12_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.SetIndex(Me._Label12_4, CType(4, Short))
        Me._Label12_4.Location = New System.Drawing.Point(272, 16)
        Me._Label12_4.Name = "_Label12_4"
        Me._Label12_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label12_4.Size = New System.Drawing.Size(32, 13)
        Me._Label12_4.TabIndex = 254
        Me._Label12_4.Text = "Days"
        '
        'Label66
        '
        Me.Label66.BackColor = System.Drawing.SystemColors.Control
        Me.Label66.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label66.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label66.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label66.Location = New System.Drawing.Point(6, 16)
        Me.Label66.Name = "Label66"
        Me.Label66.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label66.Size = New System.Drawing.Size(151, 13)
        Me.Label66.TabIndex = 253
        Me.Label66.Text = "QC Allow :"
        '
        'Label67
        '
        Me.Label67.BackColor = System.Drawing.SystemColors.Control
        Me.Label67.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label67.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label67.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label67.Location = New System.Drawing.Point(6, 38)
        Me.Label67.Name = "Label67"
        Me.Label67.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label67.Size = New System.Drawing.Size(203, 13)
        Me.Label67.TabIndex = 252
        Me.Label67.Text = "Check Max Inv. in Gate Entry :"
        '
        '_SSTab1_TabPage8
        '
        Me._SSTab1_TabPage8.Controls.Add(Me.fraCategory)
        Me._SSTab1_TabPage8.Location = New System.Drawing.Point(4, 28)
        Me._SSTab1_TabPage8.Name = "_SSTab1_TabPage8"
        Me._SSTab1_TabPage8.Size = New System.Drawing.Size(822, 533)
        Me._SSTab1_TabPage8.TabIndex = 8
        Me._SSTab1_TabPage8.Text = "Category Mapping"
        Me._SSTab1_TabPage8.UseVisualStyleBackColor = True
        '
        'fraCategory
        '
        Me.fraCategory.BackColor = System.Drawing.SystemColors.Control
        Me.fraCategory.Controls.Add(Me.SprdCategory)
        Me.fraCategory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.fraCategory.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraCategory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraCategory.Location = New System.Drawing.Point(0, 0)
        Me.fraCategory.Name = "fraCategory"
        Me.fraCategory.Padding = New System.Windows.Forms.Padding(0)
        Me.fraCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraCategory.Size = New System.Drawing.Size(822, 533)
        Me.fraCategory.TabIndex = 272
        Me.fraCategory.TabStop = False
        '
        'SprdCategory
        '
        Me.SprdCategory.DataSource = Nothing
        Me.SprdCategory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SprdCategory.Location = New System.Drawing.Point(0, 15)
        Me.SprdCategory.Name = "SprdCategory"
        Me.SprdCategory.OcxState = CType(resources.GetObject("SprdCategory.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdCategory.Size = New System.Drawing.Size(822, 518)
        Me.SprdCategory.TabIndex = 325
        '
        'Frame8
        '
        Me.Frame8.BackColor = System.Drawing.SystemColors.Control
        Me.Frame8.Controls.Add(Me.cmdSave)
        Me.Frame8.Controls.Add(Me.cmdcancel)
        Me.Frame8.Controls.Add(Me.cmdSavePrint)
        Me.Frame8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame8.Location = New System.Drawing.Point(7, 542)
        Me.Frame8.Name = "Frame8"
        Me.Frame8.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame8.Size = New System.Drawing.Size(819, 47)
        Me.Frame8.TabIndex = 2
        Me.Frame8.TabStop = False
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Image = CType(resources.GetObject("cmdSave.Image"), System.Drawing.Image)
        Me.cmdSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSave.Location = New System.Drawing.Point(4, 10)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(69, 34)
        Me.cmdSave.TabIndex = 184
        Me.cmdSave.Text = "&Save"
        Me.cmdSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'OptPrintCompanyFull_ShortName
        '
        '
        'optInvGen
        '
        '
        'optInvPostingType
        '
        '
        'optLoadingSlip
        '
        '
        'optPOLocking
        '
        '
        'optPOPrint
        '
        '
        'optProductionPlanLocking
        '
        '
        'optPurPostingType
        '
        '
        'optSOLocking
        '
        '
        'txtMargin
        '
        '
        'frmSysPref
        '
        Me.AcceptButton = Me.cmdSave
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(836, 596)
        Me.Controls.Add(Me.Frame13)
        Me.Controls.Add(Me.Frame8)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(8, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSysPref"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "System Options"
        Me.Frame13.ResumeLayout(False)
        Me.SSTab1.ResumeLayout(False)
        Me._SSTab1_TabPage0.ResumeLayout(False)
        Me._FraBorder_8.ResumeLayout(False)
        Me._FraBorder_8.PerformLayout()
        Me.GroupBox31.ResumeLayout(False)
        Me.GroupBox31.PerformLayout()
        Me.GroupBox30.ResumeLayout(False)
        Me.GroupBox30.PerformLayout()
        Me.GroupBox29.ResumeLayout(False)
        Me.GroupBox29.PerformLayout()
        Me.GroupBox28.ResumeLayout(False)
        Me.GroupBox28.PerformLayout()
        Me.GroupBox27.ResumeLayout(False)
        Me.GroupBox27.PerformLayout()
        Me.GroupBox26.ResumeLayout(False)
        Me.GroupBox26.PerformLayout()
        Me.GroupBox25.ResumeLayout(False)
        Me.GroupBox25.PerformLayout()
        Me.GroupBox24.ResumeLayout(False)
        Me.GroupBox24.PerformLayout()
        Me.GroupBox23.ResumeLayout(False)
        Me.GroupBox23.PerformLayout()
        Me.GroupBox22.ResumeLayout(False)
        Me.GroupBox22.PerformLayout()
        Me.GroupBox21.ResumeLayout(False)
        Me.GroupBox21.PerformLayout()
        Me.GroupBox20.ResumeLayout(False)
        Me.GroupBox20.PerformLayout()
        Me.GroupBox19.ResumeLayout(False)
        Me.GroupBox19.PerformLayout()
        Me.GroupBox18.ResumeLayout(False)
        Me.GroupBox18.PerformLayout()
        Me.GroupBox17.ResumeLayout(False)
        Me.GroupBox17.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        Me.GroupBox16.ResumeLayout(False)
        Me.GroupBox16.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox14.ResumeLayout(False)
        Me.GroupBox14.PerformLayout()
        Me.Frame10.ResumeLayout(False)
        Me.GroupBox13.ResumeLayout(False)
        Me.GroupBox13.PerformLayout()
        Me.Frame21.ResumeLayout(False)
        Me.Frame15.ResumeLayout(False)
        Me.GroupBox11.ResumeLayout(False)
        Me.GroupBox11.PerformLayout()
        Me.Frame18.ResumeLayout(False)
        Me.Frame17.ResumeLayout(False)
        Me.Frame17.PerformLayout()
        Me.Frame9.ResumeLayout(False)
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.Frame6.ResumeLayout(False)
        Me.fraMargin.ResumeLayout(False)
        Me.fraMargin.PerformLayout()
        Me.Frame11.ResumeLayout(False)
        Me.Frame12.ResumeLayout(False)
        Me.Frame7.ResumeLayout(False)
        Me.Frame5.ResumeLayout(False)
        Me.Frame16.ResumeLayout(False)
        Me.Frame16.PerformLayout()
        Me.Frame19.ResumeLayout(False)
        Me.Frame19.PerformLayout()
        Me._SSTab1_TabPage1.ResumeLayout(False)
        Me._FraBorder_5.ResumeLayout(False)
        Me._FraBorder_5.PerformLayout()
        Me._SSTab1_TabPage3.ResumeLayout(False)
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me._SSTab1_TabPage4.ResumeLayout(False)
        Me._FraBorder_2.ResumeLayout(False)
        Me._FraBorder_2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.Frame32.ResumeLayout(False)
        Me.Frame32.PerformLayout()
        Me.Frame24.ResumeLayout(False)
        Me.Frame4.ResumeLayout(False)
        Me.Frame3.ResumeLayout(False)
        Me.Frame1.ResumeLayout(False)
        Me._SSTab1_TabPage6.ResumeLayout(False)
        Me._FraBorder_6.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage7.ResumeLayout(False)
        Me._FraBorder_9.ResumeLayout(False)
        Me._FraBorder_9.PerformLayout()
        Me.Frame34.ResumeLayout(False)
        Me.Frame34.PerformLayout()
        Me.Frame33.ResumeLayout(False)
        Me.Frame33.PerformLayout()
        Me.Frame31.ResumeLayout(False)
        Me.Frame30.ResumeLayout(False)
        Me.Frame25.ResumeLayout(False)
        Me.Frame25.PerformLayout()
        Me.Frame26.ResumeLayout(False)
        Me.Frame26.PerformLayout()
        Me.Frame27.ResumeLayout(False)
        Me.Frame27.PerformLayout()
        Me.Frame28.ResumeLayout(False)
        Me.Frame29.ResumeLayout(False)
        Me._SSTab1_TabPage8.ResumeLayout(False)
        Me.fraCategory.ResumeLayout(False)
        CType(Me.SprdCategory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame8.ResumeLayout(False)
        CType(Me.FraBorder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptPrintCompanyFull_ShortName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optInvGen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optInvPostingType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optLoadingSlip, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optPOLocking, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optPOPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optProductionPlanLocking, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optPurPostingType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optSOLocking, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMargin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optCBJSeq, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents txtCreditBank As System.Windows.Forms.TextBox
    Public WithEvents txtFurtherBank As System.Windows.Forms.TextBox
    Public WithEvents txtADCode As System.Windows.Forms.TextBox
    Public WithEvents txtCreditBankAddress As System.Windows.Forms.TextBox
    Public WithEvents Label37 As System.Windows.Forms.Label
    Public WithEvents Label38 As System.Windows.Forms.Label
    Public WithEvents Label39 As System.Windows.Forms.Label
    Public WithEvents Label40 As System.Windows.Forms.Label
    Public WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Public WithEvents chkHideHeatNo As System.Windows.Forms.CheckBox
    Public WithEvents chkHideBatchNo As System.Windows.Forms.CheckBox
    Public WithEvents txtCompAc As System.Windows.Forms.TextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents chkAcPRAutoJv As System.Windows.Forms.CheckBox
    Public WithEvents chkCheckPORate As CheckBox
    Public WithEvents GroupBox2 As GroupBox
    Public WithEvents optDailySeq As RadioButton
    Public WithEvents optCBJSeq As VB6.RadioButtonArray
    Public WithEvents optYearlySeq As RadioButton
    Public WithEvents optMonthlySeq As RadioButton
    Public WithEvents GroupBox3 As GroupBox
    Public WithEvents optPrintPortrait As RadioButton
    Public WithEvents optPrintLandScape As RadioButton
    Public WithEvents GroupBox4 As GroupBox
    Public WithEvents optA4 As RadioButton
    Public WithEvents optA3 As RadioButton
    Public WithEvents txtInvoiceDigit As TextBox
    Public WithEvents Label2 As Label
    Public WithEvents chkRateDiffCrWithGST As CheckBox
    Public WithEvents chkRateDiffDrWithGST As CheckBox
    Public WithEvents chkRejectionWithGST As CheckBox
    Public WithEvents chkShortageWithGST As CheckBox
    Public WithEvents chkCreditLimit As CheckBox
    Friend WithEvents _SSTab1_TabPage8 As TabPage
    Public WithEvents fraCategory As GroupBox
    Public WithEvents GroupBox5 As GroupBox
    Public WithEvents ChkPrintOTInPayslip As CheckBox
    Public WithEvents Label3 As Label
    Public WithEvents GroupBox7 As GroupBox
    Public WithEvents ChkGSTSeparate As CheckBox
    Public WithEvents Label13 As Label
    Public WithEvents GroupBox8 As GroupBox
    Public WithEvents ChkAWavailable As CheckBox
    Public WithEvents Label17 As Label
    Public WithEvents GroupBox11 As GroupBox
    Public WithEvents ChkEInvoiceApp As CheckBox
    Public WithEvents Label21 As Label
    Public WithEvents GroupBox13 As GroupBox
    Public WithEvents ChkPacketCol As CheckBox
    Public WithEvents Label23 As Label
    Public WithEvents GroupBox14 As GroupBox
    Public WithEvents ChkTCS As CheckBox
    Public WithEvents Label24 As Label
    Public WithEvents GroupBox16 As GroupBox
    Public WithEvents ChkInnerPackCol As CheckBox
    Public WithEvents Label26 As Label
    Public WithEvents GroupBox6 As GroupBox
    Public WithEvents ChkDivlocation As CheckBox
    Public WithEvents Label7 As Label
    Public WithEvents GroupBox17 As GroupBox
    Public WithEvents ChkBilladj As CheckBox
    Public WithEvents Label27 As Label
    Public WithEvents GroupBox19 As GroupBox
    Public WithEvents ChkAutoGenCode As CheckBox
    Public WithEvents Label29 As Label
    Public WithEvents GroupBox18 As GroupBox
    Public WithEvents ChkDCInLedger As CheckBox
    Public WithEvents Label28 As Label
    Public WithEvents GroupBox27 As GroupBox
    Public WithEvents ChkINVFromStock As CheckBox
    Public WithEvents Label42 As Label
    Public WithEvents GroupBox26 As GroupBox
    Public WithEvents ChkMRPSaleOrder As CheckBox
    Public WithEvents Label41 As Label
    Public WithEvents GroupBox25 As GroupBox
    Public WithEvents ChkLockPayterms As CheckBox
    Public WithEvents Label36 As Label
    Public WithEvents GroupBox24 As GroupBox
    Public WithEvents ChkSaleOrderIndent As CheckBox
    Public WithEvents Label35 As Label
    Public WithEvents GroupBox23 As GroupBox
    Public WithEvents ChkDoubleSalary As CheckBox
    Public WithEvents Label34 As Label
    Public WithEvents GroupBox22 As GroupBox
    Public WithEvents ChkElFixed As CheckBox
    Public WithEvents Label32 As Label
    Public WithEvents GroupBox21 As GroupBox
    Public WithEvents ChkAfterConfirm As CheckBox
    Public WithEvents Label31 As Label
    Public WithEvents GroupBox20 As GroupBox
    Public WithEvents ChkWarHouse As CheckBox
    Public WithEvents Label30 As Label
    Public WithEvents GroupBox30 As GroupBox
    Public WithEvents ChkBom As CheckBox
    Public WithEvents Label45 As Label
    Public WithEvents GroupBox29 As GroupBox
    Public WithEvents ChkSaleScheduleReq As CheckBox
    Public WithEvents Label44 As Label
    Public WithEvents GroupBox28 As GroupBox
    Public WithEvents ChkQtyCheck As CheckBox
    Public WithEvents Label43 As Label
    Public WithEvents GroupBox31 As GroupBox
    Public WithEvents ChkOuterPackCol As CheckBox
    Public WithEvents Label46 As Label
#End Region
End Class