Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmPO_GST
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
        'InventoryGST.Master.Show
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
    Public WithEvents chkDevelopment As System.Windows.Forms.CheckBox
    Public WithEvents chkPrintApp As System.Windows.Forms.CheckBox
    Public WithEvents cboGSTStatus As System.Windows.Forms.ComboBox
    Public WithEvents cmdSearchItem As System.Windows.Forms.Button
    Public WithEvents txtSearchItem As System.Windows.Forms.TextBox
    Public WithEvents chkCapital As System.Windows.Forms.CheckBox
    Public WithEvents txtDivision As System.Windows.Forms.TextBox
    Public WithEvents cmdDivSearch As System.Windows.Forms.Button
    Public WithEvents txtIndentNo As System.Windows.Forms.TextBox
    Public WithEvents ChkPrintAllItem As System.Windows.Forms.CheckBox
    Public WithEvents TxtExchangeRate As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchAmend As System.Windows.Forms.Button
    Public WithEvents ChkActivate As System.Windows.Forms.CheckBox
    Public WithEvents txtPrevPONo As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchPrevPO As System.Windows.Forms.Button
    Public WithEvents cmdSearchPO As System.Windows.Forms.Button
    Public WithEvents txtWEF As System.Windows.Forms.TextBox
    Public WithEvents chkStatus As System.Windows.Forms.CheckBox
    Public WithEvents txtAmendDate As System.Windows.Forms.TextBox
    Public WithEvents txtAmendNo As System.Windows.Forms.TextBox
    Public WithEvents txtPODate As System.Windows.Forms.TextBox
    Public WithEvents txtPONo As System.Windows.Forms.TextBox
    Public WithEvents txtRemarks As System.Windows.Forms.TextBox
    Public WithEvents txtSupplierName As System.Windows.Forms.TextBox
    Public WithEvents txtCode As System.Windows.Forms.TextBox
    Public WithEvents cmdsearch As System.Windows.Forms.Button
    Public WithEvents Label32 As System.Windows.Forms.Label
    Public WithEvents Label22 As System.Windows.Forms.Label
    Public WithEvents Label29 As System.Windows.Forms.Label
    Public WithEvents lblDivision As System.Windows.Forms.Label
    Public WithEvents Label28 As System.Windows.Forms.Label
    Public WithEvents Label26 As System.Windows.Forms.Label
    Public WithEvents Label23 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents fraTop1 As System.Windows.Forms.GroupBox
    Public WithEvents cmdUpdateCosting As System.Windows.Forms.Button
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents fraAccounts As System.Windows.Forms.GroupBox
    Public WithEvents SprdExp As AxFPSpreadADO.AxfpSpread
    Public WithEvents lblIGSTValue As System.Windows.Forms.Label
    Public WithEvents Label33 As System.Windows.Forms.Label
    Public WithEvents lblSGSTValue As System.Windows.Forms.Label
    Public WithEvents Label31 As System.Windows.Forms.Label
    Public WithEvents lblTCS As System.Windows.Forms.Label
    Public WithEvents lblTCSPercentage As System.Windows.Forms.Label
    Public WithEvents lblMSC As System.Windows.Forms.Label
    Public WithEvents lblRO As System.Windows.Forms.Label
    Public WithEvents lblSurcharge As System.Windows.Forms.Label
    Public WithEvents lblDiscount As System.Windows.Forms.Label
    Public WithEvents lblTotTaxableAmt As System.Windows.Forms.Label
    Public WithEvents lblSTPercentage As System.Windows.Forms.Label
    Public WithEvents lblEDPercentage As System.Windows.Forms.Label
    Public WithEvents lblTotExpAmt As System.Windows.Forms.Label
    Public WithEvents lblTotFreight As System.Windows.Forms.Label
    Public WithEvents lblTotCharges As System.Windows.Forms.Label
    Public WithEvents Label34 As System.Windows.Forms.Label
    Public WithEvents Label21 As System.Windows.Forms.Label
    Public WithEvents lblCGSTValue As System.Windows.Forms.Label
    Public WithEvents Label20 As System.Windows.Forms.Label
    Public WithEvents lblTotItemValue As System.Windows.Forms.Label
    Public WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents lblNetAmount As System.Windows.Forms.Label
    Public WithEvents lblTotQty As System.Windows.Forms.Label
    Public WithEvents lblTotPackQtyCap As System.Windows.Forms.Label
    Public WithEvents lblTotOtherExp As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents _TabMain_TabPage0 As System.Windows.Forms.TabPage
    Public WithEvents txtRecdDate As System.Windows.Forms.TextBox
    Public WithEvents chkRecdAcct As System.Windows.Forms.CheckBox
    Public WithEvents chkShipTo As System.Windows.Forms.CheckBox
    Public WithEvents cmdSearchShippedTo As System.Windows.Forms.Button
    Public WithEvents txtShippedTo As System.Windows.Forms.TextBox
    Public WithEvents txtServProvided As System.Windows.Forms.TextBox
    Public WithEvents cmdServProvided As System.Windows.Forms.Button
    Public WithEvents txtInsurance As System.Windows.Forms.TextBox
    Public WithEvents txtInspection As System.Windows.Forms.TextBox
    Public WithEvents txtDespMode As System.Windows.Forms.TextBox
    Public WithEvents txtDelivery As System.Windows.Forms.TextBox
    Public WithEvents txtExcise As System.Windows.Forms.TextBox
    Public WithEvents txtPacking As System.Windows.Forms.TextBox
    Public WithEvents txtOthCond2 As System.Windows.Forms.TextBox
    Public WithEvents txtPayment As System.Windows.Forms.TextBox
    Public WithEvents cmdPaySearch As System.Windows.Forms.Button
    Public WithEvents txtPaymentDays As System.Windows.Forms.TextBox
    Public WithEvents Label25 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents lblModDate As System.Windows.Forms.Label
    Public WithEvents Label48 As System.Windows.Forms.Label
    Public WithEvents lblAddDate As System.Windows.Forms.Label
    Public WithEvents Label45 As System.Windows.Forms.Label
    Public WithEvents lblModUser As System.Windows.Forms.Label
    Public WithEvents Label46 As System.Windows.Forms.Label
    Public WithEvents lblAddUser As System.Windows.Forms.Label
    Public WithEvents Label44 As System.Windows.Forms.Label
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents lblDueDays As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents lblPaymentTerms As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents _TabMain_TabPage1 As System.Windows.Forms.TabPage
    Public WithEvents txtAnnexTitle As System.Windows.Forms.TextBox
    Public WithEvents SprdAnnex As AxFPSpreadADO.AxfpSpread
    Public WithEvents Label27 As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents _TabMain_TabPage2 As System.Windows.Forms.TabPage
    Public WithEvents _optPostingDetails_2 As System.Windows.Forms.RadioButton
    Public WithEvents _optPostingDetails_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optPostingDetails_0 As System.Windows.Forms.RadioButton
    Public WithEvents FraPostingDetails As System.Windows.Forms.GroupBox
    Public WithEvents txtOwner As System.Windows.Forms.TextBox
    Public WithEvents cmdOwner As System.Windows.Forms.Button
    Public WithEvents Label30 As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents _TabMain_TabPage3 As System.Windows.Forms.TabPage
    Public WithEvents cmdTPShow As System.Windows.Forms.Button
    Public WithEvents cmdTCShow As System.Windows.Forms.Button
    Public WithEvents chkApprovedWO_TC As System.Windows.Forms.CheckBox
    Public WithEvents chkTPRAvailable As System.Windows.Forms.CheckBox
    Public WithEvents chkTCAvailable As System.Windows.Forms.CheckBox
    Public WithEvents cmdTPRI As System.Windows.Forms.Button
    Public WithEvents txtTPRPath As System.Windows.Forms.TextBox
    Public WithEvents cmdTC As System.Windows.Forms.Button
    Public WithEvents txtTCPath As System.Windows.Forms.TextBox
    Public cdgFilePathOpen As System.Windows.Forms.OpenFileDialog
    Public WithEvents Label36 As System.Windows.Forms.Label
    Public WithEvents Label35 As System.Windows.Forms.Label
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents _TabMain_TabPage4 As System.Windows.Forms.TabPage
    Public WithEvents TabMain As System.Windows.Forms.TabControl
    Public WithEvents FraTrn As System.Windows.Forms.GroupBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents CmdDelete As System.Windows.Forms.Button
    Public WithEvents cmdAnnexPrint As System.Windows.Forms.Button
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents cmdAmend As System.Windows.Forms.Button
    Public WithEvents CmdModify As System.Windows.Forms.Button
    Public WithEvents CmdAdd As System.Windows.Forms.Button
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
    Public WithEvents lblPOType As System.Windows.Forms.Label
    Public WithEvents lblBookType As System.Windows.Forms.Label
    Public WithEvents lblMkey As System.Windows.Forms.Label
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents optPostingDetails As VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPO_GST))
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
        Me.cmdSearchItem = New System.Windows.Forms.Button()
        Me.cmdDivSearch = New System.Windows.Forms.Button()
        Me.cmdSearchAmend = New System.Windows.Forms.Button()
        Me.cmdSearchPrevPO = New System.Windows.Forms.Button()
        Me.cmdSearchPO = New System.Windows.Forms.Button()
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.cmdSearchShippedTo = New System.Windows.Forms.Button()
        Me.txtServProvided = New System.Windows.Forms.TextBox()
        Me.cmdServProvided = New System.Windows.Forms.Button()
        Me.cmdPaySearch = New System.Windows.Forms.Button()
        Me.cmdOwner = New System.Windows.Forms.Button()
        Me.cmdTPRI = New System.Windows.Forms.Button()
        Me.cmdTC = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.cmdAnnexPrint = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.cmdAmend = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.cmdBillToSearch = New System.Windows.Forms.Button()
        Me.cmdShipToSearch = New System.Windows.Forms.Button()
        Me.cmdInterUnitSO = New System.Windows.Forms.Button()
        Me.cmdGetData = New System.Windows.Forms.Button()
        Me.cmdDeliveryToLocSearch = New System.Windows.Forms.Button()
        Me.cmdSearchDeliveryTo = New System.Windows.Forms.Button()
        Me.FraTrn = New System.Windows.Forms.GroupBox()
        Me.fraTop1 = New System.Windows.Forms.GroupBox()
        Me.txtRMRate = New System.Windows.Forms.TextBox()
        Me.lblRMRate = New System.Windows.Forms.Label()
        Me.txtRMQty = New System.Windows.Forms.TextBox()
        Me.lblRMQty = New System.Windows.Forms.Label()
        Me.txtRMDesc = New System.Windows.Forms.TextBox()
        Me.lblRMDesc = New System.Windows.Forms.Label()
        Me.txtOldERPNo = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtBillTo = New System.Windows.Forms.TextBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.chkDevelopment = New System.Windows.Forms.CheckBox()
        Me.chkPrintApp = New System.Windows.Forms.CheckBox()
        Me.cboGSTStatus = New System.Windows.Forms.ComboBox()
        Me.txtSearchItem = New System.Windows.Forms.TextBox()
        Me.chkCapital = New System.Windows.Forms.CheckBox()
        Me.txtDivision = New System.Windows.Forms.TextBox()
        Me.txtIndentNo = New System.Windows.Forms.TextBox()
        Me.ChkPrintAllItem = New System.Windows.Forms.CheckBox()
        Me.TxtExchangeRate = New System.Windows.Forms.TextBox()
        Me.ChkActivate = New System.Windows.Forms.CheckBox()
        Me.txtPrevPONo = New System.Windows.Forms.TextBox()
        Me.txtWEF = New System.Windows.Forms.TextBox()
        Me.chkStatus = New System.Windows.Forms.CheckBox()
        Me.txtAmendDate = New System.Windows.Forms.TextBox()
        Me.txtAmendNo = New System.Windows.Forms.TextBox()
        Me.txtPODate = New System.Windows.Forms.TextBox()
        Me.txtPONo = New System.Windows.Forms.TextBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtSupplierName = New System.Windows.Forms.TextBox()
        Me.txtCode = New System.Windows.Forms.TextBox()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.lblDivision = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TabMain = New System.Windows.Forms.TabControl()
        Me._TabMain_TabPage0 = New System.Windows.Forms.TabPage()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.lblRMPO = New System.Windows.Forms.Label()
        Me.cmdUpdateCosting = New System.Windows.Forms.Button()
        Me.fraAccounts = New System.Windows.Forms.GroupBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.SprdExp = New AxFPSpreadADO.AxfpSpread()
        Me.lblIGSTValue = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.lblSGSTValue = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.lblTCS = New System.Windows.Forms.Label()
        Me.lblTCSPercentage = New System.Windows.Forms.Label()
        Me.lblMSC = New System.Windows.Forms.Label()
        Me.lblRO = New System.Windows.Forms.Label()
        Me.lblSurcharge = New System.Windows.Forms.Label()
        Me.lblDiscount = New System.Windows.Forms.Label()
        Me.lblTotTaxableAmt = New System.Windows.Forms.Label()
        Me.lblSTPercentage = New System.Windows.Forms.Label()
        Me.lblEDPercentage = New System.Windows.Forms.Label()
        Me.lblTotExpAmt = New System.Windows.Forms.Label()
        Me.lblTotFreight = New System.Windows.Forms.Label()
        Me.lblTotCharges = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.lblCGSTValue = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.lblTotItemValue = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.lblNetAmount = New System.Windows.Forms.Label()
        Me.lblTotQty = New System.Windows.Forms.Label()
        Me.lblTotPackQtyCap = New System.Windows.Forms.Label()
        Me.lblTotOtherExp = New System.Windows.Forms.Label()
        Me._TabMain_TabPage1 = New System.Windows.Forms.TabPage()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.TxtDeliveryToLoc = New System.Windows.Forms.TextBox()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.txtDeliveryTo = New System.Windows.Forms.TextBox()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.TxtShipTo = New System.Windows.Forms.TextBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.txtRecdDate = New System.Windows.Forms.TextBox()
        Me.chkRecdAcct = New System.Windows.Forms.CheckBox()
        Me.chkShipTo = New System.Windows.Forms.CheckBox()
        Me.txtShippedTo = New System.Windows.Forms.TextBox()
        Me.txtInsurance = New System.Windows.Forms.TextBox()
        Me.txtInspection = New System.Windows.Forms.TextBox()
        Me.txtDespMode = New System.Windows.Forms.TextBox()
        Me.txtDelivery = New System.Windows.Forms.TextBox()
        Me.txtExcise = New System.Windows.Forms.TextBox()
        Me.txtPacking = New System.Windows.Forms.TextBox()
        Me.txtOthCond2 = New System.Windows.Forms.TextBox()
        Me.txtPayment = New System.Windows.Forms.TextBox()
        Me.txtPaymentDays = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblModDate = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.lblAddDate = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.lblModUser = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.lblAddUser = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblDueDays = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblPaymentTerms = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me._TabMain_TabPage2 = New System.Windows.Forms.TabPage()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.txtAnnexTitle = New System.Windows.Forms.TextBox()
        Me.SprdAnnex = New AxFPSpreadADO.AxfpSpread()
        Me.Label27 = New System.Windows.Forms.Label()
        Me._TabMain_TabPage3 = New System.Windows.Forms.TabPage()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.FraPostingDetails = New System.Windows.Forms.GroupBox()
        Me._optPostingDetails_2 = New System.Windows.Forms.RadioButton()
        Me._optPostingDetails_1 = New System.Windows.Forms.RadioButton()
        Me._optPostingDetails_0 = New System.Windows.Forms.RadioButton()
        Me.txtOwner = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me._TabMain_TabPage4 = New System.Windows.Forms.TabPage()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.cmdTPShow = New System.Windows.Forms.Button()
        Me.cmdTCShow = New System.Windows.Forms.Button()
        Me.chkApprovedWO_TC = New System.Windows.Forms.CheckBox()
        Me.chkTPRAvailable = New System.Windows.Forms.CheckBox()
        Me.chkTCAvailable = New System.Windows.Forms.CheckBox()
        Me.txtTPRPath = New System.Windows.Forms.TextBox()
        Me.txtTCPath = New System.Windows.Forms.TextBox()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.cdgFilePathOpen = New System.Windows.Forms.OpenFileDialog()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.lblPOType = New System.Windows.Forms.Label()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.lblMkey = New System.Windows.Forms.Label()
        Me.optPostingDetails = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.UltraGrid1 = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.FraTrn.SuspendLayout()
        Me.fraTop1.SuspendLayout()
        Me.TabMain.SuspendLayout()
        Me._TabMain_TabPage0.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.fraAccounts.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdExp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._TabMain_TabPage1.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me._TabMain_TabPage2.SuspendLayout()
        Me.Frame2.SuspendLayout()
        CType(Me.SprdAnnex, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._TabMain_TabPage3.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.FraPostingDetails.SuspendLayout()
        Me._TabMain_TabPage4.SuspendLayout()
        Me.Frame4.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.optPostingDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSearchItem
        '
        Me.cmdSearchItem.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchItem.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchItem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchItem.Image = CType(resources.GetObject("cmdSearchItem.Image"), System.Drawing.Image)
        Me.cmdSearchItem.Location = New System.Drawing.Point(642, 111)
        Me.cmdSearchItem.Name = "cmdSearchItem"
        Me.cmdSearchItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchItem.Size = New System.Drawing.Size(28, 23)
        Me.cmdSearchItem.TabIndex = 117
        Me.cmdSearchItem.TabStop = False
        Me.cmdSearchItem.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchItem, "Search")
        Me.cmdSearchItem.UseVisualStyleBackColor = False
        '
        'cmdDivSearch
        '
        Me.cmdDivSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdDivSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDivSearch.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDivSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDivSearch.Image = CType(resources.GetObject("cmdDivSearch.Image"), System.Drawing.Image)
        Me.cmdDivSearch.Location = New System.Drawing.Point(132, 89)
        Me.cmdDivSearch.Name = "cmdDivSearch"
        Me.cmdDivSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDivSearch.Size = New System.Drawing.Size(28, 23)
        Me.cmdDivSearch.TabIndex = 16
        Me.cmdDivSearch.TabStop = False
        Me.cmdDivSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdDivSearch, "Search")
        Me.cmdDivSearch.UseVisualStyleBackColor = False
        '
        'cmdSearchAmend
        '
        Me.cmdSearchAmend.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchAmend.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchAmend.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchAmend.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchAmend.Image = CType(resources.GetObject("cmdSearchAmend.Image"), System.Drawing.Image)
        Me.cmdSearchAmend.Location = New System.Drawing.Point(485, 12)
        Me.cmdSearchAmend.Name = "cmdSearchAmend"
        Me.cmdSearchAmend.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchAmend.Size = New System.Drawing.Size(28, 23)
        Me.cmdSearchAmend.TabIndex = 98
        Me.cmdSearchAmend.TabStop = False
        Me.cmdSearchAmend.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchAmend, "Search")
        Me.cmdSearchAmend.UseVisualStyleBackColor = False
        '
        'cmdSearchPrevPO
        '
        Me.cmdSearchPrevPO.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchPrevPO.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchPrevPO.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchPrevPO.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchPrevPO.Image = CType(resources.GetObject("cmdSearchPrevPO.Image"), System.Drawing.Image)
        Me.cmdSearchPrevPO.Location = New System.Drawing.Point(169, 37)
        Me.cmdSearchPrevPO.Name = "cmdSearchPrevPO"
        Me.cmdSearchPrevPO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchPrevPO.Size = New System.Drawing.Size(28, 23)
        Me.cmdSearchPrevPO.TabIndex = 7
        Me.cmdSearchPrevPO.TabStop = False
        Me.cmdSearchPrevPO.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchPrevPO, "Search")
        Me.cmdSearchPrevPO.UseVisualStyleBackColor = False
        '
        'cmdSearchPO
        '
        Me.cmdSearchPO.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchPO.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchPO.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchPO.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchPO.Image = CType(resources.GetObject("cmdSearchPO.Image"), System.Drawing.Image)
        Me.cmdSearchPO.Location = New System.Drawing.Point(169, 12)
        Me.cmdSearchPO.Name = "cmdSearchPO"
        Me.cmdSearchPO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchPO.Size = New System.Drawing.Size(28, 23)
        Me.cmdSearchPO.TabIndex = 2
        Me.cmdSearchPO.TabStop = False
        Me.cmdSearchPO.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchPO, "Search")
        Me.cmdSearchPO.UseVisualStyleBackColor = False
        '
        'cmdsearch
        '
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.Location = New System.Drawing.Point(481, 62)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(28, 25)
        Me.cmdsearch.TabIndex = 13
        Me.cmdsearch.TabStop = False
        Me.cmdsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearch, "Search")
        Me.cmdsearch.UseVisualStyleBackColor = False
        '
        'cmdSearchShippedTo
        '
        Me.cmdSearchShippedTo.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchShippedTo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchShippedTo.Enabled = False
        Me.cmdSearchShippedTo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchShippedTo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchShippedTo.Image = CType(resources.GetObject("cmdSearchShippedTo.Image"), System.Drawing.Image)
        Me.cmdSearchShippedTo.Location = New System.Drawing.Point(414, 318)
        Me.cmdSearchShippedTo.Name = "cmdSearchShippedTo"
        Me.cmdSearchShippedTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchShippedTo.Size = New System.Drawing.Size(24, 22)
        Me.cmdSearchShippedTo.TabIndex = 128
        Me.cmdSearchShippedTo.TabStop = False
        Me.cmdSearchShippedTo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchShippedTo, "Search")
        Me.cmdSearchShippedTo.UseVisualStyleBackColor = False
        '
        'txtServProvided
        '
        Me.txtServProvided.AcceptsReturn = True
        Me.txtServProvided.BackColor = System.Drawing.SystemColors.Window
        Me.txtServProvided.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtServProvided.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtServProvided.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtServProvided.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtServProvided.Location = New System.Drawing.Point(162, 228)
        Me.txtServProvided.MaxLength = 0
        Me.txtServProvided.Name = "txtServProvided"
        Me.txtServProvided.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtServProvided.Size = New System.Drawing.Size(431, 22)
        Me.txtServProvided.TabIndex = 33
        Me.ToolTip1.SetToolTip(Me.txtServProvided, "Press F1 For Help")
        '
        'cmdServProvided
        '
        Me.cmdServProvided.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdServProvided.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdServProvided.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdServProvided.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdServProvided.Image = CType(resources.GetObject("cmdServProvided.Image"), System.Drawing.Image)
        Me.cmdServProvided.Location = New System.Drawing.Point(593, 227)
        Me.cmdServProvided.Name = "cmdServProvided"
        Me.cmdServProvided.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdServProvided.Size = New System.Drawing.Size(27, 24)
        Me.cmdServProvided.TabIndex = 34
        Me.cmdServProvided.TabStop = False
        Me.cmdServProvided.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdServProvided, "Search")
        Me.cmdServProvided.UseVisualStyleBackColor = False
        '
        'cmdPaySearch
        '
        Me.cmdPaySearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdPaySearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPaySearch.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPaySearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPaySearch.Image = CType(resources.GetObject("cmdPaySearch.Image"), System.Drawing.Image)
        Me.cmdPaySearch.Location = New System.Drawing.Point(208, 200)
        Me.cmdPaySearch.Name = "cmdPaySearch"
        Me.cmdPaySearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPaySearch.Size = New System.Drawing.Size(23, 24)
        Me.cmdPaySearch.TabIndex = 31
        Me.cmdPaySearch.TabStop = False
        Me.cmdPaySearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPaySearch, "Search")
        Me.cmdPaySearch.UseVisualStyleBackColor = False
        '
        'cmdOwner
        '
        Me.cmdOwner.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdOwner.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOwner.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOwner.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOwner.Image = CType(resources.GetObject("cmdOwner.Image"), System.Drawing.Image)
        Me.cmdOwner.Location = New System.Drawing.Point(593, 22)
        Me.cmdOwner.Name = "cmdOwner"
        Me.cmdOwner.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOwner.Size = New System.Drawing.Size(29, 22)
        Me.cmdOwner.TabIndex = 120
        Me.cmdOwner.TabStop = False
        Me.cmdOwner.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdOwner, "Search")
        Me.cmdOwner.UseVisualStyleBackColor = False
        '
        'cmdTPRI
        '
        Me.cmdTPRI.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdTPRI.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdTPRI.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdTPRI.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdTPRI.Image = CType(resources.GetObject("cmdTPRI.Image"), System.Drawing.Image)
        Me.cmdTPRI.Location = New System.Drawing.Point(594, 102)
        Me.cmdTPRI.Name = "cmdTPRI"
        Me.cmdTPRI.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdTPRI.Size = New System.Drawing.Size(27, 22)
        Me.cmdTPRI.TabIndex = 148
        Me.cmdTPRI.TabStop = False
        Me.cmdTPRI.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdTPRI, "Search")
        Me.cmdTPRI.UseVisualStyleBackColor = False
        '
        'cmdTC
        '
        Me.cmdTC.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdTC.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdTC.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdTC.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdTC.Image = CType(resources.GetObject("cmdTC.Image"), System.Drawing.Image)
        Me.cmdTC.Location = New System.Drawing.Point(594, 48)
        Me.cmdTC.Name = "cmdTC"
        Me.cmdTC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdTC.Size = New System.Drawing.Size(27, 22)
        Me.cmdTC.TabIndex = 145
        Me.cmdTC.TabStop = False
        Me.cmdTC.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdTC, "Search")
        Me.cmdTC.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdClose.Location = New System.Drawing.Point(734, 14)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(67, 37)
        Me.CmdClose.TabIndex = 44
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close the Form")
        Me.CmdClose.UseVisualStyleBackColor = True
        '
        'CmdView
        '
        Me.CmdView.BackColor = System.Drawing.SystemColors.Control
        Me.CmdView.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdView.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
        Me.CmdView.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdView.Location = New System.Drawing.Point(668, 14)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(67, 37)
        Me.CmdView.TabIndex = 43
        Me.CmdView.Text = "List &View"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdView, "List View")
        Me.CmdView.UseVisualStyleBackColor = True
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
        Me.CmdPreview.Location = New System.Drawing.Point(602, 14)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.TabIndex = 42
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Preview")
        Me.CmdPreview.UseVisualStyleBackColor = True
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdPrint.Location = New System.Drawing.Point(536, 14)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 41
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print")
        Me.cmdPrint.UseVisualStyleBackColor = True
        '
        'CmdDelete
        '
        Me.CmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.CmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdDelete.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdDelete.Image = CType(resources.GetObject("CmdDelete.Image"), System.Drawing.Image)
        Me.CmdDelete.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdDelete.Location = New System.Drawing.Point(470, 14)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(67, 37)
        Me.CmdDelete.TabIndex = 40
        Me.CmdDelete.Text = "&Delete"
        Me.CmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdDelete, "Delete Record")
        Me.CmdDelete.UseVisualStyleBackColor = True
        '
        'cmdAnnexPrint
        '
        Me.cmdAnnexPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAnnexPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAnnexPrint.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAnnexPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAnnexPrint.Image = CType(resources.GetObject("cmdAnnexPrint.Image"), System.Drawing.Image)
        Me.cmdAnnexPrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdAnnexPrint.Location = New System.Drawing.Point(404, 14)
        Me.cmdAnnexPrint.Name = "cmdAnnexPrint"
        Me.cmdAnnexPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAnnexPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdAnnexPrint.TabIndex = 39
        Me.cmdAnnexPrint.Text = "Annex Print"
        Me.cmdAnnexPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdAnnexPrint, "Save and Print Record")
        Me.cmdAnnexPrint.UseVisualStyleBackColor = True
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdSave.Location = New System.Drawing.Point(273, 14)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(67, 37)
        Me.CmdSave.TabIndex = 38
        Me.CmdSave.Text = "&Save"
        Me.CmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSave, "Save Record")
        Me.CmdSave.UseVisualStyleBackColor = True
        '
        'cmdAmend
        '
        Me.cmdAmend.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAmend.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAmend.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAmend.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAmend.Image = CType(resources.GetObject("cmdAmend.Image"), System.Drawing.Image)
        Me.cmdAmend.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdAmend.Location = New System.Drawing.Point(207, 14)
        Me.cmdAmend.Name = "cmdAmend"
        Me.cmdAmend.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAmend.Size = New System.Drawing.Size(67, 37)
        Me.cmdAmend.TabIndex = 97
        Me.cmdAmend.Text = "A&mendment"
        Me.cmdAmend.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdAmend, "Modify Record")
        Me.cmdAmend.UseVisualStyleBackColor = True
        '
        'CmdModify
        '
        Me.CmdModify.BackColor = System.Drawing.SystemColors.Control
        Me.CmdModify.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdModify.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdModify.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdModify.Image = CType(resources.GetObject("CmdModify.Image"), System.Drawing.Image)
        Me.CmdModify.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdModify.Location = New System.Drawing.Point(141, 14)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(67, 37)
        Me.CmdModify.TabIndex = 37
        Me.CmdModify.Text = "&Modify"
        Me.CmdModify.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdModify, "Modify Record")
        Me.CmdModify.UseVisualStyleBackColor = True
        '
        'CmdAdd
        '
        Me.CmdAdd.BackColor = System.Drawing.SystemColors.Control
        Me.CmdAdd.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdAdd.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdAdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdAdd.Image = CType(resources.GetObject("CmdAdd.Image"), System.Drawing.Image)
        Me.CmdAdd.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdAdd.Location = New System.Drawing.Point(75, 14)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(67, 37)
        Me.CmdAdd.TabIndex = 0
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = True
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdSavePrint.Location = New System.Drawing.Point(338, 14)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdSavePrint.TabIndex = 103
        Me.cmdSavePrint.Text = "Save &&  Print"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save and Print Record")
        Me.cmdSavePrint.UseVisualStyleBackColor = True
        '
        'cmdBillToSearch
        '
        Me.cmdBillToSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdBillToSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdBillToSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdBillToSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdBillToSearch.Image = CType(resources.GetObject("cmdBillToSearch.Image"), System.Drawing.Image)
        Me.cmdBillToSearch.Location = New System.Drawing.Point(846, 61)
        Me.cmdBillToSearch.Name = "cmdBillToSearch"
        Me.cmdBillToSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdBillToSearch.Size = New System.Drawing.Size(28, 23)
        Me.cmdBillToSearch.TabIndex = 144
        Me.cmdBillToSearch.TabStop = False
        Me.cmdBillToSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdBillToSearch, "Search")
        Me.cmdBillToSearch.UseVisualStyleBackColor = False
        '
        'cmdShipToSearch
        '
        Me.cmdShipToSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdShipToSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdShipToSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdShipToSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdShipToSearch.Image = CType(resources.GetObject("cmdShipToSearch.Image"), System.Drawing.Image)
        Me.cmdShipToSearch.Location = New System.Drawing.Point(680, 318)
        Me.cmdShipToSearch.Name = "cmdShipToSearch"
        Me.cmdShipToSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShipToSearch.Size = New System.Drawing.Size(28, 23)
        Me.cmdShipToSearch.TabIndex = 137
        Me.cmdShipToSearch.TabStop = False
        Me.cmdShipToSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShipToSearch, "Search")
        Me.cmdShipToSearch.UseVisualStyleBackColor = False
        '
        'cmdInterUnitSO
        '
        Me.cmdInterUnitSO.AutoSize = True
        Me.cmdInterUnitSO.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdInterUnitSO.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdInterUnitSO.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdInterUnitSO.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdInterUnitSO.Location = New System.Drawing.Point(820, 12)
        Me.cmdInterUnitSO.Name = "cmdInterUnitSO"
        Me.cmdInterUnitSO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdInterUnitSO.Size = New System.Drawing.Size(87, 24)
        Me.cmdInterUnitSO.TabIndex = 147
        Me.cmdInterUnitSO.TabStop = False
        Me.cmdInterUnitSO.Text = "Copy From SO"
        Me.ToolTip1.SetToolTip(Me.cmdInterUnitSO, "Search")
        Me.cmdInterUnitSO.UseVisualStyleBackColor = False
        '
        'cmdGetData
        '
        Me.cmdGetData.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdGetData.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdGetData.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdGetData.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdGetData.Location = New System.Drawing.Point(818, 137)
        Me.cmdGetData.Name = "cmdGetData"
        Me.cmdGetData.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdGetData.Size = New System.Drawing.Size(78, 23)
        Me.cmdGetData.TabIndex = 154
        Me.cmdGetData.TabStop = False
        Me.cmdGetData.Text = "Get Data"
        Me.ToolTip1.SetToolTip(Me.cmdGetData, "Search")
        Me.cmdGetData.UseVisualStyleBackColor = False
        '
        'cmdDeliveryToLocSearch
        '
        Me.cmdDeliveryToLocSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdDeliveryToLocSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDeliveryToLocSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDeliveryToLocSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDeliveryToLocSearch.Image = CType(resources.GetObject("cmdDeliveryToLocSearch.Image"), System.Drawing.Image)
        Me.cmdDeliveryToLocSearch.Location = New System.Drawing.Point(680, 274)
        Me.cmdDeliveryToLocSearch.Name = "cmdDeliveryToLocSearch"
        Me.cmdDeliveryToLocSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDeliveryToLocSearch.Size = New System.Drawing.Size(28, 23)
        Me.cmdDeliveryToLocSearch.TabIndex = 144
        Me.cmdDeliveryToLocSearch.TabStop = False
        Me.cmdDeliveryToLocSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdDeliveryToLocSearch, "Search")
        Me.cmdDeliveryToLocSearch.UseVisualStyleBackColor = False
        '
        'cmdSearchDeliveryTo
        '
        Me.cmdSearchDeliveryTo.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchDeliveryTo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchDeliveryTo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchDeliveryTo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchDeliveryTo.Image = CType(resources.GetObject("cmdSearchDeliveryTo.Image"), System.Drawing.Image)
        Me.cmdSearchDeliveryTo.Location = New System.Drawing.Point(414, 274)
        Me.cmdSearchDeliveryTo.Name = "cmdSearchDeliveryTo"
        Me.cmdSearchDeliveryTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchDeliveryTo.Size = New System.Drawing.Size(24, 22)
        Me.cmdSearchDeliveryTo.TabIndex = 139
        Me.cmdSearchDeliveryTo.TabStop = False
        Me.cmdSearchDeliveryTo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchDeliveryTo, "Search")
        Me.cmdSearchDeliveryTo.UseVisualStyleBackColor = False
        '
        'FraTrn
        '
        Me.FraTrn.BackColor = System.Drawing.SystemColors.Control
        Me.FraTrn.Controls.Add(Me.fraTop1)
        Me.FraTrn.Controls.Add(Me.TabMain)
        Me.FraTrn.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraTrn.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraTrn.Location = New System.Drawing.Point(0, -4)
        Me.FraTrn.Name = "FraTrn"
        Me.FraTrn.Padding = New System.Windows.Forms.Padding(0)
        Me.FraTrn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraTrn.Size = New System.Drawing.Size(909, 570)
        Me.FraTrn.TabIndex = 45
        Me.FraTrn.TabStop = False
        '
        'fraTop1
        '
        Me.fraTop1.BackColor = System.Drawing.SystemColors.Control
        Me.fraTop1.Controls.Add(Me.cmdGetData)
        Me.fraTop1.Controls.Add(Me.txtRMRate)
        Me.fraTop1.Controls.Add(Me.lblRMRate)
        Me.fraTop1.Controls.Add(Me.txtRMQty)
        Me.fraTop1.Controls.Add(Me.lblRMQty)
        Me.fraTop1.Controls.Add(Me.txtRMDesc)
        Me.fraTop1.Controls.Add(Me.lblRMDesc)
        Me.fraTop1.Controls.Add(Me.cmdInterUnitSO)
        Me.fraTop1.Controls.Add(Me.txtOldERPNo)
        Me.fraTop1.Controls.Add(Me.Label24)
        Me.fraTop1.Controls.Add(Me.cmdBillToSearch)
        Me.fraTop1.Controls.Add(Me.txtBillTo)
        Me.fraTop1.Controls.Add(Me.Label37)
        Me.fraTop1.Controls.Add(Me.chkDevelopment)
        Me.fraTop1.Controls.Add(Me.chkPrintApp)
        Me.fraTop1.Controls.Add(Me.cboGSTStatus)
        Me.fraTop1.Controls.Add(Me.cmdSearchItem)
        Me.fraTop1.Controls.Add(Me.txtSearchItem)
        Me.fraTop1.Controls.Add(Me.chkCapital)
        Me.fraTop1.Controls.Add(Me.txtDivision)
        Me.fraTop1.Controls.Add(Me.cmdDivSearch)
        Me.fraTop1.Controls.Add(Me.txtIndentNo)
        Me.fraTop1.Controls.Add(Me.ChkPrintAllItem)
        Me.fraTop1.Controls.Add(Me.TxtExchangeRate)
        Me.fraTop1.Controls.Add(Me.cmdSearchAmend)
        Me.fraTop1.Controls.Add(Me.ChkActivate)
        Me.fraTop1.Controls.Add(Me.txtPrevPONo)
        Me.fraTop1.Controls.Add(Me.cmdSearchPrevPO)
        Me.fraTop1.Controls.Add(Me.cmdSearchPO)
        Me.fraTop1.Controls.Add(Me.txtWEF)
        Me.fraTop1.Controls.Add(Me.chkStatus)
        Me.fraTop1.Controls.Add(Me.txtAmendDate)
        Me.fraTop1.Controls.Add(Me.txtAmendNo)
        Me.fraTop1.Controls.Add(Me.txtPODate)
        Me.fraTop1.Controls.Add(Me.txtPONo)
        Me.fraTop1.Controls.Add(Me.txtRemarks)
        Me.fraTop1.Controls.Add(Me.txtSupplierName)
        Me.fraTop1.Controls.Add(Me.txtCode)
        Me.fraTop1.Controls.Add(Me.cmdsearch)
        Me.fraTop1.Controls.Add(Me.Label32)
        Me.fraTop1.Controls.Add(Me.Label22)
        Me.fraTop1.Controls.Add(Me.Label29)
        Me.fraTop1.Controls.Add(Me.lblDivision)
        Me.fraTop1.Controls.Add(Me.Label28)
        Me.fraTop1.Controls.Add(Me.Label26)
        Me.fraTop1.Controls.Add(Me.Label23)
        Me.fraTop1.Controls.Add(Me.Label11)
        Me.fraTop1.Controls.Add(Me.Label10)
        Me.fraTop1.Controls.Add(Me.Label9)
        Me.fraTop1.Controls.Add(Me.Label8)
        Me.fraTop1.Controls.Add(Me.Label7)
        Me.fraTop1.Controls.Add(Me.Label6)
        Me.fraTop1.Controls.Add(Me.Label2)
        Me.fraTop1.Controls.Add(Me.Label4)
        Me.fraTop1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTop1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraTop1.Location = New System.Drawing.Point(1, 0)
        Me.fraTop1.Name = "fraTop1"
        Me.fraTop1.Padding = New System.Windows.Forms.Padding(0)
        Me.fraTop1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraTop1.Size = New System.Drawing.Size(907, 166)
        Me.fraTop1.TabIndex = 46
        Me.fraTop1.TabStop = False
        '
        'txtRMRate
        '
        Me.txtRMRate.AcceptsReturn = True
        Me.txtRMRate.BackColor = System.Drawing.SystemColors.Window
        Me.txtRMRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRMRate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRMRate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRMRate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtRMRate.Location = New System.Drawing.Point(740, 138)
        Me.txtRMRate.MaxLength = 0
        Me.txtRMRate.Name = "txtRMRate"
        Me.txtRMRate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRMRate.Size = New System.Drawing.Size(72, 22)
        Me.txtRMRate.TabIndex = 152
        Me.txtRMRate.Visible = False
        '
        'lblRMRate
        '
        Me.lblRMRate.AutoSize = True
        Me.lblRMRate.BackColor = System.Drawing.SystemColors.Control
        Me.lblRMRate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblRMRate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRMRate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRMRate.Location = New System.Drawing.Point(678, 142)
        Me.lblRMRate.Name = "lblRMRate"
        Me.lblRMRate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblRMRate.Size = New System.Drawing.Size(56, 13)
        Me.lblRMRate.TabIndex = 153
        Me.lblRMRate.Text = "RM Rate :"
        Me.lblRMRate.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblRMRate.Visible = False
        '
        'txtRMQty
        '
        Me.txtRMQty.AcceptsReturn = True
        Me.txtRMQty.BackColor = System.Drawing.SystemColors.Window
        Me.txtRMQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRMQty.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRMQty.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRMQty.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtRMQty.Location = New System.Drawing.Point(569, 138)
        Me.txtRMQty.MaxLength = 0
        Me.txtRMQty.Name = "txtRMQty"
        Me.txtRMQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRMQty.Size = New System.Drawing.Size(72, 22)
        Me.txtRMQty.TabIndex = 150
        Me.txtRMQty.Visible = False
        '
        'lblRMQty
        '
        Me.lblRMQty.AutoSize = True
        Me.lblRMQty.BackColor = System.Drawing.SystemColors.Control
        Me.lblRMQty.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblRMQty.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRMQty.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRMQty.Location = New System.Drawing.Point(514, 142)
        Me.lblRMQty.Name = "lblRMQty"
        Me.lblRMQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblRMQty.Size = New System.Drawing.Size(51, 13)
        Me.lblRMQty.TabIndex = 151
        Me.lblRMQty.Text = "RM Qty :"
        Me.lblRMQty.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblRMQty.Visible = False
        '
        'txtRMDesc
        '
        Me.txtRMDesc.AcceptsReturn = True
        Me.txtRMDesc.BackColor = System.Drawing.SystemColors.Window
        Me.txtRMDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRMDesc.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRMDesc.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRMDesc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtRMDesc.Location = New System.Drawing.Point(74, 138)
        Me.txtRMDesc.MaxLength = 0
        Me.txtRMDesc.Name = "txtRMDesc"
        Me.txtRMDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRMDesc.Size = New System.Drawing.Size(408, 22)
        Me.txtRMDesc.TabIndex = 148
        Me.txtRMDesc.Visible = False
        '
        'lblRMDesc
        '
        Me.lblRMDesc.AutoSize = True
        Me.lblRMDesc.BackColor = System.Drawing.SystemColors.Control
        Me.lblRMDesc.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblRMDesc.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRMDesc.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRMDesc.Location = New System.Drawing.Point(14, 142)
        Me.lblRMDesc.Name = "lblRMDesc"
        Me.lblRMDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblRMDesc.Size = New System.Drawing.Size(57, 13)
        Me.lblRMDesc.TabIndex = 149
        Me.lblRMDesc.Text = "RM Desc :"
        Me.lblRMDesc.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblRMDesc.Visible = False
        '
        'txtOldERPNo
        '
        Me.txtOldERPNo.AcceptsReturn = True
        Me.txtOldERPNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtOldERPNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOldERPNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOldERPNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOldERPNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtOldERPNo.Location = New System.Drawing.Point(439, 37)
        Me.txtOldERPNo.MaxLength = 0
        Me.txtOldERPNo.Name = "txtOldERPNo"
        Me.txtOldERPNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOldERPNo.Size = New System.Drawing.Size(42, 22)
        Me.txtOldERPNo.TabIndex = 9
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.SystemColors.Control
        Me.Label24.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label24.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label24.Location = New System.Drawing.Point(364, 41)
        Me.Label24.Name = "Label24"
        Me.Label24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label24.Size = New System.Drawing.Size(71, 13)
        Me.Label24.TabIndex = 146
        Me.Label24.Text = "Old ERP No :"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtBillTo
        '
        Me.txtBillTo.AcceptsReturn = True
        Me.txtBillTo.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillTo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillTo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillTo.ForeColor = System.Drawing.Color.Blue
        Me.txtBillTo.Location = New System.Drawing.Point(748, 62)
        Me.txtBillTo.MaxLength = 0
        Me.txtBillTo.Name = "txtBillTo"
        Me.txtBillTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillTo.Size = New System.Drawing.Size(96, 22)
        Me.txtBillTo.TabIndex = 142
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.BackColor = System.Drawing.SystemColors.Control
        Me.Label37.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label37.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label37.Location = New System.Drawing.Point(642, 67)
        Me.Label37.Name = "Label37"
        Me.Label37.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label37.Size = New System.Drawing.Size(105, 13)
        Me.Label37.TabIndex = 143
        Me.Label37.Text = "Bill From Location :"
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'chkDevelopment
        '
        Me.chkDevelopment.AutoSize = True
        Me.chkDevelopment.BackColor = System.Drawing.SystemColors.Control
        Me.chkDevelopment.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkDevelopment.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDevelopment.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkDevelopment.Location = New System.Drawing.Point(785, 115)
        Me.chkDevelopment.Name = "chkDevelopment"
        Me.chkDevelopment.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkDevelopment.Size = New System.Drawing.Size(115, 17)
        Me.chkDevelopment.TabIndex = 141
        Me.chkDevelopment.Text = "Development PO "
        Me.chkDevelopment.UseVisualStyleBackColor = False
        '
        'chkPrintApp
        '
        Me.chkPrintApp.AutoSize = True
        Me.chkPrintApp.BackColor = System.Drawing.SystemColors.Control
        Me.chkPrintApp.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPrintApp.Enabled = False
        Me.chkPrintApp.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPrintApp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPrintApp.Location = New System.Drawing.Point(785, 87)
        Me.chkPrintApp.Name = "chkPrintApp"
        Me.chkPrintApp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPrintApp.Size = New System.Drawing.Size(103, 17)
        Me.chkPrintApp.TabIndex = 140
        Me.chkPrintApp.Text = "Print Approved"
        Me.chkPrintApp.UseVisualStyleBackColor = False
        '
        'cboGSTStatus
        '
        Me.cboGSTStatus.BackColor = System.Drawing.SystemColors.Window
        Me.cboGSTStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboGSTStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboGSTStatus.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboGSTStatus.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboGSTStatus.Location = New System.Drawing.Point(725, 39)
        Me.cboGSTStatus.Name = "cboGSTStatus"
        Me.cboGSTStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboGSTStatus.Size = New System.Drawing.Size(175, 21)
        Me.cboGSTStatus.TabIndex = 10
        '
        'txtSearchItem
        '
        Me.txtSearchItem.AcceptsReturn = True
        Me.txtSearchItem.BackColor = System.Drawing.SystemColors.Window
        Me.txtSearchItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearchItem.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSearchItem.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearchItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSearchItem.Location = New System.Drawing.Point(569, 112)
        Me.txtSearchItem.MaxLength = 0
        Me.txtSearchItem.Name = "txtSearchItem"
        Me.txtSearchItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSearchItem.Size = New System.Drawing.Size(72, 22)
        Me.txtSearchItem.TabIndex = 116
        '
        'chkCapital
        '
        Me.chkCapital.AutoSize = True
        Me.chkCapital.BackColor = System.Drawing.SystemColors.Control
        Me.chkCapital.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCapital.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCapital.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCapital.Location = New System.Drawing.Point(675, 115)
        Me.chkCapital.Name = "chkCapital"
        Me.chkCapital.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCapital.Size = New System.Drawing.Size(79, 17)
        Me.chkCapital.TabIndex = 20
        Me.chkCapital.Text = "Capital PO"
        Me.chkCapital.UseVisualStyleBackColor = False
        '
        'txtDivision
        '
        Me.txtDivision.AcceptsReturn = True
        Me.txtDivision.BackColor = System.Drawing.SystemColors.Window
        Me.txtDivision.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDivision.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDivision.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDivision.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDivision.Location = New System.Drawing.Point(74, 89)
        Me.txtDivision.MaxLength = 0
        Me.txtDivision.Name = "txtDivision"
        Me.txtDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDivision.Size = New System.Drawing.Size(55, 22)
        Me.txtDivision.TabIndex = 15
        '
        'txtIndentNo
        '
        Me.txtIndentNo.AcceptsReturn = True
        Me.txtIndentNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtIndentNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIndentNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIndentNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIndentNo.ForeColor = System.Drawing.Color.Blue
        Me.txtIndentNo.Location = New System.Drawing.Point(569, 88)
        Me.txtIndentNo.MaxLength = 0
        Me.txtIndentNo.Name = "txtIndentNo"
        Me.txtIndentNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIndentNo.Size = New System.Drawing.Size(72, 22)
        Me.txtIndentNo.TabIndex = 18
        '
        'ChkPrintAllItem
        '
        Me.ChkPrintAllItem.BackColor = System.Drawing.SystemColors.Control
        Me.ChkPrintAllItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkPrintAllItem.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkPrintAllItem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkPrintAllItem.Location = New System.Drawing.Point(645, 16)
        Me.ChkPrintAllItem.Name = "ChkPrintAllItem"
        Me.ChkPrintAllItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkPrintAllItem.Size = New System.Drawing.Size(91, 16)
        Me.ChkPrintAllItem.TabIndex = 101
        Me.ChkPrintAllItem.Text = "Print All Item"
        Me.ChkPrintAllItem.UseVisualStyleBackColor = False
        '
        'TxtExchangeRate
        '
        Me.TxtExchangeRate.AcceptsReturn = True
        Me.TxtExchangeRate.BackColor = System.Drawing.SystemColors.Window
        Me.TxtExchangeRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtExchangeRate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtExchangeRate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtExchangeRate.ForeColor = System.Drawing.Color.Blue
        Me.TxtExchangeRate.Location = New System.Drawing.Point(569, 37)
        Me.TxtExchangeRate.MaxLength = 0
        Me.TxtExchangeRate.Name = "TxtExchangeRate"
        Me.TxtExchangeRate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtExchangeRate.Size = New System.Drawing.Size(72, 22)
        Me.TxtExchangeRate.TabIndex = 9
        '
        'ChkActivate
        '
        Me.ChkActivate.AutoSize = True
        Me.ChkActivate.BackColor = System.Drawing.SystemColors.Control
        Me.ChkActivate.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkActivate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkActivate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkActivate.Location = New System.Drawing.Point(647, 90)
        Me.ChkActivate.Name = "ChkActivate"
        Me.ChkActivate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkActivate.Size = New System.Drawing.Size(121, 17)
        Me.ChkActivate.TabIndex = 11
        Me.ChkActivate.Text = "Closed Staus (Y/N)"
        Me.ChkActivate.UseVisualStyleBackColor = False
        '
        'txtPrevPONo
        '
        Me.txtPrevPONo.AcceptsReturn = True
        Me.txtPrevPONo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPrevPONo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPrevPONo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPrevPONo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPrevPONo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPrevPONo.Location = New System.Drawing.Point(74, 37)
        Me.txtPrevPONo.MaxLength = 0
        Me.txtPrevPONo.Name = "txtPrevPONo"
        Me.txtPrevPONo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPrevPONo.Size = New System.Drawing.Size(94, 22)
        Me.txtPrevPONo.TabIndex = 6
        '
        'txtWEF
        '
        Me.txtWEF.AcceptsReturn = True
        Me.txtWEF.BackColor = System.Drawing.SystemColors.Window
        Me.txtWEF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWEF.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWEF.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWEF.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtWEF.Location = New System.Drawing.Point(280, 37)
        Me.txtWEF.MaxLength = 0
        Me.txtWEF.Name = "txtWEF"
        Me.txtWEF.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWEF.Size = New System.Drawing.Size(80, 22)
        Me.txtWEF.TabIndex = 8
        '
        'chkStatus
        '
        Me.chkStatus.BackColor = System.Drawing.Color.LimeGreen
        Me.chkStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkStatus.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkStatus.ForeColor = System.Drawing.Color.Maroon
        Me.chkStatus.Location = New System.Drawing.Point(740, 16)
        Me.chkStatus.Name = "chkStatus"
        Me.chkStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkStatus.Size = New System.Drawing.Size(85, 17)
        Me.chkStatus.TabIndex = 19
        Me.chkStatus.Text = "Post Status (Posted/ Unposted)"
        Me.chkStatus.UseVisualStyleBackColor = False
        '
        'txtAmendDate
        '
        Me.txtAmendDate.AcceptsReturn = True
        Me.txtAmendDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtAmendDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmendDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAmendDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmendDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtAmendDate.Location = New System.Drawing.Point(569, 13)
        Me.txtAmendDate.MaxLength = 0
        Me.txtAmendDate.Name = "txtAmendDate"
        Me.txtAmendDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAmendDate.Size = New System.Drawing.Size(72, 22)
        Me.txtAmendDate.TabIndex = 5
        '
        'txtAmendNo
        '
        Me.txtAmendNo.AcceptsReturn = True
        Me.txtAmendNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtAmendNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmendNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAmendNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmendNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtAmendNo.Location = New System.Drawing.Point(440, 13)
        Me.txtAmendNo.MaxLength = 0
        Me.txtAmendNo.Name = "txtAmendNo"
        Me.txtAmendNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAmendNo.Size = New System.Drawing.Size(42, 22)
        Me.txtAmendNo.TabIndex = 4
        '
        'txtPODate
        '
        Me.txtPODate.AcceptsReturn = True
        Me.txtPODate.BackColor = System.Drawing.SystemColors.Window
        Me.txtPODate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPODate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPODate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPODate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPODate.Location = New System.Drawing.Point(279, 13)
        Me.txtPODate.MaxLength = 0
        Me.txtPODate.Name = "txtPODate"
        Me.txtPODate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPODate.Size = New System.Drawing.Size(80, 22)
        Me.txtPODate.TabIndex = 3
        '
        'txtPONo
        '
        Me.txtPONo.AcceptsReturn = True
        Me.txtPONo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPONo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPONo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPONo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPONo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPONo.Location = New System.Drawing.Point(74, 13)
        Me.txtPONo.MaxLength = 0
        Me.txtPONo.Name = "txtPONo"
        Me.txtPONo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPONo.Size = New System.Drawing.Size(94, 22)
        Me.txtPONo.TabIndex = 1
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtRemarks.Location = New System.Drawing.Point(74, 114)
        Me.txtRemarks.MaxLength = 0
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.Size = New System.Drawing.Size(408, 22)
        Me.txtRemarks.TabIndex = 17
        '
        'txtSupplierName
        '
        Me.txtSupplierName.AcceptsReturn = True
        Me.txtSupplierName.BackColor = System.Drawing.SystemColors.Window
        Me.txtSupplierName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplierName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSupplierName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplierName.ForeColor = System.Drawing.Color.Blue
        Me.txtSupplierName.Location = New System.Drawing.Point(74, 64)
        Me.txtSupplierName.MaxLength = 0
        Me.txtSupplierName.Name = "txtSupplierName"
        Me.txtSupplierName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSupplierName.Size = New System.Drawing.Size(406, 22)
        Me.txtSupplierName.TabIndex = 12
        '
        'txtCode
        '
        Me.txtCode.AcceptsReturn = True
        Me.txtCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCode.ForeColor = System.Drawing.Color.Blue
        Me.txtCode.Location = New System.Drawing.Point(569, 63)
        Me.txtCode.MaxLength = 0
        Me.txtCode.Name = "txtCode"
        Me.txtCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCode.Size = New System.Drawing.Size(72, 22)
        Me.txtCode.TabIndex = 14
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.BackColor = System.Drawing.SystemColors.Control
        Me.Label32.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label32.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label32.Location = New System.Drawing.Point(655, 41)
        Me.Label32.Name = "Label32"
        Me.Label32.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label32.Size = New System.Drawing.Size(67, 13)
        Me.Label32.TabIndex = 139
        Me.Label32.Text = "GST Status :"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.SystemColors.Control
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(493, 118)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(72, 13)
        Me.Label22.TabIndex = 118
        Me.Label22.Text = "Search Item :"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.SystemColors.Control
        Me.Label29.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label29.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label29.Location = New System.Drawing.Point(14, 92)
        Me.Label29.Name = "Label29"
        Me.Label29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label29.Size = New System.Drawing.Size(54, 13)
        Me.Label29.TabIndex = 114
        Me.Label29.Text = "Division :"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblDivision
        '
        Me.lblDivision.BackColor = System.Drawing.Color.Transparent
        Me.lblDivision.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDivision.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDivision.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDivision.Location = New System.Drawing.Point(161, 92)
        Me.lblDivision.Name = "lblDivision"
        Me.lblDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDivision.Size = New System.Drawing.Size(320, 19)
        Me.lblDivision.TabIndex = 113
        Me.lblDivision.Text = "lblDivision"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.SystemColors.Control
        Me.Label28.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label28.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label28.Location = New System.Drawing.Point(489, 92)
        Me.Label28.Name = "Label28"
        Me.Label28.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label28.Size = New System.Drawing.Size(75, 13)
        Me.Label28.TabIndex = 104
        Me.Label28.Text = "From Indent :"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.SystemColors.Control
        Me.Label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label26.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(481, 43)
        Me.Label26.Name = "Label26"
        Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label26.Size = New System.Drawing.Size(87, 13)
        Me.Label26.TabIndex = 99
        Me.Label26.Text = "Exchange Rate :"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.SystemColors.Control
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(18, 41)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(55, 13)
        Me.Label23.TabIndex = 96
        Me.Label23.Text = "Prev. No :"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(202, 42)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(76, 13)
        Me.Label11.TabIndex = 56
        Me.Label11.Text = "Amend w.e.f :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(532, 16)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(37, 13)
        Me.Label10.TabIndex = 55
        Me.Label10.Text = "Date :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(365, 17)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(70, 13)
        Me.Label9.TabIndex = 54
        Me.Label9.Text = "Amend No. :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(239, 17)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(37, 13)
        Me.Label8.TabIndex = 53
        Me.Label8.Text = "Date :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(14, 14)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(58, 17)
        Me.Label7.TabIndex = 52
        Me.Label7.Text = "Number :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(14, 120)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(57, 13)
        Me.Label6.TabIndex = 51
        Me.Label6.Text = "Remarks :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(32, 67)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(39, 13)
        Me.Label2.TabIndex = 47
        Me.Label2.Text = "Party :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(527, 67)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(40, 13)
        Me.Label4.TabIndex = 48
        Me.Label4.Text = "Code :"
        '
        'TabMain
        '
        Me.TabMain.Controls.Add(Me._TabMain_TabPage0)
        Me.TabMain.Controls.Add(Me._TabMain_TabPage1)
        Me.TabMain.Controls.Add(Me._TabMain_TabPage2)
        Me.TabMain.Controls.Add(Me._TabMain_TabPage3)
        Me.TabMain.Controls.Add(Me._TabMain_TabPage4)
        Me.TabMain.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabMain.ItemSize = New System.Drawing.Size(42, 18)
        Me.TabMain.Location = New System.Drawing.Point(0, 168)
        Me.TabMain.Name = "TabMain"
        Me.TabMain.SelectedIndex = 4
        Me.TabMain.Size = New System.Drawing.Size(908, 400)
        Me.TabMain.TabIndex = 59
        '
        '_TabMain_TabPage0
        '
        Me._TabMain_TabPage0.Controls.Add(Me.Frame1)
        Me._TabMain_TabPage0.Location = New System.Drawing.Point(4, 22)
        Me._TabMain_TabPage0.Name = "_TabMain_TabPage0"
        Me._TabMain_TabPage0.Size = New System.Drawing.Size(900, 374)
        Me._TabMain_TabPage0.TabIndex = 0
        Me._TabMain_TabPage0.Text = "Item Details"
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.lblRMPO)
        Me.Frame1.Controls.Add(Me.cmdUpdateCosting)
        Me.Frame1.Controls.Add(Me.fraAccounts)
        Me.Frame1.Controls.Add(Me.SprdExp)
        Me.Frame1.Controls.Add(Me.lblIGSTValue)
        Me.Frame1.Controls.Add(Me.Label33)
        Me.Frame1.Controls.Add(Me.lblSGSTValue)
        Me.Frame1.Controls.Add(Me.Label31)
        Me.Frame1.Controls.Add(Me.lblTCS)
        Me.Frame1.Controls.Add(Me.lblTCSPercentage)
        Me.Frame1.Controls.Add(Me.lblMSC)
        Me.Frame1.Controls.Add(Me.lblRO)
        Me.Frame1.Controls.Add(Me.lblSurcharge)
        Me.Frame1.Controls.Add(Me.lblDiscount)
        Me.Frame1.Controls.Add(Me.lblTotTaxableAmt)
        Me.Frame1.Controls.Add(Me.lblSTPercentage)
        Me.Frame1.Controls.Add(Me.lblEDPercentage)
        Me.Frame1.Controls.Add(Me.lblTotExpAmt)
        Me.Frame1.Controls.Add(Me.lblTotFreight)
        Me.Frame1.Controls.Add(Me.lblTotCharges)
        Me.Frame1.Controls.Add(Me.Label34)
        Me.Frame1.Controls.Add(Me.Label21)
        Me.Frame1.Controls.Add(Me.lblCGSTValue)
        Me.Frame1.Controls.Add(Me.Label20)
        Me.Frame1.Controls.Add(Me.lblTotItemValue)
        Me.Frame1.Controls.Add(Me.Label19)
        Me.Frame1.Controls.Add(Me.lblNetAmount)
        Me.Frame1.Controls.Add(Me.lblTotQty)
        Me.Frame1.Controls.Add(Me.lblTotPackQtyCap)
        Me.Frame1.Controls.Add(Me.lblTotOtherExp)
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(1, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(899, 372)
        Me.Frame1.TabIndex = 60
        Me.Frame1.TabStop = False
        '
        'lblRMPO
        '
        Me.lblRMPO.AutoSize = True
        Me.lblRMPO.Location = New System.Drawing.Point(504, 320)
        Me.lblRMPO.Name = "lblRMPO"
        Me.lblRMPO.Size = New System.Drawing.Size(51, 13)
        Me.lblRMPO.TabIndex = 143
        Me.lblRMPO.Text = "lblRMPO"
        Me.lblRMPO.Visible = False
        '
        'cmdUpdateCosting
        '
        Me.cmdUpdateCosting.BackColor = System.Drawing.SystemColors.Control
        Me.cmdUpdateCosting.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdUpdateCosting.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdUpdateCosting.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdUpdateCosting.Location = New System.Drawing.Point(590, 278)
        Me.cmdUpdateCosting.Name = "cmdUpdateCosting"
        Me.cmdUpdateCosting.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdUpdateCosting.Size = New System.Drawing.Size(99, 38)
        Me.cmdUpdateCosting.TabIndex = 142
        Me.cmdUpdateCosting.Text = "Update from Costing"
        Me.cmdUpdateCosting.UseVisualStyleBackColor = False
        '
        'fraAccounts
        '
        Me.fraAccounts.BackColor = System.Drawing.SystemColors.Control
        Me.fraAccounts.Controls.Add(Me.SprdMain)
        Me.fraAccounts.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraAccounts.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraAccounts.Location = New System.Drawing.Point(-1, -3)
        Me.fraAccounts.Name = "fraAccounts"
        Me.fraAccounts.Padding = New System.Windows.Forms.Padding(0)
        Me.fraAccounts.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraAccounts.Size = New System.Drawing.Size(901, 243)
        Me.fraAccounts.TabIndex = 83
        Me.fraAccounts.TabStop = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(1, 10)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(901, 228)
        Me.SprdMain.TabIndex = 21
        '
        'SprdExp
        '
        Me.SprdExp.DataSource = Nothing
        Me.SprdExp.Location = New System.Drawing.Point(0, 243)
        Me.SprdExp.Name = "SprdExp"
        Me.SprdExp.OcxState = CType(resources.GetObject("SprdExp.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdExp.Size = New System.Drawing.Size(406, 125)
        Me.SprdExp.TabIndex = 22
        '
        'lblIGSTValue
        '
        Me.lblIGSTValue.BackColor = System.Drawing.SystemColors.Control
        Me.lblIGSTValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblIGSTValue.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblIGSTValue.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIGSTValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblIGSTValue.Location = New System.Drawing.Point(798, 308)
        Me.lblIGSTValue.Name = "lblIGSTValue"
        Me.lblIGSTValue.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblIGSTValue.Size = New System.Drawing.Size(99, 19)
        Me.lblIGSTValue.TabIndex = 138
        Me.lblIGSTValue.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.BackColor = System.Drawing.SystemColors.Control
        Me.Label33.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label33.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label33.Location = New System.Drawing.Point(730, 312)
        Me.Label33.Name = "Label33"
        Me.Label33.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label33.Size = New System.Drawing.Size(61, 13)
        Me.Label33.TabIndex = 137
        Me.Label33.Text = "Total IGST:"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblSGSTValue
        '
        Me.lblSGSTValue.BackColor = System.Drawing.SystemColors.Control
        Me.lblSGSTValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSGSTValue.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSGSTValue.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSGSTValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSGSTValue.Location = New System.Drawing.Point(798, 288)
        Me.lblSGSTValue.Name = "lblSGSTValue"
        Me.lblSGSTValue.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSGSTValue.Size = New System.Drawing.Size(99, 19)
        Me.lblSGSTValue.TabIndex = 136
        Me.lblSGSTValue.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.BackColor = System.Drawing.SystemColors.Control
        Me.Label31.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label31.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label31.Location = New System.Drawing.Point(726, 292)
        Me.Label31.Name = "Label31"
        Me.Label31.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label31.Size = New System.Drawing.Size(64, 13)
        Me.Label31.TabIndex = 135
        Me.Label31.Text = "Total SGST:"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTCS
        '
        Me.lblTCS.BackColor = System.Drawing.SystemColors.Control
        Me.lblTCS.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTCS.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTCS.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTCS.Location = New System.Drawing.Point(412, 282)
        Me.lblTCS.Name = "lblTCS"
        Me.lblTCS.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTCS.Size = New System.Drawing.Size(43, 15)
        Me.lblTCS.TabIndex = 82
        Me.lblTCS.Text = "lblTCS"
        Me.lblTCS.Visible = False
        '
        'lblTCSPercentage
        '
        Me.lblTCSPercentage.BackColor = System.Drawing.SystemColors.Control
        Me.lblTCSPercentage.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTCSPercentage.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTCSPercentage.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTCSPercentage.Location = New System.Drawing.Point(410, 342)
        Me.lblTCSPercentage.Name = "lblTCSPercentage"
        Me.lblTCSPercentage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTCSPercentage.Size = New System.Drawing.Size(39, 13)
        Me.lblTCSPercentage.TabIndex = 81
        Me.lblTCSPercentage.Text = "lblTCSPercentage"
        Me.lblTCSPercentage.Visible = False
        '
        'lblMSC
        '
        Me.lblMSC.BackColor = System.Drawing.SystemColors.Control
        Me.lblMSC.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMSC.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMSC.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMSC.Location = New System.Drawing.Point(304, 240)
        Me.lblMSC.Name = "lblMSC"
        Me.lblMSC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMSC.Size = New System.Drawing.Size(49, 11)
        Me.lblMSC.TabIndex = 80
        Me.lblMSC.Text = "lblMSC"
        Me.lblMSC.Visible = False
        '
        'lblRO
        '
        Me.lblRO.BackColor = System.Drawing.SystemColors.Control
        Me.lblRO.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblRO.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRO.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRO.Location = New System.Drawing.Point(304, 228)
        Me.lblRO.Name = "lblRO"
        Me.lblRO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblRO.Size = New System.Drawing.Size(41, 11)
        Me.lblRO.TabIndex = 79
        Me.lblRO.Text = "lblRO"
        Me.lblRO.Visible = False
        '
        'lblSurcharge
        '
        Me.lblSurcharge.BackColor = System.Drawing.SystemColors.Control
        Me.lblSurcharge.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSurcharge.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSurcharge.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSurcharge.Location = New System.Drawing.Point(304, 212)
        Me.lblSurcharge.Name = "lblSurcharge"
        Me.lblSurcharge.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSurcharge.Size = New System.Drawing.Size(47, 11)
        Me.lblSurcharge.TabIndex = 78
        Me.lblSurcharge.Text = "lblSurcharge"
        Me.lblSurcharge.Visible = False
        '
        'lblDiscount
        '
        Me.lblDiscount.BackColor = System.Drawing.SystemColors.Control
        Me.lblDiscount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDiscount.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDiscount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDiscount.Location = New System.Drawing.Point(304, 196)
        Me.lblDiscount.Name = "lblDiscount"
        Me.lblDiscount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDiscount.Size = New System.Drawing.Size(59, 11)
        Me.lblDiscount.TabIndex = 77
        Me.lblDiscount.Text = "lblDiscount"
        Me.lblDiscount.Visible = False
        '
        'lblTotTaxableAmt
        '
        Me.lblTotTaxableAmt.AutoSize = True
        Me.lblTotTaxableAmt.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotTaxableAmt.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotTaxableAmt.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotTaxableAmt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTotTaxableAmt.Location = New System.Drawing.Point(470, 278)
        Me.lblTotTaxableAmt.Name = "lblTotTaxableAmt"
        Me.lblTotTaxableAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotTaxableAmt.Size = New System.Drawing.Size(13, 13)
        Me.lblTotTaxableAmt.TabIndex = 76
        Me.lblTotTaxableAmt.Text = "0"
        Me.lblTotTaxableAmt.Visible = False
        '
        'lblSTPercentage
        '
        Me.lblSTPercentage.AutoSize = True
        Me.lblSTPercentage.BackColor = System.Drawing.SystemColors.Control
        Me.lblSTPercentage.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSTPercentage.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSTPercentage.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSTPercentage.Location = New System.Drawing.Point(384, 328)
        Me.lblSTPercentage.Name = "lblSTPercentage"
        Me.lblSTPercentage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSTPercentage.Size = New System.Drawing.Size(13, 13)
        Me.lblSTPercentage.TabIndex = 75
        Me.lblSTPercentage.Text = "0"
        Me.lblSTPercentage.Visible = False
        '
        'lblEDPercentage
        '
        Me.lblEDPercentage.AutoSize = True
        Me.lblEDPercentage.BackColor = System.Drawing.SystemColors.Control
        Me.lblEDPercentage.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblEDPercentage.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEDPercentage.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblEDPercentage.Location = New System.Drawing.Point(460, 232)
        Me.lblEDPercentage.Name = "lblEDPercentage"
        Me.lblEDPercentage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblEDPercentage.Size = New System.Drawing.Size(13, 13)
        Me.lblEDPercentage.TabIndex = 74
        Me.lblEDPercentage.Text = "0"
        Me.lblEDPercentage.Visible = False
        '
        'lblTotExpAmt
        '
        Me.lblTotExpAmt.AutoSize = True
        Me.lblTotExpAmt.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotExpAmt.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotExpAmt.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotExpAmt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTotExpAmt.Location = New System.Drawing.Point(380, 294)
        Me.lblTotExpAmt.Name = "lblTotExpAmt"
        Me.lblTotExpAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotExpAmt.Size = New System.Drawing.Size(13, 13)
        Me.lblTotExpAmt.TabIndex = 73
        Me.lblTotExpAmt.Text = "0"
        Me.lblTotExpAmt.Visible = False
        '
        'lblTotFreight
        '
        Me.lblTotFreight.AutoSize = True
        Me.lblTotFreight.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotFreight.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotFreight.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotFreight.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTotFreight.Location = New System.Drawing.Point(460, 284)
        Me.lblTotFreight.Name = "lblTotFreight"
        Me.lblTotFreight.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotFreight.Size = New System.Drawing.Size(13, 13)
        Me.lblTotFreight.TabIndex = 72
        Me.lblTotFreight.Text = "0"
        Me.lblTotFreight.Visible = False
        '
        'lblTotCharges
        '
        Me.lblTotCharges.AutoSize = True
        Me.lblTotCharges.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotCharges.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotCharges.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotCharges.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTotCharges.Location = New System.Drawing.Point(362, 332)
        Me.lblTotCharges.Name = "lblTotCharges"
        Me.lblTotCharges.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotCharges.Size = New System.Drawing.Size(13, 13)
        Me.lblTotCharges.TabIndex = 71
        Me.lblTotCharges.Text = "0"
        Me.lblTotCharges.Visible = False
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.BackColor = System.Drawing.SystemColors.Control
        Me.Label34.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label34.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label34.Location = New System.Drawing.Point(741, 332)
        Me.Label34.Name = "Label34"
        Me.Label34.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label34.Size = New System.Drawing.Size(46, 13)
        Me.Label34.TabIndex = 70
        Me.Label34.Text = "Others :"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.SystemColors.Control
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label21.Location = New System.Drawing.Point(725, 273)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(65, 13)
        Me.Label21.TabIndex = 68
        Me.Label21.Text = "Total CGST:"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCGSTValue
        '
        Me.lblCGSTValue.BackColor = System.Drawing.SystemColors.Control
        Me.lblCGSTValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCGSTValue.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCGSTValue.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCGSTValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCGSTValue.Location = New System.Drawing.Point(798, 267)
        Me.lblCGSTValue.Name = "lblCGSTValue"
        Me.lblCGSTValue.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCGSTValue.Size = New System.Drawing.Size(99, 19)
        Me.lblCGSTValue.TabIndex = 67
        Me.lblCGSTValue.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label20.Location = New System.Drawing.Point(727, 249)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(66, 13)
        Me.Label20.TabIndex = 66
        Me.Label20.Text = "Item Value :"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotItemValue
        '
        Me.lblTotItemValue.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotItemValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotItemValue.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotItemValue.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotItemValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotItemValue.Location = New System.Drawing.Point(798, 248)
        Me.lblTotItemValue.Name = "lblTotItemValue"
        Me.lblTotItemValue.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotItemValue.Size = New System.Drawing.Size(99, 17)
        Me.lblTotItemValue.TabIndex = 65
        Me.lblTotItemValue.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label19.Location = New System.Drawing.Point(719, 352)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(74, 13)
        Me.Label19.TabIndex = 64
        Me.Label19.Text = "Net Amount :"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblNetAmount
        '
        Me.lblNetAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblNetAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNetAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblNetAmount.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblNetAmount.Location = New System.Drawing.Point(798, 348)
        Me.lblNetAmount.Name = "lblNetAmount"
        Me.lblNetAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblNetAmount.Size = New System.Drawing.Size(99, 19)
        Me.lblNetAmount.TabIndex = 63
        Me.lblNetAmount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotQty
        '
        Me.lblTotQty.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotQty.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotQty.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotQty.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotQty.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotQty.Location = New System.Drawing.Point(588, 248)
        Me.lblTotQty.Name = "lblTotQty"
        Me.lblTotQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotQty.Size = New System.Drawing.Size(99, 17)
        Me.lblTotQty.TabIndex = 62
        Me.lblTotQty.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotPackQtyCap
        '
        Me.lblTotPackQtyCap.AutoSize = True
        Me.lblTotPackQtyCap.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotPackQtyCap.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotPackQtyCap.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotPackQtyCap.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotPackQtyCap.Location = New System.Drawing.Point(526, 250)
        Me.lblTotPackQtyCap.Name = "lblTotPackQtyCap"
        Me.lblTotPackQtyCap.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotPackQtyCap.Size = New System.Drawing.Size(59, 13)
        Me.lblTotPackQtyCap.TabIndex = 61
        Me.lblTotPackQtyCap.Text = "Total Qty :"
        Me.lblTotPackQtyCap.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotOtherExp
        '
        Me.lblTotOtherExp.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotOtherExp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotOtherExp.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotOtherExp.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotOtherExp.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotOtherExp.Location = New System.Drawing.Point(798, 328)
        Me.lblTotOtherExp.Name = "lblTotOtherExp"
        Me.lblTotOtherExp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotOtherExp.Size = New System.Drawing.Size(99, 19)
        Me.lblTotOtherExp.TabIndex = 69
        Me.lblTotOtherExp.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_TabMain_TabPage1
        '
        Me._TabMain_TabPage1.Controls.Add(Me.Frame6)
        Me._TabMain_TabPage1.Location = New System.Drawing.Point(4, 22)
        Me._TabMain_TabPage1.Name = "_TabMain_TabPage1"
        Me._TabMain_TabPage1.Size = New System.Drawing.Size(900, 374)
        Me._TabMain_TabPage1.TabIndex = 1
        Me._TabMain_TabPage1.Text = "Other Details"
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.cmdDeliveryToLocSearch)
        Me.Frame6.Controls.Add(Me.TxtDeliveryToLoc)
        Me.Frame6.Controls.Add(Me.Label39)
        Me.Frame6.Controls.Add(Me.cmdSearchDeliveryTo)
        Me.Frame6.Controls.Add(Me.txtDeliveryTo)
        Me.Frame6.Controls.Add(Me.Label40)
        Me.Frame6.Controls.Add(Me.cmdShipToSearch)
        Me.Frame6.Controls.Add(Me.TxtShipTo)
        Me.Frame6.Controls.Add(Me.Label38)
        Me.Frame6.Controls.Add(Me.txtRecdDate)
        Me.Frame6.Controls.Add(Me.chkRecdAcct)
        Me.Frame6.Controls.Add(Me.chkShipTo)
        Me.Frame6.Controls.Add(Me.cmdSearchShippedTo)
        Me.Frame6.Controls.Add(Me.txtShippedTo)
        Me.Frame6.Controls.Add(Me.txtServProvided)
        Me.Frame6.Controls.Add(Me.cmdServProvided)
        Me.Frame6.Controls.Add(Me.txtInsurance)
        Me.Frame6.Controls.Add(Me.txtInspection)
        Me.Frame6.Controls.Add(Me.txtDespMode)
        Me.Frame6.Controls.Add(Me.txtDelivery)
        Me.Frame6.Controls.Add(Me.txtExcise)
        Me.Frame6.Controls.Add(Me.txtPacking)
        Me.Frame6.Controls.Add(Me.txtOthCond2)
        Me.Frame6.Controls.Add(Me.txtPayment)
        Me.Frame6.Controls.Add(Me.cmdPaySearch)
        Me.Frame6.Controls.Add(Me.txtPaymentDays)
        Me.Frame6.Controls.Add(Me.Label25)
        Me.Frame6.Controls.Add(Me.Label1)
        Me.Frame6.Controls.Add(Me.Label12)
        Me.Frame6.Controls.Add(Me.lblModDate)
        Me.Frame6.Controls.Add(Me.Label48)
        Me.Frame6.Controls.Add(Me.lblAddDate)
        Me.Frame6.Controls.Add(Me.Label45)
        Me.Frame6.Controls.Add(Me.lblModUser)
        Me.Frame6.Controls.Add(Me.Label46)
        Me.Frame6.Controls.Add(Me.lblAddUser)
        Me.Frame6.Controls.Add(Me.Label44)
        Me.Frame6.Controls.Add(Me.Label18)
        Me.Frame6.Controls.Add(Me.Label17)
        Me.Frame6.Controls.Add(Me.Label14)
        Me.Frame6.Controls.Add(Me.Label15)
        Me.Frame6.Controls.Add(Me.Label16)
        Me.Frame6.Controls.Add(Me.lblDueDays)
        Me.Frame6.Controls.Add(Me.Label3)
        Me.Frame6.Controls.Add(Me.Label5)
        Me.Frame6.Controls.Add(Me.lblPaymentTerms)
        Me.Frame6.Controls.Add(Me.Label13)
        Me.Frame6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(4, -3)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(892, 373)
        Me.Frame6.TabIndex = 84
        Me.Frame6.TabStop = False
        '
        'TxtDeliveryToLoc
        '
        Me.TxtDeliveryToLoc.AcceptsReturn = True
        Me.TxtDeliveryToLoc.BackColor = System.Drawing.SystemColors.Window
        Me.TxtDeliveryToLoc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtDeliveryToLoc.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtDeliveryToLoc.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDeliveryToLoc.ForeColor = System.Drawing.Color.Blue
        Me.TxtDeliveryToLoc.Location = New System.Drawing.Point(577, 274)
        Me.TxtDeliveryToLoc.MaxLength = 0
        Me.TxtDeliveryToLoc.Name = "TxtDeliveryToLoc"
        Me.TxtDeliveryToLoc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtDeliveryToLoc.Size = New System.Drawing.Size(102, 22)
        Me.TxtDeliveryToLoc.TabIndex = 142
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.BackColor = System.Drawing.SystemColors.Control
        Me.Label39.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label39.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label39.Location = New System.Drawing.Point(456, 277)
        Me.Label39.Name = "Label39"
        Me.Label39.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label39.Size = New System.Drawing.Size(116, 13)
        Me.Label39.TabIndex = 143
        Me.Label39.Text = "Shipped To Location :"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtDeliveryTo
        '
        Me.txtDeliveryTo.AcceptsReturn = True
        Me.txtDeliveryTo.BackColor = System.Drawing.SystemColors.Window
        Me.txtDeliveryTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDeliveryTo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDeliveryTo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeliveryTo.ForeColor = System.Drawing.Color.Blue
        Me.txtDeliveryTo.Location = New System.Drawing.Point(162, 274)
        Me.txtDeliveryTo.MaxLength = 0
        Me.txtDeliveryTo.Name = "txtDeliveryTo"
        Me.txtDeliveryTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDeliveryTo.Size = New System.Drawing.Size(250, 22)
        Me.txtDeliveryTo.TabIndex = 138
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.BackColor = System.Drawing.Color.Transparent
        Me.Label40.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label40.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.ForeColor = System.Drawing.Color.Black
        Me.Label40.Location = New System.Drawing.Point(89, 276)
        Me.Label40.Name = "Label40"
        Me.Label40.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label40.Size = New System.Drawing.Size(70, 13)
        Me.Label40.TabIndex = 140
        Me.Label40.Text = "Shipped To :"
        Me.Label40.TextAlign = System.Drawing.ContentAlignment.TopRight
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
        Me.TxtShipTo.Location = New System.Drawing.Point(577, 318)
        Me.TxtShipTo.MaxLength = 0
        Me.TxtShipTo.Name = "TxtShipTo"
        Me.TxtShipTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtShipTo.Size = New System.Drawing.Size(102, 22)
        Me.TxtShipTo.TabIndex = 135
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.BackColor = System.Drawing.SystemColors.Control
        Me.Label38.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label38.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label38.Location = New System.Drawing.Point(441, 321)
        Me.Label38.Name = "Label38"
        Me.Label38.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label38.Size = New System.Drawing.Size(131, 13)
        Me.Label38.TabIndex = 136
        Me.Label38.Text = "Shipped From Location :"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtRecdDate
        '
        Me.txtRecdDate.AcceptsReturn = True
        Me.txtRecdDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtRecdDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRecdDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRecdDate.Enabled = False
        Me.txtRecdDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRecdDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtRecdDate.Location = New System.Drawing.Point(787, 318)
        Me.txtRecdDate.MaxLength = 0
        Me.txtRecdDate.Name = "txtRecdDate"
        Me.txtRecdDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRecdDate.Size = New System.Drawing.Size(75, 22)
        Me.txtRecdDate.TabIndex = 132
        '
        'chkRecdAcct
        '
        Me.chkRecdAcct.BackColor = System.Drawing.SystemColors.Control
        Me.chkRecdAcct.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkRecdAcct.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRecdAcct.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRecdAcct.Location = New System.Drawing.Point(720, 343)
        Me.chkRecdAcct.Name = "chkRecdAcct"
        Me.chkRecdAcct.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkRecdAcct.Size = New System.Drawing.Size(168, 17)
        Me.chkRecdAcct.TabIndex = 131
        Me.chkRecdAcct.Text = "Recd By Accounts (Yes / No)"
        Me.chkRecdAcct.UseVisualStyleBackColor = False
        '
        'chkShipTo
        '
        Me.chkShipTo.BackColor = System.Drawing.SystemColors.Control
        Me.chkShipTo.Checked = True
        Me.chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkShipTo.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkShipTo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShipTo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkShipTo.Location = New System.Drawing.Point(162, 298)
        Me.chkShipTo.Name = "chkShipTo"
        Me.chkShipTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkShipTo.Size = New System.Drawing.Size(267, 16)
        Me.chkShipTo.TabIndex = 130
        Me.chkShipTo.Text = "Shipped To same as Billed (Yes / No)"
        Me.chkShipTo.UseVisualStyleBackColor = False
        '
        'txtShippedTo
        '
        Me.txtShippedTo.AcceptsReturn = True
        Me.txtShippedTo.BackColor = System.Drawing.SystemColors.Window
        Me.txtShippedTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtShippedTo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtShippedTo.Enabled = False
        Me.txtShippedTo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShippedTo.ForeColor = System.Drawing.Color.Blue
        Me.txtShippedTo.Location = New System.Drawing.Point(162, 318)
        Me.txtShippedTo.MaxLength = 0
        Me.txtShippedTo.Name = "txtShippedTo"
        Me.txtShippedTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtShippedTo.Size = New System.Drawing.Size(250, 22)
        Me.txtShippedTo.TabIndex = 127
        '
        'txtInsurance
        '
        Me.txtInsurance.AcceptsReturn = True
        Me.txtInsurance.BackColor = System.Drawing.SystemColors.Window
        Me.txtInsurance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInsurance.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInsurance.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInsurance.ForeColor = System.Drawing.Color.Blue
        Me.txtInsurance.Location = New System.Drawing.Point(162, 147)
        Me.txtInsurance.MaxLength = 0
        Me.txtInsurance.Name = "txtInsurance"
        Me.txtInsurance.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInsurance.Size = New System.Drawing.Size(598, 22)
        Me.txtInsurance.TabIndex = 28
        '
        'txtInspection
        '
        Me.txtInspection.AcceptsReturn = True
        Me.txtInspection.BackColor = System.Drawing.SystemColors.Window
        Me.txtInspection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInspection.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInspection.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInspection.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtInspection.Location = New System.Drawing.Point(162, 93)
        Me.txtInspection.MaxLength = 15
        Me.txtInspection.Name = "txtInspection"
        Me.txtInspection.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInspection.Size = New System.Drawing.Size(598, 22)
        Me.txtInspection.TabIndex = 26
        '
        'txtDespMode
        '
        Me.txtDespMode.AcceptsReturn = True
        Me.txtDespMode.BackColor = System.Drawing.SystemColors.Window
        Me.txtDespMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDespMode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDespMode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDespMode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDespMode.Location = New System.Drawing.Point(162, 66)
        Me.txtDespMode.MaxLength = 15
        Me.txtDespMode.Name = "txtDespMode"
        Me.txtDespMode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDespMode.Size = New System.Drawing.Size(598, 22)
        Me.txtDespMode.TabIndex = 25
        '
        'txtDelivery
        '
        Me.txtDelivery.AcceptsReturn = True
        Me.txtDelivery.BackColor = System.Drawing.SystemColors.Window
        Me.txtDelivery.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDelivery.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDelivery.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDelivery.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDelivery.Location = New System.Drawing.Point(162, 39)
        Me.txtDelivery.MaxLength = 15
        Me.txtDelivery.Name = "txtDelivery"
        Me.txtDelivery.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDelivery.Size = New System.Drawing.Size(598, 22)
        Me.txtDelivery.TabIndex = 24
        '
        'txtExcise
        '
        Me.txtExcise.AcceptsReturn = True
        Me.txtExcise.BackColor = System.Drawing.SystemColors.Window
        Me.txtExcise.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtExcise.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtExcise.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExcise.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtExcise.Location = New System.Drawing.Point(162, 13)
        Me.txtExcise.MaxLength = 15
        Me.txtExcise.Name = "txtExcise"
        Me.txtExcise.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExcise.Size = New System.Drawing.Size(598, 22)
        Me.txtExcise.TabIndex = 23
        '
        'txtPacking
        '
        Me.txtPacking.AcceptsReturn = True
        Me.txtPacking.BackColor = System.Drawing.SystemColors.Window
        Me.txtPacking.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPacking.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPacking.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPacking.ForeColor = System.Drawing.Color.Blue
        Me.txtPacking.Location = New System.Drawing.Point(162, 120)
        Me.txtPacking.MaxLength = 0
        Me.txtPacking.Name = "txtPacking"
        Me.txtPacking.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPacking.Size = New System.Drawing.Size(598, 22)
        Me.txtPacking.TabIndex = 27
        '
        'txtOthCond2
        '
        Me.txtOthCond2.AcceptsReturn = True
        Me.txtOthCond2.BackColor = System.Drawing.SystemColors.Window
        Me.txtOthCond2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOthCond2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOthCond2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOthCond2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtOthCond2.Location = New System.Drawing.Point(162, 174)
        Me.txtOthCond2.MaxLength = 15
        Me.txtOthCond2.Name = "txtOthCond2"
        Me.txtOthCond2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOthCond2.Size = New System.Drawing.Size(598, 22)
        Me.txtOthCond2.TabIndex = 29
        '
        'txtPayment
        '
        Me.txtPayment.AcceptsReturn = True
        Me.txtPayment.BackColor = System.Drawing.SystemColors.Window
        Me.txtPayment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPayment.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPayment.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPayment.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPayment.Location = New System.Drawing.Point(162, 201)
        Me.txtPayment.MaxLength = 15
        Me.txtPayment.Name = "txtPayment"
        Me.txtPayment.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPayment.Size = New System.Drawing.Size(47, 22)
        Me.txtPayment.TabIndex = 30
        '
        'txtPaymentDays
        '
        Me.txtPaymentDays.AcceptsReturn = True
        Me.txtPaymentDays.BackColor = System.Drawing.SystemColors.Window
        Me.txtPaymentDays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPaymentDays.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPaymentDays.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaymentDays.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPaymentDays.Location = New System.Drawing.Point(616, 202)
        Me.txtPaymentDays.MaxLength = 15
        Me.txtPaymentDays.Name = "txtPaymentDays"
        Me.txtPaymentDays.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPaymentDays.Size = New System.Drawing.Size(144, 22)
        Me.txtPaymentDays.TabIndex = 32
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.SystemColors.Control
        Me.Label25.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label25.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label25.Location = New System.Drawing.Point(717, 322)
        Me.Label25.Name = "Label25"
        Me.Label25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label25.Size = New System.Drawing.Size(65, 13)
        Me.Label25.TabIndex = 133
        Me.Label25.Text = "Recd Date :"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(74, 320)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(85, 13)
        Me.Label1.TabIndex = 129
        Me.Label1.Text = "Shipped From :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(64, 231)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(95, 13)
        Me.Label12.TabIndex = 115
        Me.Label12.Text = "Service Provider :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblModDate
        '
        Me.lblModDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblModDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblModDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblModDate.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblModDate.Location = New System.Drawing.Point(631, 344)
        Me.lblModDate.Name = "lblModDate"
        Me.lblModDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblModDate.Size = New System.Drawing.Size(73, 19)
        Me.lblModDate.TabIndex = 112
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.BackColor = System.Drawing.SystemColors.Control
        Me.Label48.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label48.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label48.Location = New System.Drawing.Point(565, 347)
        Me.Label48.Name = "Label48"
        Me.Label48.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label48.Size = New System.Drawing.Size(63, 15)
        Me.Label48.TabIndex = 111
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
        Me.lblAddDate.Location = New System.Drawing.Point(319, 344)
        Me.lblAddDate.Name = "lblAddDate"
        Me.lblAddDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAddDate.Size = New System.Drawing.Size(73, 19)
        Me.lblAddDate.TabIndex = 110
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.BackColor = System.Drawing.SystemColors.Control
        Me.Label45.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label45.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label45.Location = New System.Drawing.Point(259, 346)
        Me.Label45.Name = "Label45"
        Me.Label45.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label45.Size = New System.Drawing.Size(63, 15)
        Me.Label45.TabIndex = 109
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
        Me.lblModUser.Location = New System.Drawing.Point(462, 344)
        Me.lblModUser.Name = "lblModUser"
        Me.lblModUser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblModUser.Size = New System.Drawing.Size(69, 19)
        Me.lblModUser.TabIndex = 108
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.BackColor = System.Drawing.SystemColors.Control
        Me.Label46.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label46.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label46.Location = New System.Drawing.Point(402, 346)
        Me.Label46.Name = "Label46"
        Me.Label46.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label46.Size = New System.Drawing.Size(61, 15)
        Me.Label46.TabIndex = 107
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
        Me.lblAddUser.Location = New System.Drawing.Point(175, 344)
        Me.lblAddUser.Name = "lblAddUser"
        Me.lblAddUser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAddUser.Size = New System.Drawing.Size(69, 19)
        Me.lblAddUser.TabIndex = 106
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.BackColor = System.Drawing.SystemColors.Control
        Me.Label44.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label44.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label44.Location = New System.Drawing.Point(116, 346)
        Me.Label44.Name = "Label44"
        Me.Label44.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label44.Size = New System.Drawing.Size(61, 15)
        Me.Label44.TabIndex = 105
        Me.Label44.Text = "Add User :"
        Me.Label44.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(99, 150)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(60, 13)
        Me.Label18.TabIndex = 94
        Me.Label18.Text = "Insurance :"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(94, 98)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(65, 13)
        Me.Label17.TabIndex = 93
        Me.Label17.Text = "Inspection :"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(126, 17)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(33, 13)
        Me.Label14.TabIndex = 92
        Me.Label14.Text = "GST :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(104, 42)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(55, 13)
        Me.Label15.TabIndex = 91
        Me.Label15.Text = "Delivery :"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(52, 69)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(107, 13)
        Me.Label16.TabIndex = 90
        Me.Label16.Text = "Mode of Despatch :"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblDueDays
        '
        Me.lblDueDays.AutoSize = True
        Me.lblDueDays.BackColor = System.Drawing.SystemColors.Control
        Me.lblDueDays.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDueDays.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDueDays.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDueDays.Location = New System.Drawing.Point(46, 123)
        Me.lblDueDays.Name = "lblDueDays"
        Me.lblDueDays.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDueDays.Size = New System.Drawing.Size(113, 13)
        Me.lblDueDays.TabIndex = 89
        Me.lblDueDays.Text = "Packing Forwarding :"
        Me.lblDueDays.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(107, 179)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(51, 13)
        Me.Label3.TabIndex = 88
        Me.Label3.Text = "Process :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(72, 204)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(87, 13)
        Me.Label5.TabIndex = 87
        Me.Label5.Text = "PaymentTerms :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblPaymentTerms
        '
        Me.lblPaymentTerms.BackColor = System.Drawing.Color.Transparent
        Me.lblPaymentTerms.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPaymentTerms.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPaymentTerms.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPaymentTerms.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPaymentTerms.Location = New System.Drawing.Point(232, 202)
        Me.lblPaymentTerms.Name = "lblPaymentTerms"
        Me.lblPaymentTerms.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPaymentTerms.Size = New System.Drawing.Size(179, 19)
        Me.lblPaymentTerms.TabIndex = 86
        Me.lblPaymentTerms.Text = "lblPaymentTerms"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(527, 207)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(85, 13)
        Me.Label13.TabIndex = 85
        Me.Label13.Text = "Payment Days :"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_TabMain_TabPage2
        '
        Me._TabMain_TabPage2.Controls.Add(Me.Frame2)
        Me._TabMain_TabPage2.Location = New System.Drawing.Point(4, 22)
        Me._TabMain_TabPage2.Name = "_TabMain_TabPage2"
        Me._TabMain_TabPage2.Size = New System.Drawing.Size(900, 374)
        Me._TabMain_TabPage2.TabIndex = 2
        Me._TabMain_TabPage2.Text = "Annexure"
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.txtAnnexTitle)
        Me.Frame2.Controls.Add(Me.SprdAnnex)
        Me.Frame2.Controls.Add(Me.Label27)
        Me.Frame2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 0)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(898, 374)
        Me.Frame2.TabIndex = 100
        Me.Frame2.TabStop = False
        '
        'txtAnnexTitle
        '
        Me.txtAnnexTitle.AcceptsReturn = True
        Me.txtAnnexTitle.BackColor = System.Drawing.SystemColors.Window
        Me.txtAnnexTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAnnexTitle.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAnnexTitle.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAnnexTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtAnnexTitle.Location = New System.Drawing.Point(84, 14)
        Me.txtAnnexTitle.MaxLength = 0
        Me.txtAnnexTitle.Name = "txtAnnexTitle"
        Me.txtAnnexTitle.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAnnexTitle.Size = New System.Drawing.Size(649, 22)
        Me.txtAnnexTitle.TabIndex = 35
        '
        'SprdAnnex
        '
        Me.SprdAnnex.DataSource = Nothing
        Me.SprdAnnex.Location = New System.Drawing.Point(4, 41)
        Me.SprdAnnex.Name = "SprdAnnex"
        Me.SprdAnnex.OcxState = CType(resources.GetObject("SprdAnnex.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdAnnex.Size = New System.Drawing.Size(894, 333)
        Me.SprdAnnex.TabIndex = 36
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.SystemColors.Control
        Me.Label27.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label27.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label27.Location = New System.Drawing.Point(26, 16)
        Me.Label27.Name = "Label27"
        Me.Label27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label27.Size = New System.Drawing.Size(56, 13)
        Me.Label27.TabIndex = 102
        Me.Label27.Text = "Heading :"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_TabMain_TabPage3
        '
        Me._TabMain_TabPage3.Controls.Add(Me.Frame3)
        Me._TabMain_TabPage3.Location = New System.Drawing.Point(4, 22)
        Me._TabMain_TabPage3.Name = "_TabMain_TabPage3"
        Me._TabMain_TabPage3.Size = New System.Drawing.Size(900, 374)
        Me._TabMain_TabPage3.TabIndex = 3
        Me._TabMain_TabPage3.Text = "Lease Details"
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.FraPostingDetails)
        Me.Frame3.Controls.Add(Me.txtOwner)
        Me.Frame3.Controls.Add(Me.cmdOwner)
        Me.Frame3.Controls.Add(Me.Label30)
        Me.Frame3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 6)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(739, 263)
        Me.Frame3.TabIndex = 119
        Me.Frame3.TabStop = False
        '
        'FraPostingDetails
        '
        Me.FraPostingDetails.BackColor = System.Drawing.SystemColors.Control
        Me.FraPostingDetails.Controls.Add(Me._optPostingDetails_2)
        Me.FraPostingDetails.Controls.Add(Me._optPostingDetails_1)
        Me.FraPostingDetails.Controls.Add(Me._optPostingDetails_0)
        Me.FraPostingDetails.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraPostingDetails.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraPostingDetails.Location = New System.Drawing.Point(162, 60)
        Me.FraPostingDetails.Name = "FraPostingDetails"
        Me.FraPostingDetails.Padding = New System.Windows.Forms.Padding(0)
        Me.FraPostingDetails.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraPostingDetails.Size = New System.Drawing.Size(431, 109)
        Me.FraPostingDetails.TabIndex = 123
        Me.FraPostingDetails.TabStop = False
        Me.FraPostingDetails.Text = "Posting Details"
        '
        '_optPostingDetails_2
        '
        Me._optPostingDetails_2.BackColor = System.Drawing.SystemColors.Control
        Me._optPostingDetails_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optPostingDetails_2.Enabled = False
        Me._optPostingDetails_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optPostingDetails_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPostingDetails.SetIndex(Me._optPostingDetails_2, CType(2, Short))
        Me._optPostingDetails_2.Location = New System.Drawing.Point(108, 69)
        Me._optPostingDetails_2.Name = "_optPostingDetails_2"
        Me._optPostingDetails_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optPostingDetails_2.Size = New System.Drawing.Size(215, 17)
        Me._optPostingDetails_2.TabIndex = 126
        Me._optPostingDetails_2.TabStop = True
        Me._optPostingDetails_2.Text = "Only Cenvat && VAT Pass"
        Me._optPostingDetails_2.UseVisualStyleBackColor = False
        '
        '_optPostingDetails_1
        '
        Me._optPostingDetails_1.BackColor = System.Drawing.SystemColors.Control
        Me._optPostingDetails_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optPostingDetails_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optPostingDetails_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPostingDetails.SetIndex(Me._optPostingDetails_1, CType(1, Short))
        Me._optPostingDetails_1.Location = New System.Drawing.Point(108, 43)
        Me._optPostingDetails_1.Name = "_optPostingDetails_1"
        Me._optPostingDetails_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optPostingDetails_1.Size = New System.Drawing.Size(215, 17)
        Me._optPostingDetails_1.TabIndex = 125
        Me._optPostingDetails_1.TabStop = True
        Me._optPostingDetails_1.Text = "Only Cenvat Pass"
        Me._optPostingDetails_1.UseVisualStyleBackColor = False
        '
        '_optPostingDetails_0
        '
        Me._optPostingDetails_0.BackColor = System.Drawing.SystemColors.Control
        Me._optPostingDetails_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optPostingDetails_0.Enabled = False
        Me._optPostingDetails_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optPostingDetails_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPostingDetails.SetIndex(Me._optPostingDetails_0, CType(0, Short))
        Me._optPostingDetails_0.Location = New System.Drawing.Point(108, 16)
        Me._optPostingDetails_0.Name = "_optPostingDetails_0"
        Me._optPostingDetails_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optPostingDetails_0.Size = New System.Drawing.Size(215, 17)
        Me._optPostingDetails_0.TabIndex = 124
        Me._optPostingDetails_0.TabStop = True
        Me._optPostingDetails_0.Text = "Full Bill Pass"
        Me._optPostingDetails_0.UseVisualStyleBackColor = False
        '
        'txtOwner
        '
        Me.txtOwner.AcceptsReturn = True
        Me.txtOwner.BackColor = System.Drawing.SystemColors.Window
        Me.txtOwner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOwner.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOwner.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOwner.ForeColor = System.Drawing.Color.Blue
        Me.txtOwner.Location = New System.Drawing.Point(162, 22)
        Me.txtOwner.MaxLength = 0
        Me.txtOwner.Name = "txtOwner"
        Me.txtOwner.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOwner.Size = New System.Drawing.Size(431, 22)
        Me.txtOwner.TabIndex = 121
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.BackColor = System.Drawing.Color.Transparent
        Me.Label30.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label30.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.ForeColor = System.Drawing.Color.Black
        Me.Label30.Location = New System.Drawing.Point(111, 24)
        Me.Label30.Name = "Label30"
        Me.Label30.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label30.Size = New System.Drawing.Size(45, 13)
        Me.Label30.TabIndex = 122
        Me.Label30.Text = "Owner :"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_TabMain_TabPage4
        '
        Me._TabMain_TabPage4.Controls.Add(Me.Frame4)
        Me._TabMain_TabPage4.Location = New System.Drawing.Point(4, 22)
        Me._TabMain_TabPage4.Name = "_TabMain_TabPage4"
        Me._TabMain_TabPage4.Size = New System.Drawing.Size(900, 374)
        Me._TabMain_TabPage4.TabIndex = 4
        Me._TabMain_TabPage4.Text = "TC && Third Party Report"
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.cmdTPShow)
        Me.Frame4.Controls.Add(Me.cmdTCShow)
        Me.Frame4.Controls.Add(Me.chkApprovedWO_TC)
        Me.Frame4.Controls.Add(Me.chkTPRAvailable)
        Me.Frame4.Controls.Add(Me.chkTCAvailable)
        Me.Frame4.Controls.Add(Me.cmdTPRI)
        Me.Frame4.Controls.Add(Me.txtTPRPath)
        Me.Frame4.Controls.Add(Me.cmdTC)
        Me.Frame4.Controls.Add(Me.txtTCPath)
        Me.Frame4.Controls.Add(Me.Label36)
        Me.Frame4.Controls.Add(Me.Label35)
        Me.Frame4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(4, 22)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(739, 263)
        Me.Frame4.TabIndex = 143
        Me.Frame4.TabStop = False
        '
        'cmdTPShow
        '
        Me.cmdTPShow.BackColor = System.Drawing.SystemColors.Control
        Me.cmdTPShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdTPShow.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdTPShow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdTPShow.Location = New System.Drawing.Point(628, 100)
        Me.cmdTPShow.Name = "cmdTPShow"
        Me.cmdTPShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdTPShow.Size = New System.Drawing.Size(77, 21)
        Me.cmdTPShow.TabIndex = 154
        Me.cmdTPShow.Text = "TPR Show"
        Me.cmdTPShow.UseVisualStyleBackColor = False
        '
        'cmdTCShow
        '
        Me.cmdTCShow.BackColor = System.Drawing.SystemColors.Control
        Me.cmdTCShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdTCShow.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdTCShow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdTCShow.Location = New System.Drawing.Point(628, 46)
        Me.cmdTCShow.Name = "cmdTCShow"
        Me.cmdTCShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdTCShow.Size = New System.Drawing.Size(77, 21)
        Me.cmdTCShow.TabIndex = 153
        Me.cmdTCShow.Text = "TC Show"
        Me.cmdTCShow.UseVisualStyleBackColor = False
        '
        'chkApprovedWO_TC
        '
        Me.chkApprovedWO_TC.BackColor = System.Drawing.SystemColors.Control
        Me.chkApprovedWO_TC.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkApprovedWO_TC.Enabled = False
        Me.chkApprovedWO_TC.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkApprovedWO_TC.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkApprovedWO_TC.Location = New System.Drawing.Point(380, 26)
        Me.chkApprovedWO_TC.Name = "chkApprovedWO_TC"
        Me.chkApprovedWO_TC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkApprovedWO_TC.Size = New System.Drawing.Size(211, 17)
        Me.chkApprovedWO_TC.TabIndex = 152
        Me.chkApprovedWO_TC.Text = "Approved Without TC (Yes / No)"
        Me.chkApprovedWO_TC.UseVisualStyleBackColor = False
        '
        'chkTPRAvailable
        '
        Me.chkTPRAvailable.BackColor = System.Drawing.SystemColors.Control
        Me.chkTPRAvailable.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkTPRAvailable.Enabled = False
        Me.chkTPRAvailable.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTPRAvailable.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkTPRAvailable.Location = New System.Drawing.Point(164, 82)
        Me.chkTPRAvailable.Name = "chkTPRAvailable"
        Me.chkTPRAvailable.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkTPRAvailable.Size = New System.Drawing.Size(275, 16)
        Me.chkTPRAvailable.TabIndex = 151
        Me.chkTPRAvailable.Text = "Third Party Report Available (Yes / No)"
        Me.chkTPRAvailable.UseVisualStyleBackColor = False
        '
        'chkTCAvailable
        '
        Me.chkTCAvailable.BackColor = System.Drawing.SystemColors.Control
        Me.chkTCAvailable.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkTCAvailable.Enabled = False
        Me.chkTCAvailable.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTCAvailable.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkTCAvailable.Location = New System.Drawing.Point(164, 26)
        Me.chkTCAvailable.Name = "chkTCAvailable"
        Me.chkTCAvailable.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkTCAvailable.Size = New System.Drawing.Size(197, 17)
        Me.chkTCAvailable.TabIndex = 150
        Me.chkTCAvailable.Text = "TC Available (Yes / No)"
        Me.chkTCAvailable.UseVisualStyleBackColor = False
        '
        'txtTPRPath
        '
        Me.txtTPRPath.AcceptsReturn = True
        Me.txtTPRPath.BackColor = System.Drawing.SystemColors.Window
        Me.txtTPRPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTPRPath.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTPRPath.Enabled = False
        Me.txtTPRPath.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTPRPath.ForeColor = System.Drawing.Color.Blue
        Me.txtTPRPath.Location = New System.Drawing.Point(162, 102)
        Me.txtTPRPath.MaxLength = 0
        Me.txtTPRPath.Name = "txtTPRPath"
        Me.txtTPRPath.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTPRPath.Size = New System.Drawing.Size(431, 22)
        Me.txtTPRPath.TabIndex = 147
        '
        'txtTCPath
        '
        Me.txtTCPath.AcceptsReturn = True
        Me.txtTCPath.BackColor = System.Drawing.SystemColors.Window
        Me.txtTCPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTCPath.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTCPath.Enabled = False
        Me.txtTCPath.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTCPath.ForeColor = System.Drawing.Color.Blue
        Me.txtTCPath.Location = New System.Drawing.Point(162, 48)
        Me.txtTCPath.MaxLength = 0
        Me.txtTCPath.Name = "txtTCPath"
        Me.txtTCPath.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTCPath.Size = New System.Drawing.Size(431, 22)
        Me.txtTCPath.TabIndex = 144
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.BackColor = System.Drawing.Color.Transparent
        Me.Label36.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label36.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.ForeColor = System.Drawing.Color.Black
        Me.Label36.Location = New System.Drawing.Point(-1, 104)
        Me.Label36.Name = "Label36"
        Me.Label36.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label36.Size = New System.Drawing.Size(147, 13)
        Me.Label36.TabIndex = 149
        Me.Label36.Text = "Third Party Report Upload :"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.BackColor = System.Drawing.Color.Transparent
        Me.Label35.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label35.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.ForeColor = System.Drawing.Color.Black
        Me.Label35.Location = New System.Drawing.Point(87, 50)
        Me.Label35.Name = "Label35"
        Me.Label35.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label35.Size = New System.Drawing.Size(67, 13)
        Me.Label35.TabIndex = 146
        Me.Label35.Text = "TC Upload :"
        Me.Label35.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(0, 0)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 47
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.cmdAnnexPrint)
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.CmdView)
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.CmdDelete)
        Me.FraMovement.Controls.Add(Me.CmdSave)
        Me.FraMovement.Controls.Add(Me.cmdAmend)
        Me.FraMovement.Controls.Add(Me.CmdModify)
        Me.FraMovement.Controls.Add(Me.CmdAdd)
        Me.FraMovement.Controls.Add(Me.cmdSavePrint)
        Me.FraMovement.Controls.Add(Me.lblPOType)
        Me.FraMovement.Controls.Add(Me.lblBookType)
        Me.FraMovement.Controls.Add(Me.lblMkey)
        Me.FraMovement.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(-1, 562)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(913, 56)
        Me.FraMovement.TabIndex = 49
        Me.FraMovement.TabStop = False
        '
        'lblPOType
        '
        Me.lblPOType.BackColor = System.Drawing.SystemColors.Control
        Me.lblPOType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPOType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPOType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPOType.Location = New System.Drawing.Point(10, 28)
        Me.lblPOType.Name = "lblPOType"
        Me.lblPOType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPOType.Size = New System.Drawing.Size(71, 17)
        Me.lblPOType.TabIndex = 95
        Me.lblPOType.Text = "lblPOType"
        Me.lblPOType.Visible = False
        '
        'lblBookType
        '
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(684, 16)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(47, 21)
        Me.lblBookType.TabIndex = 58
        Me.lblBookType.Text = "lblBookType"
        '
        'lblMkey
        '
        Me.lblMkey.BackColor = System.Drawing.SystemColors.Control
        Me.lblMkey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMkey.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMkey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMkey.Location = New System.Drawing.Point(10, 18)
        Me.lblMkey.Name = "lblMkey"
        Me.lblMkey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMkey.Size = New System.Drawing.Size(45, 17)
        Me.lblMkey.TabIndex = 57
        Me.lblMkey.Text = "lblMkey"
        Me.lblMkey.Visible = False
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
        Me.UltraGrid1.Location = New System.Drawing.Point(0, 0)
        Me.UltraGrid1.Name = "UltraGrid1"
        Me.UltraGrid1.Size = New System.Drawing.Size(908, 562)
        Me.UltraGrid1.TabIndex = 81
        '
        'frmPO_GST
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(910, 621)
        Me.Controls.Add(Me.FraTrn)
        Me.Controls.Add(Me.Report1)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.UltraGrid1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(-242, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPO_GST"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Purchase Order"
        Me.FraTrn.ResumeLayout(False)
        Me.fraTop1.ResumeLayout(False)
        Me.fraTop1.PerformLayout()
        Me.TabMain.ResumeLayout(False)
        Me._TabMain_TabPage0.ResumeLayout(False)
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.fraAccounts.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdExp, System.ComponentModel.ISupportInitialize).EndInit()
        Me._TabMain_TabPage1.ResumeLayout(False)
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me._TabMain_TabPage2.ResumeLayout(False)
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        CType(Me.SprdAnnex, System.ComponentModel.ISupportInitialize).EndInit()
        Me._TabMain_TabPage3.ResumeLayout(False)
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.FraPostingDetails.ResumeLayout(False)
        Me._TabMain_TabPage4.ResumeLayout(False)
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.optPostingDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        'SprdView.DataSource = CType(ADataGrid, msdatasrc.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        'SprdView.DataSource = Nothing
    End Sub

    Public WithEvents txtBillTo As TextBox
    Public WithEvents Label37 As Label
    Public WithEvents TxtShipTo As TextBox
    Public WithEvents Label38 As Label
    Public WithEvents cmdBillToSearch As Button
    Public WithEvents cmdShipToSearch As Button
    Public WithEvents txtOldERPNo As TextBox
    Public WithEvents Label24 As Label
    Friend WithEvents UltraGrid1 As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents lblRMPO As Label
    Public WithEvents cmdInterUnitSO As Button
    Public WithEvents txtRMDesc As TextBox
    Public WithEvents lblRMDesc As Label
    Public WithEvents txtRMRate As TextBox
    Public WithEvents lblRMRate As Label
    Public WithEvents txtRMQty As TextBox
    Public WithEvents lblRMQty As Label
    Public WithEvents cmdGetData As Button
    Public WithEvents cmdDeliveryToLocSearch As Button
    Public WithEvents TxtDeliveryToLoc As TextBox
    Public WithEvents Label39 As Label
    Public WithEvents cmdSearchDeliveryTo As Button
    Public WithEvents txtDeliveryTo As TextBox
    Public WithEvents Label40 As Label
#End Region
End Class