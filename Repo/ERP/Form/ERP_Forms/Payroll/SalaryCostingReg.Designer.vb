Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmSalaryCostingReg
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
    Public WithEvents chkOT As System.Windows.Forms.CheckBox
    Public WithEvents chkFF As System.Windows.Forms.CheckBox
    Public WithEvents chkVoucher As System.Windows.Forms.CheckBox
    Public WithEvents chkArrear As System.Windows.Forms.CheckBox
    Public WithEvents chkSalary As System.Windows.Forms.CheckBox
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents cboShow As System.Windows.Forms.ComboBox
    Public WithEvents cboOrder3 As System.Windows.Forms.ComboBox
    Public WithEvents cboOrder2 As System.Windows.Forms.ComboBox
    Public WithEvents cboOrder1 As System.Windows.Forms.ComboBox
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Frame5 As System.Windows.Forms.GroupBox
    Public WithEvents cboDivision As System.Windows.Forms.ComboBox
    Public WithEvents chkDivision As System.Windows.Forms.CheckBox
    Public WithEvents cboCostCenter As System.Windows.Forms.ComboBox
    Public WithEvents chkCostC As System.Windows.Forms.CheckBox
    Public WithEvents cboCategory As System.Windows.Forms.ComboBox
    Public WithEvents chkCategory As System.Windows.Forms.CheckBox
    Public WithEvents chkAll As System.Windows.Forms.CheckBox
    Public WithEvents cboDept As System.Windows.Forms.ComboBox
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents cboMonthTerm As System.Windows.Forms.ComboBox
    Public WithEvents lblMonthTerms As System.Windows.Forms.Label
    Public WithEvents lblIsArrear As System.Windows.Forms.Label
    Public WithEvents lblRunDate As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents sprdAttn As AxFPSpreadADO.AxfpSpread
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents chkLeaves As System.Windows.Forms.CheckBox
    Public WithEvents cboEmpCatType As System.Windows.Forms.ComboBox
    Public WithEvents chkPerksHead As System.Windows.Forms.CheckBox
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdRefresh As System.Windows.Forms.Button
    Public WithEvents cmdLeave As System.Windows.Forms.Button
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents Label61 As System.Windows.Forms.Label
    Public WithEvents lblNetPay As System.Windows.Forms.Label
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents Label8 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSalaryCostingReg))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.chkOT = New System.Windows.Forms.CheckBox()
        Me.chkFF = New System.Windows.Forms.CheckBox()
        Me.chkVoucher = New System.Windows.Forms.CheckBox()
        Me.chkArrear = New System.Windows.Forms.CheckBox()
        Me.chkSalary = New System.Windows.Forms.CheckBox()
        Me.cboShow = New System.Windows.Forms.ComboBox()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me.cboOrder3 = New System.Windows.Forms.ComboBox()
        Me.cboOrder2 = New System.Windows.Forms.ComboBox()
        Me.cboOrder1 = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.cboDivision = New System.Windows.Forms.ComboBox()
        Me.chkDivision = New System.Windows.Forms.CheckBox()
        Me.cboCostCenter = New System.Windows.Forms.ComboBox()
        Me.chkCostC = New System.Windows.Forms.CheckBox()
        Me.cboCategory = New System.Windows.Forms.ComboBox()
        Me.chkCategory = New System.Windows.Forms.CheckBox()
        Me.chkAll = New System.Windows.Forms.CheckBox()
        Me.cboDept = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.cboMonthTerm = New System.Windows.Forms.ComboBox()
        Me.lblMonthTerms = New System.Windows.Forms.Label()
        Me.lblIsArrear = New System.Windows.Forms.Label()
        Me.lblRunDate = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.sprdAttn = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.chkLeaves = New System.Windows.Forms.CheckBox()
        Me.cboEmpCatType = New System.Windows.Forms.ComboBox()
        Me.chkPerksHead = New System.Windows.Forms.CheckBox()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdRefresh = New System.Windows.Forms.Button()
        Me.cmdLeave = New System.Windows.Forms.Button()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.lblNetPay = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblYear = New System.Windows.Forms.DateTimePicker()
        Me.Frame3.SuspendLayout()
        Me.Frame5.SuspendLayout()
        Me.Frame4.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame1.SuspendLayout()
        CType(Me.sprdAttn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.chkOT)
        Me.Frame3.Controls.Add(Me.chkFF)
        Me.Frame3.Controls.Add(Me.chkVoucher)
        Me.Frame3.Controls.Add(Me.chkArrear)
        Me.Frame3.Controls.Add(Me.chkSalary)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(490, 0)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(93, 99)
        Me.Frame3.TabIndex = 42
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "Show"
        '
        'chkOT
        '
        Me.chkOT.AutoSize = True
        Me.chkOT.BackColor = System.Drawing.SystemColors.Control
        Me.chkOT.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkOT.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOT.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkOT.Location = New System.Drawing.Point(4, 72)
        Me.chkOT.Name = "chkOT"
        Me.chkOT.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkOT.Size = New System.Drawing.Size(75, 18)
        Me.chkOT.TabIndex = 47
        Me.chkOT.Text = "Over Time"
        Me.chkOT.UseVisualStyleBackColor = False
        '
        'chkFF
        '
        Me.chkFF.AutoSize = True
        Me.chkFF.BackColor = System.Drawing.SystemColors.Control
        Me.chkFF.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkFF.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFF.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkFF.Location = New System.Drawing.Point(4, 58)
        Me.chkFF.Name = "chkFF"
        Me.chkFF.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkFF.Size = New System.Drawing.Size(51, 18)
        Me.chkFF.TabIndex = 46
        Me.chkFF.Text = "F && F"
        Me.chkFF.UseVisualStyleBackColor = False
        '
        'chkVoucher
        '
        Me.chkVoucher.AutoSize = True
        Me.chkVoucher.BackColor = System.Drawing.SystemColors.Control
        Me.chkVoucher.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkVoucher.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkVoucher.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkVoucher.Location = New System.Drawing.Point(4, 44)
        Me.chkVoucher.Name = "chkVoucher"
        Me.chkVoucher.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkVoucher.Size = New System.Drawing.Size(67, 18)
        Me.chkVoucher.TabIndex = 45
        Me.chkVoucher.Text = "Voucher"
        Me.chkVoucher.UseVisualStyleBackColor = False
        '
        'chkArrear
        '
        Me.chkArrear.AutoSize = True
        Me.chkArrear.BackColor = System.Drawing.SystemColors.Control
        Me.chkArrear.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkArrear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkArrear.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkArrear.Location = New System.Drawing.Point(4, 30)
        Me.chkArrear.Name = "chkArrear"
        Me.chkArrear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkArrear.Size = New System.Drawing.Size(58, 18)
        Me.chkArrear.TabIndex = 44
        Me.chkArrear.Text = "Arrear"
        Me.chkArrear.UseVisualStyleBackColor = False
        '
        'chkSalary
        '
        Me.chkSalary.AutoSize = True
        Me.chkSalary.BackColor = System.Drawing.SystemColors.Control
        Me.chkSalary.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkSalary.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSalary.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkSalary.Location = New System.Drawing.Point(4, 16)
        Me.chkSalary.Name = "chkSalary"
        Me.chkSalary.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkSalary.Size = New System.Drawing.Size(57, 18)
        Me.chkSalary.TabIndex = 43
        Me.chkSalary.Text = "Salary"
        Me.chkSalary.UseVisualStyleBackColor = False
        '
        'cboShow
        '
        Me.cboShow.BackColor = System.Drawing.SystemColors.Window
        Me.cboShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboShow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboShow.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboShow.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboShow.Location = New System.Drawing.Point(626, 72)
        Me.cboShow.Name = "cboShow"
        Me.cboShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboShow.Size = New System.Drawing.Size(121, 22)
        Me.cboShow.TabIndex = 39
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
        Me.Frame5.Size = New System.Drawing.Size(187, 99)
        Me.Frame5.TabIndex = 18
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
        Me.cboOrder3.Location = New System.Drawing.Point(60, 62)
        Me.cboOrder3.Name = "cboOrder3"
        Me.cboOrder3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboOrder3.Size = New System.Drawing.Size(125, 22)
        Me.cboOrder3.TabIndex = 24
        '
        'cboOrder2
        '
        Me.cboOrder2.BackColor = System.Drawing.SystemColors.Window
        Me.cboOrder2.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboOrder2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOrder2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboOrder2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboOrder2.Location = New System.Drawing.Point(60, 40)
        Me.cboOrder2.Name = "cboOrder2"
        Me.cboOrder2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboOrder2.Size = New System.Drawing.Size(125, 22)
        Me.cboOrder2.TabIndex = 23
        '
        'cboOrder1
        '
        Me.cboOrder1.BackColor = System.Drawing.SystemColors.Window
        Me.cboOrder1.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboOrder1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOrder1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboOrder1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboOrder1.Location = New System.Drawing.Point(60, 18)
        Me.cboOrder1.Name = "cboOrder1"
        Me.cboOrder1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboOrder1.Size = New System.Drawing.Size(125, 22)
        Me.cboOrder1.TabIndex = 22
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(8, 64)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(50, 14)
        Me.Label3.TabIndex = 21
        Me.Label3.Text = "Order 3 :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(8, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(50, 14)
        Me.Label2.TabIndex = 20
        Me.Label2.Text = "Order 2 :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(8, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(50, 14)
        Me.Label1.TabIndex = 19
        Me.Label1.Text = "Order 1 :"
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.cboDivision)
        Me.Frame4.Controls.Add(Me.chkDivision)
        Me.Frame4.Controls.Add(Me.cboCostCenter)
        Me.Frame4.Controls.Add(Me.chkCostC)
        Me.Frame4.Controls.Add(Me.cboCategory)
        Me.Frame4.Controls.Add(Me.chkCategory)
        Me.Frame4.Controls.Add(Me.chkAll)
        Me.Frame4.Controls.Add(Me.cboDept)
        Me.Frame4.Controls.Add(Me.Label7)
        Me.Frame4.Controls.Add(Me.Label6)
        Me.Frame4.Controls.Add(Me.Label5)
        Me.Frame4.Controls.Add(Me.Label4)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(188, 0)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(301, 99)
        Me.Frame4.TabIndex = 10
        Me.Frame4.TabStop = False
        '
        'cboDivision
        '
        Me.cboDivision.BackColor = System.Drawing.SystemColors.Window
        Me.cboDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivision.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDivision.Location = New System.Drawing.Point(82, 76)
        Me.cboDivision.Name = "cboDivision"
        Me.cboDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivision.Size = New System.Drawing.Size(169, 22)
        Me.cboDivision.TabIndex = 37
        '
        'chkDivision
        '
        Me.chkDivision.AutoSize = True
        Me.chkDivision.BackColor = System.Drawing.SystemColors.Control
        Me.chkDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkDivision.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDivision.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkDivision.Location = New System.Drawing.Point(252, 78)
        Me.chkDivision.Name = "chkDivision"
        Me.chkDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkDivision.Size = New System.Drawing.Size(46, 18)
        Me.chkDivision.TabIndex = 36
        Me.chkDivision.Text = "ALL"
        Me.chkDivision.UseVisualStyleBackColor = False
        '
        'cboCostCenter
        '
        Me.cboCostCenter.BackColor = System.Drawing.SystemColors.Window
        Me.cboCostCenter.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCostCenter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCostCenter.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCostCenter.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCostCenter.Location = New System.Drawing.Point(82, 54)
        Me.cboCostCenter.Name = "cboCostCenter"
        Me.cboCostCenter.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCostCenter.Size = New System.Drawing.Size(169, 22)
        Me.cboCostCenter.TabIndex = 28
        '
        'chkCostC
        '
        Me.chkCostC.AutoSize = True
        Me.chkCostC.BackColor = System.Drawing.SystemColors.Control
        Me.chkCostC.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCostC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCostC.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCostC.Location = New System.Drawing.Point(252, 56)
        Me.chkCostC.Name = "chkCostC"
        Me.chkCostC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCostC.Size = New System.Drawing.Size(46, 18)
        Me.chkCostC.TabIndex = 27
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
        Me.cboCategory.Location = New System.Drawing.Point(82, 32)
        Me.cboCategory.Name = "cboCategory"
        Me.cboCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCategory.Size = New System.Drawing.Size(169, 22)
        Me.cboCategory.TabIndex = 26
        '
        'chkCategory
        '
        Me.chkCategory.AutoSize = True
        Me.chkCategory.BackColor = System.Drawing.SystemColors.Control
        Me.chkCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCategory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCategory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCategory.Location = New System.Drawing.Point(252, 34)
        Me.chkCategory.Name = "chkCategory"
        Me.chkCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCategory.Size = New System.Drawing.Size(46, 18)
        Me.chkCategory.TabIndex = 25
        Me.chkCategory.Text = "ALL"
        Me.chkCategory.UseVisualStyleBackColor = False
        '
        'chkAll
        '
        Me.chkAll.AutoSize = True
        Me.chkAll.BackColor = System.Drawing.SystemColors.Control
        Me.chkAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAll.Location = New System.Drawing.Point(252, 14)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAll.Size = New System.Drawing.Size(46, 18)
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
        Me.cboDept.Location = New System.Drawing.Point(82, 10)
        Me.cboDept.Name = "cboDept"
        Me.cboDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDept.Size = New System.Drawing.Size(169, 22)
        Me.cboDept.TabIndex = 11
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(8, 78)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(50, 14)
        Me.Label7.TabIndex = 38
        Me.Label7.Text = "Division :"
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
        Me.Label6.TabIndex = 31
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
        Me.Label5.TabIndex = 30
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
        Me.Label4.TabIndex = 29
        Me.Label4.Text = "Cost Center :"
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.lblYear)
        Me.Frame2.Controls.Add(Me.cboMonthTerm)
        Me.Frame2.Controls.Add(Me.lblMonthTerms)
        Me.Frame2.Controls.Add(Me.lblIsArrear)
        Me.Frame2.Controls.Add(Me.lblRunDate)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(584, -2)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(163, 73)
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
        Me.cboMonthTerm.Location = New System.Drawing.Point(82, 50)
        Me.cboMonthTerm.Name = "cboMonthTerm"
        Me.cboMonthTerm.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboMonthTerm.Size = New System.Drawing.Size(79, 22)
        Me.cboMonthTerm.TabIndex = 32
        Me.cboMonthTerm.Visible = False
        '
        'lblMonthTerms
        '
        Me.lblMonthTerms.AutoSize = True
        Me.lblMonthTerms.BackColor = System.Drawing.SystemColors.Control
        Me.lblMonthTerms.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMonthTerms.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMonthTerms.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMonthTerms.Location = New System.Drawing.Point(8, 52)
        Me.lblMonthTerms.Name = "lblMonthTerms"
        Me.lblMonthTerms.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMonthTerms.Size = New System.Drawing.Size(65, 14)
        Me.lblMonthTerms.TabIndex = 33
        Me.lblMonthTerms.Text = "Month Term:"
        Me.lblMonthTerms.Visible = False
        '
        'lblIsArrear
        '
        Me.lblIsArrear.BackColor = System.Drawing.SystemColors.Control
        Me.lblIsArrear.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblIsArrear.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIsArrear.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblIsArrear.Location = New System.Drawing.Point(68, 14)
        Me.lblIsArrear.Name = "lblIsArrear"
        Me.lblIsArrear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblIsArrear.Size = New System.Drawing.Size(31, 21)
        Me.lblIsArrear.TabIndex = 15
        Me.lblIsArrear.Text = "lblIsArrear"
        Me.lblIsArrear.Visible = False
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
        Me.Frame1.Location = New System.Drawing.Point(0, 94)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(749, 315)
        Me.Frame1.TabIndex = 0
        Me.Frame1.TabStop = False
        '
        'sprdAttn
        '
        Me.sprdAttn.DataSource = Nothing
        Me.sprdAttn.Location = New System.Drawing.Point(2, 8)
        Me.sprdAttn.Name = "sprdAttn"
        Me.sprdAttn.OcxState = CType(resources.GetObject("sprdAttn.OcxState"), System.Windows.Forms.AxHost.State)
        Me.sprdAttn.Size = New System.Drawing.Size(743, 303)
        Me.sprdAttn.TabIndex = 4
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.chkLeaves)
        Me.FraMovement.Controls.Add(Me.cboEmpCatType)
        Me.FraMovement.Controls.Add(Me.chkPerksHead)
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.cmdRefresh)
        Me.FraMovement.Controls.Add(Me.cmdLeave)
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.Report1)
        Me.FraMovement.Controls.Add(Me.Label61)
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
        'chkLeaves
        '
        Me.chkLeaves.AutoSize = True
        Me.chkLeaves.BackColor = System.Drawing.SystemColors.Control
        Me.chkLeaves.Checked = True
        Me.chkLeaves.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkLeaves.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkLeaves.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkLeaves.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkLeaves.Location = New System.Drawing.Point(324, 10)
        Me.chkLeaves.Name = "chkLeaves"
        Me.chkLeaves.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkLeaves.Size = New System.Drawing.Size(107, 18)
        Me.chkLeaves.TabIndex = 41
        Me.chkLeaves.Text = "Including Leaves"
        Me.chkLeaves.UseVisualStyleBackColor = False
        '
        'cboEmpCatType
        '
        Me.cboEmpCatType.BackColor = System.Drawing.SystemColors.Window
        Me.cboEmpCatType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboEmpCatType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEmpCatType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboEmpCatType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cboEmpCatType.Location = New System.Drawing.Point(455, 26)
        Me.cboEmpCatType.Name = "cboEmpCatType"
        Me.cboEmpCatType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboEmpCatType.Size = New System.Drawing.Size(123, 22)
        Me.cboEmpCatType.TabIndex = 34
        '
        'chkPerksHead
        '
        Me.chkPerksHead.AutoSize = True
        Me.chkPerksHead.BackColor = System.Drawing.SystemColors.Control
        Me.chkPerksHead.Checked = True
        Me.chkPerksHead.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkPerksHead.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPerksHead.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPerksHead.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPerksHead.Location = New System.Drawing.Point(456, 10)
        Me.chkPerksHead.Name = "chkPerksHead"
        Me.chkPerksHead.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPerksHead.Size = New System.Drawing.Size(98, 18)
        Me.chkPerksHead.TabIndex = 17
        Me.chkPerksHead.Text = "Including Perks"
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
        Me.Report1.Location = New System.Drawing.Point(216, 18)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 42
        '
        'Label61
        '
        Me.Label61.AutoSize = True
        Me.Label61.BackColor = System.Drawing.Color.Transparent
        Me.Label61.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label61.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label61.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label61.Location = New System.Drawing.Point(332, 28)
        Me.Label61.Name = "Label61"
        Me.Label61.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label61.Size = New System.Drawing.Size(106, 14)
        Me.Label61.TabIndex = 35
        Me.Label61.Text = "Emp. Category Type:"
        Me.Label61.TextAlign = System.Drawing.ContentAlignment.TopRight
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
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(586, 76)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(42, 14)
        Me.Label8.TabIndex = 40
        Me.Label8.Text = "Show :"
        '
        'lblYear
        '
        Me.lblYear.CustomFormat = "MMMM,yyyy"
        Me.lblYear.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.lblYear.Location = New System.Drawing.Point(3, 26)
        Me.lblYear.Name = "lblYear"
        Me.lblYear.Size = New System.Drawing.Size(157, 20)
        Me.lblYear.TabIndex = 36
        '
        'frmSalaryCostingReg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(749, 456)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.cboShow)
        Me.Controls.Add(Me.Frame5)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.Label8)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 23)
        Me.MaximizeBox = False
        Me.Name = "frmSalaryCostingReg"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Salary Costing Register"
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
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
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblYear As DateTimePicker
#End Region
End Class