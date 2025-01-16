<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmeAuthorised
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
	Public WithEvents txtDesignation As System.Windows.Forms.TextBox
	Public WithEvents txtAuthorized As System.Windows.Forms.TextBox
	Public WithEvents txtAuthorizedFName As System.Windows.Forms.TextBox
	Public WithEvents Label55 As System.Windows.Forms.Label
	Public WithEvents Label54 As System.Windows.Forms.Label
	Public WithEvents Label33 As System.Windows.Forms.Label
	Public WithEvents Frame13 As System.Windows.Forms.GroupBox
	Public WithEvents cmdcancel As System.Windows.Forms.Button
	Public WithEvents cmdSave As System.Windows.Forms.Button
	Public WithEvents cmdSavePrint As System.Windows.Forms.Button
	Public WithEvents Frame8 As System.Windows.Forms.GroupBox
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmeAuthorised))
		Me.components = New System.ComponentModel.Container()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
		Me.Frame13 = New System.Windows.Forms.GroupBox
		Me.txtDesignation = New System.Windows.Forms.TextBox
		Me.txtAuthorized = New System.Windows.Forms.TextBox
		Me.txtAuthorizedFName = New System.Windows.Forms.TextBox
		Me.Label55 = New System.Windows.Forms.Label
		Me.Label54 = New System.Windows.Forms.Label
		Me.Label33 = New System.Windows.Forms.Label
		Me.Frame8 = New System.Windows.Forms.GroupBox
		Me.cmdcancel = New System.Windows.Forms.Button
		Me.cmdSave = New System.Windows.Forms.Button
		Me.cmdSavePrint = New System.Windows.Forms.Button
		Me.Frame13.SuspendLayout()
		Me.Frame8.SuspendLayout()
		Me.SuspendLayout()
		Me.ToolTip1.Active = True
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
		Me.Text = "e-Authorisation Master"
		Me.ClientSize = New System.Drawing.Size(560, 124)
		Me.Location = New System.Drawing.Point(8, 29)
		Me.Icon = CType(resources.GetObject("frmeAuthorised.Icon"), System.Drawing.Icon)
		Me.MaximizeBox = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds
		Me.MinimizeBox = False
		Me.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.SystemColors.Control
		Me.ControlBox = True
		Me.Enabled = True
		Me.KeyPreview = False
		Me.Cursor = System.Windows.Forms.Cursors.Default
		Me.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.ShowInTaskbar = True
		Me.HelpButton = False
		Me.WindowState = System.Windows.Forms.FormWindowState.Normal
		Me.Name = "frmeAuthorised"
		Me.Frame13.Size = New System.Drawing.Size(559, 87)
		Me.Frame13.Location = New System.Drawing.Point(0, -6)
		Me.Frame13.TabIndex = 5
		Me.Frame13.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame13.BackColor = System.Drawing.SystemColors.Control
		Me.Frame13.Enabled = True
		Me.Frame13.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame13.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame13.Visible = True
		Me.Frame13.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame13.Name = "Frame13"
		Me.txtDesignation.AutoSize = False
		Me.txtDesignation.Size = New System.Drawing.Size(385, 19)
		Me.txtDesignation.Location = New System.Drawing.Point(164, 60)
		Me.txtDesignation.TabIndex = 3
		Me.txtDesignation.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtDesignation.AcceptsReturn = True
		Me.txtDesignation.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtDesignation.BackColor = System.Drawing.SystemColors.Window
		Me.txtDesignation.CausesValidation = True
		Me.txtDesignation.Enabled = True
		Me.txtDesignation.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtDesignation.HideSelection = True
		Me.txtDesignation.ReadOnly = False
		Me.txtDesignation.Maxlength = 0
		Me.txtDesignation.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtDesignation.MultiLine = False
		Me.txtDesignation.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtDesignation.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtDesignation.TabStop = True
		Me.txtDesignation.Visible = True
		Me.txtDesignation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtDesignation.Name = "txtDesignation"
		Me.txtAuthorized.AutoSize = False
		Me.txtAuthorized.Size = New System.Drawing.Size(385, 19)
		Me.txtAuthorized.Location = New System.Drawing.Point(164, 16)
		Me.txtAuthorized.TabIndex = 1
		Me.txtAuthorized.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtAuthorized.AcceptsReturn = True
		Me.txtAuthorized.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtAuthorized.BackColor = System.Drawing.SystemColors.Window
		Me.txtAuthorized.CausesValidation = True
		Me.txtAuthorized.Enabled = True
		Me.txtAuthorized.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtAuthorized.HideSelection = True
		Me.txtAuthorized.ReadOnly = False
		Me.txtAuthorized.Maxlength = 0
		Me.txtAuthorized.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtAuthorized.MultiLine = False
		Me.txtAuthorized.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtAuthorized.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtAuthorized.TabStop = True
		Me.txtAuthorized.Visible = True
		Me.txtAuthorized.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtAuthorized.Name = "txtAuthorized"
		Me.txtAuthorizedFName.AutoSize = False
		Me.txtAuthorizedFName.Size = New System.Drawing.Size(385, 19)
		Me.txtAuthorizedFName.Location = New System.Drawing.Point(164, 38)
		Me.txtAuthorizedFName.TabIndex = 2
		Me.txtAuthorizedFName.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtAuthorizedFName.AcceptsReturn = True
		Me.txtAuthorizedFName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtAuthorizedFName.BackColor = System.Drawing.SystemColors.Window
		Me.txtAuthorizedFName.CausesValidation = True
		Me.txtAuthorizedFName.Enabled = True
		Me.txtAuthorizedFName.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtAuthorizedFName.HideSelection = True
		Me.txtAuthorizedFName.ReadOnly = False
		Me.txtAuthorizedFName.Maxlength = 0
		Me.txtAuthorizedFName.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtAuthorizedFName.MultiLine = False
		Me.txtAuthorizedFName.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtAuthorizedFName.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtAuthorizedFName.TabStop = True
		Me.txtAuthorizedFName.Visible = True
		Me.txtAuthorizedFName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtAuthorizedFName.Name = "txtAuthorizedFName"
		Me.Label55.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.Label55.Text = "Designation :"
		Me.Label55.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label55.Size = New System.Drawing.Size(130, 13)
		Me.Label55.Location = New System.Drawing.Point(32, 64)
		Me.Label55.TabIndex = 10
		Me.Label55.BackColor = System.Drawing.SystemColors.Control
		Me.Label55.Enabled = True
		Me.Label55.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label55.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label55.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label55.UseMnemonic = True
		Me.Label55.Visible = True
		Me.Label55.AutoSize = True
		Me.Label55.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label55.Name = "Label55"
		Me.Label54.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.Label54.Text = "Authorized :"
		Me.Label54.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label54.Size = New System.Drawing.Size(130, 13)
		Me.Label54.Location = New System.Drawing.Point(32, 20)
		Me.Label54.TabIndex = 9
		Me.Label54.BackColor = System.Drawing.SystemColors.Control
		Me.Label54.Enabled = True
		Me.Label54.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label54.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label54.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label54.UseMnemonic = True
		Me.Label54.Visible = True
		Me.Label54.AutoSize = True
		Me.Label54.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label54.Name = "Label54"
		Me.Label33.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.Label33.Text = "Authorized's Father Name :"
		Me.Label33.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label33.Size = New System.Drawing.Size(154, 13)
		Me.Label33.Location = New System.Drawing.Point(8, 42)
		Me.Label33.TabIndex = 8
		Me.Label33.BackColor = System.Drawing.SystemColors.Control
		Me.Label33.Enabled = True
		Me.Label33.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label33.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label33.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label33.UseMnemonic = True
		Me.Label33.Visible = True
		Me.Label33.AutoSize = True
		Me.Label33.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label33.Name = "Label33"
		Me.Frame8.Size = New System.Drawing.Size(563, 47)
		Me.Frame8.Location = New System.Drawing.Point(-4, 76)
		Me.Frame8.TabIndex = 6
		Me.Frame8.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame8.BackColor = System.Drawing.SystemColors.Control
		Me.Frame8.Enabled = True
		Me.Frame8.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame8.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame8.Visible = True
		Me.Frame8.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame8.Name = "Frame8"
		Me.cmdcancel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.cmdcancel.Text = "&Close"
		Me.cmdcancel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdcancel.Size = New System.Drawing.Size(69, 34)
		Me.cmdcancel.Location = New System.Drawing.Point(484, 10)
		Me.cmdcancel.TabIndex = 4
		Me.ToolTip1.SetToolTip(Me.cmdcancel, "Cancel & Close Setup")
		Me.cmdcancel.BackColor = System.Drawing.SystemColors.Control
		Me.cmdcancel.CausesValidation = True
		Me.cmdcancel.Enabled = True
		Me.cmdcancel.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdcancel.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdcancel.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdcancel.TabStop = True
		Me.cmdcancel.Name = "cmdcancel"
		Me.cmdSave.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.cmdSave.Text = "&Save"
		Me.AcceptButton = Me.cmdSave
		Me.cmdSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdSave.Size = New System.Drawing.Size(69, 34)
		Me.cmdSave.Location = New System.Drawing.Point(4, 10)
		Me.cmdSave.TabIndex = 0
		Me.cmdSave.BackColor = System.Drawing.SystemColors.Control
		Me.cmdSave.CausesValidation = True
		Me.cmdSave.Enabled = True
		Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdSave.TabStop = True
		Me.cmdSave.Name = "cmdSave"
		Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.cmdSavePrint.Text = "Save&&Print"
		Me.cmdSavePrint.Size = New System.Drawing.Size(38, 25)
		Me.cmdSavePrint.Location = New System.Drawing.Point(4, 10)
		Me.cmdSavePrint.TabIndex = 7
		Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save and Print the Current Bill")
		Me.cmdSavePrint.Visible = False
		Me.cmdSavePrint.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
		Me.cmdSavePrint.CausesValidation = True
		Me.cmdSavePrint.Enabled = True
		Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdSavePrint.TabStop = True
		Me.cmdSavePrint.Name = "cmdSavePrint"
		Me.Controls.Add(Frame13)
		Me.Controls.Add(Frame8)
		Me.Frame13.Controls.Add(txtDesignation)
		Me.Frame13.Controls.Add(txtAuthorized)
		Me.Frame13.Controls.Add(txtAuthorizedFName)
		Me.Frame13.Controls.Add(Label55)
		Me.Frame13.Controls.Add(Label54)
		Me.Frame13.Controls.Add(Label33)
		Me.Frame8.Controls.Add(cmdcancel)
		Me.Frame8.Controls.Add(cmdSave)
		Me.Frame8.Controls.Add(cmdSavePrint)
		Me.Frame13.ResumeLayout(False)
		Me.Frame8.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()
	End Sub
#End Region 
End Class