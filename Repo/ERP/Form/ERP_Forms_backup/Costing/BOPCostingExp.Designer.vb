Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmBOPCostingExp
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
        '
        '
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
	Public WithEvents _optAdd_Deduct_1 As System.Windows.Forms.RadioButton
	Public WithEvents _optAdd_Deduct_0 As System.Windows.Forms.RadioButton
	Public WithEvents Frame1 As System.Windows.Forms.GroupBox
	Public WithEvents txtSeq As System.Windows.Forms.TextBox
	Public WithEvents _chkCalcOn_5 As System.Windows.Forms.CheckBox
	Public WithEvents _chkCalcOn_4 As System.Windows.Forms.CheckBox
	Public WithEvents _chkCalcOn_3 As System.Windows.Forms.CheckBox
	Public WithEvents _chkCalcOn_2 As System.Windows.Forms.CheckBox
	Public WithEvents _chkCalcOn_1 As System.Windows.Forms.CheckBox
	Public WithEvents _chkCalcOn_0 As System.Windows.Forms.CheckBox
	Public WithEvents FraCalc As System.Windows.Forms.GroupBox
	Public WithEvents TxtDefaultPer As System.Windows.Forms.TextBox
	Public WithEvents cmdsearch As System.Windows.Forms.Button
	Public WithEvents txtName As System.Windows.Forms.TextBox
	Public WithEvents TxtCode As System.Windows.Forms.TextBox
	Public WithEvents lblCategory As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents Label4 As System.Windows.Forms.Label
	Public WithEvents _lblLabels_0 As System.Windows.Forms.Label
	Public WithEvents FraView As System.Windows.Forms.GroupBox
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents CmdClose As System.Windows.Forms.Button
	Public WithEvents CmdView As System.Windows.Forms.Button
	Public WithEvents cmdPreview As System.Windows.Forms.Button
	Public WithEvents cmdPrint As System.Windows.Forms.Button
	Public WithEvents CmdDelete As System.Windows.Forms.Button
	Public WithEvents cmdSavePrint As System.Windows.Forms.Button
	Public WithEvents CmdSave As System.Windows.Forms.Button
	Public WithEvents CmdModify As System.Windows.Forms.Button
	Public WithEvents CmdAdd As System.Windows.Forms.Button
	Public WithEvents Report1 As AxCrystal.AxCrystalReport
	Public WithEvents FraMovement As System.Windows.Forms.GroupBox
	Public WithEvents chkCalcOn As Microsoft.VisualBasic.Compatibility.VB6.CheckBoxArray
	Public WithEvents lblLabels As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
	Public WithEvents optAdd_Deduct As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBOPCostingExp))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.cmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.FraView = New System.Windows.Forms.GroupBox()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me._optAdd_Deduct_1 = New System.Windows.Forms.RadioButton()
        Me._optAdd_Deduct_0 = New System.Windows.Forms.RadioButton()
        Me.txtSeq = New System.Windows.Forms.TextBox()
        Me.FraCalc = New System.Windows.Forms.GroupBox()
        Me._chkCalcOn_5 = New System.Windows.Forms.CheckBox()
        Me._chkCalcOn_4 = New System.Windows.Forms.CheckBox()
        Me._chkCalcOn_3 = New System.Windows.Forms.CheckBox()
        Me._chkCalcOn_2 = New System.Windows.Forms.CheckBox()
        Me._chkCalcOn_1 = New System.Windows.Forms.CheckBox()
        Me._chkCalcOn_0 = New System.Windows.Forms.CheckBox()
        Me.TxtDefaultPer = New System.Windows.Forms.TextBox()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.TxtCode = New System.Windows.Forms.TextBox()
        Me.lblCategory = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me._lblLabels_0 = New System.Windows.Forms.Label()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.chkCalcOn = New Microsoft.VisualBasic.Compatibility.VB6.CheckBoxArray(Me.components)
        Me.lblLabels = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.optAdd_Deduct = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.FraView.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.FraCalc.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCalcOn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLabels, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optAdd_Deduct, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdsearch
        '
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.Location = New System.Drawing.Point(382, 12)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(27, 19)
        Me.cmdsearch.TabIndex = 3
        Me.cmdsearch.TabStop = False
        Me.cmdsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearch, "Search")
        Me.cmdsearch.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(474, 12)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(60, 34)
        Me.CmdClose.TabIndex = 18
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'CmdView
        '
        Me.CmdView.BackColor = System.Drawing.SystemColors.Control
        Me.CmdView.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdView.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
        Me.CmdView.Location = New System.Drawing.Point(414, 12)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(60, 34)
        Me.CmdView.TabIndex = 17
        Me.CmdView.Text = "List &View"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdView, "View List")
        Me.CmdView.UseVisualStyleBackColor = False
        '
        'cmdPreview
        '
        Me.cmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPreview.Image = CType(resources.GetObject("cmdPreview.Image"), System.Drawing.Image)
        Me.cmdPreview.Location = New System.Drawing.Point(364, 12)
        Me.cmdPreview.Name = "cmdPreview"
        Me.cmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPreview.Size = New System.Drawing.Size(51, 33)
        Me.cmdPreview.TabIndex = 20
        Me.cmdPreview.Text = "Preview"
        Me.cmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPreview, "Print Preview")
        Me.cmdPreview.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(304, 12)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(60, 34)
        Me.cmdPrint.TabIndex = 19
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print Record")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'CmdDelete
        '
        Me.CmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.CmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdDelete.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdDelete.Image = CType(resources.GetObject("CmdDelete.Image"), System.Drawing.Image)
        Me.CmdDelete.Location = New System.Drawing.Point(244, 12)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(60, 34)
        Me.CmdDelete.TabIndex = 15
        Me.CmdDelete.Text = "&Delete"
        Me.CmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdDelete, "Delete Record")
        Me.CmdDelete.UseVisualStyleBackColor = False
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.Location = New System.Drawing.Point(188, 12)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(57, 33)
        Me.cmdSavePrint.TabIndex = 21
        Me.cmdSavePrint.Text = "SavePrint"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save & Print Record")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.Location = New System.Drawing.Point(128, 12)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(60, 34)
        Me.CmdSave.TabIndex = 16
        Me.CmdSave.Text = "&Save"
        Me.CmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSave, "Save Record")
        Me.CmdSave.UseVisualStyleBackColor = False
        '
        'CmdModify
        '
        Me.CmdModify.BackColor = System.Drawing.SystemColors.Control
        Me.CmdModify.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdModify.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdModify.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdModify.Image = CType(resources.GetObject("CmdModify.Image"), System.Drawing.Image)
        Me.CmdModify.Location = New System.Drawing.Point(68, 12)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(60, 34)
        Me.CmdModify.TabIndex = 14
        Me.CmdModify.Text = "&Modify"
        Me.CmdModify.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdModify, "Modify Record")
        Me.CmdModify.UseVisualStyleBackColor = False
        '
        'CmdAdd
        '
        Me.CmdAdd.BackColor = System.Drawing.SystemColors.Control
        Me.CmdAdd.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdAdd.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdAdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdAdd.Image = CType(resources.GetObject("CmdAdd.Image"), System.Drawing.Image)
        Me.CmdAdd.Location = New System.Drawing.Point(8, 12)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(60, 34)
        Me.CmdAdd.TabIndex = 0
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'FraView
        '
        Me.FraView.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.FraView.Controls.Add(Me.Frame1)
        Me.FraView.Controls.Add(Me.txtSeq)
        Me.FraView.Controls.Add(Me.FraCalc)
        Me.FraView.Controls.Add(Me.TxtDefaultPer)
        Me.FraView.Controls.Add(Me.cmdsearch)
        Me.FraView.Controls.Add(Me.txtName)
        Me.FraView.Controls.Add(Me.TxtCode)
        Me.FraView.Controls.Add(Me.lblCategory)
        Me.FraView.Controls.Add(Me.Label1)
        Me.FraView.Controls.Add(Me.Label4)
        Me.FraView.Controls.Add(Me._lblLabels_0)
        Me.FraView.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraView.Location = New System.Drawing.Point(0, -6)
        Me.FraView.Name = "FraView"
        Me.FraView.Padding = New System.Windows.Forms.Padding(0)
        Me.FraView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraView.Size = New System.Drawing.Size(539, 211)
        Me.FraView.TabIndex = 1
        Me.FraView.TabStop = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me._optAdd_Deduct_1)
        Me.Frame1.Controls.Add(Me._optAdd_Deduct_0)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(338, 34)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(201, 39)
        Me.Frame1.TabIndex = 27
        Me.Frame1.TabStop = False
        '
        '_optAdd_Deduct_1
        '
        Me._optAdd_Deduct_1.AutoSize = True
        Me._optAdd_Deduct_1.BackColor = System.Drawing.SystemColors.Control
        Me._optAdd_Deduct_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optAdd_Deduct_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optAdd_Deduct_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optAdd_Deduct.SetIndex(Me._optAdd_Deduct_1, CType(1, Short))
        Me._optAdd_Deduct_1.Location = New System.Drawing.Point(116, 16)
        Me._optAdd_Deduct_1.Name = "_optAdd_Deduct_1"
        Me._optAdd_Deduct_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optAdd_Deduct_1.Size = New System.Drawing.Size(63, 18)
        Me._optAdd_Deduct_1.TabIndex = 29
        Me._optAdd_Deduct_1.TabStop = True
        Me._optAdd_Deduct_1.Text = "Deduct"
        Me._optAdd_Deduct_1.UseVisualStyleBackColor = False
        '
        '_optAdd_Deduct_0
        '
        Me._optAdd_Deduct_0.AutoSize = True
        Me._optAdd_Deduct_0.BackColor = System.Drawing.SystemColors.Control
        Me._optAdd_Deduct_0.Checked = True
        Me._optAdd_Deduct_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optAdd_Deduct_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optAdd_Deduct_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optAdd_Deduct.SetIndex(Me._optAdd_Deduct_0, CType(0, Short))
        Me._optAdd_Deduct_0.Location = New System.Drawing.Point(8, 16)
        Me._optAdd_Deduct_0.Name = "_optAdd_Deduct_0"
        Me._optAdd_Deduct_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optAdd_Deduct_0.Size = New System.Drawing.Size(47, 18)
        Me._optAdd_Deduct_0.TabIndex = 28
        Me._optAdd_Deduct_0.TabStop = True
        Me._optAdd_Deduct_0.Text = "Add"
        Me._optAdd_Deduct_0.UseVisualStyleBackColor = False
        '
        'txtSeq
        '
        Me.txtSeq.AcceptsReturn = True
        Me.txtSeq.BackColor = System.Drawing.SystemColors.Window
        Me.txtSeq.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSeq.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSeq.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSeq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSeq.Location = New System.Drawing.Point(96, 56)
        Me.txtSeq.MaxLength = 0
        Me.txtSeq.Name = "txtSeq"
        Me.txtSeq.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSeq.Size = New System.Drawing.Size(73, 20)
        Me.txtSeq.TabIndex = 25
        '
        'FraCalc
        '
        Me.FraCalc.BackColor = System.Drawing.SystemColors.Control
        Me.FraCalc.Controls.Add(Me._chkCalcOn_5)
        Me.FraCalc.Controls.Add(Me._chkCalcOn_4)
        Me.FraCalc.Controls.Add(Me._chkCalcOn_3)
        Me.FraCalc.Controls.Add(Me._chkCalcOn_2)
        Me.FraCalc.Controls.Add(Me._chkCalcOn_1)
        Me.FraCalc.Controls.Add(Me._chkCalcOn_0)
        Me.FraCalc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraCalc.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraCalc.Location = New System.Drawing.Point(2, 110)
        Me.FraCalc.Name = "FraCalc"
        Me.FraCalc.Padding = New System.Windows.Forms.Padding(0)
        Me.FraCalc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraCalc.Size = New System.Drawing.Size(535, 101)
        Me.FraCalc.TabIndex = 23
        Me.FraCalc.TabStop = False
        Me.FraCalc.Text = "Calculation"
        '
        '_chkCalcOn_5
        '
        Me._chkCalcOn_5.BackColor = System.Drawing.SystemColors.Control
        Me._chkCalcOn_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkCalcOn_5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkCalcOn_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCalcOn.SetIndex(Me._chkCalcOn_5, CType(5, Short))
        Me._chkCalcOn_5.Location = New System.Drawing.Point(186, 80)
        Me._chkCalcOn_5.Name = "_chkCalcOn_5"
        Me._chkCalcOn_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkCalcOn_5.Size = New System.Drawing.Size(195, 17)
        Me._chkCalcOn_5.TabIndex = 10
        Me._chkCalcOn_5.Text = "None"
        Me._chkCalcOn_5.UseVisualStyleBackColor = False
        '
        '_chkCalcOn_4
        '
        Me._chkCalcOn_4.BackColor = System.Drawing.SystemColors.Control
        Me._chkCalcOn_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkCalcOn_4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkCalcOn_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCalcOn.SetIndex(Me._chkCalcOn_4, CType(4, Short))
        Me._chkCalcOn_4.Location = New System.Drawing.Point(186, 66)
        Me._chkCalcOn_4.Name = "_chkCalcOn_4"
        Me._chkCalcOn_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkCalcOn_4.Size = New System.Drawing.Size(195, 17)
        Me._chkCalcOn_4.TabIndex = 9
        Me._chkCalcOn_4.Text = "Operation"
        Me._chkCalcOn_4.UseVisualStyleBackColor = False
        '
        '_chkCalcOn_3
        '
        Me._chkCalcOn_3.BackColor = System.Drawing.SystemColors.Control
        Me._chkCalcOn_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkCalcOn_3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkCalcOn_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCalcOn.SetIndex(Me._chkCalcOn_3, CType(3, Short))
        Me._chkCalcOn_3.Location = New System.Drawing.Point(186, 52)
        Me._chkCalcOn_3.Name = "_chkCalcOn_3"
        Me._chkCalcOn_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkCalcOn_3.Size = New System.Drawing.Size(195, 17)
        Me._chkCalcOn_3.TabIndex = 8
        Me._chkCalcOn_3.Text = "Process (B)"
        Me._chkCalcOn_3.UseVisualStyleBackColor = False
        '
        '_chkCalcOn_2
        '
        Me._chkCalcOn_2.BackColor = System.Drawing.SystemColors.Control
        Me._chkCalcOn_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkCalcOn_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkCalcOn_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCalcOn.SetIndex(Me._chkCalcOn_2, CType(2, Short))
        Me._chkCalcOn_2.Location = New System.Drawing.Point(186, 38)
        Me._chkCalcOn_2.Name = "_chkCalcOn_2"
        Me._chkCalcOn_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkCalcOn_2.Size = New System.Drawing.Size(195, 17)
        Me._chkCalcOn_2.TabIndex = 7
        Me._chkCalcOn_2.Text = "Process (A)"
        Me._chkCalcOn_2.UseVisualStyleBackColor = False
        '
        '_chkCalcOn_1
        '
        Me._chkCalcOn_1.BackColor = System.Drawing.SystemColors.Control
        Me._chkCalcOn_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkCalcOn_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkCalcOn_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCalcOn.SetIndex(Me._chkCalcOn_1, CType(1, Short))
        Me._chkCalcOn_1.Location = New System.Drawing.Point(186, 24)
        Me._chkCalcOn_1.Name = "_chkCalcOn_1"
        Me._chkCalcOn_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkCalcOn_1.Size = New System.Drawing.Size(195, 17)
        Me._chkCalcOn_1.TabIndex = 6
        Me._chkCalcOn_1.Text = "BOP Other Parts"
        Me._chkCalcOn_1.UseVisualStyleBackColor = False
        '
        '_chkCalcOn_0
        '
        Me._chkCalcOn_0.BackColor = System.Drawing.SystemColors.Control
        Me._chkCalcOn_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkCalcOn_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkCalcOn_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCalcOn.SetIndex(Me._chkCalcOn_0, CType(0, Short))
        Me._chkCalcOn_0.Location = New System.Drawing.Point(186, 10)
        Me._chkCalcOn_0.Name = "_chkCalcOn_0"
        Me._chkCalcOn_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkCalcOn_0.Size = New System.Drawing.Size(195, 17)
        Me._chkCalcOn_0.TabIndex = 5
        Me._chkCalcOn_0.Text = "RM/ BOP Cost"
        Me._chkCalcOn_0.UseVisualStyleBackColor = False
        '
        'TxtDefaultPer
        '
        Me.TxtDefaultPer.AcceptsReturn = True
        Me.TxtDefaultPer.BackColor = System.Drawing.SystemColors.Window
        Me.TxtDefaultPer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtDefaultPer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtDefaultPer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDefaultPer.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtDefaultPer.Location = New System.Drawing.Point(96, 34)
        Me.TxtDefaultPer.MaxLength = 0
        Me.TxtDefaultPer.Name = "TxtDefaultPer"
        Me.TxtDefaultPer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtDefaultPer.Size = New System.Drawing.Size(73, 20)
        Me.TxtDefaultPer.TabIndex = 4
        '
        'txtName
        '
        Me.txtName.AcceptsReturn = True
        Me.txtName.BackColor = System.Drawing.SystemColors.Window
        Me.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtName.Location = New System.Drawing.Point(96, 12)
        Me.txtName.MaxLength = 0
        Me.txtName.Name = "txtName"
        Me.txtName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtName.Size = New System.Drawing.Size(285, 20)
        Me.txtName.TabIndex = 2
        '
        'TxtCode
        '
        Me.TxtCode.AcceptsReturn = True
        Me.TxtCode.BackColor = System.Drawing.SystemColors.Window
        Me.TxtCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtCode.Location = New System.Drawing.Point(275, 12)
        Me.TxtCode.MaxLength = 0
        Me.TxtCode.Name = "TxtCode"
        Me.TxtCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtCode.Size = New System.Drawing.Size(19, 20)
        Me.TxtCode.TabIndex = 11
        Me.TxtCode.Text = "Text1"
        Me.TxtCode.Visible = False
        '
        'lblCategory
        '
        Me.lblCategory.BackColor = System.Drawing.SystemColors.Control
        Me.lblCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCategory.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCategory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCategory.Location = New System.Drawing.Point(434, 14)
        Me.lblCategory.Name = "lblCategory"
        Me.lblCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCategory.Size = New System.Drawing.Size(67, 15)
        Me.lblCategory.TabIndex = 30
        Me.lblCategory.Text = "lblCategory"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(22, 58)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(79, 17)
        Me.Label1.TabIndex = 26
        Me.Label1.Text = "Sequence"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(22, 36)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(79, 17)
        Me.Label4.TabIndex = 22
        Me.Label4.Text = "Default %"
        '
        '_lblLabels_0
        '
        Me._lblLabels_0.AutoSize = True
        Me._lblLabels_0.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_0, CType(0, Short))
        Me._lblLabels_0.Location = New System.Drawing.Point(22, 16)
        Me._lblLabels_0.Name = "_lblLabels_0"
        Me._lblLabels_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_0.Size = New System.Drawing.Size(38, 14)
        Me._lblLabels_0.TabIndex = 12
        Me._lblLabels_0.Text = "Name"
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(539, 205)
        Me.SprdView.TabIndex = 24
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.CmdView)
        Me.FraMovement.Controls.Add(Me.cmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.CmdDelete)
        Me.FraMovement.Controls.Add(Me.cmdSavePrint)
        Me.FraMovement.Controls.Add(Me.CmdSave)
        Me.FraMovement.Controls.Add(Me.CmdModify)
        Me.FraMovement.Controls.Add(Me.CmdAdd)
        Me.FraMovement.Controls.Add(Me.Report1)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 200)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(539, 49)
        Me.FraMovement.TabIndex = 13
        Me.FraMovement.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(30, 14)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 22
        '
        'chkCalcOn
        '
        '
        'optAdd_Deduct
        '
        '
        'frmBOPCostingExp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(540, 249)
        Me.Controls.Add(Me.FraView)
        Me.Controls.Add(Me.SprdView)
        Me.Controls.Add(Me.FraMovement)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(74, 23)
        Me.MaximizeBox = False
        Me.Name = "frmBOPCostingExp"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "BOP Costing Exp"
        Me.FraView.ResumeLayout(False)
        Me.FraView.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.FraCalc.ResumeLayout(False)
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCalcOn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLabels, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optAdd_Deduct, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        'SprdView.DataSource = CType(ADataGrid, MSDATASRC.DataSource)
    End Sub
	Public Sub VB6_RemoveADODataBinding()
		SprdView.DataSource = Nothing
	End Sub
#End Region 
End Class