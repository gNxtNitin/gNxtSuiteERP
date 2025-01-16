Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmEncashProcess
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
    Public WithEvents cboEmpType As System.Windows.Forms.ComboBox
    Public WithEvents Frame9 As System.Windows.Forms.GroupBox
    Public WithEvents cmdSearch As System.Windows.Forms.Button
    Public WithEvents OptParti As System.Windows.Forms.RadioButton
    Public WithEvents OptAll As System.Windows.Forms.RadioButton
    Public WithEvents TxtCardNo As System.Windows.Forms.TextBox
    Public WithEvents TxtName As System.Windows.Forms.TextBox
    Public WithEvents FraEmp As System.Windows.Forms.GroupBox
    Public WithEvents _OptLeave_1 As System.Windows.Forms.RadioButton
    Public WithEvents _OptLeave_0 As System.Windows.Forms.RadioButton
    Public WithEvents chkDeposit As System.Windows.Forms.CheckBox
    Public WithEvents UpDMonth As System.Windows.Forms.NumericUpDown     ''AxComCtl2.AxUpDown
    Public WithEvents txtMonth As System.Windows.Forms.TextBox
    Public WithEvents txtAsOn As System.Windows.Forms.MaskedTextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents lblBookType As System.Windows.Forms.Label
    Public WithEvents lblProcessType As System.Windows.Forms.Label
    Public WithEvents lblNewDate As System.Windows.Forms.Label
    Public WithEvents lblMonth As System.Windows.Forms.Label
    Public WithEvents FraPeriod As System.Windows.Forms.GroupBox
    Public WithEvents cmdUnProcess As System.Windows.Forms.Button
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents CmdOK As System.Windows.Forms.Button
    Public WithEvents PBar As System.Windows.Forms.ProgressBar
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents OptLeave As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(FrmEncashProcess))
        Me.components = New System.ComponentModel.Container()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
        Me.Frame9 = New System.Windows.Forms.GroupBox
        Me.cboEmpType = New System.Windows.Forms.ComboBox
        Me.FraEmp = New System.Windows.Forms.GroupBox
        Me.cmdSearch = New System.Windows.Forms.Button
        Me.OptParti = New System.Windows.Forms.RadioButton
        Me.OptAll = New System.Windows.Forms.RadioButton
        Me.TxtCardNo = New System.Windows.Forms.TextBox
        Me.TxtName = New System.Windows.Forms.TextBox
        Me.FraPeriod = New System.Windows.Forms.GroupBox
        Me._OptLeave_1 = New System.Windows.Forms.RadioButton
        Me._OptLeave_0 = New System.Windows.Forms.RadioButton
        Me.chkDeposit = New System.Windows.Forms.CheckBox
        Me.UpDMonth = New System.Windows.Forms.NumericUpDown     ''AxComCtl2.AxUpDown
        Me.txtMonth = New System.Windows.Forms.TextBox
        Me.txtAsOn = New System.Windows.Forms.MaskedTextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblBookType = New System.Windows.Forms.Label
        Me.lblProcessType = New System.Windows.Forms.Label
        Me.lblNewDate = New System.Windows.Forms.Label
        Me.lblMonth = New System.Windows.Forms.Label
        Me.Frame2 = New System.Windows.Forms.GroupBox
        Me.cmdUnProcess = New System.Windows.Forms.Button
        Me.CmdClose = New System.Windows.Forms.Button
        Me.CmdOK = New System.Windows.Forms.Button
        Me.PBar = New System.Windows.Forms.ProgressBar
        Me.OptLeave = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(components)
        Me.Frame9.SuspendLayout()
        Me.FraEmp.SuspendLayout()
        Me.FraPeriod.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.SuspendLayout()
        Me.ToolTip1.Active = True
        CType(Me.UpDMonth, System.ComponentModel.ISupportInitialize).BeginInit()
        'CType(Me.PBar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptLeave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Text = "Leave Encashment Process"
        Me.ClientSize = New System.Drawing.Size(357, 230)
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Icon = CType(resources.GetObject("FrmEncashProcess.Icon"), System.Drawing.Icon)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable
        Me.ControlBox = True
        Me.Enabled = True
        Me.KeyPreview = False
        Me.MaximizeBox = True
        Me.MinimizeBox = True
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = True
        Me.HelpButton = False
        Me.WindowState = System.Windows.Forms.FormWindowState.Normal
        Me.Name = "FrmEncashProcess"
        Me.Frame9.Text = "Emp Type"
        Me.Frame9.Size = New System.Drawing.Size(357, 41)
        Me.Frame9.Location = New System.Drawing.Point(0, 84)
        Me.Frame9.TabIndex = 23
        Me.Frame9.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame9.BackColor = System.Drawing.SystemColors.Control
        Me.Frame9.Enabled = True
        Me.Frame9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame9.Visible = True
        Me.Frame9.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame9.Name = "Frame9"
        Me.cboEmpType.Size = New System.Drawing.Size(241, 21)
        Me.cboEmpType.Location = New System.Drawing.Point(106, 14)
        Me.cboEmpType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEmpType.TabIndex = 24
        Me.cboEmpType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboEmpType.BackColor = System.Drawing.SystemColors.Window
        Me.cboEmpType.CausesValidation = True
        Me.cboEmpType.Enabled = True
        Me.cboEmpType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboEmpType.IntegralHeight = True
        Me.cboEmpType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboEmpType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboEmpType.Sorted = False
        Me.cboEmpType.TabStop = True
        Me.cboEmpType.Visible = True
        Me.cboEmpType.Name = "cboEmpType"
        Me.FraEmp.Text = "Employee"
        Me.FraEmp.Size = New System.Drawing.Size(357, 61)
        Me.FraEmp.Location = New System.Drawing.Point(0, 126)
        Me.FraEmp.TabIndex = 8
        Me.FraEmp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraEmp.BackColor = System.Drawing.SystemColors.Control
        Me.FraEmp.Enabled = True
        Me.FraEmp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraEmp.Visible = True
        Me.FraEmp.Padding = New System.Windows.Forms.Padding(0)
        Me.FraEmp.Name = "FraEmp"
        Me.cmdSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearch.Size = New System.Drawing.Size(27, 21)
        Me.cmdSearch.Location = New System.Drawing.Point(212, 12)
        Me.cmdSearch.Image = CType(resources.GetObject("cmdSearch.Image"), System.Drawing.Image)
        Me.cmdSearch.TabIndex = 4
        Me.cmdSearch.TabStop = False
        Me.ToolTip1.SetToolTip(Me.cmdSearch, "Search")
        Me.cmdSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearch.CausesValidation = True
        Me.cmdSearch.Enabled = True
        Me.cmdSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearch.Name = "cmdSearch"
        Me.OptParti.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.OptParti.Text = "Particular "
        Me.OptParti.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptParti.Size = New System.Drawing.Size(91, 18)
        Me.OptParti.Location = New System.Drawing.Point(10, 38)
        Me.OptParti.TabIndex = 1
        Me.OptParti.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.OptParti.BackColor = System.Drawing.SystemColors.Control
        Me.OptParti.CausesValidation = True
        Me.OptParti.Enabled = True
        Me.OptParti.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptParti.Cursor = System.Windows.Forms.Cursors.Default
        Me.OptParti.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptParti.Appearance = System.Windows.Forms.Appearance.Normal
        Me.OptParti.TabStop = True
        Me.OptParti.Checked = False
        Me.OptParti.Visible = True
        Me.OptParti.Name = "OptParti"
        Me.OptAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.OptAll.Text = "All "
        Me.OptAll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptAll.Size = New System.Drawing.Size(91, 13)
        Me.OptAll.Location = New System.Drawing.Point(10, 20)
        Me.OptAll.TabIndex = 0
        Me.OptAll.Checked = True
        Me.OptAll.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.OptAll.BackColor = System.Drawing.SystemColors.Control
        Me.OptAll.CausesValidation = True
        Me.OptAll.Enabled = True
        Me.OptAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.OptAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptAll.Appearance = System.Windows.Forms.Appearance.Normal
        Me.OptAll.TabStop = True
        Me.OptAll.Visible = True
        Me.OptAll.Name = "OptAll"
        Me.TxtCardNo.AutoSize = False
        Me.TxtCardNo.Size = New System.Drawing.Size(107, 21)
        Me.TxtCardNo.Location = New System.Drawing.Point(104, 12)
        Me.TxtCardNo.TabIndex = 3
        Me.TxtCardNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCardNo.AcceptsReturn = True
        Me.TxtCardNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.TxtCardNo.BackColor = System.Drawing.SystemColors.Window
        Me.TxtCardNo.CausesValidation = True
        Me.TxtCardNo.Enabled = True
        Me.TxtCardNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtCardNo.HideSelection = True
        Me.TxtCardNo.ReadOnly = False
        Me.TxtCardNo.MaxLength = 0
        Me.TxtCardNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtCardNo.Multiline = False
        Me.TxtCardNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtCardNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.TxtCardNo.TabStop = True
        Me.TxtCardNo.Visible = True
        Me.TxtCardNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtCardNo.Name = "TxtCardNo"
        Me.TxtName.AutoSize = False
        Me.TxtName.Enabled = False
        Me.TxtName.Size = New System.Drawing.Size(249, 21)
        Me.TxtName.Location = New System.Drawing.Point(104, 36)
        Me.TxtName.TabIndex = 5
        Me.TxtName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtName.AcceptsReturn = True
        Me.TxtName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.TxtName.BackColor = System.Drawing.SystemColors.Window
        Me.TxtName.CausesValidation = True
        Me.TxtName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtName.HideSelection = True
        Me.TxtName.ReadOnly = False
        Me.TxtName.MaxLength = 0
        Me.TxtName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtName.Multiline = False
        Me.TxtName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtName.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.TxtName.TabStop = True
        Me.TxtName.Visible = True
        Me.TxtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtName.Name = "TxtName"
        Me.FraPeriod.Text = "Period"
        Me.FraPeriod.Size = New System.Drawing.Size(356, 85)
        Me.FraPeriod.Location = New System.Drawing.Point(0, -2)
        Me.FraPeriod.TabIndex = 7
        Me.FraPeriod.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraPeriod.BackColor = System.Drawing.SystemColors.Control
        Me.FraPeriod.Enabled = True
        Me.FraPeriod.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraPeriod.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraPeriod.Visible = True
        Me.FraPeriod.Padding = New System.Windows.Forms.Padding(0)
        Me.FraPeriod.Name = "FraPeriod"
        Me._OptLeave_1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me._OptLeave_1.Text = "Only EL"
        Me._OptLeave_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptLeave_1.Size = New System.Drawing.Size(105, 17)
        Me._OptLeave_1.Location = New System.Drawing.Point(188, 66)
        Me._OptLeave_1.TabIndex = 21
        Me._OptLeave_1.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me._OptLeave_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptLeave_1.CausesValidation = True
        Me._OptLeave_1.Enabled = True
        Me._OptLeave_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me._OptLeave_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptLeave_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptLeave_1.Appearance = System.Windows.Forms.Appearance.Normal
        Me._OptLeave_1.TabStop = True
        Me._OptLeave_1.Checked = False
        Me._OptLeave_1.Visible = True
        Me._OptLeave_1.Name = "_OptLeave_1"
        Me._OptLeave_0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me._OptLeave_0.Text = "All Leaves"
        Me._OptLeave_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptLeave_0.Size = New System.Drawing.Size(89, 17)
        Me._OptLeave_0.Location = New System.Drawing.Point(84, 66)
        Me._OptLeave_0.TabIndex = 20
        Me._OptLeave_0.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me._OptLeave_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptLeave_0.CausesValidation = True
        Me._OptLeave_0.Enabled = True
        Me._OptLeave_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._OptLeave_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptLeave_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptLeave_0.Appearance = System.Windows.Forms.Appearance.Normal
        Me._OptLeave_0.TabStop = True
        Me._OptLeave_0.Checked = False
        Me._OptLeave_0.Visible = True
        Me._OptLeave_0.Name = "_OptLeave_0"
        Me.chkDeposit.Text = "Deposit Leaves"
        Me.chkDeposit.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDeposit.Size = New System.Drawing.Size(113, 17)
        Me.chkDeposit.Location = New System.Drawing.Point(240, 44)
        Me.chkDeposit.TabIndex = 19
        Me.chkDeposit.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkDeposit.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.chkDeposit.FlatStyle = System.Windows.Forms.FlatStyle.Standard
        Me.chkDeposit.BackColor = System.Drawing.SystemColors.Control
        Me.chkDeposit.CausesValidation = True
        Me.chkDeposit.Enabled = True
        Me.chkDeposit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkDeposit.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkDeposit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkDeposit.Appearance = System.Windows.Forms.Appearance.Normal
        Me.chkDeposit.TabStop = True
        Me.chkDeposit.Visible = True
        Me.chkDeposit.Name = "chkDeposit"
        'UpDMonth.OcxState = CType(resources.GetObject("UpDMonth.OcxState"), System.Windows.Forms.AxHost.State)
        Me.UpDMonth.Size = New System.Drawing.Size(16, 21)
        Me.UpDMonth.Location = New System.Drawing.Point(238, 16)
        Me.UpDMonth.TabIndex = 13
        Me.UpDMonth.Name = "UpDMonth"
        Me.txtMonth.AutoSize = False
        Me.txtMonth.Size = New System.Drawing.Size(123, 21)
        Me.txtMonth.Location = New System.Drawing.Point(132, 16)
        Me.txtMonth.TabIndex = 12
        Me.txtMonth.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMonth.AcceptsReturn = True
        Me.txtMonth.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtMonth.BackColor = System.Drawing.SystemColors.Window
        Me.txtMonth.CausesValidation = True
        Me.txtMonth.Enabled = True
        Me.txtMonth.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMonth.HideSelection = True
        Me.txtMonth.ReadOnly = False
        Me.txtMonth.MaxLength = 0
        Me.txtMonth.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMonth.Multiline = False
        Me.txtMonth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMonth.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtMonth.TabStop = True
        Me.txtMonth.Visible = True
        Me.txtMonth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMonth.Name = "txtMonth"
        Me.txtAsOn.AllowPromptAsInput = False
        Me.txtAsOn.Size = New System.Drawing.Size(80, 21)
        Me.txtAsOn.Location = New System.Drawing.Point(132, 42)
        Me.txtAsOn.TabIndex = 17
        Me.txtAsOn.MaxLength = 10
        Me.txtAsOn.Mask = "##/##/####"
        Me.txtAsOn.PromptChar = "_"
        Me.txtAsOn.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAsOn.Name = "txtAsOn"
        Me.Label1.Text = "As On :"
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Size = New System.Drawing.Size(43, 13)
        Me.Label1.Location = New System.Drawing.Point(80, 46)
        Me.Label1.TabIndex = 18
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
        Me.lblBookType.Text = "lblBookType"
        Me.lblBookType.Size = New System.Drawing.Size(59, 13)
        Me.lblBookType.Location = New System.Drawing.Point(4, 16)
        Me.lblBookType.TabIndex = 16
        Me.lblBookType.Visible = False
        Me.lblBookType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Enabled = True
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.UseMnemonic = True
        Me.lblBookType.AutoSize = True
        Me.lblBookType.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblBookType.Name = "lblBookType"
        Me.lblProcessType.Text = "lblProcessType"
        Me.lblProcessType.Size = New System.Drawing.Size(72, 13)
        Me.lblProcessType.Location = New System.Drawing.Point(4, 48)
        Me.lblProcessType.TabIndex = 15
        Me.lblProcessType.Visible = False
        Me.lblProcessType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProcessType.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblProcessType.BackColor = System.Drawing.SystemColors.Control
        Me.lblProcessType.Enabled = True
        Me.lblProcessType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblProcessType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblProcessType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblProcessType.UseMnemonic = True
        Me.lblProcessType.AutoSize = True
        Me.lblProcessType.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblProcessType.Name = "lblProcessType"
        Me.lblNewDate.Text = "NewDate"
        Me.lblNewDate.Size = New System.Drawing.Size(63, 17)
        Me.lblNewDate.Location = New System.Drawing.Point(4, 30)
        Me.lblNewDate.TabIndex = 14
        Me.lblNewDate.Visible = False
        Me.lblNewDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNewDate.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblNewDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblNewDate.Enabled = True
        Me.lblNewDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblNewDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblNewDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblNewDate.UseMnemonic = True
        Me.lblNewDate.AutoSize = False
        Me.lblNewDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblNewDate.Name = "lblNewDate"
        Me.lblMonth.Text = "Year :"
        Me.lblMonth.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMonth.Size = New System.Drawing.Size(35, 13)
        Me.lblMonth.Location = New System.Drawing.Point(88, 20)
        Me.lblMonth.TabIndex = 11
        Me.lblMonth.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblMonth.BackColor = System.Drawing.SystemColors.Control
        Me.lblMonth.Enabled = True
        Me.lblMonth.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMonth.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMonth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMonth.UseMnemonic = True
        Me.lblMonth.Visible = True
        Me.lblMonth.AutoSize = True
        Me.lblMonth.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblMonth.Name = "lblMonth"
        Me.Frame2.Size = New System.Drawing.Size(357, 47)
        Me.Frame2.Location = New System.Drawing.Point(0, 182)
        Me.Frame2.TabIndex = 2
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Enabled = True
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Visible = True
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.Name = "Frame2"
        Me.cmdUnProcess.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdUnProcess.Text = "Un-Process"
        Me.cmdUnProcess.Size = New System.Drawing.Size(68, 33)
        Me.cmdUnProcess.Location = New System.Drawing.Point(148, 10)
        Me.cmdUnProcess.TabIndex = 22
        Me.cmdUnProcess.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdUnProcess.BackColor = System.Drawing.SystemColors.Control
        Me.cmdUnProcess.CausesValidation = True
        Me.cmdUnProcess.Enabled = True
        Me.cmdUnProcess.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdUnProcess.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdUnProcess.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdUnProcess.TabStop = True
        Me.cmdUnProcess.Name = "cmdUnProcess"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.Size = New System.Drawing.Size(60, 34)
        Me.CmdClose.Location = New System.Drawing.Point(294, 10)
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close the Form")
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.CausesValidation = True
        Me.CmdClose.Enabled = True
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.TabStop = True
        Me.CmdClose.Name = "CmdClose"
        Me.CmdOK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdOK.Text = "Ok"
        Me.CmdOK.Size = New System.Drawing.Size(60, 33)
        Me.CmdOK.Location = New System.Drawing.Point(4, 10)
        Me.CmdOK.TabIndex = 6
        Me.CmdOK.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdOK.BackColor = System.Drawing.SystemColors.Control
        Me.CmdOK.CausesValidation = True
        Me.CmdOK.Enabled = True
        Me.CmdOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdOK.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdOK.TabStop = True
        Me.CmdOK.Name = "CmdOK"
        'PBar.OcxState = CType(resources.GetObject("PBar.OcxState"), System.Windows.Forms.AxHost.State)
        Me.PBar.Size = New System.Drawing.Size(223, 13)
        Me.PBar.Location = New System.Drawing.Point(68, 28)
        Me.PBar.TabIndex = 10
        Me.PBar.Visible = False
        Me.PBar.Name = "PBar"
        Me.OptLeave.SetIndex(_OptLeave_1, CType(1, Short))
        Me.OptLeave.SetIndex(_OptLeave_0, CType(0, Short))
        CType(Me.OptLeave, System.ComponentModel.ISupportInitialize).EndInit()
        'CType(Me.PBar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UpDMonth, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Controls.Add(Frame9)
        Me.Controls.Add(FraEmp)
        Me.Controls.Add(FraPeriod)
        Me.Controls.Add(Frame2)
        Me.Frame9.Controls.Add(cboEmpType)
        Me.FraEmp.Controls.Add(cmdSearch)
        Me.FraEmp.Controls.Add(OptParti)
        Me.FraEmp.Controls.Add(OptAll)
        Me.FraEmp.Controls.Add(TxtCardNo)
        Me.FraEmp.Controls.Add(TxtName)
        Me.FraPeriod.Controls.Add(_OptLeave_1)
        Me.FraPeriod.Controls.Add(_OptLeave_0)
        Me.FraPeriod.Controls.Add(chkDeposit)
        Me.FraPeriod.Controls.Add(UpDMonth)
        Me.FraPeriod.Controls.Add(txtMonth)
        Me.FraPeriod.Controls.Add(txtAsOn)
        Me.FraPeriod.Controls.Add(Label1)
        Me.FraPeriod.Controls.Add(lblBookType)
        Me.FraPeriod.Controls.Add(lblProcessType)
        Me.FraPeriod.Controls.Add(lblNewDate)
        Me.FraPeriod.Controls.Add(lblMonth)
        Me.Frame2.Controls.Add(cmdUnProcess)
        Me.Frame2.Controls.Add(CmdClose)
        Me.Frame2.Controls.Add(CmdOK)
        Me.Frame2.Controls.Add(PBar)
        Me.Frame9.ResumeLayout(False)
        Me.FraEmp.ResumeLayout(False)
        Me.FraPeriod.ResumeLayout(False)
        Me.Frame2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub
#End Region
End Class