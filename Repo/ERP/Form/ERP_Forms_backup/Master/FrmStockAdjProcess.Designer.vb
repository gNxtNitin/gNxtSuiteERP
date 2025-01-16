<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmStoreAdjustmentProcess
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
	Public WithEvents lstMaterialType As System.Windows.Forms.CheckedListBox
	Public WithEvents Frame6 As System.Windows.Forms.GroupBox
	Public WithEvents chkAllItem As System.Windows.Forms.CheckBox
	Public WithEvents TxtItemName As System.Windows.Forms.TextBox
	Public WithEvents cmdSearch As System.Windows.Forms.Button
	Public WithEvents txtItemCode As System.Windows.Forms.TextBox
	Public WithEvents cboDivision As System.Windows.Forms.ComboBox
	Public WithEvents CboSType As System.Windows.Forms.ComboBox
	Public WithEvents cmdSearchDept As System.Windows.Forms.Button
	Public WithEvents txtDept As System.Windows.Forms.TextBox
	Public WithEvents txtEmpCode As System.Windows.Forms.TextBox
	Public WithEvents TxtAdjDate As System.Windows.Forms.MaskedTextBox
	Public WithEvents Label4 As System.Windows.Forms.Label
	Public WithEvents Label5 As System.Windows.Forms.Label
	Public WithEvents Label21 As System.Windows.Forms.Label
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents _lblLabels_8 As System.Windows.Forms.Label
	Public WithEvents Label3 As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents Frame4 As System.Windows.Forms.GroupBox
	Public WithEvents _optUpdate_1 As System.Windows.Forms.RadioButton
	Public WithEvents _optUpdate_0 As System.Windows.Forms.RadioButton
	Public WithEvents Frame2 As System.Windows.Forms.GroupBox
	Public WithEvents _optWareHouse_2 As System.Windows.Forms.RadioButton
	Public WithEvents _optWareHouse_1 As System.Windows.Forms.RadioButton
	Public WithEvents _optWareHouse_0 As System.Windows.Forms.RadioButton
	Public WithEvents Frame1 As System.Windows.Forms.GroupBox
	Public WithEvents cmdSave As System.Windows.Forms.Button
	Public WithEvents cmdClose As System.Windows.Forms.Button
	Public WithEvents Frame3 As System.Windows.Forms.GroupBox
	Public WithEvents lblCount As System.Windows.Forms.Label
	Public WithEvents lblLabels As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
	Public WithEvents optUpdate As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
	Public WithEvents optWareHouse As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmStoreAdjustmentProcess))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TxtItemName = New System.Windows.Forms.TextBox()
        Me.cmdSearch = New System.Windows.Forms.Button()
        Me.txtItemCode = New System.Windows.Forms.TextBox()
        Me.cmdSearchDept = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.lstMaterialType = New System.Windows.Forms.CheckedListBox()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.chkAllItem = New System.Windows.Forms.CheckBox()
        Me.cboDivision = New System.Windows.Forms.ComboBox()
        Me.CboSType = New System.Windows.Forms.ComboBox()
        Me.txtDept = New System.Windows.Forms.TextBox()
        Me.txtEmpCode = New System.Windows.Forms.TextBox()
        Me.TxtAdjDate = New System.Windows.Forms.MaskedTextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me._lblLabels_8 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me._optUpdate_1 = New System.Windows.Forms.RadioButton()
        Me._optUpdate_0 = New System.Windows.Forms.RadioButton()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me._optWareHouse_2 = New System.Windows.Forms.RadioButton()
        Me._optWareHouse_1 = New System.Windows.Forms.RadioButton()
        Me._optWareHouse_0 = New System.Windows.Forms.RadioButton()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.lblCount = New System.Windows.Forms.Label()
        Me.lblLabels = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.optUpdate = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optWareHouse = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.Frame6.SuspendLayout()
        Me.Frame4.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.Frame3.SuspendLayout()
        CType(Me.lblLabels, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optUpdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optWareHouse, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TxtItemName
        '
        Me.TxtItemName.AcceptsReturn = True
        Me.TxtItemName.BackColor = System.Drawing.SystemColors.Window
        Me.TxtItemName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtItemName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtItemName.Enabled = False
        Me.TxtItemName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtItemName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtItemName.Location = New System.Drawing.Point(100, 132)
        Me.TxtItemName.MaxLength = 0
        Me.TxtItemName.Name = "TxtItemName"
        Me.TxtItemName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtItemName.Size = New System.Drawing.Size(360, 20)
        Me.TxtItemName.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.TxtItemName, "Press F1 For Help")
        '
        'cmdSearch
        '
        Me.cmdSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearch.Image = CType(resources.GetObject("cmdSearch.Image"), System.Drawing.Image)
        Me.cmdSearch.Location = New System.Drawing.Point(188, 109)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearch.Size = New System.Drawing.Size(29, 18)
        Me.cmdSearch.TabIndex = 6
        Me.cmdSearch.TabStop = False
        Me.cmdSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearch, "Search")
        Me.cmdSearch.UseVisualStyleBackColor = False
        '
        'txtItemCode
        '
        Me.txtItemCode.AcceptsReturn = True
        Me.txtItemCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtItemCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItemCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtItemCode.Location = New System.Drawing.Point(100, 108)
        Me.txtItemCode.MaxLength = 0
        Me.txtItemCode.Name = "txtItemCode"
        Me.txtItemCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemCode.Size = New System.Drawing.Size(87, 20)
        Me.txtItemCode.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.txtItemCode, "Press F1 For Help")
        '
        'cmdSearchDept
        '
        Me.cmdSearchDept.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchDept.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchDept.Image = CType(resources.GetObject("cmdSearchDept.Image"), System.Drawing.Image)
        Me.cmdSearchDept.Location = New System.Drawing.Point(206, 59)
        Me.cmdSearchDept.Name = "cmdSearchDept"
        Me.cmdSearchDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchDept.Size = New System.Drawing.Size(23, 18)
        Me.cmdSearchDept.TabIndex = 3
        Me.cmdSearchDept.TabStop = False
        Me.cmdSearchDept.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchDept, "Search")
        Me.cmdSearchDept.UseVisualStyleBackColor = False
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Image = CType(resources.GetObject("cmdSave.Image"), System.Drawing.Image)
        Me.cmdSave.Location = New System.Drawing.Point(4, 11)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(67, 34)
        Me.cmdSave.TabIndex = 15
        Me.cmdSave.Text = "&Save"
        Me.cmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSave, "Save Current Record")
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.Location = New System.Drawing.Point(392, 11)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(67, 34)
        Me.cmdClose.TabIndex = 16
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdClose, "Close the form")
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.lstMaterialType)
        Me.Frame6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(0, 188)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(466, 91)
        Me.Frame6.TabIndex = 27
        Me.Frame6.TabStop = False
        Me.Frame6.Text = "Category"
        '
        'lstMaterialType
        '
        Me.lstMaterialType.BackColor = System.Drawing.SystemColors.Window
        Me.lstMaterialType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lstMaterialType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstMaterialType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstMaterialType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lstMaterialType.IntegralHeight = False
        Me.lstMaterialType.Items.AddRange(New Object() {"lstMaterialType"})
        Me.lstMaterialType.Location = New System.Drawing.Point(0, 12)
        Me.lstMaterialType.Name = "lstMaterialType"
        Me.lstMaterialType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lstMaterialType.Size = New System.Drawing.Size(466, 79)
        Me.lstMaterialType.TabIndex = 9
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.chkAllItem)
        Me.Frame4.Controls.Add(Me.TxtItemName)
        Me.Frame4.Controls.Add(Me.cmdSearch)
        Me.Frame4.Controls.Add(Me.txtItemCode)
        Me.Frame4.Controls.Add(Me.cboDivision)
        Me.Frame4.Controls.Add(Me.CboSType)
        Me.Frame4.Controls.Add(Me.cmdSearchDept)
        Me.Frame4.Controls.Add(Me.txtDept)
        Me.Frame4.Controls.Add(Me.txtEmpCode)
        Me.Frame4.Controls.Add(Me.TxtAdjDate)
        Me.Frame4.Controls.Add(Me.Label4)
        Me.Frame4.Controls.Add(Me.Label5)
        Me.Frame4.Controls.Add(Me.Label21)
        Me.Frame4.Controls.Add(Me.Label2)
        Me.Frame4.Controls.Add(Me._lblLabels_8)
        Me.Frame4.Controls.Add(Me.Label3)
        Me.Frame4.Controls.Add(Me.Label1)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, 0)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(464, 186)
        Me.Frame4.TabIndex = 21
        Me.Frame4.TabStop = False
        '
        'chkAllItem
        '
        Me.chkAllItem.AutoSize = True
        Me.chkAllItem.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllItem.Font = New System.Drawing.Font("Arial", 7.47!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllItem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllItem.Location = New System.Drawing.Point(220, 110)
        Me.chkAllItem.Name = "chkAllItem"
        Me.chkAllItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllItem.Size = New System.Drawing.Size(37, 15)
        Me.chkAllItem.TabIndex = 30
        Me.chkAllItem.Text = "All"
        Me.chkAllItem.UseVisualStyleBackColor = False
        '
        'cboDivision
        '
        Me.cboDivision.BackColor = System.Drawing.SystemColors.Window
        Me.cboDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivision.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDivision.Location = New System.Drawing.Point(100, 33)
        Me.cboDivision.Name = "cboDivision"
        Me.cboDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivision.Size = New System.Drawing.Size(195, 22)
        Me.cboDivision.TabIndex = 1
        '
        'CboSType
        '
        Me.CboSType.BackColor = System.Drawing.SystemColors.Window
        Me.CboSType.Cursor = System.Windows.Forms.Cursors.Default
        Me.CboSType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboSType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboSType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.CboSType.Location = New System.Drawing.Point(100, 156)
        Me.CboSType.Name = "CboSType"
        Me.CboSType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CboSType.Size = New System.Drawing.Size(97, 22)
        Me.CboSType.TabIndex = 8
        '
        'txtDept
        '
        Me.txtDept.AcceptsReturn = True
        Me.txtDept.BackColor = System.Drawing.SystemColors.Window
        Me.txtDept.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDept.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDept.ForeColor = System.Drawing.Color.Blue
        Me.txtDept.Location = New System.Drawing.Point(100, 59)
        Me.txtDept.MaxLength = 0
        Me.txtDept.Name = "txtDept"
        Me.txtDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDept.Size = New System.Drawing.Size(105, 20)
        Me.txtDept.TabIndex = 2
        '
        'txtEmpCode
        '
        Me.txtEmpCode.AcceptsReturn = True
        Me.txtEmpCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmpCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmpCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmpCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpCode.ForeColor = System.Drawing.Color.Blue
        Me.txtEmpCode.Location = New System.Drawing.Point(100, 84)
        Me.txtEmpCode.MaxLength = 0
        Me.txtEmpCode.Name = "txtEmpCode"
        Me.txtEmpCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmpCode.Size = New System.Drawing.Size(105, 20)
        Me.txtEmpCode.TabIndex = 4
        '
        'TxtAdjDate
        '
        Me.TxtAdjDate.AllowPromptAsInput = False
        Me.TxtAdjDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAdjDate.Location = New System.Drawing.Point(102, 9)
        Me.TxtAdjDate.Mask = "##/##/####"
        Me.TxtAdjDate.Name = "TxtAdjDate"
        Me.TxtAdjDate.Size = New System.Drawing.Size(79, 20)
        Me.TxtAdjDate.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 7.47!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(30, 135)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(63, 12)
        Me.Label4.TabIndex = 29
        Me.Label4.Text = "Item Name :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 7.47!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(33, 111)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(60, 12)
        Me.Label5.TabIndex = 28
        Me.Label5.Text = "Item Code :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.SystemColors.Control
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.Font = New System.Drawing.Font("Arial", 7.47!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(41, 38)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(52, 12)
        Me.Label21.TabIndex = 26
        Me.Label21.Text = "Division :"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 7.47!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(30, 161)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(63, 12)
        Me.Label2.TabIndex = 25
        Me.Label2.Text = "Stock Type :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_8
        '
        Me._lblLabels_8.AutoSize = True
        Me._lblLabels_8.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_8.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_8.Font = New System.Drawing.Font("Arial", 7.47!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_8.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblLabels.SetIndex(Me._lblLabels_8, CType(8, Short))
        Me._lblLabels_8.Location = New System.Drawing.Point(32, 11)
        Me._lblLabels_8.Name = "_lblLabels_8"
        Me._lblLabels_8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_8.Size = New System.Drawing.Size(61, 12)
        Me._lblLabels_8.TabIndex = 24
        Me._lblLabels_8.Text = "Date From :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 7.47!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(26, 64)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(67, 12)
        Me.Label3.TabIndex = 23
        Me.Label3.Text = "Department :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 7.47!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(32, 88)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(61, 12)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "Emp Code :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me._optUpdate_1)
        Me.Frame2.Controls.Add(Me._optUpdate_0)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(142, 283)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(155, 72)
        Me.Frame2.TabIndex = 20
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Stock Update"
        '
        '_optUpdate_1
        '
        Me._optUpdate_1.AutoSize = True
        Me._optUpdate_1.BackColor = System.Drawing.SystemColors.Control
        Me._optUpdate_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optUpdate_1.Font = New System.Drawing.Font("Arial", 7.47!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optUpdate_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optUpdate.SetIndex(Me._optUpdate_1, CType(1, Short))
        Me._optUpdate_1.Location = New System.Drawing.Point(7, 52)
        Me._optUpdate_1.Name = "_optUpdate_1"
        Me._optUpdate_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optUpdate_1.Size = New System.Drawing.Size(114, 15)
        Me._optUpdate_1.TabIndex = 14
        Me._optUpdate_1.TabStop = True
        Me._optUpdate_1.Text = "Only Physical Item"
        Me._optUpdate_1.UseVisualStyleBackColor = False
        '
        '_optUpdate_0
        '
        Me._optUpdate_0.AutoSize = True
        Me._optUpdate_0.BackColor = System.Drawing.SystemColors.Control
        Me._optUpdate_0.Checked = True
        Me._optUpdate_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optUpdate_0.Font = New System.Drawing.Font("Arial", 7.47!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optUpdate_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optUpdate.SetIndex(Me._optUpdate_0, CType(0, Short))
        Me._optUpdate_0.Location = New System.Drawing.Point(7, 15)
        Me._optUpdate_0.Name = "_optUpdate_0"
        Me._optUpdate_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optUpdate_0.Size = New System.Drawing.Size(36, 15)
        Me._optUpdate_0.TabIndex = 13
        Me._optUpdate_0.TabStop = True
        Me._optUpdate_0.Text = "All"
        Me._optUpdate_0.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me._optWareHouse_2)
        Me.Frame1.Controls.Add(Me._optWareHouse_1)
        Me.Frame1.Controls.Add(Me._optWareHouse_0)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 283)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(141, 72)
        Me.Frame1.TabIndex = 19
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Ware House"
        '
        '_optWareHouse_2
        '
        Me._optWareHouse_2.AutoSize = True
        Me._optWareHouse_2.BackColor = System.Drawing.SystemColors.Control
        Me._optWareHouse_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optWareHouse_2.Font = New System.Drawing.Font("Arial", 7.47!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optWareHouse_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optWareHouse.SetIndex(Me._optWareHouse_2, CType(2, Short))
        Me._optWareHouse_2.Location = New System.Drawing.Point(11, 52)
        Me._optWareHouse_2.Name = "_optWareHouse_2"
        Me._optWareHouse_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optWareHouse_2.Size = New System.Drawing.Size(71, 15)
        Me._optWareHouse_2.TabIndex = 12
        Me._optWareHouse_2.TabStop = True
        Me._optWareHouse_2.Text = "Sub Store"
        Me._optWareHouse_2.UseVisualStyleBackColor = False
        '
        '_optWareHouse_1
        '
        Me._optWareHouse_1.AutoSize = True
        Me._optWareHouse_1.BackColor = System.Drawing.SystemColors.Control
        Me._optWareHouse_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optWareHouse_1.Font = New System.Drawing.Font("Arial", 7.47!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optWareHouse_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optWareHouse.SetIndex(Me._optWareHouse_1, CType(1, Short))
        Me._optWareHouse_1.Location = New System.Drawing.Point(11, 33)
        Me._optWareHouse_1.Name = "_optWareHouse_1"
        Me._optWareHouse_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optWareHouse_1.Size = New System.Drawing.Size(76, 15)
        Me._optWareHouse_1.TabIndex = 11
        Me._optWareHouse_1.TabStop = True
        Me._optWareHouse_1.Text = "Production"
        Me._optWareHouse_1.UseVisualStyleBackColor = False
        '
        '_optWareHouse_0
        '
        Me._optWareHouse_0.AutoSize = True
        Me._optWareHouse_0.BackColor = System.Drawing.SystemColors.Control
        Me._optWareHouse_0.Checked = True
        Me._optWareHouse_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optWareHouse_0.Font = New System.Drawing.Font("Arial", 7.47!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optWareHouse_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optWareHouse.SetIndex(Me._optWareHouse_0, CType(0, Short))
        Me._optWareHouse_0.Location = New System.Drawing.Point(11, 15)
        Me._optWareHouse_0.Name = "_optWareHouse_0"
        Me._optWareHouse_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optWareHouse_0.Size = New System.Drawing.Size(49, 15)
        Me._optWareHouse_0.TabIndex = 10
        Me._optWareHouse_0.TabStop = True
        Me._optWareHouse_0.Text = "Store"
        Me._optWareHouse_0.UseVisualStyleBackColor = False
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.cmdSave)
        Me.Frame3.Controls.Add(Me.cmdClose)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 379)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(466, 49)
        Me.Frame3.TabIndex = 17
        Me.Frame3.TabStop = False
        '
        'lblCount
        '
        Me.lblCount.BackColor = System.Drawing.SystemColors.Control
        Me.lblCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCount.Font = New System.Drawing.Font("Arial", 12.23!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCount.Location = New System.Drawing.Point(0, 359)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCount.Size = New System.Drawing.Size(466, 19)
        Me.lblCount.TabIndex = 18
        Me.lblCount.Text = "0"
        Me.lblCount.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'optWareHouse
        '
        '
        'frmStoreAdjustmentProcess
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(468, 429)
        Me.Controls.Add(Me.Frame6)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.lblCount)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmStoreAdjustmentProcess"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Stock Adjustment (Store) Process"
        Me.Frame6.ResumeLayout(False)
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        CType(Me.lblLabels, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optUpdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optWareHouse, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class