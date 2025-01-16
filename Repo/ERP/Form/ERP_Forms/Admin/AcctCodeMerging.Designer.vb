Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmAcctCodeMerging
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
	Public WithEvents cmdSearchAcctTo As System.Windows.Forms.Button
	Public WithEvents txtAcctNameTo As System.Windows.Forms.TextBox
	Public WithEvents txtAcctCodeTo As System.Windows.Forms.TextBox
	Public WithEvents Label4 As System.Windows.Forms.Label
	Public WithEvents Label3 As System.Windows.Forms.Label
	Public WithEvents Frame2 As System.Windows.Forms.GroupBox
	Public WithEvents txtAcctCodeFrom As System.Windows.Forms.TextBox
	Public WithEvents txtAcctNameFrom As System.Windows.Forms.TextBox
	Public WithEvents cmdSearchAcctFrom As System.Windows.Forms.Button
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAcctCodeMerging))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearchAcctTo = New System.Windows.Forms.Button()
        Me.cmdSearchAcctFrom = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.txtLocationTo = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtAcctNameTo = New System.Windows.Forms.TextBox()
        Me.txtAcctCodeTo = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.txtLocationFrom = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtAcctCodeFrom = New System.Windows.Forms.TextBox()
        Me.txtAcctNameFrom = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.Frame2.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdSearchAcctTo
        '
        Me.cmdSearchAcctTo.AutoSize = True
        Me.cmdSearchAcctTo.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchAcctTo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchAcctTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchAcctTo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchAcctTo.Image = CType(resources.GetObject("cmdSearchAcctTo.Image"), System.Drawing.Image)
        Me.cmdSearchAcctTo.Location = New System.Drawing.Point(406, 27)
        Me.cmdSearchAcctTo.Name = "cmdSearchAcctTo"
        Me.cmdSearchAcctTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchAcctTo.Size = New System.Drawing.Size(32, 26)
        Me.cmdSearchAcctTo.TabIndex = 12
        Me.cmdSearchAcctTo.TabStop = False
        Me.cmdSearchAcctTo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchAcctTo, "Search")
        Me.cmdSearchAcctTo.UseVisualStyleBackColor = False
        '
        'cmdSearchAcctFrom
        '
        Me.cmdSearchAcctFrom.AutoSize = True
        Me.cmdSearchAcctFrom.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchAcctFrom.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchAcctFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchAcctFrom.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchAcctFrom.Image = CType(resources.GetObject("cmdSearchAcctFrom.Image"), System.Drawing.Image)
        Me.cmdSearchAcctFrom.Location = New System.Drawing.Point(406, 27)
        Me.cmdSearchAcctFrom.Name = "cmdSearchAcctFrom"
        Me.cmdSearchAcctFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchAcctFrom.Size = New System.Drawing.Size(32, 26)
        Me.cmdSearchAcctFrom.TabIndex = 4
        Me.cmdSearchAcctFrom.TabStop = False
        Me.cmdSearchAcctFrom.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchAcctFrom, "Search")
        Me.cmdSearchAcctFrom.UseVisualStyleBackColor = False
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Image = CType(resources.GetObject("cmdSave.Image"), System.Drawing.Image)
        Me.cmdSave.Location = New System.Drawing.Point(4, 10)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(67, 37)
        Me.cmdSave.TabIndex = 0
        Me.cmdSave.Text = "&Save"
        Me.cmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSave, "Save Current Record")
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.Location = New System.Drawing.Point(500, 10)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(67, 37)
        Me.cmdClose.TabIndex = 1
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdClose, "Close the form")
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.txtLocationTo)
        Me.Frame2.Controls.Add(Me.Label6)
        Me.Frame2.Controls.Add(Me.cmdSearchAcctTo)
        Me.Frame2.Controls.Add(Me.txtAcctNameTo)
        Me.Frame2.Controls.Add(Me.txtAcctCodeTo)
        Me.Frame2.Controls.Add(Me.Label4)
        Me.Frame2.Controls.Add(Me.Label3)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 104)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(583, 94)
        Me.Frame2.TabIndex = 9
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Account To"
        '
        'txtLocationTo
        '
        Me.txtLocationTo.AcceptsReturn = True
        Me.txtLocationTo.BackColor = System.Drawing.Color.White
        Me.txtLocationTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLocationTo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLocationTo.Enabled = False
        Me.txtLocationTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocationTo.ForeColor = System.Drawing.Color.Blue
        Me.txtLocationTo.Location = New System.Drawing.Point(75, 52)
        Me.txtLocationTo.MaxLength = 0
        Me.txtLocationTo.Name = "txtLocationTo"
        Me.txtLocationTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLocationTo.Size = New System.Drawing.Size(328, 20)
        Me.txtLocationTo.TabIndex = 15
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(11, 54)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(60, 14)
        Me.Label6.TabIndex = 16
        Me.Label6.Text = "Location :"
        '
        'txtAcctNameTo
        '
        Me.txtAcctNameTo.AcceptsReturn = True
        Me.txtAcctNameTo.BackColor = System.Drawing.Color.White
        Me.txtAcctNameTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAcctNameTo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAcctNameTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAcctNameTo.ForeColor = System.Drawing.Color.Blue
        Me.txtAcctNameTo.Location = New System.Drawing.Point(75, 27)
        Me.txtAcctNameTo.MaxLength = 0
        Me.txtAcctNameTo.Name = "txtAcctNameTo"
        Me.txtAcctNameTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAcctNameTo.Size = New System.Drawing.Size(328, 20)
        Me.txtAcctNameTo.TabIndex = 11
        '
        'txtAcctCodeTo
        '
        Me.txtAcctCodeTo.AcceptsReturn = True
        Me.txtAcctCodeTo.BackColor = System.Drawing.Color.White
        Me.txtAcctCodeTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAcctCodeTo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAcctCodeTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAcctCodeTo.ForeColor = System.Drawing.Color.Blue
        Me.txtAcctCodeTo.Location = New System.Drawing.Point(491, 27)
        Me.txtAcctCodeTo.MaxLength = 0
        Me.txtAcctCodeTo.Name = "txtAcctCodeTo"
        Me.txtAcctCodeTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAcctCodeTo.Size = New System.Drawing.Size(85, 20)
        Me.txtAcctCodeTo.TabIndex = 10
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(27, 29)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(44, 14)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "Name :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(445, 27)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(42, 14)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "Code :"
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.txtLocationFrom)
        Me.Frame1.Controls.Add(Me.Label5)
        Me.Frame1.Controls.Add(Me.txtAcctCodeFrom)
        Me.Frame1.Controls.Add(Me.txtAcctNameFrom)
        Me.Frame1.Controls.Add(Me.cmdSearchAcctFrom)
        Me.Frame1.Controls.Add(Me.Label2)
        Me.Frame1.Controls.Add(Me.Label1)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(583, 94)
        Me.Frame1.TabIndex = 3
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Account From"
        '
        'txtLocationFrom
        '
        Me.txtLocationFrom.AcceptsReturn = True
        Me.txtLocationFrom.BackColor = System.Drawing.Color.White
        Me.txtLocationFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLocationFrom.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLocationFrom.Enabled = False
        Me.txtLocationFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocationFrom.ForeColor = System.Drawing.Color.Blue
        Me.txtLocationFrom.Location = New System.Drawing.Point(75, 52)
        Me.txtLocationFrom.MaxLength = 0
        Me.txtLocationFrom.Name = "txtLocationFrom"
        Me.txtLocationFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLocationFrom.Size = New System.Drawing.Size(328, 20)
        Me.txtLocationFrom.TabIndex = 9
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(11, 54)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(60, 14)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Location :"
        '
        'txtAcctCodeFrom
        '
        Me.txtAcctCodeFrom.AcceptsReturn = True
        Me.txtAcctCodeFrom.BackColor = System.Drawing.Color.White
        Me.txtAcctCodeFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAcctCodeFrom.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAcctCodeFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAcctCodeFrom.ForeColor = System.Drawing.Color.Blue
        Me.txtAcctCodeFrom.Location = New System.Drawing.Point(491, 27)
        Me.txtAcctCodeFrom.MaxLength = 0
        Me.txtAcctCodeFrom.Name = "txtAcctCodeFrom"
        Me.txtAcctCodeFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAcctCodeFrom.Size = New System.Drawing.Size(85, 20)
        Me.txtAcctCodeFrom.TabIndex = 6
        '
        'txtAcctNameFrom
        '
        Me.txtAcctNameFrom.AcceptsReturn = True
        Me.txtAcctNameFrom.BackColor = System.Drawing.Color.White
        Me.txtAcctNameFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAcctNameFrom.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAcctNameFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAcctNameFrom.ForeColor = System.Drawing.Color.Blue
        Me.txtAcctNameFrom.Location = New System.Drawing.Point(75, 27)
        Me.txtAcctNameFrom.MaxLength = 0
        Me.txtAcctNameFrom.Name = "txtAcctNameFrom"
        Me.txtAcctNameFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAcctNameFrom.Size = New System.Drawing.Size(328, 20)
        Me.txtAcctNameFrom.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(445, 27)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(42, 14)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Code :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(27, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(44, 14)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Name :"
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.cmdSave)
        Me.Frame3.Controls.Add(Me.cmdClose)
        Me.Frame3.Controls.Add(Me.lblStatus)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 200)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(582, 51)
        Me.Frame3.TabIndex = 2
        Me.Frame3.TabStop = False
        '
        'lblStatus
        '
        Me.lblStatus.BackColor = System.Drawing.SystemColors.Control
        Me.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblStatus.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblStatus.Location = New System.Drawing.Point(78, 8)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblStatus.Size = New System.Drawing.Size(412, 39)
        Me.lblStatus.TabIndex = 15
        Me.lblStatus.Visible = False
        '
        'FrmAcctCodeMerging
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(585, 252)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.Frame3)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmAcctCodeMerging"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds
        Me.Text = "Account Merging"
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Public WithEvents txtLocationTo As TextBox
    Public WithEvents Label6 As Label
    Public WithEvents txtLocationFrom As TextBox
    Public WithEvents Label5 As Label
#End Region
End Class