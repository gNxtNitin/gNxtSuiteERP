Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmPrintInvoice
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
    Public WithEvents txtF4No As System.Windows.Forms.TextBox
    Public WithEvents _optSCOption_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optSCOption_0 As System.Windows.Forms.RadioButton
    Public WithEvents FraF4 As System.Windows.Forms.GroupBox
    Public WithEvents OptInvoiceAnnex As System.Windows.Forms.RadioButton
    Public WithEvents OptInvoice As System.Windows.Forms.RadioButton
    Public WithEvents optSubsidiaryChallan As System.Windows.Forms.RadioButton
    Public WithEvents fraPrintOption As System.Windows.Forms.GroupBox
    Public WithEvents cmdOk As System.Windows.Forms.Button
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents FraOk As System.Windows.Forms.GroupBox
    Public WithEvents optSCOption As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrintInvoice))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.fraPrintOption = New System.Windows.Forms.GroupBox()
        Me.FraF4 = New System.Windows.Forms.GroupBox()
        Me.txtF4No = New System.Windows.Forms.TextBox()
        Me._optSCOption_1 = New System.Windows.Forms.RadioButton()
        Me._optSCOption_0 = New System.Windows.Forms.RadioButton()
        Me.OptInvoiceAnnex = New System.Windows.Forms.RadioButton()
        Me.OptInvoice = New System.Windows.Forms.RadioButton()
        Me.optSubsidiaryChallan = New System.Windows.Forms.RadioButton()
        Me.FraOk = New System.Windows.Forms.GroupBox()
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.optSCOption = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.Opt4 = New System.Windows.Forms.RadioButton()
        Me.fraPrintOption.SuspendLayout()
        Me.FraF4.SuspendLayout()
        Me.FraOk.SuspendLayout()
        CType(Me.optSCOption, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'fraPrintOption
        '
        Me.fraPrintOption.BackColor = System.Drawing.SystemColors.Control
        Me.fraPrintOption.Controls.Add(Me.Opt4)
        Me.fraPrintOption.Controls.Add(Me.FraF4)
        Me.fraPrintOption.Controls.Add(Me.OptInvoiceAnnex)
        Me.fraPrintOption.Controls.Add(Me.OptInvoice)
        Me.fraPrintOption.Controls.Add(Me.optSubsidiaryChallan)
        Me.fraPrintOption.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraPrintOption.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraPrintOption.Location = New System.Drawing.Point(0, 0)
        Me.fraPrintOption.Name = "fraPrintOption"
        Me.fraPrintOption.Padding = New System.Windows.Forms.Padding(0)
        Me.fraPrintOption.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraPrintOption.Size = New System.Drawing.Size(201, 172)
        Me.fraPrintOption.TabIndex = 3
        Me.fraPrintOption.TabStop = False
        Me.fraPrintOption.Text = "Printing Option"
        '
        'FraF4
        '
        Me.FraF4.BackColor = System.Drawing.SystemColors.Control
        Me.FraF4.Controls.Add(Me.txtF4No)
        Me.FraF4.Controls.Add(Me._optSCOption_1)
        Me.FraF4.Controls.Add(Me._optSCOption_0)
        Me.FraF4.Enabled = False
        Me.FraF4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraF4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraF4.Location = New System.Drawing.Point(14, 115)
        Me.FraF4.Name = "FraF4"
        Me.FraF4.Padding = New System.Windows.Forms.Padding(0)
        Me.FraF4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraF4.Size = New System.Drawing.Size(175, 57)
        Me.FraF4.TabIndex = 7
        Me.FraF4.TabStop = False
        '
        'txtF4No
        '
        Me.txtF4No.AcceptsReturn = True
        Me.txtF4No.BackColor = System.Drawing.SystemColors.Window
        Me.txtF4No.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtF4No.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtF4No.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtF4No.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtF4No.Location = New System.Drawing.Point(6, 30)
        Me.txtF4No.MaxLength = 0
        Me.txtF4No.Name = "txtF4No"
        Me.txtF4No.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtF4No.Size = New System.Drawing.Size(151, 20)
        Me.txtF4No.TabIndex = 10
        '
        '_optSCOption_1
        '
        Me._optSCOption_1.AutoSize = True
        Me._optSCOption_1.BackColor = System.Drawing.SystemColors.Control
        Me._optSCOption_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optSCOption_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optSCOption_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optSCOption.SetIndex(Me._optSCOption_1, CType(1, Short))
        Me._optSCOption_1.Location = New System.Drawing.Point(64, 12)
        Me._optSCOption_1.Name = "_optSCOption_1"
        Me._optSCOption_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optSCOption_1.Size = New System.Drawing.Size(85, 18)
        Me._optSCOption_1.TabIndex = 9
        Me._optSCOption_1.TabStop = True
        Me._optSCOption_1.Text = "Particular F4"
        Me._optSCOption_1.UseVisualStyleBackColor = False
        '
        '_optSCOption_0
        '
        Me._optSCOption_0.AutoSize = True
        Me._optSCOption_0.BackColor = System.Drawing.SystemColors.Control
        Me._optSCOption_0.Checked = True
        Me._optSCOption_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optSCOption_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optSCOption_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optSCOption.SetIndex(Me._optSCOption_0, CType(0, Short))
        Me._optSCOption_0.Location = New System.Drawing.Point(4, 12)
        Me._optSCOption_0.Name = "_optSCOption_0"
        Me._optSCOption_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optSCOption_0.Size = New System.Drawing.Size(37, 18)
        Me._optSCOption_0.TabIndex = 8
        Me._optSCOption_0.TabStop = True
        Me._optSCOption_0.Text = "All"
        Me._optSCOption_0.UseVisualStyleBackColor = False
        '
        'OptInvoiceAnnex
        '
        Me.OptInvoiceAnnex.AutoSize = True
        Me.OptInvoiceAnnex.BackColor = System.Drawing.SystemColors.Control
        Me.OptInvoiceAnnex.Cursor = System.Windows.Forms.Cursors.Default
        Me.OptInvoiceAnnex.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptInvoiceAnnex.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptInvoiceAnnex.Location = New System.Drawing.Point(31, 43)
        Me.OptInvoiceAnnex.Name = "OptInvoiceAnnex"
        Me.OptInvoiceAnnex.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptInvoiceAnnex.Size = New System.Drawing.Size(73, 18)
        Me.OptInvoiceAnnex.TabIndex = 5
        Me.OptInvoiceAnnex.TabStop = True
        Me.OptInvoiceAnnex.Text = "Annexure"
        Me.OptInvoiceAnnex.UseVisualStyleBackColor = False
        '
        'OptInvoice
        '
        Me.OptInvoice.AutoSize = True
        Me.OptInvoice.BackColor = System.Drawing.SystemColors.Control
        Me.OptInvoice.Checked = True
        Me.OptInvoice.Cursor = System.Windows.Forms.Cursors.Default
        Me.OptInvoice.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptInvoice.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptInvoice.Location = New System.Drawing.Point(31, 19)
        Me.OptInvoice.Name = "OptInvoice"
        Me.OptInvoice.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptInvoice.Size = New System.Drawing.Size(59, 18)
        Me.OptInvoice.TabIndex = 4
        Me.OptInvoice.TabStop = True
        Me.OptInvoice.Text = "Invoice"
        Me.OptInvoice.UseVisualStyleBackColor = False
        '
        'optSubsidiaryChallan
        '
        Me.optSubsidiaryChallan.AutoSize = True
        Me.optSubsidiaryChallan.BackColor = System.Drawing.SystemColors.Control
        Me.optSubsidiaryChallan.Cursor = System.Windows.Forms.Cursors.Default
        Me.optSubsidiaryChallan.Enabled = False
        Me.optSubsidiaryChallan.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optSubsidiaryChallan.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optSubsidiaryChallan.Location = New System.Drawing.Point(31, 67)
        Me.optSubsidiaryChallan.Name = "optSubsidiaryChallan"
        Me.optSubsidiaryChallan.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optSubsidiaryChallan.Size = New System.Drawing.Size(114, 18)
        Me.optSubsidiaryChallan.TabIndex = 6
        Me.optSubsidiaryChallan.TabStop = True
        Me.optSubsidiaryChallan.Text = "Subsidiary Challan"
        Me.optSubsidiaryChallan.UseVisualStyleBackColor = False
        Me.optSubsidiaryChallan.Visible = False
        '
        'FraOk
        '
        Me.FraOk.BackColor = System.Drawing.SystemColors.Control
        Me.FraOk.Controls.Add(Me.cmdOk)
        Me.FraOk.Controls.Add(Me.cmdCancel)
        Me.FraOk.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraOk.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraOk.Location = New System.Drawing.Point(0, 170)
        Me.FraOk.Name = "FraOk"
        Me.FraOk.Padding = New System.Windows.Forms.Padding(0)
        Me.FraOk.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraOk.Size = New System.Drawing.Size(201, 47)
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
        Me.cmdOk.Size = New System.Drawing.Size(73, 27)
        Me.cmdOk.TabIndex = 2
        Me.cmdOk.Text = "&Ok"
        Me.cmdOk.UseVisualStyleBackColor = False
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(116, 12)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(73, 27)
        Me.cmdCancel.TabIndex = 1
        Me.cmdCancel.Text = "&Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'optSCOption
        '
        '
        'Opt4
        '
        Me.Opt4.AutoSize = True
        Me.Opt4.BackColor = System.Drawing.SystemColors.Control
        Me.Opt4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Opt4.Enabled = False
        Me.Opt4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Opt4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Opt4.Location = New System.Drawing.Point(31, 91)
        Me.Opt4.Name = "Opt4"
        Me.Opt4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Opt4.Size = New System.Drawing.Size(48, 18)
        Me.Opt4.TabIndex = 8
        Me.Opt4.TabStop = True
        Me.Opt4.Text = "Opt4"
        Me.Opt4.UseVisualStyleBackColor = False
        Me.Opt4.Visible = False
        '
        'frmPrintInvoice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(203, 219)
        Me.Controls.Add(Me.fraPrintOption)
        Me.Controls.Add(Me.FraOk)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(144, 135)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPrintInvoice"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Print"
        Me.fraPrintOption.ResumeLayout(False)
        Me.fraPrintOption.PerformLayout()
        Me.FraF4.ResumeLayout(False)
        Me.FraF4.PerformLayout()
        Me.FraOk.ResumeLayout(False)
        CType(Me.optSCOption, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Public WithEvents Opt4 As RadioButton
#End Region
End Class