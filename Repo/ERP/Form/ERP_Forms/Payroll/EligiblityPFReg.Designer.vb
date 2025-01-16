Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmEligiblityPFReg
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
    Public WithEvents txtCode As System.Windows.Forms.TextBox
    Public WithEvents txtName As System.Windows.Forms.TextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents optCardNo As System.Windows.Forms.RadioButton
    Public WithEvents OptName As System.Windows.Forms.RadioButton
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents sprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdRefresh As System.Windows.Forms.Button
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmEligiblityPFReg))
        Me.components = New System.ComponentModel.Container()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
        Me.Frame2 = New System.Windows.Forms.GroupBox
        Me.txtCode = New System.Windows.Forms.TextBox
        Me.txtName = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Frame3 = New System.Windows.Forms.GroupBox
        Me.optCardNo = New System.Windows.Forms.RadioButton
        Me.OptName = New System.Windows.Forms.RadioButton
        Me.Frame1 = New System.Windows.Forms.GroupBox
        Me.sprdView = New AxFPSpreadADO.AxfpSpread
        Me.FraMovement = New System.Windows.Forms.GroupBox
        Me.cmdPrint = New System.Windows.Forms.Button
        Me.CmdPreview = New System.Windows.Forms.Button
        Me.cmdRefresh = New System.Windows.Forms.Button
        Me.CmdClose = New System.Windows.Forms.Button
        Me.Report1 = New AxCrystal.AxCrystalReport
        Me.Frame2.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.FraMovement.SuspendLayout()
        Me.SuspendLayout()
        Me.ToolTip1.Active = True
        CType(Me.sprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Text = "Eligiblity Register of Employees' for Provident Fund"
        Me.ClientSize = New System.Drawing.Size(749, 456)
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Icon = CType(resources.GetObject("frmEligiblityPFReg.Icon"), System.Drawing.Icon)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable
        Me.ControlBox = True
        Me.Enabled = True
        Me.KeyPreview = False
        Me.MaximizeBox = True
        Me.MinimizeBox = True
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = True
        Me.HelpButton = False
        Me.WindowState = System.Windows.Forms.FormWindowState.Normal
        Me.Name = "frmEligiblityPFReg"
        Me.Frame2.Text = "Establishment"
        Me.Frame2.Size = New System.Drawing.Size(445, 59)
        Me.Frame2.Location = New System.Drawing.Point(304, 0)
        Me.Frame2.TabIndex = 10
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Enabled = True
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Visible = True
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.Name = "Frame2"
        Me.txtCode.AutoSize = False
        Me.txtCode.Enabled = False
        Me.txtCode.Size = New System.Drawing.Size(361, 19)
        Me.txtCode.Location = New System.Drawing.Point(80, 36)
        Me.txtCode.TabIndex = 14
        Me.txtCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCode.AcceptsReturn = True
        Me.txtCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtCode.CausesValidation = True
        Me.txtCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCode.HideSelection = True
        Me.txtCode.ReadOnly = False
        Me.txtCode.Maxlength = 0
        Me.txtCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCode.MultiLine = False
        Me.txtCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCode.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCode.TabStop = True
        Me.txtCode.Visible = True
        Me.txtCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCode.Name = "txtCode"
        Me.txtName.AutoSize = False
        Me.txtName.Enabled = False
        Me.txtName.Size = New System.Drawing.Size(361, 19)
        Me.txtName.Location = New System.Drawing.Point(80, 14)
        Me.txtName.TabIndex = 13
        Me.txtName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtName.AcceptsReturn = True
        Me.txtName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtName.BackColor = System.Drawing.SystemColors.Window
        Me.txtName.CausesValidation = True
        Me.txtName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtName.HideSelection = True
        Me.txtName.ReadOnly = False
        Me.txtName.Maxlength = 0
        Me.txtName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtName.MultiLine = False
        Me.txtName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtName.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtName.TabStop = True
        Me.txtName.Visible = True
        Me.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtName.Name = "txtName"
        Me.Label2.Text = "Code No. :"
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Size = New System.Drawing.Size(62, 13)
        Me.Label2.Location = New System.Drawing.Point(8, 38)
        Me.Label2.TabIndex = 12
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Enabled = True
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.UseMnemonic = True
        Me.Label2.Visible = True
        Me.Label2.AutoSize = True
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Label2.Name = "Label2"
        Me.Label1.Text = "Name :"
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Size = New System.Drawing.Size(41, 13)
        Me.Label1.Location = New System.Drawing.Point(8, 16)
        Me.Label1.TabIndex = 11
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
        Me.Frame3.Text = "Order By"
        Me.Frame3.Size = New System.Drawing.Size(93, 59)
        Me.Frame3.Location = New System.Drawing.Point(0, 0)
        Me.Frame3.TabIndex = 5
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Enabled = True
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Visible = True
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.Name = "Frame3"
        Me.optCardNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.optCardNo.Text = "Card No"
        Me.optCardNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCardNo.Size = New System.Drawing.Size(69, 20)
        Me.optCardNo.Location = New System.Drawing.Point(6, 36)
        Me.optCardNo.TabIndex = 7
        Me.optCardNo.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.optCardNo.BackColor = System.Drawing.SystemColors.Control
        Me.optCardNo.CausesValidation = True
        Me.optCardNo.Enabled = True
        Me.optCardNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCardNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.optCardNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optCardNo.Appearance = System.Windows.Forms.Appearance.Normal
        Me.optCardNo.TabStop = True
        Me.optCardNo.Checked = False
        Me.optCardNo.Visible = True
        Me.optCardNo.Name = "optCardNo"
        Me.OptName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.OptName.Text = "Name"
        Me.OptName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptName.Size = New System.Drawing.Size(73, 20)
        Me.OptName.Location = New System.Drawing.Point(6, 16)
        Me.OptName.TabIndex = 6
        Me.OptName.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.OptName.BackColor = System.Drawing.SystemColors.Control
        Me.OptName.CausesValidation = True
        Me.OptName.Enabled = True
        Me.OptName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptName.Cursor = System.Windows.Forms.Cursors.Default
        Me.OptName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptName.Appearance = System.Windows.Forms.Appearance.Normal
        Me.OptName.TabStop = True
        Me.OptName.Checked = False
        Me.OptName.Visible = True
        Me.OptName.Name = "OptName"
        Me.Frame1.Size = New System.Drawing.Size(749, 355)
        Me.Frame1.Location = New System.Drawing.Point(0, 54)
        Me.Frame1.TabIndex = 0
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Enabled = True
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Visible = True
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.Name = "Frame1"
        sprdView.OcxState = CType(resources.GetObject("sprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.sprdView.Size = New System.Drawing.Size(743, 343)
        Me.sprdView.Location = New System.Drawing.Point(2, 8)
        Me.sprdView.TabIndex = 1
        Me.sprdView.Name = "sprdView"
        Me.FraMovement.Size = New System.Drawing.Size(749, 51)
        Me.FraMovement.Location = New System.Drawing.Point(0, 404)
        Me.FraMovement.TabIndex = 2
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Enabled = True
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Visible = True
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.Name = "FraMovement"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.Size = New System.Drawing.Size(80, 34)
        Me.cmdPrint.Location = New System.Drawing.Point(4, 12)
        Me.cmdPrint.TabIndex = 9
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.CausesValidation = True
        Me.cmdPrint.Enabled = True
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.TabStop = True
        Me.cmdPrint.Name = "cmdPrint"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Size = New System.Drawing.Size(79, 34)
        Me.CmdPreview.Location = New System.Drawing.Point(84, 12)
        Me.CmdPreview.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Print PO")
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.CausesValidation = True
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.TabStop = True
        Me.CmdPreview.Name = "CmdPreview"
        Me.cmdRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdRefresh.Text = "&Refresh"
        Me.cmdRefresh.Size = New System.Drawing.Size(80, 34)
        Me.cmdRefresh.Location = New System.Drawing.Point(584, 12)
        Me.cmdRefresh.TabIndex = 4
        Me.cmdRefresh.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdRefresh.BackColor = System.Drawing.SystemColors.Control
        Me.cmdRefresh.CausesValidation = True
        Me.cmdRefresh.Enabled = True
        Me.cmdRefresh.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdRefresh.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdRefresh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdRefresh.TabStop = True
        Me.cmdRefresh.Name = "cmdRefresh"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.Size = New System.Drawing.Size(80, 34)
        Me.CmdClose.Location = New System.Drawing.Point(664, 12)
        Me.CmdClose.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close Form")
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.CausesValidation = True
        Me.CmdClose.Enabled = True
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.TabStop = True
        Me.CmdClose.Name = "CmdClose"
        Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Location = New System.Drawing.Point(188, 14)
        Me.Report1.Name = "Report1"
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Controls.Add(Frame2)
        Me.Controls.Add(Frame3)
        Me.Controls.Add(Frame1)
        Me.Controls.Add(FraMovement)
        Me.Frame2.Controls.Add(txtCode)
        Me.Frame2.Controls.Add(txtName)
        Me.Frame2.Controls.Add(Label2)
        Me.Frame2.Controls.Add(Label1)
        Me.Frame3.Controls.Add(optCardNo)
        Me.Frame3.Controls.Add(OptName)
        Me.Frame1.Controls.Add(sprdView)
        Me.FraMovement.Controls.Add(cmdPrint)
        Me.FraMovement.Controls.Add(CmdPreview)
        Me.FraMovement.Controls.Add(cmdRefresh)
        Me.FraMovement.Controls.Add(CmdClose)
        Me.FraMovement.Controls.Add(Report1)
        Me.Frame2.ResumeLayout(False)
        Me.Frame3.ResumeLayout(False)
        Me.Frame1.ResumeLayout(False)
        Me.FraMovement.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub
#End Region
End Class