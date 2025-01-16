Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmTrfOPBalanceAsset
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
        'Me.MDIParent = MIS.Master
        'MIS.Master.Show
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
	Public WithEvents cmdsearchDepr As System.Windows.Forms.Button
	Public WithEvents txtDeprMode As System.Windows.Forms.TextBox
	Public WithEvents SLM As System.Windows.Forms.GroupBox
	Public WithEvents _TxtDisplayTransfer_1 As System.Windows.Forms.TextBox
	Public WithEvents _TxtDisplayTransfer_0 As System.Windows.Forms.TextBox
	Public WithEvents OptParticularAccount As System.Windows.Forms.RadioButton
	Public WithEvents OptAllAccount As System.Windows.Forms.RadioButton
	Public WithEvents txtName As System.Windows.Forms.TextBox
	Public WithEvents cmdSearch As System.Windows.Forms.Button
	Public WithEvents FraAccount As System.Windows.Forms.GroupBox
	Public WithEvents CboFYearTo As System.Windows.Forms.ComboBox
	Public WithEvents CboFYearFrom As System.Windows.Forms.ComboBox
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents FraFYear As System.Windows.Forms.GroupBox
	Public WithEvents cmdClose As System.Windows.Forms.Button
	Public WithEvents cmdStart As System.Windows.Forms.Button
	Public WithEvents FraButton As System.Windows.Forms.GroupBox
	Public WithEvents TxtDisplayTransfer As Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmTrfOPBalanceAsset))
		Me.components = New System.ComponentModel.Container()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
		Me.SLM = New System.Windows.Forms.GroupBox
		Me.cmdsearchDepr = New System.Windows.Forms.Button
		Me.txtDeprMode = New System.Windows.Forms.TextBox
		Me._TxtDisplayTransfer_1 = New System.Windows.Forms.TextBox
		Me._TxtDisplayTransfer_0 = New System.Windows.Forms.TextBox
		Me.FraAccount = New System.Windows.Forms.GroupBox
		Me.OptParticularAccount = New System.Windows.Forms.RadioButton
		Me.OptAllAccount = New System.Windows.Forms.RadioButton
		Me.txtName = New System.Windows.Forms.TextBox
		Me.cmdSearch = New System.Windows.Forms.Button
		Me.FraFYear = New System.Windows.Forms.GroupBox
		Me.CboFYearTo = New System.Windows.Forms.ComboBox
		Me.CboFYearFrom = New System.Windows.Forms.ComboBox
		Me.Label2 = New System.Windows.Forms.Label
		Me.Label1 = New System.Windows.Forms.Label
		Me.FraButton = New System.Windows.Forms.GroupBox
		Me.cmdClose = New System.Windows.Forms.Button
		Me.cmdStart = New System.Windows.Forms.Button
		Me.TxtDisplayTransfer = New Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray(components)
		Me.SLM.SuspendLayout()
		Me.FraAccount.SuspendLayout()
		Me.FraFYear.SuspendLayout()
		Me.FraButton.SuspendLayout()
		Me.SuspendLayout()
		Me.ToolTip1.Active = True
		CType(Me.TxtDisplayTransfer, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.Text = "Transfer Assets Opening Balance"
		Me.ClientSize = New System.Drawing.Size(340, 346)
		Me.Location = New System.Drawing.Point(4, 23)
		Me.MaximizeBox = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds
		Me.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.SystemColors.Control
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable
		Me.ControlBox = True
		Me.Enabled = True
		Me.KeyPreview = False
		Me.MinimizeBox = True
		Me.Cursor = System.Windows.Forms.Cursors.Default
		Me.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.ShowInTaskbar = True
		Me.HelpButton = False
		Me.WindowState = System.Windows.Forms.FormWindowState.Normal
		Me.Name = "frmTrfOPBalanceAsset"
		Me.SLM.Text = "Mode"
		Me.SLM.ForeColor = System.Drawing.Color.FromARGB(128, 0, 0)
		Me.SLM.Size = New System.Drawing.Size(77, 53)
		Me.SLM.Location = New System.Drawing.Point(262, 62)
		Me.SLM.TabIndex = 15
		Me.SLM.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.SLM.BackColor = System.Drawing.SystemColors.Control
		Me.SLM.Enabled = True
		Me.SLM.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.SLM.Visible = True
		Me.SLM.Padding = New System.Windows.Forms.Padding(0)
		Me.SLM.Name = "SLM"
		Me.cmdsearchDepr.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.cmdsearchDepr.BackColor = System.Drawing.SystemColors.Menu
		Me.cmdsearchDepr.Size = New System.Drawing.Size(23, 19)
		Me.cmdsearchDepr.Location = New System.Drawing.Point(50, 28)
		Me.cmdsearchDepr.Image = CType(resources.GetObject("cmdsearchDepr.Image"), System.Drawing.Image)
		Me.cmdsearchDepr.TabIndex = 17
		Me.cmdsearchDepr.TabStop = False
		Me.ToolTip1.SetToolTip(Me.cmdsearchDepr, "Search")
		Me.cmdsearchDepr.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdsearchDepr.CausesValidation = True
		Me.cmdsearchDepr.Enabled = True
		Me.cmdsearchDepr.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdsearchDepr.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdsearchDepr.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdsearchDepr.Name = "cmdsearchDepr"
		Me.txtDeprMode.AutoSize = False
		Me.txtDeprMode.BackColor = System.Drawing.Color.White
		Me.txtDeprMode.ForeColor = System.Drawing.Color.Blue
		Me.txtDeprMode.Size = New System.Drawing.Size(45, 19)
		Me.txtDeprMode.Location = New System.Drawing.Point(4, 28)
		Me.txtDeprMode.TabIndex = 16
		Me.txtDeprMode.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtDeprMode.AcceptsReturn = True
		Me.txtDeprMode.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtDeprMode.CausesValidation = True
		Me.txtDeprMode.Enabled = True
		Me.txtDeprMode.HideSelection = True
		Me.txtDeprMode.ReadOnly = False
		Me.txtDeprMode.Maxlength = 0
		Me.txtDeprMode.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtDeprMode.MultiLine = False
		Me.txtDeprMode.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtDeprMode.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtDeprMode.TabStop = True
		Me.txtDeprMode.Visible = True
		Me.txtDeprMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtDeprMode.Name = "txtDeprMode"
		Me._TxtDisplayTransfer_1.AutoSize = False
		Me._TxtDisplayTransfer_1.BackColor = System.Drawing.Color.Black
		Me._TxtDisplayTransfer_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me._TxtDisplayTransfer_1.ForeColor = System.Drawing.SystemColors.Window
		Me._TxtDisplayTransfer_1.Size = New System.Drawing.Size(335, 115)
		Me._TxtDisplayTransfer_1.Location = New System.Drawing.Point(0, 190)
		Me._TxtDisplayTransfer_1.MultiLine = True
		Me._TxtDisplayTransfer_1.TabIndex = 13
		Me._TxtDisplayTransfer_1.AcceptsReturn = True
		Me._TxtDisplayTransfer_1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me._TxtDisplayTransfer_1.CausesValidation = True
		Me._TxtDisplayTransfer_1.Enabled = True
		Me._TxtDisplayTransfer_1.HideSelection = True
		Me._TxtDisplayTransfer_1.ReadOnly = False
		Me._TxtDisplayTransfer_1.Maxlength = 0
		Me._TxtDisplayTransfer_1.Cursor = System.Windows.Forms.Cursors.IBeam
		Me._TxtDisplayTransfer_1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._TxtDisplayTransfer_1.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me._TxtDisplayTransfer_1.TabStop = True
		Me._TxtDisplayTransfer_1.Visible = True
		Me._TxtDisplayTransfer_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me._TxtDisplayTransfer_1.Name = "_TxtDisplayTransfer_1"
		Me._TxtDisplayTransfer_0.AutoSize = False
		Me._TxtDisplayTransfer_0.BackColor = System.Drawing.Color.Black
		Me._TxtDisplayTransfer_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me._TxtDisplayTransfer_0.ForeColor = System.Drawing.SystemColors.Window
		Me._TxtDisplayTransfer_0.Size = New System.Drawing.Size(339, 189)
		Me._TxtDisplayTransfer_0.Location = New System.Drawing.Point(0, 116)
		Me._TxtDisplayTransfer_0.MultiLine = True
		Me._TxtDisplayTransfer_0.TabIndex = 14
		Me._TxtDisplayTransfer_0.AcceptsReturn = True
		Me._TxtDisplayTransfer_0.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me._TxtDisplayTransfer_0.CausesValidation = True
		Me._TxtDisplayTransfer_0.Enabled = True
		Me._TxtDisplayTransfer_0.HideSelection = True
		Me._TxtDisplayTransfer_0.ReadOnly = False
		Me._TxtDisplayTransfer_0.Maxlength = 0
		Me._TxtDisplayTransfer_0.Cursor = System.Windows.Forms.Cursors.IBeam
		Me._TxtDisplayTransfer_0.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._TxtDisplayTransfer_0.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me._TxtDisplayTransfer_0.TabStop = True
		Me._TxtDisplayTransfer_0.Visible = True
		Me._TxtDisplayTransfer_0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me._TxtDisplayTransfer_0.Name = "_TxtDisplayTransfer_0"
		Me.FraAccount.Text = "Account"
		Me.FraAccount.ForeColor = System.Drawing.Color.FromARGB(128, 0, 0)
		Me.FraAccount.Size = New System.Drawing.Size(261, 53)
		Me.FraAccount.Location = New System.Drawing.Point(0, 62)
		Me.FraAccount.TabIndex = 8
		Me.FraAccount.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.FraAccount.BackColor = System.Drawing.SystemColors.Control
		Me.FraAccount.Enabled = True
		Me.FraAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.FraAccount.Visible = True
		Me.FraAccount.Padding = New System.Windows.Forms.Padding(0)
		Me.FraAccount.Name = "FraAccount"
		Me.OptParticularAccount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.OptParticularAccount.Text = "Particular"
		Me.OptParticularAccount.Size = New System.Drawing.Size(75, 13)
		Me.OptParticularAccount.Location = New System.Drawing.Point(24, 12)
		Me.OptParticularAccount.TabIndex = 12
		Me.OptParticularAccount.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.OptParticularAccount.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.OptParticularAccount.BackColor = System.Drawing.SystemColors.Control
		Me.OptParticularAccount.CausesValidation = True
		Me.OptParticularAccount.Enabled = True
		Me.OptParticularAccount.ForeColor = System.Drawing.SystemColors.ControlText
		Me.OptParticularAccount.Cursor = System.Windows.Forms.Cursors.Default
		Me.OptParticularAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.OptParticularAccount.Appearance = System.Windows.Forms.Appearance.Normal
		Me.OptParticularAccount.TabStop = True
		Me.OptParticularAccount.Checked = False
		Me.OptParticularAccount.Visible = True
		Me.OptParticularAccount.Name = "OptParticularAccount"
		Me.OptAllAccount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.OptAllAccount.Text = "All"
		Me.OptAllAccount.Size = New System.Drawing.Size(41, 13)
		Me.OptAllAccount.Location = New System.Drawing.Point(118, 12)
		Me.OptAllAccount.TabIndex = 11
		Me.OptAllAccount.Checked = True
		Me.OptAllAccount.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.OptAllAccount.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.OptAllAccount.BackColor = System.Drawing.SystemColors.Control
		Me.OptAllAccount.CausesValidation = True
		Me.OptAllAccount.Enabled = True
		Me.OptAllAccount.ForeColor = System.Drawing.SystemColors.ControlText
		Me.OptAllAccount.Cursor = System.Windows.Forms.Cursors.Default
		Me.OptAllAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.OptAllAccount.Appearance = System.Windows.Forms.Appearance.Normal
		Me.OptAllAccount.TabStop = True
		Me.OptAllAccount.Visible = True
		Me.OptAllAccount.Name = "OptAllAccount"
		Me.txtName.AutoSize = False
		Me.txtName.BackColor = System.Drawing.Color.White
		Me.txtName.Enabled = False
		Me.txtName.ForeColor = System.Drawing.Color.Blue
		Me.txtName.Size = New System.Drawing.Size(229, 19)
		Me.txtName.Location = New System.Drawing.Point(4, 28)
		Me.txtName.TabIndex = 10
		Me.txtName.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtName.AcceptsReturn = True
		Me.txtName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtName.CausesValidation = True
		Me.txtName.HideSelection = True
		Me.txtName.ReadOnly = False
		Me.txtName.Maxlength = 0
		Me.txtName.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtName.MultiLine = False
		Me.txtName.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtName.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtName.TabStop = True
		Me.txtName.Visible = True
		Me.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtName.Name = "txtName"
		Me.cmdSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.cmdSearch.BackColor = System.Drawing.SystemColors.Menu
		Me.cmdSearch.Enabled = False
		Me.cmdSearch.Size = New System.Drawing.Size(23, 19)
		Me.cmdSearch.Location = New System.Drawing.Point(234, 28)
		Me.cmdSearch.Image = CType(resources.GetObject("cmdSearch.Image"), System.Drawing.Image)
		Me.cmdSearch.TabIndex = 9
		Me.cmdSearch.TabStop = False
		Me.ToolTip1.SetToolTip(Me.cmdSearch, "Search")
		Me.cmdSearch.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdSearch.CausesValidation = True
		Me.cmdSearch.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdSearch.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdSearch.Name = "cmdSearch"
		Me.FraFYear.Text = "Transfer Opening Balance"
		Me.FraFYear.ForeColor = System.Drawing.Color.FromARGB(128, 0, 0)
		Me.FraFYear.Size = New System.Drawing.Size(339, 61)
		Me.FraFYear.Location = New System.Drawing.Point(0, 0)
		Me.FraFYear.TabIndex = 3
		Me.FraFYear.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.FraFYear.BackColor = System.Drawing.SystemColors.Control
		Me.FraFYear.Enabled = True
		Me.FraFYear.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.FraFYear.Visible = True
		Me.FraFYear.Padding = New System.Windows.Forms.Padding(0)
		Me.FraFYear.Name = "FraFYear"
		Me.CboFYearTo.Size = New System.Drawing.Size(199, 21)
		Me.CboFYearTo.Location = New System.Drawing.Point(138, 34)
		Me.CboFYearTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.CboFYearTo.TabIndex = 5
		Me.CboFYearTo.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CboFYearTo.BackColor = System.Drawing.SystemColors.Window
		Me.CboFYearTo.CausesValidation = True
		Me.CboFYearTo.Enabled = True
		Me.CboFYearTo.ForeColor = System.Drawing.SystemColors.WindowText
		Me.CboFYearTo.IntegralHeight = True
		Me.CboFYearTo.Cursor = System.Windows.Forms.Cursors.Default
		Me.CboFYearTo.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CboFYearTo.Sorted = False
		Me.CboFYearTo.TabStop = True
		Me.CboFYearTo.Visible = True
		Me.CboFYearTo.Name = "CboFYearTo"
		Me.CboFYearFrom.Size = New System.Drawing.Size(199, 21)
		Me.CboFYearFrom.Location = New System.Drawing.Point(138, 10)
		Me.CboFYearFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.CboFYearFrom.TabIndex = 4
		Me.CboFYearFrom.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CboFYearFrom.BackColor = System.Drawing.SystemColors.Window
		Me.CboFYearFrom.CausesValidation = True
		Me.CboFYearFrom.Enabled = True
		Me.CboFYearFrom.ForeColor = System.Drawing.SystemColors.WindowText
		Me.CboFYearFrom.IntegralHeight = True
		Me.CboFYearFrom.Cursor = System.Windows.Forms.Cursors.Default
		Me.CboFYearFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CboFYearFrom.Sorted = False
		Me.CboFYearFrom.TabStop = True
		Me.CboFYearFrom.Visible = True
		Me.CboFYearFrom.Name = "CboFYearFrom"
		Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.Label2.Text = "FYear To :"
		Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label2.Size = New System.Drawing.Size(93, 17)
		Me.Label2.Location = New System.Drawing.Point(42, 42)
		Me.Label2.TabIndex = 7
		Me.Label2.BackColor = System.Drawing.SystemColors.Control
		Me.Label2.Enabled = True
		Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label2.UseMnemonic = True
		Me.Label2.Visible = True
		Me.Label2.AutoSize = False
		Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label2.Name = "Label2"
		Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.Label1.Text = "FYear From :"
		Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.Size = New System.Drawing.Size(93, 17)
		Me.Label1.Location = New System.Drawing.Point(42, 18)
		Me.Label1.TabIndex = 6
		Me.Label1.BackColor = System.Drawing.SystemColors.Control
		Me.Label1.Enabled = True
		Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label1.UseMnemonic = True
		Me.Label1.Visible = True
		Me.Label1.AutoSize = False
		Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label1.Name = "Label1"
		Me.FraButton.Size = New System.Drawing.Size(339, 43)
		Me.FraButton.Location = New System.Drawing.Point(0, 302)
		Me.FraButton.TabIndex = 0
		Me.FraButton.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.FraButton.BackColor = System.Drawing.SystemColors.Control
		Me.FraButton.Enabled = True
		Me.FraButton.ForeColor = System.Drawing.SystemColors.ControlText
		Me.FraButton.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.FraButton.Visible = True
		Me.FraButton.Padding = New System.Windows.Forms.Padding(0)
		Me.FraButton.Name = "FraButton"
		Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.cmdClose.Text = "Close"
		Me.cmdClose.Size = New System.Drawing.Size(87, 23)
		Me.cmdClose.Location = New System.Drawing.Point(208, 14)
		Me.cmdClose.TabIndex = 2
		Me.cmdClose.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
		Me.cmdClose.CausesValidation = True
		Me.cmdClose.Enabled = True
		Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdClose.TabStop = True
		Me.cmdClose.Name = "cmdClose"
		Me.cmdStart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.cmdStart.Text = "Start"
		Me.cmdStart.Size = New System.Drawing.Size(87, 23)
		Me.cmdStart.Location = New System.Drawing.Point(52, 14)
		Me.cmdStart.TabIndex = 1
		Me.cmdStart.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdStart.BackColor = System.Drawing.SystemColors.Control
		Me.cmdStart.CausesValidation = True
		Me.cmdStart.Enabled = True
		Me.cmdStart.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdStart.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdStart.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdStart.TabStop = True
		Me.cmdStart.Name = "cmdStart"
		Me.TxtDisplayTransfer.SetIndex(_TxtDisplayTransfer_1, CType(1, Short))
		Me.TxtDisplayTransfer.SetIndex(_TxtDisplayTransfer_0, CType(0, Short))
		CType(Me.TxtDisplayTransfer, System.ComponentModel.ISupportInitialize).EndInit()
		Me.Controls.Add(SLM)
		Me.Controls.Add(_TxtDisplayTransfer_1)
		Me.Controls.Add(_TxtDisplayTransfer_0)
		Me.Controls.Add(FraAccount)
		Me.Controls.Add(FraFYear)
		Me.Controls.Add(FraButton)
		Me.SLM.Controls.Add(cmdsearchDepr)
		Me.SLM.Controls.Add(txtDeprMode)
		Me.FraAccount.Controls.Add(OptParticularAccount)
		Me.FraAccount.Controls.Add(OptAllAccount)
		Me.FraAccount.Controls.Add(txtName)
		Me.FraAccount.Controls.Add(cmdSearch)
		Me.FraFYear.Controls.Add(CboFYearTo)
		Me.FraFYear.Controls.Add(CboFYearFrom)
		Me.FraFYear.Controls.Add(Label2)
		Me.FraFYear.Controls.Add(Label1)
		Me.FraButton.Controls.Add(cmdClose)
		Me.FraButton.Controls.Add(cmdStart)
		Me.SLM.ResumeLayout(False)
		Me.FraAccount.ResumeLayout(False)
		Me.FraFYear.ResumeLayout(False)
		Me.FraButton.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()
	End Sub
#End Region 
End Class