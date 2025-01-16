Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmPOItemIndent
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
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents CmdOK As System.Windows.Forms.Button
    Public WithEvents lblDivisionCode As System.Windows.Forms.Label
    Public WithEvents lblPORowNo As System.Windows.Forms.Label
    Public WithEvents lblPOQty As System.Windows.Forms.Label
    Public WithEvents LblQty As System.Windows.Forms.Label
    Public WithEvents LblPONo As System.Windows.Forms.Label
    Public WithEvents LblItemCode As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents lblItemDesc As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPOItemIndent))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.CmdOK = New System.Windows.Forms.Button()
        Me.lblDivisionCode = New System.Windows.Forms.Label()
        Me.lblPORowNo = New System.Windows.Forms.Label()
        Me.lblPOQty = New System.Windows.Forms.Label()
        Me.LblQty = New System.Windows.Forms.Label()
        Me.LblPONo = New System.Windows.Forms.Label()
        Me.LblItemCode = New System.Windows.Forms.Label()
        Me.lblItemDesc = New System.Windows.Forms.Label()
        Me.Frame1.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.SprdMain)
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 30)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(381, 151)
        Me.Frame1.TabIndex = 0
        Me.Frame1.TabStop = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 8)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(377, 141)
        Me.SprdMain.TabIndex = 1
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.cmdCancel)
        Me.Frame2.Controls.Add(Me.CmdOK)
        Me.Frame2.Controls.Add(Me.lblDivisionCode)
        Me.Frame2.Controls.Add(Me.lblPORowNo)
        Me.Frame2.Controls.Add(Me.lblPOQty)
        Me.Frame2.Controls.Add(Me.LblQty)
        Me.Frame2.Controls.Add(Me.LblPONo)
        Me.Frame2.Controls.Add(Me.LblItemCode)
        Me.Frame2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 176)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(381, 41)
        Me.Frame2.TabIndex = 4
        Me.Frame2.TabStop = False
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(310, 10)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(67, 27)
        Me.cmdCancel.TabIndex = 3
        Me.cmdCancel.Text = "&Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'CmdOK
        '
        Me.CmdOK.BackColor = System.Drawing.SystemColors.Control
        Me.CmdOK.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdOK.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdOK.Location = New System.Drawing.Point(2, 10)
        Me.CmdOK.Name = "CmdOK"
        Me.CmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdOK.Size = New System.Drawing.Size(67, 27)
        Me.CmdOK.TabIndex = 2
        Me.CmdOK.Text = "&OK"
        Me.CmdOK.UseVisualStyleBackColor = False
        '
        'lblDivisionCode
        '
        Me.lblDivisionCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblDivisionCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDivisionCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDivisionCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDivisionCode.Location = New System.Drawing.Point(208, 16)
        Me.lblDivisionCode.Name = "lblDivisionCode"
        Me.lblDivisionCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDivisionCode.Size = New System.Drawing.Size(31, 17)
        Me.lblDivisionCode.TabIndex = 11
        Me.lblDivisionCode.Text = "lblDivisionCode"
        '
        'lblPORowNo
        '
        Me.lblPORowNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblPORowNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPORowNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPORowNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPORowNo.Location = New System.Drawing.Point(120, 18)
        Me.lblPORowNo.Name = "lblPORowNo"
        Me.lblPORowNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPORowNo.Size = New System.Drawing.Size(67, 19)
        Me.lblPORowNo.TabIndex = 10
        Me.lblPORowNo.Text = "lblPORowNo"
        '
        'lblPOQty
        '
        Me.lblPOQty.BackColor = System.Drawing.SystemColors.Control
        Me.lblPOQty.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPOQty.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPOQty.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPOQty.Location = New System.Drawing.Point(298, 12)
        Me.lblPOQty.Name = "lblPOQty"
        Me.lblPOQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPOQty.Size = New System.Drawing.Size(73, 21)
        Me.lblPOQty.TabIndex = 9
        Me.lblPOQty.Text = "lblPOQty"
        Me.lblPOQty.Visible = False
        '
        'LblQty
        '
        Me.LblQty.BackColor = System.Drawing.SystemColors.Control
        Me.LblQty.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblQty.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblQty.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblQty.Location = New System.Drawing.Point(-150, -52)
        Me.LblQty.Name = "LblQty"
        Me.LblQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblQty.Size = New System.Drawing.Size(59, 13)
        Me.LblQty.TabIndex = 7
        Me.LblQty.Text = "LblQty"
        '
        'LblPONo
        '
        Me.LblPONo.BackColor = System.Drawing.SystemColors.Control
        Me.LblPONo.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblPONo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPONo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblPONo.Location = New System.Drawing.Point(120, -36)
        Me.LblPONo.Name = "LblPONo"
        Me.LblPONo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblPONo.Size = New System.Drawing.Size(51, 13)
        Me.LblPONo.TabIndex = 6
        Me.LblPONo.Text = "LblPONo"
        '
        'LblItemCode
        '
        Me.LblItemCode.BackColor = System.Drawing.SystemColors.Control
        Me.LblItemCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblItemCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblItemCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblItemCode.Location = New System.Drawing.Point(6, -42)
        Me.LblItemCode.Name = "LblItemCode"
        Me.LblItemCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblItemCode.Size = New System.Drawing.Size(61, 11)
        Me.LblItemCode.TabIndex = 5
        Me.LblItemCode.Text = "LblItemCode"
        '
        'lblItemDesc
        '
        Me.lblItemDesc.BackColor = System.Drawing.SystemColors.Control
        Me.lblItemDesc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblItemDesc.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblItemDesc.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItemDesc.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblItemDesc.Location = New System.Drawing.Point(0, 0)
        Me.lblItemDesc.Name = "lblItemDesc"
        Me.lblItemDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblItemDesc.Size = New System.Drawing.Size(381, 29)
        Me.lblItemDesc.TabIndex = 8
        Me.lblItemDesc.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'FrmPOItemIndent
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(381, 218)
        Me.ControlBox = False
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.lblItemDesc)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmPOItemIndent"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Item Indent"
        Me.Frame1.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class