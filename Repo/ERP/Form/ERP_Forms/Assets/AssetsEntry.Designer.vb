Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmAssetsEntry
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
        'Me.MDIParent = MIS.Master
        'MIS.Master.Show
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
	Public WithEvents txtIGSTAmount As System.Windows.Forms.TextBox
	Public WithEvents txtSGSTAmount As System.Windows.Forms.TextBox
	Public WithEvents txtCGSTAmount As System.Windows.Forms.TextBox
	Public WithEvents txtSTRefund As System.Windows.Forms.TextBox
	Public WithEvents txtItemType As System.Windows.Forms.TextBox
	Public WithEvents cmdSearchCategory As System.Windows.Forms.Button
	Public WithEvents txtDrCrAmount As System.Windows.Forms.TextBox
	Public WithEvents chkCancelled As System.Windows.Forms.CheckBox
	Public WithEvents txtSalvageAmount As System.Windows.Forms.TextBox
	Public WithEvents cmdMRRSearch As System.Windows.Forms.Button
	Public WithEvents txtModvatPer As System.Windows.Forms.TextBox
	Public WithEvents txtCDAmount As System.Windows.Forms.TextBox
	Public WithEvents txtItemValue As System.Windows.Forms.TextBox
	Public WithEvents txtModvatAmount As System.Windows.Forms.TextBox
	Public WithEvents txtInstallAmount As System.Windows.Forms.TextBox
	Public WithEvents txtTotalCost As System.Windows.Forms.TextBox
	Public WithEvents txtPutDate As System.Windows.Forms.TextBox
	Public WithEvents txtInstallDate As System.Windows.Forms.TextBox
	Public WithEvents txtItemDesc As System.Windows.Forms.TextBox
	Public WithEvents txtSupplierName As System.Windows.Forms.TextBox
	Public WithEvents txtMRRDate As System.Windows.Forms.TextBox
	Public WithEvents txtMRRNo As System.Windows.Forms.TextBox
	Public WithEvents txtPVDate As System.Windows.Forms.TextBox
	Public WithEvents txtPVNo As System.Windows.Forms.TextBox
	Public WithEvents txtBillDate As System.Windows.Forms.TextBox
	Public WithEvents txtBillNo As System.Windows.Forms.TextBox
	Public WithEvents cboStatus As System.Windows.Forms.ComboBox
	Public WithEvents txtLocation As System.Windows.Forms.TextBox
	Public WithEvents txtRemarks As System.Windows.Forms.TextBox
	Public WithEvents cmdSearchAssetType As System.Windows.Forms.Button
	Public WithEvents txtAssetType As System.Windows.Forms.TextBox
	Public WithEvents cmdSearchAssetCode As System.Windows.Forms.Button
	Public WithEvents txtAssetSNo As System.Windows.Forms.TextBox
	Public WithEvents SprdSale As AxFPSpreadADO.AxfpSpread
	Public WithEvents fraSale As System.Windows.Forms.GroupBox
	Public WithEvents _SSTab1_TabPage0 As System.Windows.Forms.TabPage
	Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
	Public WithEvents Frame1 As System.Windows.Forms.GroupBox
	Public WithEvents _SSTab1_TabPage1 As System.Windows.Forms.TabPage
	Public WithEvents _SSTab1_TabPage2 As System.Windows.Forms.TabPage
	Public WithEvents SSTab1 As System.Windows.Forms.TabControl
	Public WithEvents Label23 As System.Windows.Forms.Label
	Public WithEvents Label9 As System.Windows.Forms.Label
	Public WithEvents Label8 As System.Windows.Forms.Label
	Public WithEvents Label33 As System.Windows.Forms.Label
	Public WithEvents Label32 As System.Windows.Forms.Label
	Public WithEvents Label31 As System.Windows.Forms.Label
	Public WithEvents lblBookType As System.Windows.Forms.Label
	Public WithEvents lblVMKey As System.Windows.Forms.Label
	Public WithEvents Label3 As System.Windows.Forms.Label
	Public WithEvents lblAssetCode As System.Windows.Forms.Label
	Public WithEvents Label17 As System.Windows.Forms.Label
	Public WithEvents Label22 As System.Windows.Forms.Label
	Public WithEvents Label21 As System.Windows.Forms.Label
	Public WithEvents Label20 As System.Windows.Forms.Label
	Public WithEvents Label19 As System.Windows.Forms.Label
	Public WithEvents Label15 As System.Windows.Forms.Label
	Public WithEvents Label14 As System.Windows.Forms.Label
	Public WithEvents Label12 As System.Windows.Forms.Label
	Public WithEvents Label11 As System.Windows.Forms.Label
	Public WithEvents Label4 As System.Windows.Forms.Label
	Public WithEvents Label28 As System.Windows.Forms.Label
	Public WithEvents Label24 As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents Label5 As System.Windows.Forms.Label
	Public WithEvents Label6 As System.Windows.Forms.Label
	Public WithEvents Label13 As System.Windows.Forms.Label
	Public WithEvents Label10 As System.Windows.Forms.Label
	Public WithEvents Label18 As System.Windows.Forms.Label
	Public WithEvents Label16 As System.Windows.Forms.Label
	Public WithEvents Label7 As System.Windows.Forms.Label
	Public WithEvents fraTop1 As System.Windows.Forms.GroupBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
	Public WithEvents CmdClose As System.Windows.Forms.Button
	Public WithEvents CmdView As System.Windows.Forms.Button
	Public WithEvents CmdPreview As System.Windows.Forms.Button
	Public WithEvents cmdPrint As System.Windows.Forms.Button
	Public WithEvents CmdDelete As System.Windows.Forms.Button
	Public WithEvents cmdSavePrint As System.Windows.Forms.Button
	Public WithEvents CmdSave As System.Windows.Forms.Button
	Public WithEvents CmdModify As System.Windows.Forms.Button
	Public WithEvents CmdAdd As System.Windows.Forms.Button
	Public WithEvents FraMovement As System.Windows.Forms.GroupBox
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAssetsEntry))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearchCategory = New System.Windows.Forms.Button()
        Me.cmdMRRSearch = New System.Windows.Forms.Button()
        Me.cmdSearchAssetType = New System.Windows.Forms.Button()
        Me.cmdSearchAssetCode = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.fraTop1 = New System.Windows.Forms.GroupBox()
        Me.txtIGSTAmount = New System.Windows.Forms.TextBox()
        Me.txtSGSTAmount = New System.Windows.Forms.TextBox()
        Me.txtCGSTAmount = New System.Windows.Forms.TextBox()
        Me.txtSTRefund = New System.Windows.Forms.TextBox()
        Me.txtItemType = New System.Windows.Forms.TextBox()
        Me.txtDrCrAmount = New System.Windows.Forms.TextBox()
        Me.chkCancelled = New System.Windows.Forms.CheckBox()
        Me.txtSalvageAmount = New System.Windows.Forms.TextBox()
        Me.txtModvatPer = New System.Windows.Forms.TextBox()
        Me.txtCDAmount = New System.Windows.Forms.TextBox()
        Me.txtItemValue = New System.Windows.Forms.TextBox()
        Me.txtModvatAmount = New System.Windows.Forms.TextBox()
        Me.txtInstallAmount = New System.Windows.Forms.TextBox()
        Me.txtTotalCost = New System.Windows.Forms.TextBox()
        Me.txtPutDate = New System.Windows.Forms.TextBox()
        Me.txtInstallDate = New System.Windows.Forms.TextBox()
        Me.txtItemDesc = New System.Windows.Forms.TextBox()
        Me.txtSupplierName = New System.Windows.Forms.TextBox()
        Me.txtMRRDate = New System.Windows.Forms.TextBox()
        Me.txtMRRNo = New System.Windows.Forms.TextBox()
        Me.txtPVDate = New System.Windows.Forms.TextBox()
        Me.txtPVNo = New System.Windows.Forms.TextBox()
        Me.txtBillDate = New System.Windows.Forms.TextBox()
        Me.txtBillNo = New System.Windows.Forms.TextBox()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.txtLocation = New System.Windows.Forms.TextBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtAssetType = New System.Windows.Forms.TextBox()
        Me.txtAssetSNo = New System.Windows.Forms.TextBox()
        Me.SSTab1 = New System.Windows.Forms.TabControl()
        Me._SSTab1_TabPage0 = New System.Windows.Forms.TabPage()
        Me.fraSale = New System.Windows.Forms.GroupBox()
        Me.SprdSale = New AxFPSpreadADO.AxfpSpread()
        Me._SSTab1_TabPage1 = New System.Windows.Forms.TabPage()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me._SSTab1_TabPage2 = New System.Windows.Forms.TabPage()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.lblVMKey = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblAssetCode = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.fraTop1.SuspendLayout()
        Me.SSTab1.SuspendLayout()
        Me._SSTab1_TabPage0.SuspendLayout()
        Me.fraSale.SuspendLayout()
        CType(Me.SprdSale, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage1.SuspendLayout()
        Me.Frame1.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdSearchCategory
        '
        Me.cmdSearchCategory.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchCategory.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchCategory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchCategory.Image = CType(resources.GetObject("cmdSearchCategory.Image"), System.Drawing.Image)
        Me.cmdSearchCategory.Location = New System.Drawing.Point(732, 35)
        Me.cmdSearchCategory.Name = "cmdSearchCategory"
        Me.cmdSearchCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchCategory.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchCategory.TabIndex = 5
        Me.cmdSearchCategory.TabStop = False
        Me.cmdSearchCategory.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchCategory, "Search")
        Me.cmdSearchCategory.UseVisualStyleBackColor = False
        '
        'cmdMRRSearch
        '
        Me.cmdMRRSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdMRRSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdMRRSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdMRRSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMRRSearch.Image = CType(resources.GetObject("cmdMRRSearch.Image"), System.Drawing.Image)
        Me.cmdMRRSearch.Location = New System.Drawing.Point(282, 56)
        Me.cmdMRRSearch.Name = "cmdMRRSearch"
        Me.cmdMRRSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdMRRSearch.Size = New System.Drawing.Size(23, 19)
        Me.cmdMRRSearch.TabIndex = 7
        Me.cmdMRRSearch.TabStop = False
        Me.cmdMRRSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdMRRSearch, "Search")
        Me.cmdMRRSearch.UseVisualStyleBackColor = False
        '
        'cmdSearchAssetType
        '
        Me.cmdSearchAssetType.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchAssetType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchAssetType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchAssetType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchAssetType.Image = CType(resources.GetObject("cmdSearchAssetType.Image"), System.Drawing.Image)
        Me.cmdSearchAssetType.Location = New System.Drawing.Point(378, 35)
        Me.cmdSearchAssetType.Name = "cmdSearchAssetType"
        Me.cmdSearchAssetType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchAssetType.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchAssetType.TabIndex = 3
        Me.cmdSearchAssetType.TabStop = False
        Me.cmdSearchAssetType.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchAssetType, "Search")
        Me.cmdSearchAssetType.UseVisualStyleBackColor = False
        '
        'cmdSearchAssetCode
        '
        Me.cmdSearchAssetCode.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchAssetCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchAssetCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchAssetCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchAssetCode.Image = CType(resources.GetObject("cmdSearchAssetCode.Image"), System.Drawing.Image)
        Me.cmdSearchAssetCode.Location = New System.Drawing.Point(282, 13)
        Me.cmdSearchAssetCode.Name = "cmdSearchAssetCode"
        Me.cmdSearchAssetCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchAssetCode.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchAssetCode.TabIndex = 1
        Me.cmdSearchAssetCode.TabStop = False
        Me.cmdSearchAssetCode.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchAssetCode, "Search")
        Me.cmdSearchAssetCode.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(612, 11)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(67, 37)
        Me.CmdClose.TabIndex = 40
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
        Me.CmdView.Location = New System.Drawing.Point(546, 11)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(67, 37)
        Me.CmdView.TabIndex = 39
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
        Me.CmdPreview.Location = New System.Drawing.Point(480, 11)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.TabIndex = 38
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
        Me.cmdPrint.Location = New System.Drawing.Point(414, 11)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 37
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
        Me.CmdDelete.Location = New System.Drawing.Point(348, 11)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(67, 37)
        Me.CmdDelete.TabIndex = 36
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
        Me.cmdSavePrint.Location = New System.Drawing.Point(282, 11)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdSavePrint.TabIndex = 35
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
        Me.CmdSave.Location = New System.Drawing.Point(216, 11)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(67, 37)
        Me.CmdSave.TabIndex = 34
        Me.CmdSave.Text = "&Save"
        Me.CmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSave, "Save Record")
        Me.CmdSave.UseVisualStyleBackColor = False
        '
        'CmdModify
        '
        Me.CmdModify.BackColor = System.Drawing.SystemColors.Control
        Me.CmdModify.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdModify.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdModify.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdModify.Image = CType(resources.GetObject("CmdModify.Image"), System.Drawing.Image)
        Me.CmdModify.Location = New System.Drawing.Point(150, 11)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(67, 37)
        Me.CmdModify.TabIndex = 33
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
        Me.CmdAdd.Location = New System.Drawing.Point(84, 11)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(67, 37)
        Me.CmdAdd.TabIndex = 32
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'fraTop1
        '
        Me.fraTop1.BackColor = System.Drawing.SystemColors.Control
        Me.fraTop1.Controls.Add(Me.txtIGSTAmount)
        Me.fraTop1.Controls.Add(Me.txtSGSTAmount)
        Me.fraTop1.Controls.Add(Me.txtCGSTAmount)
        Me.fraTop1.Controls.Add(Me.txtSTRefund)
        Me.fraTop1.Controls.Add(Me.txtItemType)
        Me.fraTop1.Controls.Add(Me.cmdSearchCategory)
        Me.fraTop1.Controls.Add(Me.txtDrCrAmount)
        Me.fraTop1.Controls.Add(Me.chkCancelled)
        Me.fraTop1.Controls.Add(Me.txtSalvageAmount)
        Me.fraTop1.Controls.Add(Me.cmdMRRSearch)
        Me.fraTop1.Controls.Add(Me.txtModvatPer)
        Me.fraTop1.Controls.Add(Me.txtCDAmount)
        Me.fraTop1.Controls.Add(Me.txtItemValue)
        Me.fraTop1.Controls.Add(Me.txtModvatAmount)
        Me.fraTop1.Controls.Add(Me.txtInstallAmount)
        Me.fraTop1.Controls.Add(Me.txtTotalCost)
        Me.fraTop1.Controls.Add(Me.txtPutDate)
        Me.fraTop1.Controls.Add(Me.txtInstallDate)
        Me.fraTop1.Controls.Add(Me.txtItemDesc)
        Me.fraTop1.Controls.Add(Me.txtSupplierName)
        Me.fraTop1.Controls.Add(Me.txtMRRDate)
        Me.fraTop1.Controls.Add(Me.txtMRRNo)
        Me.fraTop1.Controls.Add(Me.txtPVDate)
        Me.fraTop1.Controls.Add(Me.txtPVNo)
        Me.fraTop1.Controls.Add(Me.txtBillDate)
        Me.fraTop1.Controls.Add(Me.txtBillNo)
        Me.fraTop1.Controls.Add(Me.cboStatus)
        Me.fraTop1.Controls.Add(Me.txtLocation)
        Me.fraTop1.Controls.Add(Me.txtRemarks)
        Me.fraTop1.Controls.Add(Me.cmdSearchAssetType)
        Me.fraTop1.Controls.Add(Me.txtAssetType)
        Me.fraTop1.Controls.Add(Me.cmdSearchAssetCode)
        Me.fraTop1.Controls.Add(Me.txtAssetSNo)
        Me.fraTop1.Controls.Add(Me.SSTab1)
        Me.fraTop1.Controls.Add(Me.Label23)
        Me.fraTop1.Controls.Add(Me.Label9)
        Me.fraTop1.Controls.Add(Me.Label8)
        Me.fraTop1.Controls.Add(Me.Label33)
        Me.fraTop1.Controls.Add(Me.Label32)
        Me.fraTop1.Controls.Add(Me.Label31)
        Me.fraTop1.Controls.Add(Me.lblBookType)
        Me.fraTop1.Controls.Add(Me.lblVMKey)
        Me.fraTop1.Controls.Add(Me.Label3)
        Me.fraTop1.Controls.Add(Me.lblAssetCode)
        Me.fraTop1.Controls.Add(Me.Label17)
        Me.fraTop1.Controls.Add(Me.Label22)
        Me.fraTop1.Controls.Add(Me.Label21)
        Me.fraTop1.Controls.Add(Me.Label20)
        Me.fraTop1.Controls.Add(Me.Label19)
        Me.fraTop1.Controls.Add(Me.Label15)
        Me.fraTop1.Controls.Add(Me.Label14)
        Me.fraTop1.Controls.Add(Me.Label12)
        Me.fraTop1.Controls.Add(Me.Label11)
        Me.fraTop1.Controls.Add(Me.Label4)
        Me.fraTop1.Controls.Add(Me.Label28)
        Me.fraTop1.Controls.Add(Me.Label24)
        Me.fraTop1.Controls.Add(Me.Label1)
        Me.fraTop1.Controls.Add(Me.Label2)
        Me.fraTop1.Controls.Add(Me.Label5)
        Me.fraTop1.Controls.Add(Me.Label6)
        Me.fraTop1.Controls.Add(Me.Label13)
        Me.fraTop1.Controls.Add(Me.Label10)
        Me.fraTop1.Controls.Add(Me.Label18)
        Me.fraTop1.Controls.Add(Me.Label16)
        Me.fraTop1.Controls.Add(Me.Label7)
        Me.fraTop1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTop1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraTop1.Location = New System.Drawing.Point(0, -6)
        Me.fraTop1.Name = "fraTop1"
        Me.fraTop1.Padding = New System.Windows.Forms.Padding(0)
        Me.fraTop1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraTop1.Size = New System.Drawing.Size(766, 469)
        Me.fraTop1.TabIndex = 42
        Me.fraTop1.TabStop = False
        '
        'txtIGSTAmount
        '
        Me.txtIGSTAmount.AcceptsReturn = True
        Me.txtIGSTAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtIGSTAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIGSTAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIGSTAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIGSTAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtIGSTAmount.Location = New System.Drawing.Point(636, 222)
        Me.txtIGSTAmount.MaxLength = 0
        Me.txtIGSTAmount.Name = "txtIGSTAmount"
        Me.txtIGSTAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIGSTAmount.Size = New System.Drawing.Size(91, 20)
        Me.txtIGSTAmount.TabIndex = 23
        '
        'txtSGSTAmount
        '
        Me.txtSGSTAmount.AcceptsReturn = True
        Me.txtSGSTAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtSGSTAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSGSTAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSGSTAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSGSTAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSGSTAmount.Location = New System.Drawing.Point(636, 202)
        Me.txtSGSTAmount.MaxLength = 0
        Me.txtSGSTAmount.Name = "txtSGSTAmount"
        Me.txtSGSTAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSGSTAmount.Size = New System.Drawing.Size(91, 20)
        Me.txtSGSTAmount.TabIndex = 20
        '
        'txtCGSTAmount
        '
        Me.txtCGSTAmount.AcceptsReturn = True
        Me.txtCGSTAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtCGSTAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCGSTAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCGSTAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCGSTAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtCGSTAmount.Location = New System.Drawing.Point(636, 182)
        Me.txtCGSTAmount.MaxLength = 0
        Me.txtCGSTAmount.Name = "txtCGSTAmount"
        Me.txtCGSTAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCGSTAmount.Size = New System.Drawing.Size(91, 20)
        Me.txtCGSTAmount.TabIndex = 17
        '
        'txtSTRefund
        '
        Me.txtSTRefund.AcceptsReturn = True
        Me.txtSTRefund.BackColor = System.Drawing.SystemColors.Window
        Me.txtSTRefund.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSTRefund.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSTRefund.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSTRefund.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSTRefund.Location = New System.Drawing.Point(171, 242)
        Me.txtSTRefund.MaxLength = 0
        Me.txtSTRefund.Name = "txtSTRefund"
        Me.txtSTRefund.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSTRefund.Size = New System.Drawing.Size(91, 20)
        Me.txtSTRefund.TabIndex = 24
        '
        'txtItemType
        '
        Me.txtItemType.AcceptsReturn = True
        Me.txtItemType.BackColor = System.Drawing.SystemColors.Window
        Me.txtItemType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemType.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItemType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtItemType.Location = New System.Drawing.Point(525, 35)
        Me.txtItemType.MaxLength = 0
        Me.txtItemType.Name = "txtItemType"
        Me.txtItemType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemType.Size = New System.Drawing.Size(205, 20)
        Me.txtItemType.TabIndex = 4
        '
        'txtDrCrAmount
        '
        Me.txtDrCrAmount.AcceptsReturn = True
        Me.txtDrCrAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtDrCrAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDrCrAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDrCrAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDrCrAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDrCrAmount.Location = New System.Drawing.Point(170, 304)
        Me.txtDrCrAmount.MaxLength = 0
        Me.txtDrCrAmount.Name = "txtDrCrAmount"
        Me.txtDrCrAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDrCrAmount.Size = New System.Drawing.Size(91, 20)
        Me.txtDrCrAmount.TabIndex = 30
        '
        'chkCancelled
        '
        Me.chkCancelled.AutoSize = True
        Me.chkCancelled.BackColor = System.Drawing.SystemColors.Control
        Me.chkCancelled.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCancelled.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCancelled.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkCancelled.Location = New System.Drawing.Point(526, 16)
        Me.chkCancelled.Name = "chkCancelled"
        Me.chkCancelled.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCancelled.Size = New System.Drawing.Size(80, 18)
        Me.chkCancelled.TabIndex = 69
        Me.chkCancelled.Text = "Cancelled"
        Me.chkCancelled.UseVisualStyleBackColor = False
        '
        'txtSalvageAmount
        '
        Me.txtSalvageAmount.AcceptsReturn = True
        Me.txtSalvageAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtSalvageAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSalvageAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSalvageAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalvageAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSalvageAmount.Location = New System.Drawing.Point(437, 262)
        Me.txtSalvageAmount.MaxLength = 0
        Me.txtSalvageAmount.Name = "txtSalvageAmount"
        Me.txtSalvageAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSalvageAmount.Size = New System.Drawing.Size(89, 20)
        Me.txtSalvageAmount.TabIndex = 27
        '
        'txtModvatPer
        '
        Me.txtModvatPer.AcceptsReturn = True
        Me.txtModvatPer.BackColor = System.Drawing.SystemColors.Window
        Me.txtModvatPer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModvatPer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtModvatPer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtModvatPer.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtModvatPer.Location = New System.Drawing.Point(171, 262)
        Me.txtModvatPer.MaxLength = 0
        Me.txtModvatPer.Name = "txtModvatPer"
        Me.txtModvatPer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtModvatPer.Size = New System.Drawing.Size(91, 20)
        Me.txtModvatPer.TabIndex = 26
        '
        'txtCDAmount
        '
        Me.txtCDAmount.AcceptsReturn = True
        Me.txtCDAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtCDAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCDAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCDAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCDAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtCDAmount.Location = New System.Drawing.Point(437, 202)
        Me.txtCDAmount.MaxLength = 0
        Me.txtCDAmount.Name = "txtCDAmount"
        Me.txtCDAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCDAmount.Size = New System.Drawing.Size(89, 20)
        Me.txtCDAmount.TabIndex = 19
        '
        'txtItemValue
        '
        Me.txtItemValue.AcceptsReturn = True
        Me.txtItemValue.BackColor = System.Drawing.SystemColors.Window
        Me.txtItemValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemValue.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItemValue.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtItemValue.Location = New System.Drawing.Point(171, 202)
        Me.txtItemValue.MaxLength = 0
        Me.txtItemValue.Name = "txtItemValue"
        Me.txtItemValue.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemValue.Size = New System.Drawing.Size(91, 20)
        Me.txtItemValue.TabIndex = 18
        '
        'txtModvatAmount
        '
        Me.txtModvatAmount.AcceptsReturn = True
        Me.txtModvatAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtModvatAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModvatAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtModvatAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtModvatAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtModvatAmount.Location = New System.Drawing.Point(437, 222)
        Me.txtModvatAmount.MaxLength = 0
        Me.txtModvatAmount.Name = "txtModvatAmount"
        Me.txtModvatAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtModvatAmount.Size = New System.Drawing.Size(89, 20)
        Me.txtModvatAmount.TabIndex = 22
        '
        'txtInstallAmount
        '
        Me.txtInstallAmount.AcceptsReturn = True
        Me.txtInstallAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtInstallAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInstallAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInstallAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInstallAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtInstallAmount.Location = New System.Drawing.Point(171, 222)
        Me.txtInstallAmount.MaxLength = 0
        Me.txtInstallAmount.Name = "txtInstallAmount"
        Me.txtInstallAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInstallAmount.Size = New System.Drawing.Size(91, 20)
        Me.txtInstallAmount.TabIndex = 21
        '
        'txtTotalCost
        '
        Me.txtTotalCost.AcceptsReturn = True
        Me.txtTotalCost.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotalCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalCost.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotalCost.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalCost.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTotalCost.Location = New System.Drawing.Point(437, 242)
        Me.txtTotalCost.MaxLength = 0
        Me.txtTotalCost.Name = "txtTotalCost"
        Me.txtTotalCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotalCost.Size = New System.Drawing.Size(89, 20)
        Me.txtTotalCost.TabIndex = 25
        '
        'txtPutDate
        '
        Me.txtPutDate.AcceptsReturn = True
        Me.txtPutDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtPutDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPutDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPutDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPutDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPutDate.Location = New System.Drawing.Point(437, 182)
        Me.txtPutDate.MaxLength = 0
        Me.txtPutDate.Name = "txtPutDate"
        Me.txtPutDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPutDate.Size = New System.Drawing.Size(89, 20)
        Me.txtPutDate.TabIndex = 16
        '
        'txtInstallDate
        '
        Me.txtInstallDate.AcceptsReturn = True
        Me.txtInstallDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtInstallDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInstallDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInstallDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInstallDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtInstallDate.Location = New System.Drawing.Point(171, 182)
        Me.txtInstallDate.MaxLength = 0
        Me.txtInstallDate.Name = "txtInstallDate"
        Me.txtInstallDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInstallDate.Size = New System.Drawing.Size(91, 20)
        Me.txtInstallDate.TabIndex = 15
        '
        'txtItemDesc
        '
        Me.txtItemDesc.AcceptsReturn = True
        Me.txtItemDesc.BackColor = System.Drawing.SystemColors.Window
        Me.txtItemDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemDesc.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItemDesc.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemDesc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtItemDesc.Location = New System.Drawing.Point(171, 136)
        Me.txtItemDesc.MaxLength = 0
        Me.txtItemDesc.Multiline = True
        Me.txtItemDesc.Name = "txtItemDesc"
        Me.txtItemDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemDesc.Size = New System.Drawing.Size(555, 45)
        Me.txtItemDesc.TabIndex = 14
        '
        'txtSupplierName
        '
        Me.txtSupplierName.AcceptsReturn = True
        Me.txtSupplierName.BackColor = System.Drawing.SystemColors.Window
        Me.txtSupplierName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplierName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSupplierName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplierName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSupplierName.Location = New System.Drawing.Point(171, 116)
        Me.txtSupplierName.MaxLength = 0
        Me.txtSupplierName.Name = "txtSupplierName"
        Me.txtSupplierName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSupplierName.Size = New System.Drawing.Size(553, 20)
        Me.txtSupplierName.TabIndex = 13
        '
        'txtMRRDate
        '
        Me.txtMRRDate.AcceptsReturn = True
        Me.txtMRRDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtMRRDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMRRDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMRRDate.Enabled = False
        Me.txtMRRDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMRRDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtMRRDate.Location = New System.Drawing.Point(525, 56)
        Me.txtMRRDate.MaxLength = 0
        Me.txtMRRDate.Name = "txtMRRDate"
        Me.txtMRRDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMRRDate.Size = New System.Drawing.Size(109, 20)
        Me.txtMRRDate.TabIndex = 8
        '
        'txtMRRNo
        '
        Me.txtMRRNo.AcceptsReturn = True
        Me.txtMRRNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtMRRNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMRRNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMRRNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMRRNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtMRRNo.Location = New System.Drawing.Point(171, 56)
        Me.txtMRRNo.MaxLength = 0
        Me.txtMRRNo.Name = "txtMRRNo"
        Me.txtMRRNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMRRNo.Size = New System.Drawing.Size(109, 20)
        Me.txtMRRNo.TabIndex = 6
        '
        'txtPVDate
        '
        Me.txtPVDate.AcceptsReturn = True
        Me.txtPVDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtPVDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPVDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPVDate.Enabled = False
        Me.txtPVDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPVDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPVDate.Location = New System.Drawing.Point(525, 76)
        Me.txtPVDate.MaxLength = 0
        Me.txtPVDate.Name = "txtPVDate"
        Me.txtPVDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPVDate.Size = New System.Drawing.Size(109, 20)
        Me.txtPVDate.TabIndex = 10
        '
        'txtPVNo
        '
        Me.txtPVNo.AcceptsReturn = True
        Me.txtPVNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPVNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPVNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPVNo.Enabled = False
        Me.txtPVNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPVNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPVNo.Location = New System.Drawing.Point(171, 76)
        Me.txtPVNo.MaxLength = 0
        Me.txtPVNo.Name = "txtPVNo"
        Me.txtPVNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPVNo.Size = New System.Drawing.Size(109, 20)
        Me.txtPVNo.TabIndex = 9
        '
        'txtBillDate
        '
        Me.txtBillDate.AcceptsReturn = True
        Me.txtBillDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillDate.Enabled = False
        Me.txtBillDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtBillDate.Location = New System.Drawing.Point(525, 96)
        Me.txtBillDate.MaxLength = 0
        Me.txtBillDate.Name = "txtBillDate"
        Me.txtBillDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillDate.Size = New System.Drawing.Size(109, 20)
        Me.txtBillDate.TabIndex = 12
        '
        'txtBillNo
        '
        Me.txtBillNo.AcceptsReturn = True
        Me.txtBillNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillNo.Enabled = False
        Me.txtBillNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtBillNo.Location = New System.Drawing.Point(171, 96)
        Me.txtBillNo.MaxLength = 0
        Me.txtBillNo.Name = "txtBillNo"
        Me.txtBillNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillNo.Size = New System.Drawing.Size(109, 20)
        Me.txtBillNo.TabIndex = 11
        '
        'cboStatus
        '
        Me.cboStatus.BackColor = System.Drawing.SystemColors.Window
        Me.cboStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatus.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboStatus.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboStatus.Location = New System.Drawing.Point(437, 304)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboStatus.Size = New System.Drawing.Size(95, 22)
        Me.cboStatus.TabIndex = 31
        '
        'txtLocation
        '
        Me.txtLocation.AcceptsReturn = True
        Me.txtLocation.BackColor = System.Drawing.SystemColors.Window
        Me.txtLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLocation.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLocation.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtLocation.Location = New System.Drawing.Point(171, 283)
        Me.txtLocation.MaxLength = 0
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLocation.Size = New System.Drawing.Size(91, 20)
        Me.txtLocation.TabIndex = 28
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtRemarks.Location = New System.Drawing.Point(437, 283)
        Me.txtRemarks.MaxLength = 0
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.Size = New System.Drawing.Size(89, 20)
        Me.txtRemarks.TabIndex = 29
        '
        'txtAssetType
        '
        Me.txtAssetType.AcceptsReturn = True
        Me.txtAssetType.BackColor = System.Drawing.SystemColors.Window
        Me.txtAssetType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAssetType.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAssetType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAssetType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAssetType.Location = New System.Drawing.Point(171, 35)
        Me.txtAssetType.MaxLength = 0
        Me.txtAssetType.Name = "txtAssetType"
        Me.txtAssetType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAssetType.Size = New System.Drawing.Size(205, 20)
        Me.txtAssetType.TabIndex = 2
        '
        'txtAssetSNo
        '
        Me.txtAssetSNo.AcceptsReturn = True
        Me.txtAssetSNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtAssetSNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAssetSNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAssetSNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAssetSNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtAssetSNo.Location = New System.Drawing.Point(171, 13)
        Me.txtAssetSNo.MaxLength = 0
        Me.txtAssetSNo.Name = "txtAssetSNo"
        Me.txtAssetSNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAssetSNo.Size = New System.Drawing.Size(109, 20)
        Me.txtAssetSNo.TabIndex = 0
        '
        'SSTab1
        '
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage0)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage1)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage2)
        Me.SSTab1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SSTab1.ItemSize = New System.Drawing.Size(42, 18)
        Me.SSTab1.Location = New System.Drawing.Point(2, 328)
        Me.SSTab1.Name = "SSTab1"
        Me.SSTab1.SelectedIndex = 0
        Me.SSTab1.Size = New System.Drawing.Size(761, 139)
        Me.SSTab1.TabIndex = 70
        '
        '_SSTab1_TabPage0
        '
        Me._SSTab1_TabPage0.Controls.Add(Me.fraSale)
        Me._SSTab1_TabPage0.Location = New System.Drawing.Point(4, 22)
        Me._SSTab1_TabPage0.Name = "_SSTab1_TabPage0"
        Me._SSTab1_TabPage0.Size = New System.Drawing.Size(753, 113)
        Me._SSTab1_TabPage0.TabIndex = 0
        Me._SSTab1_TabPage0.Text = "Sale Detail"
        '
        'fraSale
        '
        Me.fraSale.BackColor = System.Drawing.SystemColors.Control
        Me.fraSale.Controls.Add(Me.SprdSale)
        Me.fraSale.Dock = System.Windows.Forms.DockStyle.Fill
        Me.fraSale.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraSale.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraSale.Location = New System.Drawing.Point(0, 0)
        Me.fraSale.Name = "fraSale"
        Me.fraSale.Padding = New System.Windows.Forms.Padding(0)
        Me.fraSale.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraSale.Size = New System.Drawing.Size(753, 113)
        Me.fraSale.TabIndex = 73
        Me.fraSale.TabStop = False
        '
        'SprdSale
        '
        Me.SprdSale.DataSource = Nothing
        Me.SprdSale.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SprdSale.Location = New System.Drawing.Point(0, 13)
        Me.SprdSale.Name = "SprdSale"
        Me.SprdSale.OcxState = CType(resources.GetObject("SprdSale.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdSale.Size = New System.Drawing.Size(753, 100)
        Me.SprdSale.TabIndex = 77
        '
        '_SSTab1_TabPage1
        '
        Me._SSTab1_TabPage1.Controls.Add(Me.Frame1)
        Me._SSTab1_TabPage1.Location = New System.Drawing.Point(4, 22)
        Me._SSTab1_TabPage1.Name = "_SSTab1_TabPage1"
        Me._SSTab1_TabPage1.Size = New System.Drawing.Size(753, 113)
        Me._SSTab1_TabPage1.TabIndex = 1
        Me._SSTab1_TabPage1.Text = "Depreciation Detail (Company Act)"
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.SprdMain)
        Me.Frame1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(753, 113)
        Me.Frame1.TabIndex = 71
        Me.Frame1.TabStop = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SprdMain.Location = New System.Drawing.Point(0, 13)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(753, 100)
        Me.SprdMain.TabIndex = 72
        '
        '_SSTab1_TabPage2
        '
        Me._SSTab1_TabPage2.Location = New System.Drawing.Point(4, 22)
        Me._SSTab1_TabPage2.Name = "_SSTab1_TabPage2"
        Me._SSTab1_TabPage2.Size = New System.Drawing.Size(753, 113)
        Me._SSTab1_TabPage2.TabIndex = 2
        Me._SSTab1_TabPage2.Text = "Purchase Details"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.SystemColors.Control
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(551, 225)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(85, 14)
        Me.Label23.TabIndex = 80
        Me.Label23.Text = "IGST Amount :"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(547, 205)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(89, 14)
        Me.Label9.TabIndex = 79
        Me.Label9.Text = "SGST Amount :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(547, 185)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(90, 14)
        Me.Label8.TabIndex = 78
        Me.Label8.Text = "CGST Amount :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.BackColor = System.Drawing.SystemColors.Control
        Me.Label33.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label33.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label33.Location = New System.Drawing.Point(55, 245)
        Me.Label33.Name = "Label33"
        Me.Label33.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label33.Size = New System.Drawing.Size(106, 14)
        Me.Label33.TabIndex = 76
        Me.Label33.Text = "Sales Tax Refund :"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.BackColor = System.Drawing.SystemColors.Control
        Me.Label32.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label32.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label32.Location = New System.Drawing.Point(453, 38)
        Me.Label32.Name = "Label32"
        Me.Label32.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label32.Size = New System.Drawing.Size(67, 14)
        Me.Label32.TabIndex = 75
        Me.Label32.Text = "Item Type :"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.BackColor = System.Drawing.SystemColors.Control
        Me.Label31.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label31.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label31.Location = New System.Drawing.Point(3, 307)
        Me.Label31.Name = "Label31"
        Me.Label31.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label31.Size = New System.Drawing.Size(159, 14)
        Me.Label31.TabIndex = 74
        Me.Label31.Text = "Debit / Credit Note Amount :"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblBookType
        '
        Me.lblBookType.AutoSize = True
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(664, 210)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(64, 14)
        Me.lblBookType.TabIndex = 68
        Me.lblBookType.Text = "lblBookType"
        '
        'lblVMKey
        '
        Me.lblVMKey.BackColor = System.Drawing.SystemColors.Control
        Me.lblVMKey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblVMKey.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVMKey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblVMKey.Location = New System.Drawing.Point(666, 182)
        Me.lblVMKey.Name = "lblVMKey"
        Me.lblVMKey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblVMKey.Size = New System.Drawing.Size(87, 13)
        Me.lblVMKey.TabIndex = 67
        Me.lblVMKey.Text = "lblVMKey"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(334, 265)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(102, 14)
        Me.Label3.TabIndex = 66
        Me.Label3.Text = "Salvage Amount :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblAssetCode
        '
        Me.lblAssetCode.AutoSize = True
        Me.lblAssetCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblAssetCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAssetCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAssetCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAssetCode.Location = New System.Drawing.Point(318, 16)
        Me.lblAssetCode.Name = "lblAssetCode"
        Me.lblAssetCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAssetCode.Size = New System.Drawing.Size(71, 14)
        Me.lblAssetCode.TabIndex = 65
        Me.lblAssetCode.Text = "lblAssetCode"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(12, 265)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(152, 14)
        Me.Label17.TabIndex = 64
        Me.Label17.Text = "Modvat % During the Year :"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.SystemColors.Control
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(287, 205)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(142, 14)
        Me.Label22.TabIndex = 63
        Me.Label22.Text = "C.D. / Ins. && Freight Amt :"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.SystemColors.Control
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(100, 205)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(71, 14)
        Me.Label21.TabIndex = 62
        Me.Label21.Text = "Item Value :"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(338, 225)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(100, 14)
        Me.Label20.TabIndex = 61
        Me.Label20.Text = "Modvat Amount :"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(52, 225)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(118, 14)
        Me.Label19.TabIndex = 60
        Me.Label19.Text = "Installation && Other :"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(338, 245)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(99, 14)
        Me.Label15.TabIndex = 59
        Me.Label15.Text = "Invoice Amount :"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(276, 185)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(149, 14)
        Me.Label14.TabIndex = 58
        Me.Label14.Text = "Date on Which Put to Use :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(51, 185)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(114, 14)
        Me.Label12.TabIndex = 57
        Me.Label12.Text = "Date of Installation :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(103, 139)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(68, 14)
        Me.Label11.TabIndex = 56
        Me.Label11.Text = "Item Desc :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(78, 119)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(93, 14)
        Me.Label4.TabIndex = 55
        Me.Label4.Text = "Supplier Name :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.SystemColors.Control
        Me.Label28.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label28.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label28.Location = New System.Drawing.Point(451, 59)
        Me.Label28.Name = "Label28"
        Me.Label28.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label28.Size = New System.Drawing.Size(64, 14)
        Me.Label28.TabIndex = 54
        Me.Label28.Text = "MRR Date :"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.SystemColors.Control
        Me.Label24.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label24.Location = New System.Drawing.Point(112, 59)
        Me.Label24.Name = "Label24"
        Me.Label24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label24.Size = New System.Drawing.Size(54, 14)
        Me.Label24.TabIndex = 53
        Me.Label24.Text = "MRR No :"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(426, 79)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(92, 14)
        Me.Label1.TabIndex = 52
        Me.Label1.Text = "Purchase Date :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(87, 79)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(82, 14)
        Me.Label2.TabIndex = 51
        Me.Label2.Text = "Purchase No :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(405, 99)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(111, 14)
        Me.Label5.TabIndex = 50
        Me.Label5.Text = "Purchase Bill Date :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(66, 99)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(101, 14)
        Me.Label6.TabIndex = 49
        Me.Label6.Text = "Purchase Bill No :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(390, 307)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(48, 14)
        Me.Label13.TabIndex = 48
        Me.Label13.Text = "Status :"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(109, 286)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(60, 14)
        Me.Label10.TabIndex = 47
        Me.Label10.Text = "Location :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(375, 286)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(63, 14)
        Me.Label18.TabIndex = 46
        Me.Label18.Text = "Remarks :"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(95, 38)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(75, 14)
        Me.Label16.TabIndex = 45
        Me.Label16.Text = "Asset Type :"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(124, 18)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(40, 14)
        Me.Label7.TabIndex = 43
        Me.Label7.Text = "S. No :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
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
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(766, 463)
        Me.SprdView.TabIndex = 44
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
        Me.FraMovement.Controls.Add(Me.CmdModify)
        Me.FraMovement.Controls.Add(Me.CmdAdd)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 458)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(767, 51)
        Me.FraMovement.TabIndex = 41
        Me.FraMovement.TabStop = False
        '
        'frmAssetsEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(767, 509)
        Me.Controls.Add(Me.fraTop1)
        Me.Controls.Add(Me.Report1)
        Me.Controls.Add(Me.SprdView)
        Me.Controls.Add(Me.FraMovement)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(-242, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAssetsEntry"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds
        Me.Text = "Assets Entry"
        Me.fraTop1.ResumeLayout(False)
        Me.fraTop1.PerformLayout()
        Me.SSTab1.ResumeLayout(False)
        Me._SSTab1_TabPage0.ResumeLayout(False)
        Me.fraSale.ResumeLayout(False)
        CType(Me.SprdSale, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage1.ResumeLayout(False)
        Me.Frame1.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        SprdView.DataSource = Nothing
    End Sub
	Public Sub VB6_RemoveADODataBinding()
		SprdView.DataSource = Nothing
	End Sub
#End Region 
End Class