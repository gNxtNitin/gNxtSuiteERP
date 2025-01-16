Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmDespNoteSeq
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
	Public WithEvents _optUpdate_1 As System.Windows.Forms.RadioButton
	Public WithEvents _optUpdate_0 As System.Windows.Forms.RadioButton
	Public WithEvents txtToDNo As System.Windows.Forms.TextBox
	Public WithEvents txtToDNDate As System.Windows.Forms.TextBox
	Public WithEvents Label4 As System.Windows.Forms.Label
	Public WithEvents Label3 As System.Windows.Forms.Label
	Public WithEvents Frame2 As System.Windows.Forms.GroupBox
	Public WithEvents txtFromDNDate As System.Windows.Forms.TextBox
	Public WithEvents txtFromDNo As System.Windows.Forms.TextBox
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents Frame1 As System.Windows.Forms.GroupBox
	Public WithEvents cmdSave As System.Windows.Forms.Button
	Public WithEvents cmdClose As System.Windows.Forms.Button
	Public WithEvents lblStatus As System.Windows.Forms.Label
	Public WithEvents Frame3 As System.Windows.Forms.GroupBox
	Public WithEvents optUpdate As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmDespNoteSeq))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me._optUpdate_1 = New System.Windows.Forms.RadioButton()
        Me._optUpdate_0 = New System.Windows.Forms.RadioButton()
        Me.txtToDNo = New System.Windows.Forms.TextBox()
        Me.txtToDNDate = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.txtFromDNDate = New System.Windows.Forms.TextBox()
        Me.txtFromDNo = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.optUpdate = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.Frame2.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.Frame3.SuspendLayout()
        CType(Me.optUpdate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.cmdClose.Location = New System.Drawing.Point(458, 10)
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
        Me.Frame2.Controls.Add(Me._optUpdate_1)
        Me.Frame2.Controls.Add(Me._optUpdate_0)
        Me.Frame2.Controls.Add(Me.txtToDNo)
        Me.Frame2.Controls.Add(Me.txtToDNDate)
        Me.Frame2.Controls.Add(Me.Label4)
        Me.Frame2.Controls.Add(Me.Label3)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 78)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(529, 77)
        Me.Frame2.TabIndex = 8
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Old Despatch Note"
        '
        '_optUpdate_1
        '
        Me._optUpdate_1.AutoSize = True
        Me._optUpdate_1.BackColor = System.Drawing.SystemColors.Control
        Me._optUpdate_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optUpdate_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optUpdate_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optUpdate.SetIndex(Me._optUpdate_1, CType(1, Short))
        Me._optUpdate_1.Location = New System.Drawing.Point(268, 16)
        Me._optUpdate_1.Name = "_optUpdate_1"
        Me._optUpdate_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optUpdate_1.Size = New System.Drawing.Size(96, 18)
        Me._optUpdate_1.TabIndex = 15
        Me._optUpdate_1.TabStop = True
        Me._optUpdate_1.Text = "Blank Update"
        Me._optUpdate_1.UseVisualStyleBackColor = False
        '
        '_optUpdate_0
        '
        Me._optUpdate_0.AutoSize = True
        Me._optUpdate_0.BackColor = System.Drawing.SystemColors.Control
        Me._optUpdate_0.Checked = True
        Me._optUpdate_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optUpdate_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optUpdate_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optUpdate.SetIndex(Me._optUpdate_0, CType(0, Short))
        Me._optUpdate_0.Location = New System.Drawing.Point(126, 16)
        Me._optUpdate_0.Name = "_optUpdate_0"
        Me._optUpdate_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optUpdate_0.Size = New System.Drawing.Size(90, 18)
        Me._optUpdate_0.TabIndex = 14
        Me._optUpdate_0.TabStop = True
        Me._optUpdate_0.Text = "New Update"
        Me._optUpdate_0.UseVisualStyleBackColor = False
        '
        'txtToDNo
        '
        Me.txtToDNo.AcceptsReturn = True
        Me.txtToDNo.BackColor = System.Drawing.Color.White
        Me.txtToDNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtToDNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtToDNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToDNo.ForeColor = System.Drawing.Color.Blue
        Me.txtToDNo.Location = New System.Drawing.Point(128, 40)
        Me.txtToDNo.MaxLength = 0
        Me.txtToDNo.Name = "txtToDNo"
        Me.txtToDNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtToDNo.Size = New System.Drawing.Size(153, 19)
        Me.txtToDNo.TabIndex = 10
        '
        'txtToDNDate
        '
        Me.txtToDNDate.AcceptsReturn = True
        Me.txtToDNDate.BackColor = System.Drawing.Color.White
        Me.txtToDNDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtToDNDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtToDNDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToDNDate.ForeColor = System.Drawing.Color.Blue
        Me.txtToDNDate.Location = New System.Drawing.Point(418, 40)
        Me.txtToDNDate.MaxLength = 0
        Me.txtToDNDate.Name = "txtToDNDate"
        Me.txtToDNDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtToDNDate.Size = New System.Drawing.Size(85, 19)
        Me.txtToDNDate.TabIndex = 9
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(66, 42)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(55, 14)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Old DNo :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(334, 42)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(75, 14)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Old DN Date :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.txtFromDNDate)
        Me.Frame1.Controls.Add(Me.txtFromDNo)
        Me.Frame1.Controls.Add(Me.Label2)
        Me.Frame1.Controls.Add(Me.Label1)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(529, 77)
        Me.Frame1.TabIndex = 3
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "New Despatch Note"
        '
        'txtFromDNDate
        '
        Me.txtFromDNDate.AcceptsReturn = True
        Me.txtFromDNDate.BackColor = System.Drawing.Color.White
        Me.txtFromDNDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFromDNDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFromDNDate.Enabled = False
        Me.txtFromDNDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromDNDate.ForeColor = System.Drawing.Color.Blue
        Me.txtFromDNDate.Location = New System.Drawing.Point(418, 32)
        Me.txtFromDNDate.MaxLength = 0
        Me.txtFromDNDate.Name = "txtFromDNDate"
        Me.txtFromDNDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFromDNDate.Size = New System.Drawing.Size(85, 19)
        Me.txtFromDNDate.TabIndex = 5
        '
        'txtFromDNo
        '
        Me.txtFromDNo.AcceptsReturn = True
        Me.txtFromDNo.BackColor = System.Drawing.Color.White
        Me.txtFromDNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFromDNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFromDNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromDNo.ForeColor = System.Drawing.Color.Blue
        Me.txtFromDNo.Location = New System.Drawing.Point(128, 32)
        Me.txtFromDNo.MaxLength = 0
        Me.txtFromDNo.Name = "txtFromDNo"
        Me.txtFromDNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFromDNo.Size = New System.Drawing.Size(153, 19)
        Me.txtFromDNo.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(306, 34)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(81, 14)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "New DN Date :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(16, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(61, 14)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "New DNo :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.cmdSave)
        Me.Frame3.Controls.Add(Me.cmdClose)
        Me.Frame3.Controls.Add(Me.lblStatus)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 150)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(529, 51)
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
        Me.lblStatus.Size = New System.Drawing.Size(373, 39)
        Me.lblStatus.TabIndex = 13
        Me.lblStatus.Visible = False
        '
        'FrmDespNoteSeq
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(530, 202)
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
        Me.Name = "FrmDespNoteSeq"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds
        Me.Text = "Update Despatch Note Seq"
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        CType(Me.optUpdate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class