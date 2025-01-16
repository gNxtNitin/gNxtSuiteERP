Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmToolMaster
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
        'Me.MDIParent = HemaProduction.Master
        'HemaProduction.Master.Show
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
	Public WithEvents txtPcsNoStroke As System.Windows.Forms.TextBox
	Public WithEvents txtOPProduction As System.Windows.Forms.TextBox
	Public WithEvents cboUnit As System.Windows.Forms.ComboBox
	Public WithEvents txtProducedQty As System.Windows.Forms.TextBox
	Public WithEvents txtToolItemName As System.Windows.Forms.TextBox
	Public WithEvents txtToolItemCode As System.Windows.Forms.TextBox
	Public WithEvents txtDeptDesc As System.Windows.Forms.TextBox
	Public WithEvents chkToolUB As System.Windows.Forms.CheckBox
	Public WithEvents cboToolFreq As System.Windows.Forms.ComboBox
	Public WithEvents cmdSearchToolItem As System.Windows.Forms.Button
	Public WithEvents txtItemCode As System.Windows.Forms.TextBox
	Public WithEvents txtMasterToolNo As System.Windows.Forms.TextBox
	Public WithEvents cmdSearchMasterToolNo As System.Windows.Forms.Button
	Public WithEvents txtToolLife As System.Windows.Forms.TextBox
	Public WithEvents txtToolPrdQty As System.Windows.Forms.TextBox
	Public WithEvents txtToolPreventiveQty As System.Windows.Forms.TextBox
	Public WithEvents cmdSearchOPR As System.Windows.Forms.Button
	Public WithEvents txtOprCode As System.Windows.Forms.TextBox
	Public WithEvents txtLocation As System.Windows.Forms.TextBox
	Public WithEvents txtDrgNo As System.Windows.Forms.TextBox
	Public WithEvents txtOprDesc As System.Windows.Forms.TextBox
	Public WithEvents txtDeptCode As System.Windows.Forms.TextBox
	Public WithEvents cmdSearchDept As System.Windows.Forms.Button
	Public WithEvents cboToolStatus As System.Windows.Forms.ComboBox
	Public WithEvents cmdSearchItem As System.Windows.Forms.Button
	Public WithEvents cmdSearchToolNo As System.Windows.Forms.Button
	Public WithEvents txtToolNo As System.Windows.Forms.TextBox
	Public WithEvents txtRemarks As System.Windows.Forms.TextBox
	Public WithEvents txtItemName As System.Windows.Forms.TextBox
	Public WithEvents txtToolManuDate As System.Windows.Forms.MaskedTextBox
	Public WithEvents txtToolLoadDate As System.Windows.Forms.MaskedTextBox
	Public WithEvents txtToolLoadTime As System.Windows.Forms.MaskedTextBox
	Public WithEvents txtOpAsOnDate As System.Windows.Forms.MaskedTextBox
	Public WithEvents Label21 As System.Windows.Forms.Label
	Public WithEvents Label19 As System.Windows.Forms.Label
	Public WithEvents Label12 As System.Windows.Forms.Label
	Public WithEvents Label11 As System.Windows.Forms.Label
	Public WithEvents Label5 As System.Windows.Forms.Label
	Public WithEvents Label20 As System.Windows.Forms.Label
	Public WithEvents Label18 As System.Windows.Forms.Label
	Public WithEvents Label3 As System.Windows.Forms.Label
	Public WithEvents Label17 As System.Windows.Forms.Label
	Public WithEvents Label16 As System.Windows.Forms.Label
	Public WithEvents Label15 As System.Windows.Forms.Label
	Public WithEvents Label14 As System.Windows.Forms.Label
	Public WithEvents Label13 As System.Windows.Forms.Label
	Public WithEvents Label10 As System.Windows.Forms.Label
	Public WithEvents Label9 As System.Windows.Forms.Label
	Public WithEvents Label6 As System.Windows.Forms.Label
	Public WithEvents Label4 As System.Windows.Forms.Label
	Public WithEvents _lblUnder_20 As System.Windows.Forms.Label
	Public WithEvents Label7 As System.Windows.Forms.Label
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents Label8 As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents fraTop As System.Windows.Forms.GroupBox
	Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
	Public WithEvents ADataGrid As VB6.ADODC
	Public WithEvents Report1 As AxCrystal.AxCrystalReport
	Public WithEvents CmdModify As System.Windows.Forms.Button
	Public WithEvents cmdPreview As System.Windows.Forms.Button
	Public WithEvents cmdSavePrint As System.Windows.Forms.Button
	Public WithEvents CmdAdd As System.Windows.Forms.Button
	Public WithEvents CmdDelete As System.Windows.Forms.Button
	Public WithEvents CmdView As System.Windows.Forms.Button
	Public WithEvents CmdClose As System.Windows.Forms.Button
	Public WithEvents cmdPrint As System.Windows.Forms.Button
	Public WithEvents CmdSave As System.Windows.Forms.Button
	Public WithEvents FraMovement As System.Windows.Forms.GroupBox
	Public WithEvents lblUnder As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmToolMaster))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearchToolItem = New System.Windows.Forms.Button()
        Me.cmdSearchMasterToolNo = New System.Windows.Forms.Button()
        Me.cmdSearchOPR = New System.Windows.Forms.Button()
        Me.cmdSearchDept = New System.Windows.Forms.Button()
        Me.cmdSearchItem = New System.Windows.Forms.Button()
        Me.cmdSearchToolNo = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.fraTop = New System.Windows.Forms.GroupBox()
        Me.txtPcsNoStroke = New System.Windows.Forms.TextBox()
        Me.txtOPProduction = New System.Windows.Forms.TextBox()
        Me.cboUnit = New System.Windows.Forms.ComboBox()
        Me.txtProducedQty = New System.Windows.Forms.TextBox()
        Me.txtToolItemName = New System.Windows.Forms.TextBox()
        Me.txtToolItemCode = New System.Windows.Forms.TextBox()
        Me.txtDeptDesc = New System.Windows.Forms.TextBox()
        Me.chkToolUB = New System.Windows.Forms.CheckBox()
        Me.cboToolFreq = New System.Windows.Forms.ComboBox()
        Me.txtItemCode = New System.Windows.Forms.TextBox()
        Me.txtMasterToolNo = New System.Windows.Forms.TextBox()
        Me.txtToolLife = New System.Windows.Forms.TextBox()
        Me.txtToolPrdQty = New System.Windows.Forms.TextBox()
        Me.txtToolPreventiveQty = New System.Windows.Forms.TextBox()
        Me.txtOprCode = New System.Windows.Forms.TextBox()
        Me.txtLocation = New System.Windows.Forms.TextBox()
        Me.txtDrgNo = New System.Windows.Forms.TextBox()
        Me.txtOprDesc = New System.Windows.Forms.TextBox()
        Me.txtDeptCode = New System.Windows.Forms.TextBox()
        Me.cboToolStatus = New System.Windows.Forms.ComboBox()
        Me.txtToolNo = New System.Windows.Forms.TextBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtItemName = New System.Windows.Forms.TextBox()
        Me.txtToolManuDate = New System.Windows.Forms.MaskedTextBox()
        Me.txtToolLoadDate = New System.Windows.Forms.MaskedTextBox()
        Me.txtToolLoadTime = New System.Windows.Forms.MaskedTextBox()
        Me.txtOpAsOnDate = New System.Windows.Forms.MaskedTextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me._lblUnder_20 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.ADataGrid = New Microsoft.VisualBasic.Compatibility.VB6.ADODC()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.cmdPreview = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.lblUnder = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.fraTop.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.lblUnder, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSearchToolItem
        '
        Me.cmdSearchToolItem.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchToolItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchToolItem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchToolItem.Image = CType(resources.GetObject("cmdSearchToolItem.Image"), System.Drawing.Image)
        Me.cmdSearchToolItem.Location = New System.Drawing.Point(235, 38)
        Me.cmdSearchToolItem.Name = "cmdSearchToolItem"
        Me.cmdSearchToolItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchToolItem.Size = New System.Drawing.Size(27, 19)
        Me.cmdSearchToolItem.TabIndex = 3
        Me.cmdSearchToolItem.TabStop = False
        Me.cmdSearchToolItem.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchToolItem, "Search")
        Me.cmdSearchToolItem.UseVisualStyleBackColor = False
        '
        'cmdSearchMasterToolNo
        '
        Me.cmdSearchMasterToolNo.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchMasterToolNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchMasterToolNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchMasterToolNo.Image = CType(resources.GetObject("cmdSearchMasterToolNo.Image"), System.Drawing.Image)
        Me.cmdSearchMasterToolNo.Location = New System.Drawing.Point(235, 230)
        Me.cmdSearchMasterToolNo.Name = "cmdSearchMasterToolNo"
        Me.cmdSearchMasterToolNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchMasterToolNo.Size = New System.Drawing.Size(27, 19)
        Me.cmdSearchMasterToolNo.TabIndex = 20
        Me.cmdSearchMasterToolNo.TabStop = False
        Me.cmdSearchMasterToolNo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchMasterToolNo, "Search")
        Me.cmdSearchMasterToolNo.UseVisualStyleBackColor = False
        '
        'cmdSearchOPR
        '
        Me.cmdSearchOPR.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchOPR.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchOPR.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchOPR.Image = CType(resources.GetObject("cmdSearchOPR.Image"), System.Drawing.Image)
        Me.cmdSearchOPR.Location = New System.Drawing.Point(235, 110)
        Me.cmdSearchOPR.Name = "cmdSearchOPR"
        Me.cmdSearchOPR.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchOPR.Size = New System.Drawing.Size(27, 19)
        Me.cmdSearchOPR.TabIndex = 12
        Me.cmdSearchOPR.TabStop = False
        Me.cmdSearchOPR.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchOPR, "Search")
        Me.cmdSearchOPR.UseVisualStyleBackColor = False
        '
        'cmdSearchDept
        '
        Me.cmdSearchDept.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchDept.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchDept.Image = CType(resources.GetObject("cmdSearchDept.Image"), System.Drawing.Image)
        Me.cmdSearchDept.Location = New System.Drawing.Point(235, 62)
        Me.cmdSearchDept.Name = "cmdSearchDept"
        Me.cmdSearchDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchDept.Size = New System.Drawing.Size(27, 19)
        Me.cmdSearchDept.TabIndex = 6
        Me.cmdSearchDept.TabStop = False
        Me.cmdSearchDept.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchDept, "Search")
        Me.cmdSearchDept.UseVisualStyleBackColor = False
        '
        'cmdSearchItem
        '
        Me.cmdSearchItem.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchItem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchItem.Image = CType(resources.GetObject("cmdSearchItem.Image"), System.Drawing.Image)
        Me.cmdSearchItem.Location = New System.Drawing.Point(235, 88)
        Me.cmdSearchItem.Name = "cmdSearchItem"
        Me.cmdSearchItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchItem.Size = New System.Drawing.Size(27, 19)
        Me.cmdSearchItem.TabIndex = 9
        Me.cmdSearchItem.TabStop = False
        Me.cmdSearchItem.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchItem, "Search")
        Me.cmdSearchItem.UseVisualStyleBackColor = False
        '
        'cmdSearchToolNo
        '
        Me.cmdSearchToolNo.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchToolNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchToolNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchToolNo.Image = CType(resources.GetObject("cmdSearchToolNo.Image"), System.Drawing.Image)
        Me.cmdSearchToolNo.Location = New System.Drawing.Point(235, 14)
        Me.cmdSearchToolNo.Name = "cmdSearchToolNo"
        Me.cmdSearchToolNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchToolNo.Size = New System.Drawing.Size(27, 19)
        Me.cmdSearchToolNo.TabIndex = 1
        Me.cmdSearchToolNo.TabStop = False
        Me.cmdSearchToolNo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchToolNo, "Search")
        Me.cmdSearchToolNo.UseVisualStyleBackColor = False
        '
        'CmdModify
        '
        Me.CmdModify.BackColor = System.Drawing.SystemColors.Control
        Me.CmdModify.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdModify.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdModify.Image = CType(resources.GetObject("CmdModify.Image"), System.Drawing.Image)
        Me.CmdModify.Location = New System.Drawing.Point(86, 12)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(60, 37)
        Me.CmdModify.TabIndex = 35
        Me.CmdModify.Text = "&Modify"
        Me.CmdModify.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdModify, "Modify Record")
        Me.CmdModify.UseVisualStyleBackColor = False
        '
        'CmdAdd
        '
        Me.CmdAdd.BackColor = System.Drawing.SystemColors.Control
        Me.CmdAdd.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdAdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdAdd.Image = CType(resources.GetObject("CmdAdd.Image"), System.Drawing.Image)
        Me.CmdAdd.Location = New System.Drawing.Point(26, 12)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(60, 37)
        Me.CmdAdd.TabIndex = 33
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'CmdDelete
        '
        Me.CmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.CmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdDelete.Image = CType(resources.GetObject("CmdDelete.Image"), System.Drawing.Image)
        Me.CmdDelete.Location = New System.Drawing.Point(266, 12)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(60, 37)
        Me.CmdDelete.TabIndex = 38
        Me.CmdDelete.Text = "&Delete"
        Me.CmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdDelete, "Delete Record")
        Me.CmdDelete.UseVisualStyleBackColor = False
        '
        'CmdView
        '
        Me.CmdView.BackColor = System.Drawing.SystemColors.Control
        Me.CmdView.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
        Me.CmdView.Location = New System.Drawing.Point(446, 12)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(60, 37)
        Me.CmdView.TabIndex = 41
        Me.CmdView.Text = "List &View"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdView, "View List")
        Me.CmdView.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(506, 12)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(60, 37)
        Me.CmdClose.TabIndex = 42
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(326, 12)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(60, 37)
        Me.cmdPrint.TabIndex = 39
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print List")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.Location = New System.Drawing.Point(146, 12)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(60, 37)
        Me.CmdSave.TabIndex = 36
        Me.CmdSave.Text = "&Save"
        Me.CmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSave, "Save Record")
        Me.CmdSave.UseVisualStyleBackColor = False
        '
        'fraTop
        '
        Me.fraTop.BackColor = System.Drawing.SystemColors.Control
        Me.fraTop.Controls.Add(Me.txtPcsNoStroke)
        Me.fraTop.Controls.Add(Me.txtOPProduction)
        Me.fraTop.Controls.Add(Me.cboUnit)
        Me.fraTop.Controls.Add(Me.txtProducedQty)
        Me.fraTop.Controls.Add(Me.txtToolItemName)
        Me.fraTop.Controls.Add(Me.txtToolItemCode)
        Me.fraTop.Controls.Add(Me.txtDeptDesc)
        Me.fraTop.Controls.Add(Me.chkToolUB)
        Me.fraTop.Controls.Add(Me.cboToolFreq)
        Me.fraTop.Controls.Add(Me.cmdSearchToolItem)
        Me.fraTop.Controls.Add(Me.txtItemCode)
        Me.fraTop.Controls.Add(Me.txtMasterToolNo)
        Me.fraTop.Controls.Add(Me.cmdSearchMasterToolNo)
        Me.fraTop.Controls.Add(Me.txtToolLife)
        Me.fraTop.Controls.Add(Me.txtToolPrdQty)
        Me.fraTop.Controls.Add(Me.txtToolPreventiveQty)
        Me.fraTop.Controls.Add(Me.cmdSearchOPR)
        Me.fraTop.Controls.Add(Me.txtOprCode)
        Me.fraTop.Controls.Add(Me.txtLocation)
        Me.fraTop.Controls.Add(Me.txtDrgNo)
        Me.fraTop.Controls.Add(Me.txtOprDesc)
        Me.fraTop.Controls.Add(Me.txtDeptCode)
        Me.fraTop.Controls.Add(Me.cmdSearchDept)
        Me.fraTop.Controls.Add(Me.cboToolStatus)
        Me.fraTop.Controls.Add(Me.cmdSearchItem)
        Me.fraTop.Controls.Add(Me.cmdSearchToolNo)
        Me.fraTop.Controls.Add(Me.txtToolNo)
        Me.fraTop.Controls.Add(Me.txtRemarks)
        Me.fraTop.Controls.Add(Me.txtItemName)
        Me.fraTop.Controls.Add(Me.txtToolManuDate)
        Me.fraTop.Controls.Add(Me.txtToolLoadDate)
        Me.fraTop.Controls.Add(Me.txtToolLoadTime)
        Me.fraTop.Controls.Add(Me.txtOpAsOnDate)
        Me.fraTop.Controls.Add(Me.Label21)
        Me.fraTop.Controls.Add(Me.Label19)
        Me.fraTop.Controls.Add(Me.Label12)
        Me.fraTop.Controls.Add(Me.Label11)
        Me.fraTop.Controls.Add(Me.Label5)
        Me.fraTop.Controls.Add(Me.Label20)
        Me.fraTop.Controls.Add(Me.Label18)
        Me.fraTop.Controls.Add(Me.Label3)
        Me.fraTop.Controls.Add(Me.Label17)
        Me.fraTop.Controls.Add(Me.Label16)
        Me.fraTop.Controls.Add(Me.Label15)
        Me.fraTop.Controls.Add(Me.Label14)
        Me.fraTop.Controls.Add(Me.Label13)
        Me.fraTop.Controls.Add(Me.Label10)
        Me.fraTop.Controls.Add(Me.Label9)
        Me.fraTop.Controls.Add(Me.Label6)
        Me.fraTop.Controls.Add(Me.Label4)
        Me.fraTop.Controls.Add(Me._lblUnder_20)
        Me.fraTop.Controls.Add(Me.Label7)
        Me.fraTop.Controls.Add(Me.Label2)
        Me.fraTop.Controls.Add(Me.Label8)
        Me.fraTop.Controls.Add(Me.Label1)
        Me.fraTop.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraTop.Location = New System.Drawing.Point(0, -4)
        Me.fraTop.Name = "fraTop"
        Me.fraTop.Padding = New System.Windows.Forms.Padding(0)
        Me.fraTop.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraTop.Size = New System.Drawing.Size(591, 391)
        Me.fraTop.TabIndex = 44
        Me.fraTop.TabStop = False
        '
        'txtPcsNoStroke
        '
        Me.txtPcsNoStroke.AcceptsReturn = True
        Me.txtPcsNoStroke.BackColor = System.Drawing.SystemColors.Window
        Me.txtPcsNoStroke.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPcsNoStroke.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPcsNoStroke.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtPcsNoStroke.Location = New System.Drawing.Point(472, 344)
        Me.txtPcsNoStroke.MaxLength = 30
        Me.txtPcsNoStroke.Name = "txtPcsNoStroke"
        Me.txtPcsNoStroke.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPcsNoStroke.Size = New System.Drawing.Size(85, 19)
        Me.txtPcsNoStroke.TabIndex = 30
        '
        'txtOPProduction
        '
        Me.txtOPProduction.AcceptsReturn = True
        Me.txtOPProduction.BackColor = System.Drawing.SystemColors.Window
        Me.txtOPProduction.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOPProduction.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOPProduction.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtOPProduction.Location = New System.Drawing.Point(144, 300)
        Me.txtOPProduction.MaxLength = 30
        Me.txtOPProduction.Name = "txtOPProduction"
        Me.txtOPProduction.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOPProduction.Size = New System.Drawing.Size(85, 19)
        Me.txtOPProduction.TabIndex = 25
        '
        'cboUnit
        '
        Me.cboUnit.BackColor = System.Drawing.SystemColors.Window
        Me.cboUnit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboUnit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboUnit.Location = New System.Drawing.Point(144, 366)
        Me.cboUnit.Name = "cboUnit"
        Me.cboUnit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboUnit.Size = New System.Drawing.Size(85, 21)
        Me.cboUnit.TabIndex = 31
        '
        'txtProducedQty
        '
        Me.txtProducedQty.AcceptsReturn = True
        Me.txtProducedQty.BackColor = System.Drawing.SystemColors.Window
        Me.txtProducedQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtProducedQty.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtProducedQty.Enabled = False
        Me.txtProducedQty.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtProducedQty.Location = New System.Drawing.Point(470, 134)
        Me.txtProducedQty.MaxLength = 30
        Me.txtProducedQty.Name = "txtProducedQty"
        Me.txtProducedQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProducedQty.Size = New System.Drawing.Size(85, 19)
        Me.txtProducedQty.TabIndex = 15
        '
        'txtToolItemName
        '
        Me.txtToolItemName.AcceptsReturn = True
        Me.txtToolItemName.BackColor = System.Drawing.SystemColors.Window
        Me.txtToolItemName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtToolItemName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtToolItemName.Enabled = False
        Me.txtToolItemName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtToolItemName.Location = New System.Drawing.Point(264, 38)
        Me.txtToolItemName.MaxLength = 30
        Me.txtToolItemName.Name = "txtToolItemName"
        Me.txtToolItemName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtToolItemName.Size = New System.Drawing.Size(293, 19)
        Me.txtToolItemName.TabIndex = 4
        '
        'txtToolItemCode
        '
        Me.txtToolItemCode.AcceptsReturn = True
        Me.txtToolItemCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtToolItemCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtToolItemCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtToolItemCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtToolItemCode.Location = New System.Drawing.Point(144, 38)
        Me.txtToolItemCode.MaxLength = 0
        Me.txtToolItemCode.Name = "txtToolItemCode"
        Me.txtToolItemCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtToolItemCode.Size = New System.Drawing.Size(85, 19)
        Me.txtToolItemCode.TabIndex = 2
        '
        'txtDeptDesc
        '
        Me.txtDeptDesc.AcceptsReturn = True
        Me.txtDeptDesc.BackColor = System.Drawing.SystemColors.Window
        Me.txtDeptDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDeptDesc.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDeptDesc.Enabled = False
        Me.txtDeptDesc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtDeptDesc.Location = New System.Drawing.Point(264, 62)
        Me.txtDeptDesc.MaxLength = 30
        Me.txtDeptDesc.Name = "txtDeptDesc"
        Me.txtDeptDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDeptDesc.Size = New System.Drawing.Size(293, 19)
        Me.txtDeptDesc.TabIndex = 7
        '
        'chkToolUB
        '
        Me.chkToolUB.AutoSize = True
        Me.chkToolUB.BackColor = System.Drawing.SystemColors.Control
        Me.chkToolUB.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkToolUB.Enabled = False
        Me.chkToolUB.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkToolUB.Location = New System.Drawing.Point(394, 260)
        Me.chkToolUB.Name = "chkToolUB"
        Me.chkToolUB.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkToolUB.Size = New System.Drawing.Size(141, 17)
        Me.chkToolUB.TabIndex = 22
        Me.chkToolUB.Text = "Tool Under Break Down"
        Me.chkToolUB.UseVisualStyleBackColor = False
        '
        'cboToolFreq
        '
        Me.cboToolFreq.BackColor = System.Drawing.SystemColors.Window
        Me.cboToolFreq.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboToolFreq.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboToolFreq.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboToolFreq.Location = New System.Drawing.Point(144, 254)
        Me.cboToolFreq.Name = "cboToolFreq"
        Me.cboToolFreq.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboToolFreq.Size = New System.Drawing.Size(85, 21)
        Me.cboToolFreq.TabIndex = 21
        '
        'txtItemCode
        '
        Me.txtItemCode.AcceptsReturn = True
        Me.txtItemCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtItemCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItemCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtItemCode.Location = New System.Drawing.Point(144, 86)
        Me.txtItemCode.MaxLength = 0
        Me.txtItemCode.Name = "txtItemCode"
        Me.txtItemCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemCode.Size = New System.Drawing.Size(85, 19)
        Me.txtItemCode.TabIndex = 8
        '
        'txtMasterToolNo
        '
        Me.txtMasterToolNo.AcceptsReturn = True
        Me.txtMasterToolNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtMasterToolNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMasterToolNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMasterToolNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMasterToolNo.Location = New System.Drawing.Point(144, 230)
        Me.txtMasterToolNo.MaxLength = 0
        Me.txtMasterToolNo.Name = "txtMasterToolNo"
        Me.txtMasterToolNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMasterToolNo.Size = New System.Drawing.Size(85, 19)
        Me.txtMasterToolNo.TabIndex = 19
        '
        'txtToolLife
        '
        Me.txtToolLife.AcceptsReturn = True
        Me.txtToolLife.BackColor = System.Drawing.SystemColors.Window
        Me.txtToolLife.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtToolLife.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtToolLife.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtToolLife.Location = New System.Drawing.Point(144, 344)
        Me.txtToolLife.MaxLength = 0
        Me.txtToolLife.Name = "txtToolLife"
        Me.txtToolLife.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtToolLife.Size = New System.Drawing.Size(85, 19)
        Me.txtToolLife.TabIndex = 29
        '
        'txtToolPrdQty
        '
        Me.txtToolPrdQty.AcceptsReturn = True
        Me.txtToolPrdQty.BackColor = System.Drawing.SystemColors.Window
        Me.txtToolPrdQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtToolPrdQty.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtToolPrdQty.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtToolPrdQty.Location = New System.Drawing.Point(472, 322)
        Me.txtToolPrdQty.MaxLength = 30
        Me.txtToolPrdQty.Name = "txtToolPrdQty"
        Me.txtToolPrdQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtToolPrdQty.Size = New System.Drawing.Size(85, 19)
        Me.txtToolPrdQty.TabIndex = 28
        '
        'txtToolPreventiveQty
        '
        Me.txtToolPreventiveQty.AcceptsReturn = True
        Me.txtToolPreventiveQty.BackColor = System.Drawing.SystemColors.Window
        Me.txtToolPreventiveQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtToolPreventiveQty.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtToolPreventiveQty.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtToolPreventiveQty.Location = New System.Drawing.Point(144, 322)
        Me.txtToolPreventiveQty.MaxLength = 30
        Me.txtToolPreventiveQty.Name = "txtToolPreventiveQty"
        Me.txtToolPreventiveQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtToolPreventiveQty.Size = New System.Drawing.Size(85, 19)
        Me.txtToolPreventiveQty.TabIndex = 27
        '
        'txtOprCode
        '
        Me.txtOprCode.AcceptsReturn = True
        Me.txtOprCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtOprCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOprCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOprCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOprCode.Location = New System.Drawing.Point(144, 110)
        Me.txtOprCode.MaxLength = 0
        Me.txtOprCode.Name = "txtOprCode"
        Me.txtOprCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOprCode.Size = New System.Drawing.Size(85, 19)
        Me.txtOprCode.TabIndex = 11
        '
        'txtLocation
        '
        Me.txtLocation.AcceptsReturn = True
        Me.txtLocation.BackColor = System.Drawing.SystemColors.Window
        Me.txtLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLocation.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLocation.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtLocation.Location = New System.Drawing.Point(144, 206)
        Me.txtLocation.MaxLength = 30
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLocation.Size = New System.Drawing.Size(415, 19)
        Me.txtLocation.TabIndex = 18
        '
        'txtDrgNo
        '
        Me.txtDrgNo.AcceptsReturn = True
        Me.txtDrgNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtDrgNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDrgNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDrgNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtDrgNo.Location = New System.Drawing.Point(144, 182)
        Me.txtDrgNo.MaxLength = 30
        Me.txtDrgNo.Name = "txtDrgNo"
        Me.txtDrgNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDrgNo.Size = New System.Drawing.Size(415, 19)
        Me.txtDrgNo.TabIndex = 17
        '
        'txtOprDesc
        '
        Me.txtOprDesc.AcceptsReturn = True
        Me.txtOprDesc.BackColor = System.Drawing.SystemColors.Window
        Me.txtOprDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOprDesc.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOprDesc.Enabled = False
        Me.txtOprDesc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtOprDesc.Location = New System.Drawing.Point(264, 110)
        Me.txtOprDesc.MaxLength = 30
        Me.txtOprDesc.Name = "txtOprDesc"
        Me.txtOprDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOprDesc.Size = New System.Drawing.Size(293, 19)
        Me.txtOprDesc.TabIndex = 13
        '
        'txtDeptCode
        '
        Me.txtDeptCode.AcceptsReturn = True
        Me.txtDeptCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtDeptCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDeptCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDeptCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDeptCode.Location = New System.Drawing.Point(144, 62)
        Me.txtDeptCode.MaxLength = 0
        Me.txtDeptCode.Name = "txtDeptCode"
        Me.txtDeptCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDeptCode.Size = New System.Drawing.Size(85, 19)
        Me.txtDeptCode.TabIndex = 5
        '
        'cboToolStatus
        '
        Me.cboToolStatus.BackColor = System.Drawing.SystemColors.Window
        Me.cboToolStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboToolStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboToolStatus.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboToolStatus.Location = New System.Drawing.Point(472, 366)
        Me.cboToolStatus.Name = "cboToolStatus"
        Me.cboToolStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboToolStatus.Size = New System.Drawing.Size(85, 21)
        Me.cboToolStatus.TabIndex = 32
        '
        'txtToolNo
        '
        Me.txtToolNo.AcceptsReturn = True
        Me.txtToolNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtToolNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtToolNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtToolNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtToolNo.Location = New System.Drawing.Point(144, 14)
        Me.txtToolNo.MaxLength = 0
        Me.txtToolNo.Name = "txtToolNo"
        Me.txtToolNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtToolNo.Size = New System.Drawing.Size(85, 19)
        Me.txtToolNo.TabIndex = 0
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtRemarks.Location = New System.Drawing.Point(144, 158)
        Me.txtRemarks.MaxLength = 30
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.Size = New System.Drawing.Size(415, 19)
        Me.txtRemarks.TabIndex = 16
        '
        'txtItemName
        '
        Me.txtItemName.AcceptsReturn = True
        Me.txtItemName.BackColor = System.Drawing.SystemColors.Window
        Me.txtItemName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItemName.Enabled = False
        Me.txtItemName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtItemName.Location = New System.Drawing.Point(264, 86)
        Me.txtItemName.MaxLength = 0
        Me.txtItemName.Name = "txtItemName"
        Me.txtItemName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemName.Size = New System.Drawing.Size(293, 19)
        Me.txtItemName.TabIndex = 10
        '
        'txtToolManuDate
        '
        Me.txtToolManuDate.AllowPromptAsInput = False
        Me.txtToolManuDate.Location = New System.Drawing.Point(144, 134)
        Me.txtToolManuDate.Mask = "##/##/####"
        Me.txtToolManuDate.Name = "txtToolManuDate"
        Me.txtToolManuDate.Size = New System.Drawing.Size(85, 20)
        Me.txtToolManuDate.TabIndex = 14
        '
        'txtToolLoadDate
        '
        Me.txtToolLoadDate.AllowPromptAsInput = False
        Me.txtToolLoadDate.Location = New System.Drawing.Point(144, 278)
        Me.txtToolLoadDate.Mask = "##/##/####"
        Me.txtToolLoadDate.Name = "txtToolLoadDate"
        Me.txtToolLoadDate.Size = New System.Drawing.Size(85, 20)
        Me.txtToolLoadDate.TabIndex = 23
        '
        'txtToolLoadTime
        '
        Me.txtToolLoadTime.AllowPromptAsInput = False
        Me.txtToolLoadTime.Location = New System.Drawing.Point(472, 278)
        Me.txtToolLoadTime.Mask = "##:##"
        Me.txtToolLoadTime.Name = "txtToolLoadTime"
        Me.txtToolLoadTime.Size = New System.Drawing.Size(85, 20)
        Me.txtToolLoadTime.TabIndex = 24
        '
        'txtOpAsOnDate
        '
        Me.txtOpAsOnDate.AllowPromptAsInput = False
        Me.txtOpAsOnDate.Location = New System.Drawing.Point(472, 300)
        Me.txtOpAsOnDate.Mask = "##/##/####"
        Me.txtOpAsOnDate.Name = "txtOpAsOnDate"
        Me.txtOpAsOnDate.Size = New System.Drawing.Size(85, 20)
        Me.txtOpAsOnDate.TabIndex = 26
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.SystemColors.Control
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label21.Location = New System.Drawing.Point(58, 370)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(81, 13)
        Me.Label21.TabIndex = 66
        Me.Label21.Text = "Tool Life Base :"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(334, 346)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(131, 13)
        Me.Label19.TabIndex = 65
        Me.Label19.Text = "No of Pcs Per Stroke :"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(399, 304)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(66, 13)
        Me.Label12.TabIndex = 64
        Me.Label12.Text = "As on Date :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(12, 302)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(127, 13)
        Me.Label11.TabIndex = 63
        Me.Label11.Text = "Opening Production :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(336, 138)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(127, 13)
        Me.Label5.TabIndex = 62
        Me.Label5.Text = "Total Produced Qty :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label20.Location = New System.Drawing.Point(82, 42)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(57, 13)
        Me.Label20.TabIndex = 61
        Me.Label20.Text = "Tool Item :"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(402, 282)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(63, 13)
        Me.Label18.TabIndex = 60
        Me.Label18.Text = "Load Time :"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(76, 282)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(63, 13)
        Me.Label3.TabIndex = 59
        Me.Label3.Text = "Load Date :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label17.Location = New System.Drawing.Point(50, 232)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(89, 13)
        Me.Label17.TabIndex = 58
        Me.Label17.Text = "Master Tool No. :"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label16.Location = New System.Drawing.Point(85, 348)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(54, 13)
        Me.Label16.TabIndex = 57
        Me.Label16.Text = "Tool Life :"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(338, 324)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(127, 13)
        Me.Label15.TabIndex = 56
        Me.Label15.Text = "Production Capacity :"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(18, 326)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(121, 13)
        Me.Label14.TabIndex = 55
        Me.Label14.Text = "Preventive Qty :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(18, 258)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(121, 13)
        Me.Label13.TabIndex = 54
        Me.Label13.Text = "Tool Fequency :"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label10.Location = New System.Drawing.Point(80, 114)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(59, 13)
        Me.Label10.TabIndex = 53
        Me.Label10.Text = "Operation :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(18, 210)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(121, 13)
        Me.Label9.TabIndex = 52
        Me.Label9.Text = "Location :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(18, 186)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(121, 13)
        Me.Label6.TabIndex = 51
        Me.Label6.Text = "Drawing No. :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label4.Location = New System.Drawing.Point(68, 64)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(71, 13)
        Me.Label4.TabIndex = 50
        Me.Label4.Text = "Deptartment :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_20
        '
        Me._lblUnder_20.AutoSize = True
        Me._lblUnder_20.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_20.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_20, CType(20, Short))
        Me._lblUnder_20.Location = New System.Drawing.Point(422, 370)
        Me._lblUnder_20.Name = "_lblUnder_20"
        Me._lblUnder_20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_20.Size = New System.Drawing.Size(43, 13)
        Me._lblUnder_20.TabIndex = 49
        Me._lblUnder_20.Text = "Status :"
        Me._lblUnder_20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(32, 138)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(107, 13)
        Me.Label7.TabIndex = 48
        Me.Label7.Text = "Manufacturing Date :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label2.Location = New System.Drawing.Point(85, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(54, 13)
        Me.Label2.TabIndex = 47
        Me.Label2.Text = "Tool No. :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(18, 162)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(121, 13)
        Me.Label8.TabIndex = 46
        Me.Label8.Text = "Remarks :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label1.Location = New System.Drawing.Point(52, 90)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(87, 13)
        Me.Label1.TabIndex = 45
        Me.Label1.Text = "Production Item :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 2)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(591, 385)
        Me.SprdView.TabIndex = 43
        '
        'ADataGrid
        '
        Me.ADataGrid.BackColor = System.Drawing.SystemColors.Window
        Me.ADataGrid.CommandType = ADODB.CommandTypeEnum.adCmdUnknown
        Me.ADataGrid.CursorType = ADODB.CursorTypeEnum.adOpenStatic
        Me.ADataGrid.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ADataGrid.Location = New System.Drawing.Point(0, 0)
        Me.ADataGrid.LockType = ADODB.LockTypeEnum.adLockOptimistic
        Me.ADataGrid.Name = "ADataGrid"
        Me.ADataGrid.Size = New System.Drawing.Size(80, 23)
        Me.ADataGrid.TabIndex = 45
        Me.ADataGrid.Text = "Adodc1"
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(0, 0)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 46
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.CmdModify)
        Me.FraMovement.Controls.Add(Me.cmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdSavePrint)
        Me.FraMovement.Controls.Add(Me.CmdAdd)
        Me.FraMovement.Controls.Add(Me.CmdDelete)
        Me.FraMovement.Controls.Add(Me.CmdView)
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.CmdSave)
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 382)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(591, 55)
        Me.FraMovement.TabIndex = 34
        Me.FraMovement.TabStop = False
        '
        'cmdPreview
        '
        Me.cmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPreview.Image = CType(resources.GetObject("cmdPreview.Image"), System.Drawing.Image)
        Me.cmdPreview.Location = New System.Drawing.Point(386, 12)
        Me.cmdPreview.Name = "cmdPreview"
        Me.cmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPreview.Size = New System.Drawing.Size(60, 37)
        Me.cmdPreview.TabIndex = 40
        Me.cmdPreview.Text = "Preview"
        Me.cmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdPreview.UseVisualStyleBackColor = False
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.Location = New System.Drawing.Point(206, 12)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(60, 37)
        Me.cmdSavePrint.TabIndex = 37
        Me.cmdSavePrint.Text = "SavePrint"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'frmToolMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(592, 439)
        Me.Controls.Add(Me.fraTop)
        Me.Controls.Add(Me.SprdView)
        Me.Controls.Add(Me.ADataGrid)
        Me.Controls.Add(Me.Report1)
        Me.Controls.Add(Me.FraMovement)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(73, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmToolMaster"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds
        Me.Text = "Tool Master"
        Me.fraTop.ResumeLayout(False)
        Me.fraTop.PerformLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.lblUnder, System.ComponentModel.ISupportInitialize).EndInit()
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