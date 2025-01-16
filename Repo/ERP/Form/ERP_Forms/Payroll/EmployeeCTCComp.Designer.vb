Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmEmployeeCTCComp
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
    Public WithEvents chkAll As System.Windows.Forms.CheckBox
    Public WithEvents cboDept As System.Windows.Forms.ComboBox
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents cboDivision As System.Windows.Forms.ComboBox
    Public WithEvents chkAllDiv As System.Windows.Forms.CheckBox
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents txtTo As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtFrom As System.Windows.Forms.MaskedTextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents FraPeriod As System.Windows.Forms.GroupBox
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
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents cboEmpType As System.Windows.Forms.ComboBox
    Public WithEvents Frame5 As System.Windows.Forms.GroupBox
    Public WithEvents cboShow As System.Windows.Forms.ComboBox
    Public WithEvents Frame8 As System.Windows.Forms.GroupBox
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEmployeeCTCComp))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.chkAll = New System.Windows.Forms.CheckBox()
        Me.cboDept = New System.Windows.Forms.ComboBox()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.cboDivision = New System.Windows.Forms.ComboBox()
        Me.chkAllDiv = New System.Windows.Forms.CheckBox()
        Me.FraPeriod = New System.Windows.Forms.GroupBox()
        Me.txtTo = New System.Windows.Forms.MaskedTextBox()
        Me.txtFrom = New System.Windows.Forms.MaskedTextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
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
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me.cboEmpType = New System.Windows.Forms.ComboBox()
        Me.Frame8 = New System.Windows.Forms.GroupBox()
        Me.cboShow = New System.Windows.Forms.ComboBox()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdRefresh = New System.Windows.Forms.Button()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.lblRegType = New System.Windows.Forms.Label()
        Me.Frame4.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.FraPeriod.SuspendLayout()
        Me.Frame7.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me.FraType.SuspendLayout()
        Me.Frame1.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        Me.Frame5.SuspendLayout()
        Me.Frame8.SuspendLayout()
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
        Me.Frame4.Size = New System.Drawing.Size(233, 39)
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
        Me.chkAll.Location = New System.Drawing.Point(184, 18)
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
        Me.cboDept.Size = New System.Drawing.Size(179, 22)
        Me.cboDept.TabIndex = 8
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.cboDivision)
        Me.Frame2.Controls.Add(Me.chkAllDiv)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(82, 38)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(233, 39)
        Me.Frame2.TabIndex = 29
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Division"
        '
        'cboDivision
        '
        Me.cboDivision.BackColor = System.Drawing.SystemColors.Window
        Me.cboDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivision.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDivision.Location = New System.Drawing.Point(4, 14)
        Me.cboDivision.Name = "cboDivision"
        Me.cboDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivision.Size = New System.Drawing.Size(179, 22)
        Me.cboDivision.TabIndex = 31
        '
        'chkAllDiv
        '
        Me.chkAllDiv.AutoSize = True
        Me.chkAllDiv.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllDiv.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllDiv.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllDiv.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllDiv.Location = New System.Drawing.Point(184, 18)
        Me.chkAllDiv.Name = "chkAllDiv"
        Me.chkAllDiv.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllDiv.Size = New System.Drawing.Size(46, 18)
        Me.chkAllDiv.TabIndex = 30
        Me.chkAllDiv.Text = "ALL"
        Me.chkAllDiv.UseVisualStyleBackColor = False
        '
        'FraPeriod
        '
        Me.FraPeriod.BackColor = System.Drawing.SystemColors.Control
        Me.FraPeriod.Controls.Add(Me.txtTo)
        Me.FraPeriod.Controls.Add(Me.txtFrom)
        Me.FraPeriod.Controls.Add(Me.Label2)
        Me.FraPeriod.Controls.Add(Me.Label1)
        Me.FraPeriod.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraPeriod.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraPeriod.Location = New System.Drawing.Point(544, 0)
        Me.FraPeriod.Name = "FraPeriod"
        Me.FraPeriod.Padding = New System.Windows.Forms.Padding(0)
        Me.FraPeriod.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraPeriod.Size = New System.Drawing.Size(127, 77)
        Me.FraPeriod.TabIndex = 24
        Me.FraPeriod.TabStop = False
        Me.FraPeriod.Text = "Comparison Period"
        '
        'txtTo
        '
        Me.txtTo.AllowPromptAsInput = False
        Me.txtTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTo.Location = New System.Drawing.Point(40, 44)
        Me.txtTo.Mask = "##/##/####"
        Me.txtTo.Name = "txtTo"
        Me.txtTo.Size = New System.Drawing.Size(81, 20)
        Me.txtTo.TabIndex = 25
        '
        'txtFrom
        '
        Me.txtFrom.AllowPromptAsInput = False
        Me.txtFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFrom.Location = New System.Drawing.Point(40, 24)
        Me.txtFrom.Mask = "##/##/####"
        Me.txtFrom.Name = "txtFrom"
        Me.txtFrom.Size = New System.Drawing.Size(81, 20)
        Me.txtFrom.TabIndex = 26
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(4, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(24, 14)
        Me.Label2.TabIndex = 28
        Me.Label2.Text = "To :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(4, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(37, 14)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = "From :"
        '
        'Frame7
        '
        Me.Frame7.BackColor = System.Drawing.SystemColors.Control
        Me.Frame7.Controls.Add(Me.chkDesgCategory)
        Me.Frame7.Controls.Add(Me.cboDesgCategory)
        Me.Frame7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame7.Location = New System.Drawing.Point(316, 38)
        Me.Frame7.Name = "Frame7"
        Me.Frame7.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame7.Size = New System.Drawing.Size(227, 39)
        Me.Frame7.TabIndex = 20
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
        Me.chkDesgCategory.Location = New System.Drawing.Point(178, 18)
        Me.chkDesgCategory.Name = "chkDesgCategory"
        Me.chkDesgCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkDesgCategory.Size = New System.Drawing.Size(46, 18)
        Me.chkDesgCategory.TabIndex = 22
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
        Me.cboDesgCategory.Location = New System.Drawing.Point(4, 14)
        Me.cboDesgCategory.Name = "cboDesgCategory"
        Me.cboDesgCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDesgCategory.Size = New System.Drawing.Size(173, 22)
        Me.cboDesgCategory.TabIndex = 21
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
        Me.Frame3.Size = New System.Drawing.Size(81, 77)
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
        Me.optDept.Location = New System.Drawing.Point(6, 52)
        Me.optDept.Name = "optDept"
        Me.optDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optDept.Size = New System.Drawing.Size(47, 18)
        Me.optDept.TabIndex = 19
        Me.optDept.TabStop = True
        Me.optDept.Text = "Dept"
        Me.optDept.UseVisualStyleBackColor = False
        '
        'optCardNo
        '
        Me.optCardNo.AutoSize = True
        Me.optCardNo.BackColor = System.Drawing.SystemColors.Control
        Me.optCardNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.optCardNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCardNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCardNo.Location = New System.Drawing.Point(6, 36)
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
        Me.OptName.Location = New System.Drawing.Point(6, 20)
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
        Me.Frame6.Location = New System.Drawing.Point(316, 0)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(227, 39)
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
        Me.cboCategory.Location = New System.Drawing.Point(6, 14)
        Me.cboCategory.Name = "cboCategory"
        Me.cboCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCategory.Size = New System.Drawing.Size(171, 22)
        Me.cboCategory.TabIndex = 14
        '
        'chkCategory
        '
        Me.chkCategory.AutoSize = True
        Me.chkCategory.BackColor = System.Drawing.SystemColors.Control
        Me.chkCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCategory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCategory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCategory.Location = New System.Drawing.Point(180, 18)
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
        Me.FraType.Location = New System.Drawing.Point(672, 0)
        Me.FraType.Name = "FraType"
        Me.FraType.Padding = New System.Windows.Forms.Padding(0)
        Me.FraType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraType.Size = New System.Drawing.Size(77, 77)
        Me.FraType.TabIndex = 16
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
        Me.optExisting.Location = New System.Drawing.Point(4, 48)
        Me.optExisting.Name = "optExisting"
        Me.optExisting.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optExisting.Size = New System.Drawing.Size(62, 18)
        Me.optExisting.TabIndex = 18
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
        Me.optAllEmp.Location = New System.Drawing.Point(4, 26)
        Me.optAllEmp.Name = "optAllEmp"
        Me.optAllEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optAllEmp.Size = New System.Drawing.Size(45, 18)
        Me.optAllEmp.TabIndex = 17
        Me.optAllEmp.TabStop = True
        Me.optAllEmp.Text = "ALL"
        Me.optAllEmp.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.SprdView)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 72)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(749, 337)
        Me.Frame1.TabIndex = 0
        Me.Frame1.TabStop = False
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(2, 8)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(743, 325)
        Me.SprdView.TabIndex = 15
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.Frame5)
        Me.FraMovement.Controls.Add(Me.Frame8)
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
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me.cboEmpType)
        Me.Frame5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(350, 4)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(175, 45)
        Me.Frame5.TabIndex = 34
        Me.Frame5.TabStop = False
        Me.Frame5.Text = "Emp Type"
        '
        'cboEmpType
        '
        Me.cboEmpType.BackColor = System.Drawing.SystemColors.Window
        Me.cboEmpType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboEmpType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEmpType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboEmpType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboEmpType.Location = New System.Drawing.Point(4, 16)
        Me.cboEmpType.Name = "cboEmpType"
        Me.cboEmpType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboEmpType.Size = New System.Drawing.Size(167, 22)
        Me.cboEmpType.TabIndex = 35
        '
        'Frame8
        '
        Me.Frame8.BackColor = System.Drawing.SystemColors.Control
        Me.Frame8.Controls.Add(Me.cboShow)
        Me.Frame8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame8.Location = New System.Drawing.Point(208, 4)
        Me.Frame8.Name = "Frame8"
        Me.Frame8.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame8.Size = New System.Drawing.Size(141, 45)
        Me.Frame8.TabIndex = 32
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
        Me.cboShow.Location = New System.Drawing.Point(4, 16)
        Me.cboShow.Name = "cboShow"
        Me.cboShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboShow.Size = New System.Drawing.Size(135, 22)
        Me.cboShow.TabIndex = 33
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
        Me.Report1.TabIndex = 35
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
        Me.lblRegType.TabIndex = 23
        Me.lblRegType.Text = "lblRegType"
        '
        'frmEmployeeCTCComp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(749, 456)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.FraPeriod)
        Me.Controls.Add(Me.Frame7)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.Frame6)
        Me.Controls.Add(Me.FraType)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.FraMovement)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "frmEmployeeCTCComp"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Employee CTC Comparison"
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.FraPeriod.ResumeLayout(False)
        Me.FraPeriod.PerformLayout()
        Me.Frame7.ResumeLayout(False)
        Me.Frame7.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me.FraType.ResumeLayout(False)
        Me.FraType.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        Me.Frame5.ResumeLayout(False)
        Me.Frame8.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
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