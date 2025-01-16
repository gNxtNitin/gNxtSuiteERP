Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmEmpCompAssetsDetail
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
    Public WithEvents chkDesgCategory As System.Windows.Forms.CheckBox
    Public WithEvents cboDesgCategory As System.Windows.Forms.ComboBox
    Public WithEvents Frame7 As System.Windows.Forms.GroupBox
    Public WithEvents optDept As System.Windows.Forms.RadioButton
    Public WithEvents optCardNo As System.Windows.Forms.RadioButton
    Public WithEvents OptName As System.Windows.Forms.RadioButton
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents cboCategory As System.Windows.Forms.ComboBox
    Public WithEvents chkCategory As System.Windows.Forms.CheckBox
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents optExisting As System.Windows.Forms.RadioButton
    Public WithEvents optAllEmp As System.Windows.Forms.RadioButton
    Public WithEvents FraType As System.Windows.Forms.GroupBox
    Public WithEvents chkAll As System.Windows.Forms.CheckBox
    Public WithEvents cboDept As System.Windows.Forms.ComboBox
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents txtName As System.Windows.Forms.TextBox
    Public WithEvents txtEmpCode As System.Windows.Forms.TextBox
    Public WithEvents cmdsearch As System.Windows.Forms.Button
    Public WithEvents chkAllEmp As System.Windows.Forms.CheckBox
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Frame5 As System.Windows.Forms.GroupBox
    Public WithEvents sprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents cboEmpCatType As System.Windows.Forms.ComboBox
    Public WithEvents Frame9 As System.Windows.Forms.GroupBox
    Public WithEvents cboShow As System.Windows.Forms.ComboBox
    Public WithEvents Frame8 As System.Windows.Forms.GroupBox
    Public WithEvents chkConsolidated As System.Windows.Forms.CheckBox
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdRefresh As System.Windows.Forms.Button
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents lblRegType As System.Windows.Forms.Label
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEmpCompAssetsDetail))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.Frame7 = New System.Windows.Forms.GroupBox()
        Me.chkDesgCategory = New System.Windows.Forms.CheckBox()
        Me.cboDesgCategory = New System.Windows.Forms.ComboBox()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.optDept = New System.Windows.Forms.RadioButton()
        Me.optCardNo = New System.Windows.Forms.RadioButton()
        Me.OptName = New System.Windows.Forms.RadioButton()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.cboCategory = New System.Windows.Forms.ComboBox()
        Me.chkCategory = New System.Windows.Forms.CheckBox()
        Me.FraType = New System.Windows.Forms.GroupBox()
        Me.optExisting = New System.Windows.Forms.RadioButton()
        Me.optAllEmp = New System.Windows.Forms.RadioButton()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.chkAll = New System.Windows.Forms.CheckBox()
        Me.cboDept = New System.Windows.Forms.ComboBox()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.txtEmpCode = New System.Windows.Forms.TextBox()
        Me.chkAllEmp = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.sprdView = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.Frame9 = New System.Windows.Forms.GroupBox()
        Me.cboEmpCatType = New System.Windows.Forms.ComboBox()
        Me.Frame8 = New System.Windows.Forms.GroupBox()
        Me.cboShow = New System.Windows.Forms.ComboBox()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.chkConsolidated = New System.Windows.Forms.CheckBox()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdRefresh = New System.Windows.Forms.Button()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.lblRegType = New System.Windows.Forms.Label()
        Me.Frame7.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me.FraType.SuspendLayout()
        Me.Frame4.SuspendLayout()
        Me.Frame5.SuspendLayout()
        Me.Frame1.SuspendLayout()
        CType(Me.sprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        Me.Frame9.SuspendLayout()
        Me.Frame8.SuspendLayout()
        Me.Frame2.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdsearch
        '
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.Enabled = False
        Me.cmdsearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.Location = New System.Drawing.Point(178, 10)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(27, 19)
        Me.cmdsearch.TabIndex = 30
        Me.cmdsearch.TabStop = False
        Me.cmdsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearch, "Search")
        Me.cmdsearch.UseVisualStyleBackColor = False
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
        Me.CmdPreview.TabIndex = 11
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
        Me.CmdClose.TabIndex = 2
        Me.CmdClose.Text = "&Close"
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'Frame7
        '
        Me.Frame7.BackColor = System.Drawing.SystemColors.Control
        Me.Frame7.Controls.Add(Me.chkDesgCategory)
        Me.Frame7.Controls.Add(Me.cboDesgCategory)
        Me.Frame7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame7.Location = New System.Drawing.Point(430, 0)
        Me.Frame7.Name = "Frame7"
        Me.Frame7.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame7.Size = New System.Drawing.Size(193, 43)
        Me.Frame7.TabIndex = 19
        Me.Frame7.TabStop = False
        Me.Frame7.Text = "Desg Category"
        '
        'chkDesgCategory
        '
        Me.chkDesgCategory.AutoSize = True
        Me.chkDesgCategory.BackColor = System.Drawing.SystemColors.Control
        Me.chkDesgCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkDesgCategory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDesgCategory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkDesgCategory.Location = New System.Drawing.Point(144, 20)
        Me.chkDesgCategory.Name = "chkDesgCategory"
        Me.chkDesgCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkDesgCategory.Size = New System.Drawing.Size(46, 18)
        Me.chkDesgCategory.TabIndex = 21
        Me.chkDesgCategory.Text = "ALL"
        Me.chkDesgCategory.UseVisualStyleBackColor = False
        '
        'cboDesgCategory
        '
        Me.cboDesgCategory.BackColor = System.Drawing.SystemColors.Window
        Me.cboDesgCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDesgCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDesgCategory.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDesgCategory.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDesgCategory.Location = New System.Drawing.Point(4, 16)
        Me.cboDesgCategory.Name = "cboDesgCategory"
        Me.cboDesgCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDesgCategory.Size = New System.Drawing.Size(137, 22)
        Me.cboDesgCategory.TabIndex = 20
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.optDept)
        Me.Frame3.Controls.Add(Me.optCardNo)
        Me.Frame3.Controls.Add(Me.OptName)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 0)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(81, 87)
        Me.Frame3.TabIndex = 4
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "Order By"
        '
        'optDept
        '
        Me.optDept.AutoSize = True
        Me.optDept.BackColor = System.Drawing.SystemColors.Control
        Me.optDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.optDept.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optDept.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDept.Location = New System.Drawing.Point(4, 56)
        Me.optDept.Name = "optDept"
        Me.optDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optDept.Size = New System.Drawing.Size(47, 18)
        Me.optDept.TabIndex = 18
        Me.optDept.TabStop = True
        Me.optDept.Text = "Dept"
        Me.optDept.UseVisualStyleBackColor = False
        Me.optDept.Visible = False
        '
        'optCardNo
        '
        Me.optCardNo.AutoSize = True
        Me.optCardNo.BackColor = System.Drawing.SystemColors.Control
        Me.optCardNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.optCardNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCardNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCardNo.Location = New System.Drawing.Point(4, 38)
        Me.optCardNo.Name = "optCardNo"
        Me.optCardNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optCardNo.Size = New System.Drawing.Size(64, 18)
        Me.optCardNo.TabIndex = 7
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
        Me.OptName.Location = New System.Drawing.Point(4, 20)
        Me.OptName.Name = "OptName"
        Me.OptName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptName.Size = New System.Drawing.Size(52, 18)
        Me.OptName.TabIndex = 6
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
        Me.Frame6.Location = New System.Drawing.Point(430, 44)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(193, 43)
        Me.Frame6.TabIndex = 12
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
        Me.cboCategory.Size = New System.Drawing.Size(139, 22)
        Me.cboCategory.TabIndex = 14
        '
        'chkCategory
        '
        Me.chkCategory.AutoSize = True
        Me.chkCategory.BackColor = System.Drawing.SystemColors.Control
        Me.chkCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCategory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCategory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCategory.Location = New System.Drawing.Point(146, 16)
        Me.chkCategory.Name = "chkCategory"
        Me.chkCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCategory.Size = New System.Drawing.Size(46, 18)
        Me.chkCategory.TabIndex = 13
        Me.chkCategory.Text = "ALL"
        Me.chkCategory.UseVisualStyleBackColor = False
        '
        'FraType
        '
        Me.FraType.BackColor = System.Drawing.SystemColors.Control
        Me.FraType.Controls.Add(Me.optExisting)
        Me.FraType.Controls.Add(Me.optAllEmp)
        Me.FraType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraType.Location = New System.Drawing.Point(624, 54)
        Me.FraType.Name = "FraType"
        Me.FraType.Padding = New System.Windows.Forms.Padding(0)
        Me.FraType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraType.Size = New System.Drawing.Size(123, 29)
        Me.FraType.TabIndex = 15
        Me.FraType.TabStop = False
        Me.FraType.Text = "Type"
        '
        'optExisting
        '
        Me.optExisting.AutoSize = True
        Me.optExisting.BackColor = System.Drawing.SystemColors.Control
        Me.optExisting.Checked = True
        Me.optExisting.Cursor = System.Windows.Forms.Cursors.Default
        Me.optExisting.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optExisting.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optExisting.Location = New System.Drawing.Point(52, 14)
        Me.optExisting.Name = "optExisting"
        Me.optExisting.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optExisting.Size = New System.Drawing.Size(62, 18)
        Me.optExisting.TabIndex = 17
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
        Me.optAllEmp.Location = New System.Drawing.Point(4, 14)
        Me.optAllEmp.Name = "optAllEmp"
        Me.optAllEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optAllEmp.Size = New System.Drawing.Size(45, 18)
        Me.optAllEmp.TabIndex = 16
        Me.optAllEmp.TabStop = True
        Me.optAllEmp.Text = "ALL"
        Me.optAllEmp.UseVisualStyleBackColor = False
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.chkAll)
        Me.Frame4.Controls.Add(Me.cboDept)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(82, 0)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(347, 39)
        Me.Frame4.TabIndex = 5
        Me.Frame4.TabStop = False
        Me.Frame4.Text = "Department"
        '
        'chkAll
        '
        Me.chkAll.AutoSize = True
        Me.chkAll.BackColor = System.Drawing.SystemColors.Control
        Me.chkAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAll.Location = New System.Drawing.Point(216, 18)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAll.Size = New System.Drawing.Size(46, 18)
        Me.chkAll.TabIndex = 9
        Me.chkAll.Text = "ALL"
        Me.chkAll.UseVisualStyleBackColor = False
        '
        'cboDept
        '
        Me.cboDept.BackColor = System.Drawing.SystemColors.Window
        Me.cboDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDept.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDept.Location = New System.Drawing.Point(4, 14)
        Me.cboDept.Name = "cboDept"
        Me.cboDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDept.Size = New System.Drawing.Size(209, 22)
        Me.cboDept.TabIndex = 8
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me.txtName)
        Me.Frame5.Controls.Add(Me.txtEmpCode)
        Me.Frame5.Controls.Add(Me.cmdsearch)
        Me.Frame5.Controls.Add(Me.chkAllEmp)
        Me.Frame5.Controls.Add(Me.Label3)
        Me.Frame5.Controls.Add(Me.Label4)
        Me.Frame5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(82, 34)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(347, 53)
        Me.Frame5.TabIndex = 28
        Me.Frame5.TabStop = False
        '
        'txtName
        '
        Me.txtName.AcceptsReturn = True
        Me.txtName.BackColor = System.Drawing.SystemColors.Window
        Me.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtName.Enabled = False
        Me.txtName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtName.Location = New System.Drawing.Point(106, 30)
        Me.txtName.MaxLength = 0
        Me.txtName.Name = "txtName"
        Me.txtName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtName.Size = New System.Drawing.Size(237, 19)
        Me.txtName.TabIndex = 32
        '
        'txtEmpCode
        '
        Me.txtEmpCode.AcceptsReturn = True
        Me.txtEmpCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmpCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmpCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmpCode.Enabled = False
        Me.txtEmpCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEmpCode.Location = New System.Drawing.Point(106, 10)
        Me.txtEmpCode.MaxLength = 0
        Me.txtEmpCode.Name = "txtEmpCode"
        Me.txtEmpCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmpCode.Size = New System.Drawing.Size(71, 19)
        Me.txtEmpCode.TabIndex = 31
        '
        'chkAllEmp
        '
        Me.chkAllEmp.AutoSize = True
        Me.chkAllEmp.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllEmp.Checked = True
        Me.chkAllEmp.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllEmp.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllEmp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllEmp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllEmp.Location = New System.Drawing.Point(208, 12)
        Me.chkAllEmp.Name = "chkAllEmp"
        Me.chkAllEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllEmp.Size = New System.Drawing.Size(38, 18)
        Me.chkAllEmp.TabIndex = 29
        Me.chkAllEmp.Text = "All"
        Me.chkAllEmp.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(6, 12)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(87, 14)
        Me.Label3.TabIndex = 34
        Me.Label3.Text = "Employee Code :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(6, 32)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(89, 14)
        Me.Label4.TabIndex = 33
        Me.Label4.Text = "Employee Name :"
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.sprdView)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 82)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(749, 327)
        Me.Frame1.TabIndex = 0
        Me.Frame1.TabStop = False
        '
        'sprdView
        '
        Me.sprdView.DataSource = Nothing
        Me.sprdView.Location = New System.Drawing.Point(2, 8)
        Me.sprdView.Name = "sprdView"
        Me.sprdView.OcxState = CType(resources.GetObject("sprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.sprdView.Size = New System.Drawing.Size(743, 315)
        Me.sprdView.TabIndex = 23
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.Frame9)
        Me.FraMovement.Controls.Add(Me.Frame8)
        Me.FraMovement.Controls.Add(Me.Frame2)
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.cmdRefresh)
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.Report1)
        Me.FraMovement.Controls.Add(Me.lblRegType)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 404)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(749, 51)
        Me.FraMovement.TabIndex = 1
        Me.FraMovement.TabStop = False
        '
        'Frame9
        '
        Me.Frame9.BackColor = System.Drawing.SystemColors.Control
        Me.Frame9.Controls.Add(Me.cboEmpCatType)
        Me.Frame9.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame9.Location = New System.Drawing.Point(206, 10)
        Me.Frame9.Name = "Frame9"
        Me.Frame9.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame9.Size = New System.Drawing.Size(123, 37)
        Me.Frame9.TabIndex = 35
        Me.Frame9.TabStop = False
        Me.Frame9.Text = "Emp. Category Type"
        '
        'cboEmpCatType
        '
        Me.cboEmpCatType.BackColor = System.Drawing.SystemColors.Window
        Me.cboEmpCatType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboEmpCatType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEmpCatType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboEmpCatType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cboEmpCatType.Location = New System.Drawing.Point(4, 12)
        Me.cboEmpCatType.Name = "cboEmpCatType"
        Me.cboEmpCatType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboEmpCatType.Size = New System.Drawing.Size(117, 22)
        Me.cboEmpCatType.TabIndex = 36
        '
        'Frame8
        '
        Me.Frame8.BackColor = System.Drawing.SystemColors.Control
        Me.Frame8.Controls.Add(Me.cboShow)
        Me.Frame8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame8.Location = New System.Drawing.Point(330, 10)
        Me.Frame8.Name = "Frame8"
        Me.Frame8.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame8.Size = New System.Drawing.Size(129, 37)
        Me.Frame8.TabIndex = 26
        Me.Frame8.TabStop = False
        Me.Frame8.Text = "Corporate"
        '
        'cboShow
        '
        Me.cboShow.BackColor = System.Drawing.SystemColors.Window
        Me.cboShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboShow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboShow.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboShow.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboShow.Location = New System.Drawing.Point(4, 12)
        Me.cboShow.Name = "cboShow"
        Me.cboShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboShow.Size = New System.Drawing.Size(123, 22)
        Me.cboShow.TabIndex = 27
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.chkConsolidated)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(460, 10)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(119, 37)
        Me.Frame2.TabIndex = 24
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Consolidated"
        '
        'chkConsolidated
        '
        Me.chkConsolidated.AutoSize = True
        Me.chkConsolidated.BackColor = System.Drawing.SystemColors.Control
        Me.chkConsolidated.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkConsolidated.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkConsolidated.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkConsolidated.Location = New System.Drawing.Point(10, 16)
        Me.chkConsolidated.Name = "chkConsolidated"
        Me.chkConsolidated.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkConsolidated.Size = New System.Drawing.Size(88, 18)
        Me.chkConsolidated.TabIndex = 25
        Me.chkConsolidated.Text = "Consolidated"
        Me.chkConsolidated.UseVisualStyleBackColor = False
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
        Me.cmdPrint.TabIndex = 10
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
        Me.cmdRefresh.TabIndex = 3
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
        Me.Report1.TabIndex = 36
        '
        'lblRegType
        '
        Me.lblRegType.BackColor = System.Drawing.SystemColors.Control
        Me.lblRegType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblRegType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRegType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRegType.Location = New System.Drawing.Point(278, 18)
        Me.lblRegType.Name = "lblRegType"
        Me.lblRegType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblRegType.Size = New System.Drawing.Size(165, 19)
        Me.lblRegType.TabIndex = 22
        Me.lblRegType.Text = "lblRegType"
        '
        'frmEmpCompAssetsDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(749, 456)
        Me.Controls.Add(Me.Frame7)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.Frame6)
        Me.Controls.Add(Me.FraType)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.Frame5)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.FraMovement)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "frmEmpCompAssetsDetail"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Employee Company Assets Details"
        Me.Frame7.ResumeLayout(False)
        Me.Frame7.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me.FraType.ResumeLayout(False)
        Me.FraType.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me.Frame5.ResumeLayout(False)
        Me.Frame5.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        CType(Me.sprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        Me.Frame9.ResumeLayout(False)
        Me.Frame8.ResumeLayout(False)
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        ''SprdView.DataSource = CType(AData1, MSDATASRC.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        sprdView.DataSource = Nothing
    End Sub
#End Region
End Class