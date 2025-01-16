Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmParamVoucherChk
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
        'Me.MdiParent = AccountGST.Master

        VB6_AddADODataBinding()
    End Sub
    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
        If Disposing Then
            VB6_RemoveADODataBinding()
            If Not components Is Nothing Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(Disposing)
    End Sub
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Public ToolTip1 As System.Windows.Forms.ToolTip
    Public WithEvents chkExpenseDate As System.Windows.Forms.CheckBox
    Public WithEvents TxtDtFrom As System.Windows.Forms.MaskedTextBox
    Public WithEvents TxtDtTo As System.Windows.Forms.MaskedTextBox
    Public WithEvents LblDtfr As System.Windows.Forms.Label
    Public WithEvents LblDtto As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents cmdShow As System.Windows.Forms.Button
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents FraButton As System.Windows.Forms.GroupBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmParamVoucherChk))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.chkExpenseDate = New System.Windows.Forms.CheckBox()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.TxtDtFrom = New System.Windows.Forms.MaskedTextBox()
        Me.TxtDtTo = New System.Windows.Forms.MaskedTextBox()
        Me.LblDtfr = New System.Windows.Forms.Label()
        Me.LblDtto = New System.Windows.Forms.Label()
        Me.FraButton = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.Frame2.SuspendLayout()
        Me.FraButton.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdShow
        '
        Me.cmdShow.BackColor = System.Drawing.SystemColors.Control
        Me.cmdShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdShow.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdShow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdShow.Image = CType(resources.GetObject("cmdShow.Image"), System.Drawing.Image)
        Me.cmdShow.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdShow.Location = New System.Drawing.Point(752, 12)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShow.Size = New System.Drawing.Size(67, 37)
        Me.cmdShow.TabIndex = 2
        Me.cmdShow.Text = "Sho&w"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Image = CType(resources.GetObject("cmdCancel.Image"), System.Drawing.Image)
        Me.cmdCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdCancel.Location = New System.Drawing.Point(826, 12)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(67, 37)
        Me.cmdCancel.TabIndex = 3
        Me.cmdCancel.Text = "&Close"
        Me.cmdCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdCancel, "Close")
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'chkExpenseDate
        '
        Me.chkExpenseDate.BackColor = System.Drawing.SystemColors.Control
        Me.chkExpenseDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkExpenseDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkExpenseDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkExpenseDate.Location = New System.Drawing.Point(102, 8)
        Me.chkExpenseDate.Name = "chkExpenseDate"
        Me.chkExpenseDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkExpenseDate.Size = New System.Drawing.Size(133, 18)
        Me.chkExpenseDate.TabIndex = 8
        Me.chkExpenseDate.Text = "Expense Date"
        Me.chkExpenseDate.UseVisualStyleBackColor = False
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.TxtDtFrom)
        Me.Frame2.Controls.Add(Me.TxtDtTo)
        Me.Frame2.Controls.Add(Me.LblDtfr)
        Me.Frame2.Controls.Add(Me.LblDtto)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(587, 0)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(311, 39)
        Me.Frame2.TabIndex = 5
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Date Range"
        '
        'TxtDtFrom
        '
        Me.TxtDtFrom.AllowPromptAsInput = False
        Me.TxtDtFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDtFrom.Location = New System.Drawing.Point(78, 14)
        Me.TxtDtFrom.Mask = "##/##/####"
        Me.TxtDtFrom.Name = "TxtDtFrom"
        Me.TxtDtFrom.Size = New System.Drawing.Size(75, 20)
        Me.TxtDtFrom.TabIndex = 0
        '
        'TxtDtTo
        '
        Me.TxtDtTo.AllowPromptAsInput = False
        Me.TxtDtTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDtTo.Location = New System.Drawing.Point(228, 14)
        Me.TxtDtTo.Mask = "##/##/####"
        Me.TxtDtTo.Name = "TxtDtTo"
        Me.TxtDtTo.Size = New System.Drawing.Size(75, 20)
        Me.TxtDtTo.TabIndex = 1
        '
        'LblDtfr
        '
        Me.LblDtfr.AutoSize = True
        Me.LblDtfr.BackColor = System.Drawing.SystemColors.Control
        Me.LblDtfr.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblDtfr.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDtfr.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblDtfr.Location = New System.Drawing.Point(10, 18)
        Me.LblDtfr.Name = "LblDtfr"
        Me.LblDtfr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblDtfr.Size = New System.Drawing.Size(69, 14)
        Me.LblDtfr.TabIndex = 7
        Me.LblDtfr.Text = "Date From :"
        '
        'LblDtto
        '
        Me.LblDtto.AutoSize = True
        Me.LblDtto.BackColor = System.Drawing.SystemColors.Control
        Me.LblDtto.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblDtto.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDtto.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblDtto.Location = New System.Drawing.Point(168, 18)
        Me.LblDtto.Name = "LblDtto"
        Me.LblDtto.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblDtto.Size = New System.Drawing.Size(53, 14)
        Me.LblDtto.TabIndex = 6
        Me.LblDtto.Text = "Date To :"
        '
        'FraButton
        '
        Me.FraButton.BackColor = System.Drawing.SystemColors.Control
        Me.FraButton.Controls.Add(Me.cmdShow)
        Me.FraButton.Controls.Add(Me.cmdCancel)
        Me.FraButton.Controls.Add(Me.Report1)
        Me.FraButton.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraButton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraButton.Location = New System.Drawing.Point(1, 558)
        Me.FraButton.Name = "FraButton"
        Me.FraButton.Padding = New System.Windows.Forms.Padding(0)
        Me.FraButton.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraButton.Size = New System.Drawing.Size(897, 51)
        Me.FraButton.TabIndex = 4
        Me.FraButton.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(6, 10)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 4
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(0, 42)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(900, 516)
        Me.SprdMain.TabIndex = 9
        '
        'FrmParamVoucherChk
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(901, 611)
        Me.Controls.Add(Me.chkExpenseDate)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.FraButton)
        Me.Controls.Add(Me.SprdMain)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(4, 23)
        Me.MaximizeBox = False
        Me.Name = "FrmParamVoucherChk"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Voucher Integrity Check"
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.FraButton.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        'SprdMain.DataSource = CType(AData1, MSDATASRC.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        SprdMain.DataSource = Nothing
    End Sub
#End Region
End Class