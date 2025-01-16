Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmMrrCorrection
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
        'Me.MDIParent = AccountGST.Master
        'AccountGST.Master.Show
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
	Public WithEvents FocCheck As System.Windows.Forms.CheckBox
	Public WithEvents txtMrrNo As System.Windows.Forms.TextBox
	Public WithEvents Lblfoc As System.Windows.Forms.Label
	Public WithEvents _lblMrrNo_0 As System.Windows.Forms.Label
	Public WithEvents FraView As System.Windows.Forms.GroupBox
	Public WithEvents CmdClose As System.Windows.Forms.Button
	Public WithEvents CmdSave As System.Windows.Forms.Button
	Public WithEvents FraMovement As System.Windows.Forms.GroupBox
	Public WithEvents lblMrrNo As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMrrCorrection))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.FraView = New System.Windows.Forms.GroupBox()
        Me.FocCheck = New System.Windows.Forms.CheckBox()
        Me.txtMrrNo = New System.Windows.Forms.TextBox()
        Me.Lblfoc = New System.Windows.Forms.Label()
        Me._lblMrrNo_0 = New System.Windows.Forms.Label()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.lblMrrNo = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.FraView.SuspendLayout()
        Me.FraMovement.SuspendLayout()
        CType(Me.lblMrrNo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(222, 12)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(60, 34)
        Me.CmdClose.TabIndex = 2
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.Location = New System.Drawing.Point(4, 12)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(60, 34)
        Me.CmdSave.TabIndex = 1
        Me.CmdSave.Text = "&Save"
        Me.CmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSave, "Save Record")
        Me.CmdSave.UseVisualStyleBackColor = False
        '
        'FraView
        '
        Me.FraView.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.FraView.Controls.Add(Me.FocCheck)
        Me.FraView.Controls.Add(Me.txtMrrNo)
        Me.FraView.Controls.Add(Me.Lblfoc)
        Me.FraView.Controls.Add(Me._lblMrrNo_0)
        Me.FraView.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraView.Location = New System.Drawing.Point(0, -6)
        Me.FraView.Name = "FraView"
        Me.FraView.Padding = New System.Windows.Forms.Padding(0)
        Me.FraView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraView.Size = New System.Drawing.Size(289, 79)
        Me.FraView.TabIndex = 3
        Me.FraView.TabStop = False
        '
        'FocCheck
        '
        Me.FocCheck.BackColor = System.Drawing.SystemColors.Control
        Me.FocCheck.Cursor = System.Windows.Forms.Cursors.Default
        Me.FocCheck.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FocCheck.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FocCheck.Location = New System.Drawing.Point(106, 46)
        Me.FocCheck.Name = "FocCheck"
        Me.FocCheck.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FocCheck.Size = New System.Drawing.Size(17, 21)
        Me.FocCheck.TabIndex = 7
        Me.FocCheck.Text = "Check1"
        Me.FocCheck.UseVisualStyleBackColor = False
        '
        'txtMrrNo
        '
        Me.txtMrrNo.AcceptsReturn = True
        Me.txtMrrNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtMrrNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMrrNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMrrNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMrrNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtMrrNo.Location = New System.Drawing.Point(106, 18)
        Me.txtMrrNo.MaxLength = 0
        Me.txtMrrNo.Name = "txtMrrNo"
        Me.txtMrrNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMrrNo.Size = New System.Drawing.Size(131, 21)
        Me.txtMrrNo.TabIndex = 0
        '
        'Lblfoc
        '
        Me.Lblfoc.AutoSize = True
        Me.Lblfoc.BackColor = System.Drawing.SystemColors.Control
        Me.Lblfoc.Cursor = System.Windows.Forms.Cursors.Default
        Me.Lblfoc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lblfoc.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lblfoc.Location = New System.Drawing.Point(58, 48)
        Me.Lblfoc.Name = "Lblfoc"
        Me.Lblfoc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Lblfoc.Size = New System.Drawing.Size(35, 14)
        Me.Lblfoc.TabIndex = 6
        Me.Lblfoc.Text = "FOC :"
        Me.Lblfoc.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblMrrNo_0
        '
        Me._lblMrrNo_0.AutoSize = True
        Me._lblMrrNo_0.BackColor = System.Drawing.SystemColors.Control
        Me._lblMrrNo_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblMrrNo_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblMrrNo_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMrrNo.SetIndex(Me._lblMrrNo_0, CType(0, Short))
        Me._lblMrrNo_0.Location = New System.Drawing.Point(20, 22)
        Me._lblMrrNo_0.Name = "_lblMrrNo_0"
        Me._lblMrrNo_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblMrrNo_0.Size = New System.Drawing.Size(57, 14)
        Me._lblMrrNo_0.TabIndex = 5
        Me._lblMrrNo_0.Text = "MRR No. :"
        Me._lblMrrNo_0.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.CmdSave)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 68)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(289, 49)
        Me.FraMovement.TabIndex = 4
        Me.FraMovement.TabStop = False
        '
        'frmMrrCorrection
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(289, 118)
        Me.Controls.Add(Me.FraView)
        Me.Controls.Add(Me.FraMovement)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(73, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMrrCorrection"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Reset FOC MRR"
        Me.FraView.ResumeLayout(False)
        Me.FraView.PerformLayout()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.lblMrrNo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class