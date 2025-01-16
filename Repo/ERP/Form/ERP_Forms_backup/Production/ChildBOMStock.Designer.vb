Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmChildBOMStock
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
    Public WithEvents SprdDlv As AxFPSpreadADO.AxfpSpread
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents lblItemCode As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmChildBOMStock))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.SprdDlv = New AxFPSpreadADO.AxfpSpread()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.lblRefNo = New System.Windows.Forms.Label()
        Me.lblRefType = New System.Windows.Forms.Label()
        Me.lblDivision = New System.Windows.Forms.Label()
        Me.lblProductionType = New System.Windows.Forms.Label()
        Me.lblDate = New System.Windows.Forms.Label()
        Me.lblDeptCode = New System.Windows.Forms.Label()
        Me.lblItemCode = New System.Windows.Forms.Label()
        Me.Frame2.SuspendLayout()
        CType(Me.SprdDlv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        Me.SuspendLayout()
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(462, 12)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(60, 34)
        Me.CmdClose.TabIndex = 5
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close the Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.SprdDlv)
        Me.Frame2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, -2)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(527, 327)
        Me.Frame2.TabIndex = 0
        Me.Frame2.TabStop = False
        '
        'SprdDlv
        '
        Me.SprdDlv.DataSource = Nothing
        Me.SprdDlv.Location = New System.Drawing.Point(2, 8)
        Me.SprdDlv.Name = "SprdDlv"
        Me.SprdDlv.OcxState = CType(resources.GetObject("SprdDlv.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdDlv.Size = New System.Drawing.Size(522, 314)
        Me.SprdDlv.TabIndex = 30
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.lblRefNo)
        Me.Frame3.Controls.Add(Me.lblRefType)
        Me.Frame3.Controls.Add(Me.lblDivision)
        Me.Frame3.Controls.Add(Me.lblProductionType)
        Me.Frame3.Controls.Add(Me.lblDate)
        Me.Frame3.Controls.Add(Me.lblDeptCode)
        Me.Frame3.Controls.Add(Me.CmdClose)
        Me.Frame3.Controls.Add(Me.lblItemCode)
        Me.Frame3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 320)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(527, 49)
        Me.Frame3.TabIndex = 1
        Me.Frame3.TabStop = False
        '
        'lblRefNo
        '
        Me.lblRefNo.AutoSize = True
        Me.lblRefNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblRefNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblRefNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRefNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRefNo.Location = New System.Drawing.Point(396, 16)
        Me.lblRefNo.Name = "lblRefNo"
        Me.lblRefNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblRefNo.Size = New System.Drawing.Size(52, 13)
        Me.lblRefNo.TabIndex = 14
        Me.lblRefNo.Text = "lblRefNo"
        Me.lblRefNo.Visible = False
        '
        'lblRefType
        '
        Me.lblRefType.AutoSize = True
        Me.lblRefType.BackColor = System.Drawing.SystemColors.Control
        Me.lblRefType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblRefType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRefType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRefType.Location = New System.Drawing.Point(340, 28)
        Me.lblRefType.Name = "lblRefType"
        Me.lblRefType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblRefType.Size = New System.Drawing.Size(61, 13)
        Me.lblRefType.TabIndex = 13
        Me.lblRefType.Text = "lblRefType"
        Me.lblRefType.Visible = False
        '
        'lblDivision
        '
        Me.lblDivision.AutoSize = True
        Me.lblDivision.BackColor = System.Drawing.SystemColors.Control
        Me.lblDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDivision.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDivision.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDivision.Location = New System.Drawing.Point(344, 12)
        Me.lblDivision.Name = "lblDivision"
        Me.lblDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDivision.Size = New System.Drawing.Size(61, 13)
        Me.lblDivision.TabIndex = 12
        Me.lblDivision.Text = "lblDivision"
        Me.lblDivision.Visible = False
        '
        'lblProductionType
        '
        Me.lblProductionType.AutoSize = True
        Me.lblProductionType.BackColor = System.Drawing.SystemColors.Control
        Me.lblProductionType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblProductionType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProductionType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblProductionType.Location = New System.Drawing.Point(126, 28)
        Me.lblProductionType.Name = "lblProductionType"
        Me.lblProductionType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblProductionType.Size = New System.Drawing.Size(99, 13)
        Me.lblProductionType.TabIndex = 11
        Me.lblProductionType.Text = "lblProductionType"
        Me.lblProductionType.Visible = False
        '
        'lblDate
        '
        Me.lblDate.AutoSize = True
        Me.lblDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDate.Location = New System.Drawing.Point(188, 6)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDate.Size = New System.Drawing.Size(44, 13)
        Me.lblDate.TabIndex = 10
        Me.lblDate.Text = "lblDate"
        Me.lblDate.Visible = False
        '
        'lblDeptCode
        '
        Me.lblDeptCode.AutoSize = True
        Me.lblDeptCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblDeptCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDeptCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDeptCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDeptCode.Location = New System.Drawing.Point(244, 18)
        Me.lblDeptCode.Name = "lblDeptCode"
        Me.lblDeptCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDeptCode.Size = New System.Drawing.Size(72, 13)
        Me.lblDeptCode.TabIndex = 9
        Me.lblDeptCode.Text = "lblDeptCode"
        Me.lblDeptCode.Visible = False
        '
        'lblItemCode
        '
        Me.lblItemCode.AutoSize = True
        Me.lblItemCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblItemCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblItemCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItemCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblItemCode.Location = New System.Drawing.Point(86, 16)
        Me.lblItemCode.Name = "lblItemCode"
        Me.lblItemCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblItemCode.Size = New System.Drawing.Size(70, 13)
        Me.lblItemCode.TabIndex = 8
        Me.lblItemCode.Text = "lblItemCode"
        Me.lblItemCode.Visible = False
        '
        'frmChildBOMStock
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(528, 370)
        Me.ControlBox = False
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.Frame3)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmChildBOMStock"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Child BOM Stock"
        Me.Frame2.ResumeLayout(False)
        CType(Me.SprdDlv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Public WithEvents lblDeptCode As Label
    Public WithEvents lblDate As Label
    Public WithEvents lblProductionType As Label
    Public WithEvents lblRefNo As Label
    Public WithEvents lblRefType As Label
    Public WithEvents lblDivision As Label
#End Region
End Class