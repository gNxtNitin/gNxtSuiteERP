Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmPrintAppLtr
#Region "Windows Form Designer generated code "
    <System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
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
    Public WithEvents _OptPrint_2 As System.Windows.Forms.RadioButton
    Public WithEvents _OptPrint_0 As System.Windows.Forms.RadioButton
    Public WithEvents _OptPrint_1 As System.Windows.Forms.RadioButton
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents cmdOk As System.Windows.Forms.Button
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents FraOk As System.Windows.Forms.GroupBox
    Public WithEvents OptPrint As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrintAppLtr))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me._OptPrint_2 = New System.Windows.Forms.RadioButton()
        Me._OptPrint_0 = New System.Windows.Forms.RadioButton()
        Me._OptPrint_1 = New System.Windows.Forms.RadioButton()
        Me.FraOk = New System.Windows.Forms.GroupBox()
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.OptPrint = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me._OptPrint_3 = New System.Windows.Forms.RadioButton()
        Me._OptPrint_4 = New System.Windows.Forms.RadioButton()
        Me.Frame1.SuspendLayout()
        Me.FraOk.SuspendLayout()
        CType(Me.OptPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me._OptPrint_4)
        Me.Frame1.Controls.Add(Me._OptPrint_3)
        Me.Frame1.Controls.Add(Me._OptPrint_2)
        Me.Frame1.Controls.Add(Me._OptPrint_0)
        Me.Frame1.Controls.Add(Me._OptPrint_1)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(201, 140)
        Me.Frame1.TabIndex = 3
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Printing Status"
        '
        '_OptPrint_2
        '
        Me._OptPrint_2.AutoSize = True
        Me._OptPrint_2.BackColor = System.Drawing.SystemColors.Control
        Me._OptPrint_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptPrint_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptPrint_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptPrint.SetIndex(Me._OptPrint_2, CType(2, Short))
        Me._OptPrint_2.Location = New System.Drawing.Point(18, 66)
        Me._OptPrint_2.Name = "_OptPrint_2"
        Me._OptPrint_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptPrint_2.Size = New System.Drawing.Size(104, 18)
        Me._OptPrint_2.TabIndex = 6
        Me._OptPrint_2.TabStop = True
        Me._OptPrint_2.Text = "Salary Structure"
        Me._OptPrint_2.UseVisualStyleBackColor = False
        '
        '_OptPrint_0
        '
        Me._OptPrint_0.AutoSize = True
        Me._OptPrint_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptPrint_0.Checked = True
        Me._OptPrint_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptPrint_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptPrint_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptPrint.SetIndex(Me._OptPrint_0, CType(0, Short))
        Me._OptPrint_0.Location = New System.Drawing.Point(18, 20)
        Me._OptPrint_0.Name = "_OptPrint_0"
        Me._OptPrint_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptPrint_0.Size = New System.Drawing.Size(116, 18)
        Me._OptPrint_0.TabIndex = 5
        Me._OptPrint_0.TabStop = True
        Me._OptPrint_0.Text = "Appointment Letter"
        Me._OptPrint_0.UseVisualStyleBackColor = False
        '
        '_OptPrint_1
        '
        Me._OptPrint_1.AutoSize = True
        Me._OptPrint_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptPrint_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptPrint_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptPrint_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptPrint.SetIndex(Me._OptPrint_1, CType(1, Short))
        Me._OptPrint_1.Location = New System.Drawing.Point(18, 43)
        Me._OptPrint_1.Name = "_OptPrint_1"
        Me._OptPrint_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptPrint_1.Size = New System.Drawing.Size(130, 18)
        Me._OptPrint_1.TabIndex = 4
        Me._OptPrint_1.TabStop = True
        Me._OptPrint_1.Text = "Letter of Intent / Offer"
        Me._OptPrint_1.UseVisualStyleBackColor = False
        '
        'FraOk
        '
        Me.FraOk.BackColor = System.Drawing.SystemColors.Control
        Me.FraOk.Controls.Add(Me.cmdOk)
        Me.FraOk.Controls.Add(Me.cmdCancel)
        Me.FraOk.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraOk.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraOk.Location = New System.Drawing.Point(0, 138)
        Me.FraOk.Name = "FraOk"
        Me.FraOk.Padding = New System.Windows.Forms.Padding(0)
        Me.FraOk.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraOk.Size = New System.Drawing.Size(201, 43)
        Me.FraOk.TabIndex = 0
        Me.FraOk.TabStop = False
        '
        'cmdOk
        '
        Me.cmdOk.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOk.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOk.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOk.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOk.Location = New System.Drawing.Point(8, 12)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOk.Size = New System.Drawing.Size(73, 25)
        Me.cmdOk.TabIndex = 2
        Me.cmdOk.Text = "&Ok"
        Me.cmdOk.UseVisualStyleBackColor = False
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(116, 12)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(73, 25)
        Me.cmdCancel.TabIndex = 1
        Me.cmdCancel.Text = "&Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        '_OptPrint_3
        '
        Me._OptPrint_3.AutoSize = True
        Me._OptPrint_3.BackColor = System.Drawing.SystemColors.Control
        Me._OptPrint_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptPrint_3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptPrint_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptPrint.SetIndex(Me._OptPrint_3, CType(3, Short))
        Me._OptPrint_3.Location = New System.Drawing.Point(18, 89)
        Me._OptPrint_3.Name = "_OptPrint_3"
        Me._OptPrint_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptPrint_3.Size = New System.Drawing.Size(116, 18)
        Me._OptPrint_3.TabIndex = 7
        Me._OptPrint_3.TabStop = True
        Me._OptPrint_3.Text = "Confirmation Letter"
        Me._OptPrint_3.UseVisualStyleBackColor = False
        '
        '_OptPrint_4
        '
        Me._OptPrint_4.AutoSize = True
        Me._OptPrint_4.BackColor = System.Drawing.SystemColors.Control
        Me._OptPrint_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptPrint_4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptPrint_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptPrint.SetIndex(Me._OptPrint_4, CType(4, Short))
        Me._OptPrint_4.Location = New System.Drawing.Point(18, 112)
        Me._OptPrint_4.Name = "_OptPrint_4"
        Me._OptPrint_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptPrint_4.Size = New System.Drawing.Size(73, 18)
        Me._OptPrint_4.TabIndex = 8
        Me._OptPrint_4.TabStop = True
        Me._OptPrint_4.Text = "Joining Kit"
        Me._OptPrint_4.UseVisualStyleBackColor = False
        '
        'frmPrintAppLtr
        '
        Me.AcceptButton = Me.cmdOk
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(201, 183)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.FraOk)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(144, 135)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPrintAppLtr"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Print"
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.FraOk.ResumeLayout(False)
        CType(Me.OptPrint, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Public WithEvents _OptPrint_3 As RadioButton
    Public WithEvents _OptPrint_4 As RadioButton
#End Region
End Class