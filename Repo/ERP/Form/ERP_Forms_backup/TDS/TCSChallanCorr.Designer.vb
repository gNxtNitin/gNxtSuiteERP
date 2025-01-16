<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmTCSChallanCorr
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
		Me.MDIParent = TDS.Master
		TDS.Master.Show
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
	Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
	Public WithEvents txtTCSAmount As System.Windows.Forms.TextBox
	Public WithEvents txtSurcharge As System.Windows.Forms.TextBox
	Public WithEvents txtCess As System.Windows.Forms.TextBox
	Public WithEvents txtInterest As System.Windows.Forms.TextBox
	Public WithEvents txtOthers As System.Windows.Forms.TextBox
	Public WithEvents txtNetAmount As System.Windows.Forms.TextBox
	Public WithEvents txtAmountPaid As System.Windows.Forms.TextBox
	Public WithEvents _OptSelection_1 As System.Windows.Forms.RadioButton
	Public WithEvents _OptSelection_0 As System.Windows.Forms.RadioButton
	Public WithEvents Frame1 As System.Windows.Forms.GroupBox
	Public WithEvents cboCollectionCode As System.Windows.Forms.ComboBox
	Public WithEvents txtChqDate As System.Windows.Forms.TextBox
	Public WithEvents txtChqNo As System.Windows.Forms.TextBox
	Public WithEvents txtBankCode As System.Windows.Forms.TextBox
	Public WithEvents cmdShow As System.Windows.Forms.Button
	Public WithEvents txtRefNo As System.Windows.Forms.TextBox
	Public WithEvents txtChallanNo As System.Windows.Forms.TextBox
	Public WithEvents txtChallanDate As System.Windows.Forms.TextBox
	Public WithEvents txtBankName As System.Windows.Forms.TextBox
	Public WithEvents txtRefDate As System.Windows.Forms.TextBox
	Public WithEvents Label13 As System.Windows.Forms.Label
	Public WithEvents Label6 As System.Windows.Forms.Label
	Public WithEvents Label5 As System.Windows.Forms.Label
	Public WithEvents Label4 As System.Windows.Forms.Label
	Public WithEvents lblMKey As System.Windows.Forms.Label
	Public WithEvents _Lbl_3 As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents Lable11 As System.Windows.Forms.Label
	Public WithEvents _Lbl_0 As System.Windows.Forms.Label
	Public WithEvents FraChallan As System.Windows.Forms.GroupBox
	Public WithEvents AData1 As VB6.ADODC
	Public WithEvents Label7 As System.Windows.Forms.Label
	Public WithEvents Label8 As System.Windows.Forms.Label
	Public WithEvents Label9 As System.Windows.Forms.Label
	Public WithEvents Label10 As System.Windows.Forms.Label
	Public WithEvents Label11 As System.Windows.Forms.Label
	Public WithEvents Label12 As System.Windows.Forms.Label
	Public WithEvents Label3 As System.Windows.Forms.Label
	Public WithEvents FraView As System.Windows.Forms.GroupBox
	Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
	Public WithEvents cmdPreview As System.Windows.Forms.Button
	Public WithEvents cmdSavePrint As System.Windows.Forms.Button
	Public WithEvents CmdAdd As System.Windows.Forms.Button
	Public WithEvents CmdModify As System.Windows.Forms.Button
	Public WithEvents CmdDelete As System.Windows.Forms.Button
	Public WithEvents CmdSave As System.Windows.Forms.Button
	Public WithEvents CmdView As System.Windows.Forms.Button
	Public WithEvents CmdClose As System.Windows.Forms.Button
	Public WithEvents cmdPrint As System.Windows.Forms.Button
	Public WithEvents Report1 As AxCrystal.AxCrystalReport
	Public WithEvents FraMovement As System.Windows.Forms.GroupBox
	Public WithEvents ADataGrid As VB6.ADODC
	Public WithEvents Lbl As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
	Public WithEvents OptSelection As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmTCSChallanCorr))
		Me.components = New System.ComponentModel.Container()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
		Me.FraView = New System.Windows.Forms.GroupBox
		Me.SprdMain = New AxFPSpreadADO.AxfpSpread
		Me.txtTCSAmount = New System.Windows.Forms.TextBox
		Me.txtSurcharge = New System.Windows.Forms.TextBox
		Me.txtCess = New System.Windows.Forms.TextBox
		Me.txtInterest = New System.Windows.Forms.TextBox
		Me.txtOthers = New System.Windows.Forms.TextBox
		Me.txtNetAmount = New System.Windows.Forms.TextBox
		Me.txtAmountPaid = New System.Windows.Forms.TextBox
		Me.FraChallan = New System.Windows.Forms.GroupBox
		Me.Frame1 = New System.Windows.Forms.GroupBox
		Me._OptSelection_1 = New System.Windows.Forms.RadioButton
		Me._OptSelection_0 = New System.Windows.Forms.RadioButton
		Me.cboCollectionCode = New System.Windows.Forms.ComboBox
		Me.txtChqDate = New System.Windows.Forms.TextBox
		Me.txtChqNo = New System.Windows.Forms.TextBox
		Me.txtBankCode = New System.Windows.Forms.TextBox
		Me.cmdShow = New System.Windows.Forms.Button
		Me.txtRefNo = New System.Windows.Forms.TextBox
		Me.txtChallanNo = New System.Windows.Forms.TextBox
		Me.txtChallanDate = New System.Windows.Forms.TextBox
		Me.txtBankName = New System.Windows.Forms.TextBox
		Me.txtRefDate = New System.Windows.Forms.TextBox
		Me.Label13 = New System.Windows.Forms.Label
		Me.Label6 = New System.Windows.Forms.Label
		Me.Label5 = New System.Windows.Forms.Label
		Me.Label4 = New System.Windows.Forms.Label
		Me.lblMKey = New System.Windows.Forms.Label
		Me._Lbl_3 = New System.Windows.Forms.Label
		Me.Label1 = New System.Windows.Forms.Label
		Me.Label2 = New System.Windows.Forms.Label
		Me.Lable11 = New System.Windows.Forms.Label
		Me._Lbl_0 = New System.Windows.Forms.Label
		Me.AData1 = New VB6.ADODC
		Me.Label7 = New System.Windows.Forms.Label
		Me.Label8 = New System.Windows.Forms.Label
		Me.Label9 = New System.Windows.Forms.Label
		Me.Label10 = New System.Windows.Forms.Label
		Me.Label11 = New System.Windows.Forms.Label
		Me.Label12 = New System.Windows.Forms.Label
		Me.Label3 = New System.Windows.Forms.Label
		Me.SprdView = New AxFPSpreadADO.AxfpSpread
		Me.FraMovement = New System.Windows.Forms.GroupBox
		Me.cmdPreview = New System.Windows.Forms.Button
		Me.cmdSavePrint = New System.Windows.Forms.Button
		Me.CmdAdd = New System.Windows.Forms.Button
		Me.CmdModify = New System.Windows.Forms.Button
		Me.CmdDelete = New System.Windows.Forms.Button
		Me.CmdSave = New System.Windows.Forms.Button
		Me.CmdView = New System.Windows.Forms.Button
		Me.CmdClose = New System.Windows.Forms.Button
		Me.cmdPrint = New System.Windows.Forms.Button
		Me.Report1 = New AxCrystal.AxCrystalReport
		Me.ADataGrid = New VB6.ADODC
		Me.Lbl = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(components)
		Me.OptSelection = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(components)
		Me.FraView.SuspendLayout()
		Me.FraChallan.SuspendLayout()
		Me.Frame1.SuspendLayout()
		Me.FraMovement.SuspendLayout()
		Me.SuspendLayout()
		Me.ToolTip1.Active = True
		CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.Lbl, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.OptSelection, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
		Me.Text = "TCS Challan Correction"
		Me.ClientSize = New System.Drawing.Size(730, 458)
		Me.Location = New System.Drawing.Point(73, 22)
		Me.Icon = CType(resources.GetObject("frmTCSChallanCorr.Icon"), System.Drawing.Icon)
		Me.KeyPreview = True
		Me.MaximizeBox = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds
		Me.MinimizeBox = False
		Me.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.SystemColors.Control
		Me.ControlBox = True
		Me.Enabled = True
		Me.Cursor = System.Windows.Forms.Cursors.Default
		Me.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.ShowInTaskbar = True
		Me.HelpButton = False
		Me.WindowState = System.Windows.Forms.FormWindowState.Normal
		Me.Name = "frmTCSChallanCorr"
		Me.FraView.Size = New System.Drawing.Size(729, 415)
		Me.FraView.Location = New System.Drawing.Point(0, -4)
		Me.FraView.TabIndex = 17
		Me.FraView.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.FraView.BackColor = System.Drawing.SystemColors.Control
		Me.FraView.Enabled = True
		Me.FraView.ForeColor = System.Drawing.SystemColors.ControlText
		Me.FraView.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.FraView.Visible = True
		Me.FraView.Padding = New System.Windows.Forms.Padding(0)
		Me.FraView.Name = "FraView"
		SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SprdMain.Size = New System.Drawing.Size(723, 201)
		Me.SprdMain.Location = New System.Drawing.Point(2, 140)
		Me.SprdMain.TabIndex = 47
		Me.SprdMain.Name = "SprdMain"
		Me.txtTCSAmount.AutoSize = False
		Me.txtTCSAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
		Me.txtTCSAmount.Enabled = False
		Me.txtTCSAmount.Size = New System.Drawing.Size(79, 19)
		Me.txtTCSAmount.Location = New System.Drawing.Point(95, 370)
		Me.txtTCSAmount.TabIndex = 11
		Me.txtTCSAmount.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtTCSAmount.AcceptsReturn = True
		Me.txtTCSAmount.BackColor = System.Drawing.SystemColors.Window
		Me.txtTCSAmount.CausesValidation = True
		Me.txtTCSAmount.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtTCSAmount.HideSelection = True
		Me.txtTCSAmount.ReadOnly = False
		Me.txtTCSAmount.Maxlength = 0
		Me.txtTCSAmount.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtTCSAmount.MultiLine = False
		Me.txtTCSAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtTCSAmount.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtTCSAmount.TabStop = True
		Me.txtTCSAmount.Visible = True
		Me.txtTCSAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtTCSAmount.Name = "txtTCSAmount"
		Me.txtSurcharge.AutoSize = False
		Me.txtSurcharge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
		Me.txtSurcharge.Enabled = False
		Me.txtSurcharge.Size = New System.Drawing.Size(79, 19)
		Me.txtSurcharge.Location = New System.Drawing.Point(391, 370)
		Me.txtSurcharge.TabIndex = 12
		Me.txtSurcharge.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtSurcharge.AcceptsReturn = True
		Me.txtSurcharge.BackColor = System.Drawing.SystemColors.Window
		Me.txtSurcharge.CausesValidation = True
		Me.txtSurcharge.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtSurcharge.HideSelection = True
		Me.txtSurcharge.ReadOnly = False
		Me.txtSurcharge.Maxlength = 0
		Me.txtSurcharge.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtSurcharge.MultiLine = False
		Me.txtSurcharge.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtSurcharge.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtSurcharge.TabStop = True
		Me.txtSurcharge.Visible = True
		Me.txtSurcharge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtSurcharge.Name = "txtSurcharge"
		Me.txtCess.AutoSize = False
		Me.txtCess.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
		Me.txtCess.Enabled = False
		Me.txtCess.Size = New System.Drawing.Size(79, 19)
		Me.txtCess.Location = New System.Drawing.Point(644, 370)
		Me.txtCess.TabIndex = 13
		Me.txtCess.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtCess.AcceptsReturn = True
		Me.txtCess.BackColor = System.Drawing.SystemColors.Window
		Me.txtCess.CausesValidation = True
		Me.txtCess.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtCess.HideSelection = True
		Me.txtCess.ReadOnly = False
		Me.txtCess.Maxlength = 0
		Me.txtCess.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtCess.MultiLine = False
		Me.txtCess.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtCess.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtCess.TabStop = True
		Me.txtCess.Visible = True
		Me.txtCess.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtCess.Name = "txtCess"
		Me.txtInterest.AutoSize = False
		Me.txtInterest.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
		Me.txtInterest.Size = New System.Drawing.Size(79, 19)
		Me.txtInterest.Location = New System.Drawing.Point(95, 392)
		Me.txtInterest.TabIndex = 14
		Me.txtInterest.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtInterest.AcceptsReturn = True
		Me.txtInterest.BackColor = System.Drawing.SystemColors.Window
		Me.txtInterest.CausesValidation = True
		Me.txtInterest.Enabled = True
		Me.txtInterest.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtInterest.HideSelection = True
		Me.txtInterest.ReadOnly = False
		Me.txtInterest.Maxlength = 0
		Me.txtInterest.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtInterest.MultiLine = False
		Me.txtInterest.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtInterest.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtInterest.TabStop = True
		Me.txtInterest.Visible = True
		Me.txtInterest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtInterest.Name = "txtInterest"
		Me.txtOthers.AutoSize = False
		Me.txtOthers.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
		Me.txtOthers.Size = New System.Drawing.Size(79, 19)
		Me.txtOthers.Location = New System.Drawing.Point(391, 392)
		Me.txtOthers.TabIndex = 15
		Me.txtOthers.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtOthers.AcceptsReturn = True
		Me.txtOthers.BackColor = System.Drawing.SystemColors.Window
		Me.txtOthers.CausesValidation = True
		Me.txtOthers.Enabled = True
		Me.txtOthers.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtOthers.HideSelection = True
		Me.txtOthers.ReadOnly = False
		Me.txtOthers.Maxlength = 0
		Me.txtOthers.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtOthers.MultiLine = False
		Me.txtOthers.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtOthers.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtOthers.TabStop = True
		Me.txtOthers.Visible = True
		Me.txtOthers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtOthers.Name = "txtOthers"
		Me.txtNetAmount.AutoSize = False
		Me.txtNetAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
		Me.txtNetAmount.Enabled = False
		Me.txtNetAmount.Size = New System.Drawing.Size(79, 19)
		Me.txtNetAmount.Location = New System.Drawing.Point(644, 392)
		Me.txtNetAmount.TabIndex = 16
		Me.txtNetAmount.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtNetAmount.AcceptsReturn = True
		Me.txtNetAmount.BackColor = System.Drawing.SystemColors.Window
		Me.txtNetAmount.CausesValidation = True
		Me.txtNetAmount.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtNetAmount.HideSelection = True
		Me.txtNetAmount.ReadOnly = False
		Me.txtNetAmount.Maxlength = 0
		Me.txtNetAmount.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtNetAmount.MultiLine = False
		Me.txtNetAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtNetAmount.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtNetAmount.TabStop = True
		Me.txtNetAmount.Visible = True
		Me.txtNetAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtNetAmount.Name = "txtNetAmount"
		Me.txtAmountPaid.AutoSize = False
		Me.txtAmountPaid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
		Me.txtAmountPaid.Enabled = False
		Me.txtAmountPaid.Size = New System.Drawing.Size(79, 19)
		Me.txtAmountPaid.Location = New System.Drawing.Point(644, 348)
		Me.txtAmountPaid.TabIndex = 10
		Me.txtAmountPaid.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtAmountPaid.AcceptsReturn = True
		Me.txtAmountPaid.BackColor = System.Drawing.SystemColors.Window
		Me.txtAmountPaid.CausesValidation = True
		Me.txtAmountPaid.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtAmountPaid.HideSelection = True
		Me.txtAmountPaid.ReadOnly = False
		Me.txtAmountPaid.Maxlength = 0
		Me.txtAmountPaid.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtAmountPaid.MultiLine = False
		Me.txtAmountPaid.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtAmountPaid.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtAmountPaid.TabStop = True
		Me.txtAmountPaid.Visible = True
		Me.txtAmountPaid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtAmountPaid.Name = "txtAmountPaid"
		Me.FraChallan.ForeColor = System.Drawing.Color.FromARGB(128, 0, 0)
		Me.FraChallan.Size = New System.Drawing.Size(729, 137)
		Me.FraChallan.Location = New System.Drawing.Point(0, 2)
		Me.FraChallan.TabIndex = 26
		Me.FraChallan.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.FraChallan.BackColor = System.Drawing.SystemColors.Control
		Me.FraChallan.Enabled = True
		Me.FraChallan.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.FraChallan.Visible = True
		Me.FraChallan.Padding = New System.Windows.Forms.Padding(0)
		Me.FraChallan.Name = "FraChallan"
		Me.Frame1.Text = "Selection"
		Me.Frame1.ForeColor = System.Drawing.Color.FromARGB(128, 0, 0)
		Me.Frame1.Size = New System.Drawing.Size(125, 43)
		Me.Frame1.Location = New System.Drawing.Point(482, 84)
		Me.Frame1.TabIndex = 48
		Me.Frame1.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame1.BackColor = System.Drawing.SystemColors.Control
		Me.Frame1.Enabled = True
		Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame1.Visible = True
		Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame1.Name = "Frame1"
		Me._OptSelection_1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._OptSelection_1.Text = "None"
		Me._OptSelection_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me._OptSelection_1.Size = New System.Drawing.Size(59, 15)
		Me._OptSelection_1.Location = New System.Drawing.Point(64, 20)
		Me._OptSelection_1.TabIndex = 50
		Me._OptSelection_1.Checked = True
		Me._OptSelection_1.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._OptSelection_1.BackColor = System.Drawing.SystemColors.Control
		Me._OptSelection_1.CausesValidation = True
		Me._OptSelection_1.Enabled = True
		Me._OptSelection_1.ForeColor = System.Drawing.SystemColors.ControlText
		Me._OptSelection_1.Cursor = System.Windows.Forms.Cursors.Default
		Me._OptSelection_1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._OptSelection_1.Appearance = System.Windows.Forms.Appearance.Normal
		Me._OptSelection_1.TabStop = True
		Me._OptSelection_1.Visible = True
		Me._OptSelection_1.Name = "_OptSelection_1"
		Me._OptSelection_0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._OptSelection_0.Text = "All"
		Me._OptSelection_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me._OptSelection_0.Size = New System.Drawing.Size(37, 15)
		Me._OptSelection_0.Location = New System.Drawing.Point(10, 20)
		Me._OptSelection_0.TabIndex = 49
		Me._OptSelection_0.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._OptSelection_0.BackColor = System.Drawing.SystemColors.Control
		Me._OptSelection_0.CausesValidation = True
		Me._OptSelection_0.Enabled = True
		Me._OptSelection_0.ForeColor = System.Drawing.SystemColors.ControlText
		Me._OptSelection_0.Cursor = System.Windows.Forms.Cursors.Default
		Me._OptSelection_0.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._OptSelection_0.Appearance = System.Windows.Forms.Appearance.Normal
		Me._OptSelection_0.TabStop = True
		Me._OptSelection_0.Checked = False
		Me._OptSelection_0.Visible = True
		Me._OptSelection_0.Name = "_OptSelection_0"
		Me.cboCollectionCode.Size = New System.Drawing.Size(305, 21)
		Me.cboCollectionCode.Location = New System.Drawing.Point(106, 60)
		Me.cboCollectionCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cboCollectionCode.TabIndex = 46
		Me.cboCollectionCode.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cboCollectionCode.BackColor = System.Drawing.SystemColors.Window
		Me.cboCollectionCode.CausesValidation = True
		Me.cboCollectionCode.Enabled = True
		Me.cboCollectionCode.ForeColor = System.Drawing.SystemColors.WindowText
		Me.cboCollectionCode.IntegralHeight = True
		Me.cboCollectionCode.Cursor = System.Windows.Forms.Cursors.Default
		Me.cboCollectionCode.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cboCollectionCode.Sorted = False
		Me.cboCollectionCode.TabStop = True
		Me.cboCollectionCode.Visible = True
		Me.cboCollectionCode.Name = "cboCollectionCode"
		Me.txtChqDate.AutoSize = False
		Me.txtChqDate.Size = New System.Drawing.Size(79, 19)
		Me.txtChqDate.Location = New System.Drawing.Point(332, 110)
		Me.txtChqDate.TabIndex = 8
		Me.txtChqDate.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtChqDate.AcceptsReturn = True
		Me.txtChqDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtChqDate.BackColor = System.Drawing.SystemColors.Window
		Me.txtChqDate.CausesValidation = True
		Me.txtChqDate.Enabled = True
		Me.txtChqDate.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtChqDate.HideSelection = True
		Me.txtChqDate.ReadOnly = False
		Me.txtChqDate.Maxlength = 0
		Me.txtChqDate.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtChqDate.MultiLine = False
		Me.txtChqDate.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtChqDate.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtChqDate.TabStop = True
		Me.txtChqDate.Visible = True
		Me.txtChqDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtChqDate.Name = "txtChqDate"
		Me.txtChqNo.AutoSize = False
		Me.txtChqNo.Size = New System.Drawing.Size(79, 19)
		Me.txtChqNo.Location = New System.Drawing.Point(106, 110)
		Me.txtChqNo.TabIndex = 7
		Me.txtChqNo.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtChqNo.AcceptsReturn = True
		Me.txtChqNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtChqNo.BackColor = System.Drawing.SystemColors.Window
		Me.txtChqNo.CausesValidation = True
		Me.txtChqNo.Enabled = True
		Me.txtChqNo.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtChqNo.HideSelection = True
		Me.txtChqNo.ReadOnly = False
		Me.txtChqNo.Maxlength = 0
		Me.txtChqNo.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtChqNo.MultiLine = False
		Me.txtChqNo.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtChqNo.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtChqNo.TabStop = True
		Me.txtChqNo.Visible = True
		Me.txtChqNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtChqNo.Name = "txtChqNo"
		Me.txtBankCode.AutoSize = False
		Me.txtBankCode.Size = New System.Drawing.Size(77, 19)
		Me.txtBankCode.Location = New System.Drawing.Point(640, 34)
		Me.txtBankCode.TabIndex = 4
		Me.ToolTip1.SetToolTip(Me.txtBankCode, "Press F1 For Help")
		Me.txtBankCode.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtBankCode.AcceptsReturn = True
		Me.txtBankCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtBankCode.BackColor = System.Drawing.SystemColors.Window
		Me.txtBankCode.CausesValidation = True
		Me.txtBankCode.Enabled = True
		Me.txtBankCode.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtBankCode.HideSelection = True
		Me.txtBankCode.ReadOnly = False
		Me.txtBankCode.Maxlength = 0
		Me.txtBankCode.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtBankCode.MultiLine = False
		Me.txtBankCode.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtBankCode.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtBankCode.TabStop = True
		Me.txtBankCode.Visible = True
		Me.txtBankCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtBankCode.Name = "txtBankCode"
		Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.cmdShow.Text = "Populate"
		Me.cmdShow.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdShow.Size = New System.Drawing.Size(69, 35)
		Me.cmdShow.Location = New System.Drawing.Point(646, 92)
		Me.cmdShow.Image = CType(resources.GetObject("cmdShow.Image"), System.Drawing.Image)
		Me.cmdShow.TabIndex = 9
		Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
		Me.cmdShow.BackColor = System.Drawing.SystemColors.Control
		Me.cmdShow.CausesValidation = True
		Me.cmdShow.Enabled = True
		Me.cmdShow.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdShow.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdShow.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdShow.TabStop = True
		Me.cmdShow.Name = "cmdShow"
		Me.txtRefNo.AutoSize = False
		Me.txtRefNo.Size = New System.Drawing.Size(79, 19)
		Me.txtRefNo.Location = New System.Drawing.Point(106, 10)
		Me.txtRefNo.TabIndex = 1
		Me.ToolTip1.SetToolTip(Me.txtRefNo, "Press F1 For Help")
		Me.txtRefNo.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtRefNo.AcceptsReturn = True
		Me.txtRefNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtRefNo.BackColor = System.Drawing.SystemColors.Window
		Me.txtRefNo.CausesValidation = True
		Me.txtRefNo.Enabled = True
		Me.txtRefNo.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtRefNo.HideSelection = True
		Me.txtRefNo.ReadOnly = False
		Me.txtRefNo.Maxlength = 0
		Me.txtRefNo.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtRefNo.MultiLine = False
		Me.txtRefNo.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtRefNo.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtRefNo.TabStop = True
		Me.txtRefNo.Visible = True
		Me.txtRefNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtRefNo.Name = "txtRefNo"
		Me.txtChallanNo.AutoSize = False
		Me.txtChallanNo.Size = New System.Drawing.Size(79, 19)
		Me.txtChallanNo.Location = New System.Drawing.Point(106, 88)
		Me.txtChallanNo.TabIndex = 5
		Me.txtChallanNo.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtChallanNo.AcceptsReturn = True
		Me.txtChallanNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtChallanNo.BackColor = System.Drawing.SystemColors.Window
		Me.txtChallanNo.CausesValidation = True
		Me.txtChallanNo.Enabled = True
		Me.txtChallanNo.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtChallanNo.HideSelection = True
		Me.txtChallanNo.ReadOnly = False
		Me.txtChallanNo.Maxlength = 0
		Me.txtChallanNo.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtChallanNo.MultiLine = False
		Me.txtChallanNo.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtChallanNo.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtChallanNo.TabStop = True
		Me.txtChallanNo.Visible = True
		Me.txtChallanNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtChallanNo.Name = "txtChallanNo"
		Me.txtChallanDate.AutoSize = False
		Me.txtChallanDate.Size = New System.Drawing.Size(79, 19)
		Me.txtChallanDate.Location = New System.Drawing.Point(332, 88)
		Me.txtChallanDate.TabIndex = 6
		Me.txtChallanDate.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtChallanDate.AcceptsReturn = True
		Me.txtChallanDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtChallanDate.BackColor = System.Drawing.SystemColors.Window
		Me.txtChallanDate.CausesValidation = True
		Me.txtChallanDate.Enabled = True
		Me.txtChallanDate.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtChallanDate.HideSelection = True
		Me.txtChallanDate.ReadOnly = False
		Me.txtChallanDate.Maxlength = 0
		Me.txtChallanDate.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtChallanDate.MultiLine = False
		Me.txtChallanDate.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtChallanDate.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtChallanDate.TabStop = True
		Me.txtChallanDate.Visible = True
		Me.txtChallanDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtChallanDate.Name = "txtChallanDate"
		Me.txtBankName.AutoSize = False
		Me.txtBankName.Size = New System.Drawing.Size(303, 19)
		Me.txtBankName.Location = New System.Drawing.Point(106, 34)
		Me.txtBankName.TabIndex = 3
		Me.ToolTip1.SetToolTip(Me.txtBankName, "Press F1 For Help")
		Me.txtBankName.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtBankName.AcceptsReturn = True
		Me.txtBankName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtBankName.BackColor = System.Drawing.SystemColors.Window
		Me.txtBankName.CausesValidation = True
		Me.txtBankName.Enabled = True
		Me.txtBankName.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtBankName.HideSelection = True
		Me.txtBankName.ReadOnly = False
		Me.txtBankName.Maxlength = 0
		Me.txtBankName.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtBankName.MultiLine = False
		Me.txtBankName.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtBankName.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtBankName.TabStop = True
		Me.txtBankName.Visible = True
		Me.txtBankName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtBankName.Name = "txtBankName"
		Me.txtRefDate.AutoSize = False
		Me.txtRefDate.ForeColor = System.Drawing.Color.FromARGB(0, 0, 192)
		Me.txtRefDate.Size = New System.Drawing.Size(79, 19)
		Me.txtRefDate.Location = New System.Drawing.Point(638, 10)
		Me.txtRefDate.TabIndex = 2
		Me.txtRefDate.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtRefDate.AcceptsReturn = True
		Me.txtRefDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtRefDate.BackColor = System.Drawing.SystemColors.Window
		Me.txtRefDate.CausesValidation = True
		Me.txtRefDate.Enabled = True
		Me.txtRefDate.HideSelection = True
		Me.txtRefDate.ReadOnly = False
		Me.txtRefDate.Maxlength = 0
		Me.txtRefDate.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtRefDate.MultiLine = False
		Me.txtRefDate.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtRefDate.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtRefDate.TabStop = True
		Me.txtRefDate.Visible = True
		Me.txtRefDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtRefDate.Name = "txtRefDate"
		Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.Label13.Text = "Collection Code :"
		Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label13.Size = New System.Drawing.Size(98, 13)
		Me.Label13.Location = New System.Drawing.Point(5, 60)
		Me.Label13.TabIndex = 45
		Me.Label13.BackColor = System.Drawing.SystemColors.Control
		Me.Label13.Enabled = True
		Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label13.UseMnemonic = True
		Me.Label13.Visible = True
		Me.Label13.AutoSize = True
		Me.Label13.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label13.Name = "Label13"
		Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.Label6.Text = "Chq / DD Date :"
		Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label6.Size = New System.Drawing.Size(94, 13)
		Me.Label6.Location = New System.Drawing.Point(232, 112)
		Me.Label6.TabIndex = 38
		Me.Label6.BackColor = System.Drawing.SystemColors.Control
		Me.Label6.Enabled = True
		Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label6.UseMnemonic = True
		Me.Label6.Visible = True
		Me.Label6.AutoSize = True
		Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label6.Name = "Label6"
		Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.Label5.Text = "Chq / DD No :"
		Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label5.Size = New System.Drawing.Size(83, 13)
		Me.Label5.Location = New System.Drawing.Point(20, 112)
		Me.Label5.TabIndex = 37
		Me.Label5.BackColor = System.Drawing.SystemColors.Control
		Me.Label5.Enabled = True
		Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label5.UseMnemonic = True
		Me.Label5.Visible = True
		Me.Label5.AutoSize = True
		Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label5.Name = "Label5"
		Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.Label4.Text = "Bank Code :"
		Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label4.Size = New System.Drawing.Size(71, 13)
		Me.Label4.Location = New System.Drawing.Point(564, 36)
		Me.Label4.TabIndex = 35
		Me.Label4.BackColor = System.Drawing.SystemColors.Control
		Me.Label4.Enabled = True
		Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label4.UseMnemonic = True
		Me.Label4.Visible = True
		Me.Label4.AutoSize = True
		Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label4.Name = "Label4"
		Me.lblMKey.Text = "lblMKey"
		Me.lblMKey.Size = New System.Drawing.Size(37, 13)
		Me.lblMKey.Location = New System.Drawing.Point(222, 12)
		Me.lblMKey.TabIndex = 28
		Me.lblMKey.Visible = False
		Me.lblMKey.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblMKey.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.lblMKey.BackColor = System.Drawing.SystemColors.Control
		Me.lblMKey.Enabled = True
		Me.lblMKey.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblMKey.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblMKey.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblMKey.UseMnemonic = True
		Me.lblMKey.AutoSize = True
		Me.lblMKey.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lblMKey.Name = "lblMKey"
		Me._Lbl_3.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me._Lbl_3.Text = "Ref No :"
		Me._Lbl_3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me._Lbl_3.Size = New System.Drawing.Size(85, 13)
		Me._Lbl_3.Location = New System.Drawing.Point(18, 12)
		Me._Lbl_3.TabIndex = 27
		Me._Lbl_3.BackColor = System.Drawing.SystemColors.Control
		Me._Lbl_3.Enabled = True
		Me._Lbl_3.ForeColor = System.Drawing.SystemColors.ControlText
		Me._Lbl_3.Cursor = System.Windows.Forms.Cursors.Default
		Me._Lbl_3.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._Lbl_3.UseMnemonic = True
		Me._Lbl_3.Visible = True
		Me._Lbl_3.AutoSize = True
		Me._Lbl_3.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me._Lbl_3.Name = "_Lbl_3"
		Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.Label1.Text = "Challan No :"
		Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.Size = New System.Drawing.Size(85, 13)
		Me.Label1.Location = New System.Drawing.Point(18, 90)
		Me.Label1.TabIndex = 31
		Me.Label1.BackColor = System.Drawing.SystemColors.Control
		Me.Label1.Enabled = True
		Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label1.UseMnemonic = True
		Me.Label1.Visible = True
		Me.Label1.AutoSize = True
		Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label1.Name = "Label1"
		Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.Label2.Text = "Challan Date :"
		Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label2.Size = New System.Drawing.Size(96, 13)
		Me.Label2.Location = New System.Drawing.Point(230, 90)
		Me.Label2.TabIndex = 32
		Me.Label2.BackColor = System.Drawing.SystemColors.Control
		Me.Label2.Enabled = True
		Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label2.UseMnemonic = True
		Me.Label2.Visible = True
		Me.Label2.AutoSize = True
		Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label2.Name = "Label2"
		Me.Lable11.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.Lable11.Text = "Bank Name :"
		Me.Lable11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Lable11.Size = New System.Drawing.Size(85, 13)
		Me.Lable11.Location = New System.Drawing.Point(18, 34)
		Me.Lable11.TabIndex = 30
		Me.Lable11.BackColor = System.Drawing.SystemColors.Control
		Me.Lable11.Enabled = True
		Me.Lable11.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Lable11.Cursor = System.Windows.Forms.Cursors.Default
		Me.Lable11.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Lable11.UseMnemonic = True
		Me.Lable11.Visible = True
		Me.Lable11.AutoSize = True
		Me.Lable11.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Lable11.Name = "Lable11"
		Me._Lbl_0.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me._Lbl_0.Text = "Ref Date :"
		Me._Lbl_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me._Lbl_0.Size = New System.Drawing.Size(60, 13)
		Me._Lbl_0.Location = New System.Drawing.Point(575, 11)
		Me._Lbl_0.TabIndex = 29
		Me._Lbl_0.BackColor = System.Drawing.SystemColors.Control
		Me._Lbl_0.Enabled = True
		Me._Lbl_0.ForeColor = System.Drawing.SystemColors.ControlText
		Me._Lbl_0.Cursor = System.Windows.Forms.Cursors.Default
		Me._Lbl_0.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._Lbl_0.UseMnemonic = True
		Me._Lbl_0.Visible = True
		Me._Lbl_0.AutoSize = True
		Me._Lbl_0.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me._Lbl_0.Name = "_Lbl_0"
		Me.AData1.Size = New System.Drawing.Size(131, 23)
		Me.AData1.Location = New System.Drawing.Point(4, 10)
		Me.AData1.Visible = 0
		Me.AData1.CursorLocation = ADODB.CursorLocationEnum.adUseClient
		Me.AData1.ConnectionTimeout = 15
		Me.AData1.CommandTimeout = 30
		Me.AData1.CursorType = ADODB.CursorTypeEnum.adOpenStatic
		Me.AData1.LockType = ADODB.LockTypeEnum.adLockOptimistic
		Me.AData1.CommandType = ADODB.CommandTypeEnum.adCmdUnknown
		Me.AData1.CacheSize = 50
		Me.AData1.MaxRecords = 0
		Me.AData1.BOFAction = Microsoft.VisualBasic.Compatibility.VB6.ADODC.BOFActionEnum.adDoMoveFirst
		Me.AData1.EOFAction = Microsoft.VisualBasic.Compatibility.VB6.ADODC.EOFActionEnum.adDoMoveLast
		Me.AData1.BackColor = System.Drawing.SystemColors.Window
		Me.AData1.ForeColor = System.Drawing.SystemColors.WindowText
		Me.AData1.Orientation = Microsoft.VisualBasic.Compatibility.VB6.ADODC.OrientationEnum.adHorizontal
		Me.AData1.Enabled = True
		Me.AData1.UserName = ""
		Me.AData1.RecordSource = ""
		Me.AData1.Text = "Adodc1"
		Me.AData1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.AData1.ConnectionString = ""
		Me.AData1.Name = "AData1"
		Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.Label7.Text = "TCS Amount :"
		Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label7.Size = New System.Drawing.Size(79, 13)
		Me.Label7.Location = New System.Drawing.Point(13, 372)
		Me.Label7.TabIndex = 44
		Me.Label7.BackColor = System.Drawing.SystemColors.Control
		Me.Label7.Enabled = True
		Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label7.UseMnemonic = True
		Me.Label7.Visible = True
		Me.Label7.AutoSize = True
		Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label7.Name = "Label7"
		Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.Label8.Text = "Edu. Cess :"
		Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label8.Size = New System.Drawing.Size(66, 13)
		Me.Label8.Location = New System.Drawing.Point(574, 374)
		Me.Label8.TabIndex = 43
		Me.Label8.BackColor = System.Drawing.SystemColors.Control
		Me.Label8.Enabled = True
		Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label8.UseMnemonic = True
		Me.Label8.Visible = True
		Me.Label8.AutoSize = True
		Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label8.Name = "Label8"
		Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.Label9.Text = "Interest :"
		Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label9.Size = New System.Drawing.Size(52, 13)
		Me.Label9.Location = New System.Drawing.Point(37, 394)
		Me.Label9.TabIndex = 42
		Me.Label9.BackColor = System.Drawing.SystemColors.Control
		Me.Label9.Enabled = True
		Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label9.UseMnemonic = True
		Me.Label9.Visible = True
		Me.Label9.AutoSize = True
		Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label9.Name = "Label9"
		Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.Label10.Text = "Others :"
		Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label10.Size = New System.Drawing.Size(46, 13)
		Me.Label10.Location = New System.Drawing.Point(342, 394)
		Me.Label10.TabIndex = 41
		Me.Label10.BackColor = System.Drawing.SystemColors.Control
		Me.Label10.Enabled = True
		Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label10.UseMnemonic = True
		Me.Label10.Visible = True
		Me.Label10.AutoSize = True
		Me.Label10.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label10.Name = "Label10"
		Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.Label11.Text = "Surcharge :"
		Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label11.Size = New System.Drawing.Size(67, 13)
		Me.Label11.Location = New System.Drawing.Point(320, 372)
		Me.Label11.TabIndex = 40
		Me.Label11.BackColor = System.Drawing.SystemColors.Control
		Me.Label11.Enabled = True
		Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label11.UseMnemonic = True
		Me.Label11.Visible = True
		Me.Label11.AutoSize = True
		Me.Label11.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label11.Name = "Label11"
		Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.Label12.Text = "Net Amount :"
		Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label12.Size = New System.Drawing.Size(75, 13)
		Me.Label12.Location = New System.Drawing.Point(564, 394)
		Me.Label12.TabIndex = 39
		Me.Label12.BackColor = System.Drawing.SystemColors.Control
		Me.Label12.Enabled = True
		Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label12.UseMnemonic = True
		Me.Label12.Visible = True
		Me.Label12.AutoSize = True
		Me.Label12.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label12.Name = "Label12"
		Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.Label3.Text = "Amount Paid :"
		Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label3.Size = New System.Drawing.Size(85, 13)
		Me.Label3.Location = New System.Drawing.Point(556, 350)
		Me.Label3.TabIndex = 36
		Me.Label3.BackColor = System.Drawing.SystemColors.Control
		Me.Label3.Enabled = True
		Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label3.UseMnemonic = True
		Me.Label3.Visible = True
		Me.Label3.AutoSize = True
		Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label3.Name = "Label3"
		SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SprdView.Size = New System.Drawing.Size(727, 411)
		Me.SprdView.Location = New System.Drawing.Point(0, 0)
		Me.SprdView.TabIndex = 33
		Me.SprdView.Name = "SprdView"
		Me.FraMovement.Size = New System.Drawing.Size(729, 51)
		Me.FraMovement.Location = New System.Drawing.Point(0, 406)
		Me.FraMovement.TabIndex = 34
		Me.FraMovement.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
		Me.FraMovement.Enabled = True
		Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
		Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.FraMovement.Visible = True
		Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
		Me.FraMovement.Name = "FraMovement"
		Me.cmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.cmdPreview.Text = "Preview"
		Me.cmdPreview.Size = New System.Drawing.Size(60, 37)
		Me.cmdPreview.Location = New System.Drawing.Point(450, 10)
		Me.cmdPreview.Image = CType(resources.GetObject("cmdPreview.Image"), System.Drawing.Image)
		Me.cmdPreview.TabIndex = 23
		Me.cmdPreview.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdPreview.BackColor = System.Drawing.SystemColors.Control
		Me.cmdPreview.CausesValidation = True
		Me.cmdPreview.Enabled = True
		Me.cmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdPreview.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdPreview.TabStop = True
		Me.cmdPreview.Name = "cmdPreview"
		Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.cmdSavePrint.Text = "SavePrint"
		Me.cmdSavePrint.Size = New System.Drawing.Size(60, 37)
		Me.cmdSavePrint.Location = New System.Drawing.Point(270, 10)
		Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
		Me.cmdSavePrint.TabIndex = 20
		Me.cmdSavePrint.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
		Me.cmdSavePrint.CausesValidation = True
		Me.cmdSavePrint.Enabled = True
		Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdSavePrint.TabStop = True
		Me.cmdSavePrint.Name = "cmdSavePrint"
		Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.CmdAdd.Text = "&Add"
		Me.CmdAdd.Size = New System.Drawing.Size(60, 37)
		Me.CmdAdd.Location = New System.Drawing.Point(90, 10)
		Me.CmdAdd.Image = CType(resources.GetObject("CmdAdd.Image"), System.Drawing.Image)
		Me.CmdAdd.TabIndex = 0
		Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
		Me.CmdAdd.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CmdAdd.BackColor = System.Drawing.SystemColors.Control
		Me.CmdAdd.CausesValidation = True
		Me.CmdAdd.Enabled = True
		Me.CmdAdd.ForeColor = System.Drawing.SystemColors.ControlText
		Me.CmdAdd.Cursor = System.Windows.Forms.Cursors.Default
		Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CmdAdd.TabStop = True
		Me.CmdAdd.Name = "CmdAdd"
		Me.CmdModify.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.CmdModify.Text = "&Modify"
		Me.CmdModify.Size = New System.Drawing.Size(60, 37)
		Me.CmdModify.Location = New System.Drawing.Point(150, 10)
		Me.CmdModify.Image = CType(resources.GetObject("CmdModify.Image"), System.Drawing.Image)
		Me.CmdModify.TabIndex = 18
		Me.ToolTip1.SetToolTip(Me.CmdModify, "Modify Record")
		Me.CmdModify.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CmdModify.BackColor = System.Drawing.SystemColors.Control
		Me.CmdModify.CausesValidation = True
		Me.CmdModify.Enabled = True
		Me.CmdModify.ForeColor = System.Drawing.SystemColors.ControlText
		Me.CmdModify.Cursor = System.Windows.Forms.Cursors.Default
		Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CmdModify.TabStop = True
		Me.CmdModify.Name = "CmdModify"
		Me.CmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.CmdDelete.Text = "&Delete"
		Me.CmdDelete.Size = New System.Drawing.Size(60, 37)
		Me.CmdDelete.Location = New System.Drawing.Point(330, 10)
		Me.CmdDelete.Image = CType(resources.GetObject("CmdDelete.Image"), System.Drawing.Image)
		Me.CmdDelete.TabIndex = 21
		Me.ToolTip1.SetToolTip(Me.CmdDelete, "Delete Record")
		Me.CmdDelete.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CmdDelete.BackColor = System.Drawing.SystemColors.Control
		Me.CmdDelete.CausesValidation = True
		Me.CmdDelete.Enabled = True
		Me.CmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
		Me.CmdDelete.Cursor = System.Windows.Forms.Cursors.Default
		Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CmdDelete.TabStop = True
		Me.CmdDelete.Name = "CmdDelete"
		Me.CmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.CmdSave.Text = "&Save"
		Me.CmdSave.Size = New System.Drawing.Size(60, 37)
		Me.CmdSave.Location = New System.Drawing.Point(210, 10)
		Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
		Me.CmdSave.TabIndex = 19
		Me.ToolTip1.SetToolTip(Me.CmdSave, "Save Record")
		Me.CmdSave.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
		Me.CmdSave.CausesValidation = True
		Me.CmdSave.Enabled = True
		Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
		Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
		Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CmdSave.TabStop = True
		Me.CmdSave.Name = "CmdSave"
		Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.CmdView.Text = "List &View"
		Me.CmdView.Size = New System.Drawing.Size(60, 37)
		Me.CmdView.Location = New System.Drawing.Point(510, 10)
		Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
		Me.CmdView.TabIndex = 24
		Me.ToolTip1.SetToolTip(Me.CmdView, "View List")
		Me.CmdView.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CmdView.BackColor = System.Drawing.SystemColors.Control
		Me.CmdView.CausesValidation = True
		Me.CmdView.Enabled = True
		Me.CmdView.ForeColor = System.Drawing.SystemColors.ControlText
		Me.CmdView.Cursor = System.Windows.Forms.Cursors.Default
		Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CmdView.TabStop = True
		Me.CmdView.Name = "CmdView"
		Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.CmdClose.Text = "&Close"
		Me.CmdClose.Size = New System.Drawing.Size(60, 37)
		Me.CmdClose.Location = New System.Drawing.Point(570, 10)
		Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
		Me.CmdClose.TabIndex = 25
		Me.ToolTip1.SetToolTip(Me.CmdClose, "Close Form")
		Me.CmdClose.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
		Me.CmdClose.CausesValidation = True
		Me.CmdClose.Enabled = True
		Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
		Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
		Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CmdClose.TabStop = True
		Me.CmdClose.Name = "CmdClose"
		Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.cmdPrint.Text = "&Print"
		Me.cmdPrint.Size = New System.Drawing.Size(60, 37)
		Me.cmdPrint.Location = New System.Drawing.Point(390, 10)
		Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
		Me.cmdPrint.TabIndex = 22
		Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print List")
		Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
		Me.cmdPrint.CausesValidation = True
		Me.cmdPrint.Enabled = True
		Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdPrint.TabStop = True
		Me.cmdPrint.Name = "cmdPrint"
		Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
		Me.Report1.Location = New System.Drawing.Point(6, 14)
		Me.Report1.Name = "Report1"
		Me.ADataGrid.Size = New System.Drawing.Size(125, 23)
		Me.ADataGrid.Location = New System.Drawing.Point(0, 0)
		Me.ADataGrid.CursorLocation = ADODB.CursorLocationEnum.adUseClient
		Me.ADataGrid.ConnectionTimeout = 30
		Me.ADataGrid.CommandTimeout = 30
		Me.ADataGrid.CursorType = ADODB.CursorTypeEnum.adOpenKeyset
		Me.ADataGrid.LockType = ADODB.LockTypeEnum.adLockReadOnly
		Me.ADataGrid.CommandType = ADODB.CommandTypeEnum.adCmdUnknown
		Me.ADataGrid.CacheSize = 50
		Me.ADataGrid.MaxRecords = 100000
		Me.ADataGrid.BOFAction = Microsoft.VisualBasic.Compatibility.VB6.ADODC.BOFActionEnum.adDoMoveFirst
		Me.ADataGrid.EOFAction = Microsoft.VisualBasic.Compatibility.VB6.ADODC.EOFActionEnum.adDoMoveLast
		Me.ADataGrid.BackColor = System.Drawing.SystemColors.Window
		Me.ADataGrid.ForeColor = System.Drawing.SystemColors.WindowText
		Me.ADataGrid.Orientation = Microsoft.VisualBasic.Compatibility.VB6.ADODC.OrientationEnum.adHorizontal
		Me.ADataGrid.Enabled = True
		Me.ADataGrid.UserName = ""
		Me.ADataGrid.RecordSource = ""
		Me.ADataGrid.Text = "AdodcView"
		Me.ADataGrid.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.ADataGrid.ConnectionString = ""
		Me.ADataGrid.Name = "ADataGrid"
		Me.Lbl.SetIndex(_Lbl_3, CType(3, Short))
		Me.Lbl.SetIndex(_Lbl_0, CType(0, Short))
		Me.OptSelection.SetIndex(_OptSelection_1, CType(1, Short))
		Me.OptSelection.SetIndex(_OptSelection_0, CType(0, Short))
		CType(Me.OptSelection, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.Lbl, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
		Me.Controls.Add(FraView)
		Me.Controls.Add(SprdView)
		Me.Controls.Add(FraMovement)
		Me.Controls.Add(ADataGrid)
		Me.FraView.Controls.Add(SprdMain)
		Me.FraView.Controls.Add(txtTCSAmount)
		Me.FraView.Controls.Add(txtSurcharge)
		Me.FraView.Controls.Add(txtCess)
		Me.FraView.Controls.Add(txtInterest)
		Me.FraView.Controls.Add(txtOthers)
		Me.FraView.Controls.Add(txtNetAmount)
		Me.FraView.Controls.Add(txtAmountPaid)
		Me.FraView.Controls.Add(FraChallan)
		Me.FraView.Controls.Add(AData1)
		Me.FraView.Controls.Add(Label7)
		Me.FraView.Controls.Add(Label8)
		Me.FraView.Controls.Add(Label9)
		Me.FraView.Controls.Add(Label10)
		Me.FraView.Controls.Add(Label11)
		Me.FraView.Controls.Add(Label12)
		Me.FraView.Controls.Add(Label3)
		Me.FraChallan.Controls.Add(Frame1)
		Me.FraChallan.Controls.Add(cboCollectionCode)
		Me.FraChallan.Controls.Add(txtChqDate)
		Me.FraChallan.Controls.Add(txtChqNo)
		Me.FraChallan.Controls.Add(txtBankCode)
		Me.FraChallan.Controls.Add(cmdShow)
		Me.FraChallan.Controls.Add(txtRefNo)
		Me.FraChallan.Controls.Add(txtChallanNo)
		Me.FraChallan.Controls.Add(txtChallanDate)
		Me.FraChallan.Controls.Add(txtBankName)
		Me.FraChallan.Controls.Add(txtRefDate)
		Me.FraChallan.Controls.Add(Label13)
		Me.FraChallan.Controls.Add(Label6)
		Me.FraChallan.Controls.Add(Label5)
		Me.FraChallan.Controls.Add(Label4)
		Me.FraChallan.Controls.Add(lblMKey)
		Me.FraChallan.Controls.Add(_Lbl_3)
		Me.FraChallan.Controls.Add(Label1)
		Me.FraChallan.Controls.Add(Label2)
		Me.FraChallan.Controls.Add(Lable11)
		Me.FraChallan.Controls.Add(_Lbl_0)
		Me.Frame1.Controls.Add(_OptSelection_1)
		Me.Frame1.Controls.Add(_OptSelection_0)
		Me.FraMovement.Controls.Add(cmdPreview)
		Me.FraMovement.Controls.Add(cmdSavePrint)
		Me.FraMovement.Controls.Add(CmdAdd)
		Me.FraMovement.Controls.Add(CmdModify)
		Me.FraMovement.Controls.Add(CmdDelete)
		Me.FraMovement.Controls.Add(CmdSave)
		Me.FraMovement.Controls.Add(CmdView)
		Me.FraMovement.Controls.Add(CmdClose)
		Me.FraMovement.Controls.Add(cmdPrint)
		Me.FraMovement.Controls.Add(Report1)
		Me.FraView.ResumeLayout(False)
		Me.FraChallan.ResumeLayout(False)
		Me.Frame1.ResumeLayout(False)
		Me.FraMovement.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()
	End Sub
#End Region 
#Region "Upgrade Support"
	Public Sub VB6_AddADODataBinding()
		SprdMain.DataSource = CType(AData1, MSDATASRC.DataSource)
		SprdView.DataSource = CType(ADataGrid, MSDATASRC.DataSource)
	End Sub
	Public Sub VB6_RemoveADODataBinding()
		SprdMain.DataSource = Nothing
		SprdView.DataSource = Nothing
	End Sub
#End Region 
End Class