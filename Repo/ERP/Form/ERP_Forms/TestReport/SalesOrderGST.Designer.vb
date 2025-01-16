Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmSalesOrderGST
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
    Public WithEvents cmdSearchItem As System.Windows.Forms.Button
    Public WithEvents txtSearchItem As System.Windows.Forms.TextBox
    Public WithEvents cmdAmendExcel As System.Windows.Forms.Button
    Public WithEvents txtServProvided As System.Windows.Forms.TextBox
    Public WithEvents cboInvType As System.Windows.Forms.ComboBox
    Public WithEvents chkApproved As System.Windows.Forms.CheckBox
    Public WithEvents cboOrderType As System.Windows.Forms.ComboBox
    Public WithEvents txtCustAmendNo As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchAmend As System.Windows.Forms.Button
    Public WithEvents txtPONo As System.Windows.Forms.TextBox
    Public WithEvents txtPODate As System.Windows.Forms.TextBox
    Public WithEvents cboStatus As System.Windows.Forms.ComboBox
    Public WithEvents txtWEF As System.Windows.Forms.TextBox
    Public WithEvents txtAmendDate As System.Windows.Forms.TextBox
    Public WithEvents txtAmendNo As System.Windows.Forms.TextBox
    Public WithEvents txtSODate As System.Windows.Forms.TextBox
    Public WithEvents txtSONo As System.Windows.Forms.TextBox
    Public WithEvents txtRemarks As System.Windows.Forms.TextBox
    Public WithEvents txtCustomerName As System.Windows.Forms.TextBox
    Public WithEvents txtCode As System.Windows.Forms.TextBox
    Public WithEvents cmdsearch As System.Windows.Forms.Button
    Public WithEvents Label39 As System.Windows.Forms.Label
    Public WithEvents lblAddItem As System.Windows.Forms.Label
    Public WithEvents Label53 As System.Windows.Forms.Label
    Public WithEvents Label38 As System.Windows.Forms.Label
    Public WithEvents lblType As System.Windows.Forms.Label
    Public WithEvents Label37 As System.Windows.Forms.Label
    Public WithEvents Label36 As System.Windows.Forms.Label
    Public WithEvents Label20 As System.Windows.Forms.Label
    Public WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents fraTop1 As System.Windows.Forms.GroupBox
    Public WithEvents txtAnnexTitle As System.Windows.Forms.TextBox
    Public WithEvents SprdAnnex As AxFPSpreadADO.AxfpSpread
    Public WithEvents Label35 As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents txtDelivery As System.Windows.Forms.TextBox
    Public WithEvents txtExcise As System.Windows.Forms.TextBox
    Public WithEvents txtPacking As System.Windows.Forms.TextBox
    Public WithEvents txtOthCond2 As System.Windows.Forms.TextBox
    Public WithEvents txtOthCond1 As System.Windows.Forms.TextBox
    Public WithEvents cmdPaySearch As System.Windows.Forms.Button
    Public WithEvents txtPaymentDays As System.Windows.Forms.TextBox
    Public WithEvents Label34 As System.Windows.Forms.Label
    Public WithEvents Label33 As System.Windows.Forms.Label
    Public WithEvents Label32 As System.Windows.Forms.Label
    Public WithEvents Label31 As System.Windows.Forms.Label
    Public WithEvents Label30 As System.Windows.Forms.Label
    Public WithEvents Label29 As System.Windows.Forms.Label
    Public WithEvents Label28 As System.Windows.Forms.Label
    Public WithEvents Label27 As System.Windows.Forms.Label
    Public WithEvents Label26 As System.Windows.Forms.Label
    Public WithEvents lblPaymentTerms As System.Windows.Forms.Label
    Public WithEvents Label25 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents fraAccounts As System.Windows.Forms.GroupBox
    Public WithEvents _TabMain_TabPage0 As System.Windows.Forms.TabPage
    Public WithEvents txtEPCGDate As System.Windows.Forms.TextBox
    Public WithEvents txtEPCGNo As System.Windows.Forms.TextBox
    Public WithEvents txtOctroi As System.Windows.Forms.TextBox
    Public WithEvents txtDespMode As System.Windows.Forms.TextBox
    Public WithEvents txtLCClaim As System.Windows.Forms.TextBox
    Public WithEvents txtSaleType As System.Windows.Forms.TextBox
    Public WithEvents txtRoadPermit As System.Windows.Forms.TextBox
    Public WithEvents txtFreight As System.Windows.Forms.TextBox
    Public WithEvents txtInspection As System.Windows.Forms.TextBox
    Public WithEvents txtCommission As System.Windows.Forms.TextBox
    Public WithEvents txtPayment As System.Windows.Forms.TextBox
    Public WithEvents txtBalPayment As System.Windows.Forms.TextBox
    Public WithEvents txtDestination As System.Windows.Forms.TextBox
    Public WithEvents txtTransporter As System.Windows.Forms.TextBox
    Public WithEvents txtDescDetail As System.Windows.Forms.TextBox
    Public WithEvents txtInsurance As System.Windows.Forms.TextBox
    Public WithEvents Label41 As System.Windows.Forms.Label
    Public WithEvents Label40 As System.Windows.Forms.Label
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents lblDueDays As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents Label21 As System.Windows.Forms.Label
    Public WithEvents Label22 As System.Windows.Forms.Label
    Public WithEvents Label23 As System.Windows.Forms.Label
    Public WithEvents Label24 As System.Windows.Forms.Label
    'Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents _TabMain_TabPage1 As System.Windows.Forms.TabPage
    Public WithEvents TabMain As System.Windows.Forms.TabControl
    Public WithEvents FraTrn As System.Windows.Forms.GroupBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents CmdDelete As System.Windows.Forms.Button
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents cmdAmend As System.Windows.Forms.Button
    Public WithEvents CmdModify As System.Windows.Forms.Button
    Public WithEvents CmdAdd As System.Windows.Forms.Button
    Public CommonDialogOpen As System.Windows.Forms.OpenFileDialog
    Public CommonDialogSave As System.Windows.Forms.SaveFileDialog
    Public CommonDialogFont As System.Windows.Forms.FontDialog
    Public CommonDialogColor As System.Windows.Forms.ColorDialog
    Public CommonDialogPrint As System.Windows.Forms.PrintDialog
    Public WithEvents lblBookType As System.Windows.Forms.Label
    Public WithEvents lblMkey As System.Windows.Forms.Label
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSalesOrderGST))
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
        Me.cmdSearchAmend = New System.Windows.Forms.Button()
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.cmdPaySearch = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.cmdAmend = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.cmdBillToSearch = New System.Windows.Forms.Button()
        Me.cmdShipToSearch = New System.Windows.Forms.Button()
        Me.cmdsearchShipCust = New System.Windows.Forms.Button()
        Me.FraTrn = New System.Windows.Forms.GroupBox()
        Me.fraTop1 = New System.Windows.Forms.GroupBox()
        Me.txtVendorCode = New System.Windows.Forms.TextBox()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.cboPOType = New System.Windows.Forms.ComboBox()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.txtShipCustomer = New System.Windows.Forms.TextBox()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.chkShipTo = New System.Windows.Forms.CheckBox()
        Me.txtShipTo = New System.Windows.Forms.TextBox()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.txtBillTo = New System.Windows.Forms.TextBox()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.txtSearchItem = New System.Windows.Forms.TextBox()
        Me.cmdAmendExcel = New System.Windows.Forms.Button()
        Me.txtServProvided = New System.Windows.Forms.TextBox()
        Me.cboInvType = New System.Windows.Forms.ComboBox()
        Me.chkApproved = New System.Windows.Forms.CheckBox()
        Me.cboOrderType = New System.Windows.Forms.ComboBox()
        Me.txtCustAmendNo = New System.Windows.Forms.TextBox()
        Me.txtPONo = New System.Windows.Forms.TextBox()
        Me.txtPODate = New System.Windows.Forms.TextBox()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.txtWEF = New System.Windows.Forms.TextBox()
        Me.txtAmendDate = New System.Windows.Forms.TextBox()
        Me.txtAmendNo = New System.Windows.Forms.TextBox()
        Me.txtSODate = New System.Windows.Forms.TextBox()
        Me.txtSONo = New System.Windows.Forms.TextBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtCustomerName = New System.Windows.Forms.TextBox()
        Me.txtCode = New System.Windows.Forms.TextBox()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.lblAddItem = New System.Windows.Forms.Label()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.lblType = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
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
        Me.fraAccounts = New System.Windows.Forms.GroupBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me._TabMain_TabPage1 = New System.Windows.Forms.TabPage()
        Me.chkDI = New System.Windows.Forms.CheckBox()
        Me.txtEPCGDate = New System.Windows.Forms.TextBox()
        Me.txtEPCGNo = New System.Windows.Forms.TextBox()
        Me.txtOctroi = New System.Windows.Forms.TextBox()
        Me.txtDespMode = New System.Windows.Forms.TextBox()
        Me.txtLCClaim = New System.Windows.Forms.TextBox()
        Me.txtSaleType = New System.Windows.Forms.TextBox()
        Me.txtRoadPermit = New System.Windows.Forms.TextBox()
        Me.txtFreight = New System.Windows.Forms.TextBox()
        Me.txtInspection = New System.Windows.Forms.TextBox()
        Me.txtCommission = New System.Windows.Forms.TextBox()
        Me.txtPayment = New System.Windows.Forms.TextBox()
        Me.txtBalPayment = New System.Windows.Forms.TextBox()
        Me.txtDestination = New System.Windows.Forms.TextBox()
        Me.txtTransporter = New System.Windows.Forms.TextBox()
        Me.txtDescDetail = New System.Windows.Forms.TextBox()
        Me.txtInsurance = New System.Windows.Forms.TextBox()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblDueDays = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.txtAnnexTitle = New System.Windows.Forms.TextBox()
        Me.SprdAnnex = New AxFPSpreadADO.AxfpSpread()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.txtDelivery = New System.Windows.Forms.TextBox()
        Me.txtExcise = New System.Windows.Forms.TextBox()
        Me.txtPacking = New System.Windows.Forms.TextBox()
        Me.txtOthCond2 = New System.Windows.Forms.TextBox()
        Me.txtOthCond1 = New System.Windows.Forms.TextBox()
        Me.txtPaymentDays = New System.Windows.Forms.TextBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.lblPaymentTerms = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.lblMkey = New System.Windows.Forms.Label()
        Me.CommonDialogOpen = New System.Windows.Forms.OpenFileDialog()
        Me.CommonDialogSave = New System.Windows.Forms.SaveFileDialog()
        Me.CommonDialogFont = New System.Windows.Forms.FontDialog()
        Me.CommonDialogColor = New System.Windows.Forms.ColorDialog()
        Me.CommonDialogPrint = New System.Windows.Forms.PrintDialog()
        Me.UltraGrid1 = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.FraTrn.SuspendLayout()
        Me.fraTop1.SuspendLayout()
        Me.TabMain.SuspendLayout()
        Me._TabMain_TabPage0.SuspendLayout()
        Me.fraAccounts.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._TabMain_TabPage1.SuspendLayout()
        Me.Frame3.SuspendLayout()
        CType(Me.SprdAnnex, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame1.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.UltraGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSearchItem
        '
        Me.cmdSearchItem.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchItem.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchItem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchItem.Image = CType(resources.GetObject("cmdSearchItem.Image"), System.Drawing.Image)
        Me.cmdSearchItem.Location = New System.Drawing.Point(653, 167)
        Me.cmdSearchItem.Name = "cmdSearchItem"
        Me.cmdSearchItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchItem.Size = New System.Drawing.Size(28, 23)
        Me.cmdSearchItem.TabIndex = 108
        Me.cmdSearchItem.TabStop = False
        Me.cmdSearchItem.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchItem, "Search")
        Me.cmdSearchItem.UseVisualStyleBackColor = False
        '
        'cmdSearchAmend
        '
        Me.cmdSearchAmend.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchAmend.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchAmend.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchAmend.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchAmend.Image = CType(resources.GetObject("cmdSearchAmend.Image"), System.Drawing.Image)
        Me.cmdSearchAmend.Location = New System.Drawing.Point(678, 11)
        Me.cmdSearchAmend.Name = "cmdSearchAmend"
        Me.cmdSearchAmend.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchAmend.Size = New System.Drawing.Size(28, 23)
        Me.cmdSearchAmend.TabIndex = 4
        Me.cmdSearchAmend.TabStop = False
        Me.cmdSearchAmend.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchAmend, "Search")
        Me.cmdSearchAmend.UseVisualStyleBackColor = False
        '
        'cmdsearch
        '
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.Location = New System.Drawing.Point(471, 34)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(28, 23)
        Me.cmdsearch.TabIndex = 7
        Me.cmdsearch.TabStop = False
        Me.cmdsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearch, "Search")
        Me.cmdsearch.UseVisualStyleBackColor = False
        '
        'cmdPaySearch
        '
        Me.cmdPaySearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdPaySearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPaySearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPaySearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPaySearch.Image = CType(resources.GetObject("cmdPaySearch.Image"), System.Drawing.Image)
        Me.cmdPaySearch.Location = New System.Drawing.Point(226, 216)
        Me.cmdPaySearch.Name = "cmdPaySearch"
        Me.cmdPaySearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPaySearch.Size = New System.Drawing.Size(23, 19)
        Me.cmdPaySearch.TabIndex = 62
        Me.cmdPaySearch.TabStop = False
        Me.cmdPaySearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPaySearch, "Search")
        Me.cmdPaySearch.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(707, 11)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(67, 37)
        Me.CmdClose.TabIndex = 9
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close the Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'CmdView
        '
        Me.CmdView.BackColor = System.Drawing.SystemColors.Control
        Me.CmdView.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdView.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
        Me.CmdView.Location = New System.Drawing.Point(641, 11)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(67, 37)
        Me.CmdView.TabIndex = 8
        Me.CmdView.Text = "List &View"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdView, "List View")
        Me.CmdView.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(575, 11)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.TabIndex = 7
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
        Me.cmdPrint.Location = New System.Drawing.Point(509, 11)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 6
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'CmdDelete
        '
        Me.CmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.CmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdDelete.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdDelete.Image = CType(resources.GetObject("CmdDelete.Image"), System.Drawing.Image)
        Me.CmdDelete.Location = New System.Drawing.Point(443, 11)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(67, 37)
        Me.CmdDelete.TabIndex = 5
        Me.CmdDelete.Text = "&Delete"
        Me.CmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdDelete, "Delete Record")
        Me.CmdDelete.UseVisualStyleBackColor = False
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.Location = New System.Drawing.Point(377, 11)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdSavePrint.TabIndex = 4
        Me.cmdSavePrint.Text = "Save&&Print"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save and Print Record")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.Location = New System.Drawing.Point(311, 11)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(67, 37)
        Me.CmdSave.TabIndex = 3
        Me.CmdSave.Text = "&Save"
        Me.CmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSave, "Save Record")
        Me.CmdSave.UseVisualStyleBackColor = False
        '
        'cmdAmend
        '
        Me.cmdAmend.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAmend.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAmend.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAmend.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAmend.Image = CType(resources.GetObject("cmdAmend.Image"), System.Drawing.Image)
        Me.cmdAmend.Location = New System.Drawing.Point(245, 11)
        Me.cmdAmend.Name = "cmdAmend"
        Me.cmdAmend.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAmend.Size = New System.Drawing.Size(67, 37)
        Me.cmdAmend.TabIndex = 2
        Me.cmdAmend.Text = "&Amendment"
        Me.cmdAmend.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdAmend, "Modify Record")
        Me.cmdAmend.UseVisualStyleBackColor = False
        '
        'CmdModify
        '
        Me.CmdModify.BackColor = System.Drawing.SystemColors.Control
        Me.CmdModify.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdModify.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdModify.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdModify.Image = CType(resources.GetObject("CmdModify.Image"), System.Drawing.Image)
        Me.CmdModify.Location = New System.Drawing.Point(179, 11)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(67, 37)
        Me.CmdModify.TabIndex = 1
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
        Me.CmdAdd.Location = New System.Drawing.Point(113, 11)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(67, 37)
        Me.CmdAdd.TabIndex = 0
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'cmdBillToSearch
        '
        Me.cmdBillToSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdBillToSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdBillToSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdBillToSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdBillToSearch.Image = CType(resources.GetObject("cmdBillToSearch.Image"), System.Drawing.Image)
        Me.cmdBillToSearch.Location = New System.Drawing.Point(678, 34)
        Me.cmdBillToSearch.Name = "cmdBillToSearch"
        Me.cmdBillToSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdBillToSearch.Size = New System.Drawing.Size(28, 23)
        Me.cmdBillToSearch.TabIndex = 9
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
        Me.cmdShipToSearch.Location = New System.Drawing.Point(678, 94)
        Me.cmdShipToSearch.Name = "cmdShipToSearch"
        Me.cmdShipToSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShipToSearch.Size = New System.Drawing.Size(28, 23)
        Me.cmdShipToSearch.TabIndex = 15
        Me.cmdShipToSearch.TabStop = False
        Me.cmdShipToSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShipToSearch, "Search")
        Me.cmdShipToSearch.UseVisualStyleBackColor = False
        '
        'cmdsearchShipCust
        '
        Me.cmdsearchShipCust.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearchShipCust.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearchShipCust.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearchShipCust.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearchShipCust.Image = CType(resources.GetObject("cmdsearchShipCust.Image"), System.Drawing.Image)
        Me.cmdsearchShipCust.Location = New System.Drawing.Point(471, 94)
        Me.cmdsearchShipCust.Name = "cmdsearchShipCust"
        Me.cmdsearchShipCust.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearchShipCust.Size = New System.Drawing.Size(28, 23)
        Me.cmdsearchShipCust.TabIndex = 13
        Me.cmdsearchShipCust.TabStop = False
        Me.cmdsearchShipCust.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearchShipCust, "Search")
        Me.cmdsearchShipCust.UseVisualStyleBackColor = False
        '
        'FraTrn
        '
        Me.FraTrn.BackColor = System.Drawing.SystemColors.Control
        Me.FraTrn.Controls.Add(Me.fraTop1)
        Me.FraTrn.Controls.Add(Me.TabMain)
        Me.FraTrn.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraTrn.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraTrn.Location = New System.Drawing.Point(0, -6)
        Me.FraTrn.Name = "FraTrn"
        Me.FraTrn.Padding = New System.Windows.Forms.Padding(0)
        Me.FraTrn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraTrn.Size = New System.Drawing.Size(909, 570)
        Me.FraTrn.TabIndex = 42
        Me.FraTrn.TabStop = False
        '
        'fraTop1
        '
        Me.fraTop1.BackColor = System.Drawing.SystemColors.Control
        Me.fraTop1.Controls.Add(Me.txtVendorCode)
        Me.fraTop1.Controls.Add(Me.Label46)
        Me.fraTop1.Controls.Add(Me.cboPOType)
        Me.fraTop1.Controls.Add(Me.Label45)
        Me.fraTop1.Controls.Add(Me.txtAddress)
        Me.fraTop1.Controls.Add(Me.txtShipCustomer)
        Me.fraTop1.Controls.Add(Me.cmdsearchShipCust)
        Me.fraTop1.Controls.Add(Me.Label44)
        Me.fraTop1.Controls.Add(Me.chkShipTo)
        Me.fraTop1.Controls.Add(Me.cmdShipToSearch)
        Me.fraTop1.Controls.Add(Me.txtShipTo)
        Me.fraTop1.Controls.Add(Me.Label43)
        Me.fraTop1.Controls.Add(Me.cmdBillToSearch)
        Me.fraTop1.Controls.Add(Me.txtBillTo)
        Me.fraTop1.Controls.Add(Me.Label42)
        Me.fraTop1.Controls.Add(Me.cmdSearchItem)
        Me.fraTop1.Controls.Add(Me.txtSearchItem)
        Me.fraTop1.Controls.Add(Me.cmdAmendExcel)
        Me.fraTop1.Controls.Add(Me.txtServProvided)
        Me.fraTop1.Controls.Add(Me.cboInvType)
        Me.fraTop1.Controls.Add(Me.chkApproved)
        Me.fraTop1.Controls.Add(Me.cboOrderType)
        Me.fraTop1.Controls.Add(Me.txtCustAmendNo)
        Me.fraTop1.Controls.Add(Me.cmdSearchAmend)
        Me.fraTop1.Controls.Add(Me.txtPONo)
        Me.fraTop1.Controls.Add(Me.txtPODate)
        Me.fraTop1.Controls.Add(Me.cboStatus)
        Me.fraTop1.Controls.Add(Me.txtWEF)
        Me.fraTop1.Controls.Add(Me.txtAmendDate)
        Me.fraTop1.Controls.Add(Me.txtAmendNo)
        Me.fraTop1.Controls.Add(Me.txtSODate)
        Me.fraTop1.Controls.Add(Me.txtSONo)
        Me.fraTop1.Controls.Add(Me.txtRemarks)
        Me.fraTop1.Controls.Add(Me.txtCustomerName)
        Me.fraTop1.Controls.Add(Me.txtCode)
        Me.fraTop1.Controls.Add(Me.cmdsearch)
        Me.fraTop1.Controls.Add(Me.Label39)
        Me.fraTop1.Controls.Add(Me.lblAddItem)
        Me.fraTop1.Controls.Add(Me.Label53)
        Me.fraTop1.Controls.Add(Me.Label38)
        Me.fraTop1.Controls.Add(Me.lblType)
        Me.fraTop1.Controls.Add(Me.Label37)
        Me.fraTop1.Controls.Add(Me.Label36)
        Me.fraTop1.Controls.Add(Me.Label20)
        Me.fraTop1.Controls.Add(Me.Label19)
        Me.fraTop1.Controls.Add(Me.Label12)
        Me.fraTop1.Controls.Add(Me.Label11)
        Me.fraTop1.Controls.Add(Me.Label10)
        Me.fraTop1.Controls.Add(Me.Label9)
        Me.fraTop1.Controls.Add(Me.Label8)
        Me.fraTop1.Controls.Add(Me.Label7)
        Me.fraTop1.Controls.Add(Me.Label6)
        Me.fraTop1.Controls.Add(Me.Label2)
        Me.fraTop1.Controls.Add(Me.Label4)
        Me.fraTop1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTop1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraTop1.Location = New System.Drawing.Point(2, 0)
        Me.fraTop1.Name = "fraTop1"
        Me.fraTop1.Padding = New System.Windows.Forms.Padding(0)
        Me.fraTop1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraTop1.Size = New System.Drawing.Size(908, 196)
        Me.fraTop1.TabIndex = 0
        Me.fraTop1.TabStop = False
        '
        'txtVendorCode
        '
        Me.txtVendorCode.AcceptsReturn = True
        Me.txtVendorCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtVendorCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVendorCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVendorCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVendorCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtVendorCode.Location = New System.Drawing.Point(646, 118)
        Me.txtVendorCode.MaxLength = 0
        Me.txtVendorCode.Name = "txtVendorCode"
        Me.txtVendorCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVendorCode.Size = New System.Drawing.Size(105, 20)
        Me.txtVendorCode.TabIndex = 17
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.BackColor = System.Drawing.SystemColors.Control
        Me.Label46.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label46.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label46.Location = New System.Drawing.Point(596, 121)
        Me.Label46.Name = "Label46"
        Me.Label46.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label46.Size = New System.Drawing.Size(48, 14)
        Me.Label46.TabIndex = 124
        Me.Label46.Text = "Vendor :"
        Me.Label46.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cboPOType
        '
        Me.cboPOType.BackColor = System.Drawing.SystemColors.Window
        Me.cboPOType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboPOType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPOType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPOType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboPOType.Location = New System.Drawing.Point(573, 62)
        Me.cboPOType.Name = "cboPOType"
        Me.cboPOType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboPOType.Size = New System.Drawing.Size(106, 22)
        Me.cboPOType.TabIndex = 8
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.BackColor = System.Drawing.SystemColors.Control
        Me.Label45.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label45.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label45.Location = New System.Drawing.Point(516, 65)
        Me.Label45.Name = "Label45"
        Me.Label45.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label45.Size = New System.Drawing.Size(53, 14)
        Me.Label45.TabIndex = 122
        Me.Label45.Text = "PO Type :"
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.TopRight
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
        Me.txtAddress.Location = New System.Drawing.Point(104, 56)
        Me.txtAddress.MaxLength = 0
        Me.txtAddress.Multiline = True
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAddress.Size = New System.Drawing.Size(392, 38)
        Me.txtAddress.TabIndex = 7
        '
        'txtShipCustomer
        '
        Me.txtShipCustomer.AcceptsReturn = True
        Me.txtShipCustomer.BackColor = System.Drawing.SystemColors.Window
        Me.txtShipCustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtShipCustomer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtShipCustomer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShipCustomer.ForeColor = System.Drawing.Color.Blue
        Me.txtShipCustomer.Location = New System.Drawing.Point(104, 95)
        Me.txtShipCustomer.MaxLength = 0
        Me.txtShipCustomer.Name = "txtShipCustomer"
        Me.txtShipCustomer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtShipCustomer.Size = New System.Drawing.Size(366, 20)
        Me.txtShipCustomer.TabIndex = 10
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.BackColor = System.Drawing.Color.Transparent
        Me.Label44.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label44.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.ForeColor = System.Drawing.Color.Black
        Me.Label44.Location = New System.Drawing.Point(5, 98)
        Me.Label44.Name = "Label44"
        Me.Label44.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label44.Size = New System.Drawing.Size(97, 14)
        Me.Label44.TabIndex = 119
        Me.Label44.Text = "Ship To Customer :"
        Me.Label44.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'chkShipTo
        '
        Me.chkShipTo.BackColor = System.Drawing.SystemColors.Control
        Me.chkShipTo.Checked = True
        Me.chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkShipTo.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkShipTo.Enabled = False
        Me.chkShipTo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShipTo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkShipTo.Location = New System.Drawing.Point(765, 60)
        Me.chkShipTo.Name = "chkShipTo"
        Me.chkShipTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkShipTo.Size = New System.Drawing.Size(129, 30)
        Me.chkShipTo.TabIndex = 9
        Me.chkShipTo.Text = "'Shipped To' Same as 'Billed To' (Y/N)"
        Me.chkShipTo.UseVisualStyleBackColor = False
        '
        'txtShipTo
        '
        Me.txtShipTo.AcceptsReturn = True
        Me.txtShipTo.BackColor = System.Drawing.SystemColors.Window
        Me.txtShipTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtShipTo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtShipTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShipTo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtShipTo.Location = New System.Drawing.Point(573, 95)
        Me.txtShipTo.MaxLength = 0
        Me.txtShipTo.Name = "txtShipTo"
        Me.txtShipTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtShipTo.Size = New System.Drawing.Size(105, 20)
        Me.txtShipTo.TabIndex = 11
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.BackColor = System.Drawing.SystemColors.Control
        Me.Label43.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label43.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label43.Location = New System.Drawing.Point(515, 95)
        Me.Label43.Name = "Label43"
        Me.Label43.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label43.Size = New System.Drawing.Size(54, 14)
        Me.Label43.TabIndex = 115
        Me.Label43.Text = "Location :"
        Me.Label43.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtBillTo
        '
        Me.txtBillTo.AcceptsReturn = True
        Me.txtBillTo.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillTo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillTo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtBillTo.Location = New System.Drawing.Point(573, 35)
        Me.txtBillTo.MaxLength = 0
        Me.txtBillTo.Name = "txtBillTo"
        Me.txtBillTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillTo.Size = New System.Drawing.Size(105, 20)
        Me.txtBillTo.TabIndex = 5
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.BackColor = System.Drawing.SystemColors.Control
        Me.Label42.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label42.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label42.Location = New System.Drawing.Point(515, 37)
        Me.Label42.Name = "Label42"
        Me.Label42.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label42.Size = New System.Drawing.Size(54, 14)
        Me.Label42.TabIndex = 112
        Me.Label42.Text = "Location :"
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtSearchItem
        '
        Me.txtSearchItem.AcceptsReturn = True
        Me.txtSearchItem.BackColor = System.Drawing.SystemColors.Window
        Me.txtSearchItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearchItem.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSearchItem.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearchItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSearchItem.Location = New System.Drawing.Point(548, 168)
        Me.txtSearchItem.MaxLength = 0
        Me.txtSearchItem.Name = "txtSearchItem"
        Me.txtSearchItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSearchItem.Size = New System.Drawing.Size(105, 20)
        Me.txtSearchItem.TabIndex = 23
        '
        'cmdAmendExcel
        '
        Me.cmdAmendExcel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAmendExcel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAmendExcel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAmendExcel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAmendExcel.Location = New System.Drawing.Point(763, 168)
        Me.cmdAmendExcel.Name = "cmdAmendExcel"
        Me.cmdAmendExcel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAmendExcel.Size = New System.Drawing.Size(133, 23)
        Me.cmdAmendExcel.TabIndex = 24
        Me.cmdAmendExcel.Text = "Amend From Excel"
        Me.cmdAmendExcel.UseVisualStyleBackColor = False
        '
        'txtServProvided
        '
        Me.txtServProvided.AcceptsReturn = True
        Me.txtServProvided.BackColor = System.Drawing.SystemColors.Window
        Me.txtServProvided.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtServProvided.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtServProvided.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtServProvided.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtServProvided.Location = New System.Drawing.Point(341, 142)
        Me.txtServProvided.MaxLength = 0
        Me.txtServProvided.Name = "txtServProvided"
        Me.txtServProvided.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtServProvided.Size = New System.Drawing.Size(337, 20)
        Me.txtServProvided.TabIndex = 20
        '
        'cboInvType
        '
        Me.cboInvType.BackColor = System.Drawing.SystemColors.Window
        Me.cboInvType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboInvType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboInvType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboInvType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboInvType.Location = New System.Drawing.Point(104, 142)
        Me.cboInvType.Name = "cboInvType"
        Me.cboInvType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboInvType.Size = New System.Drawing.Size(101, 22)
        Me.cboInvType.TabIndex = 19
        '
        'chkApproved
        '
        Me.chkApproved.AutoSize = True
        Me.chkApproved.BackColor = System.Drawing.SystemColors.Control
        Me.chkApproved.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkApproved.Enabled = False
        Me.chkApproved.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkApproved.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkApproved.Location = New System.Drawing.Point(763, 121)
        Me.chkApproved.Name = "chkApproved"
        Me.chkApproved.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkApproved.Size = New System.Drawing.Size(126, 18)
        Me.chkApproved.TabIndex = 18
        Me.chkApproved.Text = "Approved (Yes / No)"
        Me.chkApproved.UseVisualStyleBackColor = False
        '
        'cboOrderType
        '
        Me.cboOrderType.BackColor = System.Drawing.SystemColors.Window
        Me.cboOrderType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboOrderType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOrderType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboOrderType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboOrderType.Location = New System.Drawing.Point(763, 142)
        Me.cboOrderType.Name = "cboOrderType"
        Me.cboOrderType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboOrderType.Size = New System.Drawing.Size(133, 22)
        Me.cboOrderType.TabIndex = 21
        '
        'txtCustAmendNo
        '
        Me.txtCustAmendNo.AcceptsReturn = True
        Me.txtCustAmendNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustAmendNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustAmendNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustAmendNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustAmendNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtCustAmendNo.Location = New System.Drawing.Point(404, 118)
        Me.txtCustAmendNo.MaxLength = 0
        Me.txtCustAmendNo.Name = "txtCustAmendNo"
        Me.txtCustAmendNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustAmendNo.Size = New System.Drawing.Size(29, 20)
        Me.txtCustAmendNo.TabIndex = 15
        '
        'txtPONo
        '
        Me.txtPONo.AcceptsReturn = True
        Me.txtPONo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPONo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPONo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPONo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPONo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPONo.Location = New System.Drawing.Point(104, 118)
        Me.txtPONo.MaxLength = 0
        Me.txtPONo.Name = "txtPONo"
        Me.txtPONo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPONo.Size = New System.Drawing.Size(101, 20)
        Me.txtPONo.TabIndex = 13
        '
        'txtPODate
        '
        Me.txtPODate.AcceptsReturn = True
        Me.txtPODate.BackColor = System.Drawing.SystemColors.Window
        Me.txtPODate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPODate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPODate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPODate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPODate.Location = New System.Drawing.Point(252, 118)
        Me.txtPODate.MaxLength = 0
        Me.txtPODate.Name = "txtPODate"
        Me.txtPODate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPODate.Size = New System.Drawing.Size(75, 20)
        Me.txtPODate.TabIndex = 14
        '
        'cboStatus
        '
        Me.cboStatus.BackColor = System.Drawing.SystemColors.Window
        Me.cboStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatus.Enabled = False
        Me.cboStatus.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboStatus.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboStatus.Location = New System.Drawing.Point(763, 95)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboStatus.Size = New System.Drawing.Size(133, 22)
        Me.cboStatus.TabIndex = 12
        '
        'txtWEF
        '
        Me.txtWEF.AcceptsReturn = True
        Me.txtWEF.BackColor = System.Drawing.SystemColors.Window
        Me.txtWEF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWEF.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWEF.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWEF.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtWEF.Location = New System.Drawing.Point(513, 118)
        Me.txtWEF.MaxLength = 0
        Me.txtWEF.Name = "txtWEF"
        Me.txtWEF.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWEF.Size = New System.Drawing.Size(72, 20)
        Me.txtWEF.TabIndex = 16
        '
        'txtAmendDate
        '
        Me.txtAmendDate.AcceptsReturn = True
        Me.txtAmendDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtAmendDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmendDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAmendDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmendDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtAmendDate.Location = New System.Drawing.Point(763, 12)
        Me.txtAmendDate.MaxLength = 0
        Me.txtAmendDate.Name = "txtAmendDate"
        Me.txtAmendDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAmendDate.Size = New System.Drawing.Size(133, 20)
        Me.txtAmendDate.TabIndex = 3
        '
        'txtAmendNo
        '
        Me.txtAmendNo.AcceptsReturn = True
        Me.txtAmendNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtAmendNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmendNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAmendNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmendNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtAmendNo.Location = New System.Drawing.Point(573, 12)
        Me.txtAmendNo.MaxLength = 0
        Me.txtAmendNo.Name = "txtAmendNo"
        Me.txtAmendNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAmendNo.Size = New System.Drawing.Size(105, 20)
        Me.txtAmendNo.TabIndex = 2
        '
        'txtSODate
        '
        Me.txtSODate.AcceptsReturn = True
        Me.txtSODate.BackColor = System.Drawing.SystemColors.Window
        Me.txtSODate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSODate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSODate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSODate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSODate.Location = New System.Drawing.Point(347, 12)
        Me.txtSODate.MaxLength = 0
        Me.txtSODate.Name = "txtSODate"
        Me.txtSODate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSODate.Size = New System.Drawing.Size(75, 20)
        Me.txtSODate.TabIndex = 1
        '
        'txtSONo
        '
        Me.txtSONo.AcceptsReturn = True
        Me.txtSONo.BackColor = System.Drawing.SystemColors.Window
        Me.txtSONo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSONo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSONo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSONo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSONo.Location = New System.Drawing.Point(104, 12)
        Me.txtSONo.MaxLength = 0
        Me.txtSONo.Name = "txtSONo"
        Me.txtSONo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSONo.Size = New System.Drawing.Size(101, 20)
        Me.txtSONo.TabIndex = 0
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtRemarks.Location = New System.Drawing.Point(104, 168)
        Me.txtRemarks.MaxLength = 0
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.Size = New System.Drawing.Size(368, 20)
        Me.txtRemarks.TabIndex = 22
        '
        'txtCustomerName
        '
        Me.txtCustomerName.AcceptsReturn = True
        Me.txtCustomerName.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustomerName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerName.ForeColor = System.Drawing.Color.Blue
        Me.txtCustomerName.Location = New System.Drawing.Point(104, 35)
        Me.txtCustomerName.MaxLength = 0
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustomerName.Size = New System.Drawing.Size(366, 20)
        Me.txtCustomerName.TabIndex = 4
        '
        'txtCode
        '
        Me.txtCode.AcceptsReturn = True
        Me.txtCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCode.ForeColor = System.Drawing.Color.Blue
        Me.txtCode.Location = New System.Drawing.Point(763, 35)
        Me.txtCode.MaxLength = 0
        Me.txtCode.Name = "txtCode"
        Me.txtCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCode.Size = New System.Drawing.Size(133, 20)
        Me.txtCode.TabIndex = 6
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.BackColor = System.Drawing.SystemColors.Control
        Me.Label39.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label39.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label39.Location = New System.Drawing.Point(474, 173)
        Me.Label39.Name = "Label39"
        Me.Label39.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label39.Size = New System.Drawing.Size(70, 14)
        Me.Label39.TabIndex = 109
        Me.Label39.Text = "Search Item :"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblAddItem
        '
        Me.lblAddItem.BackColor = System.Drawing.SystemColors.Control
        Me.lblAddItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAddItem.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddItem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAddItem.Location = New System.Drawing.Point(775, 120)
        Me.lblAddItem.Name = "lblAddItem"
        Me.lblAddItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAddItem.Size = New System.Drawing.Size(61, 15)
        Me.lblAddItem.TabIndex = 105
        Me.lblAddItem.Text = "lblAddItem"
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.BackColor = System.Drawing.SystemColors.Control
        Me.Label53.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label53.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label53.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label53.Location = New System.Drawing.Point(242, 146)
        Me.Label53.Name = "Label53"
        Me.Label53.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label53.Size = New System.Drawing.Size(95, 14)
        Me.Label53.TabIndex = 104
        Me.Label53.Text = "Service Provided :"
        Me.Label53.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.BackColor = System.Drawing.SystemColors.Control
        Me.Label38.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label38.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label38.Location = New System.Drawing.Point(11, 145)
        Me.Label38.Name = "Label38"
        Me.Label38.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label38.Size = New System.Drawing.Size(91, 14)
        Me.Label38.TabIndex = 103
        Me.Label38.Text = "Goods / Service :"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblType
        '
        Me.lblType.BackColor = System.Drawing.SystemColors.Control
        Me.lblType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblType.Location = New System.Drawing.Point(402, 32)
        Me.lblType.Name = "lblType"
        Me.lblType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblType.Size = New System.Drawing.Size(113, 17)
        Me.lblType.TabIndex = 102
        Me.lblType.Text = "lblType"
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.BackColor = System.Drawing.SystemColors.Control
        Me.Label37.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label37.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label37.Location = New System.Drawing.Point(692, 147)
        Me.Label37.Name = "Label37"
        Me.Label37.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label37.Size = New System.Drawing.Size(67, 14)
        Me.Label37.TabIndex = 100
        Me.Label37.Text = "Order Type :"
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.BackColor = System.Drawing.SystemColors.Control
        Me.Label36.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label36.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label36.Location = New System.Drawing.Point(337, 120)
        Me.Label36.Name = "Label36"
        Me.Label36.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label36.Size = New System.Drawing.Size(66, 14)
        Me.Label36.TabIndex = 99
        Me.Label36.Text = "Amend No. :"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(18, 120)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(84, 14)
        Me.Label20.TabIndex = 58
        Me.Label20.Text = "Vendor PO No. :"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(215, 121)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(35, 14)
        Me.Label19.TabIndex = 57
        Me.Label19.Text = "Date :"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(715, 102)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(44, 14)
        Me.Label12.TabIndex = 54
        Me.Label12.Text = "Status :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(440, 121)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(70, 14)
        Me.Label11.TabIndex = 53
        Me.Label11.Text = "Amend wef :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(724, 15)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(35, 14)
        Me.Label10.TabIndex = 52
        Me.Label10.Text = "Date :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(480, 15)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(89, 14)
        Me.Label9.TabIndex = 51
        Me.Label9.Text = "Amendment No. :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(308, 15)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(35, 14)
        Me.Label8.TabIndex = 50
        Me.Label8.Text = "Date :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(52, 14)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(50, 14)
        Me.Label7.TabIndex = 49
        Me.Label7.Text = "Number :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(47, 171)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(55, 14)
        Me.Label6.TabIndex = 48
        Me.Label6.Text = "Remarks :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(13, 38)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(89, 14)
        Me.Label2.TabIndex = 44
        Me.Label2.Text = "Bill To Customer :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(721, 38)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(38, 14)
        Me.Label4.TabIndex = 45
        Me.Label4.Text = "Code :"
        '
        'TabMain
        '
        Me.TabMain.Controls.Add(Me._TabMain_TabPage0)
        Me.TabMain.Controls.Add(Me._TabMain_TabPage1)
        Me.TabMain.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabMain.ItemSize = New System.Drawing.Size(42, 18)
        Me.TabMain.Location = New System.Drawing.Point(0, 194)
        Me.TabMain.Name = "TabMain"
        Me.TabMain.SelectedIndex = 0
        Me.TabMain.Size = New System.Drawing.Size(912, 374)
        Me.TabMain.TabIndex = 0
        '
        '_TabMain_TabPage0
        '
        Me._TabMain_TabPage0.Controls.Add(Me.fraAccounts)
        Me._TabMain_TabPage0.Location = New System.Drawing.Point(4, 22)
        Me._TabMain_TabPage0.Name = "_TabMain_TabPage0"
        Me._TabMain_TabPage0.Size = New System.Drawing.Size(904, 348)
        Me._TabMain_TabPage0.TabIndex = 0
        Me._TabMain_TabPage0.Text = "Item Details"
        '
        'fraAccounts
        '
        Me.fraAccounts.BackColor = System.Drawing.SystemColors.Control
        Me.fraAccounts.Controls.Add(Me.SprdMain)
        Me.fraAccounts.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraAccounts.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraAccounts.Location = New System.Drawing.Point(3, -1)
        Me.fraAccounts.Name = "fraAccounts"
        Me.fraAccounts.Padding = New System.Windows.Forms.Padding(0)
        Me.fraAccounts.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraAccounts.Size = New System.Drawing.Size(899, 345)
        Me.fraAccounts.TabIndex = 83
        Me.fraAccounts.TabStop = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(1, 10)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(897, 330)
        Me.SprdMain.TabIndex = 0
        '
        '_TabMain_TabPage1
        '
        Me._TabMain_TabPage1.Controls.Add(Me.chkDI)
        Me._TabMain_TabPage1.Controls.Add(Me.txtEPCGDate)
        Me._TabMain_TabPage1.Controls.Add(Me.txtEPCGNo)
        Me._TabMain_TabPage1.Controls.Add(Me.txtOctroi)
        Me._TabMain_TabPage1.Controls.Add(Me.txtDespMode)
        Me._TabMain_TabPage1.Controls.Add(Me.txtLCClaim)
        Me._TabMain_TabPage1.Controls.Add(Me.txtSaleType)
        Me._TabMain_TabPage1.Controls.Add(Me.txtRoadPermit)
        Me._TabMain_TabPage1.Controls.Add(Me.txtFreight)
        Me._TabMain_TabPage1.Controls.Add(Me.txtInspection)
        Me._TabMain_TabPage1.Controls.Add(Me.txtCommission)
        Me._TabMain_TabPage1.Controls.Add(Me.txtPayment)
        Me._TabMain_TabPage1.Controls.Add(Me.txtBalPayment)
        Me._TabMain_TabPage1.Controls.Add(Me.txtDestination)
        Me._TabMain_TabPage1.Controls.Add(Me.txtTransporter)
        Me._TabMain_TabPage1.Controls.Add(Me.txtDescDetail)
        Me._TabMain_TabPage1.Controls.Add(Me.txtInsurance)
        Me._TabMain_TabPage1.Controls.Add(Me.Label41)
        Me._TabMain_TabPage1.Controls.Add(Me.Label40)
        Me._TabMain_TabPage1.Controls.Add(Me.Label18)
        Me._TabMain_TabPage1.Controls.Add(Me.Label17)
        Me._TabMain_TabPage1.Controls.Add(Me.Label14)
        Me._TabMain_TabPage1.Controls.Add(Me.Label15)
        Me._TabMain_TabPage1.Controls.Add(Me.Label16)
        Me._TabMain_TabPage1.Controls.Add(Me.lblDueDays)
        Me._TabMain_TabPage1.Controls.Add(Me.Label1)
        Me._TabMain_TabPage1.Controls.Add(Me.Label3)
        Me._TabMain_TabPage1.Controls.Add(Me.Label5)
        Me._TabMain_TabPage1.Controls.Add(Me.Label13)
        Me._TabMain_TabPage1.Controls.Add(Me.Label21)
        Me._TabMain_TabPage1.Controls.Add(Me.Label22)
        Me._TabMain_TabPage1.Controls.Add(Me.Label23)
        Me._TabMain_TabPage1.Controls.Add(Me.Label24)
        Me._TabMain_TabPage1.Location = New System.Drawing.Point(4, 22)
        Me._TabMain_TabPage1.Name = "_TabMain_TabPage1"
        Me._TabMain_TabPage1.Size = New System.Drawing.Size(904, 348)
        Me._TabMain_TabPage1.TabIndex = 1
        Me._TabMain_TabPage1.Text = "Other Details"
        '
        'chkDI
        '
        Me.chkDI.AutoSize = True
        Me.chkDI.BackColor = System.Drawing.SystemColors.Control
        Me.chkDI.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkDI.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDI.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkDI.Location = New System.Drawing.Point(626, 196)
        Me.chkDI.Name = "chkDI"
        Me.chkDI.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkDI.Size = New System.Drawing.Size(174, 18)
        Me.chkDI.TabIndex = 114
        Me.chkDI.Text = "Delivery Against OD (Yes / No)"
        Me.chkDI.UseVisualStyleBackColor = False
        '
        'txtEPCGDate
        '
        Me.txtEPCGDate.AcceptsReturn = True
        Me.txtEPCGDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtEPCGDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEPCGDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEPCGDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEPCGDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtEPCGDate.Location = New System.Drawing.Point(302, 196)
        Me.txtEPCGDate.MaxLength = 0
        Me.txtEPCGDate.Name = "txtEPCGDate"
        Me.txtEPCGDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEPCGDate.Size = New System.Drawing.Size(67, 20)
        Me.txtEPCGDate.TabIndex = 111
        '
        'txtEPCGNo
        '
        Me.txtEPCGNo.AcceptsReturn = True
        Me.txtEPCGNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtEPCGNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEPCGNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEPCGNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEPCGNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtEPCGNo.Location = New System.Drawing.Point(144, 196)
        Me.txtEPCGNo.MaxLength = 0
        Me.txtEPCGNo.Name = "txtEPCGNo"
        Me.txtEPCGNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEPCGNo.Size = New System.Drawing.Size(101, 20)
        Me.txtEPCGNo.TabIndex = 110
        '
        'txtOctroi
        '
        Me.txtOctroi.AcceptsReturn = True
        Me.txtOctroi.BackColor = System.Drawing.SystemColors.Window
        Me.txtOctroi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOctroi.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOctroi.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOctroi.ForeColor = System.Drawing.Color.Blue
        Me.txtOctroi.Location = New System.Drawing.Point(626, 62)
        Me.txtOctroi.MaxLength = 0
        Me.txtOctroi.Name = "txtOctroi"
        Me.txtOctroi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOctroi.Size = New System.Drawing.Size(225, 20)
        Me.txtOctroi.TabIndex = 28
        '
        'txtDespMode
        '
        Me.txtDespMode.AcceptsReturn = True
        Me.txtDespMode.BackColor = System.Drawing.SystemColors.Window
        Me.txtDespMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDespMode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDespMode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDespMode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDespMode.Location = New System.Drawing.Point(626, 36)
        Me.txtDespMode.MaxLength = 15
        Me.txtDespMode.Name = "txtDespMode"
        Me.txtDespMode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDespMode.Size = New System.Drawing.Size(225, 20)
        Me.txtDespMode.TabIndex = 27
        '
        'txtLCClaim
        '
        Me.txtLCClaim.AcceptsReturn = True
        Me.txtLCClaim.BackColor = System.Drawing.SystemColors.Window
        Me.txtLCClaim.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLCClaim.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLCClaim.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLCClaim.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtLCClaim.Location = New System.Drawing.Point(144, 36)
        Me.txtLCClaim.MaxLength = 15
        Me.txtLCClaim.Name = "txtLCClaim"
        Me.txtLCClaim.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLCClaim.Size = New System.Drawing.Size(225, 20)
        Me.txtLCClaim.TabIndex = 20
        '
        'txtSaleType
        '
        Me.txtSaleType.AcceptsReturn = True
        Me.txtSaleType.BackColor = System.Drawing.SystemColors.Window
        Me.txtSaleType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSaleType.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSaleType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSaleType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSaleType.Location = New System.Drawing.Point(626, 10)
        Me.txtSaleType.MaxLength = 15
        Me.txtSaleType.Name = "txtSaleType"
        Me.txtSaleType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSaleType.Size = New System.Drawing.Size(225, 20)
        Me.txtSaleType.TabIndex = 26
        '
        'txtRoadPermit
        '
        Me.txtRoadPermit.AcceptsReturn = True
        Me.txtRoadPermit.BackColor = System.Drawing.SystemColors.Window
        Me.txtRoadPermit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRoadPermit.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRoadPermit.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRoadPermit.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtRoadPermit.Location = New System.Drawing.Point(144, 10)
        Me.txtRoadPermit.MaxLength = 15
        Me.txtRoadPermit.Name = "txtRoadPermit"
        Me.txtRoadPermit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRoadPermit.Size = New System.Drawing.Size(225, 20)
        Me.txtRoadPermit.TabIndex = 19
        '
        'txtFreight
        '
        Me.txtFreight.AcceptsReturn = True
        Me.txtFreight.BackColor = System.Drawing.SystemColors.Window
        Me.txtFreight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFreight.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFreight.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFreight.ForeColor = System.Drawing.Color.Blue
        Me.txtFreight.Location = New System.Drawing.Point(144, 62)
        Me.txtFreight.MaxLength = 0
        Me.txtFreight.Name = "txtFreight"
        Me.txtFreight.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFreight.Size = New System.Drawing.Size(225, 20)
        Me.txtFreight.TabIndex = 21
        '
        'txtInspection
        '
        Me.txtInspection.AcceptsReturn = True
        Me.txtInspection.BackColor = System.Drawing.SystemColors.Window
        Me.txtInspection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInspection.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInspection.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInspection.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtInspection.Location = New System.Drawing.Point(626, 90)
        Me.txtInspection.MaxLength = 15
        Me.txtInspection.Name = "txtInspection"
        Me.txtInspection.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInspection.Size = New System.Drawing.Size(225, 20)
        Me.txtInspection.TabIndex = 29
        '
        'txtCommission
        '
        Me.txtCommission.AcceptsReturn = True
        Me.txtCommission.BackColor = System.Drawing.SystemColors.Window
        Me.txtCommission.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCommission.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCommission.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCommission.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtCommission.Location = New System.Drawing.Point(144, 90)
        Me.txtCommission.MaxLength = 15
        Me.txtCommission.Name = "txtCommission"
        Me.txtCommission.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCommission.Size = New System.Drawing.Size(225, 20)
        Me.txtCommission.TabIndex = 22
        '
        'txtPayment
        '
        Me.txtPayment.AcceptsReturn = True
        Me.txtPayment.BackColor = System.Drawing.SystemColors.Window
        Me.txtPayment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPayment.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPayment.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPayment.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPayment.Location = New System.Drawing.Point(144, 170)
        Me.txtPayment.MaxLength = 15
        Me.txtPayment.Name = "txtPayment"
        Me.txtPayment.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPayment.Size = New System.Drawing.Size(225, 20)
        Me.txtPayment.TabIndex = 25
        '
        'txtBalPayment
        '
        Me.txtBalPayment.AcceptsReturn = True
        Me.txtBalPayment.BackColor = System.Drawing.SystemColors.Window
        Me.txtBalPayment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBalPayment.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBalPayment.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBalPayment.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtBalPayment.Location = New System.Drawing.Point(626, 170)
        Me.txtBalPayment.MaxLength = 15
        Me.txtBalPayment.Name = "txtBalPayment"
        Me.txtBalPayment.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBalPayment.Size = New System.Drawing.Size(225, 20)
        Me.txtBalPayment.TabIndex = 32
        '
        'txtDestination
        '
        Me.txtDestination.AcceptsReturn = True
        Me.txtDestination.BackColor = System.Drawing.SystemColors.Window
        Me.txtDestination.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDestination.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDestination.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDestination.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDestination.Location = New System.Drawing.Point(144, 118)
        Me.txtDestination.MaxLength = 15
        Me.txtDestination.Name = "txtDestination"
        Me.txtDestination.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDestination.Size = New System.Drawing.Size(225, 20)
        Me.txtDestination.TabIndex = 23
        '
        'txtTransporter
        '
        Me.txtTransporter.AcceptsReturn = True
        Me.txtTransporter.BackColor = System.Drawing.SystemColors.Window
        Me.txtTransporter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTransporter.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTransporter.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransporter.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTransporter.Location = New System.Drawing.Point(626, 118)
        Me.txtTransporter.MaxLength = 15
        Me.txtTransporter.Name = "txtTransporter"
        Me.txtTransporter.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTransporter.Size = New System.Drawing.Size(225, 20)
        Me.txtTransporter.TabIndex = 30
        '
        'txtDescDetail
        '
        Me.txtDescDetail.AcceptsReturn = True
        Me.txtDescDetail.BackColor = System.Drawing.SystemColors.Window
        Me.txtDescDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDescDetail.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDescDetail.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescDetail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDescDetail.Location = New System.Drawing.Point(144, 144)
        Me.txtDescDetail.MaxLength = 15
        Me.txtDescDetail.Name = "txtDescDetail"
        Me.txtDescDetail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDescDetail.Size = New System.Drawing.Size(225, 20)
        Me.txtDescDetail.TabIndex = 24
        '
        'txtInsurance
        '
        Me.txtInsurance.AcceptsReturn = True
        Me.txtInsurance.BackColor = System.Drawing.SystemColors.Window
        Me.txtInsurance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInsurance.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInsurance.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInsurance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtInsurance.Location = New System.Drawing.Point(626, 144)
        Me.txtInsurance.MaxLength = 15
        Me.txtInsurance.Name = "txtInsurance"
        Me.txtInsurance.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInsurance.Size = New System.Drawing.Size(225, 20)
        Me.txtInsurance.TabIndex = 31
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.BackColor = System.Drawing.SystemColors.Control
        Me.Label41.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label41.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label41.Location = New System.Drawing.Point(257, 198)
        Me.Label41.Name = "Label41"
        Me.Label41.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label41.Size = New System.Drawing.Size(35, 14)
        Me.Label41.TabIndex = 113
        Me.Label41.Text = "Date :"
        Me.Label41.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.BackColor = System.Drawing.SystemColors.Control
        Me.Label40.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label40.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label40.Location = New System.Drawing.Point(79, 198)
        Me.Label40.Name = "Label40"
        Me.Label40.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label40.Size = New System.Drawing.Size(56, 14)
        Me.Label40.TabIndex = 112
        Me.Label40.Text = "EPCG No :"
        Me.Label40.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(580, 66)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(42, 14)
        Me.Label18.TabIndex = 98
        Me.Label18.Text = "Octroi :"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(528, 38)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(94, 14)
        Me.Label17.TabIndex = 97
        Me.Label17.Text = "Mode of Delivery :"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(65, 12)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(70, 14)
        Me.Label14.TabIndex = 96
        Me.Label14.Text = "Road Permit :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(549, 12)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(73, 14)
        Me.Label15.TabIndex = 95
        Me.Label15.Text = "Type of Sale :"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(72, 38)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(63, 14)
        Me.Label16.TabIndex = 94
        Me.Label16.Text = "L/C Claims :"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblDueDays
        '
        Me.lblDueDays.AutoSize = True
        Me.lblDueDays.BackColor = System.Drawing.SystemColors.Control
        Me.lblDueDays.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDueDays.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDueDays.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDueDays.Location = New System.Drawing.Point(45, 66)
        Me.lblDueDays.Name = "lblDueDays"
        Me.lblDueDays.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDueDays.Size = New System.Drawing.Size(90, 14)
        Me.lblDueDays.TabIndex = 93
        Me.lblDueDays.Text = "Freight Charges :"
        Me.lblDueDays.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(30, 92)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(105, 14)
        Me.Label1.TabIndex = 92
        Me.Label1.Text = "Commission Details :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(560, 92)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(62, 14)
        Me.Label3.TabIndex = 91
        Me.Label3.Text = "Inspection :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(34, 172)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(101, 14)
        Me.Label5.TabIndex = 90
        Me.Label5.Text = "Pay. Receipt Detail :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(515, 172)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(107, 14)
        Me.Label13.TabIndex = 89
        Me.Label13.Text = "Balance Pay. Terms :"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.Black
        Me.Label21.Location = New System.Drawing.Point(552, 120)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(70, 14)
        Me.Label21.TabIndex = 88
        Me.Label21.Text = "Transporter :"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.Black
        Me.Label22.Location = New System.Drawing.Point(69, 120)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(66, 14)
        Me.Label22.TabIndex = 87
        Me.Label22.Text = "Destination :"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.Color.Transparent
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.Black
        Me.Label23.Location = New System.Drawing.Point(561, 146)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(61, 14)
        Me.Label23.TabIndex = 86
        Me.Label23.Text = "Insurance :"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.Black
        Me.Label24.Location = New System.Drawing.Point(41, 146)
        Me.Label24.Name = "Label24"
        Me.Label24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label24.Size = New System.Drawing.Size(94, 14)
        Me.Label24.TabIndex = 85
        Me.Label24.Text = "Despatch Details :"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.txtAnnexTitle)
        Me.Frame3.Controls.Add(Me.SprdAnnex)
        Me.Frame3.Controls.Add(Me.Label35)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(-4996, 22)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(739, 279)
        Me.Frame3.TabIndex = 79
        Me.Frame3.TabStop = False
        '
        'txtAnnexTitle
        '
        Me.txtAnnexTitle.AcceptsReturn = True
        Me.txtAnnexTitle.BackColor = System.Drawing.SystemColors.Window
        Me.txtAnnexTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAnnexTitle.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAnnexTitle.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAnnexTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtAnnexTitle.Location = New System.Drawing.Point(84, 12)
        Me.txtAnnexTitle.MaxLength = 0
        Me.txtAnnexTitle.Name = "txtAnnexTitle"
        Me.txtAnnexTitle.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAnnexTitle.Size = New System.Drawing.Size(649, 20)
        Me.txtAnnexTitle.TabIndex = 80
        '
        'SprdAnnex
        '
        Me.SprdAnnex.DataSource = Nothing
        Me.SprdAnnex.Location = New System.Drawing.Point(4, 34)
        Me.SprdAnnex.Name = "SprdAnnex"
        Me.SprdAnnex.OcxState = CType(resources.GetObject("SprdAnnex.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdAnnex.Size = New System.Drawing.Size(731, 243)
        Me.SprdAnnex.TabIndex = 81
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.BackColor = System.Drawing.SystemColors.Control
        Me.Label35.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label35.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label35.Location = New System.Drawing.Point(26, 14)
        Me.Label35.Name = "Label35"
        Me.Label35.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label35.Size = New System.Drawing.Size(52, 14)
        Me.Label35.TabIndex = 82
        Me.Label35.Text = "Heading :"
        Me.Label35.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.txtDelivery)
        Me.Frame1.Controls.Add(Me.txtExcise)
        Me.Frame1.Controls.Add(Me.txtPacking)
        Me.Frame1.Controls.Add(Me.txtOthCond2)
        Me.Frame1.Controls.Add(Me.txtOthCond1)
        Me.Frame1.Controls.Add(Me.cmdPaySearch)
        Me.Frame1.Controls.Add(Me.txtPaymentDays)
        Me.Frame1.Controls.Add(Me.Label34)
        Me.Frame1.Controls.Add(Me.Label33)
        Me.Frame1.Controls.Add(Me.Label32)
        Me.Frame1.Controls.Add(Me.Label31)
        Me.Frame1.Controls.Add(Me.Label30)
        Me.Frame1.Controls.Add(Me.Label29)
        Me.Frame1.Controls.Add(Me.Label28)
        Me.Frame1.Controls.Add(Me.Label27)
        Me.Frame1.Controls.Add(Me.Label26)
        Me.Frame1.Controls.Add(Me.lblPaymentTerms)
        Me.Frame1.Controls.Add(Me.Label25)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(-4996, 22)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(739, 279)
        Me.Frame1.TabIndex = 60
        Me.Frame1.TabStop = False
        '
        'txtDelivery
        '
        Me.txtDelivery.AcceptsReturn = True
        Me.txtDelivery.BackColor = System.Drawing.SystemColors.Window
        Me.txtDelivery.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDelivery.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDelivery.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDelivery.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDelivery.Location = New System.Drawing.Point(178, 48)
        Me.txtDelivery.MaxLength = 15
        Me.txtDelivery.Name = "txtDelivery"
        Me.txtDelivery.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDelivery.Size = New System.Drawing.Size(459, 20)
        Me.txtDelivery.TabIndex = 67
        '
        'txtExcise
        '
        Me.txtExcise.AcceptsReturn = True
        Me.txtExcise.BackColor = System.Drawing.SystemColors.Window
        Me.txtExcise.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtExcise.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtExcise.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExcise.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtExcise.Location = New System.Drawing.Point(178, 24)
        Me.txtExcise.MaxLength = 15
        Me.txtExcise.Name = "txtExcise"
        Me.txtExcise.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExcise.Size = New System.Drawing.Size(459, 20)
        Me.txtExcise.TabIndex = 66
        '
        'txtPacking
        '
        Me.txtPacking.AcceptsReturn = True
        Me.txtPacking.BackColor = System.Drawing.SystemColors.Window
        Me.txtPacking.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPacking.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPacking.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPacking.ForeColor = System.Drawing.Color.Blue
        Me.txtPacking.Location = New System.Drawing.Point(178, 120)
        Me.txtPacking.MaxLength = 0
        Me.txtPacking.Name = "txtPacking"
        Me.txtPacking.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPacking.Size = New System.Drawing.Size(459, 20)
        Me.txtPacking.TabIndex = 65
        '
        'txtOthCond2
        '
        Me.txtOthCond2.AcceptsReturn = True
        Me.txtOthCond2.BackColor = System.Drawing.SystemColors.Window
        Me.txtOthCond2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOthCond2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOthCond2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOthCond2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtOthCond2.Location = New System.Drawing.Point(178, 192)
        Me.txtOthCond2.MaxLength = 15
        Me.txtOthCond2.Name = "txtOthCond2"
        Me.txtOthCond2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOthCond2.Size = New System.Drawing.Size(459, 20)
        Me.txtOthCond2.TabIndex = 64
        '
        'txtOthCond1
        '
        Me.txtOthCond1.AcceptsReturn = True
        Me.txtOthCond1.BackColor = System.Drawing.SystemColors.Window
        Me.txtOthCond1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOthCond1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOthCond1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOthCond1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtOthCond1.Location = New System.Drawing.Point(178, 168)
        Me.txtOthCond1.MaxLength = 15
        Me.txtOthCond1.Name = "txtOthCond1"
        Me.txtOthCond1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOthCond1.Size = New System.Drawing.Size(459, 20)
        Me.txtOthCond1.TabIndex = 63
        '
        'txtPaymentDays
        '
        Me.txtPaymentDays.AcceptsReturn = True
        Me.txtPaymentDays.BackColor = System.Drawing.SystemColors.Window
        Me.txtPaymentDays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPaymentDays.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPaymentDays.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaymentDays.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPaymentDays.Location = New System.Drawing.Point(178, 240)
        Me.txtPaymentDays.MaxLength = 15
        Me.txtPaymentDays.Name = "txtPaymentDays"
        Me.txtPaymentDays.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPaymentDays.Size = New System.Drawing.Size(101, 20)
        Me.txtPaymentDays.TabIndex = 61
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.BackColor = System.Drawing.SystemColors.Control
        Me.Label34.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label34.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label34.Location = New System.Drawing.Point(18, 148)
        Me.Label34.Name = "Label34"
        Me.Label34.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label34.Size = New System.Drawing.Size(61, 14)
        Me.Label34.TabIndex = 78
        Me.Label34.Text = "Insurance :"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.BackColor = System.Drawing.Color.Transparent
        Me.Label33.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label33.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.Color.Black
        Me.Label33.Location = New System.Drawing.Point(18, 98)
        Me.Label33.Name = "Label33"
        Me.Label33.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label33.Size = New System.Drawing.Size(62, 14)
        Me.Label33.TabIndex = 77
        Me.Label33.Text = "Inspection :"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.BackColor = System.Drawing.Color.Transparent
        Me.Label32.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label32.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.ForeColor = System.Drawing.Color.Black
        Me.Label32.Location = New System.Drawing.Point(18, 26)
        Me.Label32.Name = "Label32"
        Me.Label32.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label32.Size = New System.Drawing.Size(45, 14)
        Me.Label32.TabIndex = 76
        Me.Label32.Text = "Excise :"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.BackColor = System.Drawing.Color.Transparent
        Me.Label31.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label31.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.ForeColor = System.Drawing.Color.Black
        Me.Label31.Location = New System.Drawing.Point(18, 50)
        Me.Label31.Name = "Label31"
        Me.Label31.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label31.Size = New System.Drawing.Size(52, 14)
        Me.Label31.TabIndex = 75
        Me.Label31.Text = "Delivery :"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.BackColor = System.Drawing.Color.Transparent
        Me.Label30.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label30.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.ForeColor = System.Drawing.Color.Black
        Me.Label30.Location = New System.Drawing.Point(18, 74)
        Me.Label30.Name = "Label30"
        Me.Label30.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label30.Size = New System.Drawing.Size(101, 14)
        Me.Label30.TabIndex = 74
        Me.Label30.Text = "Mode of Despatch :"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.SystemColors.Control
        Me.Label29.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label29.Location = New System.Drawing.Point(18, 124)
        Me.Label29.Name = "Label29"
        Me.Label29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label29.Size = New System.Drawing.Size(109, 14)
        Me.Label29.TabIndex = 73
        Me.Label29.Text = "Packing Forwarding :"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.Color.Transparent
        Me.Label28.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label28.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.ForeColor = System.Drawing.Color.Black
        Me.Label28.Location = New System.Drawing.Point(18, 170)
        Me.Label28.Name = "Label28"
        Me.Label28.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label28.Size = New System.Drawing.Size(96, 14)
        Me.Label28.TabIndex = 72
        Me.Label28.Text = "Other Condition 1 :"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.Color.Transparent
        Me.Label27.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.Color.Black
        Me.Label27.Location = New System.Drawing.Point(18, 194)
        Me.Label27.Name = "Label27"
        Me.Label27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label27.Size = New System.Drawing.Size(77, 14)
        Me.Label27.TabIndex = 71
        Me.Label27.Text = "Other Cond 2 :"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.Color.Transparent
        Me.Label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.Black
        Me.Label26.Location = New System.Drawing.Point(18, 218)
        Me.Label26.Name = "Label26"
        Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label26.Size = New System.Drawing.Size(83, 14)
        Me.Label26.TabIndex = 70
        Me.Label26.Text = "PaymentTerms :"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblPaymentTerms
        '
        Me.lblPaymentTerms.BackColor = System.Drawing.Color.Transparent
        Me.lblPaymentTerms.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPaymentTerms.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPaymentTerms.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPaymentTerms.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPaymentTerms.Location = New System.Drawing.Point(250, 216)
        Me.lblPaymentTerms.Name = "lblPaymentTerms"
        Me.lblPaymentTerms.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPaymentTerms.Size = New System.Drawing.Size(387, 19)
        Me.lblPaymentTerms.TabIndex = 69
        Me.lblPaymentTerms.Text = "lblPaymentTerms"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        Me.Label25.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.Color.Black
        Me.Label25.Location = New System.Drawing.Point(18, 242)
        Me.Label25.Name = "Label25"
        Me.Label25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label25.Size = New System.Drawing.Size(82, 14)
        Me.Label25.TabIndex = 68
        Me.Label25.Text = "Payment Days :"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(0, 0)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 44
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.CmdView)
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.CmdDelete)
        Me.FraMovement.Controls.Add(Me.cmdSavePrint)
        Me.FraMovement.Controls.Add(Me.CmdSave)
        Me.FraMovement.Controls.Add(Me.cmdAmend)
        Me.FraMovement.Controls.Add(Me.CmdModify)
        Me.FraMovement.Controls.Add(Me.CmdAdd)
        Me.FraMovement.Controls.Add(Me.lblBookType)
        Me.FraMovement.Controls.Add(Me.lblMkey)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 561)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(910, 51)
        Me.FraMovement.TabIndex = 46
        Me.FraMovement.TabStop = False
        '
        'lblBookType
        '
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(684, 16)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(47, 21)
        Me.lblBookType.TabIndex = 56
        Me.lblBookType.Text = "lblBookType"
        Me.lblBookType.Visible = False
        '
        'lblMkey
        '
        Me.lblMkey.BackColor = System.Drawing.SystemColors.Control
        Me.lblMkey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMkey.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMkey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMkey.Location = New System.Drawing.Point(10, 18)
        Me.lblMkey.Name = "lblMkey"
        Me.lblMkey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMkey.Size = New System.Drawing.Size(45, 17)
        Me.lblMkey.TabIndex = 55
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
        Me.UltraGrid1.Size = New System.Drawing.Size(906, 560)
        Me.UltraGrid1.TabIndex = 80
        '
        'frmSalesOrderGST
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(910, 614)
        Me.Controls.Add(Me.FraTrn)
        Me.Controls.Add(Me.Report1)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.UltraGrid1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(-242, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSalesOrderGST"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Sales Order (GST)"
        Me.FraTrn.ResumeLayout(False)
        Me.fraTop1.ResumeLayout(False)
        Me.fraTop1.PerformLayout()
        Me.TabMain.ResumeLayout(False)
        Me._TabMain_TabPage0.ResumeLayout(False)
        Me.fraAccounts.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me._TabMain_TabPage1.ResumeLayout(False)
        Me._TabMain_TabPage1.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        CType(Me.SprdAnnex, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.UltraGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        'SprdView.DataSource = CType(ADataGrid, MSDATASRC.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        'SprdView.DataSource = Nothing
    End Sub

    Public WithEvents cmdShipToSearch As Button
    Public WithEvents txtShipTo As TextBox
    Public WithEvents Label43 As Label
    Public WithEvents cmdBillToSearch As Button
    Public WithEvents txtBillTo As TextBox
    Public WithEvents Label42 As Label
    Public WithEvents chkShipTo As CheckBox
    Public WithEvents txtShipCustomer As TextBox
    Public WithEvents cmdsearchShipCust As Button
    Public WithEvents Label44 As Label
    Public WithEvents chkDI As CheckBox
    Public WithEvents txtAddress As TextBox
    Public WithEvents cboPOType As ComboBox
    Public WithEvents Label45 As Label
    Public WithEvents txtVendorCode As TextBox
    Public WithEvents Label46 As Label
    Friend WithEvents UltraGrid1 As Infragistics.Win.UltraWinGrid.UltraGrid
#End Region
End Class