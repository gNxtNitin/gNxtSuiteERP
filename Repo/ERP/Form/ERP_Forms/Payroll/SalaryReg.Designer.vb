Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmSalaryReg
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
    Public WithEvents chkPerksHead As System.Windows.Forms.CheckBox
    Public WithEvents cboEmpCatType As System.Windows.Forms.ComboBox
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents cboCorporate As System.Windows.Forms.ComboBox
    Public WithEvents cboOrder3 As System.Windows.Forms.ComboBox
    Public WithEvents cboOrder2 As System.Windows.Forms.ComboBox
    Public WithEvents cboOrder1 As System.Windows.Forms.ComboBox
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Frame5 As System.Windows.Forms.GroupBox
    Public WithEvents chkDivision As System.Windows.Forms.CheckBox
    Public WithEvents cboDivision As System.Windows.Forms.ComboBox
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
    Public WithEvents Command1 As System.Windows.Forms.Button
    Public WithEvents cmdWelFare As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdAccountPost As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdRefresh As System.Windows.Forms.Button
    Public WithEvents cmdLeave As System.Windows.Forms.Button
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents cmdPaySlipeMail As System.Windows.Forms.Button
    Public WithEvents lblShowType As System.Windows.Forms.Label
    Public WithEvents lblNetPay As System.Windows.Forms.Label
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents Label8 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSalaryReg))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Command1 = New System.Windows.Forms.Button()
        Me.cmdAccountPost = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.cmdPaySlipeMail = New System.Windows.Forms.Button()
        Me.cmdReprocessDays = New System.Windows.Forms.Button()
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.chkPerksHead = New System.Windows.Forms.CheckBox()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.cboEmpCatType = New System.Windows.Forms.ComboBox()
        Me.cboCorporate = New System.Windows.Forms.ComboBox()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me.cboOrder3 = New System.Windows.Forms.ComboBox()
        Me.cboOrder2 = New System.Windows.Forms.ComboBox()
        Me.cboOrder1 = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.chkAllEmp = New System.Windows.Forms.CheckBox()
        Me.txtEmpCode = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.chkDivision = New System.Windows.Forms.CheckBox()
        Me.cboDivision = New System.Windows.Forms.ComboBox()
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
        Me.lblYear = New System.Windows.Forms.DateTimePicker()
        Me.cboMonthTerm = New System.Windows.Forms.ComboBox()
        Me.lblMonthTerms = New System.Windows.Forms.Label()
        Me.lblIsArrear = New System.Windows.Forms.Label()
        Me.lblRunDate = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.sprdAttn = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.cmdWelFare = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdRefresh = New System.Windows.Forms.Button()
        Me.cmdLeave = New System.Windows.Forms.Button()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.lblShowType = New System.Windows.Forms.Label()
        Me.lblNetPay = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cboShowSalary = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cmdExport = New System.Windows.Forms.Button()
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
        'Command1
        '
        Me.Command1.BackColor = System.Drawing.SystemColors.Control
        Me.Command1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Command1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Command1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Command1.Location = New System.Drawing.Point(226, 10)
        Me.Command1.Name = "Command1"
        Me.Command1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Command1.Size = New System.Drawing.Size(85, 38)
        Me.Command1.TabIndex = 45
        Me.Command1.Text = "Update List for Payment"
        Me.ToolTip1.SetToolTip(Me.Command1, "Print PO")
        Me.Command1.UseVisualStyleBackColor = False
        '
        'cmdAccountPost
        '
        Me.cmdAccountPost.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAccountPost.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAccountPost.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAccountPost.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAccountPost.Location = New System.Drawing.Point(141, 10)
        Me.cmdAccountPost.Name = "cmdAccountPost"
        Me.cmdAccountPost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAccountPost.Size = New System.Drawing.Size(85, 38)
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
        Me.CmdPreview.Location = New System.Drawing.Point(654, 10)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(85, 38)
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
        Me.CmdClose.Location = New System.Drawing.Point(827, 10)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(86, 38)
        Me.CmdClose.TabIndex = 6
        Me.CmdClose.Text = "&Close"
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'cmdPaySlipeMail
        '
        Me.cmdPaySlipeMail.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPaySlipeMail.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPaySlipeMail.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPaySlipeMail.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPaySlipeMail.Location = New System.Drawing.Point(483, 10)
        Me.cmdPaySlipeMail.Name = "cmdPaySlipeMail"
        Me.cmdPaySlipeMail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPaySlipeMail.Size = New System.Drawing.Size(85, 38)
        Me.cmdPaySlipeMail.TabIndex = 18
        Me.cmdPaySlipeMail.Text = "Pay Slip e-Mail"
        Me.ToolTip1.SetToolTip(Me.cmdPaySlipeMail, "Print PO")
        Me.cmdPaySlipeMail.UseVisualStyleBackColor = False
        '
        'cmdReprocessDays
        '
        Me.cmdReprocessDays.BackColor = System.Drawing.SystemColors.Control
        Me.cmdReprocessDays.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdReprocessDays.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdReprocessDays.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdReprocessDays.Location = New System.Drawing.Point(708, 52)
        Me.cmdReprocessDays.Name = "cmdReprocessDays"
        Me.cmdReprocessDays.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdReprocessDays.Size = New System.Drawing.Size(176, 38)
        Me.cmdReprocessDays.TabIndex = 46
        Me.cmdReprocessDays.Text = "Update Form1 Days"
        Me.ToolTip1.SetToolTip(Me.cmdReprocessDays, "Print PO")
        Me.cmdReprocessDays.UseVisualStyleBackColor = False
        '
        'cmdsearch
        '
        Me.cmdsearch.AutoSize = True
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.Enabled = False
        Me.cmdsearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.Location = New System.Drawing.Point(390, 76)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(32, 26)
        Me.cmdsearch.TabIndex = 39
        Me.cmdsearch.TabStop = False
        Me.cmdsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearch, "Search")
        Me.cmdsearch.UseVisualStyleBackColor = False
        '
        'chkPerksHead
        '
        Me.chkPerksHead.BackColor = System.Drawing.SystemColors.Control
        Me.chkPerksHead.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPerksHead.Enabled = False
        Me.chkPerksHead.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPerksHead.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPerksHead.Location = New System.Drawing.Point(1013, 70)
        Me.chkPerksHead.Name = "chkPerksHead"
        Me.chkPerksHead.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPerksHead.Size = New System.Drawing.Size(89, 21)
        Me.chkPerksHead.TabIndex = 42
        Me.chkPerksHead.Text = "Only Perks"
        Me.chkPerksHead.UseVisualStyleBackColor = False
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.cboEmpCatType)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(889, 60)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(123, 41)
        Me.Frame3.TabIndex = 40
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "Emp. Category Type"
        '
        'cboEmpCatType
        '
        Me.cboEmpCatType.BackColor = System.Drawing.SystemColors.Window
        Me.cboEmpCatType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboEmpCatType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEmpCatType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboEmpCatType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cboEmpCatType.Location = New System.Drawing.Point(4, 14)
        Me.cboEmpCatType.Name = "cboEmpCatType"
        Me.cboEmpCatType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboEmpCatType.Size = New System.Drawing.Size(117, 22)
        Me.cboEmpCatType.TabIndex = 41
        '
        'cboCorporate
        '
        Me.cboCorporate.BackColor = System.Drawing.SystemColors.Window
        Me.cboCorporate.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCorporate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCorporate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCorporate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCorporate.Location = New System.Drawing.Point(66, 78)
        Me.cboCorporate.Name = "cboCorporate"
        Me.cboCorporate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCorporate.Size = New System.Drawing.Size(133, 22)
        Me.cboCorporate.TabIndex = 38
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
        Me.Frame5.Size = New System.Drawing.Size(203, 77)
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
        Me.Frame4.Controls.Add(Me.txtName)
        Me.Frame4.Controls.Add(Me.chkAllEmp)
        Me.Frame4.Controls.Add(Me.cmdsearch)
        Me.Frame4.Controls.Add(Me.txtEmpCode)
        Me.Frame4.Controls.Add(Me.Label10)
        Me.Frame4.Controls.Add(Me.chkDivision)
        Me.Frame4.Controls.Add(Me.cboDivision)
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
        Me.Frame4.Location = New System.Drawing.Point(204, 0)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(500, 110)
        Me.Frame4.TabIndex = 10
        Me.Frame4.TabStop = False
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
        Me.txtName.Location = New System.Drawing.Point(150, 79)
        Me.txtName.MaxLength = 0
        Me.txtName.Name = "txtName"
        Me.txtName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtName.Size = New System.Drawing.Size(239, 20)
        Me.txtName.TabIndex = 42
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
        Me.chkAllEmp.Location = New System.Drawing.Point(430, 81)
        Me.chkAllEmp.Name = "chkAllEmp"
        Me.chkAllEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllEmp.Size = New System.Drawing.Size(38, 18)
        Me.chkAllEmp.TabIndex = 41
        Me.chkAllEmp.Text = "All"
        Me.chkAllEmp.UseVisualStyleBackColor = False
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
        Me.txtEmpCode.Location = New System.Drawing.Point(78, 79)
        Me.txtEmpCode.MaxLength = 0
        Me.txtEmpCode.Name = "txtEmpCode"
        Me.txtEmpCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmpCode.Size = New System.Drawing.Size(71, 20)
        Me.txtEmpCode.TabIndex = 38
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(13, 81)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(61, 14)
        Me.Label10.TabIndex = 40
        Me.Label10.Text = "Emp Code :"
        '
        'chkDivision
        '
        Me.chkDivision.AutoSize = True
        Me.chkDivision.BackColor = System.Drawing.SystemColors.Control
        Me.chkDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkDivision.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDivision.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkDivision.Location = New System.Drawing.Point(430, 56)
        Me.chkDivision.Name = "chkDivision"
        Me.chkDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkDivision.Size = New System.Drawing.Size(46, 18)
        Me.chkDivision.TabIndex = 36
        Me.chkDivision.Text = "ALL"
        Me.chkDivision.UseVisualStyleBackColor = False
        '
        'cboDivision
        '
        Me.cboDivision.BackColor = System.Drawing.SystemColors.Window
        Me.cboDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivision.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDivision.Location = New System.Drawing.Point(305, 54)
        Me.cboDivision.Name = "cboDivision"
        Me.cboDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivision.Size = New System.Drawing.Size(117, 22)
        Me.cboDivision.TabIndex = 35
        '
        'cboCostCenter
        '
        Me.cboCostCenter.BackColor = System.Drawing.SystemColors.Window
        Me.cboCostCenter.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCostCenter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCostCenter.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCostCenter.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCostCenter.Location = New System.Drawing.Point(78, 54)
        Me.cboCostCenter.Name = "cboCostCenter"
        Me.cboCostCenter.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCostCenter.Size = New System.Drawing.Size(117, 22)
        Me.cboCostCenter.TabIndex = 29
        '
        'chkCostC
        '
        Me.chkCostC.AutoSize = True
        Me.chkCostC.BackColor = System.Drawing.SystemColors.Control
        Me.chkCostC.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCostC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCostC.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCostC.Location = New System.Drawing.Point(200, 56)
        Me.chkCostC.Name = "chkCostC"
        Me.chkCostC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCostC.Size = New System.Drawing.Size(46, 18)
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
        Me.cboCategory.Location = New System.Drawing.Point(78, 32)
        Me.cboCategory.Name = "cboCategory"
        Me.cboCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCategory.Size = New System.Drawing.Size(346, 22)
        Me.cboCategory.TabIndex = 27
        '
        'chkCategory
        '
        Me.chkCategory.AutoSize = True
        Me.chkCategory.BackColor = System.Drawing.SystemColors.Control
        Me.chkCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCategory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCategory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCategory.Location = New System.Drawing.Point(430, 34)
        Me.chkCategory.Name = "chkCategory"
        Me.chkCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCategory.Size = New System.Drawing.Size(46, 18)
        Me.chkCategory.TabIndex = 26
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
        Me.chkAll.Location = New System.Drawing.Point(430, 14)
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
        Me.cboDept.Location = New System.Drawing.Point(78, 10)
        Me.cboDept.Name = "cboDept"
        Me.cboDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDept.Size = New System.Drawing.Size(346, 22)
        Me.cboDept.TabIndex = 11
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(251, 56)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(50, 14)
        Me.Label7.TabIndex = 37
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
        Me.Label5.Location = New System.Drawing.Point(17, 34)
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
        Me.Label4.Location = New System.Drawing.Point(4, 56)
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
        Me.Frame2.Controls.Add(Me.lblIsArrear)
        Me.Frame2.Controls.Add(Me.lblRunDate)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(889, -2)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(215, 61)
        Me.Frame2.TabIndex = 1
        Me.Frame2.TabStop = False
        '
        'lblYear
        '
        Me.lblYear.CustomFormat = "MMMM,yyyy"
        Me.lblYear.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.lblYear.Location = New System.Drawing.Point(53, 10)
        Me.lblYear.Name = "lblYear"
        Me.lblYear.Size = New System.Drawing.Size(157, 22)
        Me.lblYear.TabIndex = 35
        '
        'cboMonthTerm
        '
        Me.cboMonthTerm.BackColor = System.Drawing.SystemColors.Window
        Me.cboMonthTerm.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboMonthTerm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMonthTerm.Enabled = False
        Me.cboMonthTerm.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboMonthTerm.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboMonthTerm.Location = New System.Drawing.Point(132, 38)
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
        Me.lblMonthTerms.Location = New System.Drawing.Point(58, 40)
        Me.lblMonthTerms.Name = "lblMonthTerms"
        Me.lblMonthTerms.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMonthTerms.Size = New System.Drawing.Size(65, 14)
        Me.lblMonthTerms.TabIndex = 34
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
        Me.lblIsArrear.TabIndex = 16
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
        Me.Frame1.Location = New System.Drawing.Point(0, 107)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(1106, 472)
        Me.Frame1.TabIndex = 0
        Me.Frame1.TabStop = False
        '
        'sprdAttn
        '
        Me.sprdAttn.DataSource = Nothing
        Me.sprdAttn.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sprdAttn.Location = New System.Drawing.Point(0, 13)
        Me.sprdAttn.Name = "sprdAttn"
        Me.sprdAttn.OcxState = CType(resources.GetObject("sprdAttn.OcxState"), System.Windows.Forms.AxHost.State)
        Me.sprdAttn.Size = New System.Drawing.Size(1106, 459)
        Me.sprdAttn.TabIndex = 4
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.cmdExport)
        Me.FraMovement.Controls.Add(Me.Command1)
        Me.FraMovement.Controls.Add(Me.cmdWelFare)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.cmdAccountPost)
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdRefresh)
        Me.FraMovement.Controls.Add(Me.cmdLeave)
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.Report1)
        Me.FraMovement.Controls.Add(Me.cmdPaySlipeMail)
        Me.FraMovement.Controls.Add(Me.lblShowType)
        Me.FraMovement.Controls.Add(Me.lblNetPay)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 569)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(1106, 51)
        Me.FraMovement.TabIndex = 5
        Me.FraMovement.TabStop = False
        '
        'cmdWelFare
        '
        Me.cmdWelFare.BackColor = System.Drawing.SystemColors.Control
        Me.cmdWelFare.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdWelFare.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdWelFare.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdWelFare.Location = New System.Drawing.Point(397, 10)
        Me.cmdWelFare.Name = "cmdWelFare"
        Me.cmdWelFare.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdWelFare.Size = New System.Drawing.Size(86, 38)
        Me.cmdWelFare.TabIndex = 44
        Me.cmdWelFare.Text = "&Welfare File"
        Me.cmdWelFare.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Location = New System.Drawing.Point(568, 10)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(86, 38)
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
        Me.cmdRefresh.Location = New System.Drawing.Point(55, 10)
        Me.cmdRefresh.Name = "cmdRefresh"
        Me.cmdRefresh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdRefresh.Size = New System.Drawing.Size(86, 38)
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
        Me.cmdLeave.Location = New System.Drawing.Point(311, 10)
        Me.cmdLeave.Name = "cmdLeave"
        Me.cmdLeave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdLeave.Size = New System.Drawing.Size(86, 38)
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
        Me.Report1.TabIndex = 46
        '
        'lblShowType
        '
        Me.lblShowType.BackColor = System.Drawing.SystemColors.Control
        Me.lblShowType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblShowType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblShowType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblShowType.Location = New System.Drawing.Point(630, 20)
        Me.lblShowType.Name = "lblShowType"
        Me.lblShowType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblShowType.Size = New System.Drawing.Size(17, 13)
        Me.lblShowType.TabIndex = 43
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
        Me.lblNetPay.TabIndex = 17
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
        Me.Label8.Location = New System.Drawing.Point(2, 82)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(61, 14)
        Me.Label8.TabIndex = 39
        Me.Label8.Text = "Corporate :"
        '
        'cboShowSalary
        '
        Me.cboShowSalary.BackColor = System.Drawing.SystemColors.Window
        Me.cboShowSalary.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboShowSalary.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboShowSalary.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboShowSalary.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboShowSalary.Location = New System.Drawing.Point(747, 8)
        Me.cboShowSalary.Name = "cboShowSalary"
        Me.cboShowSalary.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboShowSalary.Size = New System.Drawing.Size(137, 22)
        Me.cboShowSalary.TabIndex = 43
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(704, 12)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(42, 14)
        Me.Label9.TabIndex = 44
        Me.Label9.Text = "Show :"
        '
        'cmdExport
        '
        Me.cmdExport.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExport.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExport.Enabled = False
        Me.cmdExport.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdExport.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExport.Location = New System.Drawing.Point(740, 10)
        Me.cmdExport.Name = "cmdExport"
        Me.cmdExport.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExport.Size = New System.Drawing.Size(85, 38)
        Me.cmdExport.TabIndex = 47
        Me.cmdExport.Text = "&Export"
        Me.ToolTip1.SetToolTip(Me.cmdExport, "Print PO")
        Me.cmdExport.UseVisualStyleBackColor = False
        '
        'frmSalaryReg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1108, 621)
        Me.Controls.Add(Me.cmdReprocessDays)
        Me.Controls.Add(Me.cboShowSalary)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.chkPerksHead)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.cboCorporate)
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
        Me.Name = "frmSalaryReg"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Salary Register"
        Me.Frame3.ResumeLayout(False)
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
    Public WithEvents cboShowSalary As ComboBox
    Public WithEvents Label9 As Label
    Public WithEvents cmdReprocessDays As Button
    Public WithEvents chkAllEmp As CheckBox
    Public WithEvents cmdsearch As Button
    Public WithEvents txtEmpCode As TextBox
    Public WithEvents Label10 As Label
    Public WithEvents txtName As TextBox
    Public WithEvents cmdExport As Button
#End Region
End Class