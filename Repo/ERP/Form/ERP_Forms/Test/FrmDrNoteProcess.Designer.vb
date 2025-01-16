Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmDrNoteProcess
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
        'Me.MDIParent = AccountGST.Master
        'AccountGST.Master.Show
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
	Public WithEvents txtAmendNo As System.Windows.Forms.TextBox
	Public WithEvents txtPONo As System.Windows.Forms.TextBox
	Public WithEvents _optRate_1 As System.Windows.Forms.RadioButton
	Public WithEvents _optRate_0 As System.Windows.Forms.RadioButton
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents Frame8 As System.Windows.Forms.GroupBox
	Public WithEvents cboDivision As System.Windows.Forms.ComboBox
	Public WithEvents Frame7 As System.Windows.Forms.GroupBox
	Public WithEvents _optAgt_2 As System.Windows.Forms.RadioButton
	Public WithEvents _optAgt_0 As System.Windows.Forms.RadioButton
	Public WithEvents _optAgt_1 As System.Windows.Forms.RadioButton
	Public WithEvents Frame6 As System.Windows.Forms.GroupBox
	Public WithEvents _optBaseOn_2 As System.Windows.Forms.RadioButton
	Public WithEvents _optBaseOn_0 As System.Windows.Forms.RadioButton
	Public WithEvents _optBaseOn_1 As System.Windows.Forms.RadioButton
	Public WithEvents Frame5 As System.Windows.Forms.GroupBox
	Public WithEvents chkAgtD3 As System.Windows.Forms.CheckBox
	Public WithEvents _optType_1 As System.Windows.Forms.RadioButton
	Public WithEvents _optType_0 As System.Windows.Forms.RadioButton
	Public WithEvents Frame3 As System.Windows.Forms.GroupBox
	Public WithEvents _OptItem_0 As System.Windows.Forms.RadioButton
	Public WithEvents _OptItem_1 As System.Windows.Forms.RadioButton
	Public WithEvents cmdSearchItem As System.Windows.Forms.Button
	Public WithEvents txtItem As System.Windows.Forms.TextBox
	Public WithEvents Frame4 As System.Windows.Forms.GroupBox
	Public WithEvents _optCustomer_0 As System.Windows.Forms.RadioButton
	Public WithEvents _optCustomer_1 As System.Windows.Forms.RadioButton
	Public WithEvents TxtAccount As System.Windows.Forms.TextBox
	Public WithEvents cmdsearch As System.Windows.Forms.Button
	Public WithEvents Frame1 As System.Windows.Forms.GroupBox
	Public WithEvents TxtDtFrom As System.Windows.Forms.MaskedTextBox
	Public WithEvents TxtDtTo As System.Windows.Forms.MaskedTextBox
	Public WithEvents LblDtto As System.Windows.Forms.Label
	Public WithEvents LblDtfr As System.Windows.Forms.Label
	Public WithEvents Frame2 As System.Windows.Forms.GroupBox
	Public WithEvents cmdProcess As System.Windows.Forms.Button
	Public WithEvents cmdCancel As System.Windows.Forms.Button
	Public WithEvents Report1 As AxCrystal.AxCrystalReport
	Public WithEvents lblDNCNSeqType As System.Windows.Forms.Label
	Public WithEvents FraButton As System.Windows.Forms.GroupBox
	Public WithEvents OptItem As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
	Public WithEvents optAgt As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
	Public WithEvents optBaseOn As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
	Public WithEvents optCustomer As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
	Public WithEvents optRate As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
	Public WithEvents optType As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmDrNoteProcess))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtAmendNo = New System.Windows.Forms.TextBox()
        Me.txtPONo = New System.Windows.Forms.TextBox()
        Me.cmdSearchItem = New System.Windows.Forms.Button()
        Me.txtItem = New System.Windows.Forms.TextBox()
        Me.TxtAccount = New System.Windows.Forms.TextBox()
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.cmdProcess = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.Frame8 = New System.Windows.Forms.GroupBox()
        Me._optRate_1 = New System.Windows.Forms.RadioButton()
        Me._optRate_0 = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Frame7 = New System.Windows.Forms.GroupBox()
        Me.cboDivision = New System.Windows.Forms.ComboBox()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me._optAgt_2 = New System.Windows.Forms.RadioButton()
        Me._optAgt_0 = New System.Windows.Forms.RadioButton()
        Me._optAgt_1 = New System.Windows.Forms.RadioButton()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me._optBaseOn_2 = New System.Windows.Forms.RadioButton()
        Me._optBaseOn_0 = New System.Windows.Forms.RadioButton()
        Me._optBaseOn_1 = New System.Windows.Forms.RadioButton()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.chkAgtD3 = New System.Windows.Forms.CheckBox()
        Me._optType_1 = New System.Windows.Forms.RadioButton()
        Me._optType_0 = New System.Windows.Forms.RadioButton()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me._OptItem_0 = New System.Windows.Forms.RadioButton()
        Me._OptItem_1 = New System.Windows.Forms.RadioButton()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me._optCustomer_0 = New System.Windows.Forms.RadioButton()
        Me._optCustomer_1 = New System.Windows.Forms.RadioButton()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.TxtDtFrom = New System.Windows.Forms.MaskedTextBox()
        Me.TxtDtTo = New System.Windows.Forms.MaskedTextBox()
        Me.LblDtto = New System.Windows.Forms.Label()
        Me.LblDtfr = New System.Windows.Forms.Label()
        Me.FraButton = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.lblDNCNSeqType = New System.Windows.Forms.Label()
        Me.OptItem = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optAgt = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optBaseOn = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optCustomer = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optRate = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optType = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.Frame8.SuspendLayout()
        Me.Frame7.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me.Frame5.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.Frame4.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.FraButton.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optAgt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optBaseOn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtAmendNo
        '
        Me.txtAmendNo.AcceptsReturn = True
        Me.txtAmendNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtAmendNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmendNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAmendNo.Enabled = False
        Me.txtAmendNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmendNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAmendNo.Location = New System.Drawing.Point(268, 32)
        Me.txtAmendNo.MaxLength = 0
        Me.txtAmendNo.Name = "txtAmendNo"
        Me.txtAmendNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAmendNo.Size = New System.Drawing.Size(45, 19)
        Me.txtAmendNo.TabIndex = 37
        Me.ToolTip1.SetToolTip(Me.txtAmendNo, "Press F1 For Help")
        '
        'txtPONo
        '
        Me.txtPONo.AcceptsReturn = True
        Me.txtPONo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPONo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPONo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPONo.Enabled = False
        Me.txtPONo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPONo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPONo.Location = New System.Drawing.Point(108, 32)
        Me.txtPONo.MaxLength = 0
        Me.txtPONo.Name = "txtPONo"
        Me.txtPONo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPONo.Size = New System.Drawing.Size(89, 19)
        Me.txtPONo.TabIndex = 35
        Me.ToolTip1.SetToolTip(Me.txtPONo, "Press F1 For Help")
        '
        'cmdSearchItem
        '
        Me.cmdSearchItem.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchItem.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchItem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchItem.Image = CType(resources.GetObject("cmdSearchItem.Image"), System.Drawing.Image)
        Me.cmdSearchItem.Location = New System.Drawing.Point(294, 30)
        Me.cmdSearchItem.Name = "cmdSearchItem"
        Me.cmdSearchItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchItem.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearchItem.TabIndex = 10
        Me.cmdSearchItem.TabStop = False
        Me.cmdSearchItem.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchItem, "Search")
        Me.cmdSearchItem.UseVisualStyleBackColor = False
        '
        'txtItem
        '
        Me.txtItem.AcceptsReturn = True
        Me.txtItem.BackColor = System.Drawing.SystemColors.Window
        Me.txtItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItem.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItem.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItem.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtItem.Location = New System.Drawing.Point(4, 30)
        Me.txtItem.MaxLength = 0
        Me.txtItem.Name = "txtItem"
        Me.txtItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItem.Size = New System.Drawing.Size(289, 19)
        Me.txtItem.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.txtItem, "Press F1 For Help")
        '
        'TxtAccount
        '
        Me.TxtAccount.AcceptsReturn = True
        Me.TxtAccount.BackColor = System.Drawing.SystemColors.Window
        Me.TxtAccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtAccount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtAccount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAccount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtAccount.Location = New System.Drawing.Point(4, 29)
        Me.TxtAccount.MaxLength = 0
        Me.TxtAccount.Name = "TxtAccount"
        Me.TxtAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtAccount.Size = New System.Drawing.Size(289, 19)
        Me.TxtAccount.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.TxtAccount, "Press F1 For Help")
        '
        'cmdsearch
        '
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.Location = New System.Drawing.Point(294, 29)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(29, 19)
        Me.cmdsearch.TabIndex = 6
        Me.cmdsearch.TabStop = False
        Me.cmdsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearch, "Search")
        Me.cmdsearch.UseVisualStyleBackColor = False
        '
        'cmdProcess
        '
        Me.cmdProcess.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdProcess.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdProcess.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdProcess.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdProcess.Image = CType(resources.GetObject("cmdProcess.Image"), System.Drawing.Image)
        Me.cmdProcess.Location = New System.Drawing.Point(4, 12)
        Me.cmdProcess.Name = "cmdProcess"
        Me.cmdProcess.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdProcess.Size = New System.Drawing.Size(80, 34)
        Me.cmdProcess.TabIndex = 17
        Me.cmdProcess.Text = "&Process"
        Me.cmdProcess.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdProcess, "Save Current Record")
        Me.cmdProcess.UseVisualStyleBackColor = False
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Image = CType(resources.GetObject("cmdCancel.Image"), System.Drawing.Image)
        Me.cmdCancel.Location = New System.Drawing.Point(210, 12)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(80, 34)
        Me.cmdCancel.TabIndex = 11
        Me.cmdCancel.Text = "&Close"
        Me.cmdCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdCancel, "Close")
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'Frame8
        '
        Me.Frame8.BackColor = System.Drawing.SystemColors.Control
        Me.Frame8.Controls.Add(Me.txtAmendNo)
        Me.Frame8.Controls.Add(Me.txtPONo)
        Me.Frame8.Controls.Add(Me._optRate_1)
        Me.Frame8.Controls.Add(Me._optRate_0)
        Me.Frame8.Controls.Add(Me.Label1)
        Me.Frame8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame8.Location = New System.Drawing.Point(0, 263)
        Me.Frame8.Name = "Frame8"
        Me.Frame8.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame8.Size = New System.Drawing.Size(327, 60)
        Me.Frame8.TabIndex = 32
        Me.Frame8.TabStop = False
        Me.Frame8.Text = "Rate "
        '
        '_optRate_1
        '
        Me._optRate_1.AutoSize = True
        Me._optRate_1.BackColor = System.Drawing.SystemColors.Control
        Me._optRate_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optRate_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optRate_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optRate.SetIndex(Me._optRate_1, CType(1, Short))
        Me._optRate_1.Location = New System.Drawing.Point(44, 34)
        Me._optRate_1.Name = "_optRate_1"
        Me._optRate_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optRate_1.Size = New System.Drawing.Size(57, 18)
        Me._optRate_1.TabIndex = 34
        Me._optRate_1.TabStop = True
        Me._optRate_1.Text = "PO No"
        Me._optRate_1.UseVisualStyleBackColor = False
        '
        '_optRate_0
        '
        Me._optRate_0.AutoSize = True
        Me._optRate_0.BackColor = System.Drawing.SystemColors.Control
        Me._optRate_0.Checked = True
        Me._optRate_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optRate_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optRate_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optRate.SetIndex(Me._optRate_0, CType(0, Short))
        Me._optRate_0.Location = New System.Drawing.Point(44, 10)
        Me._optRate_0.Name = "_optRate_0"
        Me._optRate_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optRate_0.Size = New System.Drawing.Size(94, 18)
        Me._optRate_0.TabIndex = 33
        Me._optRate_0.TabStop = True
        Me._optRate_0.Text = "Lastest Rate"
        Me._optRate_0.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(200, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(70, 14)
        Me.Label1.TabIndex = 36
        Me.Label1.Text = "Amend No :"
        '
        'Frame7
        '
        Me.Frame7.BackColor = System.Drawing.SystemColors.Control
        Me.Frame7.Controls.Add(Me.cboDivision)
        Me.Frame7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame7.Location = New System.Drawing.Point(0, 42)
        Me.Frame7.Name = "Frame7"
        Me.Frame7.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame7.Size = New System.Drawing.Size(327, 35)
        Me.Frame7.TabIndex = 29
        Me.Frame7.TabStop = False
        Me.Frame7.Text = "Division"
        '
        'cboDivision
        '
        Me.cboDivision.BackColor = System.Drawing.SystemColors.Window
        Me.cboDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivision.Enabled = False
        Me.cboDivision.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDivision.Location = New System.Drawing.Point(56, 10)
        Me.cboDivision.Name = "cboDivision"
        Me.cboDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivision.Size = New System.Drawing.Size(245, 22)
        Me.cboDivision.TabIndex = 30
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me._optAgt_2)
        Me.Frame6.Controls.Add(Me._optAgt_0)
        Me.Frame6.Controls.Add(Me._optAgt_1)
        Me.Frame6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(0, 324)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(327, 39)
        Me.Frame6.TabIndex = 26
        Me.Frame6.TabStop = False
        Me.Frame6.Text = "Against"
        '
        '_optAgt_2
        '
        Me._optAgt_2.AutoSize = True
        Me._optAgt_2.BackColor = System.Drawing.SystemColors.Control
        Me._optAgt_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optAgt_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optAgt_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optAgt.SetIndex(Me._optAgt_2, CType(2, Short))
        Me._optAgt_2.Location = New System.Drawing.Point(250, 16)
        Me._optAgt_2.Name = "_optAgt_2"
        Me._optAgt_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optAgt_2.Size = New System.Drawing.Size(47, 18)
        Me._optAgt_2.TabIndex = 31
        Me._optAgt_2.TabStop = True
        Me._optAgt_2.Text = "RGP"
        Me._optAgt_2.UseVisualStyleBackColor = False
        '
        '_optAgt_0
        '
        Me._optAgt_0.AutoSize = True
        Me._optAgt_0.BackColor = System.Drawing.SystemColors.Control
        Me._optAgt_0.Checked = True
        Me._optAgt_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optAgt_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optAgt_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optAgt.SetIndex(Me._optAgt_0, CType(0, Short))
        Me._optAgt_0.Location = New System.Drawing.Point(10, 16)
        Me._optAgt_0.Name = "_optAgt_0"
        Me._optAgt_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optAgt_0.Size = New System.Drawing.Size(112, 18)
        Me._optAgt_0.TabIndex = 28
        Me._optAgt_0.TabStop = True
        Me._optAgt_0.Text = "Purchase Order"
        Me._optAgt_0.UseVisualStyleBackColor = False
        '
        '_optAgt_1
        '
        Me._optAgt_1.AutoSize = True
        Me._optAgt_1.BackColor = System.Drawing.SystemColors.Control
        Me._optAgt_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optAgt_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optAgt_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optAgt.SetIndex(Me._optAgt_1, CType(1, Short))
        Me._optAgt_1.Location = New System.Drawing.Point(138, 16)
        Me._optAgt_1.Name = "_optAgt_1"
        Me._optAgt_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optAgt_1.Size = New System.Drawing.Size(88, 18)
        Me._optAgt_1.TabIndex = 27
        Me._optAgt_1.TabStop = True
        Me._optAgt_1.Text = "Sale Return"
        Me._optAgt_1.UseVisualStyleBackColor = False
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me._optBaseOn_2)
        Me.Frame5.Controls.Add(Me._optBaseOn_0)
        Me.Frame5.Controls.Add(Me._optBaseOn_1)
        Me.Frame5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(0, 76)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(327, 35)
        Me.Frame5.TabIndex = 21
        Me.Frame5.TabStop = False
        Me.Frame5.Text = "Base On"
        '
        '_optBaseOn_2
        '
        Me._optBaseOn_2.AutoSize = True
        Me._optBaseOn_2.BackColor = System.Drawing.SystemColors.Control
        Me._optBaseOn_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optBaseOn_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optBaseOn_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optBaseOn.SetIndex(Me._optBaseOn_2, CType(2, Short))
        Me._optBaseOn_2.Location = New System.Drawing.Point(214, 14)
        Me._optBaseOn_2.Name = "_optBaseOn_2"
        Me._optBaseOn_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optBaseOn_2.Size = New System.Drawing.Size(76, 18)
        Me._optBaseOn_2.TabIndex = 24
        Me._optBaseOn_2.TabStop = True
        Me._optBaseOn_2.Text = "MRR Date"
        Me._optBaseOn_2.UseVisualStyleBackColor = False
        '
        '_optBaseOn_0
        '
        Me._optBaseOn_0.AutoSize = True
        Me._optBaseOn_0.BackColor = System.Drawing.SystemColors.Control
        Me._optBaseOn_0.Checked = True
        Me._optBaseOn_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optBaseOn_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optBaseOn_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optBaseOn.SetIndex(Me._optBaseOn_0, CType(0, Short))
        Me._optBaseOn_0.Location = New System.Drawing.Point(10, 14)
        Me._optBaseOn_0.Name = "_optBaseOn_0"
        Me._optBaseOn_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optBaseOn_0.Size = New System.Drawing.Size(68, 18)
        Me._optBaseOn_0.TabIndex = 23
        Me._optBaseOn_0.TabStop = True
        Me._optBaseOn_0.Text = "Bill Date"
        Me._optBaseOn_0.UseVisualStyleBackColor = False
        '
        '_optBaseOn_1
        '
        Me._optBaseOn_1.AutoSize = True
        Me._optBaseOn_1.BackColor = System.Drawing.SystemColors.Control
        Me._optBaseOn_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optBaseOn_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optBaseOn_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optBaseOn.SetIndex(Me._optBaseOn_1, CType(1, Short))
        Me._optBaseOn_1.Location = New System.Drawing.Point(104, 14)
        Me._optBaseOn_1.Name = "_optBaseOn_1"
        Me._optBaseOn_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optBaseOn_1.Size = New System.Drawing.Size(57, 18)
        Me._optBaseOn_1.TabIndex = 22
        Me._optBaseOn_1.TabStop = True
        Me._optBaseOn_1.Text = "VDate"
        Me._optBaseOn_1.UseVisualStyleBackColor = False
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.chkAgtD3)
        Me.Frame3.Controls.Add(Me._optType_1)
        Me.Frame3.Controls.Add(Me._optType_0)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 112)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(327, 35)
        Me.Frame3.TabIndex = 18
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "Type"
        '
        'chkAgtD3
        '
        Me.chkAgtD3.AutoSize = True
        Me.chkAgtD3.BackColor = System.Drawing.SystemColors.Control
        Me.chkAgtD3.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAgtD3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAgtD3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAgtD3.Location = New System.Drawing.Point(224, 15)
        Me.chkAgtD3.Name = "chkAgtD3"
        Me.chkAgtD3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAgtD3.Size = New System.Drawing.Size(84, 18)
        Me.chkAgtD3.TabIndex = 25
        Me.chkAgtD3.Text = "Against D3"
        Me.chkAgtD3.UseVisualStyleBackColor = False
        '
        '_optType_1
        '
        Me._optType_1.AutoSize = True
        Me._optType_1.BackColor = System.Drawing.SystemColors.Control
        Me._optType_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optType_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optType_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optType.SetIndex(Me._optType_1, CType(1, Short))
        Me._optType_1.Location = New System.Drawing.Point(120, 15)
        Me._optType_1.Name = "_optType_1"
        Me._optType_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optType_1.Size = New System.Drawing.Size(87, 18)
        Me._optType_1.TabIndex = 20
        Me._optType_1.TabStop = True
        Me._optType_1.Text = "Credit Note"
        Me._optType_1.UseVisualStyleBackColor = False
        '
        '_optType_0
        '
        Me._optType_0.AutoSize = True
        Me._optType_0.BackColor = System.Drawing.SystemColors.Control
        Me._optType_0.Checked = True
        Me._optType_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optType_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optType_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optType.SetIndex(Me._optType_0, CType(0, Short))
        Me._optType_0.Location = New System.Drawing.Point(8, 15)
        Me._optType_0.Name = "_optType_0"
        Me._optType_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optType_0.Size = New System.Drawing.Size(81, 18)
        Me._optType_0.TabIndex = 19
        Me._optType_0.TabStop = True
        Me._optType_0.Text = "Debit Note"
        Me._optType_0.UseVisualStyleBackColor = False
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me._OptItem_0)
        Me.Frame4.Controls.Add(Me._OptItem_1)
        Me.Frame4.Controls.Add(Me.cmdSearchItem)
        Me.Frame4.Controls.Add(Me.txtItem)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, 205)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(327, 55)
        Me.Frame4.TabIndex = 16
        Me.Frame4.TabStop = False
        Me.Frame4.Text = "Item"
        '
        '_OptItem_0
        '
        Me._OptItem_0.AutoSize = True
        Me._OptItem_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptItem_0.Checked = True
        Me._OptItem_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptItem_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptItem_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptItem.SetIndex(Me._OptItem_0, CType(0, Short))
        Me._OptItem_0.Location = New System.Drawing.Point(102, 12)
        Me._OptItem_0.Name = "_OptItem_0"
        Me._OptItem_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptItem_0.Size = New System.Drawing.Size(39, 18)
        Me._OptItem_0.TabIndex = 7
        Me._OptItem_0.TabStop = True
        Me._OptItem_0.Text = "All"
        Me._OptItem_0.UseVisualStyleBackColor = False
        '
        '_OptItem_1
        '
        Me._OptItem_1.AutoSize = True
        Me._OptItem_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptItem_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptItem_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptItem_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptItem.SetIndex(Me._OptItem_1, CType(1, Short))
        Me._OptItem_1.Location = New System.Drawing.Point(214, 12)
        Me._OptItem_1.Name = "_OptItem_1"
        Me._OptItem_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptItem_1.Size = New System.Drawing.Size(84, 18)
        Me._OptItem_1.TabIndex = 8
        Me._OptItem_1.TabStop = True
        Me._OptItem_1.Text = "Particulars"
        Me._OptItem_1.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me._optCustomer_0)
        Me.Frame1.Controls.Add(Me._optCustomer_1)
        Me.Frame1.Controls.Add(Me.TxtAccount)
        Me.Frame1.Controls.Add(Me.cmdsearch)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 152)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(327, 54)
        Me.Frame1.TabIndex = 15
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Customer"
        '
        '_optCustomer_0
        '
        Me._optCustomer_0.AutoSize = True
        Me._optCustomer_0.BackColor = System.Drawing.SystemColors.Control
        Me._optCustomer_0.Checked = True
        Me._optCustomer_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optCustomer_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optCustomer_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCustomer.SetIndex(Me._optCustomer_0, CType(0, Short))
        Me._optCustomer_0.Location = New System.Drawing.Point(102, 10)
        Me._optCustomer_0.Name = "_optCustomer_0"
        Me._optCustomer_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optCustomer_0.Size = New System.Drawing.Size(39, 18)
        Me._optCustomer_0.TabIndex = 3
        Me._optCustomer_0.TabStop = True
        Me._optCustomer_0.Text = "All"
        Me._optCustomer_0.UseVisualStyleBackColor = False
        '
        '_optCustomer_1
        '
        Me._optCustomer_1.AutoSize = True
        Me._optCustomer_1.BackColor = System.Drawing.SystemColors.Control
        Me._optCustomer_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optCustomer_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optCustomer_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCustomer.SetIndex(Me._optCustomer_1, CType(1, Short))
        Me._optCustomer_1.Location = New System.Drawing.Point(214, 10)
        Me._optCustomer_1.Name = "_optCustomer_1"
        Me._optCustomer_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optCustomer_1.Size = New System.Drawing.Size(84, 18)
        Me._optCustomer_1.TabIndex = 4
        Me._optCustomer_1.TabStop = True
        Me._optCustomer_1.Text = "Particulars"
        Me._optCustomer_1.UseVisualStyleBackColor = False
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.TxtDtFrom)
        Me.Frame2.Controls.Add(Me.TxtDtTo)
        Me.Frame2.Controls.Add(Me.LblDtto)
        Me.Frame2.Controls.Add(Me.LblDtfr)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 0)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(327, 41)
        Me.Frame2.TabIndex = 0
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Date Range"
        '
        'TxtDtFrom
        '
        Me.TxtDtFrom.AllowPromptAsInput = False
        Me.TxtDtFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDtFrom.Location = New System.Drawing.Point(80, 14)
        Me.TxtDtFrom.Mask = "##/##/####"
        Me.TxtDtFrom.Name = "TxtDtFrom"
        Me.TxtDtFrom.Size = New System.Drawing.Size(81, 20)
        Me.TxtDtFrom.TabIndex = 1
        '
        'TxtDtTo
        '
        Me.TxtDtTo.AllowPromptAsInput = False
        Me.TxtDtTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDtTo.Location = New System.Drawing.Point(228, 14)
        Me.TxtDtTo.Mask = "##/##/####"
        Me.TxtDtTo.Name = "TxtDtTo"
        Me.TxtDtTo.Size = New System.Drawing.Size(81, 20)
        Me.TxtDtTo.TabIndex = 2
        '
        'LblDtto
        '
        Me.LblDtto.AutoSize = True
        Me.LblDtto.BackColor = System.Drawing.SystemColors.Control
        Me.LblDtto.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblDtto.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDtto.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblDtto.Location = New System.Drawing.Point(172, 16)
        Me.LblDtto.Name = "LblDtto"
        Me.LblDtto.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblDtto.Size = New System.Drawing.Size(53, 14)
        Me.LblDtto.TabIndex = 14
        Me.LblDtto.Text = "Date To :"
        '
        'LblDtfr
        '
        Me.LblDtfr.AutoSize = True
        Me.LblDtfr.BackColor = System.Drawing.SystemColors.Control
        Me.LblDtfr.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblDtfr.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDtfr.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblDtfr.Location = New System.Drawing.Point(10, 16)
        Me.LblDtfr.Name = "LblDtfr"
        Me.LblDtfr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblDtfr.Size = New System.Drawing.Size(69, 14)
        Me.LblDtfr.TabIndex = 12
        Me.LblDtfr.Text = "Date From :"
        '
        'FraButton
        '
        Me.FraButton.BackColor = System.Drawing.SystemColors.Control
        Me.FraButton.Controls.Add(Me.cmdProcess)
        Me.FraButton.Controls.Add(Me.cmdCancel)
        Me.FraButton.Controls.Add(Me.Report1)
        Me.FraButton.Controls.Add(Me.lblDNCNSeqType)
        Me.FraButton.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraButton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraButton.Location = New System.Drawing.Point(0, 360)
        Me.FraButton.Name = "FraButton"
        Me.FraButton.Padding = New System.Windows.Forms.Padding(0)
        Me.FraButton.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraButton.Size = New System.Drawing.Size(327, 49)
        Me.FraButton.TabIndex = 13
        Me.FraButton.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(6, 10)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 18
        '
        'lblDNCNSeqType
        '
        Me.lblDNCNSeqType.BackColor = System.Drawing.SystemColors.Control
        Me.lblDNCNSeqType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDNCNSeqType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDNCNSeqType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDNCNSeqType.Location = New System.Drawing.Point(118, 18)
        Me.lblDNCNSeqType.Name = "lblDNCNSeqType"
        Me.lblDNCNSeqType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDNCNSeqType.Size = New System.Drawing.Size(67, 15)
        Me.lblDNCNSeqType.TabIndex = 38
        Me.lblDNCNSeqType.Text = "lblDNCNSeqType"
        '
        'OptItem
        '
        '
        'optBaseOn
        '
        '
        'optCustomer
        '
        '
        'optRate
        '
        '
        'optType
        '
        '
        'FrmDrNoteProcess
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(328, 411)
        Me.Controls.Add(Me.Frame8)
        Me.Controls.Add(Me.Frame7)
        Me.Controls.Add(Me.Frame6)
        Me.Controls.Add(Me.Frame5)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.FraButton)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmDrNoteProcess"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds
        Me.Text = "Debit Note Process (Rate Diff After PO Amend)"
        Me.Frame8.ResumeLayout(False)
        Me.Frame8.PerformLayout()
        Me.Frame7.ResumeLayout(False)
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me.Frame5.ResumeLayout(False)
        Me.Frame5.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.FraButton.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optAgt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optBaseOn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class