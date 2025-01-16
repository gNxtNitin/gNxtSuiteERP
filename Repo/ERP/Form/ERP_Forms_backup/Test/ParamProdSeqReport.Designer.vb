<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmParamProdSeqReport
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
		Me.MDIParent = Production.Master
		Production.Master.Show
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
	Public WithEvents chkDept As System.Windows.Forms.CheckBox
	Public WithEvents txtDept As System.Windows.Forms.TextBox
	Public WithEvents cmdSearchDept As System.Windows.Forms.Button
	Public WithEvents cmdItemDesc As System.Windows.Forms.Button
	Public WithEvents txtItemName As System.Windows.Forms.TextBox
	Public WithEvents chkItemAll As System.Windows.Forms.CheckBox
	Public WithEvents chkOPR As System.Windows.Forms.CheckBox
	Public WithEvents txtOPRName As System.Windows.Forms.TextBox
	Public WithEvents cmdSearchOPR As System.Windows.Forms.Button
	Public WithEvents Label6 As System.Windows.Forms.Label
	Public WithEvents Label5 As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents Frame3 As System.Windows.Forms.GroupBox
	Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
	Public WithEvents Report1 As AxCrystal.AxCrystalReport
	Public WithEvents CmdPreview As System.Windows.Forms.Button
	Public WithEvents cmdPrint As System.Windows.Forms.Button
	Public WithEvents cmdShow As System.Windows.Forms.Button
	Public WithEvents cmdExit As System.Windows.Forms.Button
	Public WithEvents lblBookType As System.Windows.Forms.Label
	Public WithEvents Frame2 As System.Windows.Forms.GroupBox
	Public WithEvents AData1 As VB6.ADODC
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmParamProdSeqReport))
		Me.components = New System.ComponentModel.Container()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
		Me.Frame3 = New System.Windows.Forms.GroupBox
		Me.chkDept = New System.Windows.Forms.CheckBox
		Me.txtDept = New System.Windows.Forms.TextBox
		Me.cmdSearchDept = New System.Windows.Forms.Button
		Me.cmdItemDesc = New System.Windows.Forms.Button
		Me.txtItemName = New System.Windows.Forms.TextBox
		Me.chkItemAll = New System.Windows.Forms.CheckBox
		Me.chkOPR = New System.Windows.Forms.CheckBox
		Me.txtOPRName = New System.Windows.Forms.TextBox
		Me.cmdSearchOPR = New System.Windows.Forms.Button
		Me.Label6 = New System.Windows.Forms.Label
		Me.Label5 = New System.Windows.Forms.Label
		Me.Label1 = New System.Windows.Forms.Label
		Me.SprdMain = New AxFPSpreadADO.AxfpSpread
		Me.Report1 = New AxCrystal.AxCrystalReport
		Me.Frame2 = New System.Windows.Forms.GroupBox
		Me.CmdPreview = New System.Windows.Forms.Button
		Me.cmdPrint = New System.Windows.Forms.Button
		Me.cmdShow = New System.Windows.Forms.Button
		Me.cmdExit = New System.Windows.Forms.Button
		Me.lblBookType = New System.Windows.Forms.Label
		Me.AData1 = New VB6.ADODC
		Me.Frame3.SuspendLayout()
		Me.Frame2.SuspendLayout()
		Me.SuspendLayout()
		Me.ToolTip1.Active = True
		CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
		Me.Text = "Production Sequence Report"
		Me.ClientSize = New System.Drawing.Size(751, 458)
		Me.Location = New System.Drawing.Point(4, 16)
		Me.Icon = CType(resources.GetObject("frmParamProdSeqReport.Icon"), System.Drawing.Icon)
		Me.MaximizeBox = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds
		Me.MinimizeBox = False
		Me.ShowInTaskbar = False
		Me.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.SystemColors.Control
		Me.ControlBox = True
		Me.Enabled = True
		Me.KeyPreview = False
		Me.Cursor = System.Windows.Forms.Cursors.Default
		Me.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.HelpButton = False
		Me.WindowState = System.Windows.Forms.FormWindowState.Normal
		Me.Name = "frmParamProdSeqReport"
		Me.Frame3.Text = "Show"
		Me.Frame3.Size = New System.Drawing.Size(751, 57)
		Me.Frame3.Location = New System.Drawing.Point(0, 0)
		Me.Frame3.TabIndex = 6
		Me.Frame3.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame3.BackColor = System.Drawing.SystemColors.Control
		Me.Frame3.Enabled = True
		Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame3.Visible = True
		Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame3.Name = "Frame3"
		Me.chkDept.Text = "All"
		Me.chkDept.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.chkDept.Size = New System.Drawing.Size(39, 13)
		Me.chkDept.Location = New System.Drawing.Point(704, 14)
		Me.chkDept.TabIndex = 18
		Me.chkDept.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkDept.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.chkDept.FlatStyle = System.Windows.Forms.FlatStyle.Standard
		Me.chkDept.BackColor = System.Drawing.SystemColors.Control
		Me.chkDept.CausesValidation = True
		Me.chkDept.Enabled = True
		Me.chkDept.ForeColor = System.Drawing.SystemColors.ControlText
		Me.chkDept.Cursor = System.Windows.Forms.Cursors.Default
		Me.chkDept.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.chkDept.Appearance = System.Windows.Forms.Appearance.Normal
		Me.chkDept.TabStop = True
		Me.chkDept.Visible = True
		Me.chkDept.Name = "chkDept"
		Me.txtDept.AutoSize = False
		Me.txtDept.Enabled = False
		Me.txtDept.ForeColor = System.Drawing.Color.Blue
		Me.txtDept.Size = New System.Drawing.Size(173, 19)
		Me.txtDept.Location = New System.Drawing.Point(506, 10)
		Me.txtDept.TabIndex = 17
		Me.txtDept.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtDept.AcceptsReturn = True
		Me.txtDept.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtDept.BackColor = System.Drawing.SystemColors.Window
		Me.txtDept.CausesValidation = True
		Me.txtDept.HideSelection = True
		Me.txtDept.ReadOnly = False
		Me.txtDept.Maxlength = 0
		Me.txtDept.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtDept.MultiLine = False
		Me.txtDept.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtDept.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtDept.TabStop = True
		Me.txtDept.Visible = True
		Me.txtDept.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtDept.Name = "txtDept"
		Me.cmdSearchDept.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.cmdSearchDept.BackColor = System.Drawing.SystemColors.Menu
		Me.cmdSearchDept.Enabled = False
		Me.cmdSearchDept.Size = New System.Drawing.Size(23, 19)
		Me.cmdSearchDept.Location = New System.Drawing.Point(680, 10)
		Me.cmdSearchDept.Image = CType(resources.GetObject("cmdSearchDept.Image"), System.Drawing.Image)
		Me.cmdSearchDept.TabIndex = 16
		Me.cmdSearchDept.TabStop = False
		Me.ToolTip1.SetToolTip(Me.cmdSearchDept, "Search")
		Me.cmdSearchDept.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdSearchDept.CausesValidation = True
		Me.cmdSearchDept.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdSearchDept.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdSearchDept.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdSearchDept.Name = "cmdSearchDept"
		Me.cmdItemDesc.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.cmdItemDesc.BackColor = System.Drawing.SystemColors.Menu
		Me.cmdItemDesc.Enabled = False
		Me.cmdItemDesc.Size = New System.Drawing.Size(23, 19)
		Me.cmdItemDesc.Location = New System.Drawing.Point(362, 10)
		Me.cmdItemDesc.Image = CType(resources.GetObject("cmdItemDesc.Image"), System.Drawing.Image)
		Me.cmdItemDesc.TabIndex = 13
		Me.cmdItemDesc.TabStop = False
		Me.ToolTip1.SetToolTip(Me.cmdItemDesc, "Search")
		Me.cmdItemDesc.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdItemDesc.CausesValidation = True
		Me.cmdItemDesc.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdItemDesc.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdItemDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdItemDesc.Name = "cmdItemDesc"
		Me.txtItemName.AutoSize = False
		Me.txtItemName.Enabled = False
		Me.txtItemName.ForeColor = System.Drawing.Color.Blue
		Me.txtItemName.Size = New System.Drawing.Size(283, 19)
		Me.txtItemName.Location = New System.Drawing.Point(76, 10)
		Me.txtItemName.TabIndex = 12
		Me.txtItemName.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtItemName.AcceptsReturn = True
		Me.txtItemName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtItemName.BackColor = System.Drawing.SystemColors.Window
		Me.txtItemName.CausesValidation = True
		Me.txtItemName.HideSelection = True
		Me.txtItemName.ReadOnly = False
		Me.txtItemName.Maxlength = 0
		Me.txtItemName.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtItemName.MultiLine = False
		Me.txtItemName.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtItemName.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtItemName.TabStop = True
		Me.txtItemName.Visible = True
		Me.txtItemName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtItemName.Name = "txtItemName"
		Me.chkItemAll.Text = "All"
		Me.chkItemAll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.chkItemAll.Size = New System.Drawing.Size(39, 13)
		Me.chkItemAll.Location = New System.Drawing.Point(386, 14)
		Me.chkItemAll.TabIndex = 11
		Me.chkItemAll.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkItemAll.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.chkItemAll.FlatStyle = System.Windows.Forms.FlatStyle.Standard
		Me.chkItemAll.BackColor = System.Drawing.SystemColors.Control
		Me.chkItemAll.CausesValidation = True
		Me.chkItemAll.Enabled = True
		Me.chkItemAll.ForeColor = System.Drawing.SystemColors.ControlText
		Me.chkItemAll.Cursor = System.Windows.Forms.Cursors.Default
		Me.chkItemAll.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.chkItemAll.Appearance = System.Windows.Forms.Appearance.Normal
		Me.chkItemAll.TabStop = True
		Me.chkItemAll.Visible = True
		Me.chkItemAll.Name = "chkItemAll"
		Me.chkOPR.Text = "All"
		Me.chkOPR.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.chkOPR.Size = New System.Drawing.Size(39, 13)
		Me.chkOPR.Location = New System.Drawing.Point(386, 34)
		Me.chkOPR.TabIndex = 10
		Me.chkOPR.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkOPR.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.chkOPR.FlatStyle = System.Windows.Forms.FlatStyle.Standard
		Me.chkOPR.BackColor = System.Drawing.SystemColors.Control
		Me.chkOPR.CausesValidation = True
		Me.chkOPR.Enabled = True
		Me.chkOPR.ForeColor = System.Drawing.SystemColors.ControlText
		Me.chkOPR.Cursor = System.Windows.Forms.Cursors.Default
		Me.chkOPR.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.chkOPR.Appearance = System.Windows.Forms.Appearance.Normal
		Me.chkOPR.TabStop = True
		Me.chkOPR.Visible = True
		Me.chkOPR.Name = "chkOPR"
		Me.txtOPRName.AutoSize = False
		Me.txtOPRName.Enabled = False
		Me.txtOPRName.ForeColor = System.Drawing.Color.Blue
		Me.txtOPRName.Size = New System.Drawing.Size(283, 19)
		Me.txtOPRName.Location = New System.Drawing.Point(76, 32)
		Me.txtOPRName.TabIndex = 8
		Me.txtOPRName.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtOPRName.AcceptsReturn = True
		Me.txtOPRName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtOPRName.BackColor = System.Drawing.SystemColors.Window
		Me.txtOPRName.CausesValidation = True
		Me.txtOPRName.HideSelection = True
		Me.txtOPRName.ReadOnly = False
		Me.txtOPRName.Maxlength = 0
		Me.txtOPRName.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtOPRName.MultiLine = False
		Me.txtOPRName.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtOPRName.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtOPRName.TabStop = True
		Me.txtOPRName.Visible = True
		Me.txtOPRName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtOPRName.Name = "txtOPRName"
		Me.cmdSearchOPR.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.cmdSearchOPR.BackColor = System.Drawing.SystemColors.Menu
		Me.cmdSearchOPR.Enabled = False
		Me.cmdSearchOPR.Size = New System.Drawing.Size(23, 19)
		Me.cmdSearchOPR.Location = New System.Drawing.Point(360, 32)
		Me.cmdSearchOPR.Image = CType(resources.GetObject("cmdSearchOPR.Image"), System.Drawing.Image)
		Me.cmdSearchOPR.TabIndex = 7
		Me.cmdSearchOPR.TabStop = False
		Me.ToolTip1.SetToolTip(Me.cmdSearchOPR, "Search")
		Me.cmdSearchOPR.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdSearchOPR.CausesValidation = True
		Me.cmdSearchOPR.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdSearchOPR.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdSearchOPR.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdSearchOPR.Name = "cmdSearchOPR"
		Me.Label6.Text = "Dept. :"
		Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label6.Size = New System.Drawing.Size(40, 13)
		Me.Label6.Location = New System.Drawing.Point(444, 12)
		Me.Label6.TabIndex = 19
		Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label6.BackColor = System.Drawing.SystemColors.Control
		Me.Label6.Enabled = True
		Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label6.UseMnemonic = True
		Me.Label6.Visible = True
		Me.Label6.AutoSize = True
		Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label6.Name = "Label6"
		Me.Label5.Text = "Item Desc :"
		Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label5.Size = New System.Drawing.Size(66, 13)
		Me.Label5.Location = New System.Drawing.Point(8, 12)
		Me.Label5.TabIndex = 14
		Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label5.BackColor = System.Drawing.SystemColors.Control
		Me.Label5.Enabled = True
		Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label5.UseMnemonic = True
		Me.Label5.Visible = True
		Me.Label5.AutoSize = True
		Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label5.Name = "Label5"
		Me.Label1.Text = "Operation :"
		Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.Size = New System.Drawing.Size(64, 13)
		Me.Label1.Location = New System.Drawing.Point(8, 34)
		Me.Label1.TabIndex = 9
		Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label1.BackColor = System.Drawing.SystemColors.Control
		Me.Label1.Enabled = True
		Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label1.UseMnemonic = True
		Me.Label1.Visible = True
		Me.Label1.AutoSize = True
		Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label1.Name = "Label1"
		SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SprdMain.Size = New System.Drawing.Size(751, 351)
		Me.SprdMain.Location = New System.Drawing.Point(0, 58)
		Me.SprdMain.TabIndex = 0
		Me.SprdMain.Name = "SprdMain"
		Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
		Me.Report1.Location = New System.Drawing.Point(84, 138)
		Me.Report1.Name = "Report1"
		Me.Frame2.Size = New System.Drawing.Size(251, 51)
		Me.Frame2.Location = New System.Drawing.Point(500, 406)
		Me.Frame2.TabIndex = 5
		Me.Frame2.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame2.BackColor = System.Drawing.SystemColors.Control
		Me.Frame2.Enabled = True
		Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame2.Visible = True
		Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame2.Name = "Frame2"
		Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.CmdPreview.Text = "Pre&view"
		Me.CmdPreview.Size = New System.Drawing.Size(60, 37)
		Me.CmdPreview.Location = New System.Drawing.Point(126, 10)
		Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
		Me.CmdPreview.TabIndex = 3
		Me.ToolTip1.SetToolTip(Me.CmdPreview, "Preview")
		Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
		Me.CmdPreview.CausesValidation = True
		Me.CmdPreview.Enabled = True
		Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
		Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
		Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CmdPreview.TabStop = True
		Me.CmdPreview.Name = "CmdPreview"
		Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.cmdPrint.Text = "&Print"
		Me.cmdPrint.Size = New System.Drawing.Size(60, 37)
		Me.cmdPrint.Location = New System.Drawing.Point(66, 10)
		Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
		Me.cmdPrint.TabIndex = 2
		Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print")
		Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
		Me.cmdPrint.CausesValidation = True
		Me.cmdPrint.Enabled = True
		Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdPrint.TabStop = True
		Me.cmdPrint.Name = "cmdPrint"
		Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.cmdShow.Text = "Sho&w"
		Me.cmdShow.Size = New System.Drawing.Size(60, 37)
		Me.cmdShow.Location = New System.Drawing.Point(6, 10)
		Me.cmdShow.Image = CType(resources.GetObject("cmdShow.Image"), System.Drawing.Image)
		Me.cmdShow.TabIndex = 1
		Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
		Me.cmdShow.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdShow.BackColor = System.Drawing.SystemColors.Control
		Me.cmdShow.CausesValidation = True
		Me.cmdShow.Enabled = True
		Me.cmdShow.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdShow.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdShow.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdShow.TabStop = True
		Me.cmdShow.Name = "cmdShow"
		Me.cmdExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.cmdExit.Text = "&Close"
		Me.cmdExit.Size = New System.Drawing.Size(60, 37)
		Me.cmdExit.Location = New System.Drawing.Point(186, 10)
		Me.cmdExit.Image = CType(resources.GetObject("cmdExit.Image"), System.Drawing.Image)
		Me.cmdExit.TabIndex = 4
		Me.ToolTip1.SetToolTip(Me.cmdExit, "Close the Form")
		Me.cmdExit.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
		Me.cmdExit.CausesValidation = True
		Me.cmdExit.Enabled = True
		Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdExit.TabStop = True
		Me.cmdExit.Name = "cmdExit"
		Me.lblBookType.Text = "lblBookType"
		Me.lblBookType.Size = New System.Drawing.Size(59, 13)
		Me.lblBookType.Location = New System.Drawing.Point(190, 18)
		Me.lblBookType.TabIndex = 15
		Me.lblBookType.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblBookType.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
		Me.lblBookType.Enabled = True
		Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblBookType.UseMnemonic = True
		Me.lblBookType.Visible = True
		Me.lblBookType.AutoSize = True
		Me.lblBookType.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lblBookType.Name = "lblBookType"
		Me.AData1.Size = New System.Drawing.Size(113, 28)
		Me.AData1.Location = New System.Drawing.Point(134, 202)
		Me.AData1.Visible = 0
		Me.AData1.CursorLocation = ADODB.CursorLocationEnum.adUseClient
		Me.AData1.ConnectionTimeout = 15
		Me.AData1.CommandTimeout = 30
		Me.AData1.CursorType = ADODB.CursorTypeEnum.adOpenStatic
		Me.AData1.LockType = ADODB.LockTypeEnum.adLockOptimistic
		Me.AData1.CommandType = ADODB.CommandTypeEnum.adCmdUnknown
		Me.AData1.CacheSize = 50
		Me.AData1.MaxRecords = 0
		Me.AData1.BOFAction = Microsoft.VisualBasic.Compatibility.VB6.ADODC.BOFActionEnum.adDoMoveFirst
		Me.AData1.EOFAction = Microsoft.VisualBasic.Compatibility.VB6.ADODC.EOFActionEnum.adDoMoveLast
		Me.AData1.BackColor = System.Drawing.SystemColors.Window
		Me.AData1.ForeColor = System.Drawing.SystemColors.WindowText
		Me.AData1.Orientation = Microsoft.VisualBasic.Compatibility.VB6.ADODC.OrientationEnum.adHorizontal
		Me.AData1.Enabled = True
		Me.AData1.UserName = ""
		Me.AData1.RecordSource = ""
		Me.AData1.Text = "Adodc1"
		Me.AData1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.AData1.ConnectionString = ""
		Me.AData1.Name = "AData1"
		CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
		Me.Controls.Add(Frame3)
		Me.Controls.Add(SprdMain)
		Me.Controls.Add(Report1)
		Me.Controls.Add(Frame2)
		Me.Controls.Add(AData1)
		Me.Frame3.Controls.Add(chkDept)
		Me.Frame3.Controls.Add(txtDept)
		Me.Frame3.Controls.Add(cmdSearchDept)
		Me.Frame3.Controls.Add(cmdItemDesc)
		Me.Frame3.Controls.Add(txtItemName)
		Me.Frame3.Controls.Add(chkItemAll)
		Me.Frame3.Controls.Add(chkOPR)
		Me.Frame3.Controls.Add(txtOPRName)
		Me.Frame3.Controls.Add(cmdSearchOPR)
		Me.Frame3.Controls.Add(Label6)
		Me.Frame3.Controls.Add(Label5)
		Me.Frame3.Controls.Add(Label1)
		Me.Frame2.Controls.Add(CmdPreview)
		Me.Frame2.Controls.Add(cmdPrint)
		Me.Frame2.Controls.Add(cmdShow)
		Me.Frame2.Controls.Add(cmdExit)
		Me.Frame2.Controls.Add(lblBookType)
		Me.Frame3.ResumeLayout(False)
		Me.Frame2.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()
	End Sub
#End Region 
#Region "Upgrade Support"
	Public Sub VB6_AddADODataBinding()
		SprdMain.DataSource = CType(AData1, MSDATASRC.DataSource)
	End Sub
	Public Sub VB6_RemoveADODataBinding()
		SprdMain.DataSource = Nothing
	End Sub
#End Region 
End Class