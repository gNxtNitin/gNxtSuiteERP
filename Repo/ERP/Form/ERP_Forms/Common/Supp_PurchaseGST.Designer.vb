Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmSupp_PurchaseGST
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
    Public WithEvents sprdAcctPostDetail As AxFPSpreadADO.AxfpSpread
    Public WithEvents FraAcctPostDetail As System.Windows.Forms.GroupBox
    Public WithEvents cboGoodService As System.Windows.Forms.ComboBox
    Public WithEvents txtOBillDate As System.Windows.Forms.TextBox
    Public WithEvents txtOBillNo As System.Windows.Forms.TextBox
    Public WithEvents chkGSTClaim As System.Windows.Forms.CheckBox
    Public WithEvents cboGSTStatus As System.Windows.Forms.ComboBox
    Public WithEvents txtVNoPrefix As System.Windows.Forms.TextBox
    Public WithEvents txtGSTDate As System.Windows.Forms.TextBox
    Public WithEvents txtAmendNo As System.Windows.Forms.TextBox
    Public WithEvents chkViewAllPO As System.Windows.Forms.CheckBox
    Public WithEvents cboDivision As System.Windows.Forms.ComboBox
    Public WithEvents chkViewAll As System.Windows.Forms.CheckBox
    Public WithEvents cmdShowPO As System.Windows.Forms.Button
    Public WithEvents txtToDate As System.Windows.Forms.TextBox
    Public WithEvents _optBaseOn_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optBaseOn_0 As System.Windows.Forms.RadioButton
    Public WithEvents cmdReCalculate As System.Windows.Forms.Button
    Public WithEvents SprdPostingDetail As AxFPSpreadADO.AxfpSpread
    Public WithEvents FraPostingDtl As System.Windows.Forms.GroupBox
    Public WithEvents txtGSTNo As System.Windows.Forms.TextBox
    Public WithEvents ChkCapital As System.Windows.Forms.CheckBox
    Public WithEvents CmdSearchPO As System.Windows.Forms.Button
    Public WithEvents txtWEFDate As System.Windows.Forms.TextBox
    Public WithEvents chkFinalPost As System.Windows.Forms.CheckBox
    Public WithEvents txtVNo As System.Windows.Forms.TextBox
    Public WithEvents txtVDate As System.Windows.Forms.TextBox
    Public WithEvents txtPONo As System.Windows.Forms.TextBox
    Public WithEvents txtPODate As System.Windows.Forms.TextBox
    Public WithEvents chkCancelled As System.Windows.Forms.CheckBox
    Public WithEvents SprdExp As AxFPSpreadADO.AxfpSpread
    Public WithEvents txtSGSTRefundAmt As System.Windows.Forms.TextBox
    Public WithEvents txtIGSTRefundAmt As System.Windows.Forms.TextBox
    Public WithEvents txtCGSTRefundAmt As System.Windows.Forms.TextBox
    Public WithEvents Label48 As System.Windows.Forms.Label
    Public WithEvents Label44 As System.Windows.Forms.Label
    Public WithEvents Label20 As System.Windows.Forms.Label
    Public WithEvents Frame7 As System.Windows.Forms.GroupBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents OLE1 As System.Windows.Forms.Label
    Public WithEvents lblTotQty As System.Windows.Forms.Label
    Public WithEvents lblSHEPer As System.Windows.Forms.Label
    Public WithEvents Label29 As System.Windows.Forms.Label
    Public WithEvents lblOthersAmount As System.Windows.Forms.Label
    Public WithEvents lblTotSGST As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents lblServicePercentage As System.Windows.Forms.Label
    Public WithEvents lblCESSableAmount As System.Windows.Forms.Label
    Public WithEvents lblTotIGST As System.Windows.Forms.Label
    Public WithEvents Label47 As System.Windows.Forms.Label
    Public WithEvents lblEDUPercent As System.Windows.Forms.Label
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
    Public WithEvents LblMKey As System.Windows.Forms.Label
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents lblTotCGST As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents lblTotItemValue As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents lblNetAmount As System.Windows.Forms.Label
    Public WithEvents lblTotPackQtyCap As System.Windows.Forms.Label
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents _SSTab1_TabPage0 As System.Windows.Forms.TabPage
    Public WithEvents txtPaymentdate As System.Windows.Forms.TextBox
    Public WithEvents txtTariff As System.Windows.Forms.TextBox
    Public WithEvents txtNarration As System.Windows.Forms.TextBox
    Public WithEvents txtRemarks As System.Windows.Forms.TextBox
    Public WithEvents txtItemType As System.Windows.Forms.TextBox
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label32 As System.Windows.Forms.Label
    Public WithEvents Label26 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents _SSTab1_TabPage1 As System.Windows.Forms.TabPage
    Public WithEvents SSTab1 As System.Windows.Forms.TabControl
    Public WithEvents txtDebitAccount As System.Windows.Forms.TextBox
    Public WithEvents txtSupplier As System.Windows.Forms.TextBox
    Public WithEvents txtBillDate As System.Windows.Forms.TextBox
    Public WithEvents cboInvType As System.Windows.Forms.ComboBox
    Public WithEvents txtBillNo As System.Windows.Forms.TextBox
    Public WithEvents Label21 As System.Windows.Forms.Label
    Public WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents lblClaimStatus As System.Windows.Forms.Label
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents LblBookCode As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label36 As System.Windows.Forms.Label
    Public WithEvents Label35 As System.Windows.Forms.Label
    Public WithEvents Label27 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents lblPurchaseVNo As System.Windows.Forms.Label
    Public WithEvents lblVNo As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents lblCust As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents lblInvType As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents FraFront As System.Windows.Forms.GroupBox
    Public WithEvents AdoDCMain As VB6.ADODC
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents cmdAdd As System.Windows.Forms.Button
    Public WithEvents cmdShow As System.Windows.Forms.Button
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
    Public WithEvents cmdDelete As System.Windows.Forms.Button
    Public WithEvents cmdSave As System.Windows.Forms.Button
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents cmdPostingHead As System.Windows.Forms.Button
    Public WithEvents cmdModify As System.Windows.Forms.Button
    Public WithEvents lblBookType As System.Windows.Forms.Label
    Public WithEvents lblPMKey As System.Windows.Forms.Label
    Public WithEvents lblSODates As System.Windows.Forms.Label
    Public WithEvents lblSONos As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents optBaseOn As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSupp_PurchaseGST))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdShowPO = New System.Windows.Forms.Button()
        Me.CmdSearchPO = New System.Windows.Forms.Button()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.cmdDelete = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdPostingHead = New System.Windows.Forms.Button()
        Me.cmdModify = New System.Windows.Forms.Button()
        Me.FraAcctPostDetail = New System.Windows.Forms.GroupBox()
        Me.sprdAcctPostDetail = New AxFPSpreadADO.AxfpSpread()
        Me.FraFront = New System.Windows.Forms.GroupBox()
        Me.CmdPopFromFile = New System.Windows.Forms.Button()
        Me.txtLocationID = New System.Windows.Forms.TextBox()
        Me.cboGoodService = New System.Windows.Forms.ComboBox()
        Me.txtOBillDate = New System.Windows.Forms.TextBox()
        Me.txtOBillNo = New System.Windows.Forms.TextBox()
        Me.chkGSTClaim = New System.Windows.Forms.CheckBox()
        Me.cboGSTStatus = New System.Windows.Forms.ComboBox()
        Me.txtVNoPrefix = New System.Windows.Forms.TextBox()
        Me.txtGSTDate = New System.Windows.Forms.TextBox()
        Me.txtAmendNo = New System.Windows.Forms.TextBox()
        Me.chkViewAllPO = New System.Windows.Forms.CheckBox()
        Me.cboDivision = New System.Windows.Forms.ComboBox()
        Me.chkViewAll = New System.Windows.Forms.CheckBox()
        Me.txtToDate = New System.Windows.Forms.TextBox()
        Me.FraPostingDtl = New System.Windows.Forms.GroupBox()
        Me._optBaseOn_1 = New System.Windows.Forms.RadioButton()
        Me._optBaseOn_0 = New System.Windows.Forms.RadioButton()
        Me.cmdReCalculate = New System.Windows.Forms.Button()
        Me.SprdPostingDetail = New AxFPSpreadADO.AxfpSpread()
        Me.txtGSTNo = New System.Windows.Forms.TextBox()
        Me.ChkCapital = New System.Windows.Forms.CheckBox()
        Me.txtWEFDate = New System.Windows.Forms.TextBox()
        Me.chkFinalPost = New System.Windows.Forms.CheckBox()
        Me.txtVNo = New System.Windows.Forms.TextBox()
        Me.txtVDate = New System.Windows.Forms.TextBox()
        Me.txtPONo = New System.Windows.Forms.TextBox()
        Me.txtPODate = New System.Windows.Forms.TextBox()
        Me.chkCancelled = New System.Windows.Forms.CheckBox()
        Me.SSTab1 = New System.Windows.Forms.TabControl()
        Me._SSTab1_TabPage0 = New System.Windows.Forms.TabPage()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.SprdExp = New AxFPSpreadADO.AxfpSpread()
        Me.Frame7 = New System.Windows.Forms.GroupBox()
        Me.txtSGSTRefundAmt = New System.Windows.Forms.TextBox()
        Me.txtIGSTRefundAmt = New System.Windows.Forms.TextBox()
        Me.txtCGSTRefundAmt = New System.Windows.Forms.TextBox()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.lblTotQty = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.lblTotPackQtyCap = New System.Windows.Forms.Label()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.OLE1 = New System.Windows.Forms.Label()
        Me.lblSHEPer = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.lblOthersAmount = New System.Windows.Forms.Label()
        Me.lblTotSGST = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblServicePercentage = New System.Windows.Forms.Label()
        Me.lblCESSableAmount = New System.Windows.Forms.Label()
        Me.lblTotIGST = New System.Windows.Forms.Label()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.lblEDUPercent = New System.Windows.Forms.Label()
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
        Me.LblMKey = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.lblTotCGST = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblTotItemValue = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblNetAmount = New System.Windows.Forms.Label()
        Me._SSTab1_TabPage1 = New System.Windows.Forms.TabPage()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.txtPaymentdate = New System.Windows.Forms.TextBox()
        Me.txtTariff = New System.Windows.Forms.TextBox()
        Me.txtNarration = New System.Windows.Forms.TextBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtItemType = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtDebitAccount = New System.Windows.Forms.TextBox()
        Me.txtSupplier = New System.Windows.Forms.TextBox()
        Me.txtBillDate = New System.Windows.Forms.TextBox()
        Me.cboInvType = New System.Windows.Forms.ComboBox()
        Me.txtBillNo = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblClaimStatus = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.LblBookCode = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblPurchaseVNo = New System.Windows.Forms.Label()
        Me.lblVNo = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblCust = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblInvType = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.AdoDCMain = New Microsoft.VisualBasic.Compatibility.VB6.ADODC()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.lblPMKey = New System.Windows.Forms.Label()
        Me.lblSODates = New System.Windows.Forms.Label()
        Me.lblSONos = New System.Windows.Forms.Label()
        Me.optBaseOn = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.CommonDialogOpen = New System.Windows.Forms.OpenFileDialog()
        Me.CommonDialogSave = New System.Windows.Forms.SaveFileDialog()
        Me.CommonDialogPrint = New System.Windows.Forms.PrintDialog()
        Me.CommonDialogColor = New System.Windows.Forms.ColorDialog()
        Me.CommonDialogFont = New System.Windows.Forms.FontDialog()
        Me.Frame8 = New System.Windows.Forms.GroupBox()
        Me.lblJVTMKey = New System.Windows.Forms.Label()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.txtSection = New System.Windows.Forms.TextBox()
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
        Me.FraAcctPostDetail.SuspendLayout()
        CType(Me.sprdAcctPostDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraFront.SuspendLayout()
        Me.FraPostingDtl.SuspendLayout()
        CType(Me.SprdPostingDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SSTab1.SuspendLayout()
        Me._SSTab1_TabPage0.SuspendLayout()
        Me.Frame6.SuspendLayout()
        CType(Me.SprdExp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame7.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage1.SuspendLayout()
        Me.Frame1.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optBaseOn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame8.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdShowPO
        '
        Me.cmdShowPO.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdShowPO.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdShowPO.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdShowPO.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdShowPO.Image = CType(resources.GetObject("cmdShowPO.Image"), System.Drawing.Image)
        Me.cmdShowPO.Location = New System.Drawing.Point(780, 94)
        Me.cmdShowPO.Name = "cmdShowPO"
        Me.cmdShowPO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShowPO.Size = New System.Drawing.Size(23, 19)
        Me.cmdShowPO.TabIndex = 10
        Me.cmdShowPO.TabStop = False
        Me.cmdShowPO.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShowPO, "Show PO Detail")
        Me.cmdShowPO.UseVisualStyleBackColor = False
        '
        'CmdSearchPO
        '
        Me.CmdSearchPO.BackColor = System.Drawing.SystemColors.Menu
        Me.CmdSearchPO.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSearchPO.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSearchPO.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSearchPO.Image = CType(resources.GetObject("CmdSearchPO.Image"), System.Drawing.Image)
        Me.CmdSearchPO.Location = New System.Drawing.Point(162, 94)
        Me.CmdSearchPO.Name = "CmdSearchPO"
        Me.CmdSearchPO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSearchPO.Size = New System.Drawing.Size(23, 19)
        Me.CmdSearchPO.TabIndex = 93
        Me.CmdSearchPO.TabStop = False
        Me.CmdSearchPO.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSearchPO, "Seach Pending DC")
        Me.CmdSearchPO.UseVisualStyleBackColor = False
        '
        'cmdAdd
        '
        Me.cmdAdd.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdAdd.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAdd.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAdd.Image = CType(resources.GetObject("cmdAdd.Image"), System.Drawing.Image)
        Me.cmdAdd.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdAdd.Location = New System.Drawing.Point(118, 11)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAdd.Size = New System.Drawing.Size(62, 36)
        Me.cmdAdd.TabIndex = 0
        Me.cmdAdd.Text = "&Add"
        Me.cmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdAdd, "Add New")
        Me.cmdAdd.UseVisualStyleBackColor = False
        '
        'cmdShow
        '
        Me.cmdShow.BackColor = System.Drawing.SystemColors.Control
        Me.cmdShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdShow.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdShow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdShow.Image = CType(resources.GetObject("cmdShow.Image"), System.Drawing.Image)
        Me.cmdShow.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdShow.Location = New System.Drawing.Point(363, 11)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShow.Size = New System.Drawing.Size(62, 36)
        Me.cmdShow.TabIndex = 96
        Me.cmdShow.Text = "S&ummary"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdClose.Location = New System.Drawing.Point(730, 11)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(62, 36)
        Me.cmdClose.TabIndex = 37
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
        Me.CmdView.Location = New System.Drawing.Point(669, 11)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(62, 36)
        Me.CmdView.TabIndex = 36
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
        Me.CmdPreview.Location = New System.Drawing.Point(608, 11)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(62, 36)
        Me.CmdPreview.TabIndex = 35
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
        Me.cmdPrint.Location = New System.Drawing.Point(548, 11)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(62, 36)
        Me.cmdPrint.TabIndex = 34
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
        Me.cmdSavePrint.Location = New System.Drawing.Point(487, 11)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(62, 36)
        Me.cmdSavePrint.TabIndex = 33
        Me.cmdSavePrint.Text = "Save&&Print"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save & Print")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'cmdDelete
        '
        Me.cmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDelete.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDelete.Image = CType(resources.GetObject("cmdDelete.Image"), System.Drawing.Image)
        Me.cmdDelete.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdDelete.Location = New System.Drawing.Point(302, 11)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDelete.Size = New System.Drawing.Size(62, 36)
        Me.cmdDelete.TabIndex = 32
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
        Me.cmdSave.Location = New System.Drawing.Point(241, 11)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(62, 36)
        Me.cmdSave.TabIndex = 31
        Me.cmdSave.Text = "&Save"
        Me.cmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSave, "Save Current Record")
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'cmdPostingHead
        '
        Me.cmdPostingHead.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPostingHead.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPostingHead.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPostingHead.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPostingHead.Image = CType(resources.GetObject("cmdPostingHead.Image"), System.Drawing.Image)
        Me.cmdPostingHead.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdPostingHead.Location = New System.Drawing.Point(425, 11)
        Me.cmdPostingHead.Name = "cmdPostingHead"
        Me.cmdPostingHead.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPostingHead.Size = New System.Drawing.Size(62, 36)
        Me.cmdPostingHead.TabIndex = 118
        Me.cmdPostingHead.Text = "&Posting Detail"
        Me.cmdPostingHead.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPostingHead, "Delete")
        Me.cmdPostingHead.UseVisualStyleBackColor = False
        '
        'cmdModify
        '
        Me.cmdModify.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdModify.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdModify.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdModify.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdModify.Image = CType(resources.GetObject("cmdModify.Image"), System.Drawing.Image)
        Me.cmdModify.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdModify.Location = New System.Drawing.Point(179, 11)
        Me.cmdModify.Name = "cmdModify"
        Me.cmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdModify.Size = New System.Drawing.Size(62, 36)
        Me.cmdModify.TabIndex = 30
        Me.cmdModify.Text = "&Modify"
        Me.cmdModify.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdModify, "Modify ")
        Me.cmdModify.UseVisualStyleBackColor = False
        '
        'FraAcctPostDetail
        '
        Me.FraAcctPostDetail.BackColor = System.Drawing.SystemColors.Control
        Me.FraAcctPostDetail.Controls.Add(Me.sprdAcctPostDetail)
        Me.FraAcctPostDetail.Enabled = False
        Me.FraAcctPostDetail.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraAcctPostDetail.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraAcctPostDetail.Location = New System.Drawing.Point(5, 365)
        Me.FraAcctPostDetail.Name = "FraAcctPostDetail"
        Me.FraAcctPostDetail.Padding = New System.Windows.Forms.Padding(0)
        Me.FraAcctPostDetail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraAcctPostDetail.Size = New System.Drawing.Size(414, 207)
        Me.FraAcctPostDetail.TabIndex = 119
        Me.FraAcctPostDetail.TabStop = False
        Me.FraAcctPostDetail.Visible = False
        '
        'sprdAcctPostDetail
        '
        Me.sprdAcctPostDetail.DataSource = Nothing
        Me.sprdAcctPostDetail.Location = New System.Drawing.Point(2, 8)
        Me.sprdAcctPostDetail.Name = "sprdAcctPostDetail"
        Me.sprdAcctPostDetail.OcxState = CType(resources.GetObject("sprdAcctPostDetail.OcxState"), System.Windows.Forms.AxHost.State)
        Me.sprdAcctPostDetail.Size = New System.Drawing.Size(407, 195)
        Me.sprdAcctPostDetail.TabIndex = 120
        '
        'FraFront
        '
        Me.FraFront.BackColor = System.Drawing.SystemColors.Control
        Me.FraFront.Controls.Add(Me.CmdPopFromFile)
        Me.FraFront.Controls.Add(Me.txtLocationID)
        Me.FraFront.Controls.Add(Me.cboGoodService)
        Me.FraFront.Controls.Add(Me.txtOBillDate)
        Me.FraFront.Controls.Add(Me.txtOBillNo)
        Me.FraFront.Controls.Add(Me.chkGSTClaim)
        Me.FraFront.Controls.Add(Me.cboGSTStatus)
        Me.FraFront.Controls.Add(Me.txtVNoPrefix)
        Me.FraFront.Controls.Add(Me.txtGSTDate)
        Me.FraFront.Controls.Add(Me.txtAmendNo)
        Me.FraFront.Controls.Add(Me.chkViewAllPO)
        Me.FraFront.Controls.Add(Me.cboDivision)
        Me.FraFront.Controls.Add(Me.chkViewAll)
        Me.FraFront.Controls.Add(Me.cmdShowPO)
        Me.FraFront.Controls.Add(Me.txtToDate)
        Me.FraFront.Controls.Add(Me.FraPostingDtl)
        Me.FraFront.Controls.Add(Me.txtGSTNo)
        Me.FraFront.Controls.Add(Me.ChkCapital)
        Me.FraFront.Controls.Add(Me.CmdSearchPO)
        Me.FraFront.Controls.Add(Me.txtWEFDate)
        Me.FraFront.Controls.Add(Me.chkFinalPost)
        Me.FraFront.Controls.Add(Me.txtVNo)
        Me.FraFront.Controls.Add(Me.txtVDate)
        Me.FraFront.Controls.Add(Me.txtPONo)
        Me.FraFront.Controls.Add(Me.txtPODate)
        Me.FraFront.Controls.Add(Me.chkCancelled)
        Me.FraFront.Controls.Add(Me.SSTab1)
        Me.FraFront.Controls.Add(Me.txtDebitAccount)
        Me.FraFront.Controls.Add(Me.txtSupplier)
        Me.FraFront.Controls.Add(Me.txtBillDate)
        Me.FraFront.Controls.Add(Me.cboInvType)
        Me.FraFront.Controls.Add(Me.txtBillNo)
        Me.FraFront.Controls.Add(Me.Label21)
        Me.FraFront.Controls.Add(Me.Label19)
        Me.FraFront.Controls.Add(Me.Label8)
        Me.FraFront.Controls.Add(Me.lblClaimStatus)
        Me.FraFront.Controls.Add(Me.Label18)
        Me.FraFront.Controls.Add(Me.LblBookCode)
        Me.FraFront.Controls.Add(Me.Label10)
        Me.FraFront.Controls.Add(Me.Label36)
        Me.FraFront.Controls.Add(Me.Label35)
        Me.FraFront.Controls.Add(Me.Label27)
        Me.FraFront.Controls.Add(Me.Label14)
        Me.FraFront.Controls.Add(Me.Label15)
        Me.FraFront.Controls.Add(Me.Label4)
        Me.FraFront.Controls.Add(Me.lblPurchaseVNo)
        Me.FraFront.Controls.Add(Me.lblVNo)
        Me.FraFront.Controls.Add(Me.Label6)
        Me.FraFront.Controls.Add(Me.Label12)
        Me.FraFront.Controls.Add(Me.Label3)
        Me.FraFront.Controls.Add(Me.lblCust)
        Me.FraFront.Controls.Add(Me.Label5)
        Me.FraFront.Controls.Add(Me.lblInvType)
        Me.FraFront.Controls.Add(Me.Label1)
        Me.FraFront.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraFront.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraFront.Location = New System.Drawing.Point(0, -4)
        Me.FraFront.Name = "FraFront"
        Me.FraFront.Padding = New System.Windows.Forms.Padding(0)
        Me.FraFront.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraFront.Size = New System.Drawing.Size(908, 576)
        Me.FraFront.TabIndex = 43
        Me.FraFront.TabStop = False
        '
        'CmdPopFromFile
        '
        Me.CmdPopFromFile.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPopFromFile.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPopFromFile.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPopFromFile.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPopFromFile.Location = New System.Drawing.Point(708, 124)
        Me.CmdPopFromFile.Name = "CmdPopFromFile"
        Me.CmdPopFromFile.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPopFromFile.Size = New System.Drawing.Size(134, 23)
        Me.CmdPopFromFile.TabIndex = 152
        Me.CmdPopFromFile.Text = "Populate From File"
        Me.CmdPopFromFile.UseVisualStyleBackColor = False
        '
        'txtLocationID
        '
        Me.txtLocationID.AcceptsReturn = True
        Me.txtLocationID.BackColor = System.Drawing.SystemColors.Window
        Me.txtLocationID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLocationID.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLocationID.Enabled = False
        Me.txtLocationID.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocationID.ForeColor = System.Drawing.Color.Blue
        Me.txtLocationID.Location = New System.Drawing.Point(186, 120)
        Me.txtLocationID.MaxLength = 0
        Me.txtLocationID.Name = "txtLocationID"
        Me.txtLocationID.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLocationID.Size = New System.Drawing.Size(118, 20)
        Me.txtLocationID.TabIndex = 127
        '
        'cboGoodService
        '
        Me.cboGoodService.BackColor = System.Drawing.SystemColors.Window
        Me.cboGoodService.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboGoodService.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboGoodService.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboGoodService.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboGoodService.Location = New System.Drawing.Point(105, 148)
        Me.cboGoodService.Name = "cboGoodService"
        Me.cboGoodService.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboGoodService.Size = New System.Drawing.Size(199, 22)
        Me.cboGoodService.TabIndex = 14
        '
        'txtOBillDate
        '
        Me.txtOBillDate.AcceptsReturn = True
        Me.txtOBillDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtOBillDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOBillDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOBillDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOBillDate.ForeColor = System.Drawing.Color.Blue
        Me.txtOBillDate.Location = New System.Drawing.Point(584, 120)
        Me.txtOBillDate.MaxLength = 0
        Me.txtOBillDate.Name = "txtOBillDate"
        Me.txtOBillDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOBillDate.Size = New System.Drawing.Size(69, 20)
        Me.txtOBillDate.TabIndex = 13
        '
        'txtOBillNo
        '
        Me.txtOBillNo.AcceptsReturn = True
        Me.txtOBillNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtOBillNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOBillNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOBillNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOBillNo.ForeColor = System.Drawing.Color.Blue
        Me.txtOBillNo.Location = New System.Drawing.Point(452, 120)
        Me.txtOBillNo.MaxLength = 0
        Me.txtOBillNo.Name = "txtOBillNo"
        Me.txtOBillNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOBillNo.Size = New System.Drawing.Size(75, 20)
        Me.txtOBillNo.TabIndex = 12
        '
        'chkGSTClaim
        '
        Me.chkGSTClaim.AutoSize = True
        Me.chkGSTClaim.BackColor = System.Drawing.SystemColors.Control
        Me.chkGSTClaim.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkGSTClaim.Enabled = False
        Me.chkGSTClaim.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGSTClaim.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkGSTClaim.Location = New System.Drawing.Point(706, 173)
        Me.chkGSTClaim.Name = "chkGSTClaim"
        Me.chkGSTClaim.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkGSTClaim.Size = New System.Drawing.Size(82, 18)
        Me.chkGSTClaim.TabIndex = 17
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
        Me.cboGSTStatus.Location = New System.Drawing.Point(83, 120)
        Me.cboGSTStatus.Name = "cboGSTStatus"
        Me.cboGSTStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboGSTStatus.Size = New System.Drawing.Size(101, 22)
        Me.cboGSTStatus.TabIndex = 11
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
        Me.txtVNoPrefix.TabIndex = 121
        '
        'txtGSTDate
        '
        Me.txtGSTDate.AcceptsReturn = True
        Me.txtGSTDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtGSTDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGSTDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGSTDate.Enabled = False
        Me.txtGSTDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGSTDate.ForeColor = System.Drawing.Color.Blue
        Me.txtGSTDate.Location = New System.Drawing.Point(584, 148)
        Me.txtGSTDate.MaxLength = 0
        Me.txtGSTDate.Name = "txtGSTDate"
        Me.txtGSTDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGSTDate.Size = New System.Drawing.Size(69, 20)
        Me.txtGSTDate.TabIndex = 16
        '
        'txtAmendNo
        '
        Me.txtAmendNo.AcceptsReturn = True
        Me.txtAmendNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtAmendNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmendNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAmendNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmendNo.ForeColor = System.Drawing.Color.Blue
        Me.txtAmendNo.Location = New System.Drawing.Point(452, 94)
        Me.txtAmendNo.MaxLength = 0
        Me.txtAmendNo.Name = "txtAmendNo"
        Me.txtAmendNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAmendNo.Size = New System.Drawing.Size(51, 20)
        Me.txtAmendNo.TabIndex = 7
        '
        'chkViewAllPO
        '
        Me.chkViewAllPO.AutoSize = True
        Me.chkViewAllPO.BackColor = System.Drawing.SystemColors.Control
        Me.chkViewAllPO.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkViewAllPO.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkViewAllPO.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkViewAllPO.Location = New System.Drawing.Point(344, 173)
        Me.chkViewAllPO.Name = "chkViewAllPO"
        Me.chkViewAllPO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkViewAllPO.Size = New System.Drawing.Size(89, 18)
        Me.chkViewAllPO.TabIndex = 111
        Me.chkViewAllPO.Text = "View All PO"
        Me.chkViewAllPO.UseVisualStyleBackColor = False
        '
        'cboDivision
        '
        Me.cboDivision.BackColor = System.Drawing.SystemColors.Window
        Me.cboDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivision.Enabled = False
        Me.cboDivision.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDivision.Location = New System.Drawing.Point(83, 66)
        Me.cboDivision.Name = "cboDivision"
        Me.cboDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivision.Size = New System.Drawing.Size(221, 22)
        Me.cboDivision.TabIndex = 109
        '
        'chkViewAll
        '
        Me.chkViewAll.AutoSize = True
        Me.chkViewAll.BackColor = System.Drawing.SystemColors.Control
        Me.chkViewAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkViewAll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkViewAll.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkViewAll.Location = New System.Drawing.Point(536, 173)
        Me.chkViewAll.Name = "chkViewAll"
        Me.chkViewAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkViewAll.Size = New System.Drawing.Size(71, 18)
        Me.chkViewAll.TabIndex = 115
        Me.chkViewAll.Text = "View All"
        Me.chkViewAll.UseVisualStyleBackColor = False
        '
        'txtToDate
        '
        Me.txtToDate.AcceptsReturn = True
        Me.txtToDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtToDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtToDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToDate.ForeColor = System.Drawing.Color.Blue
        Me.txtToDate.Location = New System.Drawing.Point(711, 94)
        Me.txtToDate.MaxLength = 0
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtToDate.Size = New System.Drawing.Size(69, 20)
        Me.txtToDate.TabIndex = 9
        '
        'FraPostingDtl
        '
        Me.FraPostingDtl.BackColor = System.Drawing.SystemColors.Control
        Me.FraPostingDtl.Controls.Add(Me._optBaseOn_1)
        Me.FraPostingDtl.Controls.Add(Me._optBaseOn_0)
        Me.FraPostingDtl.Controls.Add(Me.cmdReCalculate)
        Me.FraPostingDtl.Controls.Add(Me.SprdPostingDetail)
        Me.FraPostingDtl.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraPostingDtl.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraPostingDtl.Location = New System.Drawing.Point(6, 355)
        Me.FraPostingDtl.Name = "FraPostingDtl"
        Me.FraPostingDtl.Padding = New System.Windows.Forms.Padding(0)
        Me.FraPostingDtl.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraPostingDtl.Size = New System.Drawing.Size(457, 225)
        Me.FraPostingDtl.TabIndex = 97
        Me.FraPostingDtl.TabStop = False
        Me.FraPostingDtl.Visible = False
        '
        '_optBaseOn_1
        '
        Me._optBaseOn_1.AutoSize = True
        Me._optBaseOn_1.BackColor = System.Drawing.SystemColors.Control
        Me._optBaseOn_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optBaseOn_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optBaseOn_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optBaseOn.SetIndex(Me._optBaseOn_1, CType(1, Short))
        Me._optBaseOn_1.Location = New System.Drawing.Point(304, 204)
        Me._optBaseOn_1.Name = "_optBaseOn_1"
        Me._optBaseOn_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optBaseOn_1.Size = New System.Drawing.Size(97, 18)
        Me._optBaseOn_1.TabIndex = 106
        Me._optBaseOn_1.TabStop = True
        Me._optBaseOn_1.Text = "Base On Rate"
        Me._optBaseOn_1.UseVisualStyleBackColor = False
        '
        '_optBaseOn_0
        '
        Me._optBaseOn_0.AutoSize = True
        Me._optBaseOn_0.BackColor = System.Drawing.SystemColors.Control
        Me._optBaseOn_0.Checked = True
        Me._optBaseOn_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optBaseOn_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optBaseOn_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optBaseOn.SetIndex(Me._optBaseOn_0, CType(0, Short))
        Me._optBaseOn_0.Location = New System.Drawing.Point(168, 204)
        Me._optBaseOn_0.Name = "_optBaseOn_0"
        Me._optBaseOn_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optBaseOn_0.Size = New System.Drawing.Size(91, 18)
        Me._optBaseOn_0.TabIndex = 105
        Me._optBaseOn_0.TabStop = True
        Me._optBaseOn_0.Text = "Base On Qty"
        Me._optBaseOn_0.UseVisualStyleBackColor = False
        '
        'cmdReCalculate
        '
        Me.cmdReCalculate.BackColor = System.Drawing.SystemColors.Control
        Me.cmdReCalculate.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdReCalculate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdReCalculate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdReCalculate.Location = New System.Drawing.Point(2, 203)
        Me.cmdReCalculate.Name = "cmdReCalculate"
        Me.cmdReCalculate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdReCalculate.Size = New System.Drawing.Size(91, 21)
        Me.cmdReCalculate.TabIndex = 104
        Me.cmdReCalculate.Text = "Re-Calculate"
        Me.cmdReCalculate.UseVisualStyleBackColor = False
        '
        'SprdPostingDetail
        '
        Me.SprdPostingDetail.DataSource = Nothing
        Me.SprdPostingDetail.Location = New System.Drawing.Point(4, 8)
        Me.SprdPostingDetail.Name = "SprdPostingDetail"
        Me.SprdPostingDetail.OcxState = CType(resources.GetObject("SprdPostingDetail.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdPostingDetail.Size = New System.Drawing.Size(449, 193)
        Me.SprdPostingDetail.TabIndex = 98
        '
        'txtGSTNo
        '
        Me.txtGSTNo.AcceptsReturn = True
        Me.txtGSTNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtGSTNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGSTNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGSTNo.Enabled = False
        Me.txtGSTNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGSTNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtGSTNo.Location = New System.Drawing.Point(453, 148)
        Me.txtGSTNo.MaxLength = 0
        Me.txtGSTNo.Name = "txtGSTNo"
        Me.txtGSTNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGSTNo.Size = New System.Drawing.Size(75, 20)
        Me.txtGSTNo.TabIndex = 15
        '
        'ChkCapital
        '
        Me.ChkCapital.AutoSize = True
        Me.ChkCapital.BackColor = System.Drawing.SystemColors.Control
        Me.ChkCapital.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkCapital.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkCapital.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ChkCapital.Location = New System.Drawing.Point(830, 173)
        Me.ChkCapital.Name = "ChkCapital"
        Me.ChkCapital.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkCapital.Size = New System.Drawing.Size(76, 18)
        Me.ChkCapital.TabIndex = 94
        Me.ChkCapital.Text = "Is Capital"
        Me.ChkCapital.UseVisualStyleBackColor = False
        '
        'txtWEFDate
        '
        Me.txtWEFDate.AcceptsReturn = True
        Me.txtWEFDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtWEFDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWEFDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWEFDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWEFDate.ForeColor = System.Drawing.Color.Blue
        Me.txtWEFDate.Location = New System.Drawing.Point(584, 94)
        Me.txtWEFDate.MaxLength = 0
        Me.txtWEFDate.Name = "txtWEFDate"
        Me.txtWEFDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWEFDate.Size = New System.Drawing.Size(69, 20)
        Me.txtWEFDate.TabIndex = 8
        '
        'chkFinalPost
        '
        Me.chkFinalPost.AutoSize = True
        Me.chkFinalPost.BackColor = System.Drawing.SystemColors.Control
        Me.chkFinalPost.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkFinalPost.Enabled = False
        Me.chkFinalPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFinalPost.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkFinalPost.Location = New System.Drawing.Point(618, 173)
        Me.chkFinalPost.Name = "chkFinalPost"
        Me.chkFinalPost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkFinalPost.Size = New System.Drawing.Size(76, 18)
        Me.chkFinalPost.TabIndex = 114
        Me.chkFinalPost.Text = "FinalPost"
        Me.chkFinalPost.UseVisualStyleBackColor = False
        '
        'txtVNo
        '
        Me.txtVNo.AcceptsReturn = True
        Me.txtVNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtVNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVNo.ForeColor = System.Drawing.Color.Blue
        Me.txtVNo.Location = New System.Drawing.Point(106, 14)
        Me.txtVNo.MaxLength = 0
        Me.txtVNo.Name = "txtVNo"
        Me.txtVNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVNo.Size = New System.Drawing.Size(88, 20)
        Me.txtVNo.TabIndex = 1
        '
        'txtVDate
        '
        Me.txtVDate.AcceptsReturn = True
        Me.txtVDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtVDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVDate.ForeColor = System.Drawing.Color.Blue
        Me.txtVDate.Location = New System.Drawing.Point(235, 14)
        Me.txtVDate.MaxLength = 0
        Me.txtVDate.Name = "txtVDate"
        Me.txtVDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVDate.Size = New System.Drawing.Size(69, 20)
        Me.txtVDate.TabIndex = 2
        '
        'txtPONo
        '
        Me.txtPONo.AcceptsReturn = True
        Me.txtPONo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPONo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPONo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPONo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPONo.ForeColor = System.Drawing.Color.Blue
        Me.txtPONo.Location = New System.Drawing.Point(83, 94)
        Me.txtPONo.MaxLength = 0
        Me.txtPONo.Name = "txtPONo"
        Me.txtPONo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPONo.Size = New System.Drawing.Size(79, 20)
        Me.txtPONo.TabIndex = 5
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
        Me.txtPODate.Location = New System.Drawing.Point(235, 93)
        Me.txtPODate.MaxLength = 0
        Me.txtPODate.Name = "txtPODate"
        Me.txtPODate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPODate.Size = New System.Drawing.Size(69, 20)
        Me.txtPODate.TabIndex = 6
        '
        'chkCancelled
        '
        Me.chkCancelled.AutoSize = True
        Me.chkCancelled.BackColor = System.Drawing.SystemColors.Control
        Me.chkCancelled.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCancelled.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCancelled.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkCancelled.Location = New System.Drawing.Point(444, 173)
        Me.chkCancelled.Name = "chkCancelled"
        Me.chkCancelled.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCancelled.Size = New System.Drawing.Size(80, 18)
        Me.chkCancelled.TabIndex = 113
        Me.chkCancelled.Text = "Cancelled"
        Me.chkCancelled.UseVisualStyleBackColor = False
        '
        'SSTab1
        '
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage0)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage1)
        Me.SSTab1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SSTab1.ItemSize = New System.Drawing.Size(42, 18)
        Me.SSTab1.Location = New System.Drawing.Point(2, 173)
        Me.SSTab1.Name = "SSTab1"
        Me.SSTab1.SelectedIndex = 0
        Me.SSTab1.Size = New System.Drawing.Size(908, 401)
        Me.SSTab1.TabIndex = 48
        '
        '_SSTab1_TabPage0
        '
        Me._SSTab1_TabPage0.Controls.Add(Me.Frame6)
        Me._SSTab1_TabPage0.Location = New System.Drawing.Point(4, 22)
        Me._SSTab1_TabPage0.Name = "_SSTab1_TabPage0"
        Me._SSTab1_TabPage0.Size = New System.Drawing.Size(900, 375)
        Me._SSTab1_TabPage0.TabIndex = 0
        Me._SSTab1_TabPage0.Text = "Item Details"
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.SprdExp)
        Me.Frame6.Controls.Add(Me.Frame7)
        Me.Frame6.Controls.Add(Me.SprdMain)
        Me.Frame6.Controls.Add(Me.OLE1)
        Me.Frame6.Controls.Add(Me.lblSHEPer)
        Me.Frame6.Controls.Add(Me.Label29)
        Me.Frame6.Controls.Add(Me.lblOthersAmount)
        Me.Frame6.Controls.Add(Me.lblTotSGST)
        Me.Frame6.Controls.Add(Me.Label2)
        Me.Frame6.Controls.Add(Me.lblServicePercentage)
        Me.Frame6.Controls.Add(Me.lblCESSableAmount)
        Me.Frame6.Controls.Add(Me.lblTotIGST)
        Me.Frame6.Controls.Add(Me.Label47)
        Me.Frame6.Controls.Add(Me.lblEDUPercent)
        Me.Frame6.Controls.Add(Me.lblMSC)
        Me.Frame6.Controls.Add(Me.lblRO)
        Me.Frame6.Controls.Add(Me.lblSurcharge)
        Me.Frame6.Controls.Add(Me.lblDiscount)
        Me.Frame6.Controls.Add(Me.lblTotTaxableAmt)
        Me.Frame6.Controls.Add(Me.lblSTPercentage)
        Me.Frame6.Controls.Add(Me.lblEDPercentage)
        Me.Frame6.Controls.Add(Me.lblTotExpAmt)
        Me.Frame6.Controls.Add(Me.lblTotFreight)
        Me.Frame6.Controls.Add(Me.lblTotCharges)
        Me.Frame6.Controls.Add(Me.LblMKey)
        Me.Frame6.Controls.Add(Me.Label17)
        Me.Frame6.Controls.Add(Me.lblTotCGST)
        Me.Frame6.Controls.Add(Me.Label16)
        Me.Frame6.Controls.Add(Me.lblTotItemValue)
        Me.Frame6.Controls.Add(Me.Label13)
        Me.Frame6.Controls.Add(Me.lblNetAmount)
        Me.Frame6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(0, -3)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(900, 377)
        Me.Frame6.TabIndex = 53
        Me.Frame6.TabStop = False
        '
        'SprdExp
        '
        Me.SprdExp.DataSource = Nothing
        Me.SprdExp.Location = New System.Drawing.Point(2, 198)
        Me.SprdExp.Name = "SprdExp"
        Me.SprdExp.OcxState = CType(resources.GetObject("SprdExp.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdExp.Size = New System.Drawing.Size(452, 150)
        Me.SprdExp.TabIndex = 21
        '
        'Frame7
        '
        Me.Frame7.BackColor = System.Drawing.SystemColors.Control
        Me.Frame7.Controls.Add(Me.txtSGSTRefundAmt)
        Me.Frame7.Controls.Add(Me.txtIGSTRefundAmt)
        Me.Frame7.Controls.Add(Me.txtCGSTRefundAmt)
        Me.Frame7.Controls.Add(Me.Label48)
        Me.Frame7.Controls.Add(Me.lblTotQty)
        Me.Frame7.Controls.Add(Me.Label44)
        Me.Frame7.Controls.Add(Me.Label20)
        Me.Frame7.Controls.Add(Me.lblTotPackQtyCap)
        Me.Frame7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame7.Location = New System.Drawing.Point(462, 233)
        Me.Frame7.Name = "Frame7"
        Me.Frame7.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame7.Size = New System.Drawing.Size(217, 105)
        Me.Frame7.TabIndex = 76
        Me.Frame7.TabStop = False
        '
        'txtSGSTRefundAmt
        '
        Me.txtSGSTRefundAmt.AcceptsReturn = True
        Me.txtSGSTRefundAmt.BackColor = System.Drawing.SystemColors.Window
        Me.txtSGSTRefundAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSGSTRefundAmt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSGSTRefundAmt.Enabled = False
        Me.txtSGSTRefundAmt.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSGSTRefundAmt.ForeColor = System.Drawing.Color.Blue
        Me.txtSGSTRefundAmt.Location = New System.Drawing.Point(136, 30)
        Me.txtSGSTRefundAmt.MaxLength = 0
        Me.txtSGSTRefundAmt.Name = "txtSGSTRefundAmt"
        Me.txtSGSTRefundAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSGSTRefundAmt.Size = New System.Drawing.Size(77, 20)
        Me.txtSGSTRefundAmt.TabIndex = 23
        '
        'txtIGSTRefundAmt
        '
        Me.txtIGSTRefundAmt.AcceptsReturn = True
        Me.txtIGSTRefundAmt.BackColor = System.Drawing.SystemColors.Window
        Me.txtIGSTRefundAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIGSTRefundAmt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIGSTRefundAmt.Enabled = False
        Me.txtIGSTRefundAmt.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIGSTRefundAmt.ForeColor = System.Drawing.Color.Blue
        Me.txtIGSTRefundAmt.Location = New System.Drawing.Point(136, 50)
        Me.txtIGSTRefundAmt.MaxLength = 0
        Me.txtIGSTRefundAmt.Name = "txtIGSTRefundAmt"
        Me.txtIGSTRefundAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIGSTRefundAmt.Size = New System.Drawing.Size(77, 20)
        Me.txtIGSTRefundAmt.TabIndex = 24
        '
        'txtCGSTRefundAmt
        '
        Me.txtCGSTRefundAmt.AcceptsReturn = True
        Me.txtCGSTRefundAmt.BackColor = System.Drawing.SystemColors.Window
        Me.txtCGSTRefundAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCGSTRefundAmt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCGSTRefundAmt.Enabled = False
        Me.txtCGSTRefundAmt.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCGSTRefundAmt.ForeColor = System.Drawing.Color.Blue
        Me.txtCGSTRefundAmt.Location = New System.Drawing.Point(136, 10)
        Me.txtCGSTRefundAmt.MaxLength = 0
        Me.txtCGSTRefundAmt.Name = "txtCGSTRefundAmt"
        Me.txtCGSTRefundAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCGSTRefundAmt.Size = New System.Drawing.Size(77, 20)
        Me.txtCGSTRefundAmt.TabIndex = 22
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.BackColor = System.Drawing.SystemColors.Control
        Me.Label48.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label48.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label48.Location = New System.Drawing.Point(4, 32)
        Me.Label48.Name = "Label48"
        Me.Label48.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label48.Size = New System.Drawing.Size(131, 14)
        Me.Label48.TabIndex = 88
        Me.Label48.Text = "SGST Refund Amount :"
        Me.Label48.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotQty
        '
        Me.lblTotQty.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotQty.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotQty.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotQty.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotQty.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotQty.Location = New System.Drawing.Point(132, -13)
        Me.lblTotQty.Name = "lblTotQty"
        Me.lblTotQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotQty.Size = New System.Drawing.Size(99, 17)
        Me.lblTotQty.TabIndex = 55
        Me.lblTotQty.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.BackColor = System.Drawing.SystemColors.Control
        Me.Label44.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label44.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label44.Location = New System.Drawing.Point(8, 52)
        Me.Label44.Name = "Label44"
        Me.Label44.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label44.Size = New System.Drawing.Size(127, 14)
        Me.Label44.TabIndex = 81
        Me.Label44.Text = "IGST Refund Amount :"
        Me.Label44.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(4, 12)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(132, 14)
        Me.Label20.TabIndex = 77
        Me.Label20.Text = "CGST Refund Amount :"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotPackQtyCap
        '
        Me.lblTotPackQtyCap.AutoSize = True
        Me.lblTotPackQtyCap.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotPackQtyCap.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotPackQtyCap.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotPackQtyCap.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotPackQtyCap.Location = New System.Drawing.Point(34, -12)
        Me.lblTotPackQtyCap.Name = "lblTotPackQtyCap"
        Me.lblTotPackQtyCap.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotPackQtyCap.Size = New System.Drawing.Size(60, 14)
        Me.lblTotPackQtyCap.TabIndex = 54
        Me.lblTotPackQtyCap.Text = "Total Qty :"
        Me.lblTotPackQtyCap.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 10)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(896, 186)
        Me.SprdMain.TabIndex = 107
        '
        'OLE1
        '
        Me.OLE1.BackColor = System.Drawing.Color.Red
        Me.OLE1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.OLE1.ForeColor = System.Drawing.Color.Black
        Me.OLE1.Location = New System.Drawing.Point(250, 26)
        Me.OLE1.Name = "OLE1"
        Me.OLE1.Size = New System.Drawing.Size(2, 2)
        Me.OLE1.TabIndex = 108
        Me.OLE1.Text = "OLE1"
        '
        'lblSHEPer
        '
        Me.lblSHEPer.BackColor = System.Drawing.SystemColors.Control
        Me.lblSHEPer.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSHEPer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSHEPer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSHEPer.Location = New System.Drawing.Point(460, 170)
        Me.lblSHEPer.Name = "lblSHEPer"
        Me.lblSHEPer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSHEPer.Size = New System.Drawing.Size(77, 13)
        Me.lblSHEPer.TabIndex = 103
        Me.lblSHEPer.Text = "lblSHEPer"
        Me.lblSHEPer.Visible = False
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.SystemColors.Control
        Me.Label29.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label29.Location = New System.Drawing.Point(753, 307)
        Me.Label29.Name = "Label29"
        Me.Label29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label29.Size = New System.Drawing.Size(54, 14)
        Me.Label29.TabIndex = 102
        Me.Label29.Text = "Others : "
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblOthersAmount
        '
        Me.lblOthersAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblOthersAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblOthersAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblOthersAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOthersAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblOthersAmount.Location = New System.Drawing.Point(812, 303)
        Me.lblOthersAmount.Name = "lblOthersAmount"
        Me.lblOthersAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblOthersAmount.Size = New System.Drawing.Size(85, 19)
        Me.lblOthersAmount.TabIndex = 101
        Me.lblOthersAmount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotSGST
        '
        Me.lblTotSGST.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotSGST.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotSGST.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotSGST.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotSGST.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotSGST.Location = New System.Drawing.Point(812, 263)
        Me.lblTotSGST.Name = "lblTotSGST"
        Me.lblTotSGST.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotSGST.Size = New System.Drawing.Size(85, 19)
        Me.lblTotSGST.TabIndex = 87
        Me.lblTotSGST.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(765, 267)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(42, 14)
        Me.Label2.TabIndex = 86
        Me.Label2.Text = "SGST :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
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
        Me.lblServicePercentage.TabIndex = 85
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
        Me.lblCESSableAmount.TabIndex = 84
        Me.lblCESSableAmount.Text = "lblCESSableAmount"
        Me.lblCESSableAmount.Visible = False
        '
        'lblTotIGST
        '
        Me.lblTotIGST.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotIGST.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotIGST.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotIGST.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotIGST.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotIGST.Location = New System.Drawing.Point(812, 283)
        Me.lblTotIGST.Name = "lblTotIGST"
        Me.lblTotIGST.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotIGST.Size = New System.Drawing.Size(85, 19)
        Me.lblTotIGST.TabIndex = 83
        Me.lblTotIGST.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.BackColor = System.Drawing.SystemColors.Control
        Me.Label47.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label47.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label47.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label47.Location = New System.Drawing.Point(766, 287)
        Me.Label47.Name = "Label47"
        Me.Label47.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label47.Size = New System.Drawing.Size(41, 14)
        Me.Label47.TabIndex = 82
        Me.Label47.Text = "IGST : "
        Me.Label47.TextAlign = System.Drawing.ContentAlignment.TopRight
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
        Me.lblEDUPercent.TabIndex = 80
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
        Me.lblMSC.TabIndex = 73
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
        Me.lblRO.TabIndex = 72
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
        Me.lblSurcharge.TabIndex = 71
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
        Me.lblDiscount.TabIndex = 70
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
        Me.lblTotTaxableAmt.TabIndex = 69
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
        Me.lblSTPercentage.TabIndex = 68
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
        Me.lblEDPercentage.TabIndex = 67
        Me.lblEDPercentage.Text = "0"
        Me.lblEDPercentage.Visible = False
        '
        'lblTotExpAmt
        '
        Me.lblTotExpAmt.AutoSize = True
        Me.lblTotExpAmt.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotExpAmt.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotExpAmt.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotExpAmt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTotExpAmt.Location = New System.Drawing.Point(380, 214)
        Me.lblTotExpAmt.Name = "lblTotExpAmt"
        Me.lblTotExpAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotExpAmt.Size = New System.Drawing.Size(13, 14)
        Me.lblTotExpAmt.TabIndex = 66
        Me.lblTotExpAmt.Text = "0"
        Me.lblTotExpAmt.Visible = False
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
        Me.lblTotFreight.TabIndex = 65
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
        Me.lblTotCharges.TabIndex = 64
        Me.lblTotCharges.Text = "0"
        Me.lblTotCharges.Visible = False
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
        Me.LblMKey.TabIndex = 62
        Me.LblMKey.Text = "LblMKey"
        Me.LblMKey.Visible = False
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label17.Location = New System.Drawing.Point(764, 249)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(43, 14)
        Me.Label17.TabIndex = 61
        Me.Label17.Text = "CGST :"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotCGST
        '
        Me.lblTotCGST.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotCGST.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotCGST.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotCGST.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotCGST.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotCGST.Location = New System.Drawing.Point(812, 245)
        Me.lblTotCGST.Name = "lblTotCGST"
        Me.lblTotCGST.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotCGST.Size = New System.Drawing.Size(85, 19)
        Me.lblTotCGST.TabIndex = 60
        Me.lblTotCGST.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label16.Location = New System.Drawing.Point(736, 229)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(71, 14)
        Me.Label16.TabIndex = 59
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
        Me.lblTotItemValue.Location = New System.Drawing.Point(812, 228)
        Me.lblTotItemValue.Name = "lblTotItemValue"
        Me.lblTotItemValue.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotItemValue.Size = New System.Drawing.Size(85, 17)
        Me.lblTotItemValue.TabIndex = 58
        Me.lblTotItemValue.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label13.Location = New System.Drawing.Point(729, 329)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(78, 14)
        Me.Label13.TabIndex = 57
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
        Me.lblNetAmount.Location = New System.Drawing.Point(812, 325)
        Me.lblNetAmount.Name = "lblNetAmount"
        Me.lblNetAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblNetAmount.Size = New System.Drawing.Size(85, 19)
        Me.lblNetAmount.TabIndex = 56
        Me.lblNetAmount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_SSTab1_TabPage1
        '
        Me._SSTab1_TabPage1.Controls.Add(Me.Frame1)
        Me._SSTab1_TabPage1.Location = New System.Drawing.Point(4, 22)
        Me._SSTab1_TabPage1.Name = "_SSTab1_TabPage1"
        Me._SSTab1_TabPage1.Size = New System.Drawing.Size(900, 375)
        Me._SSTab1_TabPage1.TabIndex = 1
        Me._SSTab1_TabPage1.Text = "Other Details"
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.Frame8)
        Me.Frame1.Controls.Add(Me.txtPaymentdate)
        Me.Frame1.Controls.Add(Me.txtTariff)
        Me.Frame1.Controls.Add(Me.txtNarration)
        Me.Frame1.Controls.Add(Me.txtRemarks)
        Me.Frame1.Controls.Add(Me.txtItemType)
        Me.Frame1.Controls.Add(Me.Label7)
        Me.Frame1.Controls.Add(Me.Label9)
        Me.Frame1.Controls.Add(Me.Label32)
        Me.Frame1.Controls.Add(Me.Label26)
        Me.Frame1.Controls.Add(Me.Label11)
        Me.Frame1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(900, 375)
        Me.Frame1.TabIndex = 49
        Me.Frame1.TabStop = False
        '
        'txtPaymentdate
        '
        Me.txtPaymentdate.AcceptsReturn = True
        Me.txtPaymentdate.BackColor = System.Drawing.SystemColors.Window
        Me.txtPaymentdate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPaymentdate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPaymentdate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaymentdate.ForeColor = System.Drawing.Color.Blue
        Me.txtPaymentdate.Location = New System.Drawing.Point(96, 120)
        Me.txtPaymentdate.MaxLength = 0
        Me.txtPaymentdate.Name = "txtPaymentdate"
        Me.txtPaymentdate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPaymentdate.Size = New System.Drawing.Size(73, 20)
        Me.txtPaymentdate.TabIndex = 29
        '
        'txtTariff
        '
        Me.txtTariff.AcceptsReturn = True
        Me.txtTariff.BackColor = System.Drawing.SystemColors.Window
        Me.txtTariff.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTariff.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTariff.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTariff.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTariff.Location = New System.Drawing.Point(77, 16)
        Me.txtTariff.MaxLength = 0
        Me.txtTariff.Name = "txtTariff"
        Me.txtTariff.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTariff.Size = New System.Drawing.Size(113, 20)
        Me.txtTariff.TabIndex = 25
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
        Me.txtNarration.Location = New System.Drawing.Point(76, 94)
        Me.txtNarration.MaxLength = 0
        Me.txtNarration.Name = "txtNarration"
        Me.txtNarration.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNarration.Size = New System.Drawing.Size(267, 20)
        Me.txtNarration.TabIndex = 28
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRemarks.Location = New System.Drawing.Point(76, 68)
        Me.txtRemarks.MaxLength = 0
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.Size = New System.Drawing.Size(267, 20)
        Me.txtRemarks.TabIndex = 27
        '
        'txtItemType
        '
        Me.txtItemType.AcceptsReturn = True
        Me.txtItemType.BackColor = System.Drawing.SystemColors.Window
        Me.txtItemType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemType.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItemType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtItemType.Location = New System.Drawing.Point(76, 42)
        Me.txtItemType.MaxLength = 0
        Me.txtItemType.Name = "txtItemType"
        Me.txtItemType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemType.Size = New System.Drawing.Size(267, 20)
        Me.txtItemType.TabIndex = 26
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(4, 122)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(88, 14)
        Me.Label7.TabIndex = 90
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
        Me.Label9.Location = New System.Drawing.Point(4, 18)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(41, 14)
        Me.Label9.TabIndex = 89
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
        Me.Label32.Location = New System.Drawing.Point(4, 96)
        Me.Label32.Name = "Label32"
        Me.Label32.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label32.Size = New System.Drawing.Size(63, 14)
        Me.Label32.TabIndex = 52
        Me.Label32.Text = "Narration :"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.SystemColors.Control
        Me.Label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(4, 70)
        Me.Label26.Name = "Label26"
        Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label26.Size = New System.Drawing.Size(63, 14)
        Me.Label26.TabIndex = 51
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
        Me.Label11.Location = New System.Drawing.Point(4, 44)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(67, 14)
        Me.Label11.TabIndex = 50
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
        Me.txtDebitAccount.Location = New System.Drawing.Point(452, 66)
        Me.txtDebitAccount.MaxLength = 0
        Me.txtDebitAccount.Name = "txtDebitAccount"
        Me.txtDebitAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDebitAccount.Size = New System.Drawing.Size(448, 20)
        Me.txtDebitAccount.TabIndex = 20
        '
        'txtSupplier
        '
        Me.txtSupplier.AcceptsReturn = True
        Me.txtSupplier.BackColor = System.Drawing.SystemColors.Window
        Me.txtSupplier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplier.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSupplier.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplier.ForeColor = System.Drawing.Color.Blue
        Me.txtSupplier.Location = New System.Drawing.Point(452, 40)
        Me.txtSupplier.MaxLength = 0
        Me.txtSupplier.Name = "txtSupplier"
        Me.txtSupplier.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSupplier.Size = New System.Drawing.Size(448, 20)
        Me.txtSupplier.TabIndex = 19
        '
        'txtBillDate
        '
        Me.txtBillDate.AcceptsReturn = True
        Me.txtBillDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillDate.ForeColor = System.Drawing.Color.Blue
        Me.txtBillDate.Location = New System.Drawing.Point(235, 36)
        Me.txtBillDate.MaxLength = 0
        Me.txtBillDate.Name = "txtBillDate"
        Me.txtBillDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillDate.Size = New System.Drawing.Size(69, 20)
        Me.txtBillDate.TabIndex = 4
        '
        'cboInvType
        '
        Me.cboInvType.BackColor = System.Drawing.SystemColors.Window
        Me.cboInvType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboInvType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboInvType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboInvType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboInvType.Location = New System.Drawing.Point(452, 12)
        Me.cboInvType.Name = "cboInvType"
        Me.cboInvType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboInvType.Size = New System.Drawing.Size(448, 22)
        Me.cboInvType.TabIndex = 18
        '
        'txtBillNo
        '
        Me.txtBillNo.AcceptsReturn = True
        Me.txtBillNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtBillNo.Location = New System.Drawing.Point(83, 40)
        Me.txtBillNo.MaxLength = 0
        Me.txtBillNo.Name = "txtBillNo"
        Me.txtBillNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillNo.Size = New System.Drawing.Size(110, 20)
        Me.txtBillNo.TabIndex = 3
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.SystemColors.Control
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(8, 153)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(93, 14)
        Me.Label21.TabIndex = 126
        Me.Label21.Text = "Goods/Service :"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(546, 124)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(37, 14)
        Me.Label19.TabIndex = 125
        Me.Label19.Text = "Date :"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(371, 124)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(76, 13)
        Me.Label8.TabIndex = 124
        Me.Label8.Text = "Original Bill :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblClaimStatus
        '
        Me.lblClaimStatus.BackColor = System.Drawing.SystemColors.Control
        Me.lblClaimStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblClaimStatus.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClaimStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblClaimStatus.Location = New System.Drawing.Point(738, 132)
        Me.lblClaimStatus.Name = "lblClaimStatus"
        Me.lblClaimStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblClaimStatus.Size = New System.Drawing.Size(73, 13)
        Me.lblClaimStatus.TabIndex = 123
        Me.lblClaimStatus.Text = "lblClaimStatus"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(8, 124)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(73, 14)
        Me.Label18.TabIndex = 122
        Me.Label18.Text = "GST Status :"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LblBookCode
        '
        Me.LblBookCode.BackColor = System.Drawing.SystemColors.Control
        Me.LblBookCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblBookCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBookCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblBookCode.Location = New System.Drawing.Point(590, 128)
        Me.LblBookCode.Name = "LblBookCode"
        Me.LblBookCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblBookCode.Size = New System.Drawing.Size(31, 14)
        Me.LblBookCode.TabIndex = 117
        Me.LblBookCode.Text = "LblBookCode"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(546, 153)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(37, 14)
        Me.Label10.TabIndex = 116
        Me.Label10.Text = "Date :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.BackColor = System.Drawing.SystemColors.Control
        Me.Label36.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label36.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label36.Location = New System.Drawing.Point(377, 97)
        Me.Label36.Name = "Label36"
        Me.Label36.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label36.Size = New System.Drawing.Size(70, 14)
        Me.Label36.TabIndex = 112
        Me.Label36.Text = "Amend No :"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.BackColor = System.Drawing.SystemColors.Control
        Me.Label35.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label35.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label35.Location = New System.Drawing.Point(24, 69)
        Me.Label35.Name = "Label35"
        Me.Label35.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label35.Size = New System.Drawing.Size(56, 14)
        Me.Label35.TabIndex = 110
        Me.Label35.Text = "Division :"
        Me.Label35.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.SystemColors.Control
        Me.Label27.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label27.Location = New System.Drawing.Point(656, 97)
        Me.Label27.Name = "Label27"
        Me.Label27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label27.Size = New System.Drawing.Size(53, 14)
        Me.Label27.TabIndex = 99
        Me.Label27.Text = "To Date :"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(358, 153)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(92, 13)
        Me.Label14.TabIndex = 95
        Me.Label14.Text = "GST No :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(515, 97)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(69, 14)
        Me.Label15.TabIndex = 92
        Me.Label15.Text = "From Date :"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(5, 97)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(76, 13)
        Me.Label4.TabIndex = 91
        Me.Label4.Text = "PO No :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
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
        Me.lblPurchaseVNo.TabIndex = 79
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
        Me.lblVNo.TabIndex = 75
        Me.lblVNo.Text = "Voucher No :"
        Me.lblVNo.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(195, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(37, 14)
        Me.Label6.TabIndex = 74
        Me.Label6.Text = "Date :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(197, 95)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(37, 14)
        Me.Label12.TabIndex = 63
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
        Me.Label3.Location = New System.Drawing.Point(386, 69)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(61, 14)
        Me.Label3.TabIndex = 47
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
        Me.lblCust.Location = New System.Drawing.Point(388, 44)
        Me.lblCust.Name = "lblCust"
        Me.lblCust.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCust.Size = New System.Drawing.Size(59, 14)
        Me.lblCust.TabIndex = 46
        Me.lblCust.Text = "Supplier :"
        Me.lblCust.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(194, 38)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(37, 14)
        Me.Label5.TabIndex = 45
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
        Me.lblInvType.Location = New System.Drawing.Point(366, 14)
        Me.lblInvType.Name = "lblInvType"
        Me.lblInvType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblInvType.Size = New System.Drawing.Size(81, 14)
        Me.lblInvType.TabIndex = 38
        Me.lblInvType.Text = "Invoice Type :"
        Me.lblInvType.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(5, 43)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(76, 13)
        Me.Label1.TabIndex = 44
        Me.Label1.Text = "Bill No :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'AdoDCMain
        '
        Me.AdoDCMain.BackColor = System.Drawing.SystemColors.Window
        Me.AdoDCMain.CommandTimeout = 0
        Me.AdoDCMain.CommandType = ADODB.CommandTypeEnum.adCmdUnknown
        Me.AdoDCMain.ConnectionString = Nothing
        Me.AdoDCMain.CursorType = ADODB.CursorTypeEnum.adOpenStatic
        Me.AdoDCMain.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AdoDCMain.ForeColor = System.Drawing.SystemColors.WindowText
        Me.AdoDCMain.Location = New System.Drawing.Point(56, 270)
        Me.AdoDCMain.LockType = ADODB.LockTypeEnum.adLockOptimistic
        Me.AdoDCMain.Name = "AdoDCMain"
        Me.AdoDCMain.Size = New System.Drawing.Size(313, 33)
        Me.AdoDCMain.TabIndex = 120
        Me.AdoDCMain.Text = "Adodc1"
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 1)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(908, 571)
        Me.SprdView.TabIndex = 40
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.cmdAdd)
        Me.Frame3.Controls.Add(Me.cmdShow)
        Me.Frame3.Controls.Add(Me.cmdClose)
        Me.Frame3.Controls.Add(Me.CmdView)
        Me.Frame3.Controls.Add(Me.CmdPreview)
        Me.Frame3.Controls.Add(Me.cmdPrint)
        Me.Frame3.Controls.Add(Me.cmdSavePrint)
        Me.Frame3.Controls.Add(Me.cmdDelete)
        Me.Frame3.Controls.Add(Me.cmdSave)
        Me.Frame3.Controls.Add(Me.Report1)
        Me.Frame3.Controls.Add(Me.cmdPostingHead)
        Me.Frame3.Controls.Add(Me.cmdModify)
        Me.Frame3.Controls.Add(Me.lblBookType)
        Me.Frame3.Controls.Add(Me.lblPMKey)
        Me.Frame3.Controls.Add(Me.lblSODates)
        Me.Frame3.Controls.Add(Me.lblSONos)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 570)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(908, 51)
        Me.Frame3.TabIndex = 39
        Me.Frame3.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(14, 12)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 97
        '
        'lblBookType
        '
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(720, 22)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(25, 19)
        Me.lblBookType.TabIndex = 100
        Me.lblBookType.Text = "lblBookType"
        Me.lblBookType.Visible = False
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
        Me.lblPMKey.TabIndex = 78
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
        Me.lblSODates.TabIndex = 42
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
        Me.lblSONos.TabIndex = 41
        Me.lblSONos.Text = "lblSONos"
        Me.lblSONos.Visible = False
        '
        'optBaseOn
        '
        '
        'Frame8
        '
        Me.Frame8.BackColor = System.Drawing.SystemColors.Control
        Me.Frame8.Controls.Add(Me.lblJVTMKey)
        Me.Frame8.Controls.Add(Me.Label50)
        Me.Frame8.Controls.Add(Me.txtSection)
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
        Me.Frame8.Location = New System.Drawing.Point(377, 0)
        Me.Frame8.Name = "Frame8"
        Me.Frame8.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame8.Size = New System.Drawing.Size(525, 96)
        Me.Frame8.TabIndex = 129
        Me.Frame8.TabStop = False
        Me.Frame8.Text = "Deduction"
        '
        'lblJVTMKey
        '
        Me.lblJVTMKey.AutoSize = True
        Me.lblJVTMKey.BackColor = System.Drawing.SystemColors.Control
        Me.lblJVTMKey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblJVTMKey.Enabled = False
        Me.lblJVTMKey.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJVTMKey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblJVTMKey.Location = New System.Drawing.Point(272, 74)
        Me.lblJVTMKey.Name = "lblJVTMKey"
        Me.lblJVTMKey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblJVTMKey.Size = New System.Drawing.Size(63, 14)
        Me.lblJVTMKey.TabIndex = 206
        Me.lblJVTMKey.Text = "lblJVTMKey"
        Me.lblJVTMKey.Visible = False
        '
        'Label50
        '
        Me.Label50.AutoSize = True
        Me.Label50.BackColor = System.Drawing.SystemColors.Control
        Me.Label50.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label50.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label50.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label50.Location = New System.Drawing.Point(270, 8)
        Me.Label50.Name = "Label50"
        Me.Label50.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label50.Size = New System.Drawing.Size(43, 14)
        Me.Label50.TabIndex = 135
        Me.Label50.Text = "Section"
        '
        'txtSection
        '
        Me.txtSection.AcceptsReturn = True
        Me.txtSection.BackColor = System.Drawing.SystemColors.Window
        Me.txtSection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSection.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSection.Enabled = False
        Me.txtSection.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSection.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSection.Location = New System.Drawing.Point(270, 24)
        Me.txtSection.MaxLength = 0
        Me.txtSection.Name = "txtSection"
        Me.txtSection.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSection.Size = New System.Drawing.Size(63, 20)
        Me.txtSection.TabIndex = 134
        '
        'ChkSTDSRO
        '
        Me.ChkSTDSRO.BackColor = System.Drawing.SystemColors.Control
        Me.ChkSTDSRO.Checked = True
        Me.ChkSTDSRO.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChkSTDSRO.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkSTDSRO.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkSTDSRO.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkSTDSRO.Location = New System.Drawing.Point(250, 68)
        Me.ChkSTDSRO.Name = "ChkSTDSRO"
        Me.ChkSTDSRO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkSTDSRO.Size = New System.Drawing.Size(15, 13)
        Me.ChkSTDSRO.TabIndex = 43
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
        Me.ChkESIRO.Location = New System.Drawing.Point(250, 47)
        Me.ChkESIRO.Name = "ChkESIRO"
        Me.ChkESIRO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkESIRO.Size = New System.Drawing.Size(15, 13)
        Me.ChkESIRO.TabIndex = 38
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
        Me.ChkTDSRO.TabIndex = 33
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
        Me.txtSTDSDeductOn.Location = New System.Drawing.Point(62, 68)
        Me.txtSTDSDeductOn.MaxLength = 0
        Me.txtSTDSDeductOn.Name = "txtSTDSDeductOn"
        Me.txtSTDSDeductOn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSTDSDeductOn.Size = New System.Drawing.Size(75, 20)
        Me.txtSTDSDeductOn.TabIndex = 40
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
        Me.txtESIDeductOn.Location = New System.Drawing.Point(62, 46)
        Me.txtESIDeductOn.MaxLength = 0
        Me.txtESIDeductOn.Name = "txtESIDeductOn"
        Me.txtESIDeductOn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtESIDeductOn.Size = New System.Drawing.Size(75, 20)
        Me.txtESIDeductOn.TabIndex = 35
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
        Me.txtTDSDeductOn.TabIndex = 30
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
        Me.txtJVVNO.Location = New System.Drawing.Point(328, 46)
        Me.txtJVVNO.MaxLength = 0
        Me.txtJVVNO.Name = "txtJVVNO"
        Me.txtJVVNO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtJVVNO.Size = New System.Drawing.Size(158, 20)
        Me.txtJVVNO.TabIndex = 44
        '
        'ChkSTDS
        '
        Me.ChkSTDS.AutoSize = True
        Me.ChkSTDS.BackColor = System.Drawing.SystemColors.Control
        Me.ChkSTDS.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkSTDS.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkSTDS.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkSTDS.Location = New System.Drawing.Point(6, 68)
        Me.ChkSTDS.Name = "ChkSTDS"
        Me.ChkSTDS.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkSTDS.Size = New System.Drawing.Size(53, 18)
        Me.ChkSTDS.TabIndex = 39
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
        Me.txtSTDSRate.Location = New System.Drawing.Point(138, 68)
        Me.txtSTDSRate.MaxLength = 0
        Me.txtSTDSRate.Name = "txtSTDSRate"
        Me.txtSTDSRate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSTDSRate.Size = New System.Drawing.Size(47, 20)
        Me.txtSTDSRate.TabIndex = 41
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
        Me.txtSTDSAmount.Location = New System.Drawing.Point(186, 68)
        Me.txtSTDSAmount.MaxLength = 0
        Me.txtSTDSAmount.Name = "txtSTDSAmount"
        Me.txtSTDSAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSTDSAmount.Size = New System.Drawing.Size(63, 20)
        Me.txtSTDSAmount.TabIndex = 42
        Me.txtSTDSAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'chkESI
        '
        Me.chkESI.AutoSize = True
        Me.chkESI.BackColor = System.Drawing.SystemColors.Control
        Me.chkESI.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkESI.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkESI.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkESI.Location = New System.Drawing.Point(6, 47)
        Me.chkESI.Name = "chkESI"
        Me.chkESI.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkESI.Size = New System.Drawing.Size(41, 18)
        Me.chkESI.TabIndex = 34
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
        Me.txtESIRate.Location = New System.Drawing.Point(138, 46)
        Me.txtESIRate.MaxLength = 0
        Me.txtESIRate.Name = "txtESIRate"
        Me.txtESIRate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtESIRate.Size = New System.Drawing.Size(47, 20)
        Me.txtESIRate.TabIndex = 36
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
        Me.txtESIAmount.Location = New System.Drawing.Point(186, 46)
        Me.txtESIAmount.MaxLength = 0
        Me.txtESIAmount.Name = "txtESIAmount"
        Me.txtESIAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtESIAmount.Size = New System.Drawing.Size(63, 20)
        Me.txtESIAmount.TabIndex = 37
        Me.txtESIAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'chkTDS
        '
        Me.chkTDS.AutoSize = True
        Me.chkTDS.BackColor = System.Drawing.SystemColors.Control
        Me.chkTDS.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkTDS.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTDS.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkTDS.Location = New System.Drawing.Point(6, 26)
        Me.chkTDS.Name = "chkTDS"
        Me.chkTDS.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkTDS.Size = New System.Drawing.Size(46, 18)
        Me.chkTDS.TabIndex = 29
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
        Me.txtTDSRate.TabIndex = 31
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
        Me.txtTDSAmount.TabIndex = 32
        Me.txtTDSAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.BackColor = System.Drawing.SystemColors.Control
        Me.Label42.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label42.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label42.Location = New System.Drawing.Point(248, 10)
        Me.Label42.Name = "Label42"
        Me.Label42.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label42.Size = New System.Drawing.Size(20, 14)
        Me.Label42.TabIndex = 133
        Me.Label42.Text = "Rd"
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.BackColor = System.Drawing.SystemColors.Control
        Me.Label46.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label46.Enabled = False
        Me.Label46.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label46.Location = New System.Drawing.Point(272, 49)
        Me.Label46.Name = "Label46"
        Me.Label46.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label46.Size = New System.Drawing.Size(50, 14)
        Me.Label46.TabIndex = 132
        Me.Label46.Text = "JV VNo :"
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.BackColor = System.Drawing.SystemColors.Control
        Me.Label43.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label43.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label43.Location = New System.Drawing.Point(64, 10)
        Me.Label43.Name = "Label43"
        Me.Label43.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label43.Size = New System.Drawing.Size(58, 14)
        Me.Label43.TabIndex = 131
        Me.Label43.Text = "Deduct On"
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.BackColor = System.Drawing.SystemColors.Control
        Me.Label40.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label40.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label40.Location = New System.Drawing.Point(138, 10)
        Me.Label40.Name = "Label40"
        Me.Label40.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label40.Size = New System.Drawing.Size(47, 14)
        Me.Label40.TabIndex = 130
        Me.Label40.Text = "Rate(%)"
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.BackColor = System.Drawing.SystemColors.Control
        Me.Label41.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label41.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label41.Location = New System.Drawing.Point(194, 10)
        Me.Label41.Name = "Label41"
        Me.Label41.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label41.Size = New System.Drawing.Size(44, 14)
        Me.Label41.TabIndex = 129
        Me.Label41.Text = "Amount"
        '
        'FrmSupp_PurchaseGST
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(910, 621)
        Me.Controls.Add(Me.FraAcctPostDetail)
        Me.Controls.Add(Me.FraFront)
        Me.Controls.Add(Me.AdoDCMain)
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
        Me.Name = "FrmSupp_PurchaseGST"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Purchase Supplementary Invoice"
        Me.FraAcctPostDetail.ResumeLayout(False)
        CType(Me.sprdAcctPostDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraFront.ResumeLayout(False)
        Me.FraFront.PerformLayout()
        Me.FraPostingDtl.ResumeLayout(False)
        Me.FraPostingDtl.PerformLayout()
        CType(Me.SprdPostingDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SSTab1.ResumeLayout(False)
        Me._SSTab1_TabPage0.ResumeLayout(False)
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        CType(Me.SprdExp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame7.ResumeLayout(False)
        Me.Frame7.PerformLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage1.ResumeLayout(False)
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame3.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optBaseOn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame8.ResumeLayout(False)
        Me.Frame8.PerformLayout()
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

    Public WithEvents txtLocationID As TextBox
    Public WithEvents CmdPopFromFile As Button
    Public WithEvents CommonDialogOpen As OpenFileDialog
    Public WithEvents CommonDialogSave As SaveFileDialog
    Public WithEvents CommonDialogPrint As PrintDialog
    Public WithEvents CommonDialogColor As ColorDialog
    Public WithEvents CommonDialogFont As FontDialog
    Public WithEvents Frame8 As GroupBox
    Public WithEvents lblJVTMKey As Label
    Public WithEvents Label50 As Label
    Public WithEvents txtSection As TextBox
    Public WithEvents ChkSTDSRO As CheckBox
    Public WithEvents ChkESIRO As CheckBox
    Public WithEvents ChkTDSRO As CheckBox
    Public WithEvents txtSTDSDeductOn As TextBox
    Public WithEvents txtESIDeductOn As TextBox
    Public WithEvents txtTDSDeductOn As TextBox
    Public WithEvents txtJVVNO As TextBox
    Public WithEvents ChkSTDS As CheckBox
    Public WithEvents txtSTDSRate As TextBox
    Public WithEvents txtSTDSAmount As TextBox
    Public WithEvents chkESI As CheckBox
    Public WithEvents txtESIRate As TextBox
    Public WithEvents txtESIAmount As TextBox
    Public WithEvents chkTDS As CheckBox
    Public WithEvents txtTDSRate As TextBox
    Public WithEvents txtTDSAmount As TextBox
    Public WithEvents Label42 As Label
    Public WithEvents Label46 As Label
    Public WithEvents Label43 As Label
    Public WithEvents Label40 As Label
    Public WithEvents Label41 As Label
#End Region
End Class