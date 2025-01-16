Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmPurchaseShipGST
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
    Public WithEvents SprdPostingDetail As AxFPSpreadADO.AxfpSpread
    Public WithEvents FraPostingDtl As System.Windows.Forms.GroupBox
    Public WithEvents chkGSTClaim As System.Windows.Forms.CheckBox
    Public WithEvents cboGSTStatus As System.Windows.Forms.ComboBox
    Public WithEvents chkCreditRC As System.Windows.Forms.CheckBox
    Public WithEvents txtShippedTo As System.Windows.Forms.TextBox
    Public WithEvents chkShipTo As System.Windows.Forms.CheckBox
    Public WithEvents cboDivision As System.Windows.Forms.ComboBox
    Public WithEvents chkFOC As System.Windows.Forms.CheckBox
    Public WithEvents chkFinalPost As System.Windows.Forms.CheckBox
    Public WithEvents ChkCapital As System.Windows.Forms.CheckBox
    Public WithEvents txtModvatNo As System.Windows.Forms.TextBox
    Public WithEvents txtModvatDate As System.Windows.Forms.TextBox
    Public WithEvents txtVNo As System.Windows.Forms.TextBox
    Public WithEvents txtVNoPrefix As System.Windows.Forms.TextBox
    Public WithEvents txtVNoSuffix As System.Windows.Forms.TextBox
    Public WithEvents txtVDate As System.Windows.Forms.TextBox
    Public WithEvents chkRejection As System.Windows.Forms.CheckBox
    Public WithEvents chkCancelled As System.Windows.Forms.CheckBox
    Public WithEvents lblSeprateGST As System.Windows.Forms.Label
    Public WithEvents Label45 As System.Windows.Forms.Label
    Public WithEvents lblTotSGSTAmount As System.Windows.Forms.Label
    Public WithEvents lblTotCGSTAmount As System.Windows.Forms.Label
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents lblTotExpAmt As System.Windows.Forms.Label
    Public WithEvents Label34 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents lblTotIGSTAmount As System.Windows.Forms.Label
    Public WithEvents _SSTabLevies_TabPage0 As System.Windows.Forms.TabPage
    Public WithEvents Label48 As System.Windows.Forms.Label
    Public WithEvents Label44 As System.Windows.Forms.Label
    Public WithEvents Label20 As System.Windows.Forms.Label
    Public WithEvents lblSaleBillNoSeq As System.Windows.Forms.Label
    Public WithEvents lblSaleBillNo As System.Windows.Forms.Label
    Public WithEvents lblSaleBillDate As System.Windows.Forms.Label
    Public WithEvents lblPurchaseType As System.Windows.Forms.Label
    Public WithEvents lblPurchaseSeqType As System.Windows.Forms.Label
    Public WithEvents txtTotSGSTRefund As System.Windows.Forms.TextBox
    Public WithEvents txtTotIGSTRefund As System.Windows.Forms.TextBox
    Public WithEvents txtTotCGSTRefund As System.Windows.Forms.TextBox
    Public WithEvents _SSTabLevies_TabPage1 As System.Windows.Forms.TabPage
    Public WithEvents SSTabLevies As System.Windows.Forms.TabControl
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents SprdExp As AxFPSpreadADO.AxfpSpread
    Public WithEvents lblClaimStatus As System.Windows.Forms.Label
    Public WithEvents lblTotQty As System.Windows.Forms.Label
    Public WithEvents lblTotPackQtyCap As System.Windows.Forms.Label
    Public WithEvents lblADE1 As System.Windows.Forms.Label
    Public WithEvents lblServicePercentage As System.Windows.Forms.Label
    Public WithEvents lblCESSableAmount As System.Windows.Forms.Label
    Public WithEvents lblEDUPercent As System.Windows.Forms.Label
    Public WithEvents lblMSC As System.Windows.Forms.Label
    Public WithEvents lblRO As System.Windows.Forms.Label
    Public WithEvents lblSurcharge As System.Windows.Forms.Label
    Public WithEvents lblDiscount As System.Windows.Forms.Label
    Public WithEvents lblTotTaxableAmt As System.Windows.Forms.Label
    Public WithEvents lblSTPercentage As System.Windows.Forms.Label
    Public WithEvents lblEDPercentage As System.Windows.Forms.Label
    Public WithEvents lblTotOtherExp As System.Windows.Forms.Label
    Public WithEvents lblTotFreight As System.Windows.Forms.Label
    Public WithEvents lblTotCharges As System.Windows.Forms.Label
    Public WithEvents LblBookCode As System.Windows.Forms.Label
    Public WithEvents LblMKey As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents lblTotItemValue As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents lblNetAmount As System.Windows.Forms.Label
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents _SSTab1_TabPage0 As System.Windows.Forms.TabPage
    Public WithEvents txtRecipientPer As System.Windows.Forms.TextBox
    Public WithEvents txtProviderPer As System.Windows.Forms.TextBox
    Public WithEvents txtServiceTaxAmount As System.Windows.Forms.TextBox
    Public WithEvents txtServiceTaxPer As System.Windows.Forms.TextBox
    Public WithEvents txtServiceOn As System.Windows.Forms.TextBox
    Public WithEvents Label61 As System.Windows.Forms.Label
    Public WithEvents Label60 As System.Windows.Forms.Label
    Public WithEvents Label28 As System.Windows.Forms.Label
    Public WithEvents Label63 As System.Windows.Forms.Label
    Public WithEvents Label62 As System.Windows.Forms.Label
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents txtAdvAdjust As System.Windows.Forms.TextBox
    Public WithEvents txtAdvVNo As System.Windows.Forms.TextBox
    Public WithEvents txtAdvDate As System.Windows.Forms.TextBox
    Public WithEvents txtAdvIGST As System.Windows.Forms.TextBox
    Public WithEvents txtAdvSGST As System.Windows.Forms.TextBox
    Public WithEvents txtAdvCGST As System.Windows.Forms.TextBox
    Public WithEvents txtItemAdvAdjust As System.Windows.Forms.TextBox
    Public WithEvents txtAdvBal As System.Windows.Forms.TextBox
    Public WithEvents Label24 As System.Windows.Forms.Label
    Public WithEvents Label23 As System.Windows.Forms.Label
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents Label22 As System.Windows.Forms.Label
    Public WithEvents Label21 As System.Windows.Forms.Label
    Public WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents _txtCreditDays_0 As System.Windows.Forms.TextBox
    Public WithEvents _txtCreditDays_1 As System.Windows.Forms.TextBox
    Public WithEvents _OptFreight_0 As System.Windows.Forms.RadioButton
    Public WithEvents _OptFreight_1 As System.Windows.Forms.RadioButton
    Public WithEvents Frame7 As System.Windows.Forms.GroupBox
    Public WithEvents txtPaymentdate As System.Windows.Forms.TextBox
    Public WithEvents txtTariff As System.Windows.Forms.TextBox
    Public WithEvents txtServProvided As System.Windows.Forms.TextBox
    Public WithEvents Label53 As System.Windows.Forms.Label
    Public WithEvents FraServiceTax As System.Windows.Forms.GroupBox
    Public WithEvents ChkSTDSRO As System.Windows.Forms.CheckBox
    Public WithEvents ChkESIRO As System.Windows.Forms.CheckBox
    Public WithEvents ChkTDSRO As System.Windows.Forms.CheckBox
    Public WithEvents txtSTDSDeductOn As System.Windows.Forms.TextBox
    Public WithEvents txtESIDeductOn As System.Windows.Forms.TextBox
    Public WithEvents txtTDSDeductOn As System.Windows.Forms.TextBox
    Public WithEvents txtJVVNO As System.Windows.Forms.TextBox
    Public WithEvents ChkSTDS As System.Windows.Forms.CheckBox
    Public WithEvents txtSTDSRate As System.Windows.Forms.TextBox
    Public WithEvents txtSTDSAmount As System.Windows.Forms.TextBox
    Public WithEvents chkESI As System.Windows.Forms.CheckBox
    Public WithEvents txtESIRate As System.Windows.Forms.TextBox
    Public WithEvents txtESIAmount As System.Windows.Forms.TextBox
    Public WithEvents chkTDS As System.Windows.Forms.CheckBox
    Public WithEvents txtTDSRate As System.Windows.Forms.TextBox
    Public WithEvents txtTDSAmount As System.Windows.Forms.TextBox
    Public WithEvents Label42 As System.Windows.Forms.Label
    Public WithEvents Label46 As System.Windows.Forms.Label
    Public WithEvents Label43 As System.Windows.Forms.Label
    Public WithEvents Label40 As System.Windows.Forms.Label
    Public WithEvents Label41 As System.Windows.Forms.Label
    Public WithEvents Frame8 As System.Windows.Forms.GroupBox
    Public WithEvents txtNarration As System.Windows.Forms.TextBox
    Public WithEvents txtDocsThru As System.Windows.Forms.TextBox
    Public WithEvents txtCarriers As System.Windows.Forms.TextBox
    Public WithEvents txtVehicle As System.Windows.Forms.TextBox
    Public WithEvents txtMode As System.Windows.Forms.TextBox
    Public WithEvents txtRemarks As System.Windows.Forms.TextBox
    Public WithEvents txtItemType As System.Windows.Forms.TextBox
    Public WithEvents Label33 As System.Windows.Forms.Label
    Public WithEvents Label35 As System.Windows.Forms.Label
    Public WithEvents Label37 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label32 As System.Windows.Forms.Label
    Public WithEvents Label31 As System.Windows.Forms.Label
    Public WithEvents Label30 As System.Windows.Forms.Label
    Public WithEvents Label29 As System.Windows.Forms.Label
    Public WithEvents Label27 As System.Windows.Forms.Label
    Public WithEvents Label26 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents _SSTab1_TabPage1 As System.Windows.Forms.TabPage
    Public WithEvents SSTab1 As System.Windows.Forms.TabControl
    Public WithEvents txtDebitAccount As System.Windows.Forms.TextBox
    Public WithEvents txtSupplier As System.Windows.Forms.TextBox
    Public WithEvents txtMRRDate As System.Windows.Forms.TextBox
    Public WithEvents txtBillDate As System.Windows.Forms.TextBox
    Public WithEvents cboInvType As System.Windows.Forms.ComboBox
    Public WithEvents txtMRRNo As System.Windows.Forms.TextBox
    Public WithEvents CmdSearchMRR As System.Windows.Forms.Button
    Public WithEvents txtBillNo As System.Windows.Forms.TextBox
    Public WithEvents txtServNo As System.Windows.Forms.TextBox
    Public WithEvents txtServDate As System.Windows.Forms.TextBox
    Public WithEvents txtPONo As System.Windows.Forms.TextBox
    Public WithEvents txtPODate As System.Windows.Forms.TextBox
    Public WithEvents lblGSTClaimNo As System.Windows.Forms.Label
    Public WithEvents lblGSTClaimDate As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label59 As System.Windows.Forms.Label
    Public WithEvents Label56 As System.Windows.Forms.Label
    Public WithEvents Label38 As System.Windows.Forms.Label
    Public WithEvents lblPurchaseVNo As System.Windows.Forms.Label
    Public WithEvents lblVNo As System.Windows.Forms.Label
    Public WithEvents lblVDate As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents lblCust As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents lblInvType As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents FraFront As System.Windows.Forms.GroupBox
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
    Public WithEvents cmdPostingHead As System.Windows.Forms.Button
    Public WithEvents cmdBarCode As System.Windows.Forms.Button
    Public WithEvents cmdDelete As System.Windows.Forms.Button
    Public WithEvents cmdSave As System.Windows.Forms.Button
    Public WithEvents cmdModify As System.Windows.Forms.Button
    Public WithEvents cmdAdd As System.Windows.Forms.Button
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents lblPMKey As System.Windows.Forms.Label
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPurchaseShipGST))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CmdSearchMRR = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.cmdPostingHead = New System.Windows.Forms.Button()
        Me.cmdBarCode = New System.Windows.Forms.Button()
        Me.cmdDelete = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdModify = New System.Windows.Forms.Button()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.FraPostingDtl = New System.Windows.Forms.GroupBox()
        Me.SprdPostingDetail = New AxFPSpreadADO.AxfpSpread()
        Me.FraFront = New System.Windows.Forms.GroupBox()
        Me.TxtShipTo = New System.Windows.Forms.TextBox()
        Me.txtBillTo = New System.Windows.Forms.TextBox()
        Me.chkGSTClaim = New System.Windows.Forms.CheckBox()
        Me.cboGSTStatus = New System.Windows.Forms.ComboBox()
        Me.chkCreditRC = New System.Windows.Forms.CheckBox()
        Me.txtShippedTo = New System.Windows.Forms.TextBox()
        Me.chkShipTo = New System.Windows.Forms.CheckBox()
        Me.cboDivision = New System.Windows.Forms.ComboBox()
        Me.chkFOC = New System.Windows.Forms.CheckBox()
        Me.chkFinalPost = New System.Windows.Forms.CheckBox()
        Me.ChkCapital = New System.Windows.Forms.CheckBox()
        Me.txtModvatNo = New System.Windows.Forms.TextBox()
        Me.txtModvatDate = New System.Windows.Forms.TextBox()
        Me.txtVNo = New System.Windows.Forms.TextBox()
        Me.txtVNoPrefix = New System.Windows.Forms.TextBox()
        Me.txtVNoSuffix = New System.Windows.Forms.TextBox()
        Me.txtVDate = New System.Windows.Forms.TextBox()
        Me.chkRejection = New System.Windows.Forms.CheckBox()
        Me.chkCancelled = New System.Windows.Forms.CheckBox()
        Me.SSTab1 = New System.Windows.Forms.TabControl()
        Me._SSTab1_TabPage0 = New System.Windows.Forms.TabPage()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.SSTabLevies = New System.Windows.Forms.TabControl()
        Me._SSTabLevies_TabPage0 = New System.Windows.Forms.TabPage()
        Me.lblSeprateGST = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.lblTotSGSTAmount = New System.Windows.Forms.Label()
        Me.lblTotCGSTAmount = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.lblTotExpAmt = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblTotIGSTAmount = New System.Windows.Forms.Label()
        Me._SSTabLevies_TabPage1 = New System.Windows.Forms.TabPage()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.lblSaleBillNoSeq = New System.Windows.Forms.Label()
        Me.lblSaleBillNo = New System.Windows.Forms.Label()
        Me.lblSaleBillDate = New System.Windows.Forms.Label()
        Me.lblPurchaseType = New System.Windows.Forms.Label()
        Me.lblPurchaseSeqType = New System.Windows.Forms.Label()
        Me.txtTotSGSTRefund = New System.Windows.Forms.TextBox()
        Me.txtTotIGSTRefund = New System.Windows.Forms.TextBox()
        Me.txtTotCGSTRefund = New System.Windows.Forms.TextBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.SprdExp = New AxFPSpreadADO.AxfpSpread()
        Me.lblClaimStatus = New System.Windows.Forms.Label()
        Me.lblTotQty = New System.Windows.Forms.Label()
        Me.lblTotPackQtyCap = New System.Windows.Forms.Label()
        Me.lblADE1 = New System.Windows.Forms.Label()
        Me.lblServicePercentage = New System.Windows.Forms.Label()
        Me.lblCESSableAmount = New System.Windows.Forms.Label()
        Me.lblEDUPercent = New System.Windows.Forms.Label()
        Me.lblMSC = New System.Windows.Forms.Label()
        Me.lblRO = New System.Windows.Forms.Label()
        Me.lblSurcharge = New System.Windows.Forms.Label()
        Me.lblDiscount = New System.Windows.Forms.Label()
        Me.lblTotTaxableAmt = New System.Windows.Forms.Label()
        Me.lblSTPercentage = New System.Windows.Forms.Label()
        Me.lblEDPercentage = New System.Windows.Forms.Label()
        Me.lblTotOtherExp = New System.Windows.Forms.Label()
        Me.lblTotFreight = New System.Windows.Forms.Label()
        Me.lblTotCharges = New System.Windows.Forms.Label()
        Me.LblBookCode = New System.Windows.Forms.Label()
        Me.LblMKey = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblTotItemValue = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblNetAmount = New System.Windows.Forms.Label()
        Me._SSTab1_TabPage1 = New System.Windows.Forms.TabPage()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.txtRecipientPer = New System.Windows.Forms.TextBox()
        Me.txtProviderPer = New System.Windows.Forms.TextBox()
        Me.txtServiceTaxAmount = New System.Windows.Forms.TextBox()
        Me.txtServiceTaxPer = New System.Windows.Forms.TextBox()
        Me.txtServiceOn = New System.Windows.Forms.TextBox()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.txtAdvAdjust = New System.Windows.Forms.TextBox()
        Me.txtAdvVNo = New System.Windows.Forms.TextBox()
        Me.txtAdvDate = New System.Windows.Forms.TextBox()
        Me.txtAdvIGST = New System.Windows.Forms.TextBox()
        Me.txtAdvSGST = New System.Windows.Forms.TextBox()
        Me.txtAdvCGST = New System.Windows.Forms.TextBox()
        Me.txtItemAdvAdjust = New System.Windows.Forms.TextBox()
        Me.txtAdvBal = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me._txtCreditDays_0 = New System.Windows.Forms.TextBox()
        Me._txtCreditDays_1 = New System.Windows.Forms.TextBox()
        Me.Frame7 = New System.Windows.Forms.GroupBox()
        Me._OptFreight_0 = New System.Windows.Forms.RadioButton()
        Me._OptFreight_1 = New System.Windows.Forms.RadioButton()
        Me.txtPaymentdate = New System.Windows.Forms.TextBox()
        Me.txtTariff = New System.Windows.Forms.TextBox()
        Me.FraServiceTax = New System.Windows.Forms.GroupBox()
        Me.txtServProvided = New System.Windows.Forms.TextBox()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.Frame8 = New System.Windows.Forms.GroupBox()
        Me.ChkSTDSRO = New System.Windows.Forms.CheckBox()
        Me.ChkESIRO = New System.Windows.Forms.CheckBox()
        Me.ChkTDSRO = New System.Windows.Forms.CheckBox()
        Me.txtSTDSDeductOn = New System.Windows.Forms.TextBox()
        Me.txtESIDeductOn = New System.Windows.Forms.TextBox()
        Me.txtTDSDeductOn = New System.Windows.Forms.TextBox()
        Me.txtJVVNO = New System.Windows.Forms.TextBox()
        Me.ChkSTDS = New System.Windows.Forms.CheckBox()
        Me.txtSTDSRate = New System.Windows.Forms.TextBox()
        Me.txtSTDSAmount = New System.Windows.Forms.TextBox()
        Me.chkESI = New System.Windows.Forms.CheckBox()
        Me.txtESIRate = New System.Windows.Forms.TextBox()
        Me.txtESIAmount = New System.Windows.Forms.TextBox()
        Me.chkTDS = New System.Windows.Forms.CheckBox()
        Me.txtTDSRate = New System.Windows.Forms.TextBox()
        Me.txtTDSAmount = New System.Windows.Forms.TextBox()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.txtNarration = New System.Windows.Forms.TextBox()
        Me.txtDocsThru = New System.Windows.Forms.TextBox()
        Me.txtCarriers = New System.Windows.Forms.TextBox()
        Me.txtVehicle = New System.Windows.Forms.TextBox()
        Me.txtMode = New System.Windows.Forms.TextBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtItemType = New System.Windows.Forms.TextBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtDebitAccount = New System.Windows.Forms.TextBox()
        Me.txtSupplier = New System.Windows.Forms.TextBox()
        Me.txtMRRDate = New System.Windows.Forms.TextBox()
        Me.txtBillDate = New System.Windows.Forms.TextBox()
        Me.cboInvType = New System.Windows.Forms.ComboBox()
        Me.txtMRRNo = New System.Windows.Forms.TextBox()
        Me.txtBillNo = New System.Windows.Forms.TextBox()
        Me.txtServNo = New System.Windows.Forms.TextBox()
        Me.txtServDate = New System.Windows.Forms.TextBox()
        Me.txtPONo = New System.Windows.Forms.TextBox()
        Me.txtPODate = New System.Windows.Forms.TextBox()
        Me.lblGSTClaimNo = New System.Windows.Forms.Label()
        Me.lblGSTClaimDate = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.lblPurchaseVNo = New System.Windows.Forms.Label()
        Me.lblVNo = New System.Windows.Forms.Label()
        Me.lblVDate = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblCust = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblInvType = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.lblPMKey = New System.Windows.Forms.Label()
        Me.lblSODates = New System.Windows.Forms.Label()
        Me.lblSONos = New System.Windows.Forms.Label()
        Me.OptFreight = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.txtCreditDays = New Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray(Me.components)
        Me.FraPostingDtl.SuspendLayout()
        CType(Me.SprdPostingDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraFront.SuspendLayout()
        Me.SSTab1.SuspendLayout()
        Me._SSTab1_TabPage0.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me.SSTabLevies.SuspendLayout()
        Me._SSTabLevies_TabPage0.SuspendLayout()
        Me._SSTabLevies_TabPage1.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdExp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage1.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.Frame4.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame7.SuspendLayout()
        Me.FraServiceTax.SuspendLayout()
        Me.Frame8.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptFreight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCreditDays, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CmdSearchMRR
        '
        Me.CmdSearchMRR.BackColor = System.Drawing.SystemColors.Menu
        Me.CmdSearchMRR.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSearchMRR.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSearchMRR.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSearchMRR.Image = CType(resources.GetObject("CmdSearchMRR.Image"), System.Drawing.Image)
        Me.CmdSearchMRR.Location = New System.Drawing.Point(186, 74)
        Me.CmdSearchMRR.Name = "CmdSearchMRR"
        Me.CmdSearchMRR.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSearchMRR.Size = New System.Drawing.Size(23, 19)
        Me.CmdSearchMRR.TabIndex = 11
        Me.CmdSearchMRR.TabStop = False
        Me.CmdSearchMRR.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSearchMRR, "Seach Pending DC")
        Me.CmdSearchMRR.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdClose.Location = New System.Drawing.Point(676, 10)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(67, 37)
        Me.cmdClose.TabIndex = 74
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
        Me.CmdView.Location = New System.Drawing.Point(610, 10)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(67, 37)
        Me.CmdView.TabIndex = 73
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
        Me.CmdPreview.Location = New System.Drawing.Point(544, 10)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.TabIndex = 72
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
        Me.cmdPrint.Location = New System.Drawing.Point(477, 10)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 71
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
        Me.cmdSavePrint.Location = New System.Drawing.Point(411, 10)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdSavePrint.TabIndex = 70
        Me.cmdSavePrint.Text = "Save&&Print"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save & Print")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'cmdPostingHead
        '
        Me.cmdPostingHead.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPostingHead.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPostingHead.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPostingHead.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPostingHead.Image = CType(resources.GetObject("cmdPostingHead.Image"), System.Drawing.Image)
        Me.cmdPostingHead.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdPostingHead.Location = New System.Drawing.Point(344, 10)
        Me.cmdPostingHead.Name = "cmdPostingHead"
        Me.cmdPostingHead.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPostingHead.Size = New System.Drawing.Size(67, 37)
        Me.cmdPostingHead.TabIndex = 133
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
        Me.cmdBarCode.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdBarCode.Location = New System.Drawing.Point(278, 10)
        Me.cmdBarCode.Name = "cmdBarCode"
        Me.cmdBarCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdBarCode.Size = New System.Drawing.Size(67, 37)
        Me.cmdBarCode.TabIndex = 69
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
        Me.cmdDelete.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdDelete.Location = New System.Drawing.Point(211, 10)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDelete.Size = New System.Drawing.Size(67, 37)
        Me.cmdDelete.TabIndex = 68
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
        Me.cmdSave.Location = New System.Drawing.Point(144, 10)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(67, 37)
        Me.cmdSave.TabIndex = 67
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
        Me.cmdModify.Location = New System.Drawing.Point(77, 10)
        Me.cmdModify.Name = "cmdModify"
        Me.cmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdModify.Size = New System.Drawing.Size(67, 37)
        Me.cmdModify.TabIndex = 66
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
        Me.cmdAdd.Location = New System.Drawing.Point(10, 10)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAdd.Size = New System.Drawing.Size(67, 37)
        Me.cmdAdd.TabIndex = 0
        Me.cmdAdd.Text = "&Add"
        Me.cmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdAdd, "Add New")
        Me.cmdAdd.UseVisualStyleBackColor = False
        '
        'FraPostingDtl
        '
        Me.FraPostingDtl.BackColor = System.Drawing.SystemColors.Control
        Me.FraPostingDtl.Controls.Add(Me.SprdPostingDetail)
        Me.FraPostingDtl.Enabled = False
        Me.FraPostingDtl.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraPostingDtl.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraPostingDtl.Location = New System.Drawing.Point(6, 230)
        Me.FraPostingDtl.Name = "FraPostingDtl"
        Me.FraPostingDtl.Padding = New System.Windows.Forms.Padding(0)
        Me.FraPostingDtl.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraPostingDtl.Size = New System.Drawing.Size(414, 207)
        Me.FraPostingDtl.TabIndex = 134
        Me.FraPostingDtl.TabStop = False
        Me.FraPostingDtl.Visible = False
        '
        'SprdPostingDetail
        '
        Me.SprdPostingDetail.DataSource = Nothing
        Me.SprdPostingDetail.Location = New System.Drawing.Point(2, 8)
        Me.SprdPostingDetail.Name = "SprdPostingDetail"
        Me.SprdPostingDetail.OcxState = CType(resources.GetObject("SprdPostingDetail.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdPostingDetail.Size = New System.Drawing.Size(407, 195)
        Me.SprdPostingDetail.TabIndex = 135
        '
        'FraFront
        '
        Me.FraFront.BackColor = System.Drawing.SystemColors.Control
        Me.FraFront.Controls.Add(Me.TxtShipTo)
        Me.FraFront.Controls.Add(Me.txtBillTo)
        Me.FraFront.Controls.Add(Me.chkGSTClaim)
        Me.FraFront.Controls.Add(Me.cboGSTStatus)
        Me.FraFront.Controls.Add(Me.chkCreditRC)
        Me.FraFront.Controls.Add(Me.txtShippedTo)
        Me.FraFront.Controls.Add(Me.chkShipTo)
        Me.FraFront.Controls.Add(Me.cboDivision)
        Me.FraFront.Controls.Add(Me.chkFOC)
        Me.FraFront.Controls.Add(Me.chkFinalPost)
        Me.FraFront.Controls.Add(Me.ChkCapital)
        Me.FraFront.Controls.Add(Me.txtModvatNo)
        Me.FraFront.Controls.Add(Me.txtModvatDate)
        Me.FraFront.Controls.Add(Me.txtVNo)
        Me.FraFront.Controls.Add(Me.txtVNoPrefix)
        Me.FraFront.Controls.Add(Me.txtVNoSuffix)
        Me.FraFront.Controls.Add(Me.txtVDate)
        Me.FraFront.Controls.Add(Me.chkRejection)
        Me.FraFront.Controls.Add(Me.chkCancelled)
        Me.FraFront.Controls.Add(Me.SSTab1)
        Me.FraFront.Controls.Add(Me.txtDebitAccount)
        Me.FraFront.Controls.Add(Me.txtSupplier)
        Me.FraFront.Controls.Add(Me.txtMRRDate)
        Me.FraFront.Controls.Add(Me.txtBillDate)
        Me.FraFront.Controls.Add(Me.cboInvType)
        Me.FraFront.Controls.Add(Me.txtMRRNo)
        Me.FraFront.Controls.Add(Me.CmdSearchMRR)
        Me.FraFront.Controls.Add(Me.txtBillNo)
        Me.FraFront.Controls.Add(Me.txtServNo)
        Me.FraFront.Controls.Add(Me.txtServDate)
        Me.FraFront.Controls.Add(Me.txtPONo)
        Me.FraFront.Controls.Add(Me.txtPODate)
        Me.FraFront.Controls.Add(Me.lblGSTClaimNo)
        Me.FraFront.Controls.Add(Me.lblGSTClaimDate)
        Me.FraFront.Controls.Add(Me.Label6)
        Me.FraFront.Controls.Add(Me.Label4)
        Me.FraFront.Controls.Add(Me.Label59)
        Me.FraFront.Controls.Add(Me.Label56)
        Me.FraFront.Controls.Add(Me.Label38)
        Me.FraFront.Controls.Add(Me.lblPurchaseVNo)
        Me.FraFront.Controls.Add(Me.lblVNo)
        Me.FraFront.Controls.Add(Me.lblVDate)
        Me.FraFront.Controls.Add(Me.Label12)
        Me.FraFront.Controls.Add(Me.Label3)
        Me.FraFront.Controls.Add(Me.lblCust)
        Me.FraFront.Controls.Add(Me.Label15)
        Me.FraFront.Controls.Add(Me.Label5)
        Me.FraFront.Controls.Add(Me.lblInvType)
        Me.FraFront.Controls.Add(Me.Label14)
        Me.FraFront.Controls.Add(Me.Label1)
        Me.FraFront.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraFront.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraFront.Location = New System.Drawing.Point(0, -6)
        Me.FraFront.Name = "FraFront"
        Me.FraFront.Padding = New System.Windows.Forms.Padding(0)
        Me.FraFront.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraFront.Size = New System.Drawing.Size(751, 451)
        Me.FraFront.TabIndex = 80
        Me.FraFront.TabStop = False
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
        Me.TxtShipTo.Location = New System.Drawing.Point(666, 73)
        Me.TxtShipTo.MaxLength = 0
        Me.TxtShipTo.Name = "TxtShipTo"
        Me.TxtShipTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtShipTo.Size = New System.Drawing.Size(82, 22)
        Me.TxtShipTo.TabIndex = 200
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
        Me.txtBillTo.Location = New System.Drawing.Point(666, 52)
        Me.txtBillTo.MaxLength = 0
        Me.txtBillTo.Name = "txtBillTo"
        Me.txtBillTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillTo.Size = New System.Drawing.Size(82, 22)
        Me.txtBillTo.TabIndex = 199
        '
        'chkGSTClaim
        '
        Me.chkGSTClaim.AutoSize = True
        Me.chkGSTClaim.BackColor = System.Drawing.SystemColors.Control
        Me.chkGSTClaim.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkGSTClaim.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGSTClaim.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkGSTClaim.Location = New System.Drawing.Point(350, 98)
        Me.chkGSTClaim.Name = "chkGSTClaim"
        Me.chkGSTClaim.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkGSTClaim.Size = New System.Drawing.Size(82, 18)
        Me.chkGSTClaim.TabIndex = 191
        Me.chkGSTClaim.Text = "GST Claim"
        Me.chkGSTClaim.UseVisualStyleBackColor = False
        '
        'cboGSTStatus
        '
        Me.cboGSTStatus.BackColor = System.Drawing.SystemColors.Window
        Me.cboGSTStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboGSTStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboGSTStatus.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboGSTStatus.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboGSTStatus.Location = New System.Drawing.Point(82, 116)
        Me.cboGSTStatus.Name = "cboGSTStatus"
        Me.cboGSTStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboGSTStatus.Size = New System.Drawing.Size(129, 22)
        Me.cboGSTStatus.TabIndex = 17
        '
        'chkCreditRC
        '
        Me.chkCreditRC.AutoSize = True
        Me.chkCreditRC.BackColor = System.Drawing.SystemColors.Control
        Me.chkCreditRC.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCreditRC.Enabled = False
        Me.chkCreditRC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCreditRC.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkCreditRC.Location = New System.Drawing.Point(296, 118)
        Me.chkCreditRC.Name = "chkCreditRC"
        Me.chkCreditRC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCreditRC.Size = New System.Drawing.Size(160, 18)
        Me.chkCreditRC.TabIndex = 20
        Me.chkCreditRC.Text = "Credit (Reverse Charge)"
        Me.chkCreditRC.UseVisualStyleBackColor = False
        '
        'txtShippedTo
        '
        Me.txtShippedTo.AcceptsReturn = True
        Me.txtShippedTo.BackColor = System.Drawing.SystemColors.Window
        Me.txtShippedTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtShippedTo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtShippedTo.Enabled = False
        Me.txtShippedTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShippedTo.ForeColor = System.Drawing.Color.Blue
        Me.txtShippedTo.Location = New System.Drawing.Point(446, 74)
        Me.txtShippedTo.MaxLength = 0
        Me.txtShippedTo.Name = "txtShippedTo"
        Me.txtShippedTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtShippedTo.Size = New System.Drawing.Size(220, 20)
        Me.txtShippedTo.TabIndex = 14
        '
        'chkShipTo
        '
        Me.chkShipTo.AutoSize = True
        Me.chkShipTo.BackColor = System.Drawing.SystemColors.Control
        Me.chkShipTo.Checked = True
        Me.chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkShipTo.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkShipTo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShipTo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkShipTo.Location = New System.Drawing.Point(447, 98)
        Me.chkShipTo.Name = "chkShipTo"
        Me.chkShipTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkShipTo.Size = New System.Drawing.Size(284, 18)
        Me.chkShipTo.TabIndex = 16
        Me.chkShipTo.Text = "'Shipped From' as 'Same Billed From' (Yes / No)"
        Me.chkShipTo.UseVisualStyleBackColor = False
        '
        'cboDivision
        '
        Me.cboDivision.BackColor = System.Drawing.SystemColors.Window
        Me.cboDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivision.Enabled = False
        Me.cboDivision.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDivision.Location = New System.Drawing.Point(82, 94)
        Me.cboDivision.Name = "cboDivision"
        Me.cboDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivision.Size = New System.Drawing.Size(129, 22)
        Me.cboDivision.TabIndex = 15
        '
        'chkFOC
        '
        Me.chkFOC.AutoSize = True
        Me.chkFOC.BackColor = System.Drawing.SystemColors.Control
        Me.chkFOC.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkFOC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFOC.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkFOC.Location = New System.Drawing.Point(460, 118)
        Me.chkFOC.Name = "chkFOC"
        Me.chkFOC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkFOC.Size = New System.Drawing.Size(48, 18)
        Me.chkFOC.TabIndex = 21
        Me.chkFOC.Text = "FOC"
        Me.chkFOC.UseVisualStyleBackColor = False
        '
        'chkFinalPost
        '
        Me.chkFinalPost.AutoSize = True
        Me.chkFinalPost.BackColor = System.Drawing.SystemColors.Control
        Me.chkFinalPost.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkFinalPost.Enabled = False
        Me.chkFinalPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFinalPost.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkFinalPost.Location = New System.Drawing.Point(592, 118)
        Me.chkFinalPost.Name = "chkFinalPost"
        Me.chkFinalPost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkFinalPost.Size = New System.Drawing.Size(76, 18)
        Me.chkFinalPost.TabIndex = 23
        Me.chkFinalPost.Text = "FinalPost"
        Me.chkFinalPost.UseVisualStyleBackColor = False
        '
        'ChkCapital
        '
        Me.ChkCapital.AutoSize = True
        Me.ChkCapital.BackColor = System.Drawing.SystemColors.Control
        Me.ChkCapital.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkCapital.Enabled = False
        Me.ChkCapital.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkCapital.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ChkCapital.Location = New System.Drawing.Point(668, 118)
        Me.ChkCapital.Name = "ChkCapital"
        Me.ChkCapital.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkCapital.Size = New System.Drawing.Size(76, 18)
        Me.ChkCapital.TabIndex = 13
        Me.ChkCapital.Text = "Is Capital"
        Me.ChkCapital.UseVisualStyleBackColor = False
        '
        'txtModvatNo
        '
        Me.txtModvatNo.AcceptsReturn = True
        Me.txtModvatNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtModvatNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModvatNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtModvatNo.Enabled = False
        Me.txtModvatNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtModvatNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtModvatNo.Location = New System.Drawing.Point(83, 34)
        Me.txtModvatNo.MaxLength = 0
        Me.txtModvatNo.Name = "txtModvatNo"
        Me.txtModvatNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtModvatNo.Size = New System.Drawing.Size(101, 20)
        Me.txtModvatNo.TabIndex = 4
        '
        'txtModvatDate
        '
        Me.txtModvatDate.AcceptsReturn = True
        Me.txtModvatDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtModvatDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModvatDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtModvatDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtModvatDate.ForeColor = System.Drawing.Color.Blue
        Me.txtModvatDate.Location = New System.Drawing.Point(269, 34)
        Me.txtModvatDate.MaxLength = 0
        Me.txtModvatDate.Name = "txtModvatDate"
        Me.txtModvatDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtModvatDate.Size = New System.Drawing.Size(73, 20)
        Me.txtModvatDate.TabIndex = 5
        '
        'txtVNo
        '
        Me.txtVNo.AcceptsReturn = True
        Me.txtVNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtVNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVNo.ForeColor = System.Drawing.Color.Blue
        Me.txtVNo.Location = New System.Drawing.Point(107, 14)
        Me.txtVNo.MaxLength = 0
        Me.txtVNo.Name = "txtVNo"
        Me.txtVNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVNo.Size = New System.Drawing.Size(77, 20)
        Me.txtVNo.TabIndex = 2
        Me.txtVNo.Visible = False
        '
        'txtVNoPrefix
        '
        Me.txtVNoPrefix.AcceptsReturn = True
        Me.txtVNoPrefix.BackColor = System.Drawing.SystemColors.Window
        Me.txtVNoPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVNoPrefix.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVNoPrefix.Enabled = False
        Me.txtVNoPrefix.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVNoPrefix.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtVNoPrefix.Location = New System.Drawing.Point(84, 14)
        Me.txtVNoPrefix.MaxLength = 0
        Me.txtVNoPrefix.Name = "txtVNoPrefix"
        Me.txtVNoPrefix.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVNoPrefix.Size = New System.Drawing.Size(23, 20)
        Me.txtVNoPrefix.TabIndex = 1
        '
        'txtVNoSuffix
        '
        Me.txtVNoSuffix.AcceptsReturn = True
        Me.txtVNoSuffix.BackColor = System.Drawing.SystemColors.Window
        Me.txtVNoSuffix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVNoSuffix.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVNoSuffix.Enabled = False
        Me.txtVNoSuffix.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVNoSuffix.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtVNoSuffix.Location = New System.Drawing.Point(167, 14)
        Me.txtVNoSuffix.MaxLength = 0
        Me.txtVNoSuffix.Name = "txtVNoSuffix"
        Me.txtVNoSuffix.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVNoSuffix.Size = New System.Drawing.Size(17, 20)
        Me.txtVNoSuffix.TabIndex = 118
        '
        'txtVDate
        '
        Me.txtVDate.AcceptsReturn = True
        Me.txtVDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtVDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVDate.ForeColor = System.Drawing.Color.Blue
        Me.txtVDate.Location = New System.Drawing.Point(269, 14)
        Me.txtVDate.MaxLength = 0
        Me.txtVDate.Name = "txtVDate"
        Me.txtVDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVDate.Size = New System.Drawing.Size(73, 20)
        Me.txtVDate.TabIndex = 3
        Me.txtVDate.Visible = False
        '
        'chkRejection
        '
        Me.chkRejection.AutoSize = True
        Me.chkRejection.BackColor = System.Drawing.SystemColors.Control
        Me.chkRejection.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkRejection.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRejection.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkRejection.Location = New System.Drawing.Point(218, 118)
        Me.chkRejection.Name = "chkRejection"
        Me.chkRejection.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkRejection.Size = New System.Drawing.Size(64, 18)
        Me.chkRejection.TabIndex = 19
        Me.chkRejection.Text = "Agt. D3"
        Me.chkRejection.UseVisualStyleBackColor = False
        '
        'chkCancelled
        '
        Me.chkCancelled.AutoSize = True
        Me.chkCancelled.BackColor = System.Drawing.SystemColors.Control
        Me.chkCancelled.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCancelled.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCancelled.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkCancelled.Location = New System.Drawing.Point(510, 118)
        Me.chkCancelled.Name = "chkCancelled"
        Me.chkCancelled.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCancelled.Size = New System.Drawing.Size(80, 18)
        Me.chkCancelled.TabIndex = 22
        Me.chkCancelled.Text = "Cancelled"
        Me.chkCancelled.UseVisualStyleBackColor = False
        '
        'SSTab1
        '
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage0)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage1)
        Me.SSTab1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SSTab1.ItemSize = New System.Drawing.Size(42, 18)
        Me.SSTab1.Location = New System.Drawing.Point(2, 138)
        Me.SSTab1.Name = "SSTab1"
        Me.SSTab1.SelectedIndex = 0
        Me.SSTab1.Size = New System.Drawing.Size(747, 309)
        Me.SSTab1.TabIndex = 87
        '
        '_SSTab1_TabPage0
        '
        Me._SSTab1_TabPage0.Controls.Add(Me.Frame6)
        Me._SSTab1_TabPage0.Location = New System.Drawing.Point(4, 22)
        Me._SSTab1_TabPage0.Name = "_SSTab1_TabPage0"
        Me._SSTab1_TabPage0.Size = New System.Drawing.Size(739, 283)
        Me._SSTab1_TabPage0.TabIndex = 0
        Me._SSTab1_TabPage0.Text = "Item Details"
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.SSTabLevies)
        Me.Frame6.Controls.Add(Me.SprdMain)
        Me.Frame6.Controls.Add(Me.SprdExp)
        Me.Frame6.Controls.Add(Me.lblClaimStatus)
        Me.Frame6.Controls.Add(Me.lblTotQty)
        Me.Frame6.Controls.Add(Me.lblTotPackQtyCap)
        Me.Frame6.Controls.Add(Me.lblADE1)
        Me.Frame6.Controls.Add(Me.lblServicePercentage)
        Me.Frame6.Controls.Add(Me.lblCESSableAmount)
        Me.Frame6.Controls.Add(Me.lblEDUPercent)
        Me.Frame6.Controls.Add(Me.lblMSC)
        Me.Frame6.Controls.Add(Me.lblRO)
        Me.Frame6.Controls.Add(Me.lblSurcharge)
        Me.Frame6.Controls.Add(Me.lblDiscount)
        Me.Frame6.Controls.Add(Me.lblTotTaxableAmt)
        Me.Frame6.Controls.Add(Me.lblSTPercentage)
        Me.Frame6.Controls.Add(Me.lblEDPercentage)
        Me.Frame6.Controls.Add(Me.lblTotOtherExp)
        Me.Frame6.Controls.Add(Me.lblTotFreight)
        Me.Frame6.Controls.Add(Me.lblTotCharges)
        Me.Frame6.Controls.Add(Me.LblBookCode)
        Me.Frame6.Controls.Add(Me.LblMKey)
        Me.Frame6.Controls.Add(Me.Label16)
        Me.Frame6.Controls.Add(Me.lblTotItemValue)
        Me.Frame6.Controls.Add(Me.Label13)
        Me.Frame6.Controls.Add(Me.lblNetAmount)
        Me.Frame6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(3, -5)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(739, 281)
        Me.Frame6.TabIndex = 96
        Me.Frame6.TabStop = False
        '
        'SSTabLevies
        '
        Me.SSTabLevies.Alignment = System.Windows.Forms.TabAlignment.Right
        Me.SSTabLevies.Controls.Add(Me._SSTabLevies_TabPage0)
        Me.SSTabLevies.Controls.Add(Me._SSTabLevies_TabPage1)
        Me.SSTabLevies.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SSTabLevies.ItemSize = New System.Drawing.Size(42, 12)
        Me.SSTabLevies.Location = New System.Drawing.Point(354, 176)
        Me.SSTabLevies.Multiline = True
        Me.SSTabLevies.Name = "SSTabLevies"
        Me.SSTabLevies.SelectedIndex = 1
        Me.SSTabLevies.Size = New System.Drawing.Size(383, 82)
        Me.SSTabLevies.TabIndex = 137
        Me.SSTabLevies.TabStop = False
        '
        '_SSTabLevies_TabPage0
        '
        Me._SSTabLevies_TabPage0.Controls.Add(Me.lblSeprateGST)
        Me._SSTabLevies_TabPage0.Controls.Add(Me.Label45)
        Me._SSTabLevies_TabPage0.Controls.Add(Me.lblTotSGSTAmount)
        Me._SSTabLevies_TabPage0.Controls.Add(Me.lblTotCGSTAmount)
        Me._SSTabLevies_TabPage0.Controls.Add(Me.Label17)
        Me._SSTabLevies_TabPage0.Controls.Add(Me.lblTotExpAmt)
        Me._SSTabLevies_TabPage0.Controls.Add(Me.Label34)
        Me._SSTabLevies_TabPage0.Controls.Add(Me.Label2)
        Me._SSTabLevies_TabPage0.Controls.Add(Me.lblTotIGSTAmount)
        Me._SSTabLevies_TabPage0.Location = New System.Drawing.Point(4, 4)
        Me._SSTabLevies_TabPage0.Name = "_SSTabLevies_TabPage0"
        Me._SSTabLevies_TabPage0.Size = New System.Drawing.Size(351, 74)
        Me._SSTabLevies_TabPage0.TabIndex = 0
        Me._SSTabLevies_TabPage0.Text = "Tax"
        '
        'lblSeprateGST
        '
        Me.lblSeprateGST.AutoSize = True
        Me.lblSeprateGST.BackColor = System.Drawing.SystemColors.Control
        Me.lblSeprateGST.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSeprateGST.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSeprateGST.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSeprateGST.Location = New System.Drawing.Point(46, 60)
        Me.lblSeprateGST.Name = "lblSeprateGST"
        Me.lblSeprateGST.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSeprateGST.Size = New System.Drawing.Size(76, 14)
        Me.lblSeprateGST.TabIndex = 163
        Me.lblSeprateGST.Text = "lblSeprateGST"
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.BackColor = System.Drawing.SystemColors.Control
        Me.Label45.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label45.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label45.Location = New System.Drawing.Point(212, 9)
        Me.Label45.Name = "Label45"
        Me.Label45.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label45.Size = New System.Drawing.Size(42, 14)
        Me.Label45.TabIndex = 151
        Me.Label45.Text = "SGST :"
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotSGSTAmount
        '
        Me.lblTotSGSTAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotSGSTAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotSGSTAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotSGSTAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotSGSTAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotSGSTAmount.Location = New System.Drawing.Point(260, 7)
        Me.lblTotSGSTAmount.Name = "lblTotSGSTAmount"
        Me.lblTotSGSTAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotSGSTAmount.Size = New System.Drawing.Size(85, 19)
        Me.lblTotSGSTAmount.TabIndex = 150
        Me.lblTotSGSTAmount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotCGSTAmount
        '
        Me.lblTotCGSTAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotCGSTAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotCGSTAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotCGSTAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotCGSTAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotCGSTAmount.Location = New System.Drawing.Point(82, 7)
        Me.lblTotCGSTAmount.Name = "lblTotCGSTAmount"
        Me.lblTotCGSTAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotCGSTAmount.Size = New System.Drawing.Size(85, 19)
        Me.lblTotCGSTAmount.TabIndex = 143
        Me.lblTotCGSTAmount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label17.Location = New System.Drawing.Point(38, 9)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(43, 14)
        Me.Label17.TabIndex = 142
        Me.Label17.Text = "CGST :"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotExpAmt
        '
        Me.lblTotExpAmt.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotExpAmt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotExpAmt.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotExpAmt.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotExpAmt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotExpAmt.Location = New System.Drawing.Point(260, 27)
        Me.lblTotExpAmt.Name = "lblTotExpAmt"
        Me.lblTotExpAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotExpAmt.Size = New System.Drawing.Size(85, 19)
        Me.lblTotExpAmt.TabIndex = 141
        Me.lblTotExpAmt.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.BackColor = System.Drawing.SystemColors.Control
        Me.Label34.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label34.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label34.Location = New System.Drawing.Point(208, 31)
        Me.Label34.Name = "Label34"
        Me.Label34.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label34.Size = New System.Drawing.Size(51, 14)
        Me.Label34.TabIndex = 140
        Me.Label34.Text = "Others :"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(42, 31)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(38, 14)
        Me.Label2.TabIndex = 139
        Me.Label2.Text = "IGST :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotIGSTAmount
        '
        Me.lblTotIGSTAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotIGSTAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotIGSTAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotIGSTAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotIGSTAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotIGSTAmount.Location = New System.Drawing.Point(82, 27)
        Me.lblTotIGSTAmount.Name = "lblTotIGSTAmount"
        Me.lblTotIGSTAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotIGSTAmount.Size = New System.Drawing.Size(85, 19)
        Me.lblTotIGSTAmount.TabIndex = 138
        Me.lblTotIGSTAmount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_SSTabLevies_TabPage1
        '
        Me._SSTabLevies_TabPage1.Controls.Add(Me.Label48)
        Me._SSTabLevies_TabPage1.Controls.Add(Me.Label44)
        Me._SSTabLevies_TabPage1.Controls.Add(Me.Label20)
        Me._SSTabLevies_TabPage1.Controls.Add(Me.lblSaleBillNoSeq)
        Me._SSTabLevies_TabPage1.Controls.Add(Me.lblSaleBillNo)
        Me._SSTabLevies_TabPage1.Controls.Add(Me.lblSaleBillDate)
        Me._SSTabLevies_TabPage1.Controls.Add(Me.lblPurchaseType)
        Me._SSTabLevies_TabPage1.Controls.Add(Me.lblPurchaseSeqType)
        Me._SSTabLevies_TabPage1.Controls.Add(Me.txtTotSGSTRefund)
        Me._SSTabLevies_TabPage1.Controls.Add(Me.txtTotIGSTRefund)
        Me._SSTabLevies_TabPage1.Controls.Add(Me.txtTotCGSTRefund)
        Me._SSTabLevies_TabPage1.Location = New System.Drawing.Point(4, 4)
        Me._SSTabLevies_TabPage1.Name = "_SSTabLevies_TabPage1"
        Me._SSTabLevies_TabPage1.Size = New System.Drawing.Size(351, 74)
        Me._SSTabLevies_TabPage1.TabIndex = 1
        Me._SSTabLevies_TabPage1.Text = "Modvat"
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.BackColor = System.Drawing.SystemColors.Control
        Me.Label48.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label48.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label48.Location = New System.Drawing.Point(190, 26)
        Me.Label48.Name = "Label48"
        Me.Label48.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label48.Size = New System.Drawing.Size(84, 14)
        Me.Label48.TabIndex = 147
        Me.Label48.Text = "SGST Refund :"
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.BackColor = System.Drawing.SystemColors.Control
        Me.Label44.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label44.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label44.Location = New System.Drawing.Point(190, 46)
        Me.Label44.Name = "Label44"
        Me.Label44.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label44.Size = New System.Drawing.Size(80, 14)
        Me.Label44.TabIndex = 148
        Me.Label44.Text = "IGST Refund :"
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(190, 6)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(95, 13)
        Me.Label20.TabIndex = 149
        Me.Label20.Text = "CGST Refund :"
        '
        'lblSaleBillNoSeq
        '
        Me.lblSaleBillNoSeq.AutoSize = True
        Me.lblSaleBillNoSeq.BackColor = System.Drawing.SystemColors.Control
        Me.lblSaleBillNoSeq.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSaleBillNoSeq.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSaleBillNoSeq.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSaleBillNoSeq.Location = New System.Drawing.Point(78, 12)
        Me.lblSaleBillNoSeq.Name = "lblSaleBillNoSeq"
        Me.lblSaleBillNoSeq.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSaleBillNoSeq.Size = New System.Drawing.Size(83, 14)
        Me.lblSaleBillNoSeq.TabIndex = 164
        Me.lblSaleBillNoSeq.Text = "lblSaleBillNoSeq"
        '
        'lblSaleBillNo
        '
        Me.lblSaleBillNo.AutoSize = True
        Me.lblSaleBillNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblSaleBillNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSaleBillNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSaleBillNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSaleBillNo.Location = New System.Drawing.Point(74, 30)
        Me.lblSaleBillNo.Name = "lblSaleBillNo"
        Me.lblSaleBillNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSaleBillNo.Size = New System.Drawing.Size(64, 14)
        Me.lblSaleBillNo.TabIndex = 165
        Me.lblSaleBillNo.Text = "lblSaleBillNo"
        '
        'lblSaleBillDate
        '
        Me.lblSaleBillDate.AutoSize = True
        Me.lblSaleBillDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblSaleBillDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSaleBillDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSaleBillDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSaleBillDate.Location = New System.Drawing.Point(84, 46)
        Me.lblSaleBillDate.Name = "lblSaleBillDate"
        Me.lblSaleBillDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSaleBillDate.Size = New System.Drawing.Size(73, 14)
        Me.lblSaleBillDate.TabIndex = 166
        Me.lblSaleBillDate.Text = "lblSaleBillDate"
        '
        'lblPurchaseType
        '
        Me.lblPurchaseType.BackColor = System.Drawing.SystemColors.Control
        Me.lblPurchaseType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPurchaseType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPurchaseType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPurchaseType.Location = New System.Drawing.Point(134, 30)
        Me.lblPurchaseType.Name = "lblPurchaseType"
        Me.lblPurchaseType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPurchaseType.Size = New System.Drawing.Size(45, 15)
        Me.lblPurchaseType.TabIndex = 167
        Me.lblPurchaseType.Text = "lblPurchaseType"
        '
        'lblPurchaseSeqType
        '
        Me.lblPurchaseSeqType.BackColor = System.Drawing.SystemColors.Control
        Me.lblPurchaseSeqType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPurchaseSeqType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPurchaseSeqType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPurchaseSeqType.Location = New System.Drawing.Point(148, 60)
        Me.lblPurchaseSeqType.Name = "lblPurchaseSeqType"
        Me.lblPurchaseSeqType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPurchaseSeqType.Size = New System.Drawing.Size(63, 17)
        Me.lblPurchaseSeqType.TabIndex = 193
        Me.lblPurchaseSeqType.Text = "lblPurchaseSeqType"
        '
        'txtTotSGSTRefund
        '
        Me.txtTotSGSTRefund.AcceptsReturn = True
        Me.txtTotSGSTRefund.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotSGSTRefund.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotSGSTRefund.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotSGSTRefund.Enabled = False
        Me.txtTotSGSTRefund.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotSGSTRefund.ForeColor = System.Drawing.Color.Blue
        Me.txtTotSGSTRefund.Location = New System.Drawing.Point(278, 24)
        Me.txtTotSGSTRefund.MaxLength = 0
        Me.txtTotSGSTRefund.Name = "txtTotSGSTRefund"
        Me.txtTotSGSTRefund.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotSGSTRefund.Size = New System.Drawing.Size(77, 20)
        Me.txtTotSGSTRefund.TabIndex = 144
        '
        'txtTotIGSTRefund
        '
        Me.txtTotIGSTRefund.AcceptsReturn = True
        Me.txtTotIGSTRefund.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotIGSTRefund.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotIGSTRefund.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotIGSTRefund.Enabled = False
        Me.txtTotIGSTRefund.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotIGSTRefund.ForeColor = System.Drawing.Color.Blue
        Me.txtTotIGSTRefund.Location = New System.Drawing.Point(278, 44)
        Me.txtTotIGSTRefund.MaxLength = 0
        Me.txtTotIGSTRefund.Name = "txtTotIGSTRefund"
        Me.txtTotIGSTRefund.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotIGSTRefund.Size = New System.Drawing.Size(77, 20)
        Me.txtTotIGSTRefund.TabIndex = 145
        '
        'txtTotCGSTRefund
        '
        Me.txtTotCGSTRefund.AcceptsReturn = True
        Me.txtTotCGSTRefund.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotCGSTRefund.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotCGSTRefund.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotCGSTRefund.Enabled = False
        Me.txtTotCGSTRefund.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotCGSTRefund.ForeColor = System.Drawing.Color.Blue
        Me.txtTotCGSTRefund.Location = New System.Drawing.Point(278, 4)
        Me.txtTotCGSTRefund.MaxLength = 0
        Me.txtTotCGSTRefund.Name = "txtTotCGSTRefund"
        Me.txtTotCGSTRefund.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotCGSTRefund.Size = New System.Drawing.Size(77, 20)
        Me.txtTotCGSTRefund.TabIndex = 146
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 9)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(734, 151)
        Me.SprdMain.TabIndex = 24
        '
        'SprdExp
        '
        Me.SprdExp.DataSource = Nothing
        Me.SprdExp.Location = New System.Drawing.Point(2, 160)
        Me.SprdExp.Name = "SprdExp"
        Me.SprdExp.OcxState = CType(resources.GetObject("SprdExp.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdExp.Size = New System.Drawing.Size(351, 117)
        Me.SprdExp.TabIndex = 27
        '
        'lblClaimStatus
        '
        Me.lblClaimStatus.BackColor = System.Drawing.SystemColors.Control
        Me.lblClaimStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblClaimStatus.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClaimStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblClaimStatus.Location = New System.Drawing.Point(430, 260)
        Me.lblClaimStatus.Name = "lblClaimStatus"
        Me.lblClaimStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblClaimStatus.Size = New System.Drawing.Size(73, 13)
        Me.lblClaimStatus.TabIndex = 192
        Me.lblClaimStatus.Text = "lblClaimStatus"
        '
        'lblTotQty
        '
        Me.lblTotQty.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotQty.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotQty.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotQty.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotQty.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotQty.Location = New System.Drawing.Point(434, 159)
        Me.lblTotQty.Name = "lblTotQty"
        Me.lblTotQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotQty.Size = New System.Drawing.Size(91, 17)
        Me.lblTotQty.TabIndex = 98
        Me.lblTotQty.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotPackQtyCap
        '
        Me.lblTotPackQtyCap.AutoSize = True
        Me.lblTotPackQtyCap.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotPackQtyCap.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotPackQtyCap.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotPackQtyCap.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotPackQtyCap.Location = New System.Drawing.Point(371, 160)
        Me.lblTotPackQtyCap.Name = "lblTotPackQtyCap"
        Me.lblTotPackQtyCap.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotPackQtyCap.Size = New System.Drawing.Size(60, 14)
        Me.lblTotPackQtyCap.TabIndex = 97
        Me.lblTotPackQtyCap.Text = "Total Qty :"
        Me.lblTotPackQtyCap.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblADE1
        '
        Me.lblADE1.AutoSize = True
        Me.lblADE1.BackColor = System.Drawing.SystemColors.Control
        Me.lblADE1.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblADE1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblADE1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblADE1.Location = New System.Drawing.Point(424, 186)
        Me.lblADE1.Name = "lblADE1"
        Me.lblADE1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblADE1.Size = New System.Drawing.Size(38, 14)
        Me.lblADE1.TabIndex = 136
        Me.lblADE1.Text = "lblADE"
        '
        'lblServicePercentage
        '
        Me.lblServicePercentage.BackColor = System.Drawing.SystemColors.Control
        Me.lblServicePercentage.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblServicePercentage.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblServicePercentage.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblServicePercentage.Location = New System.Drawing.Point(384, 200)
        Me.lblServicePercentage.Name = "lblServicePercentage"
        Me.lblServicePercentage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblServicePercentage.Size = New System.Drawing.Size(38, 13)
        Me.lblServicePercentage.TabIndex = 129
        Me.lblServicePercentage.Text = "lblServicePercentage"
        Me.lblServicePercentage.Visible = False
        '
        'lblCESSableAmount
        '
        Me.lblCESSableAmount.AutoSize = True
        Me.lblCESSableAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblCESSableAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCESSableAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCESSableAmount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCESSableAmount.Location = New System.Drawing.Point(470, 198)
        Me.lblCESSableAmount.Name = "lblCESSableAmount"
        Me.lblCESSableAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCESSableAmount.Size = New System.Drawing.Size(101, 14)
        Me.lblCESSableAmount.TabIndex = 128
        Me.lblCESSableAmount.Text = "lblCESSableAmount"
        Me.lblCESSableAmount.Visible = False
        '
        'lblEDUPercent
        '
        Me.lblEDUPercent.BackColor = System.Drawing.SystemColors.Control
        Me.lblEDUPercent.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblEDUPercent.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEDUPercent.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblEDUPercent.Location = New System.Drawing.Point(490, 184)
        Me.lblEDUPercent.Name = "lblEDUPercent"
        Me.lblEDUPercent.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblEDUPercent.Size = New System.Drawing.Size(77, 15)
        Me.lblEDUPercent.TabIndex = 127
        Me.lblEDUPercent.Text = "lblEDUPercent"
        Me.lblEDUPercent.Visible = False
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
        Me.lblMSC.TabIndex = 115
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
        Me.lblRO.TabIndex = 114
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
        Me.lblSurcharge.TabIndex = 113
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
        Me.lblDiscount.TabIndex = 112
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
        Me.lblTotTaxableAmt.TabIndex = 111
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
        Me.lblSTPercentage.TabIndex = 110
        Me.lblSTPercentage.Text = "0"
        Me.lblSTPercentage.Visible = False
        '
        'lblEDPercentage
        '
        Me.lblEDPercentage.AutoSize = True
        Me.lblEDPercentage.BackColor = System.Drawing.SystemColors.Control
        Me.lblEDPercentage.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblEDPercentage.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEDPercentage.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblEDPercentage.Location = New System.Drawing.Point(460, 214)
        Me.lblEDPercentage.Name = "lblEDPercentage"
        Me.lblEDPercentage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblEDPercentage.Size = New System.Drawing.Size(13, 14)
        Me.lblEDPercentage.TabIndex = 109
        Me.lblEDPercentage.Text = "0"
        Me.lblEDPercentage.Visible = False
        '
        'lblTotOtherExp
        '
        Me.lblTotOtherExp.AutoSize = True
        Me.lblTotOtherExp.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotOtherExp.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotOtherExp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotOtherExp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTotOtherExp.Location = New System.Drawing.Point(380, 214)
        Me.lblTotOtherExp.Name = "lblTotOtherExp"
        Me.lblTotOtherExp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotOtherExp.Size = New System.Drawing.Size(13, 14)
        Me.lblTotOtherExp.TabIndex = 108
        Me.lblTotOtherExp.Text = "0"
        Me.lblTotOtherExp.Visible = False
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
        Me.lblTotFreight.TabIndex = 107
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
        Me.lblTotCharges.TabIndex = 106
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
        Me.LblBookCode.TabIndex = 104
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
        Me.LblMKey.TabIndex = 103
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
        Me.Label16.Location = New System.Drawing.Point(587, 160)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(71, 14)
        Me.Label16.TabIndex = 102
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
        Me.lblTotItemValue.Location = New System.Drawing.Point(658, 159)
        Me.lblTotItemValue.Name = "lblTotItemValue"
        Me.lblTotItemValue.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotItemValue.Size = New System.Drawing.Size(77, 17)
        Me.lblTotItemValue.TabIndex = 101
        Me.lblTotItemValue.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label13.Location = New System.Drawing.Point(581, 260)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(78, 14)
        Me.Label13.TabIndex = 100
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
        Me.lblNetAmount.Location = New System.Drawing.Point(658, 258)
        Me.lblNetAmount.Name = "lblNetAmount"
        Me.lblNetAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblNetAmount.Size = New System.Drawing.Size(77, 19)
        Me.lblNetAmount.TabIndex = 99
        Me.lblNetAmount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_SSTab1_TabPage1
        '
        Me._SSTab1_TabPage1.Controls.Add(Me.Frame1)
        Me._SSTab1_TabPage1.Location = New System.Drawing.Point(4, 22)
        Me._SSTab1_TabPage1.Name = "_SSTab1_TabPage1"
        Me._SSTab1_TabPage1.Size = New System.Drawing.Size(739, 283)
        Me._SSTab1_TabPage1.TabIndex = 1
        Me._SSTab1_TabPage1.Text = "Other Details"
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.Frame4)
        Me.Frame1.Controls.Add(Me.Frame2)
        Me.Frame1.Controls.Add(Me._txtCreditDays_0)
        Me.Frame1.Controls.Add(Me._txtCreditDays_1)
        Me.Frame1.Controls.Add(Me.Frame7)
        Me.Frame1.Controls.Add(Me.txtPaymentdate)
        Me.Frame1.Controls.Add(Me.txtTariff)
        Me.Frame1.Controls.Add(Me.FraServiceTax)
        Me.Frame1.Controls.Add(Me.Frame8)
        Me.Frame1.Controls.Add(Me.txtNarration)
        Me.Frame1.Controls.Add(Me.txtDocsThru)
        Me.Frame1.Controls.Add(Me.txtCarriers)
        Me.Frame1.Controls.Add(Me.txtVehicle)
        Me.Frame1.Controls.Add(Me.txtMode)
        Me.Frame1.Controls.Add(Me.txtRemarks)
        Me.Frame1.Controls.Add(Me.txtItemType)
        Me.Frame1.Controls.Add(Me.Label33)
        Me.Frame1.Controls.Add(Me.Label35)
        Me.Frame1.Controls.Add(Me.Label37)
        Me.Frame1.Controls.Add(Me.Label7)
        Me.Frame1.Controls.Add(Me.Label9)
        Me.Frame1.Controls.Add(Me.Label32)
        Me.Frame1.Controls.Add(Me.Label31)
        Me.Frame1.Controls.Add(Me.Label30)
        Me.Frame1.Controls.Add(Me.Label29)
        Me.Frame1.Controls.Add(Me.Label27)
        Me.Frame1.Controls.Add(Me.Label26)
        Me.Frame1.Controls.Add(Me.Label11)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(1, -2)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(739, 281)
        Me.Frame1.TabIndex = 88
        Me.Frame1.TabStop = False
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.txtRecipientPer)
        Me.Frame4.Controls.Add(Me.txtProviderPer)
        Me.Frame4.Controls.Add(Me.txtServiceTaxAmount)
        Me.Frame4.Controls.Add(Me.txtServiceTaxPer)
        Me.Frame4.Controls.Add(Me.txtServiceOn)
        Me.Frame4.Controls.Add(Me.Label61)
        Me.Frame4.Controls.Add(Me.Label60)
        Me.Frame4.Controls.Add(Me.Label28)
        Me.Frame4.Controls.Add(Me.Label63)
        Me.Frame4.Controls.Add(Me.Label62)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(646, 14)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(93, 93)
        Me.Frame4.TabIndex = 175
        Me.Frame4.TabStop = False
        Me.Frame4.Text = "Frame4"
        Me.Frame4.Visible = False
        '
        'txtRecipientPer
        '
        Me.txtRecipientPer.AcceptsReturn = True
        Me.txtRecipientPer.BackColor = System.Drawing.SystemColors.Window
        Me.txtRecipientPer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRecipientPer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRecipientPer.Enabled = False
        Me.txtRecipientPer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRecipientPer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRecipientPer.Location = New System.Drawing.Point(42, 46)
        Me.txtRecipientPer.MaxLength = 0
        Me.txtRecipientPer.Name = "txtRecipientPer"
        Me.txtRecipientPer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRecipientPer.Size = New System.Drawing.Size(33, 20)
        Me.txtRecipientPer.TabIndex = 184
        '
        'txtProviderPer
        '
        Me.txtProviderPer.AcceptsReturn = True
        Me.txtProviderPer.BackColor = System.Drawing.SystemColors.Window
        Me.txtProviderPer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtProviderPer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtProviderPer.Enabled = False
        Me.txtProviderPer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProviderPer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtProviderPer.Location = New System.Drawing.Point(45, 48)
        Me.txtProviderPer.MaxLength = 0
        Me.txtProviderPer.Name = "txtProviderPer"
        Me.txtProviderPer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProviderPer.Size = New System.Drawing.Size(33, 20)
        Me.txtProviderPer.TabIndex = 182
        '
        'txtServiceTaxAmount
        '
        Me.txtServiceTaxAmount.AcceptsReturn = True
        Me.txtServiceTaxAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtServiceTaxAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtServiceTaxAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtServiceTaxAmount.Enabled = False
        Me.txtServiceTaxAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtServiceTaxAmount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtServiceTaxAmount.Location = New System.Drawing.Point(56, 58)
        Me.txtServiceTaxAmount.MaxLength = 0
        Me.txtServiceTaxAmount.Name = "txtServiceTaxAmount"
        Me.txtServiceTaxAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtServiceTaxAmount.Size = New System.Drawing.Size(31, 20)
        Me.txtServiceTaxAmount.TabIndex = 180
        '
        'txtServiceTaxPer
        '
        Me.txtServiceTaxPer.AcceptsReturn = True
        Me.txtServiceTaxPer.BackColor = System.Drawing.SystemColors.Window
        Me.txtServiceTaxPer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtServiceTaxPer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtServiceTaxPer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtServiceTaxPer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtServiceTaxPer.Location = New System.Drawing.Point(26, 38)
        Me.txtServiceTaxPer.MaxLength = 0
        Me.txtServiceTaxPer.Name = "txtServiceTaxPer"
        Me.txtServiceTaxPer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtServiceTaxPer.Size = New System.Drawing.Size(37, 20)
        Me.txtServiceTaxPer.TabIndex = 178
        '
        'txtServiceOn
        '
        Me.txtServiceOn.AcceptsReturn = True
        Me.txtServiceOn.BackColor = System.Drawing.SystemColors.Window
        Me.txtServiceOn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtServiceOn.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtServiceOn.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtServiceOn.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtServiceOn.Location = New System.Drawing.Point(4, 16)
        Me.txtServiceOn.MaxLength = 0
        Me.txtServiceOn.Name = "txtServiceOn"
        Me.txtServiceOn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtServiceOn.Size = New System.Drawing.Size(79, 20)
        Me.txtServiceOn.TabIndex = 177
        '
        'Label61
        '
        Me.Label61.AutoSize = True
        Me.Label61.BackColor = System.Drawing.SystemColors.Control
        Me.Label61.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label61.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label61.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label61.Location = New System.Drawing.Point(6, 48)
        Me.Label61.Name = "Label61"
        Me.Label61.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label61.Size = New System.Drawing.Size(76, 14)
        Me.Label61.TabIndex = 185
        Me.Label61.Text = "Recipient % :"
        '
        'Label60
        '
        Me.Label60.AutoSize = True
        Me.Label60.BackColor = System.Drawing.SystemColors.Control
        Me.Label60.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label60.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label60.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label60.Location = New System.Drawing.Point(4, 50)
        Me.Label60.Name = "Label60"
        Me.Label60.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label60.Size = New System.Drawing.Size(72, 14)
        Me.Label60.TabIndex = 183
        Me.Label60.Text = "Provider % :"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.SystemColors.Control
        Me.Label28.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label28.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label28.Location = New System.Drawing.Point(4, 60)
        Me.Label28.Name = "Label28"
        Me.Label28.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label28.Size = New System.Drawing.Size(57, 14)
        Me.Label28.TabIndex = 181
        Me.Label28.Text = "Amount :"
        '
        'Label63
        '
        Me.Label63.AutoSize = True
        Me.Label63.BackColor = System.Drawing.SystemColors.Control
        Me.Label63.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label63.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label63.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label63.Location = New System.Drawing.Point(6, 40)
        Me.Label63.Name = "Label63"
        Me.Label63.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label63.Size = New System.Drawing.Size(22, 14)
        Me.Label63.TabIndex = 179
        Me.Label63.Text = "% :"
        '
        'Label62
        '
        Me.Label62.AutoSize = True
        Me.Label62.BackColor = System.Drawing.SystemColors.Control
        Me.Label62.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label62.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label62.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label62.Location = New System.Drawing.Point(4, 30)
        Me.Label62.Name = "Label62"
        Me.Label62.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label62.Size = New System.Drawing.Size(93, 14)
        Me.Label62.TabIndex = 176
        Me.Label62.Text = "Service Tax On :"
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.txtAdvAdjust)
        Me.Frame2.Controls.Add(Me.txtAdvVNo)
        Me.Frame2.Controls.Add(Me.txtAdvDate)
        Me.Frame2.Controls.Add(Me.txtAdvIGST)
        Me.Frame2.Controls.Add(Me.txtAdvSGST)
        Me.Frame2.Controls.Add(Me.txtAdvCGST)
        Me.Frame2.Controls.Add(Me.txtItemAdvAdjust)
        Me.Frame2.Controls.Add(Me.txtAdvBal)
        Me.Frame2.Controls.Add(Me.Label24)
        Me.Frame2.Controls.Add(Me.Label23)
        Me.Frame2.Controls.Add(Me.Label18)
        Me.Frame2.Controls.Add(Me.Label22)
        Me.Frame2.Controls.Add(Me.Label21)
        Me.Frame2.Controls.Add(Me.Label19)
        Me.Frame2.Controls.Add(Me.Label10)
        Me.Frame2.Controls.Add(Me.Label8)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(366, 108)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(373, 173)
        Me.Frame2.TabIndex = 169
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Advance Details"
        '
        'txtAdvAdjust
        '
        Me.txtAdvAdjust.AcceptsReturn = True
        Me.txtAdvAdjust.BackColor = System.Drawing.SystemColors.Window
        Me.txtAdvAdjust.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAdvAdjust.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAdvAdjust.Enabled = False
        Me.txtAdvAdjust.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdvAdjust.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAdvAdjust.Location = New System.Drawing.Point(293, 126)
        Me.txtAdvAdjust.MaxLength = 0
        Me.txtAdvAdjust.Name = "txtAdvAdjust"
        Me.txtAdvAdjust.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAdvAdjust.Size = New System.Drawing.Size(73, 20)
        Me.txtAdvAdjust.TabIndex = 65
        Me.txtAdvAdjust.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtAdvVNo
        '
        Me.txtAdvVNo.AcceptsReturn = True
        Me.txtAdvVNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtAdvVNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAdvVNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAdvVNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdvVNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtAdvVNo.Location = New System.Drawing.Point(108, 16)
        Me.txtAdvVNo.MaxLength = 0
        Me.txtAdvVNo.Name = "txtAdvVNo"
        Me.txtAdvVNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAdvVNo.Size = New System.Drawing.Size(77, 20)
        Me.txtAdvVNo.TabIndex = 58
        '
        'txtAdvDate
        '
        Me.txtAdvDate.AcceptsReturn = True
        Me.txtAdvDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtAdvDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAdvDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAdvDate.Enabled = False
        Me.txtAdvDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdvDate.ForeColor = System.Drawing.Color.Blue
        Me.txtAdvDate.Location = New System.Drawing.Point(293, 16)
        Me.txtAdvDate.MaxLength = 0
        Me.txtAdvDate.Name = "txtAdvDate"
        Me.txtAdvDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAdvDate.Size = New System.Drawing.Size(73, 20)
        Me.txtAdvDate.TabIndex = 59
        '
        'txtAdvIGST
        '
        Me.txtAdvIGST.AcceptsReturn = True
        Me.txtAdvIGST.BackColor = System.Drawing.SystemColors.Window
        Me.txtAdvIGST.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAdvIGST.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAdvIGST.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdvIGST.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAdvIGST.Location = New System.Drawing.Point(293, 104)
        Me.txtAdvIGST.MaxLength = 0
        Me.txtAdvIGST.Name = "txtAdvIGST"
        Me.txtAdvIGST.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAdvIGST.Size = New System.Drawing.Size(73, 20)
        Me.txtAdvIGST.TabIndex = 64
        Me.txtAdvIGST.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtAdvSGST
        '
        Me.txtAdvSGST.AcceptsReturn = True
        Me.txtAdvSGST.BackColor = System.Drawing.SystemColors.Window
        Me.txtAdvSGST.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAdvSGST.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAdvSGST.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdvSGST.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAdvSGST.Location = New System.Drawing.Point(293, 82)
        Me.txtAdvSGST.MaxLength = 0
        Me.txtAdvSGST.Name = "txtAdvSGST"
        Me.txtAdvSGST.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAdvSGST.Size = New System.Drawing.Size(73, 20)
        Me.txtAdvSGST.TabIndex = 63
        Me.txtAdvSGST.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtAdvCGST
        '
        Me.txtAdvCGST.AcceptsReturn = True
        Me.txtAdvCGST.BackColor = System.Drawing.SystemColors.Window
        Me.txtAdvCGST.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAdvCGST.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAdvCGST.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdvCGST.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAdvCGST.Location = New System.Drawing.Point(293, 60)
        Me.txtAdvCGST.MaxLength = 0
        Me.txtAdvCGST.Name = "txtAdvCGST"
        Me.txtAdvCGST.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAdvCGST.Size = New System.Drawing.Size(73, 20)
        Me.txtAdvCGST.TabIndex = 62
        Me.txtAdvCGST.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtItemAdvAdjust
        '
        Me.txtItemAdvAdjust.AcceptsReturn = True
        Me.txtItemAdvAdjust.BackColor = System.Drawing.SystemColors.Window
        Me.txtItemAdvAdjust.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemAdvAdjust.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItemAdvAdjust.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemAdvAdjust.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtItemAdvAdjust.Location = New System.Drawing.Point(293, 38)
        Me.txtItemAdvAdjust.MaxLength = 0
        Me.txtItemAdvAdjust.Name = "txtItemAdvAdjust"
        Me.txtItemAdvAdjust.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemAdvAdjust.Size = New System.Drawing.Size(73, 20)
        Me.txtItemAdvAdjust.TabIndex = 61
        Me.txtItemAdvAdjust.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtAdvBal
        '
        Me.txtAdvBal.AcceptsReturn = True
        Me.txtAdvBal.BackColor = System.Drawing.SystemColors.Window
        Me.txtAdvBal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAdvBal.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAdvBal.Enabled = False
        Me.txtAdvBal.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdvBal.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAdvBal.Location = New System.Drawing.Point(109, 38)
        Me.txtAdvBal.MaxLength = 0
        Me.txtAdvBal.Name = "txtAdvBal"
        Me.txtAdvBal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAdvBal.Size = New System.Drawing.Size(75, 20)
        Me.txtAdvBal.TabIndex = 60
        Me.txtAdvBal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.SystemColors.Control
        Me.Label24.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label24.Location = New System.Drawing.Point(155, 128)
        Me.Label24.Name = "Label24"
        Me.Label24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label24.Size = New System.Drawing.Size(139, 14)
        Me.Label24.TabIndex = 188
        Me.Label24.Text = "Total Adjusted Amount :"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.SystemColors.Control
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(6, 18)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(86, 14)
        Me.Label23.TabIndex = 187
        Me.Label23.Text = "Payment VNo :"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(188, 18)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(37, 14)
        Me.Label18.TabIndex = 186
        Me.Label18.Text = "Date :"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.SystemColors.Control
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(208, 106)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(85, 14)
        Me.Label22.TabIndex = 174
        Me.Label22.Text = "IGST Amount :"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.SystemColors.Control
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(204, 84)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(89, 14)
        Me.Label21.TabIndex = 173
        Me.Label21.Text = "SGST Amount :"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(188, 62)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(90, 14)
        Me.Label19.TabIndex = 172
        Me.Label19.Text = "CGST Amount :"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(187, 40)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(107, 14)
        Me.Label10.TabIndex = 171
        Me.Label10.Text = "Advance Amount :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(6, 40)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(102, 14)
        Me.Label8.TabIndex = 170
        Me.Label8.Text = "Balance Amount :"
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
        Me._txtCreditDays_0.Location = New System.Drawing.Point(134, 170)
        Me._txtCreditDays_0.MaxLength = 0
        Me._txtCreditDays_0.Name = "_txtCreditDays_0"
        Me._txtCreditDays_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtCreditDays_0.Size = New System.Drawing.Size(37, 20)
        Me._txtCreditDays_0.TabIndex = 50
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
        Me._txtCreditDays_1.Location = New System.Drawing.Point(214, 170)
        Me._txtCreditDays_1.MaxLength = 0
        Me._txtCreditDays_1.Name = "_txtCreditDays_1"
        Me._txtCreditDays_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtCreditDays_1.Size = New System.Drawing.Size(37, 20)
        Me._txtCreditDays_1.TabIndex = 51
        '
        'Frame7
        '
        Me.Frame7.BackColor = System.Drawing.SystemColors.Control
        Me.Frame7.Controls.Add(Me._OptFreight_0)
        Me.Frame7.Controls.Add(Me._OptFreight_1)
        Me.Frame7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame7.Location = New System.Drawing.Point(266, 224)
        Me.Frame7.Name = "Frame7"
        Me.Frame7.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame7.Size = New System.Drawing.Size(99, 57)
        Me.Frame7.TabIndex = 52
        Me.Frame7.TabStop = False
        Me.Frame7.Text = "Freight"
        '
        '_OptFreight_0
        '
        Me._OptFreight_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptFreight_0.Checked = True
        Me._OptFreight_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptFreight_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptFreight_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptFreight.SetIndex(Me._OptFreight_0, CType(0, Short))
        Me._OptFreight_0.Location = New System.Drawing.Point(18, 18)
        Me._OptFreight_0.Name = "_OptFreight_0"
        Me._OptFreight_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptFreight_0.Size = New System.Drawing.Size(65, 13)
        Me._OptFreight_0.TabIndex = 157
        Me._OptFreight_0.TabStop = True
        Me._OptFreight_0.Text = "To Pay"
        Me._OptFreight_0.UseVisualStyleBackColor = False
        '
        '_OptFreight_1
        '
        Me._OptFreight_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptFreight_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptFreight_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptFreight_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptFreight.SetIndex(Me._OptFreight_1, CType(1, Short))
        Me._OptFreight_1.Location = New System.Drawing.Point(18, 38)
        Me._OptFreight_1.Name = "_OptFreight_1"
        Me._OptFreight_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptFreight_1.Size = New System.Drawing.Size(49, 13)
        Me._OptFreight_1.TabIndex = 156
        Me._OptFreight_1.TabStop = True
        Me._OptFreight_1.Text = "Paid"
        Me._OptFreight_1.UseVisualStyleBackColor = False
        '
        'txtPaymentdate
        '
        Me.txtPaymentdate.AcceptsReturn = True
        Me.txtPaymentdate.BackColor = System.Drawing.SystemColors.Window
        Me.txtPaymentdate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPaymentdate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPaymentdate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaymentdate.ForeColor = System.Drawing.Color.Blue
        Me.txtPaymentdate.Location = New System.Drawing.Point(96, 150)
        Me.txtPaymentdate.MaxLength = 0
        Me.txtPaymentdate.Name = "txtPaymentdate"
        Me.txtPaymentdate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPaymentdate.Size = New System.Drawing.Size(73, 20)
        Me.txtPaymentdate.TabIndex = 48
        '
        'txtTariff
        '
        Me.txtTariff.AcceptsReturn = True
        Me.txtTariff.BackColor = System.Drawing.SystemColors.Window
        Me.txtTariff.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTariff.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTariff.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTariff.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTariff.Location = New System.Drawing.Point(251, 150)
        Me.txtTariff.MaxLength = 0
        Me.txtTariff.Name = "txtTariff"
        Me.txtTariff.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTariff.Size = New System.Drawing.Size(111, 20)
        Me.txtTariff.TabIndex = 49
        '
        'FraServiceTax
        '
        Me.FraServiceTax.BackColor = System.Drawing.SystemColors.Control
        Me.FraServiceTax.Controls.Add(Me.txtServProvided)
        Me.FraServiceTax.Controls.Add(Me.Label53)
        Me.FraServiceTax.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraServiceTax.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraServiceTax.Location = New System.Drawing.Point(0, 188)
        Me.FraServiceTax.Name = "FraServiceTax"
        Me.FraServiceTax.Padding = New System.Windows.Forms.Padding(0)
        Me.FraServiceTax.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraServiceTax.Size = New System.Drawing.Size(365, 35)
        Me.FraServiceTax.TabIndex = 56
        Me.FraServiceTax.TabStop = False
        Me.FraServiceTax.Text = "Service Tax "
        '
        'txtServProvided
        '
        Me.txtServProvided.AcceptsReturn = True
        Me.txtServProvided.BackColor = System.Drawing.SystemColors.Window
        Me.txtServProvided.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtServProvided.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtServProvided.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtServProvided.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtServProvided.Location = New System.Drawing.Point(115, 10)
        Me.txtServProvided.MaxLength = 0
        Me.txtServProvided.Name = "txtServProvided"
        Me.txtServProvided.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtServProvided.Size = New System.Drawing.Size(245, 20)
        Me.txtServProvided.TabIndex = 57
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.BackColor = System.Drawing.SystemColors.Control
        Me.Label53.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label53.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label53.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label53.Location = New System.Drawing.Point(8, 12)
        Me.Label53.Name = "Label53"
        Me.Label53.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label53.Size = New System.Drawing.Size(106, 14)
        Me.Label53.TabIndex = 132
        Me.Label53.Text = "Service Provided :"
        Me.Label53.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame8
        '
        Me.Frame8.BackColor = System.Drawing.SystemColors.Control
        Me.Frame8.Controls.Add(Me.ChkSTDSRO)
        Me.Frame8.Controls.Add(Me.ChkESIRO)
        Me.Frame8.Controls.Add(Me.ChkTDSRO)
        Me.Frame8.Controls.Add(Me.txtSTDSDeductOn)
        Me.Frame8.Controls.Add(Me.txtESIDeductOn)
        Me.Frame8.Controls.Add(Me.txtTDSDeductOn)
        Me.Frame8.Controls.Add(Me.txtJVVNO)
        Me.Frame8.Controls.Add(Me.ChkSTDS)
        Me.Frame8.Controls.Add(Me.txtSTDSRate)
        Me.Frame8.Controls.Add(Me.txtSTDSAmount)
        Me.Frame8.Controls.Add(Me.chkESI)
        Me.Frame8.Controls.Add(Me.txtESIRate)
        Me.Frame8.Controls.Add(Me.txtESIAmount)
        Me.Frame8.Controls.Add(Me.chkTDS)
        Me.Frame8.Controls.Add(Me.txtTDSRate)
        Me.Frame8.Controls.Add(Me.txtTDSAmount)
        Me.Frame8.Controls.Add(Me.Label42)
        Me.Frame8.Controls.Add(Me.Label46)
        Me.Frame8.Controls.Add(Me.Label43)
        Me.Frame8.Controls.Add(Me.Label40)
        Me.Frame8.Controls.Add(Me.Label41)
        Me.Frame8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame8.Location = New System.Drawing.Point(368, 0)
        Me.Frame8.Name = "Frame8"
        Me.Frame8.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame8.Size = New System.Drawing.Size(269, 107)
        Me.Frame8.TabIndex = 120
        Me.Frame8.TabStop = False
        Me.Frame8.Text = "Deduction"
        '
        'ChkSTDSRO
        '
        Me.ChkSTDSRO.BackColor = System.Drawing.SystemColors.Control
        Me.ChkSTDSRO.Checked = True
        Me.ChkSTDSRO.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChkSTDSRO.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkSTDSRO.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkSTDSRO.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkSTDSRO.Location = New System.Drawing.Point(250, 66)
        Me.ChkSTDSRO.Name = "ChkSTDSRO"
        Me.ChkSTDSRO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkSTDSRO.Size = New System.Drawing.Size(15, 13)
        Me.ChkSTDSRO.TabIndex = 42
        Me.ChkSTDSRO.UseVisualStyleBackColor = False
        '
        'ChkESIRO
        '
        Me.ChkESIRO.BackColor = System.Drawing.SystemColors.Control
        Me.ChkESIRO.Checked = True
        Me.ChkESIRO.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChkESIRO.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkESIRO.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkESIRO.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkESIRO.Location = New System.Drawing.Point(250, 46)
        Me.ChkESIRO.Name = "ChkESIRO"
        Me.ChkESIRO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkESIRO.Size = New System.Drawing.Size(15, 13)
        Me.ChkESIRO.TabIndex = 37
        Me.ChkESIRO.UseVisualStyleBackColor = False
        '
        'ChkTDSRO
        '
        Me.ChkTDSRO.BackColor = System.Drawing.SystemColors.Control
        Me.ChkTDSRO.Checked = True
        Me.ChkTDSRO.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChkTDSRO.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkTDSRO.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkTDSRO.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkTDSRO.Location = New System.Drawing.Point(250, 26)
        Me.ChkTDSRO.Name = "ChkTDSRO"
        Me.ChkTDSRO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkTDSRO.Size = New System.Drawing.Size(15, 13)
        Me.ChkTDSRO.TabIndex = 32
        Me.ChkTDSRO.UseVisualStyleBackColor = False
        '
        'txtSTDSDeductOn
        '
        Me.txtSTDSDeductOn.AcceptsReturn = True
        Me.txtSTDSDeductOn.BackColor = System.Drawing.SystemColors.Window
        Me.txtSTDSDeductOn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSTDSDeductOn.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSTDSDeductOn.Enabled = False
        Me.txtSTDSDeductOn.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSTDSDeductOn.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSTDSDeductOn.Location = New System.Drawing.Point(62, 64)
        Me.txtSTDSDeductOn.MaxLength = 0
        Me.txtSTDSDeductOn.Name = "txtSTDSDeductOn"
        Me.txtSTDSDeductOn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSTDSDeductOn.Size = New System.Drawing.Size(75, 20)
        Me.txtSTDSDeductOn.TabIndex = 39
        Me.txtSTDSDeductOn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtESIDeductOn
        '
        Me.txtESIDeductOn.AcceptsReturn = True
        Me.txtESIDeductOn.BackColor = System.Drawing.SystemColors.Window
        Me.txtESIDeductOn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtESIDeductOn.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtESIDeductOn.Enabled = False
        Me.txtESIDeductOn.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtESIDeductOn.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtESIDeductOn.Location = New System.Drawing.Point(62, 44)
        Me.txtESIDeductOn.MaxLength = 0
        Me.txtESIDeductOn.Name = "txtESIDeductOn"
        Me.txtESIDeductOn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtESIDeductOn.Size = New System.Drawing.Size(75, 20)
        Me.txtESIDeductOn.TabIndex = 34
        Me.txtESIDeductOn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTDSDeductOn
        '
        Me.txtTDSDeductOn.AcceptsReturn = True
        Me.txtTDSDeductOn.BackColor = System.Drawing.SystemColors.Window
        Me.txtTDSDeductOn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTDSDeductOn.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTDSDeductOn.Enabled = False
        Me.txtTDSDeductOn.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTDSDeductOn.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTDSDeductOn.Location = New System.Drawing.Point(62, 24)
        Me.txtTDSDeductOn.MaxLength = 0
        Me.txtTDSDeductOn.Name = "txtTDSDeductOn"
        Me.txtTDSDeductOn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTDSDeductOn.Size = New System.Drawing.Size(75, 20)
        Me.txtTDSDeductOn.TabIndex = 29
        Me.txtTDSDeductOn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtJVVNO
        '
        Me.txtJVVNO.AcceptsReturn = True
        Me.txtJVVNO.BackColor = System.Drawing.SystemColors.Window
        Me.txtJVVNO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtJVVNO.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtJVVNO.Enabled = False
        Me.txtJVVNO.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtJVVNO.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtJVVNO.Location = New System.Drawing.Point(62, 84)
        Me.txtJVVNO.MaxLength = 0
        Me.txtJVVNO.Name = "txtJVVNO"
        Me.txtJVVNO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtJVVNO.Size = New System.Drawing.Size(187, 20)
        Me.txtJVVNO.TabIndex = 43
        '
        'ChkSTDS
        '
        Me.ChkSTDS.AutoSize = True
        Me.ChkSTDS.BackColor = System.Drawing.SystemColors.Control
        Me.ChkSTDS.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkSTDS.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkSTDS.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkSTDS.Location = New System.Drawing.Point(6, 66)
        Me.ChkSTDS.Name = "ChkSTDS"
        Me.ChkSTDS.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkSTDS.Size = New System.Drawing.Size(54, 18)
        Me.ChkSTDS.TabIndex = 38
        Me.ChkSTDS.Text = "STDS"
        Me.ChkSTDS.UseVisualStyleBackColor = False
        '
        'txtSTDSRate
        '
        Me.txtSTDSRate.AcceptsReturn = True
        Me.txtSTDSRate.BackColor = System.Drawing.SystemColors.Window
        Me.txtSTDSRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSTDSRate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSTDSRate.Enabled = False
        Me.txtSTDSRate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSTDSRate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSTDSRate.Location = New System.Drawing.Point(138, 64)
        Me.txtSTDSRate.MaxLength = 0
        Me.txtSTDSRate.Name = "txtSTDSRate"
        Me.txtSTDSRate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSTDSRate.Size = New System.Drawing.Size(47, 20)
        Me.txtSTDSRate.TabIndex = 40
        Me.txtSTDSRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSTDSAmount
        '
        Me.txtSTDSAmount.AcceptsReturn = True
        Me.txtSTDSAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtSTDSAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSTDSAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSTDSAmount.Enabled = False
        Me.txtSTDSAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSTDSAmount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSTDSAmount.Location = New System.Drawing.Point(186, 64)
        Me.txtSTDSAmount.MaxLength = 0
        Me.txtSTDSAmount.Name = "txtSTDSAmount"
        Me.txtSTDSAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSTDSAmount.Size = New System.Drawing.Size(63, 20)
        Me.txtSTDSAmount.TabIndex = 41
        Me.txtSTDSAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'chkESI
        '
        Me.chkESI.AutoSize = True
        Me.chkESI.BackColor = System.Drawing.SystemColors.Control
        Me.chkESI.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkESI.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkESI.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkESI.Location = New System.Drawing.Point(6, 46)
        Me.chkESI.Name = "chkESI"
        Me.chkESI.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkESI.Size = New System.Drawing.Size(42, 18)
        Me.chkESI.TabIndex = 33
        Me.chkESI.Text = "ESI"
        Me.chkESI.UseVisualStyleBackColor = False
        '
        'txtESIRate
        '
        Me.txtESIRate.AcceptsReturn = True
        Me.txtESIRate.BackColor = System.Drawing.SystemColors.Window
        Me.txtESIRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtESIRate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtESIRate.Enabled = False
        Me.txtESIRate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtESIRate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtESIRate.Location = New System.Drawing.Point(138, 44)
        Me.txtESIRate.MaxLength = 0
        Me.txtESIRate.Name = "txtESIRate"
        Me.txtESIRate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtESIRate.Size = New System.Drawing.Size(47, 20)
        Me.txtESIRate.TabIndex = 35
        Me.txtESIRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtESIAmount
        '
        Me.txtESIAmount.AcceptsReturn = True
        Me.txtESIAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtESIAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtESIAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtESIAmount.Enabled = False
        Me.txtESIAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtESIAmount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtESIAmount.Location = New System.Drawing.Point(186, 44)
        Me.txtESIAmount.MaxLength = 0
        Me.txtESIAmount.Name = "txtESIAmount"
        Me.txtESIAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtESIAmount.Size = New System.Drawing.Size(63, 20)
        Me.txtESIAmount.TabIndex = 36
        Me.txtESIAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'chkTDS
        '
        Me.chkTDS.AutoSize = True
        Me.chkTDS.BackColor = System.Drawing.SystemColors.Control
        Me.chkTDS.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkTDS.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTDS.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkTDS.Location = New System.Drawing.Point(6, 26)
        Me.chkTDS.Name = "chkTDS"
        Me.chkTDS.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkTDS.Size = New System.Drawing.Size(47, 18)
        Me.chkTDS.TabIndex = 28
        Me.chkTDS.Text = "TDS"
        Me.chkTDS.UseVisualStyleBackColor = False
        '
        'txtTDSRate
        '
        Me.txtTDSRate.AcceptsReturn = True
        Me.txtTDSRate.BackColor = System.Drawing.SystemColors.Window
        Me.txtTDSRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTDSRate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTDSRate.Enabled = False
        Me.txtTDSRate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTDSRate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTDSRate.Location = New System.Drawing.Point(138, 24)
        Me.txtTDSRate.MaxLength = 0
        Me.txtTDSRate.Name = "txtTDSRate"
        Me.txtTDSRate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTDSRate.Size = New System.Drawing.Size(47, 20)
        Me.txtTDSRate.TabIndex = 30
        Me.txtTDSRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTDSAmount
        '
        Me.txtTDSAmount.AcceptsReturn = True
        Me.txtTDSAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtTDSAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTDSAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTDSAmount.Enabled = False
        Me.txtTDSAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTDSAmount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTDSAmount.Location = New System.Drawing.Point(186, 24)
        Me.txtTDSAmount.MaxLength = 0
        Me.txtTDSAmount.Name = "txtTDSAmount"
        Me.txtTDSAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTDSAmount.Size = New System.Drawing.Size(63, 20)
        Me.txtTDSAmount.TabIndex = 31
        Me.txtTDSAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.BackColor = System.Drawing.SystemColors.Control
        Me.Label42.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label42.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label42.Location = New System.Drawing.Point(248, 10)
        Me.Label42.Name = "Label42"
        Me.Label42.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label42.Size = New System.Drawing.Size(21, 14)
        Me.Label42.TabIndex = 125
        Me.Label42.Text = "Rd"
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.BackColor = System.Drawing.SystemColors.Control
        Me.Label46.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label46.Enabled = False
        Me.Label46.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label46.Location = New System.Drawing.Point(6, 86)
        Me.Label46.Name = "Label46"
        Me.Label46.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label46.Size = New System.Drawing.Size(52, 14)
        Me.Label46.TabIndex = 124
        Me.Label46.Text = "JV VNo :"
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.BackColor = System.Drawing.SystemColors.Control
        Me.Label43.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label43.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label43.Location = New System.Drawing.Point(64, 10)
        Me.Label43.Name = "Label43"
        Me.Label43.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label43.Size = New System.Drawing.Size(63, 14)
        Me.Label43.TabIndex = 123
        Me.Label43.Text = "Deduct On"
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.BackColor = System.Drawing.SystemColors.Control
        Me.Label40.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label40.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label40.Location = New System.Drawing.Point(138, 10)
        Me.Label40.Name = "Label40"
        Me.Label40.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label40.Size = New System.Drawing.Size(48, 14)
        Me.Label40.TabIndex = 122
        Me.Label40.Text = "Rate(%)"
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.BackColor = System.Drawing.SystemColors.Control
        Me.Label41.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label41.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label41.Location = New System.Drawing.Point(194, 10)
        Me.Label41.Name = "Label41"
        Me.Label41.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label41.Size = New System.Drawing.Size(51, 14)
        Me.Label41.TabIndex = 121
        Me.Label41.Text = "Amount"
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
        Me.txtNarration.Location = New System.Drawing.Point(96, 50)
        Me.txtNarration.MaxLength = 0
        Me.txtNarration.Name = "txtNarration"
        Me.txtNarration.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNarration.Size = New System.Drawing.Size(267, 20)
        Me.txtNarration.TabIndex = 46
        '
        'txtDocsThru
        '
        Me.txtDocsThru.AcceptsReturn = True
        Me.txtDocsThru.BackColor = System.Drawing.SystemColors.Window
        Me.txtDocsThru.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDocsThru.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDocsThru.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDocsThru.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDocsThru.Location = New System.Drawing.Point(96, 110)
        Me.txtDocsThru.MaxLength = 0
        Me.txtDocsThru.Name = "txtDocsThru"
        Me.txtDocsThru.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDocsThru.Size = New System.Drawing.Size(267, 20)
        Me.txtDocsThru.TabIndex = 54
        '
        'txtCarriers
        '
        Me.txtCarriers.AcceptsReturn = True
        Me.txtCarriers.BackColor = System.Drawing.SystemColors.Window
        Me.txtCarriers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCarriers.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCarriers.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCarriers.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCarriers.Location = New System.Drawing.Point(96, 70)
        Me.txtCarriers.MaxLength = 0
        Me.txtCarriers.Name = "txtCarriers"
        Me.txtCarriers.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCarriers.Size = New System.Drawing.Size(267, 20)
        Me.txtCarriers.TabIndex = 47
        '
        'txtVehicle
        '
        Me.txtVehicle.AcceptsReturn = True
        Me.txtVehicle.BackColor = System.Drawing.SystemColors.Window
        Me.txtVehicle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVehicle.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVehicle.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVehicle.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtVehicle.Location = New System.Drawing.Point(96, 130)
        Me.txtVehicle.MaxLength = 0
        Me.txtVehicle.Name = "txtVehicle"
        Me.txtVehicle.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVehicle.Size = New System.Drawing.Size(267, 20)
        Me.txtVehicle.TabIndex = 55
        '
        'txtMode
        '
        Me.txtMode.AcceptsReturn = True
        Me.txtMode.BackColor = System.Drawing.SystemColors.Window
        Me.txtMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMode.Location = New System.Drawing.Point(96, 90)
        Me.txtMode.MaxLength = 0
        Me.txtMode.Name = "txtMode"
        Me.txtMode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMode.Size = New System.Drawing.Size(267, 20)
        Me.txtMode.TabIndex = 53
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRemarks.Location = New System.Drawing.Point(96, 30)
        Me.txtRemarks.MaxLength = 0
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.Size = New System.Drawing.Size(267, 20)
        Me.txtRemarks.TabIndex = 45
        '
        'txtItemType
        '
        Me.txtItemType.AcceptsReturn = True
        Me.txtItemType.BackColor = System.Drawing.SystemColors.Window
        Me.txtItemType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemType.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItemType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtItemType.Location = New System.Drawing.Point(96, 10)
        Me.txtItemType.MaxLength = 0
        Me.txtItemType.Name = "txtItemType"
        Me.txtItemType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemType.Size = New System.Drawing.Size(267, 20)
        Me.txtItemType.TabIndex = 44
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.BackColor = System.Drawing.SystemColors.Control
        Me.Label33.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label33.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label33.Location = New System.Drawing.Point(96, 172)
        Me.Label33.Name = "Label33"
        Me.Label33.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label33.Size = New System.Drawing.Size(42, 14)
        Me.Label33.TabIndex = 160
        Me.Label33.Text = "From :"
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.BackColor = System.Drawing.SystemColors.Control
        Me.Label35.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label35.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label35.Location = New System.Drawing.Point(184, 172)
        Me.Label35.Name = "Label35"
        Me.Label35.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label35.Size = New System.Drawing.Size(26, 14)
        Me.Label35.TabIndex = 159
        Me.Label35.Text = "To :"
        '
        'Label37
        '
        Me.Label37.BackColor = System.Drawing.SystemColors.Control
        Me.Label37.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label37.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label37.Location = New System.Drawing.Point(12, 172)
        Me.Label37.Name = "Label37"
        Me.Label37.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label37.Size = New System.Drawing.Size(75, 17)
        Me.Label37.TabIndex = 158
        Me.Label37.Text = "Credit Days :"
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(2, 152)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(88, 14)
        Me.Label7.TabIndex = 153
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
        Me.Label9.Location = New System.Drawing.Point(178, 152)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(41, 14)
        Me.Label9.TabIndex = 152
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
        Me.Label32.Location = New System.Drawing.Point(2, 52)
        Me.Label32.Name = "Label32"
        Me.Label32.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label32.Size = New System.Drawing.Size(63, 14)
        Me.Label32.TabIndex = 95
        Me.Label32.Text = "Narration :"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.BackColor = System.Drawing.SystemColors.Control
        Me.Label31.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label31.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label31.Location = New System.Drawing.Point(20, 112)
        Me.Label31.Name = "Label31"
        Me.Label31.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label31.Size = New System.Drawing.Size(69, 14)
        Me.Label31.TabIndex = 94
        Me.Label31.Text = "Docs Thru :"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.BackColor = System.Drawing.SystemColors.Control
        Me.Label30.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label30.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label30.Location = New System.Drawing.Point(2, 72)
        Me.Label30.Name = "Label30"
        Me.Label30.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label30.Size = New System.Drawing.Size(59, 14)
        Me.Label30.TabIndex = 93
        Me.Label30.Text = "Carriers :"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.SystemColors.Control
        Me.Label29.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label29.Location = New System.Drawing.Point(20, 132)
        Me.Label29.Name = "Label29"
        Me.Label29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label29.Size = New System.Drawing.Size(53, 14)
        Me.Label29.TabIndex = 92
        Me.Label29.Text = "Vehicle :"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.SystemColors.Control
        Me.Label27.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label27.Location = New System.Drawing.Point(51, 92)
        Me.Label27.Name = "Label27"
        Me.Label27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label27.Size = New System.Drawing.Size(44, 14)
        Me.Label27.TabIndex = 91
        Me.Label27.Text = "Mode :"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.SystemColors.Control
        Me.Label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(2, 32)
        Me.Label26.Name = "Label26"
        Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label26.Size = New System.Drawing.Size(63, 14)
        Me.Label26.TabIndex = 90
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
        Me.Label11.Location = New System.Drawing.Point(2, 12)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(67, 14)
        Me.Label11.TabIndex = 89
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
        Me.txtDebitAccount.Location = New System.Drawing.Point(446, 54)
        Me.txtDebitAccount.MaxLength = 0
        Me.txtDebitAccount.Name = "txtDebitAccount"
        Me.txtDebitAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDebitAccount.Size = New System.Drawing.Size(220, 20)
        Me.txtDebitAccount.TabIndex = 9
        '
        'txtSupplier
        '
        Me.txtSupplier.AcceptsReturn = True
        Me.txtSupplier.BackColor = System.Drawing.SystemColors.Window
        Me.txtSupplier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplier.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSupplier.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplier.ForeColor = System.Drawing.Color.Blue
        Me.txtSupplier.Location = New System.Drawing.Point(446, 34)
        Me.txtSupplier.MaxLength = 0
        Me.txtSupplier.Name = "txtSupplier"
        Me.txtSupplier.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSupplier.Size = New System.Drawing.Size(301, 20)
        Me.txtSupplier.TabIndex = 6
        '
        'txtMRRDate
        '
        Me.txtMRRDate.AcceptsReturn = True
        Me.txtMRRDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtMRRDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMRRDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMRRDate.Enabled = False
        Me.txtMRRDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMRRDate.ForeColor = System.Drawing.Color.Blue
        Me.txtMRRDate.Location = New System.Drawing.Point(269, 74)
        Me.txtMRRDate.MaxLength = 0
        Me.txtMRRDate.Name = "txtMRRDate"
        Me.txtMRRDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMRRDate.Size = New System.Drawing.Size(73, 20)
        Me.txtMRRDate.TabIndex = 12
        '
        'txtBillDate
        '
        Me.txtBillDate.AcceptsReturn = True
        Me.txtBillDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillDate.ForeColor = System.Drawing.Color.Blue
        Me.txtBillDate.Location = New System.Drawing.Point(269, 54)
        Me.txtBillDate.MaxLength = 0
        Me.txtBillDate.Name = "txtBillDate"
        Me.txtBillDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillDate.Size = New System.Drawing.Size(73, 20)
        Me.txtBillDate.TabIndex = 8
        '
        'cboInvType
        '
        Me.cboInvType.BackColor = System.Drawing.SystemColors.Window
        Me.cboInvType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboInvType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboInvType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboInvType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboInvType.Location = New System.Drawing.Point(446, 12)
        Me.cboInvType.Name = "cboInvType"
        Me.cboInvType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboInvType.Size = New System.Drawing.Size(303, 22)
        Me.cboInvType.TabIndex = 18
        '
        'txtMRRNo
        '
        Me.txtMRRNo.AcceptsReturn = True
        Me.txtMRRNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtMRRNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMRRNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMRRNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMRRNo.ForeColor = System.Drawing.Color.Blue
        Me.txtMRRNo.Location = New System.Drawing.Point(83, 74)
        Me.txtMRRNo.MaxLength = 0
        Me.txtMRRNo.Name = "txtMRRNo"
        Me.txtMRRNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMRRNo.Size = New System.Drawing.Size(101, 20)
        Me.txtMRRNo.TabIndex = 10
        '
        'txtBillNo
        '
        Me.txtBillNo.AcceptsReturn = True
        Me.txtBillNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtBillNo.Location = New System.Drawing.Point(83, 54)
        Me.txtBillNo.MaxLength = 0
        Me.txtBillNo.Name = "txtBillNo"
        Me.txtBillNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillNo.Size = New System.Drawing.Size(101, 20)
        Me.txtBillNo.TabIndex = 7
        '
        'txtServNo
        '
        Me.txtServNo.AcceptsReturn = True
        Me.txtServNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtServNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtServNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtServNo.Enabled = False
        Me.txtServNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtServNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtServNo.Location = New System.Drawing.Point(106, 14)
        Me.txtServNo.MaxLength = 0
        Me.txtServNo.Name = "txtServNo"
        Me.txtServNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtServNo.Size = New System.Drawing.Size(77, 20)
        Me.txtServNo.TabIndex = 130
        Me.txtServNo.Visible = False
        '
        'txtServDate
        '
        Me.txtServDate.AcceptsReturn = True
        Me.txtServDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtServDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtServDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtServDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtServDate.ForeColor = System.Drawing.Color.Blue
        Me.txtServDate.Location = New System.Drawing.Point(268, 14)
        Me.txtServDate.MaxLength = 0
        Me.txtServDate.Name = "txtServDate"
        Me.txtServDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtServDate.Size = New System.Drawing.Size(73, 20)
        Me.txtServDate.TabIndex = 131
        Me.txtServDate.Visible = False
        '
        'txtPONo
        '
        Me.txtPONo.AcceptsReturn = True
        Me.txtPONo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPONo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPONo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPONo.Enabled = False
        Me.txtPONo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPONo.ForeColor = System.Drawing.Color.Blue
        Me.txtPONo.Location = New System.Drawing.Point(83, 134)
        Me.txtPONo.MaxLength = 0
        Me.txtPONo.Name = "txtPONo"
        Me.txtPONo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPONo.Size = New System.Drawing.Size(101, 20)
        Me.txtPONo.TabIndex = 25
        Me.txtPONo.Visible = False
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
        Me.txtPODate.Location = New System.Drawing.Point(269, 134)
        Me.txtPODate.MaxLength = 0
        Me.txtPODate.Name = "txtPODate"
        Me.txtPODate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPODate.Size = New System.Drawing.Size(73, 20)
        Me.txtPODate.TabIndex = 26
        Me.txtPODate.Visible = False
        '
        'lblGSTClaimNo
        '
        Me.lblGSTClaimNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblGSTClaimNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblGSTClaimNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGSTClaimNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblGSTClaimNo.Location = New System.Drawing.Point(186, 36)
        Me.lblGSTClaimNo.Name = "lblGSTClaimNo"
        Me.lblGSTClaimNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblGSTClaimNo.Size = New System.Drawing.Size(25, 15)
        Me.lblGSTClaimNo.TabIndex = 190
        Me.lblGSTClaimNo.Text = "lblGSTClaimNo"
        '
        'lblGSTClaimDate
        '
        Me.lblGSTClaimDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblGSTClaimDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblGSTClaimDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGSTClaimDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblGSTClaimDate.Location = New System.Drawing.Point(346, 32)
        Me.lblGSTClaimDate.Name = "lblGSTClaimDate"
        Me.lblGSTClaimDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblGSTClaimDate.Size = New System.Drawing.Size(21, 15)
        Me.lblGSTClaimDate.TabIndex = 189
        Me.lblGSTClaimDate.Text = "lblGSTClaimDate"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(6, 120)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(73, 14)
        Me.Label6.TabIndex = 168
        Me.Label6.Text = "GST Status :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(360, 76)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(90, 14)
        Me.Label4.TabIndex = 162
        Me.Label4.Text = "Shipped From :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label59
        '
        Me.Label59.AutoSize = True
        Me.Label59.BackColor = System.Drawing.SystemColors.Control
        Me.Label59.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label59.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label59.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label59.Location = New System.Drawing.Point(20, 96)
        Me.Label59.Name = "Label59"
        Me.Label59.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label59.Size = New System.Drawing.Size(56, 14)
        Me.Label59.TabIndex = 161
        Me.Label59.Text = "Division :"
        Me.Label59.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label56
        '
        Me.Label56.AutoSize = True
        Me.Label56.BackColor = System.Drawing.SystemColors.Control
        Me.Label56.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label56.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label56.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label56.Location = New System.Drawing.Point(209, 38)
        Me.Label56.Name = "Label56"
        Me.Label56.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label56.Size = New System.Drawing.Size(37, 14)
        Me.Label56.TabIndex = 155
        Me.Label56.Text = "Date :"
        Me.Label56.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.BackColor = System.Drawing.SystemColors.Control
        Me.Label38.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label38.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label38.Location = New System.Drawing.Point(26, 32)
        Me.Label38.Name = "Label38"
        Me.Label38.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label38.Size = New System.Drawing.Size(52, 14)
        Me.Label38.TabIndex = 154
        Me.Label38.Text = "GST No :"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.TopRight
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
        Me.lblPurchaseVNo.TabIndex = 126
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
        Me.lblVNo.TabIndex = 117
        Me.lblVNo.Text = "Voucher No :"
        Me.lblVNo.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblVDate
        '
        Me.lblVDate.AutoSize = True
        Me.lblVDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblVDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblVDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblVDate.Location = New System.Drawing.Point(209, 16)
        Me.lblVDate.Name = "lblVDate"
        Me.lblVDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblVDate.Size = New System.Drawing.Size(37, 14)
        Me.lblVDate.TabIndex = 116
        Me.lblVDate.Text = "Date :"
        Me.lblVDate.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(231, 138)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(37, 14)
        Me.Label12.TabIndex = 105
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
        Me.Label3.Location = New System.Drawing.Point(379, 56)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(61, 14)
        Me.Label3.TabIndex = 86
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
        Me.lblCust.Location = New System.Drawing.Point(388, 36)
        Me.lblCust.Name = "lblCust"
        Me.lblCust.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCust.Size = New System.Drawing.Size(59, 14)
        Me.lblCust.TabIndex = 85
        Me.lblCust.Text = "Supplier :"
        Me.lblCust.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(209, 76)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(37, 14)
        Me.Label15.TabIndex = 84
        Me.Label15.Text = "Date :"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(209, 56)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(37, 14)
        Me.Label5.TabIndex = 83
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
        Me.lblInvType.Location = New System.Drawing.Point(360, 14)
        Me.lblInvType.Name = "lblInvType"
        Me.lblInvType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblInvType.Size = New System.Drawing.Size(81, 14)
        Me.lblInvType.TabIndex = 75
        Me.lblInvType.Text = "Invoice Type :"
        Me.lblInvType.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(24, 76)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(54, 14)
        Me.Label14.TabIndex = 82
        Me.Label14.Text = "MRR No :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(5, 56)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(77, 13)
        Me.Label1.TabIndex = 81
        Me.Label1.Text = "Bill No :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(749, 443)
        Me.SprdView.TabIndex = 77
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.cmdClose)
        Me.Frame3.Controls.Add(Me.CmdView)
        Me.Frame3.Controls.Add(Me.CmdPreview)
        Me.Frame3.Controls.Add(Me.cmdPrint)
        Me.Frame3.Controls.Add(Me.cmdSavePrint)
        Me.Frame3.Controls.Add(Me.cmdPostingHead)
        Me.Frame3.Controls.Add(Me.cmdBarCode)
        Me.Frame3.Controls.Add(Me.cmdDelete)
        Me.Frame3.Controls.Add(Me.cmdSave)
        Me.Frame3.Controls.Add(Me.cmdModify)
        Me.Frame3.Controls.Add(Me.cmdAdd)
        Me.Frame3.Controls.Add(Me.Report1)
        Me.Frame3.Controls.Add(Me.lblPMKey)
        Me.Frame3.Controls.Add(Me.lblSODates)
        Me.Frame3.Controls.Add(Me.lblSONos)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 440)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(751, 51)
        Me.Frame3.TabIndex = 76
        Me.Frame3.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(14, 12)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 134
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
        Me.lblPMKey.TabIndex = 119
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
        Me.lblSODates.TabIndex = 79
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
        Me.lblSONos.TabIndex = 78
        Me.lblSONos.Text = "lblSONos"
        Me.lblSONos.Visible = False
        '
        'OptFreight
        '
        '
        'txtCreditDays
        '
        '
        'FrmPurchaseShipGST
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(752, 492)
        Me.Controls.Add(Me.FraPostingDtl)
        Me.Controls.Add(Me.FraFront)
        Me.Controls.Add(Me.SprdView)
        Me.Controls.Add(Me.Frame3)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(3, 23)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmPurchaseShipGST"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Purchase (Ship GST)"
        Me.FraPostingDtl.ResumeLayout(False)
        CType(Me.SprdPostingDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraFront.ResumeLayout(False)
        Me.FraFront.PerformLayout()
        Me.SSTab1.ResumeLayout(False)
        Me._SSTab1_TabPage0.ResumeLayout(False)
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me.SSTabLevies.ResumeLayout(False)
        Me._SSTabLevies_TabPage0.ResumeLayout(False)
        Me._SSTabLevies_TabPage0.PerformLayout()
        Me._SSTabLevies_TabPage1.ResumeLayout(False)
        Me._SSTabLevies_TabPage1.PerformLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdExp, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage1.ResumeLayout(False)
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.Frame7.ResumeLayout(False)
        Me.FraServiceTax.ResumeLayout(False)
        Me.FraServiceTax.PerformLayout()
        Me.Frame8.ResumeLayout(False)
        Me.Frame8.PerformLayout()
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

    Public WithEvents TxtShipTo As TextBox
    Public WithEvents txtBillTo As TextBox
#End Region
End Class