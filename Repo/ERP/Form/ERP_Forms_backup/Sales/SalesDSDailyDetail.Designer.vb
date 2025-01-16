Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmSalesDSDailyDetail
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
    Public WithEvents cmdMonth As System.Windows.Forms.Button
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
    Public WithEvents LblTempSeq As System.Windows.Forms.Label
    Public WithEvents lblScheQty As System.Windows.Forms.Label
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSalesDSDailyDetail))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.cmdMonth = New System.Windows.Forms.Button()
        Me.txtMonthDeliverySchedule = New System.Windows.Forms.TextBox()
        Me.cmd2Month = New System.Windows.Forms.Button()
        Me.cmd2Week = New System.Windows.Forms.Button()
        Me.cmd1Week = New System.Windows.Forms.Button()
        Me.CmdDaily = New System.Windows.Forms.Button()
        Me.SprdDlv = New AxFPSpreadADO.AxfpSpread()
        Me.lblWeek5Qty = New System.Windows.Forms.Label()
        Me.lblWeek4Qty = New System.Windows.Forms.Label()
        Me.lblWeek3Qty = New System.Windows.Forms.Label()
        Me.lblWeek2Qty = New System.Windows.Forms.Label()
        Me.lblWeek1Qty = New System.Windows.Forms.Label()
        Me.lblWeek5 = New System.Windows.Forms.Label()
        Me.lblWeek4 = New System.Windows.Forms.Label()
        Me.lblWeek3 = New System.Windows.Forms.Label()
        Me.lblWeek2 = New System.Windows.Forms.Label()
        Me.lblWeek1 = New System.Windows.Forms.Label()
        Me.lbl1 = New System.Windows.Forms.Label()
        Me.lblActual = New System.Windows.Forms.Label()
        Me.lblPlanQty = New System.Windows.Forms.Label()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.lblDI = New System.Windows.Forms.Label()
        Me.lblStoreLoc = New System.Windows.Forms.Label()
        Me.LblTempSeq = New System.Windows.Forms.Label()
        Me.lblScheQty = New System.Windows.Forms.Label()
        Me.lblMainActiveRow = New System.Windows.Forms.Label()
        Me.lblSuppCode = New System.Windows.Forms.Label()
        Me.lblItemCode = New System.Windows.Forms.Label()
        Me.lblPoNo = New System.Windows.Forms.Label()
        Me.LblModifyMode = New System.Windows.Forms.Label()
        Me.LblAddMode = New System.Windows.Forms.Label()
        Me.LblPODate = New System.Windows.Forms.Label()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.Frame2.SuspendLayout()
        CType(Me.SprdDlv, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.cmdOk.TabIndex = 7
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
        Me.CmdClose.TabIndex = 5
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close the Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.cmdMonth)
        Me.Frame2.Controls.Add(Me.txtMonthDeliverySchedule)
        Me.Frame2.Controls.Add(Me.cmd2Month)
        Me.Frame2.Controls.Add(Me.cmd2Week)
        Me.Frame2.Controls.Add(Me.cmd1Week)
        Me.Frame2.Controls.Add(Me.CmdDaily)
        Me.Frame2.Controls.Add(Me.SprdDlv)
        Me.Frame2.Controls.Add(Me.lblWeek5Qty)
        Me.Frame2.Controls.Add(Me.lblWeek4Qty)
        Me.Frame2.Controls.Add(Me.lblWeek3Qty)
        Me.Frame2.Controls.Add(Me.lblWeek2Qty)
        Me.Frame2.Controls.Add(Me.lblWeek1Qty)
        Me.Frame2.Controls.Add(Me.lblWeek5)
        Me.Frame2.Controls.Add(Me.lblWeek4)
        Me.Frame2.Controls.Add(Me.lblWeek3)
        Me.Frame2.Controls.Add(Me.lblWeek2)
        Me.Frame2.Controls.Add(Me.lblWeek1)
        Me.Frame2.Controls.Add(Me.lbl1)
        Me.Frame2.Controls.Add(Me.lblActual)
        Me.Frame2.Controls.Add(Me.lblPlanQty)
        Me.Frame2.Controls.Add(Me.lblTotal)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, -2)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(527, 327)
        Me.Frame2.TabIndex = 0
        Me.Frame2.TabStop = False
        '
        'cmdMonth
        '
        Me.cmdMonth.BackColor = System.Drawing.SystemColors.Control
        Me.cmdMonth.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdMonth.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdMonth.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMonth.Location = New System.Drawing.Point(10, 176)
        Me.cmdMonth.Name = "cmdMonth"
        Me.cmdMonth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdMonth.Size = New System.Drawing.Size(125, 29)
        Me.cmdMonth.TabIndex = 31
        Me.cmdMonth.Text = "Month's"
        Me.cmdMonth.UseVisualStyleBackColor = False
        '
        'txtMonthDeliverySchedule
        '
        Me.txtMonthDeliverySchedule.AcceptsReturn = True
        Me.txtMonthDeliverySchedule.BackColor = System.Drawing.SystemColors.Window
        Me.txtMonthDeliverySchedule.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMonthDeliverySchedule.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMonthDeliverySchedule.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMonthDeliverySchedule.Location = New System.Drawing.Point(14, 34)
        Me.txtMonthDeliverySchedule.MaxLength = 0
        Me.txtMonthDeliverySchedule.Name = "txtMonthDeliverySchedule"
        Me.txtMonthDeliverySchedule.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMonthDeliverySchedule.Size = New System.Drawing.Size(117, 20)
        Me.txtMonthDeliverySchedule.TabIndex = 29
        Me.txtMonthDeliverySchedule.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmd2Month
        '
        Me.cmd2Month.BackColor = System.Drawing.SystemColors.Control
        Me.cmd2Month.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmd2Month.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd2Month.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmd2Month.Location = New System.Drawing.Point(10, 148)
        Me.cmd2Month.Name = "cmd2Month"
        Me.cmd2Month.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmd2Month.Size = New System.Drawing.Size(125, 29)
        Me.cmd2Month.TabIndex = 17
        Me.cmd2Month.Text = "2's Month"
        Me.cmd2Month.UseVisualStyleBackColor = False
        '
        'cmd2Week
        '
        Me.cmd2Week.BackColor = System.Drawing.SystemColors.Control
        Me.cmd2Week.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmd2Week.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd2Week.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmd2Week.Location = New System.Drawing.Point(10, 120)
        Me.cmd2Week.Name = "cmd2Week"
        Me.cmd2Week.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmd2Week.Size = New System.Drawing.Size(125, 29)
        Me.cmd2Week.TabIndex = 16
        Me.cmd2Week.Text = "2's Week"
        Me.cmd2Week.UseVisualStyleBackColor = False
        '
        'cmd1Week
        '
        Me.cmd1Week.BackColor = System.Drawing.SystemColors.Control
        Me.cmd1Week.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmd1Week.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd1Week.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmd1Week.Location = New System.Drawing.Point(10, 92)
        Me.cmd1Week.Name = "cmd1Week"
        Me.cmd1Week.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmd1Week.Size = New System.Drawing.Size(125, 29)
        Me.cmd1Week.TabIndex = 15
        Me.cmd1Week.Text = "1's Week"
        Me.cmd1Week.UseVisualStyleBackColor = False
        '
        'CmdDaily
        '
        Me.CmdDaily.BackColor = System.Drawing.SystemColors.Control
        Me.CmdDaily.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdDaily.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdDaily.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdDaily.Location = New System.Drawing.Point(10, 64)
        Me.CmdDaily.Name = "CmdDaily"
        Me.CmdDaily.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDaily.Size = New System.Drawing.Size(125, 29)
        Me.CmdDaily.TabIndex = 14
        Me.CmdDaily.Text = "Daily"
        Me.CmdDaily.UseVisualStyleBackColor = False
        '
        'SprdDlv
        '
        Me.SprdDlv.DataSource = Nothing
        Me.SprdDlv.Location = New System.Drawing.Point(142, 8)
        Me.SprdDlv.Name = "SprdDlv"
        Me.SprdDlv.OcxState = CType(resources.GetObject("SprdDlv.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdDlv.Size = New System.Drawing.Size(381, 295)
        Me.SprdDlv.TabIndex = 30
        '
        'lblWeek5Qty
        '
        Me.lblWeek5Qty.AutoSize = True
        Me.lblWeek5Qty.BackColor = System.Drawing.SystemColors.Control
        Me.lblWeek5Qty.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblWeek5Qty.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWeek5Qty.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblWeek5Qty.Location = New System.Drawing.Point(8, 286)
        Me.lblWeek5Qty.Name = "lblWeek5Qty"
        Me.lblWeek5Qty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblWeek5Qty.Size = New System.Drawing.Size(49, 14)
        Me.lblWeek5Qty.TabIndex = 27
        Me.lblWeek5Qty.Text = "Week 5 :"
        Me.lblWeek5Qty.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblWeek4Qty
        '
        Me.lblWeek4Qty.AutoSize = True
        Me.lblWeek4Qty.BackColor = System.Drawing.SystemColors.Control
        Me.lblWeek4Qty.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblWeek4Qty.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWeek4Qty.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblWeek4Qty.Location = New System.Drawing.Point(8, 268)
        Me.lblWeek4Qty.Name = "lblWeek4Qty"
        Me.lblWeek4Qty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblWeek4Qty.Size = New System.Drawing.Size(49, 14)
        Me.lblWeek4Qty.TabIndex = 26
        Me.lblWeek4Qty.Text = "Week 4 :"
        Me.lblWeek4Qty.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblWeek3Qty
        '
        Me.lblWeek3Qty.AutoSize = True
        Me.lblWeek3Qty.BackColor = System.Drawing.SystemColors.Control
        Me.lblWeek3Qty.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblWeek3Qty.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWeek3Qty.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblWeek3Qty.Location = New System.Drawing.Point(8, 252)
        Me.lblWeek3Qty.Name = "lblWeek3Qty"
        Me.lblWeek3Qty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblWeek3Qty.Size = New System.Drawing.Size(49, 14)
        Me.lblWeek3Qty.TabIndex = 25
        Me.lblWeek3Qty.Text = "Week 3 :"
        Me.lblWeek3Qty.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblWeek2Qty
        '
        Me.lblWeek2Qty.AutoSize = True
        Me.lblWeek2Qty.BackColor = System.Drawing.SystemColors.Control
        Me.lblWeek2Qty.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblWeek2Qty.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWeek2Qty.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblWeek2Qty.Location = New System.Drawing.Point(8, 232)
        Me.lblWeek2Qty.Name = "lblWeek2Qty"
        Me.lblWeek2Qty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblWeek2Qty.Size = New System.Drawing.Size(49, 14)
        Me.lblWeek2Qty.TabIndex = 24
        Me.lblWeek2Qty.Text = "Week 2 :"
        Me.lblWeek2Qty.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblWeek1Qty
        '
        Me.lblWeek1Qty.AutoSize = True
        Me.lblWeek1Qty.BackColor = System.Drawing.SystemColors.Control
        Me.lblWeek1Qty.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblWeek1Qty.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWeek1Qty.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblWeek1Qty.Location = New System.Drawing.Point(8, 214)
        Me.lblWeek1Qty.Name = "lblWeek1Qty"
        Me.lblWeek1Qty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblWeek1Qty.Size = New System.Drawing.Size(49, 14)
        Me.lblWeek1Qty.TabIndex = 23
        Me.lblWeek1Qty.Text = "Week 1 :"
        Me.lblWeek1Qty.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblWeek5
        '
        Me.lblWeek5.BackColor = System.Drawing.SystemColors.Control
        Me.lblWeek5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblWeek5.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblWeek5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWeek5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblWeek5.Location = New System.Drawing.Point(64, 284)
        Me.lblWeek5.Name = "lblWeek5"
        Me.lblWeek5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblWeek5.Size = New System.Drawing.Size(70, 17)
        Me.lblWeek5.TabIndex = 22
        Me.lblWeek5.Text = "0.00"
        Me.lblWeek5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblWeek4
        '
        Me.lblWeek4.BackColor = System.Drawing.SystemColors.Control
        Me.lblWeek4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblWeek4.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblWeek4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWeek4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblWeek4.Location = New System.Drawing.Point(64, 266)
        Me.lblWeek4.Name = "lblWeek4"
        Me.lblWeek4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblWeek4.Size = New System.Drawing.Size(70, 17)
        Me.lblWeek4.TabIndex = 21
        Me.lblWeek4.Text = "0.00"
        Me.lblWeek4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblWeek3
        '
        Me.lblWeek3.BackColor = System.Drawing.SystemColors.Control
        Me.lblWeek3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblWeek3.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblWeek3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWeek3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblWeek3.Location = New System.Drawing.Point(64, 248)
        Me.lblWeek3.Name = "lblWeek3"
        Me.lblWeek3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblWeek3.Size = New System.Drawing.Size(70, 17)
        Me.lblWeek3.TabIndex = 20
        Me.lblWeek3.Text = "0.00"
        Me.lblWeek3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblWeek2
        '
        Me.lblWeek2.BackColor = System.Drawing.SystemColors.Control
        Me.lblWeek2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblWeek2.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblWeek2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWeek2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblWeek2.Location = New System.Drawing.Point(64, 230)
        Me.lblWeek2.Name = "lblWeek2"
        Me.lblWeek2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblWeek2.Size = New System.Drawing.Size(70, 17)
        Me.lblWeek2.TabIndex = 19
        Me.lblWeek2.Text = "0.00"
        Me.lblWeek2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblWeek1
        '
        Me.lblWeek1.BackColor = System.Drawing.SystemColors.Control
        Me.lblWeek1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblWeek1.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblWeek1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWeek1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblWeek1.Location = New System.Drawing.Point(64, 212)
        Me.lblWeek1.Name = "lblWeek1"
        Me.lblWeek1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblWeek1.Size = New System.Drawing.Size(70, 17)
        Me.lblWeek1.TabIndex = 18
        Me.lblWeek1.Text = "0.00"
        Me.lblWeek1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lbl1
        '
        Me.lbl1.BackColor = System.Drawing.SystemColors.Control
        Me.lbl1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbl1.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbl1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbl1.Location = New System.Drawing.Point(14, 10)
        Me.lbl1.Name = "lbl1"
        Me.lbl1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbl1.Size = New System.Drawing.Size(117, 23)
        Me.lbl1.TabIndex = 13
        '
        'lblActual
        '
        Me.lblActual.BackColor = System.Drawing.SystemColors.Control
        Me.lblActual.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblActual.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblActual.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblActual.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblActual.Location = New System.Drawing.Point(418, 306)
        Me.lblActual.Name = "lblActual"
        Me.lblActual.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblActual.Size = New System.Drawing.Size(85, 19)
        Me.lblActual.TabIndex = 12
        Me.lblActual.Text = "0.00"
        Me.lblActual.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblPlanQty
        '
        Me.lblPlanQty.BackColor = System.Drawing.SystemColors.Control
        Me.lblPlanQty.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPlanQty.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPlanQty.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPlanQty.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPlanQty.Location = New System.Drawing.Point(332, 306)
        Me.lblPlanQty.Name = "lblPlanQty"
        Me.lblPlanQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPlanQty.Size = New System.Drawing.Size(85, 19)
        Me.lblPlanQty.TabIndex = 11
        Me.lblPlanQty.Text = "0.00"
        Me.lblPlanQty.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotal
        '
        Me.lblTotal.AutoSize = True
        Me.lblTotal.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotal.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotal.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotal.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTotal.Location = New System.Drawing.Point(292, 308)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotal.Size = New System.Drawing.Size(35, 14)
        Me.lblTotal.TabIndex = 10
        Me.lblTotal.Text = "Total :"
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.lblBookType)
        Me.Frame3.Controls.Add(Me.lblDI)
        Me.Frame3.Controls.Add(Me.lblStoreLoc)
        Me.Frame3.Controls.Add(Me.cmdOk)
        Me.Frame3.Controls.Add(Me.CmdClose)
        Me.Frame3.Controls.Add(Me.LblTempSeq)
        Me.Frame3.Controls.Add(Me.lblScheQty)
        Me.Frame3.Controls.Add(Me.lblMainActiveRow)
        Me.Frame3.Controls.Add(Me.lblSuppCode)
        Me.Frame3.Controls.Add(Me.lblItemCode)
        Me.Frame3.Controls.Add(Me.lblPoNo)
        Me.Frame3.Controls.Add(Me.LblModifyMode)
        Me.Frame3.Controls.Add(Me.LblAddMode)
        Me.Frame3.Controls.Add(Me.LblPODate)
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
        'lblDI
        '
        Me.lblDI.AutoSize = True
        Me.lblDI.Location = New System.Drawing.Point(232, 30)
        Me.lblDI.Name = "lblDI"
        Me.lblDI.Size = New System.Drawing.Size(26, 14)
        Me.lblDI.TabIndex = 35
        Me.lblDI.Text = "lblDI"
        '
        'lblStoreLoc
        '
        Me.lblStoreLoc.AutoSize = True
        Me.lblStoreLoc.Location = New System.Drawing.Point(332, 28)
        Me.lblStoreLoc.Name = "lblStoreLoc"
        Me.lblStoreLoc.Size = New System.Drawing.Size(61, 14)
        Me.lblStoreLoc.TabIndex = 34
        Me.lblStoreLoc.Text = "lblStoreLoc"
        '
        'LblTempSeq
        '
        Me.LblTempSeq.BackColor = System.Drawing.SystemColors.Control
        Me.LblTempSeq.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblTempSeq.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTempSeq.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblTempSeq.Location = New System.Drawing.Point(230, 12)
        Me.LblTempSeq.Name = "LblTempSeq"
        Me.LblTempSeq.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblTempSeq.Size = New System.Drawing.Size(59, 11)
        Me.LblTempSeq.TabIndex = 33
        Me.LblTempSeq.Text = "LblTempSeq"
        '
        'lblScheQty
        '
        Me.lblScheQty.BackColor = System.Drawing.SystemColors.Control
        Me.lblScheQty.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblScheQty.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblScheQty.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblScheQty.Location = New System.Drawing.Point(366, 28)
        Me.lblScheQty.Name = "lblScheQty"
        Me.lblScheQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblScheQty.Size = New System.Drawing.Size(37, 15)
        Me.lblScheQty.TabIndex = 32
        Me.lblScheQty.Text = "lblScheQty"
        '
        'lblMainActiveRow
        '
        Me.lblMainActiveRow.BackColor = System.Drawing.SystemColors.Control
        Me.lblMainActiveRow.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMainActiveRow.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMainActiveRow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMainActiveRow.Location = New System.Drawing.Point(412, 16)
        Me.lblMainActiveRow.Name = "lblMainActiveRow"
        Me.lblMainActiveRow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMainActiveRow.Size = New System.Drawing.Size(45, 13)
        Me.lblMainActiveRow.TabIndex = 28
        Me.lblMainActiveRow.Text = "MainActiveRow"
        Me.lblMainActiveRow.Visible = False
        '
        'lblSuppCode
        '
        Me.lblSuppCode.AutoSize = True
        Me.lblSuppCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblSuppCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSuppCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSuppCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSuppCode.Location = New System.Drawing.Point(84, 30)
        Me.lblSuppCode.Name = "lblSuppCode"
        Me.lblSuppCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSuppCode.Size = New System.Drawing.Size(67, 14)
        Me.lblSuppCode.TabIndex = 9
        Me.lblSuppCode.Text = "lblSuppCode"
        Me.lblSuppCode.Visible = False
        '
        'lblItemCode
        '
        Me.lblItemCode.AutoSize = True
        Me.lblItemCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblItemCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblItemCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItemCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblItemCode.Location = New System.Drawing.Point(86, 16)
        Me.lblItemCode.Name = "lblItemCode"
        Me.lblItemCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblItemCode.Size = New System.Drawing.Size(61, 14)
        Me.lblItemCode.TabIndex = 8
        Me.lblItemCode.Text = "lblItemCode"
        Me.lblItemCode.Visible = False
        '
        'lblPoNo
        '
        Me.lblPoNo.AutoSize = True
        Me.lblPoNo.BackColor = System.Drawing.Color.Transparent
        Me.lblPoNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPoNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPoNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPoNo.Location = New System.Drawing.Point(186, 28)
        Me.lblPoNo.Name = "lblPoNo"
        Me.lblPoNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPoNo.Size = New System.Drawing.Size(34, 14)
        Me.lblPoNo.TabIndex = 6
        Me.lblPoNo.Text = "PONo"
        Me.lblPoNo.Visible = False
        '
        'LblModifyMode
        '
        Me.LblModifyMode.AutoSize = True
        Me.LblModifyMode.BackColor = System.Drawing.SystemColors.Control
        Me.LblModifyMode.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblModifyMode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblModifyMode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblModifyMode.Location = New System.Drawing.Point(308, 14)
        Me.LblModifyMode.Name = "LblModifyMode"
        Me.LblModifyMode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblModifyMode.Size = New System.Drawing.Size(79, 14)
        Me.LblModifyMode.TabIndex = 4
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
        Me.LblAddMode.Location = New System.Drawing.Point(264, 22)
        Me.LblAddMode.Name = "LblAddMode"
        Me.LblAddMode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblAddMode.Size = New System.Drawing.Size(67, 14)
        Me.LblAddMode.TabIndex = 3
        Me.LblAddMode.Text = "LblAddMode"
        Me.LblAddMode.Visible = False
        '
        'LblPODate
        '
        Me.LblPODate.AutoSize = True
        Me.LblPODate.BackColor = System.Drawing.SystemColors.Control
        Me.LblPODate.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblPODate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPODate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblPODate.Location = New System.Drawing.Point(186, 14)
        Me.LblPODate.Name = "LblPODate"
        Me.LblPODate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblPODate.Size = New System.Drawing.Size(43, 14)
        Me.LblPODate.TabIndex = 2
        Me.LblPODate.Text = "PODate"
        Me.LblPODate.Visible = False
        '
        'lblBookType
        '
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(243, 17)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(41, 15)
        Me.lblBookType.TabIndex = 36
        Me.lblBookType.Text = "lblBookType"
        Me.lblBookType.Visible = False
        '
        'FrmSalesDSDailyDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(528, 370)
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
        Me.Name = "FrmSalesDSDailyDetail"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Daily Sales Delivery Schedule Details"
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        CType(Me.SprdDlv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lblStoreLoc As Label
    Friend WithEvents lblDI As Label
    Public WithEvents lblBookType As Label
#End Region
End Class