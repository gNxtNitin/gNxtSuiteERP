Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmGenSalIncrement
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
    Public WithEvents cboDept As System.Windows.Forms.ComboBox
    Public WithEvents chkAll As System.Windows.Forms.CheckBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents cboCategory As System.Windows.Forms.ComboBox
    Public WithEvents chkCategory As System.Windows.Forms.CheckBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents txtBasicSalary As System.Windows.Forms.TextBox
    Public WithEvents _optBasicSalary_2 As System.Windows.Forms.RadioButton
    Public WithEvents _optBasicSalary_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optBasicSalary_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents cboAppMon As System.Windows.Forms.ComboBox
    Public WithEvents cboAppYear As System.Windows.Forms.ComboBox
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents txtWEF As System.Windows.Forms.TextBox
    Public WithEvents lblAppDate As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents lblWEF As System.Windows.Forms.Label
    Public WithEvents fraTop As System.Windows.Forms.GroupBox
    Public WithEvents cboArrearMonth As System.Windows.Forms.ComboBox
    Public WithEvents cboArrearYear As System.Windows.Forms.ComboBox
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents cboYear As System.Windows.Forms.ComboBox
    Public WithEvents cboMonth As System.Windows.Forms.ComboBox
    Public WithEvents Label24 As System.Windows.Forms.Label
    Public WithEvents Label29 As System.Windows.Forms.Label
    Public WithEvents fraSalMY As System.Windows.Forms.GroupBox
    Public WithEvents sprdDeduct As AxFPSpreadADO.AxfpSpread
    Public WithEvents sprdEarn As AxFPSpreadADO.AxfpSpread
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents grdDeductions As System.Windows.Forms.Label
    Public WithEvents FraMain As System.Windows.Forms.GroupBox
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents CmdDelete As System.Windows.Forms.Button
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents Label44 As System.Windows.Forms.Label
    Public WithEvents optBasicSalary As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGenSalIncrement))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.cboDept = New System.Windows.Forms.ComboBox()
        Me.chkAll = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.cboCategory = New System.Windows.Forms.ComboBox()
        Me.chkCategory = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.FraMain = New System.Windows.Forms.GroupBox()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.txtBasicSalary = New System.Windows.Forms.TextBox()
        Me._optBasicSalary_2 = New System.Windows.Forms.RadioButton()
        Me._optBasicSalary_1 = New System.Windows.Forms.RadioButton()
        Me._optBasicSalary_0 = New System.Windows.Forms.RadioButton()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.cboAppMon = New System.Windows.Forms.ComboBox()
        Me.cboAppYear = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.fraTop = New System.Windows.Forms.GroupBox()
        Me.txtWEF = New System.Windows.Forms.TextBox()
        Me.lblAppDate = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblWEF = New System.Windows.Forms.Label()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.cboArrearMonth = New System.Windows.Forms.ComboBox()
        Me.cboArrearYear = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.fraSalMY = New System.Windows.Forms.GroupBox()
        Me.cboYear = New System.Windows.Forms.ComboBox()
        Me.cboMonth = New System.Windows.Forms.ComboBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.sprdDeduct = New AxFPSpreadADO.AxfpSpread()
        Me.sprdEarn = New AxFPSpreadADO.AxfpSpread()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.grdDeductions = New System.Windows.Forms.Label()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.optBasicSalary = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.Frame4.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me.FraMain.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.fraTop.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.fraSalMY.SuspendLayout()
        CType(Me.sprdDeduct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sprdEarn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optBasicSalary, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(678, 10)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(67, 37)
        Me.CmdClose.TabIndex = 12
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'CmdDelete
        '
        Me.CmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.CmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdDelete.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdDelete.Image = CType(resources.GetObject("CmdDelete.Image"), System.Drawing.Image)
        Me.CmdDelete.Location = New System.Drawing.Point(70, 10)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(67, 37)
        Me.CmdDelete.TabIndex = 11
        Me.CmdDelete.Text = "&Delete"
        Me.CmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdDelete, "Delete Record")
        Me.CmdDelete.UseVisualStyleBackColor = False
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.Location = New System.Drawing.Point(4, 10)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(67, 37)
        Me.CmdSave.TabIndex = 10
        Me.CmdSave.Text = "&Save"
        Me.CmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSave, "Save Record")
        Me.CmdSave.UseVisualStyleBackColor = False
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.cboDept)
        Me.Frame4.Controls.Add(Me.chkAll)
        Me.Frame4.Controls.Add(Me.Label1)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, 0)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(269, 49)
        Me.Frame4.TabIndex = 33
        Me.Frame4.TabStop = False
        Me.Frame4.Text = "Department"
        '
        'cboDept
        '
        Me.cboDept.BackColor = System.Drawing.SystemColors.Window
        Me.cboDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDept.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDept.Location = New System.Drawing.Point(80, 16)
        Me.cboDept.Name = "cboDept"
        Me.cboDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDept.Size = New System.Drawing.Size(139, 22)
        Me.cboDept.TabIndex = 35
        '
        'chkAll
        '
        Me.chkAll.AutoSize = True
        Me.chkAll.BackColor = System.Drawing.SystemColors.Control
        Me.chkAll.Checked = True
        Me.chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAll.Location = New System.Drawing.Point(220, 20)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAll.Size = New System.Drawing.Size(46, 18)
        Me.chkAll.TabIndex = 34
        Me.chkAll.Text = "ALL"
        Me.chkAll.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(5, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(68, 14)
        Me.Label1.TabIndex = 36
        Me.Label1.Text = "Department :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.cboCategory)
        Me.Frame6.Controls.Add(Me.chkCategory)
        Me.Frame6.Controls.Add(Me.Label2)
        Me.Frame6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(270, 0)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(269, 49)
        Me.Frame6.TabIndex = 30
        Me.Frame6.TabStop = False
        Me.Frame6.Text = "Category"
        '
        'cboCategory
        '
        Me.cboCategory.BackColor = System.Drawing.SystemColors.Window
        Me.cboCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCategory.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCategory.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCategory.Location = New System.Drawing.Point(68, 16)
        Me.cboCategory.Name = "cboCategory"
        Me.cboCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCategory.Size = New System.Drawing.Size(151, 22)
        Me.cboCategory.TabIndex = 32
        '
        'chkCategory
        '
        Me.chkCategory.AutoSize = True
        Me.chkCategory.BackColor = System.Drawing.SystemColors.Control
        Me.chkCategory.Checked = True
        Me.chkCategory.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCategory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCategory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCategory.Location = New System.Drawing.Point(220, 20)
        Me.chkCategory.Name = "chkCategory"
        Me.chkCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCategory.Size = New System.Drawing.Size(46, 18)
        Me.chkCategory.TabIndex = 31
        Me.chkCategory.Text = "ALL"
        Me.chkCategory.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(7, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(57, 14)
        Me.Label2.TabIndex = 37
        Me.Label2.Text = "Category :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'FraMain
        '
        Me.FraMain.BackColor = System.Drawing.SystemColors.Control
        Me.FraMain.Controls.Add(Me.Frame1)
        Me.FraMain.Controls.Add(Me.Frame2)
        Me.FraMain.Controls.Add(Me.fraTop)
        Me.FraMain.Controls.Add(Me.Frame3)
        Me.FraMain.Controls.Add(Me.fraSalMY)
        Me.FraMain.Controls.Add(Me.sprdDeduct)
        Me.FraMain.Controls.Add(Me.sprdEarn)
        Me.FraMain.Controls.Add(Me.Label11)
        Me.FraMain.Controls.Add(Me.grdDeductions)
        Me.FraMain.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMain.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMain.Location = New System.Drawing.Point(0, -6)
        Me.FraMain.Name = "FraMain"
        Me.FraMain.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMain.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMain.Size = New System.Drawing.Size(749, 385)
        Me.FraMain.TabIndex = 13
        Me.FraMain.TabStop = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.txtBasicSalary)
        Me.Frame1.Controls.Add(Me._optBasicSalary_2)
        Me.Frame1.Controls.Add(Me._optBasicSalary_1)
        Me.Frame1.Controls.Add(Me._optBasicSalary_0)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 124)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(620, 55)
        Me.Frame1.TabIndex = 38
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Increment on Basic Salary"
        '
        'txtBasicSalary
        '
        Me.txtBasicSalary.AcceptsReturn = True
        Me.txtBasicSalary.BackColor = System.Drawing.SystemColors.Window
        Me.txtBasicSalary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBasicSalary.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBasicSalary.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBasicSalary.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBasicSalary.Location = New System.Drawing.Point(498, 22)
        Me.txtBasicSalary.MaxLength = 0
        Me.txtBasicSalary.Name = "txtBasicSalary"
        Me.txtBasicSalary.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBasicSalary.Size = New System.Drawing.Size(87, 19)
        Me.txtBasicSalary.TabIndex = 42
        '
        '_optBasicSalary_2
        '
        Me._optBasicSalary_2.AutoSize = True
        Me._optBasicSalary_2.BackColor = System.Drawing.SystemColors.Control
        Me._optBasicSalary_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optBasicSalary_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optBasicSalary_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optBasicSalary.SetIndex(Me._optBasicSalary_2, CType(2, Short))
        Me._optBasicSalary_2.Location = New System.Drawing.Point(330, 24)
        Me._optBasicSalary_2.Name = "_optBasicSalary_2"
        Me._optBasicSalary_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optBasicSalary_2.Size = New System.Drawing.Size(99, 18)
        Me._optBasicSalary_2.TabIndex = 41
        Me._optBasicSalary_2.TabStop = True
        Me._optBasicSalary_2.Text = "Increment (Rs.)"
        Me._optBasicSalary_2.UseVisualStyleBackColor = False
        '
        '_optBasicSalary_1
        '
        Me._optBasicSalary_1.AutoSize = True
        Me._optBasicSalary_1.BackColor = System.Drawing.SystemColors.Control
        Me._optBasicSalary_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optBasicSalary_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optBasicSalary_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optBasicSalary.SetIndex(Me._optBasicSalary_1, CType(1, Short))
        Me._optBasicSalary_1.Location = New System.Drawing.Point(188, 24)
        Me._optBasicSalary_1.Name = "_optBasicSalary_1"
        Me._optBasicSalary_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optBasicSalary_1.Size = New System.Drawing.Size(93, 18)
        Me._optBasicSalary_1.TabIndex = 40
        Me._optBasicSalary_1.TabStop = True
        Me._optBasicSalary_1.Text = "Increment (%)"
        Me._optBasicSalary_1.UseVisualStyleBackColor = False
        '
        '_optBasicSalary_0
        '
        Me._optBasicSalary_0.AutoSize = True
        Me._optBasicSalary_0.BackColor = System.Drawing.SystemColors.Control
        Me._optBasicSalary_0.Checked = True
        Me._optBasicSalary_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optBasicSalary_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optBasicSalary_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optBasicSalary.SetIndex(Me._optBasicSalary_0, CType(0, Short))
        Me._optBasicSalary_0.Location = New System.Drawing.Point(22, 24)
        Me._optBasicSalary_0.Name = "_optBasicSalary_0"
        Me._optBasicSalary_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optBasicSalary_0.Size = New System.Drawing.Size(119, 18)
        Me._optBasicSalary_0.TabIndex = 39
        Me._optBasicSalary_0.TabStop = True
        Me._optBasicSalary_0.Text = "No Change In Basic"
        Me._optBasicSalary_0.UseVisualStyleBackColor = False
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.cboAppMon)
        Me.Frame2.Controls.Add(Me.cboAppYear)
        Me.Frame2.Controls.Add(Me.Label5)
        Me.Frame2.Controls.Add(Me.Label4)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(208, 56)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(206, 67)
        Me.Frame2.TabIndex = 25
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "With Applicable From"
        '
        'cboAppMon
        '
        Me.cboAppMon.BackColor = System.Drawing.SystemColors.Window
        Me.cboAppMon.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboAppMon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAppMon.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboAppMon.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboAppMon.Location = New System.Drawing.Point(92, 18)
        Me.cboAppMon.Name = "cboAppMon"
        Me.cboAppMon.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboAppMon.Size = New System.Drawing.Size(111, 22)
        Me.cboAppMon.TabIndex = 4
        '
        'cboAppYear
        '
        Me.cboAppYear.BackColor = System.Drawing.SystemColors.Window
        Me.cboAppYear.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboAppYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAppYear.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboAppYear.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboAppYear.Location = New System.Drawing.Point(92, 42)
        Me.cboAppYear.Name = "cboAppYear"
        Me.cboAppYear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboAppYear.Size = New System.Drawing.Size(61, 22)
        Me.cboAppYear.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(12, 22)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(42, 14)
        Me.Label5.TabIndex = 27
        Me.Label5.Text = "Month :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(12, 46)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(36, 14)
        Me.Label4.TabIndex = 26
        Me.Label4.Text = "Year :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'fraTop
        '
        Me.fraTop.BackColor = System.Drawing.SystemColors.Control
        Me.fraTop.Controls.Add(Me.txtWEF)
        Me.fraTop.Controls.Add(Me.lblAppDate)
        Me.fraTop.Controls.Add(Me.Label3)
        Me.fraTop.Controls.Add(Me.lblWEF)
        Me.fraTop.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTop.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraTop.Location = New System.Drawing.Point(540, 6)
        Me.fraTop.Name = "fraTop"
        Me.fraTop.Padding = New System.Windows.Forms.Padding(0)
        Me.fraTop.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraTop.Size = New System.Drawing.Size(209, 49)
        Me.fraTop.TabIndex = 23
        Me.fraTop.TabStop = False
        '
        'txtWEF
        '
        Me.txtWEF.AcceptsReturn = True
        Me.txtWEF.BackColor = System.Drawing.SystemColors.Window
        Me.txtWEF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWEF.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWEF.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWEF.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtWEF.Location = New System.Drawing.Point(77, 16)
        Me.txtWEF.MaxLength = 0
        Me.txtWEF.Name = "txtWEF"
        Me.txtWEF.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWEF.Size = New System.Drawing.Size(87, 19)
        Me.txtWEF.TabIndex = 0
        '
        'lblAppDate
        '
        Me.lblAppDate.AutoSize = True
        Me.lblAppDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblAppDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAppDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAppDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAppDate.Location = New System.Drawing.Point(50, 26)
        Me.lblAppDate.Name = "lblAppDate"
        Me.lblAppDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAppDate.Size = New System.Drawing.Size(59, 14)
        Me.lblAppDate.TabIndex = 29
        Me.lblAppDate.Text = "lblAppDate"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(6, 20)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(60, 14)
        Me.Label3.TabIndex = 28
        Me.Label3.Text = "WEF Date :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblWEF
        '
        Me.lblWEF.BackColor = System.Drawing.SystemColors.Control
        Me.lblWEF.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblWEF.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWEF.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblWEF.Location = New System.Drawing.Point(124, 30)
        Me.lblWEF.Name = "lblWEF"
        Me.lblWEF.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblWEF.Size = New System.Drawing.Size(61, 15)
        Me.lblWEF.TabIndex = 24
        Me.lblWEF.Text = "lblWEF"
        Me.lblWEF.Visible = False
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.cboArrearMonth)
        Me.Frame3.Controls.Add(Me.cboArrearYear)
        Me.Frame3.Controls.Add(Me.Label8)
        Me.Frame3.Controls.Add(Me.Label6)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(414, 56)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(206, 67)
        Me.Frame3.TabIndex = 14
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "Arrear To Paid with the salary of :"
        '
        'cboArrearMonth
        '
        Me.cboArrearMonth.BackColor = System.Drawing.SystemColors.Window
        Me.cboArrearMonth.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboArrearMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboArrearMonth.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboArrearMonth.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboArrearMonth.Location = New System.Drawing.Point(92, 18)
        Me.cboArrearMonth.Name = "cboArrearMonth"
        Me.cboArrearMonth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboArrearMonth.Size = New System.Drawing.Size(111, 22)
        Me.cboArrearMonth.TabIndex = 6
        '
        'cboArrearYear
        '
        Me.cboArrearYear.BackColor = System.Drawing.SystemColors.Window
        Me.cboArrearYear.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboArrearYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboArrearYear.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboArrearYear.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboArrearYear.Location = New System.Drawing.Point(92, 42)
        Me.cboArrearYear.Name = "cboArrearYear"
        Me.cboArrearYear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboArrearYear.Size = New System.Drawing.Size(61, 22)
        Me.cboArrearYear.TabIndex = 7
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(12, 22)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(42, 14)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "Month :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(12, 46)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(36, 14)
        Me.Label6.TabIndex = 16
        Me.Label6.Text = "Year :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'fraSalMY
        '
        Me.fraSalMY.BackColor = System.Drawing.SystemColors.Control
        Me.fraSalMY.Controls.Add(Me.cboYear)
        Me.fraSalMY.Controls.Add(Me.cboMonth)
        Me.fraSalMY.Controls.Add(Me.Label24)
        Me.fraSalMY.Controls.Add(Me.Label29)
        Me.fraSalMY.Enabled = False
        Me.fraSalMY.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraSalMY.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraSalMY.Location = New System.Drawing.Point(0, 56)
        Me.fraSalMY.Name = "fraSalMY"
        Me.fraSalMY.Padding = New System.Windows.Forms.Padding(0)
        Me.fraSalMY.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraSalMY.Size = New System.Drawing.Size(208, 67)
        Me.fraSalMY.TabIndex = 20
        Me.fraSalMY.TabStop = False
        Me.fraSalMY.Text = "Increment Due From"
        '
        'cboYear
        '
        Me.cboYear.BackColor = System.Drawing.SystemColors.Window
        Me.cboYear.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboYear.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboYear.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboYear.Location = New System.Drawing.Point(92, 42)
        Me.cboYear.Name = "cboYear"
        Me.cboYear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboYear.Size = New System.Drawing.Size(61, 22)
        Me.cboYear.TabIndex = 3
        '
        'cboMonth
        '
        Me.cboMonth.BackColor = System.Drawing.SystemColors.Window
        Me.cboMonth.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMonth.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboMonth.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboMonth.Location = New System.Drawing.Point(92, 18)
        Me.cboMonth.Name = "cboMonth"
        Me.cboMonth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboMonth.Size = New System.Drawing.Size(111, 22)
        Me.cboMonth.TabIndex = 2
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.SystemColors.Control
        Me.Label24.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label24.Location = New System.Drawing.Point(12, 46)
        Me.Label24.Name = "Label24"
        Me.Label24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label24.Size = New System.Drawing.Size(42, 14)
        Me.Label24.TabIndex = 22
        Me.Label24.Text = "Year   :"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.SystemColors.Control
        Me.Label29.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label29.Location = New System.Drawing.Point(12, 22)
        Me.Label29.Name = "Label29"
        Me.Label29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label29.Size = New System.Drawing.Size(42, 14)
        Me.Label29.TabIndex = 21
        Me.Label29.Text = "Month :"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'sprdDeduct
        '
        Me.sprdDeduct.DataSource = Nothing
        Me.sprdDeduct.Location = New System.Drawing.Point(374, 203)
        Me.sprdDeduct.Name = "sprdDeduct"
        Me.sprdDeduct.OcxState = CType(resources.GetObject("sprdDeduct.OcxState"), System.Windows.Forms.AxHost.State)
        Me.sprdDeduct.Size = New System.Drawing.Size(371, 179)
        Me.sprdDeduct.TabIndex = 9
        '
        'sprdEarn
        '
        Me.sprdEarn.DataSource = Nothing
        Me.sprdEarn.Location = New System.Drawing.Point(2, 203)
        Me.sprdEarn.Name = "sprdEarn"
        Me.sprdEarn.OcxState = CType(resources.GetObject("sprdEarn.OcxState"), System.Windows.Forms.AxHost.State)
        Me.sprdEarn.Size = New System.Drawing.Size(371, 179)
        Me.sprdEarn.TabIndex = 8
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(2, 182)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(371, 19)
        Me.Label11.TabIndex = 17
        Me.Label11.Text = "Earnings"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'grdDeductions
        '
        Me.grdDeductions.BackColor = System.Drawing.SystemColors.Control
        Me.grdDeductions.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdDeductions.Cursor = System.Windows.Forms.Cursors.Default
        Me.grdDeductions.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdDeductions.ForeColor = System.Drawing.SystemColors.ControlText
        Me.grdDeductions.Location = New System.Drawing.Point(374, 182)
        Me.grdDeductions.Name = "grdDeductions"
        Me.grdDeductions.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.grdDeductions.Size = New System.Drawing.Size(371, 19)
        Me.grdDeductions.TabIndex = 18
        Me.grdDeductions.Text = "Deductions"
        Me.grdDeductions.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.CmdDelete)
        Me.FraMovement.Controls.Add(Me.CmdSave)
        Me.FraMovement.Controls.Add(Me.Report1)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 374)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(749, 51)
        Me.FraMovement.TabIndex = 19
        Me.FraMovement.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(14, 14)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 13
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.BackColor = System.Drawing.SystemColors.Menu
        Me.Label44.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label44.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label44.Location = New System.Drawing.Point(222, 42)
        Me.Label44.Name = "Label44"
        Me.Label44.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label44.Size = New System.Drawing.Size(32, 14)
        Me.Label44.TabIndex = 1
        Me.Label44.Text = "Sex :"
        '
        'optBasicSalary
        '
        '
        'frmGenSalIncrement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(749, 425)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.Frame6)
        Me.Controls.Add(Me.FraMain)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.Label44)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 23)
        Me.MaximizeBox = False
        Me.Name = "frmGenSalIncrement"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "General Salary Increment"
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me.FraMain.ResumeLayout(False)
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.fraTop.ResumeLayout(False)
        Me.fraTop.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.fraSalMY.ResumeLayout(False)
        Me.fraSalMY.PerformLayout()
        CType(Me.sprdDeduct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sprdEarn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optBasicSalary, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region
End Class