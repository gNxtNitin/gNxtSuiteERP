Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmPrintRGP_F4
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
    Public WithEvents _chkPrintOption_4 As System.Windows.Forms.CheckBox
    Public WithEvents _chkPrintOption_3 As System.Windows.Forms.CheckBox
    Public WithEvents _chkPrintOption_2 As System.Windows.Forms.CheckBox
    Public WithEvents _chkPrintOption_1 As System.Windows.Forms.CheckBox
    Public WithEvents _chkPrintOption_0 As System.Windows.Forms.CheckBox
    Public WithEvents fraPrintOption As System.Windows.Forms.GroupBox
    Public WithEvents cmdOk As System.Windows.Forms.Button
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents FraOk As System.Windows.Forms.GroupBox
    Public WithEvents chkPrintOption As VB6.CheckBoxArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrintRGP_F4))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.fraPrintOption = New System.Windows.Forms.GroupBox()
        Me._chkPrintOption_4 = New System.Windows.Forms.CheckBox()
        Me._chkPrintOption_3 = New System.Windows.Forms.CheckBox()
        Me._chkPrintOption_2 = New System.Windows.Forms.CheckBox()
        Me._chkPrintOption_1 = New System.Windows.Forms.CheckBox()
        Me._chkPrintOption_0 = New System.Windows.Forms.CheckBox()
        Me.FraOk = New System.Windows.Forms.GroupBox()
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.chkPrintOption = New VB6.CheckBoxArray(Me.components)
        Me.fraPrintOption.SuspendLayout()
        Me.FraOk.SuspendLayout()
        CType(Me.chkPrintOption, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'fraPrintOption
        '
        Me.fraPrintOption.BackColor = System.Drawing.SystemColors.Control
        Me.fraPrintOption.Controls.Add(Me._chkPrintOption_4)
        Me.fraPrintOption.Controls.Add(Me._chkPrintOption_3)
        Me.fraPrintOption.Controls.Add(Me._chkPrintOption_2)
        Me.fraPrintOption.Controls.Add(Me._chkPrintOption_1)
        Me.fraPrintOption.Controls.Add(Me._chkPrintOption_0)
        Me.fraPrintOption.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraPrintOption.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraPrintOption.Location = New System.Drawing.Point(0, 0)
        Me.fraPrintOption.Name = "fraPrintOption"
        Me.fraPrintOption.Padding = New System.Windows.Forms.Padding(0)
        Me.fraPrintOption.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraPrintOption.Size = New System.Drawing.Size(201, 107)
        Me.fraPrintOption.TabIndex = 5
        Me.fraPrintOption.TabStop = False
        Me.fraPrintOption.Text = "Printing Option"
        '
        '_chkPrintOption_4
        '
        Me._chkPrintOption_4.BackColor = System.Drawing.SystemColors.Control
        Me._chkPrintOption_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkPrintOption_4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkPrintOption_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPrintOption.SetIndex(Me._chkPrintOption_4, CType(4, Short))
        Me._chkPrintOption_4.Location = New System.Drawing.Point(24, 80)
        Me._chkPrintOption_4.Name = "_chkPrintOption_4"
        Me._chkPrintOption_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkPrintOption_4.Size = New System.Drawing.Size(151, 16)
        Me._chkPrintOption_4.TabIndex = 8
        Me._chkPrintOption_4.Text = "Form JJ"
        Me._chkPrintOption_4.UseVisualStyleBackColor = False
        '
        '_chkPrintOption_3
        '
        Me._chkPrintOption_3.BackColor = System.Drawing.SystemColors.Control
        Me._chkPrintOption_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkPrintOption_3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkPrintOption_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPrintOption.SetIndex(Me._chkPrintOption_3, CType(3, Short))
        Me._chkPrintOption_3.Location = New System.Drawing.Point(24, 66)
        Me._chkPrintOption_3.Name = "_chkPrintOption_3"
        Me._chkPrintOption_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkPrintOption_3.Size = New System.Drawing.Size(151, 16)
        Me._chkPrintOption_3.TabIndex = 7
        Me._chkPrintOption_3.Text = "Consumption Detail"
        Me._chkPrintOption_3.UseVisualStyleBackColor = False
        '
        '_chkPrintOption_2
        '
        Me._chkPrintOption_2.BackColor = System.Drawing.SystemColors.Control
        Me._chkPrintOption_2.Checked = True
        Me._chkPrintOption_2.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkPrintOption_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkPrintOption_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkPrintOption_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPrintOption.SetIndex(Me._chkPrintOption_2, CType(2, Short))
        Me._chkPrintOption_2.Location = New System.Drawing.Point(24, 50)
        Me._chkPrintOption_2.Name = "_chkPrintOption_2"
        Me._chkPrintOption_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkPrintOption_2.Size = New System.Drawing.Size(151, 16)
        Me._chkPrintOption_2.TabIndex = 6
        Me._chkPrintOption_2.Text = "RGP Certificate"
        Me._chkPrintOption_2.UseVisualStyleBackColor = False
        '
        '_chkPrintOption_1
        '
        Me._chkPrintOption_1.BackColor = System.Drawing.SystemColors.Control
        Me._chkPrintOption_1.Checked = True
        Me._chkPrintOption_1.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkPrintOption_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkPrintOption_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkPrintOption_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPrintOption.SetIndex(Me._chkPrintOption_1, CType(1, Short))
        Me._chkPrintOption_1.Location = New System.Drawing.Point(24, 34)
        Me._chkPrintOption_1.Name = "_chkPrintOption_1"
        Me._chkPrintOption_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkPrintOption_1.Size = New System.Drawing.Size(151, 16)
        Me._chkPrintOption_1.TabIndex = 1
        Me._chkPrintOption_1.Text = "Outward F4"
        Me._chkPrintOption_1.UseVisualStyleBackColor = False
        '
        '_chkPrintOption_0
        '
        Me._chkPrintOption_0.BackColor = System.Drawing.SystemColors.Control
        Me._chkPrintOption_0.Checked = True
        Me._chkPrintOption_0.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkPrintOption_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkPrintOption_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkPrintOption_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPrintOption.SetIndex(Me._chkPrintOption_0, CType(0, Short))
        Me._chkPrintOption_0.Location = New System.Drawing.Point(24, 18)
        Me._chkPrintOption_0.Name = "_chkPrintOption_0"
        Me._chkPrintOption_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkPrintOption_0.Size = New System.Drawing.Size(141, 16)
        Me._chkPrintOption_0.TabIndex = 0
        Me._chkPrintOption_0.Text = "RGP / NRGP "
        Me._chkPrintOption_0.UseVisualStyleBackColor = False
        '
        'FraOk
        '
        Me.FraOk.BackColor = System.Drawing.SystemColors.Control
        Me.FraOk.Controls.Add(Me.cmdOk)
        Me.FraOk.Controls.Add(Me.cmdCancel)
        Me.FraOk.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraOk.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraOk.Location = New System.Drawing.Point(0, 102)
        Me.FraOk.Name = "FraOk"
        Me.FraOk.Padding = New System.Windows.Forms.Padding(0)
        Me.FraOk.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraOk.Size = New System.Drawing.Size(201, 47)
        Me.FraOk.TabIndex = 2
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
        Me.cmdOk.TabIndex = 4
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
        Me.cmdCancel.TabIndex = 3
        Me.cmdCancel.Text = "&Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'frmPrintRGP_F4
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(201, 149)
        Me.Controls.Add(Me.fraPrintOption)
        Me.Controls.Add(Me.FraOk)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(144, 135)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPrintRGP_F4"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Print"
        Me.fraPrintOption.ResumeLayout(False)
        Me.FraOk.ResumeLayout(False)
        CType(Me.chkPrintOption, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class