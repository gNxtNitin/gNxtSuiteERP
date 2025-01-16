<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmPrintLedg
#Region "Windows Form Designer generated code "
	<System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
		MyBase.New()
		'This call is required by the Windows Form Designer.
		InitializeComponent()
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
	Public WithEvents _chkPrintOption_7 As System.Windows.Forms.CheckBox
	Public WithEvents _chkPrintOption_4 As System.Windows.Forms.CheckBox
	Public WithEvents _chkPrintOption_5 As System.Windows.Forms.CheckBox
	Public WithEvents _chkPrintOption_6 As System.Windows.Forms.CheckBox
	Public WithEvents _chkPrintOption_3 As System.Windows.Forms.CheckBox
	Public WithEvents _chkPrintOption_2 As System.Windows.Forms.CheckBox
	Public WithEvents _chkPrintOption_1 As System.Windows.Forms.CheckBox
	Public WithEvents _chkPrintOption_0 As System.Windows.Forms.CheckBox
	Public WithEvents fraPrintOption As System.Windows.Forms.GroupBox
	Public WithEvents chkWideFormat As System.Windows.Forms.CheckBox
	Public WithEvents cmdOk As System.Windows.Forms.Button
	Public WithEvents cmdCancel As System.Windows.Forms.Button
	Public WithEvents FraOk As System.Windows.Forms.GroupBox
	Public WithEvents OptDebtors As System.Windows.Forms.RadioButton
	Public WithEvents OptCreditors As System.Windows.Forms.RadioButton
	Public WithEvents OptGeneral As System.Windows.Forms.RadioButton
	Public WithEvents OptExpenses As System.Windows.Forms.RadioButton
	Public WithEvents cmdsearch As System.Windows.Forms.Button
	Public WithEvents txtLedgerGroup As System.Windows.Forms.TextBox
	Public WithEvents OptGroup As System.Windows.Forms.RadioButton
	Public WithEvents OptSelected As System.Windows.Forms.RadioButton
	Public WithEvents OptAll As System.Windows.Forms.RadioButton
	Public WithEvents Frame1 As System.Windows.Forms.GroupBox
	Public WithEvents chkPrintOption As Microsoft.VisualBasic.Compatibility.VB6.CheckBoxArray
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrintLedg))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.cmdsearchSale = New System.Windows.Forms.Button()
        Me.fraPrintOption = New System.Windows.Forms.GroupBox()
        Me._chkPrintOption_7 = New System.Windows.Forms.CheckBox()
        Me._chkPrintOption_4 = New System.Windows.Forms.CheckBox()
        Me._chkPrintOption_5 = New System.Windows.Forms.CheckBox()
        Me._chkPrintOption_6 = New System.Windows.Forms.CheckBox()
        Me._chkPrintOption_3 = New System.Windows.Forms.CheckBox()
        Me._chkPrintOption_2 = New System.Windows.Forms.CheckBox()
        Me._chkPrintOption_1 = New System.Windows.Forms.CheckBox()
        Me._chkPrintOption_0 = New System.Windows.Forms.CheckBox()
        Me.FraOk = New System.Windows.Forms.GroupBox()
        Me.chkWideFormat = New System.Windows.Forms.CheckBox()
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.txtSalesPerson = New System.Windows.Forms.TextBox()
        Me.optSalesPerson = New System.Windows.Forms.RadioButton()
        Me.OptDebtors = New System.Windows.Forms.RadioButton()
        Me.OptCreditors = New System.Windows.Forms.RadioButton()
        Me.OptGeneral = New System.Windows.Forms.RadioButton()
        Me.OptExpenses = New System.Windows.Forms.RadioButton()
        Me.txtLedgerGroup = New System.Windows.Forms.TextBox()
        Me.OptGroup = New System.Windows.Forms.RadioButton()
        Me.OptSelected = New System.Windows.Forms.RadioButton()
        Me.OptAll = New System.Windows.Forms.RadioButton()
        Me.chkPrintOption = New Microsoft.VisualBasic.Compatibility.VB6.CheckBoxArray(Me.components)
        Me.fraPrintOption.SuspendLayout()
        Me.FraOk.SuspendLayout()
        Me.Frame1.SuspendLayout()
        CType(Me.chkPrintOption, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdsearch
        '
        Me.cmdsearch.AutoSize = True
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.Location = New System.Drawing.Point(295, 109)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(32, 26)
        Me.cmdsearch.TabIndex = 7
        Me.cmdsearch.TabStop = False
        Me.cmdsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearch, "Search")
        Me.cmdsearch.UseVisualStyleBackColor = False
        '
        'cmdsearchSale
        '
        Me.cmdsearchSale.AutoSize = True
        Me.cmdsearchSale.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearchSale.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearchSale.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearchSale.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearchSale.Image = CType(resources.GetObject("cmdsearchSale.Image"), System.Drawing.Image)
        Me.cmdsearchSale.Location = New System.Drawing.Point(295, 151)
        Me.cmdsearchSale.Name = "cmdsearchSale"
        Me.cmdsearchSale.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearchSale.Size = New System.Drawing.Size(32, 26)
        Me.cmdsearchSale.TabIndex = 10
        Me.cmdsearchSale.TabStop = False
        Me.cmdsearchSale.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearchSale, "Search")
        Me.cmdsearchSale.UseVisualStyleBackColor = False
        '
        'fraPrintOption
        '
        Me.fraPrintOption.BackColor = System.Drawing.SystemColors.Control
        Me.fraPrintOption.Controls.Add(Me._chkPrintOption_7)
        Me.fraPrintOption.Controls.Add(Me._chkPrintOption_4)
        Me.fraPrintOption.Controls.Add(Me._chkPrintOption_5)
        Me.fraPrintOption.Controls.Add(Me._chkPrintOption_6)
        Me.fraPrintOption.Controls.Add(Me._chkPrintOption_3)
        Me.fraPrintOption.Controls.Add(Me._chkPrintOption_2)
        Me.fraPrintOption.Controls.Add(Me._chkPrintOption_1)
        Me.fraPrintOption.Controls.Add(Me._chkPrintOption_0)
        Me.fraPrintOption.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraPrintOption.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraPrintOption.Location = New System.Drawing.Point(0, 180)
        Me.fraPrintOption.Name = "fraPrintOption"
        Me.fraPrintOption.Padding = New System.Windows.Forms.Padding(0)
        Me.fraPrintOption.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraPrintOption.Size = New System.Drawing.Size(335, 108)
        Me.fraPrintOption.TabIndex = 18
        Me.fraPrintOption.TabStop = False
        Me.fraPrintOption.Text = "Printing Option"
        '
        '_chkPrintOption_7
        '
        Me._chkPrintOption_7.AutoSize = True
        Me._chkPrintOption_7.BackColor = System.Drawing.SystemColors.Control
        Me._chkPrintOption_7.Checked = True
        Me._chkPrintOption_7.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkPrintOption_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkPrintOption_7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkPrintOption_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPrintOption.SetIndex(Me._chkPrintOption_7, CType(7, Short))
        Me._chkPrintOption_7.Location = New System.Drawing.Point(26, 52)
        Me._chkPrintOption_7.Name = "_chkPrintOption_7"
        Me._chkPrintOption_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkPrintOption_7.Size = New System.Drawing.Size(75, 18)
        Me._chkPrintOption_7.TabIndex = 11
        Me._chkPrintOption_7.Text = "Bill Detail"
        Me._chkPrintOption_7.UseVisualStyleBackColor = False
        '
        '_chkPrintOption_4
        '
        Me._chkPrintOption_4.AutoSize = True
        Me._chkPrintOption_4.BackColor = System.Drawing.SystemColors.Control
        Me._chkPrintOption_4.Checked = True
        Me._chkPrintOption_4.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkPrintOption_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkPrintOption_4.Enabled = False
        Me._chkPrintOption_4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkPrintOption_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPrintOption.SetIndex(Me._chkPrintOption_4, CType(4, Short))
        Me._chkPrintOption_4.Location = New System.Drawing.Point(148, 18)
        Me._chkPrintOption_4.Name = "_chkPrintOption_4"
        Me._chkPrintOption_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkPrintOption_4.Size = New System.Drawing.Size(63, 18)
        Me._chkPrintOption_4.TabIndex = 21
        Me._chkPrintOption_4.Text = "Cost C"
        Me._chkPrintOption_4.UseVisualStyleBackColor = False
        Me._chkPrintOption_4.Visible = False
        '
        '_chkPrintOption_5
        '
        Me._chkPrintOption_5.AutoSize = True
        Me._chkPrintOption_5.BackColor = System.Drawing.SystemColors.Control
        Me._chkPrintOption_5.Checked = True
        Me._chkPrintOption_5.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkPrintOption_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkPrintOption_5.Enabled = False
        Me._chkPrintOption_5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkPrintOption_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPrintOption.SetIndex(Me._chkPrintOption_5, CType(5, Short))
        Me._chkPrintOption_5.Location = New System.Drawing.Point(148, 34)
        Me._chkPrintOption_5.Name = "_chkPrintOption_5"
        Me._chkPrintOption_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkPrintOption_5.Size = New System.Drawing.Size(91, 18)
        Me._chkPrintOption_5.TabIndex = 20
        Me._chkPrintOption_5.Text = "Department"
        Me._chkPrintOption_5.UseVisualStyleBackColor = False
        Me._chkPrintOption_5.Visible = False
        '
        '_chkPrintOption_6
        '
        Me._chkPrintOption_6.AutoSize = True
        Me._chkPrintOption_6.BackColor = System.Drawing.SystemColors.Control
        Me._chkPrintOption_6.Checked = True
        Me._chkPrintOption_6.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkPrintOption_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkPrintOption_6.Enabled = False
        Me._chkPrintOption_6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkPrintOption_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPrintOption.SetIndex(Me._chkPrintOption_6, CType(6, Short))
        Me._chkPrintOption_6.Location = New System.Drawing.Point(148, 50)
        Me._chkPrintOption_6.Name = "_chkPrintOption_6"
        Me._chkPrintOption_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkPrintOption_6.Size = New System.Drawing.Size(80, 18)
        Me._chkPrintOption_6.TabIndex = 19
        Me._chkPrintOption_6.Text = "Employee"
        Me._chkPrintOption_6.UseVisualStyleBackColor = False
        Me._chkPrintOption_6.Visible = False
        '
        '_chkPrintOption_3
        '
        Me._chkPrintOption_3.AutoSize = True
        Me._chkPrintOption_3.BackColor = System.Drawing.SystemColors.Control
        Me._chkPrintOption_3.Checked = True
        Me._chkPrintOption_3.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkPrintOption_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkPrintOption_3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkPrintOption_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPrintOption.SetIndex(Me._chkPrintOption_3, CType(3, Short))
        Me._chkPrintOption_3.Location = New System.Drawing.Point(26, 88)
        Me._chkPrintOption_3.Name = "_chkPrintOption_3"
        Me._chkPrintOption_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkPrintOption_3.Size = New System.Drawing.Size(105, 18)
        Me._chkPrintOption_3.TabIndex = 13
        Me._chkPrintOption_3.Text = "Account Name"
        Me._chkPrintOption_3.UseVisualStyleBackColor = False
        '
        '_chkPrintOption_2
        '
        Me._chkPrintOption_2.AutoSize = True
        Me._chkPrintOption_2.BackColor = System.Drawing.SystemColors.Control
        Me._chkPrintOption_2.Checked = True
        Me._chkPrintOption_2.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkPrintOption_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkPrintOption_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkPrintOption_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPrintOption.SetIndex(Me._chkPrintOption_2, CType(2, Short))
        Me._chkPrintOption_2.Location = New System.Drawing.Point(26, 70)
        Me._chkPrintOption_2.Name = "_chkPrintOption_2"
        Me._chkPrintOption_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkPrintOption_2.Size = New System.Drawing.Size(124, 18)
        Me._chkPrintOption_2.TabIndex = 12
        Me._chkPrintOption_2.Text = "Cheque No && Date"
        Me._chkPrintOption_2.UseVisualStyleBackColor = False
        '
        '_chkPrintOption_1
        '
        Me._chkPrintOption_1.AutoSize = True
        Me._chkPrintOption_1.BackColor = System.Drawing.SystemColors.Control
        Me._chkPrintOption_1.Checked = True
        Me._chkPrintOption_1.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkPrintOption_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkPrintOption_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkPrintOption_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPrintOption.SetIndex(Me._chkPrintOption_1, CType(1, Short))
        Me._chkPrintOption_1.Location = New System.Drawing.Point(26, 34)
        Me._chkPrintOption_1.Name = "_chkPrintOption_1"
        Me._chkPrintOption_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkPrintOption_1.Size = New System.Drawing.Size(76, 18)
        Me._chkPrintOption_1.TabIndex = 10
        Me._chkPrintOption_1.Text = "Narration"
        Me._chkPrintOption_1.UseVisualStyleBackColor = False
        '
        '_chkPrintOption_0
        '
        Me._chkPrintOption_0.AutoSize = True
        Me._chkPrintOption_0.BackColor = System.Drawing.SystemColors.Control
        Me._chkPrintOption_0.Checked = True
        Me._chkPrintOption_0.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkPrintOption_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkPrintOption_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkPrintOption_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPrintOption.SetIndex(Me._chkPrintOption_0, CType(0, Short))
        Me._chkPrintOption_0.Location = New System.Drawing.Point(26, 16)
        Me._chkPrintOption_0.Name = "_chkPrintOption_0"
        Me._chkPrintOption_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkPrintOption_0.Size = New System.Drawing.Size(116, 18)
        Me._chkPrintOption_0.TabIndex = 9
        Me._chkPrintOption_0.Text = "Running Balance"
        Me._chkPrintOption_0.UseVisualStyleBackColor = False
        '
        'FraOk
        '
        Me.FraOk.BackColor = System.Drawing.SystemColors.Control
        Me.FraOk.Controls.Add(Me.chkWideFormat)
        Me.FraOk.Controls.Add(Me.cmdOk)
        Me.FraOk.Controls.Add(Me.cmdCancel)
        Me.FraOk.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraOk.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraOk.Location = New System.Drawing.Point(0, 285)
        Me.FraOk.Name = "FraOk"
        Me.FraOk.Padding = New System.Windows.Forms.Padding(0)
        Me.FraOk.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraOk.Size = New System.Drawing.Size(335, 61)
        Me.FraOk.TabIndex = 15
        Me.FraOk.TabStop = False
        '
        'chkWideFormat
        '
        Me.chkWideFormat.AutoSize = True
        Me.chkWideFormat.BackColor = System.Drawing.SystemColors.Control
        Me.chkWideFormat.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkWideFormat.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkWideFormat.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkWideFormat.Location = New System.Drawing.Point(90, 12)
        Me.chkWideFormat.Name = "chkWideFormat"
        Me.chkWideFormat.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkWideFormat.Size = New System.Drawing.Size(95, 18)
        Me.chkWideFormat.TabIndex = 22
        Me.chkWideFormat.Text = "Wide Format"
        Me.chkWideFormat.UseVisualStyleBackColor = False
        '
        'cmdOk
        '
        Me.cmdOk.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOk.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOk.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOk.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOk.Location = New System.Drawing.Point(8, 32)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOk.Size = New System.Drawing.Size(73, 25)
        Me.cmdOk.TabIndex = 17
        Me.cmdOk.Text = "&Ok"
        Me.cmdOk.UseVisualStyleBackColor = False
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(116, 32)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(73, 25)
        Me.cmdCancel.TabIndex = 16
        Me.cmdCancel.Text = "&Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.cmdsearchSale)
        Me.Frame1.Controls.Add(Me.txtSalesPerson)
        Me.Frame1.Controls.Add(Me.optSalesPerson)
        Me.Frame1.Controls.Add(Me.OptDebtors)
        Me.Frame1.Controls.Add(Me.OptCreditors)
        Me.Frame1.Controls.Add(Me.OptGeneral)
        Me.Frame1.Controls.Add(Me.OptExpenses)
        Me.Frame1.Controls.Add(Me.cmdsearch)
        Me.Frame1.Controls.Add(Me.txtLedgerGroup)
        Me.Frame1.Controls.Add(Me.OptGroup)
        Me.Frame1.Controls.Add(Me.OptSelected)
        Me.Frame1.Controls.Add(Me.OptAll)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(335, 179)
        Me.Frame1.TabIndex = 14
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Printing Status"
        '
        'txtSalesPerson
        '
        Me.txtSalesPerson.AcceptsReturn = True
        Me.txtSalesPerson.BackColor = System.Drawing.SystemColors.Window
        Me.txtSalesPerson.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSalesPerson.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSalesPerson.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalesPerson.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSalesPerson.Location = New System.Drawing.Point(16, 154)
        Me.txtSalesPerson.MaxLength = 0
        Me.txtSalesPerson.Name = "txtSalesPerson"
        Me.txtSalesPerson.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSalesPerson.Size = New System.Drawing.Size(279, 20)
        Me.txtSalesPerson.TabIndex = 11
        '
        'optSalesPerson
        '
        Me.optSalesPerson.AutoSize = True
        Me.optSalesPerson.BackColor = System.Drawing.SystemColors.Control
        Me.optSalesPerson.Cursor = System.Windows.Forms.Cursors.Default
        Me.optSalesPerson.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optSalesPerson.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optSalesPerson.Location = New System.Drawing.Point(16, 134)
        Me.optSalesPerson.Name = "optSalesPerson"
        Me.optSalesPerson.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optSalesPerson.Size = New System.Drawing.Size(128, 18)
        Me.optSalesPerson.TabIndex = 9
        Me.optSalesPerson.TabStop = True
        Me.optSalesPerson.Text = "Sales Person Wise"
        Me.optSalesPerson.UseVisualStyleBackColor = False
        '
        'OptDebtors
        '
        Me.OptDebtors.AutoSize = True
        Me.OptDebtors.BackColor = System.Drawing.SystemColors.Control
        Me.OptDebtors.Cursor = System.Windows.Forms.Cursors.Default
        Me.OptDebtors.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptDebtors.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptDebtors.Location = New System.Drawing.Point(16, 66)
        Me.OptDebtors.Name = "OptDebtors"
        Me.OptDebtors.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptDebtors.Size = New System.Drawing.Size(69, 18)
        Me.OptDebtors.TabIndex = 3
        Me.OptDebtors.TabStop = True
        Me.OptDebtors.Text = "Debtors"
        Me.OptDebtors.UseVisualStyleBackColor = False
        '
        'OptCreditors
        '
        Me.OptCreditors.AutoSize = True
        Me.OptCreditors.BackColor = System.Drawing.SystemColors.Control
        Me.OptCreditors.Cursor = System.Windows.Forms.Cursors.Default
        Me.OptCreditors.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptCreditors.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptCreditors.Location = New System.Drawing.Point(171, 66)
        Me.OptCreditors.Name = "OptCreditors"
        Me.OptCreditors.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptCreditors.Size = New System.Drawing.Size(78, 18)
        Me.OptCreditors.TabIndex = 4
        Me.OptCreditors.TabStop = True
        Me.OptCreditors.Text = "Creditors"
        Me.OptCreditors.UseVisualStyleBackColor = False
        '
        'OptGeneral
        '
        Me.OptGeneral.AutoSize = True
        Me.OptGeneral.BackColor = System.Drawing.SystemColors.Control
        Me.OptGeneral.Cursor = System.Windows.Forms.Cursors.Default
        Me.OptGeneral.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptGeneral.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptGeneral.Location = New System.Drawing.Point(171, 43)
        Me.OptGeneral.Name = "OptGeneral"
        Me.OptGeneral.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptGeneral.Size = New System.Drawing.Size(68, 18)
        Me.OptGeneral.TabIndex = 2
        Me.OptGeneral.TabStop = True
        Me.OptGeneral.Text = "General"
        Me.OptGeneral.UseVisualStyleBackColor = False
        '
        'OptExpenses
        '
        Me.OptExpenses.AutoSize = True
        Me.OptExpenses.BackColor = System.Drawing.SystemColors.Control
        Me.OptExpenses.Cursor = System.Windows.Forms.Cursors.Default
        Me.OptExpenses.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptExpenses.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptExpenses.Location = New System.Drawing.Point(16, 43)
        Me.OptExpenses.Name = "OptExpenses"
        Me.OptExpenses.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptExpenses.Size = New System.Drawing.Size(79, 18)
        Me.OptExpenses.TabIndex = 1
        Me.OptExpenses.TabStop = True
        Me.OptExpenses.Text = "Expenses"
        Me.OptExpenses.UseVisualStyleBackColor = False
        '
        'txtLedgerGroup
        '
        Me.txtLedgerGroup.AcceptsReturn = True
        Me.txtLedgerGroup.BackColor = System.Drawing.SystemColors.Window
        Me.txtLedgerGroup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLedgerGroup.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLedgerGroup.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLedgerGroup.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLedgerGroup.Location = New System.Drawing.Point(16, 109)
        Me.txtLedgerGroup.MaxLength = 0
        Me.txtLedgerGroup.Name = "txtLedgerGroup"
        Me.txtLedgerGroup.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLedgerGroup.Size = New System.Drawing.Size(279, 20)
        Me.txtLedgerGroup.TabIndex = 8
        '
        'OptGroup
        '
        Me.OptGroup.AutoSize = True
        Me.OptGroup.BackColor = System.Drawing.SystemColors.Control
        Me.OptGroup.Cursor = System.Windows.Forms.Cursors.Default
        Me.OptGroup.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptGroup.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptGroup.Location = New System.Drawing.Point(171, 89)
        Me.OptGroup.Name = "OptGroup"
        Me.OptGroup.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptGroup.Size = New System.Drawing.Size(89, 18)
        Me.OptGroup.TabIndex = 6
        Me.OptGroup.TabStop = True
        Me.OptGroup.Text = "Group Wise"
        Me.OptGroup.UseVisualStyleBackColor = False
        '
        'OptSelected
        '
        Me.OptSelected.AutoSize = True
        Me.OptSelected.BackColor = System.Drawing.SystemColors.Control
        Me.OptSelected.Checked = True
        Me.OptSelected.Cursor = System.Windows.Forms.Cursors.Default
        Me.OptSelected.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptSelected.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptSelected.Location = New System.Drawing.Point(16, 20)
        Me.OptSelected.Name = "OptSelected"
        Me.OptSelected.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptSelected.Size = New System.Drawing.Size(73, 18)
        Me.OptSelected.TabIndex = 0
        Me.OptSelected.TabStop = True
        Me.OptSelected.Text = "Selected"
        Me.OptSelected.UseVisualStyleBackColor = False
        '
        'OptAll
        '
        Me.OptAll.AutoSize = True
        Me.OptAll.BackColor = System.Drawing.SystemColors.Control
        Me.OptAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.OptAll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptAll.Location = New System.Drawing.Point(16, 89)
        Me.OptAll.Name = "OptAll"
        Me.OptAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptAll.Size = New System.Drawing.Size(39, 18)
        Me.OptAll.TabIndex = 5
        Me.OptAll.TabStop = True
        Me.OptAll.Text = "All"
        Me.OptAll.UseVisualStyleBackColor = False
        '
        'frmPrintLedg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(339, 345)
        Me.Controls.Add(Me.fraPrintOption)
        Me.Controls.Add(Me.FraOk)
        Me.Controls.Add(Me.Frame1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(144, 135)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPrintLedg"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Print"
        Me.fraPrintOption.ResumeLayout(False)
        Me.fraPrintOption.PerformLayout()
        Me.FraOk.ResumeLayout(False)
        Me.FraOk.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        CType(Me.chkPrintOption, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Public WithEvents cmdsearchSale As Button
    Public WithEvents txtSalesPerson As TextBox
    Public WithEvents optSalesPerson As RadioButton
#End Region
End Class