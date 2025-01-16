<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmItemCodeMerging
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
	Public WithEvents cmdSearchItemTo As System.Windows.Forms.Button
	Public WithEvents txtItemNameTo As System.Windows.Forms.TextBox
	Public WithEvents txtItemCodeTo As System.Windows.Forms.TextBox
	Public WithEvents Label4 As System.Windows.Forms.Label
	Public WithEvents Label3 As System.Windows.Forms.Label
	Public WithEvents Frame2 As System.Windows.Forms.GroupBox
	Public WithEvents txtItemCodeFrom As System.Windows.Forms.TextBox
	Public WithEvents txtItemNameFrom As System.Windows.Forms.TextBox
	Public WithEvents cmdSearchItemFrom As System.Windows.Forms.Button
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents Frame1 As System.Windows.Forms.GroupBox
	Public WithEvents cmdSave As System.Windows.Forms.Button
	Public WithEvents cmdClose As System.Windows.Forms.Button
	Public WithEvents lblStatus As System.Windows.Forms.Label
	Public WithEvents Frame3 As System.Windows.Forms.GroupBox
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(FrmItemCodeMerging))
		Me.components = New System.ComponentModel.Container()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
		Me.Frame2 = New System.Windows.Forms.GroupBox
		Me.cmdSearchItemTo = New System.Windows.Forms.Button
		Me.txtItemNameTo = New System.Windows.Forms.TextBox
		Me.txtItemCodeTo = New System.Windows.Forms.TextBox
		Me.Label4 = New System.Windows.Forms.Label
		Me.Label3 = New System.Windows.Forms.Label
		Me.Frame1 = New System.Windows.Forms.GroupBox
		Me.txtItemCodeFrom = New System.Windows.Forms.TextBox
		Me.txtItemNameFrom = New System.Windows.Forms.TextBox
		Me.cmdSearchItemFrom = New System.Windows.Forms.Button
		Me.Label2 = New System.Windows.Forms.Label
		Me.Label1 = New System.Windows.Forms.Label
		Me.Frame3 = New System.Windows.Forms.GroupBox
		Me.cmdSave = New System.Windows.Forms.Button
		Me.cmdClose = New System.Windows.Forms.Button
		Me.lblStatus = New System.Windows.Forms.Label
		Me.Frame2.SuspendLayout()
		Me.Frame1.SuspendLayout()
		Me.Frame3.SuspendLayout()
		Me.SuspendLayout()
		Me.ToolTip1.Active = True
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
		Me.Text = "Item Merging"
		Me.ClientSize = New System.Drawing.Size(530, 202)
		Me.Location = New System.Drawing.Point(3, 22)
		Me.Icon = CType(resources.GetObject("FrmItemCodeMerging.Icon"), System.Drawing.Icon)
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
		Me.Name = "FrmItemCodeMerging"
		Me.Frame2.Text = "Item To"
		Me.Frame2.Size = New System.Drawing.Size(529, 77)
		Me.Frame2.Location = New System.Drawing.Point(0, 78)
		Me.Frame2.TabIndex = 9
		Me.Frame2.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame2.BackColor = System.Drawing.SystemColors.Control
		Me.Frame2.Enabled = True
		Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame2.Visible = True
		Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame2.Name = "Frame2"
		Me.cmdSearchItemTo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.cmdSearchItemTo.BackColor = System.Drawing.SystemColors.Menu
		Me.cmdSearchItemTo.Size = New System.Drawing.Size(23, 19)
		Me.cmdSearchItemTo.Location = New System.Drawing.Point(360, 32)
		Me.cmdSearchItemTo.Image = CType(resources.GetObject("cmdSearchItemTo.Image"), System.Drawing.Image)
		Me.cmdSearchItemTo.TabIndex = 12
		Me.cmdSearchItemTo.TabStop = False
		Me.ToolTip1.SetToolTip(Me.cmdSearchItemTo, "Search")
		Me.cmdSearchItemTo.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdSearchItemTo.CausesValidation = True
		Me.cmdSearchItemTo.Enabled = True
		Me.cmdSearchItemTo.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdSearchItemTo.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdSearchItemTo.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdSearchItemTo.Name = "cmdSearchItemTo"
		Me.txtItemNameTo.AutoSize = False
		Me.txtItemNameTo.BackColor = System.Drawing.Color.White
		Me.txtItemNameTo.ForeColor = System.Drawing.Color.Blue
		Me.txtItemNameTo.Size = New System.Drawing.Size(307, 19)
		Me.txtItemNameTo.Location = New System.Drawing.Point(52, 32)
		Me.txtItemNameTo.TabIndex = 11
		Me.txtItemNameTo.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtItemNameTo.AcceptsReturn = True
		Me.txtItemNameTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtItemNameTo.CausesValidation = True
		Me.txtItemNameTo.Enabled = True
		Me.txtItemNameTo.HideSelection = True
		Me.txtItemNameTo.ReadOnly = False
		Me.txtItemNameTo.Maxlength = 0
		Me.txtItemNameTo.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtItemNameTo.MultiLine = False
		Me.txtItemNameTo.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtItemNameTo.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtItemNameTo.TabStop = True
		Me.txtItemNameTo.Visible = True
		Me.txtItemNameTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtItemNameTo.Name = "txtItemNameTo"
		Me.txtItemCodeTo.AutoSize = False
		Me.txtItemCodeTo.BackColor = System.Drawing.Color.White
		Me.txtItemCodeTo.ForeColor = System.Drawing.Color.Blue
		Me.txtItemCodeTo.Size = New System.Drawing.Size(85, 19)
		Me.txtItemCodeTo.Location = New System.Drawing.Point(440, 32)
		Me.txtItemCodeTo.TabIndex = 10
		Me.txtItemCodeTo.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtItemCodeTo.AcceptsReturn = True
		Me.txtItemCodeTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtItemCodeTo.CausesValidation = True
		Me.txtItemCodeTo.Enabled = True
		Me.txtItemCodeTo.HideSelection = True
		Me.txtItemCodeTo.ReadOnly = False
		Me.txtItemCodeTo.Maxlength = 0
		Me.txtItemCodeTo.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtItemCodeTo.MultiLine = False
		Me.txtItemCodeTo.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtItemCodeTo.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtItemCodeTo.TabStop = True
		Me.txtItemCodeTo.Visible = True
		Me.txtItemCodeTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtItemCodeTo.Name = "txtItemCodeTo"
		Me.Label4.Text = "Name :"
		Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label4.Size = New System.Drawing.Size(41, 13)
		Me.Label4.Location = New System.Drawing.Point(8, 34)
		Me.Label4.TabIndex = 14
		Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopLeft
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
		Me.Label3.Text = "Code :"
		Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label3.Size = New System.Drawing.Size(38, 13)
		Me.Label3.Location = New System.Drawing.Point(394, 34)
		Me.Label3.TabIndex = 13
		Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopLeft
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
		Me.Frame1.Text = "Item From"
		Me.Frame1.Size = New System.Drawing.Size(529, 77)
		Me.Frame1.Location = New System.Drawing.Point(0, 0)
		Me.Frame1.TabIndex = 3
		Me.Frame1.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame1.BackColor = System.Drawing.SystemColors.Control
		Me.Frame1.Enabled = True
		Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame1.Visible = True
		Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame1.Name = "Frame1"
		Me.txtItemCodeFrom.AutoSize = False
		Me.txtItemCodeFrom.BackColor = System.Drawing.Color.White
		Me.txtItemCodeFrom.ForeColor = System.Drawing.Color.Blue
		Me.txtItemCodeFrom.Size = New System.Drawing.Size(85, 19)
		Me.txtItemCodeFrom.Location = New System.Drawing.Point(440, 32)
		Me.txtItemCodeFrom.TabIndex = 6
		Me.txtItemCodeFrom.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtItemCodeFrom.AcceptsReturn = True
		Me.txtItemCodeFrom.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtItemCodeFrom.CausesValidation = True
		Me.txtItemCodeFrom.Enabled = True
		Me.txtItemCodeFrom.HideSelection = True
		Me.txtItemCodeFrom.ReadOnly = False
		Me.txtItemCodeFrom.Maxlength = 0
		Me.txtItemCodeFrom.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtItemCodeFrom.MultiLine = False
		Me.txtItemCodeFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtItemCodeFrom.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtItemCodeFrom.TabStop = True
		Me.txtItemCodeFrom.Visible = True
		Me.txtItemCodeFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtItemCodeFrom.Name = "txtItemCodeFrom"
		Me.txtItemNameFrom.AutoSize = False
		Me.txtItemNameFrom.BackColor = System.Drawing.Color.White
		Me.txtItemNameFrom.ForeColor = System.Drawing.Color.Blue
		Me.txtItemNameFrom.Size = New System.Drawing.Size(307, 19)
		Me.txtItemNameFrom.Location = New System.Drawing.Point(52, 32)
		Me.txtItemNameFrom.TabIndex = 5
		Me.txtItemNameFrom.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtItemNameFrom.AcceptsReturn = True
		Me.txtItemNameFrom.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtItemNameFrom.CausesValidation = True
		Me.txtItemNameFrom.Enabled = True
		Me.txtItemNameFrom.HideSelection = True
		Me.txtItemNameFrom.ReadOnly = False
		Me.txtItemNameFrom.Maxlength = 0
		Me.txtItemNameFrom.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtItemNameFrom.MultiLine = False
		Me.txtItemNameFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtItemNameFrom.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtItemNameFrom.TabStop = True
		Me.txtItemNameFrom.Visible = True
		Me.txtItemNameFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtItemNameFrom.Name = "txtItemNameFrom"
		Me.cmdSearchItemFrom.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.cmdSearchItemFrom.BackColor = System.Drawing.SystemColors.Menu
		Me.cmdSearchItemFrom.Size = New System.Drawing.Size(23, 19)
		Me.cmdSearchItemFrom.Location = New System.Drawing.Point(360, 32)
		Me.cmdSearchItemFrom.Image = CType(resources.GetObject("cmdSearchItemFrom.Image"), System.Drawing.Image)
		Me.cmdSearchItemFrom.TabIndex = 4
		Me.cmdSearchItemFrom.TabStop = False
		Me.ToolTip1.SetToolTip(Me.cmdSearchItemFrom, "Search")
		Me.cmdSearchItemFrom.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdSearchItemFrom.CausesValidation = True
		Me.cmdSearchItemFrom.Enabled = True
		Me.cmdSearchItemFrom.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdSearchItemFrom.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdSearchItemFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdSearchItemFrom.Name = "cmdSearchItemFrom"
		Me.Label2.Text = "Code :"
		Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label2.Size = New System.Drawing.Size(38, 13)
		Me.Label2.Location = New System.Drawing.Point(394, 34)
		Me.Label2.TabIndex = 8
		Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopLeft
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
		Me.Label1.Text = "Name :"
		Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.Size = New System.Drawing.Size(41, 13)
		Me.Label1.Location = New System.Drawing.Point(8, 34)
		Me.Label1.TabIndex = 7
		Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopLeft
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
		Me.Frame3.Size = New System.Drawing.Size(529, 51)
		Me.Frame3.Location = New System.Drawing.Point(0, 150)
		Me.Frame3.TabIndex = 2
		Me.Frame3.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame3.BackColor = System.Drawing.SystemColors.Control
		Me.Frame3.Enabled = True
		Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame3.Visible = True
		Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame3.Name = "Frame3"
		Me.cmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.cmdSave.BackColor = System.Drawing.SystemColors.Menu
		Me.cmdSave.Text = "&Save"
		Me.cmdSave.Size = New System.Drawing.Size(67, 37)
		Me.cmdSave.Location = New System.Drawing.Point(4, 10)
		Me.cmdSave.Image = CType(resources.GetObject("cmdSave.Image"), System.Drawing.Image)
		Me.cmdSave.TabIndex = 0
		Me.ToolTip1.SetToolTip(Me.cmdSave, "Save Current Record")
		Me.cmdSave.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdSave.CausesValidation = True
		Me.cmdSave.Enabled = True
		Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdSave.TabStop = True
		Me.cmdSave.Name = "cmdSave"
		Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.cmdClose.BackColor = System.Drawing.SystemColors.Menu
		Me.cmdClose.Text = "&Close"
		Me.cmdClose.Size = New System.Drawing.Size(67, 37)
		Me.cmdClose.Location = New System.Drawing.Point(458, 10)
		Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
		Me.cmdClose.TabIndex = 1
		Me.ToolTip1.SetToolTip(Me.cmdClose, "Close the form")
		Me.cmdClose.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdClose.CausesValidation = True
		Me.cmdClose.Enabled = True
		Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdClose.TabStop = True
		Me.cmdClose.Name = "cmdClose"
		Me.lblStatus.Size = New System.Drawing.Size(373, 39)
		Me.lblStatus.Location = New System.Drawing.Point(78, 8)
		Me.lblStatus.TabIndex = 15
		Me.lblStatus.Visible = False
		Me.lblStatus.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.lblStatus.BackColor = System.Drawing.SystemColors.Control
		Me.lblStatus.Enabled = True
		Me.lblStatus.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblStatus.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblStatus.UseMnemonic = True
		Me.lblStatus.AutoSize = False
		Me.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.lblStatus.Name = "lblStatus"
		Me.Controls.Add(Frame2)
		Me.Controls.Add(Frame1)
		Me.Controls.Add(Frame3)
		Me.Frame2.Controls.Add(cmdSearchItemTo)
		Me.Frame2.Controls.Add(txtItemNameTo)
		Me.Frame2.Controls.Add(txtItemCodeTo)
		Me.Frame2.Controls.Add(Label4)
		Me.Frame2.Controls.Add(Label3)
		Me.Frame1.Controls.Add(txtItemCodeFrom)
		Me.Frame1.Controls.Add(txtItemNameFrom)
		Me.Frame1.Controls.Add(cmdSearchItemFrom)
		Me.Frame1.Controls.Add(Label2)
		Me.Frame1.Controls.Add(Label1)
		Me.Frame3.Controls.Add(cmdSave)
		Me.Frame3.Controls.Add(cmdClose)
		Me.Frame3.Controls.Add(lblStatus)
		Me.Frame2.ResumeLayout(False)
		Me.Frame1.ResumeLayout(False)
		Me.Frame3.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()
	End Sub
#End Region 
End Class