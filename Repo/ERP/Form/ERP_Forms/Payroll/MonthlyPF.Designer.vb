Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmMonthlyPF
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
    Public WithEvents _chkShow_5 As System.Windows.Forms.CheckBox
    Public WithEvents _chkShow_4 As System.Windows.Forms.CheckBox
    Public WithEvents _chkShow_3 As System.Windows.Forms.CheckBox
    Public WithEvents _chkShow_2 As System.Windows.Forms.CheckBox
    Public WithEvents _chkShow_1 As System.Windows.Forms.CheckBox
    Public WithEvents _chkShow_0 As System.Windows.Forms.CheckBox
    Public WithEvents Frame7 As System.Windows.Forms.GroupBox
    Public WithEvents cboCategory As System.Windows.Forms.ComboBox
    Public WithEvents chkCategory As System.Windows.Forms.CheckBox
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents chkAll As System.Windows.Forms.CheckBox
    Public WithEvents cboCeiling As System.Windows.Forms.ComboBox
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents OptPF As System.Windows.Forms.RadioButton
    Public WithEvents optCardNo As System.Windows.Forms.RadioButton
    Public WithEvents OptName As System.Windows.Forms.RadioButton
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents lblRunDate As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents _OptFields_1 As System.Windows.Forms.RadioButton
    Public WithEvents _OptFields_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame5 As System.Windows.Forms.GroupBox
    Public WithEvents cmdCTextFile As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdRefresh As System.Windows.Forms.Button
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents OptFields As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents chkShow As Microsoft.VisualBasic.Compatibility.VB6.CheckBoxArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMonthlyPF))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdCTextFile = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.Frame7 = New System.Windows.Forms.GroupBox()
        Me._chkShow_5 = New System.Windows.Forms.CheckBox()
        Me._chkShow_4 = New System.Windows.Forms.CheckBox()
        Me._chkShow_3 = New System.Windows.Forms.CheckBox()
        Me._chkShow_2 = New System.Windows.Forms.CheckBox()
        Me._chkShow_1 = New System.Windows.Forms.CheckBox()
        Me._chkShow_0 = New System.Windows.Forms.CheckBox()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.cboCategory = New System.Windows.Forms.ComboBox()
        Me.chkCategory = New System.Windows.Forms.CheckBox()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.chkAll = New System.Windows.Forms.CheckBox()
        Me.cboCeiling = New System.Windows.Forms.ComboBox()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.OptPF = New System.Windows.Forms.RadioButton()
        Me.optCardNo = New System.Windows.Forms.RadioButton()
        Me.OptName = New System.Windows.Forms.RadioButton()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.lblRunDate = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me._OptFields_1 = New System.Windows.Forms.RadioButton()
        Me._OptFields_0 = New System.Windows.Forms.RadioButton()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdRefresh = New System.Windows.Forms.Button()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.OptFields = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.chkShow = New Microsoft.VisualBasic.Compatibility.VB6.CheckBoxArray(Me.components)
        Me.lblYear = New System.Windows.Forms.DateTimePicker()
        Me.Frame7.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me.Frame4.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame1.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        Me.Frame5.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptFields, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkShow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdCTextFile
        '
        Me.cmdCTextFile.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCTextFile.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCTextFile.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCTextFile.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCTextFile.Image = CType(resources.GetObject("cmdCTextFile.Image"), System.Drawing.Image)
        Me.cmdCTextFile.Location = New System.Drawing.Point(162, 12)
        Me.cmdCTextFile.Name = "cmdCTextFile"
        Me.cmdCTextFile.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCTextFile.Size = New System.Drawing.Size(67, 33)
        Me.cmdCTextFile.TabIndex = 28
        Me.cmdCTextFile.Text = "Create Text File"
        Me.cmdCTextFile.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdCTextFile, "Show Record")
        Me.cmdCTextFile.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Location = New System.Drawing.Point(84, 12)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(79, 34)
        Me.CmdPreview.TabIndex = 15
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
        Me.CmdClose.TabIndex = 5
        Me.CmdClose.Text = "&Close"
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'Frame7
        '
        Me.Frame7.BackColor = System.Drawing.SystemColors.Control
        Me.Frame7.Controls.Add(Me._chkShow_5)
        Me.Frame7.Controls.Add(Me._chkShow_4)
        Me.Frame7.Controls.Add(Me._chkShow_3)
        Me.Frame7.Controls.Add(Me._chkShow_2)
        Me.Frame7.Controls.Add(Me._chkShow_1)
        Me.Frame7.Controls.Add(Me._chkShow_0)
        Me.Frame7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame7.Location = New System.Drawing.Point(356, 0)
        Me.Frame7.Name = "Frame7"
        Me.Frame7.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame7.Size = New System.Drawing.Size(231, 61)
        Me.Frame7.TabIndex = 21
        Me.Frame7.TabStop = False
        Me.Frame7.Text = "Show"
        '
        '_chkShow_5
        '
        Me._chkShow_5.AutoSize = True
        Me._chkShow_5.BackColor = System.Drawing.SystemColors.Control
        Me._chkShow_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkShow_5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkShow_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkShow.SetIndex(Me._chkShow_5, CType(5, Short))
        Me._chkShow_5.Location = New System.Drawing.Point(158, 38)
        Me._chkShow_5.Name = "_chkShow_5"
        Me._chkShow_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkShow_5.Size = New System.Drawing.Size(46, 18)
        Me._chkShow_5.TabIndex = 27
        Me._chkShow_5.Text = "ELA"
        Me._chkShow_5.UseVisualStyleBackColor = False
        '
        '_chkShow_4
        '
        Me._chkShow_4.AutoSize = True
        Me._chkShow_4.BackColor = System.Drawing.SystemColors.Control
        Me._chkShow_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkShow_4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkShow_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkShow.SetIndex(Me._chkShow_4, CType(4, Short))
        Me._chkShow_4.Location = New System.Drawing.Point(158, 22)
        Me._chkShow_4.Name = "_chkShow_4"
        Me._chkShow_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkShow_4.Size = New System.Drawing.Size(40, 18)
        Me._chkShow_4.TabIndex = 26
        Me._chkShow_4.Text = "VP"
        Me._chkShow_4.UseVisualStyleBackColor = False
        '
        '_chkShow_3
        '
        Me._chkShow_3.AutoSize = True
        Me._chkShow_3.BackColor = System.Drawing.SystemColors.Control
        Me._chkShow_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkShow_3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkShow_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkShow.SetIndex(Me._chkShow_3, CType(3, Short))
        Me._chkShow_3.Location = New System.Drawing.Point(84, 38)
        Me._chkShow_3.Name = "_chkShow_3"
        Me._chkShow_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkShow_3.Size = New System.Drawing.Size(45, 18)
        Me._chkShow_3.TabIndex = 25
        Me._chkShow_3.Text = "F&&F"
        Me._chkShow_3.UseVisualStyleBackColor = False
        '
        '_chkShow_2
        '
        Me._chkShow_2.AutoSize = True
        Me._chkShow_2.BackColor = System.Drawing.SystemColors.Control
        Me._chkShow_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkShow_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkShow_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkShow.SetIndex(Me._chkShow_2, CType(2, Short))
        Me._chkShow_2.Location = New System.Drawing.Point(8, 38)
        Me._chkShow_2.Name = "_chkShow_2"
        Me._chkShow_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkShow_2.Size = New System.Drawing.Size(44, 18)
        Me._chkShow_2.TabIndex = 24
        Me._chkShow_2.Text = "L.E."
        Me._chkShow_2.UseVisualStyleBackColor = False
        '
        '_chkShow_1
        '
        Me._chkShow_1.AutoSize = True
        Me._chkShow_1.BackColor = System.Drawing.SystemColors.Control
        Me._chkShow_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkShow_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkShow_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkShow.SetIndex(Me._chkShow_1, CType(1, Short))
        Me._chkShow_1.Location = New System.Drawing.Point(84, 22)
        Me._chkShow_1.Name = "_chkShow_1"
        Me._chkShow_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkShow_1.Size = New System.Drawing.Size(58, 18)
        Me._chkShow_1.TabIndex = 23
        Me._chkShow_1.Text = "Arrear"
        Me._chkShow_1.UseVisualStyleBackColor = False
        '
        '_chkShow_0
        '
        Me._chkShow_0.AutoSize = True
        Me._chkShow_0.BackColor = System.Drawing.SystemColors.Control
        Me._chkShow_0.Checked = True
        Me._chkShow_0.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkShow_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkShow_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkShow_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkShow.SetIndex(Me._chkShow_0, CType(0, Short))
        Me._chkShow_0.Location = New System.Drawing.Point(8, 22)
        Me._chkShow_0.Name = "_chkShow_0"
        Me._chkShow_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkShow_0.Size = New System.Drawing.Size(57, 18)
        Me._chkShow_0.TabIndex = 22
        Me._chkShow_0.Text = "Salary"
        Me._chkShow_0.UseVisualStyleBackColor = False
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.cboCategory)
        Me.Frame6.Controls.Add(Me.chkCategory)
        Me.Frame6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(200, 0)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(155, 61)
        Me.Frame6.TabIndex = 16
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
        Me.cboCategory.Location = New System.Drawing.Point(4, 34)
        Me.cboCategory.Name = "cboCategory"
        Me.cboCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCategory.Size = New System.Drawing.Size(147, 22)
        Me.cboCategory.TabIndex = 18
        '
        'chkCategory
        '
        Me.chkCategory.AutoSize = True
        Me.chkCategory.BackColor = System.Drawing.SystemColors.Control
        Me.chkCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCategory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCategory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCategory.Location = New System.Drawing.Point(108, 16)
        Me.chkCategory.Name = "chkCategory"
        Me.chkCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCategory.Size = New System.Drawing.Size(46, 18)
        Me.chkCategory.TabIndex = 17
        Me.chkCategory.Text = "ALL"
        Me.chkCategory.UseVisualStyleBackColor = False
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.chkAll)
        Me.Frame4.Controls.Add(Me.cboCeiling)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(84, 0)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(115, 61)
        Me.Frame4.TabIndex = 9
        Me.Frame4.TabStop = False
        Me.Frame4.Text = "Ceiling"
        '
        'chkAll
        '
        Me.chkAll.AutoSize = True
        Me.chkAll.BackColor = System.Drawing.SystemColors.Control
        Me.chkAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAll.Location = New System.Drawing.Point(66, 16)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAll.Size = New System.Drawing.Size(46, 18)
        Me.chkAll.TabIndex = 13
        Me.chkAll.Text = "ALL"
        Me.chkAll.UseVisualStyleBackColor = False
        '
        'cboCeiling
        '
        Me.cboCeiling.BackColor = System.Drawing.SystemColors.Window
        Me.cboCeiling.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCeiling.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCeiling.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCeiling.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCeiling.Location = New System.Drawing.Point(2, 34)
        Me.cboCeiling.Name = "cboCeiling"
        Me.cboCeiling.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCeiling.Size = New System.Drawing.Size(111, 22)
        Me.cboCeiling.TabIndex = 12
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.OptPF)
        Me.Frame3.Controls.Add(Me.optCardNo)
        Me.Frame3.Controls.Add(Me.OptName)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 0)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(83, 61)
        Me.Frame3.TabIndex = 8
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "Order By"
        '
        'OptPF
        '
        Me.OptPF.AutoSize = True
        Me.OptPF.BackColor = System.Drawing.SystemColors.Control
        Me.OptPF.Cursor = System.Windows.Forms.Cursors.Default
        Me.OptPF.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptPF.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptPF.Location = New System.Drawing.Point(6, 30)
        Me.OptPF.Name = "OptPF"
        Me.OptPF.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptPF.Size = New System.Drawing.Size(53, 18)
        Me.OptPF.TabIndex = 19
        Me.OptPF.TabStop = True
        Me.OptPF.Text = "PF No"
        Me.OptPF.UseVisualStyleBackColor = False
        '
        'optCardNo
        '
        Me.optCardNo.AutoSize = True
        Me.optCardNo.BackColor = System.Drawing.SystemColors.Control
        Me.optCardNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.optCardNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCardNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCardNo.Location = New System.Drawing.Point(6, 44)
        Me.optCardNo.Name = "optCardNo"
        Me.optCardNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optCardNo.Size = New System.Drawing.Size(64, 18)
        Me.optCardNo.TabIndex = 11
        Me.optCardNo.TabStop = True
        Me.optCardNo.Text = "Card No"
        Me.optCardNo.UseVisualStyleBackColor = False
        '
        'OptName
        '
        Me.OptName.AutoSize = True
        Me.OptName.BackColor = System.Drawing.SystemColors.Control
        Me.OptName.Cursor = System.Windows.Forms.Cursors.Default
        Me.OptName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptName.Location = New System.Drawing.Point(6, 16)
        Me.OptName.Name = "OptName"
        Me.OptName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptName.Size = New System.Drawing.Size(52, 18)
        Me.OptName.TabIndex = 10
        Me.OptName.TabStop = True
        Me.OptName.Text = "Name"
        Me.OptName.UseVisualStyleBackColor = False
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.lblYear)
        Me.Frame2.Controls.Add(Me.lblRunDate)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(588, -2)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(161, 63)
        Me.Frame2.TabIndex = 1
        Me.Frame2.TabStop = False
        '
        'lblRunDate
        '
        Me.lblRunDate.AutoSize = True
        Me.lblRunDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblRunDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblRunDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRunDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRunDate.Location = New System.Drawing.Point(10, 20)
        Me.lblRunDate.Name = "lblRunDate"
        Me.lblRunDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblRunDate.Size = New System.Drawing.Size(48, 14)
        Me.lblRunDate.TabIndex = 7
        Me.lblRunDate.Text = "RunDate"
        Me.lblRunDate.Visible = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.SprdView)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 56)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(749, 353)
        Me.Frame1.TabIndex = 0
        Me.Frame1.TabStop = False
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(2, 8)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(743, 341)
        Me.SprdView.TabIndex = 20
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.Frame5)
        Me.FraMovement.Controls.Add(Me.cmdCTextFile)
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.cmdRefresh)
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.Report1)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 404)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(749, 51)
        Me.FraMovement.TabIndex = 4
        Me.FraMovement.TabStop = False
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me._OptFields_1)
        Me.Frame5.Controls.Add(Me._OptFields_0)
        Me.Frame5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(260, 8)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(227, 43)
        Me.Frame5.TabIndex = 29
        Me.Frame5.TabStop = False
        Me.Frame5.Text = "Field Details"
        '
        '_OptFields_1
        '
        Me._OptFields_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptFields_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptFields_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptFields_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptFields.SetIndex(Me._OptFields_1, CType(1, Short))
        Me._OptFields_1.Location = New System.Drawing.Point(96, 20)
        Me._OptFields_1.Name = "_OptFields_1"
        Me._OptFields_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptFields_1.Size = New System.Drawing.Size(77, 13)
        Me._OptFields_1.TabIndex = 31
        Me._OptFields_1.TabStop = True
        Me._OptFields_1.Text = "UAN"
        Me._OptFields_1.UseVisualStyleBackColor = False
        '
        '_OptFields_0
        '
        Me._OptFields_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptFields_0.Checked = True
        Me._OptFields_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptFields_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptFields_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptFields.SetIndex(Me._OptFields_0, CType(0, Short))
        Me._OptFields_0.Location = New System.Drawing.Point(4, 20)
        Me._OptFields_0.Name = "_OptFields_0"
        Me._OptFields_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptFields_0.Size = New System.Drawing.Size(77, 13)
        Me._OptFields_0.TabIndex = 30
        Me._OptFields_0.TabStop = True
        Me._OptFields_0.Text = "PF No"
        Me._OptFields_0.UseVisualStyleBackColor = False
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
        Me.cmdPrint.TabIndex = 14
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
        Me.cmdRefresh.TabIndex = 6
        Me.cmdRefresh.Text = "&Refresh"
        Me.cmdRefresh.UseVisualStyleBackColor = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(190, 14)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 30
        '
        'lblYear
        '
        Me.lblYear.CustomFormat = "MMMM,yyyy"
        Me.lblYear.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.lblYear.Location = New System.Drawing.Point(13, 20)
        Me.lblYear.Name = "lblYear"
        Me.lblYear.Size = New System.Drawing.Size(135, 22)
        Me.lblYear.TabIndex = 37
        '
        'frmMonthlyPF
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(749, 456)
        Me.Controls.Add(Me.Frame7)
        Me.Controls.Add(Me.Frame6)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.FraMovement)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "frmMonthlyPF"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "MonthlyPF Detail"
        Me.Frame7.ResumeLayout(False)
        Me.Frame7.PerformLayout()
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        Me.Frame5.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptFields, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkShow, System.ComponentModel.ISupportInitialize).EndInit()
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

    Friend WithEvents lblYear As DateTimePicker
#End Region
End Class