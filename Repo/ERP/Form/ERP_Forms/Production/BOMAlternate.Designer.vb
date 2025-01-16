Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmBOMAlternate
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
    Public WithEvents SprdBOM As AxFPSpreadADO.AxfpSpread
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents cmdOk As System.Windows.Forms.Button
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents LblModifyMode As System.Windows.Forms.Label
    Public WithEvents LblAddMode As System.Windows.Forms.Label
    Public WithEvents LblMainItemSNO As System.Windows.Forms.Label
    Public WithEvents lblMainItemCode As System.Windows.Forms.Label
    Public WithEvents lblDeptCode As System.Windows.Forms.Label
    Public WithEvents lblMKey As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmBOMAlternate))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.SprdBOM = New AxFPSpreadADO.AxfpSpread()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.LblModifyMode = New System.Windows.Forms.Label()
        Me.LblAddMode = New System.Windows.Forms.Label()
        Me.LblMainItemSNO = New System.Windows.Forms.Label()
        Me.lblMainItemCode = New System.Windows.Forms.Label()
        Me.lblDeptCode = New System.Windows.Forms.Label()
        Me.lblMKey = New System.Windows.Forms.Label()
        Me.Frame2.SuspendLayout()
        CType(Me.SprdBOM, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdOk
        '
        Me.cmdOk.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOk.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOk.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOk.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOk.Image = CType(resources.GetObject("cmdOk.Image"), System.Drawing.Image)
        Me.cmdOk.Location = New System.Drawing.Point(4, 12)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOk.Size = New System.Drawing.Size(60, 34)
        Me.cmdOk.TabIndex = 3
        Me.cmdOk.Text = "&Save"
        Me.cmdOk.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdOk, "Save the Form")
        Me.cmdOk.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(462, 12)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(60, 34)
        Me.CmdClose.TabIndex = 2
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close the Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.SprdBOM)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, -2)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(577, 327)
        Me.Frame2.TabIndex = 0
        Me.Frame2.TabStop = False
        '
        'SprdBOM
        '
        Me.SprdBOM.DataSource = Nothing
        Me.SprdBOM.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SprdBOM.Location = New System.Drawing.Point(0, 13)
        Me.SprdBOM.Name = "SprdBOM"
        Me.SprdBOM.OcxState = CType(resources.GetObject("SprdBOM.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdBOM.Size = New System.Drawing.Size(577, 314)
        Me.SprdBOM.TabIndex = 4
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.cmdOk)
        Me.Frame3.Controls.Add(Me.CmdClose)
        Me.Frame3.Controls.Add(Me.LblModifyMode)
        Me.Frame3.Controls.Add(Me.LblAddMode)
        Me.Frame3.Controls.Add(Me.LblMainItemSNO)
        Me.Frame3.Controls.Add(Me.lblMainItemCode)
        Me.Frame3.Controls.Add(Me.lblDeptCode)
        Me.Frame3.Controls.Add(Me.lblMKey)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 320)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(527, 49)
        Me.Frame3.TabIndex = 1
        Me.Frame3.TabStop = False
        '
        'LblModifyMode
        '
        Me.LblModifyMode.AutoSize = True
        Me.LblModifyMode.BackColor = System.Drawing.SystemColors.Control
        Me.LblModifyMode.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblModifyMode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblModifyMode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblModifyMode.Location = New System.Drawing.Point(278, 28)
        Me.LblModifyMode.Name = "LblModifyMode"
        Me.LblModifyMode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblModifyMode.Size = New System.Drawing.Size(79, 14)
        Me.LblModifyMode.TabIndex = 10
        Me.LblModifyMode.Text = "LblModifyMode"
        Me.LblModifyMode.Visible = False
        '
        'LblAddMode
        '
        Me.LblAddMode.AutoSize = True
        Me.LblAddMode.BackColor = System.Drawing.SystemColors.Control
        Me.LblAddMode.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblAddMode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAddMode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblAddMode.Location = New System.Drawing.Point(420, 14)
        Me.LblAddMode.Name = "LblAddMode"
        Me.LblAddMode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblAddMode.Size = New System.Drawing.Size(67, 14)
        Me.LblAddMode.TabIndex = 9
        Me.LblAddMode.Text = "LblAddMode"
        Me.LblAddMode.Visible = False
        '
        'LblMainItemSNO
        '
        Me.LblMainItemSNO.AutoSize = True
        Me.LblMainItemSNO.BackColor = System.Drawing.SystemColors.Control
        Me.LblMainItemSNO.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblMainItemSNO.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMainItemSNO.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblMainItemSNO.Location = New System.Drawing.Point(314, 14)
        Me.LblMainItemSNO.Name = "LblMainItemSNO"
        Me.LblMainItemSNO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblMainItemSNO.Size = New System.Drawing.Size(84, 14)
        Me.LblMainItemSNO.TabIndex = 8
        Me.LblMainItemSNO.Text = "LblMainItemSNO"
        Me.LblMainItemSNO.Visible = False
        '
        'lblMainItemCode
        '
        Me.lblMainItemCode.AutoSize = True
        Me.lblMainItemCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblMainItemCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMainItemCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMainItemCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMainItemCode.Location = New System.Drawing.Point(200, 14)
        Me.lblMainItemCode.Name = "lblMainItemCode"
        Me.lblMainItemCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMainItemCode.Size = New System.Drawing.Size(83, 14)
        Me.lblMainItemCode.TabIndex = 7
        Me.lblMainItemCode.Text = "lblMainItemCode"
        Me.lblMainItemCode.Visible = False
        '
        'lblDeptCode
        '
        Me.lblDeptCode.AutoSize = True
        Me.lblDeptCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblDeptCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDeptCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDeptCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDeptCode.Location = New System.Drawing.Point(140, 14)
        Me.lblDeptCode.Name = "lblDeptCode"
        Me.lblDeptCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDeptCode.Size = New System.Drawing.Size(64, 14)
        Me.lblDeptCode.TabIndex = 6
        Me.lblDeptCode.Text = "lblDeptCode"
        Me.lblDeptCode.Visible = False
        '
        'lblMKey
        '
        Me.lblMKey.AutoSize = True
        Me.lblMKey.BackColor = System.Drawing.SystemColors.Control
        Me.lblMKey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMKey.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMKey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMKey.Location = New System.Drawing.Point(74, 14)
        Me.lblMKey.Name = "lblMKey"
        Me.lblMKey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMKey.Size = New System.Drawing.Size(44, 14)
        Me.lblMKey.TabIndex = 5
        Me.lblMKey.Text = "lblMKey"
        Me.lblMKey.Visible = False
        '
        'FrmBOMAlternate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(578, 370)
        Me.ControlBox = False
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.Frame3)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmBOMAlternate"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Alternate Raw Material Deatil (BOM)"
        Me.Frame2.ResumeLayout(False)
        CType(Me.SprdBOM, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class