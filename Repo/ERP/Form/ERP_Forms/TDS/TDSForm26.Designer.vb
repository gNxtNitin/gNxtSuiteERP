<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmTDSForm26
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
	Public WithEvents ChkNo As System.Windows.Forms.CheckBox
	Public WithEvents ChkYes As System.Windows.Forms.CheckBox
	Public WithEvents txtPinCode As System.Windows.Forms.TextBox
	Public WithEvents txtState As System.Windows.Forms.TextBox
	Public WithEvents txtTown As System.Windows.Forms.TextBox
	Public WithEvents txtArea As System.Windows.Forms.TextBox
	Public WithEvents txtRoad As System.Windows.Forms.TextBox
	Public WithEvents txtBuilding As System.Windows.Forms.TextBox
	Public WithEvents txtFlat As System.Windows.Forms.TextBox
	Public WithEvents txtCompanyName As System.Windows.Forms.TextBox
	Public WithEvents txtPANNo As System.Windows.Forms.TextBox
	Public WithEvents txtTDSNo As System.Windows.Forms.TextBox
	Public WithEvents lblTile As System.Windows.Forms.Label
	Public WithEvents Label24 As System.Windows.Forms.Label
	Public WithEvents lbl2C As System.Windows.Forms.Label
	Public WithEvents Label12 As System.Windows.Forms.Label
	Public WithEvents Label11 As System.Windows.Forms.Label
	Public WithEvents Label10 As System.Windows.Forms.Label
	Public WithEvents Label9 As System.Windows.Forms.Label
	Public WithEvents Label8 As System.Windows.Forms.Label
	Public WithEvents Label7 As System.Windows.Forms.Label
	Public WithEvents Label6 As System.Windows.Forms.Label
	Public WithEvents Label5 As System.Windows.Forms.Label
	Public WithEvents Label4 As System.Windows.Forms.Label
	Public WithEvents Label3 As System.Windows.Forms.Label
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents Frame1 As System.Windows.Forms.GroupBox
	Public WithEvents _SSTab1_TabPage0 As System.Windows.Forms.TabPage
	Public WithEvents SprdView3 As AxFPSpreadADO.AxfpSpread
	Public WithEvents txt26A4E As System.Windows.Forms.TextBox
	Public WithEvents txt26A4D As System.Windows.Forms.TextBox
	Public WithEvents txt26A4C As System.Windows.Forms.TextBox
	Public WithEvents txt26A4B As System.Windows.Forms.TextBox
	Public WithEvents txt26A4A As System.Windows.Forms.TextBox
	Public WithEvents lbl26A4E As System.Windows.Forms.Label
	Public WithEvents lbl26A4D As System.Windows.Forms.Label
	Public WithEvents lbl26A4C As System.Windows.Forms.Label
	Public WithEvents lbl26A4B As System.Windows.Forms.Label
	Public WithEvents lbl26A4A As System.Windows.Forms.Label
	Public WithEvents Fra26A As System.Windows.Forms.GroupBox
	Public WithEvents lbl3A As System.Windows.Forms.Label
	Public WithEvents Frame2 As System.Windows.Forms.GroupBox
	Public WithEvents _SSTab1_TabPage1 As System.Windows.Forms.TabPage
	Public WithEvents SprdView4A As AxFPSpreadADO.AxfpSpread
	Public WithEvents SprdView4B As AxFPSpreadADO.AxfpSpread
	Public WithEvents lbl4B As System.Windows.Forms.Label
	Public WithEvents lbl4A As System.Windows.Forms.Label
	Public WithEvents lbl4 As System.Windows.Forms.Label
	Public WithEvents Frame4 As System.Windows.Forms.GroupBox
	Public WithEvents _SSTab1_TabPage2 As System.Windows.Forms.TabPage
	Public WithEvents SprdView5A As AxFPSpreadADO.AxfpSpread
	Public WithEvents lbl5A As System.Windows.Forms.Label
	Public WithEvents lbl5 As System.Windows.Forms.Label
	Public WithEvents Frame5 As System.Windows.Forms.GroupBox
	Public WithEvents _SSTab1_TabPage3 As System.Windows.Forms.TabPage
	Public WithEvents SprdView5B As AxFPSpreadADO.AxfpSpread
	Public WithEvents lbl5B As System.Windows.Forms.Label
	Public WithEvents Frame6 As System.Windows.Forms.GroupBox
	Public WithEvents _SSTab1_TabPage4 As System.Windows.Forms.TabPage
	Public WithEvents SprdView6A As AxFPSpreadADO.AxfpSpread
	Public WithEvents lbl6A As System.Windows.Forms.Label
	Public WithEvents lbl6 As System.Windows.Forms.Label
	Public WithEvents Frame7 As System.Windows.Forms.GroupBox
	Public WithEvents _SSTab1_TabPage5 As System.Windows.Forms.TabPage
	Public WithEvents SprdView6B As AxFPSpreadADO.AxfpSpread
	Public WithEvents lbl6B As System.Windows.Forms.Label
	Public WithEvents Frame8 As System.Windows.Forms.GroupBox
	Public WithEvents _SSTab1_TabPage6 As System.Windows.Forms.TabPage
	Public WithEvents SprdView7A As AxFPSpreadADO.AxfpSpread
	Public WithEvents lbl7 As System.Windows.Forms.Label
	Public WithEvents lbl7A As System.Windows.Forms.Label
	Public WithEvents Frame3 As System.Windows.Forms.GroupBox
	Public WithEvents _SSTab1_TabPage7 As System.Windows.Forms.TabPage
	Public WithEvents SSTab1 As System.Windows.Forms.TabControl
	Public WithEvents CmdPreview As System.Windows.Forms.Button
	Public WithEvents cmdPrint As System.Windows.Forms.Button
	Public WithEvents cmdClose As System.Windows.Forms.Button
	Public WithEvents cmdShow As System.Windows.Forms.Button
	Public WithEvents Report1 As AxCrystal.AxCrystalReport
	Public WithEvents lblFormType As System.Windows.Forms.Label
	Public WithEvents FraMovement As System.Windows.Forms.GroupBox
	Public WithEvents AData1 As VB6.ADODC
	Public WithEvents AData5A As VB6.ADODC
	Public WithEvents AData5B As VB6.ADODC
	Public WithEvents AData6A As VB6.ADODC
	Public WithEvents AData6B As VB6.ADODC
	Public WithEvents AData7A As VB6.ADODC
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmTDSForm26))
		Me.components = New System.ComponentModel.Container()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
		Me.SSTab1 = New System.Windows.Forms.TabControl
		Me._SSTab1_TabPage0 = New System.Windows.Forms.TabPage
		Me.Frame1 = New System.Windows.Forms.GroupBox
		Me.ChkNo = New System.Windows.Forms.CheckBox
		Me.ChkYes = New System.Windows.Forms.CheckBox
		Me.txtPinCode = New System.Windows.Forms.TextBox
		Me.txtState = New System.Windows.Forms.TextBox
		Me.txtTown = New System.Windows.Forms.TextBox
		Me.txtArea = New System.Windows.Forms.TextBox
		Me.txtRoad = New System.Windows.Forms.TextBox
		Me.txtBuilding = New System.Windows.Forms.TextBox
		Me.txtFlat = New System.Windows.Forms.TextBox
		Me.txtCompanyName = New System.Windows.Forms.TextBox
		Me.txtPANNo = New System.Windows.Forms.TextBox
		Me.txtTDSNo = New System.Windows.Forms.TextBox
		Me.lblTile = New System.Windows.Forms.Label
		Me.Label24 = New System.Windows.Forms.Label
		Me.lbl2C = New System.Windows.Forms.Label
		Me.Label12 = New System.Windows.Forms.Label
		Me.Label11 = New System.Windows.Forms.Label
		Me.Label10 = New System.Windows.Forms.Label
		Me.Label9 = New System.Windows.Forms.Label
		Me.Label8 = New System.Windows.Forms.Label
		Me.Label7 = New System.Windows.Forms.Label
		Me.Label6 = New System.Windows.Forms.Label
		Me.Label5 = New System.Windows.Forms.Label
		Me.Label4 = New System.Windows.Forms.Label
		Me.Label3 = New System.Windows.Forms.Label
		Me.Label2 = New System.Windows.Forms.Label
		Me.Label1 = New System.Windows.Forms.Label
		Me._SSTab1_TabPage1 = New System.Windows.Forms.TabPage
		Me.Frame2 = New System.Windows.Forms.GroupBox
		Me.SprdView3 = New AxFPSpreadADO.AxfpSpread
		Me.Fra26A = New System.Windows.Forms.GroupBox
		Me.txt26A4E = New System.Windows.Forms.TextBox
		Me.txt26A4D = New System.Windows.Forms.TextBox
		Me.txt26A4C = New System.Windows.Forms.TextBox
		Me.txt26A4B = New System.Windows.Forms.TextBox
		Me.txt26A4A = New System.Windows.Forms.TextBox
		Me.lbl26A4E = New System.Windows.Forms.Label
		Me.lbl26A4D = New System.Windows.Forms.Label
		Me.lbl26A4C = New System.Windows.Forms.Label
		Me.lbl26A4B = New System.Windows.Forms.Label
		Me.lbl26A4A = New System.Windows.Forms.Label
		Me.lbl3A = New System.Windows.Forms.Label
		Me._SSTab1_TabPage2 = New System.Windows.Forms.TabPage
		Me.Frame4 = New System.Windows.Forms.GroupBox
		Me.SprdView4A = New AxFPSpreadADO.AxfpSpread
		Me.SprdView4B = New AxFPSpreadADO.AxfpSpread
		Me.lbl4B = New System.Windows.Forms.Label
		Me.lbl4A = New System.Windows.Forms.Label
		Me.lbl4 = New System.Windows.Forms.Label
		Me._SSTab1_TabPage3 = New System.Windows.Forms.TabPage
		Me.Frame5 = New System.Windows.Forms.GroupBox
		Me.SprdView5A = New AxFPSpreadADO.AxfpSpread
		Me.lbl5A = New System.Windows.Forms.Label
		Me.lbl5 = New System.Windows.Forms.Label
		Me._SSTab1_TabPage4 = New System.Windows.Forms.TabPage
		Me.Frame6 = New System.Windows.Forms.GroupBox
		Me.SprdView5B = New AxFPSpreadADO.AxfpSpread
		Me.lbl5B = New System.Windows.Forms.Label
		Me._SSTab1_TabPage5 = New System.Windows.Forms.TabPage
		Me.Frame7 = New System.Windows.Forms.GroupBox
		Me.SprdView6A = New AxFPSpreadADO.AxfpSpread
		Me.lbl6A = New System.Windows.Forms.Label
		Me.lbl6 = New System.Windows.Forms.Label
		Me._SSTab1_TabPage6 = New System.Windows.Forms.TabPage
		Me.Frame8 = New System.Windows.Forms.GroupBox
		Me.SprdView6B = New AxFPSpreadADO.AxfpSpread
		Me.lbl6B = New System.Windows.Forms.Label
		Me._SSTab1_TabPage7 = New System.Windows.Forms.TabPage
		Me.Frame3 = New System.Windows.Forms.GroupBox
		Me.SprdView7A = New AxFPSpreadADO.AxfpSpread
		Me.lbl7 = New System.Windows.Forms.Label
		Me.lbl7A = New System.Windows.Forms.Label
		Me.FraMovement = New System.Windows.Forms.GroupBox
		Me.CmdPreview = New System.Windows.Forms.Button
		Me.cmdPrint = New System.Windows.Forms.Button
		Me.cmdClose = New System.Windows.Forms.Button
		Me.cmdShow = New System.Windows.Forms.Button
		Me.Report1 = New AxCrystal.AxCrystalReport
		Me.lblFormType = New System.Windows.Forms.Label
		Me.AData1 = New VB6.ADODC
		Me.AData5A = New VB6.ADODC
		Me.AData5B = New VB6.ADODC
		Me.AData6A = New VB6.ADODC
		Me.AData6B = New VB6.ADODC
		Me.AData7A = New VB6.ADODC
		Me.SSTab1.SuspendLayout()
		Me._SSTab1_TabPage0.SuspendLayout()
		Me.Frame1.SuspendLayout()
		Me._SSTab1_TabPage1.SuspendLayout()
		Me.Frame2.SuspendLayout()
		Me.Fra26A.SuspendLayout()
		Me._SSTab1_TabPage2.SuspendLayout()
		Me.Frame4.SuspendLayout()
		Me._SSTab1_TabPage3.SuspendLayout()
		Me.Frame5.SuspendLayout()
		Me._SSTab1_TabPage4.SuspendLayout()
		Me.Frame6.SuspendLayout()
		Me._SSTab1_TabPage5.SuspendLayout()
		Me.Frame7.SuspendLayout()
		Me._SSTab1_TabPage6.SuspendLayout()
		Me.Frame8.SuspendLayout()
		Me._SSTab1_TabPage7.SuspendLayout()
		Me.Frame3.SuspendLayout()
		Me.FraMovement.SuspendLayout()
		Me.SuspendLayout()
		Me.ToolTip1.Active = True
		CType(Me.SprdView3, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.SprdView4A, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.SprdView4B, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.SprdView5A, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.SprdView5B, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.SprdView6A, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.SprdView6B, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.SprdView7A, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
		Me.Text = "Form No. 26"
		Me.ClientSize = New System.Drawing.Size(671, 392)
		Me.Location = New System.Drawing.Point(3, 22)
		Me.Icon = CType(resources.GetObject("frmTDSForm26.Icon"), System.Drawing.Icon)
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
		Me.Name = "frmTDSForm26"
		Me.SSTab1.Size = New System.Drawing.Size(671, 349)
		Me.SSTab1.Location = New System.Drawing.Point(0, 0)
		Me.SSTab1.TabIndex = 0
		Me.SSTab1.SelectedIndex = 4
		Me.SSTab1.ItemSize = New System.Drawing.Size(42, 18)
		Me.SSTab1.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.SSTab1.Name = "SSTab1"
		Me._SSTab1_TabPage0.Text = "Tab 0"
		Me.Frame1.Size = New System.Drawing.Size(661, 321)
		Me.Frame1.Location = New System.Drawing.Point(6, 24)
		Me.Frame1.TabIndex = 24
		Me.Frame1.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame1.BackColor = System.Drawing.SystemColors.Control
		Me.Frame1.Enabled = True
		Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame1.Visible = True
		Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame1.Name = "Frame1"
		Me.ChkNo.Text = "No"
		Me.ChkNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.ChkNo.Size = New System.Drawing.Size(67, 19)
		Me.ChkNo.Location = New System.Drawing.Point(258, 298)
		Me.ChkNo.TabIndex = 50
		Me.ChkNo.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.ChkNo.FlatStyle = System.Windows.Forms.FlatStyle.Standard
		Me.ChkNo.BackColor = System.Drawing.SystemColors.Control
		Me.ChkNo.CausesValidation = True
		Me.ChkNo.Enabled = True
		Me.ChkNo.ForeColor = System.Drawing.SystemColors.ControlText
		Me.ChkNo.Cursor = System.Windows.Forms.Cursors.Default
		Me.ChkNo.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.ChkNo.Appearance = System.Windows.Forms.Appearance.Normal
		Me.ChkNo.TabStop = True
		Me.ChkNo.CheckState = System.Windows.Forms.CheckState.Unchecked
		Me.ChkNo.Visible = True
		Me.ChkNo.Name = "ChkNo"
		Me.ChkYes.Text = "Yes"
		Me.ChkYes.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.ChkYes.Size = New System.Drawing.Size(69, 15)
		Me.ChkYes.Location = New System.Drawing.Point(162, 300)
		Me.ChkYes.TabIndex = 49
		Me.ChkYes.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.ChkYes.FlatStyle = System.Windows.Forms.FlatStyle.Standard
		Me.ChkYes.BackColor = System.Drawing.SystemColors.Control
		Me.ChkYes.CausesValidation = True
		Me.ChkYes.Enabled = True
		Me.ChkYes.ForeColor = System.Drawing.SystemColors.ControlText
		Me.ChkYes.Cursor = System.Windows.Forms.Cursors.Default
		Me.ChkYes.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.ChkYes.Appearance = System.Windows.Forms.Appearance.Normal
		Me.ChkYes.TabStop = True
		Me.ChkYes.CheckState = System.Windows.Forms.CheckState.Unchecked
		Me.ChkYes.Visible = True
		Me.ChkYes.Name = "ChkYes"
		Me.txtPinCode.AutoSize = False
		Me.txtPinCode.Size = New System.Drawing.Size(101, 19)
		Me.txtPinCode.Location = New System.Drawing.Point(555, 242)
		Me.txtPinCode.TabIndex = 46
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
		Me.txtState.AutoSize = False
		Me.txtState.Size = New System.Drawing.Size(179, 19)
		Me.txtState.Location = New System.Drawing.Point(215, 242)
		Me.txtState.TabIndex = 44
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
		Me.txtTown.AutoSize = False
		Me.txtTown.Size = New System.Drawing.Size(441, 19)
		Me.txtTown.Location = New System.Drawing.Point(215, 222)
		Me.txtTown.TabIndex = 42
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
		Me.txtArea.AutoSize = False
		Me.txtArea.Size = New System.Drawing.Size(441, 19)
		Me.txtArea.Location = New System.Drawing.Point(215, 202)
		Me.txtArea.TabIndex = 40
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
		Me.txtRoad.AutoSize = False
		Me.txtRoad.Size = New System.Drawing.Size(441, 19)
		Me.txtRoad.Location = New System.Drawing.Point(215, 182)
		Me.txtRoad.TabIndex = 38
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
		Me.txtBuilding.AutoSize = False
		Me.txtBuilding.Size = New System.Drawing.Size(441, 19)
		Me.txtBuilding.Location = New System.Drawing.Point(215, 162)
		Me.txtBuilding.TabIndex = 36
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
		Me.txtFlat.AutoSize = False
		Me.txtFlat.Size = New System.Drawing.Size(441, 19)
		Me.txtFlat.Location = New System.Drawing.Point(215, 142)
		Me.txtFlat.TabIndex = 34
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
		Me.txtCompanyName.AutoSize = False
		Me.txtCompanyName.Size = New System.Drawing.Size(441, 19)
		Me.txtCompanyName.Location = New System.Drawing.Point(215, 110)
		Me.txtCompanyName.TabIndex = 31
		Me.txtCompanyName.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtCompanyName.AcceptsReturn = True
		Me.txtCompanyName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtCompanyName.BackColor = System.Drawing.SystemColors.Window
		Me.txtCompanyName.CausesValidation = True
		Me.txtCompanyName.Enabled = True
		Me.txtCompanyName.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtCompanyName.HideSelection = True
		Me.txtCompanyName.ReadOnly = False
		Me.txtCompanyName.Maxlength = 0
		Me.txtCompanyName.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtCompanyName.MultiLine = False
		Me.txtCompanyName.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtCompanyName.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtCompanyName.TabStop = True
		Me.txtCompanyName.Visible = True
		Me.txtCompanyName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtCompanyName.Name = "txtCompanyName"
		Me.txtPANNo.AutoSize = False
		Me.txtPANNo.Size = New System.Drawing.Size(425, 19)
		Me.txtPANNo.Location = New System.Drawing.Point(234, 70)
		Me.txtPANNo.TabIndex = 28
		Me.txtPANNo.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtPANNo.AcceptsReturn = True
		Me.txtPANNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtPANNo.BackColor = System.Drawing.SystemColors.Window
		Me.txtPANNo.CausesValidation = True
		Me.txtPANNo.Enabled = True
		Me.txtPANNo.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtPANNo.HideSelection = True
		Me.txtPANNo.ReadOnly = False
		Me.txtPANNo.Maxlength = 0
		Me.txtPANNo.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtPANNo.MultiLine = False
		Me.txtPANNo.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtPANNo.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtPANNo.TabStop = True
		Me.txtPANNo.Visible = True
		Me.txtPANNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtPANNo.Name = "txtPANNo"
		Me.txtTDSNo.AutoSize = False
		Me.txtTDSNo.Size = New System.Drawing.Size(425, 19)
		Me.txtTDSNo.Location = New System.Drawing.Point(234, 50)
		Me.txtTDSNo.TabIndex = 26
		Me.txtTDSNo.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtTDSNo.AcceptsReturn = True
		Me.txtTDSNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtTDSNo.BackColor = System.Drawing.SystemColors.Window
		Me.txtTDSNo.CausesValidation = True
		Me.txtTDSNo.Enabled = True
		Me.txtTDSNo.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtTDSNo.HideSelection = True
		Me.txtTDSNo.ReadOnly = False
		Me.txtTDSNo.Maxlength = 0
		Me.txtTDSNo.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtTDSNo.MultiLine = False
		Me.txtTDSNo.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtTDSNo.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtTDSNo.TabStop = True
		Me.txtTDSNo.Visible = True
		Me.txtTDSNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtTDSNo.Name = "txtTDSNo"
		Me.lblTile.TextAlign = System.Drawing.ContentAlignment.TopCenter
		Me.lblTile.Text = "Title"
		Me.lblTile.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblTile.Size = New System.Drawing.Size(655, 37)
		Me.lblTile.Location = New System.Drawing.Point(2, 8)
		Me.lblTile.TabIndex = 57
		Me.lblTile.BackColor = System.Drawing.SystemColors.Control
		Me.lblTile.Enabled = True
		Me.lblTile.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblTile.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblTile.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblTile.UseMnemonic = True
		Me.lblTile.Visible = True
		Me.lblTile.AutoSize = False
		Me.lblTile.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.lblTile.Name = "lblTile"
		Me.Label24.Text = "         Tick as applicable"
		Me.Label24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label24.Size = New System.Drawing.Size(141, 13)
		Me.Label24.Location = New System.Drawing.Point(8, 300)
		Me.Label24.TabIndex = 48
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
		Me.lbl2C.Text = "    (C) Has address of the person responsible for paying any sum referred to in section 194J changed since     submitting the last return. :"
		Me.lbl2C.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lbl2C.Size = New System.Drawing.Size(648, 26)
		Me.lbl2C.Location = New System.Drawing.Point(8, 268)
		Me.lbl2C.TabIndex = 47
		Me.lbl2C.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.lbl2C.BackColor = System.Drawing.SystemColors.Control
		Me.lbl2C.Enabled = True
		Me.lbl2C.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lbl2C.Cursor = System.Windows.Forms.Cursors.Default
		Me.lbl2C.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lbl2C.UseMnemonic = True
		Me.lbl2C.Visible = True
		Me.lbl2C.AutoSize = True
		Me.lbl2C.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lbl2C.Name = "lbl2C"
		Me.Label12.Text = "Pin Code :"
		Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label12.Size = New System.Drawing.Size(60, 13)
		Me.Label12.Location = New System.Drawing.Point(486, 244)
		Me.Label12.TabIndex = 45
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
		Me.Label11.Text = "         State :"
		Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label11.Size = New System.Drawing.Size(75, 13)
		Me.Label11.Location = New System.Drawing.Point(8, 244)
		Me.Label11.TabIndex = 43
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
		Me.Label10.Text = "         Town / City / District :"
		Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label10.Size = New System.Drawing.Size(165, 13)
		Me.Label10.Location = New System.Drawing.Point(8, 224)
		Me.Label10.TabIndex = 41
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
		Me.Label9.Text = "         Area / Locality :"
		Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label9.Size = New System.Drawing.Size(129, 13)
		Me.Label9.Location = New System.Drawing.Point(8, 204)
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
		Me.Label8.Text = "         Road / Street / Lane :"
		Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label8.Size = New System.Drawing.Size(165, 13)
		Me.Label8.Location = New System.Drawing.Point(8, 184)
		Me.Label8.TabIndex = 37
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
		Me.Label7.Text = "         Name of Premises / Building :"
		Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label7.Size = New System.Drawing.Size(205, 13)
		Me.Label7.Location = New System.Drawing.Point(8, 164)
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
		Me.Label6.Text = "         Flat / Door / Block No. :"
		Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label6.Size = New System.Drawing.Size(177, 13)
		Me.Label6.Location = New System.Drawing.Point(8, 146)
		Me.Label6.TabIndex = 33
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
		Me.Label5.Text = "    (b) Address :"
		Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label5.Size = New System.Drawing.Size(89, 13)
		Me.Label5.Location = New System.Drawing.Point(8, 130)
		Me.Label5.TabIndex = 32
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
		Me.Label4.Text = "    (a) Name / Designation :"
		Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label4.Size = New System.Drawing.Size(157, 13)
		Me.Label4.Location = New System.Drawing.Point(8, 114)
		Me.Label4.TabIndex = 30
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
		Me.Label3.Text = "2. Details of the person responsible for paying any sum referred to in section 194J :"
		Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label3.Size = New System.Drawing.Size(473, 13)
		Me.Label3.Location = New System.Drawing.Point(8, 94)
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
		Me.Label2.Text = "    (b) Permanent Account Number :"
		Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label2.Size = New System.Drawing.Size(202, 13)
		Me.Label2.Location = New System.Drawing.Point(8, 72)
		Me.Label2.TabIndex = 27
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
		Me.Label1.Text = "1. (a) Tax Deduction Account Number :"
		Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.Size = New System.Drawing.Size(224, 13)
		Me.Label1.Location = New System.Drawing.Point(8, 52)
		Me.Label1.TabIndex = 25
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
		Me._SSTab1_TabPage1.Text = "Tab 1"
		Me.Frame2.Size = New System.Drawing.Size(661, 321)
		Me.Frame2.Location = New System.Drawing.Point(6, 24)
		Me.Frame2.TabIndex = 21
		Me.Frame2.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame2.BackColor = System.Drawing.SystemColors.Control
		Me.Frame2.Enabled = True
		Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame2.Visible = True
		Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame2.Name = "Frame2"
		SprdView3.OcxState = CType(resources.GetObject("SprdView3.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SprdView3.Size = New System.Drawing.Size(655, 173)
		Me.SprdView3.Location = New System.Drawing.Point(2, 40)
		Me.SprdView3.TabIndex = 23
		Me.SprdView3.Name = "SprdView3"
		Me.Fra26A.Size = New System.Drawing.Size(661, 113)
		Me.Fra26A.Location = New System.Drawing.Point(0, 208)
		Me.Fra26A.TabIndex = 62
		Me.Fra26A.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Fra26A.BackColor = System.Drawing.SystemColors.Control
		Me.Fra26A.Enabled = True
		Me.Fra26A.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Fra26A.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Fra26A.Visible = True
		Me.Fra26A.Padding = New System.Windows.Forms.Padding(0)
		Me.Fra26A.Name = "Fra26A"
		Me.txt26A4E.AutoSize = False
		Me.txt26A4E.Size = New System.Drawing.Size(133, 19)
		Me.txt26A4E.Location = New System.Drawing.Point(524, 90)
		Me.txt26A4E.TabIndex = 72
		Me.txt26A4E.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txt26A4E.AcceptsReturn = True
		Me.txt26A4E.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txt26A4E.BackColor = System.Drawing.SystemColors.Window
		Me.txt26A4E.CausesValidation = True
		Me.txt26A4E.Enabled = True
		Me.txt26A4E.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txt26A4E.HideSelection = True
		Me.txt26A4E.ReadOnly = False
		Me.txt26A4E.Maxlength = 0
		Me.txt26A4E.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txt26A4E.MultiLine = False
		Me.txt26A4E.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txt26A4E.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txt26A4E.TabStop = True
		Me.txt26A4E.Visible = True
		Me.txt26A4E.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txt26A4E.Name = "txt26A4E"
		Me.txt26A4D.AutoSize = False
		Me.txt26A4D.Size = New System.Drawing.Size(133, 19)
		Me.txt26A4D.Location = New System.Drawing.Point(524, 70)
		Me.txt26A4D.TabIndex = 71
		Me.txt26A4D.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txt26A4D.AcceptsReturn = True
		Me.txt26A4D.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txt26A4D.BackColor = System.Drawing.SystemColors.Window
		Me.txt26A4D.CausesValidation = True
		Me.txt26A4D.Enabled = True
		Me.txt26A4D.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txt26A4D.HideSelection = True
		Me.txt26A4D.ReadOnly = False
		Me.txt26A4D.Maxlength = 0
		Me.txt26A4D.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txt26A4D.MultiLine = False
		Me.txt26A4D.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txt26A4D.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txt26A4D.TabStop = True
		Me.txt26A4D.Visible = True
		Me.txt26A4D.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txt26A4D.Name = "txt26A4D"
		Me.txt26A4C.AutoSize = False
		Me.txt26A4C.Size = New System.Drawing.Size(133, 19)
		Me.txt26A4C.Location = New System.Drawing.Point(524, 50)
		Me.txt26A4C.TabIndex = 70
		Me.txt26A4C.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txt26A4C.AcceptsReturn = True
		Me.txt26A4C.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txt26A4C.BackColor = System.Drawing.SystemColors.Window
		Me.txt26A4C.CausesValidation = True
		Me.txt26A4C.Enabled = True
		Me.txt26A4C.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txt26A4C.HideSelection = True
		Me.txt26A4C.ReadOnly = False
		Me.txt26A4C.Maxlength = 0
		Me.txt26A4C.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txt26A4C.MultiLine = False
		Me.txt26A4C.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txt26A4C.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txt26A4C.TabStop = True
		Me.txt26A4C.Visible = True
		Me.txt26A4C.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txt26A4C.Name = "txt26A4C"
		Me.txt26A4B.AutoSize = False
		Me.txt26A4B.Size = New System.Drawing.Size(133, 19)
		Me.txt26A4B.Location = New System.Drawing.Point(524, 30)
		Me.txt26A4B.TabIndex = 69
		Me.txt26A4B.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txt26A4B.AcceptsReturn = True
		Me.txt26A4B.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txt26A4B.BackColor = System.Drawing.SystemColors.Window
		Me.txt26A4B.CausesValidation = True
		Me.txt26A4B.Enabled = True
		Me.txt26A4B.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txt26A4B.HideSelection = True
		Me.txt26A4B.ReadOnly = False
		Me.txt26A4B.Maxlength = 0
		Me.txt26A4B.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txt26A4B.MultiLine = False
		Me.txt26A4B.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txt26A4B.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txt26A4B.TabStop = True
		Me.txt26A4B.Visible = True
		Me.txt26A4B.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txt26A4B.Name = "txt26A4B"
		Me.txt26A4A.AutoSize = False
		Me.txt26A4A.Size = New System.Drawing.Size(133, 19)
		Me.txt26A4A.Location = New System.Drawing.Point(524, 10)
		Me.txt26A4A.TabIndex = 68
		Me.txt26A4A.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txt26A4A.AcceptsReturn = True
		Me.txt26A4A.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txt26A4A.BackColor = System.Drawing.SystemColors.Window
		Me.txt26A4A.CausesValidation = True
		Me.txt26A4A.Enabled = True
		Me.txt26A4A.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txt26A4A.HideSelection = True
		Me.txt26A4A.ReadOnly = False
		Me.txt26A4A.Maxlength = 0
		Me.txt26A4A.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txt26A4A.MultiLine = False
		Me.txt26A4A.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txt26A4A.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txt26A4A.TabStop = True
		Me.txt26A4A.Visible = True
		Me.txt26A4A.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txt26A4A.Name = "txt26A4A"
		Me.lbl26A4E.Size = New System.Drawing.Size(513, 19)
		Me.lbl26A4E.Location = New System.Drawing.Point(8, 90)
		Me.lbl26A4E.TabIndex = 67
		Me.lbl26A4E.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lbl26A4E.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.lbl26A4E.BackColor = System.Drawing.Color.Transparent
		Me.lbl26A4E.Enabled = True
		Me.lbl26A4E.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lbl26A4E.Cursor = System.Windows.Forms.Cursors.Default
		Me.lbl26A4E.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lbl26A4E.UseMnemonic = True
		Me.lbl26A4E.Visible = True
		Me.lbl26A4E.AutoSize = False
		Me.lbl26A4E.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.lbl26A4E.Name = "lbl26A4E"
		Me.lbl26A4D.Size = New System.Drawing.Size(513, 19)
		Me.lbl26A4D.Location = New System.Drawing.Point(8, 70)
		Me.lbl26A4D.TabIndex = 66
		Me.lbl26A4D.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lbl26A4D.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.lbl26A4D.BackColor = System.Drawing.Color.Transparent
		Me.lbl26A4D.Enabled = True
		Me.lbl26A4D.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lbl26A4D.Cursor = System.Windows.Forms.Cursors.Default
		Me.lbl26A4D.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lbl26A4D.UseMnemonic = True
		Me.lbl26A4D.Visible = True
		Me.lbl26A4D.AutoSize = False
		Me.lbl26A4D.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.lbl26A4D.Name = "lbl26A4D"
		Me.lbl26A4C.Size = New System.Drawing.Size(513, 19)
		Me.lbl26A4C.Location = New System.Drawing.Point(8, 50)
		Me.lbl26A4C.TabIndex = 65
		Me.lbl26A4C.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lbl26A4C.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.lbl26A4C.BackColor = System.Drawing.Color.Transparent
		Me.lbl26A4C.Enabled = True
		Me.lbl26A4C.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lbl26A4C.Cursor = System.Windows.Forms.Cursors.Default
		Me.lbl26A4C.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lbl26A4C.UseMnemonic = True
		Me.lbl26A4C.Visible = True
		Me.lbl26A4C.AutoSize = False
		Me.lbl26A4C.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.lbl26A4C.Name = "lbl26A4C"
		Me.lbl26A4B.Size = New System.Drawing.Size(513, 19)
		Me.lbl26A4B.Location = New System.Drawing.Point(8, 30)
		Me.lbl26A4B.TabIndex = 64
		Me.lbl26A4B.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lbl26A4B.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.lbl26A4B.BackColor = System.Drawing.Color.Transparent
		Me.lbl26A4B.Enabled = True
		Me.lbl26A4B.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lbl26A4B.Cursor = System.Windows.Forms.Cursors.Default
		Me.lbl26A4B.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lbl26A4B.UseMnemonic = True
		Me.lbl26A4B.Visible = True
		Me.lbl26A4B.AutoSize = False
		Me.lbl26A4B.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.lbl26A4B.Name = "lbl26A4B"
		Me.lbl26A4A.Size = New System.Drawing.Size(513, 19)
		Me.lbl26A4A.Location = New System.Drawing.Point(8, 10)
		Me.lbl26A4A.TabIndex = 63
		Me.lbl26A4A.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lbl26A4A.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.lbl26A4A.BackColor = System.Drawing.Color.Transparent
		Me.lbl26A4A.Enabled = True
		Me.lbl26A4A.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lbl26A4A.Cursor = System.Windows.Forms.Cursors.Default
		Me.lbl26A4A.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lbl26A4A.UseMnemonic = True
		Me.lbl26A4A.Visible = True
		Me.lbl26A4A.AutoSize = False
		Me.lbl26A4A.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.lbl26A4A.Name = "lbl26A4A"
		Me.lbl3A.Text = "3. Details of fees for professional or technical services referred to in section 194J credited / paid and tax deducted thereon :"
		Me.lbl3A.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lbl3A.Size = New System.Drawing.Size(646, 26)
		Me.lbl3A.Location = New System.Drawing.Point(8, 12)
		Me.lbl3A.TabIndex = 22
		Me.lbl3A.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.lbl3A.BackColor = System.Drawing.SystemColors.Control
		Me.lbl3A.Enabled = True
		Me.lbl3A.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lbl3A.Cursor = System.Windows.Forms.Cursors.Default
		Me.lbl3A.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lbl3A.UseMnemonic = True
		Me.lbl3A.Visible = True
		Me.lbl3A.AutoSize = True
		Me.lbl3A.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lbl3A.Name = "lbl3A"
		Me._SSTab1_TabPage2.Text = "Tab 2"
		Me.Frame4.Size = New System.Drawing.Size(661, 321)
		Me.Frame4.Location = New System.Drawing.Point(6, 24)
		Me.Frame4.TabIndex = 15
		Me.Frame4.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame4.BackColor = System.Drawing.SystemColors.Control
		Me.Frame4.Enabled = True
		Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame4.Visible = True
		Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame4.Name = "Frame4"
		SprdView4A.OcxState = CType(resources.GetObject("SprdView4A.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SprdView4A.Size = New System.Drawing.Size(654, 111)
		Me.SprdView4A.Location = New System.Drawing.Point(4, 42)
		Me.SprdView4A.TabIndex = 18
		Me.SprdView4A.Name = "SprdView4A"
		SprdView4B.OcxState = CType(resources.GetObject("SprdView4B.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SprdView4B.Size = New System.Drawing.Size(654, 143)
		Me.SprdView4B.Location = New System.Drawing.Point(4, 174)
		Me.SprdView4B.TabIndex = 20
		Me.SprdView4B.Name = "SprdView4B"
		Me.lbl4B.Text = "    (b) By persons responsible for paying tax other than Central Government :"
		Me.lbl4B.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lbl4B.Size = New System.Drawing.Size(433, 13)
		Me.lbl4B.Location = New System.Drawing.Point(8, 156)
		Me.lbl4B.TabIndex = 19
		Me.lbl4B.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.lbl4B.BackColor = System.Drawing.SystemColors.Control
		Me.lbl4B.Enabled = True
		Me.lbl4B.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lbl4B.Cursor = System.Windows.Forms.Cursors.Default
		Me.lbl4B.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lbl4B.UseMnemonic = True
		Me.lbl4B.Visible = True
		Me.lbl4B.AutoSize = True
		Me.lbl4B.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lbl4B.Name = "lbl4B"
		Me.lbl4A.Text = "    (a) By or on behalf of Central Government :"
		Me.lbl4A.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lbl4A.Size = New System.Drawing.Size(261, 13)
		Me.lbl4A.Location = New System.Drawing.Point(8, 26)
		Me.lbl4A.TabIndex = 17
		Me.lbl4A.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.lbl4A.BackColor = System.Drawing.SystemColors.Control
		Me.lbl4A.Enabled = True
		Me.lbl4A.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lbl4A.Cursor = System.Windows.Forms.Cursors.Default
		Me.lbl4A.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lbl4A.UseMnemonic = True
		Me.lbl4A.Visible = True
		Me.lbl4A.AutoSize = True
		Me.lbl4A.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lbl4A.Name = "lbl4A"
		Me.lbl4.Text = "4. Details of tax paid to the credit of Central Government :"
		Me.lbl4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lbl4.Size = New System.Drawing.Size(331, 13)
		Me.lbl4.Location = New System.Drawing.Point(8, 12)
		Me.lbl4.TabIndex = 16
		Me.lbl4.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.lbl4.BackColor = System.Drawing.SystemColors.Control
		Me.lbl4.Enabled = True
		Me.lbl4.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lbl4.Cursor = System.Windows.Forms.Cursors.Default
		Me.lbl4.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lbl4.UseMnemonic = True
		Me.lbl4.Visible = True
		Me.lbl4.AutoSize = True
		Me.lbl4.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lbl4.Name = "lbl4"
		Me._SSTab1_TabPage3.Text = "Tab 3"
		Me.Frame5.Size = New System.Drawing.Size(661, 321)
		Me.Frame5.Location = New System.Drawing.Point(6, 24)
		Me.Frame5.TabIndex = 11
		Me.Frame5.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame5.BackColor = System.Drawing.SystemColors.Control
		Me.Frame5.Enabled = True
		Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame5.Visible = True
		Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame5.Name = "Frame5"
		SprdView5A.OcxState = CType(resources.GetObject("SprdView5A.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SprdView5A.Size = New System.Drawing.Size(654, 259)
		Me.SprdView5A.Location = New System.Drawing.Point(4, 60)
		Me.SprdView5A.TabIndex = 14
		Me.SprdView5A.Name = "SprdView5A"
		Me.lbl5A.Text = "    (a) In the Case of companies :"
		Me.lbl5A.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lbl5A.Size = New System.Drawing.Size(188, 13)
		Me.lbl5A.Location = New System.Drawing.Point(8, 44)
		Me.lbl5A.TabIndex = 13
		Me.lbl5A.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.lbl5A.BackColor = System.Drawing.SystemColors.Control
		Me.lbl5A.Enabled = True
		Me.lbl5A.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lbl5A.Cursor = System.Windows.Forms.Cursors.Default
		Me.lbl5A.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lbl5A.UseMnemonic = True
		Me.lbl5A.Visible = True
		Me.lbl5A.AutoSize = True
		Me.lbl5A.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lbl5A.Name = "lbl5A"
		Me.lbl5.Text = "5. Details of fees for professional or technical services referred to in section 194J credited / paid during the yearand of tax deducted at source at the prescribed rates in force :"
		Me.lbl5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lbl5.Size = New System.Drawing.Size(560, 25)
		Me.lbl5.Location = New System.Drawing.Point(8, 12)
		Me.lbl5.TabIndex = 12
		Me.lbl5.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.lbl5.BackColor = System.Drawing.SystemColors.Control
		Me.lbl5.Enabled = True
		Me.lbl5.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lbl5.Cursor = System.Windows.Forms.Cursors.Default
		Me.lbl5.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lbl5.UseMnemonic = True
		Me.lbl5.Visible = True
		Me.lbl5.AutoSize = True
		Me.lbl5.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lbl5.Name = "lbl5"
		Me._SSTab1_TabPage4.Text = "Tab 4"
		Me.Frame6.Size = New System.Drawing.Size(661, 321)
		Me.Frame6.Location = New System.Drawing.Point(6, 24)
		Me.Frame6.TabIndex = 8
		Me.Frame6.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame6.BackColor = System.Drawing.SystemColors.Control
		Me.Frame6.Enabled = True
		Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame6.Visible = True
		Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame6.Name = "Frame6"
		SprdView5B.OcxState = CType(resources.GetObject("SprdView5B.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SprdView5B.Size = New System.Drawing.Size(656, 291)
		Me.SprdView5B.Location = New System.Drawing.Point(2, 28)
		Me.SprdView5B.TabIndex = 10
		Me.SprdView5B.Name = "SprdView5B"
		Me.lbl5B.Text = "    (b) In the case of persons / payees other than companies :"
		Me.lbl5B.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lbl5B.Size = New System.Drawing.Size(351, 13)
		Me.lbl5B.Location = New System.Drawing.Point(8, 12)
		Me.lbl5B.TabIndex = 9
		Me.lbl5B.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.lbl5B.BackColor = System.Drawing.SystemColors.Control
		Me.lbl5B.Enabled = True
		Me.lbl5B.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lbl5B.Cursor = System.Windows.Forms.Cursors.Default
		Me.lbl5B.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lbl5B.UseMnemonic = True
		Me.lbl5B.Visible = True
		Me.lbl5B.AutoSize = True
		Me.lbl5B.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lbl5B.Name = "lbl5B"
		Me._SSTab1_TabPage5.Text = "Tab 5"
		Me.Frame7.Size = New System.Drawing.Size(661, 321)
		Me.Frame7.Location = New System.Drawing.Point(6, 24)
		Me.Frame7.TabIndex = 4
		Me.Frame7.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame7.BackColor = System.Drawing.SystemColors.Control
		Me.Frame7.Enabled = True
		Me.Frame7.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame7.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame7.Visible = True
		Me.Frame7.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame7.Name = "Frame7"
		SprdView6A.OcxState = CType(resources.GetObject("SprdView6A.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SprdView6A.Size = New System.Drawing.Size(656, 251)
		Me.SprdView6A.Location = New System.Drawing.Point(2, 68)
		Me.SprdView6A.TabIndex = 7
		Me.SprdView6A.Name = "SprdView6A"
		Me.lbl6A.Text = "    (a) In the Case of companies :"
		Me.lbl6A.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lbl6A.Size = New System.Drawing.Size(188, 13)
		Me.lbl6A.Location = New System.Drawing.Point(8, 52)
		Me.lbl6A.TabIndex = 6
		Me.lbl6A.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.lbl6A.BackColor = System.Drawing.SystemColors.Control
		Me.lbl6A.Enabled = True
		Me.lbl6A.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lbl6A.Cursor = System.Windows.Forms.Cursors.Default
		Me.lbl6A.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lbl6A.UseMnemonic = True
		Me.lbl6A.Visible = True
		Me.lbl6A.AutoSize = True
		Me.lbl6A.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lbl6A.Name = "lbl6A"
		Me.lbl6.Text = "6. Details of fees for professional or technical services Credited / paid during the year and of tax deducted at source at a lower rate or no tax deducted in accordance with the provisions of section 194J(2) :"
		Me.lbl6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lbl6.Size = New System.Drawing.Size(568, 40)
		Me.lbl6.Location = New System.Drawing.Point(8, 12)
		Me.lbl6.TabIndex = 5
		Me.lbl6.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.lbl6.BackColor = System.Drawing.SystemColors.Control
		Me.lbl6.Enabled = True
		Me.lbl6.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lbl6.Cursor = System.Windows.Forms.Cursors.Default
		Me.lbl6.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lbl6.UseMnemonic = True
		Me.lbl6.Visible = True
		Me.lbl6.AutoSize = True
		Me.lbl6.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lbl6.Name = "lbl6"
		Me._SSTab1_TabPage6.Text = "Tab 6"
		Me.Frame8.Size = New System.Drawing.Size(661, 321)
		Me.Frame8.Location = New System.Drawing.Point(6, 24)
		Me.Frame8.TabIndex = 1
		Me.Frame8.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame8.BackColor = System.Drawing.SystemColors.Control
		Me.Frame8.Enabled = True
		Me.Frame8.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame8.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame8.Visible = True
		Me.Frame8.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame8.Name = "Frame8"
		SprdView6B.OcxState = CType(resources.GetObject("SprdView6B.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SprdView6B.Size = New System.Drawing.Size(656, 291)
		Me.SprdView6B.Location = New System.Drawing.Point(2, 28)
		Me.SprdView6B.TabIndex = 3
		Me.SprdView6B.Name = "SprdView6B"
		Me.lbl6B.Text = "    (b) In the case of persons / payees other than companies :"
		Me.lbl6B.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lbl6B.Size = New System.Drawing.Size(351, 13)
		Me.lbl6B.Location = New System.Drawing.Point(8, 12)
		Me.lbl6B.TabIndex = 2
		Me.lbl6B.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.lbl6B.BackColor = System.Drawing.SystemColors.Control
		Me.lbl6B.Enabled = True
		Me.lbl6B.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lbl6B.Cursor = System.Windows.Forms.Cursors.Default
		Me.lbl6B.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lbl6B.UseMnemonic = True
		Me.lbl6B.Visible = True
		Me.lbl6B.AutoSize = True
		Me.lbl6B.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lbl6B.Name = "lbl6B"
		Me._SSTab1_TabPage7.Text = "Tab 7"
		Me.Frame3.Size = New System.Drawing.Size(661, 321)
		Me.Frame3.Location = New System.Drawing.Point(6, 24)
		Me.Frame3.TabIndex = 58
		Me.Frame3.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame3.BackColor = System.Drawing.SystemColors.Control
		Me.Frame3.Enabled = True
		Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame3.Visible = True
		Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame3.Name = "Frame3"
		SprdView7A.OcxState = CType(resources.GetObject("SprdView7A.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SprdView7A.Size = New System.Drawing.Size(656, 251)
		Me.SprdView7A.Location = New System.Drawing.Point(2, 68)
		Me.SprdView7A.TabIndex = 59
		Me.SprdView7A.Name = "SprdView7A"
		Me.lbl7.Text = "6. Details of fees for professional or technical services Credited / paid during the year and of tax deducted at source at a lower rate or no tax deducted in accordance with the provisions of section 194J(2) :"
		Me.lbl7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lbl7.Size = New System.Drawing.Size(568, 40)
		Me.lbl7.Location = New System.Drawing.Point(8, 12)
		Me.lbl7.TabIndex = 61
		Me.lbl7.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.lbl7.BackColor = System.Drawing.SystemColors.Control
		Me.lbl7.Enabled = True
		Me.lbl7.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lbl7.Cursor = System.Windows.Forms.Cursors.Default
		Me.lbl7.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lbl7.UseMnemonic = True
		Me.lbl7.Visible = True
		Me.lbl7.AutoSize = True
		Me.lbl7.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lbl7.Name = "lbl7"
		Me.lbl7A.Text = "    (a) In the Case of companies :"
		Me.lbl7A.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lbl7A.Size = New System.Drawing.Size(188, 13)
		Me.lbl7A.Location = New System.Drawing.Point(8, 52)
		Me.lbl7A.TabIndex = 60
		Me.lbl7A.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.lbl7A.BackColor = System.Drawing.SystemColors.Control
		Me.lbl7A.Enabled = True
		Me.lbl7A.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lbl7A.Cursor = System.Windows.Forms.Cursors.Default
		Me.lbl7A.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lbl7A.UseMnemonic = True
		Me.lbl7A.Visible = True
		Me.lbl7A.AutoSize = True
		Me.lbl7A.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lbl7A.Name = "lbl7A"
		Me.FraMovement.Size = New System.Drawing.Size(671, 49)
		Me.FraMovement.Location = New System.Drawing.Point(0, 342)
		Me.FraMovement.TabIndex = 51
		Me.FraMovement.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
		Me.FraMovement.Enabled = True
		Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
		Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.FraMovement.Visible = True
		Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
		Me.FraMovement.Name = "FraMovement"
		Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.CmdPreview.Text = "Pre&view"
		Me.CmdPreview.Enabled = False
		Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
		Me.CmdPreview.Location = New System.Drawing.Point(533, 9)
		Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
		Me.CmdPreview.TabIndex = 54
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
		Me.cmdPrint.TabIndex = 53
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
		Me.cmdClose.TabIndex = 55
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
		Me.cmdShow.Location = New System.Drawing.Point(400, 9)
		Me.cmdShow.Image = CType(resources.GetObject("cmdShow.Image"), System.Drawing.Image)
		Me.cmdShow.TabIndex = 52
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
		Me.lblFormType.Text = "lblFormType"
		Me.lblFormType.Size = New System.Drawing.Size(57, 13)
		Me.lblFormType.Location = New System.Drawing.Point(368, 14)
		Me.lblFormType.TabIndex = 56
		Me.lblFormType.Visible = False
		Me.lblFormType.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblFormType.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.lblFormType.BackColor = System.Drawing.SystemColors.Control
		Me.lblFormType.Enabled = True
		Me.lblFormType.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblFormType.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblFormType.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblFormType.UseMnemonic = True
		Me.lblFormType.AutoSize = True
		Me.lblFormType.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lblFormType.Name = "lblFormType"
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
		CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SprdView7A, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SprdView6B, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SprdView6A, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SprdView5B, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SprdView5A, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SprdView4B, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SprdView4A, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SprdView3, System.ComponentModel.ISupportInitialize).EndInit()
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
		Me.SSTab1.Controls.Add(_SSTab1_TabPage4)
		Me.SSTab1.Controls.Add(_SSTab1_TabPage5)
		Me.SSTab1.Controls.Add(_SSTab1_TabPage6)
		Me.SSTab1.Controls.Add(_SSTab1_TabPage7)
		Me._SSTab1_TabPage0.Controls.Add(Frame1)
		Me.Frame1.Controls.Add(ChkNo)
		Me.Frame1.Controls.Add(ChkYes)
		Me.Frame1.Controls.Add(txtPinCode)
		Me.Frame1.Controls.Add(txtState)
		Me.Frame1.Controls.Add(txtTown)
		Me.Frame1.Controls.Add(txtArea)
		Me.Frame1.Controls.Add(txtRoad)
		Me.Frame1.Controls.Add(txtBuilding)
		Me.Frame1.Controls.Add(txtFlat)
		Me.Frame1.Controls.Add(txtCompanyName)
		Me.Frame1.Controls.Add(txtPANNo)
		Me.Frame1.Controls.Add(txtTDSNo)
		Me.Frame1.Controls.Add(lblTile)
		Me.Frame1.Controls.Add(Label24)
		Me.Frame1.Controls.Add(lbl2C)
		Me.Frame1.Controls.Add(Label12)
		Me.Frame1.Controls.Add(Label11)
		Me.Frame1.Controls.Add(Label10)
		Me.Frame1.Controls.Add(Label9)
		Me.Frame1.Controls.Add(Label8)
		Me.Frame1.Controls.Add(Label7)
		Me.Frame1.Controls.Add(Label6)
		Me.Frame1.Controls.Add(Label5)
		Me.Frame1.Controls.Add(Label4)
		Me.Frame1.Controls.Add(Label3)
		Me.Frame1.Controls.Add(Label2)
		Me.Frame1.Controls.Add(Label1)
		Me._SSTab1_TabPage1.Controls.Add(Frame2)
		Me.Frame2.Controls.Add(SprdView3)
		Me.Frame2.Controls.Add(Fra26A)
		Me.Frame2.Controls.Add(lbl3A)
		Me.Fra26A.Controls.Add(txt26A4E)
		Me.Fra26A.Controls.Add(txt26A4D)
		Me.Fra26A.Controls.Add(txt26A4C)
		Me.Fra26A.Controls.Add(txt26A4B)
		Me.Fra26A.Controls.Add(txt26A4A)
		Me.Fra26A.Controls.Add(lbl26A4E)
		Me.Fra26A.Controls.Add(lbl26A4D)
		Me.Fra26A.Controls.Add(lbl26A4C)
		Me.Fra26A.Controls.Add(lbl26A4B)
		Me.Fra26A.Controls.Add(lbl26A4A)
		Me._SSTab1_TabPage2.Controls.Add(Frame4)
		Me.Frame4.Controls.Add(SprdView4A)
		Me.Frame4.Controls.Add(SprdView4B)
		Me.Frame4.Controls.Add(lbl4B)
		Me.Frame4.Controls.Add(lbl4A)
		Me.Frame4.Controls.Add(lbl4)
		Me._SSTab1_TabPage3.Controls.Add(Frame5)
		Me.Frame5.Controls.Add(SprdView5A)
		Me.Frame5.Controls.Add(lbl5A)
		Me.Frame5.Controls.Add(lbl5)
		Me._SSTab1_TabPage4.Controls.Add(Frame6)
		Me.Frame6.Controls.Add(SprdView5B)
		Me.Frame6.Controls.Add(lbl5B)
		Me._SSTab1_TabPage5.Controls.Add(Frame7)
		Me.Frame7.Controls.Add(SprdView6A)
		Me.Frame7.Controls.Add(lbl6A)
		Me.Frame7.Controls.Add(lbl6)
		Me._SSTab1_TabPage6.Controls.Add(Frame8)
		Me.Frame8.Controls.Add(SprdView6B)
		Me.Frame8.Controls.Add(lbl6B)
		Me._SSTab1_TabPage7.Controls.Add(Frame3)
		Me.Frame3.Controls.Add(SprdView7A)
		Me.Frame3.Controls.Add(lbl7)
		Me.Frame3.Controls.Add(lbl7A)
		Me.FraMovement.Controls.Add(CmdPreview)
		Me.FraMovement.Controls.Add(cmdPrint)
		Me.FraMovement.Controls.Add(cmdClose)
		Me.FraMovement.Controls.Add(cmdShow)
		Me.FraMovement.Controls.Add(Report1)
		Me.FraMovement.Controls.Add(lblFormType)
		Me.SSTab1.ResumeLayout(False)
		Me._SSTab1_TabPage0.ResumeLayout(False)
		Me.Frame1.ResumeLayout(False)
		Me._SSTab1_TabPage1.ResumeLayout(False)
		Me.Frame2.ResumeLayout(False)
		Me.Fra26A.ResumeLayout(False)
		Me._SSTab1_TabPage2.ResumeLayout(False)
		Me.Frame4.ResumeLayout(False)
		Me._SSTab1_TabPage3.ResumeLayout(False)
		Me.Frame5.ResumeLayout(False)
		Me._SSTab1_TabPage4.ResumeLayout(False)
		Me.Frame6.ResumeLayout(False)
		Me._SSTab1_TabPage5.ResumeLayout(False)
		Me.Frame7.ResumeLayout(False)
		Me._SSTab1_TabPage6.ResumeLayout(False)
		Me.Frame8.ResumeLayout(False)
		Me._SSTab1_TabPage7.ResumeLayout(False)
		Me.Frame3.ResumeLayout(False)
		Me.FraMovement.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()
	End Sub
#End Region 
#Region "Upgrade Support"
	Public Sub VB6_AddADODataBinding()
		SprdView7A.DataSource = CType(AData7A, MSDATASRC.DataSource)
		SprdView6B.DataSource = CType(AData6B, MSDATASRC.DataSource)
		SprdView6A.DataSource = CType(AData6A, MSDATASRC.DataSource)
		SprdView5B.DataSource = CType(AData5B, MSDATASRC.DataSource)
		SprdView5A.DataSource = CType(AData5A, MSDATASRC.DataSource)
		SprdView4B.DataSource = CType(AData1, MSDATASRC.DataSource)
	End Sub
	Public Sub VB6_RemoveADODataBinding()
		SprdView7A.DataSource = Nothing
		SprdView6B.DataSource = Nothing
		SprdView6A.DataSource = Nothing
		SprdView5B.DataSource = Nothing
		SprdView5A.DataSource = Nothing
		SprdView4B.DataSource = Nothing
	End Sub
#End Region 
End Class