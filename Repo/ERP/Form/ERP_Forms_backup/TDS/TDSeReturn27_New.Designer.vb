<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmTDSeReturn27_New
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
	Public WithEvents CmdSearch As System.Windows.Forms.Button
	Public WithEvents TxtAccount As System.Windows.Forms.TextBox
	Public WithEvents txtDateFrom As System.Windows.Forms.MaskedTextBox
	Public WithEvents txtDateTo As System.Windows.Forms.MaskedTextBox
	Public WithEvents Label15 As System.Windows.Forms.Label
	Public WithEvents Label16 As System.Windows.Forms.Label
	Public WithEvents Label17 As System.Windows.Forms.Label
	Public WithEvents Frame2 As System.Windows.Forms.GroupBox
	Public WithEvents txtPanNo As System.Windows.Forms.TextBox
	Public WithEvents txtTDSAcNo As System.Windows.Forms.TextBox
	Public WithEvents txtFlat As System.Windows.Forms.TextBox
	Public WithEvents txtBuilding As System.Windows.Forms.TextBox
	Public WithEvents txtRoad As System.Windows.Forms.TextBox
	Public WithEvents txtArea As System.Windows.Forms.TextBox
	Public WithEvents txtTown As System.Windows.Forms.TextBox
	Public WithEvents txtState As System.Windows.Forms.TextBox
	Public WithEvents txtPinCode As System.Windows.Forms.TextBox
	Public WithEvents txtDesg As System.Windows.Forms.TextBox
	Public WithEvents txtPersonName As System.Windows.Forms.TextBox
	Public WithEvents Label20 As System.Windows.Forms.Label
	Public WithEvents Label19 As System.Windows.Forms.Label
	Public WithEvents Label18 As System.Windows.Forms.Label
	Public WithEvents Label14 As System.Windows.Forms.Label
	Public WithEvents Label13 As System.Windows.Forms.Label
	Public WithEvents Label8 As System.Windows.Forms.Label
	Public WithEvents Label9 As System.Windows.Forms.Label
	Public WithEvents Label10 As System.Windows.Forms.Label
	Public WithEvents Label11 As System.Windows.Forms.Label
	Public WithEvents Label12 As System.Windows.Forms.Label
	Public WithEvents Label5 As System.Windows.Forms.Label
	Public WithEvents Label4 As System.Windows.Forms.Label
	Public WithEvents Frame3 As System.Windows.Forms.GroupBox
	Public WithEvents _SSTab1_TabPage0 As System.Windows.Forms.TabPage
	Public WithEvents txtReturnPeriod As System.Windows.Forms.TextBox
	Public WithEvents txtDeductorStatus As System.Windows.Forms.TextBox
	Public WithEvents txtAddressChange As System.Windows.Forms.TextBox
	Public WithEvents txtRundate As System.Windows.Forms.TextBox
	Public WithEvents Label7 As System.Windows.Forms.Label
	Public WithEvents Label6 As System.Windows.Forms.Label
	Public WithEvents Label3 As System.Windows.Forms.Label
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents Lable11 As System.Windows.Forms.Label
	Public WithEvents Frame6 As System.Windows.Forms.GroupBox
	Public WithEvents chk194C As System.Windows.Forms.CheckBox
	Public WithEvents chk194BB As System.Windows.Forms.CheckBox
	Public WithEvents chk194B As System.Windows.Forms.CheckBox
	Public WithEvents chk194A As System.Windows.Forms.CheckBox
	Public WithEvents chk194 As System.Windows.Forms.CheckBox
	Public WithEvents chk193 As System.Windows.Forms.CheckBox
	Public WithEvents _optStatus_0 As System.Windows.Forms.RadioButton
	Public WithEvents _optStatus_1 As System.Windows.Forms.RadioButton
	Public WithEvents Frame9 As System.Windows.Forms.GroupBox
	Public WithEvents _optCentralGovt_1 As System.Windows.Forms.RadioButton
	Public WithEvents _optCentralGovt_0 As System.Windows.Forms.RadioButton
	Public WithEvents Frame8 As System.Windows.Forms.GroupBox
	Public WithEvents Label23 As System.Windows.Forms.Label
	Public WithEvents Label22 As System.Windows.Forms.Label
	Public WithEvents Label21 As System.Windows.Forms.Label
	Public WithEvents Frame7 As System.Windows.Forms.GroupBox
	Public WithEvents _SSTab1_TabPage1 As System.Windows.Forms.TabPage
	Public WithEvents SprdView26 As AxFPSpreadADO.AxfpSpread
	Public WithEvents Frame10 As System.Windows.Forms.GroupBox
	Public WithEvents Label24 As System.Windows.Forms.Label
	Public WithEvents _SSTab1_TabPage2 As System.Windows.Forms.TabPage
	Public WithEvents SprdViewAnnex As AxFPSpreadADO.AxfpSpread
	Public WithEvents Frame1 As System.Windows.Forms.GroupBox
	Public WithEvents _SSTab1_TabPage3 As System.Windows.Forms.TabPage
	Public WithEvents SSTab1 As System.Windows.Forms.TabControl
	Public WithEvents cmdValidate As System.Windows.Forms.Button
	Public WithEvents chkConsolidated As System.Windows.Forms.CheckBox
	Public WithEvents cmdCD As System.Windows.Forms.Button
	Public WithEvents CmdPreview As System.Windows.Forms.Button
	Public WithEvents cmdPrint As System.Windows.Forms.Button
	Public WithEvents cmdClose As System.Windows.Forms.Button
	Public WithEvents cmdShow As System.Windows.Forms.Button
	Public WithEvents Report1 As AxCrystal.AxCrystalReport
	Public WithEvents FraMovement As System.Windows.Forms.GroupBox
	Public WithEvents optCentralGovt As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
	Public WithEvents optStatus As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmTDSeReturn27_New))
		Me.components = New System.ComponentModel.Container()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
		Me.SSTab1 = New System.Windows.Forms.TabControl
		Me._SSTab1_TabPage0 = New System.Windows.Forms.TabPage
		Me.Frame2 = New System.Windows.Forms.GroupBox
		Me.CmdSearch = New System.Windows.Forms.Button
		Me.TxtAccount = New System.Windows.Forms.TextBox
		Me.txtDateFrom = New System.Windows.Forms.MaskedTextBox
		Me.txtDateTo = New System.Windows.Forms.MaskedTextBox
		Me.Label15 = New System.Windows.Forms.Label
		Me.Label16 = New System.Windows.Forms.Label
		Me.Label17 = New System.Windows.Forms.Label
		Me.Frame3 = New System.Windows.Forms.GroupBox
		Me.txtPanNo = New System.Windows.Forms.TextBox
		Me.txtTDSAcNo = New System.Windows.Forms.TextBox
		Me.txtFlat = New System.Windows.Forms.TextBox
		Me.txtBuilding = New System.Windows.Forms.TextBox
		Me.txtRoad = New System.Windows.Forms.TextBox
		Me.txtArea = New System.Windows.Forms.TextBox
		Me.txtTown = New System.Windows.Forms.TextBox
		Me.txtState = New System.Windows.Forms.TextBox
		Me.txtPinCode = New System.Windows.Forms.TextBox
		Me.txtDesg = New System.Windows.Forms.TextBox
		Me.txtPersonName = New System.Windows.Forms.TextBox
		Me.Label20 = New System.Windows.Forms.Label
		Me.Label19 = New System.Windows.Forms.Label
		Me.Label18 = New System.Windows.Forms.Label
		Me.Label14 = New System.Windows.Forms.Label
		Me.Label13 = New System.Windows.Forms.Label
		Me.Label8 = New System.Windows.Forms.Label
		Me.Label9 = New System.Windows.Forms.Label
		Me.Label10 = New System.Windows.Forms.Label
		Me.Label11 = New System.Windows.Forms.Label
		Me.Label12 = New System.Windows.Forms.Label
		Me.Label5 = New System.Windows.Forms.Label
		Me.Label4 = New System.Windows.Forms.Label
		Me._SSTab1_TabPage1 = New System.Windows.Forms.TabPage
		Me.Frame6 = New System.Windows.Forms.GroupBox
		Me.txtReturnPeriod = New System.Windows.Forms.TextBox
		Me.txtDeductorStatus = New System.Windows.Forms.TextBox
		Me.txtAddressChange = New System.Windows.Forms.TextBox
		Me.txtRundate = New System.Windows.Forms.TextBox
		Me.Label7 = New System.Windows.Forms.Label
		Me.Label6 = New System.Windows.Forms.Label
		Me.Label3 = New System.Windows.Forms.Label
		Me.Label2 = New System.Windows.Forms.Label
		Me.Label1 = New System.Windows.Forms.Label
		Me.Lable11 = New System.Windows.Forms.Label
		Me.Frame7 = New System.Windows.Forms.GroupBox
		Me.chk194C = New System.Windows.Forms.CheckBox
		Me.chk194BB = New System.Windows.Forms.CheckBox
		Me.chk194B = New System.Windows.Forms.CheckBox
		Me.chk194A = New System.Windows.Forms.CheckBox
		Me.chk194 = New System.Windows.Forms.CheckBox
		Me.chk193 = New System.Windows.Forms.CheckBox
		Me.Frame9 = New System.Windows.Forms.GroupBox
		Me._optStatus_0 = New System.Windows.Forms.RadioButton
		Me._optStatus_1 = New System.Windows.Forms.RadioButton
		Me.Frame8 = New System.Windows.Forms.GroupBox
		Me._optCentralGovt_1 = New System.Windows.Forms.RadioButton
		Me._optCentralGovt_0 = New System.Windows.Forms.RadioButton
		Me.Label23 = New System.Windows.Forms.Label
		Me.Label22 = New System.Windows.Forms.Label
		Me.Label21 = New System.Windows.Forms.Label
		Me._SSTab1_TabPage2 = New System.Windows.Forms.TabPage
		Me.Frame10 = New System.Windows.Forms.GroupBox
		Me.SprdView26 = New AxFPSpreadADO.AxfpSpread
		Me.Label24 = New System.Windows.Forms.Label
		Me._SSTab1_TabPage3 = New System.Windows.Forms.TabPage
		Me.Frame1 = New System.Windows.Forms.GroupBox
		Me.SprdViewAnnex = New AxFPSpreadADO.AxfpSpread
		Me.FraMovement = New System.Windows.Forms.GroupBox
		Me.cmdValidate = New System.Windows.Forms.Button
		Me.chkConsolidated = New System.Windows.Forms.CheckBox
		Me.cmdCD = New System.Windows.Forms.Button
		Me.CmdPreview = New System.Windows.Forms.Button
		Me.cmdPrint = New System.Windows.Forms.Button
		Me.cmdClose = New System.Windows.Forms.Button
		Me.cmdShow = New System.Windows.Forms.Button
		Me.Report1 = New AxCrystal.AxCrystalReport
		Me.optCentralGovt = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(components)
		Me.optStatus = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(components)
		Me.SSTab1.SuspendLayout()
		Me._SSTab1_TabPage0.SuspendLayout()
		Me.Frame2.SuspendLayout()
		Me.Frame3.SuspendLayout()
		Me._SSTab1_TabPage1.SuspendLayout()
		Me.Frame6.SuspendLayout()
		Me.Frame7.SuspendLayout()
		Me.Frame9.SuspendLayout()
		Me.Frame8.SuspendLayout()
		Me._SSTab1_TabPage2.SuspendLayout()
		Me.Frame10.SuspendLayout()
		Me._SSTab1_TabPage3.SuspendLayout()
		Me.Frame1.SuspendLayout()
		Me.FraMovement.SuspendLayout()
		Me.SuspendLayout()
		Me.ToolTip1.Active = True
		CType(Me.SprdView26, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.SprdViewAnnex, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.optCentralGovt, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.optStatus, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
		Me.Text = "TDS e-Return (Form 27)"
		Me.ClientSize = New System.Drawing.Size(672, 394)
		Me.Location = New System.Drawing.Point(3, 22)
		Me.Icon = CType(resources.GetObject("frmTDSeReturn27_New.Icon"), System.Drawing.Icon)
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
		Me.Name = "frmTDSeReturn27_New"
		Me.SSTab1.Size = New System.Drawing.Size(671, 349)
		Me.SSTab1.Location = New System.Drawing.Point(0, 0)
		Me.SSTab1.TabIndex = 9
		Me.SSTab1.SelectedIndex = 1
		Me.SSTab1.ItemSize = New System.Drawing.Size(42, 22)
		Me.SSTab1.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.SSTab1.Name = "SSTab1"
		Me._SSTab1_TabPage0.Text = "Company Details"
		Me.Frame2.Size = New System.Drawing.Size(659, 69)
		Me.Frame2.Location = New System.Drawing.Point(4, 26)
		Me.Frame2.TabIndex = 64
		Me.Frame2.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame2.BackColor = System.Drawing.SystemColors.Control
		Me.Frame2.Enabled = True
		Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame2.Visible = True
		Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame2.Name = "Frame2"
		Me.CmdSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.CmdSearch.BackColor = System.Drawing.SystemColors.Menu
		Me.CmdSearch.Size = New System.Drawing.Size(27, 19)
		Me.CmdSearch.Location = New System.Drawing.Point(592, 42)
		Me.CmdSearch.Image = CType(resources.GetObject("CmdSearch.Image"), System.Drawing.Image)
		Me.CmdSearch.TabIndex = 66
		Me.CmdSearch.TabStop = False
		Me.ToolTip1.SetToolTip(Me.CmdSearch, "Search")
		Me.CmdSearch.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CmdSearch.CausesValidation = True
		Me.CmdSearch.Enabled = True
		Me.CmdSearch.ForeColor = System.Drawing.SystemColors.ControlText
		Me.CmdSearch.Cursor = System.Windows.Forms.Cursors.Default
		Me.CmdSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CmdSearch.Name = "CmdSearch"
		Me.TxtAccount.AutoSize = False
		Me.TxtAccount.Size = New System.Drawing.Size(357, 19)
		Me.TxtAccount.Location = New System.Drawing.Point(234, 42)
		Me.TxtAccount.TabIndex = 65
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
		Me.txtDateFrom.AllowPromptAsInput = False
		Me.txtDateFrom.Size = New System.Drawing.Size(83, 21)
		Me.txtDateFrom.Location = New System.Drawing.Point(235, 16)
		Me.txtDateFrom.TabIndex = 67
		Me.txtDateFrom.MaxLength = 10
		Me.txtDateFrom.Mask = "##/##/####"
		Me.txtDateFrom.PromptChar = "_"
		Me.txtDateFrom.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtDateFrom.Name = "txtDateFrom"
		Me.txtDateTo.AllowPromptAsInput = False
		Me.txtDateTo.Size = New System.Drawing.Size(83, 21)
		Me.txtDateTo.Location = New System.Drawing.Point(447, 16)
		Me.txtDateTo.TabIndex = 68
		Me.txtDateTo.MaxLength = 10
		Me.txtDateTo.Mask = "##/##/####"
		Me.txtDateTo.PromptChar = "_"
		Me.txtDateTo.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtDateTo.Name = "txtDateTo"
		Me.Label15.Text = "TDS Account Name :"
		Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label15.Size = New System.Drawing.Size(125, 13)
		Me.Label15.Location = New System.Drawing.Point(14, 42)
		Me.Label15.TabIndex = 71
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
		Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.Label16.Text = "From : "
		Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label16.Size = New System.Drawing.Size(40, 13)
		Me.Label16.Location = New System.Drawing.Point(196, 20)
		Me.Label16.TabIndex = 70
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
		Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.Label17.Text = "To : "
		Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label17.Size = New System.Drawing.Size(28, 13)
		Me.Label17.Location = New System.Drawing.Point(420, 20)
		Me.Label17.TabIndex = 69
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
		Me.Frame3.Size = New System.Drawing.Size(661, 255)
		Me.Frame3.Location = New System.Drawing.Point(4, 90)
		Me.Frame3.TabIndex = 15
		Me.Frame3.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame3.BackColor = System.Drawing.SystemColors.Control
		Me.Frame3.Enabled = True
		Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame3.Visible = True
		Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame3.Name = "Frame3"
		Me.txtPanNo.AutoSize = False
		Me.txtPanNo.Size = New System.Drawing.Size(101, 19)
		Me.txtPanNo.Location = New System.Drawing.Point(555, 228)
		Me.txtPanNo.TabIndex = 44
		Me.txtPanNo.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtPanNo.AcceptsReturn = True
		Me.txtPanNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtPanNo.BackColor = System.Drawing.SystemColors.Window
		Me.txtPanNo.CausesValidation = True
		Me.txtPanNo.Enabled = True
		Me.txtPanNo.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtPanNo.HideSelection = True
		Me.txtPanNo.ReadOnly = False
		Me.txtPanNo.Maxlength = 0
		Me.txtPanNo.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtPanNo.MultiLine = False
		Me.txtPanNo.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtPanNo.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtPanNo.TabStop = True
		Me.txtPanNo.Visible = True
		Me.txtPanNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtPanNo.Name = "txtPanNo"
		Me.txtTDSAcNo.AutoSize = False
		Me.txtTDSAcNo.Size = New System.Drawing.Size(101, 19)
		Me.txtTDSAcNo.Location = New System.Drawing.Point(241, 228)
		Me.txtTDSAcNo.TabIndex = 43
		Me.txtTDSAcNo.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtTDSAcNo.AcceptsReturn = True
		Me.txtTDSAcNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtTDSAcNo.BackColor = System.Drawing.SystemColors.Window
		Me.txtTDSAcNo.CausesValidation = True
		Me.txtTDSAcNo.Enabled = True
		Me.txtTDSAcNo.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtTDSAcNo.HideSelection = True
		Me.txtTDSAcNo.ReadOnly = False
		Me.txtTDSAcNo.Maxlength = 0
		Me.txtTDSAcNo.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtTDSAcNo.MultiLine = False
		Me.txtTDSAcNo.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtTDSAcNo.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtTDSAcNo.TabStop = True
		Me.txtTDSAcNo.Visible = True
		Me.txtTDSAcNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtTDSAcNo.Name = "txtTDSAcNo"
		Me.txtFlat.AutoSize = False
		Me.txtFlat.Size = New System.Drawing.Size(415, 19)
		Me.txtFlat.Location = New System.Drawing.Point(241, 84)
		Me.txtFlat.TabIndex = 2
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
		Me.txtBuilding.Size = New System.Drawing.Size(415, 19)
		Me.txtBuilding.Location = New System.Drawing.Point(241, 108)
		Me.txtBuilding.TabIndex = 3
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
		Me.txtRoad.Size = New System.Drawing.Size(415, 19)
		Me.txtRoad.Location = New System.Drawing.Point(241, 132)
		Me.txtRoad.TabIndex = 4
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
		Me.txtArea.Size = New System.Drawing.Size(415, 19)
		Me.txtArea.Location = New System.Drawing.Point(241, 156)
		Me.txtArea.TabIndex = 5
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
		Me.txtTown.Size = New System.Drawing.Size(415, 19)
		Me.txtTown.Location = New System.Drawing.Point(241, 180)
		Me.txtTown.TabIndex = 6
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
		Me.txtState.Size = New System.Drawing.Size(101, 19)
		Me.txtState.Location = New System.Drawing.Point(241, 204)
		Me.txtState.TabIndex = 7
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
		Me.txtPinCode.Location = New System.Drawing.Point(555, 204)
		Me.txtPinCode.TabIndex = 8
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
		Me.txtDesg.Size = New System.Drawing.Size(415, 19)
		Me.txtDesg.Location = New System.Drawing.Point(241, 60)
		Me.txtDesg.TabIndex = 1
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
		Me.txtPersonName.Size = New System.Drawing.Size(415, 19)
		Me.txtPersonName.Location = New System.Drawing.Point(241, 36)
		Me.txtPersonName.TabIndex = 0
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
		Me.Label20.Text = "(d) Permanent A/c Number :"
		Me.Label20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label20.Size = New System.Drawing.Size(160, 13)
		Me.Label20.Location = New System.Drawing.Point(386, 232)
		Me.Label20.TabIndex = 42
		Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label20.BackColor = System.Drawing.SystemColors.Control
		Me.Label20.Enabled = True
		Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label20.UseMnemonic = True
		Me.Label20.Visible = True
		Me.Label20.AutoSize = True
		Me.Label20.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label20.Name = "Label20"
		Me.Label19.Text = "(c) Tax Deduction A/c Number :"
		Me.Label19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label19.Size = New System.Drawing.Size(183, 13)
		Me.Label19.Location = New System.Drawing.Point(24, 232)
		Me.Label19.TabIndex = 41
		Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label19.BackColor = System.Drawing.SystemColors.Control
		Me.Label19.Enabled = True
		Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label19.UseMnemonic = True
		Me.Label19.Visible = True
		Me.Label19.AutoSize = True
		Me.Label19.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label19.Name = "Label19"
		Me.Label18.Text = "1. Particulars of the person making deduction of tax :"
		Me.Label18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label18.Size = New System.Drawing.Size(303, 13)
		Me.Label18.Location = New System.Drawing.Point(12, 16)
		Me.Label18.TabIndex = 40
		Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label18.BackColor = System.Drawing.SystemColors.Control
		Me.Label18.Enabled = True
		Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label18.UseMnemonic = True
		Me.Label18.Visible = True
		Me.Label18.AutoSize = True
		Me.Label18.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label18.Name = "Label18"
		Me.Label14.Text = "(b) Address :Flat / Door / Block No. :"
		Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label14.Size = New System.Drawing.Size(213, 13)
		Me.Label14.Location = New System.Drawing.Point(24, 88)
		Me.Label14.TabIndex = 24
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
		Me.Label13.Location = New System.Drawing.Point(42, 110)
		Me.Label13.TabIndex = 23
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
		Me.Label8.Location = New System.Drawing.Point(42, 134)
		Me.Label8.TabIndex = 22
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
		Me.Label9.Location = New System.Drawing.Point(42, 158)
		Me.Label9.TabIndex = 21
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
		Me.Label10.Location = New System.Drawing.Point(42, 182)
		Me.Label10.TabIndex = 20
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
		Me.Label11.Location = New System.Drawing.Point(42, 206)
		Me.Label11.TabIndex = 19
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
		Me.Label12.Location = New System.Drawing.Point(486, 206)
		Me.Label12.TabIndex = 18
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
		Me.Label5.Text = "Designation :"
		Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label5.Size = New System.Drawing.Size(76, 13)
		Me.Label5.Location = New System.Drawing.Point(42, 60)
		Me.Label5.TabIndex = 17
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
		Me.Label4.Text = "(a) Name :"
		Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label4.Size = New System.Drawing.Size(60, 13)
		Me.Label4.Location = New System.Drawing.Point(24, 36)
		Me.Label4.TabIndex = 16
		Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label4.BackColor = System.Drawing.SystemColors.Control
		Me.Label4.Enabled = True
		Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label4.UseMnemonic = True
		Me.Label4.Visible = True
		Me.Label4.AutoSize = True
		Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label4.Name = "Label4"
		Me._SSTab1_TabPage1.Text = "e Return Detail"
		Me.Frame6.Size = New System.Drawing.Size(661, 67)
		Me.Frame6.Location = New System.Drawing.Point(4, 264)
		Me.Frame6.TabIndex = 29
		Me.Frame6.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame6.BackColor = System.Drawing.SystemColors.Control
		Me.Frame6.Enabled = True
		Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame6.Visible = True
		Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame6.Name = "Frame6"
		Me.txtReturnPeriod.AutoSize = False
		Me.txtReturnPeriod.Size = New System.Drawing.Size(45, 19)
		Me.txtReturnPeriod.Location = New System.Drawing.Point(552, 40)
		Me.txtReturnPeriod.TabIndex = 33
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
		Me.txtDeductorStatus.Location = New System.Drawing.Point(187, 40)
		Me.txtDeductorStatus.TabIndex = 32
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
		Me.txtAddressChange.Location = New System.Drawing.Point(552, 14)
		Me.txtAddressChange.TabIndex = 31
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
		Me.txtRundate.Location = New System.Drawing.Point(187, 14)
		Me.txtRundate.TabIndex = 30
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
		Me.Label7.Text = "(C/O)"
		Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label7.Size = New System.Drawing.Size(32, 13)
		Me.Label7.Location = New System.Drawing.Point(234, 42)
		Me.Label7.TabIndex = 39
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
		Me.Label6.Location = New System.Drawing.Point(604, 16)
		Me.Label6.TabIndex = 38
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
		Me.Label3.Text = "Quarterly/Yearly Return :"
		Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label3.Size = New System.Drawing.Size(143, 13)
		Me.Label3.Location = New System.Drawing.Point(374, 40)
		Me.Label3.TabIndex = 37
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
		Me.Label2.Location = New System.Drawing.Point(12, 40)
		Me.Label2.TabIndex = 36
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
		Me.Label1.Location = New System.Drawing.Point(374, 14)
		Me.Label1.TabIndex = 35
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
		Me.Lable11.Location = New System.Drawing.Point(12, 14)
		Me.Lable11.TabIndex = 34
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
		Me.Frame7.Size = New System.Drawing.Size(661, 239)
		Me.Frame7.Location = New System.Drawing.Point(4, 26)
		Me.Frame7.TabIndex = 45
		Me.Frame7.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame7.BackColor = System.Drawing.SystemColors.Control
		Me.Frame7.Enabled = True
		Me.Frame7.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame7.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame7.Visible = True
		Me.Frame7.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame7.Name = "Frame7"
		Me.chk194C.Text = "196D"
		Me.chk194C.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.chk194C.Size = New System.Drawing.Size(60, 13)
		Me.chk194C.Location = New System.Drawing.Point(523, 190)
		Me.chk194C.TabIndex = 60
		Me.chk194C.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chk194C.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.chk194C.FlatStyle = System.Windows.Forms.FlatStyle.Standard
		Me.chk194C.BackColor = System.Drawing.SystemColors.Control
		Me.chk194C.CausesValidation = True
		Me.chk194C.Enabled = True
		Me.chk194C.ForeColor = System.Drawing.SystemColors.ControlText
		Me.chk194C.Cursor = System.Windows.Forms.Cursors.Default
		Me.chk194C.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.chk194C.Appearance = System.Windows.Forms.Appearance.Normal
		Me.chk194C.TabStop = True
		Me.chk194C.Visible = True
		Me.chk194C.Name = "chk194C"
		Me.chk194BB.Text = "196C"
		Me.chk194BB.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.chk194BB.Size = New System.Drawing.Size(60, 13)
		Me.chk194BB.Location = New System.Drawing.Point(429, 190)
		Me.chk194BB.TabIndex = 59
		Me.chk194BB.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chk194BB.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.chk194BB.FlatStyle = System.Windows.Forms.FlatStyle.Standard
		Me.chk194BB.BackColor = System.Drawing.SystemColors.Control
		Me.chk194BB.CausesValidation = True
		Me.chk194BB.Enabled = True
		Me.chk194BB.ForeColor = System.Drawing.SystemColors.ControlText
		Me.chk194BB.Cursor = System.Windows.Forms.Cursors.Default
		Me.chk194BB.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.chk194BB.Appearance = System.Windows.Forms.Appearance.Normal
		Me.chk194BB.TabStop = True
		Me.chk194BB.Visible = True
		Me.chk194BB.Name = "chk194BB"
		Me.chk194B.Text = "196B"
		Me.chk194B.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.chk194B.Size = New System.Drawing.Size(60, 13)
		Me.chk194B.Location = New System.Drawing.Point(336, 188)
		Me.chk194B.TabIndex = 58
		Me.chk194B.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chk194B.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.chk194B.FlatStyle = System.Windows.Forms.FlatStyle.Standard
		Me.chk194B.BackColor = System.Drawing.SystemColors.Control
		Me.chk194B.CausesValidation = True
		Me.chk194B.Enabled = True
		Me.chk194B.ForeColor = System.Drawing.SystemColors.ControlText
		Me.chk194B.Cursor = System.Windows.Forms.Cursors.Default
		Me.chk194B.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.chk194B.Appearance = System.Windows.Forms.Appearance.Normal
		Me.chk194B.TabStop = True
		Me.chk194B.Visible = True
		Me.chk194B.Name = "chk194B"
		Me.chk194A.Text = "196A"
		Me.chk194A.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.chk194A.Size = New System.Drawing.Size(60, 13)
		Me.chk194A.Location = New System.Drawing.Point(243, 190)
		Me.chk194A.TabIndex = 57
		Me.chk194A.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chk194A.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.chk194A.FlatStyle = System.Windows.Forms.FlatStyle.Standard
		Me.chk194A.BackColor = System.Drawing.SystemColors.Control
		Me.chk194A.CausesValidation = True
		Me.chk194A.Enabled = True
		Me.chk194A.ForeColor = System.Drawing.SystemColors.ControlText
		Me.chk194A.Cursor = System.Windows.Forms.Cursors.Default
		Me.chk194A.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.chk194A.Appearance = System.Windows.Forms.Appearance.Normal
		Me.chk194A.TabStop = True
		Me.chk194A.Visible = True
		Me.chk194A.Name = "chk194A"
		Me.chk194.Text = "195"
		Me.chk194.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.chk194.Size = New System.Drawing.Size(60, 13)
		Me.chk194.Location = New System.Drawing.Point(149, 192)
		Me.chk194.TabIndex = 56
		Me.chk194.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chk194.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.chk194.FlatStyle = System.Windows.Forms.FlatStyle.Standard
		Me.chk194.BackColor = System.Drawing.SystemColors.Control
		Me.chk194.CausesValidation = True
		Me.chk194.Enabled = True
		Me.chk194.ForeColor = System.Drawing.SystemColors.ControlText
		Me.chk194.Cursor = System.Windows.Forms.Cursors.Default
		Me.chk194.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.chk194.Appearance = System.Windows.Forms.Appearance.Normal
		Me.chk194.TabStop = True
		Me.chk194.Visible = True
		Me.chk194.Name = "chk194"
		Me.chk193.Text = "194E"
		Me.chk193.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.chk193.Size = New System.Drawing.Size(60, 13)
		Me.chk193.Location = New System.Drawing.Point(56, 192)
		Me.chk193.TabIndex = 55
		Me.chk193.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chk193.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.chk193.FlatStyle = System.Windows.Forms.FlatStyle.Standard
		Me.chk193.BackColor = System.Drawing.SystemColors.Control
		Me.chk193.CausesValidation = True
		Me.chk193.Enabled = True
		Me.chk193.ForeColor = System.Drawing.SystemColors.ControlText
		Me.chk193.Cursor = System.Windows.Forms.Cursors.Default
		Me.chk193.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.chk193.Appearance = System.Windows.Forms.Appearance.Normal
		Me.chk193.TabStop = True
		Me.chk193.Visible = True
		Me.chk193.Name = "chk193"
		Me.Frame9.Text = "Tick as applicable"
		Me.Frame9.Size = New System.Drawing.Size(291, 37)
		Me.Frame9.Location = New System.Drawing.Point(188, 98)
		Me.Frame9.TabIndex = 51
		Me.Frame9.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame9.BackColor = System.Drawing.SystemColors.Control
		Me.Frame9.Enabled = True
		Me.Frame9.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame9.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame9.Visible = True
		Me.Frame9.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame9.Name = "Frame9"
		Me._optStatus_0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._optStatus_0.Text = "Yes"
		Me._optStatus_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me._optStatus_0.Size = New System.Drawing.Size(121, 13)
		Me._optStatus_0.Location = New System.Drawing.Point(18, 18)
		Me._optStatus_0.TabIndex = 53
		Me._optStatus_0.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._optStatus_0.BackColor = System.Drawing.SystemColors.Control
		Me._optStatus_0.CausesValidation = True
		Me._optStatus_0.Enabled = True
		Me._optStatus_0.ForeColor = System.Drawing.SystemColors.ControlText
		Me._optStatus_0.Cursor = System.Windows.Forms.Cursors.Default
		Me._optStatus_0.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._optStatus_0.Appearance = System.Windows.Forms.Appearance.Normal
		Me._optStatus_0.TabStop = True
		Me._optStatus_0.Checked = False
		Me._optStatus_0.Visible = True
		Me._optStatus_0.Name = "_optStatus_0"
		Me._optStatus_1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._optStatus_1.Text = "No"
		Me._optStatus_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me._optStatus_1.Size = New System.Drawing.Size(121, 13)
		Me._optStatus_1.Location = New System.Drawing.Point(152, 18)
		Me._optStatus_1.TabIndex = 52
		Me._optStatus_1.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._optStatus_1.BackColor = System.Drawing.SystemColors.Control
		Me._optStatus_1.CausesValidation = True
		Me._optStatus_1.Enabled = True
		Me._optStatus_1.ForeColor = System.Drawing.SystemColors.ControlText
		Me._optStatus_1.Cursor = System.Windows.Forms.Cursors.Default
		Me._optStatus_1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._optStatus_1.Appearance = System.Windows.Forms.Appearance.Normal
		Me._optStatus_1.TabStop = True
		Me._optStatus_1.Checked = False
		Me._optStatus_1.Visible = True
		Me._optStatus_1.Name = "_optStatus_1"
		Me.Frame8.Text = "Tick as applicable"
		Me.Frame8.Size = New System.Drawing.Size(291, 37)
		Me.Frame8.Location = New System.Drawing.Point(188, 38)
		Me.Frame8.TabIndex = 48
		Me.Frame8.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame8.BackColor = System.Drawing.SystemColors.Control
		Me.Frame8.Enabled = True
		Me.Frame8.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame8.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame8.Visible = True
		Me.Frame8.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame8.Name = "Frame8"
		Me._optCentralGovt_1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._optCentralGovt_1.Text = "Others"
		Me._optCentralGovt_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me._optCentralGovt_1.Size = New System.Drawing.Size(121, 13)
		Me._optCentralGovt_1.Location = New System.Drawing.Point(152, 18)
		Me._optCentralGovt_1.TabIndex = 50
		Me._optCentralGovt_1.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._optCentralGovt_1.BackColor = System.Drawing.SystemColors.Control
		Me._optCentralGovt_1.CausesValidation = True
		Me._optCentralGovt_1.Enabled = True
		Me._optCentralGovt_1.ForeColor = System.Drawing.SystemColors.ControlText
		Me._optCentralGovt_1.Cursor = System.Windows.Forms.Cursors.Default
		Me._optCentralGovt_1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._optCentralGovt_1.Appearance = System.Windows.Forms.Appearance.Normal
		Me._optCentralGovt_1.TabStop = True
		Me._optCentralGovt_1.Checked = False
		Me._optCentralGovt_1.Visible = True
		Me._optCentralGovt_1.Name = "_optCentralGovt_1"
		Me._optCentralGovt_0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._optCentralGovt_0.Text = "Central Govt."
		Me._optCentralGovt_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me._optCentralGovt_0.Size = New System.Drawing.Size(121, 13)
		Me._optCentralGovt_0.Location = New System.Drawing.Point(18, 18)
		Me._optCentralGovt_0.TabIndex = 49
		Me._optCentralGovt_0.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._optCentralGovt_0.BackColor = System.Drawing.SystemColors.Control
		Me._optCentralGovt_0.CausesValidation = True
		Me._optCentralGovt_0.Enabled = True
		Me._optCentralGovt_0.ForeColor = System.Drawing.SystemColors.ControlText
		Me._optCentralGovt_0.Cursor = System.Windows.Forms.Cursors.Default
		Me._optCentralGovt_0.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._optCentralGovt_0.Appearance = System.Windows.Forms.Appearance.Normal
		Me._optCentralGovt_0.TabStop = True
		Me._optCentralGovt_0.Checked = False
		Me._optCentralGovt_0.Visible = True
		Me._optCentralGovt_0.Name = "_optCentralGovt_0"
		Me.Label23.Text = "(a) Please tick the boxes below indicating the section and type of payment (other than ""Salaries"") made during the year from which tax was required to be deducted at source by you under the Income-tax Act, 1961 :"
		Me.Label23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label23.Size = New System.Drawing.Size(629, 29)
		Me.Label23.Location = New System.Drawing.Point(24, 148)
		Me.Label23.TabIndex = 54
		Me.Label23.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label23.BackColor = System.Drawing.SystemColors.Control
		Me.Label23.Enabled = True
		Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label23.UseMnemonic = True
		Me.Label23.Visible = True
		Me.Label23.AutoSize = True
		Me.Label23.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label23.Name = "Label23"
		Me.Label22.Text = "2. Has address of the person making deduction of tax Changed :"
		Me.Label22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label22.Size = New System.Drawing.Size(367, 13)
		Me.Label22.Location = New System.Drawing.Point(12, 20)
		Me.Label22.TabIndex = 47
		Me.Label22.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label22.BackColor = System.Drawing.SystemColors.Control
		Me.Label22.Enabled = True
		Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label22.UseMnemonic = True
		Me.Label22.Visible = True
		Me.Label22.AutoSize = True
		Me.Label22.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label22.Name = "Label22"
		Me.Label21.Text = "3. Status as defined within the meaning of section 204 read with rule 30 :"
		Me.Label21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label21.Size = New System.Drawing.Size(417, 13)
		Me.Label21.Location = New System.Drawing.Point(12, 80)
		Me.Label21.TabIndex = 46
		Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label21.BackColor = System.Drawing.SystemColors.Control
		Me.Label21.Enabled = True
		Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label21.UseMnemonic = True
		Me.Label21.Visible = True
		Me.Label21.AutoSize = True
		Me.Label21.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label21.Name = "Label21"
		Me._SSTab1_TabPage2.Text = "Challan Detail"
		Me.Frame10.Size = New System.Drawing.Size(661, 301)
		Me.Frame10.Location = New System.Drawing.Point(4, 42)
		Me.Frame10.TabIndex = 61
		Me.Frame10.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame10.BackColor = System.Drawing.SystemColors.Control
		Me.Frame10.Enabled = True
		Me.Frame10.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame10.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame10.Visible = True
		Me.Frame10.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame10.Name = "Frame10"
		SprdView26.OcxState = CType(resources.GetObject("SprdView26.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SprdView26.Size = New System.Drawing.Size(656, 287)
		Me.SprdView26.Location = New System.Drawing.Point(2, 10)
		Me.SprdView26.TabIndex = 62
		Me.SprdView26.Name = "SprdView26"
		Me.Label24.Text = "4.Details of tax deducted and paid to the credit of the Central Government :"
		Me.Label24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label24.Size = New System.Drawing.Size(431, 13)
		Me.Label24.Location = New System.Drawing.Point(4, 30)
		Me.Label24.TabIndex = 63
		Me.Label24.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label24.BackColor = System.Drawing.SystemColors.Control
		Me.Label24.Enabled = True
		Me.Label24.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label24.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label24.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label24.UseMnemonic = True
		Me.Label24.Visible = True
		Me.Label24.AutoSize = True
		Me.Label24.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label24.Name = "Label24"
		Me._SSTab1_TabPage3.Text = "Annexure Detail"
		Me.Frame1.Size = New System.Drawing.Size(661, 317)
		Me.Frame1.Location = New System.Drawing.Point(4, 26)
		Me.Frame1.TabIndex = 27
		Me.Frame1.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame1.BackColor = System.Drawing.SystemColors.Control
		Me.Frame1.Enabled = True
		Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame1.Visible = True
		Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame1.Name = "Frame1"
		SprdViewAnnex.OcxState = CType(resources.GetObject("SprdViewAnnex.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SprdViewAnnex.Size = New System.Drawing.Size(656, 305)
		Me.SprdViewAnnex.Location = New System.Drawing.Point(2, 8)
		Me.SprdViewAnnex.TabIndex = 28
		Me.SprdViewAnnex.Name = "SprdViewAnnex"
		Me.FraMovement.Size = New System.Drawing.Size(671, 49)
		Me.FraMovement.Location = New System.Drawing.Point(0, 344)
		Me.FraMovement.TabIndex = 10
		Me.FraMovement.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
		Me.FraMovement.Enabled = True
		Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
		Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.FraMovement.Visible = True
		Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
		Me.FraMovement.Name = "FraMovement"
		Me.cmdValidate.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.cmdValidate.Text = "&Validate"
		Me.cmdValidate.Size = New System.Drawing.Size(67, 37)
		Me.cmdValidate.Location = New System.Drawing.Point(332, 9)
		Me.cmdValidate.Image = CType(resources.GetObject("cmdValidate.Image"), System.Drawing.Image)
		Me.cmdValidate.TabIndex = 72
		Me.ToolTip1.SetToolTip(Me.cmdValidate, "Show Record")
		Me.cmdValidate.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdValidate.BackColor = System.Drawing.SystemColors.Control
		Me.cmdValidate.CausesValidation = True
		Me.cmdValidate.Enabled = True
		Me.cmdValidate.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdValidate.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdValidate.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdValidate.TabStop = True
		Me.cmdValidate.Name = "cmdValidate"
		Me.chkConsolidated.Text = "Consolidated"
		Me.chkConsolidated.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.chkConsolidated.Size = New System.Drawing.Size(177, 19)
		Me.chkConsolidated.Location = New System.Drawing.Point(12, 20)
		Me.chkConsolidated.TabIndex = 26
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
		Me.cmdCD.TabIndex = 25
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
		Me.CmdPreview.TabIndex = 13
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
		Me.cmdPrint.TabIndex = 12
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
		Me.cmdClose.TabIndex = 14
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
		Me.cmdShow.Location = New System.Drawing.Point(264, 9)
		Me.cmdShow.Image = CType(resources.GetObject("cmdShow.Image"), System.Drawing.Image)
		Me.cmdShow.TabIndex = 11
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
		Me.Report1.Location = New System.Drawing.Point(148, 10)
		Me.Report1.Name = "Report1"
		Me.optCentralGovt.SetIndex(_optCentralGovt_1, CType(1, Short))
		Me.optCentralGovt.SetIndex(_optCentralGovt_0, CType(0, Short))
		Me.optStatus.SetIndex(_optStatus_0, CType(0, Short))
		Me.optStatus.SetIndex(_optStatus_1, CType(1, Short))
		CType(Me.optStatus, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.optCentralGovt, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SprdViewAnnex, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SprdView26, System.ComponentModel.ISupportInitialize).EndInit()
		Me.Controls.Add(SSTab1)
		Me.Controls.Add(FraMovement)
		Me.SSTab1.Controls.Add(_SSTab1_TabPage0)
		Me.SSTab1.Controls.Add(_SSTab1_TabPage1)
		Me.SSTab1.Controls.Add(_SSTab1_TabPage2)
		Me.SSTab1.Controls.Add(_SSTab1_TabPage3)
		Me._SSTab1_TabPage0.Controls.Add(Frame2)
		Me._SSTab1_TabPage0.Controls.Add(Frame3)
		Me.Frame2.Controls.Add(CmdSearch)
		Me.Frame2.Controls.Add(TxtAccount)
		Me.Frame2.Controls.Add(txtDateFrom)
		Me.Frame2.Controls.Add(txtDateTo)
		Me.Frame2.Controls.Add(Label15)
		Me.Frame2.Controls.Add(Label16)
		Me.Frame2.Controls.Add(Label17)
		Me.Frame3.Controls.Add(txtPanNo)
		Me.Frame3.Controls.Add(txtTDSAcNo)
		Me.Frame3.Controls.Add(txtFlat)
		Me.Frame3.Controls.Add(txtBuilding)
		Me.Frame3.Controls.Add(txtRoad)
		Me.Frame3.Controls.Add(txtArea)
		Me.Frame3.Controls.Add(txtTown)
		Me.Frame3.Controls.Add(txtState)
		Me.Frame3.Controls.Add(txtPinCode)
		Me.Frame3.Controls.Add(txtDesg)
		Me.Frame3.Controls.Add(txtPersonName)
		Me.Frame3.Controls.Add(Label20)
		Me.Frame3.Controls.Add(Label19)
		Me.Frame3.Controls.Add(Label18)
		Me.Frame3.Controls.Add(Label14)
		Me.Frame3.Controls.Add(Label13)
		Me.Frame3.Controls.Add(Label8)
		Me.Frame3.Controls.Add(Label9)
		Me.Frame3.Controls.Add(Label10)
		Me.Frame3.Controls.Add(Label11)
		Me.Frame3.Controls.Add(Label12)
		Me.Frame3.Controls.Add(Label5)
		Me.Frame3.Controls.Add(Label4)
		Me._SSTab1_TabPage1.Controls.Add(Frame6)
		Me._SSTab1_TabPage1.Controls.Add(Frame7)
		Me.Frame6.Controls.Add(txtReturnPeriod)
		Me.Frame6.Controls.Add(txtDeductorStatus)
		Me.Frame6.Controls.Add(txtAddressChange)
		Me.Frame6.Controls.Add(txtRundate)
		Me.Frame6.Controls.Add(Label7)
		Me.Frame6.Controls.Add(Label6)
		Me.Frame6.Controls.Add(Label3)
		Me.Frame6.Controls.Add(Label2)
		Me.Frame6.Controls.Add(Label1)
		Me.Frame6.Controls.Add(Lable11)
		Me.Frame7.Controls.Add(chk194C)
		Me.Frame7.Controls.Add(chk194BB)
		Me.Frame7.Controls.Add(chk194B)
		Me.Frame7.Controls.Add(chk194A)
		Me.Frame7.Controls.Add(chk194)
		Me.Frame7.Controls.Add(chk193)
		Me.Frame7.Controls.Add(Frame9)
		Me.Frame7.Controls.Add(Frame8)
		Me.Frame7.Controls.Add(Label23)
		Me.Frame7.Controls.Add(Label22)
		Me.Frame7.Controls.Add(Label21)
		Me.Frame9.Controls.Add(_optStatus_0)
		Me.Frame9.Controls.Add(_optStatus_1)
		Me.Frame8.Controls.Add(_optCentralGovt_1)
		Me.Frame8.Controls.Add(_optCentralGovt_0)
		Me._SSTab1_TabPage2.Controls.Add(Frame10)
		Me._SSTab1_TabPage2.Controls.Add(Label24)
		Me.Frame10.Controls.Add(SprdView26)
		Me._SSTab1_TabPage3.Controls.Add(Frame1)
		Me.Frame1.Controls.Add(SprdViewAnnex)
		Me.FraMovement.Controls.Add(cmdValidate)
		Me.FraMovement.Controls.Add(chkConsolidated)
		Me.FraMovement.Controls.Add(cmdCD)
		Me.FraMovement.Controls.Add(CmdPreview)
		Me.FraMovement.Controls.Add(cmdPrint)
		Me.FraMovement.Controls.Add(cmdClose)
		Me.FraMovement.Controls.Add(cmdShow)
		Me.FraMovement.Controls.Add(Report1)
		Me.SSTab1.ResumeLayout(False)
		Me._SSTab1_TabPage0.ResumeLayout(False)
		Me.Frame2.ResumeLayout(False)
		Me.Frame3.ResumeLayout(False)
		Me._SSTab1_TabPage1.ResumeLayout(False)
		Me.Frame6.ResumeLayout(False)
		Me.Frame7.ResumeLayout(False)
		Me.Frame9.ResumeLayout(False)
		Me.Frame8.ResumeLayout(False)
		Me._SSTab1_TabPage2.ResumeLayout(False)
		Me.Frame10.ResumeLayout(False)
		Me._SSTab1_TabPage3.ResumeLayout(False)
		Me.Frame1.ResumeLayout(False)
		Me.FraMovement.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()
	End Sub
#End Region 
End Class