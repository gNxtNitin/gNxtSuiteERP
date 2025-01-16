Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmInvoiceGST
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
    Public WithEvents chkRejection As System.Windows.Forms.CheckBox
    Public WithEvents chkLUT As System.Windows.Forms.CheckBox
    Public WithEvents cboDivision As System.Windows.Forms.ComboBox
    Public WithEvents txtPONo As System.Windows.Forms.TextBox
    Public WithEvents txtPODate As System.Windows.Forms.TextBox
    Public WithEvents chkFOC As System.Windows.Forms.CheckBox
    Public WithEvents chkCancelled As System.Windows.Forms.CheckBox
    Public WithEvents txtCustMatValue As System.Windows.Forms.TextBox
    Public WithEvents MSComm1 As Object
    Public WithEvents SprdExp As AxFPSpreadADO.AxfpSpread
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents lblTotTaxableAmt As System.Windows.Forms.Label
    Public WithEvents _Label_9 As System.Windows.Forms.Label
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents lblTotCD As System.Windows.Forms.Label
    Public WithEvents lblCDLabel As System.Windows.Forms.Label
    Public WithEvents lblEDUOnCDAmount As System.Windows.Forms.Label
    Public WithEvents lblCessCDLabel As System.Windows.Forms.Label
    Public WithEvents _Label_23 As System.Windows.Forms.Label
    Public WithEvents lblMRPValue As System.Windows.Forms.Label
    Public WithEvents _Label_22 As System.Windows.Forms.Label
    Public WithEvents lblTotItemValue As System.Windows.Forms.Label
    Public WithEvents lblTotQty As System.Windows.Forms.Label
    Public WithEvents lblTotPackQtyCap As System.Windows.Forms.Label
    Public WithEvents _Label_29 As System.Windows.Forms.Label
    Public WithEvents _Label_25 As System.Windows.Forms.Label
    Public WithEvents lblTotSGSTAmount As System.Windows.Forms.Label
    Public WithEvents _Label_26 As System.Windows.Forms.Label
    Public WithEvents lblTotIGSTAmount As System.Windows.Forms.Label
    Public WithEvents lblServicePercentage As System.Windows.Forms.Label
    Public WithEvents lblTCS As System.Windows.Forms.Label
    Public WithEvents lblTCSPercentage As System.Windows.Forms.Label
    Public WithEvents lblMSC As System.Windows.Forms.Label
    Public WithEvents lblRO As System.Windows.Forms.Label
    Public WithEvents lblTotExpAmt As System.Windows.Forms.Label
    Public WithEvents LblBookCode As System.Windows.Forms.Label
    Public WithEvents LblMKey As System.Windows.Forms.Label
    Public WithEvents _Label_24 As System.Windows.Forms.Label
    Public WithEvents lblTotCGSTAmount As System.Windows.Forms.Label
    Public WithEvents lblNetAmount As System.Windows.Forms.Label
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents SprdPostingDetail As AxFPSpreadADO.AxfpSpread
    Public WithEvents FraPostingDtl As System.Windows.Forms.GroupBox
    Public WithEvents _TabMain_TabPage0 As System.Windows.Forms.TabPage
    Public WithEvents cmdSearchDespatchFrom As System.Windows.Forms.Button
    Public WithEvents txtShippedFrom As System.Windows.Forms.TextBox
    Public WithEvents chkDespatchFrom As System.Windows.Forms.CheckBox
    Public WithEvents chkExWork As System.Windows.Forms.CheckBox
    Public WithEvents txtModvatDate As System.Windows.Forms.TextBox
    Public WithEvents txteRefNo As System.Windows.Forms.TextBox
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Frame10 As System.Windows.Forms.GroupBox
    Public WithEvents txtAbatementPer As System.Windows.Forms.TextBox
    Public WithEvents chkTaxOnMRP As System.Windows.Forms.CheckBox
    Public WithEvents txtShippedTo As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchShippedTo As System.Windows.Forms.Button
    Public WithEvents chkShipTo As System.Windows.Forms.CheckBox
    Public WithEvents txtServProvided As System.Windows.Forms.TextBox
    Public WithEvents txtProcessNature As System.Windows.Forms.TextBox
    Public WithEvents Label53 As System.Windows.Forms.Label
    Public WithEvents Label55 As System.Windows.Forms.Label
    Public WithEvents Frame5 As System.Windows.Forms.GroupBox
    Public WithEvents chkAgtPermission As System.Windows.Forms.CheckBox
    Public WithEvents txtPOAmendNo As System.Windows.Forms.TextBox
    Public WithEvents txtPOWEFDate As System.Windows.Forms.TextBox
    Public WithEvents txtSuppFromDate As System.Windows.Forms.TextBox
    Public WithEvents txtSuppToDate As System.Windows.Forms.TextBox
    Public WithEvents txtIntRate As System.Windows.Forms.TextBox
    Public WithEvents Label41 As System.Windows.Forms.Label
    Public WithEvents Label39 As System.Windows.Forms.Label
    Public WithEvents Label43 As System.Windows.Forms.Label
    Public WithEvents Label44 As System.Windows.Forms.Label
    Public WithEvents Label45 As System.Windows.Forms.Label
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents txtDNDate As System.Windows.Forms.TextBox
    Public WithEvents txtDNNo As System.Windows.Forms.TextBox
    Public WithEvents Label37 As System.Windows.Forms.Label
    Public WithEvents lblDNAmount As System.Windows.Forms.Label
    Public WithEvents Label40 As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents txtModvatNo As System.Windows.Forms.TextBox
    Public WithEvents _OptFreight_0 As System.Windows.Forms.RadioButton
    Public WithEvents _OptFreight_1 As System.Windows.Forms.RadioButton
    Public WithEvents Frame8 As System.Windows.Forms.GroupBox
    Public WithEvents _txtCreditDays_0 As System.Windows.Forms.TextBox
    Public WithEvents _txtCreditDays_1 As System.Windows.Forms.TextBox
    Public WithEvents Label33 As System.Windows.Forms.Label
    Public WithEvents Label35 As System.Windows.Forms.Label
    Public WithEvents Frame7 As System.Windows.Forms.GroupBox
    Public WithEvents txtTariff As System.Windows.Forms.TextBox
    Public WithEvents chkChallanMade As System.Windows.Forms.CheckBox
    Public WithEvents chkPackmat As System.Windows.Forms.CheckBox
    Public WithEvents txtNarration As System.Windows.Forms.TextBox
    Public WithEvents txtDocsThru As System.Windows.Forms.TextBox
    Public WithEvents txtRemarks As System.Windows.Forms.TextBox
    Public WithEvents txtItemType As System.Windows.Forms.TextBox
    Public WithEvents Label50 As System.Windows.Forms.Label
    Public WithEvents Label60 As System.Windows.Forms.Label
    Public WithEvents Label56 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label61 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label32 As System.Windows.Forms.Label
    Public WithEvents Label31 As System.Windows.Forms.Label
    Public WithEvents Label26 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents _TabMain_TabPage1 As System.Windows.Forms.TabPage
    Public WithEvents txtPortCode As System.Windows.Forms.TextBox
    Public WithEvents chkPrintByGroup As System.Windows.Forms.CheckBox
    Public WithEvents txtTextDesc As System.Windows.Forms.TextBox
    Public WithEvents chkPrintTextDesc As System.Windows.Forms.CheckBox
    Public WithEvents chkDutyFreePurchase As System.Windows.Forms.CheckBox
    Public WithEvents chkJWDetail As System.Windows.Forms.CheckBox
    Public WithEvents cmdCoBuyerSearch As System.Windows.Forms.Button
    Public WithEvents txtCoBuyerName As System.Windows.Forms.TextBox
    Public WithEvents ChkPaintPrint As System.Windows.Forms.CheckBox
    Public WithEvents chkPrintType As System.Windows.Forms.CheckBox
    Public WithEvents chkStockTrf As System.Windows.Forms.CheckBox
    Public WithEvents txtBuyerName As System.Windows.Forms.TextBox
    Public WithEvents cmdBuyerSearch As System.Windows.Forms.Button
    Public WithEvents txtLocation As System.Windows.Forms.TextBox
    Public WithEvents txtAdvLicense As System.Windows.Forms.TextBox
    Public WithEvents txtTotalEuro As System.Windows.Forms.TextBox
    Public WithEvents txtExchangeRate As System.Windows.Forms.TextBox
    Public WithEvents txtExportBillNo As System.Windows.Forms.TextBox
    Public WithEvents txtExportBillDate As System.Windows.Forms.TextBox
    Public WithEvents txtARE1No As System.Windows.Forms.TextBox
    Public WithEvents txtShippingNo As System.Windows.Forms.TextBox
    Public WithEvents txtARE1Date As System.Windows.Forms.TextBox
    Public WithEvents txtShippingDate As System.Windows.Forms.TextBox
    Public WithEvents _Label_10 As System.Windows.Forms.Label
    Public WithEvents _Label_33 As System.Windows.Forms.Label
    Public WithEvents _Label_8 As System.Windows.Forms.Label
    Public WithEvents _Label_7 As System.Windows.Forms.Label
    Public WithEvents _Label_6 As System.Windows.Forms.Label
    Public WithEvents _Label_5 As System.Windows.Forms.Label
    Public WithEvents _Label_4 As System.Windows.Forms.Label
    Public WithEvents _Label_3 As System.Windows.Forms.Label
    Public WithEvents lblTotExportExp As System.Windows.Forms.Label
    Public WithEvents _Label_2 As System.Windows.Forms.Label
    Public WithEvents Label48 As System.Windows.Forms.Label
    Public WithEvents Label47 As System.Windows.Forms.Label
    Public WithEvents _Label_1 As System.Windows.Forms.Label
    Public WithEvents _Label_0 As System.Windows.Forms.Label
    Public WithEvents Label59 As System.Windows.Forms.Label
    Public WithEvents Frame9 As System.Windows.Forms.GroupBox
    Public WithEvents _TabMain_TabPage2 As System.Windows.Forms.TabPage
    Public WithEvents txtAdvIGSTBal As System.Windows.Forms.TextBox
    Public WithEvents txtAdvSGSTBal As System.Windows.Forms.TextBox
    Public WithEvents txtAdvCGSTBal As System.Windows.Forms.TextBox
    Public WithEvents txtAdvBal As System.Windows.Forms.TextBox
    Public WithEvents txtItemAdvAdjust As System.Windows.Forms.TextBox
    Public WithEvents txtAdvCGST As System.Windows.Forms.TextBox
    Public WithEvents txtAdvSGST As System.Windows.Forms.TextBox
    Public WithEvents txtAdvIGST As System.Windows.Forms.TextBox
    Public WithEvents txtAdvDate As System.Windows.Forms.TextBox
    Public WithEvents txtAdvVNo As System.Windows.Forms.TextBox
    Public WithEvents txtAdvAdjust As System.Windows.Forms.TextBox
    Public WithEvents Label34 As System.Windows.Forms.Label
    Public WithEvents Label28 As System.Windows.Forms.Label
    Public WithEvents Label25 As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label21 As System.Windows.Forms.Label
    Public WithEvents Label22 As System.Windows.Forms.Label
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents Label23 As System.Windows.Forms.Label
    Public WithEvents Label24 As System.Windows.Forms.Label
    Public WithEvents Frame11 As System.Windows.Forms.GroupBox
    Public WithEvents _TabMain_TabPage3 As System.Windows.Forms.TabPage
    Public WithEvents txtDistance As System.Windows.Forms.TextBox
    Public WithEvents txtTransportCode As System.Windows.Forms.TextBox
    Public WithEvents cboTransmode As System.Windows.Forms.ComboBox
    Public WithEvents cboVehicleType As System.Windows.Forms.ComboBox
    Public WithEvents txtCarriers As System.Windows.Forms.TextBox
    Public WithEvents txtVehicle As System.Windows.Forms.TextBox
    Public WithEvents txtMode As System.Windows.Forms.TextBox
    Public WithEvents Label49 As System.Windows.Forms.Label
    Public WithEvents Label46 As System.Windows.Forms.Label
    Public WithEvents Label42 As System.Windows.Forms.Label
    Public WithEvents Label38 As System.Windows.Forms.Label
    Public WithEvents Label30 As System.Windows.Forms.Label
    Public WithEvents Label29 As System.Windows.Forms.Label
    Public WithEvents Label27 As System.Windows.Forms.Label
    Public WithEvents Frame12 As System.Windows.Forms.GroupBox
    Public WithEvents txtResponseId As System.Windows.Forms.TextBox
    Public WithEvents txtEWayBillNo As System.Windows.Forms.TextBox
    Public WithEvents Label57 As System.Windows.Forms.Label
    Public WithEvents Label52 As System.Windows.Forms.Label
    Public WithEvents Frame13 As System.Windows.Forms.GroupBox
    Public WithEvents cmpPrinteInvoice As System.Windows.Forms.Button
    Public WithEvents cmdQRCode As System.Windows.Forms.Button
    Public WithEvents cmdeInvoice As System.Windows.Forms.Button
    Public WithEvents txteInvAckDate As System.Windows.Forms.TextBox
    Public WithEvents txteInvAckNo As System.Windows.Forms.TextBox
    Public WithEvents txtIRNNo As System.Windows.Forms.TextBox
    Public WithEvents Label58 As System.Windows.Forms.Label
    Public WithEvents Label54 As System.Windows.Forms.Label
    Public WithEvents Label51 As System.Windows.Forms.Label
    Public WithEvents Frame14 As System.Windows.Forms.GroupBox
    Public WithEvents _TabMain_TabPage4 As System.Windows.Forms.TabPage
    Public WithEvents TabMain As System.Windows.Forms.TabControl
    Public WithEvents TxtGRDate As System.Windows.Forms.TextBox
    Public WithEvents TxtGRNo As System.Windows.Forms.TextBox
    Public WithEvents txtCreditAccount As System.Windows.Forms.TextBox
    Public WithEvents txtCustomer As System.Windows.Forms.TextBox
    Public WithEvents txtRemovalDate As System.Windows.Forms.TextBox
    Public WithEvents txtRemovalTime As System.Windows.Forms.TextBox
    Public WithEvents txtDCDate As System.Windows.Forms.TextBox
    Public WithEvents txtBillDate As System.Windows.Forms.TextBox
    Public WithEvents TxtBillTm As System.Windows.Forms.TextBox
    Public WithEvents cboInvType As System.Windows.Forms.ComboBox
    Public WithEvents txtDCNo As System.Windows.Forms.TextBox
    Public WithEvents CmdSearchDC As System.Windows.Forms.Button
    Public WithEvents txtBillNoPrefix As System.Windows.Forms.TextBox
    Public WithEvents txtBillNoSuffix As System.Windows.Forms.TextBox
    Public WithEvents txtBillNo As System.Windows.Forms.TextBox
    Public WithEvents TxtDCNoPrefix As System.Windows.Forms.TextBox
    Public WithEvents txtDCNoSuffix As System.Windows.Forms.TextBox
    Public WithEvents lblInvoiceSeq As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents lblSoDate As System.Windows.Forms.Label
    Public WithEvents lblDespRef As System.Windows.Forms.Label
    Public WithEvents lblPoNo As System.Windows.Forms.Label
    Public WithEvents lblInvHeading As System.Windows.Forms.Label
    Public WithEvents Label36 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label20 As System.Windows.Forms.Label
    Public WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents lblCust As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents FraFront As System.Windows.Forms.GroupBox
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdPostingHead As System.Windows.Forms.Button
    Public WithEvents cmdBarCode As System.Windows.Forms.Button
    Public WithEvents cmdDelete As System.Windows.Forms.Button
    Public WithEvents cmdAuthorised As System.Windows.Forms.Button
    Public WithEvents cmdSave As System.Windows.Forms.Button
    Public WithEvents cmdModify As System.Windows.Forms.Button
    Public WithEvents cmdAdd As System.Windows.Forms.Button
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents lblSODates As System.Windows.Forms.Label
    Public WithEvents lblSONos As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents Label As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Public WithEvents OptFreight As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents txtCreditDays As Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmInvoiceGST))
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
        Me.Label17 = New System.Windows.Forms.Label()
        Me.cmdSearchDespatchFrom = New System.Windows.Forms.Button()
        Me.cmdSearchShippedTo = New System.Windows.Forms.Button()
        Me.txtPOAmendNo = New System.Windows.Forms.TextBox()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.txtDNNo = New System.Windows.Forms.TextBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.cmdCoBuyerSearch = New System.Windows.Forms.Button()
        Me.cmdBuyerSearch = New System.Windows.Forms.Button()
        Me.cmpPrinteInvoice = New System.Windows.Forms.Button()
        Me.cmdQRCode = New System.Windows.Forms.Button()
        Me.cmdeInvoice = New System.Windows.Forms.Button()
        Me.CmdSearchDC = New System.Windows.Forms.Button()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdPostingHead = New System.Windows.Forms.Button()
        Me.cmdBarCode = New System.Windows.Forms.Button()
        Me.cmdDelete = New System.Windows.Forms.Button()
        Me.cmdAuthorised = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdModify = New System.Windows.Forms.Button()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.txtDistanceUpdate = New System.Windows.Forms.Button()
        Me.cmdeWayBill = New System.Windows.Forms.Button()
        Me.cmdResetPO = New System.Windows.Forms.Button()
        Me.Label70 = New System.Windows.Forms.Label()
        Me.CmdPopFromFile = New System.Windows.Forms.Button()
        Me.FraFront = New System.Windows.Forms.GroupBox()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.txtVendorCode = New System.Windows.Forms.TextBox()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.txtBillTo = New System.Windows.Forms.TextBox()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.chkRejection = New System.Windows.Forms.CheckBox()
        Me.chkLUT = New System.Windows.Forms.CheckBox()
        Me.cboDivision = New System.Windows.Forms.ComboBox()
        Me.txtPONo = New System.Windows.Forms.TextBox()
        Me.txtPODate = New System.Windows.Forms.TextBox()
        Me.chkFOC = New System.Windows.Forms.CheckBox()
        Me.chkCancelled = New System.Windows.Forms.CheckBox()
        Me.TabMain = New System.Windows.Forms.TabControl()
        Me._TabMain_TabPage0 = New System.Windows.Forms.TabPage()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.lblCompanyCode = New System.Windows.Forms.Label()
        Me.txtTDSOnSale = New System.Windows.Forms.TextBox()
        Me.FraPostingDtl = New System.Windows.Forms.GroupBox()
        Me.SprdPostingDetail = New AxFPSpreadADO.AxfpSpread()
        Me.txtCustMatValue = New System.Windows.Forms.TextBox()
        Me.SprdExp = New AxFPSpreadADO.AxfpSpread()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.lblTotTaxableAmt = New System.Windows.Forms.Label()
        Me._Label_9 = New System.Windows.Forms.Label()
        Me.lblTotCD = New System.Windows.Forms.Label()
        Me.lblCDLabel = New System.Windows.Forms.Label()
        Me.lblEDUOnCDAmount = New System.Windows.Forms.Label()
        Me.lblCessCDLabel = New System.Windows.Forms.Label()
        Me._Label_23 = New System.Windows.Forms.Label()
        Me.lblMRPValue = New System.Windows.Forms.Label()
        Me._Label_22 = New System.Windows.Forms.Label()
        Me.lblTotItemValue = New System.Windows.Forms.Label()
        Me.lblTotQty = New System.Windows.Forms.Label()
        Me.lblTotPackQtyCap = New System.Windows.Forms.Label()
        Me._Label_29 = New System.Windows.Forms.Label()
        Me._Label_25 = New System.Windows.Forms.Label()
        Me.lblTotSGSTAmount = New System.Windows.Forms.Label()
        Me._Label_26 = New System.Windows.Forms.Label()
        Me.lblTotIGSTAmount = New System.Windows.Forms.Label()
        Me.lblServicePercentage = New System.Windows.Forms.Label()
        Me.lblTCS = New System.Windows.Forms.Label()
        Me.lblTCSPercentage = New System.Windows.Forms.Label()
        Me.lblMSC = New System.Windows.Forms.Label()
        Me.lblRO = New System.Windows.Forms.Label()
        Me.lblTotExpAmt = New System.Windows.Forms.Label()
        Me.LblBookCode = New System.Windows.Forms.Label()
        Me.LblMKey = New System.Windows.Forms.Label()
        Me._Label_24 = New System.Windows.Forms.Label()
        Me.lblTotCGSTAmount = New System.Windows.Forms.Label()
        Me.lblNetAmount = New System.Windows.Forms.Label()
        Me._TabMain_TabPage1 = New System.Windows.Forms.TabPage()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.txtStoreDetail = New System.Windows.Forms.TextBox()
        Me.txtApplicant = New System.Windows.Forms.TextBox()
        Me.Label71 = New System.Windows.Forms.Label()
        Me.Label72 = New System.Windows.Forms.Label()
        Me.lblModDate = New System.Windows.Forms.Label()
        Me.Label66 = New System.Windows.Forms.Label()
        Me.lblAddDate = New System.Windows.Forms.Label()
        Me.Label67 = New System.Windows.Forms.Label()
        Me.lblModUser = New System.Windows.Forms.Label()
        Me.Label68 = New System.Windows.Forms.Label()
        Me.lblAddUser = New System.Windows.Forms.Label()
        Me.Label69 = New System.Windows.Forms.Label()
        Me.txtPacking = New System.Windows.Forms.TextBox()
        Me.Label65 = New System.Windows.Forms.Label()
        Me.TxtShipTo = New System.Windows.Forms.TextBox()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.txtShippedFrom = New System.Windows.Forms.TextBox()
        Me.chkDespatchFrom = New System.Windows.Forms.CheckBox()
        Me.chkExWork = New System.Windows.Forms.CheckBox()
        Me.txtModvatDate = New System.Windows.Forms.TextBox()
        Me.Frame10 = New System.Windows.Forms.GroupBox()
        Me.txteRefNo = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtAbatementPer = New System.Windows.Forms.TextBox()
        Me.chkTaxOnMRP = New System.Windows.Forms.CheckBox()
        Me.txtShippedTo = New System.Windows.Forms.TextBox()
        Me.chkShipTo = New System.Windows.Forms.CheckBox()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me.txtServProvided = New System.Windows.Forms.TextBox()
        Me.txtProcessNature = New System.Windows.Forms.TextBox()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.chkAgtPermission = New System.Windows.Forms.CheckBox()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.txtPOWEFDate = New System.Windows.Forms.TextBox()
        Me.txtSuppFromDate = New System.Windows.Forms.TextBox()
        Me.txtSuppToDate = New System.Windows.Forms.TextBox()
        Me.txtIntRate = New System.Windows.Forms.TextBox()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.txtDNDate = New System.Windows.Forms.TextBox()
        Me.lblDNAmount = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.txtModvatNo = New System.Windows.Forms.TextBox()
        Me.Frame8 = New System.Windows.Forms.GroupBox()
        Me._OptFreight_0 = New System.Windows.Forms.RadioButton()
        Me._OptFreight_1 = New System.Windows.Forms.RadioButton()
        Me.Frame7 = New System.Windows.Forms.GroupBox()
        Me._txtCreditDays_0 = New System.Windows.Forms.TextBox()
        Me._txtCreditDays_1 = New System.Windows.Forms.TextBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.txtTariff = New System.Windows.Forms.TextBox()
        Me.chkChallanMade = New System.Windows.Forms.CheckBox()
        Me.chkPackmat = New System.Windows.Forms.CheckBox()
        Me.txtNarration = New System.Windows.Forms.TextBox()
        Me.txtDocsThru = New System.Windows.Forms.TextBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtItemType = New System.Windows.Forms.TextBox()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me._TabMain_TabPage2 = New System.Windows.Forms.TabPage()
        Me.Frame9 = New System.Windows.Forms.GroupBox()
        Me.txtPortCode = New System.Windows.Forms.TextBox()
        Me.chkPrintByGroup = New System.Windows.Forms.CheckBox()
        Me.txtTextDesc = New System.Windows.Forms.TextBox()
        Me.chkPrintTextDesc = New System.Windows.Forms.CheckBox()
        Me.chkDutyFreePurchase = New System.Windows.Forms.CheckBox()
        Me.chkJWDetail = New System.Windows.Forms.CheckBox()
        Me.txtCoBuyerName = New System.Windows.Forms.TextBox()
        Me.ChkPaintPrint = New System.Windows.Forms.CheckBox()
        Me.chkPrintType = New System.Windows.Forms.CheckBox()
        Me.chkStockTrf = New System.Windows.Forms.CheckBox()
        Me.txtBuyerName = New System.Windows.Forms.TextBox()
        Me.txtLocation = New System.Windows.Forms.TextBox()
        Me.txtAdvLicense = New System.Windows.Forms.TextBox()
        Me.txtTotalEuro = New System.Windows.Forms.TextBox()
        Me.txtExchangeRate = New System.Windows.Forms.TextBox()
        Me.txtExportBillNo = New System.Windows.Forms.TextBox()
        Me.txtExportBillDate = New System.Windows.Forms.TextBox()
        Me.txtARE1No = New System.Windows.Forms.TextBox()
        Me.txtShippingNo = New System.Windows.Forms.TextBox()
        Me.txtARE1Date = New System.Windows.Forms.TextBox()
        Me.txtShippingDate = New System.Windows.Forms.TextBox()
        Me._Label_10 = New System.Windows.Forms.Label()
        Me._Label_33 = New System.Windows.Forms.Label()
        Me._Label_8 = New System.Windows.Forms.Label()
        Me._Label_7 = New System.Windows.Forms.Label()
        Me._Label_6 = New System.Windows.Forms.Label()
        Me._Label_5 = New System.Windows.Forms.Label()
        Me._Label_4 = New System.Windows.Forms.Label()
        Me._Label_3 = New System.Windows.Forms.Label()
        Me.lblTotExportExp = New System.Windows.Forms.Label()
        Me._Label_2 = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.Label47 = New System.Windows.Forms.Label()
        Me._Label_1 = New System.Windows.Forms.Label()
        Me._Label_0 = New System.Windows.Forms.Label()
        Me.Label59 = New System.Windows.Forms.Label()
        Me._TabMain_TabPage3 = New System.Windows.Forms.TabPage()
        Me.Frame11 = New System.Windows.Forms.GroupBox()
        Me.txtAdvIGSTBal = New System.Windows.Forms.TextBox()
        Me.txtAdvSGSTBal = New System.Windows.Forms.TextBox()
        Me.txtAdvCGSTBal = New System.Windows.Forms.TextBox()
        Me.txtAdvBal = New System.Windows.Forms.TextBox()
        Me.txtItemAdvAdjust = New System.Windows.Forms.TextBox()
        Me.txtAdvCGST = New System.Windows.Forms.TextBox()
        Me.txtAdvSGST = New System.Windows.Forms.TextBox()
        Me.txtAdvIGST = New System.Windows.Forms.TextBox()
        Me.txtAdvDate = New System.Windows.Forms.TextBox()
        Me.txtAdvVNo = New System.Windows.Forms.TextBox()
        Me.txtAdvAdjust = New System.Windows.Forms.TextBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me._TabMain_TabPage4 = New System.Windows.Forms.TabPage()
        Me.Frame12 = New System.Windows.Forms.GroupBox()
        Me.txtDistance = New System.Windows.Forms.TextBox()
        Me.txtTransportCode = New System.Windows.Forms.TextBox()
        Me.cboTransmode = New System.Windows.Forms.ComboBox()
        Me.cboVehicleType = New System.Windows.Forms.ComboBox()
        Me.txtCarriers = New System.Windows.Forms.TextBox()
        Me.txtVehicle = New System.Windows.Forms.TextBox()
        Me.txtMode = New System.Windows.Forms.TextBox()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Frame13 = New System.Windows.Forms.GroupBox()
        Me.txtResponseId = New System.Windows.Forms.TextBox()
        Me.txtEWayBillNo = New System.Windows.Forms.TextBox()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.Frame14 = New System.Windows.Forms.GroupBox()
        Me.txteInvAckDate = New System.Windows.Forms.TextBox()
        Me.txteInvAckNo = New System.Windows.Forms.TextBox()
        Me.txtIRNNo = New System.Windows.Forms.TextBox()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.TxtGRDate = New System.Windows.Forms.TextBox()
        Me.TxtGRNo = New System.Windows.Forms.TextBox()
        Me.txtCreditAccount = New System.Windows.Forms.TextBox()
        Me.txtCustomer = New System.Windows.Forms.TextBox()
        Me.txtRemovalDate = New System.Windows.Forms.TextBox()
        Me.txtRemovalTime = New System.Windows.Forms.TextBox()
        Me.txtDCDate = New System.Windows.Forms.TextBox()
        Me.txtBillDate = New System.Windows.Forms.TextBox()
        Me.TxtBillTm = New System.Windows.Forms.TextBox()
        Me.cboInvType = New System.Windows.Forms.ComboBox()
        Me.txtDCNo = New System.Windows.Forms.TextBox()
        Me.txtBillNoPrefix = New System.Windows.Forms.TextBox()
        Me.txtBillNoSuffix = New System.Windows.Forms.TextBox()
        Me.txtBillNo = New System.Windows.Forms.TextBox()
        Me.TxtDCNoPrefix = New System.Windows.Forms.TextBox()
        Me.txtDCNoSuffix = New System.Windows.Forms.TextBox()
        Me.lblInvoiceSeq = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblSoDate = New System.Windows.Forms.Label()
        Me.lblDespRef = New System.Windows.Forms.Label()
        Me.lblPoNo = New System.Windows.Forms.Label()
        Me.lblInvHeading = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblCust = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.lblSODates = New System.Windows.Forms.Label()
        Me.lblSONos = New System.Windows.Forms.Label()
        Me.Label = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.OptFreight = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.txtCreditDays = New Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray(Me.components)
        Me.UltraGrid1 = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.CommonDialogOpen = New System.Windows.Forms.OpenFileDialog()
        Me.chkByHand = New System.Windows.Forms.CheckBox()
        Me.FraFront.SuspendLayout()
        Me.TabMain.SuspendLayout()
        Me._TabMain_TabPage0.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me.FraPostingDtl.SuspendLayout()
        CType(Me.SprdPostingDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdExp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._TabMain_TabPage1.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.Frame10.SuspendLayout()
        Me.Frame5.SuspendLayout()
        Me.Frame4.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame8.SuspendLayout()
        Me.Frame7.SuspendLayout()
        Me._TabMain_TabPage2.SuspendLayout()
        Me.Frame9.SuspendLayout()
        Me._TabMain_TabPage3.SuspendLayout()
        Me.Frame11.SuspendLayout()
        Me._TabMain_TabPage4.SuspendLayout()
        Me.Frame12.SuspendLayout()
        Me.Frame13.SuspendLayout()
        Me.Frame14.SuspendLayout()
        Me.Frame3.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptFreight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCreditDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(435, 291)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(94, 14)
        Me.Label17.TabIndex = 155
        Me.Label17.Text = "Cust. Mat. Value  :"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.ToolTip1.SetToolTip(Me.Label17, "AWB/RRP No.")
        '
        'cmdSearchDespatchFrom
        '
        Me.cmdSearchDespatchFrom.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchDespatchFrom.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchDespatchFrom.Enabled = False
        Me.cmdSearchDespatchFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchDespatchFrom.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchDespatchFrom.Image = CType(resources.GetObject("cmdSearchDespatchFrom.Image"), System.Drawing.Image)
        Me.cmdSearchDespatchFrom.Location = New System.Drawing.Point(324, 210)
        Me.cmdSearchDespatchFrom.Name = "cmdSearchDespatchFrom"
        Me.cmdSearchDespatchFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchDespatchFrom.Size = New System.Drawing.Size(24, 22)
        Me.cmdSearchDespatchFrom.TabIndex = 239
        Me.cmdSearchDespatchFrom.TabStop = False
        Me.cmdSearchDespatchFrom.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchDespatchFrom, "Search")
        Me.cmdSearchDespatchFrom.UseVisualStyleBackColor = False
        '
        'cmdSearchShippedTo
        '
        Me.cmdSearchShippedTo.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchShippedTo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchShippedTo.Enabled = False
        Me.cmdSearchShippedTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchShippedTo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchShippedTo.Image = CType(resources.GetObject("cmdSearchShippedTo.Image"), System.Drawing.Image)
        Me.cmdSearchShippedTo.Location = New System.Drawing.Point(324, 254)
        Me.cmdSearchShippedTo.Name = "cmdSearchShippedTo"
        Me.cmdSearchShippedTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchShippedTo.Size = New System.Drawing.Size(24, 22)
        Me.cmdSearchShippedTo.TabIndex = 30
        Me.cmdSearchShippedTo.TabStop = False
        Me.cmdSearchShippedTo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchShippedTo, "Search")
        Me.cmdSearchShippedTo.UseVisualStyleBackColor = False
        Me.cmdSearchShippedTo.Visible = False
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
        Me.txtPOAmendNo.Location = New System.Drawing.Point(95, 16)
        Me.txtPOAmendNo.MaxLength = 0
        Me.txtPOAmendNo.Name = "txtPOAmendNo"
        Me.txtPOAmendNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPOAmendNo.Size = New System.Drawing.Size(77, 20)
        Me.txtPOAmendNo.TabIndex = 165
        Me.ToolTip1.SetToolTip(Me.txtPOAmendNo, "click double click or F1 for search")
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.BackColor = System.Drawing.SystemColors.Control
        Me.Label41.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label41.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label41.Location = New System.Drawing.Point(178, 18)
        Me.Label41.Name = "Label41"
        Me.Label41.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label41.Size = New System.Drawing.Size(77, 14)
        Me.Label41.TabIndex = 233
        Me.Label41.Text = "PO WEF Date :"
        Me.Label41.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.ToolTip1.SetToolTip(Me.Label41, "AWB/RRP No.")
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.BackColor = System.Drawing.SystemColors.Control
        Me.Label39.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label39.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label39.Location = New System.Drawing.Point(12, 18)
        Me.Label39.Name = "Label39"
        Me.Label39.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label39.Size = New System.Drawing.Size(79, 14)
        Me.Label39.TabIndex = 232
        Me.Label39.Text = "PO Amend No :"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.ToolTip1.SetToolTip(Me.Label39, "AWB/RRP No.")
        '
        'txtDNNo
        '
        Me.txtDNNo.AcceptsReturn = True
        Me.txtDNNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtDNNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDNNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDNNo.Enabled = False
        Me.txtDNNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDNNo.ForeColor = System.Drawing.Color.Blue
        Me.txtDNNo.Location = New System.Drawing.Point(95, 18)
        Me.txtDNNo.MaxLength = 0
        Me.txtDNNo.Name = "txtDNNo"
        Me.txtDNNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDNNo.Size = New System.Drawing.Size(77, 20)
        Me.txtDNNo.TabIndex = 159
        Me.ToolTip1.SetToolTip(Me.txtDNNo, "click double click or F1 for search")
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.BackColor = System.Drawing.SystemColors.Control
        Me.Label37.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label37.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label37.Location = New System.Drawing.Point(6, 20)
        Me.Label37.Name = "Label37"
        Me.Label37.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label37.Size = New System.Drawing.Size(78, 14)
        Me.Label37.TabIndex = 163
        Me.Label37.Text = "DN No && Date :"
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.ToolTip1.SetToolTip(Me.Label37, "AWB/RRP No.")
        '
        'cmdCoBuyerSearch
        '
        Me.cmdCoBuyerSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdCoBuyerSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCoBuyerSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCoBuyerSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCoBuyerSearch.Image = CType(resources.GetObject("cmdCoBuyerSearch.Image"), System.Drawing.Image)
        Me.cmdCoBuyerSearch.Location = New System.Drawing.Point(352, 250)
        Me.cmdCoBuyerSearch.Name = "cmdCoBuyerSearch"
        Me.cmdCoBuyerSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCoBuyerSearch.Size = New System.Drawing.Size(25, 21)
        Me.cmdCoBuyerSearch.TabIndex = 52
        Me.cmdCoBuyerSearch.TabStop = False
        Me.cmdCoBuyerSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdCoBuyerSearch, "Search")
        Me.cmdCoBuyerSearch.UseVisualStyleBackColor = False
        '
        'cmdBuyerSearch
        '
        Me.cmdBuyerSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdBuyerSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdBuyerSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdBuyerSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdBuyerSearch.Image = CType(resources.GetObject("cmdBuyerSearch.Image"), System.Drawing.Image)
        Me.cmdBuyerSearch.Location = New System.Drawing.Point(352, 223)
        Me.cmdBuyerSearch.Name = "cmdBuyerSearch"
        Me.cmdBuyerSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdBuyerSearch.Size = New System.Drawing.Size(25, 21)
        Me.cmdBuyerSearch.TabIndex = 50
        Me.cmdBuyerSearch.TabStop = False
        Me.cmdBuyerSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdBuyerSearch, "Search")
        Me.cmdBuyerSearch.UseVisualStyleBackColor = False
        '
        'cmpPrinteInvoice
        '
        Me.cmpPrinteInvoice.BackColor = System.Drawing.SystemColors.Control
        Me.cmpPrinteInvoice.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmpPrinteInvoice.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmpPrinteInvoice.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmpPrinteInvoice.Image = CType(resources.GetObject("cmpPrinteInvoice.Image"), System.Drawing.Image)
        Me.cmpPrinteInvoice.Location = New System.Drawing.Point(292, 126)
        Me.cmpPrinteInvoice.Name = "cmpPrinteInvoice"
        Me.cmpPrinteInvoice.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmpPrinteInvoice.Size = New System.Drawing.Size(136, 29)
        Me.cmpPrinteInvoice.TabIndex = 249
        Me.cmpPrinteInvoice.Text = "Print e-Invoice"
        Me.cmpPrinteInvoice.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmpPrinteInvoice, "Delete")
        Me.cmpPrinteInvoice.UseVisualStyleBackColor = False
        '
        'cmdQRCode
        '
        Me.cmdQRCode.BackColor = System.Drawing.SystemColors.Control
        Me.cmdQRCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdQRCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdQRCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdQRCode.Image = CType(resources.GetObject("cmdQRCode.Image"), System.Drawing.Image)
        Me.cmdQRCode.Location = New System.Drawing.Point(170, 126)
        Me.cmdQRCode.Name = "cmdQRCode"
        Me.cmdQRCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdQRCode.Size = New System.Drawing.Size(100, 29)
        Me.cmdQRCode.TabIndex = 248
        Me.cmdQRCode.Text = "Generate QR Code"
        Me.cmdQRCode.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdQRCode, "Delete")
        Me.cmdQRCode.UseVisualStyleBackColor = False
        '
        'cmdeInvoice
        '
        Me.cmdeInvoice.BackColor = System.Drawing.SystemColors.Control
        Me.cmdeInvoice.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdeInvoice.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdeInvoice.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdeInvoice.Location = New System.Drawing.Point(12, 126)
        Me.cmdeInvoice.Name = "cmdeInvoice"
        Me.cmdeInvoice.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdeInvoice.Size = New System.Drawing.Size(136, 29)
        Me.cmdeInvoice.TabIndex = 247
        Me.cmdeInvoice.Text = "Generate IRN && QR Code"
        Me.ToolTip1.SetToolTip(Me.cmdeInvoice, "Delete")
        Me.cmdeInvoice.UseVisualStyleBackColor = False
        '
        'CmdSearchDC
        '
        Me.CmdSearchDC.BackColor = System.Drawing.SystemColors.Menu
        Me.CmdSearchDC.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSearchDC.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSearchDC.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSearchDC.Image = CType(resources.GetObject("CmdSearchDC.Image"), System.Drawing.Image)
        Me.CmdSearchDC.Location = New System.Drawing.Point(225, 13)
        Me.CmdSearchDC.Name = "CmdSearchDC"
        Me.CmdSearchDC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSearchDC.Size = New System.Drawing.Size(24, 21)
        Me.CmdSearchDC.TabIndex = 2
        Me.CmdSearchDC.TabStop = False
        Me.CmdSearchDC.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSearchDC, "Seach Pending DC")
        Me.CmdSearchDC.UseVisualStyleBackColor = False
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.BackColor = System.Drawing.SystemColors.Control
        Me.Label36.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label36.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label36.Location = New System.Drawing.Point(35, 135)
        Me.Label36.Name = "Label36"
        Me.Label36.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label36.Size = New System.Drawing.Size(43, 14)
        Me.Label36.TabIndex = 97
        Me.Label36.Text = "PO No :"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.ToolTip1.SetToolTip(Me.Label36, "AWB/RRP No.")
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(34, 114)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(44, 14)
        Me.Label19.TabIndex = 77
        Me.Label19.Text = "GR No :"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.ToolTip1.SetToolTip(Me.Label19, "AWB/RRP No.")
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Info
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.Location = New System.Drawing.Point(828, 10)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(74, 41)
        Me.cmdClose.TabIndex = 61
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
        Me.CmdView.Location = New System.Drawing.Point(754, 10)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(74, 41)
        Me.CmdView.TabIndex = 60
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
        Me.CmdPreview.Location = New System.Drawing.Point(680, 10)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(74, 41)
        Me.CmdPreview.TabIndex = 59
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
        Me.cmdPrint.Location = New System.Drawing.Point(606, 10)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(74, 41)
        Me.cmdPrint.TabIndex = 58
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
        Me.cmdPostingHead.Location = New System.Drawing.Point(458, 10)
        Me.cmdPostingHead.Name = "cmdPostingHead"
        Me.cmdPostingHead.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPostingHead.Size = New System.Drawing.Size(74, 41)
        Me.cmdPostingHead.TabIndex = 115
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
        Me.cmdBarCode.Location = New System.Drawing.Point(384, 10)
        Me.cmdBarCode.Name = "cmdBarCode"
        Me.cmdBarCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdBarCode.Size = New System.Drawing.Size(74, 41)
        Me.cmdBarCode.TabIndex = 56
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
        Me.cmdDelete.Location = New System.Drawing.Point(310, 10)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDelete.Size = New System.Drawing.Size(74, 41)
        Me.cmdDelete.TabIndex = 55
        Me.cmdDelete.Text = "&Delete"
        Me.cmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdDelete, "Delete")
        Me.cmdDelete.UseVisualStyleBackColor = False
        '
        'cmdAuthorised
        '
        Me.cmdAuthorised.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAuthorised.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAuthorised.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAuthorised.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAuthorised.Image = CType(resources.GetObject("cmdAuthorised.Image"), System.Drawing.Image)
        Me.cmdAuthorised.Location = New System.Drawing.Point(236, 10)
        Me.cmdAuthorised.Name = "cmdAuthorised"
        Me.cmdAuthorised.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAuthorised.Size = New System.Drawing.Size(74, 41)
        Me.cmdAuthorised.TabIndex = 118
        Me.cmdAuthorised.Text = "A&uthorised"
        Me.cmdAuthorised.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdAuthorised, "Save & Print Voucher")
        Me.cmdAuthorised.UseVisualStyleBackColor = False
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Image = CType(resources.GetObject("cmdSave.Image"), System.Drawing.Image)
        Me.cmdSave.Location = New System.Drawing.Point(162, 10)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(74, 41)
        Me.cmdSave.TabIndex = 54
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
        Me.cmdModify.Location = New System.Drawing.Point(88, 10)
        Me.cmdModify.Name = "cmdModify"
        Me.cmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdModify.Size = New System.Drawing.Size(74, 41)
        Me.cmdModify.TabIndex = 53
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
        Me.cmdAdd.Location = New System.Drawing.Point(14, 10)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAdd.Size = New System.Drawing.Size(74, 41)
        Me.cmdAdd.TabIndex = 0
        Me.cmdAdd.Text = "&Add"
        Me.cmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdAdd, "Add New")
        Me.cmdAdd.UseVisualStyleBackColor = False
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.CausesValidation = False
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.Location = New System.Drawing.Point(532, 10)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(74, 41)
        Me.cmdSavePrint.TabIndex = 57
        Me.cmdSavePrint.Text = "F4- Print"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save & Print")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'txtDistanceUpdate
        '
        Me.txtDistanceUpdate.BackColor = System.Drawing.SystemColors.Menu
        Me.txtDistanceUpdate.Cursor = System.Windows.Forms.Cursors.Default
        Me.txtDistanceUpdate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDistanceUpdate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtDistanceUpdate.Location = New System.Drawing.Point(258, 134)
        Me.txtDistanceUpdate.Name = "txtDistanceUpdate"
        Me.txtDistanceUpdate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDistanceUpdate.Size = New System.Drawing.Size(126, 44)
        Me.txtDistanceUpdate.TabIndex = 226
        Me.txtDistanceUpdate.TabStop = False
        Me.txtDistanceUpdate.Text = "Distance Transport Details"
        Me.ToolTip1.SetToolTip(Me.txtDistanceUpdate, "Distance Update")
        Me.txtDistanceUpdate.UseVisualStyleBackColor = False
        '
        'cmdeWayBill
        '
        Me.cmdeWayBill.BackColor = System.Drawing.SystemColors.Control
        Me.cmdeWayBill.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdeWayBill.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdeWayBill.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdeWayBill.Image = CType(resources.GetObject("cmdeWayBill.Image"), System.Drawing.Image)
        Me.cmdeWayBill.Location = New System.Drawing.Point(158, 74)
        Me.cmdeWayBill.Name = "cmdeWayBill"
        Me.cmdeWayBill.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdeWayBill.Size = New System.Drawing.Size(160, 29)
        Me.cmdeWayBill.TabIndex = 250
        Me.cmdeWayBill.Text = "Print e-Way Bill"
        Me.cmdeWayBill.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdeWayBill, "Delete")
        Me.cmdeWayBill.UseVisualStyleBackColor = False
        '
        'cmdResetPO
        '
        Me.cmdResetPO.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdResetPO.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdResetPO.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdResetPO.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdResetPO.Location = New System.Drawing.Point(444, 130)
        Me.cmdResetPO.Name = "cmdResetPO"
        Me.cmdResetPO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdResetPO.Size = New System.Drawing.Size(66, 26)
        Me.cmdResetPO.TabIndex = 243
        Me.cmdResetPO.TabStop = False
        Me.cmdResetPO.Text = "Reset PO"
        Me.ToolTip1.SetToolTip(Me.cmdResetPO, "Distance Update")
        Me.cmdResetPO.UseVisualStyleBackColor = False
        '
        'Label70
        '
        Me.Label70.AutoSize = True
        Me.Label70.BackColor = System.Drawing.SystemColors.Control
        Me.Label70.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label70.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label70.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label70.Location = New System.Drawing.Point(460, 314)
        Me.Label70.Name = "Label70"
        Me.Label70.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label70.Size = New System.Drawing.Size(69, 14)
        Me.Label70.TabIndex = 187
        Me.Label70.Text = "TDS on Sale:"
        Me.Label70.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.ToolTip1.SetToolTip(Me.Label70, "AWB/RRP No.")
        '
        'CmdPopFromFile
        '
        Me.CmdPopFromFile.BackColor = System.Drawing.SystemColors.Menu
        Me.CmdPopFromFile.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPopFromFile.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPopFromFile.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPopFromFile.Location = New System.Drawing.Point(447, 82)
        Me.CmdPopFromFile.Name = "CmdPopFromFile"
        Me.CmdPopFromFile.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPopFromFile.Size = New System.Drawing.Size(150, 26)
        Me.CmdPopFromFile.TabIndex = 244
        Me.CmdPopFromFile.TabStop = False
        Me.CmdPopFromFile.Text = "Rate Update From Excel"
        Me.ToolTip1.SetToolTip(Me.CmdPopFromFile, "Search")
        Me.CmdPopFromFile.UseVisualStyleBackColor = False
        Me.CmdPopFromFile.Visible = False
        '
        'FraFront
        '
        Me.FraFront.BackColor = System.Drawing.SystemColors.Control
        Me.FraFront.Controls.Add(Me.CmdPopFromFile)
        Me.FraFront.Controls.Add(Me.cmdResetPO)
        Me.FraFront.Controls.Add(Me.txtAddress)
        Me.FraFront.Controls.Add(Me.txtVendorCode)
        Me.FraFront.Controls.Add(Me.Label64)
        Me.FraFront.Controls.Add(Me.txtBillTo)
        Me.FraFront.Controls.Add(Me.Label62)
        Me.FraFront.Controls.Add(Me.chkRejection)
        Me.FraFront.Controls.Add(Me.chkLUT)
        Me.FraFront.Controls.Add(Me.cboDivision)
        Me.FraFront.Controls.Add(Me.txtPONo)
        Me.FraFront.Controls.Add(Me.txtPODate)
        Me.FraFront.Controls.Add(Me.chkFOC)
        Me.FraFront.Controls.Add(Me.chkCancelled)
        Me.FraFront.Controls.Add(Me.TabMain)
        Me.FraFront.Controls.Add(Me.TxtGRDate)
        Me.FraFront.Controls.Add(Me.TxtGRNo)
        Me.FraFront.Controls.Add(Me.txtCreditAccount)
        Me.FraFront.Controls.Add(Me.txtCustomer)
        Me.FraFront.Controls.Add(Me.txtRemovalDate)
        Me.FraFront.Controls.Add(Me.txtRemovalTime)
        Me.FraFront.Controls.Add(Me.txtDCDate)
        Me.FraFront.Controls.Add(Me.txtBillDate)
        Me.FraFront.Controls.Add(Me.TxtBillTm)
        Me.FraFront.Controls.Add(Me.cboInvType)
        Me.FraFront.Controls.Add(Me.txtDCNo)
        Me.FraFront.Controls.Add(Me.CmdSearchDC)
        Me.FraFront.Controls.Add(Me.txtBillNoPrefix)
        Me.FraFront.Controls.Add(Me.txtBillNoSuffix)
        Me.FraFront.Controls.Add(Me.txtBillNo)
        Me.FraFront.Controls.Add(Me.TxtDCNoPrefix)
        Me.FraFront.Controls.Add(Me.txtDCNoSuffix)
        Me.FraFront.Controls.Add(Me.lblInvoiceSeq)
        Me.FraFront.Controls.Add(Me.Label13)
        Me.FraFront.Controls.Add(Me.lblSoDate)
        Me.FraFront.Controls.Add(Me.lblDespRef)
        Me.FraFront.Controls.Add(Me.lblPoNo)
        Me.FraFront.Controls.Add(Me.lblInvHeading)
        Me.FraFront.Controls.Add(Me.Label36)
        Me.FraFront.Controls.Add(Me.Label12)
        Me.FraFront.Controls.Add(Me.Label20)
        Me.FraFront.Controls.Add(Me.Label19)
        Me.FraFront.Controls.Add(Me.Label3)
        Me.FraFront.Controls.Add(Me.lblCust)
        Me.FraFront.Controls.Add(Me.Label7)
        Me.FraFront.Controls.Add(Me.Label15)
        Me.FraFront.Controls.Add(Me.Label5)
        Me.FraFront.Controls.Add(Me.Label2)
        Me.FraFront.Controls.Add(Me.Label14)
        Me.FraFront.Controls.Add(Me.Label1)
        Me.FraFront.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraFront.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraFront.Location = New System.Drawing.Point(1, -4)
        Me.FraFront.Name = "FraFront"
        Me.FraFront.Padding = New System.Windows.Forms.Padding(0)
        Me.FraFront.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraFront.Size = New System.Drawing.Size(908, 569)
        Me.FraFront.TabIndex = 69
        Me.FraFront.TabStop = False
        '
        'txtAddress
        '
        Me.txtAddress.AcceptsReturn = True
        Me.txtAddress.BackColor = System.Drawing.SystemColors.Window
        Me.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAddress.Enabled = False
        Me.txtAddress.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress.ForeColor = System.Drawing.Color.Blue
        Me.txtAddress.Location = New System.Drawing.Point(614, 60)
        Me.txtAddress.MaxLength = 0
        Me.txtAddress.Multiline = True
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAddress.Size = New System.Drawing.Size(287, 48)
        Me.txtAddress.TabIndex = 242
        '
        'txtVendorCode
        '
        Me.txtVendorCode.AcceptsReturn = True
        Me.txtVendorCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtVendorCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVendorCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVendorCode.Enabled = False
        Me.txtVendorCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVendorCode.ForeColor = System.Drawing.Color.Blue
        Me.txtVendorCode.Location = New System.Drawing.Point(820, 133)
        Me.txtVendorCode.MaxLength = 0
        Me.txtVendorCode.Name = "txtVendorCode"
        Me.txtVendorCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVendorCode.Size = New System.Drawing.Size(81, 22)
        Me.txtVendorCode.TabIndex = 240
        '
        'Label64
        '
        Me.Label64.AutoSize = True
        Me.Label64.BackColor = System.Drawing.SystemColors.Control
        Me.Label64.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label64.Enabled = False
        Me.Label64.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label64.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label64.Location = New System.Drawing.Point(737, 137)
        Me.Label64.Name = "Label64"
        Me.Label64.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label64.Size = New System.Drawing.Size(79, 13)
        Me.Label64.TabIndex = 241
        Me.Label64.Text = "Vendor Code :"
        Me.Label64.TextAlign = System.Drawing.ContentAlignment.TopRight
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
        Me.txtBillTo.Location = New System.Drawing.Point(614, 133)
        Me.txtBillTo.MaxLength = 0
        Me.txtBillTo.Name = "txtBillTo"
        Me.txtBillTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillTo.Size = New System.Drawing.Size(108, 22)
        Me.txtBillTo.TabIndex = 236
        '
        'Label62
        '
        Me.Label62.AutoSize = True
        Me.Label62.BackColor = System.Drawing.SystemColors.Control
        Me.Label62.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label62.Enabled = False
        Me.Label62.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label62.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label62.Location = New System.Drawing.Point(520, 135)
        Me.Label62.Name = "Label62"
        Me.Label62.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label62.Size = New System.Drawing.Size(90, 13)
        Me.Label62.TabIndex = 239
        Me.Label62.Text = "Bill To Location :"
        Me.Label62.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'chkRejection
        '
        Me.chkRejection.BackColor = System.Drawing.SystemColors.Control
        Me.chkRejection.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkRejection.Enabled = False
        Me.chkRejection.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRejection.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkRejection.Location = New System.Drawing.Point(794, 159)
        Me.chkRejection.Name = "chkRejection"
        Me.chkRejection.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkRejection.Size = New System.Drawing.Size(103, 16)
        Me.chkRejection.TabIndex = 231
        Me.chkRejection.Text = "Purchase Rtn"
        Me.chkRejection.UseVisualStyleBackColor = False
        '
        'chkLUT
        '
        Me.chkLUT.BackColor = System.Drawing.SystemColors.Control
        Me.chkLUT.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkLUT.Enabled = False
        Me.chkLUT.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkLUT.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkLUT.Location = New System.Drawing.Point(708, 159)
        Me.chkLUT.Name = "chkLUT"
        Me.chkLUT.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkLUT.Size = New System.Drawing.Size(105, 16)
        Me.chkLUT.TabIndex = 210
        Me.chkLUT.Text = "Under LUT"
        Me.chkLUT.UseVisualStyleBackColor = False
        '
        'cboDivision
        '
        Me.cboDivision.BackColor = System.Drawing.SystemColors.Window
        Me.cboDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivision.Enabled = False
        Me.cboDivision.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDivision.Location = New System.Drawing.Point(82, 85)
        Me.cboDivision.Name = "cboDivision"
        Me.cboDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivision.Size = New System.Drawing.Size(162, 22)
        Me.cboDivision.TabIndex = 11
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
        Me.txtPONo.Location = New System.Drawing.Point(82, 133)
        Me.txtPONo.MaxLength = 0
        Me.txtPONo.Name = "txtPONo"
        Me.txtPONo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPONo.Size = New System.Drawing.Size(163, 20)
        Me.txtPONo.TabIndex = 17
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
        Me.txtPODate.Location = New System.Drawing.Point(338, 133)
        Me.txtPODate.MaxLength = 0
        Me.txtPODate.Name = "txtPODate"
        Me.txtPODate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPODate.Size = New System.Drawing.Size(103, 20)
        Me.txtPODate.TabIndex = 18
        '
        'chkFOC
        '
        Me.chkFOC.BackColor = System.Drawing.SystemColors.Control
        Me.chkFOC.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkFOC.Enabled = False
        Me.chkFOC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFOC.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkFOC.Location = New System.Drawing.Point(642, 159)
        Me.chkFOC.Name = "chkFOC"
        Me.chkFOC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkFOC.Size = New System.Drawing.Size(59, 16)
        Me.chkFOC.TabIndex = 20
        Me.chkFOC.Text = "F.O.C."
        Me.chkFOC.UseVisualStyleBackColor = False
        '
        'chkCancelled
        '
        Me.chkCancelled.BackColor = System.Drawing.SystemColors.Control
        Me.chkCancelled.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCancelled.Enabled = False
        Me.chkCancelled.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCancelled.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkCancelled.Location = New System.Drawing.Point(554, 159)
        Me.chkCancelled.Name = "chkCancelled"
        Me.chkCancelled.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCancelled.Size = New System.Drawing.Size(81, 16)
        Me.chkCancelled.TabIndex = 19
        Me.chkCancelled.Text = "Cancelled"
        Me.chkCancelled.UseVisualStyleBackColor = False
        '
        'TabMain
        '
        Me.TabMain.Controls.Add(Me._TabMain_TabPage0)
        Me.TabMain.Controls.Add(Me._TabMain_TabPage1)
        Me.TabMain.Controls.Add(Me._TabMain_TabPage2)
        Me.TabMain.Controls.Add(Me._TabMain_TabPage3)
        Me.TabMain.Controls.Add(Me._TabMain_TabPage4)
        Me.TabMain.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabMain.ItemSize = New System.Drawing.Size(42, 18)
        Me.TabMain.Location = New System.Drawing.Point(-2, 158)
        Me.TabMain.Name = "TabMain"
        Me.TabMain.SelectedIndex = 2
        Me.TabMain.Size = New System.Drawing.Size(912, 414)
        Me.TabMain.TabIndex = 79
        '
        '_TabMain_TabPage0
        '
        Me._TabMain_TabPage0.Controls.Add(Me.Frame6)
        Me._TabMain_TabPage0.Location = New System.Drawing.Point(4, 22)
        Me._TabMain_TabPage0.Name = "_TabMain_TabPage0"
        Me._TabMain_TabPage0.Size = New System.Drawing.Size(904, 388)
        Me._TabMain_TabPage0.TabIndex = 0
        Me._TabMain_TabPage0.Text = "Item Details"
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.lblCompanyCode)
        Me.Frame6.Controls.Add(Me.txtTDSOnSale)
        Me.Frame6.Controls.Add(Me.Label70)
        Me.Frame6.Controls.Add(Me.FraPostingDtl)
        Me.Frame6.Controls.Add(Me.txtCustMatValue)
        Me.Frame6.Controls.Add(Me.SprdExp)
        Me.Frame6.Controls.Add(Me.SprdMain)
        Me.Frame6.Controls.Add(Me.lblTotTaxableAmt)
        Me.Frame6.Controls.Add(Me._Label_9)
        Me.Frame6.Controls.Add(Me.Label17)
        Me.Frame6.Controls.Add(Me.lblTotCD)
        Me.Frame6.Controls.Add(Me.lblCDLabel)
        Me.Frame6.Controls.Add(Me.lblEDUOnCDAmount)
        Me.Frame6.Controls.Add(Me.lblCessCDLabel)
        Me.Frame6.Controls.Add(Me._Label_23)
        Me.Frame6.Controls.Add(Me.lblMRPValue)
        Me.Frame6.Controls.Add(Me._Label_22)
        Me.Frame6.Controls.Add(Me.lblTotItemValue)
        Me.Frame6.Controls.Add(Me.lblTotQty)
        Me.Frame6.Controls.Add(Me.lblTotPackQtyCap)
        Me.Frame6.Controls.Add(Me._Label_29)
        Me.Frame6.Controls.Add(Me._Label_25)
        Me.Frame6.Controls.Add(Me.lblTotSGSTAmount)
        Me.Frame6.Controls.Add(Me._Label_26)
        Me.Frame6.Controls.Add(Me.lblTotIGSTAmount)
        Me.Frame6.Controls.Add(Me.lblServicePercentage)
        Me.Frame6.Controls.Add(Me.lblTCS)
        Me.Frame6.Controls.Add(Me.lblTCSPercentage)
        Me.Frame6.Controls.Add(Me.lblMSC)
        Me.Frame6.Controls.Add(Me.lblRO)
        Me.Frame6.Controls.Add(Me.lblTotExpAmt)
        Me.Frame6.Controls.Add(Me.LblBookCode)
        Me.Frame6.Controls.Add(Me.LblMKey)
        Me.Frame6.Controls.Add(Me._Label_24)
        Me.Frame6.Controls.Add(Me.lblTotCGSTAmount)
        Me.Frame6.Controls.Add(Me.lblNetAmount)
        Me.Frame6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(3, 0)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(898, 387)
        Me.Frame6.TabIndex = 85
        Me.Frame6.TabStop = False
        '
        'lblCompanyCode
        '
        Me.lblCompanyCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblCompanyCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCompanyCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompanyCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCompanyCode.Location = New System.Drawing.Point(543, 334)
        Me.lblCompanyCode.Name = "lblCompanyCode"
        Me.lblCompanyCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCompanyCode.Size = New System.Drawing.Size(95, 21)
        Me.lblCompanyCode.TabIndex = 186
        Me.lblCompanyCode.Text = "lblCompanyCode"
        Me.lblCompanyCode.Visible = False
        '
        'txtTDSOnSale
        '
        Me.txtTDSOnSale.AcceptsReturn = True
        Me.txtTDSOnSale.BackColor = System.Drawing.SystemColors.Window
        Me.txtTDSOnSale.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTDSOnSale.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTDSOnSale.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTDSOnSale.ForeColor = System.Drawing.Color.Blue
        Me.txtTDSOnSale.Location = New System.Drawing.Point(534, 311)
        Me.txtTDSOnSale.MaxLength = 0
        Me.txtTDSOnSale.Name = "txtTDSOnSale"
        Me.txtTDSOnSale.ReadOnly = True
        Me.txtTDSOnSale.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTDSOnSale.Size = New System.Drawing.Size(95, 20)
        Me.txtTDSOnSale.TabIndex = 186
        '
        'FraPostingDtl
        '
        Me.FraPostingDtl.BackColor = System.Drawing.SystemColors.Control
        Me.FraPostingDtl.Controls.Add(Me.SprdPostingDetail)
        Me.FraPostingDtl.Enabled = False
        Me.FraPostingDtl.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraPostingDtl.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraPostingDtl.Location = New System.Drawing.Point(2, 189)
        Me.FraPostingDtl.Name = "FraPostingDtl"
        Me.FraPostingDtl.Padding = New System.Windows.Forms.Padding(0)
        Me.FraPostingDtl.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraPostingDtl.Size = New System.Drawing.Size(432, 196)
        Me.FraPostingDtl.TabIndex = 156
        Me.FraPostingDtl.TabStop = False
        Me.FraPostingDtl.Visible = False
        '
        'SprdPostingDetail
        '
        Me.SprdPostingDetail.DataSource = Nothing
        Me.SprdPostingDetail.Location = New System.Drawing.Point(4, 10)
        Me.SprdPostingDetail.Name = "SprdPostingDetail"
        Me.SprdPostingDetail.OcxState = CType(resources.GetObject("SprdPostingDetail.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdPostingDetail.Size = New System.Drawing.Size(422, 182)
        Me.SprdPostingDetail.TabIndex = 157
        '
        'txtCustMatValue
        '
        Me.txtCustMatValue.AcceptsReturn = True
        Me.txtCustMatValue.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustMatValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustMatValue.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustMatValue.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustMatValue.ForeColor = System.Drawing.Color.Blue
        Me.txtCustMatValue.Location = New System.Drawing.Point(534, 288)
        Me.txtCustMatValue.MaxLength = 0
        Me.txtCustMatValue.Name = "txtCustMatValue"
        Me.txtCustMatValue.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustMatValue.Size = New System.Drawing.Size(95, 20)
        Me.txtCustMatValue.TabIndex = 154
        '
        'SprdExp
        '
        Me.SprdExp.DataSource = Nothing
        Me.SprdExp.Location = New System.Drawing.Point(2, 270)
        Me.SprdExp.Name = "SprdExp"
        Me.SprdExp.OcxState = CType(resources.GetObject("SprdExp.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdExp.Size = New System.Drawing.Size(420, 115)
        Me.SprdExp.TabIndex = 137
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 11)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(896, 253)
        Me.SprdMain.TabIndex = 21
        '
        'lblTotTaxableAmt
        '
        Me.lblTotTaxableAmt.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotTaxableAmt.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotTaxableAmt.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotTaxableAmt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTotTaxableAmt.Location = New System.Drawing.Point(534, 352)
        Me.lblTotTaxableAmt.Name = "lblTotTaxableAmt"
        Me.lblTotTaxableAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotTaxableAmt.Size = New System.Drawing.Size(95, 17)
        Me.lblTotTaxableAmt.TabIndex = 185
        Me.lblTotTaxableAmt.Text = "0"
        Me.lblTotTaxableAmt.Visible = False
        '
        '_Label_9
        '
        Me._Label_9.AutoSize = True
        Me._Label_9.BackColor = System.Drawing.SystemColors.Control
        Me._Label_9.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label_9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label_9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label.SetIndex(Me._Label_9, CType(9, Short))
        Me._Label_9.Location = New System.Drawing.Point(723, 350)
        Me._Label_9.Name = "_Label_9"
        Me._Label_9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label_9.Size = New System.Drawing.Size(64, 14)
        Me._Label_9.TabIndex = 184
        Me._Label_9.Text = "Other Exp. :"
        Me._Label_9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotCD
        '
        Me.lblTotCD.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotCD.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotCD.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotCD.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotCD.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotCD.Location = New System.Drawing.Point(709, 309)
        Me.lblTotCD.Name = "lblTotCD"
        Me.lblTotCD.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotCD.Size = New System.Drawing.Size(33, 17)
        Me.lblTotCD.TabIndex = 136
        Me.lblTotCD.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCDLabel
        '
        Me.lblCDLabel.AutoSize = True
        Me.lblCDLabel.BackColor = System.Drawing.SystemColors.Control
        Me.lblCDLabel.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCDLabel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCDLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCDLabel.Location = New System.Drawing.Point(630, 311)
        Me.lblCDLabel.Name = "lblCDLabel"
        Me.lblCDLabel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCDLabel.Size = New System.Drawing.Size(74, 14)
        Me.lblCDLabel.TabIndex = 135
        Me.lblCDLabel.Text = "Custom Duty :"
        Me.lblCDLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblEDUOnCDAmount
        '
        Me.lblEDUOnCDAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblEDUOnCDAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblEDUOnCDAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblEDUOnCDAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEDUOnCDAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblEDUOnCDAmount.Location = New System.Drawing.Point(709, 329)
        Me.lblEDUOnCDAmount.Name = "lblEDUOnCDAmount"
        Me.lblEDUOnCDAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblEDUOnCDAmount.Size = New System.Drawing.Size(33, 17)
        Me.lblEDUOnCDAmount.TabIndex = 134
        Me.lblEDUOnCDAmount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCessCDLabel
        '
        Me.lblCessCDLabel.AutoSize = True
        Me.lblCessCDLabel.BackColor = System.Drawing.SystemColors.Control
        Me.lblCessCDLabel.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCessCDLabel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCessCDLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCessCDLabel.Location = New System.Drawing.Point(632, 331)
        Me.lblCessCDLabel.Name = "lblCessCDLabel"
        Me.lblCessCDLabel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCessCDLabel.Size = New System.Drawing.Size(72, 14)
        Me.lblCessCDLabel.TabIndex = 133
        Me.lblCessCDLabel.Text = "Cess On CD :"
        Me.lblCessCDLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label_23
        '
        Me._Label_23.AutoSize = True
        Me._Label_23.BackColor = System.Drawing.SystemColors.Control
        Me._Label_23.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label_23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label_23.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label.SetIndex(Me._Label_23, CType(23, Short))
        Me._Label_23.Location = New System.Drawing.Point(465, 365)
        Me._Label_23.Name = "_Label_23"
        Me._Label_23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label_23.Size = New System.Drawing.Size(64, 14)
        Me._Label_23.TabIndex = 132
        Me._Label_23.Text = "MRP Value :"
        Me._Label_23.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMRPValue
        '
        Me.lblMRPValue.BackColor = System.Drawing.SystemColors.Control
        Me.lblMRPValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMRPValue.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMRPValue.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMRPValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblMRPValue.Location = New System.Drawing.Point(534, 362)
        Me.lblMRPValue.Name = "lblMRPValue"
        Me.lblMRPValue.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMRPValue.Size = New System.Drawing.Size(95, 17)
        Me.lblMRPValue.TabIndex = 131
        Me.lblMRPValue.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label_22
        '
        Me._Label_22.AutoSize = True
        Me._Label_22.BackColor = System.Drawing.SystemColors.Control
        Me._Label_22.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label_22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label_22.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label.SetIndex(Me._Label_22, CType(22, Short))
        Me._Label_22.Location = New System.Drawing.Point(725, 272)
        Me._Label_22.Name = "_Label_22"
        Me._Label_22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label_22.Size = New System.Drawing.Size(62, 14)
        Me._Label_22.TabIndex = 91
        Me._Label_22.Text = "Item Value :"
        Me._Label_22.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotItemValue
        '
        Me.lblTotItemValue.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotItemValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotItemValue.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotItemValue.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotItemValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotItemValue.Location = New System.Drawing.Point(792, 269)
        Me.lblTotItemValue.Name = "lblTotItemValue"
        Me.lblTotItemValue.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotItemValue.Size = New System.Drawing.Size(98, 17)
        Me.lblTotItemValue.TabIndex = 90
        Me.lblTotItemValue.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotQty
        '
        Me.lblTotQty.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotQty.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotQty.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotQty.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotQty.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotQty.Location = New System.Drawing.Point(534, 269)
        Me.lblTotQty.Name = "lblTotQty"
        Me.lblTotQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotQty.Size = New System.Drawing.Size(95, 17)
        Me.lblTotQty.TabIndex = 87
        Me.lblTotQty.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotPackQtyCap
        '
        Me.lblTotPackQtyCap.AutoSize = True
        Me.lblTotPackQtyCap.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotPackQtyCap.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotPackQtyCap.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotPackQtyCap.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotPackQtyCap.Location = New System.Drawing.Point(474, 271)
        Me.lblTotPackQtyCap.Name = "lblTotPackQtyCap"
        Me.lblTotPackQtyCap.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotPackQtyCap.Size = New System.Drawing.Size(55, 14)
        Me.lblTotPackQtyCap.TabIndex = 86
        Me.lblTotPackQtyCap.Text = "Total Qty :"
        Me.lblTotPackQtyCap.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label_29
        '
        Me._Label_29.AutoSize = True
        Me._Label_29.BackColor = System.Drawing.SystemColors.Control
        Me._Label_29.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label_29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label_29.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label.SetIndex(Me._Label_29, CType(29, Short))
        Me._Label_29.Location = New System.Drawing.Point(719, 369)
        Me._Label_29.Name = "_Label_29"
        Me._Label_29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label_29.Size = New System.Drawing.Size(68, 14)
        Me._Label_29.TabIndex = 89
        Me._Label_29.Text = "Net Amount :"
        Me._Label_29.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label_25
        '
        Me._Label_25.AutoSize = True
        Me._Label_25.BackColor = System.Drawing.SystemColors.Control
        Me._Label_25.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label_25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label_25.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label.SetIndex(Me._Label_25, CType(25, Short))
        Me._Label_25.Location = New System.Drawing.Point(746, 308)
        Me._Label_25.Name = "_Label_25"
        Me._Label_25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label_25.Size = New System.Drawing.Size(41, 14)
        Me._Label_25.TabIndex = 114
        Me._Label_25.Text = "SGST :"
        Me._Label_25.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotSGSTAmount
        '
        Me.lblTotSGSTAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotSGSTAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotSGSTAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotSGSTAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotSGSTAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotSGSTAmount.Location = New System.Drawing.Point(792, 306)
        Me.lblTotSGSTAmount.Name = "lblTotSGSTAmount"
        Me.lblTotSGSTAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotSGSTAmount.Size = New System.Drawing.Size(98, 17)
        Me.lblTotSGSTAmount.TabIndex = 113
        Me.lblTotSGSTAmount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label_26
        '
        Me._Label_26.AutoSize = True
        Me._Label_26.BackColor = System.Drawing.SystemColors.Control
        Me._Label_26.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label_26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label_26.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label.SetIndex(Me._Label_26, CType(26, Short))
        Me._Label_26.Location = New System.Drawing.Point(751, 328)
        Me._Label_26.Name = "_Label_26"
        Me._Label_26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label_26.Size = New System.Drawing.Size(36, 14)
        Me._Label_26.TabIndex = 112
        Me._Label_26.Text = "IGST :"
        Me._Label_26.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotIGSTAmount
        '
        Me.lblTotIGSTAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotIGSTAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotIGSTAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotIGSTAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotIGSTAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotIGSTAmount.Location = New System.Drawing.Point(792, 326)
        Me.lblTotIGSTAmount.Name = "lblTotIGSTAmount"
        Me.lblTotIGSTAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotIGSTAmount.Size = New System.Drawing.Size(98, 17)
        Me.lblTotIGSTAmount.TabIndex = 111
        Me.lblTotIGSTAmount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblServicePercentage
        '
        Me.lblServicePercentage.BackColor = System.Drawing.SystemColors.Control
        Me.lblServicePercentage.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblServicePercentage.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblServicePercentage.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblServicePercentage.Location = New System.Drawing.Point(578, 236)
        Me.lblServicePercentage.Name = "lblServicePercentage"
        Me.lblServicePercentage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblServicePercentage.Size = New System.Drawing.Size(38, 13)
        Me.lblServicePercentage.TabIndex = 110
        Me.lblServicePercentage.Text = "lblServicePercentage"
        Me.lblServicePercentage.Visible = False
        '
        'lblTCS
        '
        Me.lblTCS.BackColor = System.Drawing.SystemColors.Control
        Me.lblTCS.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTCS.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTCS.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTCS.Location = New System.Drawing.Point(550, 236)
        Me.lblTCS.Name = "lblTCS"
        Me.lblTCS.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTCS.Size = New System.Drawing.Size(43, 15)
        Me.lblTCS.TabIndex = 104
        Me.lblTCS.Text = "lblTCS"
        Me.lblTCS.Visible = False
        '
        'lblTCSPercentage
        '
        Me.lblTCSPercentage.BackColor = System.Drawing.SystemColors.Control
        Me.lblTCSPercentage.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTCSPercentage.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTCSPercentage.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTCSPercentage.Location = New System.Drawing.Point(548, 242)
        Me.lblTCSPercentage.Name = "lblTCSPercentage"
        Me.lblTCSPercentage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTCSPercentage.Size = New System.Drawing.Size(39, 13)
        Me.lblTCSPercentage.TabIndex = 103
        Me.lblTCSPercentage.Text = "lblTCSPercentage"
        Me.lblTCSPercentage.Visible = False
        '
        'lblMSC
        '
        Me.lblMSC.AutoSize = True
        Me.lblMSC.BackColor = System.Drawing.SystemColors.Control
        Me.lblMSC.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMSC.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMSC.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMSC.Location = New System.Drawing.Point(536, 242)
        Me.lblMSC.Name = "lblMSC"
        Me.lblMSC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMSC.Size = New System.Drawing.Size(39, 14)
        Me.lblMSC.TabIndex = 100
        Me.lblMSC.Text = "lblMSC"
        Me.lblMSC.Visible = False
        '
        'lblRO
        '
        Me.lblRO.BackColor = System.Drawing.SystemColors.Control
        Me.lblRO.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblRO.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRO.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRO.Location = New System.Drawing.Point(544, 246)
        Me.lblRO.Name = "lblRO"
        Me.lblRO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblRO.Size = New System.Drawing.Size(41, 11)
        Me.lblRO.TabIndex = 99
        Me.lblRO.Text = "lblRO"
        Me.lblRO.Visible = False
        '
        'lblTotExpAmt
        '
        Me.lblTotExpAmt.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotExpAmt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotExpAmt.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotExpAmt.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotExpAmt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTotExpAmt.Location = New System.Drawing.Point(792, 346)
        Me.lblTotExpAmt.Name = "lblTotExpAmt"
        Me.lblTotExpAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotExpAmt.Size = New System.Drawing.Size(98, 17)
        Me.lblTotExpAmt.TabIndex = 98
        Me.lblTotExpAmt.Text = "0"
        Me.lblTotExpAmt.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LblBookCode
        '
        Me.LblBookCode.AutoSize = True
        Me.LblBookCode.BackColor = System.Drawing.SystemColors.Control
        Me.LblBookCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblBookCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBookCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblBookCode.Location = New System.Drawing.Point(568, 248)
        Me.LblBookCode.Name = "LblBookCode"
        Me.LblBookCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblBookCode.Size = New System.Drawing.Size(70, 14)
        Me.LblBookCode.TabIndex = 95
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
        Me.LblMKey.Location = New System.Drawing.Point(538, 240)
        Me.LblMKey.Name = "LblMKey"
        Me.LblMKey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblMKey.Size = New System.Drawing.Size(48, 14)
        Me.LblMKey.TabIndex = 94
        Me.LblMKey.Text = "LblMKey"
        Me.LblMKey.Visible = False
        '
        '_Label_24
        '
        Me._Label_24.AutoSize = True
        Me._Label_24.BackColor = System.Drawing.SystemColors.Control
        Me._Label_24.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label_24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label_24.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label.SetIndex(Me._Label_24, CType(24, Short))
        Me._Label_24.Location = New System.Drawing.Point(746, 292)
        Me._Label_24.Name = "_Label_24"
        Me._Label_24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label_24.Size = New System.Drawing.Size(41, 14)
        Me._Label_24.TabIndex = 93
        Me._Label_24.Text = "CGST :"
        Me._Label_24.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotCGSTAmount
        '
        Me.lblTotCGSTAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotCGSTAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotCGSTAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotCGSTAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotCGSTAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotCGSTAmount.Location = New System.Drawing.Point(792, 288)
        Me.lblTotCGSTAmount.Name = "lblTotCGSTAmount"
        Me.lblTotCGSTAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotCGSTAmount.Size = New System.Drawing.Size(98, 17)
        Me.lblTotCGSTAmount.TabIndex = 92
        Me.lblTotCGSTAmount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblNetAmount
        '
        Me.lblNetAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblNetAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNetAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblNetAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblNetAmount.Location = New System.Drawing.Point(792, 366)
        Me.lblNetAmount.Name = "lblNetAmount"
        Me.lblNetAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblNetAmount.Size = New System.Drawing.Size(98, 17)
        Me.lblNetAmount.TabIndex = 88
        Me.lblNetAmount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_TabMain_TabPage1
        '
        Me._TabMain_TabPage1.Controls.Add(Me.Frame1)
        Me._TabMain_TabPage1.Location = New System.Drawing.Point(4, 22)
        Me._TabMain_TabPage1.Name = "_TabMain_TabPage1"
        Me._TabMain_TabPage1.Size = New System.Drawing.Size(904, 388)
        Me._TabMain_TabPage1.TabIndex = 1
        Me._TabMain_TabPage1.Text = "Other Details"
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.txtStoreDetail)
        Me.Frame1.Controls.Add(Me.txtApplicant)
        Me.Frame1.Controls.Add(Me.Label71)
        Me.Frame1.Controls.Add(Me.Label72)
        Me.Frame1.Controls.Add(Me.lblModDate)
        Me.Frame1.Controls.Add(Me.Label66)
        Me.Frame1.Controls.Add(Me.lblAddDate)
        Me.Frame1.Controls.Add(Me.Label67)
        Me.Frame1.Controls.Add(Me.lblModUser)
        Me.Frame1.Controls.Add(Me.Label68)
        Me.Frame1.Controls.Add(Me.lblAddUser)
        Me.Frame1.Controls.Add(Me.Label69)
        Me.Frame1.Controls.Add(Me.txtPacking)
        Me.Frame1.Controls.Add(Me.Label65)
        Me.Frame1.Controls.Add(Me.TxtShipTo)
        Me.Frame1.Controls.Add(Me.Label63)
        Me.Frame1.Controls.Add(Me.cmdSearchDespatchFrom)
        Me.Frame1.Controls.Add(Me.txtShippedFrom)
        Me.Frame1.Controls.Add(Me.chkDespatchFrom)
        Me.Frame1.Controls.Add(Me.chkExWork)
        Me.Frame1.Controls.Add(Me.txtModvatDate)
        Me.Frame1.Controls.Add(Me.Frame10)
        Me.Frame1.Controls.Add(Me.txtAbatementPer)
        Me.Frame1.Controls.Add(Me.chkTaxOnMRP)
        Me.Frame1.Controls.Add(Me.txtShippedTo)
        Me.Frame1.Controls.Add(Me.cmdSearchShippedTo)
        Me.Frame1.Controls.Add(Me.chkShipTo)
        Me.Frame1.Controls.Add(Me.Frame5)
        Me.Frame1.Controls.Add(Me.chkAgtPermission)
        Me.Frame1.Controls.Add(Me.Frame4)
        Me.Frame1.Controls.Add(Me.Frame2)
        Me.Frame1.Controls.Add(Me.txtModvatNo)
        Me.Frame1.Controls.Add(Me.Frame8)
        Me.Frame1.Controls.Add(Me.Frame7)
        Me.Frame1.Controls.Add(Me.txtTariff)
        Me.Frame1.Controls.Add(Me.chkChallanMade)
        Me.Frame1.Controls.Add(Me.chkPackmat)
        Me.Frame1.Controls.Add(Me.txtNarration)
        Me.Frame1.Controls.Add(Me.txtDocsThru)
        Me.Frame1.Controls.Add(Me.txtRemarks)
        Me.Frame1.Controls.Add(Me.txtItemType)
        Me.Frame1.Controls.Add(Me.Label50)
        Me.Frame1.Controls.Add(Me.Label60)
        Me.Frame1.Controls.Add(Me.Label56)
        Me.Frame1.Controls.Add(Me.Label4)
        Me.Frame1.Controls.Add(Me.Label61)
        Me.Frame1.Controls.Add(Me.Label9)
        Me.Frame1.Controls.Add(Me.Label32)
        Me.Frame1.Controls.Add(Me.Label31)
        Me.Frame1.Controls.Add(Me.Label26)
        Me.Frame1.Controls.Add(Me.Label11)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(2, -2)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(902, 398)
        Me.Frame1.TabIndex = 80
        Me.Frame1.TabStop = False
        '
        'txtStoreDetail
        '
        Me.txtStoreDetail.AcceptsReturn = True
        Me.txtStoreDetail.BackColor = System.Drawing.SystemColors.Window
        Me.txtStoreDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStoreDetail.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtStoreDetail.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStoreDetail.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtStoreDetail.Location = New System.Drawing.Point(479, 188)
        Me.txtStoreDetail.MaxLength = 0
        Me.txtStoreDetail.Name = "txtStoreDetail"
        Me.txtStoreDetail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtStoreDetail.Size = New System.Drawing.Size(317, 20)
        Me.txtStoreDetail.TabIndex = 257
        '
        'txtApplicant
        '
        Me.txtApplicant.AcceptsReturn = True
        Me.txtApplicant.BackColor = System.Drawing.SystemColors.Window
        Me.txtApplicant.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtApplicant.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtApplicant.Enabled = False
        Me.txtApplicant.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtApplicant.ForeColor = System.Drawing.Color.Blue
        Me.txtApplicant.Location = New System.Drawing.Point(479, 214)
        Me.txtApplicant.MaxLength = 0
        Me.txtApplicant.Name = "txtApplicant"
        Me.txtApplicant.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtApplicant.Size = New System.Drawing.Size(317, 20)
        Me.txtApplicant.TabIndex = 255
        '
        'Label71
        '
        Me.Label71.AutoSize = True
        Me.Label71.BackColor = System.Drawing.SystemColors.Control
        Me.Label71.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label71.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label71.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label71.Location = New System.Drawing.Point(422, 191)
        Me.Label71.Name = "Label71"
        Me.Label71.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label71.Size = New System.Drawing.Size(39, 14)
        Me.Label71.TabIndex = 258
        Me.Label71.Text = "Store :"
        Me.Label71.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label72
        '
        Me.Label72.AutoSize = True
        Me.Label72.BackColor = System.Drawing.Color.Transparent
        Me.Label72.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label72.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label72.ForeColor = System.Drawing.Color.Black
        Me.Label72.Location = New System.Drawing.Point(415, 216)
        Me.Label72.Name = "Label72"
        Me.Label72.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label72.Size = New System.Drawing.Size(58, 14)
        Me.Label72.TabIndex = 256
        Me.Label72.Text = "Applicant :"
        Me.Label72.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label72.Visible = False
        '
        'lblModDate
        '
        Me.lblModDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblModDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblModDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblModDate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblModDate.Location = New System.Drawing.Point(283, 360)
        Me.lblModDate.Name = "lblModDate"
        Me.lblModDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblModDate.Size = New System.Drawing.Size(69, 19)
        Me.lblModDate.TabIndex = 254
        '
        'Label66
        '
        Me.Label66.AutoSize = True
        Me.Label66.BackColor = System.Drawing.SystemColors.Control
        Me.Label66.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label66.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label66.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label66.Location = New System.Drawing.Point(217, 362)
        Me.Label66.Name = "Label66"
        Me.Label66.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label66.Size = New System.Drawing.Size(62, 15)
        Me.Label66.TabIndex = 253
        Me.Label66.Text = "Mod Date:"
        Me.Label66.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblAddDate
        '
        Me.lblAddDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblAddDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAddDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAddDate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAddDate.Location = New System.Drawing.Point(283, 342)
        Me.lblAddDate.Name = "lblAddDate"
        Me.lblAddDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAddDate.Size = New System.Drawing.Size(69, 19)
        Me.lblAddDate.TabIndex = 252
        '
        'Label67
        '
        Me.Label67.AutoSize = True
        Me.Label67.BackColor = System.Drawing.SystemColors.Control
        Me.Label67.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label67.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label67.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label67.Location = New System.Drawing.Point(216, 344)
        Me.Label67.Name = "Label67"
        Me.Label67.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label67.Size = New System.Drawing.Size(63, 15)
        Me.Label67.TabIndex = 251
        Me.Label67.Text = "Add Date :"
        Me.Label67.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblModUser
        '
        Me.lblModUser.BackColor = System.Drawing.SystemColors.Control
        Me.lblModUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblModUser.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblModUser.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModUser.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblModUser.Location = New System.Drawing.Point(87, 360)
        Me.lblModUser.Name = "lblModUser"
        Me.lblModUser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblModUser.Size = New System.Drawing.Size(69, 19)
        Me.lblModUser.TabIndex = 250
        '
        'Label68
        '
        Me.Label68.AutoSize = True
        Me.Label68.BackColor = System.Drawing.SystemColors.Control
        Me.Label68.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label68.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label68.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label68.Location = New System.Drawing.Point(20, 362)
        Me.Label68.Name = "Label68"
        Me.Label68.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label68.Size = New System.Drawing.Size(63, 15)
        Me.Label68.TabIndex = 249
        Me.Label68.Text = "Mod User:"
        Me.Label68.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblAddUser
        '
        Me.lblAddUser.BackColor = System.Drawing.SystemColors.Control
        Me.lblAddUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAddUser.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAddUser.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddUser.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAddUser.Location = New System.Drawing.Point(87, 342)
        Me.lblAddUser.Name = "lblAddUser"
        Me.lblAddUser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAddUser.Size = New System.Drawing.Size(69, 19)
        Me.lblAddUser.TabIndex = 248
        '
        'Label69
        '
        Me.Label69.AutoSize = True
        Me.Label69.BackColor = System.Drawing.SystemColors.Control
        Me.Label69.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label69.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label69.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label69.Location = New System.Drawing.Point(21, 344)
        Me.Label69.Name = "Label69"
        Me.Label69.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label69.Size = New System.Drawing.Size(64, 15)
        Me.Label69.TabIndex = 247
        Me.Label69.Text = "Add User :"
        Me.Label69.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtPacking
        '
        Me.txtPacking.AcceptsReturn = True
        Me.txtPacking.BackColor = System.Drawing.SystemColors.Window
        Me.txtPacking.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPacking.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPacking.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPacking.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPacking.Location = New System.Drawing.Point(78, 157)
        Me.txtPacking.MaxLength = 0
        Me.txtPacking.Name = "txtPacking"
        Me.txtPacking.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPacking.Size = New System.Drawing.Size(267, 20)
        Me.txtPacking.TabIndex = 27
        '
        'Label65
        '
        Me.Label65.AutoSize = True
        Me.Label65.BackColor = System.Drawing.SystemColors.Control
        Me.Label65.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label65.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label65.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label65.Location = New System.Drawing.Point(-4, 159)
        Me.Label65.Name = "Label65"
        Me.Label65.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label65.Size = New System.Drawing.Size(85, 14)
        Me.Label65.TabIndex = 243
        Me.Label65.Text = "Packing Details :"
        Me.Label65.TextAlign = System.Drawing.ContentAlignment.TopRight
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
        Me.TxtShipTo.Location = New System.Drawing.Point(78, 282)
        Me.TxtShipTo.MaxLength = 0
        Me.TxtShipTo.Name = "TxtShipTo"
        Me.TxtShipTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtShipTo.Size = New System.Drawing.Size(100, 22)
        Me.TxtShipTo.TabIndex = 240
        '
        'Label63
        '
        Me.Label63.AutoSize = True
        Me.Label63.BackColor = System.Drawing.SystemColors.Control
        Me.Label63.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label63.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label63.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label63.Location = New System.Drawing.Point(8, 286)
        Me.Label63.Name = "Label63"
        Me.Label63.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label63.Size = New System.Drawing.Size(56, 13)
        Me.Label63.TabIndex = 241
        Me.Label63.Text = "Location :"
        Me.Label63.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtShippedFrom
        '
        Me.txtShippedFrom.AcceptsReturn = True
        Me.txtShippedFrom.BackColor = System.Drawing.SystemColors.Window
        Me.txtShippedFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtShippedFrom.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtShippedFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShippedFrom.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtShippedFrom.Location = New System.Drawing.Point(78, 211)
        Me.txtShippedFrom.MaxLength = 0
        Me.txtShippedFrom.Name = "txtShippedFrom"
        Me.txtShippedFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtShippedFrom.Size = New System.Drawing.Size(245, 20)
        Me.txtShippedFrom.TabIndex = 237
        '
        'chkDespatchFrom
        '
        Me.chkDespatchFrom.AutoSize = True
        Me.chkDespatchFrom.BackColor = System.Drawing.SystemColors.Control
        Me.chkDespatchFrom.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkDespatchFrom.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDespatchFrom.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkDespatchFrom.Location = New System.Drawing.Point(76, 190)
        Me.chkDespatchFrom.Name = "chkDespatchFrom"
        Me.chkDespatchFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkDespatchFrom.Size = New System.Drawing.Size(220, 18)
        Me.chkDespatchFrom.TabIndex = 27
        Me.chkDespatchFrom.Text = "'Despatch From' Other Than Bill Address"
        Me.chkDespatchFrom.UseVisualStyleBackColor = False
        '
        'chkExWork
        '
        Me.chkExWork.AutoSize = True
        Me.chkExWork.BackColor = System.Drawing.SystemColors.Control
        Me.chkExWork.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkExWork.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkExWork.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkExWork.Location = New System.Drawing.Point(78, 314)
        Me.chkExWork.Name = "chkExWork"
        Me.chkExWork.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkExWork.Size = New System.Drawing.Size(178, 18)
        Me.chkExWork.TabIndex = 31
        Me.chkExWork.Text = "'Shipped To' Ex Work (Yes / No)"
        Me.chkExWork.UseVisualStyleBackColor = False
        '
        'txtModvatDate
        '
        Me.txtModvatDate.AcceptsReturn = True
        Me.txtModvatDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtModvatDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModvatDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtModvatDate.Enabled = False
        Me.txtModvatDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtModvatDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtModvatDate.Location = New System.Drawing.Point(512, 284)
        Me.txtModvatDate.MaxLength = 0
        Me.txtModvatDate.Name = "txtModvatDate"
        Me.txtModvatDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtModvatDate.Size = New System.Drawing.Size(71, 20)
        Me.txtModvatDate.TabIndex = 139
        Me.txtModvatDate.Visible = False
        '
        'Frame10
        '
        Me.Frame10.BackColor = System.Drawing.SystemColors.Control
        Me.Frame10.Controls.Add(Me.txteRefNo)
        Me.Frame10.Controls.Add(Me.Label6)
        Me.Frame10.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame10.Location = New System.Drawing.Point(648, 342)
        Me.Frame10.Name = "Frame10"
        Me.Frame10.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame10.Size = New System.Drawing.Size(255, 43)
        Me.Frame10.TabIndex = 181
        Me.Frame10.TabStop = False
        Me.Frame10.Text = "Electronic Reference Number"
        Me.Frame10.Visible = False
        '
        'txteRefNo
        '
        Me.txteRefNo.AcceptsReturn = True
        Me.txteRefNo.BackColor = System.Drawing.SystemColors.Window
        Me.txteRefNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txteRefNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txteRefNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txteRefNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txteRefNo.Location = New System.Drawing.Point(120, 20)
        Me.txteRefNo.MaxLength = 0
        Me.txteRefNo.Name = "txteRefNo"
        Me.txteRefNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txteRefNo.Size = New System.Drawing.Size(129, 20)
        Me.txteRefNo.TabIndex = 179
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(5, 22)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(96, 14)
        Me.Label6.TabIndex = 182
        Me.Label6.Text = "Electronic Ref No :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtAbatementPer
        '
        Me.txtAbatementPer.AcceptsReturn = True
        Me.txtAbatementPer.BackColor = System.Drawing.SystemColors.Window
        Me.txtAbatementPer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAbatementPer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAbatementPer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAbatementPer.ForeColor = System.Drawing.Color.Blue
        Me.txtAbatementPer.Location = New System.Drawing.Point(593, 312)
        Me.txtAbatementPer.MaxLength = 0
        Me.txtAbatementPer.Name = "txtAbatementPer"
        Me.txtAbatementPer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAbatementPer.Size = New System.Drawing.Size(31, 20)
        Me.txtAbatementPer.TabIndex = 235
        Me.txtAbatementPer.Visible = False
        '
        'chkTaxOnMRP
        '
        Me.chkTaxOnMRP.AutoSize = True
        Me.chkTaxOnMRP.BackColor = System.Drawing.SystemColors.Control
        Me.chkTaxOnMRP.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkTaxOnMRP.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTaxOnMRP.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkTaxOnMRP.Location = New System.Drawing.Point(426, 312)
        Me.chkTaxOnMRP.Name = "chkTaxOnMRP"
        Me.chkTaxOnMRP.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkTaxOnMRP.Size = New System.Drawing.Size(84, 18)
        Me.chkTaxOnMRP.TabIndex = 234
        Me.chkTaxOnMRP.Text = "Tax On MRP"
        Me.chkTaxOnMRP.UseVisualStyleBackColor = False
        Me.chkTaxOnMRP.Visible = False
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
        Me.txtShippedTo.Location = New System.Drawing.Point(78, 254)
        Me.txtShippedTo.MaxLength = 0
        Me.txtShippedTo.Name = "txtShippedTo"
        Me.txtShippedTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtShippedTo.Size = New System.Drawing.Size(245, 20)
        Me.txtShippedTo.TabIndex = 29
        '
        'chkShipTo
        '
        Me.chkShipTo.AutoSize = True
        Me.chkShipTo.BackColor = System.Drawing.SystemColors.Control
        Me.chkShipTo.Checked = True
        Me.chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkShipTo.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkShipTo.Enabled = False
        Me.chkShipTo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShipTo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkShipTo.Location = New System.Drawing.Point(76, 233)
        Me.chkShipTo.Name = "chkShipTo"
        Me.chkShipTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkShipTo.Size = New System.Drawing.Size(226, 18)
        Me.chkShipTo.TabIndex = 28
        Me.chkShipTo.Text = "'Shipped To' Same as 'Billed To' (Yes / No)"
        Me.chkShipTo.UseVisualStyleBackColor = False
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me.txtServProvided)
        Me.Frame5.Controls.Add(Me.txtProcessNature)
        Me.Frame5.Controls.Add(Me.Label53)
        Me.Frame5.Controls.Add(Me.Label55)
        Me.Frame5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(421, 333)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(221, 61)
        Me.Frame5.TabIndex = 174
        Me.Frame5.TabStop = False
        Me.Frame5.Text = "Service Details"
        Me.Frame5.Visible = False
        '
        'txtServProvided
        '
        Me.txtServProvided.AcceptsReturn = True
        Me.txtServProvided.BackColor = System.Drawing.SystemColors.Window
        Me.txtServProvided.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtServProvided.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtServProvided.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtServProvided.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtServProvided.Location = New System.Drawing.Point(108, 14)
        Me.txtServProvided.MaxLength = 0
        Me.txtServProvided.Name = "txtServProvided"
        Me.txtServProvided.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtServProvided.Size = New System.Drawing.Size(90, 20)
        Me.txtServProvided.TabIndex = 176
        '
        'txtProcessNature
        '
        Me.txtProcessNature.AcceptsReturn = True
        Me.txtProcessNature.BackColor = System.Drawing.SystemColors.Window
        Me.txtProcessNature.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtProcessNature.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtProcessNature.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProcessNature.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtProcessNature.Location = New System.Drawing.Point(108, 35)
        Me.txtProcessNature.MaxLength = 0
        Me.txtProcessNature.Name = "txtProcessNature"
        Me.txtProcessNature.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProcessNature.Size = New System.Drawing.Size(90, 20)
        Me.txtProcessNature.TabIndex = 175
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.BackColor = System.Drawing.SystemColors.Control
        Me.Label53.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label53.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label53.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label53.Location = New System.Drawing.Point(14, 16)
        Me.Label53.Name = "Label53"
        Me.Label53.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label53.Size = New System.Drawing.Size(83, 14)
        Me.Label53.TabIndex = 178
        Me.Label53.Text = "Serv. Provided :"
        Me.Label53.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label55
        '
        Me.Label55.AutoSize = True
        Me.Label55.BackColor = System.Drawing.SystemColors.Control
        Me.Label55.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label55.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label55.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label55.Location = New System.Drawing.Point(10, 37)
        Me.Label55.Name = "Label55"
        Me.Label55.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label55.Size = New System.Drawing.Size(88, 14)
        Me.Label55.TabIndex = 177
        Me.Label55.Text = "Process Nature :"
        Me.Label55.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'chkAgtPermission
        '
        Me.chkAgtPermission.AutoSize = True
        Me.chkAgtPermission.BackColor = System.Drawing.SystemColors.Control
        Me.chkAgtPermission.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAgtPermission.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAgtPermission.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAgtPermission.Location = New System.Drawing.Point(748, 312)
        Me.chkAgtPermission.Name = "chkAgtPermission"
        Me.chkAgtPermission.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAgtPermission.Size = New System.Drawing.Size(98, 18)
        Me.chkAgtPermission.TabIndex = 173
        Me.chkAgtPermission.Text = "Agt Permission"
        Me.chkAgtPermission.UseVisualStyleBackColor = False
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.txtPOAmendNo)
        Me.Frame4.Controls.Add(Me.txtPOWEFDate)
        Me.Frame4.Controls.Add(Me.txtSuppFromDate)
        Me.Frame4.Controls.Add(Me.txtSuppToDate)
        Me.Frame4.Controls.Add(Me.txtIntRate)
        Me.Frame4.Controls.Add(Me.Label41)
        Me.Frame4.Controls.Add(Me.Label39)
        Me.Frame4.Controls.Add(Me.Label43)
        Me.Frame4.Controls.Add(Me.Label44)
        Me.Frame4.Controls.Add(Me.Label45)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(426, 117)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(474, 63)
        Me.Frame4.TabIndex = 164
        Me.Frame4.TabStop = False
        Me.Frame4.Text = "Supplimentary Details"
        '
        'txtPOWEFDate
        '
        Me.txtPOWEFDate.AcceptsReturn = True
        Me.txtPOWEFDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtPOWEFDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPOWEFDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPOWEFDate.Enabled = False
        Me.txtPOWEFDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPOWEFDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPOWEFDate.Location = New System.Drawing.Point(259, 16)
        Me.txtPOWEFDate.MaxLength = 0
        Me.txtPOWEFDate.Name = "txtPOWEFDate"
        Me.txtPOWEFDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPOWEFDate.Size = New System.Drawing.Size(77, 20)
        Me.txtPOWEFDate.TabIndex = 166
        '
        'txtSuppFromDate
        '
        Me.txtSuppFromDate.AcceptsReturn = True
        Me.txtSuppFromDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtSuppFromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSuppFromDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSuppFromDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSuppFromDate.ForeColor = System.Drawing.Color.Blue
        Me.txtSuppFromDate.Location = New System.Drawing.Point(95, 38)
        Me.txtSuppFromDate.MaxLength = 0
        Me.txtSuppFromDate.Name = "txtSuppFromDate"
        Me.txtSuppFromDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSuppFromDate.Size = New System.Drawing.Size(69, 20)
        Me.txtSuppFromDate.TabIndex = 167
        '
        'txtSuppToDate
        '
        Me.txtSuppToDate.AcceptsReturn = True
        Me.txtSuppToDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtSuppToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSuppToDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSuppToDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSuppToDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSuppToDate.Location = New System.Drawing.Point(259, 38)
        Me.txtSuppToDate.MaxLength = 0
        Me.txtSuppToDate.Name = "txtSuppToDate"
        Me.txtSuppToDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSuppToDate.Size = New System.Drawing.Size(69, 20)
        Me.txtSuppToDate.TabIndex = 168
        '
        'txtIntRate
        '
        Me.txtIntRate.AcceptsReturn = True
        Me.txtIntRate.BackColor = System.Drawing.SystemColors.Window
        Me.txtIntRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIntRate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIntRate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIntRate.ForeColor = System.Drawing.Color.Blue
        Me.txtIntRate.Location = New System.Drawing.Point(416, 38)
        Me.txtIntRate.MaxLength = 0
        Me.txtIntRate.Name = "txtIntRate"
        Me.txtIntRate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIntRate.Size = New System.Drawing.Size(53, 20)
        Me.txtIntRate.TabIndex = 169
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.BackColor = System.Drawing.SystemColors.Control
        Me.Label43.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label43.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label43.Location = New System.Drawing.Point(23, 40)
        Me.Label43.Name = "Label43"
        Me.Label43.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label43.Size = New System.Drawing.Size(68, 14)
        Me.Label43.TabIndex = 172
        Me.Label43.Text = "Supp. From :"
        Me.Label43.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.BackColor = System.Drawing.SystemColors.Control
        Me.Label44.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label44.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label44.Location = New System.Drawing.Point(346, 40)
        Me.Label44.Name = "Label44"
        Me.Label44.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label44.Size = New System.Drawing.Size(67, 14)
        Me.Label44.TabIndex = 171
        Me.Label44.Text = "Interest(%) :"
        Me.Label44.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.BackColor = System.Drawing.SystemColors.Control
        Me.Label45.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label45.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label45.Location = New System.Drawing.Point(231, 40)
        Me.Label45.Name = "Label45"
        Me.Label45.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label45.Size = New System.Drawing.Size(24, 14)
        Me.Label45.TabIndex = 170
        Me.Label45.Text = "To :"
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.txtDNDate)
        Me.Frame2.Controls.Add(Me.txtDNNo)
        Me.Frame2.Controls.Add(Me.Label37)
        Me.Frame2.Controls.Add(Me.lblDNAmount)
        Me.Frame2.Controls.Add(Me.Label40)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(426, 60)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(387, 45)
        Me.Frame2.TabIndex = 158
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Debit Note Details"
        '
        'txtDNDate
        '
        Me.txtDNDate.AcceptsReturn = True
        Me.txtDNDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtDNDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDNDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDNDate.Enabled = False
        Me.txtDNDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDNDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDNDate.Location = New System.Drawing.Point(174, 18)
        Me.txtDNDate.MaxLength = 0
        Me.txtDNDate.Name = "txtDNDate"
        Me.txtDNDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDNDate.Size = New System.Drawing.Size(77, 20)
        Me.txtDNDate.TabIndex = 160
        '
        'lblDNAmount
        '
        Me.lblDNAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblDNAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDNAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDNAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDNAmount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDNAmount.Location = New System.Drawing.Point(306, 18)
        Me.lblDNAmount.Name = "lblDNAmount"
        Me.lblDNAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDNAmount.Size = New System.Drawing.Size(77, 19)
        Me.lblDNAmount.TabIndex = 162
        Me.lblDNAmount.Text = "lblDNAmount"
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.BackColor = System.Drawing.SystemColors.Control
        Me.Label40.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label40.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label40.Location = New System.Drawing.Point(254, 20)
        Me.Label40.Name = "Label40"
        Me.Label40.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label40.Size = New System.Drawing.Size(50, 14)
        Me.Label40.TabIndex = 161
        Me.Label40.Text = "Amount :"
        '
        'txtModvatNo
        '
        Me.txtModvatNo.AcceptsReturn = True
        Me.txtModvatNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtModvatNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModvatNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtModvatNo.Enabled = False
        Me.txtModvatNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtModvatNo.ForeColor = System.Drawing.Color.Blue
        Me.txtModvatNo.Location = New System.Drawing.Point(512, 256)
        Me.txtModvatNo.MaxLength = 0
        Me.txtModvatNo.Name = "txtModvatNo"
        Me.txtModvatNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtModvatNo.Size = New System.Drawing.Size(71, 20)
        Me.txtModvatNo.TabIndex = 140
        Me.txtModvatNo.Visible = False
        '
        'Frame8
        '
        Me.Frame8.BackColor = System.Drawing.SystemColors.Control
        Me.Frame8.Controls.Add(Me._OptFreight_0)
        Me.Frame8.Controls.Add(Me._OptFreight_1)
        Me.Frame8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame8.Location = New System.Drawing.Point(426, 8)
        Me.Frame8.Name = "Frame8"
        Me.Frame8.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame8.Size = New System.Drawing.Size(195, 41)
        Me.Frame8.TabIndex = 109
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
        Me._OptFreight_0.TabIndex = 32
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
        Me._OptFreight_1.TabIndex = 33
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
        Me.Frame7.Location = New System.Drawing.Point(622, 8)
        Me.Frame7.Name = "Frame7"
        Me.Frame7.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame7.Size = New System.Drawing.Size(191, 41)
        Me.Frame7.TabIndex = 106
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
        Me._txtCreditDays_0.TabIndex = 34
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
        Me._txtCreditDays_1.TabIndex = 35
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
        Me.Label33.TabIndex = 108
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
        Me.Label35.TabIndex = 107
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
        Me.txtTariff.Location = New System.Drawing.Point(78, 12)
        Me.txtTariff.MaxLength = 0
        Me.txtTariff.Name = "txtTariff"
        Me.txtTariff.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTariff.Size = New System.Drawing.Size(267, 20)
        Me.txtTariff.TabIndex = 22
        '
        'chkChallanMade
        '
        Me.chkChallanMade.AutoSize = True
        Me.chkChallanMade.BackColor = System.Drawing.SystemColors.Control
        Me.chkChallanMade.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkChallanMade.Enabled = False
        Me.chkChallanMade.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkChallanMade.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkChallanMade.Location = New System.Drawing.Point(748, 284)
        Me.chkChallanMade.Name = "chkChallanMade"
        Me.chkChallanMade.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkChallanMade.Size = New System.Drawing.Size(90, 18)
        Me.chkChallanMade.TabIndex = 37
        Me.chkChallanMade.Text = "Challan Made"
        Me.chkChallanMade.UseVisualStyleBackColor = False
        '
        'chkPackmat
        '
        Me.chkPackmat.AutoSize = True
        Me.chkPackmat.BackColor = System.Drawing.SystemColors.Control
        Me.chkPackmat.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPackmat.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPackmat.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPackmat.Location = New System.Drawing.Point(748, 256)
        Me.chkPackmat.Name = "chkPackmat"
        Me.chkPackmat.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPackmat.Size = New System.Drawing.Size(103, 18)
        Me.chkPackmat.TabIndex = 36
        Me.chkPackmat.Text = "Packing Material"
        Me.chkPackmat.UseVisualStyleBackColor = False
        '
        'txtNarration
        '
        Me.txtNarration.AcceptsReturn = True
        Me.txtNarration.BackColor = System.Drawing.SystemColors.Window
        Me.txtNarration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNarration.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNarration.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNarration.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNarration.Location = New System.Drawing.Point(78, 134)
        Me.txtNarration.MaxLength = 0
        Me.txtNarration.Name = "txtNarration"
        Me.txtNarration.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNarration.Size = New System.Drawing.Size(267, 20)
        Me.txtNarration.TabIndex = 26
        '
        'txtDocsThru
        '
        Me.txtDocsThru.AcceptsReturn = True
        Me.txtDocsThru.BackColor = System.Drawing.SystemColors.Window
        Me.txtDocsThru.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDocsThru.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDocsThru.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDocsThru.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDocsThru.Location = New System.Drawing.Point(78, 56)
        Me.txtDocsThru.MaxLength = 0
        Me.txtDocsThru.Name = "txtDocsThru"
        Me.txtDocsThru.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDocsThru.Size = New System.Drawing.Size(267, 20)
        Me.txtDocsThru.TabIndex = 24
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRemarks.Location = New System.Drawing.Point(78, 78)
        Me.txtRemarks.MaxLength = 0
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.Size = New System.Drawing.Size(267, 53)
        Me.txtRemarks.TabIndex = 25
        '
        'txtItemType
        '
        Me.txtItemType.AcceptsReturn = True
        Me.txtItemType.BackColor = System.Drawing.SystemColors.Window
        Me.txtItemType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemType.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItemType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtItemType.Location = New System.Drawing.Point(78, 34)
        Me.txtItemType.MaxLength = 0
        Me.txtItemType.Name = "txtItemType"
        Me.txtItemType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemType.Size = New System.Drawing.Size(267, 20)
        Me.txtItemType.TabIndex = 23
        '
        'Label50
        '
        Me.Label50.AutoSize = True
        Me.Label50.BackColor = System.Drawing.SystemColors.Control
        Me.Label50.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label50.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label50.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label50.Location = New System.Drawing.Point(15, 214)
        Me.Label50.Name = "Label50"
        Me.Label50.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label50.Size = New System.Drawing.Size(59, 14)
        Me.Label50.TabIndex = 238
        Me.Label50.Text = "Despatch :"
        Me.Label50.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label60
        '
        Me.Label60.AutoSize = True
        Me.Label60.BackColor = System.Drawing.SystemColors.Control
        Me.Label60.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label60.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label60.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label60.Location = New System.Drawing.Point(426, 287)
        Me.Label60.Name = "Label60"
        Me.Label60.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label60.Size = New System.Drawing.Size(73, 14)
        Me.Label60.TabIndex = 141
        Me.Label60.Text = "Modvat Date :"
        Me.Label60.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label60.Visible = False
        '
        'Label56
        '
        Me.Label56.AutoSize = True
        Me.Label56.BackColor = System.Drawing.SystemColors.Control
        Me.Label56.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label56.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label56.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label56.Location = New System.Drawing.Point(508, 312)
        Me.Label56.Name = "Label56"
        Me.Label56.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label56.Size = New System.Drawing.Size(78, 14)
        Me.Label56.TabIndex = 236
        Me.Label56.Text = "Abatement % :"
        Me.Label56.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label56.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(8, 256)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(66, 14)
        Me.Label4.TabIndex = 180
        Me.Label4.Text = "Shipped To :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label4.Visible = False
        '
        'Label61
        '
        Me.Label61.AutoSize = True
        Me.Label61.BackColor = System.Drawing.SystemColors.Control
        Me.Label61.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label61.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label61.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label61.Location = New System.Drawing.Point(426, 258)
        Me.Label61.Name = "Label61"
        Me.Label61.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label61.Size = New System.Drawing.Size(64, 14)
        Me.Label61.TabIndex = 142
        Me.Label61.Text = "Modvat No :"
        Me.Label61.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label61.Visible = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(36, 14)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(38, 14)
        Me.Label9.TabIndex = 105
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
        Me.Label32.Location = New System.Drawing.Point(17, 137)
        Me.Label32.Name = "Label32"
        Me.Label32.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label32.Size = New System.Drawing.Size(57, 14)
        Me.Label32.TabIndex = 84
        Me.Label32.Text = "Narration :"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.BackColor = System.Drawing.SystemColors.Control
        Me.Label31.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label31.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label31.Location = New System.Drawing.Point(11, 54)
        Me.Label31.Name = "Label31"
        Me.Label31.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label31.Size = New System.Drawing.Size(63, 14)
        Me.Label31.TabIndex = 83
        Me.Label31.Text = "Docs Thru :"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.SystemColors.Control
        Me.Label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(19, 81)
        Me.Label26.Name = "Label26"
        Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label26.Size = New System.Drawing.Size(55, 14)
        Me.Label26.TabIndex = 82
        Me.Label26.Text = "Remarks :"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(16, 34)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(58, 14)
        Me.Label11.TabIndex = 81
        Me.Label11.Text = "Item Type :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_TabMain_TabPage2
        '
        Me._TabMain_TabPage2.Controls.Add(Me.Frame9)
        Me._TabMain_TabPage2.Location = New System.Drawing.Point(4, 22)
        Me._TabMain_TabPage2.Name = "_TabMain_TabPage2"
        Me._TabMain_TabPage2.Size = New System.Drawing.Size(904, 388)
        Me._TabMain_TabPage2.TabIndex = 2
        Me._TabMain_TabPage2.Text = "Export Detail"
        '
        'Frame9
        '
        Me.Frame9.BackColor = System.Drawing.SystemColors.Control
        Me.Frame9.Controls.Add(Me.txtPortCode)
        Me.Frame9.Controls.Add(Me.chkPrintByGroup)
        Me.Frame9.Controls.Add(Me.txtTextDesc)
        Me.Frame9.Controls.Add(Me.chkPrintTextDesc)
        Me.Frame9.Controls.Add(Me.chkDutyFreePurchase)
        Me.Frame9.Controls.Add(Me.chkJWDetail)
        Me.Frame9.Controls.Add(Me.cmdCoBuyerSearch)
        Me.Frame9.Controls.Add(Me.txtCoBuyerName)
        Me.Frame9.Controls.Add(Me.ChkPaintPrint)
        Me.Frame9.Controls.Add(Me.chkPrintType)
        Me.Frame9.Controls.Add(Me.chkStockTrf)
        Me.Frame9.Controls.Add(Me.txtBuyerName)
        Me.Frame9.Controls.Add(Me.cmdBuyerSearch)
        Me.Frame9.Controls.Add(Me.txtLocation)
        Me.Frame9.Controls.Add(Me.txtAdvLicense)
        Me.Frame9.Controls.Add(Me.txtTotalEuro)
        Me.Frame9.Controls.Add(Me.txtExchangeRate)
        Me.Frame9.Controls.Add(Me.txtExportBillNo)
        Me.Frame9.Controls.Add(Me.txtExportBillDate)
        Me.Frame9.Controls.Add(Me.txtARE1No)
        Me.Frame9.Controls.Add(Me.txtShippingNo)
        Me.Frame9.Controls.Add(Me.txtARE1Date)
        Me.Frame9.Controls.Add(Me.txtShippingDate)
        Me.Frame9.Controls.Add(Me._Label_10)
        Me.Frame9.Controls.Add(Me._Label_33)
        Me.Frame9.Controls.Add(Me._Label_8)
        Me.Frame9.Controls.Add(Me._Label_7)
        Me.Frame9.Controls.Add(Me._Label_6)
        Me.Frame9.Controls.Add(Me._Label_5)
        Me.Frame9.Controls.Add(Me._Label_4)
        Me.Frame9.Controls.Add(Me._Label_3)
        Me.Frame9.Controls.Add(Me.lblTotExportExp)
        Me.Frame9.Controls.Add(Me._Label_2)
        Me.Frame9.Controls.Add(Me.Label48)
        Me.Frame9.Controls.Add(Me.Label47)
        Me.Frame9.Controls.Add(Me._Label_1)
        Me.Frame9.Controls.Add(Me._Label_0)
        Me.Frame9.Controls.Add(Me.Label59)
        Me.Frame9.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame9.Location = New System.Drawing.Point(0, -2)
        Me.Frame9.Name = "Frame9"
        Me.Frame9.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame9.Size = New System.Drawing.Size(904, 400)
        Me.Frame9.TabIndex = 119
        Me.Frame9.TabStop = False
        '
        'txtPortCode
        '
        Me.txtPortCode.AcceptsReturn = True
        Me.txtPortCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtPortCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPortCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPortCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPortCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPortCode.Location = New System.Drawing.Point(112, 68)
        Me.txtPortCode.MaxLength = 0
        Me.txtPortCode.Name = "txtPortCode"
        Me.txtPortCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPortCode.Size = New System.Drawing.Size(115, 20)
        Me.txtPortCode.TabIndex = 42
        '
        'chkPrintByGroup
        '
        Me.chkPrintByGroup.AutoSize = True
        Me.chkPrintByGroup.BackColor = System.Drawing.SystemColors.Control
        Me.chkPrintByGroup.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPrintByGroup.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPrintByGroup.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPrintByGroup.Location = New System.Drawing.Point(552, 12)
        Me.chkPrintByGroup.Name = "chkPrintByGroup"
        Me.chkPrintByGroup.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPrintByGroup.Size = New System.Drawing.Size(118, 18)
        Me.chkPrintByGroup.TabIndex = 153
        Me.chkPrintByGroup.Text = "Print By Item Group"
        Me.chkPrintByGroup.UseVisualStyleBackColor = False
        '
        'txtTextDesc
        '
        Me.txtTextDesc.AcceptsReturn = True
        Me.txtTextDesc.BackColor = System.Drawing.SystemColors.Window
        Me.txtTextDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTextDesc.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTextDesc.Enabled = False
        Me.txtTextDesc.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTextDesc.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTextDesc.Location = New System.Drawing.Point(452, 208)
        Me.txtTextDesc.MaxLength = 0
        Me.txtTextDesc.Multiline = True
        Me.txtTextDesc.Name = "txtTextDesc"
        Me.txtTextDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTextDesc.Size = New System.Drawing.Size(283, 43)
        Me.txtTextDesc.TabIndex = 151
        Me.txtTextDesc.Visible = False
        '
        'chkPrintTextDesc
        '
        Me.chkPrintTextDesc.AutoSize = True
        Me.chkPrintTextDesc.BackColor = System.Drawing.SystemColors.Control
        Me.chkPrintTextDesc.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPrintTextDesc.Enabled = False
        Me.chkPrintTextDesc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPrintTextDesc.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPrintTextDesc.Location = New System.Drawing.Point(552, 162)
        Me.chkPrintTextDesc.Name = "chkPrintTextDesc"
        Me.chkPrintTextDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPrintTextDesc.Size = New System.Drawing.Size(127, 18)
        Me.chkPrintTextDesc.TabIndex = 150
        Me.chkPrintTextDesc.Text = "Print Text Description"
        Me.chkPrintTextDesc.UseVisualStyleBackColor = False
        Me.chkPrintTextDesc.Visible = False
        '
        'chkDutyFreePurchase
        '
        Me.chkDutyFreePurchase.AutoSize = True
        Me.chkDutyFreePurchase.BackColor = System.Drawing.SystemColors.Control
        Me.chkDutyFreePurchase.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkDutyFreePurchase.Enabled = False
        Me.chkDutyFreePurchase.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDutyFreePurchase.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkDutyFreePurchase.Location = New System.Drawing.Point(552, 137)
        Me.chkDutyFreePurchase.Name = "chkDutyFreePurchase"
        Me.chkDutyFreePurchase.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkDutyFreePurchase.Size = New System.Drawing.Size(122, 18)
        Me.chkDutyFreePurchase.TabIndex = 148
        Me.chkDutyFreePurchase.Text = "Duty Free Purchase"
        Me.chkDutyFreePurchase.UseVisualStyleBackColor = False
        Me.chkDutyFreePurchase.Visible = False
        '
        'chkJWDetail
        '
        Me.chkJWDetail.AutoSize = True
        Me.chkJWDetail.BackColor = System.Drawing.SystemColors.Control
        Me.chkJWDetail.Checked = True
        Me.chkJWDetail.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkJWDetail.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkJWDetail.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkJWDetail.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkJWDetail.Location = New System.Drawing.Point(552, 112)
        Me.chkJWDetail.Name = "chkJWDetail"
        Me.chkJWDetail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkJWDetail.Size = New System.Drawing.Size(100, 18)
        Me.chkJWDetail.TabIndex = 147
        Me.chkJWDetail.Text = "Job Work Detail"
        Me.chkJWDetail.UseVisualStyleBackColor = False
        '
        'txtCoBuyerName
        '
        Me.txtCoBuyerName.AcceptsReturn = True
        Me.txtCoBuyerName.BackColor = System.Drawing.SystemColors.Window
        Me.txtCoBuyerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCoBuyerName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCoBuyerName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCoBuyerName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCoBuyerName.Location = New System.Drawing.Point(112, 250)
        Me.txtCoBuyerName.MaxLength = 0
        Me.txtCoBuyerName.Name = "txtCoBuyerName"
        Me.txtCoBuyerName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCoBuyerName.Size = New System.Drawing.Size(240, 20)
        Me.txtCoBuyerName.TabIndex = 51
        '
        'ChkPaintPrint
        '
        Me.ChkPaintPrint.AutoSize = True
        Me.ChkPaintPrint.BackColor = System.Drawing.SystemColors.Control
        Me.ChkPaintPrint.Checked = True
        Me.ChkPaintPrint.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChkPaintPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkPaintPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkPaintPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkPaintPrint.Location = New System.Drawing.Point(552, 87)
        Me.ChkPaintPrint.Name = "ChkPaintPrint"
        Me.ChkPaintPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkPaintPrint.Size = New System.Drawing.Size(49, 18)
        Me.ChkPaintPrint.TabIndex = 145
        Me.ChkPaintPrint.Text = "Paint"
        Me.ChkPaintPrint.UseVisualStyleBackColor = False
        '
        'chkPrintType
        '
        Me.chkPrintType.AutoSize = True
        Me.chkPrintType.BackColor = System.Drawing.SystemColors.Control
        Me.chkPrintType.Checked = True
        Me.chkPrintType.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkPrintType.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPrintType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPrintType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPrintType.Location = New System.Drawing.Point(552, 62)
        Me.chkPrintType.Name = "chkPrintType"
        Me.chkPrintType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPrintType.Size = New System.Drawing.Size(95, 18)
        Me.chkPrintType.TabIndex = 144
        Me.chkPrintType.Text = "Printed Format"
        Me.chkPrintType.UseVisualStyleBackColor = False
        '
        'chkStockTrf
        '
        Me.chkStockTrf.AutoSize = True
        Me.chkStockTrf.BackColor = System.Drawing.SystemColors.Control
        Me.chkStockTrf.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkStockTrf.Enabled = False
        Me.chkStockTrf.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkStockTrf.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkStockTrf.Location = New System.Drawing.Point(552, 37)
        Me.chkStockTrf.Name = "chkStockTrf"
        Me.chkStockTrf.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkStockTrf.Size = New System.Drawing.Size(67, 18)
        Me.chkStockTrf.TabIndex = 143
        Me.chkStockTrf.Text = "StockTrf"
        Me.chkStockTrf.UseVisualStyleBackColor = False
        Me.chkStockTrf.Visible = False
        '
        'txtBuyerName
        '
        Me.txtBuyerName.AcceptsReturn = True
        Me.txtBuyerName.BackColor = System.Drawing.SystemColors.Window
        Me.txtBuyerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBuyerName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBuyerName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBuyerName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBuyerName.Location = New System.Drawing.Point(112, 224)
        Me.txtBuyerName.MaxLength = 0
        Me.txtBuyerName.Name = "txtBuyerName"
        Me.txtBuyerName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBuyerName.Size = New System.Drawing.Size(240, 20)
        Me.txtBuyerName.TabIndex = 49
        '
        'txtLocation
        '
        Me.txtLocation.AcceptsReturn = True
        Me.txtLocation.BackColor = System.Drawing.SystemColors.Window
        Me.txtLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLocation.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLocation.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLocation.Location = New System.Drawing.Point(112, 198)
        Me.txtLocation.MaxLength = 0
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLocation.Size = New System.Drawing.Size(263, 20)
        Me.txtLocation.TabIndex = 48
        '
        'txtAdvLicense
        '
        Me.txtAdvLicense.AcceptsReturn = True
        Me.txtAdvLicense.BackColor = System.Drawing.SystemColors.Window
        Me.txtAdvLicense.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAdvLicense.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAdvLicense.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdvLicense.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAdvLicense.Location = New System.Drawing.Point(112, 172)
        Me.txtAdvLicense.MaxLength = 0
        Me.txtAdvLicense.Name = "txtAdvLicense"
        Me.txtAdvLicense.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAdvLicense.Size = New System.Drawing.Size(263, 20)
        Me.txtAdvLicense.TabIndex = 47
        '
        'txtTotalEuro
        '
        Me.txtTotalEuro.AcceptsReturn = True
        Me.txtTotalEuro.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotalEuro.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalEuro.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotalEuro.Enabled = False
        Me.txtTotalEuro.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalEuro.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotalEuro.Location = New System.Drawing.Point(112, 146)
        Me.txtTotalEuro.MaxLength = 0
        Me.txtTotalEuro.Name = "txtTotalEuro"
        Me.txtTotalEuro.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotalEuro.Size = New System.Drawing.Size(115, 20)
        Me.txtTotalEuro.TabIndex = 46
        '
        'txtExchangeRate
        '
        Me.txtExchangeRate.AcceptsReturn = True
        Me.txtExchangeRate.BackColor = System.Drawing.SystemColors.Window
        Me.txtExchangeRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtExchangeRate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtExchangeRate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExchangeRate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtExchangeRate.Location = New System.Drawing.Point(112, 120)
        Me.txtExchangeRate.MaxLength = 0
        Me.txtExchangeRate.Name = "txtExchangeRate"
        Me.txtExchangeRate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExchangeRate.Size = New System.Drawing.Size(115, 20)
        Me.txtExchangeRate.TabIndex = 45
        '
        'txtExportBillNo
        '
        Me.txtExportBillNo.AcceptsReturn = True
        Me.txtExportBillNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtExportBillNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtExportBillNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtExportBillNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExportBillNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtExportBillNo.Location = New System.Drawing.Point(112, 94)
        Me.txtExportBillNo.MaxLength = 0
        Me.txtExportBillNo.Name = "txtExportBillNo"
        Me.txtExportBillNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExportBillNo.Size = New System.Drawing.Size(115, 20)
        Me.txtExportBillNo.TabIndex = 43
        '
        'txtExportBillDate
        '
        Me.txtExportBillDate.AcceptsReturn = True
        Me.txtExportBillDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtExportBillDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtExportBillDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtExportBillDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExportBillDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtExportBillDate.Location = New System.Drawing.Point(284, 90)
        Me.txtExportBillDate.MaxLength = 0
        Me.txtExportBillDate.Name = "txtExportBillDate"
        Me.txtExportBillDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExportBillDate.Size = New System.Drawing.Size(93, 20)
        Me.txtExportBillDate.TabIndex = 44
        '
        'txtARE1No
        '
        Me.txtARE1No.AcceptsReturn = True
        Me.txtARE1No.BackColor = System.Drawing.SystemColors.Window
        Me.txtARE1No.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtARE1No.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtARE1No.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtARE1No.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtARE1No.Location = New System.Drawing.Point(112, 42)
        Me.txtARE1No.MaxLength = 0
        Me.txtARE1No.Name = "txtARE1No"
        Me.txtARE1No.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtARE1No.Size = New System.Drawing.Size(115, 20)
        Me.txtARE1No.TabIndex = 40
        '
        'txtShippingNo
        '
        Me.txtShippingNo.AcceptsReturn = True
        Me.txtShippingNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtShippingNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtShippingNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtShippingNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShippingNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtShippingNo.Location = New System.Drawing.Point(112, 16)
        Me.txtShippingNo.MaxLength = 0
        Me.txtShippingNo.Name = "txtShippingNo"
        Me.txtShippingNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtShippingNo.Size = New System.Drawing.Size(117, 20)
        Me.txtShippingNo.TabIndex = 38
        '
        'txtARE1Date
        '
        Me.txtARE1Date.AcceptsReturn = True
        Me.txtARE1Date.BackColor = System.Drawing.SystemColors.Window
        Me.txtARE1Date.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtARE1Date.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtARE1Date.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtARE1Date.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtARE1Date.Location = New System.Drawing.Point(284, 42)
        Me.txtARE1Date.MaxLength = 0
        Me.txtARE1Date.Name = "txtARE1Date"
        Me.txtARE1Date.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtARE1Date.Size = New System.Drawing.Size(93, 20)
        Me.txtARE1Date.TabIndex = 41
        '
        'txtShippingDate
        '
        Me.txtShippingDate.AcceptsReturn = True
        Me.txtShippingDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtShippingDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtShippingDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtShippingDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShippingDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtShippingDate.Location = New System.Drawing.Point(284, 18)
        Me.txtShippingDate.MaxLength = 0
        Me.txtShippingDate.Name = "txtShippingDate"
        Me.txtShippingDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtShippingDate.Size = New System.Drawing.Size(93, 20)
        Me.txtShippingDate.TabIndex = 39
        '
        '_Label_10
        '
        Me._Label_10.AutoSize = True
        Me._Label_10.BackColor = System.Drawing.SystemColors.Control
        Me._Label_10.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label_10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label_10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label.SetIndex(Me._Label_10, CType(10, Short))
        Me._Label_10.Location = New System.Drawing.Point(48, 69)
        Me._Label_10.Name = "_Label_10"
        Me._Label_10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label_10.Size = New System.Drawing.Size(60, 14)
        Me._Label_10.TabIndex = 186
        Me._Label_10.Text = "Port Code :"
        Me._Label_10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label_33
        '
        Me._Label_33.AutoSize = True
        Me._Label_33.BackColor = System.Drawing.SystemColors.Control
        Me._Label_33.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label_33.Enabled = False
        Me._Label_33.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label_33.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label.SetIndex(Me._Label_33, CType(33, Short))
        Me._Label_33.Location = New System.Drawing.Point(451, 194)
        Me._Label_33.Name = "_Label_33"
        Me._Label_33.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label_33.Size = New System.Drawing.Size(90, 14)
        Me._Label_33.TabIndex = 152
        Me._Label_33.Text = "Text Description :"
        Me._Label_33.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me._Label_33.Visible = False
        '
        '_Label_8
        '
        Me._Label_8.AutoSize = True
        Me._Label_8.BackColor = System.Drawing.Color.Transparent
        Me._Label_8.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label_8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label_8.ForeColor = System.Drawing.Color.Black
        Me.Label.SetIndex(Me._Label_8, CType(8, Short))
        Me._Label_8.Location = New System.Drawing.Point(19, 259)
        Me._Label_8.Name = "_Label_8"
        Me._Label_8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label_8.Size = New System.Drawing.Size(89, 14)
        Me._Label_8.TabIndex = 146
        Me._Label_8.Text = "Co-Buyer Name :"
        Me._Label_8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label_7
        '
        Me._Label_7.AutoSize = True
        Me._Label_7.BackColor = System.Drawing.Color.Transparent
        Me._Label_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label_7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label_7.ForeColor = System.Drawing.Color.Black
        Me.Label.SetIndex(Me._Label_7, CType(7, Short))
        Me._Label_7.Location = New System.Drawing.Point(36, 231)
        Me._Label_7.Name = "_Label_7"
        Me._Label_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label_7.Size = New System.Drawing.Size(72, 14)
        Me._Label_7.TabIndex = 138
        Me._Label_7.Text = "Buyer Name :"
        Me._Label_7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label_6
        '
        Me._Label_6.AutoSize = True
        Me._Label_6.BackColor = System.Drawing.SystemColors.Control
        Me._Label_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label_6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label.SetIndex(Me._Label_6, CType(6, Short))
        Me._Label_6.Location = New System.Drawing.Point(54, 203)
        Me._Label_6.Name = "_Label_6"
        Me._Label_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label_6.Size = New System.Drawing.Size(54, 14)
        Me._Label_6.TabIndex = 130
        Me._Label_6.Text = "Location :"
        '
        '_Label_5
        '
        Me._Label_5.AutoSize = True
        Me._Label_5.BackColor = System.Drawing.SystemColors.Control
        Me._Label_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label_5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label.SetIndex(Me._Label_5, CType(5, Short))
        Me._Label_5.Location = New System.Drawing.Point(10, 175)
        Me._Label_5.Name = "_Label_5"
        Me._Label_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label_5.Size = New System.Drawing.Size(98, 14)
        Me._Label_5.TabIndex = 129
        Me._Label_5.Text = "Advance License :"
        Me._Label_5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label_4
        '
        Me._Label_4.AutoSize = True
        Me._Label_4.BackColor = System.Drawing.SystemColors.Control
        Me._Label_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label_4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label.SetIndex(Me._Label_4, CType(4, Short))
        Me._Label_4.Location = New System.Drawing.Point(48, 147)
        Me._Label_4.Name = "_Label_4"
        Me._Label_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label_4.Size = New System.Drawing.Size(60, 14)
        Me._Label_4.TabIndex = 128
        Me._Label_4.Text = "Total Euro :"
        Me._Label_4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label_3
        '
        Me._Label_3.AutoSize = True
        Me._Label_3.BackColor = System.Drawing.SystemColors.Control
        Me._Label_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label_3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label.SetIndex(Me._Label_3, CType(3, Short))
        Me._Label_3.Location = New System.Drawing.Point(22, 121)
        Me._Label_3.Name = "_Label_3"
        Me._Label_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label_3.Size = New System.Drawing.Size(86, 14)
        Me._Label_3.TabIndex = 127
        Me._Label_3.Text = "Exchange Rate :"
        Me._Label_3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotExportExp
        '
        Me.lblTotExportExp.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotExportExp.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotExportExp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotExportExp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTotExportExp.Location = New System.Drawing.Point(464, 36)
        Me.lblTotExportExp.Name = "lblTotExportExp"
        Me.lblTotExportExp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotExportExp.Size = New System.Drawing.Size(39, 13)
        Me.lblTotExportExp.TabIndex = 126
        Me.lblTotExportExp.Text = "lblTotExportExp"
        Me.lblTotExportExp.Visible = False
        '
        '_Label_2
        '
        Me._Label_2.AutoSize = True
        Me._Label_2.BackColor = System.Drawing.SystemColors.Control
        Me._Label_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label.SetIndex(Me._Label_2, CType(2, Short))
        Me._Label_2.Location = New System.Drawing.Point(32, 93)
        Me._Label_2.Name = "_Label_2"
        Me._Label_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label_2.Size = New System.Drawing.Size(76, 14)
        Me._Label_2.TabIndex = 125
        Me._Label_2.Text = "Export Bill No :"
        Me._Label_2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.BackColor = System.Drawing.SystemColors.Control
        Me.Label48.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label48.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label48.Location = New System.Drawing.Point(245, 92)
        Me.Label48.Name = "Label48"
        Me.Label48.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label48.Size = New System.Drawing.Size(35, 14)
        Me.Label48.TabIndex = 124
        Me.Label48.Text = "Date :"
        Me.Label48.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.BackColor = System.Drawing.SystemColors.Control
        Me.Label47.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label47.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label47.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label47.Location = New System.Drawing.Point(244, 18)
        Me.Label47.Name = "Label47"
        Me.Label47.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label47.Size = New System.Drawing.Size(35, 14)
        Me.Label47.TabIndex = 123
        Me.Label47.Text = "Date :"
        Me.Label47.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label_1
        '
        Me._Label_1.AutoSize = True
        Me._Label_1.BackColor = System.Drawing.SystemColors.Control
        Me._Label_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label.SetIndex(Me._Label_1, CType(1, Short))
        Me._Label_1.Location = New System.Drawing.Point(52, 45)
        Me._Label_1.Name = "_Label_1"
        Me._Label_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label_1.Size = New System.Drawing.Size(56, 14)
        Me._Label_1.TabIndex = 122
        Me._Label_1.Text = "ARE1 No :"
        Me._Label_1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label_0
        '
        Me._Label_0.AutoSize = True
        Me._Label_0.BackColor = System.Drawing.SystemColors.Control
        Me._Label_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label.SetIndex(Me._Label_0, CType(0, Short))
        Me._Label_0.Location = New System.Drawing.Point(19, 19)
        Me._Label_0.Name = "_Label_0"
        Me._Label_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label_0.Size = New System.Drawing.Size(89, 14)
        Me._Label_0.TabIndex = 121
        Me._Label_0.Text = "Shipping Bill No. :"
        Me._Label_0.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label59
        '
        Me.Label59.AutoSize = True
        Me.Label59.BackColor = System.Drawing.SystemColors.Control
        Me.Label59.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label59.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label59.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label59.Location = New System.Drawing.Point(245, 44)
        Me.Label59.Name = "Label59"
        Me.Label59.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label59.Size = New System.Drawing.Size(35, 14)
        Me.Label59.TabIndex = 120
        Me.Label59.Text = "Date :"
        Me.Label59.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_TabMain_TabPage3
        '
        Me._TabMain_TabPage3.Controls.Add(Me.Frame11)
        Me._TabMain_TabPage3.Location = New System.Drawing.Point(4, 22)
        Me._TabMain_TabPage3.Name = "_TabMain_TabPage3"
        Me._TabMain_TabPage3.Size = New System.Drawing.Size(904, 388)
        Me._TabMain_TabPage3.TabIndex = 3
        Me._TabMain_TabPage3.Text = "Advance Details"
        '
        'Frame11
        '
        Me.Frame11.BackColor = System.Drawing.SystemColors.Control
        Me.Frame11.Controls.Add(Me.txtAdvIGSTBal)
        Me.Frame11.Controls.Add(Me.txtAdvSGSTBal)
        Me.Frame11.Controls.Add(Me.txtAdvCGSTBal)
        Me.Frame11.Controls.Add(Me.txtAdvBal)
        Me.Frame11.Controls.Add(Me.txtItemAdvAdjust)
        Me.Frame11.Controls.Add(Me.txtAdvCGST)
        Me.Frame11.Controls.Add(Me.txtAdvSGST)
        Me.Frame11.Controls.Add(Me.txtAdvIGST)
        Me.Frame11.Controls.Add(Me.txtAdvDate)
        Me.Frame11.Controls.Add(Me.txtAdvVNo)
        Me.Frame11.Controls.Add(Me.txtAdvAdjust)
        Me.Frame11.Controls.Add(Me.Label34)
        Me.Frame11.Controls.Add(Me.Label28)
        Me.Frame11.Controls.Add(Me.Label25)
        Me.Frame11.Controls.Add(Me.Label16)
        Me.Frame11.Controls.Add(Me.Label10)
        Me.Frame11.Controls.Add(Me.Label8)
        Me.Frame11.Controls.Add(Me.Label21)
        Me.Frame11.Controls.Add(Me.Label22)
        Me.Frame11.Controls.Add(Me.Label18)
        Me.Frame11.Controls.Add(Me.Label23)
        Me.Frame11.Controls.Add(Me.Label24)
        Me.Frame11.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame11.Location = New System.Drawing.Point(7, -1)
        Me.Frame11.Name = "Frame11"
        Me.Frame11.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame11.Size = New System.Drawing.Size(373, 151)
        Me.Frame11.TabIndex = 187
        Me.Frame11.TabStop = False
        Me.Frame11.Text = "Advance Details"
        '
        'txtAdvIGSTBal
        '
        Me.txtAdvIGSTBal.AcceptsReturn = True
        Me.txtAdvIGSTBal.BackColor = System.Drawing.SystemColors.Window
        Me.txtAdvIGSTBal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAdvIGSTBal.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAdvIGSTBal.Enabled = False
        Me.txtAdvIGSTBal.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdvIGSTBal.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAdvIGSTBal.Location = New System.Drawing.Point(109, 106)
        Me.txtAdvIGSTBal.MaxLength = 0
        Me.txtAdvIGSTBal.Name = "txtAdvIGSTBal"
        Me.txtAdvIGSTBal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAdvIGSTBal.Size = New System.Drawing.Size(73, 20)
        Me.txtAdvIGSTBal.TabIndex = 206
        Me.txtAdvIGSTBal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtAdvSGSTBal
        '
        Me.txtAdvSGSTBal.AcceptsReturn = True
        Me.txtAdvSGSTBal.BackColor = System.Drawing.SystemColors.Window
        Me.txtAdvSGSTBal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAdvSGSTBal.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAdvSGSTBal.Enabled = False
        Me.txtAdvSGSTBal.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdvSGSTBal.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAdvSGSTBal.Location = New System.Drawing.Point(109, 84)
        Me.txtAdvSGSTBal.MaxLength = 0
        Me.txtAdvSGSTBal.Name = "txtAdvSGSTBal"
        Me.txtAdvSGSTBal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAdvSGSTBal.Size = New System.Drawing.Size(73, 20)
        Me.txtAdvSGSTBal.TabIndex = 205
        Me.txtAdvSGSTBal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtAdvCGSTBal
        '
        Me.txtAdvCGSTBal.AcceptsReturn = True
        Me.txtAdvCGSTBal.BackColor = System.Drawing.SystemColors.Window
        Me.txtAdvCGSTBal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAdvCGSTBal.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAdvCGSTBal.Enabled = False
        Me.txtAdvCGSTBal.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdvCGSTBal.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAdvCGSTBal.Location = New System.Drawing.Point(109, 62)
        Me.txtAdvCGSTBal.MaxLength = 0
        Me.txtAdvCGSTBal.Name = "txtAdvCGSTBal"
        Me.txtAdvCGSTBal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAdvCGSTBal.Size = New System.Drawing.Size(73, 20)
        Me.txtAdvCGSTBal.TabIndex = 204
        Me.txtAdvCGSTBal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
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
        Me.txtAdvBal.TabIndex = 190
        Me.txtAdvBal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
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
        Me.txtItemAdvAdjust.TabIndex = 191
        Me.txtItemAdvAdjust.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
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
        Me.txtAdvCGST.TabIndex = 192
        Me.txtAdvCGST.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
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
        Me.txtAdvSGST.TabIndex = 193
        Me.txtAdvSGST.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
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
        Me.txtAdvIGST.TabIndex = 194
        Me.txtAdvIGST.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
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
        Me.txtAdvDate.TabIndex = 189
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
        Me.txtAdvVNo.TabIndex = 188
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
        Me.txtAdvAdjust.TabIndex = 195
        Me.txtAdvAdjust.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.BackColor = System.Drawing.SystemColors.Control
        Me.Label34.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label34.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label34.Location = New System.Drawing.Point(25, 108)
        Me.Label34.Name = "Label34"
        Me.Label34.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label34.Size = New System.Drawing.Size(78, 14)
        Me.Label34.TabIndex = 209
        Me.Label34.Text = "Bal. IGST Amt :"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.SystemColors.Control
        Me.Label28.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label28.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label28.Location = New System.Drawing.Point(20, 86)
        Me.Label28.Name = "Label28"
        Me.Label28.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label28.Size = New System.Drawing.Size(83, 14)
        Me.Label28.TabIndex = 208
        Me.Label28.Text = "Bal. SGST Amt :"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.SystemColors.Control
        Me.Label25.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label25.Location = New System.Drawing.Point(20, 64)
        Me.Label25.Name = "Label25"
        Me.Label25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label25.Size = New System.Drawing.Size(83, 14)
        Me.Label25.TabIndex = 207
        Me.Label25.Text = "Bal. CGST Amt :"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(12, 40)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(91, 14)
        Me.Label16.TabIndex = 203
        Me.Label16.Text = "Balance Amount :"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(191, 40)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(96, 14)
        Me.Label10.TabIndex = 202
        Me.Label10.Text = "Advance Amount :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(207, 62)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(80, 14)
        Me.Label8.TabIndex = 201
        Me.Label8.Text = "CGST Amount :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.SystemColors.Control
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(207, 84)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(80, 14)
        Me.Label21.TabIndex = 200
        Me.Label21.Text = "SGST Amount :"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.SystemColors.Control
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(212, 106)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(75, 14)
        Me.Label22.TabIndex = 199
        Me.Label22.Text = "IGST Amount :"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(252, 18)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(35, 14)
        Me.Label18.TabIndex = 198
        Me.Label18.Text = "Date :"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.SystemColors.Control
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(25, 18)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(78, 14)
        Me.Label23.TabIndex = 197
        Me.Label23.Text = "Payment VNo :"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.SystemColors.Control
        Me.Label24.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label24.Location = New System.Drawing.Point(168, 128)
        Me.Label24.Name = "Label24"
        Me.Label24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label24.Size = New System.Drawing.Size(119, 14)
        Me.Label24.TabIndex = 196
        Me.Label24.Text = "Total Adjusted Amount :"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_TabMain_TabPage4
        '
        Me._TabMain_TabPage4.Controls.Add(Me.Frame12)
        Me._TabMain_TabPage4.Controls.Add(Me.Frame13)
        Me._TabMain_TabPage4.Controls.Add(Me.Frame14)
        Me._TabMain_TabPage4.Location = New System.Drawing.Point(4, 22)
        Me._TabMain_TabPage4.Name = "_TabMain_TabPage4"
        Me._TabMain_TabPage4.Size = New System.Drawing.Size(904, 388)
        Me._TabMain_TabPage4.TabIndex = 4
        Me._TabMain_TabPage4.Text = "Vehicle && eWay Detail"
        '
        'Frame12
        '
        Me.Frame12.BackColor = System.Drawing.SystemColors.Control
        Me.Frame12.Controls.Add(Me.chkByHand)
        Me.Frame12.Controls.Add(Me.txtDistanceUpdate)
        Me.Frame12.Controls.Add(Me.txtDistance)
        Me.Frame12.Controls.Add(Me.txtTransportCode)
        Me.Frame12.Controls.Add(Me.cboTransmode)
        Me.Frame12.Controls.Add(Me.cboVehicleType)
        Me.Frame12.Controls.Add(Me.txtCarriers)
        Me.Frame12.Controls.Add(Me.txtVehicle)
        Me.Frame12.Controls.Add(Me.txtMode)
        Me.Frame12.Controls.Add(Me.Label49)
        Me.Frame12.Controls.Add(Me.Label46)
        Me.Frame12.Controls.Add(Me.Label42)
        Me.Frame12.Controls.Add(Me.Label38)
        Me.Frame12.Controls.Add(Me.Label30)
        Me.Frame12.Controls.Add(Me.Label29)
        Me.Frame12.Controls.Add(Me.Label27)
        Me.Frame12.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame12.Location = New System.Drawing.Point(3, -2)
        Me.Frame12.Name = "Frame12"
        Me.Frame12.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame12.Size = New System.Drawing.Size(457, 286)
        Me.Frame12.TabIndex = 211
        Me.Frame12.TabStop = False
        Me.Frame12.Text = "Vehicle Details"
        '
        'txtDistance
        '
        Me.txtDistance.AcceptsReturn = True
        Me.txtDistance.BackColor = System.Drawing.SystemColors.Window
        Me.txtDistance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDistance.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDistance.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDistance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDistance.Location = New System.Drawing.Point(118, 134)
        Me.txtDistance.MaxLength = 0
        Me.txtDistance.Name = "txtDistance"
        Me.txtDistance.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDistance.Size = New System.Drawing.Size(137, 20)
        Me.txtDistance.TabIndex = 217
        '
        'txtTransportCode
        '
        Me.txtTransportCode.AcceptsReturn = True
        Me.txtTransportCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtTransportCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTransportCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTransportCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransportCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTransportCode.Location = New System.Drawing.Point(118, 112)
        Me.txtTransportCode.MaxLength = 0
        Me.txtTransportCode.Name = "txtTransportCode"
        Me.txtTransportCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTransportCode.Size = New System.Drawing.Size(267, 20)
        Me.txtTransportCode.TabIndex = 216
        '
        'cboTransmode
        '
        Me.cboTransmode.BackColor = System.Drawing.SystemColors.Window
        Me.cboTransmode.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboTransmode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTransmode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboTransmode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboTransmode.Location = New System.Drawing.Point(118, 44)
        Me.cboTransmode.Name = "cboTransmode"
        Me.cboTransmode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboTransmode.Size = New System.Drawing.Size(187, 22)
        Me.cboTransmode.TabIndex = 213
        '
        'cboVehicleType
        '
        Me.cboVehicleType.BackColor = System.Drawing.SystemColors.Window
        Me.cboVehicleType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboVehicleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboVehicleType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboVehicleType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboVehicleType.Location = New System.Drawing.Point(118, 156)
        Me.cboVehicleType.Name = "cboVehicleType"
        Me.cboVehicleType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboVehicleType.Size = New System.Drawing.Size(137, 22)
        Me.cboVehicleType.TabIndex = 218
        '
        'txtCarriers
        '
        Me.txtCarriers.AcceptsReturn = True
        Me.txtCarriers.BackColor = System.Drawing.SystemColors.Window
        Me.txtCarriers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCarriers.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCarriers.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCarriers.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCarriers.Location = New System.Drawing.Point(118, 90)
        Me.txtCarriers.MaxLength = 0
        Me.txtCarriers.Name = "txtCarriers"
        Me.txtCarriers.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCarriers.Size = New System.Drawing.Size(267, 20)
        Me.txtCarriers.TabIndex = 215
        '
        'txtVehicle
        '
        Me.txtVehicle.AcceptsReturn = True
        Me.txtVehicle.BackColor = System.Drawing.SystemColors.Window
        Me.txtVehicle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVehicle.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVehicle.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVehicle.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtVehicle.Location = New System.Drawing.Point(118, 68)
        Me.txtVehicle.MaxLength = 0
        Me.txtVehicle.Name = "txtVehicle"
        Me.txtVehicle.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVehicle.Size = New System.Drawing.Size(267, 20)
        Me.txtVehicle.TabIndex = 214
        '
        'txtMode
        '
        Me.txtMode.AcceptsReturn = True
        Me.txtMode.BackColor = System.Drawing.SystemColors.Window
        Me.txtMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMode.Location = New System.Drawing.Point(118, 22)
        Me.txtMode.MaxLength = 0
        Me.txtMode.Name = "txtMode"
        Me.txtMode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMode.Size = New System.Drawing.Size(267, 20)
        Me.txtMode.TabIndex = 212
        '
        'Label49
        '
        Me.Label49.AutoSize = True
        Me.Label49.BackColor = System.Drawing.SystemColors.Control
        Me.Label49.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label49.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label49.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label49.Location = New System.Drawing.Point(45, 48)
        Me.Label49.Name = "Label49"
        Me.Label49.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label49.Size = New System.Drawing.Size(70, 14)
        Me.Label49.TabIndex = 225
        Me.Label49.Text = "Trans Mode :"
        Me.Label49.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.BackColor = System.Drawing.SystemColors.Control
        Me.Label46.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label46.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label46.Location = New System.Drawing.Point(60, 136)
        Me.Label46.Name = "Label46"
        Me.Label46.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label46.Size = New System.Drawing.Size(55, 14)
        Me.Label46.TabIndex = 224
        Me.Label46.Text = "Distance :"
        Me.Label46.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.BackColor = System.Drawing.SystemColors.Control
        Me.Label42.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label42.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label42.Location = New System.Drawing.Point(43, 114)
        Me.Label42.Name = "Label42"
        Me.Label42.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label42.Size = New System.Drawing.Size(72, 14)
        Me.Label42.TabIndex = 223
        Me.Label42.Text = "Transport ID :"
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.BackColor = System.Drawing.SystemColors.Control
        Me.Label38.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label38.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label38.Location = New System.Drawing.Point(41, 158)
        Me.Label38.Name = "Label38"
        Me.Label38.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label38.Size = New System.Drawing.Size(74, 14)
        Me.Label38.TabIndex = 222
        Me.Label38.Text = "Vehicle Type :"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.BackColor = System.Drawing.SystemColors.Control
        Me.Label30.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label30.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label30.Location = New System.Drawing.Point(15, 94)
        Me.Label30.Name = "Label30"
        Me.Label30.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label30.Size = New System.Drawing.Size(100, 14)
        Me.Label30.TabIndex = 221
        Me.Label30.Text = "Transporter Name :"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.SystemColors.Control
        Me.Label29.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label29.Location = New System.Drawing.Point(67, 72)
        Me.Label29.Name = "Label29"
        Me.Label29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label29.Size = New System.Drawing.Size(48, 14)
        Me.Label29.TabIndex = 220
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
        Me.Label27.Location = New System.Drawing.Point(76, 24)
        Me.Label27.Name = "Label27"
        Me.Label27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label27.Size = New System.Drawing.Size(39, 14)
        Me.Label27.TabIndex = 219
        Me.Label27.Text = "Mode :"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame13
        '
        Me.Frame13.BackColor = System.Drawing.SystemColors.Control
        Me.Frame13.Controls.Add(Me.cmdeWayBill)
        Me.Frame13.Controls.Add(Me.txtResponseId)
        Me.Frame13.Controls.Add(Me.txtEWayBillNo)
        Me.Frame13.Controls.Add(Me.Label57)
        Me.Frame13.Controls.Add(Me.Label52)
        Me.Frame13.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame13.Location = New System.Drawing.Point(466, 162)
        Me.Frame13.Name = "Frame13"
        Me.Frame13.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame13.Size = New System.Drawing.Size(438, 120)
        Me.Frame13.TabIndex = 226
        Me.Frame13.TabStop = False
        Me.Frame13.Text = "eWay Details"
        '
        'txtResponseId
        '
        Me.txtResponseId.AcceptsReturn = True
        Me.txtResponseId.BackColor = System.Drawing.SystemColors.Window
        Me.txtResponseId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtResponseId.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtResponseId.Enabled = False
        Me.txtResponseId.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtResponseId.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtResponseId.Location = New System.Drawing.Point(158, 22)
        Me.txtResponseId.MaxLength = 0
        Me.txtResponseId.Name = "txtResponseId"
        Me.txtResponseId.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtResponseId.Size = New System.Drawing.Size(267, 20)
        Me.txtResponseId.TabIndex = 228
        '
        'txtEWayBillNo
        '
        Me.txtEWayBillNo.AcceptsReturn = True
        Me.txtEWayBillNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtEWayBillNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEWayBillNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEWayBillNo.Enabled = False
        Me.txtEWayBillNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEWayBillNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEWayBillNo.Location = New System.Drawing.Point(158, 46)
        Me.txtEWayBillNo.MaxLength = 0
        Me.txtEWayBillNo.Name = "txtEWayBillNo"
        Me.txtEWayBillNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEWayBillNo.Size = New System.Drawing.Size(267, 20)
        Me.txtEWayBillNo.TabIndex = 227
        '
        'Label57
        '
        Me.Label57.AutoSize = True
        Me.Label57.BackColor = System.Drawing.SystemColors.Control
        Me.Label57.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label57.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label57.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label57.Location = New System.Drawing.Point(66, 24)
        Me.Label57.Name = "Label57"
        Me.Label57.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label57.Size = New System.Drawing.Size(74, 14)
        Me.Label57.TabIndex = 230
        Me.Label57.Text = "Response ID :"
        Me.Label57.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label52
        '
        Me.Label52.AutoSize = True
        Me.Label52.BackColor = System.Drawing.SystemColors.Control
        Me.Label52.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label52.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label52.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label52.Location = New System.Drawing.Point(87, 50)
        Me.Label52.Name = "Label52"
        Me.Label52.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label52.Size = New System.Drawing.Size(57, 14)
        Me.Label52.TabIndex = 229
        Me.Label52.Text = "eWay No :"
        Me.Label52.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame14
        '
        Me.Frame14.BackColor = System.Drawing.SystemColors.Control
        Me.Frame14.Controls.Add(Me.cmpPrinteInvoice)
        Me.Frame14.Controls.Add(Me.cmdQRCode)
        Me.Frame14.Controls.Add(Me.cmdeInvoice)
        Me.Frame14.Controls.Add(Me.txteInvAckDate)
        Me.Frame14.Controls.Add(Me.txteInvAckNo)
        Me.Frame14.Controls.Add(Me.txtIRNNo)
        Me.Frame14.Controls.Add(Me.Label58)
        Me.Frame14.Controls.Add(Me.Label54)
        Me.Frame14.Controls.Add(Me.Label51)
        Me.Frame14.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame14.Location = New System.Drawing.Point(466, -2)
        Me.Frame14.Name = "Frame14"
        Me.Frame14.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame14.Size = New System.Drawing.Size(436, 166)
        Me.Frame14.TabIndex = 240
        Me.Frame14.TabStop = False
        Me.Frame14.Text = "Generate e-Invoice"
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
        Me.txteInvAckDate.Size = New System.Drawing.Size(363, 20)
        Me.txteInvAckDate.TabIndex = 245
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
        Me.txteInvAckNo.Size = New System.Drawing.Size(363, 20)
        Me.txteInvAckNo.TabIndex = 242
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
        Me.txtIRNNo.Size = New System.Drawing.Size(363, 49)
        Me.txtIRNNo.TabIndex = 241
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
        Me.Label58.TabIndex = 246
        Me.Label58.Text = "Ack Date :"
        '
        'Label54
        '
        Me.Label54.AutoSize = True
        Me.Label54.BackColor = System.Drawing.SystemColors.Control
        Me.Label54.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label54.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label54.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label54.Location = New System.Drawing.Point(7, 78)
        Me.Label54.Name = "Label54"
        Me.Label54.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label54.Size = New System.Drawing.Size(48, 14)
        Me.Label54.TabIndex = 244
        Me.Label54.Text = "Ack No :"
        '
        'Label51
        '
        Me.Label51.AutoSize = True
        Me.Label51.BackColor = System.Drawing.SystemColors.Control
        Me.Label51.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label51.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label51.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label51.Location = New System.Drawing.Point(7, 24)
        Me.Label51.Name = "Label51"
        Me.Label51.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label51.Size = New System.Drawing.Size(29, 14)
        Me.Label51.TabIndex = 243
        Me.Label51.Text = "IRN :"
        '
        'TxtGRDate
        '
        Me.TxtGRDate.AcceptsReturn = True
        Me.TxtGRDate.BackColor = System.Drawing.SystemColors.Window
        Me.TxtGRDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtGRDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtGRDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtGRDate.ForeColor = System.Drawing.Color.Blue
        Me.TxtGRDate.Location = New System.Drawing.Point(338, 110)
        Me.TxtGRDate.MaxLength = 0
        Me.TxtGRDate.Name = "TxtGRDate"
        Me.TxtGRDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtGRDate.Size = New System.Drawing.Size(103, 20)
        Me.TxtGRDate.TabIndex = 16
        '
        'TxtGRNo
        '
        Me.TxtGRNo.AcceptsReturn = True
        Me.TxtGRNo.BackColor = System.Drawing.SystemColors.Window
        Me.TxtGRNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtGRNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtGRNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtGRNo.ForeColor = System.Drawing.Color.Blue
        Me.TxtGRNo.Location = New System.Drawing.Point(82, 110)
        Me.TxtGRNo.MaxLength = 0
        Me.TxtGRNo.Name = "TxtGRNo"
        Me.TxtGRNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtGRNo.Size = New System.Drawing.Size(163, 20)
        Me.TxtGRNo.TabIndex = 15
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
        Me.txtCreditAccount.Location = New System.Drawing.Point(614, 110)
        Me.txtCreditAccount.MaxLength = 0
        Me.txtCreditAccount.Name = "txtCreditAccount"
        Me.txtCreditAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCreditAccount.Size = New System.Drawing.Size(287, 20)
        Me.txtCreditAccount.TabIndex = 14
        Me.txtCreditAccount.Visible = False
        '
        'txtCustomer
        '
        Me.txtCustomer.AcceptsReturn = True
        Me.txtCustomer.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustomer.Enabled = False
        Me.txtCustomer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomer.ForeColor = System.Drawing.Color.Blue
        Me.txtCustomer.Location = New System.Drawing.Point(614, 38)
        Me.txtCustomer.MaxLength = 0
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustomer.Size = New System.Drawing.Size(287, 20)
        Me.txtCustomer.TabIndex = 10
        '
        'txtRemovalDate
        '
        Me.txtRemovalDate.AcceptsReturn = True
        Me.txtRemovalDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemovalDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemovalDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemovalDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemovalDate.ForeColor = System.Drawing.Color.Blue
        Me.txtRemovalDate.Location = New System.Drawing.Point(338, 85)
        Me.txtRemovalDate.MaxLength = 0
        Me.txtRemovalDate.Name = "txtRemovalDate"
        Me.txtRemovalDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemovalDate.Size = New System.Drawing.Size(71, 20)
        Me.txtRemovalDate.TabIndex = 12
        '
        'txtRemovalTime
        '
        Me.txtRemovalTime.AcceptsReturn = True
        Me.txtRemovalTime.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemovalTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemovalTime.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemovalTime.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemovalTime.ForeColor = System.Drawing.Color.Blue
        Me.txtRemovalTime.Location = New System.Drawing.Point(410, 85)
        Me.txtRemovalTime.MaxLength = 0
        Me.txtRemovalTime.Name = "txtRemovalTime"
        Me.txtRemovalTime.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemovalTime.Size = New System.Drawing.Size(31, 20)
        Me.txtRemovalTime.TabIndex = 13
        '
        'txtDCDate
        '
        Me.txtDCDate.AcceptsReturn = True
        Me.txtDCDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtDCDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDCDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDCDate.Enabled = False
        Me.txtDCDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDCDate.ForeColor = System.Drawing.Color.Blue
        Me.txtDCDate.Location = New System.Drawing.Point(338, 13)
        Me.txtDCDate.MaxLength = 0
        Me.txtDCDate.Name = "txtDCDate"
        Me.txtDCDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDCDate.Size = New System.Drawing.Size(103, 20)
        Me.txtDCDate.TabIndex = 3
        '
        'txtBillDate
        '
        Me.txtBillDate.AcceptsReturn = True
        Me.txtBillDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillDate.ForeColor = System.Drawing.Color.Blue
        Me.txtBillDate.Location = New System.Drawing.Point(338, 38)
        Me.txtBillDate.MaxLength = 0
        Me.txtBillDate.Name = "txtBillDate"
        Me.txtBillDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillDate.Size = New System.Drawing.Size(71, 20)
        Me.txtBillDate.TabIndex = 8
        '
        'TxtBillTm
        '
        Me.TxtBillTm.AcceptsReturn = True
        Me.TxtBillTm.BackColor = System.Drawing.SystemColors.Window
        Me.TxtBillTm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtBillTm.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtBillTm.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBillTm.ForeColor = System.Drawing.Color.Blue
        Me.TxtBillTm.Location = New System.Drawing.Point(410, 38)
        Me.TxtBillTm.MaxLength = 0
        Me.TxtBillTm.Name = "TxtBillTm"
        Me.TxtBillTm.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtBillTm.Size = New System.Drawing.Size(31, 20)
        Me.TxtBillTm.TabIndex = 9
        '
        'cboInvType
        '
        Me.cboInvType.BackColor = System.Drawing.SystemColors.Window
        Me.cboInvType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboInvType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboInvType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboInvType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboInvType.Location = New System.Drawing.Point(614, 13)
        Me.cboInvType.Name = "cboInvType"
        Me.cboInvType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboInvType.Size = New System.Drawing.Size(287, 22)
        Me.cboInvType.TabIndex = 7
        '
        'txtDCNo
        '
        Me.txtDCNo.AcceptsReturn = True
        Me.txtDCNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtDCNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDCNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDCNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDCNo.ForeColor = System.Drawing.Color.Blue
        Me.txtDCNo.Location = New System.Drawing.Point(82, 13)
        Me.txtDCNo.MaxLength = 0
        Me.txtDCNo.Name = "txtDCNo"
        Me.txtDCNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDCNo.Size = New System.Drawing.Size(142, 20)
        Me.txtDCNo.TabIndex = 1
        '
        'txtBillNoPrefix
        '
        Me.txtBillNoPrefix.AcceptsReturn = True
        Me.txtBillNoPrefix.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillNoPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillNoPrefix.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillNoPrefix.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillNoPrefix.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtBillNoPrefix.Location = New System.Drawing.Point(82, 38)
        Me.txtBillNoPrefix.MaxLength = 0
        Me.txtBillNoPrefix.Name = "txtBillNoPrefix"
        Me.txtBillNoPrefix.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillNoPrefix.Size = New System.Drawing.Size(68, 20)
        Me.txtBillNoPrefix.TabIndex = 4
        '
        'txtBillNoSuffix
        '
        Me.txtBillNoSuffix.AcceptsReturn = True
        Me.txtBillNoSuffix.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillNoSuffix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillNoSuffix.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillNoSuffix.Enabled = False
        Me.txtBillNoSuffix.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillNoSuffix.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtBillNoSuffix.Location = New System.Drawing.Point(226, 38)
        Me.txtBillNoSuffix.MaxLength = 0
        Me.txtBillNoSuffix.Name = "txtBillNoSuffix"
        Me.txtBillNoSuffix.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillNoSuffix.Size = New System.Drawing.Size(73, 20)
        Me.txtBillNoSuffix.TabIndex = 6
        '
        'txtBillNo
        '
        Me.txtBillNo.AcceptsReturn = True
        Me.txtBillNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtBillNo.Location = New System.Drawing.Point(151, 38)
        Me.txtBillNo.MaxLength = 0
        Me.txtBillNo.Name = "txtBillNo"
        Me.txtBillNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillNo.Size = New System.Drawing.Size(73, 20)
        Me.txtBillNo.TabIndex = 5
        '
        'TxtDCNoPrefix
        '
        Me.TxtDCNoPrefix.AcceptsReturn = True
        Me.TxtDCNoPrefix.BackColor = System.Drawing.SystemColors.Window
        Me.TxtDCNoPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtDCNoPrefix.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtDCNoPrefix.Enabled = False
        Me.TxtDCNoPrefix.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDCNoPrefix.ForeColor = System.Drawing.Color.Blue
        Me.TxtDCNoPrefix.Location = New System.Drawing.Point(88, 12)
        Me.TxtDCNoPrefix.MaxLength = 0
        Me.TxtDCNoPrefix.Name = "TxtDCNoPrefix"
        Me.TxtDCNoPrefix.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtDCNoPrefix.Size = New System.Drawing.Size(49, 20)
        Me.TxtDCNoPrefix.TabIndex = 62
        Me.TxtDCNoPrefix.Visible = False
        '
        'txtDCNoSuffix
        '
        Me.txtDCNoSuffix.AcceptsReturn = True
        Me.txtDCNoSuffix.BackColor = System.Drawing.SystemColors.Window
        Me.txtDCNoSuffix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDCNoSuffix.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDCNoSuffix.Enabled = False
        Me.txtDCNoSuffix.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDCNoSuffix.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDCNoSuffix.Location = New System.Drawing.Point(226, 14)
        Me.txtDCNoSuffix.MaxLength = 0
        Me.txtDCNoSuffix.Name = "txtDCNoSuffix"
        Me.txtDCNoSuffix.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDCNoSuffix.Size = New System.Drawing.Size(17, 20)
        Me.txtDCNoSuffix.TabIndex = 63
        Me.txtDCNoSuffix.Visible = False
        '
        'lblInvoiceSeq
        '
        Me.lblInvoiceSeq.AutoSize = True
        Me.lblInvoiceSeq.BackColor = System.Drawing.SystemColors.Control
        Me.lblInvoiceSeq.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblInvoiceSeq.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvoiceSeq.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInvoiceSeq.Location = New System.Drawing.Point(509, 85)
        Me.lblInvoiceSeq.Name = "lblInvoiceSeq"
        Me.lblInvoiceSeq.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblInvoiceSeq.Size = New System.Drawing.Size(70, 14)
        Me.lblInvoiceSeq.TabIndex = 183
        Me.lblInvoiceSeq.Text = "lblInvoiceSeq"
        Me.lblInvoiceSeq.Visible = False
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(28, 89)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(50, 14)
        Me.Label13.TabIndex = 149
        Me.Label13.Text = "Division :"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblSoDate
        '
        Me.lblSoDate.AutoSize = True
        Me.lblSoDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblSoDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSoDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSoDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSoDate.Location = New System.Drawing.Point(452, 102)
        Me.lblSoDate.Name = "lblSoDate"
        Me.lblSoDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSoDate.Size = New System.Drawing.Size(52, 14)
        Me.lblSoDate.TabIndex = 117
        Me.lblSoDate.Text = "lblSoDate"
        Me.lblSoDate.Visible = False
        '
        'lblDespRef
        '
        Me.lblDespRef.AutoSize = True
        Me.lblDespRef.BackColor = System.Drawing.SystemColors.Control
        Me.lblDespRef.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDespRef.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDespRef.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDespRef.Location = New System.Drawing.Point(520, 62)
        Me.lblDespRef.Name = "lblDespRef"
        Me.lblDespRef.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDespRef.Size = New System.Drawing.Size(59, 14)
        Me.lblDespRef.TabIndex = 116
        Me.lblDespRef.Text = "lblDespRef"
        Me.lblDespRef.Visible = False
        '
        'lblPoNo
        '
        Me.lblPoNo.AutoSize = True
        Me.lblPoNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblPoNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPoNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPoNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPoNo.Location = New System.Drawing.Point(454, 58)
        Me.lblPoNo.Name = "lblPoNo"
        Me.lblPoNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPoNo.Size = New System.Drawing.Size(42, 14)
        Me.lblPoNo.TabIndex = 102
        Me.lblPoNo.Text = "lblPoNo"
        Me.lblPoNo.Visible = False
        '
        'lblInvHeading
        '
        Me.lblInvHeading.BackColor = System.Drawing.SystemColors.Control
        Me.lblInvHeading.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblInvHeading.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvHeading.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInvHeading.Location = New System.Drawing.Point(452, 80)
        Me.lblInvHeading.Name = "lblInvHeading"
        Me.lblInvHeading.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblInvHeading.Size = New System.Drawing.Size(59, 15)
        Me.lblInvHeading.TabIndex = 101
        Me.lblInvHeading.Text = "Label6"
        Me.lblInvHeading.Visible = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(282, 135)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(52, 14)
        Me.Label12.TabIndex = 96
        Me.Label12.Text = "PO Date :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(281, 114)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(53, 14)
        Me.Label20.TabIndex = 78
        Me.Label20.Text = "GR Date :"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(550, 114)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(60, 14)
        Me.Label3.TabIndex = 76
        Me.Label3.Text = "Credit A/c :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label3.Visible = False
        '
        'lblCust
        '
        Me.lblCust.AutoSize = True
        Me.lblCust.BackColor = System.Drawing.SystemColors.Control
        Me.lblCust.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCust.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCust.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCust.Location = New System.Drawing.Point(551, 42)
        Me.lblCust.Name = "lblCust"
        Me.lblCust.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCust.Size = New System.Drawing.Size(59, 14)
        Me.lblCust.TabIndex = 75
        Me.lblCust.Text = "Customer :"
        Me.lblCust.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(255, 88)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(79, 14)
        Me.Label7.TabIndex = 74
        Me.Label7.Text = "Removal Date :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(299, 15)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(35, 14)
        Me.Label15.TabIndex = 73
        Me.Label15.Text = "Date :"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(299, 39)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(35, 14)
        Me.Label5.TabIndex = 72
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
        Me.Label2.Location = New System.Drawing.Point(557, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(53, 14)
        Me.Label2.TabIndex = 64
        Me.Label2.Text = "Inv Type :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(35, 16)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(43, 14)
        Me.Label14.TabIndex = 71
        Me.Label14.Text = "DC No :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(10, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(68, 13)
        Me.Label1.TabIndex = 70
        Me.Label1.Text = "Invoice No :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
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
        Me.Frame3.Controls.Add(Me.cmdAuthorised)
        Me.Frame3.Controls.Add(Me.cmdSave)
        Me.Frame3.Controls.Add(Me.cmdModify)
        Me.Frame3.Controls.Add(Me.cmdAdd)
        Me.Frame3.Controls.Add(Me.cmdSavePrint)
        Me.Frame3.Controls.Add(Me.Report1)
        Me.Frame3.Controls.Add(Me.lblSODates)
        Me.Frame3.Controls.Add(Me.lblSONos)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 562)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(908, 56)
        Me.Frame3.TabIndex = 65
        Me.Frame3.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(14, 12)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 119
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
        Me.lblSODates.TabIndex = 68
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
        Me.lblSONos.TabIndex = 67
        Me.lblSONos.Text = "lblSONos"
        Me.lblSONos.Visible = False
        '
        'OptFreight
        '
        '
        'txtCreditDays
        '
        '
        'UltraGrid1
        '
        Appearance1.BackColor = System.Drawing.SystemColors.Window
        Appearance1.BorderColor = System.Drawing.Color.White
        Me.UltraGrid1.DisplayLayout.Appearance = Appearance1
        Me.UltraGrid1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.UltraGrid1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
        Appearance2.BackColor = System.Drawing.Color.White
        Appearance2.BackColor2 = System.Drawing.Color.White
        Appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance2.BorderColor = System.Drawing.SystemColors.Window
        Me.UltraGrid1.DisplayLayout.GroupByBox.Appearance = Appearance2
        Appearance3.ForeColor = System.Drawing.SystemColors.GrayText
        Me.UltraGrid1.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance3
        Me.UltraGrid1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.UltraGrid1.DisplayLayout.GroupByBox.Hidden = True
        Appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance4.BackColor2 = System.Drawing.SystemColors.Control
        Appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance4.ForeColor = System.Drawing.SystemColors.GrayText
        Me.UltraGrid1.DisplayLayout.GroupByBox.PromptAppearance = Appearance4
        Me.UltraGrid1.DisplayLayout.MaxColScrollRegions = 1
        Me.UltraGrid1.DisplayLayout.MaxRowScrollRegions = 1
        Appearance5.BackColor = System.Drawing.SystemColors.Window
        Appearance5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.UltraGrid1.DisplayLayout.Override.ActiveCellAppearance = Appearance5
        Appearance6.BackColor = System.Drawing.SystemColors.Highlight
        Appearance6.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.UltraGrid1.DisplayLayout.Override.ActiveRowAppearance = Appearance6
        Me.UltraGrid1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
        Me.UltraGrid1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
        Appearance7.BackColor = System.Drawing.SystemColors.Window
        Me.UltraGrid1.DisplayLayout.Override.CardAreaAppearance = Appearance7
        Appearance8.BorderColor = System.Drawing.Color.Silver
        Appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
        Me.UltraGrid1.DisplayLayout.Override.CellAppearance = Appearance8
        Me.UltraGrid1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
        Me.UltraGrid1.DisplayLayout.Override.CellPadding = 0
        Appearance9.BackColor = System.Drawing.SystemColors.Control
        Appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
        Appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance9.BorderColor = System.Drawing.SystemColors.Window
        Me.UltraGrid1.DisplayLayout.Override.GroupByRowAppearance = Appearance9
        Appearance10.TextHAlignAsString = "Left"
        Me.UltraGrid1.DisplayLayout.Override.HeaderAppearance = Appearance10
        Me.UltraGrid1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Me.UltraGrid1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
        Appearance11.BackColor = System.Drawing.SystemColors.Window
        Appearance11.BorderColor = System.Drawing.Color.Silver
        Me.UltraGrid1.DisplayLayout.Override.RowAppearance = Appearance11
        Me.UltraGrid1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
        Appearance12.BackColor = System.Drawing.SystemColors.ControlLight
        Me.UltraGrid1.DisplayLayout.Override.TemplateAddRowAppearance = Appearance12
        Me.UltraGrid1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.UltraGrid1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.UltraGrid1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraGrid1.Location = New System.Drawing.Point(3, 3)
        Me.UltraGrid1.Name = "UltraGrid1"
        Me.UltraGrid1.Size = New System.Drawing.Size(905, 561)
        Me.UltraGrid1.TabIndex = 71
        '
        'chkByHand
        '
        Me.chkByHand.BackColor = System.Drawing.SystemColors.Control
        Me.chkByHand.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkByHand.Enabled = False
        Me.chkByHand.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkByHand.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkByHand.Location = New System.Drawing.Point(311, 47)
        Me.chkByHand.Name = "chkByHand"
        Me.chkByHand.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkByHand.Size = New System.Drawing.Size(103, 16)
        Me.chkByHand.TabIndex = 232
        Me.chkByHand.Text = "By Hand"
        Me.chkByHand.UseVisualStyleBackColor = False
        '
        'FrmInvoiceGST
        '
        Me.AcceptButton = Me.cmdSave
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(910, 621)
        Me.Controls.Add(Me.FraFront)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.UltraGrid1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmInvoiceGST"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Invoice "
        Me.FraFront.ResumeLayout(False)
        Me.FraFront.PerformLayout()
        Me.TabMain.ResumeLayout(False)
        Me._TabMain_TabPage0.ResumeLayout(False)
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me.FraPostingDtl.ResumeLayout(False)
        CType(Me.SprdPostingDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdExp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me._TabMain_TabPage1.ResumeLayout(False)
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.Frame10.ResumeLayout(False)
        Me.Frame10.PerformLayout()
        Me.Frame5.ResumeLayout(False)
        Me.Frame5.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.Frame8.ResumeLayout(False)
        Me.Frame8.PerformLayout()
        Me.Frame7.ResumeLayout(False)
        Me.Frame7.PerformLayout()
        Me._TabMain_TabPage2.ResumeLayout(False)
        Me.Frame9.ResumeLayout(False)
        Me.Frame9.PerformLayout()
        Me._TabMain_TabPage3.ResumeLayout(False)
        Me.Frame11.ResumeLayout(False)
        Me.Frame11.PerformLayout()
        Me._TabMain_TabPage4.ResumeLayout(False)
        Me.Frame12.ResumeLayout(False)
        Me.Frame12.PerformLayout()
        Me.Frame13.ResumeLayout(False)
        Me.Frame13.PerformLayout()
        Me.Frame14.ResumeLayout(False)
        Me.Frame14.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptFreight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCreditDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        'SprdView.DataSource = CType(AdoDCMain, MSDATASRC.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        'SprdView.DataSource = Nothing
    End Sub

    Public WithEvents txtBillTo As TextBox
    Public WithEvents Label62 As Label
    Public WithEvents TxtShipTo As TextBox
    Public WithEvents Label63 As Label
    Public WithEvents txtVendorCode As TextBox
    Public WithEvents Label64 As Label
    Public WithEvents txtPacking As TextBox
    Public WithEvents Label65 As Label
    Public WithEvents txtAddress As TextBox
    Friend WithEvents UltraGrid1 As Infragistics.Win.UltraWinGrid.UltraGrid
    Public WithEvents txtDistanceUpdate As Button
    Public WithEvents lblCompanyCode As Label
    Public WithEvents cmdeWayBill As Button
    Public WithEvents cmdResetPO As Button
    Public WithEvents lblModDate As Label
    Public WithEvents Label66 As Label
    Public WithEvents lblAddDate As Label
    Public WithEvents Label67 As Label
    Public WithEvents lblModUser As Label
    Public WithEvents Label68 As Label
    Public WithEvents lblAddUser As Label
    Public WithEvents Label69 As Label
    Public WithEvents txtTDSOnSale As TextBox
    Public WithEvents Label70 As Label
    Public WithEvents CmdPopFromFile As Button
    Public WithEvents CommonDialogOpen As OpenFileDialog
    Public WithEvents txtStoreDetail As TextBox
    Public WithEvents txtApplicant As TextBox
    Public WithEvents Label71 As Label
    Public WithEvents Label72 As Label
    Public WithEvents chkByHand As CheckBox
#End Region
End Class