Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmPackingDetail
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
    Public WithEvents SprdOPR As AxFPSpreadADO.AxfpSpread
    Public WithEvents lblProductCode As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents cmdOk As System.Windows.Forms.Button
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents lblPDIQty As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents lblTotQty As System.Windows.Forms.Label
    Public WithEvents lblMainProductCode As System.Windows.Forms.Label
    Public WithEvents lblRefDate As System.Windows.Forms.Label
    Public WithEvents lblDeptCode As System.Windows.Forms.Label
    Public WithEvents LblModifyMode As System.Windows.Forms.Label
    Public WithEvents LblAddMode As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(FrmPackingDetail))
        Me.components = New System.ComponentModel.Container()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
        Me.Frame2 = New System.Windows.Forms.GroupBox
        Me.SprdOPR = New AxFPSpreadADO.AxfpSpread
        Me.lblProductCode = New System.Windows.Forms.Label
        Me.Frame3 = New System.Windows.Forms.GroupBox
        Me.cmdOk = New System.Windows.Forms.Button
        Me.CmdClose = New System.Windows.Forms.Button
        Me.lblPDIQty = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblTotQty = New System.Windows.Forms.Label
        Me.lblMainProductCode = New System.Windows.Forms.Label
        Me.lblRefDate = New System.Windows.Forms.Label
        Me.lblDeptCode = New System.Windows.Forms.Label
        Me.LblModifyMode = New System.Windows.Forms.Label
        Me.LblAddMode = New System.Windows.Forms.Label
        Me.Frame2.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.SuspendLayout()
        Me.ToolTip1.Active = True
        CType(Me.SprdOPR, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Text = "Packing Details"
        Me.ClientSize = New System.Drawing.Size(528, 370)
        Me.Location = New System.Drawing.Point(3, 22)
        Me.ControlBox = False
        Me.Icon = CType(resources.GetObject("FrmPackingDetail.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.Enabled = True
        Me.KeyPreview = False
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = True
        Me.HelpButton = False
        Me.WindowState = System.Windows.Forms.FormWindowState.Normal
        Me.Name = "FrmPackingDetail"
        Me.Frame2.Size = New System.Drawing.Size(527, 327)
        Me.Frame2.Location = New System.Drawing.Point(0, -2)
        Me.Frame2.TabIndex = 0
        Me.Frame2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Enabled = True
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Visible = True
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.Name = "Frame2"
        SprdOPR.OcxState = CType(resources.GetObject("SprdOPR.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdOPR.Size = New System.Drawing.Size(521, 287)
        Me.SprdOPR.Location = New System.Drawing.Point(2, 36)
        Me.SprdOPR.TabIndex = 6
        Me.SprdOPR.Name = "SprdOPR"
        Me.lblProductCode.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.lblProductCode.Text = "lblProductCode"
        Me.lblProductCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProductCode.Size = New System.Drawing.Size(519, 23)
        Me.lblProductCode.Location = New System.Drawing.Point(4, 10)
        Me.lblProductCode.TabIndex = 7
        Me.lblProductCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblProductCode.Enabled = True
        Me.lblProductCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblProductCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblProductCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblProductCode.UseMnemonic = True
        Me.lblProductCode.Visible = True
        Me.lblProductCode.AutoSize = False
        Me.lblProductCode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblProductCode.Name = "lblProductCode"
        Me.Frame3.Size = New System.Drawing.Size(527, 49)
        Me.Frame3.Location = New System.Drawing.Point(0, 320)
        Me.Frame3.TabIndex = 1
        Me.Frame3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Enabled = True
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Visible = True
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.Name = "Frame3"
        Me.cmdOk.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdOk.Text = "&Save"
        Me.cmdOk.Size = New System.Drawing.Size(60, 34)
        Me.cmdOk.Location = New System.Drawing.Point(4, 12)
        Me.cmdOk.Image = CType(resources.GetObject("cmdOk.Image"), System.Drawing.Image)
        Me.cmdOk.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.cmdOk, "Save the Form")
        Me.cmdOk.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOk.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOk.CausesValidation = True
        Me.cmdOk.Enabled = True
        Me.cmdOk.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOk.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOk.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOk.TabStop = True
        Me.cmdOk.Name = "cmdOk"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.Size = New System.Drawing.Size(60, 34)
        Me.CmdClose.Location = New System.Drawing.Point(64, 12)
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close the Form")
        Me.CmdClose.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.CausesValidation = True
        Me.CmdClose.Enabled = True
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.TabStop = True
        Me.CmdClose.Name = "CmdClose"
        Me.lblPDIQty.Text = "0"
        Me.lblPDIQty.Size = New System.Drawing.Size(41, 11)
        Me.lblPDIQty.Location = New System.Drawing.Point(346, 30)
        Me.lblPDIQty.TabIndex = 13
        Me.lblPDIQty.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPDIQty.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblPDIQty.BackColor = System.Drawing.SystemColors.Control
        Me.lblPDIQty.Enabled = True
        Me.lblPDIQty.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPDIQty.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPDIQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPDIQty.UseMnemonic = True
        Me.lblPDIQty.Visible = True
        Me.lblPDIQty.AutoSize = False
        Me.lblPDIQty.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblPDIQty.Name = "lblPDIQty"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label1.Text = "Total Packing :"
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Size = New System.Drawing.Size(91, 13)
        Me.Label1.Location = New System.Drawing.Point(330, 10)
        Me.Label1.TabIndex = 12
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Enabled = True
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.UseMnemonic = True
        Me.Label1.Visible = True
        Me.Label1.AutoSize = False
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Label1.Name = "Label1"
        Me.lblTotQty.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblTotQty.Text = "0"
        Me.lblTotQty.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotQty.Size = New System.Drawing.Size(95, 15)
        Me.lblTotQty.Location = New System.Drawing.Point(426, 10)
        Me.lblTotQty.TabIndex = 11
        Me.lblTotQty.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotQty.Enabled = True
        Me.lblTotQty.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTotQty.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotQty.UseMnemonic = True
        Me.lblTotQty.Visible = True
        Me.lblTotQty.AutoSize = False
        Me.lblTotQty.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotQty.Name = "lblTotQty"
        Me.lblMainProductCode.Text = "lblMainProductCode"
        Me.lblMainProductCode.Size = New System.Drawing.Size(47, 13)
        Me.lblMainProductCode.Location = New System.Drawing.Point(272, 16)
        Me.lblMainProductCode.TabIndex = 10
        Me.lblMainProductCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMainProductCode.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblMainProductCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblMainProductCode.Enabled = True
        Me.lblMainProductCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMainProductCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMainProductCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMainProductCode.UseMnemonic = True
        Me.lblMainProductCode.Visible = True
        Me.lblMainProductCode.AutoSize = False
        Me.lblMainProductCode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblMainProductCode.Name = "lblMainProductCode"
        Me.lblRefDate.Text = "lblRefDate"
        Me.lblRefDate.Size = New System.Drawing.Size(50, 13)
        Me.lblRefDate.Location = New System.Drawing.Point(284, 32)
        Me.lblRefDate.TabIndex = 9
        Me.lblRefDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRefDate.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblRefDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblRefDate.Enabled = True
        Me.lblRefDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRefDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblRefDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblRefDate.UseMnemonic = True
        Me.lblRefDate.Visible = True
        Me.lblRefDate.AutoSize = True
        Me.lblRefDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblRefDate.Name = "lblRefDate"
        Me.lblDeptCode.Text = "lblDeptCode"
        Me.lblDeptCode.Size = New System.Drawing.Size(31, 11)
        Me.lblDeptCode.Location = New System.Drawing.Point(148, 18)
        Me.lblDeptCode.TabIndex = 8
        Me.lblDeptCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDeptCode.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblDeptCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblDeptCode.Enabled = True
        Me.lblDeptCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDeptCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDeptCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDeptCode.UseMnemonic = True
        Me.lblDeptCode.Visible = True
        Me.lblDeptCode.AutoSize = False
        Me.lblDeptCode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblDeptCode.Name = "lblDeptCode"
        Me.LblModifyMode.Text = "LblModifyMode"
        Me.LblModifyMode.Size = New System.Drawing.Size(72, 13)
        Me.LblModifyMode.Location = New System.Drawing.Point(190, 32)
        Me.LblModifyMode.TabIndex = 3
        Me.LblModifyMode.Visible = False
        Me.LblModifyMode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblModifyMode.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.LblModifyMode.BackColor = System.Drawing.SystemColors.Control
        Me.LblModifyMode.Enabled = True
        Me.LblModifyMode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblModifyMode.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblModifyMode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblModifyMode.UseMnemonic = True
        Me.LblModifyMode.AutoSize = True
        Me.LblModifyMode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.LblModifyMode.Name = "LblModifyMode"
        Me.LblAddMode.Text = "LblAddMode"
        Me.LblAddMode.Size = New System.Drawing.Size(60, 13)
        Me.LblAddMode.Location = New System.Drawing.Point(190, 18)
        Me.LblAddMode.TabIndex = 2
        Me.LblAddMode.Visible = False
        Me.LblAddMode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAddMode.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.LblAddMode.BackColor = System.Drawing.SystemColors.Control
        Me.LblAddMode.Enabled = True
        Me.LblAddMode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblAddMode.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblAddMode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblAddMode.UseMnemonic = True
        Me.LblAddMode.AutoSize = True
        Me.LblAddMode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.LblAddMode.Name = "LblAddMode"
        Me.Controls.Add(Frame2)
        Me.Controls.Add(Frame3)
        Me.Frame2.Controls.Add(SprdOPR)
        Me.Frame2.Controls.Add(lblProductCode)
        Me.Frame3.Controls.Add(cmdOk)
        Me.Frame3.Controls.Add(CmdClose)
        Me.Frame3.Controls.Add(lblPDIQty)
        Me.Frame3.Controls.Add(Label1)
        Me.Frame3.Controls.Add(lblTotQty)
        Me.Frame3.Controls.Add(lblMainProductCode)
        Me.Frame3.Controls.Add(lblRefDate)
        Me.Frame3.Controls.Add(lblDeptCode)
        Me.Frame3.Controls.Add(LblModifyMode)
        Me.Frame3.Controls.Add(LblAddMode)
        CType(Me.SprdOPR, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame2.ResumeLayout(False)
        Me.Frame3.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub
#End Region
End Class