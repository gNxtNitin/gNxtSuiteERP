Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmPrintForm16
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
    Public WithEvents _optPrint_3 As System.Windows.Forms.RadioButton
    Public WithEvents _optPrint_2 As System.Windows.Forms.RadioButton
    Public WithEvents _optPrint_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optPrint_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents cmdOk As System.Windows.Forms.Button
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents FraOk As System.Windows.Forms.GroupBox
    Public WithEvents optPrint As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrintForm16))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me._optPrint_3 = New System.Windows.Forms.RadioButton()
        Me._optPrint_2 = New System.Windows.Forms.RadioButton()
        Me._optPrint_1 = New System.Windows.Forms.RadioButton()
        Me._optPrint_0 = New System.Windows.Forms.RadioButton()
        Me.FraOk = New System.Windows.Forms.GroupBox()
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.optPrint = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.Frame1.SuspendLayout()
        Me.FraOk.SuspendLayout()
        CType(Me.optPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me._optPrint_3)
        Me.Frame1.Controls.Add(Me._optPrint_2)
        Me.Frame1.Controls.Add(Me._optPrint_1)
        Me.Frame1.Controls.Add(Me._optPrint_0)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(205, 121)
        Me.Frame1.TabIndex = 0
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Printing Status"
        '
        '_optPrint_3
        '
        Me._optPrint_3.AutoSize = True
        Me._optPrint_3.BackColor = System.Drawing.SystemColors.Control
        Me._optPrint_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._optPrint_3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optPrint_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPrint.SetIndex(Me._optPrint_3, CType(3, Short))
        Me._optPrint_3.Location = New System.Drawing.Point(28, 92)
        Me._optPrint_3.Name = "_optPrint_3"
        Me._optPrint_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optPrint_3.Size = New System.Drawing.Size(87, 18)
        Me._optPrint_3.TabIndex = 7
        Me._optPrint_3.TabStop = True
        Me._optPrint_3.Text = "Annexure 'B'"
        Me._optPrint_3.UseVisualStyleBackColor = False
        '
        '_optPrint_2
        '
        Me._optPrint_2.AutoSize = True
        Me._optPrint_2.BackColor = System.Drawing.SystemColors.Control
        Me._optPrint_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optPrint_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optPrint_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPrint.SetIndex(Me._optPrint_2, CType(2, Short))
        Me._optPrint_2.Location = New System.Drawing.Point(28, 70)
        Me._optPrint_2.Name = "_optPrint_2"
        Me._optPrint_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optPrint_2.Size = New System.Drawing.Size(58, 18)
        Me._optPrint_2.TabIndex = 6
        Me._optPrint_2.TabStop = True
        Me._optPrint_2.Text = "Part 'B'"
        Me._optPrint_2.UseVisualStyleBackColor = False
        '
        '_optPrint_1
        '
        Me._optPrint_1.AutoSize = True
        Me._optPrint_1.BackColor = System.Drawing.SystemColors.Control
        Me._optPrint_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optPrint_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optPrint_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPrint.SetIndex(Me._optPrint_1, CType(1, Short))
        Me._optPrint_1.Location = New System.Drawing.Point(28, 46)
        Me._optPrint_1.Name = "_optPrint_1"
        Me._optPrint_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optPrint_1.Size = New System.Drawing.Size(59, 18)
        Me._optPrint_1.TabIndex = 5
        Me._optPrint_1.TabStop = True
        Me._optPrint_1.Text = "Part 'A'"
        Me._optPrint_1.UseVisualStyleBackColor = False
        '
        '_optPrint_0
        '
        Me._optPrint_0.AutoSize = True
        Me._optPrint_0.BackColor = System.Drawing.SystemColors.Control
        Me._optPrint_0.Checked = True
        Me._optPrint_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optPrint_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optPrint_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPrint.SetIndex(Me._optPrint_0, CType(0, Short))
        Me._optPrint_0.Location = New System.Drawing.Point(28, 24)
        Me._optPrint_0.Name = "_optPrint_0"
        Me._optPrint_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optPrint_0.Size = New System.Drawing.Size(91, 18)
        Me._optPrint_0.TabIndex = 1
        Me._optPrint_0.TabStop = True
        Me._optPrint_0.Text = "Form 16 (Full)"
        Me._optPrint_0.UseVisualStyleBackColor = False
        '
        'FraOk
        '
        Me.FraOk.BackColor = System.Drawing.SystemColors.Control
        Me.FraOk.Controls.Add(Me.cmdOk)
        Me.FraOk.Controls.Add(Me.cmdCancel)
        Me.FraOk.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraOk.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraOk.Location = New System.Drawing.Point(0, 116)
        Me.FraOk.Name = "FraOk"
        Me.FraOk.Padding = New System.Windows.Forms.Padding(0)
        Me.FraOk.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraOk.Size = New System.Drawing.Size(205, 43)
        Me.FraOk.TabIndex = 2
        Me.FraOk.TabStop = False
        '
        'cmdOk
        '
        Me.cmdOk.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOk.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOk.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOk.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOk.Location = New System.Drawing.Point(4, 12)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOk.Size = New System.Drawing.Size(57, 25)
        Me.cmdOk.TabIndex = 4
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
        Me.cmdCancel.Location = New System.Drawing.Point(142, 12)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(57, 25)
        Me.cmdCancel.TabIndex = 3
        Me.cmdCancel.Text = "&Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'frmPrintForm16
        '
        Me.AcceptButton = Me.cmdOk
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(205, 160)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.FraOk)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(144, 135)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPrintForm16"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Print Form 16"
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.FraOk.ResumeLayout(False)
        CType(Me.optPrint, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class