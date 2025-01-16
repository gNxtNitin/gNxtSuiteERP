Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmLeaveSummary
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
        'Me.MDIParent = Payroll.Master
        'Payroll.Master.Show
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
	Public WithEvents _OptShow_2 As System.Windows.Forms.RadioButton
	Public WithEvents _OptShow_1 As System.Windows.Forms.RadioButton
	Public WithEvents _OptShow_0 As System.Windows.Forms.RadioButton
	Public WithEvents Frame5 As System.Windows.Forms.GroupBox
	Public WithEvents cboCategory As System.Windows.Forms.ComboBox
	Public WithEvents chkCategory As System.Windows.Forms.CheckBox
	Public WithEvents Frame6 As System.Windows.Forms.GroupBox
	Public WithEvents chkAll As System.Windows.Forms.CheckBox
	Public WithEvents cboDept As System.Windows.Forms.ComboBox
	Public WithEvents Frame4 As System.Windows.Forms.GroupBox
	Public WithEvents optCardNo As System.Windows.Forms.RadioButton
	Public WithEvents OptName As System.Windows.Forms.RadioButton
	Public WithEvents Frame3 As System.Windows.Forms.GroupBox
	Public WithEvents txtTo As System.Windows.Forms.MaskedTextBox
	Public WithEvents txtFrom As System.Windows.Forms.MaskedTextBox
	Public WithEvents lblTo As System.Windows.Forms.Label
	Public WithEvents lblFrom As System.Windows.Forms.Label
	Public WithEvents Frame2 As System.Windows.Forms.GroupBox
	Public WithEvents sprdLeave As AxFPSpreadADO.AxfpSpread
	Public WithEvents Frame1 As System.Windows.Forms.GroupBox
	Public WithEvents cmdPrint As System.Windows.Forms.Button
	Public WithEvents CmdPreview As System.Windows.Forms.Button
	Public WithEvents cmdRefresh As System.Windows.Forms.Button
	Public WithEvents CmdClose As System.Windows.Forms.Button
	Public WithEvents Report1 As AxCrystal.AxCrystalReport
	Public WithEvents FraMovement As System.Windows.Forms.GroupBox
	Public WithEvents OptShow As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLeaveSummary))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me._OptShow_2 = New System.Windows.Forms.RadioButton()
        Me._OptShow_1 = New System.Windows.Forms.RadioButton()
        Me._OptShow_0 = New System.Windows.Forms.RadioButton()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.cboCategory = New System.Windows.Forms.ComboBox()
        Me.chkCategory = New System.Windows.Forms.CheckBox()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.chkAll = New System.Windows.Forms.CheckBox()
        Me.cboDept = New System.Windows.Forms.ComboBox()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.optCardNo = New System.Windows.Forms.RadioButton()
        Me.OptName = New System.Windows.Forms.RadioButton()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.txtTo = New System.Windows.Forms.MaskedTextBox()
        Me.txtFrom = New System.Windows.Forms.MaskedTextBox()
        Me.lblTo = New System.Windows.Forms.Label()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.sprdLeave = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdRefresh = New System.Windows.Forms.Button()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.OptShow = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optDept = New System.Windows.Forms.RadioButton()
        Me.Frame5.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me.Frame4.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame1.SuspendLayout()
        CType(Me.sprdLeave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptShow, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.CmdPreview.TabIndex = 12
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
        Me.CmdClose.TabIndex = 4
        Me.CmdClose.Text = "&Close"
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me._OptShow_2)
        Me.Frame5.Controls.Add(Me._OptShow_1)
        Me.Frame5.Controls.Add(Me._OptShow_0)
        Me.Frame5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(787, 0)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(109, 82)
        Me.Frame5.TabIndex = 17
        Me.Frame5.TabStop = False
        Me.Frame5.Text = "Show"
        '
        '_OptShow_2
        '
        Me._OptShow_2.AutoSize = True
        Me._OptShow_2.BackColor = System.Drawing.SystemColors.Control
        Me._OptShow_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptShow_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptShow_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptShow.SetIndex(Me._OptShow_2, CType(2, Short))
        Me._OptShow_2.Location = New System.Drawing.Point(4, 48)
        Me._OptShow_2.Name = "_OptShow_2"
        Me._OptShow_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptShow_2.Size = New System.Drawing.Size(74, 18)
        Me._OptShow_2.TabIndex = 20
        Me._OptShow_2.TabStop = True
        Me._OptShow_2.Text = "Only Left"
        Me._OptShow_2.UseVisualStyleBackColor = False
        '
        '_OptShow_1
        '
        Me._OptShow_1.AutoSize = True
        Me._OptShow_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptShow_1.Checked = True
        Me._OptShow_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptShow_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptShow_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptShow.SetIndex(Me._OptShow_1, CType(1, Short))
        Me._OptShow_1.Location = New System.Drawing.Point(4, 30)
        Me._OptShow_1.Name = "_OptShow_1"
        Me._OptShow_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptShow_1.Size = New System.Drawing.Size(96, 18)
        Me._OptShow_1.TabIndex = 19
        Me._OptShow_1.TabStop = True
        Me._OptShow_1.Text = "Only Present"
        Me._OptShow_1.UseVisualStyleBackColor = False
        '
        '_OptShow_0
        '
        Me._OptShow_0.AutoSize = True
        Me._OptShow_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptShow_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptShow_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptShow_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptShow.SetIndex(Me._OptShow_0, CType(0, Short))
        Me._OptShow_0.Location = New System.Drawing.Point(4, 14)
        Me._OptShow_0.Name = "_OptShow_0"
        Me._OptShow_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptShow_0.Size = New System.Drawing.Size(39, 18)
        Me._OptShow_0.TabIndex = 18
        Me._OptShow_0.TabStop = True
        Me._OptShow_0.Text = "All"
        Me._OptShow_0.UseVisualStyleBackColor = False
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.cboCategory)
        Me.Frame6.Controls.Add(Me.chkCategory)
        Me.Frame6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(555, 0)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(229, 82)
        Me.Frame6.TabIndex = 14
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
        Me.cboCategory.Location = New System.Drawing.Point(2, 26)
        Me.cboCategory.Name = "cboCategory"
        Me.cboCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCategory.Size = New System.Drawing.Size(224, 22)
        Me.cboCategory.TabIndex = 16
        '
        'chkCategory
        '
        Me.chkCategory.AutoSize = True
        Me.chkCategory.BackColor = System.Drawing.SystemColors.Control
        Me.chkCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCategory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCategory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCategory.Location = New System.Drawing.Point(3, 55)
        Me.chkCategory.Name = "chkCategory"
        Me.chkCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCategory.Size = New System.Drawing.Size(48, 18)
        Me.chkCategory.TabIndex = 15
        Me.chkCategory.Text = "ALL"
        Me.chkCategory.UseVisualStyleBackColor = False
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.chkAll)
        Me.Frame4.Controls.Add(Me.cboDept)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(273, 0)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(279, 82)
        Me.Frame4.TabIndex = 7
        Me.Frame4.TabStop = False
        Me.Frame4.Text = "Department"
        '
        'chkAll
        '
        Me.chkAll.AutoSize = True
        Me.chkAll.BackColor = System.Drawing.SystemColors.Control
        Me.chkAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAll.Location = New System.Drawing.Point(6, 54)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAll.Size = New System.Drawing.Size(48, 18)
        Me.chkAll.TabIndex = 11
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
        Me.cboDept.Location = New System.Drawing.Point(6, 26)
        Me.cboDept.Name = "cboDept"
        Me.cboDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDept.Size = New System.Drawing.Size(270, 22)
        Me.cboDept.TabIndex = 10
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.optDept)
        Me.Frame3.Controls.Add(Me.optCardNo)
        Me.Frame3.Controls.Add(Me.OptName)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(151, -1)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(119, 83)
        Me.Frame3.TabIndex = 6
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "Order By"
        '
        'optCardNo
        '
        Me.optCardNo.AutoSize = True
        Me.optCardNo.BackColor = System.Drawing.SystemColors.Control
        Me.optCardNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.optCardNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCardNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCardNo.Location = New System.Drawing.Point(9, 38)
        Me.optCardNo.Name = "optCardNo"
        Me.optCardNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optCardNo.Size = New System.Drawing.Size(68, 18)
        Me.optCardNo.TabIndex = 9
        Me.optCardNo.Text = "Card No"
        Me.optCardNo.UseVisualStyleBackColor = False
        '
        'OptName
        '
        Me.OptName.AutoSize = True
        Me.OptName.BackColor = System.Drawing.SystemColors.Control
        Me.OptName.Checked = True
        Me.OptName.Cursor = System.Windows.Forms.Cursors.Default
        Me.OptName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptName.Location = New System.Drawing.Point(9, 15)
        Me.OptName.Name = "OptName"
        Me.OptName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptName.Size = New System.Drawing.Size(56, 18)
        Me.OptName.TabIndex = 8
        Me.OptName.TabStop = True
        Me.OptName.Text = "Name"
        Me.OptName.UseVisualStyleBackColor = False
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.txtTo)
        Me.Frame2.Controls.Add(Me.txtFrom)
        Me.Frame2.Controls.Add(Me.lblTo)
        Me.Frame2.Controls.Add(Me.lblFrom)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, -2)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(149, 84)
        Me.Frame2.TabIndex = 1
        Me.Frame2.TabStop = False
        '
        'txtTo
        '
        Me.txtTo.AllowPromptAsInput = False
        Me.txtTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTo.Location = New System.Drawing.Point(58, 40)
        Me.txtTo.Mask = "##/##/####"
        Me.txtTo.Name = "txtTo"
        Me.txtTo.Size = New System.Drawing.Size(81, 20)
        Me.txtTo.TabIndex = 21
        '
        'txtFrom
        '
        Me.txtFrom.AllowPromptAsInput = False
        Me.txtFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFrom.Location = New System.Drawing.Point(58, 16)
        Me.txtFrom.Mask = "##/##/####"
        Me.txtFrom.Name = "txtFrom"
        Me.txtFrom.Size = New System.Drawing.Size(81, 20)
        Me.txtFrom.TabIndex = 22
        '
        'lblTo
        '
        Me.lblTo.AutoSize = True
        Me.lblTo.BackColor = System.Drawing.SystemColors.Control
        Me.lblTo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTo.Location = New System.Drawing.Point(22, 46)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTo.Size = New System.Drawing.Size(26, 14)
        Me.lblTo.TabIndex = 24
        Me.lblTo.Text = "To :"
        '
        'lblFrom
        '
        Me.lblFrom.AutoSize = True
        Me.lblFrom.BackColor = System.Drawing.SystemColors.Control
        Me.lblFrom.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblFrom.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFrom.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFrom.Location = New System.Drawing.Point(22, 20)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblFrom.Size = New System.Drawing.Size(42, 14)
        Me.lblFrom.TabIndex = 23
        Me.lblFrom.Text = "From :"
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.sprdLeave)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 79)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(896, 486)
        Me.Frame1.TabIndex = 0
        Me.Frame1.TabStop = False
        '
        'sprdLeave
        '
        Me.sprdLeave.DataSource = Nothing
        Me.sprdLeave.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sprdLeave.Location = New System.Drawing.Point(0, 13)
        Me.sprdLeave.Name = "sprdLeave"
        Me.sprdLeave.OcxState = CType(resources.GetObject("sprdLeave.OcxState"), System.Windows.Forms.AxHost.State)
        Me.sprdLeave.Size = New System.Drawing.Size(896, 473)
        Me.sprdLeave.TabIndex = 2
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdRefresh)
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.Report1)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(2, 559)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(749, 51)
        Me.FraMovement.TabIndex = 3
        Me.FraMovement.TabStop = False
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
        Me.cmdRefresh.Location = New System.Drawing.Point(584, 12)
        Me.cmdRefresh.Name = "cmdRefresh"
        Me.cmdRefresh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdRefresh.Size = New System.Drawing.Size(80, 34)
        Me.cmdRefresh.TabIndex = 5
        Me.cmdRefresh.Text = "&Refresh"
        Me.cmdRefresh.UseVisualStyleBackColor = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(188, 14)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 14
        '
        'optDept
        '
        Me.optDept.AutoSize = True
        Me.optDept.BackColor = System.Drawing.SystemColors.Control
        Me.optDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.optDept.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optDept.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDept.Location = New System.Drawing.Point(9, 61)
        Me.optDept.Name = "optDept"
        Me.optDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optDept.Size = New System.Drawing.Size(90, 18)
        Me.optDept.TabIndex = 10
        Me.optDept.Text = "Department"
        Me.optDept.UseVisualStyleBackColor = False
        '
        'frmLeaveSummary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(900, 611)
        Me.Controls.Add(Me.Frame5)
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
        Me.Name = "frmLeaveSummary"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Leave Summary (As on Date)"
        Me.Frame5.ResumeLayout(False)
        Me.Frame5.PerformLayout()
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        CType(Me.sprdLeave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptShow, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Public WithEvents optDept As RadioButton
#End Region
End Class