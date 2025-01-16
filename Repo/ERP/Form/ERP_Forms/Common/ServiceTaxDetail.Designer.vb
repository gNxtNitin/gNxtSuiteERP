Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmServiceTaxDetail
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
    Public WithEvents txtKKCessPer As System.Windows.Forms.TextBox
    Public WithEvents txtSBCessPer As System.Windows.Forms.TextBox
    Public WithEvents cmdRefresh As System.Windows.Forms.Button
    Public WithEvents cmdCalc As System.Windows.Forms.Button
    Public WithEvents txtSTPer As System.Windows.Forms.TextBox
    Public WithEvents txtSHECessPer As System.Windows.Forms.TextBox
    Public WithEvents txtCessPer As System.Windows.Forms.TextBox
    Public WithEvents txtServPer As System.Windows.Forms.TextBox
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents fraserviceInfo As System.Windows.Forms.GroupBox
    Public WithEvents lblDC As System.Windows.Forms.Label
    Public WithEvents lblAmount As System.Windows.Forms.Label
    Public WithEvents Amount As System.Windows.Forms.Label
    Public WithEvents lblAccountName As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents cmdOk As System.Windows.Forms.Button
    Public WithEvents lblKKCessAmount As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents lblSBCessAmount As System.Windows.Forms.Label
    Public WithEvents lblBillAmount As System.Windows.Forms.Label
    Public WithEvents lblSHECessAmt As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents LblNetAmt As System.Windows.Forms.Label
    Public WithEvents LblNet As System.Windows.Forms.Label
    Public WithEvents lblCessAmt As System.Windows.Forms.Label
    Public WithEvents lblServiceTaxAmt As System.Windows.Forms.Label
    Public WithEvents LblTotal As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents lblDiffAmt As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents lblTrnRowNo As System.Windows.Forms.Label
    Public WithEvents lblAccountCode As System.Windows.Forms.Label
    Public WithEvents lblADDMode As System.Windows.Forms.Label
    Public WithEvents lblModifyMode As System.Windows.Forms.Label
    Public WithEvents lblBillNo As System.Windows.Forms.Label
    Public WithEvents lblBillYear As System.Windows.Forms.Label
    Public WithEvents lblVDate As System.Windows.Forms.Label
    Public WithEvents lblNarration As System.Windows.Forms.Label
    Public WithEvents lblBookType As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmServiceTaxDetail))
        Me.components = New System.ComponentModel.Container()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
        Me.fraserviceInfo = New System.Windows.Forms.GroupBox
        Me.txtKKCessPer = New System.Windows.Forms.TextBox
        Me.txtSBCessPer = New System.Windows.Forms.TextBox
        Me.cmdRefresh = New System.Windows.Forms.Button
        Me.cmdCalc = New System.Windows.Forms.Button
        Me.txtSTPer = New System.Windows.Forms.TextBox
        Me.txtSHECessPer = New System.Windows.Forms.TextBox
        Me.txtCessPer = New System.Windows.Forms.TextBox
        Me.txtServPer = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Frame1 = New System.Windows.Forms.GroupBox
        Me.lblDC = New System.Windows.Forms.Label
        Me.lblAmount = New System.Windows.Forms.Label
        Me.Amount = New System.Windows.Forms.Label
        Me.lblAccountName = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Frame2 = New System.Windows.Forms.GroupBox
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread
        Me.Frame3 = New System.Windows.Forms.GroupBox
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.cmdOk = New System.Windows.Forms.Button
        Me.lblKKCessAmount = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.lblSBCessAmount = New System.Windows.Forms.Label
        Me.lblBillAmount = New System.Windows.Forms.Label
        Me.lblSHECessAmt = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.LblNetAmt = New System.Windows.Forms.Label
        Me.LblNet = New System.Windows.Forms.Label
        Me.lblCessAmt = New System.Windows.Forms.Label
        Me.lblServiceTaxAmt = New System.Windows.Forms.Label
        Me.LblTotal = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblDiffAmt = New System.Windows.Forms.Label
        Me.lblTrnRowNo = New System.Windows.Forms.Label
        Me.lblAccountCode = New System.Windows.Forms.Label
        Me.lblADDMode = New System.Windows.Forms.Label
        Me.lblModifyMode = New System.Windows.Forms.Label
        Me.lblBillNo = New System.Windows.Forms.Label
        Me.lblBillYear = New System.Windows.Forms.Label
        Me.lblVDate = New System.Windows.Forms.Label
        Me.lblNarration = New System.Windows.Forms.Label
        Me.lblBookType = New System.Windows.Forms.Label
        Me.fraserviceInfo.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.SuspendLayout()
        Me.ToolTip1.Active = True
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Text = "Service Tax Detail"
        Me.ClientSize = New System.Drawing.Size(684, 449)
        Me.Location = New System.Drawing.Point(10, 29)
        Me.ControlBox = False
        Me.Icon = CType(resources.GetObject("frmServiceTaxDetail.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.Enabled = True
        Me.KeyPreview = False
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = True
        Me.HelpButton = False
        Me.WindowState = System.Windows.Forms.FormWindowState.Normal
        Me.Name = "frmServiceTaxDetail"
        Me.fraserviceInfo.Size = New System.Drawing.Size(683, 55)
        Me.fraserviceInfo.Location = New System.Drawing.Point(0, 32)
        Me.fraserviceInfo.TabIndex = 27
        Me.fraserviceInfo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraserviceInfo.BackColor = System.Drawing.SystemColors.Control
        Me.fraserviceInfo.Enabled = True
        Me.fraserviceInfo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraserviceInfo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraserviceInfo.Visible = True
        Me.fraserviceInfo.Padding = New System.Windows.Forms.Padding(0)
        Me.fraserviceInfo.Name = "fraserviceInfo"
        Me.txtKKCessPer.AutoSize = False
        Me.txtKKCessPer.Size = New System.Drawing.Size(41, 19)
        Me.txtKKCessPer.Location = New System.Drawing.Point(599, 10)
        Me.txtKKCessPer.TabIndex = 32
        Me.txtKKCessPer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtKKCessPer.AcceptsReturn = True
        Me.txtKKCessPer.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtKKCessPer.BackColor = System.Drawing.SystemColors.Window
        Me.txtKKCessPer.CausesValidation = True
        Me.txtKKCessPer.Enabled = True
        Me.txtKKCessPer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtKKCessPer.HideSelection = True
        Me.txtKKCessPer.ReadOnly = False
        Me.txtKKCessPer.Maxlength = 0
        Me.txtKKCessPer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtKKCessPer.MultiLine = False
        Me.txtKKCessPer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtKKCessPer.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtKKCessPer.TabStop = True
        Me.txtKKCessPer.Visible = True
        Me.txtKKCessPer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtKKCessPer.Name = "txtKKCessPer"
        Me.txtSBCessPer.AutoSize = False
        Me.txtSBCessPer.Size = New System.Drawing.Size(41, 19)
        Me.txtSBCessPer.Location = New System.Drawing.Point(456, 10)
        Me.txtSBCessPer.TabIndex = 31
        Me.txtSBCessPer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSBCessPer.AcceptsReturn = True
        Me.txtSBCessPer.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtSBCessPer.BackColor = System.Drawing.SystemColors.Window
        Me.txtSBCessPer.CausesValidation = True
        Me.txtSBCessPer.Enabled = True
        Me.txtSBCessPer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSBCessPer.HideSelection = True
        Me.txtSBCessPer.ReadOnly = False
        Me.txtSBCessPer.Maxlength = 0
        Me.txtSBCessPer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSBCessPer.MultiLine = False
        Me.txtSBCessPer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSBCessPer.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSBCessPer.TabStop = True
        Me.txtSBCessPer.Visible = True
        Me.txtSBCessPer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSBCessPer.Name = "txtSBCessPer"
        Me.cmdRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdRefresh.Text = "Refresh % of Service Provider"
        Me.cmdRefresh.Size = New System.Drawing.Size(171, 21)
        Me.cmdRefresh.Location = New System.Drawing.Point(508, 30)
        Me.cmdRefresh.TabIndex = 45
        Me.cmdRefresh.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdRefresh.BackColor = System.Drawing.SystemColors.Control
        Me.cmdRefresh.CausesValidation = True
        Me.cmdRefresh.Enabled = True
        Me.cmdRefresh.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdRefresh.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdRefresh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdRefresh.TabStop = True
        Me.cmdRefresh.Name = "cmdRefresh"
        Me.cmdCalc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdCalc.Text = "Calculation"
        Me.cmdCalc.Size = New System.Drawing.Size(69, 19)
        Me.cmdCalc.Location = New System.Drawing.Point(362, 30)
        Me.cmdCalc.TabIndex = 43
        Me.cmdCalc.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCalc.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCalc.CausesValidation = True
        Me.cmdCalc.Enabled = True
        Me.cmdCalc.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCalc.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCalc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCalc.TabStop = True
        Me.cmdCalc.Name = "cmdCalc"
        Me.txtSTPer.AutoSize = False
        Me.txtSTPer.Size = New System.Drawing.Size(41, 19)
        Me.txtSTPer.Location = New System.Drawing.Point(278, 30)
        Me.txtSTPer.TabIndex = 39
        Me.txtSTPer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSTPer.AcceptsReturn = True
        Me.txtSTPer.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtSTPer.BackColor = System.Drawing.SystemColors.Window
        Me.txtSTPer.CausesValidation = True
        Me.txtSTPer.Enabled = True
        Me.txtSTPer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSTPer.HideSelection = True
        Me.txtSTPer.ReadOnly = False
        Me.txtSTPer.Maxlength = 0
        Me.txtSTPer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSTPer.MultiLine = False
        Me.txtSTPer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSTPer.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSTPer.TabStop = True
        Me.txtSTPer.Visible = True
        Me.txtSTPer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSTPer.Name = "txtSTPer"
        Me.txtSHECessPer.AutoSize = False
        Me.txtSHECessPer.Size = New System.Drawing.Size(41, 19)
        Me.txtSHECessPer.Location = New System.Drawing.Point(327, 10)
        Me.txtSHECessPer.TabIndex = 30
        Me.txtSHECessPer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSHECessPer.AcceptsReturn = True
        Me.txtSHECessPer.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtSHECessPer.BackColor = System.Drawing.SystemColors.Window
        Me.txtSHECessPer.CausesValidation = True
        Me.txtSHECessPer.Enabled = True
        Me.txtSHECessPer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSHECessPer.HideSelection = True
        Me.txtSHECessPer.ReadOnly = False
        Me.txtSHECessPer.Maxlength = 0
        Me.txtSHECessPer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSHECessPer.MultiLine = False
        Me.txtSHECessPer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSHECessPer.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSHECessPer.TabStop = True
        Me.txtSHECessPer.Visible = True
        Me.txtSHECessPer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSHECessPer.Name = "txtSHECessPer"
        Me.txtCessPer.AutoSize = False
        Me.txtCessPer.Size = New System.Drawing.Size(41, 19)
        Me.txtCessPer.Location = New System.Drawing.Point(186, 10)
        Me.txtCessPer.TabIndex = 29
        Me.txtCessPer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCessPer.AcceptsReturn = True
        Me.txtCessPer.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtCessPer.BackColor = System.Drawing.SystemColors.Window
        Me.txtCessPer.CausesValidation = True
        Me.txtCessPer.Enabled = True
        Me.txtCessPer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCessPer.HideSelection = True
        Me.txtCessPer.ReadOnly = False
        Me.txtCessPer.Maxlength = 0
        Me.txtCessPer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCessPer.MultiLine = False
        Me.txtCessPer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCessPer.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCessPer.TabStop = True
        Me.txtCessPer.Visible = True
        Me.txtCessPer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCessPer.Name = "txtCessPer"
        Me.txtServPer.AutoSize = False
        Me.txtServPer.Size = New System.Drawing.Size(41, 19)
        Me.txtServPer.Location = New System.Drawing.Point(86, 10)
        Me.txtServPer.TabIndex = 28
        Me.txtServPer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtServPer.AcceptsReturn = True
        Me.txtServPer.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtServPer.BackColor = System.Drawing.SystemColors.Window
        Me.txtServPer.CausesValidation = True
        Me.txtServPer.Enabled = True
        Me.txtServPer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtServPer.HideSelection = True
        Me.txtServPer.ReadOnly = False
        Me.txtServPer.Maxlength = 0
        Me.txtServPer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtServPer.MultiLine = False
        Me.txtServPer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtServPer.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtServPer.TabStop = True
        Me.txtServPer.Visible = True
        Me.txtServPer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtServPer.Name = "txtServPer"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label12.Text = "K.K. Cess % :"
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Size = New System.Drawing.Size(77, 13)
        Me.Label12.Location = New System.Drawing.Point(520, 12)
        Me.Label12.TabIndex = 49
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Enabled = True
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.UseMnemonic = True
        Me.Label12.Visible = True
        Me.Label12.AutoSize = True
        Me.Label12.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Label12.Name = "Label12"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label11.Text = "S.B. Cess % :"
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Size = New System.Drawing.Size(77, 13)
        Me.Label11.Location = New System.Drawing.Point(377, 12)
        Me.Label11.TabIndex = 46
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Enabled = True
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.UseMnemonic = True
        Me.Label11.Visible = True
        Me.Label11.AutoSize = True
        Me.Label11.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Label11.Name = "Label11"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label10.Text = "%)"
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Size = New System.Drawing.Size(14, 13)
        Me.Label10.Location = New System.Drawing.Point(321, 32)
        Me.Label10.TabIndex = 42
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Enabled = True
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.UseMnemonic = True
        Me.Label10.Visible = True
        Me.Label10.AutoSize = True
        Me.Label10.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Label10.Name = "Label10"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label9.Text = "Bill Amount - ( Bill Amount  * "
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Size = New System.Drawing.Size(164, 13)
        Me.Label9.Location = New System.Drawing.Point(113, 34)
        Me.Label9.TabIndex = 41
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Enabled = True
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.UseMnemonic = True
        Me.Label9.Visible = True
        Me.Label9.AutoSize = True
        Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Label9.Name = "Label9"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label8.Text = "S.T. On Calc :"
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Size = New System.Drawing.Size(82, 13)
        Me.Label8.Location = New System.Drawing.Point(7, 34)
        Me.Label8.TabIndex = 40
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Enabled = True
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.UseMnemonic = True
        Me.Label8.Visible = True
        Me.Label8.AutoSize = True
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Label8.Name = "Label8"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label6.Text = "S.H.E. Cess % :"
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Size = New System.Drawing.Size(90, 13)
        Me.Label6.Location = New System.Drawing.Point(235, 12)
        Me.Label6.TabIndex = 36
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Enabled = True
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.UseMnemonic = True
        Me.Label6.Visible = True
        Me.Label6.AutoSize = True
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Label6.Name = "Label6"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label5.Text = "Cess % :"
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Size = New System.Drawing.Size(49, 13)
        Me.Label5.Location = New System.Drawing.Point(133, 12)
        Me.Label5.TabIndex = 35
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Enabled = True
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.UseMnemonic = True
        Me.Label5.Visible = True
        Me.Label5.AutoSize = True
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Label5.Name = "Label5"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label4.Text = "Serv. Tax % :"
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Size = New System.Drawing.Size(77, 13)
        Me.Label4.Location = New System.Drawing.Point(7, 12)
        Me.Label4.TabIndex = 34
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Enabled = True
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.UseMnemonic = True
        Me.Label4.Visible = True
        Me.Label4.AutoSize = True
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Label4.Name = "Label4"
        Me.Frame1.Size = New System.Drawing.Size(684, 36)
        Me.Frame1.Location = New System.Drawing.Point(0, -4)
        Me.Frame1.TabIndex = 0
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Enabled = True
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Visible = True
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.Name = "Frame1"
        Me.lblDC.Text = "lblDC"
        Me.lblDC.ForeColor = System.Drawing.Color.FromArgb(128, 0, 0)
        Me.lblDC.Size = New System.Drawing.Size(25, 20)
        Me.lblDC.Location = New System.Drawing.Point(654, 11)
        Me.lblDC.TabIndex = 6
        Me.lblDC.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDC.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblDC.BackColor = System.Drawing.SystemColors.Control
        Me.lblDC.Enabled = True
        Me.lblDC.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDC.UseMnemonic = True
        Me.lblDC.Visible = True
        Me.lblDC.AutoSize = False
        Me.lblDC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDC.Name = "lblDC"
        Me.lblAmount.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblAmount.Text = "lblAmount"
        Me.lblAmount.ForeColor = System.Drawing.Color.FromArgb(128, 0, 0)
        Me.lblAmount.Size = New System.Drawing.Size(118, 20)
        Me.lblAmount.Location = New System.Drawing.Point(536, 11)
        Me.lblAmount.TabIndex = 5
        Me.lblAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblAmount.Enabled = True
        Me.lblAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAmount.UseMnemonic = True
        Me.lblAmount.Visible = True
        Me.lblAmount.AutoSize = False
        Me.lblAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAmount.Name = "lblAmount"
        Me.Amount.Text = "Amount :"
        Me.Amount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Amount.Size = New System.Drawing.Size(51, 13)
        Me.Amount.Location = New System.Drawing.Point(433, 12)
        Me.Amount.TabIndex = 4
        Me.Amount.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Amount.BackColor = System.Drawing.SystemColors.Control
        Me.Amount.Enabled = True
        Me.Amount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Amount.Cursor = System.Windows.Forms.Cursors.Default
        Me.Amount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Amount.UseMnemonic = True
        Me.Amount.Visible = True
        Me.Amount.AutoSize = True
        Me.Amount.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Amount.Name = "Amount"
        Me.lblAccountName.Text = "lblAccountName"
        Me.lblAccountName.ForeColor = System.Drawing.Color.FromArgb(128, 0, 0)
        Me.lblAccountName.Size = New System.Drawing.Size(328, 20)
        Me.lblAccountName.Location = New System.Drawing.Point(101, 11)
        Me.lblAccountName.TabIndex = 3
        Me.lblAccountName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAccountName.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblAccountName.BackColor = System.Drawing.SystemColors.Control
        Me.lblAccountName.Enabled = True
        Me.lblAccountName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAccountName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAccountName.UseMnemonic = True
        Me.lblAccountName.Visible = True
        Me.lblAccountName.AutoSize = False
        Me.lblAccountName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAccountName.Name = "lblAccountName"
        Me.Label1.Text = "Account Name :"
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Size = New System.Drawing.Size(94, 13)
        Me.Label1.Location = New System.Drawing.Point(6, 12)
        Me.Label1.TabIndex = 2
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
        Me.Frame2.Size = New System.Drawing.Size(684, 325)
        Me.Frame2.Location = New System.Drawing.Point(0, 82)
        Me.Frame2.TabIndex = 1
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Enabled = True
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Visible = True
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.Name = "Frame2"
        SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(679, 313)
        Me.SprdMain.Location = New System.Drawing.Point(2, 8)
        Me.SprdMain.TabIndex = 33
        Me.SprdMain.Name = "SprdMain"
        Me.Frame3.Size = New System.Drawing.Size(684, 50)
        Me.Frame3.Location = New System.Drawing.Point(0, 403)
        Me.Frame3.TabIndex = 15
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Enabled = True
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Visible = True
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.Name = "Frame3"
        Me.cmdCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdCancel.Text = "&Cancel"
        Me.cmdCancel.Size = New System.Drawing.Size(60, 34)
        Me.cmdCancel.Location = New System.Drawing.Point(64, 10)
        Me.cmdCancel.Image = CType(resources.GetObject("cmdCancel.Image"), System.Drawing.Image)
        Me.cmdCancel.TabIndex = 26
        Me.ToolTip1.SetToolTip(Me.cmdCancel, "Close the form")
        Me.cmdCancel.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancel.CausesValidation = True
        Me.cmdCancel.Enabled = True
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.TabStop = True
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdOk.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdOk.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdOk.Text = "&Ok"
        Me.AcceptButton = Me.cmdOk
        Me.cmdOk.Size = New System.Drawing.Size(60, 34)
        Me.cmdOk.Location = New System.Drawing.Point(4, 10)
        Me.cmdOk.Image = CType(resources.GetObject("cmdOk.Image"), System.Drawing.Image)
        Me.cmdOk.TabIndex = 25
        Me.ToolTip1.SetToolTip(Me.cmdOk, "Save Voucher")
        Me.cmdOk.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOk.CausesValidation = True
        Me.cmdOk.Enabled = True
        Me.cmdOk.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOk.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOk.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOk.TabStop = True
        Me.cmdOk.Name = "cmdOk"
        Me.lblKKCessAmount.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblKKCessAmount.Text = "lblKKCessAmount"
        Me.lblKKCessAmount.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblKKCessAmount.ForeColor = System.Drawing.Color.FromArgb(128, 0, 0)
        Me.lblKKCessAmount.Size = New System.Drawing.Size(74, 17)
        Me.lblKKCessAmount.Location = New System.Drawing.Point(347, 26)
        Me.lblKKCessAmount.TabIndex = 51
        Me.lblKKCessAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblKKCessAmount.Enabled = True
        Me.lblKKCessAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblKKCessAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblKKCessAmount.UseMnemonic = True
        Me.lblKKCessAmount.Visible = True
        Me.lblKKCessAmount.AutoSize = False
        Me.lblKKCessAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblKKCessAmount.Name = "lblKKCessAmount"
        Me.Label14.Text = "KK  Cess:"
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Size = New System.Drawing.Size(56, 13)
        Me.Label14.Location = New System.Drawing.Point(292, 26)
        Me.Label14.TabIndex = 50
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Enabled = True
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.UseMnemonic = True
        Me.Label14.Visible = True
        Me.Label14.AutoSize = True
        Me.Label14.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Label14.Name = "Label14"
        Me.Label13.Text = "S.B. Tax :"
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Size = New System.Drawing.Size(58, 13)
        Me.Label13.Location = New System.Drawing.Point(132, 26)
        Me.Label13.TabIndex = 48
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Enabled = True
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.UseMnemonic = True
        Me.Label13.Visible = True
        Me.Label13.AutoSize = True
        Me.Label13.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Label13.Name = "Label13"
        Me.lblSBCessAmount.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblSBCessAmount.Text = "lblSBCessAmount"
        Me.lblSBCessAmount.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSBCessAmount.ForeColor = System.Drawing.Color.FromArgb(128, 0, 0)
        Me.lblSBCessAmount.Size = New System.Drawing.Size(74, 17)
        Me.lblSBCessAmount.Location = New System.Drawing.Point(213, 26)
        Me.lblSBCessAmount.TabIndex = 47
        Me.lblSBCessAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblSBCessAmount.Enabled = True
        Me.lblSBCessAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSBCessAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSBCessAmount.UseMnemonic = True
        Me.lblSBCessAmount.Visible = True
        Me.lblSBCessAmount.AutoSize = False
        Me.lblSBCessAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSBCessAmount.Name = "lblSBCessAmount"
        Me.lblBillAmount.Text = "0"
        Me.lblBillAmount.Size = New System.Drawing.Size(63, 17)
        Me.lblBillAmount.Location = New System.Drawing.Point(138, 28)
        Me.lblBillAmount.TabIndex = 44
        Me.lblBillAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBillAmount.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblBillAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblBillAmount.Enabled = True
        Me.lblBillAmount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBillAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBillAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBillAmount.UseMnemonic = True
        Me.lblBillAmount.Visible = True
        Me.lblBillAmount.AutoSize = False
        Me.lblBillAmount.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblBillAmount.Name = "lblBillAmount"
        Me.lblSHECessAmt.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblSHECessAmt.Text = "lblSHECessAmt"
        Me.lblSHECessAmt.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSHECessAmt.ForeColor = System.Drawing.Color.FromArgb(128, 0, 0)
        Me.lblSHECessAmt.Size = New System.Drawing.Size(74, 17)
        Me.lblSHECessAmt.Location = New System.Drawing.Point(489, 9)
        Me.lblSHECessAmt.TabIndex = 38
        Me.lblSHECessAmt.BackColor = System.Drawing.SystemColors.Control
        Me.lblSHECessAmt.Enabled = True
        Me.lblSHECessAmt.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSHECessAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSHECessAmt.UseMnemonic = True
        Me.lblSHECessAmt.Visible = True
        Me.lblSHECessAmt.AutoSize = False
        Me.lblSHECessAmt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSHECessAmt.Name = "lblSHECessAmt"
        Me.Label7.Text = "SHECess :"
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Size = New System.Drawing.Size(61, 13)
        Me.Label7.Location = New System.Drawing.Point(426, 9)
        Me.Label7.TabIndex = 37
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Enabled = True
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.UseMnemonic = True
        Me.Label7.Visible = True
        Me.Label7.AutoSize = True
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Label7.Name = "Label7"
        Me.Label3.Text = "Cess :"
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Size = New System.Drawing.Size(36, 13)
        Me.Label3.Location = New System.Drawing.Point(292, 9)
        Me.Label3.TabIndex = 24
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Enabled = True
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.UseMnemonic = True
        Me.Label3.Visible = True
        Me.Label3.AutoSize = True
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Label3.Name = "Label3"
        Me.LblNetAmt.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.LblNetAmt.Text = "LblNetAmt"
        Me.LblNetAmt.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNetAmt.ForeColor = System.Drawing.Color.FromArgb(128, 0, 0)
        Me.LblNetAmt.Size = New System.Drawing.Size(74, 17)
        Me.LblNetAmt.Location = New System.Drawing.Point(605, 9)
        Me.LblNetAmt.TabIndex = 22
        Me.LblNetAmt.BackColor = System.Drawing.SystemColors.Control
        Me.LblNetAmt.Enabled = True
        Me.LblNetAmt.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblNetAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblNetAmt.UseMnemonic = True
        Me.LblNetAmt.Visible = True
        Me.LblNetAmt.AutoSize = False
        Me.LblNetAmt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblNetAmt.Name = "LblNetAmt"
        Me.LblNet.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.LblNet.Text = "Net :"
        Me.LblNet.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNet.ForeColor = System.Drawing.Color.Black
        Me.LblNet.Size = New System.Drawing.Size(31, 13)
        Me.LblNet.Location = New System.Drawing.Point(571, 11)
        Me.LblNet.TabIndex = 21
        Me.LblNet.BackColor = System.Drawing.SystemColors.Control
        Me.LblNet.Enabled = True
        Me.LblNet.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblNet.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblNet.UseMnemonic = True
        Me.LblNet.Visible = True
        Me.LblNet.AutoSize = True
        Me.LblNet.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.LblNet.Name = "LblNet"
        Me.lblCessAmt.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblCessAmt.Text = "lblCessAmt"
        Me.lblCessAmt.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCessAmt.ForeColor = System.Drawing.Color.FromArgb(128, 0, 0)
        Me.lblCessAmt.Size = New System.Drawing.Size(74, 17)
        Me.lblCessAmt.Location = New System.Drawing.Point(347, 9)
        Me.lblCessAmt.TabIndex = 20
        Me.lblCessAmt.BackColor = System.Drawing.SystemColors.Control
        Me.lblCessAmt.Enabled = True
        Me.lblCessAmt.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCessAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCessAmt.UseMnemonic = True
        Me.lblCessAmt.Visible = True
        Me.lblCessAmt.AutoSize = False
        Me.lblCessAmt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCessAmt.Name = "lblCessAmt"
        Me.lblServiceTaxAmt.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblServiceTaxAmt.Text = "lblServiceTaxAmt"
        Me.lblServiceTaxAmt.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblServiceTaxAmt.ForeColor = System.Drawing.Color.FromArgb(128, 0, 0)
        Me.lblServiceTaxAmt.Size = New System.Drawing.Size(74, 17)
        Me.lblServiceTaxAmt.Location = New System.Drawing.Point(213, 9)
        Me.lblServiceTaxAmt.TabIndex = 19
        Me.lblServiceTaxAmt.BackColor = System.Drawing.SystemColors.Control
        Me.lblServiceTaxAmt.Enabled = True
        Me.lblServiceTaxAmt.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblServiceTaxAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblServiceTaxAmt.UseMnemonic = True
        Me.lblServiceTaxAmt.Visible = True
        Me.lblServiceTaxAmt.AutoSize = False
        Me.lblServiceTaxAmt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblServiceTaxAmt.Name = "lblServiceTaxAmt"
        Me.LblTotal.Text = "Service Tax :"
        Me.LblTotal.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotal.ForeColor = System.Drawing.Color.Black
        Me.LblTotal.Size = New System.Drawing.Size(77, 13)
        Me.LblTotal.Location = New System.Drawing.Point(132, 9)
        Me.LblTotal.TabIndex = 18
        Me.LblTotal.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.LblTotal.BackColor = System.Drawing.SystemColors.Control
        Me.LblTotal.Enabled = True
        Me.LblTotal.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblTotal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblTotal.UseMnemonic = True
        Me.LblTotal.Visible = True
        Me.LblTotal.AutoSize = True
        Me.LblTotal.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.LblTotal.Name = "LblTotal"
        Me.Label2.Text = "Diff :"
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Size = New System.Drawing.Size(29, 13)
        Me.Label2.Location = New System.Drawing.Point(571, 29)
        Me.Label2.TabIndex = 17
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
        Me.lblDiffAmt.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblDiffAmt.Text = "lblDiffAmt"
        Me.lblDiffAmt.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDiffAmt.ForeColor = System.Drawing.Color.FromARGB(128, 0, 0)
        Me.lblDiffAmt.Size = New System.Drawing.Size(74, 17)
        Me.lblDiffAmt.Location = New System.Drawing.Point(605, 27)
        Me.lblDiffAmt.TabIndex = 16
        Me.lblDiffAmt.BackColor = System.Drawing.SystemColors.Control
        Me.lblDiffAmt.Enabled = True
        Me.lblDiffAmt.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDiffAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDiffAmt.UseMnemonic = True
        Me.lblDiffAmt.Visible = True
        Me.lblDiffAmt.AutoSize = False
        Me.lblDiffAmt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDiffAmt.Name = "lblDiffAmt"
        Me.lblTrnRowNo.Text = "lblTrnRowNo"
        Me.lblTrnRowNo.Size = New System.Drawing.Size(62, 13)
        Me.lblTrnRowNo.Location = New System.Drawing.Point(436, 0)
        Me.lblTrnRowNo.TabIndex = 23
        Me.lblTrnRowNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTrnRowNo.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblTrnRowNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblTrnRowNo.Enabled = True
        Me.lblTrnRowNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTrnRowNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTrnRowNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTrnRowNo.UseMnemonic = True
        Me.lblTrnRowNo.Visible = True
        Me.lblTrnRowNo.AutoSize = True
        Me.lblTrnRowNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblTrnRowNo.Name = "lblTrnRowNo"
        Me.lblAccountCode.Text = "lblAccountCode"
        Me.lblAccountCode.Size = New System.Drawing.Size(64, 16)
        Me.lblAccountCode.Location = New System.Drawing.Point(0, 18)
        Me.lblAccountCode.TabIndex = 14
        Me.lblAccountCode.Visible = False
        Me.lblAccountCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAccountCode.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblAccountCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblAccountCode.Enabled = True
        Me.lblAccountCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAccountCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAccountCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAccountCode.UseMnemonic = True
        Me.lblAccountCode.AutoSize = False
        Me.lblAccountCode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblAccountCode.Name = "lblAccountCode"
        Me.lblADDMode.Text = "lblADDMode"
        Me.lblADDMode.Size = New System.Drawing.Size(67, 13)
        Me.lblADDMode.Location = New System.Drawing.Point(285, 0)
        Me.lblADDMode.TabIndex = 13
        Me.lblADDMode.Visible = False
        Me.lblADDMode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblADDMode.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblADDMode.BackColor = System.Drawing.SystemColors.Control
        Me.lblADDMode.Enabled = True
        Me.lblADDMode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblADDMode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblADDMode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblADDMode.UseMnemonic = True
        Me.lblADDMode.AutoSize = False
        Me.lblADDMode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblADDMode.Name = "lblADDMode"
        Me.lblModifyMode.Text = "lblModifyMode"
        Me.lblModifyMode.Size = New System.Drawing.Size(73, 13)
        Me.lblModifyMode.Location = New System.Drawing.Point(355, 0)
        Me.lblModifyMode.TabIndex = 12
        Me.lblModifyMode.Visible = False
        Me.lblModifyMode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModifyMode.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblModifyMode.BackColor = System.Drawing.SystemColors.Control
        Me.lblModifyMode.Enabled = True
        Me.lblModifyMode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblModifyMode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblModifyMode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblModifyMode.UseMnemonic = True
        Me.lblModifyMode.AutoSize = False
        Me.lblModifyMode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblModifyMode.Name = "lblModifyMode"
        Me.lblBillNo.Text = "lblBilNo"
        Me.lblBillNo.Size = New System.Drawing.Size(47, 17)
        Me.lblBillNo.Location = New System.Drawing.Point(52, -1)
        Me.lblBillNo.TabIndex = 11
        Me.lblBillNo.Visible = False
        Me.lblBillNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBillNo.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblBillNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblBillNo.Enabled = True
        Me.lblBillNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBillNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBillNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBillNo.UseMnemonic = True
        Me.lblBillNo.AutoSize = False
        Me.lblBillNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblBillNo.Name = "lblBillNo"
        Me.lblBillYear.Text = "lblBillYear"
        Me.lblBillYear.Size = New System.Drawing.Size(51, 17)
        Me.lblBillYear.Location = New System.Drawing.Point(104, -1)
        Me.lblBillYear.TabIndex = 10
        Me.lblBillYear.Visible = False
        Me.lblBillYear.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBillYear.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblBillYear.BackColor = System.Drawing.SystemColors.Control
        Me.lblBillYear.Enabled = True
        Me.lblBillYear.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBillYear.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBillYear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBillYear.UseMnemonic = True
        Me.lblBillYear.AutoSize = False
        Me.lblBillYear.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblBillYear.Name = "lblBillYear"
        Me.lblVDate.Text = "lblVDate"
        Me.lblVDate.Size = New System.Drawing.Size(55, 17)
        Me.lblVDate.Location = New System.Drawing.Point(226, -1)
        Me.lblVDate.TabIndex = 9
        Me.lblVDate.Visible = False
        Me.lblVDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVDate.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblVDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblVDate.Enabled = True
        Me.lblVDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblVDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblVDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblVDate.UseMnemonic = True
        Me.lblVDate.AutoSize = False
        Me.lblVDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblVDate.Name = "lblVDate"
        Me.lblNarration.Text = "lblNarration"
        Me.lblNarration.Size = New System.Drawing.Size(59, 17)
        Me.lblNarration.Location = New System.Drawing.Point(162, -1)
        Me.lblNarration.TabIndex = 8
        Me.lblNarration.Visible = False
        Me.lblNarration.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNarration.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblNarration.BackColor = System.Drawing.SystemColors.Control
        Me.lblNarration.Enabled = True
        Me.lblNarration.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblNarration.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblNarration.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblNarration.UseMnemonic = True
        Me.lblNarration.AutoSize = False
        Me.lblNarration.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblNarration.Name = "lblNarration"
        Me.lblBookType.Text = "lblBType"
        Me.lblBookType.Size = New System.Drawing.Size(45, 15)
        Me.lblBookType.Location = New System.Drawing.Point(0, 0)
        Me.lblBookType.TabIndex = 7
        Me.lblBookType.Visible = False
        Me.lblBookType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Enabled = True
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.UseMnemonic = True
        Me.lblBookType.AutoSize = False
        Me.lblBookType.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblBookType.Name = "lblBookType"
        Me.Controls.Add(fraserviceInfo)
        Me.Controls.Add(Frame1)
        Me.Controls.Add(Frame2)
        Me.Controls.Add(Frame3)
        Me.Controls.Add(lblTrnRowNo)
        Me.Controls.Add(lblAccountCode)
        Me.Controls.Add(lblADDMode)
        Me.Controls.Add(lblModifyMode)
        Me.Controls.Add(lblBillNo)
        Me.Controls.Add(lblBillYear)
        Me.Controls.Add(lblVDate)
        Me.Controls.Add(lblNarration)
        Me.Controls.Add(lblBookType)
        Me.fraserviceInfo.Controls.Add(txtKKCessPer)
        Me.fraserviceInfo.Controls.Add(txtSBCessPer)
        Me.fraserviceInfo.Controls.Add(cmdRefresh)
        Me.fraserviceInfo.Controls.Add(cmdCalc)
        Me.fraserviceInfo.Controls.Add(txtSTPer)
        Me.fraserviceInfo.Controls.Add(txtSHECessPer)
        Me.fraserviceInfo.Controls.Add(txtCessPer)
        Me.fraserviceInfo.Controls.Add(txtServPer)
        Me.fraserviceInfo.Controls.Add(Label12)
        Me.fraserviceInfo.Controls.Add(Label11)
        Me.fraserviceInfo.Controls.Add(Label10)
        Me.fraserviceInfo.Controls.Add(Label9)
        Me.fraserviceInfo.Controls.Add(Label8)
        Me.fraserviceInfo.Controls.Add(Label6)
        Me.fraserviceInfo.Controls.Add(Label5)
        Me.fraserviceInfo.Controls.Add(Label4)
        Me.Frame1.Controls.Add(lblDC)
        Me.Frame1.Controls.Add(lblAmount)
        Me.Frame1.Controls.Add(Amount)
        Me.Frame1.Controls.Add(lblAccountName)
        Me.Frame1.Controls.Add(Label1)
        Me.Frame2.Controls.Add(SprdMain)
        Me.Frame3.Controls.Add(cmdCancel)
        Me.Frame3.Controls.Add(cmdOk)
        Me.Frame3.Controls.Add(lblKKCessAmount)
        Me.Frame3.Controls.Add(Label14)
        Me.Frame3.Controls.Add(Label13)
        Me.Frame3.Controls.Add(lblSBCessAmount)
        Me.Frame3.Controls.Add(lblBillAmount)
        Me.Frame3.Controls.Add(lblSHECessAmt)
        Me.Frame3.Controls.Add(Label7)
        Me.Frame3.Controls.Add(Label3)
        Me.Frame3.Controls.Add(LblNetAmt)
        Me.Frame3.Controls.Add(LblNet)
        Me.Frame3.Controls.Add(lblCessAmt)
        Me.Frame3.Controls.Add(lblServiceTaxAmt)
        Me.Frame3.Controls.Add(LblTotal)
        Me.Frame3.Controls.Add(Label2)
        Me.Frame3.Controls.Add(lblDiffAmt)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraserviceInfo.ResumeLayout(False)
        Me.Frame1.ResumeLayout(False)
        Me.Frame2.ResumeLayout(False)
        Me.Frame3.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub
#End Region
End Class