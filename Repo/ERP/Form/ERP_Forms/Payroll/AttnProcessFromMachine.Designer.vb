Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmAttnProcessFromMachine
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
    Public WithEvents chkContractor As System.Windows.Forms.CheckBox
    Public WithEvents cboContractor As System.Windows.Forms.ComboBox
    Public WithEvents Frame7 As System.Windows.Forms.GroupBox
    Public WithEvents cboDept As System.Windows.Forms.ComboBox
    Public WithEvents chkDept As System.Windows.Forms.CheckBox
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents chkShift As System.Windows.Forms.CheckBox
    Public WithEvents cboShift As System.Windows.Forms.ComboBox
    Public WithEvents Frame5 As System.Windows.Forms.GroupBox
    Public WithEvents chkCategory As System.Windows.Forms.CheckBox
    Public WithEvents cboCategory As System.Windows.Forms.ComboBox
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents _optFrom_2 As System.Windows.Forms.RadioButton
    Public WithEvents _optFrom_0 As System.Windows.Forms.RadioButton
    Public WithEvents _optFrom_1 As System.Windows.Forms.RadioButton
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents _optProcess_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optProcess_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents cmdSearch As System.Windows.Forms.Button
    Public WithEvents OptParti As System.Windows.Forms.RadioButton
    Public WithEvents OptAll As System.Windows.Forms.RadioButton
    Public WithEvents TxtCardNo As System.Windows.Forms.TextBox
    Public WithEvents TxtName As System.Windows.Forms.TextBox
    Public WithEvents FraEmp As System.Windows.Forms.GroupBox
    Public WithEvents chkReProcessMannual As System.Windows.Forms.CheckBox
    Public WithEvents chkReProcess As System.Windows.Forms.CheckBox
    Public WithEvents txtAttnDateFrom As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtAttnDateTo As System.Windows.Forms.MaskedTextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents FraPeriod As System.Windows.Forms.GroupBox
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents CmdOK As System.Windows.Forms.Button
    Public WithEvents PBar As System.Windows.Forms.ProgressBar
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents optFrom As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents optProcess As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAttnProcessFromMachine))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearch = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.Frame7 = New System.Windows.Forms.GroupBox()
        Me.chkContractor = New System.Windows.Forms.CheckBox()
        Me.cboContractor = New System.Windows.Forms.ComboBox()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.cboDept = New System.Windows.Forms.ComboBox()
        Me.chkDept = New System.Windows.Forms.CheckBox()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me.chkShift = New System.Windows.Forms.CheckBox()
        Me.cboShift = New System.Windows.Forms.ComboBox()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.chkCategory = New System.Windows.Forms.CheckBox()
        Me.cboCategory = New System.Windows.Forms.ComboBox()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me._optFrom_2 = New System.Windows.Forms.RadioButton()
        Me._optFrom_0 = New System.Windows.Forms.RadioButton()
        Me._optFrom_1 = New System.Windows.Forms.RadioButton()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me._optProcess_1 = New System.Windows.Forms.RadioButton()
        Me._optProcess_0 = New System.Windows.Forms.RadioButton()
        Me.FraEmp = New System.Windows.Forms.GroupBox()
        Me.OptParti = New System.Windows.Forms.RadioButton()
        Me.OptAll = New System.Windows.Forms.RadioButton()
        Me.TxtCardNo = New System.Windows.Forms.TextBox()
        Me.TxtName = New System.Windows.Forms.TextBox()
        Me.FraPeriod = New System.Windows.Forms.GroupBox()
        Me.chkReProcessMannual = New System.Windows.Forms.CheckBox()
        Me.chkReProcess = New System.Windows.Forms.CheckBox()
        Me.txtAttnDateFrom = New System.Windows.Forms.MaskedTextBox()
        Me.txtAttnDateTo = New System.Windows.Forms.MaskedTextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.CmdPopFromFile = New System.Windows.Forms.Button()
        Me.CmdOK = New System.Windows.Forms.Button()
        Me.PBar = New System.Windows.Forms.ProgressBar()
        Me.optFrom = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optProcess = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.CommonDialogOpen = New System.Windows.Forms.OpenFileDialog()
        Me.Frame7.SuspendLayout()
        Me.Frame4.SuspendLayout()
        Me.Frame5.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.FraEmp.SuspendLayout()
        Me.FraPeriod.SuspendLayout()
        Me.Frame2.SuspendLayout()
        CType(Me.optFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optProcess, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSearch
        '
        Me.cmdSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearch.Image = CType(resources.GetObject("cmdSearch.Image"), System.Drawing.Image)
        Me.cmdSearch.Location = New System.Drawing.Point(212, 10)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearch.Size = New System.Drawing.Size(27, 21)
        Me.cmdSearch.TabIndex = 18
        Me.cmdSearch.TabStop = False
        Me.cmdSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearch, "Search")
        Me.cmdSearch.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(294, 33)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(60, 34)
        Me.CmdClose.TabIndex = 23
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close the Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'Frame7
        '
        Me.Frame7.BackColor = System.Drawing.SystemColors.Control
        Me.Frame7.Controls.Add(Me.chkContractor)
        Me.Frame7.Controls.Add(Me.cboContractor)
        Me.Frame7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame7.Location = New System.Drawing.Point(208, 164)
        Me.Frame7.Name = "Frame7"
        Me.Frame7.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame7.Size = New System.Drawing.Size(149, 39)
        Me.Frame7.TabIndex = 33
        Me.Frame7.TabStop = False
        Me.Frame7.Text = "Contractor"
        '
        'chkContractor
        '
        Me.chkContractor.AutoSize = True
        Me.chkContractor.BackColor = System.Drawing.SystemColors.Control
        Me.chkContractor.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkContractor.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkContractor.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkContractor.Location = New System.Drawing.Point(104, 18)
        Me.chkContractor.Name = "chkContractor"
        Me.chkContractor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkContractor.Size = New System.Drawing.Size(46, 18)
        Me.chkContractor.TabIndex = 35
        Me.chkContractor.Text = "ALL"
        Me.chkContractor.UseVisualStyleBackColor = False
        '
        'cboContractor
        '
        Me.cboContractor.BackColor = System.Drawing.SystemColors.Window
        Me.cboContractor.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboContractor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboContractor.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboContractor.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboContractor.Location = New System.Drawing.Point(2, 14)
        Me.cboContractor.Name = "cboContractor"
        Me.cboContractor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboContractor.Size = New System.Drawing.Size(101, 22)
        Me.cboContractor.TabIndex = 34
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.cboDept)
        Me.Frame4.Controls.Add(Me.chkDept)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, 164)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(207, 39)
        Me.Frame4.TabIndex = 30
        Me.Frame4.TabStop = False
        Me.Frame4.Text = "Dept"
        '
        'cboDept
        '
        Me.cboDept.BackColor = System.Drawing.SystemColors.Window
        Me.cboDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDept.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDept.Location = New System.Drawing.Point(2, 14)
        Me.cboDept.Name = "cboDept"
        Me.cboDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDept.Size = New System.Drawing.Size(153, 22)
        Me.cboDept.TabIndex = 12
        '
        'chkDept
        '
        Me.chkDept.AutoSize = True
        Me.chkDept.BackColor = System.Drawing.SystemColors.Control
        Me.chkDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkDept.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDept.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkDept.Location = New System.Drawing.Point(158, 18)
        Me.chkDept.Name = "chkDept"
        Me.chkDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkDept.Size = New System.Drawing.Size(46, 18)
        Me.chkDept.TabIndex = 13
        Me.chkDept.Text = "ALL"
        Me.chkDept.UseVisualStyleBackColor = False
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me.chkShift)
        Me.Frame5.Controls.Add(Me.cboShift)
        Me.Frame5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(210, 124)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(147, 39)
        Me.Frame5.TabIndex = 29
        Me.Frame5.TabStop = False
        Me.Frame5.Text = "Shift"
        '
        'chkShift
        '
        Me.chkShift.AutoSize = True
        Me.chkShift.BackColor = System.Drawing.SystemColors.Control
        Me.chkShift.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkShift.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShift.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkShift.Location = New System.Drawing.Point(64, 18)
        Me.chkShift.Name = "chkShift"
        Me.chkShift.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkShift.Size = New System.Drawing.Size(46, 18)
        Me.chkShift.TabIndex = 11
        Me.chkShift.Text = "ALL"
        Me.chkShift.UseVisualStyleBackColor = False
        '
        'cboShift
        '
        Me.cboShift.BackColor = System.Drawing.SystemColors.Window
        Me.cboShift.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboShift.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboShift.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboShift.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboShift.Location = New System.Drawing.Point(2, 14)
        Me.cboShift.Name = "cboShift"
        Me.cboShift.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboShift.Size = New System.Drawing.Size(59, 22)
        Me.cboShift.TabIndex = 10
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.chkCategory)
        Me.Frame6.Controls.Add(Me.cboCategory)
        Me.Frame6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(0, 124)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(209, 39)
        Me.Frame6.TabIndex = 28
        Me.Frame6.TabStop = False
        Me.Frame6.Text = "Category"
        '
        'chkCategory
        '
        Me.chkCategory.AutoSize = True
        Me.chkCategory.BackColor = System.Drawing.SystemColors.Control
        Me.chkCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCategory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCategory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCategory.Location = New System.Drawing.Point(162, 18)
        Me.chkCategory.Name = "chkCategory"
        Me.chkCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCategory.Size = New System.Drawing.Size(46, 18)
        Me.chkCategory.TabIndex = 9
        Me.chkCategory.Text = "ALL"
        Me.chkCategory.UseVisualStyleBackColor = False
        '
        'cboCategory
        '
        Me.cboCategory.BackColor = System.Drawing.SystemColors.Window
        Me.cboCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCategory.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCategory.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCategory.Location = New System.Drawing.Point(4, 14)
        Me.cboCategory.Name = "cboCategory"
        Me.cboCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCategory.Size = New System.Drawing.Size(155, 22)
        Me.cboCategory.TabIndex = 8
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me._optFrom_2)
        Me.Frame3.Controls.Add(Me._optFrom_0)
        Me.Frame3.Controls.Add(Me._optFrom_1)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 58)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(355, 35)
        Me.Frame3.TabIndex = 26
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "From"
        '
        '_optFrom_2
        '
        Me._optFrom_2.AutoSize = True
        Me._optFrom_2.BackColor = System.Drawing.SystemColors.Control
        Me._optFrom_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optFrom_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optFrom_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optFrom.SetIndex(Me._optFrom_2, CType(2, Short))
        Me._optFrom_2.Location = New System.Drawing.Point(86, 14)
        Me._optFrom_2.Name = "_optFrom_2"
        Me._optFrom_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optFrom_2.Size = New System.Drawing.Size(134, 18)
        Me._optFrom_2.TabIndex = 4
        Me._optFrom_2.TabStop = True
        Me._optFrom_2.Text = "Mannual (As per Shift)"
        Me._optFrom_2.UseVisualStyleBackColor = False
        '
        '_optFrom_0
        '
        Me._optFrom_0.AutoSize = True
        Me._optFrom_0.BackColor = System.Drawing.SystemColors.Control
        Me._optFrom_0.Checked = True
        Me._optFrom_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optFrom_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optFrom_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optFrom.SetIndex(Me._optFrom_0, CType(0, Short))
        Me._optFrom_0.Location = New System.Drawing.Point(6, 14)
        Me._optFrom_0.Name = "_optFrom_0"
        Me._optFrom_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optFrom_0.Size = New System.Drawing.Size(65, 18)
        Me._optFrom_0.TabIndex = 3
        Me._optFrom_0.TabStop = True
        Me._optFrom_0.Text = "Machine"
        Me._optFrom_0.UseVisualStyleBackColor = False
        '
        '_optFrom_1
        '
        Me._optFrom_1.AutoSize = True
        Me._optFrom_1.BackColor = System.Drawing.SystemColors.Control
        Me._optFrom_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optFrom_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optFrom_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optFrom.SetIndex(Me._optFrom_1, CType(1, Short))
        Me._optFrom_1.Location = New System.Drawing.Point(246, 14)
        Me._optFrom_1.Name = "_optFrom_1"
        Me._optFrom_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optFrom_1.Size = New System.Drawing.Size(90, 18)
        Me._optFrom_1.TabIndex = 5
        Me._optFrom_1.TabStop = True
        Me._optFrom_1.Text = "Mannual Data"
        Me._optFrom_1.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me._optProcess_1)
        Me.Frame1.Controls.Add(Me._optProcess_0)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 94)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(356, 29)
        Me.Frame1.TabIndex = 25
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Process"
        '
        '_optProcess_1
        '
        Me._optProcess_1.BackColor = System.Drawing.SystemColors.Control
        Me._optProcess_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optProcess_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optProcess_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optProcess.SetIndex(Me._optProcess_1, CType(1, Short))
        Me._optProcess_1.Location = New System.Drawing.Point(192, 10)
        Me._optProcess_1.Name = "_optProcess_1"
        Me._optProcess_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optProcess_1.Size = New System.Drawing.Size(113, 15)
        Me._optProcess_1.TabIndex = 7
        Me._optProcess_1.TabStop = True
        Me._optProcess_1.Text = "Contractor"
        Me._optProcess_1.UseVisualStyleBackColor = False
        '
        '_optProcess_0
        '
        Me._optProcess_0.BackColor = System.Drawing.SystemColors.Control
        Me._optProcess_0.Checked = True
        Me._optProcess_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optProcess_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optProcess_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optProcess.SetIndex(Me._optProcess_0, CType(0, Short))
        Me._optProcess_0.Location = New System.Drawing.Point(62, 10)
        Me._optProcess_0.Name = "_optProcess_0"
        Me._optProcess_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optProcess_0.Size = New System.Drawing.Size(79, 15)
        Me._optProcess_0.TabIndex = 6
        Me._optProcess_0.TabStop = True
        Me._optProcess_0.Text = "Staff"
        Me._optProcess_0.UseVisualStyleBackColor = False
        '
        'FraEmp
        '
        Me.FraEmp.BackColor = System.Drawing.SystemColors.Control
        Me.FraEmp.Controls.Add(Me.cmdSearch)
        Me.FraEmp.Controls.Add(Me.OptParti)
        Me.FraEmp.Controls.Add(Me.OptAll)
        Me.FraEmp.Controls.Add(Me.TxtCardNo)
        Me.FraEmp.Controls.Add(Me.TxtName)
        Me.FraEmp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraEmp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraEmp.Location = New System.Drawing.Point(0, 204)
        Me.FraEmp.Name = "FraEmp"
        Me.FraEmp.Padding = New System.Windows.Forms.Padding(0)
        Me.FraEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraEmp.Size = New System.Drawing.Size(357, 57)
        Me.FraEmp.TabIndex = 22
        Me.FraEmp.TabStop = False
        Me.FraEmp.Text = "Employee"
        '
        'OptParti
        '
        Me.OptParti.AutoSize = True
        Me.OptParti.BackColor = System.Drawing.SystemColors.Control
        Me.OptParti.Cursor = System.Windows.Forms.Cursors.Default
        Me.OptParti.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptParti.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptParti.Location = New System.Drawing.Point(10, 34)
        Me.OptParti.Name = "OptParti"
        Me.OptParti.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptParti.Size = New System.Drawing.Size(73, 18)
        Me.OptParti.TabIndex = 16
        Me.OptParti.TabStop = True
        Me.OptParti.Text = "Particular "
        Me.OptParti.UseVisualStyleBackColor = False
        '
        'OptAll
        '
        Me.OptAll.AutoSize = True
        Me.OptAll.BackColor = System.Drawing.SystemColors.Control
        Me.OptAll.Checked = True
        Me.OptAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.OptAll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptAll.Location = New System.Drawing.Point(10, 18)
        Me.OptAll.Name = "OptAll"
        Me.OptAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptAll.Size = New System.Drawing.Size(40, 18)
        Me.OptAll.TabIndex = 14
        Me.OptAll.TabStop = True
        Me.OptAll.Text = "All "
        Me.OptAll.UseVisualStyleBackColor = False
        '
        'TxtCardNo
        '
        Me.TxtCardNo.AcceptsReturn = True
        Me.TxtCardNo.BackColor = System.Drawing.SystemColors.Window
        Me.TxtCardNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtCardNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtCardNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCardNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtCardNo.Location = New System.Drawing.Point(104, 10)
        Me.TxtCardNo.MaxLength = 0
        Me.TxtCardNo.Name = "TxtCardNo"
        Me.TxtCardNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtCardNo.Size = New System.Drawing.Size(107, 20)
        Me.TxtCardNo.TabIndex = 17
        '
        'TxtName
        '
        Me.TxtName.AcceptsReturn = True
        Me.TxtName.BackColor = System.Drawing.SystemColors.Window
        Me.TxtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtName.Enabled = False
        Me.TxtName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtName.Location = New System.Drawing.Point(104, 32)
        Me.TxtName.MaxLength = 0
        Me.TxtName.Name = "TxtName"
        Me.TxtName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtName.Size = New System.Drawing.Size(249, 20)
        Me.TxtName.TabIndex = 19
        '
        'FraPeriod
        '
        Me.FraPeriod.BackColor = System.Drawing.SystemColors.Control
        Me.FraPeriod.Controls.Add(Me.chkReProcessMannual)
        Me.FraPeriod.Controls.Add(Me.chkReProcess)
        Me.FraPeriod.Controls.Add(Me.txtAttnDateFrom)
        Me.FraPeriod.Controls.Add(Me.txtAttnDateTo)
        Me.FraPeriod.Controls.Add(Me.Label2)
        Me.FraPeriod.Controls.Add(Me.Label1)
        Me.FraPeriod.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraPeriod.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraPeriod.Location = New System.Drawing.Point(0, -2)
        Me.FraPeriod.Name = "FraPeriod"
        Me.FraPeriod.Padding = New System.Windows.Forms.Padding(0)
        Me.FraPeriod.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraPeriod.Size = New System.Drawing.Size(356, 59)
        Me.FraPeriod.TabIndex = 21
        Me.FraPeriod.TabStop = False
        Me.FraPeriod.Text = "Date"
        '
        'chkReProcessMannual
        '
        Me.chkReProcessMannual.BackColor = System.Drawing.SystemColors.Control
        Me.chkReProcessMannual.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkReProcessMannual.Enabled = False
        Me.chkReProcessMannual.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkReProcessMannual.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkReProcessMannual.Location = New System.Drawing.Point(174, 28)
        Me.chkReProcessMannual.Name = "chkReProcessMannual"
        Me.chkReProcessMannual.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkReProcessMannual.Size = New System.Drawing.Size(181, 15)
        Me.chkReProcessMannual.TabIndex = 32
        Me.chkReProcessMannual.Text = "Re-Process Mannual Entry"
        Me.chkReProcessMannual.UseVisualStyleBackColor = False
        '
        'chkReProcess
        '
        Me.chkReProcess.BackColor = System.Drawing.SystemColors.Control
        Me.chkReProcess.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkReProcess.Enabled = False
        Me.chkReProcess.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkReProcess.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkReProcess.Location = New System.Drawing.Point(174, 14)
        Me.chkReProcess.Name = "chkReProcess"
        Me.chkReProcess.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkReProcess.Size = New System.Drawing.Size(181, 15)
        Me.chkReProcess.TabIndex = 2
        Me.chkReProcess.Text = "Re-Process"
        Me.chkReProcess.UseVisualStyleBackColor = False
        '
        'txtAttnDateFrom
        '
        Me.txtAttnDateFrom.AllowPromptAsInput = False
        Me.txtAttnDateFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAttnDateFrom.Location = New System.Drawing.Point(86, 10)
        Me.txtAttnDateFrom.Mask = "##/##/####"
        Me.txtAttnDateFrom.Name = "txtAttnDateFrom"
        Me.txtAttnDateFrom.Size = New System.Drawing.Size(81, 20)
        Me.txtAttnDateFrom.TabIndex = 0
        '
        'txtAttnDateTo
        '
        Me.txtAttnDateTo.AllowPromptAsInput = False
        Me.txtAttnDateTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAttnDateTo.Location = New System.Drawing.Point(86, 34)
        Me.txtAttnDateTo.Mask = "##/##/####"
        Me.txtAttnDateTo.Name = "txtAttnDateTo"
        Me.txtAttnDateTo.Size = New System.Drawing.Size(81, 20)
        Me.txtAttnDateTo.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(20, 36)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(49, 14)
        Me.Label2.TabIndex = 31
        Me.Label2.Text = "To Date :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(20, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(62, 14)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = "From Date :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.CmdPopFromFile)
        Me.Frame2.Controls.Add(Me.CmdClose)
        Me.Frame2.Controls.Add(Me.CmdOK)
        Me.Frame2.Controls.Add(Me.PBar)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 256)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(357, 70)
        Me.Frame2.TabIndex = 15
        Me.Frame2.TabStop = False
        '
        'CmdPopFromFile
        '
        Me.CmdPopFromFile.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPopFromFile.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPopFromFile.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPopFromFile.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPopFromFile.Location = New System.Drawing.Point(97, 33)
        Me.CmdPopFromFile.Name = "CmdPopFromFile"
        Me.CmdPopFromFile.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPopFromFile.Size = New System.Drawing.Size(162, 31)
        Me.CmdPopFromFile.TabIndex = 58
        Me.CmdPopFromFile.Text = "Upload from Excel"
        Me.CmdPopFromFile.UseVisualStyleBackColor = False
        '
        'CmdOK
        '
        Me.CmdOK.BackColor = System.Drawing.SystemColors.Control
        Me.CmdOK.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdOK.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdOK.Location = New System.Drawing.Point(4, 33)
        Me.CmdOK.Name = "CmdOK"
        Me.CmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdOK.Size = New System.Drawing.Size(60, 33)
        Me.CmdOK.TabIndex = 20
        Me.CmdOK.Text = "Ok"
        Me.CmdOK.UseVisualStyleBackColor = False
        '
        'PBar
        '
        Me.PBar.Location = New System.Drawing.Point(68, 11)
        Me.PBar.Name = "PBar"
        Me.PBar.Size = New System.Drawing.Size(223, 13)
        Me.PBar.TabIndex = 24
        Me.PBar.Visible = False
        '
        'optProcess
        '
        '
        'FrmAttnProcessFromMachine
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(357, 327)
        Me.Controls.Add(Me.Frame7)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.Frame5)
        Me.Controls.Add(Me.Frame6)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.FraEmp)
        Me.Controls.Add(Me.FraPeriod)
        Me.Controls.Add(Me.Frame2)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "FrmAttnProcessFromMachine"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Attendance Process from Machine"
        Me.Frame7.ResumeLayout(False)
        Me.Frame7.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me.Frame5.ResumeLayout(False)
        Me.Frame5.PerformLayout()
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.FraEmp.ResumeLayout(False)
        Me.FraEmp.PerformLayout()
        Me.FraPeriod.ResumeLayout(False)
        Me.FraPeriod.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        CType(Me.optFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optProcess, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Public WithEvents CmdPopFromFile As Button
    Public WithEvents CommonDialogOpen As OpenFileDialog
#End Region
End Class