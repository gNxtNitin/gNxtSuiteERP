Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmTrfOpStock
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
        'Me.MDIParent = Admin.Master
        'Admin.Master.Show
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
	Public WithEvents txtCategory As System.Windows.Forms.TextBox
	Public WithEvents cmdsearchCategory As System.Windows.Forms.Button
	Public WithEvents chkAllCategory As System.Windows.Forms.CheckBox
	Public WithEvents Frame5 As System.Windows.Forms.GroupBox
	Public WithEvents _optExportItem_2 As System.Windows.Forms.RadioButton
	Public WithEvents _optExportItem_1 As System.Windows.Forms.RadioButton
	Public WithEvents _optExportItem_0 As System.Windows.Forms.RadioButton
	Public WithEvents cboDept As System.Windows.Forms.ComboBox
	Public WithEvents cboStockID As System.Windows.Forms.ComboBox
	Public WithEvents Label4 As System.Windows.Forms.Label
	Public WithEvents Label3 As System.Windows.Forms.Label
	Public WithEvents Frame1 As System.Windows.Forms.GroupBox
	Public WithEvents _TxtDisplayTransfer_1 As System.Windows.Forms.TextBox
	Public WithEvents OptParticularItem As System.Windows.Forms.RadioButton
	Public WithEvents OptAllItem As System.Windows.Forms.RadioButton
	Public WithEvents txtItemCode As System.Windows.Forms.TextBox
	Public WithEvents cmdSearchCode As System.Windows.Forms.Button
	Public WithEvents txtItemName As System.Windows.Forms.TextBox
	Public WithEvents cmdSearchName As System.Windows.Forms.Button
	Public WithEvents FraItem As System.Windows.Forms.GroupBox
	Public WithEvents CboFYearFrom As System.Windows.Forms.ComboBox
	Public WithEvents CboFYearTo As System.Windows.Forms.ComboBox
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents FraFYear As System.Windows.Forms.GroupBox
	Public WithEvents _TxtDisplayTransfer_0 As System.Windows.Forms.TextBox
	Public WithEvents cmdClose As System.Windows.Forms.Button
	Public WithEvents cmdStart As System.Windows.Forms.Button
	Public WithEvents Frame2 As System.Windows.Forms.GroupBox
	Public WithEvents _optStatus_1 As System.Windows.Forms.RadioButton
	Public WithEvents _optStatus_0 As System.Windows.Forms.RadioButton
	Public WithEvents Frame3 As System.Windows.Forms.GroupBox
	Public WithEvents TxtDisplayTransfer As Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray
	Public WithEvents optExportItem As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
	Public WithEvents optStatus As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTrfOpStock))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtCategory = New System.Windows.Forms.TextBox()
        Me.cmdsearchCategory = New System.Windows.Forms.Button()
        Me.cmdSearchCode = New System.Windows.Forms.Button()
        Me.cmdSearchName = New System.Windows.Forms.Button()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me.chkAllCategory = New System.Windows.Forms.CheckBox()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me._optExportItem_2 = New System.Windows.Forms.RadioButton()
        Me._optExportItem_1 = New System.Windows.Forms.RadioButton()
        Me._optExportItem_0 = New System.Windows.Forms.RadioButton()
        Me.cboDept = New System.Windows.Forms.ComboBox()
        Me.cboStockID = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me._TxtDisplayTransfer_1 = New System.Windows.Forms.TextBox()
        Me.FraItem = New System.Windows.Forms.GroupBox()
        Me.OptParticularItem = New System.Windows.Forms.RadioButton()
        Me.OptAllItem = New System.Windows.Forms.RadioButton()
        Me.txtItemCode = New System.Windows.Forms.TextBox()
        Me.txtItemName = New System.Windows.Forms.TextBox()
        Me.FraFYear = New System.Windows.Forms.GroupBox()
        Me.CboFYearFrom = New System.Windows.Forms.ComboBox()
        Me.CboFYearTo = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me._TxtDisplayTransfer_0 = New System.Windows.Forms.TextBox()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdStart = New System.Windows.Forms.Button()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me._optStatus_1 = New System.Windows.Forms.RadioButton()
        Me._optStatus_0 = New System.Windows.Forms.RadioButton()
        Me.TxtDisplayTransfer = New Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray(Me.components)
        Me.optExportItem = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optStatus = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.Frame5.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.FraItem.SuspendLayout()
        Me.FraFYear.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame3.SuspendLayout()
        CType(Me.TxtDisplayTransfer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optExportItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtCategory
        '
        Me.txtCategory.AcceptsReturn = True
        Me.txtCategory.BackColor = System.Drawing.SystemColors.Window
        Me.txtCategory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCategory.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCategory.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCategory.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCategory.Location = New System.Drawing.Point(56, 10)
        Me.txtCategory.MaxLength = 0
        Me.txtCategory.Name = "txtCategory"
        Me.txtCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCategory.Size = New System.Drawing.Size(197, 19)
        Me.txtCategory.TabIndex = 28
        Me.ToolTip1.SetToolTip(Me.txtCategory, "Press F1 For Help")
        '
        'cmdsearchCategory
        '
        Me.cmdsearchCategory.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearchCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearchCategory.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearchCategory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearchCategory.Image = CType(resources.GetObject("cmdsearchCategory.Image"), System.Drawing.Image)
        Me.cmdsearchCategory.Location = New System.Drawing.Point(256, 10)
        Me.cmdsearchCategory.Name = "cmdsearchCategory"
        Me.cmdsearchCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearchCategory.Size = New System.Drawing.Size(29, 19)
        Me.cmdsearchCategory.TabIndex = 27
        Me.cmdsearchCategory.TabStop = False
        Me.cmdsearchCategory.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearchCategory, "Search")
        Me.cmdsearchCategory.UseVisualStyleBackColor = False
        '
        'cmdSearchCode
        '
        Me.cmdSearchCode.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchCode.Image = CType(resources.GetObject("cmdSearchCode.Image"), System.Drawing.Image)
        Me.cmdSearchCode.Location = New System.Drawing.Point(310, 10)
        Me.cmdSearchCode.Name = "cmdSearchCode"
        Me.cmdSearchCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchCode.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchCode.TabIndex = 12
        Me.cmdSearchCode.TabStop = False
        Me.cmdSearchCode.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchCode, "Search")
        Me.cmdSearchCode.UseVisualStyleBackColor = False
        '
        'cmdSearchName
        '
        Me.cmdSearchName.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchName.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchName.Image = CType(resources.GetObject("cmdSearchName.Image"), System.Drawing.Image)
        Me.cmdSearchName.Location = New System.Drawing.Point(310, 30)
        Me.cmdSearchName.Name = "cmdSearchName"
        Me.cmdSearchName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchName.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchName.TabIndex = 10
        Me.cmdSearchName.TabStop = False
        Me.cmdSearchName.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchName, "Search")
        Me.cmdSearchName.UseVisualStyleBackColor = False
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me.txtCategory)
        Me.Frame5.Controls.Add(Me.cmdsearchCategory)
        Me.Frame5.Controls.Add(Me.chkAllCategory)
        Me.Frame5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(0, 114)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(339, 33)
        Me.Frame5.TabIndex = 25
        Me.Frame5.TabStop = False
        Me.Frame5.Text = "Category"
        '
        'chkAllCategory
        '
        Me.chkAllCategory.AutoSize = True
        Me.chkAllCategory.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllCategory.Checked = True
        Me.chkAllCategory.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllCategory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllCategory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllCategory.Location = New System.Drawing.Point(290, 12)
        Me.chkAllCategory.Name = "chkAllCategory"
        Me.chkAllCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllCategory.Size = New System.Drawing.Size(48, 18)
        Me.chkAllCategory.TabIndex = 26
        Me.chkAllCategory.Text = "ALL"
        Me.chkAllCategory.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me._optExportItem_2)
        Me.Frame1.Controls.Add(Me._optExportItem_1)
        Me.Frame1.Controls.Add(Me._optExportItem_0)
        Me.Frame1.Controls.Add(Me.cboDept)
        Me.Frame1.Controls.Add(Me.cboStockID)
        Me.Frame1.Controls.Add(Me.Label4)
        Me.Frame1.Controls.Add(Me.Label3)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Frame1.Location = New System.Drawing.Point(0, 58)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(339, 55)
        Me.Frame1.TabIndex = 17
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Stock ID"
        '
        '_optExportItem_2
        '
        Me._optExportItem_2.AutoSize = True
        Me._optExportItem_2.BackColor = System.Drawing.SystemColors.Control
        Me._optExportItem_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optExportItem_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optExportItem_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optExportItem.SetIndex(Me._optExportItem_2, CType(2, Short))
        Me._optExportItem_2.Location = New System.Drawing.Point(250, 38)
        Me._optExportItem_2.Name = "_optExportItem_2"
        Me._optExportItem_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optExportItem_2.Size = New System.Drawing.Size(91, 18)
        Me._optExportItem_2.TabIndex = 24
        Me._optExportItem_2.TabStop = True
        Me._optExportItem_2.Text = "None Export"
        Me._optExportItem_2.UseVisualStyleBackColor = False
        Me._optExportItem_2.Visible = False
        '
        '_optExportItem_1
        '
        Me._optExportItem_1.AutoSize = True
        Me._optExportItem_1.BackColor = System.Drawing.SystemColors.Control
        Me._optExportItem_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optExportItem_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optExportItem_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optExportItem.SetIndex(Me._optExportItem_1, CType(1, Short))
        Me._optExportItem_1.Location = New System.Drawing.Point(250, 24)
        Me._optExportItem_1.Name = "_optExportItem_1"
        Me._optExportItem_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optExportItem_1.Size = New System.Drawing.Size(88, 18)
        Me._optExportItem_1.TabIndex = 23
        Me._optExportItem_1.TabStop = True
        Me._optExportItem_1.Text = "Export Item"
        Me._optExportItem_1.UseVisualStyleBackColor = False
        Me._optExportItem_1.Visible = False
        '
        '_optExportItem_0
        '
        Me._optExportItem_0.AutoSize = True
        Me._optExportItem_0.BackColor = System.Drawing.SystemColors.Control
        Me._optExportItem_0.Checked = True
        Me._optExportItem_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optExportItem_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optExportItem_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optExportItem.SetIndex(Me._optExportItem_0, CType(0, Short))
        Me._optExportItem_0.Location = New System.Drawing.Point(250, 10)
        Me._optExportItem_0.Name = "_optExportItem_0"
        Me._optExportItem_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optExportItem_0.Size = New System.Drawing.Size(67, 18)
        Me._optExportItem_0.TabIndex = 22
        Me._optExportItem_0.TabStop = True
        Me._optExportItem_0.Text = "All Item"
        Me._optExportItem_0.UseVisualStyleBackColor = False
        Me._optExportItem_0.Visible = False
        '
        'cboDept
        '
        Me.cboDept.BackColor = System.Drawing.SystemColors.Window
        Me.cboDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDept.Enabled = False
        Me.cboDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDept.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDept.Location = New System.Drawing.Point(100, 32)
        Me.cboDept.Name = "cboDept"
        Me.cboDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDept.Size = New System.Drawing.Size(141, 22)
        Me.cboDept.TabIndex = 20
        '
        'cboStockID
        '
        Me.cboStockID.BackColor = System.Drawing.SystemColors.Window
        Me.cboStockID.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboStockID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStockID.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboStockID.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboStockID.Location = New System.Drawing.Point(100, 10)
        Me.cboStockID.Name = "cboStockID"
        Me.cboStockID.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboStockID.Size = New System.Drawing.Size(141, 22)
        Me.cboStockID.TabIndex = 18
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(2, 36)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(93, 17)
        Me.Label4.TabIndex = 21
        Me.Label4.Text = "Department :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(4, 14)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(93, 17)
        Me.Label3.TabIndex = 19
        Me.Label3.Text = "Stock ID :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_TxtDisplayTransfer_1
        '
        Me._TxtDisplayTransfer_1.AcceptsReturn = True
        Me._TxtDisplayTransfer_1.BackColor = System.Drawing.Color.Black
        Me._TxtDisplayTransfer_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me._TxtDisplayTransfer_1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._TxtDisplayTransfer_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._TxtDisplayTransfer_1.ForeColor = System.Drawing.SystemColors.Window
        Me.TxtDisplayTransfer.SetIndex(Me._TxtDisplayTransfer_1, CType(1, Short))
        Me._TxtDisplayTransfer_1.Location = New System.Drawing.Point(2, 288)
        Me._TxtDisplayTransfer_1.MaxLength = 0
        Me._TxtDisplayTransfer_1.Multiline = True
        Me._TxtDisplayTransfer_1.Name = "_TxtDisplayTransfer_1"
        Me._TxtDisplayTransfer_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._TxtDisplayTransfer_1.Size = New System.Drawing.Size(335, 115)
        Me._TxtDisplayTransfer_1.TabIndex = 16
        '
        'FraItem
        '
        Me.FraItem.BackColor = System.Drawing.SystemColors.Control
        Me.FraItem.Controls.Add(Me.OptParticularItem)
        Me.FraItem.Controls.Add(Me.OptAllItem)
        Me.FraItem.Controls.Add(Me.txtItemCode)
        Me.FraItem.Controls.Add(Me.cmdSearchCode)
        Me.FraItem.Controls.Add(Me.txtItemName)
        Me.FraItem.Controls.Add(Me.cmdSearchName)
        Me.FraItem.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.FraItem.Location = New System.Drawing.Point(0, 146)
        Me.FraItem.Name = "FraItem"
        Me.FraItem.Padding = New System.Windows.Forms.Padding(0)
        Me.FraItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraItem.Size = New System.Drawing.Size(339, 51)
        Me.FraItem.TabIndex = 9
        Me.FraItem.TabStop = False
        Me.FraItem.Text = "Item"
        '
        'OptParticularItem
        '
        Me.OptParticularItem.AutoSize = True
        Me.OptParticularItem.BackColor = System.Drawing.SystemColors.Control
        Me.OptParticularItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.OptParticularItem.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptParticularItem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptParticularItem.Location = New System.Drawing.Point(30, 14)
        Me.OptParticularItem.Name = "OptParticularItem"
        Me.OptParticularItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptParticularItem.Size = New System.Drawing.Size(77, 18)
        Me.OptParticularItem.TabIndex = 15
        Me.OptParticularItem.TabStop = True
        Me.OptParticularItem.Text = "Particular"
        Me.OptParticularItem.UseVisualStyleBackColor = False
        '
        'OptAllItem
        '
        Me.OptAllItem.AutoSize = True
        Me.OptAllItem.BackColor = System.Drawing.SystemColors.Control
        Me.OptAllItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.OptAllItem.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptAllItem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptAllItem.Location = New System.Drawing.Point(30, 34)
        Me.OptAllItem.Name = "OptAllItem"
        Me.OptAllItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptAllItem.Size = New System.Drawing.Size(39, 18)
        Me.OptAllItem.TabIndex = 14
        Me.OptAllItem.TabStop = True
        Me.OptAllItem.Text = "All"
        Me.OptAllItem.UseVisualStyleBackColor = False
        '
        'txtItemCode
        '
        Me.txtItemCode.AcceptsReturn = True
        Me.txtItemCode.BackColor = System.Drawing.Color.White
        Me.txtItemCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItemCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemCode.ForeColor = System.Drawing.Color.Blue
        Me.txtItemCode.Location = New System.Drawing.Point(116, 10)
        Me.txtItemCode.MaxLength = 0
        Me.txtItemCode.Name = "txtItemCode"
        Me.txtItemCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemCode.Size = New System.Drawing.Size(191, 19)
        Me.txtItemCode.TabIndex = 13
        '
        'txtItemName
        '
        Me.txtItemName.AcceptsReturn = True
        Me.txtItemName.BackColor = System.Drawing.Color.White
        Me.txtItemName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItemName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemName.ForeColor = System.Drawing.Color.Blue
        Me.txtItemName.Location = New System.Drawing.Point(116, 30)
        Me.txtItemName.MaxLength = 0
        Me.txtItemName.Name = "txtItemName"
        Me.txtItemName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemName.Size = New System.Drawing.Size(191, 19)
        Me.txtItemName.TabIndex = 11
        '
        'FraFYear
        '
        Me.FraFYear.BackColor = System.Drawing.SystemColors.Control
        Me.FraFYear.Controls.Add(Me.CboFYearFrom)
        Me.FraFYear.Controls.Add(Me.CboFYearTo)
        Me.FraFYear.Controls.Add(Me.Label1)
        Me.FraFYear.Controls.Add(Me.Label2)
        Me.FraFYear.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraFYear.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.FraFYear.Location = New System.Drawing.Point(0, 0)
        Me.FraFYear.Name = "FraFYear"
        Me.FraFYear.Padding = New System.Windows.Forms.Padding(0)
        Me.FraFYear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraFYear.Size = New System.Drawing.Size(339, 57)
        Me.FraFYear.TabIndex = 4
        Me.FraFYear.TabStop = False
        Me.FraFYear.Text = "Transfer Stock"
        '
        'CboFYearFrom
        '
        Me.CboFYearFrom.BackColor = System.Drawing.SystemColors.Window
        Me.CboFYearFrom.Cursor = System.Windows.Forms.Cursors.Default
        Me.CboFYearFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboFYearFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboFYearFrom.ForeColor = System.Drawing.SystemColors.WindowText
        Me.CboFYearFrom.Location = New System.Drawing.Point(138, 10)
        Me.CboFYearFrom.Name = "CboFYearFrom"
        Me.CboFYearFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CboFYearFrom.Size = New System.Drawing.Size(199, 22)
        Me.CboFYearFrom.TabIndex = 6
        '
        'CboFYearTo
        '
        Me.CboFYearTo.BackColor = System.Drawing.SystemColors.Window
        Me.CboFYearTo.Cursor = System.Windows.Forms.Cursors.Default
        Me.CboFYearTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboFYearTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboFYearTo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.CboFYearTo.Location = New System.Drawing.Point(138, 32)
        Me.CboFYearTo.Name = "CboFYearTo"
        Me.CboFYearTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CboFYearTo.Size = New System.Drawing.Size(199, 22)
        Me.CboFYearTo.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(42, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(93, 17)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "FYear From :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(42, 36)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(93, 17)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "FYear To :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_TxtDisplayTransfer_0
        '
        Me._TxtDisplayTransfer_0.AcceptsReturn = True
        Me._TxtDisplayTransfer_0.BackColor = System.Drawing.Color.Black
        Me._TxtDisplayTransfer_0.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._TxtDisplayTransfer_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._TxtDisplayTransfer_0.ForeColor = System.Drawing.SystemColors.Window
        Me.TxtDisplayTransfer.SetIndex(Me._TxtDisplayTransfer_0, CType(0, Short))
        Me._TxtDisplayTransfer_0.Location = New System.Drawing.Point(0, 220)
        Me._TxtDisplayTransfer_0.MaxLength = 0
        Me._TxtDisplayTransfer_0.Multiline = True
        Me._TxtDisplayTransfer_0.Name = "_TxtDisplayTransfer_0"
        Me._TxtDisplayTransfer_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._TxtDisplayTransfer_0.Size = New System.Drawing.Size(339, 185)
        Me._TxtDisplayTransfer_0.TabIndex = 3
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.cmdClose)
        Me.Frame2.Controls.Add(Me.cmdStart)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 400)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(339, 43)
        Me.Frame2.TabIndex = 0
        Me.Frame2.TabStop = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Location = New System.Drawing.Point(208, 14)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(87, 23)
        Me.cmdClose.TabIndex = 2
        Me.cmdClose.Text = "Close"
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'cmdStart
        '
        Me.cmdStart.BackColor = System.Drawing.SystemColors.Control
        Me.cmdStart.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdStart.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdStart.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdStart.Location = New System.Drawing.Point(52, 14)
        Me.cmdStart.Name = "cmdStart"
        Me.cmdStart.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdStart.Size = New System.Drawing.Size(87, 23)
        Me.cmdStart.TabIndex = 1
        Me.cmdStart.Text = "Start"
        Me.cmdStart.UseVisualStyleBackColor = False
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me._optStatus_1)
        Me.Frame3.Controls.Add(Me._optStatus_0)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 192)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(339, 27)
        Me.Frame3.TabIndex = 29
        Me.Frame3.TabStop = False
        '
        '_optStatus_1
        '
        Me._optStatus_1.AutoSize = True
        Me._optStatus_1.BackColor = System.Drawing.SystemColors.Control
        Me._optStatus_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optStatus_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optStatus_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optStatus.SetIndex(Me._optStatus_1, CType(1, Short))
        Me._optStatus_1.Location = New System.Drawing.Point(182, 10)
        Me._optStatus_1.Name = "_optStatus_1"
        Me._optStatus_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optStatus_1.Size = New System.Drawing.Size(57, 18)
        Me._optStatus_1.TabIndex = 31
        Me._optStatus_1.TabStop = True
        Me._optStatus_1.Text = "Close"
        Me._optStatus_1.UseVisualStyleBackColor = False
        '
        '_optStatus_0
        '
        Me._optStatus_0.AutoSize = True
        Me._optStatus_0.BackColor = System.Drawing.SystemColors.Control
        Me._optStatus_0.Checked = True
        Me._optStatus_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optStatus_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optStatus_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optStatus.SetIndex(Me._optStatus_0, CType(0, Short))
        Me._optStatus_0.Location = New System.Drawing.Point(30, 10)
        Me._optStatus_0.Name = "_optStatus_0"
        Me._optStatus_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optStatus_0.Size = New System.Drawing.Size(54, 18)
        Me._optStatus_0.TabIndex = 30
        Me._optStatus_0.TabStop = True
        Me._optStatus_0.Text = "Open"
        Me._optStatus_0.UseVisualStyleBackColor = False
        '
        'frmTrfOpStock
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(340, 444)
        Me.Controls.Add(Me.Frame5)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me._TxtDisplayTransfer_1)
        Me.Controls.Add(Me.FraItem)
        Me.Controls.Add(Me.FraFYear)
        Me.Controls.Add(Me._TxtDisplayTransfer_0)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.Frame3)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Location = New System.Drawing.Point(4, 23)
        Me.MaximizeBox = False
        Me.Name = "frmTrfOpStock"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds
        Me.Text = "Transfer Opening Stock"
        Me.Frame5.ResumeLayout(False)
        Me.Frame5.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.FraItem.ResumeLayout(False)
        Me.FraItem.PerformLayout()
        Me.FraFYear.ResumeLayout(False)
        Me.Frame2.ResumeLayout(False)
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        CType(Me.TxtDisplayTransfer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optExportItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optStatus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class