<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmCIT
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
	Public WithEvents txtPin As System.Windows.Forms.TextBox
	Public WithEvents txtCity As System.Windows.Forms.TextBox
	Public WithEvents txtCircle As System.Windows.Forms.TextBox
	Public WithEvents txtAddress As System.Windows.Forms.TextBox
	Public WithEvents Label1 As System.Windows.Forms.Label
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
		Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmCIT))
		Me.components = New System.ComponentModel.Container()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
		Me.Frame13 = New System.Windows.Forms.GroupBox
		Me.txtPin = New System.Windows.Forms.TextBox
		Me.txtCity = New System.Windows.Forms.TextBox
		Me.txtCircle = New System.Windows.Forms.TextBox
		Me.txtAddress = New System.Windows.Forms.TextBox
		Me.Label1 = New System.Windows.Forms.Label
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
		Me.Text = "CIT (TDS) Master"
		Me.ClientSize = New System.Drawing.Size(560, 124)
		Me.Location = New System.Drawing.Point(8, 29)
		Me.Icon = CType(resources.GetObject("frmCIT.Icon"), System.Drawing.Icon)
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
		Me.Name = "frmCIT"
		Me.Frame13.Size = New System.Drawing.Size(559, 87)
		Me.Frame13.Location = New System.Drawing.Point(0, -6)
		Me.Frame13.TabIndex = 6
		Me.Frame13.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame13.BackColor = System.Drawing.SystemColors.Control
		Me.Frame13.Enabled = True
		Me.Frame13.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame13.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame13.Visible = True
		Me.Frame13.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame13.Name = "Frame13"
		Me.txtPin.AutoSize = False
		Me.txtPin.Size = New System.Drawing.Size(153, 19)
		Me.txtPin.Location = New System.Drawing.Point(396, 60)
		Me.txtPin.TabIndex = 4
		Me.txtPin.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtPin.AcceptsReturn = True
		Me.txtPin.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtPin.BackColor = System.Drawing.SystemColors.Window
		Me.txtPin.CausesValidation = True
		Me.txtPin.Enabled = True
		Me.txtPin.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtPin.HideSelection = True
		Me.txtPin.ReadOnly = False
		Me.txtPin.Maxlength = 0
		Me.txtPin.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtPin.MultiLine = False
		Me.txtPin.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtPin.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtPin.TabStop = True
		Me.txtPin.Visible = True
		Me.txtPin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtPin.Name = "txtPin"
		Me.txtCity.AutoSize = False
		Me.txtCity.Size = New System.Drawing.Size(185, 19)
		Me.txtCity.Location = New System.Drawing.Point(164, 60)
		Me.txtCity.TabIndex = 3
		Me.txtCity.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtCity.AcceptsReturn = True
		Me.txtCity.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtCity.BackColor = System.Drawing.SystemColors.Window
		Me.txtCity.CausesValidation = True
		Me.txtCity.Enabled = True
		Me.txtCity.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtCity.HideSelection = True
		Me.txtCity.ReadOnly = False
		Me.txtCity.Maxlength = 0
		Me.txtCity.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtCity.MultiLine = False
		Me.txtCity.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtCity.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtCity.TabStop = True
		Me.txtCity.Visible = True
		Me.txtCity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtCity.Name = "txtCity"
		Me.txtCircle.AutoSize = False
		Me.txtCircle.Size = New System.Drawing.Size(385, 19)
		Me.txtCircle.Location = New System.Drawing.Point(164, 16)
		Me.txtCircle.TabIndex = 1
		Me.txtCircle.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtCircle.AcceptsReturn = True
		Me.txtCircle.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtCircle.BackColor = System.Drawing.SystemColors.Window
		Me.txtCircle.CausesValidation = True
		Me.txtCircle.Enabled = True
		Me.txtCircle.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtCircle.HideSelection = True
		Me.txtCircle.ReadOnly = False
		Me.txtCircle.Maxlength = 0
		Me.txtCircle.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtCircle.MultiLine = False
		Me.txtCircle.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtCircle.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtCircle.TabStop = True
		Me.txtCircle.Visible = True
		Me.txtCircle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtCircle.Name = "txtCircle"
		Me.txtAddress.AutoSize = False
		Me.txtAddress.Size = New System.Drawing.Size(385, 19)
		Me.txtAddress.Location = New System.Drawing.Point(164, 38)
		Me.txtAddress.TabIndex = 2
		Me.txtAddress.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtAddress.AcceptsReturn = True
		Me.txtAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtAddress.BackColor = System.Drawing.SystemColors.Window
		Me.txtAddress.CausesValidation = True
		Me.txtAddress.Enabled = True
		Me.txtAddress.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtAddress.HideSelection = True
		Me.txtAddress.ReadOnly = False
		Me.txtAddress.Maxlength = 0
		Me.txtAddress.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtAddress.MultiLine = False
		Me.txtAddress.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtAddress.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtAddress.TabStop = True
		Me.txtAddress.Visible = True
		Me.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtAddress.Name = "txtAddress"
		Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.Label1.Text = "Pin :"
		Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.Size = New System.Drawing.Size(27, 13)
		Me.Label1.Location = New System.Drawing.Point(367, 64)
		Me.Label1.TabIndex = 12
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
		Me.Label55.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.Label55.Text = "City :"
		Me.Label55.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label55.Size = New System.Drawing.Size(30, 13)
		Me.Label55.Location = New System.Drawing.Point(132, 64)
		Me.Label55.TabIndex = 11
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
		Me.Label54.Text = "Circle :"
		Me.Label54.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label54.Size = New System.Drawing.Size(41, 13)
		Me.Label54.Location = New System.Drawing.Point(121, 20)
		Me.Label54.TabIndex = 10
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
		Me.Label33.Text = "Address :"
		Me.Label33.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label33.Size = New System.Drawing.Size(54, 13)
		Me.Label33.Location = New System.Drawing.Point(108, 42)
		Me.Label33.TabIndex = 9
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
		Me.Frame8.TabIndex = 7
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
		Me.cmdcancel.TabIndex = 5
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
		Me.cmdSavePrint.TabIndex = 8
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
		Me.Frame13.Controls.Add(txtPin)
		Me.Frame13.Controls.Add(txtCity)
		Me.Frame13.Controls.Add(txtCircle)
		Me.Frame13.Controls.Add(txtAddress)
		Me.Frame13.Controls.Add(Label1)
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