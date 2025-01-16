<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmTDSeReturn
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
		Me.MDIParent = TDS.Master
		TDS.Master.Show
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
	Public WithEvents _optForm_1 As System.Windows.Forms.RadioButton
	Public WithEvents _optForm_0 As System.Windows.Forms.RadioButton
	Public WithEvents TxtAccount As System.Windows.Forms.TextBox
	Public WithEvents CmdSearch As System.Windows.Forms.Button
	Public WithEvents txtFlat As System.Windows.Forms.TextBox
	Public WithEvents txtBuilding As System.Windows.Forms.TextBox
	Public WithEvents txtRoad As System.Windows.Forms.TextBox
	Public WithEvents txtArea As System.Windows.Forms.TextBox
	Public WithEvents txtTown As System.Windows.Forms.TextBox
	Public WithEvents txtState As System.Windows.Forms.TextBox
	Public WithEvents txtPinCode As System.Windows.Forms.TextBox
	Public WithEvents txtDesg As System.Windows.Forms.TextBox
	Public WithEvents txtPersonName As System.Windows.Forms.TextBox
	Public WithEvents txtReturnPeriod As System.Windows.Forms.TextBox
	Public WithEvents txtDeductorStatus As System.Windows.Forms.TextBox
	Public WithEvents txtAddressChange As System.Windows.Forms.TextBox
	Public WithEvents txtRundate As System.Windows.Forms.TextBox
	Public WithEvents txtDateFrom As System.Windows.Forms.MaskedTextBox
	Public WithEvents txtDateTo As System.Windows.Forms.MaskedTextBox
	Public WithEvents Label17 As System.Windows.Forms.Label
	Public WithEvents Label16 As System.Windows.Forms.Label
	Public WithEvents Label15 As System.Windows.Forms.Label
	Public WithEvents Label14 As System.Windows.Forms.Label
	Public WithEvents Label13 As System.Windows.Forms.Label
	Public WithEvents Label8 As System.Windows.Forms.Label
	Public WithEvents Label9 As System.Windows.Forms.Label
	Public WithEvents Label10 As System.Windows.Forms.Label
	Public WithEvents Label11 As System.Windows.Forms.Label
	Public WithEvents Label12 As System.Windows.Forms.Label
	Public WithEvents Label7 As System.Windows.Forms.Label
	Public WithEvents Label6 As System.Windows.Forms.Label
	Public WithEvents Label5 As System.Windows.Forms.Label
	Public WithEvents Label4 As System.Windows.Forms.Label
	Public WithEvents Label3 As System.Windows.Forms.Label
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents Lable11 As System.Windows.Forms.Label
	Public WithEvents Frame3 As System.Windows.Forms.GroupBox
	Public WithEvents _SSTab1_TabPage0 As System.Windows.Forms.TabPage
	Public WithEvents SprdViewBH As AxFPSpreadADO.AxfpSpread
	Public WithEvents Frame2 As System.Windows.Forms.GroupBox
	Public WithEvents SprdViewFH As AxFPSpreadADO.AxfpSpread
	Public WithEvents Frame1 As System.Windows.Forms.GroupBox
	Public WithEvents _SSTab1_TabPage1 As System.Windows.Forms.TabPage
	Public WithEvents SprdViewCD As AxFPSpreadADO.AxfpSpread
	Public WithEvents Frame4 As System.Windows.Forms.GroupBox
	Public WithEvents _SSTab1_TabPage2 As System.Windows.Forms.TabPage
	Public WithEvents SprdViewDD As AxFPSpreadADO.AxfpSpread
	Public WithEvents Frame5 As System.Windows.Forms.GroupBox
	Public WithEvents _SSTab1_TabPage3 As System.Windows.Forms.TabPage
	Public WithEvents SSTab1 As System.Windows.Forms.TabControl
	Public WithEvents chkConsolidated As System.Windows.Forms.CheckBox
	Public WithEvents cmdCD As System.Windows.Forms.Button
	Public WithEvents CmdPreview As System.Windows.Forms.Button
	Public WithEvents cmdPrint As System.Windows.Forms.Button
	Public WithEvents cmdClose As System.Windows.Forms.Button
	Public WithEvents cmdShow As System.Windows.Forms.Button
	Public WithEvents Report1 As AxCrystal.AxCrystalReport
	Public WithEvents FraMovement As System.Windows.Forms.GroupBox
	Public WithEvents AData1 As VB6.ADODC
	Public WithEvents AData5A As VB6.ADODC
	Public WithEvents AData5B As VB6.ADODC
	Public WithEvents AData6A As VB6.ADODC
	Public WithEvents AData6B As VB6.ADODC
	Public WithEvents AData7A As VB6.ADODC
	Public WithEvents optForm As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmTDSeReturn))
		Me.components = New System.ComponentModel.Container()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
		Me.SSTab1 = New System.Windows.Forms.TabControl
		Me._SSTab1_TabPage0 = New System.Windows.Forms.TabPage
		Me.Frame3 = New System.Windows.Forms.GroupBox
		Me._optForm_1 = New System.Windows.Forms.RadioButton
		Me._optForm_0 = New System.Windows.Forms.RadioButton
		Me.TxtAccount = New System.Windows.Forms.TextBox
		Me.CmdSearch = New System.Windows.Forms.Button
		Me.txtFlat = New System.Windows.Forms.TextBox
		Me.txtBuilding = New System.Windows.Forms.TextBox
		Me.txtRoad = New System.Windows.Forms.TextBox
		Me.txtArea = New System.Windows.Forms.TextBox
		Me.txtTown = New System.Windows.Forms.TextBox
		Me.txtState = New System.Windows.Forms.TextBox
		Me.txtPinCode = New System.Windows.Forms.TextBox
		Me.txtDesg = New System.Windows.Forms.TextBox
		Me.txtPersonName = New System.Windows.Forms.TextBox
		Me.txtReturnPeriod = New System.Windows.Forms.TextBox
		Me.txtDeductorStatus = New System.Windows.Forms.TextBox
		Me.txtAddressChange = New System.Windows.Forms.TextBox
		Me.txtRundate = New System.Windows.Forms.TextBox
		Me.txtDateFrom = New System.Windows.Forms.MaskedTextBox
		Me.txtDateTo = New System.Windows.Forms.MaskedTextBox
		Me.Label17 = New System.Windows.Forms.Label
		Me.Label16 = New System.Windows.Forms.Label
		Me.Label15 = New System.Windows.Forms.Label
		Me.Label14 = New System.Windows.Forms.Label
		Me.Label13 = New System.Windows.Forms.Label
		Me.Label8 = New System.Windows.Forms.Label
		Me.Label9 = New System.Windows.Forms.Label
		Me.Label10 = New System.Windows.Forms.Label
		Me.Label11 = New System.Windows.Forms.Label
		Me.Label12 = New System.Windows.Forms.Label
		Me.Label7 = New System.Windows.Forms.Label
		Me.Label6 = New System.Windows.Forms.Label
		Me.Label5 = New System.Windows.Forms.Label
		Me.Label4 = New System.Windows.Forms.Label
		Me.Label3 = New System.Windows.Forms.Label
		Me.Label2 = New System.Windows.Forms.Label
		Me.Label1 = New System.Windows.Forms.Label
		Me.Lable11 = New System.Windows.Forms.Label
		Me._SSTab1_TabPage1 = New System.Windows.Forms.TabPage
		Me.Frame2 = New System.Windows.Forms.GroupBox
		Me.SprdViewBH = New AxFPSpreadADO.AxfpSpread
		Me.Frame1 = New System.Windows.Forms.GroupBox
		Me.SprdViewFH = New AxFPSpreadADO.AxfpSpread
		Me._SSTab1_TabPage2 = New System.Windows.Forms.TabPage
		Me.Frame4 = New System.Windows.Forms.GroupBox
		Me.SprdViewCD = New AxFPSpreadADO.AxfpSpread
		Me._SSTab1_TabPage3 = New System.Windows.Forms.TabPage
		Me.Frame5 = New System.Windows.Forms.GroupBox
		Me.SprdViewDD = New AxFPSpreadADO.AxfpSpread
		Me.FraMovement = New System.Windows.Forms.GroupBox
		Me.chkConsolidated = New System.Windows.Forms.CheckBox
		Me.cmdCD = New System.Windows.Forms.Button
		Me.CmdPreview = New System.Windows.Forms.Button
		Me.cmdPrint = New System.Windows.Forms.Button
		Me.cmdClose = New System.Windows.Forms.Button
		Me.cmdShow = New System.Windows.Forms.Button
		Me.Report1 = New AxCrystal.AxCrystalReport
		Me.AData1 = New VB6.ADODC
		Me.AData5A = New VB6.ADODC
		Me.AData5B = New VB6.ADODC
		Me.AData6A = New VB6.ADODC
		Me.AData6B = New VB6.ADODC
		Me.AData7A = New VB6.ADODC
		Me.optForm = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(components)
		Me.SSTab1.SuspendLayout()
		Me._SSTab1_TabPage0.SuspendLayout()
		Me.Frame3.SuspendLayout()
		Me._SSTab1_TabPage1.SuspendLayout()
		Me.Frame2.SuspendLayout()
		Me.Frame1.SuspendLayout()
		Me._SSTab1_TabPage2.SuspendLayout()
		Me.Frame4.SuspendLayout()
		Me._SSTab1_TabPage3.SuspendLayout()
		Me.Frame5.SuspendLayout()
		Me.FraMovement.SuspendLayout()
		Me.SuspendLayout()
		Me.ToolTip1.Active = True
		CType(Me.SprdViewBH, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.SprdViewFH, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.SprdViewCD, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.SprdViewDD, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.optForm, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
		Me.Text = "TDS e-Return"
		Me.ClientSize = New System.Drawing.Size(671, 392)
		Me.Location = New System.Drawing.Point(3, 22)
		Me.Icon = CType(resources.GetObject("frmTDSeReturn.Icon"), System.Drawing.Icon)
		Me.MaximizeBox = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds
		Me.MinimizeBox = False
		Me.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.SystemColors.Control
		Me.ControlBox = True
		Me.Enabled = True
		Me.KeyPreview = False
		Me.Cursor = System.Windows.Forms.Cursors.Default
		Me.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.ShowInTaskbar = True
		Me.HelpButton = False
		Me.WindowState = System.Windows.Forms.FormWindowState.Normal
		Me.Name = "frmTDSeReturn"
		Me.SSTab1.Size = New System.Drawing.Size(671, 349)
		Me.SSTab1.Location = New System.Drawing.Point(0, 0)
		Me.SSTab1.TabIndex = 13
		Me.SSTab1.ItemSize = New System.Drawing.Size(42, 18)
		Me.SSTab1.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.SSTab1.Name = "SSTab1"
		Me._SSTab1_TabPage0.Text = "Return "
		Me.Frame3.Size = New System.Drawing.Size(661, 321)
		Me.Frame3.Location = New System.Drawing.Point(4, 24)
		Me.Frame3.TabIndex = 25
		Me.Frame3.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame3.BackColor = System.Drawing.SystemColors.Control
		Me.Frame3.Enabled = True
		Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame3.Visible = True
		Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame3.Name = "Frame3"
		Me._optForm_1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._optForm_1.Text = "Form 27"
		Me._optForm_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me._optForm_1.Size = New System.Drawing.Size(69, 15)
		Me._optForm_1.Location = New System.Drawing.Point(108, 14)
		Me._optForm_1.TabIndex = 53
		Me._optForm_1.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._optForm_1.BackColor = System.Drawing.SystemColors.Control
		Me._optForm_1.CausesValidation = True
		Me._optForm_1.Enabled = True
		Me._optForm_1.ForeColor = System.Drawing.SystemColors.ControlText
		Me._optForm_1.Cursor = System.Windows.Forms.Cursors.Default
		Me._optForm_1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._optForm_1.Appearance = System.Windows.Forms.Appearance.Normal
		Me._optForm_1.TabStop = True
		Me._optForm_1.Checked = False
		Me._optForm_1.Visible = True
		Me._optForm_1.Name = "_optForm_1"
		Me._optForm_0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._optForm_0.Text = "Form 26"
		Me._optForm_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me._optForm_0.Size = New System.Drawing.Size(83, 17)
		Me._optForm_0.Location = New System.Drawing.Point(10, 14)
		Me._optForm_0.TabIndex = 52
		Me._optForm_0.Checked = True
		Me._optForm_0.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._optForm_0.BackColor = System.Drawing.SystemColors.Control
		Me._optForm_0.CausesValidation = True
		Me._optForm_0.Enabled = True
		Me._optForm_0.ForeColor = System.Drawing.SystemColors.ControlText
		Me._optForm_0.Cursor = System.Windows.Forms.Cursors.Default
		Me._optForm_0.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._optForm_0.Appearance = System.Windows.Forms.Appearance.Normal
		Me._optForm_0.TabStop = True
		Me._optForm_0.Visible = True
		Me._optForm_0.Name = "_optForm_0"
		Me.TxtAccount.AutoSize = False
		Me.TxtAccount.Size = New System.Drawing.Size(357, 19)
		Me.TxtAccount.Location = New System.Drawing.Point(184, 36)
		Me.TxtAccount.TabIndex = 46
		Me.ToolTip1.SetToolTip(Me.TxtAccount, "Press F1 For Help")
		Me.TxtAccount.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.TxtAccount.AcceptsReturn = True
		Me.TxtAccount.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.TxtAccount.BackColor = System.Drawing.SystemColors.Window
		Me.TxtAccount.CausesValidation = True
		Me.TxtAccount.Enabled = True
		Me.TxtAccount.ForeColor = System.Drawing.SystemColors.WindowText
		Me.TxtAccount.HideSelection = True
		Me.TxtAccount.ReadOnly = False
		Me.TxtAccount.Maxlength = 0
		Me.TxtAccount.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.TxtAccount.MultiLine = False
		Me.TxtAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.TxtAccount.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.TxtAccount.TabStop = True
		Me.TxtAccount.Visible = True
		Me.TxtAccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.TxtAccount.Name = "TxtAccount"
		Me.CmdSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.CmdSearch.BackColor = System.Drawing.SystemColors.Menu
		Me.CmdSearch.Size = New System.Drawing.Size(27, 19)
		Me.CmdSearch.Location = New System.Drawing.Point(542, 36)
		Me.CmdSearch.Image = CType(resources.GetObject("CmdSearch.Image"), System.Drawing.Image)
		Me.CmdSearch.TabIndex = 45
		Me.CmdSearch.TabStop = False
		Me.ToolTip1.SetToolTip(Me.CmdSearch, "Search")
		Me.CmdSearch.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CmdSearch.CausesValidation = True
		Me.CmdSearch.Enabled = True
		Me.CmdSearch.ForeColor = System.Drawing.SystemColors.ControlText
		Me.CmdSearch.Cursor = System.Windows.Forms.Cursors.Default
		Me.CmdSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CmdSearch.Name = "CmdSearch"
		Me.txtFlat.AutoSize = False
		Me.txtFlat.Size = New System.Drawing.Size(441, 19)
		Me.txtFlat.Location = New System.Drawing.Point(187, 164)
		Me.txtFlat.TabIndex = 6
		Me.txtFlat.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtFlat.AcceptsReturn = True
		Me.txtFlat.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtFlat.BackColor = System.Drawing.SystemColors.Window
		Me.txtFlat.CausesValidation = True
		Me.txtFlat.Enabled = True
		Me.txtFlat.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtFlat.HideSelection = True
		Me.txtFlat.ReadOnly = False
		Me.txtFlat.Maxlength = 0
		Me.txtFlat.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtFlat.MultiLine = False
		Me.txtFlat.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtFlat.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtFlat.TabStop = True
		Me.txtFlat.Visible = True
		Me.txtFlat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtFlat.Name = "txtFlat"
		Me.txtBuilding.AutoSize = False
		Me.txtBuilding.Size = New System.Drawing.Size(441, 19)
		Me.txtBuilding.Location = New System.Drawing.Point(187, 190)
		Me.txtBuilding.TabIndex = 7
		Me.txtBuilding.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtBuilding.AcceptsReturn = True
		Me.txtBuilding.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtBuilding.BackColor = System.Drawing.SystemColors.Window
		Me.txtBuilding.CausesValidation = True
		Me.txtBuilding.Enabled = True
		Me.txtBuilding.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtBuilding.HideSelection = True
		Me.txtBuilding.ReadOnly = False
		Me.txtBuilding.Maxlength = 0
		Me.txtBuilding.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtBuilding.MultiLine = False
		Me.txtBuilding.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtBuilding.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtBuilding.TabStop = True
		Me.txtBuilding.Visible = True
		Me.txtBuilding.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtBuilding.Name = "txtBuilding"
		Me.txtRoad.AutoSize = False
		Me.txtRoad.Size = New System.Drawing.Size(441, 19)
		Me.txtRoad.Location = New System.Drawing.Point(187, 216)
		Me.txtRoad.TabIndex = 8
		Me.txtRoad.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtRoad.AcceptsReturn = True
		Me.txtRoad.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtRoad.BackColor = System.Drawing.SystemColors.Window
		Me.txtRoad.CausesValidation = True
		Me.txtRoad.Enabled = True
		Me.txtRoad.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtRoad.HideSelection = True
		Me.txtRoad.ReadOnly = False
		Me.txtRoad.Maxlength = 0
		Me.txtRoad.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtRoad.MultiLine = False
		Me.txtRoad.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtRoad.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtRoad.TabStop = True
		Me.txtRoad.Visible = True
		Me.txtRoad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtRoad.Name = "txtRoad"
		Me.txtArea.AutoSize = False
		Me.txtArea.Size = New System.Drawing.Size(441, 19)
		Me.txtArea.Location = New System.Drawing.Point(187, 242)
		Me.txtArea.TabIndex = 9
		Me.txtArea.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtArea.AcceptsReturn = True
		Me.txtArea.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtArea.BackColor = System.Drawing.SystemColors.Window
		Me.txtArea.CausesValidation = True
		Me.txtArea.Enabled = True
		Me.txtArea.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtArea.HideSelection = True
		Me.txtArea.ReadOnly = False
		Me.txtArea.Maxlength = 0
		Me.txtArea.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtArea.MultiLine = False
		Me.txtArea.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtArea.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtArea.TabStop = True
		Me.txtArea.Visible = True
		Me.txtArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtArea.Name = "txtArea"
		Me.txtTown.AutoSize = False
		Me.txtTown.Size = New System.Drawing.Size(441, 19)
		Me.txtTown.Location = New System.Drawing.Point(187, 268)
		Me.txtTown.TabIndex = 10
		Me.txtTown.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtTown.AcceptsReturn = True
		Me.txtTown.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtTown.BackColor = System.Drawing.SystemColors.Window
		Me.txtTown.CausesValidation = True
		Me.txtTown.Enabled = True
		Me.txtTown.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtTown.HideSelection = True
		Me.txtTown.ReadOnly = False
		Me.txtTown.Maxlength = 0
		Me.txtTown.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtTown.MultiLine = False
		Me.txtTown.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtTown.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtTown.TabStop = True
		Me.txtTown.Visible = True
		Me.txtTown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtTown.Name = "txtTown"
		Me.txtState.AutoSize = False
		Me.txtState.Size = New System.Drawing.Size(179, 19)
		Me.txtState.Location = New System.Drawing.Point(187, 294)
		Me.txtState.TabIndex = 11
		Me.txtState.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtState.AcceptsReturn = True
		Me.txtState.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtState.BackColor = System.Drawing.SystemColors.Window
		Me.txtState.CausesValidation = True
		Me.txtState.Enabled = True
		Me.txtState.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtState.HideSelection = True
		Me.txtState.ReadOnly = False
		Me.txtState.Maxlength = 0
		Me.txtState.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtState.MultiLine = False
		Me.txtState.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtState.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtState.TabStop = True
		Me.txtState.Visible = True
		Me.txtState.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtState.Name = "txtState"
		Me.txtPinCode.AutoSize = False
		Me.txtPinCode.Size = New System.Drawing.Size(101, 19)
		Me.txtPinCode.Location = New System.Drawing.Point(527, 294)
		Me.txtPinCode.TabIndex = 12
		Me.txtPinCode.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtPinCode.AcceptsReturn = True
		Me.txtPinCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtPinCode.BackColor = System.Drawing.SystemColors.Window
		Me.txtPinCode.CausesValidation = True
		Me.txtPinCode.Enabled = True
		Me.txtPinCode.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtPinCode.HideSelection = True
		Me.txtPinCode.ReadOnly = False
		Me.txtPinCode.Maxlength = 0
		Me.txtPinCode.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtPinCode.MultiLine = False
		Me.txtPinCode.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtPinCode.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtPinCode.TabStop = True
		Me.txtPinCode.Visible = True
		Me.txtPinCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtPinCode.Name = "txtPinCode"
		Me.txtDesg.AutoSize = False
		Me.txtDesg.Size = New System.Drawing.Size(341, 19)
		Me.txtDesg.Location = New System.Drawing.Point(187, 138)
		Me.txtDesg.TabIndex = 5
		Me.ToolTip1.SetToolTip(Me.txtDesg, "Press F1 For Help")
		Me.txtDesg.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtDesg.AcceptsReturn = True
		Me.txtDesg.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtDesg.BackColor = System.Drawing.SystemColors.Window
		Me.txtDesg.CausesValidation = True
		Me.txtDesg.Enabled = True
		Me.txtDesg.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtDesg.HideSelection = True
		Me.txtDesg.ReadOnly = False
		Me.txtDesg.Maxlength = 0
		Me.txtDesg.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtDesg.MultiLine = False
		Me.txtDesg.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtDesg.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtDesg.TabStop = True
		Me.txtDesg.Visible = True
		Me.txtDesg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtDesg.Name = "txtDesg"
		Me.txtPersonName.AutoSize = False
		Me.txtPersonName.Size = New System.Drawing.Size(341, 19)
		Me.txtPersonName.Location = New System.Drawing.Point(187, 112)
		Me.txtPersonName.TabIndex = 4
		Me.ToolTip1.SetToolTip(Me.txtPersonName, "Press F1 For Help")
		Me.txtPersonName.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtPersonName.AcceptsReturn = True
		Me.txtPersonName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtPersonName.BackColor = System.Drawing.SystemColors.Window
		Me.txtPersonName.CausesValidation = True
		Me.txtPersonName.Enabled = True
		Me.txtPersonName.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtPersonName.HideSelection = True
		Me.txtPersonName.ReadOnly = False
		Me.txtPersonName.Maxlength = 0
		Me.txtPersonName.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtPersonName.MultiLine = False
		Me.txtPersonName.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtPersonName.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtPersonName.TabStop = True
		Me.txtPersonName.Visible = True
		Me.txtPersonName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtPersonName.Name = "txtPersonName"
		Me.txtReturnPeriod.AutoSize = False
		Me.txtReturnPeriod.Size = New System.Drawing.Size(45, 19)
		Me.txtReturnPeriod.Location = New System.Drawing.Point(552, 86)
		Me.txtReturnPeriod.TabIndex = 3
		Me.ToolTip1.SetToolTip(Me.txtReturnPeriod, "Press F1 For Help")
		Me.txtReturnPeriod.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtReturnPeriod.AcceptsReturn = True
		Me.txtReturnPeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtReturnPeriod.BackColor = System.Drawing.SystemColors.Window
		Me.txtReturnPeriod.CausesValidation = True
		Me.txtReturnPeriod.Enabled = True
		Me.txtReturnPeriod.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtReturnPeriod.HideSelection = True
		Me.txtReturnPeriod.ReadOnly = False
		Me.txtReturnPeriod.Maxlength = 0
		Me.txtReturnPeriod.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtReturnPeriod.MultiLine = False
		Me.txtReturnPeriod.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtReturnPeriod.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtReturnPeriod.TabStop = True
		Me.txtReturnPeriod.Visible = True
		Me.txtReturnPeriod.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtReturnPeriod.Name = "txtReturnPeriod"
		Me.txtDeductorStatus.AutoSize = False
		Me.txtDeductorStatus.Size = New System.Drawing.Size(43, 19)
		Me.txtDeductorStatus.Location = New System.Drawing.Point(187, 86)
		Me.txtDeductorStatus.TabIndex = 2
		Me.ToolTip1.SetToolTip(Me.txtDeductorStatus, "Press F1 For Help")
		Me.txtDeductorStatus.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtDeductorStatus.AcceptsReturn = True
		Me.txtDeductorStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtDeductorStatus.BackColor = System.Drawing.SystemColors.Window
		Me.txtDeductorStatus.CausesValidation = True
		Me.txtDeductorStatus.Enabled = True
		Me.txtDeductorStatus.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtDeductorStatus.HideSelection = True
		Me.txtDeductorStatus.ReadOnly = False
		Me.txtDeductorStatus.Maxlength = 0
		Me.txtDeductorStatus.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtDeductorStatus.MultiLine = False
		Me.txtDeductorStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtDeductorStatus.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtDeductorStatus.TabStop = True
		Me.txtDeductorStatus.Visible = True
		Me.txtDeductorStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtDeductorStatus.Name = "txtDeductorStatus"
		Me.txtAddressChange.AutoSize = False
		Me.txtAddressChange.Size = New System.Drawing.Size(43, 19)
		Me.txtAddressChange.Location = New System.Drawing.Point(552, 60)
		Me.txtAddressChange.TabIndex = 1
		Me.ToolTip1.SetToolTip(Me.txtAddressChange, "Press F1 For Help")
		Me.txtAddressChange.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtAddressChange.AcceptsReturn = True
		Me.txtAddressChange.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtAddressChange.BackColor = System.Drawing.SystemColors.Window
		Me.txtAddressChange.CausesValidation = True
		Me.txtAddressChange.Enabled = True
		Me.txtAddressChange.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtAddressChange.HideSelection = True
		Me.txtAddressChange.ReadOnly = False
		Me.txtAddressChange.Maxlength = 0
		Me.txtAddressChange.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtAddressChange.MultiLine = False
		Me.txtAddressChange.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtAddressChange.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtAddressChange.TabStop = True
		Me.txtAddressChange.Visible = True
		Me.txtAddressChange.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtAddressChange.Name = "txtAddressChange"
		Me.txtRundate.AutoSize = False
		Me.txtRundate.Size = New System.Drawing.Size(95, 19)
		Me.txtRundate.Location = New System.Drawing.Point(187, 60)
		Me.txtRundate.TabIndex = 0
		Me.ToolTip1.SetToolTip(Me.txtRundate, "Press F1 For Help")
		Me.txtRundate.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtRundate.AcceptsReturn = True
		Me.txtRundate.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtRundate.BackColor = System.Drawing.SystemColors.Window
		Me.txtRundate.CausesValidation = True
		Me.txtRundate.Enabled = True
		Me.txtRundate.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtRundate.HideSelection = True
		Me.txtRundate.ReadOnly = False
		Me.txtRundate.Maxlength = 0
		Me.txtRundate.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtRundate.MultiLine = False
		Me.txtRundate.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtRundate.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtRundate.TabStop = True
		Me.txtRundate.Visible = True
		Me.txtRundate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtRundate.Name = "txtRundate"
		Me.txtDateFrom.AllowPromptAsInput = False
		Me.txtDateFrom.Size = New System.Drawing.Size(83, 21)
		Me.txtDateFrom.Location = New System.Drawing.Point(233, 12)
		Me.txtDateFrom.TabIndex = 48
		Me.txtDateFrom.MaxLength = 10
		Me.txtDateFrom.Mask = "##/##/####"
		Me.txtDateFrom.PromptChar = "_"
		Me.txtDateFrom.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtDateFrom.Name = "txtDateFrom"
		Me.txtDateTo.AllowPromptAsInput = False
		Me.txtDateTo.Size = New System.Drawing.Size(83, 21)
		Me.txtDateTo.Location = New System.Drawing.Point(445, 12)
		Me.txtDateTo.TabIndex = 49
		Me.txtDateTo.MaxLength = 10
		Me.txtDateTo.Mask = "##/##/####"
		Me.txtDateTo.PromptChar = "_"
		Me.txtDateTo.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtDateTo.Name = "txtDateTo"
		Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.Label17.Text = "To : "
		Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label17.Size = New System.Drawing.Size(28, 13)
		Me.Label17.Location = New System.Drawing.Point(418, 16)
		Me.Label17.TabIndex = 51
		Me.Label17.BackColor = System.Drawing.SystemColors.Control
		Me.Label17.Enabled = True
		Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label17.UseMnemonic = True
		Me.Label17.Visible = True
		Me.Label17.AutoSize = True
		Me.Label17.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label17.Name = "Label17"
		Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.Label16.Text = "From : "
		Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label16.Size = New System.Drawing.Size(40, 13)
		Me.Label16.Location = New System.Drawing.Point(194, 16)
		Me.Label16.TabIndex = 50
		Me.Label16.BackColor = System.Drawing.SystemColors.Control
		Me.Label16.Enabled = True
		Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label16.UseMnemonic = True
		Me.Label16.Visible = True
		Me.Label16.AutoSize = True
		Me.Label16.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label16.Name = "Label16"
		Me.Label15.Text = "TDS Account Name :"
		Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label15.Size = New System.Drawing.Size(125, 13)
		Me.Label15.Location = New System.Drawing.Point(12, 36)
		Me.Label15.TabIndex = 47
		Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label15.BackColor = System.Drawing.SystemColors.Control
		Me.Label15.Enabled = True
		Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label15.UseMnemonic = True
		Me.Label15.Visible = True
		Me.Label15.AutoSize = True
		Me.Label15.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label15.Name = "Label15"
		Me.Label14.Text = "Flat / Door / Block No. :"
		Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label14.Size = New System.Drawing.Size(141, 13)
		Me.Label14.Location = New System.Drawing.Point(12, 168)
		Me.Label14.TabIndex = 42
		Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label14.BackColor = System.Drawing.SystemColors.Control
		Me.Label14.Enabled = True
		Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label14.UseMnemonic = True
		Me.Label14.Visible = True
		Me.Label14.AutoSize = True
		Me.Label14.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label14.Name = "Label14"
		Me.Label13.Text = "Name of Premises / Building :"
		Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label13.Size = New System.Drawing.Size(169, 13)
		Me.Label13.Location = New System.Drawing.Point(12, 192)
		Me.Label13.TabIndex = 41
		Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label13.BackColor = System.Drawing.SystemColors.Control
		Me.Label13.Enabled = True
		Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label13.UseMnemonic = True
		Me.Label13.Visible = True
		Me.Label13.AutoSize = True
		Me.Label13.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label13.Name = "Label13"
		Me.Label8.Text = "Road / Street / Lane :"
		Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label8.Size = New System.Drawing.Size(129, 13)
		Me.Label8.Location = New System.Drawing.Point(12, 218)
		Me.Label8.TabIndex = 40
		Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label8.BackColor = System.Drawing.SystemColors.Control
		Me.Label8.Enabled = True
		Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label8.UseMnemonic = True
		Me.Label8.Visible = True
		Me.Label8.AutoSize = True
		Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label8.Name = "Label8"
		Me.Label9.Text = "Area / Locality :"
		Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label9.Size = New System.Drawing.Size(93, 13)
		Me.Label9.Location = New System.Drawing.Point(12, 244)
		Me.Label9.TabIndex = 39
		Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label9.BackColor = System.Drawing.SystemColors.Control
		Me.Label9.Enabled = True
		Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label9.UseMnemonic = True
		Me.Label9.Visible = True
		Me.Label9.AutoSize = True
		Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label9.Name = "Label9"
		Me.Label10.Text = "Town / City / District :"
		Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label10.Size = New System.Drawing.Size(129, 13)
		Me.Label10.Location = New System.Drawing.Point(12, 270)
		Me.Label10.TabIndex = 38
		Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label10.BackColor = System.Drawing.SystemColors.Control
		Me.Label10.Enabled = True
		Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label10.UseMnemonic = True
		Me.Label10.Visible = True
		Me.Label10.AutoSize = True
		Me.Label10.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label10.Name = "Label10"
		Me.Label11.Text = "State :"
		Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label11.Size = New System.Drawing.Size(39, 13)
		Me.Label11.Location = New System.Drawing.Point(12, 296)
		Me.Label11.TabIndex = 37
		Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label11.BackColor = System.Drawing.SystemColors.Control
		Me.Label11.Enabled = True
		Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label11.UseMnemonic = True
		Me.Label11.Visible = True
		Me.Label11.AutoSize = True
		Me.Label11.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label11.Name = "Label11"
		Me.Label12.Text = "Pin Code :"
		Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label12.Size = New System.Drawing.Size(60, 13)
		Me.Label12.Location = New System.Drawing.Point(458, 296)
		Me.Label12.TabIndex = 36
		Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label12.BackColor = System.Drawing.SystemColors.Control
		Me.Label12.Enabled = True
		Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label12.UseMnemonic = True
		Me.Label12.Visible = True
		Me.Label12.AutoSize = True
		Me.Label12.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label12.Name = "Label12"
		Me.Label7.Text = "(C/O)"
		Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label7.Size = New System.Drawing.Size(32, 13)
		Me.Label7.Location = New System.Drawing.Point(234, 88)
		Me.Label7.TabIndex = 35
		Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label7.BackColor = System.Drawing.SystemColors.Control
		Me.Label7.Enabled = True
		Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label7.UseMnemonic = True
		Me.Label7.Visible = True
		Me.Label7.AutoSize = True
		Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label7.Name = "Label7"
		Me.Label6.Text = "(Y/N)"
		Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label6.Size = New System.Drawing.Size(32, 13)
		Me.Label6.Location = New System.Drawing.Point(604, 62)
		Me.Label6.TabIndex = 34
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
		Me.Label5.Text = "Designation of the Person Responsible for Deduction :"
		Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label5.Size = New System.Drawing.Size(168, 29)
		Me.Label5.Location = New System.Drawing.Point(12, 138)
		Me.Label5.TabIndex = 31
		Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label5.BackColor = System.Drawing.SystemColors.Control
		Me.Label5.Enabled = True
		Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label5.UseMnemonic = True
		Me.Label5.Visible = True
		Me.Label5.AutoSize = False
		Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label5.Name = "Label5"
		Me.Label4.Text = "Name of the Person Responsible for Deduction :"
		Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label4.Size = New System.Drawing.Size(161, 29)
		Me.Label4.Location = New System.Drawing.Point(12, 112)
		Me.Label4.TabIndex = 30
		Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label4.BackColor = System.Drawing.SystemColors.Control
		Me.Label4.Enabled = True
		Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label4.UseMnemonic = True
		Me.Label4.Visible = True
		Me.Label4.AutoSize = False
		Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label4.Name = "Label4"
		Me.Label3.Text = "Quarterly/Yearly Return :"
		Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label3.Size = New System.Drawing.Size(143, 13)
		Me.Label3.Location = New System.Drawing.Point(374, 86)
		Me.Label3.TabIndex = 29
		Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label3.BackColor = System.Drawing.SystemColors.Control
		Me.Label3.Enabled = True
		Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label3.UseMnemonic = True
		Me.Label3.Visible = True
		Me.Label3.AutoSize = True
		Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label3.Name = "Label3"
		Me.Label2.Text = "Status of Deductor :"
		Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label2.Size = New System.Drawing.Size(116, 13)
		Me.Label2.Location = New System.Drawing.Point(12, 86)
		Me.Label2.TabIndex = 28
		Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label2.BackColor = System.Drawing.SystemColors.Control
		Me.Label2.Enabled = True
		Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label2.UseMnemonic = True
		Me.Label2.Visible = True
		Me.Label2.AutoSize = True
		Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label2.Name = "Label2"
		Me.Label1.Text = "Change of Addres of TAN since last Return :"
		Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.Size = New System.Drawing.Size(164, 29)
		Me.Label1.Location = New System.Drawing.Point(374, 60)
		Me.Label1.TabIndex = 27
		Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label1.BackColor = System.Drawing.SystemColors.Control
		Me.Label1.Enabled = True
		Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label1.UseMnemonic = True
		Me.Label1.Visible = True
		Me.Label1.AutoSize = False
		Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label1.Name = "Label1"
		Me.Lable11.Text = "File Creation Date :"
		Me.Lable11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Lable11.Size = New System.Drawing.Size(111, 13)
		Me.Lable11.Location = New System.Drawing.Point(12, 60)
		Me.Lable11.TabIndex = 26
		Me.Lable11.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Lable11.BackColor = System.Drawing.SystemColors.Control
		Me.Lable11.Enabled = True
		Me.Lable11.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Lable11.Cursor = System.Windows.Forms.Cursors.Default
		Me.Lable11.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Lable11.UseMnemonic = True
		Me.Lable11.Visible = True
		Me.Lable11.AutoSize = True
		Me.Lable11.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Lable11.Name = "Lable11"
		Me._SSTab1_TabPage1.Text = "File / Batch Header Record"
		Me.Frame2.Size = New System.Drawing.Size(661, 173)
		Me.Frame2.Location = New System.Drawing.Point(6, 170)
		Me.Frame2.TabIndex = 16
		Me.Frame2.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame2.BackColor = System.Drawing.SystemColors.Control
		Me.Frame2.Enabled = True
		Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame2.Visible = True
		Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame2.Name = "Frame2"
		SprdViewBH.OcxState = CType(resources.GetObject("SprdViewBH.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SprdViewBH.Size = New System.Drawing.Size(656, 159)
		Me.SprdViewBH.Location = New System.Drawing.Point(2, 10)
		Me.SprdViewBH.TabIndex = 22
		Me.SprdViewBH.Name = "SprdViewBH"
		Me.Frame1.Size = New System.Drawing.Size(661, 151)
		Me.Frame1.Location = New System.Drawing.Point(6, 22)
		Me.Frame1.TabIndex = 32
		Me.Frame1.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame1.BackColor = System.Drawing.SystemColors.Control
		Me.Frame1.Enabled = True
		Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame1.Visible = True
		Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame1.Name = "Frame1"
		SprdViewFH.OcxState = CType(resources.GetObject("SprdViewFH.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SprdViewFH.Size = New System.Drawing.Size(656, 139)
		Me.SprdViewFH.Location = New System.Drawing.Point(2, 8)
		Me.SprdViewFH.TabIndex = 33
		Me.SprdViewFH.Name = "SprdViewFH"
		Me._SSTab1_TabPage2.Text = "Challan Detail Record"
		Me.Frame4.Size = New System.Drawing.Size(661, 321)
		Me.Frame4.Location = New System.Drawing.Point(4, 24)
		Me.Frame4.TabIndex = 15
		Me.Frame4.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame4.BackColor = System.Drawing.SystemColors.Control
		Me.Frame4.Enabled = True
		Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame4.Visible = True
		Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame4.Name = "Frame4"
		SprdViewCD.OcxState = CType(resources.GetObject("SprdViewCD.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SprdViewCD.Size = New System.Drawing.Size(656, 307)
		Me.SprdViewCD.Location = New System.Drawing.Point(2, 10)
		Me.SprdViewCD.TabIndex = 23
		Me.SprdViewCD.Name = "SprdViewCD"
		Me._SSTab1_TabPage3.Text = "Deductee Detail Record"
		Me.Frame5.Size = New System.Drawing.Size(661, 321)
		Me.Frame5.Location = New System.Drawing.Point(4, 24)
		Me.Frame5.TabIndex = 14
		Me.Frame5.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame5.BackColor = System.Drawing.SystemColors.Control
		Me.Frame5.Enabled = True
		Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame5.Visible = True
		Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame5.Name = "Frame5"
		SprdViewDD.OcxState = CType(resources.GetObject("SprdViewDD.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SprdViewDD.Size = New System.Drawing.Size(656, 307)
		Me.SprdViewDD.Location = New System.Drawing.Point(2, 10)
		Me.SprdViewDD.TabIndex = 24
		Me.SprdViewDD.Name = "SprdViewDD"
		Me.FraMovement.Size = New System.Drawing.Size(671, 49)
		Me.FraMovement.Location = New System.Drawing.Point(0, 342)
		Me.FraMovement.TabIndex = 17
		Me.FraMovement.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
		Me.FraMovement.Enabled = True
		Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
		Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.FraMovement.Visible = True
		Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
		Me.FraMovement.Name = "FraMovement"
		Me.chkConsolidated.Text = "Consolidated"
		Me.chkConsolidated.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.chkConsolidated.Size = New System.Drawing.Size(177, 19)
		Me.chkConsolidated.Location = New System.Drawing.Point(12, 20)
		Me.chkConsolidated.TabIndex = 44
		Me.chkConsolidated.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.chkConsolidated.FlatStyle = System.Windows.Forms.FlatStyle.Standard
		Me.chkConsolidated.BackColor = System.Drawing.SystemColors.Control
		Me.chkConsolidated.CausesValidation = True
		Me.chkConsolidated.Enabled = True
		Me.chkConsolidated.ForeColor = System.Drawing.SystemColors.ControlText
		Me.chkConsolidated.Cursor = System.Windows.Forms.Cursors.Default
		Me.chkConsolidated.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.chkConsolidated.Appearance = System.Windows.Forms.Appearance.Normal
		Me.chkConsolidated.TabStop = True
		Me.chkConsolidated.CheckState = System.Windows.Forms.CheckState.Unchecked
		Me.chkConsolidated.Visible = True
		Me.chkConsolidated.Name = "chkConsolidated"
		Me.cmdCD.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.cmdCD.Text = "Create CD"
		Me.cmdCD.Size = New System.Drawing.Size(67, 37)
		Me.cmdCD.Location = New System.Drawing.Point(400, 9)
		Me.cmdCD.Image = CType(resources.GetObject("cmdCD.Image"), System.Drawing.Image)
		Me.cmdCD.TabIndex = 43
		Me.ToolTip1.SetToolTip(Me.cmdCD, "Show Record")
		Me.cmdCD.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdCD.BackColor = System.Drawing.SystemColors.Control
		Me.cmdCD.CausesValidation = True
		Me.cmdCD.Enabled = True
		Me.cmdCD.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdCD.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdCD.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdCD.TabStop = True
		Me.cmdCD.Name = "cmdCD"
		Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.CmdPreview.Text = "Pre&view"
		Me.CmdPreview.Enabled = False
		Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
		Me.CmdPreview.Location = New System.Drawing.Point(533, 9)
		Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
		Me.CmdPreview.TabIndex = 20
		Me.ToolTip1.SetToolTip(Me.CmdPreview, "Print Preview")
		Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
		Me.CmdPreview.CausesValidation = True
		Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
		Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
		Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CmdPreview.TabStop = True
		Me.CmdPreview.Name = "CmdPreview"
		Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.cmdPrint.Text = "&Print"
		Me.cmdPrint.Enabled = False
		Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
		Me.cmdPrint.Location = New System.Drawing.Point(467, 9)
		Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
		Me.cmdPrint.TabIndex = 19
		Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print List")
		Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
		Me.cmdPrint.CausesValidation = True
		Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdPrint.TabStop = True
		Me.cmdPrint.Name = "cmdPrint"
		Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.cmdClose.Text = "&Close"
		Me.cmdClose.Size = New System.Drawing.Size(67, 37)
		Me.cmdClose.Location = New System.Drawing.Point(600, 9)
		Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
		Me.cmdClose.TabIndex = 21
		Me.ToolTip1.SetToolTip(Me.cmdClose, "Close the Form")
		Me.cmdClose.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
		Me.cmdClose.CausesValidation = True
		Me.cmdClose.Enabled = True
		Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdClose.TabStop = True
		Me.cmdClose.Name = "cmdClose"
		Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.cmdShow.Text = "Sho&w"
		Me.cmdShow.Size = New System.Drawing.Size(67, 37)
		Me.cmdShow.Location = New System.Drawing.Point(332, 9)
		Me.cmdShow.Image = CType(resources.GetObject("cmdShow.Image"), System.Drawing.Image)
		Me.cmdShow.TabIndex = 18
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
		Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
		Me.Report1.Location = New System.Drawing.Point(280, 10)
		Me.Report1.Name = "Report1"
		Me.AData1.Size = New System.Drawing.Size(113, 23)
		Me.AData1.Location = New System.Drawing.Point(0, 0)
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
		Me.AData5A.Size = New System.Drawing.Size(113, 23)
		Me.AData5A.Location = New System.Drawing.Point(100, 0)
		Me.AData5A.Visible = 0
		Me.AData5A.CursorLocation = ADODB.CursorLocationEnum.adUseClient
		Me.AData5A.ConnectionTimeout = 15
		Me.AData5A.CommandTimeout = 30
		Me.AData5A.CursorType = ADODB.CursorTypeEnum.adOpenStatic
		Me.AData5A.LockType = ADODB.LockTypeEnum.adLockOptimistic
		Me.AData5A.CommandType = ADODB.CommandTypeEnum.adCmdUnknown
		Me.AData5A.CacheSize = 50
		Me.AData5A.MaxRecords = 0
		Me.AData5A.BOFAction = Microsoft.VisualBasic.Compatibility.VB6.ADODC.BOFActionEnum.adDoMoveFirst
		Me.AData5A.EOFAction = Microsoft.VisualBasic.Compatibility.VB6.ADODC.EOFActionEnum.adDoMoveLast
		Me.AData5A.BackColor = System.Drawing.SystemColors.Window
		Me.AData5A.ForeColor = System.Drawing.SystemColors.WindowText
		Me.AData5A.Orientation = Microsoft.VisualBasic.Compatibility.VB6.ADODC.OrientationEnum.adHorizontal
		Me.AData5A.Enabled = True
		Me.AData5A.UserName = ""
		Me.AData5A.RecordSource = ""
		Me.AData5A.Text = "Adodc1"
		Me.AData5A.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.AData5A.ConnectionString = ""
		Me.AData5A.Name = "AData5A"
		Me.AData5B.Size = New System.Drawing.Size(113, 23)
		Me.AData5B.Location = New System.Drawing.Point(178, 0)
		Me.AData5B.Visible = 0
		Me.AData5B.CursorLocation = ADODB.CursorLocationEnum.adUseClient
		Me.AData5B.ConnectionTimeout = 15
		Me.AData5B.CommandTimeout = 30
		Me.AData5B.CursorType = ADODB.CursorTypeEnum.adOpenStatic
		Me.AData5B.LockType = ADODB.LockTypeEnum.adLockOptimistic
		Me.AData5B.CommandType = ADODB.CommandTypeEnum.adCmdUnknown
		Me.AData5B.CacheSize = 50
		Me.AData5B.MaxRecords = 0
		Me.AData5B.BOFAction = Microsoft.VisualBasic.Compatibility.VB6.ADODC.BOFActionEnum.adDoMoveFirst
		Me.AData5B.EOFAction = Microsoft.VisualBasic.Compatibility.VB6.ADODC.EOFActionEnum.adDoMoveLast
		Me.AData5B.BackColor = System.Drawing.SystemColors.Window
		Me.AData5B.ForeColor = System.Drawing.SystemColors.WindowText
		Me.AData5B.Orientation = Microsoft.VisualBasic.Compatibility.VB6.ADODC.OrientationEnum.adHorizontal
		Me.AData5B.Enabled = True
		Me.AData5B.UserName = ""
		Me.AData5B.RecordSource = ""
		Me.AData5B.Text = "Adodc1"
		Me.AData5B.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.AData5B.ConnectionString = ""
		Me.AData5B.Name = "AData5B"
		Me.AData6A.Size = New System.Drawing.Size(113, 23)
		Me.AData6A.Location = New System.Drawing.Point(248, 0)
		Me.AData6A.Visible = 0
		Me.AData6A.CursorLocation = ADODB.CursorLocationEnum.adUseClient
		Me.AData6A.ConnectionTimeout = 15
		Me.AData6A.CommandTimeout = 30
		Me.AData6A.CursorType = ADODB.CursorTypeEnum.adOpenStatic
		Me.AData6A.LockType = ADODB.LockTypeEnum.adLockOptimistic
		Me.AData6A.CommandType = ADODB.CommandTypeEnum.adCmdUnknown
		Me.AData6A.CacheSize = 50
		Me.AData6A.MaxRecords = 0
		Me.AData6A.BOFAction = Microsoft.VisualBasic.Compatibility.VB6.ADODC.BOFActionEnum.adDoMoveFirst
		Me.AData6A.EOFAction = Microsoft.VisualBasic.Compatibility.VB6.ADODC.EOFActionEnum.adDoMoveLast
		Me.AData6A.BackColor = System.Drawing.SystemColors.Window
		Me.AData6A.ForeColor = System.Drawing.SystemColors.WindowText
		Me.AData6A.Orientation = Microsoft.VisualBasic.Compatibility.VB6.ADODC.OrientationEnum.adHorizontal
		Me.AData6A.Enabled = True
		Me.AData6A.UserName = ""
		Me.AData6A.RecordSource = ""
		Me.AData6A.Text = "Adodc1"
		Me.AData6A.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.AData6A.ConnectionString = ""
		Me.AData6A.Name = "AData6A"
		Me.AData6B.Size = New System.Drawing.Size(113, 23)
		Me.AData6B.Location = New System.Drawing.Point(346, 0)
		Me.AData6B.Visible = 0
		Me.AData6B.CursorLocation = ADODB.CursorLocationEnum.adUseClient
		Me.AData6B.ConnectionTimeout = 15
		Me.AData6B.CommandTimeout = 30
		Me.AData6B.CursorType = ADODB.CursorTypeEnum.adOpenStatic
		Me.AData6B.LockType = ADODB.LockTypeEnum.adLockOptimistic
		Me.AData6B.CommandType = ADODB.CommandTypeEnum.adCmdUnknown
		Me.AData6B.CacheSize = 50
		Me.AData6B.MaxRecords = 0
		Me.AData6B.BOFAction = Microsoft.VisualBasic.Compatibility.VB6.ADODC.BOFActionEnum.adDoMoveFirst
		Me.AData6B.EOFAction = Microsoft.VisualBasic.Compatibility.VB6.ADODC.EOFActionEnum.adDoMoveLast
		Me.AData6B.BackColor = System.Drawing.SystemColors.Window
		Me.AData6B.ForeColor = System.Drawing.SystemColors.WindowText
		Me.AData6B.Orientation = Microsoft.VisualBasic.Compatibility.VB6.ADODC.OrientationEnum.adHorizontal
		Me.AData6B.Enabled = True
		Me.AData6B.UserName = ""
		Me.AData6B.RecordSource = ""
		Me.AData6B.Text = "Adodc1"
		Me.AData6B.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.AData6B.ConnectionString = ""
		Me.AData6B.Name = "AData6B"
		Me.AData7A.Size = New System.Drawing.Size(113, 23)
		Me.AData7A.Location = New System.Drawing.Point(0, 0)
		Me.AData7A.Visible = 0
		Me.AData7A.CursorLocation = ADODB.CursorLocationEnum.adUseClient
		Me.AData7A.ConnectionTimeout = 15
		Me.AData7A.CommandTimeout = 30
		Me.AData7A.CursorType = ADODB.CursorTypeEnum.adOpenStatic
		Me.AData7A.LockType = ADODB.LockTypeEnum.adLockOptimistic
		Me.AData7A.CommandType = ADODB.CommandTypeEnum.adCmdUnknown
		Me.AData7A.CacheSize = 50
		Me.AData7A.MaxRecords = 0
		Me.AData7A.BOFAction = Microsoft.VisualBasic.Compatibility.VB6.ADODC.BOFActionEnum.adDoMoveFirst
		Me.AData7A.EOFAction = Microsoft.VisualBasic.Compatibility.VB6.ADODC.EOFActionEnum.adDoMoveLast
		Me.AData7A.BackColor = System.Drawing.SystemColors.Window
		Me.AData7A.ForeColor = System.Drawing.SystemColors.WindowText
		Me.AData7A.Orientation = Microsoft.VisualBasic.Compatibility.VB6.ADODC.OrientationEnum.adHorizontal
		Me.AData7A.Enabled = True
		Me.AData7A.UserName = ""
		Me.AData7A.RecordSource = ""
		Me.AData7A.Text = "Adodc1"
		Me.AData7A.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.AData7A.ConnectionString = ""
		Me.AData7A.Name = "AData7A"
		Me.optForm.SetIndex(_optForm_1, CType(1, Short))
		Me.optForm.SetIndex(_optForm_0, CType(0, Short))
		CType(Me.optForm, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SprdViewDD, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SprdViewCD, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SprdViewFH, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SprdViewBH, System.ComponentModel.ISupportInitialize).EndInit()
		Me.Controls.Add(SSTab1)
		Me.Controls.Add(FraMovement)
		Me.Controls.Add(AData1)
		Me.Controls.Add(AData5A)
		Me.Controls.Add(AData5B)
		Me.Controls.Add(AData6A)
		Me.Controls.Add(AData6B)
		Me.Controls.Add(AData7A)
		Me.SSTab1.Controls.Add(_SSTab1_TabPage0)
		Me.SSTab1.Controls.Add(_SSTab1_TabPage1)
		Me.SSTab1.Controls.Add(_SSTab1_TabPage2)
		Me.SSTab1.Controls.Add(_SSTab1_TabPage3)
		Me._SSTab1_TabPage0.Controls.Add(Frame3)
		Me.Frame3.Controls.Add(_optForm_1)
		Me.Frame3.Controls.Add(_optForm_0)
		Me.Frame3.Controls.Add(TxtAccount)
		Me.Frame3.Controls.Add(CmdSearch)
		Me.Frame3.Controls.Add(txtFlat)
		Me.Frame3.Controls.Add(txtBuilding)
		Me.Frame3.Controls.Add(txtRoad)
		Me.Frame3.Controls.Add(txtArea)
		Me.Frame3.Controls.Add(txtTown)
		Me.Frame3.Controls.Add(txtState)
		Me.Frame3.Controls.Add(txtPinCode)
		Me.Frame3.Controls.Add(txtDesg)
		Me.Frame3.Controls.Add(txtPersonName)
		Me.Frame3.Controls.Add(txtReturnPeriod)
		Me.Frame3.Controls.Add(txtDeductorStatus)
		Me.Frame3.Controls.Add(txtAddressChange)
		Me.Frame3.Controls.Add(txtRundate)
		Me.Frame3.Controls.Add(txtDateFrom)
		Me.Frame3.Controls.Add(txtDateTo)
		Me.Frame3.Controls.Add(Label17)
		Me.Frame3.Controls.Add(Label16)
		Me.Frame3.Controls.Add(Label15)
		Me.Frame3.Controls.Add(Label14)
		Me.Frame3.Controls.Add(Label13)
		Me.Frame3.Controls.Add(Label8)
		Me.Frame3.Controls.Add(Label9)
		Me.Frame3.Controls.Add(Label10)
		Me.Frame3.Controls.Add(Label11)
		Me.Frame3.Controls.Add(Label12)
		Me.Frame3.Controls.Add(Label7)
		Me.Frame3.Controls.Add(Label6)
		Me.Frame3.Controls.Add(Label5)
		Me.Frame3.Controls.Add(Label4)
		Me.Frame3.Controls.Add(Label3)
		Me.Frame3.Controls.Add(Label2)
		Me.Frame3.Controls.Add(Label1)
		Me.Frame3.Controls.Add(Lable11)
		Me._SSTab1_TabPage1.Controls.Add(Frame2)
		Me._SSTab1_TabPage1.Controls.Add(Frame1)
		Me.Frame2.Controls.Add(SprdViewBH)
		Me.Frame1.Controls.Add(SprdViewFH)
		Me._SSTab1_TabPage2.Controls.Add(Frame4)
		Me.Frame4.Controls.Add(SprdViewCD)
		Me._SSTab1_TabPage3.Controls.Add(Frame5)
		Me.Frame5.Controls.Add(SprdViewDD)
		Me.FraMovement.Controls.Add(chkConsolidated)
		Me.FraMovement.Controls.Add(cmdCD)
		Me.FraMovement.Controls.Add(CmdPreview)
		Me.FraMovement.Controls.Add(cmdPrint)
		Me.FraMovement.Controls.Add(cmdClose)
		Me.FraMovement.Controls.Add(cmdShow)
		Me.FraMovement.Controls.Add(Report1)
		Me.SSTab1.ResumeLayout(False)
		Me._SSTab1_TabPage0.ResumeLayout(False)
		Me.Frame3.ResumeLayout(False)
		Me._SSTab1_TabPage1.ResumeLayout(False)
		Me.Frame2.ResumeLayout(False)
		Me.Frame1.ResumeLayout(False)
		Me._SSTab1_TabPage2.ResumeLayout(False)
		Me.Frame4.ResumeLayout(False)
		Me._SSTab1_TabPage3.ResumeLayout(False)
		Me.Frame5.ResumeLayout(False)
		Me.FraMovement.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()
	End Sub
#End Region 
#Region "Upgrade Support"
	Public Sub VB6_AddADODataBinding()
		SprdViewBH.DataSource = CType(AData5A, MSDATASRC.DataSource)
		SprdViewFH.DataSource = CType(AData5A, MSDATASRC.DataSource)
		SprdViewCD.DataSource = CType(AData5A, MSDATASRC.DataSource)
		SprdViewDD.DataSource = CType(AData5A, MSDATASRC.DataSource)
	End Sub
	Public Sub VB6_RemoveADODataBinding()
		SprdViewBH.DataSource = Nothing
		SprdViewFH.DataSource = Nothing
		SprdViewCD.DataSource = Nothing
		SprdViewDD.DataSource = Nothing
	End Sub
#End Region 
End Class