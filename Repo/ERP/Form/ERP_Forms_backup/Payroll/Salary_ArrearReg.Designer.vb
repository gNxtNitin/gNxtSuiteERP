Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmSalary_ArrearReg
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
    Public WithEvents cboOrder3 As System.Windows.Forms.ComboBox
    Public WithEvents cboOrder2 As System.Windows.Forms.ComboBox
    Public WithEvents cboOrder1 As System.Windows.Forms.ComboBox
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Frame5 As System.Windows.Forms.GroupBox
    Public WithEvents cboCostCenter As System.Windows.Forms.ComboBox
    Public WithEvents chkCostC As System.Windows.Forms.CheckBox
    Public WithEvents cboCategory As System.Windows.Forms.ComboBox
    Public WithEvents chkCategory As System.Windows.Forms.CheckBox
    Public WithEvents chkAll As System.Windows.Forms.CheckBox
    Public WithEvents cboDept As System.Windows.Forms.ComboBox
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents cboMonthTerm As System.Windows.Forms.ComboBox
    Public WithEvents lblMonthTerms As System.Windows.Forms.Label
    Public WithEvents lblRunDate As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents sprdAttn As AxFPSpreadADO.AxfpSpread
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents chkPerksHead As System.Windows.Forms.CheckBox
    Public WithEvents cmdPFESIPosting As System.Windows.Forms.Button
    Public WithEvents cmdAccountPost As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdRefresh As System.Windows.Forms.Button
    Public WithEvents cmdLeave As System.Windows.Forms.Button
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents lblNetPay As System.Windows.Forms.Label
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSalary_ArrearReg))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdPFESIPosting = New System.Windows.Forms.Button()
        Me.cmdAccountPost = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me.cboOrder3 = New System.Windows.Forms.ComboBox()
        Me.cboOrder2 = New System.Windows.Forms.ComboBox()
        Me.cboOrder1 = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.cboCostCenter = New System.Windows.Forms.ComboBox()
        Me.chkCostC = New System.Windows.Forms.CheckBox()
        Me.cboCategory = New System.Windows.Forms.ComboBox()
        Me.chkCategory = New System.Windows.Forms.CheckBox()
        Me.chkAll = New System.Windows.Forms.CheckBox()
        Me.cboDept = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.cboMonthTerm = New System.Windows.Forms.ComboBox()
        Me.lblMonthTerms = New System.Windows.Forms.Label()
        Me.lblRunDate = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.sprdAttn = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.chkPerksHead = New System.Windows.Forms.CheckBox()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdRefresh = New System.Windows.Forms.Button()
        Me.cmdLeave = New System.Windows.Forms.Button()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.lblNetPay = New System.Windows.Forms.Label()
        Me.lblYear = New System.Windows.Forms.DateTimePicker()
        Me.Frame5.SuspendLayout()
        Me.Frame4.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame1.SuspendLayout()
        CType(Me.sprdAttn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdPFESIPosting
        '
        Me.cmdPFESIPosting.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPFESIPosting.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPFESIPosting.Enabled = False
        Me.cmdPFESIPosting.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPFESIPosting.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPFESIPosting.Location = New System.Drawing.Point(400, 12)
        Me.cmdPFESIPosting.Name = "cmdPFESIPosting"
        Me.cmdPFESIPosting.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPFESIPosting.Size = New System.Drawing.Size(79, 34)
        Me.cmdPFESIPosting.TabIndex = 17
        Me.cmdPFESIPosting.Text = "PF && &ESI Posting"
        Me.ToolTip1.SetToolTip(Me.cmdPFESIPosting, "Print PO")
        Me.cmdPFESIPosting.UseVisualStyleBackColor = False
        Me.cmdPFESIPosting.Visible = False
        '
        'cmdAccountPost
        '
        Me.cmdAccountPost.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAccountPost.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAccountPost.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAccountPost.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAccountPost.Location = New System.Drawing.Point(322, 12)
        Me.cmdAccountPost.Name = "cmdAccountPost"
        Me.cmdAccountPost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAccountPost.Size = New System.Drawing.Size(79, 34)
        Me.cmdAccountPost.TabIndex = 15
        Me.cmdAccountPost.Text = "&A/c Posting"
        Me.ToolTip1.SetToolTip(Me.cmdAccountPost, "Print PO")
        Me.cmdAccountPost.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Location = New System.Drawing.Point(164, 12)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(79, 34)
        Me.CmdPreview.TabIndex = 14
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
        Me.CmdClose.Location = New System.Drawing.Point(662, 12)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(80, 34)
        Me.CmdClose.TabIndex = 6
        Me.CmdClose.Text = "&Close"
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me.cboOrder3)
        Me.Frame5.Controls.Add(Me.cboOrder2)
        Me.Frame5.Controls.Add(Me.cboOrder1)
        Me.Frame5.Controls.Add(Me.Label3)
        Me.Frame5.Controls.Add(Me.Label2)
        Me.Frame5.Controls.Add(Me.Label1)
        Me.Frame5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(0, 0)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(203, 79)
        Me.Frame5.TabIndex = 19
        Me.Frame5.TabStop = False
        Me.Frame5.Text = "Order By"
        '
        'cboOrder3
        '
        Me.cboOrder3.BackColor = System.Drawing.SystemColors.Window
        Me.cboOrder3.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboOrder3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOrder3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboOrder3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboOrder3.Location = New System.Drawing.Point(60, 54)
        Me.cboOrder3.Name = "cboOrder3"
        Me.cboOrder3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboOrder3.Size = New System.Drawing.Size(139, 22)
        Me.cboOrder3.TabIndex = 25
        '
        'cboOrder2
        '
        Me.cboOrder2.BackColor = System.Drawing.SystemColors.Window
        Me.cboOrder2.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboOrder2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOrder2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboOrder2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboOrder2.Location = New System.Drawing.Point(60, 32)
        Me.cboOrder2.Name = "cboOrder2"
        Me.cboOrder2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboOrder2.Size = New System.Drawing.Size(139, 22)
        Me.cboOrder2.TabIndex = 24
        '
        'cboOrder1
        '
        Me.cboOrder1.BackColor = System.Drawing.SystemColors.Window
        Me.cboOrder1.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboOrder1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOrder1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboOrder1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboOrder1.Location = New System.Drawing.Point(60, 10)
        Me.cboOrder1.Name = "cboOrder1"
        Me.cboOrder1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboOrder1.Size = New System.Drawing.Size(139, 22)
        Me.cboOrder1.TabIndex = 23
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(8, 56)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(50, 14)
        Me.Label3.TabIndex = 22
        Me.Label3.Text = "Order 3 :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(8, 34)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(50, 14)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "Order 2 :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(8, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(50, 14)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "Order 1 :"
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.cboCostCenter)
        Me.Frame4.Controls.Add(Me.chkCostC)
        Me.Frame4.Controls.Add(Me.cboCategory)
        Me.Frame4.Controls.Add(Me.chkCategory)
        Me.Frame4.Controls.Add(Me.chkAll)
        Me.Frame4.Controls.Add(Me.cboDept)
        Me.Frame4.Controls.Add(Me.Label6)
        Me.Frame4.Controls.Add(Me.Label5)
        Me.Frame4.Controls.Add(Me.Label4)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(204, 0)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(329, 79)
        Me.Frame4.TabIndex = 10
        Me.Frame4.TabStop = False
        '
        'cboCostCenter
        '
        Me.cboCostCenter.BackColor = System.Drawing.SystemColors.Window
        Me.cboCostCenter.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCostCenter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCostCenter.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCostCenter.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCostCenter.Location = New System.Drawing.Point(94, 54)
        Me.cboCostCenter.Name = "cboCostCenter"
        Me.cboCostCenter.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCostCenter.Size = New System.Drawing.Size(183, 22)
        Me.cboCostCenter.TabIndex = 29
        '
        'chkCostC
        '
        Me.chkCostC.BackColor = System.Drawing.SystemColors.Control
        Me.chkCostC.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCostC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCostC.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCostC.Location = New System.Drawing.Point(278, 56)
        Me.chkCostC.Name = "chkCostC"
        Me.chkCostC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCostC.Size = New System.Drawing.Size(45, 13)
        Me.chkCostC.TabIndex = 28
        Me.chkCostC.Text = "ALL"
        Me.chkCostC.UseVisualStyleBackColor = False
        '
        'cboCategory
        '
        Me.cboCategory.BackColor = System.Drawing.SystemColors.Window
        Me.cboCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCategory.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCategory.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCategory.Location = New System.Drawing.Point(94, 32)
        Me.cboCategory.Name = "cboCategory"
        Me.cboCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCategory.Size = New System.Drawing.Size(183, 22)
        Me.cboCategory.TabIndex = 27
        '
        'chkCategory
        '
        Me.chkCategory.BackColor = System.Drawing.SystemColors.Control
        Me.chkCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCategory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCategory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCategory.Location = New System.Drawing.Point(278, 34)
        Me.chkCategory.Name = "chkCategory"
        Me.chkCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCategory.Size = New System.Drawing.Size(45, 13)
        Me.chkCategory.TabIndex = 26
        Me.chkCategory.Text = "ALL"
        Me.chkCategory.UseVisualStyleBackColor = False
        '
        'chkAll
        '
        Me.chkAll.BackColor = System.Drawing.SystemColors.Control
        Me.chkAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAll.Location = New System.Drawing.Point(278, 14)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAll.Size = New System.Drawing.Size(45, 13)
        Me.chkAll.TabIndex = 12
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
        Me.cboDept.Location = New System.Drawing.Point(94, 10)
        Me.cboDept.Name = "cboDept"
        Me.cboDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDept.Size = New System.Drawing.Size(183, 22)
        Me.cboDept.TabIndex = 11
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(6, 14)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(68, 14)
        Me.Label6.TabIndex = 32
        Me.Label6.Text = "Department :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(6, 34)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(57, 14)
        Me.Label5.TabIndex = 31
        Me.Label5.Text = "Category :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(6, 56)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(70, 14)
        Me.Label4.TabIndex = 30
        Me.Label4.Text = "Cost Center :"
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.lblYear)
        Me.Frame2.Controls.Add(Me.cboMonthTerm)
        Me.Frame2.Controls.Add(Me.lblMonthTerms)
        Me.Frame2.Controls.Add(Me.lblRunDate)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(534, -2)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(215, 79)
        Me.Frame2.TabIndex = 1
        Me.Frame2.TabStop = False
        '
        'cboMonthTerm
        '
        Me.cboMonthTerm.BackColor = System.Drawing.SystemColors.Window
        Me.cboMonthTerm.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboMonthTerm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMonthTerm.Enabled = False
        Me.cboMonthTerm.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboMonthTerm.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboMonthTerm.Location = New System.Drawing.Point(132, 50)
        Me.cboMonthTerm.Name = "cboMonthTerm"
        Me.cboMonthTerm.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboMonthTerm.Size = New System.Drawing.Size(79, 22)
        Me.cboMonthTerm.TabIndex = 33
        Me.cboMonthTerm.Visible = False
        '
        'lblMonthTerms
        '
        Me.lblMonthTerms.AutoSize = True
        Me.lblMonthTerms.BackColor = System.Drawing.SystemColors.Control
        Me.lblMonthTerms.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMonthTerms.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMonthTerms.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMonthTerms.Location = New System.Drawing.Point(58, 52)
        Me.lblMonthTerms.Name = "lblMonthTerms"
        Me.lblMonthTerms.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMonthTerms.Size = New System.Drawing.Size(65, 14)
        Me.lblMonthTerms.TabIndex = 34
        Me.lblMonthTerms.Text = "Month Term:"
        Me.lblMonthTerms.Visible = False
        '
        'lblRunDate
        '
        Me.lblRunDate.AutoSize = True
        Me.lblRunDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblRunDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblRunDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRunDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRunDate.Location = New System.Drawing.Point(10, 14)
        Me.lblRunDate.Name = "lblRunDate"
        Me.lblRunDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblRunDate.Size = New System.Drawing.Size(48, 14)
        Me.lblRunDate.TabIndex = 9
        Me.lblRunDate.Text = "RunDate"
        Me.lblRunDate.Visible = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.sprdAttn)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 74)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(749, 335)
        Me.Frame1.TabIndex = 0
        Me.Frame1.TabStop = False
        '
        'sprdAttn
        '
        Me.sprdAttn.DataSource = Nothing
        Me.sprdAttn.Location = New System.Drawing.Point(2, 8)
        Me.sprdAttn.Name = "sprdAttn"
        Me.sprdAttn.OcxState = CType(resources.GetObject("sprdAttn.OcxState"), System.Windows.Forms.AxHost.State)
        Me.sprdAttn.Size = New System.Drawing.Size(743, 325)
        Me.sprdAttn.TabIndex = 4
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.chkPerksHead)
        Me.FraMovement.Controls.Add(Me.cmdPFESIPosting)
        Me.FraMovement.Controls.Add(Me.cmdAccountPost)
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.cmdRefresh)
        Me.FraMovement.Controls.Add(Me.cmdLeave)
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.Report1)
        Me.FraMovement.Controls.Add(Me.lblNetPay)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 404)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(749, 51)
        Me.FraMovement.TabIndex = 5
        Me.FraMovement.TabStop = False
        '
        'chkPerksHead
        '
        Me.chkPerksHead.BackColor = System.Drawing.SystemColors.Control
        Me.chkPerksHead.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPerksHead.Enabled = False
        Me.chkPerksHead.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPerksHead.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPerksHead.Location = New System.Drawing.Point(486, 18)
        Me.chkPerksHead.Name = "chkPerksHead"
        Me.chkPerksHead.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPerksHead.Size = New System.Drawing.Size(89, 21)
        Me.chkPerksHead.TabIndex = 18
        Me.chkPerksHead.Text = "Only Perks"
        Me.chkPerksHead.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Location = New System.Drawing.Point(84, 12)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(80, 34)
        Me.cmdPrint.TabIndex = 13
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'cmdRefresh
        '
        Me.cmdRefresh.BackColor = System.Drawing.SystemColors.Control
        Me.cmdRefresh.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdRefresh.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdRefresh.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdRefresh.Location = New System.Drawing.Point(582, 12)
        Me.cmdRefresh.Name = "cmdRefresh"
        Me.cmdRefresh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdRefresh.Size = New System.Drawing.Size(80, 34)
        Me.cmdRefresh.TabIndex = 8
        Me.cmdRefresh.Text = "&Refresh"
        Me.cmdRefresh.UseVisualStyleBackColor = False
        '
        'cmdLeave
        '
        Me.cmdLeave.BackColor = System.Drawing.SystemColors.Control
        Me.cmdLeave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdLeave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdLeave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdLeave.Location = New System.Drawing.Point(4, 12)
        Me.cmdLeave.Name = "cmdLeave"
        Me.cmdLeave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdLeave.Size = New System.Drawing.Size(80, 34)
        Me.cmdLeave.TabIndex = 7
        Me.cmdLeave.Text = "&Leave"
        Me.cmdLeave.UseVisualStyleBackColor = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(270, 14)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 19
        '
        'lblNetPay
        '
        Me.lblNetPay.AutoSize = True
        Me.lblNetPay.BackColor = System.Drawing.SystemColors.Control
        Me.lblNetPay.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblNetPay.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetPay.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblNetPay.Location = New System.Drawing.Point(322, 16)
        Me.lblNetPay.Name = "lblNetPay"
        Me.lblNetPay.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblNetPay.Size = New System.Drawing.Size(13, 14)
        Me.lblNetPay.TabIndex = 16
        Me.lblNetPay.Text = "0"
        Me.lblNetPay.Visible = False
        '
        'lblYear
        '
        Me.lblYear.CustomFormat = "MMMM,yyyy"
        Me.lblYear.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.lblYear.Location = New System.Drawing.Point(54, 18)
        Me.lblYear.Name = "lblYear"
        Me.lblYear.Size = New System.Drawing.Size(157, 22)
        Me.lblYear.TabIndex = 36
        '
        'frmSalary_ArrearReg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(749, 456)
        Me.Controls.Add(Me.Frame5)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.FraMovement)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 23)
        Me.MaximizeBox = False
        Me.Name = "frmSalary_ArrearReg"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Salary Register (Include Arrear)"
        Me.Frame5.ResumeLayout(False)
        Me.Frame5.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        CType(Me.sprdAttn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        Me.FraMovement.PerformLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lblYear As DateTimePicker
#End Region
End Class