Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmPrintPMSchd
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
    Public WithEvents optPMNotDone As System.Windows.Forms.RadioButton
    Public WithEvents optPMDone As System.Windows.Forms.RadioButton
    Public WithEvents optAll As System.Windows.Forms.RadioButton
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents cmdOk As System.Windows.Forms.Button
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents FraOk As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrintPMSchd))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.optPMNotDone = New System.Windows.Forms.RadioButton()
        Me.optPMDone = New System.Windows.Forms.RadioButton()
        Me.optAll = New System.Windows.Forms.RadioButton()
        Me.FraOk = New System.Windows.Forms.GroupBox()
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.Frame1.SuspendLayout()
        Me.FraOk.SuspendLayout()
        Me.SuspendLayout()
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.optPMNotDone)
        Me.Frame1.Controls.Add(Me.optPMDone)
        Me.Frame1.Controls.Add(Me.optAll)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(201, 97)
        Me.Frame1.TabIndex = 0
        Me.Frame1.TabStop = False
        '
        'optPMNotDone
        '
        Me.optPMNotDone.AutoSize = True
        Me.optPMNotDone.BackColor = System.Drawing.SystemColors.Control
        Me.optPMNotDone.Cursor = System.Windows.Forms.Cursors.Default
        Me.optPMNotDone.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optPMNotDone.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPMNotDone.Location = New System.Drawing.Point(42, 72)
        Me.optPMNotDone.Name = "optPMNotDone"
        Me.optPMNotDone.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optPMNotDone.Size = New System.Drawing.Size(94, 18)
        Me.optPMNotDone.TabIndex = 6
        Me.optPMNotDone.TabStop = True
        Me.optPMNotDone.Text = "PM Not Done"
        Me.optPMNotDone.UseVisualStyleBackColor = False
        '
        'optPMDone
        '
        Me.optPMDone.AutoSize = True
        Me.optPMDone.BackColor = System.Drawing.SystemColors.Control
        Me.optPMDone.Cursor = System.Windows.Forms.Cursors.Default
        Me.optPMDone.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optPMDone.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPMDone.Location = New System.Drawing.Point(42, 46)
        Me.optPMDone.Name = "optPMDone"
        Me.optPMDone.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optPMDone.Size = New System.Drawing.Size(73, 18)
        Me.optPMDone.TabIndex = 5
        Me.optPMDone.TabStop = True
        Me.optPMDone.Text = "PM Done"
        Me.optPMDone.UseVisualStyleBackColor = False
        '
        'optAll
        '
        Me.optAll.AutoSize = True
        Me.optAll.BackColor = System.Drawing.SystemColors.Control
        Me.optAll.Checked = True
        Me.optAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.optAll.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optAll.Location = New System.Drawing.Point(42, 20)
        Me.optAll.Name = "optAll"
        Me.optAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optAll.Size = New System.Drawing.Size(95, 18)
        Me.optAll.TabIndex = 1
        Me.optAll.TabStop = True
        Me.optAll.Text = "All Machines"
        Me.optAll.UseVisualStyleBackColor = False
        '
        'FraOk
        '
        Me.FraOk.BackColor = System.Drawing.SystemColors.Control
        Me.FraOk.Controls.Add(Me.cmdOk)
        Me.FraOk.Controls.Add(Me.cmdCancel)
        Me.FraOk.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraOk.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraOk.Location = New System.Drawing.Point(0, 94)
        Me.FraOk.Name = "FraOk"
        Me.FraOk.Padding = New System.Windows.Forms.Padding(0)
        Me.FraOk.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraOk.Size = New System.Drawing.Size(201, 43)
        Me.FraOk.TabIndex = 2
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
        Me.cmdCancel.Location = New System.Drawing.Point(116, 12)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(73, 25)
        Me.cmdCancel.TabIndex = 3
        Me.cmdCancel.Text = "&Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'frmPrintPMSchd
        '
        Me.AcceptButton = Me.cmdOk
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(201, 138)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.FraOk)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(144, 135)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPrintPMSchd"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Print"
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.FraOk.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class