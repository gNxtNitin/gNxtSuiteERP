Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmESIChallan
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
    Public WithEvents cboEmployee As System.Windows.Forms.ComboBox
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents UpDYear As System.Windows.Forms.NumericUpDown     ''AxComCtl2.AxUpDown
    Public WithEvents UpDMonth As System.Windows.Forms.NumericUpDown     ''AxComCtl2.AxUpDown
    Public WithEvents txtMonth As System.Windows.Forms.TextBox
    Public WithEvents TxtYear As System.Windows.Forms.TextBox
    Public WithEvents txtChallanDate As System.Windows.Forms.TextBox
    Public WithEvents txtEmpShare As System.Windows.Forms.TextBox
    Public WithEvents txtEmperShare As System.Windows.Forms.TextBox
    Public WithEvents txtTotAmount As System.Windows.Forms.TextBox
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents fraMain As System.Windows.Forms.GroupBox
    Public WithEvents cmdOk As System.Windows.Forms.Button
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents lblNewDate As System.Windows.Forms.Label
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmESIChallan))
        Me.components = New System.ComponentModel.Container()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
        Me.Frame4 = New System.Windows.Forms.GroupBox
        Me.cboEmployee = New System.Windows.Forms.ComboBox
        Me.fraMain = New System.Windows.Forms.GroupBox
        Me.UpDYear = New System.Windows.Forms.NumericUpDown     ''AxComCtl2.AxUpDown
        Me.UpDMonth = New System.Windows.Forms.NumericUpDown     ''AxComCtl2.AxUpDown
        Me.txtMonth = New System.Windows.Forms.TextBox
        Me.TxtYear = New System.Windows.Forms.TextBox
        Me.txtChallanDate = New System.Windows.Forms.TextBox
        Me.txtEmpShare = New System.Windows.Forms.TextBox
        Me.txtEmperShare = New System.Windows.Forms.TextBox
        Me.txtTotAmount = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.FraMovement = New System.Windows.Forms.GroupBox
        Me.cmdOk = New System.Windows.Forms.Button
        Me.CmdClose = New System.Windows.Forms.Button
        Me.lblNewDate = New System.Windows.Forms.Label
        Me.Frame4.SuspendLayout()
        Me.fraMain.SuspendLayout()
        Me.FraMovement.SuspendLayout()
        Me.SuspendLayout()
        Me.ToolTip1.Active = True
        CType(Me.UpDYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UpDMonth, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Text = "ESI Challan Detail"
        Me.ClientSize = New System.Drawing.Size(292, 250)
        Me.Location = New System.Drawing.Point(21, 91)
        Me.Icon = CType(resources.GetObject("frmESIChallan.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.ShowInTaskbar = False
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ControlBox = True
        Me.Enabled = True
        Me.KeyPreview = False
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.HelpButton = False
        Me.WindowState = System.Windows.Forms.FormWindowState.Normal
        Me.Name = "frmESIChallan"
        Me.Frame4.Size = New System.Drawing.Size(291, 45)
        Me.Frame4.Location = New System.Drawing.Point(0, 0)
        Me.Frame4.TabIndex = 19
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Enabled = True
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Visible = True
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.Name = "Frame4"
        Me.cboEmployee.Size = New System.Drawing.Size(279, 21)
        Me.cboEmployee.Location = New System.Drawing.Point(6, 16)
        Me.cboEmployee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEmployee.TabIndex = 20
        Me.cboEmployee.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboEmployee.BackColor = System.Drawing.SystemColors.Window
        Me.cboEmployee.CausesValidation = True
        Me.cboEmployee.Enabled = True
        Me.cboEmployee.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboEmployee.IntegralHeight = True
        Me.cboEmployee.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboEmployee.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboEmployee.Sorted = False
        Me.cboEmployee.TabStop = True
        Me.cboEmployee.Visible = True
        Me.cboEmployee.Name = "cboEmployee"
        Me.fraMain.Size = New System.Drawing.Size(291, 163)
        Me.fraMain.Location = New System.Drawing.Point(0, 40)
        Me.fraMain.TabIndex = 0
        Me.fraMain.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraMain.BackColor = System.Drawing.SystemColors.Control
        Me.fraMain.Enabled = True
        Me.fraMain.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraMain.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraMain.Visible = True
        Me.fraMain.Padding = New System.Windows.Forms.Padding(0)
        Me.fraMain.Name = "fraMain"
        'UpDYear.OcxState = CType(resources.GetObject("UpDYear.OcxState"), System.Windows.Forms.AxHost.State)
        Me.UpDYear.Size = New System.Drawing.Size(16, 19)
        Me.UpDYear.Location = New System.Drawing.Point(266, 18)
        Me.UpDYear.TabIndex = 14
        Me.UpDYear.Name = "UpDYear"
        'UpDMonth.OcxState = CType(resources.GetObject("UpDMonth.OcxState"), System.Windows.Forms.AxHost.State)
        Me.UpDMonth.Size = New System.Drawing.Size(16, 19)
        Me.UpDMonth.Location = New System.Drawing.Point(140, 18)
        Me.UpDMonth.TabIndex = 15
        Me.UpDMonth.Name = "UpDMonth"
        Me.txtMonth.AutoSize = False
        Me.txtMonth.Enabled = False
        Me.txtMonth.Size = New System.Drawing.Size(95, 19)
        Me.txtMonth.Location = New System.Drawing.Point(62, 18)
        Me.txtMonth.TabIndex = 17
        Me.txtMonth.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMonth.AcceptsReturn = True
        Me.txtMonth.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtMonth.BackColor = System.Drawing.SystemColors.Window
        Me.txtMonth.CausesValidation = True
        Me.txtMonth.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMonth.HideSelection = True
        Me.txtMonth.ReadOnly = False
        Me.txtMonth.Maxlength = 0
        Me.txtMonth.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMonth.MultiLine = False
        Me.txtMonth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMonth.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtMonth.TabStop = True
        Me.txtMonth.Visible = True
        Me.txtMonth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMonth.Name = "txtMonth"
        Me.TxtYear.AutoSize = False
        Me.TxtYear.Enabled = False
        Me.TxtYear.Size = New System.Drawing.Size(69, 19)
        Me.TxtYear.Location = New System.Drawing.Point(214, 18)
        Me.TxtYear.TabIndex = 16
        Me.TxtYear.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtYear.AcceptsReturn = True
        Me.TxtYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.TxtYear.BackColor = System.Drawing.SystemColors.Window
        Me.TxtYear.CausesValidation = True
        Me.TxtYear.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtYear.HideSelection = True
        Me.TxtYear.ReadOnly = False
        Me.TxtYear.Maxlength = 0
        Me.TxtYear.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtYear.MultiLine = False
        Me.TxtYear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtYear.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.TxtYear.TabStop = True
        Me.TxtYear.Visible = True
        Me.TxtYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtYear.Name = "TxtYear"
        Me.txtChallanDate.AutoSize = False
        Me.txtChallanDate.Size = New System.Drawing.Size(167, 19)
        Me.txtChallanDate.Location = New System.Drawing.Point(117, 132)
        Me.txtChallanDate.TabIndex = 13
        Me.txtChallanDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChallanDate.AcceptsReturn = True
        Me.txtChallanDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtChallanDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtChallanDate.CausesValidation = True
        Me.txtChallanDate.Enabled = True
        Me.txtChallanDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtChallanDate.HideSelection = True
        Me.txtChallanDate.ReadOnly = False
        Me.txtChallanDate.Maxlength = 0
        Me.txtChallanDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtChallanDate.MultiLine = False
        Me.txtChallanDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtChallanDate.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtChallanDate.TabStop = True
        Me.txtChallanDate.Visible = True
        Me.txtChallanDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtChallanDate.Name = "txtChallanDate"
        Me.txtEmpShare.AutoSize = False
        Me.txtEmpShare.Size = New System.Drawing.Size(167, 19)
        Me.txtEmpShare.Location = New System.Drawing.Point(117, 74)
        Me.txtEmpShare.TabIndex = 12
        Me.txtEmpShare.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpShare.AcceptsReturn = True
        Me.txtEmpShare.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtEmpShare.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmpShare.CausesValidation = True
        Me.txtEmpShare.Enabled = True
        Me.txtEmpShare.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEmpShare.HideSelection = True
        Me.txtEmpShare.ReadOnly = False
        Me.txtEmpShare.Maxlength = 0
        Me.txtEmpShare.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmpShare.MultiLine = False
        Me.txtEmpShare.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmpShare.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtEmpShare.TabStop = True
        Me.txtEmpShare.Visible = True
        Me.txtEmpShare.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmpShare.Name = "txtEmpShare"
        Me.txtEmperShare.AutoSize = False
        Me.txtEmperShare.Size = New System.Drawing.Size(167, 19)
        Me.txtEmperShare.Location = New System.Drawing.Point(117, 46)
        Me.txtEmperShare.TabIndex = 11
        Me.txtEmperShare.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmperShare.AcceptsReturn = True
        Me.txtEmperShare.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtEmperShare.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmperShare.CausesValidation = True
        Me.txtEmperShare.Enabled = True
        Me.txtEmperShare.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEmperShare.HideSelection = True
        Me.txtEmperShare.ReadOnly = False
        Me.txtEmperShare.Maxlength = 0
        Me.txtEmperShare.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmperShare.MultiLine = False
        Me.txtEmperShare.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmperShare.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtEmperShare.TabStop = True
        Me.txtEmperShare.Visible = True
        Me.txtEmperShare.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmperShare.Name = "txtEmperShare"
        Me.txtTotAmount.AutoSize = False
        Me.txtTotAmount.Enabled = False
        Me.txtTotAmount.Size = New System.Drawing.Size(167, 19)
        Me.txtTotAmount.Location = New System.Drawing.Point(117, 102)
        Me.txtTotAmount.TabIndex = 10
        Me.txtTotAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotAmount.AcceptsReturn = True
        Me.txtTotAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtTotAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotAmount.CausesValidation = True
        Me.txtTotAmount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotAmount.HideSelection = True
        Me.txtTotAmount.ReadOnly = False
        Me.txtTotAmount.Maxlength = 0
        Me.txtTotAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotAmount.MultiLine = False
        Me.txtTotAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotAmount.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtTotAmount.TabStop = True
        Me.txtTotAmount.Visible = True
        Me.txtTotAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotAmount.Name = "txtTotAmount"
        Me.Label6.Text = "Challan Date :"
        Me.Label6.Size = New System.Drawing.Size(67, 13)
        Me.Label6.Location = New System.Drawing.Point(10, 130)
        Me.Label6.TabIndex = 9
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopLeft
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
        Me.Label5.Text = "Employee's Share :"
        Me.Label5.Size = New System.Drawing.Size(90, 13)
        Me.Label5.Location = New System.Drawing.Point(10, 80)
        Me.Label5.TabIndex = 8
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopLeft
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
        Me.Label4.Text = "Employer's Share :"
        Me.Label4.Size = New System.Drawing.Size(87, 13)
        Me.Label4.Location = New System.Drawing.Point(10, 50)
        Me.Label4.TabIndex = 7
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopLeft
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
        Me.Label3.Text = "Total Paid Amount :"
        Me.Label3.Size = New System.Drawing.Size(93, 13)
        Me.Label3.Location = New System.Drawing.Point(10, 104)
        Me.Label3.TabIndex = 6
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Enabled = True
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.UseMnemonic = True
        Me.Label3.Visible = True
        Me.Label3.AutoSize = True
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Label3.Name = "Label3"
        Me.Label2.Text = "Year :"
        Me.Label2.Size = New System.Drawing.Size(28, 13)
        Me.Label2.Location = New System.Drawing.Point(176, 18)
        Me.Label2.TabIndex = 5
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.Label1.Text = "Month :"
        Me.Label1.Size = New System.Drawing.Size(36, 13)
        Me.Label1.Location = New System.Drawing.Point(10, 20)
        Me.Label1.TabIndex = 4
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.FraMovement.Size = New System.Drawing.Size(291, 51)
        Me.FraMovement.Location = New System.Drawing.Point(0, 198)
        Me.FraMovement.TabIndex = 1
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Enabled = True
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Visible = True
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.Name = "FraMovement"
        Me.cmdOk.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdOk.Text = "&Ok"
        Me.cmdOk.Size = New System.Drawing.Size(63, 34)
        Me.cmdOk.Location = New System.Drawing.Point(4, 12)
        Me.cmdOk.Image = CType(resources.GetObject("cmdOk.Image"), System.Drawing.Image)
        Me.cmdOk.TabIndex = 3
        Me.cmdOk.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.CmdClose.Size = New System.Drawing.Size(63, 34)
        Me.CmdClose.Location = New System.Drawing.Point(212, 12)
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close Form")
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.CausesValidation = True
        Me.CmdClose.Enabled = True
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.TabStop = True
        Me.CmdClose.Name = "CmdClose"
        Me.lblNewDate.Text = "NewDate"
        Me.lblNewDate.Size = New System.Drawing.Size(63, 17)
        Me.lblNewDate.Location = New System.Drawing.Point(80, 18)
        Me.lblNewDate.TabIndex = 18
        Me.lblNewDate.Visible = False
        Me.lblNewDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNewDate.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblNewDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblNewDate.Enabled = True
        Me.lblNewDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblNewDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblNewDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblNewDate.UseMnemonic = True
        Me.lblNewDate.AutoSize = False
        Me.lblNewDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblNewDate.Name = "lblNewDate"
        Me.Controls.Add(Frame4)
        Me.Controls.Add(fraMain)
        Me.Controls.Add(FraMovement)
        Me.Frame4.Controls.Add(cboEmployee)
        Me.fraMain.Controls.Add(UpDYear)
        Me.fraMain.Controls.Add(UpDMonth)
        Me.fraMain.Controls.Add(txtMonth)
        Me.fraMain.Controls.Add(TxtYear)
        Me.fraMain.Controls.Add(txtChallanDate)
        Me.fraMain.Controls.Add(txtEmpShare)
        Me.fraMain.Controls.Add(txtEmperShare)
        Me.fraMain.Controls.Add(txtTotAmount)
        Me.fraMain.Controls.Add(Label6)
        Me.fraMain.Controls.Add(Label5)
        Me.fraMain.Controls.Add(Label4)
        Me.fraMain.Controls.Add(Label3)
        Me.fraMain.Controls.Add(Label2)
        Me.fraMain.Controls.Add(Label1)
        Me.FraMovement.Controls.Add(cmdOk)
        Me.FraMovement.Controls.Add(CmdClose)
        Me.FraMovement.Controls.Add(lblNewDate)
        CType(Me.UpDMonth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UpDYear, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame4.ResumeLayout(False)
        Me.fraMain.ResumeLayout(False)
        Me.FraMovement.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub
#End Region
End Class