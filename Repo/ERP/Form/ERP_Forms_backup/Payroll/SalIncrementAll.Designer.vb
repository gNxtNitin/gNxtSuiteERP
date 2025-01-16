Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmSalIncrementAll
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
    Public WithEvents cboDept As System.Windows.Forms.ComboBox
    Public WithEvents chkAll As System.Windows.Forms.CheckBox
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents cboCategory As System.Windows.Forms.ComboBox
    Public WithEvents chkCategory As System.Windows.Forms.CheckBox
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents chkContractor As System.Windows.Forms.CheckBox
    Public WithEvents cboConName As System.Windows.Forms.ComboBox
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents optContBasic As System.Windows.Forms.RadioButton
    Public WithEvents optContCeiling As System.Windows.Forms.RadioButton
    Public WithEvents txtWEF As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchSalary As System.Windows.Forms.Button
    Public WithEvents lblAppDate As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents fraTop As System.Windows.Forms.GroupBox
    Public WithEvents cboAppMon As System.Windows.Forms.ComboBox
    Public WithEvents cboAppYear As System.Windows.Forms.ComboBox
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
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
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents FraMain As System.Windows.Forms.GroupBox
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents cmdUpdate As System.Windows.Forms.Button
    Public WithEvents txtNewSalary As System.Windows.Forms.TextBox
    Public WithEvents txtOldSalary As System.Windows.Forms.TextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Frame5 As System.Windows.Forms.GroupBox
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents cmdShow As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents Label44 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSalIncrementAll))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearchSalary = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.FraMain = New System.Windows.Forms.GroupBox()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.cboDept = New System.Windows.Forms.ComboBox()
        Me.chkAll = New System.Windows.Forms.CheckBox()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.cboCategory = New System.Windows.Forms.ComboBox()
        Me.chkCategory = New System.Windows.Forms.CheckBox()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.chkContractor = New System.Windows.Forms.CheckBox()
        Me.cboConName = New System.Windows.Forms.ComboBox()
        Me.fraTop = New System.Windows.Forms.GroupBox()
        Me.optContBasic = New System.Windows.Forms.RadioButton()
        Me.optContCeiling = New System.Windows.Forms.RadioButton()
        Me.txtWEF = New System.Windows.Forms.TextBox()
        Me.lblAppDate = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.cboAppMon = New System.Windows.Forms.ComboBox()
        Me.cboAppYear = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
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
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me.cmdUpdate = New System.Windows.Forms.Button()
        Me.txtNewSalary = New System.Windows.Forms.TextBox()
        Me.txtOldSalary = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.FraMain.SuspendLayout()
        Me.Frame4.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.fraTop.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.fraSalMY.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        Me.Frame5.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSearchSalary
        '
        Me.cmdSearchSalary.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchSalary.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchSalary.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchSalary.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchSalary.Image = CType(resources.GetObject("cmdSearchSalary.Image"), System.Drawing.Image)
        Me.cmdSearchSalary.Location = New System.Drawing.Point(163, 10)
        Me.cmdSearchSalary.Name = "cmdSearchSalary"
        Me.cmdSearchSalary.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchSalary.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearchSalary.TabIndex = 1
        Me.cmdSearchSalary.TabStop = False
        Me.cmdSearchSalary.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchSalary, "Search Salary Define month/year for the employee")
        Me.cmdSearchSalary.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(975, 11)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.TabIndex = 36
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Print Preview")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Enabled = False
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(909, 11)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 35
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print List")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.Location = New System.Drawing.Point(843, 11)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(67, 37)
        Me.CmdSave.TabIndex = 39
        Me.CmdSave.Text = "&Save"
        Me.CmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSave, "Save Record")
        Me.CmdSave.UseVisualStyleBackColor = False
        '
        'cmdShow
        '
        Me.cmdShow.BackColor = System.Drawing.SystemColors.Control
        Me.cmdShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdShow.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdShow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdShow.Image = CType(resources.GetObject("cmdShow.Image"), System.Drawing.Image)
        Me.cmdShow.Location = New System.Drawing.Point(777, 11)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShow.Size = New System.Drawing.Size(67, 37)
        Me.cmdShow.TabIndex = 37
        Me.cmdShow.Text = "Sho&w"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'FraMain
        '
        Me.FraMain.BackColor = System.Drawing.SystemColors.Control
        Me.FraMain.Controls.Add(Me.Frame4)
        Me.FraMain.Controls.Add(Me.Frame6)
        Me.FraMain.Controls.Add(Me.Frame1)
        Me.FraMain.Controls.Add(Me.fraTop)
        Me.FraMain.Controls.Add(Me.Frame2)
        Me.FraMain.Controls.Add(Me.Frame3)
        Me.FraMain.Controls.Add(Me.fraSalMY)
        Me.FraMain.Controls.Add(Me.SprdMain)
        Me.FraMain.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMain.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMain.Location = New System.Drawing.Point(0, -6)
        Me.FraMain.Name = "FraMain"
        Me.FraMain.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMain.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMain.Size = New System.Drawing.Size(1108, 581)
        Me.FraMain.TabIndex = 9
        Me.FraMain.TabStop = False
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.cboDept)
        Me.Frame4.Controls.Add(Me.chkAll)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, 86)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(247, 39)
        Me.Frame4.TabIndex = 32
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
        Me.cboDept.Location = New System.Drawing.Point(6, 14)
        Me.cboDept.Name = "cboDept"
        Me.cboDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDept.Size = New System.Drawing.Size(191, 22)
        Me.cboDept.TabIndex = 34
        '
        'chkAll
        '
        Me.chkAll.AutoSize = True
        Me.chkAll.BackColor = System.Drawing.SystemColors.Control
        Me.chkAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAll.Location = New System.Drawing.Point(200, 16)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAll.Size = New System.Drawing.Size(46, 18)
        Me.chkAll.TabIndex = 33
        Me.chkAll.Text = "ALL"
        Me.chkAll.UseVisualStyleBackColor = False
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.cboCategory)
        Me.Frame6.Controls.Add(Me.chkCategory)
        Me.Frame6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(248, 86)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(251, 39)
        Me.Frame6.TabIndex = 29
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
        Me.cboCategory.Location = New System.Drawing.Point(6, 14)
        Me.cboCategory.Name = "cboCategory"
        Me.cboCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCategory.Size = New System.Drawing.Size(195, 22)
        Me.cboCategory.TabIndex = 31
        '
        'chkCategory
        '
        Me.chkCategory.AutoSize = True
        Me.chkCategory.BackColor = System.Drawing.SystemColors.Control
        Me.chkCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCategory.Enabled = False
        Me.chkCategory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCategory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCategory.Location = New System.Drawing.Point(204, 18)
        Me.chkCategory.Name = "chkCategory"
        Me.chkCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCategory.Size = New System.Drawing.Size(46, 18)
        Me.chkCategory.TabIndex = 30
        Me.chkCategory.Text = "ALL"
        Me.chkCategory.UseVisualStyleBackColor = False
        Me.chkCategory.Visible = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.chkContractor)
        Me.Frame1.Controls.Add(Me.cboConName)
        Me.Frame1.Enabled = False
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(500, 86)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(247, 39)
        Me.Frame1.TabIndex = 26
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Contractor"
        Me.Frame1.Visible = False
        '
        'chkContractor
        '
        Me.chkContractor.AutoSize = True
        Me.chkContractor.BackColor = System.Drawing.SystemColors.Control
        Me.chkContractor.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkContractor.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkContractor.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkContractor.Location = New System.Drawing.Point(200, 14)
        Me.chkContractor.Name = "chkContractor"
        Me.chkContractor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkContractor.Size = New System.Drawing.Size(46, 18)
        Me.chkContractor.TabIndex = 28
        Me.chkContractor.Text = "ALL"
        Me.chkContractor.UseVisualStyleBackColor = False
        '
        'cboConName
        '
        Me.cboConName.BackColor = System.Drawing.SystemColors.Window
        Me.cboConName.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboConName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboConName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboConName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboConName.Location = New System.Drawing.Point(6, 14)
        Me.cboConName.Name = "cboConName"
        Me.cboConName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboConName.Size = New System.Drawing.Size(191, 22)
        Me.cboConName.TabIndex = 27
        '
        'fraTop
        '
        Me.fraTop.BackColor = System.Drawing.SystemColors.Control
        Me.fraTop.Controls.Add(Me.optContBasic)
        Me.fraTop.Controls.Add(Me.optContCeiling)
        Me.fraTop.Controls.Add(Me.txtWEF)
        Me.fraTop.Controls.Add(Me.cmdSearchSalary)
        Me.fraTop.Controls.Add(Me.lblAppDate)
        Me.fraTop.Controls.Add(Me.Label13)
        Me.fraTop.Controls.Add(Me.Label3)
        Me.fraTop.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTop.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraTop.Location = New System.Drawing.Point(0, 2)
        Me.fraTop.Name = "fraTop"
        Me.fraTop.Padding = New System.Windows.Forms.Padding(0)
        Me.fraTop.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraTop.Size = New System.Drawing.Size(749, 35)
        Me.fraTop.TabIndex = 18
        Me.fraTop.TabStop = False
        '
        'optContBasic
        '
        Me.optContBasic.AutoSize = True
        Me.optContBasic.BackColor = System.Drawing.SystemColors.Control
        Me.optContBasic.Checked = True
        Me.optContBasic.Cursor = System.Windows.Forms.Cursors.Default
        Me.optContBasic.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optContBasic.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optContBasic.Location = New System.Drawing.Point(576, 14)
        Me.optContBasic.Name = "optContBasic"
        Me.optContBasic.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optContBasic.Size = New System.Drawing.Size(86, 18)
        Me.optContBasic.TabIndex = 24
        Me.optContBasic.TabStop = True
        Me.optContBasic.Text = "Basic Salary"
        Me.optContBasic.UseVisualStyleBackColor = False
        '
        'optContCeiling
        '
        Me.optContCeiling.AutoSize = True
        Me.optContCeiling.BackColor = System.Drawing.SystemColors.Control
        Me.optContCeiling.Cursor = System.Windows.Forms.Cursors.Default
        Me.optContCeiling.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optContCeiling.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optContCeiling.Location = New System.Drawing.Point(680, 14)
        Me.optContCeiling.Name = "optContCeiling"
        Me.optContCeiling.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optContCeiling.Size = New System.Drawing.Size(56, 18)
        Me.optContCeiling.TabIndex = 23
        Me.optContCeiling.TabStop = True
        Me.optContCeiling.Text = "Ceiling"
        Me.optContCeiling.UseVisualStyleBackColor = False
        '
        'txtWEF
        '
        Me.txtWEF.AcceptsReturn = True
        Me.txtWEF.BackColor = System.Drawing.SystemColors.Window
        Me.txtWEF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWEF.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWEF.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWEF.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtWEF.Location = New System.Drawing.Point(75, 10)
        Me.txtWEF.MaxLength = 0
        Me.txtWEF.Name = "txtWEF"
        Me.txtWEF.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWEF.Size = New System.Drawing.Size(87, 20)
        Me.txtWEF.TabIndex = 0
        '
        'lblAppDate
        '
        Me.lblAppDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblAppDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAppDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAppDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAppDate.Location = New System.Drawing.Point(200, 12)
        Me.lblAppDate.Name = "lblAppDate"
        Me.lblAppDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAppDate.Size = New System.Drawing.Size(109, 15)
        Me.lblAppDate.TabIndex = 40
        Me.lblAppDate.Text = "lblAppDate"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(458, 12)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(100, 14)
        Me.Label13.TabIndex = 25
        Me.Label13.Text = "PF Contribution on :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(4, 12)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(60, 14)
        Me.Label3.TabIndex = 22
        Me.Label3.Text = "WEF Date :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
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
        Me.Frame2.Location = New System.Drawing.Point(248, 36)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(248, 49)
        Me.Frame2.TabIndex = 19
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
        Me.cboAppMon.Location = New System.Drawing.Point(54, 20)
        Me.cboAppMon.Name = "cboAppMon"
        Me.cboAppMon.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboAppMon.Size = New System.Drawing.Size(91, 22)
        Me.cboAppMon.TabIndex = 5
        '
        'cboAppYear
        '
        Me.cboAppYear.BackColor = System.Drawing.SystemColors.Window
        Me.cboAppYear.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboAppYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAppYear.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboAppYear.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboAppYear.Location = New System.Drawing.Point(186, 20)
        Me.cboAppYear.Name = "cboAppYear"
        Me.cboAppYear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboAppYear.Size = New System.Drawing.Size(59, 22)
        Me.cboAppYear.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(8, 24)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(42, 14)
        Me.Label5.TabIndex = 21
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
        Me.Label4.Location = New System.Drawing.Point(149, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(36, 14)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "Year :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
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
        Me.Frame3.Location = New System.Drawing.Point(500, 36)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(248, 49)
        Me.Frame3.TabIndex = 10
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
        Me.cboArrearMonth.Location = New System.Drawing.Point(54, 20)
        Me.cboArrearMonth.Name = "cboArrearMonth"
        Me.cboArrearMonth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboArrearMonth.Size = New System.Drawing.Size(91, 22)
        Me.cboArrearMonth.TabIndex = 7
        '
        'cboArrearYear
        '
        Me.cboArrearYear.BackColor = System.Drawing.SystemColors.Window
        Me.cboArrearYear.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboArrearYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboArrearYear.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboArrearYear.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboArrearYear.Location = New System.Drawing.Point(186, 20)
        Me.cboArrearYear.Name = "cboArrearYear"
        Me.cboArrearYear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboArrearYear.Size = New System.Drawing.Size(59, 22)
        Me.cboArrearYear.TabIndex = 8
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(8, 24)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(42, 14)
        Me.Label8.TabIndex = 11
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
        Me.Label6.Location = New System.Drawing.Point(149, 24)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(36, 14)
        Me.Label6.TabIndex = 12
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
        Me.fraSalMY.Location = New System.Drawing.Point(0, 36)
        Me.fraSalMY.Name = "fraSalMY"
        Me.fraSalMY.Padding = New System.Windows.Forms.Padding(0)
        Me.fraSalMY.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraSalMY.Size = New System.Drawing.Size(248, 49)
        Me.fraSalMY.TabIndex = 14
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
        Me.cboYear.Location = New System.Drawing.Point(186, 20)
        Me.cboYear.Name = "cboYear"
        Me.cboYear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboYear.Size = New System.Drawing.Size(59, 22)
        Me.cboYear.TabIndex = 4
        '
        'cboMonth
        '
        Me.cboMonth.BackColor = System.Drawing.SystemColors.Window
        Me.cboMonth.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMonth.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboMonth.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboMonth.Location = New System.Drawing.Point(54, 20)
        Me.cboMonth.Name = "cboMonth"
        Me.cboMonth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboMonth.Size = New System.Drawing.Size(91, 22)
        Me.cboMonth.TabIndex = 3
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.SystemColors.Control
        Me.Label24.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label24.Location = New System.Drawing.Point(149, 24)
        Me.Label24.Name = "Label24"
        Me.Label24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label24.Size = New System.Drawing.Size(36, 14)
        Me.Label24.TabIndex = 16
        Me.Label24.Text = "Year :"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.SystemColors.Control
        Me.Label29.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label29.Location = New System.Drawing.Point(8, 24)
        Me.Label29.Name = "Label29"
        Me.Label29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label29.Size = New System.Drawing.Size(42, 14)
        Me.Label29.TabIndex = 15
        Me.Label29.Text = "Month :"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(0, 126)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(1105, 452)
        Me.SprdMain.TabIndex = 47
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(749, 413)
        Me.SprdView.TabIndex = 17
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.Frame5)
        Me.FraMovement.Controls.Add(Me.cmdClose)
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.CmdSave)
        Me.FraMovement.Controls.Add(Me.cmdShow)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 570)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(1111, 51)
        Me.FraMovement.TabIndex = 13
        Me.FraMovement.TabStop = False
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me.cmdUpdate)
        Me.Frame5.Controls.Add(Me.txtNewSalary)
        Me.Frame5.Controls.Add(Me.txtOldSalary)
        Me.Frame5.Controls.Add(Me.Label2)
        Me.Frame5.Controls.Add(Me.Label1)
        Me.Frame5.Enabled = False
        Me.Frame5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(0, 0)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(395, 51)
        Me.Frame5.TabIndex = 41
        Me.Frame5.TabStop = False
        Me.Frame5.Visible = False
        '
        'cmdUpdate
        '
        Me.cmdUpdate.BackColor = System.Drawing.SystemColors.Control
        Me.cmdUpdate.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdUpdate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdUpdate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdUpdate.Location = New System.Drawing.Point(186, 26)
        Me.cmdUpdate.Name = "cmdUpdate"
        Me.cmdUpdate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdUpdate.Size = New System.Drawing.Size(93, 21)
        Me.cmdUpdate.TabIndex = 44
        Me.cmdUpdate.Text = "Update"
        Me.cmdUpdate.UseVisualStyleBackColor = False
        '
        'txtNewSalary
        '
        Me.txtNewSalary.AcceptsReturn = True
        Me.txtNewSalary.BackColor = System.Drawing.SystemColors.Window
        Me.txtNewSalary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNewSalary.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNewSalary.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewSalary.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNewSalary.Location = New System.Drawing.Point(90, 28)
        Me.txtNewSalary.MaxLength = 0
        Me.txtNewSalary.Name = "txtNewSalary"
        Me.txtNewSalary.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNewSalary.Size = New System.Drawing.Size(87, 20)
        Me.txtNewSalary.TabIndex = 43
        '
        'txtOldSalary
        '
        Me.txtOldSalary.AcceptsReturn = True
        Me.txtOldSalary.BackColor = System.Drawing.SystemColors.Window
        Me.txtOldSalary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOldSalary.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOldSalary.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOldSalary.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOldSalary.Location = New System.Drawing.Point(90, 8)
        Me.txtOldSalary.MaxLength = 0
        Me.txtOldSalary.Name = "txtOldSalary"
        Me.txtOldSalary.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOldSalary.Size = New System.Drawing.Size(87, 20)
        Me.txtOldSalary.TabIndex = 42
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(2, 30)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(69, 14)
        Me.Label2.TabIndex = 46
        Me.Label2.Text = "New salary :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(6, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(63, 14)
        Me.Label1.TabIndex = 45
        Me.Label1.Text = "Old Salary :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.Location = New System.Drawing.Point(1041, 11)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(67, 37)
        Me.cmdClose.TabIndex = 38
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(0, 0)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 19
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
        Me.Label44.TabIndex = 2
        Me.Label44.Text = "Sex :"
        '
        'frmSalIncrementAll
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdClose
        Me.ClientSize = New System.Drawing.Size(1108, 621)
        Me.Controls.Add(Me.FraMain)
        Me.Controls.Add(Me.SprdView)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.Report1)
        Me.Controls.Add(Me.Label44)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 23)
        Me.MaximizeBox = False
        Me.Name = "frmSalIncrementAll"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Salary Increment (All)"
        Me.FraMain.ResumeLayout(False)
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.fraTop.ResumeLayout(False)
        Me.fraTop.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.fraSalMY.ResumeLayout(False)
        Me.fraSalMY.PerformLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        Me.Frame5.ResumeLayout(False)
        Me.Frame5.PerformLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        ''SprdView.DataSource = CType(AdoDCMain, MSDATASRC.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        SprdView.DataSource = Nothing
    End Sub
#End Region
End Class