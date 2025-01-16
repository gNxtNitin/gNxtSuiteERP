Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmEmpServiceReg
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
    Public WithEvents cboShow As System.Windows.Forms.ComboBox
    Public WithEvents Frame11 As System.Windows.Forms.GroupBox
    Public WithEvents optCheckDOJ As System.Windows.Forms.RadioButton
    Public WithEvents optCheckGJ As System.Windows.Forms.RadioButton
    Public WithEvents Frame10 As System.Windows.Forms.GroupBox
    Public WithEvents chkDOJ As System.Windows.Forms.CheckBox
    Public WithEvents cboCond As System.Windows.Forms.ComboBox
    Public WithEvents txtPeriod As System.Windows.Forms.TextBox
    Public WithEvents txtAsOn As System.Windows.Forms.MaskedTextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents CboDept As System.Windows.Forms.ComboBox
    Public WithEvents chkAll As System.Windows.Forms.CheckBox
    Public WithEvents Frame7 As System.Windows.Forms.GroupBox
    Public WithEvents optDOJ As System.Windows.Forms.RadioButton
    Public WithEvents optCardNo As System.Windows.Forms.RadioButton
    Public WithEvents OptName As System.Windows.Forms.RadioButton
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents cboCategory As System.Windows.Forms.ComboBox
    Public WithEvents chkCategory As System.Windows.Forms.CheckBox
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents optExisting As System.Windows.Forms.RadioButton
    Public WithEvents optAllEmp As System.Windows.Forms.RadioButton
    Public WithEvents Frame5 As System.Windows.Forms.GroupBox
    Public WithEvents chkDiv As System.Windows.Forms.CheckBox
    Public WithEvents cboDivision As System.Windows.Forms.ComboBox
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents _OptGroup_0 As System.Windows.Forms.RadioButton
    Public WithEvents _OptGroup_2 As System.Windows.Forms.RadioButton
    Public WithEvents _OptGroup_1 As System.Windows.Forms.RadioButton
    Public WithEvents Frame9 As System.Windows.Forms.GroupBox
    Public WithEvents _OptPrint_0 As System.Windows.Forms.RadioButton
    Public WithEvents _OptPrint_1 As System.Windows.Forms.RadioButton
    Public WithEvents Frame8 As System.Windows.Forms.GroupBox
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdRefresh As System.Windows.Forms.Button
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents OptGroup As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents OptPrint As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEmpServiceReg))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.Frame11 = New System.Windows.Forms.GroupBox()
        Me.cboShow = New System.Windows.Forms.ComboBox()
        Me.Frame10 = New System.Windows.Forms.GroupBox()
        Me.optCheckDOJ = New System.Windows.Forms.RadioButton()
        Me.optCheckGJ = New System.Windows.Forms.RadioButton()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.chkDOJ = New System.Windows.Forms.CheckBox()
        Me.cboCond = New System.Windows.Forms.ComboBox()
        Me.txtPeriod = New System.Windows.Forms.TextBox()
        Me.txtAsOn = New System.Windows.Forms.MaskedTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Frame7 = New System.Windows.Forms.GroupBox()
        Me.CboDept = New System.Windows.Forms.ComboBox()
        Me.chkAll = New System.Windows.Forms.CheckBox()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.optDOJ = New System.Windows.Forms.RadioButton()
        Me.optCardNo = New System.Windows.Forms.RadioButton()
        Me.OptName = New System.Windows.Forms.RadioButton()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.cboCategory = New System.Windows.Forms.ComboBox()
        Me.chkCategory = New System.Windows.Forms.CheckBox()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me.optExisting = New System.Windows.Forms.RadioButton()
        Me.optAllEmp = New System.Windows.Forms.RadioButton()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.chkDiv = New System.Windows.Forms.CheckBox()
        Me.cboDivision = New System.Windows.Forms.ComboBox()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.Frame9 = New System.Windows.Forms.GroupBox()
        Me._OptGroup_0 = New System.Windows.Forms.RadioButton()
        Me._OptGroup_2 = New System.Windows.Forms.RadioButton()
        Me._OptGroup_1 = New System.Windows.Forms.RadioButton()
        Me.Frame8 = New System.Windows.Forms.GroupBox()
        Me._OptPrint_0 = New System.Windows.Forms.RadioButton()
        Me._OptPrint_1 = New System.Windows.Forms.RadioButton()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdRefresh = New System.Windows.Forms.Button()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.OptGroup = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.OptPrint = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.Frame11.SuspendLayout()
        Me.Frame10.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame7.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me.Frame5.SuspendLayout()
        Me.Frame4.SuspendLayout()
        Me.Frame1.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        Me.Frame9.SuspendLayout()
        Me.Frame8.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Location = New System.Drawing.Point(84, 12)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(79, 34)
        Me.CmdPreview.TabIndex = 18
        Me.CmdPreview.Text = "Pre&view"
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Print PO")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Location = New System.Drawing.Point(664, 12)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(80, 34)
        Me.CmdClose.TabIndex = 20
        Me.CmdClose.Text = "&Close"
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'Frame11
        '
        Me.Frame11.BackColor = System.Drawing.SystemColors.Control
        Me.Frame11.Controls.Add(Me.cboShow)
        Me.Frame11.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame11.Location = New System.Drawing.Point(618, 0)
        Me.Frame11.Name = "Frame11"
        Me.Frame11.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame11.Size = New System.Drawing.Size(131, 45)
        Me.Frame11.TabIndex = 39
        Me.Frame11.TabStop = False
        Me.Frame11.Text = "Corporate"
        '
        'cboShow
        '
        Me.cboShow.BackColor = System.Drawing.SystemColors.Window
        Me.cboShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboShow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboShow.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboShow.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboShow.Location = New System.Drawing.Point(4, 16)
        Me.cboShow.Name = "cboShow"
        Me.cboShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboShow.Size = New System.Drawing.Size(123, 22)
        Me.cboShow.TabIndex = 40
        '
        'Frame10
        '
        Me.Frame10.BackColor = System.Drawing.SystemColors.Control
        Me.Frame10.Controls.Add(Me.optCheckDOJ)
        Me.Frame10.Controls.Add(Me.optCheckGJ)
        Me.Frame10.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame10.Location = New System.Drawing.Point(74, 42)
        Me.Frame10.Name = "Frame10"
        Me.Frame10.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame10.Size = New System.Drawing.Size(157, 41)
        Me.Frame10.TabIndex = 36
        Me.Frame10.TabStop = False
        Me.Frame10.Text = "Base"
        '
        'optCheckDOJ
        '
        Me.optCheckDOJ.AutoSize = True
        Me.optCheckDOJ.BackColor = System.Drawing.SystemColors.Control
        Me.optCheckDOJ.Checked = True
        Me.optCheckDOJ.Cursor = System.Windows.Forms.Cursors.Default
        Me.optCheckDOJ.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCheckDOJ.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCheckDOJ.Location = New System.Drawing.Point(4, 18)
        Me.optCheckDOJ.Name = "optCheckDOJ"
        Me.optCheckDOJ.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optCheckDOJ.Size = New System.Drawing.Size(45, 18)
        Me.optCheckDOJ.TabIndex = 38
        Me.optCheckDOJ.TabStop = True
        Me.optCheckDOJ.Text = "DOJ"
        Me.optCheckDOJ.UseVisualStyleBackColor = False
        '
        'optCheckGJ
        '
        Me.optCheckGJ.AutoSize = True
        Me.optCheckGJ.BackColor = System.Drawing.SystemColors.Control
        Me.optCheckGJ.Cursor = System.Windows.Forms.Cursors.Default
        Me.optCheckGJ.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCheckGJ.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCheckGJ.Location = New System.Drawing.Point(54, 18)
        Me.optCheckGJ.Name = "optCheckGJ"
        Me.optCheckGJ.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optCheckGJ.Size = New System.Drawing.Size(91, 18)
        Me.optCheckGJ.TabIndex = 37
        Me.optCheckGJ.TabStop = True
        Me.optCheckGJ.Text = "Group Joining"
        Me.optCheckGJ.UseVisualStyleBackColor = False
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.chkDOJ)
        Me.Frame2.Controls.Add(Me.cboCond)
        Me.Frame2.Controls.Add(Me.txtPeriod)
        Me.Frame2.Controls.Add(Me.txtAsOn)
        Me.Frame2.Controls.Add(Me.Label1)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(416, 42)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(480, 43)
        Me.Frame2.TabIndex = 26
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Conditional Query (For Joining Month)"
        '
        'chkDOJ
        '
        Me.chkDOJ.AutoSize = True
        Me.chkDOJ.BackColor = System.Drawing.SystemColors.Control
        Me.chkDOJ.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkDOJ.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDOJ.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkDOJ.Location = New System.Drawing.Point(10, 20)
        Me.chkDOJ.Name = "chkDOJ"
        Me.chkDOJ.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkDOJ.Size = New System.Drawing.Size(46, 18)
        Me.chkDOJ.TabIndex = 12
        Me.chkDOJ.Text = "ALL"
        Me.chkDOJ.UseVisualStyleBackColor = False
        '
        'cboCond
        '
        Me.cboCond.BackColor = System.Drawing.SystemColors.Window
        Me.cboCond.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCond.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCond.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCond.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCond.Location = New System.Drawing.Point(199, 16)
        Me.cboCond.Name = "cboCond"
        Me.cboCond.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCond.Size = New System.Drawing.Size(51, 22)
        Me.cboCond.TabIndex = 14
        '
        'txtPeriod
        '
        Me.txtPeriod.AcceptsReturn = True
        Me.txtPeriod.BackColor = System.Drawing.SystemColors.Window
        Me.txtPeriod.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPeriod.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPeriod.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPeriod.Location = New System.Drawing.Point(255, 16)
        Me.txtPeriod.MaxLength = 0
        Me.txtPeriod.Name = "txtPeriod"
        Me.txtPeriod.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPeriod.Size = New System.Drawing.Size(61, 20)
        Me.txtPeriod.TabIndex = 15
        '
        'txtAsOn
        '
        Me.txtAsOn.AllowPromptAsInput = False
        Me.txtAsOn.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAsOn.Location = New System.Drawing.Point(111, 16)
        Me.txtAsOn.Mask = "##/##/####"
        Me.txtAsOn.Name = "txtAsOn"
        Me.txtAsOn.Size = New System.Drawing.Size(80, 20)
        Me.txtAsOn.TabIndex = 13
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(60, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(44, 14)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = "As On :"
        '
        'Frame7
        '
        Me.Frame7.BackColor = System.Drawing.SystemColors.Control
        Me.Frame7.Controls.Add(Me.CboDept)
        Me.Frame7.Controls.Add(Me.chkAll)
        Me.Frame7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame7.Location = New System.Drawing.Point(416, 0)
        Me.Frame7.Name = "Frame7"
        Me.Frame7.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame7.Size = New System.Drawing.Size(201, 41)
        Me.Frame7.TabIndex = 25
        Me.Frame7.TabStop = False
        Me.Frame7.Text = "Department"
        '
        'CboDept
        '
        Me.CboDept.BackColor = System.Drawing.SystemColors.Window
        Me.CboDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.CboDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboDept.ForeColor = System.Drawing.SystemColors.WindowText
        Me.CboDept.Location = New System.Drawing.Point(6, 14)
        Me.CboDept.Name = "CboDept"
        Me.CboDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CboDept.Size = New System.Drawing.Size(145, 22)
        Me.CboDept.TabIndex = 10
        '
        'chkAll
        '
        Me.chkAll.AutoSize = True
        Me.chkAll.BackColor = System.Drawing.SystemColors.Control
        Me.chkAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAll.Location = New System.Drawing.Point(152, 18)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAll.Size = New System.Drawing.Size(46, 18)
        Me.chkAll.TabIndex = 11
        Me.chkAll.Text = "ALL"
        Me.chkAll.UseVisualStyleBackColor = False
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.optDOJ)
        Me.Frame3.Controls.Add(Me.optCardNo)
        Me.Frame3.Controls.Add(Me.OptName)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 0)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(73, 83)
        Me.Frame3.TabIndex = 21
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "Order By"
        '
        'optDOJ
        '
        Me.optDOJ.AutoSize = True
        Me.optDOJ.BackColor = System.Drawing.SystemColors.Control
        Me.optDOJ.Cursor = System.Windows.Forms.Cursors.Default
        Me.optDOJ.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optDOJ.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDOJ.Location = New System.Drawing.Point(2, 56)
        Me.optDOJ.Name = "optDOJ"
        Me.optDOJ.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optDOJ.Size = New System.Drawing.Size(45, 18)
        Me.optDOJ.TabIndex = 3
        Me.optDOJ.TabStop = True
        Me.optDOJ.Text = "DOJ"
        Me.optDOJ.UseVisualStyleBackColor = False
        '
        'optCardNo
        '
        Me.optCardNo.AutoSize = True
        Me.optCardNo.BackColor = System.Drawing.SystemColors.Control
        Me.optCardNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.optCardNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCardNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCardNo.Location = New System.Drawing.Point(2, 36)
        Me.optCardNo.Name = "optCardNo"
        Me.optCardNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optCardNo.Size = New System.Drawing.Size(64, 18)
        Me.optCardNo.TabIndex = 2
        Me.optCardNo.TabStop = True
        Me.optCardNo.Text = "Card No"
        Me.optCardNo.UseVisualStyleBackColor = False
        '
        'OptName
        '
        Me.OptName.AutoSize = True
        Me.OptName.BackColor = System.Drawing.SystemColors.Control
        Me.OptName.Checked = True
        Me.OptName.Cursor = System.Windows.Forms.Cursors.Default
        Me.OptName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptName.Location = New System.Drawing.Point(2, 18)
        Me.OptName.Name = "OptName"
        Me.OptName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptName.Size = New System.Drawing.Size(52, 18)
        Me.OptName.TabIndex = 1
        Me.OptName.TabStop = True
        Me.OptName.Text = "Name"
        Me.OptName.UseVisualStyleBackColor = False
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.cboCategory)
        Me.Frame6.Controls.Add(Me.chkCategory)
        Me.Frame6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(232, 42)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(183, 41)
        Me.Frame6.TabIndex = 23
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
        Me.cboCategory.Location = New System.Drawing.Point(4, 14)
        Me.cboCategory.Name = "cboCategory"
        Me.cboCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCategory.Size = New System.Drawing.Size(129, 22)
        Me.cboCategory.TabIndex = 28
        '
        'chkCategory
        '
        Me.chkCategory.AutoSize = True
        Me.chkCategory.BackColor = System.Drawing.SystemColors.Control
        Me.chkCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCategory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCategory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCategory.Location = New System.Drawing.Point(134, 18)
        Me.chkCategory.Name = "chkCategory"
        Me.chkCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCategory.Size = New System.Drawing.Size(46, 18)
        Me.chkCategory.TabIndex = 9
        Me.chkCategory.Text = "ALL"
        Me.chkCategory.UseVisualStyleBackColor = False
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me.optExisting)
        Me.Frame5.Controls.Add(Me.optAllEmp)
        Me.Frame5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(74, 0)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(157, 41)
        Me.Frame5.TabIndex = 24
        Me.Frame5.TabStop = False
        Me.Frame5.Text = "Type"
        '
        'optExisting
        '
        Me.optExisting.AutoSize = True
        Me.optExisting.BackColor = System.Drawing.SystemColors.Control
        Me.optExisting.Checked = True
        Me.optExisting.Cursor = System.Windows.Forms.Cursors.Default
        Me.optExisting.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optExisting.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optExisting.Location = New System.Drawing.Point(64, 18)
        Me.optExisting.Name = "optExisting"
        Me.optExisting.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optExisting.Size = New System.Drawing.Size(62, 18)
        Me.optExisting.TabIndex = 5
        Me.optExisting.TabStop = True
        Me.optExisting.Text = "Existing"
        Me.optExisting.UseVisualStyleBackColor = False
        '
        'optAllEmp
        '
        Me.optAllEmp.AutoSize = True
        Me.optAllEmp.BackColor = System.Drawing.SystemColors.Control
        Me.optAllEmp.Cursor = System.Windows.Forms.Cursors.Default
        Me.optAllEmp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optAllEmp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optAllEmp.Location = New System.Drawing.Point(4, 18)
        Me.optAllEmp.Name = "optAllEmp"
        Me.optAllEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optAllEmp.Size = New System.Drawing.Size(45, 18)
        Me.optAllEmp.TabIndex = 4
        Me.optAllEmp.TabStop = True
        Me.optAllEmp.Text = "ALL"
        Me.optAllEmp.UseVisualStyleBackColor = False
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.chkDiv)
        Me.Frame4.Controls.Add(Me.cboDivision)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(232, 0)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(183, 41)
        Me.Frame4.TabIndex = 22
        Me.Frame4.TabStop = False
        Me.Frame4.Text = "Division"
        '
        'chkDiv
        '
        Me.chkDiv.AutoSize = True
        Me.chkDiv.BackColor = System.Drawing.SystemColors.Control
        Me.chkDiv.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkDiv.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDiv.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkDiv.Location = New System.Drawing.Point(134, 18)
        Me.chkDiv.Name = "chkDiv"
        Me.chkDiv.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkDiv.Size = New System.Drawing.Size(46, 18)
        Me.chkDiv.TabIndex = 7
        Me.chkDiv.Text = "ALL"
        Me.chkDiv.UseVisualStyleBackColor = False
        '
        'cboDivision
        '
        Me.cboDivision.BackColor = System.Drawing.SystemColors.Window
        Me.cboDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivision.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDivision.Location = New System.Drawing.Point(2, 14)
        Me.cboDivision.Name = "cboDivision"
        Me.cboDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivision.Size = New System.Drawing.Size(131, 22)
        Me.cboDivision.TabIndex = 6
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.SprdView)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 78)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(899, 487)
        Me.Frame1.TabIndex = 0
        Me.Frame1.TabStop = False
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SprdView.Location = New System.Drawing.Point(0, 13)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(899, 474)
        Me.SprdView.TabIndex = 16
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.Frame9)
        Me.FraMovement.Controls.Add(Me.Frame8)
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.cmdRefresh)
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.Report1)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 558)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(899, 51)
        Me.FraMovement.TabIndex = 8
        Me.FraMovement.TabStop = False
        '
        'Frame9
        '
        Me.Frame9.BackColor = System.Drawing.SystemColors.Control
        Me.Frame9.Controls.Add(Me._OptGroup_0)
        Me.Frame9.Controls.Add(Me._OptGroup_2)
        Me.Frame9.Controls.Add(Me._OptGroup_1)
        Me.Frame9.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame9.Location = New System.Drawing.Point(168, 8)
        Me.Frame9.Name = "Frame9"
        Me.Frame9.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame9.Size = New System.Drawing.Size(251, 39)
        Me.Frame9.TabIndex = 32
        Me.Frame9.TabStop = False
        Me.Frame9.Text = "Group By"
        '
        '_OptGroup_0
        '
        Me._OptGroup_0.AutoSize = True
        Me._OptGroup_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptGroup_0.Checked = True
        Me._OptGroup_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptGroup_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptGroup_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptGroup.SetIndex(Me._OptGroup_0, CType(0, Short))
        Me._OptGroup_0.Location = New System.Drawing.Point(10, 18)
        Me._OptGroup_0.Name = "_OptGroup_0"
        Me._OptGroup_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptGroup_0.Size = New System.Drawing.Size(50, 18)
        Me._OptGroup_0.TabIndex = 35
        Me._OptGroup_0.TabStop = True
        Me._OptGroup_0.Text = "None"
        Me._OptGroup_0.UseVisualStyleBackColor = False
        '
        '_OptGroup_2
        '
        Me._OptGroup_2.AutoSize = True
        Me._OptGroup_2.BackColor = System.Drawing.SystemColors.Control
        Me._OptGroup_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptGroup_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptGroup_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptGroup.SetIndex(Me._OptGroup_2, CType(2, Short))
        Me._OptGroup_2.Location = New System.Drawing.Point(146, 18)
        Me._OptGroup_2.Name = "_OptGroup_2"
        Me._OptGroup_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptGroup_2.Size = New System.Drawing.Size(82, 18)
        Me._OptGroup_2.TabIndex = 34
        Me._OptGroup_2.TabStop = True
        Me._OptGroup_2.Text = "Cost Center"
        Me._OptGroup_2.UseVisualStyleBackColor = False
        '
        '_OptGroup_1
        '
        Me._OptGroup_1.AutoSize = True
        Me._OptGroup_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptGroup_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptGroup_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptGroup_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptGroup.SetIndex(Me._OptGroup_1, CType(1, Short))
        Me._OptGroup_1.Location = New System.Drawing.Point(82, 18)
        Me._OptGroup_1.Name = "_OptGroup_1"
        Me._OptGroup_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptGroup_1.Size = New System.Drawing.Size(50, 18)
        Me._OptGroup_1.TabIndex = 33
        Me._OptGroup_1.TabStop = True
        Me._OptGroup_1.Text = "Dept."
        Me._OptGroup_1.UseVisualStyleBackColor = False
        '
        'Frame8
        '
        Me.Frame8.BackColor = System.Drawing.SystemColors.Control
        Me.Frame8.Controls.Add(Me._OptPrint_0)
        Me.Frame8.Controls.Add(Me._OptPrint_1)
        Me.Frame8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame8.Location = New System.Drawing.Point(420, 8)
        Me.Frame8.Name = "Frame8"
        Me.Frame8.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame8.Size = New System.Drawing.Size(161, 39)
        Me.Frame8.TabIndex = 29
        Me.Frame8.TabStop = False
        Me.Frame8.Text = "Report Print"
        '
        '_OptPrint_0
        '
        Me._OptPrint_0.AutoSize = True
        Me._OptPrint_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptPrint_0.Checked = True
        Me._OptPrint_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptPrint_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptPrint_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptPrint.SetIndex(Me._OptPrint_0, CType(0, Short))
        Me._OptPrint_0.Location = New System.Drawing.Point(6, 18)
        Me._OptPrint_0.Name = "_OptPrint_0"
        Me._OptPrint_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptPrint_0.Size = New System.Drawing.Size(62, 18)
        Me._OptPrint_0.TabIndex = 31
        Me._OptPrint_0.TabStop = True
        Me._OptPrint_0.Text = "Service"
        Me._OptPrint_0.UseVisualStyleBackColor = False
        '
        '_OptPrint_1
        '
        Me._OptPrint_1.AutoSize = True
        Me._OptPrint_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptPrint_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptPrint_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptPrint_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptPrint.SetIndex(Me._OptPrint_1, CType(1, Short))
        Me._OptPrint_1.Location = New System.Drawing.Point(78, 18)
        Me._OptPrint_1.Name = "_OptPrint_1"
        Me._OptPrint_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptPrint_1.Size = New System.Drawing.Size(73, 18)
        Me._OptPrint_1.TabIndex = 30
        Me._OptPrint_1.TabStop = True
        Me._OptPrint_1.Text = "Expenses"
        Me._OptPrint_1.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Location = New System.Drawing.Point(4, 12)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(80, 34)
        Me.cmdPrint.TabIndex = 17
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'cmdRefresh
        '
        Me.cmdRefresh.BackColor = System.Drawing.SystemColors.Control
        Me.cmdRefresh.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdRefresh.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdRefresh.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdRefresh.Location = New System.Drawing.Point(584, 12)
        Me.cmdRefresh.Name = "cmdRefresh"
        Me.cmdRefresh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdRefresh.Size = New System.Drawing.Size(80, 34)
        Me.cmdRefresh.TabIndex = 19
        Me.cmdRefresh.Text = "&Refresh"
        Me.cmdRefresh.UseVisualStyleBackColor = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(130, 14)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 33
        '
        'frmEmpServiceReg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(900, 611)
        Me.Controls.Add(Me.Frame11)
        Me.Controls.Add(Me.Frame10)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.Frame7)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.Frame6)
        Me.Controls.Add(Me.Frame5)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.FraMovement)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "frmEmpServiceReg"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Employee Service Period Register"
        Me.Frame11.ResumeLayout(False)
        Me.Frame10.ResumeLayout(False)
        Me.Frame10.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.Frame7.ResumeLayout(False)
        Me.Frame7.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me.Frame5.ResumeLayout(False)
        Me.Frame5.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        Me.Frame9.ResumeLayout(False)
        Me.Frame9.PerformLayout()
        Me.Frame8.ResumeLayout(False)
        Me.Frame8.PerformLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptPrint, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        ''SprdView.DataSource = CType(AData1, MSDATASRC.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        SprdView.DataSource = Nothing
    End Sub
#End Region
End Class