Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmShowVarDed
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
    Public WithEvents txtDeductionName As System.Windows.Forms.TextBox
    Public WithEvents cmdDeductionSearch As System.Windows.Forms.Button
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents cmdOk As System.Windows.Forms.Button
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents lblFilePath As System.Windows.Forms.Label
    Public WithEvents FraOk As System.Windows.Forms.GroupBox
    Public CommonDialogOpen As System.Windows.Forms.OpenFileDialog
    Public CommonDialogSave As System.Windows.Forms.SaveFileDialog
    Public CommonDialogFont As System.Windows.Forms.FontDialog
    Public CommonDialogColor As System.Windows.Forms.ColorDialog
    Public CommonDialogPrint As System.Windows.Forms.PrintDialog
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmShowVarDed))
        Me.components = New System.ComponentModel.Container()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
        Me.Frame1 = New System.Windows.Forms.GroupBox
        Me.txtDeductionName = New System.Windows.Forms.TextBox
        Me.cmdDeductionSearch = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.FraOk = New System.Windows.Forms.GroupBox
        Me.cmdOk = New System.Windows.Forms.Button
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.lblFilePath = New System.Windows.Forms.Label
        Me.CommonDialogOpen = New System.Windows.Forms.OpenFileDialog
        Me.CommonDialogSave = New System.Windows.Forms.SaveFileDialog
        Me.CommonDialogFont = New System.Windows.Forms.FontDialog
        Me.CommonDialogColor = New System.Windows.Forms.ColorDialog
        Me.CommonDialogPrint = New System.Windows.Forms.PrintDialog
        Me.Frame1.SuspendLayout()
        Me.FraOk.SuspendLayout()
        Me.SuspendLayout()
        Me.ToolTip1.Active = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Text = "Populate from Excel"
        Me.ClientSize = New System.Drawing.Size(266, 105)
        Me.Location = New System.Drawing.Point(144, 135)
        Me.Icon = CType(resources.GetObject("frmShowVarDed.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.ShowInTaskbar = False
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ControlBox = True
        Me.Enabled = True
        Me.KeyPreview = False
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.HelpButton = False
        Me.WindowState = System.Windows.Forms.FormWindowState.Normal
        Me.Name = "frmShowVarDed"
        Me.Frame1.Text = "Printing Status"
        Me.Frame1.Size = New System.Drawing.Size(265, 67)
        Me.Frame1.Location = New System.Drawing.Point(0, 0)
        Me.Frame1.TabIndex = 3
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Enabled = True
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Visible = True
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.Name = "Frame1"
        Me.txtDeductionName.AutoSize = False
        Me.txtDeductionName.Size = New System.Drawing.Size(231, 19)
        Me.txtDeductionName.Location = New System.Drawing.Point(4, 42)
        Me.txtDeductionName.TabIndex = 5
        Me.txtDeductionName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeductionName.AcceptsReturn = True
        Me.txtDeductionName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtDeductionName.BackColor = System.Drawing.SystemColors.Window
        Me.txtDeductionName.CausesValidation = True
        Me.txtDeductionName.Enabled = True
        Me.txtDeductionName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDeductionName.HideSelection = True
        Me.txtDeductionName.ReadOnly = False
        Me.txtDeductionName.Maxlength = 0
        Me.txtDeductionName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDeductionName.MultiLine = False
        Me.txtDeductionName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDeductionName.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtDeductionName.TabStop = True
        Me.txtDeductionName.Visible = True
        Me.txtDeductionName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDeductionName.Name = "txtDeductionName"
        Me.cmdDeductionSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdDeductionSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdDeductionSearch.Size = New System.Drawing.Size(25, 19)
        Me.cmdDeductionSearch.Location = New System.Drawing.Point(236, 42)
        Me.cmdDeductionSearch.Image = CType(resources.GetObject("cmdDeductionSearch.Image"), System.Drawing.Image)
        Me.cmdDeductionSearch.TabIndex = 4
        Me.cmdDeductionSearch.TabStop = False
        Me.ToolTip1.SetToolTip(Me.cmdDeductionSearch, "Search")
        Me.cmdDeductionSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDeductionSearch.CausesValidation = True
        Me.cmdDeductionSearch.Enabled = True
        Me.cmdDeductionSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDeductionSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDeductionSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDeductionSearch.Name = "cmdDeductionSearch"
        Me.Label1.Text = "Deduction Field Name"
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Size = New System.Drawing.Size(126, 13)
        Me.Label1.Location = New System.Drawing.Point(4, 18)
        Me.Label1.TabIndex = 6
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Enabled = True
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.UseMnemonic = True
        Me.Label1.Visible = True
        Me.Label1.AutoSize = True
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Label1.Name = "Label1"
        Me.FraOk.Size = New System.Drawing.Size(267, 43)
        Me.FraOk.Location = New System.Drawing.Point(-2, 62)
        Me.FraOk.TabIndex = 0
        Me.FraOk.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraOk.BackColor = System.Drawing.SystemColors.Control
        Me.FraOk.Enabled = True
        Me.FraOk.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraOk.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraOk.Visible = True
        Me.FraOk.Padding = New System.Windows.Forms.Padding(0)
        Me.FraOk.Name = "FraOk"
        Me.cmdOk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdOk.Text = "&Ok"
        Me.AcceptButton = Me.cmdOk
        Me.cmdOk.Size = New System.Drawing.Size(73, 25)
        Me.cmdOk.Location = New System.Drawing.Point(8, 12)
        Me.cmdOk.TabIndex = 2
        Me.cmdOk.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOk.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOk.CausesValidation = True
        Me.cmdOk.Enabled = True
        Me.cmdOk.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOk.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOk.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOk.TabStop = True
        Me.cmdOk.Name = "cmdOk"
        Me.cmdCancel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.CancelButton = Me.cmdCancel
        Me.cmdCancel.Text = "&Cancel"
        Me.cmdCancel.Size = New System.Drawing.Size(73, 25)
        Me.cmdCancel.Location = New System.Drawing.Point(188, 12)
        Me.cmdCancel.TabIndex = 1
        Me.cmdCancel.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.CausesValidation = True
        Me.cmdCancel.Enabled = True
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.TabStop = True
        Me.cmdCancel.Name = "cmdCancel"
        Me.lblFilePath.Text = "lblFilePath"
        Me.lblFilePath.Size = New System.Drawing.Size(75, 11)
        Me.lblFilePath.Location = New System.Drawing.Point(96, 16)
        Me.lblFilePath.TabIndex = 7
        Me.lblFilePath.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFilePath.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblFilePath.BackColor = System.Drawing.SystemColors.Control
        Me.lblFilePath.Enabled = True
        Me.lblFilePath.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFilePath.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblFilePath.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblFilePath.UseMnemonic = True
        Me.lblFilePath.Visible = True
        Me.lblFilePath.AutoSize = False
        Me.lblFilePath.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblFilePath.Name = "lblFilePath"
        Me.Controls.Add(Frame1)
        Me.Controls.Add(FraOk)
        Me.Frame1.Controls.Add(txtDeductionName)
        Me.Frame1.Controls.Add(cmdDeductionSearch)
        Me.Frame1.Controls.Add(Label1)
        Me.FraOk.Controls.Add(cmdOk)
        Me.FraOk.Controls.Add(cmdCancel)
        Me.FraOk.Controls.Add(lblFilePath)
        Me.Frame1.ResumeLayout(False)
        Me.FraOk.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub
#End Region
End Class