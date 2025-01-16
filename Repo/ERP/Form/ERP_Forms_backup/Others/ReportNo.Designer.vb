Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmReportNo
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
        'Me.MDIParent = Production.Master

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
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents FraDetail As System.Windows.Forms.GroupBox
    Public WithEvents cboModuleID As System.Windows.Forms.ComboBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents LblModuleId As System.Windows.Forms.Label
    Public WithEvents label1 As System.Windows.Forms.Label
    Public WithEvents FraMain As System.Windows.Forms.GroupBox
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReportNo))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.FraDetail = New System.Windows.Forms.GroupBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.FraMain = New System.Windows.Forms.GroupBox()
        Me.cboModuleID = New System.Windows.Forms.ComboBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.LblModuleId = New System.Windows.Forms.Label()
        Me.label1 = New System.Windows.Forms.Label()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.FraDetail.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMain.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(268, 8)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(88, 34)
        Me.cmdPrint.TabIndex = 5
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print Record")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(357, 8)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(93, 34)
        Me.CmdClose.TabIndex = 7
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close the Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.Location = New System.Drawing.Point(174, 8)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(93, 34)
        Me.CmdSave.TabIndex = 6
        Me.CmdSave.Text = "&Save"
        Me.CmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSave, "Save Record")
        Me.CmdSave.UseVisualStyleBackColor = False
        '
        'FraDetail
        '
        Me.FraDetail.BackColor = System.Drawing.SystemColors.Control
        Me.FraDetail.Controls.Add(Me.SprdMain)
        Me.FraDetail.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraDetail.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraDetail.Location = New System.Drawing.Point(0, 38)
        Me.FraDetail.Name = "FraDetail"
        Me.FraDetail.Padding = New System.Windows.Forms.Padding(0)
        Me.FraDetail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraDetail.Size = New System.Drawing.Size(728, 391)
        Me.FraDetail.TabIndex = 2
        Me.FraDetail.TabStop = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SprdMain.Location = New System.Drawing.Point(0, 13)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(728, 378)
        Me.SprdMain.TabIndex = 3
        '
        'FraMain
        '
        Me.FraMain.BackColor = System.Drawing.SystemColors.Control
        Me.FraMain.Controls.Add(Me.cboModuleID)
        Me.FraMain.Controls.Add(Me.Report1)
        Me.FraMain.Controls.Add(Me.LblModuleId)
        Me.FraMain.Controls.Add(Me.label1)
        Me.FraMain.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMain.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMain.Location = New System.Drawing.Point(0, 0)
        Me.FraMain.Name = "FraMain"
        Me.FraMain.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMain.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMain.Size = New System.Drawing.Size(521, 37)
        Me.FraMain.TabIndex = 0
        Me.FraMain.TabStop = False
        '
        'cboModuleID
        '
        Me.cboModuleID.BackColor = System.Drawing.SystemColors.Window
        Me.cboModuleID.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboModuleID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboModuleID.Enabled = False
        Me.cboModuleID.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboModuleID.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboModuleID.Location = New System.Drawing.Point(102, 10)
        Me.cboModuleID.Name = "cboModuleID"
        Me.cboModuleID.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboModuleID.Size = New System.Drawing.Size(195, 22)
        Me.cboModuleID.TabIndex = 8
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(12, 68)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 9
        '
        'LblModuleId
        '
        Me.LblModuleId.BackColor = System.Drawing.SystemColors.Control
        Me.LblModuleId.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblModuleId.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblModuleId.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblModuleId.Location = New System.Drawing.Point(338, 12)
        Me.LblModuleId.Name = "LblModuleId"
        Me.LblModuleId.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblModuleId.Size = New System.Drawing.Size(103, 17)
        Me.LblModuleId.TabIndex = 9
        Me.LblModuleId.Text = "LblModuleId"
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.BackColor = System.Drawing.SystemColors.Control
        Me.label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.label1.Enabled = False
        Me.label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label1.Location = New System.Drawing.Point(30, 18)
        Me.label1.Name = "label1"
        Me.label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.label1.Size = New System.Drawing.Size(50, 14)
        Me.label1.TabIndex = 1
        Me.label1.Text = "Module : "
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.CmdSave)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 427)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(728, 43)
        Me.FraMovement.TabIndex = 4
        Me.FraMovement.TabStop = False
        '
        'frmReportNo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(729, 471)
        Me.Controls.Add(Me.FraDetail)
        Me.Controls.Add(Me.FraMain)
        Me.Controls.Add(Me.FraMovement)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmReportNo"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Reports No."
        Me.FraDetail.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMain.ResumeLayout(False)
        Me.FraMain.PerformLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class