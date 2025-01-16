Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmLeave
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
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents lblDate As System.Windows.Forms.Label
    Public WithEvents lblYear As System.Windows.Forms.Label
    Public WithEvents lblMonth As System.Windows.Forms.Label
    Public WithEvents lblCode As System.Windows.Forms.Label
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents sprdLeave2 As AxFPSpreadADO.AxfpSpread
    Public WithEvents SprdLeave1 As AxFPSpreadADO.AxfpSpread
    'Public WithEvents Line1 As Microsoft.VisualBasic.PowerPacks.LineShape
    Public WithEvents lblvwMonth As System.Windows.Forms.Label
    Public WithEvents lblEmpName As System.Windows.Forms.Label
    Public WithEvents fraMain As System.Windows.Forms.GroupBox
    'Public WithEvents ShapeContainer1 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLeave))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.lblDate = New System.Windows.Forms.Label()
        Me.lblYear = New System.Windows.Forms.Label()
        Me.lblMonth = New System.Windows.Forms.Label()
        Me.lblCode = New System.Windows.Forms.Label()
        Me.fraMain = New System.Windows.Forms.GroupBox()
        Me.sprdLeave2 = New AxFPSpreadADO.AxfpSpread()
        Me.SprdLeave1 = New AxFPSpreadADO.AxfpSpread()
        Me.lblvwMonth = New System.Windows.Forms.Label()
        Me.lblEmpName = New System.Windows.Forms.Label()
        Me.FraMovement.SuspendLayout()
        Me.fraMain.SuspendLayout()
        CType(Me.sprdLeave2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdLeave1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(130, 12)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(63, 34)
        Me.CmdClose.TabIndex = 2
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.lblDate)
        Me.FraMovement.Controls.Add(Me.lblYear)
        Me.FraMovement.Controls.Add(Me.lblMonth)
        Me.FraMovement.Controls.Add(Me.lblCode)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 260)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(355, 51)
        Me.FraMovement.TabIndex = 1
        Me.FraMovement.TabStop = False
        '
        'lblDate
        '
        Me.lblDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDate.Location = New System.Drawing.Point(214, 20)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDate.Size = New System.Drawing.Size(51, 11)
        Me.lblDate.TabIndex = 10
        Me.lblDate.Text = "lblDate"
        '
        'lblYear
        '
        Me.lblYear.AutoSize = True
        Me.lblYear.BackColor = System.Drawing.SystemColors.Control
        Me.lblYear.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblYear.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblYear.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblYear.Location = New System.Drawing.Point(80, 16)
        Me.lblYear.Name = "lblYear"
        Me.lblYear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblYear.Size = New System.Drawing.Size(30, 14)
        Me.lblYear.TabIndex = 9
        Me.lblYear.Text = "Year"
        Me.lblYear.Visible = False
        '
        'lblMonth
        '
        Me.lblMonth.AutoSize = True
        Me.lblMonth.BackColor = System.Drawing.SystemColors.Control
        Me.lblMonth.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMonth.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMonth.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMonth.Location = New System.Drawing.Point(22, 34)
        Me.lblMonth.Name = "lblMonth"
        Me.lblMonth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMonth.Size = New System.Drawing.Size(36, 14)
        Me.lblMonth.TabIndex = 8
        Me.lblMonth.Text = "Month"
        Me.lblMonth.Visible = False
        '
        'lblCode
        '
        Me.lblCode.AutoSize = True
        Me.lblCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCode.Location = New System.Drawing.Point(10, 14)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCode.Size = New System.Drawing.Size(32, 14)
        Me.lblCode.TabIndex = 7
        Me.lblCode.Text = "Code"
        Me.lblCode.Visible = False
        '
        'fraMain
        '
        Me.fraMain.BackColor = System.Drawing.SystemColors.Control
        Me.fraMain.Controls.Add(Me.sprdLeave2)
        Me.fraMain.Controls.Add(Me.SprdLeave1)
        Me.fraMain.Controls.Add(Me.lblvwMonth)
        Me.fraMain.Controls.Add(Me.lblEmpName)
        Me.fraMain.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraMain.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraMain.Location = New System.Drawing.Point(0, -4)
        Me.fraMain.Name = "fraMain"
        Me.fraMain.Padding = New System.Windows.Forms.Padding(0)
        Me.fraMain.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraMain.Size = New System.Drawing.Size(355, 263)
        Me.fraMain.TabIndex = 0
        Me.fraMain.TabStop = False
        '
        'sprdLeave2
        '
        Me.sprdLeave2.DataSource = Nothing
        Me.sprdLeave2.Location = New System.Drawing.Point(2, 154)
        Me.sprdLeave2.Name = "sprdLeave2"
        Me.sprdLeave2.OcxState = CType(resources.GetObject("sprdLeave2.OcxState"), System.Windows.Forms.AxHost.State)
        Me.sprdLeave2.Size = New System.Drawing.Size(351, 107)
        Me.sprdLeave2.TabIndex = 6
        '
        'SprdLeave1
        '
        Me.SprdLeave1.DataSource = Nothing
        Me.SprdLeave1.Location = New System.Drawing.Point(2, 66)
        Me.SprdLeave1.Name = "SprdLeave1"
        Me.SprdLeave1.OcxState = CType(resources.GetObject("SprdLeave1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdLeave1.Size = New System.Drawing.Size(351, 85)
        Me.SprdLeave1.TabIndex = 5
        '
        'lblvwMonth
        '
        Me.lblvwMonth.AutoSize = True
        Me.lblvwMonth.BackColor = System.Drawing.SystemColors.Control
        Me.lblvwMonth.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblvwMonth.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblvwMonth.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblvwMonth.Location = New System.Drawing.Point(146, 34)
        Me.lblvwMonth.Name = "lblvwMonth"
        Me.lblvwMonth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblvwMonth.Size = New System.Drawing.Size(44, 16)
        Me.lblvwMonth.TabIndex = 4
        Me.lblvwMonth.Text = "Month"
        Me.lblvwMonth.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblEmpName
        '
        Me.lblEmpName.AutoSize = True
        Me.lblEmpName.BackColor = System.Drawing.SystemColors.Control
        Me.lblEmpName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblEmpName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmpName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblEmpName.Location = New System.Drawing.Point(146, 14)
        Me.lblEmpName.Name = "lblEmpName"
        Me.lblEmpName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblEmpName.Size = New System.Drawing.Size(42, 16)
        Me.lblEmpName.TabIndex = 3
        Me.lblEmpName.Text = "Name"
        Me.lblEmpName.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'frmLeave
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(356, 312)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.fraMain)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(21, 91)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLeave"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Leaves"
        Me.FraMovement.ResumeLayout(False)
        Me.FraMovement.PerformLayout()
        Me.fraMain.ResumeLayout(False)
        Me.fraMain.PerformLayout()
        CType(Me.sprdLeave2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdLeave1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class