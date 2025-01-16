Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmRGPOutDetail
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
    Public WithEvents SprdSubMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents lblInQty As System.Windows.Forms.Label
    Public WithEvents lblOutQty As System.Windows.Forms.Label
    Public WithEvents lblTotal As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents cmdOk As System.Windows.Forms.Button
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents lblRGPDate As System.Windows.Forms.Label
    Public WithEvents lblAutoNumber As System.Windows.Forms.Label
    Public WithEvents lblMainActiveRow As System.Windows.Forms.Label
    Public WithEvents lblItemCode As System.Windows.Forms.Label
    Public WithEvents LblModifyMode As System.Windows.Forms.Label
    Public WithEvents LblAddMode As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmRGPOutDetail))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.SprdSubMain = New AxFPSpreadADO.AxfpSpread()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblInQty = New System.Windows.Forms.Label()
        Me.lblOutQty = New System.Windows.Forms.Label()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.lblRGPDate = New System.Windows.Forms.Label()
        Me.lblAutoNumber = New System.Windows.Forms.Label()
        Me.lblMainActiveRow = New System.Windows.Forms.Label()
        Me.lblItemCode = New System.Windows.Forms.Label()
        Me.LblModifyMode = New System.Windows.Forms.Label()
        Me.LblAddMode = New System.Windows.Forms.Label()
        Me.Frame2.SuspendLayout()
        CType(Me.SprdSubMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdOk
        '
        Me.cmdOk.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOk.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOk.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOk.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOk.Image = CType(resources.GetObject("cmdOk.Image"), System.Drawing.Image)
        Me.cmdOk.Location = New System.Drawing.Point(4, 12)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOk.Size = New System.Drawing.Size(60, 34)
        Me.cmdOk.TabIndex = 5
        Me.cmdOk.Text = "&Save"
        Me.cmdOk.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdOk, "Save the Form")
        Me.cmdOk.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(578, 12)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(60, 34)
        Me.CmdClose.TabIndex = 4
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close the Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.SprdSubMain)
        Me.Frame2.Controls.Add(Me.Label1)
        Me.Frame2.Controls.Add(Me.lblInQty)
        Me.Frame2.Controls.Add(Me.lblOutQty)
        Me.Frame2.Controls.Add(Me.lblTotal)
        Me.Frame2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, -2)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(641, 327)
        Me.Frame2.TabIndex = 0
        Me.Frame2.TabStop = False
        '
        'SprdSubMain
        '
        Me.SprdSubMain.DataSource = Nothing
        Me.SprdSubMain.Location = New System.Drawing.Point(2, 8)
        Me.SprdSubMain.Name = "SprdSubMain"
        Me.SprdSubMain.OcxState = CType(resources.GetObject("SprdSubMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdSubMain.Size = New System.Drawing.Size(635, 295)
        Me.SprdSubMain.TabIndex = 11
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(410, 308)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(130, 13)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Total Wt. / Qty (Inward):"
        '
        'lblInQty
        '
        Me.lblInQty.BackColor = System.Drawing.SystemColors.Control
        Me.lblInQty.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblInQty.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblInQty.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInQty.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInQty.Location = New System.Drawing.Point(552, 306)
        Me.lblInQty.Name = "lblInQty"
        Me.lblInQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblInQty.Size = New System.Drawing.Size(85, 19)
        Me.lblInQty.TabIndex = 9
        Me.lblInQty.Text = "0.00"
        Me.lblInQty.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblOutQty
        '
        Me.lblOutQty.BackColor = System.Drawing.SystemColors.Control
        Me.lblOutQty.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblOutQty.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblOutQty.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOutQty.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblOutQty.Location = New System.Drawing.Point(152, 306)
        Me.lblOutQty.Name = "lblOutQty"
        Me.lblOutQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblOutQty.Size = New System.Drawing.Size(85, 19)
        Me.lblOutQty.TabIndex = 8
        Me.lblOutQty.Text = "0.00"
        Me.lblOutQty.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotal
        '
        Me.lblTotal.AutoSize = True
        Me.lblTotal.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotal.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotal.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotal.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTotal.Location = New System.Drawing.Point(2, 308)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotal.Size = New System.Drawing.Size(139, 13)
        Me.lblTotal.TabIndex = 7
        Me.lblTotal.Text = "Total Wt. / Qty (Outward):"
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.cmdOk)
        Me.Frame3.Controls.Add(Me.CmdClose)
        Me.Frame3.Controls.Add(Me.lblRGPDate)
        Me.Frame3.Controls.Add(Me.lblAutoNumber)
        Me.Frame3.Controls.Add(Me.lblMainActiveRow)
        Me.Frame3.Controls.Add(Me.lblItemCode)
        Me.Frame3.Controls.Add(Me.LblModifyMode)
        Me.Frame3.Controls.Add(Me.LblAddMode)
        Me.Frame3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 320)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(641, 49)
        Me.Frame3.TabIndex = 1
        Me.Frame3.TabStop = False
        '
        'lblRGPDate
        '
        Me.lblRGPDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblRGPDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblRGPDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRGPDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRGPDate.Location = New System.Drawing.Point(152, 14)
        Me.lblRGPDate.Name = "lblRGPDate"
        Me.lblRGPDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblRGPDate.Size = New System.Drawing.Size(65, 21)
        Me.lblRGPDate.TabIndex = 14
        Me.lblRGPDate.Text = "lblRGPDate"
        '
        'lblAutoNumber
        '
        Me.lblAutoNumber.BackColor = System.Drawing.SystemColors.Control
        Me.lblAutoNumber.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAutoNumber.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAutoNumber.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAutoNumber.Location = New System.Drawing.Point(494, 16)
        Me.lblAutoNumber.Name = "lblAutoNumber"
        Me.lblAutoNumber.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAutoNumber.Size = New System.Drawing.Size(77, 15)
        Me.lblAutoNumber.TabIndex = 13
        Me.lblAutoNumber.Text = "0"
        '
        'lblMainActiveRow
        '
        Me.lblMainActiveRow.BackColor = System.Drawing.SystemColors.Control
        Me.lblMainActiveRow.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMainActiveRow.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMainActiveRow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMainActiveRow.Location = New System.Drawing.Point(412, 16)
        Me.lblMainActiveRow.Name = "lblMainActiveRow"
        Me.lblMainActiveRow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMainActiveRow.Size = New System.Drawing.Size(45, 13)
        Me.lblMainActiveRow.TabIndex = 10
        Me.lblMainActiveRow.Text = "MainActiveRow"
        Me.lblMainActiveRow.Visible = False
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
        Me.lblItemCode.TabIndex = 6
        Me.lblItemCode.Text = "lblItemCode"
        Me.lblItemCode.Visible = False
        '
        'LblModifyMode
        '
        Me.LblModifyMode.AutoSize = True
        Me.LblModifyMode.BackColor = System.Drawing.SystemColors.Control
        Me.LblModifyMode.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblModifyMode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblModifyMode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblModifyMode.Location = New System.Drawing.Point(308, 14)
        Me.LblModifyMode.Name = "LblModifyMode"
        Me.LblModifyMode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblModifyMode.Size = New System.Drawing.Size(89, 13)
        Me.LblModifyMode.TabIndex = 3
        Me.LblModifyMode.Text = "LblModifyMode"
        Me.LblModifyMode.Visible = False
        '
        'LblAddMode
        '
        Me.LblAddMode.AutoSize = True
        Me.LblAddMode.BackColor = System.Drawing.SystemColors.Control
        Me.LblAddMode.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblAddMode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAddMode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblAddMode.Location = New System.Drawing.Point(228, 22)
        Me.LblAddMode.Name = "LblAddMode"
        Me.LblAddMode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblAddMode.Size = New System.Drawing.Size(73, 13)
        Me.LblAddMode.TabIndex = 2
        Me.LblAddMode.Text = "LblAddMode"
        Me.LblAddMode.Visible = False
        '
        'FrmRGPOutDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(642, 370)
        Me.ControlBox = False
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.Frame3)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmRGPOutDetail"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "RGP Inward Details"
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        CType(Me.SprdSubMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class