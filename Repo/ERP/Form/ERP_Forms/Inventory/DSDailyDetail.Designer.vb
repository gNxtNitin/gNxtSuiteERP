Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmDSDailyDetail
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
    Public WithEvents txtMonthDeliverySchedule As System.Windows.Forms.TextBox
    Public WithEvents cmd2Month As System.Windows.Forms.Button
    Public WithEvents cmd2Week As System.Windows.Forms.Button
    Public WithEvents cmd1Week As System.Windows.Forms.Button
    Public WithEvents CmdDaily As System.Windows.Forms.Button
    Public WithEvents SprdDlv As AxFPSpreadADO.AxfpSpread
    Public WithEvents lblWeek5Qty As System.Windows.Forms.Label
    Public WithEvents lblWeek4Qty As System.Windows.Forms.Label
    Public WithEvents lblWeek3Qty As System.Windows.Forms.Label
    Public WithEvents lblWeek2Qty As System.Windows.Forms.Label
    Public WithEvents lblWeek1Qty As System.Windows.Forms.Label
    Public WithEvents lblWeek5 As System.Windows.Forms.Label
    Public WithEvents lblWeek4 As System.Windows.Forms.Label
    Public WithEvents lblWeek3 As System.Windows.Forms.Label
    Public WithEvents lblWeek2 As System.Windows.Forms.Label
    Public WithEvents lblWeek1 As System.Windows.Forms.Label
    Public WithEvents lbl1 As System.Windows.Forms.Label
    Public WithEvents lblActual As System.Windows.Forms.Label
    Public WithEvents lblPlanQty As System.Windows.Forms.Label
    Public WithEvents lblTotal As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents cmdOk As System.Windows.Forms.Button
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents lblMainActiveRow As System.Windows.Forms.Label
    Public WithEvents lblSuppCode As System.Windows.Forms.Label
    Public WithEvents lblItemCode As System.Windows.Forms.Label
    Public WithEvents lblPoNo As System.Windows.Forms.Label
    Public WithEvents LblModifyMode As System.Windows.Forms.Label
    Public WithEvents LblAddMode As System.Windows.Forms.Label
    Public WithEvents LblPODate As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(FrmDSDailyDetail))
        Me.components = New System.ComponentModel.Container()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
        Me.Frame2 = New System.Windows.Forms.GroupBox
        Me.txtMonthDeliverySchedule = New System.Windows.Forms.TextBox
        Me.cmd2Month = New System.Windows.Forms.Button
        Me.cmd2Week = New System.Windows.Forms.Button
        Me.cmd1Week = New System.Windows.Forms.Button
        Me.CmdDaily = New System.Windows.Forms.Button
        Me.SprdDlv = New AxFPSpreadADO.AxfpSpread
        Me.lblWeek5Qty = New System.Windows.Forms.Label
        Me.lblWeek4Qty = New System.Windows.Forms.Label
        Me.lblWeek3Qty = New System.Windows.Forms.Label
        Me.lblWeek2Qty = New System.Windows.Forms.Label
        Me.lblWeek1Qty = New System.Windows.Forms.Label
        Me.lblWeek5 = New System.Windows.Forms.Label
        Me.lblWeek4 = New System.Windows.Forms.Label
        Me.lblWeek3 = New System.Windows.Forms.Label
        Me.lblWeek2 = New System.Windows.Forms.Label
        Me.lblWeek1 = New System.Windows.Forms.Label
        Me.lbl1 = New System.Windows.Forms.Label
        Me.lblActual = New System.Windows.Forms.Label
        Me.lblPlanQty = New System.Windows.Forms.Label
        Me.lblTotal = New System.Windows.Forms.Label
        Me.Frame3 = New System.Windows.Forms.GroupBox
        Me.cmdOk = New System.Windows.Forms.Button
        Me.CmdClose = New System.Windows.Forms.Button
        Me.lblMainActiveRow = New System.Windows.Forms.Label
        Me.lblSuppCode = New System.Windows.Forms.Label
        Me.lblItemCode = New System.Windows.Forms.Label
        Me.lblPoNo = New System.Windows.Forms.Label
        Me.LblModifyMode = New System.Windows.Forms.Label
        Me.LblAddMode = New System.Windows.Forms.Label
        Me.LblPODate = New System.Windows.Forms.Label
        Me.Frame2.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.SuspendLayout()
        Me.ToolTip1.Active = True
        CType(Me.SprdDlv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Text = "Daily Delivery Schedule Details"
        Me.ClientSize = New System.Drawing.Size(528, 370)
        Me.Location = New System.Drawing.Point(3, 22)
        Me.ControlBox = False
        Me.Icon = CType(resources.GetObject("FrmDSDailyDetail.Icon"), System.Drawing.Icon)
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
        Me.Name = "FrmDSDailyDetail"
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
        Me.txtMonthDeliverySchedule.AutoSize = False
        Me.txtMonthDeliverySchedule.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMonthDeliverySchedule.Size = New System.Drawing.Size(117, 25)
        Me.txtMonthDeliverySchedule.Location = New System.Drawing.Point(14, 34)
        Me.txtMonthDeliverySchedule.TabIndex = 29
        Me.txtMonthDeliverySchedule.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMonthDeliverySchedule.AcceptsReturn = True
        Me.txtMonthDeliverySchedule.BackColor = System.Drawing.SystemColors.Window
        Me.txtMonthDeliverySchedule.CausesValidation = True
        Me.txtMonthDeliverySchedule.Enabled = True
        Me.txtMonthDeliverySchedule.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMonthDeliverySchedule.HideSelection = True
        Me.txtMonthDeliverySchedule.ReadOnly = False
        Me.txtMonthDeliverySchedule.MaxLength = 0
        Me.txtMonthDeliverySchedule.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMonthDeliverySchedule.Multiline = False
        Me.txtMonthDeliverySchedule.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMonthDeliverySchedule.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtMonthDeliverySchedule.TabStop = True
        Me.txtMonthDeliverySchedule.Visible = True
        Me.txtMonthDeliverySchedule.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.txtMonthDeliverySchedule.Name = "txtMonthDeliverySchedule"
        Me.cmd2Month.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmd2Month.Text = "2's Month"
        Me.cmd2Month.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd2Month.Size = New System.Drawing.Size(125, 29)
        Me.cmd2Month.Location = New System.Drawing.Point(10, 154)
        Me.cmd2Month.TabIndex = 17
        Me.cmd2Month.BackColor = System.Drawing.SystemColors.Control
        Me.cmd2Month.CausesValidation = True
        Me.cmd2Month.Enabled = True
        Me.cmd2Month.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmd2Month.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmd2Month.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmd2Month.TabStop = True
        Me.cmd2Month.Name = "cmd2Month"
        Me.cmd2Week.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmd2Week.Text = "2's Week"
        Me.cmd2Week.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd2Week.Size = New System.Drawing.Size(125, 29)
        Me.cmd2Week.Location = New System.Drawing.Point(10, 126)
        Me.cmd2Week.TabIndex = 16
        Me.cmd2Week.BackColor = System.Drawing.SystemColors.Control
        Me.cmd2Week.CausesValidation = True
        Me.cmd2Week.Enabled = True
        Me.cmd2Week.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmd2Week.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmd2Week.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmd2Week.TabStop = True
        Me.cmd2Week.Name = "cmd2Week"
        Me.cmd1Week.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmd1Week.Text = "1's Week"
        Me.cmd1Week.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd1Week.Size = New System.Drawing.Size(125, 29)
        Me.cmd1Week.Location = New System.Drawing.Point(10, 98)
        Me.cmd1Week.TabIndex = 15
        Me.cmd1Week.BackColor = System.Drawing.SystemColors.Control
        Me.cmd1Week.CausesValidation = True
        Me.cmd1Week.Enabled = True
        Me.cmd1Week.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmd1Week.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmd1Week.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmd1Week.TabStop = True
        Me.cmd1Week.Name = "cmd1Week"
        Me.CmdDaily.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdDaily.Text = "Daily"
        Me.CmdDaily.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdDaily.Size = New System.Drawing.Size(125, 29)
        Me.CmdDaily.Location = New System.Drawing.Point(10, 70)
        Me.CmdDaily.TabIndex = 14
        Me.CmdDaily.BackColor = System.Drawing.SystemColors.Control
        Me.CmdDaily.CausesValidation = True
        Me.CmdDaily.Enabled = True
        Me.CmdDaily.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdDaily.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdDaily.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDaily.TabStop = True
        Me.CmdDaily.Name = "CmdDaily"
        SprdDlv.OcxState = CType(resources.GetObject("SprdDlv.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdDlv.Size = New System.Drawing.Size(381, 295)
        Me.SprdDlv.Location = New System.Drawing.Point(142, 8)
        Me.SprdDlv.TabIndex = 30
        Me.SprdDlv.Name = "SprdDlv"
        Me.lblWeek5Qty.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblWeek5Qty.Text = "Week 5 :"
        Me.lblWeek5Qty.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWeek5Qty.Size = New System.Drawing.Size(53, 13)
        Me.lblWeek5Qty.Location = New System.Drawing.Point(8, 286)
        Me.lblWeek5Qty.TabIndex = 27
        Me.lblWeek5Qty.BackColor = System.Drawing.SystemColors.Control
        Me.lblWeek5Qty.Enabled = True
        Me.lblWeek5Qty.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblWeek5Qty.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblWeek5Qty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblWeek5Qty.UseMnemonic = True
        Me.lblWeek5Qty.Visible = True
        Me.lblWeek5Qty.AutoSize = True
        Me.lblWeek5Qty.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblWeek5Qty.Name = "lblWeek5Qty"
        Me.lblWeek4Qty.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblWeek4Qty.Text = "Week 4 :"
        Me.lblWeek4Qty.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWeek4Qty.Size = New System.Drawing.Size(53, 13)
        Me.lblWeek4Qty.Location = New System.Drawing.Point(8, 264)
        Me.lblWeek4Qty.TabIndex = 26
        Me.lblWeek4Qty.BackColor = System.Drawing.SystemColors.Control
        Me.lblWeek4Qty.Enabled = True
        Me.lblWeek4Qty.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblWeek4Qty.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblWeek4Qty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblWeek4Qty.UseMnemonic = True
        Me.lblWeek4Qty.Visible = True
        Me.lblWeek4Qty.AutoSize = True
        Me.lblWeek4Qty.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblWeek4Qty.Name = "lblWeek4Qty"
        Me.lblWeek3Qty.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblWeek3Qty.Text = "Week 3 :"
        Me.lblWeek3Qty.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWeek3Qty.Size = New System.Drawing.Size(53, 13)
        Me.lblWeek3Qty.Location = New System.Drawing.Point(8, 244)
        Me.lblWeek3Qty.TabIndex = 25
        Me.lblWeek3Qty.BackColor = System.Drawing.SystemColors.Control
        Me.lblWeek3Qty.Enabled = True
        Me.lblWeek3Qty.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblWeek3Qty.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblWeek3Qty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblWeek3Qty.UseMnemonic = True
        Me.lblWeek3Qty.Visible = True
        Me.lblWeek3Qty.AutoSize = True
        Me.lblWeek3Qty.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblWeek3Qty.Name = "lblWeek3Qty"
        Me.lblWeek2Qty.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblWeek2Qty.Text = "Week 2 :"
        Me.lblWeek2Qty.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWeek2Qty.Size = New System.Drawing.Size(53, 13)
        Me.lblWeek2Qty.Location = New System.Drawing.Point(8, 220)
        Me.lblWeek2Qty.TabIndex = 24
        Me.lblWeek2Qty.BackColor = System.Drawing.SystemColors.Control
        Me.lblWeek2Qty.Enabled = True
        Me.lblWeek2Qty.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblWeek2Qty.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblWeek2Qty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblWeek2Qty.UseMnemonic = True
        Me.lblWeek2Qty.Visible = True
        Me.lblWeek2Qty.AutoSize = True
        Me.lblWeek2Qty.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblWeek2Qty.Name = "lblWeek2Qty"
        Me.lblWeek1Qty.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblWeek1Qty.Text = "Week 1 :"
        Me.lblWeek1Qty.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWeek1Qty.Size = New System.Drawing.Size(53, 13)
        Me.lblWeek1Qty.Location = New System.Drawing.Point(8, 198)
        Me.lblWeek1Qty.TabIndex = 23
        Me.lblWeek1Qty.BackColor = System.Drawing.SystemColors.Control
        Me.lblWeek1Qty.Enabled = True
        Me.lblWeek1Qty.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblWeek1Qty.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblWeek1Qty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblWeek1Qty.UseMnemonic = True
        Me.lblWeek1Qty.Visible = True
        Me.lblWeek1Qty.AutoSize = True
        Me.lblWeek1Qty.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblWeek1Qty.Name = "lblWeek1Qty"
        Me.lblWeek5.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblWeek5.Text = "0.00"
        Me.lblWeek5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWeek5.Size = New System.Drawing.Size(70, 17)
        Me.lblWeek5.Location = New System.Drawing.Point(64, 284)
        Me.lblWeek5.TabIndex = 22
        Me.lblWeek5.BackColor = System.Drawing.SystemColors.Control
        Me.lblWeek5.Enabled = True
        Me.lblWeek5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblWeek5.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblWeek5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblWeek5.UseMnemonic = True
        Me.lblWeek5.Visible = True
        Me.lblWeek5.AutoSize = False
        Me.lblWeek5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblWeek5.Name = "lblWeek5"
        Me.lblWeek4.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblWeek4.Text = "0.00"
        Me.lblWeek4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWeek4.Size = New System.Drawing.Size(70, 17)
        Me.lblWeek4.Location = New System.Drawing.Point(64, 262)
        Me.lblWeek4.TabIndex = 21
        Me.lblWeek4.BackColor = System.Drawing.SystemColors.Control
        Me.lblWeek4.Enabled = True
        Me.lblWeek4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblWeek4.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblWeek4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblWeek4.UseMnemonic = True
        Me.lblWeek4.Visible = True
        Me.lblWeek4.AutoSize = False
        Me.lblWeek4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblWeek4.Name = "lblWeek4"
        Me.lblWeek3.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblWeek3.Text = "0.00"
        Me.lblWeek3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWeek3.Size = New System.Drawing.Size(70, 17)
        Me.lblWeek3.Location = New System.Drawing.Point(64, 240)
        Me.lblWeek3.TabIndex = 20
        Me.lblWeek3.BackColor = System.Drawing.SystemColors.Control
        Me.lblWeek3.Enabled = True
        Me.lblWeek3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblWeek3.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblWeek3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblWeek3.UseMnemonic = True
        Me.lblWeek3.Visible = True
        Me.lblWeek3.AutoSize = False
        Me.lblWeek3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblWeek3.Name = "lblWeek3"
        Me.lblWeek2.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblWeek2.Text = "0.00"
        Me.lblWeek2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWeek2.Size = New System.Drawing.Size(70, 17)
        Me.lblWeek2.Location = New System.Drawing.Point(64, 218)
        Me.lblWeek2.TabIndex = 19
        Me.lblWeek2.BackColor = System.Drawing.SystemColors.Control
        Me.lblWeek2.Enabled = True
        Me.lblWeek2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblWeek2.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblWeek2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblWeek2.UseMnemonic = True
        Me.lblWeek2.Visible = True
        Me.lblWeek2.AutoSize = False
        Me.lblWeek2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblWeek2.Name = "lblWeek2"
        Me.lblWeek1.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblWeek1.Text = "0.00"
        Me.lblWeek1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWeek1.Size = New System.Drawing.Size(70, 17)
        Me.lblWeek1.Location = New System.Drawing.Point(64, 196)
        Me.lblWeek1.TabIndex = 18
        Me.lblWeek1.BackColor = System.Drawing.SystemColors.Control
        Me.lblWeek1.Enabled = True
        Me.lblWeek1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblWeek1.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblWeek1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblWeek1.UseMnemonic = True
        Me.lblWeek1.Visible = True
        Me.lblWeek1.AutoSize = False
        Me.lblWeek1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblWeek1.Name = "lblWeek1"
        Me.lbl1.Size = New System.Drawing.Size(117, 23)
        Me.lbl1.Location = New System.Drawing.Point(14, 10)
        Me.lbl1.TabIndex = 13
        Me.lbl1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl1.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lbl1.BackColor = System.Drawing.SystemColors.Control
        Me.lbl1.Enabled = True
        Me.lbl1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbl1.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbl1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbl1.UseMnemonic = True
        Me.lbl1.Visible = True
        Me.lbl1.AutoSize = False
        Me.lbl1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbl1.Name = "lbl1"
        Me.lblActual.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblActual.Text = "0.00"
        Me.lblActual.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblActual.Size = New System.Drawing.Size(85, 19)
        Me.lblActual.Location = New System.Drawing.Point(418, 306)
        Me.lblActual.TabIndex = 12
        Me.lblActual.BackColor = System.Drawing.SystemColors.Control
        Me.lblActual.Enabled = True
        Me.lblActual.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblActual.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblActual.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblActual.UseMnemonic = True
        Me.lblActual.Visible = True
        Me.lblActual.AutoSize = False
        Me.lblActual.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblActual.Name = "lblActual"
        Me.lblPlanQty.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblPlanQty.Text = "0.00"
        Me.lblPlanQty.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPlanQty.Size = New System.Drawing.Size(85, 19)
        Me.lblPlanQty.Location = New System.Drawing.Point(332, 306)
        Me.lblPlanQty.TabIndex = 11
        Me.lblPlanQty.BackColor = System.Drawing.SystemColors.Control
        Me.lblPlanQty.Enabled = True
        Me.lblPlanQty.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPlanQty.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPlanQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPlanQty.UseMnemonic = True
        Me.lblPlanQty.Visible = True
        Me.lblPlanQty.AutoSize = False
        Me.lblPlanQty.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPlanQty.Name = "lblPlanQty"
        Me.lblTotal.Text = "Total :"
        Me.lblTotal.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotal.Size = New System.Drawing.Size(38, 13)
        Me.lblTotal.Location = New System.Drawing.Point(292, 308)
        Me.lblTotal.TabIndex = 10
        Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblTotal.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotal.Enabled = True
        Me.lblTotal.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTotal.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotal.UseMnemonic = True
        Me.lblTotal.Visible = True
        Me.lblTotal.AutoSize = True
        Me.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblTotal.Name = "lblTotal"
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
        Me.cmdOk.TabIndex = 7
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
        Me.CmdClose.Location = New System.Drawing.Point(462, 12)
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.TabIndex = 5
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
        Me.lblMainActiveRow.Text = "MainActiveRow"
        Me.lblMainActiveRow.Size = New System.Drawing.Size(45, 13)
        Me.lblMainActiveRow.Location = New System.Drawing.Point(412, 16)
        Me.lblMainActiveRow.TabIndex = 28
        Me.lblMainActiveRow.Visible = False
        Me.lblMainActiveRow.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMainActiveRow.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblMainActiveRow.BackColor = System.Drawing.SystemColors.Control
        Me.lblMainActiveRow.Enabled = True
        Me.lblMainActiveRow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMainActiveRow.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMainActiveRow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMainActiveRow.UseMnemonic = True
        Me.lblMainActiveRow.AutoSize = False
        Me.lblMainActiveRow.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblMainActiveRow.Name = "lblMainActiveRow"
        Me.lblSuppCode.Text = "lblSuppCode"
        Me.lblSuppCode.Size = New System.Drawing.Size(60, 13)
        Me.lblSuppCode.Location = New System.Drawing.Point(84, 30)
        Me.lblSuppCode.TabIndex = 9
        Me.lblSuppCode.Visible = False
        Me.lblSuppCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSuppCode.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblSuppCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblSuppCode.Enabled = True
        Me.lblSuppCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSuppCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSuppCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSuppCode.UseMnemonic = True
        Me.lblSuppCode.AutoSize = True
        Me.lblSuppCode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblSuppCode.Name = "lblSuppCode"
        Me.lblItemCode.Text = "lblItemCode"
        Me.lblItemCode.Size = New System.Drawing.Size(55, 13)
        Me.lblItemCode.Location = New System.Drawing.Point(86, 16)
        Me.lblItemCode.TabIndex = 8
        Me.lblItemCode.Visible = False
        Me.lblItemCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItemCode.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblItemCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblItemCode.Enabled = True
        Me.lblItemCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblItemCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblItemCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblItemCode.UseMnemonic = True
        Me.lblItemCode.AutoSize = True
        Me.lblItemCode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblItemCode.Name = "lblItemCode"
        Me.lblPoNo.Text = "PONo"
        Me.lblPoNo.Size = New System.Drawing.Size(29, 13)
        Me.lblPoNo.Location = New System.Drawing.Point(186, 28)
        Me.lblPoNo.TabIndex = 6
        Me.lblPoNo.Visible = False
        Me.lblPoNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPoNo.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblPoNo.BackColor = System.Drawing.Color.Transparent
        Me.lblPoNo.Enabled = True
        Me.lblPoNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPoNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPoNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPoNo.UseMnemonic = True
        Me.lblPoNo.AutoSize = True
        Me.lblPoNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblPoNo.Name = "lblPoNo"
        Me.LblModifyMode.Text = "LblModifyMode"
        Me.LblModifyMode.Size = New System.Drawing.Size(72, 13)
        Me.LblModifyMode.Location = New System.Drawing.Point(308, 14)
        Me.LblModifyMode.TabIndex = 4
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
        Me.LblAddMode.Location = New System.Drawing.Point(264, 22)
        Me.LblAddMode.TabIndex = 3
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
        Me.LblPODate.Text = "PODate"
        Me.LblPODate.Size = New System.Drawing.Size(38, 13)
        Me.LblPODate.Location = New System.Drawing.Point(186, 14)
        Me.LblPODate.TabIndex = 2
        Me.LblPODate.Visible = False
        Me.LblPODate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPODate.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.LblPODate.BackColor = System.Drawing.SystemColors.Control
        Me.LblPODate.Enabled = True
        Me.LblPODate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblPODate.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblPODate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblPODate.UseMnemonic = True
        Me.LblPODate.AutoSize = True
        Me.LblPODate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.LblPODate.Name = "LblPODate"
        Me.Controls.Add(Frame2)
        Me.Controls.Add(Frame3)
        Me.Frame2.Controls.Add(txtMonthDeliverySchedule)
        Me.Frame2.Controls.Add(cmd2Month)
        Me.Frame2.Controls.Add(cmd2Week)
        Me.Frame2.Controls.Add(cmd1Week)
        Me.Frame2.Controls.Add(CmdDaily)
        Me.Frame2.Controls.Add(SprdDlv)
        Me.Frame2.Controls.Add(lblWeek5Qty)
        Me.Frame2.Controls.Add(lblWeek4Qty)
        Me.Frame2.Controls.Add(lblWeek3Qty)
        Me.Frame2.Controls.Add(lblWeek2Qty)
        Me.Frame2.Controls.Add(lblWeek1Qty)
        Me.Frame2.Controls.Add(lblWeek5)
        Me.Frame2.Controls.Add(lblWeek4)
        Me.Frame2.Controls.Add(lblWeek3)
        Me.Frame2.Controls.Add(lblWeek2)
        Me.Frame2.Controls.Add(lblWeek1)
        Me.Frame2.Controls.Add(lbl1)
        Me.Frame2.Controls.Add(lblActual)
        Me.Frame2.Controls.Add(lblPlanQty)
        Me.Frame2.Controls.Add(lblTotal)
        Me.Frame3.Controls.Add(cmdOk)
        Me.Frame3.Controls.Add(CmdClose)
        Me.Frame3.Controls.Add(lblMainActiveRow)
        Me.Frame3.Controls.Add(lblSuppCode)
        Me.Frame3.Controls.Add(lblItemCode)
        Me.Frame3.Controls.Add(lblPoNo)
        Me.Frame3.Controls.Add(LblModifyMode)
        Me.Frame3.Controls.Add(LblAddMode)
        Me.Frame3.Controls.Add(LblPODate)
        CType(Me.SprdDlv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame2.ResumeLayout(False)
        Me.Frame3.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub
#End Region
End Class