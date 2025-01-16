<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmForm16AProcess
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
	Public WithEvents txtChallanDate As System.Windows.Forms.MaskedTextBox
	Public WithEvents Frame1 As System.Windows.Forms.GroupBox
	Public WithEvents LstSection As System.Windows.Forms.ListBox
	Public WithEvents FraSection As System.Windows.Forms.GroupBox
	Public WithEvents _OptCustomer_1 As System.Windows.Forms.RadioButton
	Public WithEvents _OptCustomer_0 As System.Windows.Forms.RadioButton
	Public WithEvents txtCustomer As System.Windows.Forms.TextBox
	Public WithEvents cmdSearch As System.Windows.Forms.Button
	Public WithEvents FraParty As System.Windows.Forms.GroupBox
	Public WithEvents txtDateFrom As System.Windows.Forms.MaskedTextBox
	Public WithEvents txtDateTo As System.Windows.Forms.MaskedTextBox
	Public WithEvents Label4 As System.Windows.Forms.Label
	Public WithEvents Label3 As System.Windows.Forms.Label
	Public WithEvents Frame4 As System.Windows.Forms.GroupBox
	Public WithEvents Report1 As AxCrystal.AxCrystalReport
	Public WithEvents txtFName As System.Windows.Forms.TextBox
	Public WithEvents txtDesignation As System.Windows.Forms.TextBox
	Public WithEvents txtAuthorized As System.Windows.Forms.TextBox
	Public WithEvents txtPlace As System.Windows.Forms.TextBox
	Public WithEvents txtDate As System.Windows.Forms.MaskedTextBox
	Public WithEvents Label7 As System.Windows.Forms.Label
	Public WithEvents Label6 As System.Windows.Forms.Label
	Public WithEvents Label5 As System.Windows.Forms.Label
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents FraFooter As System.Windows.Forms.GroupBox
	Public WithEvents CmdProcess As System.Windows.Forms.Button
	Public WithEvents CmdUnProcess As System.Windows.Forms.Button
	Public WithEvents cmdCancel As System.Windows.Forms.Button
	Public WithEvents Frame5 As System.Windows.Forms.GroupBox
	Public WithEvents _OptNew_1 As System.Windows.Forms.RadioButton
	Public WithEvents _OptNew_0 As System.Windows.Forms.RadioButton
	Public WithEvents FraNew As System.Windows.Forms.GroupBox
	Public WithEvents OptCustomer As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
	Public WithEvents OptNew As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmForm16AProcess))
		Me.components = New System.ComponentModel.Container()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
		Me.Frame1 = New System.Windows.Forms.GroupBox
		Me.txtChallanDate = New System.Windows.Forms.MaskedTextBox
		Me.FraSection = New System.Windows.Forms.GroupBox
		Me.LstSection = New System.Windows.Forms.ListBox
		Me.FraParty = New System.Windows.Forms.GroupBox
		Me._OptCustomer_1 = New System.Windows.Forms.RadioButton
		Me._OptCustomer_0 = New System.Windows.Forms.RadioButton
		Me.txtCustomer = New System.Windows.Forms.TextBox
		Me.cmdSearch = New System.Windows.Forms.Button
		Me.Frame4 = New System.Windows.Forms.GroupBox
		Me.txtDateFrom = New System.Windows.Forms.MaskedTextBox
		Me.txtDateTo = New System.Windows.Forms.MaskedTextBox
		Me.Label4 = New System.Windows.Forms.Label
		Me.Label3 = New System.Windows.Forms.Label
		Me.Report1 = New AxCrystal.AxCrystalReport
		Me.FraFooter = New System.Windows.Forms.GroupBox
		Me.txtFName = New System.Windows.Forms.TextBox
		Me.txtDesignation = New System.Windows.Forms.TextBox
		Me.txtAuthorized = New System.Windows.Forms.TextBox
		Me.txtPlace = New System.Windows.Forms.TextBox
		Me.txtDate = New System.Windows.Forms.MaskedTextBox
		Me.Label7 = New System.Windows.Forms.Label
		Me.Label6 = New System.Windows.Forms.Label
		Me.Label5 = New System.Windows.Forms.Label
		Me.Label2 = New System.Windows.Forms.Label
		Me.Label1 = New System.Windows.Forms.Label
		Me.Frame5 = New System.Windows.Forms.GroupBox
		Me.CmdProcess = New System.Windows.Forms.Button
		Me.CmdUnProcess = New System.Windows.Forms.Button
		Me.cmdCancel = New System.Windows.Forms.Button
		Me.FraNew = New System.Windows.Forms.GroupBox
		Me._OptNew_1 = New System.Windows.Forms.RadioButton
		Me._OptNew_0 = New System.Windows.Forms.RadioButton
		Me.OptCustomer = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(components)
		Me.OptNew = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(components)
		Me.Frame1.SuspendLayout()
		Me.FraSection.SuspendLayout()
		Me.FraParty.SuspendLayout()
		Me.Frame4.SuspendLayout()
		Me.FraFooter.SuspendLayout()
		Me.Frame5.SuspendLayout()
		Me.FraNew.SuspendLayout()
		Me.SuspendLayout()
		Me.ToolTip1.Active = True
		CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.OptCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.OptNew, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.Text = "Form 16A Process"
		Me.ClientSize = New System.Drawing.Size(393, 367)
		Me.Location = New System.Drawing.Point(4, 23)
		Me.Icon = CType(resources.GetObject("frmForm16AProcess.Icon"), System.Drawing.Icon)
		Me.MaximizeBox = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds
		Me.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.SystemColors.Control
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable
		Me.ControlBox = True
		Me.Enabled = True
		Me.KeyPreview = False
		Me.MinimizeBox = True
		Me.Cursor = System.Windows.Forms.Cursors.Default
		Me.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.ShowInTaskbar = True
		Me.HelpButton = False
		Me.WindowState = System.Windows.Forms.FormWindowState.Normal
		Me.Name = "frmForm16AProcess"
		Me.Frame1.Text = "Till Challan Date"
		Me.Frame1.ForeColor = System.Drawing.Color.FromARGB(128, 0, 0)
		Me.Frame1.Size = New System.Drawing.Size(169, 43)
		Me.Frame1.Location = New System.Drawing.Point(224, 44)
		Me.Frame1.TabIndex = 29
		Me.Frame1.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame1.BackColor = System.Drawing.SystemColors.Control
		Me.Frame1.Enabled = True
		Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame1.Visible = True
		Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame1.Name = "Frame1"
		Me.txtChallanDate.AllowPromptAsInput = False
		Me.txtChallanDate.Size = New System.Drawing.Size(83, 21)
		Me.txtChallanDate.Location = New System.Drawing.Point(76, 14)
		Me.txtChallanDate.TabIndex = 30
		Me.txtChallanDate.MaxLength = 10
		Me.txtChallanDate.Mask = "##/##/####"
		Me.txtChallanDate.PromptChar = "_"
		Me.txtChallanDate.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtChallanDate.Name = "txtChallanDate"
		Me.FraSection.Text = "Section"
		Me.FraSection.Size = New System.Drawing.Size(223, 135)
		Me.FraSection.Location = New System.Drawing.Point(0, 44)
		Me.FraSection.TabIndex = 18
		Me.FraSection.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.FraSection.BackColor = System.Drawing.SystemColors.Control
		Me.FraSection.Enabled = True
		Me.FraSection.ForeColor = System.Drawing.SystemColors.ControlText
		Me.FraSection.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.FraSection.Visible = True
		Me.FraSection.Padding = New System.Windows.Forms.Padding(0)
		Me.FraSection.Name = "FraSection"
		Me.LstSection.Size = New System.Drawing.Size(217, 120)
		Me.LstSection.IntegralHeight = False
		Me.LstSection.Location = New System.Drawing.Point(2, 14)
		Me.LstSection.TabIndex = 2
		Me.LstSection.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.LstSection.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.LstSection.BackColor = System.Drawing.SystemColors.Window
		Me.LstSection.CausesValidation = True
		Me.LstSection.Enabled = True
		Me.LstSection.ForeColor = System.Drawing.SystemColors.WindowText
		Me.LstSection.Cursor = System.Windows.Forms.Cursors.Default
		Me.LstSection.SelectionMode = System.Windows.Forms.SelectionMode.One
		Me.LstSection.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.LstSection.Sorted = False
		Me.LstSection.TabStop = True
		Me.LstSection.Visible = True
		Me.LstSection.MultiColumn = False
		Me.LstSection.Name = "LstSection"
		Me.FraParty.Text = "Party"
		Me.FraParty.Size = New System.Drawing.Size(393, 47)
		Me.FraParty.Location = New System.Drawing.Point(0, 180)
		Me.FraParty.TabIndex = 19
		Me.FraParty.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.FraParty.BackColor = System.Drawing.SystemColors.Control
		Me.FraParty.Enabled = True
		Me.FraParty.ForeColor = System.Drawing.SystemColors.ControlText
		Me.FraParty.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.FraParty.Visible = True
		Me.FraParty.Padding = New System.Windows.Forms.Padding(0)
		Me.FraParty.Name = "FraParty"
		Me._OptCustomer_1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._OptCustomer_1.Text = "Particular"
		Me._OptCustomer_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me._OptCustomer_1.Size = New System.Drawing.Size(87, 13)
		Me._OptCustomer_1.Location = New System.Drawing.Point(196, 10)
		Me._OptCustomer_1.TabIndex = 4
		Me._OptCustomer_1.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._OptCustomer_1.BackColor = System.Drawing.SystemColors.Control
		Me._OptCustomer_1.CausesValidation = True
		Me._OptCustomer_1.Enabled = True
		Me._OptCustomer_1.ForeColor = System.Drawing.SystemColors.ControlText
		Me._OptCustomer_1.Cursor = System.Windows.Forms.Cursors.Default
		Me._OptCustomer_1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._OptCustomer_1.Appearance = System.Windows.Forms.Appearance.Normal
		Me._OptCustomer_1.TabStop = True
		Me._OptCustomer_1.Checked = False
		Me._OptCustomer_1.Visible = True
		Me._OptCustomer_1.Name = "_OptCustomer_1"
		Me._OptCustomer_0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._OptCustomer_0.Text = "All"
		Me._OptCustomer_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me._OptCustomer_0.Size = New System.Drawing.Size(67, 13)
		Me._OptCustomer_0.Location = New System.Drawing.Point(130, 10)
		Me._OptCustomer_0.TabIndex = 3
		Me._OptCustomer_0.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._OptCustomer_0.BackColor = System.Drawing.SystemColors.Control
		Me._OptCustomer_0.CausesValidation = True
		Me._OptCustomer_0.Enabled = True
		Me._OptCustomer_0.ForeColor = System.Drawing.SystemColors.ControlText
		Me._OptCustomer_0.Cursor = System.Windows.Forms.Cursors.Default
		Me._OptCustomer_0.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._OptCustomer_0.Appearance = System.Windows.Forms.Appearance.Normal
		Me._OptCustomer_0.TabStop = True
		Me._OptCustomer_0.Checked = False
		Me._OptCustomer_0.Visible = True
		Me._OptCustomer_0.Name = "_OptCustomer_0"
		Me.txtCustomer.AutoSize = False
		Me.txtCustomer.ForeColor = System.Drawing.Color.Blue
		Me.txtCustomer.Size = New System.Drawing.Size(347, 19)
		Me.txtCustomer.Location = New System.Drawing.Point(14, 24)
		Me.txtCustomer.TabIndex = 5
		Me.txtCustomer.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtCustomer.AcceptsReturn = True
		Me.txtCustomer.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtCustomer.BackColor = System.Drawing.SystemColors.Window
		Me.txtCustomer.CausesValidation = True
		Me.txtCustomer.Enabled = True
		Me.txtCustomer.HideSelection = True
		Me.txtCustomer.ReadOnly = False
		Me.txtCustomer.Maxlength = 0
		Me.txtCustomer.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtCustomer.MultiLine = False
		Me.txtCustomer.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtCustomer.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtCustomer.TabStop = True
		Me.txtCustomer.Visible = True
		Me.txtCustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtCustomer.Name = "txtCustomer"
		Me.cmdSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.cmdSearch.BackColor = System.Drawing.SystemColors.Menu
		Me.cmdSearch.Size = New System.Drawing.Size(25, 19)
		Me.cmdSearch.Location = New System.Drawing.Point(362, 24)
		Me.cmdSearch.Image = CType(resources.GetObject("cmdSearch.Image"), System.Drawing.Image)
		Me.cmdSearch.TabIndex = 6
		Me.cmdSearch.TabStop = False
		Me.ToolTip1.SetToolTip(Me.cmdSearch, "Search Supplier")
		Me.cmdSearch.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdSearch.CausesValidation = True
		Me.cmdSearch.Enabled = True
		Me.cmdSearch.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdSearch.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdSearch.Name = "cmdSearch"
		Me.Frame4.Text = "Payment Date"
		Me.Frame4.ForeColor = System.Drawing.Color.FromARGB(128, 0, 0)
		Me.Frame4.Size = New System.Drawing.Size(393, 43)
		Me.Frame4.Location = New System.Drawing.Point(0, 0)
		Me.Frame4.TabIndex = 15
		Me.Frame4.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame4.BackColor = System.Drawing.SystemColors.Control
		Me.Frame4.Enabled = True
		Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame4.Visible = True
		Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame4.Name = "Frame4"
		Me.txtDateFrom.AllowPromptAsInput = False
		Me.txtDateFrom.Size = New System.Drawing.Size(83, 21)
		Me.txtDateFrom.Location = New System.Drawing.Point(88, 14)
		Me.txtDateFrom.TabIndex = 0
		Me.txtDateFrom.MaxLength = 10
		Me.txtDateFrom.Mask = "##/##/####"
		Me.txtDateFrom.PromptChar = "_"
		Me.txtDateFrom.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtDateFrom.Name = "txtDateFrom"
		Me.txtDateTo.AllowPromptAsInput = False
		Me.txtDateTo.Size = New System.Drawing.Size(83, 21)
		Me.txtDateTo.Location = New System.Drawing.Point(300, 14)
		Me.txtDateTo.TabIndex = 1
		Me.txtDateTo.MaxLength = 10
		Me.txtDateTo.Mask = "##/##/####"
		Me.txtDateTo.PromptChar = "_"
		Me.txtDateTo.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtDateTo.Name = "txtDateTo"
		Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.Label4.Text = "From : "
		Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label4.Size = New System.Drawing.Size(40, 13)
		Me.Label4.Location = New System.Drawing.Point(49, 18)
		Me.Label4.TabIndex = 17
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
		Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.Label3.Text = "To : "
		Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label3.Size = New System.Drawing.Size(28, 13)
		Me.Label3.Location = New System.Drawing.Point(273, 18)
		Me.Label3.TabIndex = 16
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
		Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
		Me.Report1.Location = New System.Drawing.Point(0, 0)
		Me.Report1.Name = "Report1"
		Me.FraFooter.Size = New System.Drawing.Size(393, 99)
		Me.FraFooter.Location = New System.Drawing.Point(0, 222)
		Me.FraFooter.TabIndex = 20
		Me.FraFooter.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.FraFooter.BackColor = System.Drawing.SystemColors.Control
		Me.FraFooter.Enabled = True
		Me.FraFooter.ForeColor = System.Drawing.SystemColors.ControlText
		Me.FraFooter.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.FraFooter.Visible = True
		Me.FraFooter.Padding = New System.Windows.Forms.Padding(0)
		Me.FraFooter.Name = "FraFooter"
		Me.txtFName.AutoSize = False
		Me.txtFName.Size = New System.Drawing.Size(289, 19)
		Me.txtFName.Location = New System.Drawing.Point(98, 56)
		Me.txtFName.TabIndex = 10
		Me.txtFName.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtFName.AcceptsReturn = True
		Me.txtFName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtFName.BackColor = System.Drawing.SystemColors.Window
		Me.txtFName.CausesValidation = True
		Me.txtFName.Enabled = True
		Me.txtFName.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtFName.HideSelection = True
		Me.txtFName.ReadOnly = False
		Me.txtFName.Maxlength = 0
		Me.txtFName.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtFName.MultiLine = False
		Me.txtFName.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtFName.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtFName.TabStop = True
		Me.txtFName.Visible = True
		Me.txtFName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtFName.Name = "txtFName"
		Me.txtDesignation.AutoSize = False
		Me.txtDesignation.Size = New System.Drawing.Size(289, 19)
		Me.txtDesignation.Location = New System.Drawing.Point(98, 76)
		Me.txtDesignation.TabIndex = 11
		Me.txtDesignation.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtDesignation.AcceptsReturn = True
		Me.txtDesignation.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtDesignation.BackColor = System.Drawing.SystemColors.Window
		Me.txtDesignation.CausesValidation = True
		Me.txtDesignation.Enabled = True
		Me.txtDesignation.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtDesignation.HideSelection = True
		Me.txtDesignation.ReadOnly = False
		Me.txtDesignation.Maxlength = 0
		Me.txtDesignation.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtDesignation.MultiLine = False
		Me.txtDesignation.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtDesignation.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtDesignation.TabStop = True
		Me.txtDesignation.Visible = True
		Me.txtDesignation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtDesignation.Name = "txtDesignation"
		Me.txtAuthorized.AutoSize = False
		Me.txtAuthorized.Size = New System.Drawing.Size(289, 19)
		Me.txtAuthorized.Location = New System.Drawing.Point(98, 36)
		Me.txtAuthorized.TabIndex = 9
		Me.txtAuthorized.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtAuthorized.AcceptsReturn = True
		Me.txtAuthorized.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtAuthorized.BackColor = System.Drawing.SystemColors.Window
		Me.txtAuthorized.CausesValidation = True
		Me.txtAuthorized.Enabled = True
		Me.txtAuthorized.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtAuthorized.HideSelection = True
		Me.txtAuthorized.ReadOnly = False
		Me.txtAuthorized.Maxlength = 0
		Me.txtAuthorized.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtAuthorized.MultiLine = False
		Me.txtAuthorized.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtAuthorized.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtAuthorized.TabStop = True
		Me.txtAuthorized.Visible = True
		Me.txtAuthorized.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtAuthorized.Name = "txtAuthorized"
		Me.txtPlace.AutoSize = False
		Me.txtPlace.Size = New System.Drawing.Size(155, 19)
		Me.txtPlace.Location = New System.Drawing.Point(98, 12)
		Me.txtPlace.TabIndex = 7
		Me.txtPlace.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtPlace.AcceptsReturn = True
		Me.txtPlace.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtPlace.BackColor = System.Drawing.SystemColors.Window
		Me.txtPlace.CausesValidation = True
		Me.txtPlace.Enabled = True
		Me.txtPlace.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtPlace.HideSelection = True
		Me.txtPlace.ReadOnly = False
		Me.txtPlace.Maxlength = 0
		Me.txtPlace.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtPlace.MultiLine = False
		Me.txtPlace.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtPlace.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtPlace.TabStop = True
		Me.txtPlace.Visible = True
		Me.txtPlace.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtPlace.Name = "txtPlace"
		Me.txtDate.AllowPromptAsInput = False
		Me.txtDate.Size = New System.Drawing.Size(83, 21)
		Me.txtDate.Location = New System.Drawing.Point(304, 12)
		Me.txtDate.TabIndex = 8
		Me.txtDate.MaxLength = 10
		Me.txtDate.Mask = "##/##/####"
		Me.txtDate.PromptChar = "_"
		Me.txtDate.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtDate.Name = "txtDate"
		Me.Label7.Text = "Father's Name : "
		Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label7.Size = New System.Drawing.Size(94, 13)
		Me.Label7.Location = New System.Drawing.Point(6, 58)
		Me.Label7.TabIndex = 31
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
		Me.Label6.Text = "Designation :"
		Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label6.Size = New System.Drawing.Size(76, 13)
		Me.Label6.Location = New System.Drawing.Point(6, 78)
		Me.Label6.TabIndex = 24
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
		Me.Label5.Text = "Authorized : "
		Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label5.Size = New System.Drawing.Size(73, 13)
		Me.Label5.Location = New System.Drawing.Point(6, 38)
		Me.Label5.TabIndex = 23
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
		Me.Label2.Text = "Date : "
		Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label2.Size = New System.Drawing.Size(40, 13)
		Me.Label2.Location = New System.Drawing.Point(262, 14)
		Me.Label2.TabIndex = 22
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
		Me.Label1.Text = "Place :"
		Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.Size = New System.Drawing.Size(41, 13)
		Me.Label1.Location = New System.Drawing.Point(6, 14)
		Me.Label1.TabIndex = 21
		Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label1.BackColor = System.Drawing.Color.Transparent
		Me.Label1.Enabled = True
		Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label1.UseMnemonic = True
		Me.Label1.Visible = True
		Me.Label1.AutoSize = True
		Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label1.Name = "Label1"
		Me.Frame5.Size = New System.Drawing.Size(393, 51)
		Me.Frame5.Location = New System.Drawing.Point(0, 316)
		Me.Frame5.TabIndex = 14
		Me.Frame5.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame5.BackColor = System.Drawing.SystemColors.Control
		Me.Frame5.Enabled = True
		Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame5.Visible = True
		Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame5.Name = "Frame5"
		Me.CmdProcess.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.CmdProcess.Text = "&Process"
		Me.CmdProcess.Size = New System.Drawing.Size(84, 37)
		Me.CmdProcess.Location = New System.Drawing.Point(4, 10)
		Me.CmdProcess.Image = CType(resources.GetObject("CmdProcess.Image"), System.Drawing.Image)
		Me.CmdProcess.TabIndex = 25
		Me.ToolTip1.SetToolTip(Me.CmdProcess, "Save Record")
		Me.CmdProcess.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CmdProcess.BackColor = System.Drawing.SystemColors.Control
		Me.CmdProcess.CausesValidation = True
		Me.CmdProcess.Enabled = True
		Me.CmdProcess.ForeColor = System.Drawing.SystemColors.ControlText
		Me.CmdProcess.Cursor = System.Windows.Forms.Cursors.Default
		Me.CmdProcess.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CmdProcess.TabStop = True
		Me.CmdProcess.Name = "CmdProcess"
		Me.CmdUnProcess.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.CmdUnProcess.Text = "&UnProcess"
		Me.CmdUnProcess.Size = New System.Drawing.Size(84, 37)
		Me.CmdUnProcess.Location = New System.Drawing.Point(150, 10)
		Me.CmdUnProcess.Image = CType(resources.GetObject("CmdUnProcess.Image"), System.Drawing.Image)
		Me.CmdUnProcess.TabIndex = 12
		Me.ToolTip1.SetToolTip(Me.CmdUnProcess, "Delete Record")
		Me.CmdUnProcess.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CmdUnProcess.BackColor = System.Drawing.SystemColors.Control
		Me.CmdUnProcess.CausesValidation = True
		Me.CmdUnProcess.Enabled = True
		Me.CmdUnProcess.ForeColor = System.Drawing.SystemColors.ControlText
		Me.CmdUnProcess.Cursor = System.Windows.Forms.Cursors.Default
		Me.CmdUnProcess.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CmdUnProcess.TabStop = True
		Me.CmdUnProcess.Name = "CmdUnProcess"
		Me.cmdCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.cmdCancel.Text = "&Close"
		Me.cmdCancel.Size = New System.Drawing.Size(80, 37)
		Me.cmdCancel.Location = New System.Drawing.Point(310, 10)
		Me.cmdCancel.Image = CType(resources.GetObject("cmdCancel.Image"), System.Drawing.Image)
		Me.cmdCancel.TabIndex = 13
		Me.ToolTip1.SetToolTip(Me.cmdCancel, "Close")
		Me.cmdCancel.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
		Me.cmdCancel.CausesValidation = True
		Me.cmdCancel.Enabled = True
		Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdCancel.TabStop = True
		Me.cmdCancel.Name = "cmdCancel"
		Me.FraNew.Size = New System.Drawing.Size(169, 95)
		Me.FraNew.Location = New System.Drawing.Point(224, 84)
		Me.FraNew.TabIndex = 26
		Me.FraNew.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.FraNew.BackColor = System.Drawing.SystemColors.Control
		Me.FraNew.Enabled = True
		Me.FraNew.ForeColor = System.Drawing.SystemColors.ControlText
		Me.FraNew.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.FraNew.Visible = True
		Me.FraNew.Padding = New System.Windows.Forms.Padding(0)
		Me.FraNew.Name = "FraNew"
		Me._OptNew_1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._OptNew_1.Text = "Append to latest one"
		Me._OptNew_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me._OptNew_1.Size = New System.Drawing.Size(149, 13)
		Me._OptNew_1.Location = New System.Drawing.Point(8, 60)
		Me._OptNew_1.TabIndex = 28
		Me._OptNew_1.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._OptNew_1.BackColor = System.Drawing.SystemColors.Control
		Me._OptNew_1.CausesValidation = True
		Me._OptNew_1.Enabled = True
		Me._OptNew_1.ForeColor = System.Drawing.SystemColors.ControlText
		Me._OptNew_1.Cursor = System.Windows.Forms.Cursors.Default
		Me._OptNew_1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._OptNew_1.Appearance = System.Windows.Forms.Appearance.Normal
		Me._OptNew_1.TabStop = True
		Me._OptNew_1.Checked = False
		Me._OptNew_1.Visible = True
		Me._OptNew_1.Name = "_OptNew_1"
		Me._OptNew_0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._OptNew_0.Text = "Generate New Only"
		Me._OptNew_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me._OptNew_0.Size = New System.Drawing.Size(149, 13)
		Me._OptNew_0.Location = New System.Drawing.Point(8, 30)
		Me._OptNew_0.TabIndex = 27
		Me._OptNew_0.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._OptNew_0.BackColor = System.Drawing.SystemColors.Control
		Me._OptNew_0.CausesValidation = True
		Me._OptNew_0.Enabled = True
		Me._OptNew_0.ForeColor = System.Drawing.SystemColors.ControlText
		Me._OptNew_0.Cursor = System.Windows.Forms.Cursors.Default
		Me._OptNew_0.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._OptNew_0.Appearance = System.Windows.Forms.Appearance.Normal
		Me._OptNew_0.TabStop = True
		Me._OptNew_0.Checked = False
		Me._OptNew_0.Visible = True
		Me._OptNew_0.Name = "_OptNew_0"
		Me.OptCustomer.SetIndex(_OptCustomer_1, CType(1, Short))
		Me.OptCustomer.SetIndex(_OptCustomer_0, CType(0, Short))
		Me.OptNew.SetIndex(_OptNew_1, CType(1, Short))
		Me.OptNew.SetIndex(_OptNew_0, CType(0, Short))
		CType(Me.OptNew, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.OptCustomer, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.Controls.Add(Frame1)
		Me.Controls.Add(FraSection)
		Me.Controls.Add(FraParty)
		Me.Controls.Add(Frame4)
		Me.Controls.Add(Report1)
		Me.Controls.Add(FraFooter)
		Me.Controls.Add(Frame5)
		Me.Controls.Add(FraNew)
		Me.Frame1.Controls.Add(txtChallanDate)
		Me.FraSection.Controls.Add(LstSection)
		Me.FraParty.Controls.Add(_OptCustomer_1)
		Me.FraParty.Controls.Add(_OptCustomer_0)
		Me.FraParty.Controls.Add(txtCustomer)
		Me.FraParty.Controls.Add(cmdSearch)
		Me.Frame4.Controls.Add(txtDateFrom)
		Me.Frame4.Controls.Add(txtDateTo)
		Me.Frame4.Controls.Add(Label4)
		Me.Frame4.Controls.Add(Label3)
		Me.FraFooter.Controls.Add(txtFName)
		Me.FraFooter.Controls.Add(txtDesignation)
		Me.FraFooter.Controls.Add(txtAuthorized)
		Me.FraFooter.Controls.Add(txtPlace)
		Me.FraFooter.Controls.Add(txtDate)
		Me.FraFooter.Controls.Add(Label7)
		Me.FraFooter.Controls.Add(Label6)
		Me.FraFooter.Controls.Add(Label5)
		Me.FraFooter.Controls.Add(Label2)
		Me.FraFooter.Controls.Add(Label1)
		Me.Frame5.Controls.Add(CmdProcess)
		Me.Frame5.Controls.Add(CmdUnProcess)
		Me.Frame5.Controls.Add(cmdCancel)
		Me.FraNew.Controls.Add(_OptNew_1)
		Me.FraNew.Controls.Add(_OptNew_0)
		Me.Frame1.ResumeLayout(False)
		Me.FraSection.ResumeLayout(False)
		Me.FraParty.ResumeLayout(False)
		Me.Frame4.ResumeLayout(False)
		Me.FraFooter.ResumeLayout(False)
		Me.Frame5.ResumeLayout(False)
		Me.FraNew.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()
	End Sub
#End Region 
End Class