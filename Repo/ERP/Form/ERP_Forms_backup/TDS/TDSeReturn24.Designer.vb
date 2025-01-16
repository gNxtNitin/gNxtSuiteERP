<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmTDSeReturn24
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
	Public WithEvents txtRundate As System.Windows.Forms.TextBox
	Public WithEvents txtDateFrom As System.Windows.Forms.MaskedTextBox
	Public WithEvents txtDateTo As System.Windows.Forms.MaskedTextBox
	Public WithEvents Lable11 As System.Windows.Forms.Label
	Public WithEvents Label16 As System.Windows.Forms.Label
	Public WithEvents Label17 As System.Windows.Forms.Label
	Public WithEvents Frame7 As System.Windows.Forms.GroupBox
	Public WithEvents txtPanNo As System.Windows.Forms.TextBox
	Public WithEvents txtTDSAcNo As System.Windows.Forms.TextBox
	Public WithEvents Label20 As System.Windows.Forms.Label
	Public WithEvents Label19 As System.Windows.Forms.Label
	Public WithEvents Label18 As System.Windows.Forms.Label
	Public WithEvents Frame3 As System.Windows.Forms.GroupBox
	Public WithEvents _SSTab1_TabPage0 As System.Windows.Forms.TabPage
	Public WithEvents _optResAddChanged_1 As System.Windows.Forms.RadioButton
	Public WithEvents _optResAddChanged_0 As System.Windows.Forms.RadioButton
	Public WithEvents Frame9 As System.Windows.Forms.GroupBox
	Public WithEvents _optAddressChange_1 As System.Windows.Forms.RadioButton
	Public WithEvents _optAddressChange_0 As System.Windows.Forms.RadioButton
	Public WithEvents Frame8 As System.Windows.Forms.GroupBox
	Public WithEvents txtPhone As System.Windows.Forms.TextBox
	Public WithEvents txtEmail As System.Windows.Forms.TextBox
	Public WithEvents txtDeductorType As System.Windows.Forms.TextBox
	Public WithEvents txtFlat As System.Windows.Forms.TextBox
	Public WithEvents txtBuilding As System.Windows.Forms.TextBox
	Public WithEvents txtRoad As System.Windows.Forms.TextBox
	Public WithEvents txtArea As System.Windows.Forms.TextBox
	Public WithEvents txtTown As System.Windows.Forms.TextBox
	Public WithEvents txtState As System.Windows.Forms.TextBox
	Public WithEvents txtPinCode As System.Windows.Forms.TextBox
	Public WithEvents txtPersonName As System.Windows.Forms.TextBox
	Public WithEvents Label8 As System.Windows.Forms.Label
	Public WithEvents Label4 As System.Windows.Forms.Label
	Public WithEvents Label25 As System.Windows.Forms.Label
	Public WithEvents Label23 As System.Windows.Forms.Label
	Public WithEvents Label21 As System.Windows.Forms.Label
	Public WithEvents Label7 As System.Windows.Forms.Label
	Public WithEvents Label6 As System.Windows.Forms.Label
	Public WithEvents Label3 As System.Windows.Forms.Label
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents Label9 As System.Windows.Forms.Label
	Public WithEvents Label10 As System.Windows.Forms.Label
	Public WithEvents Label11 As System.Windows.Forms.Label
	Public WithEvents Label12 As System.Windows.Forms.Label
	Public WithEvents Label5 As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents Frame2 As System.Windows.Forms.GroupBox
	Public WithEvents _SSTab1_TabPage1 As System.Windows.Forms.TabPage
	Public WithEvents txtDesg As System.Windows.Forms.TextBox
	Public WithEvents txtPersonName_p As System.Windows.Forms.TextBox
	Public WithEvents txtPinCode_p As System.Windows.Forms.TextBox
	Public WithEvents txtState_p As System.Windows.Forms.TextBox
	Public WithEvents txtTown_p As System.Windows.Forms.TextBox
	Public WithEvents txtArea_p As System.Windows.Forms.TextBox
	Public WithEvents txtRoad_p As System.Windows.Forms.TextBox
	Public WithEvents txtBuilding_p As System.Windows.Forms.TextBox
	Public WithEvents txtFlat_p As System.Windows.Forms.TextBox
	Public WithEvents txtEmail_p As System.Windows.Forms.TextBox
	Public WithEvents txtPhone_p As System.Windows.Forms.TextBox
	Public WithEvents Label13 As System.Windows.Forms.Label
	Public WithEvents Label37 As System.Windows.Forms.Label
	Public WithEvents Label36 As System.Windows.Forms.Label
	Public WithEvents Label35 As System.Windows.Forms.Label
	Public WithEvents Label34 As System.Windows.Forms.Label
	Public WithEvents Label33 As System.Windows.Forms.Label
	Public WithEvents Label32 As System.Windows.Forms.Label
	Public WithEvents Label31 As System.Windows.Forms.Label
	Public WithEvents Label30 As System.Windows.Forms.Label
	Public WithEvents Label29 As System.Windows.Forms.Label
	Public WithEvents Label26 As System.Windows.Forms.Label
	Public WithEvents Label24 As System.Windows.Forms.Label
	Public WithEvents Frame6 As System.Windows.Forms.GroupBox
	Public WithEvents _SSTab1_TabPage2 As System.Windows.Forms.TabPage
	Public WithEvents SprdView24 As AxFPSpreadADO.AxfpSpread
	Public WithEvents Frame1 As System.Windows.Forms.GroupBox
	Public WithEvents _SSTab1_TabPage3 As System.Windows.Forms.TabPage
	Public WithEvents SprdViewChallan As AxFPSpreadADO.AxfpSpread
	Public WithEvents Frame4 As System.Windows.Forms.GroupBox
	Public WithEvents _SSTab1_TabPage4 As System.Windows.Forms.TabPage
	Public WithEvents SprdViewAnnex As AxFPSpreadADO.AxfpSpread
	Public WithEvents Frame5 As System.Windows.Forms.GroupBox
	Public WithEvents _SSTab1_TabPage5 As System.Windows.Forms.TabPage
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
	Public WithEvents AData1 As VB6.ADODC
	Public WithEvents ADataAnnx As VB6.ADODC
	Public WithEvents AData26 As VB6.ADODC
	Public WithEvents optAddressChange As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
	Public WithEvents optResAddChanged As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmTDSeReturn24))
		Me.components = New System.ComponentModel.Container()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
		Me.SSTab1 = New System.Windows.Forms.TabControl
		Me._SSTab1_TabPage0 = New System.Windows.Forms.TabPage
		Me.Frame3 = New System.Windows.Forms.GroupBox
		Me.Frame7 = New System.Windows.Forms.GroupBox
		Me.txtRundate = New System.Windows.Forms.TextBox
		Me.txtDateFrom = New System.Windows.Forms.MaskedTextBox
		Me.txtDateTo = New System.Windows.Forms.MaskedTextBox
		Me.Lable11 = New System.Windows.Forms.Label
		Me.Label16 = New System.Windows.Forms.Label
		Me.Label17 = New System.Windows.Forms.Label
		Me.txtPanNo = New System.Windows.Forms.TextBox
		Me.txtTDSAcNo = New System.Windows.Forms.TextBox
		Me.Label20 = New System.Windows.Forms.Label
		Me.Label19 = New System.Windows.Forms.Label
		Me.Label18 = New System.Windows.Forms.Label
		Me._SSTab1_TabPage1 = New System.Windows.Forms.TabPage
		Me.Frame2 = New System.Windows.Forms.GroupBox
		Me.Frame9 = New System.Windows.Forms.GroupBox
		Me._optResAddChanged_1 = New System.Windows.Forms.RadioButton
		Me._optResAddChanged_0 = New System.Windows.Forms.RadioButton
		Me.Frame8 = New System.Windows.Forms.GroupBox
		Me._optAddressChange_1 = New System.Windows.Forms.RadioButton
		Me._optAddressChange_0 = New System.Windows.Forms.RadioButton
		Me.txtPhone = New System.Windows.Forms.TextBox
		Me.txtEmail = New System.Windows.Forms.TextBox
		Me.txtDeductorType = New System.Windows.Forms.TextBox
		Me.txtFlat = New System.Windows.Forms.TextBox
		Me.txtBuilding = New System.Windows.Forms.TextBox
		Me.txtRoad = New System.Windows.Forms.TextBox
		Me.txtArea = New System.Windows.Forms.TextBox
		Me.txtTown = New System.Windows.Forms.TextBox
		Me.txtState = New System.Windows.Forms.TextBox
		Me.txtPinCode = New System.Windows.Forms.TextBox
		Me.txtPersonName = New System.Windows.Forms.TextBox
		Me.Label8 = New System.Windows.Forms.Label
		Me.Label4 = New System.Windows.Forms.Label
		Me.Label25 = New System.Windows.Forms.Label
		Me.Label23 = New System.Windows.Forms.Label
		Me.Label21 = New System.Windows.Forms.Label
		Me.Label7 = New System.Windows.Forms.Label
		Me.Label6 = New System.Windows.Forms.Label
		Me.Label3 = New System.Windows.Forms.Label
		Me.Label2 = New System.Windows.Forms.Label
		Me.Label9 = New System.Windows.Forms.Label
		Me.Label10 = New System.Windows.Forms.Label
		Me.Label11 = New System.Windows.Forms.Label
		Me.Label12 = New System.Windows.Forms.Label
		Me.Label5 = New System.Windows.Forms.Label
		Me.Label1 = New System.Windows.Forms.Label
		Me._SSTab1_TabPage2 = New System.Windows.Forms.TabPage
		Me.Frame6 = New System.Windows.Forms.GroupBox
		Me.txtDesg = New System.Windows.Forms.TextBox
		Me.txtPersonName_p = New System.Windows.Forms.TextBox
		Me.txtPinCode_p = New System.Windows.Forms.TextBox
		Me.txtState_p = New System.Windows.Forms.TextBox
		Me.txtTown_p = New System.Windows.Forms.TextBox
		Me.txtArea_p = New System.Windows.Forms.TextBox
		Me.txtRoad_p = New System.Windows.Forms.TextBox
		Me.txtBuilding_p = New System.Windows.Forms.TextBox
		Me.txtFlat_p = New System.Windows.Forms.TextBox
		Me.txtEmail_p = New System.Windows.Forms.TextBox
		Me.txtPhone_p = New System.Windows.Forms.TextBox
		Me.Label13 = New System.Windows.Forms.Label
		Me.Label37 = New System.Windows.Forms.Label
		Me.Label36 = New System.Windows.Forms.Label
		Me.Label35 = New System.Windows.Forms.Label
		Me.Label34 = New System.Windows.Forms.Label
		Me.Label33 = New System.Windows.Forms.Label
		Me.Label32 = New System.Windows.Forms.Label
		Me.Label31 = New System.Windows.Forms.Label
		Me.Label30 = New System.Windows.Forms.Label
		Me.Label29 = New System.Windows.Forms.Label
		Me.Label26 = New System.Windows.Forms.Label
		Me.Label24 = New System.Windows.Forms.Label
		Me._SSTab1_TabPage3 = New System.Windows.Forms.TabPage
		Me.Frame1 = New System.Windows.Forms.GroupBox
		Me.SprdView24 = New AxFPSpreadADO.AxfpSpread
		Me._SSTab1_TabPage4 = New System.Windows.Forms.TabPage
		Me.Frame4 = New System.Windows.Forms.GroupBox
		Me.SprdViewChallan = New AxFPSpreadADO.AxfpSpread
		Me._SSTab1_TabPage5 = New System.Windows.Forms.TabPage
		Me.Frame5 = New System.Windows.Forms.GroupBox
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
		Me.AData1 = New VB6.ADODC
		Me.ADataAnnx = New VB6.ADODC
		Me.AData26 = New VB6.ADODC
		Me.optAddressChange = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(components)
		Me.optResAddChanged = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(components)
		Me.SSTab1.SuspendLayout()
		Me._SSTab1_TabPage0.SuspendLayout()
		Me.Frame3.SuspendLayout()
		Me.Frame7.SuspendLayout()
		Me._SSTab1_TabPage1.SuspendLayout()
		Me.Frame2.SuspendLayout()
		Me.Frame9.SuspendLayout()
		Me.Frame8.SuspendLayout()
		Me._SSTab1_TabPage2.SuspendLayout()
		Me.Frame6.SuspendLayout()
		Me._SSTab1_TabPage3.SuspendLayout()
		Me.Frame1.SuspendLayout()
		Me._SSTab1_TabPage4.SuspendLayout()
		Me.Frame4.SuspendLayout()
		Me._SSTab1_TabPage5.SuspendLayout()
		Me.Frame5.SuspendLayout()
		Me.FraMovement.SuspendLayout()
		Me.SuspendLayout()
		Me.ToolTip1.Active = True
		CType(Me.SprdView24, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.SprdViewChallan, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.SprdViewAnnex, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.optAddressChange, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.optResAddChanged, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
		Me.Text = "TDS e-Return 24"
		Me.ClientSize = New System.Drawing.Size(672, 394)
		Me.Location = New System.Drawing.Point(3, 22)
		Me.Icon = CType(resources.GetObject("frmTDSeReturn24.Icon"), System.Drawing.Icon)
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
		Me.Name = "frmTDSeReturn24"
		Me.SSTab1.Size = New System.Drawing.Size(671, 349)
		Me.SSTab1.Location = New System.Drawing.Point(0, 0)
		Me.SSTab1.TabIndex = 0
		Me.SSTab1.SelectedIndex = 1
		Me.SSTab1.ItemSize = New System.Drawing.Size(42, 22)
		Me.SSTab1.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.SSTab1.Name = "SSTab1"
		Me._SSTab1_TabPage0.Text = "Company Details 1"
		Me.Frame3.Size = New System.Drawing.Size(661, 319)
		Me.Frame3.Location = New System.Drawing.Point(4, 26)
		Me.Frame3.TabIndex = 6
		Me.Frame3.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame3.BackColor = System.Drawing.SystemColors.Control
		Me.Frame3.Enabled = True
		Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame3.Visible = True
		Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame3.Name = "Frame3"
		Me.Frame7.Size = New System.Drawing.Size(619, 77)
		Me.Frame7.Location = New System.Drawing.Point(14, 62)
		Me.Frame7.TabIndex = 63
		Me.Frame7.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame7.BackColor = System.Drawing.SystemColors.Control
		Me.Frame7.Enabled = True
		Me.Frame7.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame7.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame7.Visible = True
		Me.Frame7.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame7.Name = "Frame7"
		Me.txtRundate.AutoSize = False
		Me.txtRundate.Size = New System.Drawing.Size(85, 19)
		Me.txtRundate.Location = New System.Drawing.Point(229, 44)
		Me.txtRundate.TabIndex = 82
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
		Me.txtDateFrom.Location = New System.Drawing.Point(229, 14)
		Me.txtDateFrom.TabIndex = 64
		Me.txtDateFrom.MaxLength = 10
		Me.txtDateFrom.Mask = "##/##/####"
		Me.txtDateFrom.PromptChar = "_"
		Me.txtDateFrom.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtDateFrom.Name = "txtDateFrom"
		Me.txtDateTo.AllowPromptAsInput = False
		Me.txtDateTo.Size = New System.Drawing.Size(83, 21)
		Me.txtDateTo.Location = New System.Drawing.Point(441, 14)
		Me.txtDateTo.TabIndex = 65
		Me.txtDateTo.MaxLength = 10
		Me.txtDateTo.Mask = "##/##/####"
		Me.txtDateTo.PromptChar = "_"
		Me.txtDateTo.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtDateTo.Name = "txtDateTo"
		Me.Lable11.Text = "File Creation Date :"
		Me.Lable11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Lable11.Size = New System.Drawing.Size(111, 13)
		Me.Lable11.Location = New System.Drawing.Point(18, 44)
		Me.Lable11.TabIndex = 83
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
		Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.Label16.Text = "From : "
		Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label16.Size = New System.Drawing.Size(40, 13)
		Me.Label16.Location = New System.Drawing.Point(190, 18)
		Me.Label16.TabIndex = 67
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
		Me.Label17.Location = New System.Drawing.Point(414, 18)
		Me.Label17.TabIndex = 66
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
		Me.txtPanNo.AutoSize = False
		Me.txtPanNo.Size = New System.Drawing.Size(123, 19)
		Me.txtPanNo.Location = New System.Drawing.Point(351, 200)
		Me.txtPanNo.TabIndex = 15
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
		Me.txtTDSAcNo.Size = New System.Drawing.Size(123, 19)
		Me.txtTDSAcNo.Location = New System.Drawing.Point(351, 174)
		Me.txtTDSAcNo.TabIndex = 14
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
		Me.Label20.Text = "(b) Permanent A/c Number :"
		Me.Label20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label20.Size = New System.Drawing.Size(160, 13)
		Me.Label20.Location = New System.Drawing.Point(132, 202)
		Me.Label20.TabIndex = 13
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
		Me.Label19.Text = "(a) Tax Deduction A/c Number :"
		Me.Label19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label19.Size = New System.Drawing.Size(183, 13)
		Me.Label19.Location = New System.Drawing.Point(132, 176)
		Me.Label19.TabIndex = 12
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
		Me.Label18.Text = "1. "
		Me.Label18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label18.Size = New System.Drawing.Size(16, 13)
		Me.Label18.Location = New System.Drawing.Point(120, 176)
		Me.Label18.TabIndex = 11
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
		Me._SSTab1_TabPage1.Text = "Company Details 2"
		Me.Frame2.Size = New System.Drawing.Size(661, 319)
		Me.Frame2.Location = New System.Drawing.Point(4, 28)
		Me.Frame2.TabIndex = 16
		Me.Frame2.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame2.BackColor = System.Drawing.SystemColors.Control
		Me.Frame2.Enabled = True
		Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame2.Visible = True
		Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame2.Name = "Frame2"
		Me.Frame9.Text = "Person Responsible for paying Salary"
		Me.Frame9.Size = New System.Drawing.Size(227, 41)
		Me.Frame9.Location = New System.Drawing.Point(342, 276)
		Me.Frame9.TabIndex = 71
		Me.Frame9.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame9.BackColor = System.Drawing.SystemColors.Control
		Me.Frame9.Enabled = True
		Me.Frame9.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame9.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame9.Visible = True
		Me.Frame9.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame9.Name = "Frame9"
		Me._optResAddChanged_1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._optResAddChanged_1.Text = "No"
		Me._optResAddChanged_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me._optResAddChanged_1.Size = New System.Drawing.Size(69, 13)
		Me._optResAddChanged_1.Location = New System.Drawing.Point(140, 20)
		Me._optResAddChanged_1.TabIndex = 75
		Me._optResAddChanged_1.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._optResAddChanged_1.BackColor = System.Drawing.SystemColors.Control
		Me._optResAddChanged_1.CausesValidation = True
		Me._optResAddChanged_1.Enabled = True
		Me._optResAddChanged_1.ForeColor = System.Drawing.SystemColors.ControlText
		Me._optResAddChanged_1.Cursor = System.Windows.Forms.Cursors.Default
		Me._optResAddChanged_1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._optResAddChanged_1.Appearance = System.Windows.Forms.Appearance.Normal
		Me._optResAddChanged_1.TabStop = True
		Me._optResAddChanged_1.Checked = False
		Me._optResAddChanged_1.Visible = True
		Me._optResAddChanged_1.Name = "_optResAddChanged_1"
		Me._optResAddChanged_0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._optResAddChanged_0.Text = "Yes"
		Me._optResAddChanged_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me._optResAddChanged_0.Size = New System.Drawing.Size(67, 13)
		Me._optResAddChanged_0.Location = New System.Drawing.Point(52, 20)
		Me._optResAddChanged_0.TabIndex = 74
		Me._optResAddChanged_0.Checked = True
		Me._optResAddChanged_0.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._optResAddChanged_0.BackColor = System.Drawing.SystemColors.Control
		Me._optResAddChanged_0.CausesValidation = True
		Me._optResAddChanged_0.Enabled = True
		Me._optResAddChanged_0.ForeColor = System.Drawing.SystemColors.ControlText
		Me._optResAddChanged_0.Cursor = System.Windows.Forms.Cursors.Default
		Me._optResAddChanged_0.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._optResAddChanged_0.Appearance = System.Windows.Forms.Appearance.Normal
		Me._optResAddChanged_0.TabStop = True
		Me._optResAddChanged_0.Visible = True
		Me._optResAddChanged_0.Name = "_optResAddChanged_0"
		Me.Frame8.Text = "Employer"
		Me.Frame8.Size = New System.Drawing.Size(213, 41)
		Me.Frame8.Location = New System.Drawing.Point(100, 276)
		Me.Frame8.TabIndex = 70
		Me.Frame8.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame8.BackColor = System.Drawing.SystemColors.Control
		Me.Frame8.Enabled = True
		Me.Frame8.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame8.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame8.Visible = True
		Me.Frame8.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame8.Name = "Frame8"
		Me._optAddressChange_1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._optAddressChange_1.Text = "No"
		Me._optAddressChange_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me._optAddressChange_1.Size = New System.Drawing.Size(39, 13)
		Me._optAddressChange_1.Location = New System.Drawing.Point(140, 20)
		Me._optAddressChange_1.TabIndex = 73
		Me._optAddressChange_1.Checked = True
		Me._optAddressChange_1.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._optAddressChange_1.BackColor = System.Drawing.SystemColors.Control
		Me._optAddressChange_1.CausesValidation = True
		Me._optAddressChange_1.Enabled = True
		Me._optAddressChange_1.ForeColor = System.Drawing.SystemColors.ControlText
		Me._optAddressChange_1.Cursor = System.Windows.Forms.Cursors.Default
		Me._optAddressChange_1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._optAddressChange_1.Appearance = System.Windows.Forms.Appearance.Normal
		Me._optAddressChange_1.TabStop = True
		Me._optAddressChange_1.Visible = True
		Me._optAddressChange_1.Name = "_optAddressChange_1"
		Me._optAddressChange_0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._optAddressChange_0.Text = "Yes"
		Me._optAddressChange_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me._optAddressChange_0.Size = New System.Drawing.Size(55, 13)
		Me._optAddressChange_0.Location = New System.Drawing.Point(52, 20)
		Me._optAddressChange_0.TabIndex = 72
		Me._optAddressChange_0.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._optAddressChange_0.BackColor = System.Drawing.SystemColors.Control
		Me._optAddressChange_0.CausesValidation = True
		Me._optAddressChange_0.Enabled = True
		Me._optAddressChange_0.ForeColor = System.Drawing.SystemColors.ControlText
		Me._optAddressChange_0.Cursor = System.Windows.Forms.Cursors.Default
		Me._optAddressChange_0.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._optAddressChange_0.Appearance = System.Windows.Forms.Appearance.Normal
		Me._optAddressChange_0.TabStop = True
		Me._optAddressChange_0.Checked = False
		Me._optAddressChange_0.Visible = True
		Me._optAddressChange_0.Name = "_optAddressChange_0"
		Me.txtPhone.AutoSize = False
		Me.txtPhone.Size = New System.Drawing.Size(181, 19)
		Me.txtPhone.Location = New System.Drawing.Point(241, 224)
		Me.txtPhone.TabIndex = 38
		Me.txtPhone.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtPhone.AcceptsReturn = True
		Me.txtPhone.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtPhone.BackColor = System.Drawing.SystemColors.Window
		Me.txtPhone.CausesValidation = True
		Me.txtPhone.Enabled = True
		Me.txtPhone.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtPhone.HideSelection = True
		Me.txtPhone.ReadOnly = False
		Me.txtPhone.Maxlength = 0
		Me.txtPhone.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtPhone.MultiLine = False
		Me.txtPhone.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtPhone.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtPhone.TabStop = True
		Me.txtPhone.Visible = True
		Me.txtPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtPhone.Name = "txtPhone"
		Me.txtEmail.AutoSize = False
		Me.txtEmail.Size = New System.Drawing.Size(141, 19)
		Me.txtEmail.Location = New System.Drawing.Point(514, 224)
		Me.txtEmail.TabIndex = 37
		Me.txtEmail.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtEmail.AcceptsReturn = True
		Me.txtEmail.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtEmail.BackColor = System.Drawing.SystemColors.Window
		Me.txtEmail.CausesValidation = True
		Me.txtEmail.Enabled = True
		Me.txtEmail.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtEmail.HideSelection = True
		Me.txtEmail.ReadOnly = False
		Me.txtEmail.Maxlength = 0
		Me.txtEmail.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtEmail.MultiLine = False
		Me.txtEmail.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtEmail.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtEmail.TabStop = True
		Me.txtEmail.Visible = True
		Me.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtEmail.Name = "txtEmail"
		Me.txtDeductorType.AutoSize = False
		Me.txtDeductorType.Size = New System.Drawing.Size(415, 19)
		Me.txtDeductorType.Location = New System.Drawing.Point(241, 38)
		Me.txtDeductorType.TabIndex = 35
		Me.ToolTip1.SetToolTip(Me.txtDeductorType, "Press F1 For Help")
		Me.txtDeductorType.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtDeductorType.AcceptsReturn = True
		Me.txtDeductorType.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtDeductorType.BackColor = System.Drawing.SystemColors.Window
		Me.txtDeductorType.CausesValidation = True
		Me.txtDeductorType.Enabled = True
		Me.txtDeductorType.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtDeductorType.HideSelection = True
		Me.txtDeductorType.ReadOnly = False
		Me.txtDeductorType.Maxlength = 0
		Me.txtDeductorType.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtDeductorType.MultiLine = False
		Me.txtDeductorType.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtDeductorType.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtDeductorType.TabStop = True
		Me.txtDeductorType.Visible = True
		Me.txtDeductorType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtDeductorType.Name = "txtDeductorType"
		Me.txtFlat.AutoSize = False
		Me.txtFlat.Size = New System.Drawing.Size(415, 19)
		Me.txtFlat.Location = New System.Drawing.Point(241, 80)
		Me.txtFlat.TabIndex = 24
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
		Me.txtBuilding.Location = New System.Drawing.Point(241, 104)
		Me.txtBuilding.TabIndex = 23
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
		Me.txtRoad.Location = New System.Drawing.Point(241, 128)
		Me.txtRoad.TabIndex = 22
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
		Me.txtArea.Location = New System.Drawing.Point(241, 152)
		Me.txtArea.TabIndex = 21
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
		Me.txtTown.Location = New System.Drawing.Point(241, 176)
		Me.txtTown.TabIndex = 20
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
		Me.txtState.Location = New System.Drawing.Point(241, 200)
		Me.txtState.TabIndex = 19
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
		Me.txtPinCode.Size = New System.Drawing.Size(141, 19)
		Me.txtPinCode.Location = New System.Drawing.Point(514, 200)
		Me.txtPinCode.TabIndex = 18
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
		Me.txtPersonName.AutoSize = False
		Me.txtPersonName.Size = New System.Drawing.Size(415, 19)
		Me.txtPersonName.Location = New System.Drawing.Point(241, 14)
		Me.txtPersonName.TabIndex = 17
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
		Me.Label8.Text = "Has address of the employer / person responsible for paying salary changed since filing the last return Tick as applicable:"
		Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label8.Size = New System.Drawing.Size(632, 29)
		Me.Label8.Location = New System.Drawing.Point(24, 254)
		Me.Label8.TabIndex = 69
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
		Me.Label4.Text = "3."
		Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label4.Size = New System.Drawing.Size(12, 13)
		Me.Label4.Location = New System.Drawing.Point(12, 254)
		Me.Label4.TabIndex = 68
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
		Me.Label25.Text = "Telephone No. :"
		Me.Label25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label25.Size = New System.Drawing.Size(93, 13)
		Me.Label25.Location = New System.Drawing.Point(40, 226)
		Me.Label25.TabIndex = 40
		Me.Label25.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label25.BackColor = System.Drawing.SystemColors.Control
		Me.Label25.Enabled = True
		Me.Label25.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label25.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label25.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label25.UseMnemonic = True
		Me.Label25.Visible = True
		Me.Label25.AutoSize = True
		Me.Label25.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label25.Name = "Label25"
		Me.Label23.Text = "E-mail :"
		Me.Label23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label23.Size = New System.Drawing.Size(43, 13)
		Me.Label23.Location = New System.Drawing.Point(446, 224)
		Me.Label23.TabIndex = 39
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
		Me.Label21.Text = "(b) Type of Employer :"
		Me.Label21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label21.Size = New System.Drawing.Size(126, 13)
		Me.Label21.Location = New System.Drawing.Point(24, 40)
		Me.Label21.TabIndex = 36
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
		Me.Label7.Text = "2."
		Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label7.Size = New System.Drawing.Size(12, 13)
		Me.Label7.Location = New System.Drawing.Point(12, 16)
		Me.Label7.TabIndex = 34
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
		Me.Label6.Text = "Flat No. :"
		Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label6.Size = New System.Drawing.Size(54, 13)
		Me.Label6.Location = New System.Drawing.Point(42, 82)
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
		Me.Label3.Text = "Name of the Premises / Building :"
		Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label3.Size = New System.Drawing.Size(191, 13)
		Me.Label3.Location = New System.Drawing.Point(42, 106)
		Me.Label3.TabIndex = 32
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
		Me.Label2.Text = "Road / Street / Lane :"
		Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label2.Size = New System.Drawing.Size(129, 13)
		Me.Label2.Location = New System.Drawing.Point(42, 130)
		Me.Label2.TabIndex = 31
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
		Me.Label9.Text = "Area / Locality :"
		Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label9.Size = New System.Drawing.Size(93, 13)
		Me.Label9.Location = New System.Drawing.Point(42, 154)
		Me.Label9.TabIndex = 30
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
		Me.Label10.Location = New System.Drawing.Point(42, 178)
		Me.Label10.TabIndex = 29
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
		Me.Label11.Location = New System.Drawing.Point(42, 202)
		Me.Label11.TabIndex = 28
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
		Me.Label12.Location = New System.Drawing.Point(446, 202)
		Me.Label12.TabIndex = 27
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
		Me.Label5.Text = "(c) Address :"
		Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label5.Size = New System.Drawing.Size(73, 13)
		Me.Label5.Location = New System.Drawing.Point(24, 64)
		Me.Label5.TabIndex = 26
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
		Me.Label1.Text = "(a) Name of the Employer:"
		Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.Size = New System.Drawing.Size(148, 13)
		Me.Label1.Location = New System.Drawing.Point(24, 16)
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
		Me._SSTab1_TabPage2.Text = "Company Details 3"
		Me.Frame6.Size = New System.Drawing.Size(661, 319)
		Me.Frame6.Location = New System.Drawing.Point(4, 28)
		Me.Frame6.TabIndex = 41
		Me.Frame6.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame6.BackColor = System.Drawing.SystemColors.Control
		Me.Frame6.Enabled = True
		Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame6.Visible = True
		Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame6.Name = "Frame6"
		Me.txtDesg.AutoSize = False
		Me.txtDesg.Size = New System.Drawing.Size(415, 19)
		Me.txtDesg.Location = New System.Drawing.Point(241, 48)
		Me.txtDesg.TabIndex = 80
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
		Me.txtPersonName_p.AutoSize = False
		Me.txtPersonName_p.Size = New System.Drawing.Size(415, 19)
		Me.txtPersonName_p.Location = New System.Drawing.Point(241, 16)
		Me.txtPersonName_p.TabIndex = 51
		Me.ToolTip1.SetToolTip(Me.txtPersonName_p, "Press F1 For Help")
		Me.txtPersonName_p.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtPersonName_p.AcceptsReturn = True
		Me.txtPersonName_p.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtPersonName_p.BackColor = System.Drawing.SystemColors.Window
		Me.txtPersonName_p.CausesValidation = True
		Me.txtPersonName_p.Enabled = True
		Me.txtPersonName_p.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtPersonName_p.HideSelection = True
		Me.txtPersonName_p.ReadOnly = False
		Me.txtPersonName_p.Maxlength = 0
		Me.txtPersonName_p.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtPersonName_p.MultiLine = False
		Me.txtPersonName_p.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtPersonName_p.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtPersonName_p.TabStop = True
		Me.txtPersonName_p.Visible = True
		Me.txtPersonName_p.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtPersonName_p.Name = "txtPersonName_p"
		Me.txtPinCode_p.AutoSize = False
		Me.txtPinCode_p.Size = New System.Drawing.Size(101, 19)
		Me.txtPinCode_p.Location = New System.Drawing.Point(555, 208)
		Me.txtPinCode_p.TabIndex = 50
		Me.txtPinCode_p.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtPinCode_p.AcceptsReturn = True
		Me.txtPinCode_p.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtPinCode_p.BackColor = System.Drawing.SystemColors.Window
		Me.txtPinCode_p.CausesValidation = True
		Me.txtPinCode_p.Enabled = True
		Me.txtPinCode_p.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtPinCode_p.HideSelection = True
		Me.txtPinCode_p.ReadOnly = False
		Me.txtPinCode_p.Maxlength = 0
		Me.txtPinCode_p.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtPinCode_p.MultiLine = False
		Me.txtPinCode_p.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtPinCode_p.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtPinCode_p.TabStop = True
		Me.txtPinCode_p.Visible = True
		Me.txtPinCode_p.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtPinCode_p.Name = "txtPinCode_p"
		Me.txtState_p.AutoSize = False
		Me.txtState_p.Size = New System.Drawing.Size(179, 19)
		Me.txtState_p.Location = New System.Drawing.Point(241, 208)
		Me.txtState_p.TabIndex = 49
		Me.txtState_p.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtState_p.AcceptsReturn = True
		Me.txtState_p.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtState_p.BackColor = System.Drawing.SystemColors.Window
		Me.txtState_p.CausesValidation = True
		Me.txtState_p.Enabled = True
		Me.txtState_p.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtState_p.HideSelection = True
		Me.txtState_p.ReadOnly = False
		Me.txtState_p.Maxlength = 0
		Me.txtState_p.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtState_p.MultiLine = False
		Me.txtState_p.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtState_p.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtState_p.TabStop = True
		Me.txtState_p.Visible = True
		Me.txtState_p.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtState_p.Name = "txtState_p"
		Me.txtTown_p.AutoSize = False
		Me.txtTown_p.Size = New System.Drawing.Size(415, 19)
		Me.txtTown_p.Location = New System.Drawing.Point(241, 184)
		Me.txtTown_p.TabIndex = 48
		Me.txtTown_p.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtTown_p.AcceptsReturn = True
		Me.txtTown_p.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtTown_p.BackColor = System.Drawing.SystemColors.Window
		Me.txtTown_p.CausesValidation = True
		Me.txtTown_p.Enabled = True
		Me.txtTown_p.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtTown_p.HideSelection = True
		Me.txtTown_p.ReadOnly = False
		Me.txtTown_p.Maxlength = 0
		Me.txtTown_p.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtTown_p.MultiLine = False
		Me.txtTown_p.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtTown_p.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtTown_p.TabStop = True
		Me.txtTown_p.Visible = True
		Me.txtTown_p.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtTown_p.Name = "txtTown_p"
		Me.txtArea_p.AutoSize = False
		Me.txtArea_p.Size = New System.Drawing.Size(415, 19)
		Me.txtArea_p.Location = New System.Drawing.Point(241, 160)
		Me.txtArea_p.TabIndex = 47
		Me.txtArea_p.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtArea_p.AcceptsReturn = True
		Me.txtArea_p.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtArea_p.BackColor = System.Drawing.SystemColors.Window
		Me.txtArea_p.CausesValidation = True
		Me.txtArea_p.Enabled = True
		Me.txtArea_p.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtArea_p.HideSelection = True
		Me.txtArea_p.ReadOnly = False
		Me.txtArea_p.Maxlength = 0
		Me.txtArea_p.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtArea_p.MultiLine = False
		Me.txtArea_p.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtArea_p.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtArea_p.TabStop = True
		Me.txtArea_p.Visible = True
		Me.txtArea_p.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtArea_p.Name = "txtArea_p"
		Me.txtRoad_p.AutoSize = False
		Me.txtRoad_p.Size = New System.Drawing.Size(415, 19)
		Me.txtRoad_p.Location = New System.Drawing.Point(241, 136)
		Me.txtRoad_p.TabIndex = 46
		Me.txtRoad_p.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtRoad_p.AcceptsReturn = True
		Me.txtRoad_p.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtRoad_p.BackColor = System.Drawing.SystemColors.Window
		Me.txtRoad_p.CausesValidation = True
		Me.txtRoad_p.Enabled = True
		Me.txtRoad_p.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtRoad_p.HideSelection = True
		Me.txtRoad_p.ReadOnly = False
		Me.txtRoad_p.Maxlength = 0
		Me.txtRoad_p.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtRoad_p.MultiLine = False
		Me.txtRoad_p.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtRoad_p.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtRoad_p.TabStop = True
		Me.txtRoad_p.Visible = True
		Me.txtRoad_p.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtRoad_p.Name = "txtRoad_p"
		Me.txtBuilding_p.AutoSize = False
		Me.txtBuilding_p.Size = New System.Drawing.Size(415, 19)
		Me.txtBuilding_p.Location = New System.Drawing.Point(241, 112)
		Me.txtBuilding_p.TabIndex = 45
		Me.txtBuilding_p.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtBuilding_p.AcceptsReturn = True
		Me.txtBuilding_p.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtBuilding_p.BackColor = System.Drawing.SystemColors.Window
		Me.txtBuilding_p.CausesValidation = True
		Me.txtBuilding_p.Enabled = True
		Me.txtBuilding_p.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtBuilding_p.HideSelection = True
		Me.txtBuilding_p.ReadOnly = False
		Me.txtBuilding_p.Maxlength = 0
		Me.txtBuilding_p.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtBuilding_p.MultiLine = False
		Me.txtBuilding_p.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtBuilding_p.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtBuilding_p.TabStop = True
		Me.txtBuilding_p.Visible = True
		Me.txtBuilding_p.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtBuilding_p.Name = "txtBuilding_p"
		Me.txtFlat_p.AutoSize = False
		Me.txtFlat_p.Size = New System.Drawing.Size(415, 19)
		Me.txtFlat_p.Location = New System.Drawing.Point(241, 88)
		Me.txtFlat_p.TabIndex = 44
		Me.txtFlat_p.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtFlat_p.AcceptsReturn = True
		Me.txtFlat_p.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtFlat_p.BackColor = System.Drawing.SystemColors.Window
		Me.txtFlat_p.CausesValidation = True
		Me.txtFlat_p.Enabled = True
		Me.txtFlat_p.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtFlat_p.HideSelection = True
		Me.txtFlat_p.ReadOnly = False
		Me.txtFlat_p.Maxlength = 0
		Me.txtFlat_p.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtFlat_p.MultiLine = False
		Me.txtFlat_p.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtFlat_p.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtFlat_p.TabStop = True
		Me.txtFlat_p.Visible = True
		Me.txtFlat_p.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtFlat_p.Name = "txtFlat_p"
		Me.txtEmail_p.AutoSize = False
		Me.txtEmail_p.Size = New System.Drawing.Size(415, 19)
		Me.txtEmail_p.Location = New System.Drawing.Point(241, 256)
		Me.txtEmail_p.TabIndex = 43
		Me.txtEmail_p.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtEmail_p.AcceptsReturn = True
		Me.txtEmail_p.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtEmail_p.BackColor = System.Drawing.SystemColors.Window
		Me.txtEmail_p.CausesValidation = True
		Me.txtEmail_p.Enabled = True
		Me.txtEmail_p.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtEmail_p.HideSelection = True
		Me.txtEmail_p.ReadOnly = False
		Me.txtEmail_p.Maxlength = 0
		Me.txtEmail_p.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtEmail_p.MultiLine = False
		Me.txtEmail_p.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtEmail_p.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtEmail_p.TabStop = True
		Me.txtEmail_p.Visible = True
		Me.txtEmail_p.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtEmail_p.Name = "txtEmail_p"
		Me.txtPhone_p.AutoSize = False
		Me.txtPhone_p.Size = New System.Drawing.Size(415, 19)
		Me.txtPhone_p.Location = New System.Drawing.Point(241, 232)
		Me.txtPhone_p.TabIndex = 42
		Me.txtPhone_p.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtPhone_p.AcceptsReturn = True
		Me.txtPhone_p.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtPhone_p.BackColor = System.Drawing.SystemColors.Window
		Me.txtPhone_p.CausesValidation = True
		Me.txtPhone_p.Enabled = True
		Me.txtPhone_p.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtPhone_p.HideSelection = True
		Me.txtPhone_p.ReadOnly = False
		Me.txtPhone_p.Maxlength = 0
		Me.txtPhone_p.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtPhone_p.MultiLine = False
		Me.txtPhone_p.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtPhone_p.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtPhone_p.TabStop = True
		Me.txtPhone_p.Visible = True
		Me.txtPhone_p.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtPhone_p.Name = "txtPhone_p"
		Me.Label13.Text = "Designation :"
		Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label13.Size = New System.Drawing.Size(76, 13)
		Me.Label13.Location = New System.Drawing.Point(42, 50)
		Me.Label13.TabIndex = 81
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
		Me.Label37.Text = "(b) Address :"
		Me.Label37.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label37.Size = New System.Drawing.Size(73, 13)
		Me.Label37.Location = New System.Drawing.Point(24, 72)
		Me.Label37.TabIndex = 62
		Me.Label37.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label37.BackColor = System.Drawing.SystemColors.Control
		Me.Label37.Enabled = True
		Me.Label37.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label37.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label37.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label37.UseMnemonic = True
		Me.Label37.Visible = True
		Me.Label37.AutoSize = True
		Me.Label37.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label37.Name = "Label37"
		Me.Label36.Text = "Pin Code :"
		Me.Label36.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label36.Size = New System.Drawing.Size(60, 13)
		Me.Label36.Location = New System.Drawing.Point(486, 210)
		Me.Label36.TabIndex = 61
		Me.Label36.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label36.BackColor = System.Drawing.SystemColors.Control
		Me.Label36.Enabled = True
		Me.Label36.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label36.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label36.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label36.UseMnemonic = True
		Me.Label36.Visible = True
		Me.Label36.AutoSize = True
		Me.Label36.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label36.Name = "Label36"
		Me.Label35.Text = "State :"
		Me.Label35.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label35.Size = New System.Drawing.Size(39, 13)
		Me.Label35.Location = New System.Drawing.Point(42, 210)
		Me.Label35.TabIndex = 60
		Me.Label35.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label35.BackColor = System.Drawing.SystemColors.Control
		Me.Label35.Enabled = True
		Me.Label35.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label35.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label35.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label35.UseMnemonic = True
		Me.Label35.Visible = True
		Me.Label35.AutoSize = True
		Me.Label35.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label35.Name = "Label35"
		Me.Label34.Text = "Town / City / District :"
		Me.Label34.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label34.Size = New System.Drawing.Size(129, 13)
		Me.Label34.Location = New System.Drawing.Point(42, 186)
		Me.Label34.TabIndex = 59
		Me.Label34.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label34.BackColor = System.Drawing.SystemColors.Control
		Me.Label34.Enabled = True
		Me.Label34.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label34.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label34.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label34.UseMnemonic = True
		Me.Label34.Visible = True
		Me.Label34.AutoSize = True
		Me.Label34.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label34.Name = "Label34"
		Me.Label33.Text = "Area / Locality :"
		Me.Label33.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label33.Size = New System.Drawing.Size(93, 13)
		Me.Label33.Location = New System.Drawing.Point(42, 162)
		Me.Label33.TabIndex = 58
		Me.Label33.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label33.BackColor = System.Drawing.SystemColors.Control
		Me.Label33.Enabled = True
		Me.Label33.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label33.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label33.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label33.UseMnemonic = True
		Me.Label33.Visible = True
		Me.Label33.AutoSize = True
		Me.Label33.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label33.Name = "Label33"
		Me.Label32.Text = "Road / Street / Lane :"
		Me.Label32.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label32.Size = New System.Drawing.Size(129, 13)
		Me.Label32.Location = New System.Drawing.Point(42, 138)
		Me.Label32.TabIndex = 57
		Me.Label32.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label32.BackColor = System.Drawing.SystemColors.Control
		Me.Label32.Enabled = True
		Me.Label32.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label32.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label32.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label32.UseMnemonic = True
		Me.Label32.Visible = True
		Me.Label32.AutoSize = True
		Me.Label32.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label32.Name = "Label32"
		Me.Label31.Text = "Name of the Premises / Building :"
		Me.Label31.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label31.Size = New System.Drawing.Size(191, 13)
		Me.Label31.Location = New System.Drawing.Point(42, 114)
		Me.Label31.TabIndex = 56
		Me.Label31.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label31.BackColor = System.Drawing.SystemColors.Control
		Me.Label31.Enabled = True
		Me.Label31.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label31.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label31.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label31.UseMnemonic = True
		Me.Label31.Visible = True
		Me.Label31.AutoSize = True
		Me.Label31.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label31.Name = "Label31"
		Me.Label30.Text = "Flat No. :"
		Me.Label30.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label30.Size = New System.Drawing.Size(54, 13)
		Me.Label30.Location = New System.Drawing.Point(42, 90)
		Me.Label30.TabIndex = 55
		Me.Label30.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label30.BackColor = System.Drawing.SystemColors.Control
		Me.Label30.Enabled = True
		Me.Label30.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label30.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label30.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label30.UseMnemonic = True
		Me.Label30.Visible = True
		Me.Label30.AutoSize = True
		Me.Label30.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label30.Name = "Label30"
		Me.Label29.Text = "4. (a) Name of the person responsible for paying salary (if not the employer) :"
		Me.Label29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label29.Size = New System.Drawing.Size(220, 27)
		Me.Label29.Location = New System.Drawing.Point(12, 16)
		Me.Label29.TabIndex = 54
		Me.Label29.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label29.BackColor = System.Drawing.SystemColors.Control
		Me.Label29.Enabled = True
		Me.Label29.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label29.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label29.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label29.UseMnemonic = True
		Me.Label29.Visible = True
		Me.Label29.AutoSize = True
		Me.Label29.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label29.Name = "Label29"
		Me.Label26.Text = "E-mail :"
		Me.Label26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label26.Size = New System.Drawing.Size(43, 13)
		Me.Label26.Location = New System.Drawing.Point(40, 258)
		Me.Label26.TabIndex = 53
		Me.Label26.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label26.BackColor = System.Drawing.SystemColors.Control
		Me.Label26.Enabled = True
		Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label26.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label26.UseMnemonic = True
		Me.Label26.Visible = True
		Me.Label26.AutoSize = True
		Me.Label26.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label26.Name = "Label26"
		Me.Label24.Text = "Telephone No. :"
		Me.Label24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label24.Size = New System.Drawing.Size(93, 13)
		Me.Label24.Location = New System.Drawing.Point(40, 234)
		Me.Label24.TabIndex = 52
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
		Me._SSTab1_TabPage3.Text = "Salary Detail"
		Me.Frame1.Size = New System.Drawing.Size(661, 319)
		Me.Frame1.Location = New System.Drawing.Point(4, 28)
		Me.Frame1.TabIndex = 9
		Me.Frame1.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame1.BackColor = System.Drawing.SystemColors.Control
		Me.Frame1.Enabled = True
		Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame1.Visible = True
		Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame1.Name = "Frame1"
		SprdView24.OcxState = CType(resources.GetObject("SprdView24.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SprdView24.Size = New System.Drawing.Size(656, 307)
		Me.SprdView24.Location = New System.Drawing.Point(2, 8)
		Me.SprdView24.TabIndex = 10
		Me.SprdView24.Name = "SprdView24"
		Me._SSTab1_TabPage4.Text = "Challan Detail"
		Me.Frame4.Size = New System.Drawing.Size(661, 319)
		Me.Frame4.Location = New System.Drawing.Point(4, 28)
		Me.Frame4.TabIndex = 76
		Me.Frame4.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame4.BackColor = System.Drawing.SystemColors.Control
		Me.Frame4.Enabled = True
		Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame4.Visible = True
		Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame4.Name = "Frame4"
		SprdViewChallan.OcxState = CType(resources.GetObject("SprdViewChallan.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SprdViewChallan.Size = New System.Drawing.Size(656, 307)
		Me.SprdViewChallan.Location = New System.Drawing.Point(2, 8)
		Me.SprdViewChallan.TabIndex = 77
		Me.SprdViewChallan.Name = "SprdViewChallan"
		Me._SSTab1_TabPage5.Text = "Annexure"
		Me.Frame5.Size = New System.Drawing.Size(661, 319)
		Me.Frame5.Location = New System.Drawing.Point(4, 28)
		Me.Frame5.TabIndex = 78
		Me.Frame5.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame5.BackColor = System.Drawing.SystemColors.Control
		Me.Frame5.Enabled = True
		Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame5.Visible = True
		Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame5.Name = "Frame5"
		SprdViewAnnex.OcxState = CType(resources.GetObject("SprdViewAnnex.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SprdViewAnnex.Size = New System.Drawing.Size(656, 307)
		Me.SprdViewAnnex.Location = New System.Drawing.Point(2, 8)
		Me.SprdViewAnnex.TabIndex = 79
		Me.SprdViewAnnex.Name = "SprdViewAnnex"
		Me.FraMovement.Size = New System.Drawing.Size(671, 49)
		Me.FraMovement.Location = New System.Drawing.Point(0, 344)
		Me.FraMovement.TabIndex = 1
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
		Me.cmdValidate.TabIndex = 84
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
		Me.chkConsolidated.Size = New System.Drawing.Size(107, 13)
		Me.chkConsolidated.Location = New System.Drawing.Point(12, 20)
		Me.chkConsolidated.TabIndex = 8
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
		Me.cmdCD.TabIndex = 7
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
		Me.CmdPreview.TabIndex = 4
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
		Me.cmdPrint.TabIndex = 3
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
		Me.cmdClose.TabIndex = 5
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
		Me.cmdShow.Location = New System.Drawing.Point(266, 9)
		Me.cmdShow.Image = CType(resources.GetObject("cmdShow.Image"), System.Drawing.Image)
		Me.cmdShow.TabIndex = 2
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
		Me.Report1.Location = New System.Drawing.Point(146, 10)
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
		Me.ADataAnnx.Size = New System.Drawing.Size(113, 23)
		Me.ADataAnnx.Location = New System.Drawing.Point(138, 0)
		Me.ADataAnnx.Visible = 0
		Me.ADataAnnx.CursorLocation = ADODB.CursorLocationEnum.adUseClient
		Me.ADataAnnx.ConnectionTimeout = 15
		Me.ADataAnnx.CommandTimeout = 30
		Me.ADataAnnx.CursorType = ADODB.CursorTypeEnum.adOpenStatic
		Me.ADataAnnx.LockType = ADODB.LockTypeEnum.adLockOptimistic
		Me.ADataAnnx.CommandType = ADODB.CommandTypeEnum.adCmdUnknown
		Me.ADataAnnx.CacheSize = 50
		Me.ADataAnnx.MaxRecords = 0
		Me.ADataAnnx.BOFAction = Microsoft.VisualBasic.Compatibility.VB6.ADODC.BOFActionEnum.adDoMoveFirst
		Me.ADataAnnx.EOFAction = Microsoft.VisualBasic.Compatibility.VB6.ADODC.EOFActionEnum.adDoMoveLast
		Me.ADataAnnx.BackColor = System.Drawing.SystemColors.Window
		Me.ADataAnnx.ForeColor = System.Drawing.SystemColors.WindowText
		Me.ADataAnnx.Orientation = Microsoft.VisualBasic.Compatibility.VB6.ADODC.OrientationEnum.adHorizontal
		Me.ADataAnnx.Enabled = True
		Me.ADataAnnx.UserName = ""
		Me.ADataAnnx.RecordSource = ""
		Me.ADataAnnx.Text = "Adodc1"
		Me.ADataAnnx.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.ADataAnnx.ConnectionString = ""
		Me.ADataAnnx.Name = "ADataAnnx"
		Me.AData26.Size = New System.Drawing.Size(113, 23)
		Me.AData26.Location = New System.Drawing.Point(68, 0)
		Me.AData26.Visible = 0
		Me.AData26.CursorLocation = ADODB.CursorLocationEnum.adUseClient
		Me.AData26.ConnectionTimeout = 15
		Me.AData26.CommandTimeout = 30
		Me.AData26.CursorType = ADODB.CursorTypeEnum.adOpenStatic
		Me.AData26.LockType = ADODB.LockTypeEnum.adLockOptimistic
		Me.AData26.CommandType = ADODB.CommandTypeEnum.adCmdUnknown
		Me.AData26.CacheSize = 50
		Me.AData26.MaxRecords = 0
		Me.AData26.BOFAction = Microsoft.VisualBasic.Compatibility.VB6.ADODC.BOFActionEnum.adDoMoveFirst
		Me.AData26.EOFAction = Microsoft.VisualBasic.Compatibility.VB6.ADODC.EOFActionEnum.adDoMoveLast
		Me.AData26.BackColor = System.Drawing.SystemColors.Window
		Me.AData26.ForeColor = System.Drawing.SystemColors.WindowText
		Me.AData26.Orientation = Microsoft.VisualBasic.Compatibility.VB6.ADODC.OrientationEnum.adHorizontal
		Me.AData26.Enabled = True
		Me.AData26.UserName = ""
		Me.AData26.RecordSource = ""
		Me.AData26.Text = "Adodc1"
		Me.AData26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.AData26.ConnectionString = ""
		Me.AData26.Name = "AData26"
		Me.optAddressChange.SetIndex(_optAddressChange_1, CType(1, Short))
		Me.optAddressChange.SetIndex(_optAddressChange_0, CType(0, Short))
		Me.optResAddChanged.SetIndex(_optResAddChanged_1, CType(1, Short))
		Me.optResAddChanged.SetIndex(_optResAddChanged_0, CType(0, Short))
		CType(Me.optResAddChanged, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.optAddressChange, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SprdViewAnnex, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SprdViewChallan, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SprdView24, System.ComponentModel.ISupportInitialize).EndInit()
		Me.Controls.Add(SSTab1)
		Me.Controls.Add(FraMovement)
		Me.Controls.Add(AData1)
		Me.Controls.Add(ADataAnnx)
		Me.Controls.Add(AData26)
		Me.SSTab1.Controls.Add(_SSTab1_TabPage0)
		Me.SSTab1.Controls.Add(_SSTab1_TabPage1)
		Me.SSTab1.Controls.Add(_SSTab1_TabPage2)
		Me.SSTab1.Controls.Add(_SSTab1_TabPage3)
		Me.SSTab1.Controls.Add(_SSTab1_TabPage4)
		Me.SSTab1.Controls.Add(_SSTab1_TabPage5)
		Me._SSTab1_TabPage0.Controls.Add(Frame3)
		Me.Frame3.Controls.Add(Frame7)
		Me.Frame3.Controls.Add(txtPanNo)
		Me.Frame3.Controls.Add(txtTDSAcNo)
		Me.Frame3.Controls.Add(Label20)
		Me.Frame3.Controls.Add(Label19)
		Me.Frame3.Controls.Add(Label18)
		Me.Frame7.Controls.Add(txtRundate)
		Me.Frame7.Controls.Add(txtDateFrom)
		Me.Frame7.Controls.Add(txtDateTo)
		Me.Frame7.Controls.Add(Lable11)
		Me.Frame7.Controls.Add(Label16)
		Me.Frame7.Controls.Add(Label17)
		Me._SSTab1_TabPage1.Controls.Add(Frame2)
		Me.Frame2.Controls.Add(Frame9)
		Me.Frame2.Controls.Add(Frame8)
		Me.Frame2.Controls.Add(txtPhone)
		Me.Frame2.Controls.Add(txtEmail)
		Me.Frame2.Controls.Add(txtDeductorType)
		Me.Frame2.Controls.Add(txtFlat)
		Me.Frame2.Controls.Add(txtBuilding)
		Me.Frame2.Controls.Add(txtRoad)
		Me.Frame2.Controls.Add(txtArea)
		Me.Frame2.Controls.Add(txtTown)
		Me.Frame2.Controls.Add(txtState)
		Me.Frame2.Controls.Add(txtPinCode)
		Me.Frame2.Controls.Add(txtPersonName)
		Me.Frame2.Controls.Add(Label8)
		Me.Frame2.Controls.Add(Label4)
		Me.Frame2.Controls.Add(Label25)
		Me.Frame2.Controls.Add(Label23)
		Me.Frame2.Controls.Add(Label21)
		Me.Frame2.Controls.Add(Label7)
		Me.Frame2.Controls.Add(Label6)
		Me.Frame2.Controls.Add(Label3)
		Me.Frame2.Controls.Add(Label2)
		Me.Frame2.Controls.Add(Label9)
		Me.Frame2.Controls.Add(Label10)
		Me.Frame2.Controls.Add(Label11)
		Me.Frame2.Controls.Add(Label12)
		Me.Frame2.Controls.Add(Label5)
		Me.Frame2.Controls.Add(Label1)
		Me.Frame9.Controls.Add(_optResAddChanged_1)
		Me.Frame9.Controls.Add(_optResAddChanged_0)
		Me.Frame8.Controls.Add(_optAddressChange_1)
		Me.Frame8.Controls.Add(_optAddressChange_0)
		Me._SSTab1_TabPage2.Controls.Add(Frame6)
		Me.Frame6.Controls.Add(txtDesg)
		Me.Frame6.Controls.Add(txtPersonName_p)
		Me.Frame6.Controls.Add(txtPinCode_p)
		Me.Frame6.Controls.Add(txtState_p)
		Me.Frame6.Controls.Add(txtTown_p)
		Me.Frame6.Controls.Add(txtArea_p)
		Me.Frame6.Controls.Add(txtRoad_p)
		Me.Frame6.Controls.Add(txtBuilding_p)
		Me.Frame6.Controls.Add(txtFlat_p)
		Me.Frame6.Controls.Add(txtEmail_p)
		Me.Frame6.Controls.Add(txtPhone_p)
		Me.Frame6.Controls.Add(Label13)
		Me.Frame6.Controls.Add(Label37)
		Me.Frame6.Controls.Add(Label36)
		Me.Frame6.Controls.Add(Label35)
		Me.Frame6.Controls.Add(Label34)
		Me.Frame6.Controls.Add(Label33)
		Me.Frame6.Controls.Add(Label32)
		Me.Frame6.Controls.Add(Label31)
		Me.Frame6.Controls.Add(Label30)
		Me.Frame6.Controls.Add(Label29)
		Me.Frame6.Controls.Add(Label26)
		Me.Frame6.Controls.Add(Label24)
		Me._SSTab1_TabPage3.Controls.Add(Frame1)
		Me.Frame1.Controls.Add(SprdView24)
		Me._SSTab1_TabPage4.Controls.Add(Frame4)
		Me.Frame4.Controls.Add(SprdViewChallan)
		Me._SSTab1_TabPage5.Controls.Add(Frame5)
		Me.Frame5.Controls.Add(SprdViewAnnex)
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
		Me.Frame3.ResumeLayout(False)
		Me.Frame7.ResumeLayout(False)
		Me._SSTab1_TabPage1.ResumeLayout(False)
		Me.Frame2.ResumeLayout(False)
		Me.Frame9.ResumeLayout(False)
		Me.Frame8.ResumeLayout(False)
		Me._SSTab1_TabPage2.ResumeLayout(False)
		Me.Frame6.ResumeLayout(False)
		Me._SSTab1_TabPage3.ResumeLayout(False)
		Me.Frame1.ResumeLayout(False)
		Me._SSTab1_TabPage4.ResumeLayout(False)
		Me.Frame4.ResumeLayout(False)
		Me._SSTab1_TabPage5.ResumeLayout(False)
		Me.Frame5.ResumeLayout(False)
		Me.FraMovement.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()
	End Sub
#End Region 
#Region "Upgrade Support"
	Public Sub VB6_AddADODataBinding()
		SprdView24.DataSource = CType(AData26, MSDATASRC.DataSource)
		SprdViewChallan.DataSource = CType(AData26, MSDATASRC.DataSource)
		SprdViewAnnex.DataSource = CType(AData26, MSDATASRC.DataSource)
	End Sub
	Public Sub VB6_RemoveADODataBinding()
		SprdView24.DataSource = Nothing
		SprdViewChallan.DataSource = Nothing
		SprdViewAnnex.DataSource = Nothing
	End Sub
#End Region 
End Class