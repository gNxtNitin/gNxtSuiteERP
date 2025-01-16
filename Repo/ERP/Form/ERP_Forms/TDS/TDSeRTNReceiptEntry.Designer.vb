<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmTDSeRTNReceiptEntry
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
	Public WithEvents TxtIVQTRDate As System.Windows.Forms.TextBox
	Public WithEvents TxtIQTRDate As System.Windows.Forms.TextBox
	Public WithEvents TxtIIQTRDate As System.Windows.Forms.TextBox
	Public WithEvents TxtIIIQTRDate As System.Windows.Forms.TextBox
	Public WithEvents TxtIVQTRNo As System.Windows.Forms.TextBox
	Public WithEvents TxtIIIQTRNo As System.Windows.Forms.TextBox
	Public WithEvents TxtIIQTRNo As System.Windows.Forms.TextBox
	Public WithEvents CmdSearch As System.Windows.Forms.Button
	Public WithEvents txtTDSName As System.Windows.Forms.TextBox
	Public WithEvents TxtIQTRNo As System.Windows.Forms.TextBox
	Public WithEvents Report1 As AxCrystal.AxCrystalReport
	Public WithEvents ADataGrid As VB6.ADODC
	Public WithEvents Label6 As System.Windows.Forms.Label
	Public WithEvents Label5 As System.Windows.Forms.Label
	Public WithEvents Label4 As System.Windows.Forms.Label
	Public WithEvents Label3 As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents Label8 As System.Windows.Forms.Label
	Public WithEvents Label7 As System.Windows.Forms.Label
	Public WithEvents _lblLabels_1 As System.Windows.Forms.Label
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents FraView As System.Windows.Forms.GroupBox
	Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
	Public WithEvents Fragridview As System.Windows.Forms.GroupBox
	Public WithEvents cmdSavePrint As System.Windows.Forms.Button
	Public WithEvents CmdPreview As System.Windows.Forms.Button
	Public WithEvents CmdClose As System.Windows.Forms.Button
	Public WithEvents cmdPrint As System.Windows.Forms.Button
	Public WithEvents CmdView As System.Windows.Forms.Button
	Public WithEvents CmdDelete As System.Windows.Forms.Button
	Public WithEvents CmdSave As System.Windows.Forms.Button
	Public WithEvents CmdModify As System.Windows.Forms.Button
	Public WithEvents CmdAdd As System.Windows.Forms.Button
	Public WithEvents FraMovement As System.Windows.Forms.GroupBox
	Public WithEvents lblLabels As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmTDSeRTNReceiptEntry))
		Me.components = New System.ComponentModel.Container()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
		Me.FraView = New System.Windows.Forms.GroupBox
		Me.TxtIVQTRDate = New System.Windows.Forms.TextBox
		Me.TxtIQTRDate = New System.Windows.Forms.TextBox
		Me.TxtIIQTRDate = New System.Windows.Forms.TextBox
		Me.TxtIIIQTRDate = New System.Windows.Forms.TextBox
		Me.TxtIVQTRNo = New System.Windows.Forms.TextBox
		Me.TxtIIIQTRNo = New System.Windows.Forms.TextBox
		Me.TxtIIQTRNo = New System.Windows.Forms.TextBox
		Me.CmdSearch = New System.Windows.Forms.Button
		Me.txtTDSName = New System.Windows.Forms.TextBox
		Me.TxtIQTRNo = New System.Windows.Forms.TextBox
		Me.Report1 = New AxCrystal.AxCrystalReport
		Me.ADataGrid = New VB6.ADODC
		Me.Label6 = New System.Windows.Forms.Label
		Me.Label5 = New System.Windows.Forms.Label
		Me.Label4 = New System.Windows.Forms.Label
		Me.Label3 = New System.Windows.Forms.Label
		Me.Label1 = New System.Windows.Forms.Label
		Me.Label8 = New System.Windows.Forms.Label
		Me.Label7 = New System.Windows.Forms.Label
		Me._lblLabels_1 = New System.Windows.Forms.Label
		Me.Label2 = New System.Windows.Forms.Label
		Me.Fragridview = New System.Windows.Forms.GroupBox
		Me.SprdView = New AxFPSpreadADO.AxfpSpread
		Me.FraMovement = New System.Windows.Forms.GroupBox
		Me.cmdSavePrint = New System.Windows.Forms.Button
		Me.CmdPreview = New System.Windows.Forms.Button
		Me.CmdClose = New System.Windows.Forms.Button
		Me.cmdPrint = New System.Windows.Forms.Button
		Me.CmdView = New System.Windows.Forms.Button
		Me.CmdDelete = New System.Windows.Forms.Button
		Me.CmdSave = New System.Windows.Forms.Button
		Me.CmdModify = New System.Windows.Forms.Button
		Me.CmdAdd = New System.Windows.Forms.Button
		Me.lblLabels = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(components)
		Me.FraView.SuspendLayout()
		Me.Fragridview.SuspendLayout()
		Me.FraMovement.SuspendLayout()
		Me.SuspendLayout()
		Me.ToolTip1.Active = True
		CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.lblLabels, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
		Me.Text = "TDS eRTN Receipt Entry"
		Me.ClientSize = New System.Drawing.Size(551, 210)
		Me.Location = New System.Drawing.Point(73, 22)
		Me.Icon = CType(resources.GetObject("frmTDSeRTNReceiptEntry.Icon"), System.Drawing.Icon)
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
		Me.Name = "frmTDSeRTNReceiptEntry"
		Me.FraView.BackColor = System.Drawing.SystemColors.ActiveBorder
		Me.FraView.Size = New System.Drawing.Size(549, 167)
		Me.FraView.Location = New System.Drawing.Point(1, -5)
		Me.FraView.TabIndex = 19
		Me.FraView.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.FraView.Enabled = True
		Me.FraView.ForeColor = System.Drawing.SystemColors.ControlText
		Me.FraView.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.FraView.Visible = True
		Me.FraView.Padding = New System.Windows.Forms.Padding(0)
		Me.FraView.Name = "FraView"
		Me.TxtIVQTRDate.AutoSize = False
		Me.TxtIVQTRDate.Size = New System.Drawing.Size(93, 19)
		Me.TxtIVQTRDate.Location = New System.Drawing.Point(410, 136)
		Me.TxtIVQTRDate.TabIndex = 10
		Me.TxtIVQTRDate.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.TxtIVQTRDate.AcceptsReturn = True
		Me.TxtIVQTRDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.TxtIVQTRDate.BackColor = System.Drawing.SystemColors.Window
		Me.TxtIVQTRDate.CausesValidation = True
		Me.TxtIVQTRDate.Enabled = True
		Me.TxtIVQTRDate.ForeColor = System.Drawing.SystemColors.WindowText
		Me.TxtIVQTRDate.HideSelection = True
		Me.TxtIVQTRDate.ReadOnly = False
		Me.TxtIVQTRDate.Maxlength = 0
		Me.TxtIVQTRDate.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.TxtIVQTRDate.MultiLine = False
		Me.TxtIVQTRDate.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.TxtIVQTRDate.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.TxtIVQTRDate.TabStop = True
		Me.TxtIVQTRDate.Visible = True
		Me.TxtIVQTRDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.TxtIVQTRDate.Name = "TxtIVQTRDate"
		Me.TxtIQTRDate.AutoSize = False
		Me.TxtIQTRDate.Size = New System.Drawing.Size(93, 19)
		Me.TxtIQTRDate.Location = New System.Drawing.Point(410, 48)
		Me.TxtIQTRDate.TabIndex = 4
		Me.TxtIQTRDate.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.TxtIQTRDate.AcceptsReturn = True
		Me.TxtIQTRDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.TxtIQTRDate.BackColor = System.Drawing.SystemColors.Window
		Me.TxtIQTRDate.CausesValidation = True
		Me.TxtIQTRDate.Enabled = True
		Me.TxtIQTRDate.ForeColor = System.Drawing.SystemColors.WindowText
		Me.TxtIQTRDate.HideSelection = True
		Me.TxtIQTRDate.ReadOnly = False
		Me.TxtIQTRDate.Maxlength = 0
		Me.TxtIQTRDate.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.TxtIQTRDate.MultiLine = False
		Me.TxtIQTRDate.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.TxtIQTRDate.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.TxtIQTRDate.TabStop = True
		Me.TxtIQTRDate.Visible = True
		Me.TxtIQTRDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.TxtIQTRDate.Name = "TxtIQTRDate"
		Me.TxtIIQTRDate.AutoSize = False
		Me.TxtIIQTRDate.Size = New System.Drawing.Size(93, 19)
		Me.TxtIIQTRDate.Location = New System.Drawing.Point(410, 76)
		Me.TxtIIQTRDate.TabIndex = 6
		Me.TxtIIQTRDate.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.TxtIIQTRDate.AcceptsReturn = True
		Me.TxtIIQTRDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.TxtIIQTRDate.BackColor = System.Drawing.SystemColors.Window
		Me.TxtIIQTRDate.CausesValidation = True
		Me.TxtIIQTRDate.Enabled = True
		Me.TxtIIQTRDate.ForeColor = System.Drawing.SystemColors.WindowText
		Me.TxtIIQTRDate.HideSelection = True
		Me.TxtIIQTRDate.ReadOnly = False
		Me.TxtIIQTRDate.Maxlength = 0
		Me.TxtIIQTRDate.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.TxtIIQTRDate.MultiLine = False
		Me.TxtIIQTRDate.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.TxtIIQTRDate.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.TxtIIQTRDate.TabStop = True
		Me.TxtIIQTRDate.Visible = True
		Me.TxtIIQTRDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.TxtIIQTRDate.Name = "TxtIIQTRDate"
		Me.TxtIIIQTRDate.AutoSize = False
		Me.TxtIIIQTRDate.Size = New System.Drawing.Size(93, 19)
		Me.TxtIIIQTRDate.Location = New System.Drawing.Point(410, 106)
		Me.TxtIIIQTRDate.TabIndex = 8
		Me.TxtIIIQTRDate.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.TxtIIIQTRDate.AcceptsReturn = True
		Me.TxtIIIQTRDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.TxtIIIQTRDate.BackColor = System.Drawing.SystemColors.Window
		Me.TxtIIIQTRDate.CausesValidation = True
		Me.TxtIIIQTRDate.Enabled = True
		Me.TxtIIIQTRDate.ForeColor = System.Drawing.SystemColors.WindowText
		Me.TxtIIIQTRDate.HideSelection = True
		Me.TxtIIIQTRDate.ReadOnly = False
		Me.TxtIIIQTRDate.Maxlength = 0
		Me.TxtIIIQTRDate.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.TxtIIIQTRDate.MultiLine = False
		Me.TxtIIIQTRDate.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.TxtIIIQTRDate.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.TxtIIIQTRDate.TabStop = True
		Me.TxtIIIQTRDate.Visible = True
		Me.TxtIIIQTRDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.TxtIIIQTRDate.Name = "TxtIIIQTRDate"
		Me.TxtIVQTRNo.AutoSize = False
		Me.TxtIVQTRNo.Size = New System.Drawing.Size(155, 19)
		Me.TxtIVQTRNo.Location = New System.Drawing.Point(138, 136)
		Me.TxtIVQTRNo.TabIndex = 9
		Me.TxtIVQTRNo.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.TxtIVQTRNo.AcceptsReturn = True
		Me.TxtIVQTRNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.TxtIVQTRNo.BackColor = System.Drawing.SystemColors.Window
		Me.TxtIVQTRNo.CausesValidation = True
		Me.TxtIVQTRNo.Enabled = True
		Me.TxtIVQTRNo.ForeColor = System.Drawing.SystemColors.WindowText
		Me.TxtIVQTRNo.HideSelection = True
		Me.TxtIVQTRNo.ReadOnly = False
		Me.TxtIVQTRNo.Maxlength = 0
		Me.TxtIVQTRNo.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.TxtIVQTRNo.MultiLine = False
		Me.TxtIVQTRNo.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.TxtIVQTRNo.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.TxtIVQTRNo.TabStop = True
		Me.TxtIVQTRNo.Visible = True
		Me.TxtIVQTRNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.TxtIVQTRNo.Name = "TxtIVQTRNo"
		Me.TxtIIIQTRNo.AutoSize = False
		Me.TxtIIIQTRNo.Size = New System.Drawing.Size(155, 19)
		Me.TxtIIIQTRNo.Location = New System.Drawing.Point(138, 106)
		Me.TxtIIIQTRNo.TabIndex = 7
		Me.TxtIIIQTRNo.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.TxtIIIQTRNo.AcceptsReturn = True
		Me.TxtIIIQTRNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.TxtIIIQTRNo.BackColor = System.Drawing.SystemColors.Window
		Me.TxtIIIQTRNo.CausesValidation = True
		Me.TxtIIIQTRNo.Enabled = True
		Me.TxtIIIQTRNo.ForeColor = System.Drawing.SystemColors.WindowText
		Me.TxtIIIQTRNo.HideSelection = True
		Me.TxtIIIQTRNo.ReadOnly = False
		Me.TxtIIIQTRNo.Maxlength = 0
		Me.TxtIIIQTRNo.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.TxtIIIQTRNo.MultiLine = False
		Me.TxtIIIQTRNo.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.TxtIIIQTRNo.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.TxtIIIQTRNo.TabStop = True
		Me.TxtIIIQTRNo.Visible = True
		Me.TxtIIIQTRNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.TxtIIIQTRNo.Name = "TxtIIIQTRNo"
		Me.TxtIIQTRNo.AutoSize = False
		Me.TxtIIQTRNo.Size = New System.Drawing.Size(155, 19)
		Me.TxtIIQTRNo.Location = New System.Drawing.Point(138, 76)
		Me.TxtIIQTRNo.TabIndex = 5
		Me.TxtIIQTRNo.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.TxtIIQTRNo.AcceptsReturn = True
		Me.TxtIIQTRNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.TxtIIQTRNo.BackColor = System.Drawing.SystemColors.Window
		Me.TxtIIQTRNo.CausesValidation = True
		Me.TxtIIQTRNo.Enabled = True
		Me.TxtIIQTRNo.ForeColor = System.Drawing.SystemColors.WindowText
		Me.TxtIIQTRNo.HideSelection = True
		Me.TxtIIQTRNo.ReadOnly = False
		Me.TxtIIQTRNo.Maxlength = 0
		Me.TxtIIQTRNo.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.TxtIIQTRNo.MultiLine = False
		Me.TxtIIQTRNo.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.TxtIIQTRNo.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.TxtIIQTRNo.TabStop = True
		Me.TxtIIQTRNo.Visible = True
		Me.TxtIIQTRNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.TxtIIQTRNo.Name = "TxtIIQTRNo"
		Me.CmdSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.CmdSearch.BackColor = System.Drawing.SystemColors.Menu
		Me.CmdSearch.Size = New System.Drawing.Size(27, 19)
		Me.CmdSearch.Location = New System.Drawing.Point(504, 20)
		Me.CmdSearch.Image = CType(resources.GetObject("CmdSearch.Image"), System.Drawing.Image)
		Me.CmdSearch.TabIndex = 2
		Me.CmdSearch.TabStop = False
		Me.ToolTip1.SetToolTip(Me.CmdSearch, "Search")
		Me.CmdSearch.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CmdSearch.CausesValidation = True
		Me.CmdSearch.Enabled = True
		Me.CmdSearch.ForeColor = System.Drawing.SystemColors.ControlText
		Me.CmdSearch.Cursor = System.Windows.Forms.Cursors.Default
		Me.CmdSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CmdSearch.Name = "CmdSearch"
		Me.txtTDSName.AutoSize = False
		Me.txtTDSName.Size = New System.Drawing.Size(365, 19)
		Me.txtTDSName.Location = New System.Drawing.Point(138, 20)
		Me.txtTDSName.TabIndex = 1
		Me.txtTDSName.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtTDSName.AcceptsReturn = True
		Me.txtTDSName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtTDSName.BackColor = System.Drawing.SystemColors.Window
		Me.txtTDSName.CausesValidation = True
		Me.txtTDSName.Enabled = True
		Me.txtTDSName.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtTDSName.HideSelection = True
		Me.txtTDSName.ReadOnly = False
		Me.txtTDSName.Maxlength = 0
		Me.txtTDSName.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtTDSName.MultiLine = False
		Me.txtTDSName.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtTDSName.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtTDSName.TabStop = True
		Me.txtTDSName.Visible = True
		Me.txtTDSName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtTDSName.Name = "txtTDSName"
		Me.TxtIQTRNo.AutoSize = False
		Me.TxtIQTRNo.Size = New System.Drawing.Size(155, 19)
		Me.TxtIQTRNo.Location = New System.Drawing.Point(138, 48)
		Me.TxtIQTRNo.TabIndex = 3
		Me.TxtIQTRNo.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.TxtIQTRNo.AcceptsReturn = True
		Me.TxtIQTRNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.TxtIQTRNo.BackColor = System.Drawing.SystemColors.Window
		Me.TxtIQTRNo.CausesValidation = True
		Me.TxtIQTRNo.Enabled = True
		Me.TxtIQTRNo.ForeColor = System.Drawing.SystemColors.WindowText
		Me.TxtIQTRNo.HideSelection = True
		Me.TxtIQTRNo.ReadOnly = False
		Me.TxtIQTRNo.Maxlength = 0
		Me.TxtIQTRNo.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.TxtIQTRNo.MultiLine = False
		Me.TxtIQTRNo.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.TxtIQTRNo.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.TxtIQTRNo.TabStop = True
		Me.TxtIQTRNo.Visible = True
		Me.TxtIQTRNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.TxtIQTRNo.Name = "TxtIQTRNo"
		Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
		Me.Report1.Location = New System.Drawing.Point(4, 116)
		Me.Report1.Name = "Report1"
		Me.ADataGrid.Size = New System.Drawing.Size(89, 22)
		Me.ADataGrid.Location = New System.Drawing.Point(4, 60)
		Me.ADataGrid.Visible = 0
		Me.ADataGrid.CursorLocation = ADODB.CursorLocationEnum.adUseClient
		Me.ADataGrid.ConnectionTimeout = 15
		Me.ADataGrid.CommandTimeout = 30
		Me.ADataGrid.CursorType = ADODB.CursorTypeEnum.adOpenStatic
		Me.ADataGrid.LockType = ADODB.LockTypeEnum.adLockOptimistic
		Me.ADataGrid.CommandType = ADODB.CommandTypeEnum.adCmdUnknown
		Me.ADataGrid.CacheSize = 50
		Me.ADataGrid.MaxRecords = 0
		Me.ADataGrid.BOFAction = Microsoft.VisualBasic.Compatibility.VB6.ADODC.BOFActionEnum.adDoMoveFirst
		Me.ADataGrid.EOFAction = Microsoft.VisualBasic.Compatibility.VB6.ADODC.EOFActionEnum.adDoMoveLast
		Me.ADataGrid.BackColor = System.Drawing.SystemColors.Window
		Me.ADataGrid.ForeColor = System.Drawing.SystemColors.WindowText
		Me.ADataGrid.Orientation = Microsoft.VisualBasic.Compatibility.VB6.ADODC.OrientationEnum.adHorizontal
		Me.ADataGrid.Enabled = True
		Me.ADataGrid.UserName = ""
		Me.ADataGrid.RecordSource = ""
		Me.ADataGrid.Text = "Adodc1"
		Me.ADataGrid.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.ADataGrid.ConnectionString = ""
		Me.ADataGrid.Name = "ADataGrid"
		Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.Label6.Text = "IVth QTR  Date:"
		Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label6.Size = New System.Drawing.Size(93, 13)
		Me.Label6.Location = New System.Drawing.Point(314, 138)
		Me.Label6.TabIndex = 31
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
		Me.Label5.Text = "Ist QTR Date:"
		Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label5.Size = New System.Drawing.Size(80, 13)
		Me.Label5.Location = New System.Drawing.Point(327, 50)
		Me.Label5.TabIndex = 30
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
		Me.Label4.Text = "IInd QTR  Date:"
		Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label4.Size = New System.Drawing.Size(92, 13)
		Me.Label4.Location = New System.Drawing.Point(315, 78)
		Me.Label4.TabIndex = 29
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
		Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.Label3.Text = "IIIrd QTR  Date:"
		Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label3.Size = New System.Drawing.Size(93, 13)
		Me.Label3.Location = New System.Drawing.Point(314, 108)
		Me.Label3.TabIndex = 28
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
		Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.Label1.Text = "IVth QTR :"
		Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.Size = New System.Drawing.Size(95, 13)
		Me.Label1.Location = New System.Drawing.Point(40, 138)
		Me.Label1.TabIndex = 27
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
		Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.Label8.Text = "IIIrd QTR :"
		Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label8.Size = New System.Drawing.Size(95, 13)
		Me.Label8.Location = New System.Drawing.Point(40, 108)
		Me.Label8.TabIndex = 25
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
		Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.Label7.Text = "IInd QTR :"
		Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label7.Size = New System.Drawing.Size(95, 13)
		Me.Label7.Location = New System.Drawing.Point(40, 78)
		Me.Label7.TabIndex = 21
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
		Me._lblLabels_1.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me._lblLabels_1.Text = "TDS A/c Name :"
		Me._lblLabels_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me._lblLabels_1.Size = New System.Drawing.Size(95, 13)
		Me._lblLabels_1.Location = New System.Drawing.Point(40, 22)
		Me._lblLabels_1.TabIndex = 20
		Me._lblLabels_1.BackColor = System.Drawing.SystemColors.Control
		Me._lblLabels_1.Enabled = True
		Me._lblLabels_1.ForeColor = System.Drawing.SystemColors.ControlText
		Me._lblLabels_1.Cursor = System.Windows.Forms.Cursors.Default
		Me._lblLabels_1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._lblLabels_1.UseMnemonic = True
		Me._lblLabels_1.Visible = True
		Me._lblLabels_1.AutoSize = True
		Me._lblLabels_1.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me._lblLabels_1.Name = "_lblLabels_1"
		Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.Label2.Text = "Ist QTR :"
		Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label2.Size = New System.Drawing.Size(95, 13)
		Me.Label2.Location = New System.Drawing.Point(40, 50)
		Me.Label2.TabIndex = 22
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
		Me.Fragridview.Size = New System.Drawing.Size(551, 167)
		Me.Fragridview.Location = New System.Drawing.Point(0, -5)
		Me.Fragridview.TabIndex = 23
		Me.Fragridview.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Fragridview.BackColor = System.Drawing.SystemColors.Control
		Me.Fragridview.Enabled = True
		Me.Fragridview.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Fragridview.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Fragridview.Visible = True
		Me.Fragridview.Padding = New System.Windows.Forms.Padding(0)
		Me.Fragridview.Name = "Fragridview"
		SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SprdView.Size = New System.Drawing.Size(545, 159)
		Me.SprdView.Location = New System.Drawing.Point(2, 8)
		Me.SprdView.TabIndex = 26
		Me.SprdView.Name = "SprdView"
		Me.FraMovement.Size = New System.Drawing.Size(548, 53)
		Me.FraMovement.Location = New System.Drawing.Point(2, 157)
		Me.FraMovement.TabIndex = 24
		Me.FraMovement.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
		Me.FraMovement.Enabled = True
		Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
		Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.FraMovement.Visible = True
		Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
		Me.FraMovement.Name = "FraMovement"
		Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.cmdSavePrint.Text = "Save&&Print"
		Me.cmdSavePrint.Size = New System.Drawing.Size(60, 37)
		Me.cmdSavePrint.Location = New System.Drawing.Point(184, 12)
		Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
		Me.cmdSavePrint.TabIndex = 13
		Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save and Print the Current Bill")
		Me.cmdSavePrint.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
		Me.cmdSavePrint.CausesValidation = True
		Me.cmdSavePrint.Enabled = True
		Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdSavePrint.TabStop = True
		Me.cmdSavePrint.Name = "cmdSavePrint"
		Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.CmdPreview.Text = "Pre&view"
		Me.CmdPreview.Enabled = False
		Me.CmdPreview.Size = New System.Drawing.Size(60, 37)
		Me.CmdPreview.Location = New System.Drawing.Point(364, 12)
		Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
		Me.CmdPreview.TabIndex = 16
		Me.ToolTip1.SetToolTip(Me.CmdPreview, "Print PO")
		Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
		Me.CmdPreview.CausesValidation = True
		Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
		Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
		Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CmdPreview.TabStop = True
		Me.CmdPreview.Name = "CmdPreview"
		Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.CmdClose.Text = "&Close"
		Me.CmdClose.Size = New System.Drawing.Size(60, 37)
		Me.CmdClose.Location = New System.Drawing.Point(484, 12)
		Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
		Me.CmdClose.TabIndex = 18
		Me.ToolTip1.SetToolTip(Me.CmdClose, "Close the Form")
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
		Me.cmdPrint.Location = New System.Drawing.Point(304, 12)
		Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
		Me.cmdPrint.TabIndex = 15
		Me.ToolTip1.SetToolTip(Me.cmdPrint, "Close the Form")
		Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
		Me.cmdPrint.CausesValidation = True
		Me.cmdPrint.Enabled = True
		Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdPrint.TabStop = True
		Me.cmdPrint.Name = "cmdPrint"
		Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.CmdView.Text = "List &View"
		Me.CmdView.Size = New System.Drawing.Size(60, 37)
		Me.CmdView.Location = New System.Drawing.Point(424, 12)
		Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
		Me.CmdView.TabIndex = 17
		Me.ToolTip1.SetToolTip(Me.CmdView, "Close the Form")
		Me.CmdView.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CmdView.BackColor = System.Drawing.SystemColors.Control
		Me.CmdView.CausesValidation = True
		Me.CmdView.Enabled = True
		Me.CmdView.ForeColor = System.Drawing.SystemColors.ControlText
		Me.CmdView.Cursor = System.Windows.Forms.Cursors.Default
		Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CmdView.TabStop = True
		Me.CmdView.Name = "CmdView"
		Me.CmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.CmdDelete.Text = "&Delete"
		Me.CmdDelete.Size = New System.Drawing.Size(60, 37)
		Me.CmdDelete.Location = New System.Drawing.Point(244, 12)
		Me.CmdDelete.Image = CType(resources.GetObject("CmdDelete.Image"), System.Drawing.Image)
		Me.CmdDelete.TabIndex = 14
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
		Me.CmdSave.Location = New System.Drawing.Point(124, 12)
		Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
		Me.CmdSave.TabIndex = 12
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
		Me.CmdModify.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.CmdModify.Text = "&Modify"
		Me.CmdModify.Size = New System.Drawing.Size(60, 37)
		Me.CmdModify.Location = New System.Drawing.Point(64, 12)
		Me.CmdModify.Image = CType(resources.GetObject("CmdModify.Image"), System.Drawing.Image)
		Me.CmdModify.TabIndex = 11
		Me.ToolTip1.SetToolTip(Me.CmdModify, "Refresh Record(s)")
		Me.CmdModify.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CmdModify.BackColor = System.Drawing.SystemColors.Control
		Me.CmdModify.CausesValidation = True
		Me.CmdModify.Enabled = True
		Me.CmdModify.ForeColor = System.Drawing.SystemColors.ControlText
		Me.CmdModify.Cursor = System.Windows.Forms.Cursors.Default
		Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CmdModify.TabStop = True
		Me.CmdModify.Name = "CmdModify"
		Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.CmdAdd.Text = "&Add"
		Me.CmdAdd.Size = New System.Drawing.Size(60, 37)
		Me.CmdAdd.Location = New System.Drawing.Point(4, 12)
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
		Me.lblLabels.SetIndex(_lblLabels_1, CType(1, Short))
		CType(Me.lblLabels, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.Controls.Add(FraView)
		Me.Controls.Add(Fragridview)
		Me.Controls.Add(FraMovement)
		Me.FraView.Controls.Add(TxtIVQTRDate)
		Me.FraView.Controls.Add(TxtIQTRDate)
		Me.FraView.Controls.Add(TxtIIQTRDate)
		Me.FraView.Controls.Add(TxtIIIQTRDate)
		Me.FraView.Controls.Add(TxtIVQTRNo)
		Me.FraView.Controls.Add(TxtIIIQTRNo)
		Me.FraView.Controls.Add(TxtIIQTRNo)
		Me.FraView.Controls.Add(CmdSearch)
		Me.FraView.Controls.Add(txtTDSName)
		Me.FraView.Controls.Add(TxtIQTRNo)
		Me.FraView.Controls.Add(Report1)
		Me.FraView.Controls.Add(ADataGrid)
		Me.FraView.Controls.Add(Label6)
		Me.FraView.Controls.Add(Label5)
		Me.FraView.Controls.Add(Label4)
		Me.FraView.Controls.Add(Label3)
		Me.FraView.Controls.Add(Label1)
		Me.FraView.Controls.Add(Label8)
		Me.FraView.Controls.Add(Label7)
		Me.FraView.Controls.Add(_lblLabels_1)
		Me.FraView.Controls.Add(Label2)
		Me.Fragridview.Controls.Add(SprdView)
		Me.FraMovement.Controls.Add(cmdSavePrint)
		Me.FraMovement.Controls.Add(CmdPreview)
		Me.FraMovement.Controls.Add(CmdClose)
		Me.FraMovement.Controls.Add(cmdPrint)
		Me.FraMovement.Controls.Add(CmdView)
		Me.FraMovement.Controls.Add(CmdDelete)
		Me.FraMovement.Controls.Add(CmdSave)
		Me.FraMovement.Controls.Add(CmdModify)
		Me.FraMovement.Controls.Add(CmdAdd)
		Me.FraView.ResumeLayout(False)
		Me.Fragridview.ResumeLayout(False)
		Me.FraMovement.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()
	End Sub
#End Region 
#Region "Upgrade Support"
	Public Sub VB6_AddADODataBinding()
		SprdView.DataSource = CType(ADataGrid, MSDATASRC.DataSource)
	End Sub
	Public Sub VB6_RemoveADODataBinding()
		SprdView.DataSource = Nothing
	End Sub
#End Region 
End Class