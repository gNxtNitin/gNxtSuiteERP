Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmPrintMRR
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
    Public WithEvents OptIdentification As System.Windows.Forms.RadioButton
    Public WithEvents optMRR As System.Windows.Forms.RadioButton
    Public WithEvents fraPrintOption As System.Windows.Forms.GroupBox
    Public WithEvents cmdOk As System.Windows.Forms.Button
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents FraOk As System.Windows.Forms.GroupBox
    Public WithEvents OptPrint As VB6.RadioButtonArray
    Public WithEvents optPrintType As VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrintMRR))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.fraPrintOption = New System.Windows.Forms.GroupBox()
        Me.OptIdentification = New System.Windows.Forms.RadioButton()
        Me.optMRR = New System.Windows.Forms.RadioButton()
        Me.FraOk = New System.Windows.Forms.GroupBox()
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.OptPrint = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optPrintType = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.fraPrintOption.SuspendLayout()
        Me.FraOk.SuspendLayout()
        CType(Me.OptPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optPrintType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'fraPrintOption
        '
        Me.fraPrintOption.BackColor = System.Drawing.SystemColors.Control
        Me.fraPrintOption.Controls.Add(Me.OptIdentification)
        Me.fraPrintOption.Controls.Add(Me.optMRR)
        Me.fraPrintOption.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraPrintOption.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraPrintOption.Location = New System.Drawing.Point(0, 12)
        Me.fraPrintOption.Name = "fraPrintOption"
        Me.fraPrintOption.Padding = New System.Windows.Forms.Padding(0)
        Me.fraPrintOption.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraPrintOption.Size = New System.Drawing.Size(200, 67)
        Me.fraPrintOption.TabIndex = 3
        Me.fraPrintOption.TabStop = False
        Me.fraPrintOption.Text = "Printing Option"
        '
        'OptIdentification
        '
        Me.OptIdentification.BackColor = System.Drawing.SystemColors.Control
        Me.OptIdentification.Cursor = System.Windows.Forms.Cursors.Default
        Me.OptIdentification.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptIdentification.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptPrint.SetIndex(Me.OptIdentification, CType(1, Short))
        Me.OptIdentification.Location = New System.Drawing.Point(18, 42)
        Me.OptIdentification.Name = "OptIdentification"
        Me.OptIdentification.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptIdentification.Size = New System.Drawing.Size(151, 17)
        Me.OptIdentification.TabIndex = 5
        Me.OptIdentification.TabStop = True
        Me.OptIdentification.Text = "Identification Tag"
        Me.OptIdentification.UseVisualStyleBackColor = False
        '
        'optMRR
        '
        Me.optMRR.BackColor = System.Drawing.SystemColors.Control
        Me.optMRR.Checked = True
        Me.optMRR.Cursor = System.Windows.Forms.Cursors.Default
        Me.optMRR.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optMRR.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptPrint.SetIndex(Me.optMRR, CType(0, Short))
        Me.optMRR.Location = New System.Drawing.Point(18, 22)
        Me.optMRR.Name = "optMRR"
        Me.optMRR.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optMRR.Size = New System.Drawing.Size(169, 17)
        Me.optMRR.TabIndex = 4
        Me.optMRR.TabStop = True
        Me.optMRR.Text = "MRR"
        Me.optMRR.UseVisualStyleBackColor = False
        '
        'FraOk
        '
        Me.FraOk.BackColor = System.Drawing.SystemColors.Control
        Me.FraOk.Controls.Add(Me.cmdOk)
        Me.FraOk.Controls.Add(Me.cmdCancel)
        Me.FraOk.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraOk.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraOk.Location = New System.Drawing.Point(0, 110)
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
        Me.cmdOk.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(116, 12)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(73, 27)
        Me.cmdCancel.TabIndex = 1
        Me.cmdCancel.Text = "&Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'frmPrintMRR
        '
        Me.AcceptButton = Me.cmdOk
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(201, 157)
        Me.Controls.Add(Me.fraPrintOption)
        Me.Controls.Add(Me.FraOk)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(144, 135)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPrintMRR"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Print"
        Me.fraPrintOption.ResumeLayout(False)
        Me.FraOk.ResumeLayout(False)
        CType(Me.OptPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optPrintType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class