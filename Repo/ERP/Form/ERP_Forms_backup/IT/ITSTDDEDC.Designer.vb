<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmITSTDDEDC
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
		Me.MDIParent = Payroll.Master
		Payroll.Master.Show
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
	Public WithEvents Frame1 As System.Windows.Forms.GroupBox
	Public WithEvents txtAYear As System.Windows.Forms.TextBox
	Public WithEvents Label13 As System.Windows.Forms.Label
	Public WithEvents fraTop As System.Windows.Forms.GroupBox
	Public WithEvents sprdITRate As AxFPSpreadADO.AxfpSpread
	Public WithEvents Frame3 As System.Windows.Forms.GroupBox
	Public WithEvents fraBrowse As System.Windows.Forms.GroupBox
	Public WithEvents CmdClose As System.Windows.Forms.Button
	Public WithEvents CmdSave As System.Windows.Forms.Button
	Public WithEvents CmdDelete As System.Windows.Forms.Button
	Public WithEvents FraMovement As System.Windows.Forms.GroupBox
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmITSTDDEDC))
		Me.components = New System.ComponentModel.Container()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
		Me.fraBrowse = New System.Windows.Forms.GroupBox
		Me.Frame1 = New System.Windows.Forms.GroupBox
		Me.fraTop = New System.Windows.Forms.GroupBox
		Me.txtAYear = New System.Windows.Forms.TextBox
		Me.Label13 = New System.Windows.Forms.Label
		Me.Frame3 = New System.Windows.Forms.GroupBox
		Me.sprdITRate = New AxFPSpreadADO.AxfpSpread
		Me.FraMovement = New System.Windows.Forms.GroupBox
		Me.CmdClose = New System.Windows.Forms.Button
		Me.CmdSave = New System.Windows.Forms.Button
		Me.CmdDelete = New System.Windows.Forms.Button
		Me.fraBrowse.SuspendLayout()
		Me.fraTop.SuspendLayout()
		Me.Frame3.SuspendLayout()
		Me.FraMovement.SuspendLayout()
		Me.SuspendLayout()
		Me.ToolTip1.Active = True
		CType(Me.sprdITRate, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.Text = "Standard Deduction"
		Me.ClientSize = New System.Drawing.Size(413, 286)
		Me.Location = New System.Drawing.Point(4, 23)
		Me.Icon = CType(resources.GetObject("frmITSTDDEDC.Icon"), System.Drawing.Icon)
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
		Me.Name = "frmITSTDDEDC"
		Me.fraBrowse.Size = New System.Drawing.Size(413, 235)
		Me.fraBrowse.Location = New System.Drawing.Point(0, 0)
		Me.fraBrowse.TabIndex = 4
		Me.fraBrowse.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.fraBrowse.BackColor = System.Drawing.SystemColors.Control
		Me.fraBrowse.Enabled = True
		Me.fraBrowse.ForeColor = System.Drawing.SystemColors.ControlText
		Me.fraBrowse.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.fraBrowse.Visible = True
		Me.fraBrowse.Padding = New System.Windows.Forms.Padding(0)
		Me.fraBrowse.Name = "fraBrowse"
		Me.Frame1.Size = New System.Drawing.Size(303, 49)
		Me.Frame1.Location = New System.Drawing.Point(110, 0)
		Me.Frame1.TabIndex = 10
		Me.Frame1.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame1.BackColor = System.Drawing.SystemColors.Control
		Me.Frame1.Enabled = True
		Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame1.Visible = True
		Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame1.Name = "Frame1"
		Me.fraTop.Text = "Assessment Year"
		Me.fraTop.Size = New System.Drawing.Size(109, 49)
		Me.fraTop.Location = New System.Drawing.Point(0, 0)
		Me.fraTop.TabIndex = 7
		Me.fraTop.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.fraTop.BackColor = System.Drawing.SystemColors.Control
		Me.fraTop.Enabled = True
		Me.fraTop.ForeColor = System.Drawing.SystemColors.ControlText
		Me.fraTop.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.fraTop.Visible = True
		Me.fraTop.Padding = New System.Windows.Forms.Padding(0)
		Me.fraTop.Name = "fraTop"
		Me.txtAYear.AutoSize = False
		Me.txtAYear.Enabled = False
		Me.txtAYear.Size = New System.Drawing.Size(83, 19)
		Me.txtAYear.Location = New System.Drawing.Point(8, 18)
		Me.txtAYear.TabIndex = 8
		Me.txtAYear.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtAYear.AcceptsReturn = True
		Me.txtAYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtAYear.BackColor = System.Drawing.SystemColors.Window
		Me.txtAYear.CausesValidation = True
		Me.txtAYear.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtAYear.HideSelection = True
		Me.txtAYear.ReadOnly = False
		Me.txtAYear.Maxlength = 0
		Me.txtAYear.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtAYear.MultiLine = False
		Me.txtAYear.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtAYear.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtAYear.TabStop = True
		Me.txtAYear.Visible = True
		Me.txtAYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtAYear.Name = "txtAYear"
		Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopCenter
		Me.Label13.Size = New System.Drawing.Size(5, 13)
		Me.Label13.Location = New System.Drawing.Point(557, 92)
		Me.Label13.TabIndex = 9
		Me.Label13.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
		Me.Frame3.Size = New System.Drawing.Size(411, 185)
		Me.Frame3.Location = New System.Drawing.Point(0, 50)
		Me.Frame3.TabIndex = 5
		Me.Frame3.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame3.BackColor = System.Drawing.SystemColors.Control
		Me.Frame3.Enabled = True
		Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame3.Visible = True
		Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame3.Name = "Frame3"
		sprdITRate.OcxState = CType(resources.GetObject("sprdITRate.OcxState"), System.Windows.Forms.AxHost.State)
		Me.sprdITRate.Size = New System.Drawing.Size(407, 175)
		Me.sprdITRate.Location = New System.Drawing.Point(2, 8)
		Me.sprdITRate.TabIndex = 6
		Me.sprdITRate.Name = "sprdITRate"
		Me.FraMovement.Size = New System.Drawing.Size(413, 51)
		Me.FraMovement.Location = New System.Drawing.Point(0, 234)
		Me.FraMovement.TabIndex = 3
		Me.FraMovement.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
		Me.FraMovement.Enabled = True
		Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
		Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.FraMovement.Visible = True
		Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
		Me.FraMovement.Name = "FraMovement"
		Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.CmdClose.Text = "&Close"
		Me.CmdClose.Size = New System.Drawing.Size(67, 34)
		Me.CmdClose.Location = New System.Drawing.Point(342, 12)
		Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
		Me.CmdClose.TabIndex = 2
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
		Me.CmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.CmdSave.Text = "&Save"
		Me.CmdSave.Size = New System.Drawing.Size(67, 34)
		Me.CmdSave.Location = New System.Drawing.Point(2, 12)
		Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
		Me.CmdSave.TabIndex = 0
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
		Me.CmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.CmdDelete.Text = "&Delete"
		Me.CmdDelete.Size = New System.Drawing.Size(67, 34)
		Me.CmdDelete.Location = New System.Drawing.Point(70, 12)
		Me.CmdDelete.Image = CType(resources.GetObject("CmdDelete.Image"), System.Drawing.Image)
		Me.CmdDelete.TabIndex = 1
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
		CType(Me.sprdITRate, System.ComponentModel.ISupportInitialize).EndInit()
		Me.Controls.Add(fraBrowse)
		Me.Controls.Add(FraMovement)
		Me.fraBrowse.Controls.Add(Frame1)
		Me.fraBrowse.Controls.Add(fraTop)
		Me.fraBrowse.Controls.Add(Frame3)
		Me.fraTop.Controls.Add(txtAYear)
		Me.fraTop.Controls.Add(Label13)
		Me.Frame3.Controls.Add(sprdITRate)
		Me.FraMovement.Controls.Add(CmdClose)
		Me.FraMovement.Controls.Add(CmdSave)
		Me.FraMovement.Controls.Add(CmdDelete)
		Me.fraBrowse.ResumeLayout(False)
		Me.fraTop.ResumeLayout(False)
		Me.Frame3.ResumeLayout(False)
		Me.FraMovement.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()
	End Sub
#End Region 
End Class