Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmPrintRating
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
    Public WithEvents cmdOk As System.Windows.Forms.Button
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents FraOk As System.Windows.Forms.GroupBox
    Public WithEvents _OptSelected_3 As System.Windows.Forms.RadioButton
    Public WithEvents _OptSelected_4 As System.Windows.Forms.RadioButton
    Public WithEvents _OptSelected_2 As System.Windows.Forms.RadioButton
    Public WithEvents _OptSelected_1 As System.Windows.Forms.RadioButton
    Public WithEvents _OptSelected_0 As System.Windows.Forms.RadioButton
    Public WithEvents _OptSelected_5 As System.Windows.Forms.RadioButton
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents OptSelected As VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrintRating))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.FraOk = New System.Windows.Forms.GroupBox()
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me._OptSelected_3 = New System.Windows.Forms.RadioButton()
        Me._OptSelected_4 = New System.Windows.Forms.RadioButton()
        Me._OptSelected_2 = New System.Windows.Forms.RadioButton()
        Me._OptSelected_1 = New System.Windows.Forms.RadioButton()
        Me._OptSelected_0 = New System.Windows.Forms.RadioButton()
        Me._OptSelected_5 = New System.Windows.Forms.RadioButton()
        Me.OptSelected = New VB6.RadioButtonArray(Me.components)
        Me.FraOk.SuspendLayout()
        Me.Frame1.SuspendLayout()
        CType(Me.OptSelected, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FraOk
        '
        Me.FraOk.BackColor = System.Drawing.SystemColors.Control
        Me.FraOk.Controls.Add(Me.cmdOk)
        Me.FraOk.Controls.Add(Me.cmdCancel)
        Me.FraOk.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraOk.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraOk.Location = New System.Drawing.Point(0, 122)
        Me.FraOk.Name = "FraOk"
        Me.FraOk.Padding = New System.Windows.Forms.Padding(0)
        Me.FraOk.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraOk.Size = New System.Drawing.Size(201, 45)
        Me.FraOk.TabIndex = 7
        Me.FraOk.TabStop = False
        '
        'cmdOk
        '
        Me.cmdOk.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOk.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOk.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOk.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOk.Location = New System.Drawing.Point(8, 12)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOk.Size = New System.Drawing.Size(73, 27)
        Me.cmdOk.TabIndex = 9
        Me.cmdOk.Text = "&Ok"
        Me.cmdOk.UseVisualStyleBackColor = False
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(116, 12)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(73, 27)
        Me.cmdCancel.TabIndex = 8
        Me.cmdCancel.Text = "&Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me._OptSelected_3)
        Me.Frame1.Controls.Add(Me._OptSelected_4)
        Me.Frame1.Controls.Add(Me._OptSelected_2)
        Me.Frame1.Controls.Add(Me._OptSelected_1)
        Me.Frame1.Controls.Add(Me._OptSelected_0)
        Me.Frame1.Controls.Add(Me._OptSelected_5)
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(201, 121)
        Me.Frame1.TabIndex = 6
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Printing Status"
        '
        '_OptSelected_3
        '
        Me._OptSelected_3.BackColor = System.Drawing.SystemColors.Control
        Me._OptSelected_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptSelected_3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptSelected_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptSelected.SetIndex(Me._OptSelected_3, CType(3, Short))
        Me._OptSelected_3.Location = New System.Drawing.Point(6, 68)
        Me._OptSelected_3.Name = "_OptSelected_3"
        Me._OptSelected_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptSelected_3.Size = New System.Drawing.Size(191, 16)
        Me._OptSelected_3.TabIndex = 3
        Me._OptSelected_3.TabStop = True
        Me._OptSelected_3.Text = "Tabular (Order By Delivery)"
        Me._OptSelected_3.UseVisualStyleBackColor = False
        '
        '_OptSelected_4
        '
        Me._OptSelected_4.BackColor = System.Drawing.SystemColors.Control
        Me._OptSelected_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptSelected_4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptSelected_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptSelected.SetIndex(Me._OptSelected_4, CType(4, Short))
        Me._OptSelected_4.Location = New System.Drawing.Point(6, 84)
        Me._OptSelected_4.Name = "_OptSelected_4"
        Me._OptSelected_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptSelected_4.Size = New System.Drawing.Size(189, 16)
        Me._OptSelected_4.TabIndex = 4
        Me._OptSelected_4.TabStop = True
        Me._OptSelected_4.Text = "Tabular (Order By Qualitity)"
        Me._OptSelected_4.UseVisualStyleBackColor = False
        '
        '_OptSelected_2
        '
        Me._OptSelected_2.BackColor = System.Drawing.SystemColors.Control
        Me._OptSelected_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptSelected_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptSelected_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptSelected.SetIndex(Me._OptSelected_2, CType(2, Short))
        Me._OptSelected_2.Location = New System.Drawing.Point(6, 52)
        Me._OptSelected_2.Name = "_OptSelected_2"
        Me._OptSelected_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptSelected_2.Size = New System.Drawing.Size(189, 16)
        Me._OptSelected_2.TabIndex = 2
        Me._OptSelected_2.TabStop = True
        Me._OptSelected_2.Text = "Tabular (Order By Over All)"
        Me._OptSelected_2.UseVisualStyleBackColor = False
        '
        '_OptSelected_1
        '
        Me._OptSelected_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptSelected_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptSelected_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptSelected_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptSelected.SetIndex(Me._OptSelected_1, CType(1, Short))
        Me._OptSelected_1.Location = New System.Drawing.Point(6, 36)
        Me._OptSelected_1.Name = "_OptSelected_1"
        Me._OptSelected_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptSelected_1.Size = New System.Drawing.Size(189, 16)
        Me._OptSelected_1.TabIndex = 1
        Me._OptSelected_1.TabStop = True
        Me._OptSelected_1.Text = "Tabular (Order By Name)"
        Me._OptSelected_1.UseVisualStyleBackColor = False
        '
        '_OptSelected_0
        '
        Me._OptSelected_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptSelected_0.Checked = True
        Me._OptSelected_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptSelected_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptSelected_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptSelected.SetIndex(Me._OptSelected_0, CType(0, Short))
        Me._OptSelected_0.Location = New System.Drawing.Point(6, 20)
        Me._OptSelected_0.Name = "_OptSelected_0"
        Me._OptSelected_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptSelected_0.Size = New System.Drawing.Size(193, 16)
        Me._OptSelected_0.TabIndex = 0
        Me._OptSelected_0.TabStop = True
        Me._OptSelected_0.Text = "Letter Format"
        Me._OptSelected_0.UseVisualStyleBackColor = False
        '
        '_OptSelected_5
        '
        Me._OptSelected_5.BackColor = System.Drawing.SystemColors.Control
        Me._OptSelected_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptSelected_5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptSelected_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptSelected.SetIndex(Me._OptSelected_5, CType(5, Short))
        Me._OptSelected_5.Location = New System.Drawing.Point(6, 100)
        Me._OptSelected_5.Name = "_OptSelected_5"
        Me._OptSelected_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptSelected_5.Size = New System.Drawing.Size(177, 16)
        Me._OptSelected_5.TabIndex = 5
        Me._OptSelected_5.TabStop = True
        Me._OptSelected_5.Text = "Tabular (Order By Service)"
        Me._OptSelected_5.UseVisualStyleBackColor = False
        '
        'frmPrintRating
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(201, 169)
        Me.Controls.Add(Me.FraOk)
        Me.Controls.Add(Me.Frame1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(144, 135)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPrintRating"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Print"
        Me.FraOk.ResumeLayout(False)
        Me.Frame1.ResumeLayout(False)
        CType(Me.OptSelected, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class