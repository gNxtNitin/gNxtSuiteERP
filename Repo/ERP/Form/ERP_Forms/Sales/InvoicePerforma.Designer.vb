Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmInvoicePerforma
#Region "Windows Form Designer generated code "
	<System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
		MyBase.New()
		'This call is required by the Windows Form Designer.
		InitializeComponent()
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
	Public WithEvents cmdsearch As System.Windows.Forms.Button
	Public WithEvents cboDivision As System.Windows.Forms.ComboBox
	Public WithEvents txtPONo As System.Windows.Forms.TextBox
	Public WithEvents txtPODate As System.Windows.Forms.TextBox
	Public WithEvents chkCancelled As System.Windows.Forms.CheckBox
    Public WithEvents SprdExp As AxFPSpreadADO.AxfpSpread
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
	Public WithEvents lblTotTaxableAmt As System.Windows.Forms.Label
	Public WithEvents _Label_9 As System.Windows.Forms.Label
	Public WithEvents _Label_22 As System.Windows.Forms.Label
	Public WithEvents lblTotItemValue As System.Windows.Forms.Label
	Public WithEvents lblTotQty As System.Windows.Forms.Label
	Public WithEvents lblTotPackQtyCap As System.Windows.Forms.Label
	Public WithEvents _Label_29 As System.Windows.Forms.Label
	Public WithEvents _Label_25 As System.Windows.Forms.Label
	Public WithEvents lblTotSGSTAmount As System.Windows.Forms.Label
	Public WithEvents _Label_26 As System.Windows.Forms.Label
	Public WithEvents lblTotIGSTAmount As System.Windows.Forms.Label
	Public WithEvents lblTCS As System.Windows.Forms.Label
	Public WithEvents lblTCSPercentage As System.Windows.Forms.Label
	Public WithEvents lblRO As System.Windows.Forms.Label
	Public WithEvents lblTotExpAmt As System.Windows.Forms.Label
	Public WithEvents LblMKey As System.Windows.Forms.Label
	Public WithEvents _Label_24 As System.Windows.Forms.Label
	Public WithEvents lblTotCGSTAmount As System.Windows.Forms.Label
	Public WithEvents lblNetAmount As System.Windows.Forms.Label
	Public WithEvents Frame6 As System.Windows.Forms.GroupBox
	Public WithEvents _TabMain_TabPage0 As System.Windows.Forms.TabPage
	Public WithEvents txtAdvAdjust As System.Windows.Forms.TextBox
	Public WithEvents txtAdvVNo As System.Windows.Forms.TextBox
	Public WithEvents txtAdvDate As System.Windows.Forms.TextBox
	Public WithEvents txtAdvIGST As System.Windows.Forms.TextBox
	Public WithEvents txtAdvSGST As System.Windows.Forms.TextBox
	Public WithEvents txtAdvCGST As System.Windows.Forms.TextBox
	Public WithEvents txtItemAdvAdjust As System.Windows.Forms.TextBox
	Public WithEvents txtAdvBal As System.Windows.Forms.TextBox
	Public WithEvents txtAdvCGSTBal As System.Windows.Forms.TextBox
	Public WithEvents txtAdvSGSTBal As System.Windows.Forms.TextBox
	Public WithEvents txtAdvIGSTBal As System.Windows.Forms.TextBox
	Public WithEvents Label24 As System.Windows.Forms.Label
	Public WithEvents Label23 As System.Windows.Forms.Label
	Public WithEvents Label18 As System.Windows.Forms.Label
	Public WithEvents Label22 As System.Windows.Forms.Label
	Public WithEvents Label21 As System.Windows.Forms.Label
	Public WithEvents Label8 As System.Windows.Forms.Label
	Public WithEvents Label10 As System.Windows.Forms.Label
	Public WithEvents Label16 As System.Windows.Forms.Label
	Public WithEvents Label25 As System.Windows.Forms.Label
	Public WithEvents Label28 As System.Windows.Forms.Label
	Public WithEvents Label34 As System.Windows.Forms.Label
	Public WithEvents Frame11 As System.Windows.Forms.GroupBox
	Public WithEvents chkShipTo As System.Windows.Forms.CheckBox
	Public WithEvents cmdSearchShippedTo As System.Windows.Forms.Button
	Public WithEvents txtShippedTo As System.Windows.Forms.TextBox
	Public WithEvents chkExWork As System.Windows.Forms.CheckBox
	Public WithEvents chkDespatchFrom As System.Windows.Forms.CheckBox
	Public WithEvents txtShippedFrom As System.Windows.Forms.TextBox
	Public WithEvents cmdSearchDespatchFrom As System.Windows.Forms.Button
	Public WithEvents _OptFreight_0 As System.Windows.Forms.RadioButton
	Public WithEvents _OptFreight_1 As System.Windows.Forms.RadioButton
	Public WithEvents Frame8 As System.Windows.Forms.GroupBox
	Public WithEvents _txtCreditDays_0 As System.Windows.Forms.TextBox
	Public WithEvents _txtCreditDays_1 As System.Windows.Forms.TextBox
	Public WithEvents Label33 As System.Windows.Forms.Label
	Public WithEvents Label35 As System.Windows.Forms.Label
	Public WithEvents Frame7 As System.Windows.Forms.GroupBox
	Public WithEvents txtNarration As System.Windows.Forms.TextBox
	Public WithEvents txtDocsThru As System.Windows.Forms.TextBox
	Public WithEvents txtRemarks As System.Windows.Forms.TextBox
	Public WithEvents Label50 As System.Windows.Forms.Label
	Public WithEvents Label4 As System.Windows.Forms.Label
	Public WithEvents Label32 As System.Windows.Forms.Label
	Public WithEvents Label31 As System.Windows.Forms.Label
	Public WithEvents Label26 As System.Windows.Forms.Label
	Public WithEvents Frame1 As System.Windows.Forms.GroupBox
	Public WithEvents txtMode As System.Windows.Forms.TextBox
	Public WithEvents txtVehicle As System.Windows.Forms.TextBox
	Public WithEvents txtCarriers As System.Windows.Forms.TextBox
	Public WithEvents cboTransmode As System.Windows.Forms.ComboBox
	Public WithEvents Label27 As System.Windows.Forms.Label
	Public WithEvents Label29 As System.Windows.Forms.Label
	Public WithEvents Label30 As System.Windows.Forms.Label
	Public WithEvents Label49 As System.Windows.Forms.Label
	Public WithEvents Frame12 As System.Windows.Forms.GroupBox
	Public WithEvents _TabMain_TabPage1 As System.Windows.Forms.TabPage
    Public WithEvents TabMain As System.Windows.Forms.TabControl
    Public WithEvents txtCustomer As System.Windows.Forms.TextBox
	Public WithEvents txtBillDate As System.Windows.Forms.TextBox
	Public WithEvents TxtBillTm As System.Windows.Forms.TextBox
	Public WithEvents txtBillNoPrefix As System.Windows.Forms.TextBox
	Public WithEvents txtBillNoSuffix As System.Windows.Forms.TextBox
	Public WithEvents txtBillNo As System.Windows.Forms.TextBox
	Public WithEvents Label13 As System.Windows.Forms.Label
	Public WithEvents lblSoDate As System.Windows.Forms.Label
    Public WithEvents Label36 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
	Public WithEvents lblCust As System.Windows.Forms.Label
	Public WithEvents Label5 As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents FraFront As System.Windows.Forms.GroupBox
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
	Public WithEvents CmdPreview As System.Windows.Forms.Button
	Public WithEvents cmdPrint As System.Windows.Forms.Button
	Public WithEvents cmdDelete As System.Windows.Forms.Button
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmInvoicePerforma))
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
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.cmdSearchShippedTo = New System.Windows.Forms.Button()
        Me.cmdSearchDespatchFrom = New System.Windows.Forms.Button()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdDelete = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdModify = New System.Windows.Forms.Button()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.cmdGetData = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.FraFront = New System.Windows.Forms.GroupBox()
        Me.cmdPopulateExcel = New System.Windows.Forms.Button()
        Me.lblPoNo = New System.Windows.Forms.TextBox()
        Me.cboCalcOn = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtBillTo = New System.Windows.Forms.TextBox()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.cboDivision = New System.Windows.Forms.ComboBox()
        Me.txtPONo = New System.Windows.Forms.TextBox()
        Me.txtPODate = New System.Windows.Forms.TextBox()
        Me.chkCancelled = New System.Windows.Forms.CheckBox()
        Me.TabMain = New System.Windows.Forms.TabControl()
        Me._TabMain_TabPage0 = New System.Windows.Forms.TabPage()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.SprdExp = New AxFPSpreadADO.AxfpSpread()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.lblTotTaxableAmt = New System.Windows.Forms.Label()
        Me._Label_9 = New System.Windows.Forms.Label()
        Me._Label_22 = New System.Windows.Forms.Label()
        Me.lblTotItemValue = New System.Windows.Forms.Label()
        Me.lblTotQty = New System.Windows.Forms.Label()
        Me.lblTotPackQtyCap = New System.Windows.Forms.Label()
        Me._Label_29 = New System.Windows.Forms.Label()
        Me._Label_25 = New System.Windows.Forms.Label()
        Me.lblTotSGSTAmount = New System.Windows.Forms.Label()
        Me._Label_26 = New System.Windows.Forms.Label()
        Me.lblTotIGSTAmount = New System.Windows.Forms.Label()
        Me.lblTCS = New System.Windows.Forms.Label()
        Me.lblTCSPercentage = New System.Windows.Forms.Label()
        Me.lblRO = New System.Windows.Forms.Label()
        Me.lblTotExpAmt = New System.Windows.Forms.Label()
        Me.LblMKey = New System.Windows.Forms.Label()
        Me._Label_24 = New System.Windows.Forms.Label()
        Me.lblTotCGSTAmount = New System.Windows.Forms.Label()
        Me.lblNetAmount = New System.Windows.Forms.Label()
        Me._TabMain_TabPage1 = New System.Windows.Forms.TabPage()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.cboSalePersonName = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TxtShipTo = New System.Windows.Forms.TextBox()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.Frame11 = New System.Windows.Forms.GroupBox()
        Me.txtAdvAdjust = New System.Windows.Forms.TextBox()
        Me.txtAdvVNo = New System.Windows.Forms.TextBox()
        Me.txtAdvDate = New System.Windows.Forms.TextBox()
        Me.txtAdvIGST = New System.Windows.Forms.TextBox()
        Me.txtAdvSGST = New System.Windows.Forms.TextBox()
        Me.txtAdvCGST = New System.Windows.Forms.TextBox()
        Me.txtItemAdvAdjust = New System.Windows.Forms.TextBox()
        Me.txtAdvBal = New System.Windows.Forms.TextBox()
        Me.txtAdvCGSTBal = New System.Windows.Forms.TextBox()
        Me.txtAdvSGSTBal = New System.Windows.Forms.TextBox()
        Me.txtAdvIGSTBal = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.chkShipTo = New System.Windows.Forms.CheckBox()
        Me.txtShippedTo = New System.Windows.Forms.TextBox()
        Me.chkExWork = New System.Windows.Forms.CheckBox()
        Me.chkDespatchFrom = New System.Windows.Forms.CheckBox()
        Me.txtShippedFrom = New System.Windows.Forms.TextBox()
        Me.Frame8 = New System.Windows.Forms.GroupBox()
        Me._OptFreight_0 = New System.Windows.Forms.RadioButton()
        Me._OptFreight_1 = New System.Windows.Forms.RadioButton()
        Me.Frame7 = New System.Windows.Forms.GroupBox()
        Me._txtCreditDays_0 = New System.Windows.Forms.TextBox()
        Me._txtCreditDays_1 = New System.Windows.Forms.TextBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.txtNarration = New System.Windows.Forms.TextBox()
        Me.txtDocsThru = New System.Windows.Forms.TextBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Frame12 = New System.Windows.Forms.GroupBox()
        Me.txtMode = New System.Windows.Forms.TextBox()
        Me.txtVehicle = New System.Windows.Forms.TextBox()
        Me.txtCarriers = New System.Windows.Forms.TextBox()
        Me.cboTransmode = New System.Windows.Forms.ComboBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.txtCustomer = New System.Windows.Forms.TextBox()
        Me.txtBillDate = New System.Windows.Forms.TextBox()
        Me.TxtBillTm = New System.Windows.Forms.TextBox()
        Me.txtBillNoPrefix = New System.Windows.Forms.TextBox()
        Me.txtBillNoSuffix = New System.Windows.Forms.TextBox()
        Me.txtBillNo = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblSoDate = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblCust = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.lblSODates = New System.Windows.Forms.Label()
        Me.lblSONos = New System.Windows.Forms.Label()
        Me.Label = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.OptFreight = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.txtCreditDays = New Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray(Me.components)
        Me.UltraGrid1 = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.CommonDialogFont = New System.Windows.Forms.FontDialog()
        Me.CommonDialogColor = New System.Windows.Forms.ColorDialog()
        Me.CommonDialogPrint = New System.Windows.Forms.PrintDialog()
        Me.CommonDialogSave = New System.Windows.Forms.SaveFileDialog()
        Me.CommonDialogOpen = New System.Windows.Forms.OpenFileDialog()
        Me.FraFront.SuspendLayout()
        Me.TabMain.SuspendLayout()
        Me._TabMain_TabPage0.SuspendLayout()
        Me.Frame6.SuspendLayout()
        CType(Me.SprdExp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._TabMain_TabPage1.SuspendLayout()
        Me.Frame1.SuspendLayout()
        CType(Me.cboSalePersonName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame11.SuspendLayout()
        Me.Frame8.SuspendLayout()
        Me.Frame7.SuspendLayout()
        Me.Frame12.SuspendLayout()
        Me.Frame3.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptFreight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCreditDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdsearch
        '
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.Location = New System.Drawing.Point(490, 10)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(23, 19)
        Me.cmdsearch.TabIndex = 2
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
        Me.cmdSearchShippedTo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchShippedTo.Image = CType(resources.GetObject("cmdSearchShippedTo.Image"), System.Drawing.Image)
        Me.cmdSearchShippedTo.Location = New System.Drawing.Point(518, 280)
        Me.cmdSearchShippedTo.Name = "cmdSearchShippedTo"
        Me.cmdSearchShippedTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchShippedTo.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchShippedTo.TabIndex = 78
        Me.cmdSearchShippedTo.TabStop = False
        Me.cmdSearchShippedTo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchShippedTo, "Search")
        Me.cmdSearchShippedTo.UseVisualStyleBackColor = False
        '
        'cmdSearchDespatchFrom
        '
        Me.cmdSearchDespatchFrom.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchDespatchFrom.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchDespatchFrom.Enabled = False
        Me.cmdSearchDespatchFrom.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchDespatchFrom.Image = CType(resources.GetObject("cmdSearchDespatchFrom.Image"), System.Drawing.Image)
        Me.cmdSearchDespatchFrom.Location = New System.Drawing.Point(518, 234)
        Me.cmdSearchDespatchFrom.Name = "cmdSearchDespatchFrom"
        Me.cmdSearchDespatchFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchDespatchFrom.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchDespatchFrom.TabIndex = 77
        Me.cmdSearchDespatchFrom.TabStop = False
        Me.cmdSearchDespatchFrom.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchDespatchFrom, "Search")
        Me.cmdSearchDespatchFrom.UseVisualStyleBackColor = False
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.BackColor = System.Drawing.SystemColors.Control
        Me.Label36.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label36.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label36.Location = New System.Drawing.Point(2, 58)
        Me.Label36.Name = "Label36"
        Me.Label36.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label36.Size = New System.Drawing.Size(92, 13)
        Me.Label36.TabIndex = 58
        Me.Label36.Text = "Customer PO No :"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.ToolTip1.SetToolTip(Me.Label36, "AWB/RRP No.")
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.Location = New System.Drawing.Point(768, 10)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(72, 37)
        Me.cmdClose.TabIndex = 33
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdClose, "Close the form")
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'CmdView
        '
        Me.CmdView.BackColor = System.Drawing.SystemColors.Menu
        Me.CmdView.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
        Me.CmdView.Location = New System.Drawing.Point(696, 10)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(72, 37)
        Me.CmdView.TabIndex = 32
        Me.CmdView.Text = "List &View"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdView, "View Listing")
        Me.CmdView.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(624, 10)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(72, 37)
        Me.CmdPreview.TabIndex = 31
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Preview")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(552, 10)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(72, 37)
        Me.cmdPrint.TabIndex = 30
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'cmdDelete
        '
        Me.cmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDelete.Image = CType(resources.GetObject("cmdDelete.Image"), System.Drawing.Image)
        Me.cmdDelete.Location = New System.Drawing.Point(399, 10)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDelete.Size = New System.Drawing.Size(72, 37)
        Me.cmdDelete.TabIndex = 28
        Me.cmdDelete.Text = "&Delete"
        Me.cmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdDelete, "Delete")
        Me.cmdDelete.UseVisualStyleBackColor = False
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Image = CType(resources.GetObject("cmdSave.Image"), System.Drawing.Image)
        Me.cmdSave.Location = New System.Drawing.Point(327, 10)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(72, 37)
        Me.cmdSave.TabIndex = 27
        Me.cmdSave.Text = "&Save"
        Me.cmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSave, "Save Current Record")
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'cmdModify
        '
        Me.cmdModify.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdModify.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdModify.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdModify.Image = CType(resources.GetObject("cmdModify.Image"), System.Drawing.Image)
        Me.cmdModify.Location = New System.Drawing.Point(255, 10)
        Me.cmdModify.Name = "cmdModify"
        Me.cmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdModify.Size = New System.Drawing.Size(72, 37)
        Me.cmdModify.TabIndex = 26
        Me.cmdModify.Text = "&Modify"
        Me.cmdModify.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdModify, "Modify ")
        Me.cmdModify.UseVisualStyleBackColor = False
        '
        'cmdAdd
        '
        Me.cmdAdd.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdAdd.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAdd.Image = CType(resources.GetObject("cmdAdd.Image"), System.Drawing.Image)
        Me.cmdAdd.Location = New System.Drawing.Point(183, 10)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAdd.Size = New System.Drawing.Size(72, 37)
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
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.Location = New System.Drawing.Point(471, 10)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(81, 37)
        Me.cmdSavePrint.TabIndex = 29
        Me.cmdSavePrint.Text = "F4- Print"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save & Print")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'cmdGetData
        '
        Me.cmdGetData.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdGetData.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdGetData.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdGetData.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdGetData.Location = New System.Drawing.Point(966, 10)
        Me.cmdGetData.Name = "cmdGetData"
        Me.cmdGetData.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdGetData.Size = New System.Drawing.Size(125, 46)
        Me.cmdGetData.TabIndex = 242
        Me.cmdGetData.TabStop = False
        Me.cmdGetData.Text = "Populate from Schedule"
        Me.ToolTip1.SetToolTip(Me.cmdGetData, "Search")
        Me.cmdGetData.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(548, 60)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(80, 13)
        Me.Label3.TabIndex = 247
        Me.Label3.Text = "Sale Order No :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.ToolTip1.SetToolTip(Me.Label3, "AWB/RRP No.")
        '
        'FraFront
        '
        Me.FraFront.BackColor = System.Drawing.SystemColors.Control
        Me.FraFront.Controls.Add(Me.cmdPopulateExcel)
        Me.FraFront.Controls.Add(Me.lblPoNo)
        Me.FraFront.Controls.Add(Me.Label3)
        Me.FraFront.Controls.Add(Me.cboCalcOn)
        Me.FraFront.Controls.Add(Me.Label2)
        Me.FraFront.Controls.Add(Me.cmdGetData)
        Me.FraFront.Controls.Add(Me.txtBillTo)
        Me.FraFront.Controls.Add(Me.Label62)
        Me.FraFront.Controls.Add(Me.cmdsearch)
        Me.FraFront.Controls.Add(Me.cboDivision)
        Me.FraFront.Controls.Add(Me.txtPONo)
        Me.FraFront.Controls.Add(Me.txtPODate)
        Me.FraFront.Controls.Add(Me.chkCancelled)
        Me.FraFront.Controls.Add(Me.TabMain)
        Me.FraFront.Controls.Add(Me.txtCustomer)
        Me.FraFront.Controls.Add(Me.txtBillDate)
        Me.FraFront.Controls.Add(Me.TxtBillTm)
        Me.FraFront.Controls.Add(Me.txtBillNoPrefix)
        Me.FraFront.Controls.Add(Me.txtBillNoSuffix)
        Me.FraFront.Controls.Add(Me.txtBillNo)
        Me.FraFront.Controls.Add(Me.Label13)
        Me.FraFront.Controls.Add(Me.lblSoDate)
        Me.FraFront.Controls.Add(Me.Label36)
        Me.FraFront.Controls.Add(Me.Label12)
        Me.FraFront.Controls.Add(Me.lblCust)
        Me.FraFront.Controls.Add(Me.Label5)
        Me.FraFront.Controls.Add(Me.Label1)
        Me.FraFront.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraFront.Location = New System.Drawing.Point(0, -6)
        Me.FraFront.Name = "FraFront"
        Me.FraFront.Padding = New System.Windows.Forms.Padding(0)
        Me.FraFront.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraFront.Size = New System.Drawing.Size(1106, 580)
        Me.FraFront.TabIndex = 38
        Me.FraFront.TabStop = False
        '
        'cmdPopulateExcel
        '
        Me.cmdPopulateExcel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPopulateExcel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPopulateExcel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPopulateExcel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPopulateExcel.Location = New System.Drawing.Point(966, 60)
        Me.cmdPopulateExcel.Name = "cmdPopulateExcel"
        Me.cmdPopulateExcel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPopulateExcel.Size = New System.Drawing.Size(125, 23)
        Me.cmdPopulateExcel.TabIndex = 248
        Me.cmdPopulateExcel.Text = "Populate From Excel"
        Me.cmdPopulateExcel.UseVisualStyleBackColor = False
        '
        'lblPoNo
        '
        Me.lblPoNo.AcceptsReturn = True
        Me.lblPoNo.BackColor = System.Drawing.SystemColors.Window
        Me.lblPoNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPoNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.lblPoNo.Enabled = False
        Me.lblPoNo.ForeColor = System.Drawing.Color.Blue
        Me.lblPoNo.Location = New System.Drawing.Point(632, 56)
        Me.lblPoNo.MaxLength = 0
        Me.lblPoNo.Name = "lblPoNo"
        Me.lblPoNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPoNo.Size = New System.Drawing.Size(172, 20)
        Me.lblPoNo.TabIndex = 246
        '
        'cboCalcOn
        '
        Me.cboCalcOn.BackColor = System.Drawing.SystemColors.Window
        Me.cboCalcOn.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCalcOn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCalcOn.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCalcOn.Location = New System.Drawing.Point(862, 33)
        Me.cboCalcOn.Name = "cboCalcOn"
        Me.cboCalcOn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCalcOn.Size = New System.Drawing.Size(90, 21)
        Me.cboCalcOn.TabIndex = 244
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(808, 37)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(51, 13)
        Me.Label2.TabIndex = 245
        Me.Label2.Text = "Calc On :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
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
        Me.txtBillTo.Location = New System.Drawing.Point(632, 10)
        Me.txtBillTo.MaxLength = 0
        Me.txtBillTo.Name = "txtBillTo"
        Me.txtBillTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillTo.Size = New System.Drawing.Size(322, 22)
        Me.txtBillTo.TabIndex = 240
        '
        'Label62
        '
        Me.Label62.AutoSize = True
        Me.Label62.BackColor = System.Drawing.SystemColors.Control
        Me.Label62.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label62.Enabled = False
        Me.Label62.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label62.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label62.Location = New System.Drawing.Point(538, 12)
        Me.Label62.Name = "Label62"
        Me.Label62.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label62.Size = New System.Drawing.Size(90, 13)
        Me.Label62.TabIndex = 241
        Me.Label62.Text = "Bill To Location :"
        Me.Label62.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cboDivision
        '
        Me.cboDivision.BackColor = System.Drawing.SystemColors.Window
        Me.cboDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivision.Enabled = False
        Me.cboDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDivision.Location = New System.Drawing.Point(632, 33)
        Me.cboDivision.Name = "cboDivision"
        Me.cboDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivision.Size = New System.Drawing.Size(172, 21)
        Me.cboDivision.TabIndex = 8
        '
        'txtPONo
        '
        Me.txtPONo.AcceptsReturn = True
        Me.txtPONo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPONo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPONo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPONo.Enabled = False
        Me.txtPONo.ForeColor = System.Drawing.Color.Blue
        Me.txtPONo.Location = New System.Drawing.Point(100, 54)
        Me.txtPONo.MaxLength = 0
        Me.txtPONo.Name = "txtPONo"
        Me.txtPONo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPONo.Size = New System.Drawing.Size(203, 20)
        Me.txtPONo.TabIndex = 9
        '
        'txtPODate
        '
        Me.txtPODate.AcceptsReturn = True
        Me.txtPODate.BackColor = System.Drawing.SystemColors.Window
        Me.txtPODate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPODate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPODate.Enabled = False
        Me.txtPODate.ForeColor = System.Drawing.Color.Blue
        Me.txtPODate.Location = New System.Drawing.Point(384, 54)
        Me.txtPODate.MaxLength = 0
        Me.txtPODate.Name = "txtPODate"
        Me.txtPODate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPODate.Size = New System.Drawing.Size(103, 20)
        Me.txtPODate.TabIndex = 10
        '
        'chkCancelled
        '
        Me.chkCancelled.AutoSize = True
        Me.chkCancelled.BackColor = System.Drawing.SystemColors.Control
        Me.chkCancelled.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCancelled.Enabled = False
        Me.chkCancelled.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkCancelled.Location = New System.Drawing.Point(866, 62)
        Me.chkCancelled.Name = "chkCancelled"
        Me.chkCancelled.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCancelled.Size = New System.Drawing.Size(73, 17)
        Me.chkCancelled.TabIndex = 11
        Me.chkCancelled.Text = "Cancelled"
        Me.chkCancelled.UseVisualStyleBackColor = False
        '
        'TabMain
        '
        Me.TabMain.Controls.Add(Me._TabMain_TabPage0)
        Me.TabMain.Controls.Add(Me._TabMain_TabPage1)
        Me.TabMain.ItemSize = New System.Drawing.Size(42, 18)
        Me.TabMain.Location = New System.Drawing.Point(2, 82)
        Me.TabMain.Name = "TabMain"
        Me.TabMain.SelectedIndex = 0
        Me.TabMain.Size = New System.Drawing.Size(1104, 498)
        Me.TabMain.TabIndex = 42
        '
        '_TabMain_TabPage0
        '
        Me._TabMain_TabPage0.Controls.Add(Me.Frame6)
        Me._TabMain_TabPage0.Location = New System.Drawing.Point(4, 22)
        Me._TabMain_TabPage0.Name = "_TabMain_TabPage0"
        Me._TabMain_TabPage0.Size = New System.Drawing.Size(1096, 472)
        Me._TabMain_TabPage0.TabIndex = 0
        Me._TabMain_TabPage0.Text = "Item Details"
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.SprdExp)
        Me.Frame6.Controls.Add(Me.SprdMain)
        Me.Frame6.Controls.Add(Me.lblTotTaxableAmt)
        Me.Frame6.Controls.Add(Me._Label_9)
        Me.Frame6.Controls.Add(Me._Label_22)
        Me.Frame6.Controls.Add(Me.lblTotItemValue)
        Me.Frame6.Controls.Add(Me.lblTotQty)
        Me.Frame6.Controls.Add(Me.lblTotPackQtyCap)
        Me.Frame6.Controls.Add(Me._Label_29)
        Me.Frame6.Controls.Add(Me._Label_25)
        Me.Frame6.Controls.Add(Me.lblTotSGSTAmount)
        Me.Frame6.Controls.Add(Me._Label_26)
        Me.Frame6.Controls.Add(Me.lblTotIGSTAmount)
        Me.Frame6.Controls.Add(Me.lblTCS)
        Me.Frame6.Controls.Add(Me.lblTCSPercentage)
        Me.Frame6.Controls.Add(Me.lblRO)
        Me.Frame6.Controls.Add(Me.lblTotExpAmt)
        Me.Frame6.Controls.Add(Me.LblMKey)
        Me.Frame6.Controls.Add(Me._Label_24)
        Me.Frame6.Controls.Add(Me.lblTotCGSTAmount)
        Me.Frame6.Controls.Add(Me.lblNetAmount)
        Me.Frame6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(0, 0)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(1096, 472)
        Me.Frame6.TabIndex = 47
        Me.Frame6.TabStop = False
        '
        'SprdExp
        '
        Me.SprdExp.DataSource = Nothing
        Me.SprdExp.Location = New System.Drawing.Point(2, 302)
        Me.SprdExp.Name = "SprdExp"
        Me.SprdExp.OcxState = CType(resources.GetObject("SprdExp.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdExp.Size = New System.Drawing.Size(490, 166)
        Me.SprdExp.TabIndex = 72
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 10)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(1092, 287)
        Me.SprdMain.TabIndex = 12
        '
        'lblTotTaxableAmt
        '
        Me.lblTotTaxableAmt.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotTaxableAmt.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotTaxableAmt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTotTaxableAmt.Location = New System.Drawing.Point(448, 264)
        Me.lblTotTaxableAmt.Name = "lblTotTaxableAmt"
        Me.lblTotTaxableAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotTaxableAmt.Size = New System.Drawing.Size(63, 15)
        Me.lblTotTaxableAmt.TabIndex = 75
        Me.lblTotTaxableAmt.Text = "0"
        '
        '_Label_9
        '
        Me._Label_9.AutoSize = True
        Me._Label_9.BackColor = System.Drawing.SystemColors.Control
        Me._Label_9.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label_9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label.SetIndex(Me._Label_9, CType(9, Short))
        Me._Label_9.Location = New System.Drawing.Point(928, 429)
        Me._Label_9.Name = "_Label_9"
        Me._Label_9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label_9.Size = New System.Drawing.Size(63, 13)
        Me._Label_9.TabIndex = 74
        Me._Label_9.Text = "Other Exp. :"
        Me._Label_9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label_22
        '
        Me._Label_22.AutoSize = True
        Me._Label_22.BackColor = System.Drawing.SystemColors.Control
        Me._Label_22.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label_22.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label.SetIndex(Me._Label_22, CType(22, Short))
        Me._Label_22.Location = New System.Drawing.Point(928, 351)
        Me._Label_22.Name = "_Label_22"
        Me._Label_22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label_22.Size = New System.Drawing.Size(63, 13)
        Me._Label_22.TabIndex = 53
        Me._Label_22.Text = "Item Value :"
        Me._Label_22.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotItemValue
        '
        Me.lblTotItemValue.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotItemValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotItemValue.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotItemValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotItemValue.Location = New System.Drawing.Point(996, 349)
        Me.lblTotItemValue.Name = "lblTotItemValue"
        Me.lblTotItemValue.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotItemValue.Size = New System.Drawing.Size(95, 17)
        Me.lblTotItemValue.TabIndex = 52
        Me.lblTotItemValue.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotQty
        '
        Me.lblTotQty.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotQty.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotQty.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotQty.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotQty.Location = New System.Drawing.Point(802, 349)
        Me.lblTotQty.Name = "lblTotQty"
        Me.lblTotQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotQty.Size = New System.Drawing.Size(95, 17)
        Me.lblTotQty.TabIndex = 49
        Me.lblTotQty.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotPackQtyCap
        '
        Me.lblTotPackQtyCap.AutoSize = True
        Me.lblTotPackQtyCap.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotPackQtyCap.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotPackQtyCap.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotPackQtyCap.Location = New System.Drawing.Point(742, 351)
        Me.lblTotPackQtyCap.Name = "lblTotPackQtyCap"
        Me.lblTotPackQtyCap.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotPackQtyCap.Size = New System.Drawing.Size(56, 13)
        Me.lblTotPackQtyCap.TabIndex = 48
        Me.lblTotPackQtyCap.Text = "Total Qty :"
        Me.lblTotPackQtyCap.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label_29
        '
        Me._Label_29.AutoSize = True
        Me._Label_29.BackColor = System.Drawing.SystemColors.Control
        Me._Label_29.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label_29.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label.SetIndex(Me._Label_29, CType(29, Short))
        Me._Label_29.Location = New System.Drawing.Point(922, 453)
        Me._Label_29.Name = "_Label_29"
        Me._Label_29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label_29.Size = New System.Drawing.Size(69, 13)
        Me._Label_29.TabIndex = 51
        Me._Label_29.Text = "Net Amount :"
        Me._Label_29.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label_25
        '
        Me._Label_25.AutoSize = True
        Me._Label_25.BackColor = System.Drawing.SystemColors.Control
        Me._Label_25.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label_25.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label.SetIndex(Me._Label_25, CType(25, Short))
        Me._Label_25.Location = New System.Drawing.Point(949, 387)
        Me._Label_25.Name = "_Label_25"
        Me._Label_25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label_25.Size = New System.Drawing.Size(42, 13)
        Me._Label_25.TabIndex = 70
        Me._Label_25.Text = "SGST :"
        Me._Label_25.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotSGSTAmount
        '
        Me.lblTotSGSTAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotSGSTAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotSGSTAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotSGSTAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotSGSTAmount.Location = New System.Drawing.Point(996, 386)
        Me.lblTotSGSTAmount.Name = "lblTotSGSTAmount"
        Me.lblTotSGSTAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotSGSTAmount.Size = New System.Drawing.Size(96, 20)
        Me.lblTotSGSTAmount.TabIndex = 69
        Me.lblTotSGSTAmount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label_26
        '
        Me._Label_26.AutoSize = True
        Me._Label_26.BackColor = System.Drawing.SystemColors.Control
        Me._Label_26.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label_26.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label.SetIndex(Me._Label_26, CType(26, Short))
        Me._Label_26.Location = New System.Drawing.Point(953, 407)
        Me._Label_26.Name = "_Label_26"
        Me._Label_26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label_26.Size = New System.Drawing.Size(38, 13)
        Me._Label_26.TabIndex = 68
        Me._Label_26.Text = "IGST :"
        Me._Label_26.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotIGSTAmount
        '
        Me.lblTotIGSTAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotIGSTAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotIGSTAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotIGSTAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotIGSTAmount.Location = New System.Drawing.Point(996, 406)
        Me.lblTotIGSTAmount.Name = "lblTotIGSTAmount"
        Me.lblTotIGSTAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotIGSTAmount.Size = New System.Drawing.Size(96, 20)
        Me.lblTotIGSTAmount.TabIndex = 67
        Me.lblTotIGSTAmount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTCS
        '
        Me.lblTCS.BackColor = System.Drawing.SystemColors.Control
        Me.lblTCS.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTCS.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTCS.Location = New System.Drawing.Point(550, 212)
        Me.lblTCS.Name = "lblTCS"
        Me.lblTCS.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTCS.Size = New System.Drawing.Size(43, 15)
        Me.lblTCS.TabIndex = 63
        Me.lblTCS.Text = "lblTCS"
        Me.lblTCS.Visible = False
        '
        'lblTCSPercentage
        '
        Me.lblTCSPercentage.BackColor = System.Drawing.SystemColors.Control
        Me.lblTCSPercentage.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTCSPercentage.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTCSPercentage.Location = New System.Drawing.Point(548, 210)
        Me.lblTCSPercentage.Name = "lblTCSPercentage"
        Me.lblTCSPercentage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTCSPercentage.Size = New System.Drawing.Size(39, 13)
        Me.lblTCSPercentage.TabIndex = 62
        Me.lblTCSPercentage.Text = "lblTCSPercentage"
        Me.lblTCSPercentage.Visible = False
        '
        'lblRO
        '
        Me.lblRO.BackColor = System.Drawing.SystemColors.Control
        Me.lblRO.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblRO.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRO.Location = New System.Drawing.Point(544, 246)
        Me.lblRO.Name = "lblRO"
        Me.lblRO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblRO.Size = New System.Drawing.Size(41, 11)
        Me.lblRO.TabIndex = 60
        Me.lblRO.Text = "lblRO"
        Me.lblRO.Visible = False
        '
        'lblTotExpAmt
        '
        Me.lblTotExpAmt.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotExpAmt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotExpAmt.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotExpAmt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTotExpAmt.Location = New System.Drawing.Point(996, 426)
        Me.lblTotExpAmt.Name = "lblTotExpAmt"
        Me.lblTotExpAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotExpAmt.Size = New System.Drawing.Size(96, 20)
        Me.lblTotExpAmt.TabIndex = 59
        Me.lblTotExpAmt.Text = "0"
        Me.lblTotExpAmt.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LblMKey
        '
        Me.LblMKey.AutoSize = True
        Me.LblMKey.BackColor = System.Drawing.SystemColors.Control
        Me.LblMKey.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblMKey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblMKey.Location = New System.Drawing.Point(538, 210)
        Me.LblMKey.Name = "LblMKey"
        Me.LblMKey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblMKey.Size = New System.Drawing.Size(48, 13)
        Me.LblMKey.TabIndex = 56
        Me.LblMKey.Text = "LblMKey"
        Me.LblMKey.Visible = False
        '
        '_Label_24
        '
        Me._Label_24.AutoSize = True
        Me._Label_24.BackColor = System.Drawing.SystemColors.Control
        Me._Label_24.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label_24.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label.SetIndex(Me._Label_24, CType(24, Short))
        Me._Label_24.Location = New System.Drawing.Point(949, 371)
        Me._Label_24.Name = "_Label_24"
        Me._Label_24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label_24.Size = New System.Drawing.Size(42, 13)
        Me._Label_24.TabIndex = 55
        Me._Label_24.Text = "CGST :"
        Me._Label_24.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotCGSTAmount
        '
        Me.lblTotCGSTAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotCGSTAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotCGSTAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotCGSTAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotCGSTAmount.Location = New System.Drawing.Point(996, 368)
        Me.lblTotCGSTAmount.Name = "lblTotCGSTAmount"
        Me.lblTotCGSTAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotCGSTAmount.Size = New System.Drawing.Size(96, 20)
        Me.lblTotCGSTAmount.TabIndex = 54
        Me.lblTotCGSTAmount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblNetAmount
        '
        Me.lblNetAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblNetAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNetAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblNetAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblNetAmount.Location = New System.Drawing.Point(996, 447)
        Me.lblNetAmount.Name = "lblNetAmount"
        Me.lblNetAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblNetAmount.Size = New System.Drawing.Size(95, 19)
        Me.lblNetAmount.TabIndex = 50
        Me.lblNetAmount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_TabMain_TabPage1
        '
        Me._TabMain_TabPage1.Controls.Add(Me.Frame1)
        Me._TabMain_TabPage1.Controls.Add(Me.Frame12)
        Me._TabMain_TabPage1.Location = New System.Drawing.Point(4, 22)
        Me._TabMain_TabPage1.Name = "_TabMain_TabPage1"
        Me._TabMain_TabPage1.Size = New System.Drawing.Size(1096, 472)
        Me._TabMain_TabPage1.TabIndex = 1
        Me._TabMain_TabPage1.Text = "Other Details"
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.cboSalePersonName)
        Me.Frame1.Controls.Add(Me.Label6)
        Me.Frame1.Controls.Add(Me.TxtShipTo)
        Me.Frame1.Controls.Add(Me.Label63)
        Me.Frame1.Controls.Add(Me.Frame11)
        Me.Frame1.Controls.Add(Me.chkShipTo)
        Me.Frame1.Controls.Add(Me.cmdSearchShippedTo)
        Me.Frame1.Controls.Add(Me.txtShippedTo)
        Me.Frame1.Controls.Add(Me.chkExWork)
        Me.Frame1.Controls.Add(Me.chkDespatchFrom)
        Me.Frame1.Controls.Add(Me.txtShippedFrom)
        Me.Frame1.Controls.Add(Me.cmdSearchDespatchFrom)
        Me.Frame1.Controls.Add(Me.Frame8)
        Me.Frame1.Controls.Add(Me.Frame7)
        Me.Frame1.Controls.Add(Me.txtNarration)
        Me.Frame1.Controls.Add(Me.txtDocsThru)
        Me.Frame1.Controls.Add(Me.txtRemarks)
        Me.Frame1.Controls.Add(Me.Label50)
        Me.Frame1.Controls.Add(Me.Label4)
        Me.Frame1.Controls.Add(Me.Label32)
        Me.Frame1.Controls.Add(Me.Label31)
        Me.Frame1.Controls.Add(Me.Label26)
        Me.Frame1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(1096, 472)
        Me.Frame1.TabIndex = 43
        Me.Frame1.TabStop = False
        '
        'cboSalePersonName
        '
        Me.cboSalePersonName.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest
        Me.cboSalePersonName.AutoSize = False
        Me.cboSalePersonName.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains
        Appearance1.BackColor = System.Drawing.SystemColors.Window
        Appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption
        Me.cboSalePersonName.DisplayLayout.Appearance = Appearance1
        Me.cboSalePersonName.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.cboSalePersonName.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
        Appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder
        Appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance2.BorderColor = System.Drawing.SystemColors.Window
        Me.cboSalePersonName.DisplayLayout.GroupByBox.Appearance = Appearance2
        Appearance3.ForeColor = System.Drawing.SystemColors.GrayText
        Me.cboSalePersonName.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance3
        Me.cboSalePersonName.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance4.BackColor2 = System.Drawing.SystemColors.Control
        Appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance4.ForeColor = System.Drawing.SystemColors.GrayText
        Me.cboSalePersonName.DisplayLayout.GroupByBox.PromptAppearance = Appearance4
        Me.cboSalePersonName.DisplayLayout.MaxColScrollRegions = 1
        Me.cboSalePersonName.DisplayLayout.MaxRowScrollRegions = 1
        Appearance5.BackColor = System.Drawing.SystemColors.Window
        Appearance5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cboSalePersonName.DisplayLayout.Override.ActiveCellAppearance = Appearance5
        Appearance6.BackColor = System.Drawing.SystemColors.Highlight
        Appearance6.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.cboSalePersonName.DisplayLayout.Override.ActiveRowAppearance = Appearance6
        Me.cboSalePersonName.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
        Me.cboSalePersonName.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
        Appearance7.BackColor = System.Drawing.SystemColors.Window
        Me.cboSalePersonName.DisplayLayout.Override.CardAreaAppearance = Appearance7
        Appearance8.BorderColor = System.Drawing.Color.Silver
        Appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
        Me.cboSalePersonName.DisplayLayout.Override.CellAppearance = Appearance8
        Me.cboSalePersonName.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
        Me.cboSalePersonName.DisplayLayout.Override.CellPadding = 0
        Appearance9.BackColor = System.Drawing.SystemColors.Control
        Appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
        Appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance9.BorderColor = System.Drawing.SystemColors.Window
        Me.cboSalePersonName.DisplayLayout.Override.GroupByRowAppearance = Appearance9
        Appearance10.TextHAlignAsString = "Left"
        Me.cboSalePersonName.DisplayLayout.Override.HeaderAppearance = Appearance10
        Me.cboSalePersonName.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Me.cboSalePersonName.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
        Appearance11.BackColor = System.Drawing.SystemColors.Window
        Appearance11.BorderColor = System.Drawing.Color.Silver
        Me.cboSalePersonName.DisplayLayout.Override.RowAppearance = Appearance11
        Me.cboSalePersonName.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
        Appearance12.BackColor = System.Drawing.SystemColors.ControlLight
        Me.cboSalePersonName.DisplayLayout.Override.TemplateAddRowAppearance = Appearance12
        Me.cboSalePersonName.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cboSalePersonName.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cboSalePersonName.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
        Me.cboSalePersonName.Font = New System.Drawing.Font("Verdana", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboSalePersonName.Location = New System.Drawing.Point(136, 26)
        Me.cboSalePersonName.Name = "cboSalePersonName"
        Me.cboSalePersonName.Size = New System.Drawing.Size(378, 20)
        Me.cboSalePersonName.TabIndex = 245
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(27, 30)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(104, 13)
        Me.Label6.TabIndex = 244
        Me.Label6.Text = "Sale Person Name :"
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
        Me.TxtShipTo.Location = New System.Drawing.Point(135, 304)
        Me.TxtShipTo.MaxLength = 0
        Me.TxtShipTo.Name = "TxtShipTo"
        Me.TxtShipTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtShipTo.Size = New System.Drawing.Size(234, 22)
        Me.TxtShipTo.TabIndex = 242
        '
        'Label63
        '
        Me.Label63.AutoSize = True
        Me.Label63.BackColor = System.Drawing.SystemColors.Control
        Me.Label63.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label63.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label63.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label63.Location = New System.Drawing.Point(75, 308)
        Me.Label63.Name = "Label63"
        Me.Label63.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label63.Size = New System.Drawing.Size(56, 13)
        Me.Label63.TabIndex = 243
        Me.Label63.Text = "Location :"
        Me.Label63.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame11
        '
        Me.Frame11.BackColor = System.Drawing.SystemColors.Control
        Me.Frame11.Controls.Add(Me.txtAdvAdjust)
        Me.Frame11.Controls.Add(Me.txtAdvVNo)
        Me.Frame11.Controls.Add(Me.txtAdvDate)
        Me.Frame11.Controls.Add(Me.txtAdvIGST)
        Me.Frame11.Controls.Add(Me.txtAdvSGST)
        Me.Frame11.Controls.Add(Me.txtAdvCGST)
        Me.Frame11.Controls.Add(Me.txtItemAdvAdjust)
        Me.Frame11.Controls.Add(Me.txtAdvBal)
        Me.Frame11.Controls.Add(Me.txtAdvCGSTBal)
        Me.Frame11.Controls.Add(Me.txtAdvSGSTBal)
        Me.Frame11.Controls.Add(Me.txtAdvIGSTBal)
        Me.Frame11.Controls.Add(Me.Label24)
        Me.Frame11.Controls.Add(Me.Label23)
        Me.Frame11.Controls.Add(Me.Label18)
        Me.Frame11.Controls.Add(Me.Label22)
        Me.Frame11.Controls.Add(Me.Label21)
        Me.Frame11.Controls.Add(Me.Label8)
        Me.Frame11.Controls.Add(Me.Label10)
        Me.Frame11.Controls.Add(Me.Label16)
        Me.Frame11.Controls.Add(Me.Label25)
        Me.Frame11.Controls.Add(Me.Label28)
        Me.Frame11.Controls.Add(Me.Label34)
        Me.Frame11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame11.Location = New System.Drawing.Point(711, 56)
        Me.Frame11.Name = "Frame11"
        Me.Frame11.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame11.Size = New System.Drawing.Size(373, 151)
        Me.Frame11.TabIndex = 80
        Me.Frame11.TabStop = False
        Me.Frame11.Text = "Advance Details"
        Me.Frame11.Visible = False
        '
        'txtAdvAdjust
        '
        Me.txtAdvAdjust.AcceptsReturn = True
        Me.txtAdvAdjust.BackColor = System.Drawing.SystemColors.Window
        Me.txtAdvAdjust.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAdvAdjust.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAdvAdjust.Enabled = False
        Me.txtAdvAdjust.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAdvAdjust.Location = New System.Drawing.Point(293, 126)
        Me.txtAdvAdjust.MaxLength = 0
        Me.txtAdvAdjust.Name = "txtAdvAdjust"
        Me.txtAdvAdjust.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAdvAdjust.Size = New System.Drawing.Size(73, 20)
        Me.txtAdvAdjust.TabIndex = 91
        Me.txtAdvAdjust.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtAdvVNo
        '
        Me.txtAdvVNo.AcceptsReturn = True
        Me.txtAdvVNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtAdvVNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAdvVNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAdvVNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtAdvVNo.Location = New System.Drawing.Point(108, 16)
        Me.txtAdvVNo.MaxLength = 0
        Me.txtAdvVNo.Name = "txtAdvVNo"
        Me.txtAdvVNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAdvVNo.Size = New System.Drawing.Size(77, 20)
        Me.txtAdvVNo.TabIndex = 90
        '
        'txtAdvDate
        '
        Me.txtAdvDate.AcceptsReturn = True
        Me.txtAdvDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtAdvDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAdvDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAdvDate.Enabled = False
        Me.txtAdvDate.ForeColor = System.Drawing.Color.Blue
        Me.txtAdvDate.Location = New System.Drawing.Point(293, 16)
        Me.txtAdvDate.MaxLength = 0
        Me.txtAdvDate.Name = "txtAdvDate"
        Me.txtAdvDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAdvDate.Size = New System.Drawing.Size(73, 20)
        Me.txtAdvDate.TabIndex = 89
        '
        'txtAdvIGST
        '
        Me.txtAdvIGST.AcceptsReturn = True
        Me.txtAdvIGST.BackColor = System.Drawing.SystemColors.Window
        Me.txtAdvIGST.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAdvIGST.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAdvIGST.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAdvIGST.Location = New System.Drawing.Point(293, 104)
        Me.txtAdvIGST.MaxLength = 0
        Me.txtAdvIGST.Name = "txtAdvIGST"
        Me.txtAdvIGST.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAdvIGST.Size = New System.Drawing.Size(73, 20)
        Me.txtAdvIGST.TabIndex = 88
        Me.txtAdvIGST.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtAdvSGST
        '
        Me.txtAdvSGST.AcceptsReturn = True
        Me.txtAdvSGST.BackColor = System.Drawing.SystemColors.Window
        Me.txtAdvSGST.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAdvSGST.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAdvSGST.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAdvSGST.Location = New System.Drawing.Point(293, 82)
        Me.txtAdvSGST.MaxLength = 0
        Me.txtAdvSGST.Name = "txtAdvSGST"
        Me.txtAdvSGST.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAdvSGST.Size = New System.Drawing.Size(73, 20)
        Me.txtAdvSGST.TabIndex = 87
        Me.txtAdvSGST.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtAdvCGST
        '
        Me.txtAdvCGST.AcceptsReturn = True
        Me.txtAdvCGST.BackColor = System.Drawing.SystemColors.Window
        Me.txtAdvCGST.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAdvCGST.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAdvCGST.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAdvCGST.Location = New System.Drawing.Point(293, 60)
        Me.txtAdvCGST.MaxLength = 0
        Me.txtAdvCGST.Name = "txtAdvCGST"
        Me.txtAdvCGST.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAdvCGST.Size = New System.Drawing.Size(73, 20)
        Me.txtAdvCGST.TabIndex = 86
        Me.txtAdvCGST.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtItemAdvAdjust
        '
        Me.txtItemAdvAdjust.AcceptsReturn = True
        Me.txtItemAdvAdjust.BackColor = System.Drawing.SystemColors.Window
        Me.txtItemAdvAdjust.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemAdvAdjust.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItemAdvAdjust.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtItemAdvAdjust.Location = New System.Drawing.Point(293, 38)
        Me.txtItemAdvAdjust.MaxLength = 0
        Me.txtItemAdvAdjust.Name = "txtItemAdvAdjust"
        Me.txtItemAdvAdjust.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemAdvAdjust.Size = New System.Drawing.Size(73, 20)
        Me.txtItemAdvAdjust.TabIndex = 85
        Me.txtItemAdvAdjust.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtAdvBal
        '
        Me.txtAdvBal.AcceptsReturn = True
        Me.txtAdvBal.BackColor = System.Drawing.SystemColors.Window
        Me.txtAdvBal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAdvBal.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAdvBal.Enabled = False
        Me.txtAdvBal.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAdvBal.Location = New System.Drawing.Point(109, 38)
        Me.txtAdvBal.MaxLength = 0
        Me.txtAdvBal.Name = "txtAdvBal"
        Me.txtAdvBal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAdvBal.Size = New System.Drawing.Size(75, 20)
        Me.txtAdvBal.TabIndex = 84
        Me.txtAdvBal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtAdvCGSTBal
        '
        Me.txtAdvCGSTBal.AcceptsReturn = True
        Me.txtAdvCGSTBal.BackColor = System.Drawing.SystemColors.Window
        Me.txtAdvCGSTBal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAdvCGSTBal.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAdvCGSTBal.Enabled = False
        Me.txtAdvCGSTBal.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAdvCGSTBal.Location = New System.Drawing.Point(109, 62)
        Me.txtAdvCGSTBal.MaxLength = 0
        Me.txtAdvCGSTBal.Name = "txtAdvCGSTBal"
        Me.txtAdvCGSTBal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAdvCGSTBal.Size = New System.Drawing.Size(73, 20)
        Me.txtAdvCGSTBal.TabIndex = 83
        Me.txtAdvCGSTBal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtAdvSGSTBal
        '
        Me.txtAdvSGSTBal.AcceptsReturn = True
        Me.txtAdvSGSTBal.BackColor = System.Drawing.SystemColors.Window
        Me.txtAdvSGSTBal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAdvSGSTBal.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAdvSGSTBal.Enabled = False
        Me.txtAdvSGSTBal.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAdvSGSTBal.Location = New System.Drawing.Point(109, 84)
        Me.txtAdvSGSTBal.MaxLength = 0
        Me.txtAdvSGSTBal.Name = "txtAdvSGSTBal"
        Me.txtAdvSGSTBal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAdvSGSTBal.Size = New System.Drawing.Size(73, 20)
        Me.txtAdvSGSTBal.TabIndex = 82
        Me.txtAdvSGSTBal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtAdvIGSTBal
        '
        Me.txtAdvIGSTBal.AcceptsReturn = True
        Me.txtAdvIGSTBal.BackColor = System.Drawing.SystemColors.Window
        Me.txtAdvIGSTBal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAdvIGSTBal.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAdvIGSTBal.Enabled = False
        Me.txtAdvIGSTBal.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAdvIGSTBal.Location = New System.Drawing.Point(109, 106)
        Me.txtAdvIGSTBal.MaxLength = 0
        Me.txtAdvIGSTBal.Name = "txtAdvIGSTBal"
        Me.txtAdvIGSTBal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAdvIGSTBal.Size = New System.Drawing.Size(73, 20)
        Me.txtAdvIGSTBal.TabIndex = 81
        Me.txtAdvIGSTBal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.SystemColors.Control
        Me.Label24.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label24.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label24.Location = New System.Drawing.Point(155, 128)
        Me.Label24.Name = "Label24"
        Me.Label24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label24.Size = New System.Drawing.Size(120, 13)
        Me.Label24.TabIndex = 102
        Me.Label24.Text = "Total Adjusted Amount :"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.SystemColors.Control
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(6, 18)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(78, 13)
        Me.Label23.TabIndex = 101
        Me.Label23.Text = "Payment VNo :"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(188, 18)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(36, 13)
        Me.Label18.TabIndex = 100
        Me.Label18.Text = "Date :"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.SystemColors.Control
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(208, 106)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(77, 13)
        Me.Label22.TabIndex = 99
        Me.Label22.Text = "IGST Amount :"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.SystemColors.Control
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(204, 84)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(81, 13)
        Me.Label21.TabIndex = 98
        Me.Label21.Text = "SGST Amount :"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(188, 62)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(81, 13)
        Me.Label8.TabIndex = 97
        Me.Label8.Text = "CGST Amount :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(187, 40)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(95, 13)
        Me.Label10.TabIndex = 96
        Me.Label10.Text = "Advance Amount :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(6, 40)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(91, 13)
        Me.Label16.TabIndex = 95
        Me.Label16.Text = "Balance Amount :"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.SystemColors.Control
        Me.Label25.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label25.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label25.Location = New System.Drawing.Point(15, 64)
        Me.Label25.Name = "Label25"
        Me.Label25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label25.Size = New System.Drawing.Size(84, 13)
        Me.Label25.TabIndex = 94
        Me.Label25.Text = "Bal. CGST Amt :"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.SystemColors.Control
        Me.Label28.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label28.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label28.Location = New System.Drawing.Point(15, 86)
        Me.Label28.Name = "Label28"
        Me.Label28.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label28.Size = New System.Drawing.Size(84, 13)
        Me.Label28.TabIndex = 93
        Me.Label28.Text = "Bal. SGST Amt :"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.BackColor = System.Drawing.SystemColors.Control
        Me.Label34.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label34.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label34.Location = New System.Drawing.Point(19, 108)
        Me.Label34.Name = "Label34"
        Me.Label34.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label34.Size = New System.Drawing.Size(80, 13)
        Me.Label34.TabIndex = 92
        Me.Label34.Text = "Bal. IGST Amt :"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'chkShipTo
        '
        Me.chkShipTo.AutoSize = True
        Me.chkShipTo.BackColor = System.Drawing.SystemColors.Control
        Me.chkShipTo.Checked = True
        Me.chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkShipTo.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkShipTo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkShipTo.Location = New System.Drawing.Point(136, 262)
        Me.chkShipTo.Name = "chkShipTo"
        Me.chkShipTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkShipTo.Size = New System.Drawing.Size(229, 17)
        Me.chkShipTo.TabIndex = 19
        Me.chkShipTo.Text = "'Shipped To' Same as 'Billed To' (Yes / No)"
        Me.chkShipTo.UseVisualStyleBackColor = False
        '
        'txtShippedTo
        '
        Me.txtShippedTo.AcceptsReturn = True
        Me.txtShippedTo.BackColor = System.Drawing.SystemColors.Window
        Me.txtShippedTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtShippedTo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtShippedTo.Enabled = False
        Me.txtShippedTo.ForeColor = System.Drawing.Color.Blue
        Me.txtShippedTo.Location = New System.Drawing.Point(136, 280)
        Me.txtShippedTo.MaxLength = 0
        Me.txtShippedTo.Name = "txtShippedTo"
        Me.txtShippedTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtShippedTo.Size = New System.Drawing.Size(379, 20)
        Me.txtShippedTo.TabIndex = 20
        '
        'chkExWork
        '
        Me.chkExWork.AutoSize = True
        Me.chkExWork.BackColor = System.Drawing.SystemColors.Control
        Me.chkExWork.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkExWork.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkExWork.Location = New System.Drawing.Point(137, 329)
        Me.chkExWork.Name = "chkExWork"
        Me.chkExWork.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkExWork.Size = New System.Drawing.Size(181, 17)
        Me.chkExWork.TabIndex = 21
        Me.chkExWork.Text = "'Shipped To' Ex Work (Yes / No)"
        Me.chkExWork.UseVisualStyleBackColor = False
        '
        'chkDespatchFrom
        '
        Me.chkDespatchFrom.AutoSize = True
        Me.chkDespatchFrom.BackColor = System.Drawing.SystemColors.Control
        Me.chkDespatchFrom.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkDespatchFrom.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkDespatchFrom.Location = New System.Drawing.Point(136, 216)
        Me.chkDespatchFrom.Name = "chkDespatchFrom"
        Me.chkDespatchFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkDespatchFrom.Size = New System.Drawing.Size(216, 17)
        Me.chkDespatchFrom.TabIndex = 17
        Me.chkDespatchFrom.Text = "'Despatch From' Other Than Bill Address"
        Me.chkDespatchFrom.UseVisualStyleBackColor = False
        '
        'txtShippedFrom
        '
        Me.txtShippedFrom.AcceptsReturn = True
        Me.txtShippedFrom.BackColor = System.Drawing.SystemColors.Window
        Me.txtShippedFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtShippedFrom.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtShippedFrom.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtShippedFrom.Location = New System.Drawing.Point(136, 234)
        Me.txtShippedFrom.MaxLength = 0
        Me.txtShippedFrom.Name = "txtShippedFrom"
        Me.txtShippedFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtShippedFrom.Size = New System.Drawing.Size(379, 20)
        Me.txtShippedFrom.TabIndex = 18
        '
        'Frame8
        '
        Me.Frame8.BackColor = System.Drawing.SystemColors.Control
        Me.Frame8.Controls.Add(Me._OptFreight_0)
        Me.Frame8.Controls.Add(Me._OptFreight_1)
        Me.Frame8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame8.Location = New System.Drawing.Point(136, 349)
        Me.Frame8.Name = "Frame8"
        Me.Frame8.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame8.Size = New System.Drawing.Size(195, 41)
        Me.Frame8.TabIndex = 13
        Me.Frame8.TabStop = False
        Me.Frame8.Text = "Freight"
        '
        '_OptFreight_0
        '
        Me._OptFreight_0.AutoSize = True
        Me._OptFreight_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptFreight_0.Checked = True
        Me._OptFreight_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptFreight_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptFreight.SetIndex(Me._OptFreight_0, CType(0, Short))
        Me._OptFreight_0.Location = New System.Drawing.Point(36, 18)
        Me._OptFreight_0.Name = "_OptFreight_0"
        Me._OptFreight_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptFreight_0.Size = New System.Drawing.Size(59, 17)
        Me._OptFreight_0.TabIndex = 24
        Me._OptFreight_0.TabStop = True
        Me._OptFreight_0.Text = "To Pay"
        Me._OptFreight_0.UseVisualStyleBackColor = False
        '
        '_OptFreight_1
        '
        Me._OptFreight_1.AutoSize = True
        Me._OptFreight_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptFreight_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptFreight_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptFreight.SetIndex(Me._OptFreight_1, CType(1, Short))
        Me._OptFreight_1.Location = New System.Drawing.Point(108, 18)
        Me._OptFreight_1.Name = "_OptFreight_1"
        Me._OptFreight_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptFreight_1.Size = New System.Drawing.Size(46, 17)
        Me._OptFreight_1.TabIndex = 25
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
        Me.Frame7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame7.Location = New System.Drawing.Point(136, 173)
        Me.Frame7.Name = "Frame7"
        Me.Frame7.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame7.Size = New System.Drawing.Size(191, 41)
        Me.Frame7.TabIndex = 64
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
        Me._txtCreditDays_0.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCreditDays.SetIndex(Me._txtCreditDays_0, CType(0, Short))
        Me._txtCreditDays_0.Location = New System.Drawing.Point(52, 16)
        Me._txtCreditDays_0.MaxLength = 0
        Me._txtCreditDays_0.Name = "_txtCreditDays_0"
        Me._txtCreditDays_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtCreditDays_0.Size = New System.Drawing.Size(37, 20)
        Me._txtCreditDays_0.TabIndex = 22
        '
        '_txtCreditDays_1
        '
        Me._txtCreditDays_1.AcceptsReturn = True
        Me._txtCreditDays_1.BackColor = System.Drawing.SystemColors.Window
        Me._txtCreditDays_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me._txtCreditDays_1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtCreditDays_1.Enabled = False
        Me._txtCreditDays_1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCreditDays.SetIndex(Me._txtCreditDays_1, CType(1, Short))
        Me._txtCreditDays_1.Location = New System.Drawing.Point(130, 16)
        Me._txtCreditDays_1.MaxLength = 0
        Me._txtCreditDays_1.Name = "_txtCreditDays_1"
        Me._txtCreditDays_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtCreditDays_1.Size = New System.Drawing.Size(37, 20)
        Me._txtCreditDays_1.TabIndex = 23
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.BackColor = System.Drawing.SystemColors.Control
        Me.Label33.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label33.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label33.Location = New System.Drawing.Point(14, 18)
        Me.Label33.Name = "Label33"
        Me.Label33.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label33.Size = New System.Drawing.Size(36, 13)
        Me.Label33.TabIndex = 66
        Me.Label33.Text = "From :"
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.BackColor = System.Drawing.SystemColors.Control
        Me.Label35.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label35.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label35.Location = New System.Drawing.Point(102, 18)
        Me.Label35.Name = "Label35"
        Me.Label35.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label35.Size = New System.Drawing.Size(26, 13)
        Me.Label35.TabIndex = 65
        Me.Label35.Text = "To :"
        '
        'txtNarration
        '
        Me.txtNarration.AcceptsReturn = True
        Me.txtNarration.BackColor = System.Drawing.SystemColors.Window
        Me.txtNarration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNarration.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNarration.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNarration.Location = New System.Drawing.Point(136, 129)
        Me.txtNarration.MaxLength = 0
        Me.txtNarration.Multiline = True
        Me.txtNarration.Name = "txtNarration"
        Me.txtNarration.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNarration.Size = New System.Drawing.Size(378, 38)
        Me.txtNarration.TabIndex = 16
        '
        'txtDocsThru
        '
        Me.txtDocsThru.AcceptsReturn = True
        Me.txtDocsThru.BackColor = System.Drawing.SystemColors.Window
        Me.txtDocsThru.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDocsThru.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDocsThru.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDocsThru.Location = New System.Drawing.Point(136, 52)
        Me.txtDocsThru.MaxLength = 0
        Me.txtDocsThru.Name = "txtDocsThru"
        Me.txtDocsThru.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDocsThru.Size = New System.Drawing.Size(378, 20)
        Me.txtDocsThru.TabIndex = 14
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRemarks.Location = New System.Drawing.Point(136, 75)
        Me.txtRemarks.MaxLength = 0
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.Size = New System.Drawing.Size(378, 53)
        Me.txtRemarks.TabIndex = 15
        '
        'Label50
        '
        Me.Label50.AutoSize = True
        Me.Label50.BackColor = System.Drawing.SystemColors.Control
        Me.Label50.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label50.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label50.Location = New System.Drawing.Point(72, 236)
        Me.Label50.Name = "Label50"
        Me.Label50.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label50.Size = New System.Drawing.Size(59, 13)
        Me.Label50.TabIndex = 79
        Me.Label50.Text = "Despatch :"
        Me.Label50.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(63, 280)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(68, 13)
        Me.Label4.TabIndex = 76
        Me.Label4.Text = "Shipped To :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.BackColor = System.Drawing.SystemColors.Control
        Me.Label32.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label32.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label32.Location = New System.Drawing.Point(45, 133)
        Me.Label32.Name = "Label32"
        Me.Label32.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label32.Size = New System.Drawing.Size(86, 13)
        Me.Label32.TabIndex = 46
        Me.Label32.Text = "Payment Terms :"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.BackColor = System.Drawing.SystemColors.Control
        Me.Label31.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label31.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label31.Location = New System.Drawing.Point(68, 54)
        Me.Label31.Name = "Label31"
        Me.Label31.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label31.Size = New System.Drawing.Size(63, 13)
        Me.Label31.TabIndex = 45
        Me.Label31.Text = "Docs Thru :"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.SystemColors.Control
        Me.Label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(76, 77)
        Me.Label26.Name = "Label26"
        Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label26.Size = New System.Drawing.Size(55, 13)
        Me.Label26.TabIndex = 44
        Me.Label26.Text = "Remarks :"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame12
        '
        Me.Frame12.BackColor = System.Drawing.SystemColors.Control
        Me.Frame12.Controls.Add(Me.txtMode)
        Me.Frame12.Controls.Add(Me.txtVehicle)
        Me.Frame12.Controls.Add(Me.txtCarriers)
        Me.Frame12.Controls.Add(Me.cboTransmode)
        Me.Frame12.Controls.Add(Me.Label27)
        Me.Frame12.Controls.Add(Me.Label29)
        Me.Frame12.Controls.Add(Me.Label30)
        Me.Frame12.Controls.Add(Me.Label49)
        Me.Frame12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame12.Location = New System.Drawing.Point(354, 228)
        Me.Frame12.Name = "Frame12"
        Me.Frame12.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame12.Size = New System.Drawing.Size(373, 103)
        Me.Frame12.TabIndex = 103
        Me.Frame12.TabStop = False
        Me.Frame12.Text = "Vehicle Details"
        '
        'txtMode
        '
        Me.txtMode.AcceptsReturn = True
        Me.txtMode.BackColor = System.Drawing.SystemColors.Window
        Me.txtMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMode.Location = New System.Drawing.Point(126, 12)
        Me.txtMode.MaxLength = 0
        Me.txtMode.Name = "txtMode"
        Me.txtMode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMode.Size = New System.Drawing.Size(235, 20)
        Me.txtMode.TabIndex = 107
        '
        'txtVehicle
        '
        Me.txtVehicle.AcceptsReturn = True
        Me.txtVehicle.BackColor = System.Drawing.SystemColors.Window
        Me.txtVehicle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVehicle.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVehicle.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtVehicle.Location = New System.Drawing.Point(126, 58)
        Me.txtVehicle.MaxLength = 0
        Me.txtVehicle.Name = "txtVehicle"
        Me.txtVehicle.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVehicle.Size = New System.Drawing.Size(239, 20)
        Me.txtVehicle.TabIndex = 106
        '
        'txtCarriers
        '
        Me.txtCarriers.AcceptsReturn = True
        Me.txtCarriers.BackColor = System.Drawing.SystemColors.Window
        Me.txtCarriers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCarriers.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCarriers.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCarriers.Location = New System.Drawing.Point(126, 80)
        Me.txtCarriers.MaxLength = 0
        Me.txtCarriers.Name = "txtCarriers"
        Me.txtCarriers.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCarriers.Size = New System.Drawing.Size(239, 20)
        Me.txtCarriers.TabIndex = 105
        '
        'cboTransmode
        '
        Me.cboTransmode.BackColor = System.Drawing.SystemColors.Window
        Me.cboTransmode.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboTransmode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTransmode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboTransmode.Location = New System.Drawing.Point(126, 34)
        Me.cboTransmode.Name = "cboTransmode"
        Me.cboTransmode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboTransmode.Size = New System.Drawing.Size(187, 21)
        Me.cboTransmode.TabIndex = 104
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.SystemColors.Control
        Me.Label27.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label27.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label27.Location = New System.Drawing.Point(6, 14)
        Me.Label27.Name = "Label27"
        Me.Label27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label27.Size = New System.Drawing.Size(40, 13)
        Me.Label27.TabIndex = 111
        Me.Label27.Text = "Mode :"
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.SystemColors.Control
        Me.Label29.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label29.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label29.Location = New System.Drawing.Point(6, 60)
        Me.Label29.Name = "Label29"
        Me.Label29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label29.Size = New System.Drawing.Size(67, 13)
        Me.Label29.TabIndex = 110
        Me.Label29.Text = "Vehicle :"
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.SystemColors.Control
        Me.Label30.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label30.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label30.Location = New System.Drawing.Point(6, 84)
        Me.Label30.Name = "Label30"
        Me.Label30.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label30.Size = New System.Drawing.Size(111, 13)
        Me.Label30.TabIndex = 109
        Me.Label30.Text = "Transporter Name :"
        '
        'Label49
        '
        Me.Label49.BackColor = System.Drawing.SystemColors.Control
        Me.Label49.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label49.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label49.Location = New System.Drawing.Point(6, 38)
        Me.Label49.Name = "Label49"
        Me.Label49.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label49.Size = New System.Drawing.Size(93, 13)
        Me.Label49.TabIndex = 108
        Me.Label49.Text = "Trans Mode :"
        '
        'txtCustomer
        '
        Me.txtCustomer.AcceptsReturn = True
        Me.txtCustomer.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustomer.ForeColor = System.Drawing.Color.Blue
        Me.txtCustomer.Location = New System.Drawing.Point(100, 10)
        Me.txtCustomer.MaxLength = 0
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustomer.Size = New System.Drawing.Size(387, 20)
        Me.txtCustomer.TabIndex = 1
        '
        'txtBillDate
        '
        Me.txtBillDate.AcceptsReturn = True
        Me.txtBillDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillDate.ForeColor = System.Drawing.Color.Blue
        Me.txtBillDate.Location = New System.Drawing.Point(384, 32)
        Me.txtBillDate.MaxLength = 0
        Me.txtBillDate.Name = "txtBillDate"
        Me.txtBillDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillDate.Size = New System.Drawing.Size(71, 20)
        Me.txtBillDate.TabIndex = 6
        '
        'TxtBillTm
        '
        Me.TxtBillTm.AcceptsReturn = True
        Me.TxtBillTm.BackColor = System.Drawing.SystemColors.Window
        Me.TxtBillTm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtBillTm.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtBillTm.ForeColor = System.Drawing.Color.Blue
        Me.TxtBillTm.Location = New System.Drawing.Point(455, 32)
        Me.TxtBillTm.MaxLength = 0
        Me.TxtBillTm.Name = "TxtBillTm"
        Me.TxtBillTm.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtBillTm.Size = New System.Drawing.Size(31, 20)
        Me.TxtBillTm.TabIndex = 7
        '
        'txtBillNoPrefix
        '
        Me.txtBillNoPrefix.AcceptsReturn = True
        Me.txtBillNoPrefix.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillNoPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillNoPrefix.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillNoPrefix.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtBillNoPrefix.Location = New System.Drawing.Point(100, 32)
        Me.txtBillNoPrefix.MaxLength = 0
        Me.txtBillNoPrefix.Name = "txtBillNoPrefix"
        Me.txtBillNoPrefix.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillNoPrefix.Size = New System.Drawing.Size(87, 20)
        Me.txtBillNoPrefix.TabIndex = 3
        '
        'txtBillNoSuffix
        '
        Me.txtBillNoSuffix.AcceptsReturn = True
        Me.txtBillNoSuffix.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillNoSuffix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillNoSuffix.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillNoSuffix.Enabled = False
        Me.txtBillNoSuffix.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtBillNoSuffix.Location = New System.Drawing.Point(284, 32)
        Me.txtBillNoSuffix.MaxLength = 0
        Me.txtBillNoSuffix.Name = "txtBillNoSuffix"
        Me.txtBillNoSuffix.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillNoSuffix.Size = New System.Drawing.Size(20, 20)
        Me.txtBillNoSuffix.TabIndex = 5
        Me.txtBillNoSuffix.Visible = False
        '
        'txtBillNo
        '
        Me.txtBillNo.AcceptsReturn = True
        Me.txtBillNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtBillNo.Location = New System.Drawing.Point(188, 32)
        Me.txtBillNo.MaxLength = 0
        Me.txtBillNo.Name = "txtBillNo"
        Me.txtBillNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillNo.Size = New System.Drawing.Size(95, 20)
        Me.txtBillNo.TabIndex = 4
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(578, 37)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(50, 13)
        Me.Label13.TabIndex = 73
        Me.Label13.Text = "Division :"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblSoDate
        '
        Me.lblSoDate.AutoSize = True
        Me.lblSoDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblSoDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSoDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSoDate.Location = New System.Drawing.Point(636, 42)
        Me.lblSoDate.Name = "lblSoDate"
        Me.lblSoDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSoDate.Size = New System.Drawing.Size(53, 13)
        Me.lblSoDate.TabIndex = 71
        Me.lblSoDate.Text = "lblSoDate"
        Me.lblSoDate.Visible = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(325, 58)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(54, 13)
        Me.Label12.TabIndex = 57
        Me.Label12.Text = "PO Date :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCust
        '
        Me.lblCust.AutoSize = True
        Me.lblCust.BackColor = System.Drawing.SystemColors.Control
        Me.lblCust.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCust.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCust.Location = New System.Drawing.Point(37, 14)
        Me.lblCust.Name = "lblCust"
        Me.lblCust.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCust.Size = New System.Drawing.Size(57, 13)
        Me.lblCust.TabIndex = 41
        Me.lblCust.Text = "Customer :"
        Me.lblCust.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(325, 34)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(36, 13)
        Me.Label5.TabIndex = 40
        Me.Label5.Text = "Date :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(-1, 36)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(95, 13)
        Me.Label1.TabIndex = 39
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
        Me.Frame3.Controls.Add(Me.cmdDelete)
        Me.Frame3.Controls.Add(Me.cmdSave)
        Me.Frame3.Controls.Add(Me.cmdModify)
        Me.Frame3.Controls.Add(Me.cmdAdd)
        Me.Frame3.Controls.Add(Me.cmdSavePrint)
        Me.Frame3.Controls.Add(Me.Report1)
        Me.Frame3.Controls.Add(Me.lblSODates)
        Me.Frame3.Controls.Add(Me.lblSONos)
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(2, 568)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(1104, 51)
        Me.Frame3.TabIndex = 34
        Me.Frame3.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(14, 12)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 34
        '
        'lblSODates
        '
        Me.lblSODates.BackColor = System.Drawing.SystemColors.Control
        Me.lblSODates.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSODates.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSODates.Location = New System.Drawing.Point(596, 32)
        Me.lblSODates.Name = "lblSODates"
        Me.lblSODates.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSODates.Size = New System.Drawing.Size(17, 9)
        Me.lblSODates.TabIndex = 37
        Me.lblSODates.Text = "lblSODates"
        Me.lblSODates.Visible = False
        '
        'lblSONos
        '
        Me.lblSONos.BackColor = System.Drawing.SystemColors.Control
        Me.lblSONos.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSONos.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSONos.Location = New System.Drawing.Point(590, 14)
        Me.lblSONos.Name = "lblSONos"
        Me.lblSONos.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSONos.Size = New System.Drawing.Size(23, 9)
        Me.lblSONos.TabIndex = 36
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
        Appearance13.BackColor = System.Drawing.SystemColors.Window
        Appearance13.BorderColor = System.Drawing.Color.White
        Me.UltraGrid1.DisplayLayout.Appearance = Appearance13
        Me.UltraGrid1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.UltraGrid1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
        Appearance14.BackColor = System.Drawing.Color.White
        Appearance14.BackColor2 = System.Drawing.Color.White
        Appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance14.BorderColor = System.Drawing.SystemColors.Window
        Me.UltraGrid1.DisplayLayout.GroupByBox.Appearance = Appearance14
        Appearance15.ForeColor = System.Drawing.SystemColors.GrayText
        Me.UltraGrid1.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance15
        Me.UltraGrid1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.UltraGrid1.DisplayLayout.GroupByBox.Hidden = True
        Appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance16.BackColor2 = System.Drawing.SystemColors.Control
        Appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance16.ForeColor = System.Drawing.SystemColors.GrayText
        Me.UltraGrid1.DisplayLayout.GroupByBox.PromptAppearance = Appearance16
        Me.UltraGrid1.DisplayLayout.MaxColScrollRegions = 1
        Me.UltraGrid1.DisplayLayout.MaxRowScrollRegions = 1
        Appearance17.BackColor = System.Drawing.SystemColors.Window
        Appearance17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.UltraGrid1.DisplayLayout.Override.ActiveCellAppearance = Appearance17
        Appearance18.BackColor = System.Drawing.SystemColors.Highlight
        Appearance18.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.UltraGrid1.DisplayLayout.Override.ActiveRowAppearance = Appearance18
        Me.UltraGrid1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
        Me.UltraGrid1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
        Appearance19.BackColor = System.Drawing.SystemColors.Window
        Me.UltraGrid1.DisplayLayout.Override.CardAreaAppearance = Appearance19
        Appearance20.BorderColor = System.Drawing.Color.Silver
        Appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
        Me.UltraGrid1.DisplayLayout.Override.CellAppearance = Appearance20
        Me.UltraGrid1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
        Me.UltraGrid1.DisplayLayout.Override.CellPadding = 0
        Appearance21.BackColor = System.Drawing.SystemColors.Control
        Appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
        Appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance21.BorderColor = System.Drawing.SystemColors.Window
        Me.UltraGrid1.DisplayLayout.Override.GroupByRowAppearance = Appearance21
        Appearance22.TextHAlignAsString = "Left"
        Me.UltraGrid1.DisplayLayout.Override.HeaderAppearance = Appearance22
        Me.UltraGrid1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Me.UltraGrid1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
        Appearance23.BackColor = System.Drawing.SystemColors.Window
        Appearance23.BorderColor = System.Drawing.Color.Silver
        Me.UltraGrid1.DisplayLayout.Override.RowAppearance = Appearance23
        Me.UltraGrid1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
        Appearance24.BackColor = System.Drawing.SystemColors.ControlLight
        Me.UltraGrid1.DisplayLayout.Override.TemplateAddRowAppearance = Appearance24
        Me.UltraGrid1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.UltraGrid1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.UltraGrid1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraGrid1.Location = New System.Drawing.Point(0, 4)
        Me.UltraGrid1.Name = "UltraGrid1"
        Me.UltraGrid1.Size = New System.Drawing.Size(1106, 566)
        Me.UltraGrid1.TabIndex = 82
        '
        'FrmInvoicePerforma
        '
        Me.AcceptButton = Me.cmdSave
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1108, 621)
        Me.Controls.Add(Me.FraFront)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.UltraGrid1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmInvoicePerforma"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds
        Me.Text = "Invoice Performa"
        Me.FraFront.ResumeLayout(False)
        Me.FraFront.PerformLayout()
        Me.TabMain.ResumeLayout(False)
        Me._TabMain_TabPage0.ResumeLayout(False)
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        CType(Me.SprdExp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me._TabMain_TabPage1.ResumeLayout(False)
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        CType(Me.cboSalePersonName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame11.ResumeLayout(False)
        Me.Frame11.PerformLayout()
        Me.Frame8.ResumeLayout(False)
        Me.Frame8.PerformLayout()
        Me.Frame7.ResumeLayout(False)
        Me.Frame7.PerformLayout()
        Me.Frame12.ResumeLayout(False)
        Me.Frame12.PerformLayout()
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
    Public WithEvents cmdGetData As Button
    Public WithEvents cboCalcOn As ComboBox
    Public WithEvents Label2 As Label
    Public WithEvents lblPoNo As TextBox
    Public WithEvents Label3 As Label
    Friend WithEvents UltraGrid1 As Infragistics.Win.UltraWinGrid.UltraGrid
    Public WithEvents cmdPopulateExcel As Button
    Public WithEvents CommonDialogFont As FontDialog
    Public WithEvents CommonDialogColor As ColorDialog
    Public WithEvents CommonDialogPrint As PrintDialog
    Public WithEvents CommonDialogSave As SaveFileDialog
    Public WithEvents CommonDialogOpen As OpenFileDialog
    Friend WithEvents cboSalePersonName As Infragistics.Win.UltraWinGrid.UltraCombo
    Public WithEvents Label6 As Label
#End Region
End Class